using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


namespace Webb.Data
{
    [Serializable]
    public class CCRMConfigData:ISerializable
    {
        #region Auto Constructor By Macro 2011-3-28 15:35:40
		public CCRMConfigData()
        {
			_ServerAddress=string.Empty;
			_DataSavedLocation=string.Empty;
			_VictoryDBPath=string.Empty;
			_LoginName=string.Empty;
			_LoginPassword=string.Empty;
        }

        public CCRMConfigData(string p_LoginPassword, string p_LoginName, string p_VictoryDBPath, string p_DataSavedLocation, string p_ServerAddress)
        {
			_ServerAddress=p_ServerAddress;
			_DataSavedLocation=p_DataSavedLocation;
			_VictoryDBPath=p_VictoryDBPath;
			_LoginName=p_LoginName;
			_LoginPassword=p_LoginPassword;
        }
		#endregion

       
    
        private static BinaryFormatter _BinaryFormatter;// = new BinaryFormatter();
        private static BinaryFormatter M_BinaryFormatter
        {
            get
            {
                if (_BinaryFormatter == null) _BinaryFormatter = new BinaryFormatter();
                return _BinaryFormatter;
            }
        }   
       
        protected string _ServerAddress = string.Empty;
        protected string _DataSavedLocation = string.Empty;
        protected string _VictoryDBPath = string.Empty;
        protected string _LoginName = string.Empty;
        protected string _LoginPassword = string.Empty;
     

        #region Copy Function By Macro 2010-11-15 10:49:35

        #endregion


        public static CCRMConfigData CreateInitialData(string path)
        {
            CCRMConfigData _CCRMConfigData = new CCRMConfigData();

    
            _CCRMConfigData.ServerAddress = "www.coachescrm.com";

            _CCRMConfigData.DataSavedLocation = path;

              return _CCRMConfigData;
        }

        public static void SaveConfigFiles(CCRMConfigData ccrmData,string i_fileName)
        {
           
            try
            {
                if (System.IO.File.Exists(i_fileName)) System.IO.File.Delete(i_fileName);

               if (ccrmData == null) return;

                using (Stream m_stream = File.Open(i_fileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
                {
                    M_BinaryFormatter.Serialize(m_stream, ccrmData);
                }
            }
            catch (Exception ex)
            {
                Webb.Utilities.MessageBoxEx.ShowError("Failed to save Data\n" + ex.Message);
            }
        }
        public static CCRMConfigData LoadConfig(string i_fileName)
        {
            if (!File.Exists(i_fileName)) return null;
            try
            {  
                using (Stream m_stream = File.Open(i_fileName, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    return M_BinaryFormatter.Deserialize(m_stream) as CCRMConfigData;
                }
             }
            catch (Exception ex)
            {
                Webb.Utilities.MessageBoxEx.ShowError("Failed to load Data\n" + ex.Message);

                return null;
            }

        }

        #region Copy Function By Macro 2011-3-28 15:34:55
		public CCRMConfigData Copy()
        {
			CCRMConfigData thiscopy=new CCRMConfigData();
			thiscopy._ServerAddress=this._ServerAddress;
			thiscopy._DataSavedLocation=this._DataSavedLocation;
			thiscopy._VictoryDBPath=this._VictoryDBPath;
		    thiscopy._LoginName=this._LoginName;
			thiscopy._LoginPassword=this._LoginPassword;
			return thiscopy;
        }
		#endregion

        public string ServerAddress
        {
            get
            {
                return _ServerAddress;
            }
            set
            {
                _ServerAddress = value;
            }
        }

        public string DataSavedLocation
        {
            get
            {
                return _DataSavedLocation;
            }
            set
            {
                _DataSavedLocation = value;
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

        public string LoginName
        {
            get
            {
                return _LoginName;
            }
            set
            {
                _LoginName = value;
            }
        }

        public string LoginPassword
        {
            get
            {
                return _LoginPassword;
            }
            set
            {
                _LoginPassword = value;
            }
        }

        #region Serialization By Simon's Macro 2011-3-28 15:35:54
		public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
			info.AddValue("_ServerAddress",_ServerAddress);
			info.AddValue("_DataSavedLocation",_DataSavedLocation);
			info.AddValue("_VictoryDBPath",_VictoryDBPath);
			info.AddValue("_LoginName",_LoginName);
			info.AddValue("_LoginPassword",_LoginPassword);

        }

        public CCRMConfigData(SerializationInfo info, StreamingContext context)
        {
			try
			{
				_ServerAddress=info.GetString("_ServerAddress");
			}
			catch
			{
				_ServerAddress=string.Empty;
			}
			try
			{
				_DataSavedLocation=info.GetString("_DataSavedLocation");
			}
			catch
			{
				_DataSavedLocation=string.Empty;
			}
			try
			{
				_VictoryDBPath=info.GetString("_VictoryDBPath");
			}
			catch
			{
				_VictoryDBPath=string.Empty;
			}
			try
			{
				_LoginName=info.GetString("_LoginName");
			}
			catch
			{
				_LoginName=string.Empty;
			}
			try
			{
				_LoginPassword=info.GetString("_LoginPassword");
			}
			catch
			{
				_LoginPassword=string.Empty;
			}
        }
		#endregion

    
    }
}
