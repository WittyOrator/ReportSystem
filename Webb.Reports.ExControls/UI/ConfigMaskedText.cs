using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Reflection;

using Webb.Collections;
using Webb.Reports.ExControls.Data;
using Webb.Reports.ExControls.Views;

namespace Webb.Reports.ExControls.UI
{
	/// <summary>
	/// Summary description for ConfigSignedText.
	/// </summary>
	public class ConfigMaskedText : Webb.Reports.ExControls.UI.ExControlDesignerControlBase
	{
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ContextMenu C_Menu;
		private System.Windows.Forms.MenuItem Menu_RemoveGroup;
		private System.Windows.Forms.Button C_Remove;
		private System.Windows.Forms.Button C_NewStat;
		private System.Windows.Forms.Button C_CopyStat;
		private System.Windows.Forms.MenuItem Menu_AddSignInfo;
        private System.Windows.Forms.MenuItem Menu_CopySignInfo;
		private System.Windows.Forms.TreeView C_GroupInfoTree;
		private System.Windows.Forms.PropertyGrid C_PropertyGrid;
        private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Button C_MoveUP;
        private System.Windows.Forms.Button C_moveDown;
		private System.ComponentModel.IContainer components = null;

		private int CurColLeft=0;
        private System.Windows.Forms.ToolTip C_ToolTip;
		private bool InColResizing=false;



		public ConfigMaskedText():base()
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.C_CopyStat = new System.Windows.Forms.Button();
            this.C_Remove = new System.Windows.Forms.Button();
            this.C_NewStat = new System.Windows.Forms.Button();
            this.C_MoveUP = new System.Windows.Forms.Button();
            this.C_moveDown = new System.Windows.Forms.Button();
            this.C_Menu = new System.Windows.Forms.ContextMenu();
            this.Menu_AddSignInfo = new System.Windows.Forms.MenuItem();
            this.Menu_CopySignInfo = new System.Windows.Forms.MenuItem();
            this.Menu_RemoveGroup = new System.Windows.Forms.MenuItem();
            this.C_GroupInfoTree = new System.Windows.Forms.TreeView();
            this.C_PropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.C_ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.C_CopyStat);
            this.panel1.Controls.Add(this.C_Remove);
            this.panel1.Controls.Add(this.C_NewStat);
            this.panel1.Controls.Add(this.C_MoveUP);
            this.panel1.Controls.Add(this.C_moveDown);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(624, 47);
            this.panel1.TabIndex = 0;
            // 
            // C_CopyStat
            // 
            this.C_CopyStat.BackColor = System.Drawing.SystemColors.Control;
            this.C_CopyStat.Location = new System.Drawing.Point(141, 13);
            this.C_CopyStat.Name = "C_CopyStat";
            this.C_CopyStat.Size = new System.Drawing.Size(104, 23);
            this.C_CopyStat.TabIndex = 4;
            this.C_CopyStat.Text = "Copy MaskInfo";
            this.C_CopyStat.UseVisualStyleBackColor = false;
            this.C_CopyStat.Click += new System.EventHandler(this.Menu_CopyStat_Click);
            // 
            // C_Remove
            // 
            this.C_Remove.BackColor = System.Drawing.SystemColors.Control;
            this.C_Remove.Location = new System.Drawing.Point(265, 13);
            this.C_Remove.Name = "C_Remove";
            this.C_Remove.Size = new System.Drawing.Size(100, 23);
            this.C_Remove.TabIndex = 1;
            this.C_Remove.Text = "Remove Item";
            this.C_Remove.UseVisualStyleBackColor = false;
            this.C_Remove.Click += new System.EventHandler(this.Menu_RemoveItem_Click);
            // 
            // C_NewStat
            // 
            this.C_NewStat.BackColor = System.Drawing.SystemColors.Control;
            this.C_NewStat.Location = new System.Drawing.Point(18, 13);
            this.C_NewStat.Name = "C_NewStat";
            this.C_NewStat.Size = new System.Drawing.Size(104, 23);
            this.C_NewStat.TabIndex = 0;
            this.C_NewStat.Text = "Add MaskInfo";
            this.C_NewStat.UseVisualStyleBackColor = false;
            this.C_NewStat.Click += new System.EventHandler(this.Menu_AddStat_Click);
            // 
            // C_MoveUP
            // 
            this.C_MoveUP.BackColor = System.Drawing.SystemColors.Control;
            this.C_MoveUP.Location = new System.Drawing.Point(404, 13);
            this.C_MoveUP.Name = "C_MoveUP";
            this.C_MoveUP.Size = new System.Drawing.Size(88, 23);
            this.C_MoveUP.TabIndex = 1;
            this.C_MoveUP.Text = "Move Up";
            this.C_MoveUP.UseVisualStyleBackColor = false;
            this.C_MoveUP.Click += new System.EventHandler(this.Menu_MoveUp_Click);
            // 
            // C_moveDown
            // 
            this.C_moveDown.BackColor = System.Drawing.SystemColors.Control;
            this.C_moveDown.Location = new System.Drawing.Point(520, 13);
            this.C_moveDown.Name = "C_moveDown";
            this.C_moveDown.Size = new System.Drawing.Size(100, 23);
            this.C_moveDown.TabIndex = 1;
            this.C_moveDown.Text = "Move Down ";
            this.C_moveDown.UseVisualStyleBackColor = false;
            this.C_moveDown.Click += new System.EventHandler(this.Menu_MoveDown_Click);
            // 
            // C_Menu
            // 
            this.C_Menu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.Menu_AddSignInfo,
            this.Menu_CopySignInfo,
            this.Menu_RemoveGroup});
            // 
            // Menu_AddSignInfo
            // 
            this.Menu_AddSignInfo.Index = 0;
            this.Menu_AddSignInfo.Text = "Add MaskInfo";
            this.Menu_AddSignInfo.Click += new System.EventHandler(this.Menu_AddStat_Click);
            // 
            // Menu_CopySignInfo
            // 
            this.Menu_CopySignInfo.Index = 1;
            this.Menu_CopySignInfo.Text = "Copy MaskInfo";
            this.Menu_CopySignInfo.Click += new System.EventHandler(this.Menu_CopyStat_Click);
            // 
            // Menu_RemoveGroup
            // 
            this.Menu_RemoveGroup.Index = 2;
            this.Menu_RemoveGroup.Text = "Remove Item";
            this.Menu_RemoveGroup.Click += new System.EventHandler(this.Menu_RemoveItem_Click);
            // 
            // C_GroupInfoTree
            // 
            this.C_GroupInfoTree.ContextMenu = this.C_Menu;
            this.C_GroupInfoTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.C_GroupInfoTree.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.C_GroupInfoTree.ForeColor = System.Drawing.Color.Black;
            this.C_GroupInfoTree.ItemHeight = 16;
            this.C_GroupInfoTree.Location = new System.Drawing.Point(0, 47);
            this.C_GroupInfoTree.Name = "C_GroupInfoTree";
            this.C_GroupInfoTree.Size = new System.Drawing.Size(384, 361);
            this.C_GroupInfoTree.TabIndex = 6;
            this.C_GroupInfoTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.C_GroupInfoTree_AfterSelect);
            this.C_GroupInfoTree.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.C_GroupInfoTree_BeforeSelect);
            this.C_GroupInfoTree.Click += new System.EventHandler(this.C_GroupInfoTree_Click);
            // 
            // C_PropertyGrid
            // 
            this.C_PropertyGrid.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.C_PropertyGrid.Dock = System.Windows.Forms.DockStyle.Right;
            this.C_PropertyGrid.LineColor = System.Drawing.SystemColors.Control;
            this.C_PropertyGrid.Location = new System.Drawing.Point(384, 47);
            this.C_PropertyGrid.Name = "C_PropertyGrid";
            this.C_PropertyGrid.Size = new System.Drawing.Size(240, 361);
            this.C_PropertyGrid.TabIndex = 5;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(376, 47);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(8, 361);
            this.splitter1.TabIndex = 7;
            this.splitter1.TabStop = false;
            // 
            // C_ToolTip
            // 
            this.C_ToolTip.ShowAlways = true;
            // 
            // ConfigMaskedText
            // 
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.C_GroupInfoTree);
            this.Controls.Add(this.C_PropertyGrid);
            this.Controls.Add(this.panel1);
            this.Name = "ConfigMaskedText";
            this.Size = new System.Drawing.Size(624, 408);
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
			
			if(!(i_View is MaskedTextControlView)) return;

			MaskedTextControlView m_MaskView = i_View as MaskedTextControlView;

			foreach(MaskInfo info in m_MaskView.MaskInfoSetting)
			{
				TreeNode node = new TreeNode(info.ToString());				

				node.Tag = info;

				this.C_GroupInfoTree.Nodes.Add(node);
			}

            UpdateTextDisplay();
			
            if(this.C_GroupInfoTree.Nodes.Count>0)this.C_GroupInfoTree.SelectedNode=this.C_GroupInfoTree.Nodes[0];
		}
		private void UpdateTextDisplay()
		{
			foreach(TreeNode treeNode in this.C_GroupInfoTree.Nodes)
			{     
				treeNode.Text=treeNode.Tag.ToString();
			}
		}

		public override void UpdateView(Webb.Reports.ExControls.Views.ExControlView i_View)
		{			
			if(!(i_View is MaskedTextControlView)) return;
		
			MaskedTextControlView m_MaskView = i_View as MaskedTextControlView;

            m_MaskView.MaskInfoSetting.Clear();

			for(int i_Index = 0; i_Index<this.C_GroupInfoTree.Nodes.Count;i_Index++)
			{
				TreeNode node = this.C_GroupInfoTree.Nodes[i_Index];

                m_MaskView.MaskInfoSetting.Add(node.Tag as MaskInfo);
			}
		}

		

		private void Menu_AddStat_Click(object sender, System.EventArgs e)
		{
            MaskInfo info = new MaskInfo();
          
			TreeNode node = new TreeNode(info.ToString());

			node.Tag = info;

			this.C_GroupInfoTree.Nodes.Add(node);

			UpdateTextDisplay();	
		
		}
	

		private void Menu_RemoveItem_Click(object sender, System.EventArgs e)
		{
			if(this.C_GroupInfoTree.SelectedNode==null) return;			
		
			if(this.C_GroupInfoTree.SelectedNode==this.C_GroupInfoTree.Nodes[0])
			{
				MessageBox.Show("Failed to remove the first MaskInfo!","cann't remove",MessageBoxButtons.OK,MessageBoxIcon.Stop); 
				
				return;
			}

			this.C_GroupInfoTree.SelectedNode.Remove();

            UpdateTextDisplay();
	
			this.C_GroupInfoTree.Focus();			
		
		}

		private void Menu_MoveUp_Click(object sender, System.EventArgs e)
		{
			if(this.C_GroupInfoTree.SelectedNode==null) return;		
	
			TreeNode m_SelectedNode = this.C_GroupInfoTree.SelectedNode;

			int m_Index = m_SelectedNode.Index;

            if (m_Index >= 1)
            {
                TreeNodeCollection m_Nodes = null;

                if (m_SelectedNode.Parent == null)
                {
                    m_Nodes = this.C_GroupInfoTree.Nodes;
                }
                else
                {
                    m_Nodes = m_SelectedNode.Parent.Nodes;
                }

                m_SelectedNode.Remove();

                m_Nodes.Insert(m_Index - 1, m_SelectedNode);
            }

			this.C_GroupInfoTree.SelectedNode = m_SelectedNode;

            UpdateTextDisplay();

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

			UpdateTextDisplay();			

		
			this.C_GroupInfoTree.SelectedNode = m_SelectedNode;
			this.C_GroupInfoTree.Focus();
		}
		
		private void Menu_CopyStat_Click(object sender, System.EventArgs e)
		{
			TreeNode node = this.C_GroupInfoTree.SelectedNode;

			if(node == null) return;

            MaskInfo info = node.Tag as MaskInfo;

            MaskInfo newInfo = info.Copy();

			TreeNode newNode = new TreeNode(newInfo.ToString());

			newNode.Tag = newInfo;

			this.C_GroupInfoTree.Nodes.Add(newNode);

			this.UpdateTextDisplay();			

			this.C_GroupInfoTree.SelectedNode = node;
			
				
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


		
	}
}
