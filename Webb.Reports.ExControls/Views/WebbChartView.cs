/***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:WebbChartView.cs
 * Author:Country.Wu [EMail:Webb.Country.Wu@163.com]
 * Create Time:11/29/2007 01:18:59 PM
 * Copyright:1986-2007@Webb Electronics all right reserved.
 * Purpose:
 * ***********************************************************************/
#region History
/*
 * //Author@DateTime : Description
 * */
#endregion History

using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Collections.Specialized;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Serialization;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Container;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraPrinting;
using DevExpress.Utils.Design;
using DevExpress.Utils;

using Webb.Reports.ExControls;
using Webb.Reports.ExControls.Data;
using System.Security.Permissions;
using Webb.Collections;
using Webb.Reports.DataProvider;
using System.Drawing.Drawing2D;

namespace Webb.Reports.ExControls.Views
{
	#region WebbChartView : ExControlView
	[Serializable]
	public class WebbChartView : ExControlView
	{
		//ISerializable Members
		#region ISerializable Members
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info,context);

			info.AddValue("RootGroupInfo",this._RootGroupInfo,typeof(GroupInfo));

			info.AddValue("Filter",this._Filter,typeof(Webb.Data.DBFilter));

			info.AddValue("Series",this._Series,typeof(ChartSeriesCollection));

			info.AddValue("AppearanceType",this._AppearanceType,typeof(ChartAppearanceType));

			info.AddValue("StatType",this._StatType,typeof(SummaryTypes));

			info.AddValue("Combine",this._Combine);

			info.AddValue("BackColor",this._BackColor,typeof(Color));

			//info.AddValue("StringFormat",this._StringFormat,typeof(StringFormat));

			info.AddValue("Font",this._Font,typeof(Font));

			info.AddValue("AxisTextFont",this._AxisTextFont,typeof(Font));

			info.AddValue("Step",this._Step);

			info.AddValue("AxisXText",this._AxisXText);

			info.AddValue("AxisYText",this._AxisYText);

			info.AddValue("AxisXGridLine",this._AxisXGridLine);

			info.AddValue("AxisYGridLine",this._AxisYGridLine);

			info.AddValue("BarValueAlignment",this._BarValueAlignment,typeof(BarValueAlignment));

			info.AddValue("BarValueFormat",this._BarValueFormat,typeof(StringFormatFlags));

			info.AddValue("TopSpace",this._TopSpace);

			info.AddValue("LeftSpace",this._LeftSpace);

			info.AddValue("RightSpace",this._RightSpace);

			info.AddValue("BottomSpace",this._BottomSpace);

			info.AddValue("TextPosition",this._TextPosition,typeof(ChartTextPosition));

			info.AddValue("GradientText",this._GradientText);
		}
		
		public WebbChartView(SerializationInfo info, StreamingContext context) : base(info,context)
		{
			try
			{
				this._RootGroupInfo = info.GetValue("RootGroupInfo",typeof(GroupInfo)) as GroupInfo;
			}
			catch
			{
				this._RootGroupInfo = new Webb.Reports.ExControls.Data.FieldGroupInfo(Webb.Data.PublicDBFieldConverter.AvialableFields[0].ToString());
			
				this._RootGroupInfo.ColumnHeading = "New Group";
			}

			try
			{
				this._Filter = info.GetValue("Filter",typeof(Webb.Data.DBFilter)) as Webb.Data.DBFilter;
			}
			catch
			{
				this._Filter = new Webb.Data.DBFilter();
			}

			try
			{
				this._Series = info.GetValue("Series",typeof(ChartSeriesCollection)) as ChartSeriesCollection;
			}
			catch
			{
				this._Series = new ChartSeriesCollection();

				this._Series.Add(new ChartSeries());
			}

			try
			{
				this._AppearanceType = (ChartAppearanceType)info.GetValue("AppearanceType",typeof(ChartAppearanceType));
			}
			catch
			{
				this._AppearanceType = ChartAppearanceType.Bar;
			}

			try
			{
				this._StatType = (SummaryTypes)info.GetValue("StatType",typeof(SummaryTypes));
			}
			catch
			{
				this._StatType = SummaryTypes.Frequence;
			}

			try
			{
				this._Combine = info.GetBoolean("Combine");
			}
			catch
			{
				this._Combine = false;
			}

			try
			{
				this._BackColor = (Color)info.GetValue("BackColor",typeof(Color));
			}
			catch
			{
				this._BackColor = Color.Transparent;
			}

			
			this._StringFormat = new StringFormat();

			this._StringFormat.Alignment = StringAlignment.Center;

			this._StringFormat.LineAlignment = StringAlignment.Center;

			try
			{
				this._Font = info.GetValue("Font",typeof(Font)) as Font;
			}
			catch
			{
				this._Font = new Font(AppearanceObject.DefaultFont.FontFamily, 8f);
			}

			try
			{
				this._AxisTextFont = info.GetValue("AxisTextFont",typeof(Font)) as Font;
			}
			catch
			{
				this._AxisTextFont = new Font(AppearanceObject.DefaultFont.FontFamily, 10f);
			}

			try
			{
				this._Step = info.GetInt32("Step");
			}
			catch
			{
				this._Step = 2;
			}

			try
			{
				this._AxisXText = info.GetString("AxisXText");
			}
			catch
			{
				this._AxisXText = string.Empty;
			}

			try
			{
				this._AxisYText = info.GetString("AxisYText");
			}
			catch
			{
				this._AxisYText = string.Empty;
			}

			try
			{
				this._AxisXGridLine = info.GetBoolean("AxistXGridLine");
			}
			catch
			{
				this._AxisXGridLine = false;
			}

			try
			{
				this._AxisYGridLine = info.GetBoolean("AxisYGridLine");
			}
			catch
			{
				this._AxisYGridLine = false;
			}

			try
			{
				this._BarValueAlignment = (BarValueAlignment)info.GetValue("BarValueAlignment",typeof(BarValueAlignment));
			}
			catch
			{
				this._BarValueAlignment = BarValueAlignment.Top;
			}
					
			try
			{
				this._BarValueFormat = (StringFormatFlags)info.GetValue("BarValueFormat",typeof(StringFormatFlags));
			}
			catch
			{
				this._BarValueFormat = 0;
			}

			try
			{
				this._TopSpace = info.GetInt32("TopSpace");
				this._LeftSpace = info.GetInt32("LeftSpace");
				this._RightSpace = info.GetInt32("RightSpace");
				this._BottomSpace = info.GetInt32("BottomSpace");
			}
			catch
			{
				this.SetDefaultLocation();
			}

			try
			{
				this._TextPosition = (ChartTextPosition)info.GetValue("TextPosition",typeof(ChartTextPosition));
			}
			catch
			{
				this._TextPosition = ChartTextPosition.Inside;
			}

			try
			{
				this._GradientText = info.GetBoolean("GradientText");
			}
			catch
			{
				this._GradientText = false;
			}
		}
		#endregion

		//Ctor
		public WebbChartView(ExControl i_Control):base(i_Control)
		{
			this._RootGroupInfo = new Webb.Reports.ExControls.Data.FieldGroupInfo(Webb.Data.PublicDBFieldConverter.AvialableFields[0].ToString());
			
			this._RootGroupInfo.ColumnHeading = "New Group";

			this._Filter = new Webb.Data.DBFilter();

			this._Series = new ChartSeriesCollection();

			this._Series.Add(new ChartSeries());

			this._AppearanceType = ChartAppearanceType.Bar;

			this._StatType = SummaryTypes.Frequence;

			this._Combine = false;

			this._BackColor = Color.Transparent;

			this._StringFormat = new StringFormat();

			this._StringFormat.Alignment = StringAlignment.Center;

			this._StringFormat.LineAlignment = StringAlignment.Center;

			this._Font = new Font(AppearanceObject.DefaultFont.FontFamily, 8f);

			this._AxisTextFont = new Font(AppearanceObject.DefaultFont.FontFamily, 10f);

			this._Step = 2;

			this._AxisXText = string.Empty;

			this._AxisYText = string.Empty;

			this._AxisXGridLine = false;

			this._AxisYGridLine = false;

			this._BarValueAlignment = BarValueAlignment.Top;

			this._BarValueFormat = 0;

			this.SetDefaultLocation();
		}

		//Members
		protected ChartSeriesCollection _Series;
		protected GroupInfo _RootGroupInfo;
		protected ChartAppearanceType _AppearanceType;
		protected SummaryTypes _StatType;
		protected bool _Combine;
		protected Webb.Data.DBFilter _Filter;
		protected Bitmap _Bitmap;
		protected Graphics _Graphics;
		protected Color _BackColor;
		protected StringFormat _StringFormat;
		protected Font _Font;
		protected int _Step;
		protected string _AxisXText;
		protected string _AxisYText;
		protected Font _AxisTextFont;
		protected bool _AxisXGridLine;
		protected bool _AxisYGridLine;
		protected StringFormatFlags _BarValueFormat;
		protected BarValueAlignment _BarValueAlignment;
		protected int _TopSpace;
		protected int _LeftSpace;
		protected int _RightSpace;
		protected int _BottomSpace;
		protected ChartBase _Chart;
		protected ChartTextPosition _TextPosition;
		protected bool _GradientText;

		//Properties
		public ChartBase Chart
		{
			get{return this._Chart;}
			set{this._Chart = value;}
		}
	
		[Browsable(true),Category("Pie")]
		public ChartTextPosition TextPosition
		{
			get{return this._TextPosition;}
			set{this._TextPosition = value;}
		}

		[Browsable(true),Category("Pie")]
		public bool GradientText
		{
			get{return this._GradientText;}
			set{this._GradientText = value;}
		}

		[Browsable(true),Category("Chart Location")]
		public int TopSpace
		{
			get{return this._TopSpace;}
			set{this._TopSpace = value;}
		}

		[Browsable(true),Category("Chart Location")]
		public int LeftSpace
		{
			get{return this._LeftSpace;}
			set{this._LeftSpace = value;}
		}

		[Browsable(true),Category("Chart Location")]
		public int RightSpace
		{
			get{return this._RightSpace;}
			set{this._RightSpace = value;}
		}

		[Browsable(true),Category("Chart Location")]
		public int BottomSpace
		{
			get{return this._BottomSpace;}
			set{this._BottomSpace = value;}
		}

		[Browsable(false)]
		public bool Combine
		{
			get{return this._Combine;}
			set{this._Combine = value;}
		}

		[Browsable(false)]
		public ChartSeriesCollection Series
		{
			get{return this._Series;}
		}

		[Browsable(false)]
		public GroupInfo RootGroupInfo
		{
			get{return this._RootGroupInfo;}
			set{this._RootGroupInfo = value;}
		}

		[Browsable(true),Category("Appearance")]
		public ChartAppearanceType AppearanceType
		{
			get{return this._AppearanceType;}
			set
			{
				this._AppearanceType = value;
				
				if(value == ChartAppearanceType.Bar)
				{
					this.SetDefaultLocation();
				}
			}
		}

		[Browsable(true),Category("Appearance")]
		public SummaryTypes StatType
		{
			get{return this._StatType;}
			set{this._StatType = value;}
		}

		[Browsable(true),Category("Appearance")]
		public Color BackColor
		{
			get{return this._BackColor;}
			set{this._BackColor = value;}
		}

		[Browsable(true),Category("Axis")]
		public int Step
		{
			get{return this._Step;}
			set{this._Step = value;}
		}

		[Browsable(true),Category("Axis")]
		public string AxisXText
		{
			get{return this._AxisXText;}
			set{this._AxisXText = value;}
		}

		[Browsable(true),Category("Axis")]
		public string AxisYText
		{
			get{return this._AxisYText;}
			set{this._AxisYText = value;}
		}

		[Browsable(true),Category("Axis")]
		public Font AxisTextFont
		{
			get{return this._AxisTextFont;}
			set{this._AxisTextFont = value;}
		}

		[Browsable(true),Category("Axis")]
		public bool AxisXGridLine
		{
			get{return this._AxisXGridLine;}
			set{this._AxisXGridLine = value;}
		}

		[Browsable(true),Category("Axis")]
		public bool AxisYGridLine
		{
			get{return this._AxisYGridLine;}
			set{this._AxisYGridLine = value;}
		}

		[Browsable(false)]
		public Webb.Data.DBFilter Filter
		{
			get{return this._Filter;}
			set{this._Filter = value.Copy();}
		}

		[Browsable(false)]
		public int ControlWidth
		{
			get{return (int)(this.ExControl.XtraContainer.Width/Webb.Utility.ConvertCoordinate);}
		}

		[Browsable(false)]
		public int ControlHeight
		{
			get{return (int)(this.ExControl.XtraContainer.Height/Webb.Utility.ConvertCoordinate);}
		}

		[Browsable(true),Category("Bars")]
		public StringFormatFlags BarValueFormat
		{
			get{return this._BarValueFormat;}
			set{this._BarValueFormat = value;}
		}

		[Browsable(true),Category("Bars")]
		public BarValueAlignment BarValueAlignment
		{
			get{return this._BarValueAlignment;}
			set{this._BarValueAlignment = value;}
		}

		//fuction
		private void SetDefaultLocation()
		{
			this.LeftSpace = 50; this.RightSpace = 90; this.TopSpace = 50; this.BottomSpace = 50;
		}

		public override void CalculateResult(DataTable i_Table)
		{
			GroupView.ConvertOldGroupInfo(this._RootGroupInfo);

			if(i_Table == null) 
			{
				this.RootGroupInfo.ClearGroupResult(this._RootGroupInfo);
				
				return;
			}

			Webb.Collections.Int32Collection m_Rows = this.OneValueScFilter.Filter.GetFilteredRows(i_Table);

			m_Rows = this.RepeatFilter.Filter.GetFilteredRows(i_Table,m_Rows);	 //Added this code at 2008-12-26 12:22:40@Simon

			m_Rows = this.Filter.GetFilteredRows(i_Table,m_Rows);

			this.AddSummaries(this._Series);	//create summaries by series

			this._RootGroupInfo.CalculateGroupResult(i_Table,m_Rows,m_Rows,this._RootGroupInfo);

			switch(this.AppearanceType)
			{//calculate chart
				case ChartAppearanceType.Pie:
				{
//					Pie pie = new Pie();
//					this.Chart = pie.CalculatePie(i_Table,this._RootGroupInfo,this.Filter,this.OneValueScFilter);
//					if(this.Chart is PieChart)
//					{
//						PieChart pieChart = this.Chart as PieChart;
//						pieChart.GradientText = this.GradientText;
//						pieChart.Position = this.TextPosition;
//					}
					break;
				}
				default:
					break;
			}
		}
		
		private int GetTableCells()
		{
			int nCount = 1;

			switch(this._AppearanceType)
			{
				case ChartAppearanceType.Bar:
					nCount += this.GetColumns() * this._Series.Count;
					break;
				case ChartAppearanceType.Pie:
					if(this.RootGroupInfo.GroupResults != null)
					{
						nCount += this.RootGroupInfo.GroupResults.Count;
					}
					break;
				case ChartAppearanceType.Point:
					break;
			}
			return nCount;
		}

		public override bool CreatePrintingTable()
		{
			int nCells = this.GetTableCells();

			this.PrintingTable = new WebbTable(1,nCells);

			if(this._Bitmap != null) this._Bitmap.Dispose();

			this._Bitmap = new Bitmap(this.ControlWidth,this.ControlHeight,this.ExControl.GetGraphics());	

			this._Graphics = Graphics.FromImage(this._Bitmap);

			this._Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

			this.DrawImage(this._Graphics,new Rectangle(new Point(0,0),new Size(this.ControlWidth,this.ControlHeight)));

			IWebbTableCell cell = this.PrintingTable.GetCell(0,0) as WebbTableCell;

			cell.CellStyle.BorderColor = Color.Transparent;

			cell.CellStyle.Width = this._Bitmap.Width;
			
			cell.CellStyle.Height = this._Bitmap.Height;

			cell.Image = (Bitmap)this._Bitmap.Clone();//Image.FromHbitmap(this._Bitmap.GetHbitmap());

			return true;
		}

		public void DrawImage(Graphics g,Rectangle rect)
		{
			Rectangle rectChart = this.GetChartRect(rect,this._AppearanceType);

			g.FillRectangle(Brushes.White,rect);

			this.DrawBackGround(g,rect);

			switch(this._AppearanceType)
			{
				case ChartAppearanceType.Bar:
				{
					this.DrawAxis(g,rectChart);

					this.DrawAxisText(g,rectChart);

					this.DrawGroupedText(g,rectChart);

					this.DrawBars(g,rectChart);

					this.DrawLengends(g,rect);

					break;
				}
				case ChartAppearanceType.Pie:
				{
//					if(this.Chart != null)
//					{
//						this.Chart.Draw(g,rect,null);
//					}

					break;
				}
				case ChartAppearanceType.Point:
				{
					this.DrawPoints(g,rectChart);
					
					break;
				}
				default:
				{
					break;
				}
			}
		}

		private Rectangle GetChartRect(Rectangle rect, ChartAppearanceType appearanceType)
		{
			switch(appearanceType)
			{
				case ChartAppearanceType.Bar:
				{
					Size size = new Size(rect.Width - (this._LeftSpace + this._RightSpace),rect.Height - (this._TopSpace + this._BottomSpace));

					Rectangle rectChart = new Rectangle(new Point(this._LeftSpace,this._TopSpace),size);

					return rectChart;
				}
				case ChartAppearanceType.Pie:
				{
					Size size = new Size(rect.Width - 60,rect.Height);

					Rectangle rectChart = new Rectangle(new Point(0,0),size);

					return rectChart;
				}
				case ChartAppearanceType.Point:
				{
					return rect;
				}
				default:
					return rect;
			}
		}

		private void DrawBackGround(Graphics g,Rectangle rect)
		{
			Brush brush = new SolidBrush(this._BackColor);

			g.FillRectangle(brush,rect);
		}

		#region Bar
		private void DrawLengends(Graphics g,Rectangle rect)
		{
			int offset = 2;

			Size sizeLengend = new Size(80,20);

			Point location = new Point((int)(rect.Right - offset - sizeLengend.Width), rect.Top + offset);

			Rectangle rectLengend = new Rectangle(location,sizeLengend);

			foreach(ChartSeries series in this._Series)
			{
				this.DrawLengend(g,ref rectLengend,series);

				rectLengend.Offset(0,rectLengend.Height);
			}

			if(this._Series.Count > 0)
			{
				Size sizeLengends = new Size(sizeLengend.Width,rectLengend.Top - location.Y);

				Rectangle rectLengends = new Rectangle(location,sizeLengends);

				rectLengends.Height += offset;

				g.DrawRectangle(Pens.Gray,rectLengends);
			}
		}

		private void DrawLengend(Graphics g,ref Rectangle rect, ChartSeries series)
		{
			Brush brush = new SolidBrush(series.BackColor);

			Pen penBorder = new Pen(series.BorderColor);

			SizeF sizeLengend = g.MeasureString(series.SeriesName,this._Font,rect.Width*2/3,this._StringFormat);

			rect.Height = (int)sizeLengend.Height;

			Rectangle rectColor = new Rectangle(rect.Location,new Size(rect.Width/3,rect.Height));

			Rectangle rectString = new Rectangle(new Point(rect.Location.X + rect.Width/3,rect.Location.Y),new Size(rect.Width*2/3,rect.Height));

			rectColor.Inflate(-2,-2);

			rectColor.Offset(1,1);

			g.FillRectangle(brush,rectColor);

			g.DrawRectangle(penBorder,rectColor);

			g.DrawString(series.SeriesName,this._Font,Brushes.Black,rectString,this._StringFormat);
		}

		private void DrawAxisText(Graphics g,Rectangle rect)
		{
			SizeF size = g.MeasureString(this._AxisYText,this._AxisTextFont);

			g.DrawString(this._AxisYText,this._AxisTextFont,Brushes.Black,rect.Left,rect.Top - size.Height);

			size = g.MeasureString(this._AxisXText,this._AxisTextFont);

			g.DrawString(this._AxisXText,this._AxisTextFont,Brushes.Black,rect.Left + (rect.Width - size.Width)/2,rect.Bottom + 50 - size.Height);
		}

		private void DrawAxis(Graphics g,Rectangle rect)
		{
			double maxValue = this.GetMaxValue();
			
			int columns = this.GetColumns();

			double width = 0, offset = 0;

			double step = maxValue / 10;

			int nGraduationLen = 5;

			//axis line
			g.DrawLine(Pens.Black,new Point(rect.Left,rect.Top),new Point(rect.Left,rect.Bottom));

			g.DrawLine(Pens.Black,new Point(rect.Left,rect.Bottom),new Point(rect.Right,rect.Bottom));

			//horz graduation
			width = rect.Width * 1.0 / columns;

			offset = 0;

			for(int i = 0; i < columns + 1 ; i++)
			{
				float x = (float)(rect.Left + offset);

				g.DrawLine(Pens.Black,new PointF(x,rect.Bottom),new PointF(x, rect.Bottom + nGraduationLen));
 
				if(this.AxisXGridLine && i > 0) g.DrawLine(Pens.LightGray,new PointF(x,rect.Bottom - 1),new PointF(x,rect.Top));

				offset += width;
			}

			//Vert graduation
			maxValue *= 1.2;	//120% max value

			columns = 12;

			width = rect.Height * 1.0 /columns;

			offset = 0;

			for(int i = 0; i < columns + 1 ; i++)
			{
				float y = (float)(rect.Bottom - offset);

				if(i%this._Step == 0)
				{
					g.DrawLine(Pens.Black,new PointF(rect.Left,y),new PointF(rect.Left - 2*nGraduationLen, y));

					this.DrawGraduation(g,new PointF(rect.Left - 2*nGraduationLen,y),i*step);
				}
				else
				{
					g.DrawLine(Pens.Black,new PointF(rect.Left,y),new PointF(rect.Left - nGraduationLen, y));
				}

				if(this.AxisYGridLine && i > 0) g.DrawLine(Pens.LightGray,new PointF(rect.Left + 1,y),new PointF(rect.Right, y));

				offset += width;
			}
		}
		#endregion

		#region Point
		private void DrawPoints(Graphics g,Rectangle rect)
		{
			
		}
		#endregion

		private void DrawBars(Graphics g,Rectangle rect)
		{
			GroupSummaryCollection summaries = new GroupSummaryCollection();
			
			GetAllSummaries(this._RootGroupInfo,ref summaries);
			
			int nColumns = this.GetColumns();

			double maxValue = this.GetMaxValue();

			double ratio = rect.Height/maxValue/1.2;

			double groupWidth = (double)rect.Width / nColumns;

			double barWidth = groupWidth / this._Series.Count*0.8;

			double offset = 0, height = 0;

			string value = string.Empty;

			for(int i = 0; i < summaries.Count; i++)
			{
				try
				{
					height = Convert.ToDouble(summaries[i].Value)*ratio;
					
					value = summaries[i].Value.ToString();
				}
				catch
				{
					height = 0;
				}

				int nSeriesID = summaries[i].SummaryID%this._Series.Count;

				if(nSeriesID == 0)
				{
					offset += groupWidth * 0.1;
				}

				Brush brush = new SolidBrush(this._Series[nSeriesID].BackColor);

				Pen penBorder = new Pen(this._Series[nSeriesID].BorderColor);

				Color clrText = this._Series[nSeriesID].TextColor;

				RectangleF rectF = RectangleF.Empty;

				if(height < 0)
				{
					rectF = new RectangleF(new PointF((float)(rect.Left+offset),rect.Bottom),new SizeF((float)barWidth,(float)(-1*height)));
				}
				else
				{
					rectF = new RectangleF(new PointF((float)(rect.Left+offset),rect.Bottom - (float)height),new SizeF((float)barWidth,(float)height));
				}

				g.FillRectangle(brush,rectF);	//draw bar
				
				this.DrawRectangleF(g,penBorder,rectF);	//draw border

				this.SetColWithClickEvent(i + 1, rectF, summaries[i].RowIndicators);

				this.DrawBarValue(g,rectF,clrText,value);

				if(nSeriesID == this._Series.Count - 1)
				{
					offset += groupWidth * 0.1;
				}

				offset += barWidth;
			}
		}

		private RectangleF GetBarValueRectF(Graphics g,RectangleF rcfBar,String strValue,StringFormat sf)
		{
			float offset = 2.0f;

			SizeF szfBarValue = g.MeasureString(strValue,this._Font,rcfBar.Size,sf);

			szfBarValue.Width = Math.Max(szfBarValue.Width,rcfBar.Width);

			RectangleF rcfBarValue = new RectangleF(rcfBar.Location,szfBarValue);

			switch(this._BarValueAlignment)
			{
				case BarValueAlignment.Top:
					rcfBarValue.Offset(0,-1*rcfBarValue.Height - offset);
					break;
				case BarValueAlignment.Center:
					rcfBarValue.Offset(0,(rcfBar.Height - rcfBarValue.Height)/2);
					break;
				case BarValueAlignment.Bottom:
					rcfBarValue.Offset(0,rcfBar.Height - rcfBarValue.Height - offset);
					break;
			}

			return rcfBarValue;
		}

		private void DrawRectangleF(Graphics g,Pen pen,RectangleF rectF)
		{
			PointF[] points = new PointF[5];

			points[0] = new PointF(rectF.Left,rectF.Top);

			points[1] = new PointF(rectF.Right,rectF.Top);

			points[2] = new PointF(rectF.Right,rectF.Bottom);

			points[3] = new PointF(rectF.Left,rectF.Bottom);

			points[4] = new PointF(rectF.Left,rectF.Top);

			g.DrawLines(pen,points);
		}

		private void DrawBarValue(Graphics g, RectangleF rectF, Color clrText, object value)
		{
			if(value.ToString() == string.Empty) return;

			double dValue = Convert.ToDouble(value);

			string strShow = this.FormatValue(dValue,this._StatType);

			StringFormat sf = new StringFormat(this._StringFormat);

			sf.FormatFlags = this._BarValueFormat;

			RectangleF rcfBarValue = this.GetBarValueRectF(g,rectF,strShow,sf);

			g.DrawString(strShow,this._Font,new SolidBrush(clrText),rcfBarValue,sf);
		}

		private string FormatValue(object i_value,SummaryTypes i_Type)
		{
			switch(i_Type)
			{
				case SummaryTypes.Frequence:
				case SummaryTypes.TotalPointsBB:
					return string.Format("{0:0}",i_value);

				case SummaryTypes.ComputedPercent:    //Modified at 2008-9-25 13:59:44@Simon
				case SummaryTypes.Percent:
				case SummaryTypes.RelatedPercent:
				case SummaryTypes.GroupPercent:
				case SummaryTypes.DistPercent:	//Modified at 2008-10-6 14:01:25@Scott
				case SummaryTypes.DistGroupPercent: //Modified at 2008-10-6 15:08:29@Scott
				case SummaryTypes.TopPercent:
					return string.Format("{0:0%}",i_value);
				case SummaryTypes.Average:
				case SummaryTypes.AverageMinus:
				case SummaryTypes.AveragePlus:
				case SummaryTypes.Max:
				case SummaryTypes.Min:
				case SummaryTypes.Total:
				case SummaryTypes.TotalMinus:
				case SummaryTypes.TotalPlus:
					return string.Format("{0:0.00}",i_value);
				default:
					return i_value.ToString();
			}
		}

		private void DrawGraduation(Graphics g,PointF graduationPoint ,double value)
		{
			string strShow = this.FormatValue(value,this._StatType);

			SizeF size = g.MeasureString(strShow,this._Font);

			g.DrawString(strShow,this._Font,Brushes.Black,graduationPoint.X - size.Width, graduationPoint.Y - size.Height/2);
		}

		private void DrawGroupedText(Graphics g,Rectangle rect)
		{
			GroupResultCollection results = new GroupResultCollection();

			this.GetAllGroupResults(this._RootGroupInfo,ref results);

			int columns = this.GetColumns();

			double width = 0;

			double offset = 0,space = 2;

			if(columns == 0) 
			{
				width = 0;

				return;
			}
			else 
			{
				width = (double)rect.Width / columns;
			}

			int count = Math.Min(columns,results.Count);

			for(int i = 0 ; i < count ; i++)
			{
				RectangleF rectText = new RectangleF(new PointF((float)(rect.Left + offset),(float)(rect.Bottom + space)),new SizeF((float)width,20));

				g.DrawString(results[i].GroupValue.ToString(),this._Font,Brushes.Black,rectText,this._StringFormat);

				offset += width;
			}
		}

		private int GetColumns()
		{
			int nRows = 0;

			this.GetSubRows(this._RootGroupInfo,ref nRows);

			return nRows;
		}

		private void GetSubRows(GroupInfo i_GroupInfo,ref int i_value)
		{
			int m_TopGroups = i_GroupInfo.GetGroupedRows();

			if(i_value>0 && m_TopGroups>0)
			{
				i_value --;
				
				if(i_GroupInfo.ParentGroupResult!=null&&i_GroupInfo.ParentGroupResult.ParentGroupInfo!=null)
				{
                    if (i_GroupInfo.ParentGroupResult.ParentGroupInfo.DistinctValues && i_GroupInfo.ParentGroupResult.ParentGroupInfo is SectionGroupInfo)
					{
						i_value++;
					}
				}
			}

			if(i_GroupInfo != null && i_GroupInfo.SubGroupInfos.Count == 0)
			{
                if (i_GroupInfo.DistinctValues)
				{
					i_value += m_TopGroups;
				}
			}
			
			if(i_GroupInfo.AddTotal) i_value++;
			
			i_value += m_TopGroups;
			
			if(i_GroupInfo.GroupResults!=null)
			{
				for(int m_index = 0; m_index<m_TopGroups/*Math.Min(m_TopGroups,i_GroupInfo.GroupResults.Count)*/; m_index++ )
				{
					GroupResult m_Result = i_GroupInfo.GroupResults[m_index];
					
					if(m_Result.SubGroupInfos.Count > 0 && m_Result.SubGroupInfos[0] != null)
					{
						GetSubRows(m_Result.SubGroupInfos[0],ref i_value);
					}
				}
			}
		}

		private void GetAllSummaries(GroupInfo i_GroupInfo, ref GroupSummaryCollection summaries)
		{
			int m_Rows = i_GroupInfo.GetGroupedRows();
			
			if(i_GroupInfo.GroupResults!=null)
			{//sort group result
                if (!(i_GroupInfo is SectionGroupInfo)) i_GroupInfo.GroupResults.Sort(i_GroupInfo.Sorting, i_GroupInfo.SortingBy, i_GroupInfo.UserDefinedOrders);
			}

			for(int m_row = 0; m_row<m_Rows;m_row++)	//The last row is for the total value.
			{
				GroupResult m_GroupResult = i_GroupInfo.GroupResults[m_row];
			
				if(m_GroupResult.Summaries!=null && m_GroupResult.SubGroupInfos.Count == 0)
				{
					foreach(GroupSummary summary in m_GroupResult.Summaries)
					{
						summaries.Add(summary);
					}
				}			
                			
				if(m_GroupResult.SubGroupInfos.Count > 0)
				{
					this.GetAllSummaries(m_GroupResult.SubGroupInfos[0],ref summaries);
				}
			}
		}

		private void GetAllGroupResults(GroupInfo i_GroupInfo, ref GroupResultCollection results)
		{
			int m_Rows = i_GroupInfo.GetGroupedRows();
			
			if(i_GroupInfo.GroupResults!=null)
			{//sort group result
				if(!(i_GroupInfo is SectionGroupInfo)) i_GroupInfo.GroupResults.Sort(i_GroupInfo.Sorting,i_GroupInfo.SortingBy);
			}

			for(int m_row = 0; m_row<m_Rows;m_row++)	//The last row is for the total value.
			{
				GroupResult m_GroupResult = i_GroupInfo.GroupResults[m_row];
				
				if(m_GroupResult.Summaries!=null && m_GroupResult.SubGroupInfos.Count == 0)
				{
					results.Add(m_GroupResult);
				}
                
				if(m_GroupResult.SubGroupInfos.Count > 0)
				{
					this.GetAllGroupResults(m_GroupResult.SubGroupInfos[0],ref results);
				}
			}
		}

		private double GetMaxValue()
		{
			double maxValue = 1.0;

			this.GetMaxValue(this._RootGroupInfo,ref maxValue);

			if(this._StatType == SummaryTypes.Frequence)
			{
				if(maxValue >= 1 && maxValue < 10)
				{
					maxValue = 10.0;
				}
			}

			return maxValue;
		}

		private void GetMaxValue(GroupInfo i_GroupInfo, ref double maxValue)
		{
			int m_Rows = i_GroupInfo.GetGroupedRows();
			
			if(i_GroupInfo.GroupResults!=null)
			{//sort group result
				if(!(i_GroupInfo is SectionGroupInfo)) i_GroupInfo.GroupResults.Sort(i_GroupInfo.Sorting,i_GroupInfo.SortingBy);
			}

			for(int m_row = 0; m_row<m_Rows;m_row++)	//The last row is for the total value.
			{
				GroupResult m_GroupResult = i_GroupInfo.GroupResults[m_row];

				if(i_GroupInfo.SubGroupInfos.Count == 0)
				{
					if(m_GroupResult.Summaries!=null)
					{
						foreach(GroupSummary summary in m_GroupResult.Summaries)
						{
							if(summary.Value == null || summary.Value.ToString() == string.Empty) continue;

							try
							{
								double tempValue = Convert.ToDouble(summary.Value);

								maxValue = Math.Max(maxValue,tempValue);
							}
							catch
							{
								continue;
							}
						}
					}
				}
                			
				if(m_GroupResult.SubGroupInfos.Count > 0)
				{
					this.GetMaxValue(m_GroupResult.SubGroupInfos[0],ref maxValue);
				}
			}
		}

		public void AddSummaries(ChartSeriesCollection series)
		{
			GroupInfo leafGroupInfo = this.GetLeafGroupInfo();

			GroupSummaryCollection summaries = series.CreateSummaies(this._StatType);

			leafGroupInfo.Summaries = summaries.CopyStructure();
		}

		public GroupInfo GetLeafGroupInfo()
		{
			GroupInfo groupInfo = this._RootGroupInfo;

			while(true)
			{
				if(groupInfo.SubGroupInfos.Count == 0) return groupInfo;

				groupInfo = groupInfo.SubGroupInfos[0];
			}
		}

		public void CheckSummaryType()
		{
			switch(this.StatType)
			{
				case SummaryTypes.Frequence:
				case SummaryTypes.Percent:
				case SummaryTypes.RelatedPercent:
				case SummaryTypes.GroupPercent:
				case SummaryTypes.TopPercent:
				case SummaryTypes.None:
					break;
				default:
					if(!this.CheckSummaryType(this.StatType))
					{
						Webb.Utilities.TopMostMessageBox.ShowMessage("Please add series or set field for series.",MessageBoxButtons.OK);

						this.StatType = SummaryTypes.Frequence;
					}
					break;
			}
		}

		private bool CheckSummaryType(SummaryTypes type)
		{
			if(this.Series.Count == 0) return false;

			foreach(ChartSeries series in this.Series)
			{
				if(series.Field == null || series.Field == string.Empty) return false;
			}

			return true;
		}

		override public void Paint(PaintEventArgs e)
		{
			if(this.PrintingTable!=null)
			{
				(this.PrintingTable.GetCell(0,0) as WebbTableCell).PaintCell(e,0,0);
			}
			else
			{
				base.Paint(e);
			}
		}

		#region CreateArea
		private void CreateClickAreaForBarChart(string areaName, IBrickGraphics graph)
		{
			//Draw Transparent Bars Brick
			WebbTableCell m_cell = null;
			
			TextBrick m_brick = null;

			int m_row = 0,m_col = 0;

			Rectangle rect = this.GetChartRect(new Rectangle(new Point(0,0),new Size(this.ControlWidth,this.ControlHeight)),this.AppearanceType);

			GroupSummaryCollection summaries = new GroupSummaryCollection();
			
			GetAllSummaries(this._RootGroupInfo,ref summaries);
			
			int nColumns = this.GetColumns();

			double maxValue = this.GetMaxValue();
			
			double ratio = rect.Height/maxValue/1.2;

			double groupWidth = (double)rect.Width / nColumns;
	
			double barWidth = groupWidth / this._Series.Count*0.8;

			double offset = 0, height = 0;

			string value = string.Empty;

			for(m_col = 1; m_col < this.PrintingTable.GetColumns(); m_col++)
			{
				try
				{
					height = Convert.ToDouble(summaries[m_col-1].Value)*ratio;
					
					value = summaries[m_col-1].Value.ToString();
				}
				catch
				{
					height = 0;
				}

				int nSeriesID = summaries[m_col-1].SummaryID%this._Series.Count;

				if(nSeriesID == 0)
				{
					offset += groupWidth * 0.1;
				}

				Brush brush = new SolidBrush(this._Series[nSeriesID].BackColor);

				Pen penBorder = new Pen(this._Series[nSeriesID].BorderColor);

				Color clrText = this._Series[nSeriesID].TextColor;

				RectangleF rectF = RectangleF.Empty;

				if(height < 0)
				{
					rectF = new RectangleF(new PointF((float)(rect.Left+offset),rect.Bottom),new SizeF((float)barWidth,(float)(-1*height)));
				}
				else
				{
					rectF = new RectangleF(new PointF((float)(rect.Left+offset),rect.Bottom - (float)height),new SizeF((float)barWidth,(float)height));
				}

				m_cell = this.PrintingTable.GetCell(m_row,m_col) as WebbTableCell;

				m_brick = m_cell.CreateTextBrick();
				
				if(m_cell.ClickEventArg!=null)
				{
					m_brick.MouseDown += new MouseEventHandler(Webb.Reports.DataProvider.VideoPlayBackManager.OnBrickDown);
					m_brick.MouseUp += new MouseEventHandler(Webb.Reports.DataProvider.VideoPlayBackManager.OnBrickUp);
					m_brick.OnClick += new EventHandler(m_cell.ClickHandler);
					m_brick.Url = "http://www.webbelectronics.com/";
				}
				
				rectF.Offset(this.PrintingTable.Offset.Width,this.PrintingTable.Offset.Height);

				graph.DrawBrick(m_brick,rectF);

				if(nSeriesID == this._Series.Count - 1)
				{
					offset += groupWidth * 0.1;
				}

				offset += barWidth;
			}
		}

		private void CreateClickAreaForPieChart(string areaName, IBrickGraphics graph)
		{
			if(this.RootGroupInfo.GroupResults == null) return;

			int count = this.RootGroupInfo.GroupResults.Count;

			WebbTableCell m_cell = null;
			
			TextBrick m_brick = null;

			Rectangle rcClient = new Rectangle(new Point(0,0),new Size(this.ControlWidth,this.ControlHeight));

			rcClient.Offset(this.PrintingTable.Offset.Width,this.PrintingTable.Offset.Height);

			int offset = 2;

			Size sizeLengend = new Size(60,15);

			Point location = new Point((int)(rcClient.Right - offset - sizeLengend.Width), rcClient.Top + offset);

			Rectangle rectLengend = new Rectangle(location,sizeLengend);

			for(int index = 0; index < count; index++)
			{
				m_cell = this.PrintingTable.GetCell(0,index + 1) as WebbTableCell;

				m_brick = m_cell.CreateTextBrick();	
				
				if(m_cell.ClickEventArg!=null)
				{
					m_brick.MouseDown += new MouseEventHandler(Webb.Reports.DataProvider.VideoPlayBackManager.OnBrickDown);
					m_brick.MouseUp += new MouseEventHandler(Webb.Reports.DataProvider.VideoPlayBackManager.OnBrickUp);
					m_brick.OnClick += new EventHandler(m_cell.ClickHandler);
					m_brick.Url = "http://www.webbelectronics.com/";
				}

				graph.DrawBrick(m_brick,RectangleF.FromLTRB(rectLengend.Left,rectLengend.Top,rectLengend.Right,rectLengend.Bottom));

				rectLengend.Offset(0,sizeLengend.Height);
			}
		}

		public override int CreateArea(string areaName, IBrickGraphics graph)
		{
			this.PrintingTable.AdjustSize();

			int m_x = this.PrintingTable.Offset.Width;
			int m_y = this.PrintingTable.Offset.Height;

			WebbTableCell m_cell = null;

			int m_row = 0,m_col = 0;

			m_cell = this.PrintingTable.GetCell(m_row,m_col) as WebbTableCell;
				
			ImageBrick m_ImageBrick = m_cell.CreateImageBrick();

			graph.DrawBrick(m_ImageBrick,new RectangleF(m_x,m_y,m_cell.CellStyle.Width,m_cell.CellStyle.Height));

			switch(this._AppearanceType)
			{
				case ChartAppearanceType.Bar:
					this.CreateClickAreaForBarChart(areaName,graph);
					break;
				case ChartAppearanceType.Pie:
					this.CreateClickAreaForPieChart(areaName,graph);
					break;
				case ChartAppearanceType.Point:
					break;
				default:
					break;
			}
			return m_y+m_cell.CellStyle.Height;
		}
		#endregion

		#region Set_Bar_Value
		private void SetColWithClickEvent(int nCol,RectangleF rcRect, Int32Collection arrRows)
		{
			WebbTableCell cell = this.PrintingTable.GetCell(0,nCol) as WebbTableCell;

			cell.CellStyle.BorderColor = Color.Transparent;

			DataSet m_DBSet = this.ExControl.DataSource as DataSet;
			
			Webb.Reports.DataProvider.VideoPlayBackArgs m_args = new Webb.Reports.DataProvider.VideoPlayBackArgs(m_DBSet,arrRows);
			
			cell.ClickEventArg = m_args;
		}
		#endregion
	}
	#endregion

	public enum BarValueAlignment
	{
		Top = 0,
		Center,
		Bottom
	}
}
