/***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:WebbChartView.cs
 * Author:Country.Wu [EMail:Webb.Country.Wu@163.com]
 * Create Time:11/29/2007 01:18:59 PM
 * Copyright:1986-2007@Webb Electronics all right reserved.
 * Purpose:
 * ***********************************************************************/
#region History
/*
 * //Author@DateTime : Description
 * */
#endregion History

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
using System.Security.Permissions;
using Webb.Collections;
using Webb.Reports.DataProvider;
using System.Drawing.Drawing2D;

namespace Webb.Reports.ExControls.Views
{	
	#region WebbChartExView : ExControlView
	[Serializable]
	public class WebbChartExView : ExControlView
	{
		//ISerializable Members
		#region ISerializable Members
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info,context);

			info.AddValue("Settings",this.Settings,typeof(WebbChartSetting));

			info.AddValue("Filter",this.Filter,typeof(Webb.Data.DBFilter));

 			info.AddValue("ClickAreas",this.ClickAreas,typeof(ArrayList));

			info.AddValue("ShowAxesMode",this.ShowAxesMode);  //Added this code at 2008-12-17 9:26:42@Simon

			info.AddValue("AutoFitSize",this.AutoFitSize);  //Added this code at 2008-12-17 9:26:42@Simon

            info.AddValue("TopCount",this.TopCount);  //Added this code at 2008-12-17 9:26:42@Simon

			info.AddValue("Relative",this.Relative);  //Added this code at 2008-12-17 9:26:42@Simon		

		}
		
		public WebbChartExView(SerializationInfo info, StreamingContext context) : base(info,context)
		{
			try
			{
				this._Settings = info.GetValue("Settings",typeof(WebbChartSetting)) as WebbChartSetting;
				
			}
			catch
			{
				    this._Settings = new WebbChartSetting();				   
			 }

			try
			{
				this._Filter = info.GetValue("Filter",typeof(Webb.Data.DBFilter)) as Webb.Data.DBFilter;

				this._Filter=AdvFilterConvertor.GetAdvFilter(DataProvider.VideoPlayBackManager.AdvReportFilters,this._Filter);    //2009-4-29 11:37:37@Simon Add UpdateAdvFilter
			}
			catch{this._Filter = new Webb.Data.DBFilter();}

			try
			{
				this._ClickAreas = info.GetValue("ClickAreas",typeof(ArrayList)) as ArrayList;
			}
			catch
			{
				   this._ClickAreas=new ArrayList();
			}

			#region Modify codes at 2008-11-18 16:49:14@Simon
			this.HaveExisted=true;
			#endregion        //End Modify


			try
			{
				this.ShowAxesMode=info.GetBoolean("ShowAxesMode");          //Added this code at 2008-12-17 9:28:25@Simon
			}
			catch
			{
				this.ShowAxesMode=true;
			}
             try
			{
				this.AutoFitSize=info.GetBoolean("AutoFitSize");          //Added this code at 2008-12-17 9:28:25@Simon
			}
			catch
			{
				this.AutoFitSize=false;
			}
			try
			{
				this.TopCount=info.GetInt32("TopCount");          //Added this code at 2008-12-17 9:28:25@Simon
			}
			catch
			{
				this.TopCount=0;
			}
			try
			{
				this.Relative=info.GetBoolean("Relative");          //2009-6-19 11:01:33@Simon Add this Code
			}
			catch
			{
				this.Relative=this.AutoFitSize;
			}			
		}
		#endregion

		//Ctor
		public WebbChartExView(ExControl i_Control):base(i_Control)
		{
			this._Settings = new WebbChartSetting();	
			this._Filter = new Webb.Data.DBFilter();

        }

        #region Field & Properties

        //Members
		protected WebbChartSetting _Settings;
		protected Webb.Data.DBFilter _Filter;		
		protected ChartBase _Chart;
		protected ArrayList _ClickAreas=null;   //Added this code at 2008-11-11 10:37:35@Simon

		protected bool _HaveExisted=false;   //Added this code to define whether the instance was Serialized  at 2008-11-18 16:44:29@Simon

		protected bool _ShowAxesMode=true;  //Added this code at 2008-12-17 9:24:14@Simon

		protected int  _TopCount=0;  //Added this code at 2008-12-23 10:56:12@Simon	
		
		public Webb.Data.DBFilter DenominatorFilter
		{
			get
			{			
				return this.Settings.DenominatorFilter;
			}
			set{this.Settings.DenominatorFilter = value;}
		}


		public int TopCount  //Added this code at 2008-12-17 17:09:27@Simon
		{
			get{return this._TopCount;}
			set{
				if(value<0)return;
				this._TopCount=value;
				this.Settings.TopCount=value;
			   }
		}

		protected bool _AutoFitSize=false;   //Added this code at 2008-12-17 17:07:31@Simon
		public bool AutoFitSize  //Added this code at 2008-12-17 17:09:27@Simon
		{
			get{return this._AutoFitSize;}
			set{this._AutoFitSize=value;
				this.Settings.AutoFitSize=value;			
			   }
		}

		protected bool _Relative=false;	
		public bool Relative
		{
			get{return this._Relative;}
			set{this._Relative=value;
				this.Settings.Relative=value;	
			
			}
		}
	   [Browsable(false)] 
		public Color ColorWhenMax
		{
			get{return this.Settings.ColorWhenMax;}
			set
			{
				this.Settings.ColorWhenMax=value;			
			}
		}
       
		[Browsable(false)] 
		public float MaxValuesWhenTop
		{
			get{return this.Settings.MaxValuesWhenTop;}
			set
			{
				this.Settings.MaxValuesWhenTop=value;			
			}
		}
		[Browsable(false)] 
		public Color TransparentBackground
		{
			get{return this.Settings.TransparentBackground;}
			set
			{
				this.Settings.TransparentBackground=value;			
			}
		}


        public bool ShowAxesMode     //Added this code at 2008-12-17 9:25:53@Simon      
		{
			get{return this._ShowAxesMode;}
			set{				  
	
				 this.Settings.ShowAxesMode=value;
				  this._ShowAxesMode=value;
				 
			   }
		}

		public Color BackgroundColor     //Added this code at 2008-12-17 9:25:53@Simon      
		{
			get{return this.Settings.BackgroundColor;}
			set
			{
				this.Settings.BackgroundColor=value;	
			}
		}

		public bool CombinedTitle     //Added this code at 2008-12-17 9:25:53@Simon      
		{
			get{return this.Settings.CombinedTitle;}
			set
			{
				this.Settings.CombinedTitle=value;	
			}
		}
		public int  BoundSpace     //Added this code at 2008-12-17 9:25:53@Simon      
		{
			get{return this.Settings.BoundSpace;}
			set
			{
				this.Settings.BoundSpace=value;	
			}
		}

		//Properties
		public bool HaveExisted            //Added this code at 2008-11-18 16:48:06@Simon
		{
			get{return this._HaveExisted;}
			set{this._HaveExisted=value;}
		}

		public WebbChartSetting Settings
		{
			get{if(this._Settings == null) this._Settings = new WebbChartSetting();
				return this._Settings;}
			set{this._Settings = value;}
		}
		
		public ArrayList ClickAreas     //Added this code at 2008-11-11 10:57:14@Simon
		{
			get
			{
				if(this._ClickAreas== null) this._ClickAreas=new ArrayList();
				return this._ClickAreas;}
			set{this._ClickAreas = value;}
		}


		public Webb.Data.DBFilter Filter
		{
			get{if(this._Filter == null) this._Filter = new Webb.Data.DBFilter();
				return this._Filter;}
			set{this._Filter = value;}
		}

		[Browsable(false)]
		public int ControlWidth
		{
			get{return (int)(this.ExControl.XtraContainer.Width/Webb.Utility.ConvertCoordinate);}
		}

		[Browsable(false)]
		public int ControlHeight
		{
			get{return (int)(this.ExControl.XtraContainer.Height/Webb.Utility.ConvertCoordinate);}
        }
        #endregion

        public float GetMaxDatapoint()
		{
			float maxpoint=this._Chart.GetMaxDataPoint();
			
			return this._Chart.GetExMaxPoint();
		}
		//fuction
		public override void CalculateResult(DataTable i_Table)
		{	
			ChangeTypesFor3D();			

			if(i_Table != null)
			{	
				Webb.Collections.Int32Collection rows = new Int32Collection();	

				Webb.Data.DBFilter dbFilter=this.Filter.Copy();				

				if(this.ExControl!=null&&this.ExControl.Report!=null)
				{
					rows=this.ExControl.Report.Filter.GetFilteredRows(i_Table);  //2009-5-25 11:02:57@Simon Add this Code
				  
					dbFilter.Add(this.ExControl.Report.Filter);
				}

				if(this.DenominatorFilter==null)
				{
					if(this.Repeat)
					{
						this.DenominatorFilter=dbFilter;
					}
					else if(this.ExControl!=null&&this.ExControl.Report!=null&&this.ExControl.Report.OneValuePerPage)
					{
						this.DenominatorFilter=dbFilter;
					}
					else
					{
						this.DenominatorFilter=new Webb.Data.DBFilter();
					}
				}

				rows=this.OneValueScFilter.Filter.GetFilteredRows(i_Table,rows);

				rows = this.RepeatFilter.Filter.GetFilteredRows(i_Table,rows);	 //Added this code at 2008-12-26 12:22:40@Simon

				this.Filter=AdvFilterConvertor.GetAdvFilter(DataProvider.VideoPlayBackManager.AdvReportFilters,this.Filter);    //2009-4-29 11:37:37@Simon Add UpdateAdvFilter

				rows = this.Filter.GetFilteredRows(i_Table,rows);					
				
				this._Chart = this.Settings.CreateChart(i_Table,rows);
			}
			else
			{				
				this._Chart = this.Settings.CreateChart(null,null);
			}
		}		
		private void ChangeTypesFor3D()
		{
			switch(this.Settings.ChartType)
			{
				case ChartAppearanceType.Bar:
					if(this._ThreeD)
					{
						this.Settings.ChartType=ChartAppearanceType.Bar3D;
					}
					break;
				case ChartAppearanceType.Pie:
					if(this._ThreeD)
					{
						this.Settings.ChartType=ChartAppearanceType.Pie3D;
					}
					break;
				case ChartAppearanceType.HorizonBar:
					if(this._ThreeD)
					{
						this.Settings.ChartType=ChartAppearanceType.HorizonBar3D;
					}
					break;
				case ChartAppearanceType.Bar3D:
					if(!this._ThreeD)
					{
						this.Settings.ChartType=ChartAppearanceType.Bar;
					}
					break;
				case ChartAppearanceType.Pie3D:
					if(!this._ThreeD)
					{
						this.Settings.ChartType=ChartAppearanceType.Pie;
					}
					break;
				case ChartAppearanceType.HorizonBar3D:
					if(!this._ThreeD)
					{
						this.Settings.ChartType=ChartAppearanceType.HorizonBar;
					}
					break;
				default:
					break;

			}
		}		
		
		public override bool CreatePrintingTable()
		{
			int CellCount=this._Chart.GetCellCount()+1;  //Added this code at 2008-11-11 8:36:08@Simon

            this.PrintingTable = new WebbTable(1,CellCount);

			if(this.ExControl.XtraContainer == null) return false;

			Image bitmap = new Bitmap(this.ControlWidth,this.ControlHeight);	

			Graphics g = Graphics.FromImage(bitmap);

			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

			if(this._Chart != null)
			{
				this._Chart.Draw(g,new Rectangle(new Point(0,0),new Size(this.ControlWidth,this.ControlHeight)));	
			
				DataSet m_DBSet = this.ExControl.DataSource as DataSet;   //Added this code at 2008-11-11 10:29:19@Simon

				this._Chart.SetClickArgs(this.PrintingTable,m_DBSet);   //Added this code at 2008-11-11 10:29:15@Simon

				this._ClickAreas=this._Chart.ClickAreas.Clone() as ArrayList;  //Added this code at 2008-11-11 10:38:40@Simon

			}

			IWebbTableCell cell = this.PrintingTable.GetCell(0,0);

			#region Modify codes at 2009-1-13 13:57:17@Simon

			for(int i=0;i<CellCount;i++)                 
			{
				cell= this.PrintingTable.GetCell(0,i);

				cell.CellStyle.BorderColor = Color.Transparent;

				cell.CellStyle.Sides=DevExpress.XtraPrinting.BorderSide.None;

			}

			#endregion        //End Modify

		    cell= this.PrintingTable.GetCell(0,0);

			cell.CellStyle.Width = bitmap.Width;
			
			cell.CellStyle.Height = bitmap.Height;

			cell.Image = (Bitmap)bitmap.Clone();//Image.FromHbitmap(this._Bitmap.GetHbitmap());

			return true;
		}

		override public void Paint(PaintEventArgs e)
		{
			if(this.PrintingTable!=null)
			{
				(this.PrintingTable.GetCell(0,0) as WebbTableCell).PaintCell(e,0,0);
			}
			else
			{
				base.Paint(e);
			}
		}

		#region CreateArea
		public override int CreateArea(string areaName, IBrickGraphics graph)
		{
			this.PrintingTable.AdjustSize();

			int m_x = this.PrintingTable.Offset.Width;
			int m_y = this.PrintingTable.Offset.Height;

			WebbTableCell m_cell = null;

			int m_row = 0,m_col = 0;			

			m_cell = this.PrintingTable.GetCell(m_row,m_col) as WebbTableCell;
				
			ImageBrick m_ImageBrick = m_cell.CreateImageBrick();

			graph.DrawBrick(m_ImageBrick,new RectangleF(m_x,m_y,m_cell.CellStyle.Width,m_cell.CellStyle.Height));

		    this.CreateClickAreas(graph);     //Added this code at 2008-11-11 14:49:58@Simon
//			switch(this.Settings.ChartType)
//			{
//				case ChartAppearanceType.Bar:
//					break;
//				case ChartAppearanceType.Pie:					
//					break;
//				case ChartAppearanceType.Point:
//					break;
//				default:
//					break;
//			}
			return m_y+ m_cell.CellStyle.Height;
		}
		public void CreateClickAreas(IBrickGraphics graph)    //Added this function at 2008-11-11 14:50:04@Simon
		{
           if(this._ClickAreas==null||this._ClickAreas.Count<=0)return;

			int clickcount=this._ClickAreas.Count;
			
            if(clickcount>this.PrintingTable.GetColumns()-1)return;

			for(int col=0;col<clickcount;col++)
			{
				WebbTableCell m_cell=PrintingTable.GetCell(0,col+1) as WebbTableCell;		
              
				RectangleF rectF=(RectangleF)this._ClickAreas[col]; 
   
				if(rectF==RectangleF.Empty||rectF.Width*rectF.Height<=0)continue;

				TextBrick m_brick = m_cell.CreateTextBrick();
					
				if(m_cell.ClickEventArg!=null)
				{
					m_brick.MouseDown += new MouseEventHandler(Webb.Reports.DataProvider.VideoPlayBackManager.OnBrickDown);
					m_brick.MouseUp += new MouseEventHandler(Webb.Reports.DataProvider.VideoPlayBackManager.OnBrickUp);
					m_brick.OnClick += new EventHandler(m_cell.ClickHandler);
					m_brick.Url = "http://www.webbelectronics.com/";

					rectF.Offset(PrintingTable.Offset.Width,PrintingTable.Offset.Height);
					graph.DrawBrick(m_brick,rectF);
				}
			}
		}
		#endregion


        #region Only For CCRM Data
        public override void GetALLUsedFields(ref ArrayList usedFields)
        {
            if (this._Settings != null) this._Settings.GetALLUsedFields(ref usedFields);
            if (this.Filter != null) this.Filter.GetAllUsedFields(ref usedFields);
            if (this.DenominatorFilter != null) this.DenominatorFilter.GetAllUsedFields(ref usedFields);     
        }


        #endregion
	}
	#endregion
}
