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
	/// <summary>
	/// Summary description for StatControl.
	/// </summary>
	[XRDesigner("Webb.Reports.ExControls.Design.ChartControlExDesigner,Webb.Reports.ExControls"),
	Designer("Webb.Reports.ExControls.Design.ChartControlExDesigner,Webb.Reports.ExControls"),
	ToolboxBitmap(typeof(Webb.Reports.ExControls.ResourceManager), "Bitmaps.Chart.bmp")]
	public class ChartControlEx : ExControl
	{
		public ChartControlEx()
		{
			this.MainView = new WebbChartExView(this);
		}
        [Browsable(false)]
		public WebbChartExView WebbChartExView
		{
			get{return this._MainView as WebbChartExView;}
		}

		[EditorAttribute(typeof(Webb.Reports.Editors.DBFilterEditor), typeof(System.Drawing.Design.UITypeEditor))]	//06-18-2008@Scott
		public Webb.Data.DBFilter Filter
		{
			get{return this.WebbChartExView.Filter;}
			set{this.WebbChartExView.Filter = value;}
		}
		[EditorAttribute(typeof(Webb.Reports.Editors.DBFilterEditor), typeof(System.Drawing.Design.UITypeEditor))]	//06-18-2008@Scott
		public Webb.Data.DBFilter DenominatorFilter
		{
			get{return this.WebbChartExView.DenominatorFilter;}
			set{this.WebbChartExView.DenominatorFilter = value;}
		}
        [Browsable(false)]
		public bool HaveExisted         //Added this code at 2008-11-18 16:54:48@Simon
		{
			get{return this.WebbChartExView.HaveExisted;}
			set{this.WebbChartExView.HaveExisted = value;}
		}
        public bool CombinedTitle       //2009-8-14 11:41:20@Simon Add this Code
		{
			get{return this.WebbChartExView.CombinedTitle;}
			set{this.WebbChartExView.CombinedTitle = value;
				if(DesignMode)
				{
					this._MainView.UpdateView();
				}			
			   }
		}
        public bool ShowAxesMode         //Added this code at 2008-11-18 16:54:48@Simon
		{
			get{return this.WebbChartExView.ShowAxesMode;}
			set{this.WebbChartExView.ShowAxesMode = value;
				if(DesignMode)
				{
					this._MainView.UpdateView();
				}			
			   }
		}
		
		public Color TransparentBackground
		{
			get{return this.WebbChartExView.TransparentBackground;}
			set
			{
				this.WebbChartExView.TransparentBackground=value;	
				if(DesignMode)
				{
					this._MainView.UpdateView();
				}			
			}
		}
		public Color BackgroundColor         //Added this code at 2008-11-18 16:54:48@Simon
		{
			get{return this.WebbChartExView.BackgroundColor;}
			set
			{				
				this.WebbChartExView.BackgroundColor = value;
				if(DesignMode)
				{
					this._MainView.UpdateView();
				}			
			}
		}
		public int TopCount         //Added this code at 2008-11-18 16:54:48@Simon
		{
			get{return this.WebbChartExView.TopCount;}
			set
			{				
				this.WebbChartExView.TopCount = value;
				if(DesignMode)
				{
					this._MainView.UpdateView();
				}			
			}
		}
		public int BoundSpace         //Added this code at 2008-11-18 16:54:48@Simon
		{
			get{return this.WebbChartExView.BoundSpace;}
			set
			{				
				this.WebbChartExView.BoundSpace = value;
				if(DesignMode)
				{
					this._MainView.UpdateView();
				}			
			}
		}	
		[Browsable(false)]
		public Color ColorWhenMax
		{
			get{return this.WebbChartExView.ColorWhenMax;}
			set
			{
				this.WebbChartExView.ColorWhenMax=value;			
			}
		}

        [Browsable(false)]
        public float MaxValuesWhenTop
		{
			get{return this.WebbChartExView.MaxValuesWhenTop;}
			set
			{
				this.WebbChartExView.MaxValuesWhenTop=value;			
			}
		}

		[Browsable(false)]
        public bool AutoFitSize         //Added this code at 2008-11-18 16:54:48@Simon
		{
			get{return this.WebbChartExView.AutoFitSize;}
			set{this.WebbChartExView.AutoFitSize = value;
				if(DesignMode)
				{
					this._MainView.UpdateView();
				}			
			   }
		}
		[Browsable(false)]
		public bool Relative         //Added this code at 2008-11-18 16:54:48@Simon
		{
			get{return this.WebbChartExView.Relative;}
			set
			{
				this.WebbChartExView.Relative = value;
				if(DesignMode)
				{
					this._MainView.UpdateView();
				}			
			}
		}
		protected override void OnPaint(PaintEventArgs e)
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

		public override void AutoAdjustSize()
		{
			//base.AutoAdjustSize ();
			this.XtraContainer.Width = 500;
			this.XtraContainer.Height = 400;
		}
		public ArrayList GetMaxValues(SectionFilter OneValueScFilter,SectionFilterCollection repeatFilters)
		{			
			DataTable i_Table=this.GetDataSource();

			bool allpercent=true;

			ArrayList maxNames=new ArrayList();

			if(OneValueScFilter==null)return maxNames;
			
			foreach(Data.Series series in this.WebbChartExView.Settings.SeriesCollection)
			{
				if(!series.SeriesLabel.Percent)
				{
					allpercent=false;

					break;
				}
			}

			float MaxDataPoint=0f;

			float[] DataValues=new float[repeatFilters.Count];			

			for(int i=0;i<repeatFilters.Count;i++)
			{
				SectionFilter scfilter=repeatFilters[i];
 
				Webb.Collections.Int32Collection InnerRows=new Webb.Collections.Int32Collection();

				Webb.Collections.Int32Collection OutterRows=this.DenominatorFilter.GetFilteredRows(i_Table);

				OutterRows.CopyTo(InnerRows);

				InnerRows =OneValueScFilter.Filter.GetFilteredRows(i_Table,InnerRows);	

				InnerRows =scfilter.Filter.GetFilteredRows(i_Table,InnerRows);	 //Added this code at 2008-12-26 12:22:40@Simon

				float datapoint=0f;

				if(allpercent)
				{
					if(OutterRows.Count>0)datapoint=InnerRows.Count/(float)OutterRows.Count;                  
				}
				else
				{
					datapoint=InnerRows.Count;
					
				}
				if(MaxDataPoint<datapoint)MaxDataPoint=datapoint;

				DataValues[i]=datapoint;
			}
			for(int i=0;i<DataValues.Length;i++)
			{				
				if(DataValues[i]==MaxDataPoint)
				{
					maxNames.Add(repeatFilters[i].FilterName);
				}
			}

			return maxNames;
		}
		

		public float GetMaxDatapoint()
		{
			float maxDataPoint=0f;

			this.CalculateResult();

            maxDataPoint=this.WebbChartExView.GetMaxDatapoint();

			return maxDataPoint;

		}     
	}
}
