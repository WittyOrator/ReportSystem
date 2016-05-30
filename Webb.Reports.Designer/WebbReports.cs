/***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:WebbReports.cs
 * Author:Country.Wu [EMail:Webb.Country.Wu@163.com]
 * Create Time:11/23/2007 04:41:57 PM
 * Copyright:1986-2007@Webb Electronics all right reserved.
 * Purpose:
 * ***********************************************************************/
#region History
/*
 * //Author@DateTime : Description
 * */
#endregion History

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using System.Drawing.Design;
using System.Diagnostics;





namespace Webb.Reports.Designer
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class WebbReports : Webb.Reports.ReportDesigner.ReportDesignerBase
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public WebbReports()
		{	
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(WebbReports));
			// 
			// WebbReports
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(1016, 693);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "WebbReports";
			this.Text = "Webb Report Designer Tool";
		}
		#endregion

	
		protected override void ShowReportWizard()
		{		
			DialogResult m_result = this.C_ReportDesignPanel.SaveChangedReport();

			if(m_result==DialogResult.Cancel)
			{
				return;
			}

			this.RemoveDataSource();
		
            string WizardFilename=@"C:\Program Files\Webb Electronics\WebbRepWizard\WebbRepWizard.exe";

			if(!System.IO.File.Exists(WizardFilename))
			{
				MessageBox.Show("please Install \"WebbRepWizard\" program first!","No Wizards",MessageBoxButtons.OK,MessageBoxIcon.Stop);
				
				return;
			}
			
			Process process=new Process();

            process.StartInfo.FileName=WizardFilename;

			process.StartInfo.Arguments="Wizard";

			
		     process.Start(); 
			process.WaitForExit();

		
			
		}

		
	}
}
