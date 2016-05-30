using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Webb.Reports;
using Webb.Reports.DataProvider;
using Webb.Collections;
using System.Collections.Specialized;
using Microsoft.Win32;
using Webb.Reports.ReportWizard.WizardInfo;

namespace Webb.Reports.ReportWizard.DataSourceProvider
{
    public class ConfigVictoryAccess : UserControl, IDataSourceConfigControl
    {
        private Panel palDatabase;
        private TextBox C_SelectedFile;
        private Label C_TitleMsg;
        private Button C_Browse;
        private GroupBox Group_Edls;
        public CheckedListBox C_Edls;
        private GroupBox Group_Games;
        public CheckedListBox C_Games;
        private Panel palGamesAndEdls;

        private GameInfoCollection _GameInfoCollection = new GameInfoCollection();
        private DataTable _AllGameTable = null;

        private DBSourceConfig _DbSourceConfig;
    
    
        public ConfigVictoryAccess()
        {
            InitializeComponent();

            _DbSourceConfig = new DBSourceConfig();
        }

        private void InitializeComponent()
        {
            this.palDatabase = new System.Windows.Forms.Panel();
            this.C_SelectedFile = new System.Windows.Forms.TextBox();
            this.C_TitleMsg = new System.Windows.Forms.Label();
            this.C_Browse = new System.Windows.Forms.Button();
            this.palGamesAndEdls = new System.Windows.Forms.Panel();
            this.Group_Edls = new System.Windows.Forms.GroupBox();
            this.C_Edls = new System.Windows.Forms.CheckedListBox();
            this.Group_Games = new System.Windows.Forms.GroupBox();
            this.C_Games = new System.Windows.Forms.CheckedListBox();
            this.palDatabase.SuspendLayout();
            this.palGamesAndEdls.SuspendLayout();
            this.Group_Edls.SuspendLayout();
            this.Group_Games.SuspendLayout();
            this.SuspendLayout();
            // 
            // palDatabase
            // 
            this.palDatabase.BackColor = System.Drawing.Color.Transparent;
            this.palDatabase.Controls.Add(this.C_SelectedFile);
            this.palDatabase.Controls.Add(this.C_TitleMsg);
            this.palDatabase.Controls.Add(this.C_Browse);
            this.palDatabase.Dock = System.Windows.Forms.DockStyle.Top;
            this.palDatabase.Location = new System.Drawing.Point(0, 0);
            this.palDatabase.Name = "palDatabase";
            this.palDatabase.Size = new System.Drawing.Size(950, 71);
            this.palDatabase.TabIndex = 11;
            // 
            // C_SelectedFile
            // 
            this.C_SelectedFile.BackColor = System.Drawing.SystemColors.Window;
            this.C_SelectedFile.Location = new System.Drawing.Point(3, 40);
            this.C_SelectedFile.Name = "C_SelectedFile";
            this.C_SelectedFile.ReadOnly = true;
            this.C_SelectedFile.Size = new System.Drawing.Size(725, 22);
            this.C_SelectedFile.TabIndex = 11;
            // 
            // C_TitleMsg
            // 
            this.C_TitleMsg.Location = new System.Drawing.Point(3, 14);
            this.C_TitleMsg.Name = "C_TitleMsg";
            this.C_TitleMsg.Size = new System.Drawing.Size(520, 23);
            this.C_TitleMsg.TabIndex = 13;
            this.C_TitleMsg.Text = "Please select a victory database to open.";
            // 
            // C_Browse
            // 
            this.C_Browse.Location = new System.Drawing.Point(734, 42);
            this.C_Browse.Name = "C_Browse";
            this.C_Browse.Size = new System.Drawing.Size(75, 23);
            this.C_Browse.TabIndex = 12;
            this.C_Browse.Text = "Browse...";
            this.C_Browse.Click += new System.EventHandler(this.C_Browse_Click);
            // 
            // palGamesAndEdls
            // 
            this.palGamesAndEdls.Controls.Add(this.Group_Edls);
            this.palGamesAndEdls.Controls.Add(this.Group_Games);
            this.palGamesAndEdls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.palGamesAndEdls.Location = new System.Drawing.Point(0, 71);
            this.palGamesAndEdls.Name = "palGamesAndEdls";
            this.palGamesAndEdls.Size = new System.Drawing.Size(950, 342);
            this.palGamesAndEdls.TabIndex = 12;
            // 
            // Group_Edls
            // 
            this.Group_Edls.Controls.Add(this.C_Edls);
            this.Group_Edls.Dock = System.Windows.Forms.DockStyle.Right;
            this.Group_Edls.Location = new System.Drawing.Point(526, 0);
            this.Group_Edls.Name = "Group_Edls";
            this.Group_Edls.Size = new System.Drawing.Size(424, 342);
            this.Group_Edls.TabIndex = 7;
            this.Group_Edls.TabStop = false;
            this.Group_Edls.Text = "Select Cutups for Report:";
            // 
            // C_Edls
            // 
            this.C_Edls.CheckOnClick = true;
            this.C_Edls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.C_Edls.Location = new System.Drawing.Point(3, 18);
            this.C_Edls.Name = "C_Edls";
            this.C_Edls.Size = new System.Drawing.Size(418, 310);
            this.C_Edls.TabIndex = 2;
            // 
            // Group_Games
            // 
            this.Group_Games.Controls.Add(this.C_Games);
            this.Group_Games.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Group_Games.Location = new System.Drawing.Point(0, 0);
            this.Group_Games.Name = "Group_Games";
            this.Group_Games.Size = new System.Drawing.Size(950, 342);
            this.Group_Games.TabIndex = 6;
            this.Group_Games.TabStop = false;
            this.Group_Games.Text = "Select Games for Report:";
            // 
            // C_Games
            // 
            this.C_Games.CheckOnClick = true;
            this.C_Games.Dock = System.Windows.Forms.DockStyle.Fill;
            this.C_Games.Location = new System.Drawing.Point(3, 18);
            this.C_Games.Name = "C_Games";
            this.C_Games.Size = new System.Drawing.Size(944, 310);
            this.C_Games.TabIndex = 2;
            // 
            // ConfigVictoryAccess
            // 
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.palGamesAndEdls);
            this.Controls.Add(this.palDatabase);
            this.Font = new System.Drawing.Font("Verdana", 9F);
            this.Name = "ConfigVictoryAccess";
            this.Size = new System.Drawing.Size(950, 413);
            this.palDatabase.ResumeLayout(false);
            this.palDatabase.PerformLayout();
            this.palGamesAndEdls.ResumeLayout(false);
            this.Group_Edls.ResumeLayout(false);
            this.Group_Games.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #region IDataSourceConfigControl Members

        public UserControl UserControl
        {
            get { return this; }
        }

        public void SetConfig(Webb.Reports.DataProvider.DBSourceConfig config)
        {
            _DbSourceConfig.ApplyConfig(config);

            string strDatabasePath = _DbSourceConfig.DBFilePath;

            if (strDatabasePath==null||strDatabasePath == string.Empty)
            {
                VictoryPathManager victoryPathManager = new VictoryPathManager(config.WebbDBType);
                
                strDatabasePath=victoryPathManager.GetVictoryDatabasePath();
            }

   
            this.C_SelectedFile.Text = strDatabasePath;          

            this.SetGamesAndEdlsView();
             
        }

        public bool UpdateConfig(ref Webb.Reports.DataProvider.DBSourceConfig config)
        {
            if(!this.ValidateSetting())return false;

            _DbSourceConfig.DBFilePath = this.C_SelectedFile.Text;

            _DbSourceConfig.DBConnType = Webb.Data.DBConnTypes.OleDB;

            _DbSourceConfig.GameIDs = this.GetSelectedGameIDs();

            _DbSourceConfig.Edls = this.GetSelectedEdlIDs();

            config.ApplyConfig(this._DbSourceConfig);

            return true;
        }

        #endregion

        private void C_Browse_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            fileDialog.Filter = "Victory Datatbase(*.mdb)|*.mdb";

            if (fileDialog.ShowDialog()== DialogResult.OK)
            {
                this.C_SelectedFile.Text = fileDialog.FileName;

                this.SetGamesAndEdlsView();
            }

        }

        #region Sub Functions



        public void SetGames(DataTable i_Table)
        {
            //Scott@2007-11-14 13:12 modified some of the following code.
            this._AllGameTable = i_Table;
            this.C_Games.Items.Clear();
           
            if (i_Table is DBWebbVictory.GameDataTable)
            {
                this.SetGames_Victory(i_Table as DBWebbVictory.GameDataTable);

                return;
            }

        }

        #region Old
        //public void SetEdls(DataTable i_Table)
        //{
        //    this.C_Edls.Items.Clear();

        //    if (i_Table == null || i_Table.Columns.Count != 2)
        //    {
        //        this.Group_Edls.Visible = false;
        //        return;
        //    }
        //    this.Group_Edls.Visible = true;
        //    foreach (DataRow m_Row in i_Table.Rows)
        //    {
        //        string m_Value = string.Format("{0}   (CutupName:  '{1}')",
        //            m_Row[0],
        //            m_Row[1]);
        //        this.C_Edls.Items.Add(m_Value);
        //    }


        //}       
        //private void SetGames_Victory(DBWebbVictory.GameDataTable i_Table)
        //{
        //    foreach (DataRow m_Row in i_Table.Rows)
        //    {
        //        string m_Value = string.Format("{0}   (GameName:  '{1}')",
        //            m_Row[i_Table.GameIDColumn.Caption],
        //            m_Row[i_Table.GameNameColumn.Caption]
        //            ,m_Row[i_Table.DateColumn.Caption]
        //            );
        //        this.C_Games.Items.Add(m_Value);
        //    }
        //}
        #endregion

        #region New
        public void SetEdls(DataTable i_Table)
        {
            this.C_Edls.Items.Clear();

            if (i_Table == null || i_Table.Columns.Count != 2)
            {
                this.Group_Edls.Visible = false;
                return;
            }
            this.Group_Edls.Visible = true;

            foreach (DataRow m_Row in i_Table.Rows)
            {
                int key=0;

                bool convert=int.TryParse(m_Row[0].ToString(),out key);

                if(!convert)continue;

                IndexerDescription indexerDescription = new IndexerDescription("Cutup",key, m_Row[1].ToString());
                
                this.C_Edls.Items.Add(indexerDescription);
            }


        }
        private void SetGames_Victory(DBWebbVictory.GameDataTable i_Table)
        {
            foreach (DataRow m_Row in i_Table.Rows)
            {
                int key = 0;

                bool convert = int.TryParse(m_Row[0].ToString(), out key);

                if (!convert) continue;

                IndexerDescription indexerDescription = new IndexerDescription("Game",key, m_Row[1].ToString());

                this.C_Games.Items.Add(indexerDescription);
            }
        }
        #endregion

        private Webb.Data.CCRMConfigData CreateDefaultCCRM()
        {
            string strPath = Webb.Utility.ApplicationDirectory;

            strPath = strPath + @"Template\DataConfig\CCRMDataFiles\";

            if (!System.IO.Directory.Exists(strPath))
            {
                System.IO.Directory.CreateDirectory(strPath);
            }

            return Webb.Data.CCRMConfigData.CreateInitialData(strPath);

        }
        public bool ValidateSetting()
        {
            if (this.C_Games.CheckedItems.Count > 0 || this.C_Edls.CheckedItems.Count > 0)
            {
              
                return true;
            }
            else
            {
                Webb.Utilities.MessageBoxEx.ShowWarning("You must at least select 1 game or edl.");

                return false;
            }
        }

        //Scott@2007-11-15 13:39 modified some of the following code.
        public void ResetControl()
        {
            //base.ResetControl ();
            if (this._AllGameTable != null)
                this._AllGameTable.Clear();
            if (this._GameInfoCollection != null)
                this._GameInfoCollection.Clear();
        }

        public Int32Collection GetSelectedGameIDs()
        {
            Int32Collection m_GameIDs = new Int32Collection();

            foreach (object m_SelectedObj in this.C_Games.CheckedItems)
            {
                IndexerDescription IndexerDescription = m_SelectedObj as IndexerDescription;              
               
                m_GameIDs.Add(IndexerDescription.KeyId);
               
            }
            return m_GameIDs;
        }
        public StringCollection GetSelectedEdlIDs()  //2009-6-22 14:08:33@Simon Add this Code
        {
            StringCollection m_EdlIDs = new StringCollection();

            foreach (object m_SelectedObj in this.C_Edls.CheckedItems)
            {
                IndexerDescription IndexerDescription = m_SelectedObj as IndexerDescription;              

                m_EdlIDs.Add(IndexerDescription.KeyId.ToString());
            }
            return m_EdlIDs;
        }


        public void SetGamesAndEdlsView()
        {
            WebbDataProvider provider = new WebbDataProvider();

            if (!System.IO.File.Exists(this.C_SelectedFile.Text)) return;

            this._DbSourceConfig.DBFilePath = this.C_SelectedFile.Text;

            provider.InitializeDBManager(this._DbSourceConfig);

            DataTable m_Games = provider.LoadWebbGames(); 
           
            this.SetGames(m_Games);

            DataTable m_Edls = provider.LoadWebbEdls();

            this.SetEdls(m_Edls);
        }
        #endregion
    }
}
