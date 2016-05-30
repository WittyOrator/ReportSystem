using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using Webb.Reports;

namespace Webb.Reports.Designer
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
#if Release
			try
			{
				Application.Run(new WebbReports());
			}
			catch(Exception ex)
			{
				Webb.Utilities.TextLog.WriteLine(string.Format("WebbReport occur an error and application will be closed. Message:{0}",ex.Message));
				Webb.Utilities.TextLog.WriteLine(ex.StackTrace,false);
				throw ex;
			}
#else					
//			try
//			{
				Webb.Utilities.WaitingForm.ShowWaitingForm();

				WebbReports m_RepDesigner = new WebbReports();

                Webb.Reports.ExControls.Data.ColumnManager.Init();  //08-20-2010 @simon

				if(args.Length>0)
				{
					string m_Path = args[0];

					if(System.IO.File.Exists(m_Path)&&(m_Path.EndsWith(".repx")||m_Path.EndsWith(".repw")))
					{
						m_RepDesigner.OpenReport(m_Path,true);

                        DataProvider.VideoPlayBackManager.AdvSectionType = AdvScoutType.None;

                        //Webb.Utilities.WaitingForm.SetWaitingMessage("Loading Data Source......");

                        //args = ConfigFileManager.ReadDataConfig(m_Path);

                        //WebbDataSource dataSourse=InwManager.CreateDataSourse(args,false);
                       
                        //if(dataSourse!=null)
                        //{							
                        //    bool Load=m_RepDesigner.LoadDataSource(dataSourse);

                        //    if(!Load&&System.IO.File.Exists(ConfigFileManager.DataConfigFile))
                        //    {
                        //        System.IO.File.Delete(ConfigFileManager.DataConfigFile);
                        //    }						

                        //}
					}
				}

			   	DataProvider.VideoPlayBackManager.AdvSectionType=AdvScoutType.None;

				Webb.Utilities.WaitingForm.CloseWaitingForm();

				Application.Run(m_RepDesigner);				
//			}
//			catch(Exception ex)
//			{
//				Webb.Utilities.WaitingForm.CloseWaitingForm();
//				Webb.Utilities.TextLog.WriteLine(string.Format("WebbReport occur an error and application will be closed. Message:{0}",ex.Message));
//				Webb.Utilities.TextLog.WriteLine(ex.StackTrace,false);
//				throw ex;
//			}
#endif
		}
		
	}
}
