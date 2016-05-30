/***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:ConfigGames.cs
 * Author:Country.Wu [EMail:Webb.Country.Wu@163.com]
 * Create Time:11/9/2007 09:24:36 AM
 * Copyright:1986-2007@Webb Electronics all right reserved.
 * Purpose:
 * ***********************************************************************/
#region History
/*
 * //Author@DateTime : Description
 * */
#endregion History

using System;
using System.Data;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
//
using Webb.Collections;
using Webb.Utilities;

namespace Webb.Reports.DataProvider.UI
{
	public class ConfigGames : Webb.Utilities.Wizards.WinzardControlBase
	{
		public System.Windows.Forms.CheckedListBox C_Games;
		private System.ComponentModel.IContainer components = null;

		//Scott@2007-11-14 13:04 modified some of the following code.
		private GameInfoCollection _GameInfoCollection = new GameInfoCollection();
        private DataTable _AllGameTable = null;
		public System.Windows.Forms.CheckedListBox C_Edls;
		private System.Windows.Forms.GroupBox Group_Games;
		private System.Windows.Forms.GroupBox Group_Edls;
	

		public ConfigGames()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();
			this.WizardTitle = "Step 3: Choose games";	//11-12-2007@Scott
			this.FinishControl = true;
			this.SelectStep = true;
			this.LastControl = true;	//Modified at 2008-10-10 11:22:05@Scott
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
			this.Group_Games = new System.Windows.Forms.GroupBox();
			this.C_Games = new System.Windows.Forms.CheckedListBox();
			this.Group_Edls = new System.Windows.Forms.GroupBox();
			this.C_Edls = new System.Windows.Forms.CheckedListBox();
			this.Group_Games.SuspendLayout();
			this.Group_Edls.SuspendLayout();
			this.SuspendLayout();
			// 
			// Group_Games
			// 
			this.Group_Games.Controls.Add(this.C_Games);
			this.Group_Games.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Group_Games.Location = new System.Drawing.Point(0, 0);
			this.Group_Games.Name = "Group_Games";
			this.Group_Games.Size = new System.Drawing.Size(790, 480);
			this.Group_Games.TabIndex = 4;
			this.Group_Games.TabStop = false;
			this.Group_Games.Text = "Select Games for Report:";
			// 
			// C_Games
			// 
			this.C_Games.CheckOnClick = true;
			this.C_Games.Dock = System.Windows.Forms.DockStyle.Fill;
			this.C_Games.Location = new System.Drawing.Point(3, 18);
			this.C_Games.Name = "C_Games";
			this.C_Games.Size = new System.Drawing.Size(784, 446);
			this.C_Games.TabIndex = 2;
			// 
			// Group_Edls
			// 
			this.Group_Edls.Controls.Add(this.C_Edls);
			this.Group_Edls.Dock = System.Windows.Forms.DockStyle.Right;
			this.Group_Edls.Location = new System.Drawing.Point(366, 0);
			this.Group_Edls.Name = "Group_Edls";
			this.Group_Edls.Size = new System.Drawing.Size(424, 480);
			this.Group_Edls.TabIndex = 5;
			this.Group_Edls.TabStop = false;
			this.Group_Edls.Text = "Select Cutups for Report:";
			// 
			// C_Edls
			// 
			this.C_Edls.CheckOnClick = true;
			this.C_Edls.Dock = System.Windows.Forms.DockStyle.Fill;
			this.C_Edls.Location = new System.Drawing.Point(3, 18);
			this.C_Edls.Name = "C_Edls";
			this.C_Edls.Size = new System.Drawing.Size(418, 446);
			this.C_Edls.TabIndex = 2;
			// 
			// ConfigGames
			// 
			this.Controls.Add(this.Group_Edls);
			this.Controls.Add(this.Group_Games);
			this.Name = "ConfigGames";
			this.Group_Games.ResumeLayout(false);
			this.Group_Edls.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		public void SetGames(DataTable i_Table)
		{
			//Scott@2007-11-14 13:12 modified some of the following code.
			this._AllGameTable = i_Table;
			this.C_Games.Items.Clear();
			if(i_Table is DBWebbReportCommon.GamesDataTable)
			{
				this.SetGames_Common(i_Table as DBWebbReportCommon.GamesDataTable);
				
				return;
			}
			if(i_Table is DBWebbVictory.GameDataTable)
			{
				this.SetGames_Victory(i_Table as DBWebbVictory.GameDataTable);
				return;
			}
			#region Modified Area
			if(i_Table is DBWebbAdvantage.GamesDataTable)
			{
				this.SetGames_Advantage(i_Table as DBWebbAdvantage.GamesDataTable);
				return;
			}
			#endregion        //Modify at 2008-10-9 16:51:28@Scott
		}


		public void SetEdls(DataTable i_Table)
		{
			this.C_Edls.Items.Clear();

			if(i_Table==null||i_Table.Columns.Count!=2)
			{
				this.Group_Edls.Visible=false;
				return;
			}
			this.Group_Edls.Visible=true;
			foreach(DataRow m_Row in i_Table.Rows)
			{
                string m_Value = string.Format("{0}   (CutupName:  '{1}')",
					m_Row[0],
					m_Row[1]);
				this.C_Edls.Items.Add(m_Value);
			}


		}
		private void SetGames_Common(DBWebbReportCommon.GamesDataTable i_Table)
		{
			foreach(DataRow m_Row in i_Table.Rows)
			{
				string m_Value = string.Format("{0}:{1} vs {2} - {3}",
					m_Row[i_Table.GameIDColumn.Caption],
					m_Row[i_Table.ObjectColumn.Caption],
					m_Row[i_Table.OpponentColumn.Caption],
					m_Row[i_Table.DateColumn.Caption]);
				this.C_Games.Items.Add(m_Value);
			}
		}

		private void SetGames_Victory(DBWebbVictory.GameDataTable i_Table)
		{
			foreach(DataRow m_Row in i_Table.Rows)
			{
                string m_Value = string.Format("{0}   (GameName:  '{1}')",
                 m_Row[i_Table.GameIDColumn.Caption],
                 m_Row[i_Table.GameNameColumn.Caption]
                 , m_Row[i_Table.DateColumn.Caption]
                 );
				this.C_Games.Items.Add(m_Value);
			}
		}

		private void SetGames_Advantage(DBWebbAdvantage.GamesDataTable i_Table)
		{
			foreach(DataRow m_Row in i_Table.Rows)
			{
				string m_Value = string.Format("{0}:{1} VS {2} AT {3} ON {4} - {5}",
					m_Row[i_Table.GameIDColumn.Caption],
					m_Row[i_Table.ObjectColumn.Caption],
					m_Row[i_Table.OpponentColumn.Caption],
					m_Row[i_Table.LocationColumn.Caption],
					m_Row[i_Table.DateColumn.Caption],
					m_Row[i_Table.ScoutTypeIDColumn.Caption]);
				this.C_Games.Items.Add(m_Value);
			}
		}

//		private void SetDataForSQL(DS_WebbFootball_SQL.GamesDataTable i_Table)
//		{
//			foreach(DataRow m_Row in i_Table.Rows)
//			{
//				string m_Value = string.Format("{0}:{1} vs {2} - {3}",
//					m_Row[i_Table.GameIDColumn.Caption],
//					m_Row[i_Table.ObjectColumn.Caption],
//					m_Row[i_Table.OpponentColumn.Caption],
//					m_Row[i_Table.DateColumn.Caption]);
//				this.C_Games.Items.Add(m_Value);
//			}
//		}

		public override bool ValidateSetting()
		{
			if(this.C_Games.CheckedItems.Count>0||this.C_Edls.CheckedItems.Count>0)
			{
				return true;
			}
			else
			{
				MessageBoxEx.ShowWarning(this.ParentForm,"You must at least select 1 game or edl.");
				return false;
			}
		}

		//Scott@2007-11-15 13:39 modified some of the following code.
		public override void ResetControl()
		{
			//base.ResetControl ();
			if(this._AllGameTable != null)
				this._AllGameTable.Clear();
			if(this._GameInfoCollection != null)
				this._GameInfoCollection.Clear();
		}

		public Int32Collection GetSelectedGameIDs()
		{
			Int32Collection m_GameIDs = new Int32Collection();
			foreach(object m_SelectedObj in this.C_Games.CheckedItems)
			{
				string m_Value = m_SelectedObj.ToString();
				string m_ID = m_Value.Substring(0,m_Value.IndexOf(' ',0));
				try
				{
					m_GameIDs.Add(Convert.ToInt32(m_ID));
				}
				catch{}
			}
			return m_GameIDs;
		}
		public StringCollection GetSelectedEdlIDs()  //2009-6-22 14:08:33@Simon Add this Code
		{
			StringCollection m_EdlIDs = new StringCollection();
			foreach(object m_SelectedObj in this.C_Edls.CheckedItems)
			{
				string m_Value = m_SelectedObj.ToString();
				string m_ID = m_Value.Substring(0,m_Value.IndexOf(' ',0)).Trim();	
				if(m_ID==string.Empty)continue;
				m_EdlIDs.Add(m_ID);
				
				
			}
			return m_EdlIDs;
		}

		//Scott@2007-11-19 08:47 modified some of the following code.
		public override void UpdateData(object i_Data)
		{
			//base.UpdateData(i_Data);
			DBSourceConfig m_DBConfig = i_Data as DBSourceConfig;
			m_DBConfig.GameIDs = this.GetSelectedGameIDs();
            m_DBConfig.Edls = this.GetSelectedEdlIDs();
		}

		//Scott@2007-11-30 12:52 modified some of the following code.
		public override void OnSelectAll()
		{
			//base.OnSelectAll ();
			for(int i = 0;i<this.C_Games.Items.Count;i++)
			{
				this.C_Games.SetItemChecked(i,true);
			}
		}

		public override void OnClearAll()
		{
			//base.OnClearAll ();
			for(int i = 0;i<this.C_Games.Items.Count;i++)
			{
				this.C_Games.SetItemChecked(i,false);
			}
		}
	}
}

