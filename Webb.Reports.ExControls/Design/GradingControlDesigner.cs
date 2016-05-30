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
    #region public class GradingControlDesigner
    /*Descrition:   */
    public class GradingControlDesigner : ExControlDesigner
    {
        //Wu.Country@2007-10-31 03:14 PM added this class.
        //Fields
        //Properties
        //ctor
        public GradingControlDesigner()
        {
        }
        //Methods
        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            this._DesignForm = new UI.DF_GradingControl();
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
            this._ActionLists.Add(new GradingActionList(this));
        }

        #region internal class GradingActionList : ExControlActionList
        /*Descrition:   */
        internal class GradingActionList : ExControlActionList
        {
            //Wu.Country@2007-11-15 12:53 PM added this class.
            //Fields
            //Properties
            [EditorAttribute(typeof(Webb.Reports.Editors.DBFilterEditor), typeof(System.Drawing.Design.UITypeEditor))]	//06-17-2008@Scott
            public Webb.Data.DBFilter Filter
            {
                get { return ((GradingControl)Component).Filter; }
                set { SetPropertyValue("Filter", value); }
            }

          
            [EditorAttribute(typeof(Webb.Reports.ExControls.Editors.PropertyEditor), typeof(System.Drawing.Design.UITypeEditor))]
            public OurBordersSetting OurBordersSetting
            {
                get { return ((GradingControl)Component).OurBordersSetting; }
                set { ((GradingControl)Component).OurBordersSetting = value; }

            }

            public bool ShowRowIndicators
            {
                get { return ((GradingControl)Component).ShowRowIndicators; }
                set { ((GradingControl)Component).ShowRowIndicators = value; }
            }           

            public CellSizeAutoAdaptingTypes CellSizeAutoAdapting
            {
                get { return ((GradingControl)Component).CellSizeAutoAdapting; }
                set { ((GradingControl)Component).CellSizeAutoAdapting = value; }
            }
      
            public int TopCount
            {
                get { return ((GradingControl)Component).TopCount; }
                set { ((GradingControl)Component).TopCount = value; }
            }  
            public bool SectionInOneRow
            {
                get { return ((GradingControl)Component).SectionInOneRow; }
                set { ((GradingControl)Component).SectionInOneRow = value; }
            }

            public bool HaveHeader   //Modified at 2008-10-21 9:01:27@Simon
            {
                get { return ((GradingControl)Component).HaveHeader; }
                set { ((GradingControl)Component).HaveHeader = value; }
            }              	//end Modified at 2008-10-21 9:01:27@Simon
         
            //ctor
            public GradingActionList(ExControlDesigner designer)
                : base(designer)
            {

            }
            //Methods
            protected override void FillActionItemCollection(CurrentDesign.DesignerActionItemCollection actionItems)
            {
                base.FillActionItemCollection(actionItems);

                AddPropertyItem(actionItems, "Filter", "Filter", "Filters");
          
                AddPropertyItem(actionItems, "ShowRowIndicators", "ShowRowIndicators", "Show Row Indicators");

                AddPropertyItem(actionItems, "HaveHeader", "HaveHeader", "Show Field Title");
           
                AddPropertyItem(actionItems, "SectionInOneRow", "SectionInOneRow", "Section In One Row");
              
                AddPropertyItem(actionItems, "TopCount", "TopCount", "Top Count");
       
                AddPropertyItem(actionItems, "OurBordersSetting", "OurBordersSetting", "Modify Out Borders of Table");

                AddPropertyItem(actionItems, "CellSizeAutoAdapting", "CellSizeAutoAdapting", "Size Auto-Adapting");


            }
        }
        #endregion
    }
    #endregion
}
