using System;
using System.Drawing;

using System.Data;   
using System.IO;  
using Webb.Collections;
using Webb.Data;

using System.Runtime.Serialization;   
using Webb.Reports.DataProvider;
using System.Windows.Forms;
using System.Collections;

namespace Webb.Reports
{
	[Serializable]
	public class ColumnExpression:ISerializable
	{
		protected string _ColumnName=string.Empty;
		protected string _SQLStatement=string.Empty;
		protected bool _IsExpression=false;


		public ColumnExpression(string columnName)
		{
			this._ColumnName=columnName;
		}
		public override string ToString()
		{
			if(!_IsExpression)
			{
				return _ColumnName;
			}

			return _SQLStatement;
		}

		public string ColumnName
		{
			get{ return _ColumnName; }
			set{ _ColumnName = value; }
		}

		public string SQLStatement
		{
			get{ return _SQLStatement; }
			set{ _SQLStatement = value; }
		}

		public bool IsExpression
		{
			get{ return _IsExpression; }
			set{ _IsExpression = value; }
		}

		#region Serialization By Simon's Macro 2010-2-5 16:02:52
		public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			info.AddValue("_ColumnName",_ColumnName);
			info.AddValue("_SQLStatement",_SQLStatement);
			info.AddValue("_IsExpression",_IsExpression);
		
		}

		public ColumnExpression(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			try
			{
				_ColumnName=info.GetString("_ColumnName");
			}
			catch
			{
				_ColumnName=string.Empty;
			}
			try
			{
				_SQLStatement=info.GetString("_SQLStatement");
			}
			catch
			{
				_SQLStatement=string.Empty;
			}
			try
			{
				_IsExpression=info.GetBoolean("_IsExpression");
			}
			catch
			{
				_IsExpression=false;
			}
		}
		#endregion



	}
	/// <summary>
	/// Summary description for CustomSQLInfo.
	/// </summary>
	[Serializable]
	public class TableExpression:ISerializable
	{
        public const string GetTabelSQL = "select name from [sysobjects] where xtype = 'u' or  type='V'";   // from SQL-Server

        public const string GetColumnsSQL="select name from dbo.syscolumns where id=OBJECT_ID('{0}')";   



		protected string _SQLStatement=string.Empty;
		protected string _TableName=string.Empty;
		protected ArrayList _SourceTables=new ArrayList();
		protected ArrayList _Fields=new ArrayList();
		protected DBFilter _Relations=new DBFilter();
		protected bool _IsExpression=false;
	
	
		public TableExpression(string tableName)
		{
           _TableName=tableName;
		}
	

		public override string ToString()
		{
			if(!_IsExpression)
			{
				return _TableName;
			}

			return _TableName+"(TableExpression)";
		}

		public string SQLStatement
		{
			get{ return _SQLStatement; }
			set{ _SQLStatement = value; }
		}

		public string TableName
		{
			get{ return _TableName; }
			set{ _TableName = value; }
		}

		public System.Collections.ArrayList SourceTables
		{
			get{
				if(_SourceTables==null)_SourceTables=new ArrayList();
				return _SourceTables; }
			set{ _SourceTables = value; }
		}

		public System.Collections.ArrayList Fields
		{
			get{ if(_Fields==null)_Fields=new System.Collections.ArrayList();
				return _Fields; }
			set{ _Fields = value; }
		}

		public DBFilter Relations
		{
			get{ if(_Relations==null)_Relations=new DBFilter();
				return _Relations; }
			set{ _Relations = value; }
		}

		public bool IsExpression
		{
			get{ return _IsExpression; }
			set{ _IsExpression = value; }
		}

		

		#region Save &Load
			public void Save()
			{	
				SaveFileDialog fileDialog=new SaveFileDialog();

				fileDialog.Filter="Inset Tabel Expression(*.itexp)|*.itexp";

				fileDialog.FileName=this.TableName+".itexp";

				if(fileDialog.ShowDialog()!=DialogResult.OK)return;
				
				System.IO.FileStream stream = System.IO.File.Open(fileDialog.FileName,System.IO.FileMode.OpenOrCreate);
		
				try
				{			

					Webb.Utilities.Serializer.SerializeObject(stream,this);
				}
				catch(Exception ex)
				{
					Webb.Utilities.MessageBoxEx.ShowError("Failed to save files!\n"+ex.Message);				
				}
				finally
				{
					stream.Close();
				}
			}	
			public static TableExpression LoadTableExpression()
			{	
				OpenFileDialog fileDialog=new OpenFileDialog();

				fileDialog.Filter="Inset Tabel Expression(*.itexp)|*.itexp";

				if(fileDialog.ShowDialog()!=DialogResult.OK)return null;
				
				System.IO.FileStream stream = System.IO.File.Open(fileDialog.FileName,System.IO.FileMode.Open);

				TableExpression tableExpression=null;

				try
				{		

					tableExpression=Webb.Utilities.Serializer.DeserializeObject(stream) as TableExpression;
				}
				catch(Exception ex)
				{
					Webb.Utilities.MessageBoxEx.ShowError("Failed to save files!\n"+ex.Message);				
				}
				finally
				{
					stream.Close();
				}

				return tableExpression;
			}	

		#endregion

		#region Serialization By Simon's Macro 2010-2-5 14:49:48
		public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			info.AddValue("_SQLStatement",_SQLStatement);
			info.AddValue("_TableName",_TableName);
			info.AddValue("_SourceTables",_SourceTables,typeof(System.Collections.ArrayList));
			info.AddValue("_Fields",_Fields,typeof(System.Collections.ArrayList));
			info.AddValue("_Relations",_Relations,typeof(Webb.Data.DBFilter));
			info.AddValue("_IsExpression",_IsExpression);
		
		}

		public TableExpression(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			try
			{
				_SQLStatement=info.GetString("_SQLStatement");
			}
			catch
			{
				_SQLStatement=string.Empty;
			}
			try
			{
				_TableName=info.GetString("_TableName");
			}
			catch
			{
				_TableName=string.Empty;
			}
			try
			{
				_SourceTables=(System.Collections.ArrayList)info.GetValue("_SourceTables",typeof(System.Collections.ArrayList));
			}
			catch
			{
			}
			try
			{
				_Fields=(System.Collections.ArrayList)info.GetValue("_Fields",typeof(System.Collections.ArrayList));
			}
			catch
			{
			}
			try
			{
				_Relations=(Webb.Data.DBFilter)info.GetValue("_Relations",typeof(Webb.Data.DBFilter));
			}
			catch
			{
			}
			try
			{
				_IsExpression=info.GetBoolean("_IsExpression");
			}
			catch
			{
				_IsExpression=false;
			}
		}
		#endregion

		

	}



}
