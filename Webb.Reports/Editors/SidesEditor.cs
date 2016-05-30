using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using DevExpress.XtraPrinting;
using System.Drawing.Drawing2D;

namespace Webb.Reports.ExControls.Editors
{   
	//07-15-2008@Simon add
	public class SidesEditorControl : System.Windows.Forms.UserControl
	{
		private IWindowsFormsEditorService edSvc;	
		public DevExpress.XtraPrinting.BorderSide _sides=DevExpress.XtraPrinting.BorderSide.None;			
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button btnall;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.PictureBox picpreview;
		private System.Windows.Forms.Button btnnone;
		private System.Windows.Forms.Button btntop;
		private System.Windows.Forms.Button btnleft;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Button btnright;
		private System.Windows.Forms.Button btnbottom;
		private System.Windows.Forms.Panel panel4;
		private System.ComponentModel.IContainer components;
     
		public SidesEditorControl(IWindowsFormsEditorService iedSvc,DevExpress.XtraPrinting.BorderSide ivalue)
		{
				this._sides=ivalue;		
			InitializeComponent();
			this.edSvc=iedSvc;			
			
			this.picpreview.Invalidate();
		}

	
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
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SidesEditorControl));
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.panel1 = new System.Windows.Forms.Panel();
			this.btnnone = new System.Windows.Forms.Button();
			this.btnall = new System.Windows.Forms.Button();
			this.panel2 = new System.Windows.Forms.Panel();
			this.picpreview = new System.Windows.Forms.PictureBox();
			this.panel4 = new System.Windows.Forms.Panel();
			this.btnbottom = new System.Windows.Forms.Button();
			this.btntop = new System.Windows.Forms.Button();
			this.btnleft = new System.Windows.Forms.Button();
			this.panel3 = new System.Windows.Forms.Panel();
			this.btnright = new System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel4.SuspendLayout();
			this.panel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// imageList1
			// 
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.btnnone);
			this.panel1.Controls.Add(this.btnall);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(264, 56);
			this.panel1.TabIndex = 7;
			// 
			// btnnone
			// 
			this.btnnone.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnnone.ForeColor = System.Drawing.Color.Black;
			this.btnnone.ImageList = this.imageList1;
			this.btnnone.Location = new System.Drawing.Point(0, 24);
			this.btnnone.Name = "btnnone";
			this.btnnone.Size = new System.Drawing.Size(264, 32);
			this.btnnone.TabIndex = 15;
			this.btnnone.Text = "None";
			this.btnnone.Click += new System.EventHandler(this.btnnone_Click);
			// 
			// btnall
			// 
			this.btnall.Dock = System.Windows.Forms.DockStyle.Top;
			this.btnall.ForeColor = System.Drawing.Color.Black;
			this.btnall.Location = new System.Drawing.Point(0, 0);
			this.btnall.Name = "btnall";
			this.btnall.Size = new System.Drawing.Size(264, 24);
			this.btnall.TabIndex = 2;
			this.btnall.Text = "All";
			this.btnall.Click += new System.EventHandler(this.btnall_Click);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.picpreview);
			this.panel2.Controls.Add(this.panel4);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new System.Drawing.Point(0, 56);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(264, 128);
			this.panel2.TabIndex = 8;
			// 
			// picpreview
			// 
			this.picpreview.Dock = System.Windows.Forms.DockStyle.Fill;
			this.picpreview.Location = new System.Drawing.Point(40, 0);
			this.picpreview.Name = "picpreview";
			this.picpreview.Size = new System.Drawing.Size(224, 128);
			this.picpreview.TabIndex = 12;
			this.picpreview.TabStop = false;
			this.picpreview.Paint += new System.Windows.Forms.PaintEventHandler(this.picpreview_Paint);
			this.picpreview.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picpreview_MouseUp);
			// 
			// panel4
			// 
			this.panel4.Controls.Add(this.btnbottom);
			this.panel4.Controls.Add(this.btntop);
			this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel4.Location = new System.Drawing.Point(0, 0);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(40, 128);
			this.panel4.TabIndex = 19;
			this.panel4.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel4_MouseUp);
			// 
			// btnbottom
			// 
			this.btnbottom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnbottom.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnbottom.Image = ((System.Drawing.Image)(resources.GetObject("btnbottom.Image")));
			this.btnbottom.Location = new System.Drawing.Point(4, 96);
			this.btnbottom.Name = "btnbottom";
			this.btnbottom.Size = new System.Drawing.Size(32, 24);
			this.btnbottom.TabIndex = 18;
			this.btnbottom.Click += new System.EventHandler(this.btnbottom_Click);
			// 
			// btntop
			// 
			this.btntop.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btntop.Image = ((System.Drawing.Image)(resources.GetObject("btntop.Image")));
			this.btntop.Location = new System.Drawing.Point(4, 8);
			this.btntop.Name = "btntop";
			this.btntop.Size = new System.Drawing.Size(32, 24);
			this.btntop.TabIndex = 15;
			this.btntop.Click += new System.EventHandler(this.btntop_Click);
			// 
			// btnleft
			// 
			this.btnleft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnleft.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnleft.Image = ((System.Drawing.Image)(resources.GetObject("btnleft.Image")));
			this.btnleft.Location = new System.Drawing.Point(64, 5);
			this.btnleft.Name = "btnleft";
			this.btnleft.Size = new System.Drawing.Size(32, 24);
			this.btnleft.TabIndex = 14;
			this.btnleft.Click += new System.EventHandler(this.btnleft_Click);
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.btnright);
			this.panel3.Controls.Add(this.btnleft);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel3.Location = new System.Drawing.Point(0, 184);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(264, 32);
			this.panel3.TabIndex = 9;
			this.panel3.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel3_MouseUp);		
			// 
			// btnright
			// 
			this.btnright.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnright.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnright.Image = ((System.Drawing.Image)(resources.GetObject("btnright.Image")));
			this.btnright.Location = new System.Drawing.Point(216, 5);
			this.btnright.Name = "btnright";
			this.btnright.Size = new System.Drawing.Size(32, 24);
			this.btnright.TabIndex = 19;
			this.btnright.Click += new System.EventHandler(this.btnright_Click);
			// 
			// SidesEditorControl
			// 
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Name = "SidesEditorControl";
			this.Size = new System.Drawing.Size(264, 216);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SidesEditorControl_KeyDown);
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		
		private void AlterSideStyle(BorderSide bSide)
		{
			if((_sides&bSide)==bSide)
			{		
				this._sides&=(~bSide);				
			}
			else
			{ 
				this._sides|=bSide;		   
			}
		} 

		private void btnleft_Click(object sender, System.EventArgs e)
		{	
			AlterSideStyle(BorderSide.Left);
			
			this.picpreview.Invalidate();
		 	
			
		}

		private void btntop_Click(object sender, System.EventArgs e)
		{
			AlterSideStyle(BorderSide.Top);

			this.picpreview.Invalidate();
		}

		private void btnright_Click(object sender, System.EventArgs e)
		{
			AlterSideStyle(BorderSide.Right);

			this.picpreview.Invalidate();
		}


		private void btnbottom_Click(object sender, System.EventArgs e)
		{
			AlterSideStyle(BorderSide.Bottom);

			this.picpreview.Invalidate();
		}

		private void btnnone_Click(object sender, System.EventArgs e)
		{
			this._sides=BorderSide.None;
			this.picpreview.Invalidate();
		}

		private void btnall_Click(object sender, System.EventArgs e)
		{
			this._sides=BorderSide.All;;
			this.picpreview.Invalidate();
		}
	

		private void SidesEditorControl_Load(object sender, System.EventArgs e)
		{   
		
		}

		private void picpreview_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			e.Graphics.Clear(this.BackColor);
			Point start,end;
			Pen penblack=new Pen(Color.Black,1);	

			string pres="Sides Previewing:\n";
		
			start=new Point(20,20);
			end=new Point(20,this.picpreview.Height-20);
			if((_sides&BorderSide.Left)==BorderSide.Left)
			{
				penblack.DashStyle=DashStyle.Solid;		
				e.Graphics.DrawLine(penblack,start,end);	
				pres=pres +"Left,";			
			}
			else
			{
				penblack.DashStyle=DashStyle.Dot;
				e.Graphics.DrawLine(penblack,start,end);				
			}

			start=new Point(picpreview.Width-20,20);
			end=new Point(picpreview.Width-20,picpreview.Height-20);	
			if((_sides&BorderSide.Right)==BorderSide.Right)
			{				
				penblack.DashStyle=DashStyle.Solid;		
				e.Graphics.DrawLine(penblack,start,end);
				pres=pres +"Right,";
			}
			else
			{
				penblack.DashStyle=DashStyle.Dot;
				e.Graphics.DrawLine(penblack,start,end);	
			}
			
			start=new Point(20,20);
			end=new Point(picpreview.Width-20,20);
			if((this._sides&BorderSide.Top)==BorderSide.Top)
			{
				penblack.DashStyle=DashStyle.Solid;		
				e.Graphics.DrawLine(penblack,start,end);	
				pres=pres +"Top,";
			}
			else
			{
				penblack.DashStyle=DashStyle.Dot;
				e.Graphics.DrawLine(penblack,start,end);	
			}

			start=new Point(20,picpreview.Height-20);
			end=new Point(picpreview.Width-20,picpreview.Height-20);
			if((_sides&BorderSide.Bottom)==BorderSide.Bottom)
			{
				
				penblack.DashStyle=DashStyle.Solid;		
				e.Graphics.DrawLine(penblack,start,end);	
				pres=pres +"Bottom,";
			}
			else
			{
				penblack.DashStyle=DashStyle.Dot;
				e.Graphics.DrawLine(penblack,start,end);	
			}

			
			Font presfont=new Font("Arial",10);
			SizeF strf=e.Graphics.MeasureString(pres,presfont);
			StringFormat strformat=new StringFormat();
			strformat.Alignment=StringAlignment.Center;   
			strformat.LineAlignment=StringAlignment.Center;
            pres=pres.Trim(',');
			if(_sides==BorderSide.All)
			{
				pres="Sides Previewing:All";
			}
            else if(_sides==BorderSide.None)
			{
				pres="Sides Previewing:None";
			}
			e.Graphics.DrawString(pres,presfont,Brushes.Black,this.picpreview.ClientRectangle,strformat); 		
		}

		private void SidesEditorControl_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{	
			               
		}

		private void label1_Click(object sender, System.EventArgs e)
		{
		
		}

		private void SidesEditorControl_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Enter)
			{
			   		if(this.edSvc!=null)this.edSvc.CloseDropDown();
			}

		}

		private void picpreview_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{		
			if(e.X<=20)
			{
				this.AlterSideStyle(BorderSide.Left);				
			}
			else if(e.X>=this.picpreview.Width-20)
			{
				this.AlterSideStyle(BorderSide.Right);	
			}
			else if(e.Y<=20)
			{
				this.AlterSideStyle(BorderSide.Top);	
			}
			else if(e.Y>=this.picpreview.Height-20)
			{
				this.AlterSideStyle(BorderSide.Bottom);
			}
			else
			{
				if(this.edSvc!=null)this.edSvc.CloseDropDown();
			}
			this.picpreview.Invalidate();
		}

		private void panel4_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
		   this.AlterSideStyle(BorderSide.Left);	
            this.picpreview.Invalidate();
		}

		private void panel3_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			this.AlterSideStyle(BorderSide.Bottom);	
			this.picpreview.Invalidate();
		
		}
	
	}

	#region class SidesEditor
	public class  SidesEditor: System.Drawing.Design.UITypeEditor
	{
		IWindowsFormsEditorService edSvc;

		[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name="FullTrust")] 
		public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.DropDown;
		}

		// Displays the UI for value selection.
		[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name="FullTrust")]
		public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, System.IServiceProvider provider, object value)
		{      
			//			if(value is Int32)
			//			{
			//				if ((int)value<=(1+2+4+8)&&(int)value>=0)
			//					 return (DevExpress.XtraPrinting.BorderSide)value;
			//			}
			if(!(value is DevExpress.XtraPrinting.BorderSide))
				return value;
			
			edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
			DevExpress.XtraPrinting.BorderSide ivalue=(DevExpress.XtraPrinting.BorderSide)value;
			SidesEditorControl sidescontrol=new SidesEditorControl(edSvc,ivalue);
			if( edSvc != null )
			{			
				edSvc.DropDownControl(sidescontrol);
				edSvc.CloseDropDown();
				return sidescontrol._sides;				
				
			}
			return value;
		}

	
		[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name="FullTrust")]   
		public override void PaintValue(System.Drawing.Design.PaintValueEventArgs e)
		{			
		}
        
		
		[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name="FullTrust")]
		public override bool GetPaintValueSupported(System.ComponentModel.ITypeDescriptorContext context)
		{
			return false;
		}
	}
	#endregion
}
