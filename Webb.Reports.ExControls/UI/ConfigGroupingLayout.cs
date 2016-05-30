using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Reflection;

using Webb.Collections;

namespace Webb.Reports.ExControls.UI
{
	public class ConfigGroupingLayout : Webb.Reports.ExControls.UI.ExControlDesignerControlBase
	{
		private Int32Collection ColumnsWidth;
		private Int32Collection RowsHeight;
		private System.Windows.Forms.DataGrid dataGrid1;
		private System.ComponentModel.IContainer components = null;
		private int CurColLeft = 0;
		private System.Windows.Forms.ToolTip C_ToolTip;
		private bool InColResizing = false;

		public ConfigGroupingLayout()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();
			this.ColumnsWidth = new Int32Collection();
			this.RowsHeight = new Int32Collection();
			// TODO: Add any initialization after the InitializeComponent call
			this.Load += new EventHandler(ConfigGroupingLayout_Load);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.dataGrid1 = new System.Windows.Forms.DataGrid();
			this.C_ToolTip = new System.Windows.Forms.ToolTip(this.components);
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGrid1
			// 
			this.dataGrid1.AllowNavigation = false;
			this.dataGrid1.AllowSorting = false;
			this.dataGrid1.CaptionVisible = false;
			this.dataGrid1.DataMember = "";
			this.dataGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGrid1.Location = new System.Drawing.Point(0, 0);
			this.dataGrid1.Name = "dataGrid1";
			this.dataGrid1.ParentRowsVisible = false;
			this.dataGrid1.ReadOnly = true;
			this.dataGrid1.Size = new System.Drawing.Size(584, 336);
			this.dataGrid1.TabIndex = 0;
			this.dataGrid1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGrid1_MouseDown);
			this.dataGrid1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dataGrid1_MouseUp);
			this.dataGrid1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dataGrid1_MouseMove);
			// 
			// C_ToolTip
			// 
			this.C_ToolTip.ShowAlways = true;
			// 
			// ConfigGroupingLayout
			// 
			this.Controls.Add(this.dataGrid1);
			this.Name = "ConfigGroupingLayout";
			this.Size = new System.Drawing.Size(584, 336);
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

        #region UpdateView

        private void UpdateColumnRow(Views.ExControlView i_View)
		{
			if(i_View is Views.GroupView)
			{
				this.ColumnsWidth.CopyTo((i_View as Views.GroupView).ColumnsWidth);
				this.RowsHeight.CopyTo((i_View as Views.GroupView).RowsHight);
			}
            if (i_View is Views.GradingView)
            {
                this.ColumnsWidth.CopyTo((i_View as Views.GradingView).ColumnsWidth);
                this.RowsHeight.CopyTo((i_View as Views.GradingView).RowsHight);
            }
			if(i_View is Views.SimpleGroupView)
			{
				this.ColumnsWidth.CopyTo((i_View as Views.SimpleGroupView).ColumnsWidth);
				this.RowsHeight.CopyTo((i_View as Views.SimpleGroupView).RowsHight);
			}
			if(i_View is Views.GridView)
			{
				this.ColumnsWidth.CopyTo((i_View as Views.GridView).ColumnsWidth);
				this.RowsHeight.CopyTo((i_View as Views.GridView).RowsHight);
			}
            if (i_View is Views.HorizontalGridView)
            {
                this.ColumnsWidth.CopyTo((i_View as Views.HorizontalGridView).ColumnsWidth);
                this.RowsHeight.CopyTo((i_View as Views.HorizontalGridView).RowsHight);
            }
			if(i_View is Views.CompactGroupView)
			{
				this.ColumnsWidth.CopyTo((i_View as Views.CompactGroupView).ColumnsWidth);
				this.RowsHeight.CopyTo((i_View as Views.CompactGroupView).RowsHight);
			}
			if(i_View is Views.MatrixGroupView)
			{
				this.ColumnsWidth.CopyTo((i_View as Views.MatrixGroupView).ColumnsWidth);
				this.RowsHeight.CopyTo((i_View as Views.MatrixGroupView).RowsHight);
			}
			if(i_View is Views.StatControlView)
			{
				this.ColumnsWidth.CopyTo((i_View as Views.StatControlView).ColumnsWidth);				
			}
            if (i_View is Views.HorizonGroupView)
            {
                this.ColumnsWidth.CopyTo((i_View as Views.HorizonGroupView).ColumnsWidth);

                this.RowsHeight.CopyTo((i_View as Views.HorizonGroupView).RowsHight);
            }
            if (i_View is Views.MaskedTextControlView)
            {
                if ((i_View as Views.MaskedTextControlView).ColumnsWidth == null) (i_View as Views.MaskedTextControlView).ColumnsWidth = new Int32Collection();
                
                this.ColumnsWidth.CopyTo((i_View as Views.MaskedTextControlView).ColumnsWidth);


                if ((i_View as Views.MaskedTextControlView).RowsHight == null) (i_View as Views.MaskedTextControlView).RowsHight = new Int32Collection();
 
                this.RowsHeight.CopyTo((i_View as Views.MaskedTextControlView).RowsHight);
            }
		}



		public override void UpdateView(Webb.Reports.ExControls.Views.ExControlView i_View)
		{
			if(i_View == null) return;
			
			this.UpdateColumn(i_View);

			this.UpdateRow(i_View);
			
			this.UpdateColumnRow(i_View);
		}

		private void UpdateColumn(Webb.Reports.ExControls.Views.ExControlView i_View)
		{
			//col width
			WebbTable m_PrintTable = i_View.PrintingTable;

			if(m_PrintTable == null) return;
			
			if(dataGrid1.TableStyles.Count<=0) return;

			DataGridTableStyle m_CurrentStyle = dataGrid1.TableStyles[0];
			
			int m_cols = m_PrintTable.GetColumns();
			
			this.ColumnsWidth.Clear();
			
			int count = Math.Min(m_cols,m_CurrentStyle.GridColumnStyles.Count);

			for(int m_col = 0; m_col < count; m_col++)
			{
				DataGridColumnStyle m_ColStyle = m_CurrentStyle.GridColumnStyles[m_col];				
				
				this.ColumnsWidth.Add(m_ColStyle.Width);	
			}
		}

		private void UpdateRow(Webb.Reports.ExControls.Views.ExControlView i_View)
		{
			//row height
			Type m_GridType = this.dataGrid1.GetType();

			Type m_GridRowType = null;
			
			PropertyInfo m_PropertyInfo = m_GridType.GetProperty("DataGridRows",BindingFlags.NonPublic|BindingFlags.Instance);
			
			Array m_GridRows = m_PropertyInfo.GetValue(this.dataGrid1,null) as Array;
			
			if(m_GridRows == null || m_GridRows.Length <= 0) return;
			
			this.RowsHeight.Clear();
			
			m_PropertyInfo = null;
			
			foreach(object m_GridRow in m_GridRows)
			{
				if(m_GridRowType==null) m_GridRowType = m_GridRow.GetType();
				
				if(m_PropertyInfo==null) m_PropertyInfo = m_GridRowType.GetProperty("Height",BindingFlags.Public|BindingFlags.Instance);
				
				int m_Height = (int)m_PropertyInfo.GetValue(m_GridRow,null);
				
				this.RowsHeight.Add(m_Height);
			}
        }
        #endregion

        #region Set View
        public override void SetView(Webb.Reports.ExControls.Views.ExControlView i_View)
		{
			if(i_View==null) return;
			
			if(i_View.PrintingTable==null) return;

			DataTable m_Table = null;

			m_Table = i_View.PrintingTable.CreateDataTableWithoutHeader();
			
			this.ClearTable(m_Table);

			this.dataGrid1.DataSource = m_Table;

			if(i_View is Views.StatControlView)
			{
				CreateStatControlView(m_Table,i_View);
			}
			else
			{			
				this.CreateTableStyle(m_Table,i_View);
			}
		}


		private void ClearTable(DataTable dt)
		{
			for(int i = 0; i<dt.Rows.Count; i++)
			{
				for(int j = 0; j<dt.Columns.Count; j++)
				{
					object value = dt.Rows[i][j];
					if(value is System.DBNull)
					{
						dt.Rows[i][j] = string.Empty;
					}
				}
			}
		}

		private void CreateTableColumnStyle(DataTable m_Table,Views.ExControlView i_View)
		{
			//Columns width
			WebbTable i_PrintTable  = i_View.PrintingTable;

			if(i_PrintTable == null) return;

			DataGridColumnStyle m_ColStyle = null;
			
			this.dataGrid1.TableStyles.Clear();
			
			DataGridTableStyle m_TableStyle = new DataGridTableStyle();
			
			m_TableStyle.AllowSorting = false;

			m_TableStyle.MappingName = m_Table.TableName;
			
			int m_cols = i_PrintTable.GetColumns();
			
			int m_rows = i_PrintTable.GetRows();
			
			//System.Diagnostics.Debug.Assert(m_Table.Rows.Count == m_rows && m_Table.Columns.Count == m_cols);
			
			for(int m_col = 0; m_col < m_cols; m_col++)
			{
				m_ColStyle = new DataGridTextBoxColumn();

				WebbTableCell cell = i_PrintTable.GetCell(0,m_col) as WebbTableCell;

				if(cell != null)	//Modified at 2008-11-11 11:09:29@Scott
				{
					m_ColStyle.Width = i_PrintTable.GetCell(0,m_col).CellStyle.Width;

					m_ColStyle.MappingName = m_Table.Columns[m_col].ColumnName;

					m_ColStyle.HeaderText = string.Empty;
				
					m_TableStyle.GridColumnStyles.Add(m_ColStyle);
				}
			}

			this.dataGrid1.TableStyles.Add(m_TableStyle);
		}
		private void SetColumnRow(Views.ExControlView i_View)
		{	
			if(i_View is Views.GroupView)
			{              
				(i_View as Views.GroupView).ColumnsWidth.CopyTo(this.ColumnsWidth);
				(i_View as Views.GroupView).RowsHight.CopyTo(this.RowsHeight);
			}
            if (i_View is Views.GradingView)
            {
                (i_View as Views.GradingView).ColumnsWidth.CopyTo(this.ColumnsWidth);
                (i_View as Views.GradingView).RowsHight.CopyTo(this.RowsHeight);
            }
			if(i_View is Views.SimpleGroupView)
			{
				(i_View as Views.SimpleGroupView).ColumnsWidth.CopyTo(this.ColumnsWidth);
				(i_View as Views.SimpleGroupView).RowsHight.CopyTo(this.RowsHeight);
			}
			if(i_View is Views.GridView)
			{
				(i_View as Views.GridView).ColumnsWidth.CopyTo(this.ColumnsWidth);
				(i_View as Views.GridView).RowsHight.CopyTo(this.RowsHeight);
			}
            if (i_View is Views.HorizontalGridView)
            {
                (i_View as Views.HorizontalGridView).ColumnsWidth.CopyTo(this.ColumnsWidth);
                (i_View as Views.HorizontalGridView).RowsHight.CopyTo(this.RowsHeight);
            }
			if(i_View is Views.CompactGroupView)
			{
				(i_View as Views.CompactGroupView).ColumnsWidth.CopyTo(this.ColumnsWidth);
				(i_View as Views.CompactGroupView).RowsHight.CopyTo(this.RowsHeight);
			}
			if(i_View is Views.MatrixGroupView)
			{
				(i_View as Views.MatrixGroupView).ColumnsWidth.CopyTo(this.ColumnsWidth);
				(i_View as Views.MatrixGroupView).RowsHight.CopyTo(this.RowsHeight);
			}
            if (i_View is Views.HorizonGroupView)    //simon 2010-05-06
            {
                (i_View as Views.HorizonGroupView).ColumnsWidth.CopyTo(this.ColumnsWidth);
                (i_View as Views.HorizonGroupView).RowsHight.CopyTo(this.RowsHeight);
            }
            if (i_View is Views.MaskedTextControlView)    //simon 2010-05-06
            {
                if ((i_View as Views.MaskedTextControlView).ColumnsWidth != null)
                {
                    (i_View as Views.MaskedTextControlView).ColumnsWidth.CopyTo(this.ColumnsWidth);
                }
                if ((i_View as Views.MaskedTextControlView).RowsHight != null)
                {
                    (i_View as Views.MaskedTextControlView).RowsHight.CopyTo(this.RowsHeight);
                }
            }

            #region Modifed at 2011-5-31 12:27:39@simon
            if (this.RowsHeight.Count == 0 || i_View.PrintingTable != null)
            {
                this.RowsHeight = i_View.PrintingTable.ResolveRowsHeight();
            }
            #endregion

            if (i_View is Views.StatControlView)
			{
				(i_View as Views.StatControlView).ColumnsWidth.CopyTo(this.ColumnsWidth);				
			}
		}


		private void CreateTableRowStyle(DataTable m_Table,Views.ExControlView i_View)
		{
			//Rows height
			Type m_GridType = this.dataGrid1.GetType();
			
			PropertyInfo m_PropertyInfo = m_GridType.GetProperty("DataGridRows",BindingFlags.NonPublic|BindingFlags.Instance);
			
			Array m_GridRows = m_PropertyInfo.GetValue(this.dataGrid1,null) as Array;
			
			if(m_GridRows==null||m_GridRows.Length<=0) return;
			
			Type m_GridRowType = null;
			
			m_PropertyInfo = null;
			
			int count = Math.Min(m_GridRows.Length,this.RowsHeight.Count);
			
			for(int index = 0 ;index < count;index++)
			{
				object m_GridRow = m_GridRows.GetValue(index);

				if(m_GridRowType==null) m_GridRowType = m_GridRow.GetType();
				
				if(m_PropertyInfo==null) m_PropertyInfo = m_GridRowType.GetProperty("Height");
				
				m_PropertyInfo.SetValue(m_GridRow,this.RowsHeight[index],null);
			}
		}

		private void CreateTableStyle(DataTable m_Table,Views.ExControlView i_View)
		{
            this.SetColumnRow(i_View);

			this.CreateTableColumnStyle(m_Table,i_View);
			
			this.CreateTableRowStyle(m_Table,i_View);

			if(i_View is Views.GridView) this.dataGrid1.Font = (i_View as Views.GridView).Styles.RowStyle.Font;	//07-02-2008@Scott
		}

		private void CreateStatControlView(DataTable m_Table,Views.ExControlView i_View)
		{
			i_View.UpdateView();

			this.SetColumnRow(i_View);

			this.CreateTableColumnStyle(m_Table,i_View);

			this.dataGrid1.Font = (i_View as Views.StatControlView).Styles.RowStyle.Font;	//07-02-2008@Scott
		}

		#endregion

		private void dataGrid1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			Point pt = new Point(e.X,e.Y);

			if(e.Button == MouseButtons.Left && this.InColResizing)
			{
				string strWidth = string.Format("Width: {0} pixel",e.X - this.CurColLeft);			

				this.C_ToolTip.SetToolTip(this.dataGrid1,strWidth);
			}
		}

		private void dataGrid1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			Point pt = new Point(e.X,e.Y);

			if(e.Button == MouseButtons.Left)
			{
				DataGrid.HitTestInfo hti = this.dataGrid1.HitTest(pt);

				if(hti.Type == DataGrid.HitTestType.ColumnResize)
				{
					this.InColResizing = true;

					Rectangle rect = this.dataGrid1.GetCellBounds(0,hti.Column);

					this.CurColLeft = rect.Left;
				}
			}
		}

		private void dataGrid1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(e.Button == MouseButtons.Left)
			{
				this.InColResizing = false;
			}
		}

		private void ConfigGroupingLayout_Load(object sender, EventArgs e)
		{
			if(this.C_ToolTip != null) this.C_ToolTip.Dispose();

			this.C_ToolTip = new ToolTip(this.components);
			
			this.C_ToolTip.ShowAlways = true;
		}
	}
}

