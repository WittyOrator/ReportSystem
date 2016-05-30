using System;
using System.Collections.Generic;
using System.Text;
using Webb.Data;
using Webb.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Runtime.Serialization;

namespace Webb.Reports.ExControls.Data
{
    public enum GradingType
    {
        Frenquency,
        Percent
    }
    
    [Serializable]
    public class GradingSummary :GroupSummary
    {
        #region Auto Constructor By Macro 2011-7-11 14:36:31
		public GradingSummary():base()
        {			
        }
      
		#endregion  

        [Browsable(true), Category("Filters")]
        [EditorAttribute(typeof(Webb.Reports.Editors.PlayerGradingFilterEditor), typeof(System.Drawing.Design.UITypeEditor))]	//Modified at 2008-9-25 13:46:36@Simon
        public new Webb.Data.DBFilter Filter
        {
            get
            {
                if (_Filter == null) _Filter = new DBFilter();
                return _Filter;
            }
            set
            {
                _Filter = value;
            }
        }

        [Browsable(true), Category("Filters")]
        [EditorAttribute(typeof(Webb.Reports.Editors.PlayerGradingFilterEditor), typeof(System.Drawing.Design.UITypeEditor))]	//Modified at 2008-9-25 13:46:36@Simon
        public new DBFilter DenominatorFilter
        {
            get
            {
                if (_DenominatorFilter == null) _DenominatorFilter = new DBFilter();
                return _DenominatorFilter;
            }
            set
            {
                _DenominatorFilter = value;
            }
        }

        #region Serialization By Simon's Macro 2011-7-18 9:25:06
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

        public GradingSummary(SerializationInfo info, StreamingContext context):base(info,context)
        {
        }
		#endregion

        #region Copy & ToString Function
        public override GroupSummary Copy()
        {
            GradingSummary m_Summary = new GradingSummary();

            m_Summary.Apply(this);

            return m_Summary;
        }

        public override string ToString()
        {
            return string.Format("{2}:{0}({1})", this._SummaryType, this._RelatedFieldName, this._SummaryTitle);
        }
        #endregion 

      
    }


   
   
}
