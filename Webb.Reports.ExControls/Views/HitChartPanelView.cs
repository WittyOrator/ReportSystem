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
	public interface ISubReport
	{
		PointF Position{get;set;}
		SizeF Size{get;set;}
		DBFilter Filter{get;set;}
		string Name{get;set;}
		string Text{get;set;}
		ExControlViewCollection ExControlViews{get;set;}
		object Tag{get;set;}
	}

	[Serializable]
	public class HitChartField : ISubReport, ISerializable
	{
		#region Auto Constructor By Macro 2008-9-8 11:01:24
		public HitChartField()
		{
		}
	
		public HitChartField(string name, string text, System.Drawing.PointF position, System.Drawing.SizeF size, Webb.Data.DBFilter filter, object tag, Webb.Reports.ExControls.ExControlCollection exControls, Webb.Reports.ExControls.Views.ExControlViewCollection exControlViews)
		{
			this.name=name;
			this.text=text;
			this.position=position;
			this.size=size;
			this.filter=filter;
			this.tag=tag;
			this.exControls=exControls;
			this.exControlViews=exControlViews;
		}
		#endregion

		private string name;
		private string text;
		private PointF position;
		private SizeF size;
		private DBFilter filter;
		private object tag;
		[NonSerialized]
		private ExControlCollection exControls;
		private ExControlViewCollection exControlViews;

		#region Property By Macro 2008-9-8 11:01:27
		public string Name
		{
			get{ return name; }
			set{ name = value; }
		}

		public string Text
		{
			get{ return text; }
			set{ text = value; }
		}

		public System.Drawing.PointF Position
		{
			get{ return position; }
			set{ position = value; }
		}

		public System.Drawing.SizeF Size
		{
			get{ return size; }
			set{ size = value; }
		}

		public Webb.Data.DBFilter Filter
		{
			get{ return filter; }
			set{ filter = value; }
		}

		public object Tag
		{
			get{ return tag; }
			set{ tag = value; }
		}

		public Webb.Reports.ExControls.ExControlCollection ExControls
		{
			get{ return exControls; }
			set{ exControls = value; }
		}

		public Webb.Reports.ExControls.Views.ExControlViewCollection ExControlViews
		{
			get{ return exControlViews; }
			set{ exControlViews = value; }
		}

		#endregion

		#region Serialization By Macro 2008-9-8 11:01:27
		public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			info.AddValue("name",name,typeof(string));
			info.AddValue("text",text,typeof(string));
			info.AddValue("position",position,typeof(System.Drawing.PointF));
			info.AddValue("size",size,typeof(System.Drawing.SizeF));
			info.AddValue("filter",filter,typeof(Webb.Data.DBFilter));
			info.AddValue("tag",tag,typeof(object));
//			info.AddValue("exControls",exControls,typeof(Webb.Reports.ExControls.ExControlCollection));
			info.AddValue("exControlViews",exControlViews,typeof(Webb.Reports.ExControls.Views.ExControlViewCollection));
		
		}

		public HitChartField(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			try
			{
				name=(string)info.GetValue("name",typeof(string));
			}
			catch
			{
				name=string.Empty;
			}
			try
			{
				text=(string)info.GetValue("text",typeof(string));
			}
			catch
			{
				text=string.Empty;
			}
			try
			{
				position=(System.Drawing.PointF)info.GetValue("position",typeof(System.Drawing.PointF));
			}
			catch
			{
				position=PointF.Empty;
			}
			try
			{
				size=(System.Drawing.SizeF)info.GetValue("size",typeof(System.Drawing.SizeF));
			}
			catch
			{
				size=SizeF.Empty;
			}
			try
			{
				filter=(Webb.Data.DBFilter)info.GetValue("filter",typeof(Webb.Data.DBFilter));

                filter=AdvFilterConvertor.GetAdvFilter(DataProvider.VideoPlayBackManager.AdvReportFilters,this.filter);    //2009-4-29 11:37:37@Simon Add UpdateAdvFilter
			}
			catch
			{
				filter=new DBFilter();
			}
			try
			{
				tag=(object)info.GetValue("tag",typeof(object));
			}
			catch
			{
			}
			try
			{
				exControlViews=(Webb.Reports.ExControls.Views.ExControlViewCollection)info.GetValue("exControlViews",typeof(Webb.Reports.ExControls.Views.ExControlViewCollection));
			}
			catch
			{
				exControlViews=new ExControlViewCollection();
			}
		}
		#endregion

		#region Copy Function By Macro 2008-9-8 11:01:27
		public HitChartField Copy()
		{
			HitChartField thiscopy=new HitChartField();
			thiscopy.name=this.name;
			thiscopy.text=this.text;
			thiscopy.position=this.position;
			thiscopy.size=this.size;
			thiscopy.filter=this.filter;
			thiscopy.tag=this.tag;
			thiscopy.exControls=this.exControls;
			thiscopy.exControlViews=this.exControlViews;
			return thiscopy;
		}
		#endregion
	}

	/// <summary>
	/// Summary description for HitChartPanelView.
	/// </summary>
	[Serializable]
	public class HitChartPanelView : ExControlView
	{
		//ctor
		public HitChartPanelView(HitChartPanel i_Control):base(i_Control as ExControl)
		{
			
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

		protected GroupInfo _RootGroupInfo;
		public GroupInfo RootGroupInfo
		{
			get{return this._RootGroupInfo;}
			set{this._RootGroupInfo = value;}
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

		/// <summary>
		/// Calculate result by data struct and data source.
		/// </summary>
		/// <param name="i_Table">data source table</param>
		public override void CalculateResult(DataTable i_Table)
		{
			GroupView.ConvertOldGroupInfo(this._RootGroupInfo);
		}

		/// <summary>
		/// Create printing table
		/// </summary>
		/// <returns>Whether the printing table been created</returns>
		public override bool CreatePrintingTable()
		{
			return true;
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
			for(int m_Col = 0; m_Col < m_Cols; m_Col++)
			{
				this.PrintingTable.GetCell(0,m_Col).CellStyle.Width = this.ColumnsWidth[m_Col];
			}
		}

		/// <summary>
		/// Calculate result, and create printing table.
		/// </summary>
		public override void UpdateView()
		{
			base.UpdateView();
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
	}
}
