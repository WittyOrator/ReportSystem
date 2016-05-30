using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Webb.Reports.DataProvider;

namespace Webb.Reports.ReportWizard.DataSourceProvider
{
    public partial class FrmCoachCRMWay : Form
    {
        private bool _DownLoadData=true;

        private string _FileName = string.Empty;

        public FrmCoachCRMWay()
        {
            InitializeComponent();
        }

        public bool DownLoadData
        {
            get { return this._DownLoadData; }
        }
        public string FileName 
        {
            get
            {               
                return _FileName;
            }
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (this.RadioUseLast.Checked)
            {
                _DownLoadData = false;

                FileDialog fileDialog = new OpenFileDialog();

                fileDialog.CheckFileExists = true;

                fileDialog.FileOk += new CancelEventHandler(fileDialog_FileOk);

                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    this._FileName=fileDialog.FileName;

                    DialogResult = DialogResult.OK;    
                }             
            }
            else
            {
                this._DownLoadData=true;

                DialogResult = DialogResult.OK;
            }
        }

        void fileDialog_FileOk(object sender, CancelEventArgs e)
        {
            DataSet ds=new DataSet();         

            FileDialog fileDialog=sender as FileDialog;          

            try
            {
                ds.ReadXml(fileDialog.FileName);
            }
            catch(Exception ex)
            {
                Webb.Utilities.MessageBoxEx.ShowError(ex.Message);

                e.Cancel = true;
            }

            ds.Dispose();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
