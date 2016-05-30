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
using Webb.Reports.DataProvider;

using Webb.Reports.ExControls.Data;

namespace Webb.Reports.ExControls.Views		//Don't change namespace
{

    [Serializable]
    public enum SortingFrequence : byte       //Added this Enum at 2008-11-28 10:34:37@Simon
    {
        None = 0,
        ShowBeforeColumns,
        ShowAfterColumns
    }

    #region public class SortingColumn
    [Serializable]
    public class SortingColumn : ISerializable
    {
        protected string _SortingColumn = "<None>";
        protected Webb.Reports.ExControls.Data.SortingTypes _SortingTypes;
        protected Webb.Reports.ExControls.Data.SortingByTypes _SortingByTypes;

        [Browsable(true), Category("Sorting")]
        [Editor(typeof(Webb.Reports.Editors.FieldSelectEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string SortingBy
        {
            get
            {
                if (_SortingColumn == null) _SortingColumn = "<None>";
                return this._SortingColumn;
            }
            set
            {
                this._SortingColumn = value;
            }
        }

        [Browsable(true), Category("Sorting")]
        public Webb.Reports.ExControls.Data.SortingTypes SortingTypes
        {
            get
            {
                return this._SortingTypes;
            }
            set
            {
                this._SortingTypes = value;
            }
        }

        [Browsable(true), Category("Sorting")]
        public Webb.Reports.ExControls.Data.SortingByTypes SortingByTypes
        {
            get
            {
                return this._SortingByTypes;
            }
            set
            {
                this._SortingByTypes = value;
            }
        }

        public SortingColumn Copy()
        {
            SortingColumn sortingColumn = new SortingColumn();
            sortingColumn.SortingByTypes = this.SortingByTypes;
            sortingColumn.SortingBy = this.SortingBy;
            sortingColumn.SortingTypes = this.SortingTypes;
            return sortingColumn;
        }

        public SortingColumn()
        {
            this.SortingByTypes = SortingByTypes.Frequence;
            this.SortingTypes = Data.SortingTypes.Descending;
            this.SortingBy = "<None>";
        }

        #region Serialization By Macro 2009-2-9 16:47:08
        public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            info.AddValue("_SortingColumn", _SortingColumn, typeof(string));
            info.AddValue("_SortingTypes", _SortingTypes, typeof(Webb.Reports.ExControls.Data.SortingTypes));
            info.AddValue("_SortingByTypes", _SortingByTypes, typeof(Webb.Reports.ExControls.Data.SortingByTypes));
        }

        public SortingColumn(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            try
            {
                _SortingColumn = (string)info.GetValue("_SortingColumn", typeof(string));
            }
            catch
            {
                _SortingColumn = "<None>";
            }
            try
            {
                _SortingTypes = (Webb.Reports.ExControls.Data.SortingTypes)info.GetValue("_SortingTypes", typeof(Webb.Reports.ExControls.Data.SortingTypes));
            }
            catch
            {
                _SortingTypes = Data.SortingTypes.Descending;
            }
            try
            {
                _SortingByTypes = (Webb.Reports.ExControls.Data.SortingByTypes)info.GetValue("_SortingByTypes", typeof(Webb.Reports.ExControls.Data.SortingByTypes));
            }
            catch
            {
                _SortingByTypes = Data.SortingByTypes.Frequence;
            }
        }
        #endregion
    }
    #endregion

    #region public class SortingColumnCollection : CollectionBase
    [Serializable]
    public class SortingColumnCollection : CollectionBase
    {
        public SortingColumn this[int i_Index]
        {
            get { return this.InnerList[i_Index] as SortingColumn; }
            set { this.InnerList[i_Index] = value; }
        }

        public SortingColumnCollection() { }

        public int Add(SortingColumn i_Object)
        {
            return this.InnerList.Add(i_Object);
        }

        public void Remove(SortingColumn i_Obj)
        {
            this.InnerList.Remove(i_Obj);
        }

        public bool Contain(SortingColumn i_Obj)
        {
            return this.InnerList.Contains(i_Obj);
        }

        public void CopyTo(SortingColumnCollection columns)
        {
            if (object.ReferenceEquals(this, columns)) return;

            columns.Clear();

            foreach (SortingColumn col in this)
            {
                columns.Add(col.Copy());
            }
        }


        //Modified at 2009-2-10 11:02:43@Scott
        public GroupResultCollection Sorting(DataTable dt, Int32Collection filteredRows)
        {
            if (filteredRows == null || filteredRows.Count == 0) return null;

            if (this.Count <= 0) return null;

            FieldGroupInfo rootGroupInfo = null, tempGroupInfo = null, subGroupInfo = null;

            SortingColumn sColumn = null;

            int index = 0;

            for (; index < this.InnerList.Count; index++)
            {
                sColumn = this[index];

                if (dt.Columns.Contains(sColumn.SortingBy))
                {
                    rootGroupInfo = new FieldGroupInfo(sColumn.SortingBy);
                    rootGroupInfo.Sorting = sColumn.SortingTypes;
                    rootGroupInfo.SortingBy = sColumn.SortingByTypes;

                    tempGroupInfo = rootGroupInfo;

                    break;
                }
            }

            index++;

            for (; index < this.InnerList.Count; index++)
            {
                sColumn = this[index];

                if (!dt.Columns.Contains(sColumn.SortingBy))
                {
                    continue;
                }

                subGroupInfo = new FieldGroupInfo(sColumn.SortingBy);
                subGroupInfo.Sorting = sColumn.SortingTypes;
                subGroupInfo.SortingBy = sColumn.SortingByTypes;

                tempGroupInfo.SubGroupInfos.Add(subGroupInfo);

                tempGroupInfo = subGroupInfo;
            }

            GroupResultCollection results = new GroupResultCollection();

            if (rootGroupInfo != null)	//Modified at 2009-2-16 9:51:48@Scott
            {
                rootGroupInfo.CalculateGroupResult(dt, filteredRows, filteredRows, rootGroupInfo);

                this.GetLeafGroupResults(rootGroupInfo, results);
            }

            if (results.Count <= 0) return null;

            return results;
        }

        //Modified at 2009-2-10 11:02:50@Scott
        public void GetLeafGroupResults(GroupInfo groupInfo, GroupResultCollection results)
        {
            groupInfo.GroupResults.Sort(groupInfo.Sorting, groupInfo.SortingBy, groupInfo.UserDefinedOrders);

            foreach (GroupResult result in groupInfo.GroupResults)
            {
                GroupResultCollection tempResults = new GroupResultCollection();

                tempResults = groupInfo.GroupResults;

                if (result.SubGroupInfos.Count == 0)
                {
                    results.Add(result);
                }

                foreach (GroupInfo subGroupInfo in result.SubGroupInfos)
                {
                    this.GetLeafGroupResults(subGroupInfo, results);
                }
            }
        }

    }
    #endregion

    #region public class GridColumn
    [Serializable]
    public class GridColumn : ISerializable
    {

        #region Serialization By Macro 2008-12-11 14:23:05
        public virtual void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            info.AddValue("_Field", _Field, typeof(string));
            info.AddValue("_Title", _Title, typeof(string));
            info.AddValue("_Style", _Style, typeof(Webb.Reports.ExControls.BasicStyle));
            info.AddValue("_ColumnIndex", _ColumnIndex, typeof(int));
            info.AddValue("_TitleFormat", _TitleFormat, typeof(System.Drawing.StringFormatFlags));
            info.AddValue("_ColumnWidth", _ColumnWidth, typeof(int));
            info.AddValue("_ColorNeedChange", _ColorNeedChange);	//Modified at 2009-2-16 11:23:49@Scott
            info.AddValue("_Description", this._Description);	//Modified at 2009-2-16 11:23:49@Scott
            info.AddValue("_DateFormat", _DateFormat);
            info.AddValue("_DisplayAsImage", _DisplayAsImage);
            info.AddValue("_ImageRowHeight", this._ImageRowHeight);
            info.AddValue("_Units", _Units, typeof(string));
        }
        public GridColumn(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            try
            {
                _ImageRowHeight = info.GetInt32("_ImageRowHeight");
            }
            catch
            {
                _ImageRowHeight = 100;
            }
            try
            {
                _Description = (string)info.GetValue("_Description", typeof(string));
            }
            catch
            {
                _Description = string.Empty;
            }
            try
            {
                _Field = (string)info.GetValue("_Field", typeof(string));
            }
            catch
            {
                _Field = string.Empty;
            }
            try
            {
                _Title = (string)info.GetValue("_Title", typeof(string));
            }
            catch
            {
                _Title = string.Empty;
            }
            try
            {
                _Style = (Webb.Reports.ExControls.BasicStyle)info.GetValue("_Style", typeof(Webb.Reports.ExControls.BasicStyle));
            }
            catch
            {
                _Style = new BasicStyle();
            }
            try
            {
                _ColumnIndex = (int)info.GetValue("_ColumnIndex", typeof(int));
            }
            catch
            {
                _ColumnIndex = 0;
            }
            try
            {
                _TitleFormat = (System.Drawing.StringFormatFlags)info.GetValue("_TitleFormat", typeof(System.Drawing.StringFormatFlags));
            }
            catch
            {
                _TitleFormat = (System.Drawing.StringFormatFlags)0;
            }
            try
            {
                _ColumnWidth = (int)info.GetValue("_ColumnWidth", typeof(int));
            }
            catch
            {
                _ColumnWidth = 0;
            }
            try	//Modified at 2009-2-16 11:24:42@Scott
            {
                _ColorNeedChange = info.GetBoolean("_ColorNeedChange");
            }
            catch
            {
                _ColorNeedChange = true;
            }
            try
            {
                _DateFormat = info.GetString("_DateFormat");
            }
            catch
            {
                _DateFormat = @"M/d/yy";
            }
            try	  //Add this code at 2011-5-11 14:39:35@simon
            {
                _DisplayAsImage = info.GetBoolean("_DisplayAsImage");
            }
            catch
            {
                _DisplayAsImage = false;
            }
            try
            {
                _Units = (string)info.GetValue("_Units", typeof(string));
            }
            catch
            {
                _Units = string.Empty;
            }
        }
        #endregion

        protected string _Field;
        protected string _Title;
        protected string _Units;
        protected BasicStyle _Style;				//06-04-2008@Scott
        protected int _ColumnIndex;					//06-04-2008@Scott
        protected StringFormatFlags _TitleFormat;	//06-26-2008@Scott
        protected int _ColumnWidth;					//06-30-2008@Scott
        protected bool _ColorNeedChange;			//Modified at 2009-2-16 11:20:44@Scott

        protected string _Description = null;

        protected bool _DisplayAsImage = false;       

        protected int _ImageRowHeight = 100;

        [Browsable(true), Category("Image In Column")]
        public int ImageRowHeight
        {
            get { return this._ImageRowHeight; }
            set { this._ImageRowHeight = value < 0 ? 100 : value; }
        }


        protected string _DateFormat =@"M/d/yy";

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

        //[NonSerialized]
        //public Int32Collection RelatedColumns = new Int32Collection();


        [Browsable(false)]
        public string Description
        {
            get
            {
                return this._Description;
            }
            set
            {
                this._Description = value;
            }
        }

        public int ColumnWidth
        {
            get { return this._ColumnWidth; }
            set { this._ColumnWidth = value; }
        }

        public StringFormatFlags TitleFormat
        {
            get { return this._TitleFormat; }
            set { this._TitleFormat = value; }
        }      

        [Browsable(false)]
        public int ColumnIndex
        {
            get { return this._ColumnIndex; }
            set { this._ColumnIndex = value; }
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

        [Browsable(true), Category("Style")]
        public bool ColorNeedChange
        {
            get { return this._ColorNeedChange; }
            set { this._ColorNeedChange = value; }
        }

        [Browsable(false), Category("Image In Column")]
        public bool DisplayAsImage
        {
            get { return this._DisplayAsImage; }
            set { this._DisplayAsImage = value; }

        }

        [Editor(typeof(Webb.Reports.Editors.FieldSelectEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Browsable(false)]
        public string Field
        {
            get { return this._Field; }
            set
            {
                if (this._Field != value)
                {
                    this._Field = value;

                    this.Title = value;
                }
            }
        }

        public string Title
        {
            get { return this._Title; }
            set { this._Title = value; }
        }

        public string Units
        {
            get { return this._Units; }
            set { this._Units = value; }
        }

        public GridColumn()
        {
            this._Field = string.Empty;
            this._Title = "New Column";
            this._Units = string.Empty;
            this.Style = new BasicStyle();
            this._ColumnWidth = BasicStyle.ConstValue.CellWidth;
            this._ColorNeedChange = true;
        }

        public GridColumn(string strField)
        {
            this._Field = strField;

            this._Title = strField;

            this._Units = string.Empty;

            this.Style = new BasicStyle();
            this._ColumnWidth = BasicStyle.ConstValue.CellWidth;

            this._Description = strField;

            //08-18-2008@Scott
            Webb.Reports.ExControls.Data.ColumnStyle style = Webb.Reports.ExControls.Data.ColumnManager.PublicColumnStyles.ColumnStyles[this._Field];

            this._ColorNeedChange = true;
            
            #region Set Style
            if (style != null)
            {
                this.ColumnWidth = style.ColumnWidth;

                this.Style = style.Style.Copy() as BasicStyle;

                this._TitleFormat = style.TitleFormat;

                this.Title = style.ColumnHeading;

                this._ColorNeedChange = style.ColorNeedChange;
            }
            #endregion

            CResolveFieldValue cResolvePathIntoImage = new CResolveFieldValue(strField);

            this.DisplayAsImage = cResolvePathIntoImage.DisplayAsImage;

            if (cResolvePathIntoImage.DisplayAsImage)
            {
                this._ColumnWidth = Math.Max(_ColumnWidth, cResolvePathIntoImage.ColumnWidth);
            }           

        }

        public GridColumn(string strField, string strDefaultHeader)
        {
            this._Field = strField;

            this._Title = strDefaultHeader;

            this._Units = string.Empty;

            this.Style = new BasicStyle();

            this._ColumnWidth = BasicStyle.ConstValue.CellWidth;

            this._Description = strField;           

            #region Initalize the styles

            //08-18-2008@Scott
            Webb.Reports.ExControls.Data.ColumnStyle style = Webb.Reports.ExControls.Data.ColumnManager.PublicColumnStyles.ColumnStyles[this._Field];

            this._ColorNeedChange = true;

            if (style != null)
            {
                this.ColumnWidth = style.ColumnWidth;

                this.Style = style.Style.Copy() as BasicStyle;

                this._TitleFormat = style.TitleFormat;

                //this.Title = style.ColumnHeading;

                this._ColorNeedChange = style.ColorNeedChange;
            }

            #endregion

            CResolveFieldValue cResolvePathIntoImage = new CResolveFieldValue(strField);

            this.DisplayAsImage = cResolvePathIntoImage.DisplayAsImage;

            if (cResolvePathIntoImage.DisplayAsImage)
            {
                this._ColumnWidth = Math.Max(_ColumnWidth, cResolvePathIntoImage.ColumnWidth);
            }           
        }    

       

        public override string ToString()
        {
            return this.Field;
        }

        public virtual GridColumn Copy()
        {
            GridColumn newGridColumn = new GridColumn();

            newGridColumn._Field = this.Field;
            newGridColumn._Title = this.Title;
            newGridColumn._Units = this.Units;
            newGridColumn.Style = (BasicStyle)this.Style.Copy();
            newGridColumn._TitleFormat = this.TitleFormat;
            newGridColumn._ColumnWidth = this.ColumnWidth;
            newGridColumn._ColorNeedChange = this.ColorNeedChange;	//Modified at 2009-2-16 11:25:16@Scott
            newGridColumn._Description = this.Description;
            newGridColumn.DateFormat = this.DateFormat;
            newGridColumn._DisplayAsImage = this.DisplayAsImage;
            newGridColumn._ImageRowHeight = this.ImageRowHeight;

            return newGridColumn;
        }

        public virtual void GetALLUsedFields(ref ArrayList  usedFields)
        {
             if (usedFields.Contains(this.Field)) return;

             usedFields.Add(this.Field); 
        }

        public virtual object GetValue(DataRow dr)
        {
            if (dr == null || !dr.Table.Columns.Contains(this.Field)) return string.Empty;

            return dr[this.Field];
        }

    }

    #endregion

    //12-10-2008@Scott
    #region public CombinedGridColumn : GridColumn, ISerializable    {
    [Serializable]
    public class CombinedGridColumn : GridColumn, ISerializable
    {
        //Ctor
        public CombinedGridColumn()
            : base()
        {
            this._CombinedFields = new StringCollection();

            this.Title = "New Compute Column";

        }
        public override string ToString()
        {
            string strHeader = this.CalculatingFormula.GetDefaultHeader();

            return "[" + strHeader + "]";
        }
      
       #region Fields & properties

        #region Old Properties
        //Members
        protected StringCollection _CombinedFields;

        [Browsable(false),Category("CombinedColumn Setting")]
        [EditorAttribute(typeof(Webb.Reports.ExControls.Editors.FieldsGroupEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public StringCollection CombinedFields
        {
            get
            {
                if (this._CombinedFields == null) this._CombinedFields = new StringCollection();

                return this._CombinedFields;
            }
            set
            {
                this._CombinedFields = value;    
            }
        }              

        protected string _ValueSpliter = " ";

        [Browsable(false), Category("CombinedColumn Setting")]
        public string ValueSpliter
        {
            get
            {
                if (_ValueSpliter == null) _ValueSpliter = " ";
                return this._ValueSpliter;
            }

            set
            {                              
                 this._ValueSpliter = value; 
            }
        }

     
       protected CombinedOperation _CombinedOperation = CombinedOperation.StrConcat;
       [Browsable(false), Category("CombinedColumn Setting")]
       public CombinedOperation ComputeOperation
       {
           get
           {
               return this._CombinedOperation;
           }
           set
           {                        
               this._CombinedOperation = value;               
           }
       }

       protected CalculateFormulaDealer _RelatedCalculateFormulaDealer = null;
       [Browsable(false)]
       public CalculateFormulaDealer RelatedCalculateFormulaDealer
       {
           get
           {
               return this._RelatedCalculateFormulaDealer;
           }
           set
           {
               _RelatedCalculateFormulaDealer = value;
           }
       }
        #endregion

       protected CalculateColumnInfo _CalculateInfoSetting = new CalculateColumnInfo();
       [Category("Calculating formula")]
       [EditorAttribute(typeof(Webb.Reports.ExControls.Editors.CalculateColumnEditor), typeof(System.Drawing.Design.UITypeEditor))]
       public CalculateColumnInfo CalculatingFormula
       {
           get
           {
               if (_CalculateInfoSetting == null) _CalculateInfoSetting = new CalculateColumnInfo();

               return _CalculateInfoSetting;
           }
           set
           {
               _CalculateInfoSetting = value;

               this._Title = _CalculateInfoSetting.GetDefaultHeader();

               this._Field = this._Title;
           }
       }

       #endregion

        public override void GetALLUsedFields(ref ArrayList usedFields)
        {            
            foreach (string strGroupByField in this.CombinedFields)
            {
                if (!usedFields.Contains(strGroupByField))
                {
                    usedFields.Add(strGroupByField);
                }
            }          
      
        }

        #region Sub Functions for AddFieldsColumn  StrConcat /NumAdd / NumSubtract /NumMulti /NumDivide
        private string ConcatString(DataRow dataRow)
        {
            StringBuilder sbExpression = new StringBuilder();

            int IndexOfSpecialStr = this.ValueSpliter.IndexOf("{0}");

            for (int i = 0; i < this.CombinedFields.Count; i++)
            {
                string strGroupByField = this.CombinedFields[i];

                if (!dataRow.Table.Columns.Contains(strGroupByField)) continue;

                object objValue = dataRow[strGroupByField];

                if (IndexOfSpecialStr >= 0)
                {
                    #region Deal Specal Format

                    string strValue = string.Empty;

                    if (objValue == null || objValue is DBNull || objValue.ToString().Trim() == string.Empty)
                    {
                        strValue = this.ValueSpliter.Replace("{0}", string.Empty);

                        sbExpression.Append(strValue);

                    }
                    else
                    {
                        strValue = this.ValueSpliter.Replace("{0}", objValue.ToString());

                        sbExpression.AppendFormat("{0}", strValue);
                    }

                    #endregion
                }
                else
                {
                    #region Split Simple

                    if (objValue == null || objValue is DBNull || objValue.ToString().Trim() == string.Empty) continue;

                    string strValue = objValue.ToString();

                    if (sbExpression.Length > 0)
                    {
                        sbExpression.AppendFormat("{0}{1}", this.ValueSpliter, strValue);
                    }
                    else
                    {
                        sbExpression.AppendFormat("{0}", strValue);
                    }
                    #endregion
                }
            }
            if (IndexOfSpecialStr >= 0)
            {
                string firstString = this.ValueSpliter.Substring(0, IndexOfSpecialStr);

                string endString = this.ValueSpliter.Substring(IndexOfSpecialStr + 3);

                if (firstString != string.Empty && firstString == endString)
                {
                    sbExpression = sbExpression.Replace(firstString + endString, firstString);
                }
            }

            return sbExpression.ToString();
        }

        private string AddNumbersIntoAColumn(DataRow dataRow)
        {
            decimal totalNumber = 0m;

            for (int i = 0; i < this.CombinedFields.Count; i++)
            {
                string strGroupByField = this.CombinedFields[i];

                if (!dataRow.Table.Columns.Contains(strGroupByField)) continue;

                object objValue = dataRow[strGroupByField];

                if (objValue == null || objValue is DBNull || objValue.ToString().Trim() == string.Empty) continue;

                decimal number = 0m;

                bool convertOK = decimal.TryParse(objValue.ToString(), out number);

                if (!convertOK) continue;

                totalNumber += number;
            }

            return totalNumber.ToString();
        }

        private string SubtractNumbersIntoAColumn(DataRow dataRow)
        {
            decimal totalNumber = 0m;

            for (int i = 0; i < CombinedFields.Count; i++)
            {
                string strGroupByField = this.CombinedFields[i];

                if (!dataRow.Table.Columns.Contains(strGroupByField)) continue;

                object objValue = dataRow[strGroupByField];

                if (objValue == null || objValue is DBNull || objValue.ToString().Trim() == string.Empty) continue;

                decimal number = 0m;

                bool convertOK = decimal.TryParse(objValue.ToString(), out number);

                if (!convertOK) continue;

                if (i == 0)
                {
                    totalNumber = number;
                }
                else
                {
                    totalNumber -= number;
                }

            }
            return totalNumber.ToString();
        }

        private string MultiPlayNumbersIntoAColumn(DataRow dataRow)
        {
            decimal totalNumber = 1m;

            for (int i = 0; i < CombinedFields.Count; i++)
            {
                string strGroupByField = this.CombinedFields[i];

                if (!dataRow.Table.Columns.Contains(strGroupByField)) continue;

                object objValue = dataRow[strGroupByField];

                if (objValue == null || objValue is DBNull || objValue.ToString().Trim() == string.Empty) continue;

                decimal number = 0m;

                bool convertOK = decimal.TryParse(objValue.ToString(), out number);

                if (!convertOK) continue;

                totalNumber *= number;
            }

            return totalNumber.ToString();
        }

        private string DivideNumbersIntoAColumn(DataRow dataRow)
        {
            decimal totalNumber = 1m;

            for (int i = 0; i < CombinedFields.Count; i++)
            {
                string strGroupByField = this.CombinedFields[i];

                if (!dataRow.Table.Columns.Contains(strGroupByField)) continue;

                object objValue = dataRow[strGroupByField];

                if (objValue == null || objValue is DBNull || objValue.ToString().Trim() == string.Empty) continue;

                decimal number = 0m;

                bool convertOK = decimal.TryParse(objValue.ToString(), out number);

                if (!convertOK || number == 0) continue;
                if (i == 0)
                {
                    totalNumber = number;
                }
                else
                {
                    totalNumber /= number;
                }
            }       

            if (totalNumber * 100 != (int)(totalNumber * 100))
            {
                totalNumber = decimal.Round(totalNumber, 2);
            }

            return totalNumber.ToString();
        }
        private string AverageNumbersIntoAColumn(DataRow dataRow)
        {
            decimal totalNumber = 0m;

            for (int i = 0; i < CombinedFields.Count; i++)
            {
                string strGroupByField = this.CombinedFields[i];

                if (!dataRow.Table.Columns.Contains(strGroupByField)) continue;

                object objValue = dataRow[strGroupByField];

                if (objValue == null || objValue is DBNull || objValue.ToString().Trim() == string.Empty) continue;

                decimal number = 0m;

                bool convertOK = decimal.TryParse(objValue.ToString(), out number);

                if (!convertOK) continue;

                totalNumber += number;
            }

            if (CombinedFields.Count > 0) totalNumber /= CombinedFields.Count;

            if (totalNumber * 100 != (int)(totalNumber * 100))
            {
                totalNumber = decimal.Round(totalNumber, 2);
            }

            return totalNumber.ToString();
        }

        #endregion

        public override object GetValue(DataRow dr)
        {
            //CombinedValueDealer comBinedValueDealer = new CombinedValueDealer(this._CombinedOperation,this.ValueSpliter, this.CombinedFields);

            //return comBinedValueDealer.GetResultValue(dr);

            return this.CalculatingFormula.GetResultValue(dr);
        }
     
       
        public override GridColumn Copy()
        {
            CombinedGridColumn newGridColumn = new CombinedGridColumn();

            newGridColumn._Field = this.Field;
            newGridColumn._Title = this.Title;
            newGridColumn._Units = this.Units;
            newGridColumn.Style = (BasicStyle)this.Style.Copy();
            newGridColumn._TitleFormat = this.TitleFormat;
            newGridColumn._ColumnWidth = this.ColumnWidth;
            newGridColumn._ColorNeedChange = this.ColorNeedChange;	//Modified at 2009-2-16 11:25:16@Scott
            newGridColumn._Description = this.Description;
            newGridColumn.DateFormat = this.DateFormat;
            newGridColumn._DisplayAsImage = this.DisplayAsImage;
            newGridColumn._ImageRowHeight = this.ImageRowHeight;

            newGridColumn.ValueSpliter = this.ValueSpliter;

            newGridColumn.CombinedFields = new StringCollection();

            foreach (string strField in this.CombinedFields)
            {
                newGridColumn.CombinedFields.Add(strField);
            }

            newGridColumn._CombinedOperation = this._CombinedOperation;

            newGridColumn._CalculateInfoSetting = this.CalculatingFormula.Copy();

            if (this._RelatedCalculateFormulaDealer != null) newGridColumn.RelatedCalculateFormulaDealer = _RelatedCalculateFormulaDealer;

            return newGridColumn;

        }

   
        #region Serialization By Simon's Macro 2011-3-1 11:30:41
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue("_CombinedFields", _CombinedFields, typeof(System.Collections.Specialized.StringCollection));           
            info.AddValue("_ValueSpliter", _ValueSpliter);
            info.AddValue("_CombinedOperation", _CombinedOperation, typeof(Webb.Reports.ExControls.Data.CombinedOperation));
            info.AddValue("_CalculateInfoSetting", _CalculateInfoSetting, typeof(Webb.Reports.ExControls.Data.CalculateColumnInfo));

            info.AddValue("_RelatedCalculateFormulaDealer", _RelatedCalculateFormulaDealer, typeof(Webb.Reports.ExControls.Data.CalculateFormulaDealer));    

        }

        public CombinedGridColumn(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            try
            {
                _CombinedFields = (System.Collections.Specialized.StringCollection)info.GetValue("_CombinedFields", typeof(System.Collections.Specialized.StringCollection));
            }
            catch
            {
                _CombinedFields = new StringCollection();
            }        
            try
            {
                _ValueSpliter = info.GetString("_ValueSpliter");
            }
            catch
            {
                _ValueSpliter = " ";
            }
            try
            {
                _CombinedOperation = (Webb.Reports.ExControls.Data.CombinedOperation)info.GetValue("_CombinedOperation", typeof(Webb.Reports.ExControls.Data.CombinedOperation));
            }
            catch
            {
                _CombinedOperation = CombinedOperation.StrConcat;
            }
            try
            {
                _CalculateInfoSetting = (Webb.Reports.ExControls.Data.CalculateColumnInfo)info.GetValue("_CalculateInfoSetting", typeof(Webb.Reports.ExControls.Data.CalculateColumnInfo));
            }
            catch
            {
                _CalculateInfoSetting = new CalculateColumnInfo(this._CombinedFields, this._ValueSpliter, this.ComputeOperation);
            }
            try
            {
                _RelatedCalculateFormulaDealer = (Webb.Reports.ExControls.Data.CalculateFormulaDealer)info.GetValue("_RelatedCalculateFormulaDealer", typeof(Webb.Reports.ExControls.Data.CalculateFormulaDealer));
            }
            catch
            {
                _RelatedCalculateFormulaDealer = null;
            }
        }
        #endregion
    }
    #endregion

    #region public class GridColumnCollection : CollectionBase
    [Serializable]
    public class GridColumnCollection : CollectionBase
    {
        public GridColumn this[int i_Index]
        {
            get { return this.InnerList[i_Index] as GridColumn; }
            set { this.InnerList[i_Index] = value; }
        }
        public GridColumn this[string strField]
        {
            get
            {
                foreach (GridColumn column in this)
                {
                    if (column.Field == strField)
                    {
                        return column;
                    }
                }
                return null;
            }

        }
        public GridColumnCollection() { }

        public int Add(GridColumn i_Object)
        {
            return this.InnerList.Add(i_Object);
        }

        public void Remove(GridColumn i_Obj)
        {
            this.InnerList.Remove(i_Obj);
        }

        public bool Contain(GridColumn i_Obj)
        {
            return this.InnerList.Contains(i_Obj);
        }
        public bool Contains(string strField)
        {
            foreach (GridColumn column in this)
            {
                if (column.Field == strField) return true;
            }
            return false;
        }

        public void CopyTo(GridColumnCollection columns)
        {
            if (object.ReferenceEquals(this, columns)) return;

            columns.Clear();

            foreach (GridColumn col in this)
            {
                columns.Add(col.Copy());
            }
        }
    }
    #endregion

    #region public class GridInfo
    [Serializable]
    public class GridInfo : ISerializable
    {
        protected int _TopCount;
        public int TopCount
        {
            get { return this._TopCount; }
            set { this._TopCount = value < 0 ? 0 : value; }
        }

        protected ClickEvents _ClickEvent;
        public ClickEvents ClickEvent
        {
            get { return this._ClickEvent; }
            set { this._ClickEvent = value; }
        }

        protected GridColumnCollection _Columns;
        [Browsable(false)]
        public GridColumnCollection Columns
        {
            get
            {
                if (_Columns == null) _Columns = new GridColumnCollection();
                return this._Columns;
            }
            set { this._Columns = value; }
        }

        protected SortingColumnCollection _SortingColumns;	//Modified at 2009-2-10 8:47:12@Scott
        [Browsable(true), Category("Sorting")]
        public SortingColumnCollection SortingColumns
        {
            get
            {
                if (this._SortingColumns == null) this._SortingColumns = new SortingColumnCollection();

                for (int i = 0; i < _SortingColumns.Count; i++)   //Remove Empty SortigColumn at 2009-2-17 13:47:12@Simon
                {
                    SortingColumn column = _SortingColumns[i];

                    if (column.SortingBy == "<None>" || column.SortingBy == string.Empty)
                    {
                        _SortingColumns.RemoveAt(i);

                        i--;
                    }
                }
                if (_SortingColumns.Count == 0)
                {
                    this._SortingFrequence = SortingFrequence.None;
                }
                return this._SortingColumns;
            }
            set
            {
                this._SortingColumns = value;

                if (_SortingColumns.Count == 0)
                {
                    this._SortingFrequence = SortingFrequence.None;
                }
            }
        }

        protected int _ImageRowHeight = 100;

      

        #region Added thse properties at 2008-11-26 10:43:06@Simon
        protected string _SortingColumn = "<None>";
        [Browsable(false), Category("Sorting")]
        [Editor(typeof(Webb.Reports.Editors.FieldSelectEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string SortingBy
        {
            get
            {
                //Modified at 2009-2-10 11:32:04@Scott
                return this._SortingColumn;
            }
            set
            {
                this._SortingColumn = value;
            }
        }


        protected Webb.Reports.ExControls.Data.SortingTypes _SortingTypes;
        [Browsable(false), Category("Sorting")]
        public Webb.Reports.ExControls.Data.SortingTypes SortingTypes
        {
            get
            {
                return this._SortingTypes;
            }
            set { this._SortingTypes = value; }
        }

        protected Webb.Reports.ExControls.Data.SortingByTypes _SortingByTypes;
        [Browsable(false), Category("Sorting")]
        public Webb.Reports.ExControls.Data.SortingByTypes SortingByTypes
        {
            get
            {
                return this._SortingByTypes;
            }
            set
            {
                this._SortingByTypes = value;
            }
        }

        protected SortingFrequence _SortingFrequence;
        [Browsable(true), Category("Sorting")]
        public SortingFrequence SortingFrequence
        {
            get
            {
                return this._SortingFrequence;

            }
            set
            {
                //Modified at 2009-2-10 11:33:02@Scott
                if (this.SortingColumns == null || this.SortingColumns.Count <= 0)
                {
                    this._SortingFrequence = SortingFrequence.None;
                }
                else
                {
                    this._SortingFrequence = value;
                }
            }
        }
        #endregion

        public GridInfo()
        {
            this._ClickEvent = ClickEvents.PlayVideo;
            this._Columns = new GridColumnCollection();
            this._TopCount = 0;
            _SortingColumn = "<None>";    //Added this code at 2008-11-26 10:49:27@Simon
            this.SortingByTypes = Webb.Reports.ExControls.Data.SortingByTypes.Frequence;  //Added this code at 2008-11-26 10:50:14@Simon
            this.SortingTypes = Webb.Reports.ExControls.Data.SortingTypes.Descending;  //Added this code at 2008-11-26 10:50:17@Simon
            this.SortingFrequence = SortingFrequence.None;    //Added this code at 2008-11-28 10:47:55@Simon
            this.SortingColumns = new SortingColumnCollection();	//Modified at 2009-2-10 8:46:26@Scott        
        }

        public void Apply(GridInfo gridInfo)
        {
            this.ClickEvent = gridInfo.ClickEvent;
            gridInfo.Columns.CopyTo(this._Columns);
            this._TopCount = gridInfo.TopCount;

            this._SortingColumn = gridInfo.SortingBy;  //Added this code at 2008-11-26 10:51:43@Simon
            this.SortingByTypes = gridInfo.SortingByTypes;   //Added this code at 2008-11-26 10:51:43@Simon
            this.SortingTypes = gridInfo.SortingTypes;    //Added this code at 2008-11-27 10:51:43@Simon
            this._SortingFrequence = gridInfo.SortingFrequence;  //Added this code at 2008-11-28 8:56:09@Simon
            this.SortingColumns = gridInfo.SortingColumns;	//Modified at 2009-2-10 8:51:29@Scott     
           
        }

        [Browsable(false)]
        public int ImageRowHeight
        {
            get
            {
                foreach (GridColumn column in this.Columns)
                {
                    if (column.DisplayAsImage) return column.ImageRowHeight;
                }

                return 100;
            }

        }

        public void ApplyColumnsWidth(Int32Collection columnsWidth, int start)
        {
            if (columnsWidth == null) return;

            int nLen = this.Columns.Count;

            for (int col = 0; col < nLen; col++)
            {
                GridColumn gridColumn = this.Columns[col];

                while (col + start >= columnsWidth.Count) columnsWidth.Add(BasicStyle.ConstValue.CellWidth);

                gridColumn.ColumnWidth = columnsWidth[col + start];
            }
        }

        public void UpdateColumnsWidth(Int32Collection columnsWidth, int start)
        {
            if (columnsWidth == null) return;

            int nLen = this.Columns.Count;

            for (int col = 0; col < nLen; col++)
            {
                GridColumn gridColumn = this.Columns[col];

                while (col + start >= columnsWidth.Count) columnsWidth.Add(BasicStyle.ConstValue.CellWidth);

                columnsWidth[col + start] = gridColumn.ColumnWidth;
            }
        }

        public void UpdateColumnsWidth(Int32Collection columnsWidth, int start, int WrapColumns)
        {
            if (columnsWidth == null) return;

            int nLen = this.Columns.Count;

            for (int k = 0; k < WrapColumns; k++)
            {
                for (int col = 0; col < nLen; col++)
                {
                    GridColumn gridColumn = this.Columns[col];

                    while (col + start >= columnsWidth.Count) columnsWidth.Add(BasicStyle.ConstValue.CellWidth);

                    columnsWidth[col + start] = gridColumn.ColumnWidth;
                }

                start += nLen;
            }
        }

        #region Old ApplyColumnsWidth/UpdateColumnsWidth
        //		public void ApplyColumnsWidth(Int32Collection columnsWidth)
        //		{
        //			if(columnsWidth == null) return;
        //
        //			int nLen = this.Columns.Count;
        //
        //			for(int col = 0; col < nLen; col++)
        //			{
        //				GridColumn gridColumn = this.Columns[col];
        //
        //				while(col >= columnsWidth.Count) columnsWidth.Add(BasicStyle.ConstValue.CellWidth);
        //
        //				gridColumn.ColumnWidth = columnsWidth[col];
        //			}
        //		}
        //
        //		public void UpdateColumnsWidth(Int32Collection columnsWidth)
        //		{
        //			if(columnsWidth == null) return;
        //
        //			int nLen = this.Columns.Count;
        //
        //			for(int col = 0; col < nLen; col++)
        //			{
        //				GridColumn gridColumn = this.Columns[col];
        //
        //				while(col >= columnsWidth.Count) columnsWidth.Add(BasicStyle.ConstValue.CellWidth);
        //
        //				columnsWidth[col] = gridColumn.ColumnWidth;
        //			}
        //		}
        #endregion

        //Modified at 2009-2-10 11:02:43@Scott
        public GroupResultCollection Sorting(DataTable dt, Int32Collection filteredRows)	//Modified at 2009-2-10 10:02:18@Scott
        {
            if (filteredRows == null || filteredRows.Count == 0) return null;

            if (this.SortingColumns.Count <= 0) return null;

            FieldGroupInfo rootGroupInfo = null, tempGroupInfo = null, subGroupInfo = null;

            SortingColumn sColumn = null;

            int index = 0;

            for (; index < this.SortingColumns.Count; index++)
            {
                sColumn = this.SortingColumns[index];

                if (dt.Columns.Contains(sColumn.SortingBy))
                {
                    rootGroupInfo = new FieldGroupInfo(sColumn.SortingBy);
                    rootGroupInfo.Sorting = sColumn.SortingTypes;
                    rootGroupInfo.SortingBy = sColumn.SortingByTypes;

                    tempGroupInfo = rootGroupInfo;

                    break;
                }
            }

            index++;

            for (; index < this.SortingColumns.Count; index++)
            {
                sColumn = this.SortingColumns[index];

                if (!dt.Columns.Contains(sColumn.SortingBy))
                {
                    continue;
                }

                subGroupInfo = new FieldGroupInfo(sColumn.SortingBy);
                subGroupInfo.Sorting = sColumn.SortingTypes;
                subGroupInfo.SortingBy = sColumn.SortingByTypes;

                tempGroupInfo.SubGroupInfos.Add(subGroupInfo);

                tempGroupInfo = subGroupInfo;
            }

            GroupResultCollection results = new GroupResultCollection();

            if (rootGroupInfo != null)	//Modified at 2009-2-16 9:51:48@Scott
            {
                rootGroupInfo.CalculateGroupResult(dt, filteredRows, filteredRows, rootGroupInfo);

                this.GetLeafGroupResults(rootGroupInfo, results);
            }

            if (results.Count <= 0) return null;

            return results;
        }

        //Modified at 2009-2-10 11:02:50@Scott
        public void GetLeafGroupResults(GroupInfo groupInfo, GroupResultCollection results)
        {
            groupInfo.GroupResults.Sort(groupInfo.Sorting, groupInfo.SortingBy, groupInfo.UserDefinedOrders);

            foreach (GroupResult result in groupInfo.GroupResults)
            {
                GroupResultCollection tempResults = new GroupResultCollection();

                tempResults = groupInfo.GroupResults;

                if (result.SubGroupInfos.Count == 0)
                {
                    results.Add(result);
                }

                foreach (GroupInfo subGroupInfo in result.SubGroupInfos)
                {
                    this.GetLeafGroupResults(subGroupInfo, results);
                }
            }
        }

        public GroupResultCollection SortGridColumns(DataTable dt, Int32Collection filteredRows)   //modify this code at 2008-11-28 11:25:33@Simon
        {
            string strCol = this.SortingBy;

            if (!dt.Columns.Contains(strCol) || strCol == null || strCol == "<None>") return null;

            FieldGroupInfo m_GroupInfo = new FieldGroupInfo();
            m_GroupInfo.GroupByField = strCol;
            m_GroupInfo.ColumnHeading = strCol;

            m_GroupInfo.CalculateGroupResult(dt, filteredRows, filteredRows, m_GroupInfo);

            if (m_GroupInfo.GroupResults == null || m_GroupInfo.GroupResults.Count <= 0) return null;
            m_GroupInfo.GroupResults.Sort(this.SortingTypes, this.SortingByTypes, m_GroupInfo.UserDefinedOrders);

            return m_GroupInfo.GroupResults;
        }

        #region Serialization By Macro 2008-12-11 14:28:10
        public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            info.AddValue("_TopCount", _TopCount, typeof(int));
            info.AddValue("_ClickEvent", _ClickEvent, typeof(Webb.Reports.ExControls.ClickEvents));
            info.AddValue("_Columns", _Columns, typeof(GridColumnCollection));
            info.AddValue("_SortingColumn", _SortingColumn, typeof(string));
            info.AddValue("_SortingTypes", _SortingTypes, typeof(Webb.Reports.ExControls.Data.SortingTypes));
            info.AddValue("_SortingByTypes", _SortingByTypes, typeof(Webb.Reports.ExControls.Data.SortingByTypes));
            info.AddValue("_SortingFrequence", _SortingFrequence, typeof(SortingFrequence));
            info.AddValue("_SortingColumns", _SortingColumns, typeof(SortingColumnCollection));

        }

        public GridInfo(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            try
            {
                _TopCount = (int)info.GetValue("_TopCount", typeof(int));
            }
            catch
            {
                _TopCount = 0;
            }
            try
            {
                _ClickEvent = (Webb.Reports.ExControls.ClickEvents)info.GetValue("_ClickEvent", typeof(Webb.Reports.ExControls.ClickEvents));
            }
            catch
            {
                _ClickEvent = ClickEvents.PlayVideo;
            }
            try
            {
                _Columns = (GridColumnCollection)info.GetValue("_Columns", typeof(GridColumnCollection));
            }
            catch
            {
                _Columns = new GridColumnCollection();
            }
            try
            {
                _SortingColumn = (string)info.GetValue("_SortingColumn", typeof(string));
            }
            catch
            {
                _SortingColumn = "<None>";
            }
            try
            {
                _SortingTypes = (Webb.Reports.ExControls.Data.SortingTypes)info.GetValue("_SortingTypes", typeof(Webb.Reports.ExControls.Data.SortingTypes));
            }
            catch
            {
                _SortingTypes = Webb.Reports.ExControls.Data.SortingTypes.Descending;
            }
            try
            {
                _SortingByTypes = (Webb.Reports.ExControls.Data.SortingByTypes)info.GetValue("_SortingByTypes", typeof(Webb.Reports.ExControls.Data.SortingByTypes));
            }
            catch
            {
                _SortingByTypes = Webb.Reports.ExControls.Data.SortingByTypes.Frequence;
            }
            try	//Modified at 2009-2-10 8:50:43@Scott
            {
                SortingColumns = info.GetValue("_SortingColumns", typeof(SortingColumnCollection)) as SortingColumnCollection;
            }
            catch(Exception ex)
            {
                SortingColumns = new SortingColumnCollection();
                SortingColumn sColumn = new SortingColumn();
                sColumn.SortingBy = this.SortingBy;
                sColumn.SortingTypes = this.SortingTypes;
                sColumn.SortingByTypes = this.SortingByTypes;
                SortingColumns.Add(sColumn);
            }
            try
            {
                _SortingFrequence = (SortingFrequence)info.GetValue("_SortingFrequence", typeof(SortingFrequence));
            }
            catch
            {
                _SortingFrequence = SortingFrequence.None;
            }
        }
        #endregion

        public void ResetGroupInfo(GroupInfo groupInfo)
        {
            if (groupInfo == null||!(groupInfo is FieldGroupInfo)) return;

             FieldGroupInfo fieldGroupInfo=(groupInfo as FieldGroupInfo);

             if(fieldGroupInfo.GroupByField==string.Empty)return;

             GridColumn column = this.Columns[fieldGroupInfo.GroupByField];

             if (!fieldGroupInfo.DisplayAsColumn && fieldGroupInfo.DistinctValues && column!=null)
             {
                 fieldGroupInfo.Visible = true;

                 fieldGroupInfo.DisplayAsColumn = true;

                 this.Columns.Remove(column);
             }

             foreach (GroupInfo subGroupInfo in groupInfo.SubGroupInfos)
             {
                 this.ResetGroupInfo(subGroupInfo);
             }
        }

    }
    #endregion


    public class FieldColumnGroup
    {
        public string _Field = string.Empty;
        public Int32Collection _SetRows = new Int32Collection();
        public object SetValue;
    }
}