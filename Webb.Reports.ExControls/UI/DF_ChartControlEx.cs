using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Webb.Reports.ExControls.Data;

namespace Webb.Reports.ExControls.UI
{
	public class DF_ChartControlEx : Webb.Reports.ExControls.UI.ExControlDesignerFormBase
	{
		private ConfigChartExChartType C_ConfigChartType;
		private ConfigChartExChartData C_ConfigChartData;
		private ConfigChartExSeriesLabel C_ConfigChartSeriesLabel;
		private ConfigChartExAxes C_ConfigChartAxes;
		private ConfigChartExLengend C_ConfigChartLengend;
		private ConfigChartExChartPieLabel C_ConfigChartPieLabel;
		
		public Data.WebbChartSetting Settings;

		private System.Windows.Forms.LinkLabel C_LinkChartType;
		private System.Windows.Forms.LinkLabel C_LinkData;
		private System.Windows.Forms.LinkLabel C_LinkAxes;
		private System.Windows.Forms.LinkLabel C_LinkSeriesLabels;
		private System.Windows.Forms.LinkLabel C_LinkLengend;
		private System.Windows.Forms.LinkLabel C_LinkPieLabel;
		private System.ComponentModel.IContainer components = null;

		public DF_ChartControlEx()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
			this.Load += new EventHandler(DF_ChartControl_Load);

			this.C_ConfigChartType = new ConfigChartExChartType(this);
			this.C_ConfigChartData = new ConfigChartExChartData(this);
			this.C_ConfigChartSeriesLabel = new ConfigChartExSeriesLabel(this);
			this.C_ConfigChartAxes = new ConfigChartExAxes(this);
			this.C_ConfigChartLengend = new ConfigChartExLengend(this);
			this.C_ConfigChartPieLabel=new ConfigChartExChartPieLabel(this);  //2009-6-8 12:58:52@Simon Add this Code

			this.Text = "Chart Design";
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
			if(this._ExControlView is Views.WebbChartExView)
			{
				this.Settings = (this._ExControlView as Views.WebbChartExView).Settings.Copy();

				if(this.Settings.ChartType==ChartAppearanceType.Pie3D||this.Settings.ChartType==ChartAppearanceType.Pie)
				{
					this.C_LinkAxes.Visible=false;					
				}
				else
				{					
					this.C_LinkAxes.Visible=true;
					
				}
			}

			this.C_ConfigChartType.SetView(this._ExControlView);

			this.LoadConfigControl(this.C_ConfigChartType);

			this.Text = string.Format("Chart Design - {0}",this.C_LinkChartType.Text);
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.C_LinkChartType = new System.Windows.Forms.LinkLabel();
			this.C_LinkData = new System.Windows.Forms.LinkLabel();
			this.C_LinkAxes = new System.Windows.Forms.LinkLabel();
			this.C_LinkSeriesLabels = new System.Windows.Forms.LinkLabel();
			this.C_LinkLengend = new System.Windows.Forms.LinkLabel();
			this.C_LinkPieLabel = new System.Windows.Forms.LinkLabel();
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
			// 
			// C_AllTask
			// 
			this.C_AllTask.Controls.Add(this.C_LinkPieLabel);
			this.C_AllTask.Controls.Add(this.C_LinkLengend);
			this.C_AllTask.Controls.Add(this.C_LinkAxes);
			this.C_AllTask.Controls.Add(this.C_LinkSeriesLabels);
			this.C_AllTask.Controls.Add(this.C_LinkData);
			this.C_AllTask.Controls.Add(this.C_LinkChartType);
			this.C_AllTask.Name = "C_AllTask";
			this.C_AllTask.Controls.SetChildIndex(this.C_LinkChartType, 0);
			this.C_AllTask.Controls.SetChildIndex(this.C_LinkData, 0);
			this.C_AllTask.Controls.SetChildIndex(this.C_LinkSeriesLabels, 0);
			this.C_AllTask.Controls.SetChildIndex(this.C_LinkAxes, 0);
			this.C_AllTask.Controls.SetChildIndex(this.C_LinkLengend, 0);
			this.C_AllTask.Controls.SetChildIndex(this.C_LinkPieLabel, 0);
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
			this.C_AutoStyle.Location = new System.Drawing.Point(3, 157);
			this.C_AutoStyle.Name = "C_AutoStyle";
			this.C_AutoStyle.Visible = false;
			this.C_AutoStyle.Enter += new System.EventHandler(this.C_Link_Enter);
			// 
			// C_LinkChartType
			// 
			this.C_LinkChartType.Dock = System.Windows.Forms.DockStyle.Top;
			this.C_LinkChartType.Location = new System.Drawing.Point(3, 18);
			this.C_LinkChartType.Name = "C_LinkChartType";
			this.C_LinkChartType.Size = new System.Drawing.Size(162, 23);
			this.C_LinkChartType.TabIndex = 4;
			this.C_LinkChartType.TabStop = true;
			this.C_LinkChartType.Text = "Chart Type";
			this.C_LinkChartType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.C_LinkChartType.Enter += new System.EventHandler(this.C_Link_Enter);
			this.C_LinkChartType.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.C_LinkChartType_LinkClicked);
			// 
			// C_LinkData
			// 
			this.C_LinkData.Dock = System.Windows.Forms.DockStyle.Top;
			this.C_LinkData.Location = new System.Drawing.Point(3, 41);
			this.C_LinkData.Name = "C_LinkData";
			this.C_LinkData.Size = new System.Drawing.Size(162, 23);
			this.C_LinkData.TabIndex = 6;
			this.C_LinkData.TabStop = true;
			this.C_LinkData.Text = "Data && Style";
			this.C_LinkData.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.C_LinkData.Enter += new System.EventHandler(this.C_Link_Enter);
			this.C_LinkData.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.C_LinkData_LinkClicked);
			// 
			// C_LinkAxes
			// 
			this.C_LinkAxes.Dock = System.Windows.Forms.DockStyle.Top;
			this.C_LinkAxes.Location = new System.Drawing.Point(3, 87);
			this.C_LinkAxes.Name = "C_LinkAxes";
			this.C_LinkAxes.Size = new System.Drawing.Size(162, 23);
			this.C_LinkAxes.TabIndex = 7;
			this.C_LinkAxes.TabStop = true;
			this.C_LinkAxes.Text = "Axes";
			this.C_LinkAxes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.C_LinkAxes.Enter += new System.EventHandler(this.C_Link_Enter);
			this.C_LinkAxes.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.C_LinkAxes_LinkClicked);
			// 
			// C_LinkSeriesLabels
			// 
			this.C_LinkSeriesLabels.Dock = System.Windows.Forms.DockStyle.Top;
			this.C_LinkSeriesLabels.Location = new System.Drawing.Point(3, 64);
			this.C_LinkSeriesLabels.Name = "C_LinkSeriesLabels";
			this.C_LinkSeriesLabels.Size = new System.Drawing.Size(162, 23);
			this.C_LinkSeriesLabels.TabIndex = 8;
			this.C_LinkSeriesLabels.TabStop = true;
			this.C_LinkSeriesLabels.Text = "Series Labels";
			this.C_LinkSeriesLabels.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.C_LinkSeriesLabels.Enter += new System.EventHandler(this.C_Link_Enter);
			this.C_LinkSeriesLabels.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.C_LinkSeriesLabels_LinkClicked);
			// 
			// C_LinkLengend
			// 
			this.C_LinkLengend.Dock = System.Windows.Forms.DockStyle.Top;
			this.C_LinkLengend.Location = new System.Drawing.Point(3, 110);
			this.C_LinkLengend.Name = "C_LinkLengend";
			this.C_LinkLengend.Size = new System.Drawing.Size(162, 23);
			this.C_LinkLengend.TabIndex = 10;
			this.C_LinkLengend.TabStop = true;
			this.C_LinkLengend.Text = "Lengend";
			this.C_LinkLengend.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.C_LinkLengend.Enter += new System.EventHandler(this.C_Link_Enter);
			this.C_LinkLengend.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.C_LinkLengend_LinkClicked);
			// 
			// C_LinkPieLabel
			// 
			this.C_LinkPieLabel.Dock = System.Windows.Forms.DockStyle.Top;
			this.C_LinkPieLabel.Location = new System.Drawing.Point(3, 133);
			this.C_LinkPieLabel.Name = "C_LinkPieLabel";
			this.C_LinkPieLabel.Size = new System.Drawing.Size(162, 24);
			this.C_LinkPieLabel.TabIndex = 9;
			this.C_LinkPieLabel.TabStop = true;
			this.C_LinkPieLabel.Text = "Pie Titles";
			this.C_LinkPieLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.C_LinkPieLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.C_LinkPieLabel_LinkClicked);
			// 
			// DF_ChartControlEx
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(7, 15);
			this.ClientSize = new System.Drawing.Size(784, 413);
			this.Name = "DF_ChartControlEx";
			this.Closed += new System.EventHandler(this.DF_ChartControlEx_Closed);
			this.C_AllTask.ResumeLayout(false);
			this.C_LeftMenu.ResumeLayout(false);
			this.ResumeLayout(false);

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

		//Chart Type
		private void C_LinkChartType_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			this.ControlsUpdateView();

			this.C_ConfigChartType.SetView(this._ExControlView);

			this.LoadConfigControl(this.C_ConfigChartType);

			this.Text = string.Format("Chart Design - {0}",(sender as Control).Text);
		}

		//Series
		private void C_LinkData_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			this.ControlsUpdateView();

			this.C_ConfigChartData.SetView(this._ExControlView);

			this.LoadConfigControl(this.C_ConfigChartData);

			this.Text = string.Format("Chart Design - {0}",(sender as Control).Text);
		}

		//Series Labels
		private void C_LinkSeriesLabels_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			this.ControlsUpdateView();

			this.C_ConfigChartSeriesLabel.SetView(this._ExControlView);

			this.LoadConfigControl(this.C_ConfigChartSeriesLabel);

			this.Text = string.Format("Chart Design - {0}",(sender as Control).Text);
		}

		//Axes
		private void C_LinkAxes_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			this.ControlsUpdateView();
		
			this.C_ConfigChartAxes.SetView(this._ExControlView);

			this.LoadConfigControl(this.C_ConfigChartAxes);

			this.Text = string.Format("Chart Design - {0}",(sender as Control).Text);
		}

		//Lengend
		private void C_LinkLengend_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			this.ControlsUpdateView();

			this.C_ConfigChartLengend.SetView(this._ExControlView);

			this.LoadConfigControl(this.C_ConfigChartLengend);

			this.Text = string.Format("Chart Design - {0}",(sender as Control).Text);
		}

		//update view
		private void ControlsUpdateView()
		{
			this.C_ConfigChartType.UpdateView(this._ExControlView);
			this.C_ConfigChartData.UpdateView(this._ExControlView);
			this.C_ConfigChartSeriesLabel.UpdateView(this._ExControlView);
			this.C_ConfigChartAxes.UpdateView(this._ExControlView);
			this.C_ConfigChartLengend.UpdateView(this._ExControlView);
			this.C_ConfigChartPieLabel.UpdateView(this._ExControlView);
			if(this.Settings.ChartType==ChartAppearanceType.Pie3D||this.Settings.ChartType==ChartAppearanceType.Pie)
			{
				this.C_LinkAxes.Visible=false;
			}
			else
			{					
				this.C_LinkAxes.Visible=true;
					
			}
		}

		//set view
		private void ControlsSetView()
		{
			this.C_ConfigChartType.SetView(this._ExControlView);
			this.C_ConfigChartData.SetView(this._ExControlView);
			this.C_ConfigChartSeriesLabel.UpdateView(this._ExControlView);
			this.C_ConfigChartAxes.SetView(this._ExControlView);
			this.C_ConfigChartLengend.SetView(this._ExControlView);
	        this.C_ConfigChartPieLabel.SetView(this._ExControlView);

			if(this.Settings.ChartType==ChartAppearanceType.Pie3D||this.Settings.ChartType==ChartAppearanceType.Pie)
			{
				this.C_LinkAxes.Visible=false;
			}
			else
			{					
				this.C_LinkAxes.Visible=true;					
			}
		}

		//OK
		protected override void C_OK_Click(object sender, EventArgs e)
		{
			base.C_OK_Click (sender, e);
		
			#region Modify codes at 2008-12-10 14:14:12@Simon
			if(this.Settings.SelectedSeriesIndex>=0&&this.Settings.SelectedSeriesIndex<this.Settings.SeriesCollection.Count)
			{		
				this.Settings.SelectedSeriesIndex +=this.Settings.SeriesCollection.Count;
			}
			else
			{
				this.Settings.SelectedSeriesIndex = -1;
			}
			#endregion        //End Modify
	
			if(this._ExControlView is Views.WebbChartExView)
			{
				Test3DState();
				(this._ExControlView as Views.WebbChartExView).Settings = this.Settings.Copy();
			}

			this._ExControlView.UpdateView();
		}

		//Apply
		protected override void C_Apply_Click(object sender, EventArgs e)
		{
			base.C_Apply_Click (sender, e);
		
			#region Modify codes at 2008-12-10 14:14:12@Simon
			if(this.Settings.SelectedSeriesIndex>=0&&this.Settings.SelectedSeriesIndex<this.Settings.SeriesCollection.Count)
			{		
				this.Settings.SelectedSeriesIndex +=this.Settings.SeriesCollection.Count;
			}
			else
			{
				this.Settings.SelectedSeriesIndex = -1;
			}
			#endregion        //End Modify
	
			if(this._ExControlView is Views.WebbChartExView)
			{
				Test3DState();
				(this._ExControlView as Views.WebbChartExView).Settings = this.Settings.Copy();			

			}
			
			this._ExControlView.UpdateView();
		}
		private void Test3DState()
		{
			if(this.Settings.ChartType<ChartAppearanceType.Pie3D)
			{
              this._ExControlView.ThreeD=false;

			}
			else
			{
			  this._ExControlView.ThreeD=true;
			}
			if(this.Settings.ChartType==ChartAppearanceType.Pie3D||this.Settings.ChartType==ChartAppearanceType.Pie)
			{
				this.C_LinkAxes.Visible=false;
			}
			else
			{					
				this.C_LinkAxes.Visible=true;
					
			}
		}

		private void C_Link_Enter(object sender, System.EventArgs e)
		{
			this.FocusCurrentConfigControl();
		}

		private void DF_ChartControlEx_Closed(object sender, System.EventArgs e)
		{

		}

		private void C_LinkPieLabel_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			this.ControlsUpdateView();

			this.C_ConfigChartPieLabel.SetView(this._ExControlView);

			this.LoadConfigControl(this.C_ConfigChartPieLabel);

			this.Text = string.Format("Chart Design - {0}",(sender as Control).Text);
		}
	}
}

