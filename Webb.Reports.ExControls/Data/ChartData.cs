using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Collections;
using System.Reflection;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using  System.Drawing.Text;

using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Container;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraPrinting;
using DevExpress.Utils.Design;
using DevExpress.Utils;

using Webb.Data;
using Webb.Collections;
 

namespace Webb.Reports.ExControls.Data
{
	#region public class ChartBase
	[Serializable]
	public class ChartBase
	{
		protected Int32Collection ColsIndex=new Int32Collection();
		// cells
		protected IChartCell[,] table;
		// row count
		protected int nRowCount;
		// column count
		protected int nColumnCount;
		// settings
		protected WebbChartSetting setting;

		protected int nTotals=0;

		public WebbChartSetting Setting
		{
			get{if(this.setting == null) this.setting = new WebbChartSetting();
				return this.setting;}
			set{this.setting = value;}
		}

		protected ArrayList clickAreas=new ArrayList();  //Added this code at 2008-11-11 9:07:48@Simon

		public ArrayList ClickAreas     //Added this code at 2008-11-11 9:07:45@Simon
		{
			get
			{
				if(this.clickAreas == null) this.clickAreas = new ArrayList();

				return this.clickAreas;
			}
			set{this.clickAreas = value;}
		}

		protected SeriesCollection AllSeries
		{
			get{return this.setting.SeriesCollection;}
		}
		protected SeriesLabel SeriesLabel(int index)
		{          		  
			return this.AllSeries[index].SeriesLabel;
			  
		}	
		


		/// <summary>
		/// ctor
		/// </summary>
		/// <param name="nRowCount"></param>
		/// <param name="nColumnCount"></param>
		public ChartBase()
		{
			this.clickAreas=new ArrayList();

		}

		/// <summary>
		/// create table
		/// </summary>
		/// <param name="nRowCount"></param>
		/// <param name="nColumnCount"></param>
		public void InitTable(int nRowCount, int nColumnCount)
		{
			table = new ChartCell[nRowCount, nColumnCount];

			lock(Webb.Utility.SycObject)
			{
				for(int i = 0; i< nRowCount; i++)
				{
					for(int j = 0; j< nColumnCount; j++)
					{
						table[i,j] = new ChartCell();

                        table[i,j].Name="[NoValue]";
					}
				}
			}

			this.nRowCount = nRowCount;
			
			this.nColumnCount = nColumnCount;
		}
		
		/// <summary>
		/// Get certain Chartcell at the location
		/// </summary>
		/// <param name="nRow"></param>
		/// <param name="nColumn"></param>
		public IChartCell GetCell(int nRow, int nColumn)
		{
			if(nRow < 0 || nColumn < 0 || nRow >= this.nRowCount || nColumn >= this.nColumnCount)
			{
				return null;
			}

			return this.table[nRow,nColumn];
		}

		/// <summary>
		/// get All the cells count
		/// </summary>		
		public int GetCellCount()     //Added this code at 2008-11-11 8:36:15@Simon
		{
			return this.nRowCount*this.nColumnCount;
		}

		/// <summary>
		///define wheter need to draw chart
		/// </summary>	
		public bool IsEmptyChart()
		{
			if(nColumnCount*nRowCount==0)return true;

			float datamax=this.GetMaxDataPoint();

			if(datamax<=0)return true;

			return false;

		}

		/// <summary>
		///set the click-event to the chart
		/// </summary>	
		public void SetClickArgs(WebbTable PrintingTable,DataSet m_DBSet)
		{    
			if(this.GetCellCount()<1)return;			

			for(int m_row=0;m_row<this.nRowCount;m_row++)
			{
				for(int m_col=0;m_col<this.nColumnCount;m_col++)
				{
					int colindex=m_row*this.nColumnCount+m_col;               
               
					WebbTableCell m_cell=PrintingTable.GetCell(0,colindex+1) as WebbTableCell;				

					m_cell.CellStyle.BorderColor = Color.Transparent;	
				
					Int32Collection arrRows=this.table[m_row,m_col].RowIndicators;
			
					Webb.Reports.DataProvider.VideoPlayBackArgs m_args = new Webb.Reports.DataProvider.VideoPlayBackArgs(m_DBSet,arrRows);
			
					m_cell.ClickEventArg = m_args;

				}
			}
		}

		/// <summary>
		///whether the chart has multi-group
		/// </summary>	
		protected bool HaveRoot()
		{
			bool root=false,main=false;

			foreach(Series series in this.AllSeries)
			{
				root=root||series.IsRoot;

				main=main||series.MainOrder;
			}
			if(!main)return false;

			return root;
		}

		/// <summary>
		///get color of each cell show be drawn  in pie-chart
		/// </summary>	
		protected ValueColor ValueColor(int realCol,string name)
		{
		    Series series=this.setting.SeriesCollection[realCol];		

			ValueColor valuecolor=series.ValuesStyle.GetColor(name);			

			return valuecolor;

		}


		#region Original CalculateTable
//		/// <summary>
//		/// calculate table by setting
//		/// </summary>
//		virtual public void CalculateTable(DataTable table, Collections.Int32Collection rows)
//		{
//			if(table == null)
//			{
//				this.FillRandomData();
//
//				return;
//			}
//
//			if(rows == null)
//			{
//				DBFilter filter = new DBFilter();
//		
//				rows = filter.GetFilteredRows(table);
//			}
//
//			Series mainSeries = Setting.SeriesCollection[0];
//
//			FieldGroupInfo mainGroupInfo = new FieldGroupInfo(mainSeries.FieldArgument);
//
//			ArrayList fields = mainGroupInfo.GetFields(table,rows);
//
//			this.InitTable(fields.Count,Setting.SeriesCollection.Count);
//
//			for(int rowIndex = 0; rowIndex < Setting.SeriesCollection.Count; rowIndex++)
//			{
//				Series series = Setting.SeriesCollection[rowIndex];
//
//				FieldGroupInfo groupInfo = mainGroupInfo.Copy() as FieldGroupInfo;
//
//				groupInfo.Filter = series.Filter.Copy();
//
//				groupInfo.CalculateGroupResult(table,rows,rows,groupInfo);
//
//				for(int colIndex = 0; colIndex < fields.Count; colIndex ++)
//				{
//					string strField = fields[colIndex].ToString();
//
//					IChartCell cell = this.GetCell(colIndex,rowIndex);
//
//					GroupResult result = groupInfo.GroupResults[strField];
//
//					if(result != null)
//					{
//						cell.DataPoint = result.RowIndicators.Count;
//
//						cell.Name = strField;
//
//						result.RowIndicators.CopyTo(cell.RowIndicators);  //Added this code at 2008-11-11 8:53:39@Simon
//					}
//				}
//			}
//		}
		#endregion
	
		#region New CalculateTable
		//   //Simon@12-15-2008 Modified for grouping data
		/// <summary>
		/// calculate table by setting
		/// </summary>
		virtual public void CalculateTable(DataTable table, Collections.Int32Collection rows)
		{
			this.ColsIndex=new Int32Collection();

			if(table == null)
			{
				this.FillRandomData();

				return;
			}

			if(rows == null)
			{
				DBFilter filter = new DBFilter();

				rows = filter.GetFilteredRows(table);
			}
				
			if(this.setting.AutoFitSize)
			{
				nTotals=rows.Count;
			}
			else
			{
				#region Old Codes

//				nTotals=table.Rows.Count;

//				if(this.setting.OutFilters!=null)
//				{					
//					DBFilter innerFilter=new DBFilter();
//
//					Int32Collection innerRows=innerFilter.GetFilteredRows(table);
//						
//					for(int i=0;i<this.setting.OutFilters.Count;i++)
//					{
//						innerFilter=this.setting.OutFilters[i];
//
//						if(innerFilter!=null)innerRows=this.setting.OutFilters[i].GetFilteredRows(table,innerRows);
//					}
//                        
//					nTotals=innerRows.Count;
//
//					if(nTotals<rows.Count)nTotals=table.Rows.Count;
				   
//				}
				#endregion

				if(this.setting.DenominatorFilter!=null)
				{
					Int32Collection innerRows=this.setting.DenominatorFilter.GetFilteredRows(table);
				
					nTotals=innerRows.Count;
				}
				else
				{
					nTotals=table.Rows.Count;
				}	
			}	
		
			if(nTotals==0)return;

			int nRows=0;

			GroupInfoCollection groupInfos = new GroupInfoCollection();

			#region DataStructure for Grouping Data 

			GroupInfoCollection rootGroupinfos=new GroupInfoCollection();

			ArrayList RootGroups=new ArrayList();

			ArrayList MainGroups=new ArrayList();

			Int32Collection AllThird=new Int32Collection();    
             
			for(int rowIndex = 0; rowIndex< this.AllSeries.Count; rowIndex++)
			{
				Series series =AllSeries[rowIndex];

				ChartGroupInfo info=null;

				if(series.IsRoot)
				{
					info=new ChartGroupInfo(rowIndex,series.FieldArgument);

					RootGroups.Add(info);
				}
				else if(series.MainOrder)
				{
					info=new ChartGroupInfo(rowIndex,series.FieldArgument);

					MainGroups.Add(info);
				}
				else
				{
					AllThird.Add(rowIndex);
				}
			}

			if(MainGroups.Count==0)  //if have no MainGroups,Remove All the RootGroups into MainGroups 
			{
				foreach(ChartGroupInfo info in RootGroups)
				{                  
					MainGroups.Add(info);                                       
				}					
				RootGroups.Clear();

			}				

			int MainOrder=0,JudgeIndex=0;		

			Int32Collection RestThird=new Int32Collection();

			AllThird.CopyTo(RestThird);

			foreach(ChartGroupInfo info in MainGroups)    //Create Tree Structure to calculate results
			{
				for(int i=0;i<RestThird.Count;i++)
				{
					int index=RestThird[i];

					if(info.iName==AllSeries[index].FieldArgument)
					{
						info.subIndexs.Add(index);

						RestThird.RemoveAt(i);

						i--;
					}
				}

				if(info.subIndexs.Count<=0)
				{
					MainOrder=JudgeIndex;
				}

				JudgeIndex++;
			}

			if(RootGroups.Count>0)
			{
				ChartGroupInfo RootInfo=RootGroups[0] as ChartGroupInfo;	

				foreach(ChartGroupInfo mainInfo in MainGroups)
				{
					RootInfo.subIndexs.Add(mainInfo.index);
				}
			}
			if( MainGroups.Count>0)
			{
				ChartGroupInfo MainInfo=MainGroups[MainOrder] as ChartGroupInfo;		
	               
				foreach(int index in RestThird)
				{
					MainInfo.subIndexs.Add(index);
				}
			}
				
			#endregion        //End Modify

			if(MainGroups.Count==0)
			{
				#region Set GroupInfoCollection With Simple Group

				this.ColsIndex.Clear();

				for(int rowIndex = 0; rowIndex < AllSeries.Count; rowIndex++)
				{					
					Series series =AllSeries[rowIndex];

					this.ColsIndex.Add(rowIndex);

					GroupInfo groupInfo=this.CreateGroupInfo(rowIndex);

					if(series.FieldArgument.Trim()!=string.Empty)
					{
						if(series.Name=="New Series")
						{
							series.Name=series.FieldArgument;
						}
					}
	

					Int32Collection filterRows=series.Filter.GetFilteredRows(table,rows);

					Int32Collection reFilterRows=series.MinValueFilter.GetReFilterRows(table,groupInfo,filterRows);  //2009-4-21 8:55:48@Simon Add this Code
      					
					Int32Collection excludeRows=this.ExcludeRows(filterRows,reFilterRows);
						
					groupInfo.CalculateGroupResult(table,reFilterRows,reFilterRows,reFilterRows,groupInfo);
										
					this.SortResults(groupInfo,series);

					series.MinValueFilter.AddNewGroupResult(groupInfo,excludeRows);

					groupInfos.Add(groupInfo);

					nRows = Math.Max(groupInfo.GroupResults.Count,nRows);
				}  
				#endregion
			}
			else if(RootGroups.Count==0)
			{
				#region Non Root Group,Only Main Group
				this.ColsIndex.Clear();

				groupInfos.Clear();

				for(int i=0;i<AllSeries.Count;i++)
				{
					groupInfos.Add(new GroupInfo());

					this.ColsIndex.Add(i);
				}        

				foreach(ChartGroupInfo mainInfo in MainGroups)
				{
					GroupInfo maingroupInfo=this.CreateGroupInfo(mainInfo.index);

					maingroupInfo.CalculateGroupResult(table,rows,rows,rows,maingroupInfo);

					Series series =AllSeries[mainInfo.index];

					this.SortResults(maingroupInfo,series);	
					
					if(series.FieldArgument.Trim()!=string.Empty)
					{
						if(series.Name=="New Series")
						{
							series.Name=series.FieldArgument;
						}
					}			

					groupInfos[mainInfo.index]=maingroupInfo;

					nRows=Math.Max(maingroupInfo.GroupResults.Count,nRows);

					foreach(int subindex in mainInfo.subIndexs)
					{
						series=AllSeries[subindex];
							
						GroupInfo groupinfo=maingroupInfo.CreateNewByFilter(table,series.Filter);
	                       
						groupInfos[subindex]=groupinfo; 
					}                        
						
				}
				#endregion
			}
			else
			{
				#region Have Root Group
				foreach(ChartGroupInfo rootInfo in RootGroups)
				{
					GroupInfo rootgroupInfo=this.CreateGroupInfo(rootInfo.index);

					rootgroupInfo.ColumnHeading=rootInfo.index.ToString();

					Series series =AllSeries[rootInfo.index];						
					
					if(series.FieldArgument.Trim()!=string.Empty)
					{
						if(series.Name=="New Series")
						{
							series.Name=series.FieldArgument;
						}
					}			
					
					foreach(int index in rootInfo.subIndexs)
					{
						ChartGroupInfo mainInfo=this.Findinfo(MainGroups,index);

						GroupInfo groupInfo=this.CreateGroupInfo(index);

						groupInfo.ColumnIndex=index;

						series =AllSeries[mainInfo.index];						
					
						if(series.FieldArgument.Trim()!=string.Empty)
						{
							if(series.Name=="New Series")
							{
								series.Name=series.FieldArgument;
							}
						}			
		
						groupInfo.Summaries=new GroupSummaryCollection();
							
						foreach(int subindex in mainInfo.subIndexs)
						{  
							GroupSummary m_Summary=new GroupSummary();

							m_Summary.Filter=AllSeries[subindex].Filter.Copy();
								
							m_Summary.SummaryType=SummaryTypes.Frequence;

							m_Summary.ColumnIndex=subindex;  //Must Set After  SummaryType   

							groupInfo.Summaries.Add(m_Summary);
	  
						}                         
						rootgroupInfo.SubGroupInfos.Add(groupInfo);


					}
					rootgroupInfo.CalculateGroupResult(table,rows,rows,rows,rootgroupInfo);	

					this.SortAll(rootgroupInfo,rootInfo);
					
					rootGroupinfos.Add(rootgroupInfo);
				}
				ColsIndex.Clear();

				groupInfos.Clear();

				for(int index=0;index<rootGroupinfos.Count;index++)
				{
					GroupInfo rootgroupInfo = rootGroupinfos[index];

					ChartGroupInfo rootgroup=RootGroups[index] as ChartGroupInfo;

					Int32Collection structInfo=new Int32Collection();

					GroupInfoCollection horizoninfos=rootgroupInfo.ConvertCollection(table,ref structInfo);

					System.Diagnostics.Debug.Assert(horizoninfos.Count==structInfo.Count);

					ColsIndex.Add(rootgroup.index);

					groupInfos.Add(rootgroupInfo);

					nRows=Math.Max(rootgroupInfo.GroupResults.Count,nRows);
						
					for(int i=0;i<structInfo.Count;i++)
					{
						ColsIndex.Add(structInfo[i]);

						groupInfos.Add(horizoninfos[i]);                       
					}
						
				}
				#endregion
			}
			if(this.Setting.TopCount>0)
			{
				nRows=Math.Min(nRows,this.Setting.TopCount);
			}
			this.InitTable(nRows,groupInfos.Count);

			this.SetCellValue(groupInfos);

		}

		#endregion

		#region Sub-Functions For CaculateResults

			/// <summary>
			///calculate the row that are not cotainned the ReFilterd rows in pieChart
			/// </summary>	
			private Int32Collection ExcludeRows(Int32Collection rows,Int32Collection refilterRows)
			{
				Int32Collection ExcludeRows=new Int32Collection();

				foreach(int i in rows)
				{
					if(refilterRows.Contains(i))continue;

					ExcludeRows.Add(i);
				}

				return ExcludeRows;
			}

			/// <summary>
			///sort the chartCells value by the groupInfo's sorting defination
			/// </summary>	
			private void SortResults(GroupInfo groupInfo,Series series)
			{		
				if(series.SeriesLabel.SortingType==Webb.Data.SortingTypes.Ascending)
				{				
					groupInfo.GroupResults.Sort(SortingTypes.Ascending,series.SeriesLabel.SortingByTypes);
					             
				}
				else if(series.SeriesLabel.SortingType==Webb.Data.SortingTypes.Descending)
				{ 
					groupInfo.GroupResults.Sort(SortingTypes.Descending,series.SeriesLabel.SortingByTypes);
	               
				}				
			}

			/// <summary>			
			///find the ChartGroupInfo by main index ,it should be applied in mutigeoup level.
			/// </summary>	
			protected ChartGroupInfo Findinfo(ArrayList lst_Infos,int index)
			{
				foreach(ChartGroupInfo info in lst_Infos)
				{
					if(info.index==index)return info; 
				}
				return null;
			}

			/// <summary>
			///Create GroupInfo by series setting
			/// </summary>	
			private GroupInfo CreateGroupInfo(int index)
			{
				Series series =AllSeries[index];

				GroupInfo groupInfo=null;						

				if(series.FieldArgument==string.Empty&&series.SectionFilters.Count>0)
				{	
					SectionGroupInfo sectionGroupInfo=new SectionGroupInfo();	
				
					sectionGroupInfo.SectionFiltersWrapper=series.SectionFiltersWrapper;

					groupInfo=sectionGroupInfo;
				}
				else
				{
					groupInfo=new FieldGroupInfo(series.FieldArgument);  
				}

				groupInfo.Filter=series.Filter.Copy();

				return groupInfo;

				}
			/// <summary>
			///sort the all chartCells value 
			/// </summary>	
			private void SortAll(GroupInfo rootgroupInfo,ChartGroupInfo rootInfo)
			{
				this.SortResults(rootgroupInfo,AllSeries[rootInfo.index]);

				foreach(GroupResult m_Result in rootgroupInfo.GroupResults)
				{
					foreach(GroupInfo groupInfo in m_Result.SubGroupInfos)
					{
						int subindex=groupInfo.ColumnIndex;

						this.SortResults(groupInfo,this.AllSeries[subindex]);
					}
				}
			}
			
			/// <summary>
			///convert all groupinfo's calculating results into the chartCells value 
			/// </summary>	
			protected virtual void  SetCellValue(GroupInfoCollection groupInfos)
			{
				#region Set Values

				int col = 0,row = 0;

				foreach(GroupInfo groupInfo in groupInfos)
				{
					row = 0;

					foreach(GroupResult result in groupInfo.GroupResults)
					{
						if(row>=this.nRowCount)break;

						IChartCell cell = this.GetCell(row,col);

						cell.DataPoint = result.RowIndicators.Count;

                        cell.Frequence = result.RowIndicators.Count;

						if(result.GroupValue==null||result.GroupValue is System.DBNull)
						{
							cell.Name="[NULL]";
						}
						else
						{
							cell.Name = result.GroupValue.ToString();		
						}

						result.RowIndicators.CopyTo(cell.RowIndicators);  //Added this code at 2008-11-11 8:53:39@Simon	

						if(cell.DataPoint>0)
						{
							float percent=cell.DataPoint/(float)nTotals;

							cell.PercentText=percent.ToString();
						}
						else
						{
							cell.PercentText="0";
						}

						row++;
					}

					col++;
				}
				for(int colIndex = 0; colIndex <this.nColumnCount; colIndex++)
				{
					int SeriesIndex=this.ColsIndex[colIndex];

					Series series = AllSeries[SeriesIndex];

					if(series.FieldShowName)
					{
						for(int rowindex=0;rowindex<this.nRowCount; rowindex++)
						{
							IChartCell cell = this.GetCell(rowindex,colIndex);	
					
                            if(cell.Name=="[NoValue]")continue;

							cell.Name = series.Name;		
						}
					}					
				}
				#endregion
			}
		
		#endregion

		#region SubFunctions for Draw
			#region functions for Percent

				/// <summary>
				///define whether all series have percent setting
				/// </summary>	
				protected bool IsAllPercent()
				{
					foreach(Series series in this.AllSeries)
					{
						if(!series.SeriesLabel.Percent)return false;
					}
					return true;
				}

				/// <summary>
				///define whether all series have none percent setting
				/// </summary>	
				protected bool HaveSomePercent()
				{
					foreach(Series series in this.AllSeries)
					{
						if(series.SeriesLabel.Percent)return true;
					}
					return false;
				}

				/// <summary>
				///define whether the certainseries have percent setting
				/// </summary>	
				/// 
				protected bool IsPercentSeries(int col)
				{			
					return this.SeriesLabel(col).Percent;
				}
			#endregion	

			#region TextSize
					/// <summary>
					///calcualte the text-size that stores the seriesLabel's data
					/// </summary>	
					protected SizeF CalculateTextSize(Graphics g,int realCol)
					{
						float datapoint=0f;

                        bool isPercentSeries = this.IsPercentSeries(realCol);

						for(int i=0;i<this.nRowCount;i++)
						{
							for(int j=0;j<this.ColsIndex.Count;j++)
							{
								if(realCol!=this.ColsIndex[j])continue;

								IChartCell cell=this.GetCell(i,realCol);

								if(datapoint<cell.Frequence)
								{
                                    datapoint = cell.Frequence;							
								}
							}
						}					

						string maxdata=datapoint.ToString();

                        if (isPercentSeries)
						{				
							maxdata="100.0%";

                            #region Percent Format

                            string percentFormat = this.SeriesLabel(realCol).DisplayFormat;

                            if (percentFormat != string.Empty)
                            {
                                System.Text.StringBuilder sb = new System.Text.StringBuilder(percentFormat);

                                sb = sb.Replace("%", maxdata);

                                sb = sb.Replace("#", datapoint.ToString());

                                sb = sb.Replace(@"\n", "\n");

                                sb = sb.Replace("[LineBreak]", "\n");

                                maxdata = sb.ToString();
                            }
                            #endregion		
						}	
					
											
						Font Maxfont=SeriesLabel(realCol).Font;
										
						return g.MeasureString(maxdata,Maxfont);
										
					}

					/// <summary>
					///calcualte the text-size that stores the seriesLabel's data in PieChart
					/// </summary>	
					protected SizeF PieTextSize(Graphics g,int j)
					{	
						int realcol=this.ColsIndex[j];
								
						SizeF size=new SizeF(0f,0f);

						Font SeriesFont=SeriesLabel(realcol).Font;

						for(int i=0;i<this.nRowCount;i++)
						{								
							IChartCell cell=this.GetCell(i,j);

							if(cell.Name=="{NoValue]")continue;

							string strValue=cell.DataPoint.ToString();

							if(this.SeriesLabel(realcol).Percent)
							{							
								strValue = WebbTableCellHelper.FormatValue(this.GetPercentPoint(i,j),FormatTypes.Precent);

                                string percentFormat = this.SeriesLabel(realcol).DisplayFormat;

                                if (percentFormat != string.Empty)
                                {
                                    System.Text.StringBuilder sb = new System.Text.StringBuilder(percentFormat);

                                    sb = sb.Replace("%", strValue);

                                    sb = sb.Replace("#", cell.Frequence.ToString());

                                    strValue = sb.ToString();
                                }
							}					

							if(cell.Name!=string.Empty)
							{   			
								strValue =cell.Name + "\r\n" +strValue;
							}
							SizeF szfCell=g.MeasureString(strValue,SeriesFont);

							if(size.Width<szfCell.Width)size.Width=szfCell.Width;

							if(size.Height<szfCell.Height)size.Height=szfCell.Height;
							
						}		
											
						return size;
															
					}
				    

					/// <summary>
					///checked whether all the columns in chart cell fits the total series count
					/// </summary>
					protected bool CheckedCols()
					{				
						if(ColsIndex.Count!=this.nColumnCount)return false;	
				
						foreach(int col in ColsIndex)
						{
							if(col>=AllSeries.Count||col<0)return false;
						}
						for(int i=0;i<this.AllSeries.Count;i++)
						{
							if(!ColsIndex.Contains(i))return false;
						}

						return true;
					}
				#endregion
		#endregion
		/// <summary>
		/// draw all
		/// </summary>
		/// <param name="g"></param>
		/// <param name="rect"></param>
		virtual public void Draw(Graphics g, Rectangle rect)
		{	
			bool isEmpty=IsEmptyChart();

			if(isEmpty)return;

			#region Init ClickArgs

			    this.ClickAreas.Clear();
			  
				for(int i=0;i<nRowCount*nColumnCount;i++)
				{
					this.ClickAreas.Add(RectangleF.Empty);
				}

			#endregion

			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
		
			Color backColor=this.setting.TransparentBackground;

			if(backColor!=Color.Empty||backColor!=Color.Transparent)
			{
				Brush backBrushes=new SolidBrush(backColor);

				g.FillRectangle(backBrushes,rect);

				backBrushes.Dispose();
			}
			

			if(!this.setting.ShowAxesMode)
			{
				this.setting.AxisX.Gridlines=GridLineStyle.None;

				this.setting.AxisX.Tickmarks=GridLineStyle.None;

				this.setting.AxisY.Gridlines=GridLineStyle.None;

				this.setting.AxisY.Tickmarks=GridLineStyle.None;

				if(this.setting.ChartType!=ChartAppearanceType.Pie&&this.setting.ChartType!=ChartAppearanceType.Pie3D)
				{
					this.setting.Lengend.Visible=false;
				}

				if(this.Setting.ChartType==ChartAppearanceType.HorizonBar)
				{					
					this.setting.AxisX.Visible=false;   
				}
				else
				{
					this.setting.AxisY.Visible=false;   
				}
			}
		}

		/// <summary>
		/// draw chart
		/// </summary>
		/// <param name="g"></param>
		/// <param name="rect"></param>
		virtual public void DrawChart(Graphics g , Rectangle rect)
		{
			
		}

		/// <summary>
		/// Get the maxpoint that should be passed to RepeatControl When printing
		/// </summary>
		virtual public float GetExMaxPoint()
		{
			return this.GetMaxDataPoint();
		}

		/// <summary>
		/// draw lengend
		/// </summary>
		/// <param name="g"></param>
		/// <param name="rect"></param>
		/// <returns>width of lengend</returns>
		virtual public float DrawLengend(Graphics g, Rectangle rect)
		{
			return 0.0f;
		}
		
		/// <summary>
		/// create click area for chart
		/// </summary>
		/// <param name="areaName"></param>
		/// <param name="graph"></param>
		/// <param name="setting"></param>
		virtual public void CreatePrintingTable(Graphics g, Rectangle rect)
		{
			
		}

		/// <summary>
		/// copy chart
		/// </summary>
		/// <returns></returns>
		virtual public ChartBase Copy()
		{
			ChartBase chartBase = new ChartBase();

			for(int i = 0; i < this.nRowCount; i++)
			{
				for(int j = 0; j < this.nColumnCount; j++)
				{
					chartBase.table[i,j] = this.table[i,j].Copy();
				}
			}

			chartBase.ClickAreas=this.ClickAreas.Clone() as ArrayList;//Added this code at 2008-11-11 9:23:59@Simon
			
			chartBase.nRowCount = this.nRowCount;

			chartBase.nColumnCount = this.nColumnCount;

			return chartBase;
		}

		/// <summary>
		/// Get TotalDataPoint at a certain column
		/// </summary>
		public float GetTotalDataPoint(int nCol)
		{
			float totalDataPoint = 0.0f;

			if(nCol < 0 || nCol > this.nColumnCount) return totalDataPoint;

			for(int i = 0; i < this.nRowCount; i++)
			{
				totalDataPoint += this.GetCell(i,nCol).DataPoint;
			}

			return totalDataPoint;
		}
		/// <summary>
		/// Get Max-DataPoint from all series in chart
		/// </summary>
		public float GetMaxDataPoint()
		{
			float maxDataPoint = 0.0f;

			for(int i = 0; i < this.nRowCount; i++)
			{
				for(int j = 0; j < this.nColumnCount; j++)
				{
                    float datapoint=this.GetCell(i,j).DataPoint;
                    
					if(datapoint<0)return -1;

					maxDataPoint = Math.Max(maxDataPoint,datapoint);
				}
			}
			return maxDataPoint;
		}

		//Scott@12092008
		/// <summary>
		///calcualte  percent about each Chartcell represents
		/// </summary>
		public float GetPercentPoint(int nRow,int nCol)
		{
			float dataPoint = 0.0f;

			IChartCell cell = this.GetCell(nRow,nCol);

			if(cell == null) return dataPoint;

			dataPoint = cell.DataPoint;

			float totalDataPoint = this.GetTotalDataPoint(nCol);
			
			if(totalDataPoint > 0.01f)
			{
				dataPoint = dataPoint/totalDataPoint;
			}
			return dataPoint;
		}
        
		/// <summary>
		///fill random data for test and Non-datasource Mode
		/// </summary>
		public void FillRandomData()
		{//for test
			this.InitTable(5,this.AllSeries.Count);

			Random rnd=new Random(unchecked((int)DateTime.Now.Ticks));

			nTotals=110;

			for(int i=0;i<this.nRowCount;i++)
			{
				for(int j=0;j<this.nColumnCount;j++)
				{
					float fValue = (float)Math.Round(rnd.NextDouble()*100,1);

					this.table[i,j].DataPoint = fValue;

                    this.table[i, j].Frequence = fValue;

					this.table[i,j].Name = string.Format("{0}{1}",(char)(i+65),j+1);

					
				}
			}
			for(int j=0;j<this.nColumnCount;j++)
			{	
				Series series=this.AllSeries[j];

				this.ColsIndex.Add(j);

				#region SortValues at 2008-12-15 15:59:01@Simon				  
				for(int m=0;m<this.nRowCount-1;m++)           
				{
					float tempValue=this.table[m,j].DataPoint;

					for(int n=m+1;n<this.nRowCount;n++)
					{
						if(series.SeriesLabel.SortingType==Webb.Data.SortingTypes.Ascending)
						{
							if(tempValue>this.table[n,j].DataPoint)
							{
								this.table[m,j].DataPoint=this.table[n,j].DataPoint;

                                 this.table[n,j].DataPoint=tempValue;

                                 this.table[m, j].Frequence = this.table[m, j].DataPoint;

                                 this.table[n, j].Frequence = this.table[n, j].DataPoint;

							}
						}
						else if(series.SeriesLabel.SortingType==Webb.Data.SortingTypes.Descending)
						{
							if(tempValue<this.table[n,j].DataPoint)
							{
								this.table[m,j].DataPoint=this.table[n,j].DataPoint;

								this.table[n,j].DataPoint=tempValue;

                                this.table[m, j].Frequence = this.table[m, j].DataPoint;

                                this.table[n, j].Frequence = this.table[n, j].DataPoint;
							}
						}
					}
				}
				#endregion        //End Modify

				for(int i=0;i<this.nRowCount;i++)
				{					
				  this.table[i,j].PercentText=this.GetPercentPoint(i,j).ToString();	
				}
			}



		}		
	}
	#endregion

	#region public class PieChart : ChartBase
	[Serializable]
	public class PieChart : ChartBase
	{	    
		public PieChart() : base(){}

		/// <summary>
		/// calculate table by setting
		/// </summary>
		public override void CalculateTable(DataTable table, Collections.Int32Collection rows)
		{
			base.CalculateTable(table,rows);

			this.ExcludeDataPoints();
		}
		/// <summary>
		/// Exclude the series that are all emapty 
		/// </summary>
		public void ExcludeDataPoints()
		{
			int AllCols=this.nColumnCount;

            float total=0f;

			for(int j=0;j<this.nColumnCount;j++)
			{
				 total=this.GetTotalDataPoint(j);

				if(total==0f)AllCols--;
			}

			if(AllCols>0)
			{			

				IChartCell[,] newtable=new ChartCell[nRowCount,AllCols];
                
				int colIndex=0;				
 
				for(int j=0;j<this.nColumnCount;j++)
				{
					total=this.GetTotalDataPoint(j);

					if(total==0f)
					{ 
						this.ColsIndex.RemoveAt(j);

						continue;
					}

					for(int i=0;i<this.nRowCount;i++)
					{
                      newtable[i,colIndex]=table[i,j];
					}

					colIndex++;
				}
				table=newtable;

                this.nColumnCount=AllCols;				

			}

		}

		/// <summary>
		/// draw pie
		/// </summary>
		/// <param name="g"></param>
		/// <param name="rect"></param>
		public override void Draw(Graphics g, Rectangle rect)
		{
			base.Draw(g,rect);

			int boundspace=this.setting.BoundSpace;

			if(boundspace>0)
			{
				rect.Inflate(-boundspace,-boundspace);
			}

			if(this.setting.Lengend.Visible) 
			{
				float fLengendWidth = this.DrawLengend(g,rect);	// draw lengend

				rect.Width = rect.Width - (int)fLengendWidth - 5;	// get pie area
			}
			
			this.DrawChart(g,rect);
		}

		/// <summary>
		/// Draw PieLabel in a certain area
		/// </summary>  
		public virtual Rectangle DrawPieLabel(Graphics g, Rectangle rect,float offset)  //2009-6-8 10:27:51@Simon Add this Code
		{
			string text=this.setting.PieLabelInfo.Text;
            
			text=text.Replace("[FieldName]",this.AllSeries[0].FieldArgument);

			if(this.setting.PieLabelInfo==null||!this.setting.PieLabelInfo.Show||text==null||text==string.Empty)
			{
				return rect;
			}			

			Font labelFont=this.setting.PieLabelInfo.Font;		

			Brush textBrush=new SolidBrush(this.setting.PieLabelInfo.ForeColor);			

			StringFormat format=new StringFormat();

            format.LineAlignment=StringAlignment.Center;

			format.Alignment=StringAlignment.Center;

			SizeF textSize=g.MeasureString(text,labelFont,rect.Width,format);

			Rectangle textRect=new Rectangle();

			Rectangle RectForPie=new Rectangle();

		    textRect.X=rect.X;

			RectForPie.X=rect.X;

			if(this.setting.PieLabelInfo.Position==LabelPosition.Down)
			{
				textRect.Y=rect.Y+rect.Height-((int)textSize.Height+4)-(int)(offset*rect.Height);
              
                 RectForPie.Y=rect.Y;
			}
			else
			{
			    textRect.Y=rect.Y+(int)(offset*rect.Height);;

                RectForPie.Y=rect.Y+((int)textSize.Height+4)+(int)(offset*rect.Height/2);

			}

			textRect.Width=rect.Width;

			RectForPie.Width=rect.Width;

			textRect.Height=((int)textSize.Height+4);

			RectForPie.Height=rect.Height-((int)textSize.Height+4); 
  
			TextRenderingHint trHint=g.TextRenderingHint;
        
			g.TextRenderingHint=this.setting.PieLabelInfo.FontQuality;
		
			g.DrawString(text,labelFont,textBrush,textRect,format);

			g.TextRenderingHint=trHint;

			return RectForPie;

		}


		//Modified at 2008-12-23 13:40:35@Scott
		/// <summary>
		///  Draw Shadow for pitChart 
		/// </summary>  
		public void DrawShadow(Graphics g, RectangleF rectfPie)
		{
			RectangleF rectfShadow = new RectangleF(rectfPie.Location,rectfPie.Size);

			rectfShadow.Offset(5,5);

			g.FillEllipse(Brushes.LightGray,rectfShadow);
		}

		/// <summary>
		/// draw chart
		/// </summary>
		/// <param name="g"></param>
		/// <param name="rect"></param>
		/// <param name="setting"></param>
		public override void DrawChart(Graphics g, Rectangle rect)
		{
			rect=DrawPieLabel(g, rect,0f);

			for(int j = 0; j < this.nColumnCount; j++)
			{
				float startAngle = 0.0f, sweepAngle = 0.0f;	// start angle and sweep angle for each pie

				float totalDataPoint = this.GetTotalDataPoint(j);	// get total data point of each row in the table

				int realCol=this.ColsIndex[j];				

				SizeF szfText=this.PieTextSize(g,j);

				RectangleF rectfPie = this.GetPieRectF(rect,j,szfText);	// get area of each row

				if(rectfPie==RectangleF.Empty)continue;

//				this.DrawShadow(g,rectfPie);	//Modified at 2008-12-23 13:40:35@Scott

				for(int i = 0; i < this.nRowCount; i++)	// draw pies
				{
					IChartCell cell = this.GetCell(i,j);

					sweepAngle = cell.DataPoint / totalDataPoint * 360;
					
					if(cell.Name=="[NoValue]"||cell.DataPoint<1e-9)continue;					

					Color color = ColorManager.DiffColor(i);	

					Color lightColor = ColorManager.GetLightColor(color);

					float LinearGradientangle=0f;

					#region Modified Area

					if(!HaveRoot())
					{
						if(ColorManager.ValueToPieColor.Contains(cell.Name))
						{							
							PiediffColor diffcolor=(PiediffColor)ColorManager.ValueToPieColor[cell.Name];

							color = diffcolor.pieColor;

							lightColor = diffcolor.lightColor;
 
							LinearGradientangle=diffcolor.angle;

						}
					}
					#endregion        //Modify at 2008-12-23 11:34:37@Scott

					#region Modify codes at 2009-4-17 9:18:16@Simon
						ValueColor valueColor=this.ValueColor(realCol,cell.Name);

						if(valueColor!=null)
						{
							if(valueColor.ChartColor!=Color.Empty)
							{
								color=lightColor=valueColor.ChartColor;

								if(valueColor.GradientColor!=Color.Empty)
								{
									lightColor = valueColor.GradientColor;
								}
							}

                            LinearGradientangle=valueColor.GradientAngle;
						}
					#endregion        //End Modify

					#region Modified Area

						if(!HaveRoot())
						{
							if(!ColorManager.ValueToPieColor.Contains(cell.Name))
							{
								ColorManager.ValueToPieColor.Add(cell.Name,new PiediffColor(color,lightColor,LinearGradientangle));
							}						
						}
					#endregion        //Modify at 2008-12-23 11:34:37@Scott

					Brush brushPie = new LinearGradientBrush(rectfPie,lightColor,color,LinearGradientangle);	//Linear gradient brush					

					HatchStyle style = (i%2 == 0) ? HatchStyle.BackwardDiagonal : HatchStyle.LightHorizontal;	//12-16-2008@Scott

					HatchBrush hatchBrush = new HatchBrush(style,Color.LightGray,Color.Transparent);	//12-16-2008@Scott

					g.FillPie(brushPie,rectfPie.X,rectfPie.Y,rectfPie.Width,rectfPie.Height,startAngle,sweepAngle);

					g.FillPie(hatchBrush,rectfPie.X,rectfPie.Y,rectfPie.Width,rectfPie.Height,startAngle,sweepAngle);	//12-16-2008@Scott

					startAngle += sweepAngle;
				}
			
				startAngle = 0;	// initialize start angle

				for(int i = 0; i < this.nRowCount; i++)
				{
					IChartCell cell = this.GetCell(i,j);	// get each cell				

					sweepAngle = cell.DataPoint / totalDataPoint * 360;		// calculate sweep angle
		
					this.DrawHorzPieText(g,rectfPie,startAngle,sweepAngle,cell,i,j);	// draw horizontal text
					//					}
					startAngle += sweepAngle;	// calculate start angle
				}
			}
		}

		/// <summary>
		/// Calcuate the Legend Size
		/// </summary>    
		protected SizeF LegendSize(Graphics g)
		{
			SizeF szfMaxText =new SizeF(0,0);

			for(int i = 0; i < this.nRowCount; i++)
			{
				for(int j = 0; j < this.nColumnCount; j++)
				{
					IChartCell cell=this.GetCell(i,j);

					if(cell.Name=="[NoValue]")continue;

					string strValue=this.CellDescrption(cell);

					SizeF szfText=g.MeasureString(strValue,Webb.Utility.GlobalFont);

					if(szfMaxText.Width<szfText.Width)szfMaxText.Width=szfText.Width;

                    if(szfMaxText.Height<szfText.Height)szfMaxText.Height=szfText.Height;

				}
			}
			return szfMaxText;
			
		}

		/// <summary>
		/// get the string in a certain format about each cell in Legend
		/// </summary>    
		private string CellDescrption(IChartCell cell)
		{
			string strValue=this.setting.Lengend.PieTextFormat.Trim();

			if(strValue==string.Empty)
			{
				strValue="[Value]";
			}
			
		    strValue=strValue.Replace("[Value]",cell.Name);

			strValue=strValue.Replace("[Count]",cell.DataPoint.ToString());

			return strValue;

		}
		/// <summary>
		/// draw lengend
		/// </summary>
		/// <param name="g"></param>
		/// <param name="rect"></param>
		/// <returns></returns>
		public override float DrawLengend(Graphics g, Rectangle rect)
		{
			SizeF szfText =this.LegendSize(g);	// get size of text

			szfText=new SizeF(szfText.Width+2,szfText.Height+2);

			float nWidth = setting.Lengend.SizeMarker.Width + szfText.Width + setting.Lengend.SizeSpacing.Width * 3;	// get width of lengend

			float nHeight = Math.Max(szfText.Height,setting.Lengend.SizeMarker.Height) + setting.Lengend.SizeSpacing.Height * 2;	// get height of lengend

			if((!this.Setting.Lengend.VisibleMarker)||Setting.Lengend.SizeMarker.Height*Setting.Lengend.SizeMarker.Width==0)
			{
				nWidth = szfText.Width + setting.Lengend.SizeSpacing.Width * 2;
			}
			
			RectangleF rcLengend = new RectangleF(rect.Right - nWidth, rect.Top, nWidth, nHeight);	// get area of each lengend

			Brush brush = new SolidBrush(ColorManager.BackColor);

			Pen pen = new Pen(ColorManager.BorderColor);

			for(int j = 0; j < this.nColumnCount; j++)
			{
				int realCol=this.ColsIndex[j];

				for(int i = 0; i < this.nRowCount; i++)
				{
					IChartCell cell = this.GetCell(i,j);

					if(cell.Name=="[NoValue]"||cell.DataPoint<1e-9)continue;

					g.FillRectangle(brush,rcLengend);

					string strValue=this.CellDescrption(cell);

					if(this.Setting.Lengend.VisibleMarker)
					{
						RectangleF rcfMarker = new RectangleF(rcLengend.Left + setting.Lengend.SizeSpacing.Width - setting.Lengend.SizeMarker.Width,rcLengend.Top + setting.Lengend.SizeSpacing.Height,setting.Lengend.SizeMarker.Width * 2, setting.Lengend.SizeMarker.Height * 2);

						RectangleF rcfLinearGradient = new RectangleF(rcfMarker.Left + rcfMarker.Width/2, rcfMarker.Top, rcfMarker.Width/2, rcfMarker.Height/2);
										
						Color color = ColorManager.DiffColor(i);
						
						Color lightColor = ColorManager.GetLightColor(color);

						float LinearGradientangle=0f;

						#region Modified Area
						if(!HaveRoot())
						{
							if(ColorManager.ValueToPieColor.Contains(cell.Name))
							{							
								PiediffColor diffcolor=(PiediffColor)ColorManager.ValueToPieColor[cell.Name];

								color = diffcolor.pieColor;

								lightColor = diffcolor.lightColor;
 
								LinearGradientangle=diffcolor.angle;

							}
						}
						#endregion        //Modify at 2008-12-23 11:34:37@Scott

						#region Modify codes at 2009-4-17 9:18:16@Simon
						ValueColor valueColor=this.ValueColor(realCol,cell.Name);

						if(valueColor!=null)
						{
							if(valueColor.ChartColor!=Color.Empty)
							{
								color=lightColor=valueColor.ChartColor;

								if(valueColor.GradientColor!=Color.Empty)
								{
									lightColor = valueColor.GradientColor;
								}
							}
							LinearGradientangle=valueColor.GradientAngle;
						}
						#endregion        //End Modify

						#region Modified Area
						if(!HaveRoot())
						{
							if(!ColorManager.ValueToPieColor.Contains(cell.Name))
							{
								ColorManager.ValueToPieColor.Add(cell.Name,new PiediffColor(color,lightColor,LinearGradientangle));
							}						
						}
						#endregion        //Modify at 2008-12-23 11:34:37@Scott


						g.FillPie(new LinearGradientBrush(rcfLinearGradient,lightColor,color,LinearGradientangle),rcfMarker.X,rcfMarker.Y,rcfMarker.Width,rcfMarker.Height,270,90);
					
						g.DrawString(strValue,Webb.Utility.GlobalFont,Brushes.Black,new RectangleF(rcLengend.Left + setting.Lengend.SizeSpacing.Width * 2 + setting.Lengend.SizeMarker.Width, rcLengend.Top, rcLengend.Width - setting.Lengend.SizeSpacing.Width * 2 - setting.Lengend.SizeMarker.Width, rcLengend.Height),Lengend.StringFormat);
					}
					else
					{
						g.DrawString(strValue,Webb.Utility.GlobalFont,Brushes.Black,rcLengend,Lengend.StringFormat);
					}					

					rcLengend.Offset(0,rcLengend.Height - setting.Lengend.SizeSpacing.Height);
				}
			}

			g.DrawRectangle(pen,rcLengend.Left,rect.Top,nWidth,rcLengend.Bottom - rcLengend.Height + setting.Lengend.SizeSpacing.Height - rect.Top);
			
			return nWidth;
		}
		#region Original DrawLegend
//		public override float DrawLengend(Graphics g, Rectangle rect)
//		{
//			float maxDataPoint = this.GetMaxDataPoint();	// get max data point
//
//			SizeF szfText = g.MeasureString(maxDataPoint.ToString(),Webb.Utility.GlobalFont);	// get size of text
//
//			float nWidth = setting.Lengend.SizeMarker.Width + szfText.Width + setting.Lengend.SizeSpacing.Width * 3;	// get width of lengend
//
//			float nHeight = Math.Max(szfText.Height,setting.Lengend.SizeMarker.Height) + setting.Lengend.SizeSpacing.Height * 2;	// get height of lengend
//
//			RectangleF rcLengend = new RectangleF(rect.Right - nWidth, rect.Top, nWidth, nHeight);	// get area of each lengend
//
//			Brush brush = new SolidBrush(ColorManager.BackColor);
//
//			Pen pen = new Pen(ColorManager.BorderColor);
//
//			for(int j = 0; j < this.nColumnCount; j++)
//			{
//				int realCol=this.ColsIndex[j];
//
//				for(int i = 0; i < this.nRowCount; i++)
//				{
//					IChartCell cell = this.GetCell(i,j);
//
//					if(cell.Name=="[NoValue]"||cell.DataPoint<1e-9)continue;
//
//					g.FillRectangle(brush,rcLengend);
//
//					RectangleF rcfMarker = new RectangleF(rcLengend.Left + setting.Lengend.SizeSpacing.Width - setting.Lengend.SizeMarker.Width,rcLengend.Top + setting.Lengend.SizeSpacing.Height,setting.Lengend.SizeMarker.Width * 2, setting.Lengend.SizeMarker.Height * 2);
//
//					RectangleF rcfLinearGradient = new RectangleF(rcfMarker.Left + rcfMarker.Width/2, rcfMarker.Top, rcfMarker.Width/2, rcfMarker.Height/2);
//
//					Color color = ColorManager.DiffColor(i);
//
//					Color lightColor = ColorManager.GetLightColor(color);
//
//					g.FillPie(new LinearGradientBrush(rcfLinearGradient,lightColor,color,0f),rcfMarker.X,rcfMarker.Y,rcfMarker.Width,rcfMarker.Height,270,90);
//
//					g.DrawString(cell.DataPoint.ToString(),Webb.Utility.GlobalFont,Brushes.Black,new RectangleF(rcLengend.Left + setting.Lengend.SizeSpacing.Width * 2 + setting.Lengend.SizeMarker.Width, rcLengend.Top, rcLengend.Width - setting.Lengend.SizeSpacing.Width * 2 - setting.Lengend.SizeMarker.Width, rcLengend.Height),Lengend.StringFormat);
//
//					rcLengend.Offset(0,rcLengend.Height - setting.Lengend.SizeSpacing.Height);
//				}
//			}
//
//			g.DrawRectangle(pen,rcLengend.Left,rect.Top,nWidth,rcLengend.Bottom - rcLengend.Height + setting.Lengend.SizeSpacing.Height - rect.Top);
//			
//			return nWidth;
//		}
		#endregion

		/// <summary>
		/// Draw PieText in a certain location
		/// </summary>    
		private  void DrawHorzPieText(Graphics g, RectangleF rectPie, float startAngle, float sweepAngle, IChartCell cell, int row, int col)
		{
			if(cell.DataPoint<1e-9||cell.Name=="[NoValue]")return;   //Added this code at 2008-12-11 10:09:10@Simon

			string strValue = cell.DataPoint.ToString();

			float fSpace = 1.0f;

			float angle = startAngle + sweepAngle/2;

			float radius = 0;

			int realcol=this.ColsIndex[col];

			if(!SeriesLabel(realcol).Visible)return;   //Added this code at 2008-12-11 10:09:10@Simon
				
			int nColorIndex=row;			

			Color color = ColorManager.DiffColor(nColorIndex);

			int LengthConnector=this.SeriesLabel(realcol).LengthConnector;

			#region Modified Area
			if(!HaveRoot())
			{
				if(ColorManager.ValueToPieColor.Contains(cell.Name))
				{							
					PiediffColor diffcolor=(PiediffColor)ColorManager.ValueToPieColor[cell.Name];

					color = diffcolor.pieColor;					

				}
			}
			#endregion        //Modify at 2008-12-23 11:34:37@Scott

			#region Modify codes at 2009-4-17 9:18:16@Simon
			ValueColor valueColor=this.ValueColor(realcol,cell.Name);

			if(valueColor!=null)
			{
				if(valueColor.ChartColor!=Color.Empty)
				{
					color=valueColor.ChartColor;
				}
				if(valueColor.LengthConnector>0)
				{
					LengthConnector=valueColor.LengthConnector;
				}

			}

			#endregion        //End Modify   	

			switch(this.SeriesLabel(realcol).Position)
			{
				case ChartTextPosition.Outside:
					radius = rectPie.Width/2 + this.SeriesLabel(realcol).LengthConnector;
					break;
				case ChartTextPosition.Inside:
                    radius =this.SeriesLabel(realcol).LengthConnector;  //2009-4-24 10:12:10@Simon Add this Code
					break;  
				default:
					radius = rectPie.Width/4;
					break;
				
			}

			if(this.SeriesLabel(realcol).Percent)
			{
				strValue = WebbTableCellHelper.FormatValue(this.GetPercentPoint(row,col),FormatTypes.Precent);

                #region Percent Format

                string percentFormat = this.SeriesLabel(realcol).DisplayFormat;

                if (percentFormat != string.Empty)
                {
                    System.Text.StringBuilder sb=new System.Text.StringBuilder(percentFormat);

                    sb = sb.Replace("%", strValue);

                    sb=sb.Replace("#",cell.DataPoint.ToString());

                    sb=sb.Replace(@"\n","\n");

                    sb = sb.Replace("[LineBreak]", "\n");

                    strValue=sb.ToString();
                }
                #endregion

            }

			if(cell.Name != string.Empty)
			{
				strValue = cell.Name + "\r\n" + strValue;
			}

			PointF ptfCenter = this.GetCircleCenterPoint(rectPie);	// get circle center

			float x = (float)(ptfCenter.X + Math.Cos(angle*Math.PI/180)*radius);	// get x pos of arc center

			float y = (float)(ptfCenter.Y + Math.Sin(angle*Math.PI/180)*radius);	// get y pos of arc center

			float xArc = (float)(ptfCenter.X + Math.Cos(angle*Math.PI/180)*rectPie.Width/2);
			
			float yArc = (float)(ptfCenter.Y + Math.Sin(angle*Math.PI/180)*rectPie.Width/2); 

			Font seriesFont=this.SeriesLabel(realcol).Font;
           
	        SizeF szText = g.MeasureString(strValue,seriesFont);	// get size of text
	
			#region Hide OrignalText
//			cell.Font=this.SeriesLabel(realcol).Font;
//
//			SizeF szText = g.MeasureString(strValue,cell.Font);	// get size of text

//			switch(this.SeriesLabel(realcol).Position)    //Added this code at 2008-12-31 10:31:53@Simon
//			{ 
//				case ChartTextPosition.Outside:
//					if(this.SeriesLabel(realcol).VisibleConnector) 
//					{				
//						g.DrawLine(pen,new PointF(xArc,yArc),new PointF(x,y));	// draw connector
//					}           
//					// adjust x,y position
//					if(angle >= 90 && angle < 270)
//					{
//						x -= szText.Width;
//					}
//					if(angle >= 180 && angle < 360)
//					{
//						y -= szText.Height;
//					}
//					break;
//				default:
//                    x-= szText.Width/2;
//					y-=szText.Height/2;
//					break; 
//			}
//			if(realcol == setting.SelectedSeriesIndex)	// highlight selected series
//			{
//				//brush = Brushes.Red;
//				pen = Pens.Red;
//			}      
//			
//			g.FillRectangle(Brushes.White,rectF);	// fill background color
//            
//			g.DrawRectangle(pen,rectF.X,rectF.Y,rectF.Width,rectF.Height);	// draw rectangle	
			#endregion

			#region Added this code at 2009-1-15 16:38:15@Simon
			PointF point=new PointF(x,y);

			PointF pointArc=new PointF(xArc,yArc);

			ChartTextPosition position=SeriesLabel(realcol).Position;
			
			if(position==ChartTextPosition.Outside && SeriesLabel(realcol).VisibleConnector) 
			{				
				g.DrawLine(new Pen(color),pointArc,point);	// draw connector
			}     

            RectangleF rectF=ChartHelper.GetRect(angle,point,szText,position);		
	
            rectF.Inflate(fSpace,fSpace);

			Color backColor=this.SeriesLabel(realcol).BackColor;

			if(backColor==Color.Empty)
			{
				g.FillRectangle(Brushes.White,rectF);	// fill background color
			}
			else
			{
				g.FillRectangle(new SolidBrush(backColor),rectF);	// fill background color
			}
            
			if(realcol == setting.SelectedSeriesIndex)	// highlight selected series
			{
				g.DrawRectangle(Pens.Red,rectF.X,rectF.Y,rectF.Width,rectF.Height);	// draw rectangle
			} 
			else
			{	
				if(this.SeriesLabel(realcol).ShowBorders)
				{
                    Color BorderColor=color;

					Color SeriesLabelborderColor=this.SeriesLabel(realcol).RectBorderColor;

					if(SeriesLabelborderColor!=Color.Empty)
					{
						BorderColor=SeriesLabelborderColor;
					}                   
                    
					if(valueColor!=null&&valueColor.RectBorderColor!=Color.Empty)
					{
						BorderColor=valueColor.RectBorderColor;
					}

                    g.DrawRectangle(new Pen(BorderColor),rectF.X,rectF.Y,rectF.Width,rectF.Height);	// draw rectangle

				}
			}

			#endregion			
		
			StringFormat sf=new StringFormat();

			sf.Alignment=StringAlignment.Center;

            sf.LineAlignment=StringAlignment.Center;	
		
			Color SeriesTextColor=this.SeriesLabel(realcol).TextColor;

			if(SeriesTextColor!=Color.Empty)
			{
				color=SeriesTextColor;
			}	
                    
			if(valueColor!=null&&valueColor.TextColor!=Color.Empty)
			{
				color=valueColor.TextColor;
			}  
         
			g.DrawString(strValue,seriesFont,new SolidBrush(color),rectF,sf);	// draw string

			int index=row*this.nColumnCount+col;
			
			this.ClickAreas[index]=rectF;  //Added this code at 2008-11-11 14:45:37@Simon
			
		}	
	

		
		#region Hide Original "GetPieRectF" Codes at 01-16-2009@simon
//		public RectangleF GetPieRectF(Rectangle rect,int nCol)
//		{
//			int realCol=this.ColsIndex[nCol];
//
//			RectangleF rectf = new RectangleF(rect.X,rect.Y,rect.Width,rect.Height);
//
//			if(nCol < 0 || nCol > this.nColumnCount) return RectangleF.Empty;
//
//			if(this.nColumnCount > 1)
//			{				
//				rectf.Width = rectf.Width / nColumnCount;	//set width of this pie
//
//				rectf.X += rectf.Width * nCol;	//set this pie's start position
//			}
//
//			if(rectf.Width > rectf.Height)	rectf.Inflate((rectf.Height - rectf.Width)/2,0);	//get square
//
//			if(rectf.Width < rectf.Height)	rectf.Inflate(0,(rectf.Width - rectf.Height)/2);	//get square
//
//			
//			
//			if(this.SeriesLabel(realCol).Position == ChartTextPosition.Outside)
//			{
//				rectf.Inflate(-rectf.Width/5,-rectf.Width/5);	// save space of text
//			}
//
//			return rectf;
//		}

		#endregion

		#region New Codes
			/// <summary>
			/// Get the location and size for the ChartArea
			/// </summary>    
			public RectangleF GetPieRectF(Rectangle rect,int nCol,SizeF szfText)
			{
				int realCol=this.ColsIndex[nCol];

				RectangleF rectf = new RectangleF(rect.X,rect.Y,rect.Width,rect.Height);

				int nRealDiffCols=this.nColumnCount;		

				if(nCol < 0 || nCol > nRealDiffCols||nRealDiffCols<=0) return RectangleF.Empty;			

				if(nRealDiffCols<=4)
				{
					rectf.Width = rectf.Width / nRealDiffCols;	//set width of this pie

					rectf.X += rectf.Width * nCol;	//set this pie's start position
				}
				else
				{
					int nRow=0;

					if(nRealDiffCols%4==0)
					{
				    	nRow=nRealDiffCols/4;
					}
					else
					{
					   nRow=nRealDiffCols/4+1;
					}

					rectf.Width = rectf.Width / 4;	//set width of this pie

					rectf.Height= rectf.Height / nRow;	//set width of this pie

					int colIndex=nCol/4;

					int rowIndex=nCol%4;

					rectf.X += rectf.Width * rowIndex;	//set this pie's start position

					rectf.Y += rectf.Height*colIndex;
				}

				
				if(this.SeriesLabel(realCol).Position == ChartTextPosition.Outside)
				{
					int len=this.SeriesLabel(realCol).LengthConnector;
					
					szfText.Height+=len;

					szfText.Width+=len;

					rectf.Inflate(-szfText.Width,-szfText.Height);				
				}

				if(rectf.Width > rectf.Height)	rectf.Inflate((rectf.Height - rectf.Width)/2,0);	//get square

				if(rectf.Width < rectf.Height)	rectf.Inflate(0,(rectf.Width - rectf.Height)/2);	//get square

				rectf.Inflate(-1,-1);

				if(rectf.Width<=0||rectf.Height<=0)return RectangleF.Empty;

				if(rectf.Right>rect.Right||rectf.Y+rectf.Height>rect.Y+rect.Height)return RectangleF.Empty;		
				
				return rectf;
			}


		#endregion

	     
		/// <summary>
		///get the center location point of a rectange
		/// </summary>
		public PointF GetCircleCenterPoint(RectangleF rect)
		{
			return new PointF(rect.Left + rect.Width/2, rect.Top + rect.Height/2);
		}
	}
	#endregion

	//Added this code at 2008-12-29 11:22:14@Simon
	#region public class Pie3dChart:PieChart
	public class Pie3dChart:PieChart
	{
		/// <summary>
		///override DrawChart from PieChart,to draw pie3d chart in a certain rectange
		/// </summary>
		public override void DrawChart(Graphics g, Rectangle rect)
		{
			if(this.SeriesLabel(0).Position==ChartTextPosition.Outside)
			{
				rect=DrawPieLabel(g, rect,1/15f);
			}
			else
			{
                rect=DrawPieLabel(g, rect,0f);

			}		    

			for(int j = 0; j < this.nColumnCount; j++)
			{
				float startAngle = 0.0f, sweepAngle = 0.0f;	// start angle and sweep angle for each pie

				float totalDataPoint = this.GetTotalDataPoint(j);	// get total data point of each row in the table

				int realCol=this.ColsIndex[j];	
				
				SizeF szfText=PieTextSize(g,j);

				RectangleF rectfPie = this.GetPieRectF(rect,j,szfText);	// get area of each row		
		
				if(rectfPie==RectangleF.Empty)continue;

				if(this.SeriesLabel(realCol).Position == ChartTextPosition.Outside)
				{
					int len=this.SeriesLabel(realCol).LengthConnector;

				    rectfPie.Inflate(-len,-len);
				}

                if(rectfPie.Width<=0||rectfPie.Height<=0)continue;
           
				rectfPie.Y+=rectfPie.Height/30;

				rectfPie.Height=rectfPie.Height*4/5;
				
				ArrayList DrawColors=new ArrayList();	
			
				ArrayList SweepAngles=new ArrayList();            

				for(int i = 0; i < this.nRowCount; i++)	// draw pies
				{
					#region draw pie
					
						IChartCell cell = this.GetCell(i,j);
						
						if(cell.Name=="[NoValue]"||cell.DataPoint<1e-9)continue;

						sweepAngle = cell.DataPoint / totalDataPoint * 360;					

						int nColorIndex = i;

						Color color = ColorManager.DiffColor(nColorIndex);

						Color lightColor = ColorManager.GetLightColor(color);

						float LinearGradientangle=0f;

						#region Modified Area

							if(!HaveRoot())
							{
								if(ColorManager.ValueToPieColor.Contains(cell.Name))
								{							
									PiediffColor diffcolor=(PiediffColor)ColorManager.ValueToPieColor[cell.Name];

									color = diffcolor.pieColor;

									lightColor = diffcolor.lightColor;
		 
									LinearGradientangle=diffcolor.angle;

								}
							}

						#endregion        //Modify at 2008-12-23 11:34:37@Scott

						#region Modify codes at 2009-4-17 9:18:16@Simon

							ValueColor valueColor=this.ValueColor(realCol,cell.Name);

							if(valueColor!=null)
							{
								if(valueColor.ChartColor!=Color.Empty)
								{
									color=lightColor=valueColor.ChartColor;

									if(valueColor.GradientColor!=Color.Empty)
									{
										lightColor = valueColor.GradientColor;
									}
								}
								LinearGradientangle=valueColor.GradientAngle;
							}
						#endregion        //End Modify

						#region Modified Area

							if(!HaveRoot())
							{
								if(!ColorManager.ValueToPieColor.Contains(cell.Name))
								{
									ColorManager.ValueToPieColor.Add(cell.Name,new PiediffColor(color,lightColor,LinearGradientangle));
								}						
							}

						#endregion        //Modify at 2008-12-23 11:34:37@Scott

						Brush brushPie = new LinearGradientBrush(rectfPie,lightColor,color,LinearGradientangle);				

						GraphicsPath path=new GraphicsPath();

						path.AddPie(rectfPie.X,rectfPie.Y,rectfPie.Width,rectfPie.Height,startAngle,sweepAngle);

						g.FillPath(brushPie,path);		

						if(startAngle<180)
						{
							if(startAngle+sweepAngle>=180)
							{
								SweepAngles.Add(180-startAngle);
							}
							else
							{
								SweepAngles.Add(sweepAngle);
							}
							DrawColors.Add(color);	
						}

						startAngle += sweepAngle;
					#endregion
				}					
			
				float rectDiff=rectfPie.Height/6;

				RectangleF rectfPie3d=new RectangleF(rectfPie.X,rectfPie.Y+rectDiff,rectfPie.Width,rectfPie.Height);
               
				startAngle = 0;

				PointF start1=PointF.Empty,start2=PointF.Empty,end1=PointF.Empty,end2=PointF.Empty;				

				for(int i=0;i<SweepAngles.Count;i++)
				{
					Color color=(Color)DrawColors[i];				

					Color deepColor = ColorManager.GetLightColor(color);

					Brush brush=new SolidBrush(color);

					float sweepangle=(float)SweepAngles[i];

					if(sweepangle<=0)continue;

					GraphicsPath path=new GraphicsPath();	

					path.StartFigure();

					path.AddArc(rectfPie.X,rectfPie.Y,rectfPie.Width,rectfPie.Height,startAngle,sweepangle);

					path.AddArc(rectfPie3d.X,rectfPie3d.Y,rectfPie3d.Width,rectfPie3d.Height,startAngle+sweepangle,-sweepangle); 

					path.CloseFigure();
		
					g.FillPath(brush,path);
					
					if(this.AllSeries[realCol].ValuesStyle.Count>0)  //2009-4-17 15:41:38@Simon Add this Code
					{
						Color borderColor=ColorManager.BorderColor;					
                        
						g.DrawPath(new Pen(borderColor),path);

					}
					
					startAngle+=sweepangle;

				}

				startAngle=0f;

				for(int i = 0; i < this.nRowCount; i++)	// draw pies
				{
					IChartCell cell = this.GetCell(i,j);	// get each cell

					if(cell.Name=="[NoValue]"||cell.DataPoint<1e-9)continue;

					sweepAngle = cell.DataPoint / totalDataPoint * 360;		// calculate sweep angle

					this.DrawPieText(g,rectfPie, startAngle,sweepAngle,  i, j);
					
					startAngle += sweepAngle;	// calculate start angle
                  
				}
			}

		}


		/// <summary>
		///Draw PieText at pie3d-Mode
		/// </summary>
		private void DrawPieText(Graphics g, RectangleF rectPie, float startAngle, float sweepAngle,  int row, int col)
		{
			IChartCell cell=this.GetCell(row,col);

			float fSpace=1f;

			int realcol=this.ColsIndex[col];

			if(!this.SeriesLabel(realcol).Visible)return;

			string strValue = cell.DataPoint.ToString();			

			float angle = startAngle + sweepAngle/2;
			
			int nColorIndex =row;
			
			Color color = ColorManager.DiffColor(nColorIndex);	

			#region Modified Area

			if(!HaveRoot())
			{
				if(ColorManager.ValueToPieColor.Contains(cell.Name))
				{							
					PiediffColor diffcolor=(PiediffColor)ColorManager.ValueToPieColor[cell.Name];

					color = diffcolor.pieColor;					

				}
			}

			#endregion        //Modify at 2008-12-23 11:34:37@Scott
		
			#region Modify codes at 2009-4-17 9:18:16@Simon

				ValueColor valueColor=this.ValueColor(realcol,cell.Name);

				if(valueColor!=null&&valueColor.ChartColor!=Color.Empty)
				{
					color=valueColor.ChartColor;
				}
			#endregion        //End Modify			

			if(this.SeriesLabel(realcol).Percent)
			{
				strValue = WebbTableCellHelper.FormatValue(this.GetPercentPoint(row,col),FormatTypes.Precent);

                #region Percent Format

                string percentFormat = this.SeriesLabel(realcol).DisplayFormat;

                if (percentFormat != string.Empty)
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder(percentFormat);

                    sb = sb.Replace("%", strValue);

                    sb = sb.Replace("#", cell.DataPoint.ToString());

                    sb = sb.Replace(@"\n", "\n");

                    sb = sb.Replace("[LineBreak]", "\n");

                    strValue = sb.ToString();
                }
                #endregion
			}

			if(cell.Name != string.Empty)
			{
				strValue = cell.Name + "\r\n" + strValue;
			}

			int len=this.SeriesLabel(realcol).LengthConnector;

			if(valueColor!=null&&valueColor.LengthConnector>0)
			{
				len=valueColor.LengthConnector;
			}

			PointF anglepoint=ChartHelper.AnglePoint(rectPie,angle);
			
			PointF pointArc=ChartHelper.PointByPoint(rectPie,angle,anglepoint.X,anglepoint.Y,len,SeriesLabel(realcol).Position);			

			if(this.SeriesLabel(realcol).VisibleConnector && this.SeriesLabel(realcol).Position == ChartTextPosition.Outside) 
			{
				Pen LinePen=new Pen(color);

				if(angle<180)LinePen=new Pen(ColorManager.BorderColor);

				g.DrawLine(LinePen,anglepoint,pointArc);	// draw connector

				pointArc=DrawHorLine(g,LinePen,angle,pointArc,len);
			}		
		
			Font seriesFont=this.SeriesLabel(realcol).Font;
           
			SizeF szText = g.MeasureString(strValue,seriesFont);	// get size of text

			RectangleF rectF =ChartHelper.GetRect(angle,pointArc,szText,SeriesLabel(realcol).Position);			
		
			rectF.Inflate(fSpace,fSpace);

			Color backColor=this.SeriesLabel(realcol).BackColor;

			if(backColor==Color.Empty)
			{
				g.FillRectangle(Brushes.White,rectF);	// fill background color
			}
			else
			{
				g.FillRectangle(new SolidBrush(backColor),rectF);	// fill background color
			}

			if(realcol == setting.SelectedSeriesIndex)	// highlight selected series
			{	
				g.DrawRectangle(Pens.Red,rectF.X,rectF.Y,rectF.Width,rectF.Height);	// draw rectangle
			} 
			else
			{	
				if(this.SeriesLabel(realcol).ShowBorders)
				{
					Color BorderColor=color;

					Color SeriesLabelborderColor=this.SeriesLabel(realcol).RectBorderColor;

					if(SeriesLabelborderColor!=Color.Empty)
					{
						BorderColor=SeriesLabelborderColor;
					}				
                    
					if(valueColor!=null&&valueColor.RectBorderColor!=Color.Empty)
					{
						BorderColor=valueColor.RectBorderColor;
					}

					g.DrawRectangle(new Pen(BorderColor),rectF.X,rectF.Y,rectF.Width,rectF.Height);	// draw rectangle

				}
			}

			StringFormat sf=new StringFormat();

			sf.Alignment=StringAlignment.Center;

			sf.LineAlignment=StringAlignment.Center;

			Color SeriesTextColor=this.SeriesLabel(realcol).TextColor;

			if(SeriesTextColor!=Color.Empty)
			{
				color=SeriesTextColor;
			}	

			if(valueColor!=null&&valueColor.TextColor!=Color.Empty)
			{
			     color=valueColor.TextColor;
			}				

			g.DrawString(strValue,seriesFont,new SolidBrush(color),rectF,sf);	// draw string			

			int index=row*this.nColumnCount+col;
			
			this.ClickAreas[index]=rectF;  //Added this code at 2008-11-11 14:45:37@Simon
			
		}


		// <summary>
		///get the start-point of the rectange that stores the pieText and drawing line to connect the rectange and the piechart
		/// </summary>
		private PointF DrawHorLine(Graphics g,Pen pen,float angle,PointF pointArc,int LineLen)
		{
			float xArc=pointArc.X,yArc=pointArc.Y;	
			
			if(LineLen<=0)return pointArc;

			if(angle<=90)
			{
				xArc=xArc+LineLen;
			}
			else if(angle<=180)
			{
				xArc=xArc-LineLen;
			}
			else if(angle<=270)
			{
				xArc=xArc-LineLen;
				
			}
			else
			{
				xArc=xArc+LineLen;
			}

			PointF dest=new PointF(xArc,yArc);	

			g.DrawLine(pen,pointArc,dest);

			return dest;
			
		}


	}
	#endregion

	//08-01-2008@Simon
	#region public class AxesChart : ChartBase
	[Serializable]
	public class AxesChart : ChartBase
	{	
		#region Fields
		protected PointF[,] PointsToDraw;
       
		protected float XEveryWidth=0f,YEveryWidth=0;    //modify this code at 2008-12-9 10:57:06@Simon    	

		protected float everyheight=30;

		private int YScaleCount=0;

		protected  int XScaleCount=0;     //Added this code at 2008-12-9 10:57:21@Simon

		protected float HEIGHTRATE=0.04F;
         
		protected Color[] PointColor;
		
		protected PointShowStyle ShowStyle=PointShowStyle.None; 
        
		protected int AxesXExtendHeight=4;

		protected int AxesYExtendWidth=4;

		protected int AxesXStringHeight;

		protected int AxesYStringWidth;
        
		protected float AxisXTitleHeight;

		protected float AxisYTitleHeight;

		protected Size RectSpace=new Size(0,0);

		protected int XLinesCount=1;

		protected int YLinesCount=1;	

		protected float radiox=0f,radioy=0f;

	   [NonSerialized]
	   protected float RelativeExPoint=0f;
     
		#endregion
  
		#region Base Mehods of ChartBase
		    
			public AxesChart() : base()
			{		
				
			}
			/// <summary>
			///    Inherbits CalculateResult from ChartBase 
			/// </summary>
			/// <param name="table"></param>
			/// <param name="rows"></param>
			public override void CalculateTable(DataTable table, Webb.Collections.Int32Collection rows)
			{   
				base.CalculateTable (table, rows);	
		
				PointsToDraw= new PointF[nRowCount, nColumnCount];

                if (this.IsAllPercent())
                {
                    for (int i = 0; i < this.nRowCount; i++)
                    {
                        for (int j = 0; j < this.nColumnCount; j++)
                        {
                            IChartCell cell = this.GetCell(i, j);

                            cell.DataPoint = Convert.ToSingle(cell.PercentText);
                        }
                    }

                }

				PointColor=new Color[AllSeries.Count];

				for(int i=0;i<this.AllSeries.Count;i++)
				{
					if(this.AllSeries[i].Color!=Color.Empty)
					{
						PointColor[i]=this.AllSeries[i].Color;
					}
					else
					{
						PointColor[i]=ColorManager.DiffColor(i);
					}
				}

				
	            
			}

			/// <summary>
			///       draw all points in Graphics
			/// </summary>
			/// <param name="g"></param>
			/// <param name="rect"></param>
			public override void Draw(Graphics g, Rectangle rect)
			{
				Color allseriesColor=Color.Empty;

				if(this.setting.ColorWhenMax!=Color.Empty&&AllSeries.Count>0)
				{
					allseriesColor=AllSeries[0].Color;

                    AllSeries[0].Color=this.setting.ColorWhenMax;

					PointColor[0]=this.setting.ColorWhenMax;
				}

				base.Draw(g,rect);

				if(this.setting.BoundSpace>0)
				{
					rect.Inflate(-this.setting.BoundSpace,-this.setting.BoundSpace);
				}

				if(this.Setting.Lengend.Visible)
				{
					float fLengendWidth = this.DrawLengend(g,rect);	// draw lengend

					rect.Width = rect.Width - (int)fLengendWidth;	// get chart area
				}	
		
				this.DrawChart(g,rect);

				if(AllSeries.Count>0)
				{
					 AllSeries[0].Color=allseriesColor;
				}
			}
           
			/// <summary>
			///      Cacluate the top maxpoint,and passed it to repeatControl when printing
			/// </summary>			
			public override float GetExMaxPoint()
			{
				float maxDatapoint=this.GetMaxDataPoint();

				maxDatapoint=maxDatapoint+this.RelativeExPoint;

				return maxDatapoint;
			}



		#endregion 

        #region Preparations for Drawing

			#region Adjust Space For ChartArea

	         	#region AxesSpace
					/// <summary>
					///    calcluate avaliable space about the AxesLabel in Multi-Line/Rotated mode
					/// </summary>
					/// <param name="g"></param>				
					private Size GetAxesStringSpace(Graphics g)
					{ 
						#region LineCount

							switch(this.setting.AxisX.LabelStyle)
							{
								case AxisLabelStyle.None:

									this.XLinesCount=0;

									break;

								case AxisLabelStyle.OneLine:

									this.XLinesCount=1;

									break;

								case AxisLabelStyle.Staggered:

									this.XLinesCount=2;

									break;	

								case AxisLabelStyle.SeriesCompared:
			
									if(this.ShowStyle==PointShowStyle.Bar)
									{
										this.XLinesCount=this.AllSeries.Count;	
									}
									else
									{
										this.XLinesCount=1;
									}

									break;	
							}
							switch(this.setting.AxisY.LabelStyle)
							{
								case AxisLabelStyle.None:

									this.YLinesCount=0;

									break;

								case AxisLabelStyle.OneLine:

									this.YLinesCount=1;

									break;

								case AxisLabelStyle.Staggered:

									this.YLinesCount=2;

									break;	

								case AxisLabelStyle.SeriesCompared:

									if(this.ShowStyle==PointShowStyle.HorizonBar)
									{
										this.YLinesCount=this.AllSeries.Count;												
									}
									else
									{
										this.YLinesCount=1;				
									}

									break;	
							}
						#endregion
				        
						int angleX=this.setting.AxisX.LabelAngle;  //Added this code at 2008-12-9 16:07:42@Simon

						int angleY=this.setting.AxisY.LabelAngle; //Added this code at 2008-12-9 16:07:45@Simon
				    
						string MaxYString=string.Format("{0:0.00}",this.GetMaxDataPoint()*(1+HEIGHTRATE));

						if(this.IsAllPercent())
						{				
							MaxYString="100.0%";
						}	
					    
						SizeF szfmaxY=SizeF.Empty;	
									
						double sinX=Math.Sin(angleX*Math.PI/180f);

						double cosX=Math.Cos(angleX*Math.PI/180f);

						double sinY=Math.Sin(angleY*Math.PI/180f);

						double cosY=Math.Cos(angleY*Math.PI/180f);

						int sqrtY=0;

						if(this.ShowStyle==PointShowStyle.HorizonBar)
						{  
							szfmaxY=g.MeasureString(MaxYString,this.setting.AxisX.Font);

							sqrtY=(int)(szfmaxY.Width*sinX+szfmaxY.Height*cosX)+6;	
							
						}
						else
						{
							szfmaxY=g.MeasureString(MaxYString,this.setting.AxisY.Font);

							sqrtY=(int)Math.Sqrt(szfmaxY.Width*szfmaxY.Width+szfmaxY.Height*szfmaxY.Height)+2;		
						}

						#region AxesPointDataSpace

							int XInHeight=0;
					
							int sqrtX=0;

							SizeF szfmaxX=SizeF.Empty;

							for(int i=0;i<this.nRowCount;i++)
							{	
								string AxesXLabel="";

								switch(this.ShowStyle)
								{
									case PointShowStyle.HorizonBar:

										for(int j=0;j<this.nColumnCount;j++)
										{	
											AxesXLabel=this.GetAxesXLabel(i,j);
											
											szfmaxX=g.MeasureString(AxesXLabel,this.setting.AxisY.Font);

											XInHeight=(int)(szfmaxX.Width*cosY+szfmaxX.Height*sinY)+3;
											
											if(sqrtX<XInHeight)sqrtX=XInHeight;
										}

										break;

									case PointShowStyle.Bar:

										for(int j=0;j<this.nColumnCount;j++)
										{	
											AxesXLabel=this.GetAxesXLabel(i,j);	
											
											szfmaxX=g.MeasureString(AxesXLabel,this.setting.AxisX.Font);
												
											XInHeight=(int)(szfmaxX.Width*sinX+szfmaxX.Height*cosX)+6;	
										
											if(sqrtX<XInHeight)sqrtX=XInHeight;
										}

										break;

									default:

										AxesXLabel=this.GetAxesXLabel(i);	

										szfmaxX=g.MeasureString(AxesXLabel,this.setting.AxisX.Font);

										XInHeight=(int)(szfmaxX.Width*sinX+szfmaxX.Height*cosX)+3;

										if(sqrtX<XInHeight)sqrtX=XInHeight;

										break;

								}
							}
						#endregion        //End Modify

						int szfYWidth = this.YLinesCount*sqrtY;

						int szfXHeight =this.XLinesCount*sqrtX;
				
						if(this.ShowStyle==PointShowStyle.HorizonBar)
						{
							szfYWidth=this.YLinesCount*sqrtX;

							szfXHeight=this.XLinesCount*sqrtY;
						}
						
						return new Size(szfYWidth,szfXHeight);
					}       
				

					/// <summary>
					///   set avaliable space about the AxesLabel and tick-marks to the public fields to					
					///   calcualte the availble Chart-Size
					/// </summary>
					/// <param name="g"></param>		
					private void SetAxesSpace(Graphics g)
					{
						#region set values of AxesSpace
							//Get Distance between Aexs-Y and the Left of the Graphics or Distance between Aexs-X and the Bottom of the Graphics
							Size AxesStringSpace=this.GetAxesStringSpace(g);

							AxesXStringHeight=AxesStringSpace.Height;

							AxesYStringWidth=AxesStringSpace.Width;

							if(!this.setting.AxisX.Visible)
							{
								AxesXStringHeight=0;

								AxesXExtendHeight=0;
							}

							if(!this.setting.AxisY.Visible)
							{
								AxesYExtendWidth=0;

								AxesYStringWidth=0;
							}
				            
							AxisXTitleHeight=0f;

							AxisYTitleHeight=0f;

							if(this.setting.AxisX.Title!=null&&this.setting.AxisX.Title.Length>0&&this.setting.AxisX.Visible)
							{ 
								SizeF szXTitle=g.MeasureString(setting.AxisX.Title,this.setting.AxisX.TitleFont);

								AxisXTitleHeight=1f+szXTitle.Height;				
							}		

							if(this.setting.AxisY.Title!=null&&this.setting.AxisY.Title.Length>0&&this.setting.AxisY.Visible)
							{ 
								SizeF szYTitle=g.MeasureString(setting.AxisY.Title,this.setting.AxisY.TitleFont);

								AxisYTitleHeight=1f+szYTitle.Height;
							}
						
						#endregion
					}


		       #endregion

		        #region Space For ChartArea
                    
					/// <summary>
					///    Re-calculate the availble Chart-Size to make the SeriesLabel and 				
					///    connector-Line not to exceed the bound				
					/// </summary>
					/// <param name="g"></param>		
					/// <param name="RectWidth"></param>	
					/// <param name="RectHeight"></param>	
			
					protected SizeF AdjustSizeForArea(Graphics g,float RectWidth,float RectHeight)
					{
						if(!this.CheckedCols())
						{
							return new SizeF(RectWidth,RectHeight);
						}
						float TotalReal=0f;

						bool Allpercent=this.IsAllPercent(); 

						float passedMaxValue=this.setting.MaxValuesWhenTop;

						if(Allpercent)
						{
							if(this.setting.Relative)
							{
								TotalReal=GetMaxDataPoint()*(1+HEIGHTRATE);
							}
							else
							{
							    if(passedMaxValue>=0&&passedMaxValue<=1)
								{
									TotalReal=passedMaxValue*(1+HEIGHTRATE);
								}
								else
								{
								    TotalReal=(1+HEIGHTRATE);
							    }							
							}
						}
						else 
						{ 
							if(this.setting.Relative)
							{								
								TotalReal=GetMaxDataPoint()*(1+HEIGHTRATE);								
							}
							else
							{
								if(passedMaxValue>=1)
								{
									TotalReal=passedMaxValue*(1+HEIGHTRATE);
								}
								else
								{
								    TotalReal=nTotals*(1+HEIGHTRATE);
								}
							}
						}

						float ExSpaceX=0f,ExSpaceY=0f;

						float maxlen=0f;
					   
						#region ExSpace
						for(int j = 0; j < this.nColumnCount; j++)
						{
							int realcol=this.ColsIndex[j];

							if(!this.SeriesLabel(realcol).Visible)continue;

							int length=SeriesLabel(realcol).LengthConnector;

							SizeF szfText=this.CalculateTextSize(g,realcol);

							float angle=SeriesLabel(realcol).Angle;   
	                   
							float sinA=(float)Math.Sin(angle*Math.PI/180);

							float cosA=(float)Math.Cos(angle*Math.PI/180);		

							#region Differnent ways in ShowStyle

							for(int i = 0; i < this.nRowCount; i++)
							{
								IChartCell cell=this.GetCell(i,j);

								if(cell.Name=="[NoValue]")continue;

								float datapoint=cell.DataPoint;

								float lenY=0f,lenX=0f;

								float TempExSpace=0f;

								switch(this.ShowStyle)
								{
									case PointShowStyle.Bar:

										if(this.SeriesLabel(realcol).Position==ChartTextPosition.Outside)
										{ 
											lenY=szfText.Height+length+2f;                                      
											//calculate Extra space 
											TempExSpace=this.ExSpaceInArea(RectHeight,TotalReal,datapoint,lenY);

											ExSpaceY=Math.Max(TempExSpace,ExSpaceY);   

											maxlen=Math.Max(maxlen,lenY);										
										}                                
										break;
									case PointShowStyle.HorizonBar:
										if(this.SeriesLabel(realcol).Position==ChartTextPosition.Outside)
										{
											lenX=szfText.Width+length+2f;

											TempExSpace=this.ExSpaceInArea(RectWidth,TotalReal,datapoint,lenX);	
										
											ExSpaceX=Math.Max(TempExSpace,ExSpaceX); 

											maxlen=Math.Max(maxlen,lenX);
											
										}									 
	                                    
										break;
									default:
										if(cosA>=0)
										{
											lenX=szfText.Width+length*cosA+2;
										}
										if(sinA>=0)
										{
											lenY=szfText.Height+length*sinA+2f;
										}

										TempExSpace=this.ExSpaceInArea(RectWidth,nRowCount*1f,i+0.5f,lenX);
	                                    
										ExSpaceX=Math.Max(TempExSpace,ExSpaceX);  										

										TempExSpace=this.ExSpaceInArea(RectHeight,TotalReal,datapoint,lenY);
	                                    
										ExSpaceY=Math.Max(TempExSpace,ExSpaceY);   	
								
										maxlen=Math.Max(maxlen,lenY);	

										break;
								}
								
							}
							#endregion
						}

						#endregion		

						if(RectWidth-ExSpaceX<=0||RectHeight-ExSpaceY<=0)return new SizeF(0f,0f);

						float datamax=this.GetMaxDataPoint();
				
						switch(this.ShowStyle)
						{
							case PointShowStyle.Bar:
							default:
								this.RelativeExPoint=((RectHeight-ExSpaceY)*datamax)/((RectHeight-ExSpaceY)-maxlen)-datamax;								                               
								break;

							case PointShowStyle.HorizonBar:								
								this.RelativeExPoint=((RectWidth-ExSpaceX)*datamax)/((RectWidth-ExSpaceX)-maxlen)-datamax;				                                    
								break;							
						}     
						RelativeExPoint=Math.Max(0,RelativeExPoint);

						return new SizeF(RectWidth-ExSpaceX,RectHeight-ExSpaceY);
					
					}
		          

					/// <summary>
					///       calculate the max height/Width about SeriesLabel and 					
					///        connector-Line not to exceed the bound				
					/// </summary>					
					/// <param name="RectWidth"></param>	
					/// <param name="TotalReal"></param>	
					/// <param name="DataPoint"></param>	
					/// <param name="len"></param>					
					private float ExSpaceInArea(float RectWidth, float TotalReal,float DataPoint,float len)
					{
						//If set vars below
						//        k -----the Extra height/Widfth
						//        m ---- RectWidth/RectHeight
						//        max----maxdatapoint
						//        total-----the totalReal points                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      
						//        len--the Line's Length and Rectangle's Width/Height for PointDescription  
							
						//all Conditions should Fit below:(for the maxdatapoint's description should not exceed the TotalArea)
						//  (m-k)/total*max+len<=m+1
						//so k>=m-(m+1-len)*total/max

						if(DataPoint<=0)return 0;

						//apply "k>=m-(m-len)*total/max' Condition	
						float ExSpace=RectWidth-((RectWidth-len)*TotalReal/DataPoint);     

						ExSpace=Math.Max(ExSpace,0f);

						return ExSpace;
					}		
				

				#endregion

		    #endregion
	    	#region Set Important Variables in module

				/// <summary>
				///      Set important public vars for drawing chart,such as
				///       radio about the every tick-makes mean and GridLineSpace about Axes
				/// </summary>					
				/// <param name="ChartArea"></param>						
				protected void SetLocalVars(RectangleF ChartArea)
				{
					#region Modify codes at 2008-12-10 16:28:48@Simon

					float datamax=0f,MinorTicks=1f;   

					bool Allpercent=this.IsAllPercent();

					float passedMaxValue=this.setting.MaxValuesWhenTop;
		           
					if(Allpercent)
					{
						if(this.setting.Relative)
						{
							datamax=GetMaxDataPoint()*(1+HEIGHTRATE);									
						}
						else
						{
							if(passedMaxValue>=0&&passedMaxValue<=1)
							{
								datamax=passedMaxValue*(1+HEIGHTRATE);
							}
							else
							{
								datamax=(1+HEIGHTRATE);
							}
						}

						MinorTicks=0.01f;
					}
					else 
					{  
						MinorTicks=1f;

						if(this.setting.Relative)
						{						
							datamax=GetMaxDataPoint()*(1+HEIGHTRATE);										
						}
						else
						{
							if(passedMaxValue>=1)
							{
								datamax=passedMaxValue*(1+HEIGHTRATE);
							}
							else
							{
								datamax=nTotals*(1+HEIGHTRATE);
							}
						}
					}
					
					radiox=ChartArea.Width/datamax;

					radioy=ChartArea.Height/datamax;
					
					
					if(this.ShowStyle==PointShowStyle.HorizonBar)
					{
						if(this.setting.AxisX.GridLineSpace>0)
						{
							everyheight=this.setting.AxisX.GridLineSpace*radiox*MinorTicks;
						}
						else
						{
							everyheight=45f;
						}
					  
						
					}
					else
					{
						if(this.setting.AxisY.GridLineSpace>0)
						{
							everyheight=this.setting.AxisY.GridLineSpace*radioy*MinorTicks;
						}
						else
						{
							everyheight=30f;
						}			
					}
			
					#endregion        //End Modify
				}

		    #endregion

		#endregion

		#region Main Functions For DrawngChart			

			/// <summary>
			/// draw Axes and  Chart in ChartArea
			/// </summary>
			/// <param name="g"></param>
			/// <param name="BoundArea"></param>
			/// <param name="ChartArea"></param>
			protected virtual void DrawAxesAndPoints(Graphics g, Rectangle BoundArea,RectangleF ChartArea)
			{ 
				Pen MajorGridLinePen=new Pen(ColorManager.MajorGridLineColor,1);

				Pen SecondaryGridLinePen=new Pen(ColorManager.SecondaryGridLineColor,1);

				#region AxesPen

					Color AxesXBorderColor=ColorManager.AxesColor;

					if(this.setting.AxisX.MajorBorderColor!=Color.Empty)
					{
						AxesXBorderColor=this.setting.AxisX.MajorBorderColor;
					}
					Pen AxesXPen=new Pen(AxesXBorderColor,1);
		
					Color AxesYBorderColor=ColorManager.AxesColor;

					if(this.setting.AxisY.MajorBorderColor!=Color.Empty)
					{
						AxesYBorderColor=this.setting.AxisY.MajorBorderColor;
					}

					Pen AxesYPen=new Pen(AxesYBorderColor,1);	

				#endregion
	   
				Color backColor=this.setting.BackgroundColor;

				if(backColor==Color.Empty)
				{
					backColor=ColorManager.BackColor;
				}
				else
				{					
					backColor=Color.FromArgb(122,this.setting.BackgroundColor);
				}

				
				SolidBrush Backbrush=new SolidBrush(backColor);
				
			    // fill back Ground
				g.FillRectangle(Backbrush,ChartArea); 		
	
				Backbrush.Dispose();			
	           
				#region local variable for this Unit	
		
					if(this.nColumnCount<1)return;				
		            
					int XMinorCount=this.setting.AxisX.MinorCount;

					int YMinorCount=this.setting.AxisY.MinorCount;
		           
					float XMinorLineLen=this.AxesXExtendHeight/2f;

					float YMinorLineLen=this.AxesYExtendWidth/2f;
					
					int Ycount=0,Xcount=0;
			
					float StartXWidth=0f; 	

					float StartYWidth=0f;	

					if(this.ShowStyle==PointShowStyle.HorizonBar)
					{
						XEveryWidth=this.everyheight;

						YEveryWidth=ChartArea.Height/(float)(this.nRowCount);				

						XScaleCount=(int)(ChartArea.Width/this.everyheight);

						Xcount=XScaleCount;

						Ycount=this.nRowCount;

						StartXWidth=ChartArea.X; 	

						StartYWidth=ChartArea.Y+YEveryWidth/2f;	
					}
					else
					{
						XEveryWidth=ChartArea.Width/(float)(this.nRowCount);

						YEveryWidth=this.everyheight;

						YScaleCount=(int)(ChartArea.Height/this.everyheight);	

						Xcount=this.nRowCount;

						Ycount=YScaleCount;

						StartXWidth=ChartArea.X+XEveryWidth/2f; 	

						StartYWidth=ChartArea.Y;
					}		
						
					float MinorXEveryWidth=XMinorCount>0?XEveryWidth/(float)XMinorCount:0;

					float MinorYScaleSpace=YMinorCount>0?YEveryWidth/(float)YMinorCount:0;			

				#endregion
		 
				#region Fill InterlaceRectangle	
							
					for(int i=0;i<=Ycount;i++)
					{			
						if(i%2==0&&this.Setting.AxisY.Interlaced)
						{
							RectangleF RectInterY=new RectangleF(ChartArea.X,StartYWidth+YEveryWidth*i,ChartArea.Width,YEveryWidth);					
							
							g.FillRectangle(Brushes.White,RectInterY);
						}
					}

					for( int i=0;i<Xcount;i++)
					{ 			 
						if(i%2==0&&this.Setting.AxisX.Interlaced)
						{
							RectangleF RectInterX=new RectangleF(StartXWidth+i*XEveryWidth,ChartArea.Y,XEveryWidth,ChartArea.Height);					
							
							g.FillRectangle(Brushes.White,RectInterX);
						}
					}
				#endregion

				#region draw Grid-Lines

					#region draw Y_lines

						for(int i=0;i<=Ycount;i++)
						{  
							float StartY=StartYWidth+YEveryWidth*i;	
					
							if(StartY>ChartArea.Y+ChartArea.Height||StartY<ChartArea.Y)continue;

							//draw the Horizal Grid-Lines
							if(this.setting.AxisY.Tickmarks!=GridLineStyle.None&&this.setting.AxisY.Visible)
							{	//Draw Major-Tickmarks at  AxisY				
								g.DrawLine(AxesYPen,ChartArea.X-AxesYExtendWidth,StartY,ChartArea.X,StartY);
							}
							if(this.setting.AxisY.Gridlines!=GridLineStyle.None)					
							{     //Draw Major-Gridlines at  AxisY	
								g.DrawLine(MajorGridLinePen,ChartArea.X,StartY,ChartArea.X+ChartArea.Width,StartY);						
							}
								
							for(int j=-(YMinorCount/2);j<=YMinorCount/2;j++)
							{
								if(j==0)continue;
						
								float MinerY=StartY+j*MinorYScaleSpace;					
								
								if(MinerY>ChartArea.Y+ChartArea.Height||MinerY<ChartArea.Y)continue;
								
								if(this.setting.AxisY.Tickmarks==GridLineStyle.All&&this.setting.AxisY.Visible)
								{
									//Draw Secondary-Tickmarks at  AxisY	
									g.DrawLine(AxesYPen,ChartArea.X-AxesYExtendWidth/2f,MinerY,ChartArea.X,MinerY);
								}
								if(this.setting.AxisY.Gridlines==GridLineStyle.All)
								{
									//Draw Secondary-Gridlines at  AxisY	
									g.DrawLine(SecondaryGridLinePen,ChartArea.X,MinerY,ChartArea.X+ChartArea.Width,MinerY);	
								}
							} 					
						}
					#endregion	

					#region draw X_lines

						for( int i=0;i<=Xcount;i++)
						{ 	
							float StartX=StartXWidth+i*XEveryWidth;		
				
							if(StartX>ChartArea.Right||StartX<=ChartArea.X)continue;

							//	draw Vertical Grid-Lines
							if(this.setting.AxisX.Tickmarks!=GridLineStyle.None&&this.setting.AxisX.Visible)
							{
								//Draw Major-Tickmarks at  AxisX	
								g.DrawLine(AxesXPen,StartX,ChartArea.Y-AxesXExtendHeight,StartX,ChartArea.Y);
							}
							if(this.setting.AxisX.Gridlines!=GridLineStyle.None)
							{
								//Draw Major-Gridlines at  AxisX	
								g.DrawLine(MajorGridLinePen,StartX,ChartArea.Y,StartX,ChartArea.Y+ChartArea.Height);
							}
							for(int j=-(XMinorCount/2);j<=XMinorCount/2;j++)
							{
								if(j==0)continue;

								float MinerX=StartX+j*MinorXEveryWidth;					

								if(MinerX>ChartArea.Right||MinerX<=ChartArea.X)continue;
			       
								if(this.setting.AxisX.Tickmarks==GridLineStyle.All&&this.setting.AxisX.Visible)
								{
									//Draw Secondary-Tickmarks at  AxisX	
									g.DrawLine(AxesXPen,MinerX,ChartArea.Y-AxesXExtendHeight/2f,MinerX,ChartArea.Y);
								}
								if(this.setting.AxisX.Gridlines==GridLineStyle.All)
								{
									//Draw Secondary-Gridlines at  AxisX	
									g.DrawLine(SecondaryGridLinePen,MinerX,ChartArea.Y,MinerX,ChartArea.Y+ChartArea.Height);
								}

							}
							
							
						}
					#endregion
				#endregion				
				

                this.DrawChartStyle(g,BoundArea,ChartArea);       
          
				#region DrawAxes-Line

					if(this.setting.AxisX.Gridlines!=GridLineStyle.None)
					{					
						g.DrawLine(AxesYPen,ChartArea.X,ChartArea.Y,ChartArea.X,ChartArea.Bottom);					
						
						g.DrawLine( MajorGridLinePen,ChartArea.Right,ChartArea.Y,ChartArea.Right,ChartArea.Bottom);
					}
					if(this.setting.AxisY.Gridlines!=GridLineStyle.None)
					{					
						g.DrawLine(AxesXPen,ChartArea.X,ChartArea.Y,ChartArea.Right,ChartArea.Y);					
						
						g.DrawLine( MajorGridLinePen,ChartArea.X,ChartArea.Bottom,ChartArea.Right,ChartArea.Bottom);
					}	
				#endregion

				AxesXPen.Dispose();	

                AxesYPen.Dispose();
				
				AxesXPen=null;

				AxesYPen=null;

				MajorGridLinePen.Dispose();

				SecondaryGridLinePen.Dispose();

				MajorGridLinePen=null;

				SecondaryGridLinePen=null;
				
				
			}
					
			
			/// <summary>
			/// Sub Function of DrawAxesAndPoints
			/// draw Draw Chart in the Rectangle
			/// </summary>
			/// <param name="g"></param>
			/// <param name="ChartArea"></param>
			protected virtual void DrawChartStyle(Graphics g,Rectangle BoundArea,RectangleF ChartArea)
			{
				#region DrawPoint
							
				float ScaleXValue=ChartArea.X+XEveryWidth/2f;	 
			
				//save and draw points
				for(int i=0;i<this.nRowCount;i++)
				{				
					for(int j=0;j<this.nColumnCount;j++)
					{
						IChartCell cell=this.GetCell(i,j);	

						float CellToY=0;

						CellToY=ChartArea.Y+(cell.DataPoint*radioy);

						PointsToDraw[i,j]=new PointF(ScaleXValue,CellToY);	

						//draw Points
						RectangleF PointRectangle=new RectangleF(ScaleXValue-4,CellToY-4,8,8);

						int realcol=this.ColsIndex[j];

						if(cell.Name!="[NoValue]")
						{
							g.FillEllipse(new SolidBrush(PointColor[realcol]),PointRectangle);

							Color DrawColor=Color.Empty;	
	
							if(this.AllSeries[realcol].Color==Color.Empty)
							{				
								DrawColor=ColorManager.GetDeepColor(PointColor[realcol]);
							}
							else
							{
								if(this.AllSeries[realcol].BorderColor!=Color.Empty)
								{
									DrawColor=this.AllSeries[realcol].BorderColor;
								}
								else
								{
									DrawColor=ColorManager.BorderColor;
								}	
							}
							Pen CirclePen=new Pen(DrawColor,1);							

							g.DrawEllipse(CirclePen,PointRectangle);

							CirclePen.Dispose();	
						}
				
					}

					ScaleXValue=ScaleXValue+XEveryWidth;
				}
				#endregion
			}
			/// <summary>
			/// draw lengend
			/// </summary>
			/// <param name="g"></param>
			/// <param name="rect"></param>
			/// <returns></returns>
			public override float DrawLengend(Graphics g, Rectangle rect)
			{
				SolidBrush brush = new SolidBrush(ColorManager.BackColor);

				Pen pen = new Pen(ColorManager.BorderColor);

				float TextWidth=0,TextHeight=0;	

				Font seriesFont=Webb.Utility.GlobalFont;

				g.SmoothingMode=System.Drawing.Drawing2D.SmoothingMode.HighQuality;
					
				for(int i = 0; i <this.AllSeries.Count; i++)
				{
					seriesFont=this.SeriesLabel(i).Font;  //Added this code at 2008-12-18 14:19:23@Simon			

					SizeF szfText = g.MeasureString(this.AllSeries[i].Name,seriesFont);	// get size of text

					if(TextWidth<szfText.Width)TextWidth=szfText.Width;

					if(TextHeight<szfText.Height)TextHeight=szfText.Height;
						      
				}	
				
				float nWidth = setting.Lengend.SizeMarker.Width + TextWidth + setting.Lengend.SizeSpacing.Width * 3;	// get width of lengend

				float nHeight = Math.Max(TextHeight,setting.Lengend.SizeMarker.Height) + setting.Lengend.SizeSpacing.Height * 2;	// get height of lengend
				
				if((!this.Setting.Lengend.VisibleMarker)||Setting.Lengend.SizeMarker.Height*Setting.Lengend.SizeMarker.Width==0)
				{
					nWidth = TextWidth + setting.Lengend.SizeSpacing.Width * 2;
				}
			
				RectangleF rcLengend = new RectangleF(rect.Right - nWidth-2, rect.Top, nWidth, nHeight);	// get area of each lengend
				
				float radius=setting.Lengend.SizeMarker.Height/2f;            //circle's Radius(half of the diameter)  

				radius=Math.Min(radius,(setting.Lengend.SizeMarker.Width-4)/2f);

				float reLineWidth=(setting.Lengend.SizeMarker.Width-radius*2)/2f;

				reLineWidth=Math.Max(reLineWidth,2);							

				for(int seriesIndex= 0; seriesIndex < this.AllSeries.Count; seriesIndex++)
				{				
					g.FillRectangle(brush,rcLengend);  

					Color FillColor=PointColor[seriesIndex];					

					if(this.setting.Lengend.VisibleMarker)
					{	
						float reGraphLeft=rcLengend.Left + setting.Lengend.SizeSpacing.Width;

						float reGraphTop=rcLengend.Top+nHeight/2; 

						Color DrawColor=Color.Empty;	
		
						if(this.AllSeries[seriesIndex].Color==Color.Empty)
						{				
							DrawColor=ColorManager.GetDeepColor(FillColor);
						}
						else
						{
							if(this.AllSeries[seriesIndex].BorderColor!=Color.Empty)
							{
								DrawColor=this.AllSeries[seriesIndex].BorderColor;
							}
							else
							{
								DrawColor=ColorManager.BorderColor;
							}	
						}

						Pen BorderPen=new Pen(DrawColor);	
					
						Brush FillBrush=new SolidBrush(FillColor);					
					
						switch(this.ShowStyle)
						{
							case PointShowStyle.Line:

								Pen DrawPen=new Pen(FillColor);

								g.DrawLine(DrawPen,reGraphLeft,reGraphTop,reGraphLeft+reLineWidth,reGraphTop);                                

								if(radius>0)
								{
									g.FillEllipse(FillBrush,reGraphLeft+reLineWidth,reGraphTop-radius,2*radius,2*radius);
								
									g.DrawEllipse(BorderPen,reGraphLeft+reLineWidth,reGraphTop-radius,2*radius,2*radius);
								}
								
								g.DrawLine(DrawPen,reGraphLeft+reLineWidth+2*radius,reGraphTop,reGraphLeft+2*reLineWidth+2*radius,reGraphTop);
							
								DrawPen.Dispose();

								break;

							case PointShowStyle.Point:
								if(radius>0)
								{
									g.FillEllipse(FillBrush,reGraphLeft+reLineWidth,reGraphTop-radius,2*radius,2*radius);	
					
									g.DrawEllipse(BorderPen,reGraphLeft+reLineWidth,reGraphTop-radius,2*radius,2*radius);
								}

								break;

							case PointShowStyle.HorizonBar:                                   //Added this code at 2008-12-9 17:00:24@Simon
							
							case PointShowStyle.Bar:

								RectangleF BarRect=new RectangleF(rcLengend.Left + setting.Lengend.SizeSpacing.Width,
																	rcLengend.Top + setting.Lengend.SizeSpacing.Height,
																	setting.Lengend.SizeMarker.Width,
																	setting.Lengend.SizeMarker.Height
									                               );

								if(BarRect.Width*BarRect.Height>0)
								{
									Color LightColor=ColorManager.GetLightColor(FillColor);

									if(this.AllSeries[seriesIndex].Color!=Color.Empty)
									{
										LightColor=FillColor;
									}

									LinearGradientBrush BarBrush=new LinearGradientBrush(BarRect,LightColor,FillColor,LinearGradientMode.Horizontal);
									
									g.FillRectangle(BarBrush,BarRect);										

									g.DrawRectangle(BorderPen,BarRect.X,BarRect.Y,BarRect.Width,BarRect.Height);

									BarBrush.Dispose();									
								}
							
								break;
						}
						BorderPen.Dispose();

						FillBrush.Dispose();

						g.DrawString(AllSeries[seriesIndex].Name,seriesFont,Brushes.Black,new RectangleF(rcLengend.Left + setting.Lengend.SizeSpacing.Width * 2 + setting.Lengend.SizeMarker.Width, rcLengend.Top, rcLengend.Width - setting.Lengend.SizeSpacing.Width * 2 - setting.Lengend.SizeMarker.Width, rcLengend.Height),Lengend.StringFormat);
					}
					else
					{
						g.DrawString(AllSeries[seriesIndex].Name,seriesFont,Brushes.Black,rcLengend,Lengend.StringFormat);
					}

					rcLengend.Offset(0,rcLengend.Height - setting.Lengend.SizeSpacing.Height);
					
				}

				g.DrawRectangle(pen,rcLengend.Left,rect.Top,nWidth,rcLengend.Bottom - rcLengend.Height + setting.Lengend.SizeSpacing.Height - rect.Top);
				
				return nWidth+2;
			}       
			

			/// <summary>
			/// draw chart
			/// </summary>
			/// <param name="g"></param>
			/// <param name="rect"></param>	
			public override void DrawChart(Graphics g, Rectangle rect)
			{ 
				if(this.nColumnCount<1||this.GetMaxDataPoint()<=0)return;
	 
				this.SetAxesSpace(g);		

				Rectangle BoundArea=rect; 	
		
				PointsToDraw=new PointF[this.nRowCount,this.nColumnCount]; 

				int BmpWidth=BoundArea.Width;

				int BmpHeight=BoundArea.Height;

				if(BmpWidth-AxisYTitleHeight-AxesYStringWidth-2*RectSpace.Width-AxesYExtendWidth<=2 ||
					  BmpHeight-AxisXTitleHeight-AxesXStringHeight-this.AxisXTitleHeight-2*RectSpace.Height<=2)
					  return;

				Bitmap BmpMemory=new Bitmap(BmpWidth,BmpHeight);	

				float ChartX=AxesYStringWidth+RectSpace.Width+AxisYTitleHeight+AxesYExtendWidth;

				float ChartY=AxesXStringHeight+RectSpace.Height+AxisXTitleHeight+AxesXExtendHeight;

				float ChartWidth=BmpWidth-AxisYTitleHeight-AxesYStringWidth-2*RectSpace.Width-AxesYExtendWidth;

				float ChartHeight=BmpHeight-AxisXTitleHeight-AxesXStringHeight-AxesXExtendHeight-2*RectSpace.Height;

				SizeF ChartSize=this.AdjustSizeForArea(g,ChartWidth,ChartHeight);  //Added this code at 2008-12-17 12:30:02@Simon

				if(ChartSize.Width<=0||ChartSize.Height<=0)return;

				RectangleF ChartArea=new RectangleF(ChartX,ChartY,ChartSize.Width,ChartSize.Height);
				
				this.SetLocalVars(ChartArea);

				Graphics GraphicsMemory=Graphics.FromImage(BmpMemory);

				GraphicsMemory.SmoothingMode=System.Drawing.Drawing2D.SmoothingMode.HighQuality;	
			  
				//Draw and Flip the Chart to make Axis-Y up  
				this.DrawAxesAndPoints(GraphicsMemory,BoundArea,ChartArea);			

				BmpMemory.RotateFlip(RotateFlipType.RotateNoneFlipY);          
	           
				this.TransPointAll(BoundArea);

				this.DrawStrings(GraphicsMemory,BoundArea,ChartArea);
		
				g.DrawImage(BmpMemory,rect.X,rect.Y);

				GraphicsMemory.Dispose();

				BmpMemory.Dispose();
				
			}
		
		    
			/// <summary>
			///Draw all the string(contained the labels,titles and PointData showed in the graphics) about the graphics
			/// </summary>
			/// <param name="g"></param>
			/// <param name="BoundArea"></param>	
			/// <param name="ChartArea"></param>	
			protected virtual void DrawStrings(Graphics g,Rectangle BoundArea,RectangleF ChartArea)
			{	  
				StringFormat sf=new StringFormat();

				sf.Alignment=StringAlignment.Center;

				sf.LineAlignment=StringAlignment.Center;			
			
				if(this.nColumnCount<1)
				{
					return;
				}
				if(this.nRowCount*this.nColumnCount<=0)return;
				
				this.DrawPointData(g,BoundArea,ChartArea);			

				g.SmoothingMode=SmoothingMode.HighQuality;          //HightQuility Setting

				g.CompositingQuality=CompositingQuality.HighQuality;

				g.InterpolationMode=InterpolationMode.HighQualityBicubic;
				
				//draw Axes_GroupValue scalelabel
				#region //Axes_GroupValue scalelabel

				if(this.ShowStyle==PointShowStyle.HorizonBar)
				{
					#region draw Axes_HorizonBar_Y scaleLabel

						if(this.YLinesCount>0&&this.setting.AxisY.Visible)
						{
							int angleY=this.setting.AxisY.LabelAngle;

							for(int i=0;i<this.nRowCount;i++)
							{
								if(!this.setting.CombinedTitle)
								{
									#region //Draw Label Alls

										for(int j=0;j<this.nColumnCount;j++)
										{
											string AxesXLabel=this.GetAxesXLabel(i,j);

											if(AxesXLabel=="[NoValue]")continue;

											if(!string.Equals(AxesXLabel,""))
											{
												int drawYrectLen=this.AxesYStringWidth/this.YLinesCount;
			                                  
												int realCol=this.ColsIndex[j];

												int modY0=0; 

												if(this.nColumnCount<=1)
												{
													modY0=i%this.YLinesCount+1;
												}
												else
												{
													modY0=realCol%this.YLinesCount+1;
												}
																
												float AxesPoint_X=ChartArea.X-this.AxesYExtendWidth-modY0*drawYrectLen+drawYrectLen/2f;			
									
												float AxesPoint_Y=PointsToDraw[i,j].Y;

												g.TranslateTransform(AxesPoint_X,AxesPoint_Y);

												if(angleY!=0)
												{
													g.RotateTransform(angleY);
												}

												#region Modify codes at 2008-10-30 16:54:14@Simon	
													//RectangleF AreaForAxes_X=new RectangleF(-drawXrectLen/2f,-drawXrectLen/2f,drawXrectLen,drawXrectLen);
												
													int YInHeight=(int)g.MeasureString(AxesXLabel,this.setting.AxisY.Font).Height+3;
				
													int YInWidth=(int)g.MeasureString(AxesXLabel,this.setting.AxisY.Font).Width+3;	

													RectangleF AreaForAxes_X=new RectangleF(-YInWidth/2f,-YInHeight/2f,YInWidth,YInHeight);
												
												#endregion        //End Modify

												sf.FormatFlags=StringFormatFlags.NoWrap;

												g.TextRenderingHint=this.setting.AxisY.FontQuality;

												g.DrawString(AxesXLabel,this.setting.AxisY.Font,Brushes.Black,AreaForAxes_X,sf);

												g.ResetTransform();
											}
										}
									#endregion
								}
								else
								{
									#region // Draw Only First Series Title

										string AxesXLabel=this.GetAxesXLabel(i,0);

										if(AxesXLabel=="[NoValue]")continue;

										if(!string.Equals(AxesXLabel,""))
										{
											int drawYrectLen=this.AxesYStringWidth/this.YLinesCount;
			                                  
											int realCol=this.ColsIndex[0];

											int modY0=0; 

											if(this.nColumnCount<=1)
											{
												modY0=i%this.YLinesCount+1;
											}
											else
											{
												modY0=realCol%this.YLinesCount+1;
											}
																
											float AxesPoint_X=ChartArea.X-this.AxesYExtendWidth-modY0*drawYrectLen+drawYrectLen/2f;			
									
											float AxesPoint_Y=0;

											for(int j=0;j<this.nColumnCount;j++)
											{
												AxesPoint_Y+=PointsToDraw[i,j].Y;
											}

											AxesPoint_Y/=nColumnCount;

											g.TranslateTransform(AxesPoint_X,AxesPoint_Y);

											if(angleY!=0)
											{
												g.RotateTransform(angleY);
											}

											#region Modify codes at 2008-10-30 16:54:14@Simon	
												//RectangleF AreaForAxes_X=new RectangleF(-drawXrectLen/2f,-drawXrectLen/2f,drawXrectLen,drawXrectLen);
												
												int YInHeight=(int)g.MeasureString(AxesXLabel,this.setting.AxisY.Font).Height+3;	

												int YInWidth=(int)g.MeasureString(AxesXLabel,this.setting.AxisY.Font).Width+3;	

												RectangleF AreaForAxes_X=new RectangleF(-YInWidth/2f,-YInHeight/2f,YInWidth,YInHeight);

											#endregion        //End Modify

											sf.FormatFlags=StringFormatFlags.NoWrap;

											g.TextRenderingHint=this.setting.AxisY.FontQuality;

											g.DrawString(AxesXLabel,this.setting.AxisY.Font,Brushes.Black,AreaForAxes_X,sf);

											g.ResetTransform();
											
										}
									#endregion
								}
							}
						}
					#endregion	
				}
				else if(this.ShowStyle==PointShowStyle.Bar)
				{
					#region //draw Axes_Bar_X scaleLabel

						if(this.XLinesCount>0&&this.setting.AxisX.Visible)
						{
							int angleX=this.setting.AxisX.LabelAngle;

							for(int i=0;i<this.nRowCount;i++)
							{	
								if(!this.setting.CombinedTitle)
								{
									#region //Draw AxelXLabel all each Mini-bar 

										for(int j=0;j<this.nColumnCount;j++)
										{									
											string AxesXLabel=this.GetAxesXLabel(i,j);

											if(AxesXLabel=="[NoValue]")continue;

											if(!string.Equals(AxesXLabel,""))
											{
												int drawXrectLen=this.AxesXStringHeight/this.XLinesCount;

												int modY=0;
			                                 
												int realCol=this.ColsIndex[j];

												if(this.nColumnCount<=1)
												{
													modY=i%this.XLinesCount; 
												}
												else
												{
													modY=realCol%this.XLinesCount; 
												}
														
												float AxesPoint_X=PointsToDraw[i,j].X;

												float AxesPoint_Y=BoundArea.Height-(ChartArea.Y-this.AxesXExtendHeight)+modY*drawXrectLen+drawXrectLen/2f; //translate the Axes point afer RotateFlip

												g.TranslateTransform(AxesPoint_X,AxesPoint_Y);

												if(angleX!=0)
												{
													g.RotateTransform(angleX);
												}

												#region Modify codes at 2008-10-30 16:54:14@Simon	
													//RectangleF AreaForAxes_X=new RectangleF(-drawXrectLen/2f,-drawXrectLen/2f,drawXrectLen,drawXrectLen);

													int XInHeight=(int)g.MeasureString(AxesXLabel,this.setting.AxisX.Font).Height+6;
				
													int XInWidth=(int)g.MeasureString(AxesXLabel,this.setting.AxisX.Font).Width+4;	

													RectangleF AreaForAxes_X=new RectangleF(-XInWidth/2f,-XInHeight/2f,XInWidth,XInHeight);

												#endregion        //End Modify

												sf.FormatFlags=StringFormatFlags.NoWrap;	
										
												g.TextRenderingHint=this.setting.AxisX.FontQuality;

												g.DrawString(AxesXLabel,this.setting.AxisX.Font,Brushes.Black,AreaForAxes_X,sf);

												g.ResetTransform();
											}
		                                  
										}	
									#endregion						
								}
								else
								{
									#region //Only Draw First series Label  

										string AxesXLabel=this.GetAxesXLabel(i,0);

										if(AxesXLabel==""||AxesXLabel=="[NoValue]")continue;

										
										int drawXrectLen=this.AxesXStringHeight/this.XLinesCount;

										int modY=0;
			                            
										int realCol=this.ColsIndex[0];

										if(this.nColumnCount<=1)
										{
											modY=i%this.XLinesCount; 
										}
										else
										{
											modY=realCol%this.XLinesCount; 
										}	
										
										float AxesPoint_X=0f;

										for(int j=0;j<this.nColumnCount;j++)
										{
												AxesPoint_X+=PointsToDraw[i,j].X;
										}
										AxesPoint_X/=nColumnCount;		
									
										float AxesPoint_Y=BoundArea.Height-(ChartArea.Y-this.AxesXExtendHeight)+modY*drawXrectLen+drawXrectLen/2f; //translate the Axes point afer RotateFlip

										g.TranslateTransform(AxesPoint_X,AxesPoint_Y);

										if(angleX!=0)
										{
											g.RotateTransform(angleX);
										}

										#region Modify codes at 2008-10-30 16:54:14@Simon	
											//RectangleF AreaForAxes_X=new RectangleF(-drawXrectLen/2f,-drawXrectLen/2f,drawXrectLen,drawXrectLen);

											int XInHeight=(int)g.MeasureString(AxesXLabel,this.setting.AxisX.Font).Height+6;
				
											int XInWidth=(int)g.MeasureString(AxesXLabel,this.setting.AxisX.Font).Width+4;	

											RectangleF AreaForAxes_X=new RectangleF(-XInWidth/2f,-XInHeight/2f,XInWidth,XInHeight);

										#endregion        //End Modify

										sf.FormatFlags=StringFormatFlags.NoWrap;	
								
										g.TextRenderingHint=this.setting.AxisX.FontQuality;

										g.DrawString(AxesXLabel,this.setting.AxisX.Font,Brushes.Black,AreaForAxes_X,sf);

										g.ResetTransform();						
	                                  
										
									#endregion						

								}
							}
						}
					#endregion	
				}
				else
				{			  
					#region //draw Axes_PointOrLine_X scaleLabel

						if(this.XLinesCount>0&&this.setting.AxisX.Visible)
						{
							int angleX=this.setting.AxisX.LabelAngle;

							for(int i=0;i<this.nRowCount;i++)
							{		
								string AxesXLabel=this.GetAxesXLabel(i);
							
								if(!string.Equals(AxesXLabel,""))
								{
									int drawXrectLen=this.AxesXStringHeight/this.XLinesCount;

									int modY=i%this.XLinesCount; 
														
									float AxesPoint_X=PointsToDraw[i,0].X;

									float AxesPoint_Y=BoundArea.Height-(ChartArea.Y-this.AxesXExtendHeight)+modY*drawXrectLen+drawXrectLen/2f; //translate the Axes point afer RotateFlip

									g.TranslateTransform(AxesPoint_X,AxesPoint_Y);

									if(angleX!=0)
									{
										g.RotateTransform(angleX);
									}

									#region Modify codes at 2008-10-30 16:54:14@Simon	
										//RectangleF AreaForAxes_X=new RectangleF(-drawXrectLen/2f,-drawXrectLen/2f,drawXrectLen,drawXrectLen);

										int XInHeight=(int)g.MeasureString(AxesXLabel,this.setting.AxisX.Font).Height+4;
			
										int XInWidth=(int)g.MeasureString(AxesXLabel,this.setting.AxisX.Font).Width+4;	

										RectangleF AreaForAxes_X=new RectangleF(-XInWidth/2f,-XInHeight/2f,XInWidth,XInHeight);
									
									#endregion        //End Modify

									sf.FormatFlags=StringFormatFlags.NoWrap;	
								
									g.TextRenderingHint=this.setting.AxisX.FontQuality;

									g.DrawString(AxesXLabel,this.setting.AxisX.Font,Brushes.Black,AreaForAxes_X,sf);

									g.ResetTransform();
								}
							}
						}
					#endregion		
				}
				#endregion
				
				//draw Axes_DatatPoint scalelabel
				#region //Axes_DatatPoint scalelabel

					if(this.ShowStyle==PointShowStyle.HorizonBar)
					{
						#region // draw Axes_HorizonBar_X scalelabel

							if(this.XLinesCount>0&&this.setting.AxisX.Visible)
							{
								int angerX=this.setting.AxisX.LabelAngle;	
					
								for(int i=0;i<=XScaleCount;i++)
								{   
									int modX=i%this.XLinesCount;  //the flag declare whether draw in a line				
								
									float ScaleValue=0f;	

									string ScaleText=string.Empty;     //Added this code at 2008-11-27 9:44:04@Simon

									bool percent=this.IsAllPercent();

									float space=this.setting.AxisX.GridLineSpace;

									if(space>0)
									{
										if(percent)
										{
											ScaleValue=i*space*0.01f;

											ScaleText=string.Format("{0:0.0%}",ScaleValue);		
										}
										else
										{
											ScaleValue=i*space;

											ScaleText=ScaleValue.ToString();
										}
									}
									else
									{
										if(percent)
										{
											ScaleValue=i*everyheight/radiox;

											ScaleText=string.Format("{0:0.0%}",ScaleValue);	
										}
										else
										{
											ScaleValue=i*everyheight/radiox;

											ScaleText=string.Format("{0:0.0}", ScaleValue);

											if(ScaleText.EndsWith(".0"))ScaleText=ScaleText.Substring(0,ScaleText.Length-2);
										}
									}


									if(ScaleText=="0.0%")ScaleText="0";	
						
									int drawYrectLen=this.AxesXStringHeight/this.XLinesCount;    //both rectangle's width and height 
								            
									float AxesPoint_X=ChartArea.X+everyheight*i;

									float AxesPoint_Y=BoundArea.Height-ChartArea.Y+AxesXExtendHeight+modX*drawYrectLen+drawYrectLen/2f;
   
									//there Transform and rotate the scale by the angle 
									g.TranslateTransform(AxesPoint_X,AxesPoint_Y);

									if(angerX!=0)
									{
										g.RotateTransform(angerX);
									}
			                        

									int XInWidth=(int)g.MeasureString(ScaleText,this.setting.AxisX.Font).Width+4;	

									RectangleF AxesLabelYArea=new RectangleF(-XInWidth/2,-drawYrectLen/2,XInWidth,drawYrectLen);
							
									g.TextRenderingHint=this.setting.AxisX.FontQuality;

									g.DrawString(ScaleText,this.setting.AxisX.Font,Brushes.Black,AxesLabelYArea,sf); 

									g.ResetTransform();        //recover the original scale mode,Don,t forget this important step
								}
							}
						#endregion			
					}
					else
					{
						#region //draw Axes_Y scalelabel

							if(this.YLinesCount>0&&this.setting.AxisY.Visible)
							{
								int angerY=this.setting.AxisY.LabelAngle;	
				
								for(int i=0;i<=YScaleCount;i++)
								{   
									int modX=i%this.YLinesCount+1;  //the flag declare whether draw in a line						
											
									float ScaleValue=0f;	

									string ScaleText=string.Empty;     //Added this code at 2008-11-27 9:44:04@Simon
									
									bool percent=this.IsAllPercent();

									float space=this.setting.AxisY.GridLineSpace;

									if(space>0)
									{  
										if(percent)
										{
											ScaleValue=i*space*0.01f;

											ScaleText=string.Format("{0:0.0%}",ScaleValue);		
										}
										else
										{
											ScaleValue=i*space;

											ScaleText=ScaleValue.ToString();
										}
									}
									else
									{
										if(percent)
										{
											ScaleValue=(i*everyheight)/radioy;	

											ScaleText=string.Format("{0:0.0%}",ScaleValue);		
										}
										else
										{
											ScaleValue=(i*everyheight)/radioy;	

											ScaleText=string.Format("{0:0.0}", ScaleValue);

											if(ScaleText.EndsWith(".0"))ScaleText=ScaleText.Substring(0,ScaleText.Length-2);	
										}
										
									}
																
									
									if(ScaleText=="0.0%")ScaleText="0";

									int drawYrectLen=AxesYStringWidth/this.YLinesCount;    //both rectangle's width and height      
			              
									float AxesPoint_X=ChartArea.X-this.AxesYExtendWidth-modX*drawYrectLen+drawYrectLen/2f;

									float AxesPoint_Y=BoundArea.Height-(ChartArea.Y+everyheight*i);
			                    
									//there Transform and rotate the scale by the angle 
									g.TranslateTransform(AxesPoint_X,AxesPoint_Y);

									if(angerY!=0)
									{
										g.RotateTransform(angerY);
									}

									RectangleF AxesLabelYArea=new RectangleF(-drawYrectLen/2,-drawYrectLen/2,drawYrectLen ,drawYrectLen);

									g.TextRenderingHint=this.setting.AxisY.FontQuality;   //set text quality

									g.DrawString(ScaleText,this.setting.AxisY.Font,Brushes.Black,AxesLabelYArea,sf); 

									g.ResetTransform();        //recover the original scale mode,Don,t forget this important step
								}
							}
						#endregion			
					}
				#endregion				

				//draw Axes_X Title
				#region //draw Axes_X Title		
	
					if(this.setting.AxisX.Title!=null && this.setting.AxisX.Title.Length>0 && this.setting.AxisX.Visible)
					{
						sf.Alignment=this.setting.AxisX.TitleAlignment;	

						sf.LineAlignment=StringAlignment.Far;

						RectangleF RectXT=new RectangleF(ChartArea.X,
												   		BoundArea.Height-(ChartArea.Y-this.AxesXExtendHeight-this.AxesXStringHeight),
														ChartArea.Width,
														AxisXTitleHeight
														);

						g.TextRenderingHint=this.setting.AxisX.FontQuality;
	                     
						Brush axesXBrush=new SolidBrush(this.setting.AxisX.TitleColor);

						g.DrawString(this.setting.AxisX.Title,this.setting.AxisX.TitleFont,axesXBrush,RectXT,sf);
					}			
				#endregion
			
				//draw Axes_Y Title
				#region //draw Axes_Y Title

					if(this.setting.AxisY.Title!=null && this.setting.AxisY.Title.Length>0 && this.setting.AxisY.Visible)
					{  
						g.TranslateTransform(0,BoundArea.Height-ChartArea.Y);

						g.RotateTransform(270);

						RectangleF RectYT=new RectangleF(0,
														this.RectSpace.Width,
														ChartArea.Height,
														AxisYTitleHeight
														);

						g.SmoothingMode=SmoothingMode.HighQuality;

						sf.Alignment=this.setting.AxisY.TitleAlignment;

						sf.LineAlignment=StringAlignment.Near;

						g.TextRenderingHint=this.setting.AxisY.FontQuality;

						Brush axesYBrush=new SolidBrush(this.setting.AxisY.TitleColor);

						g.DrawString(this.setting.AxisY.Title,this.setting.AxisY.TitleFont,axesYBrush,RectYT,sf);

						g.ResetTransform();
					}
				#endregion
			}
			
		#endregion		

		#region Helpful and Important Sub Functions for Drawing  	

			#region Translate FlippedPoints in ChartArea
			/// <summary>
			///Translate all point in a coordinates into point which was in the reversed coordinates
			/// </summary>
			private void TransPointAll(Rectangle BoundArea)
			{
				for(int i=0;i<this.nRowCount;i++)
				{
					for(int k=0;k<this.nColumnCount;k++)
					{
						PointsToDraw[i,k]=this.TransPoint(PointsToDraw[i,k],BoundArea);
					}
				}
			
			}		
           
			/// <summary>
			/// sub functions of TransPointAll
			///Translate point in a coordinates into point reversed coordinates
			/// </summary>
			protected PointF TransPoint(PointF Oldpoint,Rectangle DrawArea)
			{
				PointF ReversedPoint=new PointF();

				ReversedPoint.X=Oldpoint.X;

				ReversedPoint.Y=DrawArea.Height-Oldpoint.Y;

				return ReversedPoint;
			}
			#endregion

			#region GetAxesLabel
		     
				/// <summary>
				///Sub function of GetAxesLabel,define whether all the groupvalue in all series are equal 
				/// </summary>					
				private bool AllLabelEquals()
				{
					string sname=string.Empty;	
				
                    string AxesXLabel=string.Empty;

					for(int i=0;i<this.nRowCount;i++)
					{
						AxesXLabel=GetCell(i,0).Name;

						for(int j=1;j<this.nColumnCount;j++)
						{
							if(GetCell(i,j).Name!=AxesXLabel)return false;							
						}

					}

					return true;
				}

				/// <summary>
				///get groupvalue-Label that show be shown in the Axes-X of PointChart/LineChart 
				/// </summary>
				/// <param name="row"></param>				
				protected string GetAxesXLabel(int row)
				{	
					if(this.setting.CombinedTitle)return GetCell(row,0).Name;

					string sname=string.Empty;

					string AxesXLabel=string.Empty;

					for(int j=0;j<this.nColumnCount;j++)
					{
						sname=GetCell(row,j).Name;		
			
						sname=(sname==null||sname=="[NoValue]")?"":sname;

						AxesXLabel=AxesXLabel+"\n"+sname;
					}

					sname=GetCell(row,0).Name;		
			
					sname=(sname==null||sname=="[NoValue]")?"":sname;

					AxesXLabel=AxesXLabel.Trim(new char[]{'\n'});

					if(this.AllLabelEquals())AxesXLabel=sname;

					return AxesXLabel;
				}
		       
				/// <summary>
				///get groupvalue-Label that show be shown in the Axes of BarChart 
				/// </summary>
				/// <param name="row"></param>	
				/// <param name="col"></param>	
				protected string GetAxesXLabel(int row,int col)
				{
					string AxesXTitle="";
				  
					string sname=GetCell(row,col).Name==null?"":GetCell(row,col).Name.Trim();
						
					AxesXTitle=sname;
					
					return AxesXTitle;
				}	
				
			#endregion

	    	#region Functions For  BarWidth Setting
				/// <summary>
				///Calculate the bar width by custom width in series and the limited Barchart-width
				/// </summary>
				/// <param name="EverWidth"></param>	
				/// <param name="TotalWidth"></param>	
				protected bool FitCustomWidth(float EverWidth,out float TotalWidth)
				{  
					TotalWidth =0f;
					
					foreach(int realCol in this.ColsIndex)
					{
						Series series=this.AllSeries[realCol];

						TotalWidth+=series.Width;

						if(series.Width<=1.2f)
						{ 
							TotalWidth=EverWidth*2/3f;

							return false;
						}
					}	
				    
					if(TotalWidth>=EverWidth-1.2f)
					{
						TotalWidth=EverWidth*2/3f;

						return false;
					}
					return true;
				}
				/// <summary>
				///Calculate the limited Barchart-width
				/// </summary>
				/// <param name="EverWidth"></param>	
				/// <param name="TotalWidth"></param>	
				protected float AutoWidth(float EverWidth)
				{
					return EverWidth*2/(3f*this.nColumnCount);           
				}	
				
			#endregion

			#region SubFunctions of DrawString
		         
				/// <summary>
				/// sub functions of DrawPointData
				/// differ the selected series and unselected series and then drawing then in different color				
				/// </summary>			
				protected void DrawDataInRect(Graphics g,Rectangle BoundArea,RectangleF ChartArea,int i,int k,Color DrawColor)
				{
					#region Draw Data In Rect	                         

						int realCol=this.ColsIndex[k];				
					
						if(!this.SeriesLabel(realCol).Visible)return;

                        IChartCell cell=this.GetCell(i,k);

						StringFormat sf=new StringFormat();

						sf.LineAlignment=StringAlignment.Center;

		    			sf.Alignment=StringAlignment.Center;

						g.TextRenderingHint=this.SeriesLabel(realCol).FontQuality;

                        float cellPercentValue = Convert.ToSingle(cell.PercentText);

						string valueToDraw=string.Empty;

						if(this.IsPercentSeries(realCol))
						{
                            valueToDraw = string.Format("{0:0.0%}", cellPercentValue);

                            #region Percent Format

                            string percentFormat = this.SeriesLabel(realCol).DisplayFormat;

                            if (percentFormat != string.Empty)
                            {
                                System.Text.StringBuilder sb = new System.Text.StringBuilder(percentFormat);

                                sb = sb.Replace("%", valueToDraw);

                                sb = sb.Replace("#", cell.Frequence.ToString());

                                sb = sb.Replace(@"\n", "\n");

                                sb = sb.Replace("[LineBreak]", "\n");

                                valueToDraw = sb.ToString();
                            }
                            #endregion
								
						}
						else
						{
                            valueToDraw = cell.DataPoint.ToString();
						}                     
					    
						Font SeriesFont=SeriesLabel(realCol).Font;

						SizeF szfTextY = this.CalculateTextSize(g,realCol);			     			
							
						PointF StartPoint=this.StartPointForText(g,BoundArea,ChartArea,szfTextY,i,k);

						RectangleF AreaForShowPoint=new RectangleF(StartPoint,szfTextY);
	                   
						#region Modify codes at 2009-4-17 9:54:09@Simon

						Color backColor=this.SeriesLabel(realCol).BackColor;				 

						if(backColor==Color.Empty)
						{
							g.FillRectangle(Brushes.White,AreaForShowPoint);	// fill background color
						}
						else
						{
							g.FillRectangle(new SolidBrush(backColor),AreaForShowPoint);	// fill background color
						}

						if(realCol == setting.SelectedSeriesIndex)	// highlight selected series
						{
							g.DrawRectangle(new Pen(DrawColor),AreaForShowPoint.X,AreaForShowPoint.Y,AreaForShowPoint.Width,AreaForShowPoint.Height);
						} 
						else
						{	
							Color borderColor=this.SeriesLabel(realCol).RectBorderColor;

							if(this.SeriesLabel(realCol).ShowBorders)
							{
								if(borderColor==Color.Empty)
								{
									g.DrawRectangle(new Pen(DrawColor),AreaForShowPoint.X,AreaForShowPoint.Y,AreaForShowPoint.Width,AreaForShowPoint.Height);
								}
								else
								{
									g.DrawRectangle(new Pen(borderColor),AreaForShowPoint.X,AreaForShowPoint.Y,AreaForShowPoint.Width,AreaForShowPoint.Height);
								}
							}
						}
						#endregion        //End Modify
						
						Color foreColor=this.SeriesLabel(realCol).TextColor;

						if(foreColor==Color.Empty)
						{
							foreColor=PointColor[realCol];
						}

						g.DrawString(valueToDraw,SeriesFont,new SolidBrush(foreColor),AreaForShowPoint,sf);

					#endregion
											
				}
	
				
		        /// <summary>
		        ///Sub functions of DrawDataInRect
				///Calcuate the started-drawing point for each series label and drawing the connector 
				/// </summary>			
				protected PointF StartPointForText(Graphics g,Rectangle BoundArea,RectangleF ChartArea,SizeF szfTextY,int i,int k)
				{		
					float ax=0f,ay=0f;					

					float BarHeight=GetCell(i,k).DataPoint*radioy;		
				
					float BarXWidth=GetCell(i,k).DataPoint*radiox;   //Added this code at 2008-12-9 13:55:18@Simon		

					int realCol=this.ColsIndex[k];

					int connectLen=SeriesLabel(realCol).LengthConnector;	

					float angle=SeriesLabel(realCol).Angle*(-1);			
					
					Color DrawColor=Color.Empty;	
			
					if(this.AllSeries[realCol].Color==Color.Empty)
					{				
						DrawColor=ColorManager.GetDeepColor(PointColor[realCol]);
					}
					else
					{
						if(this.AllSeries[realCol].BorderColor!=Color.Empty)
						{
							DrawColor=this.AllSeries[realCol].BorderColor;
						}
						else
						{
							DrawColor=ColorManager.BorderColor;
						}	
					}

					Pen Drawpen=new Pen(DrawColor);

					#region draw  Chart Connector		        	
							if(ShowStyle==PointShowStyle.Bar)
							{ 
								ax=PointsToDraw[i,k].X-szfTextY.Width/2f;

								ay=PointsToDraw[i,k].Y-connectLen-szfTextY.Height;	
			
								#region Draw Bar Connector

								if(this.SeriesLabel(realCol).Position==ChartTextPosition.Outside)
								{
									ax=PointsToDraw[i,k].X-szfTextY.Width/2f;

									ay=PointsToDraw[i,k].Y-connectLen-szfTextY.Height;
										
									if(this.SeriesLabel(realCol).VisibleConnector)
									{
										g.DrawLine(Drawpen,PointsToDraw[i,k].X,PointsToDraw[i,k].Y,PointsToDraw[i,k].X,ay+szfTextY.Height);
									}
								}
								else if(SeriesLabel(realCol).Position==ChartTextPosition.Inside)
								{
									ax=PointsToDraw[i,k].X-szfTextY.Width/2f;

									ay=PointsToDraw[i,k].Y+BarHeight/2f-szfTextY.Height/2f;	

									float point_Y=BoundArea.Height-ChartArea.Y-1;

									if(ay+szfTextY.Height>point_Y)
									{
										ay=point_Y-szfTextY.Height;
									}
								}
								else if(SeriesLabel(realCol).Position==ChartTextPosition.Center)
								{
									ax=PointsToDraw[i,k].X-szfTextY.Width/2f;

									ay=ChartArea.Height/2f-szfTextY.Height/2f;
			
									float point_Y=BoundArea.Height-ChartArea.Y-1;

									if(ay+szfTextY.Height>BoundArea.Height-ChartArea.Y-1)
									{
										ay=point_Y-szfTextY.Height;
									}
								}
								#endregion
							}
							else if(ShowStyle==PointShowStyle.HorizonBar)
							{									
								#region Draw HorizonBar Connector

								if(SeriesLabel(realCol).Position==ChartTextPosition.Outside)
								{							

									ax=PointsToDraw[i,k].X+connectLen;

									ay=PointsToDraw[i,k].Y-szfTextY.Height/2f;	
									
									if(SeriesLabel(realCol).VisibleConnector)
									{
										g.DrawLine(Drawpen,PointsToDraw[i,k].X,PointsToDraw[i,k].Y,ax,PointsToDraw[i,k].Y);
									}
								}
								else if(SeriesLabel(realCol).Position==ChartTextPosition.Inside)
								{
									ax=PointsToDraw[i,k].X-BarXWidth/2f-szfTextY.Width/2f;

									ay=PointsToDraw[i,k].Y-szfTextY.Height/2f;		

									float point_X=ChartArea.X+1;

									if(ax<point_X)
									{
										ax=point_X;
									}
								}
								else if(SeriesLabel(realCol).Position==ChartTextPosition.Center)
								{
									ax=ChartArea.X+ChartArea.Width/2f-szfTextY.Width/2f;

									ay=PointsToDraw[i,k].Y-szfTextY.Height/2f;			

									float point_X=ChartArea.X+1;

									if(ax<point_X)
									{
										ax=point_X;
									}
								}
								#endregion
							}
							else 
							{
								//draw Line/Point Chart Connector
								PointF pointArc=ChartHelper.MovePoint(angle,PointsToDraw[i,k],connectLen);
							
								if(SeriesLabel(realCol).VisibleConnector)
								{
									g.DrawLine(Drawpen,PointsToDraw[i,k],pointArc);
								}
								RectangleF rect=ChartHelper.GetRect(angle,pointArc,szfTextY,ChartTextPosition.Outside);

								ax=rect.X;

								ay=rect.Y;

								rect.Offset(5,5);

								int index=i*this.nColumnCount+k;

								this.ClickAreas[index]=rect;
							}
							#endregion

					return new PointF(ax,ay);
				}	 
				
						
				/// <summary>
				//Draw all serieslabel in the limit chartArea
				/// </summary>			
		        private void DrawPointData(Graphics g,Rectangle BoundArea,RectangleF ChartArea)
				{
					#region Draw PointData Text in a retangle	

						//draw PointData text in a Rectangle Area
						int SelectIndex=this.Setting.SelectedSeriesIndex;		 
			   
						Color DrawColor=Color.Empty;		
			 	
						for(int i=0;i<this.nRowCount;i++)
						{						        
							#region Unselected Chart
								for(int k=0;k<this.nColumnCount;k++)
								{
									int realCol=this.ColsIndex[k];

									if(GetCell(i,k).Name=="[NoValue]")continue;
				                 
									if(GetCell(i,k).DataPoint<1e-10&&!SeriesLabel(realCol).ShowZero)continue;      //To Hide 0 at 2008-12-11 8:56:20@Simon
											
									if(realCol==SelectIndex||realCol==SelectIndex-this.AllSeries.Count)continue;

									DrawColor=Color.Empty;	
					
									if(this.AllSeries[realCol].Color==Color.Empty)
									{				
										DrawColor=ColorManager.GetDeepColor(PointColor[realCol]);
									}
									else
									{
										if(this.AllSeries[realCol].BorderColor!=Color.Empty)
										{
											DrawColor=this.AllSeries[realCol].BorderColor;
										}
										else
										{
											DrawColor=ColorManager.BorderColor;
										}	
									}

									this.DrawDataInRect(g,BoundArea,ChartArea,i,k,DrawColor);
								}
							#endregion

							#region Draw select Serialabels

								for(int k=0;k<this.nColumnCount;k++)
								{	
									int realCol=this.ColsIndex[k];

									if(GetCell(i,k).Name=="[NoValue]")continue;

									if(GetCell(i,k).DataPoint<1e-10&&!this.SeriesLabel(realCol).ShowZero)continue;      //To Hide 0 at 2008-12-11 8:56:20@Simon
								
									if(realCol!=SelectIndex&&realCol!=SelectIndex-this.AllSeries.Count)continue;					
									
									if(SelectIndex>=this.AllSeries.Count)
									{						
										DrawColor=Color.Empty;	
					
										if(this.AllSeries[realCol].Color==Color.Empty)
										{				
											DrawColor=ColorManager.GetDeepColor(PointColor[realCol]);
										}
										else
										{
											if(this.AllSeries[realCol].BorderColor!=Color.Empty)
											{
												DrawColor=this.AllSeries[realCol].BorderColor;
											}
											else
											{
												DrawColor=ColorManager.BorderColor;
											}	
										}
									}	
									else
									{
										DrawColor=Color.Red;
									}
								
									this.DrawDataInRect(g,BoundArea,ChartArea,i,k,DrawColor);	                 
									
								}
							#endregion
						}	
					#endregion
				}
			#endregion
		#endregion
	}
	#endregion

	//08-04-2008@Simon  Modified
	#region public class PointChart : AxesChart
	[Serializable]
	public class PointChart :AxesChart
	{
		public PointChart() : base()
		{
			this.ShowStyle=PointShowStyle.Point;	
		}
	}
	#endregion

	//08-04-2008@Simon  Modified
	#region public class LineChart :AxesChart
	public class LineChart : AxesChart
	{
		public LineChart()
		{
			this.ShowStyle=PointShowStyle.Line;
		}
		/// <summary> 
		/// override base DrawChartStyle from AxesChart,begin drawing LineChart in alimited area
		/// </summary>
		
		protected override void DrawChartStyle(Graphics g,Rectangle BoundArea, RectangleF ChartArea)
		{
			base.DrawChartStyle (g,BoundArea, ChartArea);	
	 
			for(int i=1;i<this.nRowCount;i++)
			{
				for(int j=0;j<this.nColumnCount;j++)
				{ 	
                    if(GetCell(i,j).Name=="[NoValue]")continue;

					int realcol=this.ColsIndex[j];
                    
					g.DrawLine(new Pen(PointColor[realcol]),PointsToDraw[i-1,j],PointsToDraw[i,j]);
				}
			} 
		}		
	}
	#endregion	

	//08-06-2008@Simon
	#region public class BarChart:AxesChart
	public class BarChart:AxesChart
	{
		public BarChart()
		{
			this.ShowStyle=PointShowStyle.Bar;
		}	
		/// <summary> 
		/// override base DrawChartStyle from AxesChart,begin drawing LineChart in alimited area
		/// </summary>
		protected override void DrawChartStyle(Graphics g,Rectangle BoundArea, RectangleF ChartArea)
		{
			#region DrawBar

			float ScaleXValue=ChartArea.X+XEveryWidth/2f;             

            float TotalEveryWidth=0;

			bool b_CustomWidth=this.FitCustomWidth(XEveryWidth,out TotalEveryWidth);

			float MiniXBarSpace=0f;

			if(nRowCount+this.nColumnCount<=0)return;				

			//save and draw points
			for(int i=0;i<this.nRowCount;i++)
			{   
				float MiniXStart=ScaleXValue-TotalEveryWidth/2f;
				
				for(int j=0;j<this.nColumnCount;j++)
				{
					int realcol=this.ColsIndex[j];

					IChartCell cell=this.GetCell(i,j);

					float CellToY=0;					
				
					CellToY=ChartArea.Y+cell.DataPoint*radioy;
				
					if(b_CustomWidth)
					{
						MiniXBarSpace=this.AllSeries[realcol].Width;
					}
					else
					{
                       MiniXBarSpace=this.AutoWidth(XEveryWidth);
					}

					PointsToDraw[i,j]=new PointF(MiniXStart+(MiniXBarSpace-1.2f)/2f,CellToY);	                  
					
					//draw Points	
					
					RectangleF BarRect=new RectangleF(MiniXStart,ChartArea.Y,MiniXBarSpace-1.2f,CellToY-ChartArea.Y);
					
					if(BarRect.Width*BarRect.Height<=0)
					{ 
						MiniXStart=MiniXStart+MiniXBarSpace;
						
						continue;
					}
					Color LightColor=ColorManager.GetLightColor(PointColor[realcol]);

					if(this.AllSeries[realcol].Color!=Color.Empty)
					{
						LightColor=PointColor[realcol];
					}

					LinearGradientBrush BarBrush=new LinearGradientBrush(BarRect,LightColor,PointColor[realcol],LinearGradientMode.Horizontal);
				
					g.FillRectangle(BarBrush,BarRect);

					Color DrawColor=Color.Empty;
			
					if(this.AllSeries[realcol].Color==Color.Empty)
					{				
						DrawColor=ColorManager.GetDeepColor(PointColor[realcol]);
					}
					else
					{
						if(this.AllSeries[realcol].BorderColor!=Color.Empty)
						{
							DrawColor=this.AllSeries[realcol].BorderColor;
						}
						else
						{
							DrawColor=ColorManager.BorderColor;
						}	
					}

					Pen BarPen=new Pen(DrawColor,1);	
				
					#region DrawBar At three-Line  //2009-8-19 16:44:06@Simon modify this Code
//					  g.DrawRectangle(BarPen,BarRect.X,BarRect.Y,BarRect.Width,BarRect.Height);
					   g.DrawLine(BarPen,BarRect.X,BarRect.Y,BarRect.X,BarRect.Bottom);

					   g.DrawLine(BarPen,BarRect.X,BarRect.Bottom,BarRect.Right,BarRect.Bottom);

					   g.DrawLine(BarPen,BarRect.Right,BarRect.Bottom,BarRect.Right,BarRect.Y);

					#endregion

					BarBrush.Dispose();

					BarPen.Dispose();

					MiniXStart=MiniXStart+MiniXBarSpace;

					//Added this code at 2008-11-11 9:24:18@Simon
					//storing data For Creating click area 
					PointF tranpoint=this.TransPoint(PointsToDraw[i,j],BoundArea);

					RectangleF tranrect=new RectangleF(BarRect.X+5f,tranpoint.Y+5f,BarRect.Width,BarRect.Height);

					int index=i*this.nColumnCount+j;

					this.ClickAreas[index]=tranrect;
					//end add
					
				}

				ScaleXValue=ScaleXValue+XEveryWidth;
			}
			#endregion
		}	
	}
	#endregion

	//Added this code at 2008-12-29 10:23:30@Simon
	#region public class Bar3DChart:BarChart
	public class Bar3DChart:BarChart
	{
		/// <summary> 
		/// override base DrawChartStyle from BarChart,begin drawing LineChart in alimited area
		/// </summary>
		protected override void DrawChartStyle(Graphics g, Rectangle BoundArea, RectangleF ChartArea)
		{
			#region DrawBar

				float ScaleXValue=ChartArea.X+XEveryWidth/2f;             

				float TotalEveryWidth=0;

				bool b_CustomWidth=this.FitCustomWidth(XEveryWidth,out TotalEveryWidth);

				float MiniXBarSpace=0f;

				if(nRowCount+this.nColumnCount<=0)return;	

				//save and draw points
				for(int i=0;i<this.nRowCount;i++)
				{   
					float MiniXStart=ScaleXValue-TotalEveryWidth/2f;
					
					for(int j=0;j<this.nColumnCount;j++)
					{
						int realcol=this.ColsIndex[j];

						IChartCell cell=this.GetCell(i,j);

						float CellToY=0;					
					
						CellToY=ChartArea.Y+cell.DataPoint*radioy;
					
						if(b_CustomWidth)
						{
							MiniXBarSpace=this.AllSeries[realcol].Width;
						}
						else
						{
							MiniXBarSpace=this.AutoWidth(XEveryWidth);
						}

						PointsToDraw[i,j]=new PointF(MiniXStart+(MiniXBarSpace-1.2f)/2f,CellToY);	                  
						
						//draw Points	
						
						RectangleF BarRect=new RectangleF(MiniXStart,ChartArea.Y,MiniXBarSpace-1.2f,CellToY-ChartArea.Y);
						
						if(BarRect.Width*BarRect.Height<=0)
						{ 
							MiniXStart=MiniXStart+MiniXBarSpace;
						
							continue;
						}
						Color LightColor=ColorManager.GetLightColor(PointColor[realcol]);

						if(this.AllSeries[realcol].Color!=Color.Empty)
						{						
							LightColor=PointColor[realcol];
						}
						
						Color DrawColor=Color.Empty;	
			
						if(this.AllSeries[realcol].Color==Color.Empty)
						{				
							DrawColor=ColorManager.GetDeepColor(PointColor[realcol]);
						}
						else
						{
							if(this.AllSeries[realcol].BorderColor!=Color.Empty)
							{
								DrawColor=this.AllSeries[realcol].BorderColor;
							}
							else
							{
								DrawColor=ColorManager.BorderColor;
							}	
						}
						Pen BarPen=new Pen(DrawColor,1);

						LinearGradientBrush BarBrush=new LinearGradientBrush(BarRect,LightColor,PointColor[realcol],LinearGradientMode.Horizontal);
					
						g.FillRectangle(BarBrush,BarRect);	
			
						#region DrawBar At three-Line  //2009-8-19 16:44:06@Simon modify this Code
							//					  g.DrawRectangle(BarPen,BarRect.X,BarRect.Y,BarRect.Width,BarRect.Height);
							g.DrawLine(BarPen,BarRect.X,BarRect.Y,BarRect.X,BarRect.Bottom);

							g.DrawLine(BarPen,BarRect.X,BarRect.Bottom,BarRect.Right,BarRect.Bottom);

							g.DrawLine(BarPen,BarRect.Right,BarRect.Bottom,BarRect.Right,BarRect.Y);

						#endregion
						
						BarBrush.Dispose();
						
						#region DrawBar3D 
						float lenX=0f,lenY=0f;			

						if(BarRect.Width>=2)
						{
							GraphicsPath path3d=new GraphicsPath();

							PointF[] pathpoints=new PointF[5];				 				

							float BarLeft=BarRect.X;

							float BarRight=BarRect.X+BarRect.Width;		
				 
							float BarTop=BarRect.Y+BarRect.Height;

							float BarBottom=BarRect.Y;

							#region Modify codes at 2008-12-31 17:23:10@Simon
	//						if(BarRect.Width<16)
	//						{
	//							lenX=BarRect.Width/2f;												
	//						}
	//						else 
	//						{ 
	//							lenX=BarRect.Width/5f;
	//						}	
	//						
	//						lenY=lenX/2;
	//					    		
	//						pathpoints[0]=new PointF(BarRight,BarTop);
	//						if(BarTop-lenY>=BarBottom)
	//						{
	//							pathpoints[1]=new PointF(BarRight+lenX,BarTop-lenY);
	//						}
	//						else
	//						{
	//							pathpoints[1]=new PointF(BarRight+lenX,BarBottom);
	//						}
	//						pathpoints[2]=new PointF(BarRight+lenX,BarBottom);
	//						pathpoints[3]=new PointF(BarRight,BarBottom);
	//						pathpoints[4]=new PointF(BarRight,BarTop);
	//
	//						path3d.AddLines(pathpoints);
	//						g.FillPath(new SolidBrush(PointColor[realcol]),path3d);
	//						g.DrawLines(BarPen,pathpoints);						

							#endregion        //End Modify

							#region origin codes for DrawBar3D

								if(BarRect.Width<30)
								{
									lenX=5f;

									lenY=6f;
								}
								else
								{
									float ChartTop=ChartArea.Y+ChartArea.Height;

									float MaxDiff=ChartArea.Height+(BoundArea.Height-ChartTop)/2-BarRect.Height;

									if(MaxDiff<0)MaxDiff=-MaxDiff;
									
									lenX=lenY=Math.Min(MaxDiff,BarRect.Width/5);	
												
								}	
					
								pathpoints[0]=new PointF(BarLeft,BarTop);

								pathpoints[1]=new PointF(BarLeft+lenX,BarTop+lenY);

								pathpoints[2]=new PointF(BarRight+lenX,BarTop+lenY);

								pathpoints[3]=new PointF(BarRight,BarTop);

								pathpoints[4]=new PointF(BarLeft,BarTop);

								path3d.AddLines(pathpoints);

								g.FillPath(new SolidBrush(PointColor[realcol]),path3d);

								g.DrawLines(BarPen,pathpoints);

								path3d.Reset();

								path3d=new GraphicsPath();

								pathpoints[0]=new PointF(BarRight,BarTop);

								pathpoints[1]=new PointF(BarRight+lenX,BarTop+lenY);

								pathpoints[2]=new PointF(BarRight+lenX,BarBottom+lenY);

								pathpoints[3]=new PointF(BarRight,BarBottom);

								pathpoints[4]=new PointF(BarRight,BarTop);

								path3d.AddLines(pathpoints);

								g.FillPath(new SolidBrush(PointColor[realcol]),path3d);

								g.DrawLines(BarPen,pathpoints);

							#endregion

						}
						#endregion                  				
						
						BarPen.Dispose();

						MiniXStart=MiniXStart+MiniXBarSpace;

						//Added this code at 2008-11-11 9:24:18@Simon
						//storing data For Creating click area 
						PointF tranpoint=this.TransPoint(PointsToDraw[i,j],BoundArea);

						RectangleF tranrect=new RectangleF(BarRect.X+5f,tranpoint.Y+5f-lenY,BarRect.Width+lenX,BarRect.Height+lenY);
						
						int index=i*this.nColumnCount+j;

						this.ClickAreas[index]=tranrect;
						//end add
						
					}
					ScaleXValue=ScaleXValue+XEveryWidth;
				}
			#endregion
		}
	}

	#endregion

	//Added this code at 2008-12-9 13:08:07@Simon
	#region public class HorizonBarChart:AxesChart
	public class HorizonBarChart:AxesChart
	{
		public HorizonBarChart()
		{
			this.ShowStyle=PointShowStyle.HorizonBar;
		}	
 
		/// <summary> 
		/// override base SetCellValue from ChartBase,Reverse the orders of displaying groupvalues and series-labels
		/// </summary>
		protected override void SetCellValue(GroupInfoCollection groupInfos)
		{
			#region Set Values

			int col = 0,row = 0;

			foreach(GroupInfo groupInfo in groupInfos)
			{
				row = 0;

                for(int index=groupInfo.GroupResults.Count-1;index>=0;index--)
				{
				    GroupResult result=groupInfo.GroupResults[index];
				
					if(row>=this.nRowCount)break;

					IChartCell cell = this.GetCell(row,col);

					cell.DataPoint = result.RowIndicators.Count;

                    cell.Frequence = result.RowIndicators.Count;

					if(result.GroupValue==null||result.GroupValue is System.DBNull)
					{
						cell.Name="[NULL]";
					}
					else
					{
						cell.Name = result.GroupValue.ToString();		
					}

					result.RowIndicators.CopyTo(cell.RowIndicators);  //Added this code at 2008-11-11 8:53:39@Simon	

					if(cell.DataPoint>0)
					{
						float percent=cell.DataPoint/(float)nTotals;

						cell.PercentText=percent.ToString();
					}
					else
					{
                        cell.PercentText = "0";
					}

					row++;
				}

				col++;
			}
			for(int colIndex = 0; colIndex <this.nColumnCount; colIndex++)
			{
				int SeriesIndex=this.ColsIndex[colIndex];

				Series series = AllSeries[SeriesIndex];

				if(series.FieldShowName)
				{
					for(int rowindex=0;rowindex<this.nRowCount; rowindex++)
					{
						IChartCell cell = this.GetCell(rowindex,colIndex);	
					
						if(cell.Name=="[NoValue]")continue;

						cell.Name = series.Name;		
					}
				}					
			}
			#endregion
		}

		/// <summary> 
		/// override base DrawChartStyle from AxesChart,begin drawing HorizonChart in alimited area
		/// </summary>  
		protected override void DrawChartStyle(Graphics g,Rectangle BoundArea, RectangleF ChartArea)
		{
			#region DrawHorizonBarChart
			float ScaleYValue=ChartArea.Y+YEveryWidth/2f;

			int Xcount=XScaleCount;

			int Ycount=this.nRowCount;			

			if(nRowCount+this.nColumnCount<=0)return;

			float  MiniYBarSpace=(2*YEveryWidth)/(3f*nColumnCount);

			if(MiniYBarSpace-1.2<=0)return;

			float YTotalEveryWidth=0f;

			bool b_CustomWidth=this.FitCustomWidth(YEveryWidth,out YTotalEveryWidth);
		
			//save and draw points
			for(int i=0;i<this.nRowCount;i++)
			{   
				float MiniYStart=ScaleYValue-YTotalEveryWidth/2f;

				for(int j=0;j<this.nColumnCount;j++)
				{
					int realcol=this.ColsIndex[j];

					IChartCell cell=this.GetCell(i,j);

					float CellToX=0;
					
					CellToX=ChartArea.X+cell.DataPoint*radiox;					

					if(b_CustomWidth)
					{
						MiniYBarSpace=this.AllSeries[j].Width;
					}
					else
					{
						MiniYBarSpace=this.AutoWidth(YEveryWidth);
					}					
				
					PointsToDraw[i,j]=new PointF(CellToX,MiniYStart+MiniYBarSpace/2f);	 
                 
					//draw Points					
					RectangleF BarRect=new RectangleF(ChartArea.X,MiniYStart,CellToX-ChartArea.X,MiniYBarSpace-1.2f);

					if(BarRect.Width*BarRect.Height==0)
					{ 
						MiniYStart=MiniYStart+MiniYBarSpace;

						continue;
					}
					Color LightColor=ColorManager.GetLightColor(PointColor[realcol]);

					if(this.AllSeries[realcol].Color!=Color.Empty)
					{
						LightColor=PointColor[realcol];

					}
					LinearGradientBrush BarBrush=new LinearGradientBrush(BarRect,LightColor,PointColor[realcol],LinearGradientMode.Vertical);
					
					g.FillRectangle(BarBrush,BarRect);				

					Color DrawColor=Color.Empty;	
		
					if(this.AllSeries[realcol].Color==Color.Empty)
					{				
						DrawColor=ColorManager.GetDeepColor(PointColor[realcol]);
					}
					else
					{
						if(this.AllSeries[realcol].BorderColor!=Color.Empty)
						{
							DrawColor=this.AllSeries[realcol].BorderColor;
						}
						else
						{
							DrawColor=ColorManager.BorderColor;
						}	
					}
					Pen BarPen=new Pen(DrawColor,1);

					#region DrawBar At three-Line  //2009-8-19 16:44:06@Simon modify this Code
						//  g.DrawRectangle(BarPen,BarRect.X,BarRect.Y,BarRect.Width,BarRect.Height);

						g.DrawLine(BarPen,BarRect.X,BarRect.Bottom,BarRect.Right,BarRect.Bottom);

						g.DrawLine(BarPen,BarRect.Right,BarRect.Bottom,BarRect.Right,BarRect.Y);

						g.DrawLine(BarPen,BarRect.Right,BarRect.Y,BarRect.X,BarRect.Y);

					#endregion

					BarBrush.Dispose();

					BarPen.Dispose();

					MiniYStart=MiniYStart+MiniYBarSpace;

					//Added this code at 2008-11-11 9:24:18@Simon
					//storing data For Creating click area 
					PointF tranpoint=this.TransPoint(PointsToDraw[i,j],BoundArea);

					float clickY=tranpoint.Y-(MiniYBarSpace-1.2f)/2f;

					RectangleF tranrect=new RectangleF(BarRect.X+5f,clickY+5f,BarRect.Width,BarRect.Height);
					
					int index=i*this.nColumnCount+j;

					this.ClickAreas[index]=tranrect;
					//end add
					
				}
				ScaleYValue=ScaleYValue+YEveryWidth;
			}
			#endregion
		}	
	}
	#endregion

	//Added this code at 2009-1-5 9:30:31@Simon
	#region public class HorizonBar3DChart:HorizonBarChart
	public class HorizonBar3DChart:HorizonBarChart
	{	
		/// <summary> 
		/// override base DrawChartStyle from HorizonBarChart,begin drawing HorizonBar3DChart in alimited area
		/// </summary>  
		protected override void DrawChartStyle(Graphics g,Rectangle BoundArea, RectangleF ChartArea)
		{
			#region HorizonBar3DChart
			float ScaleYValue=ChartArea.Y+YEveryWidth/2f;

			int Xcount=XScaleCount;

			int Ycount=this.nRowCount;			

			if(nRowCount+this.nColumnCount<=0)return;

			float  MiniYBarSpace=(2*YEveryWidth)/(3f*nColumnCount);

			if(MiniYBarSpace-1.2<=0)return;

			float YTotalEveryWidth=0f;

			bool b_CustomWidth=this.FitCustomWidth(YEveryWidth,out YTotalEveryWidth);
			
			//save and draw points
			for(int i=0;i<this.nRowCount;i++)
			{   
				float MiniYStart=ScaleYValue-YTotalEveryWidth/2f;

				for(int j=0;j<this.nColumnCount;j++)
				{
					int realcol=this.ColsIndex[j];

					IChartCell cell=this.GetCell(i,j);

					float CellToX=0;
					
					CellToX=ChartArea.X+cell.DataPoint*radiox;					

					if(b_CustomWidth)
					{
						MiniYBarSpace=this.AllSeries[j].Width;
					}
					else
					{
						MiniYBarSpace=this.AutoWidth(YEveryWidth);
					}					
				
					PointsToDraw[i,j]=new PointF(CellToX,MiniYStart+(MiniYBarSpace)/2f);	 
                 
					//draw Points					
					RectangleF BarRect=new RectangleF(ChartArea.X,MiniYStart,CellToX-ChartArea.X,MiniYBarSpace-1.2f);

					if(BarRect.Width*BarRect.Height==0)
					{ 
						MiniYStart=MiniYStart+MiniYBarSpace;
					
						continue;
					}
					Color LightColor=ColorManager.GetLightColor(PointColor[realcol]);

					if(this.AllSeries[realcol].Color!=Color.Empty)
					{						
						LightColor=PointColor[realcol];
					}

					Color DrawColor=Color.Empty;			
					if(this.AllSeries[realcol].Color==Color.Empty)
					{				
						DrawColor=ColorManager.GetDeepColor(PointColor[realcol]);
					}
					else
					{
						if(this.AllSeries[realcol].BorderColor!=Color.Empty)
						{
							DrawColor=this.AllSeries[realcol].BorderColor;
						}
						else
						{
							DrawColor=ColorManager.BorderColor;
						}	
					}
					Pen BarPen=new Pen(DrawColor,1);
					
					#region DrawBar3D 
					float lenX=0f,lenY=0f;				

					if(BarRect.Height>=2)
					{
						GraphicsPath path3d=new GraphicsPath();

						PointF[] pathpoints=new PointF[5];	

						float BarLeft=BarRect.X;

						float BarRight=BarRect.X+BarRect.Width;			
		 
						float BarTop=BarRect.Y+BarRect.Height;

						float BarBottom=BarRect.Y;		

						#region Modify codes at 2008-12-31 17:23:10@Simon
//						if(BarRect.Height<14)
//						{
//							lenY=BarRect.Height/2f;												
//						}
//						else 
//						{ 
//							lenY=BarRect.Height/5f;
//						}	
//						lenX=lenY/2f;//
//
//						pathpoints[0]=new PointF(BarRight,BarTop);
//						pathpoints[1]=new PointF(BarRight-lenX,BarTop+lenY);
//						pathpoints[2]=new PointF(BarLeft,BarTop+lenY);
//						pathpoints[3]=new PointF(BarLeft,BarTop);
//						pathpoints[4]=new PointF(BarRight,BarTop);
//
//						path3d.AddLines(pathpoints);
//						g.FillPath(new SolidBrush(PointColor[realcol]),path3d);
//						g.DrawLines(BarPen,pathpoints);

						#endregion        //End Modify				

						#region origin codes for DrawHorizonBar3D		
			
							if(BarRect.Height<30)
							{
								lenX=6f;

								lenY=5f;
							}
							else
							{	
								float MaxDiff=ChartArea.Width-BarRect.Width-2;

								lenX=lenY=Math.Min(MaxDiff,BarRect.Height/5);							
							}
							
							pathpoints[0]=new PointF(BarLeft,BarTop);

							pathpoints[1]=new PointF(BarLeft+lenX,BarTop+lenY);

							pathpoints[2]=new PointF(BarRight+lenX,BarTop+lenY);

							pathpoints[3]=new PointF(BarRight,BarTop);

							pathpoints[4]=new PointF(BarLeft,BarTop);

							path3d.AddLines(pathpoints);

							g.FillPath(new SolidBrush(PointColor[realcol]),path3d);

							g.DrawLines(BarPen,pathpoints);

							path3d.Dispose();

							path3d=new GraphicsPath();

							pathpoints[0]=new PointF(BarRight,BarTop);

							pathpoints[1]=new PointF(BarRight+lenX,BarTop+lenY);

							pathpoints[2]=new PointF(BarRight+lenX,BarBottom+lenY);

							pathpoints[3]=new PointF(BarRight,BarBottom);

							pathpoints[4]=new PointF(BarRight,BarTop);

							path3d.AddLines(pathpoints);

							g.FillPath(new SolidBrush(PointColor[realcol]),path3d);

							g.DrawLines(BarPen,pathpoints);
						#endregion

					}

					#endregion

					LinearGradientBrush BarBrush=new LinearGradientBrush(BarRect,LightColor,PointColor[realcol],LinearGradientMode.Vertical);
					
					g.FillRectangle(BarBrush,BarRect);				

					#region DrawBar At three-Line  //2009-8-19 16:44:06@Simon modify this Code
					//  g.DrawRectangle(BarPen,BarRect.X,BarRect.Y,BarRect.Width,BarRect.Height);

						g.DrawLine(BarPen,BarRect.X,BarRect.Bottom,BarRect.Right,BarRect.Bottom);

						g.DrawLine(BarPen,BarRect.Right,BarRect.Bottom,BarRect.Right,BarRect.Y);

						g.DrawLine(BarPen,BarRect.Right,BarRect.Y,BarRect.X,BarRect.Y);

					#endregion

					BarBrush.Dispose();

					BarPen.Dispose();

					MiniYStart=MiniYStart+MiniYBarSpace;

					//Added this code at 2008-11-11 9:24:18@Simon
					//storing data For Creating click area 
					PointF tranpoint=this.TransPoint(PointsToDraw[i,j],BoundArea);

					float clickY=tranpoint.Y-(MiniYBarSpace-1.2f)/2f;

					RectangleF tranrect=new RectangleF(BarRect.X+5f,clickY+5f-lenY,BarRect.Width+lenX,BarRect.Height+lenY);
					
					int index=i*this.nColumnCount+j;

					this.ClickAreas[index]=tranrect;
					//end add
					
				}
				ScaleYValue=ScaleYValue+YEveryWidth;
			}
			#endregion
		}	
	}
	#endregion	
	
}
