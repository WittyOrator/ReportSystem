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
using System.Globalization;
using System.Security.Permissions;

using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Container;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraPrinting;
using DevExpress.Utils.Design;
using DevExpress.Utils;

using Webb.Reports.ExControls;
using Webb.Reports.ExControls.Data;
using Webb.Collections;
using Webb.Reports.DataProvider;

using Webb.Data;

namespace Webb.Reports.ExControls.Views
{
	/// <summary>
	/// Summary description for DateTimeControlView.
	/// </summary>
	[Serializable]
	public class MaskedTextControlView : ExControlView, ISerializable
	{	
     
		protected bool _AutoAdjustTitleSize=false;

		protected DBFilter _Filter=new DBFilter();

		protected int _IndexedRow=0;

		protected SortingColumnCollection _SortingColumns=new SortingColumnCollection();

        protected MaskInfoCollection _MaskInfoSetting = new MaskInfoCollection();
         protected Int32Collection _RowsHight;
        protected Int32Collection _ColumnsWidth;

        [NonSerialized]
        protected bool _MakeChanges = false;

     
        #region property      
        public MaskInfoCollection MaskInfoSetting
        {
            get
            {
                if (_MaskInfoSetting == null) _MaskInfoSetting = new MaskInfoCollection();
                return this._MaskInfoSetting;
            }
            set
            {
                this._MaskInfoSetting = value;
            }
        }

        public Int32Collection ColumnsWidth
        {
            get
            {
                if (this._ColumnsWidth == null) this._ColumnsWidth = new Int32Collection();
               return this._ColumnsWidth;
            }
            set { this._ColumnsWidth = value; }
        }

        public Int32Collection RowsHight
        {
            get
            {
                if (this._RowsHight == null) this._RowsHight = new Int32Collection();
                return this._RowsHight;
            }
            set { this._RowsHight = value; }
        }

        public SortingColumnCollection SortingColumns
        {
            get
            {
                if (_SortingColumns == null) _SortingColumns = new SortingColumnCollection();
                return _SortingColumns;
            }
            set
            {
                _SortingColumns = value;             
            }
        }

        public DBFilter Filter
        {
            get
            {
                if (this._Filter == null) this._Filter = new DBFilter();
                return _Filter;
            }
            set
            {
                _Filter = value;
            }
        }

        public int IndexRow
        {
            get
            {
                return this._IndexedRow;
            }
            set
            {
                this._IndexedRow = value;

                if (_IndexedRow < 0)
                {
                    _IndexedRow = 0;
                }
            }
        }

        public bool AutoAdjustTitleSize
        {
            get { return _AutoAdjustTitleSize; }
            set { _AutoAdjustTitleSize = value; }
        }
        
		
		
        #endregion


		#region Basic Functions: CalculateResult &CreatePrintingTable 

		public override void CalculateResult(DataTable i_Table)
		{
			base.CalculateResult (i_Table);          

			DataRow dr=this.GetFirstDataRow(i_Table);

            this.MaskInfoSetting.CalcuateResult(dr);
			
		}

		private DataRow GetFirstDataRow(DataTable dt)	
		{	
			if(dt==null)return null;

			Int32Collection m_Rows=this.OneValueScFilter.Filter.GetFilteredRows(dt);

			if(this.ExControl.Report!=null)m_Rows=this.ExControl.Report.Filter.GetFilteredRows(dt,m_Rows);

			m_Rows=this.RepeatFilter.Filter.GetFilteredRows(dt,m_Rows);

			m_Rows=this.Filter.GetFilteredRows(dt,m_Rows);

			GroupResultCollection results=this.SortingColumns.Sorting(dt,m_Rows);

			if(results!=null&&results.Count!=0)
			{
				m_Rows=results[0].RowIndicators;
			}

			if(this.IndexRow>=m_Rows.Count||IndexRow<0)
			{
				return null; 
			}

			int getRow=m_Rows[IndexRow];

			return dt.Rows[getRow];          
			
		}

        private void ApplyRowHeightStyle(int m_Rows)
        {
            if (this.RowsHight.Count <= 0) return;

            if (this.RowsHight.Count != m_Rows) return;

            for (int m_row = 0; m_row < m_Rows; m_row++)
            {
                IWebbTableCell cell = this.PrintingTable.GetCell(m_row, 0);

                int cellHeight = cell.CellStyle.Height;

                cell.CellStyle.Height = Math.Max(this.RowsHight[m_row], cellHeight);
            }
        }
        private void ApplyColumnWidthStyle(int m_Cols)
        {
            if (this.ColumnsWidth.Count <= 0) return;

            int count = Math.Min(this.ColumnsWidth.Count, m_Cols);

            for (int m_col = 0; m_col < count; m_col++)
            {
                IWebbTableCell cell = this.PrintingTable.GetCell(0, m_col);

                cell.CellStyle.Width = this.ColumnsWidth[m_col];
            }
        }


        private void ConvertOldMaskInfos()
        {
            #region Convert Old Mask Info
            if (_MaskInfoSetting.Count > 0 && this._MakeChanges)
            {
                MaskInfo maskInfo = this._MaskInfoSetting[0] as MaskInfo;

                if (maskInfo != null)
                {
                    if (maskInfo.ShowTitle) this._ColumnsWidth.Add(maskInfo.TitleWidth);

                    this.ColumnsWidth.Add(maskInfo.MaskedWidth);

                    MaskInfo maskInfoCopy = maskInfo.Copy();

                    maskInfoCopy.BrotherMaskInfos = new MaskInfoCollection();

                    for (int i = 1; i < this._MaskInfoSetting.Count; i++)
                    {
                        MaskInfo brotherMaskInfo = this._MaskInfoSetting[i] as MaskInfo;

                        if (brotherMaskInfo.ShowTitle) this._ColumnsWidth.Add(brotherMaskInfo.TitleWidth);

                        this._ColumnsWidth.Add(brotherMaskInfo.MaskedWidth);

                        maskInfoCopy.BrotherMaskInfos.Add(brotherMaskInfo.Copy());

                    }

                    this._MaskInfoSetting.Clear();

                    this._MaskInfoSetting.Add(maskInfoCopy);

                    this._MakeChanges = false;
                }

            }
            #endregion
        }

   
		public override bool CreatePrintingTable()
		{
            this.ConvertOldMaskInfos();

            int rows = this.MaskInfoSetting.GetTotalRows();

            int cols = this.MaskInfoSetting.GetTotalColumns();

			if(rows == 0 || cols == 0) return false;

			this.PrintingTable = new WebbTable(rows,cols);

            this.PrintingTable.SetNoBorders();            

            this.MaskInfoSetting.SetCellValueAnsStyle(this.PrintingTable); 

            ApplyColumnWidthStyle(cols);

            if (this.AutoAdjustTitleSize)
            {
                this.PrintingTable.AutoAdjustSize(this.ExControl.CreateGraphics(), false, true);
            }
            else 
            {
                this.PrintingTable.AutoAdjustSize(this.ExControl.CreateGraphics(), true, false);
            }

            ApplyRowHeightStyle(rows);

			return true;
		}
		public SizeF MeasureSize(IWebbTableCell i_Cell,Graphics m_Graph)
		{
			SizeF m_Size =  m_Graph.MeasureString(i_Cell.Text,i_Cell.CellStyle.Font);

            m_Size.Width+=3;

			 m_Size.Height+=5;

			return m_Size;
		}
	

		public SizeF MeasureSize(IWebbTableCell i_Cell,Graphics m_Graph,int Width)
		{
			SizeF m_Size =  m_Graph.MeasureString(i_Cell.Text,i_Cell.CellStyle.Font,Width);

			m_Size.Width+=4;

			m_Size.Height+=3;

			return m_Size;
		}
	

		
		#endregion

		#region Draw		
		public override void Paint(PaintEventArgs e)
		{
			if(this.PrintingTable != null)
			{
				if(this.ThreeD)
				{
					this.PrintingTable.PaintTable3D(e,true,Rectangle.Empty);
				}
				else
				{
					this.PrintingTable.PaintTable(e,true,Rectangle.Empty);
				}
			}
			else
			{
				base.Paint (e);
			}
		}

		
		public override int CreateArea(string areaName, IBrickGraphics graph)
		{
			if(PrintingTable!=null)
			{
				this.PrintingTable.ExControl=this.ExControl;
			}

			return base.CreateArea (areaName, graph);
		}
		

		#endregion

		public MaskedTextControlView(Webb.Reports.ExControls.ExControl i_Control) : base(i_Control)
		{

            this._MaskInfoSetting = new MaskInfoCollection();

            this._MaskInfoSetting.Add(new MaskInfo());

            this._RowsHight = new Int32Collection();
            this._ColumnsWidth = new Int32Collection();
			
		}	

		#region Serialization By Simon's Macro 2009-12-2 12:17:49
		public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			base.GetObjectData(info,context);
            info.AddValue("_MaskInfoSetting", this._MaskInfoSetting, typeof(Webb.Reports.ExControls.Data.MaskInfoCollection));
			info.AddValue("_AutoAdjustTitleSize",_AutoAdjustTitleSize);
			info.AddValue("_Filter",this._Filter,typeof(DBFilter));
			info.AddValue("_IndexedRow",this._IndexedRow);
			info.AddValue("_SortingColumns",_SortingColumns,typeof(SortingColumnCollection));
            info.AddValue("_ColumnsWidth", this._ColumnsWidth, typeof(Int32Collection));
            info.AddValue("_RowsHight", this._RowsHight, typeof(Int32Collection));
		}

		public MaskedTextControlView(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context):base(info,context)
		{
           
			try
			{
				_SortingColumns=(SortingColumnCollection)info.GetValue("_SortingColumns",typeof(SortingColumnCollection));
			}
			catch
			{
				_SortingColumns=new SortingColumnCollection();
			}            
            try
            {
                _AutoAdjustTitleSize = info.GetBoolean("_AutoAdjustTitleSize");
            }
            catch
            {
                _AutoAdjustTitleSize = true;
            }		
            try
            {
                _MakeChanges = false;

                this._ColumnsWidth = info.GetValue("_ColumnsWidth", typeof(Int32Collection)) as Int32Collection;
            }
            catch
            {
                _MakeChanges = true;

                this._ColumnsWidth = new Int32Collection();
              
            }
            try
            {
                this._MaskInfoSetting = (Webb.Reports.ExControls.Data.MaskInfoCollection)info.GetValue("_MaskInfoSetting", typeof(Webb.Reports.ExControls.Data.MaskInfoCollection));
                
            }
            catch
            {
                _MaskInfoSetting = new MaskInfoCollection();
            }

            try
            {
                this._RowsHight = info.GetValue("_RowsHight", typeof(Int32Collection)) as Int32Collection;
            }
            catch
            {
                this._RowsHight = new Int32Collection(); ;
            }            	
			try
			{
				_IndexedRow=info.GetInt32("_IndexedRow");
			}
			catch
			{
				_IndexedRow=0;
			}
			try
			{
				this._Filter=(DBFilter)info.GetValue("_AutoAdjustTitleSize",typeof(DBFilter));
			}
			catch
			{
				_Filter=new DBFilter();
			}            
		}
		#endregion

        #region Only For CCRM Data
        public override void GetALLUsedFields(ref ArrayList usedFields)
        {
            if (this.MaskInfoSetting != null)
            {
                foreach (MaskInfo  maskInfo in this.MaskInfoSetting)
                {
                    if (maskInfo == null) continue;

                    maskInfo.GetALLUsedFields(ref usedFields);
                  
                }
            }

            this.Filter.GetAllUsedFields(ref usedFields);


        }
        #endregion
		
		
	}
}