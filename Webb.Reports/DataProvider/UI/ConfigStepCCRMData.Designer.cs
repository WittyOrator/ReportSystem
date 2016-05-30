namespace Webb.Reports.DataProvider.UI
{
    public partial class ConfigStepCCRMData : Webb.Utilities.Wizards.WinzardControlBase
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
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
            this.chkDownLoadLateset = new System.Windows.Forms.CheckBox();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.txtLoginName = new System.Windows.Forms.TextBox();
            this.BtnBrowse = new System.Windows.Forms.Button();
            this.txtDataLocation = new System.Windows.Forms.TextBox();
            this.cmbServer = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // chkDownLoadLateset
            // 
            this.chkDownLoadLateset.AutoSize = true;
            this.chkDownLoadLateset.Checked = true;
            this.chkDownLoadLateset.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDownLoadLateset.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDownLoadLateset.Location = new System.Drawing.Point(20, 280);
            this.chkDownLoadLateset.Name = "chkDownLoadLateset";
            this.chkDownLoadLateset.Size = new System.Drawing.Size(416, 23);
            this.chkDownLoadLateset.TabIndex = 26;
            this.chkDownLoadLateset.Text = "Only download the latest record for each player";
            this.chkDownLoadLateset.UseVisualStyleBackColor = true;
            // 
            // txtPwd
            // 
            this.txtPwd.Location = new System.Drawing.Point(223, 187);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.PasswordChar = '*';
            this.txtPwd.Size = new System.Drawing.Size(407, 22);
            this.txtPwd.TabIndex = 24;
            // 
            // txtLoginName
            // 
            this.txtLoginName.Location = new System.Drawing.Point(224, 115);
            this.txtLoginName.Name = "txtLoginName";
            this.txtLoginName.Size = new System.Drawing.Size(406, 22);
            this.txtLoginName.TabIndex = 25;
            // 
            // BtnBrowse
            // 
            this.BtnBrowse.Location = new System.Drawing.Point(684, 421);
            this.BtnBrowse.Name = "BtnBrowse";
            this.BtnBrowse.Size = new System.Drawing.Size(67, 21);
            this.BtnBrowse.TabIndex = 23;
            this.BtnBrowse.Text = "Browse";
            this.BtnBrowse.UseVisualStyleBackColor = true;
            // 
            // txtDataLocation
            // 
            this.txtDataLocation.BackColor = System.Drawing.SystemColors.Control;
            this.txtDataLocation.Enabled = false;
            this.txtDataLocation.Location = new System.Drawing.Point(9, 420);
            this.txtDataLocation.Name = "txtDataLocation";
            this.txtDataLocation.Size = new System.Drawing.Size(669, 22);
            this.txtDataLocation.TabIndex = 22;
            // 
            // cmbServer
            // 
            this.cmbServer.Items.AddRange(new object[] {
            "www.coachescrm.com",
            "www.webbeng.net"});
            this.cmbServer.Location = new System.Drawing.Point(224, 42);
            this.cmbServer.Name = "cmbServer";
            this.cmbServer.Size = new System.Drawing.Size(540, 22);
            this.cmbServer.TabIndex = 21;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(16, 390);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(409, 27);
            this.label2.TabIndex = 18;
            this.label2.Text = "Default CCRM Data Saved Location";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(18, 191);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 28);
            this.label4.TabIndex = 17;
            this.label4.Text = "Password:";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(16, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(193, 27);
            this.label3.TabIndex = 20;
            this.label3.Text = "Login Name:";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(208, 21);
            this.label1.TabIndex = 19;
            this.label1.Text = "Server Address:";
            // 
            // ConfigStepCCRMData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkDownLoadLateset);
            this.Controls.Add(this.txtPwd);
            this.Controls.Add(this.txtLoginName);
            this.Controls.Add(this.BtnBrowse);
            this.Controls.Add(this.txtDataLocation);
            this.Controls.Add(this.cmbServer);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Name = "ConfigStepCCRMData";
            this.SingleStep = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkDownLoadLateset;
        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.TextBox txtLoginName;
        private System.Windows.Forms.Button BtnBrowse;
        private System.Windows.Forms.TextBox txtDataLocation;
        private System.Windows.Forms.ComboBox cmbServer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;

    }
}
