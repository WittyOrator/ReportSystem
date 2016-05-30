using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using DevExpress.XtraPrinting;
using System.Drawing.Drawing2D;
using System.Globalization;

using Webb.Data;

namespace Webb.Reports.Editors
{
	#region public class UserRightEditorForm : System.Windows.Forms.Form
	/// <summary>
	/// Summary description for FormatEditForm
	/// </summary>
	public class UserRightEditorForm : System.Windows.Forms.Form
	{

		private TabControl tabControl1;
		private TabPage tabPage1;
		private TabPage tabPage2;
		private GroupBox groupBox1;
		private Label lblshow;
		private GroupBox groupBox2;
		private GroupBox groupBox3;
		private System.Windows.Forms.ListBox LstLevel;
		private System.Windows.Forms.Button BtnOk;
		private System.Windows.Forms.Button BtnCancel;
		private System.Windows.Forms.CheckBox ChkVictory;
		private System.Windows.Forms.CheckBox ChkAdvantage;
		private System.Windows.Forms.CheckBox ChkQuickCut;
		private System.Windows.Forms.CheckBox ChkPlayMaker;
		private System.Windows.Forms.CheckBox ChkGameDay;
		public UserLevel UserLevel = null;

		private UserLevel sUser=null;
		private UserLevel cUser=new UserLevel();
	
		public UserRightEditorForm(object value)
		{
			UserLevel=value as UserLevel; 

			InitializeComponent();
			
		}

		private void InitializeComponent()
		{
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.LstLevel = new System.Windows.Forms.ListBox();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.ChkVictory = new System.Windows.Forms.CheckBox();
			this.ChkQuickCut = new System.Windows.Forms.CheckBox();
			this.ChkPlayMaker = new System.Windows.Forms.CheckBox();
			this.ChkGameDay = new System.Windows.Forms.CheckBox();
			this.ChkAdvantage = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.lblshow = new System.Windows.Forms.Label();
			this.BtnOk = new System.Windows.Forms.Button();
			this.BtnCancel = new System.Windows.Forms.Button();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Location = new System.Drawing.Point(7, 54);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(217, 214);
			this.tabControl1.TabIndex = 0;
			this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.groupBox2);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(209, 188);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Standard";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.LstLevel);
			this.groupBox2.Location = new System.Drawing.Point(4, 5);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(196, 179);
			this.groupBox2.TabIndex = 0;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Choose UserLevel";
			// 
			// LstLevel
			// 
			this.LstLevel.Location = new System.Drawing.Point(5, 16);
			this.LstLevel.Name = "LstLevel";
			this.LstLevel.Size = new System.Drawing.Size(179, 160);
			this.LstLevel.TabIndex = 1;
			this.LstLevel.DoubleClick += new System.EventHandler(this.LstLevel_DoubleClick);
			this.LstLevel.SelectedIndexChanged += new System.EventHandler(this.LstLevel_SelectedIndexChanged);
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.groupBox3);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(209, 188);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Custom";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.ChkVictory);
			this.groupBox3.Controls.Add(this.ChkQuickCut);
			this.groupBox3.Controls.Add(this.ChkPlayMaker);
			this.groupBox3.Controls.Add(this.ChkGameDay);
			this.groupBox3.Controls.Add(this.ChkAdvantage);
			this.groupBox3.Location = new System.Drawing.Point(2, 6);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(206, 178);
			this.groupBox3.TabIndex = 0;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Check Option to Create Level";
			// 
			// ChkVictory
			// 
			this.ChkVictory.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ChkVictory.Location = new System.Drawing.Point(16, 24);
			this.ChkVictory.Name = "ChkVictory";
			this.ChkVictory.Size = new System.Drawing.Size(176, 24);
			this.ChkVictory.TabIndex = 0;
			this.ChkVictory.Text = "Victory";
			this.ChkVictory.CheckStateChanged += new System.EventHandler(this.Chk_CheckedChanged);
			// 
			// ChkQuickCut
			// 
			this.ChkQuickCut.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ChkQuickCut.Location = new System.Drawing.Point(16, 56);
			this.ChkQuickCut.Name = "ChkQuickCut";
			this.ChkQuickCut.Size = new System.Drawing.Size(176, 24);
			this.ChkQuickCut.TabIndex = 0;
			this.ChkQuickCut.Text = "QuickCut";
			this.ChkQuickCut.CheckStateChanged += new System.EventHandler(this.Chk_CheckedChanged);
			// 
			// ChkPlayMaker
			// 
			this.ChkPlayMaker.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ChkPlayMaker.Location = new System.Drawing.Point(16, 88);
			this.ChkPlayMaker.Name = "ChkPlayMaker";
			this.ChkPlayMaker.Size = new System.Drawing.Size(176, 24);
			this.ChkPlayMaker.TabIndex = 0;
			this.ChkPlayMaker.Text = "PlayMaker";
			this.ChkPlayMaker.CheckStateChanged += new System.EventHandler(this.Chk_CheckedChanged);
			// 
			// ChkGameDay
			// 
			this.ChkGameDay.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ChkGameDay.Location = new System.Drawing.Point(16, 120);
			this.ChkGameDay.Name = "ChkGameDay";
			this.ChkGameDay.Size = new System.Drawing.Size(176, 24);
			this.ChkGameDay.TabIndex = 0;
			this.ChkGameDay.Text = "GameDay";
			this.ChkGameDay.CheckStateChanged += new System.EventHandler(this.Chk_CheckedChanged);
			// 
			// ChkAdvantage
			// 
			this.ChkAdvantage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ChkAdvantage.Location = new System.Drawing.Point(16, 152);
			this.ChkAdvantage.Name = "ChkAdvantage";
			this.ChkAdvantage.Size = new System.Drawing.Size(176, 24);
			this.ChkAdvantage.TabIndex = 0;
			this.ChkAdvantage.Text = "Advantage";
			this.ChkAdvantage.CheckStateChanged += new System.EventHandler(this.Chk_CheckedChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.lblshow);
			this.groupBox1.Location = new System.Drawing.Point(7, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(225, 52);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Value";
			// 
			// lblshow
			// 
			this.lblshow.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblshow.Location = new System.Drawing.Point(10, 15);
			this.lblshow.Name = "lblshow";
			this.lblshow.Size = new System.Drawing.Size(206, 33);
			this.lblshow.TabIndex = 0;
			// 
			// BtnOk
			// 
			this.BtnOk.Location = new System.Drawing.Point(88, 272);
			this.BtnOk.Name = "BtnOk";
			this.BtnOk.Size = new System.Drawing.Size(57, 24);
			this.BtnOk.TabIndex = 2;
			this.BtnOk.Text = "OK";
			this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
			// 
			// BtnCancel
			// 
			this.BtnCancel.Location = new System.Drawing.Point(160, 272);
			this.BtnCancel.Name = "BtnCancel";
			this.BtnCancel.Size = new System.Drawing.Size(56, 24);
			this.BtnCancel.TabIndex = 3;
			this.BtnCancel.Text = "Cancel";
			this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
			// 
			// UserRightEditorForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(234, 304);
			this.ControlBox = false;
			this.Controls.Add(this.BtnCancel);
			this.Controls.Add(this.BtnOk);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.tabControl1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "UserRightEditorForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "UserLevel Editor";
			this.Load += new System.EventHandler(this.UserRightEditorForm_Load);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
	

		private void SetView()
		{
			ProductRight[] rights=(ProductRight[])Enum.GetValues(typeof(ProductRight));

			this.LstLevel.Items.Clear();
			UserLevelCollection levels=UserLevelCollection.StandardUserLevels();

			foreach(UserLevel level in levels)
			{
				this.LstLevel.Items.Add(level.Copy());
			}

			if(UserLevel.Name.StartsWith("Level"))
			{
				for(int i=0;i<this.LstLevel.Items.Count;i++)				
				{
					UserLevel level=this.LstLevel.Items[i] as UserLevel;

					if(UserLevel.Name==level.Name)
					{
						this.LstLevel.SelectedIndex=i;

                        this.sUser=level;

                        ShowText(sUser);

						break;
					}
				}
				this.tabControl1.SelectedIndex=0;
			}
			else
			{
				this.cUser=UserLevel;

				this.tabControl1.SelectedIndex=1;               
			}

		
		}
		private void ShowText(UserLevel user)
		{
			if(user!=null)
			{
				this.lblshow.Text=user.ToString();
			}
			else
			{
				this.lblshow.Text="Bad Level!";
			}
		}

		private void LstLevel_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.LstLevel.SelectedIndex>=0)
			{
				sUser=this.LstLevel.SelectedItem as UserLevel;
			}
			 ShowText(sUser);
		}

		private void UpdateCustom(UserLevel level)
		{	
			if(level==null)level=new UserLevel();
			if(this.ChkVictory.Checked)
			{
				level.Rights|=ProductRight.Victory;
			}	
			else
			{
			   level.Rights&=~ProductRight.Victory;
			}
			if(this.ChkQuickCut.Checked)
			{
				level.Rights|=ProductRight.QuickCut;
			}	
			else
			{
				level.Rights&=~ProductRight.QuickCut;
			}
			if(this.ChkPlayMaker.Checked)
			{
				level.Rights|=ProductRight.PlayMaker;
			}	
			else
			{
				level.Rights&=~ProductRight.PlayMaker;
			}
			if(this.ChkGameDay.Checked)
			{
				level.Rights|=ProductRight.GameDay;
			}	
			else
			{
				level.Rights&=~ProductRight.GameDay;
			}
			if(this.ChkAdvantage.Checked)
			{
				level.Rights|=ProductRight.Advantage;
			}	
			else
			{
				level.Rights&=~ProductRight.Advantage;
			}	
			ShowText(level);
		}
	
		private void UpdateView()
		{        
			if(this.tabControl1.SelectedIndex==0)
			{
				this.UserLevel=sUser;
			}
			else
			{ 
				UpdateCustom(cUser);
				this.UserLevel=cUser;
			}
		}
		private void InitCustom()
		{
			if((cUser.Rights&ProductRight.Victory)>0)
			{
				this.ChkVictory.Checked=true;
			}
			else
			{
				this.ChkVictory.Checked=false;
			}
			if((cUser.Rights&ProductRight.QuickCut)>0)
			{
				this.ChkQuickCut.Checked=true;
			}
			else
			{
				this.ChkQuickCut.Checked=false;
			}
			if((cUser.Rights&ProductRight.PlayMaker)>0)
			{
				this.ChkPlayMaker.Checked=true;
			}
			else
			{
				this.ChkPlayMaker.Checked=false;
			}
			if((cUser.Rights&ProductRight.GameDay)>0)
			{
				this.ChkGameDay.Checked=true;
			}
			else
			{
				this.ChkGameDay.Checked=false;
			}
			if((cUser.Rights&ProductRight.Advantage)>0)
			{
				this.ChkAdvantage.Checked=true;
			}
			else
			{
				this.ChkAdvantage.Checked=false;
			}
			 ShowText(cUser);
		}
		

		private void BtnOk_Click(object sender, System.EventArgs e)
		{
			UpdateView();
			
			this.DialogResult=DialogResult.OK;
			
		}

		private void BtnCancel_Click(object sender, System.EventArgs e)
		{
			this.DialogResult=DialogResult.Cancel;
		}

		private void UserRightEditorForm_Load(object sender, System.EventArgs e)
		{
			SetView();
		}

		private void Chk_CheckedChanged(object sender, System.EventArgs e)
		{
			UserLevel tempLevel=cUser.Copy();
		    UpdateCustom(tempLevel);
		}

		private void tabControl1_SelectedIndexChanged(object sender, System.EventArgs e)
		{	
			if(this.tabControl1.SelectedIndex==0)
			{
				if(this.LstLevel.SelectedIndex<0)this.LstLevel.SelectedIndex=this.LstLevel.Items.Count-1;
				ShowText(sUser);
			}
			else
			{
				InitCustom();
			}

		}

		private void LstLevel_DoubleClick(object sender, System.EventArgs e)
		{
			UpdateView();			
			this.DialogResult=DialogResult.OK;
		}
	}
	#endregion

	#region class UserRightEditor
	public class UserRightEditor : System.Drawing.Design.UITypeEditor
	{
		[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
		public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}

		// Displays the UI for value selection.
		[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
		public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, System.IServiceProvider provider, object value)
		{

			if (!(value is UserLevel))
				return value;

			IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

			UserRightEditorForm userform = new UserRightEditorForm(value);
			if (edSvc != null)
			{
				if (edSvc.ShowDialog(userform) == DialogResult.OK)
				{
					return userform.UserLevel;
				}
			}
			return value;
		}


		[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
		public override void PaintValue(System.Drawing.Design.PaintValueEventArgs e)
		{
		}


		[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
		public override bool GetPaintValueSupported(System.ComponentModel.ITypeDescriptorContext context)
		{
			return false;
		}
	}
	#endregion
}
