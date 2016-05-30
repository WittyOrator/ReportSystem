using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections.Specialized;
using Microsoft.Win32;
using Webb.Utilities;

namespace Webb.Reports.DataProvider.UI
{
	/// <summary>
	/// Summary description for ConfigAdvFile.
	/// </summary>
	public class ConfigCommonSQL : Webb.Utilities.Wizards.WinzardControlBase
	{
		public bool bGame = true;
		private System.Windows.Forms.FolderBrowserDialog C_FolderBrowserDlg;
		//private StringCollection _Games;
		//private StringCollection _Edls;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		static readonly string RegKeyPath = "SOFTWARE\\Webb Electronics\\WebbReport\\CommonDatabase";	

		
	    string strFormat="Data Source={0};Initial Catalog={1};User ID={2};Password={3}";

		static readonly string KeyName = "LastConnection";
		static readonly string ServerNames = "LastServerIps";


		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cmbServer;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtDataBase;
		private System.Windows.Forms.Label lblUser;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtUser;
		private System.Windows.Forms.TextBox txtPwd;
		private System.Windows.Forms.Button BtnConnect;
		private System.Windows.Forms.Label lblSelectTable;
		private System.Windows.Forms.ComboBox cmbTable;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.RadioButton RadioSimple;
		private System.Windows.Forms.RadioButton RadioNext;

		private RegistryKey _RegKey;
		private RegistryKey RegKey
		{
			get
			{
				if(this._RegKey==null)
				{
                    this._RegKey = Registry.CurrentUser.OpenSubKey(RegKeyPath, true);
					if(this._RegKey==null)
					{
                        this._RegKey = Registry.CurrentUser.CreateSubKey(RegKeyPath);
					}
				}
				return this._RegKey;
			}
		}
		
	
		public ConfigCommonSQL()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			
			this.Load += new EventHandler(ConfigAdvFile_Load);

			this.WizardTitle = "Step 2: Set Connection";
			this.FinishControl = true;
			this.LastControl = true;
			this.SelectStep = true;

			//this.ClearAllList();
			// TODO: Add any initialization after the InitializeComponent call
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if(this._RegKey!=null) this._RegKey.Close();

			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.C_FolderBrowserDlg = new System.Windows.Forms.FolderBrowserDialog();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.RadioNext = new System.Windows.Forms.RadioButton();
			this.RadioSimple = new System.Windows.Forms.RadioButton();
			this.label3 = new System.Windows.Forms.Label();
			this.cmbTable = new System.Windows.Forms.ComboBox();
			this.lblSelectTable = new System.Windows.Forms.Label();
			this.BtnConnect = new System.Windows.Forms.Button();
			this.txtDataBase = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.cmbServer = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.txtUser = new System.Windows.Forms.TextBox();
			this.lblUser = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.txtPwd = new System.Windows.Forms.TextBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// C_FolderBrowserDlg
			// 
			this.C_FolderBrowserDlg.ShowNewFolderButton = false;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.RadioNext);
			this.groupBox1.Controls.Add(this.RadioSimple);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.cmbTable);
			this.groupBox1.Controls.Add(this.lblSelectTable);
			this.groupBox1.Controls.Add(this.BtnConnect);
			this.groupBox1.Controls.Add(this.txtDataBase);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.cmbServer);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.txtUser);
			this.groupBox1.Controls.Add(this.lblUser);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.txtPwd);
			this.groupBox1.Location = new System.Drawing.Point(32, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(704, 456);
			this.groupBox1.TabIndex = 7;
			this.groupBox1.TabStop = false;
			// 
			// RadioNext
			// 
			this.RadioNext.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.RadioNext.Location = new System.Drawing.Point(80, 384);
			this.RadioNext.Name = "RadioNext";
			this.RadioNext.Size = new System.Drawing.Size(176, 24);
			this.RadioNext.TabIndex = 9;
			this.RadioNext.Text = "Advanced";
			this.RadioNext.CheckedChanged += new System.EventHandler(this.Radio_CheckedChanged);
			// 
			// RadioSimple
			// 
			this.RadioSimple.Checked = true;
			this.RadioSimple.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.RadioSimple.Location = new System.Drawing.Point(80, 256);
			this.RadioSimple.Name = "RadioSimple";
			this.RadioSimple.Size = new System.Drawing.Size(216, 24);
			this.RadioSimple.TabIndex = 8;
			this.RadioSimple.TabStop = true;
			this.RadioSimple.Text = "Simply select a table";
			this.RadioSimple.CheckedChanged += new System.EventHandler(this.Radio_CheckedChanged);
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.Location = new System.Drawing.Point(56, 24);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(560, 24);
			this.label3.TabIndex = 7;
			this.label3.Text = "Please input your connection information in the below:";
			// 
			// cmbTable
			// 
			this.cmbTable.Location = new System.Drawing.Point(248, 328);
			this.cmbTable.Name = "cmbTable";
			this.cmbTable.Size = new System.Drawing.Size(352, 22);
			this.cmbTable.TabIndex = 6;
			// 
			// lblSelectTable
			// 
			this.lblSelectTable.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblSelectTable.Location = new System.Drawing.Point(120, 328);
			this.lblSelectTable.Name = "lblSelectTable";
			this.lblSelectTable.Size = new System.Drawing.Size(128, 16);
			this.lblSelectTable.TabIndex = 5;
			this.lblSelectTable.Text = "select the table";
			// 
			// BtnConnect
			// 
			this.BtnConnect.Location = new System.Drawing.Point(120, 288);
			this.BtnConnect.Name = "BtnConnect";
			this.BtnConnect.Size = new System.Drawing.Size(288, 24);
			this.BtnConnect.TabIndex = 4;
			this.BtnConnect.Text = "Connect database and get all tables";
			this.BtnConnect.Click += new System.EventHandler(this.BtnConnect_Click);
			// 
			// txtDataBase
			// 
			this.txtDataBase.Location = new System.Drawing.Point(248, 104);
			this.txtDataBase.Name = "txtDataBase";
			this.txtDataBase.Size = new System.Drawing.Size(240, 22);
			this.txtDataBase.TabIndex = 3;
			this.txtDataBase.Text = "";
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.Location = new System.Drawing.Point(112, 104);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(136, 16);
			this.label2.TabIndex = 2;
			this.label2.Text = "Database Name:";
			// 
			// cmbServer
			// 
			this.cmbServer.Location = new System.Drawing.Point(248, 64);
			this.cmbServer.Name = "cmbServer";
			this.cmbServer.Size = new System.Drawing.Size(368, 22);
			this.cmbServer.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(152, 64);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(88, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Server IP:";
			// 
			// txtUser
			// 
			this.txtUser.Location = new System.Drawing.Point(248, 152);
			this.txtUser.Name = "txtUser";
			this.txtUser.Size = new System.Drawing.Size(192, 22);
			this.txtUser.TabIndex = 3;
			this.txtUser.Text = "";
			// 
			// lblUser
			// 
			this.lblUser.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblUser.Location = new System.Drawing.Point(192, 152);
			this.lblUser.Name = "lblUser";
			this.lblUser.Size = new System.Drawing.Size(48, 16);
			this.lblUser.TabIndex = 2;
			this.lblUser.Text = "User:";
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label4.Location = new System.Drawing.Point(160, 192);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(88, 16);
			this.label4.TabIndex = 2;
			this.label4.Text = "Password:";
			// 
			// txtPwd
			// 
			this.txtPwd.Location = new System.Drawing.Point(248, 192);
			this.txtPwd.Name = "txtPwd";
			this.txtPwd.PasswordChar = '*';
			this.txtPwd.Size = new System.Drawing.Size(192, 22);
			this.txtPwd.TabIndex = 3;
			this.txtPwd.Text = "";
			// 
			// ConfigCommonSQL
			// 
			this.Controls.Add(this.groupBox1);
			this.Name = "ConfigCommonSQL";
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion		
		
		
		//<=
			

	
		//load
		private void ConfigAdvFile_Load(object sender, EventArgs e)
		{
			object setting = this.RegKey.GetValue(ServerNames);

			this.cmbServer.Items.Clear();

			if(setting != null&&setting.ToString()!=string.Empty)
			{				
				 string[] serverIps= setting.ToString().Split(";".ToCharArray());  

				foreach(string serverip in serverIps)
				{
					if(serverip!=string.Empty&&!this.cmbServer.Items.Contains(serverip))
					{
						this.cmbServer.Items.Add(serverip);
					}
				}
			}

			setting = this.RegKey.GetValue(KeyName);			

			if(setting != null&&setting.ToString()!=string.Empty)
			{				
				string[] config= setting.ToString().Split(";".ToCharArray());  

				if(config.Length==4||config.Length==5)
				{
					this.cmbServer.Text=config[0];
					this.txtDataBase.Text=config[1];
					this.txtUser.Text=config[2];
					this.txtPwd.Text=config[3];
					if(config.Length==5)
				    {
						this.cmbTable.Text=config[4];
				    }
				}
				
			}


			
		}


		public override bool ValidateSetting()
		{
			return true;
		}

		public override void SetData(object i_Data)
		{
			if(this.RadioSimple.Checked)
			{
				this.LastControl= true;
				this.FinishControl=true;
				this.BtnConnect.Enabled=true;
				this.cmbTable.Enabled=true;
			}
			else
			{
				this.LastControl= false;
				this.FinishControl=false;
				this.BtnConnect.Enabled=false;
				this.cmbTable.Enabled=false;
			}			
		}	
		private bool ConnectedDataBase()
		{
			if(this.cmbServer.Text.Trim()==string.Empty)
			{
				MessageBox.Show("Please input the serverIP which the database located in!",
					"Empty serverIp",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
				return false;
			}
			if(this.txtDataBase.Text.Trim()==string.Empty)
			{
				MessageBox.Show("Please input the database name!",
						"Empty Database-Name",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);

				return false;
			}
			
			string connectionstring=string.Format(strFormat,this.cmbServer.Text,this.txtDataBase.Text,this.txtUser.Text,this.txtPwd.Text);

			try
			{

				System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(connectionstring);

                SqlCommand cmd = new SqlCommand("select name from [sysobjects] where xtype = 'u' or  type='V'", conn);  

				Webb.Utilities.WaitingForm.ShowWaitingForm();

				Webb.Utilities.WaitingForm.SetWaitingMessage("Connecting to "+this.cmbServer.Text+" , please wait...");
				
				conn.Open();

				Webb.Utilities.WaitingForm.SetWaitingMessage("get tables information from Database-Server, please wait...");

				this.cmbTable.Items.Clear();

				SqlDataReader dataReader=cmd.ExecuteReader();

				while(dataReader.Read())
				{
					this.cmbTable.Items.Add(dataReader["name"]);

				}	

                dataReader.Close();

				conn.Close();

				conn.Dispose();

				Webb.Utilities.WaitingForm.CloseWaitingForm();



				MessageBox.Show("Succes to connect the database.","success",MessageBoxButtons.OK,MessageBoxIcon.Information);
                
				return true;

			}
			catch(Exception ex)
			{
				Webb.Utilities.WaitingForm.CloseWaitingForm();

				MessageBox.Show("Connection failed:"+ex.Message,"Failed",MessageBoxButtons.OK,MessageBoxIcon.Information);
				
				return false;
			}
			

		}

		public override void OnClearAll()
		{
			this.cmbServer.Text="";
			this.txtDataBase.Text="";
			this.txtUser.Text="";
			this.txtPwd.Text="";
			this.cmbTable.Text="";
			this.cmbTable.Items.Clear();
		}


		
		public  bool UpdateConfig(DBSourceConfig _DBSourceConfig)
		{
			if(this.cmbServer.Text.Trim()==string.Empty)
			{
				MessageBox.Show("Please input the serverIP which the database located in!",
					"Failed",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
				return false;
			}
			if(this.txtDataBase.Text.Trim()==string.Empty)
			{
				MessageBox.Show("Please input the database name!",
					"Failed",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
				return false;
			}
			if(this.cmbTable.Text.Trim()==string.Empty&&this.RadioSimple.Checked)
			{
				MessageBox.Show("Please input the table name or connect the database to fetch the tabel name!",
                            "Failed",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
				 return false;
			}

			string connectionstring=string.Format(strFormat,this.cmbServer.Text,this.txtDataBase.Text,this.txtUser.Text,this.txtPwd.Text);

			_DBSourceConfig.ConnString=connectionstring;

			string Sql="select * from ["+this.cmbTable.Text+"]";

            _DBSourceConfig.DefaultSQLCmd=Sql;

			 WriteRegistery();

			return true;
			
		}

		private void WriteRegistery()
		{
			string strText=string.Format("{0};{1};{2};{3};{4}",
									     this.cmbServer.Text,this.txtDataBase.Text,			
				                      this.txtUser.Text,this.txtPwd.Text,this.cmbTable.Text);
		
			System.Text.StringBuilder sb=new System.Text.StringBuilder();
			foreach(string strip in this.cmbServer.Items)
			{
                sb.Append(strip+";");
			}
			if(!this.cmbServer.Items.Contains(this.cmbServer.Text))
			{
				sb.Append(this.cmbServer.Text);
			}
			this.RegKey.SetValue(ServerNames,sb.ToString());
            this.RegKey.SetValue(KeyName,strText);
				                         
		}

		private void BtnConnect_Click(object sender, System.EventArgs e)
		{
			ConnectedDataBase();
		}

		private void Radio_CheckedChanged(object sender, System.EventArgs e)
		{
			if(this.RadioSimple.Checked)
			{
				this.ParentWizardForm.WizardStatus|=Webb.Utilities.Wizards.WizardStatus.OK;
			    this.ParentWizardForm.WizardStatus&=~Webb.Utilities.Wizards.WizardStatus.Next;

				this.BtnConnect.Enabled=true;
				this.cmbTable.Enabled=true;
			}
			else
			{
				this.ParentWizardForm.WizardStatus|=Webb.Utilities.Wizards.WizardStatus.Next;
				this.ParentWizardForm.WizardStatus&=~Webb.Utilities.Wizards.WizardStatus.OK;

				this.BtnConnect.Enabled=false;
				this.cmbTable.Enabled=false;
			}		
		}	
	}
}
