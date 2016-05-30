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
using System.Drawing;
using System.Windows.Forms;

using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Container;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraPrinting;
using DevExpress.Utils.Design;
using DevExpress.Utils;

using Webb.Reports.ExControls;
using Webb.Reports.ExControls.Data;
using System.Security.Permissions;
using Webb.Collections;
using Webb.Reports.DataProvider;
using Webb.Reports.ExControls.Styles;


namespace Webb.Reports.ExControls.Views
{
	#region public class SimpleGroupView
	[Serializable]
	public class SimpleGroupView : ExControlView
	{
		//ISerializable Members
		#region ISerializable Members

		override public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			// TODO:  Add ExControlView.GetObjectData implementation
			//info.AddValue("MainStyle",this.MainStyle,typeof(BasicStyle));
			base.GetObjectData(info,context);
			//
			info.AddValue("_RootGroupInfo",this._RootGroupInfo,typeof(FieldGroupInfo));
			info.AddValue("_ShowRowIndicators",this._ShowRowIndicators);
			info.AddValue("_OneValuePerPage",this._OneValuePerPage);
			info.AddValue("_SizeSelfAdapting",this._SizeSelfAdapting);
			info.AddValue("_FootballType",(int)this._FootballType);
			info.AddValue("_Fitler",this._Fitler,typeof(Webb.Data.DBFilter));
			info.AddValue("_ColumnsWidth",this._ColumnsWidth,typeof(Int32Collection));
			info.AddValue("_RowsHight",this._RowsHight,typeof(Int32Collection));
			info.AddValue("_Styles",this._Styles,typeof(Styles.ExControlStyles));
			info.AddValue("_BreakRows",this._BreakRows,typeof(Int32Collection));
			info.AddValue("_HaveHeader",this._HaveHeader);
			info.AddValue("_CellSizeAutoAdapting",this._CellSizeAutoAdapting,typeof(CellSizeAutoAdaptingTypes));
			info.AddValue("_TopCount",this._TopCount);
			info.AddValue("_Total",this._Total);
			info.AddValue("_SectionInOneRow",this._SectionInOneRow);
			info.AddValue("_SectionTitle",this._SectionTitle);
			info.AddValue("_TotalTitle",this._TotalTitle);
			info.AddValue("_TotalColumns",this._TotalColumns,typeof(Int32Collection));
			info.AddValue("_HeightPerPage",this._HeightPerPage);
			info.AddValue("_AcrossPage",this._AcrossPage);
		}

		public SimpleGroupView(SerializationInfo info, StreamingContext context):base(info,context)
		{
			// TODO:  Add ExControlView.GetObjectData implementation
			//this.MainStyle = info.GetValue("MainStyle",typeof(BasicStyle)) as BasicStyle;
			try
			{
				this._RootGroupInfo = info.GetValue("_RootGroupInfo",typeof(GroupInfo)) as GroupInfo;
			}
			catch
			{
				this._RootGroupInfo = new Webb.Reports.ExControls.Data.FieldGroupInfo(Webb.Data.PublicDBFieldConverter.AvialableFields[0].ToString());
				this._RootGroupInfo.ColumnHeading = "New Group";
			}

			try
			{
				this._ShowRowIndicators = info.GetBoolean("_ShowRowIndicators");
			}
			catch
			{
				this.ShowRowIndicators = false;
			}

			try
			{
				this._OneValuePerPage = info.GetBoolean("_OneValuePerPage");
			}
			catch
			{
				this._OneValuePerPage = false;
			}

			try
			{
				this._SizeSelfAdapting = info.GetBoolean("_SizeSelfAdapting");
			}
			catch
			{
				this._SizeSelfAdapting = false;
			}

			try
			{
				this._FootballType = (FootballTypes)info.GetInt32("_FootballType");
			}
			catch
			{
				this._FootballType = FootballTypes.Both;
			}

			try
			{
				this._Fitler = info.GetValue("_Fitler",typeof(Webb.Data.DBFilter)) as Webb.Data.DBFilter;
     			this._Fitler=AdvFilterConvertor.GetAdvFilter(DataProvider.VideoPlayBackManager.AdvReportFilters,this._Fitler);    //2009-4-29 11:37:37@Simon Add UpdateAdvFilter
			}
			catch
			{
				this._Fitler = new Webb.Data.DBFilter();
			}

			try
			{
				this._ColumnsWidth = info.GetValue("_ColumnsWidth",typeof(Int32Collection)) as Int32Collection;
			}
			catch
			{
				this._ColumnsWidth = new Int32Collection();
			}
			
			try
			{
				this._RowsHight = info.GetValue("_RowsHight",typeof(Int32Collection)) as Int32Collection;
			}
			catch
			{
				this._RowsHight = new Int32Collection();
			}

			try
			{
				this._Styles = info.GetValue("_Styles",typeof(Styles.ExControlStyles)) as Styles.ExControlStyles;
			}
			catch
			{
				this._Styles = new ExControlStyles();
                this._Styles.LoadDefaultStyle();    //08-14-2008@Scott
			}

			try
			{
				this._BreakRows = info.GetValue("_BreakRows",typeof(Int32Collection)) as Int32Collection;
			}
			catch
			{
				this._BreakRows = new Int32Collection();
			}

			try
			{
				this._HaveHeader = info.GetBoolean("_HaveHeader");
			}
			catch
			{
				this._HaveHeader = true;
			}

			try
			{
				this._CellSizeAutoAdapting = (CellSizeAutoAdaptingTypes)info.GetValue("_CellSizeAutoAdapting",typeof(CellSizeAutoAdaptingTypes));
			}
			catch
			{
				this._CellSizeAutoAdapting = CellSizeAutoAdaptingTypes.WordWrap;
			}

			try
			{
				this._TopCount = info.GetInt32("_TopCount");
			}
			catch
			{
				this._TopCount = 0;
			}

			try
			{
				this._Total = info.GetBoolean("_Total");
			}
			catch
			{
				this._Total = false;
			}

			try
			{
				this._SectionTitle = info.GetString("_SectionTitle");
			}
			catch
			{
				this._SectionTitle = string.Empty;
			}

			try
			{
				this._SectionInOneRow = info.GetBoolean("_SectionInOneRow");
			}
			catch
			{
				this._SectionInOneRow = true;
			}

			try
			{
				this._TotalTitle = info.GetString("_TotalTitle");
			}
			catch
			{
				this._TotalTitle = "Total";
			}

			try
			{
				this._TotalColumns = info.GetValue("_TotalColumns",typeof(Int32Collection)) as Int32Collection;
			}
			catch
			{
				this._TotalColumns = new Int32Collection();
			}

			try
			{
				this._HeightPerPage = info.GetInt32("_HeightPerPage");
			}
			catch
			{
				this._HeightPerPage = 0;
			}

			try
			{
				this._AcrossPage = info.GetBoolean("_AcrossPage");
			}
			catch
			{
				this._AcrossPage = false;
			}
		}
		#endregion
		
		//Fields
		#region Fields
		protected Webb.Reports.ExControls.Data.GroupInfo _RootGroupInfo;
		protected CellSizeAutoAdaptingTypes _CellSizeAutoAdapting;
		protected FootballTypes _FootballType;
		protected Webb.Data.DBFilter _Fitler;
		protected int _TopCount;
		protected bool _ShowRowIndicators;
		protected bool _HaveHeader;
		protected bool _SizeSelfAdapting;								//useless
		protected bool _OneValuePerPage;								//useless
		protected bool _Total;
		protected string _TotalTitle;									//05-09-2008@Scott
		protected bool _SectionInOneRow;								//05-07-2008@Scott
		protected string _SectionTitle;									//05-07-2008@Scott
		protected Int32Collection _TotalColumns;						//05-09-2008@Scott
		protected Int32Collection _FilteredRows;						//NonSerializable
		protected GroupSummaryCollection _TotalSummaries;				//NonSerializable

		protected Int32Collection _HeaderRows;							//header row indicators for style builder
		protected Int32Collection _SectionRows;							//section row indicators for style builder
		protected Int32Collection _TotalRows;							//total row indicators for style builder
		protected Int32Collection _BreakRows;							//useless,page break row indicators for style builder
		protected StyleBuilder.StyleColumnInfo _ColumnStyleRows;		//columns indicators for style builder

		protected Int32Collection _ColumnsWidth;
		protected Int32Collection _RowsHight;
		protected Styles.ExControlStyles _Styles;
		protected int _HeightPerPage;	//05-16-2008@Scott

		protected bool _AcrossPage;	//05-26-2008@Scott
		#endregion

		//Properties
		#region Properties
		public bool AcrossPage
		{
			get{return this._AcrossPage;}
			set{this._AcrossPage = value;}
		}
		public int HeightPerPage
		{
			get{return this._HeightPerPage;}
			set{this._HeightPerPage = value;}
		}
		public Int32Collection TotalColumns
		{
			get
			{
				if(this._TotalColumns == null) this._TotalColumns = new Int32Collection();

				return this._TotalColumns;
			}
			set{this._TotalColumns = value;}
		}
		public GroupSummaryCollection TotalSummaries
		{
			get
			{
				if(this._TotalSummaries == null) this._TotalSummaries = new GroupSummaryCollection();

				return this._TotalSummaries;
			}
			set{this._TotalSummaries = value;}
		}
		public bool SectionInOneRow
		{
			get{return this._SectionInOneRow;}
			set{this._SectionInOneRow = value;}
		}
		public string SectionTitle
		{
			get{return this._SectionTitle;}
			set{this._SectionTitle = value;}
		}
		public int TopCount
		{
			get{return this._TopCount;}
			set{this._TopCount = value < 0 ? 0 : value;}
		}
		public bool SizeSelfAdapting
		{
			get{return this._SizeSelfAdapting;}
			set{this._SizeSelfAdapting = value;}
		}
		public bool HaveHeader
		{
			get
			{
				if(this.AcrossPage) return false;	//06-27-2008@Scott

				return this._HaveHeader;
			}
			set{this._HaveHeader = value;}
		}
		public Styles.ExControlStyles Styles
		{
			get{return this._Styles;}
			set{this._Styles = value;}
		}
		public Int32Collection ColumnsWidth
		{
			get
			{
				if(this._ColumnsWidth==null) this._ColumnsWidth = new Int32Collection();
				
				return this._ColumnsWidth;
			}
			set{this._ColumnsWidth = value;}
		}
		public Int32Collection RowsHight
		{
			get
			{
				if(this._RowsHight==null) this._RowsHight = new Int32Collection();
				
				return this._RowsHight;
			}
			set{this._RowsHight = value;}
		}
		public Int32Collection HeaderRows
		{
			get
			{
				if(this._HeaderRows==null) this._HeaderRows = new Int32Collection();
				
				return this._HeaderRows;
			}
		}
		public Int32Collection SectionRows
		{
			get
			{
				if(this._SectionRows==null) this._SectionRows = new Int32Collection();
				
				return this._SectionRows;
			}
		}
		public Int32Collection TotalRows
		{
			get
			{
				if(this._TotalRows==null) this._TotalRows = new Int32Collection();
				
				return this._TotalRows;
			}
		}
		public Int32Collection BreakRows
		{
			get
			{
				if(this._BreakRows==null) this._BreakRows = new Int32Collection();
				
				return this._BreakRows;
			}
		}
		public StyleBuilder.StyleColumnInfo ColumnStyleRows
		{
			get
			{
				if(this._ColumnStyleRows == null) this._ColumnStyleRows = new Webb.Reports.ExControls.Styles.StyleBuilder.StyleColumnInfo();
				
				return this._ColumnStyleRows;
			}
		}

		public Webb.Data.DBFilter Filter
		{
			get{return this._Fitler;}
			set{this._Fitler = value.Copy();}
		}
		
		public bool ShowRowIndicators
		{
			get{return this._ShowRowIndicators;}
			set{this._ShowRowIndicators = value;}
		}
		
		//useless
		new public bool OneValuePerPage
		{
			get{return false;}
			set{}
		}

		public CellSizeAutoAdaptingTypes CellSizeAutoAdapting
		{
			get{return this._CellSizeAutoAdapting;}
			set
			{
				this._CellSizeAutoAdapting = value;
			}
		}
		
		public FootballTypes FootballType
		{
			get{return this._FootballType;}
			set{this._FootballType = value;}
		}

		public Webb.Reports.ExControls.Data.GroupInfo RootGroupInfo
		{
			get{return this._RootGroupInfo;}
			set{this._RootGroupInfo = value;}
		}

		public bool Total
		{
			get{return this._Total;}
			set{this._Total = value;}
		}

		public string TotalTitle
		{
			get{return this._TotalTitle;}
			set{this._TotalTitle = value;}
		}

		public Int32Collection FilteredRows
		{
			get
			{
				if(this._FilteredRows == null) this._FilteredRows = new Int32Collection();

				return this._FilteredRows;
			}
		}
		#endregion

		//ctor
		public SimpleGroupView(SimpleGroupingControl i_Control):base(i_Control as ExControl)
		{
			this._RootGroupInfo = new Webb.Reports.ExControls.Data.FieldGroupInfo(Webb.Data.PublicDBFieldConverter.AvialableFields[0].ToString());
			this._RootGroupInfo.ColumnHeading = "New Group";
			this._Fitler = new Webb.Data.DBFilter();
			this._Styles = new ExControlStyles();
            this._Styles.LoadDefaultStyle();    //08-14-2008@Scott
			this._ShowRowIndicators = false;
			this._CellSizeAutoAdapting = CellSizeAutoAdaptingTypes.WordWrap;
			this._OneValuePerPage = false;
			this._HaveHeader = true;
			this._SizeSelfAdapting = false;
			this._TopCount = 0;
			this._Total = false;
			this._TotalTitle = "Total";
			this._SectionTitle = string.Empty;
			this._SectionInOneRow = true;
			this._TotalColumns = new Int32Collection();
			this._HeightPerPage = 0;
		}

		//Calculate grouped rows and columns
		#region Calculate grouped rows and columns
		/// <summary>
		/// Calculate grouped columns
		/// </summary>
		/// <returns></returns>
		public int GetGroupedColumns()
		{
			if(this.RootGroupInfo == null) return 1;

			int nCols = 0;

			if(this._ShowRowIndicators)
			{
				nCols += 1;
			}

			this.GetGroupedColumns(this.RootGroupInfo, ref nCols);

			return nCols;
		}

		private void GetGroupedColumns(GroupInfo groupInfo, ref int nCols)
		{
			if(groupInfo.FollowSummaries)
			{
				if(groupInfo.Summaries != null)
				{
					foreach(GroupSummary summary in groupInfo.Summaries)
					{
						nCols++;

						if(!this.AcrossPage) summary.ColumnIndex = nCols;  
					}
				}
			}

            if (!groupInfo.DistinctValues)	//06-23-2008@Scott
			{
				nCols++;

				if(!this.AcrossPage) groupInfo.ColumnIndex = nCols;
			}

			if(groupInfo.FollowSummaries)
			{}
			else
			{
				if(groupInfo.Summaries != null)
				{
					foreach(GroupSummary summary in groupInfo.Summaries)
					{
						nCols++;

						if(!this.AcrossPage) summary.ColumnIndex = nCols;
					}
				}
			}

			if(this.AcrossPage) nCols *= groupInfo.GroupResults.Count;	//06-27-2008
			//nCols += groupInfo.GetGroupedColumns();

			foreach(GroupInfo subGroupInfo in groupInfo.SubGroupInfos)
			{
				this.GetGroupedColumns(subGroupInfo, ref nCols);
			}
		}
		
		/// <summary>
		/// Calculate grouped rows
		/// </summary>
		/// <returns></returns>
		public int GetGroupedRows()
		{
			int m_value = 0;
			
			this.GetSubRows(this._RootGroupInfo,ref m_value);

			m_value += this.GetHeaderRowsCount();	//add header rows

			if(this._Total) m_value++;	//add total rows

			return m_value;
		}

		private int GetHeaderRowsCount()
		{
			int nRet = 0;

			if(this.HaveHeader)
			{
				nRet++;
			}
			
			if(this.OneValuePerPage && this.HaveHeader)
			{
				if(this._RootGroupInfo == null) return nRet;

				if(this._RootGroupInfo.GroupResults == null) return nRet;

				if(this._RootGroupInfo.GroupResults.Count == 0) return nRet;

				return this._RootGroupInfo.GroupResults.Count;
			}

			return nRet;
		}

		/// <summary>
		/// caculate total rows grouped
		/// </summary>
		/// <param name="i_GroupInfo"></param>
		/// <param name="i_value">must be zero</param>
		private void GetSubRows(GroupInfo i_GroupInfo,ref int i_value)
		{
			int m_TopGroups = i_GroupInfo.GetGroupedRows();

			if(i_value>0 && m_TopGroups>0)
			{
				i_value --;		//-----------------------------------------------
				//|parent group result1	|	child group result1	|	//truncate this row
				//-----------------------------------------------
				//|						|	child group result2	|
				//-----------------------------------------------
				//|						|	...					|
				//-----------------------------------------------
				
				if(i_GroupInfo.ParentGroupResult!=null&&i_GroupInfo.ParentGroupResult.ParentGroupInfo!=null)
				{
                    if (i_GroupInfo.ParentGroupResult.ParentGroupInfo.DistinctValues)
					{
						i_value++;	//-----------------------------------------------
						//|parent group result1	|						|	//add this row
						//-----------------------------------------------
						//|						|	child group result1	|
						//-----------------------------------------------
						//|						|	child group result2	|
						//-----------------------------------------------
						//|						|	...					|
						//-----------------------------------------------
					}
				}
			}

			if(i_GroupInfo != null && i_GroupInfo.SubGroupInfos.Count == 0)		//calculate leaf group rows
			{
                if (i_GroupInfo.DistinctValues)
				{
					i_value += m_TopGroups;	//???
				}
			}
			
			if(i_GroupInfo.AddTotal) i_value++;	//calculate total row
			
			i_value += m_TopGroups;	//calculate grouped result rows
			
			if(i_GroupInfo.GroupResults!=null)
			{
				for(int m_index = 0; m_index<m_TopGroups/*Math.Min(m_TopGroups,i_GroupInfo.GroupResults.Count)*/; m_index++ )
				{
					GroupResult m_Result = i_GroupInfo.GroupResults[m_index];
					
					int tempValue = i_value,maxValue = 0;

					foreach(GroupInfo groupInfo in m_Result.SubGroupInfos)
					{
						GetSubRows(groupInfo,ref tempValue);						

						maxValue = Math.Max(maxValue,tempValue);

						tempValue = i_value;
					}
					i_value = Math.Max(i_value,maxValue);
				}
			}
		}
		#endregion

		public override int CreateArea(string areaName, IBrickGraphics graph)
		{
			if(this.PrintingTable != null)
			{
				 this.PrintingTable.ExControl=this.ExControl;

				this.PrintingTable.RepeatedHeader = !this.ExControl.Report.Template.OneValuePerPage && !this.ExControl.Report.Template.ControlHeaderOnce && this.HaveHeader;	//08-12-2008@Scott[1.1]

				this.PrintingTable.HeightPerPage = this.ExControl.Report.GetHeightPerPage(); //this._HeightPerPage;
			
				this.PrintingTable.ReportHeaderHeight = this.ExControl.Report.GetReportHeaderHeight();	//report header

				this.PrintingTable.ReportFooterHeight = this.ExControl.Report.GetReportFooterHeight();	//report footer
			}

			return base.CreateArea (areaName, graph);
		}

		//Override members
		#region override members
		/// <summary>
		/// Paint in design mode
		/// </summary>
		/// <param name="e"></param>
		override public void Paint(PaintEventArgs e)
		{
			if(this.PrintingTable!=null)
			{
				if(this.ThreeD)
				{
					this.PrintingTable.PaintTable3D(e,false,this.ExControl.XtraContainer.Bounds);
				}
				else
				{
					this.PrintingTable.PaintTable(e,false,this.ExControl.XtraContainer.Bounds);
				}
			}
			else
			{
				base.Paint(e);
			}
		}

		#region Convert old GroupInfo to new structure
		static public void ConvertOldGroupInfo(GroupInfo rootGroupInfo)
		{
			if(rootGroupInfo == null || rootGroupInfo.SubGroupInfo == null) return;

			GroupInfoCollection groupInfos = GetOldGroupInfo(rootGroupInfo);

			SetNewGroupInfo(rootGroupInfo,groupInfos);
		}

		static private GroupInfoCollection GetOldGroupInfo(GroupInfo rootGroupInfo)
		{
			GroupInfoCollection groupInfos = new GroupInfoCollection();

			GroupInfo groupInfo = rootGroupInfo;

			while(groupInfo.SubGroupInfo != null)
			{
				groupInfos.Add(groupInfo.SubGroupInfo.Copy());

				groupInfo = groupInfo.SubGroupInfo;
			}

			return groupInfos;
		}

		static private void SetNewGroupInfo(GroupInfo rootGroupInfo,GroupInfoCollection groupInfos)
		{
			rootGroupInfo.SubGroupInfo = null;

			rootGroupInfo.SubGroupInfos.Clear();

			GroupInfo groupInfo = rootGroupInfo;

			foreach(GroupInfo subGroupInfo in groupInfos)
			{
				groupInfo.SubGroupInfos.Add(subGroupInfo);

				groupInfo = subGroupInfo;
			}
		}
		#endregion

		/// <summary>
		/// Calculate grouped result
		/// </summary>
		/// <param name="i_Table">data source</param>
		public override void CalculateResult(DataTable i_Table)
		{
			//Convert old group struct to new one
			ConvertOldGroupInfo(this._RootGroupInfo);

			//If have no data source ,clear group struct
			if(i_Table == null) 
			{
				this.RootGroupInfo.ClearGroupResult(this._RootGroupInfo);
				
				return;
			}

			//Filter rows
			Webb.Collections.Int32Collection m_Rows = this.OneValueScFilter.Filter.GetFilteredRows(i_Table);

			m_Rows = this.RepeatFilter.Filter.GetFilteredRows(i_Table,m_Rows);	 //Added this code at 2008-12-26 12:22:40@Simon

			m_Rows = this.Filter.GetFilteredRows(i_Table,m_Rows);

			m_Rows.CopyTo(this.FilteredRows);

			//Find and create section filters
			//this.CheckSectionFilters();

			//Calculate group result
			this._RootGroupInfo.CalculateGroupResult(i_Table,m_Rows,m_Rows,this.RootGroupInfo);

			//Calculate Total
			//Int32Collection totalIndicators = this.GetAllTotalIndicators();

			//if(this.Total) this.CalculateAllTotal(i_Table,totalIndicators.Count,totalIndicators);
		}

		/// <summary>
		/// Create printing table
		/// </summary>
		/// <returns></returns>
		public override bool CreatePrintingTable()
		{
			if(this._RootGroupInfo==null) return false;
			
			int m_Rows = this.GetGroupedRows();
			int m_Column = this.GetGroupedColumns();
			if(m_Rows <= 0 || m_Column <= 0)
			{
				this.PrintingTable = null;
				return false;
			}

			System.Diagnostics.Debug.WriteLine(string.Format("Create print table:{0}X{1}",m_Rows,m_Column));
			if(this.TopCount > 0 && m_Rows < this.TopCount) m_Rows = this.TopCount;
			this.PrintingTable = new WebbTable(m_Rows,m_Column);
			//Set value
			this.HeaderRows.Clear();
			this.SectionRows.Clear();
			this.TotalRows.Clear();
			this.BreakRows.Clear();
			this.ColumnStyleRows.Clear();
			
			this.SetTableValue();

			StyleBuilder.StyleRowsInfo m_StyleInfo = new Styles.StyleBuilder.StyleRowsInfo(this._HeaderRows,this._SectionRows,this._TotalRows,this.ShowRowIndicators,this.HaveHeader);

			Int32Collection ignoreRows = this.HeaderRows.Combine(this.HeaderRows,this.SectionRows,this.TotalRows);

			StyleBuilder styleBuilder = new StyleBuilder();

			styleBuilder.BuildGroupStyle(this.PrintingTable,m_StyleInfo,this.RootGroupInfo/*this.ColumnStyleRows*/,this.Styles,ignoreRows);	//Added this code at 2008-11-4 16:10:40@Simon

			this.ApplyColumnWidthStyle(m_Column);
			this.ApplyRowHeightStyle(m_Rows);
			switch(this.CellSizeAutoAdapting)
			{
				case CellSizeAutoAdaptingTypes.NotUse:
					break;
				case CellSizeAutoAdaptingTypes.WordWrap:
					this.PrintingTable.AutoAdjustSize(this.ExControl.CreateGraphics(),true,false);
					break;
				case CellSizeAutoAdaptingTypes.OneLine:
					this.PrintingTable.AutoAdjustSize(this.ExControl.CreateGraphics(),false,false);
					break;
			}
			
			if(this.TopCount <= 0) 
			{
				this.PrintingTable.DeleteEmptyRows();
			}
			else
			{
				this.PrintingTable.DeleteExcrescentRows(this.TopCount/* + this.HeaderRows.Count*/);
			}
 
			System.Diagnostics.Debug.WriteLine("Create print table completely");
			
			return true;
		}
		#endregion

		private void CheckSectionFilters()
		{//need change
			if(this.ExControl==null) return;
			if(this.ExControl.Report==null) return;
			WebbReport m_WebbReport = this.ExControl.Report as WebbReport;
			if(m_WebbReport==null) return;
			if(m_WebbReport.Template.SectionFilters.Count<=0)
			{
				this.RemoveSectionFilters();
			}
			else
			{
				SectionFilterCollection reportSections=m_WebbReport.Template.SectionFilters;

				AdvFilterConvertor convertor = new AdvFilterConvertor();

				if(DataProvider.VideoPlayBackManager.AdvSectionType!=AdvScoutType.None&&m_WebbReport.Template.ReportScType!=ReportScType.Custom)
				{									    
					ReportScType sctype=AdvFilterConvertor.GetScType(m_WebbReport.Template.ReportScType,DataProvider.VideoPlayBackManager.AdvSectionType);  //2009-6-16 10:18:47@Simon Add this Code	    
					
					reportSections = convertor.GetReportFilters(Webb.Reports.DataProvider.VideoPlayBackManager.AdvReportFilters,sctype);	//add 1-19-2008 scott
				}           

				this.CreateSectionGroupInfo(reportSections);
			}
		}

		private void RemoveSectionFilters()
		{
			if(this._RootGroupInfo is SectionGroupInfo)
			{
				System.Diagnostics.Debug.Assert(this._RootGroupInfo.SubGroupInfos.Count == 1);

				this._RootGroupInfo = this._RootGroupInfo.SubGroupInfos[0];
			}
		}

		private void CreateSectionGroupInfo(SectionFilterCollection i_Sections)
		{
			if(this._RootGroupInfo is SectionGroupInfo)
			{
				(this._RootGroupInfo as SectionGroupInfo).SetSectionFilters(i_Sections);
			}
			else
			{
				SectionGroupInfo m_SectionInfo = new SectionGroupInfo();
				
				m_SectionInfo.SetSectionFilters(i_Sections);

				m_SectionInfo.SubGroupInfos.Clear();

				m_SectionInfo.SubGroupInfos.Add(this._RootGroupInfo);

				this._RootGroupInfo = m_SectionInfo;
			}

			this._RootGroupInfo.ColumnHeading = this.SectionTitle;

            this._RootGroupInfo.DistinctValues = this.SectionInOneRow;
		}

		private void ApplyRowHeightStyle(int m_Rows)
		{
			if(this.RowsHight.Count<=0) return;
			
			int count = Math.Min(this.RowsHight.Count,m_Rows);
			
			for(int m_row = 0;m_row<count;m_row++)
			{
				this.PrintingTable.GetCell(m_row,0).CellStyle.Height = this.RowsHight[m_row];
			}
		}

		private void ApplyColumnWidthStyle(int m_Cols)
		{
			if(this.ColumnsWidth.Count<=0) return;

			int count = Math.Min(this.ColumnsWidth.Count,m_Cols);
			
			for(int m_col = 0;m_col<count;m_col++)
			{
				this.PrintingTable.GetCell(0,m_col).CellStyle.Width = this.ColumnsWidth[m_col];
			}
		}

		private void SetTableValue()
		{
			int m_Rows = 0,m_Col =0;
			
			if(this.ShowRowIndicators) m_Col = 1;	//add row indicator columns

			//this.SetColumnStyles();	//02-25-2008@Scott	//03-18-2008@Scott
			if(!this.ShowRowIndicators) this.PrintingTable.GetCell(0,0).Text = this.SectionTitle;

			if(!this.OneValuePerPage && this.HaveHeader)
			{
				int nHeaderStart = m_Rows,nHeaderCount = 0;

				this.SetHeaderValue(ref m_Rows);	//set header value
			
				nHeaderCount = m_Rows - nHeaderStart;

				this.SetHeaderRows(nHeaderStart,nHeaderCount);
			}
			
			this.SetRowsValue(ref m_Rows,ref m_Col,this._RootGroupInfo);	//set row value

			if(this._Total)
			{
				this.SetTotalValue();	//04-30-2008@Scott
			}

			if(this.ShowRowIndicators)
			{//set row indicator value
				this.SetRowIndicators(this.PrintingTable.GetRows());
			}
		}

		private void SetTotalValue()
		{
			int lastRow = this.PrintingTable.GetRows() - 1;

			int lastCol = this.PrintingTable.GetColumns() - 1;

			this.TotalRows.Add(lastRow);

			//			this.PrintingTable.MergeCells(lastRow,lastRow,0,lastCol);
			//
			//			WebbTableCell cell = this.PrintingTable.GetCell(lastRow,0) as WebbTableCell;
			//
			//			if(cell != null)
			//			{
			//				cell.Text = string.Format("{0}: {1}",this._TotalTitle,this.FilteredRows.Count);
			//			}

			//05-09-2008@Scott
			int index = 0;

			bool bFindFirstCol = false;

			for(int col = 0; col <= lastCol; col++)
			{
				if(index > this.TotalSummaries.Count - 1) break;

				if(this.ColumnStyleRows.GroupColumns.Contains(col) || col == 0) continue;

				if(!this.TotalColumns.Contains(col)) 
				{
					index ++;
					
					continue;
				}

				if(!bFindFirstCol)
				{
					int startCol = col > 1 ? col - 2 : col - 1;

					this.PrintingTable.MergeCells(lastRow,lastRow,startCol,col - 1);

					this.PrintingTable.GetCell(lastRow,startCol).Text = this.TotalTitle;

					bFindFirstCol = true;
				}

				WebbTableCell cell = this.PrintingTable.GetCell(lastRow,col) as WebbTableCell;

				GroupSummary summary = this.TotalSummaries[index++];

				WebbTableCellHelper.SetCellValue(this.PrintingTable,lastRow,col,summary);
			}
		}

		private void SetHeaderRows(int nStartRow,int nHeaderRowCount)
		{
			if(this.AcrossPage)
			{
				this.HeaderRows.Clear();

				return;
			}

			for(int i = nStartRow; i < nStartRow + nHeaderRowCount; i++)
			{
				this.HeaderRows.Add(i);
			}
		}
		
		private void SetHeaderValue(ref int nRow)
		{
			int nCol = 0;

			if(this._ShowRowIndicators) nCol++;

			this.SetHeaderValue(this._RootGroupInfo, nRow, ref nCol);

			nRow++;
		}

		private void SetHeaderValue(GroupInfo groupInfo, int nRow, ref int nCol)
		{
			if(groupInfo.FollowSummaries)
			{
				if(groupInfo.Summaries!=null)
				{
					foreach(GroupSummary m_Summary in groupInfo.Summaries)
					{
						this.PrintingTable.GetCell(nRow,nCol).Text = m_Summary.ColumnHeading;

						this.SetSummaryColumnStyle(m_Summary.SummaryType,nCol);	//Set column style

						nCol++;
					}
				}
			}

            if (!groupInfo.DistinctValues)	//if set OnValuePerRow, don't set group title
			{
				this.PrintingTable.GetCell(nRow, nCol).Text = groupInfo.ColumnHeading;

				nCol++;
			}

			if(groupInfo.FollowSummaries)
			{}
			else
			{
				if(groupInfo.Summaries!=null)
				{
					foreach(GroupSummary m_Summary in groupInfo.Summaries)
					{
						this.PrintingTable.GetCell(nRow,nCol).Text = m_Summary.ColumnHeading;

						this.SetSummaryColumnStyle(m_Summary.SummaryType,nCol);	//Set column style

						nCol++;
					}
				}
			}

			foreach(GroupInfo subGroupInfo in groupInfo.SubGroupInfos)
			{
				SetHeaderValue(subGroupInfo, nRow, ref nCol);
			}
		}

		private void SetSummaryColumnStyle(SummaryTypes type,int col)
		{
			switch(type)
			{
				case SummaryTypes.Frequence:
					this.ColumnStyleRows.FreqencyColumns.Add(col);
					break;
				case SummaryTypes.ComputedPercent:          //Modified at 2008-9-25 13:57:36@Simon
				case SummaryTypes.Percent:
				case SummaryTypes.RelatedPercent:
				case SummaryTypes.GroupPercent:
				case SummaryTypes.DistPercent:	//Modified at 2008-10-6 14:01:25@Scott
				case SummaryTypes.DistGroupPercent: //Modified at 2008-10-6 15:08:29@Scott
				case SummaryTypes.TopPercent:
					this.ColumnStyleRows.PercentColumns.Add(col);
					break;
				case SummaryTypes.Total:
				case SummaryTypes.TotalMinus:
				case SummaryTypes.TotalPlus:
				case SummaryTypes.TotalPointsBB:
					this.ColumnStyleRows.TotalColumns.Add(col);
					break;
				case SummaryTypes.Average:
				case SummaryTypes.AverageMinus:
				case SummaryTypes.AveragePlus:
					this.ColumnStyleRows.AverageColumns.Add(col);
					break;
				default:
					break;
			}
		}
        
		private void SetRowsValue(ref int i_Row,ref int i_Col,GroupInfo i_GroupInfo)
		{
			int m_OriginalStartCol = i_Col;

			int m_Rows = i_GroupInfo.GetGroupedRows();
			
			if(i_GroupInfo.GroupResults!=null)
			{//if not section group , sort group result
				if(!(i_GroupInfo is SectionGroupInfo)) i_GroupInfo.GroupResults.Sort(i_GroupInfo.Sorting,i_GroupInfo.SortingBy);
			}

			for(int m_row = 0; m_row < m_Rows; m_row++)
			{
				GroupResult m_GroupResult = i_GroupInfo.GroupResults[m_row];

				#region set summaryies
				if(i_GroupInfo.FollowSummaries)
				{
					if(m_GroupResult.Summaries!=null)
					{
						foreach(GroupSummary m_Summary in m_GroupResult.Summaries)
						{
							//if group have click event , summaries also need.
							if(m_GroupResult.ClickEvent==ClickEvents.PlayVideo)
							{
                                WebbTableCellHelper.SetCellValueWithClickEvent(this.PrintingTable,i_Row, i_Col, m_Summary); //03-10-2008@Scott
							}
							else
							{
                                WebbTableCellHelper.SetCellValue(this.PrintingTable, i_Row, i_Col, m_Summary);
							}

							i_Col++;
						}
					}
				}
				#endregion

				//Set grouped value
				#region set grouped value
				if(m_GroupResult.ClickEvent==ClickEvents.PlayVideo)
				{
                    WebbTableCellHelper.SetCellValueWithClickEvent(this.PrintingTable, i_Row, i_Col, m_GroupResult.GroupValue, FormatTypes.String, m_GroupResult.RowIndicators);
				}
				else
				{
                    WebbTableCellHelper.SetCellValue(this.PrintingTable, i_Row, i_Col, m_GroupResult.GroupValue, FormatTypes.String);
				}

				this.ColumnStyleRows.GroupColumns.Add(i_Col);
				#endregion

				//Merge cells for section row
				#region Merge cell for section row
                if (i_GroupInfo.DistinctValues)
				{
					this.PrintingTable.MergeCells(i_Row,i_Row,i_Col,this.PrintingTable.GetColumns()-1);
					
					this.SectionRows.Add(i_Row);
					
					i_Row++;
				}
				#endregion

				//useless
				#region set header for one value per page
				if(i_GroupInfo == this._RootGroupInfo && this.OneValuePerPage && this.HaveHeader)
				{//add break row indicators for page break
					this.BreakRows.Add(i_Row);

					int nHeaderStart = i_Row,nHeaderCount = 0;

					this.SetHeaderValue(ref i_Row);

					nHeaderCount = i_Row - nHeaderStart;

					this.SetHeaderRows(nHeaderStart,nHeaderCount);
				}
				#endregion
				
				#region set summaryies
				if(i_GroupInfo.FollowSummaries)
				{}
				else
				{
					if(m_GroupResult.Summaries!=null)
					{
						foreach(GroupSummary m_Summary in m_GroupResult.Summaries)
						{
							i_Col++;

							//if group have click event , summaries also need.
							if(m_GroupResult.ClickEvent==ClickEvents.PlayVideo)
							{
                                WebbTableCellHelper.SetCellValueWithClickEvent(this.PrintingTable, i_Row, i_Col, m_Summary); //03-10-2008@Scott
							}
							else
							{
                                WebbTableCellHelper.SetCellValue(this.PrintingTable, i_Row, i_Col, m_Summary);
							}
						}
					}
				}
				#endregion

				#region Set sub rows
				if(m_GroupResult.SubGroupInfos.Count > 0)
				{
					int m_StartRow = i_Row;

					int maxRow = 0;

					bool bFirstIn = true;	//06-23-2008@Scott

					foreach(GroupInfo groupInfo in m_GroupResult.SubGroupInfos)
					{
                        if (!i_GroupInfo.DistinctValues || !bFirstIn)	//06-23-2008@Scott
						{
							if(bFirstIn) bFirstIn = false;	//06-23-2008@Scott

							i_Col++;
						}

						this.SetRowsValue(ref i_Row,ref i_Col, groupInfo);

						maxRow = Math.Max(maxRow,i_Row);

						i_Row = m_StartRow;
					}
					if(maxRow > m_StartRow)
					{
						maxRow --;
					}
                    else if (i_GroupInfo.DistinctValues)
					{
						maxRow --;
					}
					i_Row = maxRow;
				}

				if(this.AcrossPage)
				{
					i_Col++;	//06-27-2008@Scott
				}
				else
				{
					i_Row++;

					if(m_row < m_Rows - 1) i_Col = m_OriginalStartCol;
				}
				#endregion
			}

			#region Set total row
//			if(i_GroupInfo.AddTotal&&i_GroupInfo.TotalSummaries!=null)
//			{
//				i_Col = m_OriginalStartCol;
//
//				if(i_GroupInfo.FollowSummaries)
//				{
//					i_Col--;
//				}
//				else
//				{
//					SetCellValue(i_Row,i_Col,i_GroupInfo.TotalTitle,FormatTypes.String);
//				}
//
//				Int32Collection totalIndicators = (i_GroupInfo as FieldGroupInfo).GetTotalIndicators(i_GroupInfo);
//
//				foreach(GroupSummary m_TotalSummary in i_GroupInfo.TotalSummaries)
//				{
//					i_Col++;
//
//					//04-24-2008@Scott
//					while(this.ColumnStyleRows.GroupColumns.Contains(i_Col))
//					{
//						i_Col++;
//					}
//
//					//05-08-2008@Scott
//					switch(m_TotalSummary.SummaryType)
//					{
//						case SummaryTypes.RelatedPercent:
//						case SummaryTypes.GroupPercent:
//							if(m_TotalSummary.Filter.Count == 0) continue;
//							break;
//						case SummaryTypes.FreqAndPercent:
//						case SummaryTypes.FreqAndRelatedPercent:
//							continue;
//						default:
//							break;
//					}
//
//					if(i_GroupInfo.ClickEvent == ClickEvents.PlayVideo)
//					{
//						
//						WebbTableCellHelper.SetCellValueWithClickEvent(i_Row,i_Col,m_TotalSummary);
//					}
//					else
//					{
//						WebbTableCellHelper.SetCellValue(i_Row,i_Col,m_TotalSummary);
//					}
//				}
//				
//				this.TotalRows.Add(i_Row);
//				
//				i_Row++;
//			}
			#endregion
		}

		private void SetRowIndicators(int i_Rows)
		{
			int index = 1;

			for(int m_row = 0; m_row < i_Rows; m_row++)
			{
				if(this._HeaderRows.Contains(m_row) || this._TotalRows.Contains(m_row)) continue;

				this.PrintingTable.GetCell(m_row,0).Text = Webb.Utility.FormatIndicator(index++);
			}
		}

		//05-09-2008@Scott
		private void GetTotalSummaries(GroupSummaryCollection totalSummaries)
		{
			FieldGroupInfo rootGroupInfo = null;

			if(this.RootGroupInfo is SectionGroupInfo)
			{
				rootGroupInfo = this.RootGroupInfo.GroupResults[0].SubGroupInfos[0] as FieldGroupInfo;
			}
			else
			{
				rootGroupInfo = this.RootGroupInfo as FieldGroupInfo;
			}

			rootGroupInfo.GetTotalSummaries(totalSummaries,rootGroupInfo);
		}

		//05-09-2008@Scott
//		private void CalculateAllTotal(System.Data.DataTable i_Table,int i_FilteredRowsCount, Webb.Collections.Int32Collection i_Rows)
//		{
//			this.TotalSummaries.Clear();
//
//			this.GetTotalSummaries(this.TotalSummaries);
//			
//			foreach(GroupSummary summary in this.TotalSummaries)
//			{
//				summary.CalculateResult(i_Table,i_FilteredRowsCount,i_Rows.Count/*08-27-2008@Scott*/,i_Rows);
//			}
//		}

		//05-09-2008@Scott
		private Int32Collection GetAllTotalIndicators()
		{
			Int32Collection totalIndicators = new Int32Collection();

			if(this.RootGroupInfo.GroupResults == null) return totalIndicators;
	
			foreach(GroupResult result in this.RootGroupInfo.GroupResults)
			{
				if(result.RowIndicators == null) continue;

				foreach(int row in result.RowIndicators)
				{
					if(totalIndicators.Contains(row)) continue;

					totalIndicators.Add(row);
				}
			}

			return totalIndicators;
		}
	}
	#endregion
}