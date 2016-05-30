using System;
using System.ComponentModel;
using System.Collections;
using System.Drawing;

using DevExpress.XtraReports;

using Webb.Reports.DataProvider;
using Webb.Reports.ExControls.Views;
using Webb.Reports.Editors;

namespace Webb.Reports.ExControls
{
	/// <summary>
	/// Summary description for DateTimeControl.
	/// </summary>
	[XRDesigner("Webb.Reports.ExControls.Design.DateTimeControlDesigner,Webb.Reports.ExControls"),
	Designer("Webb.Reports.ExControls.Design.DateTimeControlDesigner,Webb.Reports.ExControls"),
	ToolboxBitmap(typeof(Webb.Reports.ExControls.ResourceManager), "Bitmaps.weekDaysEdit.bmp")]
	public class DateTimeControl : ExControl
	{
		public DateTimeControlView DateTimeControlView
		{
			get{return this.MainView as DateTimeControlView;}
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
			get{return this.DateTimeControlView.Font;}
			set
			{
				this.DateTimeControlView.Font = value;
			
				if(DesignMode)
				{
					this.ResetPrintingTable();
				}
			}
		}

		public Color TextColor
		{
			get{return this.DateTimeControlView.TextColor;}
			set
			{
				this.DateTimeControlView.TextColor = value;
			
				if(DesignMode)
				{
					this.ResetPrintingTable();
				}
			}
		}

		new public Color BackColor
		{
			get{return this.DateTimeControlView.BackColor;}
			set
			{
				this.DateTimeControlView.BackColor = value;
			
				if(DesignMode)
				{
					this.ResetPrintingTable();
				}
			}
		}

        public string DataFormat
        {
            get { return this.DateTimeControlView.DataFormat; }
            set
            {
                this.DateTimeControlView.DataFormat = value;

                if (DesignMode)
                {
                    this.ResetPrintingTable();
                }
            }
        }

		public Align Align
		{
			get{return this.DateTimeControlView.Align;}
			set
			{
				if(this.DateTimeControlView.Align == value) return;

				this.DateTimeControlView.Align = value;
				
				if(DesignMode)
				{
					this.ResetPrintingTable();
				}
			}
		}

		public DevExpress.Utils.HorzAlignment HorzAlignment
		{
			get{return this.DateTimeControlView.HorzAlignment;}
			set
			{
				if(this.DateTimeControlView.HorzAlignment == value) return;

				this.DateTimeControlView.HorzAlignment = value;

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

		public DateTimeControl()
		{
			this._MainView = new DateTimeControlView(this);
		}

		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged (e);

			((DateTimeControlView)this._MainView).SetSize(this.Width,this.Height);
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