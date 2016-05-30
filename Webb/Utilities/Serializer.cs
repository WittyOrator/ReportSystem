/***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:Serializer.cs
 * Author:Country.Wu [EMail:Webb.Country.Wu@163.com]
 * Create Time:11/14/2007 04:15:52 PM
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
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;

namespace Webb.Utilities
{
	/// <summary>
	/// Summary description for SerializeHelper.
	/// </summary>
	public class Serializer
	{
		private static BinaryFormatter _BinaryFormatter;// = new BinaryFormatter();
		private static BinaryFormatter M_BinaryFormatter
		{
			get
			{
				if(_BinaryFormatter==null) _BinaryFormatter = new BinaryFormatter();
				return _BinaryFormatter;
			}
		}
		//
		private Serializer(){}
		//
		static public object Deserialize(string i_fileName)
		{
			using(Stream m_stream	= File.Open(i_fileName,FileMode.Open,FileAccess.Read,FileShare.None))
			{
				return DeserializeObject(m_stream);
			}
		}
		public static object DeserializeObject(Stream i_stream)
		{
			i_stream.Position = 0;
			//if(M_BinaryFormatter==null) M_BinaryFormatter = new BinaryFormatter();
			return Serializer.M_BinaryFormatter.Deserialize(i_stream);
		}
		//
		static public void Serialize(object i_object, string i_fileName,bool i_overwrite)
		{
			using(Stream m_stream = File.Open(i_fileName,FileMode.Create,FileAccess.Write,FileShare.None))
			{
				SerializeObject(m_stream,i_object);
			}
		}
		public static void SerializeObject(Stream i_stream,object i_object)
		{
			Serializer.M_BinaryFormatter.Serialize(i_stream,i_object);
		}
	}
}
