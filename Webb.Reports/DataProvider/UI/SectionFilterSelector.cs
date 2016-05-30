using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;

using Webb.Collections;

namespace Webb.Reports.DataProvider.UI
{
	/// <summary>
	/// Summary description for SectionFilterSelector.
	/// </summary>
	public class SectionFilterSelector : Webb.Utilities.Wizards.WinzardControlBase
	{
		private System.Windows.Forms.GroupBox C_GroupBox;
		private System.Windows.Forms.ListBox C_FilterList;
		private System.Windows.Forms.ListBox C_SelectedFilterList;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private FilterInfoCollection _FilterInfoCollection = new FilterInfoCollection();
		private System.Windows.Forms.Button C_MoveUpBtn;
		private System.Windows.Forms.Button C_MoveDownBtn;
		private System.Windows.Forms.Button C_ClearBtn;
		private System.Windows.Forms.Button C_AddBtn;
		private System.Windows.Forms.Button C_DelBtn;
		private DataTable _AllFilterTable = null;

		public SectionFilterSelector()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			
			this.WizardTitle = "Choose section filters";

			// TODO: Add any initialization after the InitializeComponent call

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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.C_GroupBox = new System.Windows.Forms.GroupBox();
			this.C_DelBtn = new System.Windows.Forms.Button();
			this.C_AddBtn = new System.Windows.Forms.Button();
			this.C_ClearBtn = new System.Windows.Forms.Button();
			this.C_MoveDownBtn = new System.Windows.Forms.Button();
			this.C_MoveUpBtn = new System.Windows.Forms.Button();
			this.C_SelectedFilterList = new System.Windows.Forms.ListBox();
			this.C_FilterList = new System.Windows.Forms.ListBox();
			this.C_GroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// C_GroupBox
			// 
			this.C_GroupBox.Controls.Add(this.C_DelBtn);
			this.C_GroupBox.Controls.Add(this.C_AddBtn);
			this.C_GroupBox.Controls.Add(this.C_ClearBtn);
			this.C_GroupBox.Controls.Add(this.C_MoveDownBtn);
			this.C_GroupBox.Controls.Add(this.C_MoveUpBtn);
			this.C_GroupBox.Controls.Add(this.C_SelectedFilterList);
			this.C_GroupBox.Controls.Add(this.C_FilterList);
			this.C_GroupBox.Location = new System.Drawing.Point(8, 0);
			this.C_GroupBox.Name = "C_GroupBox";
			this.C_GroupBox.Size = new System.Drawing.Size(520, 280);
			this.C_GroupBox.TabIndex = 6;
			this.C_GroupBox.TabStop = false;
			this.C_GroupBox.Text = "Select Filters for Report:";
			// 
			// C_DelBtn
			// 
			this.C_DelBtn.Location = new System.Drawing.Point(233, 146);
			this.C_DelBtn.Name = "C_DelBtn";
			this.C_DelBtn.Size = new System.Drawing.Size(50, 23);
			this.C_DelBtn.TabIndex = 6;
			this.C_DelBtn.Text = "<=";
			this.C_DelBtn.Click += new System.EventHandler(this.C_DelBtn_Click);
			// 
			// C_AddBtn
			// 
			this.C_AddBtn.Location = new System.Drawing.Point(233, 98);
			this.C_AddBtn.Name = "C_AddBtn";
			this.C_AddBtn.Size = new System.Drawing.Size(50, 23);
			this.C_AddBtn.TabIndex = 5;
			this.C_AddBtn.Text = "=>";
			this.C_AddBtn.Click += new System.EventHandler(this.C_AddBtn_Click);
			// 
			// C_ClearBtn
			// 
			this.C_ClearBtn.Location = new System.Drawing.Point(445, 248);
			this.C_ClearBtn.Name = "C_ClearBtn";
			this.C_ClearBtn.Size = new System.Drawing.Size(72, 23);
			this.C_ClearBtn.TabIndex = 4;
			this.C_ClearBtn.Text = "Clear All";
			this.C_ClearBtn.Click += new System.EventHandler(this.C_ClearBtn_Click);
			// 
			// C_MoveDownBtn
			// 
			this.C_MoveDownBtn.Location = new System.Drawing.Point(354, 248);
			this.C_MoveDownBtn.Name = "C_MoveDownBtn";
			this.C_MoveDownBtn.Size = new System.Drawing.Size(90, 23);
			this.C_MoveDownBtn.TabIndex = 3;
			this.C_MoveDownBtn.Text = "Move Down";
			this.C_MoveDownBtn.Click += new System.EventHandler(this.C_MoveDownBtn_Click);
			// 
			// C_MoveUpBtn
			// 
			this.C_MoveUpBtn.Location = new System.Drawing.Point(281, 248);
			this.C_MoveUpBtn.Name = "C_MoveUpBtn";
			this.C_MoveUpBtn.Size = new System.Drawing.Size(72, 23);
			this.C_MoveUpBtn.TabIndex = 2;
			this.C_MoveUpBtn.Text = "Move Up";
			this.C_MoveUpBtn.Click += new System.EventHandler(this.C_MoveUpBtn_Click);
			// 
			// C_SelectedFilterList
			// 
			this.C_SelectedFilterList.HorizontalScrollbar = true;
			this.C_SelectedFilterList.ItemHeight = 14;
			this.C_SelectedFilterList.Location = new System.Drawing.Point(289, 24);
			this.C_SelectedFilterList.Name = "C_SelectedFilterList";
			this.C_SelectedFilterList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.C_SelectedFilterList.Size = new System.Drawing.Size(220, 214);
			this.C_SelectedFilterList.TabIndex = 1;
			this.C_SelectedFilterList.DoubleClick += new System.EventHandler(this.C_SelectedFilterList_DoubleClick);
			// 
			// C_FilterList
			// 
			this.C_FilterList.HorizontalScrollbar = true;
			this.C_FilterList.ItemHeight = 14;
			this.C_FilterList.Location = new System.Drawing.Point(8, 24);
			this.C_FilterList.Name = "C_FilterList";
			this.C_FilterList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.C_FilterList.Size = new System.Drawing.Size(220, 242);
			this.C_FilterList.TabIndex = 0;
			this.C_FilterList.DoubleClick += new System.EventHandler(this.C_FilterList_DoubleClick);
			// 
			// SectionFilterSelector
			// 
			this.Controls.Add(this.C_GroupBox);
			this.Name = "SectionFilterSelector";
			this.C_GroupBox.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		public void SetFilters(DataTable i_Table)
		{
			this._AllFilterTable = i_Table;

			this.C_FilterList.Items.Clear();
			
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

				this.C_FilterList.Items.Add(m_FilterValue);
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
			this.C_FilterList.Items.Clear();

			this.C_SelectedFilterList.Items.Clear();
			
			if(this._AllFilterTable != null) this._AllFilterTable.Clear();
				
			if(this._FilterInfoCollection != null) this._FilterInfoCollection.Clear();
		}

		private int GetListItemID(object i_Item)
		{
			string m_Value = i_Item.ToString();

			string m_IDStr = m_Value.Substring(0,m_Value.IndexOf(':',0));

			int m_ID = -1;

			try
			{
				m_ID = Convert.ToInt32(m_IDStr);
			}
			catch
			{
				return m_ID;
			}
			return m_ID;
		}

		public Int32Collection GetSelectedFilterIDs()
		{
			Int32Collection m_Filters = new Int32Collection();

//			foreach(object m_SelectedObj in this.C_SelectedFilterList.Items)
//			{				
//				m_Filters.Add(this.GetListItemID(m_SelectedObj));
//			}
			for(int i = 0;i<this.C_SelectedFilterList.Items.Count;i++)
			{
				m_Filters.Add(this.GetListItemID(this.C_SelectedFilterList.Items[i]));
			}
			return m_Filters;
		}

		public override void UpdateData(object i_Data)
		{
			//Scott@2007-11-23 09:30 modified some of the following code.
			DBSourceConfig m_DBConfig = i_Data as DBSourceConfig;

			m_DBConfig.SectionFilterIDs = this.GetSelectedFilterIDs();
		}

		private void C_FilterList_DoubleClick(object sender, System.EventArgs e)
		{
			object m_Item = this.C_FilterList.SelectedItem;

			if(m_Item != null)
			{
				this.C_FilterList.Items.Remove(m_Item);
				
				this.C_SelectedFilterList.Items.Add(m_Item);
			}
		}

		private void C_SelectedFilterList_DoubleClick(object sender, System.EventArgs e)
		{
			object m_Item = this.C_SelectedFilterList.SelectedItem;

			if(m_Item != null)
			{
				this.C_SelectedFilterList.Items.Remove(m_Item);
				
				this.AddToListByIDs(this.C_FilterList,m_Item);
				//this.C_FilterList.Items.Add(m_Item);
			}
		}

		private object GetHotItemInSelectedList()
		{
			object m_Item = this.C_SelectedFilterList.SelectedItem;

			if(m_Item != null)
			{
				return m_Item;
			}
			else
			{
				return null;
			}
		}

		private void C_MoveUpBtn_Click(object sender, System.EventArgs e)
		{
			object m_Item = this.GetHotItemInSelectedList();

			if(m_Item == null) return;

			int m_Index = this.C_SelectedFilterList.Items.IndexOf(m_Item);

			if(m_Index > 0)
			{
				this.C_SelectedFilterList.Items.Remove(m_Item);

				this.C_SelectedFilterList.Items.Insert(m_Index - 1,m_Item);

				this.C_SelectedFilterList.SelectedItem = m_Item;
			}
		}

		private void C_MoveDownBtn_Click(object sender, System.EventArgs e)
		{
			object m_Item = this.GetHotItemInSelectedList();

			if(m_Item == null) return;

			int m_Index = this.C_SelectedFilterList.Items.IndexOf(m_Item);

			if(m_Index < this.C_SelectedFilterList.Items.Count - 1)
			{
				this.C_SelectedFilterList.Items.Remove(m_Item);

				this.C_SelectedFilterList.Items.Insert(m_Index + 1,m_Item);

				this.C_SelectedFilterList.SelectedItem = m_Item;
			}
		}

		private void C_ClearBtn_Click(object sender, System.EventArgs e)
		{
			foreach(object i_Item in this.C_SelectedFilterList.Items)
			{
				this.AddToListByIDs(this.C_FilterList,i_Item);
				//this.C_FilterList.Items.Add(i_Item);
			}

			this.C_SelectedFilterList.Items.Clear();
		}

		private void AddToListByIDs(ListBox i_ListBox,object i_Item)
		{
			int m_ItemID = this.GetListItemID(i_Item);

			if(m_ItemID < 0) return;

			int m_Index = -1,m_TempIndex = -1;

			foreach(object i_ListItem in i_ListBox.Items)
			{
				int m_ID = this.GetListItemID(i_ListItem);

				if(m_ID > m_ItemID)
				{
					m_TempIndex = i_ListBox.Items.IndexOf(i_ListItem);

					if(m_Index == -1 || m_TempIndex < m_Index)
					{
						m_Index = m_TempIndex;
					}
				}
			}

			if(m_Index < 0) m_Index = i_ListBox.Items.Count;

			i_ListBox.Items.Insert(m_Index,i_Item);
		}

		private void C_AddBtn_Click(object sender, System.EventArgs e)
		{	
			ArrayList m_Selected = new ArrayList();

			foreach(object i_Item in this.C_FilterList.SelectedItems)
			{
				m_Selected.Add(i_Item);
			}

			//prepare for focus next
			int index = 0;
			if(m_Selected.Count == 1)
			{
				index = this.C_FilterList.Items.IndexOf(m_Selected[0]);
			}

			foreach(object i_Object in m_Selected)
			{
				if(i_Object != null)
				{
					this.C_FilterList.Items.Remove(i_Object);
					
					this.C_SelectedFilterList.Items.Add(i_Object);
				}
			}

			//focus next
			if(index == this.C_FilterList.Items.Count)
			{
				index--;
			}
			if(this.C_FilterList.Items.Count>0&&index>=0)
			{
				this.C_FilterList.SelectedIndex = index;
			}
		}

		private void C_DelBtn_Click(object sender, System.EventArgs e)
		{
			ArrayList m_Selected = new ArrayList();
			
			foreach(object i_Item in this.C_SelectedFilterList.SelectedItems)
			{
				m_Selected.Add(i_Item);
			}

			//prepare for focus next
			int index = 0;
			if(m_Selected.Count == 1)
			{
				index = this.C_SelectedFilterList.Items.IndexOf(m_Selected[0]);
			}

			foreach(object i_Object in m_Selected)
			{
				if(i_Object != null)
				{
					this.C_SelectedFilterList.Items.Remove(i_Object);
				
					this.AddToListByIDs(this.C_FilterList,i_Object);
				}
			}

			//focus next
			if(index == this.C_SelectedFilterList.Items.Count)
			{
				index--;
			}
			if(this.C_SelectedFilterList.Items.Count>0&&index>=0)
			{
				this.C_SelectedFilterList.SelectedIndex = index;
			}
		}
	}

}
