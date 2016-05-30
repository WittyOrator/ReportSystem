using System;
using System.Runtime.Serialization;
using Webb.Reports.DataProvider;

namespace Webb.Reports
{	
	public interface IWizardSetting
	{		
	}

	#region ReportWizardSetting
	/// <summary>
	/// Summary description for ReportSetting.
	/// </summary>
	[Serializable]
	public class ReportWizardSetting:ISerializable	
	{	
		#region  Fields
		protected byte _TemplateType=0;
		
		protected bool _AddScFilters=false;

		protected bool _AddWatermark=true;	
		
		protected string _SelectedStyleName=string.Empty;		

		protected byte _GroupTemplateType=0;

		protected bool _CreateByWizard=false;

		protected string _HeaderStyleName=string.Empty;

		protected string _GameListStyleName=string.Empty;

        protected string _WizardVersion = Webb.Assembly.WizardInfoVersion;

        protected string _BrowserVersion = Webb.Assembly.Version;

        protected int _ProductTypes =(int)WebbDBTypes.WebbAdvantageFootball;

        protected byte _CategoryDataType = 0;
		#endregion
	
		public ReportWizardSetting()
		{
		}

		#region Serialization By Simon's Macro 2010-2-10 16:06:58
		public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			info.AddValue("_TemplateType",_TemplateType);
			info.AddValue("_AddScFilters",_AddScFilters);
			info.AddValue("_AddWatermark",_AddWatermark);
			info.AddValue("_SelectedStyleName",_SelectedStyleName);
			info.AddValue("_GroupTemplateType",_GroupTemplateType);
			info.AddValue("_CreateByWizard",_CreateByWizard);
			info.AddValue("_HeaderStyleName",_HeaderStyleName);
			info.AddValue("_GameListStyleName",_GameListStyleName);
            info.AddValue("_ProductTypes", _ProductTypes);
            info.AddValue("_CategoryDataType", _CategoryDataType); 
            
		}

		public ReportWizardSetting(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
            try
            {
                _CategoryDataType = info.GetByte("_CategoryDataType");
            }
            catch
            {
                _CategoryDataType = 0;
            }
			try
			{
				_TemplateType=info.GetByte("_TemplateType");
			}
			catch
			{
				_TemplateType=0;
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
				_AddWatermark=info.GetBoolean("_AddWatermark");
			}
			catch
			{
				_AddWatermark=false;
			}
			try
			{
				_SelectedStyleName=info.GetString("_SelectedStyleName");
			}
			catch
			{
				_SelectedStyleName=string.Empty;
			}
			try
			{
				_GroupTemplateType=info.GetByte("_GroupTemplateType");
			}
			catch
			{
				_GroupTemplateType=0;
			}
			try
			{
				_CreateByWizard=info.GetBoolean("_CreateByWizard");
			}
			catch
			{
				_CreateByWizard=false;
			}
			try
			{
				_HeaderStyleName=info.GetString("_HeaderStyleName");
			}
			catch
			{
				_HeaderStyleName=string.Empty;
			}
			try
			{
				_GameListStyleName=info.GetString("_GameListStyleName");
			}
			catch
			{
				_GameListStyleName=string.Empty;
			}
            try
            {
                _ProductTypes = info.GetInt32("_ProductTypes");
            }
            catch
            {
                _ProductTypes = (int)WebbDBTypes.WebbAdvantageFootball;
            }
		}
		#endregion

        #region Proprties

        public byte TemplateType
        {
            get
            {
                return _TemplateType;
            }
            set
            {
                _TemplateType = value;
            }
        }

        public bool AddScFilters
        {
            get
            {
                return _AddScFilters;
            }
            set
            {
                _AddScFilters = value;
            }
        }

        public bool AddWatermark
        {
            get
            {
                return _AddWatermark;
            }
            set
            {
                _AddWatermark = value;
            }
        }

        public string SelectedStyleName
        {
            get
            {
                return _SelectedStyleName;
            }
            set
            {
                _SelectedStyleName = value;
            }
        }

        public byte GroupTemplateType
        {
            get
            {
                return _GroupTemplateType;
            }
            set
            {
                _GroupTemplateType = value;
            }
        }

        public bool CreateByWizard
        {
            get
            {
                return _CreateByWizard;
            }
            set
            {
                _CreateByWizard = value;
            }
        }

        public string HeaderStyleName
        {
            get
            {
                return _HeaderStyleName;
            }
            set
            {
                _HeaderStyleName = value;
            }
        }

        public string GameListStyleName
        {
            get
            {
                return _GameListStyleName;
            }
            set
            {
                _GameListStyleName = value;
            }
        }

        public string WizardVersion
        {
            get
            {
                return _WizardVersion;
            }
            set
            {
                _WizardVersion = value;
            }
        }

        public string BrowserVersion
        {
            get
            {
                return _BrowserVersion;
            }
            set
            {
                _BrowserVersion = value;
            }
        }

        public int ProductTypes
        {
            get
            {
                return _ProductTypes;
            }
            set
            {
                _ProductTypes = value;
            }
        }

        public byte CategoryDataType
        {
            get
            {
                return _CategoryDataType;
            }
            set
            {
                _CategoryDataType = value;
            }
        }
        #endregion

        #region Copy Function By Macro 2011-4-21 14:37:03
        public ReportWizardSetting Copy()
        {
			ReportWizardSetting thiscopy=new ReportWizardSetting();
			thiscopy._TemplateType=this._TemplateType;
			thiscopy._AddScFilters=this._AddScFilters;
			thiscopy._AddWatermark=this._AddWatermark;
			thiscopy._SelectedStyleName=this._SelectedStyleName;
			thiscopy._GroupTemplateType=this._GroupTemplateType;
			thiscopy._CreateByWizard=this._CreateByWizard;
			thiscopy._HeaderStyleName=this._HeaderStyleName;
			thiscopy._GameListStyleName=this._GameListStyleName;
			thiscopy._WizardVersion=this._WizardVersion;
			thiscopy._BrowserVersion=this._BrowserVersion;
			thiscopy._ProductTypes=this._ProductTypes;
			thiscopy._CategoryDataType=this._CategoryDataType;
			return thiscopy;
        }
		#endregion
      
		
		
	}	
	#endregion
}
