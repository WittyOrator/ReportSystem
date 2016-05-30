/***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:Class.cs
 * Author:Country.Wu [EMail:Webb.Country.Wu@163.com]
 * Create Time:11/7/2007 03:43:51 PM
 * Copyright:1986-2007@Webb Electronics all right reserved.
 * Purpose:
 * ***********************************************************************/
#region History
/*
 * //Author@DateTime : Description
 * */
#endregion History

using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Collections.Specialized;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Serialization;
using System.Diagnostics;
//
using Webb;
using Webb.Data;
using Webb.Reports;
using Webb.Collections;

namespace Webb.Reports.DataProvider
{
	[Serializable]
	public enum WebbDBTypes
	{//09-09-2008@Scott change values,add WebbVictoryBasketball and WebbVictoryHockey
		WebbAdvantageFootball = 0,

		WebbVictoryFootball = 100,
		WebbVictoryBasketball = 101,
		WebbVictoryVolleyball = 102,
		WebbVictoryHockey = 103,
        WebbVictoryLacrosse=104,
        WebbVictorySoccer=105,



	   //2009-6-15 13:46:52@Simon Add WebbDBTypes below
		QuickCuts=2,
		PlayMaker=3,
		PlayMaker_LE=4,
		PlayMaker_Basic=5,
		GameDay=8,

        CoachCRM=10,

        WebbPlaybook=20,

		Others = 10000,
	}


	#region public class DBSourceConfig
	/*Descrition:   */
	public class DBSourceConfig
	{
		private bool _UseSpecifiedConnstring = false;
		//
		private string _DefaultSQLCmd;
		private string _ConnString;
		private string _DBSourceName;
		private string _DBCatalogName;
		private string _FilePath;
		private string _UserFolder;
		private string _HeaderName;
		private Int32Collection _GameIDs;
		private Int32Collection _FilterIDs;
		private Int32Collection _SectionFilterIDs;
		private StringCollection _Games;
		private StringCollection _Edls;
		private StringCollection _Filters;
		private Webb.Data.DBConnTypes _DBConnType = DBConnTypes.File;
		private WebbDBTypes _WebbDBType = WebbDBTypes.WebbAdvantageFootball;
		private string _WartermarkImagePath;	//06-19-2008@Scott
		private StringCollection _Templates;	//08-22-2008@Scott
		private string _PrinterName;	        //08-22-2008@Scott		
        private StringCollection _PlayBookFormFiles = new StringCollection();

        public StringCollection PlayBookFormFiles  
        {
            get {
                if (_PlayBookFormFiles == null) _PlayBookFormFiles = new StringCollection();
                return this._PlayBookFormFiles;
                }
            set { _PlayBookFormFiles = value; }
        }
		public string PrinterName
		{
			get{return this._PrinterName;}
			set{this._PrinterName = value;}
		}

		public StringCollection Templates
		{
			get{return this._Templates;}
			set{this._Templates = value;}
		}

		public Int32Collection SectionFilterIDs
		{
			//protected object PropertyName;
			get{return this._SectionFilterIDs;}
			set{value.CopyTo(this._SectionFilterIDs);}
		}

		public Int32Collection GameIDs
		{
			get{return this._GameIDs;}
			set{value.CopyTo(this._GameIDs);}
		}

		public Int32Collection FilterIDs
		{
			get{return this._FilterIDs;}
			set{value.CopyTo(this._FilterIDs);}
		}

		public StringCollection Games
		{
			get{return this._Games;}
			set{this._Games = value;}
		}

		public StringCollection Edls
		{
			get{return this._Edls;}
			set{this._Edls = value;}
		}

		public StringCollection Filters
		{
			get{return this._Filters;}
			set{this._Filters = value;}
		}

		public string DBFilePath
		{
			get{return this._FilePath;}
			set{this._FilePath = value;}
		}

		public string DBSourceName
		{
			get{return this._DBSourceName;}
			set{this._DBSourceName = value;}
		}

		public string DBCatalogName
		{
			get{return this._DBCatalogName;}
			set{this._DBCatalogName = value;}
		}

		public string ConnString
		{
			get{return this._ConnString;}
			set
			{
				if(value==null||value==string.Empty)
				{
					this._ConnString = value;
					this._UseSpecifiedConnstring = false;
				}
				else if(this._ConnString!=value)
				{
					this._ConnString = value;
					this._UseSpecifiedConnstring = true;
				}
			}
		}

		public string DefaultSQLCmd
		{
			get{return this._DefaultSQLCmd;}
			set{this._DefaultSQLCmd = value;}
		}

		public string UserFolder
		{
			get{return this._UserFolder;}
			set{this._UserFolder = value;}
		}

		public string HeaderName
		{
			get{return this._HeaderName;}
			set
			{
				if(value == null || value == string.Empty)
				{
					this._HeaderName = "none";
				}
				else
				{
					this._HeaderName = value;
				}
			}
		}

		public string WartermarkImagePath
		{
			get{return this._WartermarkImagePath;}
			set{this._WartermarkImagePath = value;}
		}
		
		public DBSourceConfig()
		{
			// TODO: implement
			this._GameIDs = new Int32Collection();
			this._FilterIDs = new Int32Collection();
			this._SectionFilterIDs = new Int32Collection();
			this._Games = new StringCollection();
			this._Edls = new StringCollection();
			this._Filters = new StringCollection();
			this._Templates = new StringCollection();
			this._PrinterName = string.Empty;            
		}

		//Scott@2007-11-19 08:34 modified some of the following code.
		public DBSourceConfig(DBSourceConfig oldDBSourceConfig)
		{
			this._DefaultSQLCmd = oldDBSourceConfig._DefaultSQLCmd;
			this._ConnString = oldDBSourceConfig._ConnString;
			this._DBSourceName = oldDBSourceConfig._DBSourceName;
			this._DBCatalogName = oldDBSourceConfig._DBCatalogName;
			this._FilePath = oldDBSourceConfig._FilePath;
			this._DBConnType = oldDBSourceConfig._DBConnType;
			this._WebbDBType = oldDBSourceConfig._WebbDBType;
			this._DBConnType = oldDBSourceConfig.DBConnType;
			this._WebbDBType = oldDBSourceConfig.WebbDBType;
			oldDBSourceConfig.GameIDs.CopyTo(this._GameIDs);
			oldDBSourceConfig.FilterIDs.CopyTo(this._FilterIDs);
			oldDBSourceConfig.SectionFilterIDs.CopyTo(this._SectionFilterIDs);           
			this._Games = oldDBSourceConfig.Games;
			this._Edls = oldDBSourceConfig.Edls;
			this._UserFolder = oldDBSourceConfig.UserFolder;
			this._HeaderName = oldDBSourceConfig.HeaderName;
			this._WartermarkImagePath = oldDBSourceConfig.WartermarkImagePath;
			this._Templates = oldDBSourceConfig.Templates;
			this._PrinterName = oldDBSourceConfig._PrinterName;
			this._Filters = oldDBSourceConfig.Filters;
            this._PlayBookFormFiles = oldDBSourceConfig._PlayBookFormFiles;

		}

		public void ApplyConfig(DBSourceConfig oldDBSourceConfig)
		{
			this._DefaultSQLCmd = oldDBSourceConfig._DefaultSQLCmd;
			this._ConnString = oldDBSourceConfig._ConnString;
			this._DBSourceName = oldDBSourceConfig._DBSourceName;
			this._DBCatalogName = oldDBSourceConfig._DBCatalogName;
			this._FilePath = oldDBSourceConfig._FilePath;
			this._DBConnType = oldDBSourceConfig._DBConnType;
			this._WebbDBType = oldDBSourceConfig._WebbDBType;
			this.DBConnType = oldDBSourceConfig.DBConnType;
			this.WebbDBType = oldDBSourceConfig.WebbDBType;
			oldDBSourceConfig.GameIDs.CopyTo(this._GameIDs);
			oldDBSourceConfig.FilterIDs.CopyTo(this._FilterIDs);
			oldDBSourceConfig.SectionFilterIDs.CopyTo(this._SectionFilterIDs);
			this._Games = oldDBSourceConfig.Games;
			this._Edls = oldDBSourceConfig.Edls;
			this._UserFolder = oldDBSourceConfig.UserFolder;
			this._HeaderName = oldDBSourceConfig.HeaderName;
			this._WartermarkImagePath = oldDBSourceConfig.WartermarkImagePath;
			this._Templates = oldDBSourceConfig._Templates;
			this._PrinterName = oldDBSourceConfig._PrinterName;
			this._Filters = oldDBSourceConfig.Filters;
            this._PlayBookFormFiles = oldDBSourceConfig._PlayBookFormFiles;
		}
		//   
		public Webb.Data.DBConnTypes DBConnType
		{
			get
			{
				return _DBConnType;
			}
			set
			{
			//	if (this._DBConnType != value)
					this._DBConnType = value;
			}
		}
      
		public WebbDBTypes WebbDBType
		{
			get
			{
				return _WebbDBType;
			}
			set
			{
			//		if (this._WebbDBType != value)
					this._WebbDBType = value;
			}
		}

		public string CreateConnString()
		{
			if(this._UseSpecifiedConnstring) return this._ConnString;
			if(this.DBConnType==DBConnTypes.OleDB)
			{
				this._ConnString = Webb.Data.DBHelper.GetOleConnString(this.DBFilePath);				
			}
			else if(this.DBConnType==DBConnTypes.SQLDB)
			{
				this._ConnString = Webb.Data.DBHelper.GetSQLConnString(this.DBCatalogName,this.DBSourceName);
			}
			return this._ConnString;
		}
		
        //public string AnalysisConn(string connString)
        //{          
          
        //    if(connString.IndexOf("Server=")>=0)
        //    {
        //        if(!connString.StartsWith("Driver={SQL Server};"))
        //        {
        //             connString="Driver={SQL Server};"+connString;
        //        }
							
        //    }
        //    else if(connString.IndexOf("Data Source=")>=0)
        //    {
        //        if(!connString.StartsWith("Provider=SQLOLEDB;"))
        //        {
        //            connString="Provider=SQLOLEDB;"+connString;
        //        }
        //    }
        //    return connString;
        //}
		
		public string[] CreateConfigStrings(string filename)  //Added this code at 2009-1-22 17:13:38@Simon
		{
			string[] InitialString=new string[10];
			
			InitialString[0]="Path:"+filename;

			//1.get Action string   
			#region Get Action
				string[] subActionCmds=new string[4];

			    string diapath=VideoPlayBackManager.DiaPath.Replace(":","|");

				string diagram="[DIAGRAM:"+VideoPlayBackManager.DiagramScoutType+
                                "?INVERT:"+(VideoPlayBackManager.InvertDiagram?"1":"0")+
                                   "?DIAPATH:"+diapath+"]";			
           
                subActionCmds[0]=diagram;

				subActionCmds[1]="[WATERMARK:"+this._WartermarkImagePath+"]";

				subActionCmds[2]="[CLICKEVENT:"+(VideoPlayBackManager.ClickEvent?"1":"0")+"]";

			    subActionCmds[3]="[REPORT_SECTION_TYPE:"+((int)VideoPlayBackManager.AdvSectionType).ToString()+"]";

				InitialString[1]="Action:"+string.Join(",",subActionCmds);	
			#endregion

		
			//2.get Product string   			
             InitialString[2]="Product:"+this._WebbDBType.ToString();	
		
			
			bool EmptydataSource=(Webb.Reports.DataProvider.VideoPlayBackManager.DataSource==null);
			
             //3.Data source
			if(DBConnType!=Webb.Data.DBConnTypes.File&&!EmptydataSource)
			{
				if(DBFilePath=="victory.mdb")
				{
					InitialString[3]="DBConn:"+"Default_Victory";
				}
				else if(DBFilePath == "WebbFootball" )
				{
					InitialString[3]="DBConn:"+"Default_Advdantage";
				}
				else if(DBConnType==Webb.Data.DBConnTypes.SQLDB)
				{
                    InitialString[3]="DBConn:;"+this._ConnString;

				}
				else
				{
					InitialString[3]="DBConn:"+DBFilePath;
				}
			}
			else 
			{
				InitialString[3]="DBConn:";				
			}
			

            //4.SQlCmd
			 InitialString[4]="SQLCmd:"+this.DefaultSQLCmd;

			//5.Gameids
            InitialString[5]="GameIDs:";

			foreach(int gameid in this.GameIDs)
			{
               InitialString[5]+=gameid.ToString()+",";
			}
            InitialString[5].TrimEnd(" ,".ToCharArray());

            //6.Edls
            InitialString[6]="EdlIDs:";

			foreach(string edlid in this.Edls)
			{
				InitialString[6]+=edlid+",";
			}
			InitialString[6].TrimEnd(" ,".ToCharArray());


			//7.get FilterIDs
			InitialString[7]="FilterIDs:";

			foreach(int filterID in this.FilterIDs)
			{
               InitialString[7]+=filterID.ToString()+",";
			}

			InitialString[7]+="[Filters:";

            foreach(string filter in this.Filters)
			{
              InitialString[7]+=filter+",";
			}

            InitialString[7]+="]";	
		
			
           //8.files
			if(!EmptydataSource&&(DBConnType==Webb.Data.DBConnTypes.File||DBConnType==Webb.Data.DBConnTypes.SQLDB))
			{
				InitialString[8]=@"Files:[USER:"+this.UserFolder+"?HEADER:"+this.HeaderName+"]";

                if (this._WebbDBType == WebbDBTypes.WebbPlaybook)
                {
                    foreach (string playbookdata in this.PlayBookFormFiles)
                    {
                        InitialString[8] += "," + playbookdata;
                    }
                }
                else
                {
                    foreach (string game in this.Games)
                    {
                        InitialString[8] += "," + game;
                    }

                    foreach (string edl in this.Edls)
                    {
                        InitialString[8] += "," + edl;
                    }
                }
			}			
			else
			{
				InitialString[8]=@"Files:";
			}

            //9.print
            InitialString[9]="Print:0";


			return InitialString;

		}
	}
	#endregion

	#region public class GameInfo & GameInfoCollection
	/*Descrition:   */
	[Serializable]
	public class GameInfo
	{
		//Wu.Country@2007-11-09 10:51 AM added this class.
		private int _GameID;
		private string _Object;
		private string _Opponent;
		private string _GameDate;
		private string _Location;
		private int _FolderID;
		private string _GameName;
		private string _ScoutType;
		//Properties
		public string ScoutType
		{
			get{return this._ScoutType;}

			set{if(this._ScoutType != value) this._ScoutType = value;}
		}

		public int GameID
		{
			get{return _GameID;}
			
			set{if (this._GameID != value) this._GameID = value;}
		}
      
		public string Object
		{
			get{return _Object;}
			
			set{if (this._Object != value) this._Object = value;}
		}
      
		public string Opponent
		{
			get{return _Opponent;}

			set{if (this._Opponent != value) this._Opponent = value;}
		}
      
		public string GameDate
		{
			get{return _GameDate;}

			set{if (this._GameDate != value) this._GameDate = value;}
		}
      
		public string Location
		{
			get{return _Location;}

			set{if (this._Location != value) this._Location = value;}
		}
      
		public int FolderID
		{
			get{return _FolderID;}

			set{if (this._FolderID != value) this._FolderID = value;}
		}
      
		public string GameName
		{
			get{return _GameName;}

			set{if (this._GameName != value) this._GameName = value;}
		}

		//ctor
		public GameInfo()
		{

		}

        #region Copy Function By Macro 2010-12-28 13:58:10
		public GameInfo Copy()
        {
			GameInfo thiscopy=new GameInfo();
			thiscopy._GameID=this._GameID;
			thiscopy._Object=this._Object;
			thiscopy._Opponent=this._Opponent;
			thiscopy._GameDate=this._GameDate;
			thiscopy._Location=this._Location;
			thiscopy._FolderID=this._FolderID;
			thiscopy._GameName=this._GameName;
			thiscopy._ScoutType=this._ScoutType;
			return thiscopy;
        }
		#endregion
		//Methods
	}

	/*Descrition:   */
	[Serializable]
	public class GameInfoCollection : CollectionBase
	{
		//Wu.Country@2007-11-09 11:02:05 AM added this collection.
		//Fields
		//Properties
		public GameInfo this[int i_Index]
		{ 
			get { return this.InnerList[i_Index] as GameInfo; } 
			set { this.InnerList[i_Index] = value; } 
		} 
		//ctor
		public GameInfoCollection() {} 
		//Methods
		public int Add(GameInfo i_Object)
		{ 
			return this.InnerList.Add(i_Object);
		} 
		public void Remove(GameInfo i_Obj)
		{
			this.InnerList.Remove(i_Obj);
		} 
	}


	#endregion

    #region EdlInfo & EdlCollection

    #region add these code at 2010-12-28 14:55:30@simon
      /*Descrition:   */
    [Serializable]
    public class EdlInfo
    {
        //Wu.Country@2007-11-09 10:51 AM added this class.


        private int _EdlID = -1;

        private string _EDLName = string.Empty;

        private GameInfoCollection _RelatedGameInfos = new GameInfoCollection();

        //ctor
        public EdlInfo()
        {

        }

        public int EdlID
        {
            get
            {
                return _EdlID;
            }
            set
            {
                _EdlID = value;
            }
        }

        public string EDLName
        {
            get
            {
                return _EDLName;
            }
            set
            {
                _EDLName = value;
            }
        }

        public GameInfoCollection RelatedGameInfos
        {
            get
            {
                if (_RelatedGameInfos == null) _RelatedGameInfos = new GameInfoCollection();
                return _RelatedGameInfos;
            }
            set
            {
                _RelatedGameInfos = value;
            }
        }
        //Methods

        public bool ContainsGameInfo(GameInfo gameInfo)
        {
            foreach (GameInfo InnerGameInfo in this.RelatedGameInfos)
            {
                if (InnerGameInfo.GameName == gameInfo.GameName) return true;
            }
            return false;
        }
    }

        /*Descrition:   */
    [Serializable]
    public class EdlInfoCollection : CollectionBase
    {
        //Wu.Country@2007-11-09 11:02:05 AM added this collection.
        //Fields
        //Properties
        public EdlInfo this[int i_Index]
        {
            get { return this.InnerList[i_Index] as EdlInfo; }
            set { this.InnerList[i_Index] = value; }
        }
        //ctor
        public EdlInfoCollection() { }
        //Methods
        public int Add(EdlInfo i_Object)
        {
            return this.InnerList.Add(i_Object);
        }
        public void Remove(EdlInfo i_Obj)
        {
            this.InnerList.Remove(i_Obj);
        }
        public bool ContainsGameInfo(GameInfo gameInfo)
        {
            foreach (EdlInfo InnerEdlInfo in this.InnerList)
            {
                if (InnerEdlInfo.ContainsGameInfo(gameInfo)) return true;
            }

            return false;
        }
    }
    #endregion

    #endregion

    #region public class FilterInfo & FilterInfoCollection
    /*Descrition:   */
	public class FilterInfo
	{
		//Wu.Country@2007-11-09 10:57 AM added this class.
		//Fields

		//Properties

		//ctor
		public FilterInfo()
		{
		}
		//Methods

		private int _FilterID;
		private string _FilterName;
   
		public int FilterID
		{
			get
			{
				return _FilterID;
			}
			set
			{
				if (this._FilterID != value)
					this._FilterID = value;
			}
		}
      
		public string FilterName
		{
			get
			{
				return _FilterName;
			}
			set
			{
				if (this._FilterName != value)
					this._FilterName = value;
			}
		}
		public FilterInfo Copy()
		{
			FilterInfo ifilterinfo=new FilterInfo();
            ifilterinfo.FilterID=this.FilterID;
            ifilterinfo.FilterName=this.FilterName;
			return ifilterinfo;
		}
   
	}
	/*Descrition:   */
	[Serializable]
	public class FilterInfoCollection : CollectionBase
	{
		//Wu.Country@2007-11-09 11:02:37 AM added this collection.
		//Fields
		//Properties
		public FilterInfo this[int i_Index]
		{ 
			get { return this.InnerList[i_Index] as FilterInfo; } 
			set { this.InnerList[i_Index] = value; } 
		} 
		//ctor
		public FilterInfoCollection() {} 
		//Methods
		public int Add(FilterInfo i_Object)
		{ 
			return this.InnerList.Add(i_Object);
		} 
		public void Remove(FilterInfo i_Obj)
		{
			this.InnerList.Remove(i_Obj);
		} 
		public FilterInfoCollection Copy()
		{
			FilterInfoCollection filters=new FilterInfoCollection();

			foreach(FilterInfo filterinfo in this)
			{
               filters.Add(filterinfo.Copy());
			}
			return filters;
		}
	} 
	#endregion

	#region public class AdvFileInfo & AdvFileInfoCollection
	public class AdvFileInfo
	{
		private string _FileName;
		private string _FilePath;
		
		public string FileName
		{
			get{return this._FileName;}
			set{this._FilePath = value;}
		}

		public string FilePath
		{
			get{return this._FilePath;}
			set{this._FilePath = value;}
		}
		
		public override string ToString()
		{
			return this._FileName;
		}

		public AdvFileInfo(string strFileName,string strFilePath)
		{
			this._FileName = strFileName;
			this._FilePath = strFilePath;
		}
	}

	[Serializable]
	public class AdvFileInfoCollection : CollectionBase
	{
		public AdvFileInfo this[int i_Index]
		{ 
			get { return this.InnerList[i_Index] as AdvFileInfo; } 
			set { this.InnerList[i_Index] = value; } 
		} 
		
		public AdvFileInfoCollection() {} 
		
		public int Add(AdvFileInfo i_Object)
		{ 
			return this.InnerList.Add(i_Object);
		} 
		public void Remove(AdvFileInfo i_Obj)
		{
			this.InnerList.Remove(i_Obj);
		} 

		public bool Contain(AdvFileInfo i_Obj)
		{
			return this.InnerList.Contains(i_Obj);
		}
	}
	#endregion
}
