/***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:DF_CompactGroupingControl.cs
 * Author:Country.Wu [EMail:Webb.Country.Wu@163.com]
 * Create Time:11/29/2007 12:59:40 PM
 * Copyright:1986-2007@Webb Electronics all right reserved.
 * Purpose:
 * ***********************************************************************/
#region History
/*
 * //Author@DateTime : Description
 * */
#endregion History

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Webb.Reports.ExControls.UI
{
    public class DF_HorizonGroupControl : Webb.Reports.ExControls.UI.ExControlDesignerFormBase
    {
        private System.ComponentModel.IContainer components = null;

        private ConfigHorizonGroup _ConfigHorizonGroup;

        private ConfigGroupingLayout C_FieldAjustSize;

        private System.Windows.Forms.LinkLabel C_GroupingSetting;
        private LinkLabel C_AutoAdjustSize;

        private Webb.PropertyManager propManager = new Webb.PropertyManager();	//06-03-2008@Scott


        public DF_HorizonGroupControl()
        {
            // This call is required by the Windows Form Designer.
            InitializeComponent();

            // TODO: Add any initialization after the InitializeComponent call
            this._ConfigHorizonGroup = new ConfigHorizonGroup();

            C_FieldAjustSize = new ConfigGroupingLayout();
          
            
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.C_GroupingSetting = new System.Windows.Forms.LinkLabel();
            this.C_AutoAdjustSize = new System.Windows.Forms.LinkLabel();
            this.C_AllTask.SuspendLayout();
            this.C_LeftMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // C_MainPanel
            // 
            this.C_MainPanel.Location = new System.Drawing.Point(165, 0);
            this.C_MainPanel.Size = new System.Drawing.Size(677, 381);
            // 
            // C_AllTask
            // 
            this.C_AllTask.Controls.Add(this.C_GroupingSetting);
            this.C_AllTask.Controls.Add(this.C_AutoAdjustSize);
            this.C_AllTask.Size = new System.Drawing.Size(152, 240);
            this.C_AllTask.Controls.SetChildIndex(this.C_AutoAdjustSize, 0);
            this.C_AllTask.Controls.SetChildIndex(this.C_GroupingSetting, 0);
            this.C_AllTask.Controls.SetChildIndex(this.C_AutoStyle, 0);
            // 
            // C_LeftMenu
            // 
            this.C_LeftMenu.Size = new System.Drawing.Size(160, 421);
            // 
            // C_Splitter
            // 
            this.C_Splitter.Location = new System.Drawing.Point(160, 0);
            this.C_Splitter.Size = new System.Drawing.Size(5, 421);
            // 
            // C_AutoStyle
            // 
            this.C_AutoStyle.Location = new System.Drawing.Point(3, 43);
            this.C_AutoStyle.Size = new System.Drawing.Size(146, 32);
            this.C_AutoStyle.Visible = false;
            // 
            // C_GroupingSetting
            // 
            this.C_GroupingSetting.Dock = System.Windows.Forms.DockStyle.Top;
            this.C_GroupingSetting.Location = new System.Drawing.Point(3, 18);
            this.C_GroupingSetting.Name = "C_GroupingSetting";
            this.C_GroupingSetting.Size = new System.Drawing.Size(146, 25);
            this.C_GroupingSetting.TabIndex = 1;
            this.C_GroupingSetting.TabStop = true;
            this.C_GroupingSetting.Text = "Group Setting";
            this.C_GroupingSetting.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.C_GroupingSetting.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.C_GroupingSetting_LinkClicked);
            // 
            // C_AutoAdjustSize
            // 
            this.C_AutoAdjustSize.AutoSize = true;
            this.C_AutoAdjustSize.Location = new System.Drawing.Point(36, 56);
            this.C_AutoAdjustSize.Name = "C_AutoAdjustSize";
            this.C_AutoAdjustSize.Size = new System.Drawing.Size(77, 14);
            this.C_AutoAdjustSize.TabIndex = 4;
            this.C_AutoAdjustSize.TabStop = true;
            this.C_AutoAdjustSize.Text = "Adjust Size";
            this.C_AutoAdjustSize.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.C_AutoAdjustSize_LinkClicked);
            // 
            // DF_HorizonGroupControl
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(7, 15);
            this.ClientSize = new System.Drawing.Size(842, 421);
            this.Name = "DF_HorizonGroupControl";
            this.Load += new System.EventHandler(this.DF_CompactGroupingControl_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DF_HorizonGroupControl_FormClosed);
            this.C_AllTask.ResumeLayout(false);
            this.C_AllTask.PerformLayout();
            this.C_LeftMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        //Scott@2008-01-02 16:15 modified some of the following code.
        public override void SetView(Webb.Reports.ExControls.Views.ExControlView i_View)
        {
            this._ExControlView = i_View;

            base.SetView(i_View);

            this.ControlsSetView(this._ExControlView);
        }

        public override void UpdateView(Webb.Reports.ExControls.Views.ExControlView i_View)
        {
            base.UpdateView(i_View);

            this.ControlsUpdateView(i_View);
        }

        //on load
        private void DF_CompactGroupingControl_Load(object sender, System.EventArgs e)
        {
            this.ControlsSetView(this._ExControlView);

            this.LoadConfigControl(this._ConfigHorizonGroup);

            //propManager.SetPropertyVisibility(typeof(Data.FieldGroupInfo), "TopCount", false);	//06-03-2008@Scott
        }

        //group setting
        private void C_GroupingSetting_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            this._ConfigHorizonGroup.SetView(this._ExControlView);

            this.LoadConfigControl(this._ConfigHorizonGroup);
        }

     
        //ok
        protected override void C_OK_Click(object sender, EventArgs e)
        {
            base.C_OK_Click(sender, e);

            this.ControlsUpdateView(this._ExControlView);

            this._ExControlView.UpdateView();
        }

        //apply
        protected override void C_Apply_Click(object sender, EventArgs e)
        {
            base.C_Apply_Click(sender, e);

            this.ControlsUpdateView(this._ExControlView);

            this._ExControlView.UpdateView();
        }

        private void ControlsUpdateView(Views.ExControlView i_View)
        {
            this._ConfigHorizonGroup.UpdateView(i_View);

            if (C_FieldAjustSize.Parent != null) this.C_FieldAjustSize.UpdateView(i_View);

                        
        }

        private void ControlsSetView(Views.ExControlView i_View)
        {
            this._ConfigHorizonGroup.SetView(i_View);           

        }

        private void DF_HorizonGroupControl_FormClosed(object sender, FormClosedEventArgs e)
        {
            //propManager.SetPropertyVisibility(typeof(Data.FieldGroupInfo), "TopCount", true);	//06-03-2008@Scott
        }

        private void C_AutoAdjustSize_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.C_FieldAjustSize.SetView(this._ExControlView);

            this.LoadConfigControl(this.C_FieldAjustSize);
        }

      
    }
}

