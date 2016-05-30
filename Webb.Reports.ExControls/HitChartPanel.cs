using System;
using System.ComponentModel;
using System.Collections;
using System.Drawing;

using DevExpress.XtraReports;

using Webb.Reports.DataProvider;
using Webb.Reports.ExControls.Views;

namespace Webb.Reports.ExControls
{
	[XRDesigner("Webb.Reports.ExControls.Design.HitChartPanelDesigner,Webb.Reports.ExControls"),
	Designer("Webb.Reports.ExControls.Design.HitChartPanelDesigner,Webb.Reports.ExControls"),
	ToolboxBitmap(typeof(Webb.Reports.ExControls.ResourceManager), "Bitmaps.HitChart.bmp")]
	public class HitChartPanel : ExControl
	{
		[EditorAttribute(typeof(Webb.Reports.Editors.DBFilterEditor), typeof(System.Drawing.Design.UITypeEditor))]	//06-18-2008@Scott
		public Webb.Data.DBFilter Filter
		{
			get{return this.HitChartPanelView.Filter;}
			set{this.HitChartPanelView.Filter = value;}
		}

		public bool GridLine
		{
			get{return this.HitChartPanelView.GridLine;}
			set
			{
				if(this.HitChartPanelView.GridLine != value)
				{
					this.HitChartPanelView.GridLine = value;
				
					if(DesignMode)
					{
						this.HitChartPanelView.UpdateView();
					}
				}
			}
		}

		public HitChartPanel()
		{
			this._MainView = new HitChartPanelView(this);
		}

		public HitChartPanelView HitChartPanelView
		{
			get{return this._MainView as HitChartPanelView;}
		}

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			this.MainView.Paint(e);
		}

		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged (e);
			if(DesignMode)
			{
				this._MainView.UpdateView();
			}
		}
	}
}
