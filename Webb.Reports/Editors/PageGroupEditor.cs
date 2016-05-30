using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using DevExpress.XtraPrinting;
using System.Drawing.Drawing2D;

using Webb.Reports;

namespace Webb.Reports.Editors
{
	public class PageGroupEditorForm :System.Windows.Forms.Form
	{
		System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button C_NewGroup;
	
		private System.Windows.Forms.Button C_Remove;
		
		private System.Windows.Forms.ImageList C_ImageList;
	

		private System.Windows.Forms.Button C_ChangeGroup;
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.TreeView C_GroupInfoTree;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.PropertyGrid C_PropertyGrid;
		private System.Windows.Forms.Button BtnCancel;
		private System.Windows.Forms.Button BtnOk;
		private System.Windows.Forms.Button BtnReset;
		private System.Windows.Forms.CheckBox ChkRepeat;

		public  PageGroupInfo rootPageGroup=null;
 
		public PageGroupEditorForm(object value)
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			PageGroupInfo groupInfo=value as PageGroupInfo;

			this.rootPageGroup=groupInfo;	

			this.ChkRepeat.Checked=groupInfo.Repeat;

			this.ChkRepeat.Text=groupInfo.RepeatTitle;

			if(groupInfo.RepeatTitle=="RepeatControls")
			{
               ChkRepeat.Visible=false;
			}

			this.SetView(groupInfo);

			this.Text=groupInfo.RepeatTitle+" Setting";			
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PageGroupEditorForm));
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Group");
            this.panel1 = new System.Windows.Forms.Panel();
            this.ChkRepeat = new System.Windows.Forms.CheckBox();
            this.BtnReset = new System.Windows.Forms.Button();
            this.C_ChangeGroup = new System.Windows.Forms.Button();
            this.C_Remove = new System.Windows.Forms.Button();
            this.C_NewGroup = new System.Windows.Forms.Button();
            this.C_ImageList = new System.Windows.Forms.ImageList(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.BtnOk = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.C_PropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.C_GroupInfoTree = new System.Windows.Forms.TreeView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ChkRepeat);
            this.panel1.Controls.Add(this.BtnReset);
            this.panel1.Controls.Add(this.C_ChangeGroup);
            this.panel1.Controls.Add(this.C_Remove);
            this.panel1.Controls.Add(this.C_NewGroup);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(609, 43);
            this.panel1.TabIndex = 0;
            // 
            // ChkRepeat
            // 
            this.ChkRepeat.Location = new System.Drawing.Point(10, 9);
            this.ChkRepeat.Name = "ChkRepeat";
            this.ChkRepeat.Size = new System.Drawing.Size(153, 25);
            this.ChkRepeat.TabIndex = 11;
            this.ChkRepeat.Text = "OneValuePerPage";
            // 
            // BtnReset
            // 
            this.BtnReset.Location = new System.Drawing.Point(533, 9);
            this.BtnReset.Name = "BtnReset";
            this.BtnReset.Size = new System.Drawing.Size(67, 25);
            this.BtnReset.TabIndex = 10;
            this.BtnReset.Text = "Reset";
            this.BtnReset.Click += new System.EventHandler(this.BtnReset_Click);
            // 
            // C_ChangeGroup
            // 
            this.C_ChangeGroup.Location = new System.Drawing.Point(312, 9);
            this.C_ChangeGroup.Name = "C_ChangeGroup";
            this.C_ChangeGroup.Size = new System.Drawing.Size(105, 25);
            this.C_ChangeGroup.TabIndex = 9;
            this.C_ChangeGroup.Text = "Change Group";
            this.C_ChangeGroup.Click += new System.EventHandler(this.C_ChangeGroup_Click);
            // 
            // C_Remove
            // 
            this.C_Remove.BackColor = System.Drawing.SystemColors.Control;
            this.C_Remove.Location = new System.Drawing.Point(427, 9);
            this.C_Remove.Name = "C_Remove";
            this.C_Remove.Size = new System.Drawing.Size(96, 24);
            this.C_Remove.TabIndex = 1;
            this.C_Remove.Text = "Remove Item";
            this.C_Remove.UseVisualStyleBackColor = false;
            this.C_Remove.Click += new System.EventHandler(this.Menu_RemoveGroup_Click);
            // 
            // C_NewGroup
            // 
            this.C_NewGroup.BackColor = System.Drawing.SystemColors.Control;
            this.C_NewGroup.Location = new System.Drawing.Point(206, 9);
            this.C_NewGroup.Name = "C_NewGroup";
            this.C_NewGroup.Size = new System.Drawing.Size(96, 24);
            this.C_NewGroup.TabIndex = 0;
            this.C_NewGroup.Text = "Add Group";
            this.C_NewGroup.UseVisualStyleBackColor = false;
            this.C_NewGroup.Click += new System.EventHandler(this.Menu_AddGroup_Click);
            // 
            // C_ImageList
            // 
            this.C_ImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("C_ImageList.ImageStream")));
            this.C_ImageList.TransparentColor = System.Drawing.Color.White;
            this.C_ImageList.Images.SetKeyName(0, "");
            this.C_ImageList.Images.SetKeyName(1, "");
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.BtnCancel);
            this.panel2.Controls.Add(this.BtnOk);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 331);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(609, 51);
            this.panel2.TabIndex = 4;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Location = new System.Drawing.Point(496, 15);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(87, 26);
            this.BtnCancel.TabIndex = 1;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnOk
            // 
            this.BtnOk.Location = new System.Drawing.Point(362, 15);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(96, 26);
            this.BtnOk.TabIndex = 0;
            this.BtnOk.Text = "OK";
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.C_PropertyGrid);
            this.panel3.Controls.Add(this.splitter1);
            this.panel3.Controls.Add(this.C_GroupInfoTree);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 43);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(609, 288);
            this.panel3.TabIndex = 5;
            // 
            // C_PropertyGrid
            // 
            this.C_PropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.C_PropertyGrid.LineColor = System.Drawing.SystemColors.Control;
            this.C_PropertyGrid.Location = new System.Drawing.Point(346, 0);
            this.C_PropertyGrid.Name = "C_PropertyGrid";
            this.C_PropertyGrid.Size = new System.Drawing.Size(263, 288);
            this.C_PropertyGrid.TabIndex = 10;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(336, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(10, 288);
            this.splitter1.TabIndex = 9;
            this.splitter1.TabStop = false;
            // 
            // C_GroupInfoTree
            // 
            this.C_GroupInfoTree.AllowDrop = true;
            this.C_GroupInfoTree.Dock = System.Windows.Forms.DockStyle.Left;
            this.C_GroupInfoTree.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.C_GroupInfoTree.ForeColor = System.Drawing.Color.Black;
            this.C_GroupInfoTree.ImageIndex = 0;
            this.C_GroupInfoTree.ImageList = this.C_ImageList;
            this.C_GroupInfoTree.Location = new System.Drawing.Point(0, 0);
            this.C_GroupInfoTree.Name = "C_GroupInfoTree";
            treeNode2.Name = "";
            treeNode2.Text = "Group";
            this.C_GroupInfoTree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2});
            this.C_GroupInfoTree.SelectedImageIndex = 0;
            this.C_GroupInfoTree.Size = new System.Drawing.Size(336, 288);
            this.C_GroupInfoTree.TabIndex = 8;
            this.C_GroupInfoTree.DragDrop += new System.Windows.Forms.DragEventHandler(this.C_GroupInfoTree_DragDrop);
            this.C_GroupInfoTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.C_GroupInfoTree_AfterSelect);
            this.C_GroupInfoTree.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.C_GroupInfoTree_BeforeSelect);
            this.C_GroupInfoTree.DragOver += new System.Windows.Forms.DragEventHandler(this.C_GroupInfoTree_DragOver);
            this.C_GroupInfoTree.Click += new System.EventHandler(this.C_GroupInfoTree_Click);
            // 
            // PageGroupEditorForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(609, 382);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "PageGroupEditorForm";
            this.Text = "Group Setting";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void C_GroupInfoTree_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			this.C_PropertyGrid.SelectedObject = this.C_GroupInfoTree.SelectedNode.Tag;
		}
		
		private void SetView(PageGroupInfo groupInfo, TreeNode parentNode)
		{
			if(groupInfo == null) return;

			TreeNode node = this.CreateGroupNode(groupInfo);
			
			foreach(PageGroupInfo subGroupInfo in groupInfo.SubPageGroupInfos)
			{
				this.SetView(subGroupInfo,node);
			}

			if(parentNode == null)
			{
				this.C_GroupInfoTree.Nodes.Clear();

				this.C_GroupInfoTree.Nodes.Add(node);
			}
			else
			{
				parentNode.Nodes.Add(node);
			}
		}

		public void SetView(PageGroupInfo groupInfo)
		{
			this.C_GroupInfoTree.Nodes.Clear();           		
			
			this.SetView(groupInfo,null);

			if(this.C_GroupInfoTree.Nodes.Count > 0)
			{
				this.C_PropertyGrid.SelectedObject = this.C_GroupInfoTree.Nodes[0].Tag;
			}
		}

		private TreeNode CreateGroupNode(PageGroupInfo groupInfo)
		{
			TreeNode node = new TreeNode(groupInfo.ToString());

			node.Tag = groupInfo.Copy();

			node.ImageIndex = 0;

			node.SelectedImageIndex = 1;

			return node;
		}


		private void UpdateView(PageGroupInfo groupInfo, TreeNode node)
		{
			if(groupInfo == null || node == null) return;	

			groupInfo.SubPageGroupInfos.Clear();

			for(int index = 0; index < node.Nodes.Count; index++)
			{
				TreeNode subNode = node.Nodes[index];
				
				if(subNode.Tag is PageGroupInfo)
				{
					groupInfo.SubPageGroupInfos.Add(subNode.Tag as PageGroupInfo);

					this.UpdateView(subNode.Tag as PageGroupInfo,subNode);
				}
			}
		}


		public void UpdateView()
		{
			if(this.C_GroupInfoTree.Nodes.Count<=0) return;  			
			
			this.rootPageGroup = this.C_GroupInfoTree.Nodes[0].Tag as PageGroupInfo; 

			rootPageGroup.Repeat=this.ChkRepeat.Checked;
			
			this.UpdateView(rootPageGroup,this.C_GroupInfoTree.Nodes[0]);
			
		}

		
		private void Menu_AddGroup_Click(object sender, System.EventArgs e)
		{
			TreeNode node = this.C_GroupInfoTree.SelectedNode;

			if(node == null) return;		

			this.AddGroup(node);
		}	
		
		
		private void Menu_RemoveGroup_Click(object sender, System.EventArgs e)
		{
			if(this.C_GroupInfoTree.SelectedNode==null) return;
			
			if(this.C_GroupInfoTree.SelectedNode==this.C_GroupInfoTree.Nodes[0])
			{
				Webb.Utilities.MessageBoxEx.ShowMessage("Cannot remove the root group.");

				return;
			}
			this.C_GroupInfoTree.SelectedNode.Remove();

			this.C_GroupInfoTree.Focus();
		}

		private void Menu_MoveUp_Click(object sender, System.EventArgs e)
		{
			TreeNode m_SelectedNode = this.C_GroupInfoTree.SelectedNode;

			if(m_SelectedNode == null) return;			
			
			int m_Index = m_SelectedNode.Index;

			if(m_Index>=1)
			{
				TreeNodeCollection m_Nodes = null;
				
				if(m_SelectedNode.Parent==null)
				{
					m_Nodes= this.C_GroupInfoTree.Nodes;
				}
				else
				{
					m_Nodes = m_SelectedNode.Parent.Nodes;		
				}

				TreeNode preNode = m_Nodes[m_Index - 1];				
			
				m_SelectedNode.Remove();
				
				m_Nodes.Insert(m_Index-1,m_SelectedNode);
			}
			
			this.C_GroupInfoTree.SelectedNode = m_SelectedNode;
			
			this.C_GroupInfoTree.Focus();
			
		}

		
		private void AddGroup(TreeNode node)
		{
			if(!(node.Tag is PageGroupInfo)) return;

			string strField=Webb.Data.PublicDBFieldConverter.AvialableFields[0].ToString();

			PageFieldInfo m_GroupInfo = new PageFieldInfo(strField);

			TreeNode m_Node = this.CreateGroupNode(m_GroupInfo);
			
			node.Nodes.Add(m_Node);
		}

	
		private void C_GroupInfoTree_BeforeSelect(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
		{
			e.Node.Text = e.Node.Tag.ToString();
		}

		private void C_GroupInfoTree_Click(object sender, System.EventArgs e)
		{
			if(this.C_GroupInfoTree.SelectedNode!=null)
			{
				this.C_GroupInfoTree.SelectedNode.Text = this.C_GroupInfoTree.SelectedNode.Tag.ToString();
			}
		}

		private void GroupData_Changed(object sender, EventArgs e)
		{
			this.C_PropertyGrid.Refresh();
		}

		private void C_GroupInfoTree_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
		{
			// Retrieve the client coordinates of the mouse position.
			Point targetPoint = this.C_GroupInfoTree.PointToClient(new Point(e.X, e.Y));

			// Select the node at the mouse position.
			this.C_GroupInfoTree.SelectedNode = this.C_GroupInfoTree.GetNodeAt(targetPoint);
		}

		private void C_GroupInfoTree_ItemDrag(object sender, System.Windows.Forms.ItemDragEventArgs e)
		{
			// Move the dragged node when the left mouse button is used.
			if (e.Button == MouseButtons.Left)
			{
				DoDragDrop(e.Item, DragDropEffects.Move);
			}
				// Copy the dragged node when the right mouse button is used.
			else if (e.Button == MouseButtons.Right)
			{
				DoDragDrop(e.Item, DragDropEffects.Copy);
			}
		}

		private void C_GroupInfoTree_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
		{
			// Retrieve the client coordinates of the drop location.
			Point targetPoint = this.C_GroupInfoTree.PointToClient(new Point(e.X, e.Y));

			// Retrieve the node at the drop location.
			TreeNode targetNode = this.C_GroupInfoTree.GetNodeAt(targetPoint);

			// Retrieve the node that was dragged.
			TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));

			// Confirm that the node at the drop location is not 
			// the dragged node or a descendant of the dragged node.
			if (!draggedNode.Equals(targetNode) && !ContainsNode(draggedNode, targetNode) )
			{
				// If it is a move operation, remove the node from its current 
				// location and add it to the node at the drop location.
				if (e.Effect == DragDropEffects.Move)
				{
					draggedNode.Remove();

					this.AddNode(draggedNode,targetNode);
				}
				// If it is a copy operation, clone the dragged node 
				// and add it to the node at the drop location.
			
				// Expand the node at the location 
				// to show the dropped node.
				targetNode.Expand();
			}
		}

		// Determine whether one node is a parent 
		// or ancestor of a second node.
		private bool ContainsNode(TreeNode draggedNode, TreeNode targetNode)
		{
			// Check the parent node of the second node.
			if (targetNode.Parent == null) return false;
			if (targetNode.Parent.Equals(draggedNode)) return true;

			// If the parent node is not null or equal to the first node, 
			// call the ContainsNode method recursively using the parent of 
			// the second node.
			return ContainsNode(draggedNode, targetNode.Parent);
		}

		private void AddNode(TreeNode draggedNode, TreeNode targetNode)
		{
			if(draggedNode.Tag is PageGroupInfo)
			{
				targetNode.Nodes.Add(draggedNode);
			}
		}
	

		private void C_GroupInfoTree_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{//07-15-2008@Scott
			switch(e.KeyCode)
			{
				case Keys.Delete:
					this.Menu_RemoveGroup_Click(null,null);
					break;
				default:
					break;
			}
		}

	
		private void C_ChangeGroup_Click(object sender, System.EventArgs e)
		{
			TreeNode node = this.C_GroupInfoTree.SelectedNode;

			if(node == null||!(node.Tag is PageGroupInfo)) return;			
            
			if(node.Tag is PageFieldInfo)
			{				
				PageSectionInfo m_GroupInfo = new PageSectionInfo();

				m_GroupInfo.SectionFilterWrapper=new SectionFilterCollectionWrapper();				

				node.Tag=m_GroupInfo.Copy();	
				
				node.Text=m_GroupInfo.ToString();			
			}
			else
			{
				string strField=Webb.Data.PublicDBFieldConverter.AvialableFields[0].ToString();

				PageFieldInfo m_GroupInfo = new PageFieldInfo(strField);			    

				node.Tag=m_GroupInfo.Copy();

				node.Text=m_GroupInfo.ToString();		
			}		
			

			this.C_PropertyGrid.Refresh();
			
			this.C_GroupInfoTree.Focus();

			this.C_PropertyGrid.SelectedObject=node.Tag;

		}

		private void C_MoveDown_Click(object sender, System.EventArgs e)
		{
			TreeNode m_SelectedNode = this.C_GroupInfoTree.SelectedNode;

			if(m_SelectedNode == null) return;
			
			int m_Index = m_SelectedNode.Index;

			TreeNodeCollection m_Nodes = null;
			
			if(m_SelectedNode.Parent==null)
			{
				m_Nodes= this.C_GroupInfoTree.Nodes;
			}
			else
			{
				m_Nodes = m_SelectedNode.Parent.Nodes;		
			}
			
			if(m_Index<m_Nodes.Count-1)
			{
				TreeNode nextNode = m_Nodes[m_Index + 1];	
				
				m_SelectedNode.Remove();
				
				m_Nodes.Insert(m_Index+1,m_SelectedNode);
			}
			
			this.C_GroupInfoTree.SelectedNode = m_SelectedNode;
			
			this.C_GroupInfoTree.Focus();
			
		}

		private void BtnOk_Click(object sender, System.EventArgs e)
		{
			this.UpdateView();
			DialogResult=DialogResult.OK;
		}

		private void BtnCancel_Click(object sender, System.EventArgs e)
		{
			DialogResult=DialogResult.Cancel;
		}

		private void BtnReset_Click(object sender, System.EventArgs e)
		{
			this.SetView(this.rootPageGroup);
			this.C_GroupInfoTree.Refresh();
		}		
		
	}
	#region class  PageGroupEditor: System.Drawing.Design.UITypeEditor
	public class PageGroupEditor : System.Drawing.Design.UITypeEditor
	{
		[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
		public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}

		// Displays the UI for value selection.
		[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
		public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, System.IServiceProvider provider, object value)
		{
			if (value==null||!(value is Webb.Reports.PageGroupInfo))
				return value;

			IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

			PageGroupEditorForm webbReportForm = new PageGroupEditorForm(value);

			if (edSvc != null)
			{
				if (edSvc.ShowDialog(webbReportForm) == DialogResult.OK)
				{
					return webbReportForm.rootPageGroup;
				}
			}
			return value;
		}

		[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
		public override void PaintValue(System.Drawing.Design.PaintValueEventArgs e)
		{
			
		}

		[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
		public override bool GetPaintValueSupported(System.ComponentModel.ITypeDescriptorContext context)
		{
			return false;
		}
	}
	#endregion
}
