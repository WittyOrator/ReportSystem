//12-20-2007@Scott
using System;
using System.ComponentModel;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

using DevExpress.XtraReports;

using Webb.Reports.DataProvider;
using Webb.Reports.ExControls.Data;

using Webb.Reports.ExControls.Views;

namespace Webb.Reports.ExControls
{
    [XRDesigner("Webb.Reports.ExControls.Design.CustomImageDesigner,Webb.Reports.ExControls"),
    Designer("Webb.Reports.ExControls.Design.CustomImageDesigner,Webb.Reports.ExControls"),
    ToolboxBitmap(typeof(Webb.Reports.ExControls.ResourceManager), "Bitmaps.Image.bmp")]
    public class CustomImage : ExControl
    {
        public PictureBoxSizeMode SizeMode
        {
            get { return this.CustomImageView.SizeMode; }
            set
            {
                this.CustomImageView.SizeMode = value;

                if (DesignMode)
                {
                    this.MainView.UpdateView();
                }
            }
        }

        [Editor(typeof(Webb.Reports.Editors.DiaImageEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string DiagramImage         //Added this code at 2008-11-3 10:47:00@Simon
        {
            get { return this.CustomImageView.DiagramImage; }
            set
            {
                this.CustomImageView.DiagramImage = value;
                if (DesignMode)
                {
                    this.CustomImageView.UpdateView();
                }
            }
        }

        public SortingColumnCollection SortingColumns
        {

            get { return this.CustomImageView.SortingColumns; }
            set
            {
                this.CustomImageView.SortingColumns = value;

                if (DesignMode)
                {
                    this.CustomImageView.UpdateView();
                }

            }
        }
     

        public Image Image
        {
            get { return this.CustomImageView.Image; }
            set
            {
                if (this.CustomImageView.Image == value) return;

                this.CustomImageView.Image = value;

                if (DesignMode)
                {
                    this.MainView.UpdateView();
                }
            }
        }

        public CustomImage()
        {
            this._MainView = new CustomImageView(this);
        }

        [Browsable(false)]
        public CustomImageView CustomImageView
        {
            get { return this._MainView as CustomImageView; }
        }

        [TypeConverter(typeof(Webb.Data.PublicDBFieldConverter))]
        public string Field
        {
            get { return this.CustomImageView.Field; }
            set { this.CustomImageView.Field = value; }
        }
        public bool UsePicDir
        {
            get { return this.CustomImageView.UsePicDir; }
            set
            {
                if (this.CustomImageView.UsePicDir == value) return;

                this.CustomImageView.UsePicDir = value;

                if (DesignMode)
                {
                    this._MainView.UpdateView();
                }
            }
        }

        public bool ReadDiagramFromPlaybook
        {
            get { return this.CustomImageView.ReadDiagramFromPlaybook; }
            set
            {
                if (this.CustomImageView.ReadDiagramFromPlaybook == value) return;

                this.CustomImageView.ReadDiagramFromPlaybook = value;

                if (DesignMode)
                {
                    this._MainView.UpdateView();
                }
            }
        }

        public InvertPictureMode InvertPicture
        {
            get { return this.CustomImageView.InvertPicture; }
            set
            {
                if (this.CustomImageView.InvertPicture == value) return;

                this.CustomImageView.InvertPicture = value;

                if (DesignMode)
                {
                    this._MainView.UpdateView();
                }
            }
        }

        public PlayBookScoutType PlayBookScoutType
        {
            get { return this.CustomImageView.PlayBookScoutType; }
            set
            {
                if (this.CustomImageView.PlayBookScoutType == value) return;

                this.CustomImageView.PlayBookScoutType = value;

                if (DesignMode)
                {
                    this._MainView.UpdateView();
                }
            }
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            this.MainView.Paint(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (DesignMode)
            {
                this._MainView.UpdateView();
            }
        }

        public bool OneValue
        {
            get { return this.CustomImageView.OneValue; }
            set
            {
                if (this.CustomImageView.OneValue == value) return;

                this.CustomImageView.OneValue = value;

                if (DesignMode)
                {
                    this._MainView.UpdateView();
                }
            }
        }
    }
}
