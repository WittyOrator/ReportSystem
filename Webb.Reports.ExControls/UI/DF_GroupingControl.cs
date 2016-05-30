/***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:DF_GroupingControl.cs
 * Author:Country.Wu [EMail:Webb.Country.Wu@163.com]
 * Create Time:11/29/2007 12:59:40 PM
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

namespace Webb.Reports.ExControls.UI
{
	public class DF_GroupingControl : Webb.Reports.ExControls.UI.ExControlDesignerFormBase
	{
		private ConfigGroupingInfo _ConfigGroupControl;
		private System.Windows.Forms.LinkLabel C_GroupingSetting;
		private System.Windows.Forms.LinkLabel C_Layout;
		private ConfigGroupingLayout _ConfigLayoutControl;
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.LinkLabel C_HeadersConstruct;

		private ConfigHeaderConstructor _ConfigHeadersControl; //2008-8-28 9:56:19@simon
		public DF_GroupingControl()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
			this._ConfigGroupControl = new ConfigGroupingInfo();
			this._ConfigLayoutControl = new ConfigGroupingLayout();

			
			//2008-8-28 9:56:11@simon
			this._ConfigHeadersControl=new ConfigHeaderConstructor();
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
			this.C_GroupingSetting = new System.Windows.Forms.LinkLabel();
			this.C_Layout = new System.Windows.Forms.LinkLabel();
			this.C_HeadersConstruct = new System.Windows.Forms.LinkLabel();
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
			this.C_MainPanel.Location = new System.Drawing.Point(165, 0);
			this.C_MainPanel.Name = "C_MainPanel";
			this.C_MainPanel.Size = new System.Drawing.Size(677, 381);
			// 
			// C_AllTask
			// 
			this.C_AllTask.Controls.Add(this.C_HeadersConstruct);
			this.C_AllTask.Controls.Add(this.C_Layout);
			this.C_AllTask.Controls.Add(this.C_GroupingSetting);
			this.C_AllTask.Name = "C_AllTask";
			this.C_AllTask.Size = new System.Drawing.Size(152, 200);
			this.C_AllTask.Controls.SetChildIndex(this.C_GroupingSetting, 0);
			this.C_AllTask.Controls.SetChildIndex(this.C_AutoStyle, 0);
			this.C_AllTask.Controls.SetChildIndex(this.C_Layout, 0);
			this.C_AllTask.Controls.SetChildIndex(this.C_HeadersConstruct, 0);
			// 
			// C_HideOrShowTask
			// 
			this.C_HideOrShowTask.Name = "C_HideOrShowTask";
			// 
			// C_LeftMenu
			// 
			this.C_LeftMenu.Name = "C_LeftMenu";
			this.C_LeftMenu.Size = new System.Drawing.Size(160, 421);
			// 
			// C_Splitter
			// 
			this.C_Splitter.Location = new System.Drawing.Point(160, 0);
			this.C_Splitter.Name = "C_Splitter";
			this.C_Splitter.Size = new System.Drawing.Size(5, 421);
			// 
			// C_AutoStyle
			// 
			this.C_AutoStyle.Location = new System.Drawing.Point(3, 56);
			this.C_AutoStyle.Name = "C_AutoStyle";
			this.C_AutoStyle.Size = new System.Drawing.Size(146, 32);
			// 
			// C_GroupingSetting
			// 
			this.C_GroupingSetting.Dock = System.Windows.Forms.DockStyle.Top;
			this.C_GroupingSetting.Location = new System.Drawing.Point(3, 18);
			this.C_GroupingSetting.Name = "C_GroupingSetting";
			this.C_GroupingSetting.Size = new System.Drawing.Size(146, 38);
			this.C_GroupingSetting.TabIndex = 1;
			this.C_GroupingSetting.TabStop = true;
			this.C_GroupingSetting.Text = "Grouping Setting";
			this.C_GroupingSetting.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.C_GroupingSetting.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.C_GroupingSetting_LinkClicked);
			// 
			// C_Layout
			// 
			this.C_Layout.Dock = System.Windows.Forms.DockStyle.Top;
			this.C_Layout.Location = new System.Drawing.Point(3, 88);
			this.C_Layout.Name = "C_Layout";
			this.C_Layout.Size = new System.Drawing.Size(146, 32);
			this.C_Layout.TabIndex = 4;
			this.C_Layout.TabStop = true;
			this.C_Layout.Text = "Adjust Size";
			this.C_Layout.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.C_Layout.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.C_Layout_LinkClicked);
			// 
			// C_HeadersConstruct
			// 
			this.C_HeadersConstruct.Dock = System.Windows.Forms.DockStyle.Top;
			this.C_HeadersConstruct.Location = new System.Drawing.Point(3, 120);
			this.C_HeadersConstruct.Name = "C_HeadersConstruct";
			this.C_HeadersConstruct.Size = new System.Drawing.Size(146, 32);
			this.C_HeadersConstruct.TabIndex = 5;
			this.C_HeadersConstruct.TabStop = true;
			this.C_HeadersConstruct.Text = "Header Titles";
			this.C_HeadersConstruct.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.C_HeadersConstruct.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.C_HeadersConstruct_LinkClicked);
			// 
			// DF_GroupingControl
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(7, 15);
			this.ClientSize = new System.Drawing.Size(842, 421);
			this.Name = "DF_GroupingControl";
			this.Load += new System.EventHandler(this.DF_GroupingControl_Load);
			this.C_AllTask.ResumeLayout(false);
			this.C_LeftMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		//Scott@2008-01-02 16:15 modified some of the following code.
		public override void SetView(Webb.Reports.ExControls.Views.ExControlView i_View)
		{
			base.SetView (i_View);

			this.ControlsSetView(this._ExControlView);
		}

		public override void UpdateView(Webb.Reports.ExControls.Views.ExControlView i_View)
		{
			base.UpdateView (i_View);
			
			this.ControlsUpdateView(i_View);
		}

		//on load
		private void DF_GroupingControl_Load(object sender, System.EventArgs e)
		{
			this.ControlsSetView(this._ExControlView);
			
     		this.LoadConfigControl(this._ConfigGroupControl);
		}

		//group setting
		private void C_GroupingSetting_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			this._ConfigGroupControl.SetView(this._ExControlView);
			
			this.LoadConfigControl(this._ConfigGroupControl);
		}

		//layout setting
		private void C_Layout_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			this._ConfigLayoutControl.SetView(this._ExControlView);
			
			this.LoadConfigControl(this._ConfigLayoutControl);
		}

		//ok
		protected override void C_OK_Click(object sender, EventArgs e)
		{
			base.C_OK_Click (sender, e);
			
			this.ControlsUpdateView(this._ExControlView);
			
			this._ExControlView.UpdateView();
		}

		//apply
		protected override void C_Apply_Click(object sender, EventArgs e)
		{
			base.C_Apply_Click (sender, e);
			
			this.ControlsUpdateView(this._ExControlView);

			this._ExControlView.UpdateView();
		}

		private void ControlsUpdateView(Views.ExControlView i_View)
		{
			this._ConfigGroupControl.UpdateView(i_View);
			
            if (this._ConfigLayoutControl.Parent != null) this._ConfigLayoutControl.UpdateView(i_View);
			if (this._ConfigHeadersControl.Parent != null)this._ConfigHeadersControl.UpdateView(i_View);
		}

		private void ControlsSetView(Views.ExControlView i_View)
		{
			this._ConfigGroupControl.SetView(i_View);
			
			this._ConfigLayoutControl.SetView(i_View);

			this._ConfigHeadersControl.SetView(i_View); //2008-8-28 9:59:30@simon
		}

		private void C_HeadersConstruct_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			this._ConfigHeadersControl.SetView(this._ExControlView);
			
			this.LoadConfigControl(this._ConfigHeadersControl);
		}
		public void HideOthers()
		{
			this.C_AutoStyle.Visible=false;
			this.C_Layout.Visible=false;
			this.C_HeadersConstruct.Location=this.C_AutoStyle.Location;
		}

	

	
	}
}

