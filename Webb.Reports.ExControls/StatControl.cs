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
	[XRDesigner("Webb.Reports.ExControls.Design.StatControlDesigner,Webb.Reports.ExControls"),
	Designer("Webb.Reports.ExControls.Design.StatControlDesigner,Webb.Reports.ExControls"),
	ToolboxBitmap(typeof(Webb.Reports.ExControls.ResourceManager), "Bitmaps.Stat.bmp")]
	public class StatControl : ExControl
	{
		public StatControl()
		{
			this.MainView = new StatControlView(this);
		}

		private StatControlView StatControlView
		{
			get{return this._MainView as StatControlView;}
		}

		public bool Multiline
		{
			get{return this.StatControlView.Multiline;}

			set
			{
				if(this.StatControlView.Multiline == value) return;
				
				this.StatControlView.Multiline = value;
				
				if(DesignMode)
				{
					this.MainView.UpdateView();
				}
			}
		}
		public bool AdjustWidth
		{
			get{return this.StatControlView.AdjustWidth;}

			set
			{
				if(this.StatControlView.AdjustWidth == value) return;
				
				this.StatControlView.AdjustWidth = value;
				
				if(DesignMode)
				{
					this.MainView.UpdateView();
				}
			}
		}


        [Browsable(false)]
		public bool Overlap
		{
			get{return this.StatControlView.Overlap;}

			set
			{
				if(this.StatControlView.Overlap == value) return;
				
				this.StatControlView.Overlap = value;
				
				if(DesignMode)
				{
					this.MainView.UpdateView();
				}
			}
		}

		[EditorAttribute(typeof(Webb.Reports.Editors.DBFilterEditor), typeof(System.Drawing.Design.UITypeEditor))]	//06-18-2008@Scott
		public Webb.Data.DBFilter Filter
		{
			get{return this.StatControlView.Filter;}
			set{this.StatControlView.Filter = value;}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			this.MainView.Paint(e);
		}    
	}
}