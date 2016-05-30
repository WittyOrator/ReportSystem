using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Collections.Specialized;
using Microsoft.Win32;
using Webb.Utilities;

namespace Webb.Reports.DataProvider.UI
{
	/// <summary>
	/// Summary description for ConfigAdvFile.
	/// </summary>
	public class ConfigAdvFile : Webb.Utilities.Wizards.WinzardControlBase
	{
		public bool bGame = true;
		private System.Windows.Forms.Button C_BtnOpenFolder;
		private System.Windows.Forms.TextBox C_TextFolder;
		private System.Windows.Forms.FolderBrowserDialog C_FolderBrowserDlg;
		//private StringCollection _Games;
		//private StringCollection _Edls;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.TabPage C_TabGame;
		private System.Windows.Forms.TabPage C_TabEdl;
		private System.Windows.Forms.ListBox C_ListGamesInFolder;
		private System.Windows.Forms.Button C_BtnDelete;
		private System.Windows.Forms.Button C_BtnAdd;
		private System.Windows.Forms.ListBox C_ListSelectedGames;
		private System.Windows.Forms.TabControl C_TabGameEdl;
		private System.Windows.Forms.ListBox C_ListEdlsInFolder;
		private System.Windows.Forms.ListBox C_ListSelectedEdls;
		private System.Windows.Forms.Button C_BtnDelEdl;
		private System.Windows.Forms.Button C_BtnAddEdl;
	
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		static readonly string RegKeyPath = "SOFTWARE\\Webb Electronics\\WebbReport\\RecentAdvantageUserFolder";		
		
		static readonly string KeyName = "LastAdvantageUserPath";

		static readonly string RegSharekeyPath="Software\\WEBB ELECTRONICS, INC.\\W4W32\\SYSTEM";		
		
		static readonly string ShareKeyName = "ServerFilePath";


		private System.Windows.Forms.GroupBox groupLast;

		private string _ShareUserFolder=string.Empty;

		private string _LocalUserFolder=string.Empty;

		private RegistryKey _RegKey;
		private System.Windows.Forms.CheckBox chkLocal;
		private System.Windows.Forms.CheckBox chkShare;
		private System.Windows.Forms.Label lblShare;
	
		private RegistryKey RegKey
		{
			get
			{
				if(this._RegKey==null)
				{
                    this._RegKey = Registry.CurrentUser.OpenSubKey(RegKeyPath, true);
					if(this._RegKey==null)
					{
                        this._RegKey = Registry.CurrentUser.CreateSubKey(RegKeyPath);
					}
				}
				return this._RegKey;
			}
		}
		private RegistryKey _RegShareKey;
		private RegistryKey RegShareKey
		{
			get
			{
				if(this._RegShareKey==null)
				{
					this._RegShareKey = Registry.CurrentUser.OpenSubKey(RegSharekeyPath,true);
					if(this._RegShareKey==null)
					{
						this._RegShareKey = Registry.CurrentUser.CreateSubKey(RegSharekeyPath);
					}
				}
				return this._RegShareKey;
			}
		}


		public string Folder
		{
			get
			{
				if(this.chkLocal.Checked)
				{
					if(this.chkShare.Checked&&this._ShareUserFolder!=string.Empty)
					{
						return this._LocalUserFolder+";"+this._ShareUserFolder;
					}
					else
					{
						return this._LocalUserFolder;
					}

				}
				else
				{
					if(this.chkShare.Checked)
					{
						return this._ShareUserFolder;
					}
					else
					{
						return string.Empty;
					}

				}				
			}
		}

		public ConfigAdvFile()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();			


			this.WizardTitle = "Step 2: Choose user folder";

			this.FinishControl = true;
			this.LastControl = true;
			this.SelectStep = true;

			this._LocalUserFolder=this.LoadLastUserFolderFromReg();

			string folderPath=this._LocalUserFolder;

			folderPath=folderPath+"\\scouting.tmn";

			if(!System.IO.File.Exists(folderPath))
			{
                _LocalUserFolder=string.Empty; 
			}

			this._ShareUserFolder=this.LoadLShareFolderFromReg();

			
			//this.ClearAllList();
			// TODO: Add any initialization after the InitializeComponent call
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if(this._RegKey!=null) this._RegKey.Close();

			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.C_BtnOpenFolder = new System.Windows.Forms.Button();
            this.C_TextFolder = new System.Windows.Forms.TextBox();
            this.C_FolderBrowserDlg = new System.Windows.Forms.FolderBrowserDialog();
            this.C_TabGameEdl = new System.Windows.Forms.TabControl();
            this.C_TabGame = new System.Windows.Forms.TabPage();
            this.C_ListGamesInFolder = new System.Windows.Forms.ListBox();
            this.C_BtnDelete = new System.Windows.Forms.Button();
            this.C_BtnAdd = new System.Windows.Forms.Button();
            this.C_ListSelectedGames = new System.Windows.Forms.ListBox();
            this.C_TabEdl = new System.Windows.Forms.TabPage();
            this.C_ListEdlsInFolder = new System.Windows.Forms.ListBox();
            this.C_BtnDelEdl = new System.Windows.Forms.Button();
            this.C_BtnAddEdl = new System.Windows.Forms.Button();
            this.C_ListSelectedEdls = new System.Windows.Forms.ListBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.groupLast = new System.Windows.Forms.GroupBox();
            this.lblShare = new System.Windows.Forms.Label();
            this.chkShare = new System.Windows.Forms.CheckBox();
            this.chkLocal = new System.Windows.Forms.CheckBox();
            this.C_TabGameEdl.SuspendLayout();
            this.C_TabGame.SuspendLayout();
            this.C_TabEdl.SuspendLayout();
            this.groupLast.SuspendLayout();
            this.SuspendLayout();
            // 
            // C_BtnOpenFolder
            // 
            this.C_BtnOpenFolder.Location = new System.Drawing.Point(512, 56);
            this.C_BtnOpenFolder.Name = "C_BtnOpenFolder";
            this.C_BtnOpenFolder.Size = new System.Drawing.Size(80, 23);
            this.C_BtnOpenFolder.TabIndex = 0;
            this.C_BtnOpenFolder.Text = "Browse...";
            this.C_BtnOpenFolder.Click += new System.EventHandler(this.C_BtnOpenFolder_Click);
            // 
            // C_TextFolder
            // 
            this.C_TextFolder.Enabled = false;
            this.C_TextFolder.Location = new System.Drawing.Point(8, 56);
            this.C_TextFolder.Name = "C_TextFolder";
            this.C_TextFolder.Size = new System.Drawing.Size(480, 22);
            this.C_TextFolder.TabIndex = 6;
            // 
            // C_FolderBrowserDlg
            // 
            this.C_FolderBrowserDlg.ShowNewFolderButton = false;
            // 
            // C_TabGameEdl
            // 
            this.C_TabGameEdl.Controls.Add(this.C_TabGame);
            this.C_TabGameEdl.Controls.Add(this.C_TabEdl);
            this.C_TabGameEdl.Location = new System.Drawing.Point(0, 85);
            this.C_TabGameEdl.Name = "C_TabGameEdl";
            this.C_TabGameEdl.SelectedIndex = 0;
            this.C_TabGameEdl.Size = new System.Drawing.Size(784, 392);
            this.C_TabGameEdl.TabIndex = 7;
            this.C_TabGameEdl.SelectedIndexChanged += new System.EventHandler(this.C_TabGameEdl_SelectedIndexChanged);
            // 
            // C_TabGame
            // 
            this.C_TabGame.Controls.Add(this.C_ListGamesInFolder);
            this.C_TabGame.Controls.Add(this.C_BtnDelete);
            this.C_TabGame.Controls.Add(this.C_BtnAdd);
            this.C_TabGame.Controls.Add(this.C_ListSelectedGames);
            this.C_TabGame.Location = new System.Drawing.Point(4, 23);
            this.C_TabGame.Name = "C_TabGame";
            this.C_TabGame.Size = new System.Drawing.Size(776, 365);
            this.C_TabGame.TabIndex = 0;
            this.C_TabGame.Text = "Games";
            this.C_TabGame.UseVisualStyleBackColor = true;
            // 
            // C_ListGamesInFolder
            // 
            this.C_ListGamesInFolder.HorizontalScrollbar = true;
            this.C_ListGamesInFolder.ItemHeight = 14;
            this.C_ListGamesInFolder.Location = new System.Drawing.Point(8, 8);
            this.C_ListGamesInFolder.Name = "C_ListGamesInFolder";
            this.C_ListGamesInFolder.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.C_ListGamesInFolder.Size = new System.Drawing.Size(356, 354);
            this.C_ListGamesInFolder.Sorted = true;
            this.C_ListGamesInFolder.TabIndex = 9;
            this.C_ListGamesInFolder.DoubleClick += new System.EventHandler(this.C_ListGamesInFolder_DoubleClick);
            // 
            // C_BtnDelete
            // 
            this.C_BtnDelete.Location = new System.Drawing.Point(369, 168);
            this.C_BtnDelete.Name = "C_BtnDelete";
            this.C_BtnDelete.Size = new System.Drawing.Size(34, 23);
            this.C_BtnDelete.TabIndex = 8;
            this.C_BtnDelete.Text = "<=";
            this.C_BtnDelete.Click += new System.EventHandler(this.C_BtnDelete_Click);
            // 
            // C_BtnAdd
            // 
            this.C_BtnAdd.Location = new System.Drawing.Point(369, 120);
            this.C_BtnAdd.Name = "C_BtnAdd";
            this.C_BtnAdd.Size = new System.Drawing.Size(34, 23);
            this.C_BtnAdd.TabIndex = 7;
            this.C_BtnAdd.Text = "=>";
            this.C_BtnAdd.Click += new System.EventHandler(this.C_BtnAdd_Click);
            // 
            // C_ListSelectedGames
            // 
            this.C_ListSelectedGames.HorizontalScrollbar = true;
            this.C_ListSelectedGames.ItemHeight = 14;
            this.C_ListSelectedGames.Location = new System.Drawing.Point(414, 8);
            this.C_ListSelectedGames.Name = "C_ListSelectedGames";
            this.C_ListSelectedGames.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.C_ListSelectedGames.Size = new System.Drawing.Size(353, 354);
            this.C_ListSelectedGames.TabIndex = 6;
            this.C_ListSelectedGames.DoubleClick += new System.EventHandler(this.C_ListSelectedGames_DoubleClick);
            // 
            // C_TabEdl
            // 
            this.C_TabEdl.Controls.Add(this.C_ListEdlsInFolder);
            this.C_TabEdl.Controls.Add(this.C_BtnDelEdl);
            this.C_TabEdl.Controls.Add(this.C_BtnAddEdl);
            this.C_TabEdl.Controls.Add(this.C_ListSelectedEdls);
            this.C_TabEdl.Location = new System.Drawing.Point(4, 23);
            this.C_TabEdl.Name = "C_TabEdl";
            this.C_TabEdl.Size = new System.Drawing.Size(776, 365);
            this.C_TabEdl.TabIndex = 1;
            this.C_TabEdl.Text = "Cutups";
            this.C_TabEdl.UseVisualStyleBackColor = true;
            // 
            // C_ListEdlsInFolder
            // 
            this.C_ListEdlsInFolder.HorizontalScrollbar = true;
            this.C_ListEdlsInFolder.ItemHeight = 14;
            this.C_ListEdlsInFolder.Location = new System.Drawing.Point(8, 8);
            this.C_ListEdlsInFolder.Name = "C_ListEdlsInFolder";
            this.C_ListEdlsInFolder.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.C_ListEdlsInFolder.Size = new System.Drawing.Size(352, 326);
            this.C_ListEdlsInFolder.TabIndex = 13;
            this.C_ListEdlsInFolder.DoubleClick += new System.EventHandler(this.C_ListEdlsInFolder_DoubleClick);
            // 
            // C_BtnDelEdl
            // 
            this.C_BtnDelEdl.Location = new System.Drawing.Point(368, 120);
            this.C_BtnDelEdl.Name = "C_BtnDelEdl";
            this.C_BtnDelEdl.Size = new System.Drawing.Size(40, 23);
            this.C_BtnDelEdl.TabIndex = 12;
            this.C_BtnDelEdl.Text = "<=";
            this.C_BtnDelEdl.Click += new System.EventHandler(this.C_BtnDelEdl_Click);
            // 
            // C_BtnAddEdl
            // 
            this.C_BtnAddEdl.Location = new System.Drawing.Point(368, 72);
            this.C_BtnAddEdl.Name = "C_BtnAddEdl";
            this.C_BtnAddEdl.Size = new System.Drawing.Size(40, 23);
            this.C_BtnAddEdl.TabIndex = 11;
            this.C_BtnAddEdl.Text = "=>";
            this.C_BtnAddEdl.Click += new System.EventHandler(this.C_BtnAddEdl_Click);
            // 
            // C_ListSelectedEdls
            // 
            this.C_ListSelectedEdls.HorizontalScrollbar = true;
            this.C_ListSelectedEdls.ItemHeight = 14;
            this.C_ListSelectedEdls.Location = new System.Drawing.Point(416, 8);
            this.C_ListSelectedEdls.Name = "C_ListSelectedEdls";
            this.C_ListSelectedEdls.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.C_ListSelectedEdls.Size = new System.Drawing.Size(344, 326);
            this.C_ListSelectedEdls.TabIndex = 10;
            this.C_ListSelectedEdls.DoubleClick += new System.EventHandler(this.C_ListSelectedEdls_DoubleClick);
            // 
            // groupLast
            // 
            this.groupLast.Controls.Add(this.lblShare);
            this.groupLast.Controls.Add(this.chkShare);
            this.groupLast.Controls.Add(this.chkLocal);
            this.groupLast.Location = new System.Drawing.Point(8, 0);
            this.groupLast.Name = "groupLast";
            this.groupLast.Size = new System.Drawing.Size(776, 48);
            this.groupLast.TabIndex = 8;
            this.groupLast.TabStop = false;
            // 
            // lblShare
            // 
            this.lblShare.Location = new System.Drawing.Point(400, 18);
            this.lblShare.Name = "lblShare";
            this.lblShare.Size = new System.Drawing.Size(360, 24);
            this.lblShare.TabIndex = 4;
            // 
            // chkShare
            // 
            this.chkShare.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkShare.Location = new System.Drawing.Point(240, 16);
            this.chkShare.Name = "chkShare";
            this.chkShare.Size = new System.Drawing.Size(136, 24);
            this.chkShare.TabIndex = 3;
            this.chkShare.Text = "Shared Games";
            this.chkShare.Click += new System.EventHandler(this.chk_Click);
            this.chkShare.MouseDown += new System.Windows.Forms.MouseEventHandler(this.chkShare_MouseDown);
            // 
            // chkLocal
            // 
            this.chkLocal.Checked = true;
            this.chkLocal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLocal.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkLocal.Location = new System.Drawing.Point(16, 16);
            this.chkLocal.Name = "chkLocal";
            this.chkLocal.Size = new System.Drawing.Size(160, 24);
            this.chkLocal.TabIndex = 2;
            this.chkLocal.Text = "Local Games";
            this.chkLocal.Click += new System.EventHandler(this.chk_Click);
            this.chkLocal.MouseDown += new System.Windows.Forms.MouseEventHandler(this.chkLocal_MouseDown);
            // 
            // ConfigAdvFile
            // 
            this.Controls.Add(this.groupLast);
            this.Controls.Add(this.C_TextFolder);
            this.Controls.Add(this.C_BtnOpenFolder);
            this.Controls.Add(this.C_TabGameEdl);
            this.Name = "ConfigAdvFile";
            this.C_TabGameEdl.ResumeLayout(false);
            this.C_TabGame.ResumeLayout(false);
            this.C_TabEdl.ResumeLayout(false);
            this.groupLast.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		//Browse
		private void C_BtnOpenFolder_Click(object sender, System.EventArgs e)
		{
			if(this.C_FolderBrowserDlg.ShowDialog() == DialogResult.OK)
			{
				string folderPath=this.C_FolderBrowserDlg.SelectedPath;

				folderPath=folderPath+"\\scouting.tmn";

				if(!System.IO.File.Exists(folderPath))
				{
					Webb.Utilities.MessageBoxEx.ShowError("Please select a correct user folder which contains the file named 'scouting.tmn' !");
                   
					return;
				}

				this.ClearAllList();

				this.C_TextFolder.Text = this.C_FolderBrowserDlg.SelectedPath;

				this._LocalUserFolder=this.C_TextFolder.Text;

				this.LoadFilesName(this.C_TextFolder.Text);

				this.SaveLastUserFolderToReg(this._LocalUserFolder);
			}
		}


		#region Load Games/Edls
		private void LoadFilesName(string strDirectory)
		{
			if(!System.IO.Directory.Exists(strDirectory)) return;
			
			this.LoadGamesName(strDirectory);

			this.LoadEdlsName(strDirectory);
			
			try
			{			
				string[] arrDirectories = System.IO.Directory.GetDirectories(strDirectory);

				foreach(string strDir in arrDirectories)
				{
					this.LoadFilesName(strDir);
				}
			}
			catch(Exception ex)
			{
				Webb.Utilities.MessageBoxEx.ShowError(ex.Message);

				this.C_ListGamesInFolder.Items.Clear();

				this.C_ListEdlsInFolder.Items.Clear();
			}
		}

		private void LoadGamesName(string strDirectory)
		{
			string[] arrGames = System.IO.Directory.GetFiles(strDirectory,"*.exp");

			this.C_ListGamesInFolder.BeginUpdate();
			
			foreach(string strGame in arrGames)
			{
				string strGameName = this.GetFileName(strGame);
				
				if(this.CheckGameName(strGameName))
				{
					AdvFileInfo info = new AdvFileInfo(strGameName,strGame);
					
					this.C_ListGamesInFolder.Items.Add(info);
				}
			}

			this.C_ListGamesInFolder.EndUpdate();
		}

		private bool CheckGameName(string strGameName)
		{
			if(strGameName.IndexOf(" VS ")>0
				&&strGameName.IndexOf(" ON ")>0
				&&strGameName.IndexOf(" AT ")>0)
				return true;
			return false;
		}

		private void LoadEdlsName(string strDirectory)
		{
			string[] arrEdls = System.IO.Directory.GetFiles(strDirectory,"*.edl");

			this.C_ListEdlsInFolder.BeginUpdate();

			foreach(string strEdl in arrEdls)
			{
				string strEdlName = this.GetFileName(strEdl);

				AdvFileInfo info = new AdvFileInfo(strEdlName,strEdl);

				this.C_ListEdlsInFolder.Items.Add(info);
			}

			this.C_ListEdlsInFolder.EndUpdate();
		}

		private string GetFileName(string path)
		{
			int indexBacklash = path.LastIndexOf(@"\");
			int indexDot = path.LastIndexOf(".");
			int length = indexDot - indexBacklash;
			return path.Substring(indexBacklash + 1,length - 1);
		}

		#endregion

		#region Add/Delete items in the list
		//=>
		private void C_BtnAdd_Click(object sender, System.EventArgs e)
		{
			ArrayList arrSel = new ArrayList();

			foreach(object item in this.C_ListGamesInFolder.SelectedItems)
			{
				arrSel.Add(item);
			}

			foreach(object item in arrSel)
			{
				this.C_ListSelectedGames.Items.Add(item);
				
				this.C_ListGamesInFolder.Items.Remove(item);
			}
		}

		//<=
		private void C_BtnDelete_Click(object sender, System.EventArgs e)
		{
			ArrayList arrSel = new ArrayList();

			foreach(object item in this.C_ListSelectedGames.SelectedItems)
			{
				arrSel.Add(item);
			}

			foreach(object item in arrSel)
			{
				this.C_ListGamesInFolder.Items.Add(item);
				
				this.C_ListSelectedGames.Items.Remove(item);
			}
		}

		//=>
		private void C_BtnAddEdl_Click(object sender, System.EventArgs e)
		{
			ArrayList arrSel = new ArrayList();

			foreach(object item in this.C_ListEdlsInFolder.SelectedItems)
			{
				arrSel.Add(item);
			}

			foreach(object item in arrSel)
			{
				this.C_ListSelectedEdls.Items.Add(item);
				
				this.C_ListEdlsInFolder.Items.Remove(item);
			}
		}

		//<=
		private void C_BtnDelEdl_Click(object sender, System.EventArgs e)
		{
			ArrayList arrSel = new ArrayList();

			foreach(object item in this.C_ListSelectedEdls.SelectedItems)
			{
				arrSel.Add(item);
			}

			foreach(object item in arrSel)
			{
				this.C_ListEdlsInFolder.Items.Add(item);
				
				this.C_ListSelectedEdls.Items.Remove(item);
			}
		}

		//=>
		private void C_ListGamesInFolder_DoubleClick(object sender, System.EventArgs e)
		{
			object item = this.C_ListGamesInFolder.SelectedItem;
			
			if(item == null) return;
			
			this.C_ListSelectedGames.Items.Add(item);
				
			this.C_ListGamesInFolder.Items.Remove(item);
		}

		//<=
		private void C_ListSelectedGames_DoubleClick(object sender, System.EventArgs e)
		{
			object item = this.C_ListSelectedGames.SelectedItem;

			if(item == null) return;

			this.C_ListGamesInFolder.Items.Add(item);
			
			this.C_ListSelectedGames.Items.Remove(item);
		}
		
		//=>
		private void C_ListEdlsInFolder_DoubleClick(object sender, System.EventArgs e)
		{
			object item = this.C_ListEdlsInFolder.SelectedItem;

			if(item == null) return;

			this.C_ListSelectedEdls.Items.Add(item);
				
			this.C_ListEdlsInFolder.Items.Remove(item);
		}

		//<=
		private void C_ListSelectedEdls_DoubleClick(object sender, System.EventArgs e)
		{
			object item = this.C_ListSelectedEdls.SelectedItem;

			if(item == null) return;

			this.C_ListEdlsInFolder.Items.Add(item);
			
			this.C_ListSelectedEdls.Items.Remove(item);
		}

		//select all
		public override void OnSelectAll()
		{
			base.OnSelectAll();
			if(this.bGame)
			{
				foreach(Object item in this.C_ListGamesInFolder.Items)
				{
					this.C_ListSelectedGames.Items.Add(item);
				}
				this.C_ListGamesInFolder.Items.Clear();
			}
			else
			{
				foreach(Object item in this.C_ListEdlsInFolder.Items)
				{
					this.C_ListSelectedEdls.Items.Add(item);
				}
				this.C_ListEdlsInFolder.Items.Clear();
			}
		}

		//clear all
		public override void OnClearAll()
		{
			base.OnClearAll ();
			if(this.bGame)
			{
				foreach(Object item in this.C_ListSelectedGames.Items)
				{
					this.C_ListGamesInFolder.Items.Add(item);
				}
				this.C_ListSelectedGames.Items.Clear();
			}
			else
			{
				foreach(Object item in this.C_ListSelectedEdls.Items)
				{
					this.C_ListEdlsInFolder.Items.Add(item);
				}
				this.C_ListSelectedEdls.Items.Clear();
			}
		}


		#endregion
		

		public override bool ValidateSetting()
		{
			if(this.C_ListSelectedGames.Items.Count==0&&this.C_ListSelectedEdls.Items.Count==0)
			{	
				MessageBox.Show("Your didn't select any games or edls in this step!",
					"Failed",MessageBoxButtons.OK,MessageBoxIcon.Stop);
				return false;
				
			}

			if(!System.IO.Directory.Exists(this._ShareUserFolder)&&this.chkShare.Checked)
			{	
				MessageBox.Show(string.Format("Failed to find the path {0},please check whether your network or the path is valid",_ShareUserFolder),
					             "Failed",MessageBoxButtons.OK,MessageBoxIcon.Stop);
				return false;
				
			}
			
			return true;
		}

		public override void SetData(object i_Data)
		{			
			this.C_TextFolder.Text=this._LocalUserFolder;

			this.lblShare.Text="Shared folder Path:"+this._ShareUserFolder;

			if(!System.IO.Directory.Exists(_ShareUserFolder))
			{	
				this.chkShare.Enabled=false;	
			
				this.chkLocal.Enabled=false;

                this.lblShare.Text = "Unable to access your Shared Games!";

				this.lblShare.Visible=true;			 

			}
			else
			{
				this.chkShare.Enabled=true;	
			
				this.chkLocal.Enabled=true;	
			}

			this.chkLocal.Checked=true;

			this.chkShare.Checked=false;			

			this.ClearAllList();

			if(this.chkLocal.Checked)
			{					
				this.LoadFilesName(this._LocalUserFolder);
			}          
			
			if(this.chkShare.Checked)
			{
				this.LoadFilesName(_ShareUserFolder+"\\Imported Games"); 

				this.lblShare.Visible=true;
			}	
			else
			{
				if(this.chkShare.Enabled)
				{
					this.lblShare.Visible=false;
				}
			}
		}	

		private void ClearAllList()
		{
			this.C_ListGamesInFolder.Items.Clear();
			this.C_ListSelectedGames.Items.Clear();
			this.C_ListEdlsInFolder.Items.Clear();
			this.C_ListSelectedEdls.Items.Clear();
		}

		public override void UpdateData(object i_Data)
		{
			if(!(i_Data is DBSourceConfig)) return;
			
			DBSourceConfig m_DBConfig = i_Data as DBSourceConfig;
			
			m_DBConfig.DBFilePath = this.Folder;

			m_DBConfig.Games.Clear();
			m_DBConfig.Edls.Clear();

			string path = string.Empty;

			foreach(object o in this.C_ListSelectedGames.Items)
			{
				path = (o as AdvFileInfo).FilePath;

				m_DBConfig.Games.Add(path);		
			}

			foreach(object o in this.C_ListSelectedEdls.Items)
			{
				path = (o as AdvFileInfo).FilePath;

				m_DBConfig.Edls.Add(path);
			}


			m_DBConfig.UserFolder = this.Folder;
			m_DBConfig.HeaderName = string.Empty;
		}

		private void C_TabGameEdl_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			switch(this.C_TabGameEdl.SelectedIndex)
			{
				case 0:
					this.bGame = true;
					break;
				case 1:
					this.bGame = false;
					break;
				default:
					break;
			}
		}

		#region	Register
		private string LoadLastUserFolderFromReg()
		{
			string strFolder = string.Empty;
			
			object Folder = this.RegKey.GetValue(KeyName);

            if (Folder != null) strFolder = Folder.ToString().Trim();

            return strFolder;
		}

		private void SaveLastUserFolderToReg(string i_FilePath)
		{
			this.RegKey.SetValue(KeyName,i_FilePath);
		}
        
		private string LoadLShareFolderFromReg()
		{
			string strFolder = string.Empty;
			
			object Folder = this.RegShareKey.GetValue(ShareKeyName);

            if (Folder != null) strFolder = Folder.ToString().TrimEnd(@"\ ".ToCharArray()) + "\\share";

            return strFolder;
		}	


		#endregion

		
		private void SetCheckedState()
		{
			this.ClearAllList();

            this.C_TextFolder.Text=this._LocalUserFolder;	

			this.lblShare.Text="Shared folder Path:    "+this._ShareUserFolder;

			if(this.chkLocal.Checked)
			{
				this.LoadFilesName(this._LocalUserFolder);
			}  
			
			if(this.chkShare.Checked)
			{
				Webb.Utilities.WaitingForm.ShowWaitingForm();

                Webb.Utilities.WaitingForm.SetWaitingMessage("Searching destination network folder and loading games....");
				
				this.LoadFilesName(_ShareUserFolder+"\\Imported Games");
                
				Webb.Utilities.WaitingForm.CloseWaitingForm();

				this.lblShare.Visible=true;
			}	
				
		}

		private void chk_Click(object sender, System.EventArgs e)
		{
			if(this.chkShare.Enabled)
			{
				this.SetCheckedState();	
			}
		}

		private void chkShare_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(!this.chkShare.Checked)
			{
				this.chkLocal.Checked=false;
                this.lblShare.Visible = true;
			}
		}

        private void chkLocal_MouseDown(object sender, MouseEventArgs e)
        {
            if (!this.chkLocal.Checked)
            {
                this.chkShare.Checked = false;

            }
        }		
	}
}
