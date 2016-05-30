namespace Webb.Reports.ReportWizard.DataSourceProvider
{
    partial class FrmCoachCRMWay
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BtnOK = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.RadioUseLast = new System.Windows.Forms.RadioButton();
            this.RadioDownLoad = new System.Windows.Forms.RadioButton();
            this.lblSelect = new System.Windows.Forms.Label();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnOK
            // 
            this.BtnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnOK.Location = new System.Drawing.Point(425, 311);
            this.BtnOK.Name = "BtnOK";
            this.BtnOK.Size = new System.Drawing.Size(99, 27);
            this.BtnOK.TabIndex = 3;
            this.BtnOK.Text = "OK";
            this.BtnOK.UseVisualStyleBackColor = true;
            this.BtnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::Webb.Reports.ExControls.Properties.Resources.bgcompress;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.RadioUseLast);
            this.panel1.Controls.Add(this.RadioDownLoad);
            this.panel1.Controls.Add(this.lblSelect);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(678, 305);
            this.panel1.TabIndex = 4;
            // 
            // RadioUseLast
            // 
            this.RadioUseLast.AutoSize = true;
            this.RadioUseLast.BackColor = System.Drawing.Color.Transparent;
            this.RadioUseLast.Location = new System.Drawing.Point(79, 188);
            this.RadioUseLast.Name = "RadioUseLast";
            this.RadioUseLast.Size = new System.Drawing.Size(418, 27);
            this.RadioUseLast.TabIndex = 4;
            this.RadioUseLast.Text = "Use data which have been downloaded before";
            this.RadioUseLast.UseVisualStyleBackColor = false;
            // 
            // RadioDownLoad
            // 
            this.RadioDownLoad.AutoSize = true;
            this.RadioDownLoad.BackColor = System.Drawing.Color.Transparent;
            this.RadioDownLoad.Checked = true;
            this.RadioDownLoad.Location = new System.Drawing.Point(78, 99);
            this.RadioDownLoad.Name = "RadioDownLoad";
            this.RadioDownLoad.Size = new System.Drawing.Size(555, 27);
            this.RadioDownLoad.TabIndex = 5;
            this.RadioDownLoad.TabStop = true;
            this.RadioDownLoad.Text = "DownLoad data from CCRM server,and then use them in WRW";
            this.RadioDownLoad.UseVisualStyleBackColor = false;
            // 
            // lblSelect
            // 
            this.lblSelect.AutoSize = true;
            this.lblSelect.BackColor = System.Drawing.Color.Transparent;
            this.lblSelect.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelect.Location = new System.Drawing.Point(12, 19);
            this.lblSelect.Name = "lblSelect";
            this.lblSelect.Size = new System.Drawing.Size(408, 24);
            this.lblSelect.TabIndex = 3;
            this.lblSelect.Text = "Please select the way to get CCRM data\r\n";
            // 
            // BtnCancel
            // 
            this.BtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCancel.Location = new System.Drawing.Point(573, 311);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(91, 28);
            this.BtnCancel.TabIndex = 5;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // FrmCoachCRMWay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(678, 344);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.BtnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FrmCoachCRMWay";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select the Way";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnOK;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton RadioUseLast;
        private System.Windows.Forms.RadioButton RadioDownLoad;
        private System.Windows.Forms.Label lblSelect;
        private System.Windows.Forms.Button BtnCancel;
    }
}