/***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:DBManager.cs
 * Author:Country.Wu [EMail:Webb.Country.Wu@163.com]
 * Create Time:10/30/2007 11:10:21 AM
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
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.Common;
using System.Text;
using System.Collections.Specialized;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Serialization;
using System.Diagnostics;


namespace Webb.Data
{
	[Serializable]
	public enum DBConnTypes
	{
		OleDB = 0,
		SQLDB = 1,
		XMLFile =2,
		File = 3,
	}
	/// <summary>
	/// 
	/// </summary>
	public interface IDBManager:IDisposable
	{
		DBConnTypes DBConnType {get;set;}
		//IDbCommand Command	{get;set;}
		string ConnString	{get;set;}
		IDbConnection Connection{get;}
		//IError LastError{get;}
		void CreateNewConn(string i_connStr);
		int ExecuteNonQuery(string i_CMD);
		void Fill(DataSet i_DataSet,string i_CMD);
		void FillDataTable(DataTable i_dataTable,string i_CMD);
		void UpdateDataTable(DataTable i_dataTable,string i_CMD);
		void GetTableScheam(DataTable i_dataTable,string i_CMD);
		IDbDataParameter NewParameter(string i_paraName,object i_value);
		string[] LoadTalbes();
		IDbCommand NewCommand();
		IDbCommand NewCommand(string i_SQLCommand);
		IDbDataAdapter NewDataAdapter();
		IDbDataAdapter NewDataAdapter(string i_SQLCommand);
	}

	/// <summary>
	/// Summary description for DBManager.
	/// </summary>
	public class DBHelper
	{
		#region static string resource
		//Jet
		static private readonly string _DefaultSQLJet_1 = "workstation id=\"{0}\";packet size=4096;data source=\"{1}\";initial catalog={2};user id={3}; password={4}";
		static private readonly string _DefaultSQLJet_2 = "workstation id=\"{0}\";packet size=4096;integrated security=SSPI;persist security info=True;data source=\"{1}\";initial catalog={2}";
		static private readonly string _DefaultSQLOleJet = "Provider=SQLOLEDB;workstation id=\"{0}\";packet size=4096;integrated security=SSPI;persist security info=True;data source=\"{1}\"";//initial catalog={0}";
		static private readonly string _DefaultOleJet = @"Jet OLEDB:Global Partial Bulk Ops=2;Jet OLEDB:Registry Path=;Jet OLEDB:Database Locking Mode=1;Jet OLEDB:Database Password=;Data Source=""{0}"";Password=;Jet OLEDB:Engine Type=5;Jet OLEDB:Global Bulk Transactions=1;Provider=""Microsoft.Jet.OLEDB.4.0"";Jet OLEDB:System database=;Jet OLEDB:SFP=False;Extended Properties=;Mode=Share Deny None;Jet OLEDB:New Database Password=;Jet OLEDB:Create System Database=False;Jet OLEDB:Don't Copy Locale on Compact=False;Jet OLEDB:Compact Without Replica Repair=False;User ID=Admin;Jet OLEDB:Encrypt Database=False";
		//CMD
		//Others
		static public readonly string SQLExpress_DataSourceName = ".\\SQLEXPRESS";
		static public readonly string SQL2000_DataSourceName = ".";
		static private readonly string _DefaultSQLServerName = ".";
		#endregion

		#region Static help functions
		/// <summary>
		/// Get Ole conneciton string for Access DB file.
		/// </summary>
		/// <param name="i_DBFilePath">Full file path for Access DB file.</param>
		/// <returns></returns>
		static public string GetOleConnString(string i_DBFilePath)
		{
			return string.Format(_DefaultOleJet,i_DBFilePath);
		}

		/// <summary>
		/// Get Ole connection string for SQL with Windows Authorization. And without DB catalog.
		/// </summary>
		/// <returns></returns>		
		static public string GetSQLConnString_Ole()
		{
			return GetSQLConnString(_DefaultSQLServerName,SQLExpress_DataSourceName);
		}		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="i_ServerName">Server Name</param>
		/// <param name="i_DataSource">Data Source Name, SQL2000 or SQLExpress</param>
		/// <returns></returns>
		static public string GetSQLConnString_Ole(string i_ServerName,string i_DataSource)
		{
			return string.Format(_DefaultSQLOleJet,i_ServerName,i_DataSource);
		}

		/// <summary>
		/// Get SQL connection string for SQL with Windows Authorization.
		/// </summary>
		/// <param name="i_DBName"></param>
		/// <returns>DB name.</returns>
		static public string GetSQLConnString(string i_UserID,string i_Password, string i_ServerName, string i_DBCatalogName,string i_DataSource)
		{
			return string.Format(_DefaultSQLJet_1,i_ServerName,i_DataSource,i_DBCatalogName,i_UserID,i_Password);
		}
		static public string GetSQLConnString(string i_ServerName, string i_DBCatalogName,string i_DataSource)
		{
			return string.Format(_DefaultSQLJet_2,i_ServerName,i_DataSource,i_DBCatalogName);
		}
		static public string GetSQLConnString(string i_DBCatalogName,string i_DataSource)
		{
			return GetSQLConnString(_DefaultSQLServerName,i_DBCatalogName,i_DataSource);
		}
		static public string GetSQLConnString(string i_DBCatalogName)
		{
			return GetSQLConnString(_DefaultSQLServerName,i_DBCatalogName,SQLExpress_DataSourceName);
		}
		#endregion

		public DBHelper()
		{
			//
			// TODO: Add constructor logic here
			//			
		}

		public static IDbDataParameter NewParam(string i_paraName,object i_value,DBConnTypes i_type)
		{
			switch(i_type)
			{
				default:
				case DBConnTypes.OleDB: return new OleDbParameter(i_paraName,i_value) as IDbDataParameter;
				case DBConnTypes.SQLDB: return new SqlParameter(i_paraName,i_value) as IDbDataParameter;	
			}
		} 

		public static IDBManager NewDBManager(DBConnTypes i_type,string i_ConnString)
		{
			if(i_type==DBConnTypes.OleDB) return new OleDBManager(i_ConnString);
			else if(i_type==DBConnTypes.SQLDB) return new SQLDBManager(i_ConnString);
			else return null;
		}
	}

	#region public class DBManager
	/*Descrition:   */
	abstract public class DBManager : IDBManager
	{
		//Wu.Country@2007-10-30 11:15 AM added this class.
		protected string _ConnString;
		protected DBConnTypes _DBConnType;
		//Fields
		//Properties
		//ctor
		public DBManager(){}
		virtual protected void Initialize(){}
		//Methods
		#region IDBManager Members
		public DBConnTypes DBConnType
		{
			get{return this._DBConnType;}
			set{this._DBConnType = value;}
		}
		public string ConnString
		{
			get
			{
				// TODO:  Add DBHelper.ConnString getter implementation
				return this._ConnString;
			}
			set
			{
				// TODO:  Add DBHelper.ConnString setter implementation
				if(this._ConnString == value) return;
				this._ConnString = value;
			}
		}
		//
		abstract public void CreateNewConn(string i_connStr);
		abstract public int ExecuteNonQuery(string i_CMD);
		abstract public void Fill(DataSet i_DataSet,string i_CMD);
		abstract public void FillDataTable(DataTable i_dataTable,string i_CMD);
		abstract public void UpdateDataTable(DataTable i_dataTable,string i_CMD);
		abstract public void GetTableScheam(DataTable i_dataTable,string i_CMD);
		abstract public IDbConnection Connection{get;}		
		abstract public string[] LoadTalbes();
		abstract public IDbCommand NewCommand();
		abstract public IDbCommand NewCommand(string i_SQLCommand);
		abstract public IDbDataAdapter NewDataAdapter();
		abstract public IDbDataAdapter NewDataAdapter(string i_SQLCommand);
		//
		virtual public IDbDataParameter NewParameter(string i_paraName,object i_value)
		{
			return DBHelper.NewParam(i_paraName,i_value,this.DBConnType);
		}
		#endregion

		#region IDisposable Members
		virtual public void Dispose()
		{
		}
		#endregion
	}
	#endregion

	#region public class OleDBManager
	/*Descrition:   */
	public class OleDBManager : DBManager
	{
		//Wu.Country@2007-10-30 02:47 PM added this class.
		//Fields
		protected OleDbCommand _DefaultDBCommand;
		protected OleDbDataAdapter _DefaultDataAdapter;
		protected OleDbConnection _Connection;
		//Properties		
		//ctor
		public OleDBManager()
		{
			this._DBConnType = DBConnTypes.OleDB;
		}

		public OleDBManager(string i_ConStr)
		{
			this._ConnString = i_ConStr;
			this.Initialize();
			//
		}

		override protected void Initialize()
		{
			this._DBConnType = DBConnTypes.OleDB;
			this._Connection = new OleDbConnection(this._ConnString);
			this._DefaultDataAdapter = new OleDbDataAdapter();
			this._DefaultDBCommand = new OleDbCommand();
			this._DefaultDBCommand.Connection = this._Connection;
			//
			this._Connection.Open();
		}

		//Methods

		public override void CreateNewConn(string i_connStr)
		{
			//base.CreateNewConn (i_connStr);
			this.Dispose();		//Release all resource.
			this._ConnString = i_connStr;
			this.Initialize();	//Re-Initialize the manager again.
		}

//		public override void ExecuteNonQuery(string i_CMD)
//		{
//			base.ExecuteNonQuery (i_CMD);
//		}

//		public override void Fill(DataSet i_DataSet, string i_CMD)
//		{
//			base.Fill (i_DataSet, i_CMD);
//		}

		public override void FillDataTable(DataTable i_dataTable, string i_CMD)
		{
			try
			{
				this._DefaultDBCommand.Connection = this._Connection;
				this._DefaultDBCommand.CommandText = i_CMD;
				this._DefaultDataAdapter.SelectCommand = this._DefaultDBCommand;
				this._DefaultDataAdapter.Fill(i_dataTable);
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.ToString());
			}
		}

		public override void GetTableScheam(DataTable i_dataTable, string i_CMD)
		{
			this._DefaultDBCommand.Connection = this._Connection;
			this._DefaultDBCommand.CommandText = i_CMD;
			this._DefaultDataAdapter.SelectCommand = this._DefaultDBCommand;
			this._DefaultDataAdapter.FillSchema(i_dataTable,SchemaType.Mapped);
		}

		public override string[] LoadTalbes()
		{
			//			
			DataTable schemaTable = this._Connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] {null, null, null, "TABLE"});
			string[] m_Tables = new string[schemaTable.Rows.Count];
			//TABLE_CATALOG,TABLE_SCHEMA,TABLE_NAME,TABLE_TYPE
			int m_Index = 0;
			foreach(DataRow m_Row in schemaTable.Rows)
			{
				m_Tables[m_Index++] = m_Row["TABLE_NAME"].ToString();
			}
			return m_Tables;
		}

		public override IDbCommand NewCommand()
		{
			return new OleDbCommand(string.Empty,this._Connection);
		}

		public override IDbCommand NewCommand(string i_SQLCommand)
		{
			return new OleDbCommand(i_SQLCommand,this._Connection);
		}

		public override IDbDataAdapter NewDataAdapter()
		{
			return new OleDbDataAdapter(string.Empty,this._Connection);
		}

		public override IDbDataAdapter NewDataAdapter(string i_SQLCommand)
		{
			return new OleDbDataAdapter(i_SQLCommand,this._Connection);
		}

		public override IDbDataParameter NewParameter(string i_paraName, object i_value)
		{
			return base.NewParameter (i_paraName, i_value);
		}

		public override void UpdateDataTable(DataTable i_dataTable, string i_CMD)
		{
			this._DefaultDBCommand.Connection	= this._Connection;
			this._DefaultDataAdapter.SelectCommand = this._DefaultDBCommand;			
			new OleDbCommandBuilder(this._DefaultDataAdapter);
			this._DefaultDataAdapter.Update(i_dataTable);		
		}

		public override IDbConnection Connection
		{
			get
			{
				return this._Connection;
			}
		}

		public override int ExecuteNonQuery(string i_CMD)
		{	
			this._DefaultDBCommand.Connection = this._Connection;
			this._DefaultDBCommand.CommandText = i_CMD;
			return this._DefaultDBCommand.ExecuteNonQuery();
		}

		public override void Fill(DataSet i_DataSet, string i_CMD)
		{
			this._DefaultDBCommand.Connection = this._Connection;
			this._DefaultDBCommand.CommandText = i_CMD;
			this._DefaultDataAdapter.SelectCommand = this._DefaultDBCommand;
			this._DefaultDataAdapter.Fill(i_DataSet);
		}

		public override void Dispose()
		{
			if(this._Connection!=null){this._Connection.Dispose();this._Connection = null;}
			if(this._DefaultDataAdapter!=null){this._DefaultDataAdapter.Dispose();this._DefaultDataAdapter = null;}
			if(this._DefaultDBCommand!=null){this._DefaultDBCommand.Dispose();this._DefaultDBCommand = null;}
			base.Dispose();
		}
	}
	#endregion

	#region public class SQLDBManager
	/*Descrition:   */
	public class SQLDBManager : DBManager
	{
		//Wu.Country@2007-10-30 03:09 PM added this class.
		//Fields
		protected SqlCommand _DefaultDBCommand;
		protected SqlDataAdapter _DefaultDataAdapter;
		protected SqlConnection _Connection;
		//Properties

		//ctor
		public SQLDBManager()
		{
			this._DBConnType = DBConnTypes.SQLDB;

		}
		public SQLDBManager(string i_ConnString)
		{
			this._DBConnType = DBConnTypes.SQLDB;
			this._ConnString = i_ConnString;
			this.Initialize();
		}

		protected override void Initialize()
		{
			this._Connection = new SqlConnection(this._ConnString);
			this._DefaultDBCommand = new SqlCommand();
			this._DefaultDataAdapter = new SqlDataAdapter();
			this._DefaultDBCommand.Connection = this._Connection;
			//
			this._Connection.Open();
		}

		//Methods
		public override IDbConnection Connection
		{
			get
			{
				return this._Connection;
			}
		}

		public override void CreateNewConn(string i_connStr)
		{
			this.Dispose();
			this._ConnString = i_connStr;
			this.Initialize();
		}

		public override void Dispose()
		{
			if(this._Connection!=null){this._Connection.Dispose();this._Connection = null;}
			if(this._DefaultDataAdapter!=null){this._DefaultDataAdapter.Dispose();this._DefaultDataAdapter = null;}
			if(this._DefaultDBCommand!=null){this._DefaultDBCommand.Dispose();this._DefaultDBCommand = null;}
			base.Dispose ();
		}

		public override int ExecuteNonQuery(string i_CMD)
		{
			this._DefaultDBCommand.CommandText = i_CMD;
			this._DefaultDBCommand.Connection = this._Connection;
			return this._DefaultDBCommand.ExecuteNonQuery();			
		}
		
		public override void Fill(DataSet i_DataSet, string i_CMD)
		{			
			this._DefaultDBCommand.CommandText = i_CMD;
			this._DefaultDBCommand.Connection = this._Connection;
			this._DefaultDataAdapter.SelectCommand = this._DefaultDBCommand;
			this._DefaultDataAdapter.Fill(i_DataSet);
		}

		public override void FillDataTable(DataTable i_dataTable, string i_CMD)
		{
			this._DefaultDBCommand.CommandText = i_CMD;
			this._DefaultDBCommand.Connection = this._Connection;
			this._DefaultDataAdapter.SelectCommand = this._DefaultDBCommand;
			this._DefaultDataAdapter.Fill(i_dataTable);			
		}

		public override void GetTableScheam(DataTable i_dataTable, string i_CMD)
		{
			this._DefaultDBCommand.CommandText = i_CMD;
			this._DefaultDBCommand.Connection = this._Connection;
			this._DefaultDataAdapter.SelectCommand = this._DefaultDBCommand;
			this._DefaultDataAdapter.FillSchema(i_dataTable,SchemaType.Mapped);
		}

		public override string[] LoadTalbes()
		{
			OleDbConnection m_OleConn = new OleDbConnection(this.Connection.ConnectionString);
			m_OleConn.Open();
			DataTable m_SchemaTable = m_OleConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] {null, null, null, "TABLE"});
			string[] m_Tables = new string[m_SchemaTable.Rows.Count];
			//TABLE_CATALOG,TABLE_SCHEMA,TABLE_NAME,TABLE_TYPE
			int m_Index = 0;
			foreach(DataRow m_Row in m_SchemaTable.Rows)
			{
				m_Tables[m_Index++] = m_Row["TABLE_NAME"].ToString();
			}
			return m_Tables;
		}

		public override IDbCommand NewCommand()
		{
			return new SqlCommand(string.Empty,this._Connection);
		}

		public override IDbCommand NewCommand(string i_SQLCommand)
		{
			return new SqlCommand(i_SQLCommand,this._Connection);
		}

		public override IDbDataAdapter NewDataAdapter()
		{
			return new SqlDataAdapter(string.Empty,this._Connection);
		}

		public override IDbDataAdapter NewDataAdapter(string i_SQLCommand)
		{
			return new SqlDataAdapter(i_SQLCommand,this._Connection);
		}

		public override IDbDataParameter NewParameter(string i_paraName, object i_value)
		{
			return base.NewParameter(i_paraName, i_value);
		}

		public override void UpdateDataTable(DataTable i_dataTable, string i_CMD)
		{
			this._DefaultDBCommand.CommandText = i_CMD;
			this._DefaultDBCommand.Connection = this._Connection;
			this._DefaultDataAdapter.SelectCommand = this._DefaultDBCommand;
			new SqlCommandBuilder(this._DefaultDataAdapter);
			this._DefaultDataAdapter.Update(i_dataTable);
		}
	}
	#endregion
}
