using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Webb.Reports.ExControls;
using Webb.Reports.ExControls.Data;
using Webb.Reports.ExControls.Views;

namespace Webb.Reports.ExControls.UI
{
	public class ConfigGridGroupInfo : Webb.Reports.ExControls.UI.ExControlDesignerControlBase
	{
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button C_NewGroup;
		private System.Windows.Forms.PropertyGrid C_PropertyGrid;
		private System.Windows.Forms.TreeView C_GroupInfoTree;
		private System.Windows.Forms.ContextMenu C_Menu;
		private System.Windows.Forms.MenuItem Menu_AddGroup;
		private System.Windows.Forms.MenuItem Menu_RemoveGroup;
		private System.Windows.Forms.MenuItem Menu_MoveUp;
		private System.Windows.Forms.MenuItem Menu_MoveDown;
		private System.Windows.Forms.Button C_MoveUp;
		private System.Windows.Forms.Button C_MoveDown;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.MenuItem Menu_AddSummary;
		private System.Windows.Forms.Button C_AddSummary;
        private System.Windows.Forms.Button C_Remove;
		private System.Windows.Forms.MenuItem Menu_CopySummary;
		private System.Windows.Forms.ImageList C_ImageList;
        private Button BtnChangeGroup;
        private ContextMenuStrip C_AddGroupMenu;
        private ToolStripMenuItem addFieldGroupMenuItem;
        private ToolStripMenuItem addSectionGroupMenuItem;
        private ToolStripMenuItem addCombinedMenuItem;
		private System.ComponentModel.IContainer components = null;

		public ConfigGridGroupInfo():base()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			this.Load += new EventHandler(ConfigGroupingInfo_Load);
			// TODO: Add any initialization after the InitializeComponent call
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Group");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigGridGroupInfo));
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnChangeGroup = new System.Windows.Forms.Button();
            this.C_AddSummary = new System.Windows.Forms.Button();
            this.C_MoveDown = new System.Windows.Forms.Button();
            this.C_MoveUp = new System.Windows.Forms.Button();
            this.C_Remove = new System.Windows.Forms.Button();
            this.C_NewGroup = new System.Windows.Forms.Button();
            this.C_PropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.C_GroupInfoTree = new System.Windows.Forms.TreeView();
            this.C_Menu = new System.Windows.Forms.ContextMenu();
            this.Menu_AddGroup = new System.Windows.Forms.MenuItem();
            this.Menu_AddSummary = new System.Windows.Forms.MenuItem();
            this.Menu_CopySummary = new System.Windows.Forms.MenuItem();
            this.Menu_RemoveGroup = new System.Windows.Forms.MenuItem();
            this.Menu_MoveUp = new System.Windows.Forms.MenuItem();
            this.Menu_MoveDown = new System.Windows.Forms.MenuItem();
            this.C_ImageList = new System.Windows.Forms.ImageList(this.components);
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.C_AddGroupMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addFieldGroupMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addSectionGroupMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addCombinedMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.C_AddGroupMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.BtnChangeGroup);
            this.panel1.Controls.Add(this.C_AddSummary);
            this.panel1.Controls.Add(this.C_MoveDown);
            this.panel1.Controls.Add(this.C_MoveUp);
            this.panel1.Controls.Add(this.C_Remove);
            this.panel1.Controls.Add(this.C_NewGroup);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(680, 40);
            this.panel1.TabIndex = 0;
            // 
            // BtnChangeGroup
            // 
            this.BtnChangeGroup.Location = new System.Drawing.Point(101, 8);
            this.BtnChangeGroup.Name = "BtnChangeGroup";
            this.BtnChangeGroup.Size = new System.Drawing.Size(112, 23);
            this.BtnChangeGroup.TabIndex = 5;
            this.BtnChangeGroup.Text = "Change Group";
            this.BtnChangeGroup.UseVisualStyleBackColor = true;
            this.BtnChangeGroup.Click += new System.EventHandler(this.BtnChangeGroup_Click);
            // 
            // C_AddSummary
            // 
            this.C_AddSummary.BackColor = System.Drawing.SystemColors.Control;
            this.C_AddSummary.Location = new System.Drawing.Point(219, 8);
            this.C_AddSummary.Name = "C_AddSummary";
            this.C_AddSummary.Size = new System.Drawing.Size(105, 23);
            this.C_AddSummary.TabIndex = 4;
            this.C_AddSummary.Text = "Add Summary";
            this.C_AddSummary.UseVisualStyleBackColor = false;
            this.C_AddSummary.Click += new System.EventHandler(this.Menu_AddSummary_Click);
            // 
            // C_MoveDown
            // 
            this.C_MoveDown.BackColor = System.Drawing.SystemColors.Control;
            this.C_MoveDown.Location = new System.Drawing.Point(533, 8);
            this.C_MoveDown.Name = "C_MoveDown";
            this.C_MoveDown.Size = new System.Drawing.Size(90, 23);
            this.C_MoveDown.TabIndex = 3;
            this.C_MoveDown.Text = "Move Down";
            this.C_MoveDown.UseVisualStyleBackColor = false;
            this.C_MoveDown.Click += new System.EventHandler(this.Menu_MoveDown_Click);
            // 
            // C_MoveUp
            // 
            this.C_MoveUp.BackColor = System.Drawing.SystemColors.Control;
            this.C_MoveUp.Location = new System.Drawing.Point(445, 8);
            this.C_MoveUp.Name = "C_MoveUp";
            this.C_MoveUp.Size = new System.Drawing.Size(70, 23);
            this.C_MoveUp.TabIndex = 2;
            this.C_MoveUp.Text = "Move Up";
            this.C_MoveUp.UseVisualStyleBackColor = false;
            this.C_MoveUp.Click += new System.EventHandler(this.Menu_MoveUp_Click);
            // 
            // C_Remove
            // 
            this.C_Remove.BackColor = System.Drawing.SystemColors.Control;
            this.C_Remove.Location = new System.Drawing.Point(327, 8);
            this.C_Remove.Name = "C_Remove";
            this.C_Remove.Size = new System.Drawing.Size(100, 23);
            this.C_Remove.TabIndex = 1;
            this.C_Remove.Text = "Remove Item";
            this.C_Remove.UseVisualStyleBackColor = false;
            this.C_Remove.Click += new System.EventHandler(this.Menu_RemoveGroup_Click);
            // 
            // C_NewGroup
            // 
            this.C_NewGroup.BackColor = System.Drawing.SystemColors.Control;
            this.C_NewGroup.Location = new System.Drawing.Point(8, 8);
            this.C_NewGroup.Name = "C_NewGroup";
            this.C_NewGroup.Size = new System.Drawing.Size(80, 23);
            this.C_NewGroup.TabIndex = 0;
            this.C_NewGroup.Text = "Add Group";
            this.C_NewGroup.UseVisualStyleBackColor = false;
            this.C_NewGroup.Click += new System.EventHandler(this.C_NewGroup_Click);
            // 
            // C_PropertyGrid
            // 
            this.C_PropertyGrid.Dock = System.Windows.Forms.DockStyle.Right;
            this.C_PropertyGrid.LineColor = System.Drawing.SystemColors.Control;
            this.C_PropertyGrid.Location = new System.Drawing.Point(400, 40);
            this.C_PropertyGrid.Name = "C_PropertyGrid";
            this.C_PropertyGrid.Size = new System.Drawing.Size(280, 324);
            this.C_PropertyGrid.TabIndex = 1;
            // 
            // C_GroupInfoTree
            // 
            this.C_GroupInfoTree.AllowDrop = true;
            this.C_GroupInfoTree.ContextMenu = this.C_Menu;
            this.C_GroupInfoTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.C_GroupInfoTree.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.C_GroupInfoTree.ForeColor = System.Drawing.Color.Black;
            this.C_GroupInfoTree.ImageIndex = 0;
            this.C_GroupInfoTree.ImageList = this.C_ImageList;
            this.C_GroupInfoTree.Location = new System.Drawing.Point(0, 40);
            this.C_GroupInfoTree.Name = "C_GroupInfoTree";
            treeNode1.Name = "";
            treeNode1.Text = "Group";
            this.C_GroupInfoTree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.C_GroupInfoTree.SelectedImageIndex = 0;
            this.C_GroupInfoTree.Size = new System.Drawing.Size(400, 324);
            this.C_GroupInfoTree.TabIndex = 2;
            this.C_GroupInfoTree.Enter += new System.EventHandler(this.C_GroupInfoTree_Click);
            this.C_GroupInfoTree.DragDrop += new System.Windows.Forms.DragEventHandler(this.C_GroupInfoTree_DragDrop);
            this.C_GroupInfoTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.C_GroupInfoTree_AfterSelect);
            this.C_GroupInfoTree.DragEnter += new System.Windows.Forms.DragEventHandler(this.C_GroupInfoTree_DragEnter);
            this.C_GroupInfoTree.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.C_GroupInfoTree_BeforeSelect);
            this.C_GroupInfoTree.KeyDown += new System.Windows.Forms.KeyEventHandler(this.C_GroupInfoTree_KeyDown);
            this.C_GroupInfoTree.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.C_GroupInfoTree_ItemDrag);
            this.C_GroupInfoTree.DragOver += new System.Windows.Forms.DragEventHandler(this.C_GroupInfoTree_DragOver);
            this.C_GroupInfoTree.Click += new System.EventHandler(this.C_GroupInfoTree_Click);
            // 
            // C_Menu
            // 
            this.C_Menu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.Menu_AddGroup,
            this.Menu_AddSummary,
            this.Menu_CopySummary,
            this.Menu_RemoveGroup,
            this.Menu_MoveUp,
            this.Menu_MoveDown});
            // 
            // Menu_AddGroup
            // 
            this.Menu_AddGroup.Index = 0;
            this.Menu_AddGroup.Text = "Add Group";
            this.Menu_AddGroup.Click += new System.EventHandler(this.Menu_AddGroup_Click);
            // 
            // Menu_AddSummary
            // 
            this.Menu_AddSummary.Index = 1;
            this.Menu_AddSummary.Text = "Add Summary";
            this.Menu_AddSummary.Click += new System.EventHandler(this.Menu_AddSummary_Click);
            // 
            // Menu_CopySummary
            // 
            this.Menu_CopySummary.Index = 2;
            this.Menu_CopySummary.Text = "Copy Summary";
            this.Menu_CopySummary.Click += new System.EventHandler(this.Menu_CopySummary_Click);
            // 
            // Menu_RemoveGroup
            // 
            this.Menu_RemoveGroup.Index = 3;
            this.Menu_RemoveGroup.Text = "Remove Item";
            this.Menu_RemoveGroup.Click += new System.EventHandler(this.Menu_RemoveGroup_Click);
            // 
            // Menu_MoveUp
            // 
            this.Menu_MoveUp.Index = 4;
            this.Menu_MoveUp.Text = "Move Up";
            this.Menu_MoveUp.Click += new System.EventHandler(this.Menu_MoveUp_Click);
            // 
            // Menu_MoveDown
            // 
            this.Menu_MoveDown.Index = 5;
            this.Menu_MoveDown.Text = "Move Down";
            this.Menu_MoveDown.Click += new System.EventHandler(this.Menu_MoveDown_Click);
            // 
            // C_ImageList
            // 
            this.C_ImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("C_ImageList.ImageStream")));
            this.C_ImageList.TransparentColor = System.Drawing.Color.White;
            this.C_ImageList.Images.SetKeyName(0, "");
            this.C_ImageList.Images.SetKeyName(1, "");
            this.C_ImageList.Images.SetKeyName(2, "");
            this.C_ImageList.Images.SetKeyName(3, "");
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(395, 40);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(5, 324);
            this.splitter1.TabIndex = 3;
            this.splitter1.TabStop = false;
            // 
            // C_AddGroupMenu
            // 
            this.C_AddGroupMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addFieldGroupMenuItem,
            this.addSectionGroupMenuItem,
            this.addCombinedMenuItem});
            this.C_AddGroupMenu.Name = "contextMenuStrip1";
            this.C_AddGroupMenu.Size = new System.Drawing.Size(215, 92);
            // 
            // addFieldGroupMenuItem
            // 
            this.addFieldGroupMenuItem.Name = "addFieldGroupMenuItem";
            this.addFieldGroupMenuItem.Size = new System.Drawing.Size(214, 22);
            this.addFieldGroupMenuItem.Text = "Add Field Group";
            this.addFieldGroupMenuItem.Click += new System.EventHandler(this.addFieldGroupMenuItem_Click);
            // 
            // addSectionGroupMenuItem
            // 
            this.addSectionGroupMenuItem.Name = "addSectionGroupMenuItem";
            this.addSectionGroupMenuItem.Size = new System.Drawing.Size(214, 22);
            this.addSectionGroupMenuItem.Text = "Add Section Group";
            this.addSectionGroupMenuItem.Click += new System.EventHandler(this.addSectionGroupMenuItem_Click);
            // 
            // addCombinedMenuItem
            // 
            this.addCombinedMenuItem.Name = "addCombinedMenuItem";
            this.addCombinedMenuItem.Size = new System.Drawing.Size(214, 22);
            this.addCombinedMenuItem.Text = "Add Compute Fields Group";
            this.addCombinedMenuItem.Click += new System.EventHandler(this.addCombinedValueGroupToolStripMenuItem_Click);
            // 
            // ConfigGridGroupInfo
            // 
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.C_GroupInfoTree);
            this.Controls.Add(this.C_PropertyGrid);
            this.Controls.Add(this.panel1);
            this.Name = "ConfigGridGroupInfo";
            this.Size = new System.Drawing.Size(680, 364);
            this.panel1.ResumeLayout(false);
            this.C_AddGroupMenu.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void C_GroupInfoTree_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			this.C_PropertyGrid.SelectedObject = this.C_GroupInfoTree.SelectedNode.Tag;
		}

		private void ConfigGroupingInfo_Load(object sender, EventArgs e)
		{
			if(this.C_GroupInfoTree.Nodes.Count > 0)
			{
				this.C_PropertyGrid.SelectedObject = this.C_GroupInfoTree.Nodes[0].Tag;
			}
		}

		private void SetView(GroupInfo groupInfo, TreeNode parentNode)
		{
			if(groupInfo == null) return;

			TreeNode node = this.CreateGroupNode(groupInfo);

			if(groupInfo.Summaries!=null)
			{
				foreach(GroupSummary summary in groupInfo.Summaries)
				{
					TreeNode summaryNode = this.CreateSummaryNode(summary);

					node.Nodes.Add(summaryNode);
				}
			}

			foreach(GroupInfo subGroupInfo in groupInfo.SubGroupInfos)
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

		public override void SetView(Webb.Reports.ExControls.Views.ExControlView i_View)
		{
			this.C_GroupInfoTree.Nodes.Clear();
			
			GridView m_GridView = i_View as GridView;

            int Start = m_GridView.ResolveIndentStartCol();

            m_GridView.ApplyVirtualGroupInfoWidth(ref Start, (i_View as GridView).RootGroupInfo, (i_View as GridView).ColumnsWidth);

            m_GridView.GridInfo.ApplyColumnsWidth((i_View as GridView).ColumnsWidth, Start);	//06-30-2008@Scott
		
			GroupInfo m_GroupInfo = null;

            if (m_GridView.RootGroupInfo is SectionGroupInfo && m_GridView.RootGroupInfo.IsSectionOutSide)
			{
				if(m_GridView.RootGroupInfo.SubGroupInfos.Count > 0)
				{
					m_GroupInfo = m_GridView.RootGroupInfo.SubGroupInfos[0];
				}
			}
			else
			{
				m_GroupInfo = m_GridView.RootGroupInfo;
			}


			this.SetView(m_GroupInfo,null);
		}

		private TreeNode CreateGroupNode(GroupInfo groupInfo)
		{
			TreeNode node = new TreeNode(groupInfo.ToString());

			node.Tag = groupInfo.Copy();

			node.ImageIndex = 0;

			node.SelectedImageIndex = 1;

			return node;
		}

		private TreeNode CreateSummaryNode(GroupSummary summary)
		{
			TreeNode node = new TreeNode(summary.ToString());

			node.Tag = summary.Copy();

			node.ImageIndex = 2;

			node.SelectedImageIndex = 3;

			return node;
		}

		private void UpdateView(GroupInfo groupInfo, TreeNode node)
		{
			if(groupInfo == null || node == null) return;

			if(groupInfo.Summaries != null) groupInfo.Summaries.Clear();

			groupInfo.SubGroupInfos.Clear();

			for(int index = 0; index < node.Nodes.Count; index++)
			{
				TreeNode subNode = node.Nodes[index];

				if(subNode.Tag is GroupSummary)
				{
					if(groupInfo.Summaries == null) groupInfo.Summaries = new GroupSummaryCollection();
	
					groupInfo.Summaries.Add(subNode.Tag as GroupSummary);
				}

				if(subNode.Tag is GroupInfo)
				{
					groupInfo.SubGroupInfos.Add(subNode.Tag as GroupInfo);

					this.UpdateView(subNode.Tag as GroupInfo,subNode);
				}
			}
		}

		public override void UpdateView(Webb.Reports.ExControls.Views.ExControlView i_View)
		{
			GridView m_GridView = i_View as GridView;

			GroupInfo m_RootGroupInfo = null;

			if(this.C_GroupInfoTree.Nodes.Count<=0)
			{
				goto EXIT;
			}
			
			GroupInfo m_GroupInfo = this.C_GroupInfoTree.Nodes[0].Tag as GroupInfo;
			
			this.UpdateView(m_GroupInfo,this.C_GroupInfoTree.Nodes[0]);

			m_RootGroupInfo = this.C_GroupInfoTree.Nodes[0].Tag as GroupInfo;
			
			EXIT:
				if(m_GridView.RootGroupInfo is SectionGroupInfo&&m_GridView.RootGroupInfo.IsSectionOutSide)
				{
					m_GridView.RootGroupInfo.SubGroupInfos.Clear();
			
					if(m_RootGroupInfo != null)
					{
						m_GridView.RootGroupInfo.SubGroupInfos.Add(m_RootGroupInfo);
					}
				}
				else
				{
					m_GridView.RootGroupInfo = m_RootGroupInfo;
				}

                int Start = m_GridView.ResolveIndentStartCol();

                m_GridView.UpdateVirtualGroupInfoWidth(ref Start, m_GridView.RootGroupInfo, m_GridView.ColumnsWidth);

                m_GridView.GridInfo.UpdateColumnsWidth(m_GridView.ColumnsWidth, Start);	//06-30-2008@Scott
		}

      

		private void Menu_AddGroup_Click(object sender, System.EventArgs e)
		{
			TreeNode node = this.C_GroupInfoTree.SelectedNode;

			if(this.C_GroupInfoTree.Nodes.Count == 0) 
			{
				FieldGroupInfo m_GroupInfo = new FieldGroupInfo();
			
				m_GroupInfo.GroupByField = Webb.Data.PublicDBFieldConverter.AvialableFields[0].ToString();
			
				m_GroupInfo.ColumnHeading = "New Group";				

				TreeNode m_Node = this.CreateGroupNode(m_GroupInfo);
			
				this.C_GroupInfoTree.Nodes.Add(m_Node);
			}

			if(node == null) return;

            if (this.HaveGroupChild(node))
            {
                MessageBox.Show("This node can only have one child group.");

                return;
            }

			this.AddGroup(node);
		}



		private bool HaveGroupChild(TreeNode node)
		{
			foreach(TreeNode childNode in node.Nodes)
			{
				if(childNode.Tag is GroupInfo) return true;
			}

			return false;
		}

		private void Menu_AddSummary_Click(object sender, System.EventArgs e)
		{
			TreeNode m_Node = this.C_GroupInfoTree.SelectedNode;
			
			if(m_Node == null) return;
			
			while(!(m_Node.Tag is GroupInfo))
			{
				m_Node = m_Node.Parent;
			}
			this.AddSummary(m_Node);
		}
		
		private void Menu_CopySummary_Click(object sender, System.EventArgs e)
		{
			TreeNode node = this.C_GroupInfoTree.SelectedNode;

			if(node == null || !(node.Tag is GroupSummary)) return;

			TreeNode rootNode = this.C_GroupInfoTree.SelectedNode;

			while(!(rootNode.Tag is GroupInfo))
			{
				rootNode = rootNode.Parent;
			}

			GroupSummary newSummary = (node.Tag as GroupSummary).Copy();

			newSummary.ColumnHeading = (node.Tag as GroupSummary).ColumnHeading;

			TreeNode newNode = this.CreateSummaryNode(newSummary);

			bool bFindSubGroup = false;

			int index = 0;

			for(;index < rootNode.Nodes.Count;index++)
			{
				if(rootNode.Nodes[index].Tag is GroupInfo)
				{
					bFindSubGroup = true;

					break;
				}
			}

			if(bFindSubGroup || index == rootNode.Nodes.Count)
			{
				rootNode.Nodes.Insert(index,newNode);
			}

			this.C_GroupInfoTree.SelectedNode = node;
			
			this.C_GroupInfoTree.Focus();
		}

		private void Menu_RemoveGroup_Click(object sender, System.EventArgs e)
		{
			if(this.C_GroupInfoTree.SelectedNode==null) return;
			
//			if(this.C_GroupInfoTree.SelectedNode==this.C_GroupInfoTree.Nodes[0])
//			{
//				Webb.Utilities.MessageBoxEx.ShowMessage("Cannot remove the root group.");
//
//				return;
//			}
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
				
				if(m_SelectedNode.Tag is GroupInfo && !(preNode.Tag is GroupInfo)) goto EXIT;

				m_SelectedNode.Remove();
				
				m_Nodes.Insert(m_Index-1,m_SelectedNode);
			}
			EXIT:
			{
				this.C_GroupInfoTree.SelectedNode = m_SelectedNode;
			
				this.C_GroupInfoTree.Focus();
			}
		}

		private void Menu_MoveDown_Click(object sender, System.EventArgs e)
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

				if(m_SelectedNode.Tag is GroupSummary && !(nextNode.Tag is GroupSummary)) goto EXIT;
				
				m_SelectedNode.Remove();
				
				m_Nodes.Insert(m_Index+1,m_SelectedNode);
			}
			EXIT:
			{
				this.C_GroupInfoTree.SelectedNode = m_SelectedNode;
			
				this.C_GroupInfoTree.Focus();
			}
		}
		
		private void AddSummary(TreeNode i_ParentNode)
		{
			FieldGroupInfo gi = i_ParentNode.Tag as FieldGroupInfo;

			GroupSummary m_Summary = new GroupSummary();
			
			if(gi!=null)m_Summary.Field = gi.GroupByField;
			
			m_Summary.SummaryType = SummaryTypes.Frequence;
			
			TreeNode m_node = this.CreateSummaryNode(m_Summary);
			
			bool bFindSubGroup = false;

			int index = 0;

			for(;index < i_ParentNode.Nodes.Count;index++)
			{
				if(i_ParentNode.Nodes[index].Tag is GroupInfo)
				{
					bFindSubGroup = true;

					break;
				}
			}

			if(bFindSubGroup || index == i_ParentNode.Nodes.Count)
			{
				i_ParentNode.Nodes.Insert(index,m_node);
			}
		}

		private void AddGroup(TreeNode node)
		{
			if(!(node.Tag is GroupInfo)) return;

			FieldGroupInfo m_GroupInfo = new FieldGroupInfo();
			
			m_GroupInfo.GroupByField = Webb.Data.PublicDBFieldConverter.AvialableFields[0].ToString();
			
			m_GroupInfo.ColumnHeading = "New Group";

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

		private void C_GroupInfoTree_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
		{
		
		}

		private void C_GroupInfoTree_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
		{
		
		}

		private void C_GroupInfoTree_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
		{
		
		}

		private void C_GroupInfoTree_ItemDrag(object sender, System.Windows.Forms.ItemDragEventArgs e)
		{
		
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

        private void ChangeGroup(TreeNode node)
        {
            if (node.Tag is FieldGroupInfo)
            {
                SectionGroupInfo m_GroupInfo = new SectionGroupInfo();

                m_GroupInfo.ColumnHeading = "New Section Group";

                m_GroupInfo.DistinctValues = false;

                node.Tag = m_GroupInfo.Copy();

                node.Text = m_GroupInfo.ToString();
            }
            else
            {
                FieldGroupInfo m_GroupInfo = new FieldGroupInfo();

                m_GroupInfo.GroupByField = Webb.Data.PublicDBFieldConverter.AvialableFields[0].ToString();

                m_GroupInfo.ColumnHeading = "New Group";

                node.Tag = m_GroupInfo.Copy();

                node.Text = m_GroupInfo.ToString();
            }


            this.C_PropertyGrid.Refresh();

            this.C_GroupInfoTree.Focus();

            this.C_PropertyGrid.SelectedObject = node.Tag;

        }

        private void BtnChangeGroup_Click(object sender, EventArgs e)
        {
            TreeNode node = this.C_GroupInfoTree.SelectedNode;

            if (node == null || !(node.Tag is GroupInfo)) return;

            ChangeGroup(node);

        }

        #region Methods for add Group
            private void AddCombinedFieldsGroup()
            {
                TreeNode node = this.C_GroupInfoTree.SelectedNode;

                if (this.C_GroupInfoTree.Nodes.Count == 0)
                {
                    CombinedFieldsGroupInfo m_rootGroupInfo = new CombinedFieldsGroupInfo();

                    m_rootGroupInfo.ColumnHeading = "New Combined Group";

                    TreeNode m_rootNode = this.CreateGroupNode(m_rootGroupInfo);

                    this.C_GroupInfoTree.Nodes.Add(m_rootNode);
                }

                if (node == null) return;

                if (this.HaveGroupChild(node))
                {
                    MessageBox.Show("This node can only have one child group.");

                    return;
                }
            
                if (!(node.Tag is GroupInfo)) return;

                CombinedFieldsGroupInfo m_GroupInfo = new CombinedFieldsGroupInfo();

                m_GroupInfo.ColumnHeading = "New Compute Group";

                TreeNode m_Node = this.CreateGroupNode(m_GroupInfo);

                node.Nodes.Add(m_Node);
            }
            //Modified at 2008-11-3 15:00:44@Scott
            private void AddSectionGroup()
            {
                TreeNode node = this.C_GroupInfoTree.SelectedNode;

                if (this.C_GroupInfoTree.Nodes.Count == 0)
                {
                    SectionGroupInfo m_rootGroupInfo = new SectionGroupInfo();

                    m_rootGroupInfo.ColumnHeading = "New Section Group";

                    TreeNode m_rootNode = this.CreateGroupNode(m_rootGroupInfo);

                    this.C_GroupInfoTree.Nodes.Add(m_rootNode);
                }

                if (node == null) return;

                if (this.HaveGroupChild(node))
                {
                    MessageBox.Show("This node can only have one child group.");

                    return;
                }

                if (!(node.Tag is GroupInfo)) return;

                SectionGroupInfo m_GroupInfo = new SectionGroupInfo();

                m_GroupInfo.ColumnHeading = "New Section Group";
             
                TreeNode m_Node = this.CreateGroupNode(m_GroupInfo);

                node.Nodes.Add(m_Node);
            }


            private void AddFieldGroup()
            {
                TreeNode node = this.C_GroupInfoTree.SelectedNode;

                if (this.C_GroupInfoTree.Nodes.Count == 0)
                {
                    FieldGroupInfo m_rootGroupInfo = new FieldGroupInfo();         

                    m_rootGroupInfo.ColumnHeading = "New Group";

                    TreeNode m_rootNode = this.CreateGroupNode(m_rootGroupInfo);

                    this.C_GroupInfoTree.Nodes.Add(m_rootNode);
                }

                if (node == null) return;

                if (this.HaveGroupChild(node))
                {
                    MessageBox.Show("This node can only have one child group.");

                    return;
                }

                this.AddGroup(node);
            }

        #endregion

      #region Events For Add Group
        private void addCombinedValueGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
             this.AddCombinedFieldsGroup();
        }     
        private void addSectionGroupMenuItem_Click(object sender, EventArgs e)
        {   
            this.AddSectionGroup();
        }

        private void addFieldGroupMenuItem_Click(object sender, EventArgs e)
        {
            this.AddFieldGroup();
        }
      #endregion

        private void C_NewGroup_Click(object sender, EventArgs e)
        {
            Point ptBottomLeft = new Point(this.C_NewGroup.Left, this.C_NewGroup.Bottom);

            this.C_AddGroupMenu.Show(this, ptBottomLeft);
        }


	}
}

