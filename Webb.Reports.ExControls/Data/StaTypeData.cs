/***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:GroupingData.cs
 * Author:Country.Wu [EMail:Webb.Country.Wu@163.com]
 * Create Time:11/14/2007 04:21:17 PM
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
    #region Data Structure
        [Serializable]
        public enum StatTypes
        {
            None = 0,
            Frequence = 1,
            DistFrequence = 12,	//Modified at 2008-11-20 13:29:42@Scott
            Percent = 2,
            DistPercent = 13,	//Modified at 2008-11-20 13:31:15@Scott
            Total = 3,
            Average = 4,
            Max = 5,
            Min = 6,
            TotalPlus = 7,
            TotalMinus = 8,
            AveragePlus = 9,
            AverageMinus = 10,
            OneValuePercent = 11,	//Modified at 2008-11-11 16:11:06@Scott
        }

        [Serializable]
        public class StatTypesCollection : CollectionBase
        {
            public StatTypes this[int i_Index]
            {
                get { return (StatTypes)this.InnerList[i_Index]; }
                set { this.InnerList[i_Index] = value; }
            }
            //ctor
            public StatTypesCollection() { }
            //Methods
            public int Add(StatTypes i_Object)
            {
                return this.InnerList.Add(i_Object);
            }

            public void Remove(StatTypes i_Obj)
            {
                this.InnerList.Remove(i_Obj);
            }

            public StatTypesCollection Copy()
            {
                StatTypesCollection newStatTypes = new StatTypesCollection();

                foreach (StatTypes type in this)
                {
                    newStatTypes.Add(type);
                }

                return newStatTypes;
            }
        }

        #region class  FollowedStatTypes:ISerializable
        [Serializable]
        public class FollowedStatTypes : ISerializable
        {//Added this code at 2009-1-4 13:12:00@Simon
            private StatTypes statTypes;
            private string follows;

            private sbyte _DecimalSpace = -1;    //Added this code at 2009-2-1 15:44:55@Simon
            public sbyte DecimalSpace
            {
                get { return this._DecimalSpace; }
                set { this._DecimalSpace = value; }
            }
            public StatTypes StatTypes
            {
                get { return statTypes; }
                set { statTypes = value; }
            }
            public string Followed
            {
                get { return follows; }
                set { follows = value; }
            }
            public FollowedStatTypes()
            {
                statTypes = StatTypes.None;
                follows = string.Empty;
                _DecimalSpace = -1;
            }

            public FollowedStatTypes(StatTypes _statTypes)
            {
                statTypes = _statTypes;
                follows = string.Empty;
                _DecimalSpace = -1;
            }
            public FollowedStatTypes(StatTypes _statTypes, string _follows)
            {
                statTypes = _statTypes;
                follows = _follows;
                _DecimalSpace = -1;
            }

            public override string ToString()
            {
                return statTypes.ToString() + ":" + follows;
            }
            public FollowedStatTypes Copy()
            {
                FollowedStatTypes _FollowedStatTypes = new FollowedStatTypes(statTypes, follows);
                _FollowedStatTypes.DecimalSpace = this.DecimalSpace;
                return _FollowedStatTypes;
            }

            public void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                info.AddValue("statTypes", this.statTypes, typeof(StatTypes));

                info.AddValue("follows", this.follows);

                info.AddValue("_DecimalSpace", this._DecimalSpace);
            }

            public FollowedStatTypes(SerializationInfo info, StreamingContext context)
            {
                try
                {
                    this.statTypes = (StatTypes)(info.GetValue("statTypes", typeof(StatTypes)));
                }
                catch
                {
                    this.statTypes = StatTypes.None;
                }

                try
                {
                    this.follows = info.GetString("follows");
                }
                catch
                {
                    this.follows = "";
                }
                try
                {
                    this._DecimalSpace = info.GetSByte("_DecimalSpace");
                }
                catch
                {
                    this._DecimalSpace = -1;
                }
            }
        }
        #endregion

        #region public class FollowedStatTypesCollection
        [Serializable]
        public class FollowedStatTypesCollection : CollectionBase
        {
            public FollowedStatTypes this[int i_Index]
            {
                get { return (FollowedStatTypes)this.InnerList[i_Index]; }
                set { this.InnerList[i_Index] = value; }
            }
            //ctor
            public FollowedStatTypesCollection()
            {
            }
            public FollowedStatTypesCollection(StatTypesCollection stateTypes)
            {
                this.InnerList.Clear();

                foreach (StatTypes statetype in stateTypes)
                {
                    this.Add(new FollowedStatTypes(statetype));
                }
            }
            //Methods
            public int Add(FollowedStatTypes i_Object)
            {
                return this.InnerList.Add(i_Object);

            }
            new public void RemoveAt(int Index)
            {
                this.InnerList.RemoveAt(Index);
            }
            public void Remove(FollowedStatTypes i_Obj)
            {
                this.InnerList.Remove(i_Obj);
            }

            public FollowedStatTypesCollection Copy()
            {
                FollowedStatTypesCollection newfollowStatTypes = new FollowedStatTypesCollection();

                foreach (FollowedStatTypes i_Obj in this)
                {
                    newfollowStatTypes.Add(i_Obj.Copy());
                }

                return newfollowStatTypes;
            }
        }
        #endregion
    #endregion

    #region public class StatInfo
    [Serializable]
    public class StatInfo : ISerializable
    {//02-19-2008
        protected ClickEvents _ClickEvent;
        protected string _Title;
        protected string _FieldName;
        protected StatTypesCollection _StatTypes;
        protected DBFilter _Filter;
        protected DBFilter _DenominatorFilter;
        protected ArrayList _Result;
        protected Int32Collection _RowIndicators;

        protected FollowedStatTypesCollection _FollowedStatTypes;  //Added this code at 2009-1-4 11:27:11@Simon

        //12-11-2008@Scott
        //Begin
        virtual public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("_ClickEvent", this._ClickEvent, typeof(ClickEvents));

            info.AddValue("_Title", this._Title);

            info.AddValue("_FieldName", this._FieldName);

            info.AddValue("_StatTypes", this._StatTypes, typeof(StatTypesCollection));

            info.AddValue("_Filter", this._Filter, typeof(DBFilter));

            info.AddValue("_DenominatorFilter", this._DenominatorFilter, typeof(DBFilter));

            info.AddValue("_FollowedStatTypes", this._FollowedStatTypes, typeof(FollowedStatTypesCollection));
        }

        public StatInfo(SerializationInfo info, StreamingContext context)
        {
            try
            {
                this._ClickEvent = (ClickEvents)(info.GetValue("_ClickEvent", typeof(ClickEvents)));
            }
            catch
            {
                this._ClickEvent = ClickEvents.PlayVideo;
            }

            try
            {
                this._Title = info.GetString("_Title");
            }
            catch
            {
                this._Title = "New Stat";
            }

            try
            {
                this._FieldName = info.GetString("_FieldName");
            }
            catch
            {
                this._FieldName = string.Empty;
            }

            try
            {
                this._StatTypes = info.GetValue("_StatTypes", typeof(StatTypesCollection)) as StatTypesCollection;
            }
            catch
            {
                this._StatTypes = new StatTypesCollection();
            }

            try
            {
                this._Filter = info.GetValue("_Filter", typeof(DBFilter)) as DBFilter;

                this._Filter = AdvFilterConvertor.GetAdvFilter(DataProvider.VideoPlayBackManager.AdvReportFilters, this._Filter);    //2009-4-29 11:37:37@Simon Add UpdateAdvFilter
            }
            catch
            {
                this._Filter = new DBFilter();
            }

            try
            {
                this._DenominatorFilter = info.GetValue("_DenominatorFilter", typeof(DBFilter)) as DBFilter;

                this._DenominatorFilter = AdvFilterConvertor.GetAdvFilter(DataProvider.VideoPlayBackManager.AdvReportFilters, this._DenominatorFilter);    //2009-4-29 11:37:37@Simon Add UpdateAdvFilter
            }
            catch
            {
                this._DenominatorFilter = new DBFilter();
            }
            try  //Added this code at 2009-1-5 8:21:00@Simon
            {
                this._FollowedStatTypes = info.GetValue("_FollowedStatTypes", typeof(FollowedStatTypesCollection)) as FollowedStatTypesCollection;
            }
            catch
            {
                this._FollowedStatTypes = new FollowedStatTypesCollection(this._StatTypes);
            }
        }
        //End

        public StatInfo()
        {
            this._Filter = new DBFilter();
            this._DenominatorFilter = new DBFilter();
            this._StatTypes = new StatTypesCollection();
            this._StatTypes.Add(Data.StatTypes.Frequence);
            this._ClickEvent = ClickEvents.PlayVideo;
            this._FieldName = string.Empty;
            this._Result = new ArrayList();
            this._Title = "New Stat";

            this._FollowedStatTypes = new FollowedStatTypesCollection(this._StatTypes);   //Added this code at 2009-1-4 11:26:59@Simon
        }

        public StatInfo(StatInfo info)
        {
            this._Filter = info.Filter.Copy();
            this._DenominatorFilter = info.DenominatorFilter.Copy();
            this._StatTypes = info.StatTypes.Copy();
            this._ClickEvent = info.ClickEvent;
            this._FieldName = info.StatField;
            this._Title = info.Title;
            this._Result = new ArrayList();

            this._FollowedStatTypes = info._FollowedStatTypes.Copy();
        }

        [Browsable(false)]
        public Int32Collection RowIndicators
        {
            get
            {
                if (this._RowIndicators == null) this._RowIndicators = new Int32Collection();

                return this._RowIndicators;
            }
        }

        public FollowedStatTypesCollection StatisticalSetting
        {
            get
            {
                if (_FollowedStatTypes == null)
                {
                    _FollowedStatTypes = new FollowedStatTypesCollection(this._StatTypes);
                }
                return this._FollowedStatTypes;
            }
            set
            {
                this._FollowedStatTypes = value;
            }
        }

        public string Title
        {
            get { return this._Title; }
            set { this._Title = value; }
        }

        [Editor(typeof(Webb.Reports.Editors.FieldSelectEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string StatField
        {
            get { return this._FieldName; }
            set { this._FieldName = value; }
        }


        [EditorAttribute(typeof(Webb.Reports.Editors.DBFilterEditor), typeof(System.Drawing.Design.UITypeEditor))]	//06-17-2008@Scott
        public DBFilter Filter
        {
            get
            {
                if (this._Filter == null) this._Filter = new DBFilter();

                return this._Filter;
            }
            set
            {
                this._Filter = value.Copy();
            }
        }

        [EditorAttribute(typeof(Webb.Reports.Editors.DBFilterEditor), typeof(System.Drawing.Design.UITypeEditor))]	//06-17-2008@Scott
        public DBFilter DenominatorFilter
        {
            get
            {
                if (this._DenominatorFilter == null) this._DenominatorFilter = new DBFilter();

                return this._DenominatorFilter;
            }
            set
            {
                this._DenominatorFilter = value.Copy();
            }
        }

        [Browsable(false)]
        public StatTypesCollection StatTypes
        {
            get
            {
                if (this._StatTypes == null) this._StatTypes = new StatTypesCollection();

                return this._StatTypes;
            }
        }

        public ClickEvents ClickEvent
        {
            get { return this._ClickEvent; }
            set { this._ClickEvent = value; }
        }

        [Browsable(false)]
        public ArrayList Result
        {
            get
            {
                if (this._Result == null) this._Result = new ArrayList();

                return this._Result;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("{0}", this._Title);

            //			foreach(StatTypes type in this._StatTypes)
            //			{
            //				sb.AppendFormat("({0})",type.ToString());
            //			}
            foreach (FollowedStatTypes type in this._FollowedStatTypes)
            {
                sb.AppendFormat("({0})", type.ToString());
            }

            return sb.ToString();
        }

        #region CalculateResult
        public virtual void CalculateResult(System.Data.DataTable i_Table, int i_FilteredRowsCount, Webb.Collections.Int32Collection i_Rows)
        {
            Int32Collection m_FilteredRows = this.GetFilteredRows(i_Table, i_Rows, this._Filter);

            this._RowIndicators = m_FilteredRows;

            this.Result.Clear();

            this.CalculateResultEx(i_Table, i_Rows, m_FilteredRows);
        }

        private Int32Collection GetFilteredRows(System.Data.DataTable i_Table, Webb.Collections.Int32Collection i_Rows, Webb.Data.DBFilter filter)
        {//06-04-2008@Scott
            if (filter == null || i_Table == null || i_Rows == null) return i_Rows;

            return filter.GetFilteredRows(i_Table, i_Rows);
        }
        #region Original Codes
        //		private void CalculateResultEx(System.Data.DataTable i_Table,Webb.Collections.Int32Collection i_FilteredRows/*outer filter*/, Webb.Collections.Int32Collection i_Rows/*inner filter*/)
        //		{
        //			foreach(Data.StatTypes type in this.StatTypes)
        //			{
        //				switch(type)
        //				{
        //					case Data.StatTypes.Average:
        //					case Data.StatTypes.AveragePlus:
        //					case Data.StatTypes.AverageMinus:
        //						this.CalculateAverage(i_Table, i_Rows, type);
        //						break;
        //
        //					case Data.StatTypes.Frequence:
        //						this._Result.Add(i_Rows.Count);
        //						break;
        //
        //					case Data.StatTypes.Max:
        //						this.CalculateMax(i_Table,i_Rows);
        //						break;
        //
        //					case Data.StatTypes.Min:
        //						this.CalculateMin(i_Table,i_Rows);
        //						break;
        //
        //					case Data.StatTypes.Percent:
        //						Webb.Collections.Int32Collection denominatorRows = this.GetFilteredRows(i_Table, i_FilteredRows, this._DenominatorFilter);
        //						if(denominatorRows.Count > 0)
        //						{
        //							this._Result.Add(1.0 * i_Rows.Count / denominatorRows.Count);
        //						}
        //						else
        //						{
        //							this._Result.Add(0);
        //						}
        //						break;
        //
        //					case Data.StatTypes.DistFrequence:
        //						this.CalculateDistFrequence(i_Table,i_Rows);	//Modified at 2008-11-20 13:36:28@Scott
        //						break;
        //
        //					case Data.StatTypes.DistPercent:
        //						this.CalculateDistPercent(i_Table,i_FilteredRows,i_Rows);	//Modified at 2008-11-20 13:36:31@Scott
        //						break;
        //
        //					case Data.StatTypes.Total:
        //					case Data.StatTypes.TotalPlus:
        //					case Data.StatTypes.TotalMinus:
        //						this.CalculateTotal(i_Table, i_Rows, type);
        //						break;
        //					case Data.StatTypes.OneValuePercent:	//Modified at 2008-11-11 16:10:57@Scott
        //						this.CalculateOneValuePercent(i_Table,i_FilteredRows,i_Rows);
        //						break;
        //					default:
        //						this._Result.Add(string.Empty);
        //						break;
        //				}
        //			}
        //		}

        #endregion
        private void CalculateResultEx(System.Data.DataTable i_Table, Webb.Collections.Int32Collection i_FilteredRows/*outer filter*/, Webb.Collections.Int32Collection i_Rows/*inner filter*/)
        {
            FollowedStatTypesCollection statTypesFollow = this._FollowedStatTypes;
            this.StatTypes.Clear();
            foreach (FollowedStatTypes followtype in statTypesFollow)
            {
                StatTypes type = followtype.StatTypes;
                this.StatTypes.Add(type);
                switch (type)
                {
                    case Data.StatTypes.Average:
                    case Data.StatTypes.AveragePlus:
                    case Data.StatTypes.AverageMinus:
                        this.CalculateAverage(i_Table,i_FilteredRows, i_Rows, type);
                        break;

                    case Data.StatTypes.Frequence:
                        this._Result.Add(i_Rows.Count);
                        break;

                    case Data.StatTypes.Max:
                        this.CalculateMax(i_Table, i_Rows);
                        break;

                    case Data.StatTypes.Min:
                        this.CalculateMin(i_Table, i_Rows);
                        break;

                    case Data.StatTypes.Percent:
                        Webb.Collections.Int32Collection denominatorRows = this.GetFilteredRows(i_Table, i_FilteredRows, this._DenominatorFilter);
                        if (denominatorRows.Count > 0)
                        {
                            this._Result.Add(1.0 * i_Rows.Count / denominatorRows.Count);
                        }
                        else
                        {
                            this._Result.Add(0);
                        }
                        break;

                    case Data.StatTypes.DistFrequence:
                        this.CalculateDistFrequence(i_Table, i_Rows);	//Modified at 2008-11-20 13:36:28@Scott
                        break;

                    case Data.StatTypes.DistPercent:
                        this.CalculateDistPercent(i_Table, i_FilteredRows, i_Rows);	//Modified at 2008-11-20 13:36:31@Scott
                        break;

                    case Data.StatTypes.Total:
                    case Data.StatTypes.TotalPlus:
                    case Data.StatTypes.TotalMinus:
                        this.CalculateTotal(i_Table,i_FilteredRows, i_Rows, type);
                        break;
                    case Data.StatTypes.OneValuePercent:	//Modified at 2008-11-11 16:10:57@Scott
                        this.CalculateOneValuePercent(i_Table, i_FilteredRows, i_Rows);
                        break;
                    default:
                        this._Result.Add(string.Empty);
                        break;
                }
            }
        }

        //04-11-2008@Scott
        private void CalculateMax(DataTable i_Table, Webb.Collections.Int32Collection i_Rows)
        {
            double m_Max = 0, m_Temp = 0;

            if (!i_Table.Columns.Contains(this._FieldName)) goto EXIT;

            if (i_Rows.Count <= 0) goto EXIT;

            int i = 0;

            for (; i < i_Rows.Count; i++)
            {
                try
                {
                    string strValue = i_Table.Rows[i_Rows[i]][this._FieldName].ToString();

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
                    string strValue = i_Table.Rows[i_Rows[i]][this._FieldName].ToString();

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
            this._Result.Add(m_Max);
        }

        //04-11-2008@Scott
        private void CalculateMin(DataTable i_Table, Webb.Collections.Int32Collection i_Rows)
        {
            double m_Min = 0, m_Temp = 0;

            if (!i_Table.Columns.Contains(this._FieldName)) goto EXIT;

            if (i_Rows.Count <= 0) goto EXIT;

            int i = 0;

            for (; i < i_Rows.Count; i++)
            {
                try
                {
                    string strValue = i_Table.Rows[i_Rows[i]][this._FieldName].ToString();

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
                    string strValue = i_Table.Rows[i_Rows[i]][this._FieldName].ToString();

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
            this._Result.Add(m_Min);
        }

        private void CalculateAverage(DataTable i_Table,Webb.Collections.Int32Collection i_OuterRows,Webb.Collections.Int32Collection i_Rows, Data.StatTypes i_Type)
        {
            decimal m_Sum = 0, fieldValue = 0m;

            DateTime totalDateTime = new DateTime(0);

            long timeTicks = 0;  
            
            Type type=null;

            Webb.Reports.DataProvider.WebbDataProvider publicprovider = Webb.Reports.DataProvider.VideoPlayBackManager.PublicDBProvider;
           
            bool isTimeData = (publicprovider != null && publicprovider.IsCCRMTimeData(this._FieldName));

            bool isFeetInchesData = (publicprovider != null && publicprovider.IsFeetInchesData(this._FieldName));

            if (!i_Table.Columns.Contains(this._FieldName)) goto EXIT;

            type= i_Table.Columns[_FieldName].DataType;

            foreach (int i_RowIndex in i_Rows)
            {
                try
                {
                    #region Old
                    //string strValue = i_Table.Rows[i_RowIndex][this._FieldName].ToString();

                    //if (strValue == null || strValue == string.Empty)
                    //{
                    //    continue;
                    //}

                    //double fieldValue = Convert.ToDouble(strValue);

                    //switch (i_Type)
                    //{
                    //    case Data.StatTypes.AveragePlus:
                    //        if (fieldValue > 0) m_Sum += fieldValue;
                    //        break;

                    //    case Data.StatTypes.AverageMinus:
                    //        if (fieldValue < 0) m_Sum += fieldValue;
                    //        break;

                    //    case Data.StatTypes.Average:
                    //    default:
                    //        m_Sum += fieldValue;
                    //        break;
                    //}
                    #endregion

                    object objValue = i_Table.Rows[i_RowIndex][this._FieldName];

                    if (objValue == null || objValue is System.DBNull) continue;

                    if (isTimeData)
                    {
                        TimeSpan timeSpan = Webb.Utility.ConvertToTimeTicks(objValue.ToString());

                        totalDateTime = totalDateTime.Add(timeSpan);
                    }
                    if (isFeetInchesData)
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
                            case Data.StatTypes.AveragePlus:
                                if (fieldValue > 0) m_Sum += fieldValue;
                                break;

                            case Data.StatTypes.AverageMinus:
                                if (fieldValue < 0) m_Sum += fieldValue;

                                break;

                            case Data.StatTypes.Average:
                            default:
                                m_Sum += fieldValue;
                                break;
                        }
                        #endregion
                    }
                }
                catch { continue; }
            }

        EXIT:
            if (i_Rows.Count > 0)
            {
                #region Old
                //this._Result.Add(m_Sum / i_Rows.Count);
                #endregion

                object objResultValue = string.Empty;

                #region New
                if (type == typeof(DateTime))
                {
                    objResultValue = new DateTime(timeTicks / i_Rows.Count);

                    objResultValue = CResolveFieldValue.GetResolveValue(this._FieldName, @"M/d/yy", objResultValue);
                }
                if (isTimeData)
                {
                    timeTicks = totalDateTime.Ticks / i_Rows.Count;

                    DateTime dateTime = new DateTime(timeTicks);

                    bool outputTimeFormat = Webb.Utility.IsTimeFormatForOut(i_Table, i_OuterRows, _FieldName);

                    if (outputTimeFormat)
                    {
                        if (dateTime.Hour > 0)
                        {
                            objResultValue = dateTime.ToString("h:m:s.ff");

                        }
                        else
                        {
                            objResultValue = dateTime.ToString("m:s.ff"); ;
                        }
                    }
                    else
                    {
                        objResultValue = dateTime.TimeOfDay.TotalSeconds;
                    }              

                }
                else if (isFeetInchesData)
                {
                    m_Sum = m_Sum / i_Rows.Count;

                    objResultValue = Webb.Utility.FormatFeetInchData(m_Sum);
                }
                else
                {
                    objResultValue = m_Sum / i_Rows.Count;
                }
                #endregion

                this._Result.Add(objResultValue);
            }
            else
            {
                this._Result.Add(0);
            }
        }

        //04-11-2008@Scott
        private void CalculateTotal(DataTable i_Table, Webb.Collections.Int32Collection i_OuterRows, Webb.Collections.Int32Collection i_Rows, Data.StatTypes i_Type)
        {
            decimal m_Total = 0,fieldValue=0m;

            DateTime totalDateTime = new DateTime(0);
            
            Webb.Reports.DataProvider.WebbDataProvider publicprovider = Webb.Reports.DataProvider.VideoPlayBackManager.PublicDBProvider;          

            bool isTimeData = (publicprovider != null && publicprovider.IsCCRMTimeData(this._FieldName));

            bool isFeetInchesData = (publicprovider != null && publicprovider.IsFeetInchesData(this._FieldName));

            if (!i_Table.Columns.Contains(this._FieldName)) goto EXIT;

            foreach (int i_RowIndex in i_Rows)
            {
                try
                {
                    #region Old
                    //string strValue = i_Table.Rows[i_RowIndex][this._FieldName].ToString();

                    //if (strValue == null || strValue == string.Empty)
                    //{
                    //    continue;
                    //}

                    //decimal fieldValue = Convert.ToDecimal(strValue);

                    //switch (i_Type)
                    //{
                    //    case Data.StatTypes.TotalPlus:
                    //        if (fieldValue > 0) m_Total += fieldValue;
                    //        break;

                    //    case Data.StatTypes.TotalMinus:
                    //        if (fieldValue < 0) m_Total += fieldValue;
                    //        break;

                    //    case Data.StatTypes.Total:
                    //    default:
                    //        m_Total += fieldValue;
                    //        break;
                    //}
                    #endregion

                    #region New
                    object objValue = i_Table.Rows[i_RowIndex][this._FieldName];

                    if (objValue == null || objValue is System.DBNull) continue;

                    if (isTimeData)
                    {
                        TimeSpan timeSpan = Webb.Utility.ConvertToTimeTicks(objValue.ToString());

                        totalDateTime = totalDateTime.Add(timeSpan);
                    }
                    if (isFeetInchesData)
                    {
                        fieldValue = Webb.Utility.ConvertFeetInchToNum(objValue.ToString());

                        m_Total += fieldValue;
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
                            case Data.StatTypes.AveragePlus:
                                if (fieldValue > 0) m_Total += fieldValue;
                                break;

                            case Data.StatTypes.AverageMinus:
                                if (fieldValue < 0) m_Total += fieldValue;

                                break;

                            case Data.StatTypes.Average:
                            default:
                                m_Total += fieldValue;
                                break;
                        }
                        #endregion
                    }
                    #endregion
                }
                catch { continue; }
            }
        EXIT:
            if (isTimeData)
            {
                object objResultValue = string.Empty;

                #region CCRM Time Description

                bool outputTimeFormat = Webb.Utility.IsTimeFormatForOut(i_Table, i_OuterRows, _FieldName);

                if (outputTimeFormat)
                {
                    if (totalDateTime.Hour > 0)
                    {
                        objResultValue = totalDateTime.ToString("h:m:s.ff");

                    }
                    else
                    {
                        objResultValue = totalDateTime.ToString("m:s.ff"); ;
                    }
                }
                else
                {
                    objResultValue = totalDateTime.TimeOfDay.TotalSeconds; ;
                }

                this._Result.Add(objResultValue);
            
                #endregion
            }
            else if (isFeetInchesData)
            {
                this._Result.Add(Webb.Utility.FormatFeetInchData(m_Total));               
            }
            else
            {
                this._Result.Add(m_Total);
            }
            
        }

        //Modified at 2008-11-11 16:12:33@Scott
        private void CalculateOneValuePercent(DataTable i_Table, Webb.Collections.Int32Collection i_FilteredRows/*outer filter*/, Webb.Collections.Int32Collection i_Rows/*inner filter*/)
        {
            Int32Collection filteredRows = this.DenominatorFilter.GetFilteredRows(i_Table);	//Modified at 2008-11-28 11:11:57@Scott

            if (i_Table != null && filteredRows.Count > 0)
            {
                float percent = (float)(i_Rows.Count) / filteredRows.Count;

                this._Result.Add(percent);
            }
            else
            {
                this._Result.Add(0.0f);
            }
        }

        //Modified at 2008-11-20 13:31:43@Scott
        private void CalculateDistFrequence(DataTable i_Table, Webb.Collections.Int32Collection i_Rows)
        {
            if (i_Table.Columns.Contains(this._FieldName))
            {
                ArrayList arrValue = new ArrayList();

                foreach (int row in i_Rows)
                {
                    object value = i_Table.Rows[row][this._FieldName];

                    if (arrValue.Contains(value))
                    {
                        continue;
                    }
                    else
                    {
                        arrValue.Add(value);
                    }
                }

                this._Result.Add(arrValue.Count);
            }
            else
            {
                this._Result.Add(0);
            }
        }

        //Modified at 2008-11-20 13:31:48@Scott
        private void CalculateDistPercent(DataTable i_Table, Webb.Collections.Int32Collection i_OuterRows, Webb.Collections.Int32Collection i_InnerRows)
        {
            int nInnerCount = 0, nOuterCount = 0;

            if (i_Table.Columns.Contains(this._FieldName))
            {
                ArrayList arrValue = new ArrayList();

                foreach (int row in i_InnerRows)
                {
                    object value = i_Table.Rows[row][this._FieldName];

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
                    object value = i_Table.Rows[row][this._FieldName];

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
                    this._Result.Add(0);
                }
                else
                {
                    this._Result.Add((double)nInnerCount / nOuterCount);
                }
            }
            else
            {
                this._Result.Add(0);
            }
        }
        #endregion
    }
    #endregion

    #region public class StatInfoCollection
    [Serializable]
    public class StatInfoCollection : CollectionBase
    {
        public StatInfo this[int i_Index]
        {
            get { return this.InnerList[i_Index] as StatInfo; }
            set { this.InnerList[i_Index] = value; }
        }
        //ctor
        public StatInfoCollection() { }
        //Methods
        public int Add(StatInfo i_Object)
        {
            return this.InnerList.Add(i_Object);
        }
        public void Remove(StatInfo i_Obj)
        {
            this.InnerList.Remove(i_Obj);
        }
    }
    #endregion

}