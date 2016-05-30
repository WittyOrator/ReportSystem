/***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:DBFilter.cs
 * Author:Country.Wu [EMail:Webb.Country.Wu@163.com]
 * Create Time:10/30/2007 04:20:28 PM
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
using System.Text.RegularExpressions;

using System.ComponentModel;
using System.ComponentModel.Design;

using Webb.Collections;

namespace Webb.Data
{
	#region public enum FilterTypes,public enum FilterOpands
	public enum FilterTypes
	{
		NumEqual = 0,
		NumNotEqual = 1,
		NumGreater = 2,
		NumLess = 3,
		NumGreaterOrEqual = 4,
		NumLessOrEqual = 5,
		//
		StrEqual = 6,
		StrNotEqual = 7,
		StrStartWith = 8,
		StrEndWith = 9,
		StrInclude = 10,
		StrNotStarWith = 11,
		IsStrEmpty = 17,
		IsNotStrEmpty = 18,
		//
		StrNotEndWith = 12,
		StrNotInclude = 13,
		InArray = 14,
		IsDBNull = 15,
		IsNotDBNull = 16,
		//
	}

	public enum FilterCatalogs
	{
		Number = 0,
		String = 1,
		InArray = 2,
		DBNull = 3,
	}

	public enum FilterOperands
	{
		And = 0,
		Or = 1,
		//Not = 2,	//12-17-2007@Scott
	}

	//12-17-2007@Scott
	public enum Bracket
	{
		NONE = 0,
		Start = 1,
		End = 2,
	}

	public enum SortingTypes
	{
		None = 0,
		Ascending = 1,		
		Descending = 2
	}

	//Modified at 2008-11-6 9:43:25@Scott
	public enum DiagramScoutType
	{
		Offense = 0,
		Defense,
		Kick,
	}

	#endregion

	#region public interface IIndexCompare

	public interface IIndexCompare
	{
		int Index{get;set;}
	}

	public class IndexCompare : System.Collections.IComparer
	{
		public SortingTypes  SortingType = SortingTypes.None;

		public IndexCompare(SortingTypes i_SortingType)
		{
			this.SortingType = i_SortingType;
		}

		#region IComparer Members
		public int Compare(object x, object y)
		{
			if(this.SortingType==SortingTypes.None) return 0;
			// TODO:  Add IndexCompare.Compare implementation
			if(!(x is IIndexCompare)||!(y is IIndexCompare))
			{
				throw new Exception("Sorting object doesn't implement the interface of IIndexCompare.");
			}
			int m_value = (x as IIndexCompare).Index - (y as IIndexCompare).Index;
			if(this.SortingType==SortingTypes.Descending) m_value = -m_value;
			return m_value;
		}
		#endregion
	}
	#endregion

	#region public class DBCondition
	/// <summary>
	/// Baseic filter condition.
	/// </summary>
	[Serializable]	
	public class DBCondition
	{
		public static Regex RegexNotChar = new Regex(@"[^A-Za-z]",RegexOptions.IgnoreCase); 

		public static FilterCatalogs GetFilterCatalog(FilterTypes i_Types)
		{
			switch(i_Types)
			{				
				case FilterTypes.NumEqual:
				case FilterTypes.NumGreater:
				case FilterTypes.NumGreaterOrEqual:
				case FilterTypes.NumLess:
				case FilterTypes.NumLessOrEqual:
				case FilterTypes.NumNotEqual:
					return FilterCatalogs.Number;
				case FilterTypes.InArray:
					return  FilterCatalogs.InArray;
				case FilterTypes.IsDBNull:
				case FilterTypes.IsNotDBNull:
					return FilterCatalogs.DBNull;
				default:
					return FilterCatalogs.String;
			}
		}

		#region fields and properties
		protected string _ColumnName;//= null;
		protected object _Value;// = DBNull.Value;
		protected FilterTypes _FilterType;
		protected Bracket _Bracket = Bracket.NONE;	//12-17-2007@Scott
		protected FilterOperands _FollowedOperand;
		protected FilterCatalogs _FilterCatalog;
		protected bool _IgnoreCase;	//02-26-2008@Scott

		public bool IgnoreCase
		{
			get{return this._IgnoreCase;}
			set{this._IgnoreCase = value;}
		}

		[TypeConverter(typeof(PublicDBFieldConverter))]
		public string ColumnName
		{
			get{return this._ColumnName;}
			set{this._ColumnName = value;}
		}

		[TypeConverter(typeof(FilterObjectValueConverter))]
		public object Value
		{
			//protected object PropertyName;
			get{return this._Value;}
			set{this._Value = value;}
		}

		public FilterTypes FilterType
		{
			//protected object PropertyName;
			get{return this._FilterType;}
			set
			{
				if(this._FilterType==value) return;
				this._FilterType = value;
				this._FilterCatalog = GetFilterCatalog(value);
			}
		}

		//12-17-2007@Scott
		public Bracket Bracket
		{	
			get{return this._Bracket;}
			set{this._Bracket = value;}
		}

		public FilterOperands FollowedOperand
		{
			//protected object PropertyName;
			get{return this._FollowedOperand;}
			set{this._FollowedOperand = value;}
		}

		[Browsable(false)]
		public FilterCatalogs FilterCatalog
		{
			get{return this._FilterCatalog;}
		}
		#endregion
		//
		public DBCondition(string i_ColName,FilterTypes i_FilterType,object i_Value,Bracket i_Bracket,FilterOperands i_FollowedOperand)
		{
			this._ColumnName = i_ColName;
			this._FilterType = i_FilterType;
			this._Value = i_Value;
			this._Bracket = i_Bracket;
			this._FollowedOperand = i_FollowedOperand;
			this._FilterCatalog = GetFilterCatalog(i_FilterType);
			this._IgnoreCase = true;
		}

        public bool EqualCondition(DBCondition condition)
        {
            return    (this._ColumnName == condition.ColumnName && this._FilterType == condition.FilterType
                                       &&  this._Value == condition.Value && this._Bracket == condition.Bracket &&
                                       this._FollowedOperand == condition.FollowedOperand &&   this._FilterCatalog == condition.FilterCatalog &&
                                      this._IgnoreCase == condition.IgnoreCase);

        }

		//
		public bool CheckResult(DataRow i_Row)
		{
			if(this._ColumnName == null ) return false;
			if(!i_Row.Table.Columns.Contains(this._ColumnName))return true;
//			if(Convert.IsDBNull(i_Row[this._ColumnName])||Convert.IsDBNull(this._Value))
//			{
//				return this.CheckResult_DBNull(i_Row[this._ColumnName]);
//			}
//			else
			switch(this._FilterCatalog)
			{
				case FilterCatalogs.InArray:
					return this.CheckResult_InArray(i_Row[this._ColumnName]);
				case FilterCatalogs.Number:
					return this.CheckResult_Number(i_Row[this._ColumnName]);
				case FilterCatalogs.String:
					return this.CheckResult_String(i_Row[this._ColumnName]);
				case FilterCatalogs.DBNull:
					return this.CheckResult_DBNull(i_Row[this._ColumnName]);
				default:
					return false;
			}
		}

		private bool CheckResult_DBNull(object i_data)
		{
			if(this._FilterType==FilterTypes.IsDBNull)
                return Convert.IsDBNull(i_data);
			else
				return !Convert.IsDBNull(i_data);
		}

		private bool CheckResult_Number(object i_data)
		{
			//12-20-2007@Scott
			if(i_data is System.DBNull || i_data == null || i_data.ToString() == string.Empty)
			{
                // 12-08-2011 Scott
                if (Value == i_data)
                {
                    return true;
                }
                // End

				return false;
			}
            if (i_data.GetType() == typeof(DateTime) && _Value.GetType()==typeof(DateTime))
            {
                #region Compare DateTime

                DateTime d_data = Convert.ToDateTime(i_data).Date;

                DateTime d_value = Convert.ToDateTime(this._Value).Date;

                switch (this._FilterType)
                {
                    default:
                    case FilterTypes.NumEqual:
                        return d_data == d_value;
                    case FilterTypes.NumGreater:
                        return d_data > d_value;
                    case FilterTypes.NumGreaterOrEqual:
                        return d_data >= d_value;
                    case FilterTypes.NumLess:
                        return d_data < d_value;
                    case FilterTypes.NumLessOrEqual:
                        return d_data <= d_value;
                    case FilterTypes.NumNotEqual:
                        return d_data != d_value;
                }

                #endregion

            }
			else if(!RegexNotChar.IsMatch(i_data.ToString()))	//08-26-2008@Scott
			{
				return false;
			}

			int m_data = Convert.ToInt32(i_data);
			int m_value = Convert.ToInt32(this._Value);
			switch(this._FilterType)
			{
				default:
				case FilterTypes.NumEqual:
					return m_data == m_value;
				case FilterTypes.NumGreater:
					return m_data > m_value;
				case FilterTypes.NumGreaterOrEqual:
					return m_data >= m_value;
				case FilterTypes.NumLess:
					return m_data < m_value;
				case FilterTypes.NumLessOrEqual:
					return m_data <= m_value;
				case FilterTypes.NumNotEqual:
					return m_data != m_value;
			}
		}

		private bool CheckResult_String(object i_data)
		{
            if (i_data == null) return false;

			string m_data = i_data.ToString();

			if(this._IgnoreCase) m_data = m_data.ToLower();

			if(this._Value == null) this._Value = string.Empty;
			
			string m_value = this._Value.ToString();
			
			if(this._IgnoreCase) m_value = m_value.ToLower();

			switch(this._FilterType)
			{
				default:
				case FilterTypes.StrEndWith:
					return m_data.EndsWith(m_value);
				case FilterTypes.StrEqual:
                    switch (this._Value.ToString())	//Modified at 2009-2-13 13:55:05@Scott
					{
						case "AnyEntry":
                            return m_data.Trim() != string.Empty;
						case "NoEntry":
                            return m_data.Trim() == string.Empty;
						default:
							return m_data == m_value;
					}
				case FilterTypes.StrInclude:
					return m_data.IndexOf(m_value)>=0;
				case FilterTypes.StrNotEndWith:
					return !m_data.EndsWith(m_value);
				case FilterTypes.StrNotEqual:
					return m_data != m_value;
				case FilterTypes.StrNotInclude:
					return m_data.IndexOf(m_value)<0;
				case FilterTypes.StrNotStarWith:
					return !m_data.StartsWith(m_value);
				case FilterTypes.StrStartWith:
					return m_data.StartsWith(m_value);
				case FilterTypes.IsStrEmpty:
					return m_data.Trim() == string.Empty;
				case FilterTypes.IsNotStrEmpty:
					return m_data.Trim() != string.Empty;
			}
		}
		private bool CheckResult_InArray(object i_data)
		{
			if(!(this._Value is IList))
			{
				return false;
			}
			else
			{
				return (this._Value as IList).Contains(i_data);
			}
		}

		public DBCondition()
		{
			this._IgnoreCase = true;
		}

		public DBCondition Copy()
		{
			DBCondition m_Condition = new DBCondition();
			m_Condition._ColumnName = this._ColumnName;
			m_Condition._FilterCatalog = this._FilterCatalog;
			m_Condition._FilterType = this._FilterType;
			m_Condition._Bracket = this._Bracket;
			m_Condition._FollowedOperand = this._FollowedOperand;
			m_Condition._Value = this._Value;
			m_Condition._IgnoreCase = this._IgnoreCase;
			return m_Condition;
		}

		public override string ToString()
		{
			return string.Format("{0} {1} {2} {3} {4}",this._ColumnName,this._FilterType,this._Value,this._Bracket,this._FollowedOperand);
		}    
		//
	}
	#endregion

    public static class FilterTypeDealCls
    {
        public static string[] CreateNumericList()
        {
            string[] arrList = new string[] { "=", ">", "<", ">=", "<=","!=" };

            return arrList;
        }
      
       
        public static FilterTypes GetDateTimeFilterType(string strFilterType)
        {
            FilterTypes filterType = FilterTypes.NumLess;

            switch (strFilterType)
            {
                case  "=":
                    filterType = FilterTypes.NumEqual;
                    break;
                case ">":
                    filterType = FilterTypes.NumGreater;
                    break;
                case ">=":
                    filterType = FilterTypes.NumGreaterOrEqual;
                    break;
                case "<":
                    filterType = FilterTypes.NumLess;
                    break;
                case "<=":
                default:
                    filterType = FilterTypes.NumLessOrEqual;
                    break;
            }

            return filterType;
        }
      
    }

	#region public class DBFilter
	[Serializable]
	public class DBFilter : CollectionBase,ISerializable
	{
		//12-11-2008@Scott
		//Begin
		virtual public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("CollectionBase+list",this.InnerList);

			info.AddValue("_PlayAfter",this._PlayAfter);

			info.AddValue("_Name",this._Name);

			info.AddValue("_IsCustomFilter",this._IsCustomFilter);
		}

		public DBFilter(SerializationInfo info, StreamingContext context)
		{
			try
			{
				ArrayList arr = info.GetValue("CollectionBase+list",typeof(ArrayList)) as ArrayList;
			
				if(arr != null)
				{
					this.Clear();

					foreach(object o in arr)
					{
						this.Add(o as DBCondition);	
					}
				}
			}
			catch
			{
				this.Clear();
			}

			try
			{
				this._PlayAfter = info.GetBoolean("_PlayAfter");
			}
			catch
			{
				this._PlayAfter = false;
			}
			try
			{
				this._IsCustomFilter = info.GetBoolean("_IsCustomFilter");
			}
			catch
			{
				this._IsCustomFilter = true;
			}


			try
			{
				this._Name = info.GetString("_Name");
			}
			catch
			{
				this._Name = string.Empty;
			}
		}
		//End

		public DBCondition this[int i_Index]
		{ 
			get { return this.InnerList[i_Index] as DBCondition; } 
			set { this.InnerList[i_Index] = value; } 
		} 
		//ctor
		//Methods

		#region Add/remove filter
		public int Add(DBCondition i_Object)
		{ 
			return this.InnerList.Add(i_Object);
		}

		private  DBFilter ModifyFilter(DBFilter lastFilter)   //2009-11-13 10:08:21@Simon Add this Code
		{			
			if(lastFilter.Count<=0)return lastFilter;
	
			DBFilter newFilter=new DBFilter();

			if(lastFilter[0].Bracket== Bracket.Start)
			{
				  DBCondition condition=new DBCondition();
                
				  condition.ColumnName=string.Empty;
				  
				  condition.Bracket=Bracket.NONE;

                 condition.FollowedOperand=FilterOperands.Or;

			      newFilter.Add(condition);	
			}

			foreach(DBCondition condition in lastFilter)
			{
				newFilter.Add(condition.Copy());
			}

            newFilter._PlayAfter = lastFilter._PlayAfter;

			newFilter._Name=lastFilter._Name;

			if(newFilter[lastFilter.Count-1].Bracket== Bracket.End)
			{
				newFilter[newFilter.Count-1].FollowedOperand=FilterOperands.Or;

				DBCondition condition=new DBCondition();
                
				condition.ColumnName=string.Empty;
				  
				condition.Bracket=Bracket.NONE;

				condition.FollowedOperand=FilterOperands.And;

				newFilter.Add(condition);	
			}
			else
			{
				newFilter[newFilter.Count-1].FollowedOperand=FilterOperands.And;
			}

			if(newFilter.Count>1)
			{
				newFilter[0].Bracket = Bracket.Start;

				newFilter[newFilter.Count - 1].Bracket = Bracket.End;
			}

			 return newFilter;

		}
 
		//Modified at 2009-1-9 14:52:34@Scott
		public void Add(DBFilter appenededFilter)
		{
            if (appenededFilter == null || appenededFilter.Count == 0) return;

			DBFilter thisModifiedFilter=this.ModifyFilter(this);

            DBFilter modifiedAppenedFilter = this.ModifyFilter(appenededFilter);
	
			DBFilter resultFilter=new DBFilter();

            foreach (DBCondition condition in thisModifiedFilter)
			{
                resultFilter.Add(condition.Copy());
			}
            foreach (DBCondition condition in modifiedAppenedFilter)
			{
                resultFilter.Add(condition.Copy());
			}
			
			this.InnerList.Clear();

            foreach (DBCondition condition in resultFilter)
			{
				this.InnerList.Add(condition.Copy());
			}
		}      

		public void Remove(DBCondition i_Obj)
		{
			this.InnerList.Remove(i_Obj);
		}

  
        //Modified at 2009-1-9 14:52:34@Scott
        public static DBFilter AttachedTwoFilterWithOr(DBFilter firstFilter,DBFilter secondFilter)
        {
            if (firstFilter == null)firstFilter=new DBFilter();

            if (secondFilter == null) secondFilter = new DBFilter();         

            DBFilter resultFilter = new DBFilter();

            foreach (DBCondition condition in firstFilter)
            {
                resultFilter.Add(condition.Copy());
            }

            if (resultFilter.Count > 0) resultFilter[resultFilter.Count - 1].FollowedOperand = FilterOperands.Or;

            foreach (DBCondition condition in secondFilter)
            {
                resultFilter.Add(condition.Copy());
            }

            return resultFilter;
        }      


		#endregion

		//ctor
		public DBFilter()
		{
			this._PlayAfter = false;
			this._Name=string.Empty;  //2009-4-29 10:51:41@Simon Add this Code
            this.IsCustomFilter = true;
		}

		public DBFilter Copy()
		{
			DBFilter newFilter = new DBFilter();

			foreach(DBCondition condition in this)
			{
				newFilter.Add(condition.Copy());
			}
			newFilter.PlayAfter = this.PlayAfter;

			newFilter._Name=this._Name;

            newFilter.IsCustomFilter=this.IsCustomFilter;

			return newFilter;
		}


        public static bool Equals(DBFilter filter1,DBFilter filter2)
        {
            if (filter1 == null || filter2 == null)
            {
                if (filter1 == null && filter2 == null) return true;

                return false;
            }

            if (filter1.InnerList.Count!= filter2.InnerList.Count) return false;

            for (int i = 0; i < filter1.InnerList.Count; i++)
            {
                if(!filter1[i].EqualCondition(filter2[i]))return false;
            }
            return true;
        }


        public void GetAllUsedFields(ref ArrayList  _UsedFields)
        {
            if (this.Count <= 0) return;

            foreach(DBCondition condition in this)
            {
                if (condition.ColumnName == null && condition.ColumnName == string.Empty||_UsedFields.Contains(condition.ColumnName)) return;

                _UsedFields.Add(condition.ColumnName);
            }
        }


		#region CheckResult

		//Methods
		public bool CheckResult(DataRow row, int start, int end)		
		{
			int i = 0, j = 0, done = 0;
			bool curoperand = false, lastoperand = true;

			FilterOperands lastoperator = FilterOperands.And;

			i = start;

			if( this.Count > 0 )
			{
				while(i <= end)
				{
					if((this[i].FollowedOperand == FilterOperands.And) && (lastoperator == FilterOperands.Or))
					{
						j = i;

						done = 0;

						while( done == 0)
						{
							if( (j >= end) || (this[j].FollowedOperand != FilterOperands.And) )
							{
								done = 1;
							}
							else
							{
								j = j + 1;
							}
						}

						curoperand = this.CheckResult( row, i, j);

						i = j;
					}
					else
					{
						curoperand = this[i].CheckResult(row);
					}

					if(lastoperator == FilterOperands.And)
					{
						lastoperand = lastoperand && curoperand;
					}
					else
					{
						lastoperand = lastoperand || curoperand;
					}

					lastoperator = this[i].FollowedOperand;

					i = i + 1;
				}
			}
			return lastoperand;
		}

		public bool CheckResultWithBracket(DataRow row)
		{
			try
			{
				//12-17-2007@Scott
				BoolStack stack = new BoolStack();
				
				foreach(DBCondition condition in this)
				{
					bool bResult = condition.CheckResult(row);

					BoolResult br = new BoolResult(bResult,condition.Bracket,condition.FollowedOperand);

					if(condition.Bracket==Bracket.End)
					{
						BoolStack Bstack=new BoolStack();

						FilterOperands oper=br.Operands;

						Bstack.Push(br);
						
						br=stack.Pop() as BoolResult;                          

						while(br.Bracket!=Bracket.Start)
						{
							Bstack.Push(br);

							if(stack.Count==0)
							{
								System.Windows.Forms.MessageBox.Show("Bad Bracket Count!");

								return false;
							}

							br=stack.Pop() as BoolResult; 
						}
						Bstack.Push(br);

						br=Bstack.GetResultWithoutBracket();
						
						br.Bracket=Bracket.NONE;

						br.Operands=oper;
					}
					
					stack.Push(br);
				}

				stack = stack.Reverse();

				BoolResult result = stack.GetResultWithoutBracket();
				
				return result.Result;
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine("Check filter result error. Message:"+ex.Message);
				
				return false;
			}
		}

//		public bool CheckOldResult(DataRow row)	//old check result method
//		{
//			try
//			{
//				//12-17-2007@Scott
//				BoolStack stack = new BoolStack();
//
//				this.InnerList.Reverse();
//				
//				foreach(DBCondition condition in this)
//				{
//					bool bResult = condition.CheckResult(row);
//
//					BoolResult br = new BoolResult(bResult,condition.Bracket,condition.FollowedOperand);
//					
//					stack.Push(br);
//				}
//				BoolResult result = stack.GetResult();
//				
//				this.InnerList.Reverse();
//				
//				return result.Result;
//			}
//			catch(Exception ex)
//			{
//				System.Diagnostics.Debug.WriteLine("Check filter result error. Message:"+ex.Message);
//				
//				return false;
//			}
//		}

		//06-04-2008@Scott
		public Int32Collection GetFilteredRows(System.Data.DataTable table, Webb.Collections.Int32Collection baseRows)
		{
            if (this.InnerList.Count == 0 && !this.PlayAfter) return baseRows;

			Int32Collection filteredRows = new Int32Collection();
			
			foreach(int row in baseRows)
			{
				if(table.Rows.Count <= row) continue;

				//if(this.CheckResult(table.Rows[row],0,this.Count - 1))	//Modified at 2009-2-13 13:40:23@Scott
				if(this.CheckResultWithBracket(table.Rows[row]))	//Modified at 2009-2-16 16:38:31@Scott
				{
					if(this.PlayAfter && table.Rows.Count > row + 1)
					{
						filteredRows.Add(row + 1);
					}
					else
					{
						filteredRows.Add(row);
					}
				}
			}
			
			return filteredRows;
		}

		//06-04-2008@Scott
		public Int32Collection GetFilteredRows(System.Data.DataTable table)
		{
			Int32Collection filteredRows = new Int32Collection();
			
			for(int row = 0; row < table.Rows.Count; row++)
			{
				//if(this.CheckResult(table.Rows[row],0,this.Count - 1))	//Modified at 2009-2-13 13:40:17@Scott
				if(this.CheckResultWithBracket(table.Rows[row]))	//Modified at 2009-2-16 16:38:31@Scott
				{
					if(this.PlayAfter && table.Rows.Count > row + 1)
					{
						filteredRows.Add(row + 1);
					}
					else
					{
						filteredRows.Add(row);
					}
				}
			}
			
			return filteredRows;
		}
		#endregion


        protected bool _IsCustomFilter=false;
		protected bool _PlayAfter;
		protected string _Name;  //2009-4-29 10:50:26@Simon Add this Code

		[Browsable(true)]
		public bool PlayAfter
		{
			get{return this._PlayAfter;}
			set{this._PlayAfter = value;}
		}

		[Browsable(true)]
		public bool IsCustomFilter
		{
			get{return this._IsCustomFilter;}
			set{this._IsCustomFilter = value;}
		}

		public string Name
		{
			get{
				if(this._Name==null)this._Name=string.Empty;
				return this._Name;}
			set{this._Name = value;}
		}
		
	}
	#endregion

	//12-17-2007@Scott
	public class BoolResult
	{
		private bool _Result;
		
		private Bracket _Bracket;

		private FilterOperands _Operands;

		public bool Result
		{
			get{return this._Result;}
			set{this._Result = value;}
		}

		public Bracket Bracket
		{
			get{return this._Bracket;}
			set{this._Bracket = value;}
		}

		public FilterOperands Operands
		{
			get{return this._Operands;}
			set{this._Operands = value;}
		}

		//ctor
		public BoolResult()
		{
			this._Result = true;
			this._Bracket = Bracket.NONE;
			this._Operands = FilterOperands.And;
		}

		public BoolResult(bool result, Bracket bracket, FilterOperands opr)
		{
			this._Result = result;
			this._Bracket = bracket;
			this._Operands = opr;
		}

		public void CopyTo(BoolResult result)
		{
			result._Bracket = this._Bracket;
			result._Result = this._Result;
			result._Operands = this._Operands;
		}
	}

	//12-17-2007@Scott
	public class BoolStack : Stack
	{
		public override void Push(object obj)
		{
			if(obj == null) return;

			if(obj is BoolResult)
			{
				base.Push (obj);
			}
			else
			{
				System.Diagnostics.Trace.WriteLine(string.Format("Stack push error:{0} is not a BoolResult.",obj.ToString()));
			}
		}

		public BoolStack Reverse()
		{
			BoolStack stack=new BoolStack();

			while(this.Count>0)stack.Push(this.Pop());

			return stack;
		}

		public BoolResult GetResultWithoutBracket(ArrayList arr, int start, int end)	//Modified at 2009-2-16 16:20:18@Scott
		{
			int i = 0, j = 0, done = 0;
			bool curoperand = false, lastoperand = true;

			FilterOperands lastoperator = FilterOperands.And;

			i = start;

			if( arr.Count > 0 )
			{
				while(i <= end)
				{
					if(((arr[i] as BoolResult).Operands == FilterOperands.And) && (lastoperator == FilterOperands.Or))
					{
						j = i;

						done = 0;

						while( done == 0)
						{
							if( (j >= end) || ((arr[j] as BoolResult).Operands != FilterOperands.And) )
							{
								done = 1;
							}
							else
							{
								j = j + 1;
							}
						}

						curoperand = this.GetResultWithoutBracket(arr, i, j).Result;

						i = j;
					}
					else
					{
						curoperand = (arr[i] as BoolResult).Result;
					}

					if(lastoperator == FilterOperands.And)
					{
						lastoperand = lastoperand && curoperand;
					}
					else
					{
						lastoperand = lastoperand || curoperand;
					}

					lastoperator = (arr[i] as BoolResult).Operands;

					i = i + 1;
				}
			}
			
			BoolResult bRet = new BoolResult(lastoperand,Bracket.NONE,lastoperator);

			return bRet;
		}

		public BoolResult GetResultWithoutBracket()	//Modified at 2009-2-16 15:35:41@Scott
		{
			ArrayList arr = new ArrayList();

			while(this.Count > 0)
			{
				arr.Add(this.Pop());
			}

			BoolResult bRet = new BoolResult(true,Bracket.NONE,FilterOperands.And);

			if(arr.Count > 0)
			{
				bRet = this.GetResultWithoutBracket(arr,0,arr.Count - 1);

				bRet.Operands = (arr[arr.Count - 1] as BoolResult).Operands;
			}

			return bRet;
		}

		public BoolResult GetResult()
		{
			BoolResult result = new BoolResult();
			BoolResult br = new BoolResult();

			while(this.Count > 0)
			{
				(this.Pop() as BoolResult).CopyTo(br);	//get top one

				if(br.Bracket == Bracket.Start)	//bracket start
				{		
					br.Bracket = Bracket.NONE;

					this.Push(br); //push first bracketed elem

					br = this.GetResult();
				}

				//calculate
				if(result.Operands == FilterOperands.And)
				{
					result.Result = result.Result&&br.Result;
				}
				else if(result.Operands == FilterOperands.Or)
				{
					result.Result = result.Result||br.Result;
				}
				else
				{
					result.Result = result.Result&&br.Result;
				}
				result.Operands = br.Operands;
			
				//exit bracket
				if(br.Bracket == Bracket.End)
				{
					br.Bracket = Bracket.NONE;

					return result; 
				}
			}
			return result;
		}
	}

	#region public class DBFilterCollection
	/*Descrition:   */
	[Serializable]
	public class DBFilterCollection : CollectionBase
	{
		//Wu.Country@2007-11-21 12:50:47 PM added this collection.
		//Fields
		//Properties
		public DBFilter this[int i_Index]
		{ 
			get { return this.InnerList[i_Index] as DBFilter; } 
			set { this.InnerList[i_Index] = value; }
		}
		//ctor
		public DBFilterCollection() {} 
		//Methods
		public int Add(DBFilter i_Object)
		{ 
			return this.InnerList.Add(i_Object);
		} 
		public void Remove(DBFilter i_Obj)
		{
			this.InnerList.Remove(i_Obj);
		} 
	} 
	#endregion

	#region public class BasicFieldConverter : TypeConverter
	public class PublicDBFieldConverter : TypeConverter
	{
		//static fields
		static private ArrayList _AvialableFields;
		static private StandardValuesCollection _StandValues;
		//Properties
		static public ArrayList AvialableFields
		{
			get
			{
				if(_AvialableFields==null)
				{
					_AvialableFields = new ArrayList();
					_AvialableFields.Add(string.Empty);
				}
				return _AvialableFields;
			}
		}
		static private StandardValuesCollection StandValues
		{
			get
			{
				if(_StandValues==null)
					_StandValues = new StandardValuesCollection(AvialableFields);
				return _StandValues;
			}
		}

		static public void SetAvailableFields(ArrayList i_list)
		{
			_AvialableFields = i_list;
			
			//04-28-2008@Scott
			if(!_AvialableFields.Contains(string.Empty))
			{
				_AvialableFields.Add(string.Empty);
			}
			
			_AvialableFields.Sort(new FiedCompare());
			_StandValues = new StandardValuesCollection(_AvialableFields);			
		}

		//override methods
		public override bool GetStandardValuesSupported(System.ComponentModel.ITypeDescriptorContext context)
		{
			return true;
		}

		public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			return PublicDBFieldConverter.StandValues;
		}
		public override bool CanConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Type sourceType)
		{
			if( sourceType == typeof(string) )
				return true;
			else 
				return base.CanConvertFrom(context, sourceType);
		}

		public override object ConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
		{
			if( value.GetType() == typeof(string))
			{
				if(AvialableFields.Contains(value))
				{
					return value.ToString();
				}
				else
				{
					return value.ToString();	//Scott@12082008 Add
//					throw new System.Exception("Invalid filed name. The field name is not exist in the data source.");
					//return null;
				}
			}
			else
			{
				return base.ConvertFrom(context, culture, value);
			}
		}

		internal class FiedCompare : IComparer
		{
			#region IComparer Members

			public int Compare(object x, object y)
			{
				// TODO:  Add FiedCompare.Compare implementation
				return string.Compare(x.ToString(),y.ToString());
			}

			#endregion
		}
	}
	#endregion


    #region public class PublicTableConverter : TypeConverter
    public class PublicTableConverter : TypeConverter
    {
        //static fields
        static private ArrayList _AvialableTables;
        static private StandardValuesCollection _StandValues;
        //Properties
        static public ArrayList AvialableTables
        {
            get
            {
                if (_AvialableTables == null)
                {
                    _AvialableTables = new ArrayList();
                    _AvialableTables.Add(string.Empty);
                }
                return _AvialableTables;
            }
        }
        static private StandardValuesCollection StandValues
        {
            get
            {
                if (_StandValues == null)
                    _StandValues = new StandardValuesCollection(AvialableTables);
                return _StandValues;
            }
        }

        static public void SetAvailableTables(System.Data.DataSet dataSet)
        {
            _AvialableTables = new ArrayList();

            //04-28-2008@Scott
            if (!_AvialableTables.Contains(string.Empty))
            {
                _AvialableTables.Add(string.Empty);
            }
            if (dataSet != null & dataSet.Tables.Count > 0)
            {
                for (int i = 0; i < dataSet.Tables.Count; i++)
                {
                    _AvialableTables.Add(dataSet.Tables[i].TableName);
                }
            }

             _StandValues = new StandardValuesCollection(_AvialableTables);
        }

        //override methods
        public override bool GetStandardValuesSupported(System.ComponentModel.ITypeDescriptorContext context)
        {
            return true;
        }

        public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return PublicTableConverter.StandValues;
        }
        public override bool CanConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;
            else
                return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value.GetType() == typeof(string))
            {
                if (_AvialableTables.Contains(value))
                {
                    return value.ToString();
                }
                else
                {
                    return value.ToString();	//Scott@12082008 Add
                    //					throw new System.Exception("Invalid filed name. The field name is not exist in the data source.");
                    //return null;
                }
            }
            else
            {
                return base.ConvertFrom(context, culture, value);
            }
        }

        internal class TableCompare : IComparer
        {
            #region IComparer Members

            public int Compare(object x, object y)
            {
                // TODO:  Add FiedCompare.Compare implementation
                return string.Compare(x.ToString(), y.ToString());
            }

            #endregion
        }
    }
    #endregion


	#region public class FilterObjectValueConverter : TypeConverter
	public class FilterObjectValueConverter : TypeConverter
	{
		public override bool CanConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Type sourceType)
		{
			if( sourceType == typeof(string) )
				return true;
			else 
				return base.CanConvertFrom(context, sourceType);
		}

		public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
		{
			if( value.GetType() == typeof(string) )
			{				
//				if(OPTypesConverter.CurrentFilter.DataTypes==DBDataTypes.Numeric)
//				{
//					Convert.ToDouble(value);
//					return value;
//				}
				return value;
			}
			else
			{
				return base.ConvertFrom(context, culture, value);
			}
		}
	}
	#endregion   

	//Modified at 2008-9-27 15:46:23@Simon
	public class GroupTable
	{
		public static DataTable GetGroupTable(DataTable dt,string field)
		{
			if(!dt.Columns.Contains(field))return null;
			int colcount=dt.Columns.Count;
			            
			//create new table
			DataTable newtable=new DataTable(field+"Group");
			Type coltype=dt.Columns[field].DataType;
			newtable.Columns.Add(field,coltype);
			newtable.Columns.Add("Frequence",typeof(int));

			DataView dv=new DataView(dt);
			int count=0;
			dv.Sort=field +" asc";
			object last=null;
			for(int i=0;i<dv.Count;i++)
			{
				object fieldvalue=dv[i][field];
				if(fieldvalue.Equals(last))
				{					
					count++;
				}
				else
				{
					if(count>0)
					{
						DataRow dr=newtable.NewRow();
						dr[field]=last;
						dr["Frequence"]=count;
						newtable.Rows.Add(dr);
					}					
					last=fieldvalue;
					count=1;
				}
				if(i==dv.Count-1)
				{
					DataRow dr=newtable.NewRow();
					dr[field]=last;
					dr["Frequence"]=count;
					newtable.Rows.Add(dr);
					
				}

			}
			return newtable;			
		}
	}
}
