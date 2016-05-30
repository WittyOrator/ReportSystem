/***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:DesignerConfigation.cs
 * Author:Country.Wu [EMail:Webb.Country.Wu@163.com]
 * Create Time:11/8/2007 01:01:34 PM
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
using System.Data;
using System.Text;
using System.Collections.Specialized;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Serialization;
using System.Diagnostics;


namespace Webb.Reports.ReportDesigner
{
	#region public class DesignerConfiguation
	internal class DesignerConfiguation
	{
		public DesignerConfiguation()
		{
		}

		public void LoadConfig()
		{
			// TODO: implement
		}
   
		private Array _ExtendedTools;
   
		protected Array ExtendedTools
		{
			get
			{
				return _ExtendedTools;
			}
			set
			{
				if (this._ExtendedTools != value)
					this._ExtendedTools = value;
			}
		}   
	}
	#endregion
}
