using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Webb.Data
{
	public class IniFile
	{
		//�ļ�INI����
		public string Path;

		////������дINI�ļ���API���� 
		[DllImport("kernel32")]
		private static extern long WritePrivateProfileString(string section,string key,string val,string filePath);

		[DllImport("kernel32")]
		private static extern int GetPrivateProfileString(string section,string key,string def,StringBuilder retVal,int size,string filePath);

		//��Ĺ��캯��������INI�ļ���
		public IniFile(string inipath)
		{
			//
			// TODO: Add constructor logic here
			//
            // �ж��ļ��Ƿ����
            FileInfo fileInfo = new FileInfo(inipath);

            //Todo:����ö�ٵ��÷�
            if ((!fileInfo.Exists))
            { 
                //�ļ������ڣ������ļ�
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

		//дINI�ļ�
		public void IniWriteValue(string Section,string Key,string Value)
		{
			WritePrivateProfileString(Section,Key,Value,this.Path);
		}

		//��ȡINI�ļ�ָ��
		public string IniReadValue(string Section,string Key)
		{
			StringBuilder temp = new StringBuilder(255);
			int i = GetPrivateProfileString(Section,Key,"",temp,255,this.Path);
			return temp.ToString();
		}
	}
}
