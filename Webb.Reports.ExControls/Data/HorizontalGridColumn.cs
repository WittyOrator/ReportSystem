// 10-12-2011 Scott

using System;
using System.Collections;

using System.Data;
using System.Text;
using System.Drawing;
using System.Collections.Specialized;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Serialization;
using System.Diagnostics;
using System.ComponentModel;
using System.ComponentModel.Design;

using Webb;
using Webb.Data;
using Webb.Utilities;
using Webb.Collections;
using Webb.Reports;

namespace Webb.Reports.ExControls.Data
{
    [Serializable]
    public class HorizontalGridColumn : ISerializable
    {
        protected DBFilter _Filter;
        protected string _TitleField = string.Empty;
        protected string _Title = "New Column";
        protected int _RowIndicator = -1;
        protected int _ColumnIndex = 0;
        protected int _ColumnWidth = 60;

        [Browsable(false)]
        public int ColumnIndex
        {
            get { return _ColumnIndex; }
            set { _ColumnIndex = value; }
        }

        [Browsable(false)]
        public int ColumnWidth
        {
            get { return _ColumnWidth; }
            set { _ColumnWidth = value; }
        }

        [Category("Filters")]
        [EditorAttribute(typeof(Webb.Reports.Editors.DBFilterEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public DBFilter Filter
        {
            get
            {
                if (this._Filter == null) this._Filter = new DBFilter();

                return this._Filter;
            }
            set { this._Filter = value; }
        }

        [Editor(typeof(Webb.Reports.Editors.FieldSelectEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string TitleField
        {
            get{
                 if(_TitleField==null)_TitleField=string.Empty;
                 return this._TitleField;}
            set
            {
                if (this._TitleField != value)
                {
                    this._TitleField = value;
                }
            }
        }

        public string Title
        {
            set { this._Title = value; }
            get { return this._Title; }
        }

        [Browsable(false)]
        public int RowIndicator
        {
            get
            {
                return _RowIndicator;
            }
        }

        public HorizontalGridColumn()
        {
            
        }

        public HorizontalGridColumn Copy()
        {
            HorizontalGridColumn copyColumn = new HorizontalGridColumn();

            copyColumn._TitleField = this.TitleField;

            copyColumn._Filter = this.Filter.Copy();

            copyColumn._Title = this.Title;

            return copyColumn;
        }

        public void GetResult(System.Data.DataTable i_Table,Webb.Collections.Int32Collection i_OuterRows)
        {
            Int32Collection filteredRows = this.Filter.GetFilteredRows(i_Table, i_OuterRows);

            if (filteredRows.Count > 0)
            {
                _RowIndicator = filteredRows[0];
            }
        }

        public virtual void GetALLUsedFields(ref ArrayList usedFields)
        {
            if (usedFields.Contains(this.TitleField)) return;

            usedFields.Add(this.TitleField);
        }

        public override string ToString()
        {
            return Title;
        }

        #region Serialization By Scott 2011-10-12
		virtual public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			info.AddValue("_Filter",_Filter,typeof(Webb.Data.DBFilter));
            info.AddValue("_TitleField", _TitleField);
            info.AddValue("_Title", _Title);
		}

		public HorizontalGridColumn(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			try
			{
				_Filter = info.GetValue("_Filter",typeof(DBFilter)) as DBFilter;

				this._Filter=AdvFilterConvertor.GetAdvFilter(DataProvider.VideoPlayBackManager.AdvReportFilters,this._Filter);
			}
			catch
			{
				_Filter = new DBFilter();
			}

            try
            {
                _TitleField = info.GetString("_TitleField");
            }
            catch
            {
                _TitleField = string.Empty;
            }

            try
            {
                _Title = info.GetString("_Title");
            }
            catch
            {
                _Title = string.Empty;
            }


            _RowIndicator = -1;
		}
		#endregion
    }

    [Serializable]
    public class HorizontalGridColumnCollection : CollectionBase
    {
        public HorizontalGridColumnCollection()
		{
			
		}

        public HorizontalGridColumnCollection Copy()
		{
            HorizontalGridColumnCollection hGridColumns = new HorizontalGridColumnCollection();

            foreach (HorizontalGridColumn hGridColumn in this)
			{
                hGridColumns.Add(hGridColumn.Copy());
			}

            return hGridColumns;
		}

        public HorizontalGridColumn this[int i_index]
		{
			get
			{
                return this.InnerList[i_index] as HorizontalGridColumn;
			}
			set
			{
				this.InnerList[i_index] = value;
			}
		}

        public int Add(HorizontalGridColumn hGridColumn)
		{
            return this.InnerList.Add(hGridColumn);
		}

        public void Remove(HorizontalGridColumn hGridColumn)
		{
            this.InnerList.Remove(hGridColumn);
		}

        public bool Contains(HorizontalGridColumn hGridColumn)
		{
            return this.InnerList.Contains(hGridColumn);
		}

        public void Swap(int a1, int a2)
        {
            if (a1 < 0 || a2 < 0 || a1 >= this.Count || a2 >= this.Count)
            {
                return;
            }
            HorizontalGridColumn column = this[a1];
            this.InnerList[a1] = this.InnerList[a2];
            this.InnerList[a2] = column;
        }
    }
}
