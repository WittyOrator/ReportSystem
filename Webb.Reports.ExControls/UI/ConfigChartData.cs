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
	public class ConfigChartData : Webb.Reports.ExControls.UI.ExControlDesignerControlBase
	{
		private TreeView _CurTree;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button C_NewGroup;
		private System.Windows.Forms.PropertyGrid C_PropertyGrid;
		private System.Windows.Forms.TreeView C_GroupInfoTree;
		private System.Windows.Forms.ContextMenu C_Menu;
		private System.Windows.Forms.MenuItem Menu_AddGroup;
		private System.Windows.Forms.MenuItem Menu_RemoveGroup;
		private System.Windows.Forms.MenuItem Menu_MoveUp;
		private System.Windows.Forms.MenuItem Menu_MoveDown;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.MenuItem Menu_AddSummary;
		private System.Windows.Forms.Button C_Remove;
		private System.Windows.Forms.Splitter splitter2;
		private System.Windows.Forms.ImageList C_ImageList;
		private System.Windows.Forms.TreeView C_SeriesTree;
		private System.Windows.Forms.Button C_NewSeries;
		private System.Windows.Forms.Button C_MoveDown;
		private System.Windows.Forms.Button C_MoveUp;
		private System.ComponentModel.IContainer components = null;

		public ConfigChartData():base()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			this.Load += new EventHandler(ConfigChartData_Load);
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ConfigChartData));
			this.panel1 = new System.Windows.Forms.Panel();
			this.C_MoveDown = new System.Windows.Forms.Button();
			this.C_MoveUp = new System.Windows.Forms.Button();
			this.C_Remove = new System.Windows.Forms.Button();
			this.C_NewGroup = new System.Windows.Forms.Button();
			this.C_NewSeries = new System.Windows.Forms.Button();
			this.C_PropertyGrid = new System.Windows.Forms.PropertyGrid();
			this.C_GroupInfoTree = new System.Windows.Forms.TreeView();
			this.C_Menu = new System.Windows.Forms.ContextMenu();
			this.Menu_AddGroup = new System.Windows.Forms.MenuItem();
			this.Menu_AddSummary = new System.Windows.Forms.MenuItem();
			this.Menu_RemoveGroup = new System.Windows.Forms.MenuItem();
			this.Menu_MoveUp = new System.Windows.Forms.MenuItem();
			this.Menu_MoveDown = new System.Windows.Forms.MenuItem();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.splitter2 = new System.Windows.Forms.Splitter();
			this.C_ImageList = new System.Windows.Forms.ImageList(this.components);
			this.C_SeriesTree = new System.Windows.Forms.TreeView();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.C_MoveDown);
			this.panel1.Controls.Add(this.C_MoveUp);
			this.panel1.Controls.Add(this.C_Remove);
			this.panel1.Controls.Add(this.C_NewGroup);
			this.panel1.Controls.Add(this.C_NewSeries);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(680, 40);
			this.panel1.TabIndex = 0;
			// 
			// C_MoveDown
			// 
			this.C_MoveDown.BackColor = System.Drawing.SystemColors.Control;
			this.C_MoveDown.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.C_MoveDown.Location = new System.Drawing.Point(368, 8);
			this.C_MoveDown.Name = "C_MoveDown";
			this.C_MoveDown.Size = new System.Drawing.Size(90, 23);
			this.C_MoveDown.TabIndex = 3;
			this.C_MoveDown.Text = "Move Down";
			this.C_MoveDown.Click += new System.EventHandler(this.Menu_MoveDown_Click);
			// 
			// C_MoveUp
			// 
			this.C_MoveUp.BackColor = System.Drawing.SystemColors.Control;
			this.C_MoveUp.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.C_MoveUp.Location = new System.Drawing.Point(288, 8);
			this.C_MoveUp.Name = "C_MoveUp";
			this.C_MoveUp.Size = new System.Drawing.Size(70, 23);
			this.C_MoveUp.TabIndex = 2;
			this.C_MoveUp.Text = "Move Up";
			this.C_MoveUp.Click += new System.EventHandler(this.Menu_MoveUp_Click);
			// 
			// C_Remove
			// 
			this.C_Remove.BackColor = System.Drawing.SystemColors.Control;
			this.C_Remove.Location = new System.Drawing.Point(184, 8);
			this.C_Remove.Name = "C_Remove";
			this.C_Remove.Size = new System.Drawing.Size(100, 23);
			this.C_Remove.TabIndex = 1;
			this.C_Remove.Text = "Remove Item";
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
			this.C_NewGroup.Click += new System.EventHandler(this.Menu_AddGroup_Click);
			// 
			// C_NewSeries
			// 
			this.C_NewSeries.BackColor = System.Drawing.SystemColors.Control;
			this.C_NewSeries.Location = new System.Drawing.Point(96, 8);
			this.C_NewSeries.Name = "C_NewSeries";
			this.C_NewSeries.Size = new System.Drawing.Size(80, 23);
			this.C_NewSeries.TabIndex = 0;
			this.C_NewSeries.Text = "Add Series";
			this.C_NewSeries.Click += new System.EventHandler(this.Menu_AddSummary_Click);
			// 
			// C_PropertyGrid
			// 
			this.C_PropertyGrid.CommandsVisibleIfAvailable = true;
			this.C_PropertyGrid.Dock = System.Windows.Forms.DockStyle.Right;
			this.C_PropertyGrid.LargeButtons = false;
			this.C_PropertyGrid.LineColor = System.Drawing.SystemColors.Control;
			this.C_PropertyGrid.Location = new System.Drawing.Point(488, 40);
			this.C_PropertyGrid.Name = "C_PropertyGrid";
			this.C_PropertyGrid.Size = new System.Drawing.Size(192, 324);
			this.C_PropertyGrid.TabIndex = 1;
			this.C_PropertyGrid.Text = "propertyGrid1";
			this.C_PropertyGrid.ViewBackColor = System.Drawing.SystemColors.Window;
			this.C_PropertyGrid.ViewForeColor = System.Drawing.SystemColors.WindowText;
			// 
			// C_GroupInfoTree
			// 
			this.C_GroupInfoTree.ContextMenu = this.C_Menu;
			this.C_GroupInfoTree.Dock = System.Windows.Forms.DockStyle.Fill;
			this.C_GroupInfoTree.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.C_GroupInfoTree.ForeColor = System.Drawing.Color.Black;
			this.C_GroupInfoTree.ImageIndex = -1;
			this.C_GroupInfoTree.Location = new System.Drawing.Point(0, 40);
			this.C_GroupInfoTree.Name = "C_GroupInfoTree";
			this.C_GroupInfoTree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
																						new System.Windows.Forms.TreeNode("Node")});
			this.C_GroupInfoTree.SelectedImageIndex = -1;
			this.C_GroupInfoTree.Size = new System.Drawing.Size(288, 324);
			this.C_GroupInfoTree.TabIndex = 2;
			this.C_GroupInfoTree.KeyDown += new System.Windows.Forms.KeyEventHandler(this.C_Tree_KeyDown);
			this.C_GroupInfoTree.Click += new System.EventHandler(this.C_GroupInfoTree_Click);
			this.C_GroupInfoTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.C_GroupInfoTree_AfterSelect);
			this.C_GroupInfoTree.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.C_GroupInfoTree_BeforeSelect);
			this.C_GroupInfoTree.Enter += new System.EventHandler(this.C_GroupInfoTree_Click);
			// 
			// C_Menu
			// 
			this.C_Menu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																				   this.Menu_AddGroup,
																				   this.Menu_AddSummary,
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
			this.Menu_AddSummary.Text = "Add Series";
			this.Menu_AddSummary.Click += new System.EventHandler(this.Menu_AddSummary_Click);
			// 
			// Menu_RemoveGroup
			// 
			this.Menu_RemoveGroup.Index = 2;
			this.Menu_RemoveGroup.Text = "Remove Item";
			this.Menu_RemoveGroup.Click += new System.EventHandler(this.Menu_RemoveGroup_Click);
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
			this.splitter1.Location = new System.Drawing.Point(483, 40);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(5, 324);
			this.splitter1.TabIndex = 3;
			this.splitter1.TabStop = false;
			// 
			// splitter2
			// 
			this.splitter2.Dock = System.Windows.Forms.DockStyle.Right;
			this.splitter2.Location = new System.Drawing.Point(288, 40);
			this.splitter2.Name = "splitter2";
			this.splitter2.Size = new System.Drawing.Size(3, 324);
			this.splitter2.TabIndex = 5;
			this.splitter2.TabStop = false;
			// 
			// C_ImageList
			// 
			this.C_ImageList.ImageSize = new System.Drawing.Size(16, 16);
			this.C_ImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("C_ImageList.ImageStream")));
			this.C_ImageList.TransparentColor = System.Drawing.Color.Magenta;
			// 
			// C_SeriesTree
			// 
			this.C_SeriesTree.Dock = System.Windows.Forms.DockStyle.Right;
			this.C_SeriesTree.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.C_SeriesTree.ForeColor = System.Drawing.Color.Black;
			this.C_SeriesTree.ImageList = this.C_ImageList;
			this.C_SeriesTree.Location = new System.Drawing.Point(291, 40);
			this.C_SeriesTree.Name = "C_SeriesTree";
			this.C_SeriesTree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
																					 new System.Windows.Forms.TreeNode("Node")});
			this.C_SeriesTree.ShowLines = false;
			this.C_SeriesTree.Size = new System.Drawing.Size(192, 324);
			this.C_SeriesTree.TabIndex = 6;
			this.C_SeriesTree.KeyDown += new System.Windows.Forms.KeyEventHandler(this.C_Tree_KeyDown);
			this.C_SeriesTree.Click += new System.EventHandler(this.C_GroupInfoTree_Click);
			this.C_SeriesTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.C_GroupInfoTree_AfterSelect);
			this.C_SeriesTree.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.C_GroupInfoTree_BeforeSelect);
			this.C_SeriesTree.Enter += new System.EventHandler(this.C_GroupInfoTree_Click);
			// 
			// ConfigChartData
			// 
			this.Controls.Add(this.C_GroupInfoTree);
			this.Controls.Add(this.splitter2);
			this.Controls.Add(this.C_SeriesTree);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.C_PropertyGrid);
			this.Controls.Add(this.panel1);
			this.Name = "ConfigChartData";
			this.Size = new System.Drawing.Size(680, 364);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void C_GroupInfoTree_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			e.Node.ForeColor = Color.Red;

			this.C_PropertyGrid.SelectedObject = e.Node.Tag;
		}

		private void C_GroupInfoTree_BeforeSelect(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
		{
			TreeView m_tv = sender as TreeView;
			if(m_tv.SelectedNode!=null)
			{
				m_tv.SelectedNode.ForeColor = Color.Black;
			}

			e.Node.Text = e.Node.Tag.ToString();
		}

		private void ConfigChartData_Load(object sender, EventArgs e)
		{
			if(this.C_GroupInfoTree.Nodes.Count > 0)
			{
				this.C_PropertyGrid.SelectedObject = this.C_GroupInfoTree.Nodes[0].Tag;
			}
		}

		public override void SetView(Webb.Reports.ExControls.Views.ExControlView i_View)
		{
			//clear trees
			this.C_GroupInfoTree.Nodes.Clear();
			
			this.C_SeriesTree.Nodes.Clear();

			WebbChartView chartView = i_View as WebbChartView;

			//set group tree
			GroupInfo m_GroupInfo = null;

			m_GroupInfo = chartView.RootGroupInfo;

			while(m_GroupInfo!=null)
			{
				TreeNode m_node = new TreeNode(m_GroupInfo.ToString());
				
				m_node.Tag = m_GroupInfo.Copy();
				
				this.C_GroupInfoTree.Nodes.Add(m_node);
				
				GroupInfo subGroupInfo = null;

				if(m_GroupInfo.SubGroupInfos.Count > 0)
				{
					subGroupInfo = m_GroupInfo.SubGroupInfos[0];
				}

				m_GroupInfo = subGroupInfo;
			}

			//set series tree
			foreach(ChartSeries series in chartView.Series)
			{
				TreeNode m_node = new TreeNode(series.ToString());
				
				m_node.Tag = series.Copy();

				this.C_SeriesTree.Nodes.Add(m_node);
			}
		}

		public override void UpdateView(Webb.Reports.ExControls.Views.ExControlView i_View)
		{	
			//update group
			GroupInfo m_GroupInfo = null;
			
			for(int i_Index = 0; i_Index<this.C_GroupInfoTree.Nodes.Count;i_Index++)
			{
				if(i_Index>0)
				{
					m_GroupInfo.SubGroupInfos.Clear();

					m_GroupInfo.SubGroupInfos.Add(this.C_GroupInfoTree.Nodes[i_Index].Tag as GroupInfo);
					
					m_GroupInfo = m_GroupInfo.SubGroupInfos[0];
				}
				else
				{
					m_GroupInfo = this.C_GroupInfoTree.Nodes[i_Index].Tag as GroupInfo;
				}
				
				m_GroupInfo.SubGroupInfos.Clear();
			}
			
			WebbChartView m_GroupView = i_View as WebbChartView;

			GroupInfo m_RootGroupInfo = this.C_GroupInfoTree.Nodes[0].Tag as GroupInfo;
			
			m_GroupView.RootGroupInfo = m_RootGroupInfo;

			//update summaries
			m_GroupView.Series.Clear();
			for(int i_Index = 0; i_Index<this.C_SeriesTree.Nodes.Count;i_Index++)
			{
				m_GroupView.Series.Add(this.C_SeriesTree.Nodes[i_Index].Tag as ChartSeries);
			}
		}

		private void Menu_AddGroup_Click(object sender, System.EventArgs e)
		{
			this.AddGroup();
		}

		private void Menu_AddSummary_Click(object sender, System.EventArgs e)
		{
			ChartSeries series = new ChartSeries();

			TreeNode node = new TreeNode(series.ToString());

			node.Tag = series;

			this.C_SeriesTree.Nodes.Add(node);
		}

		private void Menu_RemoveGroup_Click(object sender, System.EventArgs e)
		{
			TreeView tree = this._CurTree;

			if(tree == null || tree.SelectedNode == null) return;

			if(tree.SelectedNode == tree.Nodes[0])
			{
				Webb.Utilities.MessageBoxEx.ShowMessage("Cannot remove the last one.");
				
				return;
			}

			tree.SelectedNode.Remove();
			
			tree.Focus();
		}

		private void Menu_MoveUp_Click(object sender, System.EventArgs e)
		{
			TreeView tree = this._CurTree;

			if(tree == null || tree.SelectedNode == null) return;

			TreeNode selectedNode = tree.SelectedNode;

			int index = selectedNode.Index;

			if(index > 0)
			{
				selectedNode.Remove();
				
				tree.Nodes.Insert(index-1,selectedNode);
			}

			tree.SelectedNode = selectedNode;
			
			tree.Focus();
		}

		private void Menu_MoveDown_Click(object sender, System.EventArgs e)
		{
			TreeView tree = this._CurTree;

			if(tree == null || tree.SelectedNode == null) return;

			TreeNode selectedNode = tree.SelectedNode;

			int index = selectedNode.Index;

			if(index < tree.Nodes.Count)
			{
				selectedNode.Remove();
				
				tree.Nodes.Insert(index+1,selectedNode);
			}

			tree.SelectedNode = selectedNode;
			
			tree.Focus();
		}

		private void AddGroup()
		{
			FieldGroupInfo m_GroupInfo = new FieldGroupInfo();
			
			m_GroupInfo.GroupByField = Webb.Data.PublicDBFieldConverter.AvialableFields[0].ToString();
			
			m_GroupInfo.ColumnHeading = "New Group";

			TreeNode m_Node = new TreeNode(m_GroupInfo.ToString());
			
			m_Node.Tag = m_GroupInfo;
			
			this.C_GroupInfoTree.Nodes.Add(m_Node);

			//m_GroupInfo.GroupFieldChanged += new EventHandler(GroupData_Changed);
		}

		private void C_GroupInfoTree_Click(object sender, System.EventArgs e)
		{
			TreeView tree = sender as TreeView;
			
			if(tree.SelectedNode == null) return;

			tree.SelectedNode.Text = tree.SelectedNode.Tag.ToString();

			if(tree == this.C_SeriesTree)
			{
				this.C_PropertyGrid.SelectedObject = tree.SelectedNode.Tag as ChartSeries;
			}

			if(tree == this.C_GroupInfoTree)
			{	
				this.C_PropertyGrid.SelectedObject = tree.SelectedNode.Tag as GroupInfo;
			}

			this._CurTree = tree;
		}

		private void GroupData_Changed(object sender, EventArgs e)
		{
			this.C_PropertyGrid.Refresh();
		}

		private void C_Tree_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
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
	}
}

