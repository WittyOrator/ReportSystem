/***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:DBFilter.cs
 * Author:Country.Wu [EMail:Webb.Country.Wu@163.com]
 * Create Time:10/30/2007 04:20:28 PM
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
using System.Text.RegularExpressions;

using System.ComponentModel;
using System.ComponentModel.Design;

using Webb.Collections;

namespace Webb.Data
{
    [Serializable]
    public enum TotalType: byte       //Added this Enum at 2008-11-28 10:34:37@Simon
    {
        None = 0,
        AllAfter,
        AllBefore       
    }

	[Serializable]
	[Flags]
	public enum ProductRight
	{  
	   None=0,
       Victory=1,		
	   QuickCut=2,
       PlayMaker=4,
	   GameDay=8,
	   Advantage=16,
	}

	#region public class UserLevel:ISerializable
    [Serializable]
	public class UserLevel:ISerializable
	{
		#region Auto Constructor By Macro 2009-6-15 8:48:26
		public UserLevel()
		{
			_Name="Custom";
			_Rights=(ProductRight)31;
		}
		public UserLevel(string name,ProductRight right)
		{
			_Name=name;
			_Rights=right;
		}
		public UserLevel(string name,int right)
		{
			_Name=name;
			_Rights=(ProductRight)right;
		}
		#endregion

	
		protected string _Name="Custom";
		protected  ProductRight _Rights=(ProductRight)31;

		#region Copy Function By Macro 2009-6-15 8:48:33
		public UserLevel Copy()
		{
			UserLevel thiscopy=new UserLevel();
			thiscopy._Name=this._Name;
			thiscopy._Rights=this._Rights;
			return thiscopy;
		}
		#endregion

		public string Name
		{
			get{ return _Name; }
			set{ _Name = value; }
		}

		public Webb.Data.ProductRight Rights
		{
			get{ return _Rights; }
			set{ _Rights = value; }
		}

		#region Serialization By Simon's Macro 2009-6-15 8:48:36
		public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			info.AddValue("_Name",_Name);
			info.AddValue("_Rights",_Rights,typeof(Webb.Data.ProductRight));
		
		}

		public UserLevel(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			try
			{
				_Name=info.GetString("_Name");
			}
			catch
			{
				_Name="Custom";
			}
			try
			{
				_Rights=(Webb.Data.ProductRight)info.GetValue("_Rights",typeof(Webb.Data.ProductRight));
			}
			catch
			{
                  _Rights=(ProductRight)31;
			}
		}
		#endregion

		public override string ToString()
		{
			if(_Name.StartsWith("Level"))
			{
				string[] leves=new string[]{"Low","Lower","Medium","Higher","High"};

				int num=Convert.ToInt32(_Name.Remove(0,5))-1;

                return  string.Format("{0}({1})",_Name,leves[num]);

			}
			else if((int)_Rights==31)
			{
				return "Custom(All)";
			}
			return string.Format("{0}({1})",_Name,_Rights);
		}


	}
	
	#endregion

	#region public class UserLevelCollection
	public class  UserLevelCollection:WebbCollection
	{
		public UserLevel this[int index]
		{
			get
			{
				return this.innerList[index] as UserLevel;
			}
			set
			{
				this.innerList[index]=value;
			}
		}
		public UserLevelCollection(params UserLevel[] levels)
		{
			this.Clear();

			foreach(UserLevel level in levels)
			{
              this.Add(level);
			}
		}
		public static UserLevelCollection StandardUserLevels()
		{		
           UserLevelCollection userLevels=new UserLevelCollection(new UserLevel("Level1",16),
			                                                      new UserLevel("Level2",24),
																  new UserLevel("Level3",28),
														          new UserLevel("Level4",30),
			                                                      new UserLevel("Level5",31)
			                                                      );
			return userLevels;
		}
	}
	#endregion

	#region public class AdvUserRights:ISerializable
	public class AdvUserRights:ISerializable
	{
		#region Auto Constructor By Macro 2009-6-17 15:37:43
		public AdvUserRights()
		{
			_All=true;
			_Users=new System.Collections.Specialized.StringCollection();
		}
		#endregion

	
		protected bool _All=true;
		protected StringCollection _Users=new StringCollection();

		#region Copy Function By Macro 2009-6-17 15:37:44
		public AdvUserRights Copy()
		{
			AdvUserRights thiscopy=new AdvUserRights();
			thiscopy._All=this._All;
			thiscopy._Users=this._Users;
			return thiscopy;
		}
		#endregion

		public bool All
		{
			get{ return _All; }
			set{ _All = value; }
		}

		public System.Collections.Specialized.StringCollection Users
		{
			get{
				  if(_Users==null)_Users=new StringCollection();
				   return _Users; }
			set{ _Users = value; }
		}

		#region Serialization By Simon's Macro 2009-6-17 15:37:53
		public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			info.AddValue("_All",_All);
			info.AddValue("_Users",_Users,typeof(System.Collections.Specialized.StringCollection));
		
		}

		public AdvUserRights(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			try
			{
				_All=info.GetBoolean("_All");
			}
			catch
			{
				_All=true;
			}
			try
			{
				_Users=(System.Collections.Specialized.StringCollection)info.GetValue("_Users",typeof(System.Collections.Specialized.StringCollection));
			}
			catch
			{
               _Users=new StringCollection();
			}
		}
		#endregion
	    

	}
    #endregion
}
