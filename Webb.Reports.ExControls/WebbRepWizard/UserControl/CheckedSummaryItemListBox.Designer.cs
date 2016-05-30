namespace Webb.Reports.ReportWizard.WizardInfo
{
    partial class CheckedSummaryItemListBox
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
            this.palList = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // palList
            // 
            this.palList.AutoScroll = true;
            this.palList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.palList.Location = new System.Drawing.Point(0, 0);
            this.palList.Name = "palList";
            this.palList.Size = new System.Drawing.Size(459, 209);
            this.palList.TabIndex = 0;
            // 
            // CheckedSummaryItemListBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.palList);
            this.Name = "CheckedSummaryItemListBox";
            this.Size = new System.Drawing.Size(459, 209);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel palList;
    }
}
