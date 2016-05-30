using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Webb.Reports.DataProvider;
using System.Drawing.Design;
using System.Collections;
using System.Windows.Forms.Design;

namespace Webb.Reports.Editors
{
    public partial class FieldSelectForm : Form
    {
        private Hashtable HashCategories = new Hashtable();
        string strCategory = string.Empty;

        private GroupDescription _GroupDescription = null;

        public GroupDescription SelectedGroupDescription
        {
            get{return this._GroupDescription;}
        }

        public FieldSelectForm(string strField)
        {
            InitializeComponent();

            InitList(strField);
        }
        private void InitList(string initField)
        {
            this.LstFields.Items.Clear();

            HashCategories.Clear();

            this.cmbCategory.Visible = true;

            this.cmbCategory.Items.Clear();

            DataSet backDataSource = Webb.Reports.DataProvider.VideoPlayBackManager.DataSource;

            Webb.Reports.DataProvider.WebbDataProvider PublicDataProvider = Webb.Reports.DataProvider.VideoPlayBackManager.PublicDBProvider;

            if (PublicDataProvider != null && PublicDataProvider.DBSourceConfig != null && PublicDataProvider.DBSourceConfig.WebbDBType == WebbDBTypes.CoachCRM && backDataSource!=null && backDataSource.Tables.Count > 1)
            {    
                #region add categoryies from Table

                List<string> categories = new List<string>();

                string strCategoriesName = backDataSource.Tables[0].TableName;

                categories.Add(strCategoriesName);

                GroupDescriptionCollection fieldsInAllcategories = new GroupDescriptionCollection();

                HashCategories.Add(strCategoriesName, fieldsInAllcategories);

                foreach (DataRow dr in backDataSource.Tables[1].Rows)
                {
                    if (dr["CurrentField"] == null || (dr["CurrentField"] is System.DBNull))
                    {
                        continue;
                    }

                    string strTableName = dr["Category"].ToString();

                    string strField = dr["CurrentField"].ToString();

                    string strHeader = dr["DefaultHeader"].ToString();

                    string TableNameId = strTableName + "ID";

                    GroupDescriptionCollection fieldList;

                    if (HashCategories.Contains(strTableName))
                    {
                        fieldList = (GroupDescriptionCollection)HashCategories[strTableName];

                        if (!fieldList.Contains(strField))
                        {
                            fieldList.Add(strField, strHeader);
                        }
                    }
                    else
                    {
                        fieldList = new GroupDescriptionCollection();

                        fieldList.Add(strField, strHeader);

                        categories.Add(strTableName);

                        HashCategories.Add(strTableName, fieldList);
                    }

                    if(!fieldList.Contains(TableNameId))fieldList.Add(TableNameId, TableNameId);                   

                    if (!fieldsInAllcategories.Contains(strField)) fieldsInAllcategories.Add(strField, strHeader);

                    if (!fieldsInAllcategories.Contains(TableNameId)) fieldsInAllcategories.Add(TableNameId, TableNameId);
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

                GroupDescriptionCollection allGroupDescription = new GroupDescriptionCollection();               

                foreach (string strfield in Webb.Data.PublicDBFieldConverter.AvialableFields)
                {
                    //this.LstFields.Items.Add(new GroupDescription(strfield, strfield));

                    allGroupDescription.Add(new GroupDescription(strfield, strfield));
                }

                strCategory = "[All Avaliable Fields]";

                HashCategories.Add(strCategory, allGroupDescription);               

                this.cmbCategory.Items.Add(strCategory);

                this.cmbCategory.SelectedIndex = 0;

                #endregion
            }

             this.LstFields.SelectedIndex = this.LstFields.FindString(initField);

        }
        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (this.LstFields.SelectedItem == null)
            {
                MessageBox.Show("Please select a field from the list.", "select a field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            this._GroupDescription = this.LstFields.SelectedItem as GroupDescription;

            DialogResult = DialogResult.OK;
        }

        private void cmbCategory_TextChanged(object sender, EventArgs e)
        {
            string strText = this.cmbCategory.Text.Trim();

            this.LstFields.Items.Clear();

            if (this.HashCategories.Contains(strText))
            {
                GroupDescriptionCollection allFieldColumns = this.HashCategories[strText] as GroupDescriptionCollection;

                foreach (GroupDescription groupDescription in allFieldColumns)
                {
                    this.LstFields.Items.Add(groupDescription.Copy());
                }

                strCategory = strText;

            }
            else if (this.HashCategories.Contains(strCategory))
            {
                GroupDescriptionCollection allFieldColumns = this.HashCategories[strCategory] as GroupDescriptionCollection;

                foreach (GroupDescription groupDescription in allFieldColumns)
                {
                    if (groupDescription.ToString().ToLower().StartsWith(strText.ToLower()))
                    {
                        this.LstFields.Items.Add(groupDescription.Copy());
                    }
                }
            }
       
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
    public class FieldSelectEditor : System.Drawing.Design.UITypeEditor
    {

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        // Displays the UI for value selection.
        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {

            string InitValue = string.Empty;

            if (value!=null)
            {
                InitValue=value.ToString();
            }   
            

            IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

            if (edSvc != null)
            {
                FieldSelectForm edtorControl = new FieldSelectForm(InitValue);

                if (edSvc.ShowDialog(edtorControl) == DialogResult.OK)
                {
                    if (edtorControl.SelectedGroupDescription != null)
                    {
                        return edtorControl.SelectedGroupDescription.Field;
                    }
                    else
                    {
                        return string.Empty;
                    }
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
}
