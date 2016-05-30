using System;
using System.Data;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
//
using System.IO;

using Webb.Collections;
using Webb.Utilities;
using Microsoft.Win32;

namespace Webb.Reports.DataProvider.UI
{
    public class ConfigPlayBookData : Webb.Utilities.Wizards.WinzardControlBase
    {
        private TextBox C_TextFolder;
        private Label lblSelect;
        private ComboBox cmbScoutType;
        private Label label1;
        private TabControl C_TabFormationAndPlays;
        private TabPage C_TabFormation;
        private TreeView treeViewFormations;
        private Button C_BtnDelete;
        private Button C_BtnAdd;
        private ListBox C_ListSelectedGames;
        private TabPage C_TabPlays;
        private Button C_BtnDelEdl;
        private Button C_BtnAddEdl;
        private ListBox C_ListSelectedPlays;
        private TreeView treeViewPlays;
        private ImageList imageList1;
        private IContainer components;
        private Button C_BtnOpenFolder;

        private string InstallKeyPath = @"Software\Webb Electronics\PlayBook";
        private string InstallDirKeyName = @"InstallDir";

        private class PlayBookFileDescription
        {
            private string _Name;
            private string _FileName;
            public PlayBookFileDescription(string name, string fileName)
            {
                _Name = name;
                _FileName = fileName;
            }
            public string FileName
            {
                get
                {
                    if (_FileName == null) _FileName = string.Empty;
                    return this._FileName;
                }
            }
            public override string ToString()
            {
                return _Name;
            }
            public override int GetHashCode()
            {
                return FileName.GetHashCode();
            }
            public override bool Equals(object obj)
            {
                if (obj is PlayBookFileDescription)
                {
                    PlayBookFileDescription playbookdescrpition = obj as PlayBookFileDescription;

                    if (this._FileName == playbookdescrpition._FileName) return true;

                }

                return false;
            }
        }

        public ConfigPlayBookData()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();


            this.WizardTitle = "Step 2: Choose user folder";

            this.cmbScoutType.SelectedIndex = 0;

            this.FinishControl = true;
            this.LastControl = true;
            this.SelectStep = false;
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigPlayBookData));
            this.C_TextFolder = new System.Windows.Forms.TextBox();
            this.C_BtnOpenFolder = new System.Windows.Forms.Button();
            this.lblSelect = new System.Windows.Forms.Label();
            this.cmbScoutType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.C_TabFormationAndPlays = new System.Windows.Forms.TabControl();
            this.C_TabFormation = new System.Windows.Forms.TabPage();
            this.treeViewFormations = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.C_BtnDelete = new System.Windows.Forms.Button();
            this.C_BtnAdd = new System.Windows.Forms.Button();
            this.C_ListSelectedGames = new System.Windows.Forms.ListBox();
            this.C_TabPlays = new System.Windows.Forms.TabPage();
            this.treeViewPlays = new System.Windows.Forms.TreeView();
            this.C_BtnDelEdl = new System.Windows.Forms.Button();
            this.C_BtnAddEdl = new System.Windows.Forms.Button();
            this.C_ListSelectedPlays = new System.Windows.Forms.ListBox();
            this.C_TabFormationAndPlays.SuspendLayout();
            this.C_TabFormation.SuspendLayout();
            this.C_TabPlays.SuspendLayout();
            this.SuspendLayout();
            // 
            // C_TextFolder
            // 
            this.C_TextFolder.Enabled = false;
            this.C_TextFolder.Location = new System.Drawing.Point(4, 38);
            this.C_TextFolder.Name = "C_TextFolder";
            this.C_TextFolder.Size = new System.Drawing.Size(439, 22);
            this.C_TextFolder.TabIndex = 8;
            // 
            // C_BtnOpenFolder
            // 
            this.C_BtnOpenFolder.Location = new System.Drawing.Point(449, 37);
            this.C_BtnOpenFolder.Name = "C_BtnOpenFolder";
            this.C_BtnOpenFolder.Size = new System.Drawing.Size(80, 23);
            this.C_BtnOpenFolder.TabIndex = 7;
            this.C_BtnOpenFolder.Text = "Browse...";
            this.C_BtnOpenFolder.Click += new System.EventHandler(this.C_BtnOpenFolder_Click);
            // 
            // lblSelect
            // 
            this.lblSelect.AutoSize = true;
            this.lblSelect.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelect.Location = new System.Drawing.Point(3, 9);
            this.lblSelect.Name = "lblSelect";
            this.lblSelect.Size = new System.Drawing.Size(468, 18);
            this.lblSelect.TabIndex = 9;
            this.lblSelect.Text = "Please select the folder which playbook is installed in";
            // 
            // cmbScoutType
            // 
            this.cmbScoutType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbScoutType.FormattingEnabled = true;
            this.cmbScoutType.Items.AddRange(new object[] {
            "Offensive",
            "Defensive",
            "Kicks"});
            this.cmbScoutType.Location = new System.Drawing.Point(637, 37);
            this.cmbScoutType.Name = "cmbScoutType";
            this.cmbScoutType.Size = new System.Drawing.Size(142, 22);
            this.cmbScoutType.TabIndex = 10;
            this.cmbScoutType.SelectedIndexChanged += new System.EventHandler(this.cmbScoutType_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(559, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 14);
            this.label1.TabIndex = 11;
            this.label1.Text = "ScoutType";
            // 
            // C_TabFormationAndPlays
            // 
            this.C_TabFormationAndPlays.Controls.Add(this.C_TabFormation);
            this.C_TabFormationAndPlays.Controls.Add(this.C_TabPlays);
            this.C_TabFormationAndPlays.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.C_TabFormationAndPlays.Location = new System.Drawing.Point(0, 72);
            this.C_TabFormationAndPlays.Name = "C_TabFormationAndPlays";
            this.C_TabFormationAndPlays.SelectedIndex = 0;
            this.C_TabFormationAndPlays.Size = new System.Drawing.Size(790, 408);
            this.C_TabFormationAndPlays.TabIndex = 12;
            // 
            // C_TabFormation
            // 
            this.C_TabFormation.Controls.Add(this.treeViewFormations);
            this.C_TabFormation.Controls.Add(this.C_BtnDelete);
            this.C_TabFormation.Controls.Add(this.C_BtnAdd);
            this.C_TabFormation.Controls.Add(this.C_ListSelectedGames);
            this.C_TabFormation.Location = new System.Drawing.Point(4, 23);
            this.C_TabFormation.Name = "C_TabFormation";
            this.C_TabFormation.Size = new System.Drawing.Size(782, 381);
            this.C_TabFormation.TabIndex = 0;
            this.C_TabFormation.Text = "Formations";
            this.C_TabFormation.UseVisualStyleBackColor = true;
            // 
            // treeViewFormations
            // 
            this.treeViewFormations.ImageIndex = 0;
            this.treeViewFormations.ImageList = this.imageList1;
            this.treeViewFormations.Location = new System.Drawing.Point(7, 8);
            this.treeViewFormations.Name = "treeViewFormations";
            this.treeViewFormations.SelectedImageIndex = 0;
            this.treeViewFormations.Size = new System.Drawing.Size(375, 368);
            this.treeViewFormations.TabIndex = 9;
            this.treeViewFormations.DoubleClick += new System.EventHandler(this.treeViewFormations_DoubleClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Folder.ico");
            this.imageList1.Images.SetKeyName(1, "Team.ico");
            // 
            // C_BtnDelete
            // 
            this.C_BtnDelete.Location = new System.Drawing.Point(386, 168);
            this.C_BtnDelete.Name = "C_BtnDelete";
            this.C_BtnDelete.Size = new System.Drawing.Size(34, 23);
            this.C_BtnDelete.TabIndex = 8;
            this.C_BtnDelete.Text = "<=";
            this.C_BtnDelete.Click += new System.EventHandler(this.C_BtnDelete_Click);
            // 
            // C_BtnAdd
            // 
            this.C_BtnAdd.Location = new System.Drawing.Point(387, 120);
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
            this.C_ListSelectedGames.Location = new System.Drawing.Point(425, 8);
            this.C_ListSelectedGames.Name = "C_ListSelectedGames";
            this.C_ListSelectedGames.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.C_ListSelectedGames.Size = new System.Drawing.Size(350, 368);
            this.C_ListSelectedGames.TabIndex = 6;
            // 
            // C_TabPlays
            // 
            this.C_TabPlays.Controls.Add(this.treeViewPlays);
            this.C_TabPlays.Controls.Add(this.C_BtnDelEdl);
            this.C_TabPlays.Controls.Add(this.C_BtnAddEdl);
            this.C_TabPlays.Controls.Add(this.C_ListSelectedPlays);
            this.C_TabPlays.Location = new System.Drawing.Point(4, 23);
            this.C_TabPlays.Name = "C_TabPlays";
            this.C_TabPlays.Size = new System.Drawing.Size(782, 381);
            this.C_TabPlays.TabIndex = 1;
            this.C_TabPlays.Text = "Plays";
            this.C_TabPlays.UseVisualStyleBackColor = true;
            // 
            // treeViewPlays
            // 
            this.treeViewPlays.ImageIndex = 0;
            this.treeViewPlays.ImageList = this.imageList1;
            this.treeViewPlays.Location = new System.Drawing.Point(8, 6);
            this.treeViewPlays.Name = "treeViewPlays";
            this.treeViewPlays.SelectedImageIndex = 0;
            this.treeViewPlays.Size = new System.Drawing.Size(354, 368);
            this.treeViewPlays.TabIndex = 13;
            this.treeViewPlays.DoubleClick += new System.EventHandler(this.treeViewPlays_DoubleClick);
            // 
            // C_BtnDelEdl
            // 
            this.C_BtnDelEdl.Location = new System.Drawing.Point(368, 144);
            this.C_BtnDelEdl.Name = "C_BtnDelEdl";
            this.C_BtnDelEdl.Size = new System.Drawing.Size(40, 23);
            this.C_BtnDelEdl.TabIndex = 12;
            this.C_BtnDelEdl.Text = "<=";
            this.C_BtnDelEdl.Click += new System.EventHandler(this.C_BtnDelEdl_Click);
            // 
            // C_BtnAddEdl
            // 
            this.C_BtnAddEdl.Location = new System.Drawing.Point(368, 75);
            this.C_BtnAddEdl.Name = "C_BtnAddEdl";
            this.C_BtnAddEdl.Size = new System.Drawing.Size(40, 23);
            this.C_BtnAddEdl.TabIndex = 11;
            this.C_BtnAddEdl.Text = "=>";
            this.C_BtnAddEdl.Click += new System.EventHandler(this.C_BtnAddEdl_Click);
            // 
            // C_ListSelectedPlays
            // 
            this.C_ListSelectedPlays.HorizontalScrollbar = true;
            this.C_ListSelectedPlays.ItemHeight = 14;
            this.C_ListSelectedPlays.Location = new System.Drawing.Point(416, 7);
            this.C_ListSelectedPlays.Name = "C_ListSelectedPlays";
            this.C_ListSelectedPlays.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.C_ListSelectedPlays.Size = new System.Drawing.Size(359, 368);
            this.C_ListSelectedPlays.TabIndex = 10;
            // 
            // ConfigPlayBookData
            // 
            this.Controls.Add(this.C_TabFormationAndPlays);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbScoutType);
            this.Controls.Add(this.lblSelect);
            this.Controls.Add(this.C_TextFolder);
            this.Controls.Add(this.C_BtnOpenFolder);
            this.Name = "ConfigPlayBookData";
            this.C_TabFormationAndPlays.ResumeLayout(false);
            this.C_TabFormation.ResumeLayout(false);
            this.C_TabPlays.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void ClearAllList()
        {
            this.treeViewFormations.Nodes.Clear();
            this.C_ListSelectedPlays.Items.Clear();
            this.C_ListSelectedGames.Items.Clear();
            this.treeViewPlays.Nodes.Clear();
        }

        private void LoadFilesName(string strDirectory)
        {
            if (!strDirectory.EndsWith(@"\")) strDirectory = strDirectory + @"\";

            strDirectory = strDirectory + this.cmbScoutType.Text + @"\";

            if (this.cmbScoutType.Text == string.Empty || !System.IO.Directory.Exists(strDirectory)) return;

            this.LoadFormationsName(strDirectory);

            this.LoadPlaysName(strDirectory);
        }
        public override void SetData(object i_Data)
        {
            this.ClearAllList();

            ReadRegistry();
        }

        private void ReadRegistry()
        {
            RegistryKey registry = Registry.LocalMachine.OpenSubKey(InstallKeyPath, false);

            if (registry == null) return;

            object obj = registry.GetValue(InstallDirKeyName);

            if (obj == null) return;

            this.C_TextFolder.Text = obj.ToString();

            this.LoadFilesName(obj.ToString());
        }

        public override bool ValidateSetting()
        {
            if (this.C_ListSelectedPlays.Items.Count == 0 && this.C_ListSelectedGames.Items.Count == 0)
            {
                Webb.Utilities.MessageBoxEx.ShowError("You need to select  1 formation or play at least!");

                return false;
            }

            return true;
        }
        public override void UpdateData(object i_Data)
        {
            if (!(i_Data is DBSourceConfig)) return;

            DBSourceConfig m_DBConfig = i_Data as DBSourceConfig;

            m_DBConfig.PlayBookFormFiles = new StringCollection();

            string path = string.Empty;

            foreach (object o in this.C_ListSelectedGames.Items)
            {
                path = (o as PlayBookFileDescription).FileName;

                m_DBConfig.PlayBookFormFiles.Add(path);
            }

            foreach (object o in this.C_ListSelectedPlays.Items)
            {
                path = (o as PlayBookFileDescription).FileName;

                m_DBConfig.PlayBookFormFiles.Add(path);
            }

            m_DBConfig.HeaderName = string.Empty;

            base.UpdateData(i_Data);
        }

        #region Create Nodes
        private TreeNode CreateDirectoryNode(string directory)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(directory);

            TreeNode node = new TreeNode();

            node.Text = directoryInfo.Name;

            node.ImageIndex = 0;

            node.SelectedImageIndex = 0;

            node.Tag = directoryInfo;

            return node;

        }
        private TreeNode CreateFileNode(FileInfo fileinfo)
        {
            TreeNode node = new TreeNode();

            node.Text = System.IO.Path.GetFileNameWithoutExtension(fileinfo.FullName);

            string installPath = this.C_TextFolder.Text;

            if (installPath != string.Empty && !installPath.EndsWith(@"\")) installPath = installPath + @"\";

            string name = fileinfo.FullName.Replace(installPath, "");

            int lastIndex = name.LastIndexOf(".");

            if (lastIndex >= 0) name = name.Substring(0, lastIndex);

            PlayBookFileDescription playBookFileDescription = new PlayBookFileDescription(name, fileinfo.FullName);

            node.ImageIndex = 1;

            node.SelectedImageIndex = 1;

            node.Tag = playBookFileDescription;

            return node;

        }

        private ArrayList GetSubNodes(string strDirectory, ListBox lstBox)
        {
            ArrayList treeNodesCollection = new ArrayList();

            DirectoryInfo directoryInfo = new DirectoryInfo(strDirectory);

            DirectoryInfo[] subdirectoryInfos = directoryInfo.GetDirectories();

            FileInfo[] fileInfos = directoryInfo.GetFiles();

            ArrayList arrList = new ArrayList();

            foreach (FileInfo fileinfo in fileInfos)
            {
                string fileName = fileinfo.FullName;

                if (lstBox.Items.Contains(fileName)) continue;

                TreeNode treeNode = this.CreateFileNode(fileinfo);

                string replaceDirectoryName = fileName.Replace(".Form", "@");

                if (System.IO.Directory.Exists(replaceDirectoryName))
                {
                    ArrayList subNodes = GetSubNodes(replaceDirectoryName, lstBox);

                    foreach (TreeNode subNode in subNodes)
                    {
                        treeNode.Nodes.Add(subNode);
                    }

                    arrList.Add(replaceDirectoryName);
                }

                treeNodesCollection.Add(treeNode);
            }

            foreach (DirectoryInfo subDirectoryInfo in subdirectoryInfos)
            {
                if (subDirectoryInfo.Name.EndsWith("@")) continue;

                TreeNode treeNode = this.CreatePlayBookNode(subDirectoryInfo.FullName, lstBox);

                if (treeNode.Nodes.Count == 0) continue;

                treeNodesCollection.Add(treeNode);
            }
            return treeNodesCollection;

        }

        private TreeNode CreatePlayBookNode(string strDirectory, ListBox lstBox)
        {
            TreeNode node = CreateDirectoryNode(strDirectory);

            DirectoryInfo directoryInfo = new DirectoryInfo(strDirectory);

            DirectoryInfo[] subdirectoryInfos = directoryInfo.GetDirectories();

            FileInfo[] fileInfos = directoryInfo.GetFiles();

            ArrayList arrList = new ArrayList();

            foreach (FileInfo fileinfo in fileInfos)
            {
                string fileName = fileinfo.FullName;

                PlayBookFileDescription playbookFileDescription = new PlayBookFileDescription("", fileName);

                if (lstBox.Items.Contains(playbookFileDescription)) continue;

                TreeNode treeNode = this.CreateFileNode(fileinfo);

                string replaceDirectoryName = fileName.Replace(".Form", "@");

                if (System.IO.Directory.Exists(replaceDirectoryName))
                {
                    ArrayList subNodes = GetSubNodes(replaceDirectoryName, lstBox);

                    foreach (TreeNode subNode in subNodes)
                    {
                        treeNode.Nodes.Add(subNode);
                    }

                    arrList.Add(replaceDirectoryName);
                }

                node.Nodes.Add(treeNode);
            }

            foreach (DirectoryInfo subDirectoryInfo in subdirectoryInfos)
            {
                if (subDirectoryInfo.Name.EndsWith("@")) continue;

                TreeNode treeNode = this.CreatePlayBookNode(subDirectoryInfo.FullName, lstBox);

                if (treeNode.Nodes.Count == 0) continue;

                node.Nodes.Add(treeNode);
            }
            return node;

        }

        #endregion

        private void LoadFormationsName(string strDirectory)
        {
            strDirectory = strDirectory + "Formation";

            if (!System.IO.Directory.Exists(strDirectory)) return;

            string[] directories = System.IO.Directory.GetDirectories(strDirectory);

            foreach (string directoryfomration in directories)
            {
                TreeNode playBookNode = CreatePlayBookNode(directoryfomration, this.C_ListSelectedGames);

                this.treeViewFormations.Nodes.Add(playBookNode);
            }

            this.treeViewFormations.ExpandAll();

            if (this.treeViewFormations.Nodes.Count > 0)
            {
                this.treeViewFormations.SelectedNode = this.treeViewFormations.Nodes[0];
            }

            this.treeViewFormations.SelectedNode = null;
        }

        private void LoadPlaysName(string strDirectory)
        {
            strDirectory = strDirectory + "Playbook";

            if (!System.IO.Directory.Exists(strDirectory)) return;

            string[] directories = System.IO.Directory.GetDirectories(strDirectory);

            foreach (string directoryfomration in directories)
            {
                TreeNode playBookNode = CreatePlayBookNode(directoryfomration, this.C_ListSelectedPlays);

                this.treeViewPlays.Nodes.Add(playBookNode);
            }
            this.treeViewPlays.ExpandAll();

            if (this.treeViewPlays.Nodes.Count > 0)
            {
                this.treeViewPlays.SelectedNode = this.treeViewFormations.Nodes[0];
            }
            this.treeViewPlays.SelectedNode = null;
        }


        private void C_BtnOpenFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog C_FolderBrowserDlg = new FolderBrowserDialog();

            C_FolderBrowserDlg.SelectedPath = this.C_TextFolder.Text;

            if (C_FolderBrowserDlg.ShowDialog() == DialogResult.OK)
            {
                string folderPath = C_FolderBrowserDlg.SelectedPath;

                this.ClearAllList();

                this.C_TextFolder.Text = folderPath;

                this.LoadFilesName(folderPath);
            }
        }


        private void AddFormations()
        {
            TreeNode selectNode = this.treeViewFormations.SelectedNode;   // 09-26-2011 Scott

            if (selectNode == null) return;

            AddFormationsOrPlays(selectNode, this.C_ListSelectedGames);

            if (selectNode.Parent != null)
            {
                selectNode.Parent.Nodes.Remove(selectNode);
            }
            else
            {
                selectNode.Nodes.Clear();
            }
            this.treeViewFormations.ExpandAll();

        }

        private void AddPlays()
        {
            TreeNode selectNode = this.treeViewPlays.SelectedNode;

            if (selectNode == null) return;

            AddFormationsOrPlays(selectNode, this.C_ListSelectedPlays);

            if (selectNode.Parent != null)
            {
                selectNode.Parent.Nodes.Remove(selectNode);
            }
            else
            {
                selectNode.Nodes.Clear();
            }
            this.treeViewPlays.ExpandAll();
        }

        private void AddFormationsOrPlays(TreeNode node, ListBox lstBox)
        {
            if (node.Tag is DirectoryInfo)
            {
                foreach (TreeNode treeNode in node.Nodes)
                {
                    AddFormationsOrPlays(treeNode, lstBox);
                }
            }
            else if (node.Tag is PlayBookFileDescription)
            {
                PlayBookFileDescription playbookFileDescription = node.Tag as PlayBookFileDescription;

                playbookFileDescription = new PlayBookFileDescription(node.FullPath, playbookFileDescription.FileName);

                lstBox.Items.Add(playbookFileDescription);

                foreach (TreeNode treeNode in node.Nodes)
                {
                    AddFormationsOrPlays(treeNode, lstBox);
                }
            }
        }

        private void RemoveFormations()
        {
            ArrayList arrSel = new ArrayList();

            foreach (object item in this.C_ListSelectedGames.SelectedItems)
            {
                arrSel.Add(item);
            }

            foreach (object item in arrSel)
            {
                this.C_ListSelectedGames.Items.Remove(item);
            }

            this.treeViewFormations.Nodes.Clear();

            string strDirectory = this.C_TextFolder.Text;

            if (!strDirectory.EndsWith(@"\")) strDirectory = strDirectory + @"\";

            strDirectory = strDirectory + this.cmbScoutType.Text + @"\";

            this.LoadFormationsName(strDirectory);
        }

        private void RemovePlays()
        {
            ArrayList arrSel = new ArrayList();

            foreach (object item in this.C_ListSelectedPlays.SelectedItems)
            {
                arrSel.Add(item);
            }

            foreach (object item in arrSel)
            {
                this.C_ListSelectedPlays.Items.Remove(item);
            }
            this.treeViewPlays.Nodes.Clear();

            string strDirectory = this.C_TextFolder.Text;

            if (!strDirectory.EndsWith(@"\")) strDirectory = strDirectory + @"\";

            strDirectory = strDirectory + this.cmbScoutType.Text + @"\";

            this.LoadPlaysName(strDirectory);
        }

        private void C_BtnAdd_Click(object sender, EventArgs e)
        {
            AddFormations();
        }

        private void C_BtnAddEdl_Click(object sender, EventArgs e)
        {
            AddPlays();
        }

        private void C_BtnDelete_Click(object sender, EventArgs e)
        {
            RemoveFormations();
        }

        private void C_BtnDelEdl_Click(object sender, EventArgs e)
        {
            RemovePlays();

        }

        private void cmbScoutType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string folderPath = this.C_TextFolder.Text;

            this.ClearAllList();

            this.LoadFilesName(folderPath);
        }

        private void treeViewFormations_DoubleClick(object sender, EventArgs e)
        {
            AddFormations();
        }

        private void treeViewPlays_DoubleClick(object sender, EventArgs e)
        {
            AddPlays();
        }
    }
}
