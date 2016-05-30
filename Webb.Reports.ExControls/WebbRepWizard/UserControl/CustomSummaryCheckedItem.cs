using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Webb.Reports.ExControls.WebbRepWizard.Data;

namespace Webb.Reports.ReportWizard.WizardInfo
{
    public partial class CustomSummaryCheckedItem : UserControl
    {
        private SummaryItemDescription summaryItem=null;

        private CheckedSummaryItemListBox parentCheckedBox;

        private Color SelectionColor = Color.DarkBlue;

        public SummaryItemDescription SummaryItem
        {
            get
            {
                return this.summaryItem;
            }
        }
        public bool ItemChecked
        {
            get { return this.chkEdItem.Checked;}
            set { this.chkEdItem.Checked = value;
                  summaryItem.IsChecked = value;
                  if (this.chkEdItem.Checked)
                  {
                      this.palComboBox.Visible = true;
                  }
                  else
                  {
                      this.palComboBox.Visible = false;
                  }
               }
        }
        public void SetField(string strField)
        {
            this.summaryItem.Field = strField;

            this.chkEdItem.Text = this.summaryItem.ToString();
        }


        public CustomSummaryCheckedItem()
        {
            InitializeComponent();
        }
        public CustomSummaryCheckedItem(SummaryItemDescription i_SummaryItem,CheckedSummaryItemListBox i_ParentCheckedBox)
        {
            InitializeComponent();

            summaryItem = i_SummaryItem;

            parentCheckedBox = i_ParentCheckedBox;

            this.chkEdItem.Text = summaryItem.ToString();

            this.comboBox1.SelectedIndex = summaryItem.OrderIndex;
        }
        public void SetView(SummaryItemDescription groupSummaryItem)
        {
            if (groupSummaryItem != null)
            {
                this.ItemChecked = true;

                this.summaryItem=groupSummaryItem.Copy();

                this.comboBox1.SelectedIndex = groupSummaryItem.OrderIndex;
                
                this.chkEdItem.Text=this.SummaryItem.ToString();
            }
            else
            {
                this.ItemChecked = false;

                SummaryItem.Revert();
                
                this.comboBox1.SelectedIndex =0;

                this.chkEdItem.Text = this.SummaryItem.ToString();
            }

        }
        public void SetSelectionColor(bool bSelect)
        {
            if (bSelect)
            {
                this.chkEdItem.BackColor = SelectionColor;

                this.chkEdItem.ForeColor = Color.White;

                this.lblPositions.BackColor = SelectionColor;

                this.lblPositions.ForeColor = Color.White;
            }
            else
            {
                this.chkEdItem.BackColor = Color.White;

                this.chkEdItem.ForeColor = Color.Black;

                this.lblPositions.BackColor = Color.White;

                this.lblPositions.ForeColor = Color.Black;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            summaryItem.OrderIndex = this.comboBox1.SelectedIndex;
        }     

        private void ChangeSelectItem()
        {
             CustomSummaryCheckedItem beforeSelectedSummaryItemBox = parentCheckedBox.SelectedSummaryItemBox;

            if (beforeSelectedSummaryItemBox == this) return;

            if (beforeSelectedSummaryItemBox != null)
            {
                beforeSelectedSummaryItemBox.SetSelectionColor(false);
            }

            this.SetSelectionColor(true);

            parentCheckedBox.SelectedSummaryItemBox = this;       
        }

        private void chkEdItem_Click(object sender, EventArgs e)
        {
            ChangeSelectItem();
        }

        private void chkEdItem_CheckedChanged(object sender, EventArgs e)
        {
            this.summaryItem.IsChecked = this.chkEdItem.Checked;

            if (this.chkEdItem.Checked)
            {
                this.palComboBox.Visible = true;
            }
            else
            {
                this.palComboBox.Visible = false;
            }
        }

        private void lblPositions_Click(object sender, EventArgs e)
        {
            ChangeSelectItem();
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            ChangeSelectItem();
        }
    }
}
