//08-18-2008@Scott
using System;
using System.Collections;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using System.ComponentModel;
using System.Drawing;

namespace Webb.Reports.ExControls.Data
{
    #region public class ColumnStyle : ISerializable
    [Serializable]
    public class ColumnStyle : ISerializable
    {
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("FieldName", this.fieldName);
            info.AddValue("Style", this.style, typeof(BasicStyle));
            info.AddValue("ColumnWidth", this.columnWidth);
			info.AddValue("title", this.title);
			info.AddValue("titleFormat", this.titleFormat,typeof(StringFormatFlags));
			info.AddValue("colorNeedChange", this.colorNeedChange);
            info.AddValue("description", this.description);
        }

        public ColumnStyle(SerializationInfo info, StreamingContext context)
        {
			try
			{
				this.fieldName = info.GetString("FieldName");
			}
			catch
			{
				fieldName=string.Empty;
			}
			try
			{
				this.style = info.GetValue("Style", typeof(BasicStyle)) as BasicStyle;
			}
			catch
			{
				style = new BasicStyle();
			}            
			try
			{
				this.columnWidth = info.GetInt32("ColumnWidth");
			}
			catch
			{
				columnWidth = BasicStyle.ConstValue.CellWidth;
			}

			try
			{
				 this.title = info.GetString("title");
			}
			catch
			{
				 this.title =fieldName;
			}
			try
			{
				 this.titleFormat =(StringFormatFlags)info.GetValue("titleFormat", typeof(StringFormatFlags));
			}
			catch
			{
                 titleFormat=0;
			}
			try
			{
                colorNeedChange=info.GetBoolean("colorNeedChange");
			}
			catch
			{
				if(this.Style.BackgroundColor!=Color.Transparent)
				{
					colorNeedChange=true;
				}
				else
				{
					colorNeedChange=false;
				}
			}
            try
            {
                description = info.GetString("description");
            }
            catch
            {
                description = string.Empty;
            }
        }

        public ColumnStyle()
        {

        }
		public ColumnStyle(string _FieldName)
		{
			 this.fieldName = _FieldName;
           
             this.title = _FieldName;             
		}

        public ColumnStyle(string _FieldName,string strDefaultHeaderName)
        {
            this.fieldName = _FieldName;

            this.title = strDefaultHeaderName;
        }

        protected string fieldName = string.Empty;
        protected BasicStyle style = new BasicStyle();
        protected int columnWidth = BasicStyle.ConstValue.CellWidth;
		protected bool colorNeedChange=false;
		protected string title=string.Empty;
		protected StringFormatFlags titleFormat;
        protected string description = string.Empty;

        [Browsable(false)]
        public string Description
        {
            get
            {
                if (this.description == null) description = string.Empty;
                return this.description;
            }
            set
            {
                this.description = value;
            }
        }
		[Category("Main")]
        public string FieldName
        {
            get { return this.fieldName; }
//          set { this.fieldName = value; }
        }
        [Category("Heading")]
		public StringFormatFlags TitleFormat
		{
			get { return this.titleFormat; }
			set { this.titleFormat = value; }
		}
		[Category("Heading")]
		public string ColumnHeading
		{
			get { if(title==null)title=string.Empty;
				return this.title; }
			set { this.title = value; }
		}
		[Category("Style")]
		public bool ColorNeedChange
		{
			get { return this.colorNeedChange; }
			set { this.colorNeedChange = value; }
		}	  
        [Category("Style")]
        public BasicStyle Style
        {
            get
            {
                if (this.style == null) this.style = new BasicStyle();

                return this.style;
            }
            set { this.style = value; }
        }
        [Category("Main")]
        public int ColumnWidth
        {
            get { return this.columnWidth; }
            set { this.columnWidth = value; }
        }

        public ColumnStyle Copy()
        {
            ColumnStyle dumyStyle = new ColumnStyle();

            dumyStyle.fieldName = this.fieldName;
            dumyStyle.Style = this.Style.Copy() as BasicStyle;
            dumyStyle.columnWidth = this.columnWidth;
			dumyStyle.title=this.title;
			dumyStyle.titleFormat=this.titleFormat;
			dumyStyle.colorNeedChange=this.colorNeedChange;

            dumyStyle.description = this.description;

            return dumyStyle;
        }

        public bool IsEdit()
        {
            if (this.fieldName != this.title
                  || this.columnWidth != BasicStyle.ConstValue.CellWidth
                   || this.titleFormat != (StringFormatFlags)0 
                     ||this.Style.IsEdited()
                      || colorNeedChange)
            {
                return true;
            }
           return false;

        }

        public override string ToString()
        {
            if (this.Description!= string.Empty)
            {
                return this.Description;
            }

            return this.fieldName;
        }
    }
    #endregion

    #region public class ColumnStyleCollection : CollectionBase
    [Serializable]
    public class ColumnStyleCollection : CollectionBase
    {
        public ColumnStyle this[int index]
        {
            get
            {
                return List[index] as ColumnStyle;
            }
            set
            {
                List[index] = value;
            }
        }

        public ColumnStyle this[string strField]
        {
            get
            {
                foreach (ColumnStyle style in this)
                {
                    if (style.FieldName == strField)
                    {
                        return style;
                    }
                }
                return null;
            }
        }

        public int Add(ColumnStyle value)
        {
            if (this.Contains(value)) return -1;
            return (List.Add(value));
        }

        public int IndexOf(ColumnStyle value)
        {
            return (List.IndexOf(value));
        }

        public void Insert(int index, ColumnStyle value)
        {
            if (this.Contains(value)) return;
            List.Insert(index, value);
        }

        public void Remove(ColumnStyle value)
        {
            List.Remove(value);
        }

        public bool Contains(ColumnStyle value)
        {
            foreach (ColumnStyle style in this)
            {
                if (style.FieldName == value.FieldName)
                {
                    return true;
                }
            }
            return false;
        }

        public void CopyTo(ColumnStyleCollection value)
        {
            if (value == null || object.ReferenceEquals(value, this)) return;

            value.Clear();

            foreach (ColumnStyle colStyle in this)
            {
                value.Add(colStyle);
            }
        }
    }
    #endregion

    #region public class ColumnStyleList
    [Serializable]
    public class ColumnStyleList
    {

        public const string DefaultFileName = "ColumnSetup.dat";

        public ColumnStyleList()
        {

        }

        private ColumnStyleCollection columnStyles = new ColumnStyleCollection();
        private string fileName = string.Empty;

        public ColumnStyleCollection ColumnStyles
        {
            get
            {
                if (this.columnStyles == null) this.columnStyles = new ColumnStyleCollection();

                return this.columnStyles; 
            }
        }

        public string FileName
        {
            get { return this.fileName; }
            set { this.fileName = value; }
        }

        public void Apply(ColumnStyleList columnStyleList)
        {
            this.FileName = columnStyleList.FileName;
            columnStyleList.ColumnStyles.CopyTo(this.ColumnStyles);
        }

        public bool Load(string strFileName)
        {
            if (File.Exists(strFileName))
            {
                try
                {
                    ColumnStyleList columnStyleList = Webb.Utilities.Serializer.Deserialize(strFileName) as ColumnStyleList;

                    this.Apply(columnStyleList);

                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);

                    return false;
                }

                return true;
            }
            else
            {
                return false;
            }
        }


        //public bool BackUpFile(string strFileName)
        //{
        //    if (File.Exists(strFileName))
        //    {
        //        string backUpFile=strFileName.Replace(".dat",".Bkdat");

        //        if(System.IO.File.Exists(backUpFile))
        //        {
        //            System.IO.File.Delete(backUpFile);

        //        }
        //        System.IO.File.Copy(strFileName,backUpFile);

        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        public bool Save(string strFileName)
        {
            try
            {
                Webb.Utilities.Serializer.Serialize(this, strFileName, true);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);

                return false;
            }
            return true;
        }
    }
    #endregion

    public class ColumnManager
    {
        public static ColumnStyleList PublicColumnStyles = new ColumnStyleList();

        public static string FilePath = Webb.Utility.ApplicationDirectory + ColumnStyleList.DefaultFileName;

        public static string DefaultStylePath = Webb.Utility.ApplicationDirectory + "Default.dat";

        private static bool bInit = false;

        public static void Init()
        {
            if (!bInit)
            {
                bInit = PublicColumnStyles.Load(FilePath);
            }
        }
    }
}
