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
    #region public class HorizonGroupControl
    /*Descrition:   */
    [XRDesigner("Webb.Reports.ExControls.Design.HorizonGroupDesigner,Webb.Reports.ExControls"),
    Designer("Webb.Reports.ExControls.Design.HorizonGroupDesigner,Webb.Reports.ExControls"),
    ToolboxBitmap(typeof(Webb.Reports.ExControls.ResourceManager), "Bitmaps.HorizonGroup.bmp")]
    public class HorizonGroupControl: ExControl//, IPrintable,IExtendedControl
    {
        //Fields
        private HorizonGroupView _HorizonGroupView
        {
            get { return base._MainView as HorizonGroupView; }
        }

        #region Properties
        //Properties
        public bool ShowRowIndicators
        {
            get { return _HorizonGroupView.ShowRowIndicators; }
            set
            {
                this._HorizonGroupView.ShowRowIndicators = value;

                if (DesignMode) this._HorizonGroupView.UpdateView();
            }
        }

        public Webb.Data.TotalType TotalPosition       //Added this code at 2009-2-5 15:55:21@Simon
        {
            get { return _HorizonGroupView.TotalPosition; }
            set
            {
                this._HorizonGroupView.TotalPosition = value;

                if (DesignMode) this._HorizonGroupView.UpdateView();
            }
        }
        public Webb.Data.TotalType TotalOthersPosition       //Added this code at 2009-2-5 15:55:21@Simon
        {
            get { return _HorizonGroupView.TotalOthersPosition; }
            set
            {
                this._HorizonGroupView.TotalOthersPosition = value;

                if (DesignMode) this._HorizonGroupView.UpdateView();
            }
        }
        [EditorAttribute(typeof(Webb.Reports.ExControls.Editors.PropertyEditor), typeof(System.Drawing.Design.UITypeEditor))]	//06-18-2008@Scott
         public Data.GroupSummary SummaryForTotalGroup       //Added this code at 2009-2-5 15:55:21@Simon
        {
            get { return _HorizonGroupView.SummaryForTotalGroup; }
            set
            {
                this._HorizonGroupView.SummaryForTotalGroup = value;

                if (DesignMode) this._HorizonGroupView.UpdateView();
            }
        }
        [EditorAttribute(typeof(Webb.Reports.ExControls.Editors.PropertyEditor), typeof(System.Drawing.Design.UITypeEditor))]	//06-18-2008@Scott
        public Data.GroupSummary SummaryForOthers       //Added this code at 2009-2-5 15:55:21@Simon
        {
            get { return _HorizonGroupView.SummaryForOthers; }
            set
            {
                this._HorizonGroupView.SummaryForOthers = value;

                if (DesignMode) this._HorizonGroupView.UpdateView();
            }
        }
 
        public bool HaveHeader   //Modified at 2008-10-21 9:01:27@Simon
        {
            get { return _HorizonGroupView.HaveHeader; }
            set
            {
                this._HorizonGroupView.HaveHeader = value;

                if (DesignMode) this._HorizonGroupView.UpdateView();
            }
        }                       //end Modified at 2008-10-21 9:01:36@Simon

        public int HorizonTopCount   //Modified at 2008-10-21 9:01:27@Simon
        {
            get { return _HorizonGroupView.HorizonTopCount; }
            set
            {
                this._HorizonGroupView.HorizonTopCount = value;

                if (DesignMode) this._HorizonGroupView.UpdateView();
            }
        }                       //end Modified at 2008-10-21 9:01:36@Simon

        public bool Total   //Modified at 2008-10-21 9:01:27@Simon
        {
            get { return _HorizonGroupView.Total; }
            set
            {
                this._HorizonGroupView.Total = value;

                if (DesignMode) this._HorizonGroupView.UpdateView();
            }
        }
        public bool TotalOthers   //Modified at 2008-10-21 9:01:27@Simon
        {
            get { return _HorizonGroupView.TotalOthers; }
            set
            {
                this._HorizonGroupView.TotalOthers = value;

                if (DesignMode) this._HorizonGroupView.UpdateView();
            }
        }

        public string TotalOthersName   //Modified at 2008-10-21 9:01:27@Simon
        {
            get { return _HorizonGroupView.TotalOthersName; }
            set
            {
                this._HorizonGroupView.TotalOthersName = value;

                if (DesignMode) this._HorizonGroupView.UpdateView();
            }
        }
    

        public BasicStyle TotalStyle   //Modified at 2008-10-21 9:01:27@Simon
        {
            get { return _HorizonGroupView.TotalStyle; }
            set
            {
                this._HorizonGroupView.TotalStyle = value;

                if (DesignMode) this._HorizonGroupView.UpdateView();
            }
        }    
     

        [EditorAttribute(typeof(Webb.Reports.Editors.DBFilterEditor), typeof(System.Drawing.Design.UITypeEditor))]	//06-18-2008@Scott
        public Webb.Data.DBFilter Filter
        {
            get { return this._HorizonGroupView.Filter; }
            set { this._HorizonGroupView.Filter = value; }
        }
        #endregion

        public HorizonGroupControl()
        {
            this._MainView = new HorizonGroupView(this);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            this.MainView.Paint(e);
        }
    }
    #endregion
}

