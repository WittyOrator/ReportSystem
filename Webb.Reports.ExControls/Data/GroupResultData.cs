
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
    #region public class GroupResult
    //Wu.Country@2007-11-14 16:25 added this region.
    [Serializable]
    public class GroupResult //: ISerializable
    {
        public GroupInfo ParentGroupInfo;
        public ClickEvents ClickEvent;
        protected Webb.Collections.Int32Collection _RowIndicators;
        protected object _GroupValue;
        protected GroupSummaryCollection _Summaries;
        protected Webb.Data.DBFilter _Filter;
        protected GroupInfoCollection _SubGroupInfos;	//03-18-2008@Scott

        public GroupResult()
        {
            this.RowIndicators = new Int32Collection();
        }

        #region Calculate
            public void CalculateSummaryResult(System.Data.DataTable i_Table, Webb.Collections.Int32Collection i_OuterRows, Webb.Collections.Int32Collection i_InnerRows)
            {
                if (this._Summaries == null) return;
                // TODO: implement
                foreach (GroupSummary m_Summary in this._Summaries)
                {
                    m_Summary.ParentGroupInfo = this.ParentGroupInfo;

                    if (m_Summary.SummaryType == SummaryTypes.ParentRelatedPercent)
                    {                       
                        GroupInfo groupInfo = this.ParentGroupInfo;
                        
                        #region Modify codes at 2009-4-8 13:22:22@Simon
                        GroupResult m_Result = groupInfo.ParentGroupResult;
                        while (m_Result != null)
                        {
                            if (m_Result.ParentGroupInfo.IsParentGroup) break;

                            m_Result = m_Result.ParentGroupInfo.ParentGroupResult;

                        }

                        if (m_Result != null)
                        {
                            m_Summary.CalculateResult(i_Table, i_OuterRows, m_Result.RowIndicators, this._RowIndicators);

                            continue;
                        }
                        else if (groupInfo.ParentGroupResult != null)
                        {
                            GroupResult parentGroupResult = groupInfo.ParentGroupResult;

                            GroupInfo parentGroupInfo = parentGroupResult.ParentGroupInfo;

                            if (parentGroupInfo.ParentGroupResult != null)
                            {
                                m_Summary.CalculateResult(i_Table, i_OuterRows, parentGroupInfo.ParentGroupResult.RowIndicators/*08-27-2008@Scott*/, this._RowIndicators);

                                continue;
                            }

                        }
                        #endregion        //End Modify

                        m_Summary.CalculateResult(i_Table, i_OuterRows, i_InnerRows/*08-27-2008@Scott*/, this._RowIndicators);
                    }
                    else if (m_Summary.SummaryType == SummaryTypes.GroupPercent)
                    {
                        #region GroupPercent

                        GroupInfo groupInfo = this.ParentGroupInfo;

                        if (groupInfo.ParentGroupResult != null)
                        {
                            GroupResult parentGroupResult = groupInfo.ParentGroupResult;

                            if (parentGroupResult != null)
                            {
                                m_Summary.CalculateResult(i_Table, i_OuterRows, parentGroupResult.RowIndicators, this._RowIndicators);

                                continue;
                            }
                        }
                       

                        m_Summary.CalculateResult(i_Table, i_OuterRows, i_InnerRows/*08-27-2008@Scott*/, this._RowIndicators);

                        #endregion
                    }
                    else if (m_Summary.SummaryType == SummaryTypes.FrequenceAllData || m_Summary.SummaryType == SummaryTypes.PercentAllData)
                    {
                        Int32Collection allResultRows = this.Filter.GetFilteredRows(i_Table, i_OuterRows);

                        m_Summary.CalculateResult(i_Table, i_OuterRows, allResultRows, allResultRows);
                    }
                    else
                    {
                        m_Summary.CalculateResult(i_Table, i_OuterRows, i_InnerRows/*08-27-2008@Scott*/, this._RowIndicators);
                    }
                }
            }

            public void CalculateRowIndicators(System.Data.DataTable i_Table, Webb.Collections.Int32Collection i_Rows)
            {//06-04-2008@Scott
                this._RowIndicators = this._Filter.GetFilteredRows(i_Table, i_Rows);
            }
        #endregion

        #region Properties

        [Browsable(false)]
        public GroupInfoCollection SubGroupInfos
        {
            get
            {
                if (this._SubGroupInfos == null) this._SubGroupInfos = new GroupInfoCollection();

                return this._SubGroupInfos;
            }
            set
            {
                this._SubGroupInfos = value;

                if (this._SubGroupInfos == null) this._SubGroupInfos = new GroupInfoCollection();

                foreach (GroupInfo groupInfo in this._SubGroupInfos)
                {
                    groupInfo.ParentGroupResult = this;
                }
            }
        }

        [Browsable(true), Category("Collection Data")]
        public DBFilter Filter
        {
            get
            {
                if (_Filter == null) _Filter = new DBFilter(); //2009-3-9 13:30:04@Simon
                return this._Filter;
            }
            set { this._Filter = value; }
        }

        [Browsable(true), Category("Collection Data")]
        public GroupSummaryCollection Summaries
        {
            get { return this._Summaries; }
            set { this._Summaries = value; }
        }

        [Browsable(false)]
        public object GroupValue
        {
            get { return this._GroupValue; }
            set { this._GroupValue = value; }
        }

        [Browsable(false)]
        public Int32Collection RowIndicators
        {
            get {
                if (_RowIndicators == null) _RowIndicators = new Int32Collection();
                  return this._RowIndicators; }
            set { this._RowIndicators = value; }
        }

        #endregion

        #region Copy Result
            public GroupResult CloneResult()
            {
                GroupResult result = new GroupResult();
                result.ClickEvent = this.ClickEvent;
                result._RowIndicators = new Int32Collection();
                result.Filter = this.Filter.Copy();
                this._RowIndicators.CopyTo(result._RowIndicators);
                result.GroupValue = this.GroupValue;
                return result;

            }
        #endregion

    }
    #endregion

    #region public class GroupResultCollection
    //Wu.Country@2007-11-14 16:28 added this region.
    [Serializable]
    public class GroupResultCollection : System.Collections.CollectionBase, IComparer
    {
        private SortingTypes _SortingType;
        private SortingByTypes _SortByType;
        private UserOrderClS _UserDefinedValues;

        public GroupResultCollection()
        {
        }
        public int Add(GroupResult i_Result)
        {
            return this.InnerList.Add(i_Result);
        }
        public void InserAt(int i_Index, GroupResult i_Result)
        {
            if (i_Result == null || i_Index >= this.InnerList.Count) return;

            this.InnerList.Insert(0, i_Result);

        }
        public GroupResult this[int i_Index]
        {
            get { return this.InnerList[i_Index] as GroupResult; }
            set { this.InnerList[i_Index] = value; }
        }
        public GroupResult this[object objValue]	//08-04-2008@Scott
        {
            get
            { 
                foreach (GroupResult groupResult in this)
                {
                    if (objValue == null)
                    {
                        if (groupResult.GroupValue == null) return groupResult;
                    }
                    else if (objValue is DBNull)
                    {
                        if (groupResult.GroupValue is DBNull) return groupResult;
                    }
                    if (groupResult.GroupValue != null && !(groupResult.GroupValue is DBNull))
                    {
                        if (groupResult.GroupValue.ToString() == objValue.ToString()) return groupResult;                       
                    }
                }

                return null;
            }
        }

        #region Sort
        public void Sort(SortingTypes i_Type, SortingByTypes i_SortByType)
        {
            this._SortingType = i_Type;
            this._SortByType = i_SortByType;

            if (this._SortByType != SortingByTypes.None)  //2009-3-11 10:16:57@Simon
            {
                this.InnerList.Sort(this);
            }
        }
        public void Sort(SortingTypes i_Type, SortingByTypes i_SortByType,UserOrderClS i_UserDefinedOrders)
        {
            this._SortingType = i_Type;
            this._SortByType = i_SortByType;
            this._UserDefinedValues = i_UserDefinedOrders;

            if(this._SortByType==SortingByTypes.UserDefinedOrder&&(i_UserDefinedOrders==null||i_UserDefinedOrders.OrderValues.Count==0))return;

            if (this._SortByType != SortingByTypes.None)  //2009-3-11 10:16:57@Simon
            {
                this.InnerList.Sort(this);
            }
        }        

        #region IComparer Members
        int IComparer.Compare(object x, object y)
        {
            // TODO:  Add GroupResultCollection.Compare implementation
            if (this._SortByType == SortingByTypes.None) return 0;
            int m_Result = 0;

            Webb.Reports.DataProvider.WebbDataProvider publicprovider = Webb.Reports.DataProvider.VideoPlayBackManager.PublicDBProvider;

            if (this._SortByType == SortingByTypes.GroupedVale)
            {
                GroupResult m_x = x as GroupResult;
                GroupResult m_y = y as GroupResult;

                string strX = string.Empty;

                if (m_x.GroupValue != null && !(m_x.GroupValue is System.DBNull)) strX = m_x.GroupValue.ToString();

                string strY = string.Empty;

                if (m_y.GroupValue != null && !(m_y.GroupValue is System.DBNull)) strY = m_y.GroupValue.ToString();

                m_Result = String.Compare(strX, strY);               

            }
            else if (this._SortByType == SortingByTypes.GroupedValueOrNumber)  //2009-12-8 16:22:28@Simon Add this Code
            {
                #region GroupedValueOrNumber
                GroupResult m_x = x as GroupResult;
                GroupResult m_y = y as GroupResult;

                if (m_x.GroupValue is System.DateTime && m_y.GroupValue is System.DateTime)
                {
                    #region Datetime Dat
                    try
                    {
                        DateTime dt1 = (DateTime)m_x.GroupValue;
                        DateTime dt2 = (DateTime)m_y.GroupValue;
                        m_Result = DateTime.Compare(dt1, dt2);

                    }
                    catch
                    {
                        m_Result = String.Compare(m_x.GroupValue.ToString(), m_y.GroupValue.ToString());
                    }
                    #endregion
                }
                else
                {
                    #region String Or Numeric
                    string strX =string.Empty;

                    if (m_x.GroupValue != null && !(m_x.GroupValue is System.DBNull)) strX = m_x.GroupValue.ToString();

                    string strY = string.Empty;

                    if (m_y.GroupValue != null && !(m_y.GroupValue is System.DBNull)) strY = m_y.GroupValue.ToString();

                    bool X_isNum = Webb.Utility.IsNumeric(strX);

                    bool Y_isNum = Webb.Utility.IsNumeric(strY);

                    if (X_isNum && Y_isNum)
                    {
                        try
                        {
                            float a = Convert.ToSingle(strX);
                            float b = Convert.ToSingle(strY);

                            if (a > b) m_Result = 1;
                            else if (a < b) m_Result = -1;

                            else m_Result = 0;

                        }
                        catch
                        {
                            m_Result = String.Compare(m_x.GroupValue.ToString(), m_y.GroupValue.ToString());
                        }
                    }
                    else
                    {
                        m_Result = String.Compare(m_x.GroupValue.ToString(), m_y.GroupValue.ToString());
                    }
                    #endregion

                    #region Time/FeetInchesData Field For CCRM
                    if ((m_x.ParentGroupInfo is FieldGroupInfo) && (m_y.ParentGroupInfo is FieldGroupInfo))
                    {
                        string strXField = (m_x.ParentGroupInfo as FieldGroupInfo).GroupByField;
                        string strYField = (m_y.ParentGroupInfo as FieldGroupInfo).GroupByField;

                        if (publicprovider != null)
                        {
                            if (publicprovider.IsCCRMTimeData(strXField) && publicprovider.IsCCRMTimeData(strYField))
                            {
                                TimeSpan timeSpanX = Webb.Utility.ConvertToTimeTicks(strX);

                                TimeSpan timeSpanY = Webb.Utility.ConvertToTimeTicks(strY);

                                m_Result = TimeSpan.Compare(timeSpanX, timeSpanY);
                            }
                            else if (publicprovider.IsFeetInchesData(strXField) && publicprovider.IsFeetInchesData(strYField))
                            {
                                decimal inumX = Webb.Utility.ConvertFeetInchToNum(strX);
                                decimal inumY = Webb.Utility.ConvertFeetInchToNum(strY);

                                m_Result = decimal.Compare(inumX, inumY);
                            }
                        }
                    }
                    #endregion
                }
                #endregion
             
            }
            else if (this._SortByType == SortingByTypes.Frequence)
            {
                GroupResult m_x = x as GroupResult;
                GroupResult m_y = y as GroupResult;

                m_Result = m_x.RowIndicators.Count - m_y.RowIndicators.Count;

                if (m_Result == 0)
                {
                    string strX = string.Empty;

                    if (m_x.GroupValue != null && !(m_x.GroupValue is System.DBNull)) strX = m_x.GroupValue.ToString();

                    string strY = string.Empty;

                    if (m_y.GroupValue != null && !(m_y.GroupValue is System.DBNull)) strY = m_y.GroupValue.ToString();

                    m_Result = String.Compare(strX, strY); 
                }
            }
            else if (this._SortByType == SortingByTypes.Number || this._SortByType == SortingByTypes.FootballField)
            {
                #region Number Or FootballField
                GroupResult m_x = x as GroupResult;
                GroupResult m_y = y as GroupResult;

                if (m_x.GroupValue is System.DateTime && m_y.GroupValue is System.DateTime)
                {
                    #region Datetime 
                    try
                    {
                        DateTime dt1 = (DateTime)m_x.GroupValue;
                        DateTime dt2 = (DateTime)m_y.GroupValue;
                        m_Result = DateTime.Compare(dt1, dt2);

                    }
                    catch
                    {
                        m_Result = String.Compare(m_x.GroupValue.ToString(), m_y.GroupValue.ToString());
                    }
                    goto EXIT;
                    #endregion
                }

                string strX = string.Empty;

                if (m_x.GroupValue != null && !(m_x.GroupValue is System.DBNull)) strX = m_x.GroupValue.ToString();

                string strY = string.Empty;

                if (m_y.GroupValue != null && !(m_y.GroupValue is System.DBNull)) strY = m_y.GroupValue.ToString();

                if (strX == string.Empty && strY != string.Empty)
                {
                    m_Result = -1;
                    goto EXIT;
                }
                if (strY == string.Empty && strX != string.Empty)
                {
                    m_Result = 1;
                    goto EXIT;
                }
                if (strY == string.Empty && strX == string.Empty)
                {
                    m_Result = 0;
                    goto EXIT;
                }
                try
                {
                   
                    if ((m_x.ParentGroupInfo is FieldGroupInfo) && (m_y.ParentGroupInfo is FieldGroupInfo))
                    {
                        #region Time/FeetInchesData Field For CCRM

                        string strXField = (m_x.ParentGroupInfo as FieldGroupInfo).GroupByField;
                        string strYField = (m_y.ParentGroupInfo as FieldGroupInfo).GroupByField;

                        if (publicprovider != null)
                        {
                            if (publicprovider.IsCCRMTimeData(strXField) && publicprovider.IsCCRMTimeData(strYField))
                            {
                                TimeSpan timeSpanX = Webb.Utility.ConvertToTimeTicks(strX);

                                TimeSpan timeSpanY = Webb.Utility.ConvertToTimeTicks(strY);

                                m_Result = TimeSpan.Compare(timeSpanX, timeSpanY);

                                goto EXIT;
                            }
                            else if (publicprovider.IsFeetInchesData(strXField) && publicprovider.IsFeetInchesData(strYField))
                            {
                                decimal inumX = Webb.Utility.ConvertFeetInchToNum(strX);
                                decimal inumY = Webb.Utility.ConvertFeetInchToNum(strY);

                                m_Result = decimal.Compare(inumX, inumY);

                                goto EXIT;
                            }
                        }

                        #endregion
                    }  
               
                    #region Number Or FootballField

                    int i = Convert.ToInt32(strX);
                    int j = Convert.ToInt32(strY);
                    if (this._SortByType == SortingByTypes.FootballField)
                    {//by football field

                        #region Old Only Yard Field

                        //GroupInfo gi = (x as GroupResult).ParentGroupInfo;
                        //if (gi is FieldGroupInfo)
                        //{
                        //    if ((gi as FieldGroupInfo).GroupByField == "Yard")
                        //    {
                        //        m_Result = CompareByFootballField(i, j);
                        //        goto EXIT;
                        //    }
                        //}
                        //m_Result = 0;

                        #endregion

                        m_Result = CompareByFootballField(i, j);
                    }
                    else
                    {//by number
                        if (i > j) m_Result = 1;
                        else if (i < j) m_Result = -1;
                        else m_Result = 0;
                    }
                    #endregion
                    
                    
                   
                }
                catch
                {
                    m_Result = 0;
                }
                #endregion
            }
            else if (this._SortByType == SortingByTypes.UserDefinedOrder&&this._UserDefinedValues!=null)
            {
                #region  UserDefinedOrder
                  GroupResult m_x = x as GroupResult;
                  GroupResult m_y = y as GroupResult;
                  if (this._UserDefinedValues.Contains(m_x.GroupValue) && this._UserDefinedValues.Contains(m_y.GroupValue))
                  {
                      m_Result=this._UserDefinedValues.IndexOf(m_x.GroupValue)-this._UserDefinedValues.IndexOf(m_y.GroupValue);
                  }
                  else if (this._UserDefinedValues.Contains(m_x.GroupValue))
                  {
                      m_Result = -1;
                  }
                  else if (this._UserDefinedValues.Contains(m_y.GroupValue))
                  {
                      m_Result = 1;
                  }
                #endregion
            }
            else if (this._SortByType == SortingByTypes.DateTime)   // 10-13-2011 Scott
            {
                GroupResult m_x = x as GroupResult;
                GroupResult m_y = y as GroupResult;

                string strX = string.Empty;
                string strY = string.Empty;

                DateTime dateX;
                DateTime dateY;

                if (m_x.GroupValue != null && !(m_x.GroupValue is System.DBNull))
                {
                    strX = m_x.GroupValue.ToString();
                }

                if (m_y.GroupValue != null && !(m_y.GroupValue is System.DBNull))
                {
                    strY = m_y.GroupValue.ToString();
                }

                try
                {
                    dateX = (DateTime)m_x.GroupValue;
                    dateY = (DateTime)m_y.GroupValue;
                    m_Result = DateTime.Compare(dateX, dateY);
                }
                catch
                {
                    m_Result = string.Compare(strX, strY);
                }
            }
            else if (this._SortByType == SortingByTypes.PlayerPosition) // 09-26-2011 Scott
            {
                GroupResult m_x = x as GroupResult;
                GroupResult m_y = y as GroupResult;
                string strX = string.Empty;
                string strY = string.Empty;
                int indexX = -1;
                int indexY = -1;

                if (m_x.GroupValue != null && !(m_x.GroupValue is System.DBNull))
                {
                    strX = m_x.GroupValue.ToString();
                }

                if (m_y.GroupValue != null && !(m_y.GroupValue is System.DBNull))
                {
                    strY = m_y.GroupValue.ToString();
                }

                for (int indexPos = 0; indexPos < PlayerPositions.Length; indexPos++)
                {
                    if (strX.StartsWith(PlayerPositions[indexPos]))
                    {
                        indexX = indexPos;

                        break;
                    }
                }

                for (int indexPos = 0; indexPos < PlayerPositions.Length; indexPos++)
                {
                    if (strY.StartsWith(PlayerPositions[indexPos]))
                    {
                        indexY = indexPos;

                        break;
                    }
                }

                if (indexX != indexY)
                {
                    m_Result = indexX < indexY ? -1 : 1;
                }
                else
                {
                    string strNumX = string.Empty;
                    string strNumY = string.Empty;

                    if (indexX >= 0)
                    {
                        strNumX = strX.Replace(PlayerPositions[indexX], string.Empty);
                    }

                    if (indexY >= 0)
                    {
                        strNumY = strY.Replace(PlayerPositions[indexY], string.Empty);
                    }

                    m_Result = string.Compare(strNumX, strNumY);
                }
            }
           EXIT:
            return _SortingType == SortingTypes.Ascending ? m_Result : -m_Result;
        }

        public static string[] PlayerPositions= {"OL","TE","WR","RB","QB","DL","LB","CB","S"};      // 09-26-2011 Scott

        public static int CompareByFootballField(int x, int y)
        {
            if (x * y < 0)
            {
                if (x < 0) return 1;
                else if (y < 0) return -1;
                else return 0;
            }
            else if (x * y == 0)
            {
                if (x == 0 && y == 0) return 0;
                if (x == 0)return -1;               
                else  return 1;
              
            }
            else
            {
                if (x > y) return 1;
                else if (x < y) return -1;
                else return 0;
            }
        }
        #endregion
        #endregion

        #region AppendResult & Combined Result  for MatrixControl
            public void AppendResult(GroupResult m_Result)
            {
                GroupResult innerResult = this[m_Result.GroupValue];

                if (innerResult == null)
                {
                    this.InnerList.Add(m_Result);
                }
                else
                {
                    m_Result.RowIndicators.CopyTo(innerResult.RowIndicators);
                }
            }
            public void CombineResult(GroupResult m_Result,GroupInfo parentGroup)
            {
                GroupResult innerResult = this[m_Result.GroupValue];

                if (innerResult == null)
                {
                    GroupResult cloneResult = innerResult.CloneResult();

                    cloneResult.ParentGroupInfo = parentGroup;

                    this.InnerList.Add(cloneResult);
                }
                else
                {
                    m_Result.RowIndicators.CopyTo(innerResult.RowIndicators);

                    foreach (GroupResult subResult in m_Result.SubGroupInfos[0].GroupResults)
                    {
                        GroupResult subcloneResult = subResult.CloneResult();

                        subcloneResult.ParentGroupInfo = innerResult.SubGroupInfos[0];

                        innerResult.SubGroupInfos[0].GroupResults.AppendResult(subcloneResult);

                    }
                }
            }         

        #endregion
    }
    #endregion
}