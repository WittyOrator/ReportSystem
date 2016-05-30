using System;
using System.Collections.Generic;
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

using DevExpress.Utils;
using Webb.Collections;
using System.Windows.Forms;
using Webb.Data;

namespace Webb.Reports.ExControls.Data
{
    #region public class PlayerPostion:ISerializable
    [Serializable]
    public class GradingPostion : ISerializable
    {
        #region Auto Constructor By Macro 2011-7-11 9:13:15
        public GradingPostion()
        {
            _Name = string.Empty;
            _Field = string.Empty;
            _GradingFields = new StringCollection();
        }

        public GradingPostion(StringCollection p_GradingFields, string p_Field, string p_Name)
        {
            _Name = p_Name;
            _Field = p_Field;
            _GradingFields = p_GradingFields;
        }
        #endregion


        protected string _Name = string.Empty;

        protected string _Field = string.Empty;

        protected StringCollection _GradingFields = new StringCollection();

        protected string _CommentField = string.Empty;

        #region Copy Function By Macro 2011-7-11 9:13:20
        public GradingPostion Copy()
        {
            GradingPostion thiscopy = new GradingPostion();
            thiscopy._Name = this._Name;
            thiscopy._Field = this._Field;
            thiscopy._GradingFields = this.GradingFields;
            thiscopy._CommentField = this._CommentField;
            return thiscopy;
        }
        #endregion

        [Browsable(false)]
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }

        [Browsable(true), Category("Comments")]
        [EditorAttribute(typeof(Webb.Reports.Editors.FieldSelectEditor), typeof(System.Drawing.Design.UITypeEditor))]	//Modified at 2008-9-25 13:46:36@Simon
        public string CommentField
        {
            get
            {
                if (_CommentField == null) _CommentField = string.Empty;

                return _CommentField;
            }
            set
            {
                _CommentField = value;               
            }
        }
        [Browsable(false), Category("Main")]
        [EditorAttribute(typeof(Webb.Reports.Editors.FieldSelectEditor), typeof(System.Drawing.Design.UITypeEditor))]	//Modified at 2008-9-25 13:46:36@Simon
        public string Field
        {
            get
            {
                return _Field;
            }
            set
            {
                _Field = value;

                _Name = _Field;
            }
        }
        [Browsable(true), Category("Main")]
        [EditorAttribute(typeof(Webb.Reports.ExControls.Editors.FieldsGroupEditor), typeof(System.Drawing.Design.UITypeEditor))]	//Modified at 2008-9-25 13:46:36@Simon
        public StringCollection GradingFields
        {
            get
            {
                if (_GradingFields == null) _GradingFields = new StringCollection();
                return _GradingFields;
            }
            set
            {
                _GradingFields = value;
            }
        }

  
        public override string ToString()
        {
            return Name;
        }
        public override bool Equals(object obj)
        {
            if (!(obj is GradingPostion)) return false;

            return (obj as GradingPostion).Name == this.Name;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #region Serialization By Simon's Macro 2011-7-19 14:08:44
		public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
			info.AddValue("_Name",_Name);
			info.AddValue("_Field",_Field);
			info.AddValue("_GradingFields",_GradingFields,typeof(System.Collections.Specialized.StringCollection));
			info.AddValue("_CommentField",_CommentField);

        }

        public GradingPostion(SerializationInfo info, StreamingContext context)
        {
			try
			{
				_Name=info.GetString("_Name");
			}
			catch
			{
				_Name=string.Empty;
			}
			try
			{
				_Field=info.GetString("_Field");
			}
			catch
			{
				_Field=string.Empty;
			}
			try
			{
				_GradingFields=(System.Collections.Specialized.StringCollection)info.GetValue("_GradingFields",typeof(System.Collections.Specialized.StringCollection));
			}
			catch
			{
				_GradingFields=new StringCollection();
			}
			try
			{
				_CommentField=info.GetString("_CommentField");
			}
			catch
			{
				_CommentField=string.Empty;
			}
        }
		#endregion


    }
    #endregion

    #region  public class GradingSection : WebbCollection
    [Serializable]
    public class GradingSection : WebbCollection
    {
        public GradingSection()
            : base()
        {
        }
        public GradingPostion this[int index]
        {
            get { return this.innerList[index] as GradingPostion; }

            set { this.innerList[index] = value; }
        }
        public GradingPostion this[string name]
        {
            get
            {
                foreach (GradingPostion element in this)
                {
                    if (element.Name == name) return element;
                }

                return null;
            }
        }

        protected string _SectionName = string.Empty;

        public int Add(GradingPostion playerPostion)
        {
            return this.innerList.Add(playerPostion);
        }
        public string SectionName
        {
            get
            {
                return this._SectionName;
            }
            set
            {
                this._SectionName = value;
            }
        }

        #region Serialization By Simon's Macro 2011-7-11 9:26:29
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue("_SectionName", _SectionName);

        }

        public GradingSection(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            try
            {
                _SectionName = info.GetString("_SectionName");
            }
            catch
            {
                _SectionName = string.Empty;
            }
        }
        #endregion

        public GradingSection Copy()
        {
            GradingSection gradingSection = new GradingSection();

            foreach (GradingPostion playerPosition in this)
            {
                gradingSection.Add(playerPosition.Copy());
            }

            return gradingSection;
        }

        public override string ToString()
        {
            return _SectionName;
        }
        public override bool Equals(object obj)
        {
            if(!(obj is GradingSection))return false;

            return (obj as GradingSection).SectionName==this.SectionName;
        }
        public override int  GetHashCode()
        {
 	         return base.GetHashCode();
        }
     
    }

    #endregion

    #region public class GradingSectionCollection
    [Serializable]
    public class GradingSectionCollection : WebbCollection
    {
        public static string[] GradingTableFields = new string[]{"SectionName",
                                                               "PostionField",
                                                               "GradeField",
                                                               "JerseyNo",
                                                               "Grade",
                                                                "CommentField",
                                                                "Comment"
                                                               };
        protected bool _ShowInRow = true;
        public GradingSectionCollection()
            : base()
        {
        }
        public GradingSection this[int index]
        {
            get { return this.innerList[index] as GradingSection; }

            set { this.innerList[index] = value; }
        }
        public GradingSection this[string name]
        {
            get
            {
                foreach (GradingSection element in this)
                {
                    if (element.SectionName == name) return element;
                }

                return null;
            }
        }
        public int Add(GradingSection playerPostionSection)
        {
            return this.innerList.Add(playerPostionSection);
        }

        #region Serialization By Simon's Macro 2011-7-11 15:08:15
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("_ShowInRow", _ShowInRow);

        }

        public GradingSectionCollection(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            try
            {
                _ShowInRow = info.GetBoolean("_ShowInRow");
            }
            catch
            {
                _ShowInRow = true;
            }
        }
        #endregion

        public GradingSectionCollection Copy()
        {
            GradingSectionCollection playerSectionCollection = new GradingSectionCollection();

            foreach (GradingSection playerSection in this)
            {
                playerSectionCollection.Add(playerSection.Copy());
            }
            return playerSectionCollection;
        }

        public DataTable CalcualteGradeTable(DataTable OriginalTable, Int32Collection i_Rows)
        {
            DataTable i_Table = new DataTable();

            i_Table.Columns.Add("RecordId", typeof(int));

            foreach (string strField in GradingTableFields)
            {
                i_Table.Columns.Add(strField, typeof(string));
            }

            foreach (GradingSection playerSection in this)
            {
                foreach (GradingPostion playerPosition in playerSection)
                {
                    string strPositionField = playerPosition.Field;

                    if (!OriginalTable.Columns.Contains(strPositionField)) continue;

                    foreach (int row in i_Rows)
                    {
                        foreach (string strGradeField in playerPosition.GradingFields)
                        {
                            #region Add Rows

                            if (!OriginalTable.Columns.Contains(strGradeField) || row < 0 || row >= OriginalTable.Rows.Count) continue;

                            DataRow originalDataRow = OriginalTable.Rows[row];

                            DataRow dr = i_Table.NewRow();

                            dr[0] = row;

                            dr[1] = playerSection.SectionName;

                            dr[2] = strPositionField;

                            dr[3] = strGradeField;

                            dr[4] = originalDataRow[strPositionField];

                            dr[5] = originalDataRow[strGradeField];

                            dr[6] = playerPosition.CommentField;

                            if (OriginalTable.Columns.Contains(playerPosition.CommentField))
                            {
                                dr[7] =originalDataRow[playerPosition.CommentField];
                            }
                            else
                            {
                                dr[7] = string.Empty;
                            }

                            i_Table.Rows.Add(dr);

                            #endregion
                        }
                    }

                }
            }

            return i_Table;
        }

        public void TruncateRowIdIntoPlayId(DataTable dt, GroupInfo groupInfo)
        {
            if (groupInfo == null||groupInfo.GroupResults==null) return;

            foreach (GroupResult groupResult in groupInfo.GroupResults)
            {
                Int32Collection realRowIndicators=new Int32Collection();

                foreach(int recorderIndex in groupResult.RowIndicators)
                {
                    try
                    {
                        int realrowIndicator=Convert.ToInt32(dt.Rows[recorderIndex][0]);

                        realRowIndicators.Add(realrowIndicator);
                    }
                    catch
                    {
                         realRowIndicators.Add(recorderIndex);
                    }  
                }

                groupResult.RowIndicators = realRowIndicators;
              
                foreach (GroupSummary groupSummary in groupResult.Summaries)
                {
                    Int32Collection summaryRows = new Int32Collection();

                    foreach (int recorderIndex in groupResult.RowIndicators)
                    {
                        try
                        {
                            int realrowIndicator = Convert.ToInt32(dt.Rows[recorderIndex][0]);

                            summaryRows.Add(realrowIndicator);
                        }
                        catch
                        {
                            summaryRows.Add(recorderIndex);
                        }
                    }
                    groupSummary.SetRowIndicators(summaryRows);
                }
             
                foreach (GroupInfo subGroupInfo in groupResult.SubGroupInfos)
                {
                    TruncateRowIdIntoPlayId(dt, subGroupInfo);
                }
            }
        } 
    }
    #endregion

    #region public class FieldGradingInfo
    [Serializable]
    public class FieldGradingInfo : FieldGroupInfo
    {
        public FieldGradingInfo() : base() { }
       
        [Browsable(true)]
        [EditorAttribute(typeof(Webb.Reports.Editors.SortingColumnEditor), typeof(System.Drawing.Design.UITypeEditor))]  //Added this code at 2008-12-25 14:52:27@Simon
        public new string GroupByField
        {
            get { return this._FieldName; }
            set
            {
                if (this._FieldName != value)
                {
                    this._FieldName = value;

                    this._GroupTitle = _FieldName;

                    this.UserDefinedOrders = new UserOrderClS();
                }

            }
        }
        [Category("Filters")]
        [EditorAttribute(typeof(Webb.Reports.Editors.PlayerGradingFilterEditor), typeof(System.Drawing.Design.UITypeEditor))]	//07-02-2008@Scott
        public new DBFilter Filter
        {
            get
            {
                if (this._Filter == null) this._Filter = new DBFilter();

                return this._Filter;
            }
            set { this._Filter = value; }
        }

        #region Serialization By Simon's Macro 2011-7-18 9:14:08
        override public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

        public FieldGradingInfo(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
        #endregion

        override public GroupInfo Copy()
        {
            FieldGradingInfo m_GroupInfo = new FieldGradingInfo();

            m_GroupInfo._AddGroupTotal = this._AddGroupTotal;

            if (this._FollowedSummaries != null)
            {
                m_GroupInfo._FollowedSummaries = this._FollowedSummaries.CopyStructure();

                if (this._AddGroupTotal)
                {
                    m_GroupInfo._TotalSummaries = this._FollowedSummaries.CopyStructure();
                }
            }
            //m_GroupInfo._GroupResults = this._AddGroupTotal;
            m_GroupInfo._GroupTitle = this._GroupTitle;
            m_GroupInfo._SortingByType = this._SortingByType;
            m_GroupInfo._SortingType = this._SortingType;
            m_GroupInfo.UserDefinedOrders = new UserOrderClS();
            m_GroupInfo.UserDefinedOrders.RelativeGroupInfo = m_GroupInfo;
            foreach (object objValue in this.UserDefinedOrders.OrderValues)
            {
                m_GroupInfo.UserDefinedOrders.OrderValues.Add(objValue);
            }
            m_GroupInfo._TopCount = this._TopCount;
            m_GroupInfo._FieldName = this._FieldName;
            m_GroupInfo._TotalTitle = this._TotalTitle;
            m_GroupInfo.Style = this.Style.Copy() as BasicStyle;
            m_GroupInfo._ClickEvent = this._ClickEvent;
            m_GroupInfo._FollowSummaries = this._FollowSummaries;
            m_GroupInfo._TitleFormat = this._TitleFormat;
            m_GroupInfo.Filter = this.Filter.Copy();
            m_GroupInfo.ColumnWidth = this.ColumnWidth;

            m_GroupInfo.ColumnIndex = this.ColumnIndex;  //Added this code at 2008-12-24 8:30:51@Simon

            m_GroupInfo.ReportScType = this.ReportScType;	//Modified at 2009-1-21 14:05:45@Scott

            m_GroupInfo.Visible = this.Visible;   //Added this code at 2009-2-1 15:52:23@Simon

            m_GroupInfo.ColorNeedChange = this.ColorNeedChange;	//Modified at 2009-2-11 16:13:38@Scott

            m_GroupInfo._GroupSides = this._GroupSides;  //Add at 2009-2-27 9:31:19@Simon

            m_GroupInfo.FollowsWith = this.FollowsWith;

            m_GroupInfo._IsRelatedForSubGroup = this._IsRelatedForSubGroup;

            m_GroupInfo._ShowSymbol = this._ShowSymbol;

            m_GroupInfo.DisregardBlank = this.DisregardBlank;

            m_GroupInfo._OneValuePerRow = this._OneValuePerRow;

            m_GroupInfo.DateFormat = this.DateFormat;

            m_GroupInfo.SkippedCount = this.SkippedCount;

            if (this._SectionSummeries != null)
            {
                m_GroupInfo._SectionSummeries = this._SectionSummeries.CopyStructure();
            }

            m_GroupInfo.IsSectionOutSide = false;

            m_GroupInfo._Description = this._Description;

            m_GroupInfo.DisplayAsColumn = this.DisplayAsColumn;

            m_GroupInfo._DisplayAsImage = this._DisplayAsImage;

            return m_GroupInfo;
        }


    }
    #endregion
}
