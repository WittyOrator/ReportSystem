using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections.Specialized;
using System.Text;
using Webb.Utilities;


namespace Webb.Reports.DataProvider.UI
{
	/// <summary>
	/// Summary description for ConfigComonTable.
	/// </summary>
	public class ConfigComonTable: Webb.Utilities.Wizards.WinzardControlBase
	{
		private System.Windows.Forms.Panel palTable;
		private System.Windows.Forms.Label lblTable;
		private System.Windows.Forms.CheckedListBox checkedListTable;

		private SqlConnection AvaliableConnecton=null;

		private string  lastConnectionString=string.Empty;



		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Panel palMain;
		private System.Windows.Forms.Button BtnRemove;
		private System.Windows.Forms.Button BtnAdd;
		private System.Windows.Forms.Panel palFieldRelation;
		private System.Windows.Forms.Label lblSetJoin;
		private System.Windows.Forms.CheckedListBox checkedListFields;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TextBox txtSql;
		private System.Windows.Forms.Button BtnEditSql;
		private System.Windows.Forms.Button BtnInsertNew;		
		private System.Windows.Forms.Button BtnExport;
		private System.Windows.Forms.Button BtnImport;
		private System.Windows.Forms.Panel palColumns;
		private System.Windows.Forms.Panel palButtonsForRelation;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button BtnNewTable;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button BtnUnChekced;
		private System.Windows.Forms.ComboBox cmbTables;
		private System.Windows.Forms.Button BTnEditInsetTable;

		private ArrayList AvaliableFieldList=new ArrayList();

		
		private void InitializeComponent()
		{
			this.palTable = new System.Windows.Forms.Panel();
			this.BTnEditInsetTable = new System.Windows.Forms.Button();
			this.BtnNewTable = new System.Windows.Forms.Button();
			this.checkedListTable = new System.Windows.Forms.CheckedListBox();
			this.lblTable = new System.Windows.Forms.Label();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.palMain = new System.Windows.Forms.Panel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.BtnImport = new System.Windows.Forms.Button();
			this.BtnExport = new System.Windows.Forms.Button();
			this.BtnEditSql = new System.Windows.Forms.Button();
			this.txtSql = new System.Windows.Forms.TextBox();
			this.palColumns = new System.Windows.Forms.Panel();
			this.cmbTables = new System.Windows.Forms.ComboBox();
			this.BtnUnChekced = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.BtnInsertNew = new System.Windows.Forms.Button();
			this.checkedListFields = new System.Windows.Forms.CheckedListBox();
			this.palButtonsForRelation = new System.Windows.Forms.Panel();
			this.BtnAdd = new System.Windows.Forms.Button();
			this.BtnRemove = new System.Windows.Forms.Button();
			this.lblSetJoin = new System.Windows.Forms.Label();
			this.palFieldRelation = new System.Windows.Forms.Panel();
			this.palTable.SuspendLayout();
			this.palMain.SuspendLayout();
			this.panel1.SuspendLayout();
			this.palColumns.SuspendLayout();
			this.palButtonsForRelation.SuspendLayout();
			this.SuspendLayout();
			// 
			// palTable
			// 
			this.palTable.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.palTable.Controls.Add(this.BTnEditInsetTable);
			this.palTable.Controls.Add(this.BtnNewTable);
			this.palTable.Controls.Add(this.checkedListTable);
			this.palTable.Controls.Add(this.lblTable);
			this.palTable.Dock = System.Windows.Forms.DockStyle.Left;
			this.palTable.Location = new System.Drawing.Point(0, 0);
			this.palTable.Name = "palTable";
			this.palTable.Size = new System.Drawing.Size(216, 480);
			this.palTable.TabIndex = 0;
			// 
			// BTnEditInsetTable
			// 
			this.BTnEditInsetTable.Enabled = false;
			this.BTnEditInsetTable.Location = new System.Drawing.Point(144, 48);
			this.BTnEditInsetTable.Name = "BTnEditInsetTable";
			this.BTnEditInsetTable.Size = new System.Drawing.Size(40, 24);
			this.BTnEditInsetTable.TabIndex = 4;
			this.BTnEditInsetTable.Text = "Edit";
			this.BTnEditInsetTable.Click += new System.EventHandler(this.BTnEditInsetTable_Click);
			// 
			// BtnNewTable
			// 
			this.BtnNewTable.Location = new System.Drawing.Point(8, 48);
			this.BtnNewTable.Name = "BtnNewTable";
			this.BtnNewTable.Size = new System.Drawing.Size(112, 24);
			this.BtnNewTable.TabIndex = 3;
			this.BtnNewTable.Text = "Add inset-table";
			this.BtnNewTable.Click += new System.EventHandler(this.BtnNewTable_Click);
			// 
			// checkedListTable
			// 
			this.checkedListTable.CheckOnClick = true;
			this.checkedListTable.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.checkedListTable.Location = new System.Drawing.Point(0, 83);
			this.checkedListTable.Name = "checkedListTable";
			this.checkedListTable.Size = new System.Drawing.Size(214, 395);
			this.checkedListTable.Sorted = true;
			this.checkedListTable.TabIndex = 1;
			this.checkedListTable.SelectedIndexChanged += new System.EventHandler(this.checkedListTable_SelectedIndexChanged);
			// 
			// lblTable
			// 
			this.lblTable.Dock = System.Windows.Forms.DockStyle.Top;
			this.lblTable.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblTable.Location = new System.Drawing.Point(0, 0);
			this.lblTable.Name = "lblTable";
			this.lblTable.Size = new System.Drawing.Size(214, 32);
			this.lblTable.TabIndex = 0;
			this.lblTable.Text = "  Please select the table you want to display";
			// 
			// splitter1
			// 
			this.splitter1.Location = new System.Drawing.Point(216, 0);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(8, 480);
			this.splitter1.TabIndex = 1;
			this.splitter1.TabStop = false;
			// 
			// palMain
			// 
			this.palMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.palMain.Controls.Add(this.panel1);
			this.palMain.Controls.Add(this.palButtonsForRelation);
			this.palMain.Controls.Add(this.palFieldRelation);
			this.palMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.palMain.Location = new System.Drawing.Point(224, 0);
			this.palMain.Name = "palMain";
			this.palMain.Size = new System.Drawing.Size(566, 480);
			this.palMain.TabIndex = 3;
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.BtnImport);
			this.panel1.Controls.Add(this.BtnExport);
			this.panel1.Controls.Add(this.BtnEditSql);
			this.panel1.Controls.Add(this.txtSql);
			this.panel1.Controls.Add(this.palColumns);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(564, 310);
			this.panel1.TabIndex = 7;
			// 
			// BtnImport
			// 
			this.BtnImport.Location = new System.Drawing.Point(16, 128);
			this.BtnImport.Name = "BtnImport";
			this.BtnImport.Size = new System.Drawing.Size(128, 24);
			this.BtnImport.TabIndex = 16;
			this.BtnImport.Text = "Import SQL";
			this.BtnImport.Visible = false;
			this.BtnImport.Click += new System.EventHandler(this.BtnImport_Click);
			// 
			// BtnExport
			// 
			this.BtnExport.Location = new System.Drawing.Point(16, 80);
			this.BtnExport.Name = "BtnExport";
			this.BtnExport.Size = new System.Drawing.Size(128, 24);
			this.BtnExport.TabIndex = 15;
			this.BtnExport.Text = "Export SQL";
			this.BtnExport.Click += new System.EventHandler(this.BtnExport_Click);
			// 
			// BtnEditSql
			// 
			this.BtnEditSql.Location = new System.Drawing.Point(32, 8);
			this.BtnEditSql.Name = "BtnEditSql";
			this.BtnEditSql.Size = new System.Drawing.Size(88, 32);
			this.BtnEditSql.TabIndex = 14;
			this.BtnEditSql.Text = "Edit SQL";
			this.BtnEditSql.Click += new System.EventHandler(this.BtnEditSql_Click);
			// 
			// txtSql
			// 
			this.txtSql.Dock = System.Windows.Forms.DockStyle.Right;
			this.txtSql.Location = new System.Drawing.Point(154, 0);
			this.txtSql.Multiline = true;
			this.txtSql.Name = "txtSql";
			this.txtSql.ReadOnly = true;
			this.txtSql.Size = new System.Drawing.Size(408, 156);
			this.txtSql.TabIndex = 13;
			this.txtSql.Text = "";
			// 
			// palColumns
			// 
			this.palColumns.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.palColumns.Controls.Add(this.cmbTables);
			this.palColumns.Controls.Add(this.BtnUnChekced);
			this.palColumns.Controls.Add(this.label2);
			this.palColumns.Controls.Add(this.label1);
			this.palColumns.Controls.Add(this.BtnInsertNew);
			this.palColumns.Controls.Add(this.checkedListFields);
			this.palColumns.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.palColumns.Location = new System.Drawing.Point(0, 156);
			this.palColumns.Name = "palColumns";
			this.palColumns.Size = new System.Drawing.Size(562, 152);
			this.palColumns.TabIndex = 12;
			// 
			// cmbTables
			// 
			this.cmbTables.Location = new System.Drawing.Point(264, 8);
			this.cmbTables.Name = "cmbTables";
			this.cmbTables.Size = new System.Drawing.Size(176, 22);
			this.cmbTables.TabIndex = 16;
			this.cmbTables.SelectedIndexChanged += new System.EventHandler(this.cmbTables_SelectedIndexChanged);
			// 
			// BtnUnChekced
			// 
			this.BtnUnChekced.Location = new System.Drawing.Point(448, 8);
			this.BtnUnChekced.Name = "BtnUnChekced";
			this.BtnUnChekced.Size = new System.Drawing.Size(96, 24);
			this.BtnUnChekced.TabIndex = 15;
			this.BtnUnChekced.Text = "UnChecked";
			this.BtnUnChekced.Click += new System.EventHandler(this.BtnUnselect_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(200, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(72, 16);
			this.label2.TabIndex = 14;
			this.label2.Text = "Scroll to";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(24, 40);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(168, 16);
			this.label1.TabIndex = 13;
			this.label1.Text = "Fields you want to use ";
			// 
			// BtnInsertNew
			// 
			this.BtnInsertNew.Location = new System.Drawing.Point(16, 80);
			this.BtnInsertNew.Name = "BtnInsertNew";
			this.BtnInsertNew.Size = new System.Drawing.Size(168, 32);
			this.BtnInsertNew.TabIndex = 12;
			this.BtnInsertNew.Text = "Add column expression";
			this.BtnInsertNew.Click += new System.EventHandler(this.BtnInsertNew_Click);
			// 
			// checkedListFields
			// 
			this.checkedListFields.CheckOnClick = true;
			this.checkedListFields.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.checkedListFields.HorizontalScrollbar = true;
			this.checkedListFields.Location = new System.Drawing.Point(200, 40);
			this.checkedListFields.Name = "checkedListFields";
			this.checkedListFields.Size = new System.Drawing.Size(352, 106);
			this.checkedListFields.Sorted = true;
			this.checkedListFields.TabIndex = 4;
			this.checkedListFields.SelectedValueChanged += new System.EventHandler(this.checkedListFields_SelectedValueChanged);
			// 
			// palButtonsForRelation
			// 
			this.palButtonsForRelation.Controls.Add(this.BtnAdd);
			this.palButtonsForRelation.Controls.Add(this.BtnRemove);
			this.palButtonsForRelation.Controls.Add(this.lblSetJoin);
			this.palButtonsForRelation.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.palButtonsForRelation.Location = new System.Drawing.Point(0, 310);
			this.palButtonsForRelation.Name = "palButtonsForRelation";
			this.palButtonsForRelation.Size = new System.Drawing.Size(564, 64);
			this.palButtonsForRelation.TabIndex = 6;
			// 
			// BtnAdd
			// 
			this.BtnAdd.Location = new System.Drawing.Point(56, 32);
			this.BtnAdd.Name = "BtnAdd";
			this.BtnAdd.Size = new System.Drawing.Size(112, 24);
			this.BtnAdd.TabIndex = 2;
			this.BtnAdd.Text = "Add Relation";
			this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
			// 
			// BtnRemove
			// 
			this.BtnRemove.Location = new System.Drawing.Point(288, 32);
			this.BtnRemove.Name = "BtnRemove";
			this.BtnRemove.Size = new System.Drawing.Size(120, 24);
			this.BtnRemove.TabIndex = 3;
			this.BtnRemove.Text = "Remove Relation";
			this.BtnRemove.Click += new System.EventHandler(this.BtnRemove_Click);
			// 
			// lblSetJoin
			// 
			this.lblSetJoin.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblSetJoin.Location = new System.Drawing.Point(40, 8);
			this.lblSetJoin.Name = "lblSetJoin";
			this.lblSetJoin.Size = new System.Drawing.Size(440, 24);
			this.lblSetJoin.TabIndex = 0;
			this.lblSetJoin.Text = "Please set the relation of tables you want to display ";
			// 
			// palFieldRelation
			// 
			this.palFieldRelation.AutoScroll = true;
			this.palFieldRelation.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.palFieldRelation.Location = new System.Drawing.Point(0, 374);
			this.palFieldRelation.Name = "palFieldRelation";
			this.palFieldRelation.Size = new System.Drawing.Size(564, 104);
			this.palFieldRelation.TabIndex = 1;
			// 
			// ConfigComonTable
			// 
			this.Controls.Add(this.palMain);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.palTable);
			this.Name = "ConfigComonTable";
			this.palTable.ResumeLayout(false);
			this.palMain.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.palColumns.ResumeLayout(false);
			this.palButtonsForRelation.ResumeLayout(false);
			this.ResumeLayout(false);

		}
	
		public ConfigComonTable()
		{
			InitializeComponent();

			
			this.WizardTitle = "Step 3: Set SQL String ";
			
		}
		public  bool UpdateConfig(DBSourceConfig _DBSourceConfig)
		{
			_DBSourceConfig.DefaultSQLCmd=this.txtSql.Text.Trim();

			string lowerWord=_DBSourceConfig.DefaultSQLCmd.ToLower();

			bool unsafeWords=(lowerWord.IndexOf("delete ")>=0);
			unsafeWords=unsafeWords||(lowerWord.IndexOf("drop ")>=0);
			unsafeWords=unsafeWords||(lowerWord.IndexOf("create ")>=0);
			unsafeWords=unsafeWords||(lowerWord.IndexOf("update ")>=0);
			unsafeWords=unsafeWords||(lowerWord.IndexOf("insert ")>=0);
			unsafeWords=unsafeWords||(lowerWord.IndexOf("alter table ")>=0);

			if(unsafeWords)
			{
				Webb.Utilities.MessageBoxEx.ShowError("Unsafety SQL statements to change datastructure for database.");
				
				return false;
			}

			if(_DBSourceConfig.DefaultSQLCmd==string.Empty)
			{
				Webb.Utilities.MessageBoxEx.ShowError("Failed to load data ,please check the config.");
				
				return false;
			}
			return true;
		}
		private void ClearAllSetting()
		{
			this.checkedListTable.Items.Clear();

			this.checkedListFields.Items.Clear();

			this.palFieldRelation.Controls.Clear();

			this.txtSql.Text=string.Empty;
		}
		public bool SetConfig(DBSourceConfig dbSourceConfig)
		{
			this.FinishControl = true;
			this.LastControl = true;
			this.SelectStep = false;

			if(dbSourceConfig.ConnString==this.lastConnectionString)return true;

			ClearAllSetting();
			
			AvaliableConnecton = new System.Data.SqlClient.SqlConnection(dbSourceConfig.ConnString); 			
			
			Webb.Utilities.WaitingForm.SetWaitingMessage("Connecting database and reading data, Please wait..");
		        
			try
			{
                SqlCommand cmd=new SqlCommand(TableExpression.GetTabelSQL,AvaliableConnecton);					
			
				AvaliableConnecton.Open();				

				SqlDataReader dataReader=cmd.ExecuteReader();

				while(dataReader.Read())
				{
					string tableName=dataReader["name"].ToString();

					TableExpression expresstion=new TableExpression(tableName);

					expresstion.IsExpression=false;

					this.checkedListTable.Items.Add(expresstion);
				}

				dataReader.Close();
				
				cmd.Dispose();   

				SqlCommand columnCommand=null;
				
				foreach(TableExpression expression in this.checkedListTable.Items)
				{
					string columnNames=string.Format(TableExpression.GetColumnsSQL,expression.TableName);

					columnCommand=new SqlCommand(columnNames,AvaliableConnecton);					

					SqlDataReader columndataReader=columnCommand.ExecuteReader();

					expression.Fields.Clear();

					while(columndataReader.Read())
					{
						string strName=columndataReader["name"].ToString();

						if(strName==null||strName==string.Empty)continue;

						if(!strName.StartsWith(expression.TableName+"."))
						{
							strName=expression.TableName+"."+strName;
						}					

						ColumnExpression column=new ColumnExpression(strName);

						expression.Fields.Add(column);	
					
					}	
					columndataReader.Close();
				}


				columnCommand.Dispose();		
				
	
				lastConnectionString=dbSourceConfig.ConnString;

				return true;

			}
			catch(System.Exception ex)
			{
				Webb.Utilities.MessageBoxEx.ShowError("Failed to connect datatbase.\n"+ex.Message);

				return false;

			}	
			

		}

		private void cmbTables_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.cmbTables.SelectedIndex<0)return;

			if(this.cmbTables.SelectedIndex==0)
			{
				this.checkedListFields.ClearSelected();

				return;
			}

            int index=this.checkedListFields.FindString(this.cmbTables.Text);

			this.checkedListFields.SelectedIndex=index;

		}

		private ArrayList GetTablelist()
		{
			ArrayList arrList=new ArrayList();

			foreach(TableExpression item in this.checkedListTable.Items)
			{
				if(!item.IsExpression)arrList.Add(item.TableName);
			}
			return arrList;
		}

		private void BtnNewTable_Click(object sender, System.EventArgs e)
		{
			if(this.checkedListTable.Items.Count==0)return;

			ArrayList tablesList=this.GetTablelist();

			FrmTableExpression frmtableExpression=new FrmTableExpression(this.AvaliableConnecton,tablesList);

			ArrayList expressionTableList=new ArrayList();

			foreach(TableExpression item in this.checkedListTable.Items)
			{
				expressionTableList.Add(item.TableName);
			}

            frmtableExpression.SetTablename(expressionTableList);

			if(frmtableExpression.ShowDialog()==DialogResult.OK)
			{
				this.checkedListTable.Items.Add(frmtableExpression.tableExpression,true);	
		         
	            int index= this.checkedListTable.FindString(frmtableExpression.tableExpression.ToString());
                
				this.checkedListTable.SelectedIndex=index;

				this.SetSQLText();
			}

		}

		private void checkedListTable_SelectedIndexChanged(object sender, System.EventArgs e)
		{
            SetEditState();

			this.CheckedTablesChanges();
		}

		private void SetEditState()
		{
			if(this.checkedListTable.SelectedIndex<0)return;

			TableExpression expression=this.checkedListTable.SelectedItem as TableExpression;

			if(expression.IsExpression)
			{
				this.BTnEditInsetTable.Enabled=true;
			}
			else
			{
				this.BTnEditInsetTable.Enabled=false;
			}
		}

		private void BtnImport_Click(object sender, System.EventArgs e)
		{			
			FileDialog fileDialog=new OpenFileDialog();

			fileDialog.Filter="SQL Config Files(*.wqsf,*.sql)|*.wqsf;*.sql";

			if(fileDialog.ShowDialog()==DialogResult.OK)
			{
				try
				{
					System.IO.StreamReader sr=new System.IO.StreamReader(fileDialog.FileName);

				    string strText=sr.ReadToEnd();

					sr.Close();

					this.txtSql.Text=strText;

					Webb.Utilities.MessageBoxEx.ShowMessage("Success to import the SQL statements!");
				}
				catch(Exception ex)
				{
					Webb.Utilities.MessageBoxEx.ShowError("Failed:"+ex.Message);
						
				}
			}


		}


		public void SetSQLText()
		{
			this.txtSql.Text=this.GetSqlResult();
		}
		
		private void CheckedTablesChanges()
		{
			this.checkedListFields.Items.Clear();

			 AvaliableFieldList.Clear();

			this.cmbTables.Items.Clear();

			this.cmbTables.Items.Add(string.Empty);

			foreach(TableExpression item in this.checkedListTable.CheckedItems)
			{					
				
					ArrayList fields=(item as TableExpression).Fields;

					foreach(ColumnExpression column in fields)
					{
						this.checkedListFields.Items.Add(column.ColumnName,true);

						AvaliableFieldList.Add(column.ColumnName);
					}	

					this.cmbTables.Items.Add(item.TableName);					
			}

			this.palFieldRelation.Controls.Clear();		
		
			this.SetSQLText();
		}
	
		private string GetSqlResult()
		{			
			StringBuilder tables=new StringBuilder();			

			ArrayList CheckedAllFieldsForTable=new ArrayList();	

			ArrayList NotCheckedFields=new ArrayList();	
		
			for(int i=0;i<this.checkedListFields.Items.Count;i++)
			{
				if(!this.checkedListFields.CheckedIndices.Contains(i))NotCheckedFields.Add(checkedListFields.Items[i].ToString());                   
			}

			foreach(TableExpression item in this.checkedListTable.CheckedItems)
			{
				if(item.IsExpression)
				{
					tables.Append(item.SQLStatement+",");
				}
				else
				{
					tables.Append(item.TableName+",");
				}

				string strTable=item.TableName;				

				bool nocontained=true;

				foreach(string strField in NotCheckedFields)
				{
					if(strField.StartsWith(strTable))
					{
                        nocontained=false;

						break;
					}
				}		
				if(nocontained)
				{
					CheckedAllFieldsForTable.Add(strTable);
				}
			}

			if(tables.Length>0)tables.Remove(tables.Length-1,1); 

			if(tables.Length==0)return string.Empty;

			StringBuilder fields=new StringBuilder();

			foreach(string strTable in CheckedAllFieldsForTable)
			{
				fields.Append(strTable+".*,");
			}

			foreach(object column in this.checkedListFields.CheckedItems)
			{
				string strField=column.ToString();

				int index=strField.IndexOf(".");

				if(index>0&&!strField.StartsWith("("))
				{ 
					string strtable=strField.Substring(0,index);

					if(CheckedAllFieldsForTable.Contains(strtable))continue;
				}
				
				fields.Append(strField+",");			
			
			}

			if(fields.Length>0)fields.Remove(fields.Length-1,1); 

			if(fields.Length==0)return string.Empty;

			StringBuilder conditions=new StringBuilder();
            
			foreach(MyRelationControl control in this.palFieldRelation.Controls)
            {
				string strResult=control.GetResult();
				
				if(strResult!=string.Empty)conditions.Append(strResult);
			}

			string SqlResult=string.Empty;

			if(conditions.Length>0)
			{
				if(conditions.ToString().EndsWith(" and "))
				{
					conditions.Remove(conditions.Length-5,5); 
				}
				else if(conditions.ToString().EndsWith(" or "))
				{
					conditions.Remove(conditions.Length-4,4);
				}

				SqlResult=string.Format("select {0} from {1} where {2}",fields,tables,conditions);
			}
			else
			{
				SqlResult=string.Format("select {0} from {1}",fields,tables);
			}

			return SqlResult;
		}



		private void BtnAdd_Click(object sender, System.EventArgs e)
		{
			MyRelationControl relationControl=new MyRelationControl();

            relationControl.SetData(this.AvaliableFieldList);

			relationControl.ParentConfig=this;

			this.palFieldRelation.Controls.Add(relationControl);

			relationControl.Dock=DockStyle.Top;

			relationControl.BringToFront();

		}
		private void BtnRemove_Click(object sender, System.EventArgs e)
		{
			if(this.palFieldRelation.Controls.Count==0)return;

			this.palFieldRelation.Controls.RemoveAt(0);

			this.SetSQLText();
		}

		
		private void BtnSelectAll_Click(object sender, System.EventArgs e)
		{
			for(int i=0;i<this.checkedListFields.Items.Count;i++)
			{
				this.checkedListFields.SetItemChecked(i,true);
			}
		    this.SetSQLText();
		}

		private void BtnUnselecttable_Click(object sender, System.EventArgs e)
		{
			for(int i=0;i<this.checkedListTable.Items.Count;i++)
			{
				this.checkedListTable.SetItemChecked(i,false);
			}
			for(int i=0;i<this.checkedListFields.Items.Count;i++)
			{
				this.checkedListFields.SetItemChecked(i,false);
			}
			this.palFieldRelation.Controls.Clear();

			this.SetSQLText();
		}	

		private void BtnInsertNew_Click(object sender, System.EventArgs e)
		{			
			FrmExpression frmExpressrion=new FrmExpression(AvaliableFieldList);

			if(frmExpressrion.ShowDialog()==DialogResult.OK)
			{
			    this.checkedListFields.Items.Add(frmExpressrion.strExpression,true);
			    
				int index= this.checkedListFields.FindString(frmExpressrion.strExpression);
   
				this.checkedListFields.SelectedIndex=index;

				this.SetSQLText();
			}
		}	
		

		private void BtnEditSql_Click(object sender, System.EventArgs e)
		{
			if(this.BtnEditSql.Text=="Edit SQL")
			{
				this.txtSql.ReadOnly=false;
				
                this.BtnEditSql.Text="End Edit";

				this.palFieldRelation.Visible=false;

				this.palButtonsForRelation.Visible=false;

				this.palColumns.Visible=false;

				this.BtnImport.Visible=true;

				this.palTable.Enabled=false;
			}
			else
			{
				this.txtSql.ReadOnly=true;

				this.BtnEditSql.Text="Edit SQL";

				this.palFieldRelation.Visible=true;

				this.palButtonsForRelation.Visible=true;

				this.palColumns.Visible=true;

				this.BtnImport.Visible=false;

				this.palTable.Enabled=true;
			}
		}
		

		private void checkedListFields_SelectedValueChanged(object sender, System.EventArgs e)
		{
			this.SetSQLText();
		}


		private void BtnUnselect_Click(object sender, System.EventArgs e)
		{
			if(this.checkedListFields.SelectedIndex<0)
			{
				for(int i=0;i<this.checkedListFields.Items.Count;i++)
				{				
					this.checkedListFields.SetItemChecked(i,false);
				}
			}
			else
			{
				string strText=this.checkedListFields.SelectedItem.ToString();

				int index=strText.IndexOf(".");

				if(index<0)return;

				strText=strText.Substring(0,index);
                
				for(int i=0;i<this.checkedListFields.Items.Count;i++)
				{	
					if(this.checkedListFields.Items[i].ToString().StartsWith(strText))
					{
						this.checkedListFields.SetItemChecked(i,false);
					}
				}
			}
			this.SetSQLText();		
		}

		private void BtnExport_Click(object sender, System.EventArgs e)
		{
			if(this.txtSql.Text==string.Empty)return;
		
			 FileDialog fileDialog=new  SaveFileDialog();

			fileDialog.Filter="SQL Config Files(*.wqsf)|*.wqsf";

			if(fileDialog.ShowDialog()==DialogResult.OK)
			{
				try
				{
					System.IO.StreamWriter sw=new System.IO.StreamWriter(fileDialog.FileName);

					sw.Write(this.txtSql.Text);

					sw.Close();

					string strMessage=string.Format("Success to export the SQL statements to file below:\n'{0}'",fileDialog.FileName);

					Webb.Utilities.MessageBoxEx.ShowMessage(strMessage);
				}
				catch(Exception ex)
				{
					Webb.Utilities.MessageBoxEx.ShowError("Failed:"+ex.Message);
				}
			}
		}

		private void CreateReleation(string strRelation)
		{
			MyRelationControl relationControl=new MyRelationControl();

			relationControl.SetData(this.AvaliableFieldList);

			relationControl.ParentConfig=this;

			this.palFieldRelation.Controls.Add(relationControl);

			relationControl.Dock=DockStyle.Top;

		}

		private void BTnEditInsetTable_Click(object sender, System.EventArgs e)
		{
			if(this.checkedListTable.SelectedItem==null)return;

			TableExpression tableExpression=this.checkedListTable.SelectedItem as TableExpression;

			FrmTableExpression frmtableExpression=new FrmTableExpression(this.AvaliableConnecton,this.GetTablelist(),tableExpression);

			ArrayList expressionTableList=new ArrayList();

			foreach(TableExpression item in this.checkedListTable.Items)
			{
				if(item!=tableExpression)expressionTableList.Add(item.TableName);
			}

			int index=this.checkedListTable.SelectedIndex;

            this.checkedListTable.SelectedIndex=-1;

			frmtableExpression.SetTablename(expressionTableList);

			if(frmtableExpression.ShowDialog()==DialogResult.OK)
			{
				 this.checkedListTable.Refresh();

                 this.checkedListTable.SelectedIndex=index;
			}
		}

	}
}
