/***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:ConfigExControlStyle.cs
 * Author:Country.Wu [EMail:Webb.Country.Wu@163.com]
 * Create Time:11/29/2007 12:59:26 PM
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

using Webb.Reports.ExControls.Data;
using Webb.Utilities;

namespace Webb.Reports.ExControls.UI
{
	public class ConfigExControlStyle : Webb.Reports.ExControls.UI.ExControlDesignerControlBase
	{
		protected NameForm _SaveFileDialog;
        private ColumnSetupForm _ColumnSetupDialog;

		private Styles.ExControlStyles _CustomStyle;
		private StyleBuilderForm  _CustomBuilderForm;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Label C_StyleLable;
		private System.Windows.Forms.Label C_PrivewLabel;
		private System.Windows.Forms.PictureBox C_PreviewPic;
		private System.Windows.Forms.Panel C_LeftPanel;
		private System.Windows.Forms.Button C_CustomStyle;
		private System.Windows.Forms.TreeView C_AllStyles;
		private System.Windows.Forms.ContextMenu C_MenuStyles;
		private System.Windows.Forms.MenuItem C_MenuItem_Edit;
		private System.Windows.Forms.MenuItem C_MenuItem_Delete;
		private System.Windows.Forms.MenuItem C_MenuItem_Add;
		private System.Windows.Forms.Button C_AddStyle;
		private System.Windows.Forms.Button C_DeleteStyle;
		private System.Windows.Forms.Timer timer;
		private System.Windows.Forms.Button C_RenameStyle;
        private Button C_ColumnSetup;
		private System.ComponentModel.IContainer components = null;

		public ConfigExControlStyle()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();
			// TODO: Add any initialization after the InitializeComponent call
            this._ColumnSetupDialog = new ColumnSetupForm();

			this._SaveFileDialog = new NameForm();

			this._CustomBuilderForm = new StyleBuilderForm();

			this._CustomStyle = new Webb.Reports.ExControls.Styles.ExControlStyles();

			this.Load += new EventHandler(ConfigExControlStyle_Load);

			this.C_PreviewPic.Paint += new PaintEventHandler(C_PreviewPic_Paint);

			this.timer.Enabled = true;
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
			this.C_PreviewPic = new System.Windows.Forms.PictureBox();
			this.C_LeftPanel = new System.Windows.Forms.Panel();
            this.C_ColumnSetup = new System.Windows.Forms.Button();
			this.C_DeleteStyle = new System.Windows.Forms.Button();
			this.C_AddStyle = new System.Windows.Forms.Button();
			this.C_AllStyles = new System.Windows.Forms.TreeView();
			this.C_CustomStyle = new System.Windows.Forms.Button();
			this.C_StyleLable = new System.Windows.Forms.Label();
			this.C_RenameStyle = new System.Windows.Forms.Button();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.C_PrivewLabel = new System.Windows.Forms.Label();
			this.C_MenuStyles = new System.Windows.Forms.ContextMenu();
			this.C_MenuItem_Edit = new System.Windows.Forms.MenuItem();
			this.C_MenuItem_Delete = new System.Windows.Forms.MenuItem();
			this.C_MenuItem_Add = new System.Windows.Forms.MenuItem();
			this.timer = new System.Windows.Forms.Timer(this.components);
			this.C_LeftPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// C_PreviewPic
			// 
			this.C_PreviewPic.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.C_PreviewPic.Dock = System.Windows.Forms.DockStyle.Fill;
			this.C_PreviewPic.Location = new System.Drawing.Point(211, 23);
			this.C_PreviewPic.Name = "C_PreviewPic";
			this.C_PreviewPic.Size = new System.Drawing.Size(541, 337);
			this.C_PreviewPic.TabIndex = 2;
			this.C_PreviewPic.TabStop = false;
			// 
			// C_LeftPanel
			// 
            this.C_LeftPanel.Controls.Add(this.C_ColumnSetup);
			this.C_LeftPanel.Controls.Add(this.C_DeleteStyle);
			this.C_LeftPanel.Controls.Add(this.C_AddStyle);
			this.C_LeftPanel.Controls.Add(this.C_AllStyles);
			this.C_LeftPanel.Controls.Add(this.C_CustomStyle);
			this.C_LeftPanel.Controls.Add(this.C_StyleLable);
			this.C_LeftPanel.Controls.Add(this.C_RenameStyle);
			this.C_LeftPanel.Dock = System.Windows.Forms.DockStyle.Left;
			this.C_LeftPanel.Location = new System.Drawing.Point(0, 0);
			this.C_LeftPanel.Name = "C_LeftPanel";
            this.C_LeftPanel.Size = new System.Drawing.Size(211, 360);
			this.C_LeftPanel.TabIndex = 3;
            // 
            // C_ColumnSetup
            // 
            this.C_ColumnSetup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.C_ColumnSetup.Location = new System.Drawing.Point(102, 328);
            this.C_ColumnSetup.Name = "C_ColumnSetup";
            this.C_ColumnSetup.Size = new System.Drawing.Size(103, 23);
            this.C_ColumnSetup.TabIndex = 7;
            this.C_ColumnSetup.Text = "Column Setup";
            this.C_ColumnSetup.Click += new System.EventHandler(this.C_ColumnSetup_Click);
			// C_DeleteStyle
			// 
			this.C_DeleteStyle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.C_DeleteStyle.Location = new System.Drawing.Point(128, 296);
			this.C_DeleteStyle.Name = "C_DeleteStyle";
			this.C_DeleteStyle.Size = new System.Drawing.Size(60, 23);
			this.C_DeleteStyle.TabIndex = 6;
			this.C_DeleteStyle.Text = "Delete";
			this.C_DeleteStyle.Click += new System.EventHandler(this.C_DeleteStyle_Click);
			// 
			// C_AddStyle
			// 
			this.C_AddStyle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.C_AddStyle.Location = new System.Drawing.Point(72, 296);
			this.C_AddStyle.Name = "C_AddStyle";
			this.C_AddStyle.Size = new System.Drawing.Size(50, 23);
			this.C_AddStyle.TabIndex = 5;
			this.C_AddStyle.Text = "Add";
			this.C_AddStyle.Click += new System.EventHandler(this.C_AddStyle_Click);
			// 
			// C_AllStyles
			// 
			this.C_AllStyles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.C_AllStyles.HideSelection = false;
			this.C_AllStyles.ImageIndex = -1;
			this.C_AllStyles.LabelEdit = true;
			this.C_AllStyles.Location = new System.Drawing.Point(8, 23);
			this.C_AllStyles.Name = "C_AllStyles";
			this.C_AllStyles.SelectedImageIndex = -1;
            this.C_AllStyles.Size = new System.Drawing.Size(197, 265);
			this.C_AllStyles.TabIndex = 4;
			this.C_AllStyles.MouseUp += new System.Windows.Forms.MouseEventHandler(this.C_AllStyles_MouseUp);
			this.C_AllStyles.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.C_AllStyles_AfterSelect);
			// 
			// C_CustomStyle
			// 
			this.C_CustomStyle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.C_CustomStyle.Location = new System.Drawing.Point(16, 296);
			this.C_CustomStyle.Name = "C_CustomStyle";
			this.C_CustomStyle.Size = new System.Drawing.Size(50, 23);
			this.C_CustomStyle.TabIndex = 3;
			this.C_CustomStyle.Text = "Edit";
			this.C_CustomStyle.Click += new System.EventHandler(this.C_CustomStyle_Click);
			// 
			// C_StyleLable
			// 
			this.C_StyleLable.Dock = System.Windows.Forms.DockStyle.Top;
			this.C_StyleLable.Location = new System.Drawing.Point(0, 0);
			this.C_StyleLable.Name = "C_StyleLable";
            this.C_StyleLable.Size = new System.Drawing.Size(211, 23);
			this.C_StyleLable.TabIndex = 2;
			this.C_StyleLable.Text = "Avilable Styles:";
			this.C_StyleLable.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// C_RenameStyle
			// 
			this.C_RenameStyle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.C_RenameStyle.Location = new System.Drawing.Point(16, 328);
			this.C_RenameStyle.Name = "C_RenameStyle";
			this.C_RenameStyle.Size = new System.Drawing.Size(80, 23);
			this.C_RenameStyle.TabIndex = 3;
			this.C_RenameStyle.Text = "Rename";
			this.C_RenameStyle.Click += new System.EventHandler(this.C_RenameStyle_Click);
			// 
			// splitter1
			// 
            this.splitter1.Location = new System.Drawing.Point(211, 0);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(3, 360);
			this.splitter1.TabIndex = 4;
			this.splitter1.TabStop = false;
			// 
			// C_PrivewLabel
			// 
			this.C_PrivewLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.C_PrivewLabel.Location = new System.Drawing.Point(214, 0);
			this.C_PrivewLabel.Name = "C_PrivewLabel";
            this.C_PrivewLabel.Size = new System.Drawing.Size(538, 23);
			this.C_PrivewLabel.TabIndex = 5;
			this.C_PrivewLabel.Text = "Style Preview";
			this.C_PrivewLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// C_MenuStyles
			// 
			this.C_MenuStyles.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.C_MenuItem_Edit,
																						 this.C_MenuItem_Delete,
																						 this.C_MenuItem_Add});
			// 
			// C_MenuItem_Edit
			// 
			this.C_MenuItem_Edit.Index = 0;
			this.C_MenuItem_Edit.Text = "Edit";
			this.C_MenuItem_Edit.Click += new System.EventHandler(this.C_CustomStyle_Click);
			// 
			// C_MenuItem_Delete
			// 
			this.C_MenuItem_Delete.Index = 1;
			this.C_MenuItem_Delete.Text = "Delete";
			this.C_MenuItem_Delete.Click += new System.EventHandler(this.C_DeleteStyle_Click);
			// 
			// C_MenuItem_Add
			// 
			this.C_MenuItem_Add.Index = 2;
			this.C_MenuItem_Add.Text = "Add";
			this.C_MenuItem_Add.Click += new System.EventHandler(this.C_AddStyle_Click);
			// 
			// timer
			// 
			this.timer.Interval = 200;
			this.timer.Tick += new System.EventHandler(this.timer_Tick);
			// 
			// ConfigExControlStyle
			// 
			this.Controls.Add(this.C_PreviewPic);
			this.Controls.Add(this.C_PrivewLabel);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.C_LeftPanel);
			this.Name = "ConfigExControlStyle";
			this.Size = new System.Drawing.Size(752, 360);
			this.C_LeftPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		public override void SetView(Webb.Reports.ExControls.Views.ExControlView i_View)
		{
			if(i_View is Views.GroupView)
			{
				this._CustomStyle.ApplyStyle((i_View as Views.GroupView).Styles);
			}
            if (i_View is Views.GradingView)
            {
                this._CustomStyle.ApplyStyle((i_View as Views.GradingView).Styles);
            }
			
			if(i_View is Views.FieldPanelView)
			{
				this._CustomStyle.ApplyStyle((i_View as Views.FieldPanelView).Styles);
			}

			if(i_View is Views.GridView)
			{
				this._CustomStyle.ApplyStyle((i_View as Views.GridView).Styles);
			}

			if(i_View is Views.StatControlView)
			{
				this._CustomStyle.ApplyStyle((i_View as Views.StatControlView).Styles);
			}

			if(i_View is Views.SimpleGroupView)
			{
				this._CustomStyle.ApplyStyle((i_View as Views.SimpleGroupView).Styles);
			}

			if(i_View is Views.CompactGroupView)
			{
				this._CustomStyle.ApplyStyle((i_View as Views.CompactGroupView).Styles);
			}
			if(i_View is Views.MatrixGroupView)
			{
				this._CustomStyle.ApplyStyle((i_View as Views.MatrixGroupView).Styles);
			}
		}

		public override void UpdateView(Webb.Reports.ExControls.Views.ExControlView i_View)
		{
			if(this.C_AllStyles.Nodes.Count == 0) return;

			if(this.C_AllStyles.SelectedNode == null) return;

			Styles.ExControlStyles style = this.C_AllStyles.SelectedNode.Tag as Styles.ExControlStyles;

			if(style == null) return;

			if(i_View is Views.GroupView)
			{
				(i_View as Views.GroupView).Styles.ApplyStyle(style);
			}
            if (i_View is Views.GradingView)
            {
                (i_View as Views.GradingView).Styles.ApplyStyle(style);
            }

			if(i_View is Views.FieldPanelView)
			{
				(i_View as Views.FieldPanelView).Styles.ApplyStyle(style);
			}

			if(i_View is Views.GridView)
			{
				(i_View as Views.GridView).Styles.ApplyStyle(style);
			}

			if(i_View is Views.StatControlView)
			{
				(i_View as Views.StatControlView).Styles.ApplyStyle(style);
			}

			if(i_View is Views.SimpleGroupView)
			{
				(i_View as Views.SimpleGroupView).Styles.ApplyStyle(style);
			}

			if(i_View is Views.CompactGroupView)
			{
				(i_View as Views.CompactGroupView).Styles.ApplyStyle(style);
			}
			if(i_View is Views.MatrixGroupView)
			{
				(i_View as Views.MatrixGroupView).Styles.ApplyStyle(style);
			}

			this._CustomStyle.ApplyStyle(style);
		}

		/// <summary>
		/// Get complete file path from a style name
		/// </summary>
		/// <param name="strStyleName">style name</param>
		/// <returns>style file path</returns>
		private string GetStylesFilePath(string strStyleName)
		{
            if (strStyleName == "DefaultStyle")     //08-14-2008@Scott
            {
                return Webb.Utility.ApplicationDirectory + strStyleName + ".wrst"; //08-14-2008@Scott
            }
            else
            {
                return Webb.Utility.ApplicationDirectory + Webb.Utility.ExControlStylesPath + "\\" + strStyleName + ".wrst";
            }
		}

		/// <summary>
		/// Get complete file path from a style
		/// </summary>
		/// <param name="style">ExControlStyles</param>
		/// <returns>style file path</returns>
		private string GetStylesFilePath(Styles.ExControlStyles style)
		{
			return this.GetStylesFilePath(style.StyleName);
		}

		/// <summary>
		/// Get default style file folder
		/// </summary>
		/// <returns></returns>
		private string GetStylesFileFolder()
		{
            return Webb.Utility.ApplicationDirectory + Webb.Utility.ExControlStylesPath + "\\";
		}

		private void LoadCurrentStyles()
		{
			TreeNode node = new TreeNode("Current");

			node.Tag = this._CustomStyle;

			this.C_AllStyles.Nodes.Insert(0,node);
		}

        private void LoadDefaultStyles()
        {
            TreeNode node = new TreeNode("Default");

            Styles.ExControlStyles styles = new Webb.Reports.ExControls.Styles.ExControlStyles();

            styles.LoadDefaultStyle();

            node.Tag = styles;

            this.C_AllStyles.Nodes.Add(node);
        }

		private void LoadStyles(Styles.ExControlStyles selectedStyle)
		{
			this.C_AllStyles.Nodes.Clear();

			this.LoadCurrentStyles();

            this.LoadDefaultStyles();

			string strFolder = this.GetStylesFileFolder();

			if(!System.IO.Directory.Exists(strFolder)) return;
			
			string[] strFiles = System.IO.Directory.GetFiles(strFolder);
			
			foreach(string strFile in strFiles)
			{
				Styles.ExControlStyles style = new Styles.ExControlStyles();

				style.Load(strFile);

				TreeNode node = new TreeNode(style.StyleName);

				node.Tag = style;

				this.C_AllStyles.Nodes.Add(node);

				if(selectedStyle != null && style.StyleName == selectedStyle.StyleName)
				{
					this.C_AllStyles.SelectedNode = node;
				}
			}

			if(this.C_AllStyles.Nodes.Count > 0 && selectedStyle == null)
			{
				this.C_AllStyles.SelectedNode = this.C_AllStyles.Nodes[0];
			}
		}

		//Delete
		private void C_DeleteStyle_Click(object sender, System.EventArgs e)
		{
			TreeNode node = this.C_AllStyles.SelectedNode;

			if(node == null || node.Index < 2) return;	//can't remove current style node and default style node    //08-14-2008@Scott

			Styles.ExControlStyles style = node.Tag as Styles.ExControlStyles;
			
			string strFilePath = this.GetStylesFilePath(style);
			
			if(MessageBox.Show(this,string.Format("Do you want to delete [{0}]?",style.StyleName),"Warnning",MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				if(System.IO.File.Exists(strFilePath))
				{
					System.IO.File.Delete(strFilePath);	
				}
				this.C_AllStyles.Nodes.Remove(node);
			}
		}

		//Add
		private void C_AddStyle_Click(object sender, System.EventArgs e)
		{
			this.CreateStylesFolder();

			Styles.ExControlStyles style = new Styles.ExControlStyles();
			
			this._CustomBuilderForm.BindStyles(style);

			if(this._SaveFileDialog.ShowDialog(this) == DialogResult.OK)
			{
				style.StyleName = this._SaveFileDialog.FileName;

				string strFile = this.GetStylesFilePath(style.StyleName);

				if(System.IO.File.Exists(strFile))
				{
					if(MessageBox.Show(this,"The style file is exist , do you want overwrite it?","Warnning",MessageBoxButtons.YesNo) == DialogResult.No)
					{
						return;
					}
				}

				style.Save(strFile);
				
				this.LoadStyles(style);
			}
		}

		private void CreateStylesFolder()
		{
			string strFolder = this.GetStylesFileFolder();
				
			if(!System.IO.Directory.Exists(strFolder)) 
			{
				System.IO.Directory.CreateDirectory(strFolder);
			}
		}

		//Edit
		private void C_CustomStyle_Click(object sender, System.EventArgs e)
		{
			TreeNode node = this.C_AllStyles.SelectedNode;

			if(node == null) return;

			Styles.ExControlStyles style = node.Tag as Styles.ExControlStyles;
			
			if(style == null) return;

			this._CustomBuilderForm.BindStyles(style);
			
			if(this._CustomBuilderForm.ShowDialog(this) == DialogResult.OK)
			{
				if(this.C_AllStyles.SelectedNode.Index == 0)	//current style node	
				{
					return;
				}
				style.Save(this.GetStylesFilePath(style));
			}

    		if(node.Text=="Default")style.Load(this.GetStylesFilePath(style));
		}

		private void C_AllStyles_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(e.Button == MouseButtons.Right)
			{
				Point point = new Point(e.X,e.Y);

				TreeNode node = this.C_AllStyles.GetNodeAt(point);
				
				this.C_AllStyles.SelectedNode = node;
				
				if(node!=null)
				{
					this.C_MenuItem_Delete.Visible = true;
					this.C_MenuItem_Edit.Visible = true;
				}
				else
				{
					this.C_MenuItem_Delete.Visible = false;
					this.C_MenuItem_Edit.Visible = false;
				}
				this.C_MenuStyles.Show(this.C_AllStyles,point);
			}
		}

		private void ConfigExControlStyle_Load(object sender, EventArgs e)
		{
			this.LoadStyles(null);
		}

		private void C_AllStyles_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			
		}

		private void C_PreviewPic_Paint(object sender, PaintEventArgs e)
		{
			TreeNode node = this.C_AllStyles.SelectedNode;

			if(node == null) return;

			Styles.ExControlStyles style = node.Tag as Styles.ExControlStyles;
		
			if(style == null) return;

			Rectangle rect = e.ClipRectangle;
			
			Pen pen = new Pen(Color.Red,1.0f);

			rect.Width --;
			rect.Height --;

			e.Graphics.DrawRectangle(pen,rect);
		}

		private void timer_Tick(object sender, System.EventArgs e)
		{
			this.C_PreviewPic.Refresh();
		}

		private void C_RenameStyle_Click(object sender, System.EventArgs e)
		{
			TreeNode node = this.C_AllStyles.SelectedNode;

			if(node == null) return;

			Styles.ExControlStyles style = node.Tag as Styles.ExControlStyles;

			if(style == null) return;

			string strOldFile = this.GetStylesFilePath(style.StyleName);

			if(this._SaveFileDialog.ShowDialog(this) == DialogResult.OK)
			{
				style.StyleName = this._SaveFileDialog.FileName;

				string strFile = this.GetStylesFilePath(style.StyleName);

				if(System.IO.File.Exists(strFile))
				{
					MessageBox.Show("The style file is exist");
				}
				else
				{
					System.IO.File.Delete(strOldFile);

					style.Save(strFile);

					this.LoadStyles(style);
				}	
			}
		}

        private void C_ColumnSetup_Click(object sender, EventArgs e)
        {
            this._ColumnSetupDialog.ShowDialog(this);
        }
	}
}
