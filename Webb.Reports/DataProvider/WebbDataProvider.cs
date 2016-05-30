/***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:WebbDataProvider.cs
 * Author:Country.Wu [EMail:Webb.Country.Wu@163.com]
 * Create Time:11/7/2007 03:38:43 PM
 * Copyright:1986-2007@Webb Electronics all right reserved.
 * Purpose:
 * ***********************************************************************/
#region History
/*
 * //Author@DateTime : Description
 * */
#endregion History

#define DEMO //12-21-2007@Scott

#undef DEBUG  //2009-6-18 9:17:05@Simon Add this Code

using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Windows.Forms;
using System.Text;

using Webb.Data;
using Webb.Collections;
using Webb.Reports.MoviePlayer;

using System.Collections.Specialized;

namespace Webb.Reports.DataProvider
{
	#region public class WebbReportDataAdapter : DbDataAdapter,IDisposable,IDbDataAdapter
	/// <summary>
	/// Summary description for WebbReportDataAdapter.
	/// </summary>
	public class WebbReportDataAdapter :DbDataAdapter, IDisposable,IDbDataAdapter
	{
		protected OleDbDataAdapter _OleAdapter;
		protected SqlDataAdapter _SqlAdatapter;
		protected DbDataAdapter _CurrentAdapter;
		protected IDbConnection _Connection;
		protected string _SelectCommandStr;
		protected DBSourceConfig _DBConfig;
		protected IDbCommand _UpdateCommand;
		protected IDbCommand _SelectCommand;
		protected IDbCommand _InsertCommand;
		protected IDbCommand _DeleteCommand;
		//
		public DBSourceConfig DBConfig
		{
			get{return this._DBConfig;}
			set{this._DBConfig = value;}
		}

		public WebbReportDataAdapter(DBSourceConfig i_DataSource, IDbConnection i_Conection, string i_SQLCommand)
		{
			this._DBConfig = i_DataSource;
			this._Connection = i_Conection;
			this._SelectCommandStr = i_SQLCommand;
			this.InitlizeDataAdapter();
		}

		private void InitlizeDataAdapter()
		{
			System.Diagnostics.Trace.Assert(this._DBConfig!=null);

			if(this._DBConfig.DBConnType != DBConnTypes.XMLFile)
			{
				System.Diagnostics.Trace.Assert(this._Connection!=null);
			}

			switch(this._DBConfig.DBConnType)
			{
				case DBConnTypes.OleDB:
					this.InitlizeAccess();
					break;

				case DBConnTypes.SQLDB:
					this.InitlizeSQL();
					break;

				case DBConnTypes.XMLFile:
					this.InitlizeXMLFile();
					break;

				default:
					break;
			}
		}

		private void InitlizeAccess()
		{
			this._OleAdapter = new OleDbDataAdapter(this._SelectCommandStr,this._Connection as OleDbConnection);			
			this._CurrentAdapter = this._OleAdapter as DbDataAdapter;
		}

		private void InitlizeSQL()
		{
			this._SqlAdatapter = new SqlDataAdapter(this._SelectCommandStr,this._Connection as SqlConnection);
			this._CurrentAdapter = this._SqlAdatapter as DbDataAdapter;
		}

		private void InitlizeXMLFile()
		{

		}

		#region override medthods
		[Obsolete]
		protected override DataAdapter CloneInternals()
		{
			return base.CloneInternals ();
		}

		public override System.Runtime.Remoting.ObjRef CreateObjRef(Type requestedType)
		{
			//return base.CreateObjRef (requestedType);
			return this._CurrentAdapter.CreateObjRef(requestedType);
		}

		protected override RowUpdatedEventArgs CreateRowUpdatedEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			//return null;			
			throw new Exception(string.Format("The function \"{0}\" has not implemented.","CreateRowUpdatedEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)"));
		}

		protected override RowUpdatingEventArgs CreateRowUpdatingEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			//return null;
			throw new Exception(string.Format("The function \"{0}\" has not implemented.","CreateRowUpdatingEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)"));
		}

		protected override DataTableMappingCollection CreateTableMappings()
		{
			//return base.CreateTableMappings ();
			throw new Exception(string.Format("The function \"{0}\" has not implemented.","CreateTableMappings()"));
		}

		protected override void Dispose(bool disposing)
		{
			//if(this._CurrentAdapter!=null) this._CurrentAdapter.Dispose();
			base.Dispose (disposing);
		}

		public override bool Equals(object obj)
		{
			return base.Equals (obj);
		}

		//Scott@2007-11-16 15:05 modified some of the following code.
		private int FillFromXml(DataSet i_DataSet)
		{
			int m_value = 0;
			if(!System.IO.File.Exists(this._DBConfig.DBFilePath)) 
				return m_value;
			try
			{
				i_DataSet.ReadXml(this._DBConfig.DBFilePath);
			}
			catch
			{
				return m_value;
			}
			if(i_DataSet.Tables.Count>0)
			{
				m_value = i_DataSet.Tables[0].Rows.Count;
			}
			return m_value;
		}

		public override int Fill(DataSet dataSet)
		{
			//Scott@2007-11-16 15:13 modified some of the following code.
			int m_value = 0;
			if(this._DBConfig == null || dataSet == null)
				return m_value;
			switch(this._DBConfig.DBConnType)
			{
				case Webb.Data.DBConnTypes.OleDB:
				case Webb.Data.DBConnTypes.SQLDB:
					m_value = this._CurrentAdapter.Fill(dataSet);
					break;
				case Webb.Data.DBConnTypes.XMLFile:
					m_value = FillFromXml(dataSet);
					break;
				default:
					break;
			}
			//Wu.Country@2007-10-10 11:29 modified some of the following code.
			if(dataSet.Tables.Count>0)
			{
				DBWebbReportCommon m_CommSet = new DBWebbReportCommon();
				this.ConvertDataTableScheme(dataSet.Tables[0],m_CommSet.GameDetails);
			}
			return m_value;
		}

		protected override int Fill(DataSet dataSet, int startRecord, int maxRecords, string srcTable, IDbCommand command, CommandBehavior behavior)
		{
			throw new Exception(string.Format("The function \"{0}\" has not implemented.","Fill(DataSet dataSet, int startRecord, int maxRecords, string srcTable, IDbCommand command, CommandBehavior behavior)"));
			//return this._CurrentAdapter.Fill (dataSet, startRecord, maxRecords, srcTable, command, behavior);
		}

		new public int Fill(DataSet dataSet, int startRecord, int maxRecords, string srcTable)
		{
			return this._CurrentAdapter.Fill(dataSet,startRecord,maxRecords,srcTable);
		}

		new public int Fill(DataSet dataSet,string srcTable)
		{
			return this._CurrentAdapter.Fill(dataSet,srcTable);
		}

		new public int Fill(DataTable i_DataTable)
		{
			return this._CurrentAdapter.Fill(i_DataTable);
		}

		protected override int Fill(DataSet dataSet, string srcTable, IDataReader dataReader, int startRecord, int maxRecords)
		{
			throw new Exception(string.Format("The function \"{0}\" has not implemented.","Fill(DataSet dataSet, string srcTable, IDataReader dataReader, int startRecord, int maxRecords)"));
			//return this._CurrentAdapter.Fill (dataSet, srcTable, dataReader, startRecord, maxRecords);
		}

		protected override int Fill(DataTable dataTable, IDataReader dataReader)
		{
			throw new Exception(string.Format("The function \"{0}\" has not implemented.","Fill(DataTable dataTable, IDataReader dataReader)"));
			//return this._CurrentAdapter.Fill (dataTable, dataReader);
		}

		protected override int Fill(DataTable dataTable, IDbCommand command, CommandBehavior behavior)
		{
			throw new Exception(string.Format("The function \"{0}\" has not implemented.","Fill(DataTable dataTable, IDbCommand command, CommandBehavior behavior)"));
			//return this._CurrentAdapter.Fill (dataTable, command, behavior);
		}

		public override DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType)
		{			
			return this._CurrentAdapter.FillSchema(dataSet,schemaType);
		}

		protected override DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType, IDbCommand command, string srcTable, CommandBehavior behavior)
		{
			throw new Exception(string.Format("The function \"{0}\" has not implemented.","FillSchema(DataSet dataSet, SchemaType schemaType, IDbCommand command, string srcTable, CommandBehavior behavior)"));
			//return this._CurrentAdapter.FillSchema (dataSet, schemaType, command, srcTable, behavior);
		}

		protected override DataTable FillSchema(DataTable dataTable, SchemaType schemaType, IDbCommand command, CommandBehavior behavior)
		{
			throw new Exception(string.Format("The function \"{0}\" has not implemented.","FillSchema(DataTable dataTable, SchemaType schemaType, IDbCommand command, CommandBehavior behavior)"));
			//return this._CurrentAdapter.FillSchema (dataTable, schemaType, command, behavior);
		}

		public override IDataParameter[] GetFillParameters()
		{			
			return this._CurrentAdapter.GetFillParameters();
		}

		public override int GetHashCode()
		{
			return base.GetHashCode ();
		}

		protected override object GetService(Type service)
		{
			return base.GetService (service);
		}

		public override object InitializeLifetimeService()
		{
			return base.InitializeLifetimeService ();
		}

		protected override void OnFillError(FillErrorEventArgs value)
		{
			base.OnFillError (value);
		}

		protected override void OnRowUpdated(RowUpdatedEventArgs value)
		{
			throw new Exception(string.Format("The function \"{0}\" has not implemented.","OnRowUpdated(RowUpdatedEventArgs value)"));
		}

		protected override void OnRowUpdating(RowUpdatingEventArgs value)
		{
			throw new Exception(string.Format("The function \"{0}\" has not implemented.","OnRowUpdating(RowUpdatingEventArgs value)"));
		}

		protected override bool ShouldSerializeTableMappings()
		{
			return base.ShouldSerializeTableMappings ();
		}

		public override System.ComponentModel.ISite Site
		{
			get
			{
				return base.Site;
			}
			set
			{
				base.Site = value;
			}
		}

		public override string ToString()
		{
			return base.ToString ();
		}

		protected override int Update(DataRow[] dataRows, DataTableMapping tableMapping)
		{
			return base.Update (dataRows, tableMapping);
		}

		public override int Update(DataSet dataSet)
		{
			throw new Exception(string.Format("The function \"{0}\" has not implemented.","Update(DataSet dataSet)"));
			//return 0;
		}

		#endregion End overrdie methods
		
		private void ConvertDataTableScheme(DataTable i_RawTable,DataTable i_TableSchema)
		{
			foreach(DataColumn m_Col in i_TableSchema.Columns)
			{
				if(!i_RawTable.Columns.Contains(m_Col.ColumnName))
				{
					//Add new column
					foreach(string m_FiledName in WebbDataProvider.BuildInNumericFields)
					{
						if(m_FiledName==m_Col.ColumnName)
						{
							DataColumn m_NewCol = new DataColumn(m_Col.ColumnName,m_Col.DataType);
							i_RawTable.Columns.Add(m_NewCol);
						}
					}
				}
				else
				{
					//Convert data type
					DataColumn m_ExistedCol = i_RawTable.Columns[m_Col.ColumnName];	
					if(m_ExistedCol.DataType!=m_Col.DataType)
					{
						
						DataColumn m_NewCol = new DataColumn(m_Col.ColumnName+"$Temp001$",m_Col.DataType);
						i_RawTable.Columns.Add(m_NewCol);
						//Copy Data
						foreach(DataRow m_Row in i_RawTable.Rows)
						{
							try
							{
								//Wu.Country@2007-10-12 16:29 modified some of the following code.
								m_Row[m_NewCol.ColumnName] = m_Row[m_Col.ColumnName];
							}
							catch(Exception ex)
							{
								System.Diagnostics.Debug.WriteLine("Copy data error in WebbReportDataAdapter.ConvertDataTableScheme(); Message:{0}",ex.Message);
							}
						}
						i_RawTable.Columns.Remove(m_ExistedCol);
						m_NewCol.ColumnName = m_Col.ColumnName;
					}
				}
			}
		}

		#region IDbDataAdapter Members

		public new IDbCommand UpdateCommand
		{
			get{return this._UpdateCommand;}
			set{this._UpdateCommand = value;}
		}

		public new IDbCommand SelectCommand
		{
			get{return this._DeleteCommand;}
			set{this._SelectCommand = value;}
		}

		public new IDbCommand DeleteCommand
		{
			get{return this._DeleteCommand;}
			set{this._DeleteCommand = value;}
		}

		public new IDbCommand InsertCommand
		{
			get{return this._InsertCommand;}
			set{this._InsertCommand = value;}
		}
		#endregion		
	}
	#endregion

	#region public class WebbDataProvider
	/*Descrition:   */
	public class WebbDataProvider : IDisposable
	{
		//Default numeric fields
		static internal readonly ReadonlyStringCollection BuildInNumericFields = new ReadonlyStringCollection(new string[]{"Distance","Gain","Yard","Gains"});
		//Const command
		static internal readonly string CMD_WebbVictory_GetGames = "select * from [Game] where Deleted = 0";
        static internal readonly string CMD_WebbVictory_GetEdlDetails = "select distinct [EdlID],[GameID] from [EdlDetail]";
		static internal readonly string CMD_WebbVictory_GetFilters = "select * from [AutoCutupTable]";
		static internal readonly string CMD_WebbAdvantage_GetGames = "select * from [Games]";
		static internal readonly string CMD_Access_GetTables = "select [Name] as [TABLE_NAME],[Flags],[Type] from [MSysObjects] where [Flags]=0 and [Type]=1";
		static internal readonly string CMD_SQL_GetTables = "select [Name] as [TABLE_NAME] from sysobjects where type='U' and objectProperty(id, 'IsMSShipped') = 0";
		static internal readonly string CMD_SQL2005_GetTables = "select t.name from sys.objects t join sys.schemas p on p.schema_id = t.schema_id where t.type ='u'  and objectProperty(t.object_id, 'IsMSShipped') = 0";
		static internal readonly string CMD_SelectGames_Access = "select * from [GameDetail] where [GameID] in ({0})";
		static internal readonly string CMD_SelectGames_SQL = "select * from [GameDetails] where [GameID] in ({0})";
		static internal readonly string CMD_SelectVideoInfo_Victory = "SELECT GameDetail.PlayID, GameDetail.GameID, GameDetail.[Start Time], GameDetail.[End Time], VidoAngleData.Angle, VidoAngleData.VideoFilePath FROM GameDetail INNER JOIN VidoAngleData ON GameDetail.GameID = VidoAngleData.GameID WHERE GameDetail.PlayID IN ({0})";
		static internal readonly string CMD_SelectGames_Access_New_Victory = "select * from [GameDetail] INNER JOIN [SubPlay] ON SubPlay.PlayID = GameDetail.PlayID where [GameID] in ({0})";	//09-09-2008@Scott
		static internal readonly string CMD_SelectGamesEdls_Access_New_Victory = 
		"(SELECT a.*,b.* FROM (SELECT m.PlayID AS pid,m.*,n.* FROM EdlDetail AS m INNER JOIN GameDetail AS n ON m.PlayID = n.PlayID) AS a LEFT JOIN SubPlay AS b ON a.pid = b.PlayID WHERE a.PlayType = 1 AND a.EdlID IN ({1})"
		+ " UNION"
		+ " SELECT a.*, b.* FROM (SELECT m.PlayID AS pid,m.*,n.* FROM EdlDetail AS m LEFT JOIN (SELECT * FROM GameDetail WHERE FALSE) AS n ON m.PlayID = n.PlayID) AS a, SubPlay AS b WHERE a.pid = b.SubPlayID AND a.PlayType = 2 AND a.EdlID IN ({1}))"
		+ " UNION"
		+ " (SELECT a.*, b.* FROM (SELECT n.PlayID AS pid,m.*,n.* FROM (SELECT * FROM EdlDetail WHERE FALSE) AS m RIGHT JOIN GameDetail AS n ON m.PlayID = n.PlayID WHERE n.GameID IN ({0})) AS a LEFT JOIN SubPlay AS b ON a.pid = b.PlayID)";
		//12-21-2007@Scott
		static internal readonly string CMD_SelectVideoInfo_Victory_Ex = "SELECT GameDetail.PlayID, GameDetail.GameID, GameDetail.[Start Time], GameDetail.[End Time], VidoAngleData.Angle, VidoAngleData.VideoFilePath FROM GameDetail INNER JOIN VidoAngleData ON GameDetail.GameID = VidoAngleData.GameID WHERE GameDetail.PlayID IN ({0}) AND VidoAngleData.VideoFilePath <> ''";
		private DBSourceConfig _DBSourceConfig;
		private IDBManager _DBManager;
		
		private UI.DataSourcePreviewForm _PreviewForm;	//Preview Form
		private UI.DBConfigWizardForm _ConfigForm;		//Wizard Form
		
		private UI.GameSelectorForm _GameSelector;
		private UI.FilterSelectorForm _FilterSelector;
		private UI.SectionFilterSelectorForm _SectionFilterSelector;

		private DataTable _GameTable;
		private DataTable _FilterTable;
      

		//Properties
		public DataTable GameTable
		{
			get{return this._GameTable;}
			set{this._GameTable = value;}
		}

		public DataTable FilterTable
		{
			get{return this._FilterTable;}
			set{this._FilterTable = value;}
		}
       

		public DBSourceConfig DBSourceConfig
		{
			get{return this._DBSourceConfig;}			
		}

		//ctor
		public WebbDataProvider()
		{
			this._DBSourceConfig = new DBSourceConfig();
		}

		//ctor
		public WebbDataProvider(DBSourceConfig i_Config)
		{
			System.Diagnostics.Trace.Assert(i_Config!=null);

			this._DBSourceConfig = i_Config;
		}

		//Init dbmanager by dbconfig
		public void InitializeDBManager(DBSourceConfig i_Config)
		{ 
			this._DBSourceConfig.ApplyConfig(i_Config);	//copy dbconfig

			this._DBSourceConfig.CreateConnString();

			if(this._DBManager!=null)
			{
				this._DBManager.Dispose();

				this._DBManager = null;
			}
			this._DBManager = Webb.Data.DBHelper.NewDBManager(this._DBSourceConfig.DBConnType,this._DBSourceConfig.ConnString);
		}

        public bool IsCCRMDataType()
        {
            if (this.DBSourceConfig == null) return false;

            return this.DBSourceConfig.WebbDBType == WebbDBTypes.CoachCRM;
        }

        public bool IsCCRMTimeData(string strField)
        {
            //if (!IsCCRMDataType()) return false;

            return strField.EndsWith("(@Time)");
        }

        public bool IsFeetInchesData(string strField)
        {
            //if (!IsCCRMDataType()) return false;

            return strField.EndsWith("(@Feet Inches)");
        }

        public  void UpdateEFFDataSource(WebbDataSource i_DataSource)
        {
            if (this._DBSourceConfig.DBConnType == Webb.Data.DBConnTypes.File
                   && i_DataSource != null && i_DataSource.Games.Count>0 
                   && i_DataSource.DataSource != null && i_DataSource.DataSource.Tables.Count>0)
            {
                DataTable dt = i_DataSource.DataSource.Tables[0];

                if(dt.Columns.Contains("EFF"))
                {
                    AdvFilterConvertor filterConvertor = new AdvFilterConvertor();

                    ScFilterList filterList = Webb.Reports.DataProvider.VideoPlayBackManager.AdvReportFilters;

                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dt.Columns.Contains("Down") && dt.Columns.Contains("Gain") && dt.Columns.Contains("Distance"))
                        {
                            #region Calculate Filter
                            DBFilter filter ;

                           string scoutType=string.Empty;
                            
                            if(dt.Columns.Contains("Scout Type")&&dr["Scout Type"]!=null&&!(dr["Scout Type"] is DBNull))
                            {
                                scoutType = dr["Scout Type"].ToString().ToLower();
                            }
                            else if (dt.Columns.Contains("ScoutType") && dr["ScoutType"] != null && !(dr["ScoutType"] is DBNull))
                            {
                                scoutType = dr["Scout Type"].ToString().ToLower();
                            }
                            else if( i_DataSource.Games.Count>0)
                            {
                                scoutType = i_DataSource.Games[0].ScoutType.ToLower();
                            }

                           scoutType = scoutType.Trim();

                           if (scoutType.StartsWith("defen")|| scoutType.StartsWith("opponent offen")) 
                           {
                                filter = filterConvertor.GetDefenseEffFilter(filterList);

                                if (filter.Count > 0)
                                {
                                    if (filter.CheckResultWithBracket(dr))
                                    {
                                        dr["EFF"] = "YES";
                                    }
                                    else
                                    {
                                        dr["EFF"] = "NO";
                                    }
                                }
                                
                            }
                           else if (scoutType.StartsWith("offen") || scoutType.StartsWith("opponent defen"))
                           {
                               filter = filterConvertor.GetOffenseEffFilter(filterList);

                               if (filter.Count > 0)
                               {
                                   if (filter.CheckResultWithBracket(dr))
                                   {
                                       dr["EFF"] = "YES";
                                   }
                                   else
                                   {
                                       dr["EFF"] = "NO";
                                   }
                               }
                           }                         
                            #endregion
                        }
                        else
                        {
                            //dr["EFF"] = string.Empty;
                        }
                    }
                }

            }
        }

		private void ClearDataSource(DataSet ds)
		{
			ds.Relations.Clear();

			foreach(DataTable dt in ds.Tables)
			{
				dt.Constraints.Clear();
			}

			ds.Tables.Clear();
		}
 
        //public void CalcSQLTable(Webb.Reports.WebbDataSource i_DBSource,string m_SQL)    //Added this code at 2009-2-2 16:06:10@Simon
        //{	
        //    if(m_SQL!="")
        //    { 	
        //        i_DBSource.DataSource.Tables.Clear();

        //        SqlConnection connection = new SqlConnection(this._DBSourceConfig.ConnString);	

        //        connection.Open();

        //        DataAdapter adapter=new SqlDataAdapter(m_SQL,connection);
                  
        //        adapter.Fill(i_DBSource.DataSource);

        //        i_DBSource.DataAdapter=adapter;

        //        connection.Close();

        //    }
        //}
		//Get data source for report
		public bool GetDataSource(Webb.Reports.WebbDataSource i_DBSource)
		{
			if(i_DBSource == null) i_DBSource = new WebbDataSource();

			this.ClearDataSource(i_DBSource.DataSource);	//clear dataset

			if(this._DBSourceConfig.DBConnType == Webb.Data.DBConnTypes.OleDB
				|| this._DBSourceConfig.DBConnType == Webb.Data.DBConnTypes.SQLDB)
			{
				if(this._DBSourceConfig.DBConnType == Webb.Data.DBConnTypes.OleDB && 
					(this._DBSourceConfig.WebbDBType == WebbDBTypes.WebbVictoryBasketball
					||this._DBSourceConfig.WebbDBType == WebbDBTypes.WebbVictoryHockey
					||this._DBSourceConfig.WebbDBType == WebbDBTypes.WebbVictoryVolleyball
                     ||this._DBSourceConfig.WebbDBType ==WebbDBTypes.WebbVictoryLacrosse
                     ||this._DBSourceConfig.WebbDBType ==WebbDBTypes.WebbVictorySoccer))
				{
					using(VictoryGameData vicGameData = new VictoryGameData(this.DBSourceConfig))
					{
						vicGameData.CalcDataSource(i_DBSource);
					}
				}
				else if(this._DBSourceConfig.DBConnType == Webb.Data.DBConnTypes.SQLDB)
                {
                    #region SQL DB  / others
                    try
					{
						i_DBSource.DataSource.Tables.Clear();

						SqlConnection connection = new SqlConnection(this._DBSourceConfig.ConnString);	

						connection.Open();

						DataAdapter adapter=new SqlDataAdapter(_DBSourceConfig.DefaultSQLCmd,connection);
                  
						adapter.Fill(i_DBSource.DataSource);

						i_DBSource.DataAdapter=adapter;
					}
					catch(Exception ex)
					{
						MessageBox.Show("Failed to connect SqlServer:\n"+ex.Message,"Failed",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                    }
                    #endregion

                }				
				else
                {
                    #region Access Datatbase
                    if (this._DBSourceConfig.WebbDBType == WebbDBTypes.Others)
                    {
                        OleDbConnection dbconnection = new OleDbConnection(this._DBSourceConfig.ConnString);

                        dbconnection.Open();

                        DataAdapter adapter = new OleDbDataAdapter(_DBSourceConfig.DefaultSQLCmd, dbconnection);

                        adapter.Fill(i_DBSource.DataSource);

                        i_DBSource.DataAdapter = adapter;

                        dbconnection.Close();

                    }
                    else    // Victory Football
                    {
                        string m_SQL = this.GetSQLCMD();	//get sql command by dbconfig

                        System.Diagnostics.Debug.WriteLine(m_SQL);

                        this._DBSourceConfig.DefaultSQLCmd = m_SQL;   //Added this code at 2009-2-2 15:26:11@Simon

                        this._DBManager.Fill(i_DBSource.DataSource, m_SQL);

                        i_DBSource.DataAdapter = new WebbReportDataAdapter(this._DBSourceConfig, this._DBManager.Connection, m_SQL);

                         #region  Calcualte Edl Name Modifed at 2011-1-4 15:53:42@simon

                            i_DBSource.EdlInfos = new EdlInfoCollection();  //Add this code at 2011-1-4 15:46:26@simon

                            StringBuilder sbEdls = new StringBuilder(); //Add this code at 2011-1-4 16:02:40@simon

			                //Edl
                            foreach (string strEdlID in this._DBSourceConfig.Edls)
                            {
                                if (strEdlID == null || strEdlID == string.Empty) continue;

                                if (sbEdls.Length > 0) sbEdls.Append(",");

                                sbEdls.Append(strEdlID);
                            }

                            if (sbEdls.Length > 0&&this._DBSourceConfig.WebbDBType==WebbDBTypes.WebbVictoryFootball)
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

                                        i_DBSource.EdlInfos.Add(edlInfo);
                                    }
                                }
                                catch
                                {
                                }
                            }

                        #endregion
                    }
                    #endregion
                }

                //i_DBSource.Games = this.GetSelectedGameInfo();   //Simon@2010-12-28
                i_DBSource.Games = this.GetSelectedGameInfo();

				i_DBSource.Filters = this.GetSelectedFilterInfo();
                
			}
			else if(this._DBSourceConfig.DBConnType == Webb.Data.DBConnTypes.File)
			{
                if (this._DBSourceConfig.WebbDBType == WebbDBTypes.WebbPlaybook)
                {
                    PlayBookData playBookData = new PlayBookData(this._DBSourceConfig);

                    playBookData.CalcDataSource(i_DBSource);
                }
                else
                {
                    using (AdvGameData advGameData = new AdvGameData(this.DBSourceConfig))
                    {
                        advGameData.CalcDataSource(i_DBSource);
                    }
                }
			}
			else
			{
				try
				{
					i_DBSource.DataSource.ReadXml(this._DBSourceConfig.DBFilePath);

                    i_DBSource.FixCMDefaultHeaders();   // 11-15-2011 Scott
				}
				catch
				{
					return false;
				}
			}
			i_DBSource.DataMember = "Table";

			return true;
		}

		//Get data source without wizard
		public bool GetDataSource(DBSourceConfig i_DBConfig, Webb.Reports.WebbDataSource i_DBSource)
		{
			this.InitializeDBManager(i_DBConfig);

			this._GameTable = this.LoadWebbGames();

			this._FilterTable = this.LoadWebbFilters();

			return this.GetDataSource(i_DBSource);
		}

		#region Convert_Filter_Fuctions
		public DBFilterCollection ConvertFilterInfoToDBFilter(Int32Collection i_FilterIDs)
		{
			if(i_FilterIDs == null)	return null;

			return VictoryFilterHelper.ConvertToDBFilter(i_FilterIDs ,this._DBManager);
		}

		public DBFilterCollection ConvertFilterInfoToDBFilter(FilterInfoCollection i_FilterInfos)
		{
			if(i_FilterInfos == null)	return null;

			DBFilterCollection m_DBFilterCollection = new DBFilterCollection();

			foreach(FilterInfo i_FilterInfo in i_FilterInfos)
			{
				DBFilter m_DBFilter = VictoryFilterHelper.ConvertToDBFilter(i_FilterInfo.FilterID ,this._DBManager);
			
				m_DBFilterCollection.Add(m_DBFilter);
			}
			return m_DBFilterCollection;
		}

		public SectionFilterCollection ConvertFilterInfoToSecFilter(FilterInfoCollection i_FilterInfos)
		{
			if(i_FilterInfos == null)	return null;

			SectionFilterCollection m_SecFilterCollection = new SectionFilterCollection();

			foreach(FilterInfo i_FilterInfo in i_FilterInfos)
			{
				DBFilter m_DBFilter = VictoryFilterHelper.ConvertToDBFilter(i_FilterInfo.FilterID ,this._DBManager);

				SectionFilter m_SectionFilter = new SectionFilter(m_DBFilter);
				
				m_SectionFilter.FilterName = i_FilterInfo.FilterName;
				
				m_SecFilterCollection.Add(m_SectionFilter);
			}
			return m_SecFilterCollection;
		}
		#endregion

		//Show wizard to select database, games , filters ... 
		public System.Windows.Forms.DialogResult ShowWizard(System.Windows.Forms.Form i_Ower,Webb.Reports.WebbDataSource dataSource)
		{
			// TODO: implement
			if(this._ConfigForm==null || this._ConfigForm.IsDisposed) this._ConfigForm = new Webb.Reports.DataProvider.UI.DBConfigWizardForm(this);
			
			this._ConfigForm.SetData(this._DBSourceConfig);
			
			System.Windows.Forms.DialogResult m_Result = this._ConfigForm.ShowDialog(i_Ower);
			
			if(m_Result==DialogResult.OK)
			{
				this._ConfigForm.UpdateData(this._DBSourceConfig);
			}
			return m_Result;
		}

		//Show preview data form for debug
		public void ShowPreviewForm( Webb.Reports.WebbDataSource i_DataSource)
		{
			if(this._PreviewForm==null || this._PreviewForm.IsDisposed) this._PreviewForm = new Webb.Reports.DataProvider.UI.DataSourcePreviewForm();
			
			this._PreviewForm.SetPreviewData(i_DataSource.DataSource);
			
			this._PreviewForm.Show();
		}

		#region Useless_Selector_Forms
		public DialogResult ShowGameSelector(GameInfoCollection i_Games)
		{
			try
			{
				DataTable m_Games = this.LoadWebbGames();
			}
			catch
			{
				MessageBox.Show("Please config data provider.");

				return DialogResult.Cancel;
			}

			this._GameSelector = new Webb.Reports.DataProvider.UI.GameSelectorForm(this);
			
			DialogResult m_Result = this._GameSelector.ShowDialog();
			
			if(m_Result == DialogResult.OK)
			{
				if(i_Games==null) i_Games = new GameInfoCollection();
				
				i_Games.Clear();
				
				GameInfoCollection m_NewGames = this.GetSelectedGameInfo();
				
				foreach(GameInfo m_game in m_NewGames)
				{
					i_Games.Add(m_game);
				}
			}
			return m_Result;
		}

		public DialogResult ShowFilterSelector(FilterInfoCollection i_Filters)
		{
			try
			{
				DataTable m_Filters = this.LoadWebbFilters();
			}
			catch
			{
				MessageBox.Show("Please config data provider.");

				return DialogResult.Cancel;
			}

			this._FilterSelector = new Webb.Reports.DataProvider.UI.FilterSelectorForm(this);

			DialogResult m_Result = this._FilterSelector.ShowDialog();

			if(m_Result == DialogResult.OK)
			{
				if(i_Filters == null) i_Filters = new FilterInfoCollection();

				i_Filters.Clear();
				
				FilterInfoCollection m_NewFilters = this.GetSelectedFilterInfo();

				foreach(FilterInfo m_filter in m_NewFilters)
				{
					i_Filters.Add(m_filter);
				}
			}
			return m_Result;
		}

		public DialogResult ShowSectionFilterSelector(SectionFilterCollection i_SectionFilters)
		{
			if(i_SectionFilters == null) return DialogResult.Cancel;

			try
			{
				DataTable m_Filters = this.LoadWebbFilters();
			}
			catch
			{
				MessageBox.Show("Please config data provider.");

				return DialogResult.Cancel;
			}

			this._SectionFilterSelector = new Webb.Reports.DataProvider.UI.SectionFilterSelectorForm(this);
			
			DialogResult m_Result = this._SectionFilterSelector.ShowDialog();
			
			if(m_Result == DialogResult.OK)
			{
				i_SectionFilters.Clear();

				FilterInfoCollection m_FilterInfos = this.GetSelectedFilterInfo(this._DBSourceConfig.SectionFilterIDs);

				SectionFilterCollection m_NewSectionFilters = this.ConvertFilterInfoToSecFilter(m_FilterInfos);

				if(m_NewSectionFilters == null) return m_Result;

				foreach(SectionFilter m_SecFilter in m_NewSectionFilters)
				{
					i_SectionFilters.Add(m_SecFilter);
				}
			}
			return m_Result;
		}

		//02-26-2008@Scott
		public DialogResult ShowCustomSectionFilterSelector(SectionFilterCollection i_SectionFilters)
		{
			if(i_SectionFilters == null) return DialogResult.Cancel;

			UI.CustomSectionFiltersForm customSectionFilter = new Webb.Reports.DataProvider.UI.CustomSectionFiltersForm();
		
			DialogResult result = customSectionFilter.ShowDialog();

			if(result == DialogResult.OK)
			{
				i_SectionFilters.Clear();

				SectionFilterCollection m_NewSectionFilters = customSectionFilter.GetSelectedSectionFilters();

                foreach (SectionFilter m_SecFilter in m_NewSectionFilters)
                {
                    i_SectionFilters.Add(m_SecFilter);
                }
            }

            return result;
		}



		//02-26-2008@Scott
		public DialogResult ShowEditorCustomSectionFilterSelector(SectionFilterCollection i_SectionFilters)
		{
			if(i_SectionFilters == null) return DialogResult.Cancel;

			UI.CustomSectionFiltersForm customSectionFilter = new Webb.Reports.DataProvider.UI.CustomSectionFiltersForm(i_SectionFilters);
		
			DialogResult result = customSectionFilter.ShowDialog();

			if(result == DialogResult.OK)
			{
				i_SectionFilters.Clear();

				SectionFilterCollection m_NewSectionFilters = customSectionFilter.GetSelectedSectionFilters();

				foreach (SectionFilter m_SecFilter in m_NewSectionFilters)
				{
					i_SectionFilters.Add(m_SecFilter);
				}
			}

			return result;
		}

        public DialogResult ShowEditorCustomSectionFilterSelector(SectionFilterCollection i_SectionFilters,WebbDBTypes webbDbType)
        {
            if (i_SectionFilters == null) return DialogResult.Cancel;

            UI.CustomSectionFiltersForm customSectionFilter = new Webb.Reports.DataProvider.UI.CustomSectionFiltersForm(i_SectionFilters, webbDbType);

            DialogResult result = customSectionFilter.ShowDialog();

            if (result == DialogResult.OK)
            {
                i_SectionFilters.Clear();

                SectionFilterCollection m_NewSectionFilters = customSectionFilter.GetSelectedSectionFilters();

                foreach (SectionFilter m_SecFilter in m_NewSectionFilters)
                {
                    i_SectionFilters.Add(m_SecFilter);
                }
            }

            return result;
        }

        //08-15-2008@Scott
        public DialogResult ShowAdvReportFilterSelector(SectionFilterCollection i_SectionFilters)
		{
            if (i_SectionFilters == null) return DialogResult.Cancel;

            UI.AdvReportSectionFiltersForm customSectionFilter = new Webb.Reports.DataProvider.UI.AdvReportSectionFiltersForm();

            DialogResult result = customSectionFilter.ShowDialog();

            if (result == DialogResult.OK)
            {
                i_SectionFilters.Clear();

                SectionFilterCollection m_NewSectionFilters = customSectionFilter.GetSelectedSectionFilters();

                foreach (SectionFilter m_SecFilter in m_NewSectionFilters)
                {
                    i_SectionFilters.Add(m_SecFilter);
                }
            }

            return result;
		}

		public DialogResult ShowEditorAdvReportFilterSelector(SectionFilterCollection i_SectionFilters)
		{
			if (i_SectionFilters == null) return DialogResult.Cancel;

			UI.AdvReportSectionFiltersForm customSectionFilter = new Webb.Reports.DataProvider.UI.AdvReportSectionFiltersForm(i_SectionFilters);

			DialogResult result = customSectionFilter.ShowDialog();

			if (result == DialogResult.OK)
			{
				i_SectionFilters.Clear();

				SectionFilterCollection m_NewSectionFilters = customSectionFilter.GetSelectedSectionFilters();

				foreach (SectionFilter m_SecFilter in m_NewSectionFilters)
				{
					i_SectionFilters.Add(m_SecFilter);
				}
			}

			return result;
		}
		#endregion
		
		#region IDisposable Members
		public void Dispose()
		{
			// TODO:  Add WebbDataProvider.Dispose implementation
			if(this._DBManager!=null)
			{
				this._DBManager.Dispose();
				this._DBManager = null;
			}
		}
		#endregion

		#region Make_SQL_Command
		//Get SQL command text, combine games command and filters command

		private string GetSQLCMD()       //Mainly in Victory Football product
		{
			string m_CMD = this.CreateSelectedGamesCMD();

			#region Modified Area
			//			Int32Collection m_SelectedFilters = this._DBSourceConfig.FilterIDs;
			//			
			//			if(m_SelectedFilters != null && m_SelectedFilters.Count>0)
			//			{
			//				string m_FilterConditions = this.CreateSelectedFilterCMD();
			//
			//				if(m_FilterConditions!=null&&m_FilterConditions.Length>0)
			//				{
			//					//m_CMD = string.Format("{0} and {1}",m_CMD,m_FilterConditions);
			//				}
			//			}
			#endregion        //Modify at 2008-10-10 8:54:48@Scott		

			#region Modify codes at 2009-6-18 16:28:37@Simon
            
			if(this._DBSourceConfig.GameIDs.Count==0)
			{
				m_CMD="select * from [GameDetail] where false";
			}
			string strEdls=string.Empty;

			foreach(string strEdlID in this._DBSourceConfig.Edls)
			{				
					string CutupSql="select [PlayID] from [EdlDetail] where [EdlID]="+strEdlID;

                    CutupSql = string.Format(" union all select * from [GameDetail] where [PlayID] in ({0})", CutupSql);

                    m_CMD = m_CMD + CutupSql;
			}
			
			#endregion        //End Modify

			
			return m_CMD;
		}

		//Get command text of selected games
		private string CreateSelectedGamesCMD()
		{
			int type=(int)_DBSourceConfig.WebbDBType ;

			if(this._DBSourceConfig.DBConnType == Webb.Data.DBConnTypes.OleDB
				&& (int)this._DBSourceConfig.WebbDBType >= 100 && (int)this._DBSourceConfig.WebbDBType <= 105)
			{
				return this.CreateSelectedGamesCMD_Access();
			}
			else if(this._DBSourceConfig.DBConnType == Webb.Data.DBConnTypes.SQLDB
				&& (this._DBSourceConfig.WebbDBType == WebbDBTypes.WebbAdvantageFootball||(type>0&&type<9)))
			{
				return this.CreateSelectedGamesCMD_SQL();
			}
			else
			{
				return null;
			}
		}

		private string CreateSelectedGamesCMD_Access()
		{
			string m_strResultSQL = string.Empty;
			string m_IDs = this.CreateIDs();
			switch(this._DBSourceConfig.WebbDBType)
			{
				case WebbDBTypes.WebbVictoryFootball:
					m_strResultSQL = string.Format(CMD_SelectGames_Access,m_IDs);
					break;
				case WebbDBTypes.WebbVictoryBasketball:
				case WebbDBTypes.WebbVictoryVolleyball:
				case WebbDBTypes.WebbVictoryHockey:
                case WebbDBTypes.WebbVictoryLacrosse:
                case WebbDBTypes.WebbVictorySoccer:
				{
					string m_EdlIDs = this.CreateEdlIDs();
					if(m_IDs == string.Empty) m_IDs = "-1";
					if(m_EdlIDs == string.Empty) m_EdlIDs = "-1";
					m_strResultSQL = string.Format(CMD_SelectGamesEdls_Access_New_Victory,m_IDs,m_EdlIDs);
					break;
				}
				default:
					m_strResultSQL = string.Format(CMD_SelectGames_Access,m_IDs);
					break;
			}

#if DEBUG
			MessageBox.Show(m_strResultSQL);	//tag
#endif
			return m_strResultSQL;
		}
		private string CreateSelectedGamesCMD_SQL()
		{
			string m_IDs = this.CreateIDs();
			if(m_IDs == string.Empty) m_IDs = "-1";
			return string.Format(CMD_SelectGames_SQL,m_IDs);
		}
		private string CreateIDs()
		{
			Int32Collection m_GameIDs = this._DBSourceConfig.GameIDs;
			StringBuilder m_SB = new StringBuilder(32);
			foreach(int m_ID in m_GameIDs)
			{
				m_SB.Append(string.Format("{0},",m_ID));
			}
			if(m_SB.Length > 0) m_SB.Length--;
			return m_SB.ToString();
		}
		private string CreateEdlIDs()
		{
			StringBuilder m_SB = new StringBuilder(32);
			foreach(string m_EdlID in this._DBSourceConfig.Edls)
			{
				m_SB.Append(string.Format("{0},",m_EdlID));
			}
			if(m_SB.Length > 0) m_SB.Length--;
			return m_SB.ToString();
		}
		//get command text of selected filters
		private string CreateSelectedFilterCMD()
		{
			Int32Collection m_SelectedFilterID = this._DBSourceConfig.FilterIDs;
			switch(this._DBSourceConfig.WebbDBType)
			{
				case WebbDBTypes.WebbVictoryFootball:
				case WebbDBTypes.WebbVictoryBasketball:
				case WebbDBTypes.WebbVictoryVolleyball:
				case WebbDBTypes.WebbVictoryHockey:
                case  WebbDBTypes.WebbVictoryLacrosse:
                case  WebbDBTypes.WebbVictorySoccer:
					return this.CreateSelectedFilterCMD_Access(m_SelectedFilterID);
				case WebbDBTypes.WebbAdvantageFootball:
					return this.CreateSelectedFilterCMD_SQL(m_SelectedFilterID);
				default:
					return this.CreateSelectedFilterCMD_SQL(m_SelectedFilterID);
					
			}			
		}
		private string CreateSelectedFilterCMD_Access(Int32Collection i_FilterIDs)
		{
			return VictoryFilterHelper.GetFiltersSQLCMD(i_FilterIDs, this._DBManager);
		}
		private string CreateSelectedFilterCMD_SQL(Int32Collection i_FilterIDs)
		{
			return null;
		}
		#endregion

		#region Load_Game_Table & Load_Filter_Table
		private DataTable CreateDataTable()
		{			
			switch(this._DBSourceConfig.WebbDBType)
			{
				case WebbDBTypes.WebbAdvantageFootball:
				case WebbDBTypes.PlayMaker_Basic:
				case WebbDBTypes.PlayMaker:
		        case WebbDBTypes.PlayMaker_LE:
				case WebbDBTypes.QuickCuts:
				case WebbDBTypes.GameDay:
					if(this._DBSourceConfig.DBConnType == Webb.Data.DBConnTypes.SQLDB)
						return new DBWebbAdvantage.GamesDataTable();
					else
						return null;
				case WebbDBTypes.WebbVictoryFootball:
				case WebbDBTypes.WebbVictoryBasketball:
				case WebbDBTypes.WebbVictoryVolleyball:
				case WebbDBTypes.WebbVictoryHockey:
                case WebbDBTypes.WebbVictoryLacrosse:
                case WebbDBTypes.WebbVictorySoccer:
					if(this._DBSourceConfig.DBConnType == Webb.Data.DBConnTypes.OleDB)
						return new DBWebbVictory.GameDataTable();
					else
						return null;
				default://common
					return null;
			}
		}

		public  DataTable LoadWebbGames()
		{
			DataTable m_Table = this.CreateDataTable();
			if(m_Table != null)
			{
				switch(this._DBSourceConfig.WebbDBType)
				{
					case WebbDBTypes.WebbAdvantageFootball:
					case WebbDBTypes.PlayMaker_Basic:  //2009-7-27 10:46:15@Simon Add this Code
					case WebbDBTypes.PlayMaker:
					case WebbDBTypes.PlayMaker_LE:
					case WebbDBTypes.QuickCuts:
					case WebbDBTypes.GameDay:
						this._DBManager.FillDataTable(m_Table,CMD_WebbAdvantage_GetGames);
						break;
					case WebbDBTypes.WebbVictoryFootball:
					case WebbDBTypes.WebbVictoryBasketball:
					case WebbDBTypes.WebbVictoryVolleyball:
					case WebbDBTypes.WebbVictoryHockey:
                    case WebbDBTypes.WebbVictoryLacrosse:
                    case WebbDBTypes.WebbVictorySoccer:
						this._DBManager.FillDataTable(m_Table,CMD_WebbVictory_GetGames);
						break;
					default://common
						break;
				}
			}
			this._GameTable = m_Table;
			return m_Table;
		}      

		public DataTable LoadWebbEdls()
		{
			DataTable m_Table = new DataTable();
			if(m_Table != null)
			{
				try
				{
					this._DBManager.FillDataTable(m_Table,"select [EdlId],[EdlName] from [Edl]");
				}
				catch
				{

					m_Table=null;
				}
					
			}			
			return m_Table;
		}

		public DataTable LoadWebbFilters()
		{
			DataTable m_Table = null;
			switch(this._DBSourceConfig.WebbDBType)
			{
				case WebbDBTypes.WebbVictoryFootball:
				case WebbDBTypes.WebbVictoryBasketball:
				case WebbDBTypes.WebbVictoryVolleyball:
				case WebbDBTypes.WebbVictoryHockey:
                case WebbDBTypes.WebbVictoryLacrosse:
                case WebbDBTypes.WebbVictorySoccer:
					m_Table = new DBWebbVictory.AutoCutupTableDataTable();
					this._DBManager.FillDataTable(m_Table,CMD_WebbVictory_GetFilters);
					break;
				case WebbDBTypes.WebbAdvantageFootball:
				case WebbDBTypes.PlayMaker_Basic:  //2009-7-27 10:46:15@Simon Add this Code
				case WebbDBTypes.PlayMaker:
				case WebbDBTypes.PlayMaker_LE:
				case WebbDBTypes.QuickCuts:
				case WebbDBTypes.GameDay:
					//Advantage filter
					break;
				default:
					break;
			}
			this._FilterTable = m_Table;
			return m_Table;
		}
		#endregion

		#region Load_All_Tables_Info(Common)
		public DataTable LoadTables()
		{
			switch(this._DBSourceConfig.DBConnType)
			{//common
				case Webb.Data.DBConnTypes.OleDB:	//Access
					return LoadTablesFromAccess();
				case Webb.Data.DBConnTypes.SQLDB:	//SQL
					return LoadTablesFromSQL();	
				case Webb.Data.DBConnTypes.XMLFile:	//XML
					return null;
			}
			return null;
		}

		private DataTable LoadTablesFromAccess()
		{
			OleDbConnection conn = this._DBManager.Connection as OleDbConnection;
			if(conn==null) return null;
			DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] {null, null, null, "TABLE"});
			return schemaTable;
		}
		
		private DataTable LoadTablesFromSQL()
		{
			DataTable m_Tables = new DataTable("Tables");
			this._DBManager.FillDataTable(m_Tables,CMD_SQL_GetTables);
			return m_Tables;
		}

		internal DataSet LoadXmlDataSet()
		{
			string m_Path = this._DBSourceConfig.DBFilePath;
			DataSet m_DataSet = new DataSet("XML");
			if(System.IO.File.Exists(m_Path))
			{
				try
				{
					m_DataSet.ReadXml(m_Path);
				}
				catch
				{
					return null;
				}
			}
			return m_DataSet;
		}
		#endregion

		#region GetSelectedGameInfo & GetSelectedFilterInfo
		internal GameInfoCollection GetSelectedGameInfo()
		{
			return this.FillGameInfoFromGameTable(this.GameTable,this._DBSourceConfig.GameIDs);
		}


        internal GameInfoCollection GetSelectedGameInfoNew()    //Simon @ 2010-12-27
        {
            Int32Collection gameIds = new Int32Collection();
      
            if (this._DBSourceConfig.GameIDs != null)
            {
                this._DBSourceConfig.GameIDs.CopyTo(gameIds);
            }

            string product = this._DBSourceConfig.WebbDBType.ToString().ToLower();

            try
            {
                if (this._DBManager != null && this._DBSourceConfig.Edls != null && product.StartsWith("webbvictory"))
                {
                    #region Fill games in eld
                    DataTable table = new DataTable();

                    string i_cmd = "select distinct [GameID] from [EdlDetail] where [EdlID] in ({0})";

                    StringBuilder sb = new StringBuilder();

                    foreach (string edlId in this._DBSourceConfig.Edls)
                    {
                        if (edlId == null || edlId==string.Empty) continue;

                        if (sb.Length > 0) sb.Append(",");

                        sb.Append(edlId);
                    }

                    i_cmd = string.Format(i_cmd, sb);

                    this._DBManager.FillDataTable(table, i_cmd);

                    if (table.Columns.Count > 0)
                    {
                        foreach (DataRow dr in table.Rows)
                        {
                            if (dr[0] is DBNull || dr[0] == null) continue;

                            int gameId = -1;

                            if (!int.TryParse(dr[0].ToString(), out gameId)) continue;

                            if (!gameIds.Contains(gameId)) gameIds.Add(gameId);
                        }

                    }
                    #endregion
                }
            }
            catch
            {
            }

            return this.FillGameInfoFromGameTable(this.GameTable, gameIds);
        }

		internal FilterInfoCollection GetSelectedFilterInfo()
		{
			return this.FillFilterInfoFromFilterTable(this.FilterTable,this._DBSourceConfig.FilterIDs);
		}     

		//Scott@2007-11-23 09:33 modified some of the following code.
		internal FilterInfoCollection GetSelectedFilterInfo(Int32Collection i_FilterIDs)
		{
			return this.FillFilterInfoFromFilterTable(this.FilterTable,i_FilterIDs);
		}

        private GameInfoCollection FillGameInfoFromGameTable(DataTable i_Table, Int32Collection i_Selected)
		{
			if(i_Table == null || i_Selected == null)
				return null;           


			if(i_Table.Rows.Count>0 && i_Selected.Count>0)
			{
				GameInfoCollection m_GameInfos = new GameInfoCollection();

				if(i_Table is DBWebbReportCommon.GamesDataTable)
				{//Common
					DBWebbReportCommon.GamesDataTable m_Table = i_Table as DBWebbReportCommon.GamesDataTable;

					foreach(DataRow i_dr in m_Table)
					{
						int GameID = Convert.ToInt32(i_dr[m_Table.GameIDColumn]);
						if(i_Selected.Contains(GameID))
						{
							GameInfo m_GameInfo = new GameInfo();
							m_GameInfo.GameID = GameID;
							m_GameInfo.FolderID = Convert.ToInt32(i_dr[m_Table.FolderIDColumn]);
							m_GameInfo.Object = i_dr[m_Table.ObjectColumn].ToString();
							m_GameInfo.Opponent = i_dr[m_Table.OpponentColumn].ToString();
							m_GameInfo.Location = i_dr[m_Table.LocationColumn].ToString();
							m_GameInfo.GameDate = i_dr[m_Table.DateColumn].ToString();
							m_GameInfos.Add(m_GameInfo);
						}
					}
					return m_GameInfos;
				}
				if(i_Table is DBWebbVictory.GameDataTable)
				{//Victory
					DBWebbVictory.GameDataTable m_Table = i_Table as DBWebbVictory.GameDataTable;
					foreach(DataRow i_dr in m_Table)
					{
						int GameID = Convert.ToInt32(i_dr[m_Table.GameIDColumn]);
						if(i_Selected.Contains(GameID))
						{
							GameInfo m_GameInfo = new GameInfo();
							m_GameInfo.GameID = GameID;
							m_GameInfo.GameName = i_dr[m_Table.GameNameColumn].ToString();
							m_GameInfo.Location = i_dr[m_Table.LocatioinColumn].ToString();
							m_GameInfo.GameDate = i_dr[m_Table.DateColumn].ToString();
							m_GameInfos.Add(m_GameInfo);
						}
					}
					return m_GameInfos;
				}
			}
			return null;
		}

		private FilterInfoCollection FillFilterInfoFromFilterTable(DataTable i_Table,Int32Collection i_Selected)
		{
			if(i_Table == null || i_Selected == null) return null;

			if(i_Table.Rows.Count>0 && i_Selected.Count>0)
			{
				FilterInfoCollection m_FilterInfos = new FilterInfoCollection();
				
				if(i_Table is DBWebbVictory.AutoCutupTableDataTable)
				{//Victory
					DBWebbVictory.AutoCutupTableDataTable m_Table = i_Table as DBWebbVictory.AutoCutupTableDataTable;
					
					foreach(int i_FilterID in i_Selected)
					{
						foreach(DataRow i_Row in m_Table)
						{
							int m_FilterID = Convert.ToInt32(i_Row[m_Table.CutupIDColumn]);
							if(m_FilterID == i_FilterID)
							{
								FilterInfo m_FilterInfo = new FilterInfo();

								m_FilterInfo.FilterID = i_FilterID;
								
								m_FilterInfo.FilterName = i_Row[m_Table.CutupNameColumn].ToString();
								
								m_FilterInfos.Add(m_FilterInfo);
							}
						}
					}
					return m_FilterInfos;
				}
			}
			return null;
		}
		#endregion

		#region
		//Wu.Country@2007-12-03 13:53 added this region.
		public VideoInfoCollection LoadVideoInfo(DataSet i_DataSet, Int32Collection i_RowIndicators)
		{
			switch(this._DBSourceConfig.WebbDBType)
			{
				case WebbDBTypes.WebbVictoryFootball:
				case WebbDBTypes.WebbVictoryBasketball:
				case WebbDBTypes.WebbVictoryVolleyball:
				case WebbDBTypes.WebbVictoryHockey:
                case WebbDBTypes.WebbVictoryLacrosse:
                case WebbDBTypes.WebbVictorySoccer:
					return this.LoadVideoInfo_Victory(i_DataSet,i_RowIndicators);

				case WebbDBTypes.WebbAdvantageFootball:
				case WebbDBTypes.PlayMaker_Basic:  //2009-7-27 10:46:15@Simon Add this Code
				case WebbDBTypes.PlayMaker:
				case WebbDBTypes.PlayMaker_LE:
				case WebbDBTypes.QuickCuts:
				case WebbDBTypes.GameDay:
					return this.LoadVideoInfo_Advatage(i_DataSet,i_RowIndicators);
			}
			return null;
		}
		private VideoInfoCollection LoadVideoInfo_Advatage(DataSet i_DataSet, Int32Collection i_RowIndicators)
		{
			if(i_DataSet.Tables.Count<=1) return null;

			DataTable m_Table = i_DataSet.Tables[1];

			if(m_Table.Rows.Count <= 0 || m_Table.Rows.Count < i_RowIndicators.Count) return null;

			VideoInfoCollection videoInfoCollection = new VideoInfoCollection();

			if(m_Table.Columns.Contains("sysid"))
			{//Sql
				foreach(int m_RowIndex in i_RowIndicators)
				{
					DataRow parentRow = i_DataSet.Tables[0].Rows[m_RowIndex];
				
					string strFilter = string.Format("GameID = {0} AND sysid = {1}",parentRow["GameID"],parentRow["sysid"]);

					DataRow[] childRows = i_DataSet.Tables[1].Select(strFilter);

					foreach(DataRow row in childRows)
					{
						VideoInfo videoInfo = new VideoInfo();

						videoInfo.StartFrame = Convert.ToInt32(row["StartFrame"]);

						videoInfo.EndFrame =  Convert.ToInt32(row["EndFrame"]);

						videoInfo.FilePath = row["DigitalFileName"].ToString();

						videoInfo.Angle = Convert.ToInt16(row["Angle"]);

						videoInfo.GameName = row["GameName"].ToString();

						videoInfo.PlayNum = Convert.ToInt32(row["sysid"]);

						videoInfo.MasterNum = Convert.ToInt32(row["sysid"]);

						videoInfoCollection.Add(videoInfo);
					}
				}
			}
			else
			{//File
				foreach(int m_RowIndex in i_RowIndicators)
				{
					DataRelation relation = i_DataSet.Relations["VideoInfo"];

					DataRow[] rows = i_DataSet.Tables[0].Rows[m_RowIndex].GetChildRows(relation);

					foreach(DataRow row in rows)
					{
						VideoInfo videoInfo = new VideoInfo();

						videoInfo.StartFrame = Convert.ToInt32(row["Start Frame"]);

						videoInfo.EndFrame =  Convert.ToInt32(row["End Frame"]);

						videoInfo.FilePath = row["Video File Path"].ToString();

						videoInfo.Angle = Convert.ToInt16(row["Angle"]);

						videoInfo.GameName = row["Game Name"].ToString();			//05-06-2008@Scott

						videoInfo.PlayNum = Convert.ToInt32(row["Play Number"]);	//05-06-2008@Scott

						videoInfo.MasterNum = Convert.ToInt32(row["Master Num"]);	//05-07-2008@Scott

						videoInfoCollection.Add(videoInfo);
					}
				}
			}
			
			return videoInfoCollection;
		}
		private VideoInfoCollection LoadVideoInfo_Victory(DataSet i_DataSet, Int32Collection i_RowIndicators)
		{
			if(i_DataSet.Tables.Count<=0) return null;
			DataTable m_Table = i_DataSet.Tables[0];
			//if(i_Table==null) return null;
			if(m_Table.Rows.Count<i_RowIndicators.Count) return null;
			if(!m_Table.Columns.Contains("PlayID")) return null;
			if(!m_Table.Columns.Contains("GameID")) return null;
			StringBuilder m_IDs = new StringBuilder(64);
			foreach(int m_RowIndex in i_RowIndicators)
			{
				string m_ID = m_Table.Rows[m_RowIndex]["PlayID"].ToString();
				m_IDs.Append(m_ID);
				m_IDs.Append(",");
			}
			m_IDs.Append("0");
			//if(m_IDs.Length>0) m_IDs.Length--;
			string M_CMD = string.Format(CMD_SelectVideoInfo_Victory_Ex,m_IDs.ToString());	//12-21-2007@Scott
			//
			DataTable m_VideoInfo = new DataTable();			
			this._DBManager.FillDataTable(m_VideoInfo,M_CMD);
			//
			return this.CreateVideoInfoCollection(m_VideoInfo);
		}

		private VideoInfoCollection CreateVideoInfoCollection(DataTable i_VideoInfoTable)
		{
			VideoInfoCollection m_VideoInfoColl = new VideoInfoCollection();
			foreach(DataRow m_Row in i_VideoInfoTable.Rows)
			{
				VideoInfo m_Info = new VideoInfo();
				m_Info.Angle = Convert.ToInt16(m_Row["Angle"]);
				m_Info.PlayID = Convert.ToInt32(m_Row["PlayID"]);
				m_Info.GameID = Convert.ToInt32(m_Row["GameID"]);
				m_Info.StartTimeSpan = VideoInfo.NewTimeSpan(m_Row["Start Time"].ToString());
				m_Info.EndTimeSpan = VideoInfo.NewTimeSpan(m_Row["End Time"].ToString());
				string m_Path = m_Row["VideoFilePath"].ToString();
#if !DEMO		
				if(!System.IO.Directory.Exists(m_Path))
				{
					continue;
				}
				else if(System.IO.Directory.GetFiles(m_Path).Length<=0)
				{
					continue;
				}
#endif
				if(m_Info.EndFrame<m_Info.StartFrame) continue;
				m_Info.FilePath = m_Path;
				m_VideoInfoColl.Add(m_Info);
			}
			return m_VideoInfoColl;
		}
		#endregion
	}
	#endregion
}
