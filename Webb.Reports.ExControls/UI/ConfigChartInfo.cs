using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Webb.Reports.ExControls.UI
{
	public class ConfigChartInfo : Webb.Reports.ExControls.UI.ExControlDesignerControlBase
	{
		private Views.WebbChartView _MainView;

		private System.Windows.Forms.Panel C_PanelPreview;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Panel C_PanelControls;
		private System.Windows.Forms.ComboBox C_CBChartStatType;
		private System.Windows.Forms.PropertyGrid C_PropertyGrid;
		private System.ComponentModel.IContainer components = null;

		public ConfigChartInfo()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
			this.SetStyle(ControlStyles.AllPaintingInWmPaint,true);
			this.SetStyle(ControlStyles.DoubleBuffer,true);
			this.SetStyle(ControlStyles.UserPaint,true);
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
			this.C_PanelPreview = new System.Windows.Forms.Panel();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.C_PanelControls = new System.Windows.Forms.Panel();
			this.C_PropertyGrid = new System.Windows.Forms.PropertyGrid();
			this.C_CBChartStatType = new System.Windows.Forms.ComboBox();
			this.C_PanelControls.SuspendLayout();
			this.SuspendLayout();
			// 
			// C_PanelPreview
			// 
			this.C_PanelPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.C_PanelPreview.Dock = System.Windows.Forms.DockStyle.Fill;
			this.C_PanelPreview.Location = new System.Drawing.Point(0, 0);
			this.C_PanelPreview.Name = "C_PanelPreview";
			this.C_PanelPreview.Size = new System.Drawing.Size(392, 373);
			this.C_PanelPreview.TabIndex = 0;
			this.C_PanelPreview.SizeChanged += new System.EventHandler(this.C_PanelPreview_SizeChanged);
			this.C_PanelPreview.Paint += new System.Windows.Forms.PaintEventHandler(this.C_PanelPreview_Paint);
			// 
			// splitter1
			// 
			this.splitter1.BackColor = System.Drawing.Color.DarkGray;
			this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
			this.splitter1.Location = new System.Drawing.Point(392, 0);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(3, 373);
			this.splitter1.TabIndex = 1;
			this.splitter1.TabStop = false;
			// 
			// C_PanelControls
			// 
			this.C_PanelControls.Controls.Add(this.C_PropertyGrid);
			this.C_PanelControls.Dock = System.Windows.Forms.DockStyle.Right;
			this.C_PanelControls.Location = new System.Drawing.Point(395, 0);
			this.C_PanelControls.Name = "C_PanelControls";
			this.C_PanelControls.Size = new System.Drawing.Size(208, 373);
			this.C_PanelControls.TabIndex = 2;
			// 
			// C_PropertyGrid
			// 
			this.C_PropertyGrid.CommandsVisibleIfAvailable = true;
			this.C_PropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.C_PropertyGrid.LargeButtons = false;
			this.C_PropertyGrid.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.C_PropertyGrid.Location = new System.Drawing.Point(0, 0);
			this.C_PropertyGrid.Name = "C_PropertyGrid";
			this.C_PropertyGrid.Size = new System.Drawing.Size(208, 373);
			this.C_PropertyGrid.TabIndex = 0;
			this.C_PropertyGrid.Text = "PropertyGrid";
			this.C_PropertyGrid.ViewBackColor = System.Drawing.SystemColors.Window;
			this.C_PropertyGrid.ViewForeColor = System.Drawing.SystemColors.WindowText;
			this.C_PropertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.C_PropertyGrid_PropertyValueChanged);
			// 
			// C_CBChartStatType
			// 
			this.C_CBChartStatType.Location = new System.Drawing.Point(0, 0);
			this.C_CBChartStatType.Name = "C_CBChartStatType";
			this.C_CBChartStatType.TabIndex = 0;
			// 
			// ConfigChartInfo
			// 
			this.Controls.Add(this.C_PanelPreview);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.C_PanelControls);
			this.Name = "ConfigChartInfo";
			this.Size = new System.Drawing.Size(603, 373);
			this.C_PanelControls.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
		
		public override void SetView(Webb.Reports.ExControls.Views.ExControlView i_View)
		{
			if(i_View is Views.WebbChartView)
			{
				this._MainView = i_View as Views.WebbChartView;

				this.C_PropertyGrid.SelectedObject = this._MainView;
			}
		}

		public override void UpdateView(Webb.Reports.ExControls.Views.ExControlView i_View)
		{
			if(i_View is Views.WebbChartView)
			{
				this.C_PanelPreview.Refresh();
			}
		}

		private void C_PanelPreview_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			if(this._MainView != null)
			{
				e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
				this._MainView.UpdateView();
				this._MainView.DrawImage(e.Graphics,this.C_PanelPreview.ClientRectangle);
			}
		}

		private void C_PanelPreview_SizeChanged(object sender, System.EventArgs e)
		{
			if(Control.MouseButtons != MouseButtons.Left)
			{
				this.C_PanelPreview.Refresh();
			}
		}

		private void C_PropertyGrid_PropertyValueChanged(object s, System.Windows.Forms.PropertyValueChangedEventArgs e)
		{
			Type type = e.OldValue.GetType();

			if(type == typeof(Webb.Reports.ExControls.Data.SummaryTypes))
			{
				this._MainView.CheckSummaryType();
			}
		}
	}
}

