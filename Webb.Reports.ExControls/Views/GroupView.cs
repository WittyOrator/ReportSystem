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
   #region Data Structure
        #region OurBordersSetting
            [Serializable]
            public class OurBordersSetting : ISerializable
            {
                #region Auto Constructor By Macro 2011-2-14 8:59:41
                public OurBordersSetting()
                {
                    _ChangeOutBorders = false;
                    _Sides = DevExpress.XtraPrinting.BorderSide.All;
                }

                public OurBordersSetting(DevExpress.XtraPrinting.BorderSide p_Sides, bool p_ChangeOutBorders)
                {
                    _ChangeOutBorders = p_ChangeOutBorders;
                    _Sides = p_Sides;
                }
                #endregion


                protected bool _ChangeOutBorders = false;
                protected DevExpress.XtraPrinting.BorderSide _Sides = DevExpress.XtraPrinting.BorderSide.All;

                #region Copy Function By Macro 2011-2-14 8:59:46
                public OurBordersSetting Copy()
                {
                    OurBordersSetting thiscopy = new OurBordersSetting();
                    thiscopy._ChangeOutBorders = this._ChangeOutBorders;
                    thiscopy._Sides = this._Sides;
                    return thiscopy;
                }
                #endregion

                #region Serialization By Simon's Macro 2011-2-14 8:59:52
                public void GetObjectData(SerializationInfo info, StreamingContext context)
                {
                    info.AddValue("_ChangeOutBorders", _ChangeOutBorders);
                    info.AddValue("_Sides", _Sides, typeof(DevExpress.XtraPrinting.BorderSide));

                }

                public OurBordersSetting(SerializationInfo info, StreamingContext context)
                {
                    try
                    {
                        _ChangeOutBorders = info.GetBoolean("_ChangeOutBorders");
                    }
                    catch
                    {
                        _ChangeOutBorders = false;
                    }
                    try
                    {
                        _Sides = (DevExpress.XtraPrinting.BorderSide)info.GetValue("_Sides", typeof(DevExpress.XtraPrinting.BorderSide));
                    }
                    catch
                    {
                        _Sides = DevExpress.XtraPrinting.BorderSide.All;
                    }
                }
                #endregion

                public bool ChangeOutBorders
                {
                    get
                    {
                        return _ChangeOutBorders;
                    }
                    set
                    {
                        _ChangeOutBorders = value;
                    }
                }

                [EditorAttribute(typeof(Webb.Reports.ExControls.Editors.SidesEditor), typeof(System.Drawing.Design.UITypeEditor))]	//06-11-2008@Scott
                public DevExpress.XtraPrinting.BorderSide Sides
                {
                    get
                    {
                        return _Sides;
                    }
                    set
                    {
                        _Sides = value;
                    }
                }


                public void ChangeTableOutBorders(WebbTable table)
                {
                    if (table == null || !this._ChangeOutBorders) return;

                    int totalRows = table.GetRows();

                    int totalColumns = table.GetColumns();

                    if (totalRows <= 0 || totalColumns <= 0) return;

                    int lastRow = totalRows - 1, lastcol = totalColumns - 1;

                    IWebbTableCell cell = null;

                    #region Set top border At first row
                    if ((int)(_Sides & DevExpress.XtraPrinting.BorderSide.Top) > 0)
                    {
                        for (int m_col = 0; m_col < totalColumns; m_col++)
                        {
                            cell = table.GetCell(0, m_col);

                            if (cell == null) continue;

                            cell.CellStyle.Sides |= DevExpress.XtraPrinting.BorderSide.Top;
                        }
                    }
                    else
                    {
                        for (int m_col = 0; m_col < totalColumns; m_col++)
                        {
                            cell = table.GetCell(0, m_col);

                            if (cell == null) continue;

                            cell.CellStyle.Sides &= ~DevExpress.XtraPrinting.BorderSide.Top;
                        }
                    }
                    #endregion

                    #region Set bottom border at last row
                    if ((int)(_Sides & DevExpress.XtraPrinting.BorderSide.Bottom) > 0)
                    {
                        for (int m_col = 0; m_col < totalColumns; m_col++)
                        {
                            cell = table.GetCell(lastRow, m_col);

                            if (cell == null) continue;

                            cell.CellStyle.Sides |= DevExpress.XtraPrinting.BorderSide.Bottom;
                        }
                    }
                    else
                    {
                        for (int m_col = 0; m_col < totalColumns; m_col++)
                        {
                            cell = table.GetCell(lastRow, m_col);

                            if (cell == null) continue;

                            cell.CellStyle.Sides &= ~DevExpress.XtraPrinting.BorderSide.Bottom;
                        }
                    }
                    #endregion

                    #region Set left border At first column
                    if ((int)(_Sides & DevExpress.XtraPrinting.BorderSide.Left) > 0)
                    {
                        for (int m_row = 0; m_row < totalRows; m_row++)
                        {
                            cell = table.GetCell(m_row, 0);

                            if (cell == null) continue;

                            cell.CellStyle.Sides |= DevExpress.XtraPrinting.BorderSide.Left;
                        }
                    }
                    else
                    {
                        for (int m_row = 0; m_row < totalRows; m_row++)
                        {
                            cell = table.GetCell(m_row, 0);

                            if (cell == null) continue;

                            cell.CellStyle.Sides &= ~DevExpress.XtraPrinting.BorderSide.Left;
                        }
                    }
                    #endregion

                    #region Set right border At last column
                    if ((int)(_Sides & DevExpress.XtraPrinting.BorderSide.Right) > 0)
                    {
                        for (int m_row = 0; m_row < totalRows; m_row++)
                        {
                            cell = table.GetCell(m_row, lastcol);

                            if (cell == null) continue;

                            cell.CellStyle.Sides |= DevExpress.XtraPrinting.BorderSide.Right;
                        }
                    }
                    else
                    {
                        for (int m_row = 0; m_row < totalRows; m_row++)
                        {
                            cell = table.GetCell(m_row, lastcol);

                            if (cell == null) continue;

                            cell.CellStyle.Sides &= ~DevExpress.XtraPrinting.BorderSide.Right;
                        }
                    }
                    #endregion

                }


            }
        #endregion

        #region GroupAdvancedSetting
            [Serializable]
            public class GroupAdvancedSetting : ISerializable
            {
                #region Field &properties
                protected DevExpress.XtraPrinting.BorderSide _TotalSides = DevExpress.XtraPrinting.BorderSide.None;
                protected Int32Collection _HiddenColumns = new Int32Collection();
                protected bool _SectionSides = false;
                protected int _ChartRowHeight = 100;
                protected GroupSummaryCollection _SummariesForSections = new GroupSummaryCollection();

                protected bool _DisregardAllPlaysBlank = false;
                protected bool _Total;
                protected string _TotalTitle;									//05-09-2008@Scott
                protected Int32Collection _TotalColumns = new Int32Collection();						//05-09-2008@Scott

                [Browsable(false)]
                [Category("Disregard Blank")]
                public bool DisregardAllPlaysBlank
                {
                    get { return this._DisregardAllPlaysBlank; }
                    set { this._DisregardAllPlaysBlank = value; }
                }

                [Category("Total Row Properties")]
                [Description("Which columns should be calculted as the total results in the total row")]
                public Int32Collection TotalColumns
                {
                    get
                    {
                        if (this._TotalColumns == null) this._TotalColumns = new Int32Collection();

                        return this._TotalColumns;
                    }
                    set { this._TotalColumns = value; }
                }
                [Category("Total Row Properties")]
                [Description("Define whether to calculate the total result for special columns and display the results in a total row")]
                public bool Total
                {
                    get { return this._Total; }
                    set { this._Total = value; }
                }

                [Category("Total Row Properties")]
                [Description("The title displayed before results in the total row")]
                public string TotalTitle
                {
                    get { return this._TotalTitle; }
                    set { this._TotalTitle = value; }
                }

                [Category("Hide Columns")]
                [Description("Define which columns should not be displayed in the control")]
                public Int32Collection HiddenColumns
                {
                    get
                    {
                        if (_HiddenColumns == null) _HiddenColumns = new Int32Collection();
                        return _HiddenColumns;
                    }
                    set { _HiddenColumns = value; }
                }

                [Category("Summaries For SectionFilter")]
                [Description("Add the summaries to display results frm calculating sectionfilters in report")]
                public GroupSummaryCollection SummariesForSections	//Modified at 2009-2-1 14:02:19@Scott
                {
                    get
                    {
                        if (_SummariesForSections == null) _SummariesForSections = new GroupSummaryCollection();
                        return this._SummariesForSections;
                    }
                    set { this._SummariesForSections = value; }
                }
                [Category("Chart Inforation")]
                [Description("Define the Row Height when selecting PieChart/BarChart/HorizonBarChart SummaryType in one summary")]
                public int ChartRowHeight
                {
                    get { return _ChartRowHeight; }
                    set
                    {
                        if (value <= 0)
                        {
                            _ChartRowHeight = 100;
                        }
                        else
                        {
                            _ChartRowHeight = value;
                        }
                    }
                }
                [Category("Secion sides properties")]
                [Description("Define whether could set the borderside in category 'Section Style' of style  window for column which displaying sectionfilters in report")]
                public bool SetSectionSides    //2009-5-7 14:10:34@Simon Add this Code
                {
                    get { return _SectionSides; }
                    set { _SectionSides = value; }
                }
                [Category("Total Row Properties")]
                [Description("Border Style for  the total row")]
                [Editor(typeof(Editors.SidesEditor), typeof(System.Drawing.Design.UITypeEditor))]
                public DevExpress.XtraPrinting.BorderSide TotalSides
                {
                    get { return _TotalSides; }
                    set { _TotalSides = value; }
                }
                #endregion

                //ISerializable Members
                #region ISerializable Members

                public void GetObjectData(SerializationInfo info, StreamingContext context)
                {
                    info.AddValue("_TotalSides", this.TotalSides, typeof(DevExpress.XtraPrinting.BorderSide));

                    info.AddValue("_SectionSides", this._SectionSides);  //2009-5-7 14:16:16@Simon Add this Code

                    info.AddValue("_ChartRowHeight", this._ChartRowHeight);  //2009-5-7 14:16:16@Simon Add this Code

                    info.AddValue("_SummariesForSections", this._SummariesForSections, typeof(GroupSummaryCollection));

                    info.AddValue("_HiddenColumns", this._HiddenColumns, typeof(Int32Collection));

                    info.AddValue("_TotalColumns", this._TotalColumns, typeof(Int32Collection));

                    info.AddValue("_Total", this._Total);  //2009-5-7 14:16:16@Simon Add this Code

                    info.AddValue("_TotalTitle", this._TotalTitle);  //2009-5-7 14:16:16@Simon Add this Code

                    info.AddValue("_DisregardAllPlaysBlank", this._DisregardAllPlaysBlank);  //2009-5-7 14:16:16@Simon Add this Code


                }

                public GroupAdvancedSetting(SerializationInfo info, StreamingContext context)
                {
                    try
                    {
                        this._DisregardAllPlaysBlank = info.GetBoolean("_DisregardAllPlaysBlank");
                    }
                    catch
                    {
                        this._DisregardAllPlaysBlank = false;
                    }

                    try
                    {
                        this._HiddenColumns = info.GetValue("_HiddenColumns", typeof(Int32Collection)) as Int32Collection;
                    }
                    catch
                    {
                        this._HiddenColumns = new Int32Collection();
                    }
                    try
                    {
                        this._SummariesForSections = (GroupSummaryCollection)info.GetValue("_SummariesForSections", typeof(GroupSummaryCollection));
                    }
                    catch
                    {
                        this._SummariesForSections = new GroupSummaryCollection();
                    }
                    try
                    {
                        this._ChartRowHeight = info.GetInt32("_ChartRowHeight");
                    }
                    catch
                    {
                        this._ChartRowHeight = 100;
                    }

                    try
                    {
                        this._SectionSides = info.GetBoolean("_SectionSides");  //2009-5-7 14:15:47@Simon Add this Code
                    }
                    catch
                    {
                        this._SectionSides = false;
                    }
                    try
                    {
                        this._TotalSides = (DevExpress.XtraPrinting.BorderSide)info.GetValue("_TotalSides", typeof(DevExpress.XtraPrinting.BorderSide));
                    }
                    catch
                    {
                        this._TotalSides = DevExpress.XtraPrinting.BorderSide.All;
                    }

                    try
                    {
                        this._Total = info.GetBoolean("_Total");
                    }
                    catch
                    {
                        this._Total = false;
                    }


                    try
                    {
                        this._TotalTitle = info.GetString("_TotalTitle");
                    }
                    catch
                    {
                        this._TotalTitle = "Total";
                    }

                    try
                    {
                        this._TotalColumns = info.GetValue("_TotalColumns", typeof(Int32Collection)) as Int32Collection;
                    }
                    catch
                    {
                        this._TotalColumns = new Int32Collection();
                    }




                }
                #endregion



                public GroupAdvancedSetting()
                {
                    this._Total = false;
                    this._TotalTitle = "Total";
                    this._TotalColumns = new Int32Collection();
                    _TotalSides = DevExpress.XtraPrinting.BorderSide.All;
                    _SectionSides = false;
                    _ChartRowHeight = 100;
                }

                #region Copy Function By Macro 2010-9-15 13:45:00
                public GroupAdvancedSetting Copy()
                {
                    GroupAdvancedSetting thiscopy = new GroupAdvancedSetting();
                    thiscopy._TotalSides = this._TotalSides;
                    this.HiddenColumns.CopyTo(thiscopy.HiddenColumns);
                    thiscopy._SectionSides = this._SectionSides;
                    thiscopy._ChartRowHeight = this._ChartRowHeight;
                    thiscopy._SummariesForSections = this.SummariesForSections.CopyStructure();
                    thiscopy._Total = this._Total;
                    thiscopy._TotalTitle = this._TotalTitle;
                    thiscopy._DisregardAllPlaysBlank = this._DisregardAllPlaysBlank;
                    this.TotalColumns.CopyTo(thiscopy.TotalColumns);
                    return thiscopy;
                }
                #endregion

            }
        #endregion

        #region public enum CellSizeAutoAdaptingTypes
            public enum CellSizeAutoAdaptingTypes
            {
                NotUse = 0,
                OneLine,
                WordWrap,
            }
        #endregion
    #endregion

   #region public class GroupView
    [Serializable]
    public class GroupView : ExControlView, IMultiHeader
    {
        //ISerializable Members
        #region ISerializable Members

        override public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            // TODO:  Add ExControlView.GetObjectData implementation
            //info.AddValue("MainStyle",this.MainStyle,typeof(BasicStyle));
            base.GetObjectData(info, context);
            //
            info.AddValue("_RootGroupInfo", this._RootGroupInfo, typeof(FieldGroupInfo));
            info.AddValue("_ShowRowIndicators", this._ShowRowIndicators);
            info.AddValue("_OneValuePerPage", this._OneValuePerPage);
            info.AddValue("_SizeSelfAdapting", this._SizeSelfAdapting);
            info.AddValue("_FootballType", (int)this._FootballType);
            info.AddValue("_Fitler", this._Fitler, typeof(Webb.Data.DBFilter));
            info.AddValue("_ColumnsWidth", this._ColumnsWidth, typeof(Int32Collection));
            info.AddValue("_RowsHight", this._RowsHight, typeof(Int32Collection));
            info.AddValue("_Styles", this._Styles, typeof(Styles.ExControlStyles));
            info.AddValue("_BreakRows", this._BreakRows, typeof(Int32Collection));
            info.AddValue("_HaveHeader", this._HaveHeader);
            info.AddValue("_CellSizeAutoAdapting", this._CellSizeAutoAdapting, typeof(CellSizeAutoAdaptingTypes));
            info.AddValue("_TopCount", this._TopCount);
            info.AddValue("_Total", this._Total);
            info.AddValue("_SectionInOneRow", this._SectionInOneRow);
            info.AddValue("_SectionTitle", this._SectionTitle);
            info.AddValue("_TotalTitle", this._TotalTitle);
            info.AddValue("_TotalColumns", this._TotalColumns, typeof(Int32Collection));
            info.AddValue("_HeightPerPage", this._HeightPerPage);
            info.AddValue("TableHeaders", this.TableHeaders, typeof(HeadersData));
            info.AddValue("_GroupSidesColumn", this._GroupSidesColumn);
            info.AddValue("SectionFilters", this.SectionFilters, typeof(SectionFilterCollection));//Modified 2008-09-08@Simon
            info.AddValue("SectionFiltersWrapper", this.SectionFiltersWrapper, typeof(SectionFilterCollectionWrapper));					//Modified at 2009-1-14 17:20:44@Scott


            info.AddValue("TotalSides", this, typeof(DevExpress.XtraPrinting.BorderSide));
            info.AddValue("_SectionSides", this._SectionSides);  //2009-5-7 14:16:16@Simon Add this Code
            info.AddValue("_ChartRowHeight", this._ChartRowHeight);  //2009-5-7 14:16:16@Simon Add this Code
            info.AddValue("_SummariesForSections", this._SummariesForSections, typeof(GroupSummaryCollection));

            info.AddValue("_VerticalGroupSides", this._VerticalGroupSides);

            info.AddValue("_GroupAdvancedSetting", this._GroupAdvancedSetting, typeof(GroupAdvancedSetting));

            info.AddValue("_OurBordersSetting", this._OurBordersSetting, typeof(OurBordersSetting));
           
        }

        public GroupView(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            // TODO:  Add ExControlView.GetObjectData implementation
            //this.MainStyle = info.GetValue("MainStyle",typeof(BasicStyle)) as BasicStyle;
            try
            {
                this._VerticalGroupSides = info.GetBoolean("_VerticalGroupSides");
            }
            catch
            {
                this._VerticalGroupSides = false;
            }
            try
            {
                this._RootGroupInfo = info.GetValue("_RootGroupInfo", typeof(GroupInfo)) as GroupInfo;

                if (_RootGroupInfo != null)
                {
                    if (this._RootGroupInfo is SectionGroupInfo)
                    {
                        this.RemoveSectionFilters();
                    }

                    _RootGroupInfo.SetAllSignToFalse(_RootGroupInfo);
                }
            }
            catch
            {
                this._RootGroupInfo = new Webb.Reports.ExControls.Data.FieldGroupInfo(Webb.Data.PublicDBFieldConverter.AvialableFields[0].ToString());
                this._RootGroupInfo.ColumnHeading = "New Group";
            }
            try
            {
                this.TableHeaders = info.GetValue("TableHeaders", typeof(HeadersData)) as HeadersData;
            }
            catch
            {
                this.TableHeaders = null;
            }
            try
            {
                this._SummariesForSections = (GroupSummaryCollection)info.GetValue("_SummariesForSections", typeof(GroupSummaryCollection));
            }
            catch
            {
                this._SummariesForSections = new GroupSummaryCollection();

                if ((_RootGroupInfo is FieldGroupInfo) && !(_RootGroupInfo as FieldGroupInfo).Visible && this.TableHeaders != null)
                {
                    TableHeaders.MoveLeft();
                }
            }
            try
            {
                this.TotalSides = (DevExpress.XtraPrinting.BorderSide)info.GetValue("TotalSides", typeof(DevExpress.XtraPrinting.BorderSide));
            }
            catch
            {
                this.TotalSides = DevExpress.XtraPrinting.BorderSide.None;
            }
            try
            {
                this._ShowRowIndicators = info.GetBoolean("_ShowRowIndicators");
            }
            catch
            {
                this.ShowRowIndicators = false;
            }
            try
            {
                this._ChartRowHeight = info.GetByte("_ChartRowHeight");
            }
            catch
            {
                this._ChartRowHeight = 100;
            }
            try
            {
                this._GroupSidesColumn = info.GetInt32("_GroupSidesColumn");
            }
            catch
            {
                this._GroupSidesColumn = -1;
            }

            try
            {
                this._OneValuePerPage = info.GetBoolean("_OneValuePerPage");
            }
            catch
            {
                this._OneValuePerPage = false;
            }

            try
            {
                this._SizeSelfAdapting = info.GetBoolean("_SizeSelfAdapting");
            }
            catch
            {
                this._SizeSelfAdapting = false;
            }

            try
            {
                this._FootballType = (FootballTypes)info.GetInt32("_FootballType");
            }
            catch
            {
                this._FootballType = FootballTypes.Both;
            }

            try
            {
                this._Fitler = info.GetValue("_Fitler", typeof(Webb.Data.DBFilter)) as Webb.Data.DBFilter;

                this._Fitler = AdvFilterConvertor.GetAdvFilter(DataProvider.VideoPlayBackManager.AdvReportFilters, this._Fitler);    //2009-4-29 11:37:37@Simon Add UpdateAdvFilter
            }
            catch
            {
                this._Fitler = new Webb.Data.DBFilter();
            }

            try
            {
                this._ColumnsWidth = info.GetValue("_ColumnsWidth", typeof(Int32Collection)) as Int32Collection;
            }
            catch
            {
                this._ColumnsWidth = new Int32Collection();
            }

            try
            {
                this._RowsHight = info.GetValue("_RowsHight", typeof(Int32Collection)) as Int32Collection;
            }
            catch
            {
                this._RowsHight = new Int32Collection();
            }

            try
            {
                this._Styles = info.GetValue("_Styles", typeof(Styles.ExControlStyles)) as Styles.ExControlStyles;
            }
            catch
            {
                this._Styles = new ExControlStyles();
                this._Styles.LoadDefaultStyle();     //08-14-2008@Scott
            }

            try
            {
                this._BreakRows = info.GetValue("_BreakRows", typeof(Int32Collection)) as Int32Collection;
            }
            catch
            {
                this._BreakRows = new Int32Collection();
            }

            try
            {
                this._HaveHeader = info.GetBoolean("_HaveHeader");
            }
            catch
            {
                this._HaveHeader = true;
            }

            try
            {
                this._CellSizeAutoAdapting = (CellSizeAutoAdaptingTypes)info.GetValue("_CellSizeAutoAdapting", typeof(CellSizeAutoAdaptingTypes));
            }
            catch
            {
                this._CellSizeAutoAdapting = CellSizeAutoAdaptingTypes.WordWrap;
            }

            try
            {
                this._TopCount = info.GetInt32("_TopCount");
            }
            catch
            {
                this._TopCount = 0;
            }
           
            try
            {
                this._Total = info.GetBoolean("_Total");
            }
            catch
            {
                this._Total = false;
            }

            try
            {
                this._SectionTitle = info.GetString("_SectionTitle");
            }
            catch
            {
                this._SectionTitle = string.Empty;
            }

            try
            {
                this._SectionInOneRow = info.GetBoolean("_SectionInOneRow");
            }
            catch
            {
                this._SectionInOneRow = true;
            }
            try
            {
                this._SectionSides = info.GetBoolean("_SectionSides");  //2009-5-7 14:15:47@Simon Add this Code
            }
            catch
            {
                this._SectionSides = false;
            }

            try
            {
                this._TotalTitle = info.GetString("_TotalTitle");
            }
            catch
            {
                this._TotalTitle = "Total";
            }

            try
            {
                this._TotalColumns = info.GetValue("_TotalColumns", typeof(Int32Collection)) as Int32Collection;
            }
            catch
            {
                this._TotalColumns = new Int32Collection();
            }

            try
            {
                this._HeightPerPage = info.GetInt32("_HeightPerPage");
            }
            catch
            {
                this._HeightPerPage = 0;
            }

            //added 2008-09-08@Simon
            try
            {
                this.SectionFilters = info.GetValue("SectionFilters", typeof(SectionFilterCollection)) as SectionFilterCollection;

            }
            catch
            {

            }

            //Modified at 2009-1-14 17:27:16@Scott
            try
            {
                this.SectionFiltersWrapper = info.GetValue("SectionFiltersWrapper", typeof(SectionFilterCollectionWrapper)) as SectionFilterCollectionWrapper;

            }
            catch
            {
                //this.SectionFiltersWrapper = new SectionFilterCollectionWrapper();
            }
            try
            {
                this._GroupAdvancedSetting = info.GetValue("_GroupAdvancedSetting", typeof(GroupAdvancedSetting)) as GroupAdvancedSetting;
            }
            catch
            {
                _GroupAdvancedSetting = this.GetNewAdvancedSetting();
            }
            try
            {
                this._OurBordersSetting = info.GetValue("_OurBordersSetting", typeof(OurBordersSetting)) as OurBordersSetting;
            }
            catch
            {
                _OurBordersSetting = new OurBordersSetting();
            }

        }
        #endregion

        //Fields
        #region Fields
        protected Webb.Reports.ExControls.Data.GroupInfo _RootGroupInfo;
        protected CellSizeAutoAdaptingTypes _CellSizeAutoAdapting;
        protected FootballTypes _FootballType;
        protected Webb.Data.DBFilter _Fitler;
        protected int _TopCount;
        protected bool _ShowRowIndicators;
        protected bool _HaveHeader;
        protected bool _SizeSelfAdapting;								//useless
        protected bool _OneValuePerPage;								//useless		

        protected bool _SectionInOneRow;								//05-07-2008@Scott
        protected string _SectionTitle;									//05-07-2008@Scott		
        protected Int32Collection _FilteredRows;						//NonSerializable
        protected GroupSummaryCollection _TotalSummaries;				//NonSerializable
        protected Int32Collection _HeaderRows;							//header row indicators for style builder
        protected Int32Collection _SectionRows;							//section row indicators for style builder
        protected Int32Collection _TotalRows;							//total row indicators for style builder
        protected Int32Collection _BreakRows;							//useless,page break row indicators for style builder
        protected StyleBuilder.StyleColumnInfo _ColumnStyleRows;		//columns indicators for style builder		
        protected Int32Collection _ColumnsWidth;
        protected Int32Collection _RowsHight;
        protected Styles.ExControlStyles _Styles;
        protected int _HeightPerPage;						//05-16-2008@Scott
        protected FieldGroupInfo _OneValuePerPageGroup;		//07-04-2008@Scott	//useless		
        protected HeadersData TableHeaders = null;     //2008-8-28 9:46:28@simon
        protected int _GroupSidesColumn = -1;  //Add at 2009-2-26 14:06:28@Simon	
        protected bool _VerticalGroupSides = false;  //Add at 2009-2-27 9:37:10@Simon	


        [NonSerialized]
        public static bool BreakedWithLine = false;
        [NonSerialized]
        protected ArrayList GroupSidesCollection = new ArrayList();
        [NonSerialized]
        protected Int32Collection GroupStartColumns = new Int32Collection();
        [NonSerialized]
        protected Int32Collection SectionStartRows = new Int32Collection();
        [NonSerialized]
        protected Int32Collection ChartCols = new Int32Collection();
        [NonSerialized]
        float BarMaxValues = -1;
        [NonSerialized]
        float MaxHorizonBarValues = -1;

        protected bool _Total;
        protected string _TotalTitle;									//05-09-2008@Scott
        protected Int32Collection _TotalColumns;						//05-09-2008@Scott
        protected bool _SectionSides = false;   //Field used in Advanced Setting
        protected byte _ChartRowHeight = 100;   //Field used in Advanced Setting
        protected GroupSummaryCollection _SummariesForSections = new GroupSummaryCollection();  //Field used in Advanced Setting
        protected DevExpress.XtraPrinting.BorderSide _TotalSides = DevExpress.XtraPrinting.BorderSide.None;  //Field used in Advanced Setting

        protected GroupAdvancedSetting _GroupAdvancedSetting = new GroupAdvancedSetting();

        protected OurBordersSetting _OurBordersSetting = new OurBordersSetting();

     
        #endregion

        //Properties
        #region Properties
        #region Old Proerties in GroupAdvancedSetting
        public GroupSummaryCollection SummariesForSections	//Modified at 2009-2-1 14:02:19@Scott
        {
            get
            {
                if (_SummariesForSections == null) _SummariesForSections = new GroupSummaryCollection();
                return this._SummariesForSections;
            }
            set { this._SummariesForSections = value; }
        }
        public byte ChartRowHeight
        {
            get { return _ChartRowHeight; }
            set
            {
                if (value <= 0)
                {
                    _ChartRowHeight = 100;
                }
                else
                {
                    _ChartRowHeight = value;
                }
            }
        }
        public bool SectionSides    //2009-5-7 14:10:34@Simon Add this Code
        {
            get { return _SectionSides; }
            set { _SectionSides = value; }
        }
        public DevExpress.XtraPrinting.BorderSide TotalSides
        {
            get { return _TotalSides; }
            set { _TotalSides = value; }
        }

        public Int32Collection TotalColumns
        {
            get
            {
                if (this._TotalColumns == null) this._TotalColumns = new Int32Collection();

                return this._TotalColumns;
            }
            set { this._TotalColumns = value; }
        }
        public bool Total
        {
            get { return this._Total; }
            set { this._Total = value; }
        }

        public string TotalTitle
        {
            get { return this._TotalTitle; }
            set { this._TotalTitle = value; }
        }

        #endregion
     

        public OurBordersSetting OurBordersSetting
        {
            get
            {
                if (_OurBordersSetting == null)
                {
                    _OurBordersSetting = new OurBordersSetting();

                }
                return _OurBordersSetting;
            }
            set
            {
                _OurBordersSetting = value;
            }
        }

        public GroupAdvancedSetting GroupAdvancedSetting
        {
            get
            {
                if (_GroupAdvancedSetting == null)
                {
                    _GroupAdvancedSetting = this.GetNewAdvancedSetting();

                }
                return _GroupAdvancedSetting;
            }
            set
            {
                _GroupAdvancedSetting = value;
            }
        }

        public bool VerticalGroupSides
        {
            get { return _VerticalGroupSides; }
            set { _VerticalGroupSides = value; }
        }
        public int GroupSidesColumn
        {
            get { return _GroupSidesColumn; }
            set { _GroupSidesColumn = value; }
        }
        public HeadersData HeadersData       //Added this code at 2009-2-5 15:55:21@Simon
        {
            get { return this.TableHeaders; }
            set { this.TableHeaders = value; }
        }


        public FieldGroupInfo OneValuePerPageGroup
        {
            get { return this._OneValuePerPageGroup; }
            set { this._OneValuePerPageGroup = value; }
        }
        public int HeightPerPage
        {
            get { return this._HeightPerPage; }
            set { this._HeightPerPage = value; }
        }

        public GroupSummaryCollection TotalSummaries
        {
            get
            {
                if (this._TotalSummaries == null) this._TotalSummaries = new GroupSummaryCollection();

                return this._TotalSummaries;
            }
            set { this._TotalSummaries = value; }
        }
        public bool SectionInOneRow
        {
            get { return this._SectionInOneRow; }
            set
            {
                if (this._SectionInOneRow != value)
                {
                    if (this.RootGroupInfo is SectionGroupInfo)
                    {
                        int column = this.ShowRowIndicators ? 1 : 0;

                        if (value)
                        {
                            this.ColumnsWidth.RemoveAt(column);
                        }
                        else
                        {
                            this.ColumnsWidth.Insert(column, BasicStyle.ConstValue.CellWidth * 2);
                        }
                    }

                    this._SectionInOneRow = value;
                }
            }
        }
        public string SectionTitle
        {
            get { return this._SectionTitle; }
            set { this._SectionTitle = value; }
        }
        public int TopCount
        {
            get { return this._TopCount; }
            set { this._TopCount = value < 0 ? 0 : value; }
        }
        public bool SizeSelfAdapting
        {
            get { return this._SizeSelfAdapting; }
            set { this._SizeSelfAdapting = value; }
        }
        public bool HaveHeader
        {
            get { return this._HaveHeader; }
            set { this._HaveHeader = value; }
        }
        public Styles.ExControlStyles Styles
        {
            get { return this._Styles; }
            set { this._Styles = value; }
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
        public Int32Collection HeaderRows
        {
            get
            {
                if (this._HeaderRows == null) this._HeaderRows = new Int32Collection();

                return this._HeaderRows;
            }
        }
        public Int32Collection SectionRows
        {
            get
            {
                if (this._SectionRows == null) this._SectionRows = new Int32Collection();

                return this._SectionRows;
            }
        }
        public Int32Collection TotalRows
        {
            get
            {
                if (this._TotalRows == null) this._TotalRows = new Int32Collection();

                return this._TotalRows;
            }
        }
        public Int32Collection BreakRows
        {
            get
            {
                if (this._BreakRows == null) this._BreakRows = new Int32Collection();

                return this._BreakRows;
            }
        }
        public StyleBuilder.StyleColumnInfo ColumnStyleRows
        {
            get
            {
                if (this._ColumnStyleRows == null) this._ColumnStyleRows = new Webb.Reports.ExControls.Styles.StyleBuilder.StyleColumnInfo();

                return this._ColumnStyleRows;
            }
        }

        public Webb.Data.DBFilter Filter
        {
            get
            {
                if (this._Fitler == null) this._Fitler = new Webb.Data.DBFilter();
                return this._Fitler;
            }
            set { this._Fitler = value.Copy(); }
        }

        public bool ShowRowIndicators
        {
            get { return this._ShowRowIndicators; }
            set
            {
                if (this._ShowRowIndicators != value)
                {
                    if (value) this.ColumnsWidth.Insert(0, 30);
                    else this.ColumnsWidth.RemoveAt(0);

                    this._ShowRowIndicators = value;
                }
            }
        }

        //useless
        new public bool OneValuePerPage
        {
            get { return false; }
            set { }
        }

        public CellSizeAutoAdaptingTypes CellSizeAutoAdapting
        {
            get { return this._CellSizeAutoAdapting; }
            set
            {
                this._CellSizeAutoAdapting = value;
            }
        }

        public FootballTypes FootballType
        {
            get { return this._FootballType; }
            set { this._FootballType = value; }
        }

        public Webb.Reports.ExControls.Data.GroupInfo RootGroupInfo
        {
            get { return this._RootGroupInfo; }
            set { this._RootGroupInfo = value; }
        }


        public Int32Collection FilteredRows
        {
            get
            {
                if (this._FilteredRows == null) this._FilteredRows = new Int32Collection();

                return this._FilteredRows;
            }
        }
        #endregion

        //ctor
        public GroupView(GroupingControl i_Control)
            : base(i_Control as ExControl)
        {
            this._RootGroupInfo = new Webb.Reports.ExControls.Data.FieldGroupInfo(Webb.Data.PublicDBFieldConverter.AvialableFields[0].ToString());
            this._RootGroupInfo.ColumnHeading = "New Group";
            this._Fitler = new Webb.Data.DBFilter();
            this._Styles = new ExControlStyles();
            this._Styles.LoadDefaultStyle();    //08-14-2008@Scott
            this._ShowRowIndicators = false;
            this._CellSizeAutoAdapting = CellSizeAutoAdaptingTypes.WordWrap;
            this._OneValuePerPage = false;
            this._HaveHeader = true;
            this._SizeSelfAdapting = false;
            this._TopCount = 0;
            this._Total = false;
            this._TotalTitle = "Total";
            this._SectionTitle = string.Empty;
            this._SectionInOneRow = true;
            this._TotalColumns = new Int32Collection();
            this._HeightPerPage = 0;
            this._GroupSidesColumn = -1;
            _TotalSides = DevExpress.XtraPrinting.BorderSide.All;
            _SectionSides = false;
            _ChartRowHeight = 100;

        }

        //Calculate grouped rows and columns
        #region Calculate grouped rows and columns
        /// <summary>
        /// Calculate grouped columns
        /// </summary>
        /// <returns></returns>
        public int GetGroupedColumns()
        {
            if (this.RootGroupInfo == null) return 1;

            int nCols = 0;

            if (this._ShowRowIndicators)
            {
                nCols += 1;
            }

            this.GroupSidesColumn = -1;

            ChartCols.Clear();

            this.GetGroupedColumns(this.RootGroupInfo, ref nCols);

            return nCols;
        }

        private void GetGroupedColumns(GroupInfo groupInfo, ref int nCols)
        {
            if (groupInfo.IsSectionOutSide)
            {
                if (!groupInfo.DistinctValues)	//06-23-2008@Scott
                {
                    if (groupInfo.UseLineBreak && this.GroupSidesColumn < nCols)  //Add at 2009-2-27 9:49:27@Simon
                    {
                        GroupSidesColumn = nCols;
                    }

                    groupInfo.ColumnIndex = nCols++;

                }
            }
            else
            {
                #region  summaries before the group
                if (groupInfo.FollowSummaries)
                {
                    if (groupInfo.Summaries != null)
                    {
                        foreach (GroupSummary summary in groupInfo.Summaries)
                        {
                            switch (summary.SummaryType)
                            {
                                case SummaryTypes.PieChart:
                                case SummaryTypes.BarChart:
                                case SummaryTypes.HorizonBarChart:
                                    ChartCols.Add(nCols);
                                    break;
                            }

                            summary.ColumnIndex = nCols++;
                        }
                    }
                }
                #endregion

                bool visible = GroupInfo.IsVisible(groupInfo);

                if (!groupInfo.DistinctValues)	//06-23-2008@Scott
                {
                    if (groupInfo.UseLineBreak && this.GroupSidesColumn < nCols)  //Add at 2009-2-27 9:49:27@Simon
                    {
                        if (visible || groupInfo.Summaries.Count > 0) GroupSidesColumn = nCols;
                    }

                    if (visible) groupInfo.ColumnIndex = nCols++;

                }

                #region  summaries after the group
                if (groupInfo.FollowSummaries)
                { }
                else
                {
                    if (groupInfo.Summaries != null)
                    {
                        foreach (GroupSummary summary in groupInfo.Summaries)
                        {
                            switch (summary.SummaryType)
                            {
                                case SummaryTypes.PieChart:
                                case SummaryTypes.BarChart:
                                case SummaryTypes.HorizonBarChart:
                                    ChartCols.Add(nCols);
                                    break;
                            }

                            summary.ColumnIndex = nCols++;
                        }
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


        #region GetColumnsWidth
        public int GetTotalColumnsWidth()
        {
            if (this.RootGroupInfo == null) return 0;

            Int32Collection columnsWidth = new Int32Collection();

            if (this._ShowRowIndicators)
            {
                columnsWidth.Add(30);
            }

            this.GetTotalColumnsWidth(this.RootGroupInfo, ref columnsWidth);

            int totalWidth = 0;

            this.ColumnsWidth = columnsWidth;

            foreach (int columnwidth in this.ColumnsWidth)
            {
                totalWidth += columnwidth;
            }
           
            return totalWidth;
        }
        public int GetIndividualTotalColumnsWidth()
        {
            if (this.RootGroupInfo == null) return 0;

            Int32Collection columnsWidth = new Int32Collection();

            if (this._ShowRowIndicators)
            {
                columnsWidth.Add(30);
            }

            this.GetTotalColumnsWidth(this.RootGroupInfo, ref columnsWidth);

            int totalWidth = 0;

            for (int i = this.ColumnsWidth.Count; i < columnsWidth.Count; i++)
            {
                totalWidth += columnsWidth[i];
            }

            this.ColumnsWidth = columnsWidth;

            return totalWidth;
        }


        private void GetTotalColumnsWidth(GroupInfo groupInfo, ref Int32Collection columnsWidth)
        {
            bool visible = GroupInfo.IsVisible(groupInfo);

            if (visible) columnsWidth.Add(groupInfo._ColumnWidth);

            if (groupInfo.Summaries != null)
            {
                foreach (GroupSummary summary in groupInfo.Summaries)
                {
                    columnsWidth.Add(summary.ColumnWidth);
                }

            }


            foreach (GroupInfo subGroupInfo in groupInfo.SubGroupInfos)
            {
                this.GetTotalColumnsWidth(subGroupInfo, ref columnsWidth);
            }
        }

        #endregion

        /// <summary>
        /// Calculate grouped rows
        /// </summary>
        /// <returns></returns>
        public int GetGroupedRows()
        {
            int m_value = 0;

            this.GetSubRows(this._RootGroupInfo, ref m_value);

            m_value += this.GetHeaderRowsCount();	//add header rows

            if (this.GroupAdvancedSetting.Total) m_value++;	//add total rows

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
                nRet++;
            }

            if (this.OneValuePerPage && this.HaveHeader)
            {
                if (this._RootGroupInfo == null) return nRet;

                if (this._RootGroupInfo.GroupResults == null) return nRet;

                if (this._RootGroupInfo.GroupResults.Count == 0) return nRet;

                return this._RootGroupInfo.GroupResults.Count;
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
                    if (i_GroupInfo.ParentGroupResult.ParentGroupInfo.DistinctValues)
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
                if (i_GroupInfo.DistinctValues && i_GroupInfo is SectionGroupInfo)
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

                        //						tempCount = tempValue - i_value;	//rows count of a sub groupinfo //07-21-2008@Scott
                        //
                        //						maxCount = Math.Max(maxCount,tempCount);	//max rows count of all sub groupinfos //07-21-2008@Scott

                        tempValue = i_value;
                    }

                    //					foreach(GroupInfo groupInfo in m_Result.SubGroupInfos)
                    //					{//07-21-2008@Scott
                    //						groupInfo.TotalRelatedRowIndex = maxCount;
                    //					}

                    i_value = Math.Max(i_value, maxValue);
                }
            }
        }
        #endregion

        #region Modified Area
        private bool RepeatHeader()
        {
            bool repeat = !this.ExControl.Report.Template.OneValuePerPage && !this.ExControl.Report.Template.ControlHeaderOnce;
            if ((this.TableHeaders == null || this.TableHeaders.RowCount <= 0) && !this.HaveHeader)
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
                if (this.TableHeaders != null)
                {
                    if (this.HaveHeader)
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

            BreakedWithLine = false;    //Add at 2009-2-26 17:24:49@Simon

            if (this.GroupSidesColumn >= 0 || this.VerticalGroupSides) BreakedWithLine = this.VerticalGroupSides;   //Add at 2009-2-26 17:25:01@Simon

            int endPosY = base.CreateArea(areaName, graph);

            BreakedWithLine = false;    //Add at 2009-2-26 17:24:49@Simon

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

        #region Convert old GroupInfo to new structure
        static public void ConvertOldGroupInfo(GroupInfo rootGroupInfo)
        {
            if (rootGroupInfo == null || rootGroupInfo.SubGroupInfo == null) return;

            GroupInfoCollection groupInfos = GetOldGroupInfo(rootGroupInfo);

            SetNewGroupInfo(rootGroupInfo, groupInfos);
        }

        static private GroupInfoCollection GetOldGroupInfo(GroupInfo rootGroupInfo)
        {
            GroupInfoCollection groupInfos = new GroupInfoCollection();

            GroupInfo groupInfo = rootGroupInfo;

            while (groupInfo.SubGroupInfo != null)
            {
                groupInfos.Add(groupInfo.SubGroupInfo.Copy());

                groupInfo = groupInfo.SubGroupInfo;
            }

            return groupInfos;
        }

        static private void SetNewGroupInfo(GroupInfo rootGroupInfo, GroupInfoCollection groupInfos)
        {
            rootGroupInfo.SubGroupInfo = null;

            rootGroupInfo.SubGroupInfos.Clear();

            GroupInfo groupInfo = rootGroupInfo;

            foreach (GroupInfo subGroupInfo in groupInfos)
            {
                groupInfo.SubGroupInfos.Add(subGroupInfo);

                groupInfo = subGroupInfo;
            }
        }
        #endregion

        /// <summary>
        /// Calculate grouped result
        /// </summary>
        /// <param name="i_Table">data source</param>
        public override void CalculateResult(DataTable i_Table)
        {
            //Convert old group struct to new one
            ConvertOldGroupInfo(this._RootGroupInfo);

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

            m_Rows.CopyTo(this.FilteredRows);

            //Find and create section filters
            bool bChange = false, bSectionGroupBefore = false, bSectionGroupAfter = false;

            bSectionGroupBefore = this.RootGroupInfo is SectionGroupInfo;

            if ((this.ExControl.Report.Template.OneValuePerPage || this.ExControl.Report.Template.RepeatedReport) &&
                (this.ExControl.Report.Template.GroupByField == string.Empty && this.ExControl.Report.Template.SectionFilters.Count > 0))	//Modified at 2008-11-14 15:43:40@Scott
            {
                this.RemoveSectionFilters();
            }
            else
            {
                this.CheckSectionFilters();
            }

            //bSectionGroupAfter = this.RootGroupInfo is SectionGroupInfo;

            //bChange = bSectionGroupBefore != bSectionGroupAfter;

            //if(bChange)
            //{
            //    if(!this._SectionInOneRow)
            //    {
            //        int column = this.ShowRowIndicators ? 1 : 0;

            //        if(bSectionGroupAfter)
            //        {
            //            this.ColumnsWidth.Insert(column,BasicStyle.ConstValue.CellWidth*2);
            //        }
            //        else
            //        {
            //            this.ColumnsWidth.RemoveAt(column);
            //        }
            //    }
            //}

            //Calculate group result

            this._RootGroupInfo.CalculateGroupResult(i_Table, m_Rows, m_Rows, this.RootGroupInfo);

            //Calculate Total
            Int32Collection totalIndicators = this.GetAllTotalIndicators();

            if (this.GroupAdvancedSetting.Total) this.CalculateAllTotal(i_Table, m_Rows, totalIndicators);
        }
        private void AdjustHeaderStyle()
        {
            if (this.HeaderRows.Count <= 0 || this.TableHeaders == null) return;

            HeaderRows.Sort();

            this.TableHeaders.SetHeadGridLine(this.PrintingTable, this.HeaderRows);

            int MinHeaderRow = HeaderRows[0];

            int MaxHeaderRow = HeaderRows[HeaderRows.Count - 1];

            #region UpdateHeadeStyle

            for (int row = MinHeaderRow; row <= MaxHeaderRow; row++)
            {
                int TableHeaderRow = row - MinHeaderRow;

                if (TableHeaderRow >= this.TableHeaders.RowCount) break;

                for (int col = 0; col < this.TableHeaders.ColCount; col++)
                {
                    HeaderCell headerCell = this.TableHeaders.GetCell(TableHeaderRow, col);

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

            foreach (int col in this.TableHeaders.ColsToMerge)
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
            if (this._RootGroupInfo == null) return false;

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
            this.BreakRows.Clear();
            this.ColumnStyleRows.Clear();

            this.GetAllBarMaxPoint(this._RootGroupInfo, ref this.BarMaxValues, ref this.MaxHorizonBarValues);

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

            ApplyChartColumnStyle(m_Rows, m_Column);

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

            SetBorderSides(PrintingTable, ignoreRows);

            this.HideColumns(this.GroupAdvancedSetting.HiddenColumns);        

            this.PrintingTable.ClearNoValueRows();

            this.OurBordersSetting.ChangeTableOutBorders(this.PrintingTable);

            System.Diagnostics.Debug.WriteLine("Create print table completely");

            return true;
        }

        #endregion

        private GroupAdvancedSetting GetNewAdvancedSetting()
        {
            GroupAdvancedSetting groupAdvancedSetting = new GroupAdvancedSetting();

            groupAdvancedSetting.SummariesForSections = this.SummariesForSections.CopyStructure();

            groupAdvancedSetting.TotalSides = this._TotalSides;

            groupAdvancedSetting.SetSectionSides = this._SectionSides;

            groupAdvancedSetting.ChartRowHeight = this._ChartRowHeight;

            groupAdvancedSetting.Total = this._Total;

            groupAdvancedSetting.TotalTitle = this._TotalTitle;

            if (this._TotalColumns != null)
            {
                this._TotalColumns.CopyTo(groupAdvancedSetting.TotalColumns);
            }

            return groupAdvancedSetting;
        }

        private void HideColumns(Int32Collection hiddenColumns)
        {
            if (hiddenColumns == null || hiddenColumns.Count == 0) return;

            int totalRows = this.PrintingTable.GetRows();

            int totalCols = this.PrintingTable.GetColumns();

            foreach (int m_col in hiddenColumns)
            {
                if (m_col < 0 || m_col >= totalCols) continue;

                for (int i = 0; i < totalRows; i++)
                {
                    IWebbTableCell cell = this.PrintingTable.GetCell(i, m_col);

                    cell.CellStyle.Width = 0;
                }
            }
        }

        private Color GetChartSummaryColor(int m_row, GroupSummary summary)
        {
            Color backColor = Color.Transparent;

            if (m_row % 2 == 0)
            {
                backColor = this.Styles.RowStyle.BackgroundColor;
            }
            else
            {
                backColor = this.Styles.AlternateStyle.BackgroundColor;
            }
            if (summary.ColorNeedChange)
            {
                backColor = summary.Style.BackgroundColor;
            }
            return backColor;
        }


        private void CheckSectionFilters()
        {//need change
            if (this.ExControl == null) return;
            if (this.ExControl.Report == null) return;
            WebbReport m_WebbReport = this.ExControl.Report as WebbReport;
            if (m_WebbReport == null) return;
            //09-08-2008@Scott modify some code
            if (SectionFilters.Count > 0)
            {
                this.CreateSectionGroupInfo(SectionFilters);
            }
            else if (m_WebbReport.Template.SectionFilters.Count > 0)
            {
                SectionFilterCollection reportSections = m_WebbReport.Template.SectionFilters;

                AdvFilterConvertor convertor = new AdvFilterConvertor();

                if (DataProvider.VideoPlayBackManager.AdvSectionType != AdvScoutType.None && m_WebbReport.Template.ReportScType != ReportScType.Custom)
                {
                    ReportScType sctype = AdvFilterConvertor.GetScType(m_WebbReport.Template.ReportScType, DataProvider.VideoPlayBackManager.AdvSectionType);  //2009-6-16 10:18:47@Simon Add this Code	    

                    reportSections = convertor.GetReportFilters(Webb.Reports.DataProvider.VideoPlayBackManager.AdvReportFilters, sctype);	//add 1-19-2008 scott
                }

                this.CreateSectionGroupInfo(reportSections);
            }
            else
            {
                this.RemoveSectionFilters();
            }
        }

        private void RemoveSectionFilters()
        {
            if (this._RootGroupInfo is SectionGroupInfo)
            {
                System.Diagnostics.Debug.Assert(this._RootGroupInfo.SubGroupInfos.Count == 1);

                this._RootGroupInfo = this._RootGroupInfo.SubGroupInfos[0];

                _RootGroupInfo.IsSectionOutSide = false;
            }
        }

        private void CreateSectionGroupInfo(SectionFilterCollection i_Sections)
        {
            if (this._RootGroupInfo is SectionGroupInfo)
            {
                (this._RootGroupInfo as SectionGroupInfo).SetSectionFilters(i_Sections);
            }
            else
            {
                SectionGroupInfo m_SectionInfo = new SectionGroupInfo();

                m_SectionInfo.SetSectionFilters(i_Sections);

                m_SectionInfo.SubGroupInfos.Clear();

                m_SectionInfo.SubGroupInfos.Add(this._RootGroupInfo);

                this._RootGroupInfo = m_SectionInfo;
            }

            if (_RootGroupInfo.SubGroupInfos.Count > 0)
            {
                _RootGroupInfo.UseLineBreak = this._RootGroupInfo.SubGroupInfos[0].UseLineBreak;
            }

            this._RootGroupInfo.ColumnHeading = this.SectionTitle;

            this._RootGroupInfo.DistinctValues = this.SectionInOneRow;

            _RootGroupInfo.IsSectionOutSide = true;

            _RootGroupInfo.Summaries = this.GroupAdvancedSetting.SummariesForSections.CopyStructure();
        }

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

        private void ApplyChartColumnStyle(int m_Rows, int m_Cols)
        {
            if (this.ChartCols.Count <= 0) return; ;

            for (int row = 0; row < m_Rows; row++)
            {
                for (int m_col = 0; m_col < m_Cols; m_col++)
                {
                    IWebbTableCell cell = this.PrintingTable.GetCell(row, m_col);         

                    if (!this.HeaderRows.Contains(row) && !this.TotalRows.Contains(row) && !this.SectionRows.Contains(row))
                    {
                        cell.CellStyle.Height = this.GroupAdvancedSetting.ChartRowHeight;
                    }
                }
            }
        }

        private int GetRealTopCount()
        {
            if (this.TopCount <= 0) return TopCount;

            int RealTopCount = this.TopCount;

            RealTopCount += this.GetHeaderRowsCount();

            if (this.GroupAdvancedSetting.Total) RealTopCount++;

            return RealTopCount;
        }

        private void SetTableValue()
        {
            int m_Rows = 0, m_Col = 0;

            if (this.ShowRowIndicators) m_Col = 1;	//add row indicator columns

            //this.SetColumnStyles();	//02-25-2008@Scott	//03-18-2008@Scott
            if (!this.ShowRowIndicators)
            {
                this.PrintingTable.GetCell(0, 0).Text = this.SectionTitle;
            }

            //if(!this.OneValuePerPage&&this.HaveHeader)   
            if (!this.OneValuePerPage)   //Modified at 2008-10-21 8:47:39@Simon
            {
                int nHeaderStart = m_Rows, nHeaderCount = 0;

                this.SetHeaderValue(ref m_Rows);	//set header value

                nHeaderCount = m_Rows - nHeaderStart;

                this.SetHeaderRows(nHeaderStart, nHeaderCount);
            }

            this.GroupSidesCollection.Clear();  //Add at 2009-2-26 15:37:07@Simon

            GroupStartColumns.Clear();

            SectionStartRows.Clear();

            this.SetRowsValue(ref m_Rows, ref m_Col, this._RootGroupInfo);	//set row value		

            if (this.GroupAdvancedSetting.Total)
            {
                this.SetTotalValue();	//04-30-2008@Scott
            }

            if (this.ShowRowIndicators)
            {//set row indicator value
                this.SetRowIndicators(this.PrintingTable.GetRows());
            }
        }

        private void SetTotalValue()
        {
            int lastRow = this.PrintingTable.GetRows() - 1;

            int lastCol = this.PrintingTable.GetColumns() - 1;

            if (this.TopCount > 0) lastRow = this.GetRealTopCount() - 1;	//07-02-2008@Scott

            this.TotalRows.Add(lastRow);

            //			this.PrintingTable.MergeCells(lastRow,lastRow,0,lastCol);
            //
            //			WebbTableCell cell = this.PrintingTable.GetCell(lastRow,0) as WebbTableCell;
            //
            //			if(cell != null)
            //			{
            //				cell.Text = string.Format("{0}: {1}",this._TotalTitle,this.FilteredRows.Count);
            //			}

            //05-09-2008@Scott
            int index = 0;

            bool bFindFirstCol = false;

            for (int col = 0; col <= lastCol; col++)
            {
                if (col < ColumnsWidth.Count && this.ColumnsWidth[col] == 0) continue;

                if (index > this.TotalSummaries.Count - 1) break;

                WebbTableCellHelper.SetCellValue(this.PrintingTable, lastRow, col, string.Empty, FormatTypes.String);	//Modified at 2009-2-1 16:02:57@Scott

                if (this.ColumnStyleRows.GroupColumns.Contains(col) || col == 0) continue;

                if (!this.GroupAdvancedSetting.TotalColumns.Contains(col))
                {
                    index++;

                    continue;
                }

                if (!bFindFirstCol)
                {
                    int startCol = col > 1 ? col - 2 : col - 1;

                    if (this.ShowRowIndicators && startCol == 0) startCol = 1;   //2009-4-28 9:14:34@Simon Add this Code

                    (this.PrintingTable.GetCell(lastRow, startCol) as WebbTableCell).ClickEventArg = null;

                    this.PrintingTable.MergeCells(lastRow, lastRow, startCol, col - 1);

                    this.PrintingTable.GetCell(lastRow, startCol).Text = this.GroupAdvancedSetting.TotalTitle;

                    bFindFirstCol = true;
                }

                WebbTableCell cell = this.PrintingTable.GetCell(lastRow, col) as WebbTableCell;

                if (index >= TotalSummaries.Count) continue;

                GroupSummary summary = this.TotalSummaries[index++];

                WebbTableCellHelper.SetCellValue(this.PrintingTable, lastRow, col, summary);
            }
        }

        public int GetFilterHeaderRow()
        {
            int index = -1;
            if (this.HeaderRows.Count <= 0) return -1;  //Modified at 2008-10-20 11:32:16@Simon

            foreach (int tl in this.HeaderRows)
            {
                if (index < tl) index = tl;
            }
            return index;
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
                if (!groupInfo.DistinctValues || groupInfo is FieldGroupInfo)	//if set OnValuePerRow, don't set group title
                {
                    prnHeaders[nCol] = groupInfo.ColumnHeading;
                    formats[nCol] = groupInfo.HeadingFormat;

                    nCol++;
                }
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

            if (this.TableHeaders != null && this.TableHeaders.RowCount > 0 && this.TableHeaders.ColCount > 0)    //2008-8-29 9:12:52@simon
            {
                this.TableHeaders.SetHeaders(PrintingTable, ref nRow, this);

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

            if (groupInfo.IsSectionOutSide)
            {
                if (!groupInfo.DistinctValues || groupInfo is FieldGroupInfo)	//if set OnValuePerRow, don't set group title
                {
                    this.PrintingTable.GetCell(nRow, nCol).Text = groupInfo.ColumnHeading;

                    nCol++;
                }
            }
            else
            {
                if (groupInfo.FollowSummaries)
                {
                    if (groupInfo.Summaries != null)
                    {
                        foreach (GroupSummary m_Summary in groupInfo.Summaries)
                        {
                            this.PrintingTable.GetCell(nRow, nCol).Text = m_Summary.ColumnHeading;

                            this.SetSummaryColumnStyle(m_Summary.SummaryType, nCol);	//Set column style

                            nCol++;
                        }
                    }
                }

                if (visible && (!groupInfo.DistinctValues || groupInfo is FieldGroupInfo))	//if set OnValuePerRow, don't set group title
                {
                    this.PrintingTable.GetCell(nRow, nCol).Text = groupInfo.ColumnHeading;

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
                            this.PrintingTable.GetCell(nRow, nCol).Text = m_Summary.ColumnHeading;

                            this.SetSummaryColumnStyle(m_Summary.SummaryType, nCol);	//Set column style

                            nCol++;
                        }
                    }
                }
            }

            foreach (GroupInfo subGroupInfo in groupInfo.SubGroupInfos)
            {
                SetHeaderValue(subGroupInfo, nRow, ref nCol);
            }
        }

        private void SetSummaryColumnStyle(SummaryTypes type, int col)
        {
            switch (type)
            {
                case SummaryTypes.Frequence:
                    this.ColumnStyleRows.FreqencyColumns.Add(col);
                    break;
                case SummaryTypes.ComputedPercent:          //Modified at 2008-9-25 13:57:36@Simon
                case SummaryTypes.Percent:
                case SummaryTypes.RelatedPercent:
                case SummaryTypes.GroupPercent:
                case SummaryTypes.DistPercent:	//Modified at 2008-10-6 14:01:25@Scott
                case SummaryTypes.DistGroupPercent: //Modified at 2008-10-6 15:08:29@Scott
                case SummaryTypes.TopPercent:
                    this.ColumnStyleRows.PercentColumns.Add(col);
                    break;
                case SummaryTypes.Total:
                case SummaryTypes.TotalMinus:
                case SummaryTypes.TotalPlus:
                case SummaryTypes.TotalPointsBB:
                    this.ColumnStyleRows.TotalColumns.Add(col);
                    break;
                case SummaryTypes.Average:
                case SummaryTypes.AverageMinus:
                case SummaryTypes.AveragePlus:
                    this.ColumnStyleRows.AverageColumns.Add(col);
                    break;
                default:
                    break;
            }
        }

        private void GetSubGroupColumns(GroupInfo i_GroupInfo, ref int Count)
        {
            Count += i_GroupInfo.GetGroupedColumns();

            if (i_GroupInfo.GroupResults == null || i_GroupInfo.GroupResults.Count == 0) return;

            GroupResult m_result = i_GroupInfo.GroupResults[0];

            if (m_result.SubGroupInfos == null || m_result.SubGroupInfos.Count == 0) return;

            foreach (GroupInfo subgroupInfo in m_result.SubGroupInfos)
            {
                GetSubGroupColumns(subgroupInfo, ref Count);
            }

        }

        private void GetAllBarMaxPoint(GroupInfo groupInfo, ref float BarMaxValues, ref float MaxHorizonBarValues)
        {
            if (groupInfo.GroupResults == null) return;

            foreach (GroupResult m_GroupResult in groupInfo.GroupResults)
            {
                if (m_GroupResult.Summaries == null) return;

                foreach (GroupSummary groupSummary in m_GroupResult.Summaries)
                {
                    if (groupSummary.ChartBase == null) continue;

                    if (groupSummary.ChartBase.Setting == null) return;

                    float maxWhenTop = groupSummary.ChartBase.Setting.MaxValuesWhenTop;

                    switch (groupSummary.SummaryType)
                    {
                        case SummaryTypes.BarChart:

                            BarMaxValues = Math.Max(BarMaxValues, maxWhenTop);

                            break;
                        case SummaryTypes.HorizonBarChart:
                            MaxHorizonBarValues = Math.Max(MaxHorizonBarValues, maxWhenTop);
                            break;
                        default:
                            break;

                    }
                }
                foreach (GroupInfo subGroupInfo in m_GroupResult.SubGroupInfos)
                {
                    GetAllBarMaxPoint(subGroupInfo, ref BarMaxValues, ref MaxHorizonBarValues);
                }
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

            #region Modify codes at 2009-2-26 14:33:43@Simon

            ChartGroupInfo colsInfo = new ChartGroupInfo(i_Col, i_GroupInfo.ColumnHeading);

            colsInfo.subIndexs = new Int32Collection();

            GroupStartColumns.Add(i_Col);

            colsInfo.StartCol = i_Col;

            int GroupCols = 0;

            this.GetSubGroupColumns(i_GroupInfo, ref GroupCols);

            colsInfo.EndCol = i_Col + GroupCols - 1;

            #endregion        //End Modify

            for (int m_row = 0; m_row < m_Rows; m_row++)
            {
                GroupResult m_GroupResult = i_GroupInfo.GroupResults[m_row];

                if (i_GroupInfo == this.RootGroupInfo && i_GroupInfo is SectionGroupInfo)
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

                    #region ColsInfo

                    colsInfo.index = i_Col;

                    colsInfo.subIndexs.Add(i_Row);

                    if (i_GroupInfo.ParentGroupResult == null)  //2009-5-7 14:25:41@Simon Add this Code
                    {
                        SectionStartRows.Add(i_Row);
                    }
                    #endregion

                    this.ColumnStyleRows.GroupColumns.Add(i_Col);

                    #endregion

                    #region Merge cell for section row
                    if (i_GroupInfo.DistinctValues && i_GroupInfo is SectionGroupInfo)
                    {
                        this.PrintingTable.MergeCells(i_Row, i_Row, i_Col, this.PrintingTable.GetColumns() - 1);

                        this.SectionRows.Add(i_Row);

                        i_Row++;
                    }
                    #endregion

                    #region Set sub rows
                    if (m_GroupResult.SubGroupInfos.Count > 0)
                    {
                        int m_StartRow = i_Row;

                        int maxRow = 0;

                        bool bFirstIn = true;	//06-23-2008@Scott

                        string strGroupValue = m_GroupResult.GroupValue.ToString();

                        foreach (GroupInfo groupInfo in m_GroupResult.SubGroupInfos)
                        {
                            if (!(i_GroupInfo.DistinctValues && i_GroupInfo is SectionGroupInfo) || !bFirstIn)	//06-23-2008@Scott
                            {
                                if (bFirstIn) bFirstIn = false;	//06-23-2008@Scott

                                i_Col++;
                            }

                            if (strGroupValue == "[NoValue]" && groupInfo.GroupResults != null)
                            {
                                foreach (GroupResult groupResult in groupInfo.GroupResults)
                                {
                                    groupResult.GroupValue = strGroupValue;
                                }
                            }

                            this.SetRowsValue(ref i_Row, ref i_Col, groupInfo);

                            maxRow = Math.Max(maxRow, i_Row);

                            i_Row = m_StartRow;
                        }
                        if (maxRow > m_StartRow)
                        {
                            maxRow--;
                        }
                        else if (i_GroupInfo.DistinctValues && i_GroupInfo is SectionGroupInfo)
                        {
                            maxRow--;
                        }
                        i_Row = maxRow;
                    }

                    i_Row++;

                    if (m_row < m_Rows - 1) i_Col = m_OriginalStartCol;
                    #endregion
                }
                else
                {
                    #region set summaryies ----Summaries before group
                    if (i_GroupInfo.FollowSummaries)
                    {
                        if (m_GroupResult.Summaries != null)
                        {
                            foreach (GroupSummary m_Summary in m_GroupResult.Summaries)
                            {
                                switch (m_Summary.SummaryType)
                                {
                                    case SummaryTypes.PieChart:
                                    case SummaryTypes.BarChart:
                                    case SummaryTypes.HorizonBarChart:
                                        {
                                            Image bitmap = new Bitmap(m_Summary.ColumnWidth, this.GroupAdvancedSetting.ChartRowHeight);

                                            Graphics g = Graphics.FromImage(bitmap);

                                            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                                            if (m_Summary.ChartBase != null)
                                            {
                                                if (m_Summary.SummaryType == SummaryTypes.BarChart && !m_Summary.ChartStyle.InnerCompared)
                                                {
                                                    m_Summary.ChartBase.Setting.MaxValuesWhenTop = this.BarMaxValues;
                                                }
                                                else if (m_Summary.SummaryType == SummaryTypes.HorizonBarChart && !m_Summary.ChartStyle.InnerCompared)
                                                {
                                                    m_Summary.ChartBase.Setting.MaxValuesWhenTop = this.MaxHorizonBarValues;
                                                }


                                                m_Summary.ChartBase.Draw(g, new Rectangle(0, 0, bitmap.Width, bitmap.Height));

                                            }

                                            IWebbTableCell cell = this.PrintingTable.GetCell(i_Row, i_Col);

                                            cell.Image = bitmap;//Image.FromHbitmap(this._Bitmap.GetHbitmap());

                                            cell.ImageSizeMode = PictureBoxSizeMode.StretchImage;


                                        }
                                        break;
                                    default:
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
                                            break;
                                        }
                                }
                                i_Col++;
                            }
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
                                object objValue = CResolveFieldValue.GetResolveValue(i_GroupInfo, m_GroupResult.GroupValue);

                                groupValue = objValue.ToString();

                                string followWith = (i_GroupInfo as FieldGroupInfo).FollowsWith;

                                if (followWith != string.Empty && followWith.IndexOf("[VALUE]") >= 0)
                                {
                                    groupValue = followWith.Replace("[VALUE]", groupValue);
                                }
                               
                            }
                        }
                        if (i_GroupInfo.DisplayAsImage)
                        {
                            WebbTableCellHelper.SetCellImageFromValue(this.PrintingTable, i_Row, i_Col, groupValue);

                            ChartCols.Add(i_Col);
                        }
                        else
                        {
                            if (m_GroupResult.ClickEvent == ClickEvents.PlayVideo)
                            {
                                WebbTableCellHelper.SetCellValueWithClickEvent(this.PrintingTable, i_Row, i_Col, groupValue, FormatTypes.String, m_GroupResult.RowIndicators);
                            }
                            else
                            {
                                WebbTableCellHelper.SetCellValue(this.PrintingTable, i_Row, i_Col, groupValue, FormatTypes.String);
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        i_Col--;
                    }

                    #region ColsInfo

                    colsInfo.index = visible ? i_Col : i_Col + 1;

                    colsInfo.subIndexs.Add(i_Row);

                    if (i_GroupInfo.ParentGroupResult == null)  //2009-5-7 14:25:41@Simon Add this Code
                    {
                        SectionStartRows.Add(i_Row);
                    }

                    #endregion

                    this.ColumnStyleRows.GroupColumns.Add(i_Col);

                    #endregion

                    #region set summaryies ----Summaries after group
                    if (i_GroupInfo.FollowSummaries)
                    { }
                    else
                    {
                        if (m_GroupResult.Summaries != null)
                        {
                            foreach (GroupSummary m_Summary in m_GroupResult.Summaries)
                            {
                                i_Col++;

                                switch (m_Summary.SummaryType)
                                {
                                    case SummaryTypes.PieChart:
                                    case SummaryTypes.BarChart:
                                    case SummaryTypes.HorizonBarChart:
                                        {
                                            Image bitmap = new Bitmap(m_Summary.ColumnWidth, this.GroupAdvancedSetting.ChartRowHeight);

                                            Graphics g = Graphics.FromImage(bitmap);

                                            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                                            if (m_Summary.ChartBase != null)
                                            {
                                                if (m_Summary.SummaryType == SummaryTypes.BarChart && !m_Summary.ChartStyle.InnerCompared)
                                                {
                                                    m_Summary.ChartBase.Setting.MaxValuesWhenTop = this.BarMaxValues;
                                                }
                                                else if (m_Summary.SummaryType == SummaryTypes.HorizonBarChart && !m_Summary.ChartStyle.InnerCompared)
                                                {
                                                    m_Summary.ChartBase.Setting.MaxValuesWhenTop = this.MaxHorizonBarValues;
                                                }

                                                m_Summary.ChartBase.Draw(g, new Rectangle(0, 0, bitmap.Width, bitmap.Height));

                                            }

                                            IWebbTableCell cell = this.PrintingTable.GetCell(i_Row, i_Col);

                                            cell.Image = bitmap;

                                            cell.ImageSizeMode = PictureBoxSizeMode.StretchImage;

                                        }
                                        break;
                                    default:
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
                                            break;
                                        }
                                }
                            }
                        }
                    }
                    #endregion

                    #region Set sub rows
                    if (m_GroupResult.SubGroupInfos.Count > 0)
                    {
                        int m_StartRow = i_Row;

                        int maxRow = 0;

                        bool bFirstIn = true;	//06-23-2008@Scott

                        string strGroupValue = m_GroupResult.GroupValue.ToString();

                        foreach (GroupInfo groupInfo in m_GroupResult.SubGroupInfos)
                        {
                            if (!(i_GroupInfo.DistinctValues && i_GroupInfo is SectionGroupInfo) || !bFirstIn)	//06-23-2008@Scott
                            {
                                if (bFirstIn) bFirstIn = false;	//06-23-2008@Scott

                                i_Col++;
                            }

                            if (strGroupValue == "[NoValue]" && groupInfo.GroupResults != null)
                            {
                                foreach (GroupResult groupResult in groupInfo.GroupResults)
                                {
                                    groupResult.GroupValue = strGroupValue;
                                }
                            }

                            this.SetRowsValue(ref i_Row, ref i_Col, groupInfo);

                            maxRow = Math.Max(maxRow, i_Row);

                            i_Row = m_StartRow;
                        }
                        if (maxRow > m_StartRow)
                        {
                            maxRow--;
                        }
                        else if (i_GroupInfo.DistinctValues && i_GroupInfo is SectionGroupInfo)
                        {
                            maxRow--;
                        }
                        i_Row = maxRow;
                    }

                    i_Row++;

                    if (m_row < m_Rows - 1) i_Col = m_OriginalStartCol;
                    #endregion
                }
            }

            if (colsInfo.index <= this.GroupSidesColumn)
            {
                this.GroupSidesCollection.Add(colsInfo);
            }

            #region Set total row
            if (i_GroupInfo.AddTotal && i_GroupInfo.TotalSummaries != null)
            {
                i_Col = m_OriginalStartCol;

                //				i_Row = Math.Max(i_Row, m_OriginalStartRow + i_GroupInfo.TotalRelatedRowIndex);	//07-21-2008@Scott

                if (i_GroupInfo.FollowSummaries)
                {
                    i_Col--;
                }
                else
                {
                    WebbTableCellHelper.SetCellValue(this.PrintingTable, i_Row, i_Col, i_GroupInfo.TotalTitle, FormatTypes.String);
                }

                Int32Collection totalIndicators = (i_GroupInfo as FieldGroupInfo).GetTotalIndicators(i_GroupInfo);

                foreach (GroupSummary m_TotalSummary in i_GroupInfo.TotalSummaries)
                {
                    i_Col++;

                    //04-24-2008@Scott
                    while (this.ColumnStyleRows.GroupColumns.Contains(i_Col))
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

                this.TotalRows.Add(i_Row);

                i_Row++;
            }
            else if (m_Rows == 0)
            {//correct column index when m_Rows is 0
                //08-07-2008@Scott delete
                //				if(i_GroupInfo.Summaries != null) 
                //				{
                //					i_Col += i_GroupInfo.Summaries.Count;	//07-23-2008@Scott
                //				}
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

        //05-09-2008@Scott
        private void GetTotalSummaries(GroupSummaryCollection totalSummaries)
        {
            FieldGroupInfo rootGroupInfo = null;

            if (this.RootGroupInfo is SectionGroupInfo)
            {
                rootGroupInfo = this.RootGroupInfo.GroupResults[0].SubGroupInfos[0] as FieldGroupInfo;
            }
            else
            {
                rootGroupInfo = this.RootGroupInfo as FieldGroupInfo;
            }

            rootGroupInfo.GetTotalSummaries(totalSummaries, rootGroupInfo);
        }

        //Modified at 2009-2-1 16:00:03@Scott
        private void CalculateAllTotal(System.Data.DataTable i_Table, Webb.Collections.Int32Collection i_OuterRows, Webb.Collections.Int32Collection i_InnerRows)
        {
            this.TotalSummaries.Clear();

            this.GetTotalSummaries(this.TotalSummaries);

            Int32Collection totalIndicators = new Int32Collection();
            i_InnerRows.CopyTo(totalIndicators);

            if (this.RootGroupInfo is FieldGroupInfo && this.RootGroupInfo.SubGroupInfos.Count == 0)
            {
                FieldGroupInfo fieldGroupInfo = this.RootGroupInfo as FieldGroupInfo;

                totalIndicators = fieldGroupInfo.GetTotalIndicators(fieldGroupInfo);
            }

            foreach (GroupSummary summary in this.TotalSummaries)
            {
                //summary.CalculateResult(i_Table,i_OuterRows,i_InnerRows/*08-27-2008@Scott*/,i_InnerRows);

                switch (summary.SummaryType)
                {
                    case SummaryTypes.Percent:
                    case SummaryTypes.GroupPercent:
                    case SummaryTypes.ComputedPercent:
                    case SummaryTypes.DistPercent:
                    case SummaryTypes.DistGroupPercent:
                    case SummaryTypes.TopPercent:
                        summary.CalculateResult(i_Table, i_OuterRows, totalIndicators/*08-27-2008@Scott*/, totalIndicators);
                        break;
                    case SummaryTypes.FreqAndPercent:
                    case SummaryTypes.FreqAndRelatedPercent:
                        summary.Value = 0;
                        break;
                    default:
                        summary.CalculateResult(i_Table, totalIndicators, totalIndicators/*08-27-2008@Scott*/, totalIndicators);
                        break;
                }
            }
        }

        //05-09-2008@Scott
        private Int32Collection GetAllTotalIndicators()
        {
            Int32Collection totalIndicators = new Int32Collection();

            if (this.RootGroupInfo.GroupResults == null) return totalIndicators;

            foreach (GroupResult result in this.RootGroupInfo.GroupResults)
            {
                if (result.RowIndicators == null) continue;

                foreach (int row in result.RowIndicators)
                {
                    if (totalIndicators.Contains(row)) continue;

                    totalIndicators.Add(row);
                }
            }

            return totalIndicators;
        }

        private void SetBorderSides(IWebbTable table, Int32Collection ignoreRows)  //Add at 2009-2-26 14:59:46@Simon
        {
            if (table == null) return;

            int EndColumn = table.GetColumns() - 1;

            IWebbTableCell cell = null;

            int EndRow = table.GetRows() - 1;

            #region Section Sides
            if (this.RootGroupInfo is SectionGroupInfo && !this.SectionInOneRow && this.GroupAdvancedSetting.SetSectionSides)
            {
                DevExpress.XtraPrinting.BorderSide sides = this.Styles.SectionStyle.Sides;

                for (int startcol = 0; startcol <= EndColumn; startcol++)
                {
                    if (this.ShowRowIndicators && startcol == 0) continue;

                    foreach (int rowindex in this.SectionStartRows)
                    {
                        cell = table.GetCell(rowindex, startcol);

                        if (cell == null) continue;

                        if ((sides & DevExpress.XtraPrinting.BorderSide.Top) > 0)   //Top Sides
                        {
                            cell.CellStyle.Sides |= DevExpress.XtraPrinting.BorderSide.Top;
                        }
                        else if (rowindex != SectionStartRows[0] && (sides & DevExpress.XtraPrinting.BorderSide.Bottom) > 0)
                        {
                            cell.CellStyle.Sides |= DevExpress.XtraPrinting.BorderSide.Top;
                        }
                    }


                    cell = table.GetCell(EndRow, startcol);   //Bottom Sides

                    if (!this.GroupAdvancedSetting.Total && (sides & DevExpress.XtraPrinting.BorderSide.Bottom) > 0)
                    {
                        cell.CellStyle.Sides = DevExpress.XtraPrinting.BorderSide.Bottom;
                    }

                    for (int rowindex = 0; rowindex <= EndRow; rowindex++)
                    {
                        if (ignoreRows.Contains(rowindex)) continue;

                        cell = table.GetCell(rowindex, startcol);

                        if ((sides & DevExpress.XtraPrinting.BorderSide.Left) > 0)   // //Left Sides
                        {
                            cell.CellStyle.Sides |= DevExpress.XtraPrinting.BorderSide.Left;
                        }
                        if ((sides & DevExpress.XtraPrinting.BorderSide.Right) > 0)   // //Left Sides
                        {
                            cell.CellStyle.Sides |= DevExpress.XtraPrinting.BorderSide.Right;
                        }
                        cell.CellStyle.BorderColor = this.Styles.SectionStyle.BorderColor;
                        cell.CellStyle.BorderStyle = this.Styles.SectionStyle.BorderStyle;
                        cell.CellStyle.BorderWidth = this.Styles.SectionStyle.BorderWidth;
                    }
                }

            }
            #endregion

            if (this.VerticalGroupSides)
            {
                for (int rowindex = 0; rowindex <= EndRow; rowindex++)
                {
                    if (ignoreRows.Contains(rowindex)) continue;

                    foreach (int startcol in this.GroupStartColumns)
                    {
                        cell = table.GetCell(rowindex, startcol);

                        if (cell != null) cell.CellStyle.Sides |= DevExpress.XtraPrinting.BorderSide.Left;

                    }

                    cell = table.GetCell(rowindex, EndColumn);

                    if (cell != null) cell.CellStyle.Sides |= DevExpress.XtraPrinting.BorderSide.Right;
                }
            }
            if (this.GroupAdvancedSetting.Total && this.GroupSidesColumn >= 0)
            {
                int startTotalCol = 0;

                if (this.ShowRowIndicators) startTotalCol++;

                DevExpress.XtraPrinting.BorderSide sides = this.GroupAdvancedSetting.TotalSides;

                cell = table.GetCell(EndRow, startTotalCol);

                cell.CellStyle.Sides = sides;

                if (startTotalCol != EndColumn)
                {
                    cell.CellStyle.Sides &= ~DevExpress.XtraPrinting.BorderSide.Right;
                }

                cell = table.GetCell(EndRow, EndColumn);

                cell.CellStyle.Sides = sides;

                if (startTotalCol != EndColumn)
                {
                    cell.CellStyle.Sides &= ~DevExpress.XtraPrinting.BorderSide.Left;
                }

                sides &= (~DevExpress.XtraPrinting.BorderSide.Left);

                sides &= (~DevExpress.XtraPrinting.BorderSide.Right);

                for (int colIndex = startTotalCol + 1; colIndex < EndColumn; colIndex++)
                {
                    cell = table.GetCell(EndRow, colIndex);

                    if (cell != null) cell.CellStyle.Sides = sides;
                }
            }

            if (this.GroupSidesCollection.Count <= 0) return;

            foreach (ChartGroupInfo GoupSideInfo in GroupSidesCollection)
            {
                for (int colindex = GoupSideInfo.StartCol; colindex <= GoupSideInfo.EndCol; colindex++)
                {
                    //foreach(int rowindex in  GoupSideInfo.subIndexs)
                    //{
                    //    cell=table.GetCell(rowindex,colindex);	

                    //    if(cell!=null)cell.CellStyle.Sides|=DevExpress.XtraPrinting.BorderSide.Top;
                    //}

                    foreach (int rowindex in GoupSideInfo.subIndexs)
                    {
                        cell = table.GetCell(rowindex, colindex);

                        if (cell != null) cell.CellStyle.Sides |= DevExpress.XtraPrinting.BorderSide.Top;

                        if (rowindex > 0)
                        {
                            cell = table.GetCell(rowindex - 1, colindex);

                            if (cell != null) cell.CellStyle.Sides |= DevExpress.XtraPrinting.BorderSide.Bottom;
                        }
                    }

                    if (this.TotalRows.Contains(EndRow)) continue;

                    cell = table.GetCell(EndRow, colindex);

                    if (cell != null) cell.CellStyle.Sides |= DevExpress.XtraPrinting.BorderSide.Bottom;
                }

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