/***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:Designer.cs
 * Author:Country.Wu [EMail:Webb.Country.Wu@163.com]
 * Create Time:10/31/2007 03:15:09 PM
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
using DevExpress.Data;
using DevExpress.LookAndFeel;
using DevExpress.LookAndFeel.Helpers;
using DevExpress.Utils.Design;
using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.Utils.Win;
using DevExpress.Utils.Controls;
using DevExpress.Utils.Drawing;
using DevExpress.Utils.Drawing.Helpers;
using DevExpress.XtraTab;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Container;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraPrinting;
using DevExpress.Accessibility;
using DevExpress.XtraEditors.Filtering;
using System.Runtime.Serialization;

using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Windows.Forms.Design;
using System.ComponentModel.Design.Serialization;
using System.Windows.Forms.ComponentModel;
using System.Reflection;
using DevExpress.XtraEditors.Design;
//
using DevExpress.XtraReports.Design;
using DevExpress.XtraReports.Localization;
using DevExpress.XtraReports.UI;

using Webb.Reports.ExControls.Views;

using CurrentDesign = DevExpress.Utils.Design;  //08-13-2008@Scott

namespace Webb.Reports.ExControls.Design
{
	#region public class CompactGroupDesigner
	/*Descrition:   */
	public class CompactGroupDesigner : ExControlDesigner
	{
		//Wu.Country@2007-10-31 03:14 PM added this class.
		//Fields
		//Properties
		//ctor
		public CompactGroupDesigner()
		{
		}
		//Methods
		public override void Initialize(IComponent component)
		{
			base.Initialize (component);
			this._DesignForm = new UI.DF_CompactGroupingControl();
			this._DesignForm.SetView(this._Control.MainView);
		}
		//
		public override void RunDesigner(object sender, EventArgs e)
		{
			//base.RunDesigner (sender, e);
			this._DesignForm.SetView(this._Control.MainView);
			DialogResult m_Result = this._DesignForm.ShowDialog();
			if(m_Result==DialogResult.OK)
			{
				this._DesignForm.UpdateView(this._Control.MainView);
				this._Control.CalculateResult();
			}
		}

		public override void InitializeVerbs()
		{
			base.InitializeVerbs ();
			this._Verbs	.Add(new DesignerVerb("Update View",new EventHandler(UpdateView)));
		}
		
		private void UpdateView(object sender,EventArgs e)
		{
			this._Control.MainView.UpdateView();
		}

		public override void InitializeActionList()
		{
			this._ActionLists.Add(new CompactGroupingActionList(this));
		}

		#region internal class CompactGroupingActionList : ExControlActionList
		/*Descrition:   */
		internal class CompactGroupingActionList : ExControlActionList
		{
			//Wu.Country@2007-11-15 12:53 PM added this class.
			//Fields
			//Properties
			[EditorAttribute(typeof(Webb.Reports.Editors.DBFilterEditor), typeof(System.Drawing.Design.UITypeEditor))]	//06-17-2008@Scott
			public Webb.Data.DBFilter Filter
			{
				get { return ((CompactGroupingControl)Component).Filter; }
				set { SetPropertyValue("Filter", value); }
			}
			public bool ShowRowIndicators
			{
				get{return ((CompactGroupingControl)Component).ShowRowIndicators;}
				set{((CompactGroupingControl)Component).ShowRowIndicators = value;}
			}
			public bool OneValuePerPage
			{
				get{return ((CompactGroupingControl)Component).OneValuePerPage;}
				set{((CompactGroupingControl)Component).OneValuePerPage = value;}
			}
			public CellSizeAutoAdaptingTypes CellSizeAutoAdapting
			{
				get{return ((CompactGroupingControl)Component).CellSizeAutoAdapting;}
				set{((CompactGroupingControl)Component).CellSizeAutoAdapting = value;}
			}
            [EditorAttribute(typeof(Webb.Reports.ExControls.Editors.PropertyEditor), typeof(System.Drawing.Design.UITypeEditor))]
            public OurBordersSetting OurBordersSetting
            {
                get { return ((CompactGroupingControl)Component).OurBordersSetting; }
                set { ((CompactGroupingControl)Component).OurBordersSetting = value; }

            }
			public bool SizeSelfAdapting
			{
				get{return ((CompactGroupingControl)Component).SizeSelfAdapting;}
				set{((CompactGroupingControl)Component).SizeSelfAdapting = value;}
			}

			public int TopCount
			{
				get{return ((CompactGroupingControl)Component).TopCount;}
				set{((CompactGroupingControl)Component).TopCount = value;}
			}

			public bool Total
			{
				get{return ((CompactGroupingControl)Component).Total;}
				set{((CompactGroupingControl)Component).Total = value;}
			}

			public string TotalTitle
			{
				get{return ((CompactGroupingControl)Component).TotalTitle;}
				set{((CompactGroupingControl)Component).TotalTitle = value;}
			}
			public ComputedStyle MatrixDisplay
			{
				get{return ((CompactGroupingControl)Component).MatrixDisplay;}
				set{((CompactGroupingControl)Component).MatrixDisplay = value;}
			}

			public string SectionTitle
			{
				get{return ((CompactGroupingControl)Component).SectionTitle;}
				set{((CompactGroupingControl)Component).SectionTitle = value;}
			}

			public bool SectionInOneRow
			{
				get{return ((CompactGroupingControl)Component).SectionInOneRow;}
				set{((CompactGroupingControl)Component).SectionInOneRow = value;}
			}

			public Webb.Collections.Int32Collection TotalColumns
			{
				get{return ((CompactGroupingControl)Component).TotalColumns;}
				set{((CompactGroupingControl)Component).TotalColumns = value;}
			}

			public int HeightPerPage
			{
				get{return ((CompactGroupingControl)Component).HeightPerPage;}
				set{((CompactGroupingControl)Component).HeightPerPage = value;}
			}

			public bool PlayAfter
			{
				get{return ((CompactGroupingControl)Component).PlayAfter;}
				set{((CompactGroupingControl)Component).PlayAfter = value;}
			}

            [EditorAttribute(typeof(Webb.Reports.Editors.SectionFiltersEditor), typeof(System.Drawing.Design.UITypeEditor))]	//06-17-2008@Scott
            public SectionFilterCollection SectionFilters
            {
                get{return ((CompactGroupingControl)Component).SectionFilters;}
				set{((CompactGroupingControl)Component).SectionFilters = value;}
            }

			[EditorAttribute(typeof(Webb.Reports.Editors.SectionFiltersEditor), typeof(System.Drawing.Design.UITypeEditor))]	//Modified at 2009-1-15 16:44:42@Scott
			public SectionFilterCollectionWrapper SectionFiltersWrapper
			{
				get{return ((CompactGroupingControl)Component).SectionFiltersWrapper;}
				set{((CompactGroupingControl)Component).SectionFiltersWrapper = value;}
			}

			public bool HaveHeader   //Modified at 2008-10-21 9:01:27@Simon
			{
				get{return ((CompactGroupingControl)Component).HaveHeader;}
				set{((CompactGroupingControl)Component).HaveHeader = value;}
			}              	//end Modified at 2008-10-21 9:01:27@Simon
            public bool Matrix   //Modified at 2008-10-21 9:01:27@Simon
			{
				get{return ((CompactGroupingControl)Component).Matrix;}
				set{((CompactGroupingControl)Component).Matrix = value;}
			}      
			public bool WidthEnable   //Modified at 2008-10-21 9:01:27@Simon
			{
				get{return ((CompactGroupingControl)Component).WidthEnable;}
				set{((CompactGroupingControl)Component).WidthEnable = value;}
			} 
			public bool SepTotal   //Modified at 2008-10-21 9:01:27@Simon
			{
				get{return ((CompactGroupingControl)Component).SepTotal;}
				set{((CompactGroupingControl)Component).SepTotal = value;}
			} 
			//ctor
			public CompactGroupingActionList(ExControlDesigner designer) : base(designer) 
			{

			} 
			//Methods
            protected override void FillActionItemCollection(CurrentDesign.DesignerActionItemCollection actionItems)
			{
				base.FillActionItemCollection (actionItems);
				AddPropertyItem(actionItems, "Filter", "Filter", "Filters");
          
				AddPropertyItem(actionItems, "HaveHeader", "HaveHeader", "Show Field Title");
				AddPropertyItem(actionItems, "ShowRowIndicators", "ShowRowIndicators", "Show Row Indicators");

                AddPropertyItem(actionItems, "CellSizeAutoAdapting", "CellSizeAutoAdapting", "Size Auto-Adapting");

                AddPropertyItem(actionItems, "OurBordersSetting", "OurBordersSetting", "Modify Out Borders of Table");

  				if(this.Matrix)
				{
					AddPropertyItem(actionItems, "MatrixDisplay", "MatrixDisplay", "Matrix Display Style");  
				}	
				else
				{
					AddPropertyItem(actionItems, "TopCount", "TopCount", "TopCount");  //@simon
				}
                
			}
		}
		#endregion
	}
	#endregion    
}
