using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;

using Webb.Collections;

namespace Webb.Reports.DataProvider.UI
{
	public class ConfigFilters : Webb.Utilities.Wizards.WinzardControlBase
	{
		public System.Windows.Forms.CheckedListBox C_Filters;
		private System.Windows.Forms.GroupBox C_GroupBox;
		private System.ComponentModel.IContainer components = null;

		//Scott@2007-11-14 16:45 modified some of the following code.
		private FilterInfoCollection _FilterInfoCollection = new FilterInfoCollection();
		private DataTable _AllFilterTable = null;

		public ConfigFilters()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();
			this.WizardTitle = "Step 4: Choose filters";	//11-12-2007@Scott
			this.LastControl = true;
			this.FinishControl = true;
			this.SelectStep = true;
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
			this.C_Filters = new System.Windows.Forms.CheckedListBox();
			this.C_GroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// C_GroupBox
			// 
			this.C_GroupBox.Controls.Add(this.C_Filters);
			this.C_GroupBox.Location = new System.Drawing.Point(8, 0);
			this.C_GroupBox.Name = "C_GroupBox";
			this.C_GroupBox.Size = new System.Drawing.Size(768, 464);
			this.C_GroupBox.TabIndex = 5;
			this.C_GroupBox.TabStop = false;
			this.C_GroupBox.Text = "Select Filters for Report:";
			// 
			// C_Filters
			// 
			this.C_Filters.CheckOnClick = true;
			this.C_Filters.Location = new System.Drawing.Point(8, 32);
			this.C_Filters.Name = "C_Filters";
			this.C_Filters.Size = new System.Drawing.Size(744, 412);
			this.C_Filters.TabIndex = 2;
			// 
			// ConfigFilters
			// 
			this.Controls.Add(this.C_GroupBox);
			this.Name = "ConfigFilters";
			this.C_GroupBox.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		public void SetFilters(DataTable i_Table)
		{
			this._AllFilterTable = i_Table;
			this.C_Filters.Items.Clear();
			if(i_Table is DBWebbVictory.AutoCutupTableDataTable)
			{
				this.SetFilters_Victory(i_Table as DBWebbVictory.AutoCutupTableDataTable);
				return;
			}
			else
			{
				//none
			}
		}

		private void SetFilters_Victory(DBWebbVictory.AutoCutupTableDataTable i_Table)
		{
			foreach(DataRow m_Row in i_Table.Rows)
			{
				string m_FilterValue = string.Format("{0}:{1}",
					m_Row[i_Table.CutupIDColumn.Caption],
					m_Row[i_Table.CutupNameColumn.Caption]);
				this.C_Filters.Items.Add(m_FilterValue);
			}
		}

		public override bool ValidateSetting()
		{
			//add validate code
			return true;
		}

		//Scott@2007-11-15 13:44 modified some of the following code.
		public override void ResetControl()
		{
			//base.ResetControl ();
			//Scott@2007-11-16 09:39 modified some of the following code.
			for(int i= 0; i<this.C_Filters.Items.Count;i++)
			{
				this.C_Filters.SetItemChecked(i,false);
			}
			if(this._AllFilterTable != null)
				this._AllFilterTable.Clear();
			if(this._FilterInfoCollection != null)
				this._FilterInfoCollection.Clear();
		}

		public Int32Collection GetSelectedFilterIDs()
		{
			Int32Collection m_Filters = new Int32Collection();
			foreach(object m_SelectedObj in this.C_Filters.CheckedItems)
			{
				string m_Value = m_SelectedObj.ToString();
				string m_ID = m_Value.Substring(0,m_Value.IndexOf(':',0));
				try
				{
					m_Filters.Add(Convert.ToInt32(m_ID));
				}
				catch{}
			}
			return m_Filters;
		}

		//Scott@2007-11-19 08:58 modified some of the following code.
		public override void UpdateData(object i_Data)
		{
			//base.UpdateData(i_Data);
			DBSourceConfig m_DBConfig = i_Data as DBSourceConfig;
			m_DBConfig.FilterIDs = this.GetSelectedFilterIDs();
		}

		//Scott@2007-11-30 12:52 modified some of the following code.
		public override void OnSelectAll()
		{
			//base.OnSelectAll ();
			for(int i = 0;i<this.C_Filters.Items.Count;i++)
			{
				this.C_Filters.SetItemChecked(i,true);
			}
		}

		public override void OnClearAll()
		{
			//base.OnClearAll ();
			for(int i = 0;i<this.C_Filters.Items.Count;i++)
			{
				this.C_Filters.SetItemChecked(i,false);
			}
		}
	}
}

