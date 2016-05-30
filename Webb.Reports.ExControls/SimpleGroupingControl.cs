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
	#region public class SimpleGroupingControl
	/*Descrition:   */
	[XRDesigner("Webb.Reports.ExControls.Design.SimpleGroupDesigner,Webb.Reports.ExControls"),
	Designer("Webb.Reports.ExControls.Design.SimpleGroupDesigner,Webb.Reports.ExControls")]
	public class SimpleGroupingControl : ExControl//, IPrintable,IExtendedControl
	{
		//Wu.Country@2007-10-31 02:40 PM added this class.
		//Fields
		private SimpleGroupView _GroupView
		{
			get{return base._MainView as SimpleGroupView;}
		}

		//Properties
		public bool AcrossPage
		{
			get{return this._GroupView.AcrossPage;}
			set
			{
				this._GroupView.AcrossPage = value;

				if(DesignMode) this._GroupView.UpdateView();
			}
		}

		public bool ShowRowIndicators
		{
			get{return _GroupView.ShowRowIndicators;}
			set
			{
				this._GroupView.ShowRowIndicators = value;

				if(DesignMode) this._GroupView.UpdateView();
			}
		}

		public CellSizeAutoAdaptingTypes CellSizeAutoAdapting
		{
			get{return this._GroupView.CellSizeAutoAdapting;}
			set
			{
				this._GroupView.CellSizeAutoAdapting = value;

				if(DesignMode) this._GroupView.UpdateView();
			}
		}

		public bool SizeSelfAdapting
		{
			get{return this._GroupView.SizeSelfAdapting;}
			set
			{
				if(this._GroupView.SizeSelfAdapting == value) return;

				this._GroupView.SizeSelfAdapting = value;

				if(DesignMode) this._GroupView.UpdateView();
			}
		}

		public bool OneValuePerPage
		{
			get{return this._GroupView.OneValuePerPage;}
			set
			{
				if(this._GroupView.OneValuePerPage == value) return;

				this._GroupView.OneValuePerPage = value;

				if(DesignMode) this._GroupView.UpdateView();
			}
		}

		[EditorAttribute(typeof(Webb.Reports.Editors.DBFilterEditor), typeof(System.Drawing.Design.UITypeEditor))]	//06-18-2008@Scott
		public Webb.Data.DBFilter Filter
		{
			get{return this._GroupView.Filter;}
			set{this._GroupView.Filter = value;}
		}

		public int TopCount
		{
			get{return this._GroupView.TopCount;}
			set
			{
				if(this._GroupView.TopCount == value) return;

				this._GroupView.TopCount = value;
			
				if(DesignMode) this._GroupView.UpdateView();
			}
		}

		public bool Total
		{
			get{return this._GroupView.Total;}
			set
			{
				if(this._GroupView.Total == value) return;

				this._GroupView.Total = value;
			
				if(DesignMode) this._GroupView.UpdateView();
			}
		}

		public bool SectionInOneRow
		{
			get{return this._GroupView.SectionInOneRow;}
			set
			{
				if(this._GroupView.SectionInOneRow == value) return;

				this._GroupView.SectionInOneRow = value;
			
				if(DesignMode) this._GroupView.UpdateView();
			}
		}

		public string SectionTitle
		{
			get{return this._GroupView.SectionTitle;}
			set
			{
				if(this._GroupView.SectionTitle == value) return;

				this._GroupView.SectionTitle = value;
			
				if(DesignMode) this._GroupView.UpdateView();
			}
		}

		public string TotalTitle
		{
			get{return this._GroupView.TotalTitle;}
			set
			{
				if(this._GroupView.TotalTitle == value) return;

				this._GroupView.TotalTitle = value;
			
				if(DesignMode) this._GroupView.UpdateView();
			}
		}

		public Webb.Collections.Int32Collection TotalColumns
		{
			get{return this._GroupView.TotalColumns;}
			set
			{
				if(this._GroupView.TotalColumns == value) return;

				this._GroupView.TotalColumns = value;
			
				if(DesignMode) this._GroupView.UpdateView();
			}
		}

		public int HeightPerPage
		{
			get{return this._GroupView.HeightPerPage;}
			set
			{
				if(this._GroupView.HeightPerPage == value) return;

				this._GroupView.HeightPerPage = value;

				if(DesignMode) this._GroupView.UpdateView();
			}
		}

		//06-12-2008@Scott
		public bool PlayAfter
		{
			get{return this._GroupView.Filter.PlayAfter;}
			set{this._GroupView.Filter.PlayAfter = value;}
		}

		//		[Browsable(false)]
		//		public GroupView MainView
		//		{
		//			get{ return this._Views[0];}
		//		}

		//ctor
		public SimpleGroupingControl()
		{
			this._MainView = new SimpleGroupView(this);
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
