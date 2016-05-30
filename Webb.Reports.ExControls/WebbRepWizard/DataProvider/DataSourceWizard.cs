using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Webb.Data;
using Webb.Utilities.Wizards;
using Webb.Reports.DataProvider;

namespace Webb.Reports.ReportWizard.DataSourceProvider
{
	/// <summary>
	/// Summary description for DataSourceWizard.
	/// </summary>
	public class DataSourceWizard: System.Windows.Forms.Form
	{	
		private DBSourceConfig _DBSourceConfig;	

		private System.Windows.Forms.Panel C_BottomPanel;
		private System.Windows.Forms.Panel C_MainPanel;
		private System.Windows.Forms.Button C_Cancel;
		private System.Windows.Forms.Button C_OK;
		private System.Windows.Forms.Label C_TitleLabel;
		private System.Windows.Forms.Label C_SperatorLabel;
		protected System.Windows.Forms.Button C_SelectAll;
		protected System.Windows.Forms.Button C_ClearAll;

        private IDataSourceConfigControl _CurrentConfig = null;
		
		public DataSourceWizard()
		{
			InitializeComponent();
			_DBSourceConfig=new DBSourceConfig();        
			
		}

        public DBSourceConfig DBSourceConfig
        {
            get
            {
                return this._DBSourceConfig;
            }
        }

        public bool CanFinished
        {
            set{
                  if (value)
                  {
                      this.C_OK.Enabled = true;
                  }
                  else
                  {
                      this.C_OK.Enabled = false;
                  }
               }

        }
		public void InitControl()
		{
            ConfigDataSourceFile Step_ConfigAdvFile= new ConfigDataSourceFile();

			Step_ConfigAdvFile.SetConfig(this._DBSourceConfig);

			this.LoadControl(Step_ConfigAdvFile as IDataSourceConfigControl);

            this.Text = "Choose user folder and games ";		

		}

        public void InitCoachCRMData()
        { 
            ConfigCoachCRMAccess coachCRMAccess = new ConfigCoachCRMAccess();

            coachCRMAccess.SetConfig(this._DBSourceConfig);

            this.LoadControl(coachCRMAccess);

            this.Text = "Choose data";

        }
        public void InitVictoryData()
        {
            ConfigVictoryAccess victoryAccess = new ConfigVictoryAccess();

            victoryAccess.SetConfig(this._DBSourceConfig);

            this.LoadControl(victoryAccess);

            this.Text = "Choose Database And Games";

        }


        public void SetDataConfig(DBSourceConfig config)
        {
            if(this._DBSourceConfig==null)_DBSourceConfig=new DBSourceConfig();

            this._DBSourceConfig.ApplyConfig(config);

            switch (config.WebbDBType)
            {
                case WebbDBTypes.CoachCRM:
                    _DBSourceConfig.DBConnType = DBConnTypes.XMLFile;
                    this.InitCoachCRMData();
                    break;
                case WebbDBTypes.WebbVictoryFootball:
                case WebbDBTypes.WebbVictoryBasketball:
                case WebbDBTypes.WebbVictoryHockey:
                case WebbDBTypes.WebbVictoryVolleyball:
                case WebbDBTypes.WebbVictoryLacrosse:
                case WebbDBTypes.WebbVictorySoccer:
                    _DBSourceConfig.DBConnType = DBConnTypes.OleDB;
                    this.InitVictoryData();
                    break;
                case WebbDBTypes.WebbAdvantageFootball:
                default:
                    _DBSourceConfig.DBConnType = DBConnTypes.File;
                    this.InitControl();
                    break;

            }
        }

        public void LoadControl(IDataSourceConfigControl i_StepConfig)
		{
			// TODO: implement
			this.C_MainPanel.Controls.Clear();

            i_StepConfig.UserControl.Dock = DockStyle.Fill;

            i_StepConfig.UserControl.Parent = this;

            this._CurrentConfig = i_StepConfig;

            i_StepConfig.UserControl.Tag = this;

            this.C_MainPanel.Controls.Add(i_StepConfig.UserControl);			
		}

		public  void OnOK()
		{
            if(!_CurrentConfig.UpdateConfig(ref this._DBSourceConfig))return;				

			this.DialogResult = DialogResult.OK;
			
		}
		public void OnCancel()
		{
			this.DialogResult = DialogResult.Cancel;
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.C_TitleLabel = new System.Windows.Forms.Label();
            this.C_BottomPanel = new System.Windows.Forms.Panel();
            this.C_ClearAll = new System.Windows.Forms.Button();
            this.C_SelectAll = new System.Windows.Forms.Button();
            this.C_SperatorLabel = new System.Windows.Forms.Label();
            this.C_Cancel = new System.Windows.Forms.Button();
            this.C_OK = new System.Windows.Forms.Button();
            this.C_MainPanel = new System.Windows.Forms.Panel();
            this.C_BottomPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // C_TitleLabel
            // 
            this.C_TitleLabel.BackColor = System.Drawing.Color.Transparent;
            this.C_TitleLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.C_TitleLabel.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.C_TitleLabel.Location = new System.Drawing.Point(0, 0);
            this.C_TitleLabel.Name = "C_TitleLabel";
            this.C_TitleLabel.Size = new System.Drawing.Size(968, 32);
            this.C_TitleLabel.TabIndex = 0;
            this.C_TitleLabel.Text = " Webb Wizard Form Step 1";
            this.C_TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // C_BottomPanel
            // 
            this.C_BottomPanel.BackColor = System.Drawing.Color.Transparent;
            this.C_BottomPanel.Controls.Add(this.C_ClearAll);
            this.C_BottomPanel.Controls.Add(this.C_SelectAll);
            this.C_BottomPanel.Controls.Add(this.C_SperatorLabel);
            this.C_BottomPanel.Controls.Add(this.C_Cancel);
            this.C_BottomPanel.Controls.Add(this.C_OK);
            this.C_BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.C_BottomPanel.Location = new System.Drawing.Point(0, 462);
            this.C_BottomPanel.Name = "C_BottomPanel";
            this.C_BottomPanel.Size = new System.Drawing.Size(968, 48);
            this.C_BottomPanel.TabIndex = 1;
            // 
            // C_ClearAll
            // 
            this.C_ClearAll.Location = new System.Drawing.Point(99, 15);
            this.C_ClearAll.Name = "C_ClearAll";
            this.C_ClearAll.Size = new System.Drawing.Size(75, 23);
            this.C_ClearAll.TabIndex = 3;
            this.C_ClearAll.Text = "Clear All";
            this.C_ClearAll.Visible = false;
            // 
            // C_SelectAll
            // 
            this.C_SelectAll.Location = new System.Drawing.Point(19, 15);
            this.C_SelectAll.Name = "C_SelectAll";
            this.C_SelectAll.Size = new System.Drawing.Size(75, 23);
            this.C_SelectAll.TabIndex = 2;
            this.C_SelectAll.Tag = "v";
            this.C_SelectAll.Text = "Select All";
            this.C_SelectAll.Visible = false;
            // 
            // C_SperatorLabel
            // 
            this.C_SperatorLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.C_SperatorLabel.Location = new System.Drawing.Point(8, 0);
            this.C_SperatorLabel.Name = "C_SperatorLabel";
            this.C_SperatorLabel.Size = new System.Drawing.Size(936, 3);
            this.C_SperatorLabel.TabIndex = 1;
            // 
            // C_Cancel
            // 
            this.C_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.C_Cancel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.C_Cancel.Location = new System.Drawing.Point(864, 16);
            this.C_Cancel.Name = "C_Cancel";
            this.C_Cancel.Size = new System.Drawing.Size(75, 23);
            this.C_Cancel.TabIndex = 0;
            this.C_Cancel.Text = "Cancel";
            this.C_Cancel.Click += new System.EventHandler(this.C_Cancel_Click);
            // 
            // C_OK
            // 
            this.C_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.C_OK.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.C_OK.Location = new System.Drawing.Point(752, 16);
            this.C_OK.Name = "C_OK";
            this.C_OK.Size = new System.Drawing.Size(75, 23);
            this.C_OK.TabIndex = 0;
            this.C_OK.Text = "OK";
            this.C_OK.Click += new System.EventHandler(this.C_OK_Click);
            // 
            // C_MainPanel
            // 
            this.C_MainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.C_MainPanel.BackColor = System.Drawing.Color.Transparent;
            this.C_MainPanel.Location = new System.Drawing.Point(8, 40);
            this.C_MainPanel.Name = "C_MainPanel";
            this.C_MainPanel.Size = new System.Drawing.Size(950, 415);
            this.C_MainPanel.TabIndex = 2;
            this.C_MainPanel.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.C_MainPanel_HelpRequested);
            // 
            // DataSourceWizard
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(7, 15);
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BackgroundImage = global::Webb.Reports.ExControls.Properties.Resources.l2;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(968, 510);
            this.Controls.Add(this.C_MainPanel);
            this.Controls.Add(this.C_BottomPanel);
            this.Controls.Add(this.C_TitleLabel);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DataSourceWizard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Webb Wizard Form";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DataSourceWizard_FormClosing);
            this.C_BottomPanel.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion
		#endregion
	
     

		private void C_OK_Click(object sender, System.EventArgs e)
		{
			this.OnOK();
		}
		private void C_Cancel_Click(object sender, System.EventArgs e)
		{
			this.OnCancel();
		}

        private void C_MainPanel_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            System.Windows.Forms.Help.ShowHelp(this, Webb.Reports.ReportWizard.WizardInfo.ReportSetting.HelpFile, "/html/SelectGames.html");
            hlpevent.Handled = true;
        }

        private void DataSourceWizard_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_CurrentConfig is ConfigCoachCRMAccess)
            {
                System.Diagnostics.Process DownloadProgram = (_CurrentConfig as ConfigCoachCRMAccess).DownloadListProgram;

                if (DownloadProgram != null && !DownloadProgram.HasExited)
                {
                    DownloadProgram.Kill();

                    DownloadProgram.Dispose();

                    DownloadProgram = null;
                }

                DownloadProgram = (_CurrentConfig as ConfigCoachCRMAccess).DownloadDataProgram;

                if (DownloadProgram != null && !DownloadProgram.HasExited)
                {
                    DownloadProgram.Kill();

                    DownloadProgram.Dispose();

                    DownloadProgram = null;
                }

            }
        }


	}

    public interface IDataSourceConfigControl
    {
        UserControl UserControl { get; }
        void SetConfig(DBSourceConfig config);
        bool UpdateConfig(ref DBSourceConfig config);
       
    }
}

