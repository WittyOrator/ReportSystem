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
	#region Data Structure For Chart
	#region Enums
	[Serializable]
	public enum ChartAppearanceType
	{
		Bar = 0,
		Pie,
		Point,
		Line,
		HorizonBar,	
		Pie3D,
		Bar3D,
		HorizonBar3D,

	}

	[Serializable]
	public enum ChartTextPosition
	{
		Inside = 0,
		Outside,
		Center
	}

	[Serializable]
	public enum GridLineStyle
	{
		None = 0,
		Major,
		All,
	}

	[Serializable]
	public enum AxisLabelStyle
	{
		None = 0,
		OneLine,
		Staggered,
		SeriesCompared,   //Added this code at 2008-12-16 16:36:28@Simon
	}

	[Serializable]
	public enum WebbChartSettingStep
	{
		Type = 0,
		Appearance,
		Series,
		SeriesLabels,
		Axes,
		Lengend,
	}

	[Serializable]
	public enum PointShowStyle	//08-04-2008@Simon
	{
		None=0,
		Point,
		Line,
		Bar,
		HorizonBar,
	}
	[Serializable]
	public enum LabelPosition
	{
		Down,
		Up,		
	}
				

	#endregion
	#region class 	ChartGroupInfo
	public class ChartGroupInfo      //Added this class at 2008-12-16 9:59:23@Simon
	{
		public int index;
		public string iName;
		public Int32Collection subIndexs;
		public int StartCol;
		public int EndCol;
		public ChartGroupInfo(int i,string name)
		{
			index=i;
			iName=name;
			subIndexs=new Int32Collection();
		}			

	}
		  	
	#endregion
	#endregion

	#region Setting Units
	//new
	#region public interface IChartCell
	public interface IChartCell
	{
		float DataPoint{get; set;}
		string Name{get; set;}
		string PercentText{get; set;}
		Font  Font{get; set;}
		StringFormatFlags FormatFlags{get; set;}
		Color ForeColor{get; set;}
		Color BackColor{get; set;}
		Color BorderColor{get; set;}
		object Tag{get;set;}
		Int32Collection RowIndicators{get;set;}
		IChartCell Copy();
        float Frequence{ get; set; }
	}
	#endregion

	#region public class ChartCell : IChartCell, ISerializable
	[Serializable]
	public class ChartCell : IChartCell, ISerializable
	{
		#region ISerializable Members
		[SecurityPermission(SecurityAction.Demand,SerializationFormatter=true)]
		virtual public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			// TODO:  Add ExControlView.GetObjectData implementation
			info.AddValue("DataPoint",this.DataPoint);
            info.AddValue("Frequence", this.Frequence);
			info.AddValue("Name",this.Name);
            info.AddValue("PercentText", this.PercentText);
			info.AddValue("Font",this.Font,typeof(Font));
			info.AddValue("FormatFlags",this.FormatFlags,typeof(StringFormatFlags));
			info.AddValue("ForeColor",this.ForeColor,typeof(Color));
			info.AddValue("BackColor",this.BackColor,typeof(Color));
			info.AddValue("BorderColor",this.BorderColor,typeof(Color));
			info.AddValue("RowIndicators",this.RowIndicators,typeof(Int32Collection));  //Added this code at 2008-11-11 8:49:04@Simon
			info.AddValue("Tag",this.Tag);  //Added this code at 2008-11-11 8:49:04@Simon
		}

		public ChartCell(SerializationInfo info, StreamingContext context)
		{
			// TODO:  Add ExControlView.GetObjectData implementation
			this.DataPoint = info.GetSingle("DataPoint");
            this.Frequence = info.GetSingle("Frequence");
			this.Name = info.GetString("Name");
            this.PercentText = info.GetString("PercentText");
			this.Font = info.GetValue("Font",typeof(Font)) as Font;
			this.formatFlags = (StringFormatFlags)info.GetValue("FormatFlags",typeof(StringFormatFlags));
			this.ForeColor = (Color)info.GetValue("ForeColor",typeof(Color));
			this.BackColor = (Color)info.GetValue("BackColor",typeof(Color));
			this.BorderColor = (Color)info.GetValue("BorderColor",typeof(Color));
			this.RowIndicators=info.GetValue("RowIndicators",typeof(Int32Collection)) as Int32Collection;  //Added this code at 2008-11-11 8:50:16@Simon
			this.Tag=info.GetValue("Tag",typeof(object));
		}
		#endregion

		//Add this code at 2008-11-11 8:40:08@Simon
		protected Int32Collection rowIndicators;
		public Int32Collection RowIndicators
		{
			get
			{  
				if(this.rowIndicators==null)this.rowIndicators=new Int32Collection();

				return this.rowIndicators;
			}
			set{this.rowIndicators = value;}
		}
		//End Add

		protected float dataPoint=0f;
		public float DataPoint
		{
			get{return this.dataPoint;}
			set{this.dataPoint = value;}
		}

        protected float frequence = 0f;
        public float Frequence
        {
            get { return this.frequence; }
            set { this.frequence = value; }
        }

		protected string name;
		public string Name
		{
			get{return this.name;}
			set{this.name = value;}
		}

        protected string percentText=string.Empty;
        public string PercentText
		{
			get
			{
                if (this.percentText == string.Empty) return "0";
                return this.percentText;
			}
			set{this.percentText = value;}
		}

		protected Font font;
		public Font Font
		{
			get{return this.font;}
			set{this.font = value;}
		}

		protected StringFormatFlags formatFlags;
		public StringFormatFlags FormatFlags
		{
			get{return this.formatFlags;}
			set{this.formatFlags = value;}
		}

		protected Color foreColor;
		public Color ForeColor
		{
			get{return this.foreColor;}
			set{this.foreColor = value;}
		}

		protected Color backColor;
		public Color BackColor
		{
			get{return this.backColor;}
			set{this.backColor = value;}
		}

		protected Color borderColor;
		public Color BorderColor
		{
			get{return this.borderColor;}
			set{this.borderColor = value;}
		}
		protected object tag;
		public object Tag
		{
			get{return this.tag;}
			set{this.tag=value;}
		}

		public ChartCell()
		{
			this.dataPoint = 0.0f;
			this.name = string.Empty;
            this.percentText = string.Empty;
			this.font = Webb.Utility.GlobalFont;
			this.formatFlags = 0;
			this.foreColor = Color.Black;
			this.backColor = Color.Transparent;
			this.borderColor = Color.Black;
			this.rowIndicators=new Int32Collection(); //Added this code at 2008-11-11 8:46:04@Simon
            frequence = 0f;
		}


		public IChartCell Copy()
		{
			ChartCell dumyCell = new ChartCell();
			dumyCell.dataPoint = this.dataPoint;
			dumyCell.name = this.name;
            dumyCell.percentText = this.percentText;
			dumyCell.font = this.font;
			dumyCell.formatFlags = this.formatFlags;
			dumyCell.foreColor = this.foreColor;
			dumyCell.backColor = this.backColor;
			dumyCell.borderColor = this.borderColor;
			this.rowIndicators.CopyTo(dumyCell.rowIndicators);   //Added this code at 2008-11-11 8:47:29@Simon
            dumyCell.frequence = this.frequence;
			return dumyCell;
		}
	}
	#endregion

	#region public class Lengend : ISerializable
	[Serializable]
	public class Lengend : ISerializable
	{
		#region ISerializable Members
		[SecurityPermission(SecurityAction.Demand,SerializationFormatter=true)]
		virtual public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			// TODO:  Add ExControlView.GetObjectData implementation
			info.AddValue("Visible",this.Visible);
			info.AddValue("VisibleMarker",this.VisibleMarker);
			info.AddValue("SizeMarker",this.SizeMarker,typeof(Size));
			info.AddValue("SizeSpacing",this.SizeSpacing,typeof(Size));
			info.AddValue("textFormat",this.textFormat);
		}

		public Lengend(SerializationInfo info, StreamingContext context)
		{
			// TODO:  Add ExControlView.GetObjectData implementation
			try{this.Visible = info.GetBoolean("Visible");}
			catch{this.Visible = true;}

			try{this.VisibleMarker = info.GetBoolean("VisibleMarker");}
			catch{this.VisibleMarker = true;}

			try{this.SizeMarker = (Size)info.GetValue("SizeMarker",typeof(Size));}
			catch{this.SizeMarker = new Size(20,16);}

			try{this.SizeSpacing = (Size)info.GetValue("SizeSpacing",typeof(Size));}
			catch{this.SizeSpacing = new Size(2,2);}

			try{this.textFormat =info.GetString("textFormat");}
			catch{this.textFormat =string.Empty;}
		}
		#endregion

		protected bool visible = true;
		protected bool visibleMarker = true;
		protected Size sizeMarker = new Size(20,16);
		protected Size sizeSpacing = new Size(2,2);
		protected string textFormat=string.Empty;

		public Lengend(){}

		public Lengend Copy()
		{
			Lengend dumyLengend = new Lengend();

			dumyLengend.Visible = this.Visible;

			dumyLengend.VisibleMarker = this.VisibleMarker;

			dumyLengend.SizeMarker = this.SizeMarker;

			dumyLengend.SizeSpacing = this.SizeSpacing;

			dumyLengend.textFormat=this.textFormat;

			return dumyLengend;
		}

		[Browsable(false)]
		public static StringFormat StringFormat
		{
			get
			{
				StringFormat sf = new StringFormat(StringFormatFlags.NoWrap);

				sf.LineAlignment = StringAlignment.Center;

				sf.Alignment = StringAlignment.Center;

				return sf;
			}
		}

		[Category("Main")]
		public bool Visible
		{
			get{return this.visible;}
			set{this.visible = value;}
		}
		[EditorAttribute(typeof(Webb.Reports.Editors.TextFormatEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[Category("Format")]
		public string PieTextFormat
		{
			get
			{				
				return this.textFormat;
			}
			set{this.textFormat = value;}
		}

		[Category("Marker")]
		public bool VisibleMarker
		{
			get{return this.visibleMarker;}
			set{this.visibleMarker = value;}
		}

		[Category("Marker")]
		public Size SizeMarker
		{
			get{return this.sizeMarker;}
			set{this.sizeMarker = value;}
		}

		[Category("Spacing")]
		public Size SizeSpacing
		{
			get{return this.sizeSpacing;}
			set{this.sizeSpacing = value;}
		}
	}
	#endregion

	#region public class Axis : ISerializable
	[Serializable]
	public class Axis : ISerializable
	{
		#region ISerializable Members
		[SecurityPermission(SecurityAction.Demand,SerializationFormatter=true)]
		virtual public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			// TODO:  Add ExControlView.GetObjectData implementation
			info.AddValue("Visible",this.Visible);

			info.AddValue("Rotated",this.Rotated);

			info.AddValue("Title",this.Title);

			info.AddValue("TitleAlignment",this.TitleAlignment,typeof(StringAlignment));

			info.AddValue("MinorCount",this.MinorCount);

			info.AddValue("Gridlines",this.Gridlines,typeof(GridLineStyle));

			info.AddValue("Tickmarks",this.Tickmarks,typeof(GridLineStyle));

			info.AddValue("Interlaced",this.Interlaced);

			info.AddValue("LabelStyle",this.LabelStyle,typeof(AxisLabelStyle));

			info.AddValue("LabelAngle",this.LabelAngle);

			info.AddValue("GridLineSpace",this.GridLineSpace);  //Added this code at 2008-12-18 9:44:44@Simon

			info.AddValue("Font",this.Font,typeof(Font));  //Added this code at 2008-12-18 9:44:44@Simon

			info.AddValue("TitleFont",this.TitleFont,typeof(Font));  //Added this code at 2008-12-18 9:44:44@Simon

			info.AddValue("TitleColor",this.TitleColor,typeof(Color));  //Added this code at 2008-12-18 9:44:44@Simon

			info.AddValue("FontQuality",this.FontQuality,typeof(TextRenderingHint));  //Added this code at 2008-12-25 13:50:54@Simon

			info.AddValue("MajorBorderColor",this.MajorBorderColor,typeof(Color));  //Added this code at 2008-12-18 9:44:44@Simon
		}

		public Axis(SerializationInfo info, StreamingContext context)
		{
			// TODO:  Add ExControlView.GetObjectData implementation
			try{this.Visible = info.GetBoolean("Visible");}
			catch{this.Visible = true;}

			try{this.Rotated = info.GetBoolean("Rotated");}
			catch{this.Rotated = false;}

			try{this.Title = info.GetString("Title");}
			catch{this.Title = string.Empty;}

			try{this.TitleAlignment = (StringAlignment)info.GetValue("TitleAlignment",typeof(StringAlignment));}
			catch{this.TitleAlignment = StringAlignment.Center;}
					
			try{this.MinorCount = info.GetInt32("MinorCount");}
			catch{this.MinorCount = 3;}

			try{this.Gridlines = (GridLineStyle)info.GetValue("Gridlines",typeof(GridLineStyle));}
			catch{this.Gridlines = GridLineStyle.Major;}

			try{this.Tickmarks = (GridLineStyle)info.GetValue("Tickmarks",typeof(GridLineStyle));}
			catch{this.Tickmarks = GridLineStyle.Major;}

			try{this.Interlaced = info.GetBoolean("Interlaced");}
			catch{this.Interlaced = false;}

			try{this.LabelStyle = (AxisLabelStyle)info.GetValue("LabelStyle",typeof(AxisLabelStyle));}
			catch{this.LabelStyle = AxisLabelStyle.OneLine;}

			try{this.LabelAngle = info.GetInt32("LabelAngle");}
			catch{this.LabelAngle = 0;}

			try{this.GridLineSpace=info.GetSingle("GridLineSpace");}
			catch{this.GridLineSpace=0;}

			try{this.Font=info.GetValue("Font",typeof(Font)) as Font;}
			catch{font= new Font(AppearanceObject.DefaultFont.FontFamily, 8f);}			
				
			try{this.TitleFont=info.GetValue("TitleFont",typeof(Font)) as Font;}
			catch{this.TitleFont=new Font(AppearanceObject.DefaultFont.FontFamily, 15f,FontStyle.Bold);}

			try{this.FontQuality=(TextRenderingHint)info.GetValue("FontQuality",typeof(TextRenderingHint));}
			catch{this.FontQuality=TextRenderingHint.SystemDefault;}

			try{this.TitleColor=(Color)info.GetValue("TitleColor",typeof(Color));}
			catch{this.TitleColor=Color.Black;}

			try{this.MajorBorderColor=(Color)info.GetValue("MajorBorderColor",typeof(Color));}
			catch{this.MajorBorderColor=Color.Empty;}
		}
		#endregion

		public Axis()
		{
			visible = true;
			rotated = false;
			title = string.Empty;
			titleAlignment = StringAlignment.Center;
			minorCount = 3;
			gridlines = GridLineStyle.Major;
			tickmarks = GridLineStyle.Major;
			interlaced = false;
			labelStyle = AxisLabelStyle.OneLine;
			labelAngle = 0;			
			font= new Font(AppearanceObject.DefaultFont.FontFamily, 8f);
			titleFont=new Font(AppearanceObject.DefaultFont.FontFamily, 15f,FontStyle.Bold);	

			gridLineSpace=0;
			fontQuality=TextRenderingHint.AntiAliasGridFit;

			titleColor=Color.Black;
            majorBorderColor=Color.Empty;
		}

		public Axis Copy()
		{
			Axis dumyAxis = new Axis();

			dumyAxis.visible = this.visible;
			dumyAxis.rotated = this.rotated;
			dumyAxis.title = this.title;
			dumyAxis.titleAlignment = this.titleAlignment;
			dumyAxis.minorCount = this.minorCount;
			dumyAxis.gridlines = this.gridlines;
			dumyAxis.tickmarks = this.tickmarks;
			dumyAxis.interlaced = this.interlaced;
			dumyAxis.labelStyle = this.labelStyle;
			dumyAxis.labelAngle = this.labelAngle;
			dumyAxis.gridLineSpace=this.gridLineSpace;  //Added this code at 2008-12-18 9:40:41@Simon
			dumyAxis.font=this.font;      //Added this code at 2008-12-18 12:48:10@Simon
			dumyAxis.titleFont=this.titleFont;

			dumyAxis.fontQuality=this.fontQuality;  //Added this code at 2008-12-25 13:45:43@Simon

			dumyAxis.titleColor=this.titleColor;

			dumyAxis.majorBorderColor=this.majorBorderColor;


			return dumyAxis;
		}

		protected bool visible = true;
		protected bool rotated = false;
		protected string title = string.Empty;
		protected StringAlignment titleAlignment = StringAlignment.Center;
		protected int minorCount = 3;
		protected GridLineStyle gridlines = GridLineStyle.Major;
		protected GridLineStyle tickmarks = GridLineStyle.Major;
		protected bool interlaced = false;
		protected AxisLabelStyle labelStyle = AxisLabelStyle.OneLine;
		protected int labelAngle = 0;
		//		protected Font font=Webb.Utility.GlobalFont;
		//		protected Font titleFont=new Font(Webb.Utility.GlobalFont.FontFamily,Webb.Utility.GlobalFont.Size+4,FontStyle.Bold);
		protected Font font= new Font(AppearanceObject.DefaultFont.FontFamily,8f);
		protected Font titleFont=new Font(AppearanceObject.DefaultFont.FontFamily, 15f,FontStyle.Bold);	

		protected Color titleColor=Color.Black;

		protected float gridLineSpace=0;

		protected TextRenderingHint fontQuality=TextRenderingHint.AntiAliasGridFit;

		protected Color majorBorderColor=Color.Black;

		[Category("TextQuality")]
		[Description("Define  AxisLabel's Showing Mode")]
		public TextRenderingHint FontQuality
		{
			get{return this.fontQuality;}
			set{this.fontQuality=value;}
		}

		[Category("Main")]
		[Description("Define Whether AxisLabel would show")]
		public bool Visible
		{
			get{return this.visible;}
			set{this.visible = value;}
		}
		
		[Category("Main")]
		[Description("Define BorderColor")]
		public Color MajorBorderColor
		{
			get{return this.majorBorderColor;}
			set{this.majorBorderColor = value;}
		}

		[Category("Main"),Browsable(false)]
		public bool Rotated
		{
			get{return this.rotated;}
			set{this.rotated = value;}
		}

		[Category("Title")]
		[Description("Axis's title")]
		public string Title
		{
			get{return this.title;}
			set{this.title = value;}
		}

		[Category("Title")]
		[Description("Axis's TitleColor")]
		public Color TitleColor
		{
			get{return this.titleColor;}
			set{this.titleColor = value;}
		}
		[Category("Title")]
		[Description("Title Font of Axis")]
		public Font TitleFont    //Added this code at 2008-12-18 12:46:05@Simon
		{
			get{return this.titleFont;}
			set{this.titleFont = value;}
		}

		[Category("Title")]
		[Description("Axis's Title Alignment")]
		public StringAlignment TitleAlignment
		{
			get{return this.titleAlignment;}
			set{this.titleAlignment = value;}
		}


		[Category("Gridlines")]
		[Description("Define MinorTicks and Gridlines of Axis")]
		public int MinorCount
		{
			get{return this.minorCount;}
			set{this.minorCount = value;}
		}

		[Category("Gridlines")]
		[Description("Define Gridlines of Axis")]
		public GridLineStyle Gridlines
		{
			get{return this.gridlines;}
			set{this.gridlines = value;}
		}
		[Category("Gridlines")]
		[Description("Define space betwwen two gridlines of Axis")]
		public float GridLineSpace
		{
			get{return this.gridLineSpace;}
			set{this.gridLineSpace = value;}
		}

		[Category("Gridlines")]
		[Description("Define Tickmarks of Axis")]
		public GridLineStyle Tickmarks
		{
			get{return this.tickmarks;}
			set{this.tickmarks = value;}
		}

		[Category("Gridlines")]
		public bool Interlaced
		{
			get{return this.interlaced;}
			set{this.interlaced = value;}
		}

		[Category("Labels")]
		public AxisLabelStyle LabelStyle
		{
			get{return this.labelStyle;}
			set{this.labelStyle = value;}
		}

		[Category("Labels")]
		public int LabelAngle
		{
			get{return this.labelAngle;}
			set{this.labelAngle = value;}
		}

		[Category("Labels")]
		public Font Font
		{
			get{return this.font;}
			set{this.font = value;}
		}

	}
	#endregion

	#region public class Series : ISerializable
	[Serializable]
	public class Series : ISerializable
	{
		#region ISerializable Members
		[SecurityPermission(SecurityAction.Demand,SerializationFormatter=true)]
		virtual public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			// TODO:  Add ExControlView.GetObjectData implementation
			info.AddValue("Name",this.Name);
			info.AddValue("FieldArgument",this.FieldArgument);
			info.AddValue("FieldValue",this.FieldValue);
			info.AddValue("Filter",this.Filter,typeof(DBFilter));
			info.AddValue("SeriesLabel",this.SeriesLabel,typeof(SeriesLabel));

			info.AddValue("SectionFilters",this.SectionFilters,typeof(SectionFilterCollection));  //Added this code at 2008-12-15 13:36:13@Simon
			info.AddValue("SectionFiltersWrapper",this.SectionFiltersWrapper,typeof(SectionFilterCollectionWrapper));  //Added this code at 2008-12-15 13:36:13@Simon
			
			info.AddValue("Color",this.Color,typeof(Color));   

			info.AddValue("BorderColor",this.BorderColor,typeof(Color));   //2009-8-17 14:19:17@Simon Add this Code

			info.AddValue("FieldShowName",this.FieldShowName);  //Added this code at 2008-12-18 15:08:50@Simon
			info.AddValue("MainOrder",this.MainOrder);   //Added this code at 2008-12-18 15:44:23@Simon

			info.AddValue("IsRoot",this.IsRoot);  

			info.AddValue("Width",this.Width);   //Added this code at 2008-12-23 11:26:28@Simon

			info.AddValue("valuesColor",this.valuesColor,typeof(ValueColorCollection));   //Added this code at 2008-12-23 11:26:28@Simon
			info.AddValue("reFilter",this.reFilter,typeof(ReFilter));   //Added this code at 2008-12-23 11:26:28@Simon

		}

		public Series(SerializationInfo info, StreamingContext context)
		{
			// TODO:  Add ExControlView.GetObjectData implementation
			try{this.Name = info.GetString("Name");}
			catch{this.Name = string.Empty;}

			try{this.FieldArgument = info.GetString("FieldArgument");}
			catch{this.FieldArgument = string.Empty;}

			try{this.FieldValue = info.GetString("FieldValue");}
			catch{this.FieldValue = string.Empty;}

			try{this.Filter = info.GetValue("Filter",typeof(DBFilter)) as DBFilter;

				 this.Filter=AdvFilterConvertor.GetAdvFilter(DataProvider.VideoPlayBackManager.AdvReportFilters,this.Filter);    //2009-4-29 11:37:37@Simon Add UpdateAdvFilter
			
			   }
			catch{this.Filter = new DBFilter();}
				
			try{this.SeriesLabel = info.GetValue("SeriesLabel",typeof(SeriesLabel)) as SeriesLabel;}
			catch{this.SeriesLabel = new SeriesLabel();}

			try{this.SectionFilters = info.GetValue("SectionFilters",typeof(SectionFilterCollection)) as SectionFilterCollection;			
		    	}  //Added this code at 2008-12-15 13:35:07@Simon
			catch{this.SectionFilters = new SectionFilterCollection();}

			try
			{
			   this.SectionFiltersWrapper= info.GetValue("SectionFiltersWrapper",typeof(SectionFilterCollectionWrapper)) as SectionFilterCollectionWrapper;
			}
			catch
			{
			}
	             
			try{this.Color =(Color)info.GetValue("Color",typeof(Color));}
			catch{this.Color =Color.Empty;}
	            
			try{this.FieldShowName = info.GetBoolean("FieldShowName");}
			catch{this.FieldShowName =false;}

			try{this.MainOrder=info.GetBoolean("MainOrder");}
			catch{this.MainOrder=false;}

			try{this.IsRoot=info.GetBoolean("IsRoot");}
			catch{this.IsRoot=false;}

			try{this.Width=info.GetInt32("Width");}
			catch{this.Width=0;}

			try{this.valuesColor = info.GetValue("valuesColor",typeof(ValueColorCollection)) as ValueColorCollection;}  //Added this code at 2008-12-15 13:35:07@Simon
			catch{this.valuesColor = new ValueColorCollection();}

			try{
				this.reFilter = info.GetValue("reFilter",typeof(ReFilter)) as ReFilter;
			   }  //Added this code at 2008-12-15 13:35:07@Simon
			catch{this.reFilter = new ReFilter();}

			try{this.BorderColor =(Color)info.GetValue("BorderColor",typeof(Color));}
			catch{this.BorderColor =Color.Empty;} 
		}
		#endregion

		public Series()
		{
			if(this.seriesLabel != null) this.seriesLabel.ParentSeries = this;
			if(this.sectionFilters==null)this.sectionFilters=new SectionFilterCollection();  //Added this code at 2008-12-15 13:30:50@Simon
		}

		public Series Copy()
		{
			Series dumySeries = new Series();

			dumySeries.Name = this.Name;
			dumySeries.FieldArgument = this.FieldArgument;
			dumySeries.FieldValue = this.FieldValue;
			dumySeries.Filter = this.Filter.Copy();
			dumySeries.SeriesLabel = this.SeriesLabel.Copy();

			dumySeries.SectionFilters=this.SectionFilters.Copy();  //Added this code at 2008-12-15 13:30:50@Simon
			dumySeries.SectionFiltersWrapper.ReportScType=this.sectiongfilterWrapper.ReportScType;
            dumySeries.SectionFiltersWrapper.SectionFilters=this.sectiongfilterWrapper.SectionFilters.Copy();

			dumySeries.borderColor=this.borderColor;

			dumySeries.Color=this.Color;  //Added this code at 2008-12-18 13:02:05@Simon
			dumySeries.FieldShowName=this.FieldShowName;

			dumySeries.MainOrder=this.MainOrder;  //Added this code at 2008-12-18 15:40:10@Simon
			dumySeries.IsRoot=this.IsRoot;  //Added this code at 2008-12-22 9:43:44@Simon

			dumySeries.Width=this.Width; //Added this code at 2008-12-23 11:25:53@Simon

			dumySeries.valuesColor=this.valuesColor.Copy();
            dumySeries.reFilter=this.reFilter.Copy();

			return dumySeries;
		}

		protected string name = "New Series";//string.Empty;
		protected string fieldArgument = string.Empty;
		protected string fieldValue = string.Empty;
		protected DBFilter filter = new DBFilter();
		protected SeriesLabel seriesLabel = new SeriesLabel();		
		protected SectionFilterCollection sectionFilters=new SectionFilterCollection();
		protected SectionFilterCollectionWrapper sectiongfilterWrapper=new SectionFilterCollectionWrapper(); 

		protected Color color=Color.Empty;
		protected Color borderColor=Color.Empty;

		protected bool mainOrder=false;  //Added this code at 2008-12-18 15:40:59@Simon
		protected bool isRoot=false;  //Added this code at 2008-12-18 15:40:59@Simon

		protected ReFilter reFilter=new ReFilter();

		protected ValueColorCollection valuesColor=new ValueColorCollection();

		[Category("Group")]
		public bool MainOrder
		{
			get{return this.mainOrder;}
			set
			{
				if(this.isRoot)
				{
					this.mainOrder=false;
				}
				else
				{
					this.mainOrder=value;
				}
			}
		}
		[Category("Group")]
		public bool IsRoot
		{
			get{return this.isRoot;}
			set
			{
				if(this.mainOrder)
				{
					this.isRoot=false;
				}
				else
				{
					this.isRoot=value;
				}
			}
		}

		protected bool fieldShowName=false;   //Added this code at 2008-12-18 15:03:04@Simon

		[Category("SeriesName")]
		public bool FieldShowName   //Added this code at 2008-12-18 15:04:18@Simon
		{
			get{return this.fieldShowName;}
			set{this.fieldShowName=value;}
		}
		[Category("Style")]
		public Color Color
		{
			get{return this.color;}
			set{this.color=value;}
		}

		[Category("Style")]
		public Color BorderColor
		{
			get{return this.borderColor;}
			set{this.borderColor=value;}
		}

		protected int _Width=0;
		[Category("Style")]
		public int Width
		{
			get{return this._Width;}
			set
			{
				if(value>=0)this._Width=value;
			}
		}
		[EditorAttribute(typeof(Webb.Reports.ExControls.Editors.ValuesColorEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[Category("Style")]	
		public ValueColorCollection ValuesStyle
		{
			get
			{
				if(this.valuesColor==null)valuesColor=new ValueColorCollection();

				valuesColor.series=this; 
              
				return this.valuesColor;
            }
			set
			{
				this.valuesColor=value;
			}
		}

		[EditorAttribute(typeof(Webb.Reports.ExControls.Editors.ReFilterEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[Category("Style")]	
		public ReFilter MinValueFilter
		{
			get
			{
				if(this.reFilter==null)reFilter=new ReFilter();					
				return this.reFilter;
			}
			set
			{
				this.reFilter=value;
			}
		}

		[Category("SeriesName")]
		public string Name
		{
			get{return this.name;}
			set
			{
				this.name = value;

				this.SeriesLabel.Name = value;
			}
		}

		[Category("Data")]
		[TypeConverter(typeof(Webb.Data.PublicDBFieldConverter))]
		public string FieldArgument
		{
			get{
				if(fieldArgument==null)fieldArgument=string.Empty;
				 return this.fieldArgument;
			    }			
			set{this.fieldArgument = value;}
		}

		[Browsable(false)]	//12-16-2008@Scott
		[Category("Data")]
		[TypeConverter(typeof(Webb.Data.PublicDBFieldConverter))]
		public string FieldValue
		{
			get{return this.fieldValue;}
			set{this.fieldValue = value;}
		}

		[EditorAttribute(typeof(Webb.Reports.Editors.DBFilterEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[Category("Data")]
		public DBFilter Filter
		{
			get
			{
				if(this.filter == null) this.filter = new DBFilter();

				return this.filter;
			}
			set{this.filter = value;}
		}

		[Browsable(false)]
		public SeriesLabel SeriesLabel
		{
			get
			{
				if(this.seriesLabel == null)
				{
					this.seriesLabel = new SeriesLabel();
					
					this.seriesLabel.ParentSeries = this;
				}

				return this.seriesLabel;
			}
			set
			{
				this.seriesLabel = value;

				if(this.seriesLabel != null) this.seriesLabel.ParentSeries = this;
			}
		}

		[EditorAttribute(typeof(Webb.Reports.Editors.SectionFiltersEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[Category("Data")]
		[Browsable(false)]
		public SectionFilterCollection SectionFilters
		{
			get
			{
				if(this.sectionFilters==null)this.sectionFilters=new SectionFilterCollection();
				return this.sectionFilters;
			}
			set
			{
				this.sectionFilters=value;
			}
		}
		[EditorAttribute(typeof(Webb.Reports.Editors.SectionFiltersEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[Category("Data")]
		[Browsable(true)]
		public SectionFilterCollectionWrapper SectionFiltersWrapper
		{
			get
			{
				if(this.sectiongfilterWrapper == null) this.sectiongfilterWrapper = new SectionFilterCollectionWrapper();
				this.sectiongfilterWrapper.SectionFilters = this.SectionFilters;
				return this.sectiongfilterWrapper;}
			set
			{
				this.sectiongfilterWrapper = value;

				sectiongfilterWrapper.ReportScType=AdvFilterConvertor.GetScType(sectiongfilterWrapper.ReportScType,DataProvider.VideoPlayBackManager.AdvSectionType);  //2009-6-16 10:18:47@Simon Add this Code
				
				this.sectiongfilterWrapper.UpdateSectionFilters();

				if(this.sectiongfilterWrapper.SectionFilters.Count > 0)	//Modified at 2009-2-6 10:51:42@Scott
				{
					this.SectionFilters = this.sectiongfilterWrapper.SectionFilters;
				}
			}
		}

		public override string ToString()
		{
			return this.Name;
		}

	}
	#endregion

	#region public class SeriesLabel : ISerializable
	[Serializable]
	public class SeriesLabel: ISerializable
	{
		#region ISerializable Members
		[SecurityPermission(SecurityAction.Demand,SerializationFormatter=true)]
		virtual public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			// TODO:  Add ExControlView.GetObjectData implementation
			info.AddValue("name",this.Name);
			info.AddValue("visible",this.Visible);
			info.AddValue("visibleConnector",this.VisibleConnector);
			info.AddValue("lengthConnector",this.LengthConnector);
			info.AddValue("position",this.Position,typeof(ChartTextPosition));
			info.AddValue("percent",this.Percent);
			info.AddValue("sortingType",this.SortingType,typeof(Webb.Data.SortingTypes));
			info.AddValue("sortingByTypes",this.SortingByTypes,typeof(SortingByTypes));

			info.AddValue("showZero",this.ShowZero);  //Added this code at 2008-12-16 11:25:33@Simon
			info.AddValue("font",this.Font,typeof(Font));
			info.AddValue("fontQuality",this.FontQuality,typeof(TextRenderingHint)); 
			info.AddValue("foreColor",this.foreColor,typeof(Color));
			info.AddValue("angle",this.Angle);
            
			info.AddValue("showRectange",this.showRectange);  //2009-4-17 9:01:51@Simon Add this Code
			info.AddValue("backColor",this.backColor,typeof(Color));
            info.AddValue("percentFormat", this.percentFormat); 

		}

		public SeriesLabel(SerializationInfo info, StreamingContext context)
		{
			// TODO:  Add ExControlView.GetObjectData implementation
			try{this.Name = info.GetString("name");}
			catch{this.Name = string.Empty;}

			try{this.Visible = info.GetBoolean("visible");}
			catch{this.Visible = true;}

			try{this.VisibleConnector = info.GetBoolean("visibleConnector");}
			catch{this.VisibleConnector = true;}

			try{this.LengthConnector = info.GetInt32("lengthConnector");}
			catch{this.LengthConnector = 10;}

			try{this.Position = (ChartTextPosition)info.GetValue("position",typeof(ChartTextPosition));}
			catch{this.Position = ChartTextPosition.Outside;}

			try{this.Percent = info.GetBoolean("percent");}
			catch{this.Percent = false;}

			try{this.SortingType =  (Webb.Data.SortingTypes)info.GetValue("sortingType",typeof(Webb.Data.SortingTypes));}
			catch{this.SortingType = Webb.Data.SortingTypes.None;}

			try{this.SortingByTypes =  (SortingByTypes)info.GetValue("sortingByTypes",typeof(SortingByTypes));}
			catch{this.SortingByTypes = SortingByTypes.Frequence;}

			try{this.ShowZero= info.GetBoolean("showZero");}   //Added this code at 2008-12-16 11:26:39@Simon
			catch{this.ShowZero = true;}

			try{this.Font=info.GetValue("font",typeof(Font)) as Font;}  //Added this code at 2008-12-18 12:58:13@Simon
			catch{this.Font=Webb.Utility.GlobalFont;}	
		
			try{this.FontQuality=(TextRenderingHint)info.GetValue("fontQuality",typeof(TextRenderingHint));}
			catch{this.FontQuality=TextRenderingHint.SystemDefault;}

			try{this.foreColor=(Color)info.GetValue("foreColor",typeof(Color));}
			catch{this.foreColor=Color.Empty;}

			try{this.Angle=info.GetSingle("angle");}  //Added this code at 2009-1-13 16:33:43@Simon
			catch{this.Angle=45f;}

			try{this.backColor=(Color)info.GetValue("backColor",typeof(Color));}
			catch{this.backColor=Color.Empty;}

			try{this.rectBorderColor=(Color)info.GetValue("rectBorderColor",typeof(Color));}
			catch{this.rectBorderColor=Color.Empty;}

			try{this.showRectange=info.GetBoolean("showRectange");}
			catch{this.showRectange=true;}

            try { this.percentFormat = info.GetString("percentFormat"); }
            catch { this.percentFormat = "%"; }
		}
		#endregion

		public SeriesLabel(){}

		public SeriesLabel Copy()
		{
			SeriesLabel dumySeriesLabel = new SeriesLabel();

			dumySeriesLabel.Name = this.Name;
			dumySeriesLabel.Visible = this.Visible;
			dumySeriesLabel.VisibleConnector = this.VisibleConnector;
			dumySeriesLabel.LengthConnector = this.LengthConnector;
			dumySeriesLabel.Position = this.Position;
			dumySeriesLabel.Percent = this.Percent;
			dumySeriesLabel.SortingType=this.SortingType;

			dumySeriesLabel.SortingByTypes=this.SortingByTypes;  //Added this code at 2008-12-19 9:52:05@Simon
			dumySeriesLabel.showZero=this.showZero;   //Added this code at 2008-12-16 11:24:07@Simon

			dumySeriesLabel.font=this.font;

			dumySeriesLabel.fontQuality=this.fontQuality;  //Added this code at 2008-12-25 13:45:43@Simon
			dumySeriesLabel.foreColor=this.foreColor;
			dumySeriesLabel.angle=this.angle;

			dumySeriesLabel.backColor=this.backColor;
			dumySeriesLabel.showRectange=this.showRectange;  //2009-4-17 9:03:02@Simon Add this Code

			dumySeriesLabel.rectBorderColor=this.rectBorderColor;  //2009-4-17 9:03:02@Simon Add this Code

            dumySeriesLabel.percentFormat = this.percentFormat;
			return dumySeriesLabel;
		}

		protected Series parentSeries;
		protected string name = string.Empty;
		protected bool visible = true;
		protected bool visibleConnector = true;
		protected int lengthConnector = 10;
		protected ChartTextPosition position = ChartTextPosition.Outside;
		protected bool percent = false;
		protected bool showZero=true;   //Added this code at 2008-12-16 11:24:12@Simon
		protected Font font=Webb.Utility.GlobalFont;	
		protected TextRenderingHint fontQuality=TextRenderingHint.SystemDefault;
		protected Color foreColor=Color.Empty;
		protected float angle=45f;
		protected Color backColor=Color.Empty;
		protected bool showRectange=true;
		protected Color rectBorderColor=Color.Empty;
        protected string percentFormat = "%";

		[Category("Style")]
		public Color RectBorderColor
		{
			get{return this.rectBorderColor;}
			set{this.rectBorderColor=value;}
		}
		[Category("Style")]
		public Color TextColor
		{
			get{return this.foreColor;}
			set{this.foreColor=value;}
		}
		[Category("Style")]
		public Color BackColor
		{
			get{return this.backColor;}
			set{this.backColor=value;}
		}
		[Category("Style")]
		public bool ShowBorders
		{
			get{return this.showRectange;}
			set{this.showRectange=value;}
		}
		[Category("Style")]
		public TextRenderingHint FontQuality
		{
			get{return this.fontQuality;}
			set{this.fontQuality=value;}
		}
		[Category("Style")]
		public Font Font    //Added this code at 2008-12-16 11:22:13@Simon
		{
			get{return this.font;}
			set{this.font=value;}
		}
		[Category("DataFormat")]
		public bool ShowZero    //Added this code at 2008-12-16 11:22:13@Simon
		{
			get{return this.showZero;}
			set{this.showZero=value;}
		}

		protected Webb.Data.SortingTypes sortingType=Webb.Data.SortingTypes.None;  //Added this code at 2008-12-10 16:58:31@Simon
		[Category("Sorting")]
		public Webb.Data.SortingTypes SortingType    //Added this code at 2008-12-10 17:00:00@Simon
		{
			get{return this.sortingType;}
			set{this.sortingType=value;}
		}

		protected SortingByTypes sortingByTypes=SortingByTypes.Frequence;  //Added this code at 2008-12-10 16:58:31@Simon
		[Category("Sorting")]
		public SortingByTypes SortingByTypes    //Added this code at 2008-12-10 17:00:00@Simon
		{
			get{return this.sortingByTypes;}
			set{this.sortingByTypes=value;}
		}

		[Browsable(false)]
		public Series ParentSeries
		{
			get{return this.parentSeries;}
			set{this.parentSeries = value;}
		}

		[Browsable(false)]
		public string Name
		{
			get{return this.name;}
			set{this.name = value;}
		}
		[Category("Style")]
		public bool Visible
		{
			get{return this.visible;}
			set{this.visible = value;}
		}
		[Category("Style")]
		public bool VisibleConnector
		{
			get{return this.visibleConnector;}
			set{this.visibleConnector = value;}
		}
		[Category("Style")]
		public int LengthConnector
		{
			get{return this.lengthConnector;}
			set{this.lengthConnector = value;}
		}
		[Category("Style")]
		public float Angle
		{
			get{return this.angle;}
			set{this.angle = value;}
		}
		[Category("Style")]
		public ChartTextPosition Position
		{
			get{return this.position;}
			set{this.position = value;}
		}
		[Category("DataFormat")]
		public bool Percent
		{
			get{return this.percent;}
			set{this.percent = value;}
		}
        [Category("DataFormat")]
        public string DisplayFormat
        {
            get
            {
                if (percentFormat == null) percentFormat ="%";
                return this.percentFormat; }
            set { this.percentFormat = value; }
        }


		public override string ToString()
		{
			if(this.ParentSeries != null)
			{
				return this.ParentSeries.Name;
			}
			else
			{
				return string.Empty;
			}
		}

	}
	#endregion

	#region public class SeriesCollection : CollectionBase
	[Serializable]
	public class SeriesCollection : CollectionBase
	{
		public SeriesCollection(){}
		
		public SeriesCollection Copy()
		{
			SeriesCollection arrSeries = new SeriesCollection();
				
			foreach(Series series in this)
			{
				arrSeries.Add(series.Copy());
			}

			return arrSeries;
		}
		
		public Series this[int i_index]
		{
			get
			{
				return this.InnerList[i_index] as Series;
			}
			set
			{
				this.InnerList[i_index] = value;
			}
		}

		public int Add(Series series)
		{
			return this.InnerList.Add(series);
		}

		public void Remove(Series delSeries)
		{
			this.InnerList.Remove(delSeries);
		}
		public void Swap(int a1,int a2)
		{
			if(a1<0||a2<0||a1>=this.Count||a2>=this.Count)
			{
				return;
			}
			Series series=this[a1];
			this.InnerList[a1]= this.InnerList[a2];
			this.InnerList[a2]=series;		
		}
			
	}
	#endregion

	#region public class PieLabelInfo:ISerializable
	[Serializable]
	public class PieLabelInfo:ISerializable
	{
		#region Auto Constructor By Macro 2009-6-8 10:01:14
		public PieLabelInfo()
		{
			show=false;
			text="[FieldName]";
			position=LabelPosition.Down;			
			fontQuality=TextRenderingHint.SystemDefault;
			foreColor=Color.Black;
			font=Webb.Utility.GlobalFont;		
		}
		#endregion

	
		protected bool show=false;
		protected string text="[FieldName]";
        protected LabelPosition position=LabelPosition.Down;
		protected Font font=Webb.Utility.GlobalFont;	
		protected TextRenderingHint fontQuality=TextRenderingHint.SystemDefault;
		protected Color foreColor=Color.Black;

		#region Copy Function By Macro 2009-6-8 9:52:12
		public PieLabelInfo Copy()
		{
			PieLabelInfo thiscopy=new PieLabelInfo();
			thiscopy.show=this.show;
			thiscopy.text=this.text;
			thiscopy.position=this.position;
			thiscopy.font=this.font;
			thiscopy.fontQuality=this.fontQuality;
			thiscopy.foreColor=this.foreColor;
			return thiscopy;
		}
		#endregion

        [Category("Visibility")]
		public bool Show
		{
			get{ return show; }
			set{ show = value; }
		}

		public string Text
		{
			get{ return text; }
			set{ text = value; }
		}

		public Webb.Reports.ExControls.Data.LabelPosition Position
		{
			get{ return position; }
			set{ position = value; }
		}

		public System.Drawing.Font Font
		{
			get{ return font; }
			set{ font = value; }
		}

		public System.Drawing.Text.TextRenderingHint FontQuality
		{
			get{ return fontQuality; }
			set{ fontQuality = value; }
		}

		public System.Drawing.Color ForeColor
		{
			get{ return foreColor; }
			set{ foreColor = value; }
		}

		#region Serialization By Simon's Macro 2009-6-8 9:52:32
		public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			info.AddValue("show",show);
			info.AddValue("text",text);
			info.AddValue("position",position,typeof(Webb.Reports.ExControls.Data.LabelPosition));
			info.AddValue("font",font,typeof(System.Drawing.Font));
			info.AddValue("fontQuality",fontQuality,typeof(System.Drawing.Text.TextRenderingHint));
			info.AddValue("foreColor",foreColor,typeof(System.Drawing.Color));
		
		}

		public PieLabelInfo(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			try
			{
				show=info.GetBoolean("show");
			}
			catch
			{
				show=false;
			}
			try
			{
				text=info.GetString("text");
			}
			catch
			{
				text="[FieldName]";
			}
			try
			{
				position=(Webb.Reports.ExControls.Data.LabelPosition)info.GetValue("position",typeof(Webb.Reports.ExControls.Data.LabelPosition));
			}
			catch
			{
				position=Webb.Reports.ExControls.Data.LabelPosition.Down;
			}
			try
			{
				font=(System.Drawing.Font)info.GetValue("font",typeof(System.Drawing.Font));
			}
			catch
			{
                font=Webb.Utility.GlobalFont;	
			}
			try
			{
				fontQuality=(System.Drawing.Text.TextRenderingHint)info.GetValue("fontQuality",typeof(System.Drawing.Text.TextRenderingHint));
			}
			catch
			{
				fontQuality=TextRenderingHint.SystemDefault;
			}
			try
			{
				foreColor=(System.Drawing.Color)info.GetValue("foreColor",typeof(System.Drawing.Color));
			}
			catch
			{
				foreColor=Color.Black;
			}
		}
		#endregion
	}
	#endregion
	#endregion

	#region public class WebbChartSetting : ISerializable
	[Serializable]
	public class WebbChartSetting : ISerializable
	{
		#region ISerializable Members
		[SecurityPermission(SecurityAction.Demand,SerializationFormatter=true)]
		virtual public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			// TODO:  Add ExControlView.GetObjectData implementation
			info.AddValue("ChartType",this.ChartType,typeof(ChartAppearanceType));
			info.AddValue("Lengend",this.Lengend,typeof(Lengend));
			info.AddValue("AxisX",this.AxisX,typeof(Axis));
			info.AddValue("AxisY",this.AxisY,typeof(Axis));
			info.AddValue("SeriesCollection",this.SeriesCollection,typeof(SeriesCollection));

			info.AddValue("ShowAxesMode",this.ShowAxesMode);

			info.AddValue("TopCount",this.TopCount);

			info.AddValue("PieLabelInfo",this.PieLabelInfo,typeof(PieLabelInfo));		
	
			info.AddValue("CombinedTitle",this.CombinedTitle);
			info.AddValue("BoundSpace",this.BoundSpace);
			info.AddValue("BackgroundColor",this.BackgroundColor,typeof(Color));

			info.AddValue("TransparentBackground",this.TransparentBackground,typeof(Color));

			info.AddValue("DenominatorFilter",this.DenominatorFilter,typeof(Webb.Data.DBFilter));		

		}

		public WebbChartSetting(SerializationInfo info, StreamingContext context)
		{
			// TODO:  Add ExControlView.GetObjectData implementation
			try{this.ChartType = (ChartAppearanceType)info.GetValue("ChartType",typeof(ChartAppearanceType));}
			catch{this.ChartType = ChartAppearanceType.Bar;}

			try{this.Lengend = info.GetValue("Lengend",typeof(Lengend)) as Lengend;}
			catch{this.Lengend = new Lengend();}

			try{this.AxisX = info.GetValue("AxisX",typeof(Axis)) as Axis;}
			catch{this.AxisX = new Axis();}

			try{this.AxisY = info.GetValue("AxisY",typeof(Axis)) as Axis;}
			catch{this.AxisY = new Axis();}

			try{this.SeriesCollection = info.GetValue("SeriesCollection",typeof(SeriesCollection)) as SeriesCollection;}
			catch{this.SeriesCollection = new SeriesCollection(); this._SeriesCollection.Add(new Series());}

			try{this.ShowAxesMode=info.GetBoolean("ShowAxesMode");}  //Added this code at 2008-12-17 10:36:16@Simon
			catch{this.ShowAxesMode=true;}

			try{this.CombinedTitle=info.GetBoolean("CombinedTitle");}  //Added this code at 2008-12-17 10:36:16@Simon
			catch{this.CombinedTitle=false;}

			try{this.TopCount=info.GetInt32("TopCount");}  //Added this code at 2008-12-17 10:36:16@Simon
			catch{this.TopCount=0;}

			try{this.BoundSpace=info.GetInt32("BoundSpace");}  //2009-8-14 13:37:47@Simon Add this Code
			catch{this.BoundSpace=5;}

			try{this.PieLabelInfo = info.GetValue("PieLabelInfo",typeof(PieLabelInfo)) as PieLabelInfo;}
			catch{this.PieLabelInfo=new PieLabelInfo();}

			try{this.BackgroundColor = (Color)info.GetValue("BackgroundColor",typeof(Color));}
			catch{this.BackgroundColor=ColorManager.BackColor;}

			try{this.TransparentBackground=(Color)info.GetValue("TransparentBackground",typeof(Color));}  //Added this code at 2008-12-17 10:36:16@Simon
		
			catch{this.TransparentBackground=Color.White;}
		
			try{this.DenominatorFilter=(DBFilter)info.GetValue("DenominatorFilter",typeof(DBFilter));}  //Added this code at 2008-12-17 10:36:16@Simon
		
			catch{DenominatorFilter=null;}		

		}
		#endregion

		public WebbChartSetting()
		{
			_DenominatorFilter=new DBFilter();	
		}

		public WebbChartSetting Copy()
		{
			WebbChartSetting dumySetting = new WebbChartSetting();

			dumySetting.ChartType = this.ChartType;
			dumySetting.SeriesCollection = this.SeriesCollection.Copy();
			dumySetting.AxisX = this.AxisX.Copy();
			dumySetting.AxisY = this.AxisY.Copy();
			dumySetting.Lengend = this.Lengend.Copy();
			dumySetting.SelectedSeriesIndex = this.SelectedSeriesIndex;

			dumySetting.ShowAxesMode=this.ShowAxesMode;

			dumySetting.AutoFitSize=this.AutoFitSize;

			dumySetting.TopCount=this.TopCount;
           
            dumySetting.PieLabelInfo=this.PieLabelInfo.Copy();
			

            dumySetting.ColorWhenMax=this.ColorWhenMax;

            dumySetting.MaxValuesWhenTop=this.MaxValuesWhenTop;  //2009-8-17 9:12:25@Simon Add this Code

            dumySetting._Relative=this._Relative;
            
			dumySetting._CombinedTitle=this._CombinedTitle;

			dumySetting.BoundSpace=this.BoundSpace;

            dumySetting.BackgroundColor=this.BackgroundColor;

			dumySetting.TransparentBackground=this.TransparentBackground;

			if(this._DenominatorFilter!=null)
			{
				dumySetting._DenominatorFilter=this._DenominatorFilter.Copy();	
			}
			else
			{
				dumySetting._DenominatorFilter=null;
			}
				
			return dumySetting;
		}

		// members
		protected ChartAppearanceType _ChartType = ChartAppearanceType.Bar;
		protected Lengend _Lengend;
		protected Axis _AxisX;
		protected Axis _AxisY;
		protected SeriesCollection _SeriesCollection;
		protected int _SelectedSeriesIndex = -1;
	
		protected PieLabelInfo _PieLabelInfo=new PieLabelInfo();
		
		protected Webb.Data.DBFilter _DenominatorFilter=new DBFilter();
		
			
		[NonSerialized]
		public Color ColorWhenMax=Color.Empty;

		[NonSerialized]
		public float MaxValuesWhenTop=-1;
		
		protected bool _Relative=false;

		protected bool _CombinedTitle=false;

		protected int _BoundSpace=5;

		public Color _BackgroundColor=ColorManager.BackColor;

		protected Color _TransparentBackground=Color.White;


		public DBFilter DenominatorFilter
		{
			get{
				  return this._DenominatorFilter;
			   }

			set{this._DenominatorFilter=value;}
		}


		public Color TransparentBackground
		{
			get{return this._TransparentBackground;}
			set{this._TransparentBackground=value;}
		}

		public bool CombinedTitle
		{
			get{return this._CombinedTitle;}
			set{this._CombinedTitle=value;}
		}
		public int BoundSpace
		{
			get{return this._BoundSpace;}
			set{this._BoundSpace=value;}
		}
		public Color BackgroundColor
		{
			get{return this._BackgroundColor;}
			set{this._BackgroundColor=value;}
		}

	
		public bool Relative
		{
			get{return this._Relative;}
			set{this._Relative=value;}
		}
		public  PieLabelInfo PieLabelInfo
		{
			get{return this._PieLabelInfo;}
			set{this._PieLabelInfo=value;}
		}

		protected int  _TopCount=0;  //Added this code at 2008-12-23 10:56:12@Simon
		public int TopCount  //Added this code at 2008-12-17 17:09:27@Simon
		{
			get{return this._TopCount;}
			set
			{				
				this._TopCount=value;				
			}
		}

		protected bool _AutoFitSize=false;
		public bool AutoFitSize  //Added this code at 2008-12-17 17:09:27@Simon
		{
			get{return this._AutoFitSize;}
			set{this._AutoFitSize=value;}
		}

		protected bool _ShowAxesMode=true;  //Added this code at 2008-12-17 9:24:14@Simon
		public bool ShowAxesMode     //Added this code at 2008-12-17 9:25:53@Simon      
		{
			get{return this._ShowAxesMode;}
			set
			{				
				this._ShowAxesMode=value;
			}
		}

		// properties
		public int SelectedSeriesIndex
		{
			get{return this._SelectedSeriesIndex;}
			set{this._SelectedSeriesIndex = value;}
		}

		public ChartAppearanceType ChartType
		{
			get{return this._ChartType;}
			set{this._ChartType = value;}
		}

		public Lengend Lengend
		{
			get
			{
				if(this._Lengend == null) this._Lengend = new Lengend();
				return this._Lengend;}
			set{this._Lengend = value;}
		}

		public Axis AxisX
		{
			get
			{
				if(this._AxisX == null) this._AxisX = new Axis();
				return this._AxisX;}
			set{this._AxisX = value;}
		}

		public Axis AxisY
		{
			get
			{
				if(this._AxisY == null) this._AxisY = new Axis();
				return this._AxisY;}
			set{this._AxisY = value;}
		}

		public SeriesCollection SeriesCollection
		{
			get
			{
				if(this._SeriesCollection == null)
				{
					this._SeriesCollection = new SeriesCollection();
					this._SeriesCollection.Add(new Series());
				}
				return this._SeriesCollection;}
			set{this._SeriesCollection = value;}
		}
	
		public ChartBase CreateChart(DataTable dt, Collections.Int32Collection rows)
		{
			ChartBase chart = null;

			switch(this.ChartType)
			{
				case ChartAppearanceType.Bar:
					chart = new BarChart();
					break;
				case ChartAppearanceType.Pie:
					chart = new PieChart();
					break;
				case ChartAppearanceType.Point:
					chart = new PointChart();
					break;
				case ChartAppearanceType.Line:
					chart = new LineChart();
					break;
				case ChartAppearanceType.HorizonBar:
					chart = new HorizonBarChart();
					break;	
				case ChartAppearanceType.Pie3D:
					chart = new Pie3dChart();
					break;
				case ChartAppearanceType.Bar3D:
					chart = new Bar3DChart();
					break;
				case ChartAppearanceType.HorizonBar3D:
					chart=new HorizonBar3DChart();
					break;
			}

			if(chart != null)
			{
				chart.Setting = this.Copy();

				chart.CalculateTable(dt,rows);
			}

			return chart;
		}

        public void GetALLUsedFields(ref ArrayList usedFields)
        {  
            if (this.DenominatorFilter != null) this.DenominatorFilter.GetAllUsedFields(ref usedFields);

            if (_SeriesCollection != null)
            {
                foreach (Series series in this._SeriesCollection)
                {
                    if (!usedFields.Contains(series.FieldArgument)) usedFields.Add(series.FieldArgument);

                    series.SectionFiltersWrapper.GetAllUsedFields(ref usedFields);

                    series.Filter.GetAllUsedFields(ref usedFields);
                }
            }
        }
	}
	#endregion
		
	#region public class ChartHelper
	public class ChartHelper
	{	
		public ChartHelper(){}	
		public static void DrawRectangleF(Graphics g,Pen pen,RectangleF rectF)
		{
			PointF[] points = new PointF[5];

			points[0] = new PointF(rectF.Left,rectF.Top);

			points[1] = new PointF(rectF.Right,rectF.Top);

			points[2] = new PointF(rectF.Right,rectF.Bottom);

			points[3] = new PointF(rectF.Left,rectF.Bottom);

			points[4] = new PointF(rectF.Left,rectF.Top);

			g.DrawLines(pen,points);
		}

		public static PointF AnglePoint(RectangleF rectPie,float angle)
		{
			PointF ptfCenter=new PointF(rectPie.Left + rectPie.Width/2, rectPie.Top + rectPie.Height/2);
			float a=rectPie.Width/2;
			float b=rectPie.Height/2;
			float sinA=(float)Math.Sin(angle*Math.PI/180);
			float cosA=(float)Math.Cos(angle*Math.PI/180);	

			//In Ecllispse's Defition,r*r*(cosA*cosA/(a*a)+sinA*sinA/(b*b))=1;

			float rate=(float)Math.Sqrt((cosA*cosA)/(a*a)+(sinA*sinA)/(b*b));
			//so 
			float r=1/rate;

			return new PointF(ptfCenter.X+r*cosA,ptfCenter.Y+r*sinA);			  
		}

		public static PointF TangentSlopeInEllipse(RectangleF rectPie,float x,float y)
		{
			float a=rectPie.Width/2;
			float b=rectPie.Height/2;
			float x0=x-(rectPie.X+ rectPie.Width/2);
			float y0=y-(rectPie.Y+ rectPie.Height/2);

			float sinX,cosX;

			if(y0==0)
			{
				sinX=0f;
				cosX=1f;
			}
			else
			{
				float k=-(b*b*x0)/(a*a*y0);				   
				sinX=(float)Math.Sqrt(1/(1+k*k));
				cosX=(float)Math.Sqrt((k*k)/(1+k*k));
			}	
			return  new PointF(cosX,sinX); 

		}

		public static PointF MovePoint(float angle,PointF point,int len)
		{
			while(angle<0||angle>=360)
			{
				if(angle<0)angle+=360;
				if(angle>=360)angle-=360;
			}
			float cosA= (float)Math.Cos(angle*Math.PI/180f);
			float sinA= (float)Math.Sin(angle*Math.PI/180f);
	         
			return new PointF(point.X+len*cosA,point.Y+len*sinA);
		}		

		public static PointF PointByPoint(RectangleF rectPie,float angle,float x,float y,int len,ChartTextPosition position)
		{
			float sinA=(float)Math.Sin((angle)*Math.PI/180);
			float cosA=(float)Math.Cos((angle)*Math.PI/180);	

			PointF ptfCenter=new PointF(rectPie.Left + rectPie.Width/2, rectPie.Top + rectPie.Height/2);
				
			float xArc=0f,yArc=0f;		

			switch(position)
			{
				case ChartTextPosition.Inside:
					x-=ptfCenter.X;
					y-=ptfCenter.Y;
					float AngleLineLen=(float)Math.Sqrt(x*x+y*y);
					float rate=rectPie.Height>0?AngleLineLen/(rectPie.Height/2):1;
					xArc=ptfCenter.X+rate*len*cosA;
					yArc=ptfCenter.Y+rate*len*sinA;
					break;					    
				case ChartTextPosition.Center:
					x-=ptfCenter.X;
					y-=ptfCenter.Y;
					x/=2;
					y/=2;
					xArc=ptfCenter.X+x;
					yArc=ptfCenter.Y+y;
					break;				
				default:
					xArc=x+len*cosA;
					yArc=y+len*sinA;
					break;
			}
				
			return new PointF(xArc,yArc);
		}		
		public static RectangleF GetRect(float angle,PointF pointArc,SizeF size,ChartTextPosition position)
		{	           			
			float xArc=0f,yArc=0f;

			while(angle<0||angle>=360)
			{
				if(angle<0)angle+=360;
				if(angle>=360)angle-=360;
			}
				
			switch(position)
			{
				case ChartTextPosition.Inside:
				case ChartTextPosition.Center:
					xArc=pointArc.X-size.Width/2;
					yArc=pointArc.Y-size.Height/2;
					break;

				default:
					if(angle==0)
					{
						xArc=pointArc.X;
						yArc=pointArc.Y-size.Height/2;
					}					      
					else if(angle<90)
					{
						xArc=pointArc.X;
						yArc=pointArc.Y;
					}
					else if(angle==90)
					{
						xArc=pointArc.X-size.Width/2;
						yArc=pointArc.Y;
					}    					          
					else if(angle<180)
					{
						xArc=pointArc.X-size.Width;
						yArc=pointArc.Y;
					}
					else if(angle==180)
					{
						xArc=pointArc.X-size.Width;
						yArc=pointArc.Y-size.Height/2;
					}    
					else if(angle<270)
					{
						xArc=pointArc.X-size.Width;
						yArc=pointArc.Y-size.Height;
					}
					else if(angle==270)
					{
						xArc=pointArc.X-size.Width/2;
						yArc=pointArc.Y-size.Height;
					}    
					else
					{
						xArc=pointArc.X;
						yArc=pointArc.Y-size.Height;
					}
					break;
			}
			return new RectangleF(new PointF(xArc,yArc),size);
		}

	}
	#endregion 

	#region public class ColorManager
	public class ColorManager
	{
		public static Hashtable ValueToPieColor = new Hashtable();	//Modified at 2008-12-23 11:34:49@Scott

		public static Color BackColor
		{
			get{return Color.FromArgb(122,248,240,225);}
		}

		public static Color MajorGridLineColor 
		{
			get{return Color.FromArgb(100,190,190,190);}
		}

		public static Color SecondaryGridLineColor 
		{
			get{return Color.FromArgb(50,190,190,190);}
		}

		public static Color AxesColor
		{
			get{return Color.FromArgb(120,20,20,20);}
		}

		public static Color BorderColor
		{
			get{return Color.FromArgb(200,190,190,190);}
		}

		public static readonly Color[] PieColors = new Color[3]{Color.SeaGreen,Color.Tomato,Color.YellowGreen};

		public static Color GetColor(int index)
		{
			int nStep = 5;

			int nInnerIndex = index % PieColors.Length;
				
			Color tempColor = PieColors[nInnerIndex];

			int r = tempColor.R, g = tempColor.G, b = tempColor.B;

			switch(nInnerIndex)
			{ 
				case 0:
					g = Math.Abs(g - index*nStep)%255;
					break;
				case 1:
					r = Math.Abs(r - index*nStep)%255;
					break;
				case 2:
					b = Math.Abs(b - index*nStep)%255;
					break;
			}

			return Color.FromArgb(r,g,b);
		}

		public static Color DiffColor(int index)
		{			
			int alpha,r=255, g=0, b=0 ;
			alpha=Math.Abs(255-index*5);  
			
			switch(index % 3)
			{ 
				case 0:
					r=Math.Abs(214-index*8);
					g=Math.Abs(157-index*8);
					b=Math.Abs(45-index*8);
					break;
				case 1:
					r=Math.Abs(109-index*8);
					g=Math.Abs(173-index*8);
					b=Math.Abs(17-index*8);
					break;
				case 2:
					r=Math.Abs(205-index*8);
					g=Math.Abs(82-index*8);
					b=Math.Abs(34-index*8);
					break;
			}		
			r=r%255;
			g=g%255;
			b=b%255;
            if(alpha<120)alpha+=120;
			if(alpha>255)alpha=255;
			return Color.FromArgb(alpha,r,g,b);
		}

		public static Color GetLightColor(Color cl)
		{  
			int r = Math.Abs(cl.R + 20);
			int g = Math.Abs(cl.G + 70);
			int b = Math.Abs(cl.B + 90);
			r=r%255;
			g=g%255;
			b=b%255;
			return Color.FromArgb(cl.A,r,g,b);
		}
		public static Color GetSoftColor(Color cl)
		{  
			int r = Math.Abs(cl.R + 5);
			int g = Math.Abs(cl.G + 5);
			int b = Math.Abs(cl.B + 5);
			r=r%255;
			g=g%255;
			b=b%255;
			return Color.FromArgb(cl.A,r,g,b);
		}

		public static Color GetDeepColor(Color cl)
		{
			int r = Math.Abs(cl.R-20);
			int g = Math.Abs(cl.G - 70);
			int b = Math.Abs(cl.B- 90);
			r=r%255;
			g=g%255;
			b=b%255;
			return Color.FromArgb(cl.A,r,g,b);
		}
        public static Color GetDeepPieColor(Color cl)
		{
			int r = Math.Abs(cl.R-10);
			int g = Math.Abs(cl.G - 70);
			int b = Math.Abs(cl.B- 200);
			r=r%255;
			g=g%255;
			b=b%255;
			return Color.FromArgb(cl.A,r,g,b);
		}
	}
	public struct PiediffColor
	{
		public Color pieColor;
		public Color lightColor;
		public float angle;
		public PiediffColor(Color color,	Color light,float gradientAngle)
		{
			pieColor=color;
            lightColor=light;
			angle=gradientAngle;
		}

	}
	#endregion
}
    	
	