using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Webb.Reports.ExControls.Views;
using System.Drawing.Drawing2D;


namespace Webb.Reports.ExControls.UI
{
	/// <summary>
	/// Summary description for CustomGridColumn.
	/// </summary>
	internal class CustomGridColumn : System.Windows.Forms.UserControl
	{
		#region struct & Field For ColumnWidth

		private int oldx;
		private int oldy;

		enum mPosition
		{   none,
			left,
			right,
			
		}
		private mPosition adjust=mPosition.none;

		#endregion


		private System.Windows.Forms.Panel oalFill;
		private System.Windows.Forms.Button BtnHeader;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label lblCol;

		protected GridColumn _GridColumn=null;	

		int MIN_WIDTH=5;

		private bool InColSizing=false;

		

		public CustomGridColumn(int height)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			this.BtnHeader.Height=height;

			this.Width=55;

			this.BtnHeader.Text="";		

		

			// TODO: Add any initialization after the InitializeComponent call

		}

		public CustomGridColumn(int height,GridColumn gridColumn)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			this.BtnHeader.Height=height;

			this.Width=gridColumn.ColumnWidth;

			this.BtnHeader.Text=gridColumn.Field;

			_GridColumn=gridColumn;				

			this.BtnHeader.MouseDown+=new MouseEventHandler(MyMouseDown);
			this.BtnHeader.MouseMove+=new MouseEventHandler(MyMouseMove);
			this.BtnHeader.MouseUp+=new MouseEventHandler(MyMouseUp);

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
			this.oalFill = new System.Windows.Forms.Panel();
			this.lblCol = new System.Windows.Forms.Label();
			this.BtnHeader = new System.Windows.Forms.Button();
			this.oalFill.SuspendLayout();
			this.SuspendLayout();
			// 
			// oalFill
			// 
			this.oalFill.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.oalFill.Controls.Add(this.lblCol);
			this.oalFill.Controls.Add(this.BtnHeader);
			this.oalFill.Dock = System.Windows.Forms.DockStyle.Fill;
			this.oalFill.Location = new System.Drawing.Point(0, 0);
			this.oalFill.Name = "oalFill";
			this.oalFill.Size = new System.Drawing.Size(80, 96);
			this.oalFill.TabIndex = 0;
			// 
			// lblCol
			// 
			this.lblCol.BackColor = System.Drawing.Color.White;
			this.lblCol.Dock = System.Windows.Forms.DockStyle.Top;
			this.lblCol.Location = new System.Drawing.Point(0, 40);
			this.lblCol.Name = "lblCol";
			this.lblCol.Size = new System.Drawing.Size(80, 20);
			this.lblCol.TabIndex = 1;
			this.lblCol.Paint += new System.Windows.Forms.PaintEventHandler(this.lblCol_Paint);
			// 
			// BtnHeader
			// 
			this.BtnHeader.BackColor = System.Drawing.SystemColors.Control;
			this.BtnHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.BtnHeader.Location = new System.Drawing.Point(0, 0);
			this.BtnHeader.Name = "BtnHeader";
			this.BtnHeader.Size = new System.Drawing.Size(80, 40);
			this.BtnHeader.TabIndex = 0;
			this.BtnHeader.Paint += new System.Windows.Forms.PaintEventHandler(this.BtnHeader_Paint);
			// 
			// CustomGridColumn
			// 
			this.Controls.Add(this.oalFill);
			this.Name = "CustomGridColumn";
			this.Size = new System.Drawing.Size(80, 96);
			this.oalFill.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region mouseEvent	
		private void MyMouseDown(object sender, MouseEventArgs e)
		{
			if(e.Button!=MouseButtons.Left)return;
			
			Rectangle rectRight = new Rectangle(this.Width - 3, 0, 3, this.BtnHeader.Height);
			
			if (rectRight.Contains(e.X, e.Y))
			{
				adjust = mPosition.right;
				this.Cursor = Cursors.SizeWE;	
				this.InColSizing=true;
			}
			else
			{
				this.Cursor = Cursors.Default;

			}
		
			
			oldx = e.X;
			oldy = e.Y;	

			

		}

		private void MyMouseMove(object sender, MouseEventArgs e)
		{
			Rectangle rectLeft = new Rectangle(0, 0,3, this.BtnHeader.Height);
			Rectangle rectRight = new Rectangle(this.Width - 3, 0, 3, this.BtnHeader.Height);
			
			if (rectRight.Contains(e.X, e.Y))
			{
				adjust = mPosition.right;
				this.Cursor = Cursors.SizeWE;
			}				
			else
			{
				this.Cursor = Cursors.Default;
			}
			
			
		}


		private void MyMouseUp(object sender, MouseEventArgs e)
		{			
			int dx = e.X - oldx;

			if(!this.InColSizing||e.Button!=MouseButtons.Left)return;

			this.InColSizing=false;

			if(this.Width + dx<MIN_WIDTH)return;

			switch (adjust)
			{
				case mPosition.left:
					
					break;
				case mPosition.right:
					
					this.Width += dx;
					break;

			}
			
		}

	
		#endregion

		private void lblCol_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			Rectangle rect=e.ClipRectangle;
			e.Graphics.Clear(Color.White);

			e.Graphics.DrawLine(Pens.LightGray,rect.Right-1,rect.Top,rect.Right-1,rect.Bottom);
			e.Graphics.DrawLine(Pens.LightGray,rect.Left,rect.Bottom-1,rect.Right-1,rect.Bottom-1);
		
		}

		private void BtnHeader_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
	   {
			#region DrawText
			Rectangle rect=e.ClipRectangle;
			e.Graphics.Clear(this.BtnHeader.BackColor);

			Brush foreBrush=new SolidBrush(this.BtnHeader.ForeColor);

			string PaintText=this.BtnHeader.Text;

			e.Graphics.DrawString(PaintText,this.BtnHeader.Font,foreBrush,5,5);	

			e.Graphics.DrawLine(Pens.Gray,rect.Right-1,rect.Top,rect.Right-1,rect.Bottom);

			e.Graphics.DrawLine(Pens.Gray,rect.Left,rect.Bottom-1,rect.Right-1,rect.Bottom-1);
		
			#endregion

//			#region New Code
//		
//			Color c5 = Color.FromArgb
//				(255,255,255);
//			Color c2 = Color.FromArgb
//				(192,192,192);
//			
//			Brush b = new LinearGradientBrush
//				(ClientRectangle, c5, c2, LinearGradientMode.Vertical);
//			
//			int offsetwidth=this.BtnHeader.Width/50;
//			Point[] points=new Point[8]; 
//			points[0].X=offsetwidth;
//			points[0].Y=0;
//
//			points[1].X=this.BtnHeader.Width-offsetwidth;
//			points[1].Y=0;
//
//			points[2].X=this.BtnHeader.Width;
//			points[2].Y=offsetwidth;
//
//			points[3].X=this.BtnHeader.Width;
//			points[3].Y=this.BtnHeader.Height-offsetwidth;
//
//			points[4].X=this.BtnHeader.Width-offsetwidth;
//			points[4].Y=this.BtnHeader.Height;
//
//			points[5].X=offsetwidth;
//			points[5].Y=this.BtnHeader.Height;
//
//			points[6].X=0;
//			points[6].Y=this.BtnHeader.Height-offsetwidth;
//
//			points[7].X=0;
//			points[7].Y=offsetwidth;
//		
//			e.Graphics.FillPolygon(b,points,FillMode.Winding);
//			
//			e.Graphics.DrawPolygon(new Pen(Color.Gray,1),points);
//		
//			StringFormat drawFormat = new StringFormat();
//			drawFormat.FormatFlags = StringFormatFlags.DisplayFormatControl;
//			drawFormat.LineAlignment=StringAlignment.Center;
//			drawFormat.Alignment=System.Drawing.StringAlignment.Center; 
//			e.Graphics.DrawString(this.BtnHeader.Text,this.BtnHeader.Font,new LinearGradientBrush(this.BtnHeader.ClientRectangle,Color.Black,Color.Black,LinearGradientMode.Vertical),this.BtnHeader.ClientRectangle,drawFormat); 
//			b.Dispose();
//
//
//			#endregion

		}

	
	}	
	public   class   ScrollPanel:   System.Windows.Forms.Panel   
	{  
		
		public   delegate   void   ScrollDelegate(object   sender);   
		public   event   ScrollDelegate   OnScroll;   
		public   ScrollPanel():base()   
		{  
			this.AutoScroll=true;			
		}   
		protected   override   void   WndProc(ref   Message   m)   
		{   
			switch(m.Msg)
			{
				case   0x0115:     //Vertival scroll
				case   0x0114:  
					if   (OnScroll   !=   null)   
					{   
						this.OnScroll(this);   
					}   
					break;
				default:
					
					break;
			}
			base.WndProc(ref   m); 
			  
		}   
	}   

}
