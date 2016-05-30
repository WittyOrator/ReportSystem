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
using Webb.Reports.ExControls.Views;
using Webb.Reports.ReportWizard.Wizards.Steps;
using System.Drawing;
using Webb.Reports.ExControls.Data;
using Webb.Reports.ExControls;
using Webb.Reports.ExControls.Styles;


namespace Webb.Reports.ReportWizard.WizardInfo
{
	[Serializable]
	public class WizardCustomStyles:ISerializable
	{
		protected string _StyleName=string.Empty;
		protected Image _Image=null;
		protected Image _NoWaterMarkImage=null;
		protected ExControlStyles _ExcontrolStyles=new ExControlStyles();
		protected TemplateType templateType=TemplateType.PlayList;

		protected SerializableWatermark _SerializableWatermark=new SerializableWatermark();

		public WizardCustomStyles()
		{
		}

		
		public override string ToString()
		{
			return _StyleName;
		}	

		public static ExControlStyles GetStyle(WebbReport webbReport)
        {            
			ExControlStyles excontrolStyle=new ExControlStyles();
			
			Band band=webbReport.Bands[BandKind.Detail];			
								
			foreach(XRControl xrControl in band.Controls)
			{
				if(!(xrControl is WinControlContainer))continue;
									
				Control c = (xrControl as WinControlContainer).WinControl;

				if(c is GridControl)
				{
					GridView gridView=(c as GridControl).GridView;

					excontrolStyle.ApplyStyle(gridView.Styles);

					return excontrolStyle;
					
				}
				else if(c is GroupingControl)
				{
					GroupView groupView=(c as GroupingControl)._GroupView;
						
					excontrolStyle.ApplyStyle(groupView.Styles);

					return excontrolStyle;	

				}
			}
			return excontrolStyle;
		}	

		#region Update Style
		public bool UpdateControlStyle(ref ReportSetting setting)
		{
			if(setting.WebbReport==null)return false;

			WebbReport webbReport=setting.WebbReport;

            if (!this.StyleName.ToLower().StartsWith("basic"))
            {
                if (this._SerializableWatermark != null && setting.WaterMarkOption.UseWaterMark)
                {
                    setting.WebbReport.Watermark = this._SerializableWatermark.ConvertTo();
                }
            }

			Band band=webbReport.Bands[BandKind.Detail];			
								
			foreach(XRControl xrControl in band.Controls)
			{
				if(!(xrControl is WinControlContainer))continue;
									
				Control c = (xrControl as WinControlContainer).WinControl;

				if(setting.TemplateType==TemplateType.PlayList)
				{
					if(!(c is GridControl))continue;	
			
					if(this.ExcontrolStyles!=null)
					{
						GridView gridView=(c as GridControl).GridView;

						Font rowFont=gridView.Styles.RowStyle.Font;

                        if (!this.ExcontrolStyles.IsAllStyleFontEdited())
                        {
                            this.ExcontrolStyles.RowStyle.Font = (Font)rowFont.Clone();

                            this.ExcontrolStyles.AlternateStyle.Font = (Font)rowFont.Clone();

                            this.ExcontrolStyles.HeaderStyle.Font = (Font)rowFont.Clone();
                        }

                        if (!this.ExcontrolStyles.IsFontEdit(this.ExcontrolStyles.SectionStyle.Font))
                        {
                            this.ExcontrolStyles.SectionStyle.Font = new Font(this.ExcontrolStyles.RowStyle.Font.FontFamily.Name, this.ExcontrolStyles.RowStyle.Font.Size + 2, this.ExcontrolStyles.RowStyle.Font.Style | FontStyle.Bold);
                        }

						this.ExcontrolStyles.SectionStyle.HorzAlignment=DevExpress.Utils.HorzAlignment.Near;

						DevExpress.XtraPrinting.BorderSide sides=this.ExcontrolStyles.RowStyle.Sides;

						if(sides== DevExpress.XtraPrinting.BorderSide.None)
						{
							foreach(GridColumn column in gridView.GridInfo.Columns)
							{
								if(column.Style.IsEdited())
								{
									column.Style.Sides=DevExpress.XtraPrinting.BorderSide.None;
    							}
							}
						}					

						gridView.Styles.ApplyStyle(this.ExcontrolStyles);

					}
					return true;
					
				}
				else if(setting.TemplateType==TemplateType.Group)
				{					
					if(!(c is GroupingControl))continue;	
					
					if(this.ExcontrolStyles!=null)
					{
						GroupView groupView=(c as GroupingControl)._GroupView;

						Font rowFont=groupView.Styles.RowStyle.Font;

                        if (!this.ExcontrolStyles.IsAllStyleFontEdited())
                        {
                            this.ExcontrolStyles.RowStyle.Font = (Font)rowFont.Clone();

                            this.ExcontrolStyles.AlternateStyle.Font = (Font)rowFont.Clone();

                            this.ExcontrolStyles.HeaderStyle.Font = (Font)rowFont.Clone();
                        }

                        if (!this.ExcontrolStyles.IsFontEdit(this.ExcontrolStyles.SectionStyle.Font))
                        {
                            this.ExcontrolStyles.SectionStyle.Font = new Font(this.ExcontrolStyles.RowStyle.Font.FontFamily.Name, this.ExcontrolStyles.RowStyle.Font.Size + 2, this.ExcontrolStyles.RowStyle.Font.Style | FontStyle.Bold);
                        }

						this.ExcontrolStyles.SectionStyle.HorzAlignment=DevExpress.Utils.HorzAlignment.Near;

						DevExpress.XtraPrinting.BorderSide sides=this.ExcontrolStyles.RowStyle.Sides;

						if(sides== DevExpress.XtraPrinting.BorderSide.None)
						{
							RemoveSides(groupView.RootGroupInfo);
						}
					  
						groupView.Styles.ApplyStyle(this.ExcontrolStyles);

					}
				}
			}
			return true;           

		}

		public void RemoveSides(GroupInfo groupInfo)
		{
			if(groupInfo.Style.IsEdited())
			{
				groupInfo.Style.Sides=DevExpress.XtraPrinting.BorderSide.None;
			}
			if(groupInfo.Summaries!=null)
			{
				foreach(GroupSummary summary in groupInfo.Summaries)
				{
					if(summary.Style.IsEdited())
					{
						summary.Style.Sides=DevExpress.XtraPrinting.BorderSide.None;
					}
				}
			}
		
			foreach(GroupInfo subGroupInfo in groupInfo.SubGroupInfos)
			{
				RemoveSides(subGroupInfo);				
			}

		}    

        public bool UpdateSampleReportStyle(WebbReport webbReport)
        {
            if (this._SerializableWatermark != null)
            {
                webbReport.Watermark = this._SerializableWatermark.ConvertTo();
            }

            Band band = webbReport.Bands[BandKind.Detail];

            foreach (XRControl xrControl in band.Controls)
            {
                if (!(xrControl is WinControlContainer)) continue;

                Control c = (xrControl as WinControlContainer).WinControl;

                if (WizardCustomStylesCollection.AvailbleWizardStyles.TemplateType == TemplateType.PlayList)
                {
                    if (!(c is GridControl)) continue;

                    GridView gridView = (c as GridControl).GridView;                   

                    gridView.Styles.ApplyStyle(this.ExcontrolStyles);

                    return true;

                }
                else if (WizardCustomStylesCollection.AvailbleWizardStyles.TemplateType == TemplateType.Group)
                {
                    if (!(c is GroupingControl)) continue;

                    if (this.ExcontrolStyles != null)
                    {
                        GroupView groupView = (c as GroupingControl)._GroupView;
                       
                        groupView.Styles.ApplyStyle(this.ExcontrolStyles);
                    }
                }
            }

            return true;
        }


		#endregion

        #region  properties

		public string StyleName
		{
            get
            {
                if (_StyleName == null) _StyleName = string.Empty;
                return _StyleName; }
			set{ _StyleName = value; }
		}

		public System.Drawing.Image Image
		{
			get{ return _Image; }
			set{ _Image = value; }
		}
		public System.Drawing.Image NoWaterMarkImage
		{
			get{ return _NoWaterMarkImage; }
			set{ _NoWaterMarkImage = value; }
		}

		public Webb.Reports.ExControls.Styles.ExControlStyles ExcontrolStyles
		{
			get{ return _ExcontrolStyles; }
			set{ _ExcontrolStyles = value; }
		}

		public Webb.Reports.ReportWizard.WizardInfo.TemplateType TemplateType
		{
			get{ return templateType; }
			set{ templateType = value; }
		}

		public Webb.Reports.SerializableWatermark SerializableWatermark
		{
			get{ return _SerializableWatermark; }
			set{ _SerializableWatermark = value; }
        }
        #endregion

        #region Copy Function By Macro 2009-12-22 10:31:20
        public WizardCustomStyles Copy()
		{
			WizardCustomStyles thiscopy=new WizardCustomStyles();
			thiscopy._StyleName=this._StyleName;
			if(_Image!=null)thiscopy._Image=new Bitmap(this._Image);
			if(NoWaterMarkImage!=null)thiscopy.NoWaterMarkImage=new Bitmap(this.NoWaterMarkImage);
			thiscopy._ExcontrolStyles.ApplyStyle(this._ExcontrolStyles);
			thiscopy.templateType=this.templateType;
			thiscopy._SerializableWatermark=this._SerializableWatermark;
           	return thiscopy;
		}

        public void ApplyStyle(WizardCustomStyles style)
        {
            if (style == null) return;

            this._StyleName = style.StyleName;

            this._Image = style.Image;
            this.NoWaterMarkImage = style.NoWaterMarkImage;
            this._ExcontrolStyles.ApplyStyle(style._ExcontrolStyles);
            this.templateType = style.templateType;
            this._SerializableWatermark = style._SerializableWatermark;
          
        }
		#endregion

		#region Serialization By Simon's Macro 2010-1-29 14:12:35
		public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{           
			info.AddValue("_StyleName",_StyleName);
			info.AddValue("_Image",_Image,typeof(System.Drawing.Image));
			info.AddValue("_NoWaterMarkImage",_NoWaterMarkImage,typeof(System.Drawing.Image));
			info.AddValue("_ExcontrolStyles",_ExcontrolStyles,typeof(Webb.Reports.ExControls.Styles.ExControlStyles));
			info.AddValue("templateType",templateType,typeof(Webb.Reports.ReportWizard.WizardInfo.TemplateType));
			info.AddValue("_SerializableWatermark",_SerializableWatermark,typeof(Webb.Reports.SerializableWatermark));
		
		}

		public WizardCustomStyles(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{          
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
				_Image=(System.Drawing.Image)info.GetValue("_Image",typeof(System.Drawing.Image));
			}
			catch
			{
			}
			try
			{
				_NoWaterMarkImage=(System.Drawing.Image)info.GetValue("_NoWaterMarkImage",typeof(System.Drawing.Image));
			}
			catch
			{
			}
			try
			{
				_ExcontrolStyles=(Webb.Reports.ExControls.Styles.ExControlStyles)info.GetValue("_ExcontrolStyles",typeof(Webb.Reports.ExControls.Styles.ExControlStyles));
			}
			catch
			{
			}
			try
			{
				templateType=(Webb.Reports.ReportWizard.WizardInfo.TemplateType)info.GetValue("templateType",typeof(Webb.Reports.ReportWizard.WizardInfo.TemplateType));
			}
			catch
			{
			}
			try
			{
				_SerializableWatermark=(Webb.Reports.SerializableWatermark)info.GetValue("_SerializableWatermark",typeof(Webb.Reports.SerializableWatermark));
			}
			catch
			{
			}
		}
		#endregion
		
		
	}


	[Serializable]
	public class WizardCustomStylesCollection:WebbCollection
	{
		protected TemplateType templateType=TemplateType.PlayList;

        protected int _DefaultReportStyleIndex = 0;

		public WizardCustomStylesCollection():base(){}

		#region WizardCustomStyles this[int index]  &  WizardStyles this[string stylename]

		public WizardCustomStyles this[int index]
		{
			get{return this.innerList[index] as WizardCustomStyles;}
			set{this.innerList[index]=value;}
		}
		public WizardCustomStyles this[string stylename]
		{
			get
			{
				foreach(WizardCustomStyles wizardStyles in this)
				{
					if(wizardStyles.StyleName==stylename)return wizardStyles;
				}
				return null;		
			}		
		}
		#endregion

        #region Add/Delete/Clear
        public int Add(WizardCustomStyles wizardCustomStyles)
		{
			WizardCustomStyles wizardStyles=this[wizardCustomStyles.StyleName];

			if(wizardStyles!=null)return -1;
			
			wizardCustomStyles.TemplateType=this.templateType;
			
			return this.innerList.Add(wizardCustomStyles);
		}
		public void Delete(string stylname)
		{
			WizardCustomStyles wizardStyles=this[stylname];

			if(wizardStyles!=null)
			{
				this.innerList.Remove(wizardStyles);
			}
		}

        public override void Clear()
        {
            this.innerList.Clear();
        }
		#endregion

		#region Properties &Serialization

		#region Serialization By Simon's Macro 2009-8-7 15:42:02
		public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			base.GetObjectData(info,context);

			info.AddValue("templateType",templateType,typeof(Webb.Reports.ReportWizard.WizardInfo.TemplateType));

            info.AddValue("_DefaultReportStyleIndex", _DefaultReportStyleIndex);		
		}

		public WizardCustomStylesCollection(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context):base(info,context)
		{
			try
			{
				templateType=(Webb.Reports.ReportWizard.WizardInfo.TemplateType)info.GetValue("templateType",typeof(Webb.Reports.ReportWizard.WizardInfo.TemplateType));
			}
			catch
			{
			}
            try
            {
                _DefaultReportStyleIndex = info.GetInt32("_DefaultReportStyleIndex");
            }
            catch
            {
                _DefaultReportStyleIndex = 0;
            }
		}
		#endregion

		public Webb.Reports.ReportWizard.WizardInfo.TemplateType TemplateType
		{
			get{ return templateType; }
			set{ templateType = value; }
		}

        public int DefaultReportStyleIndex
        {
            get
            {
                return this._DefaultReportStyleIndex;
            }
            set
            {
                this._DefaultReportStyleIndex = value;
            }
        }


	    

		#region Static Fields&properties
		private static WizardCustomStylesCollection _AvailbleWizardStyles=new WizardCustomStylesCollection();
		
		public static WizardCustomStylesCollection AvailbleWizardStyles
		{
			get
			{
				if(_AvailbleWizardStyles==null)_AvailbleWizardStyles=new WizardCustomStylesCollection();
					
				return _AvailbleWizardStyles;
			}
			set
			{
				_AvailbleWizardStyles=value;
			}
		}
		#endregion
		#endregion       
	
        #region static Get  View
        public static ExControlView GetView(TemplateType templateType,WebbReport webbReport)
		{
			Band detailband=webbReport.Bands[BandKind.Detail];
								
			foreach(XRControl xrControl in detailband.Controls)
			{
				if(!(xrControl is WinControlContainer))continue;
									
				Control c = (xrControl as WinControlContainer).WinControl;

				if(templateType==TemplateType.PlayList)
				{
					if(!(c is GridControl))continue;
					return (c as GridControl).GridView;		
					
				}
				else if(templateType==TemplateType.Group)
				{
					if(!(c is GroupingControl))continue;
					return (c as GroupingControl)._GroupView;					
				}
			}
			return null;
        }
        #endregion

        #region Save&load style
        //public static string GetOldStylePath(string templateType)
        //{
        //    string dir = Webb.Utility.ApplicationDirectory + @"Template\Styles\StyleLists";

        //    if(!Directory.Exists(dir))
        //    {	
        //        Directory.CreateDirectory(dir);
        //    }
        //    return string.Format(@"{0}\{1}.wst",dir,templateType);
        //}
        public static string GetStylePath(string templateType)
        {
            string dir = Webb.Utility.ApplicationDirectory + @"Template\Styles\ReportStyleLists";

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            return string.Format(@"{0}\{1}.wst", dir, templateType);
        }
		public static string GetDefaultStyleReportPath(TemplateType templateType)
		{
            string dir = Webb.Utility.ApplicationDirectory + @"Template\Styles\StyleReports";

			if(!Directory.Exists(dir))
			{	
				Directory.CreateDirectory(dir);
			}

			return string.Format(@"{0}\{1}.repx",dir,templateType);
		}



		public static WebbDataSource GetDefaultDataSource()
		{
            string dir = Webb.Utility.ApplicationDirectory + @"Template\Styles\StyleReports";

			if(!Directory.Exists(dir))
			{	
				Directory.CreateDirectory(dir);
			}

			string DataSourceFile=string.Format(@"{0}\data.wrdf",dir);
			
			WebbDataSource dataSource=WrdfFileManager.ReadDataConfig(DataSourceFile,"");

			if(dataSource!=null)
			{
				WebbDataProvider provider=new WebbDataProvider();

				ReportSetting.SetDataSource(provider,dataSource);
			}

			return dataSource;

		}



		public static void SerirlizeStyles()
		{	
			if(_AvailbleWizardStyles==null)return;

			string stylepath=GetStylePath(_AvailbleWizardStyles.TemplateType.ToString());

			if(File.Exists(stylepath))
			{
				File.Delete(stylepath);
			}
			
			System.IO.FileStream stream = System.IO.File.Open(stylepath,System.IO.FileMode.OpenOrCreate);
	
			try
			{			

				Webb.Utilities.Serializer.SerializeObject(stream,_AvailbleWizardStyles);
			}
			catch(Exception ex)
			{
				Webb.Utilities.MessageBoxEx.ShowError("Failed to save style files!\n"+ex.Message);				
			}
			finally
			{
				stream.Close();
			}

		}
		public static void DeSerirlizeStyles(TemplateType templateType)
		{
			string stylepath=GetStylePath(templateType.ToString());

			if(File.Exists(stylepath))
			{				
				System.IO.FileStream stream = System.IO.File.Open(stylepath,System.IO.FileMode.Open);

				try
				{
					_AvailbleWizardStyles= Webb.Utilities.Serializer.DeserializeObject(stream) as  WizardCustomStylesCollection;
				}
				catch(Exception ex)
				{
					Webb.Utilities.MessageBoxEx.ShowError("Failed to read style files!\n"+ex.Message);

					Environment.Exit(-1);
				}
				finally
				{
					stream.Close();
				}
			}
			else
			{
				_AvailbleWizardStyles=new WizardCustomStylesCollection();

				_AvailbleWizardStyles.TemplateType=templateType;

			}		   
		}
		public static void SerirlizeStyles(string stylepath)
		{	
			if(_AvailbleWizardStyles==null)return;			
	
			if(File.Exists(stylepath))
			{
				File.Delete(stylepath);
			}
				
			System.IO.FileStream stream = System.IO.File.Open(stylepath,System.IO.FileMode.OpenOrCreate);
		
			try
			{						
				Webb.Utilities.Serializer.SerializeObject(stream,_AvailbleWizardStyles);
	
				Webb.Utilities.MessageBoxEx.ShowMessage("Success to export the file!");				
			}
			catch(Exception ex)
			{
				Webb.Utilities.MessageBoxEx.ShowError("Failed to save style files!\n"+ex.Message);				
			}
			finally
			{
				stream.Close();
			}
		}
		public static bool DeSerirlizeStyles(string stylepath)
		{
			WizardCustomStylesCollection styles=null;
	
			if(File.Exists(stylepath))
			{				
				System.IO.FileStream stream = System.IO.File.Open(stylepath,System.IO.FileMode.Open);
	
				try
				{
					styles= Webb.Utilities.Serializer.DeserializeObject(stream) as WizardCustomStylesCollection;
	
					stream.Close();
	
					_AvailbleWizardStyles=styles;

					return true;	
				}
				catch(Exception ex)
				{
					stream.Close();
	
					Webb.Utilities.MessageBoxEx.ShowError("Failed to read style files!\n"+ex.Message);			
	
					return false;					
				}
					
			}
			return false;
		}	

		#endregion
		
	}

}


