/***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:
 * Author:Simon.Zhang [EMail:Webb.simon.zhang@163.com]
 * Create Time:02/01/2009
 * Copyright:1986-2009@Webb Electronics all right reserved.
 * Purpose:
 */

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
using Webb.Data;

namespace Webb.Reports.ExControls.Views
{    

    #region public class HorizonGroupView
    [Serializable]
    public class HorizonGroupView : ExControlView, IMultiHeader
    {

        //Fields
        #region Fields

        protected Webb.Reports.ExControls.Data.GroupInfo _RootGroupInfo;

        protected Webb.Data.DBFilter _Fitler;

        protected bool _ShowRowIndicators;

        protected bool _HaveHeader=true;


        protected Int32Collection _ColumnsWidth;
        protected Int32Collection _RowsHight;     //2008-8-28 9:46:28@simon		

        protected bool _Total = false;

        protected int _HorizonTopCount = 0;

        protected bool _TotalOthers = false;

        protected string _TotalOthersName = "Others";

        protected BasicStyle _TotalStyle = new BasicStyle();

        protected Int32Collection _HeaderRows = new Int32Collection();

        protected HeadersData TableHeaders = null;

        protected TotalType _TotalPosition = TotalType.None;

        protected TotalType _TotalOthersPosition = TotalType.None;
      
        protected GroupSummary _SummaryForTotalGroup = new GroupSummary();

        protected GroupSummary _SummaryForOthers = new GroupSummary();
        #endregion

        //Properties
        #region Properties

        public GroupSummary SummaryForOthers       //Added this code at 2009-2-5 15:55:21@Simon
        {
            get {
                  if(_SummaryForOthers==null)_SummaryForOthers=new GroupSummary();
                  return this._SummaryForOthers ;
                }
            set { this._SummaryForOthers = value; }
        }     
        public GroupSummary SummaryForTotalGroup       //Added this code at 2009-2-5 15:55:21@Simon
        {
            get {
                if (_SummaryForTotalGroup == null) _SummaryForTotalGroup = new GroupSummary();
                return this._SummaryForTotalGroup;
                }
            set { this._SummaryForTotalGroup = value; }
        }

        public TotalType TotalOthersPosition       //Added this code at 2009-2-5 15:55:21@Simon
        {
            get { return this._TotalOthersPosition; }
            set { this._TotalOthersPosition = value; }
        }

        public TotalType TotalPosition       //Added this code at 2009-2-5 15:55:21@Simon
        {
            get { return this._TotalPosition; }
            set { this._TotalPosition = value; }
        }

        public HeadersData HeadersData       //Added this code at 2009-2-5 15:55:21@Simon
        {
            get { return this.TableHeaders; }
            set { this.TableHeaders = value; }
        }

        public Webb.Data.DBFilter Filter
        {
            get
            {
                if (_Fitler == null) _Fitler = new Webb.Data.DBFilter();
                return this._Fitler; }
            set { this._Fitler = value.Copy(); }
        }
        public Webb.Reports.ExControls.Data.GroupInfo RootGroupInfo      //Added this code at 2009-2-5 15:55:21@Simon
        {
            get { return this._RootGroupInfo; }
            set { this._RootGroupInfo = value; }
        }
        public bool ShowRowIndicators     //Added this code at 2009-2-5 15:55:21@Simon
        {
            get { return this._ShowRowIndicators; }
            set { this._ShowRowIndicators = value; }
        }
        public Int32Collection ColumnsWidth      //Added this code at 2009-2-5 15:55:21@Simon
        {
            get
            {
                if (_ColumnsWidth == null) _ColumnsWidth = new Int32Collection();
                return this._ColumnsWidth; }
            set { this._ColumnsWidth = value; }
        }
        public Int32Collection RowsHight    //Added this code at 2009-2-5 15:55:21@Simon
        {
            get
            {
                if (_RowsHight == null) _RowsHight = new Int32Collection();

                return this._RowsHight; }
            set { this._RowsHight = value; }
        }
        public bool HaveHeader     //Added this code at 2009-2-5 15:55:21@Simon
        {
            get { return this._HaveHeader; }
            set { this._HaveHeader = value; }
        }
        public bool Total     //Added this code at 2009-2-5 15:55:21@Simon
        {
            get { return this._Total; }
            set { this._Total = value; }
        }
        public int HorizonTopCount    //Added this code at 2009-2-5 15:55:21@Simon
        {
            get { return this._HorizonTopCount; }
            set { this._HorizonTopCount = value; }
        }
        public bool TotalOthers     //Added this code at 2009-2-5 15:55:21@Simon
        {
            get { return this._TotalOthers; }
            set { this._TotalOthers = value; }
        }
        public string TotalOthersName    //Added this code at 2009-2-5 15:55:21@Simon
        {
            get { return this._TotalOthersName; }
            set { this._TotalOthersName = value; }
        }
        public BasicStyle TotalStyle
        {
            get {
                if (_TotalStyle == null) _TotalStyle = new BasicStyle();
                return this._TotalStyle; }
            set { this._TotalStyle = value; }
        }
        public Int32Collection HeaderRows    //Added this code at 2009-2-5 15:55:21@Simon
        {
            get
            {
                if (_HeaderRows == null) _HeaderRows = new Int32Collection();
                return this._HeaderRows;
            }
            set { this._HeaderRows = value; }
        }



        #endregion

        //ctor

        public HorizonGroupView(ExControl i_Control)
            : base(i_Control)
        {
        }


        #region Modified Area
        private bool RepeatHeader()
        {
            bool repeat = !this.ExControl.Report.Template.OneValuePerPage && !this.ExControl.Report.Template.ControlHeaderOnce;

            if ((this.TableHeaders == null || this.TableHeaders.RowCount <= 0) && !this.HaveHeader)
            {
                repeat = false;
            }

            //    		repeat=false;  //2009-4-3 9:32:40@Simon Add this Code

            return repeat;
        }
        #endregion        //End Modify at 2008-10-21 10:10:33@Simon

        public override int CreateArea(string areaName, IBrickGraphics graph)
        {
            if (this.PrintingTable != null)
            {
                #region Modified Area
                this.PrintingTable.RepeatedHeader = this.RepeatHeader();
                if (this.TableHeaders != null)
                {
                    if (this._HaveHeader)
                    {
                        this.PrintingTable.HeaderCount = this.TableHeaders.RowCount + 1;
                    }
                    else
                    {
                        this.PrintingTable.HeaderCount = this.TableHeaders.RowCount;
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
            else
            {
                return 0;
            }

            return base.CreateArea(areaName, graph);
        }

        //Override members
        #region override members
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
            if (RootGroupInfo == null) return;

            //If have no data source ,clear group struct
            if (i_Table == null)
            {
                this.RootGroupInfo.ClearGroupResult(this._RootGroupInfo);

                return;
            }

            //Filter rows
            Webb.Collections.Int32Collection m_OuterRows = new Int32Collection();

            if (this.ExControl != null && this.ExControl.Report != null)
            {
                m_OuterRows = this.ExControl.Report.Filter.GetFilteredRows(i_Table);  //2009-5-25 11:02:57@Simon Add this Code
            }

            m_OuterRows = this.Filter.GetFilteredRows(i_Table, m_OuterRows);	//06-04-2008@Scott

            //GroupInfo SortGroupInfo = this.RootGroupInfo.Copy();

            //SortGroupInfo.Summaries=new GroupSummaryCollection();

            //SortGroupInfo.SubGroupInfos=new GroupInfoCollection();

            //SortGroupInfo.CalculateGroupResult(i_Table, m_OuterRows, m_OuterRows, SortGroupInfo);

            //SortGroupInfo.GroupResults.Sort(SortGroupInfo.Sorting, SortGroupInfo.SortingBy,SortGroupInfo.UserDefinedOrders);

            if (this.RootGroupInfo.Summaries == null)
            {
                this.RootGroupInfo.Summaries = new GroupSummaryCollection();
            }          

            Int32Collection m_Rows = this.OneValueScFilter.Filter.GetFilteredRows(i_Table, m_OuterRows);

            m_Rows = this.RepeatFilter.Filter.GetFilteredRows(i_Table, m_Rows);

            RootGroupInfo.CalculateGroupResult(i_Table, m_Rows, m_OuterRows, m_Rows, RootGroupInfo);

            this.AddTotalAndOthers(i_Table, m_OuterRows, m_Rows);
        }

        public void AddTotalAndOthers( System.Data.DataTable i_Table,Int32Collection outerRows,Int32Collection innerRows)
        {
            if (this.RootGroupInfo == null) return;

            GroupResultCollection m_newResults = new GroupResultCollection();

            GroupResult m_TotalGroupResult = new GroupResult();

            m_TotalGroupResult.Summaries = new GroupSummaryCollection();

            GroupResult m_OtherGroupResult = new GroupResult();

            m_OtherGroupResult.Summaries = new GroupSummaryCollection();

            if (this.TotalPosition!=TotalType.None)
            {
                #region Caclculate Result for Total

                m_TotalGroupResult.GroupValue = this.RootGroupInfo.TotalTitle;

                m_TotalGroupResult.RowIndicators = new Int32Collection();

                m_TotalGroupResult.ParentGroupInfo = this.RootGroupInfo;  //Add at 2009-2-19 14:23:47@Simon

                innerRows.CopyTo(m_TotalGroupResult.RowIndicators);

                if (this.RootGroupInfo.Summaries.Count > 0)
                {
                    GroupSummary summaryTotal = this.SummaryForTotalGroup.Copy();               

                    m_TotalGroupResult.Summaries.Add(summaryTotal);

                    m_TotalGroupResult.CalculateSummaryResult(i_Table, innerRows, innerRows);

                }

                m_TotalGroupResult.SubGroupInfos = RootGroupInfo.SubGroupInfos.Copy();

                foreach (GroupInfo subGroupInfo in m_TotalGroupResult.SubGroupInfos)
                {
                    subGroupInfo.CalculateGroupResult(i_Table, outerRows, outerRows, innerRows, subGroupInfo);
                }
                #endregion
            }
            if (this.TotalPosition == TotalType.AllBefore)
            {
                m_newResults.Add(m_TotalGroupResult);
            }
            if (this.TotalOthersPosition == TotalType.AllBefore)
            {
                m_newResults.Add(m_OtherGroupResult);
            }

            Int32Collection m_OtherRowsTotal = new Int32Collection();

            for (int i = 0; i < this.RootGroupInfo.GroupResults.Count; i++)
            {
                if (i >= this.HorizonTopCount && HorizonTopCount > 0)
                {
                    m_OtherRowsTotal = m_OtherRowsTotal.Combine(this.RootGroupInfo.GroupResults[i].RowIndicators, m_OtherRowsTotal);
                }
                else
                {
                    m_newResults.Add(this.RootGroupInfo.GroupResults[i]);
                }
            }

            if (this.TotalOthersPosition != TotalType.None)
            {
                #region calculate Other results

                m_OtherGroupResult.GroupValue = this.TotalOthersName;

                m_OtherGroupResult.RowIndicators = new Int32Collection();

                m_OtherGroupResult.ParentGroupInfo = this.RootGroupInfo;  //Add at 2009-2-19 14:23:47@Simon

                m_OtherRowsTotal.CopyTo(m_OtherGroupResult.RowIndicators);

                if (this.RootGroupInfo.Summaries.Count > 0)
                {
                    GroupSummary summaryTotal = this.SummaryForOthers.Copy();
                   
                    m_OtherGroupResult.Summaries.Add(summaryTotal);

                    m_OtherGroupResult.CalculateSummaryResult(i_Table, outerRows, m_OtherRowsTotal);

                }

                m_OtherGroupResult.SubGroupInfos = this.RootGroupInfo.SubGroupInfos.Copy();

                foreach (GroupInfo subGroupInfo in m_OtherGroupResult.SubGroupInfos)
                {
                    subGroupInfo.CalculateGroupResult(i_Table, m_OtherRowsTotal, outerRows, m_OtherRowsTotal, subGroupInfo);
                }
                #endregion
            }

            if (this.TotalOthersPosition == TotalType.AllAfter)
            {
                m_newResults.Add(m_OtherGroupResult);
            }
            if (this.TotalPosition == TotalType.AllAfter)
            {
                m_newResults.Add(m_TotalGroupResult);
            }         

            this.RootGroupInfo.ResetGroupResults(m_newResults);

        }


        #endregion


        #region Calculate grouped rows and columns
        /// <summary>
        /// Calculate grouped columns
        /// </summary>
        /// <returns></returns>
        public int GetGroupedColumns()
        {
            if (this.RootGroupInfo == null || this.RootGroupInfo.GroupResults == null) return 1;

            int nCols = 0;

            if (this._ShowRowIndicators)
            {
                nCols += 1;
            }

            this.GetGroupedColumns(this.RootGroupInfo, ref nCols);

            return nCols;
        }

        private void GetGroupedColumns(GroupInfo groupInfo, ref int nCols)
        {
            int cols = this.RootGroupInfo.GroupResults.Count;

            if (groupInfo.SubGroupInfos.Count > 0)
            {
                cols *= this.RootGroupInfo.GroupResults[0].SubGroupInfos[0].GroupResults.Count;
            }

            nCols += cols;

        }

        /// <summary>
        /// Calculate grouped rows
        /// </summary>
        /// <returns></returns>
        public int GetGroupedRows()
        {
            int m_value = 0;

            if (this.RootGroupInfo.SubGroupInfos.Count > 0)
            {
                m_value += this.RootGroupInfo.SubGroupInfos[0].Summaries.Count;
            }

            m_value += this.GetHeaderRowsCount();	//add header rows

            if (this.RootGroupInfo.Summaries.Count>0) m_value++;	//add total rows

            return m_value;
        }

        private int GetHeaderRowsCount()
        {
            int nRet = 0;

            if (this.TableHeaders != null && this.TableHeaders.RowCount > 0 && this.TableHeaders.ColCount > 0)   //2008-8-29 9:12:31@simon
            {
                nRet += this.TableHeaders.RowCount;  //2008-8-29 9:12:37@simon				

            }

            if (this.HaveHeader)
            {
                if (this._RootGroupInfo == null) return nRet;

                if (this._RootGroupInfo.GroupResults == null) return nRet;

                if (this._RootGroupInfo.GroupResults.Count == 0) return nRet;

                nRet += 1;

                if (this.RootGroupInfo.SubGroupInfos.Count > 0)
                {
                    nRet += 1;
                }
            }


            return nRet;
        }

        #endregion

        #region CreatePrintingTable
        /// <summary>
        /// Create printing table
        /// </summary>
        /// <returns></returns>
        public override bool CreatePrintingTable()
        {
            if (this._RootGroupInfo == null|| _RootGroupInfo.GroupResults ==null || _RootGroupInfo.GroupResults.Count==0)
            {
                this.PrintingTable = null;

                return false;
            }

            int m_Rows = this.GetGroupedRows();

            int m_Column = this.GetGroupedColumns();

            if (m_Rows <= 0 || m_Column <= 0)
            {
                this.PrintingTable = null;
                return false;
            }

            this.HeaderRows.Clear();

            System.Diagnostics.Debug.WriteLine(string.Format("Begin Create print table:{0}X{1}", m_Rows, m_Column));

            this.PrintingTable = new WebbTable(m_Rows, m_Column);

            this.SetTableValue();

            this.ApplyColumnWidthStyle(m_Column);             

            this.ApplyRowHeightStyle(m_Rows);            

            System.Diagnostics.Debug.WriteLine("Create print table completely");

            return true;
        }
        #endregion

        #region Set ColumnWidth/RowHeights
        private void ApplyRowHeightStyle(int m_Rows)
        {
            if (this.RowsHight.Count <= 0) return;

            int count = Math.Min(this.RowsHight.Count, m_Rows);

            for (int m_row = 0; m_row < count; m_row++)
            {
                IWebbTableCell cell = this.PrintingTable.GetCell(m_row, 0);

                if(cell==null)continue;

                cell.CellStyle.Height = this.RowsHight[m_row];
            }
        }

        private void ApplyColumnWidthStyle(int m_Cols)
        {
            if (this.ColumnsWidth.Count <= 0) return;

            int count = Math.Min(this.ColumnsWidth.Count, m_Cols);

            for (int m_col = 0; m_col < count; m_col++)
            {
                IWebbTableCell cell = this.PrintingTable.GetCell(0, m_col);

                if (cell == null) continue;

                cell.CellStyle.Width = ColumnsWidth[m_col];
            }
        }
        #endregion

        #region sub function for Create PrintingTable :  SetHeaderRows &  SetTableValue & GetMergedSpan & SetRowIndicators  &SetHeaderValue
        private void SetTableValue()
        {
            int m_Rows = 0, m_Col = 0;

            if (this.ShowRowIndicators) m_Col = 1;	//add row indicator columns		

            int nHeaderStart = m_Rows, nHeaderCount = 0;

            this.SetHeaderValue(ref m_Rows);	//set header value

            nHeaderCount = m_Rows - nHeaderStart;

            this.SetHeaderRows(nHeaderStart, nHeaderCount);

            #region Set style

            int setRow = m_Rows;

            if (this.RootGroupInfo.Summaries.Count>0)
            {
                this.PrintingTable.SetRowStyle(setRow, this.RootGroupInfo.Summaries[0].Style, this.ShowRowIndicators);

                setRow++;

            }
            if (RootGroupInfo.SubGroupInfos.Count > 0)
            {
                GroupInfo subGroupInfo = RootGroupInfo.SubGroupInfos[0];

                foreach (GroupSummary m_Summary in subGroupInfo.Summaries)
                {
                    this.PrintingTable.SetRowStyle(setRow, m_Summary.Style, this.ShowRowIndicators);

                    setRow++;
                }
            }
            #endregion

            #region Set Rows Value

            int mergedspan = this.GetMergedSpan();

            for (int i = 0; i < this.RootGroupInfo.GroupResults.Count; i++)
            {
                int tempRow = m_Rows;

                GroupResult m_Result = RootGroupInfo.GroupResults[i];

                IWebbTableCell cell = null;

                if (m_Result.Summaries.Count>0)
                {
                    GroupSummary m_Summary = m_Result.Summaries[0];                       

                    if (RootGroupInfo.ClickEvent == ClickEvents.PlayVideo)
                    {
                        WebbTableCellHelper.SetCellValueWithClickEvent(this.PrintingTable, tempRow, m_Col, m_Summary);                         
                    }
                    else
                    {
                        WebbTableCellHelper.SetCellValue(this.PrintingTable, tempRow, m_Col, m_Summary); 
                    }

                    this.PrintingTable.MergeCells(tempRow, tempRow, m_Col , m_Col+ mergedspan - 1);

                    cell = this.PrintingTable.GetCell(tempRow, m_Col);

                    if (cell == null) continue;

                    if (tempRow < this.PrintingTable.GetRows()-1 && cell != null)
                    {
                        cell.CellStyle.Sides &= ~DevExpress.XtraPrinting.BorderSide.Bottom;
                    }

                    tempRow++;
                }               

                #region Sub GroupInfo

                 if (this.RootGroupInfo.SubGroupInfos.Count > 0)
                {
                    GroupInfo subGroupInfo = RootGroupInfo.GroupResults[i].SubGroupInfos[0];

                    for (int j = 0; j < subGroupInfo.GroupResults.Count; j++, m_Col++)
                    {
                        GroupResult m_subResult = subGroupInfo.GroupResults[j];

                        int nextRow = tempRow;

                        for(int k=0;k<m_subResult.Summaries.Count;k++)
                        {
                            GroupSummary m_Summary = m_subResult.Summaries[k];
                                                  
                            WebbTableCellHelper.SetCellValue(this.PrintingTable, nextRow, m_Col, m_Summary);                            
                            
                            cell = this.PrintingTable.GetCell(nextRow, m_Col);

                            if (cell == null) continue;

                            cell.CellStyle.Sides = DevExpress.XtraPrinting.BorderSide.None;

                            if (cell != null)
                            {
                                if (nextRow == 0)
                                {
                                    cell.CellStyle.Sides |= DevExpress.XtraPrinting.BorderSide.Top;
                                }
                                if (k == m_subResult.Summaries.Count - 1)
                                {
                                    cell.CellStyle.Sides |= DevExpress.XtraPrinting.BorderSide.Bottom;
                                }

                                if (j == 0)
                                {
                                    cell.CellStyle.Sides |= DevExpress.XtraPrinting.BorderSide.Left;
                                }
                                if (j == subGroupInfo.GroupResults.Count - 1)
                                {
                                    cell.CellStyle.Sides |= DevExpress.XtraPrinting.BorderSide.Right;
                                }
                            }
                            nextRow++;
                        }

                    }
                }
                else
                {
                    m_Col++;
                }

                #endregion
            }

            #endregion                      

            if (this.ShowRowIndicators)
            {
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

        private int GetMergedSpan()
        {
            if (this.RootGroupInfo.GroupResults == null) return 0;

            if (this.RootGroupInfo.SubGroupInfos.Count <= 0) return 1;

            GroupResult m_Result = RootGroupInfo.GroupResults[0];

            return m_Result.SubGroupInfos[0].GroupResults.Count;
        }

        private void SetHeaderValue(ref int nRow)
        {
            int nCol = 0;

            if (this.TableHeaders != null && this.TableHeaders.RowCount > 0 && this.TableHeaders.ColCount > 0)    //2008-8-29 9:12:52@simon
            {
                this.TableHeaders.SetHeaders(PrintingTable, ref nRow, this);
            }

            int totalRows = this.PrintingTable.GetRows();
            int totalCols = this.PrintingTable.GetColumns();

            if (this._ShowRowIndicators) nCol++;

            if (this.HaveHeader)   //Modified at 2008-10-21 8:49:30@Simon
            {
                if (RootGroupInfo.ColorNeedChange)
                {
                    this.PrintingTable.SetRowStyle(nRow, RootGroupInfo.Style);
                }
                if (RootGroupInfo.SubGroupInfos.Count > 0)
                {
                    if (RootGroupInfo.SubGroupInfos[0].ColorNeedChange)
                    {
                        this.PrintingTable.SetRowStyle(nRow + 1, RootGroupInfo.SubGroupInfos[0].Style);
                    }
                }

                #region Have Header

                int mergedspan = this.GetMergedSpan();

                for (int i = 0; i < this.RootGroupInfo.GroupResults.Count; i++)
                {
                    GroupResult m_Result = RootGroupInfo.GroupResults[i];

                    if (RootGroupInfo.ClickEvent == ClickEvents.PlayVideo)
                    {
                        WebbTableCellHelper.SetCellValueWithClickEvent(this.PrintingTable, nRow, nCol, m_Result.GroupValue, FormatTypes.String, m_Result.RowIndicators);
                    }
                    else
                    {
                        WebbTableCellHelper.SetCellValue(this.PrintingTable, nRow, nCol, m_Result.GroupValue, FormatTypes.String);
                    }

                    this.PrintingTable.MergeCells(nRow, nRow, nCol, nCol + mergedspan - 1);

                    IWebbTableCell cell = this.PrintingTable.GetCell(nRow, nCol);

                    if (nRow < totalRows - 1 && cell != null)
                    {
                        cell.CellStyle.Sides &= ~DevExpress.XtraPrinting.BorderSide.Bottom;
                    }

                    #region Sub GroupInfo
                    if (this.RootGroupInfo.SubGroupInfos.Count > 0)
                    {
                        GroupInfo subGroupInfo = RootGroupInfo.GroupResults[i].SubGroupInfos[0];

                        for (int j = 0; j < subGroupInfo.GroupResults.Count; j++, nCol++)
                        {
                            GroupResult m_subResult = subGroupInfo.GroupResults[j];

                            if (subGroupInfo.ClickEvent == ClickEvents.PlayVideo)
                            {
                                WebbTableCellHelper.SetCellValueWithClickEvent(this.PrintingTable, nRow + 1, nCol, m_subResult.GroupValue, FormatTypes.String, m_subResult.RowIndicators);
                            }
                            else
                            {
                                WebbTableCellHelper.SetCellValue(this.PrintingTable, nRow + 1, nCol, m_subResult.GroupValue, FormatTypes.String);
                            }

                            cell = this.PrintingTable.GetCell(nRow + 1, nCol);

                            cell.CellStyle.Sides = DevExpress.XtraPrinting.BorderSide.None;

                            if (cell != null)
                            {
                                cell.CellStyle.Sides |= DevExpress.XtraPrinting.BorderSide.Bottom;

                                if (j == 0)
                                {
                                    cell.CellStyle.Sides |= DevExpress.XtraPrinting.BorderSide.Left;
                                }
                                if (j == subGroupInfo.GroupResults.Count - 1)
                                {
                                    cell.CellStyle.Sides |= DevExpress.XtraPrinting.BorderSide.Right;
                                }
                            }
                        }
                    }
                    else
                    {
                        nCol++;
                    }
                    #endregion
                }

                nRow++;

                if (this.RootGroupInfo.SubGroupInfos.Count > 0) nRow++;

                #endregion
            }
        }

        private void SetRowIndicators(int i_Rows)
        {
            int index = 1;

            for (int m_row = 0; m_row < i_Rows; m_row++)
            {
                if (this._HeaderRows.Contains(m_row)) continue;

                this.PrintingTable.GetCell(m_row, 0).Text = Webb.Utility.FormatIndicator(index++);
            }
        }
        #endregion

        #region Modify codes at 2008-12-8 15:36:23@Simon
        public ArrayList GetPrnHeader(out ArrayList formats)
        {
            ArrayList prnHeaders = new ArrayList();

            formats = new ArrayList();

            #region Have Header

            int mergedspan = this.GetMergedSpan();

            for (int i = 0; i < this.RootGroupInfo.GroupResults.Count; i++)
            {
                GroupResult m_Result = RootGroupInfo.GroupResults[i];

                if (this.RootGroupInfo.SubGroupInfos.Count > 0)
                {
                    GroupInfo subGroupInfo = RootGroupInfo.GroupResults[i].SubGroupInfos[0];

                    for (int j = 0; j < subGroupInfo.GroupResults.Count; j++)
                    {
                        GroupResult m_subResult = subGroupInfo.GroupResults[j];

                        prnHeaders.Add(m_subResult.GroupValue.ToString());

                        formats.Add(0);

                    }
                }
            }

            #endregion
            return prnHeaders;
        }
        #endregion        //End Modify

     

        public override void GetALLUsedFields(ref ArrayList usedFields)
        {
            if (this._RootGroupInfo != null)
            {
                this.RootGroupInfo.GetAllUsedFields(ref usedFields);
            }

            this.Filter.GetAllUsedFields(ref usedFields);

            this.SectionFiltersWrapper.GetAllUsedFields(ref usedFields);
        }

        #region Serialization By Simon's Macro 2011-5-31 13:32:49
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

			info.AddValue("_RootGroupInfo",_RootGroupInfo,typeof(Webb.Reports.ExControls.Data.GroupInfo));
			info.AddValue("_Fitler",_Fitler,typeof(Webb.Data.DBFilter));
			info.AddValue("_ShowRowIndicators",_ShowRowIndicators);
			info.AddValue("_HaveHeader",_HaveHeader);
			info.AddValue("_ColumnsWidth",_ColumnsWidth,typeof(Webb.Collections.Int32Collection));
			info.AddValue("_RowsHight",_RowsHight,typeof(Webb.Collections.Int32Collection));
			info.AddValue("_Total",_Total);
			info.AddValue("_HorizonTopCount",_HorizonTopCount);
			info.AddValue("_TotalOthers",_TotalOthers);
			info.AddValue("_TotalOthersName",_TotalOthersName);
			info.AddValue("_TotalStyle",_TotalStyle,typeof(Webb.Reports.ExControls.BasicStyle));
			info.AddValue("_HeaderRows",_HeaderRows,typeof(Webb.Collections.Int32Collection));
			info.AddValue("TableHeaders",TableHeaders,typeof(Webb.Reports.ExControls.Data.HeadersData));
			info.AddValue("_TotalPosition",_TotalPosition,typeof(Webb.Data.TotalType));
			info.AddValue("_TotalOthersPosition",_TotalOthersPosition,typeof(Webb.Data.TotalType));			
			info.AddValue("_SummaryForTotalGroup",_SummaryForTotalGroup,typeof(Webb.Reports.ExControls.Data.GroupSummary));
			info.AddValue("_SummaryForOthers",_SummaryForOthers,typeof(Webb.Reports.ExControls.Data.GroupSummary));

        }

        public HorizonGroupView(SerializationInfo info, StreamingContext context):base(info,context)
        {
			try
			{
				_RootGroupInfo=(Webb.Reports.ExControls.Data.GroupInfo)info.GetValue("_RootGroupInfo",typeof(Webb.Reports.ExControls.Data.GroupInfo));
			}
			catch
			{
			}
			try
			{
				_Fitler=(Webb.Data.DBFilter)info.GetValue("_Fitler",typeof(Webb.Data.DBFilter));
			}
			catch
			{
			}
			try
			{
				_ShowRowIndicators=info.GetBoolean("_ShowRowIndicators");
			}
			catch
			{
				_ShowRowIndicators=false;
			}
			try
			{
				_HaveHeader=info.GetBoolean("_HaveHeader");
			}
			catch
			{
				_HaveHeader=true;
			}
			try
			{
				_ColumnsWidth=(Webb.Collections.Int32Collection)info.GetValue("_ColumnsWidth",typeof(Webb.Collections.Int32Collection));
			}
			catch
			{
			}
			try
			{
				_RowsHight=(Webb.Collections.Int32Collection)info.GetValue("_RowsHight",typeof(Webb.Collections.Int32Collection));
			}
			catch
			{
			}
			try
			{
				_Total=info.GetBoolean("_Total");
			}
			catch
			{
				_Total=true;
			}
			try
			{
				_HorizonTopCount=info.GetInt32("_HorizonTopCount");
			}
			catch
			{
				_HorizonTopCount=0;
			}
			try
			{
				_TotalOthers=info.GetBoolean("_TotalOthers");
			}
			catch
			{
				_TotalOthers=false;
			}
			try
			{
				_TotalOthersName=info.GetString("_TotalOthersName");
			}
			catch
			{
				_TotalOthersName="Others";
			}
			try
			{
				_TotalStyle=(Webb.Reports.ExControls.BasicStyle)info.GetValue("_TotalStyle",typeof(Webb.Reports.ExControls.BasicStyle));
			}
			catch
			{
				_TotalStyle=new BasicStyle();
			}
			try
			{
				_HeaderRows=(Webb.Collections.Int32Collection)info.GetValue("_HeaderRows",typeof(Webb.Collections.Int32Collection));
			}
			catch
			{
				_HeaderRows=new Int32Collection();
			}
			try
			{
				TableHeaders=(Webb.Reports.ExControls.Data.HeadersData)info.GetValue("TableHeaders",typeof(Webb.Reports.ExControls.Data.HeadersData));
			}
			catch
			{
				TableHeaders=null;
			}
			try
			{
				_TotalPosition=(Webb.Data.TotalType)info.GetValue("_TotalPosition",typeof(Webb.Data.TotalType));
			}
			catch
			{
                if (_RootGroupInfo != null && _RootGroupInfo.AddTotal)
                {
                    _TotalPosition = TotalType.AllBefore;

                    _RootGroupInfo.AddTotal = false;
                }
                else
                {
                    _TotalPosition = TotalType.None;
                }
                if (RootGroupInfo != null)
                {
                    if (RootGroupInfo.SubGroupInfos.Count > 0 && RootGroupInfo.Summaries!=null)
                    {
                        RootGroupInfo.SubGroupInfos[0].Summaries = RootGroupInfo.Summaries.CopyStructure();
                    }

                    RootGroupInfo.Summaries = new GroupSummaryCollection();

                    if (this.Total)
                    {
                        GroupSummary groupSummary = new GroupSummary();

                        groupSummary.ColorNeedChange = true;

                        groupSummary.Style.SetStyle(this.TotalStyle);

                        groupSummary.ShowZeros = true;

                        RootGroupInfo.Summaries.Add(groupSummary);
                    }
                }
			}
			try
			{
				_TotalOthersPosition=(Webb.Data.TotalType)info.GetValue("_TotalOthersPosition",typeof(Webb.Data.TotalType));
			}
			catch
			{
                if (this._TotalOthers)
                {
                    _TotalOthersPosition = TotalType.AllAfter;
                }
                else
                {
                    _TotalOthersPosition = TotalType.None;
                }
			}			
			try
			{
				_SummaryForTotalGroup=(Webb.Reports.ExControls.Data.GroupSummary)info.GetValue("_SummaryForTotalGroup",typeof(Webb.Reports.ExControls.Data.GroupSummary));
			}
			catch
			{
				_SummaryForTotalGroup=new GroupSummary();
			}
			try
			{
				_SummaryForOthers=(Webb.Reports.ExControls.Data.GroupSummary)info.GetValue("_SummaryForOthers",typeof(Webb.Reports.ExControls.Data.GroupSummary));
			}
			catch
			{
				_SummaryForOthers=new GroupSummary();
			}
        }
		#endregion       

    }

	#endregion
}