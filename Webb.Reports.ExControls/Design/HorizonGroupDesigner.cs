using System;
using System.Windows;
using System.Windows.Forms;
using System.ComponentModel.Design;
using System.Drawing;
using System.ComponentModel;
using DevExpress.XtraReports;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Design;
using DevExpress.Utils.Design;
using DevExpress.Utils;
using DevExpress.XtraReports.Localization;
using CurrentDesign = DevExpress.Utils.Design;

namespace Webb.Reports.ExControls.Design
{
    #region public class HorizonGroupDesigner
    /*Descrition:   */
    public class HorizonGroupDesigner : ExControlDesigner
    {
        //Wu.Country@2007-10-31 03:14 PM added this class.
        //Fields
        //Properties
        //ctor
        public HorizonGroupDesigner()
        {
        }
        //Methods
        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            this._DesignForm = new UI.DF_HorizonGroupControl();
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
            this._ActionLists.Add(new HorizonGroupActionList(this));
        }

        #region internal class HorizonGroupActionList : ExControlActionList
        /*Descrition:   */
        internal class HorizonGroupActionList : ExControlActionList
        {
            //Wu.Country@2007-11-15 12:53 PM added this class.
            //Fields
            //Properties
            [EditorAttribute(typeof(Webb.Reports.Editors.DBFilterEditor), typeof(System.Drawing.Design.UITypeEditor))]	//06-17-2008@Scott
            public Webb.Data.DBFilter Filter
            {
                get { return ((HorizonGroupControl)Component).Filter; }
                set { SetPropertyValue("Filter", value); }
            }
            public bool ShowRowIndicators
            {
                get { return ((HorizonGroupControl)Component).ShowRowIndicators; }
                set { ((HorizonGroupControl)Component).ShowRowIndicators = value; }
            }

            public bool HaveHeader   //Modified at 2008-10-21 9:01:27@Simon
            {
                get { return ((HorizonGroupControl)Component).HaveHeader; }
                set { ((HorizonGroupControl)Component).HaveHeader = value; }
            }             	//end Modified at 2008-10-21 9:01:27@Simon

            public bool Total   //Modified at 2008-10-21 9:01:27@Simon
            {
                get { return ((HorizonGroupControl)Component).Total; }
                set { ((HorizonGroupControl)Component).Total = value; }
            }
            public bool TotalOthers   //Modified at 2008-10-21 9:01:27@Simon
            {
                get { return ((HorizonGroupControl)Component).TotalOthers; }
                set { ((HorizonGroupControl)Component).TotalOthers = value; }
            }     

            public string TotalOthersName   //Modified at 2008-10-21 9:01:27@Simon
            {
                get { return ((HorizonGroupControl)Component).TotalOthersName; }
                set { ((HorizonGroupControl)Component).TotalOthersName = value; }
            }

            public Webb.Data.TotalType TotalPosition       //Added this code at 2009-2-5 15:55:21@Simon
            {
                get { return ((HorizonGroupControl)Component).TotalPosition; }
                set
                {
                    ((HorizonGroupControl)Component).TotalPosition = value;                   
                }
            }
            public Webb.Data.TotalType TotalOthersPosition       //Added this code at 2009-2-5 15:55:21@Simon
            {
                get { return ((HorizonGroupControl)Component).TotalOthersPosition; }
                set
                {
                    ((HorizonGroupControl)Component).TotalOthersPosition = value;                  
                }
            }          
            [EditorAttribute(typeof(Webb.Reports.ExControls.Editors.PropertyEditor), typeof(System.Drawing.Design.UITypeEditor))]	//06-18-2008@Scott
            public Data.GroupSummary SummaryForTotalGroup       //Added this code at 2009-2-5 15:55:21@Simon
            {
                get { return ((HorizonGroupControl)Component).SummaryForTotalGroup; }
                set
                {
                    ((HorizonGroupControl)Component).SummaryForTotalGroup = value;                    
                }
            }
            [EditorAttribute(typeof(Webb.Reports.ExControls.Editors.PropertyEditor), typeof(System.Drawing.Design.UITypeEditor))]	//06-18-2008@Scott
            public Data.GroupSummary SummaryForOthers       //Added this code at 2009-2-5 15:55:21@Simon
            {
                get { return ((HorizonGroupControl)Component).SummaryForOthers; }
                set
                {
                    ((HorizonGroupControl)Component).SummaryForOthers = value;                    
                }
            }

            [EditorAttribute(typeof(Webb.Reports.ExControls.Editors.PropertyEditor), typeof(System.Drawing.Design.UITypeEditor))]	//06-18-2008@Scott
            public BasicStyle TotalStyle   //Modified at 2008-10-21 9:01:27@Simon
            {
                get { return ((HorizonGroupControl)Component).TotalStyle; }
                set { ((HorizonGroupControl)Component).TotalStyle = value; }
            }
            public int HorizonTopCount   //Modified at 2008-10-21 9:01:27@Simon
            {
                get { return ((HorizonGroupControl)Component).HorizonTopCount; }
                set { ((HorizonGroupControl)Component).HorizonTopCount = value; }
            }        
            

            //ctor
            public HorizonGroupActionList(ExControlDesigner designer)
                : base(designer)
            {

            }
            //Methods
            protected override void FillActionItemCollection(CurrentDesign.DesignerActionItemCollection actionItems)
            {
                base.FillActionItemCollection(actionItems);

                AddPropertyItem(actionItems, "Filter", "Filter", "Filters");

                AddPropertyItem(actionItems, "HaveHeader", "HaveHeader", "Show Field Title");

                AddPropertyItem(actionItems, "ShowRowIndicators", "ShowRowIndicators", "Show Row Indicators");

                //AddPropertyItem(actionItems, "Total", "Total", "Add Total Row");
           
                //AddPropertyItem(actionItems, "TotalStyle", "TotalStyle", "Total Row Style");               

                AddPropertyItem(actionItems, "TotalPosition", "TotalPosition", "Show total in the 1st group");

                AddPropertyItem(actionItems, "SummaryForTotalGroup", "SummaryForTotalGroup", "Summary For total in 1st group");

                AddPropertyItem(actionItems, "HorizonTopCount", "HorizonTopCount", "TopCount Setting");

                AddPropertyItem(actionItems, "TotalOthersPosition", "TotalOthersPosition", "Show others for total in 1st group");

                AddPropertyItem(actionItems, "SummaryForOthers", "SummaryForOthers", "Summary For for others in 1st group");

                AddPropertyItem(actionItems, "TotalOthersName", "TotalOthersName", "Total Others Name");             

                

            }
        }
        #endregion
    }
    #endregion
}
