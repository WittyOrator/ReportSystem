using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Webb.Reports.ReportManager.UI
{
	public class ReportSelector : Webb.Utilities.Wizards.WizardBaseForm
	{
		private ReportSelectorControl _RepSelectControl;
		//
		private System.ComponentModel.IContainer components = null;

		public ReportSelector()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
			this._RepSelectControl = new ReportSelectorControl();
			//
			this.LoadControl(this._RepSelectControl);
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
			// ReportSelector
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(7, 15);
			this.ClientSize = new System.Drawing.Size(554, 375);
			this.Name = "ReportSelector";
			this.Load += new System.EventHandler(this.ReportSelector_Load);

		}
		#endregion

		public override void UpdateData(object i_Data)
		{
			//base.UpdateData (i_Data);
		}

		public override void SetData(object i_Data)
		{
			//base.SetData (i_Data);
		}

		private void ReportSelector_Load(object sender, System.EventArgs e)
		{
			ReportTemplateCollection m_Report = ReportManager.LoadAllTemplates();
			this._RepSelectControl.InitReports(m_Report);
		}

		public void GetSelectedReports(ReportTemplateCollection i_Reports)
		{
			this._RepSelectControl.GetSelectReports(i_Reports);
		}
	}
}

