using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Webb
{
    [Serializable]
    public class WebbAcivateCls:ISerializable
    {
        #region Auto Constructor By Macro 2011-8-11 11:33:17
		public WebbAcivateCls()
        {
			activate=false;
			isPermanently=false;
			trialDays=30;
			registerDay=DateTime.Now.Date;
			lastUseDay=DateTime.Now.Date;
        }

        public WebbAcivateCls(DateTime plastUseDay, DateTime pregisterDay, int ptrialDays, bool pisPermanently, bool pactivate)
        {
			activate=pactivate;
			isPermanently=pisPermanently;
			trialDays=ptrialDays;
			registerDay=pregisterDay;
			lastUseDay=plastUseDay;
        }
		#endregion     
        
        public static string DefaultSavedPath=Webb.Utility.ApplicationDirectory+@"Template\AcivateWRW.aws";

        protected bool activate = false;
        protected bool isPermanently = false;
        protected int trialDays = 30;
        protected DateTime registerDay = DateTime.Now.Date;
        protected DateTime lastUseDay = DateTime.Now.Date;


        public static WebbAcivateCls Load(string strFile)
        {
            if (strFile == string.Empty)
            {
                strFile = DefaultSavedPath;
            }

            WebbAcivateCls webbAcivateCls =new WebbAcivateCls();

            if (System.IO.File.Exists(strFile))
            {
                try
                {
                    using (System.IO.StreamReader stream = new System.IO.StreamReader(strFile))
                    {
                        string strLine = stream.ReadLine();

                        strLine = strLine.Trim();

                        string[] strArray = strLine.Split(",".ToCharArray());

                        webbAcivateCls.Activate = bool.Parse(strArray[0]);

                        webbAcivateCls.IsPermanently = bool.Parse(strArray[1]);

                        webbAcivateCls.RegisterDay = DateTime.Parse(strArray[2]);

                        webbAcivateCls.LastUseDay = DateTime.Parse(strArray[3]);

                        webbAcivateCls.TrialDays = int.Parse(strArray[4]);

                        stream.Close();
                    }
                }
                catch
                {
                }
                
            }
            return webbAcivateCls;

        }

        public static bool Save(WebbAcivateCls webbAcivateCls, string strFile)
        {
            if (strFile == string.Empty)
            {
                strFile = DefaultSavedPath;
            }

            if (System.IO.File.Exists(strFile))
            {
                System.IO.File.Delete(strFile);
            }

            if (webbAcivateCls == null) return false;

            try
            {
                using (System.IO.StreamWriter stream = new System.IO.StreamWriter(strFile,false))
                {
                    StringBuilder sb = new StringBuilder();

                    sb.Append(webbAcivateCls.activate);

                    sb.Append(",");

                    sb.Append(webbAcivateCls.isPermanently);

                    sb.Append(",");

                    sb.Append(webbAcivateCls.registerDay);

                    sb.Append(",");

                    sb.Append(webbAcivateCls.lastUseDay);

                    sb.Append(",");

                    sb.Append(webbAcivateCls.trialDays); 

                    stream.WriteLine(sb.ToString());

                    stream.Flush();

                    stream.Close();
                }
            }
            catch
            {
            }

            return true;
        }

        public void SavePermanentUseSetting()
        {
            this.activate = true;            

            this.registerDay = DateTime.Now.Date;

            this.lastUseDay = DateTime.Now.Date;
           
            Save(this, "");
        }

        public int GetRestDays()
        {                        
            TimeSpan tsRestTrailDay = DateTime.Now.Date.Subtract(registerDay);
          
            int restlDays = trialDays - (int)tsRestTrailDay.TotalDays;
         
            if (DateTime.Now.Date < lastUseDay)
            {
                restlDays = -1;
            }

            return restlDays;
        }

    
        public bool Activate
        {
            get
            {
                return activate;
            }
            set
            {
                activate = value;
            }
        }

        public bool IsPermanently
        {
            get
            {
                return isPermanently;
            }
            set
            {
                isPermanently = value;
            }
        }

        public int TrialDays
        {
            get
            {
                return trialDays;
            }
            set
            {
                trialDays = value;
            }
        }

        public DateTime RegisterDay
        {
            get
            {
                return registerDay;
            }
            set
            {
                registerDay = value;
            }
        }

        public DateTime LastUseDay
        {
            get
            {
                return lastUseDay;
            }
            set
            {
                lastUseDay = value;
            }
        }

        #region Serialization By Simon's Macro 2011-8-11 11:17:39
		public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
			info.AddValue("activate",activate);
			info.AddValue("isPermanently",isPermanently);
			info.AddValue("trialDays",trialDays);
			info.AddValue("registerDay",registerDay,typeof(System.DateTime));
			info.AddValue("lastUseDay",lastUseDay,typeof(System.DateTime));

        }

        public WebbAcivateCls(SerializationInfo info, StreamingContext context)
        {
			try
			{
				activate=info.GetBoolean("activate");
			}
			catch
			{
				activate=false;
			}
			try
			{
				isPermanently=info.GetBoolean("isPermanently");
			}
			catch
			{
				isPermanently=false;
			}
			try
			{
				trialDays=info.GetInt32("trialDays");
			}
			catch
			{
				trialDays=30;
			}
			try
			{
				registerDay=(System.DateTime)info.GetValue("registerDay",typeof(System.DateTime));
			}
			catch
			{
				registerDay=DateTime.Now.Date;
			}
			try
			{
				lastUseDay=(System.DateTime)info.GetValue("lastUseDay",typeof(System.DateTime));
			}
			catch
			{
				lastUseDay=DateTime.Now.Date;
			}
        }
		#endregion
      

       
    }
}
