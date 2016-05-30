using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using System.Collections.Specialized;

namespace Webb.Reports.DataProvider.UI
{
	public class ReportTemplateSelector : System.Windows.Forms.Form
	{
		private StringCollection _ReportTemplates;
		private FolderBrowserDialog _FolderBrowserDialog;

		private System.Windows.Forms.GroupBox C_GroupBox;
		private System.Windows.Forms.TextBox C_TextDirectory;
		private System.Windows.Forms.Button C_BtnBrowse;
		private System.Windows.Forms.TreeView C_TreeRepTemplates;
		private System.Windows.Forms.Button C_BtnOK;
		private System.Windows.Forms.Button C_BtnCancel;
		private System.Windows.Forms.ImageList C_ImageList;
		private System.ComponentModel.IContainer components = null;

		public ReportTemplateSelector()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
			this._FolderBrowserDialog = new FolderBrowserDialog();

			this._ReportTemplates = new StringCollection();
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ReportTemplateSelector));
			this.C_GroupBox = new System.Windows.Forms.GroupBox();
			this.C_BtnBrowse = new System.Windows.Forms.Button();
			this.C_TextDirectory = new System.Windows.Forms.TextBox();
			this.C_TreeRepTemplates = new System.Windows.Forms.TreeView();
			this.C_ImageList = new System.Windows.Forms.ImageList(this.components);
			this.C_BtnOK = new System.Windows.Forms.Button();
			this.C_BtnCancel = new System.Windows.Forms.Button();
			this.C_GroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// C_GroupBox
			// 
			this.C_GroupBox.BackColor = System.Drawing.SystemColors.Control;
			this.C_GroupBox.Controls.Add(this.C_BtnBrowse);
			this.C_GroupBox.Controls.Add(this.C_TextDirectory);
			this.C_GroupBox.Controls.Add(this.C_TreeRepTemplates);
			this.C_GroupBox.Location = new System.Drawing.Point(8, 5);
			this.C_GroupBox.Name = "C_GroupBox";
			this.C_GroupBox.Size = new System.Drawing.Size(544, 331);
			this.C_GroupBox.TabIndex = 0;
			this.C_GroupBox.TabStop = false;
			this.C_GroupBox.Text = "Select Report Templates folder";
			// 
			// C_BtnBrowse
			// 
			this.C_BtnBrowse.Location = new System.Drawing.Point(456, 24);
			this.C_BtnBrowse.Name = "C_BtnBrowse";
			this.C_BtnBrowse.TabIndex = 2;
			this.C_BtnBrowse.Text = "Browse...";
			this.C_BtnBrowse.Click += new System.EventHandler(this.C_BtnBrowse_Click);
			// 
			// C_TextDirectory
			// 
			this.C_TextDirectory.Enabled = false;
			this.C_TextDirectory.Location = new System.Drawing.Point(8, 24);
			this.C_TextDirectory.Name = "C_TextDirectory";
			this.C_TextDirectory.Size = new System.Drawing.Size(440, 20);
			this.C_TextDirectory.TabIndex = 1;
			this.C_TextDirectory.Text = "";
			// 
			// C_TreeRepTemplates
			// 
			this.C_TreeRepTemplates.CheckBoxes = true;
			this.C_TreeRepTemplates.ImageList = this.C_ImageList;
			this.C_TreeRepTemplates.Location = new System.Drawing.Point(8, 56);
			this.C_TreeRepTemplates.Name = "C_TreeRepTemplates";
			this.C_TreeRepTemplates.Size = new System.Drawing.Size(528, 264);
			this.C_TreeRepTemplates.TabIndex = 0;
			this.C_TreeRepTemplates.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.C_TreeRepTemplates_AfterExpand);
			this.C_TreeRepTemplates.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.C_TreeRepTemplates_AfterCollapse);
			// 
			// C_ImageList
			// 
			this.C_ImageList.ImageSize = new System.Drawing.Size(16, 16);
			this.C_ImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("C_ImageList.ImageStream")));
			this.C_ImageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// C_BtnOK
			// 
			this.C_BtnOK.Location = new System.Drawing.Point(384, 344);
			this.C_BtnOK.Name = "C_BtnOK";
			this.C_BtnOK.TabIndex = 2;
			this.C_BtnOK.Text = "OK";
			this.C_BtnOK.Click += new System.EventHandler(this.C_BtnOK_Click);
			// 
			// C_BtnCancel
			// 
			this.C_BtnCancel.Location = new System.Drawing.Point(464, 344);
			this.C_BtnCancel.Name = "C_BtnCancel";
			this.C_BtnCancel.TabIndex = 2;
			this.C_BtnCancel.Text = "Cancel";
			this.C_BtnCancel.Click += new System.EventHandler(this.C_BtnCancel_Click);
			// 
			// ReportTemplateSelector
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(554, 375);
			this.Controls.Add(this.C_GroupBox);
			this.Controls.Add(this.C_BtnOK);
			this.Controls.Add(this.C_BtnCancel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "ReportTemplateSelector";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Select Report Templates";
			this.C_GroupBox.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void C_BtnBrowse_Click(object sender, System.EventArgs e)
		{
			if(this._FolderBrowserDialog.ShowDialog(this) == DialogResult.OK)
			{
				this.C_TextDirectory.Text = this._FolderBrowserDialog.SelectedPath;

				this.C_TreeRepTemplates.Nodes.Clear();

				this.LoadTemplates(this._FolderBrowserDialog.SelectedPath,null);
			}
		}

		private TreeNode AddFolder(TreeNode parentNode,string strFolder)
		{ 
			int index = strFolder.LastIndexOf(@"\");

			TreeNode node = new TreeNode(strFolder.Remove(0,index + 1));

			node.Tag = strFolder;

			node.ImageIndex = (int)NodeTypes.Folder;

			node.SelectedImageIndex = (int)NodeTypes.Folder;

			if(parentNode == null)
			{
				this.C_TreeRepTemplates.Nodes.Add(node);
			}
			else
			{
				parentNode.Nodes.Add(node);
			}

			return node;
		}

		private TreeNode AddTemplate(TreeNode parentNode,string strTemplateName)
		{
			int index = strTemplateName.LastIndexOf(@"\");

			TreeNode node = new TreeNode(strTemplateName.Remove(0,index + 1));

			node.Tag = strTemplateName;

			node.ImageIndex = (int)NodeTypes.Template;

			node.SelectedImageIndex = (int)NodeTypes.Template;

			if(parentNode == null)
			{
				this.C_TreeRepTemplates.Nodes.Add(node);
			}
			else
			{
				parentNode.Nodes.Add(node);
			}

			return node;
		}

		private void LoadTemplates(string strDirectory, TreeNode parentNode)
		{	
			this.LoadFilesName(strDirectory,parentNode);
			
			string[] arrDirectories = System.IO.Directory.GetDirectories(strDirectory);

			foreach(string strDir in arrDirectories)
			{
				TreeNode root = this.AddFolder(parentNode,strDir);

				this.LoadTemplates(strDir,root);
			}
		}

		private void LoadFilesName(string strDirectory,TreeNode parentNode)
		{
			string[] arrFiles = System.IO.Directory.GetFiles(strDirectory,"*.repx");
			
			foreach(string strFile in arrFiles)
			{
				this.AddTemplate(parentNode,strFile);
			}
		}

		private void C_BtnOK_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
		
			this.Close();
		}

		private void C_BtnCancel_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;

			this.Close();
		}

		private void GetSelectedTemplates(StringCollection templates,TreeNode parentNode)
		{
			TreeNodeCollection nodes = null;

			if(parentNode == null) 
			{
				nodes = this.C_TreeRepTemplates.Nodes; 
			}
			else
			{
				nodes = parentNode.Nodes;
			}

			foreach(TreeNode node in nodes)
			{
				if(node.Nodes != null && node.Nodes.Count > 0)
				{
					GetSelectedTemplates(templates,node);
				}
				else
				{
					if(node.ImageIndex == (int)NodeTypes.Template && node.Checked == true) 
					{
						templates.Add(node.Tag.ToString());
					}
				}
			}
		}

		private void C_TreeRepTemplates_AfterCollapse(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			e.Node.ImageIndex = (int)NodeTypes.Folder;
		}

		private void C_TreeRepTemplates_AfterExpand(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			e.Node.ImageIndex = (int)NodeTypes.Folder + 1;
		}

		public void GetSelectedTemplates(StringCollection templates)
		{
			templates.Clear();

			this.GetSelectedTemplates(templates,null);
		}
	}

	public enum NodeTypes
	{
		Template = 0,
		Folder,
	}
}

