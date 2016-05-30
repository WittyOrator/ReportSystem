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

#define OldVersion

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
    #region public class GroupDesigner
    /*Descrition:   */
    public class GroupDesigner : ExControlDesigner
    {
        //Wu.Country@2007-10-31 03:14 PM added this class.
        //Fields
        //Properties
        //ctor
        public GroupDesigner()
        {
        }
        //Methods
        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            this._DesignForm = new UI.DF_GroupingControl();
            this._DesignForm.SetView(this._Control.MainView);
        }
        //
        public override void RunDesigner(object sender, EventArgs e)
        {
            //base.RunDesigner (sender, e);
            this._DesignForm.SetView(this._Control.MainView);
            DialogResult m_Result = this._DesignForm.ShowDialog();
            if (m_Result == DialogResult.OK)
            {
                this._DesignForm.UpdateView(this._Control.MainView);
                this._Control.CalculateResult();
            }
        }

        public override void InitializeVerbs()
        {
            base.InitializeVerbs();
            this._Verbs.Add(new DesignerVerb("Update View", new EventHandler(UpdateView)));
        }

        private void UpdateView(object sender, EventArgs e)
        {
            this._Control.MainView.UpdateView();
        }

        public override void InitializeActionList()
        {
            this._ActionLists.Add(new GroupingActionList(this));
        }

        #region internal class GroupingActionList : ExControlActionList
        /*Descrition:   */
        internal class GroupingActionList : ExControlActionList
        {
            //Wu.Country@2007-11-15 12:53 PM added this class.
            //Fields
            //Properties
            [EditorAttribute(typeof(Webb.Reports.Editors.DBFilterEditor), typeof(System.Drawing.Design.UITypeEditor))]	//06-17-2008@Scott
            public Webb.Data.DBFilter Filter
            {
                get { return ((GroupingControl)Component).Filter; }
                set { SetPropertyValue("Filter", value); }
            }

            [EditorAttribute(typeof(Webb.Reports.ExControls.Editors.PropertyEditor), typeof(System.Drawing.Design.UITypeEditor))]
            public GroupAdvancedSetting GroupAdvancedSetting
            {
                get { return ((GroupingControl)Component).GroupAdvancedSetting; }
                set { ((GroupingControl)Component).GroupAdvancedSetting = value; }

            }
            [EditorAttribute(typeof(Webb.Reports.ExControls.Editors.PropertyEditor), typeof(System.Drawing.Design.UITypeEditor))]
            public OurBordersSetting OurBordersSetting
            {
                get { return ((GroupingControl)Component).OurBordersSetting; }
                set { ((GroupingControl)Component).OurBordersSetting = value; }

            }


            public bool ShowRowIndicators
            {
                get { return ((GroupingControl)Component).ShowRowIndicators; }
                set { ((GroupingControl)Component).ShowRowIndicators = value; }
            }
            public bool OneValuePerPage
            {
                get { return ((GroupingControl)Component).OneValuePerPage; }
                set { ((GroupingControl)Component).OneValuePerPage = value; }
            }
        
            public CellSizeAutoAdaptingTypes CellSizeAutoAdapting
            {
                get { return ((GroupingControl)Component).CellSizeAutoAdapting; }
                set { ((GroupingControl)Component).CellSizeAutoAdapting = value; }
            }
            public bool SizeSelfAdapting
            {
                get { return ((GroupingControl)Component).SizeSelfAdapting; }
                set { ((GroupingControl)Component).SizeSelfAdapting = value; }
            }

            public int TopCount
            {
                get { return ((GroupingControl)Component).TopCount; }
                set { ((GroupingControl)Component).TopCount = value; }
            }

            public bool Total
            {
                get { return ((GroupingControl)Component).Total; }
                set { ((GroupingControl)Component).Total = value; }
            }

            public string TotalTitle
            {
                get { return ((GroupingControl)Component).TotalTitle; }
                set { ((GroupingControl)Component).TotalTitle = value; }
            }

            [Editor(typeof(Editors.SidesEditor), typeof(System.Drawing.Design.UITypeEditor))]
            public DevExpress.XtraPrinting.BorderSide TotalSides
            {
                get { return ((GroupingControl)Component).TotalSides; }
                set { ((GroupingControl)Component).TotalSides = value; }
            }

            public string SectionTitle
            {
                get { return ((GroupingControl)Component).SectionTitle; }
                set { ((GroupingControl)Component).SectionTitle = value; }
            }

            public bool SectionInOneRow
            {
                get { return ((GroupingControl)Component).SectionInOneRow; }
                set { ((GroupingControl)Component).SectionInOneRow = value; }
            }

            public bool SectionSides
            {
                get { return ((GroupingControl)Component).SectionSides; }
                set { ((GroupingControl)Component).SectionSides = value; }
            }

            public Webb.Collections.Int32Collection TotalColumns
            {
                get { return ((GroupingControl)Component).TotalColumns; }
                set { ((GroupingControl)Component).TotalColumns = value; }
            }

            public int HeightPerPage
            {
                get { return ((GroupingControl)Component).HeightPerPage; }
                set { ((GroupingControl)Component).HeightPerPage = value; }
            }

            public bool PlayAfter
            {
                get { return ((GroupingControl)Component).PlayAfter; }
                set { ((GroupingControl)Component).PlayAfter = value; }
            }

            [EditorAttribute(typeof(Webb.Reports.Editors.SectionFiltersEditor), typeof(System.Drawing.Design.UITypeEditor))]	//06-17-2008@Scott
            public SectionFilterCollection SectionFilters
            {
                get { return ((GroupingControl)Component).SectionFilters; }
                set { ((GroupingControl)Component).SectionFilters = value; }
            }

            [EditorAttribute(typeof(Webb.Reports.Editors.SectionFiltersEditor), typeof(System.Drawing.Design.UITypeEditor))]	//Modified at 2009-1-15 16:44:42@Scott
            public SectionFilterCollectionWrapper SectionFiltersWrapper
            {
                get { return ((GroupingControl)Component).SectionFiltersWrapper; }
                set { ((GroupingControl)Component).SectionFiltersWrapper = value; }
            }

            public bool HaveHeader   //Modified at 2008-10-21 9:01:27@Simon
            {
                get { return ((GroupingControl)Component).HaveHeader; }
                set { ((GroupingControl)Component).HaveHeader = value; }
            }              	//end Modified at 2008-10-21 9:01:27@Simon
            public int GroupSidesColumn
            {
                get { return ((GroupingControl)Component).GroupSidesColumn; }
                set { ((GroupingControl)Component).GroupSidesColumn = value; }
            }
            public bool VerticalGroupSides
            {
                get { return ((GroupingControl)Component).VerticalGroupSides; }
                set { ((GroupingControl)Component).VerticalGroupSides = value; }
            }
            public byte ChartRowHeight
            {
                get { return ((GroupingControl)Component).ChartRowHeight; }
                set { ((GroupingControl)Component).ChartRowHeight = value; }
            }
            //ctor
            public GroupingActionList(ExControlDesigner designer)
                : base(designer)
            {

            }
            //Methods
            protected override void FillActionItemCollection(CurrentDesign.DesignerActionItemCollection actionItems)
            {
                base.FillActionItemCollection(actionItems);

                AddPropertyItem(actionItems, "Filter", "Filter", "Filters");

                AddPropertyItem(actionItems, "SectionFiltersWrapper", "SectionFiltersWrapper", "SectionFilters"); //Modified at 2009-1-15 16:45:45@Scott

                AddPropertyItem(actionItems, "ShowRowIndicators", "ShowRowIndicators", "Show Row Indicators");

                
                AddPropertyItem(actionItems, "HaveHeader", "HaveHeader", "Show Field Title");

                AddPropertyItem(actionItems, "VerticalGroupSides", "VerticalGroupSides", "Use ColumnBorders");

                AddPropertyItem(actionItems, "SectionInOneRow", "SectionInOneRow", "Section In One Row");

                AddPropertyItem(actionItems, "SectionTitle", "SectionTitle", "Section Title");

                AddPropertyItem(actionItems, "TopCount", "TopCount", "Top Count");

                AddPropertyItem(actionItems, "GroupAdvancedSetting", "GroupAdvancedSetting", "Advanced Properties Setting");

                AddPropertyItem(actionItems, "OurBordersSetting", "OurBordersSetting", "Modify Out Borders of Table");

                AddPropertyItem(actionItems, "CellSizeAutoAdapting", "CellSizeAutoAdapting", "Size Auto-Adapting");
                

            }
        }
        #endregion
    }
    #endregion
}
