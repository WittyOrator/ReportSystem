using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Webb.Reports.ExControls.UI
{
	/// <summary>
	/// Summary description for DF_GridControl.
	/// </summary>
	public class DF_HorizontalGridControl : Webb.Reports.ExControls.UI.ExControlDesignerFormBase
	{
		private PropertyManager propManager = new PropertyManager();	//06-03-2008@Scott

		//private ConfigGridGroupInfo C_GroupInfoSetting;
		private ConfigGridInfo C_GridControlSetting;
        private ConfighorizontalGridColumn C_GridColumnsSetting;
		private ConfigGroupingLayout C_FieldAjustSize;
        private ConfigHeaderConstructor _ConfigHeadersControl; //2008-8-28 9:56:19@simon

		private System.Windows.Forms.LinkLabel C_LinkGridSetting;
        private System.Windows.Forms.LinkLabel C_LinkGridColumns;
		private System.Windows.Forms.LinkLabel C_LinkAjustSize;
		private System.Windows.Forms.LinkLabel C_HeadersConstruct;

		private System.ComponentModel.Container components = null;
		
		public DF_HorizontalGridControl()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			this.Load += new EventHandler(DF_HorizontalGridControl_Load);

            this.Closed += new EventHandler(DF_HorizontalGridControl_Closed);

			this.C_GridControlSetting = new ConfigGridInfo();

            this.C_GridColumnsSetting = new ConfighorizontalGridColumn();

			this.C_FieldAjustSize = new ConfigGroupingLayout();

			//this.C_GroupInfoSetting = new ConfigGridGroupInfo();

			//2008-8-28 9:56:11@simon
			this._ConfigHeadersControl=new ConfigHeaderConstructor();
		}


        public DF_HorizontalGridControl(Views.HorizontalGridView horizontalGridView)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

            this._ExControlView = horizontalGridView;

			//
			// TODO: Add any constructor code after InitializeComponent call
			//		

			this.Closed +=new EventHandler(DF_HorizontalGridControl_Closed);

		    this._ConfigHeadersControl=new ConfigHeaderConstructor();

			//this.C_GroupInfoSetting = new ConfigGridGroupInfo();

			
			propManager.SetPropertyVisibility(typeof(Data.FieldGroupInfo),"Style",false);	//06-03-2008@Scott
			
			propManager.SetPropertyVisibility(typeof(Data.FieldGroupInfo),"SectionSummeries",false);	//06-03-2008@Scott	

			propManager.SetPropertyVisibility(typeof(Data.FieldGroupInfo),"ColorNeedChange",false);	//06-03-2008@Scott

			propManager.SetPropertyVisibility(typeof(Data.FieldGroupInfo),"AddTotal",false);	//06-03-2008@Scott

			propManager.SetPropertyVisibility(typeof(Data.FieldGroupInfo),"TotalTitle",false);	//06-03-2008@Scott

			propManager.SetPropertyVisibility(typeof(Data.FieldGroupInfo),"UseLineBreak",false);	//06-03-2008@Scott

			this.C_LinkGridSetting.Visible=false;

            this.C_LinkGridColumns.Visible = false;

			this.C_LinkAjustSize.Visible=false;

			this.C_AutoStyle.Visible=false;

			this.C_HeadersConstruct.Visible=false;

            //this.C_GroupInfoSetting.SetView(horizontalGridView);

            this._ConfigHeadersControl.SetView(horizontalGridView); //2008-8-28 9:59:30@simon
			
			this.LoadConfigControl(this.C_GridControlSetting);	    
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
            this.C_LinkGridSetting = new System.Windows.Forms.LinkLabel();
            this.C_LinkGridColumns = new System.Windows.Forms.LinkLabel();
            this.C_LinkAjustSize = new System.Windows.Forms.LinkLabel();
            this.C_HeadersConstruct = new System.Windows.Forms.LinkLabel();
            this.C_AllTask.SuspendLayout();
            this.C_LeftMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // C_MainPanel
            // 
            this.C_MainPanel.Size = new System.Drawing.Size(651, 422);
            // 
            // C_AllTask
            // 
            this.C_AllTask.Controls.Add(this.C_HeadersConstruct);
            this.C_AllTask.Controls.Add(this.C_LinkAjustSize);
            this.C_AllTask.Controls.Add(this.C_LinkGridColumns);
            this.C_AllTask.Controls.Add(this.C_LinkGridSetting);
            this.C_AllTask.Controls.SetChildIndex(this.C_LinkGridSetting, 0);
            this.C_AllTask.Controls.SetChildIndex(this.C_LinkGridColumns, 0);
            this.C_AllTask.Controls.SetChildIndex(this.C_AutoStyle, 0);
            this.C_AllTask.Controls.SetChildIndex(this.C_LinkAjustSize, 0);
            this.C_AllTask.Controls.SetChildIndex(this.C_HeadersConstruct, 0);
            // 
            // C_LeftMenu
            // 
            this.C_LeftMenu.Size = new System.Drawing.Size(176, 462);
            // 
            // C_Splitter
            // 
            this.C_Splitter.Size = new System.Drawing.Size(5, 462);
            // 
            // C_AutoStyle
            // 
            this.C_AutoStyle.Location = new System.Drawing.Point(3, 82);
            // 
            // C_LinkGridSetting
            // 
            this.C_LinkGridSetting.Dock = System.Windows.Forms.DockStyle.Top;
            this.C_LinkGridSetting.Location = new System.Drawing.Point(3, 18);
            this.C_LinkGridSetting.Name = "C_LinkGridSetting";
            this.C_LinkGridSetting.Size = new System.Drawing.Size(162, 32);
            this.C_LinkGridSetting.TabIndex = 1;
            this.C_LinkGridSetting.TabStop = true;
            this.C_LinkGridSetting.Text = "Grid Setting";
            this.C_LinkGridSetting.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.C_LinkGridSetting.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.C_LinkGridSetting_LinkClicked);
            // 
            // C_LinkGridColumns
            // 
            this.C_LinkGridColumns.Dock = System.Windows.Forms.DockStyle.Top;
            this.C_LinkGridColumns.Location = new System.Drawing.Point(3, 50);
            this.C_LinkGridColumns.Name = "C_LinkGridColumns";
            this.C_LinkGridColumns.Size = new System.Drawing.Size(162, 32);
            this.C_LinkGridColumns.TabIndex = 2;
            this.C_LinkGridColumns.TabStop = true;
            this.C_LinkGridColumns.Text = "Grid Columns Setting";
            this.C_LinkGridColumns.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.C_LinkGridColumns.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.C_LinkGridColumns_LinkClicked);
            // 
            // C_LinkAjustSize
            // 
            this.C_LinkAjustSize.Dock = System.Windows.Forms.DockStyle.Top;
            this.C_LinkAjustSize.Location = new System.Drawing.Point(3, 114);
            this.C_LinkAjustSize.Name = "C_LinkAjustSize";
            this.C_LinkAjustSize.Size = new System.Drawing.Size(162, 32);
            this.C_LinkAjustSize.TabIndex = 5;
            this.C_LinkAjustSize.TabStop = true;
            this.C_LinkAjustSize.Text = "Adjust Size";
            this.C_LinkAjustSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.C_LinkAjustSize.Click += new System.EventHandler(this.C_LinkAjustSize_Click);
            // 
            // C_HeadersConstruct
            // 
            this.C_HeadersConstruct.Dock = System.Windows.Forms.DockStyle.Top;
            this.C_HeadersConstruct.Location = new System.Drawing.Point(3, 146);
            this.C_HeadersConstruct.Name = "C_HeadersConstruct";
            this.C_HeadersConstruct.Size = new System.Drawing.Size(162, 32);
            this.C_HeadersConstruct.TabIndex = 7;
            this.C_HeadersConstruct.TabStop = true;
            this.C_HeadersConstruct.Text = "Header Titles";
            this.C_HeadersConstruct.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.C_HeadersConstruct.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.C_HeadersConstruct_LinkClicked);
            // 
            // DF_HorizontalGridControl
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(7, 15);
            this.ClientSize = new System.Drawing.Size(832, 462);
            this.Name = "DF_HorizontalGridControl";
            this.Text = "DF_HorizontalGridControl";
            this.C_AllTask.ResumeLayout(false);
            this.C_LeftMenu.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		//ok
		protected override void C_OK_Click(object sender, EventArgs e)
		{
			base.C_OK_Click (sender, e);
			
			this.ControlsUpdateView(this._ExControlView);
			
			this._ExControlView.UpdateView();
		}

		//cancel
		protected override void C_Cancel_Click(object sender, EventArgs e)
		{
			base.C_Cancel_Click (sender, e);
		}

		//apply
		protected override void C_Apply_Click(object sender, EventArgs e)
		{
			base.C_Apply_Click (sender, e);
			
			this.ControlsUpdateView(this._ExControlView);

			this._ExControlView.UpdateView();
		}

		private void ControlsUpdateView(Views.ExControlView i_View)
		{
            if (C_GridControlSetting != null) this.C_GridControlSetting.UpdateView(i_View);

            if (C_GridColumnsSetting != null) this.C_GridColumnsSetting.UpdateView(i_View);
            //if (C_GroupInfoSetting != null && this.C_GroupInfoSetting.Parent != null) this.C_GroupInfoSetting.UpdateView(i_View);

            if (C_FieldAjustSize != null && this.C_FieldAjustSize.Parent != null) this.C_FieldAjustSize.UpdateView(i_View);	//06-30-2008@Scott

        	if (this._ConfigHeadersControl.Parent != null)this._ConfigHeadersControl.UpdateView(i_View);

		}

		private void ControlsSetView(Views.ExControlView i_View)
		{
			this.C_GridControlSetting.SetView(i_View);

            this.C_GridColumnsSetting.SetView(i_View);

			this.C_FieldAjustSize.SetView(i_View);

			//this.C_GroupInfoSetting.SetView(i_View);

			this._ConfigHeadersControl.SetView(i_View); //2008-8-28 9:59:30@simon
		}

        private void DF_HorizontalGridControl_Load(object sender, EventArgs e)
		{
            //propManager.SetPropertyVisibility(typeof(Data.FieldGroupInfo),"Style",false);	//06-03-2008@Scott

			propManager.SetPropertyVisibility(typeof(Data.FieldGroupInfo),"SectionSummeries",false);	//06-03-2008@Scott
			
            //propManager.SetPropertyVisibility(typeof(Data.FieldGroupInfo),"ColorNeedChange",false);	//06-03-2008@Scott

            //propManager.SetPropertyVisibility(typeof(Data.FieldGroupInfo),"AddTotal",false);	//06-03-2008@Scott

            //propManager.SetPropertyVisibility(typeof(Data.FieldGroupInfo),"TotalTitle",false);	//06-03-2008@Scott		

			propManager.SetPropertyVisibility(typeof(Data.FieldGroupInfo),"UseLineBreak",false);	//06-03-2008@Scott

            propManager.SetPropertyVisibility(typeof(Data.GroupInfo), "DistinctValues", true);	//06-03-2008@Scott
            propManager.SetPropertyVisibility(typeof(Data.GroupInfo), "DisplayAsColumn", true);	//06-03-2008@Scott

			this.ControlsSetView(this._ExControlView);
			
			this.LoadConfigControl(this.C_GridControlSetting);
		}

        private void DF_HorizontalGridControl_Closed(object sender, EventArgs e)
		{
            //propManager.SetPropertyVisibility(typeof(Data.FieldGroupInfo),"Style",true);	//06-03-2008@Scott
		
			propManager.SetPropertyVisibility(typeof(Data.FieldGroupInfo),"SectionSummeries",true);	//06-03-2008@Scott

            //propManager.SetPropertyVisibility(typeof(Data.FieldGroupInfo),"ColorNeedChange",true);	//06-03-2008@Scott

            //propManager.SetPropertyVisibility(typeof(Data.FieldGroupInfo),"AddTotal",true);	//06-03-2008@Scott

            //propManager.SetPropertyVisibility(typeof(Data.FieldGroupInfo),"TotalTitle",true);	//06-03-2008@Scott

			propManager.SetPropertyVisibility(typeof(Data.FieldGroupInfo),"UseLineBreak",true);	//06-03-2008@Scott

            propManager.SetPropertyVisibility(typeof(Data.GroupInfo), "DistinctValues", false);	//06-03-2008@Scott
            propManager.SetPropertyVisibility(typeof(Data.GroupInfo), "DisplayAsColumn", false);	//06-03-2008@Scott
		}

		//Grid Setting
		private void C_LinkGridSetting_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			this.C_GridControlSetting.SetView(this._ExControlView);
			
			this.LoadConfigControl(this.C_GridControlSetting);
		}

        //Grid Columns Setting
        void C_LinkGridColumns_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.C_GridColumnsSetting.SetView(this._ExControlView);

            this.LoadConfigControl(this.C_GridColumnsSetting);
        }

		//Ajust Size
		private void C_LinkAjustSize_Click(object sender, System.EventArgs e)
		{
			this.C_FieldAjustSize.SetView(this._ExControlView);

			this.LoadConfigControl(this.C_FieldAjustSize);
		}

		//GroupSetting
        //private void C_LinkGroupSetting_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        //{
        //    this.C_GroupInfoSetting.SetView(this._ExControlView);

        //    this.LoadConfigControl(this.C_GroupInfoSetting);
        //}

		private void C_HeadersConstruct_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			this._ConfigHeadersControl.SetView(this._ExControlView);
			
			this.LoadConfigControl(this._ConfigHeadersControl);
		}
		
	}
}
