using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Collections.Specialized;
using Webb.Reports.DataProvider;


using Webb.Reports.ExControls.Data;

namespace Webb.Reports.ExControls.Editors
{
    #region class FieldsGroupEditorForm
    public class FieldsGroupEditorForm : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Button BtnOk;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.PropertyGrid propertyGrid1;

        private Label label1;
        private Button C_BtnRemove;
        private Button C_BtnAdd;
        private ListBox C_LBSelFields;
        private Button C_BtnMoveUp;
        private Button C_BtnMoveDown;
        private StringCollection OriginalFields;
        private ComboBox cmbCategory;
        private Panel palAllFields;
        private ListBox C_LBFields;
        private Label lblAll;
        public StringCollection selectedFields;

        private Hashtable HashCategories = new Hashtable();
        string strCategory = string.Empty;

        public FieldsGroupEditorForm(object value)
        {
            InitializeComponent();           

            OriginalFields = (value as StringCollection);

            this.InitList(OriginalFields);

            UpdateFields();
        }

        private void InitializeComponent()
        {
            this.BtnOk = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.label1 = new System.Windows.Forms.Label();
            this.C_BtnRemove = new System.Windows.Forms.Button();
            this.C_BtnAdd = new System.Windows.Forms.Button();
            this.C_LBSelFields = new System.Windows.Forms.ListBox();
            this.C_BtnMoveUp = new System.Windows.Forms.Button();
            this.C_BtnMoveDown = new System.Windows.Forms.Button();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.palAllFields = new System.Windows.Forms.Panel();
            this.C_LBFields = new System.Windows.Forms.ListBox();
            this.lblAll = new System.Windows.Forms.Label();
            this.palAllFields.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnOk
            // 
            this.BtnOk.Location = new System.Drawing.Point(396, 349);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(79, 27);
            this.BtnOk.TabIndex = 0;
            this.BtnOk.Text = "OK";
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Location = new System.Drawing.Point(524, 349);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(79, 27);
            this.BtnCancel.TabIndex = 0;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.LineColor = System.Drawing.SystemColors.ScrollBar;
            this.propertyGrid1.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(130, 130);
            this.propertyGrid1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(392, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 17);
            this.label1.TabIndex = 12;
            this.label1.Text = "Selected Fields";
            // 
            // C_BtnRemove
            // 
            this.C_BtnRemove.Location = new System.Drawing.Point(285, 173);
            this.C_BtnRemove.Name = "C_BtnRemove";
            this.C_BtnRemove.Size = new System.Drawing.Size(53, 24);
            this.C_BtnRemove.TabIndex = 10;
            this.C_BtnRemove.Text = "<=";
            this.C_BtnRemove.Click += new System.EventHandler(this.C_BtnRemove_Click);
            // 
            // C_BtnAdd
            // 
            this.C_BtnAdd.Location = new System.Drawing.Point(285, 107);
            this.C_BtnAdd.Name = "C_BtnAdd";
            this.C_BtnAdd.Size = new System.Drawing.Size(53, 24);
            this.C_BtnAdd.TabIndex = 9;
            this.C_BtnAdd.Text = "=>";
            this.C_BtnAdd.Click += new System.EventHandler(this.C_BtnAdd_Click);
            // 
            // C_LBSelFields
            // 
            this.C_LBSelFields.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.C_LBSelFields.HorizontalScrollbar = true;
            this.C_LBSelFields.ItemHeight = 12;
            this.C_LBSelFields.Location = new System.Drawing.Point(353, 30);
            this.C_LBSelFields.Name = "C_LBSelFields";
            this.C_LBSelFields.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.C_LBSelFields.Size = new System.Drawing.Size(254, 256);
            this.C_LBSelFields.TabIndex = 6;
            this.C_LBSelFields.DoubleClick += new System.EventHandler(this.C_LBSelFields_DoubleClick);
            // 
            // C_BtnMoveUp
            // 
            this.C_BtnMoveUp.Location = new System.Drawing.Point(394, 297);
            this.C_BtnMoveUp.Name = "C_BtnMoveUp";
            this.C_BtnMoveUp.Size = new System.Drawing.Size(87, 25);
            this.C_BtnMoveUp.TabIndex = 7;
            this.C_BtnMoveUp.Text = "Move Up";
            this.C_BtnMoveUp.Click += new System.EventHandler(this.C_BtnMoveUp_Click);
            // 
            // C_BtnMoveDown
            // 
            this.C_BtnMoveDown.Location = new System.Drawing.Point(511, 297);
            this.C_BtnMoveDown.Name = "C_BtnMoveDown";
            this.C_BtnMoveDown.Size = new System.Drawing.Size(87, 25);
            this.C_BtnMoveDown.TabIndex = 8;
            this.C_BtnMoveDown.Text = "Move Down";
            this.C_BtnMoveDown.Click += new System.EventHandler(this.C_BtnMoveDown_Click);
            // 
            // cmbCategory
            // 
            this.cmbCategory.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmbCategory.DropDownHeight = 300;
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.IntegralHeight = false;
            this.cmbCategory.Location = new System.Drawing.Point(0, 0);
            this.cmbCategory.MaxDropDownItems = 18;
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(266, 20);
            this.cmbCategory.TabIndex = 13;
            this.cmbCategory.TextChanged += new System.EventHandler(this.cmbCategory_TextChanged);
            // 
            // palAllFields
            // 
            this.palAllFields.Controls.Add(this.C_LBFields);
            this.palAllFields.Controls.Add(this.cmbCategory);
            this.palAllFields.Location = new System.Drawing.Point(0, 30);
            this.palAllFields.Name = "palAllFields";
            this.palAllFields.Size = new System.Drawing.Size(266, 289);
            this.palAllFields.TabIndex = 14;
            // 
            // C_LBFields
            // 
            this.C_LBFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.C_LBFields.HorizontalScrollbar = true;
            this.C_LBFields.ItemHeight = 12;
            this.C_LBFields.Location = new System.Drawing.Point(0, 20);
            this.C_LBFields.Name = "C_LBFields";
            this.C_LBFields.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.C_LBFields.Size = new System.Drawing.Size(266, 268);
            this.C_LBFields.Sorted = true;
            this.C_LBFields.TabIndex = 14;
            this.C_LBFields.DoubleClick += new System.EventHandler(this.C_LBFields_DoubleClick);
            // 
            // lblAll
            // 
            this.lblAll.AutoSize = true;
            this.lblAll.Location = new System.Drawing.Point(33, 9);
            this.lblAll.Name = "lblAll";
            this.lblAll.Size = new System.Drawing.Size(125, 12);
            this.lblAll.TabIndex = 15;
            this.lblAll.Text = "All available fields";
            // 
            // FieldsGroupEditorForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(619, 386);
            this.Controls.Add(this.lblAll);
            this.Controls.Add(this.palAllFields);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.C_BtnRemove);
            this.Controls.Add(this.C_BtnAdd);
            this.Controls.Add(this.C_LBSelFields);
            this.Controls.Add(this.C_BtnMoveUp);
            this.Controls.Add(this.C_BtnMoveDown);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnOk);
            this.Name = "FieldsGroupEditorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Field Editor";
            this.palAllFields.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
       

        private void InitList(StringCollection initFields)
        {
            this.C_LBFields.Items.Clear();

            this.C_LBSelFields.Items.Clear();

            if (initFields != null)
            {
                foreach (string strField in initFields)
                {
                    this.C_LBSelFields.Items.Add(strField);
                }
            }
            else
            {
                initFields = new StringCollection();
            }

            HashCategories.Clear();

            DataSet backDataSource = Webb.Reports.DataProvider.VideoPlayBackManager.DataSource;

            this.cmbCategory.Visible = true;

            this.cmbCategory.Items.Clear();

            ArrayList fieldsInAllcategories;

            Webb.Reports.DataProvider.WebbDataProvider PublicDataProvider = Webb.Reports.DataProvider.VideoPlayBackManager.PublicDBProvider;

            if (backDataSource!=null&&PublicDataProvider != null && PublicDataProvider.DBSourceConfig != null && PublicDataProvider.DBSourceConfig.WebbDBType == WebbDBTypes.CoachCRM && backDataSource.Tables.Count > 1)
            {
                #region add categoryies from Table

                 ArrayList categories = new ArrayList();

                string strCategoriesName = backDataSource.Tables[0].TableName;

                categories.Add(strCategoriesName);

                fieldsInAllcategories = new ArrayList();

                HashCategories.Add(strCategoriesName, fieldsInAllcategories);

                foreach (DataRow dr in backDataSource.Tables[1].Rows)
                {
                    if (dr["CurrentField"] == null || (dr["CurrentField"] is System.DBNull))
                    {
                        continue;
                    }

                    string strTableName = dr["Category"].ToString();

                    string strField = dr["CurrentField"].ToString();

                    ArrayList fieldList;

                    if (HashCategories.Contains(strTableName))
                    {
                        fieldList = (ArrayList)HashCategories[strTableName];

                        if (!fieldList.Contains(strField))
                        {
                            fieldList.Add(strField);
                        }
                    }
                    else
                    {
                        fieldList = new ArrayList();

                        fieldList.Add(strField);

                        categories.Add(strTableName);

                        HashCategories.Add(strTableName, fieldList);
                    }

                    if (!fieldsInAllcategories.Contains(strField)) fieldsInAllcategories.Add(strField);
                }
                #endregion            

                this.cmbCategory.Text = string.Empty;

                foreach (string strKey in categories)
                {
                    this.cmbCategory.Items.Add(strKey);
                }

                this.cmbCategory.SelectedIndex = 0;
            }
            else
            {
                #region Advantage /Victory Data            

                fieldsInAllcategories = new ArrayList();

                foreach (string strfield in Webb.Data.PublicDBFieldConverter.AvialableFields)
                {
                    fieldsInAllcategories.Add(strfield);

                    //if (this.C_LBSelFields.Items.Contains(strfield)) continue;

                    //this.C_LBFields.Items.Add(strfield);
                }

                strCategory = "[All Avaliable Fields]";

                HashCategories.Add(strCategory, fieldsInAllcategories);

                this.cmbCategory.Items.Add(strCategory);

                this.cmbCategory.SelectedIndex = 0;
                #endregion
            }            

        }


        private void UpdateFields()
        {
            selectedFields = new StringCollection();

            foreach (string strField in this.C_LBSelFields.Items)
            {
                selectedFields.Add(strField);
            }
        }

        private void cmbCategory_TextChanged(object sender, EventArgs e)
        {
            string strText = this.cmbCategory.Text.Trim();

            this.C_LBFields.Items.Clear();

            if (this.HashCategories.Contains(strText))
            {
                ArrayList allFieldColumns = this.HashCategories[strText] as ArrayList;

                foreach (string strField in allFieldColumns)
                {
                    if (this.C_LBSelFields.Items.Contains(strField)) continue;

                    this.C_LBFields.Items.Add(strField);
                }

                strCategory = strText;

            }
            else if (this.HashCategories.Contains(strCategory))
            {
                ArrayList allFieldColumns = this.HashCategories[strCategory] as ArrayList;

                foreach (string strField in allFieldColumns)
                {
                    if (this.C_LBSelFields.Items.Contains(strField)) continue;

                    if (strField.ToLower().StartsWith(strText.ToLower()))
                    {
                        this.C_LBFields.Items.Add(strField);
                    }
                }
            }

        }

     
        private void BtnOk_Click(object sender, System.EventArgs e)
        {
            UpdateFields();

            this.DialogResult = DialogResult.OK;
        }

        private void BtnCancel_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void C_BtnAdd_Click(object sender, System.EventArgs e)
        {
            ArrayList selected = new ArrayList();

            foreach (object value in this.C_LBFields.SelectedItems)
            {
                selected.Add(value);
            }

            foreach (object value in selected)
            {
                this.AddItem(this.C_LBSelFields, value);

                this.C_LBFields.Items.Remove(value);
            }

            this.C_LBSelFields.SelectedIndex = -1;
        }

        private void C_BtnRemove_Click(object sender, System.EventArgs e)
        {
            ArrayList selected = new ArrayList();

            foreach (object value in this.C_LBSelFields.SelectedItems)
            {
                selected.Add(value);
            }
          
            foreach (object value in selected)
            {
                this.AddItem(this.C_LBFields, value);

                this.C_LBSelFields.Items.Remove(value);
            }

        }

        private void C_LBFields_DoubleClick(object sender, System.EventArgs e)
        {
            object value = this.C_LBFields.SelectedItem;

            if (value == null) return;

            this.AddItem(this.C_LBSelFields, value);

            this.C_LBFields.Items.Remove(value);

            this.C_LBFields.SelectedIndex = -1;
        }

        private void C_LBSelFields_DoubleClick(object sender, System.EventArgs e)
        {
            object value = this.C_LBSelFields.SelectedItem;

            if (value == null) return;

            this.AddItem(this.C_LBFields, value);

            this.C_LBSelFields.Items.Remove(value);

            this.C_LBSelFields.SelectedIndex = -1;

        }

        private void AddItem(ListBox lb, object item)
        {
            string itemEle = item as string;

            if (lb.Items.Contains(itemEle)) return;

            lb.Items.Add(itemEle);
        }

        private void C_BtnMoveUp_Click(object sender, System.EventArgs e)
        {
            int index = this.C_LBSelFields.SelectedIndex;

            if (index <= 0) return;

            object value = this.C_LBSelFields.Items[index];

            this.C_LBSelFields.Items.RemoveAt(index);

            this.C_LBSelFields.Items.Insert(index - 1, value);

            this.C_LBSelFields.SelectedIndex = index - 1;
        }

        private void C_BtnMoveDown_Click(object sender, System.EventArgs e)
        {
            int index = this.C_LBSelFields.SelectedIndex;

            if (index < 0 || index > this.C_LBSelFields.Items.Count - 2) return;

            object value = this.C_LBSelFields.Items[index];

            this.C_LBSelFields.Items.RemoveAt(index);

            this.C_LBSelFields.Items.Insert(index + 1, value);

            this.C_LBSelFields.SelectedIndex = index + 1;
        }      


    }
    #endregion

    #region class FieldsGroupEditor
    public class FieldsGroupEditor : System.Drawing.Design.UITypeEditor
    {
        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return System.Drawing.Design.UITypeEditorEditStyle.Modal;
        }

        // Displays the UI for value selection.
        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {
            if (!(value is StringCollection))
                return value;

            FieldsGroupEditorForm editorform = new FieldsGroupEditorForm(value);

            System.Windows.Forms.Design.IWindowsFormsEditorService edSvc = (System.Windows.Forms.Design.IWindowsFormsEditorService)provider.GetService(typeof(System.Windows.Forms.Design.IWindowsFormsEditorService));

            if (edSvc != null)
            {
                if (edSvc.ShowDialog(editorform) == DialogResult.OK)
                {
                    return editorform.selectedFields;
                }
            }

            return value;
        }


        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        public override void PaintValue(System.Drawing.Design.PaintValueEventArgs e)
        {
        }

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        public override bool GetPaintValueSupported(System.ComponentModel.ITypeDescriptorContext context)
        {
            return false;
        }
    }
    #endregion    

}
