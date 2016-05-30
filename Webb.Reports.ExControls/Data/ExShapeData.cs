using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Runtime.Serialization;

using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Container;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraPrinting;
using DevExpress.Utils.Design;
using DevExpress.Utils;

namespace Webb.Reports.ExControls.Data
{   
	#region Enum Data For Shape
	public enum ShapeStyleMode     
	{
		Line=0,
		Rectangle,
		Ellipse,
		//		Arrow,    	
		//		Triangle,
		//		Square	    
	  
	}
	public enum LineArrowStyle
	{
			None,
		StartCapArrow,
		EndCapArrow,
		AllArrow
	}
	public enum LineStyle  //线条样式
	{
		Horizontal,
		Slant,
		Backslant,
		Vertical   	  
	}
	public enum FillStyle
	{
		None,
		Solid,
		Hatch,
		Picture
	}
	#endregion

	#region public class ShapeEx:ISerializable
	[Serializable]
	public class ShapeEx:ISerializable
	{  
		protected Color _BorderColor=Color.Black;
		protected DashStyle _BorderStyle=DashStyle.Solid;
		protected int _BorderWidth=1;
		protected float _Angle=0f;

		private HatchStyle _HatchStyle=HatchStyle.Horizontal;  //Added this code at 2009-1-7 14:08:25@Simon
		private FillStyle _FillStyle=FillStyle.None;

		#region Constructor&Serialization Functions
         public ShapeEx()
		{
			
		}
		public ShapeEx(ShapeEx _ShapeEx)
		{
			this._BorderWidth=_ShapeEx._BorderWidth;
			this._BorderStyle=_ShapeEx.BorderStyle;
			this._Angle=_ShapeEx._Angle;
			this._BorderColor=_ShapeEx._BorderColor;

		     _HatchStyle=_ShapeEx.HatchStyle;  //Added this code at 2009-1-7 14:08:25@Simon
		     _FillStyle=_ShapeEx.FillStyle;
		}
		public virtual ShapeEx Copy()
		{
			return new ShapeEx(this);
		}
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)   
		{
			info.AddValue("_BorderColor",this._BorderColor,typeof(Color));
			info.AddValue("_BorderStyle",this._BorderStyle,typeof(DashStyle));
			info.AddValue("_BorderWidth",this._BorderWidth);
			info.AddValue("_Angle",this._Angle);

			info.AddValue("_FillStyle",this._FillStyle,typeof(FillStyle));
			info.AddValue("_HatchStyle",this._HatchStyle,typeof(HatchStyle));
            
		}
		
		
		public ShapeEx(SerializationInfo info, StreamingContext context)
		{		
			try
			{
				this._BorderColor=(Color)info.GetValue("_BorderColor",typeof(Color));
			}
			catch
			{
				this._BorderColor=Color.Black;
			}
			try
			{
				this._BorderStyle=(DashStyle)info.GetValue("_BorderStyle",typeof(DashStyle));
			}
			catch
			{
				this._BorderStyle=DashStyle.Solid;
			}
			try
			{
				this._BorderWidth=(int)info.GetInt32("_BorderWidth");
			}
			catch
			{
				this._BorderWidth=1;
			}			
			try
			{
				this._Angle=(float)info.GetSingle("_Angle");
			}
			catch
			{
				this._Angle=0.0f;
			}
			try
			{
				this._FillStyle=(FillStyle)info.GetValue("_FillStyle",typeof(FillStyle));
			}
			catch
			{
				this._FillStyle=FillStyle.None;
			}
			try
			{
				this._HatchStyle=(HatchStyle)info.GetValue("_HatchStyle",typeof(HatchStyle));
			}
			catch
			{
				this._HatchStyle=HatchStyle.Horizontal;
			}
			
		}

		#endregion

		public FillStyle FillStyle
		{
			get{return this._FillStyle;}
			set{this._FillStyle=value;}
		}
		public HatchStyle HatchStyle
		{
			get{return this._HatchStyle;}
			set{ this._HatchStyle=value; }
		}

		public DashStyle BorderStyle
		{
			get{return this._BorderStyle;}
			set{this._BorderStyle=value;}
		}
		public Color BorderColor
		{
			get{  return _BorderColor; }
			set{ _BorderColor=value;  }
		}
		public int BorderWidth
		{
			get{  return _BorderWidth; }
			set
			{
				_BorderWidth=value;                  
                    
			}
		}
		public Pen pen
		{
			get
			{
				float[] dashpattern;			
				Pen thispen = new Pen(this._BorderColor);
				thispen.Width = this._BorderWidth;
				if(this._BorderWidth>1||this._BorderStyle==DashStyle.Solid)
				{
					thispen.DashStyle=this._BorderStyle;					
				}
				else
				{  
					switch(this._BorderStyle)
					{
						case DashStyle.Dot:
							dashpattern=new float[]{1,3};
							break;
						case DashStyle.Dash:
							dashpattern=new float[]{6,2};
							break;
						case DashStyle.DashDot:
							dashpattern=new float[]{6,2,3,2};
							break;
						case DashStyle.DashDotDot:
							dashpattern=new float[]{8,2,3,2,3,2};
							break;						
						default:
							dashpattern=new float[]{5,6,2,3,6};
							break;
					}					
					thispen.DashPattern=dashpattern;
					thispen.Width=this._BorderWidth;		     			
				}

				return thispen;
			}
		}
		protected PointF TranslatePointF(PointF point)
		{
			point=GraphicsUnitConverter.Convert(point,GraphicsDpi.Pixel,GraphicsDpi.Document);		
			return point;
		}
		protected float TranslateWidth(float width)
		{
			width=GraphicsUnitConverter.PixelToDoc(width);			
			return width;
		}

		public virtual void OnDraw(IGraphics graphics,RectangleF rectBorder,bool Three3D)
		{
		}
		public virtual void CreateArea(BrickGraphics graph,RectangleF rectBorder,bool Three3D)
		{
		}
	}
	#endregion
	
	#region public class LineEx:ShapeEx
	[Serializable]
	public class LineEx:ShapeEx
	{
		private LineStyle _LineStyle=LineStyle.Horizontal;
		private LineArrowStyle _LineArrow=LineArrowStyle.None;
		public LineEx()
		{
		}
		public LineEx(LineStyle line)
		{
			this._LineStyle=line;
		}

		public LineEx(LineEx line):base(line)
		{
			this._LineStyle=line._LineStyle;
		}  
		public override ShapeEx Copy()
		{
			return new LineEx(this);
		}

		public  override void GetObjectData(SerializationInfo info, StreamingContext context)   
		{	
			base.GetObjectData(info,context);
			info.AddValue("_LineStyle",this._LineStyle,typeof(LineStyle));
			info.AddValue("_LineArrow",this._LineArrow,typeof(LineArrowStyle));
				
		}			
		public LineEx(SerializationInfo info, StreamingContext context):base(info,context)
		{		
			try
			{
				this._LineStyle=(LineStyle)info.GetValue("_LineStyle",typeof(LineStyle));
			}
			catch
			{
				this._LineStyle=LineStyle.Horizontal;
			}
			try
			{
				this._LineArrow=(LineArrowStyle)info.GetValue("_LineArrow",typeof(LineArrowStyle));
			}
			catch
			{
				this._LineArrow=LineArrowStyle.None;
			}
		}
		public LineStyle LineDirection
		{
			get{return this._LineStyle;}
			set
			{
				this._LineStyle=value;
			}
		}

		public LineArrowStyle LineArrow
		{
			get{return this._LineArrow;}
			set
			{
				this._LineArrow=value;
			}
		}
		protected Pen ArrowPen()
		{
			Pen drawpen=this.pen;
            switch(this._LineArrow)
			{
					case LineArrowStyle.AllArrow:
							drawpen.StartCap=LineCap.ArrowAnchor;
							drawpen.EndCap=LineCap.ArrowAnchor;
							break;
				  case LineArrowStyle.EndCapArrow:
					drawpen.StartCap=LineCap.NoAnchor;
					drawpen.EndCap=LineCap.ArrowAnchor;
					break;
				case LineArrowStyle.StartCapArrow:
					drawpen.StartCap=LineCap.ArrowAnchor;
					drawpen.EndCap=LineCap.NoAnchor;
					break;
				case LineArrowStyle.None:
					drawpen.StartCap=LineCap.NoAnchor;
					drawpen.EndCap=LineCap.NoAnchor;
					break;
			}	
			return 	drawpen;   
			      
		}
		protected PointF[] DrawPoints(RectangleF rectBorder)
		{
			float xstart0=0,xend0=0;
			float ystart0=0,yend0=0;  
  
			PointF[] points=new PointF[2];

			switch(this._LineStyle)
			{
				case LineStyle.Horizontal:	
					xstart0=rectBorder.X;
					ystart0=rectBorder.Y+(rectBorder.Height/2);
					xend0=rectBorder.X+rectBorder.Width;
					yend0=rectBorder.Y+(rectBorder.Height/2);
					break;
				case LineStyle.Vertical:
					xstart0=rectBorder.X+rectBorder.Width/2;
					ystart0=rectBorder.Y;
					xend0=rectBorder.X+rectBorder.Width/2;
					yend0=rectBorder.Y+rectBorder.Height;
					break;
				case LineStyle.Slant:
					xstart0=rectBorder.X;
					ystart0=rectBorder.Y;
					xend0=rectBorder.X+rectBorder.Width;
					yend0=rectBorder.Y+rectBorder.Height;
					break;
				case LineStyle.Backslant:
					xstart0=rectBorder.X+rectBorder.Width;
					ystart0=rectBorder.Y;
					xend0=rectBorder.X;
					yend0=rectBorder.Y+rectBorder.Height;				
					break;
					   
			}
			points[0]=new PointF(xstart0,ystart0);
			points[1]=new PointF(xend0,yend0);
			return points;
		}
		public override  void OnDraw(IGraphics graphics,RectangleF rectBorder,bool Three3D)
		{	
			if(this.BorderWidth<=0)return;		 
			graphics.SmoothingMode=SmoothingMode.HighQuality;

			Pen drawpen=this.ArrowPen();

			float width=this.BorderWidth;

			width=this.TranslateWidth(width);
		
			drawpen.Width=width;
		
            PointF[] points=this.DrawPoints(rectBorder);			
			graphics.DrawLine(drawpen,points[0],points[1]);
		}

		public override void CreateArea(BrickGraphics graph,RectangleF rectBorder,bool Three3D)
		{			
			Pen drawpen=this.ArrowPen();
			PointF[] points=this.DrawPoints(rectBorder);	
            graph.DrawShape(drawpen,0,points[0],points[1],this.BorderColor,this.BorderColor,this.BorderWidth,Brushes.Black,Three3D);		   
		}	
		
	}
	
	#endregion
	
	#region class RectangleEx:ShapeEx
	[Serializable]
	public class RectangleEx:ShapeEx 
	{
		protected Color _FillColor=Color.Red; 
		protected Image _FillImage=null;  
		public RectangleEx()
		{
		}
		public RectangleEx(Color fillcolor)
		{
			this._FillColor=fillcolor;
			_FillImage=null;  
				
		}
		public RectangleEx(RectangleEx _RectangleEx):base(_RectangleEx)
		{   
			this._FillColor=_RectangleEx._FillColor;
			_FillImage=_RectangleEx._FillImage;			
		}
		public override ShapeEx Copy()
		{
			return new RectangleEx(this);
		}
		 
		public Color FillColor
		{
			get{return this._FillColor;}
			set{this._FillColor=value;}
		}
		public Image FillImage
		{
			get{return this._FillImage;}
			set{this._FillImage=value;}
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)   
		{	
			base.GetObjectData(info,context);
			info.AddValue("_FillColor",this._FillColor,typeof(Color));
			info.AddValue("_FillImage",this._FillImage,typeof(Image));
				
		}			
		public RectangleEx(SerializationInfo info, StreamingContext context):base(info,context)
		{		
			try
			{
				this._FillColor=(Color)info.GetValue("_FillColor",typeof(Color));
			}
			catch
			{
				this._FillColor=Color.Red;
			} 
			try
			{
				this._FillImage=(Image)info.GetValue("_FillImage",typeof(Image));
			}
			catch
			{
				this._FillImage=null;
			}         
		}
		public override  void OnDraw(IGraphics graphics,RectangleF rectBorder,bool Three3D)
		{
			float width= this.BorderWidth;

			width=this.TranslateWidth(width);         

			Pen drawpen=this.pen;	

			drawpen.Width=width;			
		
			float Infloatwidth=width/2;

			rectBorder.Inflate(-Infloatwidth,-Infloatwidth);			
           
			Brush brush;

			if(this.FillStyle==FillStyle.Hatch)
			{
    			brush=new HatchBrush(this.HatchStyle,this.BorderColor,this.FillColor);
			}
			else if(this.FillStyle==FillStyle.Solid)
			{
				brush=new SolidBrush(this.FillColor);
			}
			else if(this.FillStyle==FillStyle.Picture&&this.FillImage!=null)
			{
				brush=new TextureBrush(this.FillImage);	
				(brush as TextureBrush).TranslateTransform(rectBorder.X,rectBorder.Y);
			}
			else
			{
				brush=new SolidBrush(Color.Transparent);
			}

			bool Fit3DAll=Three3D&&(brush is SolidBrush);
			Fit3DAll=Fit3DAll &&(rectBorder.Width>=20&&rectBorder.Height>=20);

			if(Fit3DAll)
			{ 
				this.Draw3DRect(graphics,rectBorder,brush,drawpen,Infloatwidth);				
			}
			else
			{              
				graphics.FillRectangle(brush,rectBorder);	
				if(this.BorderWidth>0)		 
					graphics.DrawRectangle(drawpen,rectBorder);

			} 
		}

		private void Draw3DRect(IGraphics graphics,RectangleF rectBorder,Brush brush,Pen drawpen,float Infloatwidth)
		{
			#region Draw3DRect
			
			float Len3D=0;
			if(rectBorder.Width<30)
			{
				Len3D=rectBorder.Width/2;                
			}
			else
			{
			   Len3D=rectBorder.Width/5f;
			}
            float tempWidth=Infloatwidth;
			Infloatwidth=Infloatwidth*(float)(Math.Sqrt(2)/2);
			float DistWidth=tempWidth-Infloatwidth;
			
			RectangleF BarRect=new RectangleF(rectBorder.X,rectBorder.Y+Len3D,rectBorder.Width-Len3D,rectBorder.Height-Len3D);

			graphics.FillRectangle(brush,BarRect);

			float BarLeft=BarRect.X-DistWidth;
			float BarRight=BarRect.X+BarRect.Width+DistWidth;					 
			float BarTop=BarRect.Y-DistWidth;
			float BarBottom=BarRect.Y+BarRect.Height+DistWidth;

			GraphicsPath path3d=new GraphicsPath();
			PointF[] pathpoints=new PointF[5];
			pathpoints[0]=new PointF(BarLeft,BarTop);
			pathpoints[1]=new PointF(BarLeft+Len3D,BarTop-Len3D);
			pathpoints[2]=new PointF(BarRight+Len3D,BarTop-Len3D);
			pathpoints[3]=new PointF(BarRight,BarTop);
			pathpoints[4]=new PointF(BarLeft,BarTop);

			path3d.AddLines(pathpoints);
			graphics.FillPath(brush,path3d);       

			PointF StartPoint=new PointF(BarLeft,BarTop);
			PointF EndPoint=new PointF(BarLeft+Len3D,BarTop-Len3D);

			if(this.BorderWidth>0)	
			{
				graphics.DrawLine(drawpen,StartPoint,EndPoint);	

				StartPoint.X=BarLeft+Len3D-Infloatwidth;
				StartPoint.Y=BarTop-Len3D+DistWidth;				
				EndPoint.X=BarRight+Len3D+Infloatwidth;
				EndPoint.Y=BarTop-Len3D+DistWidth;

				graphics.DrawLine(drawpen,StartPoint,EndPoint);		
			}

			path3d=new GraphicsPath();
			pathpoints[0]=new PointF(BarRight,BarTop);
			pathpoints[1]=new PointF(BarRight+Len3D,BarTop-Len3D);
			pathpoints[2]=new PointF(BarRight+Len3D,BarBottom-Len3D);
			pathpoints[3]=new PointF(BarRight,BarBottom);
			pathpoints[4]=new PointF(BarRight,BarTop);
			path3d.AddLines(pathpoints);
			graphics.FillPath(brush,path3d);

			if(this.BorderWidth>0)	
			{	
				StartPoint.X=BarRight;
				StartPoint.Y=BarTop;				
				EndPoint.X=BarRight+Len3D;
				EndPoint.Y=BarTop-Len3D;

				graphics.DrawLine(drawpen,StartPoint,EndPoint);

				StartPoint.X=BarRight+Len3D-DistWidth;
				StartPoint.Y=BarTop-Len3D+DistWidth;				
				EndPoint.X=BarRight+Len3D-DistWidth;
				EndPoint.Y=BarBottom-Len3D+DistWidth;	

				graphics.DrawLine(drawpen,StartPoint,EndPoint);
              
				StartPoint.X=BarRight;
				StartPoint.Y=BarBottom;				
				EndPoint.X=BarRight+Len3D;
				EndPoint.Y=BarBottom-Len3D-DistWidth;
				graphics.DrawLine(drawpen,StartPoint,EndPoint);					

				graphics.DrawRectangle(drawpen,BarRect);


			}
			#endregion
		}

		public override void CreateArea(BrickGraphics graph, RectangleF rectBorder,bool Three3D)
		{
			PointF pt1=rectBorder.Location;
            PointF pt2=new PointF(rectBorder.X+rectBorder.Width,rectBorder.Y+rectBorder.Height);
			Brush brush;
			if(this.FillStyle==FillStyle.Hatch)
			{
				brush=new HatchBrush(this.HatchStyle,this.BorderColor,this.FillColor);
			}
			else if(this.FillStyle==FillStyle.Solid)
			{
				brush=new SolidBrush(this.FillColor);
			}
			else if(this.FillStyle==FillStyle.Picture&&this.FillImage!=null)
			{
				brush=new TextureBrush(this.FillImage);
			}
			else
			{
				brush=new SolidBrush(Color.Transparent);
			}
			graph.DrawShape(this.pen,1,pt1,pt2,this.BorderColor,this._FillColor,this.BorderWidth,brush,Three3D);
		}


	}
	#endregion

	#region class EllipseEx
	[Serializable]
	public class EllipseEx:ShapeEx
	{
		private Color _EillColor=Color.Red; 
		protected Image _FillImage=null;  
		public EllipseEx()
		{
		}
		public EllipseEx(Color fillcolor)
		{
			this._EillColor=fillcolor;
			
		}
		public EllipseEx(EllipseEx _EllipseEx):base(_EllipseEx)
		{   
			 _EillColor=_EllipseEx._EillColor;	
		     _FillImage=_EllipseEx._FillImage;
		}
        public override ShapeEx Copy()
        {
            return new EllipseEx(this);
        }
		 public Color FillColor
		{
			get{return this._EillColor;}
			set{this._EillColor=value;}
		}
		public Image FillImage
		{
			get{return this._FillImage;}
			set{this._FillImage=value;}
		}
		public override void GetObjectData(SerializationInfo info, StreamingContext context)   
		{	
			base.GetObjectData(info,context);
			info.AddValue("_EillColor",this._EillColor,typeof(Color));
			info.AddValue("_FillImage",this._FillImage,typeof(Image));				
		}			
		public EllipseEx(SerializationInfo info, StreamingContext context):base(info,context)
		{		
			try
			{
				this._EillColor=(Color)info.GetValue("_EillColor",typeof(Color));
			}
			catch
			{
				this._EillColor=Color.Red;
			}
			try
			{
				this._FillImage=(Image)info.GetValue("_FillImage",typeof(Image));
			}
			catch
			{
				this._FillImage=null;
			}         
			
		}
		private  void Draw3DEllipse(IGraphics graphics,RectangleF rectBorder,Brush brush,Pen drawpen,float Infloatwidth )
	    {            
			float rectDiff=rectBorder.Height/2;

			float tempWidth=Infloatwidth;
			Infloatwidth=Infloatwidth*(float)(Math.Sqrt(2)/2);
			float DistWidth=tempWidth-Infloatwidth;

            RectangleF rectfPie=new RectangleF(rectBorder.X,rectBorder.Y,rectBorder.Width,rectBorder.Height-rectDiff);
			RectangleF rectfPie3d=new RectangleF(rectBorder.X,rectBorder.Y+rectDiff,rectBorder.Width,rectfPie.Height);

            graphics.FillEllipse(brush,rectfPie);

			GraphicsPath path=new GraphicsPath();
			path.AddArc(rectfPie.X,rectfPie.Y,rectfPie.Width,rectfPie.Height,0,180); 
			path.AddArc(rectfPie3d.X,rectfPie3d.Y,rectfPie3d.Width,rectfPie3d.Height,180,-180); 
			graphics.FillPath(brush,path);   
			
			if(this.BorderWidth>0)
			{
				#region Draw
				PointF point1=new PointF(rectfPie.X,rectfPie.Y+rectfPie.Height/2+2f); 
				PointF point2=new PointF(rectfPie3d.X,rectfPie3d.Y+rectfPie3d.Height/2+2f);	
				PointF point3=new PointF(rectfPie3d.X+rectfPie3d.Width,rectfPie3d.Y+rectfPie3d.Height/2+2f);
				PointF point4=new PointF(rectfPie.X+rectfPie.Width,rectfPie.Y+rectfPie.Height/2+2f);

				
				path.Reset();      
				path.AddArc(rectfPie3d.X,rectfPie3d.Y-DistWidth,rectfPie3d.Width,rectfPie3d.Height,180,-180);        
				graphics.DrawPath(drawpen,path);	
			
				graphics.DrawEllipse(drawpen,rectfPie);
				graphics.DrawLine(drawpen,point1,point2);
				graphics.DrawLine(drawpen,point3,point4);
				#endregion
			}           
		}
		public override  void OnDraw(IGraphics graphics,RectangleF rectBorder,bool Three3D)
		{
			float width=this.BorderWidth;

			width=this.TranslateWidth(width);

			Pen drawpen=this.pen;	

			drawpen.Width=width;
			
			float Infloatwidth=width/2;

			RectangleF rectPie=rectBorder;

			rectBorder.Inflate((-1)*Infloatwidth,(-1)*Infloatwidth);

			Brush brush;

			if(this.FillStyle==FillStyle.Hatch)
			{
				brush=new HatchBrush(this.HatchStyle,this.BorderColor,this.FillColor);
			}
			else if(this.FillStyle==FillStyle.Solid)
			{
				brush=new SolidBrush(this.FillColor);
			}
			else if(this.FillStyle==FillStyle.Picture&&this.FillImage!=null)
			{
				 brush=new TextureBrush(this.FillImage);
				(brush as TextureBrush).WrapMode=WrapMode.Clamp;
				(brush as TextureBrush).TranslateTransform(rectBorder.X,rectBorder.Y);
			}
			else
			{
				brush=new SolidBrush(Color.Transparent);
			}
			bool Fit3DAll=Three3D&&(brush is SolidBrush);
			Fit3DAll=Fit3DAll &&(rectBorder.Height>=20);
			if(Fit3DAll)
			{
				rectBorder.Inflate(-2,-2);
				this.Draw3DEllipse(graphics,rectBorder,brush,drawpen,Infloatwidth);
			}
			else
			{
				graphics.FillEllipse(brush,rectBorder);
				if(this.BorderWidth>0)		  
					graphics.DrawEllipse(drawpen,rectBorder);
			}
		}
		public override void CreateArea(BrickGraphics graph, RectangleF rectBorder,bool Three3D)
		{
			PointF pt1=rectBorder.Location;
			PointF pt2=new PointF(rectBorder.X+rectBorder.Width,rectBorder.Y+rectBorder.Height);

			Brush brush;

			if(this.FillStyle==FillStyle.Hatch)
			{
				brush=new HatchBrush(this.HatchStyle,this.BorderColor,this.FillColor);
			}
			else if(this.FillStyle==FillStyle.Solid)
			{
				brush=new SolidBrush(this.FillColor);
			}
			else if(this.FillStyle==FillStyle.Picture&&this.FillImage!=null)
			{				
				brush=new TextureBrush(this.FillImage);   
			}
			else
			{
				brush=new SolidBrush(Color.Transparent);
			}
			graph.DrawShape(this.pen,2,pt1,pt2,this.BorderColor,this.FillColor,this.BorderWidth,brush,Three3D);
		}
	}
	#endregion

	#region class Shapes
	[Serializable]
	public class Shapes:ISerializable
	{ 	
		private ShapeEx _ShapeEx=new LineEx();			    
		private ShapeStyleMode _ShapeStyle=ShapeStyleMode.Line;
		public void GetObjectData(SerializationInfo info, StreamingContext context)   
		{
			info.AddValue("_ShapeEx",this._ShapeEx,typeof(Webb.Reports.ExControls.Data.ShapeEx));
			info.AddValue("_ShapeStyle",this._ShapeStyle,typeof(ShapeStyleMode));				
		}			
		public Shapes(SerializationInfo info, StreamingContext context)
		{
			try
			{
				this._ShapeStyle=(ShapeStyleMode)info.GetValue("_ShapeStyle",typeof(ShapeStyleMode));
			}
			catch
			{
				this._ShapeStyle=ShapeStyleMode.Line;
			}
			try
			{
				this._ShapeEx=info.GetValue("_ShapeEx",typeof(Webb.Reports.ExControls.Data.ShapeEx)) as Webb.Reports.ExControls.Data.ShapeEx;
			}
			catch
			{
				this._ShapeEx=new LineEx();
			}
		}
              
		public Shapes()
		{			
		}

		public Shapes(Shapes _shape)
		{
			this._ShapeStyle=_shape._ShapeStyle;
			switch(_shape._ShapeStyle)
			{
				case ShapeStyleMode.Line:
					this._ShapeEx=new LineEx(_shape._ShapeEx as LineEx);
					break;
				case ShapeStyleMode.Rectangle:
					this._ShapeEx=new RectangleEx(_shape._ShapeEx as RectangleEx);
					break;
				case ShapeStyleMode.Ellipse:
					this._ShapeEx=new EllipseEx(_shape._ShapeEx as EllipseEx);
					break;
			}
		}
		public Shapes Copy()
		{
			return new Shapes(this);
		}

		public Shapes(ShapeStyleMode shape)
		{
			this._ShapeStyle=shape;
			switch(shape)
			{
				case ShapeStyleMode.Line:
					this._ShapeEx=new LineEx();
					break;
				case ShapeStyleMode.Rectangle:
					this._ShapeEx=new RectangleEx();
					break;
				case ShapeStyleMode.Ellipse:
					this._ShapeEx=new EllipseEx();
					break;
			}
		}
		
			

		[DefaultValue("Black"),Description("Border color"),Category("Appearance")] 
		public Color BorderColor
		{
			get 
			{					
				return this._ShapeEx.BorderColor;
			}
			set 
			{
				this._ShapeEx.BorderColor=value;					
			}
		}
		[DefaultValue(1),Description("Border width"),Category("Appearance")] 
		public int BorderWidth
		{
			get 
			{					
				return this._ShapeEx.BorderWidth;
			}
			set 
			{
				this._ShapeEx.BorderWidth=value;					
			}
		}
		[DefaultValue(1),Description("Border style"),Category("Appearance")] 
		public DashStyle BorderStyle
		{
			get 
			{					
				return this._ShapeEx.BorderStyle;
			}
			set 
			{
				this._ShapeEx.BorderStyle=value;					
			}
		}

		[Description("Shape style mode"),Category("Appearance")] 
		public ShapeStyleMode shape
		{
			get{return this._ShapeStyle;}
			set
			{
				this._ShapeStyle=value;
				switch(shape)
				{
					case ShapeStyleMode.Line:
						if(!(this._ShapeEx is LineEx))
							this._ShapeEx=new LineEx();
						break;
					case ShapeStyleMode.Rectangle:
						if(!(this._ShapeEx is RectangleEx))
							this._ShapeEx=new RectangleEx();
						break;
					case ShapeStyleMode.Ellipse:
						if(!(this._ShapeEx is EllipseEx))
							this._ShapeEx=new EllipseEx();
						break;
				}
			}
		}

		[Description("Line style"),Category("LineDirection")] 
		public LineStyle LineDirection
		{
			get
			{
				if(this._ShapeEx is LineEx) return (this._ShapeEx as LineEx).LineDirection;
				return LineStyle.Horizontal;
			}
			set 
			{
				if(this._ShapeEx is LineEx) (this._ShapeEx as LineEx).LineDirection=(LineStyle)value;
			}
		}

		public FillStyle FillStyle
		{
			get{
				  if(this._ShapeEx is LineEx)return FillStyle.None; 
				  return this._ShapeEx.FillStyle;
			    }
			set{
				if(this._ShapeEx is LineEx)return;
				 this._ShapeEx.FillStyle=value;
			    }
		}
		public HatchStyle HatchStyle
		{
			get{
				 if(this._ShapeEx is LineEx)return HatchStyle.Horizontal; 
				 return this._ShapeEx.HatchStyle;
			   }
			set{
				 if(this._ShapeEx is LineEx)return;
				 this._ShapeEx.HatchStyle=value;
			   }
		}


		[Description("Line Arrow style"),Category("Arrow Style")] 
		public LineArrowStyle LineArrow
		{
			get
			{
				if(this._ShapeEx is LineEx) return (this._ShapeEx as LineEx).LineArrow;
				return LineArrowStyle.None;
			}
			set 
			{
				if(this._ShapeEx is LineEx)
					(this._ShapeEx as LineEx).LineArrow=value;
			}
		}
          
		public Color FillColor
		{
			get
			{			
				if(this._ShapeEx is RectangleEx) return (this._ShapeEx as RectangleEx).FillColor;
				if(this._ShapeEx is EllipseEx) return (this._ShapeEx as EllipseEx).FillColor;
				return Color.FromName("Transparent");
			}
			set 
			{
				if(this._ShapeEx is RectangleEx)(this._ShapeEx as RectangleEx).FillColor=value;
				if(this._ShapeEx is EllipseEx)(this._ShapeEx as EllipseEx).FillColor=value;
			}
		}
		public Image FillImage
		{
			get
			{			
				if(this._ShapeEx is RectangleEx) return (this._ShapeEx as RectangleEx).FillImage;
				if(this._ShapeEx is EllipseEx) return (this._ShapeEx as EllipseEx).FillImage;
				return null;
			}
			set 
			{
				if(this._ShapeEx is RectangleEx)(this._ShapeEx as RectangleEx).FillImage=value;
				if(this._ShapeEx is EllipseEx)(this._ShapeEx as EllipseEx).FillImage=value;
			}
		}
			
		public void OnDraw(IGraphics graph,RectangleF rectf,bool Three3D)
		{
			try
			{
				_ShapeEx.OnDraw(graph,rectf,Three3D);
			}
			catch(Exception E)
			{
				MessageBox.Show( "Can't Redraw Control!");
				throw E;
			}			
		}	
		public void CreateArea(BrickGraphics graph,RectangleF rectBorder,bool Three3D)
		{
			try
			{
				_ShapeEx.CreateArea(graph,rectBorder,Three3D);
			}
			catch(Exception E)
			{
				MessageBox.Show( "Can't Create Shapes's Area!");
				throw E;
			}	
		
		} 
	}
	#endregion
}