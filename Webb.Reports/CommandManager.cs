/***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:CommandManager.cs
 * Author:Country.Wu [EMail:Webb.Country.Wu@163.com]
 * Create Time:11/26/2007 08:27:50 AM
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
using System.IO;
//
using Webb.Reports.DataProvider;
using Webb.Collections;

//
namespace Webb.Reports.Browser
{
	#region public enum ActionTypes
	/*Descrition:   */
	public enum ActionTypes
	{
		Demo = 1,
		Print = 2,
		Preview = 4,
		SelectGames = 8,
		SelectFilters = 16,
	}
	#endregion
	/// <summary>
	/// Summary description for CommandManager.
	/// </summary>
	public class CommandManager
	{
		#region static member
		internal class StringResource
		{
			static internal readonly string Default_ConnString_Victory = "Default_Victory";
			static internal readonly string Default_Connstring_Advdantage = "Default_Advdantage";
			static internal readonly string Default_AccessFile_Victory = "victory.mdb";
			static internal readonly string Default_SQLCatalog = "WebbFootball";
		}

		static string[] M_CommandNames = new string[]{
			"Path",				//0.Report Template Path
			"Action",			//1.Report action. Print,Preview,Demo;
			"Product",			//2.Product name. Victory, Advantage,...
			"DBConn",			//3.DB connection string.
			"SQLCmd",			//4.Default sql command.
			"GameIDs",			//5.Game IDs.
			"EDLIDs",			//6.Edl IDs.
			"FilterIDs",		//7.Section filter IDs.
			"Files",			//8.Edl names.
			"Print",			//9.If print Directly.       
		};

		static bool IsAvailableCommand(string i_CmdName)
		{
			foreach(string m_cmd in M_CommandNames)
			{
				if(m_cmd==i_CmdName) return true;
			}
			return false;
		}

		static bool IsRID(string i_CmdName)
		{
			return M_CommandNames[0] == i_CmdName;
		}

		public static string GetSampleCommand()
		{
			//Victory		DB		"Path:E:\WebbReportsV3.Setup\WebbReportsV3.Setup\Webb.Reports.Setup\Resource\DemoReports\DemoReport_006.repx" "Action:" "Product:WebbVictoryFootball" "DBConn:E:\LowCost\Bin\Victory.mdb" "SQLCmd:select * from [GameDetail]" "GameIDs:35" "EdlIDs:" "FilterIDs:" "Files:" "Print:0"
			//Advantage		File	"Path:E:\WebbReportsV3.Setup\WebbReportsV3.Setup\Webb.Reports.Setup\Resource\DemoReports\DemoReport_006.repx" "Action:" "Product:WebbAdvantageFootball" "DBConn:" "SQLCmd:" "GameIDs:" "EdlIDs:" "FilterIDs:" "Files:E:\WEBB.WEBBREPORT.V3\Resource\Common\AFCA\Imported Games\1Angle VS Opponent ON Date AT Location-Offensive Self.exp" "Print:0"	
			return	"\"Path:E:\\WebbReportsV3.Setup\\WebbReportsV3.Setup\\Webb.Reports.Setup\\Resource\\DemoReports\\DemoReport_006.repx\" " +
					"\"Action:SelectGames\" " +
					"\"Product:WebbVictory\" " +
					"\"DBConn:E:\\LowCost\\Bin\\Victory.mdb\" " +
					"\"SQLCmd:select * from [GameDetail]\" " +
					"\"GameIDs:35\" " +
					"\"EdlIDs:\" " +
					"\"FilterIDs:\" " +
					"\"Files:\" " +
					"\"Print:0\" ";				 
		}
		#endregion

		private bool _IsCommandInitialized;
		private CommandCollection _Commands;

		public CommandManager()
		{
			this._Commands = new CommandCollection();
			this._IsCommandInitialized = false;
		}
		
		public CommandManager(string[] i_Commands)
		{
			this._Commands = new CommandCollection();
			this._IsCommandInitialized = false;
			this.AnalyzeCommand(i_Commands);
		}
		
		public bool AnalyzeCommand(string[] i_Commands)
		{
			this._Commands.Clear();
			
			foreach(string m_cmd in i_Commands)
			{
				Command m_Command = new Command(m_cmd);
				this._Commands.Add(m_Command);
			}

			this._IsCommandInitialized = true;
			
			return true;
		}
		public void SetCommandInitialized()
		{
			this._IsCommandInitialized=true;
		}

		public Webb.Reports.WebbReport CreateReport(string exePath,string templatePath)
		{
			if(!this._IsCommandInitialized)	return null;
			
			string strXtraReportFilePath = templatePath;
			
			WebbReport m_Report = null;

			bool ClickEvent=VideoPlayBackManager.ClickEvent;

			if(strXtraReportFilePath != string.Empty)
			{
				m_Report = new WebbReport();

				if(File.Exists(strXtraReportFilePath))
				{
					Webb.Utility.ReplaceRefPathInRepxFile(strXtraReportFilePath,exePath);	//Modify template file

					m_Report.LoadLayout(strXtraReportFilePath);		

					Webb.Utility.CurFileName = strXtraReportFilePath;	//06-23-2008@Scott

					#region Modify codes at 2009-4-9 10:26:35@Simon//
					string strWebbReportTemplateFilePath = strXtraReportFilePath.Replace(Webb.Utility.ReportExt,Webb.Utility.WebbReportExt);
				
					if(strWebbReportTemplateFilePath.EndsWith(Webb.Utility.WebbReportExt)&&File.Exists(strWebbReportTemplateFilePath))
					{
						File.Delete(strWebbReportTemplateFilePath);
					}
					#endregion        //End Modify
				}
			}
           VideoPlayBackManager.ClickEvent=ClickEvent;

			return m_Report;
		}

		public Webb.Reports.DataProvider.DBSourceConfig CreateDBConfig()
		{
			DBSourceConfig m_DBConfig = new DBSourceConfig();
		
			this.InitializeDBConfig(m_DBConfig);
			
			return m_DBConfig;
		}

        private void GetProductType(string strProductCmd,DBSourceConfig i_Config)
        {
           
            if (strProductCmd.ToLower() == "advantage")
            {
                i_Config.WebbDBType = WebbDBTypes.WebbAdvantageFootball;
            }
            else
            {
                try
                {
                    i_Config.WebbDBType = (WebbDBTypes)Enum.Parse(typeof(WebbDBTypes), strProductCmd, true);
                }
                catch
                {

                    i_Config.WebbDBType = WebbDBTypes.WebbAdvantageFootball;
                }
            }
        }

		private void GetFiltersName(DBSourceConfig i_Config)
		{
			string m_StrFilterCmd = this._Commands.GetValue(M_CommandNames[7]);
			
			int indexStart = m_StrFilterCmd.IndexOf('[');
			int indexEnd = m_StrFilterCmd.IndexOf(']');
			string m_StrFilterIDs = string.Empty;

			if(indexStart < indexEnd)
			{
				if(indexStart == 0)
				{
					m_StrFilterIDs = this._Commands.GetValue(M_CommandNames[7]).Substring(indexStart + 1, indexEnd - indexStart - 1);

					string[] arrFilterGroup = m_StrFilterIDs.Split('?');

					int index = arrFilterGroup[0].IndexOf(":");

					string strFilters = arrFilterGroup[0].Substring(index + 1,arrFilterGroup[0].Length - index - 1);

					string[] arrFilters = strFilters.Split(',');

					i_Config.Filters = new StringCollection();

					foreach(string strFilter in arrFilters)
					{ 
						if(strFilter!=string.Empty)
						{
                           i_Config.Filters.Add(strFilter);
						}
						
					}
				}
				else
				{
					if(indexStart < 0)
					{
						m_StrFilterIDs = this._Commands.GetValue(M_CommandNames[7]);
					}
					else
					{
						m_StrFilterIDs = this._Commands.GetValue(M_CommandNames[7]).Substring(0,indexStart);
					}

					string[] m_StrFilterIDArray = m_StrFilterIDs.Split(',');
					i_Config.FilterIDs = new Int32Collection();
					foreach(string i_FilterID in m_StrFilterIDArray)
					{
						try
						{
							if(i_FilterID == string.Empty) continue;
							int m_FilterID = Convert.ToInt32(i_FilterID);
							i_Config.FilterIDs.Add(m_FilterID);
						}
						catch
						{
							continue;
						}
					}
				}
			}
        }
      
        private void InitializeDBConfig(DBSourceConfig i_Config)
        {
            //[0]TemplatePath
            i_Config.Templates.Clear();

            string strTemplates = this._Commands.GetValue(M_CommandNames[0]);

            string[] arrTemplates = strTemplates.Split(',');

            foreach (string strTemplate in arrTemplates)
            {
                string strTemplateName = this.GetTemplateName(strTemplate, '@');
                
                if (File.Exists(strTemplateName))
                {
                    i_Config.Templates.Add(strTemplate);
                }
            }

            //[1]Action		
            string strActionCmd = this._Commands.GetValue(M_CommandNames[1]);

            this.GetActions(strActionCmd, i_Config);	//06-19-2008@Scott


            //[2]Product
          
            string strProductCmd = this._Commands.GetValue(M_CommandNames[2]);

            this.GetProductType(strProductCmd, i_Config); 

            //[7]FilterIDs
            this.GetFiltersName(i_Config);


            //[8]Files
            if (this._Commands.GetValue(M_CommandNames[8]) != string.Empty)
            {
                i_Config.DBConnType = Webb.Data.DBConnTypes.File;

                this.GetFiles(M_CommandNames[8], i_Config);

                #region Modify codes at 2009-2-2 15:32:34@Simon

                string m_ConnStr = this._Commands.GetValue(M_CommandNames[3]).Trim();  

                if (m_ConnStr != "")
                {
                    i_Config.ConnString = m_ConnStr;

                    i_Config.DefaultSQLCmd = this._Commands.GetValue(M_CommandNames[4]);

                    i_Config.DBConnType = Webb.Data.DBConnTypes.SQLDB;
                }               
                #endregion        //End Modify

                return;
            }

            //[3]DBConnStr
            string m_ConnTypes = this._Commands.GetValue(M_CommandNames[3]);
            if (m_ConnTypes == StringResource.Default_ConnString_Victory)
            {
                i_Config.DBConnType = Webb.Data.DBConnTypes.OleDB;
                i_Config.DBFilePath = StringResource.Default_AccessFile_Victory;
            }
            else if (m_ConnTypes == StringResource.Default_Connstring_Advdantage)
            {
                i_Config.DBConnType = Webb.Data.DBConnTypes.SQLDB;
                i_Config.DBFilePath = StringResource.Default_SQLCatalog;
            }
            else
            {
                i_Config.DBConnType = Webb.Data.DBConnTypes.OleDB;               

                if (File.Exists(m_ConnTypes))
                {
                    i_Config.DBFilePath = m_ConnTypes;
                }
                else
                {
                    i_Config.DBFilePath = StringResource.Default_AccessFile_Victory;
                }
            }
            if (i_Config.WebbDBType == WebbDBTypes.CoachCRM)
            {
                i_Config.DBConnType = Webb.Data.DBConnTypes.XMLFile;
            }

            //[4]SQLCmd
            i_Config.DefaultSQLCmd = this._Commands.GetValue(M_CommandNames[4]);

            //[5]GameIDs
            string m_StrGameIDs = this._Commands.GetValue(M_CommandNames[5]);
            string[] m_StrGameIDArray = m_StrGameIDs.Split(',');
            i_Config.GameIDs = new Int32Collection();
            foreach (string i_GameID in m_StrGameIDArray)
            {
                try
                {
                    if (i_GameID == string.Empty) continue;
                    int m_GameID = Convert.ToInt32(i_GameID);
                    i_Config.GameIDs.Add(m_GameID);
                }
                catch
                {
                    continue;
                }
            }

            //[6]EdlIDs
            string m_StrEDLIDs = this._Commands.GetValue(M_CommandNames[6]);
            string[] m_StrEDLIDArray = m_StrEDLIDs.Split(',');
            i_Config.Edls = new StringCollection();
            foreach (string i_EDLID in m_StrEDLIDArray)
            {
                try
                {
                    if (i_EDLID == string.Empty) continue;
                    int m_EDLID = Convert.ToInt32(i_EDLID);
                    i_Config.Edls.Add(i_EDLID);
                }
                catch
                {
                    continue;
                }
            }

            //[9]Print
        }
        

   

        private void GetFiles(string command,DBSourceConfig i_Config)
		{
			i_Config.Games.Clear();
			i_Config.Edls.Clear();
			i_Config.UserFolder = string.Empty;
			i_Config.HeaderName = string.Empty;
            i_Config.PlayBookFormFiles.Clear();

			string strFiles = this._Commands.GetValue(command);

			if(strFiles == null || strFiles == string.Empty) return;

			string[] arrFiles = strFiles.Split(',');

			//System.Windows.Forms.MessageBox.Show(strFiles);

			foreach(string strFile in arrFiles)
			{
				if(strFile == string.Empty) continue;

				if(strFile.EndsWith(ExtConst.GameExt))
				{
					//System.Windows.Forms.MessageBox.Show(strFile);
					
					i_Config.Games.Add(strFile);
				}
				else if(strFile.EndsWith(ExtConst.EdlExt))
				{
					i_Config.Edls.Add(strFile);
				}               
                else if (strFile.StartsWith(@"[") && strFile.EndsWith(@"]"))
                {
                    string strUserCmd = strFile.Substring(1, strFile.Length - 2);

                    this.GetUserInfo(strUserCmd, i_Config);
                }
                else if (i_Config.WebbDBType==WebbDBTypes.WebbPlaybook && System.IO.File.Exists(strFile))
                {
                    i_Config.PlayBookFormFiles.Add(strFile);
                }  
                else
                {
                    continue;
                }
			}
		}

		private void GetUserInfo(string strUserCmd,DBSourceConfig i_Config)
		{
			string[] arrCmd = strUserCmd.Split('?');

			string strCmdHeader,strCmdValue;

			foreach(string strCmd in arrCmd)
			{
				int index = strCmd.IndexOf(@":");
			
				if(index <= 0) continue;

				strCmdHeader = strCmd.Substring(0,index);

				strCmdValue = strCmd.Remove(0,index + 1);

				switch(strCmdHeader)
				{
					case "USER":
						i_Config.UserFolder = strCmdValue;
						break;
					case "HEADER":
						i_Config.HeaderName = strCmdValue;
						break;
					default:
						continue;
				}
			}
		}

		//Parse action command
		private void GetActions(string strActionCmd,DBSourceConfig i_Config)
		{
			string[] subActionCmds = strActionCmd.Split(',');

			foreach(string subActionCmd in subActionCmds)
			{
				if(!subActionCmd.StartsWith("[") || !strActionCmd.EndsWith("]")) continue;

				string strSubAction = subActionCmd.Substring(1,subActionCmd.Length - 2);

				int index = strSubAction.IndexOf(":");

				if(index < 0) continue;

				string strActionName = strSubAction.Substring(0,index);

				string strActionValue = strSubAction.Substring(index + 1,strSubAction.Length - index - 1);

				switch(strActionName)
				{
					case "WATERMARK":
					{
						if(System.IO.File.Exists(strActionValue)) i_Config.WartermarkImagePath = strActionValue;

						break;
					}
					case "CLICKEVENT":
					{
						VideoPlayBackManager.ClickEvent = strActionValue != "0";

						break;
					}
					case "DIAGRAM":
					{
						string[] arrDiagram = strActionValue.Split('?');
						if(arrDiagram.Length > 0)
						{
							VideoPlayBackManager.DiagramScoutType = arrDiagram[0];
						}

						#region Modify codes at 2009-7-1 10:16:16@Simon
						for(int i=1;i<arrDiagram.Length;i++)
						{
							string diatype=arrDiagram[i].Trim();

							if(diatype.ToUpper().StartsWith("INVERT:"))
							{
								string invert=diatype.Remove(0,7);
																								
								VideoPlayBackManager.InvertDiagram=(invert=="1"?true:false);
							}
							else if(diatype.ToUpper().StartsWith("DIAPATH:"))
							{
								string diapath=diatype.Remove(0,8);							
																	
								VideoPlayBackManager.DiaPath=diapath.Replace("|",":");								
							}
						}
						#endregion        //End Modify

						break;
					}
					case "REPORT_SECTION_TYPE":                          //2009-6-9 11:46:56@Simon Add this Code
					{
						try
						{
							int ScTypeValue=Convert.ToInt32(strActionValue);

							VideoPlayBackManager.AdvSectionType=(AdvScoutType)ScTypeValue;
						}
						catch
						{
                            VideoPlayBackManager.AdvSectionType=AdvScoutType.None;
						}

						break;
					}
					default:
						break;
				}
			}
		}

		public bool UnionPrint //Added at 2008-10-10 10:07:42@Simon
		{
			get
			{
				string m_UnionPrint = this._Commands.GetValue(M_CommandNames[9]).Trim();
				if(m_UnionPrint.Length>=1)
				{
					int index=m_UnionPrint.IndexOf("?");

					if(index>0)
					{
						m_UnionPrint=m_UnionPrint.Substring(0,index).Trim();
					}                        
					if(m_UnionPrint.Length>1)
					{
						return true;
					}
					else
					{
						return false;
					}

				}
				else
					return false;
			}
		} //Modified at 2008-10-10 10:07:59@Simon	

		public bool PrintDirectly
		{
			get
			{
				try
				{
					string m_Print = this._Commands.GetValue(M_CommandNames[9]).Trim();
                    if(m_Print.Length<1) return false;
					m_Print = m_Print.Substring(0,1);

					if(Convert.ToInt32(m_Print) > 0) return true;
				}
				catch{return false;}

				return false;
			}
		}

		public string PrinterName
		{
			get
			{
				string m_Print = string.Empty;

				if(this._Commands == null)
				{
					return m_Print;
				}
				
				try
				{
					m_Print = this._Commands.GetValue(M_CommandNames[9]).Trim();

					int index=m_Print.IndexOf("?");

					if(index<0)return string.Empty;

					m_Print = m_Print.Remove(0,index+1);
				}
				catch{return string.Empty;}

				return m_Print;
			}
		}

        //09-01-2011@Scott
        public string GetAttachedHeader(string strTemplateName, char cSpliter)
        {
            string strHeader = string.Empty;

            string[] strArray = strTemplateName.Split(cSpliter);

            if (strArray.Length > 2)
            {
                strHeader = strArray[2];
            }

            return strHeader;
        }

		//Modified at 2008-11-24 15:22:55@Scott
		public string GetAttachedFilter(string strTemplateName,char cSpliter)
		{
			string strFilter = string.Empty;

            string[] strArray = strTemplateName.Split(cSpliter);

            if (strArray.Length > 1)
            {
                strFilter = strArray[1];
            }

			return strFilter;
		}

		//Modified at 2009-2-3 9:15:12@Scott
		public string GetTemplateName(string strTemplateName,char cSpliter)
		{
			string strTemplate = string.Empty;

			string[] strArray = strTemplateName.Split(cSpliter);

            if (strArray.Length > 0)
            {
                strTemplate = strArray[0];
            }

			return strTemplate;
		}
	}

	public class ExtConst
	{
		public const string GameExt = ".exp";
		public const string EdlExt = ".edl";
        public const string PlayBookDataExt = ".Form";
	}

	#region public class Command & collection
	//Wu.Country@2007-11-26 10:28 added this region.
	public class Command
	{
		public string Name;
		public string Value;
		public Command(string i_Command)
		{
			int nFirstComa = i_Command.IndexOf(':');
			this.Name = i_Command.Substring(0,nFirstComa);
			this.Value = i_Command.Remove(0,nFirstComa+1);
		}
	}

	/*Descrition:   */
	[Serializable]
	public class CommandCollection : CollectionBase
	{
		//Wu.Country@2007-11-26 10:43:18 AM added this collection.
		//Fields
		//Properties
		public Command this[int i_Index]
		{ 
			get { return this.InnerList[i_Index] as Command; } 
			set { this.InnerList[i_Index] = value; } 
		} 
		//ctor
		public CommandCollection() {} 
		//Methods
		public int Add(Command i_Object)
		{ 
			return this.InnerList.Add(i_Object);
		} 
		public void Remove(Command i_Obj)
		{
			this.InnerList.Remove(i_Obj);
		}
		public string GetValue(string i_Key)
		{
			foreach(Command m_cmd in this)
			{
				if(m_cmd.Name.ToLower()==i_Key.ToLower())
				{
					return m_cmd.Value;
				}
			}
			return null;
		}
	} 	
	#endregion
}

