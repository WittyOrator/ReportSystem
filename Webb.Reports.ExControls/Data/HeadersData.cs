using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Windows.Forms;
using System.Drawing;
using  System.ComponentModel;

using Webb.Reports.ExControls.Styles;
using DevExpress.Utils;

namespace Webb.Reports.ExControls.Data
{

	public interface IMultiHeader
	{
		HeadersData HeadersData{get;set;}
		bool HaveHeader{get;set;}
		ArrayList GetPrnHeader(out ArrayList formats); 
	}
	//Added this classes at 2008-8-15 @Simon
	public struct LabelInfo
	{	
		public int RowIndex,MergedColMin,MergedColMax;
		public Point LblLoc;
		public Size LblSize;
		public Color DefaultBackColor;
		

	}
	#region public class HeaderCell
	[Serializable]
	public class HeaderCell:ISerializable	
	{   
		#region Field
		private int _Row=0,_Col=0;
		private string _Text="";

		private int _MergerdIndex=0;
		private int _MergerdCount=0;

		protected bool _ChangeStyle=false;
		protected IBasicStyle _CellStyle=new BasicStyle();
		protected bool _updateAllRowstyle=false;
		
		#endregion 		

		public HeaderCell(int row,int col)
		{
			this._Row=row;
			this._Col=col;
		}

		public HeaderCell Copy()
		{ 
			HeaderCell newHeaderCell=new HeaderCell(Row,Col);		       
			newHeaderCell._MergerdIndex=this._MergerdIndex;
			newHeaderCell._MergerdCount=this._MergerdCount;		
			newHeaderCell.Text=this.Text;
            newHeaderCell.NeedChangeStyle=this.NeedChangeStyle;
            newHeaderCell._updateAllRowstyle=this._updateAllRowstyle;
			if(CellStyle!=null)newHeaderCell.CellStyle=this.CellStyle.Copy();
			return newHeaderCell;
		}
		public void UpdateCellStyle(HeaderCellStyle headerstyle)
		{
			this.UpdateAllRowstyle=headerstyle.UpdateAllRowsStyle;
			this.NeedChangeStyle=headerstyle.NeedChangeStyle;   
			this.CellStyle=headerstyle.Style.Copy();
		}
		public void SetInit(HeaderCellStyle headerstyle)
		{
			headerstyle.UpdateAllRowsStyle=this.UpdateAllRowstyle;
			headerstyle.NeedChangeStyle=this.NeedChangeStyle;   
			headerstyle.Style=this.CellStyle as BasicStyle;
		}
		public void ToZero()
		{			
			_MergerdIndex=0;
			_MergerdCount=0;
			_Text="";
			_ChangeStyle=false;
			_CellStyle=null;
			_updateAllRowstyle=false;
		}

		public bool UpdateAllRowstyle
		{
			get{ return this._updateAllRowstyle; }
			set{ _updateAllRowstyle = value; }
		}
       
		public int Row
		{
			get{ return _Row; }
			set{ _Row = value; }
		}
      
		public int Col
		{
			get{ return _Col; }
			set{ _Col = value; }
		}
        
		public string Text
		{
			get{ return _Text; }
			set{ _Text = value; }
		}

     
		public int MergerdIndex
		{
			get{ return _MergerdIndex; }
			set{ _MergerdIndex = value; }
		}

       
		public int MergerdCount
		{
			get{ return _MergerdCount; }
			set{ _MergerdCount = value; }
		}
        
		public bool NeedChangeStyle
		{
			get{ return _ChangeStyle; }
			set{ _ChangeStyle = value; }
		}
       
		public Webb.Reports.ExControls.IBasicStyle CellStyle
		{
			get{ return _CellStyle; }
			set{ _CellStyle = value; }
		}

		#region Serialization By Simon's Macro 2009-8-18 9:17:51
		public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			info.AddValue("_Row",_Row);
			info.AddValue("_Col",_Col);
			info.AddValue("_Text",_Text);
			info.AddValue("_MergerdIndex",_MergerdIndex);
			info.AddValue("_MergerdCount",_MergerdCount);
			info.AddValue("_ChangeStyle",_ChangeStyle);
			info.AddValue("_CellStyle",_CellStyle,typeof(Webb.Reports.ExControls.IBasicStyle));
		
		}

		public HeaderCell(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			try
			{
				_Row=info.GetInt32("_Row");
			}
			catch
			{
				_Row=0;
			}
			try
			{
				_Col=info.GetInt32("_Col");
			}
			catch
			{
				_Col=0;
			}
			try
			{
				_Text=info.GetString("_Text");
			}
			catch
			{
				_Text=string.Empty;
			}
			try
			{
				_MergerdIndex=info.GetInt32("_MergerdIndex");
			}
			catch
			{
				_MergerdIndex=0;
			}
			try
			{
				_MergerdCount=info.GetInt32("_MergerdCount");
			}
			catch
			{
				_MergerdCount=0;
			}
			try
			{
				_ChangeStyle=info.GetBoolean("_ChangeStyle");
			}
			catch
			{
				_ChangeStyle=false;
			}
			try
			{
				_CellStyle=(Webb.Reports.ExControls.IBasicStyle)info.GetValue("_CellStyle",typeof(Webb.Reports.ExControls.IBasicStyle));
			}
			catch
			{
				_CellStyle=new BasicStyle();
			}
		}
		#endregion
	}
		
	#endregion


	#region public class HeaderRow
	[Serializable]
	public class HeaderRow:ISerializable	
	{
		int _RowIndex;
		int _ColCount;
		ArrayList _Cells=new ArrayList();
        
		public HeaderRow(int Row,int nCols)
		{
			this._RowIndex=Row;
			this._ColCount=nCols;			
			for(int i=0;i<nCols;i++)
			{
				_Cells.Add(new HeaderCell(Row,i));
			}
		}
		public HeaderRow(int Row,int nCols,string[] strRow)
		{            
			this._RowIndex=Row;

			this._ColCount=nCols;

            _Cells = new ArrayList();

			for(int i=0;i<nCols;i++)
			{
				HeaderCell hc=new HeaderCell(Row,i);

				if(i<strRow.Length)hc.Text=strRow[i];

				_Cells.Add(hc);
			}
		}
        public HeaderRow(int iRow, ArrayList dataList)
        {
            this._RowIndex = iRow;

            this._ColCount = dataList.Count;

            _Cells = new ArrayList();

            for (int i = 0; i < _ColCount; i++)
            {
                HeaderCell hc = new HeaderCell(iRow, i);

                hc.Text = dataList[i].ToString();

                _Cells.Add(hc);
            }
        }

		#region Serialization
		public HeaderRow(SerializationInfo info, StreamingContext context)
		{		
			try
			{
				this._RowIndex=info.GetInt32("_RowIndex");
			}
			catch
			{
				this._RowIndex=0;
			}
			try
			{
				this._ColCount=info.GetInt32("_ColCount");
			}
			catch
			{
				this._ColCount=0;
			}
			try
			{
				this._Cells=info.GetValue("_Cells",typeof(ArrayList)) as ArrayList;
			}
			catch
			{
				this._Cells=new ArrayList();
			}		
		
		}
		public  void GetObjectData(SerializationInfo info, StreamingContext context)   
		{
			info.AddValue("_RowIndex",this._RowIndex);
			info.AddValue("_ColCount",this._ColCount);
			info.AddValue("_Cells",this._Cells,typeof(ArrayList));
			
		}
		#endregion
		#region Property
		public int RowIndex
		{
			get{return this._RowIndex;}
			set{this._RowIndex=value;}
		}
		public int ColCount
		{
			get{return this._ColCount;}			
			set
			{
				this._ColCount=value;
			}
		}
		public ArrayList Cells
		{
			get{return this._Cells;}			
			set{this._Cells=value;}
		}
		#endregion

		#region Method
		public HeaderCell GetCell(int Col)
		{
            if (Col >= _Cells.Count) return null;
			return this._Cells[Col] as HeaderCell;
		}	

		public void AddCol()
		{
			this._Cells.Add(new HeaderCell(this.RowIndex,this.ColCount));
			this.ColCount++;
		}
		public void RemoveCol(int ColIndex)
		{  
			if(ColIndex>=0&&ColIndex<this.ColCount)
			{ 
				HeaderCell hc=this.GetCell(ColIndex);
				
				//Adjust the cells' Properties of "MergerdCount'and "MergerdIndex"
				int count=hc.MergerdCount;
				
				if(count>0)
				{	
					count=count-2>0 ? count-1: 0;				
					int MergeStartIndex=ColIndex-hc.MergerdIndex;
					int MergeEndIndex=MergeStartIndex+hc.MergerdCount-1;
					for(int i=MergeStartIndex;i<ColIndex;i++)
					{ 
						if(i<0||i>=ColCount)break;		
						GetCell(i).MergerdCount=count;						
					}
					for(int i=ColIndex+1;i<MergeEndIndex;i++)
					{ 
						if(i<0||i>=ColCount)break;						
						GetCell(i).MergerdCount=count;
						GetCell(i).MergerdIndex--;
					}
				}
				
				this._Cells.RemoveAt(ColIndex);
				this.ColCount--;
			    	
			}
		}

		#region Move Headers Left/Right all
		public void MoveLeft()
		{
			if(this._ColCount<=1)return;
            int i=0;

			int index=GetCell(0).MergerdIndex;
			int count=GetCell(0).MergerdCount;

			for(i=1;i<count;i++)
			{
				GetCell(i).MergerdCount--;
				GetCell(i).MergerdIndex--;
				if(GetCell(i).MergerdCount<=1)
				{
					GetCell(i).MergerdCount=0;
					GetCell(i).MergerdIndex=0;
				}
			}			
			for(i=0;i<this._ColCount-1;i++)
			{
				this._Cells[i]=GetCell(i+1).Copy();
			}
			GetCell(_ColCount-1).ToZero();            
			this.AdjustIndex();

		}

		public void MoveRight()
		{
			if(this._ColCount<=1)return;
			int i=0;
			int index=GetCell(_ColCount-1).MergerdIndex;
			int count=GetCell(_ColCount-1).MergerdCount;
			if(count>0)
			{
				for(i=_ColCount-1-index;i<_ColCount;i++)
				{
					GetCell(i).MergerdCount--;					
					if(GetCell(i).MergerdCount<=1)
					{
						GetCell(i).MergerdCount=0;
						GetCell(i).MergerdIndex=0;
					}
				}
			}
			for(i=this._ColCount-1;i>0;i--)
			{
				this._Cells[i]=GetCell(i-1).Copy();
			}
			GetCell(0).ToZero();            
			this.AdjustIndex();
		}
		public void AdjustIndex()
		{
			for(int i=0;i<this._ColCount;i++)
			{
				GetCell(i).Row=this._RowIndex;
				GetCell(i).Col=i;
			}
		}
		#endregion

		public HeaderRow Copy()
		{
			HeaderRow hr=new HeaderRow(this._RowIndex,this._ColCount);
			hr.Cells.Clear();
			foreach(object obj in this._Cells)
			{ 
				HeaderCell hc=(obj as HeaderCell).Copy();
				hr.Cells.Add(hc);
			}
			return hr;
		}

		public void UpdateCellStyle(HeaderCellStyle headerstyle)
		{
			foreach(HeaderCell headercell in this._Cells)
			{				
				headercell.UpdateCellStyle(headerstyle);				
			}
		}
		#endregion


	}
	#endregion	
	

	#region public class HeadersData
	[Serializable]
	public class HeadersData:ISerializable	
	{
		int _RowCount=0,_ColCount=0;
		ArrayList _Rows=new ArrayList();
		private Control _HeaderContainer=null;
		private ArrayList _SelCells=new ArrayList();		
		private TextBox _txtEdit=null;
		private HeaderCell _CurCell=null;
		private bool _GridLine=true;

		private ArrayList _ColsToMerge=new ArrayList();

		#region Constructor

		public HeadersData()
		{
		}
		
		public HeadersData(int nRows,int nCols)
		{
			this._RowCount=nRows;
			this._ColCount=nCols;

            this._Rows = new ArrayList();

			for(int i=0;i<this._RowCount;i++)
			{
				this._Rows.Add(new HeaderRow(i,nCols));
			}			
		}
		public HeadersData(int nRows,int nCols,string[][] strhead)
		{
			this._RowCount=nRows;
			this._ColCount=nCols;

            this._Rows = new ArrayList();

			for(int i=0;i<this._RowCount;i++)
			{
				this._Rows.Add(new HeaderRow(i,nCols,strhead[i]));
			}			
		}
        public HeadersData(ArrayList dataList)
        {
            this._RowCount =1;
            this._ColCount = dataList.Count;

            this._Rows = new ArrayList();
            
             this._Rows.Add(new HeaderRow(0,dataList));
           			
        }
		#endregion

		#region Serialization
		public HeadersData(SerializationInfo info, StreamingContext context)
		{		
			try
			{
				this._RowCount=info.GetInt32("_RowCount");
			}
			catch
			{
				this._RowCount=0;
			}
			try
			{
				this._ColCount=info.GetInt32("_ColCount");
			}
			catch
			{
				this._ColCount=0;
			}
			try
			{
				this._Rows=info.GetValue("_Rows",typeof(ArrayList)) as ArrayList;
			}
			catch
			{
				this._Rows=new ArrayList();
			}
			try
			{
				this._CurCell=info.GetValue("_CurCell",typeof(HeaderCell)) as HeaderCell;
			}
			catch
			{
				this._CurCell=null;
			}

		    try
			{
				this._GridLine=info.GetBoolean("_GridLine");
			}
			catch
			{
				this._GridLine=true;
			}

			//Added this code at 2008-11-5 14:13:58@Simon
			try
			{
				this._ColsToMerge=info.GetValue("_ColsToMerge",typeof(ArrayList)) as ArrayList;
			}
			catch
			{
				this._ColsToMerge=new ArrayList();
			}
		
		}
		public  void GetObjectData(SerializationInfo info, StreamingContext context)   
		{
			info.AddValue("_RowCount",this._RowCount);
			info.AddValue("_ColCount",this._ColCount);
			info.AddValue("_Rows",this._Rows,typeof(ArrayList));
			info.AddValue("_CurCell",this._Rows,typeof(HeaderCell));
            
			info.AddValue("_GridLine",this._GridLine);

			info.AddValue("_ColsToMerge",this._ColsToMerge,typeof(ArrayList));   //Added this code at 2008-11-5 14:13:43@Simon

			
		}
		#endregion

		#region Property
		public int RowCount
		{
			get{return this._RowCount;}
			set
			{
				this._RowCount=value;		    		
			}
		}
		public int ColCount
		{
			get{return this._ColCount;}			
			set{this._ColCount=value;}
		}
		public ArrayList Rows
		{
			get{return this._Rows;}			
			set{this._Rows=value;}
		}
		public int SelCount
		{
			get
			{
				if(this._SelCells==null)return 0;
				return this._SelCells.Count;
			}			
		}
		public TextBox txtEdit
		{
			get{return this._txtEdit;}
			set{this._txtEdit=value;}
		}	
		public bool GridLine
		{
			get{return this._GridLine;}
			set{this._GridLine=value;}
		}

		//Added this code at 2008-11-5 14:15:19@Simon
		public ArrayList ColsToMerge
		{
			get
			{
				if(this._ColsToMerge==null) this._ColsToMerge=new ArrayList();
				return this._ColsToMerge;
			}
			set
			{
				this._ColsToMerge=value;
			}
		}


	#endregion

		#region Method
		public HeaderCell GetCell(int Row,int Col)
		{
			HeaderRow hr=this._Rows[Row] as HeaderRow;
			return hr.GetCell(Col);
		}
		public HeaderRow GetRow(int Row)
		{
			HeaderRow hr=this._Rows[Row] as HeaderRow;
			return hr;
		}
	
		public void SetText(int Row,int Col,string text)
		{
			GetCell(Row,Col).Text=text;
		}
		public void UpdateText(string strText)
		{
			if(this._CurCell!=null)
			{
				this._CurCell.Text=strText;
				
			}			
		}
		public void UpdateCellStyle(HeaderCellStyle headerstyle)
		{
			if(this._CurCell!=null)
			{
				if(headerstyle.UpdateAllRowsStyle)
				{
					HeaderRow headerRow=this.GetRow(_CurCell.Row);

					headerRow.UpdateCellStyle(headerstyle);
					
				}
				else
				{
					this._CurCell.UpdateCellStyle(headerstyle);
				}
			}
		}
		public void SetInitStyle(HeaderCellStyle headerstyle)
		{
			if(this._CurCell!=null)
			{				
				this._CurCell.SetInit(headerstyle);				
			}
		}
		public void CopyHeaderFromField(ArrayList prnHeaders)
		{
			if(prnHeaders.Count!=this.ColCount)return;
			int row=this._CurCell.Row;

			ArrayList MergedGroup=new ArrayList();

			int nCount=1;

			for(int i=0;i<prnHeaders.Count;i++)
			{
				HeaderCell hc=this.GetCell(row,i);
				hc.MergerdCount=0;
				hc.MergerdIndex=0; 

				string text=(string)prnHeaders[i];	

				if(text=="\n")
				{
				   nCount++;
				}
				else
				{
				    if(i!=0)MergedGroup.Add(nCount);

					nCount=1;

					this.SetText(row,i,text);
				}

			}
			 MergedGroup.Add(nCount);
            int startCol=0;
			foreach(int nCol in MergedGroup)
			{
				for(int k=startCol;k<startCol+nCol  ;k++)
				{
					if(nCol>1)
					{
						HeaderCell hc=this.GetCell(row,k);
						hc.MergerdCount=nCol;
						hc.MergerdIndex=k-startCol;
					}
				}

                startCol+=nCol;
			}

			
		}
	
		#region Merge/UnMerge cells
		public void UnMergeCells()
		{
			if(this.SelCount<=0)
			{
				//MessageBox.Show("Please select the cells you want to unmerge!","Select cells",MessageBoxButtons.OK,MessageBoxIcon.Information);
				return;
			}
			for(int i=0;i<this.SelCount;i++)
			{
				Label SelLbl=this._SelCells[i] as Label;
				LabelInfo lblinfo=(LabelInfo)SelLbl.Tag;
				HeaderCell[] selcells=this.GetMergedCells(lblinfo);
				foreach(HeaderCell hc in selcells)
				{
                    if (hc == null) return;
					hc.MergerdCount=0;
					hc.MergerdIndex=0;
				}
			}			
		}
		private Label GetLabelByCell(HeaderCell hc)
		{
			
			string sname=string.Format("{0}",hc.Row*ColCount+hc.Col);
			foreach(Control ctrl in this._HeaderContainer.Controls)
			{
				System.Diagnostics.Debug.WriteLine(ctrl.Name);
				if(ctrl.Name==sname)
				{
					return ctrl as Label;
				}
			}
			return null;
			
		}
		public void RaiseClickEvents(HeaderCell hc)
		{
			if(hc==null)return;
			Label lbl=this.GetLabelByCell(hc);
			if(lbl==null)return;
            this.MyClick(lbl,new EventArgs());
		}
		public HeaderCell MergeCells()
		{
			if(this.SelCount<=1)
			{				
				return null;
			}
			int count=this.SelCount;
			for(int i=0;i<this.SelCount;i++)
			{
				Label SelLbl=this._SelCells[i] as Label;
				LabelInfo lblinfo=(LabelInfo)SelLbl.Tag;
				if(lblinfo.MergedColMax-lblinfo.MergedColMin>0)
				{
					count+=lblinfo.MergedColMax-lblinfo.MergedColMin;
				}
			}

			int row=0;
		    int col=-1;
			for(int i=0;i<this.SelCount;i++)
			{			
				Label SelLbl=this._SelCells[i] as Label;
				LabelInfo lblinfo=(LabelInfo)SelLbl.Tag;
				row=lblinfo.RowIndex;
				if(col<0)
				{
				   col=lblinfo.MergedColMin;	
				   
				}
				else
				{
					col=Math.Min(col,lblinfo.MergedColMin);
				}
				for(int k=lblinfo.MergedColMin;k<=lblinfo.MergedColMax;k++)
				{
					HeaderCell hc=GetCell(lblinfo.RowIndex,k);
					hc.MergerdCount=count;
					hc.MergerdIndex=i+k-lblinfo.MergedColMin;					
				}
			} 
			if(col<0||col>=this.ColCount||row<0||row>=this.RowCount)
			{
				return null;
			}
			return GetCell(row,col);   				
		}
        
		//Added this code at 2008-11-5 14:35:18@Simon
		public void HideHeaderColumns()
			{
				if(this.SelCount!=1||this._CurCell==null)
				{
					MessageBox.Show("Please select the cell whose column should be hidden!","Failed");
					return; 
				}
				int col=this._CurCell.Col;
				bool b_FitHide=true;
				if(this.ColsToMerge.Contains(col))return;
				for(int i=0;i<this.RowCount;i++)
				{
					HeaderCell hc=this.GetCell(i,col);
					if(hc.MergerdCount>1)b_FitHide=false;
				}			
				if(!b_FitHide)
				{
					MessageBox.Show("Couldnn't merge cells whose column contains merged cells!","Failed");
					return; 
				}
				this.ColsToMerge.Add(col);
			}

		#endregion
		public HeadersData Copy()
		{
			HeadersData hd=new HeadersData(this._RowCount,this._ColCount);
			hd.Rows.Clear();
			foreach(object obj in this._Rows)
			{ 
				HeaderRow hr=(obj as HeaderRow).Copy();
				hd._Rows.Add(hr);
			}

			hd._GridLine=this._GridLine;

			#region Modify codes at 2008-11-5 15:03:24@Simon
			hd.ColsToMerge.Clear();
			foreach(int obj in this.ColsToMerge)
			{ 				
				hd.ColsToMerge.Add(obj);
			}
			#endregion        //End Modify

			return hd;
		}

		#region Add/Remove Row/col
		public void AddRow()
		{
			this._Rows.Add(new HeaderRow(_RowCount,_ColCount));
			_RowCount++;

		}		

		public void AddRow(string[] strRow)
		{
			this._Rows.Add(new HeaderRow(_RowCount,_ColCount,strRow));
			_RowCount++;
		 
		}
	
		public void RemoveCol()
		{	
			int ColIndex=this.ColCount-1;
			if(ColIndex<0)return;
			for(int i=0;i<this._RowCount;i++)
			{
				GetRow(i).RemoveCol(ColIndex);
			}
			this.ColCount--;
			this.Resize();
			this._SelCells.Clear();
			this._CurCell=null;

		}

		public void AddCol()
		{
			for(int i=0;i<this._RowCount;i++)
			{
				GetRow(i).AddCol();
			}
			this.ColCount++;

		}

		public void RemoveRow()
		{  
			if(this.SelCount<=0||_CurCell==null)
			{
				//MessageBox.Show("Please select the cells you want to Unmerge!","Select cells",MessageBoxButtons.OK,MessageBoxIcon.Information);
				return;
			}
		
			if(this._RowCount>0)
			{	
				int index=_CurCell.Row;
				this._Rows.RemoveAt(index);
				this._RowCount--;
				this.Resize();
				this._SelCells.Clear();
				this._CurCell=null;
			}
		}

		private void Resize()
		{
			for(int i=0;i<this._Rows.Count;i++)
			{
				HeaderRow hr=this.GetRow(i);
				for(int j=0;j<hr.ColCount;j++)
				{
					HeaderCell hc=hr.GetCell(j);
					hc.Row=i;
					hc.Col=j;					
				}
			}

		}
		public void MoveLeft()
		{
			for(int index=0;index<this._ColsToMerge.Count;index++)
			{	
				int col=(int)this._ColsToMerge[index];  
				col--;
				if(col<0||col>=this._ColCount)
				{
					this._ColsToMerge.RemoveAt(index);
				}
				else
				{
					this._ColsToMerge[index]=col;
				}				
			}

			foreach(HeaderRow row in this._Rows)
			{
				row.MoveLeft();
			}			
		}
		public void MoveRight()
		{
			for(int index=0;index<this._ColsToMerge.Count;index++)
			{
				int col=(int)this._ColsToMerge[index];  
				col++;
				if(col<0||col>=this._ColCount)
				{
					this._ColsToMerge.RemoveAt(index);
				}
				else
				{
					this._ColsToMerge[index]=col;
				}			
			}
			foreach(HeaderRow row in this._Rows)
			{
				row.MoveRight();
			}
		}		
		
		#endregion
		public HeaderCell[] GetMergedCells(LabelInfo info)
		{
			int count=info.MergedColMax-info.MergedColMin+1;			
			HeaderCell[] MergedCells=new HeaderCell[count];
			for(int i=0;i<count;i++)
			{
                HeaderCell cell = GetCell(info.RowIndex, i + info.MergedColMin);
                MergedCells[i] = cell;
			}
			return MergedCells;
		}

	
		#region Modify codes at 2008-11-6 13:56:16@Simon
		public void SetHeadGridLine(WebbTable table,Webb.Collections.Int32Collection HeaderRowsArray)
		{
			int rows=table.GetRows();
			int cols=table.GetColumns();
			if(this.GridLine||HeaderRowsArray.Count<=0||cols<=0||rows<=0)return;
			int maxH=(int)HeaderRowsArray[0],minH=(int)HeaderRowsArray[0];
			foreach(int hrow in HeaderRowsArray)     //Get the Max rowindex and min rowindex of HeaderRows
			{
				maxH=Math.Max(maxH,hrow);   
				minH=Math.Min(minH,hrow);
			}
			for(int i=minH;i<=maxH;i++)
			{   
				if(i>=rows)return;
				for(int j=0;j<cols;j++)
				{   
					IWebbTableCell bordercell=table.GetCell(i,j);
					bordercell.CellStyle.Sides=DevExpress.XtraPrinting.BorderSide.None;
					if(i==minH)bordercell.CellStyle.Sides|=DevExpress.XtraPrinting.BorderSide.Top;
					if(j==0) bordercell.CellStyle.Sides|=DevExpress.XtraPrinting.BorderSide.Left;
					if(j==cols-1) bordercell.CellStyle.Sides|=DevExpress.XtraPrinting.BorderSide.Right;
					if(i==maxH)bordercell.CellStyle.Sides|=DevExpress.XtraPrinting.BorderSide.Bottom;
					if(i==minH&&this.ColsToMerge.Contains(j))
					{											
					   bordercell.CellStyle.Sides|=DevExpress.XtraPrinting.BorderSide.Bottom;;
					}
					if(j<this.ColCount&&i<maxH)
					{
						HeaderCell hc=GetCell(i,j);						
						if(j+hc.MergerdCount-1==cols-1)bordercell.CellStyle.Sides|=DevExpress.XtraPrinting.BorderSide.Right;;
					}
				}              
			}		     
		}
		#endregion        //End Modify

        #region Old SetHeaders
        //public void SetHeaders(WebbTable table,ref int nRow)
        //{
        //    int cols=table.GetColumns();
        //    if(cols<=0)return;
        //    for(int i=0;i<this.RowCount;i++)
        //    {			
        //        for(int j=0;j<this._ColCount;j++)
        //        {					              
        //            if(i>=table.GetRows()||j>=cols)continue;
        //            HeaderCell hc=GetCell(i,j);
        //              IWebbTableCell tablecell=table.GetCell(i+nRow,j);
        //            if(hc.MergerdCount>0)
        //            {
        //                int StartIndex=j;
        //                int EndIndex=j+hc.MergerdCount-1;
        //                if(EndIndex>=cols)EndIndex=cols-1;					
        //                if(EndIndex>StartIndex)
        //                {
        //                    table.MergeCells(i,i,StartIndex,EndIndex);
        //                }				
        //                if(hc.Text!="")tablecell.Text=hc.Text;
        //                j=EndIndex;						
        //            }  
        //            else
        //            {
        //                if(hc.Text!="")tablecell.Text=hc.Text;
        //            }				
					
        //        }

        //    }			
        //    nRow+=this.RowCount;	
        //}
        #endregion


        public void SetHeaders(WebbTable table, ref int nRow,Views.ExControlView i_View)
        {
            int totalColumnsInTable = table.GetColumns();

            if (totalColumnsInTable <= 0) return;

            for (int i = 0; i < this.RowCount; i++)
            {
                for (int j = 0; j < this._ColCount; j++)
                {
                    if (i >= table.GetRows() || j >= totalColumnsInTable) continue;

                    HeaderCell hc = GetCell(i, j);

                    IWebbTableCell tablecell = table.GetCell(i + nRow, j);

                    if (hc == null || tablecell == null) continue;

                    string strHeaderText = hc.Text;

                    strHeaderText = strHeaderText.Replace("[onevalue]", i_View.OneValueScFilter.FilterName);

                    strHeaderText = strHeaderText.Replace("[repeat]", i_View.RepeatFilter.FilterName);

                    strHeaderText = strHeaderText.Replace("[ONEVALUE]", i_View.OneValueScFilter.FilterName);

                    strHeaderText = strHeaderText.Replace("[REPEAT]", i_View.RepeatFilter.FilterName);

                    if (hc.MergerdCount > 0)
                    {
                        int StartIndex = j;

                        int EndIndex = j + hc.MergerdCount - 1;

                        if (EndIndex >= totalColumnsInTable) EndIndex = totalColumnsInTable - 1;

                        if (EndIndex > StartIndex)
                        {
                            table.MergeCells(i, i, StartIndex, EndIndex);
                        }

                        tablecell.Text = strHeaderText;                       

                        j = EndIndex;
                    }
                    else
                    {
                        tablecell.MergeType = MergeTypes.None;

                        tablecell.Text = strHeaderText;                      

                    }

                }

            }
            nRow += this.RowCount;
        }


		private void DrawLabelVertical(object sender,PaintEventArgs arg)
		{			
			Label drawlabel=sender as Label;	
		
			string text=drawlabel.Text;

			if(text.Length<=0)return;

			Graphics g=arg.Graphics;

			g.Clear(drawlabel.BackColor);	
	
			Rectangle rect=arg.ClipRectangle;
            
			StringFormat styleFormat=new StringFormat();		

			styleFormat.Alignment=StringAlignment.Near;

			styleFormat.LineAlignment=StringAlignment.Center;	
			
			g.TranslateTransform(rect.X,rect.Bottom);
			g.RotateTransform(-90f);

			RectangleF newRect=new RectangleF(0,0,rect.Height,rect.Width);

			g.DrawString(text,drawlabel.Font,new SolidBrush(drawlabel.ForeColor), newRect, styleFormat);
			g.ResetTransform();

			
		}

	
		public void PaintCells(Control Ctrl,Rectangle rect,IMultiHeader HeaderView)
		{   
			ArrayList formats=new ArrayList();

            HeaderView.GetPrnHeader(out formats);

			this._HeaderContainer= Ctrl;		
			if(this.ColCount<=0)return;        
			int lblWidth=rect.Width/this.ColCount;
			int lblHeight=rect.Height/this.RowCount;            
			this._SelCells.Clear();
			this._CurCell=null;			
			#region add Controls
			for(int i=0;i<this.RowCount;i++)
			{
				int lblLocX =rect.X;
				int lblLocY=rect.Y+i*lblHeight;
				for(int j=0;j<GetRow(i).ColCount;j++)
				{					
					HeaderCell hc=GetCell(i,j);

					Rectangle LabelRect=new Rectangle(lblLocX, lblLocY,lblWidth,lblHeight);
					
					LabelInfo info = new LabelInfo();					
					info.RowIndex=i;
					string Lbltext=hc.Text;		
			
					info.MergedColMax=info.MergedColMin=j;
					
					string LblName=string.Format("{0}",i*ColCount+j);   
					if(hc.MergerdCount>0)
					{
						LabelRect.Width=hc.MergerdCount*lblWidth;
						j+=hc.MergerdCount-1;
						info.MergedColMax=j;
					} 
                    
					#region Modify codes at 2008-11-5 15:16:51@Simon
					if(this.ColsToMerge.Contains(j)&&(HeaderView.HaveHeader||i>0))
					{
						lblLocX+=LabelRect.Width;  
						continue;
					}
					#endregion        //End Modify

					info.LblLoc=LabelRect.Location;
					info.LblSize =LabelRect.Size;					
					Label lblcell = new Label();
					lblcell.Name = LblName;              
					lblcell.Text = Lbltext;					
					lblcell.TextAlign=System.Drawing.ContentAlignment.MiddleCenter;						
				
					lblcell.BackColor=Control.DefaultBackColor; 
	
					if(i==0&&!HeaderView.HaveHeader&&j<formats.Count)
					{
						StringFormatFlags strflags=(StringFormatFlags)formats[j];  //Added this code at 2008-11-5 8:53:15@Simon

						if((strflags & StringFormatFlags.DirectionVertical)==StringFormatFlags.DirectionVertical)
						{
                            lblcell.Paint += new PaintEventHandler(DrawLabelVertical);
						}
					}				
				

					info.DefaultBackColor=lblcell.BackColor;
					
					lblcell.Tag = info;	

					if(this.GridLine)
					{					
						lblcell.BorderStyle=BorderStyle.FixedSingle;
						lblcell.Location =LabelRect.Location;
						if(this.ColsToMerge.Contains(j))
						{
							lblcell.Size=new Size(lblWidth,rect.Height);	
							Ctrl.Controls.Add(lblcell);
							lblLocX+=LabelRect.Width;  
							continue;
						}
						else
						{
							lblcell.Size = LabelRect.Size;
						}
						
					}
					else
					{
						lblcell.BorderStyle=BorderStyle.None;					
						lblcell.Location=new Point(LabelRect.X+1,LabelRect.Y+1);
						if(this.ColsToMerge.Contains(j))
						{
							lblcell.Size=new Size(LabelRect.Width-2,rect.Height-2);
							lblLocX+=LabelRect.Width;  							
							Ctrl.Controls.Add(lblcell);
							continue;
						}
						else
						{
							lblcell.Size=new Size(LabelRect.Width-2,LabelRect.Height-2);
						}
					}		
					
					
			
					lblLocX+=LabelRect.Width;  
									
					lblcell.Click += new System.EventHandler(MyClick);				
					Ctrl.Controls.Add(lblcell);
				}
			}
			#endregion
		}

		public void SetCellStyle(Label lblcell,HeaderCell hc)
		{	
			if(hc.NeedChangeStyle)
			{
				Color backcolor=hc.CellStyle.BackgroundColor;
				if(backcolor==Color.Empty||backcolor==Color.Transparent)
				{
                    lblcell.BackColor=Control.DefaultBackColor;
				}
				else
				{
					lblcell.BackColor=hc.CellStyle.BackgroundColor;
				}
				lblcell.ForeColor=hc.CellStyle.ForeColor;
				lblcell.Font=hc.CellStyle.Font;
				if(hc.CellStyle.Sides==DevExpress.XtraPrinting.BorderSide.None)
				{
					lblcell.BorderStyle=BorderStyle.None;					
				}

				StringFormatFlags strflags=hc.CellStyle.StringFormat;  //Added this code at 2008-11-5 8:53:15@Simon

				if((strflags & StringFormatFlags.DirectionVertical)==StringFormatFlags.DirectionVertical)
				{
                    lblcell.Paint += new PaintEventHandler(DrawLabelVertical);
				}

				#region Aliginment
				switch(hc.CellStyle.HorzAlignment)
				{
					case HorzAlignment.Default:
					case HorzAlignment.Center:
							{
								switch(hc.CellStyle.VertAlignment)
								{
									case VertAlignment.Default:
									case VertAlignment.Center:
								       lblcell.TextAlign=ContentAlignment.MiddleCenter;
										  break;                                 
									case VertAlignment.Top:									
                                           lblcell.TextAlign=ContentAlignment.TopCenter;
										   break;
                                     case VertAlignment.Bottom:									
                                           lblcell.TextAlign=ContentAlignment.BottomCenter;
										   break;
								}
							}
						    break;                  
					case HorzAlignment.Near:
							{
								switch(hc.CellStyle.VertAlignment)
								{
									case VertAlignment.Default:
									case VertAlignment.Center:
								       lblcell.TextAlign=ContentAlignment.MiddleLeft;
										  break;                                 
									case VertAlignment.Top:									
                                           lblcell.TextAlign=ContentAlignment.TopLeft;
										   break;
                                     case VertAlignment.Bottom:									
                                           lblcell.TextAlign=ContentAlignment.BottomLeft;
										   break;
								}
							}
						    break;
					case HorzAlignment.Far:
					{
						switch(hc.CellStyle.VertAlignment)
						{
							case VertAlignment.Default:
							case VertAlignment.Center:
								lblcell.TextAlign=ContentAlignment.MiddleRight;
								break;                                 
							case VertAlignment.Top:									
								lblcell.TextAlign=ContentAlignment.TopRight;
								break;
							case VertAlignment.Bottom:									
								lblcell.TextAlign=ContentAlignment.BottomRight;
								break;
						}
					}
						break;
				}
				#endregion
			}
			else
			{
				lblcell.BackColor = Control.DefaultBackColor; 
			}
			

		}
		public void DrawSelCells()
		{
			foreach(Control ctrl in this._HeaderContainer.Controls)
			{  
				if(ctrl.Name.StartsWith("cell"))continue;  
               
				ctrl.BackColor=Control.DefaultBackColor;

				LabelInfo lblInfo=(LabelInfo)ctrl.Tag;		
				
				if(lblInfo.DefaultBackColor!=Color.Empty)
				{
                    ctrl.BackColor=lblInfo.DefaultBackColor;
				}
								
			}
			foreach(Object obj in this._SelCells)
			{
				Label lbllast=obj as Label;										
				lbllast.BackColor=Color.Tomato;
			}

		}
		private void AddLabelToArray(int row,int colmin,int colmax)
		{			    
			int minindex=row*this._ColCount+colmin;
			int maxindex=row*this._ColCount+colmax;				
		
			for(int   i=0;i<this._HeaderContainer.Controls.Count;i++)   
			{
				Label lbl=_HeaderContainer.Controls[i] as Label;
                 if(lbl.Name.StartsWith("cell"))continue;
				int index=Convert.ToInt32(lbl.Name);
				if(index>=minindex&&index<=maxindex)   
				{  
					_SelCells.Add(lbl);
				}   
			}   

		}


		#region mouseEvent
		private void MyClick(object sender, System.EventArgs e)
		{			
			Label lctl = sender as Label;
			LabelInfo lblinfo = (LabelInfo)lctl.Tag;   
         	
			if(_CurCell==null||this.SelCount==0)
			{
               
				_SelCells.Clear();
				_SelCells.Add(lctl);
			
			}
			else
			{			
				int row=_CurCell.Row;
				int col=_CurCell.Col;
				
				if(row==lblinfo.RowIndex&&this.SelCount==1)
				{ 
					int mincol=Math.Min(col,lblinfo.MergedColMax);
					int maxcol=Math.Max(col,lblinfo.MergedColMax);
					for(int i=mincol;i<=maxcol;i++)
					{
						if(this.ColsToMerge.Contains(i))
						{        _SelCells.Clear();
						    	_SelCells.Add(lctl);  
							   	_CurCell=GetCell(lblinfo.RowIndex,lblinfo.MergedColMin);
								if(this.txtEdit!=null)
								{
									this.txtEdit.Text=this._CurCell.Text;
								}
							    this.DrawSelCells();
								return;                            
						}
					}
					
					_SelCells.Clear();					
					this.AddLabelToArray(row,mincol,maxcol);					
				 
				}
				else
				{
					_SelCells.Clear();
					_SelCells.Add(lctl);					

				}
				
			}
			_CurCell=GetCell(lblinfo.RowIndex,lblinfo.MergedColMin);
			this.DrawSelCells();
			if(this.txtEdit!=null)
			{
				this.txtEdit.Text=this._CurCell.Text;
			}
			
		}
		#endregion

		#endregion
	}
	#endregion

   

	public class HeaderCellStyle
	{
		#region Auto Constructor By Macro 2009-8-18 9:49:00
		public HeaderCellStyle()
		{
			_ChangeStyle=false;
			_Style=new Webb.Reports.ExControls.BasicStyle();
			_UpdateAllRowsStyle=false;
		}
		#endregion

	
		protected bool _ChangeStyle=false;
		protected BasicStyle _Style=new BasicStyle();
		protected bool _UpdateAllRowsStyle=false;

		public bool NeedChangeStyle
		{
			get{ return _ChangeStyle; }
			set{ _ChangeStyle = value; }
		}

		public Webb.Reports.ExControls.BasicStyle Style
		{
			get{ return _Style; }
			set{ _Style = value; }
		}

		public bool UpdateAllRowsStyle
		{
			get{ return _UpdateAllRowsStyle; }
			set{ _UpdateAllRowsStyle = value; }
		}
	}
}
