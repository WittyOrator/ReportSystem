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
	[Serializable]
	public class HolePanelInfo
	{
		private bool _Flip;
		private GroupInfo _GroupInfo;

		[Browsable(false)]
		public HolePanelInfo(GroupInfo groupInfo)
		{
			this._GroupInfo = groupInfo;
		}

		[Browsable(true),Category("Custom"),TypeConverter(typeof(PublicDBFieldConverter))]
		public string HoleByField
		{
			get{return (this._GroupInfo.SubGroupInfo as FieldGroupInfo).GroupByField;}
			set{(this._GroupInfo.SubGroupInfo as FieldGroupInfo).GroupByField = value;}
		}

		[Browsable(true),Category("Custom")]
		public bool Flip
		{
			get{return this._Flip;}
			set{this._Flip = value;}
		}

		[Browsable(true),Category("ClickEvent")]
		public ClickEvents ClickEvent
		{
			get{return this._GroupInfo.ClickEvent;}
			set{this._GroupInfo.ClickEvent = value;}
		}
	}

	//HolePanelView
	[Serializable]
	public class HolePanelView : ExControlView,ISerializable
	{
		#region ISerializable Members
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData (info, context);
			info.AddValue("Filter",this._Filter,typeof(DBFilter));
			info.AddValue("HolePanelInfo",this._HolePanelInfo,typeof(HolePanelInfo));
			info.AddValue("RootGroupInfo",this._RootGroupInfo,typeof(FieldGroupInfo));
			info.AddValue("Style",this._Styles,typeof(ExControlStyles));
			info.AddValue("HoleArray",this.arrHoleIDs,typeof(ArrayList));
		}

		public HolePanelView(SerializationInfo info, StreamingContext context):base(info,context)
		{
			try
			{
				this._Filter = info.GetValue("Filter",typeof(DBFilter)) as DBFilter;
			}
			catch
			{
				this._Filter = new DBFilter();
			}

			try
			{
				this._RootGroupInfo = info.GetValue("RootGroupInfo",typeof(FieldGroupInfo)) as FieldGroupInfo;
			}
			catch
			{
				this._RootGroupInfo = new FieldGroupInfo("Hole");
				this._RootGroupInfo = new FieldGroupInfo("");
			}

			try
			{
				this._HolePanelInfo = info.GetValue("HolePanelInfo",typeof(HolePanelInfo)) as HolePanelInfo;
			}
			catch
			{
				this._HolePanelInfo = new HolePanelInfo(this._RootGroupInfo);
			}

			try
			{
				this.arrHoleIDs = info.GetValue("HoleArray",typeof(ArrayList)) as ArrayList;
			}
			catch
			{
				this.arrHoleIDs = this.CreateHoleIDArray();
			}

			try
			{
				this._Styles = info.GetValue("Style",typeof(ExControlStyles)) as ExControlStyles;
			
			}
			catch
			{
				this._Styles = new ExControlStyles();
			}
		}
		#endregion

		protected HolePanelInfo _HolePanelInfo;
		protected FieldGroupInfo _RootGroupInfo;
		protected Styles.ExControlStyles _Styles;
		protected DBFilter _Filter;
		private ArrayList arrHoleIDs = new ArrayList();
		
		public HolePanelInfo HolePanelInfo
		{
			get{return this._HolePanelInfo;}
			set{this._HolePanelInfo = value;}
		}

		public bool Flip
		{
			get{return this.HolePanelInfo.Flip;}
			set{this.HolePanelInfo.Flip = value;}
		}

		public Webb.Data.DBFilter Filter
		{
			get{return this._Filter;}
		}

		public FieldGroupInfo RootGroupInfo
		{
			get{return this._RootGroupInfo;}
		}

		public HolePanelView(HolePanel i_Control):base(i_Control as ExControl)
		{
			this._RootGroupInfo = new  FieldGroupInfo(Fields.CS_Hole);

			this._RootGroupInfo.SubGroupInfo = new FieldGroupInfo("Formation");

			this._HolePanelInfo = new HolePanelInfo(this.RootGroupInfo);

			this._Filter = new Webb.Data.DBFilter();
	
			this._Styles = new ExControlStyles();

			this.Flip = false;

			this.arrHoleIDs = this.CreateHoleIDArray();
		}

		private ArrayList CreateHoleIDArray()
		{
			ArrayList array = new ArrayList();

			int i = 0;

			for(i = 8;i>=0;i-=2)
			{
				array.Add(i);
			}
			for(i = 1;i<10;i+=2)
			{
				array.Add(i);
			}

			return array;
		}

		public int GetTotalRows()
		{
			int ret = 2,count = 0;
			
			if(this.RootGroupInfo == null) return ret;
			
			if(this.RootGroupInfo.GroupResults == null) return ret;

			foreach(GroupResult gr in this.RootGroupInfo.GroupResults)
			{
				if(gr.GroupValue == null || gr.GroupValue.ToString() == string.Empty) continue;

				count = this.GetSubResultRows(gr);

				if(count > ret) ret = count;
			}
			ret++;	//add header

			return count > ret ? count : ret;
		}

		public int GetSubResultRows(GroupResult gr)
		{
			DataTable dt = this.ExControl.GetDataSource();

			if(dt == null) return 0;

			int count = 0;

			if(gr.SubGroupInfo == null) return count;

			if(gr.SubGroupInfo.GroupResults == null) return count;

			string field = (gr.SubGroupInfo as FieldGroupInfo).GroupByField;

			if(field == null || field == string.Empty) return count;

			foreach(GroupResult groupResult in gr.SubGroupInfo.GroupResults)
			{
				if(groupResult.GroupValue == null || groupResult.GroupValue.ToString() == string.Empty) continue;

				count += groupResult.RowIndicators.Count;
			}

			return count;
		}

		public int GetTotalColumns()
		{
			return 10;	//0 to 9
		}

		//calculate result for printing table
		public override void CalculateResult(DataTable i_Table)
		{
			if(this._RootGroupInfo==null) return;

			Webb.Collections.Int32Collection m_Rows = new Webb.Collections.Int32Collection();
			
			for(int i = 0;i<i_Table.Rows.Count;i++)
			{
				if(this.Filter.CheckResult(i_Table.Rows[i]))
				{
					m_Rows.Add(i);
				}
			}
			this._RootGroupInfo.CalculateGroupResult(i_Table,m_Rows.Count,m_Rows,this.RootGroupInfo);
		}

		//create printing table
		public override bool CreatePrintingTable()
		{
			this.PrintingTable = new WebbTable(this.GetTotalRows(),this.GetTotalColumns());
			this.CreateHeader();
			this.CreateContent();
			this.ApplyStyle();
			return true;
		}

		public override void UpdateView()
		{
			base.UpdateView ();
		}

		//set header's value
		private void CreateHeader()
		{
			if(this.PrintingTable == null) return;
			
			if(this.Flip) this.arrHoleIDs.Reverse();

			for(int i = 0;i<this.PrintingTable.GetColumns();i++)
			{
				string value = string.Format("Hole{0}",this.arrHoleIDs[i]);

				if(this.HolePanelInfo.ClickEvent == ClickEvents.PlayVideo)
				{
					foreach(GroupResult gr in this.RootGroupInfo.GroupResults)
					{
						if(gr.GroupValue.ToString() == this.arrHoleIDs[i].ToString())
						{
							this.SetCellValueWithClickEvent(0,i,value,FormatTypes.String,gr.RowIndicators);
						}
						else
						{
							this.SetCellValue(0,i,value,FormatTypes.String);
						}
					}
				}
				else
				{
					this.SetCellValue(0,i,value,FormatTypes.String);
				}
			}

			if(this.Flip) this.arrHoleIDs.Reverse();
		}

		//set each row's value
		private void CreateContent()
		{
			if(this.PrintingTable == null) return;
			
			if(this.Flip) this.arrHoleIDs.Reverse();

			for(int i = 0;i<this.PrintingTable.GetColumns();i++)
			{
				this.SetHoleValue(Convert.ToInt32(this.arrHoleIDs[i]));
			}

			if(this.Flip) this.arrHoleIDs.Reverse();
		}

		private void SetHoleValue(int HoleID)
		{
			if(this.PrintingTable == null) return;

			this.CreateContentByHoleID(HoleID);
		}

		private void CreateContentByHoleID(int HoleID)
		{
			if(this.RootGroupInfo == null) return;
			
			if(this.RootGroupInfo.GroupResults == null) return;

			foreach(GroupResult gr in this.RootGroupInfo.GroupResults)
			{
				if(gr.GroupValue.ToString() == HoleID.ToString())
				{
					CreateContent(gr,HoleID);
				}
			}
		}

		private void CreateContent(GroupResult gr,int HoleID)
		{
			if(gr.SubGroupInfo == null) return;

			DataTable dt = this.ExControl.GetDataSource();

			if(dt == null) return;

			string field = (this.RootGroupInfo.SubGroupInfo as FieldGroupInfo).GroupByField;

			if(field == null) return;

			if(gr.SubGroupInfo.GroupResults == null) return;

			int i = 1;

			foreach(GroupResult grChild in gr.SubGroupInfo.GroupResults)
			{
				if(gr.SubGroupInfo.GroupResults == null) continue;

				Int32Collection rows = grChild.RowIndicators;

				foreach(int index in rows)
				{
					string value = dt.Rows[index][field].ToString();

					if(value == null || value == string.Empty)	continue;

					int col = this.arrHoleIDs.IndexOf(HoleID);

					Int32Collection cellIndicator = new Int32Collection();

					if(this.HolePanelInfo.ClickEvent == ClickEvents.PlayVideo)
					{
						cellIndicator.Add(index);
						this.SetCellValueWithClickEvent(i++,col,value,FormatTypes.String,cellIndicator);
					}
					else
					{
						this.SetCellValue(i++,col,value,FormatTypes.String);
					}
				}
			}
		}

		//set each cell's border
		private void ApplyStyle()
		{
			if(this.PrintingTable == null) return;

			for(int i = 0;i<this.PrintingTable.GetRows();i++)
			{
				for(int j = 0;j<this.PrintingTable.GetColumns();j++)
				{
					IWebbTableCell cell = this.PrintingTable.GetCell(i,j);
					cell.CellStyle.Sides = DevSides.None;
					if(i == 0)
					{
						cell.CellStyle.Sides |= DevSides.Top|DevSides.Bottom;
					}
					if(i == this.PrintingTable.GetRows()-1)
					{
						cell.CellStyle.Sides |= DevSides.Bottom;
					}
					if(j == 0)
					{
						cell.CellStyle.Sides |= DevSides.Left;
					}
					if(j == this.PrintingTable.GetColumns()-1)
					{
						cell.CellStyle.Sides |= DevSides.Right;
					}
				}
			}
		}

		//draw preview
		public override void CreateArea(string areaName, IBrickGraphics graph)
		{
			base.CreateArea (areaName, graph);
		}

		//draw designer
		public override void Paint(PaintEventArgs e)
		{
			if(this.PrintingTable != null)
			{
				this.PrintingTable.PaintTable(e);
			}
			else
			{
				base.Paint (e);
			}
		}

		#region function to set table cell value.	
		private void SetCellValue(int i_Row, int i_Col,object i_Value,FormatTypes i_Type)
		{
			if(i_Value!=null)
				this.PrintingTable.GetCell(i_Row,i_Col).Text = this.FormatValue(i_Value,i_Type);
		}

		private void SetCellValue(int i_Row, int i_Col,object i_Value,SummaryTypes i_Type)
		{
			if(i_Value!=null)
			{
				string m_Value = string.Empty;
				switch(i_Type)
				{
					case SummaryTypes.AverageMinus:
					case SummaryTypes.AveragePlus:
					case SummaryTypes.Average:
					case SummaryTypes.Total:
					case SummaryTypes.TotalMinus:
					case SummaryTypes.TotalPlus: 
						m_Value = this.FormatValue(i_Value,FormatTypes.Decimal);
						break;					
					case SummaryTypes.RelatedPercent:
					case SummaryTypes.GroupPercent:
					case SummaryTypes.Percent:
						m_Value = this.FormatValue(i_Value,FormatTypes.Precent);
						break;
					case SummaryTypes.Frequence:
						m_Value = this.FormatValue(i_Value,FormatTypes.Int);
						break;
					default:
						m_Value = this.FormatValue(i_Value,FormatTypes.String);
						break;
				}
				this.PrintingTable.GetCell(i_Row,i_Col).Text = m_Value;
			}
		}

		private void SetCellValueWithClickEvent(int i_Row, int i_Col,object i_Value,FormatTypes i_Type,Int32Collection i_Rows)
		{
			if(i_Value!=null)
			{
				DataSet m_DBSet = this.ExControl.DataSource as DataSet;
				Webb.Reports.DataProvider.VideoPlayBackArgs m_args = new Webb.Reports.DataProvider.VideoPlayBackArgs(m_DBSet,i_Rows);
				(this.PrintingTable.GetCell(i_Row,i_Col) as WebbTableCell).ClickEventArg = m_args; 
				this.PrintingTable.GetCell(i_Row,i_Col).Text = this.FormatValue(i_Value,i_Type);
			}
		}

		private string FormatValue(object i_value,FormatTypes i_Type)
		{
			switch(i_Type)
			{
				case FormatTypes.Decimal:
					return string.Format("{0:0.00}",i_value);
				case FormatTypes.Int:
					return string.Format("{0:0}",i_value);
				case FormatTypes.Precent:
					return string.Format("{0:0%}",i_value);
				case FormatTypes.String:
				default:
					return i_value.ToString();
			}
		}
		#endregion
	}
}
