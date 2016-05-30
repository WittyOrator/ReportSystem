using System;
using System.ComponentModel;
using System.Collections;
using System.Drawing;

using DevExpress.XtraReports;

using Webb.Reports.DataProvider;
using Webb.Reports.ExControls.Views;
using System.Data;
using System.Text;

namespace Webb.Reports.ExControls
{
    [XRDesigner("Webb.Reports.ExControls.Design.HorizontalGridDesigner,Webb.Reports.ExControls"),
    Designer("Webb.Reports.ExControls.Design.HorizontalGridDesigner,Webb.Reports.ExControls"),
    ToolboxBitmap(typeof(Webb.Reports.ExControls.ResourceManager), "Bitmaps.HorizonGroup.bmp")]
	public class HorizontalGridControl : ExControl
	{
        public int WrapColumns
        {
            get { return this.HorizontalGridView.WrappedColumns; }
            set { this.HorizontalGridView.WrappedColumns = value; }
        }

		[EditorAttribute(typeof(Webb.Reports.Editors.DBFilterEditor), typeof(System.Drawing.Design.UITypeEditor))]	//06-18-2008@Scott
		public Webb.Data.DBFilter Filter
		{
			get{return this.HorizontalGridView.Filter;}
			set{this.HorizontalGridView.Filter = value;}
		}
		public bool HaveHeader
		{
            get { return this.HorizontalGridView.HaveHeader; }
			set
			{
				this.HorizontalGridView.HaveHeader = value;

				if(DesignMode) this.HorizontalGridView.UpdateView();
			}
		}
        [EditorAttribute(typeof(Webb.Reports.ExControls.Editors.PropertyEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public OurBordersSetting OurBordersSetting
        {
            get { return HorizontalGridView.OurBordersSetting; }
            set
            {
                this.HorizontalGridView.OurBordersSetting = value;

                if (DesignMode) this.HorizontalGridView.UpdateView();
            }
        }
      
		public int TopCount
		{
			get{return this.HorizontalGridView.TopCount;}
			set
			{
				if(this.HorizontalGridView.TopCount == value) return;

				this.HorizontalGridView.TopCount = value;

				if(DesignMode) this.HorizontalGridView.UpdateView();
			}
		}
		public int diffColumns
		{
			get{return this.HorizontalGridView.diffColumns;}
			set
			{
				if(this.HorizontalGridView.diffColumns == value) return;

				this.HorizontalGridView.diffColumns = value;

				if(DesignMode) this.HorizontalGridView.UpdateView();
			}
		}
		[Browsable(false)]
		public string MinusDiffColumns
		{
			get{return this.HorizontalGridView.diffColumns.ToString();}
			set
			{
				if(Webb.Utility.IsDigital(value))
				{
					this.HorizontalGridView.diffColumns = int.Parse(value);

					if(DesignMode) this.HorizontalGridView.UpdateView();
				}
			}
		}


		public bool ColumnsAfterGroup
		{
			get{return this.HorizontalGridView.ColumnafterGroup;}
			set
			{
				if(this.HorizontalGridView.ColumnafterGroup == value) return;

				this.HorizontalGridView.ColumnafterGroup = value;
			
				if(DesignMode)
				{
					this.HorizontalGridView.UpdateView();
				}
			}
		}
		public bool ShowRowIndicators
		{
			get{return this.HorizontalGridView.ShowRowIndicators;}
			set
			{
				if(this.HorizontalGridView.ShowRowIndicators == value) return;

				this.HorizontalGridView.ShowRowIndicators = value;
			
				if(DesignMode)
				{
					this.HorizontalGridView.UpdateView();
				}
			}
		}

		public CellSizeAutoAdaptingTypes CellSizeAutoAdapting
		{
			get{return this.HorizontalGridView.CellSizeAutoAdapting;}
			set
			{
				this.HorizontalGridView.CellSizeAutoAdapting = value;

				if(DesignMode) this.HorizontalGridView.UpdateView();
			}
		}

		public bool Total
		{
			get{return this.HorizontalGridView.Total;}
			set
			{
				this.HorizontalGridView.Total = value;

				if(DesignMode) this.HorizontalGridView.UpdateView();
			}
		}
        [Browsable(false)]
		public int HeightPerPage
		{
			get{return this.HorizontalGridView.HeightPerPage;}
			set
			{
				this.HorizontalGridView.HeightPerPage = value;

				if(DesignMode) this.HorizontalGridView.UpdateView();
			}
		}

        public HorizontalGridControl()
		{
			this._MainView = new HorizontalGridView(this);
		}

        public HorizontalGridView HorizontalGridView
		{
            get { return this._MainView as HorizontalGridView; }
		}

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			this.MainView.Paint(e);
		}

		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged (e);
			if(DesignMode)
			{
				this._MainView.UpdateView();
			}
		}
		public string TotalTitle
		{
            get { return this.HorizontalGridView.TotalTitle; }
			set
			{
                if (this.HorizontalGridView.TotalTitle == value) return;

                this.HorizontalGridView.TotalTitle = value;

                if (DesignMode) this.HorizontalGridView.UpdateView();
			}
		}    
	}
}
