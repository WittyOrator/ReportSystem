using System;
using System.Data;
using System.Collections;
using System.Runtime.Serialization;
using System.Windows.Forms;
using Webb.Reports.DataProvider;
using Webb.Collections;
using Webb.Reports.ReportWizard.Wizards;
using System.IO;
using Webb.Reports.Browser;
using DevExpress.XtraReports.UI;
using Webb.Reports.ExControls;
using Webb.Reports.ExControls.Views;
using Webb.Reports.ReportWizard.Wizards.Steps;
using System.Drawing;
using Webb.Reports.ExControls.Data;
using Webb.Reports.ReportWizard.DataSourceProvider;
using Webb.Reports.ExControls.WebbRepWizard.Data;
using Microsoft.Win32;
using System.Collections.Generic;

namespace Webb.Reports.ReportWizard.WizardInfo
{
	[Serializable]
	public enum TemplateType
	{
		PlayList,
		Group,		
		HitChart
	}
	[Serializable]
	public enum GroupTemplateType
	{
		GroupAndGroup,
		GroupLevels,		
		IndividualGroups,
	}
   
	#region ReportSetting
	/// <summary>
	/// Summary description for ReportSetting.
	/// </summary>
	[Serializable]
	public class ReportSetting:ISerializable
	{	
		public static WebbDataSource PublicDataSource=null;

        public static readonly string EnvionmentFile = Webb.Utility.ApplicationDirectory + @"Template\EnviromentSetting.cfg";

        public static readonly string HelpFile = Webb.Utility.ApplicationDirectory + @"Template\WRW Help.chm";

        public static WizardEnviroment WizardEnviroment = new WizardEnviroment();

        public static string CCRMServerConfigPath
        {
            get
            {
                string strPath = System.Windows.Forms.Application.StartupPath;

                if (!strPath.EndsWith(@"\")) strPath = strPath + @"\";            

                strPath = strPath + @"Template\DataConfig\CCRMDataFiles";

                if (!System.IO.Directory.Exists(strPath))
                {
                    System.IO.Directory.CreateDirectory(strPath);
                }

                strPath = strPath + @"\ServerConfig.dat";

                return strPath;
            }
        }

		#region Non Static Fields
		protected TemplateType _TemplateType=TemplateType.PlayList;
		
		[NonSerialized]
		protected string _ReportFileName=string.Empty;		
		
		[NonSerialized]
		protected WizardPageCollection _WizardPageCollection=new WizardPageCollection();

		[NonSerialized]
		protected WebbReport _WebbReport=null;

        [NonSerialized]
        public List<string> UsedFields = new List<string>();
	
		
		protected bool _AddScFilters=false;


		protected WaterMarkOption _WatermarkOption;		
		
		protected WizardCustomStyles _WizardCustomStyles=null;		

		protected GroupTemplateType groupTemplateType=GroupTemplateType.GroupAndGroup;

		protected string _HeaderStyleName=string.Empty;

		protected string _GameListStyleName=string.Empty;

		[NonSerialized]
		protected bool _SetFilename=false;

		[NonSerialized]
		protected bool _OpenLastBefore=false;

		[NonSerialized]
		protected bool _ChangedFileName=false;

		
		#endregion

		#region Property
		public string ReportHeaderStyleName
		{
			get{return _HeaderStyleName;}
			set{_HeaderStyleName=value;}
		}
		public string GameListStyleName
		{
			get{return _GameListStyleName;}
			set{_GameListStyleName=value;}
		}

		public bool ChangedFileName
		{
			get
			{
				return this._ChangedFileName;
			}
			set
			{
				_ChangedFileName=value;
			}

		}
		public TemplateType TemplateType
		{
			get{ return _TemplateType; }
			set{ _TemplateType = value; }
		}
		public GroupTemplateType GroupTemplateType
		{
			get{ return groupTemplateType; }
			set{ groupTemplateType = value; }
		}
		public WaterMarkOption WaterMarkOption
		{
			get
			{
				if(_WatermarkOption==null)_WatermarkOption=new WaterMarkOption();
				return this._WatermarkOption; }
			set{ _WatermarkOption = value; }
		}
		public WizardCustomStyles WizardCustomStyles
		{
			get
			{				
				return this._WizardCustomStyles; }
			set{ _WizardCustomStyles = value; }
		}

		public string ReportFileName
		{
			get{ return _ReportFileName; }
			set{ _ReportFileName = value; }
		}
		
		public bool AddScFilters
		{
			get{ return this._AddScFilters; }
			set{ _AddScFilters = value; }
		}
		public bool SetFilename
		{
			get{ return this._SetFilename; }
			set{ _SetFilename = value; }
		}


		public Webb.Reports.ReportWizard.WizardInfo.WizardPageCollection WizardPageCollection
		{
			get
			{
				if(_WizardPageCollection==null)_WizardPageCollection=new WizardPageCollection();
				return _WizardPageCollection; }
			set{ _WizardPageCollection = value; }
		}

		public Webb.Reports.WebbReport WebbReport
		{
			get
			{
				return _WebbReport; }
			set{ _WebbReport = value; }
		}	
		public bool OpenLastBefore
		{
			get{return _OpenLastBefore;}
			set{_OpenLastBefore=value;}
		}
		#endregion
	
		public ReportSetting(bool Opened)
		{
		}

		public ReportWizardSetting ToWizardSetting()
		{
			ReportWizardSetting wizardSetting=new ReportWizardSetting();

			switch(this.groupTemplateType)
			{
				case GroupTemplateType.GroupLevels:
					wizardSetting.GroupTemplateType=1;
					break;
				case GroupTemplateType.IndividualGroups:
					wizardSetting.GroupTemplateType=2;
					break;	
				case GroupTemplateType.GroupAndGroup:
				default:
					wizardSetting.GroupTemplateType=0;
					break;

			}
			switch(this.TemplateType)
			{
				case TemplateType.Group:
					wizardSetting.TemplateType=1;
					break;
				case TemplateType.HitChart:
					wizardSetting.TemplateType=2;
					break;
				default:
					wizardSetting.TemplateType=0;
					break;
			}
		
			if(WizardCustomStyles==null)this.WizardCustomStyles=new WizardCustomStyles();

			if(WaterMarkOption==null)this.WaterMarkOption=new WaterMarkOption();
			
			wizardSetting.SelectedStyleName=WizardCustomStyles.StyleName;
			
			wizardSetting.AddWatermark=WaterMarkOption.UseWaterMark;

			wizardSetting.AddScFilters=this.AddScFilters;

			wizardSetting.HeaderStyleName=this.ReportHeaderStyleName;

			wizardSetting.GameListStyleName=this.GameListStyleName;

			wizardSetting.CreateByWizard=true;

            wizardSetting.WizardVersion = Webb.Assembly.WizardInfoVersion;

            wizardSetting.BrowserVersion = Webb.Assembly.Version;

            wizardSetting.ProductTypes = (int)ReportSetting.WizardEnviroment.ProductType;

			return wizardSetting;

		}

		

		protected void GetDefaultReportpath()
		{			
			_ReportFileName=WizardEnviroment.GetReportPath(this.TemplateType);
		}


		#region Virtual Methods
		public virtual void CreateSteps()
		{
			
		}
		public static string GetTemplatePath(string templateType)
		{
			string dir= Webb.Utility.ApplicationDirectory+ @"Template\TemplateReports";

			if(!Directory.Exists(dir))
			{
				Directory.CreateDirectory(dir);
			}
            switch(WizardEnviroment.ProductType)
            {
                case WebbDBTypes.WebbVictoryFootball:
                case WebbDBTypes.WebbVictoryBasketball:
                case WebbDBTypes.WebbVictoryHockey:
                case WebbDBTypes.WebbVictoryVolleyball:
                case WebbDBTypes.WebbVictoryLacrosse:
                case WebbDBTypes.WebbVictorySoccer:
                    return string.Format(@"{0}\Victory{1}.repx", dir, templateType);                    
            }
			return string.Format(@"{0}\{1}.repx",dir,templateType);
		}

		protected virtual void LoadWebbReport()
		{
            string TemplateFilename=GetTemplatePath(this.TemplateType.ToString());
			
			if(!File.Exists(TemplateFilename))
			{
				MessageBox.Show("Cannot find the template file '"+TemplateFilename+"'","Failed to load template",MessageBoxButtons.OK,MessageBoxIcon.Stop);
				
                Environment.Exit(-1);
			}

           Webb.Utility.ReplaceRefPathInRepxFile(TemplateFilename,Application.ExecutablePath);

           _WebbReport=new WebbReport();

           _WebbReport.LoadLayout(TemplateFilename);
           
		}	


		private void CreateReport()   //for test
		{
			_WebbReport=new WebbReport();

			DevExpress.XtraReports.UI.ReportHeaderBand reportHeaderBand = new DevExpress.XtraReports.UI.ReportHeaderBand();
		
			reportHeaderBand.Height=100;

            _WebbReport.Bands.Add(reportHeaderBand);

			_WebbReport.InitializeDefaultReport();

			WinControlContainer container=new WinControlContainer();

			container.Location=new Point(100,0);

			container.Size=new Size(100,200);

			if(this.TemplateType==TemplateType.PlayList)
			{
				GridControl gridControl=new GridControl();

				container.WinControl=gridControl;		
				
			}
			else
			{
				GroupingControl groupControl=new GroupingControl();

				container.WinControl=groupControl;	
			}

			Band detailBand=_WebbReport.Bands[DevExpress.XtraReports.UI.BandKind.Detail];

			detailBand.Controls.Add(container);			

			container=new WinControlContainer();

			container.Location=new Point(100,0);

			container.Size=new Size(40,40);
				
			ReportInfoLabel  Reportinfo=new ReportInfoLabel();

			container.WinControl=Reportinfo;

            reportHeaderBand.Controls.Add(container);

		   Band	band=WebbReport.Bands[BandKind.ReportHeader];

			container=new WinControlContainer();

			container.Location=new Point(100,0);

			container.Size=new Size(40,40);
				
			LabelControl  lable=new LabelControl();

			container.WinControl=lable;

			band.Controls.Add(container);

			return;
		

			
		}
		
		protected void IntalizeSetting()
		{	
			Webb.Utilities.WaitingForm.SetWaitingMessage("Creating wizard pages, please wait...");

			this.CreateSteps();

             Webb.Utilities.WaitingForm.SetWaitingMessage("Loading template, please wait...");

            this.LoadWebbReport();    	
			       
		}
	
		#endregion

		#region Get/set Control Property/View In WebbReport
		    #region GetPage HeaderView

				public void UpdateLocation()
				{
					if(this.WebbReport==null)return;

					Size pageSize=WebbReport.GetNoDealedSizePerPage();

					Band band=WebbReport.Bands[BandKind.PageHeader];

                    if (band != null)
                    {
                        foreach (XRControl xrControl in band.Controls)
                        {
                            if (!(xrControl is XRPageInfo)) continue;

                            int locX = pageSize.Width -xrControl.Width;

                            xrControl.Left = locX;
                        }
                    }

					band=WebbReport.Bands[BandKind.ReportHeader];

                    if (band != null)
                    {
                        foreach (XRControl xrControl in band.Controls)
                        {
                            if (!(xrControl is WinControlContainer)) continue;

                            Control c = (xrControl as WinControlContainer).WinControl;

                            if (xrControl.Name != "LogoTeamInfo" && (c is CustomImage))
                            {                           
                                xrControl.Left = pageSize.Width - xrControl.Width;

                                continue;
                            }
                            if (c is LabelControl)
                            {
                                int locX = pageSize.Width / 2 - (xrControl.Width + 2) / 2 - 2;

                                xrControl.Left = locX;

                                continue;
                            }
                        }
                    }

					SetCenterMainControl(this);

					band=WebbReport.Bands[BandKind.PageFooter];

                    if (band != null)
                    {
                        foreach (XRControl xrControl in band.Controls)
                        {
                            if (!(xrControl is XRLabel)) continue;

                            int locX = pageSize.Width / 2 - (xrControl.Width + 2) / 2 - 2;

                            xrControl.Left = locX;

                        }
                    }
				}
	
				public  void GetHeaderViews(out LabelControlView labelView,out ReportInfoLabelView gameListView)
				{					
					labelView=null;

                    gameListView=null;					

					Band band=WebbReport.Bands[BandKind.ReportHeader];

					foreach(XRControl xrControl in band.Controls)
					{
						if(!(xrControl is WinControlContainer))continue;
			            
						Control c = (xrControl as WinControlContainer).WinControl;

						if(c is ReportInfoLabel)
						{
							gameListView=(c as ReportInfoLabel).ReportInfoView;

							continue;
						}
						if(c is LabelControl)
						{
							labelView=(c as LabelControl).LabelControlView;

							continue;
						}						
						
					}	
					
				}			

                public  void SetTeamLogos(bool displayLogo)
				{		

					Band band=WebbReport.Bands[BandKind.ReportHeader];

                    WinControlContainer logoImageControl = null;

                    WinControlContainer fileNameControlContainer = null;

                    XRControl gameListInfoContainer = null;

                    foreach (XRControl xrControl in band.Controls)
                    {
                        if (!(xrControl is WinControlContainer)) continue;

                        Control c = (xrControl as WinControlContainer).WinControl;
                       
                        if (c is FileNameControl)
                        {
                            fileNameControlContainer = xrControl as WinControlContainer;                           
                        }
                        else if (xrControl.Name == "LogoTeamInfo" && (c is CustomImage))
                        {
                            logoImageControl = xrControl as WinControlContainer;                       
                        }
                        else if (c is ReportInfoLabel)
                        {
                            gameListInfoContainer = xrControl;
                        }
                    }
                    if (gameListInfoContainer != null)
                    {
                        band.Controls.Remove(gameListInfoContainer);
                    }

                    if (displayLogo)
                    {
                        #region If has Log remove filenameControl and add Logo
                        if (fileNameControlContainer != null)
                        {
                            band.Controls.Remove(fileNameControlContainer);
                        }

                        CustomImage logoCustomImage;

                        if (logoImageControl == null)
                        {
                            logoImageControl = new WinControlContainer();                          

                            logoImageControl.Name = "LogoTeamInfo";

                            logoCustomImage = new CustomImage();

                            logoCustomImage.Field = "Logo(TeamInfo)";

                            logoCustomImage.OneValue = true;

                            logoCustomImage.SizeMode = PictureBoxSizeMode.StretchImage;

                            logoImageControl.WinControl = logoCustomImage;                        

                            band.Controls.Add(logoImageControl);

                            logoImageControl.Left = 0;

                            logoImageControl.Top = 0;

                            logoImageControl.Size = new Size(80, 100);                             
                        }
                        else
                        {
                            logoCustomImage=logoImageControl.WinControl as CustomImage;

                            logoCustomImage.Field = "Logo(TeamInfo)";

                            logoCustomImage.OneValue = true;

                            logoImageControl.Bounds = new Rectangle(0, 0, 80, 100);

                            logoCustomImage.SizeMode = PictureBoxSizeMode.StretchImage;

                            logoCustomImage.CalculateResult();


                        }
                        #endregion
                    }
                    else
                    {
                        #region has no Logo for team
                        if (logoImageControl != null)
                        {
                            band.Controls.Remove(logoImageControl);
                        }                   

                        if (fileNameControlContainer == null)
                        {
                            fileNameControlContainer = new WinControlContainer();

                            fileNameControlContainer.WinControl = new CustomImage();

                            fileNameControlContainer.Bounds = new Rectangle(0, 0, 60, 22);

                            band.Controls.Add(logoImageControl);
                        }
                      
                        #endregion
                    } 
					
				}

                public bool HaveLogos()
                {
                    Band band = WebbReport.Bands[BandKind.ReportHeader];

                    return band.Controls["LogoTeamInfo"]!=null;
                }
		    #endregion
	       
	    	#region Get/Set GridView&GroupView
			
				public Font GetPageFont()
				{
					if(this.WebbReport==null)return null;

					Band detailband=WebbReport.Bands[BandKind.Detail];
										
					foreach(XRControl xrControl in detailband.Controls)
					{
						if(!(xrControl is WinControlContainer))continue;
											
						Control c = (xrControl as WinControlContainer).WinControl;

						if(this.TemplateType==TemplateType.PlayList)
						{
							if(!(c is GridControl))continue;	
						
							GridView gridview=(c as GridControl).GridView;

							return gridview.Styles.RowStyle.Font;		
							
						}
						else if(this.TemplateType==TemplateType.Group)
						{
							if(!(c is GroupingControl))continue;
							GroupView groupview=(c as GroupingControl)._GroupView;
							return groupview.Styles.RowStyle.Font;		
							
						}
					}
					return WebbReport.Font;
				}
				public void SetPageFont(Font font)
				{
					if(this.WebbReport==null)return;

					Band detailband=WebbReport.Bands[BandKind.Detail];

                    WebbReport.Font = font;
										
					foreach(XRControl xrControl in detailband.Controls)
					{
						if(!(xrControl is WinControlContainer))continue;
											
						Control c = (xrControl as WinControlContainer).WinControl;

						if(this.TemplateType==TemplateType.PlayList)
						{
							if(!(c is GridControl))continue;							
							GridView gridview=(c as GridControl).GridView;

							gridview.Styles.RowStyle.Font=(Font)font.Clone();	
							gridview.Styles.HeaderStyle.Font=(Font)font.Clone();
							gridview.Styles.AlternateStyle.Font=(Font)font.Clone();	

							gridview.Styles.SectionStyle.Font=new System.Drawing.Font(font.FontFamily.Name,font.Size+2,font.Style|System.Drawing.FontStyle.Bold);

                            gridview.Styles.SectionStyle.HorzAlignment=DevExpress.Utils.HorzAlignment.Near;


							foreach(GridColumn column in gridview.GridInfo.Columns)
							{
								if(column.Style.IsEdited())
								{
									column.Style.Font=(Font)font.Clone();
								}
							}

							
						}
						else if(this.TemplateType==TemplateType.Group)
						{
							if(!(c is GroupingControl))continue;

							GroupView groupview=(c as GroupingControl)._GroupView;

							groupview.Styles.RowStyle.Font=(Font)font.Clone();	
							groupview.Styles.AlternateStyle.Font=(Font)font.Clone();	
							groupview.Styles.HeaderStyle.Font=(Font)font.Clone();
	
							groupview.Styles.SectionStyle.Font=new System.Drawing.Font(font.FontFamily.Name,font.Size+2,font.Style|System.Drawing.FontStyle.Bold);

							groupview.Styles.SectionStyle.HorzAlignment=DevExpress.Utils.HorzAlignment.Near;
					
						    GroupWizardOption.UpdateFont(groupview.RootGroupInfo, font);
						}
					}
					

				}


	      	public void SetSectionFont(Font font)
		{
			if(this.WebbReport==null)return;

			Band detailband=WebbReport.Bands[BandKind.Detail];
										
			foreach(XRControl xrControl in detailband.Controls)
			{
				if(!(xrControl is WinControlContainer))continue;
											
				Control c = (xrControl as WinControlContainer).WinControl;

				if(this.TemplateType==TemplateType.PlayList)
				{
					if(!(c is GridControl))continue;	
						
					GridView gridview=(c as GridControl).GridView;
					
					gridview.Styles.SectionStyle.Font=(Font)font.Clone();	

					gridview.Styles.SectionStyle.HorzAlignment=DevExpress.Utils.HorzAlignment.Near;

					return;
							
				}
				else if(this.TemplateType==TemplateType.Group)
				{
					if(!(c is GroupingControl))continue;

					GroupView groupview=(c as GroupingControl)._GroupView;					
	
					groupview.Styles.SectionStyle.Font=(Font)font.Clone();

					groupview.Styles.SectionStyle.HorzAlignment=DevExpress.Utils.HorzAlignment.Near;
					
		
				}
			}
					

		}

			public bool GetShowRowIndicators()
				{
					if(this.WebbReport==null)return false;

					Band detailband=WebbReport.Bands[BandKind.Detail];
										
					foreach(XRControl xrControl in detailband.Controls)
					{
						if(!(xrControl is WinControlContainer))continue;
											
						Control c = (xrControl as WinControlContainer).WinControl;

						if(this.TemplateType==TemplateType.PlayList)
						{
							if(!(c is GridControl))continue;

							return (c as GridControl).GridView.ShowRowIndicators;
						}
						else if(this.TemplateType==TemplateType.Group)
						{
							if(!(c is GroupingControl))continue;

							return (c as GroupingControl)._GroupView.ShowRowIndicators;
						}
					}
					return false;
				}

			#endregion


			#region Set/Get GridInfo In PlayList Report
				public GridInfo GetGridInfoInPlayList()
				{
					if(this.WebbReport==null)return null;

					Band detailband=WebbReport.Bands[BandKind.Detail];
				    
					foreach(XRControl xrControl in detailband.Controls)
					{
						if(!(xrControl is WinControlContainer))continue;
			            
						Control c = (xrControl as WinControlContainer).WinControl;

						if(!(c is GridControl))continue;

						return (c as GridControl).GridView.GridInfo;
					}
					return null;

				}
				public GridView GetGridViewInPlayList()
				{
					if(this.WebbReport==null)return null;

					Band detailband=WebbReport.Bands[BandKind.Detail];
				    
					foreach(XRControl xrControl in detailband.Controls)
					{
						if(!(xrControl is WinControlContainer))continue;
			            
						Control c = (xrControl as WinControlContainer).WinControl;

						if(!(c is GridControl))continue;

						return (c as GridControl).GridView;
					}
					return null;

				}
				public bool HasSectionGroupInfo()
				{					
					if(this.WebbReport==null)return false;

					SectionFilterCollectionWrapper scFilterWraper=this.WebbReport.Template.SectionFiltersWrapper;

					if(scFilterWraper.ReportScType!=ReportScType.Custom||scFilterWraper.SectionFilters.Count>0)
					{
						return true;
					}

					return false;
				}
					
		          
				public int  GetSortWidth(GridInfo gridInfo,bool ShowRowIndicators)
				{
					if(this.WebbReport==null||gridInfo==null)return -1;					

					int index=0;

					if(ShowRowIndicators)
					{
						index++;
					}

					Int32Collection columnsWidth=null;

					Band detailband=WebbReport.Bands[BandKind.Detail];
						    
					foreach(XRControl xrControl in detailband.Controls)
					{
						if(!(xrControl is WinControlContainer))continue;
					            
						Control c = (xrControl as WinControlContainer).WinControl;

						if(!(c is GridControl))continue;

						columnsWidth=(c as GridControl).GridView.ColumnsWidth;

						break;
					}
					if(columnsWidth==null||index>=columnsWidth.Count)return -1;

					int sortWidth=-1;

					if(gridInfo.SortingFrequence==SortingFrequence.ShowBeforeColumns)
					{
						sortWidth=columnsWidth[index];
					}
					else if(gridInfo.SortingFrequence==SortingFrequence.ShowAfterColumns)
					{
                        sortWidth=columnsWidth[columnsWidth.Count-1];
					}
                     
					return sortWidth;
				}
			
		        
				public void SetGridInfoWidthInPlayList(Int32Collection columnsWidth)
				{
					if(this.WebbReport==null)return;

					Band detailband=WebbReport.Bands[BandKind.Detail];
				    
					foreach(XRControl xrControl in detailband.Controls)
					{
						if(!(xrControl is WinControlContainer))continue;
			            
						Control c = (xrControl as WinControlContainer).WinControl;

						if(!(c is GridControl))continue;

						(c as GridControl).GridView.ColumnsWidth=columnsWidth;

						return;
					}
				

				}
			
				public Int32Collection	GetGridInfoWidthInPlayList()
				{
					Int32Collection columnsWidth=new Int32Collection();

					if(this.WebbReport==null)return columnsWidth ;

					Band detailband=WebbReport.Bands[BandKind.Detail];
						    
					foreach(XRControl xrControl in detailband.Controls)
					{
						if(!(xrControl is WinControlContainer))continue;
					            
						Control c = (xrControl as WinControlContainer).WinControl;

						if(!(c is GridControl))continue;

						(c as GridControl).GridView.ColumnsWidth.CopyTo(columnsWidth);		
			        
						return columnsWidth;
					}

					return columnsWidth;
				}
					

			#endregion

			public static void UpdateSections(GroupInfo groupInfo)
			{
				groupInfo.UpdateSectionSummaries();

				foreach(GroupInfo subGroupInfo in groupInfo.SubGroupInfos)
				{
                    subGroupInfo.TotalTitle = "SubTotal";
					UpdateSections(subGroupInfo);
				}
			}

            public static void UpdateDisregardBlank(GroupInfo groupInfo)
            {                
                if (groupInfo is FieldGroupInfo&&groupInfo.DisregardBlank)
                {
                    groupInfo.Filter.Clear();

                    //Webb.Data.DBCondition condition = new Webb.Data.DBCondition();

                    //condition.ColumnName = (groupInfo as FieldGroupInfo).GroupByField;

                    //condition.FilterType = Webb.Data.FilterTypes.IsNotStrEmpty;

                    //groupInfo.Filter.Add(condition);

                    foreach (GroupSummary m_Summary in groupInfo.Summaries)
                    {  
                        m_Summary.DenominatorFilter.Clear();

                        Webb.Data.DBCondition condition = new Webb.Data.DBCondition();

                        condition.ColumnName = (groupInfo as FieldGroupInfo).GroupByField;

                        condition.FilterType = Webb.Data.FilterTypes.IsNotStrEmpty;

                        m_Summary.DenominatorFilter.Add(condition);

                    }

                }               

                foreach (GroupInfo subGroupInfo in groupInfo.SubGroupInfos)
                {
                    UpdateDisregardBlank(subGroupInfo);
                }
            }    
				     
		#endregion

		#region Check whether templateType is valid
		public static bool CheckIsValidTemplate(ref ReportSetting setting)
		{
			if(setting.WebbReport==null)return false;

			Band detailband=setting.WebbReport.Bands[BandKind.Detail];
				    
			foreach(XRControl xrControl in detailband.Controls)
			{
				if(!(xrControl is WinControlContainer))continue;
			            
				Control c = (xrControl as WinControlContainer).WinControl;

				if(setting.TemplateType==TemplateType.PlayList)
				{
					if(!(c is GridControl))continue;

					return true;
				}
				else if(setting.TemplateType==TemplateType.Group)
				{
					if(!(c is GroupingControl))continue;

					return true;
				}
			}
			return false;
		}	
		public static string ReplaceAllInvalidChars(string Text)
		{
            char[] invalidChars = Webb.Utility.InvalidSignsInFileName.ToCharArray();

			foreach(char c in invalidChars)
			{
				Text=Text.Replace(c.ToString(),"");
			} 
			return Text;
		} 

		public static bool CheckInvalidFileName(string text)
		{
            char[] invalidChars = Webb.Utility.InvalidSignsInFileName.ToCharArray();

			foreach(char c in invalidChars)
			{
				if(text.IndexOf(c)>=0)return true;
			} 
			return false;
		} 
		#endregion

		#region UpdateLocation
        public void SetGridControlLocation(int left)
        {
            if (this.WebbReport == null) return;

            if (left < 0) left = 0;

            Band detailband = WebbReport.Bands[BandKind.Detail];

            foreach (XRControl xrControl in detailband.Controls)
            {
                if (!(xrControl is WinControlContainer)) continue;

                Control c = (xrControl as WinControlContainer).WinControl;

                if (!(c is GridControl)) continue;

                xrControl.Left = left;
            }
        }

		public static void SetCenterMainControl(ReportSetting setting)
		{
			if(setting.TemplateType==Webb.Reports.ReportWizard.WizardInfo.TemplateType.PlayList)
			{
				GridView gridView=setting.GetGridViewInPlayList();

				gridView.Control.CalculateResult();

				gridView.Styles.SectionStyle.HorzAlignment=DevExpress.Utils.HorzAlignment.Near;

                int Start = gridView.ResolveIndentStartCol();

                gridView.ApplyVirtualGroupInfoWidth(ref Start, gridView.RootGroupInfo, gridView.ColumnsWidth);

				gridView.GridInfo.UpdateColumnsWidth(gridView.ColumnsWidth,Start);	
				
				int totalColumns=gridView.GetColumnsCount();

				if(gridView.ColumnsWidth.Count<totalColumns)
				{
					for(int i=gridView.ColumnsWidth.Count;i<totalColumns;i++)
					{
						gridView.ColumnsWidth.Add(55);
					}
				}
				int totalWidth=0;

				for(int i=0;i<totalColumns;i++)
				{
					totalWidth+=gridView.ColumnsWidth[i];				
				}
				
				Size pageSize=setting.WebbReport.GetSizePerPage();

				int locX=pageSize.Width/2-totalWidth/2-1;

                if (locX < 0) locX = 0;
				
				setting.SetGridControlLocation(locX);
			}
			else if(setting.TemplateType==TemplateType.Group)
			{
				Font font=setting.GetPageFont();

				GroupWizardOption groupWizardOption=new GroupWizardOption();

				if(setting.groupTemplateType==GroupTemplateType.IndividualGroups)
				{	
					GroupInfoCollection	IndivadualGroups=GroupWizardOption.GetIndividualGroup(setting);
					
					groupWizardOption.UpdateSetting(setting,IndivadualGroups,font);
				}
				else
				{	
					groupWizardOption.UpdateSetting(setting);
				}

			}
				
		}		

		#endregion

		#region Create DataSource
		public static string DataConfigSavedPath
		{
			get
			{
                string dir = Webb.Utility.ApplicationDirectory + @"Template\DataConfig";

				if(!Directory.Exists(dir))
				{
					Directory.CreateDirectory(dir);
				}

				return dir+@"\DataConfig.xml";
			}
		}

		  
			public static bool WizardDataFile()
			{
				Webb.Utilities.WaitingForm.ShowWaitingForm();

				DataSourceWizard dataSourceWizard=new DataSourceWizard();

                Webb.Utilities.WaitingForm.ShowWaitingForm();              

                DBSourceConfig config = new DBSourceConfig();

                config.ConnString = string.Empty;

                config.WebbDBType = ReportSetting.WizardEnviroment.ProductType;

                dataSourceWizard.SetDataConfig(config);             

				Webb.Utilities.WaitingForm.CloseWaitingForm();

				if(dataSourceWizard.ShowDialog()!=DialogResult.OK)return false;
			
				Webb.Utilities.WaitingForm.ShowWaitingForm();

				Webb.Utilities.WaitingForm.SetWaitingMessage("Loading Games....");

                DBSourceConfig _DbSourceConfig=dataSourceWizard.DBSourceConfig;
				
				WebbDataProvider m_DBProvider =new WebbDataProvider(_DbSourceConfig);
				
				WebbDataSource m_DBSource = new WebbDataSource();

                bool m_result = m_DBProvider.GetDataSource(_DbSourceConfig,m_DBSource);

				if(!m_result|| m_DBSource.DataSource.Tables.Count==0)return false;   
				
				SetDataSource(m_DBProvider,m_DBSource);

                ConfigFileManager.WriteDataConfig(_DbSourceConfig.WebbDBType, DataConfigSavedPath);	
			
				Webb.Utilities.WaitingForm.CloseWaitingForm();

				return true;
			}
			
	    	
			public static void  SetDataSource(WebbDataProvider m_DBProvider,WebbDataSource m_DBSource)
			{ 
				ArrayList m_Fields = new ArrayList();

				PublicDataSource=m_DBSource;						

				Webb.Reports.DataProvider.VideoPlayBackManager.PublicDBProvider = m_DBProvider;

                if (m_DBSource != null)
				{
                    if (m_DBSource.DataSource!=null&&m_DBSource.DataSource.Tables.Count > 0)
                    {
                       ArrayList strNoContains = new  ArrayList(){"gameid","start time","starttime","endtime",
                                                       "angle","end time", "duration","gamedetail.duration","subplay.duration"}; 
                                                     

                       foreach (System.Data.DataColumn m_col in m_DBSource.DataSource.Tables[0].Columns)
                       {
                           if (m_col.Caption == "{EXTENDCOLUMNS}" && m_col.ColumnName.StartsWith("C_")) continue;

                           string columnName = m_col.ColumnName;

                           if (m_Fields.Contains(columnName)) continue;

                           #region Victory Fields

                           if (m_DBProvider != null && m_DBProvider.DBSourceConfig != null && (int)m_DBProvider.DBSourceConfig.WebbDBType / 100 == 1)
                           {
                               if (strNoContains.Contains(m_col.ColumnName.ToLower())) continue;

                               bool matchPatten = System.Text.RegularExpressions.Regex.IsMatch(m_col.ColumnName, @"^[VXY]\d*$");

                               if (matchPatten) continue;
                           }

                           #endregion

                           m_Fields.Add(m_col.ColumnName);
                       }  
                    }                     
				
					Webb.Reports.DataProvider.VideoPlayBackManager.LoadAdvScFilters();

                    Webb.Reports.DataProvider.VideoPlayBackManager.ReadPictureDirFromRegistry();

                    m_DBProvider.UpdateEFFDataSource(m_DBSource);

                    Webb.Reports.DataProvider.VideoPlayBackManager.DataSource = m_DBSource.DataSource;	//Set dataset for click event                   

				}
				else
				{
					Webb.Reports.DataProvider.VideoPlayBackManager.DataSource =null;	//Set dataset for click event
				}	
		
				Webb.Data.PublicDBFieldConverter.SetAvailableFields(m_Fields);		
				
			}            
           

            public static bool ReadConfigFile(bool haveWarning,WebbDBTypes webbDbTypes)
            {
                string dataConfigFile = DataConfigSavedPath;

                string[] args = ConfigFileManager.ReadDataConfig(webbDbTypes, dataConfigFile);

                if (args==null)
                {                  
                    if (haveWarning)
                    {
                        Webb.Utilities.MessageBoxEx.ShowError("Failed to load data.\n\n" + "the default config file was lost or current product type is not fit to the product type in default config file.");
                    }

                    return false;
                }
                Webb.Utilities.WaitingForm.SetWaitingMessage("Loading Games....");
              
                CommandManager m_CmdManager = new CommandManager(args);

                DBSourceConfig m_Config = m_CmdManager.CreateDBConfig();

                if (m_Config.WebbDBType != ReportSetting.WizardEnviroment.ProductType) 
                {
                    if (haveWarning)
                    {
                        Webb.Utilities.MessageBoxEx.ShowError("Failed to load data.\n\nCurrent product type is not fit to the product type in default config file!");

                    }
                    return false;
                }

                WebbDataProvider m_DBProvider = new WebbDataProvider(m_Config);

                WebbDataSource m_DBSource = new WebbDataSource();

                try
                {
                    m_DBProvider.GetDataSource(m_Config, m_DBSource);
                }
                catch(Exception ex)
                {
                    if (haveWarning)
                    {
                        Webb.Utilities.MessageBoxEx.ShowError("Failed to load data.Exception Message:\n\n"+ex.Message);

                    }
                    return false;
                }

                if (m_DBSource.DataSource == null)
                {
                     if (haveWarning)
                    {
                        Webb.Utilities.MessageBoxEx.ShowError("Failed to load data.:\n\nCould not calculate the data from default config file!");
                    }

                    return false;
                }
                if (m_DBSource.DataSource.Tables.Count == 0||m_DBSource.DataSource.Tables[0].Columns.Count == 0) 
                {
                   if (haveWarning)
                   {
                        Webb.Utilities.MessageBoxEx.ShowError("Failed to load data.:\n\nEmpty data in your games or datafile,please make sure your games or datafile are not invalid!");
                   }
                    return false;
                }

                if (m_Config.WebbDBType == WebbDBTypes.CoachCRM)
                {                    
                    if (m_DBSource.DataSource.Tables.Count!=2)
                    {
                        if (haveWarning)
                        {
                            Webb.Utilities.MessageBoxEx.ShowError("Failed to load data.\n\nthe CCRM data file format may be harmed,please select and reload the data again!");
                        }
                        return false;
                    }

                    DataTable table = m_DBSource.DataSource.Tables[1];

                    if (!table.Columns.Contains("CurrentField") || !table.Columns.Contains("DefaultHeader"))
                    {
                        if (haveWarning)
                        {
                            Webb.Utilities.MessageBoxEx.ShowError("Failed to load data \n please check whether the file is a invalid CCRM-data File");
                        }
                        return false;
                    }                   
                }

                SetDataSource(m_DBProvider, m_DBSource);                

                return true;
            }
//			
		#endregion	

		#region Save Report
		public bool SaveReport()
            {
                if (!this._OpenLastBefore || _ChangedFileName)
                {
                    if (File.Exists(this.ReportFileName))
                    {
                        string strDisMessage = string.Format("Report '{0}'\n\nalready exists.\n\nWould you like to overwrite it?", ReportFileName);

                        DialogResult dr = MessageBox.Show(strDisMessage, "Overwrite existing report?",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                        if (dr != DialogResult.Yes)
                        {
                            return false;
                        }
                    }
                }

                string strOveerrideMessage = "You have chosen to save this edited report. but if you saved it your original report would be overwritten. \n\nDo you wish to overwite it?";
                strOveerrideMessage += "\n............................................\n\nHere are there options that you can select for this saving action:";
                strOveerrideMessage += "\n\nClick \"Yes\" to save this report and overwrite the original report";
                strOveerrideMessage += "\n\nClick \"No\" to save the report with a new name.";
                strOveerrideMessage += "\n\nClick \"Cancel\" would perform no action for this saving process";

                if (this._OpenLastBefore && System.IO.File.Exists(this.ReportFileName))
                {
                    DialogResult dr = MessageBox.Show(strOveerrideMessage, "Overwrite Existing Report?",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

                    if (dr == DialogResult.Cancel)
                    {
                        return false;
                    }
                    else if (dr == DialogResult.No)
                    {
                        SaveFileDialog savedialog = new SaveFileDialog();

                        savedialog.DefaultExt = WizardEnviroment.WizardFileExtension.Trim(new char[] { '.' });

                        string filter = string.Format("WebbReport file for Wizard(*{0})|*{0}", WizardEnviroment.WizardFileExtension);

                        savedialog.Filter = filter;

                        savedialog.InitialDirectory = System.IO.Path.GetDirectoryName(this.ReportFileName);

                        savedialog.FileName = System.IO.Path.GetFileNameWithoutExtension(this.ReportFileName);

                        if (savedialog.ShowDialog() == DialogResult.OK)
                        {
                            if (this.ReportFileName != savedialog.FileName)
                            {
                                this.SetFilename = true;

                                this.ReportFileName = savedialog.FileName;
                            }
                            else
                            {
                                this.SetFilename = false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }

                }

                Webb.Utilities.WaitingForm.ShowWaitingForm();


                string strMessage = "Saving report,please wait....\n" + this.ReportFileName;

                if (this._OpenLastBefore)
                {
                    if (!this.SetFilename)
                    {
                        strMessage = "Saving changed report directly,please wait....\n" + this.ReportFileName;
                    }
                    else
                    {
                        strMessage = "Saving as other report,please wait....\n" + this.ReportFileName;
                    }
                }

                Webb.Utilities.WaitingForm.SetWaitingMessage(strMessage);

                WebbReport.Template.ReportWizardSetting = this.ToWizardSetting();

                string ConfigFile = ReportFileName.Replace(".repx", ".wcg");

                string strFile = WizardEnviroment.GetReportPath(this.TemplateType);

                if (WizardEnviroment.ReportSaveCount < int.MaxValue) 
                {
                    WizardEnviroment.ReportSaveCount++;
                }
                else
                {
                    WizardEnviroment.ReportSaveCount=1;
                }

                if (System.IO.File.Exists(ConfigFile))
                {
                    System.IO.File.Delete(ConfigFile);
                }

                WebbDataProvider dataProvider = Webb.Reports.DataProvider.VideoPlayBackManager.PublicDBProvider;

                WebbDataSource dataSource = ReportSetting.PublicDataSource;

                SetDataSource(dataProvider, null);

                WebbReport.WebbDataSource = null;

                WebbReport.UpdataDBSourceForExControls(null, null);

                if (!this.WaterMarkOption.UseWaterMark)
                {
                    WebbReport.Watermark = new XRWatermark();
                }
                try
                {
                    this.WebbReport.SaveLayout(this.ReportFileName);
                }
                catch (Exception EX)
                {
                    Webb.Utilities.MessageBoxEx.ShowError(EX.Message);
                }

                SetDataSource(dataProvider, dataSource);

                Webb.Utility.WriteWizardInRepxFile(ReportFileName);  //2010-1-11 14:25:41@Simon Add this Code

                Webb.Utilities.WaitingForm.CloseWaitingForm();

                return true;
            }	

		   
			public static void SaveSetting(string reportpath,ReportSetting setting)
			{
				if(!reportpath.EndsWith(".repx"))return;

				string settingConfigname=reportpath.Replace(".repx",".wcg");

				if(File.Exists(settingConfigname))
				{
					File.Delete(settingConfigname);
				}
				
				System.IO.FileStream stream = System.IO.File.Open(settingConfigname,System.IO.FileMode.OpenOrCreate);
		
				try
				{
					Webb.Utilities.Serializer.SerializeObject(stream,setting);				
				}
				catch(Exception ex)
				{
					Webb.Utilities.MessageBoxEx.ShowError("Failed to save setting!\n"+ex.Message);				
				}
				finally
				{
					stream.Close();
				}
		}		
		 public static ReportSetting LoadSetting(string settingpath)
		 {		
			Webb.Utilities.WaitingForm.SetWaitingMessage("Loading setting, please wait....");
            
			ReportSetting setting=null;			

			if(File.Exists(settingpath))
			{				
				System.IO.FileStream stream = System.IO.File.Open(settingpath,System.IO.FileMode.Open);

				try
				{
					setting= Webb.Utilities.Serializer.DeserializeObject(stream) as ReportSetting;

					stream.Close();	
				}
				catch(Exception ex)
				{
					Webb.Utilities.MessageBoxEx.ShowError(ex.Message);
					stream.Close();					
				
				}
				
			}		

			return setting;			
		}
		#endregion		

		#region EnvironmentSetting
		public static  void SaveEnvironment()
		{
			if(WizardEnviroment==null)return;

            if (File.Exists(EnvionmentFile))
            {
                File.Delete(EnvionmentFile);
            }

            System.IO.FileStream stream = System.IO.File.Open(EnvionmentFile, System.IO.FileMode.OpenOrCreate);

			try
			{	
				Webb.Utilities.Serializer.SerializeObject(stream,WizardEnviroment);
			}
			catch(Exception ex)
			{
                Webb.Utilities.MessageBoxEx.ShowError(ex.Message);
			}
            finally
            {
                stream.Close();
            }

		}
 
		public static  void IntalizeEnvironment()
		{	
			if(File.Exists(EnvionmentFile))
			{	

				try
				{
                    System.IO.FileStream stream = System.IO.File.Open(EnvionmentFile, System.IO.FileMode.Open);

					WizardEnviroment= Webb.Utilities.Serializer.DeserializeObject(stream) as WizardEnviroment;

                    stream.Close();
				}
				catch
				{
					WizardEnviroment=new WizardEnviroment();
				}			
			}
		   
		}
		#endregion

		#region Serialization By Simon's Macro 2009-12-9 9:23:51
		public virtual void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			info.AddValue("_TemplateType",_TemplateType,typeof(TemplateType));
			info.AddValue("_AddScFilters",_AddScFilters);
			info.AddValue("_WatermarkOption",_WatermarkOption,typeof(Webb.Reports.ReportWizard.WizardInfo.WaterMarkOption));
			info.AddValue("_WizardCustomStyles",_WizardCustomStyles,typeof(Webb.Reports.ReportWizard.WizardInfo.WizardCustomStyles));
			info.AddValue("groupTemplateType",groupTemplateType,typeof(GroupTemplateType));
		
		}

		public ReportSetting(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			try
			{
				_TemplateType=(TemplateType)info.GetValue("_TemplateType",typeof(TemplateType));
			}
			catch
			{
				_TemplateType=TemplateType.PlayList;
			}
				
			try
			{
				_AddScFilters=info.GetBoolean("_AddScFilters");
			}
			catch
			{
				_AddScFilters=false;
			}
			try
			{
				_WatermarkOption=(Webb.Reports.ReportWizard.WizardInfo.WaterMarkOption)info.GetValue("_WatermarkOption",typeof(Webb.Reports.ReportWizard.WizardInfo.WaterMarkOption));
			}
			catch
			{
				_WatermarkOption=null;
			}
			try
			{
				_WizardCustomStyles=(Webb.Reports.ReportWizard.WizardInfo.WizardCustomStyles)info.GetValue("_WizardCustomStyles",typeof(Webb.Reports.ReportWizard.WizardInfo.WizardCustomStyles));
			}
			catch
			{
				_WizardCustomStyles=null;
			}
			try
			{
				groupTemplateType=(GroupTemplateType)info.GetValue("groupTemplateType",typeof(GroupTemplateType));
			}
			catch
			{
				groupTemplateType=GroupTemplateType.GroupAndGroup;
			}
		}
		#endregion	
		
	}
	
	#endregion	

	#region WizardPage & WizardPageCollection
	[Serializable]
	public class WizardPage  
	{
		string _Name;

		int _StepIndex=0;

		WizardPageControlBase _WizardPageControlBase=null;

		public WizardPage(string name,WizardPageControlBase wizardPageControlBase)
		{
			_Name=name;
			_WizardPageControlBase=wizardPageControlBase;
		}

		
		public bool UpdateSetting(ref ReportSetting setting)
		{
			return this.WizardPageControlBase.UpdateSetting(ref setting);
		}
		public void SetSetting(ReportSetting setting)
		{
			this._WizardPageControlBase.SetSetting(setting);
		}

		public string Name
		{
			get{ return _Name; }
			set{ _Name = value; }
		}

		public int StepIndex
		{
			get{ return _StepIndex; }
			set{ _StepIndex = value; }
		}

		public Webb.Reports.ReportWizard.Wizards.Steps.WizardPageControlBase WizardPageControlBase
		{
			get{ return _WizardPageControlBase; }
			set{ _WizardPageControlBase = value; }
		}

			
	}

	public class WizardPageCollection:WebbCollection
	{
		public WizardPageCollection():base(){}
		public WizardPage this[int index]
		{
			get{return this.innerList[index] as WizardPage;}
			set{this.innerList[index]=value;}
		}
		public WizardPage this[string name]
		{
			get
			{
				foreach(WizardPage control in this)
				{
					if(control.Name==name)return control;
				}
				return null;
			}
		}
		public int Add(string name,WizardPageControlBase _StepControlBase)
		{
			return this.Add(new WizardPage(name,_StepControlBase));
		}
		public override int Add(object value)
		{
			WizardPage control=value as WizardPage;

			if(control==null||this[control.Name]!=null)
			{
				return -1;
			}
			control.WizardPageControlBase.WizardTitle=control.Name;

			control.StepIndex=this.Count;				
                
			return base.Add (control);
		}
		
	}

	#endregion

	#region Options In Wizard  

    [Serializable]
	public class WaterMarkOption:ISerializable
	{
		bool _UseWaterMark=true;

		[NonSerialized]		
		SerializableWatermark _SerialiableWaterMark=new SerializableWatermark();

		public WaterMarkOption()
		{
		}

		public bool UseWaterMark
		{
			get{ return _UseWaterMark; }
			set{ _UseWaterMark = value; }
		}

		public Webb.Reports.SerializableWatermark SerializableWatermark
		{
			get{ 
				if(_SerialiableWaterMark==null)_SerialiableWaterMark=new SerializableWatermark();
				return _SerialiableWaterMark; }
			set{ _SerialiableWaterMark = value; }
		}



		#region Serialization By Simon's Macro 2009-12-25 10:20:33
		public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			info.AddValue("_UseWaterMark",_UseWaterMark);
		
		
		}

		public WaterMarkOption(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			try
			{
				_UseWaterMark=info.GetBoolean("_UseWaterMark");
			}
			catch
			{
				_UseWaterMark=true;
			}
			
		}
		#endregion
	


	}
	
	[Serializable]
	public class GroupWizardOption
	{		
		public GroupWizardOption()
		{

        }

        #region Static Update /Get  GroupView/GroupingControl methods
        public static GroupView GetGroupView(WebbReport webbReport)
		{			
			Band detailband=webbReport.Bands[BandKind.Detail];
				
			foreach(XRControl xrControl in detailband.Controls)
			{
				if(!(xrControl is WinControlContainer))continue;
			        
				Control c = (xrControl as WinControlContainer).WinControl;

				if(!(c is GroupingControl))continue;

				return (c as GroupingControl)._GroupView;
			}			
			return null;			

		}			
        
		public static GroupingControl GetGroupControl(WebbReport webbReport)
		{			
			Band detailband=webbReport.Bands[BandKind.Detail];
				
			foreach(XRControl xrControl in detailband.Controls)
			{
				if(!(xrControl is WinControlContainer))continue;
			        
				Control c = (xrControl as WinControlContainer).WinControl;

				if(!(c is GroupingControl))continue;

				return c as GroupingControl;
			}			
			return null;			

		}

		public static GroupingControl GetGroupControl(WebbReport webbReport, int index)
		{		
			int startIndex=0;

			Band detailband=webbReport.Bands[BandKind.Detail];
				
			foreach(XRControl xrControl in detailband.Controls)
			{
				if(!(xrControl is WinControlContainer))continue;
			        
				Control c = (xrControl as WinControlContainer).WinControl;

				if(!(c is GroupingControl))continue;

				if(index<0||index==startIndex)
				{
					return c as GroupingControl;
				}

				startIndex++;
			}			
			return null;			

		}
       #endregion


        public override bool Equals(object obj)
        {
            if (obj is GroupWizardOption)
            {
                return this.Field == (obj as GroupWizardOption).Field;    
            }

            return false;
        }

        #region Update Font For Report
        public static void UpdateRowFont(WebbReport webbReport,Font font)
		{		
			webbReport.Font=(Font)font.Clone();

			Band detailband=webbReport.Bands[BandKind.Detail];
				
			foreach(XRControl xrControl in detailband.Controls)
			{
				if(!(xrControl is WinControlContainer))continue;
			        
				Control c = (xrControl as WinControlContainer).WinControl;

				if(!(c is GroupingControl))continue;
				
				GroupingControl groupControl=c as GroupingControl;

				groupControl._GroupView.Styles.RowStyle.Font=(Font)font.Clone();

                groupControl._GroupView.Styles.AlternateStyle.Font=(Font)font.Clone();

				groupControl._GroupView.Styles.HeaderStyle.Font=(Font)font.Clone();

				groupControl._GroupView.Styles.SectionStyle.Font=new Font(font.FontFamily.Name,font.Size+2,font.Style|FontStyle.Bold);	

				groupControl._GroupView.Styles.SectionStyle.HorzAlignment=DevExpress.Utils.HorzAlignment.Near;

			}			
				

		}

		public static void UpdateFont(GroupInfo groupInfo,Font font)
		{
			if(groupInfo==null||font==null)return;

            if(groupInfo.Style.IsEdited()) groupInfo.Style.Font=font;
             
			if(groupInfo.Summaries!=null)
			{
				foreach(GroupSummary summary in groupInfo.Summaries)
				{
					if(summary.Style.IsEdited())
					 {
						 summary.Style.Font=font;
					 }
				}
			}
			if(groupInfo.SectionSummeries!=null)
			{
				foreach(GroupSummary sectionsummary in groupInfo.SectionSummeries)
				{
					if(sectionsummary.Style.IsEdited())sectionsummary.Style.Font=font;
				}
			}
			foreach(GroupInfo subGroupInfo in groupInfo.SubGroupInfos)
			{
				 UpdateFont(subGroupInfo,font);
			}
        }
        
        #endregion       

        #region Get/Set DisRegard All Plays Blank
        public static bool CheckDisregardAllPlaysBlank(WebbReport webbReport)
        {
            GroupView groupView = GetGroupView(webbReport);

            if(groupView==null)return false;

            return groupView.GroupAdvancedSetting.DisregardAllPlaysBlank;
        }
        private static void CreatDisregardPlaysFilter(ref Webb.Data.DBFilter filter, GroupInfo groupInfo)
        { 
            Webb.Data.DBCondition condition = new Webb.Data.DBCondition();

            if (groupInfo is FieldGroupInfo)
            {
                condition.ColumnName = (groupInfo as FieldGroupInfo).GroupByField;

                condition.FilterType = Webb.Data.FilterTypes.IsNotStrEmpty;

                condition.FollowedOperand = Webb.Data.FilterOperands.Or;

                filter.Add(condition);
            }

            foreach (GroupInfo subGroupInfo in groupInfo.SubGroupInfos)
            {
                CreatDisregardPlaysFilter(ref filter, subGroupInfo);
            }
        }

        public static void SetDisregardAllPlaysBlank(GroupInfo groupInfo, bool setChecked)
        {            
            Webb.Data.DBFilter filter = new Webb.Data.DBFilter();

            if (setChecked)
            {                
                CreatDisregardPlaysFilter(ref filter, groupInfo);

                groupInfo.Filter = filter;

                #region Set Summary Filter
                foreach (GroupSummary m_Summary in groupInfo.Summaries)
                {
                    if (groupInfo is FieldGroupInfo)
                    {
                        SummaryItemDescription summaryItem = m_Summary.Tag as SummaryItemDescription;

                        if (summaryItem == null) continue;

                        Webb.Data.DBCondition condition = new Webb.Data.DBCondition();

                        condition.ColumnName = (groupInfo as FieldGroupInfo).GroupByField;

                        condition.FilterType = Webb.Data.FilterTypes.IsNotStrEmpty;

                        condition.FollowedOperand = Webb.Data.FilterOperands.And;

                        m_Summary.Filter = summaryItem.CreateRunPassFilter();

                        m_Summary.Filter.Add(condition);

                        m_Summary.DenominatorFilter = filter.Copy();                        
                    }

                }
                #endregion
            }           

            foreach (GroupInfo subGroupInfo in groupInfo.SubGroupInfos)
            {
                SetDisregardAllPlaysBlank(subGroupInfo, setChecked);
            }            
        }

        public static void SetDisregardAllPlaysBlank(WebbReport webbReport,bool setChecked)
        {
            GroupView groupView = GetGroupView(webbReport);

            if(groupView==null)return;

            GroupInfo rootGroupInfo = groupView.RootGroupInfo;

            groupView.GroupAdvancedSetting.DisregardAllPlaysBlank = setChecked;

            if (rootGroupInfo is SectionGroupInfo)
            {
                rootGroupInfo = rootGroupInfo.SubGroupInfos[0];
            }

            SetDisregardAllPlaysBlank(rootGroupInfo, setChecked);
        }

        #endregion

        #region Get Group

        public static GroupInfo GetRootGroupInfo(GroupView groupView)
        {
            GroupInfo _RootGroupInfo = groupView.RootGroupInfo;

            if (_RootGroupInfo.IsSectionOutSide)
            {
                _RootGroupInfo = _RootGroupInfo.SubGroupInfos[0];
            }

            //if (!GroupInfo.IsVisible(_RootGroupInfo))
            //{
            //    if(_RootGroupInfo.SubGroupInfos.Count==0)_RootGroupInfo.SubGroupInfos.Add(new FieldGroupInfo());

            //    _RootGroupInfo = _RootGroupInfo.SubGroupInfos[0];
            //}

            return _RootGroupInfo;
        }
        public void GetGroup(GroupInfo groupInfo,ref GroupInfoCollection allGroupInfos,int Level)
		{			
            groupInfo.LevelInfo=Level;

		    allGroupInfos.Add(groupInfo);	
		
			Level++;

			int KeepLevel=Level;

			foreach(GroupInfo subGroup in  groupInfo.SubGroupInfos)
			{ 
                this.GetGroup(subGroup,ref allGroupInfos,Level);

                 Level=KeepLevel;				
			}
			
		}
		
        public static GroupInfoCollection GetIndividualGroup(ReportSetting setting)
        {
            if (setting.WebbReport == null) return null;

            GroupInfoCollection allgroups = new GroupInfoCollection();

            Band detailband = setting.WebbReport.Bands[BandKind.Detail];

            foreach (XRControl xrControl in detailband.Controls)
            {
                if (!(xrControl is WinControlContainer)) continue;

                Control c = (xrControl as WinControlContainer).WinControl;

                if (!(c is GroupingControl)) continue;

                GroupView groupView = (c as GroupingControl)._GroupView;

                GroupInfo groupInfo = GetRootGroupInfo(groupView);
            
                groupInfo.LevelInfo = 0;

                groupInfo.TopCount = groupView.TopCount;

                allgroups.Add(groupInfo.Copy());
            }
            return allgroups;
        }

        #endregion

        #region Update GroupInfo / Setting

        public void InitGroupOption(GroupInfo groupInfo)
		{	
			if(groupInfo==null||groupInfo is SectionGroupInfo)groupInfo=new FieldGroupInfo();

			this._Field=(groupInfo as FieldGroupInfo).GroupByField;

            (groupInfo as FieldGroupInfo).DateFormat = @"M/d/yy";

			this._ReportScType=groupInfo.ReportScType;

			this._Sorting=groupInfo.Sorting;

			this._SortingByTypes=groupInfo.SortingBy;

            this.ListSummaries.Clear();

			this._TopCount=groupInfo.TopCount;

            this._DisregardBlank = groupInfo.DisregardBlank;

            this._Total=groupInfo.AddTotal;

            this._DefaultHeader = groupInfo.ColumnHeading;

            this._GroupLevel = groupInfo.LevelInfo;

            this._RelatedGroupInfo = groupInfo;

            this._UserDefinedOrders = new UserOrderClS();

            this._UserDefinedOrders.OrderValues.Clear();

            this._UserDefinedOrders.RelativeGroupInfo = groupInfo;

            foreach (object objValue in groupInfo.UserDefinedOrders.OrderValues)
            {
                this._UserDefinedOrders.OrderValues.Add(objValue);
            }

			if(groupInfo.Summaries==null) groupInfo.Summaries=new GroupSummaryCollection();

            if(groupInfo.SectionSummeries==null) groupInfo.SectionSummeries=new GroupSummaryCollection();
			
			if(groupInfo.ReportScType==ReportScType.Custom)
			{
				foreach(GroupSummary m_Summary in groupInfo.Summaries)
				{
                    SummaryItemDescription summaryItems = m_Summary.Tag as SummaryItemDescription;

                    if (summaryItems == null) continue;

                    summaryItems.ColumnWidth = m_Summary.ColumnWidth;

                    summaryItems.CustomHeader = m_Summary.ColumnHeading;

                    ListSummaries.Add(summaryItems);
				}	
			}
			else
			{
				foreach(GroupSummary m_Summary in groupInfo.SectionSummeries)
				{
                    SummaryItemDescription summaryItems = m_Summary.Tag as SummaryItemDescription;

                    if (summaryItems == null) continue;

                    summaryItems.ColumnWidth = m_Summary.ColumnWidth;

                    summaryItems.CustomHeader = m_Summary.ColumnHeading;

                    ListSummaries.Add(summaryItems);
				}	
			}
		}

		public void UpdateSetting(ReportSetting setting)
		{
			if(setting.WebbReport==null)return;

			GroupingControl groupControl=GetGroupControl(setting.WebbReport);	
		
			if(groupControl==null)
			{
				MessageBox.Show("Template error","error",MessageBoxButtons.OK,MessageBoxIcon.Stop);

				Environment.Exit(-1);
			}

			GroupView groupView=groupControl._GroupView;

            Size pageSize = setting.WebbReport.GetSizePerPage();

			int totalwidth=groupView.GetTotalColumnsWidth();
            
            //int locX = pageSize.Width/2 - totalwidth / 2;  

            int locX = (int)((pageSize.Width - totalwidth) / 2);

            if (locX < 0) locX = 0;

            groupControl.XtraContainer.Left=locX;

		}

        public void UpdateSetting(ReportSetting setting, GroupInfoCollection individualGroups, Font font)
        {
            if (setting.WebbReport == null) return;

            Band detailBand = setting.WebbReport.Bands[BandKind.Detail];

            ExControls.Styles.ExControlStyles controlStyles = new Webb.Reports.ExControls.Styles.ExControlStyles();

            GroupView oldGroupView = GetGroupView(setting.WebbReport);

            if (oldGroupView != null) controlStyles = oldGroupView.Styles;

            detailBand.Controls.Clear();

            int startLeft = 1;

            int startTop = 5;

            Size pageSize = setting.WebbReport.GetSizePerPage();

            Webb.Reports.ExControls.ContainerControl container = new Webb.Reports.ExControls.ContainerControl();

            WinControlContainer wincontrolContainer = new WinControlContainer();

            wincontrolContainer.Location = new Point(0, 0);

            wincontrolContainer.Size = pageSize;

            wincontrolContainer.WinControl = container;

            detailBand.Controls.Add(wincontrolContainer);

            int RestWidth = pageSize.Width - 1;

            WinControlContainerCollection allContainers = new WinControlContainerCollection();

            int maxWidth = 0;

            foreach (GroupInfo eachgroup in individualGroups)
            {
                GroupInfo groupCopy = eachgroup.Copy();

                WinControlContainer wincontainer = new WinControlContainer();

                GroupingControl groupControl = new GroupingControl();

                wincontainer.WinControl = groupControl;

                groupControl._GroupView.TopCount = eachgroup.TopCount;  //2009-11-23 9:58:59@Simon Add this Code

                groupCopy.TopCount = 0;

                groupControl._GroupView.RootGroupInfo = groupCopy;

                if (font != null)
                {
                    controlStyles.RowStyle.Font = (Font)font.Clone();

                    controlStyles.AlternateStyle.Font = (Font)font.Clone();

                    controlStyles.HeaderStyle.Font = (Font)font.Clone();

                    controlStyles.SectionStyle.Font = new Font(font.FontFamily.Name, font.Size + 2, font.Style | FontStyle.Bold);

                }

                controlStyles.SectionStyle.HorzAlignment = DevExpress.Utils.HorzAlignment.Near;

                groupControl._GroupView.Styles.ApplyStyle(controlStyles);

                int totalwidth = groupControl._GroupView.GetIndividualTotalColumnsWidth();

                if (totalwidth == 0) totalwidth = 30;

                if (startLeft + totalwidth + 3 > pageSize.Width || RestWidth<=0)
                {
                    startLeft = 1;

                    startTop += 100;

                    if (RestWidth > 10)
                    {
                        foreach (WinControlContainer groupContainer in allContainers)
                        {
                            groupContainer.Left += RestWidth / 2;
                        }
                    }

                    RestWidth = pageSize.Width - 1;

                    allContainers.Clear();

                }

                wincontainer.WinControl = groupControl;

                wincontainer.Location = new Point(startLeft, startTop);           

                detailBand.Controls.Add(wincontainer);

                wincontainer.Size = new Size(totalwidth - 10, 200);

                maxWidth = Math.Max(startLeft + totalwidth + 1, maxWidth);

                startLeft += totalwidth;

                RestWidth -= totalwidth;

                allContainers.Add(wincontainer);

            }
            if (RestWidth > 10)
            {
                foreach (WinControlContainer groupContainer in allContainers)
                {
                    groupContainer.Left += RestWidth / 2;
                }
            }

            wincontrolContainer.Width = Math.Max(pageSize.Width, maxWidth);

        }

        public void UpdateGroupInfo(GroupInfo basegroupInfo, Font font, bool IsInPlayList)
        {
            if (basegroupInfo == null || basegroupInfo is SectionGroupInfo)
            {
                basegroupInfo = new FieldGroupInfo("");
            }

            (basegroupInfo as FieldGroupInfo).SetFieldAndTitleSimple(this._Field, this.DefaultHeader);

            basegroupInfo.ReportScType = this._ReportScType;

            basegroupInfo.Sorting = this._Sorting;

            basegroupInfo.SortingBy = this._SortingByTypes;

            basegroupInfo.TopCount = this._TopCount;

            basegroupInfo.DisregardBlank = this._DisregardBlank;

            basegroupInfo.AddTotal = this._Total;

            basegroupInfo.UserDefinedOrders= new UserOrderClS();

            basegroupInfo.UserDefinedOrders.OrderValues.Clear();

            basegroupInfo.UserDefinedOrders.RelativeGroupInfo = basegroupInfo;

            foreach (object objValue in this.UserDefinedOrders.OrderValues)
            {
                basegroupInfo.UserDefinedOrders.OrderValues.Add(objValue);
            }

            if (font == null)
            {
                font = new Font("Tahoma", 10f);
            }

            GroupSummaryCollection m_Summaries = new GroupSummaryCollection();
          
            basegroupInfo.Style.Font = (Font)font.Clone();                     

            foreach (SummaryItemDescription summaryItem in this.ListSummaries)
            {
                GroupSummary m_summary = summaryItem.ToSummary(font,IsInPlayList);

                #region Old and heading

                //if (basegroupInfo.ReportScType == ReportScType.Custom)
                //{
                //    foreach (GroupSummary groupSummary in basegroupInfo.Summaries)
                //    {
                //        if (groupSummary.Tag == null)
                //        {
                //            //if (groupSummary.SummaryType == m_summary.SummaryType && groupSummary.ColumnHeading == m_summary.ColumnHeading)
                //            //{
                //            //    m_summary.ColumnWidth = groupSummary.ColumnWidth;
                //            //}
                //        }
                //        else if (groupSummary.Tag.ToString() == m_summary.Tag.ToString())
                //        {
                //                //m_summary.ColumnWidth = groupSummary.ColumnWidth;

                //                m_summary.ColumnHeading = groupSummary.ColumnHeading;
                //        }                       
                //    }
                //}
                //else
                //{
                //    foreach (GroupSummary groupSummary in basegroupInfo.SectionSummeries)
                //    {
                //        if (groupSummary.Tag == null)
                //        {
                //        //    if (groupSummary.SummaryType == m_summary.SummaryType && groupSummary.ColumnHeading == m_summary.ColumnHeading)
                //        //    {
                //        //        m_summary.ColumnWidth = groupSummary.ColumnWidth;
                //        //    }
                //        }
                //        else if (groupSummary.Tag.ToString() == m_summary.Tag.ToString())
                //        {
                //            //m_summary.ColumnWidth = groupSummary.ColumnWidth;

                //            m_summary.ColumnHeading = groupSummary.ColumnHeading;
                //        }               
                //    }
                //}
               #endregion

                if (font != null)
                {
                    m_summary.Style.Font = (Font)font.Clone();
                }             

                if (IsInPlayList)
                {
                    System.Text.StringBuilder sbText=new System.Text.StringBuilder( m_summary.ColumnHeading);

                    int lastIndex=m_summary.ColumnHeading.LastIndexOf("it below)");

                    if (lastIndex < 0)
                    {
                        lastIndex = m_summary.ColumnHeading.LastIndexOf("')");

                         if (lastIndex >= 0)
                        {
                             sbText =sbText.Remove(0,lastIndex+2);

                             m_summary.ColumnHeading=sbText.ToString(); 
                        }
                    }
                    else
                    {
                        sbText = sbText.Remove(0, lastIndex+9);

                        m_summary.ColumnHeading = sbText.ToString(); 
                    } 

                    m_summary.ColumnHeading= m_summary.ColumnHeading.Trim("\r\n ".ToCharArray());

                    m_summary.ColumnHeading="\r\n   "+ m_summary.ColumnHeading.Replace("\r\n", " ");
                }

                m_Summaries.Add(m_summary);
            }
            if (IsInPlayList)  
            {
                if (m_Summaries.Count > 0)
                {
                    m_Summaries[0].ColumnHeading = "(it is a group value of '" + this.Field + "',here are some statistic results about it below)" + m_Summaries[0].ColumnHeading;
                }
            }

            if (basegroupInfo.ReportScType == ReportScType.Custom)
            {
                basegroupInfo.Summaries = m_Summaries;
            }
            else
            {
                basegroupInfo.SectionSummeries = m_Summaries;
            }

        }

        //public bool ContainsSummmary(string summary)
        //{
        //    return this._Summaries.Contains(summary);

        //}

        #endregion

        #region Set/Update ColumnWidth
        public static void UpdateColumnsWidth(GroupInfo groupInfo, Int32Collection ColumnsWidth)
        {
            if (groupInfo.FollowSummaries)
            {
                if (groupInfo.Summaries != null)
                {
                    foreach (GroupSummary m_Summary in groupInfo.Summaries)
                    {
                        ColumnsWidth.Add(m_Summary.ColumnWidth);
                    }
                }
            }

            if (!groupInfo.DistinctValues && GroupInfo.IsVisible(groupInfo))	//if set OnValuePerRow,
            {
                ColumnsWidth.Add(groupInfo._ColumnWidth);
            }

            if (groupInfo.FollowSummaries)
            { }
            else
            {
                if (groupInfo.Summaries != null)
                {
                    foreach (GroupSummary m_Summary in groupInfo.Summaries)
                    {
                        ColumnsWidth.Add(m_Summary.ColumnWidth);
                    }
                }
            }

            foreach (GroupInfo subGroupInfo in groupInfo.SubGroupInfos)
            {
                UpdateColumnsWidth(subGroupInfo, ColumnsWidth);
            }
        }

        public static void SetColumnsWidth(GroupInfo groupInfo, ref int nCol, Int32Collection ColumnsWidth)
        {
            if (groupInfo.FollowSummaries)
            {
                if (groupInfo.Summaries != null)
                {
                    foreach (GroupSummary m_Summary in groupInfo.Summaries)
                    {
                        if (nCol >= ColumnsWidth.Count) return;
                        m_Summary.ColumnWidth = ColumnsWidth[nCol];
                        nCol++;
                    }
                }
            }

            if (!groupInfo.DistinctValues && GroupInfo.IsVisible(groupInfo))	//if set OnValuePerRow,
            {
                if (nCol >= ColumnsWidth.Count) return;
                groupInfo._ColumnWidth = ColumnsWidth[nCol];
                nCol++;
            }

            if (groupInfo.FollowSummaries)
            { }
            else
            {
                if (groupInfo.Summaries != null)
                {
                    foreach (GroupSummary m_Summary in groupInfo.Summaries)
                    {
                        if (nCol >= ColumnsWidth.Count) return;

                        m_Summary.ColumnWidth = ColumnsWidth[nCol];

                        nCol++;
                    }
                }
            }

            foreach (GroupInfo subGroupInfo in groupInfo.SubGroupInfos)
            {
                SetColumnsWidth(subGroupInfo, ref nCol, ColumnsWidth);
            }
        }

        #endregion

        #region ReCreateReportGroupLevels  and update Style
        public void ReCreateReportGroupLevels(ref ReportSetting setting, GroupTemplateType NewGroupTemplateType)
        {
            GroupTemplateType OldGroupTemplateType = setting.GroupTemplateType;

            if (setting.WebbReport == null || OldGroupTemplateType == NewGroupTemplateType) return;

            GroupInfoCollection allGroupInfos = new GroupInfoCollection();  

            Font font= setting.GetPageFont();

            if (OldGroupTemplateType == GroupTemplateType.IndividualGroups)
            {
                allGroupInfos = GroupWizardOption.GetIndividualGroup(setting);

                if (allGroupInfos == null) allGroupInfos = new GroupInfoCollection();               
            }
            else
            {
                GroupView groupView = GroupWizardOption.GetGroupView(setting.WebbReport);

                if (groupView == null)
                {
                    MessageBox.Show("Template error", "error", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                    Environment.Exit(-1);
                }

                GroupInfo rootGroupInfo = GroupWizardOption.GetRootGroupInfo(groupView);	 

                allGroupInfos = new GroupInfoCollection();

                int Start = 0;

                this.GetGroup(rootGroupInfo, ref allGroupInfos, Start);
            }

            Webb.Reports.ExControls.Styles.ExControlStyles styles = this.GetControlStyles(setting, font);

            #region Create New Group Types

            GroupView oldGroupView = GetGroupView(setting.WebbReport);

            bool disRegardAllSubPlaysBlank=oldGroupView.GroupAdvancedSetting.DisregardAllPlaysBlank;

            if (NewGroupTemplateType == GroupTemplateType.IndividualGroups)
            {   
                foreach (GroupInfo groupInfo in allGroupInfos)
                {
                        groupInfo.Filter.Clear();

                        if (disRegardAllSubPlaysBlank) groupInfo.DisregardBlank = true;

                        ReportSetting.UpdateDisregardBlank(groupInfo);                   
                }

                this.UpdateSetting(setting, allGroupInfos, font);
            }
            else
            {
                if (allGroupInfos == null || allGroupInfos.Count == 0) return;

                GroupInfo rootGroupInfo = allGroupInfos[0];

                rootGroupInfo.SubGroupInfos.Clear();

                #region Create Group Level

                GroupInfo CurGroupInfo = rootGroupInfo;

                for (int i = 1; i < allGroupInfos.Count; i++)
                {
                    GroupInfo subGroup = allGroupInfos[i].Copy();

                    subGroup.SubGroupInfos.Clear();

                    CurGroupInfo.SubGroupInfos.Add(subGroup);

                    if (NewGroupTemplateType == GroupTemplateType.GroupLevels)
                    {
                        CurGroupInfo = subGroup;
                    }
                }
                #endregion

                CreateSingleGroupInControl(setting, rootGroupInfo, disRegardAllSubPlaysBlank);

                UpdateAllControls(setting, styles);
            }

            setting.GroupTemplateType = NewGroupTemplateType;           
            #endregion

           
        }

        #region sub Functions for ReCreateReportGroupLevels
        private void UpdateAllControls(ReportSetting setting, Webb.Reports.ExControls.Styles.ExControlStyles styles)
        {
            Band detailBand = setting.WebbReport.Bands[BandKind.Detail];

            foreach (XRControl xrControl in detailBand.Controls)
            {
                if (!(xrControl is WinControlContainer)) continue;

                Control c = (xrControl as WinControlContainer).WinControl;

                if (!(c is GroupingControl)) continue;

                GroupingControl groupControl = c as GroupingControl;

                groupControl._GroupView.Styles.ApplyStyle(styles);
            }
        }
        protected Webb.Reports.ExControls.Styles.ExControlStyles GetControlStyles(ReportSetting setting, Font rowFont)
        {
            GroupingControl groupControl = GetGroupControl(setting.WebbReport);

            if (groupControl == null)
            {
                MessageBox.Show("Template error", "error", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                return null;
            }

            Webb.Reports.ExControls.Styles.ExControlStyles style = groupControl._GroupView.Styles;

            style.RowStyle.Font = (Font)rowFont.Clone();

            style.AlternateStyle.Font = (Font)rowFont.Clone();

            style.HeaderStyle.Font = (Font)rowFont.Clone();

            style.SectionStyle.Font = new Font(rowFont.FontFamily.Name, rowFont.Size + 2, rowFont.Style | FontStyle.Bold);

            style.SectionStyle.HorzAlignment = DevExpress.Utils.HorzAlignment.Near;

            return style;
        }
        public void CreateSingleGroupInControl(ReportSetting setting, GroupInfo rootGroupInfo,bool DisRegardAllBlank)
        {              
            Band detailBand = setting.WebbReport.Bands[BandKind.Detail];

            detailBand.Controls.Clear();

            WinControlContainer wincontainer = new WinControlContainer();

            GroupingControl groupControl = new GroupingControl();

            wincontainer.WinControl = groupControl;

            detailBand.Controls.Add(wincontainer);

            groupControl._GroupView.RootGroupInfo = rootGroupInfo;

            groupControl._GroupView.GroupAdvancedSetting.DisregardAllPlaysBlank = DisRegardAllBlank;

            ReportSetting.UpdateDisregardBlank(rootGroupInfo);

            GroupWizardOption.SetDisregardAllPlaysBlank(rootGroupInfo, DisRegardAllBlank);

            this.UpdateSetting(setting);
        
        }
        #endregion

        #endregion

        #region Fields&Properties
        private string _Field=string.Empty;

        private List<SummaryItemDescription> _ListSummaries = new List<SummaryItemDescription>();

		private ReportScType _ReportScType=ReportScType.Custom;
		private SortingByTypes _SortingByTypes=SortingByTypes.Frequence;
		private SortingTypes _Sorting=SortingTypes.Descending;	
		private int _TopCount=0;
        private bool _DisregardBlank = false;
        private bool _Total = false;
        private string _DefaultHeader = string.Empty;
        private int _GroupLevel = 0;

        private GroupInfo _RelatedGroupInfo=null;
        private UserOrderClS _UserDefinedOrders = new UserOrderClS();

        public UserOrderClS UserDefinedOrders
        {
            get
            {
                if (_UserDefinedOrders == null) _UserDefinedOrders = new UserOrderClS();

                return this._UserDefinedOrders;
            }
            set
            {
                this._UserDefinedOrders = value;
            }
        }

        public GroupInfo RelatedGroupInfo
        {
            get
            {
                return this._RelatedGroupInfo;
            }            
        }

        public int GroupLevel
        {
            get
            {
                return _GroupLevel;
            }
            set
            {
                _GroupLevel = value;
            }
        }


        public string DefaultHeader
        {
            get { return this._DefaultHeader; }
            set { _DefaultHeader = value; }
        }

        public bool Total
        {
            get { return this._Total; }
            set { this._Total = value; }
        }
		public int TopCount
		{
			get
			{				
				return _TopCount; }
			set{ _TopCount = value; }
		}
        public bool DisregardBlank
        {
            get
            {
                return _DisregardBlank;
            }
            set { _DisregardBlank = value; }
        }


	
		public string Field
		{
			get
			{
				if(_Field==null)_Field=string.Empty;
				return _Field; }
			set{ _Field = value; }
		}
		public SortingTypes Sorting
		{
			get
			{return _Sorting; }
			set{ _Sorting = value; }
		}
		public SortingByTypes SortingByTypes
		{
			get
			{		
				return _SortingByTypes; }
			set{ _SortingByTypes = value; }
		}

		public List<SummaryItemDescription> ListSummaries
		{
			get
			{ 
				if(_ListSummaries==null)_ListSummaries=new List<SummaryItemDescription>() ;
                return _ListSummaries;
            }
            set { _ListSummaries = value; }
		}

		public Webb.Reports.ReportScType ReportScType
		{
			get{ return _ReportScType; }
			set{ _ReportScType = value; }
		}
		#endregion

        public override string ToString()
        {
            string strDisplayContent = this._Field;           

            string levelValue ="".PadLeft(this._GroupLevel * 2) + "|-" + strDisplayContent;

            if (_GroupLevel == 0)
            {
                levelValue = strDisplayContent;
            }
            return levelValue;
        }
	}
	  
    [Serializable]
	public class CompactGroupWizardOption
	{		
		public CompactGroupWizardOption()
		{			

		}
		public static CompactGroupView GetCompactGroupView(WebbReport webbReport)
		{			
			Band detailband=webbReport.Bands[BandKind.Detail];
				
			foreach(XRControl xrControl in detailband.Controls)
			{
				if(!(xrControl is WinControlContainer))continue;
				    
				Control c = (xrControl as WinControlContainer).WinControl;

				if(!(c is CompactGroupingControl))continue;

				return (c as CompactGroupingControl).MainView as CompactGroupView;
			}			
			return null;			

		}
	

		public static CompactGroupingControl GetCompactGroupControl(WebbReport webbReport)
		{			
			Band detailband=webbReport.Bands[BandKind.Detail];
				
			foreach(XRControl xrControl in detailband.Controls)
			{
				if(!(xrControl is WinControlContainer))continue;
				    
				Control c = (xrControl as WinControlContainer).WinControl;

				if(!(c is CompactGroupingControl))continue;

				return c as CompactGroupingControl;
			}			
			return null;			

		}
	
		public void SetSetting(ReportSetting setting)
		{
			if(setting.WebbReport==null)return;

			CompactGroupView compactGroupView=GetCompactGroupView(setting.WebbReport);			

			if(compactGroupView==null)
			{
				MessageBox.Show("Template error","error",MessageBoxButtons.OK,MessageBoxIcon.Stop);

				Environment.Exit(-1);
			}

			_CompactGroupInfo=compactGroupView.RootGroupInfo;	
		
			if(_CompactGroupInfo==null||_CompactGroupInfo.SubGroupInfos.Count==0)
			{
				_SubCompactGroupInfo=new FieldGroupInfo();
			}
			else
			{
				_SubCompactGroupInfo=_CompactGroupInfo.SubGroupInfos[0];
			}
			
		}
	

		#region Fields&Properties
		private GroupInfo _CompactGroupInfo=null;	
		private GroupInfo _SubCompactGroupInfo=null;
	

		public GroupInfo CompactGroupInfo
		{
			get{ if(_CompactGroupInfo==null)_CompactGroupInfo=new FieldGroupInfo();
				return _CompactGroupInfo; }
			set{ _CompactGroupInfo = value; }
		}
		public GroupInfo SubCompactGroupInfo
		{
			get
			{
				if(_SubCompactGroupInfo==null)_SubCompactGroupInfo=new FieldGroupInfo();
				return _SubCompactGroupInfo; }
			set{ _SubCompactGroupInfo = value; }
		}

		public bool IsRootSection
		{
			get
			{
				return (CompactGroupInfo is SectionGroupInfo);
			}
		}
		public bool IsSubSection
		{
			get
			{
				return (SubCompactGroupInfo is SectionGroupInfo);
			}
		}
		#endregion
	}
		

	#endregion

	#region WizardEnviroment
	[Serializable]
	public class WizardEnviroment:ISerializable
	{
        public static string PublicReportPath = Webb.Utility.ApplicationDirectory + @"Reports";
		
	
        protected string _DefaultHeaderStyleName = "Webb Default";

        protected string _DefaultGameStyleName = "Webb Default";
        

		public const string WizardFileExtension=".repx";

		[NonSerialized]
		protected bool _CreateDataSource=false;

        protected string _AdvUser = string.Empty;

        protected string _UserFolder = string.Empty;       

        protected WebbDBTypes _ProductType = WebbDBTypes.WebbAdvantageFootball;

        public static int ReportSaveCount = 1;

        protected string _LastReportsOpenDir = string.Empty;

        protected bool _AutoAdjustWidths = true;
        
        protected WizardReportSavedPathManager _WizardPathManager;

        protected WebbAcivateCls _WebbAcitvateInstance = new WebbAcivateCls();

        public WizardEnviroment()
		{	    
			_CreateDataSource=false;

            _WizardPathManager = new WizardReportSavedPathManager();
        }

        #region Properties
        public WebbAcivateCls WebbAcitvateInstance
        {
            get
            {
                return  _WebbAcitvateInstance;
            }
            set
            {
                _WebbAcitvateInstance = value;
            }
        }
        public WizardReportSavedPathManager WizardPathManager
        {
            get
            {
                if (_WizardPathManager == null) _WizardPathManager = new WizardReportSavedPathManager();

                return _WizardPathManager;
            }
            set
            {
                _WizardPathManager = value;
            }

        }
       

        public bool AutoAdjustWidths
        {
            get
            {
                return this._AutoAdjustWidths;
            }
            set
            {
                this._AutoAdjustWidths = value;
            }
        }
        public string UserFolder
        {
            get
            {
                return this._UserFolder;
            }
            set
            {
                _UserFolder = value;
            }
        }
        public string LastReportsOpenDir
        {
            get {
                  if (_LastReportsOpenDir == null) _LastReportsOpenDir = string.Empty;

                  return this._LastReportsOpenDir;
                 }
            set {
                   _LastReportsOpenDir = value;                  
                }
        }
        public string AdvUser
        {
            get { return this._AdvUser; }
            set { _AdvUser = value; }
        }
        public WebbDBTypes ProductType
        {
            get { return this._ProductType; }
            set { _ProductType = value; }
        }

		public bool CreateDataSource
		{
			get{ return _CreateDataSource; }
			set{ _CreateDataSource = value; }
		}

       
        
        public string DefaultHeaderStyleName
        {
            get
            {
                if (_DefaultHeaderStyleName == null) _DefaultHeaderStyleName=string.Empty;

                return _DefaultHeaderStyleName;
            }

            set { _DefaultHeaderStyleName = value; }
        }
        public string DefaultGameStyleName
        {
            get
            {
                if (_DefaultGameStyleName == null) _DefaultGameStyleName = string.Empty;

                return _DefaultGameStyleName;
            }

            set { _DefaultGameStyleName = value; }
        }     
       
        #endregion

        #region Is CCRM/Voctpry Product
        public bool IsVictoryProduct
        {
            get
            {
                switch (this.ProductType)
                {
                    case Webb.Reports.DataProvider.WebbDBTypes.WebbVictoryFootball:
                    case Webb.Reports.DataProvider.WebbDBTypes.WebbVictoryVolleyball:
                    case Webb.Reports.DataProvider.WebbDBTypes.WebbVictoryBasketball:
                    case Webb.Reports.DataProvider.WebbDBTypes.WebbVictoryHockey:
                    case Webb.Reports.DataProvider.WebbDBTypes.WebbVictoryLacrosse:
                    case Webb.Reports.DataProvider.WebbDBTypes.WebbVictorySoccer:
                        return true;
                    default:
                        break;
                }

                return false;
            }
        }

        public bool IsCoachCRMProduct
        {
            get
            {
                switch (this.ProductType)
                {
                    case Webb.Reports.DataProvider.WebbDBTypes.CoachCRM:
                        return true;
                    default:
                        break;
                }

                return false;
            }
        }

        public bool IsAdvantageProduct
        {
            get
            {
                switch (this.ProductType)
                {
                    case Webb.Reports.DataProvider.WebbDBTypes.WebbAdvantageFootball:
                        return true;
                    default:
                        break;
                }
                return false;
            }
        }
        #endregion

            #region Get Dir/Path
       

		public string GetReportPath(TemplateType templateType)
		{
            string strCurrentReportPath = this.GetWizardReportDirectory();

            if (!System.IO.Directory.Exists(strCurrentReportPath))
			{
                try
                {
                    System.IO.Directory.CreateDirectory(strCurrentReportPath);
                }
                catch(Exception ex)
                {
                    Webb.Utilities.TopMostMessageBox.ShowMessage("Error", ex.Message+"\nplease check the network or the path is valid.", MessageBoxButtons.OK);

                    Environment.Exit(-1);
                }
			}

            string dir = strCurrentReportPath.TrimEnd(@"\".ToCharArray());

            string typedefinaion= "Custom";

            if ((int)ReportSetting.WizardEnviroment.ProductType>9)
            {
                typedefinaion = ReportSetting.WizardEnviroment.ProductType.ToString().Replace("Webb","");                
            }
           
			return string.Format(@"{0}\{1} Report{2}{3}",dir,typedefinaion,WizardEnviroment.ReportSaveCount,WizardEnviroment.WizardFileExtension);
        }
            #endregion

        public string GetWizardReportDirectory()
        {
            return WizardPathManager.GetReportSavedPath(this.ProductType);
        }
        public void SetWizardReportDirectory(WebbDBTypes webbDbType,string strProductPath)
        {
            WizardPathManager.SetReportSavedPath(webbDbType, strProductPath);
        }

        #region Serialization By Simon's Macro 2009-8-7 10:18:00
        public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
            
            info.AddValue("_LastReportsOpenDir", this._LastReportsOpenDir);
		    info.AddValue("_DefaultHeaderStyleName", _DefaultHeaderStyleName);
            info.AddValue("_DefaultGameStyleName", _DefaultGameStyleName);
            info.AddValue("_AdvUser", _AdvUser);
            info.AddValue("_UserFolder", _UserFolder);
            info.AddValue("_ProductType", _ProductType,typeof(WebbDBTypes));
            info.AddValue("_AutoAdjustWidths", this._AutoAdjustWidths);
            info.AddValue("_WizardPathManager", this._WizardPathManager,typeof(WizardReportSavedPathManager));
            info.AddValue("_WebbAcitvateInstance", this._WebbAcitvateInstance,typeof(WebbAcivateCls));
			
		}

		public WizardEnviroment(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
           
            try
            {
                _AutoAdjustWidths = info.GetBoolean("_AutoAdjustWidths");
            }
            catch
            {
                _AutoAdjustWidths = true;
            }
            try
            {
                _LastReportsOpenDir = info.GetString("_LastReportsOpenDir");
            }
            catch
            {
                _LastReportsOpenDir = string.Empty;
            }
            try
			{
				_AdvUser=info.GetString("_AdvUser");
			}
			catch
			{
				_AdvUser=string.Empty;
			}
            try
			{
				_UserFolder=info.GetString("_UserFolder");
			}
			catch
			{
				_UserFolder=string.Empty;
			}
            try
			{
                _ProductType = (WebbDBTypes)info.GetValue("_ProductType", typeof(WebbDBTypes));
			}
			catch
			{
				_ProductType = WebbDBTypes.WebbAdvantageFootball;
			} 
            try
            {
                _DefaultHeaderStyleName = info.GetString("_DefaultHeaderStyleName");
            }
            catch
            {
                _DefaultHeaderStyleName = string.Empty;
            }
            try
            {
                _DefaultGameStyleName = info.GetString("_DefaultGameStyleName");
            }
            catch
            {
                _DefaultGameStyleName = string.Empty;
            }           
            try
            {
                _WizardPathManager = (WizardReportSavedPathManager)info.GetValue("_WizardPathManager", typeof(WizardReportSavedPathManager));
            }
            catch
            {
                 _WizardPathManager = new WizardReportSavedPathManager();                
            }
            try
            {
                _WebbAcitvateInstance = (WebbAcivateCls)info.GetValue("_WebbAcitvateInstance", typeof(WebbAcivateCls));
            }
            catch
            {
                _WebbAcitvateInstance = new WebbAcivateCls();
            }
			
		}
		#endregion

        public static WizardEnviroment LoadEnvironment(string strFile)
        {
            WizardEnviroment environment = new WizardEnviroment();

            if (File.Exists(strFile))
            {
                System.IO.FileStream stream = System.IO.File.Open(strFile, System.IO.FileMode.Open);

                try
                {
                    environment = Webb.Utilities.Serializer.DeserializeObject(stream) as WizardEnviroment;
                }
                catch
                {
                    environment = new WizardEnviroment();
                }
                finally
                {
                    stream.Close();
                }
            }
            return environment;

        }

        public static void SaveEnvironment(WizardEnviroment environment,string strFile)
        {
            if (environment == null) return;

            if (File.Exists(strFile))
            {
                File.Delete(strFile);
            }

            System.IO.FileStream stream = System.IO.File.Open(strFile, System.IO.FileMode.OpenOrCreate);

            try
            {               
                Webb.Utilities.Serializer.SerializeObject(stream, environment);
            }
            catch (Exception ex)
            {
                Webb.Utilities.MessageBoxEx.ShowError(ex.Message);
            }
            finally
            {
                stream.Close();
            } 
        }

	}
	#endregion

    #region VictoryPathManager

    [Serializable]
    public class VictoryPathManager:ISerializable
    {
        public const string DBPathKeyName = "mdbPath";
        public const string InstalledPathKeyName = "installPath";
        public const string VictoryKey=@"Software\Webb Electronics\Victory";


        protected WebbDBTypes _WebbDBType=WebbDBTypes.WebbVictoryFootball;
        protected string _VictoryDBPath ;
        protected string _VictoryReportsPath;

        public VictoryPathManager(WebbDBTypes dbTypes)
        {
            this._WebbDBType = dbTypes;

            _VictoryDBPath = string.Empty;

            _VictoryReportsPath = GetVictoryReportSavedPath();

            if (_VictoryReportsPath == string.Empty) _VictoryReportsPath = Webb.Utility.ApplicationDirectory + @"Reports\Victory Reports";
        }

      
        public string GetVictoryDatabasePath()
        {   
            string strProductpath = this.ReadDatabasePathFromRegistry();

            if (strProductpath != string.Empty) return strProductpath;

            strProductpath = this.SearchVictoryProductInstallPath();

            if (strProductpath == string.Empty) return string.Empty;

            if (!strProductpath.EndsWith(@"\")) strProductpath = strProductpath + @"\";

            switch (this._WebbDBType)
            {
                case Webb.Reports.DataProvider.WebbDBTypes.WebbVictoryFootball:
                default:
                    strProductpath = strProductpath + "victory.mdb";
                    break;
                case Webb.Reports.DataProvider.WebbDBTypes.WebbVictoryBasketball:
                    strProductpath = strProductpath + @"mdb\Basketball\victory.mdb";
                    break;
                case Webb.Reports.DataProvider.WebbDBTypes.WebbVictoryHockey:
                    strProductpath = strProductpath + @"mdb\Hockey\victory.mdb";
                    break;
                case Webb.Reports.DataProvider.WebbDBTypes.WebbVictoryVolleyball:
                    strProductpath = strProductpath + @"mdb\Vollyball\victory.mdb";
                    break;
                case Webb.Reports.DataProvider.WebbDBTypes.WebbVictoryLacrosse:
                    strProductpath = strProductpath + @"mdb\Lacrosse\victory.mdb";
                    break;
                case Webb.Reports.DataProvider.WebbDBTypes.WebbVictorySoccer:
                    strProductpath = strProductpath + @"mdb\Soccer\victory.mdb";
                    break;
            }

            if(!File.Exists(strProductpath))
            {
                return string.Empty;
            }

            return strProductpath;

        }      

        public string GetVictoryReportSavedPath()
        {
            string strProductpath = this.ReadInstalledPathFromRegistry();

            if (strProductpath == string.Empty)
            {
                strProductpath = this.SearchVictoryProductInstallPath();
            }

            if (strProductpath == string.Empty) return string.Empty;

            if (!strProductpath.EndsWith(@"\")) strProductpath = strProductpath + @"\";

            strProductpath = strProductpath + "Reports";

            return strProductpath;
        }

        private string SearchVictoryProductInstallPath()
        {
            string SearchDirectory = @"C:\Program Files\Webb Electronics";

            DirectoryInfo searchDirInfo = new DirectoryInfo(SearchDirectory);

            if (searchDirInfo.Exists)
            {
                string searchWord = "";

                #region Set Searachword
                switch (this._WebbDBType)
                {
                    case Webb.Reports.DataProvider.WebbDBTypes.WebbVictoryFootball:
                    default:
                        searchWord = "Victory Football*";
                        break;
                    case Webb.Reports.DataProvider.WebbDBTypes.WebbVictoryBasketball:
                        searchWord = "Victory Basketball*";
                        break;
                    case Webb.Reports.DataProvider.WebbDBTypes.WebbVictoryHockey:
                        searchWord = "Victory Hockey*";
                        break;
                    case Webb.Reports.DataProvider.WebbDBTypes.WebbVictoryVolleyball:
                        searchWord = "Victory Vollyball*";
                        break;
                    case Webb.Reports.DataProvider.WebbDBTypes.WebbVictoryLacrosse:
                        searchWord = "Victory Lacrosse*";
                        break;
                    case Webb.Reports.DataProvider.WebbDBTypes.WebbVictorySoccer:
                        searchWord = "Victory Soccer*";
                        break;
                }
                #endregion

                DirectoryInfo[] subDirectories = searchDirInfo.GetDirectories(searchWord);

                if (subDirectories.Length == 0) return string.Empty; ;

                #region Search Sub Directory for "Victory.mdb"
                foreach (DirectoryInfo subDirectory in subDirectories)
                {
                    FileInfo[] fileinfos = subDirectory.GetFiles("WebbVictory.exe");

                    if (fileinfos.Length > 0)
                    {
                        return subDirectory.FullName;
                    }
                }
                #endregion
            }

            return string.Empty;
        }

        #region Read Values From Registry
        private RegistryKey OpenVictoryRegistry()
        {
            RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(VictoryKey);

            if (registryKey == null) return null;

            string subKey = "";

            switch (this._WebbDBType)
            {
                case WebbDBTypes.WebbVictoryBasketball:
                default:
                    subKey = "basketball";
                    break;
                case WebbDBTypes.WebbVictoryHockey:
                    subKey = "hockey";
                    break;
                case WebbDBTypes.WebbVictoryVolleyball:
                    subKey = "volleyball";
                    break;
                case WebbDBTypes.WebbVictoryLacrosse:
                    subKey = "Lacrosse";
                    break;
                case WebbDBTypes.WebbVictorySoccer:
                    subKey = "soccer";
                    break;
                case WebbDBTypes.WebbVictoryFootball:
                    subKey = "football";
                    break;
            }

            registryKey = registryKey.OpenSubKey(subKey);

            return registryKey;
        }

        private string ReadDatabasePathFromRegistry()
        {
            string strDatabasePath = string.Empty;

            RegistryKey registryKey = this.OpenVictoryRegistry();

            if (registryKey != null)
            {
                object objDBPath = registryKey.GetValue(DBPathKeyName);

                if (objDBPath != null)strDatabasePath=objDBPath.ToString();

                registryKey.Close();
            }
            if(!System.IO.File.Exists(strDatabasePath))
            {
                strDatabasePath = string.Empty;
            }

            return strDatabasePath;
        }

        private string ReadInstalledPathFromRegistry()
        {
            string strInstallPath = string.Empty;

            RegistryKey registryKey = this.OpenVictoryRegistry();

            if (registryKey != null)
            {
                object objDBPath = registryKey.GetValue(InstalledPathKeyName);

                if (objDBPath != null) strInstallPath=objDBPath.ToString();

                registryKey.Close();               
            }

            if (!System.IO.Directory.Exists(strInstallPath))
            {
                strInstallPath = string.Empty;
            }
           
            return strInstallPath;
        }
        #endregion

        public Webb.Reports.DataProvider.WebbDBTypes WebbDBType
        {
            get
            {
                return _WebbDBType;
            }
            set
            {
                _WebbDBType = value;
            }
        }

        public string VictoryDBPath
        {
            get
            {
                return _VictoryDBPath;
            }
            set
            {
                _VictoryDBPath = value;
            }
        }

        public string VictoryReportsPath
        {
            get
            {
                return _VictoryReportsPath;
            }
            set
            {
                _VictoryReportsPath = value;
            }
        }

        #region Serialization By Simon's Macro 2011-4-6 15:32:49
		public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
			info.AddValue("_WebbDBType",_WebbDBType,typeof(Webb.Reports.DataProvider.WebbDBTypes));
			info.AddValue("_VictoryDBPath",_VictoryDBPath);
			info.AddValue("_VictoryReportsPath",_VictoryReportsPath);

        }

        public VictoryPathManager(SerializationInfo info, StreamingContext context)
        {
			try
			{
				_WebbDBType=(Webb.Reports.DataProvider.WebbDBTypes)info.GetValue("_WebbDBType",typeof(Webb.Reports.DataProvider.WebbDBTypes));
			}
			catch
			{
				_WebbDBType=WebbDBTypes.WebbVictoryFootball;
			}
			try
			{
				_VictoryDBPath=info.GetString("_VictoryDBPath");
			}
			catch
			{
                _VictoryDBPath =string.Empty;
			}
			try
			{
				_VictoryReportsPath=info.GetString("_VictoryReportsPath");
			}
			catch
			{
                _VictoryReportsPath = GetVictoryReportSavedPath();

                if (_VictoryReportsPath == string.Empty) _VictoryReportsPath = Webb.Utility.ApplicationDirectory + @"Reports\Victory Reports";
			}
        }
		#endregion

    }
    #endregion

    #region WizardReportSavedPathManager
    [Serializable]
    public class WizardReportSavedPathManager : ISerializable
    {
        public static string PublicReportPath = Webb.Utility.ApplicationDirectory + @"Reports";

       #region Fields
        private WebbDBTypes _webbDbtype=WebbDBTypes.WebbAdvantageFootball;
        private string _AdvantageSavedPath;
        private string _CCRMSavedPath;
        private VictoryPathManager _VictoryFootballManager;
        private VictoryPathManager _VictorybasketballManager;
        private VictoryPathManager _VictoryvolleyballManager;
        private VictoryPathManager _VictoryhockeyManager;
        private VictoryPathManager _VictoryLacrossseManager;
        private VictoryPathManager _VictorySoccerManager;
      #endregion

        public WizardReportSavedPathManager()
        {
            _AdvantageSavedPath = PublicReportPath + @"\Common";
            _CCRMSavedPath = PublicReportPath + @"\CCRM Reports";
            _VictoryFootballManager =new VictoryPathManager(WebbDBTypes.WebbVictoryFootball);
            _VictorybasketballManager = new VictoryPathManager(WebbDBTypes.WebbVictoryBasketball);
            _VictoryvolleyballManager = new VictoryPathManager(WebbDBTypes.WebbVictoryVolleyball);
            _VictoryhockeyManager = new VictoryPathManager(WebbDBTypes.WebbVictoryHockey);
            _VictoryLacrossseManager = new VictoryPathManager(WebbDBTypes.WebbVictoryLacrosse);
            _VictorySoccerManager = new VictoryPathManager(WebbDBTypes.WebbVictorySoccer);
        }
        public WizardReportSavedPathManager(WebbDBTypes webbDbtype,string defaultReportSavedPath)
        {
            this._webbDbtype = webbDbtype;
            _AdvantageSavedPath = PublicReportPath + @"\Common";
            _CCRMSavedPath = PublicReportPath + @"\CCRM Reports";
            _VictoryFootballManager = new VictoryPathManager(WebbDBTypes.WebbVictoryFootball);
            _VictorybasketballManager = new VictoryPathManager(WebbDBTypes.WebbVictoryBasketball);
            _VictoryvolleyballManager = new VictoryPathManager(WebbDBTypes.WebbVictoryVolleyball);
            _VictoryhockeyManager = new VictoryPathManager(WebbDBTypes.WebbVictoryHockey);
            _VictoryLacrossseManager = new VictoryPathManager(WebbDBTypes.WebbVictoryLacrosse);
            _VictorySoccerManager = new VictoryPathManager(WebbDBTypes.WebbVictorySoccer);
            SetReportSavedPath(webbDbtype, defaultReportSavedPath);
        }

        #region Properties

        public Webb.Reports.DataProvider.WebbDBTypes WebbDbtype
        {
            get
            {
                return _webbDbtype;
            }
            set
            {
                _webbDbtype = value;
            }
        }

        public string AdvantageSavedPath
        {
            get
            {
                return _AdvantageSavedPath;
            }
            set
            {
                _AdvantageSavedPath = value;
            }
        }

        public string CCRMSavedPath
        {
            get
            {
                return _CCRMSavedPath;
            }
            set
            {
                _CCRMSavedPath = value;
            }
        }

        public Webb.Reports.ReportWizard.WizardInfo.VictoryPathManager VictoryFootballManager
        {
            get
            {
                if (_VictoryFootballManager == null) _VictoryFootballManager = new VictoryPathManager(WebbDBTypes.WebbVictoryFootball);
                return _VictoryFootballManager;
            }
            set
            {
                _VictoryFootballManager = value;
            }
        }

        public Webb.Reports.ReportWizard.WizardInfo.VictoryPathManager VictorybasketballManager
        {
            get
            {
               if(_VictorybasketballManager==null)_VictorybasketballManager = new VictoryPathManager(WebbDBTypes.WebbVictoryBasketball);
               
                return _VictorybasketballManager;
            }
            set
            {
                _VictorybasketballManager = value;
            }
        }

        public Webb.Reports.ReportWizard.WizardInfo.VictoryPathManager VictoryvolleyballManager
        {
            get
            {
                if(_VictoryvolleyballManager==null) _VictoryvolleyballManager = new VictoryPathManager(WebbDBTypes.WebbVictoryVolleyball);
            
                return _VictoryvolleyballManager;
            }
            set
            {
                _VictoryvolleyballManager = value;
            }
        }

        public Webb.Reports.ReportWizard.WizardInfo.VictoryPathManager VictoryhockeyManager
        {
            get
            {
               if(_VictoryhockeyManager==null)_VictoryhockeyManager = new VictoryPathManager(WebbDBTypes.WebbVictoryHockey);
            
                return _VictoryhockeyManager;
            }
            set
            {
                _VictoryhockeyManager = value;
            }
        }

        public Webb.Reports.ReportWizard.WizardInfo.VictoryPathManager VictoryLacrossseManager
        {
            get
            {
               if(_VictoryLacrossseManager==null)_VictoryLacrossseManager = new VictoryPathManager(WebbDBTypes.WebbVictoryLacrosse);
               
                return _VictoryLacrossseManager;
            }
            set
            {
                _VictoryLacrossseManager = value;
            }
        }

        public Webb.Reports.ReportWizard.WizardInfo.VictoryPathManager VictorySoccerManager
        {
            get
            {
                if (_VictorySoccerManager == null) _VictorySoccerManager = new VictoryPathManager(WebbDBTypes.WebbVictorySoccer);

                return _VictorySoccerManager;
            }
            set
            {
                _VictorySoccerManager = value;
            }
        }
        #endregion

        #region Serialization By Simon's Macro 2011-4-6 15:44:10
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
			info.AddValue("_webbDbtype",_webbDbtype,typeof(Webb.Reports.DataProvider.WebbDBTypes));
			info.AddValue("_AdvantageSavedPath",_AdvantageSavedPath);
			info.AddValue("_CCRMSavedPath",_CCRMSavedPath);
			info.AddValue("_VictoryFootballManager",_VictoryFootballManager,typeof(Webb.Reports.ReportWizard.WizardInfo.VictoryPathManager));
			info.AddValue("_VictorybasketballManager",_VictorybasketballManager,typeof(Webb.Reports.ReportWizard.WizardInfo.VictoryPathManager));
			info.AddValue("_VictoryvolleyballManager",_VictoryvolleyballManager,typeof(Webb.Reports.ReportWizard.WizardInfo.VictoryPathManager));
			info.AddValue("_VictoryhockeyManager",_VictoryhockeyManager,typeof(Webb.Reports.ReportWizard.WizardInfo.VictoryPathManager));
			info.AddValue("_VictoryLacrossseManager",_VictoryLacrossseManager,typeof(Webb.Reports.ReportWizard.WizardInfo.VictoryPathManager));
			info.AddValue("_VictorySoccerManager",_VictorySoccerManager,typeof(Webb.Reports.ReportWizard.WizardInfo.VictoryPathManager));

        }

        public WizardReportSavedPathManager(SerializationInfo info, StreamingContext context)
        {
			try
			{
				_webbDbtype=(Webb.Reports.DataProvider.WebbDBTypes)info.GetValue("_webbDbtype",typeof(Webb.Reports.DataProvider.WebbDBTypes));
			}
			catch
			{
				_webbDbtype=WebbDBTypes.WebbAdvantageFootball;
			}
			try
			{
				_AdvantageSavedPath=info.GetString("_AdvantageSavedPath");
			}
			catch
			{
                _AdvantageSavedPath = PublicReportPath + @"\Common";
			}
			try
			{
				_CCRMSavedPath=info.GetString("_CCRMSavedPath");
			}
			catch
			{
                _CCRMSavedPath = PublicReportPath + @"\CCRM Reports";
			}
			try
			{
				_VictoryFootballManager=(Webb.Reports.ReportWizard.WizardInfo.VictoryPathManager)info.GetValue("_VictoryFootballManager",typeof(Webb.Reports.ReportWizard.WizardInfo.VictoryPathManager));
			}
			catch
			{
                VictoryFootballManager = new VictoryPathManager(WebbDBTypes.WebbVictoryFootball);              
			}
			try
			{
				_VictorybasketballManager=(Webb.Reports.ReportWizard.WizardInfo.VictoryPathManager)info.GetValue("_VictorybasketballManager",typeof(Webb.Reports.ReportWizard.WizardInfo.VictoryPathManager));
			}
			catch
			{
                _VictorybasketballManager = new VictoryPathManager(WebbDBTypes.WebbVictoryBasketball);               
			}
			try
			{
				_VictoryvolleyballManager=(Webb.Reports.ReportWizard.WizardInfo.VictoryPathManager)info.GetValue("_VictoryvolleyballManager",typeof(Webb.Reports.ReportWizard.WizardInfo.VictoryPathManager));
			}
			catch
			{
                _VictoryvolleyballManager = new VictoryPathManager(WebbDBTypes.WebbVictoryVolleyball);              
			}
			try
			{
				_VictoryhockeyManager=(Webb.Reports.ReportWizard.WizardInfo.VictoryPathManager)info.GetValue("_VictoryhockeyManager",typeof(Webb.Reports.ReportWizard.WizardInfo.VictoryPathManager));
			}
			catch
			{
                _VictoryhockeyManager = new VictoryPathManager(WebbDBTypes.WebbVictoryHockey);             
			}
			try
			{
				_VictoryLacrossseManager=(Webb.Reports.ReportWizard.WizardInfo.VictoryPathManager)info.GetValue("_VictoryLacrossseManager",typeof(Webb.Reports.ReportWizard.WizardInfo.VictoryPathManager));
			}
			catch
			{
                _VictoryLacrossseManager = new VictoryPathManager(WebbDBTypes.WebbVictoryLacrosse);
               
			}
			try
			{
				_VictorySoccerManager=(Webb.Reports.ReportWizard.WizardInfo.VictoryPathManager)info.GetValue("_VictorySoccerManager",typeof(Webb.Reports.ReportWizard.WizardInfo.VictoryPathManager));
			}
			catch
			{
                _VictorySoccerManager = new VictoryPathManager(WebbDBTypes.WebbVictorySoccer);
			}
        }
		
        #endregion

        public void SetReportSavedPath(WebbDBTypes webbDbtype, string defaultReportSavedPath)
        {
            this._webbDbtype = webbDbtype;
            switch (webbDbtype)
            {
                case Webb.Reports.DataProvider.WebbDBTypes.WebbVictoryFootball:
                    _VictoryFootballManager.VictoryReportsPath = defaultReportSavedPath;
                    break;
                case Webb.Reports.DataProvider.WebbDBTypes.WebbVictoryVolleyball:
                    _VictoryvolleyballManager.VictoryReportsPath = defaultReportSavedPath;
                    break;
                case Webb.Reports.DataProvider.WebbDBTypes.WebbVictoryBasketball:
                    VictorybasketballManager.VictoryReportsPath = defaultReportSavedPath;
                    break;
                case Webb.Reports.DataProvider.WebbDBTypes.WebbVictoryHockey:
                    VictoryhockeyManager.VictoryReportsPath = defaultReportSavedPath;
                    break;
                case Webb.Reports.DataProvider.WebbDBTypes.WebbVictoryLacrosse:
                    VictoryLacrossseManager.VictoryReportsPath = defaultReportSavedPath;
                    break;
                case Webb.Reports.DataProvider.WebbDBTypes.WebbVictorySoccer:
                    VictorySoccerManager.VictoryReportsPath = defaultReportSavedPath;
                    break;
                case WebbDBTypes.CoachCRM:
                    _CCRMSavedPath = defaultReportSavedPath;
                    break;
                default:
                    _AdvantageSavedPath = defaultReportSavedPath;
                    break;
            }
        }
        public string GetReportSavedPath(WebbDBTypes webbdbtype)
        {
            switch (webbdbtype)
            {
                case Webb.Reports.DataProvider.WebbDBTypes.WebbVictoryFootball:
                    return VictoryFootballManager.VictoryReportsPath;
                   
                case Webb.Reports.DataProvider.WebbDBTypes.WebbVictoryVolleyball:
                   return  VictoryvolleyballManager.VictoryReportsPath ;
                   
                case Webb.Reports.DataProvider.WebbDBTypes.WebbVictoryBasketball:
                   return VictorybasketballManager.VictoryReportsPath ;
                    
                case Webb.Reports.DataProvider.WebbDBTypes.WebbVictoryHockey:
                   return VictoryhockeyManager.VictoryReportsPath ;                    
                case Webb.Reports.DataProvider.WebbDBTypes.WebbVictoryLacrosse:
                    return VictoryLacrossseManager.VictoryReportsPath ;                  
                case Webb.Reports.DataProvider.WebbDBTypes.WebbVictorySoccer:
                   return VictorySoccerManager.VictoryReportsPath;
                    
                case WebbDBTypes.CoachCRM:
                    return this.CCRMSavedPath;
                  
                default:
                   return AdvantageSavedPath;
                    
            }
        }
    }
    #endregion
}
