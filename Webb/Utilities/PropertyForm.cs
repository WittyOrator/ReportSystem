using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Webb.Utilities
{
	/// <summary>
	/// Summary description for PropertyForm.
	/// </summary>
	public class PropertyForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Panel C_PanelControl;
		private System.Windows.Forms.Splitter C_Splitter;
		private System.Windows.Forms.Panel C_PanelMain;
		private System.Windows.Forms.Button C_BtnCancel;
		private System.Windows.Forms.Button C_BtnOK;
		private System.Windows.Forms.PropertyGrid C_PropertyGrid;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;



		public PropertyForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.C_PanelControl = new System.Windows.Forms.Panel();
			this.C_Splitter = new System.Windows.Forms.Splitter();
			this.C_PanelMain = new System.Windows.Forms.Panel();
			this.C_PropertyGrid = new System.Windows.Forms.PropertyGrid();
			this.C_BtnCancel = new System.Windows.Forms.Button();
			this.C_BtnOK = new System.Windows.Forms.Button();
			this.C_PanelControl.SuspendLayout();
			this.C_PanelMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// C_PanelControl
			// 
			this.C_PanelControl.Controls.Add(this.C_BtnCancel);
			this.C_PanelControl.Controls.Add(this.C_BtnOK);
			this.C_PanelControl.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.C_PanelControl.Location = new System.Drawing.Point(0, 262);
			this.C_PanelControl.Name = "C_PanelControl";
			this.C_PanelControl.Size = new System.Drawing.Size(280, 48);
			this.C_PanelControl.TabIndex = 0;
			// 
			// C_Splitter
			// 
			this.C_Splitter.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.C_Splitter.Location = new System.Drawing.Point(0, 259);
			this.C_Splitter.Name = "C_Splitter";
			this.C_Splitter.Size = new System.Drawing.Size(280, 3);
			this.C_Splitter.TabIndex = 1;
			this.C_Splitter.TabStop = false;
			// 
			// C_PanelMain
			// 
			this.C_PanelMain.Controls.Add(this.C_PropertyGrid);
			this.C_PanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.C_PanelMain.Location = new System.Drawing.Point(0, 0);
			this.C_PanelMain.Name = "C_PanelMain";
			this.C_PanelMain.Size = new System.Drawing.Size(280, 259);
			this.C_PanelMain.TabIndex = 2;
			// 
			// C_PropertyGrid
			// 
			this.C_PropertyGrid.CommandsVisibleIfAvailable = true;
			this.C_PropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.C_PropertyGrid.LargeButtons = false;
			this.C_PropertyGrid.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.C_PropertyGrid.Location = new System.Drawing.Point(0, 0);
			this.C_PropertyGrid.Name = "C_PropertyGrid";
			this.C_PropertyGrid.Size = new System.Drawing.Size(280, 259);
			this.C_PropertyGrid.TabIndex = 0;
			this.C_PropertyGrid.Text = "propertyGrid1";
			this.C_PropertyGrid.ViewBackColor = System.Drawing.SystemColors.Window;
			this.C_PropertyGrid.ViewForeColor = System.Drawing.SystemColors.WindowText;
			// 
			// C_BtnCancel
			// 
			this.C_BtnCancel.Location = new System.Drawing.Point(192, 8);
			this.C_BtnCancel.Name = "C_BtnCancel";
			this.C_BtnCancel.TabIndex = 0;
			this.C_BtnCancel.Text = "Cancel";
			this.C_BtnCancel.Click += new System.EventHandler(this.C_BtnCancel_Click);
			// 
			// C_BtnOK
			// 
			this.C_BtnOK.Location = new System.Drawing.Point(112, 8);
			this.C_BtnOK.Name = "C_BtnOK";
			this.C_BtnOK.TabIndex = 0;
			this.C_BtnOK.Text = "OK";
			this.C_BtnOK.Click += new System.EventHandler(this.C_BtnOK_Click);
			// 
			// PropertyForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(280, 310);
			this.ControlBox = false;
			this.Controls.Add(this.C_PanelMain);
			this.Controls.Add(this.C_Splitter);
			this.Controls.Add(this.C_PanelControl);
			this.Name = "PropertyForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Property";
			this.C_PanelControl.ResumeLayout(false);
			this.C_PanelMain.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void C_BtnOK_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.OK;

			this.Close();
		}

		private void C_BtnCancel_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;

			this.Close();
		}

		public object Object
		{
			get{return this.C_PropertyGrid.SelectedObject;}

			set{this.C_PropertyGrid.SelectedObject = value;}
		}

		public string Title
		{
			get{return this.Text;}

			set{this.Text = value;}
		}

		public void BindProperty(string title , object value)
		{
			this.Title = title;

			this.Object = value;
		}

		public void BindProperty(object value)
		{
			this.Title = "Property";

			this.Object = value;
		}
	}
}
