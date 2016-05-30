using System;
using System.IO;
using System.Text;
using System.Collections.Specialized;
using System.Windows.Forms;
using System.Data;
using System.Xml;

using Webb.Reports.DataProvider;
using Webb.Data;
using Webb.Reports.Browser;
using Webb.Reports;

namespace Webb.Reports
{	
	#region Read/Write Inw File
	//Added this code at 2009-1-23 13:02:58@Simon
	public class InwManager
	{
		public static void WriteInwFile(string filename,string path)
		{
			try 
			{				
				string InwFile=filename.Replace(".repx",".inw");	
			
				if(filename.EndsWith(".repw"))
				{
					InwFile=filename.Replace(".repw",".inw");	
				}

				Webb.Reports.DataProvider.WebbDataProvider PublicDataProvider=Webb.Reports.DataProvider.VideoPlayBackManager.PublicDBProvider;					

				string[] configs=PublicDataProvider.DBSourceConfig.CreateConfigStrings(path);

				StreamWriter sw=new StreamWriter(InwFile,false);

				foreach(string config in configs)
				{
					sw.WriteLine(config);
				}
				sw.Flush();

				sw.Close();
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);				
			}
		}
		public static void WriteInwFile(string filename)
		{
			try 
			{				
				string InwFile=filename.Replace(".repx",".inw");	
			
				if(filename.EndsWith(".repw"))
				{
					InwFile=filename.Replace(".repw",".inw");	
				}

				Webb.Reports.DataProvider.WebbDataProvider PublicDataProvider=Webb.Reports.DataProvider.VideoPlayBackManager.PublicDBProvider;					

				string[] configs=PublicDataProvider.DBSourceConfig.CreateConfigStrings(filename);

				StreamWriter sw=new StreamWriter(InwFile,false);

				foreach(string config in configs)
				{
					sw.WriteLine(config);
				}
				sw.Flush();

				sw.Close();
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);				
			}
		}
		public static string[] ReadInwFile(string filename)
		{
			if(!File.Exists(filename)||!filename.EndsWith(".inw"))return null;

			try 
			{
				StreamReader sr=new StreamReader(filename,Encoding.Default);

				StringCollection strcollect=new StringCollection();

				while(sr.Peek()>=0)
				{
					string sline=sr.ReadLine();

					sline=sline.Trim(" \r\n".ToCharArray());

					if(sline!=string.Empty)
					{
						strcollect.Add(sline);
					}
				}

				sr.Close();

				if(strcollect.Count!=10)
				{
					MessageBox.Show(string.Format("Error {0} Rows in File",strcollect.Count));

					return null;
				}
				
				string [] strConfigs=new string[strcollect.Count];

				for(int i=0;i<strcollect.Count;i++)
				{
					strConfigs[i]=strcollect[i];
				}

				return strConfigs;
				
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			return null;		

		
		}
		

		public static WebbDataSource CreateDataSourse(string[] args,bool throwException)
		{
			if(args==null||args.Length!=10)return null;

			if(args[3]=="DBConn:"&&args[8]==@"Files:")return null;

			try
			{
				CommandManager m_CmdManager = new CommandManager(args);			

				DBSourceConfig m_Config = m_CmdManager.CreateDBConfig();		

				WebbDataProvider m_DBProvider = new WebbDataProvider(m_Config);
			
				WebbDataSource m_DBSource = new WebbDataSource();

				m_DBProvider.GetDataSource(m_Config, m_DBSource);

				Webb.Reports.DataProvider.VideoPlayBackManager.PublicDBProvider=m_DBProvider;

				return m_DBSource;
			}
			catch(Exception ex)
			{
				if(throwException)MessageBox.Show("Bad parameters in Config File!\r\n"+ex.Message);

				return null;
			}
           
		}

	
	}
	#endregion

	#region Read/Write XML 
	public class ConfigFileManager
	{
        public static string DataConfigFile = Webb.Utility.ApplicationDirectory + "DataConfig.xml";

	   	public static DataTable AddColumn(DataTable dt)
		{
			if(dt==null) dt=new DataTable("ConfigTable");

			dt.Columns.Clear();

			dt.Columns.Add("Path",typeof(string));
			dt.Columns.Add("Action",typeof(string));
			dt.Columns.Add("Product",typeof(string));
			dt.Columns.Add("DBConn",typeof(string));
			dt.Columns.Add("SQLCmd",typeof(string));
			dt.Columns.Add("GameIDs",typeof(string));
			dt.Columns.Add("EdlIDs",typeof(string));
			dt.Columns.Add("FilterIDs",typeof(string));
			dt.Columns.Add("Files",typeof(string));
			dt.Columns.Add("Print",typeof(string));

			DataColumn[] dcKeys = new DataColumn[1];
			dcKeys[0] = dt.Columns["Path"];			
			dt.PrimaryKey = dcKeys;

			return dt;

		}
		

		#region Single-Record Mode		
		   public static void WriteDataConfig(string filename)
			{
				Webb.Reports.DataProvider.WebbDataProvider PublicDataProvider=Webb.Reports.DataProvider.VideoPlayBackManager.PublicDBProvider;					

				string[] configs=PublicDataProvider.DBSourceConfig.CreateConfigStrings(filename);

				DataSet ds=new DataSet("ConfigDataSet");

				DataTable dt=AddColumn(null);
	            
				DataRow dr=dt.NewRow();

				for(int i=0;i<dt.Columns.Count;i++)
				{
					dr[i]=configs[i];
				}

				dt.Rows.Add(dr);

				ds.Tables.Add(dt);

				try 
				{		
					ds.WriteXml(DataConfigFile);

					ds.Dispose();

				}		   
				catch(Exception ex)
				{ 
					ds.Dispose();
					MessageBox.Show(ex.Message);					
				}

			}

	    	public static string[] ReadDataConfig(string filename)
			{
				if(!File.Exists(DataConfigFile))return null;	
			
				DataSet ds=new DataSet();	
				try 
				{  
					ds.ReadXml(DataConfigFile);

					DataTable dt=ds.Tables[0];				

					filename="Path:"+filename;
	                    
					DataRow dr=dt.Rows[dt.Rows.Count-1];

					if(dr==null||dt.Columns.Count!=10)return null;

					string [] strConfigs=new string[dt.Columns.Count];

					strConfigs[0]=filename;

					for(int i=1;i<dt.Columns.Count;i++)
					{
						strConfigs[i]=dr[i].ToString();
					}	

					ds.Dispose();

					return strConfigs;
					
				}
				catch(Exception ex)
				{
					ds.Dispose();

                    MessageBox.Show(ex.Message);
					
					return null;
				}	
			}
			

     
         public static string[] ReadDataConfig(WebbDBTypes webbType,string sourceFile)
          {
            if(!File.Exists(sourceFile))return null;	
			
            DataSet ds=new DataSet();
	
            try 
            {  
                ds.ReadXml(sourceFile);

                DataTable dt=ds.Tables[0];	

                DataRow[] dataRows=dt.Select("Product='Product:"+webbType.ToString()+"'");

                if(dataRows.Length==0)return null;
	                    
                 DataRow dr=dataRows[0];

                if(dr==null||dt.Columns.Count!=10)return null;

                string [] strConfigs=new string[dt.Columns.Count];

                strConfigs[0]="Path:"+string.Empty;   

                for(int i=1;i<dt.Columns.Count;i++)
                {
                    strConfigs[i]=dr[i].ToString();
                }	

                ds.Dispose();

                return strConfigs;
					
            }
            catch(Exception ex)
            {
                ds.Dispose();

                MessageBox.Show(ex.Message);
					
                return null;
            }	
        }

         public static void WriteDataConfig(WebbDBTypes webbType, string TargetPath)
         {
             Webb.Reports.DataProvider.WebbDataProvider PublicDataProvider = Webb.Reports.DataProvider.VideoPlayBackManager.PublicDBProvider;

             string[] configs = PublicDataProvider.DBSourceConfig.CreateConfigStrings(string.Empty);

             DataSet ds=new DataSet("ConfigDataSet");
             
             DataTable configTable;

             DataRow productRow;

             bool isNewRow = true;

             #region Get the row which contains the product

             if (File.Exists(TargetPath))
             {
                 try
                 {
                     ds.ReadXml(TargetPath);

                     configTable = ds.Tables[0];

                     DataRow[] dataRows = configTable.Select("Product='Product:" + webbType.ToString() + "'");

                     if (dataRows.Length == 0)
                     {
                         productRow = configTable.NewRow();

                         isNewRow = true;
                     }
                     else
                     {
                         productRow = dataRows[0];

                         isNewRow = false;
                     }            
                 }
                 catch
                 {
                     ds = new DataSet();

                     configTable = AddColumn(null);

                     ds.Tables.Add(configTable);

                     productRow = configTable.NewRow();

                     isNewRow = true;
                 }                   
             }
             else
             {
                 configTable = AddColumn(null);

                ds.Tables.Add(configTable);

                productRow = configTable.NewRow();                 

                isNewRow = true;                 
             }

             #endregion

             try
             {

                 for (int i = 0; i < configTable.Columns.Count; i++)
                 {
                     productRow[i] = configs[i];
                 }

                 if (isNewRow) configTable.Rows.Add(productRow);

                 ds.AcceptChanges();

                 ds.WriteXml(TargetPath);

                 ds.Dispose();

             }
             catch (Exception ex)
             {
                 ds.Dispose();

                 MessageBox.Show(ex.Message);
             }

         }
        
			
		#endregion

   
	}
	#endregion

	#region Read/Write WRDb File 
	public class WrdfFileManager
	{
		#region Mode 
		public static void WriteDataConfig(string DataConfigFile,string filename)
		{
			Webb.Reports.DataProvider.WebbDataProvider PublicDataProvider=Webb.Reports.DataProvider.VideoPlayBackManager.PublicDBProvider;					

			string[] configs=PublicDataProvider.DBSourceConfig.CreateConfigStrings(filename);

			DataSet ds=VideoPlayBackManager.DataSource.Copy();   

			DataTable dt=ConfigFileManager.AddColumn(null);
	            
			DataRow dr=dt.NewRow();

			for(int i=0;i<dt.Columns.Count;i++)
			{
				dr[i]=configs[i];
			}

			dt.Rows.Add(dr);

			dt.TableName="ConfigTable";
     
			ds.Tables.Add(dt);	
		
			try 
			{
				ds.WriteXml(DataConfigFile,XmlWriteMode.WriteSchema);

				ds.Dispose();

			}		   
			catch(Exception ex)
			{ 
				ds.Dispose();
				MessageBox.Show(ex.Message);					
			}

		}

		public static WebbDataSource ReadDataConfig(string DataConfigFile,string filename)
		{
			if(!File.Exists(DataConfigFile))return null;	

			WebbDataSource m_DBSource = new WebbDataSource();
			
			DataSet ds=new DataSet();	
			try 
			{  
				ds.ReadXml(DataConfigFile);

				if(ds.Tables.Count==0)return null;

				int lastCount=ds.Tables.Count-1;

				if(lastCount==0)
				{					
					m_DBSource.DataSource=ds;						
				}
                else if (ds.Tables[lastCount].TableName == "[TableStructure]")
                {
                    DBSourceConfig m_Config = new DBSourceConfig();

                    m_Config.WebbDBType = WebbDBTypes.CoachCRM;

                    m_Config.DBFilePath = DataConfigFile;

                    Webb.Reports.DataProvider.VideoPlayBackManager.PublicDBProvider = new WebbDataProvider(m_Config);

                    m_DBSource.DataSource = ds;

                }
                else
                {
                    DataTable dt = ds.Tables[lastCount];

                    filename = "Path:" + filename;

                    DataRow dr = dt.Rows[dt.Rows.Count - 1];

                    if (dr == null || dt.Columns.Count != 10) return null;

                    string[] strConfigs = new string[dt.Columns.Count];

                    strConfigs[0] = "Path:" + filename;

                    for (int i = 1; i < dt.Columns.Count; i++)
                    {
                        strConfigs[i] = dr[i].ToString();
                    }

                    CommandManager m_CmdManager = new CommandManager(strConfigs);

                    DBSourceConfig m_Config = m_CmdManager.CreateDBConfig();

                    WebbDataProvider m_DBProvider = new WebbDataProvider(m_Config);

                    Webb.Reports.DataProvider.VideoPlayBackManager.PublicDBProvider = m_DBProvider;

                    ds.Tables.RemoveAt(lastCount);

                    AdvGameData.FillGamesAndFilters(m_DBSource, m_Config);

                    m_DBSource.DataSource = ds;
                }				

				return m_DBSource;
					
			}
			catch(Exception ex)
			{
				ds.Dispose();

				MessageBox.Show(ex.Message);
					
				return null;
			}	
		}			
		public static void ReadDataConfig(string DataConfigFile,out DataSet configDataSet,out string[] args)
		{

			if(!File.Exists(DataConfigFile))	
			{
				 args=null;

				 configDataSet=null;	

				return;
			}
			
			DataSet ds=new DataSet();	

			try 
			{  
				ds.ReadXml(DataConfigFile);

				int lastCount=ds.Tables.Count-1;

				DataTable dt=ds.Tables[lastCount];	
	                    
				DataRow dr=dt.Rows[dt.Rows.Count-1];		

				string [] strConfigs=new string[dt.Columns.Count];

				for(int i=0;i<dt.Columns.Count;i++)
				{
					strConfigs[i]=dr[i].ToString();
				}	
				
				ds.Tables.RemoveAt(lastCount);
				
				args=strConfigs;

				configDataSet=ds;			
				
					
			}
			catch(Exception ex)
			{
				ds.Dispose();

				MessageBox.Show(ex.Message);

				args=null;

				configDataSet=null;				
				
			}	
		}			
		#endregion		
	}
	#endregion
	
}
