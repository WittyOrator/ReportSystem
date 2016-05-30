using System;
using System.Collections.Generic;
using System.Web;
using System.IO;

/// <summary>
///WebbLog 的摘要说明
/// </summary>
public class WebbLog
{
    private FileStream fs;
    private StreamWriter sw;

    private static WebbLog instance;
    public static WebbLog Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new WebbLog();
            }

            return instance;
        }
    }

    public WebbLog()
    {
        
    }

    public void Close()
    {
        if(sw != null)
        {
            sw.Close();
        }

        if(fs != null)
        {
            fs.Close();
        }
    }

    public void WriteLog(string strMsg)
    {
        //string strPath = System.Web.HttpContext.Current.Server.MapPath("");
        //string strFile = strPath + @"\WebbLog.txt";
        //fs = File.Open(strFile, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        //fs.Seek(0, SeekOrigin.End);

        //sw = new StreamWriter(fs);
        //string strMessage = string.Format("{0}\t{1}", DateTime.Now.ToString(), strMsg);

        //sw.WriteLine(strMessage);

        //sw.Flush();

        //if (sw != null)
        //{
        //    sw.Close();
        //}

        //if (fs != null)
        //{
        //    fs.Close();
        //}
    }
}
