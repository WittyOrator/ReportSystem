//08-14-2008@Scott
using System;
using System.Design;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Reflection;
using System.Windows.Forms.Design;
using Microsoft.Win32;

using Webb.Utilities;

namespace Webb.Reports.DataProvider.UI
{
	/// <summary>
	/// Summary description for CustomSectionFiltersForm.
	/// </summary>
	public class AdvReportSectionFiltersForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Panel C_PanelControls;
		private System.Windows.Forms.Button C_BtnOK;
		private System.Windows.Forms.Button C_BtnCancel;
		private System.Windows.Forms.Button C_BtnClearAll;
        private System.Windows.Forms.Button C_BtnSelectAll;

		protected NameForm _SaveFileDialog;
		protected PropertyForm _PropertyForm;
		private System.Windows.Forms.Panel C_PanelMain;
		private System.Windows.Forms.Label C_TitleLabel;
		private System.Windows.Forms.Button C_BtnToRight;
		private System.Windows.Forms.ListBox C_ListSecFilters;
		private System.Windows.Forms.ListBox C_ListSelectedSecFilters;
		private System.Windows.Forms.Button C_BtnToLeft;
		private System.Windows.Forms.Button C_BtnMoveUp;
		private System.Windows.Forms.Button C_BtnMoveDown;
        private System.Windows.Forms.PropertyGrid C_PropertyGrid;

        static readonly string RegKeyPath = "SOFTWARE\\Webb Electronics\\WebbReport\\RecentAdvantageUserFolder";

        static readonly string KeyName = "LastAdvantageUserPath";

		protected SectionFilterCollection _InitSectionFilters;

        private RegistryKey _RegKey;
        private RegistryKey RegKey
        {
            get
            {
                if (this._RegKey == null)
                {
                    this._RegKey = Registry.CurrentUser.OpenSubKey(RegKeyPath, true);
                    if (this._RegKey == null)
                    {
                        this._RegKey = Registry.CurrentUser.CreateSubKey(RegKeyPath);
                    }
                }
                return this._RegKey;
            }
        }

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        public AdvReportSectionFiltersForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			this.Load += new EventHandler(CustomSectionFiltersForm_Load);

			this._SaveFileDialog = new NameForm();

			this._PropertyForm = new PropertyForm();
		}


		public AdvReportSectionFiltersForm(SectionFilterCollection sectionFilters)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			this._InitSectionFilters=sectionFilters;

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			this.Load += new EventHandler(CustomSectionFiltersForm_Load);

			this._SaveFileDialog = new NameForm();

			this._PropertyForm = new PropertyForm();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomSectionFiltersForm));
            this.C_PanelControls = new System.Windows.Forms.Panel();
            this.C_BtnSelectAll = new System.Windows.Forms.Button();
            this.C_BtnClearAll = new System.Windows.Forms.Button();
            this.C_BtnOK = new System.Windows.Forms.Button();
            this.C_BtnCancel = new System.Windows.Forms.Button();
            this.C_PanelMain = new System.Windows.Forms.Panel();
            this.C_BtnMoveUp = new System.Windows.Forms.Button();
            this.C_TitleLabel = new System.Windows.Forms.Label();
            this.C_BtnToRight = new System.Windows.Forms.Button();
            this.C_ListSecFilters = new System.Windows.Forms.ListBox();
            this.C_ListSelectedSecFilters = new System.Windows.Forms.ListBox();
            this.C_BtnToLeft = new System.Windows.Forms.Button();
            this.C_BtnMoveDown = new System.Windows.Forms.Button();
            this.C_PropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.C_PanelControls.SuspendLayout();
            this.C_PanelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // C_PanelControls
            // 
            this.C_PanelControls.Controls.Add(this.C_BtnSelectAll);
            this.C_PanelControls.Controls.Add(this.C_BtnClearAll);
            this.C_PanelControls.Controls.Add(this.C_BtnOK);
            this.C_PanelControls.Controls.Add(this.C_BtnCancel);
            this.C_PanelControls.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.C_PanelControls.Location = new System.Drawing.Point(0, 422);
            this.C_PanelControls.Name = "C_PanelControls";
            this.C_PanelControls.Size = new System.Drawing.Size(700, 60);
            this.C_PanelControls.TabIndex = 0;
            // 
            // C_BtnSelectAll
            // 
            this.C_BtnSelectAll.Location = new System.Drawing.Point(355, 16);
            this.C_BtnSelectAll.Name = "C_BtnSelectAll";
            this.C_BtnSelectAll.Size = new System.Drawing.Size(80, 25);
            this.C_BtnSelectAll.TabIndex = 1;
            this.C_BtnSelectAll.Text = "Select All";
            this.C_BtnSelectAll.Click += new System.EventHandler(this.C_BtnSelectAll_Click);
            // 
            // C_BtnClearAll
            // 
            this.C_BtnClearAll.Location = new System.Drawing.Point(439, 16);
            this.C_BtnClearAll.Name = "C_BtnClearAll";
            this.C_BtnClearAll.Size = new System.Drawing.Size(80, 25);
            this.C_BtnClearAll.TabIndex = 1;
            this.C_BtnClearAll.Text = "Clear All";
            this.C_BtnClearAll.Click += new System.EventHandler(this.C_BtnClearAll_Click);
            // 
            // C_BtnOK
            // 
            this.C_BtnOK.Location = new System.Drawing.Point(523, 16);
            this.C_BtnOK.Name = "C_BtnOK";
            this.C_BtnOK.Size = new System.Drawing.Size(80, 25);
            this.C_BtnOK.TabIndex = 1;
            this.C_BtnOK.Text = "OK";
            this.C_BtnOK.Click += new System.EventHandler(this.C_BtnOK_Click);
            // 
            // C_BtnCancel
            // 
            this.C_BtnCancel.Location = new System.Drawing.Point(607, 16);
            this.C_BtnCancel.Name = "C_BtnCancel";
            this.C_BtnCancel.Size = new System.Drawing.Size(80, 25);
            this.C_BtnCancel.TabIndex = 1;
            this.C_BtnCancel.Text = "Cancel";
            this.C_BtnCancel.Click += new System.EventHandler(this.C_BtnCancel_Click);
            // 
            // C_PanelMain
            // 
            this.C_PanelMain.Controls.Add(this.C_BtnMoveUp);
            this.C_PanelMain.Controls.Add(this.C_TitleLabel);
            this.C_PanelMain.Controls.Add(this.C_BtnToRight);
            this.C_PanelMain.Controls.Add(this.C_ListSecFilters);
            this.C_PanelMain.Controls.Add(this.C_ListSelectedSecFilters);
            this.C_PanelMain.Controls.Add(this.C_BtnToLeft);
            this.C_PanelMain.Controls.Add(this.C_BtnMoveDown);
            this.C_PanelMain.Controls.Add(this.C_PropertyGrid);
            this.C_PanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.C_PanelMain.Location = new System.Drawing.Point(0, 0);
            this.C_PanelMain.Name = "C_PanelMain";
            this.C_PanelMain.Size = new System.Drawing.Size(700, 422);
            this.C_PanelMain.TabIndex = 3;
            // 
            // C_BtnMoveUp
            // 
            this.C_BtnMoveUp.Location = new System.Drawing.Point(451, 384);
            this.C_BtnMoveUp.Name = "C_BtnMoveUp";
            this.C_BtnMoveUp.Size = new System.Drawing.Size(90, 25);
            this.C_BtnMoveUp.TabIndex = 4;
            this.C_BtnMoveUp.Text = "Move Up";
            this.C_BtnMoveUp.Click += new System.EventHandler(this.C_BtnMoveUp_Click);
            // 
            // C_TitleLabel
            // 
            this.C_TitleLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.C_TitleLabel.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.C_TitleLabel.Image = ((System.Drawing.Image)(resources.GetObject("C_TitleLabel.Image")));
            this.C_TitleLabel.Location = new System.Drawing.Point(0, 0);
            this.C_TitleLabel.Name = "C_TitleLabel";
            this.C_TitleLabel.Size = new System.Drawing.Size(700, 34);
            this.C_TitleLabel.TabIndex = 2;
            this.C_TitleLabel.Text = "Select Section Filters";
            this.C_TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // C_BtnToRight
            // 
            this.C_BtnToRight.Location = new System.Drawing.Point(307, 198);
            this.C_BtnToRight.Name = "C_BtnToRight";
            this.C_BtnToRight.Size = new System.Drawing.Size(90, 25);
            this.C_BtnToRight.TabIndex = 1;
            this.C_BtnToRight.Text = "=>";
            this.C_BtnToRight.Click += new System.EventHandler(this.C_BtnToRight_Click);
            // 
            // C_ListSecFilters
            // 
            this.C_ListSecFilters.ItemHeight = 12;
            this.C_ListSecFilters.Location = new System.Drawing.Point(19, 52);
            this.C_ListSecFilters.Name = "C_ListSecFilters";
            this.C_ListSecFilters.Size = new System.Drawing.Size(269, 352);
            this.C_ListSecFilters.Sorted = true;
            this.C_ListSecFilters.TabIndex = 0;
            this.C_ListSecFilters.DoubleClick += new System.EventHandler(this.C_ListSecFilters_DoubleClick);
            // 
            // C_ListSelectedSecFilters
            // 
            this.C_ListSelectedSecFilters.ItemHeight = 12;
            this.C_ListSelectedSecFilters.Location = new System.Drawing.Point(413, 52);
            this.C_ListSelectedSecFilters.Name = "C_ListSelectedSecFilters";
            this.C_ListSelectedSecFilters.Size = new System.Drawing.Size(269, 304);
            this.C_ListSelectedSecFilters.TabIndex = 0;
            this.C_ListSelectedSecFilters.DoubleClick += new System.EventHandler(this.C_ListSelectedSecFilters_DoubleClick);
            // 
            // C_BtnToLeft
            // 
            this.C_BtnToLeft.Location = new System.Drawing.Point(307, 258);
            this.C_BtnToLeft.Name = "C_BtnToLeft";
            this.C_BtnToLeft.Size = new System.Drawing.Size(90, 25);
            this.C_BtnToLeft.TabIndex = 1;
            this.C_BtnToLeft.Text = "<=";
            this.C_BtnToLeft.Click += new System.EventHandler(this.C_BtnToLeft_Click);
            // 
            // C_BtnMoveDown
            // 
            this.C_BtnMoveDown.Location = new System.Drawing.Point(547, 384);
            this.C_BtnMoveDown.Name = "C_BtnMoveDown";
            this.C_BtnMoveDown.Size = new System.Drawing.Size(90, 25);
            this.C_BtnMoveDown.TabIndex = 4;
            this.C_BtnMoveDown.Text = "Move Down";
            this.C_BtnMoveDown.Click += new System.EventHandler(this.C_BtnMoveDown_Click);
            // 
            // C_PropertyGrid
            // 
            this.C_PropertyGrid.LineColor = System.Drawing.SystemColors.ScrollBar;
            this.C_PropertyGrid.Location = new System.Drawing.Point(19, 250);
            this.C_PropertyGrid.Name = "C_PropertyGrid";
            this.C_PropertyGrid.Size = new System.Drawing.Size(269, 140);
            this.C_PropertyGrid.TabIndex = 3;
            // 
            // CustomSectionFiltersForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(700, 482);
            this.ControlBox = false;
            this.Controls.Add(this.C_PanelMain);
            this.Controls.Add(this.C_PanelControls);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "CustomSectionFiltersForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AdvReport Filters";
            this.C_PanelControls.ResumeLayout(false);
            this.C_PanelMain.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		//OK
		private void C_BtnOK_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			
			this.Close();
		}

		//Cancel
		private void C_BtnCancel_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			
			this.Close();
		}

		//Select All
		private void C_BtnSelectAll_Click(object sender, System.EventArgs e)
		{
			foreach(object item in this.C_ListSecFilters.Items)
			{
				if(!(item is SectionFilter)) continue;

				this.C_ListSelectedSecFilters.Items.Add(item);
			}

			this.C_ListSecFilters.Items.Clear();
		}

		//Clear All
		private void C_BtnClearAll_Click(object sender, System.EventArgs e)
		{
			foreach(object item in this.C_ListSelectedSecFilters.Items)
			{
				if(!(item is SectionFilter)) continue;

				this.C_ListSecFilters.Items.Add(item);
			}

			this.C_ListSelectedSecFilters.Items.Clear();
		}

		//To Right
		private void C_BtnToRight_Click(object sender, System.EventArgs e)
		{
			this.ToRight();
		}

		//To Left
		private void C_BtnToLeft_Click(object sender, System.EventArgs e)
		{
			this.ToLeft();
		}

		//To Right
		private void C_ListSecFilters_DoubleClick(object sender, System.EventArgs e)
		{
			this.ToRight();
		}

		//To Left
		private void C_ListSelectedSecFilters_DoubleClick(object sender, System.EventArgs e)
		{
			this.ToLeft();
		}

		private void ToRight()
		{
			object item = this.C_ListSecFilters.SelectedItem;

			if(!(item is SectionFilter)) return;

			this.C_ListSelectedSecFilters.Items.Add(item);

			this.C_ListSecFilters.Items.Remove(item);
		}

		private void ToLeft()
		{
			object item = this.C_ListSelectedSecFilters.SelectedItem;

			if(!(item is SectionFilter)) return;

			this.C_ListSecFilters.Items.Add(item);

			this.C_ListSelectedSecFilters.Items.Remove(item);
		}

		public SectionFilterCollection GetSelectedSectionFilters()
		{
			SectionFilterCollection secFilters = new SectionFilterCollection();

			foreach(object item in this.C_ListSelectedSecFilters.Items)
			{
				if(item is SectionFilter)
				{
					secFilters.Add(item as SectionFilter);
				}
			}

			return secFilters;
		}

		//Fill Left List
		private void LoadSectionFilters()
		{
			this.C_ListSecFilters.Items.Clear();

			this.C_ListSelectedSecFilters.Items.Clear();

            ScFilterList scFilterList = new ScFilterList();

            object objUserFolder = RegKey.GetValue(KeyName);

			if(objUserFolder == null) return;

			string strUserFolder = objUserFolder.ToString();

			if(strUserFolder.EndsWith("\\")) strUserFolder = strUserFolder.Remove(strUserFolder.Length - 1,1);

			int index = strUserFolder.LastIndexOf('\\');

			strUserFolder = strUserFolder.Remove(index + 1,strUserFolder.Length - index - 1);

			strUserFolder += @"WebbRpt\ScFilter.dat";

			ArrayList arrList=new ArrayList();

			if(this._InitSectionFilters!=null)
			{
				foreach(SectionFilter scFilter in this._InitSectionFilters)
				{
					this.C_ListSelectedSecFilters.Items.Add(scFilter);

					arrList.Add(scFilter.FilterName);
				}
			}	
			

			if(scFilterList.ReadOldFiltersFromDisk(strUserFolder))
			{
                AdvFilterConvertor convertor = new AdvFilterConvertor();

                SectionFilterCollection sectionFilters = convertor.GetReportFilters(scFilterList);

                foreach(SectionFilter filter in sectionFilters)
                {
					if(!arrList.Contains(filter.FilterName))
					{
						this.C_ListSecFilters.Items.Add(filter);
					}
                }
            }

			if(this.C_ListSecFilters.Items.Count > 0)
			{
				this.C_ListSecFilters.SelectedIndex = 0;
			}
		}

		private void CustomSectionFiltersForm_Load(object sender, EventArgs e)
		{
			this.LoadSectionFilters();
		}

		private void C_BtnMoveDown_Click(object sender, System.EventArgs e)
		{
			object item = this.C_ListSelectedSecFilters.SelectedItem;

			if(!(item is SectionFilter)) return;

			int index = this.C_ListSelectedSecFilters.SelectedIndex;

			if(index < this.C_ListSelectedSecFilters.Items.Count - 1)
			{
				this.C_ListSelectedSecFilters.Items.Remove(item);

				this.C_ListSelectedSecFilters.Items.Insert(index + 1,item);

				this.C_ListSelectedSecFilters.SelectedItem = item;
			}
		}

		private void C_BtnMoveUp_Click(object sender, System.EventArgs e)
		{
			object item = this.C_ListSelectedSecFilters.SelectedItem;

			if(!(item is SectionFilter)) return;

			int index = this.C_ListSelectedSecFilters.SelectedIndex;

			if(index > 0)
			{
				this.C_ListSelectedSecFilters.Items.Remove(item);

				this.C_ListSelectedSecFilters.Items.Insert(index - 1,item);

				this.C_ListSelectedSecFilters.SelectedItem = item;
			}
		}
	}
}
