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
	/// <summary>
	/// Summary description for StatControlView.
	/// </summary>
	[Serializable]
	public class StatControlView : ExControlView
	{
		#region ISerializable Members

		override public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			// TODO:  Add ExControlView.GetObjectData implementation
			base.GetObjectData(info,context);

			info.AddValue("Styles",this._Styles,typeof(ExControlStyles));

			info.AddValue("Filter",this._Fitler,typeof(Webb.Data.DBFilter));

			info.AddValue("StatInfos",this._StatInfos,typeof(StatInfoCollection));

			info.AddValue("Multiline",this._Multiline);

			info.AddValue("Overlap",this._Overlap);

			info.AddValue("_ColumnsWidth",this._ColumnsWidth,typeof(Int32Collection));	
             
			info.AddValue("_AdjustWidth",this._AdjustWidth);


		}

		public StatControlView(SerializationInfo info, StreamingContext context):base(info,context)
		{
			// TODO:  Add ExControlView.GetObjectData implementation
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
				this._Fitler = info.GetValue("Filter",typeof(Webb.Data.DBFilter)) as Webb.Data.DBFilter;

				this._Fitler=AdvFilterConvertor.GetAdvFilter(DataProvider.VideoPlayBackManager.AdvReportFilters,this._Fitler);    //2009-4-29 11:37:37@Simon Add UpdateAdvFilter
			}
			catch
			{
				this._Fitler = new Webb.Data.DBFilter();
			}

			try
			{
				this._StatInfos = info.GetValue("StatInfos",typeof(StatInfoCollection)) as StatInfoCollection;
			}
			catch
			{
				this._StatInfos = new StatInfoCollection();

				this._StatInfos.Add(new StatInfo());
			}

			try
			{
				this._Styles = info.GetValue("Styles",typeof(ExControlStyles)) as ExControlStyles;
			}
			catch
			{
				this._Styles = new ExControlStyles();

				this._Styles.RowStyle.Sides = DevExpress.XtraPrinting.BorderSide.None;

				this._Styles.AlternateStyle.Sides = DevExpress.XtraPrinting.BorderSide.None;
			}

			try
			{
				this._Multiline = info.GetBoolean("Multiline");
			}
			catch
			{
				this._Multiline = true;
			}
			try
			{
				this._Multiline = info.GetBoolean("Multiline");
			}
			catch
			{
				this._Multiline = true;
			}

			try
			{
				this._AdjustWidth = info.GetBoolean("_AdjustWidth");
			}
			catch
			{
				this._AdjustWidth = false;
			}
		}
		#endregion

		public StatControlView(StatControl i_Control):base(i_Control as ExControl)
		{
			this._StatInfos = new StatInfoCollection();

			this._StatInfos.Add(new StatInfo());

			this._Fitler = new Webb.Data.DBFilter();

			this._Styles = new ExControlStyles();

			this._Styles.RowStyle.Sides = DevExpress.XtraPrinting.BorderSide.None;

			this._Styles.AlternateStyle.Sides = DevExpress.XtraPrinting.BorderSide.None;

			this._Multiline = true;

			this._Overlap = true;

			_AdjustWidth=false;
		}
	
	
		#region Field & properties
		protected Webb.Data.DBFilter _Fitler;
		protected  StatInfoCollection _StatInfos;
		protected ExControlStyles _Styles;
		protected bool _Multiline;
		protected bool _Overlap;
		protected bool _AdjustWidth=false; 

		protected Int32Collection _ColumnsWidth=new Int32Collection();
		
		public bool AdjustWidth
		{
			get{return this._AdjustWidth;}
			set{this._AdjustWidth = value;}
		}

		public Int32Collection ColumnsWidth
		{
			get{
				if(_ColumnsWidth==null)_ColumnsWidth=new Int32Collection();
				return this._ColumnsWidth;}
			set{this._ColumnsWidth = value;}
		}

		public bool Overlap
		{
			get{return this._Overlap;}
			set{this._Overlap = value;}
		}

		public bool Multiline
		{
			get{return this._Multiline;}
			set{this._Multiline = value;}
		}

		public Webb.Data.DBFilter Filter
		{
			get{
                if (this._Fitler == null) _Fitler = new Webb.Data.DBFilter();
                  return this._Fitler;}
			set{this._Fitler = value.Copy();}
		}

		public StatInfoCollection StatInfos
		{
			get{return this._StatInfos;}
		}

		public ExControlStyles Styles
		{
			get{return this._Styles;}
		}
		#endregion

		#region GetRows/Columns
		private int GetMaxStatInfosCount()
		{
			int ret = 0,temp = 0;

			foreach(StatInfo info in this._StatInfos)
			{
				temp = info.StatTypes.Count;

				ret = temp > ret ? temp : ret;
			}

			return ret + 1;
		}

		private int GetTotalStatInfosCount()
		{
			int ret = 0;

			foreach(StatInfo info in this._StatInfos)
			{
				ret += info.StatTypes.Count + 1;
			}

			return ret;
		}

		protected int GetTotalRows()
		{
			if(this._Multiline)
			{
				return this._StatInfos.Count;
			}
			else
			{
				return 1;
			}
		}

		protected int GetTotalColumns()
		{
			if(this._Multiline)
			{
				return this.GetMaxStatInfosCount();
			}
			else
			{
				return this.GetTotalStatInfosCount();
			}
		}

		#endregion

		#region sub functions  ApplyColumnWidthStyle  & SetValue
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
		protected void SetValues()
		{
			int row = 0,col = 0,index = 0;

			foreach(StatInfo info in this._StatInfos)
			{
				WebbTableCell cell = this.PrintingTable.GetCell(row,col) as WebbTableCell;

				string title=info.Title;

				if(title.IndexOf(@"\[")>=0&title.IndexOf(@"\]")>=0)
				{
					title.Replace(@"\[","[");
					title.Replace(@"\]","[");
				}
				else
				{
					title = title.Replace("[onevalue]",this.OneValueScFilter.FilterName);	//06-04-2008@Scott

					title = title.Replace("[ONEVALUE]",this.OneValueScFilter.FilterName);	//06-04-2008@Scott

					title = title.Replace("[repeat]",this.RepeatFilter.FilterName);	//06-04-2008@Scott

					title = title.Replace("[REPEAT]",this.RepeatFilter.FilterName);	//06-04-2008@Scott
				}

				//set title
				if(info.ClickEvent != ClickEvents.PlayVideo)
				{
					WebbTableCellHelper.SetCellValue(this.PrintingTable,row,col,title,FormatTypes.String);
				}
				else
				{
                    WebbTableCellHelper.SetCellValueWithClickEvent(this.PrintingTable, row, col, title, FormatTypes.String, info.RowIndicators);
				}
               
				col++;

				index = 0;

				//set stat results
     			foreach(FollowedStatTypes followtype in info.StatisticalSetting)
				{
					if(index < info.Result.Count)
					{
						cell = this.PrintingTable.GetCell(row,col) as WebbTableCell;

						object value = info.Result[index];

						if(info.ClickEvent != ClickEvents.PlayVideo)
						{
							  WebbTableCellHelper.SetCellValue(this.PrintingTable, row, col, value, followtype);
						}
						else
						{

                               WebbTableCellHelper.SetCellValueWithClickEvent(this.PrintingTable, row, col, value, followtype, info.RowIndicators);
						}
						cell.Text+=followtype.Followed;
						
					}

					col++;

					index++;
				}

				if(this._Multiline) 
				{
					col = 0;

					row++;
				}
			}
		}



		public void SetColumnsWidth()
		{
			this.ExControl.CalculateResult();

			this.ColumnsWidth.Clear();

			int rows=this.PrintingTable.GetRows();

			if(rows<=0)return;

			int cols = this.PrintingTable.GetColumns();

			for(int i=0;i<cols;i++)
			{
				this.ColumnsWidth.Add(this.PrintingTable.GetColumnWidth(i));
			}
		}
		#endregion

		#region Main Draw/Print functions 
		public override bool CreatePrintingTable()
		{
			int rows = this.GetTotalRows();

			int cols = this.GetTotalColumns();

			if(rows == 0 || cols == 0) return false;

			this.PrintingTable = new WebbTable(rows,cols);

			this.SetValues();

			Int32Collection indicators = new Int32Collection();

			StyleBuilder.StyleRowsInfo m_StyleInfo = new Styles.StyleBuilder.StyleRowsInfo(indicators,indicators,indicators,false,true);
			
			StyleBuilder styleBuilder = new StyleBuilder();

			styleBuilder.BuildStyle(this.PrintingTable,m_StyleInfo,null,this.Styles,new Int32Collection());

			this.PrintingTable.AutoAdjustSize(this.ExControl.CreateGraphics(),false,true);

			this.PrintingTable.SetNoWrap();

			if(this.AdjustWidth)
			{
				this.ApplyColumnWidthStyle(cols);
			}

			return true;
		}
		public override int CreateArea(string areaName, IBrickGraphics graph)
		{
			int Rows=this.PrintingTable.GetRows();

			int Cols=this.PrintingTable.GetColumns();

			for(int row=0;row<Rows;row++)
			{
				for(int col=0;col<Cols;col++)
				{
                   WebbTableCell cell = this.PrintingTable.GetCell(row,col) as WebbTableCell;

				   cell.CellStyle.Width+=1;
				}
			}
			if(PrintingTable!=null)
			{
				this.PrintingTable.ExControl=this.ExControl;
			}
			return base.CreateArea (areaName, graph);
		}


		
		public override void CalculateResult(DataTable i_Table)
		{
			if(i_Table == null)
			{
				foreach(StatInfo info in this._StatInfos)
				{
					info.CalculateResult(new DataTable(),0,new Int32Collection());
				}
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

			m_Rows = this.Filter.GetFilteredRows(i_Table,m_Rows);	//06-04-2008@Scott

			foreach(StatInfo info in this._StatInfos)
			{
				info.CalculateResult(i_Table,m_Rows.Count,m_Rows);
			}
		}

		override public void Paint(PaintEventArgs e)
		{
			if(this.PrintingTable!=null)
			{
				if(this.ThreeD)
				{
					this.PrintingTable.PaintTable3D(e,true,Rectangle.Empty);
				}
				else
				{
					this.PrintingTable.PaintTable(e,true,Rectangle.Empty);
				}
			}
			else
			{
				base.Paint(e);
			}
		}
		#endregion

        #region Only For CCRM Data
        public override void GetALLUsedFields(ref ArrayList usedFields)
        {
            if (this.StatInfos != null)
            {
                foreach (StatInfo statInfo in this.StatInfos)
                {
                    if(statInfo==null)continue;

                    if (!usedFields.Contains(statInfo.StatField)) usedFields.Add(statInfo.StatField);

                    statInfo.Filter.GetAllUsedFields(ref usedFields);

                    statInfo.DenominatorFilter.GetAllUsedFields(ref usedFields);

                }
            }

            this.Filter.GetAllUsedFields(ref usedFields);

           
        }
        #endregion
	}
}