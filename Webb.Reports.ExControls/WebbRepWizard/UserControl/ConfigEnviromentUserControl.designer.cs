namespace WizardEnvironmentSetting
{
    partial class ConfigEnviromentUserControl
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
            this.lblInformation = new System.Windows.Forms.Label();
            this.lblStep = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblInformation
            // 
            this.lblInformation.BackColor = System.Drawing.Color.Transparent;
            this.lblInformation.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblInformation.Font = new System.Drawing.Font("Verdana", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInformation.Location = new System.Drawing.Point(0, 0);
            this.lblInformation.Name = "lblInformation";
            this.lblInformation.Size = new System.Drawing.Size(710, 59);
            this.lblInformation.TabIndex = 19;
            this.lblInformation.Text = "       Anwser questions below would make it easier and more effective to use WRW." +
                "";
            // 
            // lblStep
            // 
            this.lblStep.AutoSize = true;
            this.lblStep.BackColor = System.Drawing.Color.Transparent;
            this.lblStep.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStep.Location = new System.Drawing.Point(3, 63);
            this.lblStep.Name = "lblStep";
            this.lblStep.Size = new System.Drawing.Size(94, 23);
            this.lblStep.TabIndex = 18;
            this.lblStep.Text = "Step 2/2";
            // 
            // ConfigEnviromentUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.lblInformation);
            this.Controls.Add(this.lblStep);
            this.Name = "ConfigEnviromentUserControl";
            this.Size = new System.Drawing.Size(710, 394);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.Label lblInformation;
        protected System.Windows.Forms.Label lblStep;
    }
}
