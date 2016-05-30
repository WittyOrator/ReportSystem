using System;
/***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:ExControls.cs
 * Author:Simon
 * Create Time:7/1/2011
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
    #region public class GradingControl
    /*Descrition:   */
    [XRDesigner("Webb.Reports.ExControls.Design.GradingControlDesigner,Webb.Reports.ExControls"),
    Designer("Webb.Reports.ExControls.Design.GradingControlDesigner,Webb.Reports.ExControls"),
    ToolboxBitmap(typeof(Webb.Reports.ExControls.ResourceManager), "Bitmaps.GradingControl.bmp")]
    public class GradingControl : ExControl//, IPrintable,IExtendedControl
    {
        //Wu.Country@2007-10-31 02:40 PM added this class.
        //Fields
        public GradingView _GradingView
        {
            get { return base._MainView as GradingView; }
        }     

        [EditorAttribute(typeof(Webb.Reports.ExControls.Editors.PropertyEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public OurBordersSetting OurBordersSetting
        {
            get { return _GradingView.OurBordersSetting; }
            set
            {
                this._GradingView.OurBordersSetting = value;

                if (DesignMode) this._GradingView.UpdateView();
            }
        }


        //Properties
        public bool ShowRowIndicators
        {
            get { return _GradingView.ShowRowIndicators; }
            set
            {
                this._GradingView.ShowRowIndicators = value;

                if (DesignMode) this._GradingView.UpdateView();
            }
        }      


     

        public bool HaveHeader   //Modified at 2008-10-21 9:01:27@Simon
        {
            get { return _GradingView.HaveHeader; }
            set
            {
                this._GradingView.HaveHeader = value;

                if (DesignMode) this._GradingView.UpdateView();
            }
        }                       //end Modified at 2008-10-21 9:01:36@Simon

        public CellSizeAutoAdaptingTypes CellSizeAutoAdapting
        {
            get { return this._GradingView.CellSizeAutoAdapting; }
            set
            {
                this._GradingView.CellSizeAutoAdapting = value;

                if (DesignMode) this._GradingView.UpdateView();
            }
        }   

        [EditorAttribute(typeof(Webb.Reports.Editors.DBFilterEditor), typeof(System.Drawing.Design.UITypeEditor))]	//06-18-2008@Scott
        public Webb.Data.DBFilter Filter
        {
            get { return this._GradingView.Filter; }
            set { this._GradingView.Filter = value; }
        }

        public void UpdateView()
        {
            if (DesignMode) this._GradingView.UpdateView();
        }

        public int TopCount
        {
            get { return this._GradingView.TopCount; }
            set
            {
                if (this._GradingView.TopCount == value) return;

                this._GradingView.TopCount = value;

                if (DesignMode) this._GradingView.UpdateView();
            }
        }

    
        public bool SectionInOneRow
        {
            get { return this._GradingView.SectionInOneRow; }
            set
            {
                if (this._GradingView.SectionInOneRow == value) return;

                this._GradingView.SectionInOneRow = value;

                if (DesignMode) this._GradingView.UpdateView();
            }
        }   
    
    
        //06-12-2008@Scott
   

  

        //ctor
        public GradingControl()
        {
            this._MainView = new GradingView(this);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            this.MainView.Paint(e);
        }



    }
    #endregion


}
