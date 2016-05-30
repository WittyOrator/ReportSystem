/***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:SectionData.cs
 * Author:Country.Wu [EMail:Webb.Country.Wu@163.com]
 * Create Time:11/21/2007 02:53:19 PM
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
using Webb.Collections;
using System.ComponentModel;


namespace Webb.Reports.ExControls.Data
{
	#region public class SectionData
	//Wu.Country@2007-11-21 14:53 added this region.
	/// <summary>
	/// Summary description for SectionData.
	/// </summary>
	[Serializable]
	public class SectionGroupInfo : GroupInfo, ISerializable
	{
		protected SectionFilterCollection _SectionFilters;	//Modified at 2008-11-3 14:22:30@Scott
		protected SectionFilterCollectionWrapper _SectionFiltersWrapper;	//Modified at 2009-1-21 13:45:50@Scott
        protected bool _ShowZero = true;

	    [Category("Grouping Information")]
		[System.ComponentModel.EditorAttribute(typeof(Webb.Reports.Editors.SectionFiltersEditor), typeof(System.Drawing.Design.UITypeEditor))] //Modified at 2008-11-3 15:25:33@Scott
		public SectionFilterCollectionWrapper SectionFiltersWrapper
		{
			get
			{
				if(this._SectionFiltersWrapper == null) this.SectionFiltersWrapper = new SectionFilterCollectionWrapper();
				this._SectionFiltersWrapper.SectionFilters = this.SectionFilters;
				return this._SectionFiltersWrapper;}
			set
			{
				this._SectionFiltersWrapper = value;

				_SectionFiltersWrapper.ReportScType=AdvFilterConvertor.GetScType(_SectionFiltersWrapper.ReportScType,DataProvider.VideoPlayBackManager.AdvSectionType);  //2009-6-16 10:18:47@Simon Add this Code

				//Modified at 2009-1-19 14:09:52@Simon
				if(this._SectionFiltersWrapper.ReportScType == ReportScType.Custom)
				{	
					if(!this.Converted) //2009-7-21 10:01:33@Simon Add this Code
					{
						this._SectionFiltersWrapper.SectionFilters=AdvFilterConvertor.GetCustomFilters(DataProvider.VideoPlayBackManager.AdvReportFilters,this._SectionFiltersWrapper.SectionFilters);
					}
				}
				else
				{
                    if (this.ColumnHeading == "New Section Group")
                    {
                        this.ColumnHeading = _SectionFiltersWrapper.ReportScType.ToString();

                    }
					AdvFilterConvertor convertor=new AdvFilterConvertor();

					this._SectionFiltersWrapper.SectionFilters= convertor.GetReportFilters(Webb.Reports.DataProvider.VideoPlayBackManager.AdvReportFilters,_SectionFiltersWrapper.ReportScType);	//add 1-19-2008 scott
				}

				if(this._SectionFiltersWrapper.SectionFilters.Count > 0)	//Modified at 2009-2-6 10:50:32@Scott
				{
					this.SectionFilters = this._SectionFiltersWrapper.SectionFilters;
				}
			}
		}

		[System.ComponentModel.Browsable(false)]
		[System.ComponentModel.EditorAttribute(typeof(Webb.Reports.Editors.SectionFiltersEditor), typeof(System.Drawing.Design.UITypeEditor))] //Modified at 2008-11-3 15:25:33@Scott
		public SectionFilterCollection SectionFilters
		{
			get
			{
				if(this._SectionFilters == null) this._SectionFilters = new SectionFilterCollection();
				return this._SectionFilters;
			}
			set
			{
				this._SectionFilters = value;
			}
		}
        [Browsable(false)]
        public new int SkippedCount
        {
            get { return this._SkippedCount; }
            set { base.SkippedCount = value; }
        }

        [Category("Style")]
		public int ColumnWidth
		{
			get{return this._ColumnWidth;}
			set{this._ColumnWidth = value;}
		}
		protected bool _Visible=true;
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

        [Category("Displaying empty results")]
        public bool ShowZero
        {
            get { return this._ShowZero; }
            set { this._ShowZero = value; }
        }

        [Browsable(false)]
        public new string ShowSymbol 
        {
            get { return base._ShowSymbol; }
            set { base._ShowSymbol = value; }
        }


		public SectionGroupInfo()
		{
			//
			// TODO: Add constructor logic here
			//
			this._OneValuePerRow = true;
            this._ShowZero = true;
		}
		public SectionGroupInfo(PageSectionInfo pageGroupInfo):base(pageGroupInfo)
		{
			this._OneValuePerRow = true;
             
    		this.SectionFiltersWrapper=pageGroupInfo.SectionFilterWrapper;   //Added this code at 2009-1-24 12:51:35@Simon

			this._SectionFiltersWrapper.UpdateSectionFilters();

			this._SectionFilters=this._SectionFiltersWrapper.SectionFilters;

	     }


		public SectionGroupInfo(SectionFilterCollection sectionFilters)
		{
			this._OneValuePerRow = true;
		
			this.SectionFiltersWrapper.SectionFilters.Apply(sectionFilters);   //Added this code at 2009-1-24 12:51:35@Simon

			this.SectionFilters.Apply(sectionFilters);
		}
        public override void GetAllUsedFields(ref ArrayList _UsedFields)
        {
            this.SectionFiltersWrapper.GetAllUsedFields(ref _UsedFields);

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



		public override ArrayList GetFields(DataTable table, Webb.Collections.Int32Collection rows)
		{
			ArrayList m_tempList=new ArrayList();
			foreach(SectionFilter m_Section in this.SectionFilters)
			{
				m_tempList.Add(m_Section.FilterName);
			}
			return m_tempList;		     
		}

		public SectionGroupInfo SubSections(int nStart,int nCount)
		{
			int limitEnd=nStart+nCount;
	
			if(nStart<0||nCount<0||SectionFilters.Count==0)return this;

			if(limitEnd>0)
			{				
				limitEnd=Math.Min(limitEnd,SectionFilters.Count);				
			}
			else
			{
				return this;				
			}
			if(nCount==0)
			{
               limitEnd=SectionFilters.Count;	
			}
			if(nStart>=limitEnd)return this;		
			
			SectionGroupInfo sectionGroup=this.Copy() as SectionGroupInfo;

			sectionGroup.Apply(this);
		     
			sectionGroup.SectionFilters=new SectionFilterCollection();

			sectionGroup.SectionFiltersWrapper=new SectionFilterCollectionWrapper();

			for(int i=nStart;i<limitEnd;i++)
			{
				SectionFilter scfilter=new SectionFilter();

				scfilter.Apply(SectionFilters[i]);

				sectionGroup.SectionFilters.Add(scfilter);
			}

			return sectionGroup;

		}
		public void SortSections(DataTable i_Table,Int32Collection i_InnerRows)
		{	
			if(this.SortingBy==SortingByTypes.None)return;

			if(this.SectionFilters.Count==0)return;

			SectionGroupInfo sectionGroup=this.Copy() as SectionGroupInfo;

			sectionGroup.SubGroupInfos=null;

			sectionGroup.CalculateGroupResult(i_Table,i_InnerRows,i_InnerRows,sectionGroup);
            
			sectionGroup.GroupResults.Sort(this.Sorting,this.SortingBy,this.UserDefinedOrders);

			SectionFilterCollection sectionFilters=new SectionFilterCollection();

			foreach(GroupResult result in sectionGroup.GroupResults)
			{
                string name=result.GroupValue.ToString();

                SectionFilter scFilter=sectionGroup.Sections(name);

                sectionFilters.Add(scFilter);
			}

			this.SectionFiltersWrapper=new SectionFilterCollectionWrapper(sectionFilters,ReportScType.Custom);

			this.SectionFilters=sectionFilters;
		}
		public SectionFilter Sections(string name)
		{
			foreach(SectionFilter scFilter in this.SectionFilters)
			{
				if(scFilter.FilterName==name)return scFilter;
			}
			return null;
		}


		public Int32Collection GetFilteredRows(DataTable i_Table,Int32Collection i_InnerRows)
		{
			i_InnerRows=this.Filter.GetFilteredRows(i_Table,i_InnerRows); 
 
			Int32Collection i_FilterRows=new Int32Collection();

            foreach (int row in i_InnerRows)
            {
                if (row < 0 || row >= i_Table.Rows.Count) continue;

                DataRow dataRow = i_Table.Rows[row];

                foreach (SectionFilter m_Section in this.SectionFilters)
                {
                    if (m_Section.Filter.CheckResultWithBracket(dataRow))	//Modified at 2009-2-16 16:38:31@Scott
                    {
                        if (m_Section.Filter.PlayAfter && i_Table.Rows.Count > row + 1)
                        {
                            i_FilterRows.Add(row + 1);
                        }
                        else
                        {
                            i_FilterRows.Add(row);
                        }
                    }
                }              
                    
            }

			return i_FilterRows;
		}		

		public override void CalculateGroupResult(DataTable i_Table,Webb.Collections.Int32Collection i_OuterRows,Webb.Collections.Int32Collection i_InnerRows, GroupInfo i_GroupInfo)
		{
			i_GroupInfo.UpdateSectionSummaries();	//Modified at 2009-1-21 15:08:49@Scott

			this._Filter=AdvFilterConvertor.GetAdvFilter(DataProvider.VideoPlayBackManager.AdvReportFilters,this._Filter);    //2009-4-29 11:37:37@Simon Add UpdateAdvFilter

			#region Modified Area
			if(this._GroupResults == null) this._GroupResults = new GroupResultCollection();

			this._GroupResults.Clear();

			i_InnerRows=this.Filter.GetFilteredRows(i_Table,i_InnerRows);   //Added this code at 2008-12-24 8:26:50@Simon
             
			if( this.SectionFilters.Count>0)  //modify this code at 2009-2-6 10:15:01@Simon
			{
                int LimitCount=0;  

				foreach(SectionFilter m_Section in this.SectionFilters)
				{
					 if(this.TopCount>0&&LimitCount>=this.TopCount)break;   //Added this code at 2009-2-9 9:36:32@Simon

					GroupResult m_Result = new GroupResult();
					m_Result.GroupValue = m_Section.FilterName;
					m_Result.Filter = m_Section.Filter.Copy();
					m_Result.ParentGroupInfo = this;
					m_Result.ClickEvent = i_GroupInfo.ClickEvent;  
               
                    m_Result.CalculateRowIndicators(i_Table, i_InnerRows);

                    if (m_Result.RowIndicators.Count == 0&&!this.ShowZero)
                    {
                        continue;
                    }

                    if (this.Summaries != null)
                    {
                        m_Result.Summaries = this.Summaries.CopyStructure();
                        m_Result.CalculateSummaryResult(i_Table, i_OuterRows, i_InnerRows);
                    }

					this._GroupResults.Add(m_Result);

                     LimitCount++;

				}
				
			}
			else
			{
				GroupResult m_GroupResult = new GroupResult();

				m_GroupResult.GroupValue = string.Empty;

				m_GroupResult.ParentGroupInfo = this;

				m_GroupResult.RowIndicators = new Int32Collection();
				i_InnerRows.CopyTo(m_GroupResult.RowIndicators);
				m_GroupResult.ClickEvent = i_GroupInfo.ClickEvent;
				this._GroupResults.Add(m_GroupResult);

				if(this.Summaries != null)
				{
					m_GroupResult.Summaries = this.Summaries.CopyStructure();

					m_GroupResult.CalculateSummaryResult(i_Table,i_OuterRows,i_InnerRows);
				}
			}
			

			if(i_GroupInfo.SubGroupInfos.Count > 0)
			{
				foreach(GroupResult m_GroupResult in this._GroupResults)
				{
					if(m_GroupResult.RowIndicators==null)continue;
					m_GroupResult.SubGroupInfos = i_GroupInfo.SubGroupInfos.Copy();
				
					for(int i = 0; i < m_GroupResult.SubGroupInfos.Count; i++)
					{
						GroupInfo resultGroupInfo = m_GroupResult.SubGroupInfos[i];
						GroupInfo subGroupInfo = i_GroupInfo.SubGroupInfos[i];

                        resultGroupInfo.IsSectionOutSide = false;
                        subGroupInfo.IsSectionOutSide = false;

						System.Diagnostics.Trace.Assert(resultGroupInfo != null && subGroupInfo != null,"Calculate Section Group Error");
						resultGroupInfo.CalculateGroupResult(i_Table,i_OuterRows,m_GroupResult.RowIndicators,subGroupInfo);
					}
				
				}
			}
			#endregion        //Modify at 2008-11-4 11:29:27@Scott
		}
		
//        public override void CalculateGroupResult(DataTable i_Table, Webb.Collections.Int32Collection i_OuterRows, Webb.Collections.Int32Collection i_FilterRows, Webb.Collections.Int32Collection i_InnerRows, GroupInfo i_GroupInfo)
//        {			
//            i_GroupInfo.UpdateSectionSummaries();	//Modified at 2009-1-21 15:08:49@Scott

//            this._Filter=AdvFilterConvertor.GetAdvFilter(DataProvider.VideoPlayBackManager.AdvReportFilters,this._Filter);    //2009-4-29 11:37:37@Simon Add UpdateAdvFilter
			
//            //Added this code at 2008-12-22 8:29:27@Simon
//            #region Modified Area
//            if(this._GroupResults == null) this._GroupResults = new GroupResultCollection();
//            this._GroupResults.Clear();

//            i_InnerRows=this.Filter.GetFilteredRows(i_Table,i_InnerRows);   //Added this code at 2008-12-24 8:26:50@Simon

//            i_FilterRows=this.Filter.GetFilteredRows(i_Table,i_FilterRows);   //Added this code at 2008-12-24 8:26:50@Simon

//            if(this.SectionFilters.Count>0)
//            {
//                if (this.AddTotal)
//                {
//                    GroupResult m_GroupResult = new GroupResult();

//                    m_GroupResult.Filter = new Webb.Data.DBFilter();                   

//                    m_GroupResult.GroupValue = this.TotalTitle;

//                    m_GroupResult.ParentGroupInfo = this;  //Add at 2009-2-19 14:23:47@Simon

//                    m_GroupResult.ClickEvent = i_GroupInfo.ClickEvent;

//                    this._GroupResults.Add(m_GroupResult);
//                }

//                int LimitCount=0;

//                foreach(SectionFilter m_Section in this.SectionFilters)
//                {
//                   if(this.TopCount>0&&LimitCount>=this.TopCount)break;   //Added this code at 2009-2-9 9:36:32@Simon

//                    GroupResult m_Result = new GroupResult();

//                    m_Result.GroupValue = m_Section.FilterName;

//                    m_Result.Filter = m_Section.Filter;

//                    m_Result.ParentGroupInfo = this;

//                    m_Result.ClickEvent = i_GroupInfo.ClickEvent;

//                    this._GroupResults.Add(m_Result);

//                    LimitCount++;
//                }		

//                foreach(GroupResult m_GroupResult in this.GroupResults)
//                {
//                    m_GroupResult.ClickEvent = this.ClickEvent;	//12-28-2007@Scott

//                    m_GroupResult.CalculateRowIndicators(i_Table,i_InnerRows);				

//                    if(this.Summaries != null)
//                    {
//                        m_GroupResult.Summaries = this.Summaries.CopyStructure();
//                        m_GroupResult.CalculateSummaryResult(i_Table,i_OuterRows,i_InnerRows);
//                    }
//                }
//            }
//            else
//            {			
//                GroupResult m_GroupResult = new GroupResult();
//                m_GroupResult.GroupValue = string.Empty;

//                m_GroupResult.ParentGroupInfo = this;

//                m_GroupResult.RowIndicators = new Int32Collection();
//                i_InnerRows.CopyTo(m_GroupResult.RowIndicators);
//                m_GroupResult.ClickEvent = i_GroupInfo.ClickEvent;
//                this._GroupResults.Add(m_GroupResult);	
		
//                if(this.Summaries != null)
//                {
//                    m_GroupResult.Summaries = this.Summaries.CopyStructure();
//                    m_GroupResult.CalculateSummaryResult(i_Table,i_OuterRows,i_InnerRows);
//                }
//            }

//            if(i_GroupInfo.SubGroupInfos.Count > 0)
//            {
//                foreach(GroupResult m_GroupResult in this._GroupResults)
//                {
////					if(m_GroupResult.RowIndicators==null)continue;
////					m_GroupResult.SubGroupInfos = i_GroupInfo.SubGroupInfos.Copy();
////					foreach(GroupInfo subGroupinfo in m_GroupResult.SubGroupInfos)
////					{
////						subGroupinfo.CalculateGroupResult(i_Table, i_OuterRows,i_FilterRows, m_GroupResult.RowIndicators,subGroupinfo);
////					}

//                    if(m_GroupResult.RowIndicators==null)continue;

//                    m_GroupResult.SubGroupInfos = i_GroupInfo.SubGroupInfos.Copy();					

//                    for(int i = 0; i < m_GroupResult.SubGroupInfos.Count; i++)
//                    {						
//                        GroupInfo resultGroupInfo = m_GroupResult.SubGroupInfos[i];

//                        GroupInfo subGroupInfo = i_GroupInfo.SubGroupInfos[i];

//                        resultGroupInfo.IsSectionOutSide = false;
//                        subGroupInfo.IsSectionOutSide = false;

//                        System.Diagnostics.Trace.Assert(resultGroupInfo != null && subGroupInfo != null,"Calculate Section Group Error");
//                        resultGroupInfo.CalculateGroupResult(i_Table,i_OuterRows,i_FilterRows,m_GroupResult.RowIndicators,subGroupInfo);
//                    }
//                    //End Edit

//                }
//            }
//            #endregion        //Modify at 2008-12-19 11:29:27@Simon
//        }

        public override void CalculateGroupResult(DataTable i_Table, Webb.Collections.Int32Collection i_OuterRows, Webb.Collections.Int32Collection i_FilterRows, Webb.Collections.Int32Collection i_InnerRows, GroupInfo i_GroupInfo)
        {
            i_GroupInfo.UpdateSectionSummaries();	//Modified at 2009-1-21 15:08:49@Scott

            this._Filter = AdvFilterConvertor.GetAdvFilter(DataProvider.VideoPlayBackManager.AdvReportFilters, this._Filter);    //2009-4-29 11:37:37@Simon Add UpdateAdvFilter

            //Added this code at 2008-12-22 8:29:27@Simon
            #region Modified Area

            if (this._GroupResults == null) this._GroupResults = new GroupResultCollection();
            this._GroupResults.Clear();

            i_InnerRows = this.Filter.GetFilteredRows(i_Table, i_InnerRows);   //Added this code at 2008-12-24 8:26:50@Simon

            i_FilterRows = this.Filter.GetFilteredRows(i_Table, i_FilterRows);   //Added this code at 2008-12-24 8:26:50@Simon

            if (this.SectionFilters.Count > 0)
            {
                if (this.AddTotal)
                {
                     #region Add total
                    GroupResult m_GroupResult = new GroupResult();

                    m_GroupResult.Filter = new Webb.Data.DBFilter();

                    m_GroupResult.GroupValue = this.TotalTitle;

                    m_GroupResult.ParentGroupInfo = this;  //Add at 2009-2-19 14:23:47@Simon

                    m_GroupResult.ClickEvent = i_GroupInfo.ClickEvent;

                    i_InnerRows.CopyTo(m_GroupResult.RowIndicators);

                    this._GroupResults.Add(m_GroupResult);
                        #endregion
                }

                int LimitCount = 0;

                foreach (SectionFilter m_Section in this.SectionFilters)
                {
                    #region Add group Result
 
                    if (this.TopCount > 0 && LimitCount >= this.TopCount) break;   //Added this code at 2009-2-9 9:36:32@Simon

                    GroupResult m_Result = new GroupResult();

                    m_Result.GroupValue = m_Section.FilterName;

                    m_Result.Filter = m_Section.Filter;

                    m_Result.ParentGroupInfo = this;

                    m_Result.ClickEvent = i_GroupInfo.ClickEvent;

                    m_Result.RowIndicators=new Int32Collection();

                    m_Result.ClickEvent = this.ClickEvent;	//12-28-2007@Scott

                    this._GroupResults.Add(m_Result);

                    LimitCount++;
                   #endregion
                }
                #region Calculate result Rows

                foreach (int row in i_InnerRows)
                {  
                    if(row<0||row>=i_Table.Rows.Count)continue;

                    DataRow dataRow = i_Table.Rows[row];

                    foreach (GroupResult m_GroupResult in this.GroupResults)
                    {                         
                        if(m_GroupResult.Filter.CheckResultWithBracket(dataRow))	//Modified at 2009-2-16 16:38:31@Scott
				        {
					        if(m_GroupResult.Filter.PlayAfter && i_Table.Rows.Count > row + 1)
					        {
						        m_GroupResult.RowIndicators.Add(row + 1);
					        }
					        else
					        {
						        m_GroupResult.RowIndicators.Add(row);
					        }
                        }                      
                    }
                }
                #endregion              
            }
            else
            {
                GroupResult m_GroupResult = new GroupResult();
                m_GroupResult.GroupValue = string.Empty;

                m_GroupResult.ParentGroupInfo = this;

                m_GroupResult.RowIndicators = new Int32Collection();

                i_InnerRows.CopyTo(m_GroupResult.RowIndicators);

                m_GroupResult.ClickEvent = i_GroupInfo.ClickEvent;

                this._GroupResults.Add(m_GroupResult);               
            }

            foreach (GroupResult m_GroupResult in this.GroupResults)
            {
                if (this.Summaries != null)
                {
                    m_GroupResult.Summaries = this.Summaries.CopyStructure();
                    m_GroupResult.CalculateSummaryResult(i_Table, i_OuterRows, i_InnerRows);
                }
            }               

            if (i_GroupInfo.SubGroupInfos.Count > 0)
            {
                foreach (GroupResult m_GroupResult in this._GroupResults)
                {   
                    if (m_GroupResult.RowIndicators == null) continue;

                    m_GroupResult.SubGroupInfos = i_GroupInfo.SubGroupInfos.Copy();

                    for (int i = 0; i < m_GroupResult.SubGroupInfos.Count; i++)
                    {
                        GroupInfo resultGroupInfo = m_GroupResult.SubGroupInfos[i];

                        GroupInfo subGroupInfo = i_GroupInfo.SubGroupInfos[i];

                        resultGroupInfo.IsSectionOutSide = false;
                        subGroupInfo.IsSectionOutSide = false;

                        System.Diagnostics.Trace.Assert(resultGroupInfo != null && subGroupInfo != null, "Calculate Section Group Error");

                        resultGroupInfo.CalculateGroupResult(i_Table, i_OuterRows, i_FilterRows, m_GroupResult.RowIndicators, subGroupInfo);
                    }
                    //End Edit

                }
            }
            #endregion        //Modify at 2008-12-19 11:29:27@Simon
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
			int nRet = 0;

			if(this._GroupResults!=null) nRet += this._GroupResults.Count;

			//if(this._AddGroupTotal) nRet++;

			return nRet;
		}

		public override GroupInfo Copy()
		{
			SectionGroupInfo m_GroupInfo = new SectionGroupInfo();
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
            m_GroupInfo.SkippedCount = this.SkippedCount;
			m_GroupInfo._SortingByType = this._SortingByType;
			m_GroupInfo._SortingType = this._SortingType;
			m_GroupInfo._TopCount = this._TopCount;
			m_GroupInfo._GroupTitle = this._GroupTitle;
			m_GroupInfo._TotalTitle = this._TotalTitle;
			m_GroupInfo.Style = this.Style.Copy() as BasicStyle;
			m_GroupInfo._ClickEvent = this._ClickEvent;
			m_GroupInfo.Filter = this.Filter.Copy();
            m_GroupInfo._ShowSymbol = this._ShowSymbol;

            m_GroupInfo.UserDefinedOrders = new UserOrderClS();
            m_GroupInfo.UserDefinedOrders.RelativeGroupInfo = m_GroupInfo;
            foreach (object objValue in this.UserDefinedOrders.OrderValues)
            {
                m_GroupInfo.UserDefinedOrders.OrderValues.Add(objValue);
            }
        
			m_GroupInfo._OneValuePerPage = this._OneValuePerRow;

			m_GroupInfo._ColumnWidth=this._ColumnWidth;

            m_GroupInfo._FollowSummaries = this._FollowSummaries;

            m_GroupInfo._TitleFormat = this._TitleFormat;

			m_GroupInfo.SectionFilters.Apply(this.SectionFilters);

			m_GroupInfo.ColumnIndex=this.ColumnIndex;  //Added this code at 2008-12-24 9:17:02@Simon

			m_GroupInfo.SectionFiltersWrapper = this.SectionFiltersWrapper;	//Modified at 2009-1-21 13:50:50@Scott

			m_GroupInfo.ReportScType = this.ReportScType;	//Modified at 2009-1-21 14:05:45@Scott

			m_GroupInfo.ColorNeedChange = this.ColorNeedChange;	//Modified at 2009-2-11 16:15:49@Scott

            m_GroupInfo._GroupSides=this._GroupSides;  //Add at 2009-2-27 9:33:13@Simon

			m_GroupInfo.Visible=this.Visible; //Add at 2009-2-27 11:48:33@Simon

			m_GroupInfo._IsRelatedForSubGroup=this._IsRelatedForSubGroup;  //2009-4-8 13:06:10@Simon Add this Code

			m_GroupInfo.ShowSymbol=this.ShowSymbol;

            m_GroupInfo.UseLineBreak = this.UseLineBreak;

			m_GroupInfo.Converted=this.Converted;

            m_GroupInfo.IsSectionOutSide = this.IsSectionOutSide;

            m_GroupInfo.DisplayAsColumn = this.DisplayAsColumn;

            m_GroupInfo.DistinctValues = this.DistinctValues;

            m_GroupInfo.ShowZero = this.ShowZero;

            m_GroupInfo.DisplayAsImage = false;

			return m_GroupInfo;
		}

		public void SetSectionFilters(SectionFilterCollection i_Sections)
		{//Modified at 2008-11-3 14:31:58@Scott
			if(i_Sections != null)
			{
				this.SectionFilters.Apply(i_Sections);
			}
		}
        public override string ToString()
        {
            return string.Format("Section({0})",this.ColumnHeading);
        }

		#region Serialization By Macro 2008-12-11 16:12:02
		override public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			base.GetObjectData(info, context);

			info.AddValue("Visible",this.Visible);

			info.AddValue("_SectionFilters",_SectionFilters,typeof(Webb.Reports.SectionFilterCollection));

			info.AddValue("SectionFiltersWrapper",this.SectionFiltersWrapper,typeof(SectionFilterCollectionWrapper));	//Modified at 2009-1-21 13:54:05@Scott

            info.AddValue("_ShowZero", this._ShowZero);
		}

		public SectionGroupInfo(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
		{
            try
            {
                _ShowZero = info.GetBoolean("_ShowZero");
            }
            catch
            {
                _ShowZero = true;
            }
			try
			{
				Visible = info.GetBoolean("Visible");
			}
			catch
			{
				Visible = true;
			}

			try
			{
				_SectionFilters = info.GetValue("_SectionFilters",typeof(SectionFilterCollection)) as SectionFilterCollection;					
			}
			catch
			{
				_SectionFilters = new SectionFilterCollection();
			}	

			//Modified at 2009-1-21 13:54:02@Scott
			try
			{
				this.SectionFiltersWrapper = info.GetValue("SectionFiltersWrapper",typeof(SectionFilterCollectionWrapper)) as SectionFilterCollectionWrapper;
		     }
			catch
			{
				//this.SectionFiltersWrapper = new SectionFilterCollectionWrapper();
			}
		}
		#endregion
		
	}
	#endregion
}
