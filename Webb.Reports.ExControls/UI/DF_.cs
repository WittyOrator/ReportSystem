using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Webb.Reports.ExControls.UI
{
	public class DF_HolePanel : Webb.Reports.ExControls.UI.ExControlDesignerFormBase
	{
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.LinkLabel linkLabel2;
		private System.Windows.Forms.LinkLabel linkLabel3;
		private System.ComponentModel.IContainer components = null;

		public DF_HolePanel()
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
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.linkLabel2 = new System.Windows.Forms.LinkLabel();
			this.linkLabel3 = new System.Windows.Forms.LinkLabel();
			this.C_AllTask.SuspendLayout();
			// 
			// C_MainPanel
			// 
			this.C_MainPanel.Name = "C_MainPanel";
			// 
			// C_AllTask
			// 
			this.C_AllTask.Controls.Add(this.linkLabel3);
			this.C_AllTask.Controls.Add(this.linkLabel2);
			this.C_AllTask.Controls.Add(this.linkLabel1);
			this.C_AllTask.Dock = System.Windows.Forms.DockStyle.Fill;
			this.C_AllTask.Name = "C_AllTask";
			this.C_AllTask.Size = new System.Drawing.Size(120, 357);
			// 
			// C_HideOrShowTask
			// 
			this.C_HideOrShowTask.Name = "C_HideOrShowTask";
			// 
			// C_LeftMenu
			// 
			this.C_LeftMenu.AutoScroll = true;
			this.C_LeftMenu.Name = "C_LeftMenu";
			this.C_LeftMenu.Visible = false;
			// 
			// C_Splitter
			// 
			this.C_Splitter.Name = "C_Splitter";
			// 
			// linkLabel1
			// 
			this.linkLabel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.linkLabel1.Location = new System.Drawing.Point(3, 18);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(114, 23);
			this.linkLabel1.TabIndex = 0;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "linkLabel1";
			this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// linkLabel2
			// 
			this.linkLabel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.linkLabel2.Location = new System.Drawing.Point(3, 41);
			this.linkLabel2.Name = "linkLabel2";
			this.linkLabel2.Size = new System.Drawing.Size(114, 23);
			this.linkLabel2.TabIndex = 1;
			this.linkLabel2.TabStop = true;
			this.linkLabel2.Text = "linkLabel2";
			this.linkLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// linkLabel3
			// 
			this.linkLabel3.Dock = System.Windows.Forms.DockStyle.Top;
			this.linkLabel3.Location = new System.Drawing.Point(3, 64);
			this.linkLabel3.Name = "linkLabel3";
			this.linkLabel3.Size = new System.Drawing.Size(114, 23);
			this.linkLabel3.TabIndex = 2;
			this.linkLabel3.TabStop = true;
			this.linkLabel3.Text = "linkLabel3";
			this.linkLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// DF_ReportInfoLabel
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(7, 15);
			this.ClientSize = new System.Drawing.Size(632, 365);
			this.Name = "DF_ReportInfoLabel";
			this.C_AllTask.ResumeLayout(false);

		}
		#endregion
	}
}
