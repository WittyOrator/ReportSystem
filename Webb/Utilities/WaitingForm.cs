/***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:WaitingForm.cs
 * Author:Country.Wu [EMail:Webb.Country.Wu@163.com]
 * Create Time:11/14/2007 04:16:13 PM
 * Copyright:1986-2007@Webb Electronics all right reserved.
 * Purpose:
 * ***********************************************************************/
#region History
/*
 * //Author@DateTime : Description
 * */
#endregion History

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
//
using System.Threading;

namespace Webb.Utilities
{
	/// <summary>
	/// Summary description for WaitingForm.
	/// </summary>
	public sealed class WaitingForm : System.Windows.Forms.Form
	{
		private static bool M_IsWaiting = false;
		private static WaitingForm M_WaitingForm;
		private System.Windows.Forms.Label C_Msg;
		private System.Windows.Forms.PictureBox pictureBox1;
		private static Cursor M_OldCursor;
		//
		static public void ShowWaitingForm()
		{
			M_OldCursor = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;
			if(M_WaitingForm==null) M_WaitingForm = new WaitingForm();
			M_IsWaiting = true;
			M_WaitingForm.Show();		
			M_WaitingForm.Refresh();
		}

		static public void SetWaitingMessage(string i_Message)
		{
			if(M_WaitingForm==null) return;
			M_WaitingForm.C_Msg.Text = i_Message;
			M_WaitingForm.Refresh();
		}

		static public void CloseWaitingForm()
		{   
			Cursor.Current = M_OldCursor;
			M_IsWaiting = false;
			if(M_WaitingForm==null) return;
			M_WaitingForm.Close();
			M_WaitingForm=null;			
		}

		static private void UIUpdate()
		{
			if(M_WaitingForm==null)return;
			while(M_IsWaiting)
			{			
				if(M_WaitingForm==null) return;
				M_WaitingForm.Update();
				Thread.Sleep(10);
			}
		}
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public WaitingForm()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(WaitingForm));
			this.C_Msg = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.SuspendLayout();
			// 
			// C_Msg
			// 
			this.C_Msg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.C_Msg.Dock = System.Windows.Forms.DockStyle.Fill;
			this.C_Msg.Location = new System.Drawing.Point(0, 32);
			this.C_Msg.Name = "C_Msg";
			this.C_Msg.Size = new System.Drawing.Size(418, 71);
			this.C_Msg.TabIndex = 2;
			this.C_Msg.Text = "Loading data, please wait......";
			this.C_Msg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackColor = System.Drawing.Color.WhiteSmoke;
			this.pictureBox1.Cursor = System.Windows.Forms.Cursors.WaitCursor;
			this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.pictureBox1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(554, 32);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 1;
			this.pictureBox1.TabStop = false;
			// 
			// WaitingForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(7, 15);
			this.BackColor = System.Drawing.Color.WhiteSmoke;
			this.ClientSize = new System.Drawing.Size(418, 103);
			this.ControlBox = false;
			this.Controls.Add(this.C_Msg);
			this.Controls.Add(this.pictureBox1);
			this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
			this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "WaitingForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Waiting...";
			this.TopMost = true;
			this.ResumeLayout(false);

		}
		#endregion

		protected override void WndProc(ref Message m)
		{
			base.WndProc (ref m);
		}
	}
}
