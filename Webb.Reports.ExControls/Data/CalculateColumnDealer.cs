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
    public enum CombinedOperation : byte
    {
        StrConcat,
        NumAdd,
        NumSubtract,
        NumMultipy,
        NumDivide,
        //NumAverage
    }
    public class CalculateStackResult
    {
        #region Auto Constructor By Macro 2011-7-12 11:27:09

        public CalculateStackResult(CombinedOperation p_CacluteOperation, object p_Field, Bracket p_Bracket)
        {
			_Bracket=p_Bracket;
            _Result = p_Field;
			_CacluteOperation=p_CacluteOperation;
        }
		#endregion
    
        protected Bracket _Bracket = Bracket.NONE;

        protected object _Result = string.Empty;

        protected CombinedOperation _CacluteOperation = CombinedOperation.StrConcat;

        public Webb.Data.Bracket Bracket
        {
            get
            {
                return _Bracket;
            }
            set
            {
                _Bracket = value;
            }
        }

        public object Result
        {
            get
            {
                if(_Result==null)_Result=string.Empty;

                return _Result;
            }
            set
            {
                _Result = value;
            }
        }

        public Webb.Reports.ExControls.Data.CombinedOperation CacluteOperation
        {
            get
            {
                return _CacluteOperation;
            }
            set
            {
                _CacluteOperation = value;
            }
        }

        public CalculateStackResult CalculateResult(CalculateStackResult tempResult,string strValueSpliter)
        {            
            string strValue = string.Empty;

            switch (this.CacluteOperation)
            {
                case CombinedOperation.NumAdd:
                    strValue = this.AddNumbersIntoAColumn(tempResult);
                    break;

                case CombinedOperation.NumSubtract:
                    strValue = this.SubtractNumbersIntoAColumn(tempResult);
                    break;

                case CombinedOperation.NumMultipy:
                    strValue = this.MultiPlayNumbersIntoAColumn(tempResult);
                    break;
                case CombinedOperation.NumDivide:
                    strValue = this.DivideNumbersIntoAColumn(tempResult);
                    break;
              
                case CombinedOperation.StrConcat:
                default:
                    strValue = this.ConcatString(tempResult, strValueSpliter);
                    break;
            }

            CalculateStackResult calcuateResult = new CalculateStackResult(tempResult.CacluteOperation, strValue, tempResult.Bracket);

            return calcuateResult;
       
        }

        #region Sub Functions for AddFieldsColumn  StrConcat /NumAdd / NumSubtract /NumMulti /NumDivide

        private string ConcatString(CalculateStackResult tempResult,string strValueSpliter)
        {
            StringBuilder sbExpression = new StringBuilder();  

            string strValue = Result.ToString();

            sbExpression.AppendFormat("{0}{1}{0}{2}", strValueSpliter, this.Result, tempResult.Result);            

            return sbExpression.ToString();
        }

        private string AddNumbersIntoAColumn(CalculateStackResult tempResult)
        {
            decimal number1 = 0m,number2=0m;

            decimal.TryParse(Result.ToString(), out number1);

            decimal.TryParse(tempResult.Result.ToString(), out number2);

            number1 += number2;

            return number1.ToString();
        }

        private string SubtractNumbersIntoAColumn(CalculateStackResult tempResult)
        {
            decimal number1 = 0m, number2 = 0m;

            decimal.TryParse(Result.ToString(), out number1);

            decimal.TryParse(tempResult.Result.ToString(), out number2);

            number1 -= number2;

            return number1.ToString();
           
        }

        private string MultiPlayNumbersIntoAColumn(CalculateStackResult tempResult)
        {
            decimal number1 = 0m, number2 = 0m;

            decimal.TryParse(Result.ToString(), out number1);

            decimal.TryParse(tempResult.Result.ToString(), out number2);

            number1 *= number2;

            return number1.ToString();
        }

        private string DivideNumbersIntoAColumn(CalculateStackResult tempResult)
        {
            decimal number1 = 0m, number2 = 0m;

            decimal.TryParse(Result.ToString(), out number1);

            decimal.TryParse(tempResult.Result.ToString(), out number2);

            if (number2 == 0)
            {
                number1 = 0;
            }
            else
            {
                number1 /= number2;

                if (number1 * 100 != (int)(number1 * 100))
                {
                    number1 = decimal.Round(number1, 2);
                } 
            }           

            return number1.ToString();   
        }      

        #endregion
    }

    [Serializable]
    public class CalcElement : ISerializable
    {
        #region Auto Constructor By Macro 2011-7-13 10:21:37
		public CalcElement()
        {
			_Bracket=Bracket.NONE;
			_Field=string.Empty;
			_CacluteConstantOperation=CombinedOperation.StrConcat;
			_ConstantValue=string.Empty;
			_CacluteFieldOperation=CombinedOperation.StrConcat;
        }

        public CalcElement(CombinedOperation p_CacluteFieldOperation, string p_ConstantValue, CombinedOperation p_CacluteConstantOperation, object p_Field, Bracket p_Bracket)
        {
			_Bracket=p_Bracket;
			_Field=p_Field;
			_CacluteConstantOperation=p_CacluteConstantOperation;
			_ConstantValue=p_ConstantValue;
			_CacluteFieldOperation=p_CacluteFieldOperation;
        }
		#endregion

       
        protected Bracket _Bracket = Bracket.NONE;

        protected object _Field = string.Empty;

        protected CombinedOperation _CacluteConstantOperation = CombinedOperation.StrConcat;

        protected string _ConstantValue = string.Empty;

        protected CombinedOperation _CacluteFieldOperation = CombinedOperation.StrConcat;

        #region Copy Function By Macro 2011-7-12 11:27:13
		public CalcElement Copy()
        {
			CalcElement thiscopy=new CalcElement();
			thiscopy._Bracket=this._Bracket;

            object objValue = this._Field;

            if (this._Field is CalculateColumnInfo)
            {
                objValue = (this._Field as CalculateColumnInfo).Copy();
            }

            thiscopy._Field = objValue;


            thiscopy._CacluteConstantOperation = this._CacluteConstantOperation;

            thiscopy._ConstantValue = this._ConstantValue;

            thiscopy._CacluteFieldOperation = this._CacluteFieldOperation;
			return thiscopy;
        }
		#endregion

        #region functions GetGropByField & GetContactString & GetDefaultHeader
        public string GetGropByField()
        {
            string strField = string.Empty;

            if (Field is CalculateColumnInfo)
            {
                strField = (Field as CalculateColumnInfo).GetGropByField();
            }
            else
            {
                strField = Field.ToString();
            }
            strField += ((byte)this.CacluteConstantOperation).ToString();
            strField += this.ConstantValue;
            strField += ((byte)this.CacluteFieldOperation).ToString();
            return strField;
        }

        public static string GetContactString(CombinedOperation combinedOperation)
        {
            string[] strOperation = new string[] { "&", "+", "-", "*", @"/", "|" };

            int index = (int)combinedOperation;

            return strOperation[index];
        }

        public string GetDefaultHeader()
        {
            StringBuilder sb = new StringBuilder();

            if (this.Bracket == Bracket.Start)
            {
                sb.Append("(");
            }

            string strField = string.Empty ;

            if (Field is CalculateColumnInfo)
            {
                sb.Append("(");

                strField = (Field as CalculateColumnInfo).GetDefaultHeader();

                sb.Append(strField);

                sb.Append(")");
            }
            else
            {
                strField = Field.ToString();

                sb.Append(strField);
            }
         
            if (this.ConstantValue != string.Empty)
            {
                if (ConstantValue != string.Empty) sb.Append(GetContactString(this.CacluteConstantOperation));
                
                sb.Append(this.ConstantValue);
                
            }

            if (this.Bracket == Bracket.Start)
            {
                sb.Append(")");
            }

            sb.Append(GetContactString(this.CacluteFieldOperation));

            return sb.ToString();
        }

        public string GetCalcSteps(DataRow dr)
        {          
            StringBuilder sb = new StringBuilder();

            if (this.Bracket == Bracket.Start)
            {
                sb.Append("(");
            }

            string strFieldValue = string.Empty;

            if (Field is CalculateColumnInfo)
            {
                sb.Append("(");
                
                strFieldValue = (Field as CalculateColumnInfo).GetCalcSteps(dr);

                sb.Append(strFieldValue);

                sb.Append(")");
            }
            else
            {
                string strField=Field.ToString();

                if(dr.Table.Columns.Contains(strField))
                {
                    strFieldValue = (dr[strField] == null || dr[strField] is DBNull) ? string.Empty : dr[strField].ToString();
                }
                else
                {
                    strFieldValue = string.Empty;
                }

                if (strFieldValue.Trim().StartsWith("-"))
                {
                    sb.Append("("+strFieldValue+")");
                }
                else
                {
                    sb.Append(strFieldValue);
                }
            }        

            if (this.ConstantValue != string.Empty)
            {
                if(strFieldValue!=string.Empty)sb.Append(GetContactString(this.CacluteConstantOperation));

                if (ConstantValue.Trim().StartsWith("-"))
                {
                    sb.Append("(" + ConstantValue + ")");
                }
                else
                {
                    sb.Append(this.ConstantValue);
                }
            }

            if (this.Bracket == Bracket.Start)
            {
                sb.Append(")");
            }

            sb.Append(GetContactString(this.CacluteFieldOperation));

            return sb.ToString();
        }

        #endregion


        public string GetResult(DataRow row)
        {
            string strValue = string.Empty;

            if (this.Field is CalculateColumnInfo)
            {
                object objValue = (Field as CalculateColumnInfo).GetResultWithBracket(row);

                strValue = objValue.ToString();
            }
            else
            {
                string strField = Field.ToString();

                if (!row.Table.Columns.Contains(strField))
                {
                    strValue = string.Empty;
                }
                else
                {
                    strValue = row[strField].ToString();
                }
            }

            if (this.ConstantValue == string.Empty) return strValue;

            CalculateStackResult calcStackResult = new CalculateStackResult(this.CacluteConstantOperation, strValue, Bracket.NONE);

            CalculateStackResult calcResult2 = new CalculateStackResult(this.CacluteFieldOperation, this.ConstantValue, Bracket.NONE);

            calcStackResult=calcStackResult.CalculateResult(calcResult2,string.Empty);

            return calcStackResult.Result.ToString();

        }

        #region Properties

        public Webb.Data.Bracket Bracket
        {
            get
            {
                return _Bracket;
            }
            set
            {
                _Bracket = value;
            }
        }

        public object Field
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

        public Webb.Reports.ExControls.Data.CombinedOperation CacluteConstantOperation
        {
            get
            {
                return _CacluteConstantOperation;
            }
            set
            {
                _CacluteConstantOperation = value;
            }
        }

        public string ConstantValue
        {
            get
            {
                if (_ConstantValue == null) _ConstantValue = string.Empty;
                return _ConstantValue;
            }
            set
            {
                _ConstantValue = value;
            }
        }

        public Webb.Reports.ExControls.Data.CombinedOperation CacluteFieldOperation
        {
            get
            {
                return _CacluteFieldOperation;
            }
            set
            {
                _CacluteFieldOperation = value;
            }
        }
        #endregion

        #region Serialization By Simon's Macro 2011-7-13 9:47:09
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
			info.AddValue("_Bracket",_Bracket,typeof(Webb.Data.Bracket));
			info.AddValue("_Field",_Field);
			info.AddValue("_CacluteConstantOperation",_CacluteConstantOperation,typeof(Webb.Reports.ExControls.Data.CombinedOperation));
			info.AddValue("_ConstantValue",_ConstantValue);
			info.AddValue("_CacluteFieldOperation",_CacluteFieldOperation,typeof(Webb.Reports.ExControls.Data.CombinedOperation));

        }

        public CalcElement(SerializationInfo info, StreamingContext context)
        {
			try
			{
				_Bracket=(Webb.Data.Bracket)info.GetValue("_Bracket",typeof(Webb.Data.Bracket));
			}
			catch
			{
				_Bracket=Bracket.NONE;
			}
			try
			{
				_Field=info.GetValue("_Field",typeof(object));
			}
			catch
			{
				_Field=string.Empty;
			}
			try
			{
				_CacluteConstantOperation=(Webb.Reports.ExControls.Data.CombinedOperation)info.GetValue("_CacluteConstantOperation",typeof(Webb.Reports.ExControls.Data.CombinedOperation));
			}
			catch
			{
				_CacluteConstantOperation=CombinedOperation.StrConcat;
			}
			try
			{
				_ConstantValue=info.GetString("_ConstantValue");
			}
			catch
			{
				_ConstantValue=string.Empty;
			}
			try
			{
				_CacluteFieldOperation=(Webb.Reports.ExControls.Data.CombinedOperation)info.GetValue("_CacluteFieldOperation",typeof(Webb.Reports.ExControls.Data.CombinedOperation));
			}
			catch
			{
				_CacluteFieldOperation=CombinedOperation.StrConcat;
			}
        }
		#endregion

    }

    [Serializable]
    public class CalculateColumnInfo : WebbCollection,ISerializable
    {
        public const string ExportFileExt = ".ccif";
   
        protected string _ValueSpliter = " ";

        protected bool _DisplayedCalculteStep = false;

        public CalculateColumnInfo() : base() { }

        public CalculateColumnInfo(StringCollection fields, string strValueSpliter, CombinedOperation combinedOperation)
        {
            this.innerList = new ArrayList();

            foreach(string strField in fields)
            {
                CalcElement element=new CalcElement(combinedOperation,string.Empty,CombinedOperation.StrConcat,strField,Bracket.NONE);

                this.innerList.Add(element); 
            }
            _ValueSpliter = strValueSpliter;
        }

        public CalcElement this[int index]
        {
            get
            {
                return this.innerList[index] as CalcElement;
            }
            set
            {
                this.innerList[index] = value;
            }
        }
        public override int Add(object value)
        {
            if (!(value is CalcElement)) return -1;
 
            return base.Add(value);
        }

        public CalculateColumnInfo Copy()
        {
            CalculateColumnInfo calculateColumnInfo = new CalculateColumnInfo();

            foreach (CalcElement elet in this)
            {
                calculateColumnInfo.Add(elet.Copy());
            }
            calculateColumnInfo.ValueSpliter = this.ValueSpliter;

            calculateColumnInfo.DisplayedCalculteStep = this.DisplayedCalculteStep;

            return calculateColumnInfo;
        }      
     
        public override string ToString()
        {
            string strDefaultHeader = this.GetDefaultHeader();

            return "["+strDefaultHeader+"]";
        }

        #region GetGropByField & GetDefaultHeader  
        public string GetGropByField()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("C_");

            foreach (CalcElement calcElement in this)
            {
                sb.Append(calcElement.GetGropByField());
            }

            int num = _DisplayedCalculteStep ? 1 : 0;

            sb.Append(num);

            return sb.ToString();
        }

        public string GetDefaultHeader()
        {
            StringBuilder sb = new StringBuilder();

            foreach (CalcElement calcElement in this)
            {
                sb.Append(calcElement.GetDefaultHeader());
            }

            if (sb.Length > 0)sb=sb.Remove(sb.Length-1, 1);

            return sb.ToString();
        }
        #endregion

        public string GetResultValue(DataRow dr)
        {
            object objValue = GetResultWithBracket(dr);

            if (this._DisplayedCalculteStep)
            {
                objValue = string.Format("{0}={1}", this.GetCalcSteps(dr), objValue);
            }

            return objValue.ToString();
        }

        public string GetCalcSteps(DataRow dr)
        {
            StringBuilder sb = new StringBuilder();

            foreach (CalcElement calcElement in this)
            {
                sb.Append(calcElement.GetCalcSteps(dr));
            }

            if (sb.Length > 0) sb = sb.Remove(sb.Length - 1, 1);

            return sb.ToString();
        }

        #region GetResultWithoutBracket     

        public CalculateStackResult GetResultWithoutBracket(Stack stack)	//Modified at 2009-2-16 15:35:41@Scott
        {
            ArrayList arr = new ArrayList();

            string strValue = string.Empty;

            while (stack.Count > 0)
            {
                CalculateStackResult br = stack.Pop() as CalculateStackResult;

                if (br.CacluteOperation>=CombinedOperation.NumMultipy)
                {
                    while (stack.Count > 0 && br.CacluteOperation >= CombinedOperation.NumMultipy)
                    {
                        CalculateStackResult tempResult= stack.Pop() as CalculateStackResult;

                        br = br.CalculateResult(tempResult, this.ValueSpliter);
                    }
                }           

                arr.Add(br);
            }

            CalculateStackResult bRet = new CalculateStackResult(CombinedOperation.StrConcat,string.Empty,Bracket.NONE);

            if (arr.Count > 0)
            {
                bRet = arr[0] as CalculateStackResult;

                for(int i=1;i<arr.Count;i++)
                {
                    CalculateStackResult tempResult = arr[i] as CalculateStackResult;

                    bRet=bRet.CalculateResult(tempResult, this.ValueSpliter);
                }                      
            }

            return bRet;
        }

        #endregion

        #region GetResult WithBracket
        public object  GetResultWithBracket(DataRow row)
        {
            try
            {
                //12-17-2007@Scott
                Stack stack = new Stack();

                Stack tempstack;

                foreach (CalcElement calcElement in this)
                {
                    string strValue = calcElement.GetResult(row);                   

                    CalculateStackResult br = new CalculateStackResult(calcElement.CacluteFieldOperation, strValue, calcElement.Bracket);

                    if (calcElement.Bracket == Bracket.End)
                    {
                        tempstack = new Stack();

                        tempstack.Push(br);

                        br = stack.Pop() as CalculateStackResult;

                        while (br.Bracket != Bracket.Start)
                        {
                            tempstack.Push(br);

                            if (stack.Count == 0)
                            {
                                System.Windows.Forms.MessageBox.Show("Bad Bracket Count!");

                                return false;
                            }

                            br = stack.Pop() as CalculateStackResult;
                        }
                        tempstack.Push(br);

                        br = GetResultWithoutBracket(tempstack);

                        br.Bracket = Bracket.NONE;                       
                    }

                    stack.Push(br);
                }

                tempstack = new Stack();

                while (stack.Count > 0) tempstack.Push(stack.Pop());

                CalculateStackResult result = GetResultWithoutBracket(tempstack);

                return result.Result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Check filter result error. Message:" + ex.Message);

                return string.Empty;
            }
        }
        #endregion

        public string ValueSpliter
        {
            get
            {
                return _ValueSpliter;
            }
            set
            {
                _ValueSpliter = value;
            }
        }

        public bool DisplayedCalculteStep
        {
            get
            {
                return _DisplayedCalculteStep;
            }
            set
            {
                _DisplayedCalculteStep = value;
            }
        }

        #region Serialization By Simon's Macro 2011-7-13 10:52:30
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

			info.AddValue("_ValueSpliter",_ValueSpliter);
			info.AddValue("_DisplayedCalculteStep",_DisplayedCalculteStep);

        }

        public CalculateColumnInfo(SerializationInfo info, StreamingContext context):base(info,context)
        {
			try
			{
				_ValueSpliter=info.GetString("_ValueSpliter");
			}
			catch
			{
				_ValueSpliter=" ";
			}
			try
			{
				_DisplayedCalculteStep=info.GetBoolean("_DisplayedCalculteStep");
			}
			catch
			{
				_DisplayedCalculteStep=false;
			}
        }
		#endregion


    }

    #region Old
    [Serializable]
    public class CalculateFormulaDealer : ISerializable
    {
        public const string ExportFileExt = ".clf";    

        public CalculateFormulaDealer(){}

        protected ArrayList _Elements = new ArrayList();    

        protected CombinedOperation _CombinedOperation = CombinedOperation.StrConcat;

        protected string _ColumnHeader = string.Empty;    
   
        #region Convert Into GridColumn/FieldGroupInfo

        public CalculateColumnInfo ConvertIntoCalcInfo()
        {
            CalculateColumnInfo calculateColumnInfo = new CalculateColumnInfo();

            foreach (object objValue in this.Elements)
            { 
                if(objValue==null)continue;

                object objCalEletField = objValue;               

                if (objValue is CalculateFormulaDealer)
                {
                    objCalEletField = (objValue as CalculateFormulaDealer).ConvertIntoCalcInfo();
                }

                CalcElement calElet = new CalcElement(this.CombinedOperation, string.Empty, CombinedOperation.StrConcat, objCalEletField, Bracket.NONE);

                calculateColumnInfo.Add(calElet);
            }

            return calculateColumnInfo;
        }


        public Webb.Reports.ExControls.Views.CombinedGridColumn IntoCombinedGridColumn()
        {
            Webb.Reports.ExControls.Views.CombinedGridColumn combinedGridColumn = new Webb.Reports.ExControls.Views.CombinedGridColumn();

            combinedGridColumn.CalculatingFormula=this.ConvertIntoCalcInfo();            

            combinedGridColumn.Title = this.ColumnHeader;

            combinedGridColumn.RelatedCalculateFormulaDealer = this;

            combinedGridColumn.Field = combinedGridColumn.CalculatingFormula.GetDefaultHeader();

            return combinedGridColumn;
        }

        public CombinedFieldsGroupInfo IntoCombinedFieldsGroupInfo()
        {
            CombinedFieldsGroupInfo combinedFieldsGroupInfo = new CombinedFieldsGroupInfo();

            combinedFieldsGroupInfo.CalculatingFormula = this.ConvertIntoCalcInfo();        
        
            combinedFieldsGroupInfo.ColumnHeading = this.ColumnHeader;

            combinedFieldsGroupInfo.RelatedCalculateFormulaDealer = this;

            return combinedFieldsGroupInfo;
        }
        #endregion

        #region Properties

        public ArrayList Elements
        {
            get
            {
                if (_Elements == null) _Elements = new ArrayList();
                return _Elements;
            }
            set
            {
                _Elements = value;
            }
        }    

        public Webb.Reports.ExControls.Data.CombinedOperation CombinedOperation
        {
            get
            {
                return _CombinedOperation;
            }
            set
            {
                _CombinedOperation = value;
            }
        }

        public string ColumnHeader
        {
            get
            {
                return _ColumnHeader;
            }
            set
            {
                _ColumnHeader = value;
            }
        }
        #endregion    

        public override string ToString()
        {
            CalculateColumnInfo calcColumnInfo = this.ConvertIntoCalcInfo();

            string displayedName = calcColumnInfo.GetDefaultHeader();

            displayedName = "[" + displayedName + "]";

            return displayedName;
        }

        #region Serialization By Simon's Macro 2011-7-13 14:20:29
		public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
			info.AddValue("_Elements",_Elements,typeof(System.Collections.ArrayList));
			info.AddValue("_CombinedOperation",_CombinedOperation,typeof(Webb.Reports.ExControls.Data.CombinedOperation));
			info.AddValue("_ColumnHeader",_ColumnHeader);

        }

        public CalculateFormulaDealer(SerializationInfo info, StreamingContext context)
        {
			try
			{
				_Elements=(System.Collections.ArrayList)info.GetValue("_Elements",typeof(System.Collections.ArrayList));
			}
			catch
			{
				_Elements=new ArrayList();
			}
			try
			{
				_CombinedOperation=(Webb.Reports.ExControls.Data.CombinedOperation)info.GetValue("_CombinedOperation",typeof(Webb.Reports.ExControls.Data.CombinedOperation));
			}
			catch
			{
				_CombinedOperation=CombinedOperation.StrConcat;
			}
			try
			{
				_ColumnHeader=info.GetString("_ColumnHeader");
			}
			catch
			{
				_ColumnHeader=string.Empty;
			}
        }
		#endregion
    }

    #endregion

   
}
