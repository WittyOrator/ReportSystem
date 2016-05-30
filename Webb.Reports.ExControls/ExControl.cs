
/***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:Class.cs
 * Author:Country.Wu [EMail:Webb.Country.Wu@163.com]
 * Create Time:10/31/2007 03:08:16 PM
 * Copyright:1986-2007@Webb Electronics all right reserved.
 * Purpose:
 * ***********************************************************************/
#region History
/*
 * //Author@DateTime : Description
 * */
#endregion History

#define BROWSABLE


using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Configuration;
using System.Timers;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Drawing;

using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization;

using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Container;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraPrinting;
using DevExpress.Utils.Design;
using DevExpress.Utils;
using DevExpress.XtraReports;
using DevExpress.XtraReports.UI;

using Webb;
using Webb.Data;
using Webb.Utilities;
using Webb.Reports.ExControls;
using Webb.Reports.ExControls.Views;
using Webb.Reports.ExControls.Design;
using Webb.Reports.ExControls.Data;

using CurrentDesign = DevExpress.Utils.Design;  //08-13-2008@Scott
using Webb.Reports.DataProvider;



namespace Webb.Reports.ExControls
{
    //	public enum GroupTypes{NoGroup = 0, MultiGroup = 1, NestingGroup = 2, BandedGroup = 3,};
    [Serializable]
    public enum FootballTypes { Both = 0, Offense = 1, Defense = 2, }
    [Serializable]
    public enum ClickEvents { None = 0, PlayVideo = 1, }

    #region public class ResourceManager
    /*Descrition:   */
    public class ResourceManager
    {
        //Wu.Country@2007-10-31 02:50 PM added this class.
        //Fields

        //Properties

        //ctor
        public ResourceManager()
        {
        }
        //Methods
    }
    #endregion

    #region public class ExControl : EditorContainer,IExControl
    /*Descrition:   */
    [ToolboxItem("Extended Control"),
    XRDesigner("Webb.Reports.ExControls.Design.ExControlDesigner,Webb.Reports.ExControls"),
    Designer("Webb.Reports.ExControls.Design.ExControlDesigner,Webb.Reports.ExControls"),
    CurrentDesign.Docking(CurrentDesign.DockingBehavior.Ask), CurrentDesign.ComplexBindingProperties("DataSource", "DataMember"),
    ToolboxBitmap(typeof(Webb.Reports.ExControls.ResourceManager), "Bitmaps.Group.bmp")
    ]
    public class ExControl : EditorContainer, IExControl
    {
        public static bool printRepeat = false;

        public static RectInfoCollection ContainerRectInfos = new RectInfoCollection();

        public static RectInfoCollection ShapeContainers = new RectInfoCollection();

        public static RepeatControlCollection RepeatContainers = new RepeatControlCollection();

        #region Get Page Filters

        #region Sub functions

        private DataTable ResolveOneValueOrRepeatBasedTable(PageGroupInfo pageGroupInfo)
        {
            DataSet gameSet = Webb.Reports.DataProvider.VideoPlayBackManager.DataSource;

            if (gameSet == null || gameSet.Tables.Count == 0) return null;

            DataTable gameTable = gameSet.Tables[0];

            Webb.Reports.DataProvider.WebbDataProvider PublicDataProvider = Webb.Reports.DataProvider.VideoPlayBackManager.PublicDBProvider;

            if (PublicDataProvider != null && PublicDataProvider.DBSourceConfig != null && PublicDataProvider.DBSourceConfig.WebbDBType == WebbDBTypes.CoachCRM && gameSet.Tables.Count > 1)
            {
                System.Data.DataView dv = new System.Data.DataView(gameTable);

                #region Fields

                ArrayList usedFields = new ArrayList();

                pageGroupInfo.GetAllUsedFields(ref usedFields);

                bool containsCoach = false;

                bool containsPlayer = false;

                bool containsTeam = false;

                foreach (string strField in usedFields)
                {
                    if (strField.EndsWith("(Coach)") || strField == "CoachInfoID")
                    {
                        containsCoach = true;
                    }
                    else if (strField.IndexOf("(TeamInfo)") >= 0 || strField == "TeamInfoID")
                    {
                        containsTeam = true;
                    }
                    else
                    {
                        containsPlayer = true;
                    }
                }
                if (containsCoach && containsPlayer)
                {
                    dv.RowFilter = "PlayerInfoID is not null or CoachInfoID is not null";
                }
                else if (containsCoach)
                {
                    dv.RowFilter = "CoachInfoID is not null";
                }
                else
                {
                    dv.RowFilter = "PlayerInfoID is not null";
                }
                #endregion

                gameTable = dv.ToTable();
            }

            return gameTable;
        }


        private SectionFilterCollection ConvertResultsIntoSectionFilters(PageGroupInfo setting, GroupResultCollection m_Results)
        {
            SectionFilterCollection scFilters = new SectionFilterCollection();

            PageFieldInfo fieldInfo = setting as PageFieldInfo;

            if (fieldInfo != null && fieldInfo.TotalAll == TotalType.AllBefore)
            {
                DBFilter filter = new DBFilter();

                SectionFilter scFilter = new SectionFilter(filter);

                scFilter.FilterName = fieldInfo.TotalAllName;

                scFilters.Add(scFilter);
            }

            int resultCount = m_Results.Count;

            if (setting.TopCount > 0) resultCount = Math.Min(m_Results.Count, setting.TopCount);

            DBFilter otherFilter = new DBFilter();

            for (int i = 0; i < resultCount; i++)
            {
                Data.GroupResult result = m_Results[i];

                #region   Others Not Equal

                if (result.Filter.Count > 0)
                {
                    DBCondition condition = result.Filter[0].Copy();

                    if (condition.FilterType == FilterTypes.NumEqual)
                    {
                        condition.FilterType = FilterTypes.NumNotEqual;
                    }
                    else
                    {
                        condition.FilterType = FilterTypes.StrNotEqual;
                    }

                    condition.FollowedOperand = FilterOperands.And;

                    otherFilter.Add(condition);
                }
                #endregion

                StringBuilder sbFilterName = new StringBuilder();

                sbFilterName.Append(result.GroupValue);

                DBFilter groupFilter = result.Filter.Copy();

                if (setting.ValuesInPage > 1)
                {
                    for (int j = 1; j < setting.ValuesInPage; j++)
                    {
                        if (i + j >= resultCount) break;

                        Data.GroupResult result2 = m_Results[i + j];

                        sbFilterName.Append("," + result2.GroupValue);

                        groupFilter = DBFilter.AttachedTwoFilterWithOr(groupFilter, result2.Filter);

                        #region Other Filter
                        if (result2.Filter.Count > 0)
                        {
                            DBCondition condition2 = result2.Filter[0].Copy();

                            if (condition2.FilterType == FilterTypes.NumEqual)
                            {
                                condition2.FilterType = FilterTypes.NumNotEqual;
                            }
                            else
                            {
                                condition2.FilterType = FilterTypes.StrNotEqual;
                            }

                            condition2.FollowedOperand = FilterOperands.And;

                            otherFilter.Add(condition2);
                        }
                        #endregion
                    }

                    i += setting.ValuesInPage - 1;
                }


                SectionFilter scFilter = new SectionFilter(groupFilter);

                scFilter.FilterName = sbFilterName.ToString();

                scFilters.Add(scFilter);
            }

            if (fieldInfo != null && fieldInfo.TotalOther)
            {
                SectionFilter scFilter = new SectionFilter(otherFilter);

                scFilter.FilterName = fieldInfo.TotalOthersName;

                scFilters.Add(scFilter);
            }

            if (fieldInfo != null && fieldInfo.TotalAll == TotalType.AllAfter)
            {
                DBFilter filter = new DBFilter();

                SectionFilter scFilter = new SectionFilter(filter);

                scFilter.FilterName = fieldInfo.TotalAllName;

                scFilters.Add(scFilter);
            }

            return scFilters;

        }

        #endregion

        #region GetOneValuePerPageFilters()

        internal SectionFilterCollection GetOneValuePerPageFilters()
        {
            SectionFilterCollection scFilters = new SectionFilterCollection();

            if (!this.Report.Template.OneValuePerPage) return scFilters;

            DataTable gameTable = ResolveOneValueOrRepeatBasedTable(this.Report.Template.OneValueSetting);

            if (gameTable == null) return scFilters;

            Webb.Collections.Int32Collection rows = this.Report.Filter.GetFilteredRows(gameTable);

            GroupInfo groupInfo = GroupInfo.FromPageGroupInfo(this.Report.Template.OneValueSetting);

            groupInfo.TopCount = 0;

            groupInfo.CalculateGroupResult(gameTable, rows, rows, groupInfo);

            GroupResultCollection m_Results = new GroupResultCollection();

            groupInfo.GetLeafGroupResults(groupInfo, ref m_Results);

            scFilters = ConvertResultsIntoSectionFilters(this.Report.Template.OneValueSetting, m_Results);

            return scFilters;
        }

        #region Old  GetOneValuePerPageFilters  //2009-3-6 8:54:48@Simon
        //		internal SectionFilterCollection GetOneValuePerPageFilters()
        //		{
        //			SectionFilterCollection scFilters = new SectionFilterCollection();
        //
        //			//		if(this.Report.Template.OneValuePerPage || this.Report.Template.RepeatedReport)  //Hide this code at 2008-12-25 15:14:28@Simon
        //			if(this.Report.Template.OneValuePerPage )
        //			{
        //				string strGroupByField = this.Report.Template.GroupByField;
        //
        //				if(Webb.Data.PublicDBFieldConverter.AvialableFields.Contains(strGroupByField) && strGroupByField != string.Empty)
        //				{//group by field
        //					DataSet gameSet = Webb.Reports.DataProvider.VideoPlayBackManager.DataSource;
        //
        //					if(gameSet != null && gameSet.Tables.Count > 0)
        //					{
        //						DataTable gameTable = gameSet.Tables[0];	//get data source
        //
        //						Webb.Collections.Int32Collection rows = this.Report.Filter.GetFilteredRows(gameTable);	//Modified at 2008-10-24 16:27:42@Scott
        //
        //						Data.FieldGroupInfo groupInfo = new Webb.Reports.ExControls.Data.FieldGroupInfo(strGroupByField);	//create field group
        //
        //						groupInfo.CalculateGroupResult(gameTable,rows,rows,groupInfo);	//calculate group result
        //
        //						groupInfo.GroupResults.Sort(Data.SortingTypes.Descending,Data.SortingByTypes.Frequence);
        //
        //						foreach(Data.GroupResult result in groupInfo.GroupResults)
        //						{//make section filters
        //							SectionFilter scFilter = new SectionFilter(result.Filter);
        //
        //							scFilter.FilterName = result.GroupValue.ToString();
        //
        //							scFilters.Add(scFilter);
        //						}
        //					}
        //				}
        //				else if(this.Report.Template.GroupByFields.Count > 0)
        //				{//group by fields	//12-12-2008@Scott
        //					DataSet gameSet = Webb.Reports.DataProvider.VideoPlayBackManager.DataSource;
        //
        //					if(gameSet != null && gameSet.Tables.Count > 0)
        //					{
        //						DataTable gameTable = gameSet.Tables[0];	//get data source
        //
        //						Webb.Collections.Int32Collection rows = this.Report.Filter.GetFilteredRows(gameTable);
        //
        //						Data.FieldsGroupInfo fieldsGroupInfo = new Webb.Reports.ExControls.Data.FieldsGroupInfo();
        //						fieldsGroupInfo.GroupByFields = this.Report.Template.GroupByFields;
        //
        //						fieldsGroupInfo.FieldsGroupStyle=this.Report.Template.FieldsStyle;  //2009-3-4 15:24:46@Simon
        //
        //						fieldsGroupInfo.CalculateGroupResult(gameTable,rows,rows,fieldsGroupInfo);   			
        //
        //						foreach(Data.GroupResult result in fieldsGroupInfo.GroupResults)
        //						{//make section filters
        //							SectionFilter scFilter = new SectionFilter(result.Filter);
        //
        //							scFilter.FilterName = result.GroupValue.ToString();
        //
        //							scFilters.Add(scFilter);
        //						}
        //					}
        //				}
        //				else
        //				{
        //					scFilters.Apply(this.Report.GroupBySectionFilters);	//Modified at 2008-12-18 11:14:28@Scott
        //					//scFilters.Apply(this.Report.Template.SectionFilters);
        //				}
        //			}
        //
        //			return scFilters;
        //		}
        #endregion
        #endregion

        #region GetRepeatFilters
        internal SectionFilterCollection GetRepeatFilters(SectionFilter OutFilter)    //Added this code at 2008-12-25 15:05:54@Simon
        {
            SectionFilterCollection scFilters = new SectionFilterCollection();

            DataSet gameSet = Webb.Reports.DataProvider.VideoPlayBackManager.DataSource;

            if (gameSet == null || gameSet.Tables.Count <= 0 || !this.Report.Template.RepeatedReport) return scFilters;

            #region Old

            //DataTable gameTable = gameSet.Tables[0];	//get data source

            //Webb.Reports.DataProvider.WebbDataProvider PublicDataProvider = Webb.Reports.DataProvider.VideoPlayBackManager.PublicDBProvider;

            //if (PublicDataProvider != null && PublicDataProvider.DBSourceConfig != null && PublicDataProvider.DBSourceConfig.WebbDBType == WebbDBTypes.CoachCRM && gameSet.Tables.Count > 1)
            //{
            //    DataView dv = new DataView(gameTable);

            //    #region Fields

            //    ArrayList usedFields = new ArrayList();

            //    this.Report.Template.RepeatSetting.GetAllUsedFields(ref usedFields);

            //    bool containsCoach = false;

            //    bool containsTeam = false;

            //    bool containsPlayer = false;

            //    foreach (string strField in usedFields)
            //    {
            //        if (strField.EndsWith("(Coach)"))
            //        {
            //            containsCoach = true;
            //        }
            //        else if (strField.IndexOf("(TeamInfo)") >= 0)
            //        {
            //            containsTeam = true;
            //        }
            //        else
            //        {
            //            containsPlayer = true;
            //        }
            //    }
            //    if (containsCoach && containsPlayer)
            //    {
            //        dv.RowFilter = "PlayerInfoID is not null or CoachInfoID is not null";
            //    }
            //    else if (containsCoach)
            //    {
            //        dv.RowFilter = "CoachInfoID is not null";
            //    }
            //    else
            //    {
            //        dv.RowFilter = "PlayerInfoID is not null";
            //    }
            //    #endregion

            //    gameTable = dv.ToTable();
            //}
            #endregion

            DataTable gameTable = ResolveOneValueOrRepeatBasedTable(this.Report.Template.RepeatSetting);

            if (gameTable == null) return scFilters;

            Webb.Collections.Int32Collection rows = this.Report.Filter.GetFilteredRows(gameTable);

            if (OutFilter != null && this.Report.Template.RepeatSetting.TopCount == 0)
            {
                rows = OutFilter.Filter.GetFilteredRows(gameTable, rows);
            }

            GroupInfo groupInfo = GroupInfo.FromPageGroupInfo(this.Report.Template.RepeatSetting);

            //groupInfo.TopCount = this.Report.Template.RepeatTopCount;

            groupInfo.CalculateGroupResult(gameTable, rows, rows, groupInfo);

            GroupResultCollection m_Results = new GroupResultCollection();

            groupInfo.GetLeafGroupResults(groupInfo, ref m_Results);

            scFilters = ConvertResultsIntoSectionFilters(this.Report.Template.RepeatSetting, m_Results);

            return scFilters;
        }


        internal SectionFilterCollection GetRepeatFilters(SectionFilter OutFilter, PageGroupInfo pagegroupInfo, int RepeatTopCount)    //Added this code at 2008-12-25 15:05:54@Simon
        {
            SectionFilterCollection scFilters = new SectionFilterCollection();

            DataTable gameTable = ResolveOneValueOrRepeatBasedTable(pagegroupInfo);

            if (gameTable == null) return scFilters;

            Webb.Collections.Int32Collection rows = this.Report.Filter.GetFilteredRows(gameTable);

            if (OutFilter != null)
            {
                rows = OutFilter.Filter.GetFilteredRows(gameTable, rows);
            }

            GroupInfo groupInfo = GroupInfo.FromPageGroupInfo(pagegroupInfo);

            //groupInfo.TopCount = RepeatTopCount;

            groupInfo.CalculateGroupResult(gameTable, rows, rows, groupInfo);

            GroupResultCollection m_Results = new GroupResultCollection();

            groupInfo.GetLeafGroupResults(groupInfo, ref m_Results);

            scFilters = ConvertResultsIntoSectionFilters(this.Report.Template.RepeatSetting, m_Results);

            return scFilters;
        }
        #endregion

        #endregion

        #region Print Filterd Controls

        //To print all OnevaluescFilter and RepeatFilter in a page  at 2008-12-25 16:04:10@Simon
        #region print OneValue Excontrols In Report Template
        internal int PrintOneValueAutoLayout(int xStartPos, int yStartPos, SectionFilter scFilter, string areaName, IBrickGraphics graph)
        {
            int yEndPos = 0;

            PrintInfo LastInfo = new PrintInfo();
            PrintInfo LastContainerInfo = new PrintInfo();

            #region print ExControls In ContainerControl
            foreach (WinControlContainer m_Container in WebbReport.ExControls)
            {
                ExControl m_Control = m_Container.WinControl as ExControl;

                if (m_Control == null || m_Control.Repeat || m_Control is ContainerControl) continue;

                if ((m_Control is ExShapeControl) && (m_Control as ExShapeControl).AutoFit) continue;

                if ((m_Control is RepeatControl) || RepeatContainers.HasContainControl(m_Control)) continue;

                int nEndPos = 0;

                int ControlIndex = ContainerRectInfos.Indexof(m_Control);

                if (ControlIndex < 0) continue;

                int offsetX = xStartPos + (int)(m_Control.XtraContainer.Left / Webb.Utility.ConvertCoordinate);

                int offsetY = yStartPos + (int)(m_Control.XtraContainer.Top / Webb.Utility.ConvertCoordinate);

                if (scFilter != null)
                {
                    m_Control.MainView.OneValueScFilter.Apply(scFilter);

                    if (!(m_Control is ReportInfoLabel)) m_Control.CalculateResult();
                }

                LastContainerInfo = ContainerRectInfos[ControlIndex].PrintInfo;

                if (LastContainerInfo.Bottom < offsetY)
                {
                    ContainerRectInfos.UpdateLineInfo(m_Control, true);
                }
                else if (LastContainerInfo.CtrlBounds.Right <= m_Control.XtraContainer.Left)
                {
                    offsetX = Math.Max(offsetX, LastContainerInfo.Right);

                    offsetY = Math.Max(offsetY, LastContainerInfo.Top);

                    ContainerRectInfos.UpdateLineInfo(m_Control, false);

                }
                else
                {
                    ContainerRectInfos.UpdateLineInfo(m_Control, true);

                    int lastbootom = ContainerRectInfos[ControlIndex].LinePrintInfo.LastLineCtrlBottom;

                    int distance = m_Control.XtraContainer.Top - ContainerRectInfos[ControlIndex].LinePrintInfo.LastLineCtrlBottom;

                    if (ContainerRectInfos[ControlIndex].KeepDistance && distance > 0 && lastbootom > 0)
                    {
                        offsetY = LastContainerInfo.Bottom + (int)(distance / Webb.Utility.ConvertCoordinate);

                    }
                    else
                    {
                        offsetY = LastContainerInfo.Bottom;
                    }
                }

                int nEndX = 0;

                nEndPos = m_Control.InternalCreateArea(offsetX, offsetY, areaName, graph, out nEndX);

                ContainerRectInfos.UpdatePrintInfo(m_Control, offsetX, nEndX, offsetY, nEndPos);

                ShapeContainers.UpdatePrintInfo(m_Control, offsetX, nEndX, offsetY, nEndPos);

                ShapeContainers.UpdateLineInfo(m_Control, false);

                yEndPos = Math.Max(yEndPos, nEndPos);	//get bottom

            }
            #endregion

            #region print ExControls out of ContainerControl
            foreach (WinControlContainer m_Container in WebbReport.ExControls)
            {
                ExControl m_Control = m_Container.WinControl as ExControl;

                if (m_Control == null || m_Control.Repeat || m_Control is ContainerControl) continue;

                if ((m_Control is ExShapeControl) && (m_Control as ExShapeControl).AutoFit) continue;

                if ((m_Control is RepeatControl) || RepeatContainers.HasContainControl(m_Control)) continue;

                int nEndPos = 0;

                int ControlIndex = ContainerRectInfos.Indexof(m_Control);

                if (ControlIndex >= 0) continue;

                int offsetX = xStartPos + (int)(m_Control.XtraContainer.Left / Webb.Utility.ConvertCoordinate);

                int offsetY = yStartPos + (int)(m_Control.XtraContainer.Top / Webb.Utility.ConvertCoordinate);

                if (scFilter != null)
                {
                    m_Control.MainView.OneValueScFilter.Apply(scFilter);

                    if (!(m_Control is ReportInfoLabel)) m_Control.CalculateResult();
                }

                if (LastInfo.Bottom < offsetY)
                {
                }
                else if (LastInfo.CtrlBounds.Right <= m_Control.XtraContainer.Left)
                {
                    offsetX = Math.Max(offsetX, LastInfo.Right);

                    offsetY = Math.Max(offsetY, LastInfo.Top);

                }
                else
                {
                    offsetY = LastInfo.Bottom;
                }

                int nEndX = 0;

                nEndPos = m_Control.InternalCreateArea(offsetX, offsetY, areaName, graph, out nEndX);

                LastInfo.Update(m_Control, offsetX, nEndX, offsetY, nEndPos);

                ShapeContainers.UpdatePrintInfo(m_Control, offsetX, nEndX, offsetY, nEndPos);

                ShapeContainers.UpdateLineInfo(m_Control, false);

                yEndPos = Math.Max(yEndPos, nEndPos);	//get bottom

            }
            #endregion

            #region print ShapeControl In AutoLayOut
            foreach (RectInfo rectInfo in ShapeContainers)
            {
                ExShapeControl m_Control = rectInfo.shapeControl;

                if (m_Control.Repeat || RepeatContainers.HasContainControl(m_Control)) continue;

                int nEndPos = 0;

                int offsetX = xStartPos + (int)(m_Control.XtraContainer.Left / Webb.Utility.ConvertCoordinate);

                int offsetY = yStartPos + (int)(m_Control.XtraContainer.Top / Webb.Utility.ConvertCoordinate);

                nEndPos = m_Control.InternalCreateShapeArea(offsetX, offsetY, areaName, graph, rectInfo);

                yEndPos = Math.Max(yEndPos, nEndPos);	//get bottom

            }
            #endregion

            return yEndPos;
        }
        internal int PrintOneValueExcontrolsOnce(int xStartPos, int yStartPos, SectionFilter scFilter, string areaName, IBrickGraphics graph)
        {
            int yEndPos = 0;

            PrintInfo LastContainerInfo = new PrintInfo();

            #region print ExControls In ContainerControl
            foreach (WinControlContainer m_Container in WebbReport.ExControls)
            {
                ExControl m_Control = m_Container.WinControl as ExControl;

                if (m_Control == null || m_Control.Repeat || m_Control is ContainerControl) continue;

                if ((m_Control is ExShapeControl) && (m_Control as ExShapeControl).AutoFit) continue;

                if ((m_Control is RepeatControl) || RepeatContainers.HasContainControl(m_Control)) continue;

                int nEndPos = 0;

                int ControlIndex = ContainerRectInfos.Indexof(m_Control);

                if (ControlIndex < 0) continue;

                int offsetX = xStartPos + (int)(m_Control.XtraContainer.Left / Webb.Utility.ConvertCoordinate);

                int offsetY = yStartPos + (int)(m_Control.XtraContainer.Top / Webb.Utility.ConvertCoordinate);

                if (scFilter != null)
                {
                    m_Control.MainView.OneValueScFilter.Apply(scFilter);

                    if (!(m_Control is ReportInfoLabel)) m_Control.CalculateResult();
                }

                LastContainerInfo = ContainerRectInfos[ControlIndex].PrintInfo;

                if (LastContainerInfo.Bottom < offsetY)
                {
                    ContainerRectInfos.UpdateLineInfo(m_Control, true);
                }
                else if (LastContainerInfo.CtrlBounds.Right <= m_Control.XtraContainer.Left)
                {
                    offsetX = Math.Max(offsetX, LastContainerInfo.Right);

                    offsetY = Math.Max(offsetY, LastContainerInfo.Top);

                    ContainerRectInfos.UpdateLineInfo(m_Control, false);
                }
                else
                {
                    ContainerRectInfos.UpdateLineInfo(m_Control, true);

                    int lastbootom = ContainerRectInfos[ControlIndex].LinePrintInfo.LastLineCtrlBottom;

                    int distance = m_Control.XtraContainer.Top - ContainerRectInfos[ControlIndex].LinePrintInfo.LastLineCtrlBottom;

                    if (ContainerRectInfos[ControlIndex].KeepDistance && distance > 0 && lastbootom > 0)
                    {
                        offsetY = LastContainerInfo.Bottom + (int)(distance / Webb.Utility.ConvertCoordinate);

                    }
                    else
                    {
                        offsetY = LastContainerInfo.Bottom;
                    }
                }

                int nEndX = 0;

                nEndPos = m_Control.InternalCreateArea(offsetX, offsetY, areaName, graph, out nEndX);

                ContainerRectInfos.UpdatePrintInfo(m_Control, offsetX, nEndX, offsetY, nEndPos);

                ShapeContainers.UpdatePrintInfo(m_Control, offsetX, nEndX, offsetY, nEndPos);

                ShapeContainers.UpdateLineInfo(m_Control, false);

                yEndPos = Math.Max(yEndPos, nEndPos);	//get bottom

            }
            #endregion

            #region print Controls out of the Container
            foreach (WinControlContainer m_Container in WebbReport.ExControls)
            {
                ExControl m_Control = m_Container.WinControl as ExControl;

                if (m_Control == null || m_Control.Repeat || m_Control is ContainerControl) continue;

                if ((m_Control is ExShapeControl) && (m_Control as ExShapeControl).AutoFit) continue;

                if ((m_Control is RepeatControl) || RepeatContainers.HasContainControl(m_Control)) continue;

                int nEndPos = 0;

                int ControlIndex = ContainerRectInfos.Indexof(m_Control);

                if (ControlIndex >= 0) continue;

                int offsetX = xStartPos + (int)(m_Control.XtraContainer.Left / Webb.Utility.ConvertCoordinate);

                int offsetY = yStartPos + (int)(m_Control.XtraContainer.Top / Webb.Utility.ConvertCoordinate);

                if (scFilter != null)
                {
                    m_Control.MainView.OneValueScFilter.Apply(scFilter);

                    if (!(m_Control is ReportInfoLabel)) m_Control.CalculateResult();
                }

                int nEndX = 0;

                nEndPos = m_Control.InternalCreateArea(offsetX, offsetY, areaName, graph, out nEndX); ;	//create all controls	

                ShapeContainers.UpdatePrintInfo(m_Control, offsetX, nEndX, offsetY, nEndPos);

                ShapeContainers.UpdateLineInfo(m_Control, false);

                yEndPos = Math.Max(yEndPos, nEndPos);	//get bottom

            }
            #endregion

            #region print ShapeControl In AutoLayOut
            foreach (RectInfo rectInfo in ShapeContainers)
            {
                ExShapeControl m_Control = rectInfo.shapeControl;

                if (m_Control.Repeat || RepeatContainers.HasContainControl(m_Control)) continue;

                int nEndPos = 0;

                int offsetX = xStartPos + (int)(m_Control.XtraContainer.Left / Webb.Utility.ConvertCoordinate);

                int offsetY = yStartPos + (int)(m_Control.XtraContainer.Top / Webb.Utility.ConvertCoordinate);

                nEndPos = m_Control.InternalCreateShapeArea(offsetX, offsetY, areaName, graph, rectInfo);

                yEndPos = Math.Max(yEndPos, nEndPos);	//get bottom

            }
            #endregion

            return yEndPos;
        }
        #endregion

        #region print repeat Excontrols In Tempate Setting
        internal int PrintRepeatExcontrolsOnce(int xStartPos, int yStartPos, SectionFilter OneValueScFilter, SectionFilter RepeatscFilter, string areaName, IBrickGraphics graph)
        {
            int yEndPos = 0;

            PrintInfo LastContainerInfo = new PrintInfo();

            #region print ExControls In ContainerControl
            foreach (WinControlContainer m_Container in WebbReport.ExControls)
            {
                ExControl m_Control = m_Container.WinControl as ExControl;

                if (m_Control == null || !m_Control.Repeat || m_Control is ContainerControl) continue;

                if ((m_Control is ExShapeControl) && (m_Control as ExShapeControl).AutoFit) continue;

                if ((m_Control is RepeatControl) || RepeatContainers.HasContainControl(m_Control)) continue;

                int nEndPos = 0;

                int ControlIndex = ContainerRectInfos.Indexof(m_Control);

                if (ControlIndex < 0) continue;

                int offsetX = xStartPos + (int)(m_Control.XtraContainer.Left / Webb.Utility.ConvertCoordinate);

                int offsetY = yStartPos + (int)(m_Control.XtraContainer.Top / Webb.Utility.ConvertCoordinate);

                if (OneValueScFilter != null)
                {
                    m_Control.MainView.OneValueScFilter.Apply(OneValueScFilter);

                }
                if (RepeatscFilter != null)
                {
                    m_Control.MainView.RepeatFilter.Apply(RepeatscFilter);
                }

                if (!(m_Control is ReportInfoLabel))
                {
                    m_Control.CalculateResult();
                }

                LastContainerInfo = ContainerRectInfos[ControlIndex].PrintInfo;

                if (LastContainerInfo.Bottom < offsetY)
                {
                    ContainerRectInfos.UpdateLineInfo(m_Control, true);
                }
                else if (LastContainerInfo.CtrlBounds.Right <= m_Control.XtraContainer.Left)
                {
                    offsetX = Math.Max(offsetX, LastContainerInfo.Right);

                    offsetY = Math.Max(offsetY, LastContainerInfo.Top);

                    ContainerRectInfos.UpdateLineInfo(m_Control, false);
                }
                else
                {
                    ContainerRectInfos.UpdateLineInfo(m_Control, true);

                    int lastbootom = ContainerRectInfos[ControlIndex].LinePrintInfo.LastLineCtrlBottom;

                    int distance = m_Control.XtraContainer.Top - ContainerRectInfos[ControlIndex].LinePrintInfo.LastLineCtrlBottom;

                    if (ContainerRectInfos[ControlIndex].KeepDistance && distance > 0 && lastbootom > 0)
                    {
                        offsetY = LastContainerInfo.Bottom + (int)(distance / Webb.Utility.ConvertCoordinate);

                    }
                    else
                    {
                        offsetY = LastContainerInfo.Bottom;
                    }
                }

                int nEndX = 0;

                nEndPos = m_Control.InternalCreateArea(offsetX, offsetY, areaName, graph, out nEndX);

                ContainerRectInfos.UpdatePrintInfo(m_Control, offsetX, nEndX, offsetY, nEndPos);

                ShapeContainers.UpdatePrintInfo(m_Control, offsetX, nEndX, offsetY, nEndPos);

                ShapeContainers.UpdateLineInfo(m_Control, false);

                yEndPos = Math.Max(yEndPos, nEndPos);	//get bottom

            }
            #endregion

            #region print ExControls out of ContainerControl
            foreach (WinControlContainer m_Container in WebbReport.ExControls)
            {
                ExControl m_Control = m_Container.WinControl as ExControl;

                if (m_Control == null || !m_Control.Repeat || m_Control is ContainerControl) continue;

                if ((m_Control is ExShapeControl) && (m_Control as ExShapeControl).AutoFit) continue;

                if ((m_Control is RepeatControl) || RepeatContainers.HasContainControl(m_Control)) continue;

                int nEndPos = 0;

                int ControlIndex = ContainerRectInfos.Indexof(m_Control);

                if (ControlIndex >= 0) continue;

                int offsetX = xStartPos + (int)(m_Control.XtraContainer.Left / Webb.Utility.ConvertCoordinate);

                int offsetY = yStartPos + (int)(m_Control.XtraContainer.Top / Webb.Utility.ConvertCoordinate);

                if (OneValueScFilter != null)
                {
                    m_Control.MainView.OneValueScFilter.Apply(OneValueScFilter);

                }
                if (RepeatscFilter != null)
                {
                    m_Control.MainView.RepeatFilter.Apply(RepeatscFilter);
                }

                if (!(m_Control is ReportInfoLabel))
                {
                    m_Control.CalculateResult();
                }

                int nEndX = 0;

                nEndPos = m_Control.InternalCreateArea(offsetX, offsetY, areaName, graph, out nEndX); ;	//create all controls	

                ShapeContainers.UpdatePrintInfo(m_Control, offsetX, nEndX, offsetY, nEndPos);

                ShapeContainers.UpdateLineInfo(m_Control, false);

                yEndPos = Math.Max(yEndPos, nEndPos);	//get bottom

            }
            #endregion

            #region print ShapeControl In AutoLayOut

            foreach (RectInfo rectInfo in ShapeContainers)
            {
                ExShapeControl m_Control = rectInfo.shapeControl;

                if (!m_Control.Repeat || RepeatContainers.HasContainControl(m_Control)) continue;

                int nEndPos = 0;

                int offsetX = xStartPos + (int)(m_Control.XtraContainer.Left / Webb.Utility.ConvertCoordinate);

                int offsetY = yStartPos + (int)(m_Control.XtraContainer.Top / Webb.Utility.ConvertCoordinate);

                nEndPos = m_Control.InternalCreateShapeArea(offsetX, offsetY, areaName, graph, rectInfo);

                yEndPos = Math.Max(yEndPos, nEndPos);	//get bottom

            }
            #endregion

            return yEndPos;
        }

        internal int PrintRepeatOnceAutoLayOut(int xStartPos, int yStartPos, SectionFilter OneValueScFilter, SectionFilter RepeatscFilter, string areaName, IBrickGraphics graph)
        {
            int yEndPos = 0;

            PrintInfo LastInfo = new PrintInfo();

            PrintInfo LastContainerInfo = new PrintInfo();

            #region print ExControls In ContainerControl
            foreach (WinControlContainer m_Container in WebbReport.ExControls)
            {
                ExControl m_Control = m_Container.WinControl as ExControl;

                if (m_Control == null || !m_Control.Repeat || m_Control is ContainerControl) continue;

                if ((m_Control is ExShapeControl) && (m_Control as ExShapeControl).AutoFit) continue;

                if ((m_Control is RepeatControl) || RepeatContainers.HasContainControl(m_Control)) continue;

                int nEndPos = 0;

                int ControlIndex = ContainerRectInfos.Indexof(m_Control);

                if (ControlIndex < 0) continue;

                int offsetX = xStartPos + (int)(m_Control.XtraContainer.Left / Webb.Utility.ConvertCoordinate);

                int offsetY = yStartPos + (int)(m_Control.XtraContainer.Top / Webb.Utility.ConvertCoordinate);

                if (OneValueScFilter != null)
                {
                    m_Control.MainView.OneValueScFilter.Apply(OneValueScFilter);

                }
                if (RepeatscFilter != null)
                {
                    m_Control.MainView.RepeatFilter.Apply(RepeatscFilter);
                }

                if (!(m_Control is ReportInfoLabel))
                {
                    m_Control.CalculateResult();
                }

                LastContainerInfo = ContainerRectInfos[ControlIndex].PrintInfo;

                if (LastContainerInfo.Bottom < offsetY)
                {
                    ContainerRectInfos.UpdateLineInfo(m_Control, true);
                }

                else if (LastContainerInfo.CtrlBounds.Right <= m_Control.XtraContainer.Left)
                {
                    offsetX = Math.Max(offsetX, LastContainerInfo.Right);

                    offsetY = Math.Max(offsetY, LastContainerInfo.Top);

                    ContainerRectInfos.UpdateLineInfo(m_Control, false);
                }
                else
                {
                    ContainerRectInfos.UpdateLineInfo(m_Control, true);

                    int lastbootom = ContainerRectInfos[ControlIndex].LinePrintInfo.LastLineCtrlBottom;

                    int distance = m_Control.XtraContainer.Top - ContainerRectInfos[ControlIndex].LinePrintInfo.LastLineCtrlBottom;

                    if (ContainerRectInfos[ControlIndex].KeepDistance && distance > 0 && lastbootom > 0)
                    {
                        offsetY = LastContainerInfo.Bottom + (int)(distance / Webb.Utility.ConvertCoordinate);
                    }
                    else
                    {
                        offsetY = LastContainerInfo.Bottom;
                    }
                }

                int nEndX = 0;

                nEndPos = m_Control.InternalCreateArea(offsetX, offsetY, areaName, graph, out nEndX);

                ContainerRectInfos.UpdatePrintInfo(m_Control, offsetX, nEndX, offsetY, nEndPos);

                ShapeContainers.UpdatePrintInfo(m_Control, offsetX, nEndX, offsetY, nEndPos);

                yEndPos = Math.Max(yEndPos, nEndPos);	//get bottom

            }
            #endregion

            #region print ExControls out of ContainerControl
            foreach (WinControlContainer m_Container in WebbReport.ExControls)
            {
                ExControl m_Control = m_Container.WinControl as ExControl;

                if (m_Control == null || !m_Control.Repeat || m_Control is ContainerControl) continue;

                if ((m_Control is ExShapeControl) && (m_Control as ExShapeControl).AutoFit) continue;

                if ((m_Control is RepeatControl) || RepeatContainers.HasContainControl(m_Control)) continue;

                int nEndPos = 0;

                int ControlIndex = ContainerRectInfos.Indexof(m_Control);

                if (ControlIndex >= 0) continue;

                int offsetX = xStartPos + (int)(m_Control.XtraContainer.Left / Webb.Utility.ConvertCoordinate);

                int offsetY = yStartPos + (int)(m_Control.XtraContainer.Top / Webb.Utility.ConvertCoordinate);

                if (OneValueScFilter != null)
                {
                    m_Control.MainView.OneValueScFilter.Apply(OneValueScFilter);

                }
                if (RepeatscFilter != null)
                {
                    m_Control.MainView.RepeatFilter.Apply(RepeatscFilter);
                }

                if (!(m_Control is ReportInfoLabel))
                {
                    m_Control.CalculateResult();
                }

                if (LastInfo.Bottom < offsetY)
                {
                }
                else if (LastInfo.CtrlBounds.Right <= m_Control.XtraContainer.Left)
                {
                    offsetX = Math.Max(offsetX, LastInfo.Right);

                    offsetY = Math.Max(offsetY, LastInfo.Top);

                }
                else
                {
                    offsetY = LastInfo.Bottom;
                }

                int nEndX = 0;

                nEndPos = m_Control.InternalCreateArea(offsetX, offsetY, areaName, graph, out nEndX);

                LastInfo.Update(m_Control, offsetX, nEndX, offsetY, nEndPos);

                ShapeContainers.UpdatePrintInfo(m_Control, offsetX, nEndX, offsetY, nEndPos);

                ShapeContainers.UpdateLineInfo(m_Control, false);

                yEndPos = Math.Max(yEndPos, nEndPos);	//get bottom

            }
            #endregion

            #region print ShapeControl In AutoLayOut
            foreach (RectInfo rectInfo in ShapeContainers)
            {
                ExShapeControl m_Control = rectInfo.shapeControl;

                if (!m_Control.Repeat || RepeatContainers.HasContainControl(m_Control)) continue;

                int nEndPos = 0;

                int offsetX = xStartPos + (int)(m_Control.XtraContainer.Left / Webb.Utility.ConvertCoordinate);

                int offsetY = yStartPos + (int)(m_Control.XtraContainer.Top / Webb.Utility.ConvertCoordinate);

                nEndPos = m_Control.InternalCreateShapeArea(offsetX, offsetY, areaName, graph, rectInfo);

                yEndPos = Math.Max(yEndPos, nEndPos);	//get bottom

            }
            #endregion

            return yEndPos;
        }

        #endregion

        #region Repeat Container
        #region Old
        //#region print OneValue Excontrols In RepeatContainer
        //internal int PrintOneValueRepeatContainerAutoLayout(int yLastEndPos,int xStartPos, int yStartPos, SectionFilter scFilter,IBrickGraphics graph,RepeatControl m_RepeatControl)
        //{
        //    int yEndPos = 0;

        //    PrintInfo LastInfo=new PrintInfo();
        //    PrintInfo LastContainerInfo=new PrintInfo();

        //    #region print ExControls In ContainerControl  
        //    foreach(ExControl m_Control in m_RepeatControl.SubControls)
        //    {				
        //        if(m_Control.Repeat)continue;

        //        if(m_Control is ContainerControl)continue;

        //        int nEndPos =0;

        //        int ControlIndex=ContainerRectInfos.Indexof(m_Control);

        //        if(ControlIndex<0)continue;					

        //        int offsetX = xStartPos + (int)(m_Control.XtraContainer.Left/Webb.Utility.ConvertCoordinate);

        //        int offsetY = yStartPos + (int)(m_Control.XtraContainer.Top/Webb.Utility.ConvertCoordinate);

        //        if(m_RepeatControl.AfterLast&&yLastEndPos>0)
        //        {
        //            offsetY= yLastEndPos + (int)((m_Control.XtraContainer.Top-m_RepeatControl.XtraContainer.Top)/Webb.Utility.ConvertCoordinate);
        //        }

        //        if(scFilter != null)
        //        {					
        //            m_Control.MainView.OneValueScFilter.Apply(scFilter);				
        //        }
        //        if(!(m_Control is ReportInfoLabel))m_Control.CalculateResult();

        //        LastContainerInfo=ContainerRectInfos[ControlIndex].PrintInfo;					

        //        if(LastContainerInfo.Bottom<offsetY)
        //        {
        //            ContainerRectInfos.UpdateLineInfo(m_Control,true);						
        //        }
        //        else if(LastContainerInfo.CtrlBounds.Right<=m_Control.XtraContainer.Left)
        //        {
        //            offsetX=Math.Max(offsetX,LastContainerInfo.Right);

        //            offsetY=Math.Max(offsetY,LastContainerInfo.Top);

        //            ContainerRectInfos.UpdateLineInfo(m_Control,false);

        //        }
        //        else
        //        {
        //            ContainerRectInfos.UpdateLineInfo(m_Control,true);

        //            int lastbootom=ContainerRectInfos[ControlIndex].LinePrintInfo.LastLineCtrlBottom;

        //            int distance=m_Control.XtraContainer.Top-ContainerRectInfos[ControlIndex].LinePrintInfo.LastLineCtrlBottom;

        //            if(ContainerRectInfos[ControlIndex].KeepDistance&&distance>0&&lastbootom>0)
        //            {  
        //                offsetY=LastContainerInfo.Bottom+(int)(distance/Webb.Utility.ConvertCoordinate);

        //            }
        //            else
        //            {
        //                offsetY=LastContainerInfo.Bottom;
        //            }					
        //        }	

        //        int nEndX=0;

        //        nEndPos= m_Control.InternalCreateArea(offsetX,offsetY,"",graph,out nEndX);				

        //        ContainerRectInfos.UpdatePrintInfo(m_Control,offsetX,nEndX,offsetY,nEndPos);

        //        ShapeContainers.UpdatePrintInfo(m_Control,offsetX,nEndX,offsetY,nEndPos);

        //        yEndPos = Math.Max(yEndPos,nEndPos);	//get bottom

        //    } 
        //    #endregion

        //    #region print ExControls out of ContainerControl  
        //    foreach(ExControl m_Control in m_RepeatControl.SubControls)
        //    {				
        //        if(m_Control.Repeat)continue;

        //        if(m_Control is ContainerControl)continue;				

        //        int nEndPos =0;

        //        int ControlIndex=ContainerRectInfos.Indexof(m_Control);

        //        if(ControlIndex>=0)continue;

        //        int offsetX = xStartPos + (int)(m_Control.XtraContainer.Left/Webb.Utility.ConvertCoordinate);

        //        int offsetY = yStartPos + (int)(m_Control.XtraContainer.Top/Webb.Utility.ConvertCoordinate);	

        //        if(m_RepeatControl.AfterLast&&yLastEndPos>0)
        //        {
        //            offsetY= yLastEndPos + (int)((m_Control.XtraContainer.Top-m_RepeatControl.XtraContainer.Top)/Webb.Utility.ConvertCoordinate);
        //        }

        //        if(scFilter != null)
        //        {
        //            m_Control.MainView.OneValueScFilter.Apply(scFilter);					
        //        }
        //        if(!(m_Control is ReportInfoLabel))m_Control.CalculateResult();

        //        if(LastInfo.Bottom<offsetY)
        //        {
        //        }
        //        else if(LastInfo.CtrlBounds.Right<=m_Control.XtraContainer.Left)
        //        {
        //            offsetX=Math.Max(offsetX,LastInfo.Right);

        //            offsetY=Math.Max(offsetY,LastInfo.Top);

        //        }
        //        else
        //        {
        //            offsetY=LastInfo.Bottom;
        //        }	

        //        int nEndX=0;

        //        nEndPos= m_Control.InternalCreateArea(offsetX,offsetY,"",graph,out nEndX);				

        //        LastInfo.Update(m_Control,offsetX,nEndX,offsetY,nEndPos);	

        //        ShapeContainers.UpdatePrintInfo(m_Control,offsetX,nEndX,offsetY,nEndPos);

        //        yEndPos = Math.Max(yEndPos,nEndPos);	//get bottom

        //    } 
        //    #endregion



        //    return yEndPos;
        //}
        //internal int PrintOneValueRepeatContainerOnce(int yLastEndPos,int xStartPos, int yStartPos, SectionFilter scFilter,IBrickGraphics graph,RepeatControl m_RepeatControl)
        //{
        //    int yEndPos = 0;			

        //    PrintInfo LastContainerInfo=new PrintInfo();

        //    #region print ExControls In ContainerControl  
        //    foreach(ExControl m_Control in m_RepeatControl.SubControls)
        //    {
        //        if(m_Control.Repeat)continue;

        //        if(m_Control is ContainerControl)continue;

        //        int nEndPos =0;

        //        int ControlIndex=ContainerRectInfos.Indexof(m_Control);

        //        if(ControlIndex<0)continue;

        //        int offsetX = xStartPos + (int)(m_Control.XtraContainer.Left/Webb.Utility.ConvertCoordinate);

        //        int offsetY = yStartPos + (int)(m_Control.XtraContainer.Top/Webb.Utility.ConvertCoordinate);

        //        if(m_RepeatControl.AfterLast&&yLastEndPos>0)
        //        {
        //            offsetY= yLastEndPos + (int)((m_Control.XtraContainer.Top-m_RepeatControl.XtraContainer.Top)/Webb.Utility.ConvertCoordinate);
        //        }

        //        if(scFilter != null)
        //        {					
        //            m_Control.MainView.OneValueScFilter.Apply(scFilter);					
        //        }

        //        if(!(m_Control is ReportInfoLabel))m_Control.CalculateResult();

        //        LastContainerInfo=ContainerRectInfos[ControlIndex].PrintInfo;					

        //        if(LastContainerInfo.Bottom<offsetY)
        //        {
        //            ContainerRectInfos.UpdateLineInfo(m_Control,true);						
        //        }
        //        else if(LastContainerInfo.CtrlBounds.Right<=m_Control.XtraContainer.Left)
        //        {
        //            offsetX=Math.Max(offsetX,LastContainerInfo.Right);

        //            offsetY=Math.Max(offsetY,LastContainerInfo.Top);

        //            ContainerRectInfos.UpdateLineInfo(m_Control,false);						
        //        }
        //        else
        //        {
        //            ContainerRectInfos.UpdateLineInfo(m_Control,true);

        //            int lastbootom=ContainerRectInfos[ControlIndex].LinePrintInfo.LastLineCtrlBottom;

        //            int distance=m_Control.XtraContainer.Top-ContainerRectInfos[ControlIndex].LinePrintInfo.LastLineCtrlBottom;

        //            if(ContainerRectInfos[ControlIndex].KeepDistance&&distance>0&&lastbootom>0)
        //            {  
        //                offsetY=LastContainerInfo.Bottom+(int)(distance/Webb.Utility.ConvertCoordinate);

        //            }
        //            else
        //            {
        //                offsetY=LastContainerInfo.Bottom;
        //            }						
        //        }	

        //        int nEndX=0;

        //        nEndPos= m_Control.InternalCreateArea(offsetX,offsetY,"",graph,out nEndX);	

        //        ContainerRectInfos.UpdatePrintInfo(m_Control,offsetX,nEndX,offsetY,nEndPos);

        //        ShapeContainers.UpdatePrintInfo(m_Control,offsetX,nEndX,offsetY,nEndPos);

        //        yEndPos = Math.Max(yEndPos,nEndPos);	//get bottom

        //    } 
        //    #endregion

        //    #region print Controls out of the Container
        //    foreach(ExControl m_Control in m_RepeatControl.SubControls)
        //    {
        //        if(m_Control.Repeat)continue;

        //        if(m_Control is ContainerControl)continue;

        //        int nEndPos =0;

        //        int ControlIndex=ContainerRectInfos.Indexof(m_Control);

        //        if(ControlIndex>=0)continue;

        //        int offsetX = xStartPos + (int)(m_Control.XtraContainer.Left/Webb.Utility.ConvertCoordinate);

        //        int offsetY = yStartPos + (int)(m_Control.XtraContainer.Top/Webb.Utility.ConvertCoordinate);	

        //        if(m_RepeatControl.AfterLast&&yLastEndPos>0)
        //        {
        //            offsetY= yLastEndPos + (int)((m_Control.XtraContainer.Top-m_RepeatControl.XtraContainer.Top)/Webb.Utility.ConvertCoordinate);
        //        }

        //        if(scFilter != null)
        //        {
        //            m_Control.MainView.OneValueScFilter.Apply(scFilter);
        //        }

        //        if(!(m_Control is ReportInfoLabel))m_Control.CalculateResult();												

        //        int nEndX=0;

        //        nEndPos=m_Control.InternalCreateArea(offsetX,offsetY,"",graph,out nEndX);;	//create all controls					

        //        yEndPos = Math.Max(yEndPos,nEndPos);	//get bottom

        //    } 
        //    #endregion

        //    return yEndPos;
        //}
        //#endregion

        //#region Print Repeat all in RepeatContainers
        //internal int PrintRepeatContainerOnce(int yLastEndPos,int xStartPos, int yStartPos, SectionFilter OneValueScFilter,SectionFilter RepeatscFilter,IBrickGraphics graph,RepeatControl m_RepeatControl,ArrayList filterNames,out int xEndPos)
        //{
        //    int yEndPos = 0;

        //    xEndPos=0;

        //    PrintInfo LastContainerInfo=new PrintInfo();

        //    #region print ExControls In ContainerControl  
        //    foreach(ExControl m_Control in m_RepeatControl.SubControls)
        //    {	
        //        if(!m_Control.Repeat)continue;

        //        if(m_Control is ContainerControl)continue;			

        //        int nEndPos =0;

        //        int ControlIndex=ContainerRectInfos.Indexof(m_Control);

        //        if(ControlIndex<0)continue;

        //        int ReleativeLeft=m_Control.XtraContainer.Left-m_RepeatControl.XtraContainer.Left;

        //        int ReleativeTop=m_Control.XtraContainer.Top-m_RepeatControl.XtraContainer.Top;

        //        int offsetX = xStartPos + (int)(ReleativeLeft/Webb.Utility.ConvertCoordinate);

        //        int offsetY = yStartPos + (int)(ReleativeTop/Webb.Utility.ConvertCoordinate);	

        //        if(m_RepeatControl.AfterLast&&yLastEndPos>0)
        //        {
        //            offsetY= yLastEndPos + (int)(ReleativeTop/Webb.Utility.ConvertCoordinate);
        //        }

        //        if(OneValueScFilter != null)
        //        {
        //            m_Control.MainView.OneValueScFilter.Apply(OneValueScFilter);

        //        }
        //        if(RepeatscFilter != null)
        //        {	
        //            if(filterNames.Contains(RepeatscFilter.FilterName)&&(m_Control is ChartControlEx))
        //            {
        //                (m_Control as ChartControlEx).ColorWhenMax=m_RepeatControl.ChartColorWhenMax;
        //            }

        //            m_Control.MainView.RepeatFilter.Apply(RepeatscFilter);
        //        }

        //        if(!(m_Control is ReportInfoLabel))m_Control.CalculateResult();

        //        LastContainerInfo=ContainerRectInfos[ControlIndex].PrintInfo;					

        //        if(LastContainerInfo.Bottom<offsetY)
        //        {
        //            ContainerRectInfos.UpdateLineInfo(m_Control,true);						
        //        }
        //        else if(LastContainerInfo.CtrlBounds.Right<=m_Control.XtraContainer.Left)
        //        {
        //            offsetX=Math.Max(offsetX,LastContainerInfo.Right);

        //            offsetY=Math.Max(offsetY,LastContainerInfo.Top);

        //            ContainerRectInfos.UpdateLineInfo(m_Control,false);						
        //        }
        //        else
        //        {
        //            ContainerRectInfos.UpdateLineInfo(m_Control,true);

        //            int lastbootom=ContainerRectInfos[ControlIndex].LinePrintInfo.LastLineCtrlBottom;

        //            int distance=m_Control.XtraContainer.Top-ContainerRectInfos[ControlIndex].LinePrintInfo.LastLineCtrlBottom;

        //            if(ContainerRectInfos[ControlIndex].KeepDistance&&distance>0&&lastbootom>0)
        //            {  
        //                offsetY=LastContainerInfo.Bottom+(int)(distance/Webb.Utility.ConvertCoordinate);

        //            }
        //            else
        //            {
        //                offsetY=LastContainerInfo.Bottom;
        //            }					
        //        }	

        //        int nEndX=0;

        //        nEndPos= m_Control.InternalCreateArea(offsetX,offsetY,"",graph,out nEndX);

        //        xEndPos=Math.Max(xEndPos,nEndX);

        //        ContainerRectInfos.UpdatePrintInfo(m_Control,offsetX,nEndX,offsetY,nEndPos);				

        //        if(m_Control is ChartControlEx)
        //        {
        //            (m_Control as ChartControlEx).ColorWhenMax=Color.Empty;
        //        }

        //        yEndPos = Math.Max(yEndPos,nEndPos);	//get bottom

        //    } 
        //    #endregion

        //    #region print ExControls out of ContainerControl  
        //    foreach(ExControl m_Control in m_RepeatControl.SubControls)
        //    {
        //        if(!m_Control.Repeat)continue;

        //        if(m_Control is ContainerControl)continue;				

        //        int nEndPos = 0;

        //        int ControlIndex=ContainerRectInfos.Indexof(m_Control);

        //        if(ControlIndex>=0)continue;	

        //        int ReleativeLeft=m_Control.XtraContainer.Left-m_RepeatControl.XtraContainer.Left;

        //        int ReleativeTop=m_Control.XtraContainer.Top-m_RepeatControl.XtraContainer.Top;

        //        int offsetX = xStartPos + (int)(ReleativeLeft/Webb.Utility.ConvertCoordinate);

        //        int offsetY = yStartPos + (int)(ReleativeTop/Webb.Utility.ConvertCoordinate);	

        //        if(m_RepeatControl.AfterLast&&yLastEndPos>0)
        //        {
        //            offsetY= yLastEndPos + (int)(ReleativeTop/Webb.Utility.ConvertCoordinate);
        //        }

        //        if(OneValueScFilter != null)
        //        {
        //            m_Control.MainView.OneValueScFilter.Apply(OneValueScFilter);						
        //        }
        //        if(RepeatscFilter != null)
        //        {
        //            if(filterNames.Contains(RepeatscFilter.FilterName)&&(m_Control is ChartControlEx))
        //            {
        //                (m_Control as ChartControlEx).ColorWhenMax=m_RepeatControl.ChartColorWhenMax;
        //            }

        //            m_Control.MainView.RepeatFilter.Apply(RepeatscFilter);
        //        }		


        //        if(!(m_Control is ReportInfoLabel))m_Control.CalculateResult();

        //        int nEndX=0;

        //        nEndPos=m_Control.InternalCreateArea(offsetX,offsetY,"",graph,out nEndX);;	//create all controls	

        //        xEndPos=Math.Max(xEndPos,nEndX);

        //        if((m_Control is ChartControlEx))
        //        {
        //            (m_Control as ChartControlEx).ColorWhenMax=Color.Empty;
        //        }					

        //        yEndPos = Math.Max(yEndPos,nEndPos);	//get bottom

        //    } 
        //    #endregion			

        //    return yEndPos;
        //}

        //internal int PrintRepeatContainerAutoLayOut(int yLastEndPos,int xStartPos, int yStartPos, SectionFilter OneValueScFilter,SectionFilter RepeatscFilter, IBrickGraphics graph,RepeatControl m_RepeatControl,ArrayList filterNames,out int xEndPos)
        //{
        //    int yEndPos = 0;

        //    xEndPos=0;

        //    PrintInfo LastInfo=new PrintInfo();

        //    PrintInfo LastContainerInfo=new PrintInfo();

        //    #region print ExControls In ContainerControl  
        //    foreach(ExControl m_Control in m_RepeatControl.SubControls)
        //    {
        //        if(!m_Control.Repeat)continue;

        //        if(m_Control is ContainerControl)continue;

        //        int nEndPos =0;

        //        int ControlIndex=ContainerRectInfos.Indexof(m_Control);

        //        if(ControlIndex<0)continue;

        //        int ReleativeLeft=m_Control.XtraContainer.Left-m_RepeatControl.XtraContainer.Left;

        //        int ReleativeTop=m_Control.XtraContainer.Top-m_RepeatControl.XtraContainer.Top;

        //        int offsetX = xStartPos + (int)(ReleativeLeft/Webb.Utility.ConvertCoordinate);

        //        int offsetY = yStartPos + (int)(ReleativeTop/Webb.Utility.ConvertCoordinate);	

        //        if(m_RepeatControl.AfterLast&&yLastEndPos>0)
        //        {
        //            offsetY= yLastEndPos + (int)(ReleativeTop/Webb.Utility.ConvertCoordinate);
        //        }

        //        if(OneValueScFilter != null)
        //        {
        //            m_Control.MainView.OneValueScFilter.Apply(OneValueScFilter);

        //        }
        //        if(RepeatscFilter != null)
        //        {
        //            if(filterNames.Contains(RepeatscFilter.FilterName)&&(m_Control is ChartControlEx))
        //            {
        //                (m_Control as ChartControlEx).ColorWhenMax=m_RepeatControl.ChartColorWhenMax;
        //            }

        //            m_Control.MainView.RepeatFilter.Apply(RepeatscFilter);
        //        }		

        //        if(!(m_Control is ReportInfoLabel))m_Control.CalculateResult();

        //        LastContainerInfo=ContainerRectInfos[ControlIndex].PrintInfo;					

        //        if(LastContainerInfo.Bottom<offsetY)
        //        {
        //            ContainerRectInfos.UpdateLineInfo(m_Control,true);						
        //        }
        //        else if(LastContainerInfo.CtrlBounds.Right<=m_Control.XtraContainer.Left)
        //        {
        //            offsetX=Math.Max(offsetX,LastContainerInfo.Right);

        //            offsetY=Math.Max(offsetY,LastContainerInfo.Top);

        //            ContainerRectInfos.UpdateLineInfo(m_Control,false);						
        //        }
        //        else
        //        {
        //            ContainerRectInfos.UpdateLineInfo(m_Control,true);

        //            int lastbootom=ContainerRectInfos[ControlIndex].LinePrintInfo.LastLineCtrlBottom;

        //            int distance=m_Control.XtraContainer.Top-ContainerRectInfos[ControlIndex].LinePrintInfo.LastLineCtrlBottom;

        //            if(ContainerRectInfos[ControlIndex].KeepDistance&&distance>0&&lastbootom>0)
        //            {  
        //                offsetY=LastContainerInfo.Bottom+(int)(distance/Webb.Utility.ConvertCoordinate);
        //            }
        //            else
        //            {
        //                offsetY=LastContainerInfo.Bottom;
        //            }								
        //        }	

        //        int nEndX=0;

        //        nEndPos= m_Control.InternalCreateArea(offsetX,offsetY,"",graph,out nEndX);		

        //        xEndPos=Math.Max(nEndX,xEndPos);

        //        ContainerRectInfos.UpdatePrintInfo(m_Control,offsetX,nEndX,offsetY,nEndPos);

        //        if((m_Control is ChartControlEx))
        //        {
        //            (m_Control as ChartControlEx).ColorWhenMax=Color.Empty;
        //        }	

        //        yEndPos = Math.Max(yEndPos,nEndPos);	//get bottom						
        //    } 

        //    #endregion

        //    #region print ExControls out of ContainerControl    
        //    foreach(ExControl m_Control in m_RepeatControl.SubControls)
        //    {
        //        if(!m_Control.Repeat)continue;

        //        if(m_Control is ContainerControl)continue;				

        //        int nEndPos=0;	

        //        int ControlIndex=ContainerRectInfos.Indexof(m_Control);

        //        if(ControlIndex>=0)continue;

        //        int ReleativeLeft=m_Control.XtraContainer.Left-m_RepeatControl.XtraContainer.Left;

        //        int ReleativeTop=m_Control.XtraContainer.Top-m_RepeatControl.XtraContainer.Top;

        //        int offsetX = xStartPos + (int)(ReleativeLeft/Webb.Utility.ConvertCoordinate);

        //        int offsetY = yStartPos + (int)(ReleativeTop/Webb.Utility.ConvertCoordinate);	

        //        if(m_RepeatControl.AfterLast&&yLastEndPos>0)
        //        {
        //            offsetY= yLastEndPos + (int)(ReleativeTop/Webb.Utility.ConvertCoordinate);
        //        }

        //        if(OneValueScFilter != null)
        //        {
        //            m_Control.MainView.OneValueScFilter.Apply(OneValueScFilter);

        //        }
        //        if(RepeatscFilter != null)
        //        {
        //            if(filterNames.Contains(RepeatscFilter.FilterName)&&(m_Control is ChartControlEx))
        //            {
        //                (m_Control as ChartControlEx).ColorWhenMax=m_RepeatControl.ChartColorWhenMax;
        //            }

        //            m_Control.MainView.RepeatFilter.Apply(RepeatscFilter);
        //        }				

        //        if(!(m_Control is ReportInfoLabel))m_Control.CalculateResult();

        //        if(LastInfo.Bottom<offsetY)
        //        {
        //        }
        //        else if(LastInfo.CtrlBounds.Right<=m_Control.XtraContainer.Left)
        //        {
        //            offsetX=Math.Max(offsetX,LastInfo.Right);

        //            offsetY=Math.Max(offsetY,LastInfo.Top);

        //        }
        //        else
        //        {
        //            offsetY=LastInfo.Bottom;
        //        }	

        //        int nEndX=0;

        //        nEndPos= m_Control.InternalCreateArea(offsetX,offsetY,"",graph,out nEndX);	

        //        xEndPos=Math.Max(nEndX,xEndPos);

        //        LastInfo.Update(m_Control,offsetX,nEndX,offsetY,nEndPos);		

        //        if(m_Control is ChartControlEx)
        //        {
        //            (m_Control as ChartControlEx).ColorWhenMax=Color.Empty;
        //        }	

        //        yEndPos = Math.Max(yEndPos,nEndPos);	//get bottom

        //    } 
        //    #endregion

        //    return yEndPos;
        //}		

        //#endregion
        #endregion
        #region new  02-12-2011
        #region print OneValue Excontrols In RepeatContainer
        internal int PrintOneValueRepeatContainerAutoLayout(int yLastEndPos, int xStartPos, int yStartPos, SectionFilter scFilter, IBrickGraphics graph, RepeatControl m_RepeatControl)
        {
            int yEndPos = 0;

            PrintInfo LastInfo = new PrintInfo();
            PrintInfo LastContainerInfo = new PrintInfo();

            #region print ExControls In ContainerControl
            foreach (ExControl m_Control in m_RepeatControl.SubControls)
            {
                if (m_Control.Repeat) continue;

                if (m_Control is ContainerControl) continue;

                int nEndPos = 0;

                int ControlIndex = ContainerRectInfos.Indexof(m_Control);

                if (ControlIndex < 0) continue;

                int offsetX = xStartPos + (int)(m_Control.XtraContainer.Left / Webb.Utility.ConvertCoordinate);

                int offsetY = yStartPos + (int)(m_Control.XtraContainer.Top / Webb.Utility.ConvertCoordinate);

                if (m_RepeatControl.AfterLast && yLastEndPos > 0)
                {
                    offsetY = yLastEndPos + (int)((m_Control.XtraContainer.Top - m_RepeatControl.XtraContainer.Top) / Webb.Utility.ConvertCoordinate);
                }

                if (scFilter != null)
                {
                    m_Control.MainView.OneValueScFilter.Apply(scFilter);
                }
                if (!(m_Control is ReportInfoLabel))
                {
                    m_Control.CalculateResult();
                }

                LastContainerInfo = ContainerRectInfos[ControlIndex].PrintInfo;

                if (LastContainerInfo.Bottom < offsetY)
                {
                    ContainerRectInfos.UpdateLineInfo(m_Control, true);
                }
                else if (LastContainerInfo.CtrlBounds.Right <= m_Control.XtraContainer.Left)
                {
                    offsetX = Math.Max(offsetX, LastContainerInfo.Right);

                    offsetY = Math.Max(offsetY, LastContainerInfo.Top);

                    ContainerRectInfos.UpdateLineInfo(m_Control, false);

                }
                else
                {
                    ContainerRectInfos.UpdateLineInfo(m_Control, true);

                    int lastbootom = ContainerRectInfos[ControlIndex].LinePrintInfo.LastLineCtrlBottom;

                    int distance = m_Control.XtraContainer.Top - ContainerRectInfos[ControlIndex].LinePrintInfo.LastLineCtrlBottom;

                    if (ContainerRectInfos[ControlIndex].KeepDistance && distance > 0 && lastbootom > 0)
                    {
                        offsetY = LastContainerInfo.Bottom + (int)(distance / Webb.Utility.ConvertCoordinate);

                    }
                    else
                    {
                        offsetY = LastContainerInfo.Bottom;
                    }
                }

                int nEndX = 0;

                nEndPos = m_Control.InternalCreateArea(offsetX, offsetY, "", graph, out nEndX);

                ContainerRectInfos.UpdatePrintInfo(m_Control, offsetX, nEndX, offsetY, nEndPos);

                m_RepeatControl.SubShapeContainers.UpdatePrintInfo(m_Control, offsetX, nEndX, offsetY, nEndPos);

                yEndPos = Math.Max(yEndPos, nEndPos);	//get bottom

            }
            #endregion

            #region print ExControls out of ContainerControl
            foreach (ExControl m_Control in m_RepeatControl.SubControls)
            {
                if (m_Control.Repeat) continue;

                if (m_Control is ContainerControl) continue;

                int nEndPos = 0;

                int ControlIndex = ContainerRectInfos.Indexof(m_Control);

                if (ControlIndex >= 0) continue;

                int offsetX = xStartPos + (int)(m_Control.XtraContainer.Left / Webb.Utility.ConvertCoordinate);

                int offsetY = yStartPos + (int)(m_Control.XtraContainer.Top / Webb.Utility.ConvertCoordinate);

                if (m_RepeatControl.AfterLast && yLastEndPos > 0)
                {
                    offsetY = yLastEndPos + (int)((m_Control.XtraContainer.Top - m_RepeatControl.XtraContainer.Top) / Webb.Utility.ConvertCoordinate);
                }

                if (scFilter != null)
                {
                    m_Control.MainView.OneValueScFilter.Apply(scFilter);
                }
                if (!(m_Control is ReportInfoLabel))
                {
                    m_Control.CalculateResult();
                }

                if (LastInfo.Bottom < offsetY)
                {
                }
                else if (LastInfo.CtrlBounds.Right <= m_Control.XtraContainer.Left)
                {
                    offsetX = Math.Max(offsetX, LastInfo.Right);

                    offsetY = Math.Max(offsetY, LastInfo.Top);

                }
                else
                {
                    offsetY = LastInfo.Bottom;
                }

                int nEndX = 0;

                nEndPos = m_Control.InternalCreateArea(offsetX, offsetY, "", graph, out nEndX);

                LastInfo.Update(m_Control, offsetX, nEndX, offsetY, nEndPos);

                m_RepeatControl.SubShapeContainers.UpdatePrintInfo(m_Control, offsetX, nEndX, offsetY, nEndPos);

                yEndPos = Math.Max(yEndPos, nEndPos);	//get bottom

            }
            #endregion

            #region print ShapeControl In AutoLayOut
            foreach (RectInfo rectInfo in m_RepeatControl.SubShapeContainers)
            {
                ExShapeControl m_Control = rectInfo.shapeControl;

                if (m_Control.Repeat) continue;

                int nEndPos = 0;

                int offsetX = xStartPos + (int)(m_Control.XtraContainer.Left / Webb.Utility.ConvertCoordinate);

                int offsetY = yStartPos + (int)(m_Control.XtraContainer.Top / Webb.Utility.ConvertCoordinate);

                nEndPos = m_Control.InternalCreateShapeArea(offsetX, offsetY, string.Empty, graph, rectInfo);

                yEndPos = Math.Max(yEndPos, nEndPos);	//get bottom

            }
            #endregion

            return yEndPos;
        }
        internal int PrintOneValueRepeatContainerOnce(int yLastEndPos, int xStartPos, int yStartPos, SectionFilter scFilter, IBrickGraphics graph, RepeatControl m_RepeatControl)
        {
            int yEndPos = 0;

            PrintInfo LastContainerInfo = new PrintInfo();

            #region print ExControls In ContainerControl
            foreach (ExControl m_Control in m_RepeatControl.SubControls)
            {
                if (m_Control.Repeat) continue;

                if (m_Control is ContainerControl) continue;

                int nEndPos = 0;

                int ControlIndex = ContainerRectInfos.Indexof(m_Control);

                if (ControlIndex < 0) continue;

                int offsetX = xStartPos + (int)(m_Control.XtraContainer.Left / Webb.Utility.ConvertCoordinate);

                int offsetY = yStartPos + (int)(m_Control.XtraContainer.Top / Webb.Utility.ConvertCoordinate);

                if (m_RepeatControl.AfterLast && yLastEndPos > 0)
                {
                    offsetY = yLastEndPos + (int)((m_Control.XtraContainer.Top - m_RepeatControl.XtraContainer.Top) / Webb.Utility.ConvertCoordinate);
                }

                if (scFilter != null)
                {
                    m_Control.MainView.OneValueScFilter.Apply(scFilter);
                }

                if (!(m_Control is ReportInfoLabel))
                {
                    m_Control.CalculateResult();
                }

                LastContainerInfo = ContainerRectInfos[ControlIndex].PrintInfo;

                if (LastContainerInfo.Bottom < offsetY)
                {
                    ContainerRectInfos.UpdateLineInfo(m_Control, true);
                }
                else if (LastContainerInfo.CtrlBounds.Right <= m_Control.XtraContainer.Left)
                {
                    offsetX = Math.Max(offsetX, LastContainerInfo.Right);

                    offsetY = Math.Max(offsetY, LastContainerInfo.Top);

                    ContainerRectInfos.UpdateLineInfo(m_Control, false);
                }
                else
                {
                    ContainerRectInfos.UpdateLineInfo(m_Control, true);

                    int lastbootom = ContainerRectInfos[ControlIndex].LinePrintInfo.LastLineCtrlBottom;

                    int distance = m_Control.XtraContainer.Top - ContainerRectInfos[ControlIndex].LinePrintInfo.LastLineCtrlBottom;

                    if (ContainerRectInfos[ControlIndex].KeepDistance && distance > 0 && lastbootom > 0)
                    {
                        offsetY = LastContainerInfo.Bottom + (int)(distance / Webb.Utility.ConvertCoordinate);

                    }
                    else
                    {
                        offsetY = LastContainerInfo.Bottom;
                    }
                }

                int nEndX = 0;

                nEndPos = m_Control.InternalCreateArea(offsetX, offsetY, "", graph, out nEndX);

                ContainerRectInfos.UpdatePrintInfo(m_Control, offsetX, nEndX, offsetY, nEndPos);

                m_RepeatControl.SubShapeContainers.UpdatePrintInfo(m_Control, offsetX, nEndX, offsetY, nEndPos);

                yEndPos = Math.Max(yEndPos, nEndPos);	//get bottom

            }
            #endregion

            #region print Controls out of the Container
            foreach (ExControl m_Control in m_RepeatControl.SubControls)
            {
                if (m_Control.Repeat) continue;

                if (m_Control is ContainerControl) continue;

                int nEndPos = 0;

                int ControlIndex = ContainerRectInfos.Indexof(m_Control);

                if (ControlIndex >= 0) continue;

                int offsetX = xStartPos + (int)(m_Control.XtraContainer.Left / Webb.Utility.ConvertCoordinate);

                int offsetY = yStartPos + (int)(m_Control.XtraContainer.Top / Webb.Utility.ConvertCoordinate);

                if (m_RepeatControl.AfterLast && yLastEndPos > 0)
                {
                    offsetY = yLastEndPos + (int)((m_Control.XtraContainer.Top - m_RepeatControl.XtraContainer.Top) / Webb.Utility.ConvertCoordinate);
                }

                if (scFilter != null)
                {
                    m_Control.MainView.OneValueScFilter.Apply(scFilter);
                }

                if (!(m_Control is ReportInfoLabel))
                {
                    m_Control.CalculateResult();
                }

                int nEndX = 0;

                nEndPos = m_Control.InternalCreateArea(offsetX, offsetY, "", graph, out nEndX); ;	//create all controls	

                m_RepeatControl.SubShapeContainers.UpdatePrintInfo(m_Control, offsetX, nEndX, offsetY, nEndPos);

                yEndPos = Math.Max(yEndPos, nEndPos);	//get bottom

            }
            #endregion

            #region print ShapeControl out of ContainerControl
            foreach (RectInfo rectInfo in m_RepeatControl.SubShapeContainers)
            {
                ExShapeControl m_Control = rectInfo.shapeControl;

                if (m_Control.Repeat) continue;

                int nEndPos = 0;

                int offsetX = xStartPos + (int)(m_Control.XtraContainer.Left / Webb.Utility.ConvertCoordinate);

                int offsetY = yStartPos + (int)(m_Control.XtraContainer.Top / Webb.Utility.ConvertCoordinate);

                nEndPos = m_Control.InternalCreateShapeArea(offsetX, offsetY, string.Empty, graph, rectInfo);

                yEndPos = Math.Max(yEndPos, nEndPos);	//get bottom

            }
            #endregion

            return yEndPos;
        }
        #endregion

        #region Print Repeat all in RepeatContainers
        internal int PrintRepeatContainerOnce(int yLastEndPos, int xStartPos, int yStartPos, SectionFilter OneValueScFilter, SectionFilter RepeatscFilter, IBrickGraphics graph, RepeatControl m_RepeatControl, ArrayList filterNames, out int xEndPos)
        {
            int yEndPos = 0;

            xEndPos = 0;

            PrintInfo LastContainerInfo = new PrintInfo();

            #region print ExControls In ContainerControl
            foreach (ExControl m_Control in m_RepeatControl.SubControls)
            {
                if (!m_Control.Repeat) continue;

                if (m_Control is ContainerControl) continue;

                int nEndPos = 0;

                int ControlIndex = ContainerRectInfos.Indexof(m_Control);

                if (ControlIndex < 0) continue;

                int ReleativeLeft = m_Control.XtraContainer.Left - m_RepeatControl.XtraContainer.Left;

                int ReleativeTop = m_Control.XtraContainer.Top - m_RepeatControl.XtraContainer.Top;

                int offsetX = xStartPos + (int)(ReleativeLeft / Webb.Utility.ConvertCoordinate);

                int offsetY = yStartPos + (int)(ReleativeTop / Webb.Utility.ConvertCoordinate);

                if (m_RepeatControl.AfterLast && yLastEndPos > 0)
                {
                    offsetY = yLastEndPos + (int)(ReleativeTop / Webb.Utility.ConvertCoordinate);
                }

                if (OneValueScFilter != null)
                {
                    m_Control.MainView.OneValueScFilter.Apply(OneValueScFilter);

                }
                if (RepeatscFilter != null)
                {
                    if (filterNames.Contains(RepeatscFilter.FilterName) && (m_Control is ChartControlEx))
                    {
                        (m_Control as ChartControlEx).ColorWhenMax = m_RepeatControl.ChartColorWhenMax;
                    }

                    m_Control.MainView.RepeatFilter.Apply(RepeatscFilter);
                }

                if (!(m_Control is ReportInfoLabel))
                {
                    m_Control.CalculateResult();
                }

                LastContainerInfo = ContainerRectInfos[ControlIndex].PrintInfo;

                if (LastContainerInfo.Bottom < offsetY)
                {
                    ContainerRectInfos.UpdateLineInfo(m_Control, true);
                }
                else if (LastContainerInfo.CtrlBounds.Right <= m_Control.XtraContainer.Left)
                {
                    offsetX = Math.Max(offsetX, LastContainerInfo.Right);

                    offsetY = Math.Max(offsetY, LastContainerInfo.Top);

                    ContainerRectInfos.UpdateLineInfo(m_Control, false);
                }
                else
                {
                    ContainerRectInfos.UpdateLineInfo(m_Control, true);

                    int lastbootom = ContainerRectInfos[ControlIndex].LinePrintInfo.LastLineCtrlBottom;

                    int distance = m_Control.XtraContainer.Top - ContainerRectInfos[ControlIndex].LinePrintInfo.LastLineCtrlBottom;

                    if (ContainerRectInfos[ControlIndex].KeepDistance && distance > 0 && lastbootom > 0)
                    {
                        offsetY = LastContainerInfo.Bottom + (int)(distance / Webb.Utility.ConvertCoordinate);

                    }
                    else
                    {
                        offsetY = LastContainerInfo.Bottom;
                    }
                }

                int nEndX = 0;

                nEndPos = m_Control.InternalCreateArea(offsetX, offsetY, "", graph, out nEndX);

                xEndPos = Math.Max(xEndPos, nEndX);

                ContainerRectInfos.UpdatePrintInfo(m_Control, offsetX, nEndX, offsetY, nEndPos);

                m_RepeatControl.SubShapeContainers.UpdatePrintInfo(m_Control, offsetX, nEndX, offsetY, nEndPos);

                if (m_Control is ChartControlEx)
                {
                    (m_Control as ChartControlEx).ColorWhenMax = Color.Empty;
                }

                yEndPos = Math.Max(yEndPos, nEndPos);	//get bottom

            }
            #endregion

            #region print ExControls out of ContainerControl
            foreach (ExControl m_Control in m_RepeatControl.SubControls)
            {
                if (!m_Control.Repeat) continue;

                if (m_Control is ContainerControl) continue;

                int nEndPos = 0;

                int ControlIndex = ContainerRectInfos.Indexof(m_Control);

                if (ControlIndex >= 0) continue;

                int ReleativeLeft = m_Control.XtraContainer.Left - m_RepeatControl.XtraContainer.Left;

                int ReleativeTop = m_Control.XtraContainer.Top - m_RepeatControl.XtraContainer.Top;

                int offsetX = xStartPos + (int)(ReleativeLeft / Webb.Utility.ConvertCoordinate);

                int offsetY = yStartPos + (int)(ReleativeTop / Webb.Utility.ConvertCoordinate);

                if (m_RepeatControl.AfterLast && yLastEndPos > 0)
                {
                    offsetY = yLastEndPos + (int)(ReleativeTop / Webb.Utility.ConvertCoordinate);
                }

                if (OneValueScFilter != null)
                {
                    m_Control.MainView.OneValueScFilter.Apply(OneValueScFilter);
                }
                if (RepeatscFilter != null)
                {
                    if (filterNames.Contains(RepeatscFilter.FilterName) && (m_Control is ChartControlEx))
                    {
                        (m_Control as ChartControlEx).ColorWhenMax = m_RepeatControl.ChartColorWhenMax;
                    }

                    m_Control.MainView.RepeatFilter.Apply(RepeatscFilter);
                }

                if (!(m_Control is ReportInfoLabel))
                {
                    m_Control.CalculateResult();
                }

                int nEndX = 0;

                nEndPos = m_Control.InternalCreateArea(offsetX, offsetY, "", graph, out nEndX); ;	//create all controls	

                m_RepeatControl.SubShapeContainers.UpdatePrintInfo(m_Control, offsetX, nEndX, offsetY, nEndPos);

                xEndPos = Math.Max(xEndPos, nEndX);

                if ((m_Control is ChartControlEx))
                {
                    (m_Control as ChartControlEx).ColorWhenMax = Color.Empty;
                }

                yEndPos = Math.Max(yEndPos, nEndPos);	//get bottom

            }
            #endregion

            #region print ShapeControl In AutoLayOut
            foreach (RectInfo rectInfo in m_RepeatControl.SubShapeContainers)
            {
                ExShapeControl m_Control = rectInfo.shapeControl;

                if (!m_Control.Repeat) continue;

                int nEndPos = 0;

                int ReleativeLeft = m_Control.XtraContainer.Left - m_RepeatControl.XtraContainer.Left;

                int ReleativeTop = m_Control.XtraContainer.Top - m_RepeatControl.XtraContainer.Top;

                int offsetX = xStartPos + (int)(ReleativeLeft / Webb.Utility.ConvertCoordinate);

                int offsetY = yStartPos + (int)(ReleativeTop / Webb.Utility.ConvertCoordinate);

                nEndPos = m_Control.InternalCreateShapeArea(offsetX, offsetY, string.Empty, graph, rectInfo);

                yEndPos = Math.Max(yEndPos, nEndPos);	//get bottom

            }
            #endregion

            return yEndPos;
        }

        internal int PrintRepeatContainerAutoLayOut(int yLastEndPos, int xStartPos, int yStartPos, SectionFilter OneValueScFilter, SectionFilter RepeatscFilter, IBrickGraphics graph, RepeatControl m_RepeatControl, ArrayList filterNames, out int xEndPos)
        {
            int yEndPos = 0;

            xEndPos = 0;

            PrintInfo LastInfo = new PrintInfo();

            PrintInfo LastContainerInfo = new PrintInfo();

            #region print ExControls In ContainerControl
            foreach (ExControl m_Control in m_RepeatControl.SubControls)
            {
                if (!m_Control.Repeat) continue;

                if (m_Control is ContainerControl) continue;

                int nEndPos = 0;

                int ControlIndex = ContainerRectInfos.Indexof(m_Control);

                if (ControlIndex < 0) continue;

                int ReleativeLeft = m_Control.XtraContainer.Left - m_RepeatControl.XtraContainer.Left;

                int ReleativeTop = m_Control.XtraContainer.Top - m_RepeatControl.XtraContainer.Top;

                int offsetX = xStartPos + (int)(ReleativeLeft / Webb.Utility.ConvertCoordinate);

                int offsetY = yStartPos + (int)(ReleativeTop / Webb.Utility.ConvertCoordinate);

                if (m_RepeatControl.AfterLast && yLastEndPos > 0)
                {
                    offsetY = yLastEndPos + (int)(ReleativeTop / Webb.Utility.ConvertCoordinate);
                }

                if (OneValueScFilter != null)
                {
                    m_Control.MainView.OneValueScFilter.Apply(OneValueScFilter);

                }
                if (RepeatscFilter != null)
                {
                    if (filterNames.Contains(RepeatscFilter.FilterName) && (m_Control is ChartControlEx))
                    {
                        (m_Control as ChartControlEx).ColorWhenMax = m_RepeatControl.ChartColorWhenMax;
                    }

                    m_Control.MainView.RepeatFilter.Apply(RepeatscFilter);
                }

                if (!(m_Control is ReportInfoLabel))
                {
                    m_Control.CalculateResult();
                }

                LastContainerInfo = ContainerRectInfos[ControlIndex].PrintInfo;

                if (LastContainerInfo.Bottom < offsetY)
                {
                    ContainerRectInfos.UpdateLineInfo(m_Control, true);
                }
                else if (LastContainerInfo.CtrlBounds.Right <= m_Control.XtraContainer.Left)
                {
                    offsetX = Math.Max(offsetX, LastContainerInfo.Right);

                    offsetY = Math.Max(offsetY, LastContainerInfo.Top);

                    ContainerRectInfos.UpdateLineInfo(m_Control, false);
                }
                else
                {
                    ContainerRectInfos.UpdateLineInfo(m_Control, true);

                    int lastbootom = ContainerRectInfos[ControlIndex].LinePrintInfo.LastLineCtrlBottom;

                    int distance = m_Control.XtraContainer.Top - ContainerRectInfos[ControlIndex].LinePrintInfo.LastLineCtrlBottom;

                    if (ContainerRectInfos[ControlIndex].KeepDistance && distance > 0 && lastbootom > 0)
                    {
                        offsetY = LastContainerInfo.Bottom + (int)(distance / Webb.Utility.ConvertCoordinate);
                    }
                    else
                    {
                        offsetY = LastContainerInfo.Bottom;
                    }
                }

                int nEndX = 0;

                nEndPos = m_Control.InternalCreateArea(offsetX, offsetY, "", graph, out nEndX);

                xEndPos = Math.Max(nEndX, xEndPos);

                ContainerRectInfos.UpdatePrintInfo(m_Control, offsetX, nEndX, offsetY, nEndPos); ;

                m_RepeatControl.SubShapeContainers.UpdatePrintInfo(m_Control, offsetX, nEndX, offsetY, nEndPos);


                if ((m_Control is ChartControlEx))
                {
                    (m_Control as ChartControlEx).ColorWhenMax = Color.Empty;
                }

                yEndPos = Math.Max(yEndPos, nEndPos);	//get bottom						
            }

            #endregion

            #region print ExControls out of ContainerControl
            foreach (ExControl m_Control in m_RepeatControl.SubControls)
            {
                if (!m_Control.Repeat) continue;

                if (m_Control is ContainerControl) continue;

                int nEndPos = 0;

                int ControlIndex = ContainerRectInfos.Indexof(m_Control);

                if (ControlIndex >= 0) continue;

                int ReleativeLeft = m_Control.XtraContainer.Left - m_RepeatControl.XtraContainer.Left;

                int ReleativeTop = m_Control.XtraContainer.Top - m_RepeatControl.XtraContainer.Top;

                int offsetX = xStartPos + (int)(ReleativeLeft / Webb.Utility.ConvertCoordinate);

                int offsetY = yStartPos + (int)(ReleativeTop / Webb.Utility.ConvertCoordinate);

                if (m_RepeatControl.AfterLast && yLastEndPos > 0)
                {
                    offsetY = yLastEndPos + (int)(ReleativeTop / Webb.Utility.ConvertCoordinate);
                }

                if (OneValueScFilter != null)
                {
                    m_Control.MainView.OneValueScFilter.Apply(OneValueScFilter);

                }
                if (RepeatscFilter != null)
                {
                    if (filterNames.Contains(RepeatscFilter.FilterName) && (m_Control is ChartControlEx))
                    {
                        (m_Control as ChartControlEx).ColorWhenMax = m_RepeatControl.ChartColorWhenMax;
                    }

                    m_Control.MainView.RepeatFilter.Apply(RepeatscFilter);
                }

                if (!(m_Control is ReportInfoLabel))
                {
                    m_Control.CalculateResult();
                }

                if (LastInfo.Bottom < offsetY)
                {
                }
                else if (LastInfo.CtrlBounds.Right <= m_Control.XtraContainer.Left)
                {
                    offsetX = Math.Max(offsetX, LastInfo.Right);

                    offsetY = Math.Max(offsetY, LastInfo.Top);

                }
                else
                {
                    offsetY = LastInfo.Bottom;
                }

                int nEndX = 0;

                nEndPos = m_Control.InternalCreateArea(offsetX, offsetY, "", graph, out nEndX);

                xEndPos = Math.Max(nEndX, xEndPos);

                LastInfo.Update(m_Control, offsetX, nEndX, offsetY, nEndPos);

                m_RepeatControl.SubShapeContainers.UpdatePrintInfo(m_Control, offsetX, nEndX, offsetY, nEndPos);

                if (m_Control is ChartControlEx)
                {
                    (m_Control as ChartControlEx).ColorWhenMax = Color.Empty;
                }

                yEndPos = Math.Max(yEndPos, nEndPos);	//get bottom

            }
            #endregion

            #region print ShapeControl In AutoLayOut
            foreach (RectInfo rectInfo in m_RepeatControl.SubShapeContainers)
            {
                ExShapeControl m_Control = rectInfo.shapeControl;

                if (!m_Control.Repeat) continue;

                int nEndPos = 0;

                int ReleativeLeft = m_Control.XtraContainer.Left - m_RepeatControl.XtraContainer.Left;

                int ReleativeTop = m_Control.XtraContainer.Top - m_RepeatControl.XtraContainer.Top;

                int offsetX = xStartPos + (int)(ReleativeLeft / Webb.Utility.ConvertCoordinate);

                int offsetY = yStartPos + (int)(ReleativeTop / Webb.Utility.ConvertCoordinate);

                nEndPos = m_Control.InternalCreateShapeArea(offsetX, offsetY, string.Empty, graph, rectInfo);

                yEndPos = Math.Max(yEndPos, nEndPos);	//get bottom

            }
            #endregion

            return yEndPos;
        }

        #endregion
        #endregion
        #endregion

        #endregion


        public int MaxAbsPosition(int m_y)
        {
            int heightPerPage = this.Report.GetHeightPerPage(); //this._HeightPerPage;

            int reportHeaderHeight = this.Report.GetReportHeaderHeight();	//report header

            int reportFooterHeight = this.Report.GetReportFooterHeight();	//report footer

            int FirstPageHeight = heightPerPage + 2 + (int)(reportHeaderHeight / Webb.Utility.ConvertCoordinate);	//07-03-2008@Scott

            int NormalPageHeight = heightPerPage + 2 + (int)(reportHeaderHeight / Webb.Utility.ConvertCoordinate) + (int)(reportFooterHeight / Webb.Utility.ConvertCoordinate);	//07-03-2008@Scott

            if (FirstPageHeight < 50) return FirstPageHeight;

            if (m_y < FirstPageHeight) return FirstPageHeight;

            int LefPos = (m_y - FirstPageHeight) % NormalPageHeight;

            if (LefPos == 0) return m_y;

            return m_y - LefPos + NormalPageHeight;
        }
        private void AdjustRepeatCount()
        {
            if (!this.Report.RepeatedReport) return;

            Size pageSize = this.Report.GetNoDealedSizePerPage();

            if (pageSize == Size.Empty) return;

            int MaxCount = pageSize.Width / (int)this.Report.Template.RepeatedWidth;

            this.Report.RepeatedCount = Math.Min(MaxCount, this.Report.RepeatedCount);

        }
        //Added this code at 2009-4-10 15:08:45@brian
        protected override void InitLayout()
        {
            base.InitLayout();

            if (!DevExpress.XtraEditors.XtraForm.isDropControl && !(this is VersionControl) && !(this is FileNameControl) && !(this is LabelControl) && !(this is DateTimeControl))
            {
                this.XtraContainer.Height = 120;
            }
        }

        /// <summary>
        /// Print all controls
        /// </summary>
        /// <param name="areaName"></param>
        /// <param name="graph"></param>
        internal void PrintAllExcontrols(string areaName, IBrickGraphics graph)
        {
            if (WebbReport.ExControls.Count <= 0) return;

            int xStartPos = 0, yStartPos = 0/*,xEndPos = 0*/, yEndPos = 0;

            int count = 1;	//print times	

            if (WebbReport.firstExcontrol == null)
            {
                WebbReport.firstExcontrol = WebbReport.ExControls[0].WinControl;
            }

            if (this == WebbReport.firstExcontrol)	//If this control is the first one
            {
                SectionFilterCollection scFilters = this.GetOneValuePerPageFilters();	//get filters for one value per page

                if (scFilters.Count > 0) count = scFilters.Count;

                this.AdjustRepeatCount();

                ContainerRectInfos.Init(WebbReport.ExControls);  //Add at 2009-2-26 9:56:24@Simon			

                RepeatContainers.Init(WebbReport.ExControls);

                ShapeContainers.InitShapeControls(WebbReport.ExControls, RepeatContainers);  //Add at 2009-2-26 9:56:24@Simon

                for (int time = 0; time < count; time++)
                {
                    SectionFilter scFilter = null;

                    if (scFilters.Count > 0) scFilter = scFilters[time];

                    yEndPos = this.PrintFilteredControls(xStartPos, yStartPos, scFilter, areaName, graph);

                    if (this.Report.Template.TopCount > 0 && time >= this.Report.Template.TopCount - 1) return;	//apply top count setting 07-16-2008@Scott

                    if (time < count - 1)
                    {
                        if (this.Report.Template.OneValuePerPage)
                        {
                            //One value per page report
                            if (!this.Report.Template.Consecutive)	//Modified at 2009-2-2 14:02:54@Scott
                            {
                                yEndPos += 20;	//adjust

                                (graph as BrickGraphics).PrintingSystem.InsertPageBreak(yEndPos);
                            }

                            yStartPos = yEndPos;
                        }
                    }
                }
            }
        }

        private ArrayList GetMaxChartValues(SectionFilter oneValueScFilter, SectionFilterCollection scFilters, RepeatControl m_RepeatControl)
        {
            ArrayList arrNames = new ArrayList();

            foreach (ExControl m_Control in m_RepeatControl.SubControls)
            {
                if (m_Control is ChartControlEx)
                {
                    arrNames = (m_Control as ChartControlEx).GetMaxValues(oneValueScFilter, scFilters);

                    break;
                }
            }
            return arrNames;
        }

        protected int PrintAllRepeatControls(int yEndPosOneValue, int xStartPos, int yStartPos, SectionFilter OneValueScFilter, string areaName, IBrickGraphics graph)
        {
            int yEndPos = 0, yEndPosRepeat = 0, yEndPosTemp = 0;

            int xTempStartPos = xStartPos, yTempStartPos = yStartPos, yEndPosOneValueTemp = yEndPosOneValue;

            int yLastEndPos = -1;

            Size szfPage = this.Report.GetNoDealedSizePerPage();

            int LimitedPageWidth = (int)(szfPage.Width / Webb.Utility.ConvertCoordinate) + 2;

            foreach (RepeatControl m_RepeatControl in RepeatContainers)
            {
                xStartPos = xTempStartPos;

                yStartPos = yTempStartPos;

                yEndPosOneValue = Math.Max(yEndPosOneValue, yEndPosOneValueTemp);

                if (this.Report.Template.AutoLayOut)
                {
                    yEndPosOneValueTemp = this.PrintOneValueRepeatContainerAutoLayout(yLastEndPos, xStartPos, yStartPos, OneValueScFilter, graph, m_RepeatControl);
                }
                else
                {
                    yEndPosOneValueTemp = this.PrintOneValueRepeatContainerOnce(yLastEndPos, xStartPos, yStartPos, OneValueScFilter, graph, m_RepeatControl);
                }

                ArrayList filterNames = new ArrayList();

                SectionFilterCollection RepeatFilters = this.GetRepeatFilters(OneValueScFilter, m_RepeatControl.RepeatSetting, m_RepeatControl.RepeatTopCount);

                SectionFilter onevaluefilter = OneValueScFilter;

                if (onevaluefilter == null) onevaluefilter = new SectionFilter();

                filterNames = GetMaxChartValues(onevaluefilter, RepeatFilters, m_RepeatControl);

                int count = 1;	//print times

                if (RepeatFilters.Count > 0)
                {
                    bool hasRepeatControl = false;

                    foreach (ExControl excontrol in m_RepeatControl.SubControls)
                    {
                        if (excontrol.Repeat) hasRepeatControl = true;
                    }
                    if (hasRepeatControl) count = RepeatFilters.Count;

                }

                int endPosX = 0;

                int VerticalCount = 0;

                #region Repeat functions

                for (int time = 0; time < count; time++)
                {
                    ContainerRectInfos.InitPrintInfo();

                    ShapeContainers.InitPrintInfo();

                    SectionFilter scFilter = null;

                    if (RepeatFilters.Count > 0)
                    {
                        scFilter = RepeatFilters[time];
                    }
                    if (time == 0)
                    {
                        xStartPos = (int)(m_RepeatControl.XtraContainer.Left / Webb.Utility.ConvertCoordinate);

                        if (!m_RepeatControl.AfterLast || yLastEndPos < 0)
                        {
                            yStartPos = (int)(m_RepeatControl.XtraContainer.Top / Webb.Utility.ConvertCoordinate);
                        }
                    }

                    if (this.Report.Template.AutoLayOut)
                    {
                        yEndPosTemp = this.PrintRepeatContainerAutoLayOut(yLastEndPos, xStartPos, yStartPos, OneValueScFilter, scFilter, graph, m_RepeatControl, filterNames, out endPosX);
                    }
                    else
                    {
                        yEndPosTemp = this.PrintRepeatContainerOnce(yLastEndPos, xStartPos, yStartPos, OneValueScFilter, scFilter, graph, m_RepeatControl, filterNames, out endPosX);
                    }

                    if (yEndPosTemp > 2) yEndPosTemp -= 2;

                    yEndPosRepeat = Math.Max(yEndPosTemp, yEndPosRepeat);

                    yEndPosRepeat = Math.Max(yEndPosOneValueTemp, yEndPosRepeat);

                    yEndPos = Math.Max(yEndPosOneValue, yEndPosRepeat);

                    if (m_RepeatControl.RepeatTopCount > 0 && time >= m_RepeatControl.RepeatTopCount - 1) continue; ;	//apply top count setting 07-16-2008@Scott

                    if (time < count - 1)
                    {
                        int nColumn = 0;

                        int NextRepeatLocX = 0;

                        int NextRepeatLocY = 0;

                        if (m_RepeatControl.RepeatedCount == 0)
                        {
                            nColumn = 1;
                        }
                        else
                        {
                            nColumn = (time + 1) % m_RepeatControl.RepeatedCount;
                        }

                        if (m_RepeatControl.RepeatedWidth == 0)
                        {
                            NextRepeatLocX = endPosX + endPosX - xStartPos;
                        }
                        else
                        {
                            NextRepeatLocX = xStartPos + 2 * (int)m_RepeatControl.RepeatedWidth;
                        }

                        if (NextRepeatLocX > LimitedPageWidth && m_RepeatControl.RepeatedCount == 0)
                        {
                            nColumn = 0;

                            if (m_RepeatControl.RepeatedWidth == 0)
                            {
                                VerticalCount++;
                            }
                        }

                        if (nColumn != 0)
                        {
                            if (m_RepeatControl.RepeatedWidth == 0)
                            {
                                xStartPos = endPosX - 1;
                            }
                            else
                            {
                                xStartPos += (int)m_RepeatControl.RepeatedWidth;
                            }
                        }
                        else
                        {
                            xStartPos = (int)(m_RepeatControl.XtraContainer.Left / Webb.Utility.ConvertCoordinate);

                            int nRow = 0;

                            if (m_RepeatControl.RepeatedVerticalCount == 0)
                            {
                                #region new code at //2010-1-29 9:29:57@Simon Add this Code
                                if (m_RepeatControl.RepeatedHeight == 0)
                                {
                                    nRow = 1;
                                }
                                else
                                {
                                    NextRepeatLocY = yStartPos + 2 * (int)m_RepeatControl.RepeatedHeight;

                                    int LimitedMax = MaxAbsPosition(yStartPos + (int)m_RepeatControl.RepeatedHeight);

                                    if (NextRepeatLocY > LimitedMax)
                                    {
                                        nRow = 0;
                                    }
                                    else
                                    {
                                        nRow = 1;
                                    }
                                }
                                #endregion

                            }
                            else
                            {
                                if (m_RepeatControl.RepeatedCount == 0)
                                {
                                    nRow = VerticalCount % m_RepeatControl.RepeatedVerticalCount;
                                }
                                else
                                {
                                    nRow = ((time + 1) / m_RepeatControl.RepeatedCount) % m_RepeatControl.RepeatedVerticalCount;
                                }
                            }

                            if (nRow != 0)
                            {
                                if (m_RepeatControl.RepeatedHeight == 0)
                                {
                                    if (yLastEndPos > 0) yLastEndPos += yEndPosRepeat - yLastEndPos;  //2009-6-23 8:29:27@Simon Add this Code

                                    yStartPos = yEndPosRepeat;
                                }
                                else
                                {
                                    yStartPos += (int)m_RepeatControl.RepeatedHeight;

                                    if (yLastEndPos > 0) yLastEndPos += (int)m_RepeatControl.RepeatedHeight;  //2009-6-23 8:29:27@Simon Add this Code
                                }

                            }
                            else
                            {
                                #region Old Insert Page Break
                                //									yEndPosRepeat += 50;	//adjust
                                //
                                //									(graph as BrickGraphics).PrintingSystem.InsertPageBreak(yEndPosRepeat);
                                //
                                //									yStartPos = yEndPosRepeat;
                                //
                                //								    if(yLastEndPos>0)yLastEndPos+=50;;  //2009-6-23 8:29:27@Simon Add this Code
                                #endregion

                                #region New  Insert Page Break

                                int maxAbsBreakPos = MaxAbsPosition(yEndPosRepeat);

                                if (yLastEndPos > 0) yLastEndPos += maxAbsBreakPos + 5 - yEndPosRepeat;  //2009-6-23 8:29:27@Simon Add this Code

                                yStartPos = maxAbsBreakPos + 5;

                                #endregion
                            }

                        }
                    }

                }
                #endregion

                yLastEndPos = yEndPosRepeat;

            }
            return yEndPos;
        }

        internal int PrintFilteredControls(int xStartPos, int yStartPos, SectionFilter OneValueScFilter, string areaName, IBrickGraphics graph)
        {
            int yEndPos = 0, yEndPosOneValue = 0, yEndPosRepeat = 0, yEndPosTemp = 0;

            WebbReport.ExControls.Sort();

            printRepeat = false;

            if (this.Report.Template.AutoLayOut)
            {
                yEndPosOneValue = this.PrintOneValueAutoLayout(xStartPos, yStartPos, OneValueScFilter, areaName, graph);
            }
            else
            {
                yEndPosOneValue = this.PrintOneValueExcontrolsOnce(xStartPos, yStartPos, OneValueScFilter, areaName, graph);
            }

            printRepeat = true;

            int RepeatAllEndPos = PrintAllRepeatControls(yEndPosOneValue, xStartPos, yStartPos, OneValueScFilter, areaName, graph);

            SectionFilterCollection RepeatFilters = this.GetRepeatFilters(OneValueScFilter);

            int count = 1;	//print times

            if (RepeatFilters.Count > 0) count = RepeatFilters.Count;

            for (int time = 0; time < count; time++)
            {
                ContainerRectInfos.InitPrintInfo();

                ShapeContainers.InitPrintInfo();

                SectionFilter scFilter = null;

                if (RepeatFilters.Count > 0)
                {
                    scFilter = RepeatFilters[time];
                }
                if (this.Report.Template.AutoLayOut)
                {
                    yEndPosTemp = this.PrintRepeatOnceAutoLayOut(xStartPos, yStartPos, OneValueScFilter, scFilter, areaName, graph);
                }
                else
                {
                    yEndPosTemp = this.PrintRepeatExcontrolsOnce(xStartPos, yStartPos, OneValueScFilter, scFilter, areaName, graph);
                }

                yEndPosRepeat = Math.Max(yEndPosTemp, yEndPosRepeat);	//Modified at 2009-2-2 13:29:13@Scott

                yEndPos = Math.Max(RepeatAllEndPos, yEndPosRepeat);

                yEndPos = Math.Max(yEndPosOneValue, yEndPos);

                if (this.Report.Template.RepeatTopCount > 0 && time >= this.Report.Template.RepeatTopCount - 1) return yEndPos;	//apply top count setting 07-16-2008@Scott

                if (time < count - 1)
                {
                    if (this.Report.Template.RepeatedReport)
                    {//Repeated report
                        int nColumn = (time + 1) % this.Report.Template.RepeatedHorizonCount;

                        if (nColumn != 0)
                        {
                            xStartPos += (int)this.Report.Template.RepeatedWidth;
                        }
                        else
                        {
                            xStartPos = 0;

                            int nRow = ((time + 1) / this.Report.Template.RepeatedHorizonCount) % this.Report.Template.RepeatedVerticalCount;

                            if (nRow != 0)
                            {
                                yStartPos += (int)this.Report.Template.RepeatedHeight;
                            }
                            else
                            {
                                yEndPos += 50;	//adjust

                                (graph as BrickGraphics).PrintingSystem.InsertPageBreak(yEndPos);

                                yStartPos = yEndPos;
                            }
                        }
                    }
                }

            }
            return yEndPos;

        }

        public Rectangle GetControlRect()
        {
            Rectangle rcControl = new Rectangle();

            rcControl.X = (int)(this.XtraContainer.Left / Webb.Utility.ConvertCoordinate);

            rcControl.Y = (int)(this.XtraContainer.Top / Webb.Utility.ConvertCoordinate);

            if (this._MainView.PrintingTable != null)
            {
                rcControl.Width = this._MainView.PrintingTable.GetTotalWidth();

                rcControl.Height = this._MainView.PrintingTable.GetTotalHeight();
            }

            return rcControl;
        }

        #region Static function
        static int GetPrintOffset_Top(WinControlContainerCollection i_Containers, ExControl i_Control)
        {
            foreach (WinControlContainer m_Container in i_Containers)
            {
                ExControl m_Control = m_Container.WinControl as ExControl;
                if (m_Control == null) continue;
                if (m_Control._PrintTimes > 0)
                {
                    //
                    int m_c1_top = m_Control.GetContainerPosition_Top();
                    int m_c1_left = m_Control.GetContainerPosition_Left();
                    int m_c2_top = i_Control.GetContainerPosition_Top();
                    int m_c2_left = i_Control.GetContainerPosition_Left();
                    //System.Diagnostics.Debug.WriteLine(string.Format("{4} Top:{0};Left:{1};{5} Top:{2};Left:{3};",m_c1_top,m_c1_left,m_c2_top,m_c2_left,m_Control.Name,i_Control.Name));
                    //
                    if (m_c1_top == m_c2_top)
                    {
                        return m_Control.GetPrintTableHeight();
                    }
                }
            }
            return 0;
        }

        //Wu.Country@2007-11-13 14:21 added this region.
        public static bool LoadView(string i_Path, out ExControlView i_View)
        {
            // TODO: implement
            try
            {
                i_View = Webb.Utilities.Serializer.Deserialize(i_Path) as ExControlView;

                return true;
            }
            catch (Exception ex)
            {
                i_View = null;

                Webb.Utilities.TextLog.WriteLine(string.Format("Load ExControlView error. Message:{0}", ex.Message));

                return false;
            }
        }
        public static bool SaveView(string i_Path, ExControlView i_View)
        {
            // TODO: implement
            try
            {
                i_View.ClearPrintingTable();           //2009-8-10 9:08:50@Simon Add this Code

                Webb.Utilities.Serializer.Serialize(i_View, i_Path, true);

                i_View.UpdateView();

                return true;
            }
            catch (Exception ex)
            {
                Webb.Utilities.TextLog.WriteLine(string.Format("Save ExControl View error. Message:{0}", ex.Message));
                return false;
            }
        }
        #endregion

        //Wu.Country@2007-11-08 11:03 AM added this class.
        //Fields
        protected WinControlContainer _ControlContainer;
        protected object _DataSource;
        protected string _DataMember;

        //protected WebbReport _Report;
        protected Webb.Reports.ExControls.Views.ExControlView _MainView;
        protected bool _ThreeD;	//Scott@12082008

        #region override Property and set their "Browsable" Attribute to false
#if BROWSABLE
        [Browsable(false)]
        public new Font Font
        {
            get { return base.Font; }
            set { base.Font = value; }
        }
        [Browsable(false)]
        public new string AccessibleDescription
        {
            get { return base.AccessibleDescription; }
            set { base.AccessibleDescription = value; }
        }

        [Browsable(false)]
        public new string AccessibleName
        {
            get { return base.AccessibleName; }
            set { base.AccessibleName = value; }
        }
        [Browsable(false)]
        public new AccessibleRole AccessibleRole
        {
            get { return base.AccessibleRole; }
            set { base.AccessibleRole = value; }
        }
        [Browsable(false)]
        public new AnchorStyles Anchor
        {
            get { return base.Anchor; }
            set { base.Anchor = value; }
        }
        [Browsable(false)]
        public new bool AllowDrop
        {
            get { return base.AllowDrop; }
            set { base.AllowDrop = value; }
        }
        [Browsable(false)]
        public new Color BackColor
        {
            get { return base.BackColor; }
            set { base.BackColor = value; }
        }
        [Browsable(false)]
        public new Image BackgroundImage
        {
            get { return base.BackgroundImage; }
            set { base.BackgroundImage = value; }
        }
        [Browsable(false)]
        public new bool CausesValidation
        {
            get { return base.CausesValidation; }
            set { base.CausesValidation = value; }
        }
        [Browsable(false)]
        public new ContextMenu ContextMenu
        {
            get { return base.ContextMenu; }
            set { base.ContextMenu = value; }
        }
        [Browsable(false)]
        public new Cursor Cursor
        {
            get { return base.Cursor; }
            set { base.Cursor = value; }
        }
        [Browsable(false)]
        public new DockStyle Dock
        {
            get { return base.Dock; }
            set { base.Dock = value; }
        }
        [Browsable(false)]
        public new bool Enabled
        {
            get { return base.Enabled; }
            set { base.Enabled = value; }
        }
        [Browsable(false)]
        public new Point Location
        {
            get { return base.Location; }
            set { base.Location = value; }
        }
        [Browsable(false)]
        public new RightToLeft RightToLeft
        {
            get { return base.RightToLeft; }
            set { base.RightToLeft = value; }
        }
        [Browsable(false)]
        public new int TabIndex
        {
            get { return base.TabIndex; }
            set { base.TabIndex = value; }
        }
        [Browsable(false)]
        public new bool TabStop
        {
            get { return base.TabStop; }
            set { base.TabStop = value; }
        }
        [Browsable(false)]
        public new object Tag
        {
            get { return base.Tag; }
            set { base.Tag = value; }
        }
        [Browsable(false)]
        public new bool Visible
        {
            get { return base.Visible; }
            set { base.Visible = value; }
        }
        [Browsable(false)]
        public new ImeMode ImeMode
        {
            get { return base.ImeMode; }
            set { base.ImeMode = value; }
        }
        [Browsable(false)]
        public new ControlBindingsCollection DataBindings
        {
            get { return base.DataBindings; }
        }
#endif
        #endregion

        //Properties
        public bool Repeat	//Modified at 2008-12-22 11:28:29@Scott
        {
            get { return this.MainView.Repeat; }
            set { this.MainView.Repeat = value; }
        }
        public bool ThreeD
        {
            get { return this.MainView.ThreeD; }
            set
            {
                if (this.MainView.ThreeD != value)
                {
                    this.MainView.ThreeD = value;

                    if (DesignMode) this.MainView.UpdateView();	//Modified at 2008-12-22 15:57:05@Scott
                }
            }
        }



        [Browsable(false)]
        public WebbReport Report
        {
            get
            {
                if (this._ControlContainer == null) return null;
                return this._ControlContainer.Report as WebbReport;
            }
        }

        [Browsable(false)]
        public SectionFilterCollection SectionFilters
        {
            get { return this._MainView.SectionFilters; }
            set
            {
                this._MainView.SectionFilters = value;

                if (DesignMode) this._MainView.UpdateView();
            }
        }

        //Modified at 2009-1-15 16:43:08@Scott
        [Browsable(false)]
        public SectionFilterCollectionWrapper SectionFiltersWrapper
        {
            get { return this._MainView.SectionFiltersWrapper; }
            set
            {
                this._MainView.SectionFiltersWrapper = value;

                if (DesignMode) this._MainView.UpdateView();
            }
        }
        [Browsable(false)]
        public Webb.Reports.ExControls.Views.ExControlView MainView
        {
            get
            {
                return this._MainView;
            }
            set
            {
                if (this._MainView != value)
                {
                    this._MainView = value;
                    this._MainView.ExControl = this;
                }
            }
        }

        [Browsable(false)]
        private BasicStyle MainStyle
        {
            get
            {
                return this._MainView.MainStyle;
            }
            set
            {
                if (this._MainView.MainStyle != value)
                    this._MainView.MainStyle = value;
            }
        }
        //ctor
        public ExControl()
        {
            //Data.ColumnManager.Init();  //08-20-2008@Scott
            //this._MainView = new ExControlView(this);
            //ExControlManager.ExControls.Add(this);
        }
        //Methods
        public virtual void AutoAdjustSize()
        {
            if (this._MainView.PrintingTable != null)
            {
                int m_Height = this._MainView.PrintingTable.GetTotalHeight(this.MainView.ThreeD);
                int m_Width = this._MainView.PrintingTable.GetTotalWidth(this.MainView.ThreeD);

                if (m_Height > Webb.Utility.ControlMaxHeight)	//06-13-2008@Scott	If control's height is too large , give a warning.
                {
                    if (DialogResult.Yes == Webb.Utilities.TopMostMessageBox.ShowMessage("Warning", "The control is too large, show all data will slow down the designer, do you want continue?", MessageBoxButtons.YesNo))
                    {
                        this.XtraContainer.Height = (int)(m_Height * Webb.Utility.ConvertCoordinate);
                    }
                }
                else
                {
                    this.XtraContainer.Height = (int)(m_Height * Webb.Utility.ConvertCoordinate);
                }

                if (this.Report != null)	//06-13-2008@Scott  Control can't exceed the boundary
                {
                    int nWidth = (int)((this.Report.PageWidth - this.Report.Margins.Left - this.XtraContainer.Left) / Webb.Utility.ConvertCoordinate);

                    m_Width = m_Width > nWidth ? nWidth : m_Width;
                }
                this.XtraContainer.Width = (int)(m_Width * Webb.Utility.ConvertCoordinate);
            }
        }

        public virtual bool Load(string i_Path)
        {
            // TODO: implement
            ExControlView m_View = null;

            if (LoadView(i_Path, out m_View))
            {
                //09-02-2008@Scott
                //Begin Edit
                if (m_View == null || this._MainView == null) return false;

                if (this._MainView.GetType().Name != m_View.GetType().Name)
                {
                    return false;
                }
                //End Edit

                this._MainView = m_View;
                this._MainView.ExControl = this;

                this._MainView.UpdateView();  //2009-8-10 9:14:35@Simon Add this Code

                return true;
            }
            else
            {
                return false;
            }
        }
        public void SetView(ExControlView m_View)  //2009-8-10 9:08:16@Simon Add this Code
        {
            if (m_View == null || this._MainView == null) return;

            if (this._MainView.GetType().Name != m_View.GetType().Name)
            {
                return;
            }


            this._MainView = m_View;
            this._MainView.ExControl = this;

            this._MainView.UpdateView();  //2009-8-10 9:14:35@Simon Add this Code
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    this.XtraContainer.Left--;
                    break;
                case Keys.Right:
                    this.XtraContainer.Left++;
                    break;
                case Keys.Down:
                    this.XtraContainer.Top++;
                    break;
                case Keys.Up:
                    this.XtraContainer.Top--;
                    break;
                default:
                    base.OnKeyDown(e);
                    break;
            }
        }

        public virtual bool Save(string i_Path)
        {
            // TODO: implement
            return SaveView(i_Path, this._MainView);
        }

        public void ReCreatePrintingTable()
        {
            this.CheckDataSouceChanged();
        }

        internal int GetPrintTableHeight()
        {
            try
            {
                return this.MainView.PrintingTable.GetTotalHeight();
            }
            catch
            {
                return 0;
            }
        }

        internal int GetContainerPosition_Top()
        {
            return this.XtraContainer.Top;
        }

        internal int GetContainerPosition_Left()
        {
            return this.XtraContainer.Left;
        }


        #region IExControl Members
        internal int _PrintTimes = 0;
        public WinControlContainer XtraContainer
        {
            get
            {
                // TODO:  Add ExControlBase.XtraContainer getter implementation
                return this._ControlContainer;
            }
            set
            {
                // TODO:  Add ExControlBase.XtraContainer setter implementation
                this._ControlContainer = value;
            }
        }
        [Browsable(false)]
        [Category("Data"), Description("Gets or sets the source of data displayed in the dropdown window."), DefaultValue(null),
        TypeConverter("System.Windows.Forms.Design.DataSourceConverter, System.Design")]
        public object DataSource
        {
            get
            {
                // TODO:  Add ExControlBase.DataSource getter implementation
                return this._DataSource;
            }
            set
            {
                // TODO:  Add ExControlBase.DataSource setter implementation
                if (this._DataSource != value)
                {
                    this._DataSource = value;

                    if (this._DataSource != null)
                    {
                        this.CheckDataSouceChanged();
                    }
                }
            }
        }

        [Browsable(false)]
        [Category("Data"), Description("Gets or sets the data source member whose data is manipulated by the DataNavigator control."), DefaultValue(""), Editor(ControlConstants.DataMemberEditor, typeof(System.Drawing.Design.UITypeEditor))]
        public string DataMember
        {
            get
            {
                // TODO:  Add ExControlBase.DataMember getter implementation
                return this._DataMember;
            }
            set
            {
                // TODO:  Add ExControlBase.DataMember setter implementation
                if (this._DataMember != value)
                {
                    this._DataMember = value;
                    if (this._DataMember != null && this._DataMember != string.Empty)
                    {
                        this.CheckDataSouceChanged();
                    }
                }
            }
        }

        virtual public void CalculateResult()
        {
            DataTable m_Table = this.GetDataSource();

            this._MainView.CalculateResult(m_Table);

            this._MainView.CreatePrintingTable();
        }

        virtual public void DrawContent(IGraphics gr, RectangleF rectf)	//Modified at 2009-1-6 15:28:17@Scott
        {
            this.MainView.DrawContent(gr, rectf);
        }
        #endregion

        #region IBasePrintable Members

        PrintingSystem _PS;
        virtual public void Initialize(IPrintingSystem ps, ILink link)
        {
            this._PS = ps as PrintingSystem;

            // TODO:  Add ExControlBase.Initialize implementation
            this._PS.BeginSubreport(new PointF(-this.XtraContainer.Left / (float)Webb.Utility.ConvertCoordinate, -this.XtraContainer.Top / (float)Webb.Utility.ConvertCoordinate));
        }

        virtual public void CreateArea(string areaName, IBrickGraphics graph)
        {
            // TODO:  Add ExControlBase.CreateArea implementation

            if (_PrintTimes > 0) return;

            this.PrintAllExcontrols(areaName, graph);

            _PrintTimes++;
        }

        #region Old Create Area
        //		virtual internal int InternalCreateArea(int xStartPos,int yStartPos, string areaName, IBrickGraphics graph)
        //		{
        //			int offsetX = xStartPos; //+ (int)(this.XtraContainer.Left/Webb.Utility.ConvertCoordinate);
        //
        //			int offsetY = yStartPos; //+ (int)(this.XtraContainer.Top/Webb.Utility.ConvertCoordinate);
        //
        //			BreakHeadersHeight=0;  //Add at 2009-2-25 15:34:41@Simon
        //
        //			this._MainView.SetOffset(offsetX, offsetY);
        //
        //			this._MainView.CreateArea(areaName,graph);
        //
        //			if(this._MainView.PrintingTable != null)
        //			{
        //				yStartPos = offsetY + this._MainView.PrintingTable.GetTotalHeight(this._MainView.ThreeD);	//Modified at 2008-12-18 11:26:35@Scott
        //				//yStartPos = offsetY + this._MainView.PrintingTable.GetTotalHeight();	//Modified at 2008-12-18 11:26:49@Scott
        //				
        //                 yStartPos+=BreakHeadersHeight;    //Add at 2009-2-26 11:17:30@Simon				
        //			
        //			}
        //
        //			return yStartPos;
        //		}
        #endregion

        #region New
        virtual internal int InternalCreateShapeArea(int xStartPos, int yStartPos, string areaName, IBrickGraphics graph, RectInfo rectInfo)
        {
            int offsetX = xStartPos; //+ (int)(this.XtraContainer.Left/Webb.Utility.ConvertCoordinate);

            int offsetY = yStartPos; //+ (int)(this.XtraContainer.Top/Webb.Utility.ConvertCoordinate);			

            this._MainView.SetOffset(offsetX, offsetY);

            if (this._MainView.PrintingTable != null && rectInfo.LastBottom > 0)
            {
                IWebbTableCell cell = this._MainView.PrintingTable.GetCell(0, 0);

                int penwidth = (this as ExShapeControl).BorderWidth;

                int AutolayOutHeight = rectInfo.LastBottom - yStartPos + penwidth + 1;

                if (rectInfo.LinePrintInfo.LineCtrlBottom > 0)
                {
                    int diffHeight = rectInfo.shapeControl.XtraContainer.Bottom - rectInfo.LinePrintInfo.LineCtrlBottom;

                    diffHeight = (int)(diffHeight / Webb.Utility.ConvertCoordinate);

                    if (diffHeight > 0)
                    {
                        AutolayOutHeight += diffHeight;
                    }
                }


                cell.CellStyle.Height = AutolayOutHeight;
            }

            int yEndPos = this._MainView.CreateArea(areaName, graph);

            return yEndPos;
        }

        virtual internal int InternalCreateArea(int xStartPos, int yStartPos, string areaName, IBrickGraphics graph, out int xEndPos)
        {
            int offsetX = xStartPos; //+ (int)(this.XtraContainer.Left/Webb.Utility.ConvertCoordinate);

            int offsetY = yStartPos; //+ (int)(this.XtraContainer.Top/Webb.Utility.ConvertCoordinate);			

            this._MainView.SetOffset(offsetX, offsetY);

            int yEndPos = this._MainView.CreateArea(areaName, graph);

            xEndPos = 0;

            if (this._MainView.PrintingTable != null)
            {
                if (this is ChartControlEx || this is ExShapeControl)
                {
                    xEndPos = offsetX + this._MainView.PrintingTable.GetChartWidth();

                }
                else
                {
                    xEndPos = offsetX + this._MainView.PrintingTable.GetTotalWidth(this._MainView.ThreeD);
                }

            }

            return yEndPos;
        }
        #endregion


        virtual public void Finalize(IPrintingSystem ps, ILink link)
        {
            // TODO:  Add ExControlBase.Finalize implementation
            this._PS.EndSubreport();
        }

        #endregion

        #region protected override EditorContainerHelper CreateHelper()
        protected override EditorContainerHelper CreateHelper()
        {
            return new ExControlHelper(this);
        }

        public class ExControlHelper : EditorContainerHelper
        {
            public ExControlHelper(ExControl owner)
                : base(owner)
            {
            }

            protected new ExControl Owner { get { return base.Owner as ExControl; } }

            protected override void OnRepositoryItemRemoved(RepositoryItem item)
            {
            }

            protected override void OnRepositoryItemChanged(RepositoryItem item)
            {
            }
            protected override void RaiseInvalidValueException(InvalidValueExceptionEventArgs e)
            {
            }
            protected override void RaiseValidatingEditor(BaseContainerValidateEditorEventArgs va)
            {
            }
        }
        #endregion

        #region funcions for the data source changed
        private void CheckDataSouceChanged()
        {
            //Only update the data source at the design time.
            if (DesignMode)
            {
                this.CalculateResult();
            }
        }

        //Wu.Country@2007-11-13 13:54 added this region.
        //		protected virtual void OnDataSouruceChanged()
        //		{
        //			//this._MainView.CreatePrintingTable(i_NewRawTable);
        //			this._MainView.UpdateView();
        ////			this.CalculateResult();
        ////			this.CreatePrintingTable();
        //		}
        //
        //
        //		public void CreatePrintingTable()
        //		{
        //			this._MainView.CreatePrintingTable();
        //		}

        public virtual DataTable GetDataSource()
        {
            //07-09-2008@Scott
            //Begin Edit
            DataSet ds = Webb.Reports.DataProvider.VideoPlayBackManager.DataSource;

            if (ds == null || ds.Tables.Count <= 0) return null;

            DataTable table = ds.Tables[0];

            ArrayList usedFields = new ArrayList();

            Webb.Reports.DataProvider.WebbDataProvider PublicDataProvider = Webb.Reports.DataProvider.VideoPlayBackManager.PublicDBProvider;

            if (PublicDataProvider != null && PublicDataProvider.DBSourceConfig != null && PublicDataProvider.DBSourceConfig.WebbDBType == WebbDBTypes.CoachCRM && ds.Tables.Count > 1)
            {
                #region  recalculating datatsource in CCRM mode

                this.MainView.OneValueScFilter.Filter.GetAllUsedFields(ref usedFields);

                if (this.MainView.Repeat) this.MainView.RepeatFilter.Filter.GetAllUsedFields(ref usedFields);

                this.MainView.GetALLUsedFields(ref usedFields);

                System.Collections.Generic.List<string> lstAllFields = new System.Collections.Generic.List<string>();

                if (table.Columns.Contains("PlayerInfoID"))
                {
                    StringBuilder sbFields = new StringBuilder();

                    #region Add files and create filter for select the category rows
                    foreach (string strField in usedFields)
                    {
                        if (table.Columns.Contains(strField) && !lstAllFields.Contains(strField))
                        {
                            lstAllFields.Add(strField);

                            if (sbFields.Length > 0) sbFields.Append(" or ");

                            sbFields.AppendFormat("CurrentField='{0}'", strField);
                        }
                    }
                    #endregion

                    // 10-17-2011 Scott
                    if (lstAllFields == null || lstAllFields.Count == 0)
                    {
                        return null;
                    }
                    // end

                    #region Add ID to Distinct records

                    StringBuilder sbFilterNullValues = new StringBuilder();
                    ArrayList arrayIDs = new ArrayList();   // 10-31-2011 Scott
                    if (sbFields.Length > 0 && ds.Tables.Count > 1)
                    {
                        DataRow[] filterRows = ds.Tables[1].Select(sbFields.ToString());

                        foreach (DataRow dataRow in filterRows)
                        {
                            if (dataRow["Category"] == null || dataRow["Category"] is System.DBNull) continue;

                            string IDRows = dataRow["Category"].ToString() + "ID";

                            if (!lstAllFields.Contains(IDRows))
                            {
                                lstAllFields.Add(IDRows);

                                if (IDRows.ToLower() == "teaminfoid") continue;

                                arrayIDs.Add(IDRows);

                                //if (sbFilterNullValues.Length > 0) sbFilterNullValues.Append(" or ");

                                //sbFilterNullValues.Append(IDRows + " is not null");
                            }
                        }

                        // 10-31-2011 Scott
                        foreach (string strId in arrayIDs)
                        {
                            if (strId == "PlayerInfoID" && arrayIDs.Count > 1)
                            {
                                continue;
                            }

                            if (sbFilterNullValues.Length > 0) sbFilterNullValues.Append(" or ");

                            sbFilterNullValues.Append(strId + " is not null");
                        }
                        // end
                    }
                    #endregion

                    System.Data.DataView dv = new System.Data.DataView(table);

                    if (sbFilterNullValues.Length > 0) dv.RowFilter = sbFilterNullValues.ToString();

                    table = dv.ToTable("ConvertedTable", true, lstAllFields.ToArray());
                }
                #endregion
            }

            return table;
        }
        #endregion

        #region IPrintable Members

        public void RejectChanges()
        {
            // TODO:  Add ExControl.RejectChanges implementation
        }

        public void ShowHelp()
        {
            // TODO:  Add ExControl.ShowHelp implementation
        }

        public bool HasPropertyEditor()
        {
            // TODO:  Add ExControl.HasPropertyEditor implementation
            return false;
        }

        public bool SupportsHelp()
        {
            // TODO:  Add ExControl.SupportsHelp implementation
            return false;
        }
        [Browsable(false)]
        public UserControl PropertyEditorControl
        {
            get
            {
                // TODO:  Add ExControl.PropertyEditorControl getter implementation
                return null;
            }
        }

        public void AcceptChanges()
        {
            // TODO:  Add ExControl.AcceptChanges implementation
        }

        #endregion

        //06-10-2008@Scott
        //		protected override void OnPaint(PaintEventArgs e)
        //		{
        //			if(DesignMode)
        //			{
        //				e.Graphics.DrawString(this.Name,this.Font,Brushes.Black,this.ClientRectangle);
        //			}
        //			else
        //			{
        //				OnPaintInPageHeader(e);
        //			}
        //		}
        //
        //		virtual protected void OnPaintInPageHeader(PaintEventArgs e)
        //		{
        //			e.Graphics.DrawString(this.Name,this.Font,Brushes.Black,this.ClientRectangle);
        //		}

        protected override void Dispose(bool disposing)
        {
            //ExControlManager.ExControls.Remove(this);
            try
            {
                base.Dispose(disposing);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        //Scott@2007-11-29 10:07 modified some of the following code.
        public System.Drawing.Graphics GetGraphics()
        {
            return this.CreateGraphics();
        }
    }
    #endregion

    #region public class ExControlCollection
    /*Descrition:   */
    [Serializable]
    public class ExControlCollection : CollectionBase
    {
        //Wu.Country@2007-11-23 12:52:49 PM added this collection.
        //Fields
        //Properties
        public ExControl this[int i_Index]
        {
            get { return this.InnerList[i_Index] as ExControl; }
            set { this.InnerList[i_Index] = value; }
        }
        //ctor
        public ExControlCollection() { }
        //Methods
        public int Add(ExControl i_Object)
        {
            if (this.InnerList.Contains(i_Object)) return -1;
            return this.InnerList.Add(i_Object);
        }
        public void Remove(ExControl i_Obj)
        {
            this.InnerList.Remove(i_Obj);
        }

        public bool Contains(ExControl exControl)
        {
            return this.InnerList.Contains(exControl);
        }
    }
    #endregion

    #region public class ExControlManager
    /*Descrition:   */
    public class ExControlManager
    {
        //Wu.Country@2007-11-23 12:53 PM added this class.
        //Fields

        //Properties
        //ctor
        protected ExControlManager()
        {
        }
        //Methods
    }
    #endregion
}
