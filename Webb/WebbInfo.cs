/***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:Class1.cs
 * Author:Country.Wu [EMail:Webb.Country.Wu@163.com]
 * Create Time:10/30/2007 09:26:47 AM
 * Copyright:1986-2007@Webb Electronics all right reserved.
 * Purpose:
 * ***********************************************************************/
#region History
/*
 * //Author@DateTime : Description
 * */
#endregion History

using System;

namespace Webb
{
	public class Assembly
	{
		/*
		[assembly: AssemblyTitle("")]
		[assembly: AssemblyProduct("")]
		[assembly: AssemblyDescription(Webb.Basic.Assembly.Description)]
		[assembly: AssemblyConfiguration(Webb.Basic.Assembly.Configuration)]
		[assembly: AssemblyCompany(Webb.Basic.Assembly.Company)]
		[assembly: AssemblyCopyright(Webb.Basic.Assembly.Copyright)]
		[assembly: AssemblyTrademark(Webb.Basic.Assembly.Trademark)]
		[assembly: AssemblyCulture(Webb.Basic.Assembly.Culture)]		
		[assembly: AssemblyVersion(Webb.Basic.Assembly.Version)]
		[assembly: AssemblyDelaySign(Webb.Basic.Assembly.DelaySign)]
		[assembly: AssemblyKeyFile(Webb.Basic.Assembly.KeyFile)]
		[assembly: AssemblyKeyName(Webb.Basic.Assembly.KeyName)]
		*/
		public const string Title = "";
		public const string Description = "Webb .Net applications assembly.";
		public const string Configuration = "MS.Net framwork 2.0";
		public const string Company = "Webb Electronics";
		public const string Product = "Interactive Report";
		public const string Copyright = "Copyright(C)2001-2010";
		public const string Trademark = "Webb";
		public const string Culture = "";

		public const string Version = "1.3.1.5023";    //2010-1-11 11:14:29@Simon Add this Code

		public const string WebbExControlsDllVersion = "1.1.0.0";	//Don't Modify
		public const string WebbReportDllVersion = "1.1.0.0";	//Don't Modify
		public const string WebbDllVersion = "1.0.9.0";	//Don't Modify
		//
		public const bool DelaySign = false;

		public const string KeyFile = "..\\..\\..\\..\\Resource\\Common\\StrongKey.snk";
		public const string KeyName = "Webb.Report.PublicKey";

        public const string WizardVersion = "0.0.0.1";  //Don't Modify

		public const string WizardInfoVersion = "2.1a";  //2010-3-29      
	}

	public class Company
	{ 
		public const string Name = "Webb Electronics,LTD";
		public const string Address = "1410 Westway Circle Carrollton, TX 75006";
		public const string WebSite = "http://www.webbelectronics.com";
		public const string Telephone = "972-242-5400";
		public const string Fax = "972-242-4517";
		public const string State = "U.S";
		//
		public class President
		{
			public const string FirstName = "Joel";
			public const string FullName = "Joel A. Krause";
			public const string EMail = "Joel@webbelectronics.com";
			public const string Mobile = "214-789-0740";
		}
		//
		public class ChinaOffice
		{
			public const string Name = "武汉韦伯电子科技有限公司";
			public const string Address = "湖北省武汉市武昌区中北路楚天都市花园B栋19楼";
			public const string WebSite = "http://www.webbelectronics.com";
			public const string Telephone = "027-87814846";
			public const string Fax = "027-87814846";
			public const string State = "China";
			//
			public class Manager
			{
				public const string FirstName = "Michael";
				public const string FullName = "Michael.Pi";
				public const string EMail = "Michael@webbelectronics.com";
				public const string Mobile = "13986231170";
			}
		}
	}
}
