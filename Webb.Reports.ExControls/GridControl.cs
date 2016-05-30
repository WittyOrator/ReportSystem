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
	[XRDesigner("Webb.Reports.ExControls.Design.GridDesigner,Webb.Reports.ExControls"),
	Designer("Webb.Reports.ExControls.Design.GridDesigner,Webb.Reports.ExControls")]
	public class GridControl : ExControl
	{
        public int WrapColumns
        {
            get { return this.GridView.WrappedColumns; }
            set { this.GridView.WrappedColumns = value; }
        }

		[EditorAttribute(typeof(Webb.Reports.Editors.DBFilterEditor), typeof(System.Drawing.Design.UITypeEditor))]	//06-18-2008@Scott
		public Webb.Data.DBFilter Filter
		{
			get{return this.GridView.Filter;}
			set{this.GridView.Filter = value;}
		}
		public bool HaveHeader
		{
			get{return this.GridView.HaveHeader;}
			set
			{				
				this.GridView.HaveHeader = value;

				if(DesignMode) this.GridView.UpdateView();
			}
		}
        [EditorAttribute(typeof(Webb.Reports.ExControls.Editors.PropertyEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public OurBordersSetting OurBordersSetting
        {
            get { return GridView.OurBordersSetting; }
            set
            {
                this.GridView.OurBordersSetting = value;

                if (DesignMode) this.GridView.UpdateView();
            }
        }
      
		public int TopCount
		{
			get{return this.GridView.TopCount;}
			set
			{
				if(this.GridView.TopCount == value) return;

				this.GridView.TopCount = value;

				if(DesignMode) this.GridView.UpdateView();
			}
		}
		public int diffColumns
		{
			get{return this.GridView.diffColumns;}
			set
			{
				if(this.GridView.diffColumns == value) return;

				this.GridView.diffColumns = value;

				if(DesignMode) this.GridView.UpdateView();
			}
		}
		[Browsable(false)]
		public string MinusDiffColumns
		{
			get{return this.GridView.diffColumns.ToString();}
			set
			{
				if(Webb.Utility.IsDigital(value))
				{
					this.GridView.diffColumns = int.Parse(value);

					if(DesignMode) this.GridView.UpdateView();
				}
			}
		}


		public bool ColumnsAfterGroup
		{
			get{return this.GridView.ColumnafterGroup;}
			set
			{
				if(this.GridView.ColumnafterGroup == value) return;

				this.GridView.ColumnafterGroup = value;
			
				if(DesignMode)
				{
					this.GridView.UpdateView();
				}
			}
		}
		public bool ShowRowIndicators
		{
			get{return this.GridView.ShowRowIndicators;}
			set
			{
				if(this.GridView.ShowRowIndicators == value) return;

				this.GridView.ShowRowIndicators = value;
			
				if(DesignMode)
				{
					this.GridView.UpdateView();
				}
			}
		}

		public CellSizeAutoAdaptingTypes CellSizeAutoAdapting
		{
			get{return this.GridView.CellSizeAutoAdapting;}
			set
			{
				this.GridView.CellSizeAutoAdapting = value;

				if(DesignMode) this.GridView.UpdateView();
			}
		}

		public bool Total
		{
			get{return this.GridView.Total;}
			set
			{
				this.GridView.Total = value;

				if(DesignMode) this.GridView.UpdateView();
			}
		}
        [Browsable(false)]
		public int HeightPerPage
		{
			get{return this.GridView.HeightPerPage;}
			set
			{
				this.GridView.HeightPerPage = value;

				if(DesignMode) this.GridView.UpdateView();
			}
		}

		public GridControl()
		{
			this._MainView = new GridView(this);
		}

		public GridView GridView
		{
			get{return this._MainView as GridView;}
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
			get{return this.GridView.TotalTitle;}
			set
			{
				if(this.GridView.TotalTitle == value) return;

				this.GridView.TotalTitle = value;
			
				if(DesignMode) this.GridView.UpdateView();
			}
		}    
	}
}
