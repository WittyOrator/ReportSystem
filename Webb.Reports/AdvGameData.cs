using System;
using System.Data;
using System.Collections;
using System.Collections.Specialized;
using System.Text;

using WEBBGAMEDATALib;
using Webb.Reports.DataProvider;
using Webb.Data;
using System.Reflection;
using Microsoft.Win32;
using System.Text;
using System.Drawing;

namespace Webb.Reports
{
	public class AdvGameData : IDisposable
	{
		private AdvGameCollection _AdvGames;
		private AdvEdlCollection _AdvEdls;
		private DataSet _DataSource;
		private StringCollection _AvailableFields;
		private StringCollection _AvailableGameInEdl;
		private AdvGameInEdlInfoCollection _GamesInEdl;
		private string _UserFolder;
		private string _HeaderName;
		private StringCollection _Filters;
		private string _ConnStr;
		private IDBManager _DBManager;
		private IDBManager _SecDBManager;
		private DBSourceConfig _DBSourceConfig;

		public bool FileMode	//Modified at 2008-10-13 15:26:57@Scott
		{
			get{return this._ConnStr == null || this._ConnStr == string.Empty || this._ConnStr == "?";}
		}

		public bool DBMode		//Modified at 2008-10-13 15:27:02@Scott
		{
			get{return this._ConnStr != null && this._ConnStr != string.Empty && this._ConnStr != "?";}
		}

		internal class ConstValue
		{
			public const string Table = "Table";
			public const string VideoInfo = "VideoInfo";
			public const string PlayID = "RepPlayID";
			public const string StartFrame = "Start Frame";
			public const string EndFrame = "End Frame";
			public const string VideoFilePath = "Video File Path";
			public const string Angle = "Angle";
			public const string PlayNum = "Play Number";	//05-06-2008@Scott
			public const string GameName = "Game Name";		//05-06-2008@Scott
			public const string MasterNum = "Master Num";	//05-07-2008@Scott

			public const string Cmd_Adv_Football_Game = @"SELECT * FROM dbo.GameDetails WHERE GameID IN (SELECT GameID FROM Games WHERE (ScoutTypeID IN (SELECT ScoutTypeID FROM dbo.ScoutTypes WHERE ScoutTypeName = '{0}'))"
				+ @" AND (Object + ' VS ' + Opponent + ' ON ' + Date + ' AT ' + Location = '{1}'))";
			public const string Cmd_Adv_Football_VideoInfo = @"SELECT *,'{0}' AS GameName FROM LogTapes LEFT JOIN LogDetails ON LogTapes.LogTapeID = LogDetails.LogTapeID WHERE GameID IN (SELECT GameID FROM Games WHERE (ScoutTypeID IN (SELECT ScoutTypeID FROM dbo.ScoutTypes WHERE ScoutTypeName = '{1}'))"
				+ @" AND (Object + ' VS ' + Opponent + ' ON ' + Date + ' AT ' + Location = '{2}'))";
			public const string Cmd_Adv_Football_Edl_VideoInfo = @"SELECT *,'{0}' AS GameName FROM LogTapes LEFT JOIN LogDetails ON LogTapes.LogTapeID = LogDetails.LogTapeID"
				+ @" WHERE LogTapes.GameID IN ({1}) AND (LogDetails.sysid = {2})";
			public const string Cmd_Adv_Football_Edl = @"SELECT * FROM GameDetails WHERE GameID IN ({0}) AND sysid = {1}";
			public const string Cmd_Adv_GameID = @"SELECT GameID FROM Games WHERE (ScoutTypeID IN (SELECT ScoutTypeID FROM dbo.ScoutTypes WHERE ScoutTypeName = '{0}'))"
				+ @" AND (Object + ' VS ' + Opponent + ' ON ' + Date + ' AT ' + Location = '{1}')";
		}

		public DataSet DataSource
		{
			get{return this._DataSource;}
		}

		//ctor
		public AdvGameData(DBSourceConfig config)
		{
			this._DBSourceConfig = new DBSourceConfig(config);	//Modified at 2008-10-13 16:34:26@Scott

			if(config.ConnString != null && config.ConnString != "?")
			{
				this._ConnStr = config.ConnString;

				string[] arrConn = config.ConnString.Split('?');

				//First
				if(arrConn.Length > 0)
				{
					string strFirstConn = arrConn[0];

					int index = strFirstConn.IndexOf(";");

					if(index > 0)
					{
						strFirstConn = strFirstConn.Remove(0,index);

						if(this._DBManager!=null)
						{
							this._DBManager.Dispose();

							this._DBManager = null;
						}

						this._DBManager = Webb.Data.DBHelper.NewDBManager(DBConnTypes.SQLDB,strFirstConn);
					}
				}

				//Second
				if(arrConn.Length > 1)
				{
					string strSecConn = arrConn[1];

					int index = strSecConn.IndexOf(";");

					if(index > 0)
					{
						strSecConn = strSecConn.Remove(0,index);

						if(this._SecDBManager!=null)
						{
							this._SecDBManager.Dispose();

							this._SecDBManager = null;
						}

						this._SecDBManager = Webb.Data.DBHelper.NewDBManager(DBConnTypes.SQLDB,strSecConn);
					}
				}
			}

			this._DataSource = new DataSet("DataSource");

			this._UserFolder = config.UserFolder;

			this._HeaderName = config.HeaderName;

			this._Filters = config.Filters;

			if(this.FileMode)	//Modified at 2008-10-13 15:32:54@Scott
			{
				this._GamesInEdl = new AdvGameInEdlInfoCollection();

				this._AvailableFields = new StringCollection();

				this._AvailableGameInEdl = new StringCollection();

				this._AdvGames = new AdvGameCollection();
			}

			if(this.FileMode)	//Modified at 2008-10-14 13:47:49@Scott
			{
				foreach(string path in config.Games)	//get games
				{
					if(!System.IO.File.Exists(path))
					{
						System.Windows.Forms.MessageBox.Show("Current canot access file '"+path+"',please check the path;","failed",System.Windows.Forms.MessageBoxButtons.OK,
							                   System.Windows.Forms.MessageBoxIcon.Stop);
						break;

					}

					WebbGameClass advGame = new WebbGameClass();
				
					string strPath = this.GetPath(path);					
				
					string strGameName = this.GetGameName(path);

					try
					{
						if(_UserFolder==null|this._UserFolder == string.Empty)
						{
							advGame.OpenGame(strPath,strGameName);
						}
						else
						{
							string userFolder=string.Empty;

							string[] userFolders=this._UserFolder.Split(';');

							if(userFolders.Length==2)
							{
								if(strPath.StartsWith(userFolders[0]))
								{
									userFolder=userFolders[0];
									
								}
								else if(strPath.StartsWith(userFolders[1]))
								{
									userFolder=userFolders[1];
								}								
							}
							else
							{
								userFolder=userFolders[0];
							}

							if(userFolder!=string.Empty&&!userFolder.StartsWith(@"\\"))
							{
								advGame.OpenGameEx(userFolder,this._HeaderName,strPath,strGameName);
							}
							else
							{
								advGame.OpenGame(strPath,strGameName);								
							}
						}
					}
					catch(Exception ex)
					{
						System.Diagnostics.Trace.WriteLine(path+":"+ex.Message);

						continue;
					}
					this._AdvGames.Add(advGame);
				}
			}

			this._AdvEdls = new AdvEdlCollection();

			foreach(string path in config.Edls)	//get edls
			{
				WebbAnEditListClass advEdl = new WebbAnEditListClass();
               
				try
				{
					advEdl.OpenEDL(path);
				}
				catch
				{
					System.Diagnostics.Trace.WriteLine(path);

					continue;
				}
				this._AdvEdls.Add(advEdl);
			}
			
			if(this.FileMode)
			{
				this.CreateDataSource(config);
			}
		}

		//fill data
		public void CalcDataSource(Webb.Reports.WebbDataSource dataSource)
		{
			if(this.FileMode)	//Modified at 2008-10-13 15:34:02@Scott
			{
				int nPlayID = 1;
				//Fill Game Info
				this._DataSource.Tables[0].Rows.Clear();
			
				dataSource.Games = new GameInfoCollection();

                dataSource.EdlInfos = new EdlInfoCollection();  
             
                int nPlayCount = 0;
			
				string strValue = string.Empty;
			
				string strEdlPath = string.Empty,strTapPath = string.Empty, strType = string.Empty;
			
				int nPlayNum = 0,nMasterNum = 0,nStartFrame = 0,nEndFrame = 0,nSnapFrame = 0;

                #region Calculating data from game file

                foreach (WebbGameClass advGame in this._AdvGames)	//fill game's data
                {
                    #region Games
                    GameInfo gameInfo = new GameInfo();
				
					this.FillGameInfo(gameInfo, advGame);
				
					dataSource.Games.Add(gameInfo);

					advGame.GetPlaysNum(out nPlayCount);

					for(int i = 0;i<nPlayCount;i++)
					{
						DataRow dr = this._DataSource.Tables[0].NewRow();
					
						this.AddVideoInfoRows(advGame,i+1,nPlayID);

						foreach(string strField in this._AvailableFields)
						{
							advGame.GetFieldValueByName(i,strField,out strValue);

							dr[strField] = strValue;
						}

						dr[ConstValue.PlayID] = nPlayID.ToString();

						nPlayID++;

						this._DataSource.Tables[0].Rows.Add(dr);
                    }
                    #endregion
                }

				AdvGameInEdlInfo info = null;

				this._GamesInEdl.Clear();

				string strTempTapePath = string.Empty, strGameName = string.Empty, strPath = string.Empty, strGame = string.Empty;
          
				foreach(WebbAnEditListClass advEdl in this._AdvEdls)	//fill edl's data
                {
                     #region convert elds into games by relation

                    advEdl.GetEditsNum(out nPlayCount);

					advEdl.GetFileName(out strEdlPath);

					for(int i = 0;i<nPlayCount;i++)
					{
						advEdl.GetAnEdit(i,out strTapPath,out nPlayNum,out nMasterNum,out nStartFrame,out nEndFrame,out nSnapFrame,out strType);

						if(strTempTapePath != strTapPath)
						{
							strGameName = this.GetEditGameName(advEdl,strEdlPath,strTapPath);
						}                        

						strTempTapePath = strTapPath;

						if(!System.IO.File.Exists(strGameName)) continue;

						strPath = this.GetPath(strGameName);

						strGame = this.GetGameName(strGameName);

						info = this._GamesInEdl.Contain(strGame,strPath);

						if(info == null)
						{
							info = new AdvGameInEdlInfo(strGame,strPath);
						
							this._GamesInEdl.Add(info);
						}

						info.arrPlayNums.Add(nPlayNum);
						info.arrPlayID.Add(nPlayID);
						nPlayID++;
                    }

                     #endregion

                }

				WebbGameClass edlGame = new WebbGameClass();

				foreach(AdvGameInEdlInfo gameInEdlInfo in this._GamesInEdl)
				{
					bool bIsOpen = false;                   

                    #region Open Game in Edl
                    try
					{
						string userFolder=string.Empty;

                        #region Get User Folder

                        string[] userFolders=this._UserFolder.Split(';');

						if(userFolders.Length==2)
						{
							if(strPath.StartsWith(userFolders[0]))
							{
								userFolder=userFolders[0];
									
							}
							else if(strPath.StartsWith(userFolders[1]))
							{
								userFolder=userFolders[1];
							}								
						}
						else
						{
							userFolder=userFolders[0];
                        }
                        #endregion

                        if (userFolder== string.Empty)
						{
							edlGame.OpenGame(gameInEdlInfo.FilePath,gameInEdlInfo.FileName);
						}
						else
						{
							edlGame.OpenGameEx(userFolder,this._HeaderName,gameInEdlInfo.FilePath,gameInEdlInfo.FileName);
						}
					
						bIsOpen = true; 
					}
					catch{continue; }
                    #endregion

                    for (int nGamePlayIndex = 0; nGamePlayIndex < gameInEdlInfo.arrPlayNums.Count; nGamePlayIndex++)
					{
						int playNum = gameInEdlInfo.arrPlayNums[nGamePlayIndex];

						int playID = gameInEdlInfo.arrPlayID[nGamePlayIndex];

						DataRow dr = this._DataSource.Tables[0].NewRow();
					
						this.AddVideoInfoRows(edlGame,playNum,playID);

						foreach(string strField in this._AvailableFields)
						{
							edlGame.GetFieldValueByName(playNum - 1,strField,out strValue);
						
							dr[strField] = strValue;
						}

						dr[ConstValue.PlayID] = playID.ToString();

						this._DataSource.Tables[0].Rows.Add(dr);
					}

					if(bIsOpen) edlGame.CloseGame();
                }
                #endregion

                PlayBookData.CalcuteImagePathInTable(this._DataSource.Tables[0]);

                GameInfoHelper.SortTable(this._DataSource.Tables[0],ConstValue.PlayID);

				this.AddRelation();

				this.FillFiltersInfo(dataSource);
				this.FillEdlGameInfo(dataSource);

				dataSource.DataSource = this._DataSource;
				dataSource.DataMember = ConstValue.Table;
			}

			#region Modified Area
			if(this.DBMode)
			{
				this.CalcDataSourceDB(dataSource);
			}
			#endregion        //Modify at 2008-10-13 15:34:53@Scott
		}

		//Modified at 2008-10-13 15:35:17@Scott
		public void CalcDataSourceDB(Webb.Reports.WebbDataSource dataSource)
		{
			DataTable dt = new DataTable(ConstValue.Table);

			DataTable dtVideoInfo = new DataTable(ConstValue.VideoInfo);

			DataTable dtTemp = new DataTable();

			string strCmd = string.Empty;
			string strVideoCmd = string.Empty;
			string strTempCmd = string.Empty;

			int nRows = 0,nTempRows = 0;

			if(this._DBSourceConfig.Games.Count > 0)
			{
				foreach(string strGameFileName in this._DBSourceConfig.Games)
				{
					string strGameName = GameInfoHelper.GetGameNameFromFileName(strGameFileName);

					string strScoutType = GameInfoHelper.GetScoutType(strGameName);

					string strGameNameWithoutScoutType = GameInfoHelper.GetGameNameWithoutScoutType(strGameName);

					strCmd = string.Format(ConstValue.Cmd_Adv_Football_Game,strScoutType,strGameNameWithoutScoutType);

					strVideoCmd = string.Format(ConstValue.Cmd_Adv_Football_VideoInfo,strGameName,strScoutType,strGameNameWithoutScoutType);

					//System.Windows.Forms.MessageBox.Show(strCmd);

					nTempRows = nRows;

					if(this._DBManager != null) this._DBManager.FillDataTable(dt,strCmd);

					nRows = dt.Rows.Count;

					if(nRows == nTempRows)
					{//no plays
						if(this._SecDBManager != null) 
						{
							this._SecDBManager.FillDataTable(dt,strCmd);

							this._SecDBManager.FillDataTable(dtVideoInfo,strVideoCmd);
						}
					}
					else
					{
						if(this._DBManager != null)  this._DBManager.FillDataTable(dtVideoInfo,strVideoCmd);
					}
				}
			}
			
			if(this._DBSourceConfig.Edls.Count > 0)
			{
				int nPlayCount = 0,nMasterNum = 0,nStartFrame = 0,nEndFrame = 0,nSnapFrame = 0,nPlayNum = 0;

				string strEdlPath = string.Empty,strType = string.Empty,strTapPath = string.Empty;

				string strTempTapePath = string.Empty, strGameName = string.Empty, strPath = string.Empty, strGame = string.Empty, strGameWithoutScoutType = string.Empty,strScoutTypeName = string.Empty;

				foreach(WebbAnEditListClass advEdl in this._AdvEdls)	//fill edl's data
				{
					advEdl.GetEditsNum(out nPlayCount);

					advEdl.GetFileName(out strEdlPath);

					for(int i = 0;i<nPlayCount;i++)
					{
						advEdl.GetAnEdit(i,out strTapPath,out nPlayNum,out nMasterNum,out nStartFrame,out nEndFrame,out nSnapFrame,out strType);

						strGameName = this.GetEditGameName(advEdl,strEdlPath,strTapPath);

						strTempTapePath = strTapPath;

						strPath = this.GetPath(strGameName);

						strGame = this.GetGameName(strGameName);

						strGameWithoutScoutType = GameInfoHelper.GetGameNameWithoutScoutType(strGame);
						
						strScoutTypeName = GameInfoHelper.GetScoutType(strGame);

						strTempCmd = string.Format(ConstValue.Cmd_Adv_GameID,strScoutTypeName,strGameWithoutScoutType);

						dtTemp.Rows.Clear();

						if(this._DBManager != null) this._DBManager.FillDataTable(dtTemp,strTempCmd);

						if(dtTemp.Rows.Count > 0)
						{
							strCmd = string.Format(ConstValue.Cmd_Adv_Football_Edl,dtTemp.Rows[0][0],nMasterNum);

							strVideoCmd = string.Format(ConstValue.Cmd_Adv_Football_Edl_VideoInfo,strGame,dtTemp.Rows[0][0],nMasterNum);

							this._DBManager.FillDataTable(dt,strCmd);

							this._DBManager.FillDataTable(dtVideoInfo,strVideoCmd);
						}
						else
						{
							if(this._SecDBManager != null)  this._SecDBManager.FillDataTable(dtTemp,strTempCmd);

							if(dtTemp.Rows.Count > 0)
							{
								strCmd = string.Format(ConstValue.Cmd_Adv_Football_Edl,dtTemp.Rows[0][0],nMasterNum);

								strVideoCmd = string.Format(ConstValue.Cmd_Adv_Football_Edl_VideoInfo,strGame,dtTemp.Rows[0][0],nMasterNum);

								this._SecDBManager.FillDataTable(dt,strCmd);

								this._SecDBManager.FillDataTable(dtVideoInfo,strVideoCmd);
							}
						}
					}
				}
			}

			this.ClearDataSource(this.DataSource);
			this.DataSource.Tables.Add(dt);
			this.DataSource.Tables.Add(dtVideoInfo);

			this.FillFiltersInfo(dataSource);
			//this.FillEdlGameInfo(dataSource);

			dataSource.DataSource = this._DataSource;
			dataSource.DataMember = ConstValue.Table;
		}

		//Get all fields from games and edls, create data source structure
		private void CreateDataSource(DBSourceConfig config)
        {           
            int nFieldsCount = 0;

			string strFieldName = string.Empty;
          
            foreach (WebbGameClass advGame in this._AdvGames)	//Get all fields from games
            {
                advGame.GetFieldsNum(out nFieldsCount);

                for (int i = 0; i < nFieldsCount; i++)
                {
                    advGame.GetFieldName(i, out strFieldName);

                    if (strFieldName != string.Empty && !this._AvailableFields.Contains(strFieldName))
                    {
                        this._AvailableFields.Add(strFieldName);
                    }
                }
            }
                 
            string strTempTapePath = string.Empty;

			foreach(WebbAnEditListClass advEdl in this._AdvEdls)	//Get all fields from edls
			{
				int nEditCount = 0,nPlayNum = 0,nMasterNum = 0,nStartFrame = 0,nEndFrame = 0,nSnapFrame = 0;

				string strGameName = string.Empty,strType = string.Empty;

				string strEdlFileName = string.Empty;

				string strTapName = string.Empty;

				advEdl.GetFileName(out strEdlFileName);

				advEdl.GetEditsNum(out nEditCount);

				for(int i = 0;i<nEditCount;i++)	//Get all fields from every edit 
				{
					advEdl.GetAnEdit(i,out strTapName,out nPlayNum,out nMasterNum,out nStartFrame,out nEndFrame,out nSnapFrame,out strType);

					if(strTempTapePath == strTapName) continue;

					strTempTapePath = strTapName;

					strGameName = this.GetEditGameName(advEdl,strEdlFileName,strTapName);

					if(!System.IO.File.Exists(strGameName)) continue;

					if(!this._AvailableGameInEdl.Contains(strGameName))
					{
						this._AvailableGameInEdl.Add(strGameName);
					
						int index = this.IsGameOpen(strGameName);

						if(index < 0) this.AdjustAvailableFields(strGameName);	//If haven't opened the game, adjust available fields
					}
				}
			}

            if (this._AvailableFields.Count > 0 && !this._AvailableFields.Contains("EFF"))
            {                
                this._AvailableFields.Add("EFF");                 
            }

            using (PlayBookData playBookData = new PlayBookData())  //Add this code at 2011-5-17 10:22:26@simon
            {
                playBookData.AddPlayBookFields(this._AvailableFields);
            }

			this.CreateTable();	//Create data table
		}

		private void FillFiltersInfo(WebbDataSource dataSource)
		{
			if(dataSource.Filters == null) dataSource.Filters = new FilterInfoCollection();

			dataSource.Filters.Clear();

			foreach(string strFilter in this._Filters)
			{
				FilterInfo filterInfo = new FilterInfo();

				filterInfo.FilterName = strFilter;

				dataSource.Filters.Add(filterInfo);
			}
        }

        #region Old       
        public void FillEdlGameInfo(WebbDataSource dataSource)
        { 
            foreach (AdvGameInEdlInfo info in this._GamesInEdl)
            {
                GameInfo gameInfo = new GameInfo();              

                this.FillGameInfo(gameInfo, info.FileName);

                dataSource.Games.Add(gameInfo);
            }
        }
        #endregion

        private void CreateTable()
		{
			DataTable dt = new DataTable(ConstValue.Table);
			
			DataColumn dcPlayID = new DataColumn(ConstValue.PlayID,typeof(int));

			dt.Columns.Add(dcPlayID);
          
			foreach(string strField in this._AvailableFields)
			{
				if(strField == ConstValue.PlayID) continue;

				DataColumn dc = new DataColumn(strField);
				
				dt.Columns.Add(dc);
			}
			
			DataTable dtVideoInfo = new DataTable(ConstValue.VideoInfo);
			dtVideoInfo.Columns.Add(new DataColumn(ConstValue.PlayID,typeof(int)));
			dtVideoInfo.Columns.Add(new DataColumn(ConstValue.StartFrame));
			dtVideoInfo.Columns.Add(new DataColumn(ConstValue.EndFrame));
			dtVideoInfo.Columns.Add(new DataColumn(ConstValue.VideoFilePath));
			dtVideoInfo.Columns.Add(new DataColumn(ConstValue.Angle));
			dtVideoInfo.Columns.Add(new DataColumn(ConstValue.PlayNum));
			dtVideoInfo.Columns.Add(new DataColumn(ConstValue.GameName));
			dtVideoInfo.Columns.Add(new DataColumn(ConstValue.MasterNum));

			this.ClearDataSource(this.DataSource);
			
			this.DataSource.Tables.Add(dt);
			
			this.DataSource.Tables.Add(dtVideoInfo);
		}

		private void ClearDataSource(DataSet ds)
		{
			ds.Relations.Clear();	//clear relations at first

			foreach(DataTable dt in ds.Tables)
			{
				dt.Constraints.Clear();	//clear constraints
			}

			ds.Tables.Clear();	//clear data at last
		}

		//Add fields in the game which wasn't exist in available fields
		private void AdjustAvailableFields(string strGameName)
		{
			string strPath = this.GetPath(strGameName);

			string strGame = this.GetGameName(strGameName);

			int nFieldCount = 0;

			string strFieldName = string.Empty;

			WebbGameClass advGame = new WebbGameClass();

			try
			{
				if(this._UserFolder == string.Empty)
				{
					advGame.OpenGame(strPath,strGame);
				}
				else
				{
					advGame.OpenGameEx(this._UserFolder,this._HeaderName,strPath,strGame);
				}
			}
			catch
			{
				return;
			}

			advGame.GetFieldsNum(out nFieldCount);

			for(int i = 0 ;i< nFieldCount;i++)
			{
				advGame.GetFieldName(i,out strFieldName);

				if(!this._AvailableFields.Contains(strFieldName)) this._AvailableFields.Add(strFieldName); 
			}

			advGame.CloseGame();
		}

		//check if the game has been opened
		private int IsGameOpen(string strGameName)
		{
			string strPath = this.GetPath(strGameName);

			string strGame = this.GetGameName(strGameName);

			string strTempGameName = string.Empty;

			for(int i = 0;i<this._AdvGames.Count;i++)
			{
				WebbGameClass advGame = this._AdvGames[i];

				advGame.GetGameName(out strTempGameName);

				if(strTempGameName == strGame)
				{
					return i;
				}
			}
			return -1;
		}

		private bool SearchFiles(string dir,string searchfile,ref string LastFile)
		{			
			if(!System.IO.Directory.Exists(dir))return false;

			string[] strFiles=System.IO.Directory.GetFiles(dir);

			foreach(string file in strFiles)
			{
				if(file.EndsWith(searchfile))
				{
					LastFile=file;

					return true;
				}
			}
			string[] arrDirectories = System.IO.Directory.GetDirectories(dir);

			foreach(string strDir in arrDirectories)
			{
				bool find=SearchFiles(strDir,searchfile,ref LastFile);	
			
				if(find)return true;
			}

			return false;
		}   

		//Get game name by tap name
		private string GetEditGameName(WebbAnEditListClass advEdl,string strEdlFileName,string strTapName)
		{
			int index = 0;

			StringBuilder sbGameName = new StringBuilder();

			string strGameFolder = string.Empty;
			
			advEdl.GetGameFolder(strTapName,out strGameFolder);

			if(!strGameFolder.EndsWith(@"\")) strGameFolder += @"\";			
				
			string strGameName = string.Format("{0}{1}",strTapName.Remove(strTapName.Length - 6,6),".exp");	//remove angle *01.tap,*02.tap...

            string lastFile=string.Empty;	

            if (Webb.Utility.CurReportMode==2)
            {
                bool findFile = SearchFiles(this._UserFolder, strGameName, ref  lastFile);

                return lastFile;
            }
            else
            {
                sbGameName.Append(strGameFolder);

                sbGameName.Append(strGameName);

                //06-20-2008@Scott
                if (!System.IO.File.Exists(sbGameName.ToString()))
                {
                    index = sbGameName.ToString().IndexOf(@"\Imported Games\");

                    if (index > 0) sbGameName.Insert(index, @"\Exported Cutup-Games");	//If the folder is not exist, add "Exported Cutup-Games"
                }

                string strTempGameName = sbGameName.ToString();

                string strEDL = @"\Edit Decision Lists\";

                string strExportedGames = @"\Exported Cutup-Games\";

                int nStart = strTempGameName.IndexOf(strEDL);

                int nEnd = strTempGameName.LastIndexOf(strExportedGames) + strExportedGames.Length - 1;

                if (nEnd > nStart && nStart > 0) sbGameName.Remove(nStart, nEnd - nStart);
                //			}
            }
			return sbGameName.ToString();
		}

		private string GetGameName(string path)
		{
			int indexBacklash = path.LastIndexOf(@"\");
			
			int indexDot = path.LastIndexOf(".");
			
			int length = indexDot - indexBacklash;
			
			return path.Substring(indexBacklash + 1,length - 1);
		}

		private string GetPath(string path)
		{
			int index = path.LastIndexOf(@"\");

			return path.Substring(0,index+1);
		}

		private void AddRelation()
		{
			DataColumn parentCol = this.DataSource.Tables[ConstValue.Table].Columns[ConstValue.PlayID];

			DataColumn childCol = this.DataSource.Tables[ConstValue.VideoInfo].Columns[ConstValue.PlayID];

			DataRelation relation = new DataRelation(ConstValue.VideoInfo,parentCol,childCol);

			this.DataSource.Relations.Add(relation);
		}

		//fill video info
		private void AddVideoInfoRows(WebbGameClass advGame,int nPlayNum,int nPlayID)
		{
			string strValue = string.Empty;

			int nValue = 0,nAngleCount = 0,nAngleNum = 0,nIsNonlinear = 0,nVcrType = 0;
			
			advGame.GetAnglesNum(out nAngleCount);

			for(int nAngleIndex = 0; nAngleIndex<nAngleCount; nAngleIndex++)
			{
				DataRow drVideoInfo = this._DataSource.Tables[1].NewRow();

				advGame.GetAngleInfo(nAngleIndex,out nAngleNum,out nIsNonlinear,out nVcrType,out strValue);

				drVideoInfo[ConstValue.VideoFilePath] = strValue;

				advGame.GetStartFrame(nPlayNum - 1, nAngleIndex, out nValue);

				drVideoInfo[ConstValue.StartFrame] = nValue;

				advGame.GetEndFrame(nPlayNum - 1, nAngleIndex, out nValue);

				drVideoInfo[ConstValue.EndFrame] = nValue;

				drVideoInfo[ConstValue.PlayID] = nPlayID;
				
				drVideoInfo[ConstValue.Angle] = nAngleNum; //this.GetTapeName(advGame,nAngleNum);
				
				advGame.GetGameName(out strValue);

				drVideoInfo[ConstValue.GameName] = strValue;

				drVideoInfo[ConstValue.PlayNum] = nPlayNum;

				advGame.GetFieldValueByName(nPlayNum - 1,ConstValue.MasterNum,out strValue);

				drVideoInfo[ConstValue.MasterNum] = strValue;

				this.DataSource.Tables[1].Rows.Add(drVideoInfo);
			}
		}

		private string GetTapeName(WebbGameClass advGame, int nAngleNum)
		{
			string strTapeName = string.Empty;

			advGame.GetGameName(out strTapeName);

			return string.Format("{0}{1:00}.tap",strTapeName,nAngleNum);
		}

        public static void FillGamesAndFilters(WebbDataSource dataSource,DBSourceConfig config)
        {
            dataSource.Games = new GameInfoCollection();

            foreach (string strGame in config.Games)
            {
                string strGameName = System.IO.Path.GetFileNameWithoutExtension(strGame);

                GameInfo gameInfo = new GameInfo();

                gameInfo.GameName = strGameName;

                gameInfo.ScoutType = GameInfoHelper.GetScoutType(strGameName);

                gameInfo.Object = GameInfoHelper.GetObject(strGameName);

                gameInfo.Opponent = GameInfoHelper.GetOpponent(strGameName);

                gameInfo.GameDate = GameInfoHelper.GetDate(strGameName);

                gameInfo.Location = GameInfoHelper.GetLocation(strGameName);

                dataSource.Games.Add(gameInfo);
            }
            
            dataSource.Filters = new FilterInfoCollection();

            foreach (string strFilter in config.Filters)
            {
                FilterInfo filterInfo = new FilterInfo();

                filterInfo.FilterName = strFilter;

                dataSource.Filters.Add(filterInfo);
            }

        }

		private void FillGameInfo(GameInfo gameInfo,WebbGameClass advGame)
		{
			string value = string.Empty;

			advGame.GetGameName(out value);
			gameInfo.GameName = value;
			
			advGame.GetObject(out value);
			gameInfo.Object = value;
			
			advGame.GetOpponent(out value);
			gameInfo.Opponent = value;
			
			advGame.GetLocation(out value);
			gameInfo.Location = value;
			
			advGame.GetDate(out value);
			gameInfo.GameDate = value;

			advGame.GetGameName(out value);
			gameInfo.ScoutType = GameInfoHelper.GetScoutType(value);
		}

		private void FillGameInfo(GameInfo gameInfo,string strGameName)
		{
			gameInfo.GameName = strGameName;

			gameInfo.ScoutType = GameInfoHelper.GetScoutType(strGameName);
		
			gameInfo.Object = GameInfoHelper.GetObject(strGameName);

			gameInfo.Opponent = GameInfoHelper.GetOpponent(strGameName);

			gameInfo.GameDate = GameInfoHelper.GetDate(strGameName);

			gameInfo.Location = GameInfoHelper.GetLocation(strGameName);
		}

		public virtual void Dispose()
		{
			if(this._AdvGames == null) return;

			foreach(WebbGameClass advGame in this._AdvGames)
			{
				advGame.CloseGame();
			}
		}


		#region Modify codes at 2009-12-15 14:19:37@Simon

		public static int GetDefaultWidth(string dataPath,string reportHeader,string FieldName)  //2009-12-14 16:51:14@Simon Add this Code
		{
			WebbGameClass advGame=new WebbGameClass();
					             
			advGame.SetTerminologyPath(dataPath);
								
			// When use one condition about beow: 
			//it would reverto the defaultwidth
							
			//reportHeader="Defensive Self";

			//reportHeader="Opponent Offense";

			//reportHeader="Offensive Self";

			//reportHeader="Opponent Defense";

			//reportHeader="Kick";

			advGame.SetReportHeader(reportHeader);

			int ColumnWidth=-2;

			advGame.GetFieldWidthFromReportHeader(FieldName,out ColumnWidth);

			advGame=null;

			return ColumnWidth;

		}
		public static Hashtable GetFieldMaxLength(string dataPath,ArrayList getArrList)
		{
			WebbGameClass advGame=new WebbGameClass();
					             
			advGame.SetTerminologyPath(dataPath);

			Hashtable hashtable=new Hashtable();

			foreach(string strFieldname in  getArrList)
			{
				int MaxCharLen=-2;			

			    advGame.GetFieldLengthFromTerm(strFieldname,out MaxCharLen);

				if(MaxCharLen==200||MaxCharLen<=0)
				{
					hashtable.Add(strFieldname,-1);
				}
				else
				{
					hashtable.Add(strFieldname,MaxCharLen);
				}
			  
		  }
          
			advGame=null;
		   
			return hashtable;
		}
		#endregion        //End Modify
	}

	public class AdvGameCollection : CollectionBase
	{
		public WebbGameClass this[int i_Index]
		{ 
			get { return this.InnerList[i_Index] as WebbGameClass; } 
			set { this.InnerList[i_Index] = value; } 
		} 
		
		public AdvGameCollection() {} 
		
		public int Add(WebbGameClass i_Object)
		{ 
			return this.InnerList.Add(i_Object);
		} 

		public void Remove(WebbGameClass i_Obj)
		{
			this.InnerList.Remove(i_Obj);
		} 
	}

	public class AdvEdlCollection : CollectionBase
	{
		public WebbAnEditListClass this[int i_Index]
		{ 
			get { return this.InnerList[i_Index] as WebbAnEditListClass; } 
			set { this.InnerList[i_Index] = value; } 
		} 
		
		public AdvEdlCollection() {} 
		
		public int Add(WebbAnEditListClass i_Object)
		{ 
			return this.InnerList.Add(i_Object);
		} 

		public void Remove(WebbAnEditListClass i_Obj)
		{
			this.InnerList.Remove(i_Obj);
		} 
	}

	public class AdvGameInEdlInfo : AdvFileInfo
	{
		public Webb.Collections.Int32Collection arrPlayNums;
		public Webb.Collections.Int32Collection arrPlayID;
		
		public AdvGameInEdlInfo(string strFileName,string strFilePath) : base(strFileName,strFilePath)
		{
			this.arrPlayNums = new Webb.Collections.Int32Collection();
			this.arrPlayID = new Webb.Collections.Int32Collection();
		}

		public bool Equals(string strPath,string strName)
		{		
			if(strPath == this.FileName && strName == this.FilePath)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}

	public class AdvGameInEdlInfoCollection : CollectionBase
	{
		public AdvGameInEdlInfo this[int i_Index]
		{ 
			get { return this.InnerList[i_Index] as AdvGameInEdlInfo; } 
			set { this.InnerList[i_Index] = value; } 
		}
		
		public AdvGameInEdlInfoCollection() {} 
		
		public int Add(AdvGameInEdlInfo i_Object)
		{ 
			return this.InnerList.Add(i_Object);
		} 

		public void Remove(AdvGameInEdlInfo i_Obj)
		{
			this.InnerList.Remove(i_Obj);
		} 

		public AdvGameInEdlInfo Contain(AdvFileInfo i_Obj)
		{
			foreach(AdvGameInEdlInfo info in this.InnerList)
			{
				if(i_Obj.Equals(info))
				{
					return info;
				}
			}
			return null;
		}

		public AdvGameInEdlInfo Contain(string strPath,string strName)
		{
			foreach(AdvGameInEdlInfo info in this.InnerList)
			{
				if(info.Equals(strPath,strName))
				{
					return info;
				}
			}
			return null;
		}
	}

	public class GameInfoHelper
	{
		private static string GetSubString(string strGameName,string strStart,string strEnd)
		{
			int indexStart = strGameName.IndexOf(strStart);

            if (indexStart < 0) return string.Empty;

			int indexEnd = strGameName.IndexOf(strEnd,indexStart);

			if(indexEnd < 0 || indexEnd - indexStart - strStart.Length < 0) return string.Empty;

			return strGameName.Substring(indexStart + strStart.Length,indexEnd - indexStart - strStart.Length);
		}

		private static string GetSubString(string strGameName, string strEnd, bool bLastIndex)
		{
			int indexStart = 0;

			int indexEnd = 0;
			
			if(bLastIndex)
			{
				indexEnd = strGameName.LastIndexOf(strEnd,strGameName.Length - 1);
			}
			else
			{
				indexEnd = strGameName.IndexOf(strEnd,indexStart);
			}

			if(indexStart < 0 || indexEnd < 0 || indexEnd - indexStart < 0) return string.Empty;

			return strGameName.Substring(indexStart,indexEnd - indexStart);
		}

		public static string GetGameNameWithoutScoutType(string strGameName)
		{
			return GetSubString(strGameName,"-",true);
		}

		public static string GetScoutType(string strGameName)
		{
			int index = strGameName.LastIndexOf("-");

			if(index < 0) return string.Empty;

			return strGameName.Substring(index + 1);
		}

		public static string GetObject(string strGameName)
		{
			int index = strGameName.IndexOf(" VS ");

			if(index < 0) return string.Empty;

			return strGameName.Substring(0,index);
		}

		public static string GetOpponent(string strGameName)
		{
			return GetSubString(strGameName," VS "," ON ");
		}

		public static string GetLocation(string strGameName)
		{
			return GetSubString(strGameName," AT ","-");
		}

		public static string GetDate(string strGameName)
		{
			return GetSubString(strGameName," ON "," AT ");
		}

		public static string GetGameNameFromFileName(string strGameFileName)
		{
			string strGameName = GetSubString(strGameFileName,".",true);

			int index = strGameName.LastIndexOf(@"\");

			if(index >= 0)
			{
				return strGameName.Remove(0,index + 1);
			}
			else
			{
				return strGameName;
			}
		}

		public static void SortTable(DataTable table,string strField)
		{
			if(!table.Columns.Contains(strField)) return;

			DataRow[] rows = table.Select("", strField + " asc");

			DataTable dumyTable = table.Clone();

			foreach (DataRow row in rows) dumyTable.ImportRow(row);

			table.Rows.Clear();

			foreach (DataRow row in dumyTable.Rows) table.ImportRow(row);
		}
	}

	public class AdvDiagram
	{
		WebbDiagramClass _Diagram;

		string _DiagramPath;

		public WebbDiagramClass Diagram
		{
			get{return this._Diagram;}
		}

		public string DiagramPath
		{
			get{return this._DiagramPath;}
		}

		public AdvDiagram()
		{
			this._Diagram = new WebbDiagramClass();
		}

		public void OpenDiagram(string strDiagramPath)
		{
			if(this._Diagram == null) return;

			this._DiagramPath = strDiagramPath;

			this._Diagram.OpenDiagram(strDiagramPath);
		}

		public void DrawDiagram(System.Drawing.Graphics g,int width,int height)
		{
			if(this._Diagram == null) return;

			IntPtr hdc = g.GetHdc();

			if(Webb.Reports.DataProvider.VideoPlayBackManager.InvertDiagram)  //2009-7-1 13:59:44@Simon Add this Code
			{
				this._Diagram.DrawDiagramInvertHor((int)hdc,width,height);
                this._Diagram.DrawDiagramInvertVer((int)hdc,width,height);
			}
			else
			{
				this._Diagram.DrawDiagram((int)hdc,width,height);
			}

			g.ReleaseHdc(hdc);
		}
	}

	public class VictoryGameData : IDisposable
	{
		public DBSourceConfig _DBSourceConfig;

		public DataTable _TempTable;

		public DataTable _Table;

		public IDBManager _DBManager;
	
		public virtual void Dispose()
		{

		}    
		public VictoryGameData(DBSourceConfig config)
		{
			this._Table = new DataTable("Table");

			this._TempTable = new DataTable();

			this._DBSourceConfig = new DBSourceConfig();

			this._DBSourceConfig.ApplyConfig(config);

			string strConn = Webb.Data.DBHelper.GetOleConnString(config.DBFilePath);

			this._DBManager = Webb.Data.DBHelper.NewDBManager(DBConnTypes.OleDB,strConn);
        }

        #region Old
         public void CalcDataSourceOld(WebbDataSource webbDataSource)
		{
			string strCmd = string.Empty,strGameCmd = string.Empty;

            string Cmd_Game = @"SELECT GameDetail.PlayID as pid,* FROM GameDetail LEFT JOIN SubPlay ON GameDetail.PlayID = SubPlay.PlayID WHERE GameID IN ({0})";

		    string Cmd_EdlDetail = @"SELECT GameID,PlayID,PlayType FROM EdlDetail WHERE EdlID = {0}";

		    string Cmd_Edl_PlayFilter = @"SELECT GameDetail.PlayID as pid,* FROM GameDetail LEFT JOIN SubPlay ON GameDetail.PlayID = SubPlay.PlayID WHERE GameID = {0} AND GameDetail.PlayID = {1}";

	        string Cmd_Edl_SubPlayFilter = @"SELECT GameDetail.PlayID as pid,* FROM GameDetail LEFT JOIN SubPlay ON GameDetail.PlayID = SubPlay.PlayID WHERE GameID = {0} AND SubPlay.SubPlayID = {1}";


			//Game
			foreach(int nGameID in this._DBSourceConfig.GameIDs)
			{
				strGameCmd += string.Format("{0}",nGameID);
	
				strGameCmd += ",";
			}

			if(strGameCmd.Length > 0)
			{
				strGameCmd = strGameCmd.Remove(strGameCmd.Length - 1,1);
			}
			else
			{
				strGameCmd = "-1";
			}


           
			strCmd = string.Format(Cmd_Game,strGameCmd);

			this._DBManager.FillDataTable(this._Table,strCmd);
           
            webbDataSource.EdlInfos=new EdlInfoCollection();  //Add this code at 2011-1-4 15:46:26@simon

            StringBuilder sbEdls = new StringBuilder(); //Add this code at 2011-1-4 16:02:40@simon

			//Edl
			foreach(string strEdlID in this._DBSourceConfig.Edls)
            {
                #region cutup
                if (strEdlID == null || strEdlID == string.Empty) continue;

                if (sbEdls.Length > 0) sbEdls.Append(",");

                sbEdls.Append(strEdlID);

				strCmd = string.Format(Cmd_EdlDetail,strEdlID);

				this._TempTable.Rows.Clear();

				this._DBManager.FillDataTable(this._TempTable,strCmd);
              
                foreach (DataRow row in this._TempTable.Rows)
				{
					if(row["PlayType"].ToString() == "1")
					{//Play
						strCmd = string.Format(Cmd_Edl_PlayFilter,row["GameID"],row["PlayID"]);

						this._DBManager.FillDataTable(this._Table, strCmd);
					}
					else if(row["PlayType"].ToString() == "2")
					{//SubPlay
						strCmd = string.Format(Cmd_Edl_SubPlayFilter,row["GameID"],row["PlayID"]);

						this._DBManager.FillDataTable(this._Table,strCmd);
					}

                }
                #endregion
            }

            #region  Calcualte Edl Name Modifed at 2011-1-4 15:53:42@simon

            if (sbEdls.Length > 0)
            {
                DataTable eldShowTable = new DataTable();

                try
                {
                    string CMD_GetEDLs = "select distinct [EdlID],[EdlName] from [Edl] where [EdlID] in ({0})";

                    string i_edlcmd = string.Format(CMD_GetEDLs, sbEdls);

                    this._DBManager.FillDataTable(eldShowTable, i_edlcmd);

                    foreach (DataRow dr in eldShowTable.Rows)
                    {
                        EdlInfo edlInfo = new EdlInfo();

                        edlInfo.EdlID =Convert.ToInt32(dr[0]);

                        edlInfo.EDLName =dr[1].ToString();

                        webbDataSource.EdlInfos.Add(edlInfo);
                    }                   
                }
                catch
                {
                }
            }

            #endregion

            //#region Modifed at 2011-1-6 9:42:39@simon 

            //    DataTable fieldRelationTab = new DataTable();

            //    string cmb_FieldRelation = "select distinct SubPlayFieldName,ValueTableFieldName from subPlayConfig where ValueTableFieldName like 'V%'";

            //    try
            //    {
            //        this._DBManager.FillDataTable(fieldRelationTab, cmb_FieldRelation);

            //        foreach (DataRow dataRow in fieldRelationTab.Rows)
            //        {
            //            if(dataRow[0]==null||dataRow[0] is DBNull||dataRow[1] ==null||dataRow[1] is DBNull)continue;

            //             string SubPlayFieldName=dataRow[0].ToString();

            //             string ValueTableFieldName=dataRow[1].ToString();

            //                foreach (DataColumn dc in this._Table.Columns)
            //                {
            //                    if (dc.ColumnName == ValueTableFieldName) dc.ColumnName = SubPlayFieldName;
            //                }
            //        }
            //    }
            //    catch(Exception ex)
            //    {
                
            //    }

            //#endregion

            webbDataSource.DataSource = new DataSet("DataSource");

			webbDataSource.DataSource.Tables.Add(this._Table);
        }
         public void CalcDataSourceOld2(WebbDataSource webbDataSource)
         {
             string strCmd = string.Empty, strGameCmd = string.Empty;

             //Game
             foreach (int nGameID in this._DBSourceConfig.GameIDs)
             {
                 strGameCmd += string.Format("{0}", nGameID);

                 strGameCmd += ",";
             }

             if (strGameCmd.Length > 0)
             {
                 strGameCmd = strGameCmd.Remove(strGameCmd.Length - 1, 1);
             }
             else
             {
                 strGameCmd = "-1";
             }


             string Cmd_Game = @"SELECT GameDetail.PlayID as pid,{1},GameDetail.*,SubPlay.* FROM GameDetail LEFT JOIN SubPlay ON GameDetail.PlayID = SubPlay.PlayID WHERE GameID IN ({0})";

             string Cmd_GameWithOutSubPlay = @"SELECT GameDetail.PlayID as pid,* FROM GameDetail LEFT JOIN SubPlay ON GameDetail.PlayID = SubPlay.PlayID WHERE GameID IN ({0})";

             string Cmd_EdlDetail = @"SELECT GameID,PlayID,PlayType FROM EdlDetail WHERE EdlID = {0}";

             string Cmd_Edl_PlayFilter = @"SELECT GameDetail.PlayID as pid,{2},* FROM GameDetail LEFT JOIN SubPlay ON GameDetail.PlayID = SubPlay.PlayID WHERE GameID = {0} AND GameDetail.PlayID = {1}";

             string Cmd_Edl_SubPlayFilter = @"SELECT GameDetail.PlayID as pid,{2},* FROM GameDetail LEFT JOIN SubPlay ON GameDetail.PlayID = SubPlay.PlayID WHERE GameID = {0} AND SubPlay.SubPlayID = {1}";

             #region Modifed at 2011-1-6 9:42:39@simon

             DataTable fieldRelationTab = new DataTable();

             string cmb_FieldRelation = "select distinct SubPlayFieldName,ValueTableFieldName from subPlayConfig where ValueTableFieldName like 'V%'";

             StringBuilder sbVFields = new StringBuilder();

             try
             {
                 this._DBManager.FillDataTable(fieldRelationTab, cmb_FieldRelation);

                 foreach (DataRow dataRow in fieldRelationTab.Rows)
                 {
                     if (dataRow[0] == null || dataRow[0] is DBNull || dataRow[1] == null || dataRow[1] is DBNull) continue;

                     string SubPlayFieldName = dataRow[0].ToString();

                     string ValueTableFieldName = dataRow[1].ToString();

                     if (sbVFields.Length > 0) sbVFields.Append(",");

                     sbVFields.AppendFormat("SubPlay.{0} as [{1}]", ValueTableFieldName, SubPlayFieldName);
                 }
             }
             catch (Exception ex)
             {

             }

             #endregion

             if (sbVFields.Length > 0)
             {
                 strCmd = string.Format(Cmd_Game, strGameCmd, sbVFields);
             }
             else
             {
                 strCmd = string.Format(Cmd_GameWithOutSubPlay, strGameCmd);
             }



             this._DBManager.FillDataTable(this._Table, strCmd);

             webbDataSource.EdlInfos = new EdlInfoCollection();  //Add this code at 2011-1-4 15:46:26@simon

             StringBuilder sbEdls = new StringBuilder(); //Add this code at 2011-1-4 16:02:40@simon

             //Edl
             foreach (string strEdlID in this._DBSourceConfig.Edls)
             {
                 #region cutup
                 if (strEdlID == null || strEdlID == string.Empty) continue;

                 if (sbEdls.Length > 0) sbEdls.Append(",");

                 sbEdls.Append(strEdlID);

                 strCmd = string.Format(Cmd_EdlDetail, strEdlID);

                 this._TempTable.Rows.Clear();

                 this._DBManager.FillDataTable(this._TempTable, strCmd);

                 foreach (DataRow row in this._TempTable.Rows)
                 {
                     if (row["PlayType"].ToString() == "1")
                     {//Play
                         strCmd = string.Format(Cmd_Edl_PlayFilter, row["GameID"], row["PlayID"], sbVFields);

                         this._DBManager.FillDataTable(this._Table, strCmd);
                     }
                     else if (row["PlayType"].ToString() == "2")
                     {//SubPlay
                         strCmd = string.Format(Cmd_Edl_SubPlayFilter, row["GameID"], row["PlayID"], sbVFields);

                         this._DBManager.FillDataTable(this._Table, strCmd);
                     }

                 }
                 #endregion
             }

             #region  Calcualte Edl Name Modifed at 2011-1-4 15:53:42@simon

             if (sbEdls.Length > 0)
             {
                 DataTable eldShowTable = new DataTable();

                 try
                 {
                     string CMD_GetEDLs = "select distinct [EdlID],[EdlName] from [Edl] where [EdlID] in ({0})";

                     string i_edlcmd = string.Format(CMD_GetEDLs, sbEdls);

                     this._DBManager.FillDataTable(eldShowTable, i_edlcmd);

                     foreach (DataRow dr in eldShowTable.Rows)
                     {
                         EdlInfo edlInfo = new EdlInfo();

                         edlInfo.EdlID = Convert.ToInt32(dr[0]);

                         edlInfo.EDLName = dr[1].ToString();

                         webbDataSource.EdlInfos.Add(edlInfo);
                     }
                 }
                 catch
                 {
                 }
             }

             #endregion

             #region Calculate get long Name in sub Play

             fieldRelationTab = new DataTable();

             string CMD_getDistinctLongSubField = "select distinct SubPlayFieldName,ShortName from subPlayConfig";

             this._DBManager.FillDataTable(fieldRelationTab, CMD_getDistinctLongSubField);

             foreach (System.Data.DataColumn m_col in this._Table.Columns)
             {
                 string columnName = m_col.ColumnName;

                 if (columnName.Contains("GameDetail."))
                 {
                     string ReplacedColumnName = columnName.Replace("GameDetail.", "");

                     if (!this._Table.Columns.Contains(ReplacedColumnName))
                     {
                         m_col.ColumnName = ReplacedColumnName;
                     }

                 }
                 if (columnName.Contains("SubPlay."))
                 {

                     DataRow[] dataRows = fieldRelationTab.Select("ShortName='" + columnName.Replace("SubPlay.", "") + "'");

                     if (dataRows.Length > 0 && fieldRelationTab.Columns.Count > 0 && dataRows[0][0] != null && !(dataRows[0][0] is DBNull))
                     {
                         string ReplacedColumnName = dataRows[0][0].ToString();

                         if (!this._Table.Columns.Contains(ReplacedColumnName))
                         {
                             m_col.ColumnName = ReplacedColumnName;
                         }
                     }

                 }
             }
             #endregion

             webbDataSource.DataSource = new DataSet("DataSource");

             webbDataSource.DataSource.Tables.Add(this._Table);
         }
        #endregion

        #region New
         public void CalcDataSource(WebbDataSource webbDataSource)
         {
             string strCmd = string.Empty, strGameCmd = string.Empty;

             try
             {
                 //Game
                 foreach (int nGameID in this._DBSourceConfig.GameIDs)
                 {
                     strGameCmd += string.Format("{0}", nGameID);

                     strGameCmd += ",";
                 }

                 if (strGameCmd.Length > 0)
                 {
                     strGameCmd = strGameCmd.Remove(strGameCmd.Length - 1, 1);
                 }
                 else
                 {
                     strGameCmd = "-1";
                 }

                 string[] m_mapIDName = new string[5];

                 #region Get subActions Sql

                 #region Store Categories by different Type
                 switch (this._DBSourceConfig.WebbDBType)
                 {
                     case WebbDBTypes.WebbVictoryBasketball:
                         m_mapIDName[0] = "Shot";
                         m_mapIDName[1] = "Pass/Assist";
                         m_mapIDName[2] = "Rebound";
                         m_mapIDName[3] = "Defense";
                         m_mapIDName[4] = "Foul";
                         break;
                     case WebbDBTypes.WebbVictoryHockey:
                         m_mapIDName[0] = "Shot";
                         m_mapIDName[1] = "Faceoff";
                         m_mapIDName[2] = "Pass/Assist";
                         m_mapIDName[3] = "Hit";
                         m_mapIDName[4] = "Block/Save";
                         break;
                     case WebbDBTypes.WebbVictoryLacrosse:
                         m_mapIDName[0] = "Shot";
                         m_mapIDName[1] = "Faceoff";
                         m_mapIDName[2] = "Save";
                         m_mapIDName[3] = "Restart";
                         m_mapIDName[4] = "Infraction";
                         break;

                     case WebbDBTypes.WebbVictorySoccer:
                         m_mapIDName[0] = "Shot";
                         m_mapIDName[1] = "Block/Save";
                         m_mapIDName[2] = "Knockdown-50/50";
                         m_mapIDName[3] = "Pass/Distribute";
                         m_mapIDName[4] = "Defense";
                         break;
                     case WebbDBTypes.WebbVictoryVolleyball:
                         m_mapIDName[0] = "Serve";
                         m_mapIDName[1] = "Attack";
                         m_mapIDName[2] = "Set/Assist";
                         m_mapIDName[3] = "Serve Receive";
                         m_mapIDName[4] = "Defense";
                         break;
                 }
                 #endregion

                 #region Get SubPlay/Action Fields in Data Base

                 string strGetSubPlayFieldsSql = "select ActionType,SubPlayFieldName,ValueTableFieldName from subplayConfig where gameid=0 and actiontype>1 order by fieldid asc";

                 DataTable subPlayFieldsTable = new DataTable();

                 this._DBManager.FillDataTable(subPlayFieldsTable, strGetSubPlayFieldsSql);

                 string strGetFieldInDatabase = "select * from subplay where false";

                 DataTable getRealFieldTable = new DataTable();

                 this._DBManager.FillDataTable(getRealFieldTable, strGetFieldInDatabase);

                 int beginIndex = getRealFieldTable.Columns.IndexOf("Category") + 1;

                 StringBuilder sbSubFields = new StringBuilder();  //would be used to get fields   

                 StringBuilder sbGetLeftJoinTables = new StringBuilder();  ////would be used to get tables   

                 StringBuilder sbFieldsWouldbeUsedIngames = new StringBuilder(",B.[Point of Action#],B.[Category]");  //would be used at last to get games   

                 if (getRealFieldTable.Columns.Contains("ActionTeam"))
                 {
                     sbFieldsWouldbeUsedIngames.Append(",B.[Team]");

                     sbSubFields.Append(",subplay.[ActionTeam] as [Team]");

                 }
                 for (int i = 2; i < 7; i++)
                 {
                     sbGetLeftJoinTables.Append(" left join (select [subplayId],[playid]");

                     DataRow[] filterDataRows = subPlayFieldsTable.Select("ActionType=" + i.ToString());

                     foreach (DataRow dataRow in filterDataRows)
                     {
                         string columnName = dataRow["ValueTableFieldName"].ToString();

                         string strValue = dataRow["SubPlayFieldName"].ToString() + "(Action)";

                         sbGetLeftJoinTables.AppendFormat(",[{0}] as [{1}]", columnName, strValue);

                         sbSubFields.AppendFormat(",subTable{1}.[{0}]", strValue, i);

                         sbFieldsWouldbeUsedIngames.AppendFormat(",B.[{0}]", strValue);
                     }

                     sbGetLeftJoinTables.AppendFormat(" from subplay where [category]='{0}') as subTable{1}"
                                 + " on subplay.[subplayId]=subTable{1}.subplayId and subplay.[playid]=subTable{1}.[playid])"
                            , m_mapIDName[i - 2], i);
                 }

                 string strGetActionsPlays = string.Format("select subplay.[subplayId],subplay.[playid],subplay.[Number] as [Point of Action#],subplay.[Category]{0} from (((((subplay{1}"
                                                , sbSubFields, sbGetLeftJoinTables);

                 #endregion

                 #endregion

                 string Cmd_Game = @"SELECT A.[PlayID] as pid,A.*{1} FROM GameDetail as A LEFT JOIN ({2}) as B on A.[PlayID] = B.[PlayID] WHERE A.GameID IN ({0}) Order by A.[PlayID],B.[Point of Action#]";

                 string Cmd_EdlDetail = @"SELECT GameID,PlayID,PlayType FROM EdlDetail WHERE EdlID = {0}";

                 string Cmd_Edl_PlayFilter = @"SELECT  A.PlayID as pid,A.*{1} FROM GameDetail as A LEFT JOIN ({3}) as B ON A.PlayID = B.PlayID WHERE GameID = {0} AND GameDetail.PlayID = {1} Order by A.[PlayID],B.[Point of Action#]";

                 string Cmd_Edl_SubPlayFilter = @"SELECT  A.PlayID as pid,A.*{1} FROM GameDetail as A LEFT JOIN ({3}) as B ON A.PlayID = B.PlayID WHERE GameID = {0} AND SubPlay.SubPlayID = {1} Order by A.[PlayID],B.[Point of Action#]";

                 strCmd = string.Format(Cmd_Game, strGameCmd, sbFieldsWouldbeUsedIngames, strGetActionsPlays);

                 this._Table = new DataTable();

                 this._DBManager.FillDataTable(this._Table, strCmd);

                 webbDataSource.EdlInfos = new EdlInfoCollection();  //Add this code at 2011-1-4 15:46:26@simon

                 StringBuilder sbEdls = new StringBuilder(); //Add this code at 2011-1-4 16:02:40@simon

                 //Edl
                 foreach (string strEdlID in this._DBSourceConfig.Edls)
                 {
                     #region cutup
                     if (strEdlID == null || strEdlID == string.Empty) continue;

                     if (sbEdls.Length > 0) sbEdls.Append(",");

                     sbEdls.Append(strEdlID);

                     strCmd = string.Format(Cmd_EdlDetail, strEdlID);

                     this._TempTable.Rows.Clear();

                     this._DBManager.FillDataTable(this._TempTable, strCmd);

                     foreach (DataRow row in this._TempTable.Rows)
                     {
                         if (row["PlayType"].ToString() == "1")
                         {//Play
                             strCmd = string.Format(Cmd_Edl_PlayFilter, row["GameID"], row["PlayID"], sbFieldsWouldbeUsedIngames, strGetActionsPlays);

                             this._DBManager.FillDataTable(this._Table, strCmd);
                         }
                         else if (row["PlayType"].ToString() == "2")
                         {//SubPlay
                             strCmd = string.Format(Cmd_Edl_SubPlayFilter, row["GameID"], row["PlayID"], sbFieldsWouldbeUsedIngames, strGetActionsPlays);

                             this._DBManager.FillDataTable(this._Table, strCmd);
                         }

                     }
                     #endregion
                 }

                 #region  Calcualte Edl Name Modifed at 2011-1-4 15:53:42@simon

                 if (sbEdls.Length > 0)
                 {
                     DataTable eldShowTable = new DataTable();

                     try
                     {
                         string CMD_GetEDLs = "select distinct [EdlID],[EdlName] from [Edl] where [EdlID] in ({0})";

                         string i_edlcmd = string.Format(CMD_GetEDLs, sbEdls);

                         this._DBManager.FillDataTable(eldShowTable, i_edlcmd);

                         foreach (DataRow dr in eldShowTable.Rows)
                         {
                             EdlInfo edlInfo = new EdlInfo();

                             edlInfo.EdlID = Convert.ToInt32(dr[0]);

                             edlInfo.EDLName = dr[1].ToString();

                             webbDataSource.EdlInfos.Add(edlInfo);
                         }
                     }
                     catch
                     {
                     }
                 }

                 #endregion
             }
             catch (Exception ex)
             {
                 Webb.Utilities.TopMostMessageBox.ShowMessage(ex.Message, System.Windows.Forms.MessageBoxButtons.OK);

                 this._Table = new DataTable();
             }

             webbDataSource.DataSource = new DataSet("DataSource");

             webbDataSource.DataSource.Tables.Add(this._Table);
         }
        #endregion
    }

    public class PlayBookData:IDisposable 
    {
        private string[] playbookdataFiles =null;

        public static string PlayBookAssmblyFile=Webb.Utility.ApplicationDirectory+@"PlayBook\Webb.Playbook.Geometry.dll";

        public const string PlayBookReflectType="Webb.Playbook.Geometry.DrawingDeserializer";

        public static string PlayBookInstallPath = string.Empty;

        public static string PlayBookOffensiveMainField = string.Empty;
        public static string PlayBookOffensiveSubField = string.Empty;
        public static string PlayBookDefensiveMainField = string.Empty;
        public static string PlayBookDefensiveSubField = string.Empty;

        private const string InstallKeyPath = @"Software\Webb Electronics\PlayBook";

        private const string InstallDirKeyName = @"InstallDir";

        private void ReadPlayBookFields()
        {
            if (PlayBookInstallPath != string.Empty) return;

            PlayBookInstallPath = ReadRegistry();

            if (!PlayBookInstallPath.EndsWith(@"\")) PlayBookInstallPath += @"\";

            string strSettingPath = string.Format(@"{0}Settings", PlayBookInstallPath);

            if (System.IO.File.Exists(strSettingPath))
            {
                System.Xml.XmlDocument xmldoc = new System.Xml.XmlDocument();

                xmldoc.Load(strSettingPath);

                System.Xml.XmlNode xmlNode = xmldoc.SelectSingleNode(@"/Settings/GameSetting/OffensiveMainField");

                if (xmlNode != null) PlayBookOffensiveMainField = xmlNode.Attributes["Value"].Value;

                xmlNode = xmldoc.SelectSingleNode(@"/Settings/GameSetting/OffensiveSubField");

                if (xmlNode != null) PlayBookOffensiveSubField = xmlNode.Attributes["Value"].Value;

                xmlNode = xmldoc.SelectSingleNode(@"/Settings/GameSetting/DefensiveMainField");

                if (xmlNode != null) PlayBookDefensiveMainField = xmlNode.Attributes["Value"].Value;

                xmlNode = xmldoc.SelectSingleNode(@"/Settings/GameSetting/DefensiveSubField");

                if (xmlNode != null) PlayBookDefensiveSubField = xmlNode.Attributes["Value"].Value;

            }
        }


        public PlayBookData()
        {
            this.ReadPlayBookFields();
        }

        #region calcluting PlayBook Fields in Advantage data to display images

            #region Add Fields
            private void AddField(StringCollection _AvailableFields,string strField,string modifier)
            {
                if (strField != null && strField != string.Empty && _AvailableFields.Contains(strField))
                {
                    string strPlayBookField = strField + modifier;

                    if (!_AvailableFields.Contains(strPlayBookField))
                    {
                        _AvailableFields.Add(strPlayBookField);
                    }
                }
            }
           
            public void AddPlayBookFields(StringCollection _AvailableFields)
            {
                if(PlayBookInstallPath==string.Empty)return;

                AddField(_AvailableFields,PlayBookOffensiveMainField, "(Off Main)");
                AddField(_AvailableFields,PlayBookOffensiveSubField, "(Off Sub)");
                AddField(_AvailableFields,PlayBookDefensiveMainField, "(Def Main)");
                AddField(_AvailableFields,PlayBookDefensiveSubField, "(Def Sub)");       

            }
           #endregion

            #region Calcualting Fields
            private static void CalcuteImagePathInTable(DataRow dr,string strField, string modifier,string strScoutType)
            {
                string strPlayBookField = strField + modifier;

                if (dr.Table.Columns.Contains(strPlayBookField))
                {                 
                    if (dr.Table.Columns.Contains(strField))
                    {
                        string strValue = string.Empty;

                        if (dr[strField] != null&&!(dr[strField] is DBNull))
                        {
                            strValue = dr[strField].ToString();
                        }

                        string strImagePath = PlayBookInstallPath + @"Bitmaps\" + strScoutType + @"\" + strField + @"\"+strValue+".bmp";

                        dr[strPlayBookField] = strImagePath;
                    }
                    else
                    {
                        dr[strPlayBookField] = string.Empty;
                    }                   
                }
            }
          
            public static void CalcuteImagePathInTable(DataTable dt)
            {
                if (PlayBookInstallPath == string.Empty) return;

                foreach (DataRow dr in dt.Rows)
                {
                    CalcuteImagePathInTable(dr, PlayBookOffensiveMainField, "(Off Main)","Offense");
                    CalcuteImagePathInTable(dr, PlayBookOffensiveSubField, "(Off Sub)", "Offense");
                    CalcuteImagePathInTable(dr, PlayBookDefensiveMainField, "(Def Main)", "Defense");
                    CalcuteImagePathInTable(dr, PlayBookDefensiveSubField, "(Def Sub)", "Defense");
                }
            }
            #endregion

        #endregion


        public PlayBookData(DBSourceConfig dBSourceConfgig)
        {
            StringCollection SourceFiles=dBSourceConfgig.PlayBookFormFiles;

            playbookdataFiles = new string[SourceFiles.Count];

            for (int i = 0; i < SourceFiles.Count; i++)
            {
                playbookdataFiles[i] = SourceFiles[i];
            }
        }

        #region calc playbook  DataSource
        public void CalcDataSource(WebbDataSource webbDataSource)
        {
            DirectCalcDataSource(webbDataSource);
        }

        public void ReflectCalcDataSource(WebbDataSource webbDataSource)
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFrom(PlayBookAssmblyFile);

            Type type = assembly.GetType(PlayBookReflectType);

            MethodInfo method = type.GetMethod("ReportReadData");

            object returnValue = method.Invoke(null, new Object[] { playbookdataFiles });

            DataTable table = returnValue as DataTable;

            if (table == null) table = new DataTable();

            webbDataSource.DataSource = new DataSet();

            webbDataSource.DataSource.Tables.Add(table);          
           
        }

        public void DirectCalcDataSource(WebbDataSource webbDataSource)
        {
            DataTable table = Webb.Playbook.Geometry.DrawingDeserializer.ReportReadData(playbookdataFiles);

            if (table == null) table = new DataTable();

            webbDataSource.DataSource = new DataSet();

            table.TableName = "PlayBookData";

            webbDataSource.DataSource.Tables.Add(table);
        }
        #endregion

        private static string ReadRegistry()
        {
            RegistryKey registry = Registry.LocalMachine.OpenSubKey(InstallKeyPath,false);

            if (registry == null) return string.Empty;

            object obj = registry.GetValue(InstallDirKeyName);

            if (obj == null) return string.Empty;           

            return obj.ToString();
        }


        #region  Used in Custom Image Control
        public static System.Drawing.Image ReportReadImage(string game,int nWidth,int nHeight)
        {
            return Webb.Playbook.Geometry.DrawingDeserializer.ReportReadImage(game, nWidth, nHeight, true);
        }

        public string CheckOffensiveField(string ScoutTypeFolder, string strField)
        {
            if (strField == string.Empty) return ScoutTypeFolder;

            string offenseOrdefense = ScoutTypeFolder;

            if (strField == PlayBookOffensiveMainField || strField == PlayBookOffensiveSubField)
            {
                offenseOrdefense = "Offense";
            }
            else if (strField == PlayBookDefensiveMainField || strField == PlayBookDefensiveSubField)
            {
                offenseOrdefense = "Defense";
            }

            return offenseOrdefense;
        }
        public System.Drawing.Image ReadPictureFromValue(string ScoutTypeFolder, string strField, string strValue)
        {
            string strImagePath = string.Format(@"{0}Bitmaps\{1}\{2}\{3}.bmp", PlayBookInstallPath, ScoutTypeFolder, strField, strValue);

            Image cloneImage = null;

            if (System.IO.File.Exists(strImagePath))
            {
                cloneImage = Webb.Utility.ReadImageFromPath(strImagePath);
            }

            return cloneImage;
        }

        public System.Drawing.Image ReadBaseFormation(string ScoutTypeFolder)
        {
            string strImagePath = string.Format(@"{0}Bitmaps\{1}\Base Formation.bmp", PlayBookInstallPath, ScoutTypeFolder);

            Image cloneImage = null;

            if (System.IO.File.Exists(strImagePath))
            {
                cloneImage = Webb.Utility.ReadImageFromPath(strImagePath);
            }
            return cloneImage;
        }
    
        #endregion


       
        public bool CheckIsPlaybookImageField(string strField)
        {
            if (strField == string.Empty||PlayBookInstallPath==string.Empty) return false;

            if (strField.EndsWith("(Off Main)") || strField.EndsWith("(Off Sub)") || strField.EndsWith("(Def Main)") || strField.EndsWith("(Def Sub)"))
            {
                return true;
            }

            return false;
        }

       
  
        #region IDisposable Members

        public void  Dispose()
        {
            playbookdataFiles = null;
        }

        #endregion
}
}