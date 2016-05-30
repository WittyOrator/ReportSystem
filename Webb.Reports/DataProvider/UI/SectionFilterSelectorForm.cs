/***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:ReportDBConfigWizardForm.cs
 * Author:Country.Wu [EMail:Webb.Country.Wu@163.com]
 * Create Time:11/9/2007 10:39:50 AM
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
//
using Webb;
using Webb.Data;
using Webb.Utilities;
using Webb.Collections;
using Webb.Utilities.Wizards;

namespace Webb.Reports.DataProvider.UI
{
	public class SectionFilterSelectorForm : Webb.Utilities.Wizards.WizardBaseForm
	{
		private System.ComponentModel.IContainer components = null;
		private UI.SectionFilterSelector _ConfigFilters = null;
		private WebbDataProvider _DataProvider = null;

		public SectionFilterSelectorForm(WebbDataProvider i_DataProvider)
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
			// 
			// SectionFilterSelectorForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(7, 15);
			this.ClientSize = new System.Drawing.Size(554, 375);
			this.Name = "SectionFilterSelectorForm";
			this.Load += new System.EventHandler(this.SectionFilterSelectorForm_Load);

		}
		#endregion

		private void SectionFilterSelectorForm_Load(object sender, System.EventArgs e)
		{
			this.SetWizardStatus();

			this._ConfigFilters = new SectionFilterSelector();
			
			this._ConfigFilters.WizardTitle = "Select Section Filters:";

			try
			{
				this._ConfigFilters.SetFilters(this._DataProvider.LoadWebbFilters());
			}
			catch
			{
				MessageBox.Show("Please config data provider.");
			}

			this.LoadControl(this._ConfigFilters);
		}

		private void SetWizardStatus()
		{
			this.WizardStatus = 0;

			this.WizardStatus |= WizardStatus.OK | WizardStatus.Cancel;
		}

		public override void OnOK()
		{
			//base.OnOK ();
			try
			{
				this._ConfigFilters.UpdateData(this._DataProvider.DBSourceConfig);
			}
			catch
			{
				MessageBox.Show("Please config data provider.");
				this.DialogResult = DialogResult.Cancel;
			}
			this.DialogResult = DialogResult.OK;
			this.Close();
		}
	}
}

