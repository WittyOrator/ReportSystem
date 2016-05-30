using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

using System.Collections;
using System.Collections.Specialized;
using System.IO;
using Webb.Data;
using Microsoft.Win32;


namespace Webb.Reports.Editors
{
	#region public class AdvUserEditorForm : System.Windows.Forms.Form	
	public class AdvUserEditorForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button BtnRemoveAll;
		private System.Windows.Forms.Button BtnRemoveOne;
		private System.Windows.Forms.Button BtnAddAll;
		private System.Windows.Forms.Button BtnAddNew;
		private System.Windows.Forms.ListBox LstAdvUser;
		private System.Windows.Forms.ListBox LstLocal;
		private System.Windows.Forms.Button BtnAddOne;
		private System.Windows.Forms.TextBox txtNew;

		
		private System.Windows.Forms.Button BtnOk;
		private System.Windows.Forms.Button BtnCancel;
		private System.Windows.Forms.CheckBox ChkAll;

        public AdvUserRights AdvUsers=new AdvUserRights();

		private StringCollection AllUserList=new StringCollection();

		public AdvUserEditorForm(object value)
		{
			AdvUsers=value as AdvUserRights;		

			InitializeComponent();			
            InitAllUsers();
			RefreshAllUser();

			this.ChkAll.Checked=AdvUsers.All;		

		}


		private void InitializeComponent()
		{
            this.BtnOk = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnRemoveAll = new System.Windows.Forms.Button();
            this.BtnRemoveOne = new System.Windows.Forms.Button();
            this.BtnAddAll = new System.Windows.Forms.Button();
            this.txtNew = new System.Windows.Forms.TextBox();
            this.BtnAddNew = new System.Windows.Forms.Button();
            this.LstAdvUser = new System.Windows.Forms.ListBox();
            this.LstLocal = new System.Windows.Forms.ListBox();
            this.BtnAddOne = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.ChkAll = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnOk
            // 
            this.BtnOk.Location = new System.Drawing.Point(298, 336);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(76, 26);
            this.BtnOk.TabIndex = 9;
            this.BtnOk.Text = "Ok";
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Location = new System.Drawing.Point(403, 336);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(77, 26);
            this.BtnCancel.TabIndex = 9;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.BtnRemoveAll);
            this.groupBox1.Controls.Add(this.BtnRemoveOne);
            this.groupBox1.Controls.Add(this.BtnAddAll);
            this.groupBox1.Controls.Add(this.txtNew);
            this.groupBox1.Controls.Add(this.BtnAddNew);
            this.groupBox1.Controls.Add(this.LstAdvUser);
            this.groupBox1.Controls.Add(this.LstLocal);
            this.groupBox1.Controls.Add(this.BtnAddOne);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(10, 34);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(480, 293);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(19, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(163, 17);
            this.label1.TabIndex = 18;
            this.label1.Text = "Local AdvUsers";
            // 
            // BtnRemoveAll
            // 
            this.BtnRemoveAll.Location = new System.Drawing.Point(202, 103);
            this.BtnRemoveAll.Name = "BtnRemoveAll";
            this.BtnRemoveAll.Size = new System.Drawing.Size(67, 26);
            this.BtnRemoveAll.TabIndex = 16;
            this.BtnRemoveAll.Text = "<<---";
            this.BtnRemoveAll.Click += new System.EventHandler(this.BtnRemoveAll_Click);
            // 
            // BtnRemoveOne
            // 
            this.BtnRemoveOne.Location = new System.Drawing.Point(202, 155);
            this.BtnRemoveOne.Name = "BtnRemoveOne";
            this.BtnRemoveOne.Size = new System.Drawing.Size(67, 26);
            this.BtnRemoveOne.TabIndex = 15;
            this.BtnRemoveOne.Text = "<-";
            this.BtnRemoveOne.Click += new System.EventHandler(this.BtnRemoveOne_Click);
            // 
            // BtnAddAll
            // 
            this.BtnAddAll.Location = new System.Drawing.Point(202, 52);
            this.BtnAddAll.Name = "BtnAddAll";
            this.BtnAddAll.Size = new System.Drawing.Size(67, 26);
            this.BtnAddAll.TabIndex = 14;
            this.BtnAddAll.Text = "--->>";
            this.BtnAddAll.Click += new System.EventHandler(this.BtnAddAll_Click);
            // 
            // txtNew
            // 
            this.txtNew.Location = new System.Drawing.Point(288, 258);
            this.txtNew.Name = "txtNew";
            this.txtNew.Size = new System.Drawing.Size(106, 21);
            this.txtNew.TabIndex = 12;
            // 
            // BtnAddNew
            // 
            this.BtnAddNew.Location = new System.Drawing.Point(403, 258);
            this.BtnAddNew.Name = "BtnAddNew";
            this.BtnAddNew.Size = new System.Drawing.Size(58, 26);
            this.BtnAddNew.TabIndex = 11;
            this.BtnAddNew.Text = "Add";
            this.BtnAddNew.Click += new System.EventHandler(this.BtnAddNew_Click);
            // 
            // LstAdvUser
            // 
            this.LstAdvUser.ItemHeight = 12;
            this.LstAdvUser.Location = new System.Drawing.Point(288, 34);
            this.LstAdvUser.Name = "LstAdvUser";
            this.LstAdvUser.Size = new System.Drawing.Size(173, 208);
            this.LstAdvUser.TabIndex = 10;
            this.LstAdvUser.DoubleClick += new System.EventHandler(this.LstAdvUser_DoubleClick);
            // 
            // LstLocal
            // 
            this.LstLocal.ItemHeight = 12;
            this.LstLocal.Location = new System.Drawing.Point(19, 34);
            this.LstLocal.Name = "LstLocal";
            this.LstLocal.Size = new System.Drawing.Size(163, 232);
            this.LstLocal.TabIndex = 9;
            this.LstLocal.DoubleClick += new System.EventHandler(this.LstLocal_DoubleClick);
            // 
            // BtnAddOne
            // 
            this.BtnAddOne.Location = new System.Drawing.Point(202, 207);
            this.BtnAddOne.Name = "BtnAddOne";
            this.BtnAddOne.Size = new System.Drawing.Size(67, 26);
            this.BtnAddOne.TabIndex = 13;
            this.BtnAddOne.Text = "--->";
            this.BtnAddOne.Click += new System.EventHandler(this.BtnAddOne_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(288, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(163, 17);
            this.label2.TabIndex = 17;
            this.label2.Text = "AdvUsers";
            // 
            // ChkAll
            // 
            this.ChkAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkAll.Location = new System.Drawing.Point(14, 4);
            this.ChkAll.Name = "ChkAll";
            this.ChkAll.Size = new System.Drawing.Size(132, 29);
            this.ChkAll.TabIndex = 11;
            this.ChkAll.Text = "All Users";
            this.ChkAll.CheckedChanged += new System.EventHandler(this.ChkAll_CheckedChanged);
            // 
            // AdvUserEditorForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(493, 369);
            this.Controls.Add(this.ChkAll);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.BtnOk);
            this.Controls.Add(this.BtnCancel);
            this.Name = "AdvUserEditorForm";
            this.Text = "AdvUser Editor";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

		}

		private void InitAllUsers()
		{
			if(AllUserList==null)AllUserList=new StringCollection();

             AllUserList.Clear();

			string strUserFolder =Webb.Reports.DataProvider.VideoPlayBackManager.PublicDBProvider.DBSourceConfig.UserFolder;

			if(strUserFolder == null||strUserFolder==string.Empty)
			{
				 RegistryKey RegKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Webb Electronics\\WebbReport\\RecentAdvantageUserFolder",true);
                 
				if(RegKey==null)return;

				object objUserFolder = RegKey.GetValue("LastAdvantageUserPath");

				if(objUserFolder == null) return;

				strUserFolder = objUserFolder.ToString();			
			}

			if(strUserFolder.EndsWith("\\")) strUserFolder = strUserFolder.Remove(strUserFolder.Length - 1,1);

			int index = strUserFolder.LastIndexOf('\\');

			strUserFolder = strUserFolder.Remove(index + 1,strUserFolder.Length - index - 1);
            
			if(!Directory.Exists(strUserFolder))return;

			string[] subDirectories=Directory.GetDirectories(strUserFolder);

			foreach(string subDirectory in subDirectories)
			{
				if(File.Exists(subDirectory+@"\userinfo.dat"))
				{
					DirectoryInfo direInfo=new DirectoryInfo(subDirectory);

					AllUserList.Add(direInfo.Name);
				}
			}  
			this.LstAdvUser.Items.Clear();

			foreach(string sUser in AdvUsers.Users)
			{	
				this.LstAdvUser.Items.Add(sUser);
			}
  
		}

		private void RefreshAllUser()
		{
			this.LstLocal.Items.Clear();

			foreach(string sUser in AllUserList)
			{
				if(this.LstAdvUser.Items.Contains(sUser))continue;

                 this.LstLocal.Items.Add(sUser);
			}
		}		
		private void UpdateUsers()
		{
           this.AdvUsers.All=this.ChkAll.Checked;

		    AdvUsers.Users.Clear();
			
			foreach(string user in this.LstAdvUser.Items)
			{
				AdvUsers.Users.Add(user);
			}		
		}
		private void BtnOk_Click(object sender, System.EventArgs e)
		{
			UpdateUsers();

			DialogResult=DialogResult.OK;
		  
		}

		private void BtnAddOne_Click(object sender, System.EventArgs e)
		{
			if(this.LstLocal.SelectedIndex>=0)
			{
				this.LstAdvUser.Items.Add(this.LstLocal.SelectedItem);

                this.RefreshAllUser();
			}
		}
       
		private void BtnAddAll_Click(object sender, System.EventArgs e)
		{
			this.LstLocal.Items.Clear();

			foreach(string sUser in AllUserList)
			{
				if(this.LstAdvUser.Items.Contains(sUser))continue;

				this.LstAdvUser.Items.Add(sUser);
			}
		}

		private void BtnRemoveOne_Click(object sender, System.EventArgs e)
		{
			if(this.LstAdvUser.SelectedIndex>=0)
			{
				this.LstAdvUser.Items.RemoveAt(this.LstAdvUser.SelectedIndex);

				this.RefreshAllUser();
			}		
		}

		private void BtnRemoveAll_Click(object sender, System.EventArgs e)
		{
			this.LstAdvUser.Items.Clear();

			this.RefreshAllUser();
		}

		private void BtnCancel_Click(object sender, System.EventArgs e)
		{
			DialogResult=DialogResult.Cancel;
		}

		private void BtnAddNew_Click(object sender, System.EventArgs e)
		{
			string user=this.txtNew.Text.Trim();

			if(this.LstAdvUser.Items.Contains(user)||AllUserList.Contains(user))
			{
				MessageBox.Show("Users have existed in the List!");
				
				return;
			}

			this.LstAdvUser.Items.Add(user);
		}

		private void LstLocal_DoubleClick(object sender, System.EventArgs e)
		{
			if(this.LstLocal.SelectedIndex>=0)
			{
				this.LstAdvUser.Items.Add(this.LstLocal.SelectedItem);

				this.RefreshAllUser();
			}
		}

		private void LstAdvUser_DoubleClick(object sender, System.EventArgs e)
		{
			if(this.LstAdvUser.SelectedIndex>=0)
			{
				this.LstAdvUser.Items.RemoveAt(this.LstAdvUser.SelectedIndex);

				this.RefreshAllUser();
			}		
		}

		private void ChkAll_CheckedChanged(object sender, System.EventArgs e)
		{
			if(ChkAll.Checked)
			{
				this.groupBox1.Enabled=false;
			}
			else
			{
				this.groupBox1.Enabled=true;
			}
		}
		
	}
	#endregion

	public class AdvUserEditor : System.Drawing.Design.UITypeEditor
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

			if (!(value is AdvUserRights))
				return value;

			IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

			AdvUserEditorForm aduserForm= new AdvUserEditorForm(value);
			if (edSvc != null)
			{
				if (edSvc.ShowDialog(aduserForm) == DialogResult.OK)
				{
					return aduserForm.AdvUsers;
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
}
