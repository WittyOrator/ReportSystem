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
using Webb.Reports.ReportWizard.WizardInfo;

namespace Webb.Reports.ReportWizard.DataSourceProvider
{
    public class ConfigCoachCRMAccess :UserControl,IDataSourceConfigControl
    {
        public bool bGame = true;
        private System.Windows.Forms.FolderBrowserDialog C_FolderBrowserDlg;
        //private StringCollection _Games;
        //private StringCollection _Edls;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;
        private GroupBox GroupDownLoadOnLine;
        private ComboBox cmbServer;
        private Label label1;
        private Label label2;
        private Button BtnBrowse;
        private TextBox txtDataLocation;

        //static string ServerConfigPath = Webb.Utility.ApplicationDirectory + @"SubPrograms\ServerConfig.dat";

        protected readonly string configFile = Webb.Utility.ApplicationDirectory + @"SubPrograms\DownLoadCCRMData.exe.config";

        protected readonly string subProgramFile = Webb.Utility.ApplicationDirectory + @"SubPrograms\DownLoadCCRMData.exe";

        private CCRMConfigData _CCRMConfigData = null;

        public Process DownloadListProgram = null;
        private TextBox txtPwd;
        private TextBox txtLoginName;
        private Label label4;
        private Label label3;
        private CheckBox chkDownLoadLateset;
        public Process DownloadDataProgram = null;        


        public ConfigCoachCRMAccess()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();         
                       

            //this.ClearAllList();
            // TODO: Add any initialization after the InitializeComponent call
        }



        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.C_FolderBrowserDlg = new System.Windows.Forms.FolderBrowserDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.GroupDownLoadOnLine = new System.Windows.Forms.GroupBox();
            this.chkDownLoadLateset = new System.Windows.Forms.CheckBox();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.txtLoginName = new System.Windows.Forms.TextBox();
            this.BtnBrowse = new System.Windows.Forms.Button();
            this.txtDataLocation = new System.Windows.Forms.TextBox();
            this.cmbServer = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.GroupDownLoadOnLine.SuspendLayout();
            this.SuspendLayout();
            // 
            // C_FolderBrowserDlg
            // 
            this.C_FolderBrowserDlg.ShowNewFolderButton = false;
            // 
            // GroupDownLoadOnLine
            // 
            this.GroupDownLoadOnLine.Controls.Add(this.chkDownLoadLateset);
            this.GroupDownLoadOnLine.Controls.Add(this.txtPwd);
            this.GroupDownLoadOnLine.Controls.Add(this.txtLoginName);
            this.GroupDownLoadOnLine.Controls.Add(this.BtnBrowse);
            this.GroupDownLoadOnLine.Controls.Add(this.txtDataLocation);
            this.GroupDownLoadOnLine.Controls.Add(this.cmbServer);
            this.GroupDownLoadOnLine.Controls.Add(this.label2);
            this.GroupDownLoadOnLine.Controls.Add(this.label4);
            this.GroupDownLoadOnLine.Controls.Add(this.label3);
            this.GroupDownLoadOnLine.Controls.Add(this.label1);
            this.GroupDownLoadOnLine.Dock = System.Windows.Forms.DockStyle.Top;
            this.GroupDownLoadOnLine.Location = new System.Drawing.Point(0, 0);
            this.GroupDownLoadOnLine.Name = "GroupDownLoadOnLine";
            this.GroupDownLoadOnLine.Size = new System.Drawing.Size(950, 415);
            this.GroupDownLoadOnLine.TabIndex = 13;
            this.GroupDownLoadOnLine.TabStop = false;
            // 
            // chkDownLoadLateset
            // 
            this.chkDownLoadLateset.AutoSize = true;
            this.chkDownLoadLateset.Checked = true;
            this.chkDownLoadLateset.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDownLoadLateset.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDownLoadLateset.Location = new System.Drawing.Point(44, 258);
            this.chkDownLoadLateset.Name = "chkDownLoadLateset";
            this.chkDownLoadLateset.Size = new System.Drawing.Size(416, 23);
            this.chkDownLoadLateset.TabIndex = 16;
            this.chkDownLoadLateset.Text = "Only download the latest record for each player";
            this.chkDownLoadLateset.UseVisualStyleBackColor = true;
            // 
            // txtPwd
            // 
            this.txtPwd.Location = new System.Drawing.Point(245, 178);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.PasswordChar = '*';
            this.txtPwd.Size = new System.Drawing.Size(407, 21);
            this.txtPwd.TabIndex = 15;
            // 
            // txtLoginName
            // 
            this.txtLoginName.Location = new System.Drawing.Point(246, 106);
            this.txtLoginName.Name = "txtLoginName";
            this.txtLoginName.Size = new System.Drawing.Size(406, 21);
            this.txtLoginName.TabIndex = 15;
            // 
            // BtnBrowse
            // 
            this.BtnBrowse.Location = new System.Drawing.Point(821, 375);
            this.BtnBrowse.Name = "BtnBrowse";
            this.BtnBrowse.Size = new System.Drawing.Size(67, 21);
            this.BtnBrowse.TabIndex = 14;
            this.BtnBrowse.Text = "Browse";
            this.BtnBrowse.UseVisualStyleBackColor = true;
            this.BtnBrowse.Click += new System.EventHandler(this.BtnBrowse_Click);
            // 
            // txtDataLocation
            // 
            this.txtDataLocation.BackColor = System.Drawing.SystemColors.Control;
            this.txtDataLocation.Enabled = false;
            this.txtDataLocation.Location = new System.Drawing.Point(38, 376);
            this.txtDataLocation.Name = "txtDataLocation";
            this.txtDataLocation.Size = new System.Drawing.Size(777, 21);
            this.txtDataLocation.TabIndex = 13;
            // 
            // cmbServer
            // 
            this.cmbServer.Items.AddRange(new object[] {
            "www.coachescrm.com",
            "www.webbeng.net"});
            this.cmbServer.Location = new System.Drawing.Point(246, 33);
            this.cmbServer.Name = "cmbServer";
            this.cmbServer.Size = new System.Drawing.Size(569, 20);
            this.cmbServer.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(38, 346);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(409, 27);
            this.label2.TabIndex = 8;
            this.label2.Text = "Default CCRM Data Saved Location";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(40, 182);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 28);
            this.label4.TabIndex = 8;
            this.label4.Text = "Password:";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(38, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(193, 27);
            this.label3.TabIndex = 8;
            this.label3.Text = "Login Name:";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(38, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(208, 21);
            this.label1.TabIndex = 8;
            this.label1.Text = "Server Address:";
            // 
            // ConfigCoachCRMAccess
            // 
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.GroupDownLoadOnLine);
            this.Name = "ConfigCoachCRMAccess";
            this.Size = new System.Drawing.Size(950, 415);
            this.GroupDownLoadOnLine.ResumeLayout(false);
            this.GroupDownLoadOnLine.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        private void BtnBegin_Click(object sender, EventArgs e)
        {

        }

        private CCRMConfigData CreateDefaultCCRM()
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

                if(index>0)
                {
                    return strText.Substring(0, index);
                 }
            }
            return string.Empty;

        }
       
        //<=

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
            _CCRMConfigData = Webb.Data.CCRMConfigData.LoadConfig(ReportSetting.CCRMServerConfigPath);

            if (_CCRMConfigData == null)
            {
                _CCRMConfigData=CreateDefaultCCRM();
            }

            this.cmbServer.Text = GetServerAddress();

            this.txtLoginName.Text = _CCRMConfigData.LoginName;

            this.txtDataLocation.Text = _CCRMConfigData.DataSavedLocation + "[Player ContactInformation].wrdf"; 
           
            
        }

        public bool UpdateConfig(ref Webb.Reports.DataProvider.DBSourceConfig config)
        {
            bool success = BeginToDownLoadDataFromServer();

            if (!success)
            {
                return false;
            }            

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
                xmlNode.InnerText = @"http://" + strServerIp + @"/CMRServices/CMRService.svc";
            }

            return true;

        }

        
        private bool BeginToDownLoadDataFromServer()
        {
            if (this.cmbServer.Text.Trim()== string.Empty)
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

            string strDirectoryPath = System.IO.Path.GetDirectoryName(this.txtDataLocation.Text);

            if(!System.IO.Directory.Exists(strDirectoryPath))
            {
                System.IO.Directory.CreateDirectory(strDirectoryPath);
            }
            
            DownloadDataProgram = new Process();

            DownloadDataProgram.StartInfo = new ProcessStartInfo(subProgramFile);

            string strLoginName = this.txtLoginName.Text;

            string strPassword = this.txtPwd.Text;

            string selectAllRecord=this.chkDownLoadLateset.Checked?"0":"1";

            string strSavedLocation = this.txtDataLocation.Text;

            string strArguments = string.Format("DownLoadCategoryData \"{0}\" \"{1}\" \"{2}\" \"{3}\"", strLoginName,strPassword,selectAllRecord,strSavedLocation);

            DownloadDataProgram.StartInfo.Arguments = strArguments;

            DownloadDataProgram.Start();

            DownloadDataProgram.WaitForExit();

            if (DownloadDataProgram.ExitCode != 0) return false;

            if (_CCRMConfigData == null) _CCRMConfigData = new CCRMConfigData();

            _CCRMConfigData.ServerAddress = this.cmbServer.Text;
          
            if (!strDirectoryPath.EndsWith(@"\")) strDirectoryPath = strDirectoryPath + @"\";

            _CCRMConfigData.DataSavedLocation = strDirectoryPath;

            _CCRMConfigData.LoginName = this.txtLoginName.Text;

            Webb.Data.CCRMConfigData.SaveConfigFiles(_CCRMConfigData, ReportSetting.CCRMServerConfigPath);
            
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
