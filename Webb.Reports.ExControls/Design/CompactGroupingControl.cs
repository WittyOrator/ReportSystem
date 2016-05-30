/***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:ExControls.cs
 * Author:Country.Wu [EMail:Webb.Country.Wu@163.com]
 * Create Time:10/31/2007 02:41:03 PM
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
	#region public class CompactGroupingControl
	/*Descrition:   */
	[XRDesigner("Webb.Reports.ExControls.Design.CompactGroupDesigner,Webb.Reports.ExControls"),
	Designer("Webb.Reports.ExControls.Design.CompactGroupDesigner,Webb.Reports.ExControls")]
	public class CompactGroupingControl : ExControl//, IPrintable,IExtendedControl
	{
		//Fields
		private CompactGroupView _CompactGroupView
		{
			get{return base._MainView as CompactGroupView;}
		}

		//Properties
		public bool ShowRowIndicators
		{
			get{return _CompactGroupView.ShowRowIndicators;}
			set
			{
				this._CompactGroupView.ShowRowIndicators = value;

				if(DesignMode) this._CompactGroupView.UpdateView();
			}
		}

		public bool HaveHeader   //Modified at 2008-10-21 9:01:27@Simon
		{
			get{return _CompactGroupView.HaveHeader;}
			set
			{
				this._CompactGroupView.HaveHeader = value;

				if(DesignMode) this._CompactGroupView.UpdateView();
			}
		}                       //end Modified at 2008-10-21 9:01:36@Simon

		public CellSizeAutoAdaptingTypes CellSizeAutoAdapting
		{
			get{return this._CompactGroupView.CellSizeAutoAdapting;}
			set
			{
				this._CompactGroupView.CellSizeAutoAdapting = value;

				if(DesignMode) this._CompactGroupView.UpdateView();
			}
		}

		public bool SizeSelfAdapting
		{
			get{return this._CompactGroupView.SizeSelfAdapting;}
			set
			{
				if(this._CompactGroupView.SizeSelfAdapting == value) return;

				this._CompactGroupView.SizeSelfAdapting = value;

				if(DesignMode) this._CompactGroupView.UpdateView();
			}
		}

		[Browsable(false)]
		public bool OneValuePerPage
		{
			get{return this._CompactGroupView.OneValuePerPage;}
			set{this._CompactGroupView.OneValuePerPage = value;}
		}

		[EditorAttribute(typeof(Webb.Reports.Editors.DBFilterEditor), typeof(System.Drawing.Design.UITypeEditor))]	//06-18-2008@Scott
		public Webb.Data.DBFilter Filter
		{
			get{return this._CompactGroupView.Filter;}
			set{this._CompactGroupView.Filter = value;}
		}

		public int TopCount
		{
			get{return this._CompactGroupView.TopCount;}
			set
			{
				if(this._CompactGroupView.TopCount == value) return;

				this._CompactGroupView.TopCount = value;

				if(DesignMode) this._CompactGroupView.UpdateView();
			}
		}

		public bool Total
		{
			get{return this._CompactGroupView.Total;}
			set
			{
				if(this._CompactGroupView.Total == value) return;

				this._CompactGroupView.Total = value;
			
				if(DesignMode) this._CompactGroupView.UpdateView();
			}
		}

		public bool SectionInOneRow
		{
			get{return this._CompactGroupView.SectionInOneRow;}
			set
			{
				if(this._CompactGroupView.SectionInOneRow == value) return;

				this._CompactGroupView.SectionInOneRow = value;
			
				if(DesignMode) this._CompactGroupView.UpdateView();
			}
		}

		public string SectionTitle
		{
			get{return this._CompactGroupView.SectionTitle;}
			set
			{
				if(this._CompactGroupView.SectionTitle == value) return;

				this._CompactGroupView.SectionTitle = value;
			
				if(DesignMode) this._CompactGroupView.UpdateView();
			}
		}

		public string TotalTitle
		{
			get{return this._CompactGroupView.TotalTitle;}
			set
			{
				if(this._CompactGroupView.TotalTitle == value) return;

				this._CompactGroupView.TotalTitle = value;

				if(DesignMode) this._CompactGroupView.UpdateView();
			}
		}

		public Webb.Collections.Int32Collection TotalColumns
		{
			get{return this._CompactGroupView.TotalColumns;}
			set
			{
				if(this._CompactGroupView.TotalColumns == value) return;

				this._CompactGroupView.TotalColumns = value;
			
				if(DesignMode) this._CompactGroupView.UpdateView();
			}
		}

		public int HeightPerPage
		{
			get{return this._CompactGroupView.HeightPerPage;}
			set
			{
				if(this._CompactGroupView.HeightPerPage == value) return;

				this._CompactGroupView.HeightPerPage = value;

				if(DesignMode) this._CompactGroupView.UpdateView();
			}
		}

		//06-12-2008@Scott
		public bool PlayAfter
		{
			get{return this._CompactGroupView.Filter.PlayAfter;}
			set{this._CompactGroupView.Filter.PlayAfter = value;}
		}

//		[Browsable(false)]
//		public CompactGroupView MainView
//		{
//			get{ return this._Views[0];}
//		}

		//ctor
		public CompactGroupingControl()
		{
			this._MainView = new CompactGroupView(this);
		}
		//Methods
		/// <summary>
		/// Re-calculate the group result.
		/// </summary>
		public void UpdateGroup()
		{
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			this.MainView.Paint(e);
		}
	}
	#endregion	
}
