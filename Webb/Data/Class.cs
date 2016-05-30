/***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:Class.cs
 * Author:Country.Wu [EMail:Webb.Country.Wu@163.com]
 * Create Time:11/7/2007 04:19:13 PM
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
	#region public enum SummaryTypes
	public enum SummaryTypes
	{
		None = 0,
		Count = 1,
		Frequence = 2,
		Total = 3,
		Total_Plus = 4,
		Total_Minus = 5,
		Average = 6,
		Average_Plus = 7,
		Average_Minus = 8,
		Max = 9,
		Min = 10,
	}

	public enum SummaryRelations
	{
		Global = 0,
		Related =1,
	}
	#endregion

	#region public interface IDBPersistentObject
	public interface IDBPersistentObject
	{
		int Save();
		bool Delete(int i_ID,bool i_SetDelFlag);
		bool Load(int i_ID);
		bool Update(int i_ID);
		int ID{get; set;}
		string TableName{get;}
		Collections.ReadonlyStringCollection Fields{get;}   
	}
	#endregion
}
