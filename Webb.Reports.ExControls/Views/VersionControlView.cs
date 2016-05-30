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
	public class VersionControlView : ExControlView,ISerializable
	{
		private Align _Align = Align.Horizontal;
		private int _Width = 0;
		private int _Height = 0;
		private Font _Font = new Font(AppearanceObject.DefaultFont.FontFamily, 8f);
		private Color _TextColor = Color.Black;
		private Color _BackColor = Color.Transparent;
		private string _Text;
		private HorzAlignment _HorzAlignment = HorzAlignment.Default;
		private string _FormattedText="Webb Report Designer Ver {0}";              //Added this code at 2008-11-20 9:48:08@Simon

		public string FormattedText             //Added this code at 2008-11-20 9:49:26@Simon
		{
			get{return this._FormattedText;}
			set{this._FormattedText=value;}
		}
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

		public VersionControlView(VersionControl i_Control):base(i_Control as ExControl)
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

		private string GetVersionString()
		{	
			if(Webb.Assembly.Version.Length>7)
			{
				string Ver=Webb.Assembly.Version.Substring(0,7);
				string InternalVer=Webb.Assembly.Version.Remove(0,7);
				if(InternalVer.Length!=1)
				{
					return string.Format(this.FormattedText,"Internal "+ Ver+"."+InternalVer);
				}
				else
				{
					int number=int.Parse(InternalVer);
					char a=(char)((short)'a'+number);
                    return string.Format(this.FormattedText,Ver+a.ToString());
				}
			}
			return string.Format(this.FormattedText,Webb.Assembly.Version);  
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

			cell.Text = this.GetVersionString();
			
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
		{//Modified at 2009-1-7 14:30:45@Scott
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
					m_Cell.DrawContent(gr,rectf);
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

			info.AddValue("FormattedText",this.FormattedText);  //Added this code at 2008-11-20 9:52:15@Simon
		}

		public VersionControlView(SerializationInfo info, StreamingContext context):base(info,context)
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

			try                                                               //Added this code at 2008-11-20 9:54:36@Simon
			{
				this.FormattedText=info.GetString("FormattedText");
			}
			catch
			{
				this.FormattedText="";
			}
		}

		#endregion
	}
}
