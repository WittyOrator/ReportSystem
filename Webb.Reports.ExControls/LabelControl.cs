using System;
using System.ComponentModel;
using System.Collections;
using System.Drawing;

using DevExpress.XtraReports;

using Webb.Reports.DataProvider;
using Webb.Reports.ExControls.Data;
using Webb.Reports.ExControls.Views;

namespace Webb.Reports.ExControls
{
	/// <summary>
	/// Summary description for DateTimeControl.
	/// </summary>
	[XRDesigner("Webb.Reports.ExControls.Design.LabelControlDesigner,Webb.Reports.ExControls"),
	Designer("Webb.Reports.ExControls.Design.LabelControlDesigner,Webb.Reports.ExControls"),
	ToolboxBitmap(typeof(Webb.Reports.ExControls.ResourceManager), "Bitmaps.Label.bmp")]
	public class LabelControl : ExControl
	{
		[Browsable(false)]
		public LabelControlView LabelControlView
		{
			get{return this.MainView as LabelControlView;}
		}
    
		new public string Text
		{
			get{return this.LabelControlView.Text;}
			set
			{
				this.LabelControlView.Text = value;
			
				if(DesignMode)
				{
					this.ResetPrintingTable();
				}
			}
		}

		new public Font Font
		{
			get{return this.LabelControlView.Font;}
			set
			{
				this.LabelControlView.Font = value;
			
				if(DesignMode)
				{
					this.ResetPrintingTable();
				}
			}
		}

		public Color TextColor
		{
			get{return this.LabelControlView.TextColor;}
			set
			{
				this.LabelControlView.TextColor = value;
			
				if(DesignMode)
				{
					this.ResetPrintingTable();
				}
			}
		}

        public SortingColumnCollection SortingColumns
        {
            get { return this.LabelControlView.SortingColumns; }
            set { this.LabelControlView.SortingColumns = value; }
        }

		new public Color BackColor
		{
			get{return this.LabelControlView.BackColor;}
			set
			{
				this.LabelControlView.BackColor = value;
			
				if(DesignMode)
				{
					this.ResetPrintingTable();
				}
			}
		}
        [Editor(typeof(Editors.SidesEditor), typeof(System.Drawing.Design.UITypeEditor))]	//07-15-2008@Scott
        public DevExpress.XtraPrinting.BorderSide BorderSide
        {
            get { return this.LabelControlView.BorderSide; }
            set
            {
                this.LabelControlView.BorderSide = value;

                if (DesignMode)
                {
                    this.ResetPrintingTable();
                }
            }
        }

		public Align Align
		{
			get{return this.LabelControlView.Align;}
			set
			{
				if(this.LabelControlView.Align == value) return;

				this.LabelControlView.Align = value;
				
				if(DesignMode)
				{
					this.ResetPrintingTable();
				}
			}
		}

		public DevExpress.Utils.HorzAlignment HorzAlignment
		{
			get{return this.LabelControlView.HorzAlignment;}
			set
			{
				if(this.LabelControlView.HorzAlignment == value) return;

				this.LabelControlView.HorzAlignment = value;

				if(DesignMode)
				{
					this.ResetPrintingTable();
				}
			}
		}

		public DevExpress.Utils.VertAlignment VertAlignment
		{
			get{return this.LabelControlView.VertAlignment;}
			set
			{
				if(this.LabelControlView.VertAlignment == value) return;

				this.LabelControlView.VertAlignment = value;

				if(DesignMode)
				{
					this.ResetPrintingTable();
				}
			}
		}

        //09-01-2011 Scott
        public bool IsTitle
        {
            get { return this.LabelControlView.IsTitle; }
            set
            {
                if (this.LabelControlView.IsTitle == value) return;

                this.LabelControlView.IsTitle = value;
            }
        }

		public bool OneValuePerPage
		{
			get{return this.LabelControlView.OneValuePerPage;}
			set
			{
				if(this.LabelControlView.OneValuePerPage == value) return;

				this.LabelControlView.OneValuePerPage = value;
				
				if(DesignMode)
				{
					this.ResetPrintingTable();
				}
			}
		}

        [Editor(typeof(Webb.Reports.Editors.FieldSelectEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public string Field	//Modified at 2008-12-15 13:45:31@Scott
		{
			get{return this.LabelControlView.Field;}
			set{this.LabelControlView.Field = value;}
		}

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			//base.OnPaint(e);
			this._MainView.Paint(e);
		}

		public LabelControl()
		{
			this._MainView = new LabelControlView(this);
		}

		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged (e);

			((LabelControlView)this._MainView).SetSize(this.Width,this.Height);
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
			base.AutoAdjustSize();
		}
	}
}
