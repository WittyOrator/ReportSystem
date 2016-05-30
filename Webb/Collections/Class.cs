/***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:Class1.cs
 * Author:Country.Wu [EMail:Webb.Country.Wu@163.com]
 * Create Time:11/7/2007 04:01:36 PM
 * Copyright:1986-2007@Webb Electronics all right reserved.
 * Purpose:
 * ***********************************************************************/
#region History
/*
 * //Author@DateTime : Description
 * */
#endregion History

#define Int32Collection

using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.Serialization;


namespace Webb.Collections
{
	#region class ReadonlyStringCollection : ReadOnlyCollectionBase
	[Serializable]
	public class ReadonlyStringCollection : ReadOnlyCollectionBase
	{
		public bool Contains(string i_Value)
		{
			return this.InnerList.Contains(i_Value);
		}
		/// <summary>
		/// Check the value if it is in the collection.
		/// </summary>
		/// <param name="i_Value">The string to check.</param>
		/// <param name="i_MacthCase">Ignore the case if it is ture.</param>
		/// <returns></returns>
		public bool Contains(string i_Value,bool i_IgnoreCase)
		{
			if(i_IgnoreCase)
			{
				string m_LowValue = i_Value.ToLower();
				foreach(string m_Value in this.InnerList)
				{
					return m_Value.ToLower()==m_LowValue;
				}
				return false;
			}
			else
			{
				return this.InnerList.Contains(i_Value);
			}
		}
		public string this[int i]
		{
			get{return this.InnerList[i].ToString();}
		}
		public ReadonlyStringCollection(string[] i_Values)
		{
			foreach(string i_value in i_Values)
			{
				this.InnerList.Add(i_value);
			}			
		}
		public ReadonlyStringCollection()
		{
		}
	}
	#endregion

#	if Int32Collection
	#region public class Int32Collection : CollectionBase	
	[Serializable]
	public class Int32Collection : CollectionBase  
	{
		public Int32 this[ int index ]
		{
			get
			{
				return( (Int32) List[index] );
			}
			set  
			{
				List[index] = value;
			}
		}
		public void Sort()
		{
			this.InnerList.Sort();
		}

		public int Add( Int32 value )  
		{
			return( List.Add( value ) );
		}

		public int IndexOf( Int32 value )  
		{
			return( List.IndexOf( value ) );
		}

		public void Insert( int index, Int32 value )  
		{
			List.Insert( index, value );
		}

		public void Remove( Int32 value )  
		{
			List.Remove( value );
		}

		public bool Contains( Int32 value )  
		{
			// If value is not of type Int32, this will return false.
			return( List.Contains( value ) );
		}

        private bool EqualArray(Int32Collection array)
        {
            if (this.InnerList.Count != array.Count) return false;

            for (int i = 0; i < this.InnerList.Count;i++ )
            {
                if (this[i] != array[i]) return false;
            }

            return true;
        }

        protected override void OnInsert(int index, Object value)  
		{
			if ( value.GetType() != Type.GetType("System.Int32") )
				throw new ArgumentException( "value must be of type Int32.", "value" );
		}

		protected override void OnRemove( int index, Object value )  
		{
			if ( value.GetType() != Type.GetType("System.Int32") )
				throw new ArgumentException( "value must be of type Int32.", "value" );
		}

		protected override void OnSet( int index, Object oldValue, Object newValue )  
		{
			if ( newValue.GetType() != Type.GetType("System.Int32") )
				throw new ArgumentException( "newValue must be of type Int32.", "newValue" );
		}

		protected override void OnValidate( Object value )  
		{
			if ( value.GetType() != Type.GetType("System.Int32") )
				throw new ArgumentException( "value must be of type Int32." );
		}

		public void CopyTo(Int32Collection value)
		{
			if(value == null || object.ReferenceEquals(value, this)) return;
			
			value.Clear();

			foreach(int i in this)
			{
				value.Add(i);
			}
		}

		public Int32Collection Combine(params Int32Collection[] array)
		{
			Int32Collection retArray = new Int32Collection();

			foreach(Int32Collection col in array)
			{
				if(col == null) continue;

				foreach(int i in col)
				{
					if(!retArray.Contains(i)) retArray.Add(i);
				}
			}

			return retArray;
		}

		public bool ValueEqual(Int32Collection value)
		{
			if(this.Count != value.Count) return false;
 
			for(int i = 0; i < this.Count; i++)
			{
				if(this[i] != value[i]) return false;
			}

			return true;
		}

        public void ShiftDownRowsAfterDelete(int deletedRow)
        {
             for (int i = this.InnerList.Count - 1; i >= 0; i--)
             {
                int row = this[i];

                if (row < deletedRow) continue;

                if (row == deletedRow||row==0) this.RemoveAt(i);

                if(this.InnerList.Count>i)this.InnerList[i] = row - 1;

             }        
        }
	}

	#endregion
#	endif

#	if Int32CollectionEx
	#region public class Int32CollectionEx
	//Wu.Country@2007-11-28 15:39 added this region.
	[Serializable]
	public class Int32Collection : IEnumerable
	{
		private int[] _InnerArray;
		private int _Length;
		private int _Capability;
		private object _SycObject;
		//
		public int Length
		{
			get{return this._Length;}
		}
		public int Count
		{
			get{return this._Length;}
		}
		public int Capability
		{
			get{return this._Capability;}
		}
		public int this[ int index ]  
		{
			get  
			{
				if(index<0||index>=this._Length)
				{
					throw new IndexOutOfRangeException();
				}
				return this._InnerArray[index];
			}
			set  
			{
				if(index<0||index>=this._Length)
				{
					throw new IndexOutOfRangeException();
				}
				this._InnerArray[index] = value;
			}
		}
		//
		public Int32Collection(int i_Capability)
		{
			this._InnerArray = new int[i_Capability];
			this._Capability = i_Capability;
			this._Length = 0;
			this._SycObject = new object();
		}

		public Int32Collection() : this(6)
		{
		}
		//
		public int Add(int i_Value)
		{
			lock(this._SycObject)
			{
				this.CheckCapability();
				this._InnerArray[_Length] = i_Value;
				this._Length ++;
			}
			return this._Length;
		}

		public void Remove(int i_Value)
		{
			lock(this._SycObject)
			{
				int i_Index = this.Find(i_Value);
				if(i_Index>=0)
				{
					this.MoveValues_Front(i_Index);
				}
				this._Length--;
			}
		}

		public void Insert(int i_Index, int i_Value)
		{
			if(i_Index<0||i_Index>this._Length)
			{
				throw new IndexOutOfRangeException();
			}
			lock(this._SycObject)
			{
				this.CheckCapability();
				this.MoveValues_Back(i_Index);
				this._InnerArray[i_Index] = i_Value;
				this._Length ++;
			}
		}

		private void MoveValues_Front(int i_StartIndex)
		{
//			if(i_StartIndex<0||i_StartIndex>this._Length)
//			{
//				throw new IndexOutOfRangeException();
//			}
			for(int i=i_StartIndex+1; i<this._Length;i++)
			{
				this._InnerArray[i-1] = this._InnerArray[i];
			}
		}

		private void MoveValues_Back(int i_StartIndex)
		{
//			if(i_StartIndex<0||i_StartIndex>this._Length)
//			{
//				throw new IndexOutOfRangeException();
//			}
			for(int i=this._Length;i>i_StartIndex;i--)
			{
				this._InnerArray[i] = this._InnerArray[i-1];
			}
		}

		public int Find(int i_Value)
		{
			for(int i=0; i<this._Length;i++)
			{
				if(this._InnerArray[i]==i_Value) return i;
			}
			return -1;
		}

		public int Find(int i_StartIndex,int i_Value)
		{
			if(i_StartIndex<0||i_StartIndex>this._Length)
			{
				throw new IndexOutOfRangeException();
			}
			for(int i=i_StartIndex; i<this._Length;i++)
			{
				if(this._InnerArray[i]==i_Value) return i;
			}
			return -1;
		}

		public int IndexOf(int i_Value)
		{
			return this.Find(i_Value);
		}

		public bool Contains(int i_Value)
		{
			if(this.Find(i_Value)>0) return true;
			else return false;
		}

		//
		public void Clear()
		{
			lock(this._SycObject)
			{
				this._Length = 0;
			}
		}
		//
		public void Sort()
		{
			for(int i = 0; i<this._Length;i++)
			{
				for(int j=i;j<this._Length;j++)
				{
					if(this._InnerArray[i]<this._InnerArray[j])
					{
						int m_Temp = this._InnerArray[i];
						this._InnerArray[i] = this._InnerArray[j];
						this._InnerArray[j] = this._InnerArray[i];
					}
				}
			}
		}
		//
		private void CheckCapability()
		{
			if(this._Length>=this._Capability-1)
			{
				int[] m_NewArray = new int[this._Capability*2];
				this._Capability*=2;
				for(int i = 0; i<this._Length;i++)
				{
					m_NewArray[i] = this._InnerArray[i];
				}
				this._InnerArray = m_NewArray;
			}
		}
		//
	#region IEnumerable Members
		public Int32Enumerator GetEnumerator()
		{
			// TODO:  Add Int32CollectionEx.GetEnumerator implementation
			return new Int32Enumerator(this);
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			// TODO:  Add Int32CollectionEx.GetEnumerator implementation
			return new Int32Enumerator(this);			
		}
	#endregion		
	}
	#endregion

	#region public class Int32Enumerator
	/*Descrition:   */
	public class Int32Enumerator:IEnumerator
	{
		//Wu.Country@2007-11-28 03:57 PM added this class.
		//Fields

		//Properties
		int _EnumeratorCount = -1;
		Int32Collection _Collection;
		int _Length
		{
			get{return this._Collection.Length;}
		}

		//ctor
		internal Int32Enumerator(Int32Collection i_Collection)
		{
			this._EnumeratorCount = i_Collection.Length;
			this._Collection = i_Collection;
		}

		//Methods

	#region IEnumerator Members
		private int _Indicator = -1;

		public void Reset()
		{
			// TODO:  Add Int32CollectionEx.Reset implementation
			if(this._EnumeratorCount != this._Length)
			{
				throw new InvalidOperationException("Collection has been changed while using the Enumerator.");
			}
			this._Indicator = -1;
		}

		public int Current
		{
			get
			{
				// TODO:  Add Int32CollectionEx.Current getter implementation
				return this._Collection[this._Indicator];
			}
		}

		object IEnumerator.Current
		{
			get
			{
				// TODO:  Add Int32CollectionEx.Current getter implementation
				return this._Collection[this._Indicator];
			}
		}

		public bool MoveNext()
		{
			// TODO:  Add Int32CollectionEx.MoveNext implementation
			if(this._EnumeratorCount != this._Length)
			{
				throw new InvalidOperationException("Collection has been changed while using the Enumerator.");
			}
			this._Indicator++;
			if(this._Indicator<this._Length)
			{				
				return true;
			}
			else
			{
				return false;
			}
		}

	#endregion
	}
	#endregion
#	endif

	#region CustomTypeDescriptor

//	public class CustomTypeDescriptor : ICustomTypeDescriptor
//	{
//		public CustomTypeDescriptor()
//		{
//		}
//
//		//ICustomTypeDescriptor Members
//		#region ICustomTypeDescriptor Members
//		public AttributeCollection GetAttributes()
//		{
//			return TypeDescriptor.GetAttributes(typeof(CustomTypeDescriptor));
//		}
//		public string GetClassName()
//		{
//			return TypeDescriptor.GetClassName(typeof(CustomTypeDescriptor));
//		}
//		public string GetComponentName()
//		{
//			return TypeDescriptor.GetComponentName(typeof(CustomTypeDescriptor));
//		}
//		public TypeConverter GetConverter()
//		{
//			return TypeDescriptor.GetConverter(typeof(CustomTypeDescriptor));
//		}
//		public EventDescriptor GetDefaultEvent()
//		{
//			return TypeDescriptor.GetDefaultEvent(typeof(CustomTypeDescriptor));
//		}
//		public PropertyDescriptor GetDefaultProperty()
//		{
//			return TypeDescriptor.GetDefaultProperty(typeof(CustomTypeDescriptor));
//		}
//		public object GetEditor(Type editorBaseType)
//		{
//			return TypeDescriptor.GetEditor(typeof(CustomTypeDescriptor), editorBaseType);
//		}
//		public EventDescriptorCollection GetEvents(Attribute[] attributes)
//		{
//			return TypeDescriptor.GetEvents(typeof(CustomTypeDescriptor), attributes);
//		}
//		public EventDescriptorCollection GetEvents()
//		{
//			return TypeDescriptor.GetEvents(typeof(CustomTypeDescriptor));
//		}
//		virtual public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
//		{
////			int i = 0;
//			//parameterList是外面传入的待显示数据。
//			
//
////			PropertyDescriptor[] newProps = new PropertyDescriptor[parameterList.Count];
////			foreach (ConfigParameter parameter in parameterList)
////			{
////				//由ConfigParameter，你自己定义的类型，转成PropertyDescriptor类型：
////				newProps[i++] = new SystemOptionsPropertyDescriptor(parameter, true, attributes);
////			}
//
//			//取得了PropertyDescriptor[] newProps;现在可以对它排序，否则就按照newProps的原有顺序显示；
//            
//			//1.直接返回
//
////			PropertyDescriptorCollection tmpPDC = new PropertyDescriptorCollection(newProps);
////			return tmpPDC;
//
//			//2.默认字母顺序
////			PropertyDescriptorCollection tmpPDC = new PropertyDescriptorCollection(newProps);
////			return tmpPDC.Sort();
//
//			//3.数组的顺序：sortName就是属性的顺序，内容就是各个属性名。
//			string[] sortName = new string[] { "StrZ","StrA","StrF","StrE" };
//			PropertyDescriptorCollection tmpPDC = TypeDescriptor.GetProperties(typeof(CustomTypeDescriptor),attributes);
//			return tmpPDC.Sort(sortName);
//
//			//4.comparer规则定义的排序方式
////			ParameterListComparer myComp = new ParameterListComparer();//ParameterListComparer : IComparer
////			return tmpPDC.Sort(myComp);
//
//			//5.先数组，后comparer
////			return tmpPDC.Sort(sortName,myComp);
//
//			//而我采用的是把数据传入之前就排序完毕的方法:即调用之前，把数据parameterList排好顺序，再 1.直接返回。
//		}
//		public PropertyDescriptorCollection GetProperties()
//		{
//			return TypeDescriptor.GetProperties(typeof(CustomTypeDescriptor));
//		}
//		public object GetPropertyOwner(PropertyDescriptor pd)
//		{
//			return this;
//		}
//		#endregion
//	}
//
//	//对于数组排序方法，可以声明称一个事件，由外部传入属性的实现顺序：
//	public delegate string[] SortEventHandler();
//
//	public class CustomTypeDescriptorExtend : CustomTypeDescriptor
//	{
//		public SortEventHandler OnSort;
//
//		//......其它代码
//
//		public override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
//		{
//			string[] sortName = OnSort();
//			PropertyDescriptorCollection tmpPDC = TypeDescriptor.GetProperties(typeof(CustomTypeDescriptorExtend), attributes);
//			return tmpPDC.Sort(sortName);
//		}
//
//		protected string strA;
//		protected string strE;
//		protected string strF;
//		protected string strZ;
//
//		[Category("A")]
//		public string StrE
//		{
//			get{return this.strE;}
//			set{this.strE = value;}
//		}
//
//		[Category("A")]
//		public string StrA
//		{
//			get{return this.strA;}
//			set{this.strA = value;}
//		}
//
//		[Category("A")]
//		public string StrF
//		{
//			get{return this.strF;}
//			set{this.strF = value;}
//		}
//
//		[Category("A")]
//		public string StrZ
//		{
//			get{return this.strZ;}
//			set{this.strZ = value;}
//		}
//	}
	#endregion

	#region WebbCollection&WebbEnumerator
	[Serializable]
	public class WebbCollection:ISerializable,IEnumerable
	{
		protected ArrayList innerList=new ArrayList();

		public WebbCollection(){}

		public System.Collections.ArrayList InnerList
		{
			get{ return innerList; }
			set{ innerList = value; }
		}
		public IEnumerator GetEnumerator()
		{
			return new WebbEnumerator(this);
		}
		public int Count
		{
			get
			{
				return this.innerList.Count;
			}

		}
		
		public virtual int Add(object value )  
		{
			return( innerList.Add( value ) );
		}

		public virtual int IndexOf( object value )  
		{
			return( innerList.IndexOf( value ) );
		}

		public virtual void Insert( int index, object value )  
		{
			innerList.Insert( index, value );
		}

		public virtual void Clear()  
		{
			innerList.Clear();
		}

		public virtual void Remove( object value )  
		{
			innerList.Remove( value );
		}

		public virtual bool Contains( object value )  
		{
			// If value is not of type Int32, this will return false.
			return( this.innerList.Contains( value ) );
		}


		#region Serialization By Simon's Macro 2009-6-12 9:33:21
		public virtual void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			info.AddValue("innerList",innerList,typeof(System.Collections.ArrayList));
		
		}

		public WebbCollection(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			try
			{
				innerList=(System.Collections.ArrayList)info.GetValue("innerList",typeof(System.Collections.ArrayList));
			}
			catch
			{
				innerList=new ArrayList();
			}
		}
		#endregion

		

	}


	[Serializable]
	public class WebbEnumerator:IEnumerator	
	{	
        private int position=-1;

		private WebbCollection webbCollection;

		public WebbEnumerator(WebbCollection collection)
		{
			webbCollection=collection;
		}

		#region IEnumerator Members

		public void Reset()
		{
			position=-1;
		}

		public object Current
		{
			get
			{
				try
				{
					return webbCollection.InnerList[position];
				}
				catch
				{
					throw new InvalidOperationException();
				}
				
			}
		}

		public bool MoveNext()
		{
			if(position<webbCollection.InnerList.Count-1)
			{
				position++;

				return true;
			}
			else
			{
				return false;
			}
			
		}

		#endregion
	}
	#endregion
}
