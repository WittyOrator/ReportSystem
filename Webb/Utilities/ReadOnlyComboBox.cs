using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace Webb.Utilities
{
	/// <summary>
	/// Summary description for ReadOnlyComboBox.
	/// </summary>
	public class ReadOnlyComboBox : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.TextBox txtCombo;
		private System.Windows.Forms.ListBox lstCombo;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ReadOnlyComboBox()
		{
			// This call is required by the Windows.Forms Form Designer.
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
			this.txtCombo = new System.Windows.Forms.TextBox();
			this.lstCombo = new System.Windows.Forms.ListBox();
			this.SuspendLayout();
			// 
			// txtCombo
			// 
			this.txtCombo.Dock = System.Windows.Forms.DockStyle.Top;
			this.txtCombo.Location = new System.Drawing.Point(0, 0);
			this.txtCombo.Name = "txtCombo";
			this.txtCombo.ReadOnly = true;
			this.txtCombo.Size = new System.Drawing.Size(240, 20);
			this.txtCombo.TabIndex = 0;
			this.txtCombo.Text = "";
			// 
			// lstCombo
			// 
			this.lstCombo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstCombo.Location = new System.Drawing.Point(0, 20);
			this.lstCombo.Name = "lstCombo";
			this.lstCombo.Size = new System.Drawing.Size(240, 186);
			this.lstCombo.TabIndex = 1;
			this.lstCombo.SelectedIndexChanged += new System.EventHandler(this.lstCombo_SelectedIndexChanged);
			// 
			// ReadOnlyComboBox
			// 
			this.Controls.Add(this.lstCombo);
			this.Controls.Add(this.txtCombo);
			this.Name = "ReadOnlyComboBox";
			this.Size = new System.Drawing.Size(240, 216);
			this.ResumeLayout(false);

		}
		#endregion

		private void lstCombo_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.lstCombo.SelectedIndex>=0)
			{
				this.txtCombo.Text=this.lstCombo.SelectedItem.ToString();
			}
		}
	}
}
