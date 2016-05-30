using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Data.OleDb;

using Webb.Utilities;

namespace Webb.Reports.DataProvider.UI
{
	public class ConfigDBCatalog : Webb.Utilities.Wizards.WinzardControlBase
	{
		private System.Windows.Forms.GroupBox C_GroupBox;
		public System.Windows.Forms.CheckedListBox C_Databases;
		private System.ComponentModel.IContainer components = null;

		public ConfigDBCatalog()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();
			this.WizardTitle = "Step 2: Choose a database";
			// TODO: Add any initialization after the InitializeComponent call
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

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.C_GroupBox = new System.Windows.Forms.GroupBox();
			this.C_Databases = new System.Windows.Forms.CheckedListBox();
			this.C_GroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// C_GroupBox
			// 
			this.C_GroupBox.Controls.Add(this.C_Databases);
			this.C_GroupBox.Location = new System.Drawing.Point(8, 0);
			this.C_GroupBox.Name = "C_GroupBox";
			this.C_GroupBox.Size = new System.Drawing.Size(768, 456);
			this.C_GroupBox.TabIndex = 6;
			this.C_GroupBox.TabStop = false;
			this.C_GroupBox.Text = "Please Select a Database to open:";
			// 
			// C_Databases
			// 
			this.C_Databases.CheckOnClick = true;
			this.C_Databases.Location = new System.Drawing.Point(8, 32);
			this.C_Databases.Name = "C_Databases";
			this.C_Databases.Size = new System.Drawing.Size(744, 412);
			this.C_Databases.TabIndex = 2;
			this.C_Databases.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.C_Databases_ItemCheck);
			// 
			// ConfigDBCatalog
			// 
			this.Controls.Add(this.C_GroupBox);
			this.Name = "ConfigDBCatalog";
			this.C_GroupBox.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region static members
		static bool IsCatalogsLoaded = false;
		static DataTable schemaTable;
		static internal readonly string SQLConnString4OLE = "Provider=SQLOLEDB;workstation id=\".\";packet size=4096;integrated security=SSPI;persist security info=True;data source=\".\\SQLEXPRESS\"";//initial catalog={0}";
		static private DataTable LoadCatalogs()
		{
			if(IsCatalogsLoaded&&schemaTable!=null) return schemaTable;
			OleDbConnection m_conn = new OleDbConnection(SQLConnString4OLE);
			try
			{
				m_conn.Open();
				schemaTable = m_conn.GetOleDbSchemaTable(OleDbSchemaGuid.Catalogs,null);
				IsCatalogsLoaded = true;
				return schemaTable;
			}
			catch(Exception ex)
			{
				MessageBox.Show("Load database error. Please contact Webb for help. Message:{0}",ex.Message);
				return null;
			}
			finally			
			{
				m_conn.Close();	
				m_conn.Dispose();
			}
		}
		#endregion

		public override bool ValidateSetting()
		{
			//base.OnValidated (e);
			if(this.C_Databases.CheckedItems.Count>0)
			{
				return true;
			}
			else
			{
				MessageBoxEx.ShowWarning(this.ParentForm,"You must select 1 database.");
				return false;
			}
		}

		public bool SetDatabases()
		{
			Webb.Utilities.WaitingForm.ShowWaitingForm();
			this.C_Databases.Items.Clear();
			DataTable m_Catalogs = LoadCatalogs();
			if(m_Catalogs==null)
			{
				Webb.Utilities.WaitingForm.CloseWaitingForm();
				return false;
			}
			if(m_Catalogs.Rows.Count<=0)
			{
				MessageBox.Show("There is no database in the SQL Server.");
				Webb.Utilities.WaitingForm.CloseWaitingForm();
				return false;
			}
			foreach(DataRow m_row in m_Catalogs.Rows)
			{
				string m_DBName = m_row[0].ToString();
				this.C_Databases.Items.Add(m_DBName);
			}
			Webb.Utilities.WaitingForm.CloseWaitingForm();
			return true;
		}

		private void C_Databases_ItemCheck(object sender, System.Windows.Forms.ItemCheckEventArgs e)
		{
			for(int i= 0; i<this.C_Databases.Items.Count;i++)
			{
				if(i == e.Index)
					continue;
				this.C_Databases.SetItemChecked(i,false);
			}
		}

	}
}

