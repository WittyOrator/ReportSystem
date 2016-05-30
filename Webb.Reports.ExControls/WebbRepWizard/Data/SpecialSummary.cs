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


using Webb;
using Webb.Data;
using Webb.Utilities;
using Webb.Collections;
using Webb.Reports;
using Webb.Reports.ExControls.Data;
using System.Collections.Generic;

namespace Webb.Reports.ExControls.WebbRepWizard.Data
{
    #region SpecialSummary
    //[Serializable]
    //public class SpecialSummary : ISerializable
    //{
    //    protected string _Name;
       

    //    public SpecialSummary()
    //    {
    //        _Name = "Frequency";
    //    }
    //    public ArrayList GetArrayList()
    //    {
    //        ArrayList arrList = new ArrayList();

    //        arrList.Add("Frequency");
    //        arrList.Add("Percent");
    //        switch (Webb.Reports.ReportWizard.WizardInfo.ReportSetting.WizardEnviroment.ProductType)
    //        {
    //            case Webb.Reports.DataProvider.WebbDBTypes.WebbAdvantageFootball:
    //            case Webb.Reports.DataProvider.WebbDBTypes.WebbVictoryFootball:
    //                arrList.Add("Total(Gain)");
    //                arrList.Add("Average(Gain)");
    //                arrList.Add("Median(Gain)");
    //                arrList.Add("Mode(Gain)");
    //                arrList.Add("Frequency(Offensive EFF)");   //Eff
    //                arrList.Add("Percent(Offensive EFF)");    //Eff
    //                arrList.Add("Frequency(Defensive EFF)");   //Eff
    //                arrList.Add("Percent(Defensive EFF)");    //Eff
    //                arrList.Add("Frequency(Run)");
    //                arrList.Add("Percent(Run)");
    //                arrList.Add("Frequency(Pass)");
    //                arrList.Add("Percent(Pass)");
    //                arrList.Add("Total(Gain)(Run)");
    //                arrList.Add("Total(Gain)(Pass)");
    //                arrList.Add("Average(Gain)(Run)");
    //                arrList.Add("Average(Gain)(Pass)");
    //                arrList.Add("Pie3dChart(R/P)");
    //                arrList.Add("BarChart(R/P)");
    //                arrList.Add("HorizonBar3DChart(R/P)");
    //                break;
           
    //            default:                    
    //                string type = Webb.Reports.ReportWizard.WizardInfo.ReportSetting.WizardEnviroment.ProductType.ToString().ToLower();

    //                //if (type.StartsWith("webbvictory"))
    //                //{
    //                //    arrList.Add("Average(Grade)");
    //                //    arrList.Add("Total(Grade)");
    //                //}

    //                break;


    //        }
           
    //        return arrList;
    //    }
    //    public ArrayList GetSimpleArrayList()
    //    {
    //        ArrayList arrList = new ArrayList();

    //        arrList.Add("Frequency");
    //        arrList.Add("Percent");
    //        switch (Webb.Reports.ReportWizard.WizardInfo.ReportSetting.WizardEnviroment.ProductType)
    //        {
    //            case Webb.Reports.DataProvider.WebbDBTypes.WebbAdvantageFootball:
    //            case Webb.Reports.DataProvider.WebbDBTypes.WebbVictoryFootball:
    //                arrList.Add("Total(Gain)");
    //                arrList.Add("Average(Gain)");
    //                arrList.Add("Median(Gain)");
    //                arrList.Add("Mode(Gain)");
    //                arrList.Add("Frequency(Run)");
    //                arrList.Add("Percent(Run)");
    //                arrList.Add("Frequency(Pass)");
    //                arrList.Add("Percent(Pass)");
    //                arrList.Add("Total(Gain)(Run)");
    //                arrList.Add("Total(Gain)(Pass)");
    //                arrList.Add("Average(Gain)(Run)");
    //                arrList.Add("Average(Gain)(Pass)");
    //                break;         
    //            default:
    //                string type = Webb.Reports.ReportWizard.WizardInfo.ReportSetting.WizardEnviroment.ProductType.ToString().ToLower();

    //                //if (type.StartsWith("webbvictory"))
    //                //{
    //                //    arrList.Add("Average(Grade)");
    //                //    arrList.Add("Total(Grade)");
    //                //}
    //                break;
    //        }

    //        return arrList;
    //    }

    //    private Webb.Data.DBFilter CreateRunPassFilter(bool flag)
    //    {
    //        Webb.Data.DBFilter filter = new Webb.Data.DBFilter();

    //        Webb.Data.DBCondition m_Condition = new Webb.Data.DBCondition();

    //        m_Condition.ColumnName = "Run/Pass";

    //        m_Condition.FilterType = Webb.Data.FilterTypes.StrStartWith;

    //        if (flag)
    //        {
    //            m_Condition.Value = "r";
    //        }
    //        else
    //        {
    //            m_Condition.Value = "p";
    //        }

    //        m_Condition.FollowedOperand = FilterOperands.And;

    //        filter.Add(m_Condition);

    //        return filter;

    //    }
    //    private int MeasureWidth(Graphics g, string strField)
    //    {
    //        Font font = new Font("Tahoma", 10);

    //        SizeF szfText = g.MeasureString(strField, font);

    //        return (int)(szfText.Width*Webb.Utility.ConvertCoordinate)+2;
    //    }


    //    #region  Get Summary Name & Filter
    //    public string GetSummaryName(GroupSummary summary)
    //    {
    //        if (summary.Tag != null)
    //        {
    //            string strSummary=summary.Tag.ToString();

    //            if(strSummary=="Frequency(EFF)")
    //            {                   
                    
    //                strSummary="Frequency(Offensive EFF)";
    //            }
    //            else if(strSummary=="Percent(EFF)")
    //            {
    //                 strSummary="Percent(Offensive EFF)";   //Eff
    //            }

    //            return strSummary;
    //        }

    //        #region Old Version
    //        switch (summary.ColumnHeading)
    //        {
    //            case "#":
    //            default:
    //                return "Frequency";
    //            case "%":
    //                return "Percent";

    //            case "GAIN TOT":
    //            case "GAIN\r\nTOT":
    //                return "Total(Gain)";

    //            case "GAIN AVG":
    //            case "GAIN\r\nAVG":
    //                return "Average(Gain)";

    //            case "GAIN MEDIAN":
    //            case "GAIN\r\nMEDIAN":
    //                return "Median(Gain)";

    //            case "GAIN MODE":
    //            case "GAIN\r\nMODE":
    //                return "Mode(Gain)";
    //            case "Off EFF #":
    //            case "EFF #":
    //                return "Frequency(Offensive EFF)";   //Eff
    //            case "Off EFF %":
    //            case "EFF %":
    //                return "Percent(Offensive EFF)";   //Eff
    //            case "Def EFF #":
    //                return "Frequency(Defensive EFF)";   //Eff
    //            case "Def EFF %":
    //                return "Percent(Defensive EFF)";   //Eff

    //            case "RUN":
    //                return "Frequency(Run)";

    //            case "RUN %":
    //                return "Percent(Run)";

    //            case "PASS":
    //                return "Frequency(Pass)";

    //            case "PASS %":
    //                return "Percent(Pass)";

    //            case "RUN TOT":
    //            case "RUN\r\nTOT":
    //                return "Total(Gain)(Run)";

    //            case "PASS TOT":
    //            case "PASS\r\nTOT":
    //                return "Total(Gain)(Pass)";

    //            case "RUN AVG":
    //            case "RUN\r\nAVG":
    //                return "Average(Gain)(Run)";

    //            case "PASS AVG":
    //            case "PASS\r\nAVG":
    //                return "Average(Gain)(Pass)";

    //            case "R/P":
    //                return summary.Tag.ToString();

    //        }
    //         #endregion

    //    }

    //    public DBFilter GetSummaryFilter(GroupSummary summary)
    //    {
    //        string name=GetSummaryName(summary);

    //        DBFilter filter=new DBFilter();

    //        switch (name)
    //        {
    //            case "Frequency(Run Filter)":
    //            case "Frequency(Run)":
    //            case "Percent(Run Filter)":
    //            case "Percent(Run)":
    //            case "Average(Gain)(Run Filter)":
    //            case "Average(Gain)(Run)":
    //            case "Total(Gain)(Run Filter)":
    //            case "Total(Gain)(Run)":  
    //                filter = this.CreateRunPassFilter(true);
    //                break;

    //            case "Frequency(Pass Filter)":
    //            case "Frequency(Pass)":
    //            case "Percent(Pass Filter)":
    //            case "Percent(Pass)":
    //            case "Total(Gain)(Pass Filter)":
    //            case "Total(Gain)(Pass)":
    //            case "Average(Gain)(Pass Filter)":
    //            case "Average(Gain)(Pass)":
    //                filter = this.CreateRunPassFilter(false);
    //                break;                     
    //        }
    //        return filter;             


    //    }
    //    #endregion

    //    public GroupSummary GetGroupSummary(string name)
    //    {
    //        GroupSummary m_Summary = new GroupSummary();

    //        //08-20-2008@Simon

    //        ColumnStyleCollection columnstyles = Webb.Reports.ExControls.Data.ColumnManager.PublicColumnStyles.ColumnStyles;

    //        Image image = new Bitmap(100, 100);

    //        Graphics g = Graphics.FromImage(image);

    //        AdvFilterConvertor filterConvertor = new AdvFilterConvertor();

    //        ScFilterList filterList = Webb.Reports.DataProvider.VideoPlayBackManager.AdvReportFilters;

    //        switch (name)
    //        {
    //            case "Frequency":
    //            default:
    //                m_Summary.SummaryType = SummaryTypes.Frequence;
    //                m_Summary.ColumnHeading = "#";
    //                break;

    //            case "Percent":
    //                m_Summary.SummaryType = SummaryTypes.GroupPercent;
    //                m_Summary.ColumnHeading = "%";
    //                break;

    //            case "Total(Grade)":
    //                m_Summary.SummaryType = SummaryTypes.Total;

    //                m_Summary.Field = "Grade";

    //                m_Summary.ColumnHeading = "GRADE\r\nTOT";

    //                if (columnstyles[m_Summary.Field] != null)
    //                {
    //                    int width = this.MeasureWidth(g, m_Summary.ColumnHeading);

    //                    width = Math.Max(columnstyles[m_Summary.Field].ColumnWidth, width);

    //                    m_Summary.ColumnWidth = width;
    //                }
    //                break;
          
    //            case "Average(Grade)":
    //                m_Summary.SummaryType = SummaryTypes.Average;

    //                m_Summary.AverageType = AverageType.Mean;

    //                m_Summary.Field = "Gain";

    //                m_Summary.ColumnHeading = "GRADE\r\nAVG";

    //                if (columnstyles[m_Summary.Field] != null)
    //                {
    //                    int width = this.MeasureWidth(g, m_Summary.ColumnHeading);

    //                    width = Math.Max(columnstyles[m_Summary.Field].ColumnWidth, width);

    //                    m_Summary.ColumnWidth = width;
    //                }
    //                break;
    //            case "Total(Gain)":
    //                m_Summary.SummaryType = SummaryTypes.Total;

    //                m_Summary.Field = "Gain";

    //                m_Summary.ColumnHeading = "GAIN\r\nTOT";

    //                if (columnstyles[m_Summary.Field] != null)
    //                {
    //                    int width = this.MeasureWidth(g, m_Summary.ColumnHeading);

    //                    width=Math.Max(columnstyles[m_Summary.Field].ColumnWidth,width);

    //                    m_Summary.ColumnWidth = width;
    //                }

    //                break;

    //            case "Average(Gain)":
    //                m_Summary.SummaryType = SummaryTypes.Average;

    //                m_Summary.AverageType = AverageType.Mean;

    //                m_Summary.Field = "Gain";

    //                m_Summary.ColumnHeading = "GAIN\r\nAVG";

    //                if (columnstyles[m_Summary.Field] != null)
    //                {
    //                    int width = this.MeasureWidth(g, m_Summary.ColumnHeading);

    //                    width = Math.Max(columnstyles[m_Summary.Field].ColumnWidth, width);

    //                    m_Summary.ColumnWidth = width;
    //                }
    //                break;
    //            case "Median(Gain)":
    //                m_Summary.SummaryType = SummaryTypes.Average;

    //                m_Summary.AverageType = AverageType.Median;

    //                m_Summary.Field = "Gain";

    //                m_Summary.ColumnHeading = "GAIN\r\nMEDIAN";

    //                if (columnstyles[m_Summary.Field] != null)
    //                {
    //                    int width = this.MeasureWidth(g, m_Summary.ColumnHeading);

    //                    width = Math.Max(columnstyles[m_Summary.Field].ColumnWidth, width);

    //                    m_Summary.ColumnWidth = width;
    //                }
    //                break;
    //            case "Mode(Gain)":
    //                m_Summary.SummaryType = SummaryTypes.Average;

    //                m_Summary.AverageType = AverageType.Mode;

    //                m_Summary.Field = "Gain";

    //                m_Summary.ColumnHeading = "GAIN\r\nMODE";

    //                if (columnstyles[m_Summary.Field] != null)
    //                {
    //                    int width = this.MeasureWidth(g, m_Summary.ColumnHeading);

    //                    width = Math.Max(columnstyles[m_Summary.Field].ColumnWidth, width);

    //                    m_Summary.ColumnWidth = width;
    //                }
    //                break;
    //            case "Frequency(Offensive EFF)":
    //            case "Frequency(EFF)": 
    //                m_Summary.SummaryType = SummaryTypes.Frequence;                   

    //                m_Summary.Filter = filterConvertor.GetEffFilter(filterList);

    //                m_Summary.ColorNeedChange = true;
                    
    //                m_Summary.ColumnHeading = "Off EFF #";

    //                break;
               
    //            case "Percent(Offensive EFF)":   //Eff
    //            case "Percent(EFF)":

    //                m_Summary.SummaryType = SummaryTypes.RelatedPercent;                   

    //                m_Summary.Filter = filterConvertor.GetEffFilter(filterList);

    //                m_Summary.ColorNeedChange = true;                  

    //                 m_Summary.ColumnHeading = "Off EFF %";

    //                break;                  
    //            case "Frequency(Defensive EFF)":
    //                m_Summary.SummaryType = SummaryTypes.Frequence;

    //                m_Summary.Filter = filterConvertor.GetDefenseEffFilter(filterList);

    //                m_Summary.ColorNeedChange = true;

    //                m_Summary.ColumnHeading = "Def EFF #";

    //                break;

    //            //case "EFF%":
    //            case "Percent(Defensive EFF)":   //Eff

    //                m_Summary.SummaryType = SummaryTypes.RelatedPercent;

    //                m_Summary.Filter = filterConvertor.GetDefenseEffFilter(filterList);

    //                m_Summary.ColorNeedChange = true;

    //                m_Summary.ColumnHeading = "Def EFF %";

    //                break;

    //            case "Frequency(Run Filter)":
    //            case "Frequency(Run)":
    //                m_Summary.SummaryType = SummaryTypes.Frequence;

    //                m_Summary.Filter = this.CreateRunPassFilter(true);

    //                m_Summary.ColorNeedChange = true;

    //                m_Summary.Style.BackgroundColor = Color.Tan;

    //                m_Summary.ColumnHeading = "RUN";

    //                break;

    //            case "Percent(Run Filter)":
    //            case "Percent(Run)":
    //                m_Summary.SummaryType = SummaryTypes.GroupPercent;

    //                m_Summary.ColorNeedChange = true;

    //                m_Summary.Style.BackgroundColor = Color.Tan;

    //                m_Summary.Filter = this.CreateRunPassFilter(true);

    //                m_Summary.ColumnHeading = "RUN %";
    //                break;

    //            case "Frequency(Pass Filter)":
    //            case "Frequency(Pass)":

    //                m_Summary.SummaryType = SummaryTypes.Frequence;

    //                m_Summary.ColorNeedChange = true;

    //                m_Summary.Style.BackgroundColor = Color.LightGreen;

    //                m_Summary.Filter = this.CreateRunPassFilter(false);

    //                m_Summary.ColumnHeading = "PASS";
    //                break;

    //            case "Percent(Pass Filter)":
    //            case "Percent(Pass)":
    //                m_Summary.SummaryType = SummaryTypes.GroupPercent;

    //                m_Summary.ColorNeedChange = true;

    //                m_Summary.Style.BackgroundColor = Color.LightGreen;

    //                m_Summary.Filter = this.CreateRunPassFilter(false);

    //                m_Summary.ColumnHeading = "PASS %";

    //                break;

    //            case "Total(Gain)(Run Filter)":
    //            case "Total(Gain)(Run)":
    //                m_Summary.SummaryType = SummaryTypes.Total;

    //                m_Summary.Field = "Gain";

    //                m_Summary.ColorNeedChange = true;

    //                m_Summary.Style.BackgroundColor = Color.Tan;

    //                m_Summary.Filter = this.CreateRunPassFilter(true);

    //                m_Summary.ColumnHeading = "RUN\r\nTOT";

    //                if (columnstyles[m_Summary.Field] != null)
    //                {
    //                    m_Summary.ColumnWidth = columnstyles[m_Summary.Field].ColumnWidth;
    //                }

    //                break;
    //            case "Total(Gain)(Pass Filter)":
    //            case "Total(Gain)(Pass)":
    //                m_Summary.SummaryType = SummaryTypes.Total;

    //                m_Summary.Field = "Gain";

    //                m_Summary.ColorNeedChange = true;

    //                m_Summary.Style.BackgroundColor = Color.LightGreen;

    //                m_Summary.Filter = this.CreateRunPassFilter(false);

    //                m_Summary.ColumnHeading = "PASS\r\nTOT";

    //                if (columnstyles[m_Summary.Field] != null)
    //                {
    //                    int width = this.MeasureWidth(g, m_Summary.ColumnHeading);

    //                    width = Math.Max(columnstyles[m_Summary.Field].ColumnWidth, width);

    //                    m_Summary.ColumnWidth = width;
    //                }


    //                break;
    //            case "Average(Gain)(Run Filter)":
    //            case "Average(Gain)(Run)":
    //                m_Summary.SummaryType = SummaryTypes.Average;

    //                m_Summary.Field = "Gain";

    //                m_Summary.ColorNeedChange = true;

    //                m_Summary.Style.BackgroundColor = Color.Tan;

    //                m_Summary.Filter = this.CreateRunPassFilter(true);

    //                m_Summary.ColumnHeading = "RUN\r\nAVG";

    //                if (columnstyles[m_Summary.Field] != null)
    //                {
    //                    int width = this.MeasureWidth(g, m_Summary.ColumnHeading);

    //                    width = Math.Max(columnstyles[m_Summary.Field].ColumnWidth, width);

    //                    m_Summary.ColumnWidth = width;
    //                }

    //                break;
    //            case "Average(Gain)(Pass Filter)":
    //            case "Average(Gain)(Pass)":
    //                m_Summary.SummaryType = SummaryTypes.Average;

    //                m_Summary.Field = "Gain";

    //                m_Summary.ColorNeedChange = true;

    //                m_Summary.Style.BackgroundColor = Color.LightGreen;

    //                m_Summary.Filter = this.CreateRunPassFilter(false);

    //                m_Summary.ColumnHeading = "PASS\r\nAVG";

    //                if (columnstyles[m_Summary.Field] != null)
    //                {
    //                    int width = this.MeasureWidth(g, m_Summary.ColumnHeading);

    //                    width = Math.Max(columnstyles[m_Summary.Field].ColumnWidth, width);

    //                    m_Summary.ColumnWidth = width;
    //                }

    //                break;
    //            case "Pie3dChart(R/P)":
    //                m_Summary.SummaryType = SummaryTypes.PieChart;

    //                m_Summary.Field = "Run/Pass";

    //                m_Summary.ChartStyle.Chart3D = true;

    //                m_Summary.ColumnHeading = "R/P";

    //                break;

    //            case "BarChart(R/P)":
    //                m_Summary.SummaryType = SummaryTypes.BarChart;

    //                m_Summary.Field = "Run/Pass";

    //                m_Summary.ColumnHeading = "R/P";

    //                break;

    //            case "HorizonBar3DChart(R/P)":
    //                m_Summary.SummaryType = SummaryTypes.HorizonBarChart;

    //                m_Summary.ChartStyle.Chart3D = true;

    //                m_Summary.Field = "Run/Pass";

    //                m_Summary.ColumnHeading = "R/P";

    //                break;        

    //        }
    //        m_Summary.Tag = name;

    //        g.Dispose();

    //        image.Dispose();

    //        return m_Summary;
    //    }

    //    public string Name
    //    {
    //        get { return _Name; }
    //        set { _Name = value; }
    //    }


    //    public override string ToString()
    //    {
    //        return this._Name;
    //    }

    //    #region Serialization By Simon's Macro 2009-11-12 16:36:45
    //    public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
    //    {
    //        info.AddValue("_Name", _Name);


    //    }

    //    public SpecialSummary(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
    //    {
    //        try
    //        {
    //            _Name = info.GetString("_Name");
    //        }
    //        catch
    //        {
    //            _Name = "Frequece";
    //        }
    //    }
    //    #endregion

    //    #region Copy Function By Macro 2011-4-14 9:31:01
    //    public SpecialSummary Copy()
    //    {
    //        SpecialSummary thiscopy=new SpecialSummary();
    //        thiscopy._Name=this._Name;
    //        return thiscopy;
    //    }
    //    #endregion

    //}
    #endregion

    [Serializable]
    public class SummaryItemDescription : ISerializable
    {
        #region Auto Constructor By Macro 2011-5-26 9:22:23
		public SummaryItemDescription()
        {
			_Field=string.Empty;
			_Description=string.Empty;
			_Header=string.Empty;
            _ShowZero = true;
        }

        public SummaryItemDescription(string p_Description, string p_Field)
        {
            _Field = p_Field;

            _Description = p_Description;

            if (p_Field == "Gain")
            {
                _Header = "Yards";
            }
            else
            {
                _Header = p_Field;
            }
        }

        public SummaryItemDescription(string p_Header, string p_Description, string p_Field)
        {
			_Field=p_Field;
			_Description=p_Description;
			_Header=p_Header;
        }
		#endregion       
       

        private bool _IsChecked = false;
        private string _Field=string.Empty;
        private string _Description = string.Empty;
        private string _Header = string.Empty;
        private bool _ShowZero = true;
        private int _OrderIndex =0;
        private int _ColumnWidth = -1;
        private string _CustomHeader =null;


    
        public static string[] allSummaryList = new string[]{   "Frequency",
                                                                "Percent",
                                                                "FrequencyAllData",
                                                                "PercentAllData",
                                                                "Total({0})",
                                                                "Average({0})",
                                                                "Median({0})",
                                                                "Mode({0})",
                                                                "Frequency(Offensive EFF)",
                                                                "Percent(Offensive EFF)",                                                              
                                                                "Frequency(Offensive Run EFF)",
                                                                "Percent(Offensive Run EFF)",
                                                                "Frequency(Offensive Pass EFF)",
                                                                "Percent(Offensive Pass EFF)", 
                                                                "Frequency(Defensive EFF)",
                                                                "Percent(Defensive EFF)", 
                                                                "Frequency(Defensive Run EFF)",
                                                                "Percent(Defensive Run EFF)",
                                                                "Frequency(Defensive Pass EFF)",
                                                                "Percent(Defensive Pass EFF)",
                                                                "Frequency(Run)",
                                                                "Percent(Run)",
                                                                "Frequency(Pass)",
                                                                "Percent(Pass)",
                                                                "Total({0})(Run)",
                                                                "Total({0})(Pass)",
                                                                "Average({0})(Run)",
                                                                "Average({0})(Pass)",
                                                                "Pie3dChart(R/P)",
                                                                "BarChart(R/P)",
                                                                "HorizonBar3DChart(R/P)"
                                                            };

        //public static Hashtable allSummaryDefaultHeadersTable = new Hashtable();
        
        public static string[] allSummaryDefaultHeaders = new string[]{   "#",
                                                                "%",  
                                                                "AllData #",
                                                                "AllData %",
                                                                "{0}\r\nTOT",
                                                                "{0}\r\nAVG",
                                                                "{0}\r\nMEDIAN",
                                                                "{0}\r\nMODE",
                                                                "Off EFF #",
                                                                "Off EFF %",
                                                                "Off Run EFF #",   //Eff
                                                                "Off Run EFF %",    //Eff
                                                                "Off Pass EFF #",   //Eff
                                                                "Off Pass EFF %",    //Eff
                                                                "Def EFF #",
                                                                "Def EFF %",
                                                                "Def Run EFF #",   //Eff
                                                                "Def Run EFF %",    //Eff
                                                                "Def Pass EFF #",   //Eff
                                                                "Def Pass EFF %",    //Eff
                                                                "RUN",
                                                                "RUN %",
                                                                "PASS",
                                                                "PASS %",
                                                                "RUN\r\nTOT",
                                                                "PASS\r\nTOT",
                                                                "RUN\r\nAVG",
                                                                "PASS\r\nAVG",
                                                                "R/P",
                                                                "R/P",
                                                                "R/P"
                                                            };

        #region override Object Methods
        public override bool Equals(object obj)
        {
            if(!(obj is SummaryItemDescription))return false;

            return this.Description==(obj as SummaryItemDescription).Description;
        }
        public override int GetHashCode()
        {
            return this.Description.GetHashCode();
        }

        public override string ToString()
        {
            string strDescription = string.Format(_Description, _Field);

            return strDescription;
        }
        #endregion

        #region Function

          #region Revert()

            public void Revert()
            {         
                if (this.Description.IndexOf("{0}") >= 0)
                {
                    if (this.Description.StartsWith("Total"))
                    {
                        GroupDescription totalDescription = SummaryItemDescription.GetTotalDescription();

                        this.Field = totalDescription.Field;

                        this.Header = totalDescription.DefaultHeader;
                    }
                    else
                    {
                        GroupDescription avgDescription = SummaryItemDescription.GetAVGDescription();

                        this.Field = avgDescription.Field;

                        this.Header = avgDescription.DefaultHeader;
                    }
                }

                this.ShowZeros = false;

                this.OrderIndex = 0;

                this.CustomHeader = null;

                this._ColumnWidth = -1;

            }

          #endregion

          #region sub Functions To get Summary
            public Webb.Data.DBFilter CreateRunPassFilter()
            {
                Webb.Data.DBFilter filter = new Webb.Data.DBFilter();

                Webb.Data.DBCondition m_Condition = new Webb.Data.DBCondition();
                
                if(this.Description.EndsWith(")(Run)")||this.Description.EndsWith("Frequency(Run)")||this.Description.EndsWith("Percent(Run)")||this.Description.EndsWith("Run EFF)"))
                {
                       m_Condition.ColumnName = "Run/Pass";

                      m_Condition.FilterType = Webb.Data.FilterTypes.StrStartWith;

                      m_Condition.Value = "r";

                      m_Condition.FollowedOperand = FilterOperands.And;

                      filter.Add(m_Condition);

                }
                else if (this.Description.EndsWith(")(Pass)") || this.Description.EndsWith("Frequency(Pass)") || this.Description.EndsWith("Percent(Pass)") || this.Description.EndsWith("Pass EFF)"))
                {
                      m_Condition.Value = "p";

                      m_Condition.ColumnName = "Run/Pass";

                      m_Condition.FilterType = Webb.Data.FilterTypes.StrStartWith;

                      m_Condition.FollowedOperand = FilterOperands.And;

                      filter.Add(m_Condition);

                }
                return filter;
            }
            private int MeasureWidth(Graphics g, string strText,Font font)
            {
                //strText=strText.Replace("\r\n"," ");

                SizeF szfText = g.MeasureString(strText, font);

                int nWidth = (int)(szfText.Width * Webb.Utility.ConvertCoordinate);

                return  nWidth+2;
            }
            #endregion

            public GroupSummary ToSummary(Font font,bool isInPlayList)
            {
                GroupSummary m_Summary = new GroupSummary();

                //08-20-2008@Simon

                ColumnStyleCollection columnstyles = Webb.Reports.ExControls.Data.ColumnManager.PublicColumnStyles.ColumnStyles;

                Image image = new Bitmap(100, 100);

                Graphics g = Graphics.FromImage(image);

                AdvFilterConvertor filterConvertor = new AdvFilterConvertor();

                ScFilterList filterList = Webb.Reports.DataProvider.VideoPlayBackManager.AdvReportFilters;

                #region Intalize GroupSummary
                switch (this.Description)
                {
                    case "Frequency":
                    default:
                        m_Summary.SummaryType = SummaryTypes.Frequence;
                        if (isInPlayList)
                        {
                            m_Summary.ColumnHeading = "Frequency";
                        }
                        else
                        {
                            m_Summary.ColumnHeading = "#";
                        }
                        break;
                    case "FrequencyAllData":                    
                        m_Summary.SummaryType = SummaryTypes.FrequenceAllData;
                        if (isInPlayList)
                        {
                            m_Summary.ColumnHeading = "FrequencyAllData";
                        }
                        else
                        {
                            m_Summary.ColumnHeading = "AllData #";
                        }
                        break;

                    case "Percent":                       
                        m_Summary.SummaryType = SummaryTypes.GroupPercent;
                        if (isInPlayList)
                        {
                            m_Summary.ColumnHeading = "Percent";
                        }
                        else
                        {
                            m_Summary.ColumnHeading = "%";
                        }
                        break;
                    case "PercentAllData":                    
                        m_Summary.SummaryType = SummaryTypes.PercentAllData;
                        if (isInPlayList)
                        {
                            m_Summary.ColumnHeading = "PercentAllData";
                        }
                        else
                        {
                            m_Summary.ColumnHeading = "AllData %";
                        }
                        break;

                    case "Total({0})":
                        #region Total

                        m_Summary.SummaryType = SummaryTypes.Total;

                        m_Summary.Field = this.Field;

                        m_Summary.ColumnHeading = string.Format("TOTAL\r\n{0}", this.Header.ToUpper());
                    
                        if (columnstyles[m_Summary.Field] != null)
                        {
                            m_Summary.ColumnWidth = columnstyles[m_Summary.Field].ColumnWidth;
                        }            

                        #endregion
                        break;

                    case "Average({0})":

                        #region Average 
                        m_Summary.SummaryType = SummaryTypes.Average;

                        m_Summary.AverageType = AverageType.Mean;

                        m_Summary.Field = this.Field;

                        m_Summary.ColumnHeading = string.Format("AVERAGE\r\n{0}", this.Header.ToUpper());
                     
                        if (columnstyles[m_Summary.Field] != null)
                        {
                            m_Summary.ColumnWidth =columnstyles[m_Summary.Field].ColumnWidth;
                        }            

                        #endregion
                        break;
                    case "Median({0})":
                        #region Median
                        m_Summary.SummaryType = SummaryTypes.Average;

                        m_Summary.AverageType = AverageType.Median;

                        m_Summary.Field = this.Field;

                        m_Summary.ColumnHeading = string.Format("MEDIAN\r\n{0}", this.Header.ToUpper());
                      
                        if (columnstyles[m_Summary.Field] != null)
                        {
                            m_Summary.ColumnWidth = columnstyles[m_Summary.Field].ColumnWidth;
                        }            

                        #endregion
                        break;
                    case "Mode({0})":
                        #region Mode
                        m_Summary.SummaryType = SummaryTypes.Average;

                        m_Summary.AverageType = AverageType.Mode;                      

                        m_Summary.Field = this.Field;

                        m_Summary.ColumnHeading = string.Format("MODE\r\n{0}", this.Header.ToUpper());
                    
                        if (columnstyles[m_Summary.Field] != null)
                        {
                            m_Summary.ColumnWidth = columnstyles[m_Summary.Field].ColumnWidth;
                        }            
  
                        #endregion
                        break;
                    #region Old
                    //case "Frequency(Offensive EFF)":
                    //    #region Offensive  EFF #
                    //    m_Summary.SummaryType = SummaryTypes.Frequence;

                    //    m_Summary.Filter = filterConvertor.GetOffenseEffFilter(filterList);

                    //    m_Summary.ColorNeedChange = true;

                    //    m_Summary.ColumnHeading = "Off EFF #";
                    //    #endregion
                    //    break;

                    //case "Percent(Offensive EFF)":   //Eff
                    //    #region Offensive EFF %
                    //    m_Summary.SummaryType = SummaryTypes.RelatedPercent;

                    //    m_Summary.Filter = filterConvertor.GetOffenseEffFilter(filterList);

                    //    m_Summary.ColorNeedChange = true;

                    //    m_Summary.ColumnHeading = "Off EFF %";
                    //    #endregion
                    //    break;
                    //case "Frequency(Defensive EFF)":
                    //    #region Defensive EFF #
                    //    m_Summary.SummaryType = SummaryTypes.Frequence;

                    //    m_Summary.Filter = filterConvertor.GetDefenseEffFilter(filterList);

                    //    m_Summary.ColorNeedChange = true;

                    //    m_Summary.ColumnHeading = "Def EFF #";
                    //    #endregion
                    //    break;

                    ////case "EFF%":
                    //case "Percent(Defensive EFF)":   //Eff
                    //    #region Percent(Defensive EFF) %
                    //    m_Summary.SummaryType = SummaryTypes.RelatedPercent;

                    //    m_Summary.Filter = filterConvertor.GetDefenseEffFilter(filterList);

                    //    m_Summary.ColorNeedChange = true;

                    //    m_Summary.ColumnHeading = "Def EFF %";
                    //    #endregion
                    //    break;
                    #endregion

                  #region run/pass eff
                    
                    #region offensive  run/pass
                    case  "Frequency(Offensive EFF)":
                        #region Run/pass Off Eff #
                        m_Summary.SummaryType = SummaryTypes.Frequence;

                        m_Summary.Filter = filterConvertor.GetOffenseEffFilter(filterList);

                        m_Summary.ColorNeedChange = true;

                        m_Summary.ColumnHeading = "Off EFF #";
                        #endregion
                        break;
                    case  "Percent(Offensive EFF)":
                        #region Run/pass  Off Eff %
                        m_Summary.SummaryType = SummaryTypes.RelatedPercent;

                        m_Summary.Filter = filterConvertor.GetOffenseEffFilter(filterList);

                        m_Summary.ColorNeedChange = true;

                        m_Summary.ColumnHeading = "Off EFF %";
                        #endregion
                        break;
                                                            
                    case "Frequency(Offensive Run EFF)":
                        #region Offensive Run EFF #
                        m_Summary.SummaryType = SummaryTypes.Frequence;

                        m_Summary.Filter = filterConvertor.GetRunOffenseEffFilter(filterList);

                        m_Summary.ColorNeedChange = true;

                        m_Summary.ColumnHeading = "Off Run EFF #";
                        #endregion
                        break;

                    case "Percent(Offensive Run EFF)":   //Eff
                        #region Offensive Run EFF %
                        m_Summary.SummaryType = SummaryTypes.RelatedPercent;

                        m_Summary.Filter = filterConvertor.GetRunOffenseEffFilter(filterList);

                        m_Summary.DenominatorFilter = this.CreateRunPassFilter();

                        m_Summary.ColorNeedChange = true;

                        m_Summary.ColumnHeading = "Off Run EFF %";
                        #endregion
                        break;

                    case "Frequency(Offensive Pass EFF)":
                        #region Offensive Pass EFF #
                        m_Summary.SummaryType = SummaryTypes.Frequence;

                        m_Summary.Filter = filterConvertor.GetPassOffenseEffFilter(filterList);

                        m_Summary.ColorNeedChange = true;

                        m_Summary.ColumnHeading = "Off Pass EFF #";
                        #endregion
                        break;

                    case "Percent(Offensive Pass EFF)":   //Eff
                        #region Offensive Pass EFF %
                        m_Summary.SummaryType = SummaryTypes.RelatedPercent;

                        m_Summary.Filter = filterConvertor.GetPassOffenseEffFilter(filterList);

                        m_Summary.DenominatorFilter = this.CreateRunPassFilter();

                        m_Summary.ColorNeedChange = true;

                        m_Summary.ColumnHeading = "Off Pass EFF %";
                        #endregion
                        break;
                    #endregion

                    #region Defensive run/pass
                    case  "Frequency(Defensive EFF)":
                        #region Run/pass Def Eff #
                        m_Summary.SummaryType = SummaryTypes.Frequence;

                        m_Summary.Filter = filterConvertor.GetDefenseEffFilter(filterList);

                        m_Summary.ColorNeedChange = true;

                        m_Summary.ColumnHeading = "Def EFF #";
                        #endregion
                        break;

                    case "Percent(Defensive EFF)":
                        #region Run/pass  Def Eff %
                        m_Summary.SummaryType = SummaryTypes.RelatedPercent;

                        m_Summary.Filter = filterConvertor.GetDefenseEffFilter(filterList);

                        m_Summary.ColorNeedChange = true;

                        m_Summary.ColumnHeading = "Def EFF %";
                        #endregion
                        break;
                                
                    case "Frequency(Defensive Run EFF)":
                        #region Defensive Run EFF #
                        m_Summary.SummaryType = SummaryTypes.Frequence;

                        m_Summary.Filter = filterConvertor.GetRunDefenseEffFilter(filterList);

                        m_Summary.ColorNeedChange = true;

                        m_Summary.ColumnHeading = "Def Run EFF #";
                        #endregion
                        break;

                    //case "EFF%":
                    case "Percent(Defensive Run EFF)":   //Eff
                        #region Percent(Defensive Run EFF) %
                        m_Summary.SummaryType = SummaryTypes.RelatedPercent;

                        m_Summary.Filter = filterConvertor.GetRunDefenseEffFilter(filterList);

                        m_Summary.DenominatorFilter = this.CreateRunPassFilter();

                        m_Summary.ColorNeedChange = true;

                        m_Summary.ColumnHeading = "Def Run EFF %";
                        #endregion
                        break;
                    case "Frequency(Defensive Pass EFF)":
                        #region Defensive Pass EFF #
                        m_Summary.SummaryType = SummaryTypes.Frequence;

                        m_Summary.Filter = filterConvertor.GetPassDefenseEffFilter(filterList);

                        m_Summary.ColorNeedChange = true;

                        m_Summary.ColumnHeading = "Def Pass EFF #";
                        #endregion
                        break;

                    //case "EFF%":
                    case "Percent(Defensive Pass EFF)":   //Eff
                        #region Percent(Defensive Pass EFF) %
                        m_Summary.SummaryType = SummaryTypes.RelatedPercent;

                        m_Summary.Filter = filterConvertor.GetPassDefenseEffFilter(filterList);

                        m_Summary.DenominatorFilter = this.CreateRunPassFilter();

                        m_Summary.ColorNeedChange = true;

                        m_Summary.ColumnHeading = "Def Pass EFF %";
                        #endregion
                        break;
                    #endregion
                  #endregion

                  case "Frequency(Run)":
                        #region Run #
                        m_Summary.SummaryType = SummaryTypes.Frequence;

                        m_Summary.Filter = this.CreateRunPassFilter();

                        m_Summary.ColorNeedChange = true;

                        m_Summary.Style.BackgroundColor = Color.Tan;

                        m_Summary.ColumnHeading = "RUN";
                        #endregion
                        break;

                   case "Percent(Run)":
                        #region Run %
                        m_Summary.SummaryType = SummaryTypes.RelatedPercent;

                        m_Summary.ColorNeedChange = true;

                        m_Summary.Style.BackgroundColor = Color.Tan;

                        m_Summary.Filter = this.CreateRunPassFilter();

                        m_Summary.ColumnHeading = "RUN %";
                        #endregion
                        break;
                   case "Frequency(Pass)":
                        #region Pass #
                        m_Summary.SummaryType = SummaryTypes.Frequence;

                        m_Summary.ColorNeedChange = true;

                        m_Summary.Style.BackgroundColor = Color.LightGreen;

                        m_Summary.Filter = this.CreateRunPassFilter();

                        m_Summary.ColumnHeading = "PASS";
                        #endregion

                        break;

                   case "Percent(Pass)":
                        #region Pass %
                        m_Summary.SummaryType = SummaryTypes.RelatedPercent;

                        m_Summary.ColorNeedChange = true;

                        m_Summary.Style.BackgroundColor = Color.LightGreen;

                        m_Summary.Filter = this.CreateRunPassFilter();

                        m_Summary.ColumnHeading = "PASS %";

                        #endregion
                        break;

                  case "Total({0})(Run)":

                        #region Total Run

                        m_Summary.SummaryType = SummaryTypes.Total;

                        m_Summary.Field =this.Field;

                        m_Summary.ColorNeedChange = true;

                        m_Summary.Style.BackgroundColor = Color.Tan;

                        m_Summary.Filter = this.CreateRunPassFilter();

                        m_Summary.ColumnHeading = string.Format("RUN\r\n{0}", this.Header.ToUpper());
                       
                        if (columnstyles[m_Summary.Field] != null)
                        {
                            m_Summary.ColumnWidth =columnstyles[m_Summary.Field].ColumnWidth;
                        }            
                        #endregion

                        break;

                case "Total({0})(Pass)":

                        #region Total Pass 

                        m_Summary.SummaryType = SummaryTypes.Total;

                        m_Summary.Field = "Gain";

                        m_Summary.ColorNeedChange = true;

                        m_Summary.Style.BackgroundColor = Color.LightGreen;

                        m_Summary.Filter = this.CreateRunPassFilter();

                        m_Summary.ColumnHeading = string.Format("PASS\r\n{0}", this.Header.ToUpper());
                     
                        if (columnstyles[m_Summary.Field] != null)
                        {
                            m_Summary.ColumnWidth = columnstyles[m_Summary.Field].ColumnWidth;
                        }            
                        #endregion
                        break;                
                 case "Average({0})(Run)":
                        #region Averge Run
                        m_Summary.SummaryType = SummaryTypes.Average;

                        m_Summary.Field =this.Field;

                        m_Summary.ColorNeedChange = true;

                        m_Summary.Style.BackgroundColor = Color.Tan;

                        m_Summary.Filter = this.CreateRunPassFilter();

                        m_Summary.ColumnHeading = "RUN\r\nAVG";
                        
                        if (columnstyles[m_Summary.Field] != null)
                        {
                            m_Summary.ColumnWidth = columnstyles[m_Summary.Field].ColumnWidth;
                        }            

                        #endregion
                        break;
                   case "Average({0})(Pass)":
                        #region Average Pass
                        m_Summary.SummaryType = SummaryTypes.Average;

                        m_Summary.Field = this.Field;

                        m_Summary.ColorNeedChange = true;

                        m_Summary.Style.BackgroundColor = Color.LightGreen;

                        m_Summary.Filter = this.CreateRunPassFilter();

                        m_Summary.ColumnHeading = "PASS\r\nAVG";
                       
                        if (columnstyles[m_Summary.Field] != null)
                        {
                            m_Summary.ColumnWidth =columnstyles[m_Summary.Field].ColumnWidth;
                        }            

                        #endregion
                        break;                  
                  #region Chart
                   case "Pie3dChart(R/P)":
                        m_Summary.SummaryType = SummaryTypes.PieChart;

                        m_Summary.Field = "Run/Pass";

                        m_Summary.ChartStyle.Chart3D = true;

                        m_Summary.ColumnHeading = "R/P";

                        break;

                    case "BarChart(R/P)":
                        m_Summary.SummaryType = SummaryTypes.BarChart;

                        m_Summary.Field = "Run/Pass";

                        m_Summary.ColumnHeading = "R/P";

                        break;

                    case "HorizonBar3DChart(R/P)":
                        m_Summary.SummaryType = SummaryTypes.HorizonBarChart;

                        m_Summary.ChartStyle.Chart3D = true;

                        m_Summary.Field = "Run/Pass";

                        m_Summary.ColumnHeading = "R/P";

                        break;
                    #endregion

                }
                #endregion

                m_Summary.ColumnHeading = m_Summary.ColumnHeading.Trim("\r\n ".ToCharArray());

                m_Summary.ShowZeros = this.ShowZeros;

                if (this.CustomHeader != null)
                {
                    m_Summary.ColumnHeading = this.CustomHeader;
                }         

                int mearSureWidth = this.MeasureWidth(g, m_Summary.ColumnHeading, font);

                if (this.ColumnWidth < 0)
                {
                    m_Summary.ColumnWidth = Math.Max(mearSureWidth, m_Summary.ColumnWidth);
                }
                else
                {
                    m_Summary.ColumnWidth = this.ColumnWidth;
                }                    

                m_Summary.Tag = this.Copy();

                g.Dispose();

                image.Dispose();

                return m_Summary;
            }
        #endregion     

        #region Static Methods
            #region  Get Default Average/Total Field /Header
            public static GroupDescription GetTotalDescription()
            {
                GroupDescription groupDescription = new GroupDescription();

                if (Webb.Reports.ReportWizard.WizardInfo.ReportSetting.WizardEnviroment.IsAdvantageProduct)
                {
                    groupDescription.Field = "Gain";

                    groupDescription.DefaultHeader = "YARDS";
                }

                return groupDescription;

            }
            public static GroupDescription GetAVGDescription()
            {
                GroupDescription groupDescription = new GroupDescription();

                if (Webb.Reports.ReportWizard.WizardInfo.ReportSetting.WizardEnviroment.IsAdvantageProduct)
                {
                    groupDescription.Field = "Gain";

                    groupDescription.DefaultHeader = "GAIN";
                }

                return groupDescription;
            }
            #endregion

            #region  public static List<SummaryItemDescription> CreateIntitalSummaryList()
               public static List<SummaryItemDescription> CreateIntitalSummaryList()
               {
                    List<SummaryItemDescription> summaryList = new List<SummaryItemDescription>();

                    SummaryItemDescription summaryDescription = null;          
         
                    for(int i=0;i<allSummaryList.Length;i++)
                    {                 
                        string strDesciption=allSummaryList[i];

                        summaryDescription=new SummaryItemDescription();

                        summaryDescription.Description =strDesciption;  

                        if(strDesciption.IndexOf("{0}")>=0)
                        {
                            if(strDesciption.StartsWith("Total"))
                            {
                                GroupDescription totalDescription = GetTotalDescription();

                                summaryDescription.Field = totalDescription.Field;

                                summaryDescription.Header=totalDescription.DefaultHeader;
                            }
                            else
                            {
                               GroupDescription avgDescription = GetAVGDescription();

                                summaryDescription.Field = avgDescription.Field;

                                summaryDescription.Header = avgDescription.DefaultHeader;
                            }
                        }

                        summaryList.Add(summaryDescription);                
                    }
                    return summaryList;
               }
          #endregion


        #endregion

        #region Properties
           public bool IsChecked
           {
               get
               {
                   return this._IsChecked;
               }
               set
               {
                   this._IsChecked = value;
               }
           }
           public int OrderIndex
           {
                get
               {
                   return this._OrderIndex;
               }
               set
               {
                   this._OrderIndex = value;
               }
           }
           public int ColumnWidth
           {
               get
               {
                   return this._ColumnWidth;
               }
               set
               {
                   this._ColumnWidth = value;
               }
           }

           public bool ShowZeros
           {
               get
               {
                   return this._ShowZero;
               }
               set
               {
                   this._ShowZero = value;
               }
           }
          public string Field
         {
            get
            {
                if (_Field == null) _Field = string.Empty;
                return _Field;
            }
            set
            {
                _Field = value;
            }
        }
          public string CustomHeader
          {
              get
              {                
                  return _CustomHeader;
              }
              set
              {
                  _CustomHeader = value;
              }
          }

         public string Description
         {
            get
            {
                if (_Description == null) _Description = string.Empty;
                return _Description;
            }
            set
            {
                _Description = value;
            }
         }
        public string Header
        {
                get
                {
                    if (_Header == null) _Header = string.Empty;
                    return _Header;
                }
                set
                {
                    _Header = value;
                }
        }
        #endregion     

        #region Copy Function By Macro 2011-6-16 14:44:28
		public SummaryItemDescription Copy()
        {
			SummaryItemDescription thiscopy=new SummaryItemDescription();
			thiscopy._IsChecked=this._IsChecked;
			thiscopy._Field=this._Field;
			thiscopy._Description=this._Description;
			thiscopy._Header=this._Header;
			thiscopy._ShowZero=this._ShowZero;
			thiscopy._OrderIndex=this._OrderIndex;
			thiscopy._ColumnWidth=this._ColumnWidth;
			thiscopy._CustomHeader=this._CustomHeader;
			return thiscopy;
        }
		#endregion

        #region Serialization By Simon's Macro 2011-6-16 14:44:31
		public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
			info.AddValue("_IsChecked",_IsChecked);
			info.AddValue("_Field",_Field);
			info.AddValue("_Description",_Description);
			info.AddValue("_Header",_Header);
			info.AddValue("_ShowZero",_ShowZero);
			info.AddValue("_OrderIndex",_OrderIndex);
			info.AddValue("_ColumnWidth",_ColumnWidth);
			info.AddValue("_CustomHeader",_CustomHeader);

        }

        public SummaryItemDescription(SerializationInfo info, StreamingContext context)
        {
			try
			{
				_IsChecked=info.GetBoolean("_IsChecked");
			}
			catch
			{
				_IsChecked=false;
			}
			try
			{
				_Field=info.GetString("_Field");
			}
			catch
			{
				_Field=string.Empty;
			}
			try
			{
				_Description=info.GetString("_Description");
			}
			catch
			{
				_Description=string.Empty;
			}
			try
			{
				_Header=info.GetString("_Header");
			}
			catch
			{
				_Header=string.Empty;
			}
			try
			{
				_ShowZero=info.GetBoolean("_ShowZero");
			}
			catch
			{
				_ShowZero=true;
			}
			try
			{
				_OrderIndex=info.GetInt32("_OrderIndex");
			}
			catch
			{
				_OrderIndex=0;
			}
			try
			{
				_ColumnWidth=info.GetInt32("_ColumnWidth");
			}
			catch
			{
				_ColumnWidth=-1;
			}
			try
			{
				_CustomHeader=info.GetString("_CustomHeader");
			}
			catch
			{
				_CustomHeader=null;
			}
        }
		#endregion


   
    }
    public class CompareSummaryOrder : IComparer<SummaryItemDescription>
    {
        public CompareSummaryOrder() { ;}     


        #region IComparer<SummaryItemDescription> Members

        public int Compare(SummaryItemDescription x, SummaryItemDescription y)
        {
            if (x == null || y == null) return 0;

            //if (x.OrderIndex == y.OrderIndex)
            //{
            //    return -1;
            //}

            return x.OrderIndex.CompareTo( y.OrderIndex);
        }

        #endregion

        public void SortSummarysByOrder(List<SummaryItemDescription> allItems)
        {
            if (allItems == null || allItems.Count < 1) return;

            SummaryItemDescription temp;

            for (int i =0;i< allItems.Count - 1;i++)
            {
                for (int j =  allItems.Count-1; j > i; j--)
                {
                    if (allItems[i].OrderIndex >  allItems[j].OrderIndex)
                    {
                        temp = allItems[j];

                        allItems[j] = allItems[i];

                        allItems[i] = temp;
                    }
                }
            }
        }
    }
}
