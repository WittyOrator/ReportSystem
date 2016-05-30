/***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:MainProgram.cs
 * Author:Country.Wu [EMail:Webb.Country.Wu@163.com]
 * Create Time:11/26/2007 12:39:38 PM
 * Copyright:1986-2007@Webb Electronics all right reserved.
 * Purpose:
 * ***********************************************************************/
#region History
/*
 * //Author@DateTime : Description
 * */
#endregion History

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Threading;
using System.Drawing.Printing;
//
using Webb;
using Webb.Collections;
using Webb.Data;
using Webb.Utilities;
//
using Webb.Reports;
using Webb.Reports.DataProvider;
using Webb.Reports.ReportDesigner;
using Webb.Reports.ReportManager;
using System.Text;
using System.IO;
using Webb.Reports.ExControls;


//08-21-2008@Scott[1.1]

namespace Webb.Reports.Browser
{

	/// <summary>
	/// Summary description for MainProgram.
	/// </summary>
	public class MainProgram
	{
		
		public MainProgram()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{              
			if(args==null||(args.Length !=1&&args.Length!=10))
			{
				return;
			}
			
			ReportFileType fileType=ReportFileType.Report;

			DataSet dsDataset=null;

			if(args.Length==1)
			{
				string filename=args[0];

				if (filename.IndexOf("\n") > 0)
                {
                    args = filename.Split("\n".ToCharArray());

                    if (args.Length != 10) return;
                }
                else if (filename.EndsWith(".wmks"))
                {
                    wmksFileManager.WriteFile(filename);

                    return;
                }
                else if (filename.EndsWith(".repx"))
                {
                    string Inwfilename = filename.Replace(".repx", ".inw");

                    args = InwManager.ReadInwFile(Inwfilename);

                    fileType = ReportFileType.InwFile;

                    if (args == null)
                    {
                        args = ConfigFileManager.ReadDataConfig(filename);

                        fileType = ReportFileType.BaseXmlFile;
                    }
                }
                else if (filename.EndsWith(".repw"))
                {
                    string Inwfilename = filename.Replace(".repw", ".inw");

                    args = InwManager.ReadInwFile(Inwfilename);

                    fileType = ReportFileType.InwFile;

                    if (args == null)
                    {
                        args = ConfigFileManager.ReadDataConfig(filename);

                        fileType = ReportFileType.BaseXmlFile;
                    }
                }
                else if (filename.EndsWith(".inw"))
                {
                    args = InwManager.ReadInwFile(filename);

                    fileType = ReportFileType.InwFile;
                }
                else if (filename.EndsWith(".wrdf"))
                {
                    WrdfFileManager.ReadDataConfig(filename, out dsDataset, out args);

                    fileType = ReportFileType.WebbDataFile;
                }
                else
                {
                    return;
                }
				if(args==null)return;							
			}

			if(args[3]=="DBConn:"&&args[8]==@"Files:")return;

			Webb.Utility.CurReportMode = 1;	//set browser mode

			ThreadStart ts = new ThreadStart(LoadingThreadProc);

			Thread thread = new Thread(ts);

			thread.Start();

			CommandManager m_CmdManager = new CommandManager(args);

			//Calculate data source
			if(thread.IsAlive)
			{
				LoadingForm.MessageText = "Loading Data Source...";
			}

            DBSourceConfig m_Config = m_CmdManager.CreateDBConfig();

            if (m_Config.Templates.Count == 0)
            {
                if (thread.IsAlive)
                {
                    LoadingForm.Close();

                    thread.Abort();
                    
                }

                Webb.Utilities.TopMostMessageBox.ShowMessage("Invalid template report name!", MessageBoxButtons.OK);

                Environment.Exit(0);
            }

			
			if(m_CmdManager.PrintDirectly)
			{
				if(PrinterSettings.InstalledPrinters.Count == 0)
				{
					if(thread.IsAlive)
					{
						LoadingForm.Close();

                        thread.Abort();
					}

					Webb.Utilities.TopMostMessageBox.ShowMessage("No printer driver is installed!",MessageBoxButtons.OK);

					Environment.Exit(0);
				}
			}

			WebbDataProvider m_DBProvider = new WebbDataProvider(m_Config);
			
			WebbDataSource m_DBSource = new WebbDataSource();
            
			if(fileType==ReportFileType.WebbDataFile&&dsDataset==null)
			{				
				m_DBSource.DataSource=dsDataset.Copy();
					
			}
			else
			{
				m_DBProvider.GetDataSource(m_Config, m_DBSource);	
			}           

			ArrayList m_Fields = new ArrayList();

			foreach(System.Data.DataColumn m_col in m_DBSource.DataSource.Tables[0].Columns)
			{
                if (m_col.Caption=="{EXTENDCOLUMNS}"&&m_col.ColumnName.StartsWith("C_")) continue;

				m_Fields.Add(m_col.ColumnName);
			}


            Webb.Reports.DataProvider.VideoPlayBackManager.PublicDBProvider = m_DBProvider;

			Webb.Data.PublicDBFieldConverter.SetAvailableFields(m_Fields);

            Webb.Reports.DataProvider.VideoPlayBackManager.LoadAdvScFilters();	//Modified at 2009-1-19 13:48:30@Scott

            Webb.Reports.DataProvider.VideoPlayBackManager.ReadPictureDirFromRegistry();          

            m_DBProvider.UpdateEFFDataSource(m_DBSource);

            Webb.Reports.DataProvider.VideoPlayBackManager.DataSource = m_DBSource.DataSource;	//Set dataset for click event            
         

			//Loading report template
			if(thread.IsAlive)
			{
				LoadingForm.MessageText = "Loading Report Template...";
			}
            
			#region Modified Area 
		    ArrayList printedReports=new ArrayList();

			ArrayList invalidateReports=new ArrayList();

			bool unionprint=m_CmdManager.UnionPrint;

			#endregion        //End Modify at 2008-10-10 14:29:49@Simon


			FilterInfoCollection filterInfos=m_DBSource.Filters;  //2009-7-1 11:09:08@Simon Add this Code  For Union Print

			if(filterInfos==null)filterInfos=new FilterInfoCollection();

		    string printerName=m_CmdManager.PrinterName;
			
			foreach(string strTemplate in m_Config.Templates)
			{
				string strTemplateName = m_CmdManager.GetTemplateName(strTemplate,'@');	//Modified at 2009-2-3 9:17:34@Scott

				WebbReport m_Report = null;				

				try
				{
					m_Report = m_CmdManager.CreateReport(Application.ExecutablePath,strTemplateName);	//1 //create report with template

                    //09-01-2011@Scott
                    if (m_Config.WebbDBType == WebbDBTypes.WebbPlaybook)
                    {
                        SetReportHeader(m_Config, m_Report, m_Config.HeaderName);  //Add this code at 2011-7-28 16:23:41@simon
                    }
                    else
                    {
                        string strHeader = m_CmdManager.GetAttachedHeader(strTemplate, '@');

                        SetReportHeader(m_Config, m_Report, strHeader);
                    }
                    //End
				}
				catch(Exception ex)
				{
					Webb.Utilities.TopMostMessageBox.ShowMessage("Error", "Can't load report template!\r\n" + ex.Message, MessageBoxButtons.OK);
				
					m_Report = new WebbReport();
				}
                
                 bool Canopen=CheckedUserRight(m_Report.LicenseLevel,m_Config.WebbDBType);

				string filename=System.IO.Path.GetFileNameWithoutExtension(strTemplateName);

				if(!Canopen)
				{
					invalidateReports.Add(filename);						
				}
				else
				{
				
					//Add attached filter here
					#region Modified Area

					m_DBSource.Filters= filterInfos.Copy();  //2009-7-1 11:09:04@Simon Add this Code  For Union Print 

					string strFilterName = m_CmdManager.GetAttachedFilter(strTemplate,'@');

					if(strFilterName!=string.Empty)  //2009-7-1 11:09:04@Simon For display Filternames In GameListInfo
					{
						if(!m_DBProvider.DBSourceConfig.Filters.Contains(strFilterName))
						{
							FilterInfo filterInfo=new FilterInfo();

							filterInfo.FilterName=strFilterName;

							m_DBSource.Filters.Add(filterInfo); 
						}
					}

					ScAFilter scaFilter = Webb.Reports.DataProvider.VideoPlayBackManager.AdvReportFilters.GetFilter(strFilterName);	//Modified at 2009-1-19 14:25:30@Scott
					AdvFilterConvertor convertor = new AdvFilterConvertor();
					DBFilter AdvFilter=convertor.GetReportFilter(scaFilter).Filter;

					if(AdvFilter!=null||AdvFilter.Count>0)  //2009-5-6 9:38:37@Simon Add this Code
					{
						AdvFilter.Add(m_Report.Template.Filter);
						m_Report.Template.Filter =AdvFilter ;
					}

					SectionFilterCollection sectionFilter=m_Report.Template.SectionFilters.Copy();

					if(m_Report.Template.ReportScType==ReportScType.Custom)
					{
						m_Report.Template.SectionFilters=AdvFilterConvertor.GetCustomFilters(Webb.Reports.DataProvider.VideoPlayBackManager.AdvReportFilters,sectionFilter);
					}		
				
					#endregion        //Modify at 2008-11-24 16:04:05@Scott

					//Set data source
					if(thread.IsAlive)
					{
						LoadingForm.MessageText = "Set Data Source...";
						LoadingForm.ProcessText = Webb.Utility.GetCurFileName();
					}

					m_Report.LoadAdvSectionFilters(m_Config.UserFolder);

					m_Report.SetWatermark(m_Config.WartermarkImagePath);	//06-19-2008@Scott

					m_Report.SetDataSource(m_DBSource);                  
				}

				if(m_CmdManager.PrintDirectly)
				{
					if(!Canopen)
					{
						if(!unionprint)
						{
							Webb.Utilities.TopMostMessageBox.ShowMessage("LicenseLevel Error", "This report is not designed for your Webb application!" + "", MessageBoxButtons.OK);
						}
						continue;
					}
					else
					{
						printedReports.Add(m_Report);
					}

					#region Modified Area	
					if(unionprint)continue;		//Modified at 2008-10-10 10:04:37@Simon	
	 
					//Print					     
					if(Webb.Utility.CancelPrint)
					{
						if(thread.IsAlive)
						{
							LoadingForm.Close();
						
							thread.Join();
						}
	
						return;
					}
	
					if(thread.IsAlive)
					{
						LoadingForm.MessageText = "Printing...";
					}	               

					if(printerName!= string.Empty)
					{
						if(!PrinterExist(printerName))
						{
							Webb.Utilities.TopMostMessageBox.ShowMessage("Failed to Print","WRB Cann't Find The Printer '"+printerName+"' in you system,please check the printer setting!",
								              MessageBoxButtons.OK);
							Environment.Exit(-1);
						}
						m_Report.Print(printerName);
					}
					else
					{
						m_Report.Print();
					}     				
					#endregion        //End Modify at 2008-10-9 16:54:58@Simon
				}
				else
				{//Browser
					//Create report
					if(thread.IsAlive)
					{
						LoadingForm.MessageText = "Creating Report Browser...";
					}

					WebbRepBrowser m_Browser = new WebbRepBrowser();

					//m_Browser.LoadReport(new WebbReport[]{m_Report,m_Report});	//multiply report

                    if (m_Config.WebbDBType.ToString().ToLower().StartsWith("webbvictory"))
                    {
                        m_Browser.TopMost = true;
                    }
                    else
                    {
                        m_Browser.TopMost = false;
                    }
					if(Canopen)
					{ 
						m_Browser.LoadReport(m_Report);
					}

					if(thread.IsAlive)
					{
						LoadingForm.Close();

						thread.Join();
					}

					Webb.Reports.DataProvider.VideoPlayBackManager.PublicBrowser = m_Browser;	//05-04-2008@Scott
					
					if(!Canopen)
					{ 
						m_Browser.ReportName=filename;

						m_Browser.InvertZorder();
//						Webb.Utilities.TopMostMessageBox.ShowMessage("LicenseLevel Error", "This report is not designed for your Webb application!\n So it would not open" + "", MessageBoxButtons.OK);
					}

					Application.Run(m_Browser);	
				}
			}
			//add these codes for join all reports to print in only one document
			#region Modified Area

			WebbReport[] AllReportsToPrint=new WebbReport[printedReports.Count];

			for(int i=0;i<printedReports.Count;i++)
			{
				AllReportsToPrint[i]=printedReports[i] as WebbReport;
			}			

			if(m_CmdManager.PrintDirectly && unionprint)
			{
				if(AllReportsToPrint.Length==0)
				{
					Webb.Utilities.TopMostMessageBox.ShowMessage("No document","No document could be print!",
						MessageBoxButtons.OK);
					
				}
				else
				{					
					if(thread.IsAlive)
					{
						LoadingForm.MessageText = "Printing...";						
					
						LoadingForm.ProcessText="Union Printing Documents";
					}

					if(invalidateReports.Count>0)
					{						
						Webb.Utilities.AutoClosedMessageBox.ShowMessage(invalidateReports);						
					}					
					if(printerName!= string.Empty)
					{
						if(!PrinterExist(printerName))
						{
							Webb.Utilities.TopMostMessageBox.ShowMessage("Failed to Print","WRB Cann'i Find The Printer '"+printerName+"' in you system,\nplease check the printer setting!",
								MessageBoxButtons.OK);
							Environment.Exit(-1);
						}

						WebbReport.Print(printerName,AllReportsToPrint);
					}
					else
					{
                       WebbReport.Print(AllReportsToPrint);                        
					}
				}
			}
			#endregion        //End Modify at 2008-10-10 9:42:07@Simon

			if(thread.IsAlive)
			{
				LoadingForm.Close();
					
				thread.Join();
			}
		}

		public static void LoadingThreadProc()
		{
			LoadingForm.ShowDialog();
		}

		static LoadingForm LoadingForm = new LoadingForm();
	
		public static void RegisterComs()
		{
			//			// get file path
			//			string m_strWorkDirectory = Application.StartupPath;
			//			string strComp1 = m_strWorkDirectory + "DvSourceFilter.ax";
			//			string strComp2 = m_strWorkDirectory + "WebbSource.ax";
			//
			//				// regist
			//				HMODULE hModDllReg = ::LoadLibrary(strComp1);
			//				if(hModDllReg)
			//			{
			//				typedef int (__stdcall *pFn)(void);
			//			
			//				pFn fnn = GetProcAddress(hModDllReg, "DllRegisterServer"); //DllUnregisterServer
			//				if(fnn != NULL)
			//			{
			//				fnn();
			//			}
			//			
			//				::FreeLibrary(hModDllReg);
			//			}
			//			
			//				//	CComPtr<IBaseFilter>pWebbSourceFilter;
			//				//	HRESULT hr = CoCreateInstance(CLSID_WebbSource, NULL, 
			//				//		CLSCTX_INPROC_SERVER, IID_IBaseFilter, (void**)&pWebbSourceFilter);
			//				//	if (SUCCEEDED(hr))
			//				//	{
			//				//	}
			//				//	else
			//			{
			//				hModDllReg = ::LoadLibrary(strComp2);
			//				if(hModDllReg)
			//			{
			//				typedef int (__stdcall *pFn)(void);
			//						
			//				pFn fnn = GetProcAddress(hModDllReg, "Dll RegisterServer"); //DllUnregisterServer
			//				if(fnn != NULL)
			//			{
			//				fnn();
			//			}
			//						
			//				::FreeLibrary(hModDllReg);
			//			}
			//			}
		}

		public static bool CheckedUserRight(UserLevel level,WebbDBTypes webbDBtype)
		{ 
			if((int)level.Rights==31)return true;

            if(level.Rights==ProductRight.None)return false;			
			
			string[] rights=level.Rights.ToString().ToLower().Split(',');

			string dbtype=webbDBtype.ToString().ToLower().Trim();

			foreach(string right in rights)
			{
				if(dbtype.IndexOf(right.Trim())>=0)return true;
			}

			return false;
		}

        public static void SetReportHeader(DBSourceConfig config, WebbReport webbReport,string strHeader)
        {
            //09-01-2011@Scott
            if (strHeader == string.Empty)
            {
                return;
            }
            //End

            string strReportHeader = strHeader;

            if (strReportHeader == null || strReportHeader.Trim() == string.Empty || strReportHeader.ToLower() == "none") return;

            // report header
            DevExpress.XtraReports.UI.Band band = webbReport.Bands[DevExpress.XtraReports.UI.BandKind.ReportHeader];

            if (band != null)
            {
                foreach (DevExpress.XtraReports.UI.XRControl xrControl in band.Controls)
                {
                    if (!(xrControl is DevExpress.XtraReports.UI.WinControlContainer)) continue;

                    Control c = (xrControl as DevExpress.XtraReports.UI.WinControlContainer).WinControl;

                    if (c is LabelControl)    
                    {
                        if (config.WebbDBType == WebbDBTypes.WebbPlaybook || (c as LabelControl).IsTitle)    //09-01-2011 Scott
                        {
                            (c as LabelControl).LabelControlView.Text = strReportHeader;

                            return;
                        }
                    }
                }
            }

            // page header
            band = webbReport.Bands[DevExpress.XtraReports.UI.BandKind.PageHeader];

            if (band == null) return;

            foreach (DevExpress.XtraReports.UI.XRControl xrControl in band.Controls)
            {
                if (!(xrControl is DevExpress.XtraReports.UI.WinControlContainer)) continue;

                Control c = (xrControl as DevExpress.XtraReports.UI.WinControlContainer).WinControl;

                if (c is LabelControl && (config.WebbDBType == WebbDBTypes.WebbPlaybook || (c as LabelControl).IsTitle))    //09-01-2011 Scott
                {
                    (c as LabelControl).LabelControlView.Text = strReportHeader;

                    return;
                }
            }
        }			



		private static bool PrinterExist(string printerName)
		{
			foreach(string strPrint in PrinterSettings.InstalledPrinters)
			{
				if(printerName==strPrint)return true;
			}

			return false;
		}
	}
}
