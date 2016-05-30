using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Configuration;
using System.Timers;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Drawing;

using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization;

using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Container;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraPrinting;
using DevExpress.Utils.Design;
using DevExpress.Utils;
using DevExpress.XtraReports;
using DevExpress.XtraReports.UI;

using Webb.Reports.ExControls.Views;

namespace Webb.Reports.ExControls
{
	/// <summary>
	/// Summary description for StatControl.
	/// </summary>
	[XRDesigner("Webb.Reports.ExControls.Design.ChartControlDesigner,Webb.Reports.ExControls"),
	Designer("Webb.Reports.ExControls.Design.ChartControlDesigner,Webb.Reports.ExControls"),
	ToolboxBitmap(typeof(Webb.Reports.ExControls.ResourceManager), "Bitmaps.Chart.bmp")]
	public class ChartControl : ExControl
	{
		public ChartControl()
		{
			this.MainView = new WebbChartView(this);
		}

		private WebbChartView WebbChartView
		{
			get{return this._MainView as WebbChartView;}
		}

		[EditorAttribute(typeof(Webb.Reports.Editors.DBFilterEditor), typeof(System.Drawing.Design.UITypeEditor))]	//06-18-2008@Scott
		public Webb.Data.DBFilter Filter
		{
			get{return this.WebbChartView.Filter;}
			set{this.WebbChartView.Filter = value;}
		}

		protected override void OnPaint(PaintEventArgs e)
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

		public override void AutoAdjustSize()
		{
			//base.AutoAdjustSize ();
			this.XtraContainer.Width = 500;
			this.XtraContainer.Height = 400;
		}
	}
}
