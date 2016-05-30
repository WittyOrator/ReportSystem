using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Webb.Reports.ExControls.UI
{
	public class DF_HolePanel : Webb.Reports.ExControls.UI.ExControlDesignerFormBase
	{
		private ConfigHolePanelInfo C_HolePanelSetting;
		private System.Windows.Forms.LinkLabel C_LinkFollowField;
		private System.ComponentModel.IContainer components = null;

		public DF_HolePanel()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();
			
			// TODO: Add any initialization after the InitializeComponent call
			this.C_HolePanelSetting = new ConfigHolePanelInfo();

			this.Load += new EventHandler(DF_HolePanel_Load);
		}

		protected void DF_HolePanel_Load(object o, System.EventArgs e)
		{
			this.C_HolePanelSetting.SetView(this._ExControlView);
			
			this.LoadConfigControl(this.C_HolePanelSetting);
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
			this.C_LinkFollowField = new System.Windows.Forms.LinkLabel();
			this.C_AllTask.SuspendLayout();
			// 
			// _ExControlStyleControl
			// 
			this._ExControlStyleControl.Name = "_ExControlStyleControl";
			// 
			// C_MainPanel
			// 
			this.C_MainPanel.Name = "C_MainPanel";
			this.C_MainPanel.Size = new System.Drawing.Size(451, 325);
			// 
			// C_AllTask
			// 
			this.C_AllTask.Controls.Add(this.C_LinkFollowField);
			this.C_AllTask.Name = "C_AllTask";
			this.C_AllTask.Controls.SetChildIndex(this.C_LinkFollowField, 0);
			this.C_AllTask.Controls.SetChildIndex(this.C_AutoStyle, 0);
			// 
			// C_HideOrShowTask
			// 
			this.C_HideOrShowTask.Name = "C_HideOrShowTask";
			// 
			// C_LeftMenu
			// 
			this.C_LeftMenu.AutoScroll = true;
			this.C_LeftMenu.Name = "C_LeftMenu";
			this.C_LeftMenu.Size = new System.Drawing.Size(176, 365);
			// 
			// C_Splitter
			// 
			this.C_Splitter.Name = "C_Splitter";
			this.C_Splitter.Size = new System.Drawing.Size(5, 365);
			// 
			// C_AutoStyle
			// 
			this.C_AutoStyle.Location = new System.Drawing.Point(3, 41);
			this.C_AutoStyle.Name = "C_AutoStyle";
			this.C_AutoStyle.Visible = false;
			// 
			// C_LinkFollowField
			// 
			this.C_LinkFollowField.Dock = System.Windows.Forms.DockStyle.Top;
			this.C_LinkFollowField.Location = new System.Drawing.Point(3, 18);
			this.C_LinkFollowField.Name = "C_LinkFollowField";
			this.C_LinkFollowField.Size = new System.Drawing.Size(162, 23);
			this.C_LinkFollowField.TabIndex = 0;
			this.C_LinkFollowField.TabStop = true;
			this.C_LinkFollowField.Text = "Hole Panel Setting";
			this.C_LinkFollowField.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.C_LinkFollowField.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.C_LinkFollowField_LinkClicked);
			// 
			// DF_HolePanel
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(7, 15);
			this.ClientSize = new System.Drawing.Size(632, 365);
			this.Name = "DF_HolePanel";
			this.C_AllTask.ResumeLayout(false);

		}
		#endregion

		private void C_LinkFollowField_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			this.C_HolePanelSetting.SetView(this._ExControlView);
			
			this.LoadConfigControl(this.C_HolePanelSetting);
		}

		protected override void C_OK_Click(object sender, EventArgs e)
		{
			base.C_OK_Click (sender, e);
			this.C_HolePanelSetting.UpdateView(this._ExControlView);
			this._ExControlView.UpdateView();
		}

		protected override void C_Apply_Click(object sender, EventArgs e)
		{
			base.C_Apply_Click (sender, e);
			this.C_HolePanelSetting.UpdateView(this._ExControlView);
		}
	}
}
