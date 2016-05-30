using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;

namespace Webb.Reports.DataProvider.UI
{
	public class ConfigTables : Webb.Utilities.Wizards.WinzardControlBase
	{
		private System.Windows.Forms.GroupBox C_GroupBox;
		public System.Windows.Forms.CheckedListBox C_Tables;
		private System.ComponentModel.IContainer components = null;

		public ConfigTables()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();
			this.WizardTitle = "Step 3: Choose tables";
			//Scott@2007-11-14 15:04 modified some of the following code.
			this.LastControl = true;
			this.FinishControl = true;
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
			this.C_Tables = new System.Windows.Forms.CheckedListBox();
			this.C_GroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// C_GroupBox
			// 
			this.C_GroupBox.Controls.Add(this.C_Tables);
			this.C_GroupBox.Location = new System.Drawing.Point(8, 0);
			this.C_GroupBox.Name = "C_GroupBox";
			this.C_GroupBox.Size = new System.Drawing.Size(768, 472);
			this.C_GroupBox.TabIndex = 5;
			this.C_GroupBox.TabStop = false;
			this.C_GroupBox.Text = "Please Select Tables:";
			// 
			// C_Tables
			// 
			this.C_Tables.CheckOnClick = true;
			this.C_Tables.Location = new System.Drawing.Point(8, 32);
			this.C_Tables.Name = "C_Tables";
			this.C_Tables.Size = new System.Drawing.Size(752, 429);
			this.C_Tables.TabIndex = 2;
			// 
			// ConfigTables
			// 
			this.Controls.Add(this.C_GroupBox);
			this.Name = "ConfigTables";
			this.C_GroupBox.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		public override void SetData(object i_Data)
		{
			DataTable i_Table = i_Data as DataTable;
			this.C_Tables.Items.Clear();
			foreach(DataRow m_Row in i_Table.Rows)
			{
				this.C_Tables.Items.Add(m_Row["TABLE_NAME"]);
			}
		}

		public override void UpdateData(object i_Data)
		{
			//base.UpdateData (i_Data);
		}

		public override bool ValidateSetting()
		{
			//return base.ValidateSetting ();
			if(this.C_Tables.CheckedItems.Count<=0)
			{
				MessageBox.Show("You must at least select 1 table to run your reports.");
				return false;
			}
			return true;
		}

		public override void ResetControl()
		{
			//base.ResetControl ();
		}

		public string[] GetSelectedTables()
		{
			string[] m_Tables = new string[this.C_Tables.CheckedItems.Count];
			for(int i = 0; i<this.C_Tables.CheckedItems.Count;i++)
			{
				m_Tables[i] = this.C_Tables.CheckedItems[i].ToString();
			}
			return m_Tables;
		}

		public void SetTables(DataTable i_Table)
		{
			this.C_Tables.Items.Clear();
			if(i_Table!=null)
			{
				foreach(DataRow m_Row in i_Table.Rows)
				{
					this.C_Tables.Items.Add(m_Row["TABLE_NAME"]);
				}
			}
		}

		//Scott@2007-11-14 15:04 modified some of the following code.
		public void SetTables(DataSet i_ds)
		{
			this.C_Tables.Items.Clear();
			if(i_ds!=null)
			{
				foreach(DataTable m_dt in i_ds.Tables)
				{
					this.C_Tables.Items.Add(m_dt.TableName);
				}
			}
		}
	}
}

