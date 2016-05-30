using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Collections;
using System.Reflection;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using  System.Drawing.Text;

using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Container;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraPrinting;
using DevExpress.Utils.Design;
using DevExpress.Utils;

using Webb.Data;
using Webb.Collections;
 

namespace Webb.Reports.ExControls.Data
{
	//old
	 #region public class ChartSeries
	[Serializable]
	public class ChartSeries
	{
		//Ctor
		public ChartSeries()
		{
			this._StringFormat = new StringFormat();
			this._TextColor = Color.BlueViolet;
			this._BorderColor = Color.Black;
			this._BackColor = Color.BlueViolet;
			this._BorderWidth = 1.0f;
			this._ChartUnits = new ArrayList();
			this._Filter = new DBFilter();
			this._Field = string.Empty;
			this._SeriesName = "New Series";
		}

		//Members
		[NonSerialized]
		protected StringFormat _StringFormat;
		protected Color _TextColor;
		protected Color _BackColor;
		protected Color _BorderColor;
		protected float _BorderWidth;
		protected DBFilter _Filter;
		protected ArrayList _ChartUnits;
		protected string _Field;
		protected string _SeriesName;

		//Properties
		[Browsable(false)]
		public StringFormat StringFormat
		{
			get
			{
				if(this._StringFormat == null) this._StringFormat = new StringFormat();

				return this._StringFormat;
			}
			set{this._StringFormat = value;}
		}

		public Color TextColor
		{
			get{return this._TextColor;}
			set{this._TextColor = value;}
		}

		public Color BackColor
		{
			get{return this._BackColor;}
			set{this._BackColor = value;}
		}

		public Color BorderColor
		{
			get{return this._BorderColor;}
			set{this._BorderColor = value;}
		}

		public float BorderWidth
		{
			get{return this._BorderWidth;}
			set{this._BorderWidth = value;}
		}

		[EditorAttribute(typeof(Webb.Reports.Editors.DBFilterEditor), typeof(System.Drawing.Design.UITypeEditor))]	//06-17-2008@Scott
		public DBFilter Filter
		{
			get{return this._Filter;}
			set{this._Filter = value.Copy();}
		}

		public ArrayList ChartUnits
		{
			get{return this._ChartUnits;}
		}

		public string SeriesName
		{
			get{return this._SeriesName;}
			set{this._SeriesName = value;}
		}

		[Browsable(true),Category("Collection Data")]
		[TypeConverter(typeof(PublicDBFieldConverter))]
		public string Field
		{
			get{return this._Field;}
			set{this._Field = value;}
		}

		//fuction
		public ChartSeries Copy()
		{
			ChartSeries newChartSeries = new ChartSeries();
			
			newChartSeries.SeriesName = _SeriesName;

			newChartSeries.BackColor = _BackColor;

			newChartSeries.BorderColor = _BorderColor;

			newChartSeries.BorderWidth = _BorderWidth;

			newChartSeries._ChartUnits = new ArrayList(this._ChartUnits);

			newChartSeries.BorderColor = _BorderColor;

			newChartSeries.Field = _Field;

			_Filter=newChartSeries.Filter.Copy();

			newChartSeries.StringFormat = new StringFormat(StringFormat);

			return newChartSeries;
		}

		public void Apply(ChartSeries chartSeries)
		{
			this._BackColor = chartSeries.BackColor;

			this._BorderColor = chartSeries.BorderColor;

			this._BorderWidth = chartSeries.BorderWidth;

			this._ChartUnits = new ArrayList(chartSeries.ChartUnits);

			this._BorderColor = chartSeries.BorderColor;

			this._Field = chartSeries.Field;

			this._Filter = chartSeries.Filter.Copy();

			this._StringFormat = new StringFormat(chartSeries.StringFormat);
		}

		public GroupSummary CreateSummary(SummaryTypes type)
		{
			GroupSummary summary = new GroupSummary();

			summary.Field = this._Field;

			summary.Filter = this._Filter.Copy();

			summary.SummaryType = type;

			summary.ColumnHeading = this.SeriesName;

			return summary; 
		}

		public override string ToString()
		{
			//return base.ToString ();
			return this._SeriesName;
		}

	}
	#endregion

	 #region public class ChartSeriesCollection : CollectionBase
	[Serializable]
	public class ChartSeriesCollection : CollectionBase
	{
		public ChartSeries this[int i_Index]
		{ 
			get { return this.InnerList[i_Index] as ChartSeries; } 
			set { this.InnerList[i_Index] = value; } 
		} 
		
		public ChartSeriesCollection(){} 
		
		public int Add(ChartSeries i_Object)
		{ 
			return this.InnerList.Add(i_Object);
		}

		public void Remove(ChartSeries i_Obj)
		{
			this.InnerList.Remove(i_Obj);
		}

		public ChartSeriesCollection Copy()
		{
			ChartSeriesCollection newCollection = new ChartSeriesCollection();

			foreach(ChartSeries chartSeries in this)
			{
				newCollection.Add(chartSeries);
			}
			return newCollection;
		}

		public GroupSummaryCollection CreateSummaies(SummaryTypes type)
		{
			GroupSummaryCollection collection = new GroupSummaryCollection();

			foreach(ChartSeries chartSeries in this)
			{
				collection.Add(chartSeries.CreateSummary(type));
			}

			return collection;
		}
	}
	#endregion
}
