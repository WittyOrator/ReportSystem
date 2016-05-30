/***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:WebbTable.cs
 * Author:Country.Wu [EMail:Webb.Country.Wu@163.com]
 * Create Time:11/12/2007 12:55:13 PM
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
using System.Runtime.Serialization;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Windows.Forms.Design;
using System.ComponentModel.Design.Serialization;
using System.Windows.Forms.ComponentModel;
using System.Reflection;
using System.Drawing.Drawing2D;

using DevExpress.Data;
using DevExpress.LookAndFeel;
using DevExpress.LookAndFeel.Helpers;
using DevExpress.Utils.Design;
using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.Utils.Win;
using DevExpress.Utils.Controls;
using DevExpress.Utils.Drawing;
using DevExpress.Utils.Drawing.Helpers;
using DevExpress.XtraTab;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Container;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraPrinting;
using DevExpress.Accessibility;
using DevExpress.XtraEditors.Filtering;
using DevExpress.XtraEditors.Design;
using DevExpress.XtraReports.Design;
using DevExpress.XtraReports.Localization;
using DevExpress.XtraReports.UI;

using Webb.Collections;
using Webb.Reports.ExControls.Data;

namespace Webb.Reports.ExControls
{
	
	[Serializable]
	[Flags]
	public enum MergeTypes
	{
		None = 0,
		Right = 1,
		Down = 2,
		Merged = 4,
		End = 8,	//Modified at 2008-11-6 14:40:03@Scott
	}

	#region public interface IWebbTable : IBasePrintable,ISerializable
	[ComVisible(true)]
	public interface IWebbTable //: DevExpress.XtraPrinting.IBasePrintable//,ISerializable	//Modified at 2008-12-15 9:33:32@Scott
	{
		int InitializeTable(int i_Rows, int i_Columns);
		IWebbTableCell GetCell(int i_Row, int i_Column);
		int MergeCells(int i_StartRow, int i_EndRow, int i_StartColumn, int i_EndColumn);
		int ShowPreview();
		int Print(int i_PrinterID);
		int ReSizeTable(int i_Rows, int i_Columns);
		int GetColumnWidth(int i_ColumnIndex);
		int GetRowHight(int i_RowIndex);
		void SetColumnStyle(int i_ColumnIndex, IBasicStyle i_Style);
		void SetRowStyle(int i_RowIndex, IBasicStyle i_Style);
		int GetRows();
		int GetColumns();
		IBasicStyle GetTableStyle();
		void SetTableStyle(IBasicStyle i_Style);
		void SetOffset(int i_X, int i_Y);
        void SplitTable(int wrappedColumn, int HeaderRowsCount,int nTopCount);
	}
	#endregion

	#region public interface IWebbTableCell : ISerializable,DevExpress.XtraPrinting.IBasePrintable
	[ComVisible(true)]
	public interface IWebbTableCell : DevExpress.XtraPrinting.IBasePrintable//,ISerializable	//Modified at 2008-12-15 9:33:37@Scott
	{
		MergeTypes MergeType { get; set;}
		IBasicStyle CellStyle { get; set;}
		Image Image { get;set;} //12-20-2007@Scott
		string ImagePath { get; set;}
		string Text { get; set;}
		PictureBoxSizeMode ImageSizeMode { get;set;}
	}
	#endregion

	#region public interface IBasicStyle : ISerializable
	[ComVisible(true)]
	public interface IBasicStyle //: ISerializable	//Modified at 2008-12-15 9:33:45@Scott
	{
		int Width { get;set;}
		int Height { get;set;}
		void SetStyle(IBasicStyle i_Style);
		void SetStyle(IBasicStyle i_Style,bool bColorNeedChange);
		IBasicStyle Copy();
		Color BackgroundColor { get; set;}
		Color ForeColor { get; set;}
		DevExpress.XtraPrinting.BorderSide Sides { get; set;}
		Color BorderColor { get; set;}
		int BorderWidth { get; set;}
		Font Font { get; set;}
		System.Drawing.StringFormatFlags StringFormat { get; set;}
		//System.Drawing.StringAlignment StringAlignment{get; set;}
		System.Drawing.StringTrimming StringTrimming { get; set;}
		HorzAlignment HorzAlignment { get;set;}
		VertAlignment VertAlignment { get;set;}
		BrickBorderStyle BorderStyle { get;set;}
		//float Radius {get;set;}	//Modified at 2008-12-16 13:55:40@Scott
	}
	#endregion

	#region public class BasicStyle : IBasicStyle
	/*Descrition:   */
	[Serializable, ComVisible(true)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class BasicStyle : IBasicStyle
	{
		internal class ConstValue
		{
			public const int CellHeight = 20;
			public const int CellWidth = 55;
            public const int RowIndicatorWidth = 55;
		}
		//Wu.Country@2007-11-12 01:20 PM added this class.
		//Fields
		private Color _BackgroundColor;
		private Color _ForceColor;
		private DevExpress.XtraPrinting.BorderSide _Sides;
		private Color _BorderColor;
		private int _BorderWidth;
		private Font _Font;
		private StringFormatFlags _StringFormat;
		//private StringAlignment _StringAlignment;
		private StringTrimming _StringTrimming;
		private int _Width;
		private int _Height;
		private HorzAlignment _HorzAlignment;
		private VertAlignment _VertAlignment;
		private BrickBorderStyle _BorderStyle;
		//private float _Radius;	//Added at 2008-12-16 13:59:27@Scott

		//ctor
		public BasicStyle()
		{
			this.Revert();
		}

		public BasicStyle(BasicStyle oldBasicStyle)
		{
			//this._Width = oldBasicStyle._Width;
			//this._Height = oldBasicStyle._Height;
			this._BackgroundColor = oldBasicStyle._BackgroundColor;
			this._ForceColor = oldBasicStyle._ForceColor;
			this._Sides = oldBasicStyle._Sides;
			this._BorderColor = oldBasicStyle._BorderColor;
			this._BorderWidth = oldBasicStyle._BorderWidth;
			this._Font = oldBasicStyle._Font;
			this._StringTrimming = oldBasicStyle._StringTrimming;
			this._StringFormat = oldBasicStyle._StringFormat;
			//this._StringAlignment = oldBasicStyle._StringAlignment;
			this._HorzAlignment = oldBasicStyle._HorzAlignment;
			this._VertAlignment = oldBasicStyle._VertAlignment;
			//this._Radius = oldBasicStyle._Radius;
		}

		public bool IsEdited()
		{
			bool ret = this._Font.Equals(new Font(AppearanceObject.DefaultFont.FontFamily, 10f));

			if (/*this._Width == ConstValue.CellWidth &&
				this._Height == ConstValue.CellHeight &&*/
				this._BackgroundColor == Color.Transparent &&
				this._ForceColor == Color.Black &&
				this._Sides == DevExpress.XtraPrinting.BorderSide.All &&
				this._BorderColor == Color.Black &&
				this._BorderWidth == 1 &&
				this._Font.Equals(new Font(AppearanceObject.DefaultFont.FontFamily, 10f)) &&
				this._StringTrimming == StringTrimming.None &&
				Font.Equals(this._Font, new Font(AppearanceObject.DefaultFont.FontFamily, 10f)) &&
				this._HorzAlignment == HorzAlignment.Center &&
				this._VertAlignment == VertAlignment.Center &&
				this._BorderStyle == BrickBorderStyle.Center &&
				this._StringFormat == 0)// &&
				//this._Radius == 0)
			{
				return false;
			}
			return true;
		}


		public bool IsSimpleEdited()
		{
			bool ret = this._Font.Equals(new Font(AppearanceObject.DefaultFont.FontFamily, 10f));

			if (/*this._Width == ConstValue.CellWidth &&
				this._Height == ConstValue.CellHeight &&*/				
				this._ForceColor == Color.Black &&
				this._Sides == DevExpress.XtraPrinting.BorderSide.All &&
				this._BorderColor == Color.Black &&
				this._BorderWidth == 1 &&
				this._Font.Equals(new Font(AppearanceObject.DefaultFont.FontFamily, 10f)) &&
				this._StringTrimming == StringTrimming.None &&
				Font.Equals(this._Font, new Font(AppearanceObject.DefaultFont.FontFamily, 10f)) &&
				this._HorzAlignment == HorzAlignment.Center &&
				this._VertAlignment == VertAlignment.Center &&
				this._BorderStyle == BrickBorderStyle.Center &&
				this._StringFormat == 0)// &&
				//this._Radius == 0)
			{
				return false;
			}
			return true;
		}

		public void Revert()
		{
			this._Width = ConstValue.CellWidth;
			this._Height = ConstValue.CellHeight;
			this._BackgroundColor = Color.Transparent;
			this._ForceColor = Color.Black;
			this._Sides = DevExpress.XtraPrinting.BorderSide.All;
			this._BorderColor = Color.Black;
			this._BorderWidth = 1;
			this._Font = new Font(AppearanceObject.DefaultFont.FontFamily, 10f);
			this._StringTrimming = StringTrimming.None;
			this._StringFormat = 0;
			//this._StringAlignment = StringAlignment.Near;
			this._HorzAlignment = HorzAlignment.Center;
			this._VertAlignment = VertAlignment.Center;
			this._BorderStyle = BrickBorderStyle.Center;
			//this._Radius = 0;
		}
		//Methods


		public TextBrick CreateBrick()
		{
			// TODO: implement
			TextBrick i_Brick = new TextBrick();
			i_Brick.BackColor = this._BackgroundColor;
			i_Brick.BorderColor = this._BorderColor;
			i_Brick.BorderWidth = this._BorderWidth;
			i_Brick.Font = this._Font;
			i_Brick.ForeColor = this._ForceColor;
			i_Brick.Sides = this._Sides;
			i_Brick.StringFormat = new BrickStringFormat(this._StringFormat);
			i_Brick.HorzAlignment = this._HorzAlignment;//HorzAlignment.Center;
			i_Brick.VertAlignment = this._VertAlignment;//VertAlignment.Center;
			i_Brick.BorderStyle = this._BorderStyle;
			//i_Brick.Style.Radius = this._Radius;
			
			return i_Brick;
		}

		public ImageBrick CreateImageBrick(string i_Path, PictureBoxSizeMode i_ImageMode)
		{
			ImageBrick m_ImageBrick = new ImageBrick();
			m_ImageBrick.Image =Webb.Utility.ReadImageFromPath(i_Path);
			m_ImageBrick.BackColor = this._BackgroundColor;
			m_ImageBrick.BorderColor = this._BorderColor;
			//m_ImageBrick.BorderStyle = BrickBorderStyle.Inset;
			m_ImageBrick.BorderWidth = this._BorderWidth;
			m_ImageBrick.Sides = this._Sides;
			m_ImageBrick.BorderStyle = this._BorderStyle;
			m_ImageBrick.SizeMode = i_ImageMode;
			//m_ImageBrick.fi
			return m_ImageBrick;
		}

		//12-20-2007@Scott
		public ImageBrick CreateImageBrick(Image i_Image, PictureBoxSizeMode i_ImageMode)
		{
			ImageBrick m_ImageBrick = new ImageBrick();
			m_ImageBrick.Image = i_Image;
			m_ImageBrick.BackColor = this._BackgroundColor;
			m_ImageBrick.BorderColor = this._BorderColor;
			//m_ImageBrick.BorderStyle = BrickBorderStyle.Inset;
			m_ImageBrick.BorderWidth = this._BorderWidth;
			m_ImageBrick.Sides = this._Sides;
			m_ImageBrick.BorderStyle = this._BorderStyle;
			m_ImageBrick.SizeMode = i_ImageMode;
			//m_ImageBrick.fi
			return m_ImageBrick;
		}

		public void UpdateBrick(TextBrick i_Brick)
		{
			// TODO: implement
			i_Brick.BackColor = this._BackgroundColor;
			i_Brick.BorderColor = this._BorderColor;
			i_Brick.BorderWidth = this._BorderWidth;
			i_Brick.Font = this._Font;
			i_Brick.ForeColor = this._ForceColor;
			i_Brick.Sides = this._Sides;
			i_Brick.StringFormat = new BrickStringFormat(this._StringFormat);
			i_Brick.HorzAlignment = this._HorzAlignment;//HorzAlignment.Center;
			i_Brick.VertAlignment = this._VertAlignment;//VertAlignment.Default;
			i_Brick.BorderStyle = this._BorderStyle;
			//i_Brick.Style.Radius = this._Radius;
		}

		#region IBasicStyle
		[Browsable(false)]
		public int Width { get { return this._Width; } set { this._Width = value; } }
		[Browsable(false)]
		public int Height { get { return this._Height; } set { this._Height = value; } }
		public HorzAlignment HorzAlignment { get { return this._HorzAlignment; } set { this._HorzAlignment = value; } }
		public VertAlignment VertAlignment { get { return this._VertAlignment; } set { this._VertAlignment = value; } }
		public BrickBorderStyle BorderStyle { get { return this._BorderStyle; } set { this._BorderStyle = value; } }
		//public float Radius {get{return this._Radius;}set{ this._Radius = value;}}

		public void SetStyle(IBasicStyle NewStyle)
		{
			// TODO: implement
			//if (NewStyle.BackgroundColor != Color.Transparent)    //Added this code at 2008-12-31 10:48:18@Simon
				  this._BackgroundColor = NewStyle.BackgroundColor;  
			//if (NewStyle.ForeColor != Color.Black)
			    this._ForceColor = NewStyle.ForeColor;
			this._Sides = NewStyle.Sides;
			//if (NewStyle.BorderColor != Color.Black)
			  this._BorderColor = NewStyle.BorderColor;
			this._BorderWidth = NewStyle.BorderWidth;
			this._Font = NewStyle.Font;
			this._StringTrimming = NewStyle.StringTrimming;
			this._StringFormat = NewStyle.StringFormat;
			//
			this._HorzAlignment = NewStyle.HorzAlignment;
			this._VertAlignment = NewStyle.VertAlignment;
			this._BorderStyle = NewStyle.BorderStyle;
//			this._Radius = NewStyle.Radius;
			//this._Width = NewStyle.Width;
			//this._Height = NewStyle.Height;

			//08-15-2008@Simon
			////Comapare BackgroundColor 
			//if (NewStyle.BackgroundColor != Color.Transparent)
			//    this._BackgroundColor = NewStyle.BackgroundColor;

			////Comapare ForeColor
			//if (NewStyle.ForeColor != Color.Black)
			//    this._ForceColor = NewStyle.ForeColor;

			////Comapare BorderSides
			//if (NewStyle.Sides != DevExpress.XtraPrinting.BorderSide.All)
			//    this._Sides = NewStyle.Sides;
            
			////Comapare BorderColor
			//if (NewStyle.BorderColor != Color.Black)
			//    this._BorderColor = NewStyle.BorderColor;

			////Comapare BorderWidth
			//if (NewStyle.BorderWidth != 1)
			//    this._BorderWidth = NewStyle.BorderWidth;

			//Font testFont = new Font(AppearanceObject.DefaultFont.FontFamily, 10f);
			//if (!NewStyle.Font.Equals(testFont))
			//    this._Font = (Font)NewStyle.Font.Clone();
			//testFont.Dispose();

			////Comapare StringTrimming
			//if (NewStyle.StringTrimming != StringTrimming.None)
			//    this._StringTrimming = NewStyle.StringTrimming;

			////Comapare StringTrimming
			//if (NewStyle.StringFormat != 0)
			//    this._StringFormat = NewStyle.StringFormat;

			////Comapare HorzAlignment
			//if (NewStyle.HorzAlignment != HorzAlignment.Center)
			//    this._HorzAlignment = NewStyle.HorzAlignment;

			////Comapare VertAlignment 
			//if (NewStyle.VertAlignment != VertAlignment.Center)
			//    this._VertAlignment = NewStyle.VertAlignment;

			////Comapare BorderStyle
			//if (NewStyle.BorderStyle != BrickBorderStyle.Center)
			//  
		}

		//Modified at 2009-2-11 15:32:46@Scott
		public void SetStyle(IBasicStyle NewStyle,bool bColorNeedChange)
		{
			// TODO: implement
			//if (NewStyle.BackgroundColor != Color.Transparent)    //Added this code at 2008-12-31 10:48:18@Simon
			if(bColorNeedChange) this._BackgroundColor = NewStyle.BackgroundColor;  
			//if (NewStyle.ForeColor != Color.Black)
			this._ForceColor = NewStyle.ForeColor;
			this._Sides = NewStyle.Sides;
			//if (NewStyle.BorderColor != Color.Black)
			this._BorderColor = NewStyle.BorderColor;
			this._BorderWidth = NewStyle.BorderWidth;
			this._Font = NewStyle.Font;
			this._StringTrimming = NewStyle.StringTrimming;
			this._StringFormat = NewStyle.StringFormat;
			//
			this._HorzAlignment = NewStyle.HorzAlignment;
			this._VertAlignment = NewStyle.VertAlignment;
			this._BorderStyle = NewStyle.BorderStyle;
		}

		public IBasicStyle Copy()
		{
			BasicStyle m_NewStyle = new BasicStyle();
			//
			m_NewStyle._BackgroundColor = this._BackgroundColor;
			m_NewStyle._ForceColor = this._ForceColor;
			m_NewStyle._Sides = this._Sides;
			m_NewStyle._BorderColor = this._BorderColor;
			m_NewStyle._BorderWidth = this._BorderWidth;
			m_NewStyle._Font = this._Font;
			m_NewStyle._StringTrimming = this._StringTrimming;
			m_NewStyle._StringFormat = this._StringFormat;
			//m_NewStyle._Width = this._Width;
			//m_NewStyle._Height = this._Height;
			m_NewStyle._VertAlignment = this._VertAlignment;
			m_NewStyle._HorzAlignment = this._HorzAlignment;
			m_NewStyle._BorderStyle = this._BorderStyle;
			//m_NewStyle._Radius = this._Radius;
			//m_NewStyle._StringAlignment = this._StringAlignment;
			// TODO: implement
			return m_NewStyle;
		}

		[Category("Color")]
		public Color BackgroundColor
		{
			get
			{
				return _BackgroundColor;
			}
			set
			{
				//if (this._BackgroundColor != value)
				this._BackgroundColor = value;
			}
		}

		[Category("Color")]
		public Color ForeColor
		{
			get
			{
				return _ForceColor;
			}
			set
			{
				//if (this._ForceColor != value)
				this._ForceColor = value;
			}
		}

		[Description("Determines the Border Style"), Category("Style")]
		[Editor(typeof(Editors.SidesEditor), typeof(System.Drawing.Design.UITypeEditor))]	//07-15-2008@Scott
		public DevExpress.XtraPrinting.BorderSide Sides
		{
			get
			{
				return _Sides;
			}
			set
			{
				//if (this._BorderType != value)
				this._Sides = value;
			}
		}

		[Category("Color")]
		public Color BorderColor
		{
			get
			{
				return _BorderColor;
			}
			set
			{
				//if (this._BorderColor != value)
				this._BorderColor = value;
			}
		}

		public int BorderWidth
		{
			get
			{
				return _BorderWidth;
			}
			set
			{
				//if (this._BorderWidth != value)
				this._BorderWidth = value;
			}
		}

		public Font Font
		{
			get
			{
				return _Font;
			}
			set
			{
				if (this._Font != value)
					this._Font = value;
			}
		}
        [Editor(typeof(Editors.StringFormatEditor), typeof(System.Drawing.Design.UITypeEditor))]	//07-15-2008@Scott
		public System.Drawing.StringFormatFlags StringFormat
		{
			get
			{
				return this._StringFormat;
			}
			set
			{
				//if (this._StringFormat != value)
				this._StringFormat = value;
			}
		}

		//		public System.Drawing.StringAlignment StringAlignment
		//		{
		//			get
		//			{
		//				return _StringAlignment;
		//			}
		//			set
		//			{
		//				//if (this._StringAlignment != value)
		//					this._StringAlignment = value;
		//			}
		//		}

        [Browsable(false)]
		public System.Drawing.StringTrimming StringTrimming
		{
			get
			{
				return _StringTrimming;
			}
			set
			{
				//if (this._StringTrimming != value)
				this._StringTrimming = value;
			}
		}
		#endregion

		#region Serialization By Macro 2008-12-15 9:41:12
		public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			info.AddValue("_BackgroundColor",_BackgroundColor,typeof(System.Drawing.Color));
			info.AddValue("_ForceColor",_ForceColor,typeof(System.Drawing.Color));
			info.AddValue("_Sides",_Sides,typeof(DevExpress.XtraPrinting.BorderSide));
			info.AddValue("_BorderColor",_BorderColor,typeof(System.Drawing.Color));
			info.AddValue("_BorderWidth",_BorderWidth);
			info.AddValue("_Font",_Font,typeof(System.Drawing.Font));
			info.AddValue("_StringFormat",_StringFormat,typeof(System.Drawing.StringFormatFlags));
			info.AddValue("_StringTrimming",_StringTrimming,typeof(System.Drawing.StringTrimming));
			info.AddValue("_Width",_Width);
			info.AddValue("_Height",_Height);
			info.AddValue("_HorzAlignment",_HorzAlignment,typeof(DevExpress.Utils.HorzAlignment));
			info.AddValue("_VertAlignment",_VertAlignment,typeof(DevExpress.Utils.VertAlignment));
			info.AddValue("_BorderStyle",_BorderStyle,typeof(DevExpress.XtraPrinting.BrickBorderStyle));
			//info.AddValue("_Radius",_Radius);
		}

		public BasicStyle(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			try
			{
				_BackgroundColor=(System.Drawing.Color)info.GetValue("_BackgroundColor",typeof(System.Drawing.Color));
			}
			catch
			{
				this._BackgroundColor = Color.Transparent;
			}
			try
			{
				_ForceColor=(System.Drawing.Color)info.GetValue("_ForceColor",typeof(System.Drawing.Color));
			}
			catch
			{
				this._ForceColor = Color.Black;
			}
			try
			{
				_Sides=(DevExpress.XtraPrinting.BorderSide)info.GetValue("_Sides",typeof(DevExpress.XtraPrinting.BorderSide));
			}
			catch
			{
				this._Sides = DevExpress.XtraPrinting.BorderSide.All;
			}
			try
			{
				_BorderColor=(System.Drawing.Color)info.GetValue("_BorderColor",typeof(System.Drawing.Color));
			}
			catch
			{
				this._BorderColor = Color.Black;
			}
			try
			{
				_BorderWidth=info.GetInt32("_BorderWidth");
			}
			catch
			{
				_BorderWidth=1;
			}
			try
			{
				_Font=(System.Drawing.Font)info.GetValue("_Font",typeof(System.Drawing.Font));
			}
			catch
			{
				this._Font = new Font(AppearanceObject.DefaultFont.FontFamily, 10f);
			}
			try
			{
				_StringFormat=(System.Drawing.StringFormatFlags)info.GetValue("_StringFormat",typeof(System.Drawing.StringFormatFlags));
			}
			catch
			{
				this._StringFormat = 0;
			}
			try
			{
				_StringTrimming=(System.Drawing.StringTrimming)info.GetValue("_StringTrimming",typeof(System.Drawing.StringTrimming));
			}
			catch
			{
				this._StringTrimming = StringTrimming.None;
			}
			try
			{
				_Width=info.GetInt32("_Width");
			}
			catch
			{
				_Width=0;
			}
			try
			{
				_Height=info.GetInt32("_Height");
			}
			catch
			{
				_Height=0;
			}
			try
			{
				_HorzAlignment=(DevExpress.Utils.HorzAlignment)info.GetValue("_HorzAlignment",typeof(DevExpress.Utils.HorzAlignment));
			}
			catch
			{
				this._HorzAlignment = HorzAlignment.Center;
			}
			try
			{
				_VertAlignment=(DevExpress.Utils.VertAlignment)info.GetValue("_VertAlignment",typeof(DevExpress.Utils.VertAlignment));
			}
			catch
			{
				this._VertAlignment = VertAlignment.Center;
			}
			try
			{
				_BorderStyle=(DevExpress.XtraPrinting.BrickBorderStyle)info.GetValue("_BorderStyle",typeof(DevExpress.XtraPrinting.BrickBorderStyle));
			}
			catch
			{
				this._BorderStyle = BrickBorderStyle.Center;
			}
//			try
//			{
//				_Radius=info.GetSingle("_Radius");
//			}
//			catch
//			{
//				this._Radius = 0;
//			}
		}
		#endregion
	}

	#region public class NewCollection
	/*Descrition:   */
	[Serializable]
	public class BasicStyleCollection : CollectionBase
	{
		//Wu.Country@2007-11-30 11:27:45 AM added this collection.
		//Fields
		//Properties
		public BasicStyle this[int i_Index]
		{
			get { return this.InnerList[i_Index] as BasicStyle; }
			set { this.InnerList[i_Index] = value; }
		}
		//ctor
		public BasicStyleCollection() { }
		//Methods
		public int Add(BasicStyle i_Object)
		{
			return this.InnerList.Add(i_Object);
		}
		public void Remove(BasicStyle i_Obj)
		{
			this.InnerList.Remove(i_Obj);
		}
	}
	#endregion

	#endregion

	#region public class WebbTableCell
	/*Descrition:   */
	[Serializable, ComVisible(true)]
	public class WebbTableCell : IWebbTableCell
	{
		//Wu.Country@2007-11-12 01:57 PM added this class.
		//Fields
		private MergeTypes _MergeType;
		private BasicStyle _CellStyle;
		private string _ImagePath;
		private Image _Image;	//12-20-2007
		private string _Text;
		private EventArgs _ClickEventArg;
		private PictureBoxSizeMode _ImageSizeMode;
		private object _Tag;	//07-24-2008@Scott

		//Properties
		public object Tag
		{
			get { return this._Tag; }
			set { this._Tag = value; }
		}
		public PictureBoxSizeMode ImageSizeMode
		{
			get { return this._ImageSizeMode; }
			set { this._ImageSizeMode = value; }
		}
		public EventArgs ClickEventArg
		{
			get { return this._ClickEventArg; }
			set
			{
				if (Webb.Reports.DataProvider.VideoPlayBackManager.ClickEvent)	//06-18-2008@Scott
				{
					this._ClickEventArg = value;
				}
			}
		}

		//ctor
		public WebbTableCell()
		{
			this._MergeType = MergeTypes.None;
			this._CellStyle = new BasicStyle();
		}
		//Methods

        public WebbTableCell(IWebbTableCell cell)
        {
            if (cell == null) return;

            this._MergeType = cell.MergeType;

            this._Text = cell.Text;

            this._CellStyle = new BasicStyle();

            if (cell.CellStyle != null)
            {
                this._CellStyle.SetStyle(cell.CellStyle);

                this._CellStyle.Width = cell.CellStyle.Width;

                this._CellStyle.Height = cell.CellStyle.Height;                
                
            }

            if (cell.Image != null) this._Image = (Image)cell.Image.Clone();

            this.ImagePath = cell.ImagePath;

            this.ImageSizeMode = cell.ImageSizeMode;
           

            if (cell is WebbTableCell)
            {
                this.Tag = (cell as WebbTableCell).Tag;
                this.ClickEventArg = (cell as WebbTableCell).ClickEventArg;
            }
        }

        public IWebbTableCell Copy()
        {
            return new WebbTableCell(this);
        }

		public void ClickHandler(object i_Sender, EventArgs i_Args)
		{
			Webb.Reports.DataProvider.VideoPlayBackManager.OnClickEvent(this, this._ClickEventArg);
		}

		#region IWebbTableCell Members
		public Webb.Reports.ExControls.MergeTypes MergeType
		{
			get
			{
				// TODO:  Add WebbTableCell.MergeType getter implementation
				return this._MergeType;
			}
			set
			{
				// TODO:  Add WebbTableCell.MergeType setter implementation
				this._MergeType = value;
			}
		}

		public IBasicStyle CellStyle
		{
			get
			{
                if (_CellStyle == null) _CellStyle = new BasicStyle();
				// TODO:  Add WebbTableCell.CellStyle getter implementation
				return this._CellStyle;
			}
			set
			{
				// TODO:  Add WebbTableCell.CellStyle setter implementation
				this._CellStyle = value as BasicStyle;
			}
		}

		public string ImagePath
		{
			get
			{
                if(_ImagePath==null)_ImagePath=string.Empty;
				// TODO:  Add WebbTableCell.ImagePath getter implementation
				return this._ImagePath;
			}
			set
			{
				// TODO:  Add WebbTableCell.ImagePath setter implementation
				if (!System.IO.File.Exists(value))
				{
					return;
				}
				this._ImagePath = value;			
			}
		}

		public Image Image
		{
			get { return this._Image; }
			set { this._Image = value; }
		}

		public string Text
		{
			get
			{
				// TODO:  Add WebbTableCell.Text getter implementation
				return this._Text;
			}
			set
			{
				// TODO:  Add WebbTableCell.Text setter implementation
				this._Text = value;
			}
		}
		#endregion

		#region IBasePrintable Members

		public void Initialize(IPrintingSystem ps, ILink link)
		{
			// TODO:  Add WebbTableCell.Initialize implementation

		}

		public void CreateArea(string areaName, IBrickGraphics graph)
		{
			// TODO:  Add WebbTableCell.CreateArea implementation
			TextBrick m_Brick = this._CellStyle.CreateBrick();
			m_Brick.Text = this._Text;
			graph.DrawBrick(m_Brick, new RectangleF(0, 0, this._CellStyle.Width, this._CellStyle.Height));
		}

		public void DrawCell(IBrickGraphics graph, int i_x, int i_y, TextBrick i_Brick)
		{
			graph.DrawBrick(i_Brick, new RectangleF(i_x, i_y, this._CellStyle.Width, this._CellStyle.Height));
		}

		public void Finalize(IPrintingSystem ps, ILink link)
		{
			// TODO:  Add WebbTableCell.Finalize implementation
		}

		#endregion

		#region Create/Update Brick
		public TextBrick CreateTextBrick()
		{
			TextBrick m_Brick = this._CellStyle.CreateBrick();
			if (this._ImagePath != null && this._ImagePath != string.Empty)
			{
				m_Brick.BackColor = Color.Transparent;
			}
			return m_Brick;
		}

		public TextBrick CreateShadowBrick()	//Scott@12082008
		{
			TextBrick i_Brick = new TextBrick();
			i_Brick.BackColor = Color.LightGray;
//			i_Brick.BorderColor = this.CellStyle.BorderColor;
//			i_Brick.BorderWidth = this.CellStyle.BorderWidth;
//			i_Brick.Font = this.CellStyle.Font;
			i_Brick.ForeColor = this.CellStyle.ForeColor;

			i_Brick.Sides = DevExpress.XtraPrinting.BorderSide.None;
//			i_Brick.StringFormat = new BrickStringFormat(this.CellStyle.StringFormat);
//			i_Brick.HorzAlignment = this.CellStyle.HorzAlignment;//HorzAlignment.Center;
//			i_Brick.VertAlignment = this.CellStyle.VertAlignment;//VertAlignment.Center;
			i_Brick.BorderStyle = this.CellStyle.BorderStyle;
//			i_Brick.Style.Radius = this.CellStyle.Radius;

//			TextBrick m_Brick = new TextBrick();
//			m_Brick.BackColor = Color.LightGray;
//			m_Brick.Sides = DevExpress.XtraPrinting.BorderSide.None;
//			m_Brick.Style.Radius = this.CellStyle.Radius;
			return i_Brick;
		}

		public void UpdateBrick(TextBrick i_Brick)
		{
			this._CellStyle.UpdateBrick(i_Brick);
		}

		public ImageBrick CreateImageBrick()
		{
			return this._CellStyle.CreateImageBrick(/*this._ImagePath*/this._Image, this._ImageSizeMode);
		}
        public ImageBrick CreateImageBrickByPath()
        {
            return this._CellStyle.CreateImageBrick(this._ImagePath, this._ImageSizeMode);
        }
		#endregion

		public void PaintBorders(PaintEventArgs e, int x, int y, int width, int height)
		{//Paint Borders
			e.Graphics.FillRectangle(new SolidBrush(this._CellStyle.BackgroundColor), x, y, width, height);

			if (this._CellStyle.Sides == DevExpress.XtraPrinting.BorderSide.None) return;

			Pen borderPen=new Pen(this._CellStyle.BorderColor, this._CellStyle.BorderWidth);		

			RectangleF borderRect=new RectangleF();
			
			borderRect.X=x;

            borderRect.Y=y;

            borderRect.Width=width;

            borderRect.Height=height;

			switch(this._CellStyle.BorderStyle)
			{
				case BrickBorderStyle.Center:					 
				default:					
					break;
				case BrickBorderStyle.Inset:
                    borderRect.Inflate(-this._CellStyle.BorderWidth/2f,-this._CellStyle.BorderWidth/2f);
					break;
				case BrickBorderStyle.Outset: 
					borderRect.Inflate(this._CellStyle.BorderWidth/2f,this._CellStyle.BorderWidth/2f);
					break;					  
			}
			#region New codes
			//draw selected borders 
			if ((this._CellStyle.Sides & DevExpress.XtraPrinting.BorderSide.Left) != 0)
			{			
				e.Graphics.DrawLine(borderPen, borderRect.X, borderRect.Y, borderRect.X, borderRect.Y + borderRect.Height);
			}
			if ((this._CellStyle.Sides & DevExpress.XtraPrinting.BorderSide.Right) != 0)
			{
				e.Graphics.DrawLine(borderPen, borderRect.X + borderRect.Width, borderRect.Y,borderRect.X + borderRect.Width,borderRect.Y + borderRect.Height);
			}
			if ((this._CellStyle.Sides & DevExpress.XtraPrinting.BorderSide.Top) != 0)
			{
				e.Graphics.DrawLine(borderPen, borderRect.X, borderRect.Y, borderRect.X+ borderRect.Width, borderRect.Y);
			}
			if ((this._CellStyle.Sides & DevExpress.XtraPrinting.BorderSide.Bottom) != 0)
			{
				e.Graphics.DrawLine(borderPen, borderRect.X, borderRect.Y + borderRect.Height, borderRect.X + borderRect.Width,borderRect.Y + borderRect.Height);
			}
			#endregion
			
			#region last Codes
			//			//draw selected borders 
			//			if ((this._CellStyle.Sides & DevExpress.XtraPrinting.BorderSide.Left) != 0)
			//			{			
			//				e.Graphics.DrawLine(borderPen, x, y, x, y + height);
			//			}
			//			if ((this._CellStyle.Sides & DevExpress.XtraPrinting.BorderSide.Right) != 0)
			//			{
			//				e.Graphics.DrawLine(borderPen, x + width, y, x + width, y + height);
			//			}
			//			if ((this._CellStyle.Sides & DevExpress.XtraPrinting.BorderSide.Top) != 0)
			//			{
			//				e.Graphics.DrawLine(borderPen, x, y, x + width, y);
			//			}
			//			if ((this._CellStyle.Sides & DevExpress.XtraPrinting.BorderSide.Bottom) != 0)
			//			{
			//				e.Graphics.DrawLine(borderPen, x, y + height, x + width, y + height);
			//			}
			#endregion
			
		    
		}


		public void DrawBorderContent(IGraphics gr, RectangleF rectf)
		{//Paint Borders	

			if (this._CellStyle.Sides == DevExpress.XtraPrinting.BorderSide.None) return;

			Pen borderPen=new Pen(this._CellStyle.BorderColor, this._CellStyle.BorderWidth);		

			RectangleF borderRect=new RectangleF();
			
			borderRect.X=rectf.X;

			borderRect.Y=rectf.Y;

			borderRect.Width=rectf.Width;

			borderRect.Height=rectf.Height-1;

			switch(this._CellStyle.BorderStyle)
			{
				case BrickBorderStyle.Center:					 
				default:					
					break;
				case BrickBorderStyle.Inset:
					borderRect.Inflate(-this._CellStyle.BorderWidth/2f,-this._CellStyle.BorderWidth/2f);
					break;
				case BrickBorderStyle.Outset: 
					borderRect.Inflate(this._CellStyle.BorderWidth/2f,this._CellStyle.BorderWidth/2f);
					break;					  
			}
			#region New codes
			//draw selected borders 
			if ((this._CellStyle.Sides & DevExpress.XtraPrinting.BorderSide.Left) != 0)
			{			
				gr.DrawLine(borderPen, borderRect.X, borderRect.Y, borderRect.X, borderRect.Y + borderRect.Height);
			}
			if ((this._CellStyle.Sides & DevExpress.XtraPrinting.BorderSide.Right) != 0)
			{
				gr.DrawLine(borderPen, borderRect.X + borderRect.Width, borderRect.Y,borderRect.X + borderRect.Width,borderRect.Y + borderRect.Height);
			}
			if ((this._CellStyle.Sides & DevExpress.XtraPrinting.BorderSide.Top) != 0)
			{
				gr.DrawLine(borderPen, borderRect.X, borderRect.Y, borderRect.X+ borderRect.Width, borderRect.Y);
			}
			if ((this._CellStyle.Sides & DevExpress.XtraPrinting.BorderSide.Bottom) != 0)
			{
				gr.DrawLine(borderPen, borderRect.X, borderRect.Y + borderRect.Height, borderRect.X + borderRect.Width,borderRect.Y + borderRect.Height);
			}
			#endregion		    
		}


		//Modified at 2008-12-17 9:59:33@Scott
		public void FillRectangleArc(Graphics gr,Brush brush,RectangleF bounds,float Radius)
		{
			float R = Radius;

			lock(brush)
			{
				float x=bounds.X;
				float y=bounds.Y;
				float width=bounds.Width;
				float height=bounds.Height;
				System.Drawing.Drawing2D.GraphicsPath gp =new GraphicsPath(); 
				float [,] xy =new float[4,2];
				xy[0,0]=x+width-R;
				xy[0,1]=y+height-R;
				xy[1,0]=x;
				xy[1,1]=y+height-R;
				xy[2,0]=x;
				xy[2,1]=y;
				xy[3,0]=x+width-R;
				xy[3,1]=y;
				for(int i=0;i<4;i++)
				{
					gp.AddArc(xy[i,0],xy[i,1],R,R,90*i,90);
				}
				gp.CloseFigure();
				gr.CompositingQuality=CompositingQuality.HighQuality;
				gr.FillPath(brush,gp);
				gp.Dispose();
				gr.CompositingQuality =CompositingQuality.HighQuality;
			}    
		}

		//Modified at 2008-12-16 14:31:12@Scott
		public void ShowRAP(Graphics gr,Brush brushs,float x,float y,float radues,float ply,float startAngle,float sweepAngle)
		{
			#region set parms
			System.Drawing.Drawing2D.GraphicsPath gp =new GraphicsPath();
			float r=radues/2;
			float []zx=new float [8];
			float []zy=new float [8];
			zx[0] = x+ radues;
			zy[0] = y +r;

			zx[1] =x+r;
			zy[1]=y+radues;

			zx[2]=x;
			zy[2]=y+r;

			zx[3]=x+r;
			zy[3]=y;
            
			zx[4]=zx[0]-ply;
			zy[4]=zy[0];

			zx[5]=zx[1];
			zy[5]=zy[1]-ply;

			zx[6]=zx[2]+ply;
			zy[6]=zy[2];
             
			zx[7]=zx[3];
			zy[7]=zy[3]+ply;
			#endregion
			#region select lines
			switch(Convert.ToInt32(startAngle))
			{
				case 0:
					gp.AddLine(zx[0],zy[0],zx[4],zy[4]);
					gp.AddLine(zx[1],zy[1],zx[5],zy[5]);
					break;
				case 90:
					gp.AddLine(zx[1],zy[1],zx[5],zy[5]);
					gp.AddLine(zx[2],zy[2],zx[6],zy[6]);
					break;
				case 180:
					gp.AddLine(zx[2],zy[2],zx[6],zy[6]);
					gp.AddLine(zx[3],zy[3],zx[7],zy[7]);
					break;
				case 270:
					gp.AddLine(zx[3],zy[3],zx[7],zy[7]);
					gp.AddLine(zx[0],zy[0],zx[4],zy[4]);
					break;
			}
			#endregion
			gp.AddArc(x,y,radues,radues,startAngle,sweepAngle);
			gp.AddArc(x+ply,y+ply,2*(r-ply),2*(r-ply),startAngle,sweepAngle);
			gp.CloseFigure();
			gr.SmoothingMode=SmoothingMode.AntiAlias;
			gr.FillPath(new SolidBrush(Color.Black),gp);
			gr.SmoothingMode=SmoothingMode.Default;
			gp.Dispose();
		}

		public StringFormat RealStringFormat
		{//02-02-2008@Scott
			get
			{
				StringFormat sf = new StringFormat();

				sf.FormatFlags = this.CellStyle.StringFormat;

				sf.Trimming = this.CellStyle.StringTrimming;

				switch (this.CellStyle.HorzAlignment)
				{
					case HorzAlignment.Center:
						sf.Alignment = StringAlignment.Center;
						break;
					case HorzAlignment.Near:
					case HorzAlignment.Default:
					default:
						sf.Alignment = StringAlignment.Near;
						break;
					case HorzAlignment.Far:
						sf.Alignment = StringAlignment.Far;
						break;
				}

				switch (this.CellStyle.VertAlignment)
				{
					case VertAlignment.Center:
						sf.LineAlignment = StringAlignment.Center;
						break;
					case VertAlignment.Top:
					case VertAlignment.Default:
					default:
						sf.LineAlignment = StringAlignment.Near;
						break;
					case VertAlignment.Bottom:
						sf.LineAlignment = StringAlignment.Far;
						break;
				}

				return sf;
			}
		}

		public void CreateChartImage()	//07-24-2008@Scott
		{
			if (this._Tag is Data.ChartBase)
			{
				//				Image image = new Bitmap(this._CellStyle.Width,this._CellStyle.Height);
				//
				//				Graphics gr = Graphics.FromImage(image);
				//
				//				(this._Tag as Data.ChartBase).Draw(gr,new Rectangle(0,0,this._CellStyle.Width,this._CellStyle.Height),null);
				//
				//				this.Image = image;
				//
				//				this.ImageSizeMode = PictureBoxSizeMode.StretchImage;
			}
		}

		//Modified at 2009-1-7 13:45:40@Scott
		public void DrawContent(IGraphics gr, RectangleF rectf)
		{
			gr.FillRectangle(new SolidBrush(this.CellStyle.BackgroundColor),rectf);

			gr.DrawString(this.Text,this.CellStyle.Font,new SolidBrush(this._CellStyle.ForeColor),rectf,this.RealStringFormat);
		}

        //Modified at 2009-1-7 13:45:40@Scott
        public void DrawFileNameContent(IGraphics gr, RectangleF rectf)
        {
            gr.FillRectangle(new SolidBrush(Color.Pink), rectf);

            gr.DrawString(this.Text, this.CellStyle.Font, new SolidBrush(this._CellStyle.ForeColor), rectf, this.RealStringFormat);
        }

		//Modified at 2009-1-7 13:45:40@Scott
		public void DrawShadowContent(IGraphics gr, RectangleF rectf)
		{
			gr.FillRectangle(Brushes.LightGray,rectf);
		}

		public void PaintCell(PaintEventArgs e, int x, int y)
		{
			if(this._CellStyle.Width<=0||this._CellStyle.Height<=0)	return;  //Added this code at 2008-12-4 8:58:53@Simon

			this.PaintBorders(e, x, y, this._CellStyle.Width, this._CellStyle.Height);

			this.CreateChartImage();

			if (this._Image != null)	//12-20-2007@Scott
			{
				switch (this.ImageSizeMode)
				{
					case PictureBoxSizeMode.StretchImage:
						e.Graphics.DrawImage(this._Image,new Rectangle(x+1, y+1, this._CellStyle.Width-1, this._CellStyle.Height-1));
						break;
					default:
						e.Graphics.DrawImage(this._Image, new Rectangle(x+1, y+1, _Image.Width-1, _Image.Height-1),
							new Rectangle(0, 0, _Image.Width, _Image.Height), System.Drawing.GraphicsUnit.Pixel);
						break;
				}
			}
            else if (this.ImagePath != string.Empty&&System.IO.File.Exists(this.ImagePath))	//12-20-2007@Scott
            {
                Image image = Webb.Utility.ReadImageFromPath(this.ImagePath);

                switch (this.ImageSizeMode)
                {
                    case PictureBoxSizeMode.StretchImage:
                        e.Graphics.DrawImage(image, new Rectangle(x + 1, y + 1, this._CellStyle.Width - 1, this._CellStyle.Height - 1));
                        break;
                    default:
                        e.Graphics.DrawImage(image, new Rectangle(x + 1, y + 1, image.Width - 1, image.Height - 1),
                            new Rectangle(0, 0,image.Width, image.Height), System.Drawing.GraphicsUnit.Pixel);
                        break;
                }
                image.Dispose();

                image = null;
            }
			else
			{
				Rectangle rect=new Rectangle(x, y, this._CellStyle.Width, this._CellStyle.Height);

				if((this.RealStringFormat.FormatFlags&StringFormatFlags.DirectionVertical)>0)
				{
					StringFormat styleFormat=(StringFormat)this.RealStringFormat.Clone();  
					styleFormat.FormatFlags&=(~StringFormatFlags.DirectionVertical);
					styleFormat.Alignment=StringAlignment.Near;
					styleFormat.LineAlignment=StringAlignment.Center;				
					e.Graphics.TranslateTransform(rect.X,rect.Bottom);
					e.Graphics.RotateTransform(-90f);
					RectangleF newRect=new RectangleF(0,0,rect.Height,rect.Width);
					e.Graphics.DrawString(this._Text,this._CellStyle.Font, new SolidBrush(this._CellStyle.ForeColor), newRect, styleFormat);
					e.Graphics.ResetTransform();		

				}
				else
				{
					e.Graphics.DrawString(this._Text, this._CellStyle.Font, new SolidBrush(this._CellStyle.ForeColor), rect, this.RealStringFormat);
				}
			}
		}

		//Scott@12082008
		public void PaintShadowCell(PaintEventArgs e, int x, int y)
		{
			if(this._CellStyle.Width<=0||this._CellStyle.Height<=0)	return;  //Added this code at 2008-12-4 8:58:53@Simon

//			if(this._CellStyle.Radius > 0)
//			{//Modified at 2008-12-17 11:15:09@Scott
//				RectangleF rect = new RectangleF(x,y,this._CellStyle.Width,this._CellStyle.Height);
//
//				float R = this.CellStyle.Radius*96/300;
//
//				float borderWidth = (float)(this.CellStyle.BorderWidth);
//
//				float cr=(this._CellStyle.Width<this._CellStyle.Height?this._CellStyle.Width:this._CellStyle.Height)/2-borderWidth-1;
//			
//				if(R>cr)
//				{
//					R=cr;
//				}
//
//				R=(R+borderWidth)*2+1;
//
//				this.FillRectangleArc(e.Graphics,Brushes.LightGray,new RectangleF(x, y, this._CellStyle.Width, this._CellStyle.Height),R);
//			}
//			else
//			{
				e.Graphics.FillRectangle(Brushes.LightGray,new Rectangle(x, y, this._CellStyle.Width, this._CellStyle.Height));
//			}
		}

		public void PaintCell(PaintEventArgs e, int x, int y, int width, int height)
		{
			if(width<=0||height<=0)	return;  //Added this code at 2008-12-4 8:58:53@Simon

			this.CreateChartImage();

			this.PaintBorders(e, x, y, width, height);

			if (this._Image != null)	//12-20-2007@Scott
			{
                e.Graphics.DrawImage(this._Image, x + 1, y + 1, width-1, height-1);
			}
            else if (this.ImagePath != string.Empty && System.IO.File.Exists(this.ImagePath))	//12-20-2007@Scott
            {
                Image image = Webb.Utility.ReadImageFromPath(this.ImagePath);

                switch (this.ImageSizeMode)
                {
                    case PictureBoxSizeMode.StretchImage:
                        e.Graphics.DrawImage(image, new Rectangle(x + 1, y + 1, width - 1, height - 1));
                        break;
                    default:
                        e.Graphics.DrawImage(image, new Rectangle(x + 1, y + 1, image.Width - 1, image.Height - 1),
                            new Rectangle(0, 0, image.Width, image.Height), System.Drawing.GraphicsUnit.Pixel);
                        break;
                }
                image.Dispose();

                image = null;
            }
            else
            {
                Rectangle rect = new Rectangle(x, y, width, height);
                if ((this.RealStringFormat.FormatFlags & StringFormatFlags.DirectionVertical) > 0)
                {
                    StringFormat styleFormat = (StringFormat)this.RealStringFormat.Clone();
                    styleFormat.FormatFlags &= (~StringFormatFlags.DirectionVertical);
                    styleFormat.Alignment = StringAlignment.Near;
                    styleFormat.LineAlignment = StringAlignment.Center;
                    e.Graphics.TranslateTransform(rect.X, rect.Bottom);
                    e.Graphics.RotateTransform(-90f);
                    RectangleF newRect = new RectangleF(0, 0, rect.Height, rect.Width);
                    e.Graphics.DrawString(this._Text, this._CellStyle.Font, new SolidBrush(this._CellStyle.ForeColor), newRect, styleFormat);
                    e.Graphics.ResetTransform();

                }
                else
                {
                    e.Graphics.DrawString(this._Text, this._CellStyle.Font, new SolidBrush(this._CellStyle.ForeColor), new Rectangle(x, y, width, height), this.RealStringFormat);
                }
            }
		}

		//Scott@12082008
		public void PaintShadowCell(PaintEventArgs e, int x, int y, int width, int height)
		{
			if(width<=0||height<=0)	return;  //Added this code at 2008-12-4 8:58:53@Simon

//			if(this._CellStyle.Radius > 0)
//			{//Modified at 2008-12-17 11:14:58@Scott
//				RectangleF rect = new RectangleF(x,y,width,height);
//
//				float R = this.CellStyle.Radius*96/300;
//
//				float borderWidth = (float)(this.CellStyle.BorderWidth);
//
//				float cr=(width<height?width:height)/2-borderWidth-1;
//			
//				if(R>cr)
//				{
//					R=cr;
//				}
//
//				R=(R+borderWidth)*2+1;
//
//				this.FillRectangleArc(e.Graphics,Brushes.LightGray,new RectangleF(x, y, width, height),R);
//			}
//			else
//			{
				e.Graphics.FillRectangle(Brushes.LightGray,new Rectangle(x, y, width, height));
//			}
		}

		#region Serialization By Macro 2008-12-15 9:42:40
		public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			info.AddValue("_MergeType",_MergeType,typeof(Webb.Reports.ExControls.MergeTypes));
			info.AddValue("_CellStyle",_CellStyle,typeof(Webb.Reports.ExControls.BasicStyle));
			info.AddValue("_ImagePath",_ImagePath);
			info.AddValue("_Image",_Image,typeof(System.Drawing.Image));
			info.AddValue("_Text",_Text);
			info.AddValue("_ClickEventArg",_ClickEventArg,typeof(System.EventArgs));
			info.AddValue("_ImageSizeMode",_ImageSizeMode,typeof(System.Windows.Forms.PictureBoxSizeMode));
			info.AddValue("_Tag",_Tag);
		}

		public WebbTableCell(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			try
			{
				_MergeType=(Webb.Reports.ExControls.MergeTypes)info.GetValue("_MergeType",typeof(Webb.Reports.ExControls.MergeTypes));
			}
			catch
			{
				this._MergeType = MergeTypes.None;
			}
			try
			{
				_CellStyle=(Webb.Reports.ExControls.BasicStyle)info.GetValue("_CellStyle",typeof(Webb.Reports.ExControls.BasicStyle));
			}
			catch
			{
				this._CellStyle = new BasicStyle();
			}
			try
			{
				_ImagePath=info.GetString("_ImagePath");
			}
			catch
			{
				_ImagePath = string.Empty;
			}
			try
			{
				_Image=(System.Drawing.Image)info.GetValue("_Image",typeof(System.Drawing.Image));
			}
			catch
			{
				this._Image = null;
			}
			try
			{
				_Text=info.GetString("_Text");
			}
			catch
			{
				_Text=string.Empty;
			}
			try
			{
				_ClickEventArg=(System.EventArgs)info.GetValue("_ClickEventArg",typeof(System.EventArgs));
			}
			catch
			{
				this._ClickEventArg = null;
			}
			try
			{
				_ImageSizeMode=(System.Windows.Forms.PictureBoxSizeMode)info.GetValue("_ImageSizeMode",typeof(System.Windows.Forms.PictureBoxSizeMode));
			}
			catch
			{
				this._ImageSizeMode = System.Windows.Forms.PictureBoxSizeMode.Normal;
			}
			try
			{
				_Tag=info.GetValue("_Tag",typeof(object));
			}
			catch
			{
				_Tag=null;
			}
		}
		#endregion
	}
	#endregion

	#region public class Webbtable : IWebbTable
	/*Descrition:   */
	[Serializable]
	public class WebbTable : IWebbTable, IDisposable
	{
		//Wu.Country@2007-11-12 01:58 PM added this class.
		//Fields
		protected WebbTableCell[,] _Cells;
		protected int _Rows = -1;
		protected int _Columns = -1;
		protected BasicStyle _TableStyle;
		private object _SycObject;
		protected Size _Offset;
		protected int _HeightPerPage;			//05-16-2008@Scott
		protected int _ReportHeaderHeight;		//07-03-2008@Scott
		protected int _ReportFooterHeight;		//07-03-2008@Scott

		protected bool _RepeatedHeader = true;	// 07/25/2008@simon
		protected int _HeaderCount = 1;			// 07/25/2008@simon  
   
		[NonSerialized]
		public ExControl ExControl;  //2009-12-29 9:43:22@Simon Add this Code
		

		public int HeightPerPage
		{
			get { return this._HeightPerPage; }
			set { this._HeightPerPage = value; }
		}

		public int ReportWidth  //2009-12-29 9:58:43@Simon Add this Code
		{
			get
			{
				if(this.ExControl==null||ExControl.Report==null)return 0;

			  	int nWidth = ExControl.Report.PageWidth -  ExControl.Report.Margins.Left -  ExControl.Report.Margins.Right;

				return (int)(nWidth/Webb.Utility.ConvertCoordinate) ;
			}
		}

		public int ReportHeaderHeight
		{
			get { return this._ReportHeaderHeight; }
			set { this._ReportHeaderHeight = value; }
		}

		public int ReportFooterHeight
		{
			get { return this._ReportFooterHeight; }
			set { this._ReportFooterHeight = value; }
		}
		public bool RepeatedHeader
		{
			get { return this._RepeatedHeader; }
			set { this._RepeatedHeader = value; }
		}
		public int HeaderCount
		{
			get { return this._HeaderCount; }
			set { this._HeaderCount = value; }
		}

		//ctor
		public WebbTable()
			: this(1, 1)
		{

		}

		public WebbTable(int i_Rows, int i_Column)
		{
			this.InitializeTable();
			this.InitializeCells(i_Rows, i_Column);
		}

		private void InitializeTable()
		{
			this._SycObject = new object();
			this._TableStyle = new BasicStyle();
			this._Offset = new Size(0, 0);
		}

		private void InitializeCells(int i_Rows, int i_Columns)
		{
			System.Diagnostics.Trace.Assert(i_Rows > 0 && i_Columns > 0);
			lock (this._SycObject)
			{
				this._Cells = new WebbTableCell[i_Rows, i_Columns];
				//
				for (int m_row = 0; m_row < i_Rows; m_row++)
				{
					for (int m_column = 0; m_column < i_Columns; m_column++)
					{
						//init cell
						this._Cells[m_row, m_column] = new WebbTableCell();
					}
				}
				//
				this._Columns = i_Columns;
				this._Rows = i_Rows;
			}
		}
		//Methods

		#region IWebbTable Members

        public void SplitTable(int wrappedColumn, int HeaderRowsCount,int TopCount)
        {
            if (wrappedColumn < 2 || TopCount <= 0||this._Rows<=0||this._Columns<=0) return;

            int resultrows = HeaderRowsCount + TopCount;

            int resultColumns = this._Columns * wrappedColumn;

            WebbTableCell[,] splitedCells = new WebbTableCell[resultrows, resultColumns];
            
            for (int j = 0; j < wrappedColumn; j++)
            {
                for (int k = 0; k < this._Columns; k++)
                {
                    int colIndex = j * this._Columns + k;

                    for (int i = 0; i < resultrows; i++)
                    {
                        if (i < HeaderRowsCount)
                        {
                            splitedCells[i, colIndex] = new WebbTableCell(this.GetCell(i,k));
                        }
                        else
                        {
                            int indexRowInSource =j*TopCount +i ;

                            splitedCells[i, colIndex] = new WebbTableCell(this.GetCell(indexRowInSource, k));

                        }
                    }
                }
            }

            this._Cells = splitedCells;

            this._Rows = resultrows;

            this._Columns = resultColumns;
        }

		public int InitializeTable(int i_Rows, int i_Columns)
		{
			// TODO:  Add Webbtable.InitializeTable implementation
			this.InitializeCells(i_Rows, i_Columns);
			return 0;
		}
        //Added this code at 2009-3-5 11:04:02@brian
		public IWebbTableCell[,] GetCell()
		{
			return this._Cells;
		}
		//Added this code at 2009-3-5 11:04:07@brian
		public IWebbTableCell GetCell(int i_Row, int i_Column)
		{
			if(i_Row<0||i_Column<0)return null;
			// TODO:  Add Webbtable.GetCell implementation	
			if (i_Row < this._Rows && i_Column < this._Columns)
			{
				return this._Cells[i_Row, i_Column];
			}

			return null;
		}

		public int MergeCells(int i_StartRow, int i_EndRow, int i_StartColumn, int i_EndColumn)
		{
			//Scott@2007-12-27 11:05 modified some of the following code.
			if (i_StartRow > this._Rows - 1 || i_StartColumn > this._Columns - 1) return 0; //04-18-2008@Scott

			int rows = i_EndRow - i_StartRow;
			int cols = i_EndColumn - i_StartColumn;
			System.Diagnostics.Trace.Assert(cols >= 0 && i_EndColumn <= this._Columns);
			System.Diagnostics.Trace.Assert(rows >= 0 && i_EndRow <= this._Rows);

			// TODO:  Add Webbtable.MergeCells implementation			
			for (int i_row = i_StartRow; i_row <= i_EndRow; i_row++)
			{
				if (i_row > this._Rows - 1) break;	//04-18-2008@Scott

				for (int i_col = i_StartColumn; i_col <= i_EndColumn; i_col++)
				{
					if (i_col > this._Columns - 1) break;	//04-18-2008@Scott

					this._Cells[i_row, i_col].MergeType = MergeTypes.Merged;
				}
			}
			this._Cells[i_StartRow, i_StartColumn].MergeType = MergeTypes.None;
			if (rows > 0)
			{
				this._Cells[i_StartRow, i_StartColumn].MergeType |= MergeTypes.Down;

				#region Modified Area
				if (i_EndRow <= this._Rows - 1)
				{
					this._Cells[i_EndRow, i_StartColumn].MergeType |= MergeTypes.End;
				}
				else if(this._Rows - 1 >= 0)
				{
					this._Cells[this._Rows - 1, i_StartColumn].MergeType |= MergeTypes.End;
				}
				#endregion        //Modify at 2008-11-6 14:39:46@Scott
			}
			if (cols > 0)
			{
				this._Cells[i_StartRow, i_StartColumn].MergeType |= MergeTypes.Right;

				#region Modified Area
				if (i_EndColumn <= this._Columns - 1)
				{
					this._Cells[i_StartRow, i_EndColumn].MergeType |= MergeTypes.End;
				}
				else if(this._Columns - 1 >= 0)
				{
					this._Cells[i_StartRow, this._Columns - 1].MergeType |= MergeTypes.End;
				}
				#endregion        //Modify at 2008-11-6 14:39:51@Scott
			}
			return 1;
		}


        public void SetNoBorders()
        {
            for (int i = 0; i < this._Rows; i++)
            {
                for (int j = 0; j < this._Columns; j++)
                {
                    this._Cells[i, j].CellStyle.Sides = DevExpress.XtraPrinting.BorderSide.None;
                }
            }
        }

        public int ShowPreview()
		{
			// TODO:  Add Webbtable.ShowPreview implementation
			DevExpress.XtraPrinting.PrintingSystem m_PS = new PrintingSystem();
			m_PS.Begin();
			this.CreateArea(null, m_PS.Graph);
			m_PS.End();
			m_PS.PreviewFormEx.Show();
			return 0;
		}

		public int Print(int i_PrinterID)
		{
			// TODO:  Add Webbtable.Print implementation
			return 0;
		}

		public int ReSizeTable(int i_Rows, int i_Columns)
		{
			// TODO:  Add Webbtable.ReSizeTable implementation
			if (this._Rows >= i_Rows && this._Columns >= i_Columns)
			{
				this.SimpleResizeTable(i_Rows,i_Columns);
			}
			else
			{
				this.InitializeCells(i_Rows, i_Columns);
			}
			return 0;
		}

		private void SimpleResizeTable(int i_Rows, int i_Columns)
		{
			System.Diagnostics.Trace.Assert(i_Rows > 0 && i_Columns > 0);
			this._Rows = i_Rows;
			this._Columns = i_Columns;
		}

		public int GetColumnWidth(int i_ColumnIndex)
		{
			// TODO: implement
			return this._Cells[0, i_ColumnIndex].CellStyle.Width;
		}

		public int GetRowHight(int i_RowIndex)
		{
			// TODO: implement
			return this._Cells[i_RowIndex, 0].CellStyle.Height;
		}

		public int GetRows()
		{
			// TODO: implement
			return this._Rows;
		}

		public int GetColumns()
		{
			// TODO: implement
			return this._Columns;
		}

		public IBasicStyle GetTableStyle()
		{
			// TODO: implement
			return this._TableStyle;
		}

		public void SetTableStyle(IBasicStyle i_Style)
		{
			// TODO: implement
			this._TableStyle.SetStyle(i_Style);
		}

		public void SetColumnStyle(int i_ColumnIndex, IBasicStyle i_Style)
		{
			// TODO: implement
			if (!(i_Style as BasicStyle).IsEdited() || i_ColumnIndex >= this._Columns) return;

			for (int m_row = 0; m_row < this._Rows; m_row++)
			{
				this._Cells[m_row, i_ColumnIndex].CellStyle.SetStyle(i_Style);
			}
		}
		
		public void SetMatrixColumnStyle(int i_ColumnIndex, IBasicStyle i_Style, Webb.Collections.Int32Collection HeaderRows,bool needChange)
		{
			// TODO: implement
			if (i_ColumnIndex >= this._Columns) return;

			for (int m_row = 0; m_row < this._Rows; m_row++)
			{
				if (HeaderRows.Contains(m_row)) continue;

                this._Cells[m_row, i_ColumnIndex].CellStyle.VertAlignment = i_Style.VertAlignment;

				if(needChange)this._Cells[m_row, i_ColumnIndex].CellStyle.SetStyle(i_Style);
			}

            
		}

      

		public void SetColumnWidth(int i_ColumnIndex,int width)
		{
			// TODO: implement
			if (i_ColumnIndex<0||i_ColumnIndex >= this._Columns) return;

			for (int m_row = 0; m_row < this._Rows; m_row++)
			{
				this._Cells[m_row, i_ColumnIndex].CellStyle.Width=width;
			}
		}

		//Modified at 2009-2-11 15:42:05@Scott
		public void SetColumnStyle(int i_ColumnIndex, IBasicStyle i_Style,bool bColorNeedChange)
		{
			// TODO: implement
			if (!(i_Style as BasicStyle).IsEdited() || i_ColumnIndex >= this._Columns) return;

			for (int m_row = 0; m_row < this._Rows; m_row++)
			{
				this._Cells[m_row, i_ColumnIndex].CellStyle.SetStyle(i_Style,bColorNeedChange);
			}
		}

		//02-25-2008@Scott
		public void SetColumnStyle(int i_ColumnIndex, IBasicStyle i_Style, Webb.Collections.Int32Collection HeaderRows)
		{
			// TODO: implement
			if (!(i_Style as BasicStyle).IsEdited() || i_ColumnIndex >= this._Columns) return;

			for (int m_row = 0; m_row < this._Rows; m_row++)
			{
				if (HeaderRows.Contains(m_row)) continue;

				this._Cells[m_row, i_ColumnIndex].CellStyle.SetStyle(i_Style);
			}
		}

		public void SetColumnStyle(int i_ColumnIndex, IBasicStyle i_Style, Webb.Collections.Int32Collection HeaderRows,bool bColorNeedChange)
		{
			// TODO: implement
			if (!(i_Style as BasicStyle).IsEdited() || i_ColumnIndex >= this._Columns) return;

			for (int m_row = 0; m_row < this._Rows; m_row++)
			{
				if (HeaderRows.Contains(m_row)) continue;

				this._Cells[m_row, i_ColumnIndex].CellStyle.SetStyle(i_Style,bColorNeedChange);
			}
		}

		public void SetRowStyle(int i_RowIndex, IBasicStyle i_Style)
		{
			// TODO: implement
			if (!(i_Style as BasicStyle).IsEdited() || i_RowIndex >= this._Rows) return;

			for (int m_col = 0; m_col < this._Columns; m_col++)
			{
				this._Cells[i_RowIndex, m_col].CellStyle.SetStyle(i_Style);
			}
		}

		public void SetRowStyle(int i_RowIndex, IBasicStyle i_Style, bool bShowRowIndicators)
		{
			// TODO: implement
			if (!(i_Style as BasicStyle).IsEdited() || i_RowIndex >= this._Rows) return;

			int m_col = 0;

			if (bShowRowIndicators) m_col = 1;

			for (; m_col < this._Columns; m_col++)
			{
				this._Cells[i_RowIndex, m_col].CellStyle.SetStyle(i_Style);
			}
		}

        public void SetOverrideRowStyle(int i_RowIndex, IBasicStyle i_Style, bool bShowRowIndicators)
        {
            // TODO: implement
            if (i_RowIndex >= this._Rows) return;

            int m_col = 0;

            if (bShowRowIndicators) m_col = 1;

            for (; m_col < this._Columns; m_col++)
            {
                this._Cells[i_RowIndex, m_col].CellStyle.SetStyle(i_Style);
            }
        }


		public void SetOffset(int i_Left, int i_Top)
		{
			this._Offset.Height = i_Top;
			this._Offset.Width = i_Left;
		}

		public Size Offset
		{
			get { return this._Offset; }
		}
		#endregion

		#region IBasePrintable Members  & Create Area

		public void Initialize(IPrintingSystem ps, ILink link)
		{
			// TODO:  Add Webbtable.Initialize implementation

		}
		public void DrawLine(BrickGraphics graph,float StartX,float StartY,float EndX,float EndY)
		{			
			Pen drawpen=new Pen(Color.Black,1);

			PointF StartPoint=new PointF(StartX,StartY);

	        PointF EndPoint=new PointF(EndX,EndY);
	        
			graph.DrawShape(drawpen,0,StartPoint,EndPoint,Color.Black,Color.Black,1,Brushes.Black,false);				   
		}	


		#region Adjust Position
		public static int MaxAbsPosition(int firstPageHeight,int NormalPageHeight,int m_y)
		{
			if(m_y<firstPageHeight)return firstPageHeight;

			int LefPos=(m_y-firstPageHeight)%NormalPageHeight;

			if(LefPos==0)return m_y;

			return m_y-LefPos+NormalPageHeight;
		}

		public int AdjustFirstCell(int m_y)
		{
			int FirstPageHeight= this.HeightPerPage+2 + (int)(this.ReportFooterHeight/Webb.Utility.ConvertCoordinate);	//07-03-2008@Scott
				
			int  NormalPageHeight= this.HeightPerPage+2 + (int)(this.ReportHeaderHeight/Webb.Utility.ConvertCoordinate) + (int)(this.ReportFooterHeight/Webb.Utility.ConvertCoordinate);	//07-03-2008@Scott
				
			IWebbTableCell	firstcell=this.GetCell(0,0);

			if(firstcell!=null)
			{
				int absLoc=MaxAbsPosition(FirstPageHeight,NormalPageHeight,m_y);

				int cellHeight=firstcell.CellStyle.Height;

				if(m_y+cellHeight-1>absLoc)
				{
					m_y=absLoc+1;				
				}
			}

			return m_y;
		}

		#endregion

		#region New CreateArea
    	public int CreateArea3D(string areaName, IBrickGraphics graph)
		{
		
			// TODO:  Add Webbtable.CreateArea implementation
			this.AdjustBorders();  //2009-3-2 8:40:44@Simon
		
			this.AdjustSize();
			//
			int m_tempRow = 0;
			int m_tempRowsCount = this._Rows;
			int m_x = this._Offset.Width;
			int m_y = this._Offset.Height;
			int m_tempHeight = this._Offset.Height;
			int m_MergedWidth = 0;
			int m_MergedHeight = 0;
			WebbTableCell m_cell = null;
			
			int nBreakTime = 0;
			int nHeightPerPage = this.HeightPerPage;
			bool b_repeat = false;
			int nHeaderHeight = this.Get3DHeaderHeight();
			TextBrick m_shadowBrick = null;	//Scott@12082008

			RectangleF rectF = RectangleF.Empty;	//Scott@12082008

			int nReportWidth=this.ReportWidth;			

			if(nReportWidth>0)
			{
				nReportWidth+=2;
			}
		
			int FirstPageHeight= this.HeightPerPage + (int)(this.ReportFooterHeight/Webb.Utility.ConvertCoordinate);	//07-03-2008@Scott
				
			int  NormalPageHeight= this.HeightPerPage + (int)(this.ReportHeaderHeight/Webb.Utility.ConvertCoordinate) + (int)(this.ReportFooterHeight/Webb.Utility.ConvertCoordinate);	//07-03-2008@Scott
				
			if (this.HeightPerPage != 0)
			{
				if(m_y<FirstPageHeight)
				{
					m_tempHeight =m_y;	//07-07-2008@Scott
				}
				else
				{
					m_tempHeight=(m_y-FirstPageHeight)%NormalPageHeight;
				}
				IWebbTableCell	firstcell=this.GetCell(0,0);

				if(firstcell!=null)
				{
					int absLoc=MaxAbsPosition(FirstPageHeight,NormalPageHeight,m_y);

					int cellHeight=firstcell.CellStyle.Height+ WebbTableCellHelper.nSpace;

					if(m_y+cellHeight-1>absLoc)
					{
						m_y=absLoc+5;

						m_tempHeight=5;
					}
				}
			}
			else
			{
				System.Diagnostics.Debug.WriteLine("Height/Page is zero !");
			}
		  
			Int32Collection breakColumns=this.GetBreakColumns(nReportWidth,m_x,true);
		
			for (int m_row = 0; m_row < this._Rows; m_row++)
			{
				int NextRowCellHeight=0;
		
				if(m_row < this._Rows - 1&&this._Columns>0)
				{
					NextRowCellHeight=this._Cells[m_row+1, 0].CellStyle.Height;
		
					NextRowCellHeight += WebbTableCellHelper.nSpace;
				}	
		
				for (int m_col = 0; m_col < this._Columns; m_col++)
				{
					m_cell = this._Cells[m_row, m_col];
		
					if(Views.GroupView.BreakedWithLine)   //Add at 2009-2-27 9:10:36@Simon
					{   
						if (m_tempHeight+m_cell.CellStyle.Height+WebbTableCellHelper.nSpace+NextRowCellHeight > nHeightPerPage && m_row < m_tempRowsCount - 1 && m_row < this._Rows - 1)
						{
							m_cell.CellStyle.Sides|=DevExpress.XtraPrinting.BorderSide.Bottom;  //Add at 2009-2-27 9:19:57@Simon
						}
		
					}
		
					if ((m_cell.MergeType & MergeTypes.Merged) == MergeTypes.Merged)
					{
						m_x += m_cell.CellStyle.Width + WebbTableCellHelper.nSpace;	//Modified at 2009-1-12 14:02:38@Scott
		
						continue;
					}					
		
					TextBrick m_brick= m_cell.CreateTextBrick();

					m_shadowBrick = m_cell.CreateShadowBrick();	//Scott@12082008
		
					m_brick.Text = m_cell.Text;
		
					if (m_cell.ClickEventArg != null)
					{
						m_brick.MouseDown += new MouseEventHandler(Webb.Reports.DataProvider.VideoPlayBackManager.OnBrickDown);
		
						m_brick.MouseUp += new MouseEventHandler(Webb.Reports.DataProvider.VideoPlayBackManager.OnBrickUp);
		
						m_brick.OnClick += new EventHandler(m_cell.ClickHandler);
		
						m_brick.Url = "http://www.webbelectronics.com/";
					}
		
					if ((m_cell.MergeType & MergeTypes.Right) == MergeTypes.Right || (m_cell.MergeType & MergeTypes.Down) == MergeTypes.Down)	//12-27-2007@Scott
					{
						#region Merged Cells

						this.GetMergedSize3D(m_row, m_col, out m_MergedWidth, out m_MergedHeight,null,null);
										
						if(m_MergedWidth>0&&m_MergedHeight>0)  //Added this code at 2008-12-4 9:12:22@Simon
						{
							if (m_cell.Image != null)	//12-20-2007@Scott
							{
								ImageBrick m_ImageBrick = m_cell.CreateImageBrick();
		
								rectF = new RectangleF(m_x, m_y, m_MergedWidth, m_MergedHeight);                                
		
								rectF.Offset(WebbTableCellHelper.nShadowOffset,WebbTableCellHelper.nShadowOffset);	//Scott@12082008
		
								graph.DrawBrick(m_shadowBrick, rectF);	//Scott@12082008
		
								rectF.Offset(-WebbTableCellHelper.nShadowOffset,-WebbTableCellHelper.nShadowOffset);	//Scott@12082008
		
								graph.DrawBrick(m_ImageBrick, rectF);
							}
                            else if (m_cell.ImagePath != string.Empty)	//12-20-2007@Scott
                            {
                                ImageBrick m_ImageBrick = m_cell.CreateImageBrickByPath();

                                rectF = new RectangleF(m_x, m_y, m_MergedWidth, m_MergedHeight);

                                rectF.Offset(WebbTableCellHelper.nShadowOffset, WebbTableCellHelper.nShadowOffset);	//Scott@12082008

                                graph.DrawBrick(m_shadowBrick, rectF);	//Scott@12082008

                                rectF.Offset(-WebbTableCellHelper.nShadowOffset, -WebbTableCellHelper.nShadowOffset);	//Scott@12082008

                                graph.DrawBrick(m_ImageBrick, rectF);
                            }
							else
							{
								if(breakColumns.Contains(m_col))
								{
									m_x=nReportWidth*(m_x/nReportWidth)+nReportWidth+2;		
								}

								int DispWidth=0;

								Hashtable spllitedWidths=this.SplitMergedProperWidth(m_row,m_col,nReportWidth,m_x,true,m_MergedWidth,out DispWidth);    
    
								foreach(int startKey in spllitedWidths.Keys)
								{
									if(startKey==m_x)
									{
										int width=(int)spllitedWidths[m_x];

										rectF = new RectangleF(m_x, m_y, width, m_MergedHeight);
		
										rectF.Offset(WebbTableCellHelper.nShadowOffset,WebbTableCellHelper.nShadowOffset);	//Scott@12082008
		
										graph.DrawBrick(m_shadowBrick, rectF);	//Scott@12082008
		
										rectF.Offset(-WebbTableCellHelper.nShadowOffset,-WebbTableCellHelper.nShadowOffset);	//Scott@12082008
		
										graph.DrawBrick(m_brick, rectF);
									}
									else
									{
										int width=(int)spllitedWidths[startKey];

										TextBrick leftBick=m_cell.CreateTextBrick();	

										rectF = new RectangleF(startKey, m_y, width, m_MergedHeight);
		
										rectF.Offset(WebbTableCellHelper.nShadowOffset,WebbTableCellHelper.nShadowOffset);	//Scott@12082008
		
								    	TextBrick  shadowBrick = m_cell.CreateShadowBrick();	//Scott@12082008

										graph.DrawBrick(shadowBrick, rectF);	//Scott@12082008
		
										rectF.Offset(-WebbTableCellHelper.nShadowOffset,-WebbTableCellHelper.nShadowOffset);	//Scott@12082008
		
										graph.DrawBrick(leftBick, rectF);
												
									}
								}
								m_x+=DispWidth;

										
							}
						}
						#endregion
					}
					else
					{
						#region  Draw cell

						if(m_cell.CellStyle.Width>0&&m_cell.CellStyle.Height>0)  //Added this code at 2008-12-4 9:12:22@Simon
						{							
							if (m_cell.Image != null)	//12-20-2007@Scott
							{
								ImageBrick m_ImageBrick = m_cell.CreateImageBrick();

                                rectF = new RectangleF(m_x, m_y, m_cell.CellStyle.Width , m_cell.CellStyle.Height);

								rectF.Offset(WebbTableCellHelper.nShadowOffset,WebbTableCellHelper.nShadowOffset);	//Scott@12082008
		
								graph.DrawBrick(m_shadowBrick, rectF);	//Scott@12082008
		
								rectF.Offset(-WebbTableCellHelper.nShadowOffset,-WebbTableCellHelper.nShadowOffset);	//Scott@12082008
		
								graph.DrawBrick(m_ImageBrick, rectF);
							}
                            else if (m_cell.ImagePath != string.Empty)	//12-20-2007@Scott
                            {
                                ImageBrick m_ImageBrick = m_cell.CreateImageBrickByPath();

                                rectF = new RectangleF(m_x, m_y, m_cell.CellStyle.Width, m_cell.CellStyle.Height);

                                rectF.Offset(WebbTableCellHelper.nShadowOffset, WebbTableCellHelper.nShadowOffset);	//Scott@12082008

                                graph.DrawBrick(m_shadowBrick, rectF);	//Scott@12082008

                                rectF.Offset(-WebbTableCellHelper.nShadowOffset, -WebbTableCellHelper.nShadowOffset);	//Scott@12082008

                                graph.DrawBrick(m_ImageBrick, rectF);
                            }

							else
							{
								if(m_col>0&&breakColumns.Contains(m_col))
								{
									m_x=nReportWidth*(m_x/nReportWidth)+nReportWidth+2;											
								}		

								rectF = new RectangleF(m_x, m_y, m_cell.CellStyle.Width, m_cell.CellStyle.Height);
		
								rectF.Offset(WebbTableCellHelper.nShadowOffset,WebbTableCellHelper.nShadowOffset);	//Scott@12082008
		
								graph.DrawBrick(m_shadowBrick, rectF);	//Scott@12082008
		
								rectF.Offset(-WebbTableCellHelper.nShadowOffset,-WebbTableCellHelper.nShadowOffset);	//Scott@12082008
		
								graph.DrawBrick(m_brick, rectF);
							}
						}
						#endregion
					}
							
					m_x += m_cell.CellStyle.Width;
		
					m_x += WebbTableCellHelper.nSpace;	//Scott@12082008
				}
				m_x = this._Offset.Width;
		
				if (m_cell != null)
				{
					m_y += m_cell.CellStyle.Height;
		
					m_y += WebbTableCellHelper.nSpace;	//Scott@12082008
				}
				else
				{
					System.Diagnostics.Debug.WriteLine("Invalidate cell !");
				}
		
				if (this.RepeatedHeader&&!ExControl.printRepeat)
				{							
					#region Print Header perpage
					//go to next page	//05-16-2008@Scott
					if (m_y<FirstPageHeight)
					{//if current page is the first page, remove report header.
						nHeightPerPage = FirstPageHeight;	//07-03-2008@Scott
					}
					else
					{//if current page is not the first page
						nHeightPerPage = NormalPageHeight;	//07-03-2008@Scott
					}	

					//go to next page	//05-16-2008@Scott
					if (nHeightPerPage > 100)
					{		
						m_tempHeight += m_cell.CellStyle.Height;
		
						m_tempHeight += WebbTableCellHelper.nSpace;  //2009-6-5 10:46:13@Simon Add this Code
		
						#region Modify codes at 2009-3-24 16:00:02@Simon
						
						if (m_tempHeight<nHeightPerPage&&m_tempHeight+NextRowCellHeight+10>=nHeightPerPage && m_row < m_tempRowsCount - 1 && m_row < this._Rows - 1)
						{
							m_y = m_y-m_tempHeight+nHeightPerPage+5;

                            (graph as BrickGraphics).PrintingSystem.InsertPageBreak(m_y - 1);  //Add this code at 2010-7-8 11:12:36
																						
							nBreakTime++;	//07-03-2008@Scott						
		
							m_tempHeight =5;
		
							if (RepeatedHeader)  //define whether print the header in next page
							{
								m_tempRow = m_row;
		
								m_row = -1;	//print first row again
		
								m_tempRowsCount += this.HeaderCount;
		
								b_repeat = true;
							}
						}
						else if (m_row>=0&& m_row==HeaderCount-1&& b_repeat)
						{
							m_row = m_tempRow;	//continue to print
		
							b_repeat = false;
						}

						#endregion        //End Modify
					}
					#endregion
				}
						
			}

			return m_y+2;
		}
			
		public int CreateArea(string areaName, IBrickGraphics graph)
		{
			// TODO:  Add Webbtable.CreateArea implementation
			this.AdjustBorders();  //2009-3-2 8:40:44@Simon
		
			this.AdjustSize();
		
			//
			int m_tempRow = 0;
			int m_tempRowsCount = this._Rows;
			int m_x = this._Offset.Width;
			int m_y = this._Offset.Height;
			int m_tempHeight = this._Offset.Height;
			int m_MergedWidth = 0;
			int m_MergedHeight = 0;
			WebbTableCell m_cell = null;
			int nBreakTime = 0;
			int nHeightPerPage = this.HeightPerPage;
			bool b_repeat = false;
			int nHeaderHeight = this.GetHeaderHeight();

			int nReportWidth=this.ReportWidth;

			if(nReportWidth>0)
			{
				nReportWidth+=2;
			}

			Int32Collection breakColumns=this.GetBreakColumns(nReportWidth,m_x,false);
		
			int FirstPageHeight= this.HeightPerPage + (int)(this.ReportFooterHeight/Webb.Utility.ConvertCoordinate);	//07-03-2008@Scott
				
			int  NormalPageHeight= this.HeightPerPage + (int)(this.ReportHeaderHeight/Webb.Utility.ConvertCoordinate) + (int)(this.ReportFooterHeight/Webb.Utility.ConvertCoordinate);	//07-03-2008@Scott
				
			if (this.HeightPerPage != 0&&!ExControl.printRepeat)
			{
				if(m_y<FirstPageHeight)
				{
					m_tempHeight =m_y;	//07-07-2008@Scott
				}
				else
				{
					m_tempHeight=(m_y-FirstPageHeight)%NormalPageHeight;
				}
				IWebbTableCell	firstcell=this.GetCell(0,0);

				if(firstcell!=null)
				{
					int absLoc=MaxAbsPosition(FirstPageHeight,NormalPageHeight,m_y);

					int cellHeight=firstcell.CellStyle.Height;

					if(m_y+cellHeight-1>absLoc)
					{
						m_y=absLoc+5;

						m_tempHeight=5;
					}
				}
			}
			else
			{
				System.Diagnostics.Debug.WriteLine("Height/Page is zero !");
			}	


			for (int m_row = 0; m_row < this._Rows; m_row++)
			{
				int NextRowCellHeight=0;
		
				if(m_row < this._Rows - 1&&this._Columns>0)
				{
					NextRowCellHeight=this._Cells[m_row+1, 0].CellStyle.Height;
				}

				for (int m_col = 0; m_col < this._Columns; m_col++)
				{
					m_cell = this._Cells[m_row, m_col];
		
					if(Views.GroupView.BreakedWithLine)   //Add at 2009-2-27 9:10:36@Simon
					{   
						if (m_tempHeight+m_cell.CellStyle.Height+NextRowCellHeight> nHeightPerPage && m_row < m_tempRowsCount - 1 && m_row < this._Rows - 1&&!ExControl.printRepeat)
						{
							m_cell.CellStyle.Sides|=DevExpress.XtraPrinting.BorderSide.Bottom;  //Add at 2009-2-27 9:19:57@Simon
						}
					}
		
					if ((m_cell.MergeType & MergeTypes.Merged) == MergeTypes.Merged)
					{
						m_x += m_cell.CellStyle.Width;
		
						continue;
					}
											
					TextBrick m_brick = m_cell.CreateTextBrick();
		
					m_brick.Text = m_cell.Text;
		
					//m_brick.Style.Radius = 30;							
		
					if (m_cell.ClickEventArg != null)
					{
						m_brick.MouseDown += new MouseEventHandler(Webb.Reports.DataProvider.VideoPlayBackManager.OnBrickDown);
		
						m_brick.MouseUp += new MouseEventHandler(Webb.Reports.DataProvider.VideoPlayBackManager.OnBrickUp);
		
						m_brick.OnClick += new EventHandler(m_cell.ClickHandler);
		
						m_brick.Url = "http://www.webbelectronics.com/";
					}
		
					if ((m_cell.MergeType & MergeTypes.Right) == MergeTypes.Right || (m_cell.MergeType & MergeTypes.Down) == MergeTypes.Down)	//12-27-2007@Scott
					{
						# region Draw Merged Cells

						this.GetMergedSize(m_row, m_col, out m_MergedWidth, out m_MergedHeight,null,null);
		
						if(m_MergedWidth>0&&m_MergedHeight>0)  //Added this code at 2008-12-4 9:12:22@Simon
						{									
							if (m_cell.Image != null)	//12-20-2007@Scott
							{
								ImageBrick m_ImageBrick = m_cell.CreateImageBrick();

                                graph.DrawBrick(m_ImageBrick, new RectangleF(m_x , m_y , m_MergedWidth , m_MergedHeight));
							}
                            else if (m_cell.ImagePath != string.Empty)	//12-20-2007@Scott
                            {
                                ImageBrick m_ImageBrick = m_cell.CreateImageBrickByPath();
                                graph.DrawBrick(m_ImageBrick, new RectangleF(m_x, m_y, m_MergedWidth, m_MergedHeight));
                            }
							else
							{
								if(breakColumns.Contains(m_col))
								{
									m_x=nReportWidth*(m_x/nReportWidth)+nReportWidth+2;		
								}

								int DispWidth=0;

								Hashtable spllitedWidths=this.SplitMergedProperWidth(m_row,m_col,nReportWidth,m_x,false,m_MergedWidth,out DispWidth);       
    
								foreach(int startKey in spllitedWidths.Keys)
								{
									if(startKey==m_x)
									{
										int width=(int)spllitedWidths[m_x];

										graph.DrawBrick(m_brick, new RectangleF(m_x, m_y, width, m_MergedHeight));
									}
									else
									{
										int width=(int)spllitedWidths[startKey];

										TextBrick leftBick=m_cell.CreateTextBrick();											

										graph.DrawBrick(leftBick, new RectangleF(startKey, m_y, width, m_MergedHeight));
												
									}
								}	
								m_x+=DispWidth;
									
							}
						}

						m_x += m_cell.CellStyle.Width;

						#endregion
					}
					else
					{	
						#region Draw cell

						if(m_cell.CellStyle.Width>0&&m_cell.CellStyle.Height>0)  //Added this code at 2008-12-4 9:12:22@Simon
						{	
							if(m_col>0&&breakColumns.Contains(m_col))
							{
								m_x=nReportWidth*(m_x/nReportWidth)+nReportWidth+2;											
							}									

							if (m_cell.Image != null)	//12-20-2007@Scott
							{
								ImageBrick m_ImageBrick = m_cell.CreateImageBrick();

                                RectangleF rect= new RectangleF(m_x, m_y, m_cell.CellStyle.Width, m_cell.CellStyle.Height);
                            
                                graph.DrawBrick(m_ImageBrick, rect);
                                
							}
                            else if (m_cell.ImagePath != string.Empty)	//12-20-2007@Scott
                            {
                                ImageBrick m_ImageBrick = m_cell.CreateImageBrickByPath();

                                RectangleF rect = new RectangleF(m_x, m_y, m_cell.CellStyle.Width, m_cell.CellStyle.Height);

                                graph.DrawBrick(m_ImageBrick, rect);
                            }
							else
							{
								graph.DrawBrick(m_brick, new RectangleF(m_x, m_y, m_cell.CellStyle.Width, m_cell.CellStyle.Height));
							}									
									
							m_x += m_cell.CellStyle.Width;
						}	

						#endregion							
					}						
							
				}	
		
				m_x = this._Offset.Width;
		
				if (m_cell != null)
				{
					m_y += m_cell.CellStyle.Height;
				}
				else
				{
					System.Diagnostics.Debug.WriteLine("Invalidate cell !");
				}
		
				if (this.RepeatedHeader&& !ExControl.printRepeat )
				{
					if (m_y<FirstPageHeight)
					{//if current page is the first page, remove report header.
						nHeightPerPage = FirstPageHeight;	//07-03-2008@Scott
					}
					else
					{//if current page is not the first page
						nHeightPerPage = NormalPageHeight;	//07-03-2008@Scott
					}	

					//go to next page	//05-16-2008@Scott
					if (nHeightPerPage > 100)
					{
						m_tempHeight += m_cell.CellStyle.Height;
		
						#region Modify codes at 2009-3-24 16:00:02@Simon
						
						if (m_tempHeight<nHeightPerPage&&m_tempHeight+NextRowCellHeight+10>=nHeightPerPage && m_row < m_tempRowsCount - 1 && m_row < this._Rows - 1)
						{
							m_y = m_y-m_tempHeight+nHeightPerPage+5;

                            (graph as BrickGraphics).PrintingSystem.InsertPageBreak(m_y - 1);  //Add this code at 2010-7-8 11:12:08
																						
							nBreakTime++;	//07-03-2008@Scott						
		
							m_tempHeight =5;
		
							if (RepeatedHeader)  //define whether print the header in next page
							{
								m_tempRow = m_row;
		
								m_row = -1;	//print first row again
		
								m_tempRowsCount += this.HeaderCount;
		
								b_repeat = true;
							}
						}
						else if (m_row>=0&& m_row==HeaderCount-1&& b_repeat)        //else if (m_tempHeight == nHeaderHeight && b_repeat)
						{
							m_row = m_tempRow;	//continue to print
		
							b_repeat = false;
						}

						#endregion        //End Modify
					}
				}
			}
			return m_y+2;
		}

		public int CreateAreaWithoutAdjustSize(string areaName, IBrickGraphics graph)
		{
			// TODO:  Add Webbtable.CreateArea implementation
			this.AdjustBorders(); //2009-3-2 9:47:35@Simon

			int m_x = this._Offset.Width;
			int m_y = this._Offset.Height;
			int m_MergedWidth = 0;
			int m_MergedHeight = 0;
			WebbTableCell m_cell = null;
			TextBrick m_brick = null;

			for (int m_row = 0; m_row < this._Rows; m_row++)
			{
				for (int m_col = 0; m_col < this._Columns; m_col++)
				{
					m_cell = this._Cells[m_row, m_col];

					if ((m_cell.MergeType & MergeTypes.Merged) == MergeTypes.Merged)
					{
						m_x += m_cell.CellStyle.Width;

						continue;
					}
							
					m_brick = m_cell.CreateTextBrick();

					m_brick.Text = m_cell.Text;

					if (m_cell.ClickEventArg != null)
					{
						m_brick.MouseDown += new MouseEventHandler(Webb.Reports.DataProvider.VideoPlayBackManager.OnBrickDown);

						m_brick.MouseUp += new MouseEventHandler(Webb.Reports.DataProvider.VideoPlayBackManager.OnBrickUp);

						m_brick.OnClick += new EventHandler(m_cell.ClickHandler);

						m_brick.Url = "http://www.webbelectronics.com/";
					}

					if ((m_cell.MergeType & MergeTypes.Right) == MergeTypes.Right || (m_cell.MergeType & MergeTypes.Down) == MergeTypes.Down)	//12-27-2007@Scott
					{
						this.GetMergedSize(m_row, m_col, out m_MergedWidth, out m_MergedHeight,null,null);
								
						if (m_cell.Image != null)	//12-20-2007@Scott
						{
							ImageBrick m_ImageBrick = m_cell.CreateImageBrick();

							graph.DrawBrick(m_ImageBrick, new RectangleF(m_x, m_y, m_MergedWidth, m_MergedHeight));
						}                   
                        else if (m_cell.ImagePath != string.Empty)	//12-20-2007@Scott
                        {
                            ImageBrick m_ImageBrick = m_cell.CreateImageBrickByPath();

                            graph.DrawBrick(m_ImageBrick, new RectangleF(m_x, m_y, m_MergedWidth, m_MergedHeight));
                        }
						else
						{
							graph.DrawBrick(m_brick, new RectangleF(m_x, m_y, m_MergedWidth, m_MergedHeight));
						}						
					}
					else
					{
						if (m_cell.Image != null)	//12-20-2007@Scott
						{
							ImageBrick m_ImageBrick = m_cell.CreateImageBrick();

                            graph.DrawBrick(m_ImageBrick, new RectangleF(m_x , m_y, m_cell.CellStyle.Width , m_cell.CellStyle.Height));
						}
                        else if (m_cell.ImagePath != string.Empty)	//12-20-2007@Scott
                        {
                            ImageBrick m_ImageBrick = m_cell.CreateImageBrickByPath();

                            graph.DrawBrick(m_ImageBrick, new RectangleF(m_x, m_y, m_cell.CellStyle.Width, m_cell.CellStyle.Height));
                        }
						else
						{
							graph.DrawBrick(m_brick, new RectangleF(m_x, m_y, m_cell.CellStyle.Width, m_cell.CellStyle.Height));
						}						
					}
					m_x += m_cell.CellStyle.Width;
				}
				m_x = this._Offset.Width;

				if (m_cell != null) m_y += m_cell.CellStyle.Height;
			}
			return m_y+1;
		}	

		#endregion

		#region Create Matrix Area for matrixControl				
		public int CreateMatrixArea3D(string areaName, IBrickGraphics graph,int MergedCount)
		{
		
			// TODO:  Add Webbtable.CreateArea implementation
			this.AdjustBorders();  //2009-3-2 8:40:44@Simon
		
			this.AdjustSize();

            ShiftMergCellsIfColumnNotExist();
			//
			int m_tempRow = 0;
			int m_tempRowsCount = this._Rows;
			int m_x = this._Offset.Width;
			int m_y = this._Offset.Height;
			int m_tempHeight = this._Offset.Height;
			int m_MergedWidth = 0;
			int m_MergedHeight = 0;
			WebbTableCell m_cell = null;
			TextBrick m_brick = null;
			int nBreakTime = 0;
			int nHeightPerPage = this.HeightPerPage;
			bool b_repeat = false;
			int nHeaderHeight = this.Get3DHeaderHeight();
			TextBrick m_shadowBrick = null;	//Scott@12082008

			RectangleF rectF = RectangleF.Empty;	//Scott@12082008

			int nReportWidth=this.ReportWidth;					
		
			
			if(nReportWidth>0)
			{
				nReportWidth+=2;
			}

			int FirstPageHeight= this.HeightPerPage + (int)(this.ReportFooterHeight/Webb.Utility.ConvertCoordinate);	//07-03-2008@Scott
				
			int  NormalPageHeight= this.HeightPerPage + (int)(this.ReportHeaderHeight/Webb.Utility.ConvertCoordinate) + (int)(this.ReportFooterHeight/Webb.Utility.ConvertCoordinate);	//07-03-2008@Scott
				
			if (this.HeightPerPage != 0)
			{
				if(m_y<FirstPageHeight)
				{
					m_tempHeight =m_y;	//07-07-2008@Scott
				}
				else
				{
					m_tempHeight=(m_y-FirstPageHeight)%NormalPageHeight;
				}
				IWebbTableCell	firstcell=this.GetCell(0,0);

				if(firstcell!=null)
				{
					int absLoc=MaxAbsPosition(FirstPageHeight,NormalPageHeight,m_y);

					int cellHeight=firstcell.CellStyle.Height+ WebbTableCellHelper.nSpace;

					if(m_y+cellHeight>absLoc)
					{
						m_y=absLoc+5;

						m_tempHeight=5;
					}
				}

			}
			else
			{
				System.Diagnostics.Debug.WriteLine("Height/Page is zero !");
			}


			Int32Collection breakColumns=this.GetBreakColumns(nReportWidth,m_x,true);
		
			for (int m_row = 0; m_row < this._Rows; m_row++)
			{
				int NextRowCellHeight=0;
		
				if(m_row < this._Rows - 1&&this._Columns>0)
				{
					NextRowCellHeight=GetMatrixMergedHeight(m_row,MergedCount,true);					
				}	
		
				for (int m_col = 0; m_col < this._Columns; m_col++)
				{
					m_cell = this._Cells[m_row, m_col];                  

					if ((m_cell.MergeType & MergeTypes.Merged) == MergeTypes.Merged)
					{
						m_x += m_cell.CellStyle.Width + WebbTableCellHelper.nSpace;	//Modified at 2009-1-12 14:02:38@Scott
		      
						continue;
					}					
		
					m_brick = m_cell.CreateTextBrick();

					m_shadowBrick = m_cell.CreateShadowBrick();	//Scott@12082008
		
					m_brick.Text = m_cell.Text;
		
					if (m_cell.ClickEventArg != null)
					{
						m_brick.MouseDown += new MouseEventHandler(Webb.Reports.DataProvider.VideoPlayBackManager.OnBrickDown);
		
						m_brick.MouseUp += new MouseEventHandler(Webb.Reports.DataProvider.VideoPlayBackManager.OnBrickUp);
		
						m_brick.OnClick += new EventHandler(m_cell.ClickHandler);
		
						m_brick.Url = "http://www.webbelectronics.com/";
					}
		
					if ((m_cell.MergeType & MergeTypes.Right) == MergeTypes.Right || (m_cell.MergeType & MergeTypes.Down) == MergeTypes.Down)	//12-27-2007@Scott
					{
						#region Merged Cells

						this.GetMergedSize3D(m_row, m_col, out m_MergedWidth, out m_MergedHeight,null,null);
										
						if(m_MergedWidth>0&&m_MergedHeight>0)  //Added this code at 2008-12-4 9:12:22@Simon
						{
							if (m_cell.Image != null)	//12-20-2007@Scott
							{
								ImageBrick m_ImageBrick = m_cell.CreateImageBrick();
		
								rectF = new RectangleF(m_x, m_y, m_MergedWidth, m_MergedHeight);
		
								rectF.Offset(WebbTableCellHelper.nShadowOffset,WebbTableCellHelper.nShadowOffset);	//Scott@12082008
		
								graph.DrawBrick(m_shadowBrick, rectF);	//Scott@12082008
		
								rectF.Offset(-WebbTableCellHelper.nShadowOffset,-WebbTableCellHelper.nShadowOffset);	//Scott@12082008
		
								graph.DrawBrick(m_ImageBrick, rectF);
							}
                            else if (m_cell.ImagePath != string.Empty)	//12-20-2007@Scott
                            {
                                ImageBrick m_ImageBrick = m_cell.CreateImageBrickByPath();

                                rectF = new RectangleF(m_x, m_y, m_MergedWidth, m_MergedHeight);

                                rectF.Offset(WebbTableCellHelper.nShadowOffset, WebbTableCellHelper.nShadowOffset);	//Scott@12082008

                                graph.DrawBrick(m_shadowBrick, rectF);	//Scott@12082008

                                rectF.Offset(-WebbTableCellHelper.nShadowOffset, -WebbTableCellHelper.nShadowOffset);	//Scott@12082008

                                graph.DrawBrick(m_ImageBrick, rectF);
                            }
							else
							{
								if(breakColumns.Contains(m_col))
								{
									m_x=nReportWidth*(m_x/nReportWidth)+nReportWidth+2;		
								}

								int DispWidth=0;

								Hashtable spllitedWidths=this.SplitMergedProperWidth(m_row,m_col,nReportWidth,m_x,true,m_MergedWidth,out DispWidth);     
    
								foreach(int startKey in spllitedWidths.Keys)
								{
									if(startKey==m_x)
									{
										int width=(int)spllitedWidths[m_x];

										rectF = new RectangleF(m_x, m_y, width, m_MergedHeight);
		
										rectF.Offset(WebbTableCellHelper.nShadowOffset,WebbTableCellHelper.nShadowOffset);	//Scott@12082008
		
										TextBrick  shadowBrick = m_cell.CreateShadowBrick();	//Scott@12082008

										graph.DrawBrick(shadowBrick, rectF);	//Scott@12082008
		
										rectF.Offset(-WebbTableCellHelper.nShadowOffset,-WebbTableCellHelper.nShadowOffset);	//Scott@12082008
		
										graph.DrawBrick(m_brick, rectF);
									}
									else
									{
										int width=(int)spllitedWidths[startKey];

										TextBrick leftBick=m_cell.CreateTextBrick();	

										rectF = new RectangleF(startKey, m_y, width, m_MergedHeight);
		
										rectF.Offset(WebbTableCellHelper.nShadowOffset,WebbTableCellHelper.nShadowOffset);	//Scott@12082008
		
										graph.DrawBrick(m_shadowBrick, rectF);	//Scott@12082008
		
										rectF.Offset(-WebbTableCellHelper.nShadowOffset,-WebbTableCellHelper.nShadowOffset);	//Scott@12082008
		
										graph.DrawBrick(leftBick, rectF);
												
									}
								}	
								m_x+=DispWidth;	
							}
						}
						#endregion
					}
					else
					{
						#region  Draw cell

						if(m_cell.CellStyle.Width>0&&m_cell.CellStyle.Height>0)  //Added this code at 2008-12-4 9:12:22@Simon
						{							
							if (m_cell.Image != null)	//12-20-2007@Scott
							{
								ImageBrick m_ImageBrick = m_cell.CreateImageBrick();

                                rectF = new RectangleF(m_x , m_y , m_cell.CellStyle.Width , m_cell.CellStyle.Height );
		
								rectF.Offset(WebbTableCellHelper.nShadowOffset,WebbTableCellHelper.nShadowOffset);	//Scott@12082008
		
								graph.DrawBrick(m_shadowBrick, rectF);	//Scott@12082008
		
								rectF.Offset(-WebbTableCellHelper.nShadowOffset,-WebbTableCellHelper.nShadowOffset);	//Scott@12082008
		
								graph.DrawBrick(m_ImageBrick, rectF);
							}
                            else if (m_cell.ImagePath != string.Empty)	//12-20-2007@Scott
                            {
                                ImageBrick m_ImageBrick = m_cell.CreateImageBrickByPath();

                                rectF = new RectangleF(m_x, m_y, m_cell.CellStyle.Width, m_cell.CellStyle.Height);

                                rectF.Offset(WebbTableCellHelper.nShadowOffset, WebbTableCellHelper.nShadowOffset);	//Scott@12082008

                                graph.DrawBrick(m_shadowBrick, rectF);	//Scott@12082008

                                rectF.Offset(-WebbTableCellHelper.nShadowOffset, -WebbTableCellHelper.nShadowOffset);	//Scott@12082008

                                graph.DrawBrick(m_ImageBrick, rectF);
                            }
                            else
                            {
                                if (m_col > 0 && breakColumns.Contains(m_col))
                                {
                                    m_x = nReportWidth * (m_x / nReportWidth) + nReportWidth + 2;
                                }

                                rectF = new RectangleF(m_x, m_y, m_cell.CellStyle.Width, m_cell.CellStyle.Height);

                                rectF.Offset(WebbTableCellHelper.nShadowOffset, WebbTableCellHelper.nShadowOffset);	//Scott@12082008

                                graph.DrawBrick(m_shadowBrick, rectF);	//Scott@12082008

                                rectF.Offset(-WebbTableCellHelper.nShadowOffset, -WebbTableCellHelper.nShadowOffset);	//Scott@12082008

                                graph.DrawBrick(m_brick, rectF);
                            }
						}
						#endregion
					}
							
					m_x += m_cell.CellStyle.Width;
		
					m_x += WebbTableCellHelper.nSpace;	//Scott@12082008
				}
				m_x = this._Offset.Width;
		
				if (m_cell != null)
				{
					m_y += m_cell.CellStyle.Height;
		
					m_y += WebbTableCellHelper.nSpace;	//Scott@12082008
				}
				else
				{
					System.Diagnostics.Debug.WriteLine("Invalidate cell !");
				}
		
				if (this.RepeatedHeader&&!ExControl.printRepeat)
				{						
					if (m_y<FirstPageHeight)
					{//if current page is the first page, remove report header.
						nHeightPerPage = FirstPageHeight;	//07-03-2008@Scott
					}
					else
					{//if current page is not the first page
						nHeightPerPage = NormalPageHeight;	//07-03-2008@Scott
					}	

					//go to next page	//05-16-2008@Scott
					if (nHeightPerPage > 100)
					{		
						m_tempHeight += m_cell.CellStyle.Height;

						m_tempHeight += WebbTableCellHelper.nSpace;  //2009-6-5 10:46:13@Simon Add this Code
		
						#region Modify codes at 2009-3-24 16:00:02@Simon
						
						if (m_tempHeight<nHeightPerPage&&m_tempHeight+NextRowCellHeight+10>=nHeightPerPage && m_row < m_tempRowsCount - 1 && m_row < this._Rows - 1)
						{
							m_y = m_y-m_tempHeight+nHeightPerPage+5;
																						
							nBreakTime++;	//07-03-2008@Scott						
		
							m_tempHeight = 5;
		
							if (RepeatedHeader)  //define whether print the header in next page
							{
								m_tempRow = m_row;
		
								m_row = -1;	//print first row again
		
								m_tempRowsCount += this.HeaderCount;					
		
								b_repeat = true;
							}
						}
						else if (m_row>=0&& m_row==HeaderCount-1&& b_repeat)   
						{
							m_row = m_tempRow;	//continue to print
		
							b_repeat = false;
						}
						#endregion        //End Modify
					}	
					
				}
						
			}

			return m_y+2;
		}
     
			
		public int CreateMatrixArea(string areaName, IBrickGraphics graph,int MergedCount)
		{
			// TODO:  Add Webbtable.CreateArea implementation
			this.AdjustBorders();  //2009-3-2 8:40:44@Simon
		
			this.AdjustSize();

            ShiftMergCellsIfColumnNotExist();
		
			//
			int m_tempRow = 0;
			int m_tempRowsCount = this._Rows;
			int m_x = this._Offset.Width;
			int m_y = this._Offset.Height;
			int m_tempHeight = this._Offset.Height;
			int m_MergedWidth = 0;
			int m_MergedHeight = 0;
			WebbTableCell m_cell = null;
			TextBrick m_brick = null;			
			int nHeightPerPage = this.HeightPerPage;
			bool b_repeat = false;
			int nHeaderHeight = this.GetHeaderHeight();

			int nReportWidth=this.ReportWidth;

			if(nReportWidth>0)
			{
				nReportWidth+=2;
			}

			Int32Collection breakColumns=this.GetBreakColumns(nReportWidth,m_x,false);

			int FirstPageHeight= this.HeightPerPage+ (int)(this.ReportFooterHeight/Webb.Utility.ConvertCoordinate);	//07-03-2008@Scott
				
			int  NormalPageHeight= this.HeightPerPage+ (int)(this.ReportHeaderHeight/Webb.Utility.ConvertCoordinate) + (int)(this.ReportFooterHeight/Webb.Utility.ConvertCoordinate);	//07-03-2008@Scott
			
		
			if (this.HeightPerPage != 0)
			{
				if(m_y<FirstPageHeight)
				{
					m_tempHeight =m_y;	//07-07-2008@Scott
				}
				else
				{
					m_tempHeight=(m_y-FirstPageHeight)%NormalPageHeight;
				}
				IWebbTableCell	firstcell=this.GetCell(0,0);

				if(firstcell!=null)
				{
					int absLoc=MaxAbsPosition(FirstPageHeight,NormalPageHeight,m_y);

					int cellHeight=firstcell.CellStyle.Height;

					if(m_y+cellHeight>absLoc)
					{
						m_y=absLoc+5;

						m_tempHeight=5;
					}
				}

			}
			else
			{
				System.Diagnostics.Debug.WriteLine("Height/Page is zero !");
			}

			
		
			for (int m_row = 0; m_row < this._Rows; m_row++)
			{
				int NextRowCellHeight=0;
		
				if(m_row < this._Rows - 1&&this._Columns>0)
				{
					NextRowCellHeight=GetMatrixMergedHeight(m_row,MergedCount,false);	
				}		
		
				for (int m_col = 0; m_col < this._Columns; m_col++)
				{
					m_cell = this._Cells[m_row, m_col];                    
		
					if(Views.GroupView.BreakedWithLine)   //Add at 2009-2-27 9:10:36@Simon
					{   
						if (m_tempHeight+m_cell.CellStyle.Height+NextRowCellHeight> nHeightPerPage && m_row < m_tempRowsCount - 1 && m_row < this._Rows - 1&&!ExControl.printRepeat)
						{
							m_cell.CellStyle.Sides|=DevExpress.XtraPrinting.BorderSide.Bottom;  //Add at 2009-2-27 9:19:57@Simon
						}
					}
		
					if ((m_cell.MergeType & MergeTypes.Merged) == MergeTypes.Merged)
					{
				        m_x += m_cell.CellStyle.Width;
		
						continue;
					}
											
					m_brick = m_cell.CreateTextBrick();
		
					m_brick.Text = m_cell.Text;
		
					//m_brick.Style.Radius = 30;							
		
					if (m_cell.ClickEventArg != null)
					{
						m_brick.MouseDown += new MouseEventHandler(Webb.Reports.DataProvider.VideoPlayBackManager.OnBrickDown);
		
						m_brick.MouseUp += new MouseEventHandler(Webb.Reports.DataProvider.VideoPlayBackManager.OnBrickUp);
		
						m_brick.OnClick += new EventHandler(m_cell.ClickHandler);
		
						m_brick.Url = "http://www.webbelectronics.com/";
					}
		
					if ((m_cell.MergeType & MergeTypes.Right) == MergeTypes.Right || (m_cell.MergeType & MergeTypes.Down) == MergeTypes.Down)	//12-27-2007@Scott
					{
						# region Draw Merged Cells

						this.GetMergedSize(m_row, m_col, out m_MergedWidth, out m_MergedHeight,null,null);
		
						if(m_MergedWidth>0&&m_MergedHeight>0)  //Added this code at 2008-12-4 9:12:22@Simon
						{									
							if (m_cell.Image != null)	//12-20-2007@Scott
							{
								ImageBrick m_ImageBrick = m_cell.CreateImageBrick();
		
								graph.DrawBrick(m_ImageBrick, new RectangleF(m_x, m_y, m_MergedWidth, m_MergedHeight));
							}
                            else if (m_cell.ImagePath != string.Empty)	//12-20-2007@Scott
                            {
                                ImageBrick m_ImageBrick = m_cell.CreateImageBrickByPath();

                                graph.DrawBrick(m_ImageBrick, new RectangleF(m_x, m_y, m_MergedWidth, m_MergedHeight));
                            }
							else
							{
								if(breakColumns.Contains(m_col))
								{
									m_x=nReportWidth*(m_x/nReportWidth)+nReportWidth+2;		
								}

								int DispWidth=0;

								Hashtable spllitedWidths=this.SplitMergedProperWidth(m_row,m_col,nReportWidth,m_x,false,m_MergedWidth,out DispWidth);   
    
								foreach(int startKey in spllitedWidths.Keys)
								{
									if(startKey==m_x)
									{
										int width=(int)spllitedWidths[m_x];

										graph.DrawBrick(m_brick, new RectangleF(m_x, m_y, width, m_MergedHeight));								
										
									}
									else
									{
										int width=(int)spllitedWidths[startKey];

										TextBrick leftBick=m_cell.CreateTextBrick();											

										graph.DrawBrick(leftBick, new RectangleF(startKey, m_y, width, m_MergedHeight));									
									}

								}

                                m_x+=DispWidth;

								if((m_cell.MergeType & MergeTypes.Down) == MergeTypes.Down)
								{
									if(m_cell.CellStyle.Sides>0)
									{
										if(graph is BrickGraphics)
										{
											(graph as BrickGraphics).DrawLine(new PointF(m_x, m_y),new PointF(m_x+m_MergedWidth, m_y),m_cell.CellStyle.BorderColor,1);
										}
									}
								}									
									
							}

						}

						m_x += m_cell.CellStyle.Width;
		
						#endregion
					}
					else
					{	
						#region Draw cell

						if(m_cell.CellStyle.Width>0&&m_cell.CellStyle.Height>0)  //Added this code at 2008-12-4 9:12:22@Simon
						{	
							if(m_col>0&&breakColumns.Contains(m_col))
							{
								m_x=nReportWidth*(m_x/nReportWidth)+nReportWidth+2;											
							}									

							if (m_cell.Image != null)	//12-20-2007@Scott
							{
								ImageBrick m_ImageBrick = m_cell.CreateImageBrick();

                                graph.DrawBrick(m_ImageBrick, new RectangleF(m_x , m_y , m_cell.CellStyle.Width, m_cell.CellStyle.Height ));
							}
                            else if (m_cell.ImagePath != string.Empty)	//12-20-2007@Scott
                            {
                                ImageBrick m_ImageBrick = m_cell.CreateImageBrickByPath();

                                graph.DrawBrick(m_ImageBrick, new RectangleF(m_x, m_y, m_cell.CellStyle.Width, m_cell.CellStyle.Height));
                            }
							else
							{
								graph.DrawBrick(m_brick, new RectangleF(m_x, m_y, m_cell.CellStyle.Width, m_cell.CellStyle.Height));
							}									
									
							m_x += m_cell.CellStyle.Width;
						}	

						#endregion							
					}						
							
				}	
		
				m_x = this._Offset.Width;
		
				if (m_cell != null)
				{
					m_y += m_cell.CellStyle.Height;
				}
				else
				{
					System.Diagnostics.Debug.WriteLine("Invalidate cell !");
				}
		
				if (this.RepeatedHeader && !ExControl.printRepeat )
				{
					//go to next page	//05-16-2008@Scott
					if (m_y<FirstPageHeight)
					{//if current page is the first page, remove report header.
						nHeightPerPage = FirstPageHeight;	//07-03-2008@Scott
					}
					else
					{//if current page is not the first page
						nHeightPerPage = NormalPageHeight;	//07-03-2008@Scott
					}	

					//go to next page	//05-16-2008@Scott
					if (nHeightPerPage > 100)
					{		
						m_tempHeight += m_cell.CellStyle.Height;
		
						#region Modify codes at 2009-3-24 16:00:02@Simon
						
						if (m_tempHeight<nHeightPerPage&&m_tempHeight+NextRowCellHeight+10>=nHeightPerPage && m_row < m_tempRowsCount - 1 && m_row < this._Rows - 1)
						{
							m_y = m_y-m_tempHeight+nHeightPerPage+5;	
		
							m_tempHeight =5;
		
							if (RepeatedHeader)  //define whether print the header in next page
							{
								m_tempRow = m_row;
		
								m_row = -1;	//print first row again
		
								m_tempRowsCount += this.HeaderCount;						
		
								b_repeat = true;
							}
						}
						else if (m_row>=0&& m_row==HeaderCount-1&& b_repeat)
						{
							m_row = m_tempRow;	//continue to print
		
							b_repeat = false;
						}
						#endregion        //End Modify
					}
				}
			}
			return m_y+2;
		}	
		#endregion

		#region Sub Functions for create area
			private Hashtable SplitMergedProperWidth(int row,int col,int reportWidth,int m_x,bool ThreeD,int MergedWidth,out int dispWidth)
			{			            
				Hashtable breakwidths=new Hashtable();
						
				IWebbTableCell cell =this.GetCell(row,col);

				breakwidths.Add(m_x,cell.CellStyle.Width);	

				dispWidth=0;
		
				if((int)(cell.MergeType&MergeTypes.Right)==0)
				{
					return breakwidths;
				}			

				int Width=0;
							
				for(int j=col;j<this._Columns;j++)
				{
					cell=this.GetCell(row,j);
			                        
					int tempWidth=Width+cell.CellStyle.Width;

					if(ThreeD)tempWidth+=WebbTableCellHelper.nSpace;
					
					if(j>0&&reportWidth>0&&(m_x+Width)/reportWidth!=(m_x+tempWidth)/reportWidth)
					{				

						if(ThreeD)
						{
							breakwidths[m_x]=Width-WebbTableCellHelper.nSpace;
						}
						else
						{
							breakwidths[m_x]=Width;							
						}	
					
						dispWidth+=reportWidth*(m_x/reportWidth)+reportWidth+2-(m_x+Width);

						m_x=reportWidth*(m_x/reportWidth)+reportWidth+2;	
						
						breakwidths.Add(m_x,cell.CellStyle.Width);	

						tempWidth=cell.CellStyle.Width;

						if(ThreeD)tempWidth+=WebbTableCellHelper.nSpace;
					}

					if((int)(cell.MergeType&MergeTypes.End)>0)
					{		
						Width=tempWidth;
 
						break;
					}
					Width=tempWidth;

					
				}
				if(Width>0)
				{
					if(ThreeD)
					{
						Width-=WebbTableCellHelper.nSpace;

					}
					breakwidths[m_x]=Width;
				}
					
				return breakwidths;
			}
			private Int32Collection GetBreakColumns(int reportWidth,int m_x,bool ThreeD)
			{			            
				Int32Collection breakColumns=new Int32Collection(); 

				if(this._Rows==0)return breakColumns;
						
				IWebbTableCell cell =null;

				int Width=0;
							
				for(int j=0;j<this._Columns;j++)
				{
					cell=this.GetCell(0,j);
			                        
					int tempWidth=Width+cell.CellStyle.Width;

					if(ThreeD)tempWidth+=WebbTableCellHelper.nSpace;

					if(j>0&&reportWidth>0&&(m_x+Width)/reportWidth!=(m_x+tempWidth)/reportWidth)
					{
						breakColumns.Add(j);

						m_x=reportWidth*(m_x/reportWidth)+reportWidth+2;	

						tempWidth=cell.CellStyle.Width;

						if(ThreeD)tempWidth+=WebbTableCellHelper.nSpace;
					}

					Width=tempWidth;
				}
					
				return breakColumns;

			}


			private int GetMatrixMergedHeight(int m_Row,int mergedRows,bool Threed)
			{
				if(m_Row>=this._Rows-1)return 0;              

				IWebbTableCell	m_cell = this._Cells[m_Row+1, 0];		
		
                int m_Height=0;

				if(m_Row<this.HeaderCount)
				{
					m_Height=m_cell.CellStyle.Height;

					if(Threed)m_Height+=WebbTableCellHelper.nSpace;

					return m_Height;
				}

				if ((m_cell.MergeType & MergeTypes.Down) == MergeTypes.Down)
				{
					m_Height=0;

					m_Row++;

					if(m_Row>=this._Rows)return 0;  
	           
					for (int i=0;i<mergedRows;i++)
					{
                       if(m_Row+i>=this._Rows)break;

						m_cell = this._Cells[m_Row+i, 0];    
   
						m_Height+=m_cell.CellStyle.Height;  
 
						if(Threed)m_Height+=WebbTableCellHelper.nSpace;
					}

					return m_Height;
				
				}
				

				return this._Cells[m_Row+1, 0].CellStyle.Height;


			}
	

			private int GetHeaderHeight()
			{
				int m_tempHeight=0;
				for (int m_row = 0; m_row < this.HeaderCount; m_row++)
				{    
					if(m_row>=this._Rows)break;

					IWebbTableCell m_cell = this._Cells[m_row,0];

					if(m_cell != null) m_tempHeight+=m_cell.CellStyle.Height;

				}
				return  m_tempHeight;
			}
			private int Get3DHeaderHeight()
			{
				int m_tempHeight=0;
				for (int m_row = 0; m_row < this.HeaderCount; m_row++)
				{    
					if(m_row>=this._Rows)break;

					IWebbTableCell m_cell = this._Cells[m_row,0];

					if(m_cell != null) m_tempHeight+=m_cell.CellStyle.Height;

					if(m_row < this._Rows) m_tempHeight += WebbTableCellHelper.nSpace;

					else m_tempHeight += WebbTableCellHelper.nShadowOffset;
				}
				return  m_tempHeight;
			}

			//05-08-2008@Scott
			private int GetReportDetailHeight(IBrickGraphics graph)
			{
				DevExpress.XtraPrinting.XtraPageSettings settings = (graph as BrickGraphics).PrintingSystem.PageSettings;

				float nPaperHeight = settings.UsablePageSize.Height;

				float nPageHeaderHeight = settings.MarginsF.Top - settings.MinMargins.Top;

				float nPageFooterHeight = settings.MarginsF.Bottom - settings.MinMargins.Bottom;

				int nDetailHeight = (int)(nPaperHeight - nPageHeaderHeight - nPageFooterHeight);

				return nDetailHeight;
			}


		#region GetMergedSize
            private void GetMergedCellCount(int i_StartRow, int i_StartCol, out int i_x, out int i_y)
            {
                //Scott@2007-12-27 11:08 modified some of the following code.
               
                int m_row = 1;
                int m_col = 1;

                WebbTableCell m_cell = this._Cells[i_StartRow, i_StartCol];

                if ((m_cell.MergeType & MergeTypes.Right) == MergeTypes.Right)
                {
                    if (m_col + i_StartCol < this._Columns)
                    {
                        while ((this._Cells[i_StartRow, i_StartCol + m_col].MergeType & MergeTypes.Merged) == MergeTypes.Merged)
                        {
                            m_cell = this._Cells[i_StartRow, i_StartCol + m_col];                           

                            if ((this._Cells[i_StartRow, i_StartCol + m_col].MergeType & MergeTypes.End) == MergeTypes.End) break;	//Modified at 2008-11-6 14:48:12@Scott

                            m_col++;

                            if (m_col + i_StartCol >= this._Columns) break;
                        }
                    }
                }

                i_x = m_col;

                m_cell = this._Cells[i_StartRow, i_StartCol];           

                if ((m_cell.MergeType & MergeTypes.Down) == MergeTypes.Down)
                {
                    if (m_row + i_StartRow < this._Rows)
                    {
                        while ((this._Cells[i_StartRow + m_row, i_StartCol].MergeType & MergeTypes.Merged) == MergeTypes.Merged)
                        {
                            m_cell = this._Cells[i_StartRow + m_row, i_StartCol];
                           
                            if ((this._Cells[i_StartRow + m_row, i_StartCol].MergeType & MergeTypes.End) == MergeTypes.End) break;	//Modified at 2008-11-6 14:48:16@Scott

                            m_row++;

                            if (m_row + i_StartRow >= this._Rows) break;
                        }
                    }
                }
                i_y = m_row;
               
            }


			private void GetMergedSize(int i_StartRow, int i_StartCol, out int i_x, out int i_y, Int32Collection arrRowHeight, Int32Collection arrColumnWidth)
			{
				//Scott@2007-12-27 11:08 modified some of the following code.
				int m_width = 0;
				int m_height = 0;
				int m_row = 1;
				int m_col = 1;

				WebbTableCell m_cell = this._Cells[i_StartRow, i_StartCol];

				if(arrColumnWidth != null && arrColumnWidth.Count > i_StartCol + m_col)	//Modified at 2008-11-10 15:01:49@Scott
				{
					m_width += arrColumnWidth[i_StartCol];
				}
				else
				{
					m_width += m_cell.CellStyle.Width;
				}

				if ((m_cell.MergeType & MergeTypes.Right) == MergeTypes.Right)
				{
					if (m_col + i_StartCol < this._Columns)
					{
						while ((this._Cells[i_StartRow, i_StartCol + m_col].MergeType & MergeTypes.Merged) == MergeTypes.Merged)
						{
							m_cell = this._Cells[i_StartRow, i_StartCol + m_col];

							if(arrColumnWidth != null && arrColumnWidth.Count > i_StartCol + m_col)	//Modified at 2008-11-10 15:01:55@Scott
							{
								m_width += arrColumnWidth[i_StartCol + m_col];
							}
							else
							{
								m_width += m_cell.CellStyle.Width;
							}

							if((this._Cells[i_StartRow, i_StartCol + m_col].MergeType & MergeTypes.End) == MergeTypes.End)  break;	//Modified at 2008-11-6 14:48:12@Scott

							m_col++;

							if (m_col + i_StartCol >= this._Columns) break;
						}
					}
				}

				m_cell = this._Cells[i_StartRow, i_StartCol];

				m_height += m_cell.CellStyle.Height;

				if ((m_cell.MergeType & MergeTypes.Down) == MergeTypes.Down)
				{
					if (m_row + i_StartRow < this._Rows)
					{
						while ((this._Cells[i_StartRow + m_row, i_StartCol].MergeType & MergeTypes.Merged) == MergeTypes.Merged)
						{
							m_cell = this._Cells[i_StartRow + m_row, i_StartCol];

							m_height += m_cell.CellStyle.Height;

							if((this._Cells[i_StartRow + m_row, i_StartCol].MergeType & MergeTypes.End) == MergeTypes.End) break;	//Modified at 2008-11-6 14:48:16@Scott

							m_row++;

							if (m_row + i_StartRow >= this._Rows) break;
						}
					}
				}
				i_x = m_width;

				i_y = m_height;
			}

			//Modified at 2009-1-9 10:20:51@Scott		
		  private void GetMergedSize3D(int i_StartRow, int i_StartCol, out int i_x, out int i_y, Int32Collection arrRowHeight, Int32Collection arrColumnWidth)
			{
				//Scott@2007-12-27 11:08 modified some of the following code.
				int m_width = 0;
				int m_height = 0;
				int m_row = 1;
				int m_col = 1;

				WebbTableCell m_cell = this._Cells[i_StartRow, i_StartCol];

				if(arrColumnWidth != null && arrColumnWidth.Count > i_StartCol + m_col)	//Modified at 2008-11-10 15:01:49@Scott
				{
					m_width += arrColumnWidth[i_StartCol];
				}
				else
				{
					m_width += m_cell.CellStyle.Width;
				}

				if ((m_cell.MergeType & MergeTypes.Right) == MergeTypes.Right)
				{
					if (m_col + i_StartCol < this._Columns)
					{
						while ((this._Cells[i_StartRow, i_StartCol + m_col].MergeType & MergeTypes.Merged) == MergeTypes.Merged)
						{
							m_cell = this._Cells[i_StartRow, i_StartCol + m_col];

							if(arrColumnWidth != null && arrColumnWidth.Count > i_StartCol + m_col)	//Modified at 2008-11-10 15:01:55@Scott
							{
								m_width += arrColumnWidth[i_StartCol + m_col];

								m_width += WebbTableCellHelper.nSpace;	//Modified at 2009-1-9 10:21:36@Scott
							}
							else
							{
								m_width += m_cell.CellStyle.Width;

								m_width += WebbTableCellHelper.nSpace;	//Modified at 2009-1-9 10:21:40@Scott
							}

							if((this._Cells[i_StartRow, i_StartCol + m_col].MergeType & MergeTypes.End) == MergeTypes.End)  break;	//Modified at 2008-11-6 14:48:12@Scott

							m_col++;

							if (m_col + i_StartCol >= this._Columns) break;
						}
					}
				}

				m_cell = this._Cells[i_StartRow, i_StartCol];

				m_height += m_cell.CellStyle.Height;

				if ((m_cell.MergeType & MergeTypes.Down) == MergeTypes.Down)
				{
					if (m_row + i_StartRow < this._Rows)
					{
						while ((this._Cells[i_StartRow + m_row, i_StartCol].MergeType & MergeTypes.Merged) == MergeTypes.Merged)
						{
							m_cell = this._Cells[i_StartRow + m_row, i_StartCol];

							m_height += m_cell.CellStyle.Height;

							m_height += WebbTableCellHelper.nSpace;	//Modified at 2009-1-9 10:21:40@Scott

							if((this._Cells[i_StartRow + m_row, i_StartCol].MergeType & MergeTypes.End) == MergeTypes.End) break;	//Modified at 2008-11-6 14:48:16@Scott

							m_row++;

							if (m_row + i_StartRow >= this._Rows) break;
						}
					}
				}
				i_x = m_width;

				i_y = m_height;
			}

		#endregion
	  #endregion
		
		#region Reset BorderSides 
		//Method to set  
		private void AdjustBorders()     //Add at 2009-2-27 16:18:54@Simon
		{
			IWebbTableCell cell=null;

			for(int rowindex=0;rowindex<this._Rows;rowindex++)
			{
				for(int colIndex=0;colIndex<this._Columns;colIndex++)
				{
					cell=this._Cells[rowindex, colIndex];
                    
					if(cell.MergeType!=MergeTypes.None)continue;

					if(rowindex<_Rows-1)
					{
						if((cell.CellStyle.Sides&DevExpress.XtraPrinting.BorderSide.Bottom)==DevExpress.XtraPrinting.BorderSide.Bottom)
						{							
							_Cells[rowindex+1, colIndex].CellStyle.Sides|=DevExpress.XtraPrinting.BorderSide.Top;
						}
                    }

                    if(colIndex<_Columns-1)
					{
						if((cell.CellStyle.Sides&DevExpress.XtraPrinting.BorderSide.Right)==DevExpress.XtraPrinting.BorderSide.Right)
						{
							_Cells[rowindex, colIndex+1].CellStyle.Sides|=DevExpress.XtraPrinting.BorderSide.Left;
							
						}
					}

				}
			}

		
		}
		#endregion

		#region GetCellStandardSize
		private int GetStandardWidth(int m_col)
		{
			int m_row = 0;
							
			while(m_row < this._Rows&&(this._Cells[m_row, m_col].MergeType&MergeTypes.Merged)==MergeTypes.Merged)
			{
				m_row++;
			}					
			
			if(m_row<this._Rows)
			{
				return this._Cells[m_row, m_col].CellStyle.Width;
			}
			
			return this._Cells[0, m_col].CellStyle.Width;
		}

        private int GetStandardHeight(int m_row)
		{
			int m_col = 0; 			
				
			while(m_col < this._Columns&&(this._Cells[m_row, m_col].MergeType&MergeTypes.Merged)==MergeTypes.Merged)
			{
				m_col++;
			}
			
			if(m_col < this._Columns)
			{
				return this._Cells[m_row, m_col].CellStyle.Height;
			}

			return this._Cells[m_row, 0].CellStyle.Height;
		}
		#endregion
		
		#region Modify codes at 2009-4-1 13:19:53@Simon
//		public void AdjustSize()
//		{			
//			//Set columns width
//			for (int m_col = 0; m_col < this._Columns; m_col++)
//			{
//				int m_width = this.GetStandardWidth(m_col);
//
//				for (int m_row = 0; m_row < this._Rows; m_row++)
//				{
//					this._Cells[m_row, m_col].CellStyle.Width = m_width;
//				}
//			}
//
//			//Set rows height
//			for (int m_row = 0; m_row < this._Rows; m_row++)
//			{
//				int m_height = this.GetStandardHeight(m_row);
//
//				for (int m_col = 0; m_col < this._Columns; m_col++)
//				{
//					this._Cells[m_row, m_col].CellStyle.Height = m_height;
//				}
//			}
//		}
		public void AdjustSize()
		{	
			//Set columns width
			for (int m_col = 0; m_col < this._Columns; m_col++)
			{
				int m_width = this._Cells[0, m_col].CellStyle.Width;

				for (int m_row = 0; m_row < this._Rows; m_row++)
				{
					this._Cells[m_row, m_col].CellStyle.Width = m_width;
				}
			}

			//Set rows height
			for (int m_row = 0; m_row < this._Rows; m_row++)
			{
				int m_height = this._Cells[m_row, 0].CellStyle.Height;

				for (int m_col = 1; m_col < this._Columns; m_col++)
				{
					this._Cells[m_row, m_col].CellStyle.Height = m_height;
				}
			}
		}


        public void ShiftMergCellsIfColumnNotExist()
        {

            //Set rows height
            for (int m_row = 0; m_row < this._Rows; m_row++)
            {          
                for (int m_col = 0; m_col < this._Columns; m_col++)
                {
                    IWebbTableCell cell=this._Cells[m_row, m_col];

                    if(cell.CellStyle.Width==0)
                    {
                        if ((cell.MergeType & MergeTypes.Right) == MergeTypes.Right)
                       {
                           cell.MergeType = MergeTypes.None;

                           ArrayList arrMergedCells=new ArrayList();

                           int stepCol=1;

                          IWebbTableCell   nNextMergedCell=this.GetCell(m_row, m_col + stepCol);

                          while (nNextMergedCell!=null && (nNextMergedCell.MergeType & MergeTypes.Merged) == MergeTypes.Merged)
                          {
                              arrMergedCells.Add(nNextMergedCell);

                              if ((nNextMergedCell.MergeType & MergeTypes.End) == MergeTypes.End) break;	//Modified at 2008-11-6 14:48:12@Scott

                              stepCol++;

                              if(m_col + stepCol>=this._Columns)break;

                              nNextMergedCell=this.GetCell(m_row, m_col + stepCol);

                          }

                          if (arrMergedCells.Count > 0)
                          {
                              nNextMergedCell = this._Cells[m_row, m_col+1];

                              nNextMergedCell.CellStyle.SetStyle(cell.CellStyle);

                              nNextMergedCell.Text = cell.Text;

                              cell.Text = string.Empty;

                              if (arrMergedCells.Count == 1)
                              {
                                  nNextMergedCell.MergeType = MergeTypes.None;
                              }
                              else
                              {
                                  nNextMergedCell.MergeType = nNextMergedCell.MergeType | MergeTypes.Right;
                              }                             
                          }                         
                              
                       }                    
                    }
                }
            }
        }
		#endregion        //End Modify

		public void Finalize(IPrintingSystem ps, ILink link)
		{
			// TODO:  Add Webbtable.Finalize implementation
		}

		#endregion

		#region Create Area Test Functions
		public int CreateTestArea(string areaName, IBrickGraphics graph)
		{	
			int m_x = this._Offset.Width;
			int m_y = this._Offset.Height;
			
			for (int m_row = 0; m_row < 400; m_row++)
			{				  
				TextBrick m_brick=new TextBrick();	
				
				m_brick.Sides=	DevExpress.XtraPrinting.BorderSide.Left|DevExpress.XtraPrinting.BorderSide.Top;
		
				m_brick.HorzAlignment=DevExpress.Utils.HorzAlignment.Far;
				m_brick.Text =m_y.ToString();										
					
				graph.DrawBrick(m_brick, new RectangleF(m_x, m_y, 100,20));	

				for(int pointEndY=m_y+2;pointEndY<m_y+20;pointEndY+=2)
				{	
					if((pointEndY-m_y)%4==0)
					{
						(graph as BrickGraphics).DrawLine(new PointF(m_x, pointEndY),new PointF(m_x+12, pointEndY),Color.Black,1);
					}
					else
					{
						(graph as BrickGraphics).DrawLine(new PointF(m_x, pointEndY),new PointF(m_x+6, pointEndY),Color.Black,1);
					}
				}


									
				m_y += 20;
			}
			return m_y;
		}
		#endregion
  
		#region IDisposable Members

		public void Dispose()
		{
			// TODO:  Add Webbtable.Dispose implementation
		}

		#endregion

		#region Get Total Table Width/Height

		public int GetTotalWidth()
		{
			int m_Width = 0;

			for (int m_col = 0; m_col < this._Columns; m_col++)
			{
				m_Width += this._Cells[0, m_col].CellStyle.Width;
			}
			return m_Width;
		}

		public int GetTotalWidth(bool b3DView)
		{
			int m_Width = 0;

			for (int m_col = 0; m_col < this._Columns; m_col++)
			{
				m_Width += this._Cells[0, m_col].CellStyle.Width;

				if(b3DView)
				{
					if(m_col < this._Columns) m_Width += WebbTableCellHelper.nSpace;
					else m_Width += WebbTableCellHelper.nShadowOffset;
				}
			}
			return m_Width;
		}
		public int GetChartHeight()  //2009-6-2 14:22:04@Simon Add this Code
		{
			return this._Cells[0, 0].CellStyle.Height;
		}
		public int GetChartWidth()  //2009-6-2 14:22:08@Simon Add this Code
		{
			return this._Cells[0, 0].CellStyle.Width;
		}

		public int GetTotalHeight()
		{
			int m_Height = 0;

			for (int m_row = 0; m_row < this._Rows; m_row++)
			{
				m_Height += this._Cells[m_row, 0].CellStyle.Height;
			}
			return m_Height;
		}

		public int GetTotalHeight(bool b3DView)
		{
			int m_Height = 0;

			for (int m_row = 0; m_row < this._Rows; m_row++)
			{
				m_Height += this._Cells[m_row, 0].CellStyle.Height;

				if(b3DView)
				{
					if(m_row < this._Rows) m_Height += WebbTableCellHelper.nSpace;
					else m_Height += WebbTableCellHelper.nShadowOffset;
				}
			}
			return m_Height;
		}


		#endregion
	
		#region Paint functions					 
		//Wu.Country@2007-11-19 14:41 added this region.
		public void PaintTable(PaintEventArgs e, bool bUpdateAll, Rectangle updateRect)
		{
			this.AdjustBorders();  //2009-3-2 8:40:44@Simon

			this.AdjustSize();

            ShiftMergCellsIfColumnNotExist();

			int m_x = this._Offset.Width;
			int m_y = this._Offset.Height;
			int m_MergedWidth = 0;
			int m_MergedHeight = 0;
			WebbTableCell m_cell = null;

			for (int m_row = 0; m_row < this._Rows; m_row++)
			{
				for (int m_col = 0; m_col < this._Columns; m_col++)
				{
					m_cell = this._Cells[m_row, m_col];

					if ((m_cell.MergeType & MergeTypes.Merged) == MergeTypes.Merged) //Modified at 2008-11-6 15:01:11@Scott
					{
						m_x += m_cell.CellStyle.Width;

						continue;
					}
					
					if ((m_cell.MergeType & MergeTypes.Right) == MergeTypes.Right ||
						(m_cell.MergeType & MergeTypes.Down) == MergeTypes.Down)	//12-27-2007@Scott
					{
						this.GetMergedSize(m_row, m_col, out m_MergedWidth, out m_MergedHeight,null,null);

						if(m_MergedWidth>0&&m_MergedHeight>0)
						{
							m_cell.PaintCell(e, m_x, m_y, m_MergedWidth, m_MergedHeight);
						}
					}
					else
					{
						if(m_cell.CellStyle.Width>0&&m_cell.CellStyle.Height>0)
						{
							m_cell.PaintCell(e, m_x, m_y);
						}
					}
					m_x += m_cell.CellStyle.Width;
				}
				m_x = this._Offset.Width;

				if (m_cell != null) m_y += m_cell.CellStyle.Height;

				//05-29-2008@Scott
				if (!bUpdateAll && updateRect != Rectangle.Empty)
				{
					if (m_y > updateRect.Height) return;
				}
			}
		}

		//Scott@12082008
		public void PaintTable3D(PaintEventArgs e, bool bUpdateAll, Rectangle updateRect)
		{
			this.AdjustBorders();  //2009-3-2 8:40:44@Simon

			this.AdjustSize();

            ShiftMergCellsIfColumnNotExist();

			int m_x = this._Offset.Width;
			int m_y = this._Offset.Height;
			int m_MergedWidth = 0;
			int m_MergedHeight = 0;
			WebbTableCell m_cell = null;

			for (int m_row = 0; m_row < this._Rows; m_row++)
			{
				for (int m_col = 0; m_col < this._Columns; m_col++)
				{
					m_cell = this._Cells[m_row, m_col];

					if ((m_cell.MergeType & MergeTypes.Merged) == MergeTypes.Merged) //Modified at 2008-11-6 15:01:11@Scott
					{
						m_x += m_cell.CellStyle.Width + WebbTableCellHelper.nSpace;	//Modified at 2009-1-12 13:52:15@Scott

						continue;
					}					

					if ((m_cell.MergeType & MergeTypes.Right) == MergeTypes.Right ||
						(m_cell.MergeType & MergeTypes.Down) == MergeTypes.Down)	//12-27-2007@Scott
					{
						this.GetMergedSize3D(m_row, m_col, out m_MergedWidth, out m_MergedHeight,null,null);
						
						if(m_MergedWidth>0&&m_MergedHeight>0)
						{
							m_cell.PaintShadowCell(e, m_x + WebbTableCellHelper.nShadowOffset, m_y + WebbTableCellHelper.nShadowOffset, m_MergedWidth, m_MergedHeight);

							m_cell.PaintCell(e, m_x, m_y, m_MergedWidth, m_MergedHeight);
						}
					}
					else
					{
						if(m_cell.CellStyle.Width>0&&m_cell.CellStyle.Height>0)  //Added this code at 2008-12-4 9:12:22@Simon
						{						
							m_cell.PaintShadowCell(e, m_x + WebbTableCellHelper.nShadowOffset, m_y + WebbTableCellHelper.nShadowOffset);

							m_cell.PaintCell(e, m_x, m_y);
						}
					}
					m_x += m_cell.CellStyle.Width;

					m_x += WebbTableCellHelper.nSpace;	//Scott@12082008
				}
				m_x = this._Offset.Width;

				if (m_cell != null)
				{
					m_y += m_cell.CellStyle.Height;

					m_y += WebbTableCellHelper.nSpace;	//Scott@12082008
				}

				//05-29-2008@Scott
				if (!bUpdateAll && updateRect != Rectangle.Empty)
				{
					if (m_y > updateRect.Height) return;
				}
			}
		}

		public void PaintTableWithoutAdjustSize(PaintEventArgs e)
		{
			int m_x = this._Offset.Width;
			int m_y = this._Offset.Height;
			int m_MergedWidth = 0;
			int m_MergedHeight = 0;
			WebbTableCell m_cell = null;

			for (int m_row = 0; m_row < this._Rows; m_row++)
			{
				for (int m_col = 0; m_col < this._Columns; m_col++)
				{
					m_cell = this._Cells[m_row, m_col];

					if ((m_cell.MergeType & MergeTypes.Merged) == MergeTypes.Merged)	//Modified at 2008-11-6 15:01:22@Scott
					{
						m_x += m_cell.CellStyle.Width;

						continue;
					}
					
					if ((m_cell.MergeType & MergeTypes.Right) == MergeTypes.Right ||
						(m_cell.MergeType & MergeTypes.Down) == MergeTypes.Down)	//12-27-2007@Scott
					{
						this.GetMergedSize(m_row, m_col, out m_MergedWidth, out m_MergedHeight,null,null);
						
						m_cell.PaintCell(e, m_x, m_y, m_MergedWidth, m_MergedHeight);
						
					}
					else
					{						
						m_cell.PaintCell(e, m_x, m_y);
						
					}
					m_x += m_cell.CellStyle.Width;
				}
				m_x = this._Offset.Width;

				if (m_cell != null) m_y += m_cell.CellStyle.Height + this._Offset.Height;	//05-29-2008@Scott
			}
		}
		#endregion

		#region Create DataTable
		//Wu.Country@2007-11-29 15:21 added this region.
		public DataTable CreateDataTable()
		{
			//
			DataTable m_Table = new DataTable("WebbTable");
			//Create Columns
			for (int m_col = 0; m_col < this._Columns; m_col++)
			{
				DataColumn m_Column = new DataColumn(this._Cells[0, m_col].Text);
				m_Table.Columns.Add(m_Column);
			}
			//Add rows
			for (int m_row = 1; m_row < this._Rows; m_row++)
			{
				DataRow m_NewRow = m_Table.NewRow();
				for (int m_col = 0; m_col < this._Columns; m_col++)
				{
					m_NewRow[m_col] = this._Cells[m_row, m_col].Text;
				}
				m_Table.Rows.Add(m_NewRow);
			}
			return m_Table;
		}

		public DataTable CreateDataTableWithoutHeader()
		{
			//
			DataTable m_Table = new DataTable("WebbTable");
			//Create Columns
			for (int m_col = 0; m_col < this._Columns; m_col++)
			{
				DataColumn m_Column = new DataColumn();
				m_Column.Caption = string.Empty;
				m_Table.Columns.Add(m_Column);
			}
			//Add rows
			for (int m_row = 0; m_row < this._Rows; m_row++)
			{
				DataRow m_NewRow = m_Table.NewRow();

				for (int m_col = 0; m_col < this._Columns; m_col++)
				{
					m_NewRow[m_col] = this._Cells[m_row, m_col].Text;
				}
				m_Table.Rows.Add(m_NewRow);
			}

			return m_Table;
		}

		public void BindDataTable(DataTable m_Table, bool i_AutoAdjustSize)
		{
			int m_Rows = m_Table.Rows.Count + 1;
			int m_Cols = m_Table.Columns.Count;

			this.ReSizeTable(m_Rows, m_Cols);

			for (int m_col = 0; m_col < m_Cols; m_col++)
			{
				this._Cells[0, m_col].Text = m_Table.Columns[m_col].ColumnName;
				for (int m_row = 1; m_row < m_Rows; m_row++)
				{
					this._Cells[m_row, m_col].Text = m_Table.Rows[m_row - 1][m_col].ToString();
				}
			}
		}

		#endregion

		#region Adjust_Table
		public void AutoAdjustSize(Graphics g, bool bAutoWrap, bool bAutoWidth)
		{
			Webb.Collections.Int32Collection arrWidth = new Webb.Collections.Int32Collection();

			Webb.Collections.Int32Collection arrHeight = new Webb.Collections.Int32Collection();

			this.GetCellsSize(g, arrWidth, arrHeight, bAutoWrap);

			for (int column = 0; column < this._Columns; column++)
			{
				for (int row = 0; row < this._Rows; row++)
				{
					if (!bAutoWrap && bAutoWidth) this._Cells[row, column].CellStyle.Width = arrWidth[column] + 1;	//08-12-2008@Scott

					this._Cells[row, column].CellStyle.Height = arrHeight[row] + 1;	//08-12-2008@Scott
				}
			}
		}        

		public void AutoAdjustMatrixSize(Graphics g, bool bAutoWrap, bool bAutoWidth,MatrixInfo matrixInfo,bool ShowRowIndicators)
		{
			Webb.Collections.Int32Collection arrWidth = new Webb.Collections.Int32Collection();

			Webb.Collections.Int32Collection arrHeight = new Webb.Collections.Int32Collection();

			this.GetMatrixCellsSize(g, arrWidth, arrHeight, bAutoWrap);

			for (int column = 0; column < this._Columns; column++)
			{
				bool fitRowtotal=false;

				if(matrixInfo.RowTotal.ShowTotal)
				{
					int RowTotalindex=ShowRowIndicators?2:1;

                    fitRowtotal=matrixInfo.RowTotal.ShowFront&&column==RowTotalindex;
                    
					fitRowtotal=fitRowtotal||(!matrixInfo.RowTotal.ShowFront&&column==_Columns-1);
				}
				for (int row = 0; row < this._Rows; row++)
				{
					IWebbTableCell cell=this._Cells[row, column];

					if (fitRowtotal||(!bAutoWrap && bAutoWidth)) cell.CellStyle.Width =Math.Max(arrWidth[column] + 1,cell.CellStyle.Width);	//08-12-2008@Scott					

                    cell.CellStyle.Height = arrHeight[row] + 1;	//08-12-2008@Scott
				}
			}
		}

		public void SetNoWrap()
		{
			for (int i = 0; i < this.GetRows(); i++)
			{
				for (int j = 0; j < this.GetColumns(); j++)
				{
					this.GetCell(i, j).CellStyle.StringFormat = StringFormatFlags.NoWrap;
				}
			}
		}

	
		#region Modified By Simon //2009-4-1 14:06:46@Simon 
		public void GetMatrixCellsSize(Graphics g, Webb.Collections.Int32Collection arrWidth, Webb.Collections.Int32Collection arrHeight, bool bAutoWrap)
		{
			arrWidth.Clear();
			arrHeight.Clear();

			SizeF sizeText = new SizeF(0, 0);

			for (int column = 0; column < this._Columns; column++)
			{
				arrWidth.Add(0);

				for (int row = 0; row < this._Rows; row++)
				{
					   if (arrHeight.Count < row + 1) arrHeight.Add(0);

					    WebbTableCell cell = this._Cells[row, column];
					
						if (cell.Text == null || cell.Text.Trim() == string.Empty)
						{//text is empty
							if((cell.MergeType & MergeTypes.Merged) != MergeTypes.Merged)
							{
								sizeText = new SizeF(1, cell.CellStyle.Font.Height);
							}
							else
							{
								sizeText.Width = this._Cells[0, column].CellStyle.Width;
								//									sizeText.Height = this._Cells[0, column].CellStyle.Height;
								sizeText.Height = this._Cells[0, column].CellStyle.Font.Height;	//Modified at 2008-11-19 15:23:31@Scott
							}
						}
						else
						{
							if (bAutoWrap)
							{//auto wrap
								if((cell.MergeType & MergeTypes.Right) != MergeTypes.Right)
								{
									int width = this._Cells[0, column].CellStyle.Width;
									
									sizeText = g.MeasureString(cell.Text, cell.CellStyle.Font, width, cell.RealStringFormat);
								
									sizeText.Width = width;

									#region //2009-4-1 15:09:53@Simon Add this Code									

									if((cell.MergeType & MergeTypes.Down)== MergeTypes.Down)
									{
										string text=cell.Text;

										text=text.Replace("\n"," ");
									
										sizeText.Height=g.MeasureString(text, cell.CellStyle.Font, new PointF(0, 0), cell.RealStringFormat).Height;			
									}								
                                   
									#endregion
								}
								else
								{//Modified at 2008-11-10 15:04:22@Scott
									sizeText.Width = this._Cells[0, column].CellStyle.Width;
									//									sizeText.Height = this._Cells[0, column].CellStyle.Height;
									sizeText.Height = this._Cells[0, column].CellStyle.Font.Height;	//Modified at 2008-11-19 15:23:31@Scott
								}
							}
							else
							{//one line								

								sizeText = g.MeasureString(cell.Text, cell.CellStyle.Font, new PointF(0, 0), cell.RealStringFormat);

								#region //2009-4-1 15:09:53@Simon Add this Code									

								if((cell.MergeType & MergeTypes.Down)== MergeTypes.Down)
								{
									string text=cell.Text;

									text=text.Replace("\n"," ");
									
									sizeText.Height=g.MeasureString(text, cell.CellStyle.Font, new PointF(0, 0), cell.RealStringFormat).Height;			
								}	
							
//								sizeText.Width=(float)(Webb.Utility.ConvertCoordinate*sizeText.Width);
//
//								sizeText.Width=Math.Max(cell.CellStyle.Width,sizeText.Width);
                                   
								#endregion
							}
						
					}

					if (sizeText.Width > arrWidth[column])
					{
						arrWidth[column] = (int)sizeText.Width;
					}

					if (sizeText.Height > arrHeight[row])
					{
						arrHeight[row] = (int)sizeText.Height;
					}
				}
			}

			#region Modified Area
			//Ajust merged columns
			int nMergeWidth = 0, nMergeHeight = 0;
			for (int row = 0; row < this._Rows; row++)
			{
				for (int column = 0; column < this._Columns; column++)
				{
					WebbTableCell cell = this._Cells[row, column];

					if((cell.MergeType & MergeTypes.Right) == MergeTypes.Right)
					{
						this.GetMergedSize(row, column, out nMergeWidth, out nMergeHeight,null,arrWidth);

						sizeText = g.MeasureString(cell.Text, cell.CellStyle.Font, nMergeWidth, cell.RealStringFormat);

						if(sizeText.Height > arrHeight[row])
						{
							arrHeight[row] = (int)sizeText.Height;
						}
					}
				}
			}
			#endregion	//Modify at 2008-11-10 11:25:47@Scott
		}	
			#region Codes for GetCellsSize
			public void GetCellsSize(Graphics g, Webb.Collections.Int32Collection arrWidth, Webb.Collections.Int32Collection arrHeight, bool bAutoWrap)
			{
				arrWidth.Clear();

				arrHeight.Clear();
	
				SizeF sizeText = new SizeF(0, 0);
	
				for (int column = 0; column < this._Columns; column++)
				{
					arrWidth.Add(0);
	
					for (int row = 0; row < this._Rows; row++)
					{
						if (arrHeight.Count < row + 1) arrHeight.Add(0);
	
						WebbTableCell cell = this._Cells[row, column];
	
						if (cell.Image != null||System.IO.File.Exists(cell.ImagePath))
						{//image	//07-16-2008@Scott
							sizeText.Width = this._Cells[0, column].CellStyle.Width;
	
                            //sizeText.Height = (float)cell.Image.Height / cell.Image.Width * sizeText.Width;
                            sizeText.Height = this._Cells[row, 0].CellStyle.Height;
						}
						else if (cell.Tag is Data.ChartBase)
						{//chart element
							sizeText.Width = this._Cells[0, column].CellStyle.Width;
	
							sizeText.Height = this._Cells[row,0].CellStyle.Height;
						}
						else
						{//text
							if (cell.Text == null || cell.Text== string.Empty)
							{//text is empty
								if((cell.MergeType & MergeTypes.Merged) != MergeTypes.Merged)
								{
									sizeText = new SizeF(1, cell.CellStyle.Font.Height);
								}
								else
								{
									sizeText.Width = this._Cells[0, column].CellStyle.Width;
									//									sizeText.Height = this._Cells[0, column].CellStyle.Height;
									sizeText.Height = this._Cells[0, column].CellStyle.Font.Height;	//Modified at 2008-11-19 15:23:31@Scott
								}
							}
							else
							{
								if (bAutoWrap)
								{//auto wrap
									if((cell.MergeType & MergeTypes.Right) != MergeTypes.Right)
									{
										int width = this._Cells[0, column].CellStyle.Width;

										string MeasuredText=cell.Text;

										if(MeasuredText.Trim()==string.Empty)
										{
											MeasuredText=MeasuredText.Replace(" ","a");   //2009-8-18 14:06:54@Simon Add this Code
										}										
									
										sizeText = g.MeasureString(MeasuredText, cell.CellStyle.Font, width, cell.RealStringFormat);
									
										sizeText.Width = width;
									}
									else
									{//Modified at 2008-11-10 15:04:22@Scott
										sizeText.Width = this._Cells[0, column].CellStyle.Width;
										//									sizeText.Height = this._Cells[0, column].CellStyle.Height;
										sizeText.Height = this._Cells[0, column].CellStyle.Font.Height;	//Modified at 2008-11-19 15:23:31@Scott
									}
								}
								else
								{//one line
									string MeasuredText=cell.Text;

									if(MeasuredText.Trim()==string.Empty)
									{
										MeasuredText=MeasuredText.Replace(" ","a");   //2009-8-18 14:06:54@Simon Add this Code
									}

									sizeText = g.MeasureString(MeasuredText, cell.CellStyle.Font, new PointF(0, 0), cell.RealStringFormat);
								    
									#region //2009-4-1 15:09:53@Simon Add this Code									

									if((cell.MergeType & MergeTypes.Down)== MergeTypes.Down)
									{
										string text=cell.Text;

										text=text.Replace("\n"," ");
									
										sizeText.Height=g.MeasureString(text, cell.CellStyle.Font, new PointF(0, 0), cell.RealStringFormat).Height;		
										
									}	
							
//									sizeText.Width=(float)(Webb.Utility.ConvertCoordinate*sizeText.Width);
//
//									sizeText.Width=Math.Max(cell.CellStyle.Width,sizeText.Width);
                                   
									#endregion
								}
							}
						}
	
						if (sizeText.Width > arrWidth[column])
						{
							arrWidth[column] = (int)sizeText.Width;
						}
	
						if (sizeText.Height > arrHeight[row])
						{
							arrHeight[row] = (int)sizeText.Height;
						}
					}
				}
	
				#region Modified Area
				//Ajust merged columns
				int nMergeWidth = 0, nMergeHeight = 0;
				for (int row = 0; row < this._Rows; row++)
				{
					for (int column = 0; column < this._Columns; column++)
					{
						WebbTableCell cell = this._Cells[row, column];
	
						if((cell.MergeType & MergeTypes.Right) == MergeTypes.Right)
						{
							this.GetMergedSize(row, column, out nMergeWidth, out nMergeHeight,null,arrWidth);
	
							sizeText = g.MeasureString(cell.Text, cell.CellStyle.Font, nMergeWidth, cell.RealStringFormat);
	
							if(sizeText.Height > arrHeight[row])
							{
								arrHeight[row] = (int)sizeText.Height;
							}
						}
					}
				}
				#endregion	//Modify at 2008-11-10 11:25:47@Scott
			}

			#endregion
	  #endregion

        public Int32Collection ResolveRowsHeight()
        {
            Int32Collection rowsHeight = new Int32Collection();

            if (this._Rows <= 0 || this._Columns <= 0) return rowsHeight;

            for (int row = 0; row < this._Rows; row++)
            {
                WebbTableCell cell = this._Cells[row, 0];

                if (cell == null) continue;

                rowsHeight.Add(cell.CellStyle.Height);
            }
            return rowsHeight;

        }

		public void DeleteEmptyRows()
		{
			Webb.Collections.Int32Collection arrEmptyRows = this.GetEmptyRows();

			if (arrEmptyRows.Count == 0) return;

			for (int i = arrEmptyRows.Count - 1; i >= 0; i--)
			{
				this.DeleteRow(arrEmptyRows[i]);
			}
		}

		public void DeleteRows(Webb.Collections.Int32Collection arrDeleteRows)
		{
			for (int i = arrDeleteRows.Count - 1; i >= 0; i--)
			{
				this.DeleteRow(arrDeleteRows[i]);
			}
		}
		public void DeleteHeaderRowsAndStyle(Webb.Collections.Int32Collection arrDeleteRows)
		{
			int totalColumn=this.GetColumns();

			for (int i = arrDeleteRows.Count - 1; i >= 0; i--)
			{
				int row=arrDeleteRows[i];

				int totalRows=this.GetRows();

				if(row>=totalRows)continue;

				if(row +1<totalRows)
				{
					for(int col=0;col<totalColumn;col++)
					{
						DevExpress.XtraPrinting.BorderSide side=this._Cells[row,col].CellStyle.Sides;

						if((int)(side&DevExpress.XtraPrinting.BorderSide.Top)!=0)
						{
							this._Cells[row+1,col].CellStyle.Sides|=DevExpress.XtraPrinting.BorderSide.Top;
						}
					}
				}

				this.DeleteRow(row);
			}			 
		}
       

		public void DeleteExcrescentRows(int nTopCount)
		{
			if (nTopCount <= 0) return;

			if (this._Rows <= nTopCount) return;

			for (int row = this._Rows - 1; row > nTopCount - 1; row--)
			{
				this.DeleteRow(row);
			}
		}

        public void DeleteExcrescentRows(int nTopCount, Int32Collection deletedRows, Styles.StyleBuilder.StyleRowsInfo styleRowsInfo)
        {
            if (nTopCount <= 0) return;

            int nRow = this._Rows;

            deletedRows.Sort();

            for (int j = deletedRows.Count - 1; j >= 0; j--)
            {
                this.DeleteRow(deletedRows[j]);

                styleRowsInfo.ShiftDownRowsAfterDelete(deletedRows[j]);
            }

            this._Rows = nRow;

            if (this._Rows <= nTopCount) return;

            for (int row = this._Rows - 1; row > nTopCount - 1; row--)
            {
                this.DeleteRow(row);
            }
        }
		
        public void DeleteEmptyRows(Int32Collection ignoreRows, Styles.StyleBuilder.StyleRowsInfo styleRowsInfo, bool showRowIndicators)   //2009-6-10 15:44:10@Simon Add this Code
		{
            Webb.Collections.Int32Collection arrEmptyRows = this.GetEmptyRows(showRowIndicators);

			if (arrEmptyRows.Count == 0) return;

			for (int i = arrEmptyRows.Count - 1; i >= 0; i--)
			{
                int rowToDelete = arrEmptyRows[i];

                if (!ignoreRows.Contains(rowToDelete))
				{
                    this.DeleteRow(rowToDelete);

                    styleRowsInfo.ShiftDownRowsAfterDelete(rowToDelete);
				}
				
			}
			if(showRowIndicators)
			{
                setRowIndicators(ignoreRows);
			}
		}
		public void setRowIndicators(Int32Collection HeaderRows)
		{	
			int index = 1;

			for(int m_row = 0; m_row < _Rows; m_row++)
			{
				if(HeaderRows.Contains(m_row)) continue;

				this.GetCell(m_row,0).Text = Webb.Utility.FormatIndicator(index++);
			}
		}

		public Webb.Collections.Int32Collection GetEmptyRows()
		{
			Webb.Collections.Int32Collection arrEmptyRows = new Webb.Collections.Int32Collection();

			int i = 0, j = 0;

			bool bTag = true;

			for (i = 0; i < this._Rows; i++)
			{
				bTag = true;

				for (j = 0; j < this._Columns; j++)
				{
					IWebbTableCell cell = this._Cells[i, j];

					if (cell.Text != null && cell.Text != string.Empty)
					{
						bTag = false;
						break;
					}
				}

				if (bTag) arrEmptyRows.Add(i);
			}

			return arrEmptyRows;
		}

		public Webb.Collections.Int32Collection GetEmptyRows(bool  showRowIndicators)
		{
			Webb.Collections.Int32Collection arrEmptyRows = new Webb.Collections.Int32Collection();

			int i = 0, j = 0;

			bool isEmptyRow = true;

			for (i = 0; i < this._Rows; i++)
			{
                isEmptyRow = true;

				for (j = 0; j < this._Columns; j++)
				{
                    if (showRowIndicators && j == 0) continue;

					IWebbTableCell cell = this._Cells[i, j];

                    if ((cell.Text != null && cell.Text != string.Empty) || cell.Image != null || cell.ImagePath!=string.Empty)
					{
                        isEmptyRow = false;

						break;
					}
				}

                if (isEmptyRow) arrEmptyRows.Add(i);
			}

			return arrEmptyRows;
		}


		public void DeleteRow(int index)
		{
			for (int i = index; i < this._Rows - 1; i++)
			{
				int nDesIndex = i * this._Columns;

				int nSrcIndex = (i + 1) * this._Columns;

				Array.Copy(this._Cells, nSrcIndex, this._Cells, nDesIndex, this._Columns);

                //if (i == this._Rows - 2) Array.Clear(this._Cells, (i + 1) * this._Columns, this._Columns);
			}
            for (int j = 0; j < this._Columns; j++)
            {
                this._Cells[this._Rows - 1, j] = new WebbTableCell();
            }

			this._Rows--;
		}
		#endregion

		#region Serialization By Macro 2008-12-15 9:44:01
		public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			info.AddValue("_Cells",_Cells,typeof(Webb.Reports.ExControls.WebbTableCell[,]));
			info.AddValue("_Rows",_Rows);
			info.AddValue("_Columns",_Columns);
			info.AddValue("_TableStyle",_TableStyle,typeof(Webb.Reports.ExControls.BasicStyle));
			info.AddValue("_SycObject",_SycObject);
			info.AddValue("_Offset",_Offset,typeof(System.Drawing.Size));
			info.AddValue("_HeightPerPage",_HeightPerPage);
			info.AddValue("_ReportHeaderHeight",_ReportHeaderHeight);
			info.AddValue("_ReportFooterHeight",_ReportFooterHeight);
			info.AddValue("_RepeatedHeader",_RepeatedHeader);
			info.AddValue("_HeaderCount",_HeaderCount);
		}

		public WebbTable(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			try
			{
				_Cells=(Webb.Reports.ExControls.WebbTableCell[,])info.GetValue("_Cells",typeof(Webb.Reports.ExControls.WebbTableCell[,]));
			}
			catch
			{
				this._Cells = new WebbTableCell[1,1];
			}
			try
			{
				_Rows=info.GetInt32("_Rows");
			}
			catch
			{
				_Rows=0;
			}
			try
			{
				_Columns=info.GetInt32("_Columns");
			}
			catch
			{
				_Columns=0;
			}
			try
			{
				_TableStyle=(Webb.Reports.ExControls.BasicStyle)info.GetValue("_TableStyle",typeof(Webb.Reports.ExControls.BasicStyle));
			}
			catch
			{
				this._TableStyle = new BasicStyle();
			}
			try
			{
				_SycObject=info.GetValue("_SycObject",typeof(object));
			}
			catch
			{
				_SycObject = new object();
			}
			try
			{
				_Offset=(System.Drawing.Size)info.GetValue("_Offset",typeof(System.Drawing.Size));
			}
			catch
			{
				this._Offset = Size.Empty;
			}
			try
			{
				_HeightPerPage=info.GetInt32("_HeightPerPage");
			}
			catch
			{
				_HeightPerPage=0;
			}
			try
			{
				_ReportHeaderHeight=info.GetInt32("_ReportHeaderHeight");
			}
			catch
			{
				_ReportHeaderHeight=0;
			}
			try
			{
				_ReportFooterHeight=info.GetInt32("_ReportFooterHeight");
			}
			catch
			{
				_ReportFooterHeight=0;
			}
			try
			{
				_RepeatedHeader=info.GetBoolean("_RepeatedHeader");
			}
			catch
			{
				_RepeatedHeader=true;
			}
			try
			{
				_HeaderCount=info.GetInt32("_HeaderCount");
			}
			catch
			{
				_HeaderCount=1;
			}
		}
		#endregion        

		public void ClearNoValueRows()
		{		
			for (int m_row = 0; m_row < this._Rows; m_row++)
			{
				for (int m_column = 0; m_column < this._Columns; m_column++)
				{
					//init cell
				    string text=this._Cells[m_row, m_column].Text;
                    
					if(text=="[NoValue]")
					{
						for (int m_col = 0; m_col < _Columns; m_col++)
						{
							this._Cells[m_row, m_col].Text=string.Empty;
						}

						break;
					}
				}
			}
		}
	}
	#endregion

	//08-19-2008@Scott
	#region public class WebbTableCellHelper
	public class WebbTableCellHelper
	{
		public static int nSpace = 5;
		public static int nShadowOffset = 3;
		public static int nPageSpace = 20;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="table"></param>
		/// <param name="i_Row"></param>
		/// <param name="i_Col"></param>
		/// <param name="i_Summary"></param>
		public static void SetCellValue(IWebbTable table, int i_Row, int i_Col, GroupSummary i_Summary)
		{
			if (i_Summary.Value != null)
			{
				WebbTableCell cell = table.GetCell(i_Row, i_Col) as WebbTableCell;

				cell.ClickEventArg = null;

				if (cell == null) return;

				string m_Value = string.Empty;

				cell.Image = null;

				m_Value = FormatValue(cell, i_Summary);

                m_Value = m_Value.Trim(" \t\n".ToCharArray());

				cell.Text = m_Value;
			}
		}

		public static void SetCellValueWithClickEvent(IWebbTable table, int i_Row, int i_Col, GroupSummary i_Summary)
		{
			SetCellValueWithClickEvent(table, i_Row, i_Col, i_Summary, i_Summary.RowIndicators);
		}

		public static void SetCellValueWithClickEvent(IWebbTable table, int i_Row, int i_Col, GroupSummary i_Summary, Int32Collection i_RowIndicators)
		{
			if (i_Summary.Value != null)
			{
				WebbTableCell cell = table.GetCell(i_Row, i_Col) as WebbTableCell;

				if (cell == null) return;

				string m_Value = string.Empty;

				cell.Image = null;

				m_Value = FormatValue(cell, i_Summary);

                m_Value = m_Value.Trim(" \t\r\n".ToCharArray());

				if (i_RowIndicators != null && i_RowIndicators.Count > 0)
				{
					DataSet m_DBSet = Webb.Reports.DataProvider.VideoPlayBackManager.DataSource;

					Webb.Reports.DataProvider.VideoPlayBackArgs m_args = new Webb.Reports.DataProvider.VideoPlayBackArgs(m_DBSet, i_RowIndicators);

					cell.ClickEventArg = m_args;
				}

				cell.Text = m_Value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="table"></param>
		/// <param name="i_Row"></param>
		/// <param name="i_Col"></param>
		/// <param name="i_Value"></param>
		/// <param name="i_Type"></param>
		public static void SetCellValue(IWebbTable table, int i_Row, int i_Col, object i_Value, FormatTypes i_Type)
		{
			if (i_Value != null)
			{
				WebbTableCell cell = table.GetCell(i_Row, i_Col) as WebbTableCell;

				if (cell != null)
				{
					cell.ClickEventArg = null;

					cell.Text = WebbTableCellHelper.FormatValue(i_Value, i_Type);
				}
			}
		}

		public static void SetCellValueWithClickEvent(IWebbTable table, int i_Row, int i_Col, object i_Value, FormatTypes i_Type, Int32Collection i_Rows)
		{
			if (i_Value != null)
			{
				WebbTableCell cell = table.GetCell(i_Row, i_Col) as WebbTableCell;

				if (cell == null) return;

				if (i_Rows != null && i_Rows.Count > 0)
				{
					DataSet m_DBSet = Webb.Reports.DataProvider.VideoPlayBackManager.DataSource;

					Webb.Reports.DataProvider.VideoPlayBackArgs m_args = new Webb.Reports.DataProvider.VideoPlayBackArgs(m_DBSet, i_Rows);

					cell.ClickEventArg = m_args;
				}

				cell.Text = WebbTableCellHelper.FormatValue(i_Value, i_Type);
			}
		}


        private static Hashtable imagesHashTable = new Hashtable();

        public static void SetCellImageFromValue(IWebbTable table, int i_Row, int i_Col,object i_Value)
        {
            if (i_Value != null)
            {
                WebbTableCell cell = table.GetCell(i_Row, i_Col) as WebbTableCell;

                string strImagePath = i_Value.ToString();
           
                if (cell == null || !File.Exists(strImagePath)) return  ;

                cell.ImageSizeMode = PictureBoxSizeMode.StretchImage;

                cell.Image = null;

                cell.ImagePath = strImagePath;


            
            }
        }

     


		/// <summary>
		/// 
		/// </summary>
		/// <param name="i_Row"></param>
		/// <param name="i_Col"></param>
		/// <param name="i_Value"></param>
		/// <param name="i_Type"></param>
		public static void SetCellValue(IWebbTable table, int i_Row, int i_Col, object i_Value, StatTypes i_Type)
		{
			if (i_Value != null)
			{
				string m_Value = string.Empty;
				switch (i_Type)
				{
					case Data.StatTypes.Average:
						m_Value = FormatValue(i_Value, FormatTypes.Decimal);
						break;
					case Data.StatTypes.Percent:
					case Data.StatTypes.DistPercent:
					case Data.StatTypes.OneValuePercent:
						m_Value = FormatValue(i_Value, FormatTypes.Precent);
						break;
					case Data.StatTypes.Frequence:
					case Data.StatTypes.DistFrequence:
					case Data.StatTypes.Total:
						m_Value = FormatValue(i_Value, FormatTypes.Int);
						break;
					default:
						m_Value = FormatValue(i_Value, FormatTypes.String);
						break;
				}
				table.GetCell(i_Row, i_Col).Text = m_Value;
			}
		}

			
		public static void SetCellValueWithClickEvent(IWebbTable table, int i_Row, int i_Col, object i_Value, StatTypes i_Type, Int32Collection i_Rows)
		{
			if (i_Value != null)
			{
				string m_Value = string.Empty;
				switch (i_Type)
				{
					case Data.StatTypes.Average:
						m_Value = FormatValue(i_Value, FormatTypes.Decimal);
						break;
					case Data.StatTypes.Percent:
					case Data.StatTypes.DistPercent:
					case Data.StatTypes.OneValuePercent:
						m_Value = FormatValue(i_Value, FormatTypes.Precent);
						break;
					case Data.StatTypes.Frequence:
					case Data.StatTypes.DistFrequence:
					case Data.StatTypes.Total:
						m_Value = FormatValue(i_Value, FormatTypes.Int);
						break;
					default:
						m_Value = FormatValue(i_Value, FormatTypes.String);
						break;
				}
				if (i_Rows != null && i_Rows.Count > 0)
				{
					DataSet m_DBSet = Webb.Reports.DataProvider.VideoPlayBackManager.DataSource;
					Webb.Reports.DataProvider.VideoPlayBackArgs m_args = new Webb.Reports.DataProvider.VideoPlayBackArgs(m_DBSet, i_Rows);
					(table.GetCell(i_Row, i_Col) as WebbTableCell).ClickEventArg = m_args;
				}
				table.GetCell(i_Row, i_Col).Text = m_Value;
			}
		}

		//Added this code at 2009-2-1 16:39:43@Simon
		public static void SetCellValue(IWebbTable table, int i_Row, int i_Col, object i_Value, FollowedStatTypes i_Type)
		{
			if (i_Value != null)
			{
				string m_Value = string.Empty;
				switch (i_Type.StatTypes)
				{
					case Data.StatTypes.Average:
						m_Value = FormatValue(i_Value, FormatTypes.Decimal,i_Type.DecimalSpace);
						break;
					case Data.StatTypes.Percent:
					case Data.StatTypes.DistPercent:
					case Data.StatTypes.OneValuePercent:
						m_Value = FormatValue(i_Value, FormatTypes.Precent,i_Type.DecimalSpace);
						break;
					case Data.StatTypes.Frequence:
					case Data.StatTypes.DistFrequence:
					case Data.StatTypes.Total:
						m_Value = FormatValue(i_Value, FormatTypes.Int,i_Type.DecimalSpace);
						break;
					default:
						m_Value = FormatValue(i_Value, FormatTypes.String);
						break;
				}
				table.GetCell(i_Row, i_Col).Text = m_Value;
			}
		}

		public static void SetCellValueWithClickEvent(IWebbTable table, int i_Row, int i_Col, object i_Value, FollowedStatTypes i_Type, Int32Collection i_Rows)
		{
			if (i_Value != null)
			{				
				string m_Value = string.Empty;
				switch (i_Type.StatTypes)
				{
					case Data.StatTypes.Average:
						m_Value = FormatValue(i_Value, FormatTypes.Decimal,i_Type.DecimalSpace);
						break;
					case Data.StatTypes.Percent:
					case Data.StatTypes.DistPercent:
					case Data.StatTypes.OneValuePercent:
						m_Value = FormatValue(i_Value, FormatTypes.Precent,i_Type.DecimalSpace);
						break;
					case Data.StatTypes.Frequence:
					case Data.StatTypes.DistFrequence:
					case Data.StatTypes.Total:
						m_Value = FormatValue(i_Value, FormatTypes.Int,i_Type.DecimalSpace);
						break;
					default:
						m_Value = FormatValue(i_Value, FormatTypes.String);
						break;
				}
				if (i_Rows != null && i_Rows.Count > 0)
				{
					DataSet m_DBSet = Webb.Reports.DataProvider.VideoPlayBackManager.DataSource;
					Webb.Reports.DataProvider.VideoPlayBackArgs m_args = new Webb.Reports.DataProvider.VideoPlayBackArgs(m_DBSet, i_Rows);
					(table.GetCell(i_Row, i_Col) as WebbTableCell).ClickEventArg = m_args;
				}
				table.GetCell(i_Row, i_Col).Text = m_Value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="i_value"></param>
		/// <param name="i_Type"></param>
		/// <returns></returns>
		public static string FormatValue(object i_value, FormatTypes i_Type)
		{
			switch (i_Type)
			{
				case FormatTypes.Decimal:
					return string.Format("{0:0.00}", i_value);
				case FormatTypes.Int:
					return string.Format("{0:0}", i_value);
				case FormatTypes.Precent:
					return string.Format("{0:0.0%}", i_value);
				case FormatTypes.String:
				default:
					return i_value.ToString();
			}
		}
		private static string CreateFormat(sbyte sb_Space)
		{
			StringBuilder sb=new StringBuilder();
            
			sb.Append("0:0");

			if(sb_Space<=0)return sb.ToString();

			sb.Append(".");

			for(sbyte s=0;s<sb_Space;s++)
			{
				sb.Append("0");
			}
			return sb.ToString();
		}
		public static string CreatePercentFormat(sbyte sb_Space)
		{
			StringBuilder sb=new StringBuilder(); 

			if(sb_Space>0)
			{
				sb.Append("0");

				sb.Append(".");

				for(sbyte s=0;s<sb_Space;s++)
				{
					sb.Append("0");
				}
			}
			else if(sb_Space==0)
			{
				sb.Append("0");
			}
			else
			{
			    sb.Append("0.0");

			}

			sb.Append("%");

			return sb.ToString();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="i_value"></param>		
		/// <param name="i_Type"></param>
		/// <param name="sb_Space"></param>
		/// <returns></returns>
		public static string FormatValue(object i_value, FormatTypes i_Type,sbyte sb_Space)
		{
			string format=string.Empty; 
			switch (i_Type)
			{
				case FormatTypes.Decimal:
					if(sb_Space<0)
					{
						return string.Format("{0:0.00}", i_value);
					}
					else
					{				
                       format=CreateFormat(sb_Space);

                       return string.Format("{" + format+ "}", i_value);  
					}					
				case FormatTypes.Int:
					if(sb_Space<0)
					{
						return string.Format("{0:0}", i_value);
					}
					else
					{
						format=CreateFormat(sb_Space);

						return string.Format("{" + format+ "}", i_value);  
					}		
					
				case FormatTypes.Precent:
					if(sb_Space<0)
					{
						return string.Format("{0:0.0%}", i_value);
					}
					else
					{
						format=CreateFormat(sb_Space);

						return string.Format("{" + format+ "%}", i_value);  
					}
					
				case FormatTypes.String:
				default:
					return i_value.ToString();
			}
		}

        public static string ReplaceSpecialString(string strValue)
        {
            if (strValue == null||strValue==string.Empty) return string.Empty;

            StringBuilder sb = new StringBuilder();

            sb.Append(strValue);

            sb = sb.Replace(@"\n", "\n");
            sb = sb.Replace("[Space]", " ");
            sb = sb.Replace("[LineBreak]", "\r\n");
            sb = sb.Replace("[Tab]", "        ");
            sb = sb.Replace("[2 Space]", "  ");
            sb = sb.Replace("[4 Space]", "    ");

            return sb.ToString();

        }

		public static string FormatValue(WebbTableCell cell, GroupSummary summary)
		{
			string m_Value = string.Empty;                      

			if(summary.FollowSummary!=null)
			{
                summary.FollowSummary = ReplaceSpecialString(summary.FollowSummary);                
			}

			if (summary.Value != null)
			{
				switch (summary.SummaryType)
				{
					case SummaryTypes.AverageMinus:
					case SummaryTypes.AveragePlus:
					case SummaryTypes.Average:
                        if (summary.AverageType==AverageType.Mode)
                        {
                            m_Value = summary.Value.ToString() + summary.FollowSummary;
                        }
                        else
                        {
                            if (summary.Value == null) return summary.FollowSummary;

                            if (summary.Value.GetType() == typeof(DateTime))
                            {
                                m_Value = summary.Value.ToString() + summary.FollowSummary;  //Added this code at 2009-2-1 16:13:32@Simon
                            }
                            else
                            {
                                if (summary.ShowZeros == false)
                                {
                                    m_Value = FormatValue(summary.Value, FormatTypes.Decimal, summary.DecimalSpace);

                                    if (m_Value == string.Empty)
                                    {
                                        break;
                                    }
                                }
                                m_Value = FormatValue(summary.Value, FormatTypes.Decimal, summary.DecimalSpace) + summary.FollowSummary;  //Added this code at 2009-2-1 16:13:32@Simon
                            }
                        }
						break;
					case SummaryTypes.DistPercent:	//Modified at 2008-10-6 14:01:25@Scott
					case SummaryTypes.ComputedPercent: //Modified at 2008-9-25 14:01:22@Simon
					case SummaryTypes.RelatedPercent:
					case SummaryTypes.ParentRelatedPercent:	//Modified at 2009-2-9 15:10:53@Scott
					case SummaryTypes.GroupPercent:
					case SummaryTypes.Percent:
					case SummaryTypes.DistGroupPercent: //Modified at 2008-10-6 15:08:29@Scott
					case SummaryTypes.TopPercent:
                    case SummaryTypes.PercentAllData:
						if(summary.ShowZeros ==false)
						{
							m_Value = FormatValue(summary.Value, FormatTypes.Precent,summary.DecimalSpace);
							if(m_Value==string.Empty)
							{								
								break;
							}

						}
						m_Value = FormatValue(summary.Value, FormatTypes.Precent,summary.DecimalSpace) + summary.FollowSummary;  //Added this code at 2009-2-1 16:13:37@Simon
//						m_Value = FormatValue(summary.Value, FormatTypes.Precent) + summary.FollowSummary;
						break;
					case SummaryTypes.Frequence:
					case SummaryTypes.TotalPointsBB:
					case SummaryTypes.Total:
					case SummaryTypes.TotalMinus:
					case SummaryTypes.TotalPlus:
						if(summary.ShowZeros ==false)
						{
							m_Value = FormatValue(summary.Value, FormatTypes.Int,summary.DecimalSpace);
							if(m_Value==string.Empty)
							{								
								break;
							}

						}
						m_Value = FormatValue(summary.Value, FormatTypes.Int,summary.DecimalSpace) + summary.FollowSummary;  //Added this code at 2009-2-1 16:13:37@Simon
//						m_Value = FormatValue(summary.Value, FormatTypes.Int) + summary.FollowSummary;
						break;
					case SummaryTypes.None:
						m_Value = summary.FollowSummary;
						break;
					case SummaryTypes.PieChart:	//07-17-2008@Scott
                        //cell.Tag = summary.Value;
						break;
					default:
						m_Value = FormatValue(summary.Value, FormatTypes.String) + summary.FollowSummary;
						break;
				}
			}

            //return m_Value;

            string before =ReplaceSpecialString(summary.SeparatorBefore);         

            StringBuilder sbValue=new StringBuilder();

            sbValue.Append(before);

            if(m_Value!=string.Empty)sbValue.Append(m_Value);

            foreach (GroupSummary brotherSummary in summary.BrotherSummaries)
            {
                sbValue.Append(FormatValue(cell,brotherSummary));
            }

			return sbValue.ToString();
		}
	}
	#endregion
}