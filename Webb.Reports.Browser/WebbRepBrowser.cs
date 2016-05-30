/***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:WebbRepBrowser.cs
 * Author:Country.Wu [EMail:Webb.Country.Wu@163.com]
 * Create Time:11/26/2007 12:39:48 PM
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
using System.Data;

using Webb.Reports.DataProvider;

namespace Webb.Reports.Browser
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class WebbRepBrowser : Webb.Reports.Browser.ReportBrowserBase
	{
		private System.Windows.Forms.Label LblInfo; 
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public WebbRepBrowser()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			this.Load += new EventHandler(WebbRepBrowser_Load);
			this.HandleCreated +=new EventHandler(WebbRepBrowser_HandleCreated);
			this.LblInfo.Visible=false;
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
				if (components != null) 
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
			this.LblInfo = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// LblInfo
			// 
			this.LblInfo.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.LblInfo.ForeColor = System.Drawing.Color.Red;
			this.LblInfo.Location = new System.Drawing.Point(120, 176);
			this.LblInfo.Name = "LblInfo";
			this.LblInfo.Size = new System.Drawing.Size(520, 144);
			this.LblInfo.TabIndex = 9;
			this.LblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// WebbRepBrowser
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(696, 437);
			this.Controls.Add(this.LblInfo);
			this.Name = "WebbRepBrowser";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Webb Report Preview";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Controls.SetChildIndex(this.LblInfo, 0);
			this.ResumeLayout(false);

		}
		#endregion

		public void InvertZorder()
		{
			this.LblInfo.Visible=true;
			this.C_PrintControl.Visible=false;
			this.LblInfo.BringToFront();
		     LblInfo.Text="This report '"+this.ReportName+"' is not designed for your Webb application.!!!";
             LblInfo.BackColor=Color.White;			
			 LblInfo.Dock=DockStyle.Fill;
		}

		protected override void WndProc(ref Message m)
		{
			switch(m.Msg)
			{				
				case ProcessInfo.User_Message:
					ProcessInfo.GetMessage();
					ProcessInfo.ResetShareInputFile();
#if DEBUG
					MessageBox.Show("Receive Message!");
#endif
					break;
			}
			base.WndProc (ref m);
		}

		private void WebbRepBrowser_HandleCreated(object sender, EventArgs e)
		{
			//ProcessInfo.SetMemoryMessage(ProcessInfo.User_Message,this.Handle.ToInt32());
		}

		private void WebbRepBrowser_Load(object sender, EventArgs e)
		{
			ProcessInfo.SetMemoryMessage(ProcessInfo.User_Message,this.Handle.ToInt32());
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown (e);
		}

	}
}
