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
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Container;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraPrinting;
using DevExpress.Utils.Design;
using DevExpress.Utils;
using Webb.Reports.ExControls;
using System.Drawing.Drawing2D;
using System.Drawing.Design;

using Webb.Reports.ExControls.Data;
using System.Security.Permissions;
using Webb.Collections;
using Webb.Reports.DataProvider;
using Webb.Reports.ExControls.Styles;



namespace Webb.Reports.ExControls.Views
{	
	#region ExShapeView
	[Serializable]
	public class ExShapeView: ExControlView
	{   
		private Shapes _Shape = new Shapes();
		
		private bool _AutoFit=false;	

		public ExShapeView(ExShapeControl i_Control):base(i_Control as ExControl)
		{
		}
		
		override public void GetObjectData(SerializationInfo info, StreamingContext context)   
		{
			base.GetObjectData(info,context);		
			info.AddValue("_Shape",this._Shape,typeof(Webb.Reports.ExControls.Data.Shapes));	
			info.AddValue("_AutoFit",this._AutoFit);	
	
		}
		public ExShapeView(SerializationInfo info, StreamingContext context):base(info,context)
		{
			try
			{
				this._Shape=info.GetValue("_Shape",typeof(Webb.Reports.ExControls.Data.Shapes)) as Webb.Reports.ExControls.Data.Shapes;
			}
			catch
			{
				this._Shape=new Shapes();
			}
			try
			{
				this._AutoFit=info.GetBoolean("_AutoFit");
			}
			catch
			{
				this._AutoFit=false;
			}
			
		}
		public bool AutoFit
		{
			get{return this._AutoFit;}	
			set{this._AutoFit=value;}
		}

		public FillStyle FillStyle
		{
				get{return this._Shape.FillStyle;}
			    set{this._Shape.FillStyle=value;}
		}
		public HatchStyle HatchStyle
		{
			get{return this._Shape.HatchStyle;}
			set{ this._Shape.HatchStyle=value; }
		}

		public Color BorderColor
		{
			get 
			{					
				return this._Shape.BorderColor;
			}
			set 
			{
				this._Shape.BorderColor=value;					
			}
		}
	
		public int BorderWidth
		{
			get 
			{					
				return this._Shape.BorderWidth;
			}
			set 
			{
				this._Shape.BorderWidth=value;					
			}
		}

		public DashStyle BorderStyle
		{
			get 
			{					
				return this._Shape.BorderStyle;
			}
			set 
			{
				this._Shape.BorderStyle=value;					
			}
		}
	
	
		public ShapeStyleMode shape
		{
			get{return this._Shape.shape;}
			set{this._Shape.shape=value;}
		}

	
		public LineStyle LineDirection
		{
			get
			{
				return this._Shape.LineDirection;
			}
			set 
			{
				this._Shape.LineDirection=value;
			}
		}
		public LineArrowStyle LineArrow
		{
			get
			{
				return this._Shape.LineArrow;
			}
			set 
			{
				this._Shape.LineArrow=value;
			}
		}

          
		public Color FillColor
		{
		  get
		    { return this._Shape.FillColor;
		    }
			set 
			{
				this._Shape.FillColor=value;
			}

		} 
		public Image FillImage
		{
			get
			{
				return this._Shape.FillImage;
			}
			set 
			{
				this._Shape.FillImage=value;
			}

		} 


		public override int CreateArea(string areaName, IBrickGraphics graph)
		{
			this.PrintingTable.AdjustSize();

			int m_x = this.PrintingTable.Offset.Width;
			int m_y = this.PrintingTable.Offset.Height;

			
			this.PrintingTable.ExControl=this.ExControl;
			

			WebbTableCell m_cell =this.PrintingTable.GetCell(0,0) as WebbTableCell;
				
			RectangleF rectBorder=new RectangleF(m_x,m_y,m_cell.CellStyle.Width,m_cell.CellStyle.Height);

           this._Shape.CreateArea(graph as BrickGraphics,rectBorder,this.ThreeD);

			return m_y+m_cell.CellStyle.Height;
			
		}
		public override void CalculateResult(DataTable i_Table)
		{
		}
		public override bool CreatePrintingTable()
		{
			if(this.PrintingTable == null)
		   {
			 this.PrintingTable = new WebbTable(1,1);
		   }
			IWebbTableCell m_Cell = this.PrintingTable.GetCell(0,0);
			m_Cell.CellStyle.Width =(int)Rect.Width;
			m_Cell.CellStyle.Height = (int)Rect.Height;      
			return true;
		}

		public override void Paint(PaintEventArgs e)
		{			
		}

		public override void DrawContent(IGraphics gr, RectangleF rectf)
		{
			base.DrawContent (gr, rectf);

			this._Shape.OnDraw(gr,rectf,this.ThreeD);
		}

		public override void UpdateView()
		{			
			base.UpdateView ();
		}

		public RectangleF Rect
		{
			get
			{
				RectangleF newrect=new RectangleF();
				newrect.X=0;
				newrect.Y=0;
				newrect.Width=(float)(this.ExControl.XtraContainer.Width/Webb.Utility.ConvertCoordinate);
				newrect.Height=(float)(this.ExControl.XtraContainer.Height/Webb.Utility.ConvertCoordinate);
				return newrect;
			}
		}

	}	
	#endregion
}