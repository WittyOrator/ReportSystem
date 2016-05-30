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
using System.Drawing.Drawing2D;
using System.Drawing.Design;

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
using Webb.Reports.ExControls.Data;
using Webb.Reports.ExControls.Views;

namespace Webb.Reports.ExControls
{
	#region ExShapeControl
	[XRDesigner("Webb.Reports.ExControls.Design.ExShapeDesigner,Webb.Reports.ExControls"),
	Designer("Webb.Reports.ExControls.Design.ExShapeDesigner,Webb.Reports.ExControls"),
	ToolboxBitmap(typeof(Webb.Reports.ExControls.ResourceManager), "Bitmaps.XRShape.bmp")]
	public class ExShapeControl : ExControl//, IPrintable,IExtendedControl
	{
		public ExShapeControl()
		{
			this.MainView=new ExShapeView(this);
		}
		[Browsable(false)]
		public ExShapeView ExShapeView
		{
			get{return this.MainView as ExShapeView;}
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			this.MainView.Paint(e);
		}
		[Browsable(false)]
		public Rectangle selectrectangle
		{
			get{ return this.Bounds;}
		}

		[DefaultValue("Black"),Description("Border color"),Category("Appearance")] 
		public Color BorderColor
		{
			get 
			{					
				return this.ExShapeView.BorderColor;
			}
			set 
			{
				this.ExShapeView.BorderColor=value;	
				if(DesignMode)this._MainView.UpdateView();
			}
		}
		[DefaultValue(1),Description("Border width"),Category("Appearance")] 
		public int BorderWidth
		{
			get 
			{					
				return this.ExShapeView.BorderWidth;
			}
			set 
			{
				this.ExShapeView.BorderWidth=value;
				if(DesignMode)
			    	this._MainView.UpdateView();	
			}
		}
		[DefaultValue(1),Description("Border style"),Category("Appearance")] 
		public DashStyle BorderStyle
		{
			get 
			{
				return this.ExShapeView.BorderStyle;
			}
			set 
			{				
				this.ExShapeView.BorderStyle=value;		
				if(DesignMode)this._MainView.UpdateView();			
			}
		}

		[Description("Shape style mode"),Category("Appearance")] 
		public ShapeStyleMode shape
		{
			get
			{
				return this.ExShapeView.shape;
			}
			set
			{
				this.ExShapeView.shape=value;
				if(DesignMode)this._MainView.UpdateView();
			}
		}

		[Description("Line style"),Category("LineDirection")] 
		public LineStyle LineDirection
		{
			get
			{
				return this.ExShapeView.LineDirection;
			}
			set 
			{
				this.ExShapeView.LineDirection=value;
				if(DesignMode)
					this._MainView.UpdateView();
			}
		}

		public bool AutoFit
		{
			get{return this.ExShapeView.AutoFit;}
			set
			{
				this.ExShapeView.AutoFit=value;	
			}
		}
	
		public FillStyle FillStyle
		{
			get{return this.ExShapeView.FillStyle;}
			set{
				this.ExShapeView.FillStyle=value;
				if(DesignMode)
					this._MainView.UpdateView();
			
			    }
		}
		[Browsable(false)]
		public HatchStyle HatchStyle
		{
			get{return this.ExShapeView.HatchStyle;}
			set{
					this.ExShapeView.HatchStyle=value; 
					if(DesignMode)this._MainView.UpdateView();							
			    }
		}
		[Browsable(false)]
		public Image FillImage
		{
			get
			{
				return this.ExShapeView.FillImage;
			}
			set 
			{
				this.ExShapeView.FillImage=value;
				if(DesignMode)this._MainView.UpdateView();			
			}

		} 



		[Description("Line Arrow Style")] 
		public LineArrowStyle LineArrow
		{
			get
			{
				return this.ExShapeView.LineArrow;
			}
			set 
			{
				this.ExShapeView.LineArrow=value;
				if(DesignMode)
					this._MainView.UpdateView();
			}
		}
          
		public Color FillColor
		{
			get
			{ 
				return this.ExShapeView.FillColor;
			}
			set 
			{
				this.ExShapeView.FillColor=value;
				if(DesignMode)
					this._MainView.UpdateView();

			}
		} 
		protected override void OnResize(EventArgs e)
		{
			base.OnResize (e);
			if(DesignMode)
              this._MainView.UpdateView();
			
		}

	}
	#endregion
}
