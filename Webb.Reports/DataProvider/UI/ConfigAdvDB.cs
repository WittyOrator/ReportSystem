using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace Webb.Reports.DataProvider.UI
{
	public class ConfigAdvDB : Webb.Utilities.Wizards.WinzardControlBase
	{
		private string m_strSharedPath;
		private string m_strLocalPath;
//		private string m_strRemotePath;
		private string m_strIniPath;
		private System.Windows.Forms.CheckedListBox C_CheckList;
		private System.Windows.Forms.TextBox C_TextIniFile;
		private System.Windows.Forms.Button C_BtnBrowse;
		private System.Windows.Forms.OpenFileDialog C_OpenFileDialog;
		private System.ComponentModel.IContainer components = null;

		public ConfigAdvDB()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
			this.C_OpenFileDialog = new OpenFileDialog();
			this.C_OpenFileDialog.DefaultExt = "ini";
			this.C_OpenFileDialog.Filter = "Ini file(*.ini)|*.ini";

			this.m_strIniPath = string.Empty;

			this.WizardTitle = "Step 2: Choose data source";
			this.FinishControl = false;
			this.LastControl = false;
			this.SelectStep = true;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.C_TextIniFile = new System.Windows.Forms.TextBox();
			this.C_BtnBrowse = new System.Windows.Forms.Button();
			this.C_CheckList = new System.Windows.Forms.CheckedListBox();
			this.SuspendLayout();
			// 
			// C_TextIniFile
			// 
			this.C_TextIniFile.Enabled = false;
			this.C_TextIniFile.Location = new System.Drawing.Point(24, 16);
			this.C_TextIniFile.Name = "C_TextIniFile";
			this.C_TextIniFile.Size = new System.Drawing.Size(536, 22);
			this.C_TextIniFile.TabIndex = 8;
			this.C_TextIniFile.Text = "";
			// 
			// C_BtnBrowse
			// 
			this.C_BtnBrowse.Location = new System.Drawing.Point(568, 16);
			this.C_BtnBrowse.Name = "C_BtnBrowse";
			this.C_BtnBrowse.Size = new System.Drawing.Size(80, 23);
			this.C_BtnBrowse.TabIndex = 7;
			this.C_BtnBrowse.Text = "Browse...";
			this.C_BtnBrowse.Click += new System.EventHandler(this.C_BtnBrowse_Click);
			// 
			// C_CheckList
			// 
			this.C_CheckList.CheckOnClick = true;
			this.C_CheckList.Location = new System.Drawing.Point(24, 56);
			this.C_CheckList.Name = "C_CheckList";
			this.C_CheckList.Size = new System.Drawing.Size(736, 395);
			this.C_CheckList.TabIndex = 9;
			this.C_CheckList.SelectedIndexChanged += new System.EventHandler(this.C_CheckList_SelectedIndexChanged);
			this.C_CheckList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.C_CheckList_ItemCheck);
			// 
			// ConfigAdvDB
			// 
			this.Controls.Add(this.C_CheckList);
			this.Controls.Add(this.C_TextIniFile);
			this.Controls.Add(this.C_BtnBrowse);
			this.Name = "ConfigAdvDB";
			this.ResumeLayout(false);

		}
		#endregion

		private void C_BtnBrowse_Click(object sender, System.EventArgs e)
		{
			if(this.C_OpenFileDialog.ShowDialog(this) == DialogResult.OK)
			{
				this.m_strIniPath = this.C_OpenFileDialog.FileName;
				this.C_TextIniFile.Text = this.C_OpenFileDialog.FileName;
				this.UpdateList();
			}
		}

		private void UpdateList()
		{
			this.C_CheckList.Items.Clear();

			if(File.Exists(this.m_strIniPath))
			{
				Webb.Data.IniFile iniFile = new Webb.Data.IniFile(this.m_strIniPath);

				string strConnenction = iniFile.IniReadValue("ConnectionString","strConnection");
				
				if(strConnenction != string.Empty)
				{
					m_strLocalPath = strConnenction;
					
					this.C_CheckList.Items.Add("Local",false);
				}

				string strSharedConnection = iniFile.IniReadValue("SharedConnectionString","strConnection");

				if(strSharedConnection != string.Empty)
				{
					m_strSharedPath = strSharedConnection;

					this.C_CheckList.Items.Add("Shared",false);
				}

				//string strRemoteConnenction = iniFile.IniReadValue("RemoteConnectionString","strConnection");
				//this.C_CheckList.Items.Add("Remote",false);
			}
		}

		public override void UpdateData(object i_Data)
		{
			if(!(i_Data is DBSourceConfig)) return;
			
			DBSourceConfig m_DBConfig = i_Data as DBSourceConfig;

			object item = this.C_CheckList.SelectedItem;

			if(item == null) return;

			string strConn = item.ToString();

			switch(strConn) 
			{
				case "Local":
					strConn = m_strLocalPath;
					break;
				default:
					strConn = m_strSharedPath;
					break;
			}

			int index = strConn.IndexOf(';');

			strConn = strConn.Substring(index + 1, strConn.Length - index - 1);

			m_DBConfig.ConnString = strConn;

			//Modified at 2009-1-6 9:01:04@Scott
			if(File.Exists(this.m_strIniPath))
			{
				int bsIndex = this.m_strIniPath.LastIndexOf(@"\");

				m_DBConfig.UserFolder = this.m_strIniPath.Remove(bsIndex,this.m_strIniPath.Length - bsIndex);
			}
		}

		public override bool ValidateSetting()
		{
			if(this.C_CheckList.CheckedItems.Count > 0)
			{
				return true;
			}
			else
			{
				MessageBox.Show("Please choose a data source.");
				return false;
			}
		}

		private void C_CheckList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			
		}

		private void C_CheckList_ItemCheck(object sender, System.Windows.Forms.ItemCheckEventArgs e)
		{
			if (this.C_CheckList.CheckedItems.Count > 0)
			{
				for (int i = 0; i < this.C_CheckList.Items.Count; i++)
				{
					if (i != e.Index)
					{
						this.C_CheckList.SetItemCheckState(i, System.Windows.Forms.CheckState.Unchecked);
					}
				}
			}
		}
	}
}
