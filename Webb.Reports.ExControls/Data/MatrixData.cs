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
using Webb.Reports.ExControls.Views;

namespace Webb.Reports.ExControls.Data
{
    //Added these codes at 2009-2-13 10:29:59@Simon
    #region TotalStyles
    [Serializable]
	public class TotalStyles:ISerializable
	{
		#region Field & Property

		private bool _Show=false;
		private bool _ShowFront=false;	
		private string _TotalTitle="[Value]:[Count]";
		private bool _ShowZero=false;
		private string _HeaderTitle=string.Empty;
		private bool _ShowField=true;
        private bool _Relative=true;
		private BasicStyle _Style=new BasicStyle();
		private bool _MajorStyle=false;
		private bool _MinorTotal=true;
        private int _ColumnWidth = 55;


		private GroupSummaryCollection _totalsummaries=new GroupSummaryCollection();
	
        [Browsable(false)]
		public bool ShowTotal
		{
			get{return this._Show;}
			set{this._Show=value;}
		}
        [Category("Column Style")]
        public int ColumnWidth
        {
            get { return this._ColumnWidth; }
            set { this._ColumnWidth = value; }
        }

		[Category("Title")]
		public string HeaderTitle
		{
			get{return this._HeaderTitle;}
			set{this._HeaderTitle=value;}
		}

		[Category("Style")]
		public bool NeedChange
		{
			get{return this._MajorStyle;}
			set{
				if(this.ShowTotal)
				{
					this._MajorStyle=value;
				}
				else
				{
					this._MajorStyle=false;
				 }
			   }
		}

		[Category("Data")]
		public bool Relative
		{
			get{return this._Relative;}
			set{this._Relative=value;}
		}
		[Browsable(false)]
		public bool ShowField
		{
			get{return this._ShowField;}
			set{this._ShowField=value;}
		}
        [Category("Position")]
		public bool ShowFront
		{
			get{return this._ShowFront;}
			set{this._ShowFront=value;}
		}
		[Category("Data")]
		public bool MinorTicks
		{
			get{return this._MinorTotal;}
			set{this._MinorTotal=value;}
		}	
		[Category("Data")]
		[Editor(typeof(Webb.Reports.Editors.TextFormatEditor), typeof(System.Drawing.Design.UITypeEditor))]	
		public string TextFormat
		{
			get{	
			     if(this._TotalTitle==string.Empty&&this.MinorTicks)_TotalTitle=" ";
				 return this._TotalTitle;
			   }
			set{this._TotalTitle=value;}
		}	
		[Category("Style")]
		public BasicStyle Style
		{
			get{return this._Style;}
			set{this._Style=value;}
		}
		[Category("Data")]
        public bool ShowZero
		{
			get{return this._ShowZero;}
			set{this._ShowZero=value;}
		}	
		[Category("Data")]
		public GroupSummaryCollection TotalSummaries
		{
			get
			{
				if(this._totalsummaries==null) return new GroupSummaryCollection();
				return this._totalsummaries;}
			set{this._totalsummaries=value;}
		}
		#endregion

		public TotalStyles()
		{
			_Show=false;
			_ShowFront=false;		   
			_TotalTitle="[Value]:[Count]";
			_totalsummaries=new GroupSummaryCollection();
			_ShowZero=false;
			_HeaderTitle=string.Empty;
			this._Relative=true;
			this._ShowField=true;
			this._Style=new BasicStyle();
			this._MajorStyle=false;
			this._MinorTotal=true;
			

		}
		public TotalStyles Copy()
		{
			TotalStyles total=new TotalStyles();
			total._Show=this._Show;
			total._HeaderTitle=this._HeaderTitle;
			total._ShowZero=this._ShowZero;
			total._TotalTitle=this._TotalTitle;
			total._ShowFront=this._ShowFront;			
			total._totalsummaries=this.TotalSummaries.CopyStructure();
			total._ShowField=this._ShowField;
			total._Relative=this._Relative;
			total.Style=this.Style.Copy() as BasicStyle ;
			total._MajorStyle=this._MajorStyle;
			total._MinorTotal=this._MinorTotal;
            total._ColumnWidth = this._ColumnWidth;
			return total;
		}
		public static string FormatSummaries(GroupSummaryCollection summries)
		{
			StringBuilder sb=new StringBuilder();

			foreach(GroupSummary summary in summries)
			{
				sb.Append(WebbTableCellHelper.FormatValue(null,summary));
			}
            
			return sb.ToString();
		}


		public string ColumnResult(GroupResult gr)
		{
			GroupInfo groupinfo=gr.SubGroupInfos[0];

			StringBuilder sb=new StringBuilder();

			if(this._MinorTotal)
			{
				foreach(GroupResult result in groupinfo.GroupResults)
				{
					sb.Append(this.TotalResult(result.GroupValue.ToString(),result)+"\n");
				}
			}
			else
			{
				sb.Append(this.TotalResult(gr.GroupValue.ToString(),gr));
			}
			return sb.ToString().Trim(" \n".ToCharArray());
			
		}
	
		public string TotalParentResult(GroupResult gr)
		{			
			string name=gr.ParentGroupInfo.ParentGroupResult.GroupValue.ToString();

			string total=this.TotalResult(name,gr);

			return total;
			
		}	
		public string TotalResult(GroupResult gr)
		{	
			string name=gr.GroupValue.ToString();

			string total=this.TotalResult(name,gr);

			return total;
			
		}		
		public string TotalResult(string name,GroupResult gr)
	   {			
			 string total=this.TextFormat;	

			total=total.Replace("[VALUE]","[Count]");		
			
			total=total.Replace("[FIELD]","[Value]");  
		
			if(total.IndexOf("[Count]")<0)
			{	
				if(this.MinorTicks)
				{ 
                  total="[Value]"+total+"[Count]";
					
				}
				else
				{
					total=total+"[Count]";
				}
			}

	        string summaries=FormatSummaries(gr.Summaries);

			total=total.Replace	("[Value]",name);
        
            total=total.Replace("[Count]",summaries);

			return total;
		}

		#region Serialization By Simon's Macro 2009-2-9 9:58:59
		public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			info.AddValue("HeaderTitle",HeaderTitle);
			info.AddValue("_Show",_Show);
			info.AddValue("_ShowZero",_ShowZero);
			info.AddValue("_ShowFront",_ShowFront);
			info.AddValue("_TotalTitle",_TotalTitle);
			info.AddValue("_totalsummaries",_totalsummaries,typeof(Webb.Reports.ExControls.Data.GroupSummaryCollection));
		    info.AddValue("_ShowField",_ShowField);
			info.AddValue("_Relative",_Relative);
			info.AddValue("_Style",this._Style,typeof(BasicStyle));
            info.AddValue("_MajorStyle",this._MajorStyle);
			info.AddValue("_MinorTotal",this._MinorTotal);
            info.AddValue("_ColumnWidth", this._ColumnWidth);
		}

		public TotalStyles(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			try
			{
				_MinorTotal=info.GetBoolean("_MinorTotal");
			}
			catch
			{
				_MinorTotal=true;
			}	
			try
			{
				_MajorStyle=info.GetBoolean("_MajorStyle");
			}
			catch
			{
				_MajorStyle=false;
			}	
			try
			{
				_ShowField=info.GetBoolean("_ShowField");
			}
			catch
			{
				_ShowField=true;
			}			
			try
			{
				_Relative=info.GetBoolean("_Relative");
			}
			catch
			{
				_Relative=true;
			}
			try
			{
				HeaderTitle=info.GetString("HeaderTitle");
			}
			catch
			{
				HeaderTitle=string.Empty;
			}
			try
			{
				_ShowZero=info.GetBoolean("_ShowZero");
			}
			catch
			{
				_ShowZero=false;
			}
			try
			{
				_Show=info.GetBoolean("_Show");
			}
			catch
			{
				_Show=false;
			}
			try
			{
				_ShowFront=info.GetBoolean("_ShowFront");
			}
			catch
			{
				_ShowFront=false;
			}
			try
			{
				_TotalTitle=info.GetString("_TotalTitle");
			}
			catch
			{
				_TotalTitle=string.Empty;
			}
            try
            {
                _ColumnWidth = info.GetInt32("_ColumnWidth");
            }
            catch
            {
                _ColumnWidth = 55;
            }
			try
			{
				_Style=(BasicStyle)info.GetValue("_Style",typeof(BasicStyle));
			}
			catch
			{
               _Style=new BasicStyle();
			}
			try
			{
				_totalsummaries=(Webb.Reports.ExControls.Data.GroupSummaryCollection)info.GetValue("_totalsummaries",typeof(Webb.Reports.ExControls.Data.GroupSummaryCollection));
			}
			catch
			{
               _totalsummaries=new GroupSummaryCollection();
			}
		}
		#endregion

        public void GetFieldUsed(ref ArrayList _UsedFields)
        {
            if (this.TotalSummaries.Count > 0)
            {
                foreach (GroupSummary groupSummary in this.TotalSummaries)
                {
                    if (!_UsedFields.Contains(groupSummary.Field)) _UsedFields.Add(groupSummary.Field);

                    groupSummary.Filter.GetAllUsedFields(ref _UsedFields);

                    groupSummary.DenominatorFilter.GetAllUsedFields(ref _UsedFields);
                }
            }
        }


	}
	#endregion

	#region Struct CStyle
	public struct CStyle
	{
		public bool NeedChange;
	    public IBasicStyle Style;
	}
	#endregion

	#region MatrixInfo
	[Serializable]
	public class MatrixInfo:ISerializable
	{
        //Include Properties & Copy & Serializable Functions
        #region AutoCodes By Simon's Macro 2009-2-10 13:46:58
        public bool HaveSeprateHeader
        {
            get { return this._HaveSeprateHeader; }
            set { this._HaveSeprateHeader = value; }
        }
        public bool ShowInOneCol
        {
            get { return this._ShowInOneCol; }
            set { _ShowInOneCol = value; }
        }
        public bool TotalCellVertical
        {
            get { return this._TotalCellVertical; }
            set { this._TotalCellVertical = value; }
        }

        public GridInfo GridInfo
        {
            get
            {
                if (this._GridInfo == null) this._GridInfo = new GridInfo();
                return this._GridInfo;
            }
            set { this._GridInfo = value; }
        }
        public ComputedStyle MatrixDisplay
        {
            get { return this._MatrixDisplay; }
            set { this._MatrixDisplay = value; }
        }

        public Rectangle Rect
        {
            get { return this._Rect; }
            set { this._Rect = value; }
        }
        public Webb.Reports.ExControls.Data.GroupInfo RowGroup
        {
            get { return _RowGroup; }
            set { _RowGroup = value; }
        }

        public Webb.Reports.ExControls.Data.GroupInfo ColGroup
        {
            get { return _ColGroup; }
            set { _ColGroup = value; }
        }

        public Webb.Reports.ExControls.Data.GroupInfo SepGroup
        {
            get { return _SepGroup; }
            set { _SepGroup = value; }
        }

        public Webb.Reports.ExControls.Data.GroupInfo DisGroup
        {
            get { return _DisGroup; }
            set { _DisGroup = value; }
        }

        public Webb.Reports.ExControls.Data.TotalStyles CellTotal
        {
            get { return _CellTotal; }
            set { _CellTotal = value; }
        }

        public Webb.Reports.ExControls.Data.TotalStyles RowTotal
        {
            get { return _RowTotal; }
            set { _RowTotal = value; }
        }

        public Webb.Reports.ExControls.Data.TotalStyles ColTotal
        {
            get { return _ColTotal; }
            set { _ColTotal = value; }
        }

        public MatrixInfo Copy()
        {
            MatrixInfo thiscopy = new MatrixInfo(_ColGroup, _RowGroup, _SepGroup, _DisGroup);
            thiscopy._CellTotal = this._CellTotal.Copy();
            thiscopy._RowTotal = this._RowTotal.Copy();
            thiscopy._ColTotal = this._ColTotal.Copy();
            thiscopy._Rect = this._Rect;
            thiscopy.MatrixDisplay = this.MatrixDisplay;
            thiscopy.GridInfo.Apply(this.GridInfo);
            thiscopy._ShowInOneCol = this.ShowInOneCol;
            thiscopy._TotalCellVertical = this._TotalCellVertical;
            thiscopy._HaveSeprateHeader = this._HaveSeprateHeader;

            return thiscopy;
        }
        public static MatrixInfo CopyFrom(GroupInfo rootGroup,MatrixInfo matrixInfo)
        {
            MatrixInfo newMatrixInfo = new MatrixInfo(rootGroup, rootGroup.SubGroupInfos[0], matrixInfo.SepGroup, matrixInfo.DisGroup);

            newMatrixInfo.CellTotal = matrixInfo.CellTotal.Copy();
            newMatrixInfo.RowTotal = matrixInfo.RowTotal.Copy();
            newMatrixInfo.ColTotal = matrixInfo.ColTotal.Copy();
            newMatrixInfo.Rect = matrixInfo.Rect;
            newMatrixInfo.ShowInOneCol = matrixInfo.ShowInOneCol;
            newMatrixInfo.TotalCellVertical = matrixInfo.TotalCellVertical;
            newMatrixInfo.HaveSeprateHeader = matrixInfo.HaveSeprateHeader;

            return newMatrixInfo;
        }
        public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            info.AddValue("_SepGroup", _SepGroup, typeof(Webb.Reports.ExControls.Data.GroupInfo));
            info.AddValue("_DisGroup", _DisGroup, typeof(Webb.Reports.ExControls.Data.GroupInfo));
            info.AddValue("_CellTotal", _CellTotal, typeof(Webb.Reports.ExControls.Data.TotalStyles));
            info.AddValue("_RowTotal", _RowTotal, typeof(Webb.Reports.ExControls.Data.TotalStyles));
            info.AddValue("_ColTotal", _ColTotal, typeof(Webb.Reports.ExControls.Data.TotalStyles));
            info.AddValue("_Rect", _Rect, typeof(Rectangle));
            info.AddValue("_GridInfo", _GridInfo, typeof(GridInfo));
            info.AddValue("_MatrixDisplay", this._MatrixDisplay, typeof(ComputedStyle));
            info.AddValue("_ShowInOneCol", this._ShowInOneCol);
            info.AddValue("_TotalCellVertical", this._TotalCellVertical);
            info.AddValue("_HaveSeprateHeader", _HaveSeprateHeader);
        }
        public MatrixInfo(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            try
            {
                _HaveSeprateHeader = info.GetBoolean("_HaveSeprateHeader");
            }
            catch
            {
                _HaveSeprateHeader = false;
            }
            try
            {
                _TotalCellVertical = info.GetBoolean("_TotalCellVertical");
            }
            catch
            {
                _TotalCellVertical = false;
            }
            try
            {
                _ShowInOneCol = info.GetBoolean("_ShowInOneCol");
            }
            catch
            {
                _ShowInOneCol = false;
            }

            try
            {
                _SepGroup = (Webb.Reports.ExControls.Data.GroupInfo)info.GetValue("_SepGroup", typeof(Webb.Reports.ExControls.Data.GroupInfo));
            }
            catch
            {
            }
            try
            {
                _DisGroup = (Webb.Reports.ExControls.Data.GroupInfo)info.GetValue("_DisGroup", typeof(Webb.Reports.ExControls.Data.GroupInfo));
            }
            catch
            {
            }
            try
            {
                _CellTotal = (Webb.Reports.ExControls.Data.TotalStyles)info.GetValue("_CellTotal", typeof(Webb.Reports.ExControls.Data.TotalStyles));
            }
            catch
            {
            }
            try
            {
                _RowTotal = (Webb.Reports.ExControls.Data.TotalStyles)info.GetValue("_RowTotal", typeof(Webb.Reports.ExControls.Data.TotalStyles));
            }
            catch
            {
            }
            try
            {
                _ColTotal = (Webb.Reports.ExControls.Data.TotalStyles)info.GetValue("_ColTotal", typeof(Webb.Reports.ExControls.Data.TotalStyles));
            }
            catch
            {
            }
            try
            {
                _Rect = (Rectangle)info.GetValue("_Rect", typeof(Rectangle));
            }
            catch
            {
                _Rect = new Rectangle(0, 0, 0, 0);
            }
            try
            {
                this._MatrixDisplay = (ComputedStyle)info.GetValue("_MatrixDisplay", typeof(ComputedStyle));
            }
            catch
            {
                _MatrixDisplay = ComputedStyle.Group;
            }
            try
            {
                this._GridInfo = (GridInfo)info.GetValue("_GridInfo", typeof(GridInfo));
            }
            catch
            {
                _GridInfo = new GridInfo();
                _MatrixDisplay = ComputedStyle.Group;
            }
        }
        #endregion

		[NonSerialized]
		private GroupInfo _RowGroup;
		[NonSerialized]
		private GroupInfo _ColGroup;

		private GroupInfo _SepGroup;
		private GroupInfo _DisGroup;

		private TotalStyles _CellTotal=new TotalStyles();
		private TotalStyles _RowTotal=new TotalStyles();
		private TotalStyles _ColTotal=new TotalStyles();
		private Rectangle _Rect=new Rectangle(0,0,0,0);

        [NonSerialized] 
		private GroupInfo _TotalRowGroup;
        [NonSerialized]
		private GroupInfo _TotalColGroup;

		private ComputedStyle _MatrixDisplay=ComputedStyle.Group;
		protected GridInfo _GridInfo;
		protected bool _ShowInOneCol=false;
        protected bool _TotalCellVertical = false;
        protected bool _HaveSeprateHeader = false;

		[NonSerialized]
		public Int32Collection TotalCellRows=new Int32Collection();

		private static GroupInfo GetGroup(GroupInfo group)
		{
			if(group==null)
			{
				return new FieldGroupInfo("");
			}
			GroupInfo newGroup=group.Copy();

			if(newGroup.Summaries==null)
			{
				newGroup.Summaries=new GroupSummaryCollection();
			}
			return newGroup;
		}
		public MatrixInfo(GroupInfo colGroup,GroupInfo rowGroup,GroupInfo sepGroup, GroupInfo disGroup)
		{
			_RowGroup=GetGroup(rowGroup);
			_ColGroup=GetGroup(colGroup);
			_SepGroup=GetGroup(sepGroup);
			_DisGroup=GetGroup(disGroup);			
		     
		}	
			
		//Calculate grouped rows and columns
	 #region Get rows and columns
           #region Old
            //public int GetMatrixGroupedColumns(bool WidthEnable,bool ShowRowIndicators,Int32Collection ColumnsWidth)
            //{	
            //    int nCols = 0;
            //    if(!WidthEnable)
            //    {
            //        ColumnsWidth.Clear();
            //    }
            //    if(ShowRowIndicators)
            //    {
            //        nCols += 1;

            //        if(!WidthEnable)
            //        {
            //            ColumnsWidth.Add(BasicStyle.ConstValue.CellWidth);
            //        }
            //    }	

            //    nCols += 1;// Row Group

            //    if(!WidthEnable)
            //    {				
            //        ColumnsWidth.Add(this.RowGroup._ColumnWidth);
            //    }

            //    if (this._HaveSeprateHeader && this.ShowInOneCol)   //Separate Header
            //    {
            //        nCols++;

            //        if (!WidthEnable)
            //        {
            //            ColumnsWidth.Add(this.SepGroup._ColumnWidth);
            //        }
            //    }

            //    if(ColGroup.GroupResults!=null)
            //    {	
            //        int colcount=ColGroup.GroupResults.Count; 

            //        GroupInfo RowInfo=ColGroup.GroupResults[0].SubGroupInfos[0];

            //        GroupInfo SepInfo=RowInfo.GroupResults[0].SubGroupInfos[0];

            //        if (!this.ShowInOneCol)
            //        {
            //            int sepCount=SepInfo.GroupResults.Count;

            //            colcount *= sepCount;                

            //        }

            //        colcount *= this.ColSteps;

            //        if (this._CellTotal.ShowTotal && this._TotalCellVertical) colcount += ColGroup.GroupResults.Count;


            //        if(!WidthEnable)
            //        {
            //            if (this.RowTotal.ShowTotal && this.RowTotal.ShowFront) ColumnsWidth.Add(this.RowTotal.ColumnWidth);

            //            Int32Collection stepWidths=this.StepWidths;

            //            for(int i=0;i<ColGroup.GroupResults.Count;i++)
            //            {
            //                foreach(int width in stepWidths)
            //                {
            //                    ColumnsWidth.Add(width);
            //                }

            //            }
            //            if(this.RowTotal.ShowTotal&&!this.RowTotal.ShowFront)ColumnsWidth.Add(this.RowTotal.ColumnWidth);
            //        }	
    				
            //        nCols+=colcount;

            //        if(this.RowTotal.ShowTotal)nCols++;
    			     
            //    }				
    	
            //    return nCols;

            //}
              #endregion

          #region New Get Columns
            public int GetMatrixGroupedColumns(bool ShowRowIndicators)
            {
                int nCols = 0;
               
                if (ShowRowIndicators)
                {
                    nCols += 1;               
                }

                nCols += 1;// Row Group        

                if (this._HaveSeprateHeader && this.ShowInOneCol)   //Separate Header
                {
                    nCols++;               
                }

                if (ColGroup.GroupResults != null)
                {
                    int colcount = ColGroup.GroupResults.Count;

                    GroupInfo RowInfo = ColGroup.GroupResults[0].SubGroupInfos[0];

                    GroupInfo SepInfo = RowInfo.GroupResults[0].SubGroupInfos[0];

                    if (!this.ShowInOneCol)
                    {
                        int sepCount = SepInfo.GroupResults.Count;

                        colcount *= sepCount;

                    }

                    colcount *= this.ColSteps;

                    if (this._CellTotal.ShowTotal && this._TotalCellVertical) colcount += ColGroup.GroupResults.Count;
                 
                    nCols += colcount;

                    if (this.RowTotal.ShowTotal) nCols++;

                }

                return nCols;

            }
            #endregion

        public void SetMatrixColumnWidthAtFirst(IWebbTable table, bool ShowRowIndicators)
        {
            IWebbTableCell cell=null;

            int nCols = 0;
           
            if (ShowRowIndicators)
            {
                cell = table.GetCell(0, nCols);  // Row Group  in FirstColumn

                if (cell != null) cell.CellStyle.Width = BasicStyle.ConstValue.RowIndicatorWidth;

                nCols++;                      
            }            

            cell=table.GetCell(0,nCols);  // Row Group  in FirstColumn

           if(cell!=null)cell.CellStyle.Width=this.RowGroup._ColumnWidth;

            nCols++;       

            if (this._HaveSeprateHeader && this.ShowInOneCol)   //Separate Header
            {
                cell = table.GetCell(0, nCols);  // Row Group  in FirstColumn

                if (cell != null) cell.CellStyle.Width = this.SepGroup._ColumnWidth;

                nCols++;               
            }

            if (ColGroup.GroupResults != null)
            {
                if (this.RowTotal.ShowTotal && this.RowTotal.ShowFront)
                {
                    cell = table.GetCell(0, nCols);  // Row Group  in FirstColumn

                    if (cell != null) cell.CellStyle.Width = this.RowTotal.ColumnWidth;

                    nCols++;

                }
                
                int colcount = ColGroup.GroupResults.Count;

                GroupInfo RowInfo = ColGroup.GroupResults[0].SubGroupInfos[0];

                GroupInfo SepInfo = RowInfo.GroupResults[0].SubGroupInfos[0];

                if (!this.ShowInOneCol)
                {
                    int sepCount = SepInfo.GroupResults.Count;

                    colcount *= sepCount;

                }

                colcount *= this.ColSteps;

                if (this._CellTotal.ShowTotal && this._TotalCellVertical) colcount += ColGroup.GroupResults.Count;             

                Int32Collection stepWidths = this.StepWidths;

                for (int i = 0; i < ColGroup.GroupResults.Count; i++)
                {
                    foreach (int width in stepWidths)
                    {
                        cell = table.GetCell(0, nCols);  // Row Group  in FirstColumn

                        if (cell != null) cell.CellStyle.Width = width;

                        nCols++;                       
                    }

                }               

                if (this.RowTotal.ShowTotal && !this.RowTotal.ShowFront)
                {
                    cell = table.GetCell(0, nCols);  // Row Group  in FirstColumn

                    if (cell != null) cell.CellStyle.Width = this.RowTotal.ColumnWidth;

                    nCols++;
                }

            }           

        }
		/// <summary>
		/// Calculate grouped rows
		/// </summary>
		/// <returns></returns>
		public int GetMatrixGroupedRows(IMultiHeader i_View)
		{
			int m_value = 0;				

			if(this._ColGroup.GroupResults!=null)
			{
				GroupInfo RowInfo=ColGroup.GroupResults[0].SubGroupInfos[0];

				GroupInfo SepInfo=RowInfo.GroupResults[0].SubGroupInfos[0];	

				int count=RowInfo.GroupResults.Count;	 //RowGroup Result

				if(!this.ShowInOneCol)
				{					
					if(this._CellTotal.ShowTotal&&!this._TotalCellVertical)
					{
						count+=RowInfo.GroupResults.Count;
					}
				}
				else
				{
                    if (this._CellTotal.ShowTotal && !this._TotalCellVertical)
					{
						count*=SepInfo.GroupResults.Count*2;
					}
					else
					{
						count*=SepInfo.GroupResults.Count;
					}                    
				}

				m_value+=count;
			}
				
			if(i_View.HaveHeader)
			{
				m_value++;
			}

            if (this._HaveSeprateHeader && !this.ShowInOneCol) m_value++;

			if(this.ColTotal.ShowTotal) m_value++;	//add total rows			

			return m_value;
		}

		#endregion

        #region Helpful and important properties  of MatrixInfo
        public int ColSteps
        {
            get
            {
                int count = 0;
                if (this._MatrixDisplay == ComputedStyle.Group)
                {
                    count = this.ColGroup.Summaries.Count + 1;                   
                }
                else
                {
                    count = this.GridInfo.Columns.Count;

                    if (this._GridInfo.SortingFrequence != SortingFrequence.None)
                    {
                        count++;
                    }
                    if (count == 0) count = 1;                  

                }

                return count;
            }

        }

        public Int32Collection StepWidths
        {
            get
            {
                Int32Collection stepWidths = new Int32Collection();

                int sepCount = 0;

                #region Get count of seprateGroup results
                if (this._ColGroup.GroupResults != null && this._ColGroup.GroupResults.Count > 0)
                {
                    GroupInfo resultRowGroupInfo = ColGroup.GroupResults[0].SubGroupInfos[0];

                    GroupInfo resultSepGroupInfo = resultRowGroupInfo.GroupResults[0].SubGroupInfos[0];

                    if (this.ShowInOneCol)
                    {
                        sepCount = 1;
                    }
                    else
                    {
                        sepCount = resultSepGroupInfo.GroupResults.Count;
                    }
                }

                if (sepCount == 0) sepCount = 1;
                #endregion

                if (this._CellTotal.ShowTotal && this._CellTotal.ShowFront && this._TotalCellVertical) stepWidths.Add(this.DisGroup._ColumnWidth);

                if (this._MatrixDisplay == ComputedStyle.Grid)
                {
                    #region Matrix Grid
                    for (int i = 0; i < sepCount; i++)
                    {
                        if (GridInfo.Columns.Count == 0)
                        {
                            stepWidths.Add(this.ColGroup._ColumnWidth);
                        }
                        else
                        {
                       
                            int colwidth = this.ColGroup._ColumnWidth;

                            if (this._GridInfo.SortingFrequence == SortingFrequence.ShowBeforeColumns)
                            {
                                stepWidths.Add(colwidth);
                            }

                            foreach (GridColumn column in GridInfo.Columns)
                            {
                                stepWidths.Add(column.ColumnWidth);
                            }

                            if (this._GridInfo.SortingFrequence == SortingFrequence.ShowAfterColumns)
                            {
                                stepWidths.Add(colwidth);
                            }
                        }
                    }
                    #endregion
                }
                else
                {
                    #region Matrix Group                   

                    for (int i = 0; i < sepCount; i++)
                    {
                        if(!this.ColGroup.FollowSummaries)stepWidths.Add(this.ColGroup._ColumnWidth);

                        foreach (GroupSummary summary in this.ColGroup.Summaries)
                        {
                            int width = summary.ColumnWidth > 0 ? summary.ColumnWidth : BasicStyle.ConstValue.CellWidth;

                            stepWidths.Add(width);
                        }
                        if (this.ColGroup.FollowSummaries) stepWidths.Add(this.ColGroup._ColumnWidth);

                    }

                    #endregion
                }

                if (this._CellTotal.ShowTotal && !this._CellTotal.ShowFront && this._TotalCellVertical) stepWidths.Add(this.DisGroup._ColumnWidth);

                return stepWidths;
            }
        }

        public ArrayList StepStyles
        {
            get
            {
                ArrayList arrStyles = new ArrayList();

                CStyle style;

                int sepCount = 0;

                #region Get SeprateGroup's results count
                if (this._ColGroup.GroupResults != null && this._ColGroup.GroupResults.Count > 0)
                {
                    GroupInfo resultRowGroupInfo = ColGroup.GroupResults[0].SubGroupInfos[0];

                    GroupInfo resultSepGroupInfo = resultRowGroupInfo.GroupResults[0].SubGroupInfos[0];

                    if (this.ShowInOneCol)
                    {
                        sepCount = 1;
                    }
                    else
                    {
                        sepCount = resultSepGroupInfo.GroupResults.Count;
                    }
                }

                if (sepCount == 0) sepCount = 1;
                #endregion

                if (this._CellTotal.ShowTotal && this._CellTotal.ShowFront && this._TotalCellVertical)
                {
                    style.Style = this.CellTotal.Style.Copy();

                    style.NeedChange = this.CellTotal.NeedChange;

                    arrStyles.Add(style);
                }

                if (this._MatrixDisplay == ComputedStyle.Grid)
                {
                    #region Matrix  Grid
                    for (int i = 0; i < sepCount; i++)
                    {
                        if (GridInfo.Columns.Count == 0)
                        {
                            style.Style = this.ColGroup.Style;

                            style.NeedChange = this.ColGroup.ColorNeedChange;

                            arrStyles.Add(style);
                        }
                        else
                        {
                            #region Have GridColumns
                            if (this._GridInfo.SortingFrequence == SortingFrequence.ShowBeforeColumns)
                            {
                                style.Style = this.ColGroup.Style;

                                style.Style.VertAlignment = DevExpress.Utils.VertAlignment.Top;

                                style.NeedChange = this.ColGroup.ColorNeedChange;

                                arrStyles.Add(style);
                            }

                            foreach (GridColumn column in GridInfo.Columns)
                            {
                                style.Style = column.Style;

                                style.Style.VertAlignment = DevExpress.Utils.VertAlignment.Top;

                                style.NeedChange = column.ColorNeedChange;

                                arrStyles.Add(style);
                            }

                            if (this._GridInfo.SortingFrequence == SortingFrequence.ShowAfterColumns)
                            {
                                style.Style = this.ColGroup.Style;

                                style.Style.VertAlignment = DevExpress.Utils.VertAlignment.Top;

                                style.NeedChange = this.ColGroup.ColorNeedChange;

                                arrStyles.Add(style);
                            }
                            #endregion
                        }
                       
                    }
                    #endregion
                }
                else
                {

                    #region Matrix Group

                    for (int i = 0; i < sepCount; i++)
                    {

                        if (!this.ColGroup.FollowSummaries)   //Summaries after grop
                        {
                            style.Style = this.ColGroup.Style;

                            style.Style.VertAlignment = DevExpress.Utils.VertAlignment.Top;

                            style.NeedChange = this.ColGroup.ColorNeedChange;

                            if (ColGroup.Summaries.Count > 0) style.Style.Sides &= ~DevExpress.XtraPrinting.BorderSide.Right;

                            arrStyles.Add(style);
                        }

                        for(int j=0;j<this.ColGroup.Summaries.Count;j++)
                        {
                            GroupSummary summary = this.ColGroup.Summaries[j];
                      
                            style.Style = summary.Style;

                            style.Style.VertAlignment = DevExpress.Utils.VertAlignment.Top;

                            style.NeedChange = summary.ColorNeedChange;

                            if (!this.ColGroup.FollowSummaries)
                            {
                                style.Style.Sides &= ~DevExpress.XtraPrinting.BorderSide.Left;

                                if (j != ColGroup.Summaries.Count - 1) style.Style.Sides &= ~DevExpress.XtraPrinting.BorderSide.Right;
                            }
                            else
                            {
                                if (j != 0) style.Style.Sides &= ~DevExpress.XtraPrinting.BorderSide.Left;

                                 style.Style.Sides &= ~DevExpress.XtraPrinting.BorderSide.Right;
                            }

                            arrStyles.Add(style);
                        }

                        if (this.ColGroup.FollowSummaries)   //Summaries before grop
                        {
                            style.Style = this.ColGroup.Style;

                            style.Style.VertAlignment = DevExpress.Utils.VertAlignment.Top;

                            style.NeedChange = this.ColGroup.ColorNeedChange;

                            if (ColGroup.Summaries.Count > 0) style.Style.Sides &= ~DevExpress.XtraPrinting.BorderSide.Left;

                            arrStyles.Add(style);
                        }

                    }

                    #endregion
                }

                if (this._CellTotal.ShowTotal && !this._CellTotal.ShowFront && this._TotalCellVertical)
                {
                    style.Style = this.CellTotal.Style.Copy();

                    style.NeedChange = this.CellTotal.NeedChange;

                    arrStyles.Add(style);
                }

                return arrStyles;
            }

        }

        #endregion

        #region Calculate Result
        private SectionGroupInfo ReduceGroupInfo(DataTable i_Table, Int32Collection i_Rows, GroupInfo groupinfo, int nStart, int nCount)
        {
            SectionGroupInfo m_SectionInfo;
            if (groupinfo is FieldGroupInfo)
            {
                m_SectionInfo = (groupinfo as FieldGroupInfo).IntoSections(i_Table, i_Rows, groupinfo);
            }
            else
            {
                m_SectionInfo = (groupinfo as SectionGroupInfo);
            }

            m_SectionInfo.SortSections(i_Table, i_Rows);

            m_SectionInfo = m_SectionInfo.SubSections(nStart, nCount);

            return m_SectionInfo;
        }


        public void CalculateMatrixResult(DataTable i_Table, Int32Collection i_Rows)
        {
            Int32Collection i_ColLimitRows = i_Rows;
            Int32Collection i_RowLimitRows = i_Rows;

            SectionGroupInfo m_SectionInfo = null;

            if (Rect.Y + this.Rect.Height > 0)
            {
                m_SectionInfo = this.ReduceGroupInfo(i_Table, i_Rows, this.ColGroup, Rect.Y, Rect.Height);     //Change FieldGroupinfo into SectionGroupInfo and calculate Location and Size

                if (m_SectionInfo != null)
                {
                    i_ColLimitRows = m_SectionInfo.GetFilteredRows(i_Table, i_Rows);

                    this._ColGroup = m_SectionInfo;
                }
            }

            if (Rect.X + Rect.Width > 0)
            {
                m_SectionInfo = this.ReduceGroupInfo(i_Table, i_Rows, this._RowGroup, Rect.X, Rect.Width);

                if (m_SectionInfo != null)
                {
                    i_RowLimitRows = this._ColGroup.Filter.GetFilteredRows(i_Table, i_Rows);

                    i_RowLimitRows = m_SectionInfo.GetFilteredRows(i_Table, i_RowLimitRows);

                    this._RowGroup = m_SectionInfo;
                }
            }

            #region Calcualte Group
                _DisGroup.Summaries = _ColGroup.Summaries.CopyStructure();

                _SepGroup.Summaries = CellTotal.TotalSummaries.CopyStructure();
                _SepGroup.SubGroupInfos = new GroupInfoCollection();
                _SepGroup.SubGroupInfos.Add(_DisGroup);



                _RowGroup.Summaries = CellTotal.TotalSummaries.CopyStructure();
                _RowGroup.SubGroupInfos = new GroupInfoCollection();
                _RowGroup.SubGroupInfos.Add(_SepGroup);

                _ColGroup.SubGroupInfos = new GroupInfoCollection();
                _ColGroup.SubGroupInfos.Add(_RowGroup);

                _ColGroup.CalculateGroupResult(i_Table, i_Rows, i_Rows, i_Rows, _ColGroup);
            #endregion

            #region Calculate For toal
                GroupInfo totalColSubGroup = _SepGroup.Copy();

                totalColSubGroup.Summaries = ColTotal.TotalSummaries.CopyStructure();

                _TotalColGroup = _ColGroup.Copy();  // Total col Group  which would caluate the totalrow for each column
                _TotalColGroup.SubGroupInfos = new GroupInfoCollection();
                _TotalColGroup.Summaries = ColTotal.TotalSummaries.CopyStructure(); ;  //Add at 2009-2-19 9:21:53@Simon
                _TotalColGroup.SubGroupInfos.Add(totalColSubGroup);


                GroupInfo totalRowSubGroup = _SepGroup.Copy();
                totalRowSubGroup.Summaries = RowTotal.TotalSummaries.CopyStructure();

                _TotalRowGroup = RowGroup.Copy();     // Total col Group  which would caluate the totalcolumn for each row
                _TotalRowGroup.SubGroupInfos = new GroupInfoCollection();
                _TotalRowGroup.Summaries = RowTotal.TotalSummaries.CopyStructure();
                _TotalRowGroup.SubGroupInfos.Add(totalRowSubGroup);            

                if (this.RowTotal.Relative)
                {
                    _TotalRowGroup.CalculateGroupResult(i_Table, i_Rows, i_Rows, i_ColLimitRows, _TotalRowGroup);
                }
                else
                {
                    _TotalRowGroup.CalculateGroupResult(i_Table, i_Rows, i_Rows, i_Rows, _TotalRowGroup);
                }
                if (this.ColTotal.Relative)
                {
                    _TotalColGroup.CalculateGroupResult(i_Table, i_Rows, i_Rows, i_RowLimitRows, _TotalColGroup);
                }
                else
                {
                    _TotalColGroup.CalculateGroupResult(i_Table, i_Rows, i_Rows, i_Rows, _TotalColGroup);
                }
           #endregion

        }

        #endregion       
        
		#region HeaderValue
			public void SetHeaderValue(IMultiHeader i_View,IWebbTable table,ref int nRow, ref int nCol)
			{
				IWebbTableCell cell=null;

                int OriginalStartCol = nCol;

                #region Set FirstRow

                cell = table.GetCell(nRow, nCol);

                cell.Text = this._ColGroup.ColumnHeading;  

                nCol++;

                if (this.HaveSeprateHeader && this.ShowInOneCol) nCol++;

				if(_ColGroup.GroupResults!=null)
				{
					if(this.RowTotal.ShowTotal&&this.RowTotal.ShowFront)
					{
						cell=table.GetCell(nRow,nCol);

						cell.Text =RowTotal.HeaderTitle.ToString();  

						nCol++;
					}


					foreach(GroupResult m_Result in _ColGroup.GroupResults)
					{
						if( m_Result.GroupValue.ToString()=="[NoName]") m_Result.GroupValue=string.Empty;

						cell=table.GetCell(nRow,nCol);

						cell.Text = m_Result.GroupValue.ToString(); 
		                    
						cell.CellStyle.StringFormat=_ColGroup.HeadingFormat;
			                     
						int count=this.ColSteps;							

						GroupInfo RowInfo=ColGroup.GroupResults[0].SubGroupInfos[0];

						GroupInfo SepInfo=RowInfo.GroupResults[0].SubGroupInfos[0];

						if(!this._ShowInOneCol)count*=SepInfo.GroupResults.Count;

                        if (this.TotalCellVertical && this.CellTotal.ShowTotal) count += 1;
                       
                        if (count > 1) table.MergeCells(nRow, nRow, nCol, nCol + count - 1);  //Added this code at 2009-2-6 13:59:44@Simon                       
		                            
						nCol+=count;
					}
					if(this.RowTotal.ShowTotal&&!this.RowTotal.ShowFront)
					{
						cell=table.GetCell(nRow,nCol);

						if(cell!=null)cell.Text =RowTotal.HeaderTitle.ToString();  
					}
                }

                nRow++;
                #endregion

                if (this.HaveSeprateHeader && !this.ShowInOneCol) SetSeprateHeaderInRow(table, ref nRow, OriginalStartCol);   //Set Seprate Header

            }
			public void GetHeaderValue(ArrayList prnHeaders,ArrayList formats, ref int nCol)
			{
                prnHeaders[nCol] = this._ColGroup.ColumnHeading;

                formats[nCol] = this._ColGroup.HeadingFormat;

				nCol++;

				if(_ColGroup.GroupResults!=null)
				{
					if(this.RowTotal.ShowTotal&&this.RowTotal.ShowFront)
					{
						prnHeaders[nCol]=RowTotal.HeaderTitle.ToString();  

						nCol++;
					}

					foreach(GroupResult m_Result in _ColGroup.GroupResults)
					{
						if( m_Result.GroupValue.ToString()=="[NoName]") m_Result.GroupValue=string.Empty;

						prnHeaders[nCol] = m_Result.GroupValue.ToString();  
		                   
						formats[nCol]=_ColGroup.HeadingFormat;
			                     
						int count=this.ColSteps;						

						GroupInfo RowInfo=ColGroup.GroupResults[0].SubGroupInfos[0];

						GroupInfo SepInfo=RowInfo.GroupResults[0].SubGroupInfos[0];

						if(!this._ShowInOneCol)count*=SepInfo.GroupResults.Count;	
							
						for(int i=0;i<count-1;i++)
						{  
							nCol++; 

							prnHeaders[nCol]="\n";
						} 
		   
						nCol++; 					
					}
					if(this.RowTotal.ShowTotal&&!this.RowTotal.ShowFront)
					{					
						prnHeaders[nCol]=RowTotal.HeaderTitle.ToString();  
					}     

				}
						
			}

            public void SetSeprateHeaderInRow(IWebbTable table,ref int nRow, int nCol)
            {
                IWebbTableCell cell = null;

                cell = table.GetCell(nRow, nCol);

                if (cell != null) cell.Text = this.RowGroup.ColumnHeading;      

                nCol++;   // The Row Group

                if (_ColGroup.GroupResults != null)
                {
                    if (this.RowTotal.ShowTotal && this.RowTotal.ShowFront)
                    {
                        cell = table.GetCell(nRow, nCol);

                        if (cell != null) cell.Text = this.SepGroup.ColumnHeading;     

                        nCol++;  //RowToal Column
                    }
                    
                    foreach (GroupResult m_Result in _ColGroup.GroupResults)
                    {                 
                        int count = this.ColSteps;

                        GroupInfo RowInfo = m_Result.SubGroupInfos[0];

                        GroupInfo SepInfo = RowInfo.GroupResults[0].SubGroupInfos[0];

                        if (this.TotalCellVertical && this.CellTotal.ShowTotal && this.CellTotal.ShowFront) nCol++;

                        foreach (GroupResult sepResult in SepInfo.GroupResults)
                        {
                           if (sepResult.GroupValue.ToString() == "[NoName]") sepResult.GroupValue = string.Empty;

                            cell = table.GetCell(nRow, nCol);

                            if(cell!=null)cell.Text = sepResult.GroupValue.ToString();                             

                            if (count > 1) table.MergeCells(nRow, nRow, nCol, nCol + count - 1);  //Added this code at 2009-2-6 13:59:44@Simon

                            nCol += count;

                        }
                        if (this.TotalCellVertical && this.CellTotal.ShowTotal && !this.CellTotal.ShowFront) nCol++;
                        
                    }                   
                    nRow++;
                }

            }

            public void SetSeprateHeaderInColumn(IWebbTable table, int nRow, ref int nCol)
            {
                IWebbTableCell cell = null;
              
                if (_ColGroup.GroupResults != null)
                {
                    GroupInfo RowInfo = ColGroup.GroupResults[0].SubGroupInfos[0];

                    foreach (GroupResult m_Result in RowInfo.GroupResults)
                    {
                        int count = 1;

                        if (this.CellTotal.ShowTotal &&!this.TotalCellVertical) count += 1;

                        GroupInfo SepInfo = m_Result.SubGroupInfos[0];

                        if (this.TotalCellVertical && this.CellTotal.ShowTotal && this.CellTotal.ShowFront) nCol++;

                        foreach (GroupResult sepResult in SepInfo.GroupResults)
                        {
                            if (sepResult.GroupValue.ToString() == "[NoName]") sepResult.GroupValue = string.Empty;

                            cell = table.GetCell(nRow, nCol);

                            cell.Text = sepResult.GroupValue.ToString();

                            if (count > 1) table.MergeCells(nRow, nRow + count - 1, nCol, nCol);  //Added this code at 2009-2-6 13:59:44@Simon

                            nRow += count;
                        }                        
                    }

                    nCol++;
                }

            }
		#endregion

		#region Set RowsValue
		 public void SetMatrixRowsValue(IWebbTable table,ref int i_Row,ref int i_Col,Int32Collection TotalRows)
		{
            if (this.ColTotal.ShowFront)    //Set the total row at front of each col 
			{
				if(this.ColTotal.ShowTotal)
				{
					TotalRows.Add(i_Row);

					table.GetCell(i_Row,i_Col).Text =ColTotal.HeaderTitle.ToString();  
				}

                int totalStartCol = i_Col + 1;

                if (this.HaveSeprateHeader && this.ShowInOneCol) totalStartCol++;

                this.SetColTotalValue(table, ref i_Row, totalStartCol);   // print Total of each col 				
			}

            int m_OriginalStartCol = i_Col;

			int m_OriginalStartRow = i_Row;

			GroupResultCollection rootResults=this.ColGroup.GroupResults;

			if(rootResults==null||rootResults.Count==0)return;		
	
			 #region SetValue of First Column  Row Group's Value
			GroupResultCollection rowResults=this._TotalRowGroup.GroupResults;

			int offset=0;

	        TotalCellRows=new Int32Collection();

			for(int nRow=0;nRow<rowResults.Count;nRow++)
			{
				GroupResult m_GroupResult=rowResults[nRow];	
				
				int mergeCount=1;
			
				if(this.ShowInOneCol)mergeCount=m_GroupResult.SubGroupInfos[0].GroupResults.Count;	
			
				if(this.CellTotal.ShowTotal&&!this._TotalCellVertical)
				{	
					mergeCount*=2;

					for(int j=0;j<mergeCount;j+=2)
					{
						if(CellTotal.ShowFront)
						{						
							TotalRows.Add(offset+i_Row+j);
							TotalCellRows.Add(offset+i_Row+j);
						}
						else
						{
							TotalRows.Add(offset+i_Row+1+j);
							TotalCellRows.Add(offset+i_Row+1+j);
						}
					}							
				}

				table.MergeCells(offset+i_Row,offset+i_Row+mergeCount-1,i_Col,i_Col);  	

				#region set grouped value
				string subvalue=this.TotalRowGroupValue(m_GroupResult);

				if(m_GroupResult.ClickEvent==ClickEvents.PlayVideo)
				{
					WebbTableCellHelper.SetCellValueWithClickEvent(table,offset+i_Row,i_Col,subvalue,FormatTypes.String,m_GroupResult.RowIndicators);
				}
				else
				{
					WebbTableCellHelper.SetCellValue(table,offset+i_Row, i_Col,subvalue, FormatTypes.String);
				}	
				#endregion

				offset+=mergeCount;

			}	
			#endregion
				
			i_Col++;

            if (this.HaveSeprateHeader && this.ShowInOneCol) SetSeprateHeaderInColumn(table, m_OriginalStartRow, ref i_Col);   //Set Seprate Header

			if(this.RowTotal.ShowFront)
			{
				this.SetRowTotalValue(table,i_Row,ref i_Col);  // print Total of each row 
			}

			 foreach(GroupResult m_GroupResult in rootResults)
			 {
				 int temp=i_Col;
				
				i_Row=m_OriginalStartRow ;					
				            
				foreach(GroupResult m_subResult in m_GroupResult.SubGroupInfos[0].GroupResults)
				{
					int nCol=i_Col;

                    if (this._TotalCellVertical)
                    {
                        if (!this._ShowInOneCol)
                        {
                            this.VerticaSetCellValueInMultiColumn(table, ref i_Row, ref nCol, m_subResult);
                        }
                        else
                        {
                            this.VerticalSetCellValueInOneColumn(table, ref i_Row, ref nCol, m_subResult);
                        }
                    }
                    else
                    {
                        if (!this._ShowInOneCol)
                        {
                            this.SetCellValueInMultiColumn(table, ref i_Row, ref nCol, m_subResult);
                        }
                        else
                        {
                            this.SetCellValueInOneColumn(table, ref i_Row, ref nCol, m_subResult);
                        }
                    }

					temp=nCol;				
				}
				
				i_Col=temp;	
		
			 }
						
			if(!this.ColTotal.ShowFront)
			{
				if(this.ColTotal.ShowTotal)
				{
					TotalRows.Add(i_Row);

					table.GetCell(i_Row,m_OriginalStartCol).Text =ColTotal.HeaderTitle.ToString();  
				}	
                int StartCol=m_OriginalStartCol+1;

                if (this.HaveSeprateHeader && this.ShowInOneCol)StartCol++;

                this.SetColTotalValue(table, ref i_Row, StartCol);   // print Total of each col 				
					
			}
			if(!this.RowTotal.ShowFront)
			{
				this.SetRowTotalValue(table,m_OriginalStartRow,ref i_Col);  // print Total of each row 
			}
		}
		
    	#endregion

		#region SetCellValue		
		private void SetCellValueInMultiColumn(IWebbTable table,ref int i_Row,ref int i_Col,GroupResult gr)
		{
			GroupInfo Sepgroup=gr.SubGroupInfos[0];   //get Seprate Group

			int o_Row=i_Row;
			int o_Col=i_Col;			
		
        
			if(this.CellTotal.ShowTotal&&this.CellTotal.ShowFront&&!this.CellTotal.MinorTicks)
			{				
				SetCellTotal(table,ref o_Row,i_Col,gr,this.ColSteps*Sepgroup.GroupResults.Count);	
			}
            
			foreach(GroupResult sepResult in Sepgroup.GroupResults)
			{					
				i_Row=o_Row;

				if(this.CellTotal.ShowTotal&&this.CellTotal.ShowFront&&this.CellTotal.MinorTicks)
				{
					SetCellMinorTotal(table,ref i_Row,ref o_Col,sepResult,this.ColSteps);	
				}
                  
				if(this._MatrixDisplay==ComputedStyle.Group)
				{					
					this.SetGroupCellValue(table,i_Row,ref i_Col, sepResult);
				}
				else
				{
					this.SetGridCellValue(table,i_Row,ref i_Col, sepResult);
				}

				i_Row++;

				if(this.CellTotal.ShowTotal&&!this.CellTotal.ShowFront&&this.CellTotal.MinorTicks)
				{				
					SetCellMinorTotal(table,ref i_Row,ref o_Col,sepResult,this.ColSteps);	
				}	
			}	
			if(this.CellTotal.ShowTotal&&!this.CellTotal.ShowFront&&!this.CellTotal.MinorTicks)
			{
				SetCellTotal(table,ref i_Row,o_Col,gr,this.ColSteps*Sepgroup.GroupResults.Count);	
			}		

		}
	
		private void SetCellValueInOneColumn(IWebbTable table,ref int i_Row,ref int i_Col,GroupResult gr)
		{
			GroupInfo Sepgroup=gr.SubGroupInfos[0];   //get Seprate Group

			int o_Row=i_Row;
			int o_Col=i_Col;			

			foreach(GroupResult sepResult in Sepgroup.GroupResults)
			{	
				o_Col=i_Col;

				if(this.CellTotal.ShowTotal&&this.CellTotal.ShowFront)
				{
					SetCellMinorTotal(table,ref i_Row,ref o_Col,sepResult,this.ColSteps);
				}
                
				if(this._MatrixDisplay==ComputedStyle.Group)
				{
					o_Col=i_Col;

					this.SetGroupCellValue(table,i_Row,ref o_Col, sepResult);
				}
				else
				{
					o_Col=i_Col;

					this.SetGridCellValue(table,i_Row,ref o_Col, sepResult);
				}				

				i_Row++;	          

				if(this.CellTotal.ShowTotal&&!this.CellTotal.ShowFront)
				{	
					o_Col=i_Col;	
		
					SetCellMinorTotal(table,ref i_Row,ref o_Col,sepResult,this.ColSteps);		
				}	
			}			

			i_Col=o_Col;

		}

        private void VerticaSetCellValueInMultiColumn(IWebbTable table, ref int i_Row, ref int i_Col, GroupResult gr)
        {
            GroupInfo Sepgroup = gr.SubGroupInfos[0];   //get Seprate Group

            int o_Row = i_Row;
            int o_Col = i_Col;


            if (this.CellTotal.ShowTotal && this.CellTotal.ShowFront)
            {
                SetCellTotalInCol(table, o_Row, ref i_Col, gr, 1);
            }

            foreach (GroupResult sepResult in Sepgroup.GroupResults)
            {
                if (this._MatrixDisplay == ComputedStyle.Group)
                {
                    this.SetGroupCellValue(table, i_Row, ref i_Col, sepResult);
                }
                else
                {
                    this.SetGridCellValue(table, i_Row, ref i_Col, sepResult);
                }

                            
            }
            if (this.CellTotal.ShowTotal && !this.CellTotal.ShowFront)
            {
                SetCellTotalInCol(table, i_Row, ref i_Col, gr, 1);
            }

            i_Row++;

        }

        private void VerticalSetCellValueInOneColumn(IWebbTable table, ref int i_Row, ref int i_Col, GroupResult gr)
        {
            GroupInfo Sepgroup = gr.SubGroupInfos[0];   //get Seprate Group

            int o_Row = i_Row;
            int o_Col = i_Col;

            if (this.CellTotal.ShowTotal && this.CellTotal.ShowFront)
            {
                SetCellTotalInCol(table, o_Row, ref i_Col, gr, Sepgroup.GroupResults.Count);
            }

            foreach (GroupResult sepResult in Sepgroup.GroupResults)
            {
                o_Col = i_Col;            

                if (this._MatrixDisplay == ComputedStyle.Group)
                {
                    this.SetGroupCellValue(table, i_Row, ref o_Col, sepResult);
                }
                else
                {
                   
                    this.SetGridCellValue(table, i_Row, ref o_Col, sepResult);
                }

                i_Row++;
               
            }

            if (this.CellTotal.ShowTotal &&!this.CellTotal.ShowFront)
            {
                SetCellTotalInCol(table, o_Row, ref o_Col, gr, Sepgroup.GroupResults.Count);
            }

            i_Col = o_Col;
        }
	
		#endregion

		#region SetDisPlayGroupValue
		private void SetGroupCellValue(IWebbTable table,int i_Row,ref int i_Col,GroupResult sepResult)
		{		
			GroupInfo disgroup=sepResult.SubGroupInfos[0];   //get Seprate Group
				           
			disgroup.GroupResults.Sort(disgroup.Sorting,disgroup.SortingBy,disgroup.UserDefinedOrders);

			string strValue=this.CalcDisplayValue(sepResult);

            if (!this.ColGroup.FollowSummaries)   //summaries after group
            {
                if (sepResult.ClickEvent == ClickEvents.PlayVideo)
                {
                    WebbTableCellHelper.SetCellValueWithClickEvent(table, i_Row, i_Col, strValue, FormatTypes.String, sepResult.RowIndicators);
                }
                else
                {
                    WebbTableCellHelper.SetCellValue(table, i_Row, i_Col, strValue, FormatTypes.String);
                }
                i_Col++;
            }
				                
			this.SetDisplaySummary(table,ref i_Row,ref i_Col,sepResult);

            if (this.ColGroup.FollowSummaries)   //summaries before group
            {
                if (sepResult.ClickEvent == ClickEvents.PlayVideo)
                {
                    WebbTableCellHelper.SetCellValueWithClickEvent(table, i_Row, i_Col, strValue, FormatTypes.String, sepResult.RowIndicators);
                }
                else
                {
                    WebbTableCellHelper.SetCellValue(table, i_Row, i_Col, strValue, FormatTypes.String);
                }
                i_Col++;
            }

		}

		public string CalcDisplayValue(GroupResult gr)
		{
			GroupInfo disgroup=gr.SubGroupInfos[0];   //get Seprate Group
			       
			StringBuilder sb=new StringBuilder();

			bool once=true;

			bool allSummariesShow=false;

			foreach(GroupSummary m_Summary in disgroup.Summaries)
			{
				if(m_Summary.ShowZeros)
				{
					allSummariesShow=true;

					break;
				}
			}
				           
			foreach(GroupResult disResult in disgroup.GroupResults)
			{
				if(disResult.RowIndicators.Count==0&&!allSummariesShow)continue;

                object objValue = CResolveFieldValue.GetResolveValue(disgroup, disResult.GroupValue);

                string strValue = objValue.ToString().Trim(" \n".ToCharArray());

				if(once||strValue==string.Empty)
				{
					sb.Append(strValue);

					once=false;
				}
				else
				{
					sb.Append("\n"+strValue);
				}	
				
			}

			return sb.ToString();


		}
		public void SetDisplaySummary(IWebbTable table,ref int i_Row,ref int i_Col,GroupResult sepResult)
		{
			GroupInfo disgroup=sepResult.SubGroupInfos[0];   //get Seprate Group
		     
			for(int i=0;i<disgroup.Summaries.Count;i++)
			{
				bool once=true;	

				StringBuilder sb=new StringBuilder();

				foreach(GroupResult disResult in  disgroup.GroupResults)
				{
					GroupSummary summary=disResult.Summaries[i];

					if(disResult.RowIndicators.Count==0&&!summary.ShowZeros)continue;            
																						
					string strValue=WebbTableCellHelper.FormatValue(null,summary);

					if(once)
					{
						sb.Append(strValue);

						once=false;
					}
					else
					{
						sb.Append("\n"+strValue);
					}		
				}

				if(sepResult.ClickEvent==ClickEvents.PlayVideo)
				{
					WebbTableCellHelper.SetCellValueWithClickEvent(table,i_Row,i_Col,sb.ToString(),FormatTypes.String,sepResult.RowIndicators);
				}
				else
				{
					WebbTableCellHelper.SetCellValue(table,i_Row,i_Col,sb.ToString(),FormatTypes.String);
				}						
				i_Col++;
			}		    
							
		}
		#endregion	

		#region Set DisplayGrid Value
		private Int32Collection GridTopRows(Int32Collection rows)
		{
			if(this.GridInfo.TopCount==0||rows.Count<=this.GridInfo.TopCount)return rows;

			Int32Collection toprows=new Int32Collection();
	            
			for(int i=0;i<this.GridInfo.TopCount;i++)
			{
				toprows.Add(i);
			}
			return toprows;
				
		}
		private StringCollection GetRowsValue(DataTable dt,GroupResultCollection SortingResults,GroupResult gr)
		{
			StringCollection cellsValue=new StringCollection();

			Int32Collection filterRows=new Int32Collection();

			StringBuilder sbFreValue=new StringBuilder();

			string strValue=string.Empty;

			int totalresult=0;

			if(SortingResults!=null)
			{					
				foreach(GroupResult m_result in SortingResults)
				{
					int resultCount=m_result.RowIndicators.Count;

					for(int i=0;i<resultCount;i++)
					{
						totalresult++;

						if(this.GridInfo.TopCount>0&&totalresult>=this.GridInfo.TopCount)
						{
							break;
						}

						if(i==0)
						{
							sbFreValue.Append(resultCount.ToString()+"\n");
						}
						else
						{					
							sbFreValue.Append("\n");
						}
						filterRows.Add(m_result.RowIndicators[i]);
					}
					
				}	
			}
			else
			{
				filterRows=this.GridTopRows(gr.RowIndicators);                    
			}

			if(this.GridInfo.SortingFrequence==SortingFrequence.ShowBeforeColumns)
			{
				cellsValue.Add(sbFreValue.ToString());
			}
				
			foreach(GridColumn col in this.GridInfo.Columns)
			{
				if(!PublicDBFieldConverter.AvialableFields.Contains(col.Field)||col.Field == string.Empty) 
				{
					cellsValue.Add(string.Empty);

					continue;
				}
				StringBuilder sbFieldValue=new StringBuilder();

				foreach(int row in filterRows)
				{
                    object objValue = CResolveFieldValue.GetResolveValue(col, dt.Rows[row][col.Field]);

                    strValue = objValue.ToString();

					sbFieldValue.Append(strValue+"\n");
				}

				strValue=sbFieldValue.ToString().TrimEnd(" \n".ToCharArray());

				cellsValue.Add(strValue);
			}

			if(this.GridInfo.SortingFrequence==SortingFrequence.ShowAfterColumns)
			{
				cellsValue.Add(sbFreValue.ToString());
			}
			return cellsValue;
			
		}
		
		private void SetGridCellValue(IWebbTable table,int i_Row,ref int i_Col,GroupResult sepResult)
		{
			DataSet ds = Webb.Reports.DataProvider.VideoPlayBackManager.DataSource;
			
			if(ds == null || ds.Tables.Count <= 0) return ;

			DataTable dt = ds.Tables[0];

			GroupResultCollection SortingResults=this._GridInfo.Sorting(dt,sepResult.RowIndicators);
				
			StringCollection cellsValue=this.GetRowsValue(dt,SortingResults,sepResult);

			if(cellsValue.Count==0)cellsValue.Add(string.Empty);

			foreach(string strValue in cellsValue)
			{
				if(sepResult.ClickEvent==ClickEvents.PlayVideo)
				{
					WebbTableCellHelper.SetCellValueWithClickEvent(table,i_Row,i_Col,strValue,FormatTypes.String,sepResult.RowIndicators);
				}
				else
				{
					WebbTableCellHelper.SetCellValue(table,i_Row,i_Col,strValue,FormatTypes.String);
				}
				i_Col++;
			}
				
		}
		#endregion

		#region Set TotalValue

		 #region SetCellTotal

            #region  Cell Total is  not Vertical 
                protected void SetCellMinorTotal(IWebbTable table, ref int i_Row, ref int i_Col, GroupResult gr, int MergeSteps)
                {
                    if (this.CellTotal.ShowTotal)
                    {
                        IWebbTableCell cell = table.GetCell(i_Row, i_Col);

                        if (gr.Summaries.Count > 0)
                        {
                            if (gr.RowIndicators.Count != 0 || CellTotal.ShowZero)
                            {
                                string totalResult = CellTotal.TotalResult(gr);

                                if (totalResult != null)
                                {
                                    totalResult.Replace(@"\n", "\n");
                                }
                                if (gr.ClickEvent == ClickEvents.PlayVideo)
                                {
                                    WebbTableCellHelper.SetCellValueWithClickEvent(table, i_Row, i_Col, totalResult, FormatTypes.String, gr.RowIndicators);
                                }
                                else
                                {
                                    WebbTableCellHelper.SetCellValue(table, i_Row, i_Col, totalResult, FormatTypes.String);
                                }
                            }
                        }

                        table.MergeCells(i_Row, i_Row, i_Col, i_Col + MergeSteps - 1);

                        i_Col += MergeSteps;

                        i_Row++;
                    }
                }
                protected void SetCellTotal(IWebbTable table, ref int i_Row, int i_Col, GroupResult gr, int MergeSteps)
                {
                    if (this.CellTotal.ShowTotal)
                    {
                        IWebbTableCell cell = table.GetCell(i_Row, i_Col);

                        if (gr.Summaries.Count > 0)
                        {
                            if (gr.RowIndicators.Count != 0 || CellTotal.ShowZero)
                            {
                                string totalResult = CellTotal.TotalParentResult(gr);

                                if (totalResult != null)
                                {
                                    totalResult.Replace(@"\n", "\n");
                                }

                                if (gr.ClickEvent == ClickEvents.PlayVideo)
                                {
                                    WebbTableCellHelper.SetCellValueWithClickEvent(table, i_Row, i_Col, totalResult, FormatTypes.String, gr.RowIndicators);
                                }
                                else
                                {
                                    WebbTableCellHelper.SetCellValue(table, i_Row, i_Col, totalResult, FormatTypes.String);
                                }
                            }
                        }

                        table.MergeCells(i_Row, i_Row, i_Col, i_Col + MergeSteps - 1);

                        i_Row++;
                    }
                }	        		    
            #endregion

            #region Total Cell is Vertical
                protected void SetCellTotalInCol(IWebbTable table, int i_Row,ref int i_Col, GroupResult gr, int MergeSteps)
                {
                    if (this.CellTotal.ShowTotal)
                    {

                       if(this.TotalCellRows.Contains(i_Col))this.TotalCellRows.Add(i_Col);
                        
                        IWebbTableCell cell = table.GetCell(i_Row, i_Col);

                        if (gr.Summaries.Count > 0)
                        {
                            if (gr.RowIndicators.Count != 0 || CellTotal.ShowZero)
                            {
                                string totalResult = CellTotal.ColumnResult(gr);

                                if (totalResult != null)
                                {
                                    totalResult.Replace(@"\n", "\n");

                                    if (gr.ParentGroupInfo != null && gr.ParentGroupInfo.ParentGroupResult != null)
                                    {
                                        totalResult=totalResult.Replace(@"[colgroupresult]", gr.ParentGroupInfo.ParentGroupResult.GroupValue.ToString());
                                        totalResult=totalResult.Replace(@"[COLGROUPRESULT]", gr.ParentGroupInfo.ParentGroupResult.GroupValue.ToString());
                                    }
                                }

                                if (gr.ClickEvent == ClickEvents.PlayVideo)
                                {
                                    WebbTableCellHelper.SetCellValueWithClickEvent(table, i_Row, i_Col, totalResult, FormatTypes.String, gr.RowIndicators);
                                }
                                else
                                {
                                    WebbTableCellHelper.SetCellValue(table, i_Row, i_Col, totalResult, FormatTypes.String);
                                }
                            }                       
                        }

                        if (MergeSteps > 1) table.MergeCells(i_Row, i_Row + MergeSteps - 1, i_Col, i_Col);

                        i_Col++;
                    }
                }
            #endregion
         #endregion

        private string TotalRowGroupValue(GroupResult result)
		{			
			string strValue=result.GroupValue.ToString().Trim();

			if(result.GroupValue.ToString()=="[NoName]")strValue=string.Empty;

			return strValue; 

		}
		protected void SetRowTotalValue(IWebbTable table, int i_Row,ref int i_Col)   //print total of each row
		{			
			if(!this._RowTotal.ShowTotal) return;
			
			foreach(GroupResult gr in this._TotalRowGroup.GroupResults)
			{
				if(gr.Summaries.Count>0)
				{
					if(gr.RowIndicators.Count!=0||_RowTotal.ShowZero)
					{
						string totalResult=RowTotal.ColumnResult(gr);

						if(totalResult!=null)
						{
							totalResult.Replace(@"\n","\n");
						}

						if(gr.ClickEvent==ClickEvents.PlayVideo)
						{	            
							WebbTableCellHelper.SetCellValueWithClickEvent(table,i_Row,i_Col,totalResult,FormatTypes.String,gr.RowIndicators);
						}
						else
						{
							WebbTableCellHelper.SetCellValue(table,i_Row,i_Col,totalResult,FormatTypes.String);
						}
					}
				}
					
				int mergeCount=1;
		
				if(this.ShowInOneCol)mergeCount=gr.SubGroupInfos[0].GroupResults.Count;	
		
				if(this.CellTotal.ShowTotal&&!this._TotalCellVertical)
				{					 		    
					mergeCount*=2;
				} 
				
				table.MergeCells(i_Row,i_Row+mergeCount-1,i_Col,i_Col);												
				
				i_Row+=mergeCount;
			
			}
			i_Col++;	
						
		}
		protected void SetColTotalValue(IWebbTable table,ref int i_Row,int i_Col)
		{	
			if(this.RowTotal.ShowTotal&&this.RowTotal.ShowFront)i_Col++;

			if(this._ColTotal.ShowTotal)
			{
				int MergeSteps=0; 

				if(this._ShowInOneCol)
				{
					#region SetColTotalValue  In Show In One Column-Mode
					foreach(GroupResult m_GroupResult in this._TotalColGroup.GroupResults)
					{
						MergeSteps=this.ColSteps;

                        if (this.TotalCellVertical && this.CellTotal.ShowTotal) MergeSteps++;

						table.MergeCells(i_Row,i_Row,i_Col,i_Col+MergeSteps-1);

						if(this._ColTotal.MinorTicks)
                        {
                            #region SetValue For MinorTicks

                            string totalResult=string.Empty;

							foreach(GroupResult gr in m_GroupResult.SubGroupInfos[0].GroupResults)
							{
								if(gr.Summaries.Count>0)
								{
									if(gr.RowIndicators.Count!=0||_ColTotal.ShowZero)
									{						
										totalResult=totalResult+"\n"+_ColTotal.TotalResult(gr);
						
										if(totalResult!=null)
										{
											totalResult.Replace(@"\n","\n");
										}										
									}
								}									
							}
                            totalResult=totalResult.Trim("\n ".ToCharArray());

							if(m_GroupResult.ClickEvent==ClickEvents.PlayVideo)
							{	            
								WebbTableCellHelper.SetCellValueWithClickEvent(table,i_Row,i_Col,totalResult,FormatTypes.String,m_GroupResult.RowIndicators);
							}
							else
							{
								WebbTableCellHelper.SetCellValue(table,i_Row,i_Col,totalResult,FormatTypes.String);
                            }
                            #endregion

                            i_Col =i_Col+MergeSteps;	
						}
						else
                        {
                            #region set Value for BigTicks
                            if (m_GroupResult.Summaries.Count>0)
							{
								if(m_GroupResult.RowIndicators.Count!=0||_ColTotal.ShowZero)
								{						
									string totalResult=_ColTotal.TotalResult(m_GroupResult);

									if(totalResult!=null)
									{
										totalResult.Replace(@"\n","\n");
									}
							
									if(m_GroupResult.ClickEvent==ClickEvents.PlayVideo)
									{	            
										WebbTableCellHelper.SetCellValueWithClickEvent(table,i_Row,i_Col,totalResult,FormatTypes.String,m_GroupResult.RowIndicators);
									}
									else
									{
										WebbTableCellHelper.SetCellValue(table,i_Row,i_Col,totalResult,FormatTypes.String);
									}
								}
                            }
                            #endregion

                            i_Col =i_Col+MergeSteps;		
						}
					}
					#endregion
				}
				else
				{
					#region SetColTotalValue With Mergedcells In Show Multicolumns-Mode
					foreach(GroupResult m_GroupResult in this._TotalColGroup.GroupResults)
					{
						if(this._ColTotal.MinorTicks)
                        {
                            #region SetValue For MinorTicks

                            MergeSteps = this.ColSteps;

                            if (this.TotalCellVertical && this.CellTotal.ShowTotal && this.CellTotal.ShowFront)
                            {
                                if (m_GroupResult.Summaries.Count > 0)
                                {
                                    if (m_GroupResult.RowIndicators.Count != 0 || _ColTotal.ShowZero)
                                    {
                                        string name = this.CellTotal.HeaderTitle.Replace("[result]", m_GroupResult.GroupValue.ToString());
                      
                                        string totalResult = _ColTotal.TotalResult(name, m_GroupResult);

                                        if (totalResult != null)
                                        {
                                            totalResult.Replace(@"\n", "\n");
                                        }

                                        if (m_GroupResult.ClickEvent == ClickEvents.PlayVideo)
                                        {
                                            WebbTableCellHelper.SetCellValueWithClickEvent(table, i_Row, i_Col, totalResult, FormatTypes.String, m_GroupResult.RowIndicators);
                                        }
                                        else
                                        {
                                            WebbTableCellHelper.SetCellValue(table, i_Row, i_Col, totalResult, FormatTypes.String);
                                        }
                                    }
                                }                           

                                i_Col += 1;
                            }
							foreach(GroupResult gr in m_GroupResult.SubGroupInfos[0].GroupResults)
							{
								table.MergeCells(i_Row,i_Row,i_Col,i_Col+MergeSteps-1);

								if(gr.Summaries.Count>0)
								{
									if(gr.RowIndicators.Count!=0||_ColTotal.ShowZero)
									{						
										string totalResult=_ColTotal.TotalResult(gr);

										if(totalResult!=null)
										{
											totalResult.Replace(@"\n","\n");
										}
							
										if(gr.ClickEvent==ClickEvents.PlayVideo)
										{	            
											WebbTableCellHelper.SetCellValueWithClickEvent(table,i_Row,i_Col,totalResult,FormatTypes.String,gr.RowIndicators);
										}
										else
										{
											WebbTableCellHelper.SetCellValue(table,i_Row,i_Col,totalResult,FormatTypes.String);
										}
									}
								}                               
								i_Col=i_Col+MergeSteps;
                            }
                            if (this.TotalCellVertical && this.CellTotal.ShowTotal && !this.CellTotal.ShowFront)
                            {
                                if (m_GroupResult.Summaries.Count > 0)
                                {
                                    if (m_GroupResult.RowIndicators.Count != 0 || _ColTotal.ShowZero)
                                    {
                                        string name = this.CellTotal.HeaderTitle.Replace("[result]", m_GroupResult.GroupValue.ToString());

                                        string totalResult = _ColTotal.TotalResult(name, m_GroupResult);

                                        if (totalResult != null)
                                        {
                                            totalResult.Replace(@"\n", "\n");
                                        }

                                        if (m_GroupResult.ClickEvent == ClickEvents.PlayVideo)
                                        {
                                            WebbTableCellHelper.SetCellValueWithClickEvent(table, i_Row, i_Col, totalResult, FormatTypes.String, m_GroupResult.RowIndicators);
                                        }
                                        else
                                        {
                                            WebbTableCellHelper.SetCellValue(table, i_Row, i_Col, totalResult, FormatTypes.String);
                                        }
                                    }
                                }                           

                                i_Col += 1;
                            }
                            #endregion
                        }
						else
                        {
                            #region SetValue For BigTicks
                            
                            MergeSteps =this.ColSteps;                           

                             MergeSteps *= m_GroupResult.SubGroupInfos[0].GroupResults.Count;

                             if (this.TotalCellVertical && this.CellTotal.ShowTotal) MergeSteps += 1;

							table.MergeCells(i_Row,i_Row,i_Col,i_Col+MergeSteps-1);

							if(m_GroupResult.Summaries.Count>0)
							{
								if(m_GroupResult.RowIndicators.Count!=0||_ColTotal.ShowZero)
								{						
									string totalResult=_ColTotal.TotalResult(m_GroupResult);

									if(totalResult!=null)
									{
										totalResult.Replace(@"\n","\n");
									}
							
									if(m_GroupResult.ClickEvent==ClickEvents.PlayVideo)
									{	            
										WebbTableCellHelper.SetCellValueWithClickEvent(table,i_Row,i_Col,totalResult,FormatTypes.String,m_GroupResult.RowIndicators);
									}
									else
									{
										WebbTableCellHelper.SetCellValue(table,i_Row,i_Col,totalResult,FormatTypes.String);
									}
								}
                            }
                            #endregion

                            i_Col =i_Col+MergeSteps;		
						}
					}
					#endregion
				}
				i_Row++;
			}
		}
		#endregion

        public void GetAllUsedFields(ref ArrayList _UsedFields)
        {
            if (this._SepGroup != null) this._SepGroup.GetAllUsedFields(ref _UsedFields);            
            if (this._DisGroup != null) this._DisGroup.GetAllUsedFields(ref _UsedFields);
            if (this.CellTotal != null) this.CellTotal.GetFieldUsed(ref _UsedFields);
            if (this.ColTotal != null) this.ColTotal.GetFieldUsed(ref _UsedFields);
            if (this.RowTotal != null) this.RowTotal.GetFieldUsed(ref _UsedFields);

            foreach (GridColumn col in this.GridInfo.Columns)
            {
                col.GetALLUsedFields(ref _UsedFields);
            }
        }

			
	}
	#endregion
	
}
