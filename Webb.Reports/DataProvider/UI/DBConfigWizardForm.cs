/***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:DBConfigWizardForm.cs
 * Author:Country.Wu [EMail:Webb.Country.Wu@163.com]
 * Create Time:11/9/2007 09:21:20 AM
 * Copyright:1986-2007@Webb Electronics all right reserved.
 * Purpose:
 * ***********************************************************************/
#region History
/*
 * //Author@DateTime : Description
 * */
#endregion History

using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Webb.Data;
using Webb.Utilities.Wizards;

namespace Webb.Reports.DataProvider.UI
{
	public class DBConfigWizardForm : Webb.Utilities.Wizards.WizardBaseForm
	{
		//wizard steps
		//Scott@2007-11-15 08:51 modified some of the following code.
		public ConfigDBTypes Step_ConfigDBTypes ;
		public ConfigGames Step_ConfigGames;
		public ConfigFilters Step_ConfigFilters;
		public ConfigDBCatalog Step_ConfigDBCatalog;
		public ConfigDBFile Step_ConfigDBFile;
		public ConfigTables Step_ConfigTable;
		public ConfigAdvFile Step_ConfigAdvFile;
		public ConfigAdvDB Step_ConfigAdvDB;

		private ConfigCommonSQL Step_ConfigCommonSQL;
		private ConfigComonTable Step_ConfigCommontable;
        private ConfigCommonAccess Step_ConfigCommonAccess;
        
		//
		private WebbDataProvider _DataProvider;
		private DBSourceConfig _DBSourceConfig;


		new public Size Size
		{
			get{return base.Size;}
		}
		//
		public WebbDataProvider DataProvider
		{
			get{return this._DataProvider;}
			set{this._DataProvider = value;}
		}
		public DBSourceConfig DBSourceConfig
		{
			get{return this._DBSourceConfig;}
			set{this._DBSourceConfig = value;}
		}
		//
		private System.ComponentModel.IContainer components = null;

		public DBConfigWizardForm(WebbDataProvider i_DataProvider)
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();
			this._DataProvider = i_DataProvider;
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
            this.SuspendLayout();
            // 
            // C_Next
            // 
            this.C_Next.Location = new System.Drawing.Point(544, 16);
            // 
            // C_Back
            // 
            this.C_Back.Location = new System.Drawing.Point(464, 16);
            // 
            // DBConfigWizardForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(7, 15);
            this.ClientSize = new System.Drawing.Size(808, 574);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "DBConfigWizardForm";
            this.Text = "Webb DataSource Wizard";
            this.WizardStatus = ((Webb.Utilities.Wizards.WizardStatus)((Webb.Utilities.Wizards.WizardStatus.OK | Webb.Utilities.Wizards.WizardStatus.Cancel)));
            this.Load += new System.EventHandler(this.DBConfigWizardForm_Load);
            this.ResumeLayout(false);

		}
		#endregion

		public override void SetData(object i_Data)
		{
			System.Diagnostics.Trace.Assert(i_Data is Webb.Reports.DataProvider.DBSourceConfig);
			this._DBSourceConfig = i_Data as DBSourceConfig;
		}

		public override void UpdateData(object i_Data)
		{

		}

		private void DBConfigWizardForm_Load(object sender, System.EventArgs e)
		{
			if(this.Step_ConfigDBTypes==null) this.Step_ConfigDBTypes = new ConfigDBTypes();
			this.LoadControl(this.Step_ConfigDBTypes);
			this.Step_ConfigDBTypes.SetData(this._DBSourceConfig);
			this.WizardStatus = WizardStatus.Next|WizardStatus.Cancel;
		}

		public override void OnNextStep()
		{
			if(this._CurrentControl!=null&&this._CurrentControl.ValidateSetting())
			{
				this._CurrentControl.UpdateData(this._DBSourceConfig);
				this.LoadNextControl();
			}
		}

		public override void OnBackStep()
		{
			//base.OnBackStep();
			//Scott@2007-11-19 08:53 modified some of the following code.
			this._CurrentControl.ResetControl();
			this.LoadBackControl();
		}

		public override void OnOK()
		{
			if(this._CurrentControl is ConfigCommonSQL )
			{
				bool Connected=(_CurrentControl as ConfigCommonSQL).UpdateConfig(this._DBSourceConfig);

				if(Connected)
				{
					this.DialogResult=DialogResult.OK;
				}	

				return;
			}
			else if(this._CurrentControl is ConfigComonTable)
			{
				bool Connected=(_CurrentControl as ConfigComonTable).UpdateConfig(this._DBSourceConfig);

				if(Connected)
				{
					this.DialogResult=DialogResult.OK;
				}	

				return;

			}
            else if (this._CurrentControl is ConfigCommonAccess)
            {
                bool Connected = (_CurrentControl as ConfigCommonAccess).UpdateConfig(this._DBSourceConfig);

                if (Connected)
                {
                    this.DialogResult = DialogResult.OK;
                }

                return;

            }           
			else
			{
                if (this._CurrentControl != null&&this._CurrentControl.ValidateSetting())
				{
					//MessageBox.Show("OK");
					//Scott@2007-11-14 16:57 modified some of the following code.
					this._CurrentControl.UpdateData(this._DBSourceConfig);



					this.DialogResult = DialogResult.OK;
				}
				//Scott@2007-11-16 09:37 modified some of the following code.
				if(this._CurrentControl is ConfigGames && this.Step_ConfigFilters != null)
				{
					this.Step_ConfigFilters.ResetControl();
					if(this._DBSourceConfig.FilterIDs != null)
						this._DBSourceConfig.FilterIDs.Clear();
				}			
			
			}
		}

//		public override void OnCancel()
//		{
//			//base.OnCancel ();
//			return DialogResult.Cancel;
//		}

		#region load controls
		private void LoadNextControl()
		{
			Webb.Utilities.WaitingForm.ShowWaitingForm();
			WinzardControlBase m_TempControl = this._CurrentControl;

 			#region go to Step 2
			//Step 1, to load step 2 config Access or config SQL DB catalog.
			if(this._CurrentControl is ConfigDBTypes)
			{//All
				if(this._DBSourceConfig.DBConnType == DBConnTypes.OleDB || 
					this._DBSourceConfig.DBConnType == DBConnTypes.XMLFile)
				{//DBFile
                    if (this._DBSourceConfig.WebbDBType == WebbDBTypes.CoachCRM)
                    {
                        ConfigStepCCRMData Step_ConfigCCRM = new ConfigStepCCRMData();

                        this.C_SelectAll.Enabled = false;

                        this.LoadControl(Step_ConfigCCRM);

                        Step_ConfigCCRM.SetData(this._DBSourceConfig);

                    }
                    else
                    {
                        if (this.Step_ConfigDBFile == null) this.Step_ConfigDBFile = new ConfigDBFile();
                        this.Step_ConfigDBFile.SetData(this.DBSourceConfig);
                        this.LoadControl(this.Step_ConfigDBFile);
                    }
				}
				else if(this._DBSourceConfig.DBConnType == DBConnTypes.File)
				{//File
					this._DBSourceConfig.ConnString = string.Empty;					
					if(this._DBSourceConfig.WebbDBType == WebbDBTypes.WebbAdvantageFootball)
					{
						if(this.Step_ConfigAdvFile == null) this.Step_ConfigAdvFile = new ConfigAdvFile();
						this.Step_ConfigAdvFile.SetData(this._DBSourceConfig);
						this.LoadControl(this.Step_ConfigAdvFile);
					}
                    else if (this._DBSourceConfig.WebbDBType == WebbDBTypes.WebbPlaybook)
                    {
                        ConfigPlayBookData Step_ConfigPlayBook = new ConfigPlayBookData();
                        Step_ConfigPlayBook.SetData(this._DBSourceConfig);
                        this.LoadControl(Step_ConfigPlayBook);
                    }
					else 
					{
						MessageBox.Show("The product didn't have file version yet.");
					}
				}
				else
				{//SQL
					if(this._DBSourceConfig.WebbDBType == WebbDBTypes.WebbAdvantageFootball)
					{//choose advantage DB //Modified at 2008-10-10 10:15:39@Scott
						if(this.Step_ConfigAdvDB==null) this.Step_ConfigAdvDB = new ConfigAdvDB();
						this.LoadControl(this.Step_ConfigAdvDB);
					}
					else if(this._DBSourceConfig.WebbDBType == WebbDBTypes.Others)
					{
						if(Step_ConfigCommonSQL==null)Step_ConfigCommonSQL=new ConfigCommonSQL();						

						this.C_SelectAll.Enabled=false;

						this.LoadControl(Step_ConfigCommonSQL);

						Step_ConfigCommonSQL.SetData(this._DBSourceConfig);

					}
                    else if (this._DBSourceConfig.WebbDBType == WebbDBTypes.CoachCRM)
                    {
                        ConfigStepCCRMData Step_ConfigCCRM = new ConfigStepCCRMData();
 
                        this.C_SelectAll.Enabled = false;

                        this.LoadControl(Step_ConfigCCRM);

                        Step_ConfigCCRM.SetData(this._DBSourceConfig);

                    }
                    else
                    {
                        if (this.Step_ConfigDBCatalog == null) this.Step_ConfigDBCatalog = new ConfigDBCatalog();
                        this.Step_ConfigDBCatalog.SetDatabases();

                        this.LoadControl(this.Step_ConfigDBCatalog);
                    }
				}
				goto EXIT;
			}
			#endregion

			#region go to step 3
			//Step 2, to load games or to load tables.
			if(this._CurrentControl is ConfigDBFile)
			{//File
				this._DBSourceConfig.ConnString = string.Empty;
				this.DataProvider.InitializeDBManager(this._DBSourceConfig);

				if(this._DBSourceConfig.DBConnType == DBConnTypes.OleDB)
				{//Access
                    if (this._DBSourceConfig.WebbDBType != WebbDBTypes.Others)
                    {
                        DataTable m_Games = this.DataProvider.LoadWebbGames();
                        if (this.Step_ConfigGames == null) this.Step_ConfigGames = new ConfigGames();
                        this.Step_ConfigGames.SetGames(m_Games);
                        DataTable m_Edls = this.DataProvider.LoadWebbEdls();
                        this.Step_ConfigGames.SetEdls(m_Edls);
                        this.LoadControl(this.Step_ConfigGames);
                        this.SelectStep = true;
                    }
                    else
                    {
                        if (this.Step_ConfigCommonAccess == null) this.Step_ConfigCommonAccess = new ConfigCommonAccess();

                        if (Step_ConfigCommonAccess.SetConfig(this._DBSourceConfig))
                        {
                            this.C_SelectAll.Enabled = false;

                            this.LoadControl(Step_ConfigCommonAccess);

                        }
                        else
                        {
                            Webb.Utilities.WaitingForm.CloseWaitingForm();

                            return;
                        }

                    }
				}
				else if(this._DBSourceConfig.DBConnType == DBConnTypes.XMLFile)
				{//XML
					DataSet m_DataSet = this.DataProvider.LoadXmlDataSet();
					if(this.Step_ConfigTable==null) this.Step_ConfigTable = new ConfigTables();
					this.Step_ConfigTable.SetTables(m_DataSet);
					this.LoadControl(this.Step_ConfigTable);
				}
				else
				{//never
					Webb.Utilities.WaitingForm.CloseWaitingForm();
					return;
				}
				goto EXIT;
			}
			if(this._CurrentControl is ConfigCommonSQL )
			{
				bool Connected=(_CurrentControl as ConfigCommonSQL).UpdateConfig(this._DBSourceConfig);

				if(Connected)
				{
					if(this.Step_ConfigCommontable==null)this.Step_ConfigCommontable = new ConfigComonTable();
                         
					if(Step_ConfigCommontable.SetConfig(this._DBSourceConfig))
					{
						this.C_SelectAll.Enabled=false;

					     this.LoadControl(Step_ConfigCommontable);

					}
					else
					{
						Webb.Utilities.WaitingForm.CloseWaitingForm();

						return;
					}

					
				}	
				goto EXIT;
				
			}
			if(this._CurrentControl is ConfigDBCatalog || this._CurrentControl is ConfigAdvDB)
			{//SQL
				this.DataProvider.InitializeDBManager(this._DBSourceConfig);

				if(this._DBSourceConfig.WebbDBType != WebbDBTypes.Others)
				{
					DataTable m_Games = this.DataProvider.LoadWebbGames();

					if(this.Step_ConfigGames==null) this.Step_ConfigGames = new ConfigGames();
					this.Step_ConfigGames.SetGames(m_Games);
					this.LoadControl(this.Step_ConfigGames);
//					if(this.Step_ConfigTable==null) this.Step_ConfigTable = new ConfigTables();
//					this.Step_ConfigTable.SetTables(m_Games);
//					this.LoadControl(this.Step_ConfigTable);
				}
				else
				{//Common
					//DataTable m_Tables = this.DataProvider.LoadTables();
					if(this.Step_ConfigTable==null) this.Step_ConfigTable = new ConfigTables();
					//this.Step_ConfigTable.SetTables(m_Tables);
					this.LoadControl(this.Step_ConfigTable);
				}
				goto EXIT;
			}
			#endregion

			//11-12-2007@Scott
			#region go to setup4
			if(this._CurrentControl is ConfigGames)
			{//Access & File
				if((int)this._DBSourceConfig.WebbDBType >= 100 && (int)this._DBSourceConfig.WebbDBType <= 105)
				{//Victory
					DataTable m_Filters = this.DataProvider.LoadWebbFilters();
					if(this.Step_ConfigFilters == null) this.Step_ConfigFilters = new ConfigFilters();
					this.Step_ConfigFilters.SetFilters(m_Filters);
					this.LoadControl(this.Step_ConfigFilters);
				}
				else
				{//Advantage
					Webb.Utilities.WaitingForm.CloseWaitingForm();
					return;
				}
				goto EXIT;
			}
			#endregion
			Webb.Utilities.WaitingForm.CloseWaitingForm();
			return;
EXIT:
			this._CurrentControl.PreControl = m_TempControl;
			UpdateWizardStatus();
			Webb.Utilities.WaitingForm.CloseWaitingForm();
		}

		private void LoadBackControl()
		{
			//Scott@2007-11-15 13:36 modified some of the following code.
			this.LoadControl(this._CurrentControl.PreControl);

			if(_CurrentControl is ConfigCommonSQL)
			{	
				_CurrentControl.SetData(this._DBSourceConfig);
			}


			UpdateWizardStatus();

			#region useless back steps
//			#region back to step 1
//			//
//			if(this._CurrentControl is ConfigDBFile)
//			{
//				if(this.Step_ConfigDBTypes==null) this.Step_ConfigDBTypes = new ConfigDBTypes();
//				this.LoadControl(this.Step_ConfigDBTypes);
//                this.WizardStatus &= ~WizardStatus.Back;
//				return;
//			}
//			if(this._CurrentControl is ConfigDBCatalog)
//			{
//				if(this.Step_ConfigDBTypes==null) this.Step_ConfigDBTypes = new ConfigDBTypes();
//				this.LoadControl(this.Step_ConfigDBTypes);
//				this.WizardStatus &= ~WizardStatus.Back;
//				return;
//			}
//			#endregion
//
//			#region back to step 2
//			if(this._CurrentControl is ConfigGames)
//			{
//				if(this._DBSourceConfig.DBConnType!=DBConnTypes.SQLDB)
//				{
//					//Access file or XML file.
//					if(this.Step_ConfigDBFile == null) this.Step_ConfigDBFile = new ConfigDBFile();
//					this.LoadControl(this.Step_ConfigDBFile);
//				}
//				else
//				{
//					//SQL DB Catalog.
//				}
//				return;
//			}
//			if(this._CurrentControl is ConfigTables)
//			{
//				if(this.Step_ConfigDBCatalog==null) this.Step_ConfigDBCatalog = new ConfigDBCatalog();
//				this.LoadControl(this.Step_ConfigDBCatalog);
//				return;
//			}
//			#endregion
//	
//			//11-12-2007@Scott
//			#region go to setup3
//			if(this._CurrentControl is ConfigFilters)
//			{
//				if(this._DBSourceConfig.WebbDBType != WebbDBTypes.Others)
//				{
//					//Config Access file
//					if(this.Step_ConfigGames == null) this.Step_ConfigGames = new ConfigGames();
//					this.LoadControl(this.Step_ConfigGames);
//				}
//				else
//				{
//					//Config SQL DB catalog.
//					if(this.Step_ConfigTable==null) this.Step_ConfigTable = new ConfigTables();
//					this.LoadControl(this.Step_ConfigTable);
//				}
//				return;
//			}
//			#endregion
			#endregion
		}
		#endregion

		//11-13-2007@Scott
		private void UpdateWizardStatus()
		{
			if(this._CurrentControl.FirstControl)
			{
				this.WizardStatus &= ~WizardStatus.Back;
			}
			else
			{
				this.WizardStatus |= WizardStatus.Back;
			}

			if(this._CurrentControl.LastControl)
			{
				this.WizardStatus &= ~WizardStatus.Next;
			}
			else
			{
				this.WizardStatus |= WizardStatus.Next;
			}

			if(this._CurrentControl.FinishControl)
			{
				this.WizardStatus |= WizardStatus.OK;
			}
			else
			{
				this.WizardStatus &= ~WizardStatus.OK;
			}
		}
	}
}

