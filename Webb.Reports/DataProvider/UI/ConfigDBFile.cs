/***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:ConfigDBFile.cs
 * Author:Country.Wu [EMail:Webb.Country.Wu@163.com]
 * Create Time:11/8/2007 03:24:01 PM
 * Copyright:1986-2007@Webb Electronics all right reserved.
 * Purpose:
 * ***********************************************************************/
#region History
/*
 * //Author@DateTime : Description
 * */
#endregion History

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Microsoft.Win32;

using Webb;
using Webb.Data;
using Webb.Utilities.Wizards;

namespace Webb.Reports.DataProvider.UI
{
	/// <summary>
	/// Summary description for ConfigDBFile.
	/// </summary>
	public class ConfigDBFile : Webb.Utilities.Wizards.WinzardControlBase
	{
		//11-14-2007
		DBConnTypes _ConnType;
		WebbDBTypes _WebbDBType;

		static readonly string RegKeyPath = "SOFTWARE\\Webb Electronics\\WebbReport\\RecentAccessFiles";		
		//
		private RegistryKey _RegKey;
		private RegistryKey RegKey
		{
			get
			{
				if(this._RegKey==null)
				{
					this._RegKey = Registry.CurrentUser.OpenSubKey(RegKeyPath,true);
					if(this._RegKey==null)
					{
						this._RegKey = Registry.CurrentUser.CreateSubKey(RegKeyPath);
					}
				}
				return this._RegKey;
			}
		}

		private System.Windows.Forms.GroupBox C_AccessGroup;
		private System.Windows.Forms.LinkLabel C_ShowRecentFiles;
		private System.Windows.Forms.ListBox C_RecentFiels;
		private System.Windows.Forms.TextBox C_SelectedFile;
		private System.Windows.Forms.Button C_Browse;
		private System.Windows.Forms.ContextMenu C_ContextMenu;
		private System.Windows.Forms.MenuItem C_Menu_SelectFile;
		private System.Windows.Forms.MenuItem C_Menu_DeleteFile;
		private System.Windows.Forms.Label C_TitleMsg;
		private System.Windows.Forms.OpenFileDialog C_OpenFileDialog;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ConfigDBFile()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
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
			this.C_AccessGroup = new System.Windows.Forms.GroupBox();
			this.C_ShowRecentFiles = new System.Windows.Forms.LinkLabel();
			this.C_RecentFiels = new System.Windows.Forms.ListBox();
			this.C_ContextMenu = new System.Windows.Forms.ContextMenu();
			this.C_Menu_SelectFile = new System.Windows.Forms.MenuItem();
			this.C_Menu_DeleteFile = new System.Windows.Forms.MenuItem();
			this.C_SelectedFile = new System.Windows.Forms.TextBox();
			this.C_Browse = new System.Windows.Forms.Button();
			this.C_TitleMsg = new System.Windows.Forms.Label();
			this.C_OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.C_AccessGroup.SuspendLayout();
			this.SuspendLayout();
			// 
			// C_AccessGroup
			// 
			this.C_AccessGroup.Controls.Add(this.C_ShowRecentFiles);
			this.C_AccessGroup.Controls.Add(this.C_RecentFiels);
			this.C_AccessGroup.Controls.Add(this.C_SelectedFile);
			this.C_AccessGroup.Controls.Add(this.C_Browse);
			this.C_AccessGroup.Location = new System.Drawing.Point(8, 24);
			this.C_AccessGroup.Name = "C_AccessGroup";
			this.C_AccessGroup.Size = new System.Drawing.Size(768, 440);
			this.C_AccessGroup.TabIndex = 8;
			this.C_AccessGroup.TabStop = false;
			this.C_AccessGroup.Text = "Select Access data source  file";
			// 
			// C_ShowRecentFiles
			// 
			this.C_ShowRecentFiles.Location = new System.Drawing.Point(8, 56);
			this.C_ShowRecentFiles.Name = "C_ShowRecentFiles";
			this.C_ShowRecentFiles.Size = new System.Drawing.Size(696, 23);
			this.C_ShowRecentFiles.TabIndex = 7;
			this.C_ShowRecentFiles.TabStop = true;
			this.C_ShowRecentFiles.Text = "Hide recent files <<";
			this.C_ShowRecentFiles.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.C_ShowRecentFiles_LinkClicked);
			// 
			// C_RecentFiels
			// 
			this.C_RecentFiels.ContextMenu = this.C_ContextMenu;
			this.C_RecentFiels.ItemHeight = 14;
			this.C_RecentFiels.Location = new System.Drawing.Point(8, 80);
			this.C_RecentFiels.Name = "C_RecentFiels";
			this.C_RecentFiels.Size = new System.Drawing.Size(752, 354);
			this.C_RecentFiels.TabIndex = 6;
			this.C_RecentFiels.DoubleClick += new System.EventHandler(this.C_RecentFiels_DoubleClick);
			// 
			// C_ContextMenu
			// 
			this.C_ContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						  this.C_Menu_SelectFile,
																						  this.C_Menu_DeleteFile});
			// 
			// C_Menu_SelectFile
			// 
			this.C_Menu_SelectFile.Index = 0;
			this.C_Menu_SelectFile.Text = "Select File";
			this.C_Menu_SelectFile.Click += new System.EventHandler(this.C_Menu_SelectFile_Click);
			// 
			// C_Menu_DeleteFile
			// 
			this.C_Menu_DeleteFile.Index = 1;
			this.C_Menu_DeleteFile.Text = "Remove Recent File";
			this.C_Menu_DeleteFile.Click += new System.EventHandler(this.C_Menu_DeleteFile_Click);
			// 
			// C_SelectedFile
			// 
			this.C_SelectedFile.BackColor = System.Drawing.SystemColors.Window;
			this.C_SelectedFile.Location = new System.Drawing.Point(8, 24);
			this.C_SelectedFile.Name = "C_SelectedFile";
			this.C_SelectedFile.ReadOnly = true;
			this.C_SelectedFile.Size = new System.Drawing.Size(608, 22);
			this.C_SelectedFile.TabIndex = 4;
			this.C_SelectedFile.Text = "";
			// 
			// C_Browse
			// 
			this.C_Browse.Location = new System.Drawing.Point(632, 24);
			this.C_Browse.Name = "C_Browse";
			this.C_Browse.TabIndex = 5;
			this.C_Browse.Text = "Browse...";
			this.C_Browse.Click += new System.EventHandler(this.C_Browse_Click);
			// 
			// C_TitleMsg
			// 
			this.C_TitleMsg.Location = new System.Drawing.Point(8, 0);
			this.C_TitleMsg.Name = "C_TitleMsg";
			this.C_TitleMsg.Size = new System.Drawing.Size(520, 23);
			this.C_TitleMsg.TabIndex = 7;
			this.C_TitleMsg.Text = "Please select a Database to open.";
			// 
			// C_OpenFileDialog
			// 
			this.C_OpenFileDialog.DefaultExt = "mdb";
			this.C_OpenFileDialog.DereferenceLinks = false;
			this.C_OpenFileDialog.FileName = "Victory.mdb";
			this.C_OpenFileDialog.Filter = "Access database file(*.mdb)|*.mdb|XML file(*.xml)|*.xml|All files(*.*)|*.*";
			this.C_OpenFileDialog.RestoreDirectory = true;
			this.C_OpenFileDialog.Title = "Chose Database file to open";
			// 
			// ConfigDBFile
			// 
			this.Controls.Add(this.C_AccessGroup);
			this.Controls.Add(this.C_TitleMsg);
			this.Name = "ConfigDBFile";
			this.C_AccessGroup.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void C_ShowRecentFiles_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			this.C_RecentFiels.Visible = !this.C_RecentFiels.Visible;
			if(this.C_RecentFiels.Visible)
			{
				this.C_ShowRecentFiles.Text = "Hide recent files <<";
			}
			else
			{
				this.C_ShowRecentFiles.Text = "Show recent files >>";
			}
		}

		private void C_Browse_Click(object sender, System.EventArgs e)
		{
			if(this.C_OpenFileDialog.ShowDialog(this.ParentForm)==DialogResult.OK)
			{
				this.C_SelectedFile.Text = this.C_OpenFileDialog.FileName;
			}
		}

		private void C_Menu_SelectFile_Click(object sender, System.EventArgs e)
		{
			if(this.C_RecentFiels.SelectedItem!=null)
			{
				this.C_SelectedFile.Text = this.C_RecentFiels.SelectedItem.ToString();
			}
		}

		private void C_Menu_DeleteFile_Click(object sender, System.EventArgs e)
		{
			if(this.C_RecentFiels.SelectedItem!=null)
			{
				this.DeleteFileFromReg(this.C_RecentFiels.SelectedItem.ToString());
				this.C_RecentFiels.Items.Remove(this.C_RecentFiels.SelectedItem);
			}	
		}

		private void C_RecentFiels_DoubleClick(object sender, System.EventArgs e)
		{
			if(this.C_RecentFiels.SelectedItem!=null)
			{
				//11-14-2007@Scott
				string m_TempPath =  this.C_RecentFiels.SelectedItem.ToString();
				if((this._ConnType == Webb.Data.DBConnTypes.OleDB && m_TempPath.EndsWith(".mdb"))
					||(this._ConnType == Webb.Data.DBConnTypes.XMLFile && m_TempPath.EndsWith(".xml")))
				{
					this.C_SelectedFile.Text = m_TempPath;	
				}
			}
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad (e);

			string[] m_Files = this.LoadFilesFromReg();
			if(m_Files==null) return;
			this.C_RecentFiels.Items.Clear();
			foreach(string m_File in m_Files)
			{
				if(m_File!=null)
					this.C_RecentFiels.Items.Add(m_File);
			}
		}

		private void DeleteFileFromReg(string i_File)
		{
			string[] m_Names = this.RegKey.GetValueNames();
			string m_FindName = string.Empty;			
			foreach(string m_Name in m_Names)
			{
				if(this.RegKey.GetValue(m_Name).ToString()==i_File)
				{
					m_FindName = m_Name;
					break;
				}
			}
			this.RegKey.DeleteValue(m_FindName,false);
		}

		private string[] LoadFilesFromReg()
		{
			string[] m_Fiels = new string[this.RegKey.ValueCount];
			string[] m_Names = this.RegKey.GetValueNames();
			int i = 0;
			foreach(string m_Name in m_Names)
			{
				//Scott@2007-11-16 16:29 modified some of the following code.
				m_Fiels[this.RegKey.ValueCount - i - 1] = this.RegKey.GetValue(m_Name).ToString();
				i++;
			}
			return m_Fiels;
		}

		private void SaveFilesToReg(string i_FilePath)
		{
			string[] m_Names = this.RegKey.GetValueNames();
			foreach(string m_Name in m_Names)
			{
				if(this.RegKey.GetValue(m_Name).ToString()==i_FilePath)
				{
					return;
				}
			}
			//Scott@2007-11-16 16:17 modified some of the following code.
			this.RegKey.SetValue(System.DateTime.Now.ToString(),i_FilePath);
		}

//		private void C_SelectedFile_TextChanged(object sender, System.EventArgs e)
//		{
//			if(System.IO.File.Exists(this.C_SelectedFile.Text))
//			{
//				this.ParentWizardForm.WizardStatus |= WizardStatus.Next;
//			}
//			else
//			{
//				this.ParentWizardForm.WizardStatus &= ~WizardStatus.Next;
//			}
//		}

		public override void UpdateData(object i_Data)
		{
			//base.UpdateData (i_Data);
			DBSourceConfig m_Config = i_Data as DBSourceConfig;
			m_Config.DBFilePath = this.C_SelectedFile.Text;
		}

		public override bool ValidateSetting()
		{
			//return base.ValidateSetting ();
			if(System.IO.File.Exists(this.C_SelectedFile.Text))
			{
				this.SaveFilesToReg(this.C_SelectedFile.Text);	//11-12-2007@Scott
				return true;
			}
			else
			{
				MessageBox.Show(this,"The selected file doesn't exist, please select another file.","Error:",MessageBoxButtons.OK,MessageBoxIcon.Warning);
				//this.ParentWizardForm.WizardStatus &= ~WizardStatus.Next;
				return false;
			}
		}

		public override void ResetControl()
		{
			//base.ResetControl ();
			//this.C_RecentFiels.Items.Clear();
		}


		public override void SetData(object i_Data)
		{
			//base.SetData (i_Data);
			DBSourceConfig config = i_Data as DBSourceConfig;
			this._ConnType = config.DBConnType;
			this._WebbDBType = config.WebbDBType;

			//Scott@2007-11-30 12:48 modified some of the following code.
			//this.C_SelectedFile.Clear();
			if(this._ConnType == Webb.Data.DBConnTypes.OleDB)
			{
				this.WizardTitle = "Step 2: Select an Access file.";
				this.C_AccessGroup.Text = "Select Access data source  file";
				this.C_TitleMsg.Text = "Please select a Database to open.";
				this.C_OpenFileDialog.DefaultExt = "mdb";
				this.C_OpenFileDialog.DereferenceLinks = false;
				this.C_OpenFileDialog.FileName = ".mdb";
				this.C_OpenFileDialog.Filter = "Access database file(*.mdb)|*.mdb";
				this.C_OpenFileDialog.RestoreDirectory = true;
				this.C_OpenFileDialog.Title = "Choose Database file to open";
			}
			else if(this._ConnType == Webb.Data.DBConnTypes.XMLFile)
			{
				this.WizardTitle = "Step 2: Select an XML file.";
				this.C_AccessGroup.Text = "Select Xml data source  file";
				this.C_TitleMsg.Text = "Please select a XML file to open.";
				this.C_OpenFileDialog.DefaultExt = "xml";
				this.C_OpenFileDialog.DereferenceLinks = false;
				this.C_OpenFileDialog.FileName = ".xml";
				this.C_OpenFileDialog.Filter = "XML file(*.xml)|*.xml";
				this.C_OpenFileDialog.RestoreDirectory = true;
				this.C_OpenFileDialog.Title = "Choose Xml file to open";
			}
			else{/*never*/}
		}
	}
}
