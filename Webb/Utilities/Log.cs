/***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:Log.cs
 * Author:Country.Wu [EMail:Webb.Country.Wu@163.com]
 * Create Time:11/14/2007 04:16:33 PM
 * Copyright:1986-2007@Webb Electronics all right reserved.
 * Purpose:
 * ***********************************************************************/
#region History
/*
 * //Author@DateTime : Description
 * */
#endregion History


using System;
using System.IO;

namespace Webb.Utilities
{
	public class TextLog
	{
		private static string M_TextFilePath =  Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"WebbLogs");
		private static DateTime	M_LogTime = DateTime.Now;
		private static string M_FullFileName;
		private static TextWriter M_TextWriter;
		private static object M_SynObj = new object();
		//
		static public string CurrentLogFilePath
		{
			get{return TextLog.M_FullFileName;}
		}
		static public string LogFolder
		{
			get{return TextLog.M_TextFilePath;}
			set
			{
				if(TextLog.M_TextFilePath==value) return;
				if(Directory.Exists(value))
				{
					TextLog.M_TextFilePath = value;
				}
				else
				{
					Directory.CreateDirectory(value);
				}
				TextLog.OnLogFolderChanged();
			}
		}

		//

		private TextLog()
		{
		}

		static TextLog()
		{
			if(!Directory.Exists(TextLog.M_TextFilePath))
			{
				Directory.CreateDirectory(TextLog.M_TextFilePath);
			}
		}

		static public void WriteLine(string i_Msg)
		{
			TextLog.WriteLine(i_Msg,true,true);
		}

		static public void WriteLine(string i_Msg,bool i_AddTime)
		{
			TextLog.WriteLine(i_Msg,i_AddTime,true);
		}

		public static void WriteLine(string i_message,bool i_AddTime,bool i_Flush)
		{
			lock(TextLog.M_SynObj)
			{
				TextLog.CheckLogFileName();
				if(i_AddTime)
				{
					TextLog.M_TextWriter.WriteLine("{0}\t{1}",DateTime.Now,i_message);
				}
				else
				{
					TextLog.M_TextWriter.WriteLine("{0}",i_message);
				}
				if(i_Flush)
				{
					TextLog.M_TextWriter.Flush();
				}
			}
		}

		static private void CheckLogFileName()
		{
			if(DateTime.Now.Date!=M_LogTime.Date)
			{
				TextLog.CloseLogFile();
				TextLog.OpenLogFile();
			}
			if(M_TextWriter==null)
			{
				TextLog.OpenLogFile();
			}
		}

		static private void OpenLogFile()
		{
			if(M_TextWriter==null)
			{
				TextLog.M_LogTime = DateTime.Now;	//Get current data time again.
				string m_FileName = string.Format("{0:yyyy_MM_dd}.log",TextLog.M_LogTime);
				TextLog.M_FullFileName = Path.Combine(M_TextFilePath,m_FileName);
				TextLog.M_TextWriter = File.AppendText(M_FullFileName);
			}
		}

		static private void CloseLogFile()
		{
			if(M_TextWriter!=null)
			{
				try
				{
					TextLog.M_TextWriter.Close();
					TextLog.M_TextWriter = null;
				}
				catch{}
			}
		}

		static private void OnLogFolderChanged()
		{
			lock(M_SynObj)
			{
				TextLog.CloseLogFile();
				TextLog.OpenLogFile();
			}
		}

		static public void WriteException(Exception ex)
		{
			WriteLine(string.Format("Exception:{0};Message:{1}",ex,ex.Message));
			Exception m_ex = ex;
			while(m_ex.InnerException!=null)
			{
				m_ex = m_ex.InnerException;
				WriteLine(string.Format("    Inner Exception:{0};Message:{1}",m_ex,m_ex.Message),false);
			}
			WriteLine(ex.StackTrace,false);
		}
	}
}
