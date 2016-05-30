using System;
using System.Collections;

using System.Data;
using System.Text;
using System.Drawing;
using System.Collections.Specialized;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Serialization;
using System.Diagnostics;
using System.ComponentModel;
using System.ComponentModel.Design;

using DevExpress.Utils;
using Webb.Collections;
using System.Windows.Forms;

namespace Webb.Reports.ExControls.Data
{
	
	#region public class MaskInfo:ISerializable
	[Serializable]
	public class MaskInfo:ISerializable
	{
		#region Field &Constructor
		private Font _TitleFont = new Font(AppearanceObject.DefaultFont.FontFamily, 10f);
		
		private Color _TitleTextColor = Color.Black;
		private Color _TitleBackColor = Color.Transparent;
		private string _Title=string.Empty;
		private int _TitleWidth=55;
		private HorzAlignment _TitleAlignment = HorzAlignment.Center;	
		private VertAlignment _VerticalAlignment =VertAlignment.Bottom;	
        
		[NonSerialized]
		private string _MaskedText=string.Empty;	

		private bool _ShowTitle=true;
		private bool _ShowUnderLine=true;

		private Color _MaskedTextColor = Color.Black;   //2010-2-8 14:19:27@Simon Add this Code
		private Color _MaskedBackColor = Color.Transparent;
		private HorzAlignment _MaskedAlignment = HorzAlignment.Center;
		private string _MaskedField = string.Empty;	//Modified at 2008-12-15 13:28:56@Scott		
		private Font _MaskedFont = new Font(AppearanceObject.DefaultFont.FontFamily, 10f);		
		private int _MaskedWidth=55;
        private MaskInfoCollection _BrotherMaskInfos = new MaskInfoCollection();
        protected string _DateFormat = @"M/d/yy";       

	    public MaskInfo()
		{			
			_TitleFont = new Font(AppearanceObject.DefaultFont.FontFamily, 10f);
			_TitleTextColor = Color.Black;
			_TitleBackColor = Color.Transparent;
			_Title=string.Empty;
			_TitleAlignment = HorzAlignment.Center;		
			_MaskedField = string.Empty;
		    _MaskedFont = new Font(AppearanceObject.DefaultFont.FontFamily, 10f);
			_MaskedWidth=55;
			_ShowTitle=true;
			 _TitleWidth=55;

			 _MaskedTextColor = Color.Black;
	         _MaskedBackColor = Color.Transparent;
	        _MaskedAlignment = HorzAlignment.Center;

			_ShowUnderLine=true;
			 _VerticalAlignment =VertAlignment.Bottom;
             _BrotherMaskInfos = new MaskInfoCollection();
             _DateFormat = @"M/d/yy";          
		}

		#endregion	

		#region property
        [Category("Brothers")]
        public MaskInfoCollection BrotherMaskInfos
        {
            get {
                if (this._BrotherMaskInfos == null) _BrotherMaskInfos = new MaskInfoCollection();
                   return this._BrotherMaskInfos;
                 }
            set { _BrotherMaskInfos = value; }
        }
     
        [Editor(typeof(Webb.Reports.Editors.FormatDateTimeEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Category("Data Format")]
        public string DateFormat
        {
            get
            {
                return this._DateFormat;
            }
            set
            {
                this._DateFormat = value;
            }
        }

		[Category("Vertical Aliginment")]
		public DevExpress.Utils.VertAlignment VerticalAlignment
		{
			get{ return this._VerticalAlignment; }
			set{ _VerticalAlignment = value; }
		}

	    [Category("Title Style")]
		public System.Drawing.Font TitleFont
		{
			get{ return _TitleFont; }
			set{ _TitleFont = value; }
		}
		 [Category("Title Style")]
		public System.Drawing.Color TitleTextColor
		{
			get{ return _TitleTextColor; }
			set{ _TitleTextColor = value; }
		}
       
        [Browsable(false)]
		[Category("Title Style")]
		public int TitleWidth
		{
			get{ return this._TitleWidth; }
			set{
				   if(value<=0)return;
				   _TitleWidth = value; 				  
			   }
        }
		[Category("Show")]
		public bool ShowTitle
		{
			get{return this._ShowTitle;}
			set{
				_ShowTitle=value;
			  }
		}
		[Category("Show")]
		public bool ShowUnderLine
		{
			get{return this._ShowUnderLine;}
			set
			{
				_ShowUnderLine=value;
			}
		}

		 [Category("Title Style")]
		public System.Drawing.Color TitleBackColor
		{
			get{ return _TitleBackColor; }
			set{ _TitleBackColor = value; }
		}
		
		[Category("Title")]
		public string Title
		{
			get{ return _Title; }
			set{ _Title = value; }
		}       

		[Category("Title Style")]
		public DevExpress.Utils.HorzAlignment TitleAlignment
		{
			get{ return _TitleAlignment; }
			set{ _TitleAlignment = value; }
		}
         [Category("MakedInfo")]
        //[TypeConverter(typeof(Webb.Data.PublicDBFieldConverter))]
        [Editor(typeof(Webb.Reports.Editors.FieldSelectEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public string MaskedField
		{
			get{ return _MaskedField; }
			set{ 
				  _MaskedField = value;
			 	
                  this._Title=_MaskedField+":";
			   }
		}
		
         [Category("MakedInfo")]
		public System.Drawing.Font MaskedFont
		{
			get{ return _MaskedFont; }
			set{ _MaskedFont = value; }
		}

        [Browsable(false)]
        [Category("MakedInfo")]
		public int MaskedWidth
		{
			get{ return _MaskedWidth; }
			set{ _MaskedWidth = value; }
		}
		[Category("MakedInfo")]
		public HorzAlignment  MaskedAlignment
		{
			get{ return this._MaskedAlignment; }
			set{ _MaskedAlignment = value; }
		}
		[Category("MakedInfo")]
		public Color MaskedBackColor
		{
			get{ return this._MaskedBackColor; }
			set{ _MaskedBackColor = value; }
		}
		[Category("MakedInfo")]
		public Color MaskedTextColor
		{
			get{ return this._MaskedTextColor; }
			set{ this._MaskedTextColor = value; }
		}

		
		#endregion

		#region Serialization By Simon's Macro 2009-12-2 9:29:10
		public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			info.AddValue("_TitleWidth",this._TitleWidth);
			info.AddValue("_TitleFont",_TitleFont,typeof(System.Drawing.Font));
			info.AddValue("_TitleTextColor",_TitleTextColor,typeof(System.Drawing.Color));
			info.AddValue("_TitleBackColor",_TitleBackColor,typeof(System.Drawing.Color));
			info.AddValue("_Title",_Title);
			info.AddValue("_TitleAlignment",_TitleAlignment,typeof(DevExpress.Utils.HorzAlignment));
			info.AddValue("_MaskedField",_MaskedField);
			info.AddValue("_MaskedFont",_MaskedFont,typeof(System.Drawing.Font));
			info.AddValue("_MaskedWidth",_MaskedWidth);
            info.AddValue("_DateFormat", _DateFormat);		
			info.AddValue("_ShowTitle",this._ShowTitle);
			info.AddValue("_ShowUnderLine",this._ShowUnderLine);
			info.AddValue("_MaskedTextColor",this._MaskedTextColor,typeof(System.Drawing.Color));
			info.AddValue("_MaskedBackColor",this._MaskedBackColor,typeof(System.Drawing.Color));
			info.AddValue("_MaskedAlignment",this._MaskedAlignment,typeof(DevExpress.Utils.HorzAlignment));
			info.AddValue("_VerticalAlignment",this._VerticalAlignment,typeof(DevExpress.Utils.VertAlignment));
            info.AddValue("_BrotherMaskInfos", this._BrotherMaskInfos, typeof(MaskInfoCollection));        		
		}

		public MaskInfo(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{           
			try
			{
				this._VerticalAlignment=(DevExpress.Utils.VertAlignment)info.GetValue("_VerticalAlignment",typeof(DevExpress.Utils.VertAlignment));
			}
			catch
			{
				_VerticalAlignment =VertAlignment.Bottom;	
			}
			try
			{
				_TitleFont=(System.Drawing.Font)info.GetValue("_TitleFont",typeof(System.Drawing.Font));
			}
			catch
			{
				_TitleFont = new Font(AppearanceObject.DefaultFont.FontFamily, 10f);
					
			}
			try
			{
				_TitleTextColor=(System.Drawing.Color)info.GetValue("_TitleTextColor",typeof(System.Drawing.Color));
			}
			catch
			{
				_TitleTextColor = Color.Black;
				
			}
			try
			{
				_TitleBackColor=(System.Drawing.Color)info.GetValue("_TitleBackColor",typeof(System.Drawing.Color));
			}
			catch
			{
				_TitleBackColor = Color.Transparent;
				
			}
			try
			{
				_Title=info.GetString("_Title");
			}
			catch
			{
				_Title=string.Empty;
			}
            try
            {
                _DateFormat = info.GetString("_DateFormat");
            }
            catch
            {
                _DateFormat = @"M/d/yy";
            }
			try
			{
				_TitleAlignment=(DevExpress.Utils.HorzAlignment)info.GetValue("_TitleAlignment",typeof(DevExpress.Utils.HorzAlignment));
			}
			catch
			{
				
				_TitleAlignment = HorzAlignment.Center;					
				
			}
			try
			{
				_MaskedField=info.GetString("_MaskedField");
			}
			catch
			{
				_MaskedField=string.Empty;
			}
			try
			{
				_MaskedFont=(System.Drawing.Font)info.GetValue("_MaskedFont",typeof(System.Drawing.Font));
			}
			catch
			{
				_MaskedFont = new Font(AppearanceObject.DefaultFont.FontFamily, 10f);
				
			}
			try
			{
				_MaskedWidth=info.GetInt32("_MaskedWidth");
			}
			catch
			{
				_MaskedWidth=55;
			}
			try
			{
				this._MaskedTextColor=(System.Drawing.Color)info.GetValue("_MaskedTextColor",typeof(System.Drawing.Color));
			}
			catch
			{
				_MaskedTextColor = Color.Black;
				
			}
			try
			{
				this._MaskedBackColor=(System.Drawing.Color)info.GetValue("_MaskedBackColor",typeof(System.Drawing.Color));
			}
			catch
			{
				_MaskedBackColor = Color.Transparent;				
			}
			try
			{
				this._MaskedAlignment=(DevExpress.Utils.HorzAlignment)info.GetValue("_MaskedAlignment",typeof(DevExpress.Utils.HorzAlignment));
			}
			catch
			{
				_MaskedAlignment = HorzAlignment.Center;	
			}
			try
			{
				this._TitleWidth=info.GetInt32("_TitleWidth");
			}
			catch
			{
				_TitleWidth=55;
			}
			try
			{
				this._ShowTitle=info.GetBoolean("_ShowTitle");
			}
			catch
			{
				_ShowTitle=true;
			}
			try
			{
				this._ShowUnderLine=info.GetBoolean("_ShowUnderLine");
			}
			catch
			{
				this._ShowUnderLine=true;
			}
            try
            {
                this._BrotherMaskInfos = (MaskInfoCollection)info.GetValue("_BrotherMaskInfos", typeof(MaskInfoCollection));
            }
            catch
            {
                _BrotherMaskInfos = new MaskInfoCollection();
            }           
		}
		#endregion		

		#region Copy Function & ToString()
		
		public MaskInfo Copy()
		{
			MaskInfo thiscopy=new MaskInfo();
			thiscopy._TitleFont=(Font)this._TitleFont.Clone();
			thiscopy._TitleTextColor=this._TitleTextColor;
			thiscopy._TitleBackColor=this._TitleBackColor;
			thiscopy._Title=this._Title;
			thiscopy._TitleWidth=this._TitleWidth;
			thiscopy._TitleAlignment=this._TitleAlignment;
			thiscopy._MaskedField=this._MaskedField;
			thiscopy._MaskedFont=(Font)this._MaskedFont.Clone();
			thiscopy._MaskedWidth=this._MaskedWidth;
			thiscopy._ShowTitle=this._ShowTitle;
			thiscopy._ShowUnderLine=this._ShowUnderLine;

			thiscopy._MaskedTextColor=this._MaskedTextColor;
			thiscopy._MaskedBackColor=this._MaskedBackColor;
			thiscopy._MaskedAlignment=this._MaskedAlignment;

            thiscopy._VerticalAlignment=this._VerticalAlignment;
            thiscopy.BrotherMaskInfos = this.BrotherMaskInfos.Copy();
            thiscopy._DateFormat = this._DateFormat;
      
			return thiscopy;
		}
        public override string ToString()
        {
            StringBuilder sbExpression = new StringBuilder();

            if (this._MaskedField == string.Empty)
            {
                sbExpression.AppendFormat("{0}[____]", this.Title);
            }
            else
            {
                sbExpression.AppendFormat("{0}[{1}]", this.Title, this._MaskedField);
            }
            if (this.BrotherMaskInfos.Count > 0)
            {
                sbExpression.AppendFormat("({0} Brothers)", this.BrotherMaskInfos.Count);
            }
            return sbExpression.ToString();
        }		
	
        #endregion

        #region Function
        public void CalcuateResult(DataRow dr)
        {
            if (dr==null||this._MaskedField == string.Empty || !dr.Table.Columns.Contains(this._MaskedField))
            {
                this._MaskedText = string.Empty;               
            }
            else
            {
                object objValue = CResolveFieldValue.GetResolveValue(this._MaskedField,_DateFormat, dr[this._MaskedField]);

                this._MaskedText = objValue.ToString();
            }
            
            foreach (MaskInfo brotherInfo in this.BrotherMaskInfos)
            {
                brotherInfo.CalcuateResult(dr);
            }
        }
        public int GetColumns()
        {
            int columns = this.ShowTitle ? 2 : 1;

            foreach (MaskInfo maskInfo in this.BrotherMaskInfos)
            {
                columns += maskInfo.GetColumns();
            }

            return columns;
        }

        public void SetCellValueAnsStyle(IWebbTable webbTable, int rowStart, ref int Col)
        {
            IWebbTableCell cell = webbTable.GetCell(rowStart, Col);

            if (this.ShowTitle)
            {
                cell.CellStyle.Sides = DevExpress.XtraPrinting.BorderSide.None;

                cell.CellStyle.ForeColor = this._TitleTextColor;

                cell.CellStyle.BackgroundColor = this._TitleBackColor;

                cell.CellStyle.HorzAlignment = this._TitleAlignment;

                cell.CellStyle.VertAlignment = this._VerticalAlignment;

                cell.CellStyle.Font = this._TitleFont;

                cell.Text = this.Title;

                Col++;

                cell = webbTable.GetCell(rowStart, Col);
            }

            if (this.ShowUnderLine)
            {
                cell.CellStyle.Sides = DevExpress.XtraPrinting.BorderSide.Bottom;

                cell.CellStyle.BorderStyle = DevExpress.XtraPrinting.BrickBorderStyle.Inset;
            }
            else
            {
                cell.CellStyle.Sides = DevExpress.XtraPrinting.BorderSide.None;
            }

            cell.CellStyle.ForeColor = this._MaskedTextColor;

            cell.CellStyle.BackgroundColor = this._MaskedBackColor;

            cell.CellStyle.HorzAlignment = this._MaskedAlignment;

            cell.CellStyle.VertAlignment = this._VerticalAlignment;

            cell.CellStyle.Font = this._MaskedFont;

            cell.Text = this._MaskedText;

            Col++;

            foreach (MaskInfo brotherInfo in this.BrotherMaskInfos)
            {
                brotherInfo.SetCellValueAnsStyle(webbTable, rowStart, ref Col);
            }
        }     
        #endregion  
   
         public void GetALLUsedFields(ref ArrayList usedFields)
         {                
            if(!usedFields.Contains(this.MaskedField))usedFields.Add(this._MaskedField);

            foreach (MaskInfo brotherInfo in this.BrotherMaskInfos)
            {
                brotherInfo.GetALLUsedFields(ref usedFields);
            }               
          }
	}
	#endregion

	#region public class MaskInfoCollection:CollectionBase
	[Serializable]
	public class MaskInfoCollection:CollectionBase
	{
		public MaskInfoCollection()
		{
		}
		#region ColletionBase
		public MaskInfoCollection Copy()
		{
			MaskInfoCollection maskInfos = new MaskInfoCollection();
			
			foreach(MaskInfo maskInfo in this)
			{
				maskInfos.Add(maskInfo.Copy());
			}

			return maskInfos;
		}
	
		public MaskInfo this[int i_index]
		{
			get
			{
				return this.InnerList[i_index] as MaskInfo;
			}
			set
			{
				this.InnerList[i_index] = value;
			}
		}

		public int Add(MaskInfo maskInfo)
		{
			return this.InnerList.Add(maskInfo);
		}
		public void Remove(MaskInfo maskInfo)
		{
			if(this.InnerList.Contains(maskInfo))
			{
				this.InnerList.Remove(maskInfo);
			}
		}
		
		
		#endregion

		#region Function
        public void CalcuateResult(DataRow dr)
        {
            foreach (MaskInfo maskTextInfo in this)
            {
                maskTextInfo.CalcuateResult(dr);
            }
        }

        public void SetCellValueAnsStyle(IWebbTable webbTable)
        {
            for (int i = 0; i < this.InnerList.Count; i++)
            {
                MaskInfo maskTextInfo = this[i];

                int nCol = 0;

                maskTextInfo.SetCellValueAnsStyle(webbTable, i, ref nCol);
            }
        }



        public int GetTotalColumns()
        {
            int maxColumn = 0;

            foreach (MaskInfo maskTextInfo in this)
            {
                int totalColumn = maskTextInfo.GetColumns();

                if (totalColumn > maxColumn) maxColumn = totalColumn;
            }

            return maxColumn;
        }

        public int GetTotalRows()
        {
            return this.InnerList.Count;
        }
	

		#endregion
	}
	#endregion
}
