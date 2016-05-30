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
    public class ConfigGradingSection : Webb.Reports.ExControls.UI.ExControlDesignerControlBase
    {
        private System.Windows.Forms.PropertyGrid C_PropertyGrid;
        private System.Windows.Forms.Splitter C_SplitterLR;
        private System.Windows.Forms.Splitter C_SplitterTB;
        private System.Windows.Forms.Panel C_PanelMain;
        private IContainer components;

        private Splitter splitter1;
        private ListBox lstPostions;
        private Splitter splitter2;
        private Panel palSections;
        private ListBox lstGradingSections;
        private Button btnRemoveSection;
        private Button BtnAddSections;

        private GradingSectionCollection _GradingSectionCollection = new GradingSectionCollection();
        private Button BtnDownSection;
        private Button BtnUpSection;
        private Button BtnEdit;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem importSettingToolStripMenuItem;
        private ToolStripMenuItem exportSettingToolStripMenuItem;

        protected GradingSection _CurrentSelectSection = null;
     
        public ConfigGradingSection()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            // TODO: Add any initialization after the InitializeComponent call
           
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigGradingSection));
            this.C_PropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.C_SplitterLR = new System.Windows.Forms.Splitter();
            this.C_SplitterTB = new System.Windows.Forms.Splitter();
            this.C_PanelMain = new System.Windows.Forms.Panel();
            this.lstPostions = new System.Windows.Forms.ListBox();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.palSections = new System.Windows.Forms.Panel();
            this.BtnEdit = new System.Windows.Forms.Button();
            this.BtnDownSection = new System.Windows.Forms.Button();
            this.BtnUpSection = new System.Windows.Forms.Button();
            this.btnRemoveSection = new System.Windows.Forms.Button();
            this.lstGradingSections = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.importSettingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportSettingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BtnAddSections = new System.Windows.Forms.Button();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.C_PanelMain.SuspendLayout();
            this.palSections.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
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
            // C_SplitterTB
            // 
            this.C_SplitterTB.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.C_SplitterTB.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.C_SplitterTB.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.C_SplitterTB.Location = new System.Drawing.Point(0, 381);
            this.C_SplitterTB.Name = "C_SplitterTB";
            this.C_SplitterTB.Size = new System.Drawing.Size(461, 3);
            this.C_SplitterTB.TabIndex = 3;
            this.C_SplitterTB.TabStop = false;
            // 
            // C_PanelMain
            // 
            this.C_PanelMain.Controls.Add(this.lstPostions);
            this.C_PanelMain.Controls.Add(this.splitter2);
            this.C_PanelMain.Controls.Add(this.palSections);
            this.C_PanelMain.Controls.Add(this.splitter1);
            this.C_PanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.C_PanelMain.Location = new System.Drawing.Point(0, 0);
            this.C_PanelMain.Name = "C_PanelMain";
            this.C_PanelMain.Size = new System.Drawing.Size(461, 381);
            this.C_PanelMain.TabIndex = 4;
            // 
            // lstPostions
            // 
            this.lstPostions.Dock = System.Windows.Forms.DockStyle.Top;
            this.lstPostions.FormattingEnabled = true;
            this.lstPostions.ItemHeight = 14;
            this.lstPostions.Location = new System.Drawing.Point(235, 0);
            this.lstPostions.Name = "lstPostions";
            this.lstPostions.Size = new System.Drawing.Size(226, 382);
            this.lstPostions.TabIndex = 13;
            this.lstPostions.SelectedIndexChanged += new System.EventHandler(this.lstPostions_SelectedIndexChanged);
            // 
            // splitter2
            // 
            this.splitter2.Location = new System.Drawing.Point(232, 0);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(3, 381);
            this.splitter2.TabIndex = 12;
            this.splitter2.TabStop = false;
            // 
            // palSections
            // 
            this.palSections.Controls.Add(this.BtnEdit);
            this.palSections.Controls.Add(this.BtnDownSection);
            this.palSections.Controls.Add(this.BtnUpSection);
            this.palSections.Controls.Add(this.btnRemoveSection);
            this.palSections.Controls.Add(this.lstGradingSections);
            this.palSections.Controls.Add(this.BtnAddSections);
            this.palSections.Dock = System.Windows.Forms.DockStyle.Left;
            this.palSections.Location = new System.Drawing.Point(3, 0);
            this.palSections.Name = "palSections";
            this.palSections.Size = new System.Drawing.Size(229, 381);
            this.palSections.TabIndex = 11;
            // 
            // BtnEdit
            // 
            this.BtnEdit.Location = new System.Drawing.Point(55, 352);
            this.BtnEdit.Name = "BtnEdit";
            this.BtnEdit.Size = new System.Drawing.Size(116, 23);
            this.BtnEdit.TabIndex = 18;
            this.BtnEdit.Text = "Edit Section";
            this.BtnEdit.UseVisualStyleBackColor = true;
            this.BtnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // BtnDownSection
            // 
            this.BtnDownSection.BackColor = System.Drawing.SystemColors.Control;
            this.BtnDownSection.Image = ((System.Drawing.Image)(resources.GetObject("BtnDownSection.Image")));
            this.BtnDownSection.Location = new System.Drawing.Point(144, 276);
            this.BtnDownSection.Name = "BtnDownSection";
            this.BtnDownSection.Size = new System.Drawing.Size(48, 29);
            this.BtnDownSection.TabIndex = 17;
            this.BtnDownSection.UseVisualStyleBackColor = false;
            this.BtnDownSection.Click += new System.EventHandler(this.BtnDownSection_Click);
            // 
            // BtnUpSection
            // 
            this.BtnUpSection.BackColor = System.Drawing.SystemColors.Control;
            this.BtnUpSection.Image = ((System.Drawing.Image)(resources.GetObject("BtnUpSection.Image")));
            this.BtnUpSection.Location = new System.Drawing.Point(55, 276);
            this.BtnUpSection.Name = "BtnUpSection";
            this.BtnUpSection.Size = new System.Drawing.Size(48, 29);
            this.BtnUpSection.TabIndex = 16;
            this.BtnUpSection.UseVisualStyleBackColor = false;
            this.BtnUpSection.Click += new System.EventHandler(this.BtnUpSection_Click);
            // 
            // btnRemoveSection
            // 
            this.btnRemoveSection.Location = new System.Drawing.Point(132, 318);
            this.btnRemoveSection.Name = "btnRemoveSection";
            this.btnRemoveSection.Size = new System.Drawing.Size(91, 23);
            this.btnRemoveSection.TabIndex = 15;
            this.btnRemoveSection.Text = "Remove";
            this.btnRemoveSection.UseVisualStyleBackColor = true;
            this.btnRemoveSection.Click += new System.EventHandler(this.btnRemoveSection_Click);
            // 
            // lstGradingSections
            // 
            this.lstGradingSections.ContextMenuStrip = this.contextMenuStrip1;
            this.lstGradingSections.Dock = System.Windows.Forms.DockStyle.Top;
            this.lstGradingSections.FormattingEnabled = true;
            this.lstGradingSections.ItemHeight = 14;
            this.lstGradingSections.Location = new System.Drawing.Point(0, 0);
            this.lstGradingSections.Name = "lstGradingSections";
            this.lstGradingSections.Size = new System.Drawing.Size(229, 270);
            this.lstGradingSections.TabIndex = 0;
            this.lstGradingSections.SelectedIndexChanged += new System.EventHandler(this.lstGradingSections_SelectedIndexChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importSettingToolStripMenuItem,
            this.exportSettingToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(155, 70);
            // 
            // importSettingToolStripMenuItem
            // 
            this.importSettingToolStripMenuItem.Name = "importSettingToolStripMenuItem";
            this.importSettingToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.importSettingToolStripMenuItem.Text = "&Import Setting";
            this.importSettingToolStripMenuItem.Click += new System.EventHandler(this.importSettingToolStripMenuItem_Click);
            // 
            // exportSettingToolStripMenuItem
            // 
            this.exportSettingToolStripMenuItem.Name = "exportSettingToolStripMenuItem";
            this.exportSettingToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.exportSettingToolStripMenuItem.Text = "Export Setting";
            this.exportSettingToolStripMenuItem.Click += new System.EventHandler(this.exportSettingToolStripMenuItem_Click);
            // 
            // BtnAddSections
            // 
            this.BtnAddSections.Location = new System.Drawing.Point(6, 318);
            this.BtnAddSections.Name = "BtnAddSections";
            this.BtnAddSections.Size = new System.Drawing.Size(97, 23);
            this.BtnAddSections.TabIndex = 14;
            this.BtnAddSections.Text = "Add Section";
            this.BtnAddSections.UseVisualStyleBackColor = true;
            this.BtnAddSections.Click += new System.EventHandler(this.BtnAddSections_Click);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 381);
            this.splitter1.TabIndex = 10;
            this.splitter1.TabStop = false;
            // 
            // ConfigGradingSection
            // 
            this.Controls.Add(this.C_PanelMain);
            this.Controls.Add(this.C_SplitterTB);
            this.Controls.Add(this.C_SplitterLR);
            this.Controls.Add(this.C_PropertyGrid);
            this.Name = "ConfigGradingSection";
            this.Size = new System.Drawing.Size(664, 384);
            this.Load += new System.EventHandler(this.ConfigGridInfo_Load);
            this.C_PanelMain.ResumeLayout(false);
            this.palSections.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        public override void UpdateView(ExControlView i_View)
        {
            if (!(i_View is GradingView)) return;   //Added this code at 2008-11-20 15:24:38@Simon

            this._GradingSectionCollection = new GradingSectionCollection();

            foreach (GradingSection playerPostionSection in this.lstGradingSections.Items)
            {
                this._GradingSectionCollection.Add(playerPostionSection);
            }

            (i_View as GradingView).GradingSectionCollection = this._GradingSectionCollection;
        }

        public override void SetView(ExControlView i_View)
        {           
            if (!(i_View is GradingView)) return;   //Added this code at 2008-11-20 15:24:38@Simon

            this._GradingSectionCollection = (i_View as GradingView).GradingSectionCollection;

            this.lstGradingSections.Items.Clear();

            this.lstPostions.Items.Clear();

            foreach (GradingSection gradingSection in this._GradingSectionCollection)
            {
                this.lstGradingSections.Items.Add(gradingSection);
            }

            if (this.lstGradingSections.Items.Count > 0) this.lstGradingSections.SelectedIndex = 0;
        }

   

        private void ConfigGridInfo_Load(object sender, System.EventArgs e)
        {

        }
      
  

        private void MoveUpItem(ListBox lb)
        {
            int index = lb.SelectedIndex;

            if (index <= 0) return;

            object value = lb.Items[index];

            lb.Items.RemoveAt(index);

            lb.Items.Insert(index - 1, value);

            lb.SelectedIndex = index - 1;
        }
        private void MoveDownItem(ListBox lb)
        {
            int index = lb.SelectedIndex;

            if (index <= 0) return;

            object value = lb.Items[index];

            lb.Items.RemoveAt(index);

            lb.Items.Insert(index + 1, value);

            lb.SelectedIndex = index + 1;
        }

       

        
        private void BtnAddSections_Click(object sender, EventArgs e)
        {            
            GradingSection gradingSection = new GradingSection();

            gradingSection.SectionName = string.Empty;

            GradingPostionsEditForm gradingPostionsEditForm = new GradingPostionsEditForm(gradingSection);

            if (gradingPostionsEditForm.ShowDialog() != DialogResult.OK) return;

            gradingSection = gradingPostionsEditForm._GradingSection;

           if (!this.lstGradingSections.Items.Contains(gradingSection))
           {
               this.lstGradingSections.Items.Add(gradingSection);

               this.lstGradingSections.SelectedIndex = this.lstGradingSections.FindStringExact(gradingSection.SectionName);
           }

           else
           {
               this.lstGradingSections.SelectedIndex = this.lstGradingSections.FindStringExact(gradingSection.SectionName);            

                Webb.Utilities.MessageBoxEx.ShowMessage("faild to add a existing name in the list!"); 
           }          

        }



        private void BtnUpSection_Click(object sender, EventArgs e)
        {
            this.MoveUpItem(this.lstGradingSections);
        }

        private void BtnDownSection_Click(object sender, EventArgs e)
        {
            this.MoveDownItem(this.lstGradingSections);
        }

        private void BtnUpField_Click(object sender, EventArgs e)
        {
            this.MoveUpItem(this.lstPostions);
        }

        private void BtnDownField_Click(object sender, EventArgs e)
        {
            this.MoveDownItem(this.lstPostions);
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            int selectIndex=this.lstGradingSections.SelectedIndex;

            if (selectIndex < 0) return;

            this._CurrentSelectSection = this.lstGradingSections.SelectedItem as GradingSection;

            GradingPostionsEditForm gradingPostionsEditForm = new GradingPostionsEditForm(this._CurrentSelectSection);

            if (gradingPostionsEditForm.ShowDialog() != DialogResult.OK) return;

            this._CurrentSelectSection = gradingPostionsEditForm._GradingSection;
          
             this.lstGradingSections.Items[selectIndex] = this._CurrentSelectSection;

             ListSelectSectionsChanged();
        }

        private void btnRemoveSection_Click(object sender, EventArgs e)
        {
            int selectIndex = this.lstGradingSections.SelectedIndex;

            if (selectIndex < 0) return;

            this.lstGradingSections.Items.RemoveAt(selectIndex);

            this.lstGradingSections.SelectedIndex = -1;
        }

        private void lstGradingSections_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListSelectSectionsChanged();
        }

        private void ListSelectSectionsChanged()
        {
            this.lstPostions.Items.Clear();

            object selectItem = this.lstGradingSections.SelectedItem;

            if (selectItem == null)
            {
                this._CurrentSelectSection = null;               
            }
            else
            {
                this._CurrentSelectSection = selectItem as GradingSection;

                foreach (GradingPostion gradingposition in this._CurrentSelectSection.InnerList)
                {
                    this.lstPostions.Items.Add(gradingposition);
                }
                if (this.lstPostions.Items.Count > 0) this.lstPostions.SelectedIndex = 0;
            }
        }

        private void lstPostions_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.C_PropertyGrid.SelectedObject = this.lstPostions.SelectedItem;
        }

        private void C_PropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            object objValue = this.C_PropertyGrid.SelectedObject;

            if (objValue == null || this.lstPostions.SelectedIndex<0) return;

            this.lstPostions.Items[this.lstPostions.SelectedIndex] = objValue;
        }

        private void importSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ImportSetting();
        }
        private void ImportSetting()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = string.Format("ExportFile(*{0})|*{0}", ".exf");

                if (DialogResult.OK != openFileDialog.ShowDialog()) return;
                
                if (System.IO.File.Exists(openFileDialog.FileName))
                {
                    try
                    {
                        System.IO.FileStream stream = System.IO.File.Open(openFileDialog.FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);

                        this._GradingSectionCollection = Webb.Utilities.Serializer.DeserializeObject(stream) as GradingSectionCollection;

                        stream.Close();

                        stream.Dispose();
                    }
                    catch (Exception ex)
                    {
                        Webb.Utilities.MessageBoxEx.ShowError("Failed to import!\n" + ex.Message);

                        return;
                    }                 
                }
            }
            
           
            this.lstGradingSections.Items.Clear();

            this.lstPostions.Items.Clear();

            foreach (GradingSection gradingSection in this._GradingSectionCollection)
            {
                this.lstGradingSections.Items.Add(gradingSection);
            }

            if (this.lstGradingSections.Items.Count > 0) this.lstGradingSections.SelectedIndex = 0;
        }
        private void ExportSetting()
        {
            this._GradingSectionCollection = new GradingSectionCollection();

            foreach (GradingSection playerPostionSection in this.lstGradingSections.Items)
            {
                this._GradingSectionCollection.Add(playerPostionSection);
            }
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = string.Format("ExportFile(*{0})|*{0}", ".exf");

                if (DialogResult.OK == saveFileDialog.ShowDialog())
                {
                    try
                    {
                        System.IO.FileStream stream = System.IO.File.Open(saveFileDialog.FileName, System.IO.FileMode.OpenOrCreate);

                        Webb.Utilities.Serializer.SerializeObject(stream, this._GradingSectionCollection);

                        stream.Close();
                    }
                    catch (Exception ex)
                    {
                        Webb.Utilities.MessageBoxEx.ShowError("Failed to export!\n" + ex.Message);

                        return;
                    }
                }
            }

        }

        private void exportSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ExportSetting();
        }    
    }
}
