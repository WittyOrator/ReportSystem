using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Webb.Reports.ExControls.Views;

namespace Webb.Reports.ExControls.UI
{
	public class DF_FieldPanel : Webb.Reports.ExControls.UI.ExControlDesignerFormBase
	{
		private ConfigFieldPanelInfo C_FieldPanelSetting;
		private ConfigFieldPanelLayout C_FieldPanelLayout;
		private ConfigGridInfo C_GridControlSetting;     //Added this code at 2008-11-20 14:58:05@Simon
		private System.Windows.Forms.LinkLabel C_LinkFieldPanelSetting;
		private System.Windows.Forms.LinkLabel C_LinkLayout;
		private System.ComponentModel.IContainer components = null;

		public DF_FieldPanel()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();
			
			// TODO: Add any initialization after the InitializeComponent call
			this.C_FieldPanelSetting = new ConfigFieldPanelInfo();
			this.C_FieldPanelLayout = new ConfigFieldPanelLayout();
			this.Load += new EventHandler(DF_FieldPanel_Load);

            C_GridControlSetting=new ConfigGridInfo();   //Added this code at 2008-11-20 15:28:38@Simon
		}

		protected void DF_FieldPanel_Load(object o, System.EventArgs e)
		{
			this.ControlsSetView();
			this.LoadConfigControl(this.C_FieldPanelLayout);
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
			this.C_LinkFieldPanelSetting = new System.Windows.Forms.LinkLabel();
			this.C_LinkLayout = new System.Windows.Forms.LinkLabel();
			this.C_AllTask.SuspendLayout();
			this.C_LeftMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// _ExControlStyleControl
			// 
			this._ExControlStyleControl.Name = "_ExControlStyleControl";
			// 
			// C_MainPanel
			// 
			this.C_MainPanel.Name = "C_MainPanel";
			this.C_MainPanel.Size = new System.Drawing.Size(651, 422);
			// 
			// C_AllTask
			// 
			this.C_AllTask.Controls.Add(this.C_LinkFieldPanelSetting);
			this.C_AllTask.Controls.Add(this.C_LinkLayout);
			this.C_AllTask.Name = "C_AllTask";
			this.C_AllTask.Controls.SetChildIndex(this.C_LinkLayout, 0);
			this.C_AllTask.Controls.SetChildIndex(this.C_LinkFieldPanelSetting, 0);
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
			this.C_LeftMenu.Size = new System.Drawing.Size(176, 462);
			// 
			// C_Splitter
			// 
			this.C_Splitter.Name = "C_Splitter";
			this.C_Splitter.Size = new System.Drawing.Size(5, 462);
			// 
			// C_AutoStyle
			// 
			this.C_AutoStyle.Location = new System.Drawing.Point(3, 64);
			this.C_AutoStyle.Name = "C_AutoStyle";
			this.C_AutoStyle.Size = new System.Drawing.Size(162, 23);
			this.C_AutoStyle.TabIndex = 2;
			// 
			// C_LinkFieldPanelSetting
			// 
			this.C_LinkFieldPanelSetting.Dock = System.Windows.Forms.DockStyle.Top;
			this.C_LinkFieldPanelSetting.Location = new System.Drawing.Point(3, 41);
			this.C_LinkFieldPanelSetting.Name = "C_LinkFieldPanelSetting";
			this.C_LinkFieldPanelSetting.Size = new System.Drawing.Size(162, 23);
			this.C_LinkFieldPanelSetting.TabIndex = 1;
			this.C_LinkFieldPanelSetting.TabStop = true;
			this.C_LinkFieldPanelSetting.Text = "Data  Setting";
			this.C_LinkFieldPanelSetting.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.C_LinkFieldPanelSetting.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.C_LinkField_LinkClicked);
			// 
			// C_LinkLayout
			// 
			this.C_LinkLayout.Dock = System.Windows.Forms.DockStyle.Top;
			this.C_LinkLayout.Location = new System.Drawing.Point(3, 18);
			this.C_LinkLayout.Name = "C_LinkLayout";
			this.C_LinkLayout.Size = new System.Drawing.Size(162, 23);
			this.C_LinkLayout.TabIndex = 3;
			this.C_LinkLayout.TabStop = true;
			this.C_LinkLayout.Text = "Layout Setting";
			this.C_LinkLayout.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.C_LinkLayout.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.C_LinkLayout_LinkClicked);
			// 
			// DF_FieldPanel
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(7, 15);
			this.ClientSize = new System.Drawing.Size(832, 462);
			this.Name = "DF_FieldPanel";
			this.C_AllTask.ResumeLayout(false);
			this.C_LeftMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		//Field Panel Setting
		private void C_LinkField_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			this.ControlsUpdateView();  //2009-5-6 9:43:46@Simon Add this Code

		    ComputedStyle style=(this._ExControlView as FieldPanelView).LayOut.FieldStyle;
			if(style==ComputedStyle.Group)
			{
				this.C_FieldPanelSetting.SetView(this._ExControlView);
				this.LoadConfigControl(this.C_FieldPanelSetting);
			}
			else
			{
				this.C_GridControlSetting.SetView(this._ExControlView); 
				this.LoadConfigControl(this.C_GridControlSetting);
			}
		}

		//Layout Setting
		private void C_LinkLayout_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			this.C_FieldPanelLayout.SetView(this._ExControlView);
			this.LoadConfigControl(this.C_FieldPanelLayout);
		}

		//OK
		protected override void C_OK_Click(object sender, EventArgs e)
		{
			base.C_OK_Click (sender, e);
			this.ControlsUpdateView();
			this._ExControlView.UpdateView();
		}

		//Apply
		protected override void C_Apply_Click(object sender, EventArgs e)
		{
			base.C_Apply_Click (sender, e);
			this.ControlsUpdateView();
			this._ExControlView.UpdateView();
		}

		private void ControlsUpdateView()
		{
			this.C_FieldPanelSetting.UpdateView(this._ExControlView);
			this.C_FieldPanelLayout.UpdateView(this._ExControlView);
			this.C_GridControlSetting.UpdateView(this._ExControlView); //Added this code at 2008-11-20 15:30:03@Simon
		}

		private void ControlsSetView()
		{
			this.C_FieldPanelSetting.SetView(this._ExControlView);
			this.C_FieldPanelLayout.SetView(this._ExControlView);
			this.C_GridControlSetting.SetView(this._ExControlView); //Added this code at 2008-11-20 15:30:03@Simon
			
		}

//		private void linkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
//		{
//     		this.C_GridControlSetting.SetView(this._ExControlView); 
//			this.LoadConfigControl(this.C_GridControlSetting);
//		}
	}
}