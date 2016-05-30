using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Webb.Reports
{
    public class GroupDescription
    {
        #region Auto Constructor By Macro 2010-12-8 9:00:27
        public GroupDescription()
        {
            _Field = string.Empty;
            _DefaultHeader = string.Empty;
        }
        public GroupDescription(string p_Field)
        {
            _Field = p_Field;
            _DefaultHeader = p_Field;
        }
        public GroupDescription(string p_Field, string p_DefaultHeader)
        {
            _Field = p_Field;
            _DefaultHeader = p_DefaultHeader;
        }
        #endregion


        protected string _Field = string.Empty;
        protected string _DefaultHeader= string.Empty;

        public string Field
        {
            get
            {
                if (_Field == null) _Field = string.Empty;
                return _Field;
            }
            set
            {
                _Field = value;
            }
        }

        public string DefaultHeader
        {
            get
            {
                if (_DefaultHeader == null) _DefaultHeader = string.Empty;
                return _DefaultHeader;
            }
            set
            {
                _DefaultHeader = value;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is GroupDescription)
            {
                return this._Field == (obj as GroupDescription).Field;
            }
            return false;
        }

        public override string ToString()
        {          
           return this._Field;
         }

        #region Copy Function By Macro 2010-12-8 9:11:14
        public GroupDescription Copy()
        {
            GroupDescription thiscopy = new GroupDescription();
            thiscopy._Field = this._Field;
            thiscopy._DefaultHeader = this._DefaultHeader;
            return thiscopy;
        }
        #endregion
    }

    public class GroupDescriptionCollection : CollectionBase
    {
        public GroupDescriptionCollection()
            : base()
        {
        }
        public GroupDescription this[int index]
        {
            get
            {
                return this.InnerList[index] as GroupDescription;
            }
            set
            {
                this.InnerList[index] = value;
            }
        }
        public int Add(GroupDescription groupDescription)
        {
            return this.InnerList.Add(groupDescription);
        }
        public int Add(string strField, string strHeader)
        {
            return this.InnerList.Add(new GroupDescription(strField, strHeader));
        }
        public bool Contains(string strField)
        {
            foreach (GroupDescription group in this.InnerList)
            {
                if (group.Field == strField) return true;
            }
            return false;
        }
        public GroupDescriptionCollection Copy()
        {
            GroupDescriptionCollection collection = new GroupDescriptionCollection();

            foreach (GroupDescription group in this.InnerList)
            {
                collection.Add(group.Copy());
            }

            return collection;
        }
    }


    public class IndexerDescription
    {
        private string _KeyKind=string.Empty; 
        private int _KeyId;
        private string _KeyName = string.Empty;

        public IndexerDescription(string keyKind,int key,string keyname)
        {
            this._KeyKind = keyKind;
            this._KeyId = key;
            this._KeyName = keyname;
        }

        public int KeyId
        {
            get
            {
                return _KeyId;
            }
            set
            {
                _KeyId = value;
            }
        }
        public string KeyName
        {
            get
            {
                if (_KeyName == null) _KeyName = string.Empty;

                return _KeyName;
            }
            set
            {
                _KeyName = value;
            }
        }
        public override string ToString()
        {
            return string.Format("[{0}] {1}",this._KeyKind,KeyName);
        }

    }
}
