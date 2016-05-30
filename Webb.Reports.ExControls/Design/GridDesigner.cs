using System;
using System.Windows;
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms.Design;
using System.Drawing;

using DevExpress.XtraReports;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Design;
using DevExpress.Utils.Design;
using DevExpress.XtraReports.Localization;
using DevExpress.Utils;
//using DevExpress.XtraGrid.Design;

using Webb.Reports.ExControls.Views;

using CurrentDesign = DevExpress.Utils.Design;  //08-13-2008@Scott

namespace Webb.Reports.ExControls.Design
{
	/// <summary>
	/// Summary description for GridDesigner.
	/// </summary>
	public class GridDesigner : ExControlDesigner
	{
		public GridDesigner(){}

		public override void Initialize(IComponent component)
		{
			base.Initialize (component);
			this._DesignForm = new UI.DF_GridControl();
			this._DesignForm.SetView(this._Control.MainView);
		}

		public override void InitializeActionList()
		{
			//base.InitializeActionList();
			this._ActionLists.Add(new GridActionList(this));
		}

		public override void InitializeVerbs()
		{
			base.InitializeVerbs();
			this._Verbs	.Add(new DesignerVerb("Update View",new EventHandler(UpdateView)));
		}

		private void UpdateView(object sender,EventArgs e)
		{
			this._Control.MainView.UpdateView();
		}

		public override void RunDesigner(object sender, EventArgs e)
		{
			this._DesignForm.SetView(this._Control.MainView);

			DialogResult m_Result = this._DesignForm.ShowDialog();
			
			if(m_Result==DialogResult.OK)
			{
				this._DesignForm.UpdateView(this._Control.MainView);
				
				this._Control.CalculateResult();
			}
		}

		internal class GridActionList : Webb.Reports.ExControls.Design.ExControlDesigner.ExControlActionList
		{
			//ctor
			public GridActionList(ExControlDesigner designer) : base(designer){}

			[EditorAttribute(typeof(Webb.Reports.Editors.DBFilterEditor), typeof(System.Drawing.Design.UITypeEditor))]	//06-17-2008@Scott
			public Webb.Data.DBFilter Filter 
			{
				get { return ((GridControl)Component).Filter; }
				set { ((GridControl)Component).Filter = value;}
			}
			public int TopCount
			{
				get{return ((GridControl)Component).TopCount;}
				set{((GridControl)Component).TopCount = value;}
			}
			public bool HaveHeader
			{
				get{return ((GridControl)Component).HaveHeader;}
				set{((GridControl)Component).HaveHeader = value;}
			}

			public bool ShowRowIndicators
			{
				get{return ((GridControl)Component).ShowRowIndicators;}
				set{((GridControl)Component).ShowRowIndicators = value;}
			}
            public int WrapColumns
            {
                get { return ((GridControl)Component).WrapColumns; }
                set { ((GridControl)Component).WrapColumns = value; }
            }

			public CellSizeAutoAdaptingTypes CellSizeAutoAdapting
			{
				get{return ((GridControl)Component).CellSizeAutoAdapting;}
				set{((GridControl)Component).CellSizeAutoAdapting = value;}
			}           
			public bool Total
			{
				get{return ((GridControl)Component).Total;}
				set{((GridControl)Component).Total = value;}
			}
			public bool ColumnsAfterGroup
			{
				get{return ((GridControl)Component).ColumnsAfterGroup;}
				set{((GridControl)Component).ColumnsAfterGroup = value;}
			}
			public int diffColumns
			{	get{return ((GridControl)Component).diffColumns;}
				set{((GridControl)Component).diffColumns = value;}
			}
            [EditorAttribute(typeof(Webb.Reports.ExControls.Editors.PropertyEditor), typeof(System.Drawing.Design.UITypeEditor))]
            public OurBordersSetting OurBordersSetting
            {
                get { return ((GridControl)Component).OurBordersSetting; }
                set { ((GridControl)Component).OurBordersSetting = value; }

            }

			public string MinusDiffColumns
			{
				get{return ((GridControl)Component).MinusDiffColumns;}
				set{
						
					 ((GridControl)Component).MinusDiffColumns = value;						
				   }
			}

			public int HeightPerPage
			{
				get{return ((GridControl)Component).HeightPerPage;}
				set{((GridControl)Component).HeightPerPage = value;}
			}
			public string TotalTitle
			{
				get{return ((GridControl)Component).TotalTitle;}
				set{((GridControl)Component).TotalTitle = value;}
			}
			[EditorAttribute(typeof(Webb.Reports.Editors.SectionFiltersEditor), typeof(System.Drawing.Design.UITypeEditor))]	//Modified at 2008-11-18 10:48:18@Scott
			public SectionFilterCollection SectionFilters
			{
				get{return ((GridControl)Component).SectionFilters;}
				set{((GridControl)Component).SectionFilters = value;}
			}
			[EditorAttribute(typeof(Webb.Reports.Editors.SectionFiltersEditor), typeof(System.Drawing.Design.UITypeEditor))]	//Modified at 2009-1-15 16:44:42@Scott
			public SectionFilterCollectionWrapper SectionFiltersWrapper
			{
				get{return ((GridControl)Component).SectionFiltersWrapper;}
				set{((GridControl)Component).SectionFiltersWrapper = value;}
			}

			protected override void FillActionItemCollection(CurrentDesign.DesignerActionItemCollection actionItems)
			{
				base.FillActionItemCollection (actionItems);
               
				AddPropertyItem(actionItems, "HaveHeader", "HaveHeader", "HaveHeader");  //@simon
			    
				AddPropertyItem(actionItems, "TopCount", "TopCount", "TopCount");  //@simon

                AddPropertyItem(actionItems, "WrapColumns", "WrapColumns", "play list wrapped columns");  //@simon

				AddPropertyItem(actionItems, "MinusDiffColumns", "MinusDiffColumns", "Columns' indentation for group ");  //@simon

				AddPropertyItem(actionItems, "ColumnsAfterGroup", "ColumnsAfterGroup", "plays indent with group");  //@simon
 
				AddPropertyItem(actionItems, "Filter", "Filter", "Filters");

			
				AddPropertyItem(actionItems, "SectionFiltersWrapper", "SectionFiltersWrapper", "SectionFilters"); //Modified at 2009-1-15 16:45:45@Scott

				AddPropertyItem(actionItems, "ShowRowIndicators", "ShowRowIndicators", "Show Row Indicators");

				AddPropertyItem(actionItems, "Total", "Total", "Total");
                  
				AddPropertyItem(actionItems, "TotalTitle", "TotalTitle", "TotalTitle");  //@simon

                AddPropertyItem(actionItems, "OurBordersSetting", "OurBordersSetting", "Modify Out Borders of Table");

				AddPropertyItem(actionItems, "CellSizeAutoAdapting", "CellSizeAutoAdapting", "Size Auto-Adapting");
               
            }
		}
	}
}
