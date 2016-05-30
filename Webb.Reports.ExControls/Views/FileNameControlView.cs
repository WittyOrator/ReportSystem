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
	public class FileNameControlView : ExControlView,ISerializable
	{
		private Align _Align = Align.Horizontal;
		private int _Width = 0;
		private int _Height = 0;
		private Font _Font = new Font(AppearanceObject.DefaultFont.FontFamily, 10f);
		private Color _TextColor = Color.Black;
		private Color _BackColor = Color.Transparent;
		private string _Text;
		private HorzAlignment _HorzAlignment = HorzAlignment.Default;

		public string Text
		{
			get{return this._Text;}
			set{this._Text = value;}
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

		public FileNameControlView(FileNameControl i_Control):base(i_Control as ExControl)
		{
			this.CreatePrintingTable();
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

        //private string GetFileNameString()
        //{
        //    string strFileName = Webb.Utility.CurFileName;

        //    int indexBackSlash = strFileName.LastIndexOf(@"\");          

        //    int indexDot = strFileName.LastIndexOf(@".");

        //    if(indexBackSlash > 0 && indexDot > 0 && indexDot > indexBackSlash)
        //    {
        //        strFileName = strFileName.Substring(indexBackSlash + 1,indexDot - indexBackSlash - 1); 
        //    }        

        //    return strFileName;
        //}
        private string GetFileNameString()
		{
			string strFileName = Webb.Utility.CurFileName;

			int indexBackSlash = strFileName.LastIndexOf(@"\");

            if (indexBackSlash >= 0) strFileName = strFileName.Substring(indexBackSlash + 1);

            indexBackSlash = strFileName.LastIndexOf(@"/");

            if (indexBackSlash >= 0) strFileName = strFileName.Substring(indexBackSlash + 1);

			int indexDot = strFileName.LastIndexOf(@".");

			if(indexDot >=0)
			{
				strFileName = strFileName.Substring(0,indexDot); 
			}        

			return strFileName;
		}


		private void SetCellStyle(IWebbTableCell cell)
		{
			cell.CellStyle.HorzAlignment = this.HorzAlignment;
 
			cell.CellStyle.HorzAlignment = HorzAlignment.Near;

			cell.CellStyle.VertAlignment = VertAlignment.Top;

			cell.CellStyle.Font = this._Font;

			cell.CellStyle.ForeColor = this.TextColor;

			cell.CellStyle.BackgroundColor = this.BackColor;

			cell.CellStyle.BorderWidth = 0;

			cell.CellStyle.BorderColor = Color.Transparent;

			cell.Text = this.GetFileNameString();
			
			SizeF m_Size = this.MeasureSize(cell);

			cell.CellStyle.Width = (int)m_Size.Width;

			cell.CellStyle.Height = (int)m_Size.Height;
		}

		public void SetSize(int i_Width, int i_Height)
		{
			this._Width = i_Width;

			this._Height = i_Height;
		
			IWebbTableCell m_Cell = this.PrintingTable.GetCell(0,0);

			SizeF m_Size = this.MeasureSize(m_Cell);

			m_Cell.CellStyle.Width = (int)m_Size.Width;

			m_Cell.CellStyle.Height = (int)m_Size.Height;
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
		{//Modified at 2009-1-7 14:30:05@Scott
//			if(this.PrintingTable != null)
//			{
//				this.PrintingTable.PaintTable(e,true,Rectangle.Empty);
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
                    m_Cell.DrawContent(gr, rectf);
				}
			}
		}

		public override int CreateArea(string areaName, IBrickGraphics graph)
		{
			if(PrintingTable!=null)
			{
				this.PrintingTable.ExControl=this.ExControl;
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
		}

		public FileNameControlView(SerializationInfo info, StreamingContext context):base(info,context)
		{
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
		}

		#endregion
	}
}