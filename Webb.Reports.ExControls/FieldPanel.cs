using System;
using System.ComponentModel;
using System.Collections;
using System.Drawing;

using DevExpress.XtraReports;

using Webb.Reports.DataProvider;
using Webb.Reports.ExControls.Views;

namespace Webb.Reports.ExControls
{
	[XRDesigner("Webb.Reports.ExControls.Design.FieldPanelDesigner,Webb.Reports.ExControls"),
	Designer("Webb.Reports.ExControls.Design.FieldPanelDesigner,Webb.Reports.ExControls"),
	ToolboxBitmap(typeof(Webb.Reports.ExControls.ResourceManager), "Bitmaps.HitChart.bmp")]
	public class FieldPanel : ExControl
	{
		[EditorAttribute(typeof(Webb.Reports.Editors.DBFilterEditor), typeof(System.Drawing.Design.UITypeEditor))]	//06-18-2008@Scott
		public Webb.Data.DBFilter Filter
		{
			get{return this.FieldPanelView.Filter;}
			set{this.FieldPanelView.Filter = value;}
		}

		public bool GridLine
		{
			get{return this.FieldPanelView.GridLine;}
			set
			{
				if(this.FieldPanelView.GridLine != value)
				{
					this.FieldPanelView.GridLine = value;
				
					if(DesignMode)
					{
						this.FieldPanelView.UpdateView();
					}
				}
			}
		}
		public bool RemoveHeaderRows
		{
			get{return this.FieldPanelView.RemoveHeaderRows;}
			set
			{
				if(this.FieldPanelView.RemoveHeaderRows != value)
				{
					this.FieldPanelView.RemoveHeaderRows = value;
				
					if(DesignMode)
					{
						this.FieldPanelView.UpdateView();
					}
				}
			}
		}
		public CellSizeAutoAdaptingTypes CellSizeAutoAdapting
		{
			get{return this.FieldPanelView.CellSizeAutoAdapting;}
			set
			{
				if(this.FieldPanelView.CellSizeAutoAdapting != value)
				{
					this.FieldPanelView.CellSizeAutoAdapting = value;
				
					if(DesignMode)
					{
						this.FieldPanelView.UpdateView();
					}
				}
			}

		}

		public FieldPanel()
		{
			this._MainView = new FieldPanelView(this);
		}

		public FieldPanelView FieldPanelView
		{
			get{return this._MainView as FieldPanelView;}
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
