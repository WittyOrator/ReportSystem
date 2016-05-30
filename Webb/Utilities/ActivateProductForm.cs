using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Webb.Utilities
{
    public partial class ActivateProductForm : Form
    {
        RegisterWebbReportProduct registerWebbProduct;

        WebbAcivateCls _WebbActivateCls;

        public ActivateProductForm(RegisterWebbReportProduct _RegisterWebbProduct, WebbAcivateCls webbActivateCls)
        {
            _WebbActivateCls = webbActivateCls;

            int nRestDays = webbActivateCls.GetRestDays();

            if (nRestDays <= 0)
            {
                MessageBox.Show("The trial version of this product have been expired!\nContact Webb Electronics today for purchase!\n\nPhone:972-242-5400", "expired", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
               
            }

            InitializeComponent();

            registerWebbProduct = _RegisterWebbProduct;   

            this.txtSeeds.Text = registerWebbProduct.GetSerialNumber();

            if (!webbActivateCls.Activate)
            {
                this.lblExpired.Text = "To use 'Webb Report Wizard',Your need to activate it first! ";

                this.BtnContinueTrial.Visible = false;

            }
            else if (nRestDays<= 0)
            {
                this.lblExpired.Text = "The trial version of this product has expired! ";

                this.BtnContinueTrial.Visible = false;
            }
            else
            {
                this.lblExpired.Text = string.Format("{0} days left for the trial version of Webb Report Wizard!", nRestDays);               

                this.BtnContinueTrial.Visible = true;
            }
        }


        public void SetResumeText()
        {
            this.BtnExit.Text = "Resume";
        }

        private void BtnActivate_Click(object sender, EventArgs e)
        {
            string strSeeds=this.txtSeeds.Text;

            string strKeys=this.txtKeys.Text.Trim();

            if (this.txtKeys.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please input the keys first!","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);

                this.txtKeys.Focus();

                return;
            }

            bool isPermanently = false;

            int nDays = 0;

            bool checkOk = ActivateProductMethods.CheckSerial(strSeeds, strKeys, out isPermanently, out  nDays);

            _WebbActivateCls.IsPermanently = isPermanently;
            
            _WebbActivateCls.TrialDays=nDays;

           if (!checkOk)
           {
               MessageBox.Show("Invalid Keys!", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Stop);

               this.txtKeys.Focus();

               return;
           }

           if (_WebbActivateCls.Activate && !_WebbActivateCls.IsPermanently)
           {
               MessageBox.Show("The trial version of product have already been activated!", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Stop);

               this.txtKeys.Focus();

               return;
           }
           if (_WebbActivateCls.TrialDays <= 0 && !_WebbActivateCls.IsPermanently)
           {
               MessageBox.Show("The trial version of this product have been expired!\nContact Webb Electronics today for purchase!\n\nPhone:972-242-5400", "expired", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

               this.txtKeys.Focus();

               return;
           }

           _WebbActivateCls.SavePermanentUseSetting();         

           string strMsg = "Success to activate the product!";

           if (!_WebbActivateCls.IsPermanently)
           {
               strMsg += string.Format("\nYou have {0} trial days to use the product.", _WebbActivateCls.TrialDays); 
           }

           MessageBox.Show(strMsg, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);         
           
           DialogResult = DialogResult.OK;
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void BtnContinueTrial_Click(object sender, EventArgs e)
        {
            bool isPermanently=false;

            int nrestDays = _WebbActivateCls.GetRestDays();               

           if (nrestDays <= 0 && !_WebbActivateCls.IsPermanently)
           {
               MessageBox.Show("Webb Report Wizard\n\nThe trial version of this product have been expired!\nContact Webb Electronics today for purchase!\n\nPhone:972-242-5400", "Expired", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

               this.txtKeys.Focus();

               return;
           }

           _WebbActivateCls.LastUseDay = DateTime.Now.Date;

           WebbAcivateCls.Save(_WebbActivateCls, string.Empty);

           DialogResult = DialogResult.OK;
           
        }
    }
}
