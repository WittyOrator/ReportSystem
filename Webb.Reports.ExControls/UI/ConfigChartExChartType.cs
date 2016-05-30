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
	public class ConfigChartExChartType : Webb.Reports.ExControls.UI.ExControlDesignerControlBase
	{
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ImageList C_ImageList;
		private DevExpress.XtraEditors.ImageListBoxControl C_ListChartType;
		private System.ComponentModel.IContainer components = null;

		public ConfigChartExChartType():base()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
			
		}

		public ConfigChartExChartType(ExControlDesignerFormBase i_DesignerForm) : base(i_DesignerForm)
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ConfigChartExChartType));
			this.panel1 = new System.Windows.Forms.Panel();
			this.C_ListChartType = new DevExpress.XtraEditors.ImageListBoxControl();
			this.C_ImageList = new System.Windows.Forms.ImageList(this.components);
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.C_ListChartType)).BeginInit();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.C_ListChartType);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(768, 364);
			this.panel1.TabIndex = 0;
			// 
			// C_ListChartType
			// 
			this.C_ListChartType.ColumnWidth = 199;
			this.C_ListChartType.Dock = System.Windows.Forms.DockStyle.Fill;
			this.C_ListChartType.ImageList = this.C_ImageList;
			this.C_ListChartType.ItemHeight = 140;
			this.C_ListChartType.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageListBoxItem[] {
																										   new DevExpress.XtraEditors.Controls.ImageListBoxItem(null, 0),
																										   new DevExpress.XtraEditors.Controls.ImageListBoxItem(null, 1),
																										   new DevExpress.XtraEditors.Controls.ImageListBoxItem(null, 2),
																										   new DevExpress.XtraEditors.Controls.ImageListBoxItem(null, 3),
																										   new DevExpress.XtraEditors.Controls.ImageListBoxItem(null, 4),
																										   new DevExpress.XtraEditors.Controls.ImageListBoxItem(null, 5),
																										   new DevExpress.XtraEditors.Controls.ImageListBoxItem(null, 6),
																										   new DevExpress.XtraEditors.Controls.ImageListBoxItem(null, 7)});
			this.C_ListChartType.Location = new System.Drawing.Point(0, 0);
			this.C_ListChartType.MultiColumn = true;
			this.C_ListChartType.Name = "C_ListChartType";
			this.C_ListChartType.Size = new System.Drawing.Size(768, 364);
			this.C_ListChartType.TabIndex = 0;
			this.C_ListChartType.SelectedIndexChanged += new System.EventHandler(this.C_ListChartType_SelectedIndexChanged);
			// 
			// C_ImageList
			// 
			this.C_ImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
			this.C_ImageList.ImageSize = new System.Drawing.Size(195, 135);
			this.C_ImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("C_ImageList.ImageStream")));
			this.C_ImageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// ConfigChartExChartType
			// 
			this.Controls.Add(this.panel1);
			this.Name = "ConfigChartExChartType";
			this.Size = new System.Drawing.Size(768, 364);
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.C_ListChartType)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		public override void SetView(Webb.Reports.ExControls.Views.ExControlView i_View)
		{
			if(i_View is WebbChartExView)
			{
				WebbChartExView mainView = i_View as WebbChartExView;

				int index = (int)(this._DesignerForm as DF_ChartControlEx).Settings.ChartType;

				this.C_ListChartType.SelectedIndex = index;
			}
		}

		public override void UpdateView(Webb.Reports.ExControls.Views.ExControlView i_View)
		{			
			
		}

		private void C_ListChartType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.C_ListChartType.SelectedIndex >= 0)
			{
				(this._DesignerForm as DF_ChartControlEx).Settings.ChartType = (ChartAppearanceType)this.C_ListChartType.SelectedIndex;
			}
		}
	}
}

