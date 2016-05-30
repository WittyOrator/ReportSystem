 /***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:MaskedTextControl.cs
 * Author:Automatic Macro@simon
 * Create Time:2009-12-2 11:09:36
 * Copyright:1986-2009@Webb Electronics all right reserved.
 * Purpose:
 * ***********************************************************************/
using System;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Windows.Forms;
using System.IO;
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
using Webb.Reports.ExControls.Data;
using Webb.Reports.ExControls.Views;


namespace Webb.Reports.ExControls
{
	[XRDesigner("Webb.Reports.ExControls.Design.MaskedTextControlDesigner,Webb.Reports.ExControls"),
	Designer("Webb.Reports.ExControls.Design.MaskedTextControlDesigner,Webb.Reports.ExControls"),
	 ToolboxBitmap(typeof(Webb.Reports.ExControls.ResourceManager), "Bitmaps.SignedLabel.bmp")]
	public class MaskedTextControl : ExControl
	{
		public MaskedTextControl()
		{
			this._MainView = new MaskedTextControlView(this);
		}

		public Views.MaskedTextControlView MaskedTextControlView
		{
			get{ return this._MainView as MaskedTextControlView;}
			
		}

		override protected void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			this._MainView.Paint(e);
		
		}

		public Webb.Reports.ExControls.Data.MaskInfoCollection MaskInfoSetting
		{
			get{ return this.MaskedTextControlView.MaskInfoSetting; }
			set
			{
			     this.MaskedTextControlView.MaskInfoSetting=value;

			     if(DesignMode) this.MaskedTextControlView.UpdateView();
			}
		}
		public bool AutoAdjustTitleSize
		{
			get{ return this.MaskedTextControlView.AutoAdjustTitleSize; }
			set
			{
				this.MaskedTextControlView.AutoAdjustTitleSize=value;

				if(DesignMode) this.MaskedTextControlView.UpdateView();
			}
		}

		public Webb.Data.DBFilter Filter
		{
			get{ return this.MaskedTextControlView.Filter; }
			set
			{
				this.MaskedTextControlView.Filter=value;

				if(DesignMode) this.MaskedTextControlView.UpdateView();
			}
		}

		public int IndexRow
		{
			get{ return this.MaskedTextControlView.IndexRow; }
			set
			{
				this.MaskedTextControlView.IndexRow=value;

				if(DesignMode) this.MaskedTextControlView.UpdateView();
			}

		}
		public SortingColumnCollection SortingColumns
		{
			
			get{ return this.MaskedTextControlView.SortingColumns; }
			set
			{
				this.MaskedTextControlView.SortingColumns=value;

//				if(DesignMode) this.MaskedTextControlView.UpdateView();
			}

		}
     

	}
}
