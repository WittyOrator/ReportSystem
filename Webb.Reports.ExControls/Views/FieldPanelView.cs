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
	#region Helpful Tyle for building HitChart Control
	[Serializable]
	public enum ComputedStyle:byte
	{
		Group,
		Grid
	}

	public interface IField
	{
		Point Position{get;set;}
		SizeF Ratio{get;set;}
		SectionFilter Filter{get;set;}
	}

	[Serializable]
	public class Field:ISerializable
	{
		public Field()
		{
			this._Position = Point.Empty;
			this._Ratio = SizeF.Empty;
			this._Filter = new SectionFilter();
            this._Filter.FilterName = "[NoName]";
			this._FieldID = -1;
		}

		private Point _Position;
		private SizeF _Ratio;
		private SectionFilter _Filter;
		private int _FieldID;

		[Browsable(false)]
		public Point Position
		{
			get{return this._Position;}
			set{this._Position = value;}
		}

		[Browsable(false)]
		public SizeF Ratio
		{
			get{return this._Ratio;}
			set{this._Ratio = value;}
		}

		[Browsable(false)]
		public SectionFilter Filter
		{
			get{return this._Filter;}
		}

		[Browsable(false)]
		public int FieldID
		{
			get{return this._FieldID;}
			set{this._FieldID = value;}
		}

		//fuctions
		public void SetPosition(int x, int y)
		{
			this._Position.X = x;
			this._Position.Y = y;
		}

		public void SetXPosition(int x)
		{
			this._Position.X = x;
		}

		public void SetYPosition(int y)
		{
			this._Position.Y = y;
		}

		public void SetWidthRatio(float wRatio)
		{
			this._Ratio.Width = wRatio;
		}

		public void SetHeightRatio(float hRatio)
		{
			this._Ratio.Height = hRatio;
		}

		public Field Copy()
		{
			Field field = new Field();

			field.Position = this._Position;

			field.Ratio = this._Ratio;

			field.Filter.Apply(this._Filter);

			return field;
		}

		#region Serialization By Macro 2008-12-11 14:48:40
		public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			info.AddValue("_Position",_Position,typeof(System.Drawing.Point));
			info.AddValue("_Ratio",_Ratio,typeof(System.Drawing.SizeF));
			info.AddValue("_Filter",_Filter,typeof(Webb.Reports.SectionFilter));
			info.AddValue("_FieldID",_FieldID,typeof(int));
		
		}

		public Field(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			try
			{
				_Position=(System.Drawing.Point)info.GetValue("_Position",typeof(System.Drawing.Point));
			}
			catch
			{
				this._Position = Point.Empty;
				
			}
			try
			{
				_Ratio=(System.Drawing.SizeF)info.GetValue("_Ratio",typeof(System.Drawing.SizeF));
			}
			catch
			{this._Ratio = SizeF.Empty;
				
			}
			try
			{
				_Filter=(Webb.Reports.SectionFilter)info.GetValue("_Filter",typeof(Webb.Reports.SectionFilter));
			}
			catch
			{
				this._Filter = new SectionFilter();
                this._Filter.FilterName = "[NoName]";
				
			}
			try
			{
				_FieldID=(int)info.GetValue("_FieldID",typeof(int));
			}
			catch
			{
				this._FieldID = -1;
			}
		}
		#endregion
	}

	[Serializable]
	public class FieldRow : CollectionBase
	{
		private int _FieldRowID;
		public int FieldRowID
		{
			get{return this._FieldRowID;}
			set{this._FieldRowID = value;}
		}

		public Field this[int i_Index]
		{
			get { return this.InnerList[i_Index] as Field; } 
			set { this.InnerList[i_Index] = value; } 
		} 
		//ctor
		public FieldRow() 
		{
			this._FieldRowID = -1;
		}
		//Methods
		public int Add(Field i_Object)
		{ 
			int index = this.InnerList.Add(i_Object);

			i_Object.FieldID = index;

			i_Object.SetXPosition(index);

			return index;
		}
		public void Remove(Field i_Obj)
		{
			this.InnerList.Remove(i_Obj);
		}
		public FieldRow Copy()
		{
			FieldRow row = new FieldRow();

			foreach(Field field in this)
			{
				row.Add(field.Copy());
			}

			return row;
		}
	}

	[Serializable]
	public class FieldTable : CollectionBase
	{
		public FieldRow this[int i_Index]
		{
			get { return this.InnerList[i_Index] as FieldRow; }
			set { this.InnerList[i_Index] = value; }
		} 
		//ctor
		public FieldTable()
		{
			
		}

		public FieldTable(Size szTable, Int32Collection arrColsInRows) 
		{
			this.SetTable(szTable,arrColsInRows);
		}

		public void SetTable(Size szTable, Int32Collection arrColsInRows)
		{
			this.AdjustRows(arrColsInRows);

			int nRows = 0,nCols = 0;

			System.Diagnostics.Debug.Assert(this.Count == arrColsInRows.Count);

			nRows = this.Count;

			for(int row = 0; row < nRows; row++)
			{
				FieldRow fieldRow = this[row];

				this.AdjustCols(fieldRow,arrColsInRows);

				nCols = fieldRow.Count;

				for(int col = 0; col < nCols; col++)
				{
					Field field = fieldRow[col];					
				}
			}
		}
		//Methods
		public int Add(FieldRow i_Object)
		{ 
			int index = this.InnerList.Add(i_Object);

			i_Object.FieldRowID = index;

			for(int col = 0; col < i_Object.Count; col++)
			{
				Field field = i_Object[col];

				field.SetXPosition(col);

				field.SetYPosition(i_Object.FieldRowID);
			}

			return index;
		} 
		public void Remove(FieldRow i_Obj)
		{
			this.InnerList.Remove(i_Obj);
		}
		public FieldTable Copy()
		{
			FieldTable table = new FieldTable();

			foreach(FieldRow row in this)
			{
				table.Add(row.Copy());
			}

			return table;
		}

		//fuctions
		public Field GetField(int row,int col)
		{
			if(row >= this.Count) return null;

			FieldRow fieldRow = this[row];

			if(col >= fieldRow.Count) return null;

			Field field = fieldRow[col] as Field;

			return field;
		}
        public void GetALLUsedFields(ref ArrayList usedFields)
        {
            foreach (FieldRow fieldRow in this.InnerList)
            {
                foreach (Field field in fieldRow)
                {
                    field.Filter.Filter.GetAllUsedFields(ref usedFields);
                }
            }

        }
		public void AdjustRows(Int32Collection arrColsInRows)
		{
			int lack = arrColsInRows.Count - this.Count;

			if(lack > 0)
			{
				for(int i = 0; i < lack; i++)
				{
					this.Add(new FieldRow());
				}
			}
			if(lack < 0)
			{
				lack *= -1;

				for(int i = this.Count - 1; i > this.Count - 1 - lack; i--)
				{
					FieldRow fieldRow = this[i];

					this.Remove(fieldRow);
				}
			}

			System.Diagnostics.Debug.Assert(arrColsInRows.Count == this.Count);
		}

		public void AdjustCols(FieldRow row, Int32Collection arrColsInRows)
		{
			System.Diagnostics.Debug.Assert(row.FieldRowID < arrColsInRows.Count);

			int cols = arrColsInRows[row.FieldRowID];

			int lack = cols - row.Count;

			if(lack > 0)
			{
				for(int i = 0; i < lack; i++)
				{
					row.Add(new Field());
				}
			}
			if(lack < 0)
			{
				lack *= -1;

				for(int i = row.Count - 1; i > row.Count - lack - 1; i--)
				{
					Field field = row[i];

					this.Remove(row);
				}
			}

			System.Diagnostics.Debug.Assert(row.FieldRowID == arrColsInRows.Count);
		}
	}

	[Serializable]
	public class FieldLayOut:ISerializable
	{
		//ctor
		public FieldLayOut()
		{
			this._Size = new Size(WIDTH,HEIGHT);
			this._ColumnsEachRow = new Int32Collection();
			this._FieldTable = new FieldTable();
			this._TotalSummaries = new GroupSummaryCollection();
			this._AutoColumnStyle=true;
			_TotalTitle=string.Empty;
			_FieldArgument=string.Empty;
			_SectionWrappers=new SectionFilterCollectionWrapper();
		}

		//Fields
		protected const int WIDTH = 700;
		protected const int HEIGHT = 600;
		private Int32Collection _ColumnsEachRow;
		private FieldTable _FieldTable;
		private Size _Size;
		private GroupSummaryCollection _TotalSummaries;	//Modified at 2008-11-17 11:08:46@Scott
		private bool _TotalOnHeader;	//Modified at 2008-11-17 11:47:01@Scott
		private string _TotalTitle=string.Empty;
		private ComputedStyle _FieldStyle=ComputedStyle.Group; //Added this code at 2008-11-20 15:37:33@Simon
        private bool _AutoColumnStyle=true;
		private string _FieldArgument=string.Empty;
		private SectionFilterCollectionWrapper _SectionWrappers=new SectionFilterCollectionWrapper();
		private bool _AutoLayOut=false;
        private int _RowHeight =BasicStyle.ConstValue.CellHeight;
        private int _HeaderRowHeight = BasicStyle.ConstValue.CellHeight;

		#region Properties

		//Properties
		[Browsable(true),Category("Data Style")]
		public ComputedStyle FieldStyle     //Added this code at 2008-11-20 15:38:29@Simon
		{
			get
			{
				return this._FieldStyle;
			}
			set
			{
				this._FieldStyle = value;
			}
		}
		[Browsable(true),Category("Data Style")]
		public bool GridAutoStyle     //Added this code at 2008-11-20 15:38:29@Simon
		{
			get
			{                
				return this._AutoColumnStyle;
			}
			set
			{
				if(this._FieldStyle==ComputedStyle.Group)
				{
					this._AutoColumnStyle =true;
				}
				else
				{
					this._AutoColumnStyle = value;
				}
			}
		}

		[Browsable(true),Category("Total")]
		public bool TotalOnHeader
		{
			get
			{
				return this._TotalOnHeader;
			}
			set
			{
				this._TotalOnHeader = value;
			}
		}
		[Browsable(true),Category("Total")]
		public string TotalTitle
		{
			get
			{
				return this._TotalTitle;
			}
			set
			{
				this._TotalTitle = value;
			}
		}

		[Browsable(true),Category("Total")]
		public GroupSummaryCollection TotalSummaries
		{
			get
			{
				if(this._TotalSummaries == null) this._TotalSummaries = new GroupSummaryCollection();

				return this._TotalSummaries;
			}
			set{this._TotalSummaries = value;}
		}


		[Browsable(false)]
		public int Count
		{
			get
			{
				return this.CalculateCount();
			}
		}
		
		[Browsable(false)]
		public FieldTable FieldTable
		{
			get
			{
				if(this._FieldTable == null) 
				{
					this._FieldTable = new FieldTable();

					this.UpdateFields();
				}
				return this._FieldTable;
			}
		}
		[Browsable(true),Category("Data")]
		[TypeConverter(typeof(Webb.Data.PublicDBFieldConverter))]
		public string FieldArgument
		{
			get
			{
				return this._FieldArgument;
			}
			set{
				this._FieldArgument = value;				
			
			   }
		}
		[Browsable(true),Category("Data")]
		[EditorAttribute(typeof(Webb.Reports.Editors.SectionFiltersEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public SectionFilterCollectionWrapper SectionFiltersWrapper
		{
			get
			{
				if(this._SectionWrappers==null)this._SectionWrappers=new SectionFilterCollectionWrapper();

				return this._SectionWrappers;

			}
			set{
					
				    _SectionWrappers.ReportScType=AdvFilterConvertor.GetScType(_SectionWrappers.ReportScType,DataProvider.VideoPlayBackManager.AdvSectionType);  //2009-6-16 10:18:47@Simon Add this Code

				    this._SectionWrappers.UpdateSectionFilters();

					this._SectionWrappers = value;
			   }
		}
		[Browsable(true),Category("Data")]
		public bool LayOutSectionFilters
		{
			get
			{
				return this._AutoLayOut;
			}
			set
			{
				this._AutoLayOut = value;
			}
		}

		[Browsable(true),Category("Layout")]
		public Size Size
		{
			get
			{
				if(this._Size == Size.Empty) this._Size = new Size(WIDTH,HEIGHT);

				return this._Size;
			}
			set{this._Size = value;}
		}

        [Browsable(true), Category("Row Height")]
        public int HeaderRowHeight
        {
            get
            {
                return this._HeaderRowHeight;
            }
            set { this._HeaderRowHeight = value; }
        }

        [Browsable(true), Category("Row Height")]
        public int RowHeight
        {
            get
            {
                return this._RowHeight;
            }
            set { this._RowHeight = value; }
        }


		[Browsable(true),Category("Layout")]
		public Int32Collection ColumnsEachRow  
		{
			get
			{
				if(this._ColumnsEachRow == null) this._ColumnsEachRow = new Int32Collection();

				return this._ColumnsEachRow;
			}
			set
			{				
				this._ColumnsEachRow = value;
			}
		}
		#endregion

		#region Functions
		//fuctions
		public int CalculateCount()
		{
			int count = 0;
			foreach(int colsCount in this._ColumnsEachRow)
			{
				count += colsCount;
			}
			return count;
		}

		/// <summary>
		/// invoke after other properties update
		/// </summary>
		public void SetTable()
		{
			this._FieldTable.SetTable(this.Size,this.ColumnsEachRow);
		}

		private bool FieldsNeedUpdate()
		{
			if(this.ColumnsEachRow.Count != this.FieldTable.Count) return true;
 
			for(int row = 0; row < this.ColumnsEachRow.Count; row++)
			{
				if(this.ColumnsEachRow[row] != this.FieldTable[row].Count) return true;
			}

			return false;
		}
		public void ChangeSize(int count)
		{
			int total=0;

			Int32Collection newcols=new Int32Collection();
			
			for(int i = 0; i < this.ColumnsEachRow.Count; i++)
			{
                total+=this.ColumnsEachRow[i];
				if(total>count)
				{
                    newcols.Add(count-total+this.ColumnsEachRow[i]);
					break;
				}
				else if(total==count)
				{
					 newcols.Add(this.ColumnsEachRow[i]);
					  break;				

				}
				else
				{
					newcols.Add(this.ColumnsEachRow[i]);
				}
			}
			this.UpdateFields();

		}

		public void UpdateFields()
		{				
			if(!this.FieldsNeedUpdate()) return;

			this.FieldTable.Clear();

			for(int row = 0; row < this.ColumnsEachRow.Count; row++)
			{
				int nCols = this.ColumnsEachRow[row];

				FieldRow fieldRow = new FieldRow();

				for(int col = 0; col < nCols; col++)
				{
					Field field = new Field();

					field.SetHeightRatio(1/(float)(this.ColumnsEachRow.Count));

					field.SetWidthRatio(1/(float)nCols);

					fieldRow.Add(field);					
				}

				this.FieldTable.Add(fieldRow);
			}
		}

		public Field GetField(int row,int col)
		{
			return this.FieldTable.GetField(row,col);
		}
		public Field GetField(int index)  //Added this code at 2009-2-11 8:52:35@Simon
		{
			if(index>=this.Count)return null;
             int row=0,col=0,total=0;
			for(int i=0;i<this.ColumnsEachRow.Count;i++)
			{				
				if(total+this.ColumnsEachRow[i]>index)
				{
					row=i;
                    break;
				}
				total+=this.ColumnsEachRow[i];
			}
            col=index-total;

			

			return this.FieldTable.GetField(row,col);
		}

        public void GetALLUsedFields(ref ArrayList usedFields)
        {            
             if (!usedFields.Contains(this.FieldArgument)) usedFields.Add(this.FieldArgument);

               this.SectionFiltersWrapper.GetAllUsedFields(ref usedFields);

               this.FieldTable.GetALLUsedFields(ref usedFields);                  
              
        }
	

		public SizeF GetFieldSize(int row,int col)   //Modify this Function at 2008-12-2 13:43:03@Simon
		{
			Field field = this.GetField(row,col);		

			if(field == null) return SizeF.Empty;

			return   new SizeF(field.Ratio.Width * this.Size.Width, field.Ratio.Height * this.Size.Height);				
		}

		public void Apply(FieldLayOut layout)
		{
			this._Size = new Size(layout.Size.Width,layout.Size.Height);			

			layout._ColumnsEachRow.CopyTo(this.ColumnsEachRow);

			this._FieldTable = layout.FieldTable.Copy();

			this.TotalSummaries = layout.TotalSummaries.CopyStructure();

			this._TotalOnHeader = layout.TotalOnHeader;

			this.FieldStyle=layout._FieldStyle;//Added this code at 2008-11-20 15:42:25@Simon

			this._AutoColumnStyle=layout._AutoColumnStyle;  //Added this code at 2008-11-24 12:53:54@Simon

			this._FieldArgument=layout._FieldArgument;

			this._SectionWrappers=layout._SectionWrappers;

			this._TotalTitle=layout.TotalTitle;

			this._AutoLayOut=layout._AutoLayOut;

            this._RowHeight = layout._RowHeight;

            this._HeaderRowHeight = layout._HeaderRowHeight;
		}
		#endregion

		#region Serialization By Simon's Macro 2009-2-10 10:33:38
		public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			info.AddValue("_ColumnsEachRow",_ColumnsEachRow,typeof(Webb.Collections.Int32Collection));
			info.AddValue("_FieldTable",_FieldTable,typeof(Webb.Reports.ExControls.Views.FieldTable));
			info.AddValue("_Size",_Size,typeof(System.Drawing.Size));
			info.AddValue("_TotalSummaries",_TotalSummaries,typeof(Webb.Reports.ExControls.Data.GroupSummaryCollection));
			info.AddValue("_TotalOnHeader",_TotalOnHeader);
			info.AddValue("_TotalTitle",_TotalTitle);
			info.AddValue("_FieldStyle",_FieldStyle,typeof(Webb.Reports.ExControls.Views.ComputedStyle));
			info.AddValue("_AutoColumnStyle",_AutoColumnStyle);
			info.AddValue("_FieldArgument",_FieldArgument);
			info.AddValue("_SectionWrappers",_SectionWrappers,typeof(Webb.Reports.SectionFilterCollectionWrapper));
			info.AddValue("_AutoLayOut",_AutoLayOut);
            info.AddValue("_RowHeight", this._RowHeight);
            info.AddValue("_HeaderRowHeight", this._HeaderRowHeight);
		
		}

		public FieldLayOut(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			try
			{
				_ColumnsEachRow=(Webb.Collections.Int32Collection)info.GetValue("_ColumnsEachRow",typeof(Webb.Collections.Int32Collection));
			}
			catch
			{
			}
			try
			{
				_FieldTable=(Webb.Reports.ExControls.Views.FieldTable)info.GetValue("_FieldTable",typeof(Webb.Reports.ExControls.Views.FieldTable));
			}
			catch
			{
			}
            try
            {
                _RowHeight = info.GetInt32("_RowHeight");
            }
            catch
            {
               this._RowHeight = BasicStyle.ConstValue.CellHeight;
            }
            try
            {
                this._HeaderRowHeight = info.GetInt32("_HeaderRowHeight");
            }
            catch
            {
                this._HeaderRowHeight = BasicStyle.ConstValue.CellHeight;
            }
			try
			{
				_Size=(System.Drawing.Size)info.GetValue("_Size",typeof(System.Drawing.Size));
			}
			catch
			{
			}
			try
			{
				_TotalSummaries=(Webb.Reports.ExControls.Data.GroupSummaryCollection)info.GetValue("_TotalSummaries",typeof(Webb.Reports.ExControls.Data.GroupSummaryCollection));
			}
			catch
			{
			}
			try
			{
				_TotalOnHeader=info.GetBoolean("_TotalOnHeader");
			}
			catch
			{
				_TotalOnHeader=false;
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
				_FieldStyle=(Webb.Reports.ExControls.Views.ComputedStyle)info.GetValue("_FieldStyle",typeof(Webb.Reports.ExControls.Views.ComputedStyle));
			}
			catch
			{
			}
			try
			{
				_AutoColumnStyle=info.GetBoolean("_AutoColumnStyle");
			}
			catch
			{
				_AutoColumnStyle=true;
			}
			try
			{
				_FieldArgument=info.GetString("_FieldArgument");
			}
			catch
			{
				_FieldArgument=string.Empty;
			}
			try
			{
				_SectionWrappers=(Webb.Reports.SectionFilterCollectionWrapper)info.GetValue("_SectionWrappers",typeof(Webb.Reports.SectionFilterCollectionWrapper));
			}
			catch
			{
			}
			try
			{
				_AutoLayOut=info.GetBoolean("_AutoLayOut");
			}
			catch
			{
				_AutoLayOut=false;
			}
		}
		#endregion
				
		
	}

    #endregion
	/// <summary>
	/// Summary description for FieldPanelView.
	/// </summary>
	[Serializable]
	public class FieldPanelView : ExControlView
	{
		#region ISerializable Members
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData (info, context);
			info.AddValue("SectionFilters",this._SectionFilters,typeof(SectionFilterCollection));
			info.AddValue("RootGroupInfo",this._RootGroupInfo,typeof(SectionGroupInfo));
			info.AddValue("LayOut",this._LayOut,typeof(FieldLayOut));
			info.AddValue("Styles",this._Styles,typeof(ExControlStyles));
			info.AddValue("Filters",this._Filter,typeof(DBFilter));
			info.AddValue("HeaderRows",this._HeaderRows,typeof(Int32Collection));
			info.AddValue("ColumnsWidth",this._ColumnsWidth,typeof(Int32Collection));
			info.AddValue("RowsHight",this._RowsHight,typeof(Int32Collection));
			info.AddValue("GridLine", this._GridLine);
			info.AddValue("_GridInfo",this._GridInfo,typeof(GridInfo));  //Added this code at 2008-11-20 15:17:25@Simon
			info.AddValue("_CellSizeAutoAdapting",this._CellSizeAutoAdapting,typeof(CellSizeAutoAdaptingTypes));  //Added this code at 2008-11-20 15:17:25@Simon

			info.AddValue("_RemoveHeaderRows", this._RemoveHeaderRows);
			
		}

		public FieldPanelView(SerializationInfo info, StreamingContext context):base(info,context)
		{
			try
			{
				this._RemoveHeaderRows = info.GetBoolean("_RemoveHeaderRows");
			}
			catch
			{
				this._RemoveHeaderRows = false;
			}
			try
			{
				this._SectionFilters = info.GetValue("SectionFilters",typeof(SectionFilterCollection)) as SectionFilterCollection;
				this._SectionFilters=AdvFilterConvertor.GetCustomFilters(DataProvider.VideoPlayBackManager.AdvReportFilters,this._SectionFilters);
			}
			catch
			{
				this._SectionFilters = new SectionFilterCollection();
			}

			try
			{
				this._RootGroupInfo = info.GetValue("RootGroupInfo",typeof(SectionGroupInfo)) as SectionGroupInfo;
				this._RootGroupInfo.ClickEvent = ClickEvents.PlayVideo;
			}
			catch
			{
				this._RootGroupInfo = new SectionGroupInfo();
				this._RootGroupInfo.ClickEvent = ClickEvents.PlayVideo;
			}

			try
			{
				this._LayOut = info.GetValue("LayOut",typeof(FieldLayOut)) as FieldLayOut;
			}
			catch
			{
				this._LayOut = new FieldLayOut();
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
				this._HeaderRows = info.GetValue("HeaderRows",typeof(Int32Collection)) as Int32Collection;
			}
			catch
			{
				this._HeaderRows = new Int32Collection();
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
				this._GridLine = info.GetBoolean("GridLine");
			}
			catch
			{
				this._GridLine = true;
			}
			try
			{
				this._GridInfo=info.GetValue("_GridInfo",typeof(GridInfo)) as GridInfo;
			}
			catch
			{
				this._GridInfo=new GridInfo();
			}
			try
			{
				this._CellSizeAutoAdapting=(CellSizeAutoAdaptingTypes)info.GetValue("_CellSizeAutoAdapting",typeof(CellSizeAutoAdaptingTypes));
			}
			catch
			{
				this._CellSizeAutoAdapting=CellSizeAutoAdaptingTypes.NotUse;
			}
		}
		#endregion

		#region Field/Properties
		//ctor
		public FieldPanelView(FieldPanel i_Control):base(i_Control as ExControl)
		{
			this._HeaderRows = new Int32Collection();
			this._SectionFilters = new SectionFilterCollection();
			this._RootGroupInfo = new SectionGroupInfo();
			this._RootGroupInfo.ClickEvent = ClickEvents.PlayVideo;
			this._LayOut = new FieldLayOut();
			this._LayOut.ColumnsEachRow.Add(3);
			this._LayOut.ColumnsEachRow.Add(3);
			this._LayOut.ColumnsEachRow.Add(3);
			this._Styles = new ExControlStyles();
			this._Styles.LoadDefaultStyle();    //08-14-2008@Scott
			this._Filter = new DBFilter();
			this._ColumnsWidth = new Int32Collection();
			this._RowsHight = new Int32Collection();
			this._GridLine = true;
			this._GridInfo=new GridInfo();  //Added this code at 2008-11-20 15:15:40@Simon

			_RemoveHeaderRows=false;

			this.CellSizeAutoAdapting =CellSizeAutoAdaptingTypes.NotUse;
	
		}
		protected CellSizeAutoAdaptingTypes _CellSizeAutoAdapting;
		public CellSizeAutoAdaptingTypes CellSizeAutoAdapting
		{
		    get{return this._CellSizeAutoAdapting;}
			set{this._CellSizeAutoAdapting=value;}
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

		protected Int32Collection _HeaderRows;
		public Int32Collection HeaderRows
		{
			get{return this._HeaderRows;}
			set{this._HeaderRows = value;}
		}

		protected new  SectionFilterCollection _SectionFilters;
		new public SectionFilterCollection SectionFilters
		{
			get{return this._SectionFilters;}
		}

		protected GroupInfo _RootGroupInfo;
		public GroupInfo RootGroupInfo
		{
			get{return this._RootGroupInfo;}
			set{this._RootGroupInfo = value;}
		}

		protected bool _RemoveHeaderRows=false;
		public bool RemoveHeaderRows
		{
			get
			{
				return _RemoveHeaderRows;
			}
			set
			{
				_RemoveHeaderRows=value;
			}
		}

		protected FieldLayOut _LayOut;
		public FieldLayOut LayOut
		{
			get{return this._LayOut;}
		}

		protected Styles.ExControlStyles _Styles;
		public Styles.ExControlStyles Styles
		{
			get{return this._Styles;}
		}
		
		protected DBFilter _Filter;
		public DBFilter Filter
		{
			get{return this._Filter;}
			set{this._Filter = value.Copy();}
		}

		protected bool _GridLine;	//08-25-2008@Scott
		public bool GridLine
		{
			get{return this._GridLine;}
			set{this._GridLine = value;}
		}

		protected GridInfo _GridInfo;      //Added this code at 2008-11-20 15:14:57@Simon
		public GridInfo FieldGridInfo
		{
			get{return this._GridInfo;}
			set{this._GridInfo=value;}
		}

		#endregion

		private void UpdateSections()
		{
			SectionFilterCollection sections=(_RootGroupInfo as SectionGroupInfo).SectionFilters.Copy();

			int DiffCount=sections.Count-this.LayOut.Count;

			if(DiffCount==0)
			{
				return;
			}
			else if(DiffCount>0)
			{
				if(this.LayOut.LayOutSectionFilters)
				{
					for(int i=0;i<DiffCount/3;i++)
					{
						this.LayOut.ColumnsEachRow.Add(3);
					}
					if(DiffCount%3!=0)this.LayOut.ColumnsEachRow.Add(DiffCount%3);
					this.LayOut.UpdateFields();
				}
				else
				{
					int index=sections.Count-1;

					for(int i=0;i<DiffCount;i++)
					{
						sections.RemoveAt(index);

						index--;
					}
				}
			}
			else
			{
				if(this.LayOut.LayOutSectionFilters)
				{
					int total=0;

					Int32Collection rows=new Int32Collection();
                    
					foreach(int col in this.LayOut.ColumnsEachRow)
					{
						if(total+col>=sections.Count)
						{
							rows.Add(sections.Count-total);

							break;
						}
						else
						{
							rows.Add(col);
						}

						total+=col;

					}	
			
					this.LayOut.ColumnsEachRow=rows;

					this.LayOut.UpdateFields();

				}
				else
				{				
					for(int i=sections.Count;i<this.LayOut.Count;i++)
					{
						Field field=this.LayOut.GetField(i); 
                    
						sections.Add(field.Filter);
					}
				}
				
				
			}	
			(_RootGroupInfo as SectionGroupInfo).SetSectionFilters(sections);
			
		}
		/// <summary>
		/// Calculate result by data struct and data source.
		/// </summary>
		/// <param name="i_Table">data source table</param>
		public override void CalculateResult(DataTable i_Table)
		{
			if(this.RootGroupInfo==null) return;

			GroupView.ConvertOldGroupInfo(this._RootGroupInfo);            

			if(i_Table == null) 
			{
				this.RootGroupInfo.ClearGroupResult(this._RootGroupInfo);   
				
				return;
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
			
			m_Rows = this.Filter.GetFilteredRows(i_Table,m_Rows);

			#region Modify Codes at 2009-2-10 9:32:48@Simon	
		
			this._RootGroupInfo.Summaries = this._LayOut.TotalSummaries.CopyStructure();	//Modified at 2008-11-17 11:08:23@Scott

			if(this.LayOut.FieldArgument!=string.Empty)
			{
				FieldGroupInfo fieldgroup=new FieldGroupInfo(this.LayOut.FieldArgument);				
                  
				this._RootGroupInfo=fieldgroup.IntoSections(i_Table,m_Rows,_RootGroupInfo);	
		   
				this.UpdateSections();			

			}
			else if(this.LayOut.SectionFiltersWrapper.SectionFilters.Count>0)
			{
				SectionGroupInfo sectiongroup=new SectionGroupInfo();

				sectiongroup.Apply(_RootGroupInfo);

				sectiongroup.SetSectionFilters(LayOut.SectionFiltersWrapper.SectionFilters);

				this._RootGroupInfo=sectiongroup;

				this.UpdateSections();			
				  
			}
			else if(this._RootGroupInfo is SectionGroupInfo)
			{
				(this._RootGroupInfo as SectionGroupInfo).SetSectionFilters(this._SectionFilters);
				
			}
			else
			{
				return;
			}
			#endregion
		

			this.RootGroupInfo.CalculateGroupResult(i_Table,m_Rows,m_Rows,this.RootGroupInfo);            
			
		}

		#region Get RowsCount	
		/// <summary>
		/// Get total rows of the printing table
		/// </summary>
		/// <returns>Return total rows of the printing table</returns>
		protected int GetTotalRows()
		{
			//int nFieldRowsCount = Math.Max(this.LayOut.ColumnsEachRow.Count,1);

			//int MinRows = (this.LayOut.Size.Height / nFieldRowsCount)/BasicStyle.ConstValue.CellHeight;		


			const int MinRows = 30;

			if(this.RootGroupInfo.GroupResults == null)
			{
				return this._LayOut.ColumnsEachRow.Count * MinRows;
			}

			int nGroupedRows = 0, nRet = 0;

			
			foreach(GroupResult gr in this.RootGroupInfo.GroupResults)
			{
				if(this.LayOut.FieldStyle==ComputedStyle.Group)
				{
					if(gr.SubGroupInfos.Count > 0)
					{
						GroupInfo gi = gr.SubGroupInfos[0];

						nGroupedRows = this.GetGroupedRows(gi);

						if(this._LayOut.TotalSummaries.Count > 0) nGroupedRows++;	//Modified at 2008-11-17 11:10:12@Scott
					
						if(nGroupedRows < MinRows) nGroupedRows = MinRows;

						nRet = nGroupedRows > nRet ? nGroupedRows : nRet;
						
					}
				}
				else
				{
					nGroupedRows = gr.RowIndicators.Count+1;
					
					if(this._LayOut.TotalSummaries.Count > 0) nGroupedRows++;  //Added this code at 2008-11-28 9:40:50@Simon
					
					if(nGroupedRows < MinRows) nGroupedRows = MinRows;

					nRet = nGroupedRows > nRet ? nGroupedRows : nRet;
				}              
			}	
		
			nRet *= this._LayOut.ColumnsEachRow.Count;

			nRet=Math.Min(nRet,500);             //Added this code at 2008-12-2 8:16:43@Simon

			if(nRet == 0)
			{
				return this._LayOut.ColumnsEachRow.Count * MinRows;
			}		

			return nRet;
		}

		/// <summary>
		/// Get count of rows in group info
		/// </summary>
		/// <param name="gi">group info</param>
		/// <returns>count of rows</returns>
		private int GetGroupedRows(GroupInfo gi)
		{
			int m_value = 0;

			GetSubRows(gi,ref m_value);

			return m_value + 1;	 //Table header
		}

		private void GetSubRows(GroupInfo i_GroupInfo,ref int i_value)
		{
			int m_TopGroups = i_GroupInfo.GetGroupedRows();
			if(i_value>0&&m_TopGroups>0)
			{
				i_value --;
				if(i_GroupInfo.ParentGroupResult!=null&&i_GroupInfo.ParentGroupResult.ParentGroupInfo!=null)
				{
					i_value++;
				}
			}
			i_value += m_TopGroups;
			if(i_GroupInfo.GroupResults!=null)
			{
				for(int m_index = 0; m_index<m_TopGroups; m_index++ )
				{
					GroupResult m_Result = i_GroupInfo.GroupResults[m_index];
					if(m_Result.SubGroupInfos.Count > 0)
					{
						GetSubRows(m_Result.SubGroupInfos[0],ref i_value);
					}
				}
			}
		}

		#endregion
		
		/// <summary>
		/// Get total columns of the printing table
		/// </summary>
		/// <returns>Return total columns of the printing table</returns>
		protected int GetTotalColumns()
		{
			int nRet = 1;

			foreach(int cols in this._LayOut.ColumnsEachRow)
			{
				if(cols == 0) continue;	

				nRet= Math.Max(nRet,cols);   //Added this code at 2008-12-2 11:30:22@Simon
			}

			//			int nRet = Webb.Utility.Lcm(this._LayOut.ColumnsEachRow);

			if(this.LayOut.FieldStyle==ComputedStyle.Grid)
			{
				int count=this._GridInfo.Columns.Count;
                if (this._GridInfo.SortingColumns.Count > 0 && this._GridInfo.SortingFrequence != SortingFrequence.None)
				{
					count++;
				}
				if(count<=0)count=1;

				nRet*=count;   //Modify this code at 2008-12-2 8:21:52@Simon
			}
		
			return nRet;
		}

		/// <summary>
		/// Create printing table
		/// </summary>
		/// <returns>Whether the printing table been created</returns>
		public override bool CreatePrintingTable()
		{
			if(this.RootGroupInfo==null||this.RootGroupInfo.GroupResults==null)  //Added this code at 2009-2-10 8:56:30@Simon
			{
				this.PrintingTable = null;

				return false;
			}
			if(RootGroupInfo.GroupResults.Count!=this._LayOut.Count)   //Added this code at 2009-2-10 8:56:27@Simon
			{
				this.PrintingTable = null;

				return false;
			}
			
			int nRows = this.GetTotalRows();

			int nColumns = this.GetTotalColumns();
			
			if(nRows == 0 || nColumns == 0) 
			{
				this.PrintingTable = null;

				return false;
			}

			this.PrintingTable = new WebbTable(nRows,nColumns);

			this._HeaderRows.Clear();

			this.SetTableValue(nRows,nColumns);

			this.ModifyTable();

			StyleBuilder.StyleRowsInfo m_StyleInfo = new Styles.StyleBuilder.StyleRowsInfo(this._HeaderRows);
			
			StyleBuilder styleBuilder = new StyleBuilder();

			styleBuilder.BuildStyle(this.PrintingTable,m_StyleInfo,null,this.Styles,this.HeaderRows);

			if(this.LayOut.FieldStyle==ComputedStyle.Grid)  //Added this code at 2008-11-24 12:21:16@Simon
			{
				this.AdjustInnerGridStyle();                  
			}

			if(this._RemoveHeaderRows)
			{
				this.PrintingTable.DeleteHeaderRowsAndStyle(this._HeaderRows);			
			}

			this.AddBottom();	//08-25-2008@Scott	 

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
					
			
			return true;
		}
	
		private void AddBottom()
		{
			if(this.GridLine)
			{
				// add bottom side for last row
				int row = this.PrintingTable.GetRows() - 1;

				for(int col = 0; col < this.PrintingTable.GetColumns(); col++)
				{
					IWebbTableCell cell = this.PrintingTable.GetCell(row,col);

					if(cell != null) cell.CellStyle.Sides |= DevExpress.XtraPrinting.BorderSide.Bottom;
				}
			}
		}

		private void AdjustInnerGridStyle()                //Added this code at 2008-11-24 11:14:57@Simon
		{
			IWebbTableCell cell=null;
			int nGridCols=this._GridInfo.Columns.Count;
            if (this._GridInfo.SortingColumns.Count > 0 && this._GridInfo.SortingFrequence != SortingFrequence.None)
			{
				nGridCols++;
			}
			if(nGridCols<=0)return; 
			
			int nTableColsInCol = 0,nCellsInCol=0;
			int nTableRows = this.PrintingTable.GetRows();
			int nTableCols = this.PrintingTable.GetColumns();	
	
			#region original codes
			//			for(int index=0;index<this.HeaderRows.Count;index++)
			//			{
			//				if(index>=this.LayOut.ColumnsEachRow.Count||this.LayOut.ColumnsEachRow[index]<=0)break;
			//                nTableColsInCol=nTableCols/this.LayOut.ColumnsEachRow[index];
			//                nCellsInCol=nTableColsInCol/nGridCols;
			//				int HeaderRow=this.HeaderRows[index];
			//				for(int row=HeaderRow+1;row<nTableRows;row++)
			//				{					
			//					if(this.HeaderRows.Contains(row))break;	
			//					for(int col=0;col<nTableCols;col+=nCellsInCol)
			//					{
			//						cell=this.PrintingTable.GetCell(row,col);
			//						if(cell==null)return;
			//						if(!this.LayOut.GridAutoStyle)
			//						{
			//							Color forecolor=cell.CellStyle.ForeColor;
			//
			//							int colIndex=(col%nTableColsInCol)/nCellsInCol;
			//
			//							GridColumn Gcol=null;
			//
			//							if(this._GridInfo.SortingFrequence!=SortingFrequence.None)
			//							{
			//								if(colIndex>0&&this._GridInfo.SortingFrequence==SortingFrequence.ShowBeforeColumns)
			//								{
			//									Gcol=this._GridInfo.Columns[colIndex-1];
			//									cell.CellStyle.SetStyle(Gcol.Style); 
			//								}
			//								else if(colIndex<this._GridInfo.Columns.Count&&this._GridInfo.SortingFrequence==SortingFrequence.ShowAfterColumns)
			//								{
			//									Gcol=this._GridInfo.Columns[colIndex];
			//									cell.CellStyle.SetStyle(Gcol.Style); 
			//								}
			//							}
			//							else
			//							{
			//								Gcol=this._GridInfo.Columns[colIndex];
			//								cell.CellStyle.SetStyle(Gcol.Style); 
			//							}
			//							cell.CellStyle.StringFormat|=StringFormatFlags.NoWrap;	//Added this code at 2008-12-2 9:26:39@Simon
			//	
			//							cell.CellStyle.ForeColor=forecolor;
			//						}
			//						cell.CellStyle.Sides=DevExpress.XtraPrinting.BorderSide.None;
			//						if(this.GridLine)
			//						{
			//							if(col%nTableColsInCol==0)
			//							{
			//								cell.CellStyle.Sides|=DevExpress.XtraPrinting.BorderSide.Left;
			//							
			//							}
			//							if(col+nCellsInCol==nTableCols)
			//							{
			//								cell.CellStyle.Sides|=DevExpress.XtraPrinting.BorderSide.Right;
			//							}
			//							if(row==HeaderRow+1&&(!this.LayOut.TotalOnHeader)&&this.LayOut.TotalSummaries.Count>0&&col+nTableColsInCol==nTableCols)
			//							{
			//                               cell.CellStyle.Sides|=DevExpress.XtraPrinting.BorderSide.Right;   //Added this code at 2008-11-28 11:14:07@Simon
			//							}
			//							
			//						}
			//					}
			//			
			//				}
			//			}
			#endregion
			#region New codes
			for(int index=0;index<this.HeaderRows.Count;index++)
			{
				if(index>=this.LayOut.ColumnsEachRow.Count||this.LayOut.ColumnsEachRow[index]<=0)break;

				nTableColsInCol=this.LayOut.ColumnsEachRow[index]*nGridCols;

				nCellsInCol=nGridCols;

				int HeaderRow=this.HeaderRows[index];

				for(int row=HeaderRow+1;row<nTableRows;row++)
				{					
					if(this.HeaderRows.Contains(row))break;	

					for(int col=0;col<nTableCols;col+=1)
					{
						if(col>=nTableColsInCol)break;

						cell=this.PrintingTable.GetCell(row,col);

						if(cell==null)return;

						#region Set GridStyle					
						if(!this.LayOut.GridAutoStyle)
						{
							Color forecolor=cell.CellStyle.ForeColor;

							int colIndex=col%nCellsInCol;

							GridColumn Gcol=null;

							if(this._GridInfo.SortingFrequence!=SortingFrequence.None)
							{
								if(colIndex>0&&this._GridInfo.SortingFrequence==SortingFrequence.ShowBeforeColumns)
								{
									Gcol=this._GridInfo.Columns[colIndex-1];
									cell.CellStyle.SetStyle(Gcol.Style); 
								}
								else if(colIndex<this._GridInfo.Columns.Count&&this._GridInfo.SortingFrequence==SortingFrequence.ShowAfterColumns)
								{
									Gcol=this._GridInfo.Columns[colIndex];
									cell.CellStyle.SetStyle(Gcol.Style); 
								}
							}
							else
							{
								Gcol=this._GridInfo.Columns[colIndex];
								cell.CellStyle.SetStyle(Gcol.Style); 
							}
							cell.CellStyle.StringFormat|=StringFormatFlags.NoWrap;	//Added this code at 2008-12-2 9:26:39@Simon
	
							cell.CellStyle.ForeColor=forecolor;
						}
						#endregion
						cell.CellStyle.Sides=DevExpress.XtraPrinting.BorderSide.None;
						if(this.GridLine)
						{
							if(col%nCellsInCol==0)
							{
								cell.CellStyle.Sides|=DevExpress.XtraPrinting.BorderSide.Left;
							
							}
							if((col+1)%nCellsInCol==0)
							{
								cell.CellStyle.Sides|=DevExpress.XtraPrinting.BorderSide.Right;
							}
							if(row==HeaderRow+1&&(!this.LayOut.TotalOnHeader)&&this.LayOut.TotalSummaries.Count>0&&col%nCellsInCol==0)
							{
								cell.CellStyle.Sides|=DevExpress.XtraPrinting.BorderSide.Right;   //Added this code at 2008-11-28 11:14:07@Simon
							}
							
						}
					}
			
				}
			}
			#endregion

		}
		#region SetValue
		/// <summary>
		/// Set value of the printing table
		/// </summary>
		private void SetTableValue(int nTotalRows,int nTotalColumns)
		{
			if(this.RootGroupInfo.GroupResults == null || this.RootGroupInfo.GroupResults.Count == 0) return;

			int index = 0,xOffset = 0,yOffset = 0,nRowsInLayout = 0;

			nRowsInLayout = this._LayOut.ColumnsEachRow.Count;

			if(nRowsInLayout == 0) return;

			yOffset = nTotalRows/nRowsInLayout;

			if(yOffset == 0) return;

			#region original codes
			//			for(int i = 0;i<nTotalRows;)
			//			{
			//				for(int j = 0;j<nTotalColumns;)
			//				{
			//					GroupResult gr = this.RootGroupInfo.GroupResults[index++];
			//
			//					xOffset = nTotalColumns/this._LayOut.ColumnsEachRow[i/yOffset];					
			//
			//					this.SetSectionValue(i,j,gr,xOffset);
			//
			//					if(index > this.RootGroupInfo.GroupResults.Count - 1) return;						
			//
			//					j += xOffset;
			//				}
			//				i += yOffset;
			//			}
			#endregion
              
			#region Modify codes at 2008-12-2 9:33:16@Simon
			int nGridCols=this._GridInfo.Columns.Count;
			if(this._GridInfo.SortingColumns.Count>0&&this._GridInfo.SortingFrequence!=SortingFrequence.None)
			{
				nGridCols++;
			}
			if(nGridCols<=0)nGridCols=1; 

			for(int i = 0;i<nTotalRows;)
			{
				int colsInRow=this._LayOut.ColumnsEachRow[i/yOffset];    //Added this code at 2008-12-2 8:29:42@Simon

				if(this.LayOut.FieldStyle==ComputedStyle.Grid)
				{
					colsInRow*=nGridCols;

					xOffset=nGridCols;	
				}
				else
				{
					xOffset=1;
				}

				for(int j = 0;j<nTotalColumns;)
				{
					if(j>=colsInRow)break;

					GroupResult gr = this.RootGroupInfo.GroupResults[index++];	

					this.SetSectionValue(i,j,gr,xOffset);

					if(index > this.RootGroupInfo.GroupResults.Count - 1) return;						

					j += xOffset;
				}
				i += yOffset;
			}
			#endregion        //End Modify

		}

		/// <summary>
		/// Set value in each section filter
		/// </summary>
		/// <param name="nStartRow">start row number in printing table</param>
		/// <param name="nStartCol">start column number in printing table</param>
		/// <param name="gr">group result of a section filter</param>
		private void SetSectionValue(int nStartRow,int nStartCol,GroupResult gr,int xOffset)   //modify this code at 2008-11-21 11:28:13@Simon
		{
			if(gr == null) return;

			int row = nStartRow;

			int col = nStartCol;

			this.SetCellValue(row,col,gr);	//header

			row++;
		
			GroupInfo groupInfo = null;

			if(gr.SubGroupInfos.Count > 0)
			{
				groupInfo = gr.SubGroupInfos[0];
			}

			#region Modified Area
			if(!this.LayOut.TotalOnHeader)
			{
				StringBuilder sbTotalSummaries = new StringBuilder();

				sbTotalSummaries.Append(this.LayOut.TotalTitle);  //Added this code at 2009-2-10 11:01:52@Simon

				if(gr.Summaries != null)
				{
					foreach(GroupSummary summary in gr.Summaries)
					{
						string strTotalSummary = WebbTableCellHelper.FormatValue(null,summary);

						sbTotalSummaries.Append(strTotalSummary + " ");
					}
					if(gr.Summaries.Count > 0)
					{
						sbTotalSummaries.Length --;                                                 

						this.SetCellValue(row,col,sbTotalSummaries.ToString(),FormatTypes.String);

						if(gr.ClickEvent != ClickEvents.None && gr.RowIndicators != null && gr.RowIndicators.Count > 0)
						{
							IWebbTableCell cell = this.PrintingTable.GetCell(row,col);

							DataSet m_DBSet = this.ExControl.DataSource as DataSet;

							Webb.Reports.DataProvider.VideoPlayBackArgs m_args = new Webb.Reports.DataProvider.VideoPlayBackArgs(m_DBSet,gr.RowIndicators);

							(cell as WebbTableCell).ClickEventArg = m_args;
						}

						row++;
					}
				}
			}
			#endregion        //Modify at 2008-11-17 11:12:58@Scott

			if(this.LayOut.FieldStyle==ComputedStyle.Group)     //Added this code at 2008-11-21 8:31:12@Simon
			{            
				#region Modify codes at 2008-11-25 13:35:58@Simon
				if(groupInfo==null||groupInfo.GroupResults==null)return;
				
				this.SetRowValue(ref row,col,groupInfo);	//rows
				
				#endregion        //End Modify
			}
			else
			{
				#region Modify codes at 2008-11-21 8:31:18@Simon					
				DataTable dt=this.ExControl.GetDataSource();				    
				this.SetRowsValue(dt,ref row,col,gr.RowIndicators,xOffset);
				#endregion        //End Modify
			}
		}

		/// <summary>
		/// Set value in each row
		/// </summary>
		/// <param name="nStartRow">start row number in printing table, ref type for recursion</param>
		/// <param name="nStartCol">start </param>
		/// <param name="gi">sub group info of a section filter's group result</param>
		private void SetRowValue(ref int nStartRow,int nStartCol,GroupInfo gi)
		{
			if(gi == null || gi.GroupResults == null) return;
            
			if(this.LayOut.ColumnsEachRow.Count<=0)return;

			int RowsInTable=this.PrintingTable.GetRows()/this.LayOut.ColumnsEachRow.Count;

			int MaxRows=(nStartRow/RowsInTable)*RowsInTable+RowsInTable;    //Added this code at 2008-12-2 8:29:42@Simon

			//if not section group , sort group result
			if(!(gi is SectionGroupInfo)) gi.GroupResults.Sort(gi.Sorting,gi.SortingBy,gi.UserDefinedOrders);

			foreach(GroupResult gr in gi.GroupResults)
			{
				if(nStartRow > this.PrintingTable.GetRows() - 1) return;	//tag

				if(nStartRow >=MaxRows) return;	//tag

				this.SetCellValue(nStartRow,nStartCol,gr);

				nStartRow++;

				if(nStartRow > this.PrintingTable.GetRows() - 1) return;	//tag

				if(nStartRow >=MaxRows) return;	 //Added this code at 2008-12-2 8:38:50@Simon

				if(gr.SubGroupInfos.Count > 0) this.SetRowValue(ref nStartRow,nStartCol,gr.SubGroupInfos[0]);
			}
		}
	
		private void SetDisplayRowValue(ref int nStartRow,int nStartCol,GroupInfo gi)
		{
			if(gi == null || gi.GroupResults == null) return;

			if(this.LayOut.ColumnsEachRow.Count<=0)return;

			int RowsInTable=this.PrintingTable.GetRows()/this.LayOut.ColumnsEachRow.Count;

			int MaxRows=(nStartRow/RowsInTable)*RowsInTable+RowsInTable;    //Added this code at 2008-12-2 8:29:42@Simon
           
			//if not section group , sort group result
			if(!(gi is SectionGroupInfo)) gi.GroupResults.Sort(gi.Sorting,gi.SortingBy,gi.UserDefinedOrders);		
        
			string strGroupValue = string.Empty;	
			foreach(GroupResult gr in gi.GroupResults)
			{
				if(nStartRow > this.PrintingTable.GetRows() - 1) return;	//tag

				if(nStartRow >=MaxRows) return;	//tag
				
				this.SetCellValue(nStartRow,nStartCol,gr);	
				
				nStartRow++;

				for(int i=0;i<gr.RowIndicators.Count-1;i++)
				{	
					if(nStartRow >=MaxRows) return;	   //Added this code at 2008-12-2 8:32:01@Simon

					if(nStartRow > this.PrintingTable.GetRows() - 1) return;	//tag

					if(gr.GroupValue == null || gr.GroupValue is System.DBNull || gr.GroupValue.ToString() == string.Empty)
					{
						strGroupValue = "[Null]";
					}
					else
					{
						strGroupValue = gr.GroupValue.ToString();
					}

					this.SetCellValue(nStartRow,nStartCol,strGroupValue,FormatTypes.String);
				
					nStartRow++;
				}
			}
		}
        
		#region Modify codes at 2008-11-21 8:34:00@Simon	
		
		//Added "SetRowsValue" function at 2008-11-24 8:54:26@Simon
		private void SetRowsValue(DataTable dt, ref int nRow, int nStartCol, Int32Collection filteredRows,int xOffset)
		{
			int tempRows=nRow;

			if(filteredRows.Count == 0||this.LayOut.ColumnsEachRow.Count<=0) return;

			int RowsInTable=this.PrintingTable.GetRows()/this.LayOut.ColumnsEachRow.Count;

			int MaxRows=(nRow/RowsInTable)*RowsInTable+RowsInTable;    //Added this code at 2008-12-2 8:29:42@Simon

			int i = 0,j = 0,nCol = 0;

			#region Modify codes at 2008-11-24 8:54:22@Simon			   
			int nGridCols=this._GridInfo.Columns.Count;
            if (this._GridInfo.SortingColumns.Count > 0 && this._GridInfo.SortingFrequence != SortingFrequence.None)
			{
				nGridCols++;
			}
			if(nGridCols<=0)return;

			xOffset/=nGridCols;
			#endregion        //End Modify

			#region Modify codes at 2008-11-26 9:29:16@Simon
			GroupResultCollection SortingResults=null;
            
			int SortRow=0;

			if(this._GridInfo.Columns.Count>=1)
			{				
				//				SortingResults=this._GridInfo.SortGridColumns(dt,filteredRows);
				SortingResults=this._GridInfo.Sorting(dt,filteredRows);
			}
			if(SortingResults==null||SortingResults.Count<=0)
			{
				#region Original Codes SetRowsValue
				for(i = 0; i<filteredRows.Count;i++, nRow++)      //modify this code at 2008-11-26 12:45:53@Simon
				{				
					if(!this.CheckTopCount(nRow-tempRows)) break;

					nCol = nStartCol;	

					SortRow=filteredRows[i];

					for(j = 0; j < this._GridInfo.Columns.Count; j++)
					{
						GridColumn col = this._GridInfo.Columns[j];		
					
						if(!PublicDBFieldConverter.AvialableFields.Contains(col.Field)||col.Field == string.Empty) 
						{
							nCol+=xOffset;
							continue;
						}				

						string strFieldValue= WebbTableCellHelper.FormatValue(dt.Rows[SortRow][col.Field],FormatTypes.String);			
				

						if(this._GridInfo.ClickEvent == ClickEvents.PlayVideo)
						{
							Int32Collection indicators = new Int32Collection();

							indicators.Add(SortRow);

							WebbTableCellHelper.SetCellValueWithClickEvent(this.PrintingTable, nRow, nCol, strFieldValue, FormatTypes.String, indicators);
						}
						else
						{
							WebbTableCellHelper.SetCellValue(this.PrintingTable, nRow, nCol,strFieldValue, FormatTypes.String);
						}
						nCol+=xOffset;				
					}					
				}				
				#endregion
				return;	
			}
			
			#endregion        //End Modify

			foreach(GroupResult gr in  SortingResults)
			{			
				for(i = 0; i<gr.RowIndicators.Count;i++,nRow++ )      //modify this code at 2008-11-26 12:45:53@Simon
				{	
					if(nRow>=MaxRows)break;   //Added this code at 2008-12-2 8:27:36@Simon
					if(!this.CheckTopCount(nRow-tempRows)) break;

					nCol = nStartCol;	

					SortRow=gr.RowIndicators[i];

					string strFreqValue= WebbTableCellHelper.FormatValue(gr.RowIndicators.Count,FormatTypes.String);

					if(this._GridInfo.SortingFrequence==SortingFrequence.ShowBeforeColumns)
					{
						if(i==0)
						{
							if(this._GridInfo.ClickEvent == ClickEvents.PlayVideo)
							{							
								WebbTableCellHelper.SetCellValueWithClickEvent(this.PrintingTable, nRow, nCol, strFreqValue, FormatTypes.String, gr.RowIndicators);
					      	
							}
							else
							{
								WebbTableCellHelper.SetCellValue(this.PrintingTable, nRow, nCol,strFreqValue, FormatTypes.String);
							}
						}
						nCol+=xOffset;
					}				
					

					#region set Col values

					for(j = 0; j < this._GridInfo.Columns.Count; j++)
					{
						GridColumn col = this._GridInfo.Columns[j];		
					
						if(!PublicDBFieldConverter.AvialableFields.Contains(col.Field)||col.Field == string.Empty) 
						{
							nCol+=xOffset;
							continue;
						}				

						string strFieldValue= WebbTableCellHelper.FormatValue(dt.Rows[SortRow][col.Field],FormatTypes.String);

						if(this._GridInfo.ClickEvent == ClickEvents.PlayVideo)
						{
							Int32Collection indicators = new Int32Collection();

							indicators.Add(SortRow);

							WebbTableCellHelper.SetCellValueWithClickEvent(this.PrintingTable, nRow, nCol, strFieldValue, FormatTypes.String, indicators);
					      	
						}
						else
						{
							WebbTableCellHelper.SetCellValue(this.PrintingTable, nRow, nCol,strFieldValue, FormatTypes.String);
						}
						nCol+=xOffset;					
					}
					#endregion
					if(i==0&&this._GridInfo.SortingFrequence==SortingFrequence.ShowAfterColumns)
					{
						if(this._GridInfo.ClickEvent == ClickEvents.PlayVideo)
						{							
							WebbTableCellHelper.SetCellValueWithClickEvent(this.PrintingTable, nRow, nCol, strFreqValue, FormatTypes.String, gr.RowIndicators);
					      	
						}
						else
						{
							WebbTableCellHelper.SetCellValue(this.PrintingTable, nRow, nCol,strFreqValue, FormatTypes.String);
						}						
					}
				}
			}


		}

		private bool CheckTopCount(int nRow)               //Added this code at 2008-11-21 8:33:41@Simon
		{
			if(this._GridInfo.TopCount > 0 && nRow >= this._GridInfo.TopCount) return false;          //Added this code at 2008-11-21 8:33:47@Simon

			return true;
		}
		#endregion        //End Modify 

		#region Modify codes at 2008-11-24 10:07:09@Simon
		private void SetGridCellWidth(IWebbTableCell cell,int nTableWidth,int nCol)
		{  
			if(this._GridInfo.Columns.Count<=0||nCol>this._GridInfo.Columns.Count||nCol<0)return;

			int nGridCols=this._GridInfo.Columns.Count;
			
			int nTotalWidth=0;
			int nRealTotal=0;
			int nCellDifWidth=35;
	
			int IndexWidth=0;

			for(int i=0;i<this._GridInfo.Columns.Count;i++)
			{
				GridColumn col=this._GridInfo.Columns[i];

				nTotalWidth+=col.ColumnWidth;
			}

			if(this._GridInfo.SortingFrequence!=SortingFrequence.None)    //Added this code at 2008-12-1 8:36:43@Simon
			{   
				nGridCols++;

				nTotalWidth+=nCellDifWidth;

				nRealTotal+=(nTableWidth*nCellDifWidth)/nTotalWidth;
			}
			
			for(int i=0;i<this._GridInfo.Columns.Count;i++)
			{
				GridColumn col=this._GridInfo.Columns[i];

				nRealTotal+=(nTableWidth*col.ColumnWidth)/nTotalWidth;
			}	
		
			int nRest=nTableWidth-nRealTotal;

			if(this._GridInfo.SortingFrequence==SortingFrequence.ShowAfterColumns)  //Added this code at 2008-12-1 8:36:43@Simon
			{ 	
				if(nCol==this._GridInfo.Columns.Count)
				{ 
					IndexWidth=nCellDifWidth;	
		
					cell.CellStyle.Width=(nTableWidth*IndexWidth)/nTotalWidth+nRest;	
				}
				else
				{
					IndexWidth=this._GridInfo.Columns[nCol].ColumnWidth;

					cell.CellStyle.Width=(nTableWidth*IndexWidth)/nTotalWidth;	
				}
				
			}
			else if(this._GridInfo.SortingFrequence==SortingFrequence.ShowBeforeColumns)  //Added this code at 2008-12-1 8:36:43@Simon
			{
             
				if(nCol==0)
				{
					IndexWidth=nCellDifWidth;	
				    
					cell.CellStyle.Width=(nTableWidth*IndexWidth)/nTotalWidth+nRest;        
				}
				else
				{
					IndexWidth=this._GridInfo.Columns[nCol-1].ColumnWidth;

					cell.CellStyle.Width=(nTableWidth*IndexWidth)/nTotalWidth;
				}
				
			}
			else    //this._GridInfo.SortingFrequence==SortingFrequence.None
			{
				IndexWidth=this._GridInfo.Columns[nCol].ColumnWidth;

				if(nTotalWidth>0)
				{
				
					cell.CellStyle.Width=(nTableWidth*IndexWidth)/nTotalWidth;
				}
				else
				{
					cell.CellStyle.Width=nTableWidth/nGridCols;
				}
				if(nCol==0)
				{
					cell.CellStyle.Width+=nRest;          //Added this code at 2008-12-1 8:36:43@Simon
				}

			}			
			   
			return;		           
		}
		#endregion        //End Modify
		#endregion
       
		#region Adjust Table's Style
		/// <summary>
		/// Ajust width of printing table's columns,and merge cells depend on layout
		/// </summary>
		private void ModifyTable()
		{
			//			this.AjustColWidth();

			int nTableColsInCol = 0, nRowsInTableRow = 0;

			int nTableRows = this.PrintingTable.GetRows();

			int nTableCols = this.PrintingTable.GetColumns();

			int nRows = this._LayOut.ColumnsEachRow.Count;

			if(nTableRows > 0 && nRows > 0)
			{
				nRowsInTableRow = nTableRows/nRows;
			}

			if(nRowsInTableRow == 0) return;
			#region original codes       //Hide these code at 2008-12-2 9:46:04@Simon
			//			for(int i = 0;i< nTableRows;i++)
			//			{
			//				int nCols = this._LayOut.ColumnsEachRow[i/nRowsInTableRow];
			//
			//				if(nCols == 0) break;
			//				
			//				nTableColsInCol = nTableCols/nCols;
			//
			//				if(this.LayOut.FieldStyle==ComputedStyle.Grid)  //Added this code at 2008-11-20 9:46:33@Simon
			//				{  
			//					int nGridCols=this._GridInfo.Columns.Count;	
			//
			//					if(this._GridInfo.SortingFrequence!=SortingFrequence.None)
			//					{
			//						nGridCols++;
			//					}
			//
			//					nGridCols=nGridCols>0?nGridCols:1;		
			//
			//					for(int col = 0;col< nTableCols;)
			//					{
			//						if(i%nRowsInTableRow==0)
			//						{
			//							if(col%nTableColsInCol==0)
			//							{
			//								this.PrintingTable.MergeCells(i,i,col,col+nTableColsInCol-1);	
			//							}
			//						}
			//						else  if(i%nRowsInTableRow==1&&!this.LayOut.TotalOnHeader&&this.LayOut.TotalSummaries.Count>0)         //Added this code at 2008-11-28 9:48:26@Simon
			//						{
			//							if(col%nTableColsInCol==0)
			//							{
			//								this.PrintingTable.MergeCells(i,i,col,col+nTableColsInCol-1);	
			//							}
			//							   
			//						}
			//						else
			//						{
			//							 this.PrintingTable.MergeCells(i,i,col,col+nTableColsInCol/nGridCols-1);		 //Added this code at 2008-11-20 9:46:33@Simon					
			//						}
			//
			//						col += nTableColsInCol/nGridCols;
			//					}
			//				}
			//				else
			//				{
			//					for(int j = 0;j< nTableCols;)
			//					{
			//						this.PrintingTable.MergeCells(i,i,j,j+nTableColsInCol-1);
			//
			//						j += nTableColsInCol;
			//					}
			//				}
			//			}
			#endregion  
			#region New codes
			int nGridCols=this._GridInfo.Columns.Count;	
	
			if(this._GridInfo.SortingFrequence!=SortingFrequence.None)
			{
				nGridCols++;
			}
	
			nGridCols=nGridCols>0?nGridCols:1;
		
			for(int i = 0;i< nTableRows;i++)
			{
				int nCols = this._LayOut.ColumnsEachRow[i/nRowsInTableRow];
	
				if(nCols == 0) break;
				     
				int MaxColsInRow=nCols;
	
				if(this.LayOut.FieldStyle==ComputedStyle.Grid)  //Added this code at 2008-11-20 9:46:33@Simon
				{ 
					nTableColsInCol =nGridCols;
                      
					for(int col = 0;col< nTableCols;col++)
					{
						if(i%nRowsInTableRow==0)
						{
							if(col%nTableColsInCol==0)
							{
								this.PrintingTable.MergeCells(i,i,col,col+nTableColsInCol-1);	
							}
						}
						else  if(i%nRowsInTableRow==1&&!this.LayOut.TotalOnHeader&&this.LayOut.TotalSummaries.Count>0)         //Added this code at 2008-11-28 9:48:26@Simon
						{
							if(col%nTableColsInCol==0)
							{
								this.PrintingTable.MergeCells(i,i,col,col+nTableColsInCol-1);	
							}									
						}	
						
					}
				}
				//					else
				//					{
				//						for(int j = 0;j< nTableCols;)
				//						{
				//							this.PrintingTable.MergeCells(i,i,j,j+nTableColsInCol-1);
				//	
				//							j += nTableColsInCol;
				//						}
				//					}
			}
			#endregion

            this.InitHeadersRows();  //Added this code at 2008-12-2 11:18:26@Simon 

            this.ModifyRowHeight();

			this.AdjustTableRows();

			this.BuildGridLineStyle();	//08-25-2008@Scott
		}


        private void ModifyRowHeight()
        {
            int nTableRows = this.PrintingTable.GetRows();

            int nTableColumns = this.PrintingTable.GetColumns();

            if (this.LayOut.HeaderRowHeight > 0)
            {
                foreach (int m_row in this.HeaderRows)
                {
                    for (int m_col = 0; m_col < nTableColumns; m_col++)
                    {
                        this.PrintingTable.GetCell(m_row, m_col).CellStyle.Height = this.LayOut.HeaderRowHeight;
                    }
                }
                
            }
            if (this.LayOut.RowHeight > 0)
            {
                for (int m_row = 0; m_row < nTableRows; m_row++)
                {
                    if(this.HeaderRows.Contains(m_row))continue;

                    for (int m_col = 0; m_col < nTableColumns; m_col++)
                    {
                        this.PrintingTable.GetCell(m_row, m_col).CellStyle.Height = this.LayOut.RowHeight;
                    }
                }
            }
        }

		private void BuildGridLineStyle()
		{
			this.Styles.HeaderStyle.StringFormat = StringFormatFlags.NoWrap;
			
			this.Styles.RowStyle.StringFormat|= StringFormatFlags.NoWrap;

			this.Styles.AlternateStyle.StringFormat|= StringFormatFlags.NoWrap;

			if(this.GridLine)
			{
				this.Styles.HeaderStyle.Sides = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Top;

				this.Styles.RowStyle.Sides = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right;

				this.Styles.AlternateStyle.Sides = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right;
							
			}
			else
			{
				this.Styles.HeaderStyle.Sides = DevExpress.XtraPrinting.BorderSide.None;

				this.Styles.RowStyle.Sides = DevExpress.XtraPrinting.BorderSide.None;

				this.Styles.AlternateStyle.Sides = DevExpress.XtraPrinting.BorderSide.None;
			}
		}
		private void InitHeadersRows()
		{
			this.HeaderRows.Clear();

			int nTableRows = this.PrintingTable.GetRows();			

			int nRows = this._LayOut.ColumnsEachRow.Count;	
		
            
			int nRowsInTableRow = 0;

			if(nTableRows > 0 && nRows > 0)
			{
				nRowsInTableRow = nTableRows/nRows;
			}		
			for(int i=0;i<nRows;i++)
			{
				this.HeaderRows.Add(nRowsInTableRow*i);
			}
		}

		private void AdjustTableRows()
		{
			Int32Collection arrDeleteRows = new Int32Collection();

			int nRowsInTableRow = 0;

			int nTableRows = this.PrintingTable.GetRows();

			int nTableCols = this.PrintingTable.GetColumns();

			int nRows = this._LayOut.ColumnsEachRow.Count;

			int nFieldHeight = 0;

			if(nTableRows > 0 && nRows > 0)
			{
				nRowsInTableRow = nTableRows/nRows;
			}

			if(nRowsInTableRow == 0) return;

			#region Original Codes
			//
			//			for(int i = 0;i< nTableRows;i++)
			//			{
			//				int nCols = this._LayOut.ColumnsEachRow[i/nRowsInTableRow];
			//
			//				if(i%nRowsInTableRow == 0)
			//				{
			//					nFieldHeight = 0;	//reach next field row, clear height;
			//				}
			//
			//				if(nCols == 0) break;
			//
			//				nTableColsInCol = nTableCols/nCols;	
			//			
			//				int nGridCols=this._GridInfo.Columns.Count;
			//
			//				if(this._GridInfo.SortingFrequence!=SortingFrequence.None)
			//				{
			//					nGridCols++;
			//				}
			//
			//				nGridCols=nGridCols>0?nGridCols:1;
			//
			//				int nCellsInTable=0;
			//
			//				if(this.LayOut.FieldStyle==ComputedStyle.Grid)
			//				{
			//					nCellsInTable=nTableColsInCol/nGridCols;
			//				}
			//				else
			//				{
			//					nCellsInTable=nTableColsInCol;
			//				}		
			//				
			//				
			//				for(int j = 0;j< nTableCols;j++)
			//				{
			//					WebbTableCell cell = this.PrintingTable.GetCell(i,j) as WebbTableCell;
			//
			//					if(j%nCellsInTable == 0)
			//					{
			//						int nTableWidth =(int)Math.Round((this.LayOut.GetFieldSize(i/nRowsInTableRow,j/nTableColsInCol).Width),0);   //Modify this code at 2008-12-1 8:55:01@Simon
			//
			//						if(this.LayOut.FieldStyle==ComputedStyle.Grid&&this._GridInfo.Columns.Count>0)
			//						{					
			//							int nCol=(j/nCellsInTable)%nGridCols;	
			//
			//							this.SetGridCellWidth(cell,nTableWidth,nCol);	
			//
			//						}
			//						else
			//						{
			//							cell.CellStyle.Width=nTableWidth;							 
			//
			//						}
			//					}
			//					else
			//					{
			//						cell.CellStyle.Width = 0;
			//					}
			//
			//				}
			#endregion

			#region New Codes  //Added this code at 2008-12-2 9:53:49@Simon

			for(int i = 0;i< nTableRows;i++)
			{
				int nCols = this._LayOut.ColumnsEachRow[i/nRowsInTableRow];

				if(i%nRowsInTableRow == 0)
				{
					nFieldHeight = 0;	//reach next field row, clear height;
				}

				if(nCols == 0) break;
			
			
				int nGridCols=this._GridInfo.Columns.Count;

				if(this._GridInfo.SortingFrequence!=SortingFrequence.None)
				{
					nGridCols++;
				}

				nGridCols=nGridCols>0?nGridCols:1;

				int nCellsInTable=0;

				if(this.LayOut.FieldStyle==ComputedStyle.Grid)
				{
					nCellsInTable=nGridCols;
				}
				else
				{
					nCellsInTable=1;
				}	
	
				int MaxCols=nCols*nCellsInTable;   //Added this code at 2008-12-2 9:58:25@Simon	
			
				int WidthToAdjust=this.LayOut.Size.Width;
				
				for(int j = 0;j< nTableCols;j++)
				{
					WebbTableCell cell = this.PrintingTable.GetCell(i,j) as WebbTableCell;

					if(j<MaxCols)
					{
						int nTableWidth =(int)(this.LayOut.GetFieldSize(i/nRowsInTableRow,j/nCellsInTable).Width);   //Modify this code at 2008-12-1 8:55:01@Simon
 
						if(this.LayOut.FieldStyle==ComputedStyle.Grid&&this._GridInfo.Columns.Count>0)
						{					
							int nCol=j%nGridCols;
							
							if(nCol==0)WidthToAdjust-= nTableWidth;   //Added this code at 2008-12-2 14:01:51@Simon

							this.SetGridCellWidth(cell,nTableWidth,nCol);	
						}
						else
						{
							WidthToAdjust-= nTableWidth;   //Added this code at 2008-12-2 14:01:51@Simon

							cell.CellStyle.Width=nTableWidth;	
						}
					}
					else
					{
						cell.CellStyle.Width = 0;						
					}

				}
				for(int k = MaxCols-1;k>=0;k--)
				{	
					if(WidthToAdjust>0)
					{
						WidthToAdjust--;
						this.PrintingTable.GetCell(i,k).CellStyle.Width+=1;  //Added this code at 2008-12-2 14:03:16@Simon
					}
					else if(WidthToAdjust<0)
					{
						WidthToAdjust++;
						this.PrintingTable.GetCell(i,k).CellStyle.Width-=1;  //Added this code at 2008-12-2 14:03:16@Simon
					}
					else
					{					
						break;
					}
				}
				if(WidthToAdjust!=0)
				{
					this.PrintingTable.GetCell(i,0).CellStyle.Width+=WidthToAdjust;
				}
				#endregion
            
				nFieldHeight += this.PrintingTable.GetCell(i,0).CellStyle.Height;

				if(nFieldHeight > this.LayOut.GetFieldSize(i/nRowsInTableRow,0).Height && i%nRowsInTableRow != 0)
				{
					arrDeleteRows.Add(i);	//calculate current field row's height
				}
			}
	
			this.PrintingTable.DeleteRows(arrDeleteRows);	
		
			this.AdjustHeaderRows(arrDeleteRows,nTableRows);
		}

		private void AdjustHeaderRows(Int32Collection arrDeleteRows,int nTableRows)  
		{
			#region Modify codes at 2008-12-3 11:09:10@Simon

			for(int i=1;i<this.HeaderRows.Count;i++)
			{
				int HeaderRow=this.HeaderRows[i];

				for(int row = 0; row < HeaderRow; row++)
				{
					if(arrDeleteRows.Contains(row))
					{
						this.HeaderRows[i]--;
					}
				}
			}
			#endregion        //End Modify

			//			Int32Collection arrExistRows = new Int32Collection();
			//
			//			for(int row = 0; row < nTableRows; row++)
			//			{
			//				if(!arrDeleteRows.Contains(row))
			//				{
			//					arrExistRows.Add(row);
			//				}
			//			}

			//			this._HeaderRows.Add(0);
			//
			//			for(int row = 0; row < arrExistRows.Count - 1; row++)
			//			{
			//				if(arrExistRows[row] + 1 != arrExistRows[row + 1])
			//				{
			//					if(!this._HeaderRows.Contains(row + 1))
			//					{
			//						this._HeaderRows.Add(row + 1);
			//					}
			//				}
			//			}
		}

	

		/// <summary>
		/// Ajust width of printing table's columns
		/// </summary>
		private void AjustColWidth()
		{
			for(int i = 0;i< this.PrintingTable.GetRows();i++)
			{
				for(int j = 0;j<this.PrintingTable.GetColumns();j++)
				{
					IWebbTableCell cell = this.PrintingTable.GetCell(i,j);

					cell.CellStyle.Width = this.LayOut.Size.Width/this.PrintingTable.GetColumns();
				}
			}
		}

		private void ApplyRowHeightStyle(int m_Rows)
		{
			if(this.RowsHight.Count<=0) return;
			if(this.RowsHight.Count!=m_Rows) return;
			for(int m_row = 0;m_row<m_Rows;m_row++)
			{
				this.PrintingTable.GetCell(m_row,0).CellStyle.Height = this.RowsHight[m_row];
			}
		}

		private void ApplyColumnWidthStyle(int m_Cols)
		{
			if(this.ColumnsWidth.Count<=0) return;
			if(this.ColumnsWidth.Count!=m_Cols) return;
			for(int m_Col = 0;m_Col<m_Cols;m_Col++)
			{
				this.PrintingTable.GetCell(0,m_Col).CellStyle.Width = this.ColumnsWidth[m_Col];
			}
		}

		#endregion
		/// <summary>
		/// Calculate result, and create printing table.
		/// </summary>
		public override void UpdateView()
		{
			base.UpdateView ();
		}

		/// <summary>
		/// Paint cells when preview
		/// </summary>
		/// <param name="areaName">e.g.,"Detail"</param>
		/// <param name="graph"></param>
		public override int CreateArea(string areaName, IBrickGraphics graph)
		{
			//base.CreateArea (areaName, graph);

			if(this.PrintingTable==null) return 0;
			
			return this.PrintingTable.CreateAreaWithoutAdjustSize(areaName,graph);
		}

		/// <summary>
		/// Paint cells when design
		/// </summary>
		/// <param name="e"></param>
		public override void Paint(PaintEventArgs e)
		{
			if(this.PrintingTable != null && this.RootGroupInfo.GroupResults != null)
			{
				this.PrintingTable.PaintTableWithoutAdjustSize(e);
			}
			else
			{
				base.Paint (e);
			}
		}
        public override void GetALLUsedFields(ref ArrayList usedFields)
        {
            if (this._RootGroupInfo != null)
            {
                this.RootGroupInfo.GetAllUsedFields(ref usedFields);
            }

            this.Filter.GetAllUsedFields(ref usedFields);

            this.LayOut.GetALLUsedFields(ref usedFields);
        }

		#region function to set table cell value.
		private void GetIndent(GroupResult gr,ref int indent)
		{
			GroupInfo gi = gr.ParentGroupInfo;
			
			if(gi == null) return;

			GroupResult grParent = gi.ParentGroupResult;
			
			if(grParent == null)
			{
				return;
			}
			else
			{
				indent++;

				this.GetIndent(grParent,ref indent);
			}
		}

		private void GetIndentConverse(GroupResult gr,ref int indent)
		{
			GroupInfo gi = null;
			
			if(gr.SubGroupInfos.Count > 0)
			{
				gi = gr.SubGroupInfos[0];
			}
			
			if(gi == null || gi.GroupResults == null || gi.GroupResults.Count == 0 || gr.ParentGroupInfo == null || gr.ParentGroupInfo == this.RootGroupInfo ) return;

			GroupResult grChild = gi.GroupResults[0];
			
			if(grChild == null)
			{
				return;
			}
			else
			{
				indent++;

				this.GetIndentConverse(grChild,ref indent);
			}
		}

		private string MakeIndentString(int indent)
		{
			StringBuilder sb = new StringBuilder();
			
			for(int i = 0;i<indent;i++)
			{
				sb.Append("*");
			}
			sb.Append(" ");

			return sb.ToString();
		}

		private void SetCellValue(int i_Row, int i_Col,object i_Value,FormatTypes i_Type)
		{
			if (i_Value != null)
			{
				this.PrintingTable.GetCell(i_Row, i_Col).Text = WebbTableCellHelper.FormatValue(i_Value, i_Type);
			}
		}

		private void SetCellValue(int i_Row, int i_Col,GroupSummary summary)
		{
			WebbTableCell cell = this.PrintingTable.GetCell(i_Row, i_Col) as WebbTableCell;

			cell.Text = WebbTableCellHelper.FormatValue(cell, summary);
		}

		private void SetCellValue(int i_Row, int i_Col, GroupResult gr)
		{
			StringBuilder sb = new StringBuilder();
			
			string strSummary = string.Empty;

			int indent = 0;

			this.GetIndentConverse(gr,ref indent);

			string strGroupValue = string.Empty;

			if(gr.GroupValue == null || gr.GroupValue is System.DBNull || gr.GroupValue.ToString() == string.Empty)
			{
                //strGroupValue = "[Null]";

				strGroupValue ="   ";
			}
			else
			{
				strGroupValue = gr.GroupValue.ToString();
			}

            if (gr.ParentGroupInfo!=null&&gr.ParentGroupInfo.FollowSummaries)
            {
                sb.AppendFormat("{0}", this.MakeIndentString(indent));

                if (this.LayOut.TotalOnHeader || gr.ParentGroupInfo.ParentGroupResult != null)	//Modified at 2008-11-20 12:01:59@Scott
                {
                    if (gr.Summaries != null)
                    {
                        foreach (GroupSummary summary in gr.Summaries)
                        {
                            strSummary = WebbTableCellHelper.FormatValue(null, summary);

                            sb.AppendFormat("{0} ", strSummary);
                        }
                    }
                }

                sb.AppendFormat("{0}", strGroupValue);
            }
            else
            {
                sb.AppendFormat("{0}{1}", this.MakeIndentString(indent), strGroupValue);

                if (this.LayOut.TotalOnHeader || gr.ParentGroupInfo.ParentGroupResult != null)	//Modified at 2008-11-20 12:01:59@Scott
                {
                    if (gr.Summaries != null)
                    {
                        foreach (GroupSummary summary in gr.Summaries)
                        {
                            strSummary = WebbTableCellHelper.FormatValue(null, summary);

                            sb.AppendFormat(" {0}", strSummary);
                        }
                    }
                }
            }

			this.SetCellValue(i_Row,i_Col,sb.ToString(),FormatTypes.String);

			if(gr.ClickEvent != ClickEvents.None && gr.RowIndicators != null && gr.RowIndicators.Count > 0)
			{
				IWebbTableCell cell = this.PrintingTable.GetCell(i_Row,i_Col);

				DataSet m_DBSet = this.ExControl.DataSource as DataSet;

				Webb.Reports.DataProvider.VideoPlayBackArgs m_args = new Webb.Reports.DataProvider.VideoPlayBackArgs(m_DBSet,gr.RowIndicators);

				(cell as WebbTableCell).ClickEventArg = m_args;
			}
		}
		#endregion
	}
}
