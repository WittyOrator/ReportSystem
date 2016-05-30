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
	public class ConfigStatInfo : Webb.Reports.ExControls.UI.ExControlDesignerControlBase
	{
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.PropertyGrid C_PropertyGrid;
		private System.Windows.Forms.ContextMenu C_Menu;
		private System.Windows.Forms.MenuItem Menu_RemoveGroup;
		private System.Windows.Forms.MenuItem Menu_MoveUp;
		private System.Windows.Forms.MenuItem Menu_MoveDown;
		private System.Windows.Forms.Button C_MoveUp;
		private System.Windows.Forms.Button C_MoveDown;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Button C_Remove;
		private System.Windows.Forms.TreeView C_GroupInfoTree;
		private System.Windows.Forms.Button C_NewStat;
		private System.Windows.Forms.Button C_CopyStat;
		private System.Windows.Forms.MenuItem Menu_AddStat;
		private System.Windows.Forms.MenuItem Menu_CopyStat;
		private System.ComponentModel.IContainer components = null;

		public ConfigStatInfo():base()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

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
			this.panel1 = new System.Windows.Forms.Panel();
			this.C_CopyStat = new System.Windows.Forms.Button();
			this.C_MoveDown = new System.Windows.Forms.Button();
			this.C_MoveUp = new System.Windows.Forms.Button();
			this.C_Remove = new System.Windows.Forms.Button();
			this.C_NewStat = new System.Windows.Forms.Button();
			this.C_PropertyGrid = new System.Windows.Forms.PropertyGrid();
			this.C_Menu = new System.Windows.Forms.ContextMenu();
			this.Menu_AddStat = new System.Windows.Forms.MenuItem();
			this.Menu_CopyStat = new System.Windows.Forms.MenuItem();
			this.Menu_RemoveGroup = new System.Windows.Forms.MenuItem();
			this.Menu_MoveUp = new System.Windows.Forms.MenuItem();
			this.Menu_MoveDown = new System.Windows.Forms.MenuItem();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.C_GroupInfoTree = new System.Windows.Forms.TreeView();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.C_CopyStat);
			this.panel1.Controls.Add(this.C_MoveDown);
			this.panel1.Controls.Add(this.C_MoveUp);
			this.panel1.Controls.Add(this.C_Remove);
			this.panel1.Controls.Add(this.C_NewStat);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(600, 40);
			this.panel1.TabIndex = 0;
			// 
			// C_CopyStat
			// 
			this.C_CopyStat.BackColor = System.Drawing.SystemColors.Control;
			this.C_CopyStat.Location = new System.Drawing.Point(120, 8);
			this.C_CopyStat.Name = "C_CopyStat";
			this.C_CopyStat.Size = new System.Drawing.Size(104, 23);
			this.C_CopyStat.TabIndex = 4;
			this.C_CopyStat.Text = "Copy Stat";
			this.C_CopyStat.Click += new System.EventHandler(this.Menu_CopyStat_Click);
			// 
			// C_MoveDown
			// 
			this.C_MoveDown.BackColor = System.Drawing.SystemColors.Control;
			this.C_MoveDown.Location = new System.Drawing.Point(419, 8);
			this.C_MoveDown.Name = "C_MoveDown";
			this.C_MoveDown.Size = new System.Drawing.Size(85, 23);
			this.C_MoveDown.TabIndex = 3;
			this.C_MoveDown.Text = "Move Down";
			this.C_MoveDown.Click += new System.EventHandler(this.Menu_MoveDown_Click);
			// 
			// C_MoveUp
			// 
			this.C_MoveUp.BackColor = System.Drawing.SystemColors.Control;
			this.C_MoveUp.Location = new System.Drawing.Point(341, 8);
			this.C_MoveUp.Name = "C_MoveUp";
			this.C_MoveUp.Size = new System.Drawing.Size(70, 23);
			this.C_MoveUp.TabIndex = 2;
			this.C_MoveUp.Text = "Move Up";
			this.C_MoveUp.Click += new System.EventHandler(this.Menu_MoveUp_Click);
			// 
			// C_Remove
			// 
			this.C_Remove.BackColor = System.Drawing.SystemColors.Control;
			this.C_Remove.Location = new System.Drawing.Point(232, 8);
			this.C_Remove.Name = "C_Remove";
			this.C_Remove.Size = new System.Drawing.Size(100, 23);
			this.C_Remove.TabIndex = 1;
			this.C_Remove.Text = "Remove Item";
			this.C_Remove.Click += new System.EventHandler(this.Menu_RemoveItem_Click);
			// 
			// C_NewStat
			// 
			this.C_NewStat.BackColor = System.Drawing.SystemColors.Control;
			this.C_NewStat.Location = new System.Drawing.Point(8, 8);
			this.C_NewStat.Name = "C_NewStat";
			this.C_NewStat.Size = new System.Drawing.Size(104, 23);
			this.C_NewStat.TabIndex = 0;
			this.C_NewStat.Text = "Add Stat";
			this.C_NewStat.Click += new System.EventHandler(this.Menu_AddStat_Click);
			// 
			// C_PropertyGrid
			// 
			this.C_PropertyGrid.CommandsVisibleIfAvailable = true;
			this.C_PropertyGrid.Cursor = System.Windows.Forms.Cursors.HSplit;
			this.C_PropertyGrid.Dock = System.Windows.Forms.DockStyle.Right;
			this.C_PropertyGrid.LargeButtons = false;
			this.C_PropertyGrid.LineColor = System.Drawing.SystemColors.Control;
			this.C_PropertyGrid.Location = new System.Drawing.Point(320, 40);
			this.C_PropertyGrid.Name = "C_PropertyGrid";
			this.C_PropertyGrid.Size = new System.Drawing.Size(280, 260);
			this.C_PropertyGrid.TabIndex = 1;
			this.C_PropertyGrid.Text = "propertyGrid1";
			this.C_PropertyGrid.ViewBackColor = System.Drawing.SystemColors.Window;
			this.C_PropertyGrid.ViewForeColor = System.Drawing.SystemColors.WindowText;
			// 
			// C_Menu
			// 
			this.C_Menu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																				   this.Menu_AddStat,
																				   this.Menu_CopyStat,
																				   this.Menu_RemoveGroup,
																				   this.Menu_MoveUp,
																				   this.Menu_MoveDown});
			// 
			// Menu_AddStat
			// 
			this.Menu_AddStat.Index = 0;
			this.Menu_AddStat.Text = "Add Stat";
			this.Menu_AddStat.Click += new System.EventHandler(this.Menu_AddStat_Click);
			// 
			// Menu_CopyStat
			// 
			this.Menu_CopyStat.Index = 1;
			this.Menu_CopyStat.Text = "Copy Stat";
			this.Menu_CopyStat.Click += new System.EventHandler(this.Menu_CopyStat_Click);
			// 
			// Menu_RemoveGroup
			// 
			this.Menu_RemoveGroup.Index = 2;
			this.Menu_RemoveGroup.Text = "Remove Item";
			this.Menu_RemoveGroup.Click += new System.EventHandler(this.Menu_RemoveItem_Click);
			// 
			// Menu_MoveUp
			// 
			this.Menu_MoveUp.Index = 3;
			this.Menu_MoveUp.Text = "Move Up";
			this.Menu_MoveUp.Click += new System.EventHandler(this.Menu_MoveUp_Click);
			// 
			// Menu_MoveDown
			// 
			this.Menu_MoveDown.Index = 4;
			this.Menu_MoveDown.Text = "Move Down";
			this.Menu_MoveDown.Click += new System.EventHandler(this.Menu_MoveDown_Click);
			// 
			// splitter1
			// 
			this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
			this.splitter1.Location = new System.Drawing.Point(315, 40);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(5, 260);
			this.splitter1.TabIndex = 3;
			this.splitter1.TabStop = false;
			// 
			// C_GroupInfoTree
			// 
			this.C_GroupInfoTree.ContextMenu = this.C_Menu;
			this.C_GroupInfoTree.Dock = System.Windows.Forms.DockStyle.Fill;
			this.C_GroupInfoTree.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.C_GroupInfoTree.ForeColor = System.Drawing.Color.Black;
			this.C_GroupInfoTree.ImageIndex = -1;
			this.C_GroupInfoTree.ItemHeight = 16;
			this.C_GroupInfoTree.Location = new System.Drawing.Point(0, 40);
			this.C_GroupInfoTree.Name = "C_GroupInfoTree";
			this.C_GroupInfoTree.SelectedImageIndex = -1;
			this.C_GroupInfoTree.Size = new System.Drawing.Size(320, 260);
			this.C_GroupInfoTree.TabIndex = 2;
			this.C_GroupInfoTree.KeyDown += new System.Windows.Forms.KeyEventHandler(this.C_GroupInfoTree_KeyDown);
			this.C_GroupInfoTree.Click += new System.EventHandler(this.C_GroupInfoTree_Click);
			this.C_GroupInfoTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.C_GroupInfoTree_AfterSelect);
			this.C_GroupInfoTree.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.C_GroupInfoTree_BeforeSelect);
			this.C_GroupInfoTree.Enter += new System.EventHandler(this.C_GroupInfoTree_Click);
			// 
			// ConfigStatInfo
			// 
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.C_GroupInfoTree);
			this.Controls.Add(this.C_PropertyGrid);
			this.Controls.Add(this.panel1);
			this.Name = "ConfigStatInfo";
			this.Size = new System.Drawing.Size(600, 300);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void C_GroupInfoTree_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			this.C_PropertyGrid.SelectedObject = this.C_GroupInfoTree.SelectedNode.Tag;
		}

		public override void SetView(Webb.Reports.ExControls.Views.ExControlView i_View)
		{
			this.C_GroupInfoTree.Nodes.Clear();
			
			if(!(i_View is StatControlView)) return;

			StatControlView m_StatView = i_View as StatControlView;

			foreach(StatInfo info in m_StatView.StatInfos)
			{
				TreeNode node = new TreeNode(info.ToString());

				node.Tag = info;

				this.C_GroupInfoTree.Nodes.Add(node);
			}
		}

		public override void UpdateView(Webb.Reports.ExControls.Views.ExControlView i_View)
		{
			if(this.C_GroupInfoTree.Nodes.Count<=0) return;
			
			if(!(i_View is StatControlView)) return;

			StatControlView m_StatView = i_View as StatControlView;

			m_StatView.StatInfos.Clear();

			for(int i_Index = 0; i_Index<this.C_GroupInfoTree.Nodes.Count;i_Index++)
			{
				TreeNode node = this.C_GroupInfoTree.Nodes[i_Index];

				m_StatView.StatInfos.Add(node.Tag as StatInfo);
			}
		}

		private void Menu_AddStat_Click(object sender, System.EventArgs e)
		{
			StatInfo info = new StatInfo();

			TreeNode node = new TreeNode(info.ToString());

			node.Tag = info;

			this.C_GroupInfoTree.Nodes.Add(node);
		}

		private void Menu_RemoveItem_Click(object sender, System.EventArgs e)
		{
			if(this.C_GroupInfoTree.SelectedNode==null) return;
			
			if(this.C_GroupInfoTree.Nodes.Count <= 1) return;

			this.C_GroupInfoTree.SelectedNode.Remove();
			
			this.C_GroupInfoTree.Focus();
		}

		private void Menu_MoveUp_Click(object sender, System.EventArgs e)
		{
			if(this.C_GroupInfoTree.SelectedNode==null) return;			
			TreeNode m_SelectedNode = this.C_GroupInfoTree.SelectedNode;
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
				m_SelectedNode.Remove();
				m_Nodes.Insert(m_Index-1,m_SelectedNode);
			}
			this.C_GroupInfoTree.SelectedNode = m_SelectedNode;
			this.C_GroupInfoTree.Focus();
		}

		private void Menu_MoveDown_Click(object sender, System.EventArgs e)
		{
			if(this.C_GroupInfoTree.SelectedNode==null) return;			
			TreeNode m_SelectedNode = this.C_GroupInfoTree.SelectedNode;
			TreeNodeCollection m_Nodes = null;
			if(m_SelectedNode.Parent==null)
			{
				m_Nodes= this.C_GroupInfoTree.Nodes;
			}
			else
			{
				m_Nodes = m_SelectedNode.Parent.Nodes;		
			}
			int m_Index = m_SelectedNode.Index;
			if(m_Index<m_Nodes.Count-1)
			{
				m_SelectedNode.Remove();
				m_Nodes.Insert(m_Index+1,m_SelectedNode);
			}
			this.C_GroupInfoTree.SelectedNode = m_SelectedNode;
			this.C_GroupInfoTree.Focus();
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

		private void Menu_CopyStat_Click(object sender, System.EventArgs e)
		{
			TreeNode node = this.C_GroupInfoTree.SelectedNode;

			if(node == null) return;

			StatInfo info = node.Tag as StatInfo;

			StatInfo newInfo = new StatInfo(info);

			TreeNode newNode = new TreeNode(newInfo.ToString());

			newNode.Tag = newInfo;

			this.C_GroupInfoTree.Nodes.Add(newNode);

			this.C_GroupInfoTree.SelectedNode = node;

			this.C_GroupInfoTree.Focus();
		}

		private void C_GroupInfoTree_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{//Modified at 2008-11-3 11:38:37@Scott
			switch(e.KeyCode)
			{
				case Keys.Delete:
					this.Menu_RemoveItem_Click(null,null);
					break;
				default:
					break;
			}
		}

//		private void GroupData_Changed(object sender, EventArgs e)
//		{
//			this.C_PropertyGrid.Refresh();
//		}
	}
}
