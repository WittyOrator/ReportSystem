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
    #region Old
    //[Serializable]
    //public class CombinedValueDealer : ISerializable
    //{
    //    #region Auto Constructor By Macro 2011-6-22 11:23:20

    //    public CombinedValueDealer(CombinedOperation p_CombinedOperation, string p_ValueSplitter, StringCollection p_Fields)
    //    {
    //        if (p_Fields != null) _CombinedFields = p_Fields;
    //        if (p_ValueSplitter != null) _ValueSpliter = p_ValueSplitter;
    //        _CombinedOperation = p_CombinedOperation;
    //    }
    //    #endregion

    //    protected StringCollection _CombinedFields = new StringCollection();

    //    protected string _ValueSpliter = " ";

    //    protected CombinedOperation _CombinedOperation = CombinedOperation.StrConcat;

    //    protected string _ColumnHeader = string.Empty;


    //    public override string ToString()
    //    {
    //        StringBuilder sb = new StringBuilder();

    //        foreach (string strField in this._CombinedFields)
    //        {
    //            string strConcatString = CombinedValueDealer.GetContactString(this._CombinedOperation);

    //            if (sb.Length > 0) sb.AppendFormat(" {0} ", strConcatString);

    //            sb.Append(strField);
    //        }

    //        return "[" + sb.ToString() + "]";
    //    }


    //    public static string GetContactString(CombinedOperation combinedOperation)
    //    {
    //        string[] strOperation = new string[] { "&", "+", "-", "*", @"/", "|" };

    //        int index = (int)combinedOperation;

    //        return strOperation[index];
    //    }

    //    #region GetSpecialName
    //    public string GetSpecialName()
    //    {
    //        StringBuilder sb = new StringBuilder();

    //        sb.Append("C_");

    //        sb.Append(this.ValueSpliter);

    //        foreach (string strGroupByField in this.CombinedFields)
    //        {
    //            sb.Append(strGroupByField);
    //        }

    //        sb.Append((byte)this._CombinedOperation);

    //        return sb.ToString();
    //    }
    //    #endregion


    //    #region Sub Functions for AddFieldsColumn  StrConcat /NumAdd / NumSubtract /NumMulti /NumDivide

    //    private string ConcatString(DataRow dataRow)
    //    {
    //        StringBuilder sbExpression = new StringBuilder();

    //        if (this._ValueSpliter.Contains("{0}")) sbExpression = new StringBuilder(this._ValueSpliter);

    //        for (int i = 0; i < this._CombinedFields.Count; i++)
    //        {
    //            string strGroupByField = this._CombinedFields[i];

    //            if (!dataRow.Table.Columns.Contains(strGroupByField)) continue;

    //            object objValue = dataRow[strGroupByField];

    //            if (this._ValueSpliter.Contains("{0}"))
    //            {
    //                #region Deal Specal Format

    //                if (objValue == null || objValue is DBNull)
    //                {
    //                    objValue = string.Empty;
    //                }

    //                sbExpression = sbExpression.Replace("{" + i.ToString() + "}", objValue.ToString());

    //                #endregion
    //            }
    //            else
    //            {
    //                #region Split Simple

    //                if (objValue == null || objValue is DBNull || objValue.ToString().Trim() == string.Empty) continue;

    //                string strValue = objValue.ToString();

    //                if (sbExpression.Length > 0)
    //                {
    //                    sbExpression.AppendFormat("{0}{1}", this._ValueSpliter, strValue);
    //                }
    //                else
    //                {
    //                    sbExpression.AppendFormat("{0}", strValue);
    //                }
    //                #endregion
    //            }
    //        }

    //        return sbExpression.ToString();
    //    }

    //    private string AddNumbersIntoAColumn(DataRow dataRow)
    //    {
    //        decimal totalNumber = 0m;

    //        StringBuilder sbResult = new StringBuilder(this._ValueSpliter);

    //        for (int i = 0; i < this._CombinedFields.Count; i++)
    //        {
    //            string strGroupByField = this._CombinedFields[i];

    //            if (!dataRow.Table.Columns.Contains(strGroupByField)) continue;

    //            object objValue = dataRow[strGroupByField];

    //            if (objValue == null || objValue is DBNull) objValue = string.Empty;

    //            sbResult.Replace("{" + i.ToString() + "}", objValue.ToString());

    //            if (objValue.ToString().Trim() == string.Empty) continue;

    //            decimal number = 0m;

    //            bool convertOK = decimal.TryParse(objValue.ToString(), out number);

    //            if (!convertOK) continue;

    //            totalNumber += number;
    //        }


    //        if (this._ValueSpliter.Contains("[result]"))
    //        {
    //            sbResult.Replace("[result]", totalNumber.ToString());

    //            return sbResult.ToString();
    //        }

    //        return totalNumber.ToString();
    //    }

    //    private string SubtractNumbersIntoAColumn(DataRow dataRow)
    //    {
    //        decimal totalNumber = 0m;

    //        StringBuilder sbResult = new StringBuilder(this._ValueSpliter);

    //        for (int i = 0; i < _CombinedFields.Count; i++)
    //        {
    //            string strGroupByField = this._CombinedFields[i];

    //            if (!dataRow.Table.Columns.Contains(strGroupByField)) continue;

    //            object objValue = dataRow[strGroupByField];

    //            if (objValue == null || objValue is DBNull) objValue = string.Empty;

    //            sbResult.Replace("{" + i.ToString() + "}", objValue.ToString());

    //            if (objValue.ToString().Trim() == string.Empty) continue;

    //            decimal number = 0m;

    //            bool convertOK = decimal.TryParse(objValue.ToString(), out number);

    //            if (!convertOK) continue;

    //            if (i == 0)
    //            {
    //                totalNumber = number;
    //            }
    //            else
    //            {
    //                totalNumber -= number;
    //            }

    //        }

    //        if (this._ValueSpliter.Contains("[result]"))
    //        {
    //            sbResult.Replace("[result]", totalNumber.ToString());

    //            return sbResult.ToString();
    //        }

    //        return totalNumber.ToString();
    //    }

    //    private string MultiPlayNumbersIntoAColumn(DataRow dataRow)
    //    {
    //        decimal totalNumber = 1m;

    //        StringBuilder sbResult = new StringBuilder(this._ValueSpliter);

    //        for (int i = 0; i < _CombinedFields.Count; i++)
    //        {
    //            string strGroupByField = this._CombinedFields[i];

    //            if (!dataRow.Table.Columns.Contains(strGroupByField)) continue;

    //            object objValue = dataRow[strGroupByField];

    //            if (objValue == null || objValue is DBNull) objValue = string.Empty;

    //            sbResult.Replace("{" + i.ToString() + "}", objValue.ToString());

    //            if (objValue.ToString().Trim() == string.Empty) continue;

    //            decimal number = 0m;

    //            bool convertOK = decimal.TryParse(objValue.ToString(), out number);

    //            if (!convertOK) continue;

    //            totalNumber *= number;
    //        }

    //        if (this._ValueSpliter.Contains("[result]"))
    //        {
    //            sbResult.Replace("[result]", totalNumber.ToString());

    //            return sbResult.ToString();
    //        }


    //        return totalNumber.ToString();
    //    }

    //    private string DivideNumbersIntoAColumn(DataRow dataRow)
    //    {
    //        decimal totalNumber = 1m;

    //        StringBuilder sbResult = new StringBuilder(this._ValueSpliter);

    //        for (int i = 0; i < _CombinedFields.Count; i++)
    //        {
    //            string strGroupByField = this._CombinedFields[i];

    //            if (!dataRow.Table.Columns.Contains(strGroupByField)) continue;

    //            object objValue = dataRow[strGroupByField];

    //            if (objValue == null || objValue is DBNull) objValue = string.Empty;

    //            sbResult.Replace("{" + i.ToString() + "}", objValue.ToString());

    //            if (objValue.ToString().Trim() == string.Empty) continue;

    //            decimal number = 0m;

    //            bool convertOK = decimal.TryParse(objValue.ToString(), out number);

    //            if (!convertOK || number == 0)
    //            {
    //                totalNumber = 0;

    //                continue;
    //            }

    //            if (i == 0)
    //            {
    //                totalNumber = number;
    //            }
    //            else
    //            {
    //                totalNumber /= number;
    //            }
    //        }

    //        if (totalNumber * 100 != (int)(totalNumber * 100))
    //        {
    //            totalNumber = decimal.Round(totalNumber, 2);
    //        }

    //        if (this._ValueSpliter.Contains("[result]"))
    //        {
    //            sbResult.Replace("[result]", totalNumber.ToString());

    //            return sbResult.ToString();
    //        }


    //        return totalNumber.ToString();
    //    }

    //    private string AverageNumbersIntoAColumn(DataRow dataRow)
    //    {
    //        decimal totalNumber = 0m;

    //        StringBuilder sbResult = new StringBuilder(this._ValueSpliter);

    //        for (int i = 0; i < _CombinedFields.Count; i++)
    //        {
    //            string strGroupByField = this._CombinedFields[i];

    //            if (!dataRow.Table.Columns.Contains(strGroupByField)) continue;

    //            object objValue = dataRow[strGroupByField];

    //            if (objValue == null || objValue is DBNull) objValue = string.Empty;

    //            sbResult.Replace("{" + i.ToString() + "}", objValue.ToString());

    //            if (objValue.ToString().Trim() == string.Empty) continue;

    //            decimal number = 0m;

    //            bool convertOK = decimal.TryParse(objValue.ToString(), out number);

    //            if (!convertOK) continue;

    //            totalNumber += number;
    //        }

    //        if (_CombinedFields.Count > 0) totalNumber /= _CombinedFields.Count;

    //        if (totalNumber * 100 != (int)(totalNumber * 100))
    //        {
    //            totalNumber = decimal.Round(totalNumber, 2);
    //        }

    //        if (this._ValueSpliter.Contains("[result]"))
    //        {
    //            sbResult.Replace("[result]", totalNumber.ToString());

    //            return sbResult.ToString();
    //        }

    //        return totalNumber.ToString();
    //    }

    //    #endregion

    //    #region GetResult
    //    public string GetResultValue(DataRow dr)
    //    {
    //        string strValue = string.Empty;

    //        switch (this._CombinedOperation)
    //        {
    //            case CombinedOperation.NumAdd:
    //                strValue = this.AddNumbersIntoAColumn(dr);
    //                break;

    //            case CombinedOperation.NumSubtract:
    //                strValue = this.SubtractNumbersIntoAColumn(dr);
    //                break;

    //            case CombinedOperation.NumMultipy:
    //                strValue = this.MultiPlayNumbersIntoAColumn(dr);
    //                break;
    //            case CombinedOperation.NumDivide:
    //                strValue = this.DivideNumbersIntoAColumn(dr);
    //                break;
    //            case CombinedOperation.NumAverage:
    //                strValue = this.AverageNumbersIntoAColumn(dr);
    //                break;
    //            case CombinedOperation.StrConcat:
    //            default:
    //                strValue = this.ConcatString(dr);
    //                break;
    //        }

    //        return strValue;
    //    }
    //    #endregion


    //    #region Convert Into GridColumn/FieldGroupInfo
    //    public Webb.Reports.ExControls.Views.CombinedGridColumn IntoCombinedGridColumn()
    //    {
    //        Webb.Reports.ExControls.Views.CombinedGridColumn combinedGridColumn = new Webb.Reports.ExControls.Views.CombinedGridColumn();

    //        combinedGridColumn.Field = this.GetSpecialName();

    //        combinedGridColumn.ComputeOperation = this.CombinedOperation;

    //        foreach (string strField in this.CombinedFields)
    //        {
    //            combinedGridColumn.CombinedFields.Add(strField);
    //        }

    //        combinedGridColumn.ValueSpliter = this.ValueSpliter;

    //        combinedGridColumn.Title = this.ColumnHeader;

    //        return combinedGridColumn;
    //    }

    //    public CombinedFieldsGroupInfo IntoCombinedFieldsGroupInfo()
    //    {
    //        CombinedFieldsGroupInfo combinedFieldsGroupInfo = new CombinedFieldsGroupInfo();

    //        combinedFieldsGroupInfo.ComputeOperation = this.CombinedOperation;

    //        foreach (string strField in this.CombinedFields)
    //        {
    //            combinedFieldsGroupInfo.GroupByFields.Add(strField);
    //        }

    //        combinedFieldsGroupInfo.ValueSpliter = this.ValueSpliter;

    //        combinedFieldsGroupInfo.ColumnHeading = this.ColumnHeader;

    //        return combinedFieldsGroupInfo;
    //    }
    //    #endregion

    //    #region Properties

    //    public System.Collections.Specialized.StringCollection CombinedFields
    //    {
    //        get
    //        {
    //            if (_CombinedFields == null) _CombinedFields = new StringCollection();
    //            return _CombinedFields;
    //        }
    //        set
    //        {
    //            _CombinedFields = value;
    //        }
    //    }

    //    public string ValueSpliter
    //    {
    //        get
    //        {
    //            return _ValueSpliter;
    //        }
    //        set
    //        {
    //            _ValueSpliter = value;
    //        }
    //    }

    //    public Webb.Reports.ExControls.Data.CombinedOperation CombinedOperation
    //    {
    //        get
    //        {
    //            return _CombinedOperation;
    //        }
    //        set
    //        {
    //            _CombinedOperation = value;
    //        }
    //    }

    //    public string ColumnHeader
    //    {
    //        get
    //        {
    //            return _ColumnHeader;
    //        }
    //        set
    //        {
    //            _ColumnHeader = value;
    //        }
    //    }
    //    #endregion

    //    #region Serialization By Simon's Macro 2011-7-6 13:54:45
    //    public void GetObjectData(SerializationInfo info, StreamingContext context)
    //    {
    //        info.AddValue("_CombinedFields", _CombinedFields, typeof(System.Collections.Specialized.StringCollection));
    //        info.AddValue("_ValueSpliter", _ValueSpliter);
    //        info.AddValue("_CombinedOperation", _CombinedOperation, typeof(Webb.Reports.ExControls.Data.CombinedOperation));
    //        info.AddValue("_ColumnHeader", _ColumnHeader);

    //    }

    //    public CombinedValueDealer(SerializationInfo info, StreamingContext context)
    //    {
    //        try
    //        {
    //            _CombinedFields = (System.Collections.Specialized.StringCollection)info.GetValue("_CombinedFields", typeof(System.Collections.Specialized.StringCollection));
    //        }
    //        catch
    //        {
    //            _CombinedFields = new StringCollection();
    //        }
    //        try
    //        {
    //            _ValueSpliter = info.GetString("_ValueSpliter");
    //        }
    //        catch
    //        {
    //            _ValueSpliter = " ";
    //        }
    //        try
    //        {
    //            _CombinedOperation = (Webb.Reports.ExControls.Data.CombinedOperation)info.GetValue("_CombinedOperation", typeof(Webb.Reports.ExControls.Data.CombinedOperation));
    //        }
    //        catch
    //        {
    //            _CombinedOperation = CombinedOperation.StrConcat;
    //        }
    //        try
    //        {
    //            _ColumnHeader = info.GetString("_ColumnHeader");
    //        }
    //        catch
    //        {
    //            _ColumnHeader = string.Empty;
    //        }
    //    }
    //    #endregion
    //}

    #endregion
}