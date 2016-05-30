using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Webb.Data
{
	public class IniFile
	{
		//文件INI名称
		public string Path;

		////声明读写INI文件的API函数 
		[DllImport("kernel32")]
		private static extern long WritePrivateProfileString(string section,string key,string val,string filePath);

		[DllImport("kernel32")]
		private static extern int GetPrivateProfileString(string section,string key,string def,StringBuilder retVal,int size,string filePath);

		//类的构造函数，传递INI文件名
		public IniFile(string inipath)
		{
			//
			// TODO: Add constructor logic here
			//
            // 判断文件是否存在
            FileInfo fileInfo = new FileInfo(inipath);

            //Todo:搞清枚举的用法
            if ((!fileInfo.Exists))
            { 
                //文件不存在，建立文件
                System.IO.StreamWriter sw = new System.IO.StreamWriter(inipath, false, System.Text.Encoding.Default);
                try
                {
                    sw.Write("#ConfigFile");
                    sw.Flush();
                }

                catch
                {
                    throw (new ApplicationException("Ini File not exist"));
                }
                finally
                {
                    sw.Close();
                }
            }


			Path = inipath;
		}

		//写INI文件
		public void IniWriteValue(string Section,string Key,string Value)
		{
			WritePrivateProfileString(Section,Key,Value,this.Path);
		}

		//读取INI文件指定
		public string IniReadValue(string Section,string Key)
		{
			StringBuilder temp = new StringBuilder(255);
			int i = GetPrivateProfileString(Section,Key,"",temp,255,this.Path);
			return temp.ToString();
		}
	}
}
