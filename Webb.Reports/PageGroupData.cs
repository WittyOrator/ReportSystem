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

using Webb;
using Webb.Data;
using Webb.Utilities;
using Webb.Collections;
using Webb.Reports;

namespace Webb.Reports
{

	#region Enums
	[Serializable]
	public enum SortTypes
	{		
		Ascending,
		Descending
	}
	[Serializable]
	public enum SortByTypes
	{
		None,
		/// Sorting by the fileds that in the database.
		GroupedVale,
		/// Sort by the group result of count.
		Frequence,
		/// Sort by thg football field.
		///  (-1 to -49, 50 to 1)American  or (-1 to -54 55 to 1 )Canadian
		FootballField,
		/// Sort by number
		Number,

		GroupedValueOrNumber

	}
	#endregion 

	#region public class PageGroupInfo
	/*Descrition:   */
	[Serializable]
	public class PageGroupInfo : ISerializable
	{
		#region Auto Constructor By Macro 2009-3-5 11:06:23
		public PageGroupInfo()
		{
			_SortingType=SortTypes.Descending;
		    _SortingByType=SortByTypes.Frequence;
			_GroupTitle="";
			 _TopCount=0;	
			_Repeat=false;
		    _RepeatTitle="OneValuePerPage";		
		}

		#endregion
		/// Determine the sorting aspect.
		/// 
		protected SortTypes _SortingType=SortTypes.Descending;
		protected SortByTypes _SortingByType=SortByTypes.Frequence;

		/// Column Title
		protected string _GroupTitle=string.Empty;

		/// Determine how many group results to show.
		protected int _TopCount = 0;
		
		protected bool _Repeat=false;

		protected string _RepeatTitle="OneValuePerpage";
	
		protected PageGroupCollection  _SubPageGroupInfos;

		protected PageGroupInfo _ParentPageGroupInfo;

		protected ReportScType _ReportScType;	

		protected DBFilter _Filter;

        protected int _SkippedCount=0;

        protected int _PageValueCount =1;

		#region Property By Macro 2009-3-5 10:47:24
		[Browsable(false)]
		public bool Repeat
		{
			get{return this._Repeat;}
			set{this._Repeat=value;}
		}
		[Browsable(false)]
		public string RepeatTitle
		{
			get{return this._RepeatTitle;}
			set{this._RepeatTitle=value;}
		}
        [Category("Filters")]
		[EditorAttribute(typeof(Webb.Reports.Editors.DBFilterEditor), typeof(System.Drawing.Design.UITypeEditor))]	
		public DBFilter Filter
		{
			get
			{
				if(this._Filter == null) this._Filter = new DBFilter();

				return this._Filter;
			}
			set{this._Filter = value;}
		}
        [Category("Sorting")]
		public Webb.Reports.SortTypes SortingType
		{
			get{ return _SortingType; }
			set{ _SortingType = value; }
		}
        [Category("Sorting")]
		public Webb.Reports.SortByTypes SortingByType
		{
			get{ return _SortingByType; }
			set{ _SortingByType = value; }
		}

		public string GroupTitle
		{
			get{ return _GroupTitle; }
			set{ _GroupTitle = value; }
		}
        [Category("Filters")]
        public int SkippedCount
        {
            //protected object PropertyName;
            get { return this._SkippedCount; }
            set
            {
                this._SkippedCount = value < 0 ? 0 : value;
            }
        }
        [Category("Filters")]
		public int TopCount
		{
			get{ return _TopCount; }
			set{ _TopCount = value; }
		}

        [Browsable(false)]
		public Webb.Reports.PageGroupCollection SubPageGroupInfos
		{
			get{
					if(_SubPageGroupInfos==null)_SubPageGroupInfos=new PageGroupCollection();

					foreach(PageGroupInfo subPageGroup in  _SubPageGroupInfos)
					{
						subPageGroup.ParentPageGroupInfo=this;
					}
					return _SubPageGroupInfos;
			   }

			set{ _SubPageGroupInfos = value; }
		}
        [Browsable(false)]
		public Webb.Reports.PageGroupInfo ParentPageGroupInfo
		{
			get{ return _ParentPageGroupInfo; }
			set{ _ParentPageGroupInfo = value; }
		}
        [Browsable(false)]
		public Webb.Reports.ReportScType ReportScType
		{
			get{ return _ReportScType; }
			set{ _ReportScType = value; }
		}

        [Category("Page GroupSetting")]
        public int ValuesInPage
        {
            get { return this._PageValueCount; }
            set { this._PageValueCount = value; }
        }  

		#endregion

		#region Copy Function By Macro 2009-3-5 10:47:24
		public virtual PageGroupInfo Copy()
		{
			PageGroupInfo thiscopy=new PageGroupInfo();
			thiscopy._SortingType=this._SortingType;
			thiscopy._SortingByType=this._SortingByType;
			thiscopy._GroupTitle=this._GroupTitle;
			thiscopy._TopCount=this._TopCount;
			if(_SubPageGroupInfos!=null)thiscopy._SubPageGroupInfos=this._SubPageGroupInfos.Copy();			
			thiscopy._ReportScType=this._ReportScType;
			thiscopy._Filter=this.Filter.Copy();
			thiscopy._Repeat=this._Repeat;
			thiscopy.RepeatTitle=this.RepeatTitle;
            thiscopy.SkippedCount = this.SkippedCount;
            thiscopy._PageValueCount = this._PageValueCount;
			return thiscopy;
		}
		#endregion			

	
		public override string ToString()
		{
			return _GroupTitle;
		}

        public virtual void GetAllUsedFields(ref ArrayList _UsedFields)
        {
        }

		#region Serialization By Simon's Macro 2009-3-6 9:29:20
		public virtual void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			info.AddValue("_SortingType",_SortingType,typeof(Webb.Reports.SortTypes));
			info.AddValue("_SortingByType",_SortingByType,typeof(Webb.Reports.SortByTypes));
			info.AddValue("_GroupTitle",_GroupTitle);
            info.AddValue("_SkippedCount", _SkippedCount);
			info.AddValue("_TopCount",_TopCount);
			info.AddValue("_Repeat",_Repeat);
			info.AddValue("_RepeatTitle",_RepeatTitle);
			info.AddValue("_SubPageGroupInfos",_SubPageGroupInfos,typeof(Webb.Reports.PageGroupCollection));
			info.AddValue("_ParentPageGroupInfo",_ParentPageGroupInfo,typeof(Webb.Reports.PageGroupInfo));
			info.AddValue("_ReportScType",_ReportScType,typeof(Webb.Reports.ReportScType));
			info.AddValue("_Filter",_Filter,typeof(Webb.Data.DBFilter));
            info.AddValue("_PageValueCount", _PageValueCount);
		
		}

		public PageGroupInfo(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
            try
            {
                _PageValueCount = info.GetInt32("_PageValueCount");
            }
            catch
            {
                _PageValueCount = 1;
            }
			try
			{
				_SortingType=(Webb.Reports.SortTypes)info.GetValue("_SortingType",typeof(Webb.Reports.SortTypes));
			}
			catch
			{
			}
			try
			{
				_SortingByType=(Webb.Reports.SortByTypes)info.GetValue("_SortingByType",typeof(Webb.Reports.SortByTypes));
			}
			catch
			{
			}
			try
			{
				_GroupTitle=info.GetString("_GroupTitle");
			}
			catch
			{
				_GroupTitle=string.Empty;
			}            
            try
			{
                _SkippedCount = info.GetInt32("_SkippedCount");
			}
			catch
			{
                _SkippedCount = 0;
			}
			try
			{
				_TopCount=info.GetInt32("_TopCount");
			}
			catch
			{
				_TopCount=0;
			}
			try
			{
				_Repeat=info.GetBoolean("_Repeat");
			}
			catch
			{
				_Repeat=false;
			}
			try
			{
				_RepeatTitle=info.GetString("_RepeatTitle");
			}
			catch
			{
				_RepeatTitle=string.Empty;
			}
			try
			{
				_SubPageGroupInfos=(Webb.Reports.PageGroupCollection)info.GetValue("_SubPageGroupInfos",typeof(Webb.Reports.PageGroupCollection));
			}
			catch
			{
			}
			try
			{
				_ParentPageGroupInfo=(Webb.Reports.PageGroupInfo)info.GetValue("_ParentPageGroupInfo",typeof(Webb.Reports.PageGroupInfo));
			}
			catch
			{
			}
			try
			{
				_ReportScType=(Webb.Reports.ReportScType)info.GetValue("_ReportScType",typeof(Webb.Reports.ReportScType));
			}
			catch
			{
			}
			try
			{
				_Filter=(Webb.Data.DBFilter)info.GetValue("_Filter",typeof(Webb.Data.DBFilter));

				 this._Filter=AdvFilterConvertor.GetAdvFilter(DataProvider.VideoPlayBackManager.AdvReportFilters,this._Filter);    //2009-4-29 11:37:37@Simon Add UpdateAdvFilter
			}
			catch
			{
                 _Filter=new DBFilter();
			}
		}
		#endregion

	}
	#endregion

	#region public class PageSectionInfo :PageGroupInfo
	/*Descrition:   */
	[Serializable]
	public class PageSectionInfo :PageGroupInfo
	{

		protected SectionFilterCollectionWrapper _SectionFilterWrapper=new SectionFilterCollectionWrapper();
		public PageSectionInfo():base()
		{
			this._SortingByType=SortByTypes.None;
			this._GroupTitle="Sections";
		}
		public PageSectionInfo(SectionFilterCollectionWrapper sectionFilterWrapper):base()
		{
			this._SortingByType=SortByTypes.None;
			this._GroupTitle="Sections";

			_SectionFilterWrapper=sectionFilterWrapper;

		}
		#region Property By Macro 2009-3-5 10:56:03		
		[EditorAttribute(typeof(Webb.Reports.Editors.SectionFiltersEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[Category("SectionGroupSetting")]
		public Webb.Reports.SectionFilterCollectionWrapper SectionFilterWrapper
		{
			get{ 
                 if(_SectionFilterWrapper==null)_SectionFilterWrapper=new SectionFilterCollectionWrapper();
				return _SectionFilterWrapper;
			   }
			set{ _SectionFilterWrapper = value; }
		}
		[Browsable(false)]
		public new Webb.Reports.SortTypes SortingType
		{
			get{ return _SortingType; }
			set{ _SortingType = value; }
		}
        [Browsable(false)]
		public new Webb.Reports.SortByTypes SortingByType
		{
			get{ return _SortingByType; }
			set{ _SortingByType = value; }
		}
        [Browsable(false)]
        public new int SkippedCount
        {
            get { return this._SkippedCount; }
            set { _SkippedCount = value; }
        }

		#endregion

		#region Copy Function By Macro 2009-3-5 10:56:04
		override public PageGroupInfo Copy()
		{
			PageSectionInfo thiscopy=new PageSectionInfo();
			thiscopy._SortingType=this._SortingType;
			thiscopy._SortingByType=this._SortingByType;
			thiscopy._GroupTitle=this._GroupTitle;
			thiscopy._TopCount=this._TopCount;
			if(_SubPageGroupInfos!=null)thiscopy._SubPageGroupInfos=this._SubPageGroupInfos.Copy();			
			thiscopy._ReportScType=this._ReportScType;
			thiscopy._Filter=this.Filter.Copy();
			thiscopy._Repeat=this._Repeat;
			thiscopy.RepeatTitle=this.RepeatTitle;            
			thiscopy._SectionFilterWrapper=this._SectionFilterWrapper;
            thiscopy._SkippedCount = this._SkippedCount;
            thiscopy._PageValueCount = this._PageValueCount;
			return thiscopy;
		}
		#endregion

		#region Serialization By Simon's Macro 2009-3-5 10:56:09
		public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			base.GetObjectData(info,context);

			info.AddValue("_SectionFilterWrapper",_SectionFilterWrapper,typeof(Webb.Reports.SectionFilterCollectionWrapper));
		
		}

		public PageSectionInfo(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context):base(info,context)
		{
			try
			{
				_SectionFilterWrapper=(Webb.Reports.SectionFilterCollectionWrapper)info.GetValue("_SectionFilterWrapper",typeof(Webb.Reports.SectionFilterCollectionWrapper));
			}
			catch
			{
			}
		}
		#endregion		

        public override void GetAllUsedFields(ref ArrayList _UsedFields)
        {
            this.SectionFilterWrapper.GetAllUsedFields(ref _UsedFields);

            this.Filter.GetAllUsedFields(ref _UsedFields);

            foreach (PageGroupInfo subGroupInfo in this.SubPageGroupInfos)
            {
                subGroupInfo.GetAllUsedFields(ref _UsedFields);
            }
        }

		public override string ToString()
		{
			return this.SectionFilterWrapper.ToString();
		}	
	}
	#endregion

	#region public class PageFieldInfo :PageGroupInfo
	/*Descrition:   */
	[Serializable]
	public class PageFieldInfo :PageGroupInfo
	{
		protected string _Field=string.Empty;
        protected string _TotalName = "All";
        protected bool _TotalOther = false;
        protected string _OtherName = "Other";
        protected TotalType _TotalType = TotalType.None;
 
		public PageFieldInfo():base()
		{
			_Field=string.Empty;
			this.GroupTitle="Group";
		}
         public PageFieldInfo(string field):base()
		{
			_Field=field;
			 this.GroupTitle="Group";
		}

		#region Property By Macro 2009-3-5 10:56:03		
        //[TypeConverter(typeof(PublicDBFieldConverter))]
        [Editor(typeof(Webb.Reports.Editors.FieldSelectEditor), typeof(System.Drawing.Design.UITypeEditor))]
	    [Category("FieldGroupSetting")]
		public string GroupByField
		{
			get{
				if(_Field==null)_Field=string.Empty;
				return _Field; }
			set{ _Field = value; }
		}
        [Category("FieldGroupSetting")]
        public string TotalAllName
        {
            get
            {
                return _TotalName;
            }
            set { _TotalName = value; }
        }


        [Category("FieldGroupSetting")]
        public string TotalOthersName
        {
            get
            {
                if (_OtherName == null) _OtherName = string.Empty;
                return _OtherName;
            }
            set { _OtherName = value; }
        }
        [Category("FieldGroupSetting")]
        public TotalType TotalAll
        {
            get
            {
                return _TotalType;
            }
            set { _TotalType = value; }
        }
        [Category("FieldGroupSetting")]
        public bool TotalOther
        {
            get
            {

                return _TotalOther;
            }
            set { _TotalOther = value; }
        }

		#endregion

		#region Copy Function By Macro 2009-3-5 10:56:04
		override public PageGroupInfo Copy()
		{
			PageFieldInfo thiscopy=new PageFieldInfo();
			thiscopy._SortingType=this._SortingType;
			thiscopy._SortingByType=this._SortingByType;
			thiscopy._GroupTitle=this._GroupTitle;
			thiscopy._TopCount=this._TopCount;
			if(_SubPageGroupInfos!=null)thiscopy._SubPageGroupInfos=this._SubPageGroupInfos.Copy();			
			thiscopy._ReportScType=this._ReportScType;
			thiscopy._Filter=this.Filter.Copy();
			thiscopy._Repeat=this._Repeat;
			thiscopy.RepeatTitle=this.RepeatTitle;            
			thiscopy._Field=this._Field;
            thiscopy._OtherName = this._OtherName;
            thiscopy._TotalType = this._TotalType;
            thiscopy._TotalOther = this._TotalOther;
            thiscopy._TotalName = this._TotalName;
            thiscopy._SkippedCount = this._SkippedCount;
            thiscopy._PageValueCount = this._PageValueCount;
			return thiscopy;
		}
		#endregion

		#region Serialization By Simon's Macro 2009-3-5 10:56:09
		override public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			base.GetObjectData(info,context);

			info.AddValue("_Field",_Field,typeof(string));
            info.AddValue("_OtherName", this._OtherName, typeof(string));
            info.AddValue("_TotalOther", this._TotalOther);
            info.AddValue("_TotalType", this._TotalType,typeof(TotalType));
            info.AddValue("_TotalName", this._TotalName, typeof(string));
		
		}

		public PageFieldInfo(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context):base(info,context)
		{
			try
			{
				_Field=(string)info.GetValue("_Field",typeof(string));
			}
			catch
			{
				_Field=string.Empty;
			}
            try
            {
                _OtherName = (string)info.GetValue("_OtherName", typeof(string));
            }
            catch
            {
                _OtherName = "Other";
            }
            try
            {
                _TotalOther = info.GetBoolean("_TotalOther");
            }
            catch
            {
                _TotalOther = false;
            }
            try
            {
                _TotalType = (TotalType)info.GetValue("_TotalType", typeof(TotalType));
            }
            catch
            {
                _TotalType = TotalType.None;
            }
            try
            {
                this._TotalName = (string)info.GetValue("_TotalName", typeof(string));
            }
            catch
            {
                _TotalName ="All";
            }
		}
		#endregion		

        public override void GetAllUsedFields(ref ArrayList _UsedFields)
        {
            if (!_UsedFields.Contains(this.GroupByField))
            {
                _UsedFields.Add(this.GroupByField);
            }
            this.Filter.GetAllUsedFields(ref _UsedFields);         

            foreach (PageGroupInfo subGroupInfo in this.SubPageGroupInfos)
            {
                subGroupInfo.GetAllUsedFields(ref _UsedFields);
            }
        }

		public override string ToString()
		{	
			return string.Format("{0} (Group by:{1})",this._GroupTitle,this._Field);
		}	

	}
	#endregion

	#region public class PageGroupCollection
	[Serializable]
	public class PageGroupCollection : CollectionBase
	{
		public PageGroupCollection()
		{
			
		}
	
		public PageGroupCollection Copy()
		{
			PageGroupCollection groupInfos = new PageGroupCollection();
			
			foreach(PageGroupInfo groupInfo in this)
			{
				groupInfos.Add(groupInfo.Copy());
			}

			return groupInfos;
		}
	
		public PageGroupInfo this[int i_index]
		{
			get
			{
				return this.InnerList[i_index] as PageGroupInfo;
			}
			set
			{
				this.InnerList[i_index] = value;
			}
		}

		public int Add(PageGroupInfo groupInfo)
		{
			return this.InnerList.Add(groupInfo);
		}
	}
	#endregion

   

}
