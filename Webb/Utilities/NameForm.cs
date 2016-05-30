using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Webb.Utilities
{
	/// <summary>
	/// Summary description for NameForm.
	/// </summary>
	public class NameForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox C_textName;
		private System.Windows.Forms.Button C_btnOK;
		private System.Windows.Forms.Button C_btnCancel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public NameForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			this.C_textName.Text = string.Empty;
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
			this.C_textName = new System.Windows.Forms.TextBox();
			this.C_btnOK = new System.Windows.Forms.Button();
			this.C_btnCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// C_textName
			// 
			this.C_textName.Location = new System.Drawing.Point(38, 17);
			this.C_textName.Name = "C_textName";
			this.C_textName.Size = new System.Drawing.Size(192, 21);
			this.C_textName.TabIndex = 0;
			this.C_textName.Text = "";
			this.C_textName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.C_textName_KeyDown);
			// 
			// C_btnOK
			// 
			this.C_btnOK.Location = new System.Drawing.Point(38, 60);
			this.C_btnOK.Name = "C_btnOK";
			this.C_btnOK.Size = new System.Drawing.Size(90, 25);
			this.C_btnOK.TabIndex = 1;
			this.C_btnOK.Text = "OK";
			this.C_btnOK.Click += new System.EventHandler(this.C_btnOK_Click);
			// 
			// C_btnCancel
			// 
			this.C_btnCancel.Location = new System.Drawing.Point(134, 60);
			this.C_btnCancel.Name = "C_btnCancel";
			this.C_btnCancel.Size = new System.Drawing.Size(90, 25);
			this.C_btnCancel.TabIndex = 2;
			this.C_btnCancel.Text = "Cancel";
			this.C_btnCancel.Click += new System.EventHandler(this.C_btnCancel_Click);
			// 
			// NameForm
			// 
			
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(259, 101);
			this.ControlBox = false;
			this.Controls.Add(this.C_btnCancel);
			this.Controls.Add(this.C_btnOK);
			this.Controls.Add(this.C_textName);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "NameForm";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Name";
			this.TopMost = true;
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NameForm_KeyDown);		
			this.VisibleChanged += new System.EventHandler(this.NameForm_VisibleChanged);
			this.ResumeLayout(false);

		}
		#endregion

		private void C_btnOK_Click(object sender, System.EventArgs e)
		{
			if(this.FileName == string.Empty)
			{
				MessageBox.Show(this,"Text couldn't be empty.");

				this.C_textName.Focus();

				return;
			}

			this.DialogResult = DialogResult.OK;
			
			this.Close();
		}

		private void C_btnCancel_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			
			this.Close();
		}

		
		private void NameForm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.C_textName_KeyDown(sender,e);
		}

		private void C_textName_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			switch(e.KeyData)
			{
				case Keys.Enter:
				{
					this.C_btnOK_Click(null,null);
					break;
				}
				case Keys.Escape:
				{
					this.C_btnCancel_Click(null,null);
					break;
				}
			}
		}

		private void NameForm_VisibleChanged(object sender, System.EventArgs e)
		{
			this.C_textName.Focus();
		}

		public string FileName
		{
			get{return this.C_textName.Text.Trim();}
			set{this.C_textName.Text = value.Trim();}
		}

		public string Title
		{
			get{return this.Text;}
			set{this.Text = value;}
		}
	}
}
