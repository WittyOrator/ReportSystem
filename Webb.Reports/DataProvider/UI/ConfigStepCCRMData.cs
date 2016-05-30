using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Webb.Data;
using System.Xml;
using System.Diagnostics;
using Webb;

namespace Webb.Reports.DataProvider.UI
{
    public partial class ConfigStepCCRMData : Webb.Utilities.Wizards.WinzardControlBase
    {

        protected readonly string configFile = Webb.Utility.ApplicationDirectory + @"SubPrograms\DownLoadCCRMData.exe.config";

        protected readonly string subProgramFile = Webb.Utility.ApplicationDirectory + @"SubPrograms\DownLoadCCRMData.exe";

        public bool _ValidateSetting = false;
      
        private CCRMConfigData _CCRMConfigData = null;

        public Process DownloadListProgram = null;
        public Process DownloadDataProgram = null;

        public static string CCRMServerConfigPath
        {
            get
            {
                string strPath = System.Windows.Forms.Application.StartupPath;

                if (!strPath.EndsWith(@"\")) strPath = strPath + @"\";

                strPath = strPath + @"Template\DataConfig\CCRMDataFiles";

                if (!System.IO.Directory.Exists(strPath))
                {
                    System.IO.Directory.CreateDirectory(strPath);
                }

                strPath = strPath + @"\ServerConfig.dat";

                return strPath;
            }
        }


        public override bool ValidateSetting()
        {// 08-29-2011 Scott
            if (this.cmbServer.Text.Trim() == string.Empty)
            {
                Webb.Utilities.MessageBoxEx.ShowError("Please input the CCRM server address !");

                this.cmbServer.Focus();

                return false;

            }

            else if (this.txtDataLocation.Text.Trim() == string.Empty)
            {
                Webb.Utilities.MessageBoxEx.ShowError("Please select  data saved path! ");

                return false;
            }

            bool changeServer = ChangeServerIPLocation();

            if (!changeServer) return false;

            return true;

            //_ValidateSetting = BeginToDownLoadDataFromServer();

            //return _ValidateSetting;
        }


        public ConfigStepCCRMData()
        {
            InitializeComponent();

            this.FinishControl = true;
            this.LastControl = true;
        }
       
        public CCRMConfigData CreateDefaultCCRM()
        {
            string strPath = Webb.Utility.ApplicationDirectory;

            strPath = strPath + @"Template\DataConfig\CCRMDataFiles\";

            if (!System.IO.Directory.Exists(strPath))
            {
                System.IO.Directory.CreateDirectory(strPath);
            }

            return Webb.Data.CCRMConfigData.CreateInitialData(strPath);

        }
        private string GetServerAddress()
        {
            XmlDocument xmldoc = new XmlDocument();

            xmldoc.Load(configFile);

            XmlNode xmlNode = xmldoc.SelectSingleNode(@"/configuration/applicationSettings/Webb.Reports.CoachCRMService.Properties.Settings/setting/value");

            string strText = xmlNode.InnerText;

            if (strText.StartsWith(@"http://"))
            {
                strText = strText.Substring(7);

                int index = strText.IndexOf(@"/");

                if (index > 0)
                {
                    return strText.Substring(0, index);
                }
            }
            return string.Empty;

        }

        public override void SetData(object i_Data)
        {
            if(!(i_Data is DBSourceConfig))return;

            _ValidateSetting = true;

            this.SetConfig(i_Data as DBSourceConfig);
        }
        public override void UpdateData(object i_Data)
        {          
            this.UpdateConfig(i_Data as DBSourceConfig);
        }       


        #region IDataSourceConfigControl Members
        public UserControl UserControl
        {
            get
            {
                return this;
            }
        }
        public void SetConfig(Webb.Reports.DataProvider.DBSourceConfig config)
        {
            _CCRMConfigData = Webb.Data.CCRMConfigData.LoadConfig(CCRMServerConfigPath);

            if (_CCRMConfigData == null)
            {
                _CCRMConfigData = CreateDefaultCCRM();
            }

            this.txtLoginName.Text = _CCRMConfigData.LoginName;

            this.cmbServer.Text = GetServerAddress();

            this.txtDataLocation.Text = _CCRMConfigData.DataSavedLocation + "[Player ContactInformation].wrdf"; 

        }

        public bool UpdateConfig(Webb.Reports.DataProvider.DBSourceConfig config)
        {
            _ValidateSetting = BeginToDownLoadDataFromServer();
           
            config.WebbDBType = Webb.Reports.DataProvider.WebbDBTypes.CoachCRM;

            config.DBFilePath = this.txtDataLocation.Text;

            config.DBConnType = Webb.Data.DBConnTypes.XMLFile;

            return true;
        }

        #endregion

        private bool ChangeServerIPLocation()
        {
            #region get input text

            string strServerIp = this.cmbServer.Text.Trim();

            if (strServerIp.Contains(@"//"))
            {
                int ATIndex = strServerIp.IndexOf(@"//");

                strServerIp = strServerIp.Substring(ATIndex + 2);

            }
            if (strServerIp.Contains(@"/"))
            {
                int ATIndex = strServerIp.IndexOf(@"/");

                strServerIp = strServerIp.Substring(0, ATIndex);

            }

            if (strServerIp == string.Empty)
            {
                Webb.Utilities.MessageBoxEx.ShowError("Please input the CCRM server address !");

                this.cmbServer.Focus();

                return false;
            }

            #endregion

            XmlDocument xmldoc = new XmlDocument();

            xmldoc.Load(configFile);

            XmlNode xmlNode = xmldoc.SelectSingleNode(@"/configuration/applicationSettings/Webb.Reports.CoachCRMService.Properties.Settings/setting/value");

            string strText = xmlNode.InnerText;

            if (strText.StartsWith(@"http://"))
            {
                strText = strText.Substring(7);

                int index = strText.IndexOf(@"/");

                xmlNode.InnerText = @"http://" + strServerIp + strText.Substring(index);

                xmldoc.Save(configFile);
            }
            else
            {
                xmlNode.InnerText = @"http://" + strServerIp +@"/CMRServices/CMRService.svc";               
            }

            return true;

        }


        private bool BeginToDownLoadDataFromServer()
        {
            if (this.cmbServer.Text.Trim() == string.Empty)
            {
                Webb.Utilities.MessageBoxEx.ShowError("Please input the CCRM server address !");

                this.cmbServer.Focus();

                return false;

            }          

            else if (this.txtDataLocation.Text.Trim() == string.Empty)
            {
                Webb.Utilities.MessageBoxEx.ShowError("Please select  data saved path! ");

                return false;
            }

             bool changeServer=ChangeServerIPLocation();

             if (!changeServer) return false;

            string strDirectoryPath = System.IO.Path.GetDirectoryName(this.txtDataLocation.Text);

            if (!System.IO.Directory.Exists(strDirectoryPath))
            {
                System.IO.Directory.CreateDirectory(strDirectoryPath);
            }

            DownloadDataProgram = new Process();

            DownloadDataProgram.StartInfo = new ProcessStartInfo(subProgramFile);

            string strLoginName = this.txtLoginName.Text;

            string strPassword = this.txtPwd.Text;

            string selectAllRecord = this.chkDownLoadLateset.Checked ? "0" : "1";

            string strSavedLocation = this.txtDataLocation.Text;

            string strArguments = string.Format("DownLoadCategoryData \"{0}\" \"{1}\" \"{2}\" \"{3}\"", strLoginName, strPassword, selectAllRecord, strSavedLocation);

            DownloadDataProgram.StartInfo.Arguments = strArguments;

            DownloadDataProgram.Start();

            DownloadDataProgram.WaitForExit();

            if (DownloadDataProgram.ExitCode != 0) return false;

            if (_CCRMConfigData == null) _CCRMConfigData = new CCRMConfigData();

            _CCRMConfigData.ServerAddress = this.cmbServer.Text;

            if (!strDirectoryPath.EndsWith(@"\")) strDirectoryPath = strDirectoryPath + @"\";

            _CCRMConfigData.DataSavedLocation = strDirectoryPath;

            _CCRMConfigData.LoginName = this.txtLoginName.Text;

            Webb.Data.CCRMConfigData.SaveConfigFiles(_CCRMConfigData, CCRMServerConfigPath);

            return true;
        }   
    
  
        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();

            if (this._CCRMConfigData != null) fileDialog.InitialDirectory = this._CCRMConfigData.DataSavedLocation;

            fileDialog.OverwritePrompt = false;

            fileDialog.Title = "CCRM DataFile Saved Location";

            fileDialog.Filter = "CCRM DataFile(*.wrdf)|*.wrdf";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                this.txtDataLocation.Text = fileDialog.FileName;
            }
        }

     
    }
}
