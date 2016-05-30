using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Webb.Reports.ExControls;
using Webb.Reports.ExControls.Data;
using Webb.Reports.ExControls.Views;

using DevExpress.XtraEditors.Controls;

namespace Webb.Reports.ExControls.UI
{
	public class ConfigChartExChartData : Webb.Reports.ExControls.UI.ExControlDesignerControlBase
	{
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.PropertyGrid C_PropertyGrid;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Button C_BtnAdd;
		private System.Windows.Forms.Button C_BtnRemove;
		private System.Windows.Forms.Button C_BtnMoveUp;
		private System.Windows.Forms.Button C_BtnMoveDown;
		private System.Windows.Forms.ImageList C_ImageList;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel3;
		private DevExpress.XtraEditors.ImageListBoxControl C_ListSeries;
		private System.ComponentModel.IContainer components = null;

		public ConfigChartExChartData():base()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			this.Load += new EventHandler(ConfigChartData_Load);
			// TODO: Add any initialization after the InitializeComponent call
		}

		public ConfigChartExChartData(ExControlDesignerFormBase i_DesignerForm) : base(i_DesignerForm)
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ConfigChartExChartData));
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.C_ListSeries = new DevExpress.XtraEditors.ImageListBoxControl();
			this.C_ImageList = new System.Windows.Forms.ImageList(this.components);
			this.panel3 = new System.Windows.Forms.Panel();
			this.C_BtnRemove = new System.Windows.Forms.Button();
			this.C_BtnAdd = new System.Windows.Forms.Button();
			this.C_BtnMoveDown = new System.Windows.Forms.Button();
			this.C_BtnMoveUp = new System.Windows.Forms.Button();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.C_PropertyGrid = new System.Windows.Forms.PropertyGrid();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.C_ListSeries)).BeginInit();
			this.panel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Controls.Add(this.splitter1);
			this.panel1.Controls.Add(this.C_PropertyGrid);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(680, 364);
			this.panel1.TabIndex = 0;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.C_ListSeries);
			this.panel2.Controls.Add(this.panel3);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(477, 364);
			this.panel2.TabIndex = 7;
			// 
			// C_ListSeries
			// 
			this.C_ListSeries.Dock = System.Windows.Forms.DockStyle.Fill;
			this.C_ListSeries.ImageList = this.C_ImageList;
			this.C_ListSeries.Location = new System.Drawing.Point(0, 0);
			this.C_ListSeries.Name = "C_ListSeries";
			this.C_ListSeries.Size = new System.Drawing.Size(477, 308);
			this.C_ListSeries.TabIndex = 9;
			this.C_ListSeries.SelectedIndexChanged += new System.EventHandler(this.C_ListSeries_SelectedIndexChanged);
			// 
			// C_ImageList
			// 
			this.C_ImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
			this.C_ImageList.ImageSize = new System.Drawing.Size(16, 16);
			this.C_ImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("C_ImageList.ImageStream")));
			this.C_ImageList.TransparentColor = System.Drawing.Color.Magenta;
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.C_BtnRemove);
			this.panel3.Controls.Add(this.C_BtnAdd);
			this.panel3.Controls.Add(this.C_BtnMoveDown);
			this.panel3.Controls.Add(this.C_BtnMoveUp);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel3.Location = new System.Drawing.Point(0, 308);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(477, 56);
			this.panel3.TabIndex = 8;
			// 
			// C_BtnRemove
			// 
			this.C_BtnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.C_BtnRemove.Location = new System.Drawing.Point(96, 16);
			this.C_BtnRemove.Name = "C_BtnRemove";
			this.C_BtnRemove.TabIndex = 4;
			this.C_BtnRemove.Text = "Remove";
			this.C_BtnRemove.Click += new System.EventHandler(this.C_BtnRemove_Click);
			// 
			// C_BtnAdd
			// 
			this.C_BtnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.C_BtnAdd.Location = new System.Drawing.Point(8, 16);
			this.C_BtnAdd.Name = "C_BtnAdd";
			this.C_BtnAdd.TabIndex = 3;
			this.C_BtnAdd.Text = "Add";
			this.C_BtnAdd.Click += new System.EventHandler(this.C_BtnAdd_Click);
			// 
			// C_BtnMoveDown
			// 
			this.C_BtnMoveDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.C_BtnMoveDown.Location = new System.Drawing.Point(272, 16);
			this.C_BtnMoveDown.Name = "C_BtnMoveDown";
			this.C_BtnMoveDown.Size = new System.Drawing.Size(96, 23);
			this.C_BtnMoveDown.TabIndex = 6;
			this.C_BtnMoveDown.Text = "Move Down";
			this.C_BtnMoveDown.Click += new System.EventHandler(this.C_BtnMoveDown_Click);
			// 
			// C_BtnMoveUp
			// 
			this.C_BtnMoveUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.C_BtnMoveUp.Location = new System.Drawing.Point(184, 16);
			this.C_BtnMoveUp.Name = "C_BtnMoveUp";
			this.C_BtnMoveUp.Size = new System.Drawing.Size(80, 23);
			this.C_BtnMoveUp.TabIndex = 5;
			this.C_BtnMoveUp.Text = "Move Up";
			this.C_BtnMoveUp.Click += new System.EventHandler(this.C_BtnMoveUp_Click);
			// 
			// splitter1
			// 
			this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
			this.splitter1.Location = new System.Drawing.Point(477, 0);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(3, 364);
			this.splitter1.TabIndex = 2;
			this.splitter1.TabStop = false;
			// 
			// C_PropertyGrid
			// 
			this.C_PropertyGrid.CommandsVisibleIfAvailable = true;
			this.C_PropertyGrid.Dock = System.Windows.Forms.DockStyle.Right;
			this.C_PropertyGrid.LargeButtons = false;
			this.C_PropertyGrid.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.C_PropertyGrid.Location = new System.Drawing.Point(480, 0);
			this.C_PropertyGrid.Name = "C_PropertyGrid";
			this.C_PropertyGrid.Size = new System.Drawing.Size(200, 364);
			this.C_PropertyGrid.TabIndex = 1;
			this.C_PropertyGrid.Text = "propertyGrid1";
			this.C_PropertyGrid.ViewBackColor = System.Drawing.SystemColors.Window;
			this.C_PropertyGrid.ViewForeColor = System.Drawing.SystemColors.WindowText;
			this.C_PropertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.C_PropertyGrid_PropertyValueChanged);
			// 
			// ConfigChartExChartData
			// 
			this.Controls.Add(this.panel1);
			this.Name = "ConfigChartExChartData";
			this.Size = new System.Drawing.Size(680, 364);
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.C_ListSeries)).EndInit();
			this.panel3.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void ConfigChartData_Load(object sender, EventArgs e)
		{
			
		}

		public override void SetView(Webb.Reports.ExControls.Views.ExControlView i_View)
		{
			this.C_ListSeries.Items.Clear();

			foreach(Series series in Setting.SeriesCollection)
			{
				ImageListBoxItem item = new ImageListBoxItem(series,0);

				this.C_ListSeries.Items.Add(item);
			}

			this.C_ListSeries.SelectedIndex = 0;
		}

		public override void UpdateView(Webb.Reports.ExControls.Views.ExControlView i_View)
		{
			
		}

		private void C_BtnAdd_Click(object sender, System.EventArgs e)
		{
			int index = this.C_ListSeries.SelectedIndex;

			if(index >= 0)
			{
				Data.Series series = new Series();

				Setting.SeriesCollection.Add(series);

				ImageListBoxItem item = new ImageListBoxItem(series,0);

				this.C_ListSeries.Items.Insert(index + 1,item);

				this.C_ListSeries.SelectedIndex = index + 1;
			}
		}

		private void C_BtnRemove_Click(object sender, System.EventArgs e)
		{
			if(this.C_ListSeries.Items.Count == 1) return;

			ImageListBoxItem selectedItem = this.C_ListSeries.SelectedItem as ImageListBoxItem;

			if(selectedItem != null && selectedItem.Value is Data.Series)
			{
				Setting.SeriesCollection.Remove(selectedItem.Value as Data.Series);

				this.C_ListSeries.Items.Remove(selectedItem);
			}
		}

		private void C_BtnMoveUp_Click(object sender, System.EventArgs e)
		{
			int index = this.C_ListSeries.SelectedIndex;
			
			if(index > 0)
			{
				object item = this.C_ListSeries.SelectedItem;

				this.C_ListSeries.Items.RemoveAt(index);

				this.C_ListSeries.Items.Insert(index - 1,item);

				this.C_ListSeries.SelectedIndex = index - 1;
                 
				Setting.SeriesCollection.Swap(index-1,index);

			}
		}

		private void C_BtnMoveDown_Click(object sender, System.EventArgs e)
		{
			int index = this.C_ListSeries.SelectedIndex;
			
			if(index < this.C_ListSeries.Items.Count - 1)
			{
				object item = this.C_ListSeries.SelectedItem;

				this.C_ListSeries.Items.RemoveAt(index);

				this.C_ListSeries.Items.Insert(index + 1,item);

				this.C_ListSeries.SelectedIndex = index + 1;

				Setting.SeriesCollection.Swap(index,index+1);
			}
		}

		private void C_ListSeries_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.C_ListSeries.SelectedItem != null)
			{
				this.C_PropertyGrid.SelectedObject = (this.C_ListSeries.SelectedItem as ImageListBoxItem).Value;
			}
		}

		private void C_PropertyGrid_PropertyValueChanged(object s, System.Windows.Forms.PropertyValueChangedEventArgs e)
		{
			this.C_ListSeries.Refresh();
		}

		private Data.WebbChartSetting Setting
		{
			get{return (this._DesignerForm as DF_ChartControlEx).Settings;}
		}
	}
}