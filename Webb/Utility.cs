/***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:Utility.cs
 * Author:Country.Wu [EMail:Webb.Country.Wu@163.com]
 * Create Time:10/30/2007 09:41:49 AM
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
using System.IO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Text;
using System.ComponentModel;
using System.Reflection;
using System.Drawing;
using System.Drawing.Imaging;

using Microsoft.Win32;

namespace Webb
{
	public enum ReportFileType
	{
		Report=0,
		TemplateFile,
	    InwFile,
		BaseXmlFile,
	    WebbDataFile,	   
	}


	#region public class Utility
	public class Utility
	{
		public const double ConvertCoordinate = 1.044;
		public const string WebbDataSource = "ReportDataSource";
		public const string WebbDataAdapter = "DataAdapter";
		public const string WebbReportExt = "wrtf";
		public const string ReportExt = "repx";
		public const string BrowserExt = "inw";
		public const string WebbDataExt = "wrdf";
		public const string SectionFiltersPath = "Section Filters";
		public const string ExControlStylesPath = "Styles";
		public const int ControlMaxHeight = 1000;
		public static object SycObject = new object();
		public	static Font GlobalFont = new Font("Arial", 8f);
		public  static string CurFileName = string.Empty;
		public static bool CancelPrint = false;

        public const string InvalidSignsInFileName="/\\.\"'|<>*?:,@;#`";
        public const string SimpleInFileName = "\"'|<>*?,@;#`";


		/// <summary>
		/// 0 - designer, 1 - browser, 2 - module for Coach-CRM
		/// </summary>
		public  static int CurReportMode = 0;	 
		private static string m_vector	= "webb1986";//Encoding.ASCII.GetBytes("webb1986");
		private static string m_keys	= "2006webb";//Encoding.ASCII.GetBytes("2006webb");

		private Utility()
		{
			//
			// TODO: Add constructor logic here
			//
		}

        public static string ApplicationDirectory
        {
            get 
            {
                System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();

                string strPath = System.IO.Path.GetDirectoryName(asm.Location);

                if (strPath.ToLower().IndexOf(@"file:///") >= 0) { strPath = strPath.Substring(8, strPath.Length - 8); }

                if (!strPath.EndsWith(@"\")) strPath = strPath + @"\";

                return strPath;
            }
        }

		#region Static functions
		static public string GetVersionString()
		{
			return string.Format("Version: {0}",Webb.Assembly.Version);
		}
        public static string SetFilterText( Webb.Data.FilterTypes filterType)
        {
            string strText = string.Empty;

            switch (filterType)
            {
                case Webb.Data.FilterTypes.InArray:
                          strText="is empty";
                          break;                 

            }
            return strText;
        }
        public static void TranslateFilterType(System.Windows.Forms.ComboBox comboBox)
        {
            comboBox.Items.Clear();
            
            comboBox.Items.Add("is empty");
            comboBox.Items.Add("is not empty");
            comboBox.Items.Add("= (Equal)");            
            comboBox.Items.Add("!=(Not Equal)");
            comboBox.Items.Add(">(Greater than)");
             comboBox.Items.Add(">=(GreaterOrEqual)");
            comboBox.Items.Add("<(Less than)");           
            comboBox.Items.Add("<=(LessOrEqual)");
            comboBox.Items.Add("is null");
            comboBox.Items.Add("is not null");
            comboBox.Items.Add("|(in a array)");
            comboBox.Items.Add("&(Include)");
            comboBox.Items.Add("!&(Not include)");
            comboBox.Items.Add("!$(Not EndWith)");
            comboBox.Items.Add("!^(Not StartWith)");
            comboBox.Items.Add("$((End With)");
            comboBox.Items.Add("^(Start With)");

        // NumEqual = 0,
        //NumNotEqual = 1,
        //NumGreater = 2,
        //NumLess = 3,
        //NumGreaterOrEqual = 4,
        //NumLessOrEqual = 5,
        ////
        //StrEqual = 6,
        //StrNotEqual = 7,
        //StrStartWith = 8,
        //StrEndWith = 9,
        //StrInclude = 10,
        //StrNotStarWith = 11,
        //IsStrEmpty = 17,
        //IsNotStrEmpty = 18,
        ////
        //StrNotEndWith = 12,
        //StrNotInclude = 13,
        //InArray = 14,
        //IsDBNull = 15,
        //IsNotDBNull = 16,
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="str_inString"></param>
		/// <returns></returns>
		static public string GetMD5(string i_data)
		{
			byte[] m_datas	= Encoding.ASCII.GetBytes(i_data);
			MD5CryptoServiceProvider m_MD5 = new MD5CryptoServiceProvider();
			byte[] m_value	= m_MD5.ComputeHash(m_datas);
			m_MD5.Clear();
			return BitConverter.ToString(m_value);
		}//End function:GetMD5(),ConverToByteArray();

		/// <summary>
		/// 
		/// </summary>
		/// <param name="i_email"></param>
		/// <returns></returns>
		static public bool IsEMailAddress(string i_email)
		{
			Regex  m_reg	= new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
			return m_reg.IsMatch(i_email);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="i_phoneNumber"></param>
		/// <returns></returns>
		static public bool IsTelNumber_US(string i_phoneNumber)
		{
			Regex  m_reg	= new Regex(@"((\(\d{3}\)?)|(\d{3}-))?\d{3}-\d{4}");
			return m_reg.IsMatch(i_phoneNumber);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="i_fileName"></param>
		/// <returns></returns>
		static public bool IsFileName(string i_fileName)
		{
			Regex  m_reg = new Regex(@"[/\\:*?<>|]+");
			return !m_reg.IsMatch(i_fileName);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="i_siezNum"></param>
		/// <returns></returns>
		public static string ConvertToFileSize(long i_siezNum)
		{
			int m_rank				= 0;
			float m_decimal			= i_siezNum;
			while(m_decimal/1024>1)
			{
				m_rank++;
				m_decimal	= m_decimal/1024;
			}
			m_decimal		= (float)Math.Round(m_decimal,2);
			switch(m_rank)
			{
				default:
				case 0: return m_decimal.ToString()+ " Bytes";
				case 1: return m_decimal.ToString()+ " KB";
				case 2: return m_decimal.ToString()+ " MB";
				case 3: return m_decimal.ToString()+ " GB";
				case 4: return m_decimal.ToString()+ " TB";
				case 5: return m_decimal.ToString()+ " EB";
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="i_siezNum"></param>
		/// <returns></returns>
		public static string FormatLong(long i_siezNum)
		{
			NumberFormatInfo m_NumberInfo		= new NumberFormatInfo();
			m_NumberInfo.NumberDecimalDigits	= 0;
			return(i_siezNum.ToString("N",m_NumberInfo)+" Bytes");
		}

		public static string Encrypt(string i_data)
		{
			return Encrypt(Utility.m_keys,Utility.m_vector,i_data);
		}
		/// <summary>
		/// DES encrypt.
		/// </summary>
		/// <param name="i_key"></param>
		/// <param name="i_IV"></param>
		/// <param name="i_data"></param>
		/// <returns></returns>
		public static string Encrypt(string i_key,string i_IV,string i_data)
		{
			byte[] m_keys	= Encoding.ASCII.GetBytes(i_key);
			byte[] m_IVs	= Encoding.ASCII.GetBytes(i_IV);
			byte[] m_data	= Encoding.ASCII.GetBytes(i_data);
			DESCryptoServiceProvider m_DES	= new DESCryptoServiceProvider();
			ICryptoTransform m_encrypt		= m_DES.CreateEncryptor(m_keys,m_IVs);
			byte[] m_result	= m_encrypt.TransformFinalBlock(m_data,0,m_data.Length);
			m_encrypt.Dispose();
			m_DES.Clear();
			return BitConverter.ToString(m_result);
		}

		public static string Decrypt(string i_data)
		{
			return Decrypt(Utility.m_keys,Utility.m_vector,i_data);
		}
		/// <summary>
		/// DES descrypt.
		/// </summary>
		/// <param name="i_key">Keys</param>
		/// <param name="i_IV">initial vector</param>
		/// <param name="i_data">Data</param>
		/// <returns></returns>
		public static string Decrypt(string i_key,string i_IV,string i_data)
		{
			string[] m_datas	= i_data.Split('-');
			byte[] m_values		= new byte[m_datas.Length];
			Int32Converter m_i32Converter	= new Int32Converter();
			for(int i=0;i<m_datas.Length;i++)
			{
				m_values[i]		= Convert.ToByte(m_i32Converter.ConvertFromInvariantString("0x"+m_datas[i]).ToString());
			}
			byte[] m_keys	= Encoding.ASCII.GetBytes(i_key);
			byte[] m_IVs	= Encoding.ASCII.GetBytes(i_IV);
			byte[] m_data	= Encoding.ASCII.GetBytes(i_data);
			DESCryptoServiceProvider m_DES	= new DESCryptoServiceProvider();
			ICryptoTransform m_decrypt		= m_DES.CreateDecryptor(m_keys,m_IVs);
			byte[] m_result	= m_decrypt.TransformFinalBlock(m_values,0,m_values.Length);
			m_decrypt.Dispose();
			m_DES.Clear();
			return Encoding.ASCII.GetString(m_result);
		}
		#endregion
		
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public static bool IsDigitalCode(int i_keyValue)
		{
			return i_keyValue>=48&&i_keyValue<=57;
		}

		public static string FormatIndicator(int nIndicator)
		{
			return string.Format("{0:00}.",nIndicator);
        }

        #region Convert CCRM data into TimeSpan/demeric
        static public int SubStringCount(string WithinString, string search)
        {
            int counter = 0;

            int index = WithinString.IndexOf(search, 0);

            while (index >= 0 && index < WithinString.Length)
            {
                counter++;
                index = WithinString.IndexOf(search, index + search.Length);
            }
            return counter;
        }

        public static bool IsTimeFormatForOut(System.Data.DataTable i_Table, Webb.Collections.Int32Collection i_Rows,string fieldName)
        {
            bool bResult = false;

            if (!i_Table.Columns.Contains(fieldName)) return false;

            foreach (int i_RowIndex in i_Rows)
            {
                object objValue = i_Table.Rows[i_RowIndex][fieldName];

                if (objValue == null || objValue is System.DBNull) continue;

                string strValue=objValue.ToString();

                if (strValue.Contains(":"))
                {
                    return true;
                }               
            }

            return bResult;
        }

        public static TimeSpan ConvertToTimeTicks(string i_String)
        {
            i_String = i_String.Trim();

            if (i_String.StartsWith(".")) i_String = "0" + i_String;

            int ColonCount = SubStringCount(i_String, ":");

            if (ColonCount == 0)
            {          
                  decimal dValue=0;

                  bool isSuccess = decimal.TryParse(i_String, out dValue);

                  if (dValue >= 60)
                  {      
                      int Millseconds =(int)((dValue - (int)dValue)*1000);

                      int IntValue=(int)dValue;

                      int seconds=IntValue % 60; 

                      int minute=IntValue/60;

                      int hour = minute / 60;

                      minute = minute % 60;
                       
                      //1:20:30

                      TimeSpan ts1 = new TimeSpan(0,hour, minute, seconds, Millseconds);

                      return ts1;
                  }
              
            }

            // 10-31-2011 Scott
            if (ColonCount == 2)
            {
                try
                {
                    int indexFirstColon = i_String.IndexOf(":");

                    int nHour = int.Parse(i_String.Substring(0, indexFirstColon));

                    if(nHour >= 24)
                    {
                        i_String = string.Format("{0}.{1}{2}", (nHour / 24).ToString(), (nHour % 24).ToString(), i_String.Remove(0, indexFirstColon));
                    }
                }
                catch
                {

                }
            }
            // end

            for (int i = 0; i < 2 - ColonCount; i++)
            {
                i_String = "0:" + i_String;
            }
            try
            {
                TimeSpan timeSpan = TimeSpan.Parse(i_String);

                return timeSpan;
            }
            catch
            {
                return new TimeSpan(0);
            }

        }

        public static decimal ConvertFeetInchToNum(string i_String)
        {
             string allfeetInchString = i_String.Trim();

             if (allfeetInchString == string.Empty) return 0m;         
          
            //4FT10 5/16IN

             decimal iNumber = 0m;

             int computeNum = 0;

             int sepratorIndex =i_String.IndexOf("FT");

             if (sepratorIndex >= 0)
             {
                 string StrfeetNumber = i_String.Substring(0, sepratorIndex);

                 if (StrfeetNumber != string.Empty)
                 {
                     int.TryParse(StrfeetNumber, out computeNum);

                     iNumber += 12 * computeNum;
                 }

                 allfeetInchString = allfeetInchString.Remove(0,sepratorIndex+2);
             }

             sepratorIndex = allfeetInchString.IndexOf(" ");

             if (sepratorIndex >= 0)
             {
                 string StrInchNumber = allfeetInchString.Substring(0, sepratorIndex);

                 if (StrInchNumber != string.Empty)
                 {
                     int.TryParse(StrInchNumber, out computeNum);

                     iNumber += computeNum;
                 }

                 allfeetInchString = allfeetInchString.Remove(0, sepratorIndex + 1);

                 allfeetInchString = allfeetInchString.Trim();
             }

             allfeetInchString = allfeetInchString.Replace("IN", "");

             sepratorIndex = allfeetInchString.IndexOf(@"/");

             if (sepratorIndex >= 0)
             {
                 string StrNumber = allfeetInchString.Substring(0, sepratorIndex);

                 int firstNumber = 0,lastNum=1;

                 if (StrNumber != string.Empty)
                 {
                     int.TryParse(StrNumber, out firstNumber);
                 }

                 StrNumber = allfeetInchString.Substring(sepratorIndex + 1).Trim();

                 if (StrNumber != string.Empty)
                 {
                     int.TryParse(StrNumber, out lastNum);
                 }

                 if (lastNum != 0)
                 {
                     iNumber += firstNumber / (1.0m * lastNum);
                 }                
             }
             return iNumber;
        }

        public static string FormatFeetInchData(decimal dFeetInchNum)
        {
            int feetNum = (int)dFeetInchNum / 12;

            decimal inchNum = dFeetInchNum - 12 * feetNum;

            string strValue=string.Format("{0}' {1:0.0#}\"",feetNum,inchNum);

            if (inchNum == (int)inchNum)
            {
                strValue=string.Format("{0}' {1}\"",feetNum,inchNum);
            }

            return strValue;
    
        }

        #endregion


        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        public static extern bool SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);

        public static void GarbageCollect()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        public static void FlushMemory()
        {
            GarbageCollect();

            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            }
        }



        public static Image ReadImageFromPath(string strFilename)
        {
            if (!File.Exists(strFilename)) return null;
         
            try
            {
                FileStream imgFs = new FileStream(strFilename, FileMode.Open, FileAccess.Read);

                int byteLength = (int)imgFs.Length;

                byte[] byteArray = new byte[byteLength];

                imgFs.Read(byteArray, 0, byteLength);

                imgFs.Close();

                imgFs.Dispose();

                Image img = Image.FromStream(new MemoryStream(byteArray));

                using (ImageTypeConverter convert = new ImageTypeConverter())
                {
                    convert.ConvertToJPG(ref img);
                }              

                return img;
            }
            catch(Exception ex)
            {
                Webb.Utilities.MessageBoxEx.ShowMessage("faild to read image!.\n"+ex.Message);

                return null;
            }

           

        }

		public static string GetCurFileName()
		{
			if(System.IO.File.Exists(CurFileName))
			{
				int lastBackslash = CurFileName.LastIndexOf(@"\");

				int lastDash = CurFileName.LastIndexOf(@".");

				if(lastBackslash > 0 && lastDash > 0 && lastDash > lastBackslash)
				{
					return CurFileName.Substring(lastBackslash + 1,lastDash - lastBackslash);
				}
			}

			return string.Empty;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="i_value"></param>
		/// <returns></returns>
		public static bool IsDigital(string i_value)
		{
			System.Diagnostics.Trace.WriteLine(i_value);
			Regex m_reg = new Regex(@"^-?\d*$");
			return m_reg.IsMatch(i_value);
		}

		public static void InitFileDialog(System.Windows.Forms.FileDialog fileDialog, string fileName) 
		{
			fileDialog.Filter = String.Format("Report Files (*.{0})|*.{1}|" +
				"All Files (*.*)|*.*", ReportExt, ReportExt);
			try 
			{
				if(fileName != null && fileName.Length > 0) 
				{
					string s = System.IO.Path.GetDirectoryName(fileName);

					if(s.Length > 0) fileDialog.InitialDirectory = s;
				}
			} 
			catch {}
		}

		public static void InitWebbOpenFileDialog(System.Windows.Forms.FileDialog fileDialog, string fileName) 
		{
			fileDialog.Filter = String.Format("Report Files (*.{0},*.repw)|*.{1};*.repw");
			try 
			{
				if(fileName != null && fileName.Length > 0) 
				{
					string s = System.IO.Path.GetDirectoryName(fileName);

					if(s.Length > 0) fileDialog.InitialDirectory = s;
				}
			} 
			catch {}
		}
		public int ParseRGB(Color color)
		{
			return (int)(((uint)color.B << 16) | (ushort)(((ushort)color.G << 8) | color.R));
		}
		#region Function For Write/Check feather about wizard
		public static void WriteWizardInRepxFile(string strFile)
		{
			System.Text.StringBuilder sbText = new System.Text.StringBuilder();

			string strLine = string.Empty;
			
			StreamReader reader = new StreamReader(strFile,System.Text.Encoding.Default);  

			string strCheckLine = "<AssemblyFullName>Webb.Reports";           

            string strWriteLine = "<AssemblyFullName>Webb.WebbRepWizard|" + Webb.Assembly.WizardInfoVersion + "|" + Webb.Assembly.Version+"|";
			
			while(reader.Peek()>=0)
			{
				strLine = reader.ReadLine();

				int index=strLine.IndexOf(strCheckLine);

				if(index>=0)
				{
					strLine =strLine.Replace(strCheckLine,strWriteLine);
				}
	
				sbText.AppendFormat("{0}\r\n",strLine);	
				
			}

			reader.Close();   
			
			File.Delete(strFile);

			StreamWriter writer = new StreamWriter(strFile,false,System.Text.Encoding.Default);
 
			writer.Write(sbText.ToString());
			
			writer.Close();
		}

		public static bool IsCreatedByWizard(string strFile)
		{	
			if(!System.IO.File.Exists(strFile))	return false;
	
			string strJudge="<AssemblyFullName>";

			string strCheckLine = "<AssemblyFullName>Webb.WebbRepWizard";
			
			StreamReader reader = new StreamReader(strFile,System.Text.Encoding.Default);   

			string strLine=string.Empty;
			
			while(reader.Peek()>=0)
			{
				strLine = reader.ReadLine();

				int index=strLine.IndexOf(strJudge);				

				if(index>=0)
				{
					int judgeIndex=strLine.IndexOf(strCheckLine);	

					if(judgeIndex>=0)
					{
						reader.Close();

						return true;
					}
					else
					{
						reader.Close();

						return false;
					}	
				}			
				
			}
			reader.Close();

			return false;
		}

		#endregion
 
		public static void ReplaceRefPathInRepxFile(string strFile,string exePath)
		{
			System.Text.StringBuilder sbText = new System.Text.StringBuilder();

			int row = 1;

			string strLine = string.Empty;
			
			StreamReader reader = new StreamReader(strFile,System.Text.Encoding.Default);   
			
			while(reader.Peek()>=0)
			{
				strLine = reader.ReadLine();

				if(CheckLine(strLine))
				{
					strLine = ReplaceRefDllPath(strLine,exePath);
				}
	
				sbText.AppendFormat("{0}\r\n",strLine);
		
				row++;
			}

			reader.Close();   
			
			File.Delete(strFile);

			StreamWriter writer = new StreamWriter(strFile,false,System.Text.Encoding.Default);
 
			writer.Write(sbText.ToString());
			
			writer.Close();
		}	

		private static bool CheckLine(string strLine)
		{
			if(strLine.StartsWith(@"///     <Reference Path="))
			{
                strLine = strLine.ToLower();    //2010-3-16 8:59:41@Simon Add this Code

				if(strLine.IndexOf(@"devexpress") >= 0 ||
					strLine.IndexOf(@"webb") >= 0 ||
					strLine.IndexOf(@"movieplayerax") >= 0 ||
					strLine.IndexOf(@"wqpactivelib") >= 0)
				{
					return true;
				}
			}
			return false;
		}

		private static string ReplaceRefDllPath(string strLine,string exePath)
		{
           
			int indexStart = strLine.IndexOf("\"");

			int indexEnd = strLine.LastIndexOf("\\");


			string oldPath = strLine.Substring(indexStart + 1,indexEnd - indexStart - 1);             

			int index = exePath.LastIndexOf("\\");

            if (exePath == string.Empty)
            {
                return strLine.Replace(oldPath+@"\",string.Empty);
            }

            return strLine.Replace(oldPath,exePath.Substring(0,index).ToLower());
		}

		public static int Lcm(Webb.Collections.Int32Collection arr)
		{
			int nRet = 1;

			foreach(int i in arr)                      
			{
				nRet = m(nRet,i);
			}

			return nRet;
		}

		public static int f(int a, int b)
		{ 
			if(b==0)return a;else return f(b,a%b); 
		} 

		public static int m(int a, int b)
		{ 
			return a * b / f(a, b); 
		}

		public static bool IsNumeric(string str) //2009-12-8 16:16:26@Simon Add this Code
		{ 
  			 return System.Text.RegularExpressions.Regex.IsMatch(str, @"^[+-]?\d*[.]?\d*$");		
		
		}
        public static string GetFontDisplayText(Font font)
        {
            string boldOrItalic = "Regular";

            if (font.Bold && font.Italic)
            {
                boldOrItalic = "Bold Italic";
            }
            else if (font.Bold)
            {
                boldOrItalic = "Bold";
            }
            else if (font.Italic)
            {
                boldOrItalic = "Italic";
            }

            return string.Format("{0},{1},{2}", font.FontFamily.Name, boldOrItalic, font.Size);
        }

        #region Registry
        public static void WriteWizardVersion(string TargetPath)
		{
			try
			{
				RegistryKey WebbReg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Webb Electronics\\WebbReport\\WebbReportWizard\\",true);
			
				if(WebbReg==null)
				{
                    WebbReg = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Webb Electronics\\WebbReport\\WebbReportWizard\\");
				}
				
				WebbReg.SetValue("Version", Webb.Assembly.WizardInfoVersion);

				WebbReg.SetValue("WorkDirectory", TargetPath);
				
				WebbReg.Close();
			}
			catch (Exception ex)
			{
				Webb.Utilities.MessageBoxEx.ShowError(ex.Message);
			}
		}

        public static void RemoveWriteWizardVersion()
        {           
            try
            {
                 Registry.CurrentUser.DeleteSubKeyTree("SOFTWARE\\Webb Electronics\\WebbReport\\WebbReportWizard\\");               
            }
            catch (Exception ex)
            {
                Webb.Utilities.MessageBoxEx.ShowError("error:"+ex.Message);
            }
        }

        public static bool CheckInstallNet35Sp1()
        {
            try
            {
                RegistryKey WebbReg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\v3.5\", false);

                if (WebbReg == null)
                {
                    return false;
                }

                object isInstall = WebbReg.GetValue("Install", 0);

                object isSp1Install = WebbReg.GetValue("SP", 0);

                if (isInstall == null || isSp1Install == null)
                {
                    return false;
                }

                int installValue =System.Convert.ToInt32(isInstall);

                int sp1value = System.Convert.ToInt32(isSp1Install);

                if (installValue < 1 || sp1value < 1)
                {
                    return false;
                }  

                return true;
            }
            catch
            {
                return false;
            }
        }
       #endregion
	}
	#endregion

	#region public class CollectionTemplate
	/*Descrition:   Template for specified collection.*/
	public class CollectionTemplate : CollectionBase
	{
		//Wu.Country@2007-10-30 12:41 PM added this collection.
		//Fields
		//Properties
		public object this[int i_Index]
		{
			get{return this.InnerList[i_Index] as object;}
			set{this.InnerList[i_Index] = value;}
		}
		//ctor
		public CollectionTemplate()
		{
		}
		//Methods
		public int Add(object i_Object)
		{
			return this.InnerList.Add(i_Object);
		}
		void Remove(object i_Object )
		{
			this.InnerList.Remove(i_Object);
		}
	}
	#endregion

	//06-03-2008@Scott
	#region public class PropertyManager
	public class PropertyManager
	{
		public PropertyManager()
		{

		}

		public void SetPropertyVisibility(object obj, string strPropertyName, bool bVisible)   
		{   
			Type type = typeof(System.ComponentModel.BrowsableAttribute);

			PropertyDescriptorCollection props = TypeDescriptor.GetProperties(obj);

			AttributeCollection attrs = props[strPropertyName].Attributes;
   
			FieldInfo fieldInfo = type.GetField("browsable", BindingFlags.Instance|BindingFlags.NonPublic|BindingFlags.CreateInstance);

			fieldInfo.SetValue(attrs[type], bVisible);
		}
    
		public void   SetPropertyReadOnly(object obj, string strPropertyName, bool bReadOnly)   
		{   
			Type type = typeof(System.ComponentModel.ReadOnlyAttribute);

			PropertyDescriptorCollection props = TypeDescriptor.GetProperties(obj);
		
			AttributeCollection attrs = props[strPropertyName].Attributes;

			FieldInfo fieldInfo = type.GetField("isReadOnly", BindingFlags.Instance|BindingFlags.NonPublic|BindingFlags.CreateInstance);

			fieldInfo.SetValue(attrs[type], bReadOnly);
		}

		public void SetPropertyVisibility(Type objType, string strPropertyName, bool bVisible)   
		{   
			Type type = typeof(System.ComponentModel.BrowsableAttribute);

			PropertyDescriptorCollection props = TypeDescriptor.GetProperties(objType);

			AttributeCollection attrs = props[strPropertyName].Attributes;
   
			FieldInfo fieldInfo = type.GetField("browsable", BindingFlags.Instance|BindingFlags.NonPublic|BindingFlags.CreateInstance);

			fieldInfo.SetValue(attrs[type], bVisible);
		}
    
		public void   SetPropertyReadOnly(Type objType, string strPropertyName, bool bReadOnly)   
		{
			Type type = typeof(System.ComponentModel.ReadOnlyAttribute);

			PropertyDescriptorCollection props = TypeDescriptor.GetProperties(objType);
		
			AttributeCollection attrs = props[strPropertyName].Attributes;

			FieldInfo fieldInfo = type.GetField("isReadOnly", BindingFlags.Instance|BindingFlags.NonPublic|BindingFlags.CreateInstance);

			fieldInfo.SetValue(attrs[type], bReadOnly);
		}
	}
	#endregion

	//06-11-2008@Scott
	public class ImageTypeConverter : IDisposable
	{
		static object SyncObject = new object();

		public ImageTypeConverter()
		{
			
		}
	
		public void ConvertToJPG(ref Image image)
		{
			if(image.RawFormat == ImageFormat.Jpeg) return;

			lock(SyncObject)
			{
				MemoryStream ms = new MemoryStream();
				
				image.Save(ms,ImageFormat.Jpeg);               

				Image newImage= Image.FromStream(ms);

                image.Dispose();

                image = null;

                image = newImage;

			}
		}
		#region IDisposable Members

		public void Dispose()
		{
			// TODO:  Add ImageTypeConverter.Dispose implementation
		}

		#endregion
	}
}
