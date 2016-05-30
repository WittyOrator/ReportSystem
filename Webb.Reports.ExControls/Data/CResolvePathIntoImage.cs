using System;
using System.Collections.Generic;
using System.Text;
using Webb.Reports;
using Webb.Reports.DataProvider;
using Webb.Reports.ExControls.Views;

namespace Webb.Reports.ExControls.Data
{

    public class CResolveFieldValue
    {
        private bool _DisplayAsImage = false;
        private int _ColumnWidth = -1;
        private string _Field = string.Empty;

        public CResolveFieldValue(string strField)
        {
            _Field = strField;
            _DisplayAsImage = false;

            WebbDataProvider dataProvider = VideoPlayBackManager.PublicDBProvider;

            if (dataProvider != null && dataProvider.DBSourceConfig != null)
            {
                if (dataProvider.DBSourceConfig.WebbDBType == WebbDBTypes.WebbPlaybook)
                {
                    if (strField == "ImagePath")
                    {
                        _DisplayAsImage = true;

                        this._ColumnWidth = 300;

                    }
                }
                else if (dataProvider.DBSourceConfig.WebbDBType == WebbDBTypes.CoachCRM)
                {
                    if (strField == "Logo(TeamInfo)" || strField == "Photo(Coach)" || strField == "Photo(Player)")
                    {
                        _DisplayAsImage = true;

                        this._ColumnWidth = 80;

                    }
                }
                else if ((int)dataProvider.DBSourceConfig.WebbDBType < 10)
                {
                    PlayBookData playBookdata = new PlayBookData();

                    if (playBookdata.CheckIsPlaybookImageField(strField))
                    {
                        _DisplayAsImage = true;

                        this._ColumnWidth = 300;
                    }
                }
            }
        }


        public static object GetResolveValue(GroupInfo groupInfo, object objValue)
        {
            if (objValue == null || objValue is System.DBNull) return string.Empty;

            object returnedValue = objValue;

            if (groupInfo is FieldGroupInfo)
            {
                FieldGroupInfo fieldGroupInfo = groupInfo as FieldGroupInfo;

                string groupField = fieldGroupInfo.GroupByField;

                string dataFormat = fieldGroupInfo.DateFormat;

                if (objValue.GetType() == typeof(DateTime) && dataFormat != string.Empty)
                {
                    returnedValue = Convert.ToDateTime(objValue).ToString(dataFormat, System.Globalization.DateTimeFormatInfo.InvariantInfo);
                }
                else if (VideoPlayBackManager.PublicDBProvider != null && VideoPlayBackManager.PublicDBProvider.IsFeetInchesData(groupField))
                {
                    string strValue = returnedValue.ToString().Trim();

                    strValue = strValue.Replace("FT IN", "'");

                    strValue = strValue.Replace("FT", "' ");

                    strValue = strValue.Replace(" IN", "\"");

                    strValue = strValue.Replace("IN", "\"");

                    strValue = strValue.TrimStart("' ".ToCharArray());

                    returnedValue = strValue;
                }
            }

            return returnedValue;

        }

        public static object GetResolveValue(GridColumn col, object objValue)
        {
            if (objValue == null || objValue is System.DBNull) return string.Empty;

            object returnedValue = objValue;

            string groupField = col.Field;

            string dataFormat = col.DateFormat;

            if (objValue.GetType() == typeof(DateTime) && dataFormat != string.Empty)
            {
                returnedValue = Convert.ToDateTime(objValue).ToString(dataFormat, System.Globalization.DateTimeFormatInfo.InvariantInfo);
            }
            else if (VideoPlayBackManager.PublicDBProvider != null && VideoPlayBackManager.PublicDBProvider.IsFeetInchesData(groupField))
            {
                string strValue = returnedValue.ToString().Trim();

                strValue = strValue.Replace("FT IN", "'");

                strValue = strValue.Replace("FT", "' ");

                strValue = strValue.Replace(" IN", "\"");

                strValue = strValue.Replace("IN", "\"");

                strValue = strValue.TrimStart("' ".ToCharArray());

                returnedValue = strValue;

            }

            return returnedValue;

        }

        public static object GetResolveValue(string groupField, string _DateFormat, object objValue)
        {
            if (objValue == null || objValue is System.DBNull) return string.Empty;

            object returnedValue = objValue;

            if (objValue.GetType() == typeof(DateTime) && _DateFormat != string.Empty)
            {
                returnedValue = Convert.ToDateTime(objValue).ToString(_DateFormat, System.Globalization.DateTimeFormatInfo.InvariantInfo);
            }
            else if (VideoPlayBackManager.PublicDBProvider != null && VideoPlayBackManager.PublicDBProvider.IsFeetInchesData(groupField))
            {
                string strValue = returnedValue.ToString().Trim();

                strValue = strValue.Replace("FT IN", "'");

                strValue = strValue.Replace("FT", "' ");

                strValue = strValue.Replace(" IN", "\"");

                strValue = strValue.Replace("IN", "\"");

                strValue = strValue.TrimStart("' ".ToCharArray());

                returnedValue = strValue;

            }

            return returnedValue;
        }

        public bool DisplayAsImage
        {
            get
            {
                return _DisplayAsImage;
            }
        }

        public int ColumnWidth
        {
            get
            {
                return _ColumnWidth;
            }
        }

    }
}
