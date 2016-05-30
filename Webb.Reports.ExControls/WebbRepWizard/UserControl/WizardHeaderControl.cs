using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Webb.Reports.ExControls.Views;


namespace Webb.Reports.ReportWizard.WizardInfo
{
	/// <summary>
	/// Summary description for WizardHeaderControl.
	/// </summary>
	public class WizardHeaderControl : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.PictureBox picHeader;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		protected System.Drawing.Printing.Margins pageMargins=new System.Drawing.Printing.Margins();

		protected GridView gridView;

		Size pageSize;

		public WizardHeaderControl()
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.picHeader = new System.Windows.Forms.PictureBox();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.AutoScroll = true;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.picHeader);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(504, 112);
			this.panel1.TabIndex = 0;
			// 
			// picHeader
			// 
			this.picHeader.BackColor = System.Drawing.Color.White;
			this.picHeader.Location = new System.Drawing.Point(8, 8);
			this.picHeader.Name = "picHeader";
			this.picHeader.Size = new System.Drawing.Size(600, 72);
			this.picHeader.TabIndex = 0;
			this.picHeader.TabStop = false;
			this.picHeader.Paint += new System.Windows.Forms.PaintEventHandler(this.picHeader_Paint);
			// 
			// WizardHeaderControl
			// 
			this.AutoScroll = true;
			this.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.Controls.Add(this.panel1);
			this.Name = "WizardHeaderControl";
			this.Size = new System.Drawing.Size(504, 112);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		public void SetPrintSetting(WebbReport webbReport,GridView _GridView)
		{
			Size size=webbReport.GetNoDealedSizePerPage();

			pageSize=size;          
		     
            size.Width+=100;
			
			this.picHeader.Size=size;
	        
			this.pageMargins=webbReport.Margins;

			this.picHeader.Invalidate();

			gridView=_GridView;
         
		}
		public void InvalidatePreview(GridView _GridView)
		{
			this.gridView=_GridView;
			this.picHeader.Invalidate();
		}
		private void picHeader_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			if(gridView!=null)
			{
                gridView.PrintHeadersOnly(e.Graphics,this.picHeader.ClientRectangle,this.pageMargins);
				
			    DrawRedLine(e.Graphics);

			}
		        
		}
		private void DrawRedLine(Graphics g)
		{
			int limitedWidth=(int)((pageSize.Width+pageMargins.Left)/Webb.Utility.ConvertCoordinate)+2;
						
			Pen pen=new Pen(Color.Red,1);

			pen.DashStyle=System.Drawing.Drawing2D.DashStyle.DashDot;

			g.DrawLine(pen,limitedWidth,0,limitedWidth,pageSize.Height);	
			
		}


		
	}
}
