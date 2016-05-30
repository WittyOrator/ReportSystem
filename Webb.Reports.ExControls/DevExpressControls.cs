/***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:DevExpressControls.cs
 * Author:Country.Wu [EMail:Webb.Country.Wu@163.com]
 * Create Time:11/21/2007 01:59:00 PM
 * Copyright:1986-2007@Webb Electronics all right reserved.
 * Purpose:
 * ***********************************************************************/
#region History
/*
 * //Author@DateTime : Description
 * */
#endregion History

//using System;
//using System.Drawing;
//using System.Collections;
//using System.ComponentModel;
//using System.Windows.Forms;
//using System.Drawing.Drawing2D;
//using System.Drawing.Imaging;
//using System.Collections.Specialized;
//using System.Data;
//using System.ComponentModel.Design;
//using System.Runtime.Serialization;
//using System.IO;
//using System.Reflection;
//
//using DevExpress.XtraReports.Serialization;
//using DevExpress.XtraReports.Native;
//using DevExpress.XtraReports;
//using DevExpress.XtraReports.UI;
//using DevExpress.XtraReports.Localization;
//using DevExpress.XtraPrinting;
//using DevExpress.XtraPrinting.Control;
//using DevExpress.XtraPrinting.Design;
//using DevExpress.XtraPrinting.Drawing;
//using DevExpress.XtraPrinting.Export;
//using DevExpress.XtraPrinting.Localization;
//using DevExpress.XtraPrinting.Native;
//using DevExpress.XtraPrinting.Preview;
//using DevExpress.XtraPivotGrid;
//using DevExpress.XtraPivotGrid.Data;
//using DevExpress.XtraPivotGrid.Design;
//using DevExpress.Data.PivotGrid;
//using DevExpress.XtraGrid;
//using DevExpress.XtraGrid.Filter;
//using DevExpress.XtraGrid.Views.Grid;
//using DevExpress.XtraGrid.Views.Base;
//using DevExpress.Utils.Design;
//using DevExpress.Utils;
//using DevExpress.XtraCharts;
//
//namespace Webb.Reports.ExControls
//{
//
//	#region public class PivotGridReport : DevExpress.XtraPivotGrid.PivotGridControl,IExControl
//	//Wu.Country@2007-11-29 13:03 added this region.	
//
//	//Scott@2007-11-22 09:38 modified some of the following code.
//	/// <summary>
//	/// Summary description for PivotGroup.
//	/// </summary>
//	[XRDesigner("Webb.Reports.ExControls.Design.PivotGridReportDesign ,Webb.Reports.ExControls"),
//	Designer("Webb.Reports.ExControls.Design.PivotGridReportDesign ,Webb.Reports.ExControls")]
//	public class PivotGridReport : DevExpress.XtraPivotGrid.PivotGridControl,IExControlDev
//	{
//		protected int _DataSourceCount;
//		protected string _FilterValues = string.Empty;
//		protected WinControlContainer _ControlContainer;
//
//		public WebbReport Report
//		{
//			get
//			{
//				if(this._ControlContainer==null) return null;
//
//				return this._ControlContainer.Report as WebbReport;
//			}
//		}
//
//		public WinControlContainer XtraContainer
//		{
//			get{return this._ControlContainer;}
//
//			set{this._ControlContainer = value;}
//		}
//
//		public string FilterValues
//		{
//			get
//			{
//				System.Text.StringBuilder m_StringBuilder = new System.Text.StringBuilder();
//				
//				foreach(PivotGridField i_Field in this.Fields)
//				{
//					foreach(object i_FilterValue in i_Field.FilterValues.Values)
//					{
//						m_StringBuilder.Append(i_FilterValue.ToString());
//						
//						m_StringBuilder.Append("\r");
//					}
//					m_StringBuilder.Append("\n");
//				}
//				this._FilterValues = m_StringBuilder.ToString();
//
//				return this._FilterValues;
//			}
//			set
//			{
//				if(this._FilterValues == value) return;
//
//				if(value == null || value == string.Empty) return;
//
//				string[] m_temp = value.Split('\n');
//
//				if(m_temp.Length <= 0) return;
//
//				if(this.Fields.Count <= 0) return;
//
//				this._FilterValues = value;
//
//				System.Diagnostics.Trace.Assert(m_temp.Length - 1 == this.Fields.Count);
//
//				int i = 0;
//
//				foreach(PivotGridField i_Field in this.Fields)
//				{
//					string[] m_Values = m_temp[i].Split('\r');
//
//					foreach(string i_Value in m_Values)
//					{
//						if(i_Value == string.Empty) continue;
//						
//						try
//						{
//							i_Field.FilterValues.Add(Convert.ToInt32(i_Value));
//						}
//						catch
//						{
//							i_Field.FilterValues.Add(i_Value);
//						}
//					}
//					i++;
//				}
//			}
//		}
//
//		public PivotGridReport()
//		{
//			this.Initialize();
//		}
//
//		private void Initialize()
//		{
//			this.CustomSummary += new PivotGridCustomSummaryEventHandler(GridReport_CustomSummary);
//
//			this.DataSourceChanged += new EventHandler(GridReport_DataSourceChanged);
//		}
//
//		int i = 0;
////		private string _PrintAreaName;
//		public override void CreateArea(string areaName, IBrickGraphics graph)
//		{
////			
////			if(areaName==ExControl.PrintAreaName)
////			{
////				this._MainView.SetOffset(this.XtraContainer.Left,this.XtraContainer.Top);
////				base.CreateArea(this._PrintAreaName,graph);
////			}
////			else
////			{
////				if(this.i>0) return;
////				this._PrintAreaName = areaName;
////				ExControl.PrintAllExcontrols(this,areaName,graph);
////				this.i++;
////			}			
//
//			if(i<3)
//			{
//				i++;
//				base.CreateArea(areaName,graph);
//			}
//			else
//			{
//				return;
//			}
//		}
//
//		public virtual void RefreshPrintView()
//		{
//			this.i = 0;
//		}
//
//		protected override void Dispose(bool disposing)
//		{
//			this.CustomSummary -= new PivotGridCustomSummaryEventHandler(GridReport_CustomSummary);
//
//			this.DataSourceChanged -= new EventHandler(GridReport_DataSourceChanged);
//
//			base.Dispose (false);
//		}
//
//		protected override void OnPaint(PaintEventArgs e)
//		{
//			base.OnPaint (e);
//		}
//
//		private int GetDataSourceCount()
//		{
//			if(this.DataSource == null || this.DataMember == null) return 0;
//			
//			try
//			{
//				return (this.DataSource as DataSet).Tables[this.DataMember].Rows.Count;
//			}
//			catch
//			{
//				return 0;
//			}
//		}
//
//		private void GridReport_CustomSummary(object sender, PivotGridCustomSummaryEventArgs e)
//		{
//			if(e.DataField == null) return;
//
//			if(e.DataField.SummaryType == PivotSummaryType.CustomPercent)
//			{
//				try
//				{
//					e.CustomValue = ((decimal)e.SummaryValue.Count)/this._DataSourceCount;
//				}
//				catch
//				{
//					e.CustomValue = 0;
//				}
//			}
//		}
//
//		private void GridReport_DataSourceChanged(object sender,EventArgs e)
//		{
//			this._DataSourceCount = this.GetDataSourceCount();
//		}
//
//		public void CalculateResult()
//		{
//		}
//	}
//	#endregion
//
//	#region public class GridReport : DevExpress.XtraGrid.GridControl,IExControl
//	//Wu.Country@2007-11-29 13:03 added this region.
//	[XRDesigner("Webb.Reports.ExControls.Design.GridReportDesign ,Webb.Reports.ExControls"),
//	Designer("Webb.Reports.ExControls.Design.GridReportDesign ,Webb.Reports.ExControls")]
//	public class GridReport : DevExpress.XtraGrid.GridControl,IExControlDev
//	{
//		private string _TempFilterString = string.Empty;
//		protected WinControlContainer _ControlContainer;
//
//		public WebbReport Report
//		{
//			get
//			{
//				if(this._ControlContainer==null) return null;
//
//				return this._ControlContainer.Report as WebbReport;
//			}
//		}
//
//		public WinControlContainer XtraContainer
//		{
//			get{return this._ControlContainer;}
//
//			set{
//				this._ControlContainer = value;				
//			}
//		}
//
//		public string ActiveFilterString
//		{
//			get
//			{
//				if(this.MainView is GridView)
//				{
//					return (this.MainView as GridView).ActiveFilterString;
//				}
//				else
//				{
//					return null;
//				}
//			}
//			set{this._TempFilterString = value;}
//		}
//
//		//ctor
//		public GridReport()
//		{
//			Point m_Point = new Point(300,120);
//
//			this.Size = new Size(m_Point);
//			
//			if(this.Parent != null)
//			{
//				this.Parent.Size = new Size(m_Point);
//			}
//		}
//
//		int i = 0;
//		private string _PrintAreaName;
//		public override void CreateArea(string areaName, IBrickGraphics graph)
//		{
//			
////			if(areaName==ExControl.PrintAreaName)
////			{
////				//this._MainView.SetOffset(this.XtraContainer.Left,this.XtraContainer.Top);
////				if(this._TempFilterString != null && this._TempFilterString != string.Empty)
////				{
////					if(this.PrintingView is ColumnView)
////					{
////						(this.PrintingView as ColumnView).ActiveFilterString = this._TempFilterString;
////					}
////				}
////				base.CreateArea(this._PrintAreaName,graph);
////			}
////			else
////			{
////				if(this.i>3) return;
////				this._PrintAreaName = areaName;
////				ExControl.PrintAllExcontrols(this,areaName,graph);
////				this.i++;
////			}			
//
//			if(i < 3)
//			{
//				if(this._TempFilterString != null && this._TempFilterString != string.Empty)
//				{
//					if(this.PrintingView is ColumnView)
//					{
//						(this.PrintingView as ColumnView).ActiveFilterString = this._TempFilterString;
//					}
//				}
//				base.CreateArea(areaName,graph);
//			}
//			i++;
//		}
//
//		public virtual void RefreshPrintView()
//		{
//			this.i = 0;
//		}
//
//		protected override void Dispose(bool disposing)
//		{
//			base.Dispose(false);
//		}
//
//		protected override void OnPaint(PaintEventArgs e)
//		{
//			base.OnPaint(e);
//		}
//
//		public void CalculateResult()
//		{
//		}
//	}
//	#endregion
//
//	#region public class WebbChartControl : ChartControl, IExControl
//	/*Descrition:   */
//	[XRDesigner("Webb.Reports.ExControls.Design.WebbChartControlDesign ,Webb.Reports.ExControls"),
//	Designer("Webb.Reports.ExControls.Design.WebbChartControlDesign ,Webb.Reports.ExControls")]
//	public class WebbChartControl : ChartControl,IExControl
//	{
//		//Wu.Country@2007-11-29 01:03 PM added this class.
//		//Fields
//		protected WinControlContainer _ControlContainer;
//
//		//Properties
//
//		//ctor
//		public WebbChartControl()
//		{
//			//
//			//this.Container.Add(this._InnerChartControl);
//			//this.Controls.Add(this._InnerChartControl);
//		}
//		//Methods
//		protected override void Dispose(bool disposing)
//		{
//			base.Dispose (false);
//		}
//
//		#region IExControl Members
//
//		public WinControlContainer XtraContainer
//		{
//			get{return this._ControlContainer;}
//
//			set
//			{
//				this._ControlContainer = value;				
//			}
//		}
//
//		public WebbReport Report
//		{
//			get
//			{
//				if(this._ControlContainer==null) return null;
//
//				return this._ControlContainer.Report as WebbReport;
//			}
//		}
//
//		new public object DataSource
//		{
//			get
//			{
//				// TODO:  Add WebbChartControl.DataSource getter implementation
//				return base.DataSource;
//			}
//			set
//			{
//				// TODO:  Add WebbChartControl.DataSource setter implementation
//				base.DataSource = value;
//			}
//		}
//
//		public string DataMember
//		{
//			get
//			{
//				// TODO:  Add WebbChartControl.DataMember getter implementation
//				return null;
//			}
//			set
//			{
//				// TODO:  Add WebbChartControl.DataMember setter implementation
//			}
//		}
//
//		public void CalculateResult()
//		{
//			// TODO:  Add WebbChartControl.CalculateResult implementation
//		}
//
//		#endregion
//
//		#region IPrintable Members
//
//		public void RejectChanges()
//		{
//			// TODO:  Add WebbChartControl.RejectChanges implementation
//		}
//
//		public void ShowHelp()
//		{
//			// TODO:  Add WebbChartControl.ShowHelp implementation
//		}
//
//		public bool HasPropertyEditor()
//		{
//			// TODO:  Add WebbChartControl.HasPropertyEditor implementation
//			return false;
//		}
//
//		public bool SupportsHelp()
//		{
//			// TODO:  Add WebbChartControl.SupportsHelp implementation
//			return false;
//		}
//
//		public UserControl PropertyEditorControl
//		{
//			get
//			{
//				// TODO:  Add WebbChartControl.PropertyEditorControl getter implementation
//				return null;
//			}
//		}
//
//		public void AcceptChanges()
//		{
//			// TODO:  Add WebbChartControl.AcceptChanges implementation
//		}
//
//		#endregion
//
//		#region IBasePrintable Members
//
//		public void Initialize(IPrintingSystem ps, ILink link)
//		{
//			// TODO:  Add WebbChartControl.Initialize implementation
//		}
//
//		int PrintTiems = 0;
//		public void CreateArea(string areaName, IBrickGraphics graph)
//		{
//			// TODO:  Add WebbChartControl.CreateArea implementation			
//		}
//
//		public void Finalize(IPrintingSystem ps, ILink link)
//		{
//			// TODO:  Add WebbChartControl.Finalize implementation
//		}
//
//		#endregion
//	}
//	#endregion
//}
