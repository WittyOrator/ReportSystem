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

namespace Webb.Reports.DataProvider.UI
{
	public class ReportDBConfigWizardForm : Webb.Utilities.Wizards.WizardBaseForm
	{
		private System.ComponentModel.IContainer components = null;

		public ReportDBConfigWizardForm()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

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
			components = new System.ComponentModel.Container();
		}
		#endregion
	}
}

