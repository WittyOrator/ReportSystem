using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Webb.Reports.ExControls.UI
{
	/// <summary>
	/// Summary description for DF_StatControl.
	/// </summary>
	public class DF_MaskedText : Webb.Reports.ExControls.UI.ExControlDesignerFormBase
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private ConfigMaskedText _ConfigMaskedText;
        private ConfigGroupingLayout _ConfigGroupingLayout;
		private System.Windows.Forms.LinkLabel C_LinkSignedText;
        private LinkLabel C_Layout;
		private System.ComponentModel.Container components = null;



		public DF_MaskedText()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			this._ConfigMaskedText = new ConfigMaskedText();
            _ConfigGroupingLayout = new ConfigGroupingLayout();

			this.Load += new EventHandler(DF_MaskedTextl_Load);

			
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
            this.C_LinkSignedText = new System.Windows.Forms.LinkLabel();
            this.C_Layout = new System.Windows.Forms.LinkLabel();
            this.C_AllTask.SuspendLayout();
            this.C_LeftMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // C_MainPanel
            // 
            this.C_MainPanel.Size = new System.Drawing.Size(651, 478);
            // 
            // C_AllTask
            // 
            this.C_AllTask.Controls.Add(this.C_Layout);
            this.C_AllTask.Controls.Add(this.C_LinkSignedText);
            this.C_AllTask.Controls.SetChildIndex(this.C_LinkSignedText, 0);
            this.C_AllTask.Controls.SetChildIndex(this.C_AutoStyle, 0);
            this.C_AllTask.Controls.SetChildIndex(this.C_Layout, 0);
            // 
            // C_LeftMenu
            // 
            this.C_LeftMenu.Size = new System.Drawing.Size(176, 518);
            // 
            // C_Splitter
            // 
            this.C_Splitter.Size = new System.Drawing.Size(5, 518);
            // 
            // C_AutoStyle
            // 
            this.C_AutoStyle.Location = new System.Drawing.Point(3, 40);
            this.C_AutoStyle.Visible = false;
            // 
            // C_LinkSignedText
            // 
            this.C_LinkSignedText.Dock = System.Windows.Forms.DockStyle.Top;
            this.C_LinkSignedText.Location = new System.Drawing.Point(3, 18);
            this.C_LinkSignedText.Name = "C_LinkSignedText";
            this.C_LinkSignedText.Size = new System.Drawing.Size(162, 22);
            this.C_LinkSignedText.TabIndex = 4;
            this.C_LinkSignedText.TabStop = true;
            this.C_LinkSignedText.Text = "MaskedText Info";
            this.C_LinkSignedText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.C_LinkSignedText.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.C_LinkStatInfo_LinkClicked);
            // 
            // C_Layout
            // 
            this.C_Layout.Dock = System.Windows.Forms.DockStyle.Top;
            this.C_Layout.Location = new System.Drawing.Point(3, 72);
            this.C_Layout.Name = "C_Layout";
            this.C_Layout.Size = new System.Drawing.Size(162, 32);
            this.C_Layout.TabIndex = 5;
            this.C_Layout.TabStop = true;
            this.C_Layout.Text = "Adjust Size";
            this.C_Layout.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.C_Layout.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.C_Layout_LinkClicked);
            // 
            // DF_MaskedText
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(7, 15);
            this.ClientSize = new System.Drawing.Size(832, 518);
            this.Name = "DF_MaskedText";
            this.Text = "DF_SignedText";
            this.C_AllTask.ResumeLayout(false);
            this.C_LeftMenu.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		public override void UpdateView(Webb.Reports.ExControls.Views.ExControlView i_View)
		{
			base.UpdateView (i_View);
			
			this.ControlsUpdateView(i_View);
		}

		public override void SetView(Webb.Reports.ExControls.Views.ExControlView i_View)
		{
			base.SetView (i_View);

			this.ControlsSetView(i_View);
		}

		private void C_LinkStatInfo_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			this._ConfigMaskedText.SetView(this._ExControlView);

			this.LoadConfigControl(this._ConfigMaskedText);
		}
	
		private void ControlsUpdateView(Views.ExControlView i_View)
		{
            if (this._ConfigMaskedText.Parent != null) this._ConfigMaskedText.UpdateView(i_View);

            if (this._ConfigGroupingLayout.Parent != null) this._ConfigGroupingLayout.UpdateView(i_View);
		}

		private void ControlsSetView(Views.ExControlView i_View)
		{
			this._ConfigMaskedText.SetView(i_View);
		}

		private void DF_MaskedTextl_Load(object sender, EventArgs e)
		{
			this.ControlsSetView(this._ExControlView);

			this.LoadConfigControl(this._ConfigMaskedText);
		}

        private void C_Layout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.C_MainPanel.Controls.Count > 0)
            {
                if (this.C_MainPanel.Controls[0] is ConfigMaskedText)
                {
                    (this.C_MainPanel.Controls[0] as ConfigMaskedText).UpdateView(this._ExControlView);
                }
                else
                {
                    return;
                }
            }

            this._ConfigGroupingLayout.SetView(this._ExControlView);

            this.LoadConfigControl(this._ConfigGroupingLayout);
        }
	
	}
}
