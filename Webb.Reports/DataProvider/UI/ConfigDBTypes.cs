/***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:ConfigDBTypes.cs
 * Author:Country.Wu [EMail:Webb.Country.Wu@163.com]
 * Create Time:11/8/2007 01:15:15 PM
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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Webb;
using Webb.Data;
using Webb.Reports;
using Webb.Reports.DataProvider;


namespace Webb.Reports.DataProvider.UI
{
	public class ConfigDBTypes : Webb.Utilities.Wizards.WinzardControlBase
	{
		DBConnTypes _ConnType;
		WebbDBTypes _WebbDBType;
		//
		private System.Windows.Forms.RadioButton C_TypeOLE;
		private System.Windows.Forms.RadioButton C_SQLType;
		private System.Windows.Forms.RadioButton C_XMLType;
		private System.Windows.Forms.RadioButton C_WebbFootball;
		private System.Windows.Forms.RadioButton C_WebbVictory;
		private System.Windows.Forms.RadioButton C_CommonDataSource;
		private System.Windows.Forms.GroupBox C_DatabaseTypeGroup;
		private System.Windows.Forms.GroupBox C_DataSourceTypeGroup;
		private System.Windows.Forms.RadioButton C_FileType;
		private System.Windows.Forms.ComboBox C_CBVictoryType;
        private RadioButton C_WebbCoachCRM;
        private RadioButton C_WebbPlayBook;
		private System.ComponentModel.IContainer components = null;

		public ConfigDBTypes()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();
			this.FirstControl = true;
			// TODO: Add any initialization after the InitializeComponent call
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.C_DatabaseTypeGroup = new System.Windows.Forms.GroupBox();
            this.C_FileType = new System.Windows.Forms.RadioButton();
            this.C_TypeOLE = new System.Windows.Forms.RadioButton();
            this.C_SQLType = new System.Windows.Forms.RadioButton();
            this.C_XMLType = new System.Windows.Forms.RadioButton();
            this.C_DataSourceTypeGroup = new System.Windows.Forms.GroupBox();
            this.C_WebbCoachCRM = new System.Windows.Forms.RadioButton();
            this.C_CBVictoryType = new System.Windows.Forms.ComboBox();
            this.C_CommonDataSource = new System.Windows.Forms.RadioButton();
            this.C_WebbVictory = new System.Windows.Forms.RadioButton();
            this.C_WebbFootball = new System.Windows.Forms.RadioButton();
            this.C_WebbPlayBook = new System.Windows.Forms.RadioButton();
            this.C_DatabaseTypeGroup.SuspendLayout();
            this.C_DataSourceTypeGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // C_DatabaseTypeGroup
            // 
            this.C_DatabaseTypeGroup.Controls.Add(this.C_FileType);
            this.C_DatabaseTypeGroup.Controls.Add(this.C_TypeOLE);
            this.C_DatabaseTypeGroup.Controls.Add(this.C_SQLType);
            this.C_DatabaseTypeGroup.Controls.Add(this.C_XMLType);
            this.C_DatabaseTypeGroup.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.C_DatabaseTypeGroup.Location = new System.Drawing.Point(0, 8);
            this.C_DatabaseTypeGroup.Name = "C_DatabaseTypeGroup";
            this.C_DatabaseTypeGroup.Size = new System.Drawing.Size(768, 198);
            this.C_DatabaseTypeGroup.TabIndex = 0;
            this.C_DatabaseTypeGroup.TabStop = false;
            this.C_DatabaseTypeGroup.Text = "       Database types:";
            // 
            // C_FileType
            // 
            this.C_FileType.Location = new System.Drawing.Point(57, 138);
            this.C_FileType.Name = "C_FileType";
            this.C_FileType.Size = new System.Drawing.Size(264, 32);
            this.C_FileType.TabIndex = 1;
            this.C_FileType.Text = "File (For Advantage Products)";
            this.C_FileType.CheckedChanged += new System.EventHandler(this.C_ConfigDBTypeCheckedChanged);
            // 
            // C_TypeOLE
            // 
            this.C_TypeOLE.Location = new System.Drawing.Point(57, 33);
            this.C_TypeOLE.Name = "C_TypeOLE";
            this.C_TypeOLE.Size = new System.Drawing.Size(323, 24);
            this.C_TypeOLE.TabIndex = 0;
            this.C_TypeOLE.Text = "MS Access 2000+ ( For Victory Products)";
            this.C_TypeOLE.CheckedChanged += new System.EventHandler(this.C_ConfigDBTypeCheckedChanged);
            // 
            // C_SQLType
            // 
            this.C_SQLType.Location = new System.Drawing.Point(57, 79);
            this.C_SQLType.Name = "C_SQLType";
            this.C_SQLType.Size = new System.Drawing.Size(323, 32);
            this.C_SQLType.TabIndex = 0;
            this.C_SQLType.Text = "MS SQL Server(For Coach CRM)";
            this.C_SQLType.CheckedChanged += new System.EventHandler(this.C_ConfigDBTypeCheckedChanged);
            // 
            // C_XMLType
            // 
            this.C_XMLType.Location = new System.Drawing.Point(382, 133);
            this.C_XMLType.Name = "C_XMLType";
            this.C_XMLType.Size = new System.Drawing.Size(264, 32);
            this.C_XMLType.TabIndex = 0;
            this.C_XMLType.Text = "XML Database File";
            this.C_XMLType.Visible = false;
            this.C_XMLType.CheckedChanged += new System.EventHandler(this.C_ConfigDBTypeCheckedChanged);
            // 
            // C_DataSourceTypeGroup
            // 
            this.C_DataSourceTypeGroup.Controls.Add(this.C_WebbPlayBook);
            this.C_DataSourceTypeGroup.Controls.Add(this.C_WebbCoachCRM);
            this.C_DataSourceTypeGroup.Controls.Add(this.C_CBVictoryType);
            this.C_DataSourceTypeGroup.Controls.Add(this.C_CommonDataSource);
            this.C_DataSourceTypeGroup.Controls.Add(this.C_WebbVictory);
            this.C_DataSourceTypeGroup.Controls.Add(this.C_WebbFootball);
            this.C_DataSourceTypeGroup.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.C_DataSourceTypeGroup.Location = new System.Drawing.Point(0, 212);
            this.C_DataSourceTypeGroup.Name = "C_DataSourceTypeGroup";
            this.C_DataSourceTypeGroup.Size = new System.Drawing.Size(768, 265);
            this.C_DataSourceTypeGroup.TabIndex = 0;
            this.C_DataSourceTypeGroup.TabStop = false;
            this.C_DataSourceTypeGroup.Text = "       Data source types:";
            // 
            // C_WebbCoachCRM
            // 
            this.C_WebbCoachCRM.AutoSize = true;
            this.C_WebbCoachCRM.Location = new System.Drawing.Point(465, 128);
            this.C_WebbCoachCRM.Name = "C_WebbCoachCRM";
            this.C_WebbCoachCRM.Size = new System.Drawing.Size(132, 18);
            this.C_WebbCoachCRM.TabIndex = 4;
            this.C_WebbCoachCRM.TabStop = true;
            this.C_WebbCoachCRM.Text = "Webb CoachCRM";
            this.C_WebbCoachCRM.UseVisualStyleBackColor = true;
            this.C_WebbCoachCRM.CheckedChanged += new System.EventHandler(this.C_DBTypeCheckedChanged);
            // 
            // C_CBVictoryType
            // 
            this.C_CBVictoryType.Enabled = false;
            this.C_CBVictoryType.Items.AddRange(new object[] {
            "Football",
            "Basketball",
            "Volleyball",
            "Hockey",
            "Lacrosse",
            "Soccer"});
            this.C_CBVictoryType.Location = new System.Drawing.Point(205, 195);
            this.C_CBVictoryType.Name = "C_CBVictoryType";
            this.C_CBVictoryType.Size = new System.Drawing.Size(184, 22);
            this.C_CBVictoryType.TabIndex = 3;
            this.C_CBVictoryType.Text = "Football";
            this.C_CBVictoryType.SelectedIndexChanged += new System.EventHandler(this.C_CBVictoryType_SelectedIndexChanged);
            // 
            // C_CommonDataSource
            // 
            this.C_CommonDataSource.Location = new System.Drawing.Point(57, 122);
            this.C_CommonDataSource.Name = "C_CommonDataSource";
            this.C_CommonDataSource.Size = new System.Drawing.Size(176, 24);
            this.C_CommonDataSource.TabIndex = 2;
            this.C_CommonDataSource.Text = "Common Data Source";
            this.C_CommonDataSource.CheckedChanged += new System.EventHandler(this.C_DBTypeCheckedChanged);
            // 
            // C_WebbVictory
            // 
            this.C_WebbVictory.Location = new System.Drawing.Point(57, 193);
            this.C_WebbVictory.Name = "C_WebbVictory";
            this.C_WebbVictory.Size = new System.Drawing.Size(128, 24);
            this.C_WebbVictory.TabIndex = 1;
            this.C_WebbVictory.Text = "Webb Victory";
            this.C_WebbVictory.CheckedChanged += new System.EventHandler(this.C_DBTypeCheckedChanged);
            // 
            // C_WebbFootball
            // 
            this.C_WebbFootball.Location = new System.Drawing.Point(57, 54);
            this.C_WebbFootball.Name = "C_WebbFootball";
            this.C_WebbFootball.Size = new System.Drawing.Size(176, 24);
            this.C_WebbFootball.TabIndex = 0;
            this.C_WebbFootball.Text = "Webb Advantage";
            this.C_WebbFootball.CheckedChanged += new System.EventHandler(this.C_DBTypeCheckedChanged);
            // 
            // C_WebbPlayBook
            // 
            this.C_WebbPlayBook.AutoSize = true;
            this.C_WebbPlayBook.Location = new System.Drawing.Point(465, 60);
            this.C_WebbPlayBook.Name = "C_WebbPlayBook";
            this.C_WebbPlayBook.Size = new System.Drawing.Size(123, 18);
            this.C_WebbPlayBook.TabIndex = 4;
            this.C_WebbPlayBook.TabStop = true;
            this.C_WebbPlayBook.Text = "Webb PlayBook";
            this.C_WebbPlayBook.UseVisualStyleBackColor = true;
            this.C_WebbPlayBook.CheckedChanged += new System.EventHandler(this.C_DBTypeCheckedChanged);
            // 
            // ConfigDBTypes
            // 
            this.Controls.Add(this.C_DatabaseTypeGroup);
            this.Controls.Add(this.C_DataSourceTypeGroup);
            this.Name = "ConfigDBTypes";
            this.WizardTitle = "  Step 1: Choose the database type and source type.";
            this.C_DatabaseTypeGroup.ResumeLayout(false);
            this.C_DataSourceTypeGroup.ResumeLayout(false);
            this.C_DataSourceTypeGroup.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		public override void UpdateData(object i_Data)
		{
			//base.UpdateData (i_Data);
			//
			DBSourceConfig m_config = i_Data as DBSourceConfig;
			m_config.DBConnType = this._ConnType;
			m_config.WebbDBType = this._WebbDBType;
		}

		public override bool ValidateSetting()
		{
			return true;
		}

		public override void SetData(object i_Data)
		{
			//base.SetData (i_Data);
			DBSourceConfig m_config = i_Data as DBSourceConfig;
			//set members
			this._ConnType = m_config.DBConnType;
			this._WebbDBType = m_config.WebbDBType;
			//set data base type
			this.C_SQLType.Checked = this._ConnType==DBConnTypes.SQLDB;
			this.C_TypeOLE.Checked = this._ConnType==DBConnTypes.OleDB;
			this.C_XMLType.Checked = this._ConnType==DBConnTypes.XMLFile;
			this.C_FileType.Checked = this._ConnType==DBConnTypes.File;
			//set data source type
			this.C_WebbFootball.Checked = this._WebbDBType==WebbDBTypes.WebbAdvantageFootball;



			this.C_WebbVictory.Checked = (int)this._WebbDBType >= 100 && (int)this._WebbDBType <= 105;	//football, basketball, volleyball, hockey
			if(this.C_WebbVictory.Checked)
			{
				this.C_CBVictoryType.Enabled = true;
				this.C_CBVictoryType.SelectedIndex = (int)this._WebbDBType - 100;
			}

            this.C_WebbCoachCRM.Checked = this._WebbDBType == WebbDBTypes.CoachCRM;

            this.C_WebbPlayBook.Checked = this._WebbDBType == WebbDBTypes.WebbPlaybook;

			this.C_CommonDataSource.Checked = this._WebbDBType==WebbDBTypes.Others;

		}

		public override void ResetControl()
		{
			this.C_TypeOLE.Checked = false;
			this.C_SQLType.Checked = false;
			this.C_XMLType.Checked = false;
			this.C_FileType.Checked = false;
			this.C_WebbFootball.Checked = false;
			this.C_WebbVictory.Checked = false;
			this.C_CommonDataSource.Checked = false;
		}

		private void C_DBTypeCheckedChanged(object sender, System.EventArgs e)
		{
			if(this.C_WebbVictory.Checked)
			{
				this.C_CBVictoryType.Enabled = true;

				this._WebbDBType = (WebbDBTypes)(this.C_CBVictoryType.SelectedIndex + 100);

                this.C_TypeOLE.Checked = true;
			}
			else
			{
				this.C_CBVictoryType.Enabled = false;

				if(this.C_CommonDataSource.Checked)
				{
					this._WebbDBType = WebbDBTypes.Others;

				}
                else if (this.C_WebbFootball.Checked)
                {
                    this._WebbDBType = WebbDBTypes.WebbAdvantageFootball;

                    this.C_FileType.Checked = true;
                }
                else if (this.C_WebbCoachCRM.Checked)
                {
                    this._WebbDBType = WebbDBTypes.CoachCRM;

                    this.C_SQLType.Checked = true;
                }
                else if(this.C_WebbPlayBook.Checked)
                {
                    this._WebbDBType = WebbDBTypes.WebbPlaybook;

                    this.C_FileType.Checked = true;
                }
			}
		}

		private void C_ConfigDBTypeCheckedChanged(object sender, System.EventArgs e)
		{
			if(this.C_SQLType.Checked) this._ConnType = DBConnTypes.SQLDB;
			else if(this.C_TypeOLE.Checked) this._ConnType = DBConnTypes.OleDB;
			else if(this.C_XMLType.Checked) this._ConnType = DBConnTypes.XMLFile;
			else if(this.C_FileType.Checked) this._ConnType = DBConnTypes.File;
		}

		private void C_CBVictoryType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this._WebbDBType = (WebbDBTypes)(this.C_CBVictoryType.SelectedIndex + 100);
		}
	}
}

