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
	#region public class GroupingControl
	/*Descrition:   */
	[XRDesigner("Webb.Reports.ExControls.Design.GroupDesigner,Webb.Reports.ExControls"),
	Designer("Webb.Reports.ExControls.Design.GroupDesigner,Webb.Reports.ExControls")]
	public class GroupingControl : ExControl//, IPrintable,IExtendedControl
	{
		//Wu.Country@2007-10-31 02:40 PM added this class.
		//Fields
		public GroupView _GroupView
		{
			get{return base._MainView as GroupView;}
		}


        ////Properties
        public Data.GroupSummaryCollection SummariesForSections
        {
            get { return _GroupView.SummariesForSections; }
            set
            {
                this._GroupView.SummariesForSections = value;

                if (DesignMode) this._GroupView.UpdateView();
            }
        }

        [EditorAttribute(typeof(Webb.Reports.ExControls.Editors.PropertyEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public GroupAdvancedSetting GroupAdvancedSetting
        {
            get { return _GroupView.GroupAdvancedSetting; }
            set
            {
                this._GroupView.GroupAdvancedSetting = value;

                if (DesignMode) this._GroupView.UpdateView();
            }
        }
        [EditorAttribute(typeof(Webb.Reports.ExControls.Editors.PropertyEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public OurBordersSetting OurBordersSetting
        {
            get { return _GroupView.OurBordersSetting; }
            set
            {
                this._GroupView.OurBordersSetting = value;

                if (DesignMode) this._GroupView.UpdateView();
            }
        }   
        
       
		//Properties
		public bool ShowRowIndicators
		{
			get{return _GroupView.ShowRowIndicators;}
			set
			{
				this._GroupView.ShowRowIndicators = value;

				if(DesignMode) this._GroupView.UpdateView();
			}
		}
		public bool VerticalGroupSides
		{
			get{return _GroupView.VerticalGroupSides;}
			set
			{
				this._GroupView.VerticalGroupSides = value;

				if(DesignMode) this._GroupView.UpdateView();
			}
		}

    
		[Browsable(false)]
		public int GroupSidesColumn
		{
			get{return _GroupView.GroupSidesColumn;}
			set
			{
				this._GroupView.GroupSidesColumn = value;

				if(DesignMode) this._GroupView.UpdateView();
			}
		}


		public bool HaveHeader   //Modified at 2008-10-21 9:01:27@Simon
		{
			get{return _GroupView.HaveHeader;}
			set
			{
				this._GroupView.HaveHeader = value;

				if(DesignMode) this._GroupView.UpdateView();
			}
		}                       //end Modified at 2008-10-21 9:01:36@Simon

		public CellSizeAutoAdaptingTypes CellSizeAutoAdapting
		{
			get{return this._GroupView.CellSizeAutoAdapting;}
			set
			{
				this._GroupView.CellSizeAutoAdapting = value;

				if(DesignMode) this._GroupView.UpdateView();
			}
		}
        [Browsable(false)]
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


     
		[Browsable(false)]
		public bool OneValuePerPage
		{
			get{return this._GroupView.OneValuePerPage;}
			set{this._GroupView.OneValuePerPage = value;}
		}

		[EditorAttribute(typeof(Webb.Reports.Editors.DBFilterEditor), typeof(System.Drawing.Design.UITypeEditor))]	//06-18-2008@Scott
		public Webb.Data.DBFilter Filter
		{
			get{return this._GroupView.Filter;}
			set{this._GroupView.Filter = value;}
		}

        public void UpdateView()
        {
            if (DesignMode) this._GroupView.UpdateView();
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

                this._GroupView.GroupAdvancedSetting.Total = value;
			
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

                this._GroupView.GroupAdvancedSetting.TotalTitle = value;

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
        [Browsable(false)]
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
		[Browsable(false)]
		public bool PlayAfter
		{
			get{return this._GroupView.Filter.PlayAfter;}
			set{this._GroupView.Filter.PlayAfter = value;}
        }

        #region GroupAdvanced Setting Before
        [EditorAttribute(typeof(Webb.Reports.ExControls.Editors.SidesEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public DevExpress.XtraPrinting.BorderSide TotalSides
        {
            get { return this._GroupView.TotalSides; }
            set
            {
                if (this._GroupView.TotalSides == value) return;

                this._GroupView.TotalSides = value;

                this._GroupView.GroupAdvancedSetting.TotalSides = value;

                if (DesignMode) this._GroupView.UpdateView();
            }
        }
		public bool SectionSides
		{
			get{return _GroupView.SectionSides;}
			set{
				if(this._GroupView.SectionSides == value) return;
				_GroupView.SectionSides=value;
                _GroupView.GroupAdvancedSetting.SetSectionSides = value;
				if(DesignMode) this._GroupView.UpdateView();
			   }
		}
		public byte ChartRowHeight
		{
			get{return _GroupView.ChartRowHeight;}
			set
			{
				if(this._GroupView.ChartRowHeight == value) return;
				_GroupView.ChartRowHeight=value;
                _GroupView.GroupAdvancedSetting.ChartRowHeight = value;
				if(DesignMode) this._GroupView.UpdateView();
			}
        }
        #endregion

       
		//ctor
		public GroupingControl()
		{
			this._MainView = new GroupView(this);
		}	

		protected override void OnPaint(PaintEventArgs e)
		{
			this.MainView.Paint(e);
		}

       
		
	}
	#endregion	

 
}
