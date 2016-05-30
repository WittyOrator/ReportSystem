namespace Webb.Reports.Editors
{
    partial class SectionGroupFilterEditr
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
            this.palOffence = new System.Windows.Forms.Panel();
            this.lblOffence = new System.Windows.Forms.Label();
            this.lbldefence = new System.Windows.Forms.Label();
            this.BtnCustomFilters = new System.Windows.Forms.Button();
            this.BtnSelect = new System.Windows.Forms.Button();
            this.radioOther = new System.Windows.Forms.RadioButton();
            this.radioCustom = new System.Windows.Forms.RadioButton();
            this.radioField = new System.Windows.Forms.RadioButton();
            this.radioDownandDist = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.lblInformation = new System.Windows.Forms.Label();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.palOffence.SuspendLayout();
            this.SuspendLayout();
            // 
            // palOffence
            // 
            this.palOffence.BackColor = System.Drawing.Color.Transparent;
            this.palOffence.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.palOffence.Controls.Add(this.lblOffence);
            this.palOffence.Controls.Add(this.lbldefence);
            this.palOffence.Location = new System.Drawing.Point(260, 83);
            this.palOffence.Name = "palOffence";
            this.palOffence.Size = new System.Drawing.Size(124, 72);
            this.palOffence.TabIndex = 17;
            // 
            // lblOffence
            // 
            this.lblOffence.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOffence.ForeColor = System.Drawing.Color.Black;
            this.lblOffence.Location = new System.Drawing.Point(8, 8);
            this.lblOffence.Name = "lblOffence";
            this.lblOffence.Size = new System.Drawing.Size(92, 24);
            this.lblOffence.TabIndex = 1;
            this.lblOffence.Text = "Offense";
            // 
            // lbldefence
            // 
            this.lbldefence.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbldefence.ForeColor = System.Drawing.Color.Black;
            this.lbldefence.Location = new System.Drawing.Point(8, 40);
            this.lbldefence.Name = "lbldefence";
            this.lbldefence.Size = new System.Drawing.Size(92, 24);
            this.lbldefence.TabIndex = 0;
            this.lbldefence.Text = "Defense";
            // 
            // BtnCustomFilters
            // 
            this.BtnCustomFilters.Location = new System.Drawing.Point(260, 222);
            this.BtnCustomFilters.Name = "BtnCustomFilters";
            this.BtnCustomFilters.Size = new System.Drawing.Size(103, 24);
            this.BtnCustomFilters.TabIndex = 18;
            this.BtnCustomFilters.Text = "Select ";
            // 
            // BtnSelect
            // 
            this.BtnSelect.Location = new System.Drawing.Point(257, 297);
            this.BtnSelect.Name = "BtnSelect";
            this.BtnSelect.Size = new System.Drawing.Size(104, 24);
            this.BtnSelect.TabIndex = 19;
            this.BtnSelect.Text = "Select";
            // 
            // radioOther
            // 
            this.radioOther.BackColor = System.Drawing.Color.Transparent;
            this.radioOther.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioOther.ForeColor = System.Drawing.Color.Black;
            this.radioOther.Location = new System.Drawing.Point(22, 289);
            this.radioOther.Name = "radioOther";
            this.radioOther.Size = new System.Drawing.Size(216, 32);
            this.radioOther.TabIndex = 16;
            this.radioOther.Text = "From other source";
            this.radioOther.UseVisualStyleBackColor = false;
            // 
            // radioCustom
            // 
            this.radioCustom.BackColor = System.Drawing.Color.Transparent;
            this.radioCustom.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioCustom.ForeColor = System.Drawing.Color.Black;
            this.radioCustom.Location = new System.Drawing.Point(20, 222);
            this.radioCustom.Name = "radioCustom";
            this.radioCustom.Size = new System.Drawing.Size(232, 28);
            this.radioCustom.TabIndex = 15;
            this.radioCustom.Text = "Select custom filters";
            this.radioCustom.UseVisualStyleBackColor = false;
            // 
            // radioField
            // 
            this.radioField.BackColor = System.Drawing.Color.Transparent;
            this.radioField.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioField.ForeColor = System.Drawing.Color.Black;
            this.radioField.Location = new System.Drawing.Point(20, 138);
            this.radioField.Name = "radioField";
            this.radioField.Size = new System.Drawing.Size(184, 32);
            this.radioField.TabIndex = 14;
            this.radioField.Text = "Field position";
            this.radioField.UseVisualStyleBackColor = false;
            // 
            // radioDownandDist
            // 
            this.radioDownandDist.BackColor = System.Drawing.Color.Transparent;
            this.radioDownandDist.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioDownandDist.ForeColor = System.Drawing.Color.Black;
            this.radioDownandDist.Location = new System.Drawing.Point(22, 84);
            this.radioDownandDist.Name = "radioDownandDist";
            this.radioDownandDist.Size = new System.Drawing.Size(232, 24);
            this.radioDownandDist.TabIndex = 13;
            this.radioDownandDist.Text = "Down and distance";
            this.radioDownandDist.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(8, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(718, 35);
            this.label1.TabIndex = 12;
            this.label1.Text = "Would you like to seprate data using filters?";
            // 
            // lblInformation
            // 
            this.lblInformation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblInformation.BackColor = System.Drawing.Color.Transparent;
            this.lblInformation.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInformation.Location = new System.Drawing.Point(18, 361);
            this.lblInformation.Name = "lblInformation";
            this.lblInformation.Size = new System.Drawing.Size(396, 62);
            this.lblInformation.TabIndex = 20;
            this.lblInformation.Text = "Section filters separate the rows of data on the page based on the filter used fo" +
                "r each section.";
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(463, 70);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(282, 319);
            this.treeView1.TabIndex = 21;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(463, 429);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 22;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(628, 429);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 22;
            this.button2.Text = "button1";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // SectionGroupFilterEditr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 473);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.lblInformation);
            this.Controls.Add(this.palOffence);
            this.Controls.Add(this.BtnCustomFilters);
            this.Controls.Add(this.BtnSelect);
            this.Controls.Add(this.radioOther);
            this.Controls.Add(this.radioCustom);
            this.Controls.Add(this.radioField);
            this.Controls.Add(this.radioDownandDist);
            this.Controls.Add(this.label1);
            this.Name = "SectionGroupFilterEditr";
            this.Text = "SectionGroupFilterEditr";
            this.palOffence.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel palOffence;
        private System.Windows.Forms.Label lblOffence;
        private System.Windows.Forms.Label lbldefence;
        private System.Windows.Forms.Button BtnCustomFilters;
        private System.Windows.Forms.Button BtnSelect;
        private System.Windows.Forms.RadioButton radioOther;
        private System.Windows.Forms.RadioButton radioCustom;
        private System.Windows.Forms.RadioButton radioField;
        private System.Windows.Forms.RadioButton radioDownandDist;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblInformation;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}