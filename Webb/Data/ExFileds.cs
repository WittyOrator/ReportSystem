/***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:DBFilter.cs
 * Author:Country.Wu [EMail:Webb.Country.Wu@163.com]
 * Create Time:10/31/2007 04:20:28 PM
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

namespace Webb.Data
{
	#region public class ExField
	/*Descrition:   */
	public class ExField
	{
		//Wu.Country@2007-10-31 03:26 PM added this class.
		//Fields
		protected string _ColumnCaption;
		protected string _ColumnName;
		protected Type _DataType;
		//protected string _Expression;
		//Properties
		//ctor
		public ExField()
		{
		}
		//Methods
	}
	#endregion

	#region public class SummaryField
	/*Descrition:   */
	public class SummaryField : ExField
	{
		//Wu.Country@2007-10-31 03:27 PM added this class.
		//Fields

		//Properties

		//ctor
		public SummaryField()
		{
		}
		//Methods
	}
	#endregion


}
