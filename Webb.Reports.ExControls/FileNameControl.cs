using System;
using System.ComponentModel;
using System.Collections;
using System.Drawing;

using DevExpress.XtraReports;

using Webb.Reports.DataProvider;
using Webb.Reports.ExControls.Views;

namespace Webb.Reports.ExControls
{
	/// <summary>
	/// Summary description for DateTimeControl.
	/// </summary>
	[XRDesigner("Webb.Reports.ExControls.Design.FileNameControlDesigner,Webb.Reports.ExControls"),
	Designer("Webb.Reports.ExControls.Design.FileNameControlDesigner,Webb.Reports.ExControls"),
	ToolboxBitmap(typeof(Webb.Reports.ExControls.ResourceManager), "Bitmaps.FileName.bmp")]
	public class FileNameControl : ExControl
	{
		[Browsable(false)]
		public FileNameControlView FileNameControlView
		{
			get{return this.MainView as FileNameControlView;}
		}
		protected override void InitLayout()
		{
			base.InitLayout ();
			if(this.MainView.PrintingTable!=null)
			{
				IWebbTableCell cell=this.MainView.PrintingTable.GetCell(0,0);
				if(cell==null)return;
				if(cell.Text!=string.Empty)
				{
					this.AutoAdjustSize();
				}
			}
		}

		new public Font Font
		{
			get{return this.FileNameControlView.Font;}
			set
			{
				this.FileNameControlView.Font = value;
			
				if(DesignMode)
				{
					this.ResetPrintingTable();
				}
			}
		}

		public Color TextColor
		{
			get{return this.FileNameControlView.TextColor;}
			set
			{
				this.FileNameControlView.TextColor = value;
			
				if(DesignMode)
				{
					this.ResetPrintingTable();
				}
			}
		}

		new public Color BackColor
		{
			get{return this.FileNameControlView.BackColor;}
			set
			{
				this.FileNameControlView.BackColor = value;
			
				if(DesignMode)
				{
					this.ResetPrintingTable();
				}
			}
		}
        [Browsable(false)]
		public Align Align
		{
			get{return this.FileNameControlView.Align;}
			set
			{
				if(this.FileNameControlView.Align == value) return;

				this.FileNameControlView.Align = value;
				
				if(DesignMode)
				{
					this.ResetPrintingTable();
				}
			}
		}

		public DevExpress.Utils.HorzAlignment HorzAlignment
		{
			get{return this.FileNameControlView.HorzAlignment;}
			set
			{
				if(this.FileNameControlView.HorzAlignment == value) return;

				this.FileNameControlView.HorzAlignment = value;

				if(DesignMode)
				{
					this.ResetPrintingTable();
				}
			}
		}

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			//base.OnPaint(e);
			this._MainView.Paint(e);
			
		}

		public FileNameControl()
		{
			this._MainView = new FileNameControlView(this);
		}

		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged (e);

			((FileNameControlView)this._MainView).SetSize(this.Width,this.Height);
		}

		public void ResetPrintingTable()
		{
			this._MainView.CreatePrintingTable();
		}

		public override void CreateArea(string areaName, DevExpress.XtraPrinting.IBrickGraphics graph)
		{
			base.CreateArea (areaName, graph);
		}

		public override void AutoAdjustSize()
		{
			base.AutoAdjustSize ();
		}
	}
}
