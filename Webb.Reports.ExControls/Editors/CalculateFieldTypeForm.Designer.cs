namespace Webb.Reports.ExControls.Editors
{
    partial class CalculateFieldTypeForm
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
            this.lblInformation = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblFormula = new System.Windows.Forms.Label();
            this.BtnEditCalcInfo = new System.Windows.Forms.Button();
            this.cmbField = new System.Windows.Forms.ComboBox();
            this.radioCaluateColumnInfo = new System.Windows.Forms.RadioButton();
            this.radioString = new System.Windows.Forms.RadioButton();
            this.BtnOK = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblInformation
            // 
            this.lblInformation.AutoSize = true;
            this.lblInformation.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInformation.Location = new System.Drawing.Point(28, 9);
            this.lblInformation.Name = "lblInformation";
            this.lblInformation.Size = new System.Drawing.Size(346, 24);
            this.lblInformation.TabIndex = 0;
            this.lblInformation.Text = "Choose a field type first,then edit";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblFormula);
            this.panel1.Controls.Add(this.BtnEditCalcInfo);
            this.panel1.Controls.Add(this.cmbField);
            this.panel1.Controls.Add(this.radioCaluateColumnInfo);
            this.panel1.Controls.Add(this.radioString);
            this.panel1.Controls.Add(this.lblInformation);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(622, 224);
            this.panel1.TabIndex = 1;
            // 
            // lblFormula
            // 
            this.lblFormula.BackColor = System.Drawing.Color.Transparent;
            this.lblFormula.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormula.ForeColor = System.Drawing.Color.Red;
            this.lblFormula.Location = new System.Drawing.Point(58, 170);
            this.lblFormula.Name = "lblFormula";
            this.lblFormula.Size = new System.Drawing.Size(552, 51);
            this.lblFormula.TabIndex = 5;
            // 
            // BtnEditCalcInfo
            // 
            this.BtnEditCalcInfo.Location = new System.Drawing.Point(413, 129);
            this.BtnEditCalcInfo.Name = "BtnEditCalcInfo";
            this.BtnEditCalcInfo.Size = new System.Drawing.Size(196, 33);
            this.BtnEditCalcInfo.TabIndex = 4;
            this.BtnEditCalcInfo.Text = "View and edit";
            this.BtnEditCalcInfo.UseVisualStyleBackColor = true;
            this.BtnEditCalcInfo.Click += new System.EventHandler(this.BtnEditCalcInfo_Click);
            // 
            // cmbField
            // 
            this.cmbField.FormattingEnabled = true;
            this.cmbField.Location = new System.Drawing.Point(183, 62);
            this.cmbField.Name = "cmbField";
            this.cmbField.Size = new System.Drawing.Size(426, 27);
            this.cmbField.TabIndex = 3;
            // 
            // radioCaluateColumnInfo
            // 
            this.radioCaluateColumnInfo.AutoSize = true;
            this.radioCaluateColumnInfo.Location = new System.Drawing.Point(45, 134);
            this.radioCaluateColumnInfo.Name = "radioCaluateColumnInfo";
            this.radioCaluateColumnInfo.Size = new System.Drawing.Size(269, 23);
            this.radioCaluateColumnInfo.TabIndex = 2;
            this.radioCaluateColumnInfo.TabStop = true;
            this.radioCaluateColumnInfo.Text = "Field is calcuated from other fields";
            this.radioCaluateColumnInfo.UseVisualStyleBackColor = true;
            this.radioCaluateColumnInfo.CheckedChanged += new System.EventHandler(this.radio_CheckedChanged);
            // 
            // radioString
            // 
            this.radioString.AutoSize = true;
            this.radioString.Location = new System.Drawing.Point(45, 66);
            this.radioString.Name = "radioString";
            this.radioString.Size = new System.Drawing.Size(113, 23);
            this.radioString.TabIndex = 2;
            this.radioString.TabStop = true;
            this.radioString.Text = "Field in data";
            this.radioString.UseVisualStyleBackColor = true;
            this.radioString.CheckedChanged += new System.EventHandler(this.radio_CheckedChanged);
            // 
            // BtnOK
            // 
            this.BtnOK.Location = new System.Drawing.Point(333, 226);
            this.BtnOK.Name = "BtnOK";
            this.BtnOK.Size = new System.Drawing.Size(104, 33);
            this.BtnOK.TabIndex = 2;
            this.BtnOK.Text = "OK";
            this.BtnOK.UseVisualStyleBackColor = true;
            this.BtnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Location = new System.Drawing.Point(491, 226);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(119, 35);
            this.BtnCancel.TabIndex = 3;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // CalculateFieldTypeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 261);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnOK);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "CalculateFieldTypeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CalculateFieldTypeForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblInformation;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BtnEditCalcInfo;
        private System.Windows.Forms.ComboBox cmbField;
        private System.Windows.Forms.RadioButton radioCaluateColumnInfo;
        private System.Windows.Forms.RadioButton radioString;
        private System.Windows.Forms.Button BtnOK;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.Label lblFormula;
    }
}