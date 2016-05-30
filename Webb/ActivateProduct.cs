using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Webb
{
    #region Keys For Other products
        //   / for media player with capture
        //#define	WMP_CAP_REALKEY	 0x20
        //#define	WMP_CAP_TRIALKEY 0x4F

        //// for media analyzer with capture
        //#define	WMA_CAP_REALKEY	 0x30
        //#define	WMA_CAP_TRIALKEY 0x5F

        //// for media player
        //#define	WMP_REALKEY	     0x50 
        //#define	WMP_TRIALKEY     0x6F

        //// for media analyzer
        //#define	WMA_REALKEY	     0x60 
        //#define	WMA_TRIALKEY     0x7F

        //// for media player basic
        //#define WMP_BASIC_REALKEY  0x24
        //#define WMP_BASIC_TRIALKEY 0x46

        //// for media analyzer with runbookmark
        //#define WMA_RBK_REALKEY  0x34
        //#define WMA_RBK_TRIALKEY 0x56

        //// for media player with runbookmark
        //#define WMP_RBK_REALKEY  0x44
        //#define WMP_RBK_TRIALKEY 0x66

        //// for media player with capture ( dual view )
        //#define	WMP3_CAP_REALKEY  0x22
        //#define	WMP3_CAP_TRIALKEY 0x48

        //// for media analyzer with capture ( dual view )
        //#define	WMA3_CAP_REALKEY  0x32
        //#define	WMA3_CAP_TRIALKEY 0x58

        //// for media player ( dual view )
        //#define	WMP3_REALKEY	  0x52
        //#define	WMP3_TRIALKEY     0x68

        //// for media analyzer ( dual view )
        //#define	WMA3_REALKEY      0x62
        //#define	WMA3_TRIALKEY     0x78

        //// for media analyzer with runbookmark ( dual view )
        //#define WMA3_RBK_REALKEY  0x72
        //#define WMA3_RBK_TRIALKEY 0x38

        //// for victory football
        //#define	WV_FTREALKEY     0x3E
        //#define	WV_FTTRIALKEY	 0x5D

        ////for victory basketball
        //#define	WV_BBREALKEY	 0x4E
        //#define	WV_BBTRIALKEY	 0x6D

        ////for victory volleyball
        //#define WV_VBREALKEY	 0x5E
        //#define	WV_VBTRIALKEY	 0x7D

        ////for victory hockey
        //#define	WV_HKREALKEY	 0x1E
        //#define	WV_HKTRIALKEY	 0x3D

        ////for Lacrosse
        //#define WV_LACREALKEY    0x6E
        //#define WV_LACTRAILKEY   0x2D

        ////for Soccer
        //#define	WV_SOCREALKEY    0X7E
        //#define WV_SOCTRAILKEY   0X1D

        ////for victory basketball ( OEM for GTX ==> GameTape X-Change )
        //#define	VT_BBREALKEY_GTX  0x4A
        //#define	VT_BBTRIALKEY_GTX 0x6B

        //// playbook
        //#define WPB_PLAYBOOKREALKEY	0x3c
        //#define WPB_PLAYBOOKTRAILKEY 0x5c

        //#define WPB_PLAYBOOKLITEREALKEY 0x25
        //#define WPB_PLAYBOOKLITETRAILKEY 0x26

        //// report Wizard
        //#define WRPT_REPOERTWIZARD_REALKEY 0x55
        //#define WRPT_REPOERTWIZARD_TRAILKEY 0x56

    #endregion

    public static class ActivateProductMethods
    {

        public static byte realKey = 0x55;
        public static byte trialKey = 0x56;

        private static string GenerateKey(string strSeed, bool bReal, int nDays)
        {
            byte thebyte;
            byte[] IszKey = new byte[8];
            string strSerial = string.Empty;

            for (int i = 0; i < strSeed.Length; i++)
            {
                thebyte = Asc(strSeed[i]);

                if (bReal)
                {
                    thebyte ^= realKey;
                }
                else
                {
                    thebyte ^= trialKey;
                }

                IszKey[i] = thebyte;
            }

            for (int j = 0; j < 8; j++)
            {
                string strth = IszKey[j].ToString("x");
                if (strth.Length == 1)
                {
                    strSerial += "0" + strth;
                }
                else
                {
                    strSerial += strth;
                }
            }

            if (bReal)  // 10-20-2010 Scott
            {
                nDays = 0;
            }

            strSerial += ToBinary(nDays);
            return strSerial;
        }

        public static bool CheckSerial(string strSeed, string strSerial, out bool bReal, out int nDays)
        {
            bool bRet = false;
            int nDayCount = 8;

            bReal = false;
            nDays = 0;

            if (strSerial != null && strSerial.Length > nDayCount)
            {
                string strKey = GenerateKey(strSeed, true, nDays);

                if (string.Compare(strKey.Remove(strKey.Length - nDayCount), strSerial.Remove(strSerial.Length - nDayCount)) == 0)
                {
                    bReal = true;

                    bRet = true;
                }

                strKey = GenerateKey(strSeed, false, nDays);

                if (string.Compare(strKey.Remove(strKey.Length - nDayCount), strSerial.Remove(strSerial.Length - nDayCount)) == 0)
                {
                    bRet = true;

                    string strDay = strSerial.Substring(strKey.Length - nDayCount);

                    nDays = GetInt(strDay);
                }
            }

            return bRet;
        }

        private static int GetInt(string strBinary)
        {
            int nRet = 0;

            for (int i = 0; i < strBinary.Length; i++)
            {
                char c = strBinary[strBinary.Length - i - 1];

                switch (c)
                {
                    case '0':
                        break;
                    case '1':
                        nRet += (int)Math.Pow(2, i);
                        break;
                    default:
                        return 0;
                }
            }

            return nRet;
        }

        private static byte Asc(char strInput)
        {
            char[] cInput = new char[1];
            cInput[0] = strInput;

            System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
            byte intAsciiCode = Convert.ToByte(asciiEncoding.GetBytes(cInput)[0]);
            return (intAsciiCode);
        }

        private static string ToBinary(int val)
        {
            string strRet = string.Empty;
            for (int i = 7; i >= 0; i--)
            {
                if ((val & (1 << i)) != 0)
                {
                    strRet += "1";
                }
                else
                {
                    strRet += "0";
                }
            }
            return strRet;
        }

    }

    public class RegisterWebbReportProduct
    {
        private const string WRWResgistryKeyString = @"Software\Webb Electronics\WebbReport\WebbReportWizard";

        private RegistryKey _RegistryKey = null;


        public RegisterWebbReportProduct()
        {
            try
            {
                _RegistryKey = Registry.CurrentUser.OpenSubKey(WRWResgistryKeyString, true);

                if (_RegistryKey == null) _RegistryKey = Registry.CurrentUser.CreateSubKey(WRWResgistryKeyString);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Faild!", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                Environment.Exit(-1);
            }
        }


        #region Hide Old 
        //public bool CheckFirstRunInRegistry()
        //{
        //    try
        //    {
        //        object objValue = _RegistryKey.GetValue("Activate");

        //        return objValue != null;

        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
        public void WriteKeysInRegistry(bool isPermanently, int trialDays)
        {
            try
            {
                _RegistryKey.SetValue("Activate", true);

                _RegistryKey.SetValue("IsPermanently", isPermanently);

                _RegistryKey.SetValue("RegisterDay", DateTime.Now.Date.ToShortDateString());

                _RegistryKey.SetValue("lastUseDay", DateTime.Now.Date.ToShortDateString());

                _RegistryKey.SetValue("TrialDays", trialDays);
            }
            catch
            {
            }
        }

        //public void ChangeSimpleLastUse()
        //{
        //    _RegistryKey.SetValue("lastUseDay", DateTime.Now.Date.ToShortDateString());              
        //}

        //public bool ReadKeysInRegistry(out bool isPermanently, out int restlDays)
        //{
        //    isPermanently = false;

        //    restlDays = 0;

        //    try
        //    {

        //        object objVlaue = _RegistryKey.GetValue("IsPermanently");

        //        isPermanently = Convert.ToBoolean(objVlaue);

        //        object objregisterTime = _RegistryKey.GetValue("RegisterDay");

        //        DateTime registereDay = Convert.ToDateTime(objregisterTime);

        //        TimeSpan tsRestTrailDay = DateTime.Now.Date.Subtract(registereDay);

        //        object objTrailDays = _RegistryKey.GetValue("TrialDays");

        //        int trialDays = Convert.ToInt32(objTrailDays);

        //        restlDays = trialDays - (int)tsRestTrailDay.TotalDays;

        //        object objlastuseday = _RegistryKey.GetValue("lastUseDay");

        //        DateTime lastuseday = Convert.ToDateTime(objlastuseday);

        //        if (DateTime.Now.Date < lastuseday)
        //        {
        //            restlDays = -1;
        //        }

        //        return true;

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Faild!", MessageBoxButtons.OK, MessageBoxIcon.Stop);

        //        return false;
        //    }
        //}
        #endregion   
   
        public WebbAcivateCls ReadWebbAcivateClsFromRegistry()
        {
            WebbAcivateCls webbAcivateCls = new WebbAcivateCls();

            try
            {

                object objVlaue = _RegistryKey.GetValue("IsPermanently");

                if (objVlaue == null) return webbAcivateCls;                

                webbAcivateCls.IsPermanently = Convert.ToBoolean(objVlaue);

                webbAcivateCls.Activate = true;

                object objregisterTime = _RegistryKey.GetValue("RegisterDay");

                webbAcivateCls.RegisterDay= Convert.ToDateTime(objregisterTime);
             
                object objTrailDays = _RegistryKey.GetValue("TrialDays");

                webbAcivateCls.TrialDays = Convert.ToInt32(objTrailDays);

                object objlastuseday = _RegistryKey.GetValue("lastUseDay");

                webbAcivateCls.LastUseDay = Convert.ToDateTime(objlastuseday);

                WebbAcivateCls.Save(webbAcivateCls, string.Empty);

                return webbAcivateCls;

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Faild!", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                return webbAcivateCls;
            }
        }
     
        public string GetSerialNumber()
        {
            const int MAX_FILENAME_LEN = 256;
            int retVal = 0;
            int a = 0;
            int b = 0;
            string str1 = null;
            string str2 = null;

            GetVolumeInformation(
                @"C:\",
                str1,
                MAX_FILENAME_LEN,
                ref retVal,
                a,
                b,
                str2,
                MAX_FILENAME_LEN);

            return retVal.ToString("x");
        }


        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetVolumeInformation(
        string lpRootPathName,                      //欲获取信息的那个卷的根路径 
        string lpVolumeNameBuffer,                  //用于装载卷名（卷标）的一个字串 
        int nVolumeNameSize,                        //lpVolumeNameBuffer字串的长度   
        ref int lpVolumeSerialNumber,               //用于装载磁盘卷序列号的变量   
        int lpMaximumComponentLength,               //指定一个变量，用于装载文件名每一部分的长度。例如，在“c:\component1\component2.ext”的情况下，它就代表component1或component2名称的长度 .

        int lpFileSystemFlags,                      //用于装载一个或多个二进制位标志的变量。对这些标志位的解释如下：
            //FS_CASE_IS_PRESERVED                      //文件名的大小写记录于文件系统
            //FS_CASE_SENSITIVE                         //文件名要区分大小写
            //FS_UNICODE_STORED_ON_DISK                 //文件名保存为Unicode格式
            //FS_PERSISTANT_ACLS                        //文件系统支持文件的访问控制列表（ACL）安全机制
            //FS_FILE_COMPRESSION                       //文件系统支持逐文件的进行文件压缩
            //FS_VOL_IS_COMPRESSED                      //整个磁盘卷都是压缩的

        string lpFileSystemNameBuffer,              //指定一个缓冲区,用于装载文件系统的名称（如FAT，NTFS以及其他）
        int nFileSystemNameSize                     //lpFileSystemNameBuffer字串的长度
        );
    }

}