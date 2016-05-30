using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using System.Data.SqlClient;

namespace Webb.Reports.DataProvider.UI
{
	/// <summary>
	/// Summary description for FrmTableExpression.
	/// </summary>
	public class FrmTableExpression : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ComboBox cmbFirstTable;
		private System.Windows.Forms.Label lblJoin;
		private System.Windows.Forms.ComboBox cmbSecondTable;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtTableName;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button BtnOk;
		private System.Windows.Forms.Button BtnCancel;
		private System.Windows.Forms.ListBox lstAll;
		private System.Windows.Forms.ListBox lstSelected;
		private System.Windows.Forms.ComboBox cmbSecondRelation;
		private System.Windows.Forms.ComboBox cmbFirstRelation;

		private SqlConnection AvaliableConnection;

		protected ArrayList AvaliableFieldList=new ArrayList();
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button BtnAddColumns;
		private System.Windows.Forms.Button BtnAdditem;
		private System.Windows.Forms.Button BtnRemoveItem;
		private System.Windows.Forms.Button BtnClear;
		private System.Windows.Forms.Button BtnAddAll;

		public TableExpression tableExpression=null;
		private System.Windows.Forms.Button BtnExport;
		private System.Windows.Forms.Button BtnImport;

		private bool AddOrEdit=false;
		private ArrayList avaliableTables=new ArrayList();

		
		public void SetTablename(ArrayList arrTables)
		{
			avaliableTables=arrTables;
		}

		public FrmTableExpression(SqlConnection _AvaliableConnection,ArrayList arrTables)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			this.AvaliableConnection=_AvaliableConnection;

			 SetInitList(arrTables);

			AddOrEdit=false;
			

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}
		public FrmTableExpression(SqlConnection _AvaliableConnection,ArrayList arrTables,TableExpression _TableExpression)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			this.AvaliableConnection=_AvaliableConnection;

			tableExpression=_TableExpression;

             SetInitList(arrTables);

			SetView(tableExpression);

			AddOrEdit=true;

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		private void SetView(TableExpression expression)
		{
			if(expression==null)return;

			this.txtTableName.Text=expression.TableName;

			this.cmbFirstTable.Text=expression.SourceTables[0].ToString();

			this.cmbSecondTable.Text=expression.SourceTables[1].ToString();

			this.lstSelected.Items.Clear();

			this.lstAll.Items.Clear();

			this.cmbFirstRelation.Text=expression.Relations[0].ColumnName;

			this.cmbSecondRelation.Text=expression.Relations[0].Value.ToString();

			foreach(ColumnExpression strField in expression.Fields)
			{
				this.lstSelected.Items.Add(strField.SQLStatement);

			}
			for(int i=this.lstAll.Items.Count-1;i>=0;i--)
			{
				string field=this.lstAll.Items[i].ToString();

				if(this.lstSelected.Items.Contains(field))
				{
					this.lstAll.Items.RemoveAt(i);
				}				
			}

		}
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbSecondTable = new System.Windows.Forms.ComboBox();
            this.lblJoin = new System.Windows.Forms.Label();
            this.cmbFirstTable = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.BtnAddAll = new System.Windows.Forms.Button();
            this.BtnClear = new System.Windows.Forms.Button();
            this.BtnRemoveItem = new System.Windows.Forms.Button();
            this.BtnAdditem = new System.Windows.Forms.Button();
            this.BtnAddColumns = new System.Windows.Forms.Button();
            this.lstAll = new System.Windows.Forms.ListBox();
            this.lstSelected = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbSecondRelation = new System.Windows.Forms.ComboBox();
            this.cmbFirstRelation = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTableName = new System.Windows.Forms.TextBox();
            this.BtnOk = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.BtnExport = new System.Windows.Forms.Button();
            this.BtnImport = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbSecondTable);
            this.groupBox1.Controls.Add(this.lblJoin);
            this.groupBox1.Controls.Add(this.cmbFirstTable);
            this.groupBox1.Location = new System.Drawing.Point(10, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(633, 60);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Related Tables";
            // 
            // cmbSecondTable
            // 
            this.cmbSecondTable.Location = new System.Drawing.Point(346, 26);
            this.cmbSecondTable.Name = "cmbSecondTable";
            this.cmbSecondTable.Size = new System.Drawing.Size(278, 20);
            this.cmbSecondTable.TabIndex = 2;
            this.cmbSecondTable.SelectedIndexChanged += new System.EventHandler(this.cmbSecondTable_SelectedIndexChanged);
            this.cmbSecondTable.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbSecondTable_KeyDown);
            // 
            // lblJoin
            // 
            this.lblJoin.Location = new System.Drawing.Point(317, 31);
            this.lblJoin.Name = "lblJoin";
            this.lblJoin.Size = new System.Drawing.Size(38, 17);
            this.lblJoin.TabIndex = 1;
            this.lblJoin.Text = "Join";
            // 
            // cmbFirstTable
            // 
            this.cmbFirstTable.Location = new System.Drawing.Point(10, 26);
            this.cmbFirstTable.Name = "cmbFirstTable";
            this.cmbFirstTable.Size = new System.Drawing.Size(307, 20);
            this.cmbFirstTable.TabIndex = 0;
            this.cmbFirstTable.SelectedIndexChanged += new System.EventHandler(this.cmbFirstTable_SelectedIndexChanged);
            this.cmbFirstTable.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbFirstTable_KeyDown);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.BtnAddAll);
            this.groupBox2.Controls.Add(this.BtnClear);
            this.groupBox2.Controls.Add(this.BtnRemoveItem);
            this.groupBox2.Controls.Add(this.BtnAdditem);
            this.groupBox2.Controls.Add(this.BtnAddColumns);
            this.groupBox2.Controls.Add(this.lstAll);
            this.groupBox2.Controls.Add(this.lstSelected);
            this.groupBox2.Location = new System.Drawing.Point(10, 69);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(573, 241);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Fields";
            // 
            // BtnAddAll
            // 
            this.BtnAddAll.Location = new System.Drawing.Point(326, 164);
            this.BtnAddAll.Name = "BtnAddAll";
            this.BtnAddAll.Size = new System.Drawing.Size(48, 26);
            this.BtnAddAll.TabIndex = 5;
            this.BtnAddAll.Text = "-->>";
            this.BtnAddAll.Click += new System.EventHandler(this.BtnAddAll_Click);
            // 
            // BtnClear
            // 
            this.BtnClear.Location = new System.Drawing.Point(326, 198);
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.Size = new System.Drawing.Size(48, 26);
            this.BtnClear.TabIndex = 4;
            this.BtnClear.Text = "<<--";
            this.BtnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // BtnRemoveItem
            // 
            this.BtnRemoveItem.Location = new System.Drawing.Point(326, 103);
            this.BtnRemoveItem.Name = "BtnRemoveItem";
            this.BtnRemoveItem.Size = new System.Drawing.Size(48, 26);
            this.BtnRemoveItem.TabIndex = 3;
            this.BtnRemoveItem.Text = "<--";
            this.BtnRemoveItem.Click += new System.EventHandler(this.BtnRemoveItem_Click);
            // 
            // BtnAdditem
            // 
            this.BtnAdditem.Location = new System.Drawing.Point(326, 60);
            this.BtnAdditem.Name = "BtnAdditem";
            this.BtnAdditem.Size = new System.Drawing.Size(48, 26);
            this.BtnAdditem.TabIndex = 2;
            this.BtnAdditem.Text = "-->";
            this.BtnAdditem.Click += new System.EventHandler(this.BtnAdditem_Click);
            // 
            // BtnAddColumns
            // 
            this.BtnAddColumns.Location = new System.Drawing.Point(58, 17);
            this.BtnAddColumns.Name = "BtnAddColumns";
            this.BtnAddColumns.Size = new System.Drawing.Size(230, 26);
            this.BtnAddColumns.TabIndex = 1;
            this.BtnAddColumns.Text = "Join columns into a new column";
            this.BtnAddColumns.Click += new System.EventHandler(this.BtnAddColumns_Click);
            // 
            // lstAll
            // 
            this.lstAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstAll.HorizontalScrollbar = true;
            this.lstAll.ItemHeight = 12;
            this.lstAll.Location = new System.Drawing.Point(10, 52);
            this.lstAll.Name = "lstAll";
            this.lstAll.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstAll.Size = new System.Drawing.Size(189, 172);
            this.lstAll.TabIndex = 0;
            this.lstAll.DoubleClick += new System.EventHandler(this.lstAll_DoubleClick);
            // 
            // lstSelected
            // 
            this.lstSelected.HorizontalScrollbar = true;
            this.lstSelected.ItemHeight = 12;
            this.lstSelected.Location = new System.Drawing.Point(384, 43);
            this.lstSelected.Name = "lstSelected";
            this.lstSelected.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstSelected.Size = new System.Drawing.Size(298, 184);
            this.lstSelected.TabIndex = 0;
            this.lstSelected.DoubleClick += new System.EventHandler(this.lstSelected_DoubleClick);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.cmbSecondRelation);
            this.groupBox3.Controls.Add(this.cmbFirstRelation);
            this.groupBox3.Location = new System.Drawing.Point(10, 310);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(691, 60);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Relation";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(365, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "=";
            // 
            // cmbSecondRelation
            // 
            this.cmbSecondRelation.Location = new System.Drawing.Point(394, 17);
            this.cmbSecondRelation.Name = "cmbSecondRelation";
            this.cmbSecondRelation.Size = new System.Drawing.Size(278, 20);
            this.cmbSecondRelation.TabIndex = 2;
            this.cmbSecondRelation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbSecondRelation_KeyDown);
            // 
            // cmbFirstRelation
            // 
            this.cmbFirstRelation.Location = new System.Drawing.Point(10, 17);
            this.cmbFirstRelation.Name = "cmbFirstRelation";
            this.cmbFirstRelation.Size = new System.Drawing.Size(336, 20);
            this.cmbFirstRelation.TabIndex = 0;
            this.cmbFirstRelation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbFirstRelation_KeyDown);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(77, 379);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 26);
            this.label1.TabIndex = 3;
            this.label1.Text = "New Table Name";
            // 
            // txtTableName
            // 
            this.txtTableName.Location = new System.Drawing.Point(211, 379);
            this.txtTableName.Name = "txtTableName";
            this.txtTableName.Size = new System.Drawing.Size(423, 21);
            this.txtTableName.TabIndex = 4;
            // 
            // BtnOk
            // 
            this.BtnOk.Location = new System.Drawing.Point(432, 422);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(106, 35);
            this.BtnOk.TabIndex = 5;
            this.BtnOk.Text = "OK";
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Location = new System.Drawing.Point(586, 422);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(105, 35);
            this.BtnCancel.TabIndex = 6;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnExport
            // 
            this.BtnExport.Location = new System.Drawing.Point(19, 422);
            this.BtnExport.Name = "BtnExport";
            this.BtnExport.Size = new System.Drawing.Size(77, 35);
            this.BtnExport.TabIndex = 7;
            this.BtnExport.Text = "Export";
            this.BtnExport.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // BtnImport
            // 
            this.BtnImport.Location = new System.Drawing.Point(144, 422);
            this.BtnImport.Name = "BtnImport";
            this.BtnImport.Size = new System.Drawing.Size(96, 35);
            this.BtnImport.TabIndex = 8;
            this.BtnImport.Text = "Import";
            this.BtnImport.Click += new System.EventHandler(this.BtnImport_Click);
            // 
            // FrmTableExpression
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(592, 438);
            this.Controls.Add(this.BtnImport);
            this.Controls.Add(this.BtnExport);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnOk);
            this.Controls.Add(this.txtTableName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FrmTableExpression";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create a temp table  by expression";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion


		private void SetInitList(ArrayList arrTables)
		{
			this.lstAll.Items.Clear();

			this.cmbFirstTable.Items.Clear();

			this.cmbSecondTable.Items.Clear();

			foreach(string strTable in arrTables)
			{
				this.cmbFirstTable.Items.Add(strTable);
				this.cmbSecondTable.Items.Add(strTable);
			}	

		}

		private bool IsTableAllSelected(string strTable)
		{			
			foreach(string strField in AvaliableFieldList)
			{
				if(strField.StartsWith(strTable)&&!this.lstSelected.Items.Contains(strField))
				{
					return false;
				}
			}

			return true;
		}

		private void UpdateExpression(ref TableExpression result)
		{
			#region Check Input

			if(this.cmbFirstTable.Text==string.Empty||this.cmbSecondTable.Text==string.Empty)
			{
				Webb.Utilities.MessageBoxEx.ShowError("Please select the tables");
				
				this.cmbFirstRelation.Focus();

                result=null;

				return;

				
			}			
			
			if(this.lstSelected.Items.Count==0)
			{
				Webb.Utilities.MessageBoxEx.ShowError("Please add the columns!!");
				
				this.lstAll.Focus();

				result=null;

				return;				
			}		   

			if(this.cmbFirstRelation.Text==string.Empty||this.cmbSecondRelation.Text==string.Empty)
			{
				Webb.Utilities.MessageBoxEx.ShowError("Please select the relations");
				
				this.cmbFirstRelation.Focus();

				result=null;

				return;
				
			}
			if(this.txtTableName.Text.Trim()==string.Empty)
			{
				Webb.Utilities.MessageBoxEx.ShowError("Please input the new table Name");
				
				this.txtTableName.Focus();

				result=null;

				return;

				
			}
			#endregion		
	
			result.TableName=this.txtTableName.Text.Trim();

			System.Text.StringBuilder sb=new System.Text.StringBuilder();

			result.SourceTables.Clear();

			result.Fields=new ArrayList();

			#region Table
            result.SourceTables.Add(this.cmbFirstTable.Text);

			result.SourceTables.Add(this.cmbSecondTable.Text);
			#endregion

			bool isFirstTableAllselected=IsTableAllSelected(this.cmbFirstTable.Text);

			bool isSecondTableEdit=IsTableAllSelected(this.cmbSecondTable.Text);

			#region fields are all selected were replaced with .*
			if(isFirstTableAllselected)
			{
				sb.Append(this.cmbFirstTable.Text+".*,");
			}
			if(isSecondTableEdit)
			{
				sb.Append(this.cmbSecondTable.Text+".*,");
			}

			#endregion

			#region Selected Fields
			foreach(string text in this.lstSelected.Items)
			{
				int index=text.IndexOf(" as ");

				string strField=text;

				ColumnExpression column=new ColumnExpression(strField);

				if(index>0)
				{
					#region is new column by expression

					strField=result.TableName+"."+strField.Substring(index+3).Trim(); 	
				
					column.ColumnName=strField;

                    column.SQLStatement=text;

					column.IsExpression=true;

					#endregion
				}
				else
				{
					#region drect
					index=text.IndexOf(".");

					strField=result.TableName+"."+strField.Substring(index+1);

					column.ColumnName=strField;

					column.SQLStatement=text;

					column.IsExpression=false;

					#endregion

				}

				result.Fields.Add(column);

				if(isFirstTableAllselected&&text.StartsWith(this.cmbFirstTable.Text))continue;

				if(isSecondTableEdit&&text.StartsWith(this.cmbSecondTable.Text))continue;

				sb.Append(text+",");
			}

			if(sb.Length>1)sb.Remove(sb.Length-1,1);

			#endregion

			string strTableSQl=string.Format("(select {0} from {1},{2} where {3}={4}) as {5}",sb,
				                             this.cmbFirstTable.Text,this.cmbSecondTable.Text,
				                             this.cmbFirstRelation.Text,this.cmbSecondRelation.Text,
				                              this.txtTableName.Text);

			result.SQLStatement=strTableSQl;

			result.IsExpression=true;
			
			
			Webb.Data.DBCondition condition=new Webb.Data.DBCondition();

			condition.ColumnName=this.cmbFirstRelation.Text;

			condition.Value=this.cmbSecondRelation.Text;

			result.Relations.Add(condition);			 
		}
	
		
		private void BtnOk_Click(object sender, System.EventArgs e)
		{			
			if(!AddOrEdit)
			{
				tableExpression=new TableExpression("");
			}
			if(avaliableTables.Contains(this.txtTableName.Text.Trim()))
			{
				Webb.Utilities.MessageBoxEx.ShowError("The tableName has been existed before!");
			    return;
			}
			
		    this.UpdateExpression(ref tableExpression);

			if(tableExpression==null)return;

			DialogResult=DialogResult.OK;
		
		}

		private void BtnCancel_Click(object sender, System.EventArgs e)
		{
			DialogResult=DialogResult.Cancel;
		}

		private void cmbFirstTable_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.cmbFirstTable.SelectedIndex<0)return;

             CheckedTablesChanges();
		
		}
		private void CheckedTablesChanges()
		{	
			if(this.cmbFirstTable.SelectedIndex<0||this.cmbSecondTable.SelectedIndex<0)return;

			Webb.Utilities.WaitingForm.ShowWaitingForm();

			Webb.Utilities.WaitingForm.SetWaitingMessage("Reading data from database, Please wait..");

			this.lstAll.Items.Clear();

			this.cmbFirstRelation.Items.Clear();

			this.cmbSecondRelation.Items.Clear();

			AvaliableFieldList.Clear();

			this.lstSelected.Items.Clear();

			try
			{
				SqlCommand cmd;								

				string strTable=this.cmbFirstTable.Text;

				if(strTable!=string.Empty)
				{				
					string columnCommnd=string.Format(TableExpression.GetColumnsSQL,strTable);
					
					cmd=new SqlCommand(columnCommnd,AvaliableConnection);					

					SqlDataReader dataReader=cmd.ExecuteReader();

					while(dataReader.Read())
					{
						string strName=dataReader["name"].ToString();

						if(strName!=string.Empty&&!strName.StartsWith(strTable+"."))
						{
							strName=strTable+"."+strName;
						}						

						this.lstAll.Items.Add(strName);
						
						this.cmbFirstRelation.Items.Add(strName);

						AvaliableFieldList.Add(strName);
					}

					dataReader.Close();	
				
					cmd.Dispose();
				}
				strTable=this.cmbSecondTable.Text;

				if(strTable!=string.Empty)
				{				
					string columnCommnd=string.Format(TableExpression.GetColumnsSQL,strTable);
					
					cmd=new SqlCommand(columnCommnd,this.AvaliableConnection);					

					SqlDataReader dataReader=cmd.ExecuteReader();

					while(dataReader.Read())
					{
						string strName=dataReader["name"].ToString();

						if(strName!=string.Empty&&!strName.StartsWith(strTable+"."))
						{
							strName=strTable+"."+strName;
						}

						this.lstAll.Items.Add(strName);

						this.cmbSecondRelation.Items.Add(strName);

						AvaliableFieldList.Add(strName);
					}

					dataReader.Close();	
				
					cmd.Dispose();
				}

				Webb.Utilities.WaitingForm.CloseWaitingForm();	

			}
			catch(Exception ex)
			{
				Webb.Utilities.WaitingForm.CloseWaitingForm();	

				Webb.Utilities.MessageBoxEx.ShowError(ex.Message);				
			}
		
		}

		private void cmbSecondTable_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.cmbFirstTable.SelectedIndex<0)return;

			CheckedTablesChanges();
		}

		private void BtnAdditem_Click(object sender, System.EventArgs e)
		{
			Additem();
		}
		private void Additem()
		{
			if(this.lstAll.SelectedItems.Count==0)return;

			foreach(string strField in this.lstAll.SelectedItems)
			{
				this.lstSelected.Items.Add(strField);
			}
			for(int i=this.lstAll.Items.Count-1;i>=0;i--)
			{
				string field=this.lstAll.Items[i].ToString();

				if(this.lstSelected.Items.Contains(field))
				{
					this.lstAll.Items.RemoveAt(i);
				}
				
			}
		}
		private void RemoveItem()
		{
			if(this.lstSelected.SelectedItems.Count==0)return;

			foreach(string strField in this.lstSelected.SelectedItems)
			{
				this.lstAll.Items.Add(strField);
			}

			Webb.Collections.Int32Collection selected=new Webb.Collections.Int32Collection();
			
			foreach(int index in this.lstSelected.SelectedIndices)
			{
				selected.Add(index);
			}

			for(int i=this.lstSelected.Items.Count-1;i>=0;i--)
			{
				if(!selected.Contains(i))continue;
				
				this.lstSelected.Items.RemoveAt(i);								
			}
		}

		private void BtnRemoveItem_Click(object sender, System.EventArgs e)
		{
			RemoveItem();
			
		}

		private void BtnAddAll_Click(object sender, System.EventArgs e)
		{
			foreach(string strField in this.lstAll.Items)
			{
				if(!this.lstSelected.Items.Contains(strField))this.lstSelected.Items.Add(strField);
			}

			this.lstAll.Items.Clear();
		
		}

		private void BtnAddColumns_Click(object sender, System.EventArgs e)
		{
			FrmExpression frmExpressrion=new FrmExpression(AvaliableFieldList);

			if(frmExpressrion.ShowDialog()==DialogResult.OK)
			{	
				this.lstAll.Items.Insert(0,frmExpressrion.strExpression);

				this.lstAll.ClearSelected();

				this.lstAll.SelectedIndex=0;				
			}
		}

		private void lstAll_DoubleClick(object sender, System.EventArgs e)
		{
			this.Additem();		
		}

	

		private void BtnExport_Click(object sender, System.EventArgs e)
		{
			TableExpression expression=new TableExpression("");

			this.UpdateExpression(ref expression);

			if(expression==null)return;

             expression.Save();
		}

		private void BtnImport_Click(object sender, System.EventArgs e)
		{
			TableExpression expression=TableExpression.LoadTableExpression();

			this.SetView(expression);		
		}

		private void lstSelected_DoubleClick(object sender, System.EventArgs e)
		{
			 RemoveItem();
		}

		private void cmbFirstTable_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.cmbFirstTable.DroppedDown=true;
		}

		private void cmbSecondTable_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.cmbSecondTable.DroppedDown=true;
		}

		private void cmbFirstRelation_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
		    this.cmbFirstRelation.DroppedDown=true;
		}

		private void cmbSecondRelation_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.cmbSecondRelation.DroppedDown=true;
		}

		private void BtnClear_Click(object sender, System.EventArgs e)
		{			
			foreach(string strField in this.lstSelected.Items)
			{
				if(!this.lstAll.Items.Contains(strField))this.lstAll.Items.Add(strField);
			}

			this.lstSelected.Items.Clear();
		
		}
	
	}
}
