
using System;
using System.Collections;

using System.Data;
using System.Text;
using System.Drawing;
using System.Collections.Specialized;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Serialization;
using System.Diagnostics;
using System.ComponentModel;
using System.ComponentModel.Design;

using Webb;
using Webb.Data;
using Webb.Utilities;
using Webb.Collections;
using Webb.Reports;

namespace Webb.Reports.ExControls.Data
{
    #region Data structure for groupSummay
    [Serializable]
    public enum SummaryTypes
    {
        None = 0,
        Frequence = 1,
        Percent = 2,
        Total = 3,
        Average = 4,
        Max = 5,
        Min = 6,
        // Sum for the puls value.
        TotalPlus = 7,
        // Sum for the minus value.
        TotalMinus = 8,
        // Average for the plus value.
        AveragePlus = 9,
        // Average for the Minus value.
        AverageMinus = 10,
        RelatedPercent = 11,

        GroupPercent = 12,
        // Freq&Percent
        FreqAndPercent = 13,
        FreqAndRelatedPercent = 14,

        TotalPointsBB = 15,   //09-11-2008@Simon

        ComputedPercent = 16,   //Modified at 2008-9-25 13:44:28@Simon

        DistFrequence = 17,	//09-26-2008@Scott
        DistPercent = 18,	//10-06-2008@Scott
        DistGroupPercent = 19, //Modified at 2008-10-6 14:29:19@Scott

        ParentRelatedPercent = 20,	//Modified at 2009-2-9 14:40:52@Scott

        TopPercent = 21,  //2009-9-2 12:16:29@Simon Add this Code

        FrequenceAllData=22,
        PercentAllData=23,


        // PieChart
        PieChart = 101,		//07-17-2008@Simon
        BarChart = 102,
        HorizonBarChart = 103,

    }
    public enum FormatTypes
    {
        Precent,
        Decimal,
        String,
        Int,
    }

    [Serializable]
    public enum AverageType
    {
        Mean = 0,
        Median,
        Mode
    }

    public class Fields
    {
        public static string CS_Hole = "Hole";
        public static string CS_Zone = "Zone";
        public static string CS_Formation = "Formation";
        public static string CS_PlayName = "Play Name";
        public static string CS_Yard = "Yard";
    }
  #endregion

    #region public class ChartSummaryInformation
    [Serializable]
    public class ChartSummaryInformation : ISerializable
    {
        #region Fields
        private bool _Chart3D = false;
        private bool _InnerCompared = false;
        private Webb.Data.SortingTypes _ChartSortType = Webb.Data.SortingTypes.Descending;
        private SortingByTypes _ChartSortByType = SortingByTypes.Frequence;
        private int _ChartTopCount = 0;
        private ReFilter _MinValueFilter = new ReFilter();
        protected Color _BackColor = Color.Empty;
        protected Color _AxesBackColor = Color.Empty;
        #endregion

        #region properties

        [Category("Background Color")]
        public Color BackColor
        {
            get { return this._BackColor; }
            set { this._BackColor = value; }
        }
        [Category("Background Color")]
        public Color AxesBackColor
        {
            get { return this._AxesBackColor; }
            set { this._AxesBackColor = value; }
        }

        [Category("3DStyle")]
        public bool Chart3D
        {
            get { return this._Chart3D; }
            set { this._Chart3D = value; }
        }
        [Category("Filter")]
        public int ChartTopCount
        {
            get { return this._ChartTopCount; }
            set { this._ChartTopCount = value; }
        }

        [Category("Delete Extra Space")]
        public bool InnerCompared
        {
            get { return this._InnerCompared; }
            set { this._InnerCompared = value; }
        }

        [Category("Sorting")]
        public SortingByTypes ChartSortByType
        {
            get { return this._ChartSortByType; }
            set { this._ChartSortByType = value; }
        }

        [Category("Sorting")]
        public Webb.Data.SortingTypes ChartSortType
        {
            get { return this._ChartSortType; }
            set { this._ChartSortType = value; }
        }

        [EditorAttribute(typeof(Webb.Reports.ExControls.Editors.ReFilterEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Category("Filter")]
        public ReFilter MinValueFilter
        {
            get
            {
                if (this._MinValueFilter == null) _MinValueFilter = new ReFilter();
                return this._MinValueFilter;
            }
            set
            {
                this._MinValueFilter = value;
            }
        }
        #endregion

        #region Constructor & Copy
        public ChartSummaryInformation()
        {
        }

        public ChartSummaryInformation Copy()
        {
            ChartSummaryInformation chartStyle = new ChartSummaryInformation();

            chartStyle.Chart3D = this._Chart3D;
            chartStyle.InnerCompared = this.InnerCompared;
            chartStyle.ChartTopCount = this.ChartTopCount;
            chartStyle.ChartSortByType = this._ChartSortByType;
            chartStyle.ChartSortType = this._ChartSortType;
            chartStyle.MinValueFilter = this.MinValueFilter.Copy();

            chartStyle.BackColor = this.BackColor;

            chartStyle.AxesBackColor = this.AxesBackColor;

            return chartStyle;

        }


        public override string ToString()
        {
            return "Summary As Chart";
        }

        #endregion

        #region CreateChart--CreatePieChart&CreateBarChart&CreateHorizonChart
        public ChartBase CreatePieChart(string _Field, System.Data.DataTable i_Table, Webb.Collections.Int32Collection i_Rows)
        {
            WebbChartSetting chartsetting = new WebbChartSetting();

            chartsetting.ChartType = ChartAppearanceType.Pie;

            if (this.Chart3D)
            {
                chartsetting.ChartType = ChartAppearanceType.Pie3D;
            }

            chartsetting.SeriesCollection = new SeriesCollection();

            Series series = new Series();

            series.FieldArgument = _Field;

            series.MinValueFilter = this.MinValueFilter.Copy();

            series.SeriesLabel.Position = ChartTextPosition.Center;

            series.SeriesLabel.TextColor = Color.Black;

            series.SeriesLabel.SortingType = this._ChartSortType;

            series.SeriesLabel.SortingByTypes = this._ChartSortByType;

            chartsetting.SeriesCollection.Add(series);

            chartsetting.AxisX.Visible = false;

            chartsetting.AxisY.Visible = false;

            chartsetting.Lengend.Visible = false;

            chartsetting.TopCount = this._ChartTopCount;

            ChartBase _ChartBase = chartsetting.CreateChart(i_Table, i_Rows);

            _ChartBase.Setting.TransparentBackground = this.BackColor;

            _ChartBase.Setting.BackgroundColor = this.AxesBackColor;

            _ChartBase.Setting.MaxValuesWhenTop = _ChartBase.GetMaxDataPoint();

            return _ChartBase;

        }

        public ChartBase CreateBarChart(string _Field, System.Data.DataTable i_Table, Webb.Collections.Int32Collection i_Rows)
        {

            WebbChartSetting chartsetting = new WebbChartSetting();

            chartsetting.ChartType = ChartAppearanceType.Bar;

            if (this.Chart3D)
            {
                chartsetting.ChartType = ChartAppearanceType.Bar3D;
            }

            chartsetting.SeriesCollection = new SeriesCollection();

            Series series = new Series();

            series.FieldArgument = _Field;

            series.MinValueFilter = this.MinValueFilter.Copy();

            series.SeriesLabel.Position = ChartTextPosition.Inside;

            series.SeriesLabel.TextColor = Color.Black;

            series.SeriesLabel.SortingType = this._ChartSortType;

            series.SeriesLabel.SortingByTypes = this._ChartSortByType;

            chartsetting.SeriesCollection.Add(series);

            chartsetting.AxisX.Gridlines = GridLineStyle.None;

            chartsetting.AxisX.Tickmarks = GridLineStyle.None;

            chartsetting.AxisY.Visible = false;

            chartsetting.AxisY.Gridlines = GridLineStyle.None;

            chartsetting.Lengend.Visible = false;

            chartsetting.TopCount = this._ChartTopCount;

            ChartBase _ChartBase = chartsetting.CreateChart(i_Table, i_Rows);

            _ChartBase.Setting.TransparentBackground = this.BackColor;

            _ChartBase.Setting.BackgroundColor = this.AxesBackColor;

            _ChartBase.Setting.MaxValuesWhenTop = _ChartBase.GetMaxDataPoint();

            return _ChartBase;


        }

        public ChartBase CreateHorizonBarChart(string _Field, System.Data.DataTable i_Table, Webb.Collections.Int32Collection i_Rows)
        {
            WebbChartSetting chartsetting = new WebbChartSetting();

            chartsetting.ChartType = ChartAppearanceType.HorizonBar;

            if (this.Chart3D)
            {
                chartsetting.ChartType = ChartAppearanceType.HorizonBar3D;
            }

            chartsetting.SeriesCollection = new SeriesCollection();

            Series series = new Series();

            series.FieldArgument = _Field;

            series.SeriesLabel.Position = ChartTextPosition.Inside;

            series.SeriesLabel.TextColor = Color.Black;

            series.SeriesLabel.SortingType = this._ChartSortType;

            series.MinValueFilter = this.MinValueFilter;

            series.SeriesLabel.SortingByTypes = this._ChartSortByType;

            chartsetting.SeriesCollection.Add(series);

            chartsetting.AxisX.Gridlines = GridLineStyle.None;

            chartsetting.AxisY.Tickmarks = GridLineStyle.None;

            chartsetting.AxisX.Visible = false;

            chartsetting.AxisY.Gridlines = GridLineStyle.None;

            chartsetting.TopCount = this._ChartTopCount;

            chartsetting.Lengend.Visible = false;

            ChartBase _ChartBase = chartsetting.CreateChart(i_Table, i_Rows);

            _ChartBase.Setting.TransparentBackground = this.BackColor;

            _ChartBase.Setting.BackgroundColor = this.AxesBackColor;

            _ChartBase.Setting.MaxValuesWhenTop = _ChartBase.GetMaxDataPoint();

            return _ChartBase;

        }
        #endregion

        #region Serialization By Simon's Macro 2009-11-6 10:19:34
        public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            info.AddValue("_Chart3D", _Chart3D);
            info.AddValue("_InnerCompared", _InnerCompared);
            info.AddValue("_ChartSortType", _ChartSortType, typeof(Webb.Data.SortingTypes));
            info.AddValue("_ChartSortByType", _ChartSortByType, typeof(Webb.Reports.ExControls.Data.SortingByTypes));
            info.AddValue("_ChartTopCount", _ChartTopCount);
            info.AddValue("_MinValueFilter", _MinValueFilter, typeof(Webb.Reports.ExControls.Data.ReFilter));
            info.AddValue("_BackColor", this._BackColor, typeof(Color));
            info.AddValue("_AxesBackColor", this._AxesBackColor, typeof(Color));

        }

        public ChartSummaryInformation(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            try
            {
                _Chart3D = info.GetBoolean("_Chart3D");
            }
            catch
            {
                _Chart3D = false;
            }
            try
            {
                _InnerCompared = info.GetBoolean("_InnerCompared");
            }
            catch
            {
                _InnerCompared = false;
            }
            try
            {
                _ChartSortType = (Webb.Data.SortingTypes)info.GetValue("_ChartSortType", typeof(Webb.Data.SortingTypes));
            }
            catch
            {
                _ChartSortType = Webb.Data.SortingTypes.Descending;
            }
            try
            {
                _ChartSortByType = (Webb.Reports.ExControls.Data.SortingByTypes)info.GetValue("_ChartSortByType", typeof(Webb.Reports.ExControls.Data.SortingByTypes));
            }
            catch
            {
                _ChartSortByType = Webb.Reports.ExControls.Data.SortingByTypes.Frequence;
            }
            try
            {
                _ChartTopCount = info.GetInt32("_ChartTopCount");
            }
            catch
            {
                _ChartTopCount = 0;
            }
            try
            {
                _MinValueFilter = (Webb.Reports.ExControls.Data.ReFilter)info.GetValue("_MinValueFilter", typeof(Webb.Reports.ExControls.Data.ReFilter));
            }
            catch
            {
                _MinValueFilter = new ReFilter();
            }
            try
            {
                _BackColor = (Color)info.GetValue("_BackColor", typeof(Color));
            }
            catch
            {
                this._BackColor = Color.Empty;
            }
            try
            {
                _AxesBackColor = (Color)info.GetValue("_AxesBackColor", typeof(Color));
            }
            catch
            {
                this._AxesBackColor = Color.Empty;
            }
        }
        #endregion

    }

    #endregion

    #region public class GroupSummary
    //Wu.Country@2007-11-14 16:26 added this region.
    [Serializable]
    public class GroupSummary : ISerializable
    {

        public GroupSummary()
        {
            this._SummaryType = SummaryTypes.Frequence;
            this._RelatedFieldName = string.Empty;	//Modified at 2008-11-18 13:58:18@Scott
            this._ColorNeedChange = false;
        }    


        #region CalculateResult Functions
        public void CalculateResult(System.Data.DataTable i_Table, Webb.Collections.Int32Collection i_OuterRows, Webb.Collections.Int32Collection i_RelatedRows, Webb.Collections.Int32Collection i_InnerRows)
        {
            this._Filter = AdvFilterConvertor.GetAdvFilter(DataProvider.VideoPlayBackManager.AdvReportFilters, this._Filter);    //2009-4-29 11:37:37@Simon Add UpdateAdvFilter

            if (this.Filter != null && this.Filter.Count > 0)
            {
                Int32Collection m_FilteredRows = this.GetFilteredRows(i_Table, i_InnerRows);

                this._RowIndicators = m_FilteredRows;

                if (this._SummaryType == SummaryTypes.ComputedPercent)
                {
                    this.CalculateComputePercent(i_Table, m_FilteredRows.Count, i_InnerRows);

                    return;
                }

                this.CalculateResultEx(i_Table, i_OuterRows, i_RelatedRows, m_FilteredRows);

                if (this._SummaryType == SummaryTypes.RelatedPercent)	//different between related percent and group percent
                {
                    Int32Collection denominatorFilteRows = this.DenominatorFilter.GetFilteredRows(i_Table, i_InnerRows);

                    if (i_InnerRows.Count > 0 && denominatorFilteRows.Count>0)		//12-20-2007@Scott
                    {
                        this._Result = 1.0 * m_FilteredRows.Count / denominatorFilteRows.Count;
                    }
                    else
                        this._Result = 0;
                }
            }
            else
            {
                if (this._SummaryType == SummaryTypes.ComputedPercent)
                {
                    this._Result = 0;

                    return;
                }

                this._RowIndicators = i_InnerRows;

                this.CalculateResultEx(i_Table, i_OuterRows, i_RelatedRows, i_InnerRows);
            }

            foreach (GroupSummary brotherSummary in this.BrotherSummaries)
            {
                brotherSummary.CalculateResult(i_Table, i_OuterRows, i_RelatedRows, i_InnerRows);
            }
        }


        private Int32Collection GetFilteredRows(System.Data.DataTable i_Table, Webb.Collections.Int32Collection i_Rows)
        {//06-04-2008@Scott
            return this.Filter.GetFilteredRows(i_Table, i_Rows);
        }

        private void CalculateResultEx(System.Data.DataTable i_Table, Webb.Collections.Int32Collection i_OuterRows, Webb.Collections.Int32Collection i_RelatedRows, Webb.Collections.Int32Collection i_InnerRows)
        {
            //Wu.Country@2007-11-26 15:54 modified some of the following code.
            switch (this._SummaryType)
            {
                case SummaryTypes.None:
                    this._Result = 0;
                    break;

                case SummaryTypes.Average:
                case SummaryTypes.AveragePlus:
                case SummaryTypes.AverageMinus:
                    this.CalculateAverage(i_Table,i_OuterRows, i_InnerRows, this._SummaryType);
                    break;
                case SummaryTypes.DistFrequence:
                    this.CalculateDistFrequence(i_Table, i_InnerRows);
                    break;
                case SummaryTypes.DistPercent:      
            
                    this.CalculateDistPercent(i_Table, i_OuterRows, i_InnerRows);
                    break;
                case SummaryTypes.DistGroupPercent:
                    i_RelatedRows = this.DenominatorFilter.GetFilteredRows(i_Table, i_RelatedRows);  //2009-4-8 14:03:15@Simon Add this Code
                    this.CalculateDistGroupPercent(i_Table, i_RelatedRows, i_InnerRows);
                    break;
                case SummaryTypes.Frequence:
                case SummaryTypes.FrequenceAllData:
                    this._Result = i_InnerRows.Count;
                    break;

                case SummaryTypes.Max:
                    this.CalculateMax(i_Table, i_InnerRows);
                    break;

                case SummaryTypes.Min:
                    this.CalculateMin(i_Table, i_InnerRows);
                    break;

                case SummaryTypes.Percent:
                case SummaryTypes.PercentAllData:
                    if (this.DenominatorFilter.Count > 0) i_OuterRows = this.DenominatorFilter.GetFilteredRows(i_Table, i_OuterRows);                    
                    this.CalculatePercent(i_InnerRows, i_OuterRows.Count);
                    break;

                case SummaryTypes.TotalPointsBB:	//Modified at 2008-9-11 10:48:11@Simon
                case SummaryTypes.Total:
                case SummaryTypes.TotalPlus:
                case SummaryTypes.TotalMinus:
                    this.CalculateTotal(i_Table, i_OuterRows, i_InnerRows, this._SummaryType);	//12-20-2007@Scott
                    break;

                case SummaryTypes.GroupPercent:
                case SummaryTypes.RelatedPercent:
                case SummaryTypes.ParentRelatedPercent:	//Modified at 2009-2-9 15:03:45@Scott

                    if (this.DenominatorFilter.Count > 0) i_RelatedRows = this.DenominatorFilter.GetFilteredRows(i_Table, i_RelatedRows);  //2009-4-8 14:03:15@Simon Add this Code
                    
                    this.CalculateGroupPercent(i_Table, i_RelatedRows.Count, i_InnerRows);
                    break;

                case SummaryTypes.TopPercent:
                    if (this.DenominatorFilter.Count > 0) i_RelatedRows = this.DenominatorFilter.GetFilteredRows(i_Table, i_RelatedRows);  //2009-4-8 14:03:15@Simon Add this Code
                    this.CalculateTopPercent(i_Table, i_RelatedRows.Count, i_InnerRows);
                    break;


                case SummaryTypes.FreqAndPercent:
                    if (this.DenominatorFilter.Count > 0) i_OuterRows = this.DenominatorFilter.GetFilteredRows(i_Table, i_OuterRows);
                    this.CalculateFreqAndPercent(i_Table, i_OuterRows.Count, i_InnerRows);
                    break;
                case SummaryTypes.FreqAndRelatedPercent:
                    if (this.DenominatorFilter.Count > 0) i_RelatedRows = this.DenominatorFilter.GetFilteredRows(i_Table, i_RelatedRows);  //2009-4-8 14:03:15@Simon Add this Code
                    this.CalculateFreqAndRelatedPercent(i_Table, i_RelatedRows.Count, i_InnerRows);
                    break;

                case SummaryTypes.PieChart:
                    if (this.DenominatorFilter.Count > 0) i_OuterRows = this.DenominatorFilter.GetFilteredRows(i_Table, i_OuterRows);
                    this.CalculatePieChart(i_Table, i_OuterRows.Count, i_InnerRows);
                    break;
                case SummaryTypes.BarChart:
                    if (this.DenominatorFilter.Count > 0) i_OuterRows = this.DenominatorFilter.GetFilteredRows(i_Table, i_OuterRows);
                    this.CalculateBarChart(i_Table, i_OuterRows.Count, i_InnerRows);
                    break;
                case SummaryTypes.HorizonBarChart:
                    if (this.DenominatorFilter.Count > 0) i_OuterRows = this.DenominatorFilter.GetFilteredRows(i_Table, i_OuterRows);
                    this.CalculateHorizonChart(i_Table, i_OuterRows.Count, i_InnerRows);
                    break;

                //case SummaryTypes.ComputedPercent:
                //this.CalculateComputePercent(i_Table,i_FilteredRowsCount,i_Rows);
                //break;

                default:
                    break;
            }
        }    

        private void CalculateComputePercent(System.Data.DataTable i_Table, int i_FilteredRowsCount, Webb.Collections.Int32Collection i_Rows)
        {
            if (this._SummaryType == SummaryTypes.ComputedPercent)
            {
                Int32Collection m_OutFilteredRows = this.DenominatorFilter.GetFilteredRows(i_Table, i_Rows);

                if (m_OutFilteredRows.Count > 0)
                    this._Result = 1.0 * i_FilteredRowsCount / m_OutFilteredRows.Count;
                else
                    this._Result = 0;
            }
        }

        private void CalculateTopPercent(System.Data.DataTable i_Table, int i_FilteredRowsCount, Webb.Collections.Int32Collection i_Rows)
        {
            if (this._SummaryType == SummaryTypes.TopPercent)
            {
                GroupInfo groupInfo = this.ParentGroupInfo;

                while (groupInfo != null && groupInfo.TopCount == 0)
                {
                    GroupResult m_GroupResult = groupInfo.ParentGroupResult;

                    if (m_GroupResult == null) break;

                    groupInfo = m_GroupResult.ParentGroupInfo;
                }

                Int32Collection m_OutFilteredRows = this.DenominatorFilter.GetFilteredRows(i_Table, i_Rows);

                if (groupInfo == null || groupInfo.TopCount == 0)
                {
                    if (m_OutFilteredRows.Count > 0)
                        this._Result = i_Rows.Count / (1.0 * i_FilteredRowsCount);
                    else
                        this._Result = 0;

                }
                else
                {
                    m_OutFilteredRows = new Int32Collection();

                    foreach (GroupResult m_GroupResult in groupInfo.GroupResults)
                    {
                        m_OutFilteredRows = m_OutFilteredRows.Combine(m_OutFilteredRows, m_GroupResult.RowIndicators);
                    }

                    m_OutFilteredRows = this.DenominatorFilter.GetFilteredRows(i_Table, m_OutFilteredRows);

                    if (m_OutFilteredRows.Count > 0)

                        this._Result = i_Rows.Count / (1.0f * m_OutFilteredRows.Count);
                    else
                        this._Result = 0;

                }


            }

        }

        private void CalculateFreqAndPercent(System.Data.DataTable i_Table, int i_FilteredRowsCount, Webb.Collections.Int32Collection i_Rows)
        {
            int nFreq = i_Rows.Count;

            this.CalculatePercent(i_Rows, i_FilteredRowsCount);

            string format = WebbTableCellHelper.CreatePercentFormat(this.DecimalSpace);	  //2009-4-30 14:10:36@Simon Add this Code

            string strPercent = string.Format("{0:" + format + "}", this._Result);

            this._Result = string.Format(@"{0} / {1}", nFreq, strPercent);
        }

        private void CalculatePercent(Int32Collection i_Rows, int i_FilteredRowsCount)
        {
            if (i_FilteredRowsCount > 0)
            {
                this._Result = 1.0 * i_Rows.Count / i_FilteredRowsCount;
            }
            else
            {
                this._Result = 0;
            }
        }

        

        private void CalculateFreqAndRelatedPercent(System.Data.DataTable i_Table, int i_RelatedCount, Webb.Collections.Int32Collection i_Rows)
        {
            int nFreq = i_Rows.Count;

            this.CalculateGroupPercent(i_Table, i_RelatedCount, i_Rows);

            string format = WebbTableCellHelper.CreatePercentFormat(this.DecimalSpace);	  //2009-4-30 14:10:36@Simon Add this Code

            string strPercent = string.Format("{0:" + format + "}", this._Result);

            this._Result = string.Format(@"{0} / {1}", nFreq, strPercent);
        }

        private void CalculateGroupPercent(System.Data.DataTable i_Table, int i_RelatedCount, Webb.Collections.Int32Collection i_Rows)
        {
        
            if (i_RelatedCount > 0)
                this._Result = 1.0 * i_Rows.Count / i_RelatedCount; //i_Table.Rows.Count;	//12-19-2007@Scott
            else
                this._Result = 0;
            //			}
        }

        //04-11-2008@Scott
        private void CalculateMax(DataTable i_Table, Webb.Collections.Int32Collection i_Rows)
        {
            double m_Max = 0, m_Temp = 0;

            if (!i_Table.Columns.Contains(this._RelatedFieldName)) goto EXIT;

            if (i_Rows.Count <= 0) goto EXIT;

            int i = 0;

            for (; i < i_Rows.Count; i++)
            {
                try
                {
                    string strValue = i_Table.Rows[i_Rows[i]][this._RelatedFieldName].ToString();

                    if (strValue == null || strValue == string.Empty)
                    {
                        continue;
                    }

                    m_Max = Convert.ToDouble(strValue);

                    break;
                }
                catch { continue; }
            }

            for (i++; i < i_Rows.Count; i++)
            {
                try
                {
                    string strValue = i_Table.Rows[i_Rows[i]][this._RelatedFieldName].ToString();

                    if (strValue == null || strValue == string.Empty)
                    {
                        continue;
                    }

                    m_Temp = Convert.ToDouble(strValue);

                    if (m_Temp > m_Max)
                    {
                        m_Max = m_Temp;
                    }
                }
                catch { continue; }
            }
        EXIT:
            this._Result = m_Max;
        }

        //04-11-2008@Scott
        private void CalculateMin(DataTable i_Table, Webb.Collections.Int32Collection i_Rows)
        {
            double m_Min = 0, m_Temp = 0;

            if (!i_Table.Columns.Contains(this._RelatedFieldName)) goto EXIT;

            if (i_Rows.Count <= 0) goto EXIT;

            int i = 0;

            for (; i < i_Rows.Count; i++)
            {
                try
                {
                    string strValue = i_Table.Rows[i_Rows[i]][this._RelatedFieldName].ToString();

                    if (strValue == null || strValue == string.Empty)
                    {
                        continue;
                    }

                    m_Min = Convert.ToDouble(strValue);

                    break;
                }
                catch { continue; }
            }

            for (i++; i < i_Rows.Count; i++)
            {
                try
                {
                    string strValue = i_Table.Rows[i_Rows[i]][this._RelatedFieldName].ToString();

                    if (strValue == null || strValue == string.Empty)
                    {
                        continue;
                    }

                    m_Temp = Convert.ToDouble(strValue);

                    if (m_Temp < m_Min)
                    {
                        m_Min = m_Temp;
                    }
                }
                catch { continue; }
            }
        EXIT:
            this._Result = m_Min;
        }

        //Modified at 2008-9-26 11:08:24@Scott
        private void CalculateDistFrequence(DataTable i_Table, Webb.Collections.Int32Collection i_Rows)
        {
            if (i_Table.Columns.Contains(this._RelatedFieldName))
            {
                ArrayList arrValue = new ArrayList();

                foreach (int row in i_Rows)
                {
                    object value = i_Table.Rows[row][this._RelatedFieldName];

                    if (arrValue.Contains(value))
                    {
                        continue;
                    }
                    else
                    {
                        arrValue.Add(value);
                    }
                }

                this._Result = arrValue.Count;
            }
            else
            {
                this._Result = 0;
            }
        }

        //Modified at 2008-10-6 9:59:10@Scott
        private void CalculateDistGroupPercent(DataTable i_Table, Webb.Collections.Int32Collection i_OuterRows, Webb.Collections.Int32Collection i_InnerRows)
        {
            int nInnerCount = 0, nOuterCount = 0;

            if (i_Table.Columns.Contains(this._RelatedFieldName))
            {
                ArrayList arrValue = new ArrayList();

                foreach (int row in i_InnerRows)
                {
                    object value = i_Table.Rows[row][this._RelatedFieldName];

                    if (arrValue.Contains(value))
                    {
                        continue;
                    }
                    else
                    {
                        arrValue.Add(value);
                    }
                }

                nInnerCount = arrValue.Count;

                arrValue.Clear();

                foreach (int row in i_OuterRows)
                {
                    object value = i_Table.Rows[row][this._RelatedFieldName];

                    if (arrValue.Contains(value))
                    {
                        continue;
                    }
                    else
                    {
                        arrValue.Add(value);
                    }
                }

                nOuterCount = arrValue.Count;

                if (nOuterCount == 0)
                {
                    this._Result = 0;
                }
                else
                {
                    this._Result = (double)nInnerCount / nOuterCount;
                }
            }
            else
            {
                this._Result = 0;
            }
        }

        //Modified at 2008-10-6 9:59:10@Scott
        private void CalculateDistPercent(DataTable i_Table, Webb.Collections.Int32Collection i_OuterRows, Webb.Collections.Int32Collection i_InnerRows)
        {
            int nInnerCount = 0, nOuterCount = 0;

            if (i_Table.Columns.Contains(this._RelatedFieldName))
            {
                ArrayList arrValue = new ArrayList();

                foreach (int row in i_InnerRows)
                {
                    object value = i_Table.Rows[row][this._RelatedFieldName];

                    if (arrValue.Contains(value))
                    {
                        continue;
                    }
                    else
                    {
                        arrValue.Add(value);
                    }
                }

                nInnerCount = arrValue.Count;

                arrValue.Clear();

                foreach (int row in i_OuterRows)
                {
                    object value = i_Table.Rows[row][this._RelatedFieldName];

                    if (arrValue.Contains(value))
                    {
                        continue;
                    }
                    else
                    {
                        arrValue.Add(value);
                    }
                }

                nOuterCount = arrValue.Count;

                if (nOuterCount == 0)
                {
                    this._Result = 0;
                }
                else
                {
                    this._Result = (double)nInnerCount / nOuterCount;
                }
            }
            else
            {
                this._Result = 0;
            }
        }
      
        #region Average mode
        private void CalcualteAvgeMean(DataTable i_Table, Webb.Collections.Int32Collection i_OuterRows, Webb.Collections.Int32Collection i_Rows, SummaryTypes i_Type)
        {
            decimal m_Sum = 0, fieldValue=0m;

            DateTime totalDateTime = new DateTime(0);

            long timeTicks = 0;

            if(!i_Table.Columns.Contains(_RelatedFieldName))
            {
                this._Result=0;

                return;
            }

            Type type=i_Table.Columns[_RelatedFieldName].DataType;

            Webb.Reports.DataProvider.WebbDataProvider publicprovider = Webb.Reports.DataProvider.VideoPlayBackManager.PublicDBProvider;

            bool isTimeData=(publicprovider != null && publicprovider.IsCCRMTimeData(this._RelatedFieldName)); 

            bool isFeetInchesData=(publicprovider != null && publicprovider.IsFeetInchesData(this._RelatedFieldName));       
      
            foreach (int i_RowIndex in i_Rows)
            {
                try
                {
                    object objValue=i_Table.Rows[i_RowIndex][this._RelatedFieldName];

                    if (objValue == null || objValue is System.DBNull) continue;

                    if(isTimeData)
                    {
                        TimeSpan timeSpan =Webb.Utility.ConvertToTimeTicks(objValue.ToString());

                        totalDateTime=totalDateTime.Add(timeSpan);
                    }
                    else if (isFeetInchesData)
                    {
                        fieldValue = Webb.Utility.ConvertFeetInchToNum(objValue.ToString());

                        m_Sum += fieldValue;
                    }
                    else if (type == typeof(DateTime))
                    {
                        timeTicks += Convert.ToDateTime(objValue).Ticks;
                    }
                    else
                    {
                        #region calcaulate Numer

                        string strValue = objValue.ToString();

                        if (strValue == null || strValue == string.Empty)
                        {
                            continue;
                        }

                        fieldValue = Convert.ToDecimal(strValue);

                        switch (i_Type)
                        {
                            case SummaryTypes.AveragePlus:
                                if (fieldValue > 0) m_Sum += fieldValue;
                                break;

                            case SummaryTypes.AverageMinus:
                                if (fieldValue < 0) m_Sum += fieldValue;

                                break;

                            case SummaryTypes.Average:
                            default:
                                m_Sum += fieldValue;
                                break;
                        }
                        #endregion
                    }
                }
                catch { continue; }
            }

            if (i_Rows.Count > 0)
            {
                if (type == typeof(DateTime))
                {
                    this._Result = new DateTime(timeTicks / i_Rows.Count);
                }
                if (isTimeData)
                {
                    #region CCRM Time Data 

                      timeTicks = totalDateTime.Ticks / i_Rows.Count; 
                 
                      DateTime dateTime=new DateTime(timeTicks);

                      bool outputTimeFormat = Webb.Utility.IsTimeFormatForOut(i_Table, i_OuterRows, _RelatedFieldName);

                      if (outputTimeFormat)
                      {
                          if (dateTime.Hour > 0)
                          {
                              this._Result = dateTime.ToString("h:m:s.ff");

                          }
                          else
                          {
                              this._Result = dateTime.ToString("m:s.ff"); ;
                          }
                      }
                      else
                      {
                          this._Result = dateTime.TimeOfDay.TotalSeconds;
                      }
                    #endregion

                }
                else if (isFeetInchesData)
                {
                     m_Sum = m_Sum / i_Rows.Count;

                     this._Result = Webb.Utility.FormatFeetInchData(m_Sum);                    
                }
                else 
                {
                    this._Result = m_Sum / i_Rows.Count;
                }
            }
            else
            {
                this._Result = 0;
            }
        }
        private void CalcualteAvgeMedian(DataTable i_Table, Webb.Collections.Int32Collection i_OuterRows, Webb.Collections.Int32Collection i_Rows, SummaryTypes i_Type) //Add this code at 2010-5-20 15:38:03
        {
            if (!i_Table.Columns.Contains(_RelatedFieldName))
            {
                this._Result = 0;

                return;
            }

            ArrayList sortedValues = new ArrayList();

            Type dataType = i_Table.Columns[_RelatedFieldName].DataType;

             Webb.Reports.DataProvider.WebbDataProvider publicprovider = Webb.Reports.DataProvider.VideoPlayBackManager.PublicDBProvider;

             bool isTimeData = (publicprovider != null && publicprovider.IsCCRMTimeData(this._RelatedFieldName));

             bool isFeetInchesData = (publicprovider != null && publicprovider.IsFeetInchesData(this._RelatedFieldName));

            foreach (int i_RowIndex in i_Rows)
            {
                try
                {
                     object objValue=i_Table.Rows[i_RowIndex][this._RelatedFieldName];

                     if (objValue == null || objValue is System.DBNull) continue;

                     if (isTimeData)
                     {
                         TimeSpan timeSpan = Webb.Utility.ConvertToTimeTicks(objValue.ToString());

                         sortedValues.Add(timeSpan.Ticks);

                     }
                     else if (isFeetInchesData)
                     {
                        decimal fieldValue = Webb.Utility.ConvertFeetInchToNum(objValue.ToString());

                         sortedValues.Add(fieldValue);
                     }
                     else if (dataType == typeof(DateTime))
                     {                        
                          sortedValues.Add(Convert.ToDateTime(objValue));
                     }
                     else
                     {
                         #region No dateTime

                         string strValue = objValue.ToString();

                         if (strValue == null || strValue == string.Empty)
                         {
                             continue;
                         }

                         decimal fieldValue =Convert.ToDecimal(strValue);
                        
                         switch (i_Type)
                         {
                             case SummaryTypes.AveragePlus:
                                 if (fieldValue > 0) sortedValues.Add(fieldValue);
                                 break;

                             case SummaryTypes.AverageMinus:
                                 if (fieldValue < 0) sortedValues.Add(fieldValue);
                                 break;

                             case SummaryTypes.Average:
                             default:
                                 sortedValues.Add(fieldValue);
                                 break;
                         }
                         #endregion
                     }
                      
                }
                catch { continue; }
            }
            sortedValues.Sort();

            if (sortedValues.Count <= 0)
            {
                this._Result = 0;
            }
            else if (sortedValues.Count % 2 == 0)
            {
                if (isTimeData)
                {
                    #region CCRM Time Data Result

                    long firstMid = (long)sortedValues[sortedValues.Count / 2 - 1];

                    long secondMid = (long)sortedValues[sortedValues.Count / 2];

                    long midTicks=(firstMid + secondMid) / 2;

                    TimeSpan timeSpan = new TimeSpan(midTicks);

                    DateTime avgdateTime = new DateTime(0);

                    avgdateTime = avgdateTime.Add(timeSpan);

                    bool outputTimeFormat = Webb.Utility.IsTimeFormatForOut(i_Table, i_OuterRows, _RelatedFieldName);

                    if (outputTimeFormat)
                    {
                        if (avgdateTime.Hour > 0)
                        {
                            this._Result = avgdateTime.ToString("h:m:s.ff");

                        }
                        else
                        {
                            this._Result = avgdateTime.ToString("m:s.ff"); ;
                        }
                    }
                    else
                    {
                        this._Result = timeSpan.TotalSeconds;
                    }
                  
                    #endregion
                }
                else if (dataType == typeof(DateTime))
                {
                    long firstMid = ((DateTime)sortedValues[sortedValues.Count / 2 - 1]).Ticks;
                    long secondMid = ((DateTime)sortedValues[sortedValues.Count / 2]).Ticks;
                    this._Result =new DateTime((firstMid + secondMid) / 2);
                }
                else
                {
                    decimal firstMid = (decimal)sortedValues[sortedValues.Count / 2 - 1];

                    decimal secondMid = (decimal)sortedValues[sortedValues.Count / 2];

                    decimal midNumber=(firstMid + secondMid) / 2;

                    if (isFeetInchesData)
                    {
                        this._Result = Webb.Utility.FormatFeetInchData(midNumber);
                    }
                    else
                    {
                        this._Result = midNumber;
                    }
                }
            }
            else
            {
                if (isTimeData)
                {
                    #region CCRM Time Data Result

                    long ticks=(long)sortedValues[sortedValues.Count / 2];

                    TimeSpan timeSpan = new TimeSpan(ticks);

                     DateTime avgdateTime = new DateTime(0);

                    avgdateTime = avgdateTime.Add(timeSpan);

                    if (avgdateTime.Hour > 0)
                    {
                        this._Result = avgdateTime.ToString("h:m:s.ff");
                    }
                    else
                    {                     
                        this._Result = avgdateTime.ToString("m:s.ff"); ;
                    }
                    #endregion
                }
                else if (isFeetInchesData)
                {
                    decimal dValue = (decimal)sortedValues[sortedValues.Count / 2];

                    this._Result = Webb.Utility.FormatFeetInchData(dValue);
                }
                else
                {
                    this._Result = sortedValues[sortedValues.Count / 2];
                }
            }
            
        }

        private void CalcualteAvgeMode(DataTable i_Table, Webb.Collections.Int32Collection i_OuterRows, Webb.Collections.Int32Collection i_Rows, SummaryTypes i_Type)
        {
            FieldGroupInfo fieldGroupInfo = new FieldGroupInfo();

            fieldGroupInfo.GroupByField = this._RelatedFieldName;

            fieldGroupInfo.CalculateGroupResult(i_Table, i_Rows, i_Rows,i_Rows,fieldGroupInfo);

            fieldGroupInfo.GroupResults.Sort(SortingTypes.Descending, SortingByTypes.Frequence);

            GroupResultCollection sortedResults = fieldGroupInfo.GroupResults;

            if (sortedResults.Count >= 3)
            {
                if (sortedResults[0].RowIndicators.Count > sortedResults[1].RowIndicators.Count)
                {
                    this._Result =CResolveFieldValue.GetResolveValue(this._RelatedFieldName,fieldGroupInfo.DateFormat,sortedResults[0].GroupValue);
                }
                else if (sortedResults[1].RowIndicators.Count > sortedResults[2].RowIndicators.Count)
                {
                    object value1 = CResolveFieldValue.GetResolveValue(this._RelatedFieldName, fieldGroupInfo.DateFormat, sortedResults[0].GroupValue);

                    object value2 = CResolveFieldValue.GetResolveValue(this._RelatedFieldName, fieldGroupInfo.DateFormat, sortedResults[1].GroupValue);

                    this._Result = string.Format("{0} and {1}", value1, value2);
                }
                else
                {
                    this._Result = "none";
                }

            }
            else if (sortedResults.Count == 2)
            {
                if (sortedResults[0].RowIndicators.Count > sortedResults[1].RowIndicators.Count)
                {
                    object value1 = CResolveFieldValue.GetResolveValue(this._RelatedFieldName, fieldGroupInfo.DateFormat, sortedResults[0].GroupValue);

                    this._Result = value1;
                }
                else
                {
                    this._Result = "none";
                }

            }
            else
            {
                object value1 = CResolveFieldValue.GetResolveValue(this._RelatedFieldName, fieldGroupInfo.DateFormat, sortedResults[0].GroupValue);

                this._Result = value1;
            }
        }
        #endregion

        #region CalculateAverage
        private void CalculateAverage(DataTable i_Table, Webb.Collections.Int32Collection i_OuterRows, Webb.Collections.Int32Collection i_Rows, SummaryTypes i_Type)
        {
            if (!i_Table.Columns.Contains(this._RelatedFieldName))
            {
                this._Result = 0;

                return;
            }
            switch (this._AverageType)
            {
                case AverageType.Median:
                    this.CalcualteAvgeMedian(i_Table,i_OuterRows, i_Rows, i_Type);
                    break;
                case AverageType.Mode:
                    this.CalcualteAvgeMode(i_Table, i_OuterRows, i_Rows, i_Type);
                    break;
                case AverageType.Mean:
                default:
                    CalcualteAvgeMean(i_Table, i_OuterRows, i_Rows, i_Type);
                    break;
            }
        }
        #endregion
   

        //04-11-2008@Scott
        private void CalculateTotal(DataTable i_Table, Webb.Collections.Int32Collection i_OuterRows, Webb.Collections.Int32Collection i_Rows, SummaryTypes i_Type)
        {
            decimal m_Total = 0, fieldValue = 0m;

            DateTime totalTimes = new DateTime(0);

            if (!i_Table.Columns.Contains(this._RelatedFieldName))
            {
                this._Result = 0;

                return;
            }

            Type dataType = i_Table.Columns[_RelatedFieldName].DataType;

            Webb.Reports.DataProvider.WebbDataProvider publicprovider = Webb.Reports.DataProvider.VideoPlayBackManager.PublicDBProvider;

            bool isTimeData = (publicprovider != null && publicprovider.IsCCRMTimeData(this._RelatedFieldName));

            bool isFeetInchesData = (publicprovider != null && publicprovider.IsFeetInchesData(this._RelatedFieldName));
         
            foreach (int i_RowIndex in i_Rows)
            {
                try
                {
                    string strValue = i_Table.Rows[i_RowIndex][this._RelatedFieldName].ToString();

                    if (strValue == null || strValue == string.Empty)
                    {
                        continue;
                    }

                    if (isTimeData)
                   {
                       #region CCRM Time Data

                       TimeSpan timeSpan = Webb.Utility.ConvertToTimeTicks(strValue);

                        totalTimes=totalTimes.Add(timeSpan);

                       #endregion
                   }
                    else if (isFeetInchesData)
                   {
                       #region FeetInches Data

                       fieldValue = Webb.Utility.ConvertFeetInchToNum(strValue);

                       m_Total += fieldValue;

                       #endregion
                   }
                   else
                   {
                       #region Other
                       if (i_Type != SummaryTypes.TotalPointsBB)
                       {
                           fieldValue = Convert.ToDecimal(strValue);
                       }

                       switch (i_Type)
                       {
                           //Modified at 2008-9-11 10:32:08@Simon
                           //add Area  CalculateTotalPoints  Special in baskketball Games					  		   
                           case SummaryTypes.TotalPointsBB:
                               {
                                   m_Total += Webb.Data.BasketBallRules.GetPoint(strValue);

                                   break;
                               }
                           //end add

                           case SummaryTypes.TotalPlus:
                               if (fieldValue > 0)
                               {
                                   m_Total += fieldValue;
                               }
                               break;

                           case SummaryTypes.TotalMinus:
                               if (fieldValue < 0)
                               {
                                   m_Total += fieldValue;
                               }
                               break;

                           case SummaryTypes.Total:
                           default:
                               m_Total += fieldValue;

                               break;
                       }
                       #endregion
                   }
                }
                catch { continue; }
            }
            if (isTimeData)
            {
                #region CCRM Time Description

                  bool outputTimeFormat = Webb.Utility.IsTimeFormatForOut(i_Table, i_OuterRows, _RelatedFieldName);

                  if (outputTimeFormat)
                  {
                      if (totalTimes.Day > 1)
                      {// 10-31-2011 Scott
                          this._Result = (totalTimes - new DateTime(0)).ToString();
                      }
                      else if (totalTimes.Hour > 0)
                      {
                          this._Result = totalTimes.ToString("h:m:s.ff");

                      }
                      else
                      {
                          this._Result = totalTimes.ToString("m:s.ff"); ;
                      }
                  }
                  else
                  {
                      this._Result = totalTimes.TimeOfDay.TotalSeconds; 
                  }
                #endregion
            }
            else if (isFeetInchesData)
            {
                this._Result = Webb.Utility.FormatFeetInchData(m_Total);
            }
            else
            {
                this._Result = m_Total;
            }
        }

        #region ChartSummaries
        //2009-10-26 13:18:34@Simon Add this Code
        private void CalculatePieChart(System.Data.DataTable i_Table, int i_FilteredRowsCount, Webb.Collections.Int32Collection i_Rows)
        {
            //			if(!PublicDBFieldConverter.AvialableFields.Contains(this.Field)) this._Result = null;
            //
            //			FieldGroupInfo chartGroupInfo = new FieldGroupInfo(this.Field);
            //
            //			chartGroupInfo.CalculateGroupResult(i_Table,i_FilteredRowsCount,i_Rows,chartGroupInfo);
            //
            //			Pie pie = new Pie();
            //
            //			PieChart pieChart = pie.CalculatePie(i_Table,chartGroupInfo,new DBFilter(),null);
            //
            //			this._Result = pieChart;			


            this.ChartBase = this.ChartStyle.CreatePieChart(this._RelatedFieldName, i_Table, i_Rows);

        }

        private void CalculateBarChart(System.Data.DataTable i_Table, int i_FilteredRowsCount, Webb.Collections.Int32Collection i_Rows)
        {
            this.ChartBase = this.ChartStyle.CreateBarChart(this._RelatedFieldName, i_Table, i_Rows);
        }
        private void CalculateHorizonChart(System.Data.DataTable i_Table, int i_FilteredRowsCount, Webb.Collections.Int32Collection i_Rows)
        {
            this.ChartBase = this.ChartStyle.CreateHorizonBarChart(this._RelatedFieldName, i_Table, i_Rows);
        }

        #endregion
        #endregion

        #region Field

        public GroupInfo ParentGroupInfo;
        private int _ColumnWidth = BasicStyle.ConstValue.CellWidth;

        //public event EventHandler SummaryTypeChanged;
        protected SummaryTypes _SummaryType;
        protected object _Result;

        protected int _SummaryID;
        protected Webb.Data.DBFilter _Filter;
        protected string _RelatedFieldName;
        protected string _SummaryTitle;
        protected Int32Collection _RowIndicators;
        protected BasicStyle _Style;	//05-26-2008@Scott
        protected int _ColumnIndex;		//05-26-2008@Scott
        protected bool _ShowZeros;		//06-25-2008@Scott
        protected string _ShowSymbol;	//Modified at 2009-2-1 14:01:09@Scott
        protected StringFormatFlags _TitleFormat;	//06-26-2008@Scott
        protected string _FollowSummary;    //08-18-2008@Scott
        protected bool _ColorNeedChange;	//Modified at 2009-2-11 16:18:07@Scott
        private sbyte _DecimalSpace = -1;    //Added this code at 2009-2-1 15:44:55@Simon
        protected DBFilter _DenominatorFilter;

        [NonSerialized]
        private ChartBase _ChartBase = null;   //2009-10-23 11:17:01@Simon Add this Code

        protected ChartSummaryInformation _ChartStyle = new ChartSummaryInformation();

        protected object _Tag = null;

        protected AverageType _AverageType = AverageType.Mean;

        protected string _SepratorBefore = "[2 Space]";
        protected GroupSummaryCollection _BrotherSummaries = new GroupSummaryCollection();

        protected bool _IsDecimalData = false;

        #endregion

        #region Properties

        [Category("Summary Information")]
        [EditorAttribute(typeof(Webb.Reports.ExControls.Editors.SeparatorEditor), typeof(System.Drawing.Design.UITypeEditor))]	//06-11-2008@Scott
        public string SeparatorBefore	//Modified at 2009-2-1 14:02:19@Scott
        {
            get
            {
                if (_SepratorBefore == null) _SepratorBefore = "[2 Space]";
                return this._SepratorBefore;
            }
            set { this._SepratorBefore = value; }
        }
        [Category("Brother Summaries")]
        public GroupSummaryCollection BrotherSummaries	//Modified at 2009-2-1 14:02:19@Scott
        {
            get
            {
                if (_BrotherSummaries == null) _BrotherSummaries = new GroupSummaryCollection();
                return this._BrotherSummaries;
            }
            set { this._BrotherSummaries = value; }
        }

        [Browsable(false)]
        public object Tag
        {
            get { return _Tag; }
            set { _Tag = value; }

        }

        [Browsable(false)]
        public ChartBase ChartBase
        {
            get { return _ChartBase; }
            set { _ChartBase = value; }

        }

        [Category("Summary as Chart")]
        [EditorAttribute(typeof(Webb.Reports.ExControls.Editors.PropertyEditor), typeof(System.Drawing.Design.UITypeEditor))]	//06-11-2008@Scott
        public ChartSummaryInformation ChartStyle
        {
            get
            {
                if (_ChartStyle == null) _ChartStyle = new ChartSummaryInformation();
                return _ChartStyle;
            }
            set
            {
                _ChartStyle = value;
            }
        }


        [Category("Style")]
        public int ColumnWidth
        {
            get { return this._ColumnWidth <= 0 ? BasicStyle.ConstValue.CellWidth : this._ColumnWidth; }
            set { this._ColumnWidth = value <= 0 ? BasicStyle.ConstValue.CellWidth : value; }
        }
        [Category("Summary Information")]
        public string ShowSymbol	//Modified at 2009-2-1 14:02:19@Scott
        {
            get { return this._ShowSymbol; }
            set { this._ShowSymbol = value; }
        }

        [Category("Summary Information")]
        public string FollowSummary
        {
            get { return this._FollowSummary; }
            set { this._FollowSummary = value; }
        }
        [Category("ColumnHeading")]
        public StringFormatFlags HeadingFormat
        {
            get { return this._TitleFormat; }
            set { this._TitleFormat = value; }
        }
        [Category("ColumnHeading")]
        public string ColumnHeading
        {
            get
            {
                if (_SummaryTitle == null) _SummaryTitle = string.Empty;
                return this._SummaryTitle;
            }
            set { this._SummaryTitle = value; }
        }
        [Browsable(false)]
        public int ColumnIndex
        {
            get { return this._ColumnIndex; }
            set { this._ColumnIndex = value; }
        }
        [Category("Summary Information")]
        public bool ShowZeros
        {
            get { return this._ShowZeros; }
            set { this._ShowZeros = value; }
        }
        [Browsable(true), Category("Style")]
        public bool ColorNeedChange	//Modified at 2009-2-11 16:18:04@Scott
        {
            get { return this._ColorNeedChange; }
            set { this._ColorNeedChange = value; }
        }
        [Browsable(true), Category("Style")]
        public BasicStyle Style
        {
            get
            {
                if (this._Style == null) this._Style = new BasicStyle();

                return this._Style;
            }
            set { this._Style = value; }
        }
        [Browsable(true), Category("Defination")]
        public SummaryTypes SummaryType
        {
            //protected object PropertyName;
            get { return this._SummaryType; }
            set
            {
                this._SummaryType = value;

                this._SummaryTitle = value.ToString();

                switch (this._SummaryType)
                {
                    case SummaryTypes.PieChart:
                    case SummaryTypes.BarChart:
                    case SummaryTypes.HorizonBarChart:
                        this._ColumnWidth = 160;
                        break;
                }
                //if(this.SummaryTypeChanged != null) this.SummaryTypeChanged(this,null);
            }
        }
        [Browsable(false), Category("Average/Total Defination")]
        public bool IsDecimalData
        {
            get
            {
                return this._IsDecimalData;
            }
            set { this._IsDecimalData = value; }
        }
        [Browsable(true), Category("Average/Total Defination")]
        public AverageType AverageType
        {
            get { return this._AverageType; }
            set
            {
                if (this._SummaryType != SummaryTypes.Average)
                {
                    Webb.Utilities.MessageBoxEx.ShowMessage("please set the 'SummaryType' to 'Average' first!");

                    return;
                }

                this._AverageType = value;

                this._SummaryTitle = value.ToString();
            }
        }       

        [Browsable(false)]
        public object Value
        {
            //protected object PropertyName;
            get
            {
                if (this._Result == null)	 //Added this code at 2008-11-17 16:56:25@Simon
                    return string.Empty;

                string strValue = this._Result.ToString();

                #region Modified Area 2009-2-1 14:15:14@Scott
                if (this.RowIndicators.Count == 0)
                {
                    if (this._ShowZeros)
                    {
                        if (this._ShowSymbol != null && this._ShowSymbol != string.Empty)
                        {
                            return this._ShowSymbol;
                        }
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
                //				if(!this._ShowZeros && this.RowIndicators.Count == 0)	//06-25-2008@Scott
                //					return string.Empty;
                #endregion

                return this._Result;
            }
            set
            {
                this._Result = value;
            }
        }

        [Category("Summary Information")]
        public sbyte DecimalSpace
        {
            get { return this._DecimalSpace; }
            set { this._DecimalSpace = value; }
        }
        /// <summary>
        /// Don't modify the summary id at any time, it will auto created when you add the
        /// summary into a GroupSummaryCollection.
        /// And then you can get the summary from the collecion use the id.
        /// //Wu.Country@2007-11-30 08:02 modified some of the following code.
        /// It's useless.
        /// </summary>		
        [Browsable(false)]
        public int SummaryID
        {
            //protected object PropertyName;
            get { return this._SummaryID; }
            set { this._SummaryID = value; }
        }

        [Browsable(true), Category("Summary Information")]
        [Editor(typeof(Webb.Reports.Editors.FieldSelectEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string Field
        {
            //protected object PropertyName;
            get
            {
                if (this._RelatedFieldName == null) this._RelatedFieldName = string.Empty;
                return this._RelatedFieldName;
            }
            set { this._RelatedFieldName = value; }
        }
        

        [Browsable(true), Category("Filters")]
        [EditorAttribute(typeof(Webb.Reports.Editors.DBFilterEditor), typeof(System.Drawing.Design.UITypeEditor))]	//06-11-2008@Scott
        public DBFilter Filter
        {
            //protected object PropertyName;
            get
            {
                if (this._Filter == null) this._Filter = new DBFilter();	//Modified at 2008-11-18 14:01:50@Scott

                return this._Filter;
            }
            set { this._Filter = value.Copy(); }
        }


        [Browsable(true), Category("Filters")]
        [EditorAttribute(typeof(Webb.Reports.Editors.DBFilterEditor), typeof(System.Drawing.Design.UITypeEditor))]	//Modified at 2008-9-25 13:46:36@Simon
        public DBFilter DenominatorFilter
        {
            //protected object PropertyName;
            get
            {
                if (this._DenominatorFilter == null) this._DenominatorFilter = new DBFilter();
                return this._DenominatorFilter;
            }
            set { this._DenominatorFilter = value.Copy(); }
        }

        //03-10-2008@Scott
        [Browsable(false)]
        public Int32Collection RowIndicators
        {
            get
            {
                if (this._RowIndicators == null) this._RowIndicators = new Int32Collection();

                return this._RowIndicators;
            }
        }
        #endregion

        #region Copy & ToString Function
        public virtual GroupSummary Copy()
        {
            GroupSummary m_Summary = new GroupSummary();
            if (this._Filter != null)
            {
                m_Summary._Filter = this._Filter.Copy();
            }
            m_Summary._RelatedFieldName = this._RelatedFieldName;
            m_Summary._SummaryID = this._SummaryID;
            m_Summary._SummaryType = this._SummaryType;
            m_Summary._Result = this._Result;
            m_Summary._SummaryTitle = this._SummaryTitle;
            m_Summary._ShowZeros = this._ShowZeros;
            m_Summary.Style = this.Style.Copy() as BasicStyle;
            m_Summary._TitleFormat = this._TitleFormat;
            m_Summary._FollowSummary = this._FollowSummary;

            m_Summary._AverageType = this._AverageType;

            m_Summary.ColumnWidth = this._ColumnWidth;

            m_Summary.ColumnIndex = this.ColumnIndex;  //Added this code at 2008-12-24 9:44:15@Simon

            if (this._DenominatorFilter != null)
            {
                m_Summary._DenominatorFilter = this._DenominatorFilter.Copy();
            }

            m_Summary._SepratorBefore = this._SepratorBefore;

            if (this._BrotherSummaries != null)
            {
                m_Summary._BrotherSummaries = this._BrotherSummaries.CopyStructure();
            }         

            m_Summary._ShowSymbol = this._ShowSymbol;	//Modified at 2009-2-1 14:03:04@Scott

            m_Summary.DecimalSpace = this.DecimalSpace;  //Added this code at 2009-2-1 15:47:49@Simon

            m_Summary.ColorNeedChange = this.ColorNeedChange;	//Modified at 2009-2-11 16:19:36@Scott

            m_Summary._ChartBase = this._ChartBase;

            m_Summary._ChartStyle = this.ChartStyle.Copy();

            m_Summary.Tag = this.Tag;

            m_Summary._IsDecimalData = this._IsDecimalData;

            return m_Summary;
        }

        public virtual void Apply(GroupSummary groupSummary)
        {
            if (groupSummary == null) return;
            if (groupSummary._Filter != null)
            {
                this._Filter = groupSummary._Filter.Copy();
            }
            this._RelatedFieldName = groupSummary._RelatedFieldName;
            this._SummaryID = groupSummary._SummaryID;
            this._SummaryType = groupSummary._SummaryType;
            this._Result = groupSummary._Result;
            this._SummaryTitle = groupSummary._SummaryTitle;
            this._ShowZeros = groupSummary._ShowZeros;
            this.Style = groupSummary.Style.Copy() as BasicStyle;
            this._TitleFormat = groupSummary._TitleFormat;
            this._FollowSummary = groupSummary._FollowSummary;

            this._AverageType = groupSummary._AverageType;

            this.ColumnWidth = groupSummary._ColumnWidth;

            this.ColumnIndex = groupSummary.ColumnIndex;  //Added this code at 2008-12-24 9:44:15@Simon

            if (groupSummary._DenominatorFilter != null)
            {
                this._DenominatorFilter = groupSummary._DenominatorFilter.Copy();
            }

            this._SepratorBefore = groupSummary._SepratorBefore;

            if (groupSummary._BrotherSummaries != null)
            {
                this._BrotherSummaries = groupSummary._BrotherSummaries.CopyStructure();
            }

            this._ShowSymbol = groupSummary._ShowSymbol;	//Modified at 2009-2-1 14:03:04@Scott

            this.DecimalSpace = groupSummary.DecimalSpace;  //Added this code at 2009-2-1 15:47:49@Simon

            this.ColorNeedChange = groupSummary.ColorNeedChange;	//Modified at 2009-2-11 16:19:36@Scott

            this._ChartBase = groupSummary._ChartBase;

            this._ChartStyle = groupSummary.ChartStyle.Copy();

            this.Tag = groupSummary.Tag;

            this._IsDecimalData = groupSummary._IsDecimalData;
            
        }

        public override string ToString()
        {
            return string.Format("{2}:{0}({1})", this._SummaryType, this._RelatedFieldName, this._SummaryTitle);
        }
        #endregion

        public void SetRowIndicators(Int32Collection intRows)
        {
            this._RowIndicators = intRows;
        }

        #region Serialization By Macro 2008-12-11 16:00:06
        virtual public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            //			info.AddValue("ParentGroupInfo",ParentGroupInfo,typeof(Webb.Reports.ExControls.Data.GroupInfo));
            info.AddValue("_ColumnWidth", _ColumnWidth, typeof(int));
            info.AddValue("_SummaryType", _SummaryType, typeof(Webb.Reports.ExControls.Data.SummaryTypes));
            //			info.AddValue("_Result",_Result,typeof(object));
            info.AddValue("_SummaryID", _SummaryID, typeof(int));
            info.AddValue("_Filter", _Filter, typeof(Webb.Data.DBFilter));
            info.AddValue("_RelatedFieldName", _RelatedFieldName, typeof(string));
            info.AddValue("_SummaryTitle", _SummaryTitle, typeof(string));
            //			info.AddValue("_RowIndicators",_RowIndicators,typeof(Webb.Collections.Int32Collection));
            info.AddValue("_Style", _Style, typeof(Webb.Reports.ExControls.BasicStyle));
            //			info.AddValue("_ColumnIndex",_ColumnIndex,typeof(int));
            info.AddValue("_ShowZeros", _ShowZeros, typeof(bool));
            info.AddValue("_TitleFormat", _TitleFormat, typeof(System.Drawing.StringFormatFlags));
            info.AddValue("_FollowSummary", _FollowSummary, typeof(string));
            info.AddValue("_DenominatorFilter", _DenominatorFilter, typeof(Webb.Data.DBFilter));
            info.AddValue("_ShowSymbol", _ShowSymbol);	//Modified at 2009-2-1 14:05:34@Scott
            info.AddValue("_DecimalSpace", _DecimalSpace); //Added this code at 2009-2-1 15:48:24@Simon
            info.AddValue("_ColorNeedChange", _ColorNeedChange);	//Modified at 2009-2-11 16:20:12@Scott    

            info.AddValue("_ChartStyle", this._ChartStyle, typeof(ChartSummaryInformation));	//Modified at 2009-2-11 16:20:12@Scott   

            info.AddValue("_Tag", this._Tag);	//Modified at 2009-2-11 16:20:12@Scott 

            info.AddValue("_AverageType", _AverageType, typeof(AverageType));
            info.AddValue("_SepratorBefore", this._SepratorBefore, typeof(string));

            info.AddValue("_IsDecimalData", this._IsDecimalData);

            info.AddValue("_BrotherSummaries", this._BrotherSummaries, typeof(GroupSummaryCollection));           

            
        }

        public GroupSummary(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            try
            {
                _SepratorBefore = info.GetString("_SepratorBefore");
            }
            catch
            {
                _SepratorBefore = "[Space]";
            }
            try
            {
                _BrotherSummaries = (GroupSummaryCollection)info.GetValue("_BrotherSummaries", typeof(GroupSummaryCollection));
            }
            catch
            {
                _BrotherSummaries = new GroupSummaryCollection();
            }

            try
            {
                _AverageType = (AverageType)info.GetValue("_AverageType", typeof(AverageType));
            }
            catch
            {
                _AverageType = AverageType.Mean;
            }
            try
            {
                _RelatedFieldName = (string)info.GetValue("_RelatedFieldName", typeof(string));
            }
            catch
            {
                _RelatedFieldName = string.Empty;
            }
            try
            {
                _Tag = info.GetValue("_Tag", typeof(object));

                #region Convert SummaryItemDescription
                if (_Tag != null && _Tag.GetType() == typeof(string))
                {
                    string strSummaryTag = _Tag.ToString();

                    strSummaryTag = strSummaryTag.Replace("Gain", "{0}");

                    if (strSummaryTag == "Frequency(EFF)")
                    {
                        strSummaryTag = "Frequency(Offensive EFF)";
                    }
                    else if (strSummaryTag == "Percent(EFF)")
                    {
                        strSummaryTag = "Percent(Offensive EFF)";   //Eff
                    }

                    _Tag =new WebbRepWizard.Data.SummaryItemDescription(strSummaryTag, _RelatedFieldName);
                }
                #endregion
            }
            catch
            {
                _Tag = null;
            }
            try
            {
                _ColumnWidth = (int)info.GetValue("_ColumnWidth", typeof(int));
            }
            catch
            {
            }
            try
            {
                _SummaryType = (Webb.Reports.ExControls.Data.SummaryTypes)info.GetValue("_SummaryType", typeof(Webb.Reports.ExControls.Data.SummaryTypes));
            }
            catch
            {
            }
            //			try
            //			{
            //				_Result=(object)info.GetValue("_Result",typeof(object));
            //			}
            //			catch
            //			{
            //			}
            try
            {
                _SummaryID = (int)info.GetValue("_SummaryID", typeof(int));
            }
            catch
            {
            }
            try
            {
                _Filter = (Webb.Data.DBFilter)info.GetValue("_Filter", typeof(Webb.Data.DBFilter));

                this._Filter = AdvFilterConvertor.GetAdvFilter(DataProvider.VideoPlayBackManager.AdvReportFilters, this._Filter);    //2009-4-29 11:37:37@Simon Add UpdateAdvFilter
            }
            catch
            {

            }           
            try
            {
                _SummaryTitle = (string)info.GetValue("_SummaryTitle", typeof(string));
            }
            catch
            {
            }
            //			try
            //			{
            //				_RowIndicators=(Webb.Collections.Int32Collection)info.GetValue("_RowIndicators",typeof(Webb.Collections.Int32Collection));
            //			}
            //			catch
            //			{
            //			}
            try
            {
                _Style = (Webb.Reports.ExControls.BasicStyle)info.GetValue("_Style", typeof(Webb.Reports.ExControls.BasicStyle));
            }
            catch
            {
            }
            //			try
            //			{
            //				_ColumnIndex=(int)info.GetValue("_ColumnIndex",typeof(int));
            //			}
            //			catch
            //			{
            //			}
            try
            {
                _ShowZeros = (bool)info.GetValue("_ShowZeros", typeof(bool));
            }
            catch
            {
            }
            try
            {
                _TitleFormat = (System.Drawing.StringFormatFlags)info.GetValue("_TitleFormat", typeof(System.Drawing.StringFormatFlags));
            }
            catch
            {
            }
            try
            {
                _FollowSummary = (string)info.GetValue("_FollowSummary", typeof(string));
            }
            catch
            {
            }
            try
            {
                _DenominatorFilter = (Webb.Data.DBFilter)info.GetValue("_DenominatorFilter", typeof(Webb.Data.DBFilter));

                this._DenominatorFilter = AdvFilterConvertor.GetAdvFilter(DataProvider.VideoPlayBackManager.AdvReportFilters, this._DenominatorFilter);    //2009-4-29 11:37:37@Simon Add UpdateAdvFilter
            }
            catch
            {
            }
            try	//Modified at 2009-2-1 14:05:00@Scott
            {
                _ShowSymbol = info.GetString("_ShowSymbol");
            }
            catch
            {
                _ShowSymbol = string.Empty;
            }
            try	//Added this code at 2009-2-1 15:49:49@Simon
            {
                _DecimalSpace = info.GetSByte("_DecimalSpace");
            }
            catch
            {
                _DecimalSpace = -1;
            }
            try
            {
                _ColorNeedChange = info.GetBoolean("_ColorNeedChange");
            }
            catch
            {
                _ColorNeedChange = false;
            }
            try
            {
                this._ChartStyle = (Webb.Reports.ExControls.Data.ChartSummaryInformation)info.GetValue("_ChartStyle", typeof(Webb.Reports.ExControls.Data.ChartSummaryInformation));
            }
            catch
            {
                _ChartStyle = new ChartSummaryInformation();
            }
            try
            {
                this._IsDecimalData = info.GetBoolean("_IsDecimalData");
            }
            catch
            {
                _IsDecimalData = false;
            }
        }
        #endregion

    }
    #endregion

    #region public class GroupSummaryCollection
    //Wu.Country@2007-11-14 16:26 added this region.
    [Serializable]
    public class GroupSummaryCollection : CollectionBase
    {
        public GroupSummaryCollection()
        {
        }

        public GroupSummaryCollection CopyStructure()
        {
            GroupSummaryCollection m_Summaries = new GroupSummaryCollection();
            // TODO: implement
            foreach (GroupSummary m_summary in this)
            {
                m_Summaries.Add(m_summary.Copy());
            }
            return m_Summaries;
        }

        public GroupSummary this[int i_index]
        {
            get
            {
                return this.InnerList[i_index] as GroupSummary;
            }
            set
            {
                this.InnerList[i_index] = value;
            }
        }

        public int Add(GroupSummary i_Summary)
        {
            i_Summary.SummaryID = this.InnerList.Add(i_Summary);
            return i_Summary.SummaryID;
        }
    }
    #endregion

    #region public class SummaryBand
    /*Descrition:   */
    //Wu.Country@2007-11-15 05:18 PM added this class.
    [Serializable]
    public class SummaryBand //: ISerializable
    {
        public SummaryBand()
        {

        }

        private string _Title;
        private GroupSummaryCollection _Summaries;

        public string Title
        {
            get
            {
                return _Title;
            }
            set
            {
                if (this._Title != value)
                    this._Title = value;
            }
        }

        [Browsable(false)]
        public GroupSummaryCollection Summaries
        {
            get
            {
                if (_Summaries == null) this._Summaries = new GroupSummaryCollection();
                return _Summaries;
            }
            set
            {
                if (this._Summaries != value)
                    this._Summaries = value;
            }
        }

        //		#region Serialization By Macro 2008-12-11 16:03:45
        //		public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        //		{
        //			info.AddValue("_Title",_Title,typeof(string));
        //			info.AddValue("_Summaries",_Summaries,typeof(Webb.Reports.ExControls.Data.GroupSummaryCollection));
        //		}
        //
        //		public SummaryBand(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        //		{
        //			try
        //			{
        //				_Title=(string)info.GetValue("_Title",typeof(string));
        //			}
        //			catch
        //			{
        //			}
        //			try
        //			{
        //				_Summaries=(Webb.Reports.ExControls.Data.GroupSummaryCollection)info.GetValue("_Summaries",typeof(Webb.Reports.ExControls.Data.GroupSummaryCollection));
        //			}
        //			catch
        //			{
        //			}
        //		}
        //		#endregion
    }
    #endregion

    #region public class SummaryBandCollection
    /*Descrition:   */
    [Serializable]
    public class SummaryBandCollection : CollectionBase
    {
        //Wu.Country@2007-11-15 05:20:54 PM added this collection.
        //Fields
        //Properties
        public SummaryBand this[int i_Index]
        {
            get { return this.InnerList[i_Index] as SummaryBand; }
            set { this.InnerList[i_Index] = value; }
        }
        //ctor
        public SummaryBandCollection() { }
        //Methods
        public int Add(SummaryBand i_Object)
        {
            return this.InnerList.Add(i_Object);
        }
        public void Remove(SummaryBand i_Obj)
        {
            this.InnerList.Remove(i_Obj);
        }
    }
    #endregion	
}