/***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:WebbReport.cs
 * Author:Country.Wu [EMail:Webb.Country.Wu@163.com]
 * Create Time:11/7/2007 10:17:09 AM
 * Copyright:1986-2007@Webb Electronics all right reserved.
 * Purpose:
 * ***********************************************************************/
#region History
/*
 * //Author@DateTime : Description
 * */
#endregion History

using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Configuration;
using System.Timers;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Drawing;
using System.Collections.Specialized;

using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization;

using System.Reflection;

using DevExpress.XtraReports;
using DevExpress.XtraReports.Design;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.UserDesigner;

using Webb.Data;

namespace Webb.Reports
{
	/// <summary>
	/// Summary description for WebbReport.
	/// </summary>
	[Serializable]
	public class WebbReport : DevExpress.XtraReports.UI.XtraReport
	{
		static public WinControlContainerCollection ExControls = new WinControlContainerCollection();		//All excontrols in the report
	    static public Control firstExcontrol=null; 

		#region old properties
		[Category("Webb Report")]
		[Browsable(false)]
		[TypeConverter(typeof(Webb.Data.PublicDBFieldConverter))]
		public string GroupByField
		{
			get{return this.Template.GroupByField;}
			set{this.Template.GroupByField = value;}
		}

		[Category("Webb Report")]
		[Browsable(false)]
		[EditorAttribute(typeof(Webb.Reports.Editors.SortingColumnEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public IList GroupByFields
		{
			get{return this.Template.GroupByFields;}
			set{this.Template.GroupByFields = value;}
		}

		[Category("Webb Report")]
		[Browsable(false)]
		public bool OneValuePerPage
		{
			get{return this.Template.OneValuePerPage;}
			set{this.Template.OneValuePerPage = value;}
		}

		[Category("Webb Report")]
		[Browsable(false)]
		public int TopCount
		{
			get{return this.Template.TopCount;}
			set{this.Template.TopCount = value;}
		}

		[Category("Webb Report")]
		[Browsable(false)]
		public bool RepeatedReport
		{
			get{return this.Template.RepeatedReport;}
			set{this.Template.RepeatedReport = value;}
		}

		[Category("Webb Report")]
		[Browsable(false)]
		public float RepeatedWidth
		{
			get{return this.Template.RepeatedWidth;}
			set{this.Template.RepeatedWidth = value;}
		}

		[Category("Webb Report")]
		[Browsable(false)]
		public float RepeatedHeight
		{
			get{return this.Template.RepeatedHeight;}
			set{this.Template.RepeatedHeight = value;}
		}

		[Category("Webb Report")]
		[Browsable(false)]
		public int RepeatedCount
		{
			get{return this.Template.RepeatedHorizonCount;}
			set{this.Template.RepeatedHorizonCount = value;}
		}
		
		[Category("Webb Report")]
		[Browsable(false)]
		public int RepeatedVerticalCount
		{
			get{return this.Template.RepeatedVerticalCount;}
			set{this.Template.RepeatedVerticalCount = value;}
		}

		[Category("Webb Report")]
		[Browsable(false)]
		public bool OnePageReport
		{
			get{return this.Template.ControlHeaderOnce;}
			set{this.Template.ControlHeaderOnce = value;}
		}

		[Category("Webb Report")]
		[Browsable(false)]
		[EditorAttribute(typeof(Webb.Reports.Editors.SectionFiltersEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public SectionFilterCollection GroupBySectionFilters	//Modified at 2008-12-18 10:32:29@Scott
		{
			get{return this.Template.GroupBySectionFilters;}
			set{this.Template.GroupBySectionFilters = value;}
		}
		[Category("Webb Report")]
		[Browsable(false)]
		[EditorAttribute(typeof(Webb.Reports.Editors.SectionFiltersEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public SectionFilterCollection RepeatSectionFilters	//Modified at 2008-12-18 10:32:29@Simon
		{
			get{return this.Template.RepeatSectionFilters;}
			set{this.Template.RepeatSectionFilters = value;}
		}
		[Category("Webb Report")]
		[Browsable(false)]
		[EditorAttribute(typeof(Webb.Reports.Editors.SectionFiltersEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public IList RepeatFields	//Modified at 2008-12-18 10:32:29@Simon
		{
			get{return this.Template.RepeatFields;}
			set{this.Template.RepeatFields = value;}
		}



		//Modified at 2008-10-23 17:31:00@Scott
		[Category("Webb Report")]
		[Browsable(false)]
		[EditorAttribute(typeof(Webb.Reports.Editors.DBFilterEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public Webb.Data.DBFilter Filter
		{
			get{return this.Template.Filter;}
			set
			{
				this.Template.Filter = value;
				if(this.WebbDataSource != null)
				{
					this.UpdataDBSourceForExControls(this.WebbDataSource.DataSource,this.WebbDataSource.DataMember);
				}
			}
		}

		//Modified at 2008-11-6 9:43:19@Scott
		[Category("Webb Report")]
		[Browsable(false)]
		public Webb.Data.DiagramScoutType DiagramScoutType
		{
			get{return this.Template.DiagramScoutType;}
			set{this.Template.DiagramScoutType = value;}
		}
		#endregion

		[Category("Webb Report")]
		public WebbReportTemplate Template
		{
			get
			{
				if(!(base.Tag is WebbReportTemplate)) base.Tag = new WebbReportTemplate();

				return base.Tag as WebbReportTemplate;
			}
			set
			{
				base.Tag = value;
			}
		}


		[Category("Webb Report")]
		public bool ClickEvents
		{
			get{return this.Template.ClickEvent;}
			set
			{
				this.Template.ClickEvent=value;
				if(this.WebbDataSource != null)
				{
					this.UpdataDBSourceForExControls(this.WebbDataSource.DataSource,this.WebbDataSource.DataMember);
				}
			}
		}
		
		WebbDataSource _WebbDataSource;
		[Browsable(false)]
		public WebbDataSource WebbDataSource
		{
			get{return this._WebbDataSource;}
			set{this._WebbDataSource = value;}
		}

		[EditorAttribute(typeof(Webb.Reports.Editors.UserRightEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public UserLevel LicenseLevel
		{
			get{return this.Template.LicenseLevel;}
			set{this.Template.LicenseLevel= value;}
		}
		

		//06-03-2008@Scott
		public void ApplyTemplate(WebbReportTemplate template)
		{
			this.Template.Apply(template);	//07-08-2008@Scott
		}

		//06-03-2008@Scott
		public void UpdateTemplate(WebbReportTemplate template)
		{
			template.Apply(this.Template);	//07-08-2008@Scott
		}

		//WebbDataSource
		public WebbReport()
		{
			this.BeforePrint += new System.Drawing.Printing.PrintEventHandler(WebbReport_BeforePrint);
		}

		protected override void Dispose(bool disposing)
		{
			this.BeforePrint -= new System.Drawing.Printing.PrintEventHandler(WebbReport_BeforePrint);
			base.Dispose (disposing);
		}

		public void InitializeDefaultReport()
		{
			DevExpress.XtraReports.UI.PageHeaderBand pageHeaderBand = new DevExpress.XtraReports.UI.PageHeaderBand();			
			DevExpress.XtraReports.UI.PageFooterBand pageFooterBand = new DevExpress.XtraReports.UI.PageFooterBand();
			DevExpress.XtraReports.UI.DetailBand detailBand = new DevExpress.XtraReports.UI.DetailBand();
			pageHeaderBand.Height = 130;
			pageFooterBand.Height = 50;
			detailBand.Height = 600;
			pageHeaderBand.Name = "Page_Header";
			pageFooterBand.Name = "Page_Footer";
			detailBand.Name = "Report_Details";
			this.Bands.Add(pageHeaderBand);
			this.Bands.Add(pageFooterBand);
			this.Bands.Add(detailBand);

			this.Margins.Top = 35;
			this.Margins.Bottom = 35;
			this.Margins.Left = 35;
			this.Margins.Right = 35;

			this.Watermark.ImageTransparency = 140;
			this.Font = new Font(DevExpress.Utils.AppearanceObject.DefaultFont.FontFamily,9f);
			this.Name = "WebbReports";
		}

		//06-19-2008@Scott
		public void SetWatermark(string strFile)
		{
			if(!File.Exists(strFile)) return;

			try
			{
				if(strFile.ToLower().EndsWith(".wmks"))
				{
					XRWatermark xrwatermark=wmksFileManager.ReadFile(strFile);

					if(xrwatermark==null)return;

					this.Watermark.CopyFrom(xrwatermark);
				}
				else
				{
                    this.Watermark.Image = Webb.Utility.ReadImageFromPath(strFile);	
				}
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
			}
		}

		#region Overrided properties

		[Browsable(false),
		EditorBrowsable(EditorBrowsableState.Never),]
		new DevExpress.XtraPrinting.PrinterSettingsUsing DefaultPrinterSettingsUsing
		{
			get{return base.DefaultPrinterSettingsUsing;}
			//set{base.DefaultPrinterSettingsUsing = value;}
		}

		[Browsable(false),
		EditorBrowsable(EditorBrowsableState.Never),]
		new DevExpress.XtraReports.UserDesigner.XRDesignFormEx DesignFormEx
		{
			get{return base.DesignFormEx;}
			//set{base.DesignFormEx = value;}
		}

		[Browsable(false),
		EditorBrowsable(EditorBrowsableState.Never),]
		new string XmlDataPath
		{
			get{return base.XmlDataPath;}
			set{base.XmlDataPath = value;}
		}

		[EditorBrowsable(EditorBrowsableState.Never),]
		[Browsable(false)]
		new object Tag
		{
			get{return base.Tag;}
			set{base.Tag = value;}
		}
		
		
		[Browsable(false),
		EditorBrowsable(EditorBrowsableState.Never),]
		new DevExpress.XtraReports.UI.XtraReportScripts Scripts
		{
			get{return base.Scripts;}
			//set{base.Scripts = value;}
		}

		[Browsable(false),
		EditorBrowsable(EditorBrowsableState.Never),]
		new DevExpress.XtraReports.UI.XRControlStyleSheet StyleSheet
		{
			get{return base.StyleSheet;}
		}
		[Browsable(false),
		EditorBrowsable(EditorBrowsableState.Never),]
		new string StyleSheetPath
		{
			get{return base.StyleSheetPath;}
			set{base.StyleSheetPath = value;}
		}
		[Browsable(false),
		EditorBrowsable(EditorBrowsableState.Never),]
		new string HtmlCharSet
		{
			get{return base.HtmlCharSet;}
			set{base.HtmlCharSet = value;}
		}
		[Browsable(false),
		EditorBrowsable(EditorBrowsableState.Never),]
		new bool HtmlCompressed
		{
			get{return base.HtmlCompressed;}
			set{base.HtmlCompressed = value;}
		}
		[Browsable(false),
		EditorBrowsable(EditorBrowsableState.Never),]
		new DevExpress.XtraReports.ScriptLanguage ScriptLanguage
		{
			get{return base.ScriptLanguage;}
			set{base.ScriptLanguage = value;}
		}
		[Browsable(false),
		EditorBrowsable(EditorBrowsableState.Never),]
		new string[] ScriptReferences
		{
			get{return base.ScriptReferences;}
			set{base.ScriptReferences = value;}
		}

		#endregion

		public virtual void ShowDesignForm()
		{
			ReportDesigner.ReportDesignerBase.LoadReport(this);
		}

		public virtual void ShowPreviewForm()
		{
			base.ShowPreview();
		}

//		public override void SetDataSource(IDataAdapter i_DataAdapter, DataSet i_DataSource, string i_DataMember)
//		{
//			//base.SetDataSource (i_DataAdapter, i_DataSource, i_DataMember);
//		}

		#region UpdataDBSourceForExControls(DataSet i_DataSource,string i_DataMember)
		//Wu.Country@2007-11-21 09:44 added this region.		
		public void UpdataDBSourceForExControls(DataSet i_DataSource,string i_DataMember)
		{
			foreach(object m_obj in this.Controls)
			{
				Band m_Detail = m_obj as Band;

				if(m_Detail == null) continue;

				foreach(object m_control in m_Detail.Controls)
				{
					if(m_control is WinControlContainer)
					{
						WinControlContainer m_container = m_control as WinControlContainer;
						
						IExControl m_ExControl = m_container.WinControl as IExControl;
						
						if(m_ExControl==null)continue;
						
						m_ExControl.DataSource = i_DataSource;
						
						m_ExControl.DataMember = i_DataMember;

						m_ExControl.CalculateResult();
					}
				}
			}
		}

		public void SetDataSource(WebbDataSource i_DataSource)
		{
			this._WebbDataSource = i_DataSource;
			//this._SectionFilters = i_DataSource.SectionFilters;	//04-23-2008@Scott
			this.UpdataDBSourceForExControls(i_DataSource.DataSource,i_DataSource.DataMember);
		}
		#endregion

		public int GetHeightPerPage()
		{
			int nHeight = this.PageHeight - this.Margins.Top - this.Margins.Bottom;

			foreach(DevExpress.XtraReports.UI.Band band in this.Bands)
			{
				if(!(band is DevExpress.XtraReports.UI.DetailBand || band is DevExpress.XtraReports.UI.TopMarginBand
					|| band is DevExpress.XtraReports.UI.BottomMarginBand))	//06-02-2008@Scott
				{
					if(band.Controls.Count > 0) nHeight -= band.Height;
				}
			}

			return (int)(nHeight/Webb.Utility.ConvertCoordinate) ;
			
		}
		public Size GetSizePerPage()  //Added this code at 2009-2-3 16:36:42@Simon
		{
			int nHeight = this.PageHeight - this.Margins.Top - this.Margins.Bottom;

			int nWidth=this.PageWidth-this.Margins.Left - this.Margins.Right;			

			if(nHeight<=0||nWidth<=0)
			{
				return Size.Empty;
			}

            nWidth = (int)(nWidth / Webb.Utility.ConvertCoordinate);

            nHeight = (int)(nHeight / Webb.Utility.ConvertCoordinate);

			return new Size(nWidth,nHeight);

		}

        public Size GetNoDealedSizePerPage()  //Added this code at 2009-2-3 16:36:42@Simon
        {
            int nHeight = this.PageHeight - this.Margins.Top - this.Margins.Bottom;

            int nWidth = this.PageWidth - this.Margins.Left - this.Margins.Right;

            if (nHeight <= 0 || nWidth <= 0)
            {
                return Size.Empty;
            }
           
            return new Size(nWidth, nHeight);

        }
		
		public int GetReportHeaderHeight()
		{
			foreach(DevExpress.XtraReports.UI.Band band in this.Bands)
			{
				if(!(band is DevExpress.XtraReports.UI.DetailBand || band is DevExpress.XtraReports.UI.TopMarginBand
					|| band is DevExpress.XtraReports.UI.BottomMarginBand))
				{
					if(band is DevExpress.XtraReports.UI.ReportHeaderBand)
					{//report header
						if(band.Controls.Count > 0) return band.Height;
					}
				}
			}
			return 0;
		}
		

		public int GetReportFooterHeight()
		{
			foreach(DevExpress.XtraReports.UI.Band band in this.Bands)
			{
				if(!(band is DevExpress.XtraReports.UI.DetailBand || band is DevExpress.XtraReports.UI.TopMarginBand
					|| band is DevExpress.XtraReports.UI.BottomMarginBand))
				{
					if(band is DevExpress.XtraReports.UI.ReportFooterBand)
					{//report footer
						if(band.Controls.Count > 0) return band.Height;
					}
				}
			}
			return 0;			
		}

		private void WebbReport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			//Wu.Country@2007-11-26 16:35 modified some of the following code.
			this.Report.Name = Webb.Utility.GetCurFileName();	//09-04-2008@Scott

			this.AdjustExControlsPosition();

            this.AdjustExControlSize("ReportInfoLabel",false);	//06-13-2008@Scott		

            this.AdjustExControlSize("FileNameControl", false);	//06-13-2008@Scott
		}

		//06-13-2008@Scott Grow or shrink control
		private void AdjustExControlSize(string strControlName,bool bShrink)
		{
			Band band = null;
			
			foreach(object m_obj in this.Controls)
			{
				band = m_obj as Band;
				
				if(band == null)continue;
				
				foreach(object control in band.Controls)
				{
					if(control is WinControlContainer)
					{
						WinControlContainer container = control as WinControlContainer;

						string strName = container.WinControl.GetType().Name;

						if( strName == strControlName)
						{
							if(!bShrink)
							{
								Type type = container.WinControl.GetType();

								MethodInfo mi = type.GetMethod("AutoAdjustSize",BindingFlags.Public | BindingFlags.Instance);

								mi.Invoke(container.WinControl,null);
							}
							else
							{
								container.Height = container.Height > Webb.Utility.ControlMaxHeight ? Webb.Utility.ControlMaxHeight : container.Height;

								int nWidth = this.PageWidth - this.Margins.Left - container.Left;

								container.Width = container.Width > nWidth ? nWidth : container.Width;
							}
						}
					}
				}
			}
		}

		//06-13-2008@Scott MinimizeDesignArea
		public void MinimizeDesignArea()
		{
			this.AdjustExControlSize("GroupingControl",true);
			this.AdjustExControlSize("GridControl",true);
			this.MinimizeBand(typeof(DetailBand),600);
		}

		//06-13-2008@Scott Minimize all bands
		private void MinimizeBand(Type bandType,int nMinHeight)
		{
			foreach(XRControl xrControl in this.Controls)
			{
				if(!(xrControl.GetType() == bandType)) continue;

				xrControl.Height = nMinHeight;
			}		
		}

		private void AdjustExControlsPosition()
		{
			ExControls.Clear();

			firstExcontrol=null;

			DetailBand m_Detail = null;
			foreach(object m_obj in this.Controls)
			{
				m_Detail = m_obj as DetailBand;
				if(m_Detail ==null)continue;
				//m_Detail.Controls
				//System.Diagnostics.Debug.WriteLine(m_Detail.Container.Components.Count.ToString());
				foreach(object m_control in m_Detail.Controls)
				{
					if(m_control is WinControlContainer)
					{
						WinControlContainer m_Container = m_control as WinControlContainer;
						if(m_Container.WinControl is IExControlDev)
						{
							continue;
						}
						else
						{
							ExControls.Add(m_control);

							if(m_Container.WinControl is INonePrintControl)
							{
								if(firstExcontrol!=null)
								{
									firstExcontrol=m_Container.WinControl;
								}								
							}
						}
					}
				}
			}
			ExControls.Sort();
		}

		public bool LoadAdvSectionFilters(string strUserFolder)
		{//Modified at 2009-1-19 14:21:38@Scott		            
			
			AdvFilterConvertor convertor = new AdvFilterConvertor();

			if(this.Template.ReportScType!=ReportScType.Custom)
			{
				this.Template.SectionFilters = convertor.GetReportFilters(Webb.Reports.DataProvider.VideoPlayBackManager.AdvReportFilters,this.Template.ReportScType);
			}

			return true;
		}

   
	}

	#region public class WinControlContainerCollection
	/*Descrition:   */
	public class WinControlContainerCollection : CollectionBase,IComparer
	{
		//Wu.Country@2007-11-27 08:26:49 AM added this collection.
		//Fields
		//Properties
		public WinControlContainer this[int i_Index]
		{ 
			get { return this.InnerList[i_Index] as WinControlContainer; } 
			set { this.InnerList[i_Index] = value; } 
		} 
		//ctor
		public WinControlContainerCollection() {} 
		//Methods
		public int Add(object i_Object)
		{ 
			return this.InnerList.Add(i_Object);
		} 
		public void Remove(WinControlContainer i_Obj)
		{
			this.InnerList.Remove(i_Obj);
		} 

		public void Sort()
		{
			this.InnerList.Sort(this);
		}

		public void Reverse()
		{
			this.InnerList.Reverse();
		}

		#region IComparer Members
		public int Compare(object x, object y)
		{
			// TODO:  Add WinControlContainerCollection.Compare implementation
			WinControlContainer m_X = x as WinControlContainer;
			WinControlContainer m_Y = y as WinControlContainer;			
			//System.Diagnostics.Debug.Assert(m_X!=null&&m_Y!=null);
			if(m_X==null||m_Y==null) return 0;
			int m_Result = 0;
			if(m_X.Top==m_Y.Top)
			{
				m_Result=m_X.Left - m_Y.Left;

                if (m_Result == 0)
                {
                    m_Result = m_X.Width - m_Y.Width;
                }
			}
			else
			{
				m_Result = m_X.Top - m_Y.Top;
			}         

			return m_Result;
		}
		#endregion
	} 
	#endregion
}
