/***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:Class.cs
 * Author:Country.Wu [EMail:Webb.Country.Wu@163.com]
 * Create Time:10/31/2007 02:16:07 PM
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
using System.Security.Permissions;
using System.ComponentModel;

using Webb.Data;
using Webb.Reports.DataProvider;

namespace Webb.Reports
{
	#region public enum WebbProducts & public enum WebbBallTypes
	//Wu.Country@2007-12-04 15:58 added this region.
	[Serializable]
	public enum WebbProducts
	{
		Advantage,
		Victory,
		GameDay
	}

	[Serializable]
	public enum WebbBallTypes
	{
		Football,
		Vollayball,
		Basketball
	}
	[Serializable]
	public enum FieldsGroupStyle
	{
		CombinedValues,
		TreeView
	}

	[Serializable]
	public enum ReportAccessType
	{
		All=0,
		ReadOnly,
		ModifyOnly,
		AsTemplateFile,
		None,
	}
	
	#endregion

	#region public class WebbDataSource
	//Wu.Country@2007-11-07 14:34 added this region.
	[Serializable]
	public class WebbDataSource
	{
		public WebbDataSource()
		{
			// TODO: implement
			//Scott@2007-11-30 14:55 modified some of the following code.
			this.DataSource = new DataSet();
			this.DataMember = string.Empty;
			this.Games = new GameInfoCollection();
			this.Filters = new FilterInfoCollection();
			this.SectionFilters = new SectionFilterCollection();
            this.EdlInfos = new EdlInfoCollection();
		}
   
		public DataSet DataSource;
		public string DataMember;
      
		public GameInfoCollection Games;
		public FilterInfoCollection Filters;
		public SectionFilterCollection SectionFilters;
		public IDataAdapter DataAdapter;
        public EdlInfoCollection EdlInfos;  //Add this code at 2010-12-28 14:56:27@simon

        // 11-15-2011 Scott
        public void FixCMDefaultHeaders()
        {
            if (DataSource != null && DataSource.Tables.Count > 1)
            {
                DataTable dt = DataSource.Tables[1];

                if (!dt.Columns.Contains("DefaultHeader"))
                {
                    return;
                }

                foreach (DataRow dr in dt.Rows)
                {
                    switch (dr["DefaultHeader"].ToString())
                    {
                            // Coach
                        case "Assistant Business Phone":
                            dr["DefaultHeader"] = "Asst Bus Ph";
                            break;
                        case "Assistant Displayed Name":
                            dr["DefaultHeader"] = "Asst Displayed Name";
                            break;
                        case "Assistant Email":
                            dr["DefaultHeader"] = "Asst Email";
                            break;
                        case "Assistant First Name":
                            dr["DefaultHeader"] = "Asst FN";
                            break;
                        case "Assistant Last Name":
                            dr["DefaultHeader"] = "Asst LN";
                            break;
                        case "Assistant Home Phone":
                            dr["DefaultHeader"] = "Asst Hm Ph";
                            break;
                        case "Assistant Mobile Number":
                            dr["DefaultHeader"] = "Asst Cell";
                            break;
                        case "Assistant Title":
                            dr["DefaultHeader"] = "Asst Title";
                            break;
                        case "Business Address":
                            dr["DefaultHeader"] = "Bus Address";
                            break;
                        case "Business City":
                            dr["DefaultHeader"] = "City";
                            break;
                        case "Business Country":
                            dr["DefaultHeader"] = "Country";
                            break;
                        case "Business County":
                            dr["DefaultHeader"] = "County";
                            break;
                        case "Business Fax":
                            dr["DefaultHeader"] = "Bus Fax";
                            break;
                        case "Business Ph":
                            dr["DefaultHeader"] = "Bus Ph";
                            break;
                        case "Business Ph Ext":
                            dr["DefaultHeader"] = "Bus Ph Ext";
                            break;
                        case "Business PO Box":
                            dr["DefaultHeader"] = "Bus PO Box";
                            break;
                        case "Business State":
                            dr["DefaultHeader"] = "St";
                            break;
                        case "Business Zip Code":
                            dr["DefaultHeader"] = "Zip";
                            break;
                        case "Current Status":
                            dr["DefaultHeader"] = "Current Status";
                            break;
                        case "Department Fax":
                            dr["DefaultHeader"] = "Dept Fax";
                            break;
                        case "First Name":
                            dr["DefaultHeader"] = "First";
                            break;
                        case "Last Name":
                            dr["DefaultHeader"] = "Last";
                            break;
                        case "Home Address":
                            dr["DefaultHeader"] = "Home Address";
                            break;
                        case "Home City":
                            dr["DefaultHeader"] = "City";
                            break;
                        case "Home Country":
                            dr["DefaultHeader"] = "Country";
                            break;
                        case "Home County":
                            dr["DefaultHeader"] = "County";
                            break;
                        case "Home Email":
                            dr["DefaultHeader"] = "Hm Email";
                            break;
                        case "Home Fax":
                            dr["DefaultHeader"] = "Hm Fax";
                            break;
                        case "Home Ph":
                            dr["DefaultHeader"] = "Hm Ph";
                            break;
                        case "Home PO Box":
                            dr["DefaultHeader"] = "Hm PO Box";
                            break;
                        case "Home State":
                            dr["DefaultHeader"] = "St";
                            break;
                        case "Home Zip Code":
                            dr["DefaultHeader"] = "Zip";
                            break;
                        case "Login Name":
                            dr["DefaultHeader"] = "Login Name";
                            break;
                        case "Messenger ID":
                            dr["DefaultHeader"] = "Messenger ID";
                            break;
                        case "Middle Name":
                            dr["DefaultHeader"] = "M Name";
                            break;
                        case "Mobile Ph":
                            dr["DefaultHeader"] = "Cell";
                            break;
                        case "Mobile Ph2":
                            dr["DefaultHeader"] = "Cell 2";
                            break;
                        case "Nick Name":
                            dr["DefaultHeader"] = "Nick Name";
                            break;
                        case "Personal Webpage":
                            dr["DefaultHeader"] = "Personal Webpage";
                            break;
                        case "Preferred Method Of Contact":
                            dr["DefaultHeader"] = "Pref Method Of Contact";
                            break;
                        case "Shipping City":
                            dr["DefaultHeader"] = "City";
                            break;
                        case "Shipping Country":
                            dr["DefaultHeader"] = "Country";
                            break;
                        case "Shipping County":
                            dr["DefaultHeader"] = "County";
                            break;
                        case "Shipping PO Box":
                            dr["DefaultHeader"] = "Shipping PO Box";
                            break;
                        case "Shipping State":
                            dr["DefaultHeader"] = "St";
                            break;
                        case "Shipping Zip Code":
                            dr["DefaultHeader"] = "Zip";
                            break;
                            // Player
                        case "Away Jersey Number":
                            dr["DefaultHeader"] = "Away Jer #";
                            break;
                        case "Dads Cell Phone":
                            dr["DefaultHeader"] = "Dads Cell Ph";
                            break;
                        case "Dads First Name":
                            dr["DefaultHeader"] = "Dads FN";
                            break;
                        case "Dads Last Name":
                            dr["DefaultHeader"] = "Dads LN";
                            break;
                        case "Dads WirelessCarrier":
                            dr["DefaultHeader"] = "Dads Wireless Car";
                            break;
                        case "Dads WK PH":
                            dr["DefaultHeader"] = "Dads WK Ph";
                            break;
                        case "First Name Initial":
                            dr["DefaultHeader"] = "FN";
                            break;
                        case "Guardians Cell Phone":
                            dr["DefaultHeader"] = "G Cell";
                            break;
                        case "Guardians First Name":
                            dr["DefaultHeader"] = "G First Name";
                            break;
                        case "Guardians Last Name":
                            dr["DefaultHeader"] = "G Last Name";
                            break;
                        case "Guardians WirelessCarrier":
                            dr["DefaultHeader"] = "G Wireless Carrier";
                            break;
                        case "Guardians WK PH":
                            dr["DefaultHeader"] = "G WK Ph";
                            break;
                        case "Home Jersey Number":
                            dr["DefaultHeader"] = "HM Jer #";
                            break;
                        case "Home Phone":
                            dr["DefaultHeader"] = "Hm Ph";
                            break;
                        case "Locker Number":
                            dr["DefaultHeader"] = "Locker #";
                            break;
                        //case "Middle Name":
                        //    dr["DefaultHeader"] = "M Name";
                        //    break;
                        case "Moms First Name":
                            dr["DefaultHeader"] = "Mom FN";
                            break;
                        case "Moms Last Name":
                            dr["DefaultHeader"] = "Mom LN";
                            break;
                        case "Moms WK PH":
                            dr["DefaultHeader"] = "Moms WK Ph";
                            break;
                        case "Preferred Contact":
                            dr["DefaultHeader"] = "Pref Contact";
                            break;
                        case "Years Starter":
                            dr["DefaultHeader"] = "Yrs Starter";
                            break;
                            // Player login
                        case "duration(@Time)":
                            dr["DefaultHeader"] = "Duration";
                            break;
                        case "loginTime":
                            dr["DefaultHeader"] = "Login Time";
                            break;
                        case "logoutTime":
                            dr["DefaultHeader"] = "Logout Time";
                            break;
                        case "viewGameName":
                            dr["DefaultHeader"] = "View Video Name";
                            break;
                        case "viewGameType":
                            dr["DefaultHeader"] = "View Video Type";
                            break;
                        case "viewOpenTime":
                            dr["DefaultHeader"] = "View Open Time";
                            break;
                            // Player Files
                        case "playerFileName":
                            dr["DefaultHeader"] = "Player File Name";
                            break;
                        case "playerFilePath":
                            dr["DefaultHeader"] = "Player File Path";
                            break;
                            // Shared Cutups
                        case "playerSharedExpirationTime":
                            dr["DefaultHeader"] = "Expiration";
                            break;
                        case "playerSharedGameName":
                            dr["DefaultHeader"] = "Shared Video Name";
                            break;
                        case "playerSharedGamePath":
                            dr["DefaultHeader"] = "Video Path";
                            break;
                        case "playerSharedGameType":
                            dr["DefaultHeader"] = "Video Type";
                            break;
                            // Sponsor
                        case "Birth Date(Sponsor)":
                            dr["DefaultHeader"] = "Birthday";
                            break;
                        case "Donation Amount(Sponsor)":
                            dr["DefaultHeader"] = "Donation $";
                            break;
                        case "SponsorInfoID(Sponsor)":
                            dr["DefaultHeader"] = "Sponsor Info ID";
                            break;
                        case "State(Sponsor)":
                            dr["DefaultHeader"] = "St";
                            break;
                        case "Year of Grad(Sponsor)":
                            dr["DefaultHeader"] = "Yr of Grad";
                            break;
                        case "Relationship(Sponsor)":
                            dr["DefaultHeader"] = "Relation";
                            break;
                        case "First Name(Sponsor)":
                            dr["DefaultHeader"] = "First Name";
                            break;
                        case "Last Name(Sponsor)":
                            dr["DefaultHeader"] = "Last Name";
                            break;
                            // Statistics
                        case "Assisted Tackles":
                            dr["DefaultHeader"] = "Ast Tackles";
                            break;
                        case "Completions":
                            dr["DefaultHeader"] = "Comp";
                            break;
                        case "Interceptions":
                            dr["DefaultHeader"] = "Inter";
                            break;
                        case "Interceptions Thrown":
                            dr["DefaultHeader"] = "Inter Thrown";
                            break;
                        case "Pass Yards per Attempt":
                            dr["DefaultHeader"] = "Pass Yrds per Att";
                            break;
                        case "Passing Attempts":
                            dr["DefaultHeader"] = "Pass Att";
                            break;
                        case "Punting AVG":
                            dr["DefaultHeader"] = "Punt Avg";
                            break;
                        case "Receiving Yards":
                            dr["DefaultHeader"] = "Rec Yds";
                            break;
                        case "Rushing Attempts":
                            dr["DefaultHeader"] = "Rush Att";
                            break;
                        case "Rushing AVG":
                            dr["DefaultHeader"] = "Rush Avg";
                            break;
                        case "Rushing TD":
                            dr["DefaultHeader"] = "Rush TD";
                            break;
                        case "Rushing Yards":
                            dr["DefaultHeader"] = "Rush Yds";
                            break;
                        case "Total Tackles":
                            dr["DefaultHeader"] = "Tot Tackles";
                            break;
                        case "Yards per Reception":
                            dr["DefaultHeader"] = "Yds per Rec";
                            break;
                            // Team
                        case "Preferred Phone":
                            dr["DefaultHeader"] = "Preferred Ph";
                            break;
                        //case "State":
                        //    dr["DefaultHeader"] = "St";
                        //    break;
                        case "Zip Code":
                            dr["DefaultHeader"] = "Zip";
                            break;
                    }

                    if (dr["DefaultHeader"].ToString().Contains("(Sponsor)"))
                    {
                        dr["DefaultHeader"] = dr["DefaultHeader"].ToString().Replace("(Sponsor)", string.Empty);
                    }
                }
            }
        }
	}
	#endregion

	#region public class SectionFilter
	[Serializable]
	public class SectionFilter : ISerializable
	{
		virtual public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("FilterName",this._FilterName);

			info.AddValue("DBFilter",this._DBFilter,typeof(DBFilter));
		}

		public SectionFilter(SerializationInfo info, StreamingContext context)
		{
			try
			{
				this._FilterName = info.GetString("FilterName");
			}
			catch
			{
				this._FilterName = string.Empty;
			}

			try
			{
				this._DBFilter = info.GetValue("DBFilter",typeof(DBFilter)) as DBFilter;
			}
			catch
			{
				this._DBFilter = new DBFilter();
			}
		}

		//Wu.Country@2007-11-14 10:32 AM added this class.
		//Fields
		protected string _FilterName;
		protected DBFilter _DBFilter;

		//Properties
		public string FilterName
		{
			get
			{
                if (this._FilterName == null) _FilterName = string.Empty;
                //if(this._FilterName == string.Empty) return "[NoName]";

				return this._FilterName;
			}
			set{this._FilterName = value;}
		}

		[EditorAttribute(typeof(Editors.DBFilterEditor), typeof(System.Drawing.Design.UITypeEditor))]	//08-11-2008@Scott
		public DBFilter Filter
		{
			get
			{
				if(this._DBFilter == null) this._DBFilter = new DBFilter();

				return this._DBFilter;
			}
			set
			{
				this._DBFilter = value;
			}
		}
		//ctor
		public SectionFilter(DBFilter i_DBFilter)
		{
			this._FilterName = string.Empty;

			this._DBFilter = i_DBFilter.Copy();
		}
        // 08-22-2011 Scott
        public SectionFilter(string strName, DBFilter i_DBFilter)
        {
            this._FilterName = strName;

            this._DBFilter = i_DBFilter.Copy();
        }
		public SectionFilter()
		{
			this._FilterName = string.Empty;
		}
		public void Apply(SectionFilter i_SecFilter)
		{
			this._FilterName = i_SecFilter.FilterName;

			this._DBFilter = i_SecFilter.Filter.Copy();
		}
		//Methods
		public bool Save(string strFileName)
		{
			try
			{
				Webb.Utilities.Serializer.Serialize(this,strFileName,true);
			}
			catch
			{
				return false;
			}
			return true;
		}

		public bool Load(string strFileName)
		{
			try
			{
				SectionFilter filter = Webb.Utilities.Serializer.Deserialize(strFileName) as SectionFilter;
				
				this.Apply(filter);
			}
			catch(Exception ex)
			{
                System.Diagnostics.Debug.WriteLine(ex.Message);

				return false;
			}
			return true;
		}

		public override string ToString()
		{
			return this.FilterName;
		}
	}
	#endregion

	//Modified at 2009-1-14 14:20:29@Scott
	#region public class SectionFilterCollectionWrapper	
	[Serializable]
	public class SectionFilterCollectionWrapper	: ISerializable
	{
		virtual public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("_SectionFilters",this._SectionFilters,typeof(SectionFilterCollection));
			info.AddValue("_ReportScType",this._ReportScType,typeof(ReportScType));
		}

		public SectionFilterCollectionWrapper(SerializationInfo info, StreamingContext context)
		{
			try
			{
				this._ReportScType = (ReportScType)info.GetValue("_ReportScType",typeof(ReportScType));
			}
			catch
			{
				this._ReportScType = ReportScType.Custom;
			}
			try
			{
				this._SectionFilters = info.GetValue("_SectionFilters",typeof(SectionFilterCollection)) as SectionFilterCollection;				
			}
			catch
			{
				this._SectionFilters = new SectionFilterCollection();
			}
		}

		public SectionFilterCollectionWrapper(SectionFilterCollection scfilters,ReportScType scType)
		{
			this.SectionFilters = scfilters;
			this.ReportScType = scType;
		}

		public SectionFilterCollectionWrapper()
		{
			this.SectionFilters = new SectionFilterCollection();
			this.ReportScType = ReportScType.Custom;
		}

		protected ReportScType _ReportScType;
		protected SectionFilterCollection _SectionFilters;

		public ReportScType ReportScType
		{
			get{return this._ReportScType;}
			set{this._ReportScType = value;}
		}

		public SectionFilterCollection SectionFilters
		{
			get
			{
				if(this._SectionFilters == null) this._SectionFilters = new SectionFilterCollection();

				return this._SectionFilters;
			}
			set{this._SectionFilters = value;}
		}

        public void GetAllUsedFields(ref ArrayList _UsedFields)
        {
            if (this.SectionFilters == null || this.SectionFilters.Count == 0) return;

            foreach (SectionFilter scFilter in this.SectionFilters)
            {
                if (scFilter == null || scFilter.Filter == null) continue;

                scFilter.Filter.GetAllUsedFields(ref _UsedFields);
            }
        }

		public bool UpdateSectionFilters()
		{
			AdvFilterConvertor convertor = new AdvFilterConvertor();
			//Modified at 2009-1-19 14:09:52@Scott
			if(this.ReportScType == ReportScType.Custom)
			{
				this._SectionFilters=AdvFilterConvertor.GetCustomFilters(DataProvider.VideoPlayBackManager.AdvReportFilters,this._SectionFilters);
              
			}
			else
			{
				this.SectionFilters = convertor.GetReportFilters(Webb.Reports.DataProvider.VideoPlayBackManager.AdvReportFilters,this.ReportScType);	//add 1-19-2008 scott
			}
			return true;
		}
		
		public override string ToString()
		{
			return  string.Format("Sections({0})",this.ReportScType);
		}

	}
	#endregion

	#region public class SectionFilterCollection
	/*Descrition:   */
	[Serializable]
	public class SectionFilterCollection : CollectionBase//,ISerializable	//Add ISerializable interface,Modified at 2009-1-13 16:01:10@Scott
	{
		#region Modified Area
		virtual public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("CollectionBase+list",this.InnerList);
		}

		public SectionFilterCollection(SerializationInfo info, StreamingContext context)
		{
			try
			{
				ArrayList arr = info.GetValue("CollectionBase+list",typeof(ArrayList)) as ArrayList;
				
				if(arr != null)
				{
					this.Clear();

					foreach(object o in arr)
					{
						this.Add(o as SectionFilter);	
					}
				}
			}
			catch
			{
				this.Clear();
			}
		}
		#endregion        //Modify at 2009-1-13 16:00:29@Scott

		//Wu.Country@2007-11-14 10:32:24 AM added this collection.
		//Fields
		//Properties
		public SectionFilter this[int i_Index]
		{ 
			get { return this.InnerList[i_Index] as SectionFilter; } 
			set { this.InnerList[i_Index] = value; } 
		} 
		//ctor
		public SectionFilterCollection() {} 
		//Methods
		public int Add(SectionFilter i_Object)
		{ 
			return this.InnerList.Add(i_Object);
		} 
		public void Remove(SectionFilter i_Obj)
		{
			this.InnerList.Remove(i_Obj);
		} 
		public SectionFilterCollection Copy()
		{
			SectionFilterCollection secFilterCollection = new SectionFilterCollection();

			foreach(SectionFilter filter in this)
			{
				if(filter != null)
				{
					SectionFilter secFilter = new SectionFilter();

					secFilter.Apply(filter);

					secFilterCollection.Add(secFilter);
				}
			}

			return secFilterCollection;
		}
		public void Apply(SectionFilterCollection secFilterCollection)
		{
			this.Clear();

			foreach(SectionFilter filter in secFilterCollection)
			{
				SectionFilter secFilter = new SectionFilter();

				secFilter.Apply(filter);

				this.Add(secFilter);
			}
		}

	} 
	#endregion

	#region public interface IExControlDev : DevExpress.XtraReports.IExControl
	//Wu.Country@2007-12-04 15:57 added this region.
	/// <summary>
	/// This is flag interface.
	/// </summary>
	public interface IExControlDev : DevExpress.XtraReports.IExControl
	{
	}

	public interface INonePrintControl
	{
	}
	#endregion

	#region public class WebbReportTemplate : ISerializable
	[Serializable]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class WebbReportTemplate : ISerializable
	{
		//ctor
		public WebbReportTemplate()
		{
			this._RepeatedWidth = 240f;
			this._RepeatedHeight = 240f;
			this._RepeatedCount = 3;
			this._RepeatedVerticalCount = 3;
			this._Filter = new DBFilter();
			this._DiagramScoutType = DiagramScoutType.Offense;
			this._GroupByFields = new StringCollection();
			this._GroupBySectionFilters=new SectionFilterCollection();

			this._RepeatFields=new StringCollection();   //Added this code at 2008-12-25 14:47:36@Simon
			this._RepeatSectionFilters=new SectionFilterCollection();  //Added this code at 2008-12-25 14:49:37@Simon

			_FieldsGroupStyle=FieldsGroupStyle.TreeView;

			_UserLevel=new UserLevel("Level5",31);

			_AccessType=ReportAccessType.All;
			
		}
        
		private void UpdateSectionInfo(PageGroupInfo groupInfo)
		{
			PageSectionInfo newpageSections=groupInfo as PageSectionInfo;

			if(newpageSections!=null)
			{
                 SectionFilterCollectionWrapper scFilterWrapper=new SectionFilterCollectionWrapper();

				 scFilterWrapper.ReportScType= newpageSections.SectionFilterWrapper.ReportScType;

				foreach(SectionFilter scFilter in   newpageSections.SectionFilterWrapper.SectionFilters)
				{					
					if(scFilter==null)
					{
						scFilterWrapper.SectionFilters.Add(scFilter);
					}
					else
					{
						SectionFilter newScFilter=new SectionFilter();

                        newScFilter.Apply(scFilter);

                        scFilterWrapper.SectionFilters.Add(newScFilter);
					}
				}

				(groupInfo as PageSectionInfo).SectionFilterWrapper=scFilterWrapper;
			}
		}

		#region ISerializable Members
		[SecurityPermission(SecurityAction.Demand,SerializationFormatter=true)]
		virtual public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("GroupByField",this._GroupByField);
			
			info.AddValue("SectionFilters",this._SectionFilters,typeof(SectionFilterCollection));

			info.AddValue("ReportScType",this._ReportScType,typeof(ReportScType));

			info.AddValue("OneValuePerPage",this._OneValuePerPage);

			info.AddValue("TopCount",this._TopCount);

			info.AddValue("OnePageReport",this._OnePageReport);

			info.AddValue("RepeatedReport",this._RepeatedReport);

			info.AddValue("RepeatedWidth",this._RepeatedWidth);

			info.AddValue("RepeatedHeight",this._RepeatedHeight);

			info.AddValue("RepeatedCount",this._RepeatedCount);
		
			info.AddValue("RepeatedVerticalCount",this._RepeatedVerticalCount);

			info.AddValue("Filter",this._Filter,typeof(DBFilter));

			info.AddValue("DiagramScoutType",this._DiagramScoutType,typeof(DiagramScoutType));

			info.AddValue("_GroupByFields",this._GroupByFields,typeof(IList));

			info.AddValue("_GroupBySectionFilters",this._GroupBySectionFilters,typeof(SectionFilterCollection));

		    info.AddValue("_RepeatFields",this._RepeatFields,typeof(IList));

			info.AddValue("_RepeatSectionFilters",this._RepeatSectionFilters,typeof(SectionFilterCollection));
		
			info.AddValue("_RepeatTopCount",this._RepeatTopCount);

			info.AddValue("ClickEvent",this.ClickEvent);  //Added this code at 2009-1-12 10:02:03@Simon

			info.AddValue("SectionFiltersWrapper",this.SectionFiltersWrapper,typeof(SectionFilterCollectionWrapper));				//Modified at 2009-1-14 17:20:44@Scott
		
			info.AddValue("GroupBySectionFiltersWrapper",this.GroupBySectionFiltersWrapper,typeof(SectionFilterCollectionWrapper));	//Modified at 2009-1-14 17:22:11@Scott

			info.AddValue("RepeatSectionFiltersWrapper",this.RepeatSectionFiltersWrapper,typeof(SectionFilterCollectionWrapper));	//Modified at 2009-1-14 17:22:14@Scott
		
			info.AddValue("Consecutive",this.Consecutive);	//Modified at 2009-2-2 14:00:32@Scott

            info.AddValue("AutoLayOut",this.AutoLayOut);	//Modified at 2009-2-2 14:00:32@Scott

			info.AddValue("_FieldsGroupStyle",this._FieldsGroupStyle);

			info.AddValue("_OneValueGroupInfo",this._OneValueGroupInfo,typeof(PageGroupInfo));

             info.AddValue("_RepeatGroupInfo",this._RepeatGroupInfo,typeof(PageGroupInfo));

			 info.AddValue("_UserLevel",this._UserLevel,typeof(UserLevel));  //2009-6-15 11:06:51@Simon Add this Code

			 info.AddValue("_AccessType",this._AccessType,typeof(ReportAccessType));  //2009-6-15 11:06:51@Simon Add this Code

			 info.AddValue("_ReportWizardSetting",this._ReportWizardSetting,typeof(ReportWizardSetting));  //2009-6-15 11:06:51@Simon Add this Code


				
		}
		
		public WebbReportTemplate(SerializationInfo info, StreamingContext context)
		{
			try
			{
				this._GroupByField = info.GetString("GroupByField");
			}
			catch
			{
			}

			try
			{
				this._SectionFilters = info.GetValue("SectionFilters",typeof(SectionFilterCollection)) as SectionFilterCollection;				
			}
			catch
			{
				this._SectionFilters=new SectionFilterCollection();
			}

			try
			{
				this._ReportScType = (ReportScType)info.GetValue("ReportScType",typeof(ReportScType));
			}
			catch
			{
				this._ReportScType = ReportScType.Custom;
			}

			try
			{
				this._OneValuePerPage = info.GetBoolean("OneValuePerPage");
			}
			catch
			{
				this._OneValuePerPage = false;
			}

			try
			{
				this._TopCount = info.GetInt32("TopCount");
			}
			catch
			{
				this._TopCount = 0;
			}

			try
			{
				this._OnePageReport = info.GetBoolean("OnePageReport");
			}
			catch
			{
				this._OnePageReport = false;
			}

			try
			{
				this._RepeatedReport = info.GetBoolean("RepeatedReport");
			}
			catch
			{
				this._RepeatedReport = false;
			}

			try
			{
				this._RepeatedWidth = info.GetSingle("RepeatedWidth");
			}
			catch
			{
				this._RepeatedWidth = 240f;
			}

			try
			{
				this._RepeatedHeight = info.GetSingle("RepeatedHeight");
			}
			catch
			{
				this._RepeatedHeight = 240f;
			}

			try
			{
				this._RepeatedCount = info.GetInt32("RepeatedCount");
			}
			catch
			{
				this._RepeatedCount = 3;
			}

			try
			{
				this._RepeatedVerticalCount = info.GetInt32("RepeatedVerticalCount");
			}
			catch
			{
				this._RepeatedVerticalCount = 3;
			}

			try
			{
				this._Filter = info.GetValue("Filter",typeof(DBFilter)) as DBFilter;

                this._Filter=AdvFilterConvertor.GetAdvFilter(DataProvider.VideoPlayBackManager.AdvReportFilters,this._Filter);    //2009-4-29 11:37:37@Simon Add UpdateAdvFilter
				
			}
			catch
			{
				this._Filter = new DBFilter();
			}

			try
			{
				this._DiagramScoutType = (DiagramScoutType)info.GetValue("DiagramScoutType",typeof(DiagramScoutType));
			}
			catch
			{
				this._DiagramScoutType = DiagramScoutType.Offense;
			}

			try
			{
				this._GroupByFields = info.GetValue("_GroupByFields",typeof(IList)) as IList;
			}
			catch
			{
				this._GroupByFields = new StringCollection();
			}

			try
			{
				this._GroupBySectionFilters = info.GetValue("_GroupBySectionFilters",typeof(SectionFilterCollection)) as SectionFilterCollection;
    		   			
			}
			catch
			{
				this._GroupBySectionFilters = new SectionFilterCollection();
			}

			//Added this code at 2008-12-25 14:55:42@Simon
			try
			{
				this._RepeatFields = info.GetValue("_RepeatFields",typeof(IList)) as IList;
			}
			catch
			{
				this._RepeatFields = new StringCollection();
			}

			try
			{
				this._RepeatSectionFilters = info.GetValue("_GroupBySectionFilters",typeof(SectionFilterCollection)) as SectionFilterCollection;
		   	         
			}
			catch
			{
				this._RepeatSectionFilters = new SectionFilterCollection();
			}

			try
			{
				this._RepeatTopCount = info.GetInt32("_RepeatTopCount");
			}
			catch
			{
				this._RepeatTopCount = 0;
			}

			#region Modify codes at 2009-1-12 10:01:51@Simon
			try
			{
				this.ClickEvent = info.GetBoolean("ClickEvent");
			}
			catch
			{
				this.ClickEvent = Webb.Reports.DataProvider.VideoPlayBackManager.ClickEvent;
			}
			#endregion        //End Modify

			//Modified at 2009-1-14 17:27:16@Scott
			try
			{
				this.SectionFiltersWrapper = info.GetValue("SectionFiltersWrapper",typeof(SectionFilterCollectionWrapper)) as SectionFilterCollectionWrapper;
			}
			catch
			{
				//this.SectionFiltersWrapper = new SectionFilterCollectionWrapper();
			}

			//Modified at 2009-1-14 17:27:16@Scott
			try
			{
				this.GroupBySectionFiltersWrapper = info.GetValue("GroupBySectionFiltersWrapper",typeof(SectionFilterCollectionWrapper)) as SectionFilterCollectionWrapper;
			   			
			}
			catch
			{
				//this.GroupBySectionFiltersWrapper = new SectionFilterCollectionWrapper(this.GroupBySectionFilters,ReportScType.Custom);
			}

			//Modified at 2009-1-14 17:27:16@Scott
			try
			{
				this.RepeatSectionFiltersWrapper = info.GetValue("RepeatSectionFiltersWrapper",typeof(SectionFilterCollectionWrapper)) as SectionFilterCollectionWrapper;
		       
			
			}
			catch
			{
				//this.RepeatSectionFiltersWrapper = new SectionFilterCollectionWrapper(this.RepeatSectionFilters,ReportScType.Custom);
			}

			try	//Modified at 2009-2-2 14:01:36@Scott
			{
				this.Consecutive = info.GetBoolean("Consecutive");
			}
			catch
			{
				this.Consecutive = false;
			}
			try	
			{
				this._FieldsGroupStyle =(FieldsGroupStyle)info.GetValue("_FieldsGroupStyle",typeof(FieldsGroupStyle));
			}
			catch
			{
				_FieldsGroupStyle=FieldsGroupStyle.TreeView;;
			}
			try	
			{
				this.AutoLayOut = info.GetBoolean("AutoLayOut");
			}
			catch
			{
				this.AutoLayOut = false;
			}
            try	
			{
				this._OneValueGroupInfo = info.GetValue("_OneValueGroupInfo",typeof(PageGroupInfo)) as PageGroupInfo;

				if(_OneValueGroupInfo is PageSectionInfo)  //2009-3-11 9:46:52@Simon
				{
					_OneValueGroupInfo.SortingByType=SortByTypes.None;
				}
			}
			catch
			{
				this.CreatePageGroupInfo();
						
			}
            try	
			{
				this._RepeatGroupInfo = info.GetValue("_RepeatGroupInfo",typeof(PageGroupInfo)) as PageGroupInfo;

				if(_RepeatGroupInfo is PageSectionInfo)  //2009-3-11 9:46:55@Simon
				{
					_RepeatGroupInfo.SortingByType=SortByTypes.None;
				}
			}
			catch
			{
    			this.CreateRepeatGroupInfo();
			}
			try	
			{
				this._UserLevel = info.GetValue("_UserLevel",typeof(UserLevel)) as UserLevel;
			}
			catch
			{
				_UserLevel=new UserLevel("Level5",31);
			}	
			try	
			{
				this._AccessType = (ReportAccessType)info.GetValue("_AccessType",typeof(ReportAccessType));
			}
			catch
			{
				_AccessType=ReportAccessType.All;
			}
			try	
			{
				this._ReportWizardSetting = (ReportWizardSetting)info.GetValue("_ReportWizardSetting",typeof(ReportWizardSetting));
			}
			catch
			{
				_ReportWizardSetting=null;
			}
		}
		#endregion

        private static ArrayList _usedFields = null;


		#region Instance Field &property
		
		//members
		protected ReportScType _ReportScType;
		protected ReportScType _OneValueReportScType;	//Modified at 2009-1-14 10:03:37@Scott
		protected ReportScType _RepeatReportScType;		//Modified at 2009-1-14 10:03:42@Scott
		protected string _GroupByField;
		protected bool _OneValuePerPage;	//07-07-2008@Scott
		protected int _TopCount;			//07-16-2008@Scott
		protected int _PageSpace;			//07-24-2008@Scott
		protected bool _OnePageReport;		//08-12-2008@Scott
		protected bool _RepeatedReport;		//09-23-2008@Scott
		protected float _RepeatedWidth;		//09-23-2008@Scott
		protected float _RepeatedHeight;	//09-23-2008@Scott
		protected int _RepeatedCount;		//09-23-2008@Scott
		protected int _RepeatedVerticalCount; //Modified at 2008-10-20 10:18:00@Scott
		protected DBFilter _Filter;			//Modified at 2008-10-23 17:24:03@Scott
		protected Webb.Data.DiagramScoutType _DiagramScoutType;	//Modified at 2008-11-6 9:33:30@Scott
		protected IList _GroupByFields; //12-12-2008@Scott 
		protected SectionFilterCollection _SectionFilters;
		protected SectionFilterCollection _GroupBySectionFilters;	//Modified at 2008-12-18 10:40:24@Scott
		protected SectionFilterCollection _RepeatSectionFilters;	//Added this code at 2008-12-25 14:49:03@Simon
		protected SectionFilterCollectionWrapper _SectionFiltersWrapper;		//Modified at 2009-1-14 17:14:51@Scott
		protected SectionFilterCollectionWrapper _GroupBySectionFiltersWrapper;	//Modified at 2009-1-14 17:14:56@Scott
		protected SectionFilterCollectionWrapper _RepeatSectionFiltersWrapper;	//Modified at 2009-1-14 17:14:59@Scott
		protected IList _RepeatFields;             //Added this code at 2008-12-25 14:48:51@Simon
		protected int _RepeatTopCount=0;  //Added this code at 2008-12-26 13:34:05@Simon
		protected bool _Consecutive=false;	//Modified at 2009-2-2 13:55:31@Scott
		protected bool _AutoLayOut=false;
		protected FieldsGroupStyle _FieldsGroupStyle;
		protected PageGroupInfo _OneValueGroupInfo;
		protected PageGroupInfo _RepeatGroupInfo;

		protected UserLevel _UserLevel;	
		
		protected ReportAccessType _AccessType;     //2009-6-15 11:06:51@Simon Add this Code

		protected ReportWizardSetting _ReportWizardSetting=null;

		#region property
		[Browsable(false)]
		public ReportAccessType AccessType          //2009-6-15 11:06:51@Simon Add this Code
		{
			get
			{
				return this._AccessType;
			}
			set
			{			
				
				this._AccessType=value;
			}
		}
		[Browsable(false)]
		public ReportWizardSetting ReportWizardSetting          //2009-6-15 11:06:51@Simon Add this Code
		{
			get
			{
				return this._ReportWizardSetting;
			}
			set
			{			
				
				this._ReportWizardSetting=value;
			}
		}




        [Browsable(false)]
		public UserLevel LicenseLevel
		{
			get{
				if(this._UserLevel==null)_UserLevel=new UserLevel();
				return this._UserLevel;
			   }
			set{			
				
				 this._UserLevel=value;
			    }
		}
		
		[EditorAttribute(typeof(Webb.Reports.Editors.PageGroupEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public PageGroupInfo OneValueSetting
		{
			get
			{		
                if(_OneValueGroupInfo==null)_OneValueGroupInfo=new PageFieldInfo("");

                _OneValueGroupInfo.Repeat=this.OneValuePerPage;

				_OneValueGroupInfo.RepeatTitle="OneValuePerPage";

				return this._OneValueGroupInfo;
			}
			set
			{
				_OneValueGroupInfo=value;

				if(_OneValueGroupInfo!=null)
				{
					this.OneValuePerPage=_OneValueGroupInfo.Repeat;
				}
                _usedFields = null;
			}
		}
		[EditorAttribute(typeof(Webb.Reports.Editors.PageGroupEditor), typeof(System.Drawing.Design.UITypeEditor))]		
		public PageGroupInfo RepeatSetting
		{
			get
			{	
				if(_RepeatGroupInfo==null)_RepeatGroupInfo=new PageFieldInfo("");				

				_RepeatGroupInfo.Repeat=this.RepeatedReport;

				_RepeatGroupInfo.RepeatTitle="RepeatReport";

				return this._RepeatGroupInfo;
			}
			set
			{
				_RepeatGroupInfo=value;
				if(_RepeatGroupInfo!=null)
				{
					this.RepeatedReport=_RepeatGroupInfo.Repeat;
				}
                _usedFields = null;
			}
		}
		public bool AutoLayOut
		{
			get{return this._AutoLayOut;}
			set{this._AutoLayOut=value;}
		}

		public bool Consecutive		//Modified at 2009-2-2 13:58:19@Scott
		{
			get{return this._Consecutive;}
			set{this._Consecutive = value;}
		}

		public int RepeatTopCount
		{
			get{return this._RepeatTopCount;}
			set{this._RepeatTopCount = value < 0 ? 0 : value;}
		}

		//Modified at 2009-1-15 10:15:27@Scott
		[EditorAttribute(typeof(Webb.Reports.Editors.SectionFiltersEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[Browsable(false)]
		public SectionFilterCollectionWrapper SectionFiltersWrapper
		{
			get
			{
				if(this._SectionFiltersWrapper == null) this.SectionFiltersWrapper = new SectionFilterCollectionWrapper();
			
				this._SectionFiltersWrapper.SectionFilters = this.SectionFilters;				

				return this._SectionFiltersWrapper;}
			set
			{
				this._SectionFiltersWrapper = value;  
            
				this._SectionFiltersWrapper.ReportScType=this.ReportScType;
				
				this._SectionFiltersWrapper.UpdateSectionFilters();

				if(this._SectionFiltersWrapper.SectionFilters.Count > 0)	//Modified at 2009-2-6 10:49:45@Scott
				{
					this.SectionFilters = this._SectionFiltersWrapper.SectionFilters;
				}
                _usedFields = null;
			}
		}

		//Modified at 2009-1-15 10:15:18@Scott
		[EditorAttribute(typeof(Webb.Reports.Editors.SectionFiltersEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[Browsable(false)]
		public SectionFilterCollectionWrapper GroupBySectionFiltersWrapper
		{
			get
			{
				if(this._GroupBySectionFiltersWrapper == null) this._GroupBySectionFiltersWrapper = new SectionFilterCollectionWrapper();
				this._GroupBySectionFiltersWrapper.SectionFilters = this.GroupBySectionFilters;
				return this._GroupBySectionFiltersWrapper;}
			set
			{
				this._GroupBySectionFiltersWrapper = value;

				_GroupBySectionFiltersWrapper.ReportScType=AdvFilterConvertor.GetScType(_GroupBySectionFiltersWrapper.ReportScType,DataProvider.VideoPlayBackManager.AdvSectionType);  //2009-6-16 10:18:47@Simon Add this Code

				AdvFilterConvertor convertor = new AdvFilterConvertor();
				//Modified at 2009-1-19 14:09:52@Scott
				if(_GroupBySectionFiltersWrapper.ReportScType != ReportScType.Custom)
				{
					_GroupBySectionFiltersWrapper.SectionFilters = convertor.GetReportFilters(Webb.Reports.DataProvider.VideoPlayBackManager.AdvReportFilters,_GroupBySectionFiltersWrapper.ReportScType);	//add 1-19-2008 scott
				}

				this.GroupBySectionFilters = this._GroupBySectionFiltersWrapper.SectionFilters;
			}
		}

		//Modified at 2009-1-15 10:15:14@Scott
		[EditorAttribute(typeof(Webb.Reports.Editors.SectionFiltersEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[Browsable(false)]
		public SectionFilterCollectionWrapper RepeatSectionFiltersWrapper
		{
			get
			{
				if(this._RepeatSectionFiltersWrapper == null) this._RepeatSectionFiltersWrapper = new SectionFilterCollectionWrapper();
				
				this._RepeatSectionFiltersWrapper.SectionFilters = this.RepeatSectionFilters;

				return this._RepeatSectionFiltersWrapper;}
			set
			{
				this._RepeatSectionFiltersWrapper = value;
				
				_RepeatSectionFiltersWrapper.ReportScType=AdvFilterConvertor.GetScType(_RepeatSectionFiltersWrapper.ReportScType,DataProvider.VideoPlayBackManager.AdvSectionType);  //2009-6-16 10:18:47@Simon Add this Code
					
			   AdvFilterConvertor convertor = new AdvFilterConvertor();
				//Modified at 2009-1-19 14:09:52@Scott
				if(_RepeatSectionFiltersWrapper.ReportScType!= ReportScType.Custom)
				{
					_RepeatSectionFiltersWrapper.SectionFilters = convertor.GetReportFilters(Webb.Reports.DataProvider.VideoPlayBackManager.AdvReportFilters,_RepeatSectionFiltersWrapper.ReportScType);	//add 1-19-2008 scott
				}


				this.RepeatSectionFilters = this._RepeatSectionFiltersWrapper.SectionFilters;
			}
		}

		[EditorAttribute(typeof(Webb.Reports.Editors.SectionFiltersEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[Browsable(false)]
		public SectionFilterCollection GroupBySectionFilters  
		{
			get
			{
				if(this._GroupBySectionFilters == null) this._GroupBySectionFilters = new SectionFilterCollection();

				return this._GroupBySectionFilters;
			}
			set{this._GroupBySectionFilters = value;}
		}

		[EditorAttribute(typeof(Webb.Reports.Editors.SortingColumnEditor), typeof(System.Drawing.Design.UITypeEditor))]  //Added this code at 2008-12-25 14:52:27@Simon
		[Browsable(false)]
		public IList GroupByFields 
		{
			get
			{
				if(this._GroupByFields == null) this._GroupByFields = new StringCollection();
				return this._GroupByFields;
			}
			set{this._GroupByFields = value;}
		}
        [Browsable(false)]
        public FieldsGroupStyle FieldsStyle
		{
			get
			{				
				return this._FieldsGroupStyle;
			}
			set{this._FieldsGroupStyle = value;}
		}
	
	   [Browsable(false)]
		public IList GroupFieldsOrder
		{
			get
			{
				if(this._GroupByFields== null) this._GroupByFields = new StringCollection();

				return this._GroupByFields;
			}
			set{this._GroupByFields = value;}
		}
		[Browsable(false)]
		public IList RepeatFieldsOrder
		{
			get
			{
				if(this._RepeatFields== null) this._RepeatFields = new StringCollection();

				return this._RepeatFields;
			}
			set{this._RepeatFields = value;}
		}

		[EditorAttribute(typeof(Webb.Reports.Editors.SectionFiltersEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[Browsable(false)]
		public SectionFilterCollection RepeatSectionFilters
		{
			get
			{
				if(this._RepeatSectionFilters == null) this._RepeatSectionFilters = new SectionFilterCollection();

				return this._RepeatSectionFilters;
			}
			set{this._RepeatSectionFilters = value;}
		}

		[EditorAttribute(typeof(Webb.Reports.Editors.SortingColumnEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[Browsable(false)]
		public IList RepeatFields
		{
			get
			{				
				if(this._RepeatFields== null) this._RepeatFields = new StringCollection();
				return this._RepeatFields;
			}
			set{this._RepeatFields = value;}
		}

		public DiagramScoutType DiagramScoutType
		{
			get{return this._DiagramScoutType;}
			set{this._DiagramScoutType = value;}
		}

		[EditorAttribute(typeof(Webb.Reports.Editors.DBFilterEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public DBFilter Filter
		{
			get
			{
				if(this._Filter == null) this._Filter = new DBFilter();

				return this._Filter;
			}
			set{this._Filter = value;
            _usedFields = null;
              }
		}
        [Browsable(false)]
		public bool RepeatedReport
		{
			get{return this._RepeatedReport;}
			
			set{this._RepeatedReport = value;}
		}

		public int RepeatedHorizonCount
		{
			get{return this._RepeatedCount;}

			set{this._RepeatedCount = value;}
		}        
		public int RepeatedVerticalCount
		{
			get{return this._RepeatedVerticalCount;}
			
			set{this._RepeatedVerticalCount = value;}
		}

		public float RepeatedWidth
		{
			get{return this._RepeatedWidth;}
			
			set{this._RepeatedWidth = value;}
		}

		public float RepeatedHeight
		{
			get{return this._RepeatedHeight;}
			
			set{this._RepeatedHeight = value;}
		}

		[EditorAttribute(typeof(Webb.Reports.Editors.SectionFiltersEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[Browsable(false)]
		public SectionFilterCollection SectionFilters
		{
			get
			{
				if(this._SectionFilters == null) this._SectionFilters = new SectionFilterCollection();

				return this._SectionFilters;
			}
			set{this._SectionFilters = value;}
		}
        
		[TypeConverter(typeof(Webb.Data.PublicDBFieldConverter))]
		[Browsable(false)]
		public string GroupByField
		{
			get{
                   if(_GroupByField==null)_GroupByField=string.Empty;
			    	return this._GroupByField;
			   }
			set{this._GroupByField = value;}
		}

		[Browsable(false)]
		public ReportScType ReportScType
		{
			get{return this._ReportScType;}
			set{this._ReportScType = value;}
		}

		//Modified at 2009-1-14 10:01:53@Scott
		[Browsable(false)]
		public ReportScType OneValueReportScType
		{
			get{return this._OneValueReportScType;}
			set{this._OneValueReportScType = value;}
		}

		//Modified at 2009-1-14 10:02:01@Scott
		[Browsable(false)]
		public ReportScType RepeatReportScType
		{
			get{return this._RepeatReportScType;}
			set{this._RepeatReportScType = value;}
		}

		[Browsable(false)]
		public bool ClickEvent
		{
			get{return Webb.Reports.DataProvider.VideoPlayBackManager.ClickEvent;}
			set
			{
				Webb.Reports.DataProvider.VideoPlayBackManager.ClickEvent=value;
			}
		}
        [Browsable(false)]
		public bool OneValuePerPage
		{
			get{return this._OneValuePerPage;}
			set{this._OneValuePerPage = value;}
		}

		public int TopCount
		{
			get{return this._TopCount;}
			set{this._TopCount = value < 0 ? 0 : value;}
		}
      
		public bool ControlHeaderOnce
		{
			get{return this._OnePageReport;}
			set{this._OnePageReport = value;}
		}
		#endregion

		#endregion

		//fuction
		public bool Save(string strFileName)
		{
			try
			{
				Webb.Utilities.Serializer.Serialize(this,strFileName,true);
			}
			catch
			{
				return false;
			}
			return true;
		}
		public void CreatePageGroupInfo()
		{	
			if(!this.OneValuePerPage)
			{
				_OneValueGroupInfo=new PageFieldInfo("");

				return;
			}
			else if(GroupByField!=null&&this.GroupByField!=string.Empty)
			{
				this._OneValueGroupInfo=new PageFieldInfo(this.GroupByField);
			}
			else if(this.GroupByFields.Count>0)
			{
				this._OneValueGroupInfo=new PageFieldInfo((string)this.GroupByFields[0]);

				PageGroupInfo group=_OneValueGroupInfo;

				for(int i=1;i<GroupByFields.Count;i++)
				{
					PageGroupInfo subgroup=new PageFieldInfo((string)this.GroupByFields[i]);

                    group.SubPageGroupInfos.Clear();
					 
                    group.SubPageGroupInfos.Add(subgroup);

					group=subgroup;
				}

			}
			else
			{
				this._OneValueGroupInfo=new PageSectionInfo(this.GroupBySectionFiltersWrapper);
			}

		}
	
		public void CreateRepeatGroupInfo()
		{	
			if(!this.RepeatedReport)
			{
				this._RepeatGroupInfo=new PageFieldInfo("");

				return;
			}			
			else if(this._RepeatFields.Count>0)
			{
				this._RepeatGroupInfo=new PageFieldInfo((string)this._RepeatFields[0]);

				PageGroupInfo group=_RepeatGroupInfo;

				for(int i=1;i<_RepeatFields.Count;i++)
				{
					PageGroupInfo subgroup=new PageFieldInfo((string)this._RepeatFields[i]);

					group.SubPageGroupInfos.Clear();
					 
					group.SubPageGroupInfos.Add(subgroup);

					group=subgroup;
				}

			}
			else
			{
				this._RepeatGroupInfo=new PageSectionInfo(this.RepeatSectionFiltersWrapper);
			}

		}
	
		public bool Load(string strFileName)
		{
			try
			{
				WebbReportTemplate tmpWebbReport = Webb.Utilities.Serializer.Deserialize(strFileName) as WebbReportTemplate;
				
				this.Apply(tmpWebbReport);
			}
			catch
			{
				return false;
			}
			return true;
		}

		public void Apply(WebbReportTemplate template)
		{
			this._SectionFilters = template.SectionFilters.Copy();
			
			this._GroupByField = template.GroupByField;

			this._ReportScType = template.ReportScType;

			this._TopCount = template.TopCount;

			this._OnePageReport = template._OnePageReport;

			this._RepeatedReport = template.RepeatedReport;

			this._RepeatedWidth = template.RepeatedWidth;

			this._RepeatedHeight = template.RepeatedHeight;

			this._RepeatedCount = template._RepeatedCount;

			this._RepeatedVerticalCount = template.RepeatedVerticalCount;

			this.Filter = template.Filter.Copy();

			this._DiagramScoutType = template.DiagramScoutType;

			this.GroupByFields.Clear();
			foreach(object o in template.GroupByFields)
			{
				this.GroupByFields.Add(o);
			}

			this._GroupBySectionFilters = template.GroupBySectionFilters.Copy();

			this.RepeatFields.Clear();

			foreach(object o in template.RepeatFields)
			{
				this.RepeatFields.Add(o);
			}

			this.RepeatSectionFilters= template.RepeatSectionFilters.Copy();

			this._RepeatTopCount=template.RepeatTopCount;

			this.SectionFiltersWrapper = template.SectionFiltersWrapper;	//Modified at 2009-1-15 9:12:52@Scott

			this.GroupBySectionFiltersWrapper = template.GroupBySectionFiltersWrapper;	//Modified at 2009-1-15 9:12:55@Scott

			this.RepeatSectionFiltersWrapper = template.RepeatSectionFiltersWrapper;	//Modified at 2009-1-15 9:12:58@Scott

			this.Consecutive = template.Consecutive;	//Modified at 2009-2-2 14:00:01@Scott

			this.AutoLayOut=template.AutoLayOut;  //Add at 2009-2-25 14:05:11@Simon

			this._FieldsGroupStyle=template._FieldsGroupStyle;  //2009-3-4 14:37:40@Simon

			this._OneValueGroupInfo=template._OneValueGroupInfo;  //2009-3-5 15:14:09@Simon

			this.LicenseLevel=template.LicenseLevel.Copy();   //2009-6-15 10:57:48@Simon Add this Code

			if(ReportWizardSetting!=null)
			{
				this._ReportWizardSetting=template.ReportWizardSetting.Copy();
			}
        }


        #region Fields for CCRM

        private void CalculateAllFieldsUsed()
        {
            _usedFields = new ArrayList();

            this.SectionFiltersWrapper.GetAllUsedFields(ref _usedFields);

            this.Filter.GetAllUsedFields(ref _usedFields);

            if (this.OneValueSetting.Repeat)
            {
                this.OneValueSetting.GetAllUsedFields(ref _usedFields);
            }
            if (this.RepeatSetting.Repeat)
            {
                this.RepeatSetting.GetAllUsedFields(ref _usedFields);
            }

        }

        public void GetAllUsedFields(ref ArrayList _UsedFields)
        {
            if (_usedFields == null) CalculateAllFieldsUsed();

            foreach(string strField in _usedFields)
            {
                if (!_UsedFields.Contains(strField))
                {
                    _UsedFields.Add(strField);
                }
            }

        }
        #endregion


    }
	#endregion

	[Serializable]
	public class GroupInfoData
	{
		protected SectionFilterCollection _SectionFilters;
		protected string _Field;

		public SectionFilterCollection SectionFilters
		{
			get
			{
				if(this._SectionFilters == null) this._SectionFilters = new SectionFilterCollection();
				
				return this._SectionFilters;
			}

			set{this._SectionFilters = value;}
		}

		public string Field
		{
			get
			{
				return this._Field;
			}
			set
			{
				this._Field = value;
			}
		}
	}
}