using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Webb;
using Webb.Data;
using Webb.Utilities;
using Webb.Collections;
using Webb.Utilities.Wizards;

namespace Webb.Reports.DataProvider.UI
{
	public class GameSelectorForm : Webb.Utilities.Wizards.WizardBaseForm
	{
		private System.ComponentModel.IContainer components = null;
		private UI.ConfigGames _ConfigGames = null;
		private WebbDataProvider _DataProvider = null;

		public GameSelectorForm(WebbDataProvider i_DataProvider)
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			this._DataProvider = i_DataProvider;
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
			// 
			// GameSelectorForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(7, 15);
			this.ClientSize = new System.Drawing.Size(554, 375);
			this.Name = "GameSelectorForm";
			this.Load += new System.EventHandler(this.GameSelectorForm_Load);

		}
		#endregion

		private void SetWizardStatus()
		{
			this.WizardStatus = 0;

			this.WizardStatus |= WizardStatus.OK | WizardStatus.Cancel;
		}

		private void GameSelectorForm_Load(object sender, System.EventArgs e)
		{
			this.SetWizardStatus();

			this._ConfigGames = new ConfigGames();
			
			this._ConfigGames.WizardTitle = "Select Games:";

			try
			{
				this._ConfigGames.SetGames(this._DataProvider.LoadWebbGames());
			}
			catch
			{
				MessageBox.Show("Please config data provider.");
			}

			this.LoadControl(this._ConfigGames);
		}

		public override void OnOK()
		{
			//base.OnOK ();
			try
			{
				this._ConfigGames.UpdateData(this._DataProvider.DBSourceConfig);
			}
			catch
			{
				MessageBox.Show("Please config data provider.");
				this.DialogResult = DialogResult.Cancel;
			}
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

	}
}

