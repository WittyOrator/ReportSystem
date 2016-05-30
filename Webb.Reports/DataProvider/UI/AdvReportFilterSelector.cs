using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Microsoft.Win32;

namespace Webb.Reports.DataProvider.UI
{
	/// <summary>
	/// Summary description for AdvReportFilterSelector.
	/// </summary>
	public class AdvReportFilterSelector : System.Windows.Forms.UserControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		static readonly string RegKeyPath = "SOFTWARE\\Webb Electronics\\WebbReport\\RecentAdvantageUserFolder";		
		
//		static readonly string KeyName = "LastAdvantageUserPath";

		private SectionFilter _SectionFilter;
		private System.Windows.Forms.TextBox txtValue;
		private System.Windows.Forms.ListBox C_LBAdvFilters;

		private SectionFilterCollection sectionFilters=new SectionFilterCollection();
	
		public SectionFilter SectionFilter
		{
			get{return this._SectionFilter;}
			set{this._SectionFilter = value;}
		}

		private RegistryKey _RegKey;
		private RegistryKey RegKey
		{
			get
			{
				if(this._RegKey==null)
				{
                    this._RegKey = Registry.CurrentUser.OpenSubKey(RegKeyPath, true);
					if(this._RegKey==null)
					{
                        this._RegKey = Registry.CurrentUser.CreateSubKey(RegKeyPath);
					}
				}
				return this._RegKey;
			}
		}

		public AdvReportFilterSelector()
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
            this.txtValue = new System.Windows.Forms.TextBox();
            this.C_LBAdvFilters = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // txtValue
            // 
            this.txtValue.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtValue.Location = new System.Drawing.Point(0, 0);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(360, 21);
            this.txtValue.TabIndex = 0;
            this.txtValue.TextChanged += new System.EventHandler(this.txtValue_TextChanged);
            this.txtValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtValue_KeyDown);
            // 
            // C_LBAdvFilters
            // 
            this.C_LBAdvFilters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.C_LBAdvFilters.ItemHeight = 12;
            this.C_LBAdvFilters.Location = new System.Drawing.Point(0, 21);
            this.C_LBAdvFilters.Name = "C_LBAdvFilters";
            this.C_LBAdvFilters.Size = new System.Drawing.Size(360, 364);
            this.C_LBAdvFilters.TabIndex = 1;
            this.C_LBAdvFilters.DoubleClick += new System.EventHandler(this.C_LBAdvFilters_DoubleClick);
            this.C_LBAdvFilters.KeyDown += new System.Windows.Forms.KeyEventHandler(this.C_LBAdvFilters_KeyDown);
            // 
            // AdvReportFilterSelector
            // 
            this.Controls.Add(this.C_LBAdvFilters);
            this.Controls.Add(this.txtValue);
            this.Name = "AdvReportFilterSelector";
            this.Size = new System.Drawing.Size(360, 392);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		public new bool Update()
		{
			this.C_LBAdvFilters.Items.Clear();

			this.txtValue.Text=string.Empty;

			ScFilterList scFilterList =Webb.Reports.DataProvider.VideoPlayBackManager.AdvReportFilters;

//			if(scFilterList==null)
//			{
//				object objUserFolder = RegKey.GetValue(KeyName);
//
//				if(objUserFolder == null) return false;
//
//				string strUserFolder = objUserFolder.ToString();
//
//				if(strUserFolder.EndsWith("\\")) strUserFolder = strUserFolder.Remove(strUserFolder.Length - 1,1);
//
//				int index = strUserFolder.LastIndexOf('\\');
//
//				strUserFolder = strUserFolder.Remove(index + 1,strUserFolder.Length - index - 1);
//
//				strUserFolder += @"WebbRpt\ScFilter.dat";
//
//				if(!scFilterList.ReadOldFiltersFromDisk(strUserFolder))return false;
//			}
				
			AdvFilterConvertor convert = new AdvFilterConvertor();

			sectionFilters = convert.GetReportFilters(scFilterList);

			if(sectionFilters.Count > 0)
			{
				foreach(SectionFilter filter in sectionFilters)
				{
					this.C_LBAdvFilters.Items.Add(filter);
				}
				return true;
			}
			else
			{
				return false;
			}			
		}

        // 08-22-2011 Scott
        public bool UpdateEffFilter()
        {
            this.C_LBAdvFilters.Items.Clear();

            this.txtValue.Text = string.Empty;

            ScFilterList scFilterList = Webb.Reports.DataProvider.VideoPlayBackManager.AdvReportFilters;

            AdvFilterConvertor convert = new AdvFilterConvertor();

            sectionFilters = convert.GetEffFilters(scFilterList);

            if (sectionFilters.Count > 0)
            {
                foreach (SectionFilter filter in sectionFilters)
                {
                    this.C_LBAdvFilters.Items.Add(filter);
                }
                return true;
            }
            else
            {
                return false;
            }
        }

		private void C_LBAdvFilters_DoubleClick(object sender, System.EventArgs e)
		{
			UpdateItemsValues();
		}
		private void UpdateItemsValues()
		{
			if(this.C_LBAdvFilters.SelectedItem is SectionFilter)
			{
				this.SectionFilter = this.C_LBAdvFilters.SelectedItem as SectionFilter;
			
				this.Visible = false;
			}
		}
		private void ChangedValues()
		{
		    this.C_LBAdvFilters.Items.Clear();

			string textfilter=this.txtValue.Text.ToLower();
		
			foreach(SectionFilter filter in sectionFilters)
			{
				if(filter.FilterName.ToLower().StartsWith(textfilter))
				{
					this.C_LBAdvFilters.Items.Add(filter);
				}
			}
			if(this.C_LBAdvFilters.Items.Count>0)
			{
				this.C_LBAdvFilters.SelectedIndex=0;
			}
			
		}

		private void txtValue_TextChanged(object sender, System.EventArgs e)
		{
		    ChangedValues();
		}

		private void C_LBAdvFilters_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Return&&this.C_LBAdvFilters.SelectedIndex>=0)
			{
                 UpdateItemsValues();
			}
		}

		private void txtValue_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Return||e.KeyCode==Keys.Down)
			{
				this.C_LBAdvFilters.Focus();
			}
		}	

	}
}
