using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Webb.Reports.ExControls.UI
{
	public class DF_ChartControl : Webb.Reports.ExControls.UI.ExControlDesignerFormBase
	{
		private ConfigChartData C_ChartDataSetting;
		private ConfigChartInfo C_ChartInfoSetting;

		private System.Windows.Forms.LinkLabel C_LinkChartType;
		private System.Windows.Forms.LinkLabel C_LinkChartInfo;
		private System.Windows.Forms.LinkLabel C_LinkData;
		private System.ComponentModel.IContainer components = null;

		public DF_ChartControl()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
			this.Load += new EventHandler(DF_ChartControl_Load);

			this.C_ChartDataSetting = new ConfigChartData();
			this.C_ChartInfoSetting = new ConfigChartInfo();
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

		private void DF_ChartControl_Load(object sender, EventArgs e)
		{
			this.ControlsSetView();
			this.LoadConfigControl(this.C_ChartDataSetting);
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.C_LinkChartType = new System.Windows.Forms.LinkLabel();
			this.C_LinkChartInfo = new System.Windows.Forms.LinkLabel();
			this.C_LinkData = new System.Windows.Forms.LinkLabel();
			this.C_AllTask.SuspendLayout();
			// 
			// _ExControlStyleControl
			// 
			this._ExControlStyleControl.Name = "_ExControlStyleControl";
			// 
			// C_MainPanel
			// 
			this.C_MainPanel.Name = "C_MainPanel";
			// 
			// C_AllTask
			// 
			this.C_AllTask.Controls.Add(this.C_LinkChartType);
			this.C_AllTask.Controls.Add(this.C_LinkChartInfo);
			this.C_AllTask.Controls.Add(this.C_LinkData);
			this.C_AllTask.Name = "C_AllTask";
			this.C_AllTask.Controls.SetChildIndex(this.C_LinkData, 0);
			this.C_AllTask.Controls.SetChildIndex(this.C_LinkChartInfo, 0);
			this.C_AllTask.Controls.SetChildIndex(this.C_LinkChartType, 0);
			this.C_AllTask.Controls.SetChildIndex(this.C_AutoStyle, 0);
			// 
			// C_HideOrShowTask
			// 
			this.C_HideOrShowTask.Name = "C_HideOrShowTask";
			// 
			// C_LeftMenu
			// 
			this.C_LeftMenu.Name = "C_LeftMenu";
			// 
			// C_Splitter
			// 
			this.C_Splitter.Name = "C_Splitter";
			// 
			// C_AutoStyle
			// 
			this.C_AutoStyle.Location = new System.Drawing.Point(3, 87);
			this.C_AutoStyle.Name = "C_AutoStyle";
			this.C_AutoStyle.Visible = false;
			// 
			// C_LinkChartType
			// 
			this.C_LinkChartType.Dock = System.Windows.Forms.DockStyle.Top;
			this.C_LinkChartType.Location = new System.Drawing.Point(3, 64);
			this.C_LinkChartType.Name = "C_LinkChartType";
			this.C_LinkChartType.Size = new System.Drawing.Size(162, 23);
			this.C_LinkChartType.TabIndex = 4;
			this.C_LinkChartType.TabStop = true;
			this.C_LinkChartType.Text = "Chart Type";
			this.C_LinkChartType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.C_LinkChartType.Visible = false;
			this.C_LinkChartType.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.C_LinkChartType_LinkClicked);
			// 
			// C_LinkChartInfo
			// 
			this.C_LinkChartInfo.Dock = System.Windows.Forms.DockStyle.Top;
			this.C_LinkChartInfo.Location = new System.Drawing.Point(3, 41);
			this.C_LinkChartInfo.Name = "C_LinkChartInfo";
			this.C_LinkChartInfo.Size = new System.Drawing.Size(162, 23);
			this.C_LinkChartInfo.TabIndex = 5;
			this.C_LinkChartInfo.TabStop = true;
			this.C_LinkChartInfo.Text = "Chart Info";
			this.C_LinkChartInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.C_LinkChartInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.C_LinkChartInfo_LinkClicked);
			// 
			// C_LinkData
			// 
			this.C_LinkData.Dock = System.Windows.Forms.DockStyle.Top;
			this.C_LinkData.Location = new System.Drawing.Point(3, 18);
			this.C_LinkData.Name = "C_LinkData";
			this.C_LinkData.Size = new System.Drawing.Size(162, 23);
			this.C_LinkData.TabIndex = 6;
			this.C_LinkData.TabStop = true;
			this.C_LinkData.Text = "Data";
			this.C_LinkData.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.C_LinkData.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.C_LinkData_LinkClicked);
			// 
			// DF_ChartControl
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(7, 15);
			this.ClientSize = new System.Drawing.Size(784, 413);
			this.Name = "DF_ChartControl";
			this.C_AllTask.ResumeLayout(false);

		}
		#endregion

		public override void SetView(Webb.Reports.ExControls.Views.ExControlView i_View)
		{
			base.SetView (i_View);
		}

		public override void UpdateView(Webb.Reports.ExControls.Views.ExControlView i_View)
		{
			base.UpdateView (i_View);
		}

		private void C_LinkChartType_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			
		}

		private void C_LinkChartInfo_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			this.C_ChartInfoSetting.SetView(this._ExControlView);
			
			this.LoadConfigControl(this.C_ChartInfoSetting);
		}

		private void C_LinkData_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			this.C_ChartDataSetting.SetView(this._ExControlView);

			this.LoadConfigControl(this.C_ChartDataSetting);
		}

		private void ControlsUpdateView()
		{
			this.C_ChartInfoSetting.UpdateView(this._ExControlView);

			this.C_ChartDataSetting.UpdateView(this._ExControlView);
		}

		private void ControlsSetView()
		{
			this.C_ChartInfoSetting.SetView(this._ExControlView);

			this.C_ChartDataSetting.SetView(this._ExControlView);
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
	}
}

