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
	public class DF_StatControl : Webb.Reports.ExControls.UI.ExControlDesignerFormBase
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private ConfigStatInfo _ConfigStatInfo;
		private System.Windows.Forms.LinkLabel C_LinkStatInfo;
		private System.Windows.Forms.LinkLabel C_linkAdjustSize;
		private ConfigGroupingLayout _ConfigLayOut;
		private System.ComponentModel.Container components = null;

		public DF_StatControl()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			this._ConfigStatInfo = new ConfigStatInfo();
			_ConfigLayOut=new ConfigGroupingLayout();

			this.Load += new EventHandler(DF_StatControl_Load);
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
			this.C_LinkStatInfo = new System.Windows.Forms.LinkLabel();
			this.C_linkAdjustSize = new System.Windows.Forms.LinkLabel();
			this.C_AllTask.SuspendLayout();
			this.C_LeftMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// _ExControlStyleControl
			// 
			this._ExControlStyleControl.Name = "_ExControlStyleControl";
			// 
			// C_MainPanel
			// 
			this.C_MainPanel.Name = "C_MainPanel";
			this.C_MainPanel.Size = new System.Drawing.Size(627, 382);
			// 
			// C_AllTask
			// 
			this.C_AllTask.Controls.Add(this.C_linkAdjustSize);
			this.C_AllTask.Controls.Add(this.C_LinkStatInfo);
			this.C_AllTask.Name = "C_AllTask";
			this.C_AllTask.Controls.SetChildIndex(this.C_LinkStatInfo, 0);
			this.C_AllTask.Controls.SetChildIndex(this.C_linkAdjustSize, 0);
			this.C_AllTask.Controls.SetChildIndex(this.C_AutoStyle, 0);
			// 
			// C_HideOrShowTask
			// 
			this.C_HideOrShowTask.Name = "C_HideOrShowTask";
			// 
			// C_LeftMenu
			// 
			this.C_LeftMenu.Name = "C_LeftMenu";
			this.C_LeftMenu.Size = new System.Drawing.Size(176, 422);
			// 
			// C_Splitter
			// 
			this.C_Splitter.Name = "C_Splitter";
			this.C_Splitter.Size = new System.Drawing.Size(5, 422);
			// 
			// C_AutoStyle
			// 
			this.C_AutoStyle.Location = new System.Drawing.Point(3, 41);
			this.C_AutoStyle.Name = "C_AutoStyle";
			// 
			// C_LinkStatInfo
			// 
			this.C_LinkStatInfo.Dock = System.Windows.Forms.DockStyle.Top;
			this.C_LinkStatInfo.Location = new System.Drawing.Point(3, 18);
			this.C_LinkStatInfo.Name = "C_LinkStatInfo";
			this.C_LinkStatInfo.Size = new System.Drawing.Size(162, 23);
			this.C_LinkStatInfo.TabIndex = 4;
			this.C_LinkStatInfo.TabStop = true;
			this.C_LinkStatInfo.Text = "Stat Info";
			this.C_LinkStatInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.C_LinkStatInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.C_LinkStatInfo_LinkClicked);
			// 
			// C_linkAdjustSize
			// 
			this.C_linkAdjustSize.Location = new System.Drawing.Point(3, 72);
			this.C_linkAdjustSize.Name = "C_linkAdjustSize";
			this.C_linkAdjustSize.Size = new System.Drawing.Size(162, 32);
			this.C_linkAdjustSize.TabIndex = 5;
			this.C_linkAdjustSize.TabStop = true;
			this.C_linkAdjustSize.Text = "Adjust Size";
			this.C_linkAdjustSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.C_linkAdjustSize.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.C_linkAdjustSize_LinkClicked);
			// 
			// DF_StatControl
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(7, 15);
			this.ClientSize = new System.Drawing.Size(808, 422);
			this.Name = "DF_StatControl";
			this.Text = "DF_StatControl";
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

			if(i_View is Views.StatControlView)
			{
				if((i_View as Views.StatControlView).AdjustWidth)
				{
					this.C_linkAdjustSize.Visible=true;
				}
				else
				{
					this.C_linkAdjustSize.Visible=false;
				}
			}

			this.ControlsSetView(i_View);
		}

		private void C_LinkStatInfo_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			this._ConfigStatInfo.SetView(this._ExControlView);

			this.LoadConfigControl(this._ConfigStatInfo);
		}
	
		private void ControlsUpdateView(Views.ExControlView i_View)
		{
			this._ConfigStatInfo.UpdateView(i_View);
			this._ConfigLayOut.UpdateView(i_View);
		}

		private void ControlsSetView(Views.ExControlView i_View)
		{
			this._ConfigStatInfo.SetView(i_View);
			this._ConfigLayOut.SetView(i_View);
		}

		private void DF_StatControl_Load(object sender, EventArgs e)
		{
			this.ControlsSetView(this._ExControlView);

			this.LoadConfigControl(this._ConfigStatInfo);
		}

		private void C_linkAdjustSize_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			this._ConfigLayOut.SetView(this._ExControlView);

			this.LoadConfigControl(this._ConfigLayOut);
		}
	}
}
