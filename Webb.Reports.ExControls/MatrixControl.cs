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
	#region public class MatrixControl
	/*Descrition:   */
	[XRDesigner("Webb.Reports.ExControls.Design.MatrixGroupControlDesigner,Webb.Reports.ExControls"),
	Designer("Webb.Reports.ExControls.Design.MatrixGroupControlDesigner,Webb.Reports.ExControls"),
	ToolboxBitmap(typeof(Webb.Reports.ExControls.ResourceManager), "Bitmaps.MatrixControl.bmp")]
	public class MatrixGroupControl : ExControl//, IPrintable,IExtendedControl
	{
		//Fields
		private MatrixGroupView _MatrixGroupView
		{
			get{return base._MainView as MatrixGroupView;}
		}

		//Properties
		public bool ShowRowIndicators
		{
			get{return _MatrixGroupView.ShowRowIndicators;}
			set
			{
				this._MatrixGroupView.ShowRowIndicators = value;

				if(DesignMode) this._MatrixGroupView.UpdateView();
			}
		}
	
        [Browsable(false)]
		public bool WidthEnable    //Added this code at 2009-2-6 11:48:45@Simon
		{
			get{return _MatrixGroupView.WidthEnable;}
			set
			{
				this._MatrixGroupView.WidthEnable = value;

				if(DesignMode) this._MatrixGroupView.UpdateView();
			}
		}
		
		public bool HaveHeader   //Modified at 2008-10-21 9:01:27@Simon
		{
			get{return _MatrixGroupView.HaveHeader;}
			set
			{
				this._MatrixGroupView.HaveHeader = value;

				if(DesignMode) this._MatrixGroupView.UpdateView();
			}
		}                       //end Modified at 2008-10-21 9:01:36@Simon


		public CellSizeAutoAdaptingTypes CellSizeAutoAdapting
		{
			get{return this._MatrixGroupView.CellSizeAutoAdapting;}
			set
			{
				this._MatrixGroupView.CellSizeAutoAdapting = value;

				if(DesignMode) this._MatrixGroupView.UpdateView();
			}
		}	


		[EditorAttribute(typeof(Webb.Reports.Editors.DBFilterEditor), typeof(System.Drawing.Design.UITypeEditor))]	//06-18-2008@Scott
		public Webb.Data.DBFilter Filter
		{
			get{return this._MatrixGroupView.Filter;}
			set{this._MatrixGroupView.Filter = value;}
		}		
	
		public ComputedStyle MatrixDisplay
		{
			get{return this._MatrixGroupView.MatrixDisplay;}
			set
			{
				
				this._MatrixGroupView.MatrixDisplay = value;		
			
				if(DesignMode) this._MatrixGroupView.UpdateView();
			}
		}	

	    [Browsable(false)]
		public int HeightPerPage
		{
			get{return this._MatrixGroupView.HeightPerPage;}
			set
			{
				if(this._MatrixGroupView.HeightPerPage == value) return;

				this._MatrixGroupView.HeightPerPage = value;

				if(DesignMode) this._MatrixGroupView.UpdateView();
			}
		}

		
		public MatrixGroupControl()
		{
			this._MainView = new MatrixGroupView(this);
		}		

		protected override void OnPaint(PaintEventArgs e)
		{
			this.MainView.Paint(e);
		}
	}
	#endregion	
}

