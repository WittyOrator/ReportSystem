using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Webb.Utilities
{
	/// <summary>
	/// Summary description for TopMostMessageBox.
	/// </summary>
	public class TopMostMessageBox : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label C_LabelMessage;
		private System.Windows.Forms.Button C_BtnOK;
		private System.Windows.Forms.Button C_BtnYes;
		private System.Windows.Forms.Button C_BtnNo;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public TopMostMessageBox()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.C_LabelMessage = new System.Windows.Forms.Label();
            this.C_BtnOK = new System.Windows.Forms.Button();
            this.C_BtnYes = new System.Windows.Forms.Button();
            this.C_BtnNo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // C_LabelMessage
            // 
            this.C_LabelMessage.Dock = System.Windows.Forms.DockStyle.Top;
            this.C_LabelMessage.Location = new System.Drawing.Point(0, 0);
            this.C_LabelMessage.Name = "C_LabelMessage";
            this.C_LabelMessage.Size = new System.Drawing.Size(307, 50);
            this.C_LabelMessage.TabIndex = 0;
            this.C_LabelMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // C_BtnOK
            // 
            this.C_BtnOK.Location = new System.Drawing.Point(88, 56);
            this.C_BtnOK.Name = "C_BtnOK";
            this.C_BtnOK.Size = new System.Drawing.Size(75, 23);
            this.C_BtnOK.TabIndex = 1;
            this.C_BtnOK.Text = "OK";
            this.C_BtnOK.Click += new System.EventHandler(this.C_BtnOK_Click);
            // 
            // C_BtnYes
            // 
            this.C_BtnYes.Location = new System.Drawing.Point(53, 56);
            this.C_BtnYes.Name = "C_BtnYes";
            this.C_BtnYes.Size = new System.Drawing.Size(63, 21);
            this.C_BtnYes.TabIndex = 2;
            this.C_BtnYes.Text = "Yes";
            this.C_BtnYes.Click += new System.EventHandler(this.C_BtnYes_Click);
            // 
            // C_BtnNo
            // 
            this.C_BtnNo.Location = new System.Drawing.Point(147, 56);
            this.C_BtnNo.Name = "C_BtnNo";
            this.C_BtnNo.Size = new System.Drawing.Size(62, 21);
            this.C_BtnNo.TabIndex = 3;
            this.C_BtnNo.Text = "No";
            this.C_BtnNo.Click += new System.EventHandler(this.C_BtnNo_Click);
            // 
            // TopMostMessageBox
            // 
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(307, 101);
            this.ControlBox = false;
            this.Controls.Add(this.C_BtnNo);
            this.Controls.Add(this.C_BtnYes);
            this.Controls.Add(this.C_BtnOK);
            this.Controls.Add(this.C_LabelMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "TopMostMessageBox";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Prompt";
            this.TopMost = true;
            this.ResumeLayout(false);

		}
		#endregion

		private void AjustSize(string strMsg)
		{
			SizeF size = this.CreateGraphics().MeasureString(strMsg,this.C_LabelMessage.Font);
		
			int measureSize = (int)(size.Width + 10);

			this.Width = Math.Max(200,measureSize);

			this.C_BtnOK.Left = (this.Width - this.C_BtnOK.Width)/2;

			int nSpace = this.C_BtnNo.Left - this.C_BtnYes.Right;

			this.C_BtnYes.Left = (this.Width - (this.C_BtnNo.Right - this.C_BtnYes.Left))/2;

			this.C_BtnNo.Left = this.C_BtnYes.Right + nSpace;
		}

		private DialogResult ShowMsg(string strTitle,string strMsg)
		{
			this.Text = strTitle;
			this.C_LabelMessage.Text = strMsg;
			this.AjustSize(strMsg);
			return this.ShowDialog();
		}

		private DialogResult ShowMsg(string strMsg)
		{
			this.C_LabelMessage.Text = strMsg;
			this.AjustSize(strMsg);
			return this.ShowDialog();
		}

		private MessageBoxButtons MsgBtns
		{
			set
			{
				this.C_BtnYes.Visible = false;
				this.C_BtnNo.Visible = false;
				this.C_BtnOK.Visible = false;

				switch(value)
				{
					case MessageBoxButtons.OK:
						this.C_BtnOK.Visible = true;
						break;
					case MessageBoxButtons.YesNo:
						this.C_BtnYes.Visible = true;
						this.C_BtnNo.Visible = true;
						break;;
				}
			}
		}

		static private TopMostMessageBox _TMMessageBox;

		private void C_BtnYes_Click(object sender, System.EventArgs e)
		{
			TMMessageBox.DialogResult = DialogResult.Yes;
			TMMessageBox.Close();
		}

		private void C_BtnNo_Click(object sender, System.EventArgs e)
		{
			TMMessageBox.DialogResult = DialogResult.No;
			TMMessageBox.Close();
		}

		private void C_BtnOK_Click(object sender, System.EventArgs e)
		{
			TMMessageBox.DialogResult = DialogResult.OK;
			TMMessageBox.Close();
		}

		static public DialogResult ShowMessage(string strTitle,string strMsg,MessageBoxButtons msgBtns)
		{
			TMMessageBox.MsgBtns = msgBtns;
			return TMMessageBox.ShowMsg(strTitle,strMsg);
		}

		static public DialogResult ShowMessage(string strMsg,MessageBoxButtons msgBtns)
		{
			TMMessageBox.MsgBtns = msgBtns;
			return TMMessageBox.ShowMsg(strMsg);
		}

		static private TopMostMessageBox TMMessageBox
		{
			get
			{
				if(_TMMessageBox == null) _TMMessageBox = new TopMostMessageBox();

				return _TMMessageBox;
			}
		}
	}
}
