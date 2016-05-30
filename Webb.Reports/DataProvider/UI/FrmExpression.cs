using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Webb.Reports.DataProvider.UI
{
	/// <summary>
	/// Summary description for FrmExpression.
	/// </summary>
	public class FrmExpression : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button BtnOK;
		private System.Windows.Forms.TextBox txtColumn;
		private System.Windows.Forms.Button BtnCancel;
		private System.Windows.Forms.Label lblColumns;
		private System.Windows.Forms.ComboBox cmbColumn;
		private System.Windows.Forms.Button BtnAddColumns;
		private System.Windows.Forms.TextBox txtColumnName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtModifier;
		private System.Windows.Forms.Label label2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public string strExpression=string.Empty;

		public FrmExpression()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}
		public FrmExpression(ArrayList arrList)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			this.cmbColumn.Items.Clear();
             
			foreach(string strName in arrList)
			{
				this.cmbColumn.Items.Add(strName);
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
			this.BtnOK = new System.Windows.Forms.Button();
			this.txtColumn = new System.Windows.Forms.TextBox();
			this.BtnCancel = new System.Windows.Forms.Button();
			this.lblColumns = new System.Windows.Forms.Label();
			this.cmbColumn = new System.Windows.Forms.ComboBox();
			this.BtnAddColumns = new System.Windows.Forms.Button();
			this.txtColumnName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.txtModifier = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// BtnOK
			// 
			this.BtnOK.Location = new System.Drawing.Point(472, 112);
			this.BtnOK.Name = "BtnOK";
			this.BtnOK.Size = new System.Drawing.Size(64, 24);
			this.BtnOK.TabIndex = 2;
			this.BtnOK.Text = "OK";
			this.BtnOK.Click += new System.EventHandler(this.BtnOK_Click);
			// 
			// txtColumn
			// 
			this.txtColumn.Location = new System.Drawing.Point(8, 40);
			this.txtColumn.Name = "txtColumn";
			this.txtColumn.Size = new System.Drawing.Size(616, 20);
			this.txtColumn.TabIndex = 3;
			this.txtColumn.Text = "";
			// 
			// BtnCancel
			// 
			this.BtnCancel.Location = new System.Drawing.Point(560, 112);
			this.BtnCancel.Name = "BtnCancel";
			this.BtnCancel.Size = new System.Drawing.Size(56, 24);
			this.BtnCancel.TabIndex = 4;
			this.BtnCancel.Text = "Cancel";
			this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
			// 
			// lblColumns
			// 
			this.lblColumns.Location = new System.Drawing.Point(16, 80);
			this.lblColumns.Name = "lblColumns";
			this.lblColumns.Size = new System.Drawing.Size(128, 24);
			this.lblColumns.TabIndex = 5;
			this.lblColumns.Text = "Alias The ColumnName";
			// 
			// cmbColumn
			// 
			this.cmbColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbColumn.Location = new System.Drawing.Point(96, 8);
			this.cmbColumn.Name = "cmbColumn";
			this.cmbColumn.Size = new System.Drawing.Size(232, 21);
			this.cmbColumn.TabIndex = 6;
			// 
			// BtnAddColumns
			// 
			this.BtnAddColumns.Location = new System.Drawing.Point(552, 8);
			this.BtnAddColumns.Name = "BtnAddColumns";
			this.BtnAddColumns.Size = new System.Drawing.Size(64, 24);
			this.BtnAddColumns.TabIndex = 7;
			this.BtnAddColumns.Text = "Add ";
			this.BtnAddColumns.Click += new System.EventHandler(this.BtnAddColumns_Click);
			// 
			// txtColumnName
			// 
			this.txtColumnName.Location = new System.Drawing.Point(136, 80);
			this.txtColumnName.Name = "txtColumnName";
			this.txtColumnName.Size = new System.Drawing.Size(256, 20);
			this.txtColumnName.TabIndex = 9;
			this.txtColumnName.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(336, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(46, 16);
			this.label1.TabIndex = 10;
			this.label1.Text = "Modifier";
			// 
			// txtModifier
			// 
			this.txtModifier.Location = new System.Drawing.Point(376, 8);
			this.txtModifier.Name = "txtModifier";
			this.txtModifier.Size = new System.Drawing.Size(160, 20);
			this.txtModifier.TabIndex = 11;
			this.txtModifier.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(96, 16);
			this.label2.TabIndex = 12;
			this.label2.Text = "Existing columns";
			// 
			// FrmExpression
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(634, 144);
			this.Controls.Add(this.txtModifier);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtColumnName);
			this.Controls.Add(this.BtnAddColumns);
			this.Controls.Add(this.cmbColumn);
			this.Controls.Add(this.lblColumns);
			this.Controls.Add(this.BtnCancel);
			this.Controls.Add(this.txtColumn);
			this.Controls.Add(this.BtnOK);
			this.Controls.Add(this.label2);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "FrmExpression";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Add a column";
			this.ResumeLayout(false);

		}
		#endregion

		private void BtnOK_Click(object sender, System.EventArgs e)
		{
			string columnName=this.txtColumnName.Text.Trim();

			if(columnName==string.Empty)
			{				
				Webb.Utilities.MessageBoxEx.ShowError("Please input the column name !");
				
				this.txtColumnName.Focus();

				return;					
			}
            string column=this.txtColumn.Text.Trim();

			strExpression="("+column+") as "+columnName;

			DialogResult=DialogResult.OK;
		}

		private void BtnAddColumns_Click(object sender, System.EventArgs e)
		{
			if(this.cmbColumn.Text==string.Empty)
			{
				Webb.Utilities.MessageBoxEx.ShowError("Please select a exsting column to add first!");
				
				this.cmbColumn.Focus();
				return;
			}

			string text=this.txtColumn.Text.Trim();

			string columnsExpress;

			if(this.txtModifier.Text==string.Empty)
			{
				columnsExpress="isnull("+this.cmbColumn.Text+",'')";
			}
			else
			{
				columnsExpress="isnull("+this.cmbColumn.Text+",'')+'"+this.txtModifier.Text+"'";
			}
			
			if(text==string.Empty)
			{				
				this.txtColumn.Text = columnsExpress;
			}
			else
			{
				this.txtColumn.Text =text+ "+" + columnsExpress;
			}

			this.cmbColumn.SelectedIndex=-1;

			this.txtModifier.Text=string.Empty;
			
		}

		private void BtnCancel_Click(object sender, System.EventArgs e)
		{
			DialogResult=DialogResult.Cancel;
		}
	}
}
