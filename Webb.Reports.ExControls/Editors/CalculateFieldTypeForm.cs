using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Webb.Reports.ExControls.Data;

namespace Webb.Reports.ExControls.Editors
{
    public partial class CalculateFieldTypeForm : Form
    {
        private object objField = string.Empty;

        public object ObjField
        {
            get
            {
                return this.objField;
            }
        }

        public CalculateFieldTypeForm(object value)
        {
            InitializeComponent();           

            if (value is CalculateColumnInfo)
            {
                objField = (value as CalculateColumnInfo).Copy();               
            }
            else
            {
                objField = value;
            }

            if (objField == null) objField = string.Empty;           

            this.InitList();


        }
        private void InitList()
        {
            this.cmbField.Items.Clear();

            foreach (object field in Webb.Data.PublicDBFieldConverter.AvialableFields)
            {
                this.cmbField.Items.Add(field);
            }

            if (objField is CalculateColumnInfo)
            {
                this.radioCaluateColumnInfo.Checked = true;

                SetDisplayFormula(objField);        
            }
            else
            {
                this.radioString.Checked = true;

                this.cmbField.Text=objField.ToString();
               
            }
           
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            UpdateField();

            DialogResult = DialogResult.OK;
        }
        private bool UpdateField()
        {
            if (this.radioString.Checked)
            {
                this.objField = this.cmbField.Text;
            }
            else
            {
                if (this.objField == null) this.objField = new CalculateColumnEditor();
            }
            return true;
        }

        private void radio_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioCaluateColumnInfo.Checked)
            {
                this.cmbField.Enabled = false;
                this.BtnEditCalcInfo.Enabled = true;
                this.lblFormula.Visible = true;
            }
            else
            {
                this.BtnEditCalcInfo.Enabled = false;
                this.cmbField.Enabled = true;
                this.lblFormula.Visible = false;
            }
        }

        private void SetDisplayFormula(object objValue)
        {
            this.lblFormula.Text =string.Format("Formula: {0}",objValue);   
        }

        private void BtnEditCalcInfo_Click(object sender, EventArgs e)
        {
            CalculateColumnInfo calculateColumnInfo = objField as CalculateColumnInfo;

            if (calculateColumnInfo == null) calculateColumnInfo = new CalculateColumnInfo();

            CalculateColumnEditorForm calculateColumnInfoEditForm = new CalculateColumnEditorForm(calculateColumnInfo);

            if (calculateColumnInfoEditForm.ShowDialog() == DialogResult.OK)
            {
                calculateColumnInfoEditForm.HidechkDisplayedSteps();

                objField = calculateColumnInfoEditForm.Value;

                this.SetDisplayFormula(objField);
            }
        }
    }
}
