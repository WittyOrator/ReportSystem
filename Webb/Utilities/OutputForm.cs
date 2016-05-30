/***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:FormTrace.cs
 * Author:Country.Wu [EMail:Webb.Country.Wu@163.com]
 * Create Time:10/30/2007 10:49:28 AM
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
using System.Diagnostics;

namespace Webb.Utilities
{
	/// <summary>
	/// Summary description for WinDebuger.
	/// </summary>		
	public class OutputForm : System.Windows.Forms.Form
	{
		private static bool _autoShow = true;
		private static bool _autoAddTime = true;
		private static readonly OutputForm _debugForm = new OutputForm();
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.RichTextBox _outTextBox;
		private System.Windows.Forms.MenuItem menuItem3;

		/// <summary>
		/// 
		/// </summary>
		[Conditional("DEBUG")]	
		public static void SetAutoAddTime(bool i_isAuto)
		{
			_autoAddTime = i_isAuto;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="i_isAuto"></param>
		[Conditional("DEBUG")]	
		public static void SetAutoShow(bool i_isAuto)
		{
			_autoShow = i_isAuto;
		}

		/// <summary>
		/// 
		/// </summary>
		[Conditional("DEBUG")]
		public static void ShowDebuger()
		{
			_debugForm.Show();
		}

		/// <summary>
		/// 
		/// </summary>
		[Conditional("DEBUG")]
		public static void HideDebuger()
		{
			_debugForm.Hide();
		}

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		//[Conditional("DEBUG")]
		static OutputForm()
		{
		}

		//[Conditional("DEBUG")]
		private OutputForm()
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
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this._outTextBox = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.menuItem1,
																						 this.menuItem2,
																						 this.menuItem3});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.Text = "Clean All";
			this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.Text = "Select All";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 2;
			this.menuItem3.Text = "Copy";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// _outTextBox
			// 
			this._outTextBox.ContextMenu = this.contextMenu1;
			this._outTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this._outTextBox.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._outTextBox.Location = new System.Drawing.Point(0, 0);
			this._outTextBox.Name = "_outTextBox";
			this._outTextBox.ReadOnly = true;
			this._outTextBox.Size = new System.Drawing.Size(648, 181);
			this._outTextBox.TabIndex = 0;
			this._outTextBox.Text = "";
			this._outTextBox.WordWrap = false;
			// 
			// OutputForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
			this.ClientSize = new System.Drawing.Size(648, 181);
			this.Controls.Add(this._outTextBox);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "OutputForm";
			this.Text = "Output";
			this.TopMost = true;
			this.Closing += new System.ComponentModel.CancelEventHandler(this.WinDebuger_Closing);
			this.ResumeLayout(false);

		}
		#endregion

		#region IDebuger Members
		[Conditional("DEBUG")]
		static public void WriteLine(string i_message)
		{
			// TODO:  Add WinDebuger.WriteLine implementation
			if(_autoAddTime)
			{
				_debugForm._outTextBox.AppendText(string.Format("{0:hh:mm:ss}  {1}\r\n",DateTime.Now,i_message));
			}
			else
			{
				_debugForm._outTextBox.AppendText(string.Format("{0}\r\n",i_message));
			}
			_debugForm.Show();
		}
		[Conditional("DEBUG")]
		static public void Write(string i_message)
		{
			// TODO:  Add WinDebuger.WriteLine implementation
			if(_autoAddTime)
			{
				_debugForm._outTextBox.AppendText(string.Format("{0:hh:mm:ss}  {1}",DateTime.Now,i_message));
			}
			else
			{
				_debugForm._outTextBox.AppendText(string.Format("{0}",i_message));
			}
			_debugForm.Show();
		}

		#endregion

		private void menuItem1_Click(object sender, System.EventArgs e)
		{
			this._outTextBox.Clear();
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			this._outTextBox.SelectAll();
		}

		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			this._outTextBox.Copy();
		}

		private void WinDebuger_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			this.Hide();
			e.Cancel = true;
		}
	}
}
