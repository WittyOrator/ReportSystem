using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Webb.Reports.ReportWizard.WizardInfo
{
	/// <summary>
	/// Summary description for RectARP.
	/// </summary>
	public class RectARP : System.Windows.Forms.UserControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private int _ARPWidth=10;

		private int _BorderWidth=7;


        [Browsable(true)]
		public int BorderWidth
		{
			get
			{
				return this._BorderWidth;
			}
			set
			{
				this._BorderWidth=value;
			}
		}
		[Browsable(true)]
		public int ARPWidth
		{
			get
			{
				return this._ARPWidth;
			}
			set
			{
				this._ARPWidth=value;
			}
		}

		public RectARP()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
  
		}
	

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// RectARP
			// 
			this.Name = "RectARP";
			this.Size = new System.Drawing.Size(304, 192);
			this.Resize += new System.EventHandler(this.RectARP_Resize);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.RectARP_Paint);

		}
		#endregion

		private void RectARP_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			e.Graphics.SmoothingMode=SmoothingMode.HighQuality;

			GraphicsPath graphicPath=new GraphicsPath();

		     graphicPath.AddArc(0,0,_ARPWidth*2,_ARPWidth*2,180f,90f);

            graphicPath.AddLine(_ARPWidth,0,this.Width-_ARPWidth,0);

			graphicPath.AddArc(this.Width-_ARPWidth*2,0,_ARPWidth*2,_ARPWidth*2,270f,90f);

			graphicPath.AddLine(this.Width,_ARPWidth,this.Width,this.Height-_ARPWidth);

			graphicPath.AddArc(this.Width-_ARPWidth*2,this.Height-_ARPWidth*2,_ARPWidth*2,_ARPWidth*2,0f,90f);

			graphicPath.AddLine(this.Width-_ARPWidth,this.Height,_ARPWidth,this.Height);

			graphicPath.AddArc(0,this.Height-_ARPWidth*2,_ARPWidth*2,_ARPWidth*2,90f,90f);

			graphicPath.AddLine(0,this.Height-_ARPWidth,0,_ARPWidth);

			if(_BorderWidth>0)
			{
				System.Drawing.Pen drawPen=new System.Drawing.Pen(Color.Black,_BorderWidth);

				drawPen.Alignment=PenAlignment.Inset;

				e.Graphics.DrawPath(drawPen,graphicPath);

				drawPen.Dispose();
			}

			this.Region=new Region(graphicPath);

			
			

		}

		private void RectARP_Resize(object sender, System.EventArgs e)
		{
			this.Invalidate();
		}
	}
}
