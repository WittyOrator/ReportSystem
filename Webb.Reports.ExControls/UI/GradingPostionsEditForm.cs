using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Collections.Specialized;
using Webb.Reports.DataProvider;

using Webb.Reports.ExControls.Data;

namespace Webb.Reports.ExControls.UI
{
    #region class GradingPostionsEditForm
    public class GradingPostionsEditForm : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Button BtnOk;
        private System.Windows.Forms.Button BtnCancel;
       
        private Button C_BtnRemove;
        private Button C_BtnAdd;
        private ListBox C_LBSelFields;
        private Button C_BtnMoveUp;
        private Button C_BtnMoveDown;
        private StringCollection OriginalFields;
        private ComboBox cmbCategory;
        private Panel palAllFields;
        private ListBox C_LBFields;    
       

        private Hashtable HashCategories = new Hashtable();
        private Label lblDown;
        private TextBox textBox1;
        private Label label1;
        private Label label2;

        string strCategory = string.Empty;
        private Button BtnEditJerseyNo;

        public GradingSection _GradingSection; 

        public GradingPostionsEditForm(GradingSection gradingSection)
        {
            InitializeComponent();

            this.InitList(gradingSection);

            _GradingSection = gradingSection;  
            
        }

        private void InitializeComponent()
        {
            this.BtnOk = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.C_BtnRemove = new System.Windows.Forms.Button();
            this.C_BtnAdd = new System.Windows.Forms.Button();
            this.C_LBSelFields = new System.Windows.Forms.ListBox();
            this.C_BtnMoveUp = new System.Windows.Forms.Button();
            this.C_BtnMoveDown = new System.Windows.Forms.Button();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.palAllFields = new System.Windows.Forms.Panel();
            this.C_LBFields = new System.Windows.Forms.ListBox();
            this.lblDown = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.BtnEditJerseyNo = new System.Windows.Forms.Button();
            this.palAllFields.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnOk
            // 
            this.BtnOk.Location = new System.Drawing.Point(452, 414);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(78, 22);
            this.BtnOk.TabIndex = 0;
            this.BtnOk.Text = "OK";
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Location = new System.Drawing.Point(564, 413);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(79, 24);
            this.BtnCancel.TabIndex = 0;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // C_BtnRemove
            // 
            this.C_BtnRemove.Location = new System.Drawing.Point(292, 234);
            this.C_BtnRemove.Name = "C_BtnRemove";
            this.C_BtnRemove.Size = new System.Drawing.Size(53, 24);
            this.C_BtnRemove.TabIndex = 10;
            this.C_BtnRemove.Text = "<=";
            this.C_BtnRemove.Click += new System.EventHandler(this.C_BtnRemove_Click);
            // 
            // C_BtnAdd
            // 
            this.C_BtnAdd.Location = new System.Drawing.Point(292, 151);
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
            this.C_LBSelFields.Location = new System.Drawing.Point(360, 61);
            this.C_LBSelFields.Name = "C_LBSelFields";
            this.C_LBSelFields.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.C_LBSelFields.Size = new System.Drawing.Size(283, 304);
            this.C_LBSelFields.TabIndex = 6;
            this.C_LBSelFields.DoubleClick += new System.EventHandler(this.C_LBSelFields_DoubleClick);
            // 
            // C_BtnMoveUp
            // 
            this.C_BtnMoveUp.Location = new System.Drawing.Point(360, 369);
            this.C_BtnMoveUp.Name = "C_BtnMoveUp";
            this.C_BtnMoveUp.Size = new System.Drawing.Size(75, 25);
            this.C_BtnMoveUp.TabIndex = 7;
            this.C_BtnMoveUp.Text = "Move Up";
            this.C_BtnMoveUp.Click += new System.EventHandler(this.C_BtnMoveUp_Click);
            // 
            // C_BtnMoveDown
            // 
            this.C_BtnMoveDown.Location = new System.Drawing.Point(452, 369);
            this.C_BtnMoveDown.Name = "C_BtnMoveDown";
            this.C_BtnMoveDown.Size = new System.Drawing.Size(78, 25);
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
            this.cmbCategory.Size = new System.Drawing.Size(269, 20);
            this.cmbCategory.TabIndex = 13;
            this.cmbCategory.TextChanged += new System.EventHandler(this.cmbCategory_TextChanged);
            // 
            // palAllFields
            // 
            this.palAllFields.Controls.Add(this.C_LBFields);
            this.palAllFields.Controls.Add(this.cmbCategory);
            this.palAllFields.Location = new System.Drawing.Point(7, 61);
            this.palAllFields.Name = "palAllFields";
            this.palAllFields.Size = new System.Drawing.Size(269, 343);
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
            this.C_LBFields.Size = new System.Drawing.Size(269, 316);
            this.C_LBFields.Sorted = true;
            this.C_LBFields.TabIndex = 14;
            this.C_LBFields.DoubleClick += new System.EventHandler(this.C_LBFields_DoubleClick);
            // 
            // lblDown
            // 
            this.lblDown.AutoSize = true;
            this.lblDown.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDown.Location = new System.Drawing.Point(35, 6);
            this.lblDown.Name = "lblDown";
            this.lblDown.Size = new System.Drawing.Size(115, 19);
            this.lblDown.TabIndex = 16;
            this.lblDown.Text = "Section Name";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(185, 7);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(429, 21);
            this.textBox1.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(50, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 19);
            this.label1.TabIndex = 16;
            this.label1.Text = "All Fields";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(394, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 19);
            this.label2.TabIndex = 16;
            this.label2.Text = "JerseyNo  Fields";
            // 
            // BtnEditJerseyNo
            // 
            this.BtnEditJerseyNo.Location = new System.Drawing.Point(564, 370);
            this.BtnEditJerseyNo.Name = "BtnEditJerseyNo";
            this.BtnEditJerseyNo.Size = new System.Drawing.Size(75, 23);
            this.BtnEditJerseyNo.TabIndex = 18;
            this.BtnEditJerseyNo.Text = "Edit ";
            this.BtnEditJerseyNo.UseVisualStyleBackColor = true;
            this.BtnEditJerseyNo.Click += new System.EventHandler(this.BtnEditJerseyNo_Click);
            // 
            // GradingPostionsEditForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(655, 440);
            this.Controls.Add(this.BtnEditJerseyNo);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblDown);
            this.Controls.Add(this.palAllFields);
            this.Controls.Add(this.C_BtnRemove);
            this.Controls.Add(this.C_BtnAdd);
            this.Controls.Add(this.C_LBSelFields);
            this.Controls.Add(this.C_BtnMoveUp);
            this.Controls.Add(this.C_BtnMoveDown);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnOk);
            this.Name = "GradingPostionsEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Field Editor";
            this.palAllFields.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        private bool UpdateFields()
        {    
            if (this.textBox1.Text.Trim() == string.Empty)
            {
                Webb.Utilities.MessageBoxEx.ShowError("Invalid section name!");

                return false;
            }

            if (this.C_LBSelFields.Items.Count<1)
            {
                Webb.Utilities.MessageBoxEx.ShowError("it should have 1 postion field at least!");

                return false;
            }

            _GradingSection = new GradingSection();

            _GradingSection.SectionName = this.textBox1.Text;

            _GradingSection.Clear();

            foreach (GradingPostion gradingPosition in this.C_LBSelFields.Items)
            {
                _GradingSection.Add(gradingPosition);
            }
            return true;
        }


        private void InitList(GradingSection gradingSection)
        {
            this.textBox1.Text = gradingSection.SectionName;

            this.C_LBFields.Items.Clear();

            this.C_LBSelFields.Items.Clear();
            
            foreach (GradingPostion gradingPostion in gradingSection)
            {
                this.C_LBSelFields.Items.Add(gradingPostion);
            }            

            HashCategories.Clear();

            this.cmbCategory.Visible = true;

            this.cmbCategory.Items.Clear();

            this.cmbCategory.Text = string.Empty;

            ArrayList fieldsInAllcategories = new ArrayList();

            DataSet backDataSource = Webb.Reports.DataProvider.VideoPlayBackManager.DataSource;

            Webb.Reports.DataProvider.WebbDataProvider PublicDataProvider = Webb.Reports.DataProvider.VideoPlayBackManager.PublicDBProvider;

            if (backDataSource != null && PublicDataProvider != null && PublicDataProvider.DBSourceConfig != null && PublicDataProvider.DBSourceConfig.WebbDBType == WebbDBTypes.CoachCRM && backDataSource.Tables.Count > 1)
            {                
                #region add categoryies from Table

                ArrayList categories = new ArrayList();

                string strCategoriesName = backDataSource.Tables[0].TableName;

                categories.Add(strCategoriesName);
              
                HashCategories.Add(strCategoriesName, fieldsInAllcategories);

                foreach (DataRow dr in backDataSource.Tables[1].Rows)
                {
                    if (dr["CurrentField"] == null || (dr["CurrentField"] is System.DBNull))
                    {
                        continue;
                    }

                    string strTableName = dr["Category"].ToString();

                    string strField = dr["CurrentField"].ToString();

                    GradingPostion newGradingPostion = new GradingPostion();

                    newGradingPostion.Field = strField;

                    ArrayList fieldList;

                    if (HashCategories.Contains(strTableName))
                    {
                        fieldList = (ArrayList)HashCategories[strTableName];

                        if (!fieldList.Contains(newGradingPostion))
                        {
                            fieldList.Add(newGradingPostion);
                        }
                    }
                    else
                    {
                        fieldList = new ArrayList();

                        fieldList.Add(newGradingPostion);

                        categories.Add(strTableName);

                        HashCategories.Add(strTableName, fieldList);
                    }

                    if (!fieldsInAllcategories.Contains(newGradingPostion)) fieldsInAllcategories.Add(newGradingPostion);
                }
                #endregion              

                foreach (string strKey in categories)
                {
                    this.cmbCategory.Items.Add(strKey);
                }

                this.cmbCategory.SelectedIndex = 0;

            }
            else
            {
                #region Advantage/Victory Data             

                foreach (string strField in Webb.Data.PublicDBFieldConverter.AvialableFields)
                {
                    GradingPostion newGradingPostion = new GradingPostion();

                    newGradingPostion.Field = strField;
                   
                    fieldsInAllcategories.Add(newGradingPostion);
                }

                strCategory = "[All Avaliable Fields]";

                HashCategories.Add(strCategory, fieldsInAllcategories);

                this.cmbCategory.Items.Add(strCategory);

                this.cmbCategory.SelectedIndex = 0;

                #endregion
            }

        }
    

        private void cmbCategory_TextChanged(object sender, EventArgs e)
        {
            string strText = this.cmbCategory.Text.Trim();

            this.C_LBFields.Items.Clear();

            if (this.HashCategories.Contains(strText))
            {
                ArrayList allFieldColumns = this.HashCategories[strText] as ArrayList;

                foreach (GradingPostion gradingPosition in allFieldColumns)
                {
                    if (this.C_LBSelFields.Items.Contains(gradingPosition)) continue;

                    this.C_LBFields.Items.Add(gradingPosition);
                }

                strCategory = strText;

            }
            else if (this.HashCategories.Contains(strCategory))
            {
                ArrayList allFieldColumns = this.HashCategories[strCategory] as ArrayList;

                foreach (GradingPostion gradingPosition in allFieldColumns)
                {
                    if (this.C_LBSelFields.Items.Contains(gradingPosition)) continue;

                    if (gradingPosition.Field.ToLower().StartsWith(strText.ToLower()))
                    {
                        this.C_LBFields.Items.Add(gradingPosition);
                    }
                }
            }

        }


        private void BtnOk_Click(object sender, System.EventArgs e)
        {
            if (!UpdateFields()) return;
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
            if (lb.Items.Contains(item)) return;

            lb.Items.Add(item);
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

        private void BtnEditJerseyNo_Click(object sender, EventArgs e)
        {
            EditItem();
        }

        private void EditItem()
        {
            int selectIndex = this.C_LBSelFields.SelectedIndex;

            object value = this.C_LBSelFields.SelectedItem;

            if (selectIndex < 0 || value == null) return;

            GradingPostion EditValue = (value as GradingPostion).Copy();

            Webb.Utilities.PropertyForm propertyForm = new Webb.Utilities.PropertyForm();

            propertyForm.BindProperty("Jersey Editor", EditValue);

            if (DialogResult.OK == propertyForm.ShowDialog())
            {
                this.C_LBSelFields.Items[selectIndex] = propertyForm.Object;
            }           
        }

    }
    #endregion
}
