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
	/// <summary>
	/// Summary description for DataSourceWizard.
	/// </summary>
	public class DataSourceWizard: Webb.Utilities.Wizards.WizardBaseForm
	{
		public ConfigAdvFile Step_ConfigAdvFile;
	
        private WebbDataProvider _DataProvider;
		private DBSourceConfig _DBSourceConfig;	
		
		public DataSourceWizard():base()
		{
			_DBSourceConfig=new DBSourceConfig();
         
			
		}
		public void InitControl()
		{		

			if(this._DBSourceConfig==null)_DBSourceConfig=new DBSourceConfig();

			this._DBSourceConfig.ConnString = string.Empty;	
				
			_DBSourceConfig.WebbDBType = WebbDBTypes.WebbAdvantageFootball;

			_DBSourceConfig.DBConnType=DBConnTypes.File;
			
			if(this.Step_ConfigAdvFile == null) this.Step_ConfigAdvFile = new ConfigAdvFile();

			this.Step_ConfigAdvFile.SetData(this._DBSourceConfig);

			this.LoadControl(this.Step_ConfigAdvFile);	

			   this.HideNextAndBack();

			
		}
		public override void OnOK()
		{				
			this.Step_ConfigAdvFile.UpdateData(this._DBSourceConfig);		
			

			this.DialogResult = DialogResult.OK;
			
		}
		public override void OnCancel()
		{
			this.DialogResult = DialogResult.Cancel;
		}

		private void InitializeComponent()
		{
			// 
			// C_Next
			// 
			this.C_Next.Location = new System.Drawing.Point(470, 16);
			this.C_Next.Name = "C_Next";
			// 
			// C_Back
			// 
			this.C_Back.Location = new System.Drawing.Point(390, 16);
			this.C_Back.Name = "C_Back";
			// 
			// C_SelectAll
			// 
			this.C_SelectAll.Name = "C_SelectAll";
			// 
			// C_ClearAll
			// 
			this.C_ClearAll.Name = "C_ClearAll";
			// 
			// DataSourceWizard
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(7, 15);
			this.ClientSize = new System.Drawing.Size(720, 486);
			this.Name = "DataSourceWizard";

		}
	
		public  WebbDataProvider GetDatatprovider()
		{
			return new WebbDataProvider(this._DBSourceConfig);
		}

	}
}
