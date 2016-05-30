//12-20-2007@Scott
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

using System.IO;

namespace Webb.Reports.ExControls.Views
{
    [Flags]
    [Serializable]
    public enum InvertPictureMode
    {
        None=0,
        Rotate90=1,
        Rotate180=2,
        Rotate270=4,
        HorizontalInvert=8,
        VerticalInvert=16       
    }

    [Serializable]
    public enum PlayBookScoutType
    {
        Default,
        Offense,
        Defense,
        OffenseBaseFormation,
        DefenseBaseFormation
    }
	/// <summary>
	/// Summary description for CustomImageView.
	/// </summary>
	[Serializable]
	public class CustomImageView : ExControlView , ISerializable
	{
		private PictureBoxSizeMode _SizeMode;

		private Image _Image;	  

		private bool _OneValue=false;

		private string _DiagramImage="";   //Added this code at 2008-11-3 10:45:45@Simon

		private string _Field;		//Modified at 2009-1-4 9:52:42@Scott

        private bool _UsePicDir=false;

        private Size _Offset = new Size(0, 0);  // 01-04-2012 Scott

        protected bool _ReadDiagramFromPlaybook = false;

        private InvertPictureMode _InvertPicture = InvertPictureMode.None;

        protected PlayBookScoutType _PlayBookScoutType = PlayBookScoutType.Default;

        protected SortingColumnCollection _SortingColumns = new SortingColumnCollection();
     
        [NonSerialized]
        protected string _ImagePath = string.Empty;

        #region propertyies

        public SortingColumnCollection SortingColumns
        {
            get
            {
                if (_SortingColumns == null) _SortingColumns = new SortingColumnCollection();
                return _SortingColumns;
            }
            set
            {
                _SortingColumns = value;
            }
        }

        public string ImagePath
        {
            get { 
                   if(this._ImagePath==null)_ImagePath=string.Empty;
                   return this._ImagePath; 
                }
            set
            {
                this._ImagePath = value;
            }
        }

        public PlayBookScoutType PlayBookScoutType
        {
            get { return this._PlayBookScoutType; }
            set {this._PlayBookScoutType = value; }
        }

        public bool ReadDiagramFromPlaybook
        {
            get { return this._ReadDiagramFromPlaybook;}
            set { this._ReadDiagramFromPlaybook = value; }
        }


        public InvertPictureMode InvertPicture
        {
            get
            {
                return this._InvertPicture;
            }
            set
            {
                _InvertPicture = value;
            }
        }

        public bool UsePicDir
        {
            get { return _UsePicDir; }
            set { _UsePicDir = value; }
        }
		public string Field
		{
			get{
                if(_Field==null)_Field=string.Empty;
				return this._Field;
			   }
			set{this._Field = value;}
		}

		public string DiagramImage         //Added this code at 2008-11-3 10:47:00@Simon
		{
			get{return this._DiagramImage;}
			set{this._DiagramImage = value;}
		}

		public bool OneValue
		{
			get{return this._OneValue;}
			set{this._OneValue = value;}
		}

		public PictureBoxSizeMode SizeMode
		{
			get{return this._SizeMode;}
			set{this._SizeMode = value;}
		}
		
		public Image Image
		{
			get{return this._Image;}
			set
			{
				this._Image = value;              
			}
        }
        #endregion

        #region ISerializable members
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData (info, context);
			info.AddValue("Image",this._Image,typeof(Image));
			info.AddValue("SizeMode",this._SizeMode,typeof(PictureBoxSizeMode));
			info.AddValue("Field",this.Field);
            info.AddValue("_UsePicDir", this._UsePicDir);
            info.AddValue("_ReadDiagramFromPlaybook", this._ReadDiagramFromPlaybook);
            info.AddValue("_InvertPicture", this._InvertPicture, typeof(InvertPictureMode));
            info.AddValue("_PlayBookScoutType", this._PlayBookScoutType, typeof(PlayBookScoutType));
            info.AddValue("_SortingColumns", this._SortingColumns, typeof(SortingColumnCollection));
            info.AddValue("_Offset", this._Offset, typeof(Size));   // 01-04-2012 Scott
		}
		public CustomImageView(SerializationInfo info, StreamingContext context) : base(info,context)
		{
            try
            {
                this._SortingColumns = (SortingColumnCollection)info.GetValue("_SortingColumns", typeof(SortingColumnCollection));
            }
            catch
            {
                _SortingColumns =new SortingColumnCollection();
            }
            try
            {
                this._PlayBookScoutType = (PlayBookScoutType)info.GetValue("_PlayBookScoutType", typeof(PlayBookScoutType));
            }
            catch
            {
                _PlayBookScoutType = PlayBookScoutType.Default;
            }
            try
            {
                this._InvertPicture = (InvertPictureMode)info.GetValue("_InvertPicture", typeof(InvertPictureMode));
            }
            catch
            {
                _InvertPicture = InvertPictureMode.None;
            }
			try
			{
				this._Image = info.GetValue("Image",typeof(Image)) as Image;
			}
			catch
			{
				
			}

			try
			{
				this._SizeMode = (PictureBoxSizeMode)info.GetValue("SizeMode",typeof(PictureBoxSizeMode));
			}
			catch
			{
				this._SizeMode = PictureBoxSizeMode.StretchImage;
			}

			try
			{
				this._Field = info.GetString("Field");
			}
			catch
			{
				this._Field = string.Empty;
			}
            try
            {
                this._UsePicDir = info.GetBoolean("_UsePicDir");
            }
            catch
            {
                this._UsePicDir = false;
            }
            try
            {
                this._ReadDiagramFromPlaybook = info.GetBoolean("_ReadDiagramFromPlaybook");
            }
            catch
            {
                this._ReadDiagramFromPlaybook = false;
            }
            // 01-04-2012 Scott
            try
            {
                this._Offset = (Size)info.GetValue("_Offset",typeof(Size));
            }
            catch
            {
                this._Offset = new Size(0, 0);
            }
		}
		#endregion

		public CustomImageView(CustomImage i_Control):base(i_Control as ExControl)
		{
			this.CreatePrintingTable();
		}

        private Image InverImage(Image oldImage)
        {
            if (this._InvertPicture == InvertPictureMode.None) return oldImage;

            Image image = new Bitmap(oldImage);

            RotateFlipType rotateFlip=RotateFlipType.RotateNoneFlipNone;

            switch (this._InvertPicture)
            {
                case InvertPictureMode.Rotate90:
                    rotateFlip = RotateFlipType.Rotate90FlipNone;
                    break;
                case InvertPictureMode.Rotate180:
                    rotateFlip = RotateFlipType.Rotate180FlipNone;
                    break;
                case InvertPictureMode.Rotate270:
                    rotateFlip = RotateFlipType.Rotate270FlipNone;
                    break;
                case InvertPictureMode.HorizontalInvert:
                    rotateFlip=RotateFlipType.RotateNoneFlipX;
                    break;
                case InvertPictureMode.VerticalInvert:
                    rotateFlip = RotateFlipType.RotateNoneFlipY;
                    break;
            }

            image.RotateFlip(rotateFlip);

            return image;
        }

		public override bool CreatePrintingTable()
		{
            if (this._Image != null)
            {
                this.PrintingTable = new WebbTable(1, 1);

                IWebbTableCell cell = this.PrintingTable.GetCell(0, 0);

                cell.CellStyle.BorderColor = Color.Transparent;

                cell.Image = this.InverImage(this._Image);

                if (this._SizeMode == PictureBoxSizeMode.StretchImage)
                {
                    cell.CellStyle.Width = (int)(this.ExControl.XtraContainer.Width / Webb.Utility.ConvertCoordinate);

                    cell.CellStyle.Height = (int)(this.ExControl.XtraContainer.Height / Webb.Utility.ConvertCoordinate);
                }
                else if (this._SizeMode == PictureBoxSizeMode.CenterImage)
                {// 01-04-2012 Scott
                    int nWidth = (int)(this.ExControl.XtraContainer.Width / Webb.Utility.ConvertCoordinate);
                    int nHeight = (int)(this.ExControl.XtraContainer.Height / Webb.Utility.ConvertCoordinate);

                    int nSuggestedWidth = (int)((float)nHeight * this._Image.Width / this._Image.Height);
                    int nSuggestedHeight = (int)((float)nWidth * this._Image.Height / this._Image.Width);

                    if (nWidth > nSuggestedWidth)
                    {
                        this._Offset = new Size((int)(((float)nWidth - nSuggestedWidth) / 2), 0);

                        this.SetOffset(0, 0);

                        nWidth = nSuggestedWidth;
                    }
                    else
                    {
                        this._Offset = new Size(0, (int)(((float)nHeight - nSuggestedHeight) / 2));

                        this.SetOffset(0, 0);

                        nHeight = nSuggestedHeight;
                    }

                    cell.CellStyle.Width = nWidth;

                    cell.CellStyle.Height = nHeight;
                }
                else
                {
                    cell.CellStyle.Width = this._Image.Width;

                    cell.CellStyle.Height = this._Image.Height;
                }

                if (this._SizeMode == PictureBoxSizeMode.CenterImage)
                {// 01-04-2012 Scott
                    cell.ImageSizeMode = PictureBoxSizeMode.StretchImage;
                }
                else
                {
                    cell.ImageSizeMode = (PictureBoxSizeMode)Enum.Parse(typeof(PictureBoxSizeMode), this._SizeMode.ToString(), false);
                }

                this.PrintingTable.ExControl = this.ExControl;
            }

			return true;
		}	
		
		private string GetFieldValue(DataTable dt)	//Modified at 2009-1-4 9:57:34@Scott
		{
			string strRet = string.Empty;

			if(dt==null||this.ExControl==null)return "<NULLVALUES>"; 
 
			if(_Field==null||(this._Field==string.Empty)||_Field=="<None>"||!dt.Columns.Contains(_Field))return "<NULLVALUES>"; 	

			Int32Collection m_Rows=this.OneValueScFilter.Filter.GetFilteredRows(dt);

			if(this.ExControl.Report!=null)m_Rows=this.ExControl.Report.Filter.GetFilteredRows(dt,m_Rows);

			 m_Rows=this.RepeatFilter.Filter.GetFilteredRows(dt,m_Rows);

             if (this.SortingColumns.Count > 0)
             {
                 GroupResultCollection results = this.SortingColumns.Sorting(dt, m_Rows);

                 if (results != null && results.Count != 0)
                 {
                     m_Rows = results[0].RowIndicators;
                 }
             }

            if(m_Rows.Count==0)return "<NULLVALUES>"; 

			int firstRow=m_Rows[0];

            if (dt.Rows[firstRow][_Field] is System.DBNull || dt.Rows[firstRow][_Field]==null)return string.Empty;

            strRet=(string)dt.Rows[firstRow][_Field];
          
			return strRet;
		}

        private string GetPlayBookImagePath(DataTable dt)	//Modified at 2009-1-4 9:57:34@Scott
        {
            string strRet = string.Empty;

            if (dt == null || this.ExControl == null) return strRet;

            if (!dt.Columns.Contains("ImagePath")) return string.Empty;

            Int32Collection m_Rows = this.OneValueScFilter.Filter.GetFilteredRows(dt);

            if (this.ExControl.Report != null) m_Rows = this.ExControl.Report.Filter.GetFilteredRows(dt, m_Rows);

            m_Rows = this.RepeatFilter.Filter.GetFilteredRows(dt, m_Rows);

            if (this.SortingColumns.Count > 0)
            {
                GroupResultCollection results = this.SortingColumns.Sorting(dt, m_Rows);

                if (results != null && results.Count != 0)
                {
                    m_Rows = results[0].RowIndicators;
                }
            }


            if (m_Rows.Count == 0) return string.Empty;

            int firstRow = m_Rows[0];

            strRet = (string)dt.Rows[firstRow]["ImagePath"];

            return strRet;
        }

		#region Modified Area 

		private void GetOneValueImage(DataTable dt)
        {
            int nWidth = this.ExControl.Width,nHeight = this.ExControl.Height;

			nHeight = (int)((float)nWidth*2/3);

			string strField = string.Empty;

			string strValue = "<NULLVALUES>";

            strValue = this.GetFieldValue(dt);

			if(strValue=="<NULLVALUES>")
			{
				if(this.Repeat)
				{
					strValue=this.RepeatFilter.FilterName;
					
				}
				else
				{
					strValue=this.OneValueScFilter.FilterName;
				}
			}			

			if(this.OneValue)
            {
                #region OneValue

                this.Image = null;	

				strField = this.Field;		

				string strScoutType = string.Empty;

				if(Webb.Utility.CurReportMode == 0)
				{
					strScoutType = this.ExControl.Report.Template.DiagramScoutType.ToString();
				}
				else
				{
					strScoutType = Webb.Reports.DataProvider.VideoPlayBackManager.DiagramScoutType;
				}

                bool IsAddingPlayBookGames = false;

                bool IsAddingCCRMData = false;

				WebbDataProvider dataProvider= Webb.Reports.DataProvider.VideoPlayBackManager.PublicDBProvider;

				string strUserFolder =string.Empty;                

				if(dataProvider!=null&&dataProvider.DBSourceConfig!=null)
				{
					strUserFolder=dataProvider.DBSourceConfig.UserFolder;

                    if (strUserFolder == null) strUserFolder = string.Empty;

                    IsAddingPlayBookGames = (dataProvider.DBSourceConfig.WebbDBType == WebbDBTypes.WebbPlaybook);

                    IsAddingCCRMData=(dataProvider.DBSourceConfig.WebbDBType == WebbDBTypes.CoachCRM);
				}

				int index=strUserFolder.IndexOf(";");

				if(index>=0)
				{
					strUserFolder=strUserFolder.Substring(0,index);  
				}

				if(Webb.Reports.DataProvider.VideoPlayBackManager.DiaPath!=string.Empty)
				{
                    strUserFolder=Webb.Reports.DataProvider.VideoPlayBackManager.DiaPath;

				    strUserFolder=strUserFolder.TrimEnd(@"\".ToCharArray());	
				}
				
				string strImageFilePath = string.Format(@"{0}\Diagrams\{1}\{2}\{3}.dia",strUserFolder,strScoutType,strField,strValue);
                
                PlayBookData playBookData = new PlayBookData();

                if (IsAddingPlayBookGames)
                {
                    nWidth = this.ExControl.Width;
                    
                    nHeight = this.ExControl.Height;

                    string strGetImagePath = this.GetPlayBookImagePath(dt);

                    #region Play Book Games

                    if (System.IO.File.Exists(strGetImagePath))
                    {
                        this.Image = Webb.Utility.ReadImageFromPath(strGetImagePath);
                    }
                    #endregion
                }
                else if (IsAddingCCRMData)
                {
                    nWidth = this.ExControl.Width;

                    nHeight = this.ExControl.Height;

                    if (System.IO.File.Exists(strValue))
                    {
                        this.Image = Webb.Utility.ReadImageFromPath(strValue);                       
                    }
                }
                else if (this._ReadDiagramFromPlaybook)
                {
                    #region PlayBook Diagram
                    if (this._PlayBookScoutType == PlayBookScoutType.Default)
                    {
                        strScoutType = playBookData.CheckOffensiveField(strScoutType, strField);

                        if (playBookData.CheckIsPlaybookImageField(strField))
                        {
                            this.Image = Webb.Utility.ReadImageFromPath(strValue);
                        }
                        else
                        {
                            this.Image = playBookData.ReadPictureFromValue(strScoutType, strField, strValue);
                        }
                    }
                    else if (this._PlayBookScoutType == PlayBookScoutType.OffenseBaseFormation)
                    {
                        this.Image = playBookData.ReadBaseFormation("Offense");
                    }
                    else if (this._PlayBookScoutType == PlayBookScoutType.DefenseBaseFormation)
                    {
                        this.Image = playBookData.ReadBaseFormation("Defense");
                    }
                    else
                    {
                        strScoutType = this._PlayBookScoutType.ToString();

                        if (playBookData.CheckIsPlaybookImageField(strField))
                        {
                            this.Image = Webb.Utility.ReadImageFromPath(strValue);
                        }
                        else
                        {
                            this.Image = playBookData.ReadPictureFromValue(strScoutType, strField, strValue);
                        }
                    }
                   
                    #endregion

                }
                else if (this._UsePicDir)
                {
                    #region Use Picture  in  destine-Directory

                    strUserFolder = Webb.Reports.DataProvider.VideoPlayBackManager.PictureDir;

                    strImageFilePath = string.Format(@"{0}\SavePic\{1}\{2}.bmp", strUserFolder, strField, strValue);

                    if (File.Exists(strImageFilePath)) this.Image = Webb.Utility.ReadImageFromPath(strImageFilePath);

                    #endregion
                } 
                else if(playBookData.CheckIsPlaybookImageField(strField))
                {
                    this.Image = Webb.Utility.ReadImageFromPath(strValue);
                }
                else
                {
                    #region Advantage Diagram

                    if (File.Exists(strImageFilePath))
                    {
                        Image image = new Bitmap(nWidth, nHeight);

                        Graphics g = Graphics.FromImage(image);

                        g.Clear(Color.White);   //Modify at 2009-2-24 10:27:26@Simon

                        AdvDiagram diagram = new AdvDiagram();

                        diagram.OpenDiagram(strImageFilePath);

                        diagram.DrawDiagram(g, nWidth, nHeight);

                        this.Image = image;
                    }

                    #endregion

                }
                #endregion
            }
			else
			{
				if(this._DiagramImage.ToLower().EndsWith(".dia")&& File.Exists(this._DiagramImage))
				{
					Image image = new Bitmap(nWidth,nHeight);

					Graphics g = Graphics.FromImage(image);

					g.Clear(Color.White);  //Modify at 2009-2-24 10:27:30@Simon

					AdvDiagram diagram = new AdvDiagram();

					diagram.OpenDiagram(this._DiagramImage);

					diagram.DrawDiagram(g,nWidth,nHeight);

					this.Image = image;
				}
			}
		}

		#endregion        //End Modify at 2008-10-7 10:29:20@Simon


        // 01-04-2012 Scott
        public override void SetOffset(int left, int top)
        {
            if (this.PrintingTable == null) return;

            this.PrintingTable.SetOffset(left + this._Offset.Width, top + this._Offset.Height);
        }

		public override void CalculateResult(DataTable i_Table)
		{
			//base.CalculateResult (i_Table);
		   this.GetOneValueImage(i_Table);  //Modified at 2008-10-7 10:26:04@Simon
		}

        public override void GetALLUsedFields(ref ArrayList usedFields)
        {
            if (!usedFields.Contains(this.Field)) usedFields.Add(this.Field);
        }
        
		public override void Paint(PaintEventArgs e)
		{
			if(this.PrintingTable != null && this.Image != null)
			{
				this.PrintingTable.PaintTable(e,true,Rectangle.Empty);
			}
			else
			{
				base.Paint (e);
			}
		}

	}
}
