using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Webb.Reports.ExControls;
using Webb.Reports.ExControls.Data;
using Webb.Reports.ExControls.Views;

namespace Webb.Reports.ExControls.UI
{
	public class ConfigChartExSeriesLabel : Webb.Reports.ExControls.UI.ExControlDesignerControlBase
	{
		private System.Windows.Forms.Panel C_Panel;
		private System.Windows.Forms.PropertyGrid C_PropertyGrid;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.PictureBox C_PBPreview;
		private System.Windows.Forms.ComboBox C_CBSeriesName;
		private System.Windows.Forms.Panel C_PanelR;
		private System.ComponentModel.IContainer components = null;

		public ConfigChartExSeriesLabel():base()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			this.Load += new EventHandler(ConfigChartData_Load);
			// TODO: Add any initialization after the InitializeComponent call
		}

		public ConfigChartExSeriesLabel(ExControlDesignerFormBase i_DesignerForm) : base(i_DesignerForm)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
			this._DesignerForm = i_DesignerForm;
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
			this.C_Panel = new System.Windows.Forms.Panel();
			this.C_PBPreview = new System.Windows.Forms.PictureBox();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.C_PanelR = new System.Windows.Forms.Panel();
			this.C_PropertyGrid = new System.Windows.Forms.PropertyGrid();
			this.C_CBSeriesName = new System.Windows.Forms.ComboBox();
			this.C_Panel.SuspendLayout();
			this.C_PanelR.SuspendLayout();
			this.SuspendLayout();
			// 
			// C_Panel
			// 
			this.C_Panel.Controls.Add(this.C_PBPreview);
			this.C_Panel.Controls.Add(this.splitter1);
			this.C_Panel.Controls.Add(this.C_PanelR);
			this.C_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.C_Panel.Location = new System.Drawing.Point(0, 0);
			this.C_Panel.Name = "C_Panel";
			this.C_Panel.Size = new System.Drawing.Size(680, 364);
			this.C_Panel.TabIndex = 0;
			// 
			// C_PBPreview
			// 
			this.C_PBPreview.BackColor = System.Drawing.Color.White;
			this.C_PBPreview.Dock = System.Windows.Forms.DockStyle.Fill;
			this.C_PBPreview.Location = new System.Drawing.Point(0, 0);
			this.C_PBPreview.Name = "C_PBPreview";
			this.C_PBPreview.Size = new System.Drawing.Size(497, 364);
			this.C_PBPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.C_PBPreview.TabIndex = 0;
			this.C_PBPreview.TabStop = false;
			this.C_PBPreview.Paint += new System.Windows.Forms.PaintEventHandler(this.C_PBPreview_Paint);
			// 
			// splitter1
			// 
			this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
			this.splitter1.Location = new System.Drawing.Point(497, 0);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(3, 364);
			this.splitter1.TabIndex = 1;
			this.splitter1.TabStop = false;
			// 
			// C_PanelR
			// 
			this.C_PanelR.Controls.Add(this.C_PropertyGrid);
			this.C_PanelR.Controls.Add(this.C_CBSeriesName);
			this.C_PanelR.Dock = System.Windows.Forms.DockStyle.Right;
			this.C_PanelR.Location = new System.Drawing.Point(500, 0);
			this.C_PanelR.Name = "C_PanelR";
			this.C_PanelR.Size = new System.Drawing.Size(180, 364);
			this.C_PanelR.TabIndex = 3;
			// 
			// C_PropertyGrid
			// 
			this.C_PropertyGrid.CommandsVisibleIfAvailable = true;
			this.C_PropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.C_PropertyGrid.LargeButtons = false;
			this.C_PropertyGrid.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.C_PropertyGrid.Location = new System.Drawing.Point(0, 22);
			this.C_PropertyGrid.Name = "C_PropertyGrid";
			this.C_PropertyGrid.Size = new System.Drawing.Size(180, 342);
			this.C_PropertyGrid.TabIndex = 0;
			this.C_PropertyGrid.Text = "propertyGrid1";
			this.C_PropertyGrid.ViewBackColor = System.Drawing.SystemColors.Window;
			this.C_PropertyGrid.ViewForeColor = System.Drawing.SystemColors.WindowText;
			this.C_PropertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.C_PropertyGrid_PropertyValueChanged);
			// 
			// C_CBSeriesName
			// 
			this.C_CBSeriesName.Dock = System.Windows.Forms.DockStyle.Top;
			this.C_CBSeriesName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.C_CBSeriesName.DropDownWidth = 104;
			this.C_CBSeriesName.Location = new System.Drawing.Point(0, 0);
			this.C_CBSeriesName.Name = "C_CBSeriesName";
			this.C_CBSeriesName.Size = new System.Drawing.Size(180, 22);
			this.C_CBSeriesName.TabIndex = 2;
			this.C_CBSeriesName.SelectedIndexChanged += new System.EventHandler(this.C_CBSeriesName_SelectedIndexChanged);
			// 
			// ConfigChartExSeriesLabel
			// 
			this.Controls.Add(this.C_Panel);
			this.Name = "ConfigChartExSeriesLabel";
			this.Size = new System.Drawing.Size(680, 364);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.ConfigChartExChartType_Paint);
			this.C_Panel.ResumeLayout(false);
			this.C_PanelR.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void ConfigChartData_Load(object sender, EventArgs e)
		{

		}

		public override void SetView(Webb.Reports.ExControls.Views.ExControlView i_View)
		{
			if(i_View is WebbChartExView)
			{
				WebbChartExView mainView = i_View as WebbChartExView;

				this.C_CBSeriesName.Items.Clear();

				foreach(Series series in Setting.SeriesCollection)
				{
					this.C_CBSeriesName.Items.Add(series.SeriesLabel);
				}

				if(this.C_CBSeriesName.Items.Count > 0)
				{
					this.C_CBSeriesName.SelectedIndex = 0;
				}
			}
		}

		public override void UpdateView(Webb.Reports.ExControls.Views.ExControlView i_View)
		{	
			if(i_View is WebbChartExView)
			{
				WebbChartExView mainView = i_View as WebbChartExView;
			}
		}

		private void ConfigChartExChartType_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			
		}

		private void C_CBSeriesName_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.C_CBSeriesName.SelectedItem != null)
			{
				this.C_PropertyGrid.SelectedObject = this.C_CBSeriesName.SelectedItem;

				Setting.SelectedSeriesIndex = this.C_CBSeriesName.SelectedIndex;

				this.C_PBPreview.Refresh();
			}
			else
			{
				Setting.SelectedSeriesIndex = -1;
			}
		}

		private void C_PBPreview_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			ChartBase chart = null;

			if(this._DesignerForm != null && this._DesignerForm.ExControlView is WebbChartExView)
			{//12-16-2008@Scott
				WebbChartExView chartView = this._DesignerForm.ExControlView as WebbChartExView;

				System.Data.DataTable table = chartView.ExControl.GetDataSource();

				if(table == null)
				{	
					chart = Setting.CreateChart(View.ExControl.GetDataSource(),null);
				}
				else
				{
					Webb.Collections.Int32Collection rows = chartView.Filter.GetFilteredRows(table);

					if(chartView.ExControl!=null)
					{
						if(chartView.ExControl.Report!=null)
						{
							rows=chartView.ExControl.Report.Filter.GetFilteredRows(table,rows);  //2009-5-25 11:02:57@Simon Add this Code
						}						

					}
			
					chart = Setting.CreateChart(View.ExControl.GetDataSource(),rows);
				}
			}
			else
			{
				chart = Setting.CreateChart(View.ExControl.GetDataSource(),null);
			}

			if(chart != null) chart.Draw(e.Graphics,this.C_PBPreview.ClientRectangle);
		}

		private void C_PropertyGrid_PropertyValueChanged(object s, System.Windows.Forms.PropertyValueChangedEventArgs e)
		{
			this.C_PBPreview.Refresh();
		}

		private Data.WebbChartSetting Setting
		{
			get{return (this._DesignerForm as DF_ChartControlEx).Settings;}
		}

		private Views.WebbChartExView View
		{
			get{return this._DesignerForm.ExControlView as WebbChartExView;}
		}
	}
}