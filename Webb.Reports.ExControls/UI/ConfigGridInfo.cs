using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Collections.Generic;

using Webb.Reports.ExControls;
using Webb.Reports.ExControls.Data;
using Webb.Reports.ExControls.Views;

namespace Webb.Reports.ExControls.UI
{
	/// <summary>
	/// Summary description for ConfigGridInfo.
	/// </summary>
	public class ConfigGridInfo : Webb.Reports.ExControls.UI.ExControlDesignerControlBase
	{
		private System.Windows.Forms.PropertyGrid C_PropertyGrid;
		private GridInfo _GridInfo;
		private System.Windows.Forms.Splitter C_SplitterLR;
		private System.Windows.Forms.Panel C_PanelCtrls;
		private System.Windows.Forms.Splitter C_SplitterTB;
        private System.Windows.Forms.Panel C_PanelMain;
		private System.Windows.Forms.Button C_BtnMoveUp;
		private System.Windows.Forms.Button C_BtnMoveDown;
        private System.Windows.Forms.Button C_BtnRevertStyle;
        private Panel palAllColumns;
        private TextBox TBXEdit;
        private ListBox C_LBFields;
        private ComboBox cmbAllFieldCategories;
        private IContainer components;

        private Hashtable HashCategories = new Hashtable();
        private Panel palSelect;
        private ListBox C_LBSelFields;
        private Label lblSelectedColumns;
        private Splitter splitter1;
        private Panel palButtons;
        private Button C_BtnRemove;
        private Button C_BtnAdd;
        private Button BtnAddCombinedGridColumn;
        string strCategory = string.Empty;

		public ConfigGridInfo()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
			this._GridInfo = new GridInfo();
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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.C_PropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.C_SplitterLR = new System.Windows.Forms.Splitter();
            this.C_PanelCtrls = new System.Windows.Forms.Panel();
            this.BtnAddCombinedGridColumn = new System.Windows.Forms.Button();
            this.C_BtnMoveDown = new System.Windows.Forms.Button();
            this.C_BtnMoveUp = new System.Windows.Forms.Button();
            this.C_SplitterTB = new System.Windows.Forms.Splitter();
            this.C_PanelMain = new System.Windows.Forms.Panel();
            this.palSelect = new System.Windows.Forms.Panel();
            this.C_LBSelFields = new System.Windows.Forms.ListBox();
            this.lblSelectedColumns = new System.Windows.Forms.Label();
            this.palButtons = new System.Windows.Forms.Panel();
            this.C_BtnRemove = new System.Windows.Forms.Button();
            this.C_BtnAdd = new System.Windows.Forms.Button();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.palAllColumns = new System.Windows.Forms.Panel();
            this.TBXEdit = new System.Windows.Forms.TextBox();
            this.C_LBFields = new System.Windows.Forms.ListBox();
            this.cmbAllFieldCategories = new System.Windows.Forms.ComboBox();
            this.C_BtnRevertStyle = new System.Windows.Forms.Button();
            this.C_PanelCtrls.SuspendLayout();
            this.C_PanelMain.SuspendLayout();
            this.palSelect.SuspendLayout();
            this.palButtons.SuspendLayout();
            this.palAllColumns.SuspendLayout();
            this.SuspendLayout();
            // 
            // C_PropertyGrid
            // 
            this.C_PropertyGrid.Dock = System.Windows.Forms.DockStyle.Right;
            this.C_PropertyGrid.LineColor = System.Drawing.SystemColors.ScrollBar;
            this.C_PropertyGrid.Location = new System.Drawing.Point(464, 0);
            this.C_PropertyGrid.Name = "C_PropertyGrid";
            this.C_PropertyGrid.Size = new System.Drawing.Size(200, 384);
            this.C_PropertyGrid.TabIndex = 0;
            this.C_PropertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.C_PropertyGrid_PropertyValueChanged);
            // 
            // C_SplitterLR
            // 
            this.C_SplitterLR.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.C_SplitterLR.Dock = System.Windows.Forms.DockStyle.Right;
            this.C_SplitterLR.Location = new System.Drawing.Point(461, 0);
            this.C_SplitterLR.Name = "C_SplitterLR";
            this.C_SplitterLR.Size = new System.Drawing.Size(3, 384);
            this.C_SplitterLR.TabIndex = 1;
            this.C_SplitterLR.TabStop = false;
            // 
            // C_PanelCtrls
            // 
            this.C_PanelCtrls.Controls.Add(this.BtnAddCombinedGridColumn);
            this.C_PanelCtrls.Controls.Add(this.C_BtnMoveDown);
            this.C_PanelCtrls.Controls.Add(this.C_BtnMoveUp);
            this.C_PanelCtrls.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.C_PanelCtrls.Location = new System.Drawing.Point(0, 344);
            this.C_PanelCtrls.Name = "C_PanelCtrls";
            this.C_PanelCtrls.Size = new System.Drawing.Size(461, 40);
            this.C_PanelCtrls.TabIndex = 2;
            // 
            // BtnAddCombinedGridColumn
            // 
            this.BtnAddCombinedGridColumn.Location = new System.Drawing.Point(5, 11);
            this.BtnAddCombinedGridColumn.Name = "BtnAddCombinedGridColumn";
            this.BtnAddCombinedGridColumn.Size = new System.Drawing.Size(192, 23);
            this.BtnAddCombinedGridColumn.TabIndex = 2;
            this.BtnAddCombinedGridColumn.Text = "Add Compute Column";
            this.BtnAddCombinedGridColumn.UseVisualStyleBackColor = true;
            this.BtnAddCombinedGridColumn.Click += new System.EventHandler(this.BtnAddCombinedGridColumn_Click);
            // 
            // C_BtnMoveDown
            // 
            this.C_BtnMoveDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.C_BtnMoveDown.Location = new System.Drawing.Point(367, 12);
            this.C_BtnMoveDown.Name = "C_BtnMoveDown";
            this.C_BtnMoveDown.Size = new System.Drawing.Size(88, 23);
            this.C_BtnMoveDown.TabIndex = 1;
            this.C_BtnMoveDown.Text = "Move Down";
            this.C_BtnMoveDown.Click += new System.EventHandler(this.C_BtnMoveDown_Click);
            // 
            // C_BtnMoveUp
            // 
            this.C_BtnMoveUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.C_BtnMoveUp.Location = new System.Drawing.Point(280, 12);
            this.C_BtnMoveUp.Name = "C_BtnMoveUp";
            this.C_BtnMoveUp.Size = new System.Drawing.Size(72, 23);
            this.C_BtnMoveUp.TabIndex = 0;
            this.C_BtnMoveUp.Text = "Move Up";
            this.C_BtnMoveUp.Click += new System.EventHandler(this.C_BtnMoveUp_Click);
            // 
            // C_SplitterTB
            // 
            this.C_SplitterTB.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.C_SplitterTB.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.C_SplitterTB.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.C_SplitterTB.Location = new System.Drawing.Point(0, 341);
            this.C_SplitterTB.Name = "C_SplitterTB";
            this.C_SplitterTB.Size = new System.Drawing.Size(461, 3);
            this.C_SplitterTB.TabIndex = 3;
            this.C_SplitterTB.TabStop = false;
            // 
            // C_PanelMain
            // 
            this.C_PanelMain.Controls.Add(this.palSelect);
            this.C_PanelMain.Controls.Add(this.palButtons);
            this.C_PanelMain.Controls.Add(this.splitter1);
            this.C_PanelMain.Controls.Add(this.palAllColumns);
            this.C_PanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.C_PanelMain.Location = new System.Drawing.Point(0, 0);
            this.C_PanelMain.Name = "C_PanelMain";
            this.C_PanelMain.Size = new System.Drawing.Size(461, 341);
            this.C_PanelMain.TabIndex = 4;
            this.C_PanelMain.SizeChanged += new System.EventHandler(this.C_PanelMain_SizeChanged);
            // 
            // palSelect
            // 
            this.palSelect.Controls.Add(this.C_LBSelFields);
            this.palSelect.Controls.Add(this.lblSelectedColumns);
            this.palSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.palSelect.Location = new System.Drawing.Point(294, 0);
            this.palSelect.Name = "palSelect";
            this.palSelect.Size = new System.Drawing.Size(167, 341);
            this.palSelect.TabIndex = 13;
            // 
            // C_LBSelFields
            // 
            this.C_LBSelFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.C_LBSelFields.HorizontalScrollbar = true;
            this.C_LBSelFields.ItemHeight = 14;
            this.C_LBSelFields.Location = new System.Drawing.Point(0, 19);
            this.C_LBSelFields.Name = "C_LBSelFields";
            this.C_LBSelFields.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.C_LBSelFields.Size = new System.Drawing.Size(167, 312);
            this.C_LBSelFields.TabIndex = 2;
            this.C_LBSelFields.DoubleClick += new System.EventHandler(this.C_LBSelFields_DoubleClick);
            this.C_LBSelFields.SelectedValueChanged += new System.EventHandler(this.C_LBSelFields_SelectedValueChanged);
            // 
            // lblSelectedColumns
            // 
            this.lblSelectedColumns.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSelectedColumns.Location = new System.Drawing.Point(0, 0);
            this.lblSelectedColumns.Name = "lblSelectedColumns";
            this.lblSelectedColumns.Size = new System.Drawing.Size(167, 19);
            this.lblSelectedColumns.TabIndex = 0;
            this.lblSelectedColumns.Text = "Columns In Report";
            this.lblSelectedColumns.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // palButtons
            // 
            this.palButtons.Controls.Add(this.C_BtnRemove);
            this.palButtons.Controls.Add(this.C_BtnAdd);
            this.palButtons.Dock = System.Windows.Forms.DockStyle.Left;
            this.palButtons.Location = new System.Drawing.Point(213, 0);
            this.palButtons.Name = "palButtons";
            this.palButtons.Size = new System.Drawing.Size(81, 341);
            this.palButtons.TabIndex = 14;
            // 
            // C_BtnRemove
            // 
            this.C_BtnRemove.Location = new System.Drawing.Point(7, 169);
            this.C_BtnRemove.Name = "C_BtnRemove";
            this.C_BtnRemove.Size = new System.Drawing.Size(57, 23);
            this.C_BtnRemove.TabIndex = 1;
            this.C_BtnRemove.Text = "<--";
            this.C_BtnRemove.UseVisualStyleBackColor = true;
            this.C_BtnRemove.Click += new System.EventHandler(this.C_BtnRemove_Click);
            // 
            // C_BtnAdd
            // 
            this.C_BtnAdd.Location = new System.Drawing.Point(7, 89);
            this.C_BtnAdd.Name = "C_BtnAdd";
            this.C_BtnAdd.Size = new System.Drawing.Size(57, 23);
            this.C_BtnAdd.TabIndex = 0;
            this.C_BtnAdd.Text = "-->";
            this.C_BtnAdd.UseVisualStyleBackColor = true;
            this.C_BtnAdd.Click += new System.EventHandler(this.C_BtnAdd_Click);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(210, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 341);
            this.splitter1.TabIndex = 10;
            this.splitter1.TabStop = false;
            // 
            // palAllColumns
            // 
            this.palAllColumns.Controls.Add(this.TBXEdit);
            this.palAllColumns.Controls.Add(this.C_LBFields);
            this.palAllColumns.Controls.Add(this.cmbAllFieldCategories);
            this.palAllColumns.Dock = System.Windows.Forms.DockStyle.Left;
            this.palAllColumns.Location = new System.Drawing.Point(0, 0);
            this.palAllColumns.Name = "palAllColumns";
            this.palAllColumns.Size = new System.Drawing.Size(210, 341);
            this.palAllColumns.TabIndex = 5;
            // 
            // TBXEdit
            // 
            this.TBXEdit.Location = new System.Drawing.Point(2, 24);
            this.TBXEdit.Name = "TBXEdit";
            this.TBXEdit.Size = new System.Drawing.Size(194, 22);
            this.TBXEdit.TabIndex = 7;
            this.TBXEdit.Visible = false;
            this.TBXEdit.Leave += new System.EventHandler(this.TBXEdit_Leave);
            // 
            // C_LBFields
            // 
            this.C_LBFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.C_LBFields.HorizontalScrollbar = true;
            this.C_LBFields.ItemHeight = 14;
            this.C_LBFields.Location = new System.Drawing.Point(0, 22);
            this.C_LBFields.Name = "C_LBFields";
            this.C_LBFields.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.C_LBFields.Size = new System.Drawing.Size(210, 312);
            this.C_LBFields.Sorted = true;
            this.C_LBFields.TabIndex = 6;
            this.C_LBFields.DoubleClick += new System.EventHandler(this.C_LBFields_DoubleClick);
            // 
            // cmbAllFieldCategories
            // 
            this.cmbAllFieldCategories.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmbAllFieldCategories.DropDownHeight = 300;
            this.cmbAllFieldCategories.FormattingEnabled = true;
            this.cmbAllFieldCategories.IntegralHeight = false;
            this.cmbAllFieldCategories.Location = new System.Drawing.Point(0, 0);
            this.cmbAllFieldCategories.MaxDropDownItems = 18;
            this.cmbAllFieldCategories.Name = "cmbAllFieldCategories";
            this.cmbAllFieldCategories.Size = new System.Drawing.Size(210, 22);
            this.cmbAllFieldCategories.TabIndex = 5;
            this.cmbAllFieldCategories.TextChanged += new System.EventHandler(this.cmbAllFieldCategories_TextChanged);
            // 
            // C_BtnRevertStyle
            // 
            this.C_BtnRevertStyle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.C_BtnRevertStyle.Location = new System.Drawing.Point(557, 350);
            this.C_BtnRevertStyle.Name = "C_BtnRevertStyle";
            this.C_BtnRevertStyle.Size = new System.Drawing.Size(96, 23);
            this.C_BtnRevertStyle.TabIndex = 5;
            this.C_BtnRevertStyle.Text = "Revert Style";
            this.C_BtnRevertStyle.Click += new System.EventHandler(this.C_BtnRevertStyle_Click);
            // 
            // ConfigGridInfo
            // 
            this.Controls.Add(this.C_BtnRevertStyle);
            this.Controls.Add(this.C_PanelMain);
            this.Controls.Add(this.C_SplitterTB);
            this.Controls.Add(this.C_PanelCtrls);
            this.Controls.Add(this.C_SplitterLR);
            this.Controls.Add(this.C_PropertyGrid);
            this.Name = "ConfigGridInfo";
            this.Size = new System.Drawing.Size(664, 384);
            this.Load += new System.EventHandler(this.ConfigGridInfo_Load);
            this.C_PanelCtrls.ResumeLayout(false);
            this.C_PanelMain.ResumeLayout(false);
            this.palSelect.ResumeLayout(false);
            this.palButtons.ResumeLayout(false);
            this.palAllColumns.ResumeLayout(false);
            this.palAllColumns.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion
	
		public override void UpdateView(ExControlView i_View)
		{
            if (!(i_View is GridView || i_View is HorizontalGridView || i_View is FieldPanelView || i_View is CompactGroupView || i_View is MatrixGroupView)) return;   //Added this code at 2008-11-20 15:24:38@Simon
			
			this._GridInfo.Columns.Clear();
			
			foreach(object value in this.C_LBSelFields.Items)
			{
				if(value is GridColumn)
				{
					this._GridInfo.Columns.Add(value as GridColumn);
				}
			}
			if(i_View is GridView)
			{
                int Start = (i_View as GridView).ResolveIndentStartCol();

                (i_View as GridView).UpdateVirtualGroupInfoWidth(ref Start, (i_View as GridView).RootGroupInfo, (i_View as GridView).ColumnsWidth);

                this._GridInfo.UpdateColumnsWidth((i_View as GridView).ColumnsWidth, Start);	//06-30-2008@Scott                

				(i_View as GridView).GridInfo.Apply(this._GridInfo);
			}
            else if (i_View is HorizontalGridView)
            {//10-11-2011@Scott
                int Start = (i_View as HorizontalGridView).ResolveIndentStartCol();

                (i_View as HorizontalGridView).UpdateVirtualGroupInfoWidth(ref Start, (i_View as HorizontalGridView).RootGroupInfo, (i_View as HorizontalGridView).ColumnsWidth);

                this._GridInfo.UpdateColumnsWidth((i_View as HorizontalGridView).ColumnsWidth, Start);	//06-30-2008@Scott                

                (i_View as HorizontalGridView).GridInfo.Apply(this._GridInfo);
            }
			else if(i_View is FieldPanelView)
			{
				(i_View as FieldPanelView).FieldGridInfo.Apply(this._GridInfo);   //Added this code at 2008-11-20 15:24:44@Simon
			}
            else if(i_View is CompactGroupView)
			{
				(i_View as CompactGroupView).GridInfo.Apply(this._GridInfo);   //Added this code at 2008-11-20 15:24:44@Simon
			}
			else if(i_View is MatrixGroupView)
			{
				(i_View as MatrixGroupView).GridInfo.Apply(this._GridInfo);   //Added this code at 2008-11-20 15:24:44@Simon
			}
		}

		public override void SetView(ExControlView i_View)
		{	
			 if(!(i_View is GridView || i_View is HorizontalGridView ||i_View is FieldPanelView||i_View is CompactGroupView||i_View is MatrixGroupView)) return;
		
            if(i_View is GridView)
			{
			    this._GridInfo.Apply((i_View as GridView).GridInfo);

                int Start = (i_View as GridView).ResolveIndentStartCol();

                (i_View as GridView).ApplyVirtualGroupInfoWidth(ref Start, (i_View as GridView).RootGroupInfo, (i_View as GridView).ColumnsWidth);

				this._GridInfo.ApplyColumnsWidth((i_View as GridView).ColumnsWidth,Start);	//06-30-2008@Scott
			}
            else if (i_View is HorizontalGridView)
            {//10-11-2011@Scott
                this._GridInfo.Apply((i_View as HorizontalGridView).GridInfo);

                int Start = (i_View as HorizontalGridView).ResolveIndentStartCol();

                (i_View as HorizontalGridView).ApplyVirtualGroupInfoWidth(ref Start, (i_View as HorizontalGridView).RootGroupInfo, (i_View as HorizontalGridView).ColumnsWidth);

                this._GridInfo.ApplyColumnsWidth((i_View as HorizontalGridView).ColumnsWidth, Start);	//06-30-2008@Scott
            }
			else if(i_View is FieldPanelView)
			{		
				this._GridInfo.Apply((i_View as FieldPanelView).FieldGridInfo);   //Added this code at 2008-11-20 15:24:44@Simon
			}	
			 else if(i_View is CompactGroupView)
			{
				this._GridInfo.Apply((i_View as CompactGroupView).GridInfo);
			}
			else if(i_View is MatrixGroupView)
			{
				this._GridInfo.Apply((i_View as MatrixGroupView).GridInfo);
			}

			this.C_LBSelFields.Items.Clear();

			this.C_LBFields.Items.Clear();
			
            //foreach(object value in Webb.Data.PublicDBFieldConverter.AvialableFields)
            //{
            //    GridColumn column = new GridColumn(value.ToString());

            //    this.C_LBFields.Items.Add(column);
            //}

            this.SetALLList(this._GridInfo);  //Add this code at 2011-5-3 15:01:38@simon

			foreach(GridColumn column in this._GridInfo.Columns)
			{
				this.C_LBSelFields.Items.Add(column);
				
				object value = this.GetItemByName(this.C_LBFields, column.ToString());

				if(value != null && value.ToString() != string.Empty) this.C_LBFields.Items.Remove(value);
			}

			this.C_PropertyGrid.SelectedObject = this._GridInfo;
		}

		private object GetItemByName(ListBox lb, string strName)
		{
			foreach(object value in lb.Items)
			{
				if(value.ToString() == strName)
				{
					return value;
				}
			}

			return null;
		}

		private void ConfigGridInfo_Load(object sender, System.EventArgs e)
		{
			
		}

		private void C_PanelMain_SizeChanged(object sender, System.EventArgs e)
		{
			Size size = (sender as Control).Size;

            int space = 213;

			this.C_LBSelFields.Left = (size.Width - space)/2;

			this.C_LBFields.Width = this.C_LBSelFields.Width = (size.Width - space)/2;
					
		}
        private void SetALLList(GridInfo gridInfo)
        {
            this.C_LBFields.Items.Clear();

            Webb.Reports.DataProvider.WebbDataProvider PublicDataProvider = Webb.Reports.DataProvider.VideoPlayBackManager.PublicDBProvider;

            if (PublicDataProvider != null && PublicDataProvider.DBSourceConfig != null && PublicDataProvider.DBSourceConfig.WebbDBType== Webb.Reports.DataProvider.WebbDBTypes.CoachCRM)
            {
                #region  CoachCRM List
                DataSet ds = Webb.Reports.DataProvider.VideoPlayBackManager.DataSource;

                if (ds != null && ds.Tables.Count > 1)
                {
                    #region Have Structure

                    this.cmbAllFieldCategories.Visible = true;

                    this.lblSelectedColumns.Visible = true;

                    this.TBXEdit.Visible = false;

                    this.TBXEdit.Location = this.C_LBFields.Location;

                    HashCategories.Clear();

                    List<string> categories = new List<string>();

                    string strCategoriesName = ds.Tables[0].TableName;

                    categories.Add(strCategoriesName);

                    GridColumnCollection colfieldsInAllcategories = new GridColumnCollection();
                   
                    HashCategories.Add(strCategoriesName, colfieldsInAllcategories);

                    foreach (DataRow dr in ds.Tables[1].Rows)
                    {
                        string strValue = string.Empty;

                        if (dr["CurrentField"] == null || (dr["CurrentField"] is System.DBNull))
                        {
                            continue;
                        }

                        string strTableName = dr["Category"].ToString();

                        strValue = dr["CurrentField"].ToString();

                        string strDefaultHeader = dr["DefaultHeader"].ToString();

                        GridColumnCollection colfieldList;

                        GridColumn column = new GridColumn(strValue, strDefaultHeader);

                        column.Description = strDefaultHeader;

                        if (HashCategories.Contains(strTableName))
                        {
                            colfieldList = (GridColumnCollection)HashCategories[strTableName];

                            if (!colfieldList.Contains(strValue))
                            {
                                colfieldList.Add(column);
                            }
                        }
                        else
                        {
                            colfieldList = new GridColumnCollection();

                            colfieldList.Add(column);

                            categories.Add(strTableName);

                            HashCategories.Add(strTableName, colfieldList);
                        }

                        if (!colfieldsInAllcategories.Contains(strValue)) colfieldsInAllcategories.Add(column);
                    }

                    this.cmbAllFieldCategories.Items.Clear();

                    this.cmbAllFieldCategories.Text = string.Empty;

                    foreach (string strKey in categories)
                    {
                        this.cmbAllFieldCategories.Items.Add(strKey);
                    }

                    this.cmbAllFieldCategories.SelectedIndex = 0;

                    #endregion
                }
                else
                {
                    this.cmbAllFieldCategories.Visible = false;

                    this.lblSelectedColumns.Visible = false;

                    this.TBXEdit.Visible = false;

                    this.TBXEdit.Location = this.C_LBFields.Location;

                    foreach (string strField in Webb.Data.PublicDBFieldConverter.AvialableFields)
                    {
                        if (gridInfo.Columns.Contains(strField)) continue;

                        GridColumn column = new GridColumn(strField);

                        this.C_LBFields.Items.Add(column);
                    }
                }
                #endregion
            }
            else
            {
                this.cmbAllFieldCategories.Visible = false;

                this.lblSelectedColumns.Visible = false;

                this.TBXEdit.Visible = false;

                this.TBXEdit.Location = this.C_LBFields.Location;

                foreach (string strField in Webb.Data.PublicDBFieldConverter.AvialableFields)
                {
                    if (gridInfo.Columns.Contains(strField)) continue;

                    GridColumn column = new GridColumn(strField);

                    this.C_LBFields.Items.Add(column);
                }
            }     
        }

		private void C_BtnAdd_Click(object sender, System.EventArgs e)
		{
			ArrayList selected = new ArrayList();

			foreach(object value in this.C_LBFields.SelectedItems)
			{
				if(value != null && value.ToString() == string.Empty)
				{
					selected.Add(new GridColumn(string.Empty));
				}
				else
				{
					selected.Add(value);
				}
			}

			foreach(object value in selected)
			{
				if(value.ToString() != string.Empty)
				{
					this.AddItem(this.C_LBSelFields,value);

					this.C_LBFields.Items.Remove(value);
				}
				else
				{
					this.C_LBSelFields.Items.Add(value);
				}
			}

			this.C_LBSelFields.SelectedIndex = -1;
		}

		private void C_BtnRemove_Click(object sender, System.EventArgs e)
		{
			ArrayList selected = new ArrayList();

			foreach(object value in this.C_LBSelFields.SelectedItems)
			{
				selected.Add(value);
			}
            this.C_LBSelFields.SelectedIndex = -1;

			foreach(object value in selected)
			{
				this.AddItem(this.C_LBFields,value);
				this.C_LBSelFields.Items.Remove(value);
			}

			
			this.C_PropertyGrid.SelectedObject = this._GridInfo;
		}

		private void C_LBFields_DoubleClick(object sender, System.EventArgs e)
		{
			object value = this.C_LBFields.SelectedItem;

			if(value == null) return;

			if(value.ToString() != string.Empty)
			{
				this.AddItem(this.C_LBSelFields,value);
				
				this.C_LBFields.Items.Remove(value);
			}
			else
			{
				// Read this code at 2009-2-12 15:18:05@brian
//				this.C_LBSelFields.Items.Add(new GridColumn(string.Empty));
				// Read this code at 2009-2-12 15:18:09@brian
				this.TBXEdit.Visible= true;
                this.TBXEdit.Focus();
			}

			this.C_LBSelFields.SelectedIndex = -1;
		}

		private void C_LBSelFields_DoubleClick(object sender, System.EventArgs e)
		{
			object value = this.C_LBSelFields.SelectedItem;

			if(value == null) return;

			this.AddItem(this.C_LBFields,value);

			this.C_LBSelFields.Items.Remove(value);

			this.C_LBSelFields.SelectedIndex = -1;
			this.C_PropertyGrid.SelectedObject = this._GridInfo;
		}

		private void AddItem(ListBox lb, object item)
		{
			object value = this.GetItemByName(lb, item.ToString());

			if(value != null) return;
			
			lb.Items.Add(item);
		}
// Read this code at 2009-2-12 15:52:59@brian
//		private void C_LBSelFields_SelectedValueChanged(object sender, System.EventArgs e)
//		{//06-30-2008@Scott edit multiply properties
//			if(this.C_LBSelFields.SelectedItems.Count <= 0) return;
//
//			object[] items = new object[this.C_LBSelFields.SelectedItems.Count];
//
//			int index = 0;
//
//			foreach(object item in this.C_LBSelFields.SelectedItems)
//			{
//				if(item is GridColumn && index < this.C_LBSelFields.SelectedItems.Count)
//				{
//					items.SetValue(item as GridColumn, index++);
//				}
//				
//			}
//
//			this.C_PropertyGrid.SelectedObjects = items;
//		}
// Read this code at 2009-2-12 15:53:04@brian
		private void C_LBSelFields_SelectedValueChanged(object sender, System.EventArgs e)
		{//06-30-2008@Scott edit multiply properties
			if(this.C_LBSelFields.SelectedItems.Count <= 0) return;

			object[] items = new object[this.C_LBSelFields.SelectedItems.Count];

			int index = 0;

			foreach(object item in this.C_LBSelFields.SelectedItems)
			{
				if(item is GridColumn && index < this.C_LBSelFields.SelectedItems.Count)
				{
					items.SetValue(item as GridColumn, index++);
				}
				else
				{
					items.SetValue(item as GridColumn, index++);
				}
			}

			this.C_PropertyGrid.SelectedObjects = items;
		}
		private void C_BtnMoveUp_Click(object sender, System.EventArgs e)
		{
			int index = this.C_LBSelFields.SelectedIndex;

			if(index <= 0) return;

			object value = this.C_LBSelFields.Items[index];

			this.C_LBSelFields.Items.RemoveAt(index);

			this.C_LBSelFields.Items.Insert(index - 1,value);

			this.C_LBSelFields.SelectedIndex = index - 1;
		}

		private void C_BtnMoveDown_Click(object sender, System.EventArgs e)
		{
			int index = this.C_LBSelFields.SelectedIndex;

			if(index < 0 || index > this.C_LBSelFields.Items.Count - 2) return;

			object value = this.C_LBSelFields.Items[index];

			this.C_LBSelFields.Items.RemoveAt(index);

			this.C_LBSelFields.Items.Insert(index + 1,value);

			this.C_LBSelFields.SelectedIndex = index + 1;
		}

		private void C_LBFields_SelectedValueChanged(object sender, System.EventArgs e)
		{
			this.C_PropertyGrid.SelectedObject = this._GridInfo;
		}

		private void C_BtnRevertStyle_Click(object sender, System.EventArgs e)
		{
			object value = this.C_LBSelFields.SelectedItem;

			if(value is GridColumn)
			{
				GridColumn gridColumn = value as GridColumn;

				gridColumn.Style.Revert();
			}
		}

		private void TBXEdit_Leave(object sender, System.EventArgs e)
		{
			if(TBXEdit.Text!=string.Empty)
			{
				GridColumn column = new GridColumn(TBXEdit.Text.ToString());
				this.C_LBFields.Items.Add(column);
				TBXEdit.Text="";
			}
			TBXEdit.Visible=false;
		}

        private void cmbAllFieldCategories_TextChanged(object sender, EventArgs e)
        {
            string strText = this.cmbAllFieldCategories.Text.Trim();

            this.C_LBFields.Items.Clear();

            if (this.HashCategories.Contains(strText))
            {
                GridColumnCollection allFieldColumns = this.HashCategories[strText] as GridColumnCollection;

                foreach (GridColumn grdColumn in allFieldColumns)
                {
                    if (this._GridInfo.Columns.Contains(grdColumn.Field)) continue;

                    this.C_LBFields.Items.Add(grdColumn.Copy());
                }

                strCategory = strText;

            }
            else if (this.HashCategories.Contains(strCategory))
            {
                GridColumnCollection allFieldColumns = this.HashCategories[strCategory] as GridColumnCollection;

                foreach (GridColumn grdColumn in allFieldColumns)
                {
                    if (this._GridInfo.Columns.Contains(grdColumn.Field)) continue;

                    if (grdColumn.ToString().ToLower().StartsWith(strText.ToLower()))
                    {
                        this.C_LBFields.Items.Add(grdColumn.Copy());
                    }
                }

            }
        }

    
        private void BtnAddCombinedGridColumn_Click(object sender, EventArgs e)
        {
            CalculateColumnInfo calcColumnInfo=new CalculateColumnInfo();

            Editors.CalculateColumnEditorForm calcEditorForm = new Webb.Reports.ExControls.Editors.CalculateColumnEditorForm(calcColumnInfo);

            if (calcEditorForm.ShowDialog() == DialogResult.OK)
            {
                calcColumnInfo = calcEditorForm.Value;
            }

             CombinedGridColumn combinedGridColumn = new CombinedGridColumn();

             combinedGridColumn.CalculatingFormula = calcColumnInfo;

            this.C_LBSelFields.Items.Add(combinedGridColumn);
        }

        private void C_PropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (this.C_LBSelFields.SelectedItems==null || this.C_PropertyGrid.SelectedObjects== null) return;

            if(this.C_LBSelFields.SelectedItems.Count!=1||this.C_PropertyGrid.SelectedObjects.Length!=this.C_LBSelFields.SelectedItems.Count)return;

            object objValue=this.C_PropertyGrid.SelectedObjects[0];

            if(!(objValue is CombinedGridColumn)||!(this.C_LBSelFields.SelectedItem is CombinedGridColumn))return;

            this.C_LBSelFields.Items[this.C_LBSelFields.SelectedIndex] = objValue;
        }

	}
}
