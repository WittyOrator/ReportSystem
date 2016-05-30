/***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:StyleBuilderForm.cs
 * Author:Country.Wu [EMail:Webb.Country.Wu@163.com]
 * Create Time:12/3/2007 04:35:46 PM
 * Copyright:1986-2007@Webb Electronics all right reserved.
 * Purpose:
 * ***********************************************************************/
#region History
/*
 * //Author@DateTime : Description
 * */
#endregion History

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;

namespace Webb.Reports.ExControls.UI
{
	/// <summary>
	/// Summary description for StyleBuilderForm.
	/// </summary>
	public class StyleBuilderForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Button C_OK;
		private System.Windows.Forms.Panel C_BottomPanel;
		private System.Windows.Forms.Button C_Cancel;
		private System.Windows.Forms.PropertyGrid C_PropertyGrid;
		private System.Windows.Forms.TreeView C_StyleTree;
		private System.Windows.Forms.Button C_BtnRevert;
		private BasicStyle _RowsStyle;
		private BasicStyle _ColumnsStyle;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public StyleBuilderForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			this._RowsStyle = new BasicStyle();
			this._ColumnsStyle = new BasicStyle();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.C_BottomPanel = new System.Windows.Forms.Panel();
			this.C_Cancel = new System.Windows.Forms.Button();
			this.C_OK = new System.Windows.Forms.Button();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.C_PropertyGrid = new System.Windows.Forms.PropertyGrid();
			this.C_StyleTree = new System.Windows.Forms.TreeView();
			this.C_BtnRevert = new System.Windows.Forms.Button();
			this.C_BottomPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// C_BottomPanel
			// 
			this.C_BottomPanel.Controls.Add(this.C_BtnRevert);
			this.C_BottomPanel.Controls.Add(this.C_Cancel);
			this.C_BottomPanel.Controls.Add(this.C_OK);
			this.C_BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.C_BottomPanel.Location = new System.Drawing.Point(0, 358);
			this.C_BottomPanel.Name = "C_BottomPanel";
			this.C_BottomPanel.Size = new System.Drawing.Size(728, 40);
			this.C_BottomPanel.TabIndex = 0;
			// 
			// C_Cancel
			// 
			this.C_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.C_Cancel.Location = new System.Drawing.Point(640, 8);
			this.C_Cancel.Name = "C_Cancel";
			this.C_Cancel.TabIndex = 1;
			this.C_Cancel.Text = "Cancel";
			this.C_Cancel.Click += new System.EventHandler(this.C_Cancel_Click);
			// 
			// C_OK
			// 
			this.C_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.C_OK.Location = new System.Drawing.Point(552, 8);
			this.C_OK.Name = "C_OK";
			this.C_OK.TabIndex = 0;
			this.C_OK.Text = "OK";
			this.C_OK.Click += new System.EventHandler(this.C_OK_Click);
			// 
			// splitter1
			// 
			this.splitter1.Location = new System.Drawing.Point(184, 0);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(5, 358);
			this.splitter1.TabIndex = 2;
			this.splitter1.TabStop = false;
			// 
			// C_PropertyGrid
			// 
			this.C_PropertyGrid.CommandsVisibleIfAvailable = true;
			this.C_PropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.C_PropertyGrid.LargeButtons = false;
			this.C_PropertyGrid.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.C_PropertyGrid.Location = new System.Drawing.Point(189, 0);
			this.C_PropertyGrid.Name = "C_PropertyGrid";
			this.C_PropertyGrid.Size = new System.Drawing.Size(539, 358);
			this.C_PropertyGrid.TabIndex = 3;
			this.C_PropertyGrid.Text = "propertyGrid1";
			this.C_PropertyGrid.ViewBackColor = System.Drawing.SystemColors.Window;
			this.C_PropertyGrid.ViewForeColor = System.Drawing.SystemColors.WindowText;
			this.C_PropertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.C_PropertyGrid_PropertyValueChanged);
			// 
			// C_StyleTree
			// 
			this.C_StyleTree.Dock = System.Windows.Forms.DockStyle.Left;
			this.C_StyleTree.ImageIndex = -1;
			this.C_StyleTree.Location = new System.Drawing.Point(0, 0);
			this.C_StyleTree.Name = "C_StyleTree";
			this.C_StyleTree.SelectedImageIndex = -1;
			this.C_StyleTree.Size = new System.Drawing.Size(184, 358);
			this.C_StyleTree.TabIndex = 4;
			this.C_StyleTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.C_StyleTree_AfterSelect);
			this.C_StyleTree.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.C_StyleTree_BeforeSelect);
			// 
			// C_BtnRevert
			// 
			this.C_BtnRevert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.C_BtnRevert.Location = new System.Drawing.Point(16, 8);
			this.C_BtnRevert.Name = "C_BtnRevert";
			this.C_BtnRevert.TabIndex = 2;
			this.C_BtnRevert.Text = "Revert";
			this.C_BtnRevert.Click += new System.EventHandler(this.C_BtnRevert_Click);
			// 
			// StyleBuilderForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(7, 15);
			this.ClientSize = new System.Drawing.Size(728, 398);
			this.Controls.Add(this.C_PropertyGrid);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.C_StyleTree);
			this.Controls.Add(this.C_BottomPanel);
			this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "StyleBuilderForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Style Builder Form";
			this.C_BottomPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void C_Cancel_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
		}

		private void C_OK_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
		}

		public void BindStyles(Styles.ExControlStyles i_Styles)
		{          
			this.C_StyleTree.Nodes.Clear();
			//Root
			TreeNode rowsRoot = new TreeNode("Rows Style");
			rowsRoot.Tag = this._RowsStyle;
			TreeNode colsRoot = new TreeNode("Columns Style");
			colsRoot.Tag = this._ColumnsStyle;
			this.C_StyleTree.Nodes.Add(rowsRoot);
			this.C_StyleTree.Nodes.Add(colsRoot);
			//Rows
			//BandStyle
			TreeNode m_BandStyle = new TreeNode();
			m_BandStyle.Text = "Band Style";
			m_BandStyle.Tag = i_Styles.BandStyle;
			rowsRoot.Nodes.Add(m_BandStyle);
			//HanderStyle
			TreeNode m_HanderStyle = new TreeNode();
			m_HanderStyle.Text = "Header Style";
			m_HanderStyle.Tag = i_Styles.HeaderStyle;
			rowsRoot.Nodes.Add(m_HanderStyle);
			//RowsStyle
			TreeNode m_RowsStyle = new TreeNode();
			m_RowsStyle.Text = "Rows Style";
			m_RowsStyle.Tag = i_Styles.RowStyle;
			//RowsStyl-AlternateSyle            

			TreeNode m_AlternateSyle = new TreeNode();
			m_AlternateSyle.Text = "Alternate Rows Style";
			m_AlternateSyle.Tag = i_Styles.AlternateStyle;
			m_RowsStyle.Nodes.Add(m_AlternateSyle);

            TreeNode m_AlternateInterval = new TreeNode();
            m_AlternateInterval.Text = "AlternateRows Interval";
            m_AlternateInterval.Tag = i_Styles.AlternateIntervals;
            m_RowsStyle.Nodes.Add(m_AlternateInterval);         

			rowsRoot.Nodes.Add(m_RowsStyle);
			//SectionStyle
			TreeNode m_SectionStyle = new TreeNode();
			m_SectionStyle.Text = "Section Style";
			m_SectionStyle.Tag = i_Styles.SectionStyle;
			rowsRoot.Nodes.Add(m_SectionStyle);
			//SectionStyle
			TreeNode m_TotalStyle = new TreeNode();
			m_TotalStyle.Text = "Total Style";
			m_TotalStyle.Tag = i_Styles.TotalStyle;
			rowsRoot.Nodes.Add(m_TotalStyle);
			//Columns
			//RowIndicatorStyle
			TreeNode m_RowIndicator = new TreeNode();
			m_RowIndicator.Text = "Row Numbering Style";
			m_RowIndicator.Tag = i_Styles.RowIndicatorStyle;
			colsRoot.Nodes.Add(m_RowIndicator);
			//06-03-2008@Scott
//			//GroupStyle
//			TreeNode m_GroupStyle = new TreeNode();
//			m_GroupStyle.Text = "Group Style";
//			m_GroupStyle.Tag = i_Styles.GroupStyle;
//			colsRoot.Nodes.Add(m_GroupStyle);
//			//FreqencyStyle
//			TreeNode m_FreqencyStyle = new TreeNode();
//			m_FreqencyStyle.Text = "Freqency Style";
//			m_FreqencyStyle.Tag = i_Styles.FreqencyStyle;
//			colsRoot.Nodes.Add(m_FreqencyStyle);
//			//GroupStyle
//			TreeNode m_PercentStyle = new TreeNode();
//			m_PercentStyle.Text = "Percent Style";
//			m_PercentStyle.Tag = i_Styles.PercentStyle;
//			colsRoot.Nodes.Add(m_PercentStyle);
//			//TotalColStyle
//			TreeNode m_TotalColStyle = new TreeNode();
//			m_TotalColStyle.Text = "Total Style";
//			m_TotalColStyle.Tag = i_Styles.TotalColStyle;
//			colsRoot.Nodes.Add(m_TotalColStyle);
//			//AverageStyle
//			TreeNode m_AverageStyle = new TreeNode();
//			m_AverageStyle.Text = "Average Style";
//			m_AverageStyle.Tag = i_Styles.AverageStyle;
//			colsRoot.Nodes.Add(m_AverageStyle);
			//
			rowsRoot.Expand();
			colsRoot.Expand();
			this.C_StyleTree.SelectedNode = m_RowsStyle;
			this.C_StyleTree.Focus();
		}

		private void C_StyleTree_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			e.Node.ForeColor = Color.Red;
			TreeNode m_SelectedNode = this.C_StyleTree.SelectedNode;
			if(m_SelectedNode==null||m_SelectedNode.Tag==null) return;
			this.C_PropertyGrid.SelectedObject = m_SelectedNode.Tag;
		}

		private void C_StyleTree_BeforeSelect(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
		{
			TreeView m_tv = sender as TreeView;
			if(m_tv.SelectedNode!=null)
			{
				m_tv.SelectedNode.ForeColor = Color.Black;
			}
		}

		private void C_PropertyGrid_PropertyValueChanged(object s, System.Windows.Forms.PropertyValueChangedEventArgs e)
		{
			TreeNode selectedNode = this.C_StyleTree.SelectedNode;

			if(selectedNode.Text == "Rows Style" && selectedNode.Parent != null)
			{
                BasicStyle alternateStyle = (this.C_StyleTree.SelectedNode.Nodes[0].Tag) as BasicStyle;       
				SetStyleValue(alternateStyle,e);
			}
			
			//TreeNode childNode = null;

			if(selectedNode.Parent == null)
			{
				foreach(TreeNode childNode in selectedNode.Nodes)
				{
					BasicStyle rowsStyle = childNode.Tag as BasicStyle;

					SetStyleValue(rowsStyle,e);

					if(childNode.Nodes.Count > 1)
					{
						SetStyleValue(childNode.Nodes[0].Tag as BasicStyle,e);
					}
				}
			}
		}

		private void SetStyleValue(BasicStyle style, System.Windows.Forms.PropertyValueChangedEventArgs e)
		{
			Type type = typeof(BasicStyle);

			PropertyInfo property = type.GetProperty(e.ChangedItem.Label);

			if(property == null) return;

			property.SetValue(style,e.ChangedItem.Value,null);
		}

		private void C_BtnRevert_Click(object sender, System.EventArgs e)
		{
			TreeNode node = this.C_StyleTree.SelectedNode;

			if(node == null) return;

			BasicStyle style = node.Tag as BasicStyle;

			if(style == null) return;

			style.Revert();

			this.C_PropertyGrid.Refresh();
		}
	}
}
