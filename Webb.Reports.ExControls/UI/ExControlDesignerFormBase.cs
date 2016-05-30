/***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:ExControlDesignerFormBase.cs
 * Author:Country.Wu [EMail:Webb.Country.Wu@163.com]
 * Create Time:11/9/2007 01:37:28 PM
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
using Webb.Reports.ExControls;
using Webb.Reports.ExControls.Views;

namespace Webb.Reports.ExControls.UI
{
	/// <summary>
	/// Summary description for ExControlDesignerFormBase.
	/// </summary>
	public class ExControlDesignerFormBase : System.Windows.Forms.Form
	{
		//
		protected ExControlView _ExControlView;
		protected ConfigExControlStyle _ExControlStyleControl;
		//
		private System.Windows.Forms.Button C_OK;
		private System.Windows.Forms.Button C_Apply;
		private System.Windows.Forms.Button C_Cancel;
		protected System.Windows.Forms.Panel C_MainPanel;
		private System.Windows.Forms.Panel C_Bottom;
		protected System.Windows.Forms.GroupBox C_AllTask;
		protected System.Windows.Forms.LinkLabel C_HideOrShowTask;
		protected System.Windows.Forms.Panel C_LeftMenu;
		protected System.Windows.Forms.Splitter C_Splitter;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label C_TopLabel;
		private System.Windows.Forms.Label C_LeftLabel;
		private System.Windows.Forms.Label label2;
		protected System.Windows.Forms.LinkLabel C_AutoStyle;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ExControlView ExControlView	//08-04-2008@Scott
		{
			get{return this._ExControlView;}
		}

		public ExControlDesignerFormBase()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			this._ExControlStyleControl = new ConfigExControlStyle();
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
			this.C_LeftMenu = new System.Windows.Forms.Panel();
			this.C_AllTask = new System.Windows.Forms.GroupBox();
			this.C_AutoStyle = new System.Windows.Forms.LinkLabel();
			this.label2 = new System.Windows.Forms.Label();
			this.C_LeftLabel = new System.Windows.Forms.Label();
			this.C_TopLabel = new System.Windows.Forms.Label();
			this.C_Bottom = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.C_Apply = new System.Windows.Forms.Button();
			this.C_OK = new System.Windows.Forms.Button();
			this.C_HideOrShowTask = new System.Windows.Forms.LinkLabel();
			this.C_Cancel = new System.Windows.Forms.Button();
			this.C_MainPanel = new System.Windows.Forms.Panel();
			this.C_Splitter = new System.Windows.Forms.Splitter();
			this.C_LeftMenu.SuspendLayout();
			this.C_AllTask.SuspendLayout();
			this.C_Bottom.SuspendLayout();
			this.SuspendLayout();
			// 
			// C_LeftMenu
			// 
			this.C_LeftMenu.BackColor = System.Drawing.SystemColors.ControlLight;
			this.C_LeftMenu.Controls.Add(this.C_AllTask);
			this.C_LeftMenu.Controls.Add(this.label2);
			this.C_LeftMenu.Controls.Add(this.C_LeftLabel);
			this.C_LeftMenu.Controls.Add(this.C_TopLabel);
			this.C_LeftMenu.Dock = System.Windows.Forms.DockStyle.Left;
			this.C_LeftMenu.Location = new System.Drawing.Point(0, 0);
			this.C_LeftMenu.Name = "C_LeftMenu";
			this.C_LeftMenu.Size = new System.Drawing.Size(176, 413);
			this.C_LeftMenu.TabIndex = 0;
			// 
			// C_AllTask
			// 
			this.C_AllTask.Controls.Add(this.C_AutoStyle);
			this.C_AllTask.Dock = System.Windows.Forms.DockStyle.Top;
			this.C_AllTask.Location = new System.Drawing.Point(4, 8);
			this.C_AllTask.Name = "C_AllTask";
			this.C_AllTask.Size = new System.Drawing.Size(168, 200);
			this.C_AllTask.TabIndex = 1;
			this.C_AllTask.TabStop = false;
			this.C_AllTask.Text = "Design Tasks";
			// 
			// C_AutoStyle
			// 
			this.C_AutoStyle.Dock = System.Windows.Forms.DockStyle.Top;
			this.C_AutoStyle.Location = new System.Drawing.Point(3, 18);
			this.C_AutoStyle.Name = "C_AutoStyle";
			this.C_AutoStyle.Size = new System.Drawing.Size(162, 32);
			this.C_AutoStyle.TabIndex = 3;
			this.C_AutoStyle.TabStop = true;
            this.C_AutoStyle.Text = "Generate Style";
			this.C_AutoStyle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.C_AutoStyle.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.C_AutoStyle_LinkClicked);
			// 
			// label2
			// 
			this.label2.Dock = System.Windows.Forms.DockStyle.Right;
			this.label2.Location = new System.Drawing.Point(172, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(4, 405);
			this.label2.TabIndex = 4;
			// 
			// C_LeftLabel
			// 
			this.C_LeftLabel.Dock = System.Windows.Forms.DockStyle.Left;
			this.C_LeftLabel.Location = new System.Drawing.Point(0, 8);
			this.C_LeftLabel.Name = "C_LeftLabel";
			this.C_LeftLabel.Size = new System.Drawing.Size(4, 405);
			this.C_LeftLabel.TabIndex = 3;
			// 
			// C_TopLabel
			// 
			this.C_TopLabel.Dock = System.Windows.Forms.DockStyle.Top;
			this.C_TopLabel.Location = new System.Drawing.Point(0, 0);
			this.C_TopLabel.Name = "C_TopLabel";
			this.C_TopLabel.Size = new System.Drawing.Size(176, 8);
			this.C_TopLabel.TabIndex = 2;
			// 
			// C_Bottom
			// 
			this.C_Bottom.BackColor = System.Drawing.SystemColors.ControlLight;
			this.C_Bottom.Controls.Add(this.label1);
			this.C_Bottom.Controls.Add(this.C_Apply);
			this.C_Bottom.Controls.Add(this.C_OK);
			this.C_Bottom.Controls.Add(this.C_HideOrShowTask);
			this.C_Bottom.Controls.Add(this.C_Cancel);
			this.C_Bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.C_Bottom.Location = new System.Drawing.Point(181, 373);
			this.C_Bottom.Name = "C_Bottom";
			this.C_Bottom.Size = new System.Drawing.Size(603, 40);
			this.C_Bottom.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label1.Dock = System.Windows.Forms.DockStyle.Top;
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(603, 2);
			this.label1.TabIndex = 4;
			// 
			// C_Apply
			// 
			this.C_Apply.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.C_Apply.BackColor = System.Drawing.SystemColors.Control;
			this.C_Apply.Location = new System.Drawing.Point(440, 8);
			this.C_Apply.Name = "C_Apply";
            this.C_Apply.Size = new System.Drawing.Size(75, 23);
			this.C_Apply.TabIndex = 1;
			this.C_Apply.Text = "Apply";
			this.C_Apply.Click += new System.EventHandler(this.C_Apply_Click);
			// 
			// C_OK
			// 
			this.C_OK.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.C_OK.BackColor = System.Drawing.SystemColors.Control;
			this.C_OK.Location = new System.Drawing.Point(360, 8);
			this.C_OK.Name = "C_OK";
            this.C_OK.Size = new System.Drawing.Size(75, 23);
			this.C_OK.TabIndex = 0;
			this.C_OK.Text = "OK";
			this.C_OK.Click += new System.EventHandler(this.C_OK_Click);
			// 
			// C_HideOrShowTask
			// 
			this.C_HideOrShowTask.Location = new System.Drawing.Point(8, 8);
			this.C_HideOrShowTask.Name = "C_HideOrShowTask";
			this.C_HideOrShowTask.Size = new System.Drawing.Size(104, 23);
			this.C_HideOrShowTask.TabIndex = 3;
			this.C_HideOrShowTask.TabStop = true;
			this.C_HideOrShowTask.Text = "<< Hide Tasks";
			this.C_HideOrShowTask.VisitedLinkColor = System.Drawing.Color.Blue;
			this.C_HideOrShowTask.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.C_HideOrShowTask_LinkClicked);
			// 
			// C_Cancel
			// 
			this.C_Cancel.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.C_Cancel.BackColor = System.Drawing.SystemColors.Control;
			this.C_Cancel.Location = new System.Drawing.Point(520, 8);
			this.C_Cancel.Name = "C_Cancel";
            this.C_Cancel.Size = new System.Drawing.Size(75, 23);
			this.C_Cancel.TabIndex = 2;
			this.C_Cancel.Text = "Cancel";
			this.C_Cancel.Click += new System.EventHandler(this.C_Cancel_Click);
			// 
			// C_MainPanel
			// 
			this.C_MainPanel.BackColor = System.Drawing.SystemColors.ControlLight;
			this.C_MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.C_MainPanel.Location = new System.Drawing.Point(181, 0);
			this.C_MainPanel.Name = "C_MainPanel";
			this.C_MainPanel.Size = new System.Drawing.Size(603, 373);
			this.C_MainPanel.TabIndex = 2;
			// 
			// C_Splitter
			// 
			this.C_Splitter.BackColor = System.Drawing.SystemColors.Control;
			this.C_Splitter.Location = new System.Drawing.Point(176, 0);
			this.C_Splitter.Name = "C_Splitter";
			this.C_Splitter.Size = new System.Drawing.Size(5, 413);
			this.C_Splitter.TabIndex = 3;
			this.C_Splitter.TabStop = false;
			this.C_Splitter.DoubleClick += new System.EventHandler(this.C_Splitter_DoubleClick);
			// 
			// ExControlDesignerFormBase
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(7, 15);
			this.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.ClientSize = new System.Drawing.Size(784, 413);
			this.ControlBox = false;
			this.Controls.Add(this.C_MainPanel);
			this.Controls.Add(this.C_Bottom);
			this.Controls.Add(this.C_Splitter);
			this.Controls.Add(this.C_LeftMenu);
			this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ExControlDesignerFormBase";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Control Designer";
			this.C_LeftMenu.ResumeLayout(false);
			this.C_AllTask.ResumeLayout(false);
			this.C_Bottom.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		public virtual void SetView(ExControlView i_View)
		{
			// TODO: implement
			this._ExControlView = i_View;
		}
		
		public virtual void UpdateView(ExControlView i_View)
		{
			// TODO: implement
			this._ExControlStyleControl.UpdateView(i_View);
		}

		virtual protected void C_Apply_Click(object sender, System.EventArgs e)
		{
			this.UpdateView(this._ExControlView);
			this.FocusCurrentConfigControl();
		}

		virtual protected void C_Cancel_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;		
		}

		virtual protected void C_OK_Click(object sender, System.EventArgs e)
		{
			this.UpdateView(this._ExControlView);
			this.DialogResult = DialogResult.OK;
		}

		private void C_HideOrShowTask_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			this.ChangeLeftMenu();
		}

		private void C_Splitter_DoubleClick(object sender, System.EventArgs e)
		{
			this.ChangeLeftMenu();
		}

		private void ChangeLeftMenu()
		{
			this.C_LeftMenu.Visible = !this.C_LeftMenu.Visible;
			if(this.C_LeftMenu.Visible)
			{
				this.C_HideOrShowTask.Text = "<< Hide Tasks";
			}
			else
			{
				this.C_HideOrShowTask.Text = "Show Tasks >>";
			}	
		}

		protected void LoadConfigControl(ExControlDesignerControlBase i_Control)
		{
			this.C_MainPanel.Controls.Clear();
			i_Control.Dock = DockStyle.Fill;
			this.C_MainPanel.Controls.Add(i_Control);
			this.FocusCurrentConfigControl();
		}   

		private void C_AutoStyle_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			this._ExControlStyleControl.SetView(this._ExControlView);
			this.LoadConfigControl(this._ExControlStyleControl);
		}

		protected void FocusCurrentConfigControl()
		{
			if(this.C_MainPanel.Controls.Count > 0)
			{
				this.C_MainPanel.Controls[0].Focus();
			}
		}
	}
}
