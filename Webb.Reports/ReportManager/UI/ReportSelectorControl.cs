/***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:ReportSelectorControl.cs
 * Author:Country.Wu [EMail:Webb.Country.Wu@163.com]
 * Create Time:11/27/2007 04:05:29 PM
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
using System.Windows.Forms;

namespace Webb.Reports.ReportManager.UI
{
	public class ReportSelectorControl : Webb.Utilities.Wizards.WinzardControlBase
	{
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.TreeView C_ReportsTree;
		private System.Windows.Forms.PropertyGrid C_Properties;
		private System.ComponentModel.IContainer components = null;
		private ReportTemplateCollection _AllTemplate;

		public ReportSelectorControl()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();
			this.WizardTitle = "Select Report Template:";
			this.SingleStep = true;
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
			this.C_ReportsTree = new System.Windows.Forms.TreeView();
			this.C_Properties = new System.Windows.Forms.PropertyGrid();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.SuspendLayout();
			// 
			// C_ReportsTree
			// 
			this.C_ReportsTree.CheckBoxes = true;
			this.C_ReportsTree.Dock = System.Windows.Forms.DockStyle.Left;
			this.C_ReportsTree.ImageIndex = -1;
			this.C_ReportsTree.Location = new System.Drawing.Point(0, 0);
			this.C_ReportsTree.Name = "C_ReportsTree";
			this.C_ReportsTree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
																					  new System.Windows.Forms.TreeNode("Advantage Reports", new System.Windows.Forms.TreeNode[] {
																																													 new System.Windows.Forms.TreeNode("Report 1")}),
																					  new System.Windows.Forms.TreeNode("Victory Reports", new System.Windows.Forms.TreeNode[] {
																																												   new System.Windows.Forms.TreeNode("Report 1")}),
																					  new System.Windows.Forms.TreeNode("Other Reports", new System.Windows.Forms.TreeNode[] {
																																												 new System.Windows.Forms.TreeNode("Report 1")})});
			this.C_ReportsTree.SelectedImageIndex = -1;
			this.C_ReportsTree.Size = new System.Drawing.Size(216, 280);
			this.C_ReportsTree.TabIndex = 0;
			this.C_ReportsTree.Click += new System.EventHandler(this.C_ReportsTree_Click);
			this.C_ReportsTree.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.C_ReportsTree_AfterCheck);
			this.C_ReportsTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.C_ReportsTree_AfterSelect);
			// 
			// C_Properties
			// 
			this.C_Properties.CommandsVisibleIfAvailable = true;
			this.C_Properties.Dock = System.Windows.Forms.DockStyle.Fill;
			this.C_Properties.LargeButtons = false;
			this.C_Properties.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.C_Properties.Location = new System.Drawing.Point(216, 0);
			this.C_Properties.Name = "C_Properties";
			this.C_Properties.Size = new System.Drawing.Size(320, 280);
			this.C_Properties.TabIndex = 1;
			this.C_Properties.Text = "propertyGrid1";
			this.C_Properties.ViewBackColor = System.Drawing.SystemColors.Window;
			this.C_Properties.ViewForeColor = System.Drawing.SystemColors.WindowText;
			// 
			// splitter1
			// 
			this.splitter1.Location = new System.Drawing.Point(216, 0);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(5, 280);
			this.splitter1.TabIndex = 2;
			this.splitter1.TabStop = false;
			// 
			// ReportSelectorControl
			// 
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.C_Properties);
			this.Controls.Add(this.C_ReportsTree);
			this.Name = "ReportSelectorControl";
			this.ResumeLayout(false);

		}
		#endregion

		public override void SetData(object i_Data)
		{
			//base.SetData (i_Data);
			ReportTemplateCollection m_Repots = i_Data as ReportTemplateCollection;
			if(m_Repots==null) return;
		}

		public override void UpdateData(object i_Data)
		{
			//base.UpdateData (i_Data);
			ReportTemplateCollection m_Repots = i_Data as ReportTemplateCollection;
			if(m_Repots==null) return;
		}

		public override bool ValidateSetting()
		{
			return true;
		}

		private void C_ReportsTree_Click(object sender, System.EventArgs e)
		{
//			TreeNode m_Node = this.C_ReportsTree.SelectedNode;
//
//			if(m_Node == null) return;
//
//			m_Node.Checked = !m_Node.Checked;
//
//			foreach(TreeNode m_SubNode in m_Node.Nodes)
//			{
//				m_SubNode.Checked = m_Node.Checked;
//			}
		}

		private void C_ReportsTree_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{	
			TreeNode m_Node = e.Node;

			this.C_Properties.SelectedObject = m_Node.Tag;
		}

		private void C_ReportsTree_AfterCheck(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			TreeNode m_Node = e.Node;

			foreach(TreeNode m_SubNode in m_Node.Nodes)
			{
				m_SubNode.Checked = m_Node.Checked;
			}
		}

		public void InitReports(ReportTemplateCollection i_Reports)
		{
			this._AllTemplate = i_Reports;

			this.ResetTreeNodes();
			foreach(ReportTemplate m_Report in i_Reports)
			{
				if(m_Report.TemplateType==TemplateTypes.Advantage)
				{
					TreeNode m_Node = new TreeNode(m_Report.TemplateName);
					m_Node.Tag = m_Report;
					this.C_ReportsTree.Nodes[0].Nodes.Add(m_Node);
				}
				else if(m_Report.TemplateType==TemplateTypes.Victory)
				{
					TreeNode m_Node = new TreeNode(m_Report.TemplateName);
					m_Node.Tag = m_Report;
					this.C_ReportsTree.Nodes[1].Nodes.Add(m_Node);
				}
				else if(m_Report.TemplateType==TemplateTypes.Other)
				{
					TreeNode m_Node = new TreeNode(m_Report.TemplateName);
					m_Node.Tag = m_Report;
					this.C_ReportsTree.Nodes[2].Nodes.Add(m_Node);
				}
			}
		}

		//Scott@2007-12-03 10:38 modified some of the following code.
		public void GetSelectReports(ReportTemplateCollection i_Reports)
		{
			if(i_Reports == null) return;

			i_Reports.Clear();
			
			foreach(TreeNode i_node in this.C_ReportsTree.Nodes)
			{
				AddReportsFromNode(i_node,i_Reports);
			}
		}

		//Scott@2007-12-03 10:39 modified some of the following code.
		private void AddReportsFromNode(TreeNode i_node,ReportTemplateCollection i_Reports)
		{
			if(i_node == null) return;
			
			if(i_node.Checked && i_node.Parent != null)
			{
				ReportTemplate m_Template = i_node.Tag as ReportTemplate;

				if(m_Template != null)
				{
					i_Reports.Add(m_Template);
				}
			}

			//Recursion 
			if(i_node.Nodes != null)
			{
				foreach(TreeNode i_ChildNode in i_node.Nodes)
				{
					AddReportsFromNode(i_ChildNode,i_Reports);
				}		
			}
		}

		private void ResetTreeNodes()
		{
			this.C_ReportsTree.Nodes[0].Checked = false;
			this.C_ReportsTree.Nodes[1].Checked = false;
			this.C_ReportsTree.Nodes[2].Checked = false;
			this.C_ReportsTree.Nodes[0].Nodes.Clear();	//Adv
			this.C_ReportsTree.Nodes[1].Nodes.Clear();	//Vic
			this.C_ReportsTree.Nodes[2].Nodes.Clear();	//Other
		}
	}
}

