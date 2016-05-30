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
	#region public DataStructure for Grouping
	[Serializable]
	public enum SortingTypes
	{		
		Ascending,
		Descending
	}

    
	[Serializable]
	public enum SortingByTypes
	{
		None,
		/// Sorting by the fileds that in the database.
		GroupedVale,
		/// Sort by the group result of count.
		Frequence,
		/// Sort by thg football field.
		///  (-1 to -49, 50 to 1)American  or (-1 to -54 55 to 1 )Canadian
		FootballField,
		/// Sort by number
		Number,

		GroupedValueOrNumber,

        UserDefinedOrder,

        PlayerPosition, // 09-26-2011 Scott

        DateTime, // 10-13-2011 Scott
	}

	#endregion

    #region public class UserOrderClS
    [Serializable]
    public class UserOrderClS : ISerializable
    {
        #region Auto Constructor By Macro 2011-4-11 17:02:26
		public UserOrderClS()
        {
			_OrderValues=new ArrayList();			
        }

        public UserOrderClS(GroupInfo p_RelativeGroupInfo, ArrayList p_OrderValues)
        {
			_OrderValues=p_OrderValues;
			_RelativeGroupInfo=p_RelativeGroupInfo;
        }
		#endregion

        #region Fields
           protected ArrayList _OrderValues = new ArrayList();

           [NonSerialized]
           protected GroupInfo _RelativeGroupInfo =null;
           
          #endregion

       #region Serialization By Simon's Macro 2011-4-11 17:01:05
	       public void GetObjectData(SerializationInfo info, StreamingContext context)
           {
		    info.AddValue("_OrderValues",_OrderValues,typeof(System.Collections.ArrayList));
    		
           }

           public UserOrderClS(SerializationInfo info, StreamingContext context)
           {
		    try
		    {
			    _OrderValues=(System.Collections.ArrayList)info.GetValue("_OrderValues",typeof(System.Collections.ArrayList));
		    }
		    catch
		    {
			    _OrderValues=new ArrayList();
		    }			
         }
	  #endregion

       #region Properties
           public System.Collections.ArrayList OrderValues
           {
               get
               {
                   if (_OrderValues == null) _OrderValues = new ArrayList();

                  return _OrderValues;
               }
               set
               {
                 _OrderValues = value;
               }
           }

           public Webb.Reports.ExControls.Data.GroupInfo RelativeGroupInfo
           {
               get
               {
                return _RelativeGroupInfo;
               }
               set
               {
                _RelativeGroupInfo = value;
               }
           }
       #endregion

       public bool Contains(object objValue)
       {
           return this.OrderValues.Contains(objValue);
       }
       public int IndexOf(object objValue)
       {
           return this.OrderValues.IndexOf(objValue);
       }

    }

    #endregion

    #region public class GroupInfo
    /*Descrition:   */
	[Serializable]
	public class GroupInfo : ISerializable
	{
		#region Serialization By Scott 2008-12-11 14:59:08
		virtual public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			info.AddValue("_SortingType",_SortingType,typeof(Webb.Reports.ExControls.Data.SortingTypes));
			info.AddValue("_GroupTitle",_GroupTitle);
			info.AddValue("_TopCount",_TopCount);
			info.AddValue("_AddGroupTotal",_AddGroupTotal);
			info.AddValue("_OneValuePerRow",_OneValuePerRow);
			info.AddValue("_FollowedSummaries",_FollowedSummaries,typeof(GroupSummaryCollection));
			info.AddValue("_TotalSummaries",_TotalSummaries,typeof(GroupSummaryCollection));
			//			info.AddValue("_GroupResults",_GroupResults,typeof(GroupResultCollection));
			//			info.AddValue("_ParentGroupResult",_ParentGroupResult,typeof(GroupResult));
			info.AddValue("_SubGroupInfo",_SubGroupInfo,typeof(Webb.Reports.ExControls.Data.GroupInfo));
			info.AddValue("_SubGroupInfos",_SubGroupInfos,typeof(Webb.Reports.ExControls.Data.GroupInfoCollection));
			info.AddValue("_SortingByType",_SortingByType,typeof(Webb.Reports.ExControls.Data.SortingByTypes));
			info.AddValue("_TotalTitle",_TotalTitle);
			info.AddValue("_ClickEvent",_ClickEvent,typeof(Webb.Reports.ExControls.ClickEvents));
			info.AddValue("_Style",_Style,typeof(Webb.Reports.ExControls.BasicStyle));
			//			info.AddValue("_ColumnIndex",_ColumnIndex);
			info.AddValue("_FollowSummaries",_FollowSummaries);
			info.AddValue("_TitleFormat",_TitleFormat,typeof(System.Drawing.StringFormatFlags));
			info.AddValue("_Filter",_Filter,typeof(Webb.Data.DBFilter));
			info.AddValue("_OneValuePerPage",_OneValuePerPage);
			info.AddValue("_ColumnWidth",_ColumnWidth);
			//			info.AddValue("_SummaryBands",_SummaryBands,typeof(SummaryBandCollection));
			info.AddValue("_ReportScType",_ReportScType,typeof(ReportScType));	//Modified at 2009-1-21 14:10:32@Scott
			info.AddValue("_ColorNeedChange",_ColorNeedChange);	//Modified at 2009-2-11 16:08:30@Scott
			info.AddValue("_GroupSides",this._GroupSides); //Add at 2009-2-27 9:26:18@Simon
			info.AddValue("_FollowsWith",this._FollowsWith); //2009-3-31 9:13:42@Simon Add this Code
			info.AddValue("_ShowSymbol",this._ShowSymbol); //2009-3-31 9:13:42@Simon Add this Code
			info.AddValue("_IsRelatedForSubGroup",this._IsRelatedForSubGroup); //2009-3-31 9:13:42@Simon Add this Code
			info.AddValue("_SectionSummeries",this._SectionSummeries,typeof(GroupSummaryCollection));
            info.AddValue("_DisregardBlank", this._DisregardBlank); //2009-3-31 9:13:42@Simon Add this Code
            info.AddValue("_IsSectionOutSide", this._IsSectionOutSide); //2009-3-31 9:13:42@Simon Add this Code
            info.AddValue("_DisplayAsColumn", this._DisplayAsColumn); //2009-3-31 9:13:42@Simon Add this Code
            info.AddValue("_Description", this._Description); //2009-3-31 9:13:42@Simon Add this Code
            info.AddValue("_UserDefinedOrders", _UserDefinedOrders, typeof(UserOrderClS));
            info.AddValue("_DisplayAsImage", _DisplayAsImage);
            info.AddValue("_SkippedCount", _SkippedCount);
		
		}

		public GroupInfo(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
            try
            {
                _SkippedCount = info.GetInt32("_SkippedCount");
            }
            catch
            {
                _SkippedCount = 0;
            }
            try
            {
                _DisplayAsImage = info.GetBoolean("_DisplayAsImage");
            }
            catch
            {
                _DisplayAsImage = false;
            }
            try
            {
                _UserDefinedOrders = (UserOrderClS)info.GetValue("_UserDefinedOrders", typeof(UserOrderClS));
            }
            catch
            {
                _UserDefinedOrders = new UserOrderClS();
            }
            try
            {
                _Description = info.GetString("_Description");
            }
            catch
            {
                _Description = string.Empty;
            }
            try
            {
                _DisregardBlank = info.GetBoolean("_DisregardBlank");
            }
            catch
            {
                _DisregardBlank =false;
            }
			
			try
			{
				_ShowSymbol = info.GetString("_ShowSymbol");
			}
			catch
			{
				_ShowSymbol = string.Empty;
			}
			try
			{
				_IsRelatedForSubGroup = info.GetBoolean("_IsRelatedForSubGroup");
			}
			catch
			{
				_IsRelatedForSubGroup = false;
			}
			try
			{
				_GroupSides = info.GetBoolean("_GroupSides");
			}
			catch
			{
				_GroupSides = false;
			}

			try
			{
				_SortingType = (SortingTypes)info.GetValue("_SortingType",typeof(SortingTypes));
			}
			catch
			{
				_SortingType = Webb.Reports.ExControls.Data.SortingTypes.Descending;
			}

			try
			{
				_GroupTitle = info.GetString("_GroupTitle");
			}
			catch
			{
				_GroupTitle = "New Group";
			}

			try
			{
				_TopCount = info.GetInt32("_TopCount");
			}
			catch
			{
				_TopCount = 0;
			}

			try
			{
				_AddGroupTotal = info.GetBoolean("_AddGroupTotal");
			}
			catch
			{
				_AddGroupTotal = false;
			}

			try
			{
				_OneValuePerRow = info.GetBoolean("_OneValuePerRow");
			}
			catch
			{
				_OneValuePerRow = false;
			}          
			try
			{
				_FollowedSummaries = info.GetValue("_FollowedSummaries",typeof(GroupSummaryCollection)) as GroupSummaryCollection;
			}
			catch
			{
				_FollowedSummaries = new GroupSummaryCollection();
			}

			try
			{
				_TotalSummaries = info.GetValue("_TotalSummaries",typeof(GroupSummaryCollection)) as GroupSummaryCollection;
			}
			catch
			{
				_TotalSummaries = new GroupSummaryCollection();
			}

			//			try
			//			{
			//				_GroupResults = info.GetValue("_GroupResults",typeof(GroupResultCollection)) as GroupResultCollection;
			//			}
			//			catch
			//			{
			//				_GroupResults = new GroupResultCollection();
			//			}

			//			try
			//			{
			//				_ParentGroupResult = info.GetValue("_ParentGroupResult",typeof(GroupResult)) as GroupResult;
			//			}
			//			catch
			//			{
			//				_ParentGroupResult = new GroupResult();
			//			}

			try
			{
				_SubGroupInfo = info.GetValue("_SubGroupInfo",typeof(GroupInfo)) as GroupInfo;
			}
			catch
			{
				
			}

			try
			{
				_SubGroupInfos = info.GetValue("_SubGroupInfos",typeof(GroupInfoCollection)) as GroupInfoCollection;
			}
			catch
			{
				_SubGroupInfos = new GroupInfoCollection();
			}

			try
			{
				_SortingByType = (SortingByTypes)info.GetValue("_SortingByType",typeof(SortingByTypes));
			}
			catch
			{
				_SortingByType = SortingByTypes.Frequence;
			}

			try
			{
				_TotalTitle = info.GetString("_TotalTitle");
			}
			catch
			{
				_TotalTitle = "Total";
			}

			try
			{
				_ClickEvent=(ClickEvents)info.GetValue("_ClickEvent",typeof(ClickEvents));
			}
			catch
			{
				_ClickEvent = ClickEvents.PlayVideo;
			}

			try
			{
				_Style = info.GetValue("_Style",typeof(BasicStyle)) as BasicStyle;
			}
			catch
			{
				_Style = new BasicStyle();
			}

			//			try
			//			{
			//				_ColumnIndex = info.GetInt32("_ColumnIndex");
			//			}
			//			catch
			//			{
			//				
			//			}

			try
			{
				_FollowSummaries = info.GetBoolean("_FollowSummaries");
			}
			catch
			{
				_FollowSummaries = false;
			}

			try
			{
				_TitleFormat = (StringFormatFlags)info.GetValue("_TitleFormat",typeof(StringFormatFlags));
			}
			catch
			{
				_TitleFormat = 0;
			}

			try
			{
				_Filter = info.GetValue("_Filter",typeof(DBFilter)) as DBFilter;

				this._Filter=AdvFilterConvertor.GetAdvFilter(DataProvider.VideoPlayBackManager.AdvReportFilters,this._Filter);    //2009-4-29 11:37:37@Simon Add UpdateAdvFilter
			}
			catch
			{
				_Filter = new DBFilter();
			}

			try
			{
				_OneValuePerPage = info.GetBoolean("_OneValuePerPage");
			}
			catch
			{
				_OneValuePerPage = false;
			}

			try
			{
				_ColumnWidth = info.GetInt32("_ColumnWidth");
			}
			catch
			{
				_ColumnWidth = BasicStyle.ConstValue.CellWidth;
			}

			//			try
			//			{
			//				_SummaryBands = info.GetValue("_SummaryBands",typeof(SummaryBandCollection)) as SummaryBandCollection;
			//			}
			//			catch
			//			{
			//				
			//			}

			try
			{
				_ReportScType = (ReportScType)info.GetValue("_ReportScType",typeof(ReportScType));
			}
			catch
			{
				_ReportScType = ReportScType.Custom;
			}

			try	//Modified at 2009-2-11 16:09:53@Scott
			{
				_ColorNeedChange = info.GetBoolean("_ColorNeedChange");
			}
			catch
			{
				_ColorNeedChange = true;
			}
			try
			{
				this._FollowsWith = info.GetString("_FollowsWith");
			}
			catch
			{
				_FollowsWith = "";
			}
			try
			{
				this._SectionSummeries = info.GetValue("_SectionSummeries",typeof(GroupSummaryCollection)) as GroupSummaryCollection;
			}
			catch
			{
				_SectionSummeries = new GroupSummaryCollection();//
			}
            try
            {
                this._DisplayAsColumn = info.GetBoolean("_DisplayAsColumn");
            }
            catch
            {
                _DisplayAsColumn=false;                
            }
            try
            {
                this._IsSectionOutSide = info.GetBoolean("_IsSectionOutSide");
            }
            catch
            {
               if(this is SectionGroupInfo)
               {
                    _IsSectionOutSide = true;
                }
                else
                {
                    _IsSectionOutSide = false;
                }
            }

		}
		#endregion

		#region Fields

        protected int _SkippedCount = 0;
		/// Determine the sorting aspect.
		protected SortingTypes _SortingType;
		/// Column Title
		protected string _GroupTitle;
		/// Determine how many group results to show.
        
		protected int _TopCount = 0;
		/// Determine that if the group add the total summay after finished all the grouping result.
		protected bool _AddGroupTotal = false;
		protected bool _OneValuePerRow = false;
		protected GroupSummaryCollection _FollowedSummaries;
		protected GroupSummaryCollection _TotalSummaries;
		protected GroupResultCollection _GroupResults;
		protected GroupResult _ParentGroupResult;
		protected GroupInfo _SubGroupInfo;
		protected GroupInfoCollection _SubGroupInfos;	//03-18-2008@Scott
		protected SortingByTypes _SortingByType;
		protected string _TotalTitle;
		protected ClickEvents _ClickEvent;
		protected BasicStyle _Style;				//05-26-2008@Scott
		protected bool _ColorNeedChange;
		protected int _ColumnIndex;					//05-26-2008@Scott
		protected bool _FollowSummaries;			//06-24-2008@Scott
		protected StringFormatFlags _TitleFormat;	//06-26-2008@Scott
		protected DBFilter _Filter;					//07-02-2008@Scott
		protected bool _OneValuePerPage;			//07-04-2008@Scott
		public int _ColumnWidth = BasicStyle.ConstValue.CellWidth;
		private SummaryBandCollection _SummaryBands;
		protected ReportScType _ReportScType;		//Modified at 2009-1-21 11:34:11@Scott

		protected bool _GroupSides=false;  //Add at 2009-2-27 9:25:08@Simon
		protected string _FollowsWith=string.Empty;
		protected bool _IsRelatedForSubGroup=false;
		protected string _ShowSymbol=string.Empty;

        protected bool _DisregardBlank = false;

        protected bool _DisplayAsColumn = false;

        protected string _Description = string.Empty;

        
        protected bool _IsSectionOutSide = false;

        protected UserOrderClS _UserDefinedOrders = new UserOrderClS();

		[NonSerialized]
		public bool Converted=false;

		[NonSerialized]
		public int LevelInfo;   
   
		protected GroupSummaryCollection _SectionSummeries=new GroupSummaryCollection();

        [NonSerialized]
        protected Size LimitSize = Size.Empty;

        protected bool _DisplayAsImage = false;

		#endregion

		#region Constructor
		public GroupInfo()
		{
			this._AddGroupTotal = false;
			this._SortingByType = SortingByTypes.Frequence;
			this._SortingType = SortingTypes.Descending;
			this._TopCount  = 0;
			this._TotalTitle = "Total";
			this._ClickEvent = ClickEvents.PlayVideo;
			this._SubGroupInfos = new GroupInfoCollection();	//03-18-2008@Scott
			this._ColorNeedChange = true;
			_GroupSides=false; 
			_IsRelatedForSubGroup=false;
			
		}
		
		public GroupInfo(PageGroupInfo pageGroupInfo)
		{
			this._AddGroupTotal = false;

			int sortByType=(int)pageGroupInfo.SortingByType;
			this._SortingByType = (SortingByTypes)sortByType;

            int sortType = (int)pageGroupInfo.SortingType;
            this._SortingType = (SortingTypes)sortType;

            this._SkippedCount = pageGroupInfo.SkippedCount;

			this._GroupTitle=pageGroupInfo.GroupTitle;
			this._TopCount  = pageGroupInfo.TopCount;
			this._TotalTitle = "Total";
			this._ClickEvent = ClickEvents.PlayVideo;
			this._SubGroupInfos = new GroupInfoCollection();	//03-18-2008@Scott
			this._ColorNeedChange = true;
			this._Filter=pageGroupInfo.Filter.Copy();

			_GroupSides=false;  
			_IsRelatedForSubGroup=false;
		}
		
		#endregion

		#region virtual Functions

        public virtual void GetAllUsedFields(ref ArrayList _UsedFields)
        {
        }

		public virtual void ClearGroupResult(GroupInfo i_GroupInfo)
		{
			if(i_GroupInfo.GroupResults != null)
			{
				i_GroupInfo.GroupResults.Clear();
			}

			if(i_GroupInfo.TotalSummaries != null)
			{
				i_GroupInfo.TotalSummaries.Clear();
			}

			//03-18-2008@Scott
			//Begin Edit
			//			if(i_GroupInfo.SubGroupInfo != null)
			//			{
			//				this.ClearGroupResult(i_GroupInfo.SubGroupInfo);
			//			}
			foreach(GroupInfo groupInfo in i_GroupInfo.SubGroupInfos)
			{
				this.ClearGroupResult(groupInfo);
			}
			//End Edit
		}
        
		public virtual void CalculateGroupResult(System.Data.DataTable i_Table, Webb.Collections.Int32Collection i_OuterRows,Webb.Collections.Int32Collection i_InnerRows,GroupInfo i_GroupInfo)
		{
			// TODO: implement
		}
        public virtual void CalculateGroupResult(DataTable i_Table, Int32Collection i_OuterRows, Int32Collection i_FilterRows, Int32Collection i_InnerRows, GroupInfo i_GroupInfo)
        {
            // TODO: implement
        }
      
		public virtual 	ArrayList GetFields(DataTable table, Int32Collection rows)
		{
			return new ArrayList();
		}
		virtual public int GetGroupedColumns()
		{
			return 0;
		}

		virtual public int GetGroupedRows()
		{
			return 0;
		}
        public virtual void SetAllSignToFalse(GroupInfo i_GroupInfo)
        {
            i_GroupInfo.IsSectionOutSide = false;

            foreach (GroupInfo groupInfo in i_GroupInfo.SubGroupInfos)
            {
                this.SetAllSignToFalse(groupInfo);
            }
            //End Edit
        }

        public static bool IsVisible(GroupInfo groupInfo)
        {
            bool visible = true;

            if (groupInfo is FieldGroupInfo)
            {
                visible = (groupInfo as FieldGroupInfo).Visible;
            }
            else if (groupInfo is SectionGroupInfo)
            {
                visible = (groupInfo as SectionGroupInfo).Visible;
            }

            return visible;

        }

		virtual public GroupInfo Copy()
		{
			GroupInfo m_GroupInfo = new GroupInfo();
			m_GroupInfo._AddGroupTotal = this._AddGroupTotal;
			if(this._FollowedSummaries!=null)
			{
				m_GroupInfo._FollowedSummaries = this._FollowedSummaries.CopyStructure();
			}
			if(this._TotalSummaries!=null)
			{
				m_GroupInfo._TotalSummaries = this._TotalSummaries.CopyStructure();
			}
			if(this._SectionSummeries!=null)
			{
				m_GroupInfo._SectionSummeries = this._SectionSummeries.CopyStructure();
			}
			//m_GroupInfo._GroupResults = this._AddGroupTotal;
			m_GroupInfo._GroupTitle = this._GroupTitle;
			m_GroupInfo._SortingByType = this._SortingByType;
			m_GroupInfo._SortingType = this._SortingType;
			m_GroupInfo._TopCount = this._TopCount;

			m_GroupInfo._GroupTitle = this._GroupTitle;
			m_GroupInfo._TotalTitle = this._TotalTitle;

			m_GroupInfo.Style = this.Style.Copy() as BasicStyle;

			m_GroupInfo._ClickEvent = this._ClickEvent;
			m_GroupInfo.Filter = this.Filter.Copy();	

			m_GroupInfo.ColumnIndex=this.ColumnIndex;  //Added this code at 2008-12-24 8:30:51@Simon

			m_GroupInfo.ReportScType = this.ReportScType;	//Modified at 2009-1-21 14:05:45@Scott

			m_GroupInfo._ColorNeedChange = this._ColorNeedChange;	//Modified at 2009-2-11 16:10:44@Scott

			m_GroupInfo._GroupSides=this._GroupSides;  //Add at 2009-2-27 9:30:20@Simon

			m_GroupInfo._ColumnWidth=this._ColumnWidth; 

			m_GroupInfo._IsRelatedForSubGroup=this._IsRelatedForSubGroup;
  
			m_GroupInfo.ShowSymbol=this._ShowSymbol;

            
			return m_GroupInfo;
		}

        virtual public string GetDescription()
        {
            return string.Empty;
        }

		#endregion

        #region Fuctions For MatrixControl
        public void InitResults()
        {
            this._GroupResults = new GroupResultCollection();
        }
        public void SortResults(GroupResultCollection m_Results)
        {
            GroupResultCollection sortedResults = new GroupResultCollection();

            foreach (GroupResult result in m_Results)
            {
                GroupResult innerResult = this._GroupResults[result.GroupValue];
                if (innerResult == null)
                {
                    throw new Exception("Results are not fit");
                }
                else
                {
                    sortedResults.Add(result);
                }
            }
            this._GroupResults = sortedResults;
        }
        public void CutOffResults(int iStart, int iCount)
        {
            if (iStart + iCount <= 0 || iStart >= this._GroupResults.Count) return;

            if (iCount == 0) iCount = this._GroupResults.Count - iStart;

            GroupResultCollection results = new GroupResultCollection();

            for (int i = iStart; i < iStart + iCount; i++)
            {
                if (i >= this._GroupResults.Count) break;

                results.Add(this._GroupResults[i]);
            }

            this._GroupResults = results;
        }
        public ArrayList KeptValuesAfterCutOff(int iStart, int iCount)
        {
            if (iStart + iCount <= 0 || iStart >= this._GroupResults.Count) return null;

            if (iCount == 0) iCount = this._GroupResults.Count - iStart;

            ArrayList keptValues = new ArrayList();

            for (int i = iStart; i < iStart + iCount; i++)
            {
                if (i >= this._GroupResults.Count) break;

                keptValues.Add(this._GroupResults[i].GroupValue.ToString());
            }
            return keptValues;
        }

        public static void CreateTotalResults(DataTable i_Table, Int32Collection i_Rows, MatrixInfo matrixInfo)
        {
            GroupInfo ColGroup = matrixInfo.ColGroup;

            GroupInfo tempGroup = matrixInfo.RowGroup.Copy();

            tempGroup.InitResults();

            foreach (GroupResult colResult in ColGroup.GroupResults)
            {
                foreach (GroupResult rowResult in colResult.SubGroupInfos[0].GroupResults)
                {
                    tempGroup._GroupResults.AppendResult(rowResult);
                }
            }
            if (tempGroup is FieldGroupInfo) tempGroup._GroupResults.Sort(tempGroup.Sorting, tempGroup.SortingBy, tempGroup.UserDefinedOrders);
        }
        #endregion

        #region Helpful Functions for ChartControl @simon

            public GroupInfo CreateNewByFilter(DataTable table, DBFilter filter)  //Added this code at 2008-12-9 9:27:45@Simon
	         {
		        GroupInfo i_groupinfo=new GroupInfo();
		        i_groupinfo._GroupResults=new GroupResultCollection();

		        foreach(GroupResult m_Result in this.GroupResults)
		        {
			        GroupResult m_GroupResult = new GroupResult();
			        m_GroupResult.GroupValue ="F:"+m_Result.GroupValue;
			        m_GroupResult.RowIndicators=filter.GetFilteredRows(table,m_Result.RowIndicators);
			        m_GroupResult.ClickEvent = this.ClickEvent;
			        i_groupinfo._GroupResults.Add(m_GroupResult);
		        }
		        return i_groupinfo;
	        }

	        public GroupInfoCollection	ConvertCollection(DataTable table,ref Int32Collection StructInfo)  //Added this code at 2008-12-9 9:27:45@Simon
	        {
		        GroupInfoCollection groupinfos=new GroupInfoCollection();
		        int count=-1;
		        ArrayList m_tempList=new ArrayList();
		        foreach(GroupResult m_result in this.GroupResults)
		        {
			        GroupResultCollection m_Results=GetResults(table,m_result,StructInfo);				
			        System.Diagnostics.Debug.Assert(count<0||m_Results.Count==count);
			        count=  m_Results.Count;  
			        m_tempList.Add(m_Results);
		        }
		        for(int i=0;i<count;i++)
		        { 
			        GroupInfo i_Groupinfo=new GroupInfo();
			        i_Groupinfo._GroupResults=new GroupResultCollection();
			        groupinfos.Add(i_Groupinfo);
		        }
		        foreach(GroupResultCollection m_Results in m_tempList)
		        {
			        for(int col=0;col<count;col++)
			        { 
				        GroupInfo i_Groupinfo=groupinfos[col];
				        i_Groupinfo.GroupResults.Add(m_Results[col]);					
			        }
		        }
		        return groupinfos;
    				
	        }
	     
            public GroupResultCollection GetResults(DataTable table,GroupResult result,Int32Collection StructInfo)
	        {
		        StructInfo.Clear();

		        GroupResultCollection m_Results=new GroupResultCollection();

		        foreach(GroupInfo i_Groupinfo in result.SubGroupInfos)
		        {	
			        int index=i_Groupinfo.ColumnIndex;	
    			
			        foreach(GroupResult m_result in i_Groupinfo.GroupResults)
			        {
				        m_Results.Add(m_result);				

				        StructInfo.Add(index);

				        if(m_result.Summaries!=null)
				        {
					        foreach(GroupSummary m_summary in m_result.Summaries)
					        {
						        int subindex=m_summary.ColumnIndex;

						        GroupResult m_GroupResult = new GroupResult();

						        m_GroupResult.GroupValue = "F"+subindex.ToString()+":"+m_result.GroupValue.ToString();

						        m_GroupResult.RowIndicators=m_summary.Filter.GetFilteredRows(table,m_result.RowIndicators);                       
    				
						        m_Results.Add(m_GroupResult);

						        StructInfo.Add(subindex);
    							
					        }	
				        }
			        }
    					
		        }			
		        return m_Results;

	        }      
    		
	    #endregion

		#region PageGroupInfo
		private static GroupInfo IntoGroupInfo(PageGroupInfo pageGroupInfo)
		{
			GroupInfo  groupInfo=null;

			if(pageGroupInfo is PageSectionInfo)
			{
				groupInfo=new SectionGroupInfo(pageGroupInfo as PageSectionInfo);
			}
			else if(pageGroupInfo is PageFieldInfo)
			{
				groupInfo=new FieldGroupInfo(pageGroupInfo as PageFieldInfo);
			}
			else 
			{
				groupInfo=new FieldGroupInfo("");
			}
			return groupInfo;
		}

		public static GroupInfo FromPageGroupInfo(PageGroupInfo pageGroupInfo)
		{
			GroupInfo  groupInfo=IntoGroupInfo(pageGroupInfo);

			ConvertPageGroup(groupInfo, pageGroupInfo);

			return groupInfo;
		}

		private static void ConvertPageGroup(GroupInfo groupInfo,PageGroupInfo pageGroupInfo)
		{
			if(groupInfo == null||pageGroupInfo==null) return;			

			groupInfo.SubGroupInfos.Clear();

			foreach(PageGroupInfo subpageGroupInfo in  pageGroupInfo.SubPageGroupInfos)
			{	
				GroupInfo subGroupInfo=FromPageGroupInfo(subpageGroupInfo);
				
				groupInfo.SubGroupInfos.Add(subGroupInfo);

				ConvertPageGroup(subGroupInfo,subpageGroupInfo);
				
			}
		}

		public void GetLeafGroupResults(GroupInfo groupInfo,ref GroupResultCollection results)
		{
			if(groupInfo.SortingBy!=SortingByTypes.None)
			{
                groupInfo.GroupResults.Sort(groupInfo.Sorting, groupInfo.SortingBy, groupInfo.UserDefinedOrders);		
			}

			foreach(GroupResult result in groupInfo.GroupResults)
			{ 
				result.Filter.Add(groupInfo.Filter);

				if(groupInfo.ParentGroupResult!=null)
				{
					GroupResult parentGroupResult=groupInfo.ParentGroupResult;

					result.GroupValue=parentGroupResult.GroupValue.ToString()+","+result.GroupValue.ToString();					

					result.Filter.Add(parentGroupResult.Filter);

				}
				if(result.SubGroupInfos==null||result.SubGroupInfos.Count == 0)
				{
					results.Add(result);                    
				}

				foreach(GroupInfo subGroupInfo in result.SubGroupInfos)
				{
					this.GetLeafGroupResults(subGroupInfo,ref results);	
				}
			}
		}

		#endregion

		#region Instance Functionss
		public void CalculateTotalResult()
		{
			// TODO: implement
		}

        public void ResetGroupResults(GroupResultCollection groupResults)
        {
            this._GroupResults = groupResults;
        }

        public int GetVirtualGridGroupColumns()
        {
            int columnCount = 0;

            if (this.DisplayAsColumn)
            {
                bool visible = true;

                if (this is FieldGroupInfo)
                {
                    visible = (this as FieldGroupInfo).Visible;
                }
                else if (this is SectionGroupInfo)
                {
                    visible = (this as SectionGroupInfo).Visible;
                }
                if (visible) columnCount++;

                columnCount += this.Summaries.Count;

            }           

            foreach (GroupInfo subGroupInfo in this.SubGroupInfos)
            {
                columnCount += subGroupInfo.GetVirtualGridGroupColumns();
            }

            return columnCount;
        }

		public void RemoveSubGroupInfo(GroupInfo removedGroupInfo)
		{
			if(this.SubGroupInfos.Contains(removedGroupInfo))
			{
				this.SubGroupInfos.Remove(removedGroupInfo);

				return;
			}

			foreach(GroupInfo subGroupInfo in this.SubGroupInfos)
			{
				subGroupInfo.RemoveSubGroupInfo(removedGroupInfo);
			}
		}

		
		public virtual void Apply(GroupInfo baseGroupInfo)
		{
			if(baseGroupInfo==null)return;

			this._AddGroupTotal=baseGroupInfo._AddGroupTotal;

			this._FollowedSummaries = baseGroupInfo._FollowedSummaries;
			
			this._TotalSummaries = baseGroupInfo._TotalSummaries;
			
			this.SubGroupInfos=baseGroupInfo.SubGroupInfos;
			this.SubGroupInfo=baseGroupInfo.SubGroupInfo;

			//this._GroupResults = this._AddGroupTotal;
		
			this._SortingByType = baseGroupInfo._SortingByType;
			this._SortingType = baseGroupInfo._SortingType;
            this.UserDefinedOrders = new UserOrderClS();
            this.UserDefinedOrders.RelativeGroupInfo = this;
            foreach (object objValue in baseGroupInfo.UserDefinedOrders.OrderValues)
            {
                this.UserDefinedOrders.OrderValues.Add(objValue);
            }
			this._TopCount = baseGroupInfo._TopCount;
			this._GroupTitle = baseGroupInfo._GroupTitle;
			this._TotalTitle = baseGroupInfo._TotalTitle;
			this.Style = baseGroupInfo.Style.Copy() as BasicStyle;
			this._ClickEvent = baseGroupInfo._ClickEvent;
			this.Filter = baseGroupInfo.Filter.Copy();	
         
			this.ColumnIndex=baseGroupInfo.ColumnIndex;  //Added this code at 2008-12-24 8:30:51@Simon

			this.ReportScType = baseGroupInfo.ReportScType;	//Modified at 2009-1-21 14:05:45@Scott	

		
			this._ColorNeedChange = baseGroupInfo._ColorNeedChange;	//Modified at 2009-2-11 16:11:11@Scott

			this._GroupSides=baseGroupInfo._GroupSides;  //Add at 2009-2-27 9:30:20@Simon

			this._ColumnWidth=baseGroupInfo._ColumnWidth;

			this._IsRelatedForSubGroup=baseGroupInfo._IsRelatedForSubGroup;
  
			this.ShowSymbol=baseGroupInfo._ShowSymbol;

            this._Description = baseGroupInfo.Description;
			
		   this._SectionSummeries=baseGroupInfo.SectionSummeries.CopyStructure();

           this._DisplayAsColumn = baseGroupInfo._DisplayAsColumn;

           this._DisregardBlank = baseGroupInfo._DisregardBlank;

           this._DisplayAsImage = baseGroupInfo.DisplayAsImage;

           this._IsSectionOutSide = baseGroupInfo._IsSectionOutSide;
		}


		public void ApplySimple(GroupInfo baseGroupInfo)
		{
			if(baseGroupInfo==null)return;

			this._AddGroupTotal=baseGroupInfo._AddGroupTotal;			
			
			this._SortingByType = baseGroupInfo._SortingByType;
			this._SortingType = baseGroupInfo._SortingType;

            this.UserDefinedOrders = new UserOrderClS();
            this.UserDefinedOrders.RelativeGroupInfo = this;
            foreach (object objValue in baseGroupInfo.UserDefinedOrders.OrderValues)
            {
                this.UserDefinedOrders.OrderValues.Add(objValue);
            }

			this._TopCount = baseGroupInfo._TopCount;
			
			this._TotalTitle = baseGroupInfo._TotalTitle;

			this.Style = baseGroupInfo.Style.Copy() as BasicStyle;

			this._ClickEvent = baseGroupInfo._ClickEvent;

			this.Filter = baseGroupInfo.Filter.Copy();	
		
			this._ColorNeedChange = baseGroupInfo._ColorNeedChange;	//Modified at 2009-2-11 16:11:11@Scott

			this._GroupSides=baseGroupInfo._GroupSides;  //Add at 2009-2-27 9:30:20@Simon

			this._ColumnWidth=baseGroupInfo._ColumnWidth;

			this._IsRelatedForSubGroup=baseGroupInfo._IsRelatedForSubGroup;

            this._Description = baseGroupInfo.Description;
  
			this.ShowSymbol=baseGroupInfo._ShowSymbol;

            this._DisplayAsColumn = baseGroupInfo._DisplayAsColumn;

            this._DisregardBlank = baseGroupInfo._DisregardBlank;

            this._DisplayAsImage = baseGroupInfo.DisplayAsImage;

            this._IsSectionOutSide = baseGroupInfo._IsSectionOutSide;

		}
		

		//Modified at 2009-1-21 14:58:51@Scott
		public void UpdateSectionSummaries()
		{
			this.ReportScType=AdvFilterConvertor.GetScType(this.ReportScType,DataProvider.VideoPlayBackManager.AdvSectionType);  //2009-6-16 10:18:47@Simon Add this Code	    
					
			if(this.ReportScType != ReportScType.Custom)
			{
				if(this.Summaries == null) this.Summaries = new GroupSummaryCollection();

				this.Summaries.Clear();

				AdvFilterConvertor convertor = new AdvFilterConvertor();

				SectionFilterCollection scFilters = convertor.GetReportFilters(Webb.Reports.DataProvider.VideoPlayBackManager.AdvReportFilters,this.ReportScType);

				if(this.SectionSummeries.Count==0)
				{
                   this.SectionSummeries.Add(new GroupSummary());
				}

				foreach(SectionFilter scFilter in scFilters)
				{					
					foreach(GroupSummary sectionSummary in SectionSummeries)
					{
                        GroupSummary summary=sectionSummary.Copy();

						summary.Filter.Add(scFilter.Filter);

						string strHeader=scFilter.FilterName;

						if(sectionSummary.ColumnHeading!=null&&sectionSummary.ColumnHeading!=string.Empty)
						{
							strHeader+="\r\n("+sectionSummary.ColumnHeading + ")";
						}

						summary.ColumnHeading = strHeader;   

						this.Summaries.Add(summary);
					}

					
				}
			}	
		}

		#endregion

		#region properties
        [Browsable(true), Category("Sorting Infomation")]
        [EditorAttribute(typeof(Webb.Reports.ExControls.Editors.CustomOrdersEditor), typeof(System.Drawing.Design.UITypeEditor))]	//06-18-2008@Scott
        public UserOrderClS UserDefinedOrders
        {
            get
            {
                if (_UserDefinedOrders == null) _UserDefinedOrders = new UserOrderClS();

                _UserDefinedOrders.RelativeGroupInfo = this;

                return this._UserDefinedOrders;
            }
            set
            {
                this._UserDefinedOrders = value;
            }
        }
		[Browsable(true)]
		[Category("Section Summeries")]
		public GroupSummaryCollection SectionSummeries
		{
			//protected object PropertyName;
			get{
				 if(_SectionSummeries==null)_SectionSummeries=new GroupSummaryCollection();
				return this._SectionSummeries;}
			set{this._SectionSummeries = value;}
		}

        [Browsable(false)]       
        public bool DisplayAsImage
        {
            get { return this._DisplayAsImage; }
            set { this._DisplayAsImage = value; }
        }

       [Category("Filters")]    
        public bool DisregardBlank
        {
            get { return this._DisregardBlank; }
            set { this._DisregardBlank = value; }
        }


		[Category("Grouping Information")]
		public string ShowSymbol	//Modified at 2009-2-1 14:02:19@Scott
		{
			get{return this._ShowSymbol;}
			set{this._ShowSymbol = value;}
		}

		//Properties
        [Browsable(false)]
		public bool OneValuePerPage
		{
			get{return this._OneValuePerPage;}
			set{this._OneValuePerPage = value;}
		}
        [Browsable(false), Category("Grid Information")]
        public bool DisplayAsColumn
        {
            get { return this._DisplayAsColumn; }
            set { this._DisplayAsColumn = value; }
        }
        [Browsable(false), Category("Grid Information")]
        public bool IsSectionOutSide
        {
            get { return this._IsSectionOutSide; }
            set { this._IsSectionOutSide = value; }
        }


		

		[Category("Filters")]
		[EditorAttribute(typeof(Webb.Reports.Editors.DBFilterEditor), typeof(System.Drawing.Design.UITypeEditor))]	//07-02-2008@Scott
		public DBFilter Filter
		{
			get
			{
				if(this._Filter == null) this._Filter = new DBFilter();

				return this._Filter;
			}
			set{this._Filter = value;}
		}
		[Browsable(true),Category("Grouping Borders Information")]
		public bool UseLineBreak
		{
			get{return this._GroupSides;}
			set{this._GroupSides = value;}
		}

        [Browsable(true), Category("Information for ParentRelatedPercent Summary")]
		public bool IsParentGroup
		{
			get{return this._IsRelatedForSubGroup;}
			set{this._IsRelatedForSubGroup = value;}
		}
		[Category("Grouping Information")]
		public bool FollowSummaries
		{
			get{return this._FollowSummaries;}
			set{this._FollowSummaries = value;}
		}
		[Category("ColumnHeading Information")]
		public StringFormatFlags HeadingFormat
		{
			get{return this._TitleFormat;}
			set{this._TitleFormat = value;}
		}
		
		internal GroupResultCollection GroupResults
		{
			get{return this._GroupResults;}
		}
        [Browsable(false), Category("Grid Information")]
		public bool DistinctValues
		{
			set{this._OneValuePerRow = value;}
			get{return this._OneValuePerRow;}
		}

		[Browsable(false)]
		public int ColumnIndex
		{
			get{return this._ColumnIndex;}
			set{this._ColumnIndex = value;}
		}

		[Browsable(true),Category("Style")]
		public BasicStyle Style
		{
			get
			{
				if(this._Style == null) this._Style = new BasicStyle();

				return this._Style;
			}
			set
			{
				this._Style = value;
			}
		}

		[Browsable(true),Category("Style")]	//Modified at 2009-2-11 16:12:47@Scott
		public bool ColorNeedChange
		{
			get{return this._ColorNeedChange;}
			set{this._ColorNeedChange = value;}
		}

		[Browsable(true),Category("Sorting Infomation")]
		public SortingByTypes SortingBy
		{
			get{
                  return this._SortingByType;
               }
			set{

                    if (this._SortingByType != value)
                    {
                        this._SortingByType = value;

                        if (this._SortingByType == SortingByTypes.GroupedValueOrNumber || this._SortingByType == SortingByTypes.UserDefinedOrder)
                        {
                            this._SortingType = SortingTypes.Ascending;
                        }
                    }
               }
		}
		[Browsable(true),Category("Sorting Infomation")]
		public SortingTypes Sorting
		{
			//protected object PropertyName;
			get{return this._SortingType;}
			set{this._SortingType = value;}
		}

		[Browsable(true),Category("Grouping Information")]
		public ClickEvents ClickEvent
		{
			get{return this._ClickEvent;}
			set{this._ClickEvent = value;}
		}
		[Browsable(false)]
		public GroupResult ParentGroupResult
		{
			get{return this._ParentGroupResult;}
			set{this._ParentGroupResult = value;}
		}
		/// <summary>
		/// useless , don't set value
		/// </summary>
		[Browsable(false)]
		public GroupInfo SubGroupInfo
		{
			get{return this._SubGroupInfo;}
			set{this._SubGroupInfo = value;}
		}
		[Browsable(false)]
		public GroupInfoCollection SubGroupInfos
		{
			//03-18-2008@Scott
			//Begin Edit
			get
			{
				if(this._SubGroupInfos == null) this._SubGroupInfos = new GroupInfoCollection();

				return this._SubGroupInfos;
			}
			set
			{
				this._SubGroupInfos = value;

				if(this._SubGroupInfos == null) this._SubGroupInfos = new GroupInfoCollection();
			}
			//End Edit
		}

    
        [Browsable(false)]
        public string Description
        {
            get
            {
                if (this._Description == null)
                {
                    _Description = string.Empty;
                }
                return this._Description;
            }
            set
            {
                this._Description = value;
            }
        }
		

		[Category("ColumnHeading Information")]
		public string ColumnHeading
		{
			//protected object PropertyName;
			get{
				if(_GroupTitle==null)_GroupTitle=string.Empty;
				return this._GroupTitle;}
			set{this._GroupTitle = value;}
		}

        [Browsable(true), Category("Filters")]
        public int TopCount
        {
            //protected object PropertyName;
            get { return this._TopCount; }
            set
            {
                this._TopCount = value < 0 ? 0 : value;
            }
        }
        
		[Browsable(true),Category("Filters")]
        public int SkippedCount
		{
			//protected object PropertyName;
            get { return this._SkippedCount; }
			set
			{
                this._SkippedCount = value < 0 ? 0 : value;
			}
		}

		[Browsable(true),Category("Grouping Total")]
		public bool AddTotal
		{
			//protected object PropertyName;
			get{return this._AddGroupTotal;}
			set{this._AddGroupTotal = value;}
		}

		[Browsable(true),Category("Grouping Total")]
		public string TotalTitle
		{
			//protected object PropertyName;
			get{return this._TotalTitle;}
			set{this._TotalTitle = value;}
		}

       

		[Browsable(false)]
		public GroupSummaryCollection Summaries
		{
			//protected object PropertyName;
			get{
                if (_FollowedSummaries == null) _FollowedSummaries = new GroupSummaryCollection();
				 return this._FollowedSummaries;}
			set{this._FollowedSummaries = value;}
		}

		[Browsable(false)]
		public GroupSummaryCollection TotalSummaries
		{
			//protected object PropertyName;
			get{return this._TotalSummaries;}
			set{this._TotalSummaries = value;}
		}

		//Modified at 2009-1-21 14:07:11@Scott
		[Browsable(false)]
		public ReportScType ReportScType
		{
			get{return this._ReportScType;}
			set{this._ReportScType = value;}
		}

		public override string ToString()
		{
			//return base.ToString ();
			return this._GroupTitle;
		}

		[Browsable(false)]
		public SummaryBandCollection SummaryBands
		{
			get
			{
				if(this._SummaryBands==null) this._SummaryBands = new SummaryBandCollection();
				return _SummaryBands;
			}
			set
			{
				if (this._SummaryBands != value)
					this._SummaryBands = value;
			}
		}
		#endregion
	}
	#endregion

	//03-18-2008@Scott
	#region public class GroupInfoCollection
	[Serializable]
	public class GroupInfoCollection : CollectionBase
	{
		public GroupInfoCollection()
		{
			
		}
	
		public GroupInfoCollection Copy()
		{
			GroupInfoCollection groupInfos = new GroupInfoCollection();
			
			foreach(GroupInfo groupInfo in this)
			{
				groupInfos.Add(groupInfo.Copy());
			}

			return groupInfos;
		}
	
		public GroupInfo this[int i_index]
		{
			get
			{
				return this.InnerList[i_index] as GroupInfo;
			}
			set
			{
				this.InnerList[i_index] = value;
			}
		}

		public int Add(GroupInfo groupInfo)
		{
			return this.InnerList.Add(groupInfo);
		}
		public void Remove(GroupInfo groupInfo)
		{
			this.InnerList.Remove(groupInfo);
		}
		public bool Contains(GroupInfo groupInfo)
		{
			return this.InnerList.Contains(groupInfo);
		}
	}
	#endregion

	#region public class FieldGroupInfo
	/*Descrition:   */
	[Serializable]
	public class FieldGroupInfo : GroupInfo,ISerializable
	{
		override public GroupInfo Copy()
		{
			FieldGroupInfo m_GroupInfo = new FieldGroupInfo();

			m_GroupInfo._AddGroupTotal = this._AddGroupTotal;

			if(this._FollowedSummaries!=null)
			{
				m_GroupInfo._FollowedSummaries = this._FollowedSummaries.CopyStructure();
			
				if(this._AddGroupTotal)
				{
					m_GroupInfo._TotalSummaries = this._FollowedSummaries.CopyStructure();
				}
			}
			//m_GroupInfo._GroupResults = this._AddGroupTotal;
			m_GroupInfo._GroupTitle = this._GroupTitle;
			m_GroupInfo._SortingByType = this._SortingByType;
			m_GroupInfo._SortingType = this._SortingType;
            m_GroupInfo.UserDefinedOrders = new UserOrderClS();
            m_GroupInfo.UserDefinedOrders.RelativeGroupInfo = m_GroupInfo;
            foreach(object objValue in this.UserDefinedOrders.OrderValues)
            {
                m_GroupInfo.UserDefinedOrders.OrderValues.Add(objValue);
            }
			m_GroupInfo._TopCount = this._TopCount;
			m_GroupInfo._FieldName = this._FieldName;
			m_GroupInfo._TotalTitle = this._TotalTitle;
			m_GroupInfo.Style = this.Style.Copy() as BasicStyle;
			m_GroupInfo._ClickEvent = this._ClickEvent;
			m_GroupInfo._FollowSummaries = this._FollowSummaries;
			m_GroupInfo._TitleFormat = this._TitleFormat;
			m_GroupInfo.Filter = this.Filter.Copy();
			m_GroupInfo.ColumnWidth = this.ColumnWidth;

			m_GroupInfo.ColumnIndex=this.ColumnIndex;  //Added this code at 2008-12-24 8:30:51@Simon

			m_GroupInfo.ReportScType = this.ReportScType;	//Modified at 2009-1-21 14:05:45@Scott

			m_GroupInfo.Visible=this.Visible;   //Added this code at 2009-2-1 15:52:23@Simon

			m_GroupInfo.ColorNeedChange = this.ColorNeedChange;	//Modified at 2009-2-11 16:13:38@Scott

			m_GroupInfo._GroupSides=this._GroupSides;  //Add at 2009-2-27 9:31:19@Simon

			m_GroupInfo.FollowsWith=this.FollowsWith;

			m_GroupInfo._IsRelatedForSubGroup=this._IsRelatedForSubGroup;

			m_GroupInfo._ShowSymbol=this._ShowSymbol;

            m_GroupInfo.DisregardBlank = this.DisregardBlank;

            m_GroupInfo._OneValuePerRow = this._OneValuePerRow;

            m_GroupInfo.DateFormat = this.DateFormat;

            m_GroupInfo.SkippedCount = this.SkippedCount;

			if(this._SectionSummeries!=null)
			{
				m_GroupInfo._SectionSummeries = this._SectionSummeries.CopyStructure();
			}

            m_GroupInfo.IsSectionOutSide =false;

            m_GroupInfo._Description = this._Description;

            m_GroupInfo.DisplayAsColumn = this.DisplayAsColumn;

            m_GroupInfo._DisplayAsImage = this._DisplayAsImage;

			return m_GroupInfo;
		}

		public FieldGroupInfo()
	    {    
//			ColumnStyleCollection columnstyles=Webb.Reports.ExControls.Data.ColumnManager.PublicColumnStyles.ColumnStyles;
//			if (columnstyles[_FieldName] != null)
//			{
//				this._ColumnWidth = columnstyles[_FieldName].ColumnWidth;
//				this._Style = columnstyles[_FieldName].Style.Copy() as BasicStyle;
//				this._TitleFormat = columnstyles[_FieldName].TitleFormat;
//				this.ColumnHeading = columnstyles[_FieldName].ColumnHeading;
//			}
		}

		public FieldGroupInfo(string i_GroupByFiled)
		{
			this._FieldName = i_GroupByFiled;

			ColumnStyleCollection columnstyles = Webb.Reports.ExControls.Data.ColumnManager.PublicColumnStyles.ColumnStyles;


			if (columnstyles[_FieldName] != null)
			{
				this._ColumnWidth = columnstyles[_FieldName].ColumnWidth;
				this._TitleFormat = columnstyles[_FieldName].TitleFormat;
				this.ColumnHeading = columnstyles[_FieldName].ColumnHeading;               
				this._Style = columnstyles[_FieldName].Style.Copy() as BasicStyle;
				this._ColorNeedChange=columnstyles[_FieldName].ColorNeedChange;
			}

            CResolveFieldValue cResolvePathIntoImage = new CResolveFieldValue(i_GroupByFiled);

            this.DisplayAsImage = cResolvePathIntoImage.DisplayAsImage;

            if (cResolvePathIntoImage.DisplayAsImage)
            {
                this._ColumnWidth = Math.Max(_ColumnWidth, cResolvePathIntoImage.ColumnWidth);
            }           
		}

        public FieldGroupInfo(string i_GroupByFiled,string strDefaultHeader)
        {
            this._FieldName = i_GroupByFiled;

            this.ColumnHeading = strDefaultHeader;

            ColumnStyleCollection columnstyles = Webb.Reports.ExControls.Data.ColumnManager.PublicColumnStyles.ColumnStyles;
         
            if (columnstyles[_FieldName] != null)
            {
                this._ColumnWidth = columnstyles[_FieldName].ColumnWidth;
                this._TitleFormat = columnstyles[_FieldName].TitleFormat;
                this.ColumnHeading = columnstyles[_FieldName].ColumnHeading;
                this._Style = columnstyles[_FieldName].Style.Copy() as BasicStyle;
                this._ColorNeedChange = columnstyles[_FieldName].ColorNeedChange;
            }

            CResolveFieldValue cResolvePathIntoImage = new CResolveFieldValue(i_GroupByFiled);

            this.DisplayAsImage = cResolvePathIntoImage.DisplayAsImage;

            if (cResolvePathIntoImage.DisplayAsImage)
            {
                this._ColumnWidth = Math.Max(_ColumnWidth, cResolvePathIntoImage.ColumnWidth);
            }           
        }

		public FieldGroupInfo(PageFieldInfo pageGroupInfo):base(pageGroupInfo)
		{
			this._FieldName=pageGroupInfo.GroupByField;
		}

		public override int GetGroupedColumns()
		{
            if (this.Visible)
            {               
                if (this._FollowedSummaries != null)
                    return this._FollowedSummaries.Count + 1;
                else
                    return 1;
            }
            else
            {
                if (this._FollowedSummaries != null)
                    return this._FollowedSummaries.Count;
                else
                    return 0;
            }
		}
        
		public override int GetGroupedRows()
		{
			//return base.GetGroupedRows ();
			int m_Rows = 0;
			if(this._TopCount<=0)
			{
				if(this._GroupResults!=null)
				{
					m_Rows = this._GroupResults.Count;
				}
				else
				{
					m_Rows = 0;
				}
			}
			else
			{
				if(this._GroupResults!=null)
				{
					m_Rows	= /*this._TopCount;*/Math.Min(this._TopCount,this._GroupResults.Count);
				}
				else
				{
					m_Rows = 0;
				}
			}
			//if(this._AddGroupTotal) m_Rows++;
			return m_Rows;
		}
		
		//04-28-2008@Scott
		public bool IsGroupFieldInvalidate(System.Data.DataTable i_Table)
		{
			if(i_Table == null || !i_Table.Columns.Contains(this._FieldName) || this._FieldName == string.Empty)
			{
				return true;
			}
			return false;
		}

		#region Modify "public override ArrayList GetFields(DataTable table, Int32Collection rows)"  //2009-3-5 9:24:33@Simon
		private bool Contains(DataTable table,ArrayList groupValues,object groupElement)
		{
			Type type=this.GetDataType(table);

			if(type==typeof(System.String))
			{
				foreach(object groupValue in groupValues)
				{ 
					string strValue=groupElement as string;

					string strGroupValue=groupValue as string;

					if(strValue==null)
					{
						return groupValues.Contains(groupElement);
					}

					if(strGroupValue==null)
					{
						continue;
					}
					if(strValue.Trim().ToLower()==strGroupValue.Trim().ToLower())
					{
						return true;
					}
				}
				return false;
			}

			return groupValues.Contains(groupElement);

		}
		//2009-3-5 9:32:33@Simon
		public override ArrayList GetFields(DataTable table, Int32Collection rows)
		{
			ArrayList groupValues = new ArrayList();

			if(!IsGroupFieldInvalidate(table))
			{
				foreach(int row in rows)    
				{//Get all values
					object groupElement=table.Rows[row][this._FieldName];
					
					if(!this.Contains(table,groupValues,groupElement))
					{                        
						groupValues.Add(groupElement);
					}
				}
			}

			if(groupValues.Count == 0) groupValues.Add(string.Empty);	//Modified at 2008-11-4 10:26:14@Scott

           if(this._SortingType==SortingTypes.Descending)groupValues.Reverse();

			return groupValues;
		}
		//		//08-04-2008@Scott
		//		public override ArrayList GetFields(DataTable table, Int32Collection rows)
		//		{
		//			ArrayList groupValues = new ArrayList();
		//
		//			if(!IsGroupFieldInvalidate(table))
		//			{
		//				foreach(int row in rows)
		//				{//Get all values
		//					if(!groupValues.Contains(table.Rows[row][this._FieldName]))
		//					{
		//						groupValues.Add(table.Rows[row][this._FieldName]);
		//					}
		//				}
		//			}
		//
		//			if(groupValues.Count == 0) groupValues.Add(string.Empty);	//Modified at 2008-11-4 10:26:14@Scott
		//
		//			groupValues.Reverse();
		//
		//			return groupValues;
		//		}
		#endregion

		//08-04-2008@Scott
		public Type GetDataType(DataTable table)
		{
			Type dataType = typeof(System.DBNull);

			if(IsGroupFieldInvalidate(table))
			{//if grouping field is invalidate, set default datatype
				dataType = typeof(System.String);
			}
			else
			{
				dataType =  table.Columns[this._FieldName].DataType;
			}

			return dataType;
		}

		public SectionGroupInfo IntoSections(DataTable i_Table,Int32Collection i_InnerRows,GroupInfo baseGroupinfo)
		{
			i_InnerRows = this.Filter.GetFilteredRows(i_Table,i_InnerRows);	//07-15-2008@Scott

			bool groupFieldInvalidate = this.IsGroupFieldInvalidate(i_Table);	//04-28-2008@Scott 

			Type m_DataType = this.GetDataType(i_Table);

			SectionGroupInfo groupinfo=new SectionGroupInfo();

			groupinfo.Apply(baseGroupinfo);

            if (baseGroupinfo is SectionGroupInfo)
            {
                groupinfo.Visible=(baseGroupinfo as SectionGroupInfo).Visible;
            }
            else
            {
                groupinfo.Visible = (baseGroupinfo as FieldGroupInfo).Visible;
            }

			groupinfo.SectionFilters=new SectionFilterCollection();		
			
			if(!groupFieldInvalidate)	
			{	
				ArrayList fields= this.GetFields(i_Table,i_InnerRows);
					
				foreach(object m_Result in fields)
				{
                    if (this.DisregardBlank)             //2010-5-4 9:32:33@Simon
                    {
                        if (m_Result == null || m_Result is DBNull || m_Result.ToString().Trim() == string.Empty) continue;
                    }

					DBFilter filter=this.CreateFilter(m_Result,m_DataType);

					SectionFilter section=new SectionFilter(filter);

					section.FilterName=m_Result.ToString();
				
					groupinfo.SectionFilters.Add(section);
				}
			}
			groupinfo.Converted=true;

			return groupinfo;
		}

        private Int32Collection ClearEmptyRows(System.Data.DataTable table, Int32Collection baseRows)
        {
            Int32Collection filteredRows = new Int32Collection();

            foreach (int row in baseRows)
            {
                if (table.Rows.Count <= row) continue;

                 object groupElement = table.Rows[row][this._FieldName];

                 if (groupElement is System.DBNull || groupElement == null || groupElement.ToString().Trim() == string.Empty)
                 {
                 }
                 else
                 {
                     filteredRows.Add(row);
                 }                
            }
            return filteredRows;
        }

		static private ArrayList m_TempGroupResults = null;

		public override void CalculateGroupResult(System.Data.DataTable i_Table,Webb.Collections.Int32Collection i_OuterRows,Webb.Collections.Int32Collection i_InnerRows,GroupInfo i_GroupInfo)
		{
			i_GroupInfo.UpdateSectionSummaries();	//Modified at 2009-1-21 15:08:49@Scott

			i_GroupInfo.Filter=AdvFilterConvertor.GetAdvFilter(DataProvider.VideoPlayBackManager.AdvReportFilters,i_GroupInfo.Filter);    //2009-4-29 11:37:37@Simon Add UpdateAdvFilter

			i_InnerRows = i_GroupInfo.Filter.GetFilteredRows(i_Table,i_InnerRows);	//07-15-2008@Scott            

			bool groupFieldInvalidate = this.IsGroupFieldInvalidate(i_Table);	//04-28-2008@Scott

            if (this.DisregardBlank && !groupFieldInvalidate)             //2010-5-4 9:32:33@Simon
            {
                i_InnerRows=ClearEmptyRows(i_Table, i_InnerRows);
            }

			Type m_DataType = this.GetDataType(i_Table);

			//1. Create groups.
			m_TempGroupResults = this.GetFields(i_Table,i_InnerRows);

			//2. Calculate grouped result.
			if(this._GroupResults ==null) this._GroupResults = new GroupResultCollection();
			this._GroupResults.Clear();
        	
			if(groupFieldInvalidate)	//08-07-2008@Scott
			{//if grouping field is invalidate, make a empty group
				object m_Result = m_TempGroupResults[0];
				GroupResult m_GroupResult = new GroupResult();
				m_GroupResult.GroupValue = m_Result;
				this.CreateFilter(m_GroupResult,m_Result,m_DataType);
				m_GroupResult.RowIndicators = new Int32Collection();
				i_InnerRows.CopyTo(m_GroupResult.RowIndicators);
				m_GroupResult.ClickEvent = i_GroupInfo.ClickEvent;

				m_GroupResult.ParentGroupInfo=this;  //Add at 2009-2-19 14:24:05@Simon

				this._GroupResults.Add(m_GroupResult);
			}
			else
			{
				GroupResultCollection m_Results=new GroupResultCollection();

				foreach(object m_Result in m_TempGroupResults)
				{
					GroupResult m_GroupResult = new GroupResult();
					
					this.CreateFilter(m_GroupResult,m_Result,m_DataType);					

					m_GroupResult.GroupValue = m_Result;					

					m_GroupResult.ParentGroupInfo=this;  //Add at 2009-2-19 14:23:47@Simon

					m_GroupResult.CalculateRowIndicators(i_Table,i_InnerRows);

					#region Modify codes at 2009-4-8 14:20:23@Simon
					if(this.ShowSymbol!=string.Empty&&m_GroupResult.RowIndicators.Count>0)
					{
						string strValue=m_Result as string;

						if(strValue==null||strValue==string.Empty)
						{
							m_GroupResult.GroupValue=this.ShowSymbol;
						}
					}
					#endregion        //End Modify

					m_GroupResult.ClickEvent = i_GroupInfo.ClickEvent;

					m_Results.Add(m_GroupResult);
				}

				m_Results.Sort(this.Sorting,this.SortingBy,this.UserDefinedOrders); // 09-26-2011 Scott

				if(this.TopCount>0)
				{
                    int minTopCount = Math.Min(this.SkippedCount + this.TopCount, m_Results.Count);

                    for (int i = this.SkippedCount; i < minTopCount; i++)
					{
						this._GroupResults.Add(m_Results[i]);
					}
				}
				else
				{
                    for (int i = this.SkippedCount; i < m_Results.Count; i++)
                    {
                        this._GroupResults.Add(m_Results[i]);
                    }					
				}
			}
			
			//3. Calculate the grouped result's summary value.		
			foreach(GroupResult m_GroupResult in this._GroupResults)
			{
				m_GroupResult.ParentGroupInfo = this;
				if(this._FollowedSummaries!=null)	//12-28-2007@Scott
				{
					m_GroupResult.Summaries = this._FollowedSummaries.CopyStructure();
					m_GroupResult.CalculateSummaryResult(i_Table,i_OuterRows,i_InnerRows);
				}
			}

			//4. Calculate the nested grouped value.
			//03-18-2008@Scott
			//Begin Edit
			foreach(GroupResult groupResult in this._GroupResults)
			{
				groupResult.SubGroupInfos.Clear();

				if(groupResult.RowIndicators == null) continue;	//if group result is empty, continue

				foreach(GroupInfo groupInfo in i_GroupInfo.SubGroupInfos)
				{//calculate sub group infos
					GroupInfo resultGroupInfo = groupInfo.Copy();

                    resultGroupInfo.IsSectionOutSide = false;
                    groupInfo.IsSectionOutSide = false;

					groupResult.SubGroupInfos.Add(resultGroupInfo);

					resultGroupInfo.ParentGroupResult = groupResult;

					resultGroupInfo.CalculateGroupResult(i_Table,i_OuterRows,groupResult.RowIndicators,groupInfo);
				}
			}
			//End Edit

			//5. calcualte total summary
			if(this._AddGroupTotal)
			{
				this._TotalSummaries = new GroupSummaryCollection();

				this.GetTotalSummaries(this._TotalSummaries,this);	//04-24-2008@Scott	

				Int32Collection totalIndicators = this.GetTotalIndicators(this);

				foreach(GroupSummary m_Summary in this._TotalSummaries)
				{
					m_Summary.ParentGroupInfo = this;

					//05-08-2008@Scott
					switch(m_Summary.SummaryType)
					{
						case SummaryTypes.Percent:
							m_Summary.CalculateResult(i_Table,i_OuterRows,totalIndicators/*08-27-2008@Scott*/,totalIndicators);
							break;
						case SummaryTypes.FreqAndPercent:
						case SummaryTypes.FreqAndRelatedPercent:
							m_Summary.Value = 0;
							break;
						default:
							m_Summary.CalculateResult(i_Table,totalIndicators,totalIndicators/*08-27-2008@Scott*/,totalIndicators);
							break;
					}
				}
			}
		}    

        #region add this code for compare   //Added this code at 2008-12-19 8:31:58@Simon

        //public override void CalculateGroupResult(DataTable i_Table, Int32Collection i_OuterRows, Int32Collection i_FilterRows, Int32Collection i_InnerRows, GroupInfo i_GroupInfo)
        //{
        //    i_FilterRows = i_GroupInfo.Filter.GetFilteredRows(i_Table,i_FilterRows);	

        //    i_InnerRows= i_GroupInfo.Filter.GetFilteredRows(i_Table,i_InnerRows);

        //    bool groupFieldInvalidate = this.IsGroupFieldInvalidate(i_Table);

        //    if (this.DisregardBlank && !groupFieldInvalidate)             //2010-5-4 9:32:33@Simon
        //    {
        //        i_InnerRows = ClearEmptyRows(i_Table, i_InnerRows);
        //    }

        //    Type m_DataType = this.GetDataType(i_Table);

        //    ////1. Create groups.			
        //    m_TempGroupResults = this.GetFields(i_Table, i_FilterRows);
			

        //    //2. Calculate grouped result.
        //    if(this._GroupResults ==null) this._GroupResults = new GroupResultCollection();
        //    this._GroupResults.Clear();
			
        //    if(groupFieldInvalidate)	
        //    {//if grouping field is invalidate, make a empty group

        //        #region  Valid Field

        //        object m_Result = m_TempGroupResults[0];
        //        GroupResult m_GroupResult = new GroupResult();
        //        m_GroupResult.GroupValue = m_Result;
        //        this.CreateFilter(m_GroupResult,m_Result,m_DataType);

        //        m_GroupResult.RowIndicators = new Int32Collection();

        //        m_GroupResult.ParentGroupInfo=this;  //Add at 2009-2-19 14:23:47@Simon

        //        i_InnerRows.CopyTo(m_GroupResult.RowIndicators);

        //        m_GroupResult.ClickEvent = i_GroupInfo.ClickEvent;
        //        this._GroupResults.Add(m_GroupResult);
        //        #endregion
        //    }
        //    else
        //    {
        //        int limitTopCount=0;

        //        if (this.AddTotal)
        //        {
        //            #region Add Total Results
        //            GroupResult m_GroupResult = new GroupResult();

        //            m_GroupResult.Filter= new DBFilter();

        //            i_InnerRows.CopyTo(m_GroupResult.RowIndicators);

        //            m_GroupResult.GroupValue = this.TotalTitle;

        //            m_GroupResult.ParentGroupInfo = this;  //Add at 2009-2-19 14:23:47@Simon

        //            m_GroupResult.ClickEvent = i_GroupInfo.ClickEvent;

        //            this._GroupResults.Add(m_GroupResult);
        //            #endregion
        //        }              

        //        foreach(object m_Result in m_TempGroupResults)
        //        {	
        //            if(this.TopCount>0&&limitTopCount>=this.TopCount)break;

        //            GroupResult m_GroupResult = new GroupResult();

        //            m_GroupResult.GroupValue = m_Result;

        //            this.CreateFilter(m_GroupResult,m_Result,m_DataType);

        //            m_GroupResult.CalculateRowIndicators(i_Table,i_InnerRows);

        //            m_GroupResult.ParentGroupInfo=this;  //Add at 2009-2-19 14:23:47@Simon

        //            m_GroupResult.ClickEvent = i_GroupInfo.ClickEvent;
					
        //            this._GroupResults.Add(m_GroupResult);

        //            limitTopCount++;
        //        }	
				
        //    }

        //    //3.sort results		    
			
        //    //4. Calculate the grouped result's summary value.		
        //    foreach(GroupResult m_GroupResult in this._GroupResults)
        //    {
        //        m_GroupResult.ParentGroupInfo = this;
        //        if(this._FollowedSummaries!=null)	
        //        {
        //            m_GroupResult.Summaries = this._FollowedSummaries.CopyStructure();
        //            m_GroupResult.CalculateSummaryResult(i_Table,i_OuterRows,i_InnerRows);
        //        }
        //    }

        //    //5. Calculate the nested grouped value.

        //    //Begin Edit
        //    foreach(GroupResult groupResult in this._GroupResults)
        //    {
        //        groupResult.SubGroupInfos.Clear();

        //        if(groupResult.RowIndicators == null) continue;	//if group result is empty, continue

        //        foreach(GroupInfo groupInfo in i_GroupInfo.SubGroupInfos)
        //        {//calculate sub group infos
        //            GroupInfo resultGroupInfo = groupInfo.Copy();

        //            resultGroupInfo.IsSectionOutSide = false;                  

        //            groupResult.SubGroupInfos.Add(resultGroupInfo);

        //            resultGroupInfo.ParentGroupResult = groupResult;

        //            resultGroupInfo.CalculateGroupResult(i_Table,i_OuterRows,i_FilterRows,groupResult.RowIndicators,groupInfo);
        //        }
        //    }
        //    //End Edit		
        //}
        public override void CalculateGroupResult(DataTable i_Table, Int32Collection i_OuterRows, Int32Collection i_FilterRows, Int32Collection i_InnerRows, GroupInfo i_GroupInfo)
        {
            i_FilterRows = i_GroupInfo.Filter.GetFilteredRows(i_Table, i_FilterRows);

            i_InnerRows = i_GroupInfo.Filter.GetFilteredRows(i_Table, i_InnerRows);

            bool groupFieldInvalidate = this.IsGroupFieldInvalidate(i_Table);          

            Type m_DataType = this.GetDataType(i_Table);
        
            //1. Calculate grouped result.
            if (this._GroupResults == null) this._GroupResults = new GroupResultCollection();

            this._GroupResults.Clear();           

            if (groupFieldInvalidate)
            {//if grouping field is invalidate, make a empty group

                #region  Valid Field

                object m_Result = string.Empty;
                GroupResult m_GroupResult = new GroupResult();
                m_GroupResult.GroupValue = m_Result;
                this.CreateFilter(m_GroupResult, m_Result, m_DataType);

                m_GroupResult.RowIndicators = new Int32Collection();

                m_GroupResult.ParentGroupInfo = this;  //Add at 2009-2-19 14:23:47@Simon

                i_InnerRows.CopyTo(m_GroupResult.RowIndicators);

                m_GroupResult.ClickEvent = i_GroupInfo.ClickEvent;
                this._GroupResults.Add(m_GroupResult);
                #endregion
            }
            else
            {
                #region Calculate Group Result

                foreach (int row in i_FilterRows)
                {
                    object groupElement = i_Table.Rows[row][this._FieldName];

                    if (this.DisregardBlank)             //2010-5-4 9:32:33@Simon
                    {
                        if (groupElement == null || groupElement is DBNull || groupElement.ToString().Trim() == string.Empty) continue;
                    }

                     GroupResult m_GroupResult=this._GroupResults[groupElement];

                    if(m_GroupResult==null)
                    {
                        m_GroupResult=new GroupResult();

                        this.CreateFilter(m_GroupResult, groupElement, m_DataType);

                        m_GroupResult.GroupValue = groupElement;

                        m_GroupResult.ParentGroupInfo = this;  //Add at 2009-2-19 14:23:47@Simon

                        m_GroupResult.ClickEvent = i_GroupInfo.ClickEvent;

                        m_GroupResult.RowIndicators=new Int32Collection();

                        this._GroupResults.Add(m_GroupResult);
                    }

                    if(m_GroupResult.RowIndicators==null)m_GroupResult.RowIndicators=new Int32Collection();

                    if(!i_InnerRows.Contains(row))continue;

                    m_GroupResult.RowIndicators.Add(row);                  
                }
                 #endregion

                if (this.TopCount > 0 && this.TopCount < this._GroupResults.Count)
                {
                    for(int j=this._GroupResults.Count-1;j>=this.TopCount;j--)
                    {
                        this._GroupResults.RemoveAt(j);
                    }
                }
                    
                #region Add Total Results

                if (this.AddTotal)
                {                 
                    GroupResult m_GroupResult = new GroupResult();

                    m_GroupResult.Filter = new DBFilter();

                    i_InnerRows.CopyTo(m_GroupResult.RowIndicators);

                    m_GroupResult.GroupValue = this.TotalTitle;

                    m_GroupResult.ParentGroupInfo = this;  //Add at 2009-2-19 14:23:47@Simon

                    m_GroupResult.ClickEvent = i_GroupInfo.ClickEvent;

                    if (this._GroupResults.Count > 0)
                    {
                        this._GroupResults.InserAt(0, m_GroupResult);
                    }
                    else
                    {
                        this._GroupResults.Add(m_GroupResult);
                    }
                }

                #endregion


            }

            //2.sort results		    

            //3. Calculate the grouped result's summary value.		
            foreach (GroupResult m_GroupResult in this._GroupResults)
            {
                m_GroupResult.ParentGroupInfo = this;
                if (this._FollowedSummaries != null)
                {
                    m_GroupResult.Summaries = this._FollowedSummaries.CopyStructure();
                    m_GroupResult.CalculateSummaryResult(i_Table, i_OuterRows, i_InnerRows);
                }
            }

            //4. Calculate the nested grouped value.

            //Begin Edit
            foreach (GroupResult groupResult in this._GroupResults)
            {
                groupResult.SubGroupInfos.Clear();

                if (groupResult.RowIndicators == null) continue;	//if group result is empty, continue

                foreach (GroupInfo groupInfo in i_GroupInfo.SubGroupInfos)
                {//calculate sub group infos
                    GroupInfo resultGroupInfo = groupInfo.Copy();

                    resultGroupInfo.IsSectionOutSide = false;

                    groupResult.SubGroupInfos.Add(resultGroupInfo);

                    resultGroupInfo.ParentGroupResult = groupResult;

                    resultGroupInfo.CalculateGroupResult(i_Table, i_OuterRows, i_FilterRows, groupResult.RowIndicators, groupInfo);
                }
            }
            //End Edit		
        }

		#endregion	

		public void GetTotalSummaries(GroupSummaryCollection totalSummaries, GroupInfo groupInfo)
		{
			if(groupInfo == null) return;

			if(groupInfo.Summaries != null)
			{
				foreach(GroupSummary summary in groupInfo.Summaries)
				{
					GroupSummary dumySummary = summary.Copy();
					//Modified at 2009-1-9 14:53:37@Scott
					//Start
					if(groupInfo.Filter.Count != 0)
					{
						dumySummary.Filter.Add(groupInfo.Filter);
					}

					totalSummaries.Add(dumySummary);	//05-09-2008@Scott
					//End
				}
			}

			if(groupInfo.GroupResults != null && groupInfo.GroupResults.Count > 0)
			{
				GroupResult result = groupInfo.GroupResults[0];

				foreach(GroupInfo subGroupInfo in result.SubGroupInfos)
				{
					this.GetTotalSummaries(totalSummaries,subGroupInfo);
				}
			}
		}

		//Get filtered indicators
		public Int32Collection GetTotalIndicators(GroupInfo groupInfo)
		{
			Int32Collection totalIndicators = new Int32Collection();

			if(groupInfo.GroupResults == null) return totalIndicators;

            groupInfo.GroupResults.Sort(groupInfo.Sorting, groupInfo.SortingBy, groupInfo.UserDefinedOrders);	//Modified at 2009-2-1 10:26:41@Scott

			int nTopCount = 0;

			if(groupInfo.TopCount > 0)
			{
				nTopCount = Math.Min(groupInfo.TopCount,groupInfo.GroupResults.Count);
			}
			else
			{
				nTopCount = groupInfo.GroupResults.Count;
			}

			for(int i = 0; i < nTopCount; i++)
			{
				GroupResult result = groupInfo.GroupResults[i];

				if(result.RowIndicators == null) continue;

				foreach(int row in result.RowIndicators)
				{
					if(!totalIndicators.Contains(row)) totalIndicators.Add(row);
				}
			}

			return totalIndicators;
		}

		/// <summary>
		/// Create filter for group result
		/// </summary>
		/// <param name="i_GroupResult">group result</param>
		/// <param name="i_Result">filtered value</param>
		/// <param name="i_DataType">data type</param>
		private void CreateFilter(GroupResult i_GroupResult,object i_Result,Type i_DataType)
		{
			DBCondition m_Condition = new DBCondition();
			m_Condition.ColumnName = this._FieldName;
			if(i_DataType==typeof(System.Int32))
			{
				m_Condition.FilterType = FilterTypes.NumEqual;
			}
			else
			{
				m_Condition.FilterType = FilterTypes.StrEqual;
			}
			m_Condition.Value = i_Result;
			m_Condition.FollowedOperand = FilterOperands.And;
			i_GroupResult.Filter = new DBFilter();
			//Don't need the parent's filter, because the row indicators already filtered.
			//			if(this._ParentGroupResult!=null)
			//			{
			//				foreach(DBCondition m_SingleCondition in this._ParentGroupResult.Filter)
			//				{
			//					i_GroupResult.Filter.Add(m_SingleCondition);
			//				}
			//			}
			i_GroupResult.Filter.Add(m_Condition);
		}
		public DBFilter CreateFilter(object i_Result,Type i_DataType)
		{   
			DBFilter filter = new DBFilter();

			DBCondition m_Condition = new DBCondition();
			m_Condition.ColumnName = this._FieldName;
			if(i_DataType==typeof(System.Int32))
			{
				m_Condition.FilterType = FilterTypes.NumEqual;
			}
			else
			{
				m_Condition.FilterType = FilterTypes.StrEqual;
			}
			m_Condition.Value = i_Result;

			m_Condition.FollowedOperand = FilterOperands.And;

			filter.Add(m_Condition);		
	
			return filter;
		}
        public void SetFieldAndTitle(string strField, string strHeader)
        { 
			this._FieldName = strField;         

			//08-20-2008@Simon
			ColumnStyleCollection columnstyles = Webb.Reports.ExControls.Data.ColumnManager.PublicColumnStyles.ColumnStyles;
			if (columnstyles[_FieldName] != null)
			{
				this._ColumnWidth = columnstyles[_FieldName].ColumnWidth;
				this._Style = columnstyles[_FieldName].Style.Copy() as BasicStyle;
				this._TitleFormat = columnstyles[_FieldName].TitleFormat;
                this.ColumnHeading = columnstyles[_FieldName].ColumnHeading;
				this._ColorNeedChange=columnstyles[_FieldName].ColorNeedChange;
			}

            CResolveFieldValue cResolvePathIntoImage = new CResolveFieldValue(_FieldName);

            this.DisplayAsImage = cResolvePathIntoImage.DisplayAsImage;

            if (cResolvePathIntoImage.DisplayAsImage)
            {
                this._ColumnWidth = Math.Max(_ColumnWidth, cResolvePathIntoImage.ColumnWidth);
            }           

            this.ColumnHeading = strHeader;
        }

        public void SetFieldAndTitleSimple(string strField, string strHeader)
        {
            this._FieldName = strField;
           
            this._GroupTitle = strHeader;

            CResolveFieldValue cResolvePathIntoImage = new CResolveFieldValue(_FieldName);

            this.DisplayAsImage = cResolvePathIntoImage.DisplayAsImage;

            if (cResolvePathIntoImage.DisplayAsImage)
            {
                this._ColumnWidth = Math.Max(_ColumnWidth, cResolvePathIntoImage.ColumnWidth);
            }           
        }


        public override void GetAllUsedFields(ref ArrayList _UsedFields)
        {
            if(!_UsedFields.Contains(this.GroupByField))
            {
                _UsedFields.Add(this.GroupByField);
            }
            this.Filter.GetAllUsedFields(ref _UsedFields);
           
            if (this.Summaries.Count > 0)
            {
                foreach (GroupSummary groupSummary in this.Summaries)
                {
                    if (!_UsedFields.Contains(groupSummary.Field)) _UsedFields.Add(groupSummary.Field);

                    groupSummary.Filter.GetAllUsedFields(ref _UsedFields);

                    groupSummary.DenominatorFilter.GetAllUsedFields(ref _UsedFields);
                }
            }

            foreach (GroupInfo subGroupInfo in this.SubGroupInfos)
            {
                 subGroupInfo.GetAllUsedFields(ref _UsedFields);
            }
        }


		protected string _FieldName=string.Empty;

        //[TypeConverter(typeof(PublicDBFieldConverter))]
        [Editor(typeof(Webb.Reports.Editors.FieldSelectEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[Category("Grouping Information")]
		public string GroupByField
		{
			get{
                 if(_FieldName==null)_FieldName=string.Empty;
                 return this._FieldName;}
			set
			{  
				//When field changed , load default style setting   //08-14-2008@Scott
				//todo
				if (this._FieldName != value)
				{
					this._FieldName = value;
                 
                     this._GroupTitle = _FieldName;

                     this.UserDefinedOrders = new UserOrderClS();

					//08-20-2008@Simon
					ColumnStyleCollection columnstyles = Webb.Reports.ExControls.Data.ColumnManager.PublicColumnStyles.ColumnStyles;
					if (columnstyles[_FieldName] != null)
					{
						this._ColumnWidth = columnstyles[_FieldName].ColumnWidth;
						this._Style = columnstyles[_FieldName].Style.Copy() as BasicStyle;
						this._TitleFormat = columnstyles[_FieldName].TitleFormat;
						this.ColumnHeading = columnstyles[_FieldName].ColumnHeading;
						this._ColorNeedChange=columnstyles[_FieldName].ColorNeedChange;
					}

                    CResolveFieldValue cResolvePathIntoImage = new CResolveFieldValue(_FieldName);

                    this.DisplayAsImage = cResolvePathIntoImage.DisplayAsImage;

                    if (cResolvePathIntoImage.DisplayAsImage)
                    {
                        this._ColumnWidth = Math.Max(_ColumnWidth, cResolvePathIntoImage.ColumnWidth);
                    }           
				}
				//if(this.GroupFieldChanged !=null) this.GroupFieldChanged(this,null);
			}
		}

		[Category("Grouping Information")]
		public string FollowsWith
		{
			get{
                if (_FollowsWith == null) _FollowsWith = string.Empty;
                return this._FollowsWith;}
			set{this._FollowsWith=value;}
		}
		[Category("Style")]
		public int ColumnWidth
		{
			get { return this._ColumnWidth <0 ? BasicStyle.ConstValue.CellWidth : this._ColumnWidth; }
			set { this._ColumnWidth = value < 0 ? BasicStyle.ConstValue.CellWidth : value; }
		}
		private bool _Visible=true;
		[Category("Grouping Information")]
		public bool Visible
		{
			get{return this._Visible;}
			set
			{
                if (!value)
                {
                    this.ColumnWidth = 0;
                }
                else
                {
                    if (this.ColumnWidth == 0)
                    {
                        this.ColumnWidth = BasicStyle.ConstValue.CellWidth;
                    }
                }
				this._Visible=value;
				
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

		public override string ToString()
		{
			//return base.ToString ();
			
			return string.Format("{0}({1})",this._GroupTitle,this._FieldName);
		}	

		#region Serialization By Macro 2008-12-11 15:46:20
		override public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			base.GetObjectData(info,context);

			info.AddValue("_FieldName",_FieldName);

			info.AddValue("_Visible",_Visible);

            info.AddValue("_DateFormat", _DateFormat);
		}

		public FieldGroupInfo(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
		{
			try
			{
				_FieldName = info.GetString("_FieldName");
			}
			catch
			{
				_FieldName = string.Empty;
			}
			try
			{
				_Visible = info.GetBoolean("_Visible");
			}
			catch
			{
				_Visible = true;
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
	#endregion

	//12-10-2008@Scott
	#region public class CombinedFieldsGroupInfo : FieldGroupInfo,ISerializable
	[Serializable]
	public class CombinedFieldsGroupInfo : FieldGroupInfo,ISerializable
	{
		//Ctor
		public CombinedFieldsGroupInfo() : base()
		{
			this._GroupByFields = new StringCollection();

        }


        #region Old Properties
        //Members
		protected StringCollection _GroupByFields;   
	
        [Browsable(false),Category("FieldsGroup Setting")]
        [EditorAttribute(typeof(Webb.Reports.ExControls.Editors.FieldsGroupEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public StringCollection GroupByFields
		{
			get
			{
				if(this._GroupByFields == null) this._GroupByFields = new StringCollection();

				return this._GroupByFields;
			}
			set{
                    this._GroupByFields = value;                   
                    
               }
		}    
     
        protected CombinedOperation _CombinedOperation = CombinedOperation.StrConcat;
        [Browsable(false),Category("FieldsGroup Setting")]
        public CombinedOperation ComputeOperation
        {
            get
            {
                return this._CombinedOperation;
            }
            set
            {
                if (this._CombinedOperation != value)
                { 
                    this._CombinedOperation = value;                   
                }                
            }
        }

		[Browsable(false)]
		[TypeConverter(typeof(PublicDBFieldConverter))]
		public new string GroupByField
		{
			get{return this._FieldName;}
			set
			{ 
				if (this._FieldName != value)
				{
					this._FieldName = value;					
                    
				}				
			}
		}

		protected FieldsGroupStyle _FieldsGroupStyle=FieldsGroupStyle.CombinedValues;  //2009-3-4 15:05:21@Simon       

        [Browsable(false)]
        public FieldsGroupStyle FieldsGroupStyle
		{
			get{return this._FieldsGroupStyle;}

			set{this._FieldsGroupStyle=value;}
		}

		protected  string _ValueSpliter =" ";

        [Browsable(false),Category("FieldsGroup Setting")]
        public string ValueSpliter
		{
			get{
                if (_ValueSpliter == null) _ValueSpliter = " ";
                   return this._ValueSpliter;
                }

            set
            {
                if (this._ValueSpliter != value)
                {  
                    this._ValueSpliter = value;                   
                }
            }
        }
        #endregion

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

        protected CalculateColumnInfo _CalculateInfoSetting = new CalculateColumnInfo();
        [Category("Calculateing Formula")]
        [EditorAttribute(typeof(Webb.Reports.ExControls.Editors.CalculateColumnEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public CalculateColumnInfo CalculatingFormula
        {
            get
            {
               if(_CalculateInfoSetting==null)_CalculateInfoSetting=new CalculateColumnInfo();

               return _CalculateInfoSetting;
            }
            set
            {
                _CalculateInfoSetting = value;

                this._GroupTitle = _CalculateInfoSetting.GetDefaultHeader();
            }
        }

        public override void GetAllUsedFields(ref ArrayList _UsedFields)
        {
            foreach (string strGroupByField in this.GroupByFields)
            {
                if (!_UsedFields.Contains(strGroupByField))
                {
                    _UsedFields.Add(strGroupByField);
                }
            }            

            this.Filter.GetAllUsedFields(ref _UsedFields);

            if (this.Summaries.Count > 0)
            {
                foreach (GroupSummary groupSummary in this.Summaries)
                {
                    if (!_UsedFields.Contains(groupSummary.Field)) _UsedFields.Add(this.GroupByField);

                    groupSummary.Filter.GetAllUsedFields(ref _UsedFields);
                }
            }

            foreach (GroupInfo subGroupInfo in this.SubGroupInfos)
            {
                subGroupInfo.GetAllUsedFields(ref _UsedFields);
            }
        }
     


		//Functions
		public override void CalculateGroupResult(DataTable i_Table, Int32Collection i_OuterRows, Int32Collection i_InnerRows, GroupInfo i_GroupInfo)
		{
            if (this.CalculatingFormula.Count <= 0) return;

            string strFieldName = this.CalculatingFormula.GetGropByField();

            this.AddFieldsColumn(i_Table, strFieldName);

			this._FieldName = strFieldName;

			base.CalculateGroupResult(i_Table, i_OuterRows, i_InnerRows, i_GroupInfo);			
		
        }   

    
        virtual public void AddFieldsColumn(DataTable i_Table,string strFieldName)
		{
             if (strFieldName == string.Empty || i_Table == null || i_Table.Columns.Contains(strFieldName)) return;

             DataColumn dc=new DataColumn(strFieldName,typeof(string));

             dc.Caption = "{EXTENDCOLUMNS}";

             i_Table.Columns.Add(dc);

            // CombinedValueDealer comBinedValueDealer = new CombinedValueDealer(this._CombinedOperation, this.ValueSpliter, this.GroupByFields);

            //foreach (DataRow dataRow in i_Table.Rows)
            //{
            //    dataRow[strFieldName] = comBinedValueDealer.GetResultValue(dataRow);
            //}		

             foreach (DataRow dataRow in i_Table.Rows)
             {
                 dataRow[strFieldName] = this.CalculatingFormula.GetResultValue(dataRow);
             }			
           
		}
		

		public override GroupInfo Copy()
		{
			CombinedFieldsGroupInfo m_GroupInfo = new CombinedFieldsGroupInfo();
            
            m_GroupInfo._AddGroupTotal = this._AddGroupTotal;

            if (this._FollowedSummaries != null)
            {
                m_GroupInfo._FollowedSummaries = this._FollowedSummaries.CopyStructure();

                if (this._AddGroupTotal)
                {
                    m_GroupInfo._TotalSummaries = this._FollowedSummaries.CopyStructure();
                }
            }
            //m_GroupInfo._GroupResults = this._AddGroupTotal;
            m_GroupInfo._GroupTitle = this._GroupTitle;
            m_GroupInfo._SortingByType = this._SortingByType;
            m_GroupInfo._SortingType = this._SortingType;
            m_GroupInfo.UserDefinedOrders = new UserOrderClS();
            m_GroupInfo.UserDefinedOrders.RelativeGroupInfo = m_GroupInfo;

            foreach (object objValue in this.UserDefinedOrders.OrderValues)
            {
                m_GroupInfo.UserDefinedOrders.OrderValues.Add(objValue);
            }
            m_GroupInfo._TopCount = this._TopCount;
            m_GroupInfo._FieldName = this._FieldName;
            m_GroupInfo._TotalTitle = this._TotalTitle;
            m_GroupInfo.Style = this.Style.Copy() as BasicStyle;
            m_GroupInfo._ClickEvent = this._ClickEvent;
            m_GroupInfo._FollowSummaries = this._FollowSummaries;
            m_GroupInfo._TitleFormat = this._TitleFormat;
            m_GroupInfo.Filter = this.Filter.Copy();
            m_GroupInfo.ColumnWidth = this.ColumnWidth;

            m_GroupInfo.ColumnIndex = this.ColumnIndex;  //Added this code at 2008-12-24 8:30:51@Simon

            m_GroupInfo.ReportScType = this.ReportScType;	//Modified at 2009-1-21 14:05:45@Scott

            m_GroupInfo.Visible = this.Visible;   //Added this code at 2009-2-1 15:52:23@Simon

            m_GroupInfo.ColorNeedChange = this.ColorNeedChange;	//Modified at 2009-2-11 16:13:38@Scott

            m_GroupInfo._GroupSides = this._GroupSides;  //Add at 2009-2-27 9:31:19@Simon

            m_GroupInfo.SkippedCount = this.SkippedCount;

            m_GroupInfo.FollowsWith = this.FollowsWith;

            m_GroupInfo._IsRelatedForSubGroup = this._IsRelatedForSubGroup;

            m_GroupInfo._ShowSymbol = this._ShowSymbol;

            m_GroupInfo.DisregardBlank = this.DisregardBlank;

            m_GroupInfo._OneValuePerRow = this._OneValuePerRow;

            m_GroupInfo.DateFormat = this.DateFormat;

            if (this._SectionSummeries != null)
            {
                m_GroupInfo._SectionSummeries = this._SectionSummeries.CopyStructure();
            }

            m_GroupInfo.IsSectionOutSide = false;

            m_GroupInfo._Description = this._Description;

            m_GroupInfo.DisplayAsColumn = this.DisplayAsColumn;

            foreach(string strGroupByField in this.GroupByFields)
            {
                m_GroupInfo.GroupByFields.Add(strGroupByField);
            }
			m_GroupInfo.FieldsGroupStyle=this.FieldsGroupStyle; //2009-3-4 15:05:13@Simon

            m_GroupInfo.ValueSpliter=this.ValueSpliter;

            m_GroupInfo._CombinedOperation = this._CombinedOperation;

            m_GroupInfo.DisplayAsImage = false;

            m_GroupInfo._CalculateInfoSetting = this.CalculatingFormula.Copy();

			return m_GroupInfo;
		}

        #region Serialization By Simon's Macro 2011-3-1 11:30:41
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info,context);

			info.AddValue("_GroupByFields",_GroupByFields,typeof(System.Collections.Specialized.StringCollection));
			info.AddValue("_FieldsGroupStyle",_FieldsGroupStyle,typeof(Webb.Reports.FieldsGroupStyle));
			info.AddValue("_ValueSpliter",_ValueSpliter);
            info.AddValue("_CombinedOperation", _CombinedOperation, typeof(Webb.Reports.ExControls.Data.CombinedOperation));
            info.AddValue("_CalculateInfoSetting", _CalculateInfoSetting, typeof(Webb.Reports.ExControls.Data.CalculateColumnInfo));
            info.AddValue("_RelatedCalculateFormulaDealer", _RelatedCalculateFormulaDealer, typeof(Webb.Reports.ExControls.Data.CalculateFormulaDealer));
        
        }

        public CombinedFieldsGroupInfo(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
			try
			{
				_GroupByFields=(System.Collections.Specialized.StringCollection)info.GetValue("_GroupByFields",typeof(System.Collections.Specialized.StringCollection));
			}
			catch
			{
                _GroupByFields=new StringCollection();
			}

			try
			{
				_FieldsGroupStyle=(Webb.Reports.FieldsGroupStyle)info.GetValue("_FieldsGroupStyle",typeof(Webb.Reports.FieldsGroupStyle));
			}
			catch
			{
				_FieldsGroupStyle=FieldsGroupStyle.CombinedValues;
			}
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
                _CalculateInfoSetting = new CalculateColumnInfo(this._GroupByFields,this._ValueSpliter,this.ComputeOperation);
            }
            try
            {
                _RelatedCalculateFormulaDealer = (Webb.Reports.ExControls.Data.CalculateFormulaDealer)info.GetValue("_CalculateInfoSetting", typeof(Webb.Reports.ExControls.Data.CalculateFormulaDealer));
            }
            catch
            {
                _RelatedCalculateFormulaDealer = new CalculateFormulaDealer(); ;
            }
        }
		#endregion
	}
	#endregion

	
	
}
