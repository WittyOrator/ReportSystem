// 10-13-2011 Scott

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
    [Serializable]
    public class HorizontalGridData : ISerializable
    {
        protected Int32Collection _RowIndicators;
        [Browsable(false)]
        public Int32Collection RowIndicators
        {
            get 
            {
                if (_RowIndicators == null)
                {
                    _RowIndicators = new Int32Collection();
                }
 
                return _RowIndicators; 
            }
        }

        protected string _DateFormat = @"M/d/yy";
        [Editor(typeof(Webb.Reports.Editors.FormatDateTimeEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Category("Data Format")]
        public string DateFormat
        {
            get
            {
                return this._DateFormat;
            }
            set
            {
                this._DateFormat = value;
            }
        }

        protected string _FieldTitle = string.Empty;
        public string FieldTitle
        {
            get { return _FieldTitle; }
            set { _FieldTitle = value; }
        }

        protected string _UnitsTitle = string.Empty;
        public string UnitsTitle
        {
            get { return _UnitsTitle; }
            set { _UnitsTitle = value; }
        }

        protected SortingByTypes _SortingBy = SortingByTypes.DateTime;
        [Category("Sorting")]
        public SortingByTypes SortingBy
        {
            get { return _SortingBy; }
            set { _SortingBy = value; }
        }

        protected SortingTypes _Sorting = SortingTypes.Descending;
        [Category("Sorting")]
        public SortingTypes Sorting
        {
            get { return _Sorting; }
            set { _Sorting = value; }
        }

        protected string _SortingField = string.Empty;
        [Editor(typeof(Webb.Reports.Editors.FieldSelectEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Category("Sorting")]
        public string SortingField
        {
            get { return _SortingField; }
            set { _SortingField = value; }
        }

        protected int _TopCount = 7;
        public int TopCount
        {
            get { return _TopCount; }
            set { _TopCount = value; }
        }

        public HorizontalGridData()
        {
            
        }

        public void GetResult(System.Data.DataTable i_Table, Webb.Collections.Int32Collection i_OuterRows)
        {
            RowIndicators.Clear();

            FieldGroupInfo m_GroupInfo = new FieldGroupInfo();
            m_GroupInfo.GroupByField = this.SortingField;
            m_GroupInfo.ColumnHeading = this.SortingField;

            m_GroupInfo.CalculateGroupResult(i_Table, i_OuterRows, i_OuterRows, m_GroupInfo);

            if (m_GroupInfo.GroupResults == null || m_GroupInfo.GroupResults.Count <= 0) return;

            m_GroupInfo.GroupResults.Sort(this.Sorting, this.SortingBy, m_GroupInfo.UserDefinedOrders);

            foreach (GroupResult gr in m_GroupInfo.GroupResults)
            {
                if (gr.RowIndicators.Count > 0)
                {
                    RowIndicators.Add(gr.RowIndicators[0]);
                }
            }
        }

        public HorizontalGridData Copy()
        {
            HorizontalGridData copyHorizontalGridData = new HorizontalGridData();

            copyHorizontalGridData._SortingField = this.SortingField;

            copyHorizontalGridData._SortingBy = this.SortingBy;

            copyHorizontalGridData._Sorting = this.Sorting;

            copyHorizontalGridData._FieldTitle = this.FieldTitle;

            copyHorizontalGridData._UnitsTitle = this.UnitsTitle;

            copyHorizontalGridData._TopCount = this.TopCount;

            copyHorizontalGridData._DateFormat = this.DateFormat;

            return copyHorizontalGridData;
        }

        public virtual void GetALLUsedFields(ref ArrayList usedFields)
        {
            if (usedFields.Contains(this.SortingField)) return;

            usedFields.Add(this.SortingField);
        }

        #region Serialization By Scott 2011-10-13
        virtual public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            info.AddValue("_SortingByField", _SortingField);
            info.AddValue("_SortingByType", _SortingBy, typeof(SortingByTypes));
            info.AddValue("_SortingType", _Sorting, typeof(SortingTypes));
            info.AddValue("_FieldTitle", _FieldTitle);
            info.AddValue("_UnitsTitle", _UnitsTitle);
            info.AddValue("_TopCount", _TopCount);
            info.AddValue("_DateFormat", _DateFormat);
        }

        public HorizontalGridData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            try
            {
                _SortingField = info.GetString("_SortingByField");
            }
            catch
            {
                _SortingField = string.Empty;
            }

            try
            {
                _SortingBy = (SortingByTypes)info.GetValue("_SortingByType",typeof(SortingByTypes));
            }
            catch
            {
                _SortingBy = SortingByTypes.DateTime;
            }

            try
            {
                _Sorting = (SortingTypes)info.GetValue("_SortingType", typeof(SortingTypes));
            }
            catch
            {
                _Sorting = SortingTypes.Descending;
            }

            try
            {
                _FieldTitle = info.GetString("_FieldTitle");
            }
            catch
            {
                _FieldTitle = string.Empty;
            }

            try
            {
                _UnitsTitle = info.GetString("_UnitsTitle");
            }
            catch
            {
                _UnitsTitle = string.Empty;
            }

            try
            {
                _TopCount = info.GetInt32("_TopCount");
            }
            catch
            {
                _TopCount = 7;
            }

            try
            {
                _DateFormat = info.GetString("_DateFormat");
            }
            catch
            {
                _DateFormat = @"M/d/yy";
            }
        }
        #endregion
    }
}
