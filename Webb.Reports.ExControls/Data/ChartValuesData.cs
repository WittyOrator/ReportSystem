using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Collections;
using System.Reflection;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using  System.Drawing.Text;
using System.Collections.Specialized;

using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Container;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraPrinting;
using DevExpress.Utils.Design;
using DevExpress.Utils;

using Webb.Data;
using Webb.Collections;
namespace Webb.Reports.ExControls.Data
{
	#region public class ValueColor 
	[Serializable]
	public class ValueColor:ISerializable 
	{
		public ValueColor()
		{
			_Value=string.Empty;
			_Color=Color.Empty;
			_TextColor=Color.Empty;
			_RectBorderColor=Color.Empty;
			_GradientColor=Color.Empty;
			_GradientAngle=0f;

		}
		public ValueColor(string field,Color color)
		{
			_Value=field;
			_Color=color;
			_TextColor=Color.Empty;
			_RectBorderColor=Color.Empty;
			_GradientColor=Color.Empty;
			_GradientAngle=0f;
			_LengthConnector=0;

		}


		protected string _Value=string.Empty;
		protected Color _Color=Color.Empty;
		protected Color _TextColor=Color.Empty;
		protected Color _RectBorderColor=Color.Empty;
		protected bool  _Hidden=false;
		protected Color _GradientColor=Color.Empty;
		protected float _GradientAngle=0f;
		protected int _LengthConnector=0;		

		public override string ToString()
		{
			if(_Hidden)
			{
				return "[NoExist]"+this._Value;
			}

			return this._Value;
		}
	   
		[Category("Length Connector")]
		public int LengthConnector
		{
			get{ return _LengthConnector; }
			set{ _LengthConnector = value; }
		}


        [Browsable(false)]
		public string Value
		{
			get{ return _Value; }
			set{ _Value = value; }
		}
       
		[Category("Gradient Option")]
		public System.Drawing.Color ChartColor
		{
			get{ return _Color; }
			set{ _Color = value; }
		}
        [Category("SeriesLabel")]
		public System.Drawing.Color TextColor
		{
			get{ return _TextColor; }
			set{ _TextColor = value; }
		}
		[Category("SeriesLabel")]
		public System.Drawing.Color RectBorderColor
		{
			get{ return _RectBorderColor; }
			set{ _RectBorderColor = value; }
		}
        [Browsable(false)]
		public bool Hidden
		{
			get{ return _Hidden; }
			set{ _Hidden = value; }
		}
        [Category("Gradient Option")]
		public System.Drawing.Color GradientColor
		{
			get{ return _GradientColor; }
			set{ _GradientColor = value; }
		}
        [Category("Gradient Option")]
		public float GradientAngle
		{
			get{ return _GradientAngle; }
			set{ _GradientAngle = value; }
		}	

		#region Serialization By Simon's Macro 2009-4-24 8:38:14
		public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			info.AddValue("_Value",_Value);
			info.AddValue("_Color",_Color,typeof(System.Drawing.Color));
			info.AddValue("_TextColor",_TextColor,typeof(System.Drawing.Color));
			info.AddValue("_RectBorderColor",_RectBorderColor,typeof(System.Drawing.Color));
			info.AddValue("_Hidden",_Hidden);
			info.AddValue("_GradientColor",_GradientColor,typeof(System.Drawing.Color));
			info.AddValue("_GradientAngle",_GradientAngle);
			info.AddValue("_LengthConnector",_LengthConnector);
		
		}

		public ValueColor(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			try
			{
				_LengthConnector=info.GetInt32("_LengthConnector");
			}
			catch
			{
				_LengthConnector=0;
			}
			try
			{
				_Value=info.GetString("_Value");
			}
			catch
			{
				_Value=string.Empty;
			}
			try
			{
				_Color=(System.Drawing.Color)info.GetValue("_Color",typeof(System.Drawing.Color));
			}
			catch
			{
				_Color=Color.Empty;
			}
			try
			{
				_TextColor=(System.Drawing.Color)info.GetValue("_TextColor",typeof(System.Drawing.Color));
			}
			catch
			{
				_TextColor=Color.Empty;
			}
			try
			{
				_RectBorderColor=(System.Drawing.Color)info.GetValue("_RectBorderColor",typeof(System.Drawing.Color));
			}
			catch
			{
				_RectBorderColor=Color.Empty;
			}
			try
			{
				_Hidden=info.GetBoolean("_Hidden");
			}
			catch
			{
				_Hidden=false;
			}
			try
			{
				_GradientColor=(System.Drawing.Color)info.GetValue("_GradientColor",typeof(System.Drawing.Color));
			}
			catch
			{
                _GradientColor=Color.Empty;
			}
			try
			{
				_GradientAngle=info.GetSingle("_GradientAngle");
			}
			catch
			{
				_GradientAngle=0f;
			}
		}
		#endregion

		#region Copy Function By Macro 2009-4-24 10:51:22
		public ValueColor Copy()
		{
			ValueColor thiscopy=new ValueColor();
			thiscopy._Value=this._Value;
			thiscopy._Color=this._Color;
			thiscopy._TextColor=this._TextColor;
			thiscopy._RectBorderColor=this._RectBorderColor;
			thiscopy._Hidden=this._Hidden;
			thiscopy._GradientColor=this._GradientColor;
			thiscopy._GradientAngle=this._GradientAngle;
			thiscopy._LengthConnector=this._LengthConnector;
			return thiscopy;
		}
		#endregion
       

	}
	#endregion

	#region public class ValueColorCollection
	[Serializable]
	public class ValueColorCollection : CollectionBase
	{		
		public ValueColorCollection()
		{
			
		}
	
		public ValueColorCollection Copy()
		{
			ValueColorCollection valueColors = new ValueColorCollection();

			valueColors.series=this.series;
			
			foreach(ValueColor valueColor in this)
			{
				valueColors.Add(valueColor.Copy());
			}
			return valueColors;
		}
	    
        [NonSerialized]
        public Series series;	 		

		public ValueColor this[int i_index]
		{
			get
			{
				return this.InnerList[i_index] as ValueColor;
			}
			set
			{
				this.InnerList[i_index] = value;
			}
		}


		public ValueColor GetColor(string strValue)
		{
			foreach(ValueColor valueColor in this)
			{
				if(valueColor.Value.ToLower()==strValue.ToLower())
				{
					return valueColor;
				}			
			}
			return null;
		}
		public int Add(ValueColor valueColor)
		{
			return this.InnerList.Add(valueColor);
		}

		public bool Contains(ValueColor valueColor)
		{
			foreach(ValueColor vcolor in this)
			{
				if(vcolor.Value.ToLower()==valueColor.Value.ToLower())
				{
					return true;
				}				
			}
			return false;
		}

		public static DataTable GetDataSource()
		{
			//07-09-2008@Scott
			//Begin Edit         
			DataSet ds = Webb.Reports.DataProvider.VideoPlayBackManager.DataSource;
			
			if(ds == null || ds.Tables.Count <= 0) return null;

			DataTable table = ds.Tables[0];

			return table;
		}
		public ValueColorCollection CreateValuesColor()
		{
			ValueColorCollection valusColor=new ValueColorCollection();
			
            valusColor.series=this.series;

			DataTable dt=GetDataSource();

			if(dt==null||this.series==null)return  valusColor;			

			ArrayList fields=ReFilter.GetFields(dt,series);            
			
			foreach(string field in fields)
			{
				valusColor.Add(new ValueColor(field,Color.Empty));
			}
			return valusColor;

		}
	
	

		
	}
	#endregion
    
	#region public class ReElement
	[Serializable]
	public class ReElement:ISerializable
	{
		#region Auto Constructor By Macro 2009-4-20 13:56:31
		public ReElement()
		{
			_Value=string.Empty;
			_Frequence=0;
			_FilterType=FilterTypes.NumGreaterOrEqual;
            _FollowedOperand=FilterOperands.Or;

		}
		#endregion

	
		protected string _Value;
		protected int _Frequence=0;		
		protected FilterTypes _FilterType=FilterTypes.NumGreaterOrEqual;		
		protected FilterOperands _FollowedOperand;

		public bool CheckResult(int FreqNum)
		{
			bool b_Result=true;

			switch(_FilterType)
			{
				case FilterTypes.NumGreaterOrEqual:
				default:
					b_Result=(FreqNum>=this.Frequence);
					break;
					
				case FilterTypes.NumEqual:
					b_Result=(FreqNum==this.Frequence);
					break;
					
				case FilterTypes.NumGreater:
					b_Result=(FreqNum>this.Frequence);
					break;
					
				case FilterTypes.NumLess:
					b_Result=(FreqNum<this.Frequence);
					break;
				
				case FilterTypes.NumLessOrEqual:
					b_Result=(FreqNum<=this.Frequence);
					break;
				
				case FilterTypes.NumNotEqual:
					b_Result=(FreqNum!=this.Frequence);
					break;
					
			}
			return b_Result;

		}

		public string Value
		{
			get{ return _Value; }
			set{ _Value = value; }
		}

		public int Frequence
		{
			get{ return _Frequence; }
			set{ _Frequence = value; }
		}

		public Webb.Data.FilterTypes FilterType
		{
			get{ return _FilterType; }
			set{ _FilterType = value; }
		}

		public Webb.Data.FilterOperands FollowedOperand
		{
			get{ return _FollowedOperand; }
			set{ _FollowedOperand = value; }
		}

		#region Copy Function By Macro 2009-4-22 8:16:16
		public ReElement Copy()
		{
			ReElement thiscopy=new ReElement();
			thiscopy._Value=this._Value;
			thiscopy._Frequence=this._Frequence;
			thiscopy._FilterType=this._FilterType;
			thiscopy._FollowedOperand=this._FollowedOperand;
			return thiscopy;
		}
		#endregion

		#region Serialization By Simon's Macro 2009-4-22 8:16:19
		public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			info.AddValue("_Value",_Value);
			info.AddValue("_Frequence",_Frequence);
			info.AddValue("_FilterType",_FilterType,typeof(Webb.Data.FilterTypes));
			info.AddValue("_FollowedOperand",_FollowedOperand,typeof(Webb.Data.FilterOperands));
		
		}

		public ReElement(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			try
			{
				_Value=info.GetString("_Value");
			}
			catch
			{
				_Value=string.Empty;
			}
			try
			{
				_Frequence=info.GetInt32("_Frequence");
			}
			catch
			{
				_Frequence=0;
			}
			try
			{
				_FilterType=(Webb.Data.FilterTypes)info.GetValue("_FilterType",typeof(Webb.Data.FilterTypes));
			}
			catch
			{
				_FilterType=FilterTypes.NumGreaterOrEqual;
			}
			try
			{
				_FollowedOperand=(Webb.Data.FilterOperands)info.GetValue("_FollowedOperand",typeof(Webb.Data.FilterOperands));
			}
			catch
			{
                 _FollowedOperand=FilterOperands.Or;
			}
		}
		#endregion	
	
	}
	#endregion

	#region public class ReFilter
	[Serializable]
	public class ReFilter:CollectionBase
	{		
		public ReFilter()
		{	_TotalOther=false;
		    _OtherName="Other";
		}

		public ReElement this[int i_index]
		{
			get
			{
				return this.InnerList[i_index] as ReElement;
			}
			set
			{
				this.InnerList[i_index] = value;
			}
		}


		protected bool _TotalOther=false;
		protected string _OtherName="Other";

		public bool TotalOther
		{
			get{ return _TotalOther; }
			set{ _TotalOther = value; }
		}

		public string OtherName
		{
			get{
				if(_OtherName==null)_OtherName="Other";
				return _OtherName;
			   }
			set{ _OtherName = value; }
		}
	

		#region Basic Functions For CollectionBase
			public int Add(ReElement reElement)
			{
				return this.InnerList.Add(reElement);
			}
			public void Remove(ReElement reElement)
			{
				this.InnerList.Remove(reElement);
			}		
			public bool Contains(ReElement reElement)
			{
				foreach(ReElement element in this)
				{
					if(element.Value.ToLower()==reElement.Value.ToLower())
					{
						return true;
					}				
				}
				return false;
				
			}
			
		#endregion

		#region Copy Function By Macro 2009-4-20 13:43:34
		public ReFilter Copy()
		{
			ReFilter thiscopy=new ReFilter();			
			
			foreach(ReElement value in this)
			{
				thiscopy.Add(value);
			}
			thiscopy._TotalOther=this._TotalOther;
            thiscopy._OtherName=this._OtherName;
			return thiscopy;
		}
		#endregion
      
		#region CalculateResult

			public void AddNewGroupResult(GroupInfo groupInfo,Int32Collection i_ExRows)
			{
				if(!this._TotalOther||groupInfo==null||i_ExRows==null||i_ExRows.Count==0)return;

				GroupResult m_GroupResult = new GroupResult();

				m_GroupResult.GroupValue = this.OtherName;
				
				m_GroupResult.RowIndicators=i_ExRows;

				m_GroupResult.ParentGroupInfo=groupInfo;  //Add at 2009-2-19 14:23:47@Simon

				m_GroupResult.ClickEvent = groupInfo.ClickEvent;

				groupInfo.GroupResults.Add(m_GroupResult);
			}

			public Int32Collection GetReFilterRows(DataTable i_Table,GroupInfo groupInfo,Int32Collection rows)
			{
				if(i_Table == null || rows==null||groupInfo==null)
				{
					return rows;
				}
				
				Int32Collection reFilterRows=new Int32Collection();			
				
				groupInfo.CalculateGroupResult(i_Table,rows,rows,rows,groupInfo);					        
			
				foreach(GroupResult m_Result in groupInfo.GroupResults)
				{
					if(!this.CheckResult(m_Result.RowIndicators.Count))continue;					

					reFilterRows=reFilterRows.Combine(reFilterRows,m_Result.RowIndicators);
				}			

				return reFilterRows;
				
			}
			public bool CheckResult(int FreqNum)
			{
				try
				{
					//12-17-2007@Scott
					BoolStack stack = new BoolStack();
					
					foreach(ReElement elemnt in this)
					{
						bool bResult = elemnt.CheckResult(FreqNum);

						BoolResult br = new BoolResult(bResult,Bracket.NONE,elemnt.FollowedOperand);		
							
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

			
		    public static ArrayList GetFields(DataTable i_Table,Series series)
			{
				GroupInfo groupInfo;

				if(series.FieldArgument==string.Empty&&series.SectionFilters.Count>0)
				{
					groupInfo=new SectionGroupInfo(series.SectionFilters);					
				}
				else
				{
					groupInfo=new FieldGroupInfo(series.FieldArgument);  
				}

				Int32Collection rows=series.Filter.GetFilteredRows(i_Table);

                rows=series.MinValueFilter.GetReFilterRows(i_Table,groupInfo,rows);
              
				ArrayList fields=groupInfo.GetFields(i_Table,rows);

				if(series.MinValueFilter.TotalOther)
				{
					fields.Add(series.MinValueFilter.OtherName);
				}


               return fields;
			}
			private GroupInfo CreateGroupInfo(Series series)
			{			
				GroupInfo groupInfo;

				if(series.FieldArgument==string.Empty&&series.SectionFilters.Count>0)
				{
					groupInfo=new SectionGroupInfo(series.SectionFilters);					
				}
				else
				{
					groupInfo=new FieldGroupInfo(series.FieldArgument);  
				}

				groupInfo.Filter=series.Filter.Copy();

				return groupInfo;

			}
          
		#endregion
		

		

	}
	#endregion

}
