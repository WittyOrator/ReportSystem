/***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:View.cs
 * Author:Country.Wu [EMail:Webb.Country.Wu@163.com]
 * Create Time:10/31/2007 03:11:26 PM
 * Copyright:1986-2007@Webb Electronics all right reserved.
 * Purpose:
 * ***********************************************************************/
#region History
/*
 * //Author@DateTime : Description
 * */
#endregion History

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
using System.Security.Permissions;
using Webb.Collections;
using Webb.Reports.DataProvider;
using Webb.Reports.ExControls.Styles;

using System.ComponentModel.Design;
using System.ComponentModel;
using System.Drawing.Design;

namespace Webb.Reports.ExControls.Views
{
  
    #region public class GradingView
    [Serializable]
    public class GradingView : ExControlView, IMultiHeader
    {
        #region Base  element   : Field & Properties & Serializable functions
        //Fields
        #region Fields
        protected GradingSectionCollection _GradingSectionCollection = new GradingSectionCollection();
        protected Webb.Reports.ExControls.Data.GroupInfo _RootGroupInfo=new FieldGradingInfo();
        protected CellSizeAutoAdaptingTypes _CellSizeAutoAdapting;
   
        protected Webb.Data.DBFilter _Filter=new Webb.Data.DBFilter();
        protected int _TopCount=0;
        protected bool _ShowRowIndicators=false;
        protected bool _HaveHeader=true;       		

        protected bool _SectionInOneRow=true;								//05-07-2008@Scott
       
        protected Int32Collection _HeaderRows=new Int32Collection();							//header row indicators for style builder
        protected Int32Collection _SectionRows=new Int32Collection();							//section row indicators for style builder
        protected Int32Collection _TotalRows=new Int32Collection();							//total row indicators for style builder
         protected Int32Collection _ColumnsWidth=new Int32Collection();
        protected Int32Collection _RowsHight=new Int32Collection();
        protected Styles.ExControlStyles _Styles = new ExControlStyles();     //2008-8-28 9:46:28@simon    
        protected HeadersData _HeadersData = null;
       
        protected OurBordersSetting _OurBordersSetting = new OurBordersSetting();

        [NonSerialized]
        protected Int32Collection _GroupColumns = new Int32Collection();


        #endregion

        #region Properties
        public Webb.Reports.ExControls.Data.GradingSectionCollection GradingSectionCollection
        {
            get
            {
                if (_GradingSectionCollection == null) _GradingSectionCollection = new GradingSectionCollection();
                return _GradingSectionCollection;
            }
            set
            {
                _GradingSectionCollection = value;
            }
        }

        public Webb.Reports.ExControls.Data.GroupInfo RootGroupInfo
        {
            get
            {
                if (_RootGroupInfo == null) _RootGroupInfo = new FieldGradingInfo();
                return _RootGroupInfo;
            }
            set
            {
                _RootGroupInfo = value;
            }
        }

        public Webb.Reports.ExControls.Views.CellSizeAutoAdaptingTypes CellSizeAutoAdapting
        {
            get
            {
                return _CellSizeAutoAdapting;
            }
            set
            {
                _CellSizeAutoAdapting = value;
            }
        }

        public Webb.Data.DBFilter Filter
        {
            get
            {
                if (_Filter == null) _Filter = new Webb.Data.DBFilter();
                return _Filter;
            }
            set
            {
                _Filter = value;
            }
        }

        public int TopCount
        {
            get
            {
                return _TopCount;
            }
            set
            {
                _TopCount = value;
            }
        }

        public bool ShowRowIndicators
        {
            get
            {
                return _ShowRowIndicators;
            }
            set
            {
                _ShowRowIndicators = value;
            }
        }

        public bool HaveHeader
        {
            get
            {
                return _HaveHeader;
            }
            set
            {
                _HaveHeader = value;
            }
        }

        public bool SectionInOneRow
        {
            get
            {
                return _SectionInOneRow;
            }
            set
            {
                _SectionInOneRow = value;
            }
        }

        public Webb.Collections.Int32Collection HeaderRows
        {
            get
            {
                return _HeaderRows;
            }
            set
            {
                _HeaderRows = value;
            }
        }

        public Webb.Collections.Int32Collection SectionRows
        {
            get
            {               
                if (_SectionRows == null) _SectionRows = new Int32Collection();
                return _SectionRows;
            }
            set
            {
                _SectionRows = value;
            }
        }

        public Webb.Collections.Int32Collection TotalRows
        {
            get
            {
                if (_TotalRows == null) _TotalRows = new Int32Collection();
                return _TotalRows;
            }
            set
            {
                _TotalRows = value;
            }
        }     

      

        public Webb.Collections.Int32Collection ColumnsWidth
        {
            get
            {
                if (_ColumnsWidth == null) _ColumnsWidth = new Int32Collection();
                return _ColumnsWidth;
            }
            set
            {
                _ColumnsWidth = value;
            }
        }

        public Webb.Collections.Int32Collection RowsHight
        {
            get
            {
                if (_RowsHight == null) _RowsHight = new Int32Collection();
                return _RowsHight;
            }
            set
            {
                _RowsHight = value;
            }
        }

        public Webb.Reports.ExControls.Styles.ExControlStyles Styles
        {
            get
            {
                if(_Styles==null)_Styles=new ExControlStyles();
                return _Styles;
            }
            set
            {
                _Styles = value;
            }
        }

        public Webb.Reports.ExControls.Data.HeadersData HeadersData
        {
            get
            {
                return _HeadersData;
            }
            set
            {
                _HeadersData = value;
            }
        }
        public OurBordersSetting OurBordersSetting
        {
            get
            {
                if (_OurBordersSetting == null) _OurBordersSetting = new OurBordersSetting();

                return _OurBordersSetting;
            }
            set
            {
                _OurBordersSetting = value;
            }
        }
        #endregion

        #region Serialization By Simon's Macro 2011-7-18 13:16:44
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("_GradingSectionCollection", _GradingSectionCollection, typeof(Webb.Reports.ExControls.Data.GradingSectionCollection));
            info.AddValue("_RootGroupInfo", _RootGroupInfo, typeof(Webb.Reports.ExControls.Data.GroupInfo));
            info.AddValue("_CellSizeAutoAdapting", _CellSizeAutoAdapting, typeof(Webb.Reports.ExControls.Views.CellSizeAutoAdaptingTypes));
            info.AddValue("_Filter", _Filter, typeof(Webb.Data.DBFilter));
            info.AddValue("_TopCount", _TopCount);
            info.AddValue("_ShowRowIndicators", _ShowRowIndicators);
            info.AddValue("_HaveHeader", _HaveHeader);
            info.AddValue("_SectionInOneRow", _SectionInOneRow);
            info.AddValue("_HeaderRows", _HeaderRows, typeof(Webb.Collections.Int32Collection));
            info.AddValue("_SectionRows", _SectionRows, typeof(Webb.Collections.Int32Collection));
            info.AddValue("_TotalRows", _TotalRows, typeof(Webb.Collections.Int32Collection));         
           
            info.AddValue("_ColumnsWidth", _ColumnsWidth, typeof(Webb.Collections.Int32Collection));
            info.AddValue("_RowsHight", _RowsHight, typeof(Webb.Collections.Int32Collection));
            info.AddValue("_Styles", _Styles, typeof(Webb.Reports.ExControls.Styles.ExControlStyles));
            info.AddValue("_HeadersData", _HeadersData, typeof(Webb.Reports.ExControls.Data.HeadersData));
            info.AddValue("_OurBordersSetting", _OurBordersSetting, typeof(Webb.Reports.ExControls.Views.OurBordersSetting));

        }

        public GradingView(SerializationInfo info, StreamingContext context):base(info,context)
        {
            try
            {
                _GradingSectionCollection = (Webb.Reports.ExControls.Data.GradingSectionCollection)info.GetValue("_GradingSectionCollection", typeof(Webb.Reports.ExControls.Data.GradingSectionCollection));
            }
            catch
            {
                _GradingSectionCollection = new GradingSectionCollection();
            }
            try
            {
                _RootGroupInfo = (Webb.Reports.ExControls.Data.GroupInfo)info.GetValue("_RootGroupInfo", typeof(Webb.Reports.ExControls.Data.GroupInfo));
            }
            catch
            {
                _RootGroupInfo = new FieldGradingInfo();
            }
            try
            {
                _CellSizeAutoAdapting = (Webb.Reports.ExControls.Views.CellSizeAutoAdaptingTypes)info.GetValue("_CellSizeAutoAdapting", typeof(Webb.Reports.ExControls.Views.CellSizeAutoAdaptingTypes));
            }
            catch
            {
                _CellSizeAutoAdapting = CellSizeAutoAdaptingTypes.NotUse;
            }
            try
            {
                _Filter = (Webb.Data.DBFilter)info.GetValue("_Filter", typeof(Webb.Data.DBFilter));
            }
            catch
            {
                _Filter = new Webb.Data.DBFilter();
            }
            try
            {
                _TopCount = info.GetInt32("_TopCount");
            }
            catch
            {
                _TopCount = 0;
            }
            try
            {
                _ShowRowIndicators = info.GetBoolean("_ShowRowIndicators");
            }
            catch
            {
                _ShowRowIndicators = false;
            }
            try
            {
                _HaveHeader = info.GetBoolean("_HaveHeader");
            }
            catch
            {
                _HaveHeader = true;
            }
            try
            {
                _SectionInOneRow = info.GetBoolean("_SectionInOneRow");
            }
            catch
            {
                _SectionInOneRow = true;
            }
            try
            {
                _HeaderRows = (Webb.Collections.Int32Collection)info.GetValue("_HeaderRows", typeof(Webb.Collections.Int32Collection));
            }
            catch
            {
                _HeaderRows = new Int32Collection();
            }
            try
            {
                _SectionRows = (Webb.Collections.Int32Collection)info.GetValue("_SectionRows", typeof(Webb.Collections.Int32Collection));
            }
            catch
            {
                _SectionRows = new Int32Collection();
            }
            try
            {
                _TotalRows = (Webb.Collections.Int32Collection)info.GetValue("_TotalRows", typeof(Webb.Collections.Int32Collection));
            }
            catch
            {
                _TotalRows = new Int32Collection();
            }          
            
            try
            {
                _ColumnsWidth = (Webb.Collections.Int32Collection)info.GetValue("_ColumnsWidth", typeof(Webb.Collections.Int32Collection));
            }
            catch
            {
                _ColumnsWidth = new Int32Collection();
            }
            try
            {
                _RowsHight = (Webb.Collections.Int32Collection)info.GetValue("_RowsHight", typeof(Webb.Collections.Int32Collection));
            }
            catch
            {
                _RowsHight = new Int32Collection();
            }
            try
            {
                _Styles = (Webb.Reports.ExControls.Styles.ExControlStyles)info.GetValue("_Styles", typeof(Webb.Reports.ExControls.Styles.ExControlStyles));
            }
            catch
            {
                _Styles = new ExControlStyles();
            }
            try
            {
                _HeadersData = (HeadersData)info.GetValue("_HeadersData", typeof(Webb.Reports.ExControls.Data.HeadersData));
            }
            catch
            {
                _HeadersData =null;
            }
            try
            {
                _OurBordersSetting = (Webb.Reports.ExControls.Views.OurBordersSetting)info.GetValue("_OurBordersSetting", typeof(Webb.Reports.ExControls.Views.OurBordersSetting));
            }
            catch
            {
                _OurBordersSetting = new OurBordersSetting();
            }
        }
        #endregion

        #endregion

     

        //ctor
        public GradingView(GradingControl i_Control)
            : base(i_Control as ExControl)
        {
            _GradingSectionCollection = new GradingSectionCollection();
            _RootGroupInfo = new FieldGradingInfo();

        }

        //Calculate grouped rows and columns
        #region Calculate grouped rows and columns
        /// <summary>
        /// Calculate grouped columns
        /// </summary>
        /// <returns></returns>
        public int GetGroupedColumns()
        {
            _GroupColumns.Clear();

            if (this.RootGroupInfo == null) return 1;

            int nCols = 0;

            if (this._ShowRowIndicators)
            {
                nCols += 1;
            }

            this.RootGroupInfo.IsSectionOutSide = this.SectionInOneRow;

            this.GetGroupedColumns(this.RootGroupInfo, ref nCols);

            return nCols;
        }

        private void GetGroupedColumns(GroupInfo groupInfo, ref int nCols)
        {
            if (!groupInfo.IsSectionOutSide)
            {            
                #region  summaries before the group
                if (groupInfo.FollowSummaries)
                {
                    foreach (GroupSummary summary in groupInfo.Summaries)
                    {
                        summary.ColumnIndex = nCols++;
                    }
                    
                }
                #endregion

                bool visible = GroupInfo.IsVisible(groupInfo);

                if (visible)
                {
                    _GroupColumns.Add(nCols);

                    groupInfo.ColumnIndex = nCols++;
                }

                #region  summaries after the group
                if (!groupInfo.FollowSummaries)
                {
                    foreach (GroupSummary summary in groupInfo.Summaries)
                    {
                        summary.ColumnIndex = nCols++;
                    }                    
                }
                #endregion
            }

            foreach (GroupInfo subGroupInfo in groupInfo.SubGroupInfos)
            {
                subGroupInfo.IsSectionOutSide = false;

                this.GetGroupedColumns(subGroupInfo, ref nCols);
            }
        }


     
        /// <summary>
        /// Calculate grouped rows
        /// </summary>
        /// <returns></returns>
        public int GetGroupedRows()
        {
            int m_value = 0;

            this.GetSubRows(this._RootGroupInfo, ref m_value);

            m_value += this.GetHeaderRowsCount();	//add header rows          

            return m_value;
        }

        private int GetHeaderRowsCount()
        {
            int nRet = 0;

            if (this._HeadersData != null && this._HeadersData.RowCount > 0 && this._HeadersData.ColCount > 0)   //2008-8-29 9:12:31@simon
            {
                nRet += this._HeadersData.RowCount;  //2008-8-29 9:12:37@simon				

            }

            if (this.HaveHeader)
            {
                nRet++;
            }           
            return nRet;
        }

        /// <summary>
        /// caculate total rows grouped
        /// </summary>
        /// <param name="i_GroupInfo"></param>
        /// <param name="i_value">must be zero</param>
        private void GetSubRows(GroupInfo i_GroupInfo, ref int i_value)
        {
            int m_TopGroups = i_GroupInfo.GetGroupedRows();

            if (i_value > 0 && m_TopGroups > 0)
            {
                i_value--;		//-----------------------------------------------
                //|parent group result1	|	child group result1	|	//truncate this row
                //-----------------------------------------------
                //|						|	child group result2	|
                //-----------------------------------------------
                //|						|	...					|
                //-----------------------------------------------

                if (i_GroupInfo.ParentGroupResult != null && i_GroupInfo.ParentGroupResult.ParentGroupInfo != null)
                {
                    if (i_GroupInfo.ParentGroupResult.ParentGroupInfo.IsSectionOutSide)
                    {
                        i_value++;	//-----------------------------------------------
                        //|parent group result1	|						|	//add this row
                        //-----------------------------------------------
                        //|						|	child group result1	|
                        //-----------------------------------------------
                        //|						|	child group result2	|
                        //-----------------------------------------------
                        //|						|	...					|
                        //-----------------------------------------------
                    }
                }
            }

            if (i_GroupInfo != null && i_GroupInfo.SubGroupInfos.Count == 0)		//calculate leaf group rows
            {
                if (i_GroupInfo.IsSectionOutSide)
                {
                    i_value += m_TopGroups;	//???
                }
            }

            if (i_GroupInfo.AddTotal) i_value++;	//calculate total row

            i_value += m_TopGroups;	//calculate grouped result rows

            if (i_GroupInfo.GroupResults != null)
            {
                for (int m_index = 0; m_index < m_TopGroups/*Math.Min(m_TopGroups,i_GroupInfo.GroupResults.Count)*/; m_index++)
                {
                    GroupResult m_Result = i_GroupInfo.GroupResults[m_index];

                    int tempValue = i_value, maxValue = 0/*,tempCount = 0,maxCount = 0*/;

                    foreach (GroupInfo groupInfo in m_Result.SubGroupInfos)
                    {
                        GetSubRows(groupInfo, ref tempValue);

                        maxValue = Math.Max(maxValue, tempValue);

                        tempValue = i_value;
                    }
                
                    i_value = Math.Max(i_value, maxValue);
                }
            }
        }
        #endregion

        #region Modified Area
        private bool RepeatHeader()
        {
            bool repeat = !this.ExControl.Report.Template.OneValuePerPage && !this.ExControl.Report.Template.ControlHeaderOnce;
            if ((this.HeadersData == null || this.HeadersData.RowCount <= 0) && !this.HaveHeader)
            {
                repeat = false;
            }
            return repeat;
        }
        #endregion        //End Modify at 2008-10-21 10:10:33@Simon

        public override int CreateArea(string areaName, IBrickGraphics graph)
        {
            if (this.PrintingTable != null)
            {
                #region Modified Area
                this.PrintingTable.RepeatedHeader = this.RepeatHeader();
                if (this.HeadersData != null)
                {
                    if (this.HaveHeader)
                    {
                        this.PrintingTable.HeaderCount = this._HeadersData.RowCount + 1;
                    }
                    else
                    {
                        this.PrintingTable.HeaderCount = this._HeadersData.RowCount;
                    }
                }
                else
                {
                    if (this.HaveHeader)
                    {
                        this.PrintingTable.HeaderCount = 1;
                    }
                    else
                    {
                        this.PrintingTable.RepeatedHeader = false;
                        this.PrintingTable.HeaderCount = 0;
                    }
                }

                #endregion        //End Modify at 2008-10-21 9:35:11@Simon

                this.PrintingTable.ExControl = this.ExControl;

                this.PrintingTable.HeightPerPage = this.ExControl.Report.GetHeightPerPage(); //this._HeightPerPage;

                this.PrintingTable.ReportHeaderHeight = this.ExControl.Report.GetReportHeaderHeight();	//report header

                this.PrintingTable.ReportFooterHeight = this.ExControl.Report.GetReportFooterHeight();	//report footer
            }

            int endPosY = base.CreateArea(areaName, graph);
        
            return endPosY;
        }

        //Override members
        #region override members
        /// <summary>
        /// Paint in design mode
        /// </summary>
        /// <param name="e"></param>
        override public void Paint(PaintEventArgs e)
        {
            if (this.PrintingTable != null)
            {
                if (this.ThreeD)
                {
                    this.PrintingTable.PaintTable3D(e, false, this.ExControl.XtraContainer.Bounds);
                }
                else
                {
                    this.PrintingTable.PaintTable(e, false, this.ExControl.XtraContainer.Bounds);
                }
            }
            else
            {
                base.Paint(e);
            }
        }
      
        /// <summary>
        /// Calculate grouped result
        /// </summary>
        /// <param name="i_Table">data source</param>
        public override void CalculateResult(DataTable i_Table)
        {            
            //If have no data source ,clear group struct
            if (i_Table == null)
            {
                this.RootGroupInfo.ClearGroupResult(this._RootGroupInfo);

                return;
            }

            //Filter rows
            Webb.Collections.Int32Collection m_Rows = new Int32Collection();

            if (this.ExControl != null && this.ExControl.Report != null)
            {
                m_Rows = this.ExControl.Report.Filter.GetFilteredRows(i_Table);  //2009-5-25 11:02:57@Simon Add this Code
            }

            m_Rows = this.OneValueScFilter.Filter.GetFilteredRows(i_Table, m_Rows);	//06-04-2008@Scott

            m_Rows = this.RepeatFilter.Filter.GetFilteredRows(i_Table, m_Rows);	 //Added this code at 2008-12-26 12:22:40@Simon

            this.Filter = AdvFilterConvertor.GetAdvFilter(DataProvider.VideoPlayBackManager.AdvReportFilters, this.Filter);    //2009-4-29 11:37:37@Simon Add UpdateAdvFilter

            m_Rows = this.Filter.GetFilteredRows(i_Table, m_Rows);	//06-04-2008@Scott

            DataTable table = this.GradingSectionCollection.CalcualteGradeTable(i_Table, m_Rows);

            Webb.Data.DBFilter filter = new Webb.Data.DBFilter();

            Int32Collection rows = filter.GetFilteredRows(table);

            this._RootGroupInfo.CalculateGroupResult(table, rows, rows, this._RootGroupInfo);

            GradingSectionCollection.TruncateRowIdIntoPlayId(table, this._RootGroupInfo);
        
        }



        private void AdjustHeaderStyle()
        {
            if (this.HeaderRows.Count <= 0 || this.HeadersData == null) return;

            HeaderRows.Sort();

            this.HeadersData.SetHeadGridLine(this.PrintingTable, this.HeaderRows);

            int MinHeaderRow = HeaderRows[0];

            int MaxHeaderRow = HeaderRows[HeaderRows.Count - 1];

            #region UpdateHeadeStyle

            for (int row = MinHeaderRow; row <= MaxHeaderRow; row++)
            {
                int TableHeaderRow = row - MinHeaderRow;

                if (TableHeaderRow >= this.HeadersData.RowCount) break;

                for (int col = 0; col < this.HeadersData.ColCount; col++)
                {
                    HeaderCell headerCell = this.HeadersData.GetCell(TableHeaderRow, col);

                    IWebbTableCell cell = PrintingTable.GetCell(row, col);

                    if (headerCell == null || cell == null) continue;

                    if (headerCell.NeedChangeStyle)
                    {
                        cell.CellStyle = headerCell.CellStyle.Copy();
                    }
                }
            }

            #endregion

            #region Cols In Merge

            foreach (int col in this.HeadersData.ColsToMerge)
            {
                IWebbTableCell Mergedcell = PrintingTable.GetCell(MaxHeaderRow, col);

                this.PrintingTable.MergeCells(MinHeaderRow, MaxHeaderRow, col, col);

                IWebbTableCell bordercell = PrintingTable.GetCell(MinHeaderRow, col);

                if (this.HaveHeader)
                {
                    bordercell.Text = Mergedcell.Text;

                    bordercell.CellStyle.StringFormat = Mergedcell.CellStyle.StringFormat;
                }

                if ((bordercell.CellStyle.StringFormat & StringFormatFlags.DirectionVertical) != 0)
                {
                    bordercell.CellStyle.HorzAlignment = HorzAlignment.Far;
                    bordercell.CellStyle.VertAlignment = VertAlignment.Center;
                }
                else
                {
                    bordercell.CellStyle.HorzAlignment = HorzAlignment.Center;
                    bordercell.CellStyle.VertAlignment = VertAlignment.Center;
                }

            }
            #endregion
        }

        /// <summary>
        /// Create printing table
        /// </summary>
        /// <returns></returns>
        public override bool CreatePrintingTable()
        {
            if (this._RootGroupInfo == null||!(this._RootGroupInfo is FieldGradingInfo)) return false;



            int m_Rows = this.GetGroupedRows();

            int m_Column = this.GetGroupedColumns();

            if (m_Rows <= 0 || m_Column <= 0)
            {
                this.PrintingTable = null;
                return false;
            }

            System.Diagnostics.Debug.WriteLine(string.Format("Create print table:{0}X{1}", m_Rows, m_Column));

            #region Modify codes at 2009-4-8 11:04:18@Simon
            int RealTopCount = this.GetRealTopCount();

            if (RealTopCount > 0 && m_Rows < RealTopCount)
            {
                m_Rows = RealTopCount;
            }
            #endregion        //End Modify

            this.PrintingTable = new WebbTable(m_Rows, m_Column);
            //Set value
            this.HeaderRows.Clear();
            this.SectionRows.Clear();
            this.TotalRows.Clear();           

        
            this.SetTableValue();

            StyleBuilder.StyleRowsInfo m_StyleInfo = new Styles.StyleBuilder.StyleRowsInfo(this._HeaderRows, this._SectionRows, this._TotalRows, this.ShowRowIndicators, this.HaveHeader);

            #region TopCount
            if (this.TopCount <= 0)
            {
                Int32Collection Rows = this.HeaderRows.Combine(HeaderRows, TotalRows);

                this.PrintingTable.DeleteEmptyRows(Rows, m_StyleInfo, this.ShowRowIndicators);

            }
            else
            {
                this.PrintingTable.DeleteExcrescentRows(RealTopCount);
            }

            m_Rows = this.PrintingTable.GetRows();

            if (m_Rows <= 0)
            {
                this.PrintingTable = null;
                return false;
            }
            #endregion

            Int32Collection ignoreRows = this.HeaderRows.Combine(this.HeaderRows, this.SectionRows, this.TotalRows);

            StyleBuilder styleBuilder = new StyleBuilder();

            styleBuilder.BuildGroupStyle(this.PrintingTable, m_StyleInfo, this.RootGroupInfo, this.Styles, ignoreRows);

            this.AdjustHeaderStyle();
     
            this.ApplyColumnWidthStyle(m_Column);

            this.ApplyRowHeightStyle(m_Rows);

            switch (this.CellSizeAutoAdapting)
            {
                case CellSizeAutoAdaptingTypes.NotUse:
                    break;
                case CellSizeAutoAdaptingTypes.WordWrap:
                    this.PrintingTable.AutoAdjustSize(this.ExControl.CreateGraphics(), true, false);
                    break;
                case CellSizeAutoAdaptingTypes.OneLine:
                    this.PrintingTable.AutoAdjustSize(this.ExControl.CreateGraphics(), false, false);
                    break;
            }           
         
            this.OurBordersSetting.ChangeTableOutBorders(this.PrintingTable);

            System.Diagnostics.Debug.WriteLine("Create print table completely");

            return true;
        }

        #endregion
     
     
        private void ApplyRowHeightStyle(int m_Rows)
        {
            if (this.RowsHight.Count <= 0) return;

            int count = Math.Min(this.RowsHight.Count, m_Rows);

            for (int m_row = 0; m_row < count; m_row++)
            {
                IWebbTableCell cell = this.PrintingTable.GetCell(m_row, 0);

                if (cell == null) continue;

                cell.CellStyle.Height = this.RowsHight[m_row];
            }
        }

        private void ApplyColumnWidthStyle(int m_Cols)
        {
            for (int i = this.ColumnsWidth.Count - 1; i >= 0; i--)
            {
                if (this.ColumnsWidth[i] == 0) this.ColumnsWidth.RemoveAt(i);
            }

            if (this.ColumnsWidth.Count <= 0) return;

            int count = Math.Min(this.ColumnsWidth.Count, m_Cols);

            int m_rows = this.PrintingTable.GetRows();

            for (int m_col = 0; m_col < count; m_col++)
            {
                IWebbTableCell cell = this.PrintingTable.GetCell(0, m_col);

                cell.CellStyle.Width = ColumnsWidth[m_col];
            }
        }      

        private int GetRealTopCount()
        {
            if (this.TopCount <= 0) return TopCount;

            int RealTopCount = this.TopCount;

            RealTopCount += this.GetHeaderRowsCount();
           
            return RealTopCount;
        }

        private void SetTableValue()
        {
            int m_Rows = 0, m_Col = 0;

            if (this.ShowRowIndicators) m_Col = 1;	//add row indicator columns   

            int nHeaderStart = 0, nHeaderCount = 0;

            this.SetHeaderValue(ref m_Rows);	//set header value

             nHeaderCount = m_Rows - nHeaderStart;

            this.SetHeaderRows(nHeaderStart, nHeaderCount);

            this.SetRowsValue(ref m_Rows, ref m_Col, this._RootGroupInfo);	//set row value	

            if (this.ShowRowIndicators)
            {//set row indicator value
                this.SetRowIndicators(this.PrintingTable.GetRows());
            }
        }       
     

        private void SetHeaderRows(int nStartRow, int nHeaderRowCount)
        {
            for (int i = nStartRow; i < nStartRow + nHeaderRowCount; i++)
            {
                if (!HeaderRows.Contains(i)) this.HeaderRows.Add(i);
            }
        }

        #region Modify codes at 2008-12-8 15:36:23@Simon

        public ArrayList GetPrnHeader(out ArrayList formats)
        {
            ArrayList prnHeaders = new ArrayList();
            formats = new ArrayList();

            int m_Column = this.GetGroupedColumns();
            for (int i = 0; i < m_Column; i++)
            {
                prnHeaders.Add("");
                formats.Add(0);
            }
            int nCol = 0;

            if (this._ShowRowIndicators) nCol++;
            if (this.RootGroupInfo != null) GetHeaderValue(this.RootGroupInfo, prnHeaders, formats, ref nCol);

            return prnHeaders;
        }

        private void GetHeaderValue(GroupInfo groupInfo, ArrayList prnHeaders, ArrayList formats, ref int nCol)
        {
            bool visible = GroupInfo.IsVisible(groupInfo);

            if (groupInfo.IsSectionOutSide)
            {  
            }
            else
            {
                if (groupInfo.FollowSummaries)
                {
                    if (groupInfo.Summaries != null)
                    {
                        foreach (GroupSummary m_Summary in groupInfo.Summaries)
                        {
                            prnHeaders[nCol] = m_Summary.ColumnHeading;

                            formats[nCol] = m_Summary.HeadingFormat;

                            nCol++;
                        }
                    }
                }

                if (visible && (!groupInfo.DistinctValues || groupInfo is FieldGroupInfo))	//if set OnValuePerRow, don't set group title
                {
                    prnHeaders[nCol] = groupInfo.ColumnHeading;
                    formats[nCol] = groupInfo.HeadingFormat;

                    nCol++;
                }

                if (groupInfo.FollowSummaries)
                { }
                else
                {
                    if (groupInfo.Summaries != null)
                    {
                        foreach (GroupSummary m_Summary in groupInfo.Summaries)
                        {
                            prnHeaders[nCol] = m_Summary.ColumnHeading;
                            formats[nCol] = m_Summary.HeadingFormat;
                            nCol++;
                        }
                    }
                }
            }
            foreach (GroupInfo subGroupInfo in groupInfo.SubGroupInfos)
            {
                GetHeaderValue(subGroupInfo, prnHeaders, formats, ref nCol);
            }
        }
        #endregion        //End Modify

        private void SetHeaderValue(ref int nRow)
        {
            int nCol = 0;

            if (this.HeadersData != null && this.HeadersData.RowCount > 0 && this.HeadersData.ColCount > 0)    //2008-8-29 9:12:52@simon
            {
                this.HeadersData.SetHeaders(PrintingTable, ref nRow, this);

            }

            if (this._ShowRowIndicators) nCol++;

            if (this.HaveHeader)   //Modified at 2008-10-21 8:49:30@Simon
            {
                this.SetHeaderValue(this._RootGroupInfo, nRow, ref nCol);

                nRow++;
            }

        }

        private void SetHeaderValue(GroupInfo groupInfo, int nRow, ref int nCol)
        {
            bool visible = GroupInfo.IsVisible(groupInfo);

            if (!groupInfo.IsSectionOutSide)            
            {
                if (groupInfo.FollowSummaries)
                {
                    foreach (GroupSummary m_Summary in groupInfo.Summaries)
                    {
                        this.PrintingTable.GetCell(nRow, nCol).Text = m_Summary.ColumnHeading;

                    

                        nCol++;
                    }                   
                }

                if (visible)	//if set OnValuePerRow, don't set group title
                {
                    this.PrintingTable.GetCell(nRow, nCol).Text = groupInfo.ColumnHeading;

                    nCol++;
                }

                if (!groupInfo.FollowSummaries)               
                {                    
                    foreach (GroupSummary m_Summary in groupInfo.Summaries)
                    {
                        this.PrintingTable.GetCell(nRow, nCol).Text = m_Summary.ColumnHeading;                     

                        nCol++;
                    }                    
                }
            }

            foreach (GroupInfo subGroupInfo in groupInfo.SubGroupInfos)
            {
                SetHeaderValue(subGroupInfo, nRow, ref nCol);
            }
        }
      
        private void SetRowsValue(ref int i_Row, ref int i_Col, GroupInfo i_GroupInfo)
        {
            int m_OriginalStartCol = i_Col;

            int m_OriginalStartRow = i_Row;

            int m_Rows = i_GroupInfo.GetGroupedRows();

            if (i_GroupInfo.GroupResults != null)
            {//if not section group , sort group result
                if (!(i_GroupInfo is SectionGroupInfo)) i_GroupInfo.GroupResults.Sort(i_GroupInfo.Sorting, i_GroupInfo.SortingBy, i_GroupInfo.UserDefinedOrders);
            }

            bool visible = GroupInfo.IsVisible(i_GroupInfo);
       
            for (int m_row = 0; m_row < m_Rows; m_row++)
            {
                GroupResult m_GroupResult = i_GroupInfo.GroupResults[m_row];

                if (i_GroupInfo == this.RootGroupInfo && i_GroupInfo.IsSectionOutSide )
                {
                    //Set grouped value
                    #region set grouped value

                    StringBuilder sbGroup = new StringBuilder();

                    sbGroup.Append(m_GroupResult.GroupValue);

                    foreach (GroupSummary summary in m_GroupResult.Summaries)
                    {
                        string summaryValue = WebbTableCellHelper.FormatValue(null, summary);

                        sbGroup.Append(summaryValue);
                    }

                    if (m_GroupResult.ClickEvent == ClickEvents.PlayVideo)
                    {
                        WebbTableCellHelper.SetCellValueWithClickEvent(this.PrintingTable, i_Row, i_Col, sbGroup.ToString(), FormatTypes.String, m_GroupResult.RowIndicators);
                    }
                    else
                    {
                        WebbTableCellHelper.SetCellValue(this.PrintingTable, i_Row, i_Col, sbGroup.ToString(), FormatTypes.String);
                    }  

                    #endregion

                    #region Merge cell for section row
                  
                    this.PrintingTable.MergeCells(i_Row, i_Row, i_Col, this.PrintingTable.GetColumns() - 1);

                    this.SectionRows.Add(i_Row);

                    i_Row++;
                    
                    #endregion

                    #region Set sub rows
                    if (m_GroupResult.SubGroupInfos.Count > 0)
                    {
                        int m_StartRow = i_Row;

                        int maxRow = 0;
                   
                        foreach (GroupInfo groupInfo in m_GroupResult.SubGroupInfos)
                        {
                            this.SetRowsValue(ref i_Row, ref i_Col, groupInfo);

                            maxRow = Math.Max(maxRow, i_Row);

                            i_Row = m_StartRow;

                        }                           
                        
                        i_Row = maxRow-1;
                    }

                    i_Row++;

                    if (m_row < m_Rows - 1) i_Col = m_OriginalStartCol;
                    #endregion
                }
                else
                {
                    #region set summaryies ----Summaries before group
                    if (i_GroupInfo.FollowSummaries&& m_GroupResult.Summaries!=null)
                    {
                        foreach (GroupSummary m_Summary in m_GroupResult.Summaries)
                        {                               
                            //if group have click event , summaries also need.
                            if (m_GroupResult.ClickEvent == ClickEvents.PlayVideo)
                            {
                                WebbTableCellHelper.SetCellValueWithClickEvent(this.PrintingTable, i_Row, i_Col, m_Summary); //03-10-2008@Scott
                            }
                            else
                            {
                                WebbTableCellHelper.SetCellValue(this.PrintingTable, i_Row, i_Col, m_Summary);
                            }
                                       
                            i_Col++;
                        }                        
                    }
                    #endregion

                    //Set grouped value
                    #region set grouped value
                    if (visible)
                    {
                        #region New
                        string groupValue = string.Empty;

                        if (m_GroupResult.GroupValue != null)
                        {
                            groupValue = m_GroupResult.GroupValue.ToString();

                            if (i_GroupInfo is FieldGroupInfo)
                            {
                                string followWith = (i_GroupInfo as FieldGroupInfo).FollowsWith;

                                if (followWith != string.Empty && followWith.IndexOf("[VALUE]") >= 0)
                                {
                                    groupValue = followWith.Replace("[VALUE]", groupValue);
                                }
                            }
                        }
                        
                        if (m_GroupResult.ClickEvent == ClickEvents.PlayVideo)
                        {
                            WebbTableCellHelper.SetCellValueWithClickEvent(this.PrintingTable, i_Row, i_Col, groupValue, FormatTypes.String, m_GroupResult.RowIndicators);
                        }
                        else
                        {
                            WebbTableCellHelper.SetCellValue(this.PrintingTable, i_Row, i_Col, groupValue, FormatTypes.String);
                        }
                        
                        #endregion

                        i_Col++;
                    }                   

                    #endregion

                    #region set summaryies ----Summaries after group
                    if (!i_GroupInfo.FollowSummaries)
                    { 
                        if (m_GroupResult.Summaries != null)
                        {
                            foreach (GroupSummary m_Summary in m_GroupResult.Summaries)
                            {     
                                //if group have click event , summaries also need.
                                if (m_GroupResult.ClickEvent == ClickEvents.PlayVideo)
                                {
                                    WebbTableCellHelper.SetCellValueWithClickEvent(this.PrintingTable, i_Row, i_Col, m_Summary); //03-10-2008@Scott
                                }
                                else
                                {
                                    WebbTableCellHelper.SetCellValue(this.PrintingTable, i_Row, i_Col, m_Summary);
                                }

                                i_Col++;  
                                
                            }
                        }
                    }
                    #endregion

                    #region Set sub rows
                    if (m_GroupResult.SubGroupInfos.Count > 0)
                    {
                        int m_StartRow = i_Row;

                        int maxRow = 0;                        
                     
                        foreach (GroupInfo groupInfo in m_GroupResult.SubGroupInfos)
                        {
                            this.SetRowsValue(ref i_Row, ref i_Col, groupInfo);

                            maxRow = Math.Max(maxRow, i_Row);

                            i_Row = m_StartRow;
                        }
                       
                        i_Row = maxRow-1;
                    }

                    i_Row++;

                    if (m_row < m_Rows - 1) i_Col = m_OriginalStartCol;
                    #endregion
                }
            }
          

            #region Set total row
            if (i_GroupInfo.AddTotal && i_GroupInfo.TotalSummaries != null)
            {
                i_Col = m_OriginalStartCol;  

                if (i_GroupInfo.IsSectionOutSide)
                {
                    #region RootGroup Info & In IsSectionRow
                  
                    StringBuilder sbSummaryValue = new StringBuilder();

                    sbSummaryValue.Append(i_GroupInfo.TotalTitle);

                    for(int i=0;i<i_GroupInfo.Summaries.Count;i++)
                    {
                        if (i >= i_GroupInfo.TotalSummaries.Count) continue;

                        GroupSummary summary = i_GroupInfo.TotalSummaries[i];
                   
                        string summaryValue = WebbTableCellHelper.FormatValue(null, summary);

                        sbSummaryValue.Append(summaryValue);
                    }

                    if (i_GroupInfo.ClickEvent == ClickEvents.PlayVideo)
                    {
                        Int32Collection totalIndicators = (i_GroupInfo as FieldGroupInfo).GetTotalIndicators(i_GroupInfo);

                        WebbTableCellHelper.SetCellValueWithClickEvent(this.PrintingTable, i_Row, i_Col, sbSummaryValue.ToString(), FormatTypes.String, totalIndicators);
                    }
                    else
                    {
                        WebbTableCellHelper.SetCellValue(this.PrintingTable, i_Row, i_Col, sbSummaryValue.ToString(), FormatTypes.String);
                    }    
              

                    #endregion

                    this.PrintingTable.MergeCells(i_Row, i_Row, i_Col, this.PrintingTable.GetColumns() - 1);

                }
                else
                {
                    #region Not IsSectionOutSide

                    if (i_GroupInfo.FollowSummaries)
                    {
                        i_Col--;
                    }
                    else
                    {
                        WebbTableCellHelper.SetCellValue(this.PrintingTable, i_Row, i_Col, i_GroupInfo.TotalTitle, FormatTypes.String);
                    }

                    foreach (GroupSummary m_TotalSummary in i_GroupInfo.TotalSummaries)
                    {
                        i_Col++;

                        while (this._GroupColumns.Contains(i_Col))
                        {
                            i_Col++;
                        }

                        //05-08-2008@Scott
                        switch (m_TotalSummary.SummaryType)
                        {
                            case SummaryTypes.RelatedPercent:
                            case SummaryTypes.GroupPercent:
                                if (m_TotalSummary.Filter.Count == 0) continue;
                                break;
                            case SummaryTypes.FreqAndPercent:
                            case SummaryTypes.FreqAndRelatedPercent:
                                continue;
                            default:
                                break;
                        }

                        if (i_GroupInfo.ClickEvent == ClickEvents.PlayVideo)
                        {
                            WebbTableCellHelper.SetCellValueWithClickEvent(this.PrintingTable, i_Row, i_Col, m_TotalSummary);
                        }
                        else
                        {
                            WebbTableCellHelper.SetCellValue(this.PrintingTable, i_Row, i_Col, m_TotalSummary);
                        }
                    }
                    #endregion
                }

                this.TotalRows.Add(i_Row);

                i_Row++;
            }            
            #endregion
        }

        private void SetRowIndicators(int i_Rows)
        {
            int index = 1;

            for (int m_row = 0; m_row < i_Rows; m_row++)
            {
                if (this._HeaderRows.Contains(m_row) || this._TotalRows.Contains(m_row)) continue;

                this.PrintingTable.GetCell(m_row, 0).Text = Webb.Utility.FormatIndicator(index++);
            }
        }     

        #region Only For CCRM Data
        public override void GetALLUsedFields(ref ArrayList usedFields)
        {
            if (this._RootGroupInfo != null)
            {
                this.RootGroupInfo.GetAllUsedFields(ref usedFields);
            }

            this.Filter.GetAllUsedFields(ref usedFields);

            this.SectionFiltersWrapper.GetAllUsedFields(ref usedFields);
        }
        #endregion

     
    }
    #endregion

}