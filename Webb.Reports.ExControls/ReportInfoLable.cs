/***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:ReportInfoLable.cs
 * Author:Country.Wu [EMail:Webb.Country.Wu@163.com]
 * Create Time:11/29/2007 12:59:06 PM
 * Copyright:1986-2007@Webb Electronics all right reserved.
 * Purpose:
 * ***********************************************************************/
#region History
/*
 * //Author@DateTime : Description
 * */
#endregion History

using System;
using System.ComponentModel;
using System.Collections;
using System.Drawing;

using DevExpress.XtraReports;

using Webb.Reports.DataProvider;
using Webb.Reports.ExControls.Views;

namespace Webb.Reports.ExControls
{
	public enum LabelTypes
	{
		GameName = 0,
		FilterName,
		Both,
		Custom,
	}

	public enum Align
	{
		Horizontal = 0,
		Vertical,
	}

	/// <summary>
	/// Summary description for ReportInfoLable.
	/// </summary>
	[XRDesigner("Webb.Reports.ExControls.Design.ReportInfoLabelDesigner,Webb.Reports.ExControls"),
	Designer("Webb.Reports.ExControls.Design.ReportInfoLabelDesigner,Webb.Reports.ExControls"),
	ToolboxBitmap(typeof(Webb.Reports.ExControls.ResourceManager), "Bitmaps.List.bmp")]
	public class ReportInfoLabel : ExControl
	{
		[Browsable(false)]
		public ReportInfoLabelView ReportInfoView
		{
			get{return this.MainView as ReportInfoLabelView;}
		}

		new public Font Font
		{
			get{return this.ReportInfoView.Font;}
			set
			{
				this.ReportInfoView.Font = value;
			
				if(DesignMode)
				{
					this.ResetPrintingTable();
				}
			}
		}

		public Color TextColor
		{
			get{return this.ReportInfoView.TextColor;}
			set
			{
				this.ReportInfoView.TextColor = value;
			
				if(DesignMode)
				{
					this.ResetPrintingTable();
				}
			}
		}

		new public Color BackColor
		{
			get{return this.ReportInfoView.BackColor;}
			set
			{
				this.ReportInfoView.BackColor = value;
			
				if(DesignMode)
				{
					this.ResetPrintingTable();
				}
			}
		}

		public LabelTypes LabelType
		{
			get{return this.ReportInfoView.LabelType;}
			set
			{
				if(this.ReportInfoView.LabelType == value) return;

				this.ReportInfoView.LabelType = value;

				if(DesignMode)
				{
					this.ResetPrintingTable();
				}
			}
		}

		public Align Align
		{
			get{return this.ReportInfoView.Align;}
			set
			{
				if(this.ReportInfoView.Align == value) return;

				this.ReportInfoView.Align = value;
				
				if(DesignMode)
				{
					this.ResetPrintingTable();
				}
			}
		}

		public bool Multiline
		{
			get{return this.ReportInfoView.Multiline;}
			set
			{
				if(this.ReportInfoView.Multiline == value) return;

				this.ReportInfoView.Multiline = value;

				if(DesignMode)
				{
					this.ResetPrintingTable();
				}
			}
		}

		public bool WordWrap
		{
			get{return this.ReportInfoView.WordWrap;}
			set
			{
				if(this.ReportInfoView.WordWrap == value) return;

				this.ReportInfoView.WordWrap = value;

				if(DesignMode)
				{
					this.ResetPrintingTable();
				}
			}
		}

		public bool Title
		{
			get{return this.ReportInfoView.Title;}
			set
			{
				if(this.ReportInfoView.Title == value) return;

				this.ReportInfoView.Title = value;

				if(DesignMode)
				{
					this.ResetPrintingTable();
				}
			}
		}

		public bool Object
		{
			get{return this.ReportInfoView.Object;}

			set
			{
				if(this.ReportInfoView.Object == value) return;

				this.ReportInfoView.Object = value;

				if(DesignMode)
				{
					this.ResetPrintingTable();
				}
			}
		}

		public bool Opponent
		{
			get{return this.ReportInfoView.Opponent;}

			set
			{
				if(this.ReportInfoView.Opponent == value) return;

				this.ReportInfoView.Opponent = value;

				if(DesignMode)
				{
					this.ResetPrintingTable();
				}
			}
		}

		public bool Date
		{
			get{return this.ReportInfoView.Date;}
			set
			{
				if(this.ReportInfoView.Date == value) return;

				this.ReportInfoView.Date = value;

				if(DesignMode)
				{
					this.ResetPrintingTable();
				}
			}
		}

		new public bool Location
		{
			get{return this.ReportInfoView.Location;}
			set
			{
				if(this.ReportInfoView.Location == value) return;

				this.ReportInfoView.Location = value;

				if(DesignMode)
				{
					this.ResetPrintingTable();
				}
			}
		}

		public bool FirstObjectOnly
		{
			get{return this.ReportInfoView.FirstObjectOnly;}
			set
			{
				if(this.ReportInfoView.FirstObjectOnly == value) return;

				this.ReportInfoView.FirstObjectOnly = value;

				if(DesignMode)
				{
					this.ResetPrintingTable();
				}
			}
		}

		public bool ScoutType
		{
			get{return this.ReportInfoView.ScoutType;}
			set
			{
				if(this.ReportInfoView.ScoutType == value) return;

				this.ReportInfoView.ScoutType = value;

				if(DesignMode)
				{
					this.ResetPrintingTable();
				}
			}
		}
		public bool OnceScoutType  //add this property at 2008-10-22 14:00:23@Simon
		{
			get{return this.ReportInfoView.OnceScoutType;}
			set
			{
				if(this.ReportInfoView.OnceScoutType== value) return;

				this.ReportInfoView.OnceScoutType  = value;

				if(DesignMode)
				{
					this.ResetPrintingTable();
				}
			}
		}

		new public string Text
		{
			get{return this.ReportInfoView.Text;}
			set
			{
				if(this.ReportInfoView.Text == value) return;

				this.ReportInfoView.Text = value;

				if(DesignMode)
				{
					this.ResetPrintingTable();
				}
			}
		}

		public DevExpress.Utils.HorzAlignment HorzAlignment
		{
			get{return this.ReportInfoView.HorzAlignment;}
			set
			{
				if(this.ReportInfoView.HorzAlignment == value) return;

				this.ReportInfoView.HorzAlignment = value;

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

		public ReportInfoLabel()
		{
			this._MainView = new ReportInfoLabelView(this);
		}

		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged (e);

			((ReportInfoLabelView)this._MainView).SetSize(this.Width,this.Height);
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
