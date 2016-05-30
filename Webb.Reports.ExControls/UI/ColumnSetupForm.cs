//08-18-2008@Scott
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Webb.Reports.ExControls.Data;

namespace Webb.Reports.ExControls.UI
{
    public class ColumnSetupForm : Form
    {
        private ColumnStyleList m_ColStyles = new ColumnStyleList();

		private System.Windows.Forms.Button BtnReset;
	
		private System.Windows.Forms.Button BtnClear;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Panel palContainer;
		private System.Windows.Forms.Panel palSimple;
		private System.Windows.Forms.NumericUpDown numericUpDownColumnWidth;
		private System.Windows.Forms.ComboBox cmbValueAligin;
		private System.Windows.Forms.ComboBox cmbHeaderFormat;
		private System.Windows.Forms.TextBox txtColumnHeader;
		private System.Windows.Forms.Label lblDisplayName;
		private System.Windows.Forms.Label lblColumnHeading;
		private System.Windows.Forms.Label lblColumnName;
		private System.Windows.Forms.Label lblColumnWidth;
		private System.Windows.Forms.Label lblValueFormat;
		private System.Windows.Forms.Label lblvalueAlign;
		private System.Windows.Forms.ComboBox cmbValueFormat;
		private System.Windows.Forms.Label lblFormat;
		private System.Windows.Forms.Button BtnAdvance;
		private System.Windows.Forms.PropertyGrid C_PropertyGrid;

        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblSample;
		private System.Windows.Forms.Panel palSample;
        private Label lblDisplayBackColor;
        private TextBox txtFont;
        private Button BtnFont;
        private Button BtnBackColor;
        private Label lblBackColor;
        private Label lblFont;

		private bool InColSetting=false;		

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

		#region Windows Designer Generate
        private void InitializeComponent()
        {
            this.C_PanelCommands = new System.Windows.Forms.Panel();
            this.BtnClear = new System.Windows.Forms.Button();
            this.BtnReset = new System.Windows.Forms.Button();
            this.C_BtnCancel = new System.Windows.Forms.Button();
            this.C_BtnOK = new System.Windows.Forms.Button();
            this.C_LBColumns = new System.Windows.Forms.ListBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.palContainer = new System.Windows.Forms.Panel();
            this.BtnAdvance = new System.Windows.Forms.Button();
            this.palSimple = new System.Windows.Forms.Panel();
            this.lblDisplayBackColor = new System.Windows.Forms.Label();
            this.txtFont = new System.Windows.Forms.TextBox();
            this.BtnFont = new System.Windows.Forms.Button();
            this.BtnBackColor = new System.Windows.Forms.Button();
            this.lblBackColor = new System.Windows.Forms.Label();
            this.lblFont = new System.Windows.Forms.Label();
            this.palSample = new System.Windows.Forms.Panel();
            this.lblSample = new System.Windows.Forms.Label();
            this.numericUpDownColumnWidth = new System.Windows.Forms.NumericUpDown();
            this.cmbValueAligin = new System.Windows.Forms.ComboBox();
            this.cmbHeaderFormat = new System.Windows.Forms.ComboBox();
            this.txtColumnHeader = new System.Windows.Forms.TextBox();
            this.lblDisplayName = new System.Windows.Forms.Label();
            this.lblColumnHeading = new System.Windows.Forms.Label();
            this.lblColumnName = new System.Windows.Forms.Label();
            this.lblColumnWidth = new System.Windows.Forms.Label();
            this.lblValueFormat = new System.Windows.Forms.Label();
            this.lblvalueAlign = new System.Windows.Forms.Label();
            this.cmbValueFormat = new System.Windows.Forms.ComboBox();
            this.lblFormat = new System.Windows.Forms.Label();
            this.C_PropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.C_PanelCommands.SuspendLayout();
            this.palContainer.SuspendLayout();
            this.palSimple.SuspendLayout();
            this.palSample.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownColumnWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // C_PanelCommands
            // 
            this.C_PanelCommands.Controls.Add(this.BtnClear);
            this.C_PanelCommands.Controls.Add(this.BtnReset);
            this.C_PanelCommands.Controls.Add(this.C_BtnCancel);
            this.C_PanelCommands.Controls.Add(this.C_BtnOK);
            this.C_PanelCommands.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.C_PanelCommands.Location = new System.Drawing.Point(0, 428);
            this.C_PanelCommands.Name = "C_PanelCommands";
            this.C_PanelCommands.Size = new System.Drawing.Size(693, 51);
            this.C_PanelCommands.TabIndex = 3;
            // 
            // BtnClear
            // 
            this.BtnClear.Location = new System.Drawing.Point(269, 13);
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.Size = new System.Drawing.Size(105, 30);
            this.BtnClear.TabIndex = 3;
            this.BtnClear.Text = "Clear Setting";
            this.BtnClear.Visible = false;
            this.BtnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // BtnReset
            // 
            this.BtnReset.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnReset.Location = new System.Drawing.Point(10, 13);
            this.BtnReset.Name = "BtnReset";
            this.BtnReset.Size = new System.Drawing.Size(230, 30);
            this.BtnReset.TabIndex = 2;
            this.BtnReset.Text = "Reset to the default style";
            this.BtnReset.Click += new System.EventHandler(this.BtnReset_Click);
            // 
            // C_BtnCancel
            // 
            this.C_BtnCancel.Location = new System.Drawing.Point(586, 15);
            this.C_BtnCancel.Name = "C_BtnCancel";
            this.C_BtnCancel.Size = new System.Drawing.Size(76, 26);
            this.C_BtnCancel.TabIndex = 1;
            this.C_BtnCancel.Text = "Cancel";
            this.C_BtnCancel.Click += new System.EventHandler(this.C_BtnCancel_Click);
            // 
            // C_BtnOK
            // 
            this.C_BtnOK.Location = new System.Drawing.Point(470, 15);
            this.C_BtnOK.Name = "C_BtnOK";
            this.C_BtnOK.Size = new System.Drawing.Size(87, 26);
            this.C_BtnOK.TabIndex = 0;
            this.C_BtnOK.Text = "OK";
            this.C_BtnOK.Click += new System.EventHandler(this.C_BtnOK_Click);
            // 
            // C_LBColumns
            // 
            this.C_LBColumns.Dock = System.Windows.Forms.DockStyle.Left;
            this.C_LBColumns.ItemHeight = 12;
            this.C_LBColumns.Location = new System.Drawing.Point(0, 0);
            this.C_LBColumns.Name = "C_LBColumns";
            this.C_LBColumns.Size = new System.Drawing.Size(307, 424);
            this.C_LBColumns.Sorted = true;
            this.C_LBColumns.TabIndex = 4;
            this.C_LBColumns.SelectedIndexChanged += new System.EventHandler(this.C_LBColumnStyles_SelectedIndexChanged);
            this.C_LBColumns.SelectedValueChanged += new System.EventHandler(this.C_LBColumns_SelectedValueChanged);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(307, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(10, 428);
            this.splitter1.TabIndex = 5;
            this.splitter1.TabStop = false;
            // 
            // palContainer
            // 
            this.palContainer.Controls.Add(this.BtnAdvance);
            this.palContainer.Controls.Add(this.palSimple);
            this.palContainer.Controls.Add(this.C_PropertyGrid);
            this.palContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.palContainer.Location = new System.Drawing.Point(317, 0);
            this.palContainer.Name = "palContainer";
            this.palContainer.Size = new System.Drawing.Size(376, 428);
            this.palContainer.TabIndex = 7;
            // 
            // BtnAdvance
            // 
            this.BtnAdvance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAdvance.Location = new System.Drawing.Point(8, 0);
            this.BtnAdvance.Name = "BtnAdvance";
            this.BtnAdvance.Size = new System.Drawing.Size(173, 25);
            this.BtnAdvance.TabIndex = 2;
            this.BtnAdvance.Text = "+  Advanced properties";
            this.BtnAdvance.Click += new System.EventHandler(this.BtnAdvance_Click);
            // 
            // palSimple
            // 
            this.palSimple.Controls.Add(this.lblDisplayBackColor);
            this.palSimple.Controls.Add(this.txtFont);
            this.palSimple.Controls.Add(this.BtnFont);
            this.palSimple.Controls.Add(this.BtnBackColor);
            this.palSimple.Controls.Add(this.lblBackColor);
            this.palSimple.Controls.Add(this.lblFont);
            this.palSimple.Controls.Add(this.palSample);
            this.palSimple.Controls.Add(this.numericUpDownColumnWidth);
            this.palSimple.Controls.Add(this.cmbValueAligin);
            this.palSimple.Controls.Add(this.cmbHeaderFormat);
            this.palSimple.Controls.Add(this.txtColumnHeader);
            this.palSimple.Controls.Add(this.lblDisplayName);
            this.palSimple.Controls.Add(this.lblColumnHeading);
            this.palSimple.Controls.Add(this.lblColumnName);
            this.palSimple.Controls.Add(this.lblColumnWidth);
            this.palSimple.Controls.Add(this.lblValueFormat);
            this.palSimple.Controls.Add(this.lblvalueAlign);
            this.palSimple.Controls.Add(this.cmbValueFormat);
            this.palSimple.Controls.Add(this.lblFormat);
            this.palSimple.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.palSimple.Location = new System.Drawing.Point(0, 30);
            this.palSimple.Name = "palSimple";
            this.palSimple.Size = new System.Drawing.Size(376, 398);
            this.palSimple.TabIndex = 1;
            // 
            // lblDisplayBackColor
            // 
            this.lblDisplayBackColor.BackColor = System.Drawing.Color.White;
            this.lblDisplayBackColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDisplayBackColor.Location = new System.Drawing.Point(157, 243);
            this.lblDisplayBackColor.Name = "lblDisplayBackColor";
            this.lblDisplayBackColor.Size = new System.Drawing.Size(159, 23);
            this.lblDisplayBackColor.TabIndex = 72;
            // 
            // txtFont
            // 
            this.txtFont.Enabled = false;
            this.txtFont.Location = new System.Drawing.Point(156, 207);
            this.txtFont.Name = "txtFont";
            this.txtFont.Size = new System.Drawing.Size(162, 21);
            this.txtFont.TabIndex = 71;
            // 
            // BtnFont
            // 
            this.BtnFont.Enabled = false;
            this.BtnFont.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnFont.Location = new System.Drawing.Point(324, 206);
            this.BtnFont.Name = "BtnFont";
            this.BtnFont.Size = new System.Drawing.Size(44, 26);
            this.BtnFont.TabIndex = 69;
            this.BtnFont.Text = "...";
            this.BtnFont.Click += new System.EventHandler(this.BtnFont_Click);
            // 
            // BtnBackColor
            // 
            this.BtnBackColor.Enabled = false;
            this.BtnBackColor.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBackColor.Location = new System.Drawing.Point(324, 243);
            this.BtnBackColor.Name = "BtnBackColor";
            this.BtnBackColor.Size = new System.Drawing.Size(44, 26);
            this.BtnBackColor.TabIndex = 70;
            this.BtnBackColor.Text = "...";
            this.BtnBackColor.Click += new System.EventHandler(this.BtnBackColor_Click);
            // 
            // lblBackColor
            // 
            this.lblBackColor.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBackColor.ForeColor = System.Drawing.Color.Black;
            this.lblBackColor.Location = new System.Drawing.Point(14, 245);
            this.lblBackColor.Name = "lblBackColor";
            this.lblBackColor.Size = new System.Drawing.Size(125, 26);
            this.lblBackColor.TabIndex = 67;
            this.lblBackColor.Text = "Background Color";
            // 
            // lblFont
            // 
            this.lblFont.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFont.ForeColor = System.Drawing.Color.Black;
            this.lblFont.Location = new System.Drawing.Point(14, 209);
            this.lblFont.Name = "lblFont";
            this.lblFont.Size = new System.Drawing.Size(125, 26);
            this.lblFont.TabIndex = 68;
            this.lblFont.Text = "Font && TextColor";
            // 
            // palSample
            // 
            this.palSample.Controls.Add(this.lblSample);
            this.palSample.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.palSample.Location = new System.Drawing.Point(0, 285);
            this.palSample.Name = "palSample";
            this.palSample.Size = new System.Drawing.Size(376, 113);
            this.palSample.TabIndex = 60;
            // 
            // lblSample
            // 
            this.lblSample.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSample.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSample.Location = new System.Drawing.Point(6, 0);
            this.lblSample.Name = "lblSample";
            this.lblSample.Size = new System.Drawing.Size(370, 110);
            this.lblSample.TabIndex = 65;
            this.lblSample.Paint += new System.Windows.Forms.PaintEventHandler(this.lblSample_Paint);
            // 
            // numericUpDownColumnWidth
            // 
            this.numericUpDownColumnWidth.Enabled = false;
            this.numericUpDownColumnWidth.Location = new System.Drawing.Point(153, 75);
            this.numericUpDownColumnWidth.Maximum = new decimal(new int[] {
            8000,
            0,
            0,
            0});
            this.numericUpDownColumnWidth.Name = "numericUpDownColumnWidth";
            this.numericUpDownColumnWidth.Size = new System.Drawing.Size(210, 21);
            this.numericUpDownColumnWidth.TabIndex = 59;
            this.numericUpDownColumnWidth.ValueChanged += new System.EventHandler(this.numericUpDownColumnWidth_ValueChanged);
            // 
            // cmbValueAligin
            // 
            this.cmbValueAligin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbValueAligin.Enabled = false;
            this.cmbValueAligin.Location = new System.Drawing.Point(153, 143);
            this.cmbValueAligin.Name = "cmbValueAligin";
            this.cmbValueAligin.Size = new System.Drawing.Size(210, 20);
            this.cmbValueAligin.TabIndex = 58;
            this.cmbValueAligin.SelectedIndexChanged += new System.EventHandler(this.cmbValueAligin_SelectedIndexChanged);
            // 
            // cmbHeaderFormat
            // 
            this.cmbHeaderFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHeaderFormat.Enabled = false;
            this.cmbHeaderFormat.Location = new System.Drawing.Point(153, 109);
            this.cmbHeaderFormat.Name = "cmbHeaderFormat";
            this.cmbHeaderFormat.Size = new System.Drawing.Size(210, 20);
            this.cmbHeaderFormat.TabIndex = 56;
            this.cmbHeaderFormat.SelectedIndexChanged += new System.EventHandler(this.cmbHeaderFormat_SelectedIndexChanged);
            // 
            // txtColumnHeader
            // 
            this.txtColumnHeader.Enabled = false;
            this.txtColumnHeader.Location = new System.Drawing.Point(153, 40);
            this.txtColumnHeader.Name = "txtColumnHeader";
            this.txtColumnHeader.Size = new System.Drawing.Size(210, 21);
            this.txtColumnHeader.TabIndex = 55;
            this.txtColumnHeader.TextChanged += new System.EventHandler(this.txtColumnHeader_TextChanged);
            // 
            // lblDisplayName
            // 
            this.lblDisplayName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDisplayName.ForeColor = System.Drawing.Color.Black;
            this.lblDisplayName.Location = new System.Drawing.Point(153, 6);
            this.lblDisplayName.Name = "lblDisplayName";
            this.lblDisplayName.Size = new System.Drawing.Size(210, 25);
            this.lblDisplayName.TabIndex = 54;
            // 
            // lblColumnHeading
            // 
            this.lblColumnHeading.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblColumnHeading.ForeColor = System.Drawing.Color.Black;
            this.lblColumnHeading.Location = new System.Drawing.Point(14, 40);
            this.lblColumnHeading.Name = "lblColumnHeading";
            this.lblColumnHeading.Size = new System.Drawing.Size(135, 26);
            this.lblColumnHeading.TabIndex = 53;
            this.lblColumnHeading.Text = "Column Heading";
            // 
            // lblColumnName
            // 
            this.lblColumnName.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblColumnName.ForeColor = System.Drawing.Color.Black;
            this.lblColumnName.Location = new System.Drawing.Point(14, 6);
            this.lblColumnName.Name = "lblColumnName";
            this.lblColumnName.Size = new System.Drawing.Size(125, 17);
            this.lblColumnName.TabIndex = 49;
            this.lblColumnName.Text = "ColumnName";
            // 
            // lblColumnWidth
            // 
            this.lblColumnWidth.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblColumnWidth.ForeColor = System.Drawing.Color.Black;
            this.lblColumnWidth.Location = new System.Drawing.Point(14, 75);
            this.lblColumnWidth.Name = "lblColumnWidth";
            this.lblColumnWidth.Size = new System.Drawing.Size(135, 25);
            this.lblColumnWidth.TabIndex = 48;
            this.lblColumnWidth.Text = "Column Width";
            // 
            // lblValueFormat
            // 
            this.lblValueFormat.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValueFormat.ForeColor = System.Drawing.Color.Black;
            this.lblValueFormat.Location = new System.Drawing.Point(14, 109);
            this.lblValueFormat.Name = "lblValueFormat";
            this.lblValueFormat.Size = new System.Drawing.Size(135, 26);
            this.lblValueFormat.TabIndex = 50;
            this.lblValueFormat.Text = "Heading Format";
            // 
            // lblvalueAlign
            // 
            this.lblvalueAlign.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblvalueAlign.ForeColor = System.Drawing.Color.Black;
            this.lblvalueAlign.Location = new System.Drawing.Point(14, 143);
            this.lblvalueAlign.Name = "lblvalueAlign";
            this.lblvalueAlign.Size = new System.Drawing.Size(135, 26);
            this.lblvalueAlign.TabIndex = 52;
            this.lblvalueAlign.Text = "Value Alignment";
            // 
            // cmbValueFormat
            // 
            this.cmbValueFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbValueFormat.Enabled = false;
            this.cmbValueFormat.Location = new System.Drawing.Point(153, 176);
            this.cmbValueFormat.Name = "cmbValueFormat";
            this.cmbValueFormat.Size = new System.Drawing.Size(210, 20);
            this.cmbValueFormat.TabIndex = 57;
            this.cmbValueFormat.SelectedIndexChanged += new System.EventHandler(this.cmbValueFormat_SelectedIndexChanged);
            // 
            // lblFormat
            // 
            this.lblFormat.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormat.ForeColor = System.Drawing.Color.Black;
            this.lblFormat.Location = new System.Drawing.Point(14, 177);
            this.lblFormat.Name = "lblFormat";
            this.lblFormat.Size = new System.Drawing.Size(135, 26);
            this.lblFormat.TabIndex = 51;
            this.lblFormat.Text = "Value Format";
            // 
            // C_PropertyGrid
            // 
            this.C_PropertyGrid.LineColor = System.Drawing.SystemColors.ScrollBar;
            this.C_PropertyGrid.Location = new System.Drawing.Point(18, 30);
            this.C_PropertyGrid.Name = "C_PropertyGrid";
            this.C_PropertyGrid.Size = new System.Drawing.Size(347, 359);
            this.C_PropertyGrid.TabIndex = 0;
            this.C_PropertyGrid.Visible = false;
            // 
            // ColumnSetupForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(693, 479);
            this.Controls.Add(this.palContainer);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.C_LBColumns);
            this.Controls.Add(this.C_PanelCommands);
            this.Name = "ColumnSetupForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Field Setup";
            this.Load += new System.EventHandler(this.ColumnSetupForm_Load);
            this.C_PanelCommands.ResumeLayout(false);
            this.palContainer.ResumeLayout(false);
            this.palSimple.ResumeLayout(false);
            this.palSimple.PerformLayout();
            this.palSample.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownColumnWidth)).EndInit();
            this.ResumeLayout(false);

		}

        #endregion

        private System.Windows.Forms.Panel C_PanelCommands;
        private System.Windows.Forms.Button C_BtnCancel;
        private System.Windows.Forms.Button C_BtnOK;
        private System.Windows.Forms.ListBox C_LBColumns;

        public ColumnSetupForm()
        {
            InitializeComponent();

			InitList();

        }		

        private void ColumnSetupForm_Load(object sender, EventArgs e)
        {
            this.C_LBColumns.SelectedIndex = -1;

            this.C_LBColumns.Items.Clear();

            this.LoadList();

            this.UpdateList();

            if (Webb.Reports.ReportWizard.WizardInfo.ReportSetting.WizardEnviroment.ProductType != Webb.Reports.DataProvider.WebbDBTypes.WebbAdvantageFootball)
            {
                this.BtnReset.Visible = false;
            }
            else
            {
                this.BtnReset.Visible = true;
            }
        }

        //Load list
        private void LoadList()
        {
            if (!System.IO.File.Exists(ColumnManager.FilePath))
            {
                ColumnManager.PublicColumnStyles.Save(ColumnManager.FilePath);
            }

            ColumnManager.PublicColumnStyles.Load(ColumnManager.FilePath);

            this.m_ColStyles.Apply(ColumnManager.PublicColumnStyles);

            foreach (Webb.Reports.ExControls.Data.ColumnStyle colStyle in this.m_ColStyles.ColumnStyles)
            {
                this.C_LBColumns.Items.Add(colStyle);
            }
        }		

        //Update list by avialable fields
        private void UpdateList()
        {
            foreach(object value in Webb.Data.PublicDBFieldConverter.AvialableFields)
            {
				string strFieldName=value.ToString();

                Webb.Reports.ExControls.Data.ColumnStyle colStyle = new Webb.Reports.ExControls.Data.ColumnStyle(strFieldName);

                if (this.m_ColStyles.ColumnStyles.Add(colStyle) != -1)
                {//if success, add to list
                    this.C_LBColumns.Items.Add(colStyle);
                }
            }
        }

        

        private void C_BtnOK_Click(object sender, EventArgs e)
        {
            ColumnManager.PublicColumnStyles.Apply(this.m_ColStyles);

            ColumnManager.PublicColumnStyles.Save(ColumnManager.FilePath);

            this.DialogResult = DialogResult.OK;

            this.Close();
        }

        private void C_BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;

            this.Close();
        }

        private void C_PropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            
        }

        private void C_LBColumnStyles_SelectedIndexChanged(object sender, EventArgs e)
        {
//            object item = this.C_LBColumns.SelectedItem;
//
//            if (item == null) this.C_PropertyGrid.SelectedObject = item;
//            else
//            {
//                if (item is Webb.Reports.ExControls.Data.ColumnStyle)
//                {
//                    this.C_PropertyGrid.SelectedObject = item;
//                }
//            }
		
			if(this.palSimple.Visible)
			{
				this.lblSample.Invalidate();

				this.C_PropertyGrid.SelectedObject = this.C_LBColumns.SelectedItem;

				if(this.C_LBColumns.SelectedIndex<0)
				{
					this.BtnFont.Enabled=false;

					this.BtnBackColor.Enabled=false;

					this.txtColumnHeader.Enabled=false;

					this.cmbHeaderFormat.Enabled=false;

					this.numericUpDownColumnWidth.Enabled=false;

					this.cmbValueAligin.Enabled=false;

					this.cmbValueFormat.Enabled=false;

					return;
				}

				this.BtnFont.Enabled=true;
				this.BtnBackColor.Enabled=true;
				this.txtColumnHeader.Enabled=true;
				this.cmbHeaderFormat.Enabled=true;
				this.numericUpDownColumnWidth.Enabled=true;
				this.cmbValueAligin.Enabled=true;
				this.cmbValueFormat.Enabled=true;

                this.SelectedGridColumn(this.C_LBColumns.SelectedItem as Webb.Reports.ExControls.Data.ColumnStyle);              
				
			}
        }

		private void C_LBColumns_SelectedValueChanged(object sender, System.EventArgs e)
		{
			if(this.palSimple.Visible)return;

			if(this.C_LBColumns.SelectedItems.Count <= 0)
			{
				this.C_LBColumns.SelectedItem=null;

				return;
			}

			object[] items = new object[this.C_LBColumns.SelectedItems.Count];

			int index = 0;

			foreach(object item in this.C_LBColumns.SelectedItems)
			{
                if (item is Webb.Reports.ExControls.Data.ColumnStyle && index < this.C_LBColumns.SelectedItems.Count)
				{
                    items.SetValue(item as Webb.Reports.ExControls.Data.ColumnStyle, index++);
				}
				else
				{
					items.SetValue(item, index++);
				}
			}

			this.C_PropertyGrid.SelectedObjects = items;
			
		}

	
		private Color GetDefaultColor(string strField)
		{
			switch(strField)
			{
				case "Run/Pass":

					  return Color.Gold;

				case "Hash":
					 
					  return Color.Lavender;					

				case "Front":

					return Color.Thistle;
					
				case "Coverages":

					return Color.FromArgb(238,238,224);

					
				case "Stunts":

					return Color.PeachPuff;
					
				case "Dog":

					return Color.SeaShell;
				
				case "Blitz":
					return Color.AliceBlue;
					
				case "Motion":

					return Color.Honeydew;
					
				case "Formation":
					return Color.DarkSeaGreen;
					
				case "Play Name":
					return Color.LightSteelBlue;
					
				case "Personnel":

					return Color.LightYellow;
					
				case "Back Formation":

					return Color.FromArgb(142, 229, 238);
					
				case "Strength":

					return Color.LightSalmon;
					
				}
			return Color.Empty;
			
		}

        private ColumnStyleList GetEditColumnStyles(ColumnStyleList styleList)
        {
            ColumnStyleList editStyleList = new ColumnStyleList();

            editStyleList.FileName = styleList.FileName;

            foreach (Webb.Reports.ExControls.Data.ColumnStyle colStyle in styleList.ColumnStyles)
            {
                if (colStyle.IsEdit())
                {
                    editStyleList.ColumnStyles.Add(colStyle.Copy());
                }
            }
            return editStyleList;
        }

		private void ResetColumnFromTerminomoly()
		{
				Webb.Reports.DataProvider.DBSourceConfig _DBSourceConfig=Webb.Reports.DataProvider.VideoPlayBackManager.PublicDBProvider.DBSourceConfig;

				if(_DBSourceConfig==null)return;

				string userFolder=_DBSourceConfig.UserFolder;

				if(userFolder==null)userFolder=string.Empty;

				int index=userFolder.IndexOf(";");
	                
				if(index>=0)
				{
					userFolder=userFolder.Substring(0,index);
				}
				if(userFolder==string.Empty)return;

				string filename=userFolder.Trim(@"\".ToCharArray())+@"\scouting.tmn";

				if(!System.IO.File.Exists(filename))
				{
					string strMessage=string.Format("Failed to find the file {0}!\nplease reload the games again in the correct folder",filename);
					
					Webb.Utilities.MessageBoxEx.ShowError(strMessage);

					return;
				}

				DialogResult dr=Webb.Utilities.MessageBoxEx.ShowQuestion_YesNo("Waring: this action would clear all your setting and reset it to default,would you like to continiue with it??");
	  
				if(dr!=DialogResult.Yes)return;
			
				this.m_ColStyles.ColumnStyles.Clear();

				this.C_LBColumns.Items.Clear();

				Font font= new Font(DevExpress.Utils.AppearanceObject.DefaultFont.FontFamily, 10f);

				Graphics g=this.CreateGraphics();			

				SizeF szfText=g.MeasureString("a",font);

				g.Dispose();

				Hashtable hashTable=Webb.Reports.AdvGameData.GetFieldMaxLength(userFolder,
										Webb.Data.PublicDBFieldConverter.AvialableFields);

				foreach(object value in Webb.Data.PublicDBFieldConverter.AvialableFields)
				{
					string strFieldName=value.ToString().Trim();

                    Webb.Reports.ExControls.Data.ColumnStyle colStyle = new Webb.Reports.ExControls.Data.ColumnStyle(strFieldName);
				
					int defaultCharWidth=(int)hashTable[strFieldName];

					if(defaultCharWidth>0)
					{
					defaultCharWidth=(int)((szfText.Width+1)*defaultCharWidth);

						colStyle.ColumnWidth=defaultCharWidth;
					}

					Color backColor=this.GetDefaultColor(strFieldName);

					if(backColor!=Color.Empty)
					{
					colStyle.Style.BackgroundColor=backColor;

						colStyle.ColorNeedChange=true;
					}

					this.m_ColStyles.ColumnStyles.Add(colStyle) ;
					
					this.C_LBColumns.Items.Add(colStyle);				

				 
				}
			}

        private void LoadDefaultStyle()
        {
            DialogResult dr = Webb.Utilities.MessageBoxEx.ShowQuestion_YesNo("Waring: this action would clear all your current setting and reset it to default,would you like to continiue with it??");

            if (dr != DialogResult.Yes) return;

            if (!System.IO.File.Exists(ColumnManager.DefaultStylePath))
            {
                Webb.Utilities.MessageBoxEx.ShowError("default style was lost!");
              
                return;
            }

            this.C_LBColumns.Items.Clear();

            ColumnManager.PublicColumnStyles.Load(ColumnManager.DefaultStylePath);

            this.m_ColStyles.Apply(ColumnManager.PublicColumnStyles);

            foreach (Webb.Reports.ExControls.Data.ColumnStyle colStyle in this.m_ColStyles.ColumnStyles)
            {
                this.C_LBColumns.Items.Add(colStyle);
            }
            this.C_LBColumns.SelectedIndex = 0;
            this.C_LBColumns.SelectedIndex = -1;
        }
		private void ResetWidthFromDataSource()
		{		
				DataSet ds=Webb.Reports.DataProvider.VideoPlayBackManager.DataSource;			

				if(ds==null||ds.Tables.Count==0)
				{
					string strMessage=string.Format("No games or edls are avaliable in current.please reload the games again");
					
					Webb.Utilities.MessageBoxEx.ShowError(strMessage);

					return;
				}

				DialogResult dr=Webb.Utilities.MessageBoxEx.ShowQuestion_YesNo("Waring: this action would clear your current setting and reset it to default.\nwould you like to continue with it??");
	  
				if(dr!=DialogResult.Yes)return;

				this.m_ColStyles.ColumnStyles.Clear();

				this.C_LBColumns.Items.Clear();				

				Graphics g=this.CreateGraphics();

				DataTable dt=ds.Tables[0];
			
				foreach(object value in Webb.Data.PublicDBFieldConverter.AvialableFields)
				{
					string strFieldName=value.ToString().Trim();

                    Webb.Reports.ExControls.Data.ColumnStyle colStyle = new Webb.Reports.ExControls.Data.ColumnStyle(strFieldName);

					SizeF textSize=g.MeasureString(strFieldName,colStyle.Style.Font);
				
					int defaultCharWidth=(int)(textSize.Width*Webb.Utility.ConvertCoordinate)+3;

					if(dt.Columns.Contains(strFieldName))
					{
						for(int i=0;i<dt.Rows.Count;i++)
						{
							if(!(dt.Rows[i][strFieldName] is System.DBNull))
							{
								string fieldValue=dt.Rows[i][strFieldName].ToString();

								textSize=g.MeasureString(fieldValue,colStyle.Style.Font);

								defaultCharWidth=Math.Max(defaultCharWidth,(int)(textSize.Width*Webb.Utility.ConvertCoordinate)+3);
							}
						}
					}

					colStyle.ColumnWidth=defaultCharWidth;

					Color backColor=this.GetDefaultColor(strFieldName);

					if(backColor!=Color.Empty)
					{
						colStyle.Style.BackgroundColor=backColor;

						colStyle.ColorNeedChange=true;
					}

					this.m_ColStyles.ColumnStyles.Add(colStyle) ;
					
					this.C_LBColumns.Items.Add(colStyle);				

				 
				}
                this.lblSample.Invalidate();
			
		}
		private void BtnReset_Click(object sender, System.EventArgs e)
		{
            LoadDefaultStyle();
           
            //ResetWidthFromDataSource();
		}


		private void BtnClear_Click(object sender, System.EventArgs e)
		{
			this.m_ColStyles.ColumnStyles.Clear();

			this.C_LBColumns.Items.Clear();			

			foreach(object value in Webb.Data.PublicDBFieldConverter.AvialableFields)
			{
				string strFieldName=value.ToString().Trim();

                Webb.Reports.ExControls.Data.ColumnStyle colStyle = new Webb.Reports.ExControls.Data.ColumnStyle(strFieldName);
		
				colStyle.ColumnWidth=55;

				Color backColor=this.GetDefaultColor(strFieldName);

				if(backColor!=Color.Empty)
				{
					colStyle.Style.BackgroundColor=backColor;

					colStyle.ColorNeedChange=true;
				}

				this.m_ColStyles.ColumnStyles.Add(colStyle) ;
				
				this.C_LBColumns.Items.Add(colStyle);				 
			}
		}

		private void BtnAdvance_Click(object sender, System.EventArgs e)
		{
			if(this.BtnAdvance.Text=="+  Advanced properties")
			{	
				this.C_PropertyGrid.Visible=true;

				C_PropertyGrid.Dock=DockStyle.Bottom;
					
				this.BtnAdvance.Text="--  Advanced properties";			
				
                this.C_LBColumns.SelectionMode=SelectionMode.MultiExtended;

				this.palSimple.Visible=false;

			}
			else
			{
				this.C_PropertyGrid.Visible=false;
					
				this.BtnAdvance.Text="+  Advanced properties";

				this.C_LBColumns.SelectionMode=SelectionMode.One;

				this.palSimple.Visible=true;

			}
		}  

        
    	#region Set Simple
        private void SelectedGridColumn(Webb.Reports.ExControls.Data.ColumnStyle columnStyle)
			{
				this.palSimple.Enabled=true;

				InColSetting=true;

				this.lblDisplayName.Text=columnStyle.FieldName;
				this.txtColumnHeader.Text=columnStyle.ColumnHeading;

				this.numericUpDownColumnWidth.Value=columnStyle.ColumnWidth;

				if(columnStyle.TitleFormat==0)
				{
					this.cmbHeaderFormat.SelectedIndex=0;
				}
				else
				{
					switch(columnStyle.TitleFormat)
					{
						case StringFormatFlags.DirectionRightToLeft:
							this.cmbHeaderFormat.SelectedIndex=1;
							break;				
						case StringFormatFlags.DirectionVertical:
							this.cmbHeaderFormat.SelectedIndex=2;
							break;
						case StringFormatFlags.NoWrap:
							this.cmbHeaderFormat.SelectedIndex=3;
							break;
						default:
							this.cmbHeaderFormat.SelectedIndex=0;
							break;
					}
				}
				if(columnStyle.Style.StringFormat==0)
				{
					this.cmbValueFormat.SelectedIndex=0;
				}
				else
				{
					switch(columnStyle.Style.StringFormat)
					{
						case StringFormatFlags.DirectionRightToLeft:
							this.cmbValueFormat.SelectedIndex=1;
							break;				
						case StringFormatFlags.DirectionVertical:
							this.cmbValueFormat.SelectedIndex=2;
							break;
						case StringFormatFlags.NoWrap:
							this.cmbValueFormat.SelectedIndex=3;
							break;
						default:
							this.cmbValueFormat.SelectedIndex=0;
							break;
					}
				}			
			
				this.cmbValueAligin.SelectedIndex=cmbValueAligin.FindString(columnStyle.Style.HorzAlignment.ToString());

                Color color = columnStyle.Style.BackgroundColor;

                if (color.IsKnownColor)
                {
                    this.lblDisplayBackColor.Text = color.ToKnownColor().ToString();
                }
                else
                {
                    this.lblDisplayBackColor.Text = string.Format("{0},{1},{2}", color.R, color.G, color.B);
                }

                if (color != Color.Transparent)
                {
                    this.lblDisplayBackColor.BackColor = color;
                }
                else
                {
                    this.lblDisplayBackColor.BackColor = Color.White;
                }

                this.txtFont.Text = Webb.Utility.GetFontDisplayText(columnStyle.Style.Font);

				this.lblSample.Font=columnStyle.Style.Font;
				this.lblSample.BackColor=columnStyle.Style.BackgroundColor;

				InColSetting=false;
			}
			private void InitList()
			{
				this.cmbValueAligin.Items.Clear();
				cmbHeaderFormat.Items.Clear();

				this.cmbValueFormat.Items.Clear();
				
				string[] Valueformats=new string[]{"Default","Right to Left","Vertical","NoWrap"};

				foreach(string strFormat in Valueformats)
				{
					this.cmbValueFormat.Items.Add(strFormat);
					this.cmbHeaderFormat.Items.Add(strFormat);
				}
		        
				string[] ValueAligns=Enum.GetNames(typeof(DevExpress.Utils.HorzAlignment));

				foreach(string valuealigin in ValueAligns)
				{
					this.cmbValueAligin.Items.Add(valuealigin);
				}


			}

		#endregion

		#region Update Simple		
		private void txtColumnHeader_TextChanged(object sender, System.EventArgs e)
		{
			if(this.C_LBColumns.SelectedItem==null||InColSetting)return;

            Webb.Reports.ExControls.Data.ColumnStyle column = this.C_LBColumns.SelectedItem as Webb.Reports.ExControls.Data.ColumnStyle;

			if(column==null)return;

			if(this.txtColumnHeader.Text!=column.ColumnHeading)
			{
				column.ColumnHeading=this.txtColumnHeader.Text;	
			
				this.lblSample.Invalidate();
			}
		}

		private void ColumnWidthChange()
		{
			if(this.C_LBColumns.SelectedItem==null||InColSetting)return;

            Webb.Reports.ExControls.Data.ColumnStyle column = this.C_LBColumns.SelectedItem as Webb.Reports.ExControls.Data.ColumnStyle;

			int columnWidth=(int)this.numericUpDownColumnWidth.Value;

			if(column.ColumnWidth!=columnWidth)
			{ 	           
				column.ColumnWidth=columnWidth;
				this.lblSample.Invalidate();
				
			}
		}
	

		

		private void cmbHeaderFormat_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.C_LBColumns.SelectedItem==null||InColSetting)return;

            Webb.Reports.ExControls.Data.ColumnStyle column = this.C_LBColumns.SelectedItem as Webb.Reports.ExControls.Data.ColumnStyle;

			if(column==null||this.cmbHeaderFormat.SelectedIndex<0)return;

			switch(this.cmbHeaderFormat.SelectedIndex)
			{
				case 1:
					column.TitleFormat=StringFormatFlags.DirectionRightToLeft;
					break;				
				case 2:
					column.TitleFormat=StringFormatFlags.DirectionVertical;
					break;
				case 3:
					column.TitleFormat=StringFormatFlags.NoWrap;
					break;
				default:
					column.TitleFormat=0;
					break;
			}	
			this.lblSample.Invalidate();
			
		}

		private void cmbValueAligin_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.C_LBColumns.SelectedItem==null||InColSetting)return;

            Webb.Reports.ExControls.Data.ColumnStyle column = this.C_LBColumns.SelectedItem as Webb.Reports.ExControls.Data.ColumnStyle;

			if(column==null||this.cmbValueAligin.SelectedIndex<0)return;

			column.Style.HorzAlignment=(DevExpress.Utils.HorzAlignment)Enum.Parse(typeof(DevExpress.Utils.HorzAlignment),this.cmbValueAligin.Text);
		
			this.lblSample.Invalidate();
			
		}

		private void cmbValueFormat_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.C_LBColumns.SelectedItem==null||InColSetting)return;

            Webb.Reports.ExControls.Data.ColumnStyle column = this.C_LBColumns.SelectedItem as Webb.Reports.ExControls.Data.ColumnStyle;

			if(column==null||this.cmbValueFormat.SelectedIndex<0)return;

			switch(this.cmbValueFormat.SelectedIndex)
			{
				case 1:
					column.Style.StringFormat=StringFormatFlags.DirectionRightToLeft;
					break;				
				case 2:
					column.Style.StringFormat=StringFormatFlags.DirectionVertical;
					break;
				case 3:
					column.Style.StringFormat=StringFormatFlags.NoWrap;
					break;
				default:
					column.Style.StringFormat=0;
					break;
			}	

			this.lblSample.Invalidate();
		}

		

		
		private void numericUpDownColumnWidth_ValueChanged(object sender, System.EventArgs e)
		{
			this.ColumnWidthChange();
		}

		
		#endregion

        #region Font &BackColor

        private void BtnFont_Click(object sender, System.EventArgs e)
        {
            Webb.Reports.ExControls.Data.ColumnStyle column = this.C_LBColumns.SelectedItem as Webb.Reports.ExControls.Data.ColumnStyle;

            FontDialog dialog = new FontDialog();

            dialog.Font = column.Style.Font;

            dialog.Color = column.Style.ForeColor;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                column.Style.Font = dialog.Font;

                column.Style.ForeColor = dialog.Color;

                this.txtFont.Text = Webb.Utility.GetFontDisplayText(dialog.Font);

                this.lblSample.Invalidate();
            }


        }
		private int ParseRGB(Color color)
		{
			return (int)(((uint)color.B << 16) | (ushort)(((ushort)color.G << 8) | color.R));
		}

        private void BtnBackColor_Click(object sender, System.EventArgs e)
        {
            Webb.Reports.ExControls.Data.ColumnStyle column = this.C_LBColumns.SelectedItem as Webb.Reports.ExControls.Data.ColumnStyle;

            ColorDialog dialog = new ColorDialog();

            int customColor = ParseRGB(column.Style.BackgroundColor);

            dialog.CustomColors = new int[] { customColor };

            dialog.Color = column.Style.BackgroundColor;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Color color = dialog.Color;

                column.Style.BackgroundColor = color;

                if (column.Style.BackgroundColor != Color.Transparent && column.Style.BackgroundColor != Color.Empty)
                {
                    column.ColorNeedChange = true;
                }

                if (color.IsKnownColor)
                {
                    this.lblDisplayBackColor.Text = color.ToKnownColor().ToString();
                }
                else
                {
                    this.lblDisplayBackColor.Text = string.Format("{0},{1},{2}", color.R, color.G, color.B);
                }

                if (color != Color.Transparent)
                {
                    this.lblDisplayBackColor.BackColor = color;
                }
                else
                {
                    this.lblDisplayBackColor.BackColor = Color.White;
                }
                this.lblSample.Invalidate();
            }
        }
	      #endregion

		private void lblSample_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			e.Graphics.Clear(Control.DefaultBackColor);

			if(this.C_LBColumns.SelectedItem==null||InColSetting)
			{
				e.Graphics.DrawString("This is a sample.",Webb.Utility.GlobalFont,new SolidBrush(Color.Black),0f,0f);

				return;
			}

            Webb.Reports.ExControls.Data.ColumnStyle column = this.C_LBColumns.SelectedItem as Webb.Reports.ExControls.Data.ColumnStyle;
		
			SizeF szfText=e.Graphics.MeasureString("Sample:",Webb.Utility.GlobalFont);

		    e.Graphics.DrawString("Sample:",Webb.Utility.GlobalFont,new SolidBrush(Color.Black),0f,0f);

			int begintWidth=(int)szfText.Width+10;

            int headerHeight=20;

			Rectangle rect=new Rectangle(begintWidth, 0, column.ColumnWidth,headerHeight);

			StringFormat styleFormat=new StringFormat();

			StringAlignment saLigin;

			switch (column.Style.HorzAlignment)
			{
				case DevExpress.Utils.HorzAlignment.Center:
					saLigin = StringAlignment.Center;
					break;
				case DevExpress.Utils.HorzAlignment.Near:
				case DevExpress.Utils.HorzAlignment.Default:
				default:
					saLigin= StringAlignment.Near;
					break;
				case DevExpress.Utils.HorzAlignment.Far:
					saLigin= StringAlignment.Far;
					break;
			}
			
			
			if((column.TitleFormat & StringFormatFlags.DirectionVertical)>0)				
			{	
				headerHeight+=10;

				rect.Height+=10;

				if(column.ColorNeedChange)
				{
					e.Graphics.FillRectangle(new SolidBrush(column.Style.BackgroundColor),rect);
				}

				styleFormat.Alignment=StringAlignment.Near;
				styleFormat.LineAlignment=StringAlignment.Center;				
				e.Graphics.TranslateTransform(rect.X,rect.Bottom);
				e.Graphics.RotateTransform(-90f);
				RectangleF newRect=new RectangleF(0,0,rect.Height,rect.Width);
				e.Graphics.DrawString(column.ColumnHeading,column.Style.Font, new SolidBrush(column.Style.ForeColor), newRect, styleFormat);
				e.Graphics.ResetTransform();
			}
			else
			{
				if(column.ColorNeedChange)
				{
					e.Graphics.FillRectangle(new SolidBrush(column.Style.BackgroundColor),rect);
				}
				styleFormat.Alignment=saLigin;

                styleFormat.FormatFlags=column.TitleFormat;

				e.Graphics.DrawString(column.ColumnHeading,column.Style.Font, new SolidBrush(column.Style.ForeColor), rect, styleFormat);
			}
			e.Graphics.DrawLine(Pens.Black,begintWidth,headerHeight+1,column.ColumnWidth+begintWidth,headerHeight+1);

			rect=new Rectangle(begintWidth, headerHeight+2, column.ColumnWidth,50);

			styleFormat=new StringFormat();

			if(column.ColorNeedChange)
			{
				e.Graphics.FillRectangle(new SolidBrush(column.Style.BackgroundColor),rect);
			}

            
			string strValue=string.Empty;

			DataSet ds=Webb.Reports.DataProvider.VideoPlayBackManager.DataSource;			

			if(ds!=null&&ds.Tables.Count>0&&ds.Tables[0].Columns.Contains(column.ColumnHeading))		
			{
				string rowFilter=string.Format(@"[{0}] is not null and TRIM([{0}])<>''",column.ColumnHeading);

				if(ds.Tables[0].Columns[column.ColumnHeading].DataType!=typeof(string))
				{
					rowFilter=string.Format(@"[{0}] is not null",column.ColumnHeading);
				}

				DataView dv=new DataView(ds.Tables[0], rowFilter ,null, DataViewRowState.CurrentRows);				

				if(dv.Count>0)
				{
                    strValue=dv[0][column.ColumnHeading].ToString();
				}				
			}
			if((column.Style.StringFormat & StringFormatFlags.DirectionVertical)>0)				
			{					
				styleFormat.Alignment=StringAlignment.Near;
				styleFormat.LineAlignment=StringAlignment.Center;				
				e.Graphics.TranslateTransform(rect.X,rect.Bottom);
				e.Graphics.RotateTransform(-90f);
				RectangleF newRect=new RectangleF(0,0,rect.Height,rect.Width);
				e.Graphics.DrawString(strValue,column.Style.Font, new SolidBrush(column.Style.ForeColor), newRect, styleFormat);
				e.Graphics.ResetTransform();
			}
			else
			{
				styleFormat.Alignment=saLigin;

				styleFormat.FormatFlags=column.Style.StringFormat;

				e.Graphics.DrawString(strValue,column.Style.Font, new SolidBrush(column.Style.ForeColor), rect, styleFormat);
			}
        }
       




    }
}