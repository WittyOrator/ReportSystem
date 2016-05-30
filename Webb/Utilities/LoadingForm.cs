using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Webb.Utilities
{
	/// <summary>
	/// Summary description for LoadingForm.
	/// </summary>
	public class LoadingForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label C_TextMessage;
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.Button C_BtnCancel;
		private System.Windows.Forms.Label C_TextProcess;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public LoadingForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		public string MessageText
		{
			get{return this.C_TextMessage.Text;}
			set{this.C_TextMessage.Text = value;}
		}

		public string ProcessText
		{
			get{return this.C_TextProcess.Text;}
			set{this.C_TextProcess.Text = value;}
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoadingForm));
            this.C_TextMessage = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.C_BtnCancel = new System.Windows.Forms.Button();
            this.C_TextProcess = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // C_TextMessage
            // 
            this.C_TextMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.C_TextMessage.Location = new System.Drawing.Point(88, 54);
            this.C_TextMessage.Name = "C_TextMessage";
            this.C_TextMessage.Size = new System.Drawing.Size(392, 16);
            this.C_TextMessage.TabIndex = 0;
            this.C_TextMessage.Text = "Loading...";
            this.C_TextMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(32, 48);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 42);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pictureBox2.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(0, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(508, 32);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // C_BtnCancel
            // 
            this.C_BtnCancel.Location = new System.Drawing.Point(401, 99);
            this.C_BtnCancel.Name = "C_BtnCancel";
            this.C_BtnCancel.Size = new System.Drawing.Size(75, 23);
            this.C_BtnCancel.TabIndex = 3;
            this.C_BtnCancel.Text = "Cancel";
            this.C_BtnCancel.Click += new System.EventHandler(this.C_BtnCancel_Click);
            // 
            // C_TextProcess
            // 
            this.C_TextProcess.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.C_TextProcess.Location = new System.Drawing.Point(88, 78);
            this.C_TextProcess.Name = "C_TextProcess";
            this.C_TextProcess.Size = new System.Drawing.Size(392, 16);
            this.C_TextProcess.TabIndex = 4;
            this.C_TextProcess.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LoadingForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(508, 136);
            this.ControlBox = false;
            this.Controls.Add(this.C_TextProcess);
            this.Controls.Add(this.C_BtnCancel);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.C_TextMessage);
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "LoadingForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Webb Electronics, LTD";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void C_BtnCancel_Click(object sender, System.EventArgs e)
		{
			 Cancel();
		}
		protected virtual void Cancel()
		{
			if(MessageBoxEx.ShowQuestion_YesNo("Do you want to exit this process?")==System.Windows.Forms.DialogResult.Yes)
			{
				Webb.Utility.CancelPrint = true;
				Environment.Exit(-1);
			}
		}
	}

    public class NoCancelLoadingForm : LoadingForm
    {
        public NoCancelLoadingForm()
            : base()
        {
        }
        protected override void Cancel()
        {
            if (MessageBoxEx.ShowQuestion_YesNo("Do you want to stop this process?") == System.Windows.Forms.DialogResult.Yes)
            {
                Webb.Utility.CancelPrint = true;
                this.Close();
            }
        }
    }
}
