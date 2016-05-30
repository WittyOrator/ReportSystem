using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Webb.Reports.ExControls.Data;
using Webb.Reports.ExControls.Views;

namespace Webb.Reports.ExControls.UI
{
	/// <summary>
	/// Summary description for CustomDataGrid.
	/// </summary>
	public class CustomDataGrid : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.Panel PalPreview;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button btnText;
		private System.Windows.Forms.Button BtnHeaders;
		private ScrollPanel palGridColum;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        GridInfo gridInfo=null;


		public CustomDataGrid()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			this.SetStyle(ControlStyles.DoubleBuffer|ControlStyles.AllPaintingInWmPaint|ControlStyles.ResizeRedraw,true);
		

			// TODO: Add any initialization after the InitializeComponent call

		}
		public void SetGridInfo(GridInfo _GridInfo)
		{
			gridInfo=_GridInfo;

			this.palGridColum.Controls.Clear();

			if(gridInfo==null)
			{
				return;				
			}

			int height=BtnHeaders.Height;		

			if(this.gridInfo.SortingFrequence==SortingFrequence.ShowBeforeColumns)
			{
				CustomGridColumn sortGridColumn=new CustomGridColumn(height);
               
				sortGridColumn.Dock=DockStyle.Left;

				this.palGridColum.Controls.Add(sortGridColumn);	                
        
			}	
			foreach(GridColumn column in gridInfo.Columns)
			{
				CustomGridColumn gridColumnControl=new CustomGridColumn(height,column);
               
				gridColumnControl.Dock=DockStyle.Left;

				gridColumnControl.Resize+=new EventHandler(gridColumnControl_Resize);

				this.palGridColum.Controls.Add(gridColumnControl);						
			}
			if(this.gridInfo.SortingFrequence==SortingFrequence.ShowAfterColumns)
			{
				CustomGridColumn sortGridColumn=new CustomGridColumn(height);
               
				sortGridColumn.Dock=DockStyle.Left;

				this.palGridColum.Controls.Add(sortGridColumn);  
			}

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
			this.PalPreview = new System.Windows.Forms.Panel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.btnText = new System.Windows.Forms.Button();
			this.BtnHeaders = new System.Windows.Forms.Button();
			this.palGridColum = new Webb.Reports.ExControls.UI.ScrollPanel();
			this.PalPreview.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// PalPreview
			// 
			this.PalPreview.Controls.Add(this.panel1);
			this.PalPreview.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PalPreview.Location = new System.Drawing.Point(0, 0);
			this.PalPreview.Name = "PalPreview";
			this.PalPreview.Size = new System.Drawing.Size(432, 80);
			this.PalPreview.TabIndex = 0;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.panel1.Controls.Add(this.btnText);
			this.panel1.Controls.Add(this.BtnHeaders);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(24, 80);
			this.panel1.TabIndex = 0;
			// 
			// btnText
			// 
			this.btnText.BackColor = System.Drawing.SystemColors.Control;
			this.btnText.Dock = System.Windows.Forms.DockStyle.Top;
			this.btnText.Location = new System.Drawing.Point(0, 32);
			this.btnText.Name = "btnText";
			this.btnText.Size = new System.Drawing.Size(24, 20);
			this.btnText.TabIndex = 2;
			// 
			// BtnHeaders
			// 
			this.BtnHeaders.BackColor = System.Drawing.SystemColors.Control;
			this.BtnHeaders.Dock = System.Windows.Forms.DockStyle.Top;
			this.BtnHeaders.Location = new System.Drawing.Point(0, 0);
			this.BtnHeaders.Name = "BtnHeaders";
			this.BtnHeaders.Size = new System.Drawing.Size(24, 32);
			this.BtnHeaders.TabIndex = 1;
			// 
			// palGridColum
			// 
			this.palGridColum.AutoScroll = true;
			this.palGridColum.ForeColor = System.Drawing.SystemColors.AppWorkspace;
			this.palGridColum.Location = new System.Drawing.Point(0, 0);
			this.palGridColum.Name = "palGridColum";
			this.palGridColum.TabIndex = 0;
			this.palGridColum.OnScroll += new Webb.Reports.ExControls.UI.ScrollPanel.ScrollDelegate(this.panel_OnScroll);
			// 
			// CustomDataGrid
			// 
			this.Controls.Add(this.PalPreview);
			this.Name = "CustomDataGrid";
			this.Size = new System.Drawing.Size(432, 80);
			this.PalPreview.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private   void   panel_OnScroll(object   sender)   
		{   
			this.palGridColum.Invalidate();
		}

		private void gridColumnControl_Resize(object sender, EventArgs e)
		{
          this.palGridColum.Invalidate();
		}
	}
}
