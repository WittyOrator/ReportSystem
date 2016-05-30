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
using System.Security.Permissions;
using System.ComponentModel;

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
using Webb.Collections;
using Webb.Reports.DataProvider;
using Webb.Data;
using Webb.Reports.ExControls.Styles;

using DevSides = DevExpress.XtraPrinting.BorderSide;

namespace Webb.Reports.ExControls.Views
{   
	/// <summary>
	/// Summary description for HorizontalGridView.
	/// </summary>
	[Serializable]
	public class HorizontalGridView : ExControlView,IMultiHeader
	{
		#region ISerializable Members
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData (info, context);
			info.AddValue("Styles",this._Styles,typeof(ExControlStyles));
			info.AddValue("Filters",this._Filter,typeof(DBFilter));
			info.AddValue("ColumnsWidth",this._ColumnsWidth,typeof(Int32Collection));
			info.AddValue("RowsHight",this._RowsHight,typeof(Int32Collection));
			info.AddValue("ShowRowIndicators",this._ShowRowIndicators);
			info.AddValue("GridInfo",this._GridInfo,typeof(GridInfo));
            info.AddValue("HorizontalGridData", this._HorizontalGridData, typeof(HorizontalGridData));
			info.AddValue("_CellSizeAutoAdapting",this._CellSizeAutoAdapting,typeof(CellSizeAutoAdaptingTypes));
			info.AddValue("_RootGroupInfo",this._RootGroupInfo,typeof(FieldGroupInfo));
			info.AddValue("_Total",this._Total);
			info.AddValue("_HeightPerPage",this._HeightPerPage);
			info.AddValue("HeaderRows",this._HeaderRows,typeof(Int32Collection));
			//info.AddValue("SectionRows",this._SectionRows,typeof(Int32Collection));
			info.AddValue("_TotalTitle",this._TotalTitle);   //09-07-2008
			info.AddValue("TableHeaders",this.TableHeaders,typeof(HeadersData));
			info.AddValue("_diffColumns",this._diffColumns);   //09-07-2008
          
            info.AddValue("_WrapColumns", this._WrapColumns);   //Add this code at 2011-2-9 13:45:49@simon

			#region Modified Area 
			info.AddValue("_HaveHeader",this._HaveHeader);
			info.AddValue("_TopCount",this._TopCount);
			#endregion        //End Modify at 2008-9-23 16:37:11@Simon
			
			info.AddValue("SectionFilters", this.SectionFilters, typeof(SectionFilterCollection));  //Modified at 2008-11-18 10:38:24@Scott

			info.AddValue("SectionFiltersWrapper",this.SectionFiltersWrapper,typeof(SectionFilterCollectionWrapper));   //Modified at 2009-1-14 17:20:44@Scott

			info.AddValue("_ColumnafterGroup",this._ColumnafterGroup);

            info.AddValue("_OurBordersSetting", this._OurBordersSetting,typeof(OurBordersSetting));

            info.AddValue("_UseNewMethods", this._UseNewMethods);

            info.AddValue("GridColumns", this._GridColumns, typeof(HorizontalGridColumnCollection));
		}

		public HorizontalGridView(SerializationInfo info, StreamingContext context) : base(info, context)
		{  
            try
            {
                this._OurBordersSetting = info.GetValue("_OurBordersSetting", typeof(OurBordersSetting)) as OurBordersSetting;
            }
            catch
            {
                this._OurBordersSetting = new OurBordersSetting();
            }

			#region Modified Area
			try
			{
				this._ColumnafterGroup = info.GetBoolean("_ColumnafterGroup");
			}
			catch
			{
				this._ColumnafterGroup = false;
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
				this._TopCount = info.GetInt32("_TopCount");
			}
			catch
			{
				this._TopCount = 0;
			}
			#endregion        //End Modify at 2008-9-23 16:36:07@Simon

            try
            {
                this._WrapColumns = info.GetInt32("_WrapColumns");
            }
            catch
            {
                this._WrapColumns = 1;
            }

			try
			{
				this._diffColumns = info.GetInt32("_diffColumns");
			}
			catch
			{
				this._diffColumns =2;
			}
            

			try
			{
				this._Styles = info.GetValue("Styles",typeof(ExControlStyles)) as ExControlStyles;
			}
			catch
			{
				this._Styles = new ExControlStyles();
                this._Styles.LoadDefaultStyle();    //08-14-2008@Scott
			}

			try
			{
				this._Filter = info.GetValue("Filters",typeof(DBFilter)) as DBFilter;

				this._Filter=AdvFilterConvertor.GetAdvFilter(DataProvider.VideoPlayBackManager.AdvReportFilters,this._Filter);    //2009-4-29 11:37:37@Simon Add UpdateAdvFilter
			}
			catch
			{
				this._Filter = new DBFilter();
			}

			try
			{
				this._ColumnsWidth = info.GetValue("ColumnsWidth",typeof(Int32Collection)) as Int32Collection;
			}
			catch
			{
				this._ColumnsWidth = new Int32Collection();
			}

			try
			{
				this._RowsHight = info.GetValue("RowsHight",typeof(Int32Collection)) as Int32Collection;
			}
			catch
			{
				this._RowsHight = new Int32Collection();
			}

			try
			{
				this._ShowRowIndicators = info.GetBoolean("ShowRowIndicators");
			}
			catch
			{
				this._ShowRowIndicators = false;
			}

			try
			{
				this._GridInfo = info.GetValue("GridInfo",typeof(GridInfo)) as GridInfo;
			}
			catch
			{
				this._GridInfo = new GridInfo();
			}

            try
            {
                this._HorizontalGridData = info.GetValue("HorizontalGridData", typeof(HorizontalGridData)) as HorizontalGridData;
            }
            catch
            {
                this._HorizontalGridData = new HorizontalGridData();
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
				this._RootGroupInfo = info.GetValue("_RootGroupInfo",typeof(GroupInfo)) as GroupInfo;

                if (this._RootGroupInfo != null)
                {
                    if(_RootGroupInfo.IsSectionOutSide)
                    {
                        this.RemoveSectionFilters();
                    }
                    _RootGroupInfo.SetAllSignToFalse(_RootGroupInfo);
                }

			}
			catch
			{
				//Root group info in this control could be null
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
				this._HeightPerPage = info.GetInt32("_HeightPerPage");
			}
			catch
			{
				this._HeightPerPage = 0;
			}

			try
			{
				this._HeaderRows = info.GetValue("HeaderRows",typeof(Int32Collection)) as Int32Collection;
			}
			catch
			{
				this._HeaderRows = new Int32Collection();
			}

			//			try
			//			{
			//				this._SectionRows = info.GetValue("SectionRows",typeof(Int32Collection)) as Int32Collection;
			//			}
			//			catch
			//			{
			//				this._SectionRows = new Int32Collection();
			//			}

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
				this.TableHeaders= info.GetValue("TableHeaders",typeof(HeadersData)) as HeadersData;
			}
			catch
			{
				this.TableHeaders=null;
			}

			//Modified at 2008-11-18 10:39:14@Scott
			try
			{
				this.SectionFilters = info.GetValue("SectionFilters", typeof(SectionFilterCollection)) as SectionFilterCollection;     			
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
                this._UseNewMethods = info.GetBoolean("_UseNewMethods");
            }
            catch
            {
                _UseNewMethods = false;
               
                if (this.GridInfo != null)
                {
                    this.GridInfo.ResetGroupInfo(this.RootGroupInfo);
                }
                
            }

            try
            {
                this._GridColumns = info.GetValue("GridColumns", typeof(HorizontalGridColumnCollection)) as HorizontalGridColumnCollection;
            }
            catch
            {
                this._GridColumns = new HorizontalGridColumnCollection();
            }
		}
		#endregion

		//ctor
        public HorizontalGridView(HorizontalGridControl i_Control)
            : base(i_Control as ExControl)
		{
			this._HeaderRows = new Int32Collection();
			this._SectionRows = new Int32Collection();
			this._TotalRows = new Int32Collection();
			this._ColumnsWidth = new Int32Collection();
			this._RowsHight = new Int32Collection();
			this._Styles = new ExControlStyles();
            this._Styles.LoadDefaultStyle();    //08-14-2008@Scott
			this._Filter = new DBFilter();
			this._ShowRowIndicators = false;
			this._GridInfo = new GridInfo();
            this._HorizontalGridData = new HorizontalGridData();
            this._GridColumns = new HorizontalGridColumnCollection();   // 10-12-2011 Scott
			this._CellSizeAutoAdapting = CellSizeAutoAdaptingTypes.WordWrap;
			this._Total = false;
			this._HeightPerPage = 0;

			this._HaveHeader = true;
			this._TopCount=0;
			_diffColumns=3;
			_ColumnafterGroup=false;
            this._WrapColumns = 1;
        }

        #region Field & property

        protected bool _UseNewMethods = true;

        protected int _WrapColumns = 1;
        public int WrappedColumns
        {
            get {
                    if(this._WrapColumns<1)_WrapColumns = 1;
                   return this._WrapColumns; }
            set { this._WrapColumns = value; }
        }        

        protected OurBordersSetting _OurBordersSetting = new OurBordersSetting();
        public OurBordersSetting OurBordersSetting
        {
            get
            {
                if (_OurBordersSetting == null) _OurBordersSetting = new OurBordersSetting();
                return this._OurBordersSetting;
            }
            set { this._OurBordersSetting = value; }
        }  

        protected int _diffColumns=2;

		public int diffColumns
		{
			get{return _diffColumns;}
			set{

					if(this._ColumnafterGroup)
					{
						int newValue=value>0?value:0;

                        int oldValue=_diffColumns>0?_diffColumns:0;

						int minusCount=0;

						this.GeDiffCol(ref minusCount,this.RootGroupInfo,newValue,oldValue);

						int insertPosition=this._ShowRowIndicators?1:0;

						if(GridInfo.SortingFrequence==SortingFrequence.ShowBeforeColumns)
						{
							insertPosition++;
						}

						if(minusCount>0)
						{
							for(int i=0;i<minusCount;i++)
							{
								if(ColumnsWidth.Count>insertPosition)this.ColumnsWidth.Insert(insertPosition,55);
							}

						}
						else
						{
							for(int i=minusCount;i<0;i++)
							{
								if(ColumnsWidth.Count>insertPosition)this.ColumnsWidth.RemoveAt(insertPosition);
							}
						}
					}
				    
				   _diffColumns=value;

					if(_diffColumns<=0)
					{
						_ColumnafterGroup=false;
					}			
			   }
		}

		protected bool _ColumnafterGroup=false;

		public bool ColumnafterGroup
		{
			get{return _ColumnafterGroup;}
			set
			{
				if(this._ColumnafterGroup != value&&_diffColumns>0)
				{
                    int cols = this.ResolveIndentStartCol();

					int insertPosition=this._ShowRowIndicators?1:0;

					if(GridInfo.SortingFrequence==SortingFrequence.ShowBeforeColumns)
					{
						insertPosition++;
					}
				
					if(value) 
					{
						for(int i=0;i<cols;i++)
						{
							if(ColumnsWidth.Count>insertPosition)this.ColumnsWidth.Insert(insertPosition,55);
						}
					}
					else
					{
						for(int i=0;i<cols;i++)
						{
							if(ColumnsWidth.Count>insertPosition)this.ColumnsWidth.RemoveAt(insertPosition);
						}
					}
				
					this._ColumnafterGroup = value;
				}
			}
		}

		protected HeadersData TableHeaders=null;     //2008-8-28 9:46:28@simon
		public HeadersData HeadersData       //Added this code at 2009-2-5 15:55:21@Simon
		{
			get{return this.TableHeaders;}
			set{this.TableHeaders=value;}
		}
        
		protected int _HeightPerPage;
		public int HeightPerPage
		{
			get{return this._HeightPerPage;}
			set{this._HeightPerPage = value;}
		}
       
		//Modified at 2008-9-23 16:40:16@Simon
		protected bool _HaveHeader;
		public bool HaveHeader
		{
			get{return this._HaveHeader;}
			set{this._HaveHeader = value;}
		}

        protected int _TopCount; 
		public int TopCount
		{
			get{return this.GridInfo.TopCount;}
			set{this.GridInfo.TopCount = value < 0 ? 0 : value;}
		}
        //Modified at 2008-9-23 16:40:12@Simon

		protected Int32Collection _GroupRows;
		public Int32Collection GroupRows
		{
			get
			{
				if(this._GroupRows == null) this._GroupRows = new Int32Collection();

				return this._GroupRows;
			}
			set
			{
				this._GroupRows = value;
			}
		}

		protected GroupInfo _RootGroupInfo;
		public GroupInfo RootGroupInfo
		{
			get{return this._RootGroupInfo;}
			set{this._RootGroupInfo = value;}
		}

		protected CellSizeAutoAdaptingTypes _CellSizeAutoAdapting;
		public CellSizeAutoAdaptingTypes CellSizeAutoAdapting
		{
			get{return this._CellSizeAutoAdapting;}
			set{this._CellSizeAutoAdapting = value;}
		}

		protected GridInfo _GridInfo;
		public GridInfo GridInfo
		{
			get{return this._GridInfo;}
		}

        // 10-12-2011 Scott
        protected HorizontalGridColumnCollection _GridColumns;
        public HorizontalGridColumnCollection GridColumns
        {
            get { return this._GridColumns; }
            set { this._GridColumns = value; }
        }

        // 10-13-2011 Scott
        protected HorizontalGridData _HorizontalGridData;
        public HorizontalGridData HorizontalGridData
        {
            get { return _HorizontalGridData; }
        }

		protected bool _ShowRowIndicators;
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

		protected Int32Collection _HeaderRows;
		public Int32Collection HeaderRows
		{
			get
			{
				if(this._HeaderRows == null) this._HeaderRows = new Int32Collection();

				return this._HeaderRows;
			}
			set{this._HeaderRows = value;}
		}

		protected Int32Collection _SectionRows;
		public Int32Collection SectionRows
		{
			get
			{
				if(this._SectionRows == null) this._SectionRows = new Int32Collection();

				return this._SectionRows;
			}
			set{this._SectionRows = value;}
		}

		protected Int32Collection _TotalRows;
		public Int32Collection TotalRows
		{
			get
			{
				if(this._TotalRows == null) this._TotalRows = new Int32Collection();

				return this._TotalRows;
			}
			set{this._TotalRows = value;}
		}

		protected Int32Collection _ColumnsWidth;
		public Int32Collection ColumnsWidth
		{
			get{return this._ColumnsWidth;}
			set{this._ColumnsWidth = value;}
		}

		protected Int32Collection _RowsHight;
		public Int32Collection RowsHight
		{
			get{return this._RowsHight;}
			set{this._RowsHight = value;}
		}

		protected DBFilter _Filter;	
		public DBFilter Filter
		{
			get{return this._Filter;}
			set{this._Filter = value.Copy();}
		}

		protected Styles.ExControlStyles _Styles;
		public Styles.ExControlStyles Styles
		{
			get{return this._Styles;}
		}

		protected bool _Total;
		public bool Total
		{
			get{return this._Total;}
			set{this._Total = value;}
		}

		protected Int32Collection _FilteredRows;		//NonSerializable
		public Int32Collection FilteredRows
		{
			get
			{
				if(this._FilteredRows == null) this._FilteredRows = new Int32Collection();

				return this._FilteredRows;
			}
        }

        #endregion

        [NonSerialized]
        protected Int32Collection _RowsNeedToDelete = new Int32Collection();

        [NonSerialized]
        protected GroupInfoCollection VerticalGroupInfoInCols = new GroupInfoCollection();

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

        protected string _TotalTitle="Total";	//NonSerializable
		public string TotalTitle
		{
			get{return this._TotalTitle;}
			set{this._TotalTitle = value;}
		}

		#region Modified Area 
		private bool RepeatHeader()
		{
			bool repeat=!this.ExControl.Report.Template.OneValuePerPage && !this.ExControl.Report.Template.ControlHeaderOnce ;	
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
//				if(this.TableHeaders!=null)this.PrintingTable.HeaderCount=this.TableHeaders.RowCount+1;   //origianl code at 2008-10-20 10:12:23
//				this.PrintingTable.RepeatedHeader = !this.ExControl.Report.Template.OneValuePerPage && !this.ExControl.Report.Template.OnePageReport && (this.HeaderRows.Count > 0) && this.HaveHeader;	    //origianl code at 2008-10-20 10:12:23
				
				#region add this code for hide original headers at 2008-10-20 10:15:27@Simon
				
				this.PrintingTable.RepeatedHeader =this.RepeatHeader() ;

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
					}

				}
			
				#endregion        //End Modify 

				this.PrintingTable.ExControl=this.ExControl;

				this.PrintingTable.HeightPerPage = this.ExControl.Report.GetHeightPerPage(); //this._HeightPerPage
			
				this.PrintingTable.ReportHeaderHeight = this.ExControl.Report.GetReportHeaderHeight();	//report header

				this.PrintingTable.ReportFooterHeight = this.ExControl.Report.GetReportFooterHeight();	//report footer
			}

			return base.CreateArea (areaName, graph);
		}

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
				else
				{
					bordercell.CellStyle.HorzAlignment=HorzAlignment.Center;
					bordercell.CellStyle.VertAlignment=VertAlignment.Center;
				}					
			}
			#endregion
		}

		private int GetRealTopCount()
		{			
			if(this.TopCount<=0)return TopCount;

            int count=this.GetHeaderRowsCount();

            count += this.TopCount * this.WrappedColumns;						
			
			if(this.Total)count++;

            return count;
		}

        public bool CustomColumn
        {
            get
            {
                return GridColumns != null && GridColumns.Count > 0;
            }
        }

		public override bool  CreatePrintingTable()
		{
            //if (this.VerticalGroupInfoInCols == null) VerticalGroupInfoInCols = new GroupInfoCollection();
            //else VerticalGroupInfoInCols.Clear();   // Groups which are displayed in a column

			int nRows = this.GetRowsCount();
			
			#region Modify codes at 2009-4-8 11:04:18@Simon			
            //int RealTopCount = this.GetRealTopCount();

            //if (RealTopCount > 0 && nRows < RealTopCount)
            //{
            //    nRows = RealTopCount;
            //}	
			#endregion        //End Modify
            
			int nColumns = this.GetColumnsCount();

			if(nColumns == 0 || nRows == 0) 
			{   
				this.PrintingTable=null;
				return false;
			}

			this.PrintingTable = new WebbTable(nRows,nColumns);

            _RowsNeedToDelete = new Int32Collection();

			this.SetTableValue();

            StyleBuilder.StyleRowsInfo m_StyleInfo = new Styles.StyleBuilder.StyleRowsInfo(this.HeaderRows, this.SectionRows, this.TotalRows, this.ShowRowIndicators, this.HaveHeader);

            #region Top Count /Limit Rows

            ////if (RealTopCount <= 0)
            ////{
            ////    Int32Collection rowsToDeletes= this.HeaderRows.Combine(this.HeaderRows, this.SectionRows, this.TotalRows);

            ////    this.PrintingTable.DeleteEmptyRows(rowsToDeletes, m_StyleInfo, this.ShowRowIndicators);

            ////}          
            ////else
            ////{
            ////    this.PrintingTable.DeleteExcrescentRows(RealTopCount, this._RowsNeedToDelete, m_StyleInfo);
            ////}

            ////nRows = this.PrintingTable.GetRows();

            ////if (nRows <= 0)
            ////{
            ////    this.PrintingTable = null;
            ////    return false;
            ////}
            #endregion            

            Int32Collection ignoreRows = this.HeaderRows.Combine(this.HeaderRows, this.SectionRows, this.TotalRows); 

           	StyleBuilder styleBuilder = new StyleBuilder();	

			styleBuilder.BuildGridStyle(this.PrintingTable,m_StyleInfo,this.GridInfo,this.VerticalGroupInfoInCols,this.Styles,ignoreRows);  

			this.AdjustHeaderStyle();			

             if (this.ShowRowIndicators)
            {//set row indicator value
                this.SetRowIndicators(this.PrintingTable.GetRows());
            }

            //int headerRowsCount=this.GetHeaderRowsCount();

            //if (this.WrappedColumns > 1 && this.TopCount > 0)
            //{
            //    this.PrintingTable.SplitTable(this.WrappedColumns, headerRowsCount, this.TopCount);

            //    nRows = this.PrintingTable.GetRows();
            //    nColumns = this.PrintingTable.GetColumns();

            //    if (this.TableHeaders != null)
            //    {
            //        int BeginRow = 0;

            //        this.TableHeaders.SetHeaders(this.PrintingTable, ref BeginRow, this);
            //    }

            //}

            this.ApplyColumnWidthStyle(nColumns);

            this.ApplyRowHeightStyle(nRows);
              
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

            this.OurBordersSetting.ChangeTableOutBorders(this.PrintingTable);

			System.Diagnostics.Debug.WriteLine("Create print table completely");

			return true;
		}

		private void ClearStyleIndicators()
		{
			this.HeaderRows.Clear();
			this.SectionRows.Clear();
			this.TotalRows.Clear();
		}

		private void SetTableValue()
		{
			this.ClearStyleIndicators();	//clear row indicators for style

			int nRow = 0, nStartCol = 0;
          
            if (this.ShowRowIndicators) nStartCol++;

			this.SetHeaderValue(ref nRow, nStartCol);	//set header value

			DataTable dt = this.ExControl.GetDataSource();

			if(dt == null) return;

            //if(this._RootGroupInfo != null)
            //{			
            //    int HeaderStart = this.ResolveIndentStartCol();

            //    GroupRows.Clear();

            //    this.SetRowsValue(dt, ref nRow, nStartCol, this._RootGroupInfo, HeaderStart);
            //}
            //else
            //{
            //    this.SetRowsValueWithNoneGroup(dt, ref nRow, nStartCol, this.FilteredRows);
            //}

			//if(this._Total) this.SetTotalValue();	//04-30-2008@Scott            

            SetRowsValue(dt, ref nRow, nStartCol); 
        }

		private void SetTotalValue()
		{
			int lastRow = this.PrintingTable.GetRows() - 1;

			int lastCol = this.PrintingTable.GetColumns() - 1;

			this.TotalRows.Add(lastRow);

			this.PrintingTable.MergeCells(lastRow,lastRow,0,lastCol);

			WebbTableCell cell = this.PrintingTable.GetCell(lastRow,0) as WebbTableCell;

			if(cell != null)
			{
				cell.Text = string.Format("{0}  {1}",this.TotalTitle,this.FilteredRows.Count);
			}
		}

		private void MergeRowText(int row, int nStartMergeCol, int nEndMergeCol)
		{
			if(row >= this.PrintingTable.GetRows()) return;

			if(nEndMergeCol > this.PrintingTable.GetColumns() - 1) nEndMergeCol = this.PrintingTable.GetColumns() - 1;	//scott@12032008

			if(nStartMergeCol > nEndMergeCol) return;	//scott@12032008

			StringBuilder sb = new StringBuilder();

			for(int col = nStartMergeCol; col <= nEndMergeCol; col++)
			{
				WebbTableCell cell = this.PrintingTable.GetCell(row,col) as WebbTableCell;

				if(cell != null)
				{
					sb.AppendFormat(" {0}",cell.Text);

					cell.Text = string.Empty;
				}
			}

			this.PrintingTable.GetCell(row,nStartMergeCol).Text = sb.ToString().Trim();

			this.PrintingTable.MergeCells(row,row,nStartMergeCol,nEndMergeCol);
		}
       
		#region Set value By GroupInfo
		private void SetRowsValue(DataTable dt, ref int nRow, int nCol, GroupInfo groupInfo,int AvailableColsStart)
		{
			int nStartMergeCol = 0;

			int nOriCol = nCol;

			if(groupInfo.GroupResults == null) return;
          
			//sort group result
            if (!(groupInfo is SectionGroupInfo)) groupInfo.GroupResults.Sort(groupInfo.Sorting, groupInfo.SortingBy, groupInfo.UserDefinedOrders);

			int nTotalCols=this.PrintingTable.GetColumns();            

			for(int i = 0; i < groupInfo.GroupResults.Count; i++)
			{
				if(groupInfo.TopCount>0 && groupInfo.TopCount - 1 < i) return;	//Modified at 2008-11-18 11:06:10@Scott

				GroupResult groupResult = groupInfo.GroupResults[i];

                int nStartCol = this._ShowRowIndicators ? 1 : 0;

				nStartMergeCol = nCol;

                #region Display Group and summaries In One Row

               if (!this.GroupRows.Contains(nRow))
                {
                    this.GroupRows.Add(nRow);
                }
                if (!groupInfo.DisplayAsColumn)
                {  
                    #region Set Group Value

                    StringBuilder sb = new StringBuilder();

                    bool visible = GroupInfo.IsVisible(groupInfo);                 

                    if (visible)   //2010-2-8 14:10:57@Simon Add this Code
                    {
                        #region Visible 
                            #region Append Group Values
                            if (!this.SectionRows.Contains(nRow))
                            {
                                this.SectionRows.Add(nRow);
                            }

                            string FollowsWith = string.Empty;                         

                            if (groupInfo is FieldGroupInfo)
                            {
                                FollowsWith = (groupInfo as FieldGroupInfo).FollowsWith;  //2009-3-31 9:30:37@Simon Add this Code                              
                            }                           

                            object objValue = CResolveFieldValue.GetResolveValue(groupInfo, groupResult.GroupValue); 
                        
                            string groupValue =objValue.ToString(); 

                            sb.Append(groupValue);

                            if (FollowsWith == string.Empty)
                            {
                                if (!groupInfo.FollowSummaries)
                                {
                                    FollowsWith = " ";
                                }
                                else
                                {
                                    FollowsWith = " : ";
                                }
                            }

                            sb.Append(FollowsWith);

                            #endregion

                            #region Append Summary Values
                            if (groupResult.Summaries != null && groupResult.Summaries.Count > 0 && !groupInfo.AddTotal)
                            {
                                bool once = true;

                                foreach (GroupSummary summary in groupResult.Summaries)
                                {
                                    #region Modify codes at 2008-12-3 10:30:56@Simon

                                    string summaryValue = WebbTableCellHelper.FormatValue(null, summary);

                                    if (once)
                                    {
                                        once = false;
                                    }
                                    else
                                    {
                                        sb.Append(" ");
                                    }

                                    string strSummaryHeader = WebbTableCellHelper.ReplaceSpecialString(summary.ColumnHeading);

                                    string strRemoveSpecialChars = strSummaryHeader.Trim(" \t\r\n".ToCharArray());

                                    if (!groupInfo.FollowSummaries)
                                    {
                                        sb.Append(strSummaryHeader);

                                        if (strRemoveSpecialChars != string.Empty) sb.Append(":");

                                        sb.Append(summaryValue);
                                    }
                                    else
                                    {
                                        sb.Append(summaryValue);

                                        if (strRemoveSpecialChars != string.Empty) sb.Append("(");

                                        sb.Append(strSummaryHeader);

                                        if (strRemoveSpecialChars != string.Empty) sb.Append(")");

                                    }
                                    #endregion        //End Modify
                                }
                            }
                            #endregion

                            #region Set Vale

                            WebbTableCell cell = this.PrintingTable.GetCell(nRow, nCol) as WebbTableCell;

                            if (cell != null)
                            {
                                if (groupInfo.ClickEvent == ClickEvents.PlayVideo)
                                {
                                    WebbTableCellHelper.SetCellValueWithClickEvent(this.PrintingTable, nRow, nStartMergeCol, sb.ToString(), FormatTypes.String, groupResult.RowIndicators);
                                }
                                else
                                {
                                    WebbTableCellHelper.SetCellValue(this.PrintingTable, nRow, nStartMergeCol, sb.ToString(), FormatTypes.String);
                                }
                            }
                            #endregion                       

                            #region Merge Columns

                            nCol = nTotalCols - 1;

                            if (nStartMergeCol - 1 > nStartCol && diffColumns > 0)
                            {
                                this.PrintingTable.MergeCells(nRow, nRow, nStartCol, nStartMergeCol - 1);
                            }

                            if (nTotalCols - 1 > nStartMergeCol)
                            {
                                this.PrintingTable.MergeCells(nRow, nRow, nStartMergeCol, nTotalCols - 1);
                            }
                            #endregion
                        #endregion
                    }
                    else
                    {
                        if (!this._RowsNeedToDelete.Contains(nRow)) this._RowsNeedToDelete.Add(nRow);
                    }
                    #endregion                  
                }
                else
                {
                    if (!this._RowsNeedToDelete.Contains(nRow)) this._RowsNeedToDelete.Add(nRow);
                }
                #endregion

                nRow++;

                int startFirstSetRow = nRow;

                int colStart = AvailableColsStart;

                if (groupInfo.DisplayAsColumn)
                {
                    colStart += groupInfo.Summaries.Count;

                    if (GroupInfo.IsVisible(groupInfo))
                    {
                        colStart += 1;
                    }
                }

                #region Set SubRows
                if (groupResult.SubGroupInfos.Count > 0)
				{  					
					nCol =nStartMergeCol+Math.Abs(this.diffColumns);                    

                    this.SetRowsValue(dt, ref nRow, nCol, groupResult.SubGroupInfos[0], colStart);                       
                }
				else
				{//set play by play value

                    if (diffColumns >= 0 && (!groupInfo.DisplayAsColumn))
					{
						this.MergeRowText(nRow-1, nStartMergeCol, this.PrintingTable.GetColumns() - 1);
					}                   

                    if (groupResult.RowIndicators.Count > 0)
                    {
                        if (this.ColumnafterGroup && this.diffColumns > 0)
                        {
                            #region ColumnafterGroup
                            int startMerRow = nRow;

                            int endSetCol = colStart;

                            if (endSetCol < 0) endSetCol = nStartCol;

                            this.SetLeafRowsValue(dt, ref nRow, endSetCol, groupResult.RowIndicators);

                            if (nStartMergeCol - 1 > nStartCol && (!groupInfo.DisplayAsColumn))
                            {
                                if (this.HeaderRows.Count > 0 )
                                {
                                    int row = HeaderRows[this.HeaderRows.Count - 1];

                                    this.PrintingTable.MergeCells(row, row, nStartCol, nStartMergeCol - 1);
                                }

                                for (int row = startMerRow; row < nRow; row++)
                                {
                                    this.PrintingTable.MergeCells(row, row, nStartCol, nStartMergeCol - 1);
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            this.SetLeafRowsValue(dt, ref nRow, colStart, groupResult.RowIndicators);
                        }
                    }
                }
                #endregion         
   
                #region Set First RowValue when Display as column

                if (groupInfo.DisplayAsColumn)
                {
                    int setStartCol = AvailableColsStart;
                  
                    object value =CResolveFieldValue.GetResolveValue(groupInfo,groupResult.GroupValue);
                  
                    for (int k = 0; k < groupResult.RowIndicators.Count; k++)
                    {
                        while (this.GroupRows.Contains(startFirstSetRow)||this.TotalRows.Contains(startFirstSetRow)) startFirstSetRow++;

                        if (startFirstSetRow >= nRow) break;

                        int setCol = setStartCol;

                        if (groupInfo.FollowSummaries)
                        {
                            foreach (GroupSummary groupSummary in groupResult.Summaries)
                            {
                                if (k == 0)
                                {
                                    if (this._GridInfo.ClickEvent == ClickEvents.PlayVideo)
                                    {
                                        WebbTableCellHelper.SetCellValueWithClickEvent(this.PrintingTable, startFirstSetRow, setCol, groupSummary);
                                    }
                                    else
                                    {
                                        WebbTableCellHelper.SetCellValue(this.PrintingTable, startFirstSetRow, setCol, groupSummary);
                                    }
                                }

                                setCol++;
                            }
                        }
                        if (GroupInfo.IsVisible(groupInfo))
                        {
                            #region Set GroupValue
                            if (groupInfo.DisplayAsImage)
                            {
                                WebbTableCellHelper.SetCellImageFromValue(this.PrintingTable, startFirstSetRow, setCol, value);

                                IWebbTableCell firstCell = this.PrintingTable.GetCell(startFirstSetRow, 0);

                                if (firstCell != null) firstCell.CellStyle.Height = this.GridInfo.ImageRowHeight;
                            }
                            else
                            {
                                if (this._GridInfo.ClickEvent == ClickEvents.PlayVideo)
                                {
                                    WebbTableCellHelper.SetCellValueWithClickEvent(this.PrintingTable, startFirstSetRow, setCol, value, FormatTypes.String, groupResult.RowIndicators);
                                }
                                else
                                {
                                    WebbTableCellHelper.SetCellValue(this.PrintingTable, startFirstSetRow, setCol, value, FormatTypes.String);
                                }
                            }
                            #endregion

                            setCol++;
                        }
                        if (!groupInfo.FollowSummaries)
                        {
                            foreach (GroupSummary groupSummary in groupResult.Summaries)
                            {
                                if (k == 0)
                                {
                                    if (this._GridInfo.ClickEvent == ClickEvents.PlayVideo)
                                    {
                                        WebbTableCellHelper.SetCellValueWithClickEvent(this.PrintingTable, startFirstSetRow, setCol, groupSummary);
                                    }
                                    else
                                    {
                                        WebbTableCellHelper.SetCellValue(this.PrintingTable, startFirstSetRow, setCol, groupSummary);
                                    }
                                }
                                setCol++;
                            }
                        }

                        startFirstSetRow++;

                        if (k == 0 && groupInfo.DistinctValues) break;
                    }
                   
                }
                #endregion

				nCol = nOriCol;
			
				//set group total
                if (groupInfo.AddTotal ) 
				{
					this.SetGroupTotalValue(ref nRow,groupResult);
				}	
			}
		}              

		#endregion

		private void SetGroupTotalValue(ref int nRow, GroupResult groupResult)
		{
			GroupInfo groupInfo = groupResult.ParentGroupInfo;

			if(groupInfo == null) return;

			if(groupResult.Summaries != null)
			{
				int index = 0;

				PrintingTable.GetCell(nRow, index++).Text = groupInfo.TotalTitle;

				foreach(GroupSummary summary in groupResult.Summaries)
				{
                    WebbTableCellHelper.SetCellValue(this.PrintingTable, nRow, index++, summary.ColumnHeading, FormatTypes.String);

					if(groupInfo.ClickEvent == ClickEvents.PlayVideo)
					{
                        WebbTableCellHelper.SetCellValueWithClickEvent(this.PrintingTable, nRow, index++, summary, groupResult.RowIndicators);
					}
					else
					{
                        WebbTableCellHelper.SetCellValue(this.PrintingTable, nRow, index++, summary);
					}
				}
				
				this.MergeRowText(nRow,0,this.PrintingTable.GetColumns() - 1);

				this.TotalRows.Add(nRow);
			}
			nRow++;
		}      

        private void SetLeafRowsValue(DataTable dt, ref int nRow, int nStartCol, Int32Collection filteredRows)
        {
            if (filteredRows.Count == 0)return; //Modify this code at 2008-11-26 11:36:51@Simon
           
            int i = 0, j = 0, nCol = 0;  
            
            for (i = 0; i < filteredRows.Count; i++, nRow++)
            {
                nCol = nStartCol;

                int filterRow = filteredRows[i]; 

                for (j = 0; j < this._GridInfo.Columns.Count; j++, nCol++)
                {
                    GridColumn col = this._GridInfo.Columns[j];

                    DataRow dataRow = dt.Rows[filterRow];

                    object value = col.GetValue(dataRow);

                    value = CResolveFieldValue.GetResolveValue(col, value);

                    if (col.DisplayAsImage)
                    {
                        WebbTableCellHelper.SetCellImageFromValue(this.PrintingTable, nRow, nCol, value);

                        IWebbTableCell firstCell = this.PrintingTable.GetCell(nRow, 0);

                        if (firstCell != null) firstCell.CellStyle.Height = this.GridInfo.ImageRowHeight;
                    }
                    else
                    {
                        if (this._GridInfo.ClickEvent == ClickEvents.PlayVideo)
                        {
                            Int32Collection indicators = new Int32Collection();

                            indicators.Add(filterRow);

                            WebbTableCellHelper.SetCellValueWithClickEvent(this.PrintingTable, nRow, nCol, value, FormatTypes.String, indicators);
                        }
                        else
                        {
                            WebbTableCellHelper.SetCellValue(this.PrintingTable, nRow, nCol, value, FormatTypes.String);
                        }
                    }
                }
            }          


        }


        private void SetRowsValueWithNoneGroup(DataTable dt, ref int nRow, int nStartCol, Int32Collection filteredRows)
        {
            if (filteredRows.Count == 0 || this._GridInfo.Columns.Count <= 0) return; //Modify this code at 2008-11-26 11:36:51@Simon

            int i = 0, j = 0, nCol = 0;
         
            //set value
            #region new codes for sorting    //Added this code at 2008-11-26 12:41:37@Simon          ;
            int valueIndexRow = 0;

            GroupResultCollection SortingResults = this._GridInfo.Sorting(dt, filteredRows);	//test     //Added this code at 2008-11-26 11:41:46@Simon

            if (SortingResults != null && SortingResults.Count > 0)
            {
                filteredRows.Clear();

                foreach (GroupResult gr in SortingResults)
                {
                    foreach (int row in gr.RowIndicators)
                    {
                        filteredRows.Add(row);
                    }
                }
            }

            bool b_ExistColumn = (this.RootGroupInfo == null && this.GridInfo.SortingFrequence != SortingFrequence.None);

            b_ExistColumn = (b_ExistColumn && SortingResults != null);

            if (!b_ExistColumn)
            {
                int tempRow = nRow;              
               
                #region not Exist SortingFrequence Columns

                    for (i = 0; i < filteredRows.Count; i++, nRow++)
                    {
                        nCol = nStartCol;

                        valueIndexRow = filteredRows[i];

                        #region Set Rows Value
                        for (j = 0; j < this._GridInfo.Columns.Count; j++, nCol++)
                        {
                            GridColumn col = this._GridInfo.Columns[j];
                           
                            DataRow dataRow = dt.Rows[valueIndexRow];

                            object value = col.GetValue(dataRow);

                            value = CResolveFieldValue.GetResolveValue(col, value);

                            #region Set Image/Value

                            if (col.DisplayAsImage)
                            {
                                WebbTableCellHelper.SetCellImageFromValue(this.PrintingTable, nRow, nCol,  value);

                                IWebbTableCell firstCell = this.PrintingTable.GetCell(nRow, 0);

                                if (firstCell != null) firstCell.CellStyle.Height = this.GridInfo.ImageRowHeight;
                            }
                            else
                            {
                                if (this._GridInfo.ClickEvent == ClickEvents.PlayVideo)
                                {
                                    Int32Collection indicators = new Int32Collection();

                                    indicators.Add(valueIndexRow);

                                    WebbTableCellHelper.SetCellValueWithClickEvent(this.PrintingTable, nRow, nCol, value, FormatTypes.String, indicators);
                                }
                                else
                                {
                                    WebbTableCellHelper.SetCellValue(this.PrintingTable, nRow, nCol, value, FormatTypes.String);
                                }
                            }
                            #endregion
                        }
                        #endregion
                    }
                    #endregion                       
                
            }
            else
            {
                #region  Exist SortingFrequence Columns
                foreach (GroupResult gr in SortingResults)
                {
                    for (i = 0; i < gr.RowIndicators.Count; i++, nRow++)      //modify this code at 2008-11-26 12:45:53@Simon
                    {
                        nCol = nStartCol;

                        valueIndexRow = gr.RowIndicators[i];

                        string strFreqValue = WebbTableCellHelper.FormatValue(gr.RowIndicators.Count, FormatTypes.String);

                        if (this._GridInfo.SortingFrequence == SortingFrequence.ShowBeforeColumns)
                        {
                            if (i == 0)
                            {
                                if (this._GridInfo.ClickEvent == ClickEvents.PlayVideo)
                                {
                                    WebbTableCellHelper.SetCellValueWithClickEvent(this.PrintingTable, nRow, nCol, strFreqValue, FormatTypes.String, gr.RowIndicators);

                                }
                                else
                                {
                                    WebbTableCellHelper.SetCellValue(this.PrintingTable, nRow, nCol, strFreqValue, FormatTypes.String);
                                }
                            }
                            nCol++;
                        }

                        #region set Col values
                        for (j = 0; j < this._GridInfo.Columns.Count; j++, nCol++)
                        {
                            GridColumn col = this._GridInfo.Columns[j];                          
                        
                            DataRow dataRow = dt.Rows[valueIndexRow];

                            object value = col.GetValue(dataRow);

                            value = CResolveFieldValue.GetResolveValue(col, value);

                             #region Set Image/Value

                            if (col.DisplayAsImage)
                            {
                                WebbTableCellHelper.SetCellImageFromValue(this.PrintingTable, nRow, nCol, value);

                                IWebbTableCell firstCell = this.PrintingTable.GetCell(nRow, 0);

                                if (firstCell != null) firstCell.CellStyle.Height = this.GridInfo.ImageRowHeight;
                            }
                            else
                            {

                                if (this._GridInfo.ClickEvent == ClickEvents.PlayVideo)
                                {
                                    Int32Collection indicators = new Int32Collection();

                                    indicators.Add(valueIndexRow);

                                    WebbTableCellHelper.SetCellValueWithClickEvent(this.PrintingTable, nRow, nCol, value, FormatTypes.String, indicators);
                                }
                                else
                                {
                                    WebbTableCellHelper.SetCellValue(this.PrintingTable, nRow, nCol, value, FormatTypes.String);
                                }
                            }
                             #endregion
                        }

                        #endregion

                        if (i == 0 && this._GridInfo.SortingFrequence == SortingFrequence.ShowAfterColumns)
                        {
                            if (this._GridInfo.ClickEvent == ClickEvents.PlayVideo)
                            {
                                WebbTableCellHelper.SetCellValueWithClickEvent(this.PrintingTable, nRow, nCol, strFreqValue, FormatTypes.String, gr.RowIndicators);

                            }
                            else
                            {
                                WebbTableCellHelper.SetCellValue(this.PrintingTable, nRow, nCol, strFreqValue, FormatTypes.String);
                            }
                        }
                    }
                }
                #endregion
            }

            #endregion

        }

        // Horizontal Grid
        private void SetRowsValue(DataTable dt, ref int nRow, int nStartCol)
        {
            if (this.GridInfo.Columns.Count <= 0) return;

            int nCol = nStartCol;

            foreach (GridColumn row in this.GridInfo.Columns)
            {
                WebbTableCellHelper.SetCellValue(this.PrintingTable, nRow, nCol, row.Title, FormatTypes.String);
                nCol++;
                WebbTableCellHelper.SetCellValue(this.PrintingTable, nRow, nCol, row.Units, FormatTypes.String);
                nCol++;

                if (!CustomColumn)
                {
                    foreach(int rowIndicator in this.HorizontalGridData.RowIndicators)
                    {
                        if (dt.Columns.Contains(row.Field) && rowIndicator >= 0)
                        {
                            WebbTableCellHelper.SetCellValue(this.PrintingTable, nRow, nCol, dt.Rows[rowIndicator][row.Field], FormatTypes.String);
                        }

                        nCol++;
                    }
                }
                else
                {
                    foreach (HorizontalGridColumn col in this.GridColumns)
                    {
                        Int32Collection RowIndicators = new Int32Collection();
                        RowIndicators.Add(col.RowIndicator);

                        if (this._GridInfo.ClickEvent == ClickEvents.PlayVideo)
                        {
                            if (dt.Columns.Contains(row.Field) && col.RowIndicator >= 0)
                            {
                                WebbTableCellHelper.SetCellValueWithClickEvent(this.PrintingTable, nRow, nCol, dt.Rows[col.RowIndicator][row.Field], FormatTypes.String, RowIndicators);
                            }
                        }
                        else
                        {
                            if (dt.Columns.Contains(row.Field) && col.RowIndicator >= 0)
                            {
                                WebbTableCellHelper.SetCellValue(this.PrintingTable, nRow, nCol, dt.Rows[col.RowIndicator][row.Field], FormatTypes.String);
                            }
                        }

                        nCol++;
                    }
                }

                nCol = nStartCol;
                nRow++;
            }
        }


		//added this code by front codes for hide Origal field title at 2008-10-20 10:42:59@Simon
		#region Modified Area 
		
		public void GeDiffCol(ref int nCol,GroupInfo groupInfo,int newValue,int OldValue)
		{
			if(groupInfo==null||groupInfo.SubGroupInfos.Count==0)return;
             
			nCol+=newValue-OldValue;

			GeDiffCol(ref nCol,groupInfo.SubGroupInfos[0],newValue,OldValue);
            
		}
        private void SetGroupHeadersInCol(int nRow, ref int nStartCol, GroupInfo groupInfo)
        {
            if (groupInfo == null||!this.HaveHeader) return;

            if (groupInfo.DisplayAsColumn)
            {              
                if (groupInfo.FollowSummaries)
                {
                    foreach (GroupSummary groupSummary in groupInfo.Summaries)
                    {
                        WebbTableCellHelper.SetCellValue(this.PrintingTable, nRow, nStartCol, groupSummary.ColumnHeading, FormatTypes.String);

                        groupSummary.ColumnIndex = nStartCol;

                        IWebbTableCell cell = this.PrintingTable.GetCell(0, nStartCol);

                        if (cell!=null)
                        {
                            cell.CellStyle.Width = groupSummary.ColumnWidth;
                        }

                        nStartCol++;
                    }
                }
                if(GroupInfo.IsVisible(groupInfo))
                {
                    WebbTableCellHelper.SetCellValue(this.PrintingTable, nRow, nStartCol, groupInfo.ColumnHeading, FormatTypes.String);

                    groupInfo.ColumnIndex = nStartCol;

                    IWebbTableCell cell = this.PrintingTable.GetCell(0, nStartCol);

                    if (cell != null)
                    {
                        cell.CellStyle.Width = groupInfo._ColumnWidth;
                    }

                    nStartCol++;
                }
                if (!groupInfo.FollowSummaries)
                {
                    foreach (GroupSummary groupSummary in groupInfo.Summaries)
                    {
                        WebbTableCellHelper.SetCellValue(this.PrintingTable, nRow, nStartCol, groupSummary.ColumnHeading, FormatTypes.String);

                        groupSummary.ColumnIndex = nStartCol;

                        IWebbTableCell cell = this.PrintingTable.GetCell(0, nStartCol);

                        if (cell != null)
                        {
                            cell.CellStyle.Width = groupSummary.ColumnWidth;
                        }

                        nStartCol++;
                    }
                }

                this.VerticalGroupInfoInCols.Add(groupInfo);

            }
            foreach (GroupInfo subgroupInfo in groupInfo.SubGroupInfos)
            {
                SetGroupHeadersInCol(nRow, ref nStartCol, subgroupInfo);
            }
        }

		private void SetHeaderValue(ref int nRow, int nStartCol)
		{	
			#region Modify codes at 2008-12-3 10:45:00@Simon

            //nStartCol = this.ResolveIndentStartCol();

			#endregion        //End Modify

			if(this.TableHeaders!=null&&this.TableHeaders.RowCount>0&&this.TableHeaders.ColCount>0)    //2008-8-29 9:12:52@simon
			{
				this.SetHeaderRows(nRow,this.TableHeaders.RowCount);

				this.TableHeaders.SetHeaders(PrintingTable,ref nRow,this);				
			}

            //SetGroupHeadersInCol(nRow, ref nStartCol, this.RootGroupInfo);
            
            //for(int i = 0;i<this._GridInfo.Columns.Count;i++,nStartCol++)
            //{
            //    GridColumn col = this._GridInfo.Columns[i];

            //    col.ColumnIndex = nStartCol;

            //    IWebbTableCell cell = this.PrintingTable.GetCell(0, nStartCol);

            //    if (cell != null)
            //    {
            //        cell.CellStyle.Width = col.ColumnWidth;
            //    }

            //    if(this.HaveHeader)
            //    {
            //        WebbTableCellHelper.SetCellValue(this.PrintingTable, nRow, nStartCol, col.Title, FormatTypes.String);
            //    }				
            //}

            nStartCol += 2;

            DataTable dt = this.ExControl.GetDataSource();

            if (dt == null)
            {
                return;
            }

            WebbTableCellHelper.SetCellValue(this.PrintingTable, nRow, nStartCol - 2, HorizontalGridData.FieldTitle, FormatTypes.String);
            WebbTableCellHelper.SetCellValue(this.PrintingTable, nRow, nStartCol - 1, HorizontalGridData.UnitsTitle, FormatTypes.String);

            if (!CustomColumn)
            {
                for (int i = 0; i < this.HorizontalGridData.TopCount; i++, nStartCol++)
                {
                    IWebbTableCell cell = this.PrintingTable.GetCell(0, nStartCol);

                    if (this.HaveHeader)
                    {
                        if (HorizontalGridData.SortingField != string.Empty)
                        {
                            if (dt.Columns.Contains(HorizontalGridData.SortingField) && i < HorizontalGridData.RowIndicators.Count)
                            {
                                object objValue = CResolveFieldValue.GetResolveValue(HorizontalGridData.SortingField, HorizontalGridData.DateFormat, dt.Rows[HorizontalGridData.RowIndicators[i]][HorizontalGridData.SortingField]);

                                WebbTableCellHelper.SetCellValue(this.PrintingTable, nRow, nStartCol, objValue, FormatTypes.String);
                            }
                        }
                        else
                        {
                            
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < this.GridColumns.Count; i++, nStartCol++)
                {
                    HorizontalGridColumn col = this.GridColumns[i];

                    col.ColumnIndex = nStartCol;

                    IWebbTableCell cell = this.PrintingTable.GetCell(0, nStartCol);

                    if (cell != null)
                    {
                        cell.CellStyle.Width = col.ColumnWidth;
                    }

                    if (this.HaveHeader)
                    {
                        if (col.TitleField != string.Empty)
                        {
                            if (dt.Columns.Contains(col.TitleField) && col.RowIndicator >= 0)
                            {
                                WebbTableCellHelper.SetCellValue(this.PrintingTable, nRow, nStartCol, dt.Rows[col.RowIndicator][col.TitleField], FormatTypes.String);
                            }
                        }
                        else
                        {
                            WebbTableCellHelper.SetCellValue(this.PrintingTable, nRow, nStartCol, col.Title, FormatTypes.String);
                        }
                    }
                }
            }
           
			if(this.HaveHeader)
			{
				this.HeaderRows.Add(nRow);

				nRow++;		
			}		
		}
		#endregion        //End Modify at 2008-10-20 10:44:00@Simon

		#region Modify codes at 2008-12-8 15:36:23@Simon
		public ArrayList GetPrnHeader(out ArrayList formats)
		{
			ArrayList prnHeaders=new ArrayList();

			formats=new ArrayList();		
	
			int m_Column = this.GetColumnsCount();

            if(m_Column<=0)return prnHeaders;

            if (this.TopCount > 0)
            {
                m_Column *= this.WrappedColumns;
            }

			for(int i=0;i<m_Column;i++)
			{
				prnHeaders.Add("");
				formats.Add(0);
			}

			int nCol=0;

            if (this.TopCount > 0)
            {
                for (int i = 0; i < this.WrappedColumns; i++)
                {
                    if (this._ShowRowIndicators) nCol++;

                    GetPrnHeader(prnHeaders, formats, ref nCol);
                }
            }

			return prnHeaders;
		}

        private void GetGroupHeadersInCol(int nStartCol, GroupInfo groupInfo, ArrayList prnHeaders, ArrayList formats)
        {
            if (groupInfo == null || !this.HaveHeader) return;

            if (groupInfo.DisplayAsColumn)
            {
                if (groupInfo.FollowSummaries)
                {
                    foreach (GroupSummary groupSummary in groupInfo.Summaries)
                    {
                        prnHeaders[nStartCol] = groupSummary.ColumnHeading;

                        formats[nStartCol] = groupSummary.HeadingFormat;
                   
                        nStartCol++;
                    }
                }
                if (GroupInfo.IsVisible(groupInfo))
                {
                    prnHeaders[nStartCol] = groupInfo.ColumnHeading;

                    formats[nStartCol] = groupInfo.HeadingFormat;

                    nStartCol++;
                }
                if (!groupInfo.FollowSummaries)
                {
                    foreach (GroupSummary groupSummary in groupInfo.Summaries)
                    {
                        prnHeaders[nStartCol] = groupSummary.ColumnHeading;

                        formats[nStartCol] = groupSummary.HeadingFormat;

                        nStartCol++;
                    }
                }
            }
            foreach (GroupInfo subgroupInfo in groupInfo.SubGroupInfos)
            {
                GetGroupHeadersInCol(nStartCol, subgroupInfo, prnHeaders, formats);
            }
        }
		private void GetPrnHeader(ArrayList prnHeaders, ArrayList formats,ref int nCol)
		{
			bool b_ExistColumn=(this.RootGroupInfo==null&&this.GridInfo.SortingFrequence==SortingFrequence.ShowBeforeColumns);

			if(b_ExistColumn)
			{
				nCol++;
			}

            GetGroupHeadersInCol(nCol, this.RootGroupInfo, prnHeaders, formats);
             
           for (int i = 0; i < this._GridInfo.Columns.Count; i++, nCol++)
            {
                GridColumn col = this._GridInfo.Columns[i];
                prnHeaders[nCol] = col.Title;
                formats[nCol] = col.TitleFormat;
            }
		}

		#endregion        //End Modify

		private void ApplyRowHeightStyle(int m_Rows)
		{
			if(this.RowsHight.Count <= 0) return;
			
			if(this.RowsHight.Count != m_Rows) return;

			for(int m_row = 0;m_row<m_Rows;m_row++)
			{
				this.PrintingTable.GetCell(m_row,0).CellStyle.Height = this.RowsHight[m_row];
			}
		}

		private void ApplyColumnWidthStyle(int m_Cols)
		{
			if(this.ColumnsWidth.Count<=0) return;		

			int count = Math.Min(this.ColumnsWidth.Count, m_Cols);

			for (int m_col = 0; m_col < count; m_col++)
			{
				IWebbTableCell cell = this.PrintingTable.GetCell(0,m_col);

				cell.CellStyle.Width = this.ColumnsWidth[m_col];
			}
		}

		public ExControl Control
		{
			get
			{
				return this.ExControl;
			}
		}

		public override void CalculateResult(DataTable i_Table)
		{
			if(i_Table == null)
			{
                //if(this._RootGroupInfo!= null)this.RootGroupInfo.ClearGroupResult(this._RootGroupInfo);
				
				return;
			}

            //if((this.ExControl.Report.Template.OneValuePerPage || this.ExControl.Report.Template.RepeatedReport) &&
            //    (this.ExControl.Report.Template.GroupByField == string.Empty && this.ExControl.Report.Template.SectionFilters.Count > 0))	//Modified at 2008-11-14 15:43:30@Scott
            //{
            //    this.RemoveSectionFilters();
            //}
            //else
            //{
            //    this.CheckSectionFilters();
            //}

			//Filter rows
			Webb.Collections.Int32Collection m_Rows = new Int32Collection();	

			if(this.ExControl!=null&&this.ExControl.Report!=null)
			{
				m_Rows=this.ExControl.Report.Filter.GetFilteredRows(i_Table);  //2009-5-25 11:02:57@Simon Add this Code
			}

			m_Rows = this.OneValueScFilter.Filter.GetFilteredRows(i_Table,m_Rows);	//06-04-2008@Scott

			m_Rows = this.RepeatFilter.Filter.GetFilteredRows(i_Table,m_Rows);	//Added this code at 2008-12-26 12:23:19@Simon

			this.Filter=AdvFilterConvertor.GetAdvFilter(DataProvider.VideoPlayBackManager.AdvReportFilters,this.Filter);    //2009-4-29 11:37:37@Simon Add UpdateAdvFilter

			m_Rows = this.Filter.GetFilteredRows(i_Table,m_Rows);	//06-04-2008@Scott

			m_Rows.CopyTo(this.FilteredRows);

            foreach (HorizontalGridColumn hGridColumn in GridColumns)
            {
                hGridColumn.GetResult(i_Table, m_Rows);
            }

            HorizontalGridData.GetResult(i_Table, m_Rows);

            //if(this._RootGroupInfo!= null) this._RootGroupInfo.CalculateGroupResult(i_Table,m_Rows,m_Rows,this.RootGroupInfo);
		}


		private void CheckSectionFilters()    //2009-6-10 9:53:28@Simon Add this Code
		{//need change
			if(this.ExControl==null) return;
			if(this.ExControl.Report==null) return;
			WebbReport m_WebbReport = this.ExControl.Report as WebbReport;
			if(m_WebbReport==null) return;
			//09-08-2008@Scott modify some code
			if(SectionFilters.Count > 0)
			{
				this.CreateSectionGroupInfo(SectionFilters);
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
			}
			else
			{
				this.RemoveSectionFilters();
			}
		}

		private void RemoveSectionFilters()
		{
            if (this._RootGroupInfo is SectionGroupInfo && _RootGroupInfo.IsSectionOutSide)
			{
				if(this._RootGroupInfo.SubGroupInfos.Count > 0)
				{
					this._RootGroupInfo = this._RootGroupInfo.SubGroupInfos[0];
				}
				else
				{
					this._RootGroupInfo = null;
				}
			}
		}

        private void SetRowIndicators(int i_Rows)
        {
            int index = 1;          

            for (int m_row = 0; m_row < i_Rows; m_row++)
            {
                if (this._HeaderRows.Contains(m_row) || this._TotalRows.Contains(m_row)) continue;

                if (this.Total && m_row == i_Rows - 1) continue;

                this.PrintingTable.GetCell(m_row, 0).Text = Webb.Utility.FormatIndicator(index++);
            }
        }

		private void CreateSectionGroupInfo(SectionFilterCollection i_Sections)
		{
            if (this._RootGroupInfo is SectionGroupInfo && _RootGroupInfo.IsSectionOutSide)
			{
				(this._RootGroupInfo as SectionGroupInfo).SetSectionFilters(i_Sections);
			}
			else
			{
				SectionGroupInfo m_SectionInfo = new SectionGroupInfo();
				
				m_SectionInfo.SetSectionFilters(i_Sections);

				m_SectionInfo.SubGroupInfos.Clear();

				if(this._RootGroupInfo != null)
				{
					m_SectionInfo.SubGroupInfos.Add(this._RootGroupInfo);
				}

				this._RootGroupInfo = m_SectionInfo;
			}

            _RootGroupInfo.IsSectionOutSide = true;
		}

		private int GetHeaderRowsCount()
		{
			int nCount = 1;

			if(this.TableHeaders!=null&&this.TableHeaders.RowCount>0&&this.TableHeaders.ColCount>0)   
			{
				nCount += this.TableHeaders.RowCount;  
			}
		
			if(!this.HaveHeader)nCount--;
     
			return nCount;
		}

		private int GetRowsCount()
		{
			int	nRet = this.GetHeaderRowsCount();	//get header rows
          
			DataTable dt = this.ExControl.GetDataSource();
	
			if(dt == null) return nRet;

            if (GridInfo != null)
            {
                nRet += GridInfo.Columns.Count;
            }

            //if(this._RootGroupInfo != null)		//calculate rows
            //{//have group info
            //    int nGroupedRows = 0;

            //    this.GetGroupedRows(this._RootGroupInfo,ref nGroupedRows);

            //    nRet += nGroupedRows;
            //}
            //else
            //{//simple play by play
            //    Webb.Collections.Int32Collection m_Rows = this.ExControl.Report.Filter.GetFilteredRows(dt);	//Modified at 2008-10-24 16:18:21@Scott

            //    m_Rows = this.OneValueScFilter.Filter.GetFilteredRows(dt,m_Rows);	//06-04-2008@Scott

            //    m_Rows = this.RepeatFilter.Filter.GetFilteredRows(dt,m_Rows);	//06-04-2008@Scott

            //    m_Rows = this.Filter.GetFilteredRows(dt,m_Rows);	//06-04-2008@Scott

            //    m_Rows.CopyTo(this.FilteredRows);

            //    nRet += m_Rows.Count;
            //}

            //if(this._Total) nRet++;

			return nRet;
		}

		private void GetGroupedRows(GroupInfo groupInfo, ref int value)
		{
			if(groupInfo.GroupResults == null)
			{
				return;
			}

			if(groupInfo.AddTotal)	//05-28-2008@Scott
			{
				value += groupInfo.GroupResults.Count;
			}

			foreach(GroupResult result in groupInfo.GroupResults)
			{
				if(result.SubGroupInfos.Count > 0)
				{
					value++;   

					this.GetGroupedRows(result.SubGroupInfos[0], ref value);
				}
				else
				{
                    value++;

					if(result.RowIndicators != null) value += result.RowIndicators.Count;				
					
				}
			}
		}

     
		
        #region GetColumnsCount
  
        public int GetColumnsCount()
        {
            int nRet = 0;

            if (this._ShowRowIndicators)
            {
                nRet += 1;
            }

            nRet += 2; // Field and Description

            if (CustomColumn)
            {
                nRet += GridColumns.Count;
            }
            else
            {
                nRet += HorizontalGridData.TopCount;
            }

            //int nVerticalColCount = this.GetVerticalColCount();

            //int nHorizonGroupColumns = 0;

            //if (this.RootGroupInfo == null)
            //{
            //    if (this.GridInfo.SortingFrequence != SortingFrequence.None)   //Added this code at 2008-12-3 9:09:51@Simon
            //    {
            //        nRet += 1;
            //    }
            //}
            //else
            //{
            //    this.GetHorizonColumnsCount(this._RootGroupInfo, ref nHorizonGroupColumns, 0);

            //}

            //nRet += Math.Max(nVerticalColCount, nHorizonGroupColumns);

            return nRet;
        }

        private int GetVerticalColCount()
        {
            int verticalColCount = this._GridInfo.Columns.Count;

            if (this.RootGroupInfo != null) verticalColCount += this.RootGroupInfo.GetVirtualGridGroupColumns();

            return verticalColCount;
        }

        public void GetHorizonColumnsCount(GroupInfo groupInfo, ref int totalHorizonCols,int IndentColumns)
        {
            if (groupInfo == null) return;

            int currentColumns = IndentColumns;

            if (this.ColumnafterGroup)
            {
                currentColumns += this.GetVerticalColCount();
            }
            else
            {
                currentColumns +=4;
            }

            if(GroupInfo.IsVisible(groupInfo)&&!groupInfo.DisplayAsColumn)
            {
                totalHorizonCols = Math.Max(totalHorizonCols, currentColumns);
            }

            if (groupInfo.SubGroupInfos.Count > 0)
            {    
                IndentColumns += Math.Abs(this.diffColumns);

                GroupInfo  subGroupInfo = groupInfo.SubGroupInfos[0];

                this.GetHorizonColumnsCount(subGroupInfo, ref totalHorizonCols, IndentColumns);
            }              
        }		
			
		#endregion

		private SectionGroupInfo CreateSectionGroupInfo()
		{
			if(this.ExControl == null || this.ExControl.Report == null) return null;

			WebbReport m_WebbReport = this.ExControl.Report as WebbReport;
			
			if(m_WebbReport == null || m_WebbReport.WebbDataSource == null) return null;
			
			if(m_WebbReport.WebbDataSource.SectionFilters == null || m_WebbReport.WebbDataSource.SectionFilters.Count <= 0)
			{
				return null;
			}
			else
			{
				SectionGroupInfo sectionInfo = new SectionGroupInfo();
				
				sectionInfo.SetSectionFilters(m_WebbReport.WebbDataSource.SectionFilters);

				return sectionInfo;
			}
		}

		//2008-9-1 8:55:08@simon
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

		//2008-9-1 8:55:08simon
		private void SetHeaderRows(int nStartRow,int nHeaderRowCount)
		{
			for(int i = nStartRow; i < nStartRow + nHeaderRowCount; i++)
			{
				if(!HeaderRows.Contains(i))this.HeaderRows.Add(i);
			}
		}

        public void UpdateVirtualGroupInfoWidth(ref int nStartCol, GroupInfo groupInfo, Int32Collection columnsWidth)
        {
            if (groupInfo == null) return;

            if (groupInfo.DisplayAsColumn)
            { 
                if (groupInfo.FollowSummaries)
                {
                    foreach (GroupSummary groupSummary in groupInfo.Summaries)
                    {
                        while (nStartCol >= columnsWidth.Count) columnsWidth.Add(BasicStyle.ConstValue.CellWidth);

                        columnsWidth[nStartCol] = groupSummary.ColumnWidth;

                        nStartCol++;
                    }
                }
                if (GroupInfo.IsVisible(groupInfo))
                {
                    while (nStartCol >= columnsWidth.Count) columnsWidth.Add(BasicStyle.ConstValue.CellWidth);

                    columnsWidth[nStartCol] = groupInfo._ColumnWidth;

                    nStartCol++;
                }
                if (!groupInfo.FollowSummaries)
                {
                    foreach (GroupSummary groupSummary in groupInfo.Summaries)
                    {
                        while (nStartCol >= columnsWidth.Count) columnsWidth.Add(BasicStyle.ConstValue.CellWidth);

                        columnsWidth[nStartCol] = groupSummary.ColumnWidth;

                        nStartCol++;
                    }
                }
               
            }

            if (groupInfo.SubGroupInfos.Count > 0) UpdateVirtualGroupInfoWidth(ref nStartCol, groupInfo.SubGroupInfos[0], columnsWidth);

        }

        //public void GetHeaderStartCol(ref int nStartCol, GroupInfo groupInfo)
        //{
        //    if (groupInfo == null || groupInfo.SubGroupInfos.Count == 0) return;

        //    nStartCol +=Math.Abs(this.diffColumns);

        //    GetHeaderStartCol(ref nStartCol, groupInfo.SubGroupInfos[0]);
        //}

        public int ResolveIndentStartCol()
        {
            int totalColumns = this.GetColumnsCount();

            if (this.ColumnafterGroup)
            {
                return totalColumns - this.GridInfo.Columns.Count;
            }

            int Start = this.ShowRowIndicators ? 1 : 0;

            if (this.RootGroupInfo == null && this._GridInfo.SortingFrequence == SortingFrequence.ShowBeforeColumns)   //Added this code at 2008-12-3 9:09:51@Simon
            {
                Start += 1;
            }

            return Start;           
        }



        public void ApplyVirtualGroupInfoWidth(ref int nStartCol, GroupInfo groupInfo, Int32Collection columnsWidth)
        {
            if (groupInfo == null) return;

            if (groupInfo.DisplayAsColumn)
            {
                while (nStartCol + groupInfo.Summaries.Count + 1 >= columnsWidth.Count) columnsWidth.Add(BasicStyle.ConstValue.CellWidth);

                if (groupInfo.FollowSummaries)
                {
                    foreach (GroupSummary groupSummary in groupInfo.Summaries)
                    {
                        while (nStartCol>= columnsWidth.Count) columnsWidth.Add(BasicStyle.ConstValue.CellWidth);

                        groupSummary.ColumnWidth = columnsWidth[nStartCol];

                        nStartCol++;
                    }
                }
                if (GroupInfo.IsVisible(groupInfo))
                {
                    while (nStartCol >= columnsWidth.Count) columnsWidth.Add(BasicStyle.ConstValue.CellWidth);

                    groupInfo._ColumnWidth = columnsWidth[nStartCol];

                    nStartCol++;
                }
                if (!groupInfo.FollowSummaries)
                {
                    foreach (GroupSummary groupSummary in groupInfo.Summaries)
                    {
                        while (nStartCol >= columnsWidth.Count) columnsWidth.Add(BasicStyle.ConstValue.CellWidth);

                        groupSummary.ColumnWidth = columnsWidth[nStartCol];

                        nStartCol++;
                    }
                }

              
            }

            if (groupInfo.SubGroupInfos.Count > 0) ApplyVirtualGroupInfoWidth(ref nStartCol, groupInfo.SubGroupInfos[0], columnsWidth);

        }

		#region Only Create create HeaderTable
			private WebbTable CreateHeaderTable(BasicStyle styles)
			{
				int nRows=1;				

				int nColumns = this.GetColumnsCount();			

				if(nColumns == 0 || nRows == 0) 
				{ 	
					return null;
				}
				
				WebbTable table= new WebbTable(nRows,nColumns);

                int nStartCol = 0; // ResolveIndentStartCol();

				table.SetRowStyle(0,styles,this.ShowRowIndicators);

				//Bitmap img=new Bitmap(100,100);

				//Graphics g=Graphics.FromImage(img);

				int MaxHeight=0;

                int tempStartCol = nStartCol;

				for(int i = 0;i<this._GridInfo.Columns.Count;i++,nStartCol++)
				{
					GridColumn col = this._GridInfo.Columns[i];

					col.ColumnIndex = nStartCol; 
					
					WebbTableCellHelper.SetCellValue(table, 0, nStartCol, col.Title, FormatTypes.String);

					IWebbTableCell cell=table.GetCell(0, nStartCol);

					cell.CellStyle.Width=col.ColumnWidth;

					if(col.Style.IsSimpleEdited())					
					{
						cell.CellStyle.SetStyle(col.Style,col.ColorNeedChange);	
					}

					cell.CellStyle.StringFormat=col.TitleFormat;	

					StringFormat sf=new StringFormat(col.TitleFormat);

					switch(cell.CellStyle.HorzAlignment)
					{
						case HorzAlignment.Default:
						case HorzAlignment.Center:
							sf.LineAlignment=StringAlignment.Center;
							break;                                 
						case HorzAlignment.Far:									
							sf.LineAlignment=StringAlignment.Far;
							break;
						case HorzAlignment.Near:									
							sf.LineAlignment=StringAlignment.Near;
							break;
					}

					switch(cell.CellStyle.VertAlignment)
					{
						case VertAlignment.Default:
						case VertAlignment.Center:
							sf.Alignment=StringAlignment.Center;
							break;                                 
						case VertAlignment.Top:									
							sf.Alignment=StringAlignment.Near;
							break;
						case VertAlignment.Bottom:									
							sf.Alignment=StringAlignment.Far;
							break;
					}
                   //SizeF szfText=g.MeasureString(cell.Text,cell.CellStyle.Font,col.ColumnWidth,sf);
					
                   //MaxHeight=Math.Max((int)szfText.Height+1,MaxHeight);
					
				}
				for(int i = 0;i<this._GridInfo.Columns.Count;i++,tempStartCol++)
				{
					IWebbTableCell cell=table.GetCell(0, tempStartCol);

					cell.CellStyle.Height=MaxHeight;
				}
				

                //img.Dispose();

				//g.Dispose();
			

				return table;

			}	
		public void UpdateAllFont()
		{
			Font rowFont=this.Styles.RowStyle.Font;	

			for(int i = 0;i<this._GridInfo.Columns.Count;i++)
			{
				GridColumn col = this._GridInfo.Columns[i];

				if(col.Style.IsEdited())
				{
					col.Style.Font=(Font)rowFont.Clone();
				}
			}

			this.Styles.SectionStyle.Font=new Font(rowFont.FontFamily.Name,rowFont.Size+2,rowFont.Style|FontStyle.Bold);

			this.Styles.SectionStyle.HorzAlignment=DevExpress.Utils.HorzAlignment.Near;

		}

		

		public void PrintHeadersOnly(Graphics g,Rectangle pageRect,System.Drawing.Printing.Margins margrins)
		{
			BasicStyle headerStyle=this.Styles.HeaderStyle;

			WebbTable table=this.CreateHeaderTable(headerStyle);

			g.Clear(Color.White);

			if(table==null)return;

			Rectangle clipRect=pageRect;

			PaintEventArgs args=new PaintEventArgs(g,clipRect);

			int left=(int)(margrins.Left/Webb.Utility.ConvertCoordinate);

			table.SetOffset(left,margrins.Top-10);
                
			table.PaintTable(args,false,clipRect);                
			
		}
		

		public void PrintHeadersOnly(System.Drawing.Printing.PrintPageEventArgs e)
		{
			BasicStyle headerStyle=this.Styles.HeaderStyle;

			WebbTable table=this.CreateHeaderTable(headerStyle);

			Rectangle clipRect=e.PageSettings.Bounds;

			PaintEventArgs args=new PaintEventArgs(e.Graphics,clipRect);

			table.SetOffset(e.PageSettings.Margins.Left,e.PageSettings.Margins.Top);
                
			table.PaintTable(args,false,clipRect);
                
			e.HasMorePages=false;
		}
		

			#endregion

        #region Only For CCRM Data
        public override void GetALLUsedFields(ref ArrayList  usedFields)
        {
            foreach (GridColumn col in this.GridInfo.Columns)
            {
                col.GetALLUsedFields(ref usedFields);
            }

            foreach (HorizontalGridColumn col in this.GridColumns)
            {
                col.GetALLUsedFields(ref usedFields);   
            }

            HorizontalGridData.GetALLUsedFields(ref usedFields);

            if (this._RootGroupInfo != null)
            {
                this.RootGroupInfo.GetAllUsedFields(ref usedFields);
            }
            this.Filter.GetAllUsedFields(ref usedFields);

            this.SectionFiltersWrapper.GetAllUsedFields(ref usedFields);
        }


        #endregion
    }
}
