/***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:WizardBaseForm.cs
 * Author:Country.Wu [EMail:Webb.Country.Wu@163.com]
 * Create Time:11/8/2007 10:09:53 AM
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

namespace Webb.Utilities.Wizards
{
	/// <summary>
	/// Summary description for WizardBaseForm.
	/// </summary>
	public class WizardBaseForm : System.Windows.Forms.Form
	{

		//
		private int _CurrentStepIndex = -1;
		private WizardStatus _Status = WizardStatus.Cancel;
		protected WinzardControlBase _CurrentControl;
		//
//		new public Size Size
//		{
//			get{return base.Size;}
//		}
        //new public FormBorderStyle FormBorderStyle
        //{
        //    get{return base.FormBorderStyle;}
        //}
		//
		public int CurrentStep
		{
			get{return this._CurrentStepIndex;}
			set{this._CurrentStepIndex = value; }
		}

		public bool SelectStep
		{
			set
			{
				this.C_ClearAll.Visible = value;			
				this.C_SelectAll.Visible = value;
			}
		}

		public bool SingleStep
		{
			set
			{
				this.C_Next.Visible = !value;			
				this.C_Back.Visible = !value;
			}
		}

		public WizardStatus WizardStatus
		{
			get{return this._Status;}
			set{
				this._Status = value;
				if((value&WizardStatus.Back)==WizardStatus.Back)	{this.C_Back.Enabled = true;}	else{this.C_Back.Enabled = false;}
				if((value&WizardStatus.Cancel)==WizardStatus.Cancel){this.C_Cancel.Enabled = true;}	else{this.C_Cancel.Enabled = false;}
				if((value&WizardStatus.Next)==WizardStatus.Next)	{this.C_Next.Enabled = true;}else{this.C_Next.Enabled = false;}
				if((value&WizardStatus.OK)==WizardStatus.OK)		{this.C_OK.Enabled = true;} else{this.C_OK.Enabled = false;}
			}
		} 

		public string Title
		{
			get{return this.C_TitleLabel.Text;}
			set{this.C_TitleLabel.Text = value;}
		}
		public Image TitleBackImg
		{
			get{return this.C_TitleLabel.Image;}
			set{this.C_TitleLabel.Image = value;}
		}
		//
		private System.Windows.Forms.Panel C_BottomPanel;
		private System.Windows.Forms.Panel C_MainPanel;
		private System.Windows.Forms.Button C_Cancel;
		private System.Windows.Forms.Button C_OK;
		protected System.Windows.Forms.Button C_Next;
		protected System.Windows.Forms.Button C_Back;
		private System.Windows.Forms.Label C_TitleLabel;
		private System.Windows.Forms.Label C_SperatorLabel;
		protected System.Windows.Forms.Button C_SelectAll;
		protected System.Windows.Forms.Button C_ClearAll;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public WizardBaseForm()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WizardBaseForm));
            this.C_TitleLabel = new System.Windows.Forms.Label();
            this.C_BottomPanel = new System.Windows.Forms.Panel();
            this.C_ClearAll = new System.Windows.Forms.Button();
            this.C_SelectAll = new System.Windows.Forms.Button();
            this.C_SperatorLabel = new System.Windows.Forms.Label();
            this.C_Cancel = new System.Windows.Forms.Button();
            this.C_OK = new System.Windows.Forms.Button();
            this.C_Next = new System.Windows.Forms.Button();
            this.C_Back = new System.Windows.Forms.Button();
            this.C_MainPanel = new System.Windows.Forms.Panel();
            this.C_BottomPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // C_TitleLabel
            // 
            this.C_TitleLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.C_TitleLabel.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.C_TitleLabel.Image = ((System.Drawing.Image)(resources.GetObject("C_TitleLabel.Image")));
            this.C_TitleLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.C_TitleLabel.Location = new System.Drawing.Point(0, 0);
            this.C_TitleLabel.Name = "C_TitleLabel";
            this.C_TitleLabel.Size = new System.Drawing.Size(808, 32);
            this.C_TitleLabel.TabIndex = 0;
            this.C_TitleLabel.Text = " Webb Wizard Form Step 1";
            this.C_TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // C_BottomPanel
            // 
            this.C_BottomPanel.Controls.Add(this.C_ClearAll);
            this.C_BottomPanel.Controls.Add(this.C_SelectAll);
            this.C_BottomPanel.Controls.Add(this.C_SperatorLabel);
            this.C_BottomPanel.Controls.Add(this.C_Cancel);
            this.C_BottomPanel.Controls.Add(this.C_OK);
            this.C_BottomPanel.Controls.Add(this.C_Next);
            this.C_BottomPanel.Controls.Add(this.C_Back);
            this.C_BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.C_BottomPanel.Location = new System.Drawing.Point(0, 534);
            this.C_BottomPanel.Name = "C_BottomPanel";
            this.C_BottomPanel.Size = new System.Drawing.Size(808, 48);
            this.C_BottomPanel.TabIndex = 1;
            // 
            // C_ClearAll
            // 
            this.C_ClearAll.Location = new System.Drawing.Point(99, 15);
            this.C_ClearAll.Name = "C_ClearAll";
            this.C_ClearAll.Size = new System.Drawing.Size(75, 23);
            this.C_ClearAll.TabIndex = 3;
            this.C_ClearAll.Text = "Clear All";
            this.C_ClearAll.Visible = false;
            this.C_ClearAll.Click += new System.EventHandler(this.C_ClearAll_Click);
            // 
            // C_SelectAll
            // 
            this.C_SelectAll.Location = new System.Drawing.Point(19, 15);
            this.C_SelectAll.Name = "C_SelectAll";
            this.C_SelectAll.Size = new System.Drawing.Size(75, 23);
            this.C_SelectAll.TabIndex = 2;
            this.C_SelectAll.Tag = "v";
            this.C_SelectAll.Text = "Select All";
            this.C_SelectAll.Visible = false;
            this.C_SelectAll.Click += new System.EventHandler(this.C_SelectAll_Click);
            // 
            // C_SperatorLabel
            // 
            this.C_SperatorLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.C_SperatorLabel.Location = new System.Drawing.Point(8, 0);
            this.C_SperatorLabel.Name = "C_SperatorLabel";
            this.C_SperatorLabel.Size = new System.Drawing.Size(816, 3);
            this.C_SperatorLabel.TabIndex = 1;
            // 
            // C_Cancel
            // 
            this.C_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.C_Cancel.Location = new System.Drawing.Point(718, 16);
            this.C_Cancel.Name = "C_Cancel";
            this.C_Cancel.Size = new System.Drawing.Size(75, 23);
            this.C_Cancel.TabIndex = 0;
            this.C_Cancel.Text = "Cancel";
            this.C_Cancel.Click += new System.EventHandler(this.C_Cancel_Click);
            // 
            // C_OK
            // 
            this.C_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.C_OK.Location = new System.Drawing.Point(638, 16);
            this.C_OK.Name = "C_OK";
            this.C_OK.Size = new System.Drawing.Size(75, 23);
            this.C_OK.TabIndex = 0;
            this.C_OK.Text = "OK";
            this.C_OK.Click += new System.EventHandler(this.C_OK_Click);
            // 
            // C_Next
            // 
            this.C_Next.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.C_Next.Location = new System.Drawing.Point(558, 16);
            this.C_Next.Name = "C_Next";
            this.C_Next.Size = new System.Drawing.Size(75, 23);
            this.C_Next.TabIndex = 0;
            this.C_Next.Text = "Next ->";
            this.C_Next.Click += new System.EventHandler(this.C_Next_Click);
            // 
            // C_Back
            // 
            this.C_Back.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.C_Back.Location = new System.Drawing.Point(478, 16);
            this.C_Back.Name = "C_Back";
            this.C_Back.Size = new System.Drawing.Size(75, 23);
            this.C_Back.TabIndex = 0;
            this.C_Back.Text = "<- Back";
            this.C_Back.Click += new System.EventHandler(this.C_Back_Click);
            // 
            // C_MainPanel
            // 
            this.C_MainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.C_MainPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.C_MainPanel.Location = new System.Drawing.Point(8, 40);
            this.C_MainPanel.Name = "C_MainPanel";
            this.C_MainPanel.Size = new System.Drawing.Size(800, 487);
            this.C_MainPanel.TabIndex = 2;
            // 
            // WizardBaseForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(7, 15);
            this.ClientSize = new System.Drawing.Size(808, 582);
            this.Controls.Add(this.C_MainPanel);
            this.Controls.Add(this.C_BottomPanel);
            this.Controls.Add(this.C_TitleLabel);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WizardBaseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Webb Wizard Form";
            this.C_BottomPanel.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void C_OK_Click(object sender, System.EventArgs e)
		{
			this.OnOK();
		}

		private void C_Cancel_Click(object sender, System.EventArgs e)
		{
			this.OnCancel();
		}

		public virtual void OnNextStep()
		{
			// TODO: implement
		}
      
		public virtual void OnBackStep()
		{
			// TODO: implement
		}
      
		public virtual void OnCancel()
		{
			this.DialogResult = DialogResult.Cancel;
		}
      
		public virtual void OnOK()
		{
			this.DialogResult = DialogResult.OK;
		}
      
		public void LoadControl(WinzardControlBase i_SetpControl)
		{
			// TODO: implement
			this.C_MainPanel.Controls.Clear();
			i_SetpControl.Dock = DockStyle.Fill;
			i_SetpControl.ParentWizardForm = this;
			this.Title = i_SetpControl.WizardTitle;
			this.SelectStep = i_SetpControl.SelectStep;
			this.SingleStep = i_SetpControl.SingleStep;
			this.C_MainPanel.Controls.Add(i_SetpControl);
			this._CurrentControl = i_SetpControl;
		}

		public virtual void UpdateData(object i_Data)
		{
			// TODO: implement
		}
      
		public virtual void SetData(object i_Data)
		{
			// TODO: implement
		}

		private void C_Back_Click(object sender, System.EventArgs e)
		{
			this.OnBackStep();
		}

		private void C_Next_Click(object sender, System.EventArgs e)
		{
            this.OnNextStep();
		}

		private void C_SelectAll_Click(object sender, System.EventArgs e)
		{
			this._CurrentControl.OnSelectAll();
		}

		private void C_ClearAll_Click(object sender, System.EventArgs e)
		{
			this._CurrentControl.OnClearAll();
		}
		protected void HideNextAndBack()
		{
			this.C_Next.Visible=false;
			this.C_Back.Visible=false;
		}
	}
}
