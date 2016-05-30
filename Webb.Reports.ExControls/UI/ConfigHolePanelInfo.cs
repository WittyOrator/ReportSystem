using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Reflection;

using Webb.Reports.ExControls.Views;
using Webb.Reports.ExControls.Data;

namespace Webb.Reports.ExControls.UI
{
	public class ConfigHolePanelInfo : Webb.Reports.ExControls.UI.ExControlDesignerControlBase
	{
		private System.Windows.Forms.PropertyGrid C_PropertyGrid;
		private System.ComponentModel.IContainer components = null;

		public ConfigHolePanelInfo()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

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
			this.C_PropertyGrid = new System.Windows.Forms.PropertyGrid();
			this.SuspendLayout();
			// 
			// C_PropertyGrid
			// 
			this.C_PropertyGrid.CommandsVisibleIfAvailable = true;
			this.C_PropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.C_PropertyGrid.LargeButtons = false;
			this.C_PropertyGrid.LineColor = System.Drawing.SystemColors.Control;
			this.C_PropertyGrid.Location = new System.Drawing.Point(0, 0);
			this.C_PropertyGrid.Name = "C_PropertyGrid";
			this.C_PropertyGrid.Size = new System.Drawing.Size(584, 336);
			this.C_PropertyGrid.TabIndex = 3;
			this.C_PropertyGrid.Text = "propertyGrid1";
			this.C_PropertyGrid.ViewBackColor = System.Drawing.SystemColors.Window;
			this.C_PropertyGrid.ViewForeColor = System.Drawing.SystemColors.WindowText;
			// 
			// ConfigHolePanelInfo
			// 
			this.Controls.Add(this.C_PropertyGrid);
			this.Name = "ConfigHolePanelInfo";
			this.Size = new System.Drawing.Size(584, 336);
			this.ResumeLayout(false);

		}
		#endregion
		
		public override void UpdateView(Webb.Reports.ExControls.Views.ExControlView i_View)
		{
			HolePanelView mainView = i_View as HolePanelView;
		}

		public override void SetView(Webb.Reports.ExControls.Views.ExControlView i_View)
		{
			HolePanelView mainView = i_View as HolePanelView;

			this.C_PropertyGrid.SelectedObject = mainView.HolePanelInfo;
		}
	}
}