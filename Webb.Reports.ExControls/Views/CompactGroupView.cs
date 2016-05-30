/***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:View.cs
 * Author:Country.Wu [EMail:Webb.Country.Wu@163.com]
 * Create Time:10/31/2007 03:11:26 PM
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

using System.ComponentModel.Design;
using System.ComponentModel;
using System.Drawing.Design;

namespace Webb.Reports.ExControls.Views
{
	#region public class CompactGroupView
	[Serializable]
	public class CompactGroupView : ExControlView,IMultiHeader
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
			info.AddValue( "TableHeaders",this.TableHeaders,typeof(HeadersData));

            info.AddValue("SectionFilters", this.SectionFilters, typeof(SectionFilterCollection));//Modified 2008-09-08@Simon

			info.AddValue("SectionFiltersWrapper",this.SectionFiltersWrapper,typeof(SectionFilterCollectionWrapper));
			//Modified at 2009-1-14 17:20:44@Scott

	         info.AddValue("_Matrix",this._Matrix);
			  info.AddValue("_WidthEnable",this._WidthEnable);
            info.AddValue( "_SectionRoot",this._SectionRoot);  //Added this code at 2009-2-6 9:36:59@Simon
			info.AddValue("_MatrixInfo",this._MatrixInfo,typeof(MatrixInfo));
			info.AddValue("_MatrixDisplay",this._MatrixDisplay,typeof(ComputedStyle));
             info.AddValue("_GridInfo",this._GridInfo,typeof(GridInfo));

             info.AddValue("_OurBordersSetting", this._OurBordersSetting, typeof(OurBordersSetting));
  		}

		public CompactGroupView(SerializationInfo info, StreamingContext context):base(info,context)
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
				this._Styles.LoadDefaultStyle();     //08-14-2008@Scott
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
				this.TableHeaders= info.GetValue("TableHeaders",typeof(HeadersData)) as HeadersData;
			}
			catch
			{
				this.TableHeaders=null;
			}

            //added 2008-09-08@Simon
            try
            {
                this.SectionFilters = info.GetValue("SectionFilters", typeof(SectionFilterCollection)) as SectionFilterCollection;
				
//				this.SectionFilters=AdvFilterConvertor.GetSectionFilters(this.SectionFilters);
            }
            catch
            {

            }

			//Modified at 2009-1-14 17:27:16@Scott
			try
			{
				this.SectionFiltersWrapper = info.GetValue("SectionFiltersWrapper",typeof(SectionFilterCollectionWrapper)) as SectionFilterCollectionWrapper;
			}
			catch
			{
				//this.SectionFiltersWrapper = new SectionFilterCollectionWrapper();
			}
			try
			{
				this._SectionRoot = info.GetBoolean("_SectionRoot");
			}
			catch
			{
				this._SectionRoot = false;
			}
			try
			{
				this._MatrixInfo= info.GetValue("_MatrixInfo",typeof(MatrixInfo)) as MatrixInfo;
			}
			catch
			{
			}
			try
			{
				this._Matrix = info.GetBoolean("_Matrix");
			}
			catch
			{
				this._Matrix = false;
			}
			
			try
			{
				this._WidthEnable = info.GetBoolean("_WidthEnable");
			}
			catch
			{
				this._WidthEnable = false;
			}
			try
			{
				this._MatrixDisplay=(ComputedStyle)info.GetValue("_MatrixDisplay",typeof(ComputedStyle));
			}
			catch
			{
				this._MatrixDisplay = ComputedStyle.Group;
			}
			try
			{
				this._GridInfo= info.GetValue("_GridInfo",typeof(GridInfo)) as GridInfo;
			}
			catch
			{
				this._GridInfo=null;
				this._MatrixDisplay = ComputedStyle.Group;
			}
            try
            {
                this._OurBordersSetting = info.GetValue("_OurBordersSetting", typeof(OurBordersSetting)) as OurBordersSetting;
            }
            catch
            {
                _OurBordersSetting = new OurBordersSetting();
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
		protected Int32Collection _RootGroupRows;						//Modified at 2009-2-10 15:08:56@Scott
		protected StyleBuilder.StyleColumnInfo _ColumnStyleRows;		//columns indicators for style builder
		
		protected Int32Collection _ColumnsWidth;
		protected Int32Collection _RowsHight;
		
		protected Styles.ExControlStyles _Styles;
	
		protected int _HeightPerPage;						//05-16-2008@Scott
		protected FieldGroupInfo _OneValuePerPageGroup;		//07-04-2008@Scott	//useless
		
		protected HeadersData TableHeaders=null;     //2008-8-28 9:46:28@simon

		protected bool _SectionRoot=false;   //Added this code at 2009-2-6 9:37:39@Simon
		protected bool _Matrix=false;      //Added this code at 2009-2-6 11:43:49@Simon
		protected MatrixInfo _MatrixInfo=null;
		private bool _WidthEnable=false;
		private bool _HaveData=false;

		private ComputedStyle _MatrixDisplay=ComputedStyle.Group;
		protected GridInfo _GridInfo; 
		protected bool _SepTotal=true;

        protected OurBordersSetting _OurBordersSetting = new OurBordersSetting();

		#endregion

		//Properties
		#region Properties

        public OurBordersSetting OurBordersSetting
        {
            get
            {
                if (_OurBordersSetting == null)
                {
                    _OurBordersSetting = new OurBordersSetting();

                }
                return _OurBordersSetting;
            }
            set
            {
                _OurBordersSetting = value;
            }
        }


		public bool SepTotal
		{
			get
			{
				return this._SepTotal;
			}
			set{this._SepTotal=value;}
		}
		public GridInfo GridInfo
		{
			get{
				if(this._GridInfo==null)this._GridInfo=new GridInfo();
				return this._GridInfo;
			   }
			set{this._GridInfo=value;}
		}
		public ComputedStyle MatrixDisplay
		{
			get{return this._MatrixDisplay;}
			set{
					this._MatrixDisplay=value;

					if(this._MatrixDisplay==ComputedStyle.Grid)
					{
						_CellSizeAutoAdapting=CellSizeAutoAdaptingTypes.OneLine;
					}			
			   }
		}

		public MatrixInfo MatrixInfo
		{
			get{return this._MatrixInfo;}		
			set{this._MatrixInfo=value;}
		}
        public bool WidthEnable
		{
			get{return this._WidthEnable;}		
			set{this._WidthEnable=value;}
		}


		public bool Matrix     //Added this code at 2009-2-6 11:44:40@Simon
		{
			get{return this._Matrix;}
			set{this._Matrix=value;}
		}
		public bool SectionRoot
		{
			get{return this._SectionRoot;}
			set{this._SectionRoot=value;}
		}

		public HeadersData HeadersData       //Added this code at 2009-2-5 15:55:21@Simon
		{
			get{return this.TableHeaders;}
			set{this.TableHeaders=value;}
		}

		public FieldGroupInfo OneValuePerPageGroup
		{
			get{return this._OneValuePerPageGroup;}
			set{this._OneValuePerPageGroup = value;}
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
			set{
				if(this._SectionInOneRow != value)
				{
					if(this.RootGroupInfo is SectionGroupInfo)
					{
						int column = this.ShowRowIndicators ? 1 : 0;

						if(value)
						{
							this.ColumnsWidth.RemoveAt(column);
						}
						else
						{
							this.ColumnsWidth.Insert(column,BasicStyle.ConstValue.CellWidth*2);
						}
					}

					this._SectionInOneRow = value;
				}
			}
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
			set{this._SizeSelfAdapting = value;				
			   }
		}
		public bool HaveHeader
		{
			get{return this._HaveHeader;}
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
		public Int32Collection RootGroupRows	//Modified at 2009-2-10 15:10:40@Scott
		{
			get
			{
				if(this._RootGroupRows == null) this._RootGroupRows = new Int32Collection();
				
				return this._RootGroupRows;
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
			set
			{
				if(this._ShowRowIndicators != value)
				{
					if(value) this.ColumnsWidth.Insert(0,30);
					else this.ColumnsWidth.RemoveAt(0);
				
					this._ShowRowIndicators = value;
				}
			}
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
				if(this._CellSizeAutoAdapting==CellSizeAutoAdaptingTypes.NotUse)
				{
					this.WidthEnable=true;
				}
				else
				{
					this.WidthEnable=false;
				}
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
		public CompactGroupView(CompactGroupingControl i_Control):base(i_Control as ExControl)
		{
			this._RootGroupInfo = new Webb.Reports.ExControls.Data.FieldGroupInfo(Webb.Data.PublicDBFieldConverter.AvialableFields[0].ToString());
			this._RootGroupInfo.ColumnHeading = "New Group";
			this._Fitler = new Webb.Data.DBFilter();
			this._Styles = new ExControlStyles();
            this._Styles.LoadDefaultStyle();    //08-14-2008@Scott
			this._ShowRowIndicators = false;
			this._CellSizeAutoAdapting = CellSizeAutoAdaptingTypes.OneLine;
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
			this._SectionRoot=false;
		}

		//Calculate Compact grouped rows and columns
		#region Calculate Compact grouped rows and columns
		/// <summary>
		/// Calculate grouped columns
		/// </summary>
		/// <returns></returns>
		public int GetGroupedColumns()
		{
			int nCols = 1;

			if(this.RootGroupInfo == null) return nCols;

			if(this._ShowRowIndicators)
			{
				nCols += 1;
			}

			this.RootGroupInfo.ColumnIndex = nCols - 1;

			if(this.RootGroupInfo.Summaries != null)
			{
				foreach(GroupSummary summary in this.RootGroupInfo.Summaries)
				{
					summary.ColumnIndex = nCols;

					nCols ++;
				}
			}

			return nCols;
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

			m_value += 30;

			return m_value;
		}

		private int GetHeaderRowsCount()
		{
			int nRet = 0;

			if(this.TableHeaders!=null&&this.TableHeaders.RowCount>0&&this.TableHeaders.ColCount>0)   //2008-8-29 9:12:31@simon
			{				
				nRet+=this.TableHeaders.RowCount;  //2008-8-29 9:12:37@simon				
			}

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

			i_value += m_TopGroups;	//calculate grouped result rows
			
			if(i_GroupInfo.GroupResults!=null)
			{
				for(int m_index = 0; m_index<m_TopGroups; m_index++ )
				{
					GroupResult m_Result = i_GroupInfo.GroupResults[m_index];

					foreach(GroupInfo groupInfo in m_Result.SubGroupInfos)
					{
						GetSubRows(groupInfo,ref i_value);
					}
				}
			}
		}



		#endregion
		
        
		#region Modified Area 
		private bool RepeatHeader()
		{
			bool repeat=!this.ExControl.Report.Template.OneValuePerPage && !this.ExControl.Report.Template.ControlHeaderOnce;	
			if((this.TableHeaders==null||this.TableHeaders.RowCount<=0)&&!this.HaveHeader)
			{
				repeat=false;
			}
			return repeat;
		}
		#endregion        //End Modify at 2008-10-21 10:10:33@Simon

		public override int CreateArea(string areaName, IBrickGraphics graph)
		{
			if(this.PrintingTable != null)
			{
//				this.PrintingTable.RepeatedHeader = !this.ExControl.Report.Template.OneValuePerPage && !this.ExControl.Report.Template.OnePageReport && this.HaveHeader;	//08-22-2008@Scott
//				if(this.TableHeaders!=null)this.PrintingTable.HeaderCount=this.TableHeaders.RowCount+1;
				#region Modified Area 
						this.PrintingTable.RepeatedHeader=this.RepeatHeader();
				        if(this.TableHeaders!=null)
						{   
							if(this.HaveHeader)
							{
								this.PrintingTable.HeaderCount=this.TableHeaders.RowCount+1;
							}
							else
							{
								this.PrintingTable.HeaderCount=this.TableHeaders.RowCount;
							}
						}
						else
						{
							if(this.HaveHeader)
							{
								this.PrintingTable.HeaderCount=1;
							}
							else
							{
								this.PrintingTable.RepeatedHeader=false;
								this.PrintingTable.HeaderCount=0;
							}
						}
				#endregion        //End Modify at 2008-10-21 9:35:11@Simon

				this.PrintingTable.ExControl=this.ExControl;

				this.PrintingTable.HeightPerPage = this.ExControl.Report.GetHeightPerPage(); //this._HeightPerPage;

				this.PrintingTable.ReportHeaderHeight = this.ExControl.Report.GetReportHeaderHeight();	//report header

				this.PrintingTable.ReportFooterHeight = this.ExControl.Report.GetReportFooterHeight();	//report footer
			}

			if(this.PrintingTable==null) return 0;

			if(this.Matrix)
			{
				int mergedCount=GetMatrixMergedCount(PrintingTable);

				if(!this.ThreeD)
				{
					return this.PrintingTable.CreateMatrixArea(areaName,graph,mergedCount);	//Scott@12082008
				}
				else
				{
					return this.PrintingTable.CreateMatrixArea3D(areaName,graph,mergedCount);
				}
			}
			else
			{
				if(this.ThreeD)
				{
					return this.PrintingTable.CreateArea3D(areaName,graph);	//Scott@12082008
				}
				else
				{
					return this.PrintingTable.CreateArea(areaName,graph);
				}
				
			}
			
		}
		private int GetMatrixMergedCount(WebbTable table)
		{
			if(MatrixInfo!=null)
			{
				if(!MatrixInfo.ShowInOneCol)
				{					
					if(MatrixInfo.CellTotal.ShowTotal)
					{
						return 2;
					}
					else
					{
						return 1;
					}
				}
			}

			int count=1;	 //RowGroup Result

			int index=0;

			if(this.ShowRowIndicators)
			{	
				index=1;				
			}		
	
			int MaxRows=table.GetRows();

            if(index>=table.GetColumns()||table.HeaderCount>=MaxRows)return 1;

			int row=table.HeaderCount;

			IWebbTableCell cell=table.GetCell(row,index);  
          	
			while((cell.MergeType&MergeTypes.Down)!=MergeTypes.Down)
			{
				row++;

				if(row>=MaxRows)break;

				cell=table.GetCell(row,index);  
			}

			if(row>=MaxRows-1)return 1;

			row++;
            
			cell=table.GetCell(row,index);  

			while((cell.MergeType&MergeTypes.Merged)==MergeTypes.Merged)
			{
				count++;

				row++;

				if(row>=MaxRows||(cell.MergeType&MergeTypes.End)==MergeTypes.End)break;

				cell=table.GetCell(row,index);  
			}
			return count;		
			
		}

		//Override members
		#region override members	
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

		/// <summary>
		/// Calculate grouped result
		/// </summary>
		/// <param name="i_Table">data source</param>
		public override void CalculateResult(DataTable i_Table)
		{
			//If have no data source ,clear group struct
			if(i_Table == null)
			{
				this.RootGroupInfo.ClearGroupResult(this._RootGroupInfo);	
			
				_HaveData=false;

				return;
			}

			_HaveData=true;

			if(this.Matrix)   //Added this code at 2009-2-6 14:04:42@Simon
			{
				if(this.RootGroupInfo.Summaries == null)
				{
					this.RootGroupInfo.Summaries=new GroupSummaryCollection();
				}
				if(this.RootGroupInfo.SubGroupInfos == null||this.RootGroupInfo.SubGroupInfos.Count==0)
				{
					this.RootGroupInfo.SubGroupInfos=new GroupInfoCollection();

					this.RootGroupInfo.SubGroupInfos.Add(new FieldGroupInfo(""));
				}
				if(MatrixInfo==null)
				{
					this.MatrixInfo=new MatrixInfo(RootGroupInfo,RootGroupInfo.SubGroupInfos[0],null,null);
				}
				else
				{
                    MatrixInfo tempMatrixInfo = MatrixInfo.CopyFrom(RootGroupInfo, MatrixInfo);

                    this.MatrixInfo = tempMatrixInfo;
					
				}               
				this.MatrixInfo.MatrixDisplay=this.MatrixDisplay;
				this.MatrixInfo.GridInfo.Apply(this.GridInfo);
			}

			if(this.RootGroupInfo.Summaries != null&&!this.Matrix)
			{
				foreach(GroupInfo subGroupInfo in this.RootGroupInfo.SubGroupInfos)
				{	
					if(subGroupInfo.Summaries == null) subGroupInfo.Summaries = new GroupSummaryCollection();
					else subGroupInfo.Summaries.Clear();

					foreach(GroupSummary summary in this.RootGroupInfo.Summaries)
					{
						subGroupInfo.Summaries.Add(summary.Copy());
					}
				}
			}			
			
			//Filter rows
			Webb.Collections.Int32Collection m_Rows = new Int32Collection();	

			if(this.ExControl!=null&&this.ExControl.Report!=null)
			{
				m_Rows=this.ExControl.Report.Filter.GetFilteredRows(i_Table);  //2009-5-25 11:02:57@Simon Add this Code
			}

			m_Rows = this.OneValueScFilter.Filter.GetFilteredRows(i_Table,m_Rows);	//06-04-2008@Scott

			m_Rows = this.RepeatFilter.Filter.GetFilteredRows(i_Table,m_Rows);	 //Added this code at 2008-12-26 12:22:40@Simon

			this.Filter=AdvFilterConvertor.GetAdvFilter(DataProvider.VideoPlayBackManager.AdvReportFilters,this.Filter);    //2009-4-29 11:37:37@Simon Add UpdateAdvFilter

			m_Rows = this.Filter.GetFilteredRows(i_Table,m_Rows);	//06-04-2008@Scott

			m_Rows.CopyTo(this.FilteredRows);

			if(this.Matrix)   //Added this code at 2009-2-6 13:31:57@Simon
			{
				MatrixInfo.CalculateMatrixResult(i_Table,m_Rows);
			}
			else
			{				
				this._RootGroupInfo.CalculateGroupResult(i_Table,m_Rows,m_Rows,this.RootGroupInfo);
			}

				//Calculate Total
				//			Int32Collection totalIndicators = this.GetAllTotalIndicators();
				//
				//			if(this.Total) this.CalculateAllTotal(i_Table,totalIndicators,totalIndicators);
		}

		/// <summary>
		/// Create printing table
		/// </summary>
		/// <returns></returns>
		public override bool CreatePrintingTable()
		{
			if(this._RootGroupInfo==null)
			{
				this.PrintingTable=null;

				return false;
			}

			bool create=false;
			
			if(Matrix)
			{
				create=this.CreateMatrixPrintingTable();
			}
			else
			{
				create=this.CreateCompactPrintingTable();
			}

            this.OurBordersSetting.ChangeTableOutBorders(this.PrintingTable);

			return create;
		}
		#endregion

		private void AdjustHeaderStyle()
		{
			if(this.HeaderRows.Count<=0||this.TableHeaders==null) return ;	
			HeaderRows.Sort();

			this.TableHeaders.SetHeadGridLine(this.PrintingTable,this.HeaderRows);
		
			int MinHeaderRow=HeaderRows[0];

			int MaxHeaderRow=HeaderRows[HeaderRows.Count-1];

			#region UpdateHeadeStyle

			for(int row=MinHeaderRow;row<=MaxHeaderRow;row++)
			{		
				int TableHeaderRow=row-MinHeaderRow;

				if(TableHeaderRow>=this.TableHeaders.RowCount)break;

				for(int col=0;col<this.TableHeaders.ColCount;col++)
				{
					HeaderCell headerCell=this.TableHeaders.GetCell(TableHeaderRow,col);

					IWebbTableCell cell=PrintingTable.GetCell(row,col);	

					if(headerCell==null||cell==null)continue;

					if(headerCell.NeedChangeStyle)
					{
						cell.CellStyle=headerCell.CellStyle.Copy();
					}
				}
			}

			#endregion

			#region Cols In Merge
				
			foreach(int col in this.TableHeaders.ColsToMerge)
			{					
				IWebbTableCell Mergedcell=PrintingTable.GetCell(MaxHeaderRow,col);	
				
				this.PrintingTable.MergeCells(MinHeaderRow,MaxHeaderRow,col,col);

				IWebbTableCell bordercell=PrintingTable.GetCell(MinHeaderRow,col);	
			
				if(this.HaveHeader)
				{   
					bordercell.Text= Mergedcell.Text;

					bordercell.CellStyle.StringFormat=Mergedcell.CellStyle.StringFormat;
				}			

				if((bordercell.CellStyle.StringFormat&StringFormatFlags.DirectionVertical)!=0)
				{
					bordercell.CellStyle.HorzAlignment=HorzAlignment.Far;
					bordercell.CellStyle.VertAlignment=VertAlignment.Center;
				}									
										
			}
			#endregion
		}
		private int GetRealTopCount()
		{
			if(this.TopCount<=0)return TopCount;
			
			int RealTopCount=this.TopCount;
						
			RealTopCount+=this.GetHeaderRowsCount();

			if(this.Total)RealTopCount++;
			
			return RealTopCount;
		}

		private bool CreateCompactPrintingTable()
		{
			int m_Rows = this.GetGroupedRows(); 

			int m_Column = this.GetGroupedColumns();

			if(m_Rows <= 0 || m_Column <= 0)
			{
				this.PrintingTable = null;
				return false;
			}			

			System.Diagnostics.Debug.WriteLine(string.Format("Create print table:{0}X{1}",m_Rows,m_Column));

			#region Modify codes at 2009-4-8 11:04:18@Simon			
			int RealTopCount=this.GetRealTopCount();

			if(RealTopCount > 0 && m_Rows < RealTopCount)
			{
				m_Rows = RealTopCount;
			}	
			#endregion        //End Modify					

			this.PrintingTable = new WebbTable(m_Rows,m_Column);

			//Set value
			this.HeaderRows.Clear();
			this.SectionRows.Clear();
			this.TotalRows.Clear();
			this.BreakRows.Clear();
			this.ColumnStyleRows.Clear();
			this.RootGroupRows.Clear();

			this.SetTableValue();

			StyleBuilder.StyleRowsInfo m_StyleInfo = new Styles.StyleBuilder.StyleRowsInfo(this._HeaderRows,this._SectionRows,this._TotalRows,this.ShowRowIndicators,this.HaveHeader);

            #region TopCount Setting
            if (this.TopCount <= 0)
            {
                Int32Collection containedRows = this.HeaderRows.Combine(HeaderRows, TotalRows);

                this.PrintingTable.DeleteEmptyRows(containedRows, m_StyleInfo, this.ShowRowIndicators);
            }
            else
            {
                this.PrintingTable.DeleteExcrescentRows(RealTopCount);
            }

            m_Rows = this.PrintingTable.GetRows();

            if (m_Rows <= 0)
            {
                this.PrintingTable = null;
                return false;
            }
            #endregion

            
            Int32Collection ignoreRows = this.HeaderRows.Combine(this.HeaderRows,this.SectionRows,this.TotalRows);
			StyleBuilder styleBuilder = new StyleBuilder();
			
			styleBuilder.BuildCompactGroupStyle(this.PrintingTable,m_StyleInfo,this.RootGroupInfo,this.Styles,ignoreRows,this.RootGroupRows);	
			
		    this.AdjustHeaderStyle(); //2009-4-14 15:20:56@Simon Add this Code

			this.ApplyColumnWidthStyle(m_Column);
			this.ApplyRowHeightStyle(m_Rows);

       
            switch (this.CellSizeAutoAdapting)
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
			

			System.Diagnostics.Debug.WriteLine("Create print table completely");

			return true;
		}
        private bool CreateMatrixPrintingTable()
		{
			if(MatrixInfo==null||!_HaveData)
			{
				this.PrintingTable = null;
				return false;
			}   

			int m_Rows = MatrixInfo.GetMatrixGroupedRows(this);
            
			if(this.TableHeaders!=null&&this.TableHeaders.RowCount>0&&this.TableHeaders.ColCount>0)   //2008-8-29 9:12:31@simon
			{				
				m_Rows+=this.TableHeaders.RowCount;  //2008-8-29 9:12:37@simon
				
			}

            int m_Column = MatrixInfo.GetMatrixGroupedColumns(this.ShowRowIndicators);         


			if(m_Rows <= 0 || m_Column <= 0)
			{
				this.PrintingTable = null;
				return false;
			}

			System.Diagnostics.Debug.WriteLine(string.Format("Begin Create print table:{0}X{1}",m_Rows,m_Column));
			if(this.TopCount > 0 && m_Rows < this.TopCount) m_Rows = this.TopCount;
			this.PrintingTable = new WebbTable(m_Rows,m_Column);

            MatrixInfo.SetMatrixColumnWidthAtFirst(this.PrintingTable, this.ShowRowIndicators);


			//Set value
			this.HeaderRows.Clear();
			this.SectionRows.Clear();
			this.TotalRows.Clear();
			this.BreakRows.Clear();
			this.ColumnStyleRows.Clear();
			this.RootGroupRows.Clear();

			this.SetTableValue();

			StyleBuilder.StyleRowsInfo m_StyleInfo = new Styles.StyleBuilder.StyleRowsInfo(this._HeaderRows,this._SectionRows,this._TotalRows,this.ShowRowIndicators,this.HaveHeader);
			Int32Collection ignoreRows = this.HeaderRows.Combine(this.HeaderRows,this.SectionRows,this.TotalRows);
			StyleBuilder styleBuilder = new StyleBuilder();

			if(this.TableHeaders!=null)
			{
				this.TableHeaders.SetHeadGridLine(this.PrintingTable,this.HeaderRows);
			}

			#region Modify codes at 2008-11-4 15:45:04@Simon			
					
			styleBuilder.BuildMatrixGroupStyle(this.PrintingTable,m_StyleInfo,this.MatrixInfo,this.Styles,ignoreRows);	
					
			if(this.TableHeaders!=null&&this.TableHeaders.RowCount>0&&this.TableHeaders.ColCount>0)   //Added this code at 2008-11-6 10:22:40@Simon
			{
				int minh=0;
				int i_Titleindex=0;
				if(this.HeaderRows.Count>0)
				{				
					foreach(int tl in this.HeaderRows)
					{								
						if(i_Titleindex<tl)i_Titleindex=tl;
					}
				}
				if(i_Titleindex>minh)
				{
					foreach(int col in this.TableHeaders.ColsToMerge)
					{					
						IWebbTableCell Mergedcell=PrintingTable.GetCell(i_Titleindex,col);					
						this.PrintingTable.MergeCells(minh,i_Titleindex,col,col);
						IWebbTableCell bordercell=PrintingTable.GetCell(minh,col);					
						if(this.HaveHeader)
						{   
							bordercell.Text= Mergedcell.Text;					   
							bordercell.CellStyle.StringFormat=Mergedcell.CellStyle.StringFormat;
							if((bordercell.CellStyle.StringFormat&StringFormatFlags.DirectionVertical)!=0)
							{
								bordercell.CellStyle.HorzAlignment=HorzAlignment.Far;
								bordercell.CellStyle.VertAlignment=VertAlignment.Center;
							}								
						}				
					}
				}
			}
			#endregion        //End Modify

			this.ApplyColumnWidthStyle(m_Column);
			this.ApplyRowHeightStyle(m_Rows);

			switch(this.CellSizeAutoAdapting)
			{
				case CellSizeAutoAdaptingTypes.NotUse:
					break;
				case CellSizeAutoAdaptingTypes.WordWrap:
					this.PrintingTable.AutoAdjustMatrixSize(this.ExControl.CreateGraphics(),true,false,this.MatrixInfo,this.ShowRowIndicators);
					break;
				case CellSizeAutoAdaptingTypes.OneLine:
					this.PrintingTable.AutoAdjustMatrixSize(this.ExControl.CreateGraphics(),false,false,this.MatrixInfo,this.ShowRowIndicators);
					break;
			}
			
    		System.Diagnostics.Debug.WriteLine("Create print table completely");

			return true;
		}

		private void CheckSectionFilters()
		{//need change
			if(this.ExControl==null) return;
			if(this.ExControl.Report==null) return;
			WebbReport m_WebbReport = this.ExControl.Report as WebbReport;
			if(m_WebbReport==null) return;
			//09-08-2008@Scott modify some code
			if(SectionFilters.Count > 0)
			{
				this.CreateSectionGroupInfo(SectionFilters);

				this._SectionRoot=true;  //Added this code at 2009-2-6 9:48:30@Simon
			}
			else if(m_WebbReport.Template.SectionFilters.Count > 0)
			{
				SectionFilterCollection reportSections=m_WebbReport.Template.SectionFilters;

				AdvFilterConvertor convertor = new AdvFilterConvertor();

				if(DataProvider.VideoPlayBackManager.AdvSectionType!=AdvScoutType.None&&m_WebbReport.Template.ReportScType!=ReportScType.Custom)
				{									    
					ReportScType sctype=AdvFilterConvertor.GetScType(m_WebbReport.Template.ReportScType,DataProvider.VideoPlayBackManager.AdvSectionType);  //2009-6-16 10:18:47@Simon Add this Code	    
					
					reportSections = convertor.GetReportFilters(Webb.Reports.DataProvider.VideoPlayBackManager.AdvReportFilters,sctype);	//add 1-19-2008 scott
				}               

				this.CreateSectionGroupInfo(reportSections);

				this._SectionRoot=true;  //Added this code at 2009-2-6 9:48:30@Simon
			}
			else
			{
//				this.RemoveSectionFilters();
			}
		}
		
		private void RemoveSectionFilters()
		{
			if(this.SectionRoot)
			{
				System.Diagnostics.Debug.Assert(this._RootGroupInfo.SubGroupInfos.Count == 1);

				this._RootGroupInfo = this._RootGroupInfo.SubGroupInfos[0];
			}
		}

		private void CreateSectionGroupInfo(SectionFilterCollection i_Sections)
		{
			if(this.SectionRoot)
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

			this.SectionRoot=true;   //Added this code at 2009-2-6 9:56:30@Simon

			this._RootGroupInfo.ColumnHeading = this.SectionTitle;

            this._RootGroupInfo.DistinctValues = this.SectionInOneRow;
		}

		private void ApplyRowHeightStyle(int m_Rows)
		{
			if(this.RowsHight.Count<=0) return;
			
			int count = Math.Min(this.RowsHight.Count,m_Rows);
			
			for(int m_row = 0;m_row<count;m_row++)
			{
				IWebbTableCell cell = this.PrintingTable.GetCell(m_row,0);
				
				cell.CellStyle.Height = this.RowsHight[m_row];
			}
		}

		private void ApplyColumnWidthStyle(int m_Cols)
		{
			if(this.ColumnsWidth.Count<=0) return;		

            int count = Math.Min(this.ColumnsWidth.Count, m_Cols);

            for (int m_col = 0; m_col < count; m_col++)
			{  
				IWebbTableCell cell = this.PrintingTable.GetCell(0,m_col);

                cell.CellStyle.Width = ColumnsWidth[m_col];
			}
		}

		private void SetTableValue()
		{
			int m_Rows = 0,m_Col =0;
			
			if(this.ShowRowIndicators) m_Col = 1;	//add row indicator columns	

			if(!this.ShowRowIndicators) this.PrintingTable.GetCell(0,0).Text = this.SectionTitle;

			//if(!this.OneValuePerPage&&this.HaveHeader)
			if(!this.OneValuePerPage)   //Modified at 2008-10-21 8:47:39@Simon
			{
				int nHeaderStart = m_Rows,nHeaderCount = 0;

				this.SetHeaderValue(ref m_Rows);	//set header value

				nHeaderCount = m_Rows - nHeaderStart;

				this.SetHeaderRows(nHeaderStart,nHeaderCount);
			}
			
			if(this.Matrix)
			{
				this.MatrixInfo.SetMatrixRowsValue(this.PrintingTable,ref m_Rows,ref m_Col,this.TotalRows);	//set row value
			}
			else
			{				

				this.SetRowsValue(ref m_Rows,ref m_Col,this._RootGroupInfo);	//set row value
			}		

			if(this.ShowRowIndicators)
			{//set row indicator value
				this.SetRowIndicators(this.PrintingTable.GetRows());
			}
		}

		
		public int GetFilterHeaderRow()
		{
			int index=-1;
			if(this.HeaderRows.Count<=0)return -1;  //Modified at 2008-10-20 11:32:16@Simon
			
			foreach(int tl in this.HeaderRows)
			{
				if(index<tl)index=tl;
			}
			return index;
		}
        
		private void SetHeaderRows(int nStartRow,int nHeaderRowCount)
		{
			for(int i = nStartRow; i < nStartRow + nHeaderRowCount; i++)
			{
				if(!HeaderRows.Contains(i))this.HeaderRows.Add(i);
			}
		}

		#region Modify codes at 2008-12-8 15:36:23@Simon	
		public ArrayList GetPrnHeader(out ArrayList formats)
		{
			 ArrayList prnHeaders=new ArrayList();
			 formats=new ArrayList();			

			if(!this.Matrix)
			{				int m_Column = this.GetGroupedColumns();
				for(int i=0;i<m_Column;i++)
				{
					prnHeaders.Add("");
					formats.Add(0);
				}
				int nCol=0;

				if(this._ShowRowIndicators) nCol++;
				if(this.RootGroupInfo!=null)GetHeaderValue(this.RootGroupInfo, prnHeaders,formats,ref nCol);
			}
			else if(this.MatrixInfo!=null)
			{
                int m_Column = MatrixInfo.GetMatrixGroupedColumns(this.ShowRowIndicators);
				for(int i=0;i<m_Column;i++)
				{
					prnHeaders.Add("");
					formats.Add(0);
				}
				int nCol=0;

				if(this._ShowRowIndicators) nCol++;
                MatrixInfo.GetHeaderValue(prnHeaders,formats,ref nCol);
			}
 
			return prnHeaders;
		}
		private void GetHeaderValue(GroupInfo groupInfo, ArrayList prnHeaders,ArrayList formats, ref int nCol)
		{
			if(groupInfo.FollowSummaries)
			{
				if(groupInfo.Summaries!=null)
				{
					foreach(GroupSummary m_Summary in groupInfo.Summaries)
					{
						prnHeaders[nCol] = m_Summary.ColumnHeading;	
			            formats[nCol]=m_Summary.HeadingFormat;
						nCol++;
					}
				}
			}

            if (!groupInfo.DistinctValues)	//if set OnValuePerRow, don't set group title
			{
				prnHeaders[nCol] =  groupInfo.ColumnHeading;	
				formats[nCol]=groupInfo.HeadingFormat;

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
						prnHeaders[nCol] = m_Summary.ColumnHeading;				
                        formats[nCol]=m_Summary.HeadingFormat;
						nCol++;
					}
				}
			}
		
		}
		#endregion        //End Modify
		
		private void SetHeaderValue(ref int nRow)
		{
			int nCol = 0;
			
			if(this.TableHeaders!=null&&this.TableHeaders.RowCount>0&&this.TableHeaders.ColCount>0)    //2008-8-29 9:12:52@simon
			{				
				this.TableHeaders.SetHeaders(PrintingTable,ref nRow,this);
			}

			if(this._ShowRowIndicators) nCol++;

			if(this.HaveHeader)   //Modified at 2008-10-21 8:49:30@Simon
			{
				this.SetHeaderValue(this._RootGroupInfo, ref nRow, ref nCol);				
			}
		}

		

		private void SetHeaderValue(GroupInfo groupInfo, ref int nRow, ref int nCol)
		{
			if(this.Matrix)   //Added this code at 2009-2-6 14:05:51@Simon
			{
				this.MatrixInfo.SetHeaderValue(this,this.PrintingTable,ref nRow, ref nCol);
			}
			else
			{
				this.PrintingTable.GetCell(nRow, nCol).Text = groupInfo.ColumnHeading;

				nCol++;

				if(groupInfo.Summaries!=null)
				{
					foreach(GroupSummary m_Summary in groupInfo.Summaries)
					{
						this.PrintingTable.GetCell(nRow,nCol).Text = m_Summary.ColumnHeading;

						this.SetSummaryColumnStyle(m_Summary.SummaryType,nCol);	//Set column style

						nCol++;
					}
				}
                nRow++;
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

			int m_OriginalStartRow = i_Row;

			int m_Rows = i_GroupInfo.GetGroupedRows();
			
			if(i_GroupInfo.GroupResults!=null)
			{//if not section group , sort group result
                if (!(i_GroupInfo is SectionGroupInfo)) i_GroupInfo.GroupResults.Sort(i_GroupInfo.Sorting, i_GroupInfo.SortingBy,i_GroupInfo.UserDefinedOrders);
			}

			for(int m_row = 0; m_row < m_Rows; m_row++)
			{
				GroupResult m_GroupResult = i_GroupInfo.GroupResults[m_row];

				if(i_GroupInfo == this.RootGroupInfo) this.RootGroupRows.Add(i_Row);	//Modified at 2009-2-10 15:13:52@Scott

				//Set grouped value
				#region set grouped value

                string groupValue = string.Empty;

                if (m_GroupResult.GroupValue != null)
                {
                    groupValue = m_GroupResult.GroupValue.ToString();

                    object objValue = CResolveFieldValue.GetResolveValue(i_GroupInfo, m_GroupResult.GroupValue);

                    groupValue = objValue.ToString();        

                    if (i_GroupInfo is FieldGroupInfo)
                    {
                        string followWith = (i_GroupInfo as FieldGroupInfo).FollowsWith;

                        if (followWith != string.Empty && followWith.IndexOf("[VALUE]") >= 0)
                        {
                            groupValue = followWith.Replace("[VALUE]", groupValue);
                        }
                    }
                }

				if(m_GroupResult.ClickEvent==ClickEvents.PlayVideo)
				{
                    WebbTableCellHelper.SetCellValueWithClickEvent(this.PrintingTable, i_Row, i_Col, groupValue, FormatTypes.String, m_GroupResult.RowIndicators);
				}
				else
				{
                    WebbTableCellHelper.SetCellValue(this.PrintingTable, i_Row, i_Col, groupValue, FormatTypes.String);
				}

				this.ColumnStyleRows.GroupColumns.Add(i_Col);
				#endregion
				
				#region set summaryies
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
				#endregion

				#region Set sub rows
				if(m_GroupResult.SubGroupInfos.Count > 0)
				{
					foreach(GroupInfo groupInfo in m_GroupResult.SubGroupInfos)
					{
						i_Row++;
						
						i_Col = 0;

						if(this._ShowRowIndicators)i_Col++;

						this.SetRowsValue(ref i_Row,ref i_Col, groupInfo);
					}
				}

				i_Row++;

				if(m_row < m_Rows - 1) i_Col = m_OriginalStartCol;
				#endregion
			}

			i_Row ++;

			if(i_GroupInfo.AddTotal)
			{
				i_Col = 0;

				WebbTableCellHelper.SetCellValue(this.PrintingTable, i_Row, i_Col, i_GroupInfo.TotalTitle, FormatTypes.String);

				Int32Collection totalIndicators = (i_GroupInfo as FieldGroupInfo).GetTotalIndicators(i_GroupInfo);

				foreach(GroupSummary m_TotalSummary in i_GroupInfo.TotalSummaries)
				{
					i_Col++;

//					while(this.ColumnStyleRows.GroupColumns.Contains(i_Col))
//					{
//						i_Col++;
//					}

					switch(m_TotalSummary.SummaryType)
					{
						case SummaryTypes.RelatedPercent:
						case SummaryTypes.GroupPercent:
							if(m_TotalSummary.Filter.Count == 0) continue;
							break;
						case SummaryTypes.FreqAndPercent:
						case SummaryTypes.FreqAndRelatedPercent:
							continue;
						default:
							break;
					}

					if(i_GroupInfo.ClickEvent == ClickEvents.PlayVideo)
					{
						WebbTableCellHelper.SetCellValueWithClickEvent(this.PrintingTable, i_Row, i_Col, m_TotalSummary);
					}
					else
					{
						WebbTableCellHelper.SetCellValue(this.PrintingTable, i_Row, i_Col, m_TotalSummary);
					}
				}
				
				this.TotalRows.Add(i_Row);
				
				i_Row++;
			}
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

		//Modified at 2009-2-1 16:00:03@Scott
		private void CalculateAllTotal(System.Data.DataTable i_Table,Webb.Collections.Int32Collection i_OuterRows, Webb.Collections.Int32Collection i_InnerRows)
		{
			this.TotalSummaries.Clear();

			this.GetTotalSummaries(this.TotalSummaries);

			Int32Collection totalIndicators = new Int32Collection();
			i_InnerRows.CopyTo(totalIndicators);

			if(this.RootGroupInfo is FieldGroupInfo && this.RootGroupInfo.SubGroupInfos.Count == 0)
			{
				FieldGroupInfo fieldGroupInfo = this.RootGroupInfo as FieldGroupInfo;

				totalIndicators =  fieldGroupInfo.GetTotalIndicators(fieldGroupInfo);
			}
			
			foreach(GroupSummary summary in this.TotalSummaries)
			{
				//summary.CalculateResult(i_Table,i_OuterRows,i_InnerRows/*08-27-2008@Scott*/,i_InnerRows);

				switch(summary.SummaryType)
				{
					case SummaryTypes.Percent:
						summary.CalculateResult(i_Table,i_OuterRows,totalIndicators/*08-27-2008@Scott*/,totalIndicators);
						break;
					case SummaryTypes.FreqAndPercent:
					case SummaryTypes.FreqAndRelatedPercent:
						summary.Value = 0;
						break;
					default:
						summary.CalculateResult(i_Table,totalIndicators,totalIndicators/*08-27-2008@Scott*/,totalIndicators);
						break;
				}
			}
		}

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

        #region Only For CCRM Data
        public void GetALLUsedFields(ref ArrayList usedFields)
        {
            if (this._RootGroupInfo != null)
            {
                this.RootGroupInfo.GetAllUsedFields(ref usedFields);
            }

            this.Filter.GetAllUsedFields(ref usedFields);

            this.SectionFiltersWrapper.GetAllUsedFields(ref usedFields);
        }
        #endregion
   	}
	#endregion
}
