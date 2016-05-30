using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text;

namespace Webb.Reports.Browser
{
	/// <summary>
	/// Summary description for MessageStatus.
	/// </summary>
	public class MessageStatus : System.Windows.Forms.Form
	{
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Timer timer1;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Label lblMessage;
		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.PictureBox pictureBox2;

	    private DateTime startTime;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PictureBox pictureBox3;
		private System.Windows.Forms.Label label2;

		private int ShowSeconds=5;

		public MessageStatus(ArrayList messages)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			startTime=DateTime.Now;

			this.SetMessage(messages);

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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MessageStatus));
			this.lblMessage = new System.Windows.Forms.Label();
			this.lblStatus = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.button1 = new System.Windows.Forms.Button();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.pictureBox3 = new System.Windows.Forms.PictureBox();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lblMessage
			// 
			this.lblMessage.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblMessage.Location = new System.Drawing.Point(16, 88);
			this.lblMessage.Name = "lblMessage";
			this.lblMessage.Size = new System.Drawing.Size(400, 40);
			this.lblMessage.TabIndex = 0;
			this.lblMessage.Text = "The following reports do not have the proper report key for this application and " +
				"are not available for printing:";
			// 
			// lblStatus
			// 
			this.lblStatus.Location = new System.Drawing.Point(24, 136);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(328, 64);
			this.lblStatus.TabIndex = 1;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(376, 144);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(40, 39);
			this.pictureBox1.TabIndex = 3;
			this.pictureBox1.TabStop = false;
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Interval = 1000;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(152, 208);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(128, 32);
			this.button1.TabIndex = 4;
			this.button1.Text = "OK";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// pictureBox2
			// 
			this.pictureBox2.BackColor = System.Drawing.Color.WhiteSmoke;
			this.pictureBox2.Cursor = System.Windows.Forms.Cursors.WaitCursor;
			this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Top;
			this.pictureBox2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
			this.pictureBox2.Location = new System.Drawing.Point(0, 0);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(554, 32);
			this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox2.TabIndex = 5;
			this.pictureBox2.TabStop = false;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(24, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(392, 16);
			this.label1.TabIndex = 6;
			this.label1.Text = "This Window would be closed in 5 secondes!";
			this.label1.Click += new System.EventHandler(this.label1_Click);
			// 
			// pictureBox3
			// 
			this.pictureBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pictureBox3.Location = new System.Drawing.Point(0, 72);
			this.pictureBox3.Name = "pictureBox3";
			this.pictureBox3.Size = new System.Drawing.Size(416, 1);
			this.pictureBox3.TabIndex = 8;
			this.pictureBox3.TabStop = false;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(88, 56);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(192, 16);
			this.label2.TabIndex = 9;
			this.label2.Text = "You also can click \"Ok\" to cancel it";
			// 
			// MessageStatus
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(426, 248);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.pictureBox3);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.pictureBox2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.lblStatus);
			this.Controls.Add(this.lblMessage);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "MessageStatus";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Printing";
			this.TopMost = true;
			this.ResumeLayout(false);

		}
		#endregion

		private void SetMessage(ArrayList messages)
		{
			
			StringBuilder sb=new StringBuilder();			

			if(messages.Count>0)
			{		
				foreach(string filename in  messages)
				{
					sb.Append(filename+",");
				}	           
				this.lblStatus.Text=sb.ToString().Trim(" ,".ToCharArray());							
			}
			else
			{
				this.lblMessage.Text="all reports are available for printing:";
                this.lblStatus.Text=string.Empty;
				ShowSeconds=2;
			}

		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			DialogResult=DialogResult.OK;
		}

		private void timer1_Tick(object sender, System.EventArgs e)
		{
			TimeSpan timespan=DateTime.Now-startTime;
			this.label1.Text=string.Format("This Window would be closed in {0} secondes!",this.ShowSeconds-(int)timespan.TotalSeconds);
			if(timespan.TotalSeconds>=this.ShowSeconds)
			{
				DialogResult=DialogResult.OK;
			}
		}

		private void label1_Click(object sender, System.EventArgs e)
		{
		
		}
	}
}
