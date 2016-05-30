using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace Webb.Reports.DataProvider.UI
{
	/// <summary>
	/// Summary description for DataSourcePreviewForm.
	/// </summary>
	public class DataSourcePreviewForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.DataGrid C_PreviewGrid;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DataSourcePreviewForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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

		public void SetPreviewData(object i_DataSource)
		{
			if(i_DataSource is DataTable)
			{
				DataTable m_dt = i_DataSource as DataTable;
				this.C_PreviewGrid.DataSource = m_dt.DefaultView;
			}
			else if(i_DataSource is DataSet)
			{
				DataSet m_ds = i_DataSource as DataSet;
				this.C_PreviewGrid.DataSource = m_ds.Tables[0].DefaultView;
			}
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.C_PreviewGrid = new System.Windows.Forms.DataGrid();
			((System.ComponentModel.ISupportInitialize)(this.C_PreviewGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// C_PreviewGrid
			// 
			this.C_PreviewGrid.AlternatingBackColor = System.Drawing.Color.GhostWhite;
			this.C_PreviewGrid.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(224)), ((System.Byte)(224)), ((System.Byte)(224)));
			this.C_PreviewGrid.BackgroundColor = System.Drawing.Color.LightYellow;
			this.C_PreviewGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.C_PreviewGrid.CaptionBackColor = System.Drawing.Color.RoyalBlue;
			this.C_PreviewGrid.CaptionFont = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.C_PreviewGrid.CaptionForeColor = System.Drawing.Color.White;
			this.C_PreviewGrid.CaptionVisible = false;
			this.C_PreviewGrid.DataMember = "";
			this.C_PreviewGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.C_PreviewGrid.FlatMode = true;
			this.C_PreviewGrid.Font = new System.Drawing.Font("Verdana", 9F);
			this.C_PreviewGrid.ForeColor = System.Drawing.Color.MidnightBlue;
			this.C_PreviewGrid.GridLineColor = System.Drawing.Color.RoyalBlue;
			this.C_PreviewGrid.HeaderBackColor = System.Drawing.Color.MidnightBlue;
			this.C_PreviewGrid.HeaderFont = new System.Drawing.Font("Verdana", 9F);
			this.C_PreviewGrid.HeaderForeColor = System.Drawing.Color.LightSteelBlue;
			this.C_PreviewGrid.LinkColor = System.Drawing.Color.Teal;
			this.C_PreviewGrid.Location = new System.Drawing.Point(0, 0);
			this.C_PreviewGrid.Name = "C_PreviewGrid";
			this.C_PreviewGrid.ParentRowsBackColor = System.Drawing.Color.LightSteelBlue;
			this.C_PreviewGrid.ParentRowsForeColor = System.Drawing.Color.MidnightBlue;
			this.C_PreviewGrid.ReadOnly = true;
			this.C_PreviewGrid.RowHeadersVisible = false;
			this.C_PreviewGrid.SelectionBackColor = System.Drawing.Color.Teal;
			this.C_PreviewGrid.SelectionForeColor = System.Drawing.Color.PaleGreen;
			this.C_PreviewGrid.Size = new System.Drawing.Size(640, 389);
			this.C_PreviewGrid.TabIndex = 0;
			// 
			// DataSourcePreviewForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(640, 389);
			this.Controls.Add(this.C_PreviewGrid);
			this.Name = "DataSourcePreviewForm";
			this.Text = "DataSourcePreviewForm";
			((System.ComponentModel.ISupportInitialize)(this.C_PreviewGrid)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
	}
}
