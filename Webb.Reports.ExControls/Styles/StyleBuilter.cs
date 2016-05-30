/***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:StyleBuilter.cs
 * Author:Country.Wu [EMail:Webb.Country.Wu@163.com]
 * Create Time:11/21/2007 02:20:19 PM
 * Copyright:1986-2007@Webb Electronics all right reserved.
 * Purpose:
 * ***********************************************************************/
#region History
/*
 * //Author@DateTime : Description
 * */
#endregion History

using System;
using System.Drawing;
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

using DevExpress.Utils;

using Webb.Reports.ExControls;
using Webb.Reports;
using Webb.Collections;
using Webb.Reports.ExControls.Data;


namespace Webb.Reports.ExControls.Styles
{
    #region public class StyleBuilder
    /// <summary>
	/// Summary description for StyleBuilder.
	/// </summary>
	public class StyleBuilder
	{
		#region Build RowsStyle
		//2009-2-12 15:36:36@Simon
		public void BuildMatrixRowsStyle(WebbTable i_Table,StyleRowsInfo i_StyleInfo, ExControlStyles i_Styles, Int32Collection HeaderRows,MatrixInfo matrixinfo,int i_HeaderIndex)
		{	
            int seprateHeaderRow=i_HeaderIndex;

			i_HeaderIndex++;
			//Rows and alternate rows.
			 
			int maxrows=i_Table.GetRows();

			if(matrixinfo.ColTotal.ShowTotal)
			{
				if(matrixinfo.ColTotal.ShowFront)
				{
					i_HeaderIndex++;
				}
				else
				{
					maxrows--;
				}
			}

			if(matrixinfo.RowTotal.ShowTotal)
			{
				for(int m_row =i_HeaderIndex ; m_row<maxrows; m_row+=2)
				{					
					if((m_row-i_HeaderIndex)%4==0)
					{
						i_Table.SetRowStyle(m_row,i_Styles.RowStyle,i_StyleInfo.ShowRowIndicators);						
                        i_Table.SetRowStyle(m_row+1,i_Styles.RowStyle,i_StyleInfo.ShowRowIndicators);

					}
					else
					{
						i_Table.SetRowStyle(m_row,i_Styles.AlternateStyle,i_StyleInfo.ShowRowIndicators);
						i_Table.SetRowStyle(m_row+1,i_Styles.AlternateStyle,i_StyleInfo.ShowRowIndicators);
					}				
				}
			}
			else
			{
				for(int m_row =i_HeaderIndex ; m_row<maxrows; m_row++)
				{
					if(HeaderRows.Contains(m_row) || (i_StyleInfo.TotalRows != null && i_StyleInfo.TotalRows.Contains(m_row)) || (i_StyleInfo.SectionRows != null && i_StyleInfo.SectionRows.Contains(m_row))) continue;
					
					if(m_row%2==0)
					{
						i_Table.SetRowStyle(m_row,i_Styles.RowStyle,i_StyleInfo.ShowRowIndicators);
					}
					else
					{
						i_Table.SetRowStyle(m_row,i_Styles.AlternateStyle,i_StyleInfo.ShowRowIndicators);
					}				
				}
			}
			//Header
			if(i_StyleInfo.HeaderRows != null)
			{
				foreach(int i_row in i_StyleInfo.HeaderRows)
				{
					i_Table.SetRowStyle(i_row,i_Styles.HeaderStyle,i_StyleInfo.ShowRowIndicators);
				}
			}
			//Section
			if(i_StyleInfo.SectionRows != null)
			{
				foreach(int i_row in i_StyleInfo.SectionRows)
				{
					i_Table.SetRowStyle(i_row,i_Styles.SectionStyle,i_StyleInfo.ShowRowIndicators);
				}
			}
			//Total
			if(i_StyleInfo.TotalRows != null)
			{
				foreach(int i_row in i_StyleInfo.TotalRows)
				{
					i_Table.SetRowStyle(i_row,i_Styles.TotalStyle,i_StyleInfo.ShowRowIndicators);
				}
			}
			//Row indicator
			if(i_StyleInfo.ShowRowIndicators)
			{
				i_Table.SetColumnStyle(0,i_Styles.RowIndicatorStyle,HeaderRows);
			}
            if (matrixinfo.HaveSeprateHeader && !matrixinfo.ShowInOneCol)
            {
                if (matrixinfo.SepGroup.ColorNeedChange)
                {
                    i_Table.SetRowStyle(seprateHeaderRow,matrixinfo.SepGroup.Style, i_StyleInfo.ShowRowIndicators);
                }
            }
		}

		public void BuildRowsStyle(WebbTable i_Table,StyleRowsInfo i_StyleInfo, ExControlStyles i_Styles, Int32Collection HeaderRows)
		{
            int maxHeaderIndex = -1;

            foreach (int row in HeaderRows)
            {
              if(row>maxHeaderIndex)maxHeaderIndex = row;
            }

			//Rows and alternate rows.
			for(int m_row =0 ; m_row<i_Table.GetRows(); m_row++)
			{
				if(HeaderRows.Contains(m_row) || (i_StyleInfo.TotalRows != null && i_StyleInfo.TotalRows.Contains(m_row)) || (i_StyleInfo.SectionRows != null && i_StyleInfo.SectionRows.Contains(m_row))) continue;

                int jugdedRows = m_row - maxHeaderIndex;

                 int alterRowInterval = i_Styles.AlternateIntervals.AlternateIntervals + 1;

                if (jugdedRows % alterRowInterval == 0)
                {
                    i_Table.SetRowStyle(m_row, i_Styles.AlternateStyle, i_StyleInfo.ShowRowIndicators);
                }
                else
                {
                    i_Table.SetRowStyle(m_row, i_Styles.RowStyle, i_StyleInfo.ShowRowIndicators);
                }
                
			}
			//Header
			if(i_StyleInfo.HeaderRows != null)
			{
				foreach(int i_row in i_StyleInfo.HeaderRows)
				{
					i_Table.SetRowStyle(i_row,i_Styles.HeaderStyle,i_StyleInfo.ShowRowIndicators);
				}
			}
			//Section
			if(i_StyleInfo.SectionRows != null)
			{
				foreach(int i_row in i_StyleInfo.SectionRows)
				{
					i_Table.SetRowStyle(i_row,i_Styles.SectionStyle,i_StyleInfo.ShowRowIndicators);
				}
			}
			//Total
			if(i_StyleInfo.TotalRows != null)
			{
				foreach(int i_row in i_StyleInfo.TotalRows)
				{
					i_Table.SetRowStyle(i_row,i_Styles.TotalStyle,i_StyleInfo.ShowRowIndicators);
				}
			}
			//Row indicator
			if(i_StyleInfo.ShowRowIndicators)
			{
				i_Table.SetColumnStyle(0,i_Styles.RowIndicatorStyle,HeaderRows);
			}
		}
       
        public void BuildOverrideRowsStyle(WebbTable i_Table, StyleRowsInfo i_StyleInfo, ExControlStyles i_Styles, Int32Collection HeaderRows)
        {
            int maxHeaderIndex = -1;

            foreach (int row in HeaderRows)
            {
                if (row > maxHeaderIndex) maxHeaderIndex = row;
            }

            //Rows and alternate rows.
            for (int m_row = 0; m_row < i_Table.GetRows(); m_row++)
            {
                if (HeaderRows.Contains(m_row) || (i_StyleInfo.TotalRows != null && i_StyleInfo.TotalRows.Contains(m_row)) || (i_StyleInfo.SectionRows != null && i_StyleInfo.SectionRows.Contains(m_row))) continue;

                int jugdedRows = m_row - maxHeaderIndex;

                int alterRowInterval = i_Styles.AlternateIntervals.AlternateIntervals + 1;

                if (jugdedRows % alterRowInterval == 0)
                {
                    i_Table.SetOverrideRowStyle(m_row, i_Styles.AlternateStyle, i_StyleInfo.ShowRowIndicators);
                }
                else
                {
                    i_Table.SetOverrideRowStyle(m_row, i_Styles.RowStyle, i_StyleInfo.ShowRowIndicators);
                }

            }
            //Header
            if (i_StyleInfo.HeaderRows != null)
            {
                foreach (int i_row in i_StyleInfo.HeaderRows)
                {
                    i_Table.SetRowStyle(i_row, i_Styles.HeaderStyle, i_StyleInfo.ShowRowIndicators);
                }
            }
            //Section
            if (i_StyleInfo.SectionRows != null)
            {
                foreach (int i_row in i_StyleInfo.SectionRows)
                {
                    i_Table.SetRowStyle(i_row, i_Styles.SectionStyle, i_StyleInfo.ShowRowIndicators);
                }
            }
            //Total
            if (i_StyleInfo.TotalRows != null)
            {
                foreach (int i_row in i_StyleInfo.TotalRows)
                {
                    i_Table.SetRowStyle(i_row, i_Styles.TotalStyle, i_StyleInfo.ShowRowIndicators);
                }
            }
            //Row indicator
            if (i_StyleInfo.ShowRowIndicators)
            {
                i_Table.SetColumnStyle(0, i_Styles.RowIndicatorStyle, HeaderRows);
            }
        }

		
		#endregion	
		
		#region BuildClickStyle&BuildColumnsStyle&BuildStyle
		//02-25-2008@Scott
		public void BuildColumnsStyle(WebbTable i_Table,StyleColumnInfo i_StyleInfo, ExControlStyles i_Styles, Int32Collection HeaderRows)
		{
			//Group Columns
			if(i_StyleInfo.GroupColumns != null)
			{
				foreach(int i_col in i_StyleInfo.GroupColumns)
				{
					i_Table.SetColumnStyle(i_col,i_Styles.GroupStyle,HeaderRows);
				}
			}

			//Freqency Columns
			if(i_StyleInfo.FreqencyColumns != null)
			{
				foreach(int i_col in i_StyleInfo.FreqencyColumns)
				{
					i_Table.SetColumnStyle(i_col,i_Styles.FreqencyStyle,HeaderRows);
				}
			}

			//Percent Columns
			if(i_StyleInfo.PercentColumns != null)
			{
				foreach(int i_col in i_StyleInfo.PercentColumns)
				{
					i_Table.SetColumnStyle(i_col,i_Styles.PercentStyle,HeaderRows);
				}
			}

			//Total Columns
			if(i_StyleInfo.TotalColumns != null)
			{
				foreach(int i_col in i_StyleInfo.TotalColumns)
				{
					i_Table.SetColumnStyle(i_col,i_Styles.TotalColStyle,HeaderRows);
				}
			}

			//Average Columns
			if(i_StyleInfo.AverageColumns != null)
			{
				foreach(int i_col in i_StyleInfo.AverageColumns)
				{
					i_Table.SetColumnStyle(i_col,i_Styles.AverageStyle,HeaderRows);
				}
			}	
		}

		public void BuildClickStyle(WebbTable i_Table)
		{
			//Click Style
			for(int row = 0 ; row < i_Table.GetRows() ; row++)
			{
				for(int column = 0 ; column < i_Table.GetColumns() ; column++)
				{
					WebbTableCell cell = i_Table.GetCell(row,column) as WebbTableCell;

					if(cell.ClickEventArg != null)
					{
						cell.CellStyle.ForeColor = Color.Blue;
					}
				}
			}
		}


		public void BuildStyle(WebbTable i_Table,StyleRowsInfo i_StyleInfo,StyleColumnInfo i_StyleColInfo, ExControlStyles i_Styles ,Int32Collection i_HeaderRows)
		{
			if(i_StyleInfo != null) BuildRowsStyle(i_Table,i_StyleInfo,i_Styles,i_HeaderRows);

			//if(i_StyleColInfo != null) BuildColumnsStyle(i_Table,i_StyleColInfo,i_Styles,i_HeaderRows);	//06-03-2008@Scott

			BuildClickStyle(i_Table);
		}
	  
		#endregion

		#region Build GroupStyle
		//modified this code at 2008-11-4 15:43:08@Simon
		public void BuildGroupStyle(WebbTable i_Table,StyleRowsInfo i_StyleInfo,Data.GroupInfo i_GroupInfo, ExControlStyles i_Styles ,Int32Collection ignoreRows)
		{
            if (!i_Styles.MajorRowStyle)
            {
                if (i_StyleInfo != null) BuildRowsStyle(i_Table, i_StyleInfo, i_Styles, ignoreRows);
            }

            #region Build Group Style
                 bool bColorNeedChange = true;

			    if(i_Styles.RowStyle.BackgroundColor != i_Styles.AlternateStyle.BackgroundColor)
			    {
				    bColorNeedChange = false;
			    }

			    int MinHeaderIndex=0;	
    		
			    if(i_StyleInfo.HeaderRows.Count==0)
			    {
				    MinHeaderIndex=-1;
			    }
			    else if(i_StyleInfo.ShowFieldTitle)
			    {
                    MinHeaderIndex=i_StyleInfo.HeaderRows[i_StyleInfo.HeaderRows.Count-1];
			    }

			    BuildGroupStyle(i_Table,i_GroupInfo,ignoreRows,MinHeaderIndex,bColorNeedChange);

            #endregion            

            if (i_Styles.MajorRowStyle)
            {
                if (i_StyleInfo != null) BuildOverrideRowsStyle(i_Table, i_StyleInfo, i_Styles, ignoreRows);
            }

            BuildClickStyle(i_Table);
		}		
		//modified this code at 2008-11-4 15:43:08@Simon
		public void BuildGroupStyle(WebbTable i_Table,Data.GroupInfo groupInfo,Int32Collection ignoreRows,int MinHeaderIndex,bool bColorNeedChange)
		{
            bool visible = GroupInfo.IsVisible(groupInfo);

            if (visible)
            {
                i_Table.SetColumnStyle(groupInfo.ColumnIndex, groupInfo.Style, ignoreRows, groupInfo.ColorNeedChange);

                if (MinHeaderIndex >= 0)
                {
                    i_Table.GetCell(MinHeaderIndex, groupInfo.ColumnIndex).CellStyle.StringFormat = groupInfo.HeadingFormat;
                }
            }
			
			if(groupInfo.Summaries != null)
			{
				foreach(Data.GroupSummary summary in groupInfo.Summaries)
				{					
					i_Table.SetColumnStyle(summary.ColumnIndex,summary.Style,ignoreRows,summary.ColorNeedChange);

					i_Table.SetColumnWidth(summary.ColumnIndex,summary.ColumnWidth);
					
					if(MinHeaderIndex>=0)
					{
                       i_Table.GetCell(MinHeaderIndex,summary.ColumnIndex).CellStyle.StringFormat|=summary.HeadingFormat;
					}
				}
			}
		
			foreach(Data.GroupInfo subGroupInfo in groupInfo.SubGroupInfos)
			{
				BuildGroupStyle(i_Table,subGroupInfo,ignoreRows,MinHeaderIndex,subGroupInfo.ColorNeedChange);
			}
		}
		       
		#endregion        //End Modify

		#region Build  CompactGroup Style 
		public void BuildCompactGroupStyle(WebbTable i_Table,StyleRowsInfo i_StyleInfo,Data.GroupInfo i_GroupInfo, ExControlStyles i_Styles ,Int32Collection ignoreRows,Int32Collection RootGroupRows)
		{
			if(i_StyleInfo != null) BuildRowsStyle(i_Table,i_StyleInfo,i_Styles,ignoreRows);
			
			BuildCompactGroupStyle(i_Table,i_GroupInfo,ignoreRows,RootGroupRows,i_StyleInfo);

			BuildClickStyle(i_Table);
		}
		//Modified at 2009-2-10 15:36:06@Scott
		public void BuildCompactGroupStyle(WebbTable i_Table,Data.GroupInfo groupInfo,Int32Collection ignoreRows,Int32Collection rootGroupRows,StyleRowsInfo i_StyleInfo)
		{	
			#region Modify codes at 2009-4-14 14:47:06@Simon
			int MinHeaderIndex=0;	
					
			if(i_StyleInfo.HeaderRows.Count==0)
			{
				MinHeaderIndex=-1;
			}
			else if(i_StyleInfo.ShowFieldTitle)
			{
				MinHeaderIndex=i_StyleInfo.HeaderRows[i_StyleInfo.HeaderRows.Count-1];
			}
			#endregion        //End Modify

			if(groupInfo.SubGroupInfos.Count > 0)
			{
				foreach(int row in rootGroupRows)
				{
					i_Table.SetRowStyle(row,groupInfo.Style,i_StyleInfo.ShowRowIndicators);
				}
			}
			else
			{
				for(int row = 0; row < i_Table.GetRows() - 1; row++)
				{
					if(!ignoreRows.Contains(row) && !rootGroupRows.Contains(row))
					{
						i_Table.SetRowStyle(row,groupInfo.Style,i_StyleInfo.ShowRowIndicators);
					}
				}
			}	

			StringFormatFlags flag = groupInfo.HeadingFormat;

				
				IWebbTableCell cell = i_Table.GetCell(MinHeaderIndex,groupInfo.ColumnIndex);	

				if(cell != null)
				{
					if((flag&StringFormatFlags.DirectionVertical)==StringFormatFlags.DirectionVertical)
					{	
						cell.CellStyle.HorzAlignment=HorzAlignment.Far;
						cell.CellStyle.VertAlignment=VertAlignment.Center;
					}
					cell.CellStyle.StringFormat= flag;	//modify this code at 2008-11-6 9:59:58@Simon
				}
			

			if(groupInfo.Summaries != null)
			{
				foreach(Data.GroupSummary summary in groupInfo.Summaries)
				{
					flag = summary.HeadingFormat;
		
					cell = i_Table.GetCell(MinHeaderIndex,summary.ColumnIndex);
		
					if(cell != null)
					{
						if((flag&StringFormatFlags.DirectionVertical)==StringFormatFlags.DirectionVertical)
						{	
							cell.CellStyle.HorzAlignment=HorzAlignment.Far;
							cell.CellStyle.VertAlignment=VertAlignment.Center;
						}
						cell.CellStyle.StringFormat|=flag;	//modify this code at 2008-11-6 9:59:58@Simon
					}
		
					i_Table.SetColumnStyle(summary.ColumnIndex,summary.Style,ignoreRows);
				}
			}
		
			foreach(Data.GroupInfo subGroupInfo in groupInfo.SubGroupInfos)
			{
				if(subGroupInfo.Summaries != null)
				{
					subGroupInfo.Summaries.Clear();
				}

				BuildCompactGroupStyle(i_Table,subGroupInfo,ignoreRows,rootGroupRows,i_StyleInfo);
			}
		}
		//modified this code at 2008-11-4 15:43:08@Simon
	
		#endregion        //End Modify

		#region Build MatrixGroupStyle
		public void BuildMatrixGroupStyle(WebbTable i_Table,StyleRowsInfo i_StyleInfo,Data.MatrixInfo matrixInfo,ExControlStyles i_Styles ,Int32Collection ignoreRows)
		{
			#region Modify codes at 2009-4-14 14:47:06@Simon
			int MinHeaderIndex=0;	
					
			if(i_StyleInfo.ShowFieldTitle)
			{
                i_StyleInfo.HeaderRows.Sort();

				MinHeaderIndex=i_StyleInfo.HeaderRows[i_StyleInfo.HeaderRows.Count-1];
			}
			#endregion        //End Modify
 
			if(i_StyleInfo != null) BuildMatrixRowsStyle(i_Table,i_StyleInfo,i_Styles,ignoreRows,
										matrixInfo,MinHeaderIndex);
			
			BuildMatrixGroupStyle(i_Table,matrixInfo,ignoreRows,MinHeaderIndex,i_StyleInfo);
		
			BuildClickStyle(i_Table);
		}

		//modified this code at 2008-11-4 15:43:08@Simon
		public void BuildMatrixGroupStyle(WebbTable i_Table,Data.MatrixInfo matrixInfo ,Int32Collection ignoreRows,int i_TitleIndex,StyleRowsInfo i_StyleInfo)
		{
			 Int32Collection HeadersRows=new Int32Collection();

			i_StyleInfo.HeaderRows.CopyTo(HeadersRows);

			int maxrow=i_Table.GetRows()-1;

			if(matrixInfo.ColTotal.ShowTotal)    //Apply TotalRow's Style
			{
				if(matrixInfo.ColTotal.ShowFront)
				{
					if(matrixInfo.ColTotal.NeedChange)i_Table.SetRowStyle(i_TitleIndex+1,matrixInfo.ColTotal.Style,i_StyleInfo.ShowRowIndicators);

					HeadersRows.Add(i_TitleIndex+1);
                
					i_TitleIndex++;
				}
				else
				{
					if(matrixInfo.ColTotal.NeedChange)i_Table.SetRowStyle(maxrow,matrixInfo.ColTotal.Style,i_StyleInfo.ShowRowIndicators);

					HeadersRows.Add(maxrow);
                
					maxrow--;
				}
			}
		
			if(matrixInfo.CellTotal.NeedChange&&matrixInfo.CellTotal.ShowTotal&&matrixInfo.TotalCellRows!=null)
			{
                if (!matrixInfo.TotalCellVertical)
                {                  
                    int titleindex = i_TitleIndex + 2;

                    if (matrixInfo.CellTotal.ShowFront) titleindex = i_TitleIndex + 1;

                    for (int i = titleindex; i <= maxrow; i += 2)
                    {
                        i_Table.SetRowStyle(i, matrixInfo.CellTotal.Style, i_StyleInfo.ShowRowIndicators);
                    }
                }
			}		
			int index=0;

			if(i_StyleInfo.ShowRowIndicators)index++;

			if(matrixInfo.RowGroup.ColorNeedChange)
			{			
				i_Table.SetMatrixColumnStyle(index,matrixInfo.RowGroup.Style,HeadersRows,true);	
			}

			index++;

            if (matrixInfo.HaveSeprateHeader && matrixInfo.ShowInOneCol)
            {
                if (matrixInfo.SepGroup.ColorNeedChange)
                {
                    i_Table.SetMatrixColumnStyle(index, matrixInfo.SepGroup.Style, HeadersRows, true);	
                }
                index++;
            }
            
			int maxcol=i_Table.GetColumns()-1;

			if(matrixInfo.RowTotal.ShowTotal)     //TotalColumn's Style
			{
				if(matrixInfo.RowTotal.ShowFront)
				{
					if(matrixInfo.RowTotal.NeedChange)
					{			
						i_Table.SetMatrixColumnStyle(index,matrixInfo.RowTotal.Style,HeadersRows,true);
						
					}

					index++;
				}
				else
				{
					if(matrixInfo.RowTotal.NeedChange)
					{
						if(matrixInfo.CellTotal.ShowFront&&matrixInfo.CellTotal.ShowTotal)
						{				
							i_Table.SetMatrixColumnStyle(maxcol,matrixInfo.RowTotal.Style,HeadersRows,true);
						}
						else
						{
							i_Table.SetMatrixColumnStyle(maxcol,matrixInfo.RowTotal.Style,ignoreRows,true);
						}
					}

					maxcol--;
				}               
				
			}			
			ArrayList arrStyles=matrixInfo.StepStyles;

			for(int i=index;i<=maxcol;i+=arrStyles.Count)
			{			
				for(int j=0;j<arrStyles.Count;j++)
				{
					Data.CStyle style=(Data.CStyle)arrStyles[j] ;					

                    i_Table.SetMatrixColumnStyle(i + j, style.Style, ignoreRows, style.NeedChange); 
					
				}				   

			}	
           
			#region Modify codes at 2009-4-10 8:57:32@Simon
			if(matrixInfo.ColGroup.UseLineBreak&&!matrixInfo.ShowInOneCol)
			{
				int sepCount=matrixInfo.ColGroup.GroupResults[0].SubGroupInfos[0].GroupResults[0].SubGroupInfos[0].GroupResults.Count;

				int tablerows=i_Table.GetRows()-1;
                for(int j=0;j<=tablerows;j++)
				{
					if(i_StyleInfo.HeaderRows.Contains(j))continue;

					for(int i=index;i<=maxcol;i++)
					{	
						IWebbTableCell cell=i_Table.GetCell(j,i);

						if((i-index)%sepCount!=0)cell.CellStyle.Sides&=~DevExpress.XtraPrinting.BorderSide.Left;

						if((i-index)%sepCount!=sepCount-1)cell.CellStyle.Sides&=~DevExpress.XtraPrinting.BorderSide.Right; 

					}
					
				}
			}
			#endregion        //End Modify

		}
		#endregion		
		
		#region Build GridStyle at 2008-11-6 10:06:11@Simon

		public void BuildGridStyle(WebbTable i_Table,StyleRowsInfo i_StyleInfo,Views.GridInfo i_GridInfo,GroupInfoCollection virtualGroups, ExControlStyles i_Styles ,Int32Collection i_HeaderRows)
		{
            if (!i_Styles.MajorRowStyle)
            {
                if (i_StyleInfo != null) BuildRowsStyle(i_Table, i_StyleInfo, i_Styles, i_HeaderRows);
            }

            BuildGridStyle(i_Table, i_GridInfo, virtualGroups,i_HeaderRows, i_StyleInfo);
          

            if (i_Styles.MajorRowStyle)
            {
                if (i_StyleInfo != null) BuildOverrideRowsStyle(i_Table, i_StyleInfo, i_Styles, i_HeaderRows);
            }

            BuildClickStyle(i_Table);
        }

        private void SetHeaderVerticalFormat(WebbTable i_Table,int MinHeaderIndex,int colIndex, StringFormatFlags flag)
        {
            if (MinHeaderIndex >= 0)
            {   
                IWebbTableCell cell = i_Table.GetCell(MinHeaderIndex, colIndex);

                if (cell != null)
                {
                    if ((flag & StringFormatFlags.DirectionVertical) == StringFormatFlags.DirectionVertical)
                    {
                        cell.CellStyle.HorzAlignment = HorzAlignment.Far;
                        cell.CellStyle.VertAlignment = VertAlignment.Center;
                    }

                    cell.CellStyle.StringFormat |= flag;	//modify this code at 2008-11-6 9:59:58@Simon
                }

            }
        }
       
        public void BuildGridStyle(WebbTable i_Table,Views.GridInfo gridInfo, GroupInfoCollection virtualGroups, Int32Collection i_HeaderRows, StyleRowsInfo i_StyleInfo)
		{
			int MinHeaderIndex=0;	
		
			if(i_StyleInfo.HeaderRows.Count==0)
			{
				MinHeaderIndex=-1;
			}
			else if(i_StyleInfo.ShowFieldTitle)
			{
				MinHeaderIndex=i_StyleInfo.HeaderRows[i_StyleInfo.HeaderRows.Count-1];
			}

            foreach (GroupInfo virtualGroup in virtualGroups)
            {
                if(GroupInfo.IsVisible(virtualGroup))
                {
                    i_Table.SetColumnStyle(virtualGroup.ColumnIndex, virtualGroup.Style, i_HeaderRows, virtualGroup.ColorNeedChange);

                    SetHeaderVerticalFormat(i_Table, MinHeaderIndex, virtualGroup.ColumnIndex, virtualGroup.HeadingFormat);
                }

                foreach (GroupSummary groupSummary in virtualGroup.Summaries)
                {
                    i_Table.SetColumnStyle(groupSummary.ColumnIndex, groupSummary.Style, i_HeaderRows, groupSummary.ColorNeedChange);

                    SetHeaderVerticalFormat(i_Table, MinHeaderIndex, groupSummary.ColumnIndex, groupSummary.HeadingFormat);
                }                
            }

			foreach(Views.GridColumn column in gridInfo.Columns)
			{
                SetHeaderVerticalFormat(i_Table, MinHeaderIndex, column.ColumnIndex, column.TitleFormat);

                i_Table.SetColumnStyle(column.ColumnIndex, column.Style, i_HeaderRows, column.ColorNeedChange);
               
			}
        }
       

        #endregion        //End Modify

        #region StyleInfo
        public class StyleRowsInfo
			{
				public bool ShowFieldTitle=true;
				public bool ShowRowIndicators = false;
				public Webb.Collections.Int32Collection HeaderRows;
				public Webb.Collections.Int32Collection SectionRows;
				public Webb.Collections.Int32Collection TotalRows;
				public StyleRowsInfo(Int32Collection i_h,Int32Collection i_s,Int32Collection i_t, bool i_ShowRowIndicators,bool i_ShowFieldTitle)
				{
					HeaderRows = i_h;
					SectionRows = i_s;
					TotalRows = i_t;
					ShowRowIndicators = i_ShowRowIndicators;
					ShowFieldTitle=i_ShowFieldTitle;
				}
				public StyleRowsInfo(Int32Collection i_h)
				{
					HeaderRows = i_h;
				}
             
                public void ShiftDownRowsAfterDelete(int deletedRow)
                {
                    if (this.HeaderRows != null)
                    {
                        this.HeaderRows.ShiftDownRowsAfterDelete(deletedRow);
                    }
                    if (this.SectionRows != null)
                    {
                        this.SectionRows.ShiftDownRowsAfterDelete(deletedRow);
                    }
                    if (this.TotalRows != null)
                    {
                        this.TotalRows.ShiftDownRowsAfterDelete(deletedRow);
                    }

                }
            }

			//02-25-2008@Scott
			public class StyleColumnInfo
			{
				private Webb.Collections.Int32Collection _GroupColumns;
				private Webb.Collections.Int32Collection _FreqencyColumns;
				private Webb.Collections.Int32Collection _PercentColumns;
				private Webb.Collections.Int32Collection _TotalColumns;
				private Webb.Collections.Int32Collection _AverageColumns;

				public Webb.Collections.Int32Collection GroupColumns
				{
					get{return this._GroupColumns;}
					set{if(value != null) this._GroupColumns = value;}
				}
				public Webb.Collections.Int32Collection FreqencyColumns
				{
					get{return this._FreqencyColumns;}
					set{if(value != null) this._FreqencyColumns = value;}
				}
				public Webb.Collections.Int32Collection PercentColumns
				{
					get{return this._PercentColumns;}
					set{if(value != null) this._PercentColumns = value;}
				}
				public Webb.Collections.Int32Collection TotalColumns
				{
					get{return this._TotalColumns;}
					set{if(value != null) this._TotalColumns = value;}
				}
				public Webb.Collections.Int32Collection AverageColumns
				{
					get{return this._AverageColumns;}
					set{if(value != null) this._AverageColumns = value;}
				}

				public StyleColumnInfo()
				{
					this.GroupColumns = new Int32Collection();
					this.FreqencyColumns = new Int32Collection();
					this.PercentColumns = new Int32Collection();
					this.TotalColumns = new Int32Collection();
					this.AverageColumns = new Int32Collection();
				}

				public void Clear()
				{
					this.GroupColumns.Clear();
					this.FreqencyColumns.Clear();
					this.PercentColumns.Clear();
					this.TotalColumns.Clear();
					this.AverageColumns.Clear();
				}
			}
		#endregion
    }
    #endregion

    #region    public class AlternateRowsIntervals
    [Serializable]
    public class AlternateRowsIntervals : ISerializable
    {
        #region Auto Constructor By Macro 2011-2-16 10:40:07
		public AlternateRowsIntervals()
        {
			_AlternateIntervals=1;
        }

        public AlternateRowsIntervals(int p_AlternateIntervals)
        {
			_AlternateIntervals=p_AlternateIntervals;
        }
		#endregion

    
        protected int _AlternateIntervals = 1;
        public int AlternateIntervals
        {
            get
            {
                if (_AlternateIntervals < 1) _AlternateIntervals = 1;
                return this._AlternateIntervals;
            }
            set { this._AlternateIntervals = value; }
        }

        #region Serialization By Simon's Macro 2011-2-16 10:40:15
		public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
			info.AddValue("_AlternateIntervals",_AlternateIntervals);

        }

        public AlternateRowsIntervals(SerializationInfo info, StreamingContext context)
        {
			try
			{
				_AlternateIntervals=info.GetInt32("_AlternateIntervals");
			}
			catch
			{
				_AlternateIntervals=1;
			}
        }
		#endregion

        #region Copy Function By Macro 2011-2-16 10:52:06
		public AlternateRowsIntervals Copy()
        {
			AlternateRowsIntervals thiscopy=new AlternateRowsIntervals();
			thiscopy._AlternateIntervals=this._AlternateIntervals;
			return thiscopy;
        }
		#endregion
    }
    #endregion

	#region public class ExControlStyles 
    /*Descrition:   */
	[Serializable]
	public class ExControlStyles:ISerializable
	{
		//Wu.Country@2007-11-22 17:17 modified some of the following code.
		public virtual void InitializeStyle()
		{
			// TODO: implement
			//Rows Style
			this._BandStyle = new BasicStyle();
			this._HeaderStyle = new BasicStyle();
			this._RowStyle = new BasicStyle();
			this._SectionStyle = new BasicStyle();
			this._AlternateStyle = new BasicStyle();
			this._TotalStyle = new BasicStyle();

			//Columns Style
			this._RowIndicatorStyle = new BasicStyle();
			//06-03-2008@Scott
//			this._GroupStyle = new BasicStyle();
//			this._FreqencyStyle = new BasicStyle();
//			this._PercentStyle = new BasicStyle();
//			this._TotalColStyle = new BasicStyle();
//			this._AverageStyle = new BasicStyle();

			this._StyleName = "ExControlStyles";
		}

		public ExControlStyles()
		{
			this.InitializeStyle();
        }

        #region Fields
        //Rows Style
		protected BasicStyle _HeaderStyle;
		protected BasicStyle _BandStyle;
		protected BasicStyle _RowStyle;
		protected BasicStyle _AlternateStyle;
		protected BasicStyle _TotalStyle;
		protected BasicStyle _SectionStyle;
		protected BasicStyle _RowIndicatorStyle;
		
		//Columns Style
		protected BasicStyle _GroupStyle;
		protected BasicStyle _FreqencyStyle;
		protected BasicStyle _PercentStyle;
		protected BasicStyle _TotalColStyle;
		protected BasicStyle _AverageStyle;

		protected string _StyleName;

        protected AlternateRowsIntervals _AlternateIntervals = new AlternateRowsIntervals();

        protected bool _MajorRowStyle = false;

        #endregion

        #region Properties
        public bool MajorRowStyle
        {
            get
            {
                return this._MajorRowStyle;
            }
            set
            {
                this._MajorRowStyle = value;
            }
        }
        public AlternateRowsIntervals AlternateIntervals
        {
            get
            {
                if (_AlternateIntervals == null) _AlternateIntervals = new AlternateRowsIntervals();
                  return this._AlternateIntervals; 
            }
            set { this._AlternateIntervals = value; }
        }

        public string StyleName
		{
			get
			{
				if(this._StyleName == null) this._StyleName = "ExControlStyles";

				return this._StyleName;
			}
			set{this._StyleName = value;}
		}
   
		public BasicStyle HeaderStyle
		{
			get
			{
				if(_HeaderStyle == null) _HeaderStyle = new BasicStyle();
				
				return _HeaderStyle;
			}
			set
			{
				if (this._HeaderStyle != value) this._HeaderStyle = value;
			}
		}
      
		public BasicStyle BandStyle
		{
			get
			{
				if(_BandStyle == null) _BandStyle = new BasicStyle();

				return _BandStyle;
			}
			set
			{
				if (this._BandStyle != value) this._BandStyle = value;
			}
		}
      
		public BasicStyle RowStyle
		{
			get
			{
				if(_RowStyle == null) _RowStyle = new BasicStyle();

				return _RowStyle;
			}
			set
			{
				if (this._RowStyle != value) this._RowStyle = value;
			}
		}
      
		public BasicStyle AlternateStyle
		{
			get
			{
				if(_AlternateStyle == null) _AlternateStyle = new BasicStyle();

				return _AlternateStyle;
			}
			set
			{
				if (this._AlternateStyle != value) this._AlternateStyle = value;
			}
		}
      
		public BasicStyle TotalStyle
		{
			get
			{
				if(_TotalStyle == null) _TotalStyle = new BasicStyle();

				return _TotalStyle;
			}
			set
			{
				if (this._TotalStyle != value) this._TotalStyle = value;
			}
		}
      
		public BasicStyle SectionStyle
		{
			get
			{
				if(_SectionStyle == null) _SectionStyle = new BasicStyle();

				return _SectionStyle;
			}
			set
			{
				if (this._SectionStyle != value) this._SectionStyle = value;
			}
		}

		public BasicStyle RowIndicatorStyle
		{
			get
			{
				if(_RowIndicatorStyle == null) _RowIndicatorStyle = new BasicStyle();

				return _RowIndicatorStyle;
			}
			set
			{
				if (this._RowIndicatorStyle != value) this._RowIndicatorStyle = value;
			}
		}

		public BasicStyle GroupStyle
		{
			get
			{
				System.Diagnostics.Debug.Assert(this._GroupStyle == null);	//06-03-2008@Scott

				if(_GroupStyle == null) _GroupStyle = new BasicStyle();

				return _GroupStyle;
			}
			set
			{
				if (this._GroupStyle != value) this._GroupStyle = value;
			}
		}

		public BasicStyle FreqencyStyle
		{
			get
			{
				System.Diagnostics.Debug.Assert(this._FreqencyStyle == null);	//06-03-2008@Scott

				if(_FreqencyStyle == null) _FreqencyStyle = new BasicStyle();

				return _FreqencyStyle;
			}
			set
			{
				if (this._FreqencyStyle != value) this._FreqencyStyle = value;
			}
		}

		public BasicStyle PercentStyle
		{
			get
			{
				System.Diagnostics.Debug.Assert(this._PercentStyle == null);	//06-03-2008@Scott

				if(_PercentStyle == null) _PercentStyle = new BasicStyle();

				return _PercentStyle;
			}
			set
			{
				if (this._PercentStyle != value) this._PercentStyle = value;
			}
		}

		public BasicStyle TotalColStyle
		{
			get
			{
				System.Diagnostics.Debug.Assert(this._TotalColStyle == null);	//06-03-2008@Scott

				if(_TotalColStyle == null) _TotalColStyle = new BasicStyle();

				return _TotalColStyle;
			}
			set
			{
				if (this._TotalColStyle != value) this._TotalColStyle = value;
			}
		}

		public BasicStyle AverageStyle
		{
			get
			{
				System.Diagnostics.Debug.Assert(this._AverageStyle == null);	//06-03-2008@Scott

				if(_AverageStyle == null) _AverageStyle = new BasicStyle();

				return _AverageStyle;
			}
			set
			{
				if (this._AverageStyle != value) this._AverageStyle = value;
			}
        }
        #endregion

        public void ApplyStyle(ExControlStyles i_Style)
		{
			this.BandStyle.SetStyle(i_Style.BandStyle);
			this.HeaderStyle.SetStyle(i_Style.HeaderStyle);
			this.RowStyle.SetStyle(i_Style.RowStyle);
			this.SectionStyle.SetStyle(i_Style.SectionStyle);
			this.AlternateStyle.SetStyle(i_Style.AlternateStyle);			
			this.RowIndicatorStyle.SetStyle(i_Style.RowIndicatorStyle);
			this.TotalStyle.SetStyle(i_Style.TotalStyle);
            this.AlternateIntervals= i_Style.AlternateIntervals.Copy();
            this.MajorRowStyle = i_Style.MajorRowStyle;
			//06-03-2008@Scott
//			this.GroupStyle.SetStyle(i_Style.GroupStyle);
//			this.FreqencyStyle.SetStyle(i_Style.FreqencyStyle);
//			this.PercentStyle.SetStyle(i_Style.PercentStyle);
//			this.TotalColStyle.SetStyle(i_Style.TotalColStyle);
//			this.AverageStyle.SetStyle(i_Style.AverageStyle);
			this.StyleName = i_Style.StyleName;
        }


        public bool IsFontEdit(Font font)
        {
            Font defaultFont=new Font(AppearanceObject.DefaultFont.FontFamily, 10f);

            return font.Equals(defaultFont);

        }
        public bool IsAllStyleFontEdited()
        {          
            Font headerFont = HeaderStyle.Font;

            Font rowFont = RowStyle.Font;

            Font alterateFont = AlternateStyle.Font;

            if(headerFont.Equals(rowFont)&&headerFont.Equals(alterateFont)&&IsFontEdit(headerFont))
            {
                return false;
            }

            return true;
        }

        #region Load SaveStyle
        //Scott@2007-12-29 13:12 modified some of the following code.
		public bool Save(string strFileName)
		{
			try
			{
				Webb.Utilities.Serializer.Serialize(this,strFileName,true);
			}
			catch
			{
				return false;
			}
			return true;
		}

		public bool Load(string strFileName)
		{
			try
			{
				ExControlStyles styles = Webb.Utilities.Serializer.Deserialize(strFileName) as ExControlStyles;
				this.ApplyStyle(styles);
			}
			catch(Exception ex)
			{
                System.Diagnostics.Debug.WriteLine(ex.Message);

				return false;
			}
			return true;
		}

        //08-14-2008@Scott
        public bool LoadDefaultStyle()
        {
            string strFilterPath = Webb.Utility.ApplicationDirectory + "DefaultStyle.wrst";

            if (System.IO.File.Exists(strFilterPath))
            {
                return this.Load(strFilterPath);
            }
            else
            {
                this.StyleName = "DefaultStyle";
                
                this.Save(strFilterPath);
            }

            return true;
        }

		public override string ToString()
		{
			return this.StyleName;
		}       

        #endregion

        #region Serialization By Simon's Macro 2011-2-16 14:41:37
		public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
			info.AddValue("_HeaderStyle",_HeaderStyle,typeof(Webb.Reports.ExControls.BasicStyle));
			info.AddValue("_BandStyle",_BandStyle,typeof(Webb.Reports.ExControls.BasicStyle));
			info.AddValue("_RowStyle",_RowStyle,typeof(Webb.Reports.ExControls.BasicStyle));
			info.AddValue("_AlternateStyle",_AlternateStyle,typeof(Webb.Reports.ExControls.BasicStyle));
			info.AddValue("_TotalStyle",_TotalStyle,typeof(Webb.Reports.ExControls.BasicStyle));
			info.AddValue("_SectionStyle",_SectionStyle,typeof(Webb.Reports.ExControls.BasicStyle));
			info.AddValue("_RowIndicatorStyle",_RowIndicatorStyle,typeof(Webb.Reports.ExControls.BasicStyle));
			info.AddValue("_GroupStyle",_GroupStyle,typeof(Webb.Reports.ExControls.BasicStyle));
			info.AddValue("_FreqencyStyle",_FreqencyStyle,typeof(Webb.Reports.ExControls.BasicStyle));
			info.AddValue("_PercentStyle",_PercentStyle,typeof(Webb.Reports.ExControls.BasicStyle));
			info.AddValue("_TotalColStyle",_TotalColStyle,typeof(Webb.Reports.ExControls.BasicStyle));
			info.AddValue("_AverageStyle",_AverageStyle,typeof(Webb.Reports.ExControls.BasicStyle));
			info.AddValue("_StyleName",_StyleName);
			info.AddValue("_AlternateIntervals",_AlternateIntervals,typeof(Webb.Reports.ExControls.Styles.AlternateRowsIntervals));
			info.AddValue("_MajorRowStyle",_MajorRowStyle);

        }

        public ExControlStyles(SerializationInfo info, StreamingContext context)
        {
			try
			{
				_HeaderStyle=(Webb.Reports.ExControls.BasicStyle)info.GetValue("_HeaderStyle",typeof(Webb.Reports.ExControls.BasicStyle));
			}
			catch
			{
			}
			try
			{
				_BandStyle=(Webb.Reports.ExControls.BasicStyle)info.GetValue("_BandStyle",typeof(Webb.Reports.ExControls.BasicStyle));
			}
			catch
			{
			}
			try
			{
				_RowStyle=(Webb.Reports.ExControls.BasicStyle)info.GetValue("_RowStyle",typeof(Webb.Reports.ExControls.BasicStyle));
			}
			catch
			{
			}
			try
			{
				_AlternateStyle=(Webb.Reports.ExControls.BasicStyle)info.GetValue("_AlternateStyle",typeof(Webb.Reports.ExControls.BasicStyle));
			}
			catch
			{
			}
			try
			{
				_TotalStyle=(Webb.Reports.ExControls.BasicStyle)info.GetValue("_TotalStyle",typeof(Webb.Reports.ExControls.BasicStyle));
			}
			catch
			{
			}
			try
			{
				_SectionStyle=(Webb.Reports.ExControls.BasicStyle)info.GetValue("_SectionStyle",typeof(Webb.Reports.ExControls.BasicStyle));
			}
			catch
			{
			}
			try
			{
				_RowIndicatorStyle=(Webb.Reports.ExControls.BasicStyle)info.GetValue("_RowIndicatorStyle",typeof(Webb.Reports.ExControls.BasicStyle));
			}
			catch
			{
			}
			try
			{
				_GroupStyle=(Webb.Reports.ExControls.BasicStyle)info.GetValue("_GroupStyle",typeof(Webb.Reports.ExControls.BasicStyle));
			}
			catch
			{
			}
			try
			{
				_FreqencyStyle=(Webb.Reports.ExControls.BasicStyle)info.GetValue("_FreqencyStyle",typeof(Webb.Reports.ExControls.BasicStyle));
			}
			catch
			{
			}
			try
			{
				_PercentStyle=(Webb.Reports.ExControls.BasicStyle)info.GetValue("_PercentStyle",typeof(Webb.Reports.ExControls.BasicStyle));
			}
			catch
			{
			}
			try
			{
				_TotalColStyle=(Webb.Reports.ExControls.BasicStyle)info.GetValue("_TotalColStyle",typeof(Webb.Reports.ExControls.BasicStyle));
			}
			catch
			{
			}
			try
			{
				_AverageStyle=(Webb.Reports.ExControls.BasicStyle)info.GetValue("_AverageStyle",typeof(Webb.Reports.ExControls.BasicStyle));
			}
			catch
			{
			}
			try
			{
				_StyleName=info.GetString("_StyleName");
			}
			catch
			{
				_StyleName=string.Empty;
			}
			try
			{
				_AlternateIntervals=(Webb.Reports.ExControls.Styles.AlternateRowsIntervals)info.GetValue("_AlternateIntervals",typeof(Webb.Reports.ExControls.Styles.AlternateRowsIntervals));
			}
			catch
			{
				_AlternateIntervals=new AlternateRowsIntervals();
			}
			try
			{
				_MajorRowStyle=info.GetBoolean("_MajorRowStyle");
			}
			catch
			{
				_MajorRowStyle=false;
			}
        }
		#endregion

     
    }
	#endregion


	#region Build in Styles
	//Wu.Country@2007-11-22 17:19 added this region.
	[Serializable]
	public class SimplyStyle : ExControlStyles
	{
		public override void InitializeStyle()
		{
			// TODO: implement
			this._BandStyle = new BasicStyle();
			this._HeaderStyle = new BasicStyle();
			this._RowStyle = new BasicStyle();
			this._SectionStyle = new BasicStyle();
			this._AlternateStyle = new BasicStyle();
			this._TotalStyle = new BasicStyle();
			this._RowIndicatorStyle = new BasicStyle();
			this._StyleName = "SimplyStyle";
		}
      
		public SimplyStyle()
		{
			// TODO: implement
		}
   
	}

	[Serializable]
	public class ColorfulStyle_GrayGold : ExControlStyles
	{
		public override void InitializeStyle()
		{
			// TODO: implement
			this._BandStyle = new BasicStyle();
			//
			this._HeaderStyle = new BasicStyle();			
			this._HeaderStyle.BackgroundColor = Color.Gray;
			//
			this._RowStyle = new BasicStyle();
			this._RowStyle.BackgroundColor = Color.Gold;
			this._RowStyle.HorzAlignment = DevExpress.Utils.HorzAlignment.Default;
			//
			this._AlternateStyle = new BasicStyle();
			this._AlternateStyle.BackgroundColor = Color.Gold;
			this._AlternateStyle.HorzAlignment = DevExpress.Utils.HorzAlignment.Default;
			//
			this._SectionStyle = new BasicStyle();
			this._SectionStyle.HorzAlignment = DevExpress.Utils.HorzAlignment.Default;
			//
			this._RowIndicatorStyle = new BasicStyle();
			//
			this._TotalStyle = new BasicStyle();
			this._TotalStyle.BackgroundColor = Color.LightGray;
			//
			this._StyleName = "ColorfulStyle_GrayGold";
		}
      
		public ColorfulStyle_GrayGold()
		{
			// TODO: implement
		}
   
	}
	#endregion

}
