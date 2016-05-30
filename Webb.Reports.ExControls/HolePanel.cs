using System;
using System.ComponentModel;
using System.Collections;
using System.Drawing;

using DevExpress.XtraReports;

using Webb.Reports.DataProvider;
using Webb.Reports.ExControls.Views;

namespace Webb.Reports.ExControls
{
	[XRDesigner("Webb.Reports.ExControls.Design.HolePanelDesigner,Webb.Reports.ExControls"),
	Designer("Webb.Reports.ExControls.Design.HolePanelDesigner,Webb.Reports.ExControls"),
	ToolboxBitmap(typeof(Webb.Reports.ExControls.ResourceManager), "Bitmaps.Group.bmp")]
	public class HolePanel : ExControl
	{
		public Webb.Data.DBFilter Filter
		{
			get{return this.HolePanelView.Filter;}
		}

		public HolePanel()
		{
			this._MainView = new HolePanelView(this);
		}

		public HolePanelView HolePanelView
		{
			get{return this._MainView as HolePanelView;}
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
