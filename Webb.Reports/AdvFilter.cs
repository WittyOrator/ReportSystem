using System;
using System.IO;
using System.Collections;
using Microsoft.Win32;

using Webb.Data;

namespace Webb.Reports
{
	#region Enums
	public enum Oper
	{
		NumericEqual = 0,
		NumericNotEqual = 1,
		NumericGreaterThan = 2,
		NumericLessThan = 3,
		NumericGreaterThanOrEqual = 4,
		NumericLessThanOrEqual = 5,
		StringEqual = 6,
		StringNotEqual = 7,
		StringStartsWith = 8,
		StringEndsWith = 9,
		StringIncludes = 10,
		StringNotStartsWith = 11,
		StringNotEndsWith = 12,
		StringNotIncludes = 13,
	}

	public enum ReportScType
	{
		Custom = 0,
		FieldZoneOffense = 1,
		DownAndDistanceOffense = 2,
		FieldZoneDefense = 3,
		DownAndDistanceDefense = 4,

        //OffenseDefineEfficiency= 10,
        //DefenseDefineEfficiency = 11,

        //RunPassOffenseEFF= 20,
        //RunPassDefenseEFF = 21,
            //        const TCHAR szFilterNameOffRunEff[]  = _T( "-RUNEFF-O" );
            //       const TCHAR szFilterNameOffPassEff[] = _T( "-PASSEFF-O" );
            //      const TCHAR szFilterNameDefRunEff[]  = _T( "-RUNEFF-D" );
            //     const TCHAR szFilterNameDefPassEff[] = _T( "-PASSEFF-D" );
	}

	public enum ScoutType
	{
		Offense = 0,
		Defence = 1,
		Error = 2,
	}

	public enum Opr
	{
		OR = 1,
		AND = 0,
		END = -1,
	}
	public enum AdvScoutType
    {
		None= 0,	
		Offense = 1,
		Defence = 2,		
    }
	#endregion

	#region Utilities
	public class AdvFilterUtilities
	{
		public const int cDataBufLen  = 200;
		public const string FilterRev01 = "FL001";
		public const string FilterRev02 = "FL002";
		public const string FilterRev03 = "FL003";
		public const string FilterName = "ScFilterList";

		public static string ReadString(FileStream fs,bool bTranslateAsc,bool bAddPos)
		{
			int len = 0;
			
			if(bTranslateAsc) 
			{
				len = ReadInt32(fs);
			}
			else
			{
				len = fs.ReadByte();
			}

			byte[] bytes = new byte[len];

			fs.Read(bytes,0,len);

			string str = System.Text.Encoding.Default.GetString(bytes);

			if(bAddPos) fs.Position++;

			return str;
		}

		#region Modify codes at 2009-4-2 16:38:11@Simon
		public static short ReadShort(System.IO.FileStream fs)
		{
			byte[]   byTmp = new byte[2]; 

			fs.Read(byTmp,0,2);

			return  BitConverter.ToInt16(byTmp,0);            
		}
		#endregion        //End Modify

		public static int ReadInt32(FileStream fs)
		{
			System.Text.StringBuilder strInt = new System.Text.StringBuilder();

			while(true)
			{
				char c = (char)fs.ReadByte();

				if(c == ' ') break;

				strInt.Append(c);
			}

			int n =  Int32.Parse(strInt.ToString());

			return n;
		}

	
	}
	#endregion

	#region public class ScFilterElement
	[Serializable]
	public class ScFilterElement
	{
		public ScFilterElement(int o, int r, string field, string value)
		{
			Field = field;
			Value = value;
			Oper = o;
			Opr = r;
		}

		public ScFilterElement(ScFilterElement scFilter)
		{
			Field = scFilter.Field;
			Value = scFilter.Value;
			Oper = scFilter.Oper;
			Opr = scFilter.Opr;
		}

		public ScFilterElement()
		{
			Type = -1;
		}

		private int _Type;
		public int Type
		{
			get{return this._Type;}
			set{this._Type = value;}
		}
		private int _Oper;
		public int Oper
		{
			get{return this._Oper;}
			set{this._Oper = value;}
		}
		private int _Opr;
		public int Opr
		{
			get{return this._Opr;}
			set{this._Opr = value;}
		}
		private string _Value;
		public string Value
		{
			get
			{
				if(this._Value == null) this._Value = string.Empty;

				return this._Value;
			}
			set{this._Value = value;}
		}
		private string _Field;
		public string Field
		{
			get
			{
				if(this._Field == null) this._Field = string.Empty;

				return this._Field;
			}
			set{this._Field = value;}
		}

		public override string ToString()
		{
			//return base.ToString ();

			return string.Format("({0}) {1} {2} {3} {4}",Enum.GetName(typeof(ScoutType),this.Type),this._Field,Enum.GetName(typeof(Oper),this.Oper),this._Value,Enum.GetName(typeof(Opr),this.Opr));
		}
	}
	#endregion

	#region public class ScFilterElementCollection : CollectionBase
	[Serializable]
	public class ScFilterElementCollection : CollectionBase
	{
		public ScFilterElement this[int i_Index]
		{ 
			get { return this.InnerList[i_Index] as ScFilterElement; } 
			set { this.InnerList[i_Index] = value; } 
		} 
		//ctor
		public ScFilterElementCollection() {} 
		//Methods
		public int Add(ScFilterElement i_Object)
		{
			return this.InnerList.Add(i_Object);
		}

		public void Insert(int index,ScFilterElement i_Object)
		{
			this.InnerList.Insert(index,i_Object);
		}

		public void Remove(ScFilterElement i_Obj)
		{
			this.InnerList.Remove(i_Obj);
		}

		public ScFilterElementCollection Copy()
		{
			ScFilterElementCollection scFilterElements = new ScFilterElementCollection();

			foreach(ScFilterElement scFilterElement in this)
			{
				scFilterElements.Add(scFilterElement);
			}

			return scFilterElements;
		}
	}
	#endregion

	#region public class ScAFilter
	[Serializable]
	public class ScAFilter
	{
		private int _PlayAfter;
		public int PlayAfter
		{
			get{return this._PlayAfter;}
			set{this._PlayAfter = value;}
		}
		private ScFilterElementCollection _Elements;
		public ScFilterElementCollection Elements
		{
			get
			{
				if(this._Elements == null) this._Elements = new ScFilterElementCollection();

				return this._Elements;
			}
			set{this._Elements = value;}
		}
		private ArrayList _Types;
		public ArrayList Types
		{
			get
			{
				if(this._Types == null) this._Types = new ArrayList();

				return this._Types;
			}
			set{this._Types = value;}
		}
		private string _Name;
		public string Name
		{
			get
			{
				if(this._Name == null) this._Name = string.Empty;

				return this._Name;
			}
			set{this._Name = value;}
		}
		private int _ReportScType;
		public int ReportScType
		{
			get{return this._ReportScType;}
			set{this._ReportScType = value;}
		}

		public ScAFilter()
		{
			this.Name = "New Filter";
		}

		public ScAFilter(string s)
		{
			this.Name = s;
		}

		public ScAFilter(ScAFilter scFilter)
		{
			this.Name = scFilter.Name;
			this.Elements = scFilter.Elements.Copy();
			this.Types.Clear();
			foreach(object type in scFilter.Types)
			{
				this.Types.Add(type);
			}
		}

		public override string ToString()
		{
			//return base.ToString ();
			return string.Format("({0}) {1}",Enum.GetName(typeof(ReportScType),this.ReportScType),this.Name);
		}
	}
	#endregion

	#region public class ScAFilterCollection : CollectionBase
	[Serializable]
	public class ScAFilterCollection : CollectionBase
	{
		public ScAFilter this[int i_Index]
		{ 
			get { return this.InnerList[i_Index] as ScAFilter; } 
			set { this.InnerList[i_Index] = value; } 
		} 
		public ScAFilter this[string i_Name]	//Modified at 2008-11-24 15:42:52@Scott
		{
			get
			{
				return this.GetScAFilterByName(i_Name);
			}
		}
		private ScAFilter GetScAFilterByName(string strFilterName)	//Modified at 2008-11-24 15:42:56@Scott
		{
			if(strFilterName == null) return null;

			foreach(ScAFilter filter in this)
			{
				if(filter.Name.ToLower() == strFilterName.ToLower())
				{
					return filter;
				}
			}
			return null;
		}
		//ctor
		public ScAFilterCollection() {} 
		//Methods
		public int Add(ScAFilter i_Object)
		{
			return this.InnerList.Add(i_Object);
		}

		public void Insert(int index,ScAFilter i_Object)
		{
			this.InnerList.Insert(index,i_Object);
		}

		public void Remove(ScAFilter i_Obj)
		{
			this.InnerList.Remove(i_Obj);
		}

		public ScAFilterCollection Copy()
		{
			ScAFilterCollection scFilters = new ScAFilterCollection();

			foreach(ScAFilter scFilter in this)
			{
				scFilters.Add(scFilter);
			}

			return scFilters;
		}
	}
	#endregion

	#region public class ScFilterList
	[Serializable]
	public class ScFilterList
	{
		private string _FileName;
		public string FileName
		{
			get
			{
				if(this._FileName == null) this._FileName = string.Empty;

				return this._FileName;
			}
			set{this._FileName = value;}
		}
		private ScAFilterCollection _ScFilters;
		public ScAFilterCollection ScFilters
		{
			get
			{
				if(this._ScFilters == null) this._ScFilters = new ScAFilterCollection();

				return this._ScFilters;
			}
			set{this._ScFilters = value;}
		}
		private bool _Update;
		private bool Update
		{
			get{return this._Update;}
		}

		public ScFilterList()
		{
			this._Update = false;
		}

		public ScAFilter GetFilter(string strFilterName)	//Modified at 2008-11-24 15:44:29@Scott
		{
			return this.ScFilters[strFilterName];
		}

		public bool ReadFromDisk(string strFileName)
		{
			bool bSuccess = true;

			if(!File.Exists(strFileName)) return false;

			FileStream fs = File.Open(strFileName,FileMode.Open,FileAccess.Read,FileShare.Read);

			String str = string.Empty;

			int len = 0,count = 0;
			
			//read version
			str = AdvFilterUtilities.ReadString(fs,true,true);

			if(String.Compare(str,AdvFilterUtilities.FilterRev01) > 0)	//check filter list version
			{
				fs.Close();

				bSuccess = false;

				goto EXIT;
			}

			//			//read count of filters
			count = AdvFilterUtilities.ReadShort(fs);

			//read filters
			ScAFilter aFilter = null;
			ScFilterElement anElement = null;

			for(int i = 0; i<count; i++)
			{
				aFilter = new ScAFilter();

				aFilter.Name = AdvFilterUtilities.ReadString(fs,true,true);

				len = AdvFilterUtilities.ReadInt32(fs);

				for(int j = 0; j < len; j++)
				{
					aFilter.Types.Add(AdvFilterUtilities.ReadString(fs,true,true));
				}

				len = AdvFilterUtilities.ReadInt32(fs);

				for(int j = 0; j < len; j++)
				{
					anElement = new ScFilterElement();

					anElement.Field = AdvFilterUtilities.ReadString(fs,true,true);

					anElement.Value = AdvFilterUtilities.ReadString(fs,true,true);

					anElement.Oper = AdvFilterUtilities.ReadInt32(fs);

					anElement.Opr = AdvFilterUtilities.ReadInt32(fs);
					
					aFilter.Elements.Add(anElement);
				}
				this.ScFilters.Add(aFilter);
			}

			EXIT:
			{
				fs.Close();

				return bSuccess;
			}
		}

		public bool ReadOldFiltersFromDisk(string strFileName)
		{
			bool bSuccess = true;

			if(!File.Exists(strFileName)) return false;

			FileStream fs = File.Open(strFileName,FileMode.Open,FileAccess.Read,FileShare.Read);

			String str = string.Empty,dev = string.Empty;

			int len = 0,count = 0;

			char c = char.MinValue;

			//Opening bracket
			c = (char)fs.ReadByte();

			if(c != '[') 
			{
				bSuccess = false;

				goto EXIT;
			}

			//Read filer name - "ScfilterList"
			str = AdvFilterUtilities.ReadString(fs,false,false);
			if(string.Compare(AdvFilterUtilities.FilterName,str) != 0)
			{
				bSuccess = false;

				goto EXIT;
			}

			//Read version
			dev = AdvFilterUtilities.ReadString(fs,false,false);
			if(string.Compare(dev,AdvFilterUtilities.FilterRev02) < 0)
			{
				bSuccess = false;

				goto EXIT;
			}

			//Read filters
			ScAFilter aFilter = null;
			ScFilterElement anElement = null;
         
			count = AdvFilterUtilities.ReadShort(fs);

			for(int i = 0; i<count; i++)
			{
				aFilter = new ScAFilter();

				aFilter.Name = AdvFilterUtilities.ReadString(fs,false,false);	//filter name

                aFilter.ReportScType = fs.ReadByte();								//type      //10--Efficiency

				fs.Position++;

				if(string.Compare(dev,AdvFilterUtilities.FilterRev03) < 0)		//Scouting filter Ver.3 have play after feature
				{
					aFilter.PlayAfter = 0;
				}
				else
				{
					aFilter.PlayAfter = fs.ReadByte();

					fs.Position ++;
				}

				len = fs.ReadByte();	//count of elements

				fs.Position ++;

				for(int j = 0; j<len; j++)
				{
					anElement = new ScFilterElement();

					anElement.Type = fs.ReadByte();		//element's type

					fs.Position += 3;

					anElement.Field = AdvFilterUtilities.ReadString(fs,false,false);	//element's field

					fs.Position += 2;

					anElement.Value = AdvFilterUtilities.ReadString(fs,false,false);	//element's value

					anElement.Oper = fs.ReadByte();		//element's oper

					fs.Position ++;

					anElement.Opr = fs.ReadByte();		//element's opr	

					fs.Position ++;

					aFilter.Elements.Add(anElement);				
				}

				this.ScFilters.Add(aFilter);
			}

			EXIT:
			{
				fs.Close();
				
				return bSuccess;
			}
		}

		
	}
	#endregion

	#region public class AdvFilterConvertor
	public class AdvFilterConvertor
	{
        public const string EfficiencyDisplayString = "Define Efficiency";

		public static ReportScType GetScType(ReportScType reportScType,AdvScoutType advscoutType)
		{
			if(reportScType==ReportScType.Custom||advscoutType==AdvScoutType.None)
			{
				return reportScType;
			}

            ReportScType RealReportScType=reportScType;
            
			if(advscoutType==AdvScoutType.Offense)
			{
				if(reportScType==ReportScType.FieldZoneDefense)
				{
					RealReportScType=ReportScType.FieldZoneOffense;
				}
				else if(reportScType==ReportScType.DownAndDistanceDefense)
				{
					RealReportScType=ReportScType.DownAndDistanceOffense;
				}
			}
			else if(advscoutType==AdvScoutType.Defence)
			{
				if(reportScType==ReportScType.FieldZoneOffense)
				{
					RealReportScType=ReportScType.FieldZoneDefense;
				}
				else if(reportScType==ReportScType.DownAndDistanceOffense)
				{
					RealReportScType=ReportScType.DownAndDistanceDefense;
				}
			}
			
			return RealReportScType;
			
		}

		public AdvFilterConvertor()
		{
			
		}


		public static string GetAdvatageReportsPath()
		{
			 string RegKeyPath = "SOFTWARE\\Webb Electronics\\WebbReport\\RecentAdvantageUserFolder";

            string KeyName = "LastAdvantageUserPath";

            RegistryKey _RegKey = Registry.CurrentUser.OpenSubKey(RegKeyPath, true);
		
			if (_RegKey == null)
			{
                _RegKey = Registry.CurrentUser.CreateSubKey(RegKeyPath);
			}
			
			object objUserFolder = _RegKey.GetValue(KeyName);

			if(objUserFolder == null) return string.Empty;

			string strUserFolder = objUserFolder.ToString();		

			if(strUserFolder.EndsWith("\\")) strUserFolder = strUserFolder.Remove(strUserFolder.Length - 1,1);

			int index = strUserFolder.LastIndexOf('\\');

			strUserFolder = strUserFolder.Remove(index + 1,strUserFolder.Length - index - 1);

			strUserFolder += @"Interactive Reports";

			if(!Directory.Exists(strUserFolder))
			{
				try
				{
					Directory.CreateDirectory(strUserFolder);                  
				}
				catch(Exception ex)
				{
					Webb.Utilities.MessageBoxEx.ShowError(ex.Message);

					return string.Empty;
				}
			}

			return strUserFolder;

		}

        // 08-22-2011 Scott
        public SectionFilterCollection GetEffFilters(ScFilterList scFilterList)
        {
            SectionFilterCollection sectionFilters = new SectionFilterCollection();

            sectionFilters.Add(new SectionFilter("Offense Eff", this.GetOffenseEffFilter(scFilterList)));
            sectionFilters.Add(new SectionFilter("Offense Run Eff", this.GetRunOffenseEffFilter(scFilterList)));
            sectionFilters.Add(new SectionFilter("Offense Pass Eff", this.GetPassOffenseEffFilter(scFilterList)));
            sectionFilters.Add(new SectionFilter("Defense Eff", this.GetDefenseEffFilter(scFilterList)));
            sectionFilters.Add(new SectionFilter("Defense Run Eff", this.GetRunDefenseEffFilter(scFilterList)));
            sectionFilters.Add(new SectionFilter("Defense Pass Eff", this.GetPassDefenseEffFilter(scFilterList)));

            return sectionFilters;
        }

		public SectionFilterCollection GetReportFilters(ScFilterList scFilterList,ReportScType reportScType)
		{
			SectionFilterCollection sectionFilters = new SectionFilterCollection();

			foreach(ScAFilter scFilter in scFilterList.ScFilters)
			{
				if(Enum.GetName(typeof(ReportScType),scFilter.ReportScType) == reportScType.ToString())
				{
                    if ((!scFilter.Name.EndsWith("-O") && !scFilter.Name.EndsWith("-D")) && scFilter.ReportScType !=10 &&scFilter.ReportScType != 11)	//crazy   //scFilter.ReportScType!=9  
                    {
                        continue;
                    }

					sectionFilters.Add(this.GetReportFilter(scFilter));
				}
			}

			return sectionFilters;
		}


        #region  Old EFF filter  for advantage 5030 build030
            //public DBFilter GetDefenseEffFilter(ScFilterList scFilterList)
            //{
            //    DBFilter EffFilter = new DBFilter();

            //    foreach (ScAFilter scFilter in scFilterList.ScFilters)
            //    {
            //        if (scFilter.ReportScType != 11) continue;

            //        for (int i = 0; i < scFilter.Elements.Count; i++)
            //        {
            //            ScFilterElement element = scFilter.Elements[i];

            //            Webb.Data.DBCondition condition = new Webb.Data.DBCondition();

            //            condition.ColumnName = element.Field;

            //            condition.Value = element.Value;

            //            condition.FilterType = GetFilterOper(element.Oper);

            //            if (condition.ColumnName == "Distance" && condition.Value != null && condition.Value.ToString() == "16")
            //            {
            //                condition.FilterType = FilterTypes.NumGreaterOrEqual;
            //            }

            //            condition.FollowedOperand = this.GetFilterOpr(element.Opr);

            //            if (i == scFilter.Elements.Count - 1) condition.FollowedOperand = FilterOperands.Or;

            //            condition.IgnoreCase = true;

            //            EffFilter.Add(condition);
            //        }

            //        EffFilter.PlayAfter = scFilter.PlayAfter == 1;

            //    }           

            //    EffFilter.IsCustomFilter = true;   //2010-11-1 12:11:46@Simon Add this Code

            //    EffFilter.Name = "DefenseEFF";  //2009-4-29 12:11:46@Simon Add this Code      

            //    return EffFilter;
            //}

            //public DBFilter GetOffenseEffFilter(ScFilterList scFilterList)
            //{
            //    DBFilter EffFilter = new DBFilter();

            //    foreach (ScAFilter scFilter in scFilterList.ScFilters)
            //    {
            //        if (scFilter.ReportScType != 10) continue;

            //        for (int i = 0; i < scFilter.Elements.Count; i++)
            //        {
            //            ScFilterElement element = scFilter.Elements[i];

            //            Webb.Data.DBCondition condition = new Webb.Data.DBCondition();

            //            condition.ColumnName = element.Field;

            //            condition.Value = element.Value;

            //            condition.FilterType = GetFilterOper(element.Oper);

            //            if (condition.ColumnName == "Distance" && condition.Value != null && condition.Value.ToString()== "16")
            //            {
            //                condition.FilterType = FilterTypes.NumGreaterOrEqual;
            //            }

            //            condition.FollowedOperand = this.GetFilterOpr(element.Opr);

            //            if (i == scFilter.Elements.Count - 1) condition.FollowedOperand = FilterOperands.Or;

            //            condition.IgnoreCase = true;

            //            EffFilter.Add(condition);
            //        }

            //        EffFilter.PlayAfter = scFilter.PlayAfter == 1;

            //    }
               
            //    EffFilter.IsCustomFilter = true;   //2010-11-1 12:11:46@Simon Add this Code

            //    EffFilter.Name = "OffenseEFF";  //2009-4-29 12:11:46@Simon Add this Code      

            //    return EffFilter;
            //}
            
        #endregion

        #region  EFF filter 5030 build031

        public DBFilter GetDefenseEffFilter(ScFilterList scFilterList)
        {
            DBFilter runFilter = this.GetRunDefenseEffFilter(scFilterList);

            DBFilter passFilter = this.GetPassDefenseEffFilter(scFilterList);

            DBFilter effDefenseFilter = DBFilter.AttachedTwoFilterWithOr(runFilter, passFilter);

            effDefenseFilter.IsCustomFilter = true;   //2010-11-1 12:11:46@Simon Add this Code

            effDefenseFilter.Name = "DefenseEFF";  //2009-4-29 12:11:46@Simon Add this Code      

            return effDefenseFilter;
        }

        public DBFilter GetOffenseEffFilter(ScFilterList scFilterList)
        {
            DBFilter runFilter = this.GetRunOffenseEffFilter(scFilterList);

            DBFilter passFilter = this.GetPassOffenseEffFilter(scFilterList);

            DBFilter effoffenseFilter = DBFilter.AttachedTwoFilterWithOr(runFilter, passFilter);

            effoffenseFilter.IsCustomFilter = true;   //2010-11-1 12:11:46@Simon Add this Code

            effoffenseFilter.Name = "OffenseEFF";  //2009-4-29 12:11:46@Simon Add this Code      

            return effoffenseFilter;
        }

        public DBFilter UpdateEffFilter(ScFilterList scFilterList, DBFilter filter)
        {
            if (filter == null) return null;

            if (filter.Name == "OffenseEFF")
            {
                DBFilter offensiveFilter = GetOffenseEffFilter(scFilterList);

                if (offensiveFilter != null && offensiveFilter.Count > 0)
                {
                    return offensiveFilter;
                }
            }
            else if (filter.Name == "RunOffenseEFF")
            {
                DBFilter runOffenseEFFFilter = GetRunOffenseEffFilter(scFilterList);

                if (runOffenseEFFFilter != null && runOffenseEFFFilter.Count > 0)
                {
                    return runOffenseEFFFilter;
                }
            }
            else if (filter.Name == "PassOffenseEFF")
            {
                DBFilter passOffenseEFFFilter = GetPassOffenseEffFilter(scFilterList);

                if (passOffenseEFFFilter != null && passOffenseEFFFilter.Count > 0)
                {
                    return passOffenseEFFFilter;
                }
            }
            else if (filter.Name == "DefenseEFF")
            {
                DBFilter defensiveFilter = GetDefenseEffFilter(scFilterList);

                if (defensiveFilter != null && defensiveFilter.Count > 0)
                {
                    return defensiveFilter;
                }
            }
            else if (filter.Name == "RunDefenseEFF")
            {
                DBFilter rundefensiveFilter = GetRunDefenseEffFilter(scFilterList);

                if (rundefensiveFilter != null && rundefensiveFilter.Count > 0)
                {
                    return rundefensiveFilter;
                }
            }
            else if (filter.Name == "PassDefenseEFF")
            {
                DBFilter passdefensiveFilter =GetPassDefenseEffFilter(scFilterList);

                if (passdefensiveFilter != null && passdefensiveFilter.Count > 0)
                {
                    return passdefensiveFilter;
                }
            }
            return filter;
        }

        #endregion


        #region subfunctions for eff filter Run/pass Eff filter
        public DBFilter GetRunDefenseEffFilter(ScFilterList scFilterList)
        {
            DBFilter EffFilter = new DBFilter();

            foreach (ScAFilter scFilter in scFilterList.ScFilters)
            {
                if (scFilter.ReportScType != 21 || !(scFilter.Name.EndsWith("-RUNEFF-D"))) continue;   //Run Eff

                for (int i = 0; i < scFilter.Elements.Count; i++)
                {
                    ScFilterElement element = scFilter.Elements[i];

                    Webb.Data.DBCondition condition = new Webb.Data.DBCondition();

                    condition.ColumnName = element.Field;

                    condition.Value = element.Value;

                    condition.FilterType = GetFilterOper(element.Oper);

                    condition.FollowedOperand = this.GetFilterOpr(element.Opr);

                    if (i == scFilter.Elements.Count - 1) condition.FollowedOperand = FilterOperands.Or;

                    condition.IgnoreCase = true;

                    EffFilter.Add(condition);
                }

                EffFilter.PlayAfter = scFilter.PlayAfter == 1;

            }

            EffFilter.IsCustomFilter = true;   //2010-11-1 12:11:46@Simon Add this Code

            EffFilter.Name = "RunDefenseEFF";  //2009-4-29 12:11:46@Simon Add this Code      

            return EffFilter;
        }

        public DBFilter GetPassDefenseEffFilter(ScFilterList scFilterList)
        {
            DBFilter EffFilter = new DBFilter();

            foreach (ScAFilter scFilter in scFilterList.ScFilters)
            {
                if (scFilter.ReportScType != 21 || !(scFilter.Name.EndsWith("-PASSEFF-D"))) continue;   //Pass EFF                     

                for (int i = 0; i < scFilter.Elements.Count; i++)
                {
                    ScFilterElement element = scFilter.Elements[i];

                    Webb.Data.DBCondition condition = new Webb.Data.DBCondition();

                    condition.ColumnName = element.Field;

                    condition.Value = element.Value;

                    condition.FilterType = GetFilterOper(element.Oper);

                    condition.FollowedOperand = this.GetFilterOpr(element.Opr);

                    if (i == scFilter.Elements.Count - 1) condition.FollowedOperand = FilterOperands.Or;

                    condition.IgnoreCase = true;

                    EffFilter.Add(condition);
                }

                EffFilter.PlayAfter = scFilter.PlayAfter == 1;

            }

            EffFilter.IsCustomFilter = true;   //2010-11-1 12:11:46@Simon Add this Code

            EffFilter.Name = "PassDefenseEFF";  //2009-4-29 12:11:46@Simon Add this Code      

            return EffFilter;
        }

        public DBFilter GetRunOffenseEffFilter(ScFilterList scFilterList)
        {
            DBFilter EffFilter = new DBFilter();

            foreach (ScAFilter scFilter in scFilterList.ScFilters)
            {
                if (scFilter.ReportScType != 20 || !(scFilter.Name.EndsWith("-RUNEFF-O"))) continue;

                for (int i = 0; i < scFilter.Elements.Count; i++)
                {
                    ScFilterElement element = scFilter.Elements[i];

                    Webb.Data.DBCondition condition = new Webb.Data.DBCondition();

                    condition.ColumnName = element.Field;

                    condition.Value = element.Value;

                    condition.FilterType = GetFilterOper(element.Oper);

                    condition.FollowedOperand = this.GetFilterOpr(element.Opr);

                    if (i == scFilter.Elements.Count - 1) condition.FollowedOperand = FilterOperands.Or;

                    condition.IgnoreCase = true;

                    EffFilter.Add(condition);
                }

                EffFilter.PlayAfter = scFilter.PlayAfter == 1;

            }

            EffFilter.IsCustomFilter = true;   //2010-11-1 12:11:46@Simon Add this Code

            EffFilter.Name = "RunOffenseEFF";  //2009-4-29 12:11:46@Simon Add this Code      

            return EffFilter;
        }

        public DBFilter GetPassOffenseEffFilter(ScFilterList scFilterList)
        {
            DBFilter EffFilter = new DBFilter();

            foreach (ScAFilter scFilter in scFilterList.ScFilters)
            {
                if (scFilter.ReportScType != 20 || !(scFilter.Name.EndsWith("-PASSEFF-O"))) continue;

                for (int i = 0; i < scFilter.Elements.Count; i++)
                {
                    ScFilterElement element = scFilter.Elements[i];

                    Webb.Data.DBCondition condition = new Webb.Data.DBCondition();

                    condition.ColumnName = element.Field;

                    condition.Value = element.Value;

                    condition.FilterType = GetFilterOper(element.Oper);

                    condition.FollowedOperand = this.GetFilterOpr(element.Opr);

                    if (i == scFilter.Elements.Count - 1) condition.FollowedOperand = FilterOperands.Or;

                    condition.IgnoreCase = true;

                    EffFilter.Add(condition);
                }

                EffFilter.PlayAfter = scFilter.PlayAfter == 1;

            }

            EffFilter.IsCustomFilter = true;   //2010-11-1 12:11:46@Simon Add this Code

            EffFilter.Name = "PassOffenseEFF";  //2009-4-29 12:11:46@Simon Add this Code      

            return EffFilter;
        }
        #endregion


        public SectionFilterCollection GetReportFilters(ScFilterList scFilterList)
		{
			SectionFilterCollection sectionFilters = new SectionFilterCollection();

            foreach (ScAFilter scFilter in scFilterList.ScFilters)
            {
                sectionFilters.Add(this.GetReportFilter(scFilter));
            }

            #region Run/Pass Off/Def filters
            //DBFilter filter = this.GetRunOffenseEffFilter(scFilterList);

            //SectionFilter scFilter = new SectionFilter(filter);

            //scFilter.FilterName = filter.Name;

            //sectionFilters.Add(scFilter);

            //filter = this.GetPassOffenseEffFilter(scFilterList);

            //scFilter = new SectionFilter(filter);

            //scFilter.FilterName = filter.Name;

            //sectionFilters.Add(scFilter);

            //filter = this.GetRunDefenseEffFilter(scFilterList);

            //scFilter = new SectionFilter(filter);

            //scFilter.FilterName = filter.Name;

            //sectionFilters.Add(scFilter);

            //filter = this.GetPassDefenseEffFilter(scFilterList);

            //scFilter = new SectionFilter(filter);

            //scFilter.FilterName = filter.Name;

            //sectionFilters.Add(scFilter);
            #endregion

            return sectionFilters;
		}
		public SectionFilterCollection GetAllCustomReportFilters(ScFilterList scFilterList)
		{
			SectionFilterCollection sectionFilters = new SectionFilterCollection();

			foreach(ScAFilter scFilter in scFilterList.ScFilters)
			{
                if (scFilter.ReportScType > 0 && scFilter.ReportScType < 5) continue;

				sectionFilters.Add(this.GetReportFilter(scFilter));
			}

			return sectionFilters;
		}

		public SectionFilter GetReportFilter(ScAFilter scFilter)
		{
			SectionFilter sectionFilter = new SectionFilter();

			if(scFilter == null) return sectionFilter;	//Modified at 2008-11-24 15:54:17@Scott

			sectionFilter.FilterName = scFilter.Name;

			foreach(ScFilterElement element in scFilter.Elements)
			{
				Webb.Data.DBCondition condition = new Webb.Data.DBCondition();
              
                condition.ColumnName = element.Field;

                condition.Value = element.Value;

                condition.FilterType = this.GetFilterOper(element.Oper);               

                condition.FollowedOperand = this.GetFilterOpr(element.Opr);
               
                condition.IgnoreCase = true;

                sectionFilter.Filter.PlayAfter = scFilter.PlayAfter == 1;

				sectionFilter.Filter.Add(condition);
			}

            sectionFilter.Filter.IsCustomFilter = false;   //2010-11-1 12:11:46@Simon Add this Code

			sectionFilter.Filter.Name=scFilter.Name;  //2009-4-29 12:11:46@Simon Add this Code

			return sectionFilter;
		}

		public Webb.Data.FilterTypes GetFilterOper(int oper)
		{
			switch(oper)
			{
				case 0:		return FilterTypes.NumEqual;
				case 1:		return FilterTypes.NumNotEqual;
				case 2:		return FilterTypes.NumGreater;
				case 3:		return FilterTypes.NumLess;
				case 4:		return FilterTypes.NumGreaterOrEqual;
				case 5:		return FilterTypes.NumLessOrEqual;
				case 6:		return FilterTypes.StrEqual;
				case 7:		return FilterTypes.StrNotEqual;
				case 8:		return FilterTypes.StrStartWith;
				case 9:		return FilterTypes.StrEndWith;
				case 10:	return FilterTypes.StrInclude;
				case 11:	return FilterTypes.StrNotStarWith;
				case 12:	return FilterTypes.StrNotEndWith;
				case 13:	return FilterTypes.StrNotInclude;
				default:	return FilterTypes.StrEqual;
			}
		}

		public FilterOperands GetFilterOpr(int opr)
		{
			switch(opr)
			{
				case 1: return FilterOperands.Or;
				
				default: return FilterOperands.And;
			}
		}


		public static DBFilter GetAdvFilter(ScFilterList scFilterList,DBFilter Oldfilter)
		{  
            AdvFilterConvertor convert = new AdvFilterConvertor();  

			if(Oldfilter==null||Oldfilter.Name==string.Empty)
			{
				return Oldfilter;
			}

			if(scFilterList==null||scFilterList.ScFilters.Count==0)return Oldfilter;

            if (Oldfilter.IsCustomFilter)
            {
                return convert.UpdateEffFilter(scFilterList, Oldfilter);
            }
		
			ScAFilter scAfilter=scFilterList.GetFilter(Oldfilter.Name);

            if (scAfilter == null)
            {
                return Oldfilter;
            }
            

			SectionFilter  scFilter = convert.GetReportFilter(scAfilter);
			
			if(scFilter.Filter!=null)
			{				
				return scFilter.Filter.Copy();
			}



			return Oldfilter;			
		}


		public static SectionFilterCollection GetCustomFilters(ScFilterList scFilterList,SectionFilterCollection scFilters)
		{
			//2009-5-1 13:33:26@Simon Add this Code

			if(scFilters==null) return scFilters;			

			if(scFilterList==null||scFilterList.ScFilters.Count==0)return scFilters;			

			AdvFilterConvertor convert = new AdvFilterConvertor();

			SectionFilterCollection newscfFilters=new SectionFilterCollection();

			foreach(SectionFilter scFilter in scFilters)
			{
				if(scFilter==null)
				{
					return scFilters;
				}
 
				ScAFilter scAfilter=scFilterList.GetFilter(scFilter.FilterName);

				SectionFilter  secFilter=new SectionFilter();                

				if(scAfilter!=null&&scFilter.Filter.Name!=string.Empty)
				{
					if(!scFilter.Filter.IsCustomFilter)
					{
						secFilter= convert.GetReportFilter(scAfilter);
					}
					else
					{
						secFilter.Apply(scFilter);
					}					
				}
				else
				{
					secFilter.Apply(scFilter);
				}
				
				newscfFilters.Add(secFilter);
			}		
						
			return newscfFilters;
		}

	}
	#endregion
}