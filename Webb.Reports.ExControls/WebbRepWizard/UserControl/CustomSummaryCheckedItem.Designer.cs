namespace Webb.Reports.ReportWizard.WizardInfo
{
    partial class CustomSummaryCheckedItem
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
            this.palComboBox = new System.Windows.Forms.Panel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.lblPositions = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.chkEdItem = new System.Windows.Forms.CheckBox();
            this.palComboBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // palComboBox
            // 
            this.palComboBox.Controls.Add(this.comboBox1);
            this.palComboBox.Controls.Add(this.lblPositions);
            this.palComboBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.palComboBox.Location = new System.Drawing.Point(309, 0);
            this.palComboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.palComboBox.Name = "palComboBox";
            this.palComboBox.Size = new System.Drawing.Size(103, 22);
            this.palComboBox.TabIndex = 0;
            // 
            // comboBox1
            // 
            this.comboBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15"});
            this.comboBox1.Location = new System.Drawing.Point(51, 0);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(52, 24);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            this.comboBox1.Click += new System.EventHandler(this.comboBox1_Click);
            // 
            // lblPositions
            // 
            this.lblPositions.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblPositions.Location = new System.Drawing.Point(0, 0);
            this.lblPositions.Name = "lblPositions";
            this.lblPositions.Size = new System.Drawing.Size(51, 22);
            this.lblPositions.TabIndex = 1;
            this.lblPositions.Text = "Order:";
            this.lblPositions.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblPositions.Click += new System.EventHandler(this.lblPositions_Click);
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(307, 0);
            this.splitter1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(2, 22);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // chkEdItem
            // 
            this.chkEdItem.AutoSize = true;
            this.chkEdItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkEdItem.Location = new System.Drawing.Point(0, 0);
            this.chkEdItem.Name = "chkEdItem";
            this.chkEdItem.Size = new System.Drawing.Size(307, 22);
            this.chkEdItem.TabIndex = 2;
            this.chkEdItem.UseVisualStyleBackColor = true;
            this.chkEdItem.Click += new System.EventHandler(this.chkEdItem_Click);
            this.chkEdItem.CheckedChanged += new System.EventHandler(this.chkEdItem_CheckedChanged);
            // 
            // CustomSummaryCheckedItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.chkEdItem);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.palComboBox);
            this.Font = new System.Drawing.Font("Arial", 9.75F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "CustomSummaryCheckedItem";
            this.Size = new System.Drawing.Size(412, 22);
            this.palComboBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel palComboBox;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label lblPositions;
        private System.Windows.Forms.CheckBox chkEdItem;
    }
}
