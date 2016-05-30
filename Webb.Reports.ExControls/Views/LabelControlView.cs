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
using System.Globalization;
using System.Security.Permissions;

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
using Webb.Collections;
using Webb.Reports.DataProvider;

namespace Webb.Reports.ExControls.Views
{
	/// <summary>
	/// Summary description for DateTimeControlView.
	/// </summary>
	[Serializable]
	public class LabelControlView : ExControlView,ISerializable
	{
		private Align _Align = Align.Horizontal;
		private int _Width = 0;
		private int _Height = 0;
		private Font _Font = new Font(AppearanceObject.DefaultFont.FontFamily, 10f);
		private Color _TextColor = Color.Black;
		private Color _BackColor = Color.Transparent;
		private string _Text;
        private bool _IsTitle = false;    //09-01-2011 Scott
		private HorzAlignment _HorzAlignment = HorzAlignment.Default;
		private VertAlignment _VertAlignment = VertAlignment.Default;
		private bool _OneValuePerPage = false;
		private string _Field = string.Empty;	//Modified at 2008-12-15 13:28:56@Scott
		protected int _BorderWidth=1;
		protected DevExpress.XtraPrinting.BorderSide _BorderSide=DevExpress.XtraPrinting.BorderSide.None;
		protected Color _BorderColor=Color.Black;

		[NonSerialized]
		private string _FilterdText=string.Empty;

        protected SortingColumnCollection _SortingColumns = new SortingColumnCollection();

		#region Properties
		public string Field
		{
			get{return this._Field;}
			set{this._Field = value;}
		}

        public SortingColumnCollection SortingColumns
        {
            get {
                   if (_SortingColumns == null) _SortingColumns = new SortingColumnCollection();
                    return this._SortingColumns;
                 }
            set { this._SortingColumns = value; }
        }

        public bool IsTitle //09-01-2011 Scott
        {
            get { return _IsTitle; }
            set { _IsTitle = value; }
        }
		public string Text
		{
			get{return this._Text;}
			set{this._Text = value;}
		}
		public DevExpress.XtraPrinting.BorderSide BorderSide
		{
			get{return this._BorderSide;}
			set{this._BorderSide = value;}
		}
		public int BorderWidth
		{
			get{return this._BorderWidth;}
			set{this._BorderWidth = value;}
		}
		public Color BorderColor
		{
			get{return this._BorderColor;}
			set{this._BorderColor = value;}
		}


		public Font Font
		{
			get{return this._Font;}
			set{this._Font = value;}
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

		public int Width
		{
			get{return this._Width;}
			set{this._Width = value;}
		}

		public int Height
		{
			get{return this._Height;}
			set{this._Height = value;}
		}

		public Align Align
		{
			get{return this._Align;}
			set
			{
				if(this._Align == value) return;

				this._Align = value;
			}
		}

		public HorzAlignment HorzAlignment
		{
			get{return this._HorzAlignment;}
			set{this._HorzAlignment = value;}
		}

		public VertAlignment VertAlignment
		{
			get{return this._VertAlignment;}
			set{this._VertAlignment = value;}
		}

		new public bool OneValuePerPage
		{
			get{return this._OneValuePerPage;}
			set{this._OneValuePerPage = value;}
		}
		#endregion

		public LabelControlView(LabelControl i_Control):base(i_Control as ExControl)
		{
			this.CreatePrintingTable();
		}
		
		private WebbDataSource GetWebbDataSource()
		{
			if(this.ExControl == null) return null;

			if(this.ExControl.Report == null) return null;

			WebbReport m_WebbReport = this.ExControl.Report as WebbReport;

			if(m_WebbReport == null) return null;

			return m_WebbReport.WebbDataSource;
		}

		public override void CalculateResult(DataTable i_Table)
		{
            base.CalculateResult (i_Table);

			this._FilterdText=GetFieldValue(i_Table);

			if(_FilterdText=="<NULLVALUES>")
			{
				if(this.Repeat)
				{
					this._FilterdText=this.RepeatFilter.FilterName;
					
				}
				else
				{
					this._FilterdText=this.OneValueScFilter.FilterName;
				}
			}			
		}

		private string GetFieldValue(DataTable dt)	//Modified at 2009-1-4 9:57:34@Scott
		{
			string strRet = "<NULLVALUES>";

            if(dt==null||this.ExControl==null)return "<NULLVALUES>"; 
 
			if(_Field==null||(this._Field==string.Empty)||_Field=="<None>"||!dt.Columns.Contains(_Field))return "<NULLVALUES>"; 	

			Int32Collection m_Rows=this.OneValueScFilter.Filter.GetFilteredRows(dt);

			if(this.ExControl.Report!=null)m_Rows=this.ExControl.Report.Filter.GetFilteredRows(dt,m_Rows);

			m_Rows=this.RepeatFilter.Filter.GetFilteredRows(dt,m_Rows);

            if (this.SortingColumns.Count > 0)
            {
                GroupResultCollection results = this.SortingColumns.Sorting(dt, m_Rows);

                if (results != null && results.Count != 0)
                {
                    m_Rows = results[0].RowIndicators;
                }
            }

			if(m_Rows.Count==0)return string.Empty; 

			int firstRow=m_Rows[0];

            object objResult = CResolveFieldValue.GetResolveValue(_Field, "M/d/yy", dt.Rows[firstRow][_Field]);
             
            strRet = objResult.ToString();
          
			return strRet;
		}


		public override bool CreatePrintingTable()
		{
			if(this.PrintingTable == null)
			{
				this.PrintingTable = new WebbTable(1,1);
			}

			IWebbTableCell m_Cell = this.PrintingTable.GetCell(0,0);

			this.SetCellStyle(m_Cell);

			return true;
        }
      
        private string GetText()
        {
            string strRet = string.Empty;

            if (this.ExControl.Report == null)
            {
                return this.Text;
            }

            if (this.OneValuePerPage)
            {
                strRet = this._FilterdText;
            }           
            else
            {
                strRet = this.Text;
            }

            return strRet;
        }

        public override void GetALLUsedFields(ref ArrayList usedFields)
        {
            if (!usedFields.Contains(this.Field)) usedFields.Add(this.Field);
        }

        private void SetCellStyle(IWebbTableCell cell)
		{
			cell.CellStyle.HorzAlignment = this.HorzAlignment;

			cell.CellStyle.VertAlignment = this.VertAlignment;

			cell.CellStyle.Font = this._Font;

			cell.CellStyle.ForeColor = this.TextColor;

			cell.CellStyle.BackgroundColor = this.BackColor;

//			cell.CellStyle.BorderWidth = 0;
//
//			cell.CellStyle.BorderColor = Color.Transparent;

			cell.CellStyle.Sides=this.BorderSide;

            cell.CellStyle.BorderColor=this.BorderColor;

			cell.CellStyle.BorderWidth=this.BorderWidth;

			cell.CellStyle.BorderStyle=BrickBorderStyle.Inset;

	    	cell.Text = this.GetText();  //Add at 2009-2-24 10:18:00@Simon 
			
			SizeF m_Size = this.ExControl.Bounds.Size;//this.MeasureSize(cell);

			if(this.ThreeD)
			{
				cell.CellStyle.Width = (int)m_Size.Width - 5;

				cell.CellStyle.Height = (int)m_Size.Height - 5;
			}
			else
			{
				cell.CellStyle.Width = (int)m_Size.Width;

				cell.CellStyle.Height = (int)m_Size.Height;
			}
		}

		public void SetSize(int i_Width, int i_Height)
		{
//			this._Width = i_Width;
//
//			this._Height = i_Height;
//		
			IWebbTableCell m_Cell = this.PrintingTable.GetCell(0,0);
//
//			SizeF m_Size = this.MeasureSize(m_Cell);

			if(this.ThreeD)
			{
				m_Cell.CellStyle.Width = i_Width - 5;

				m_Cell.CellStyle.Height = i_Height - 5;
			}
			else
			{
				m_Cell.CellStyle.Width = i_Width;

				m_Cell.CellStyle.Height = i_Height;
			}
		}

        public int SetAutoWapSize()
        {
            this.CreatePrintingTable();

            Graphics m_Graph = this.ExControl.CreateGraphics();

            SizeF m_Size = SizeF.Empty;

            int OldControlHeight = this.ExControl.Height;

            IWebbTableCell i_Cell = this.PrintingTable.GetCell(0, 0);

            m_Size = m_Graph.MeasureString(i_Cell.Text, i_Cell.CellStyle.Font, this.ExControl.Width);          

            this.ExControl.Height = Math.Max(OldControlHeight, (int)m_Size.Height + 2);

            return this.ExControl.Height - OldControlHeight; 

        }

		public SizeF MeasureSize(IWebbTableCell i_Cell)
		{
			Graphics m_Graph = this.ExControl.CreateGraphics();

			SizeF m_Size = SizeF.Empty;

			m_Size = m_Graph.MeasureString(i_Cell.Text,i_Cell.CellStyle.Font);

			m_Size.Width += 5;

			m_Size.Height += 5;

			return m_Size;
		}

		public override void Paint(PaintEventArgs e)
		{//Modified at 2009-1-7 13:47:47@Scott
//			if(this.PrintingTable != null)
//			{
//				if(this.ThreeD)
//				{
//					this.PrintingTable.PaintTable3D(e,true,Rectangle.Empty);
//				}
//				else
//				{
//					this.PrintingTable.PaintTable(e,true,Rectangle.Empty);
//				}
//			}
//			else
//			{
//				base.Paint (e);
//			}
		}

		//Modified at 2009-1-7 14:27:57@Scott
		public override void DrawContent(IGraphics gr, RectangleF rectf)
		{
			base.DrawContent (gr, rectf);

			if(this.PrintingTable != null)
			{
				WebbTableCell m_Cell = this.PrintingTable.GetCell(0,0) as WebbTableCell;

				if(m_Cell != null)
				{
					if(this.ThreeD)
					{
						int nSpace = (int)GraphicsUnitConverter.Convert(5,GraphicsUnit.Pixel,GraphicsUnit.Document);

						rectf.Width -= nSpace;

						rectf.Height -= nSpace;

						rectf.Offset(nSpace,nSpace);

						m_Cell.DrawShadowContent(gr,rectf);

						rectf.Offset(-nSpace,-nSpace);

						m_Cell.DrawContent(gr,rectf);

						m_Cell.DrawBorderContent(gr, rectf);
					}
					else
					{
						m_Cell.DrawContent(gr,rectf);

						m_Cell.DrawBorderContent(gr, rectf);
					}
				}
			}
		}

		public override int CreateArea(string areaName, IBrickGraphics graph)
		{
			if(PrintingTable!=null)
			{
				this.PrintingTable.ExControl=this.ExControl;

//				return PrintingTable.CreateTestArea(areaName,graph);
			}
			return base.CreateArea (areaName, graph);

		}

		#region ISerializable Members
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData (info, context);
			
			info.AddValue("Height",this.Height);
			
			info.AddValue("Width",this.Width);

			info.AddValue("Font",this.Font,typeof(Font));

			info.AddValue("TextColor",this.TextColor,typeof(Color));

			info.AddValue("BackColor",this.BackColor,typeof(Color));

			info.AddValue("Text",this.Text);

			info.AddValue("Align",this.Align,typeof(Align));

			info.AddValue("HorzAlignment",this.HorzAlignment,typeof(HorzAlignment));

			info.AddValue("VertAlignment",this.VertAlignment,typeof(VertAlignment));

			info.AddValue("OneValuePerPage",this.OneValuePerPage);

			info.AddValue("Field",this.Field);

			info.AddValue("BorderWidth",this.BorderWidth);

			info.AddValue("BorderColor",this.BorderColor,typeof(Color));

			info.AddValue("BorderSide",this.BorderSide,typeof(DevExpress.XtraPrinting.BorderSide));

            info.AddValue("_SortingColumns", this._SortingColumns, typeof(SortingColumnCollection));

            info.AddValue("IsTitle", this.IsTitle); //09-01-2011 Scott
		}

		public LabelControlView(SerializationInfo info, StreamingContext context):base(info,context)
		{
			try
			{
				this.BorderSide =(DevExpress.XtraPrinting.BorderSide)info.GetValue("BorderSide",typeof(DevExpress.XtraPrinting.BorderSide));
			}
			catch
			{
				this.BorderSide = DevExpress.XtraPrinting.BorderSide.None;
			}
			try
			{
				this.BorderColor = (Color)info.GetValue("BorderColor",typeof(Color));
			}
			catch
			{
				this.BorderColor = Color.Black;
			}
			try
			{
				this.BorderWidth = info.GetInt32("BorderWidth");
			}
			catch
			{
				this.BorderWidth = 1;
			}
			try
			{
				this.Height = info.GetInt32("Height");
			}
			catch
			{
				this.Height = 0;
			}

			try
			{
				this.Width = info.GetInt32("Width");
			}
			catch
			{
				this.Width = 0;
			}

			try
			{
				this.Font = info.GetValue("Font",typeof(Font)) as Font;
			}
			catch
			{
				this.Font = new Font(AppearanceObject.DefaultFont.FontFamily, 10f);
			}

			try
			{
				this.TextColor = (Color)info.GetValue("TextColor",typeof(Color));
			}
			catch
			{
				this.TextColor = Color.Black;
			}

			try
			{
				this.BackColor = (Color)info.GetValue("BackColor",typeof(Color));
			}
			catch
			{
				this.BackColor = Color.Transparent;
			}

			try
			{
				this.Text = info.GetString("Text");
			}
			catch
			{
				this.Text = string.Empty;
			}

			try
			{
				this.Align = (Align)info.GetValue("Align",typeof(Align));
			}
			catch
			{
				this.Align = Align.Horizontal;
			}

			try
			{
				this.HorzAlignment = (HorzAlignment)info.GetValue("HorzAlignment",typeof(HorzAlignment));
			}
			catch
			{
				this.HorzAlignment = HorzAlignment.Default;
			}

			try
			{
				this.VertAlignment = (VertAlignment)info.GetValue("VertAlignment",typeof(VertAlignment));
			}
			catch
			{
				this.VertAlignment = VertAlignment.Default;
			}

			try
			{
				this.OneValuePerPage = info.GetBoolean("OneValuePerPage");
			}
			catch
			{
				this.OneValuePerPage = false;
			}

			try
			{
				this.Field = info.GetString("Field");
			}
			catch
			{
				this.Field = string.Empty;
			}
            try
            {
                this._SortingColumns = (SortingColumnCollection)info.GetValue("_SortingColumns", typeof(SortingColumnCollection));
            }
            catch
            {
                this._SortingColumns = new SortingColumnCollection();
            }
            //09-01-2011 Scott
            try
            {
                this.IsTitle = info.GetBoolean("IsTitle");
            }
            catch
            {
                this.IsTitle = false;
            }
		}

		#endregion
	}
}