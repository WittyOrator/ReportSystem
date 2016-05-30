/***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:
 * Author:Simon.Zhang [EMail:Webb.simon.zhang@163.com]
 * Create Time:02/01/2009
 * Copyright:1986-2009@Webb Electronics all right reserved.
 * Purpose:
 */

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
	#region public class MatrixGroupView
	[Serializable]
	public class MatrixGroupView : ExControlView,IMultiHeader
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
			
			info.AddValue("_FootballType",(int)this._FootballType);
			info.AddValue("_Fitler",this._Fitler,typeof(Webb.Data.DBFilter));
			info.AddValue("_ColumnsWidth",this._ColumnsWidth,typeof(Int32Collection));
			info.AddValue("_RowsHight",this._RowsHight,typeof(Int32Collection));
			info.AddValue("_Styles",this._Styles,typeof(Styles.ExControlStyles));
			info.AddValue("_BreakRows",this._BreakRows,typeof(Int32Collection));
			info.AddValue("_HaveHeader",this._HaveHeader);
			info.AddValue("_CellSizeAutoAdapting",this._CellSizeAutoAdapting,typeof(CellSizeAutoAdaptingTypes));	
		
			info.AddValue("_HeightPerPage",this._HeightPerPage);
			info.AddValue( "TableHeaders",this.TableHeaders,typeof(HeadersData));

			info.AddValue("SectionFilters", this.SectionFilters, typeof(SectionFilterCollection));//Modified 2008-09-08@Simon

			info.AddValue("SectionFiltersWrapper",this.SectionFiltersWrapper,typeof(SectionFilterCollectionWrapper));
			//Modified at 2009-1-14 17:20:44@Scott

		
			info.AddValue("_WidthEnable",this._WidthEnable);
			info.AddValue( "_SectionRoot",this._SectionRoot);  //Added this code at 2009-2-6 9:36:59@Simon
			info.AddValue("_MatrixInfo",this._MatrixInfo,typeof(MatrixInfo));
			info.AddValue("_MatrixDisplay",this._MatrixDisplay,typeof(ComputedStyle));
			info.AddValue("_GridInfo",this._GridInfo,typeof(GridInfo));
		
		}

		public MatrixGroupView(SerializationInfo info, StreamingContext context):base(info,context)
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
		}
		#endregion

		//Fields
		#region Fields
		protected Webb.Reports.ExControls.Data.GroupInfo _RootGroupInfo;
		protected CellSizeAutoAdaptingTypes _CellSizeAutoAdapting;
		protected FootballTypes _FootballType;
		protected Webb.Data.DBFilter _Fitler;
	
		protected bool _ShowRowIndicators;
		protected bool _HaveHeader;
		protected Int32Collection _FilteredRows;						//NonSerializable

		protected Int32Collection _HeaderRows;							//header row indicators for style builder
		protected Int32Collection _SectionRows;							//section row indicators for style builder
		protected Int32Collection _TotalRows;							//total row indicators for style builder
		protected Int32Collection _BreakRows;							//useless,page break row indicators for style builder
    	protected StyleBuilder.StyleColumnInfo _ColumnStyleRows;		//columns indicators for style builder
		
		protected Int32Collection _ColumnsWidth;
		protected Int32Collection _RowsHight;
		
		protected Styles.ExControlStyles _Styles;
	
		protected int _HeightPerPage;						//05-16-2008@Scott		
		
		protected HeadersData TableHeaders=null;     //2008-8-28 9:46:28@simon

		protected bool _SectionRoot=false;   //Added this code at 2009-2-6 9:37:39@Simon
		
		protected MatrixInfo _MatrixInfo=null;
		private bool _WidthEnable=false;
		private bool _HaveData=false;
		private ComputedStyle _MatrixDisplay=ComputedStyle.Group;
		protected GridInfo _GridInfo; 
		
		#endregion

		//Properties
		#region Properties
		
		public GridInfo GridInfo
		{
			get
			{
				if(this._GridInfo==null)this._GridInfo=new GridInfo();
				return this._GridInfo;
			}
			set{this._GridInfo=value;}
		}
		public ComputedStyle MatrixDisplay
		{
			get{return this._MatrixDisplay;}
			set
			{
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

		
		public int HeightPerPage
		{
			get{return this._HeightPerPage;}
			set{this._HeightPerPage = value;}
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
                if (this._CellSizeAutoAdapting != value)
                {
                    if (this._ColumnsWidth == null) this._ColumnsWidth = new Int32Collection();

                    this._ColumnsWidth.Clear();

                    this._CellSizeAutoAdapting = value;
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
		public MatrixGroupView(MatrixGroupControl i_Control):base(i_Control as ExControl)
		{
			this._RootGroupInfo = new Webb.Reports.ExControls.Data.FieldGroupInfo(Webb.Data.PublicDBFieldConverter.AvialableFields[0].ToString());
			this._RootGroupInfo.ColumnHeading = "New Group";
			this._Fitler = new Webb.Data.DBFilter();
			this._Styles = new ExControlStyles();
			this._Styles.LoadDefaultStyle();    //08-14-2008@Scott
			this._ShowRowIndicators = false;
			this._CellSizeAutoAdapting = CellSizeAutoAdaptingTypes.OneLine;
			
			this._HaveHeader = true;			
			
			this._HeightPerPage = 0;
			this._SectionRoot=false;
		}

		
        
		#region Modified Area 
		private bool RepeatHeader()
		{
			bool repeat=!this.ExControl.Report.Template.OneValuePerPage && !this.ExControl.Report.Template.ControlHeaderOnce ;	

			if((this.TableHeaders==null||this.TableHeaders.RowCount<=0)&&!this.HaveHeader)
			{
				repeat=false;
			}

//    		repeat=false;  //2009-4-3 9:32:40@Simon Add this Code
			
			return repeat;
		}
		#endregion        //End Modify at 2008-10-21 10:10:33@Simon

		public override int CreateArea(string areaName, IBrickGraphics graph)
		{
			if(this.PrintingTable != null)
			{				
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
			else
			{
				return 0;
			}

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
				
				if(this.RootGroupInfo.Summaries == null)
				{
					this.RootGroupInfo.Summaries=new GroupSummaryCollection();
				}
                if (this.RootGroupInfo.SubGroupInfos == null || this.RootGroupInfo.SubGroupInfos.Count == 0)
                {
                    this.RootGroupInfo.SubGroupInfos = new GroupInfoCollection();

                    this.RootGroupInfo.SubGroupInfos.Add(new FieldGroupInfo(""));
                }				
				if(MatrixInfo==null)
				{
					this.MatrixInfo=new MatrixInfo(RootGroupInfo,RootGroupInfo.SubGroupInfos[0],null,null);
				}
				else
				{
                    this.MatrixInfo = MatrixInfo.CopyFrom(RootGroupInfo, MatrixInfo); 
				}              

				this.MatrixInfo.MatrixDisplay=this.MatrixDisplay;
				this.MatrixInfo.GridInfo.Apply(this.GridInfo);
				
				//Filter rows
				Webb.Collections.Int32Collection m_Rows = new Int32Collection();	

				if(this.ExControl!=null&&this.ExControl.Report!=null)
				{
					m_Rows=this.ExControl.Report.Filter.GetFilteredRows(i_Table);  //2009-5-25 11:02:57@Simon Add this Code
				}


				m_Rows = this.OneValueScFilter.Filter.GetFilteredRows(i_Table,m_Rows);	

				m_Rows = this.RepeatFilter.Filter.GetFilteredRows(i_Table,m_Rows);

				this.Filter=AdvFilterConvertor.GetAdvFilter(DataProvider.VideoPlayBackManager.AdvReportFilters,this.Filter);    //2009-4-29 11:37:37@Simon Add UpdateAdvFilter

				m_Rows = this.Filter.GetFilteredRows(i_Table,m_Rows);	//06-04-2008@Scott

				m_Rows.CopyTo(this.FilteredRows);
			
				MatrixInfo.CalculateMatrixResult(i_Table,m_Rows);
				
			}

			#region CreatePrintingTable
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

					bool create=this.CreateMatrixPrintingTable();				

					return create;
				}
				private bool CreateMatrixPrintingTable()
				{

					if(MatrixInfo==null||!_HaveData||this.RootGroupInfo==null)
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
				
					this.PrintingTable = new WebbTable(m_Rows,m_Column);

                    MatrixInfo.SetMatrixColumnWidthAtFirst(this.PrintingTable, this.ShowRowIndicators);

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

					if(this.TableHeaders!=null)
					{
						this.TableHeaders.SetHeadGridLine(this.PrintingTable,this.HeaderRows);
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
			#endregion
		#endregion
	

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

                if (this.MatrixInfo != null && this.MatrixInfo.RowTotal.ShowTotal && !this.MatrixInfo.RowTotal.ShowFront)
                {
                    if (m_Cols < this.ColumnsWidth.Count)
                    {
                        if (m_col == count - 1)
                        {
                            cell.CellStyle.Width = ColumnsWidth[this.ColumnsWidth.Count - 1];

                            continue;
                        }
                    }
                    else if (m_Cols > this.ColumnsWidth.Count)
                    {
                        if (m_col == count - 1) continue;                        
                    }
                }

                cell.CellStyle.Width = ColumnsWidth[m_col];
			}
            if (this.MatrixInfo != null && this.MatrixInfo.RowTotal.ShowTotal && !this.MatrixInfo.RowTotal.ShowFront)
            {
                IWebbTableCell cell = this.PrintingTable.GetCell(0, m_Cols - 1);

                if (m_Cols > this.ColumnsWidth.Count&&cell!=null)
                {
                    cell.CellStyle.Width = ColumnsWidth[this.ColumnsWidth.Count - 1];
                }
            }
		}

		private void SetTableValue()
		{
			int m_Rows = 0,m_Col =0;
			
			if(this.ShowRowIndicators) m_Col = 1;	//add row indicator columns	
	
			if(!this.OneValuePerPage)   //Modified at 2008-10-21 8:47:39@Simon
			{
				int nHeaderStart = m_Rows,nHeaderCount = 0;

				this.SetHeaderValue(ref m_Rows);	//set header value

				nHeaderCount = m_Rows - nHeaderStart;

				this.SetHeaderRows(nHeaderStart,nHeaderCount);
			}
			
			
			this.MatrixInfo.SetMatrixRowsValue(this.PrintingTable,ref m_Rows,ref m_Col,this.TotalRows);	//set row value

			if(this.ShowRowIndicators)
			{
				this.SetRowIndicators(this.PrintingTable.GetRows());
			}
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
		
           
            if (MatrixInfo == null||MatrixInfo.ColGroup == null || MatrixInfo.RowGroup == null)
			{				
                this.ExControl.CalculateResult();
            }
            
		    int m_Column = MatrixInfo.GetMatrixGroupedColumns(this.ShowRowIndicators);
			
            for(int i=0;i<m_Column;i++)
			{
				prnHeaders.Add("");
				formats.Add(0);
			}
             
			int nCol=0;

			if(this._ShowRowIndicators) nCol++;

			MatrixInfo.GetHeaderValue(prnHeaders,formats,ref nCol);				
			         
			return prnHeaders;
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
				this.MatrixInfo.SetHeaderValue(this,this.PrintingTable, ref nRow, ref nCol);	
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

        public override void GetALLUsedFields(ref ArrayList usedFields)
        {
            if (this._RootGroupInfo != null)
            {
                this.RootGroupInfo.GetAllUsedFields(ref usedFields);
            }

            if (this._MatrixInfo != null)
            {
                this._MatrixInfo.GetAllUsedFields(ref usedFields);
            }

            this.Filter.GetAllUsedFields(ref usedFields);
            
        }


		
	}
	#endregion
}
