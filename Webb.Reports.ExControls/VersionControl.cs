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
	[XRDesigner("Webb.Reports.ExControls.Design.VersionControlDesigner,Webb.Reports.ExControls"),
	Designer("Webb.Reports.ExControls.Design.VersionControlDesigner,Webb.Reports.ExControls"),
	ToolboxBitmap(typeof(Webb.Reports.ExControls.ResourceManager), "Bitmaps.Version.bmp")]
	public class VersionControl : ExControl
	{
		[Browsable(false)]
		public VersionControlView VersionControlView
		{
			get{return this.MainView as VersionControlView;}
		}
		protected override void InitLayout()
		{
			base.InitLayout ();
			if(this.VersionControlView.PrintingTable!=null)
			{   IWebbTableCell cell=this.VersionControlView.PrintingTable.GetCell(0,0);
				if(cell==null)return;
				if(cell.Text!=string.Empty)
				{
					this.AutoAdjustSize();
				}
			}
		}

		new public Font Font
		{
			get{return this.VersionControlView.Font;}
			set
			{
				this.VersionControlView.Font = value;
			
				if(DesignMode)
				{
					this.ResetPrintingTable();
				}
			}
		}

		public string FormattedText
		{
			get{return this.VersionControlView.FormattedText;}

			set
			{
					this.VersionControlView.FormattedText = value;			
					if(DesignMode)
					{
						this.ResetPrintingTable();
						this.AutoAdjustSize();
					}
			}
		}

		public Color TextColor
		{
			get{return this.VersionControlView.TextColor;}
			set
			{
				this.VersionControlView.TextColor = value;
			
				if(DesignMode)
				{
					this.ResetPrintingTable();
				}
			}
		}

		new public Color BackColor
		{
			get{return this.VersionControlView.BackColor;}
			set
			{
				this.VersionControlView.BackColor = value;
			
				if(DesignMode)
				{
					this.ResetPrintingTable();
				}
			}
		}

		public Align Align
		{
			get{return this.VersionControlView.Align;}
			set
			{
				if(this.VersionControlView.Align == value) return;

				this.VersionControlView.Align = value;
				
				if(DesignMode)
				{
					this.ResetPrintingTable();
				}
			}
		}

		public DevExpress.Utils.HorzAlignment HorzAlignment
		{
			get{return this.VersionControlView.HorzAlignment;}
			set
			{
				if(this.VersionControlView.HorzAlignment == value) return;

				this.VersionControlView.HorzAlignment = value;

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

		public VersionControl()
		{
			this._MainView = new VersionControlView(this);
		}

		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged (e);

			((VersionControlView)this._MainView).SetSize(this.Width,this.Height);
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
