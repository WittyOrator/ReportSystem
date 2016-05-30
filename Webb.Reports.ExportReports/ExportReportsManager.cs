using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Threading;
using System.Drawing.Printing;

//
using Webb;
using Webb.Collections;
using Webb.Data;
using Webb.Utilities;
//
using Webb.Reports;
using Webb.Reports.DataProvider;
using Webb.Reports.ReportDesigner;
using Webb.Reports.ReportManager;
using System.Text;
using Webb.Reports.Browser;
using Webb.Reports.ExControls;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;


namespace Webb.Reports.ExportReports
{
    /// <summary>
    /// Summary description for ExportReportsManager.
    /// </summary>
    public class ExportReportsManager
    {
        public ExportReportsManager()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public string Stone()
        {
            return "Stone";
        }

        public string AdjustPDFFile(string strFile)
        {
            string LastPDFFile = string.Empty;

            int index = strFile.LastIndexOf(".");

            if (index < 0)
            {
                LastPDFFile = strFile + ".pdf";
            }
            else
            {
                string FirstPart = strFile.Substring(0, index);

                LastPDFFile = FirstPart + ".pdf";
            }

            return LastPDFFile;



        }
        #region Old  Coach CRM
        public string CreateReport(string strInwFile, string strFile, string dllpath)
        {
            Webb.Utility.CurReportMode = 2;

            strFile = AdjustPDFFile(strFile);

            string[] args = InwManager.ReadInwFile(strInwFile);

            if (args == null) return "Failed";


            CommandManager m_CmdManager = new CommandManager(args);

            DBSourceConfig m_Config = m_CmdManager.CreateDBConfig();

            WebbDataProvider m_DBProvider = new WebbDataProvider(m_Config);

            WebbDataSource m_DBSource = new WebbDataSource();

            try
            {
                m_DBProvider.GetDataSource(m_Config, m_DBSource);

                ArrayList m_Fields = new ArrayList();

                foreach (System.Data.DataColumn m_col in m_DBSource.DataSource.Tables[0].Columns)
                {
                    if (m_col.Caption == "{EXTENDCOLUMNS}" && m_col.ColumnName.StartsWith("C_")) continue;

                    m_Fields.Add(m_col.ColumnName);
                }

                Webb.Data.PublicDBFieldConverter.SetAvailableFields(m_Fields);

                Webb.Reports.DataProvider.VideoPlayBackManager.DataSource = m_DBSource.DataSource;	//Set dataset for click event

                Webb.Reports.DataProvider.VideoPlayBackManager.PublicDBProvider = m_DBProvider;

                Webb.Reports.DataProvider.VideoPlayBackManager.LoadAdvScFilters();	//Modified at 2009-1-19 13:48:30@Scott

                m_DBProvider.UpdateEFFDataSource(m_DBSource);

                Webb.Reports.DataProvider.VideoPlayBackManager.ReadPictureDirFromRegistry();

            }
            catch (Exception e)
            {
                return "Failed:at addding datasouce|" + e.Message;
            }

            FilterInfoCollection filterInfos = m_DBSource.Filters;  //2009-7-1 11:09:08@Simon Add this Code  For Union Print

            if (filterInfos == null) filterInfos = new FilterInfoCollection();

            if (m_Config.Templates.Count <= 0) return "Failed:No tempaltes";

            string strTemplate = m_Config.Templates[0];

            string strTemplateName = m_CmdManager.GetTemplateName(strTemplate, '@');	//Modified at 2009-2-3 9:17:34@Scott

            WebbReport m_Report = null;

            try
            {
                m_Report = m_CmdManager.CreateReport(dllpath, strTemplateName);	//1 //create report with template
            }
            catch (Exception ex)
            {
                return "Failed:Can't load report template" + ex.Message;
            }

            //Add attached filter here
            #region Modified Area

            m_DBSource.Filters = filterInfos.Copy();  //2009-7-1 11:09:04@Simon Add this Code  For Union Print 

            string strFilterName = m_CmdManager.GetAttachedFilter(strTemplate, '@');

            if (strFilterName != string.Empty)  //2009-7-1 11:09:04@Simon For display Filternames In GameListInfo
            {
                if (!m_DBProvider.DBSourceConfig.Filters.Contains(strFilterName))
                {
                    FilterInfo filterInfo = new FilterInfo();

                    filterInfo.FilterName = strFilterName;

                    m_DBSource.Filters.Add(filterInfo);
                }
            }

            ScAFilter scaFilter = Webb.Reports.DataProvider.VideoPlayBackManager.AdvReportFilters.GetFilter(strFilterName);	//Modified at 2009-1-19 14:25:30@Scott

            AdvFilterConvertor convertor = new AdvFilterConvertor();

            DBFilter AdvFilter = convertor.GetReportFilter(scaFilter).Filter;

            if (AdvFilter != null || AdvFilter.Count > 0)  //2009-5-6 9:38:37@Simon Add this Code
            {
                AdvFilter.Add(m_Report.Template.Filter);
                m_Report.Template.Filter = AdvFilter;
            }

            SectionFilterCollection sectionFilter = m_Report.Template.SectionFilters.Copy();

            if (m_Report.Template.ReportScType == ReportScType.Custom)
            {
                m_Report.Template.SectionFilters = AdvFilterConvertor.GetCustomFilters(Webb.Reports.DataProvider.VideoPlayBackManager.AdvReportFilters, sectionFilter);
            }

            #endregion        //Modify at 2008-11-24 16:04:05@Scott

            try
            {
                if (System.IO.File.Exists(strFile))
                {
                    System.IO.File.Delete(strFile);
                }

                m_Report.LoadAdvSectionFilters(m_Config.UserFolder);

                m_Report.SetWatermark(m_Config.WartermarkImagePath);	//06-19-2008@Scott

                m_Report.SetDataSource(m_DBSource);

                m_Report.CreatePdfDocument(strFile);

                if (!System.IO.File.Exists(strFile))
                {
                    return "Failed:dll path " + dllpath;
                }
            }
            catch (Exception ex2)
            {
                return "Failed:in create PDf Documnet|" + ex2.Message;
            }

            return strFile;

        }

        public string CreateReport(DataSet ds, string strDestineFile, string dllpath, string strUserFolder, string TemplateFileName, int[] TablesIndex)
        {

            strDestineFile = AdjustPDFFile(strDestineFile);

            if (!strUserFolder.EndsWith(@"\")) strUserFolder += @"\";

            string[] templateFiles = TemplateFileName.Split(new char[] { ',' });

            XtraReport[] preparedReports = new WebbReport[templateFiles.Length];

            if (System.IO.File.Exists(strDestineFile))
            {
                System.IO.File.Delete(strDestineFile);
            }

            try
            {
                for (int i = 0; i < templateFiles.Length; i++)
                {
                    templateFiles[i] = strUserFolder + templateFiles[i];

                    string strTemplate = templateFiles[i];

                    if (strTemplate != string.Empty)
                    {
                        int index = 0;

                        if (i < TablesIndex.Length) index = TablesIndex[i];

                        DataTable table = ds.Tables[index].Copy();

                        WebbDataSource m_DBSource = new WebbDataSource();

                        m_DBSource.DataSource.Tables.Add(table);

                        ArrayList m_Fields = new ArrayList();

                        foreach (System.Data.DataColumn m_col in m_DBSource.DataSource.Tables[0].Columns)
                        {
                            if (m_col.Caption == "{EXTENDCOLUMNS}" && m_col.ColumnName.StartsWith("C_")) continue;

                            m_Fields.Add(m_col.ColumnName);
                        }

                        Webb.Data.PublicDBFieldConverter.SetAvailableFields(m_Fields);

                        Webb.Reports.DataProvider.VideoPlayBackManager.DataSource = m_DBSource.DataSource;	//Set dataset for click event

                        WebbReport m_Report = new WebbReport();

                        Webb.Utility.ReplaceRefPathInRepxFile(strTemplate, dllpath);	//Modify template file

                        m_Report.LoadLayout(strTemplate);

                        Webb.Utility.CurFileName = strTemplate;	//06-23-2008@Scott	

                        m_Report.SetDataSource(m_DBSource);

                        preparedReports[i] = m_Report;
                    }
                }
            }
            catch (Exception ex)
            {
                return "Failed:in create report|" + ex.Message;
            }

            try
            {
                if (preparedReports.Length == 1)
                {
                    preparedReports[0].CreatePdfDocument(strDestineFile);
                }
                else
                {
                    XtraReport.UnionReportsToPdf(preparedReports, strDestineFile);
                }
            }
            catch (Exception ex2)
            {
                return "Failed:in create PDf Document|" + ex2.Message;
            }
            if (!System.IO.File.Exists(strDestineFile))
            {
                return "Failed:Undestine Pdf Error in create PDF";
            }

            return strDestineFile;

        }

        public string ExportUnionReportToPdf(string strInwFile, string DestinationPdfFile)
        {
            Webb.Utility.CurReportMode = 2;

            DestinationPdfFile = AdjustPDFFile(DestinationPdfFile);

            string[] args = InwManager.ReadInwFile(strInwFile);

            if (args == null) return "Failed";

            CommandManager m_CmdManager = new CommandManager(args);

            DBSourceConfig m_Config = m_CmdManager.CreateDBConfig();

            WebbDataProvider m_DBProvider = new WebbDataProvider(m_Config);

            WebbDataSource m_DBSource = new WebbDataSource();

            try
            {

                m_DBProvider.GetDataSource(m_Config, m_DBSource);


                ArrayList m_Fields = new ArrayList();

                foreach (System.Data.DataColumn m_col in m_DBSource.DataSource.Tables[0].Columns)
                {
                    if (m_col.Caption == "{EXTENDCOLUMNS}" && m_col.ColumnName.StartsWith("C_")) continue;

                    m_Fields.Add(m_col.ColumnName);
                }

                Webb.Data.PublicDBFieldConverter.SetAvailableFields(m_Fields);

                Webb.Reports.DataProvider.VideoPlayBackManager.DataSource = m_DBSource.DataSource;	//Set dataset for click event

                Webb.Reports.DataProvider.VideoPlayBackManager.PublicDBProvider = m_DBProvider;

                //Webb.Reports.DataProvider.VideoPlayBackManager.ReadPictureDirFromRegistry();

                Webb.Reports.DataProvider.VideoPlayBackManager.LoadAdvScFilters();	//Modified at 2009-1-19 13:48:30@Scott

                m_DBProvider.UpdateEFFDataSource(m_DBSource);

            }
            catch (Exception e)
            {
                return "Failed:at addding datasouce|" + e.Message;
            }

            FilterInfoCollection filterInfos = m_DBSource.Filters;  //2009-7-1 11:09:08@Simon Add this Code  For Union Print

            if (filterInfos == null) filterInfos = new FilterInfoCollection();

            if (m_Config.Templates.Count <= 0) return "Failed";

            XtraReport[] preparedReports = new WebbReport[m_Config.Templates.Count];

            if (System.IO.File.Exists(DestinationPdfFile))
            {
                System.IO.File.Delete(DestinationPdfFile);
            }

            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();

            string dllPath = System.IO.Path.GetDirectoryName(asm.Location);

            if (!dllPath.EndsWith(@"\")) dllPath += @"\";

            for (int i = 0; i < m_Config.Templates.Count; i++)
            {
                string strTemplate = m_Config.Templates[i];

                string strTemplateName = m_CmdManager.GetTemplateName(strTemplate, '@');	//Modified at 2009-2-3 9:17:34@Scott

                WebbReport m_Report = null;

                try
                {
                    m_Report = m_CmdManager.CreateReport(dllPath, strTemplateName);	//1 //create report with template
                }
                catch (Exception ex)
                {
                    return "Failed:in create report" + ex.Message;
                }

                //Add attached filter here
                #region Modified Area

                m_DBSource.Filters = filterInfos.Copy();  //2009-7-1 11:09:04@Simon Add this Code  For Union Print 

                string strFilterName = m_CmdManager.GetAttachedFilter(strTemplate, '@');

                if (strFilterName != string.Empty)  //2009-7-1 11:09:04@Simon For display Filternames In GameListInfo
                {
                    if (!m_DBProvider.DBSourceConfig.Filters.Contains(strFilterName))
                    {
                        FilterInfo filterInfo = new FilterInfo();

                        filterInfo.FilterName = strFilterName;

                        m_DBSource.Filters.Add(filterInfo);
                    }
                }

                ScAFilter scaFilter = Webb.Reports.DataProvider.VideoPlayBackManager.AdvReportFilters.GetFilter(strFilterName);	//Modified at 2009-1-19 14:25:30@Scott

                AdvFilterConvertor convertor = new AdvFilterConvertor();

                DBFilter AdvFilter = convertor.GetReportFilter(scaFilter).Filter;

                if (AdvFilter != null || AdvFilter.Count > 0)  //2009-5-6 9:38:37@Simon Add this Code
                {
                    AdvFilter.Add(m_Report.Template.Filter);
                    m_Report.Template.Filter = AdvFilter;
                }
                #endregion        //Modify at 2008-11-24 16:04:05@Scott

                try
                {

                    m_Report.LoadAdvSectionFilters(m_Config.UserFolder);

                    m_Report.SetWatermark(m_Config.WartermarkImagePath);	//06-19-2008@Scott

                    m_Report.SetDataSource(m_DBSource);

                    preparedReports[i] = m_Report;
                }
                catch (Exception ex2)
                {
                    return "Failed:load report|" + ex2.Message;
                }
            }
            try
            {
                XtraReport.UnionReportsToPdf(preparedReports, DestinationPdfFile);
            }
            catch (Exception ex3)
            {
                return "Failed:in create PDf Documnet|" + ex3.Message;
            }

            if (!System.IO.File.Exists(DestinationPdfFile))
            {
                return "Failed:file didn't exist";
            }

            return DestinationPdfFile;
        }
        #endregion

        #region Sub Functions
        private DataTable UnionAllTableInto1Table(DataTable playerTable, DataTable teamTable, DataTable coachTable, DataTable sponsorTable)
        {
            DataTable DestineTable = new DataTable();

            foreach (DataColumn dc in playerTable.Columns)
            {
                DestineTable.Columns.Add(dc.ColumnName, dc.DataType);
            }

            foreach (DataRow dr in playerTable.Rows)
            {
                DestineTable.ImportRow(dr);
            }

            foreach (DataColumn dc in coachTable.Columns)
            {
                DestineTable.Columns.Add(dc.ColumnName, dc.DataType);
            }

            foreach (DataColumn dc in teamTable.Columns)
            {
                DestineTable.Columns.Add(dc.ColumnName, dc.DataType);
            }

            foreach (DataColumn dc in sponsorTable.Columns)
            {
                DestineTable.Columns.Add(dc.ColumnName, dc.DataType);
            }

            if (playerTable.Rows.Count >= coachTable.Rows.Count && playerTable.Rows.Count >= sponsorTable.Rows.Count)
            {
                #region  Add Coach and  Team Table  into Playertable
                for (int i = 0; i < playerTable.Rows.Count; i++)
                {
                    if (i < coachTable.Rows.Count)
                    {
                        for (int j = 0; j < coachTable.Columns.Count; j++)
                        {
                            string columnName = coachTable.Columns[j].ColumnName;

                            DestineTable.Rows[i][columnName] = coachTable.Rows[i][columnName];
                        }
                    }

                    // 10-24-2011 Scott
                    if (i < sponsorTable.Rows.Count)
                    {
                        for (int j = 0; j < sponsorTable.Columns.Count; j++)
                        {
                            string columnName = sponsorTable.Columns[j].ColumnName;

                            DestineTable.Rows[i][columnName] = sponsorTable.Rows[i][columnName];
                        }
                    }

                    if (teamTable.Rows.Count == 0) continue;

                    //Repeat team Rows
                    for (int j = 0; j < teamTable.Columns.Count; j++)
                    {
                        string columnName = teamTable.Columns[j].ColumnName;

                        DestineTable.Rows[i][columnName] = teamTable.Rows[0][columnName];
                    }
                }
                #endregion
            }
            else
            {
                #region  Union Player and  Team Table  into Coachtable

                // Add new rows first
                for (int i = 0; i < System.Math.Max(coachTable.Rows.Count, sponsorTable.Rows.Count) - playerTable.Rows.Count; i++)
                {
                    DataRow dataRow = DestineTable.NewRow();

                    DestineTable.Rows.Add(dataRow);
                }

                for (int i = 0; i < System.Math.Max(coachTable.Rows.Count, sponsorTable.Rows.Count); i++)
                {
                    if (i < coachTable.Rows.Count)
                    {
                        for (int j = 0; j < coachTable.Columns.Count; j++)
                        {
                            string columnName = coachTable.Columns[j].ColumnName;

                            DestineTable.Rows[i][columnName] = coachTable.Rows[i][columnName];
                        }
                    }

                    // 10-24-2011 Scott
                    if (i < sponsorTable.Rows.Count)
                    {
                        for (int j = 0; j < sponsorTable.Columns.Count; j++)
                        {
                            string columnName = sponsorTable.Columns[j].ColumnName;

                            DestineTable.Rows[i][columnName] = sponsorTable.Rows[i][columnName];
                        }
                    }

                    if (teamTable.Rows.Count == 0) continue;

                    //Repeat team Rows
                    for (int j = 0; j < teamTable.Columns.Count; j++)
                    {
                        string columnName = teamTable.Columns[j].ColumnName;

                        DestineTable.Rows[i][columnName] = teamTable.Rows[0][columnName];
                    }
                }

                #endregion
            }

            DestineTable.TableName = "[All Avaliable Fields]";

            return DestineTable;
        }

        private void FillImagePath(string imgBaseDirectory, DataTable table, string imgField)
        {
            if (!table.Columns.Contains(imgField)) return;

            table.Columns[imgField].MaxLength = int.MaxValue;

            if (!imgBaseDirectory.EndsWith(@"\")) imgBaseDirectory += @"\";

            foreach (DataRow dr in table.Rows)
            {
                if (dr[imgField] == null || dr[imgField] is System.DBNull) continue;

                string strPhotoPath = dr[imgField].ToString().Replace(@"/", @"\");

                string strTargetImagePath = imgBaseDirectory + strPhotoPath;

                dr[imgField] = strTargetImagePath;
            }
        }

        private DataSet ReplaceImagePathAndUnionTable(DataSet originalDs, string imgBaseDirectory)
        {
            DataTable playerTable = originalDs.Tables["[Player ContactInformation]"];

            DataTable teamTable = originalDs.Tables["[Team Info]"];

            DataTable coachTable = originalDs.Tables["[Coach Info]"];

            DataTable sponsorTable = new DataTable();
            if(originalDs.Tables.Contains("[Sponsor Info]"))
            {
                sponsorTable = originalDs.Tables["[Sponsor Info]"];
            }

            this.FillImagePath(imgBaseDirectory, playerTable, "Photo(Player)");

            this.FillImagePath(imgBaseDirectory, teamTable, "Logo(TeamInfo)");

            this.FillImagePath(imgBaseDirectory, coachTable, "Photo(Coach)");

            DataTable unionedTable = this.UnionAllTableInto1Table(playerTable, teamTable, coachTable, sponsorTable);

            DataSet resultDataSet = new DataSet();

            resultDataSet.Tables.Add(unionedTable);

            DataTable tableStructure = originalDs.Tables["[TableStructure]"];

            resultDataSet.Tables.Add(tableStructure.Copy());

            return resultDataSet;
        }
        #endregion

        public string CreateGameReport(string[] strCommands, string strDestinePdfFile, string strDllPath)
        {
            Webb.Utility.CurReportMode = 2;

            strDestinePdfFile = AdjustPDFFile(strDestinePdfFile);

            if (strCommands == null) return "Failed";

            CommandManager m_CmdManager = new CommandManager(strCommands);

            DBSourceConfig m_Config = m_CmdManager.CreateDBConfig();

            WebbDataProvider m_DBProvider = new WebbDataProvider(m_Config);

            WebbDataSource m_DBSource = new WebbDataSource();

            try
            {
                m_DBProvider.GetDataSource(m_Config, m_DBSource);

                ArrayList m_Fields = new ArrayList();

                foreach (System.Data.DataColumn m_col in m_DBSource.DataSource.Tables[0].Columns)
                {
                    if (m_col.Caption == "{EXTENDCOLUMNS}" && m_col.ColumnName.StartsWith("C_")) continue;

                    m_Fields.Add(m_col.ColumnName);
                }

                Webb.Data.PublicDBFieldConverter.SetAvailableFields(m_Fields);

                Webb.Reports.DataProvider.VideoPlayBackManager.DataSource = m_DBSource.DataSource;	//Set dataset for click event

                Webb.Reports.DataProvider.VideoPlayBackManager.PublicDBProvider = m_DBProvider;

                Webb.Reports.DataProvider.VideoPlayBackManager.LoadAdvScFilters();	//Modified at 2009-1-19 13:48:30@Scott

                m_DBProvider.UpdateEFFDataSource(m_DBSource);
            }
            catch (Exception e)
            {
                return "Failed:at addding datasouce|" + e.Message;
            }


            if (m_Config.Templates.Count <= 0) return "Failed:No tempaltes";


            XtraReport[] preparedReports = new WebbReport[m_Config.Templates.Count];

            for (int i = 0; i < m_Config.Templates.Count; i++)
            {
                string strTemplateName = m_Config.Templates[i];

                WebbReport m_Report = null;

                try
                {
                    m_Report = m_CmdManager.CreateReport(strDllPath, strTemplateName);	//1 //create report with template
                }
                catch (Exception ex)
                {
                    return "Failed:Can't load report template" + ex.Message;
                }

                try
                {
                    m_Report.LoadAdvSectionFilters(m_Config.UserFolder);

                    m_Report.SetWatermark(m_Config.WartermarkImagePath);	//06-19-2008@Scott

                    m_Report.SetDataSource(m_DBSource);

                    preparedReports[i] = m_Report;

                }
                catch (Exception ex2)
                {
                    return "Failed:in create PDf Documnet\r\n" + strTemplateName + "\r\n" + ex2.Message;
                }
            }
            try
            {
                if (System.IO.File.Exists(strDestinePdfFile))
                {
                    System.IO.File.Delete(strDestinePdfFile);
                }

                XtraReport.UnionReportsToPdf(preparedReports, strDestinePdfFile);
            }
            catch (Exception ex3)
            {
                return "Failed:in create PDf Documnet|" + ex3.Message;
            }

            if (!System.IO.File.Exists(strDestinePdfFile))
            {
                return "Failed:some error happed,and report could not be created!";
            }

            return strDestinePdfFile;

        }

        //10-14-2011 Scott Read
        public string CreatePlayerReport(DataSet ds, string strDestineFile, string dllpath, string strUserFolder, string[] templateFiles)
        {
            WebbLog.Instance.WriteLog("AdjustPDFFile");

            strDestineFile = AdjustPDFFile(strDestineFile);

            WebbLog.Instance.WriteLog("AdjustPDFFile Complete");

            if (!strUserFolder.EndsWith(@"\")) strUserFolder += @"\";

            WebbLog.Instance.WriteLog("new WebbReport");

            XtraReport[] preparedReports = new WebbReport[templateFiles.Length];

            WebbLog.Instance.WriteLog("new WebbReport Complete");

            try
            {
                if (System.IO.File.Exists(strDestineFile))
                {
                    System.IO.File.Delete(strDestineFile);
                }

                WebbLog.Instance.WriteLog("ReplaceImagePathAndUnionTable");

                ds = this.ReplaceImagePathAndUnionTable(ds, strUserFolder);

                WebbLog.Instance.WriteLog("ReplaceImagePathAndUnionTable Complete");

                DBSourceConfig dBSourceConfig = new DBSourceConfig();

                dBSourceConfig.WebbDBType = WebbDBTypes.CoachCRM;

                Webb.Reports.DataProvider.VideoPlayBackManager.PublicDBProvider = new WebbDataProvider(dBSourceConfig);

                WebbLog.Instance.WriteLog("for");

                for (int i = 0; i < templateFiles.Length; i++)
                {
                    WebbLog.Instance.WriteLog("1");

                    templateFiles[i] = templateFiles[i].Replace(@"/", @"\");

                    string strTemplate = strUserFolder + templateFiles[i];

                    if (strTemplate != string.Empty)
                    {
                        WebbLog.Instance.WriteLog("2");

                        WebbDataSource m_DBSource = new WebbDataSource();

                        m_DBSource.DataSource = ds.Copy();

                        WebbLog.Instance.WriteLog("3");

                        ArrayList m_Fields = new ArrayList();

                        foreach (System.Data.DataColumn m_col in m_DBSource.DataSource.Tables[0].Columns)
                        {
                            if (m_col.Caption == "{EXTENDCOLUMNS}" && m_col.ColumnName.StartsWith("C_")) continue;

                            m_Fields.Add(m_col.ColumnName);
                        }

                        WebbLog.Instance.WriteLog("5");

                        Webb.Data.PublicDBFieldConverter.SetAvailableFields(m_Fields);

                        WebbLog.Instance.WriteLog("6");

                        Webb.Reports.DataProvider.VideoPlayBackManager.DataSource = m_DBSource.DataSource;	//Set dataset for click event

                        WebbReport m_Report = new WebbReport();

                        WebbLog.Instance.WriteLog("7");

                        Webb.Utility.ReplaceRefPathInRepxFile(strTemplate, dllpath);	//Modify template file

                        WebbLog.Instance.WriteLog("8");

                        m_Report.LoadLayout(strTemplate);

                        WebbLog.Instance.WriteLog("9");

                        Webb.Utility.CurFileName = strTemplate;	//06-23-2008@Scott	

                        m_Report.SetDataSource(m_DBSource);

                        preparedReports[i] = m_Report;

                        WebbLog.Instance.WriteLog("10");
                    }
                }

                WebbLog.Instance.WriteLog("for complete");
            }
            catch (Exception ex)
            {
                return "Failed:in create report|" + ex.Message;
            }

            try
            {
                WebbLog.Instance.WriteLog("11");

                if (preparedReports.Length == 1)
                {
                    WebbLog.Instance.WriteLog("22");

                    preparedReports[0].CreatePdfDocument(strDestineFile);

                    WebbLog.Instance.WriteLog("33");
                }
                else
                {
                    WebbLog.Instance.WriteLog("44");

                    XtraReport.UnionReportsToPdf(preparedReports, strDestineFile);

                    WebbLog.Instance.WriteLog("55");
                }
            }
            catch (Exception ex2)
            {
                return "Failed:in create PDf Document|" + ex2.Message;
            }
            if (!System.IO.File.Exists(strDestineFile))
            {
                return "Failed:Undestine Pdf Error in create PDF";
            }

            return strDestineFile;
        }
    }
}
