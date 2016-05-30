using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Collections;
using System.Data;

using Webb.Data;
using Webb.Reports.ExControls.Data;


namespace Webb.Reports.ExControls.Editors
{
	
	#region public class ReFilterEditForm : System.Windows.Forms.Form
	/// <summary>
	/// Summary description for FilterEditForm.
	/// </summary>
	public class ReFilterEditForm : System.Windows.Forms.Form
	{
		private DataTable conditionTable;
		private ReFilter value;
		private int nSelectedRow;
		private int nSelectedCol;
		private string strTableName = "Condition Table";
	

		private System.Windows.Forms.Button C_BtnOK;
		private System.Windows.Forms.Button C_BtnCancel;
		private System.Windows.Forms.Button C_BtnAddCondition;
		private System.Windows.Forms.Button C_BtnRemoveCondition;
		private System.Windows.Forms.ComboBox C_ComboValue;
		private System.Windows.Forms.NumericUpDown C_FreqUpDowm;
		private System.Windows.Forms.DataGrid C_DataGrid;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox CkhOther;
		private System.Windows.Forms.Label lblTotalOther;
		private System.Windows.Forms.TextBox txtOther;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ReFilter Value
		{
			get{return this.value;}
			set{this.value = value;}
		}

		public ReFilterEditForm(ReFilter filter)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			//Create table style
			DataGridTableStyle style = new DataGridTableStyle();
 
			style.MappingName = strTableName;

			this.C_DataGrid.TableStyles.Add(style);
			//
			this.Value = filter.Copy();

			this.CreateConditionTable();

             SetOtherInfo(filter);  //2009-4-23 10:54:16@Simon Add this Code
		
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if(this.conditionTable != null) this.conditionTable.Dispose();

			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}


		private void SetOtherInfo(ReFilter filter)
		{
			this.CkhOther.Checked=filter.TotalOther;
			this.txtOther.Text=filter.OtherName;

		}
		//Ok
		private void C_BtnOK_Click(object sender, System.EventArgs e)
		{
			this.HideCombo();

			this.UpdateFilter();

			this.DialogResult = DialogResult.OK;

			this.Close();
		}

		//Cancel
		private void C_BtnCancel_Click(object sender, System.EventArgs e)
		{
			this.HideCombo();

			this.DialogResult = DialogResult.Cancel;

			this.Close();
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.C_BtnOK = new System.Windows.Forms.Button();
			this.C_BtnCancel = new System.Windows.Forms.Button();
			this.C_BtnAddCondition = new System.Windows.Forms.Button();
			this.C_BtnRemoveCondition = new System.Windows.Forms.Button();
			this.C_ComboValue = new System.Windows.Forms.ComboBox();
			this.C_FreqUpDowm = new System.Windows.Forms.NumericUpDown();
			this.C_DataGrid = new System.Windows.Forms.DataGrid();
			this.CkhOther = new System.Windows.Forms.CheckBox();
			this.lblTotalOther = new System.Windows.Forms.Label();
			this.txtOther = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			((System.ComponentModel.ISupportInitialize)(this.C_FreqUpDowm)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.C_DataGrid)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// C_BtnOK
			// 
			this.C_BtnOK.Location = new System.Drawing.Point(144, 256);
			this.C_BtnOK.Name = "C_BtnOK";
			this.C_BtnOK.Size = new System.Drawing.Size(63, 22);
			this.C_BtnOK.TabIndex = 1;
			this.C_BtnOK.Text = "OK";
			this.C_BtnOK.Click += new System.EventHandler(this.C_BtnOK_Click);
			// 
			// C_BtnCancel
			// 
			this.C_BtnCancel.Location = new System.Drawing.Point(240, 256);
			this.C_BtnCancel.Name = "C_BtnCancel";
			this.C_BtnCancel.Size = new System.Drawing.Size(62, 22);
			this.C_BtnCancel.TabIndex = 2;
			this.C_BtnCancel.Text = "Cancel";
			this.C_BtnCancel.Click += new System.EventHandler(this.C_BtnCancel_Click);
			// 
			// C_BtnAddCondition
			// 
			this.C_BtnAddCondition.Location = new System.Drawing.Point(56, 0);
			this.C_BtnAddCondition.Name = "C_BtnAddCondition";
			this.C_BtnAddCondition.Size = new System.Drawing.Size(72, 22);
			this.C_BtnAddCondition.TabIndex = 3;
			this.C_BtnAddCondition.Text = "Add Row";
			this.C_BtnAddCondition.Click += new System.EventHandler(this.C_BtnAddCondition_Click);
			// 
			// C_BtnRemoveCondition
			// 
			this.C_BtnRemoveCondition.Location = new System.Drawing.Point(176, 0);
			this.C_BtnRemoveCondition.Name = "C_BtnRemoveCondition";
			this.C_BtnRemoveCondition.Size = new System.Drawing.Size(80, 22);
			this.C_BtnRemoveCondition.TabIndex = 4;
			this.C_BtnRemoveCondition.Text = "Remove Row";
			this.C_BtnRemoveCondition.Click += new System.EventHandler(this.C_BtnRemoveCondition_Click);
			// 
			// C_ComboValue
			// 
			this.C_ComboValue.Location = new System.Drawing.Point(72, 88);
			this.C_ComboValue.Name = "C_ComboValue";
			this.C_ComboValue.Size = new System.Drawing.Size(80, 21);
			this.C_ComboValue.TabIndex = 8;
			this.C_ComboValue.Visible = false;
			this.C_ComboValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.C_ComboValue_KeyDown);
			this.C_ComboValue.VisibleChanged += new System.EventHandler(this.C_ComboValue_VisibleChanged);
			// 
			// C_FreqUpDowm
			// 
			this.C_FreqUpDowm.Location = new System.Drawing.Point(184, 96);
			this.C_FreqUpDowm.Maximum = new System.Decimal(new int[] {
																		 10000000,
																		 0,
																		 0,
																		 0});
			this.C_FreqUpDowm.Name = "C_FreqUpDowm";
			this.C_FreqUpDowm.Size = new System.Drawing.Size(72, 20);
			this.C_FreqUpDowm.TabIndex = 10;
			this.C_FreqUpDowm.Visible = false;
			this.C_FreqUpDowm.VisibleChanged += new System.EventHandler(this.C_Freq_VisibleChanged);
			// 
			// C_DataGrid
			// 
			this.C_DataGrid.AllowSorting = false;
			this.C_DataGrid.AlternatingBackColor = System.Drawing.Color.GhostWhite;
			this.C_DataGrid.BackColor = System.Drawing.Color.GhostWhite;
			this.C_DataGrid.BackgroundColor = System.Drawing.Color.Lavender;
			this.C_DataGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.C_DataGrid.CaptionBackColor = System.Drawing.Color.RoyalBlue;
			this.C_DataGrid.CaptionForeColor = System.Drawing.Color.White;
			this.C_DataGrid.DataMember = "";
			this.C_DataGrid.FlatMode = true;
			this.C_DataGrid.Font = new System.Drawing.Font("Tahoma", 8F);
			this.C_DataGrid.ForeColor = System.Drawing.Color.MidnightBlue;
			this.C_DataGrid.GridLineColor = System.Drawing.Color.RoyalBlue;
			this.C_DataGrid.HeaderBackColor = System.Drawing.Color.MidnightBlue;
			this.C_DataGrid.HeaderFont = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.C_DataGrid.HeaderForeColor = System.Drawing.Color.Lavender;
			this.C_DataGrid.LinkColor = System.Drawing.Color.Teal;
			this.C_DataGrid.Location = new System.Drawing.Point(8, 24);
			this.C_DataGrid.Name = "C_DataGrid";
			this.C_DataGrid.ParentRowsBackColor = System.Drawing.Color.Lavender;
			this.C_DataGrid.ParentRowsForeColor = System.Drawing.Color.MidnightBlue;
			this.C_DataGrid.ReadOnly = true;
			this.C_DataGrid.SelectionBackColor = System.Drawing.Color.Teal;
			this.C_DataGrid.SelectionForeColor = System.Drawing.Color.PaleGreen;
			this.C_DataGrid.Size = new System.Drawing.Size(312, 193);
			this.C_DataGrid.TabIndex = 9;
			this.C_DataGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.C_DataGrid_MouseDown);
			// 
			// CkhOther
			// 
			this.CkhOther.Location = new System.Drawing.Point(8, 8);
			this.CkhOther.Name = "CkhOther";
			this.CkhOther.Size = new System.Drawing.Size(104, 16);
			this.CkhOther.TabIndex = 11;
			this.CkhOther.Text = "Total Others";
			// 
			// lblTotalOther
			// 
			this.lblTotalOther.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblTotalOther.Location = new System.Drawing.Point(120, 8);
			this.lblTotalOther.Name = "lblTotalOther";
			this.lblTotalOther.Size = new System.Drawing.Size(72, 16);
			this.lblTotalOther.TabIndex = 12;
			this.lblTotalOther.Text = "Total Name";
			// 
			// txtOther
			// 
			this.txtOther.Location = new System.Drawing.Point(200, 8);
			this.txtOther.Name = "txtOther";
			this.txtOther.Size = new System.Drawing.Size(104, 20);
			this.txtOther.TabIndex = 13;
			this.txtOther.Text = "";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.CkhOther);
			this.groupBox1.Controls.Add(this.lblTotalOther);
			this.groupBox1.Controls.Add(this.txtOther);
			this.groupBox1.Location = new System.Drawing.Point(8, 216);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(312, 32);
			this.groupBox1.TabIndex = 15;
			this.groupBox1.TabStop = false;
			this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
			// 
			// ReFilterEditForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(330, 288);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.C_FreqUpDowm);
			this.Controls.Add(this.C_ComboValue);
			this.Controls.Add(this.C_DataGrid);
			this.Controls.Add(this.C_BtnRemoveCondition);
			this.Controls.Add(this.C_BtnAddCondition);
			this.Controls.Add(this.C_BtnCancel);
			this.Controls.Add(this.C_BtnOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "ReFilterEditForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "ValueFilter Editor";
			((System.ComponentModel.ISupportInitialize)(this.C_FreqUpDowm)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.C_DataGrid)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void CreateConditionTable()
		{
			this.CreateConditionTableStructure();

			this.FillConditionTable();
			
			this.C_DataGrid.DataSource = this.conditionTable;

			this.C_DataGrid.TableStyles[strTableName].GridColumnStyles[0].Width = 50;
			this.C_DataGrid.TableStyles[strTableName].GridColumnStyles[1].Width =120	;
			this.C_DataGrid.TableStyles[strTableName].GridColumnStyles[2].Width = 50;
			this.C_DataGrid.TableStyles[strTableName].GridColumnStyles[3].Width =50;	
		}

		private void CreateConditionTableStructure()
		{
			conditionTable = new DataTable(strTableName);
			
			//field column
			DataColumn colField = new DataColumn("Field",typeof(string));

			colField.DefaultValue = "Value";

			//operator column
			DataColumn colOperator = new DataColumn("Operator",typeof(Webb.Data.FilterTypes));

			colOperator.DefaultValue = Webb.Data.FilterTypes.NumGreaterOrEqual;

			//value column
			DataColumn colValue = new DataColumn("Value",typeof(object));

			colValue.DefaultValue= 0;

			//operand column
			DataColumn colOperand = new DataColumn("Operand",typeof(Webb.Data.FilterOperands));

			colOperand.DefaultValue = Webb.Data.FilterOperands.Or;
		
			//add columns
			conditionTable.Columns.Add(colField);
			conditionTable.Columns.Add(colOperator);
			conditionTable.Columns.Add(colValue);
			conditionTable.Columns.Add(colOperand);
			
		}
	

		private void FillConditionTable()
		{
			if(this.Value == null) return;

			foreach(ReElement element in this.Value)
			{
				DataRow row = this.conditionTable.NewRow();

				row[0] = "Value";
				row[1] = element.FilterType;
				row[2] = element.Frequence;
				row[3] = element.FollowedOperand;		

				this.conditionTable.Rows.Add(row);
			}
		}

		private void UpdateFilter()
		{
			this.value.Clear();
            this.value.TotalOther=this.CkhOther.Checked;
	        this.value.OtherName=this.txtOther.Text;

			foreach(DataRow row in this.conditionTable.Rows)
			{
				ReElement element = new ReElement();

				element.Value = row[0].ToString();
				element.FilterType= (Webb.Data.FilterTypes)row[1];
				element.Frequence = Convert.ToInt32(row[2]);
				element.FollowedOperand = (Webb.Data.FilterOperands)row[3];

				this.value.Add(element);
			}
			

			
		}

		//Add condition
		private void C_BtnAddCondition_Click(object sender, System.EventArgs e)
		{
			DataRow row = this.conditionTable.NewRow();

			this.conditionTable.Rows.Add(row);

			HideCombo();
		}

		//Remove condition
		private void C_BtnRemoveCondition_Click(object sender, System.EventArgs e)
		{
			if(this.conditionTable.Rows.Count > 0)
			{
				this.conditionTable.Rows.RemoveAt(this.conditionTable.Rows.Count - 1);
			}

			HideCombo();
		}

		
	
		//DataGrid Mouse Down
		private void C_DataGrid_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			HideCombo();

			DataGrid.HitTestInfo Hti = this.C_DataGrid.HitTest(e.X,e.Y);

			if(Hti.Row >= 0 && Hti.Column >= 1)
			{
				ShowCombo(Hti.Row,Hti.Column);
			}
		}

		//Combo Key Down
		private void C_ComboValue_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyData == Keys.Enter)
			{
				HideCombo();
			}
		}

		//Hide edit combo
		private void HideCombo()
		{			
			this.C_ComboValue.Visible = false;	
			this.C_FreqUpDowm.Visible=false;
             this.C_FreqUpDowm.Value=0;
			
		}

		//Show edit combo
		private void ShowCombo(int row, int col)
		{			
			this.nSelectedRow = row;

			this.nSelectedCol = col;

			string strValue = string.Empty;

			object value = this.conditionTable.Rows[row][col];

			Type type = this.conditionTable.Columns[col].DataType;

			if(type.BaseType == typeof(Enum))
			{
				strValue = Enum.GetName(this.conditionTable.Columns[col].DataType,value);
			}
			else
			{
				strValue = value.ToString();
			}

			if(col==2)
			{
				Rectangle numbounds = this.C_DataGrid.GetCellBounds(row,col);

				Rectangle numRect = this.C_DataGrid.RectangleToScreen(numbounds);

				this.C_FreqUpDowm.Bounds=this.RectangleToClient(numRect);
				
				this.C_FreqUpDowm.Visible=true;

                this.C_FreqUpDowm.Value=Convert.ToUInt32(strValue);

				return;
			}
			

			this.C_ComboValue.Items.Clear();

			Rectangle bounds = this.C_DataGrid.GetCellBounds(row,col);			

			this.C_ComboValue.Text = strValue;

			Rectangle tempRect = this.C_DataGrid.RectangleToScreen(bounds);

			this.C_ComboValue.Bounds = this.RectangleToClient(tempRect);

			switch(col)
			{
				case 0:	//field
				{
					foreach(object field in PublicDBFieldConverter.AvialableFields)
					{
						this.C_ComboValue.Items.Add(field);
					}
					break;
				}
				case 1: //Operator
					foreach(object oper in Enum.GetValues(this.conditionTable.Columns[col].DataType))
					{
						if(oper.ToString().StartsWith("Num"))
						{
							this.C_ComboValue.Items.Add(oper);
						}
					}
					break;
				case 3: //Operand			
				{
					foreach(object oper in Enum.GetValues(this.conditionTable.Columns[col].DataType))
					{
						this.C_ComboValue.Items.Add(oper);
					}
					break;
				}
				default:
					break;
			}
			this.C_ComboValue.Visible = true;

			this.C_ComboValue.SelectAll();
		}

		private void EditValue()
		{
			if(this.nSelectedRow >= this.conditionTable.Rows.Count
				|| this.nSelectedCol >= this.conditionTable.Columns.Count)
			{
				System.Diagnostics.Debug.WriteLine("Cell was not exsit");
				return;
			}

			object value = this.C_ComboValue.Text;

			bool bFind = false;

			Type type = this.conditionTable.Columns[this.nSelectedCol].DataType;

			if(type.BaseType == typeof(Enum))
			{
				foreach(object name in Enum.GetNames(type))
				{
					if(name.ToString() == this.C_ComboValue.Text)
					{
						value = Enum.Parse(type,this.C_ComboValue.Text,true);

						bFind = true;

						break;
					}
				}

				if(!bFind)
				{
					value = this.conditionTable.Columns[this.nSelectedCol].DefaultValue;
				}
			}		

			this.conditionTable.Rows[this.nSelectedRow][this.nSelectedCol] = value;
		}

		//Combo visible change
		private void C_ComboValue_VisibleChanged(object sender, System.EventArgs e)
		{
			if(this.C_ComboValue.Visible == false)
			{
				this.EditValue();
			}
		}

		//Combo visible change
		private void C_Freq_VisibleChanged(object sender, System.EventArgs e)
		{
			if(this.C_FreqUpDowm.Visible == false)
			{
				int value=(int)this.C_FreqUpDowm.Value;

				this.conditionTable.Rows[this.nSelectedRow][2] = value;

			}
		}

		private void groupBox1_Enter(object sender, System.EventArgs e)
		{
		
		}
		
		
	}
	#endregion

	#region public class ReFilterEditor : System.Drawing.Design.UITypeEditor
	// This UITypeEditor can be associated with DBFilter
	// properties to provide a design-mode angle selection interface.
	public class ReFilterEditor : System.Drawing.Design.UITypeEditor
	{
		public ReFilterEditor()
		{

		}

		// Indicates whether the UITypeEditor provides a form-based (modal) dialog, 
		// drop down dialog, or no UI outside of the properties window.
		[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name="FullTrust")] 
		public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}

		// Indicates whether the UITypeEditor supports painting a 
		// representation of a property's value.
		[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name="FullTrust")]
		public override bool GetPaintValueSupported(System.ComponentModel.ITypeDescriptorContext context)
		{
			return false;
		}

		// Displays the UI for value selection.
		[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name="FullTrust")]
		public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, System.IServiceProvider provider, object value)
		{
			// Return the value if the value is not of type Int32, Double and Single.
			if( !(value is ReFilter) ) return value;

			// Uses the IWindowsFormsEditorService to display a 
			// drop-down UI in the Properties window.
			IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
			if( edSvc != null )
			{
				// Display an angle selection control and retrieve the value.
				ReFilterEditForm filterEditForm = new ReFilterEditForm(value as ReFilter);

				if(DialogResult.OK == edSvc.ShowDialog( filterEditForm ))
				{
					// Return the value in the appropraite data format.
					return filterEditForm.Value;
				}
			}
			return value;
		}

		// Draws a representation of the property's value.
		[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name="FullTrust")]
		public override void PaintValue(System.Drawing.Design.PaintValueEventArgs e)
		{
			base.PaintValue(e);
		}
	}
	#endregion
}