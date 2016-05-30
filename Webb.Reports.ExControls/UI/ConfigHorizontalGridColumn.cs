using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Webb.Reports.ExControls;
using Webb.Reports.ExControls.Data;
using Webb.Reports.ExControls.Views;

using DevExpress.XtraEditors.Controls;

namespace Webb.Reports.ExControls.UI
{
	public class ConfighorizontalGridColumn : Webb.Reports.ExControls.UI.ExControlDesignerControlBase
	{
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.PropertyGrid C_PropertyGrid;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Button C_BtnAdd;
		private System.Windows.Forms.Button C_BtnRemove;
		private System.Windows.Forms.Button C_BtnMoveUp;
		private System.Windows.Forms.Button C_BtnMoveDown;
		private System.Windows.Forms.ImageList C_ImageList;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel3;
		private DevExpress.XtraEditors.ImageListBoxControl C_ListHorizontalColumns;
		private System.ComponentModel.IContainer components = null;
        private Button btnProperties;

        private HorizontalGridColumnCollection hGridColumns = new HorizontalGridColumnCollection();

		public ConfighorizontalGridColumn():base()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

            this.Load += new EventHandler(ConfigHorizontalGridColumn_Load);
			// TODO: Add any initialization after the InitializeComponent call
		}

        public ConfighorizontalGridColumn(ExControlDesignerFormBase i_DesignerForm)
            : base(i_DesignerForm)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

            this.Load += new EventHandler(ConfigHorizontalGridColumn_Load);
			// TODO: Add any initialization after the InitializeComponent call
			this._DesignerForm = i_DesignerForm;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfighorizontalGridColumn));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.C_ListHorizontalColumns = new DevExpress.XtraEditors.ImageListBoxControl();
            this.C_ImageList = new System.Windows.Forms.ImageList(this.components);
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnProperties = new System.Windows.Forms.Button();
            this.C_BtnRemove = new System.Windows.Forms.Button();
            this.C_BtnAdd = new System.Windows.Forms.Button();
            this.C_BtnMoveDown = new System.Windows.Forms.Button();
            this.C_BtnMoveUp = new System.Windows.Forms.Button();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.C_PropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.C_ListHorizontalColumns)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.splitter1);
            this.panel1.Controls.Add(this.C_PropertyGrid);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(680, 364);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.C_ListHorizontalColumns);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(477, 364);
            this.panel2.TabIndex = 7;
            // 
            // C_ListHorizontalColumns
            // 
            this.C_ListHorizontalColumns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.C_ListHorizontalColumns.ImageList = this.C_ImageList;
            this.C_ListHorizontalColumns.Location = new System.Drawing.Point(0, 0);
            this.C_ListHorizontalColumns.Name = "C_ListSeries";
            this.C_ListHorizontalColumns.Size = new System.Drawing.Size(477, 308);
            this.C_ListHorizontalColumns.TabIndex = 9;
            this.C_ListHorizontalColumns.SelectedIndexChanged += new System.EventHandler(this.C_ListSeries_SelectedIndexChanged);
            // 
            // C_ImageList
            // 
            this.C_ImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("C_ImageList.ImageStream")));
            this.C_ImageList.TransparentColor = System.Drawing.Color.Magenta;
            this.C_ImageList.Images.SetKeyName(0, "XRTable.bmp");
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnProperties);
            this.panel3.Controls.Add(this.C_BtnRemove);
            this.panel3.Controls.Add(this.C_BtnAdd);
            this.panel3.Controls.Add(this.C_BtnMoveDown);
            this.panel3.Controls.Add(this.C_BtnMoveUp);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 308);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(477, 56);
            this.panel3.TabIndex = 8;
            // 
            // btnProperties
            // 
            this.btnProperties.Location = new System.Drawing.Point(354, 16);
            this.btnProperties.Name = "btnProperties";
            this.btnProperties.Size = new System.Drawing.Size(96, 23);
            this.btnProperties.TabIndex = 7;
            this.btnProperties.Text = "Properties";
            this.btnProperties.UseVisualStyleBackColor = true;
            this.btnProperties.Click += new System.EventHandler(this.btnProperties_Click);
            // 
            // C_BtnRemove
            // 
            this.C_BtnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.C_BtnRemove.Location = new System.Drawing.Point(88, 16);
            this.C_BtnRemove.Name = "C_BtnRemove";
            this.C_BtnRemove.Size = new System.Drawing.Size(75, 23);
            this.C_BtnRemove.TabIndex = 4;
            this.C_BtnRemove.Text = "Remove";
            this.C_BtnRemove.Click += new System.EventHandler(this.C_BtnRemove_Click);
            // 
            // C_BtnAdd
            // 
            this.C_BtnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.C_BtnAdd.Location = new System.Drawing.Point(8, 16);
            this.C_BtnAdd.Name = "C_BtnAdd";
            this.C_BtnAdd.Size = new System.Drawing.Size(75, 23);
            this.C_BtnAdd.TabIndex = 3;
            this.C_BtnAdd.Text = "Add";
            this.C_BtnAdd.Click += new System.EventHandler(this.C_BtnAdd_Click);
            // 
            // C_BtnMoveDown
            // 
            this.C_BtnMoveDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.C_BtnMoveDown.Location = new System.Drawing.Point(252, 16);
            this.C_BtnMoveDown.Name = "C_BtnMoveDown";
            this.C_BtnMoveDown.Size = new System.Drawing.Size(96, 23);
            this.C_BtnMoveDown.TabIndex = 6;
            this.C_BtnMoveDown.Text = "Move Down";
            this.C_BtnMoveDown.Click += new System.EventHandler(this.C_BtnMoveDown_Click);
            // 
            // C_BtnMoveUp
            // 
            this.C_BtnMoveUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.C_BtnMoveUp.Location = new System.Drawing.Point(168, 16);
            this.C_BtnMoveUp.Name = "C_BtnMoveUp";
            this.C_BtnMoveUp.Size = new System.Drawing.Size(80, 23);
            this.C_BtnMoveUp.TabIndex = 5;
            this.C_BtnMoveUp.Text = "Move Up";
            this.C_BtnMoveUp.Click += new System.EventHandler(this.C_BtnMoveUp_Click);
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(477, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 364);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // C_PropertyGrid
            // 
            this.C_PropertyGrid.Dock = System.Windows.Forms.DockStyle.Right;
            this.C_PropertyGrid.LineColor = System.Drawing.SystemColors.ScrollBar;
            this.C_PropertyGrid.Location = new System.Drawing.Point(480, 0);
            this.C_PropertyGrid.Name = "C_PropertyGrid";
            this.C_PropertyGrid.Size = new System.Drawing.Size(200, 364);
            this.C_PropertyGrid.TabIndex = 1;
            this.C_PropertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.C_PropertyGrid_PropertyValueChanged);
            // 
            // ConfighorizontalGridColumn
            // 
            this.Controls.Add(this.panel1);
            this.Name = "ConfighorizontalGridColumn";
            this.Size = new System.Drawing.Size(680, 364);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.C_ListHorizontalColumns)).EndInit();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void ConfigHorizontalGridColumn_Load(object sender, EventArgs e)
		{
			
		}

		public override void SetView(Webb.Reports.ExControls.Views.ExControlView i_View)
		{
            horizontalGridView = i_View as HorizontalGridView;
            HorizontalGridView hGridView = i_View as HorizontalGridView;

            if (hGridView != null)
            {
                hGridColumns = hGridView.GridColumns.Copy();

                // Update UI
                this.C_ListHorizontalColumns.Items.Clear();

                foreach (HorizontalGridColumn column in hGridColumns)
                {
                    ImageListBoxItem item = new ImageListBoxItem(column, 0);

                    this.C_ListHorizontalColumns.Items.Add(item);
                }

                this.C_ListHorizontalColumns.SelectedIndex = 0;
            }

            this.C_PropertyGrid.SelectedObject = horizontalGridView.HorizontalGridData;
		}

		public override void UpdateView(Webb.Reports.ExControls.Views.ExControlView i_View)
		{
			HorizontalGridView hGridView = i_View as HorizontalGridView;

            if (hGridView != null)
            {
                hGridView.GridColumns = hGridColumns.Copy();
            }
		}

		private void C_BtnAdd_Click(object sender, System.EventArgs e)
		{
			int index = this.C_ListHorizontalColumns.SelectedIndex;

            HorizontalGridColumn hGridColumn = new HorizontalGridColumn();

            hGridColumns.Add(hGridColumn);

            ImageListBoxItem item = new ImageListBoxItem(hGridColumn, 0);

			this.C_ListHorizontalColumns.Items.Insert(index + 1,item);

			this.C_ListHorizontalColumns.SelectedIndex = index + 1;
		}

		private void C_BtnRemove_Click(object sender, System.EventArgs e)
		{
			ImageListBoxItem selectedItem = this.C_ListHorizontalColumns.SelectedItem as ImageListBoxItem;

            if (selectedItem != null && selectedItem.Value is HorizontalGridColumn)
			{
                hGridColumns.Remove(selectedItem.Value as HorizontalGridColumn);

				this.C_ListHorizontalColumns.Items.Remove(selectedItem);
			}
		}

		private void C_BtnMoveUp_Click(object sender, System.EventArgs e)
		{
			int index = this.C_ListHorizontalColumns.SelectedIndex;
			
			if(index > 0)
			{
				object item = this.C_ListHorizontalColumns.SelectedItem;

				this.C_ListHorizontalColumns.Items.RemoveAt(index);

				this.C_ListHorizontalColumns.Items.Insert(index - 1,item);

				this.C_ListHorizontalColumns.SelectedIndex = index - 1;

                hGridColumns.Swap(index - 1, index);
			}
		}

		private void C_BtnMoveDown_Click(object sender, System.EventArgs e)
		{
			int index = this.C_ListHorizontalColumns.SelectedIndex;
			
			if(index < this.C_ListHorizontalColumns.Items.Count - 1)
			{
				object item = this.C_ListHorizontalColumns.SelectedItem;

				this.C_ListHorizontalColumns.Items.RemoveAt(index);

				this.C_ListHorizontalColumns.Items.Insert(index + 1,item);

				this.C_ListHorizontalColumns.SelectedIndex = index + 1;

                hGridColumns.Swap(index, index + 1);
			}
		}

		private void C_ListSeries_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.C_ListHorizontalColumns.SelectedItem != null)
			{
				this.C_PropertyGrid.SelectedObject = (this.C_ListHorizontalColumns.SelectedItem as ImageListBoxItem).Value;
			}
		}

		private void C_PropertyGrid_PropertyValueChanged(object s, System.Windows.Forms.PropertyValueChangedEventArgs e)
		{
			this.C_ListHorizontalColumns.Refresh();
		}

        HorizontalGridView horizontalGridView;
        private void btnProperties_Click(object sender, EventArgs e)
        {
            this.C_PropertyGrid.SelectedObject = horizontalGridView.HorizontalGridData;
        }
	}
}