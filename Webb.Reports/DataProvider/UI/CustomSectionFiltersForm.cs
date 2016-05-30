using System;
using System.Design;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Reflection;
using System.Windows.Forms.Design;

using Webb.Utilities;

namespace Webb.Reports.DataProvider.UI
{
	/// <summary>
	/// Summary description for CustomSectionFiltersForm.
	/// </summary>
	public class CustomSectionFiltersForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Panel C_PanelControls;
		private System.Windows.Forms.Button C_BtnOK;
		private System.Windows.Forms.Button C_BtnCancel;
		private System.Windows.Forms.Button C_BtnClearAll;
		private System.Windows.Forms.Button C_BtnSelectAll;
        private System.Windows.Forms.Button C_BtnCustom;

        protected NameForm _SaveFileDialog;
        //protected PropertyForm _PropertyForm;

        private System.Windows.Forms.Panel C_PanelMain;
		private System.Windows.Forms.Button C_BtnToRight;
		private System.Windows.Forms.ListBox C_ListSecFilters;
		private System.Windows.Forms.ListBox C_ListSelectedSecFilters;
		private System.Windows.Forms.Button C_BtnToLeft;
		private System.Windows.Forms.Button C_BtnMoveUp;
        private System.Windows.Forms.Button C_BtnMoveDown;
		private System.Windows.Forms.Button C_BtnCopy;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
        private Label label1;
        private Label label2;
        private Button C_BtnNew;
        private Button C_BtnDel;

		private SectionFilterCollection _InitSectionFilters=null;
        private Label lblText;

        private WebbDBTypes _WebbDBTypes = WebbDBTypes.WebbAdvantageFootball;

		public CustomSectionFiltersForm()
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

            _WebbDBTypes = WebbDBTypes.WebbAdvantageFootball;

            //this._PropertyForm = new PropertyForm();
		}


		public CustomSectionFiltersForm(SectionFilterCollection sections)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			_InitSectionFilters=sections;

			this.Load += new EventHandler(CustomSectionFiltersForm_Load);

            this._SaveFileDialog = new NameForm();

            _WebbDBTypes = WebbDBTypes.WebbAdvantageFootball;

            //this._PropertyForm = new PropertyForm();


		}


        public CustomSectionFiltersForm(SectionFilterCollection sections, WebbDBTypes webbDBTypes)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //

            _InitSectionFilters = sections;
            
            _WebbDBTypes = webbDBTypes;

            this.Load += new EventHandler(CustomSectionFiltersForm_Load);

            this._SaveFileDialog = new NameForm();
           
            //this._PropertyForm = new PropertyForm();


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
            this.C_PanelControls = new System.Windows.Forms.Panel();
            this.C_BtnDel = new System.Windows.Forms.Button();
            this.C_BtnCustom = new System.Windows.Forms.Button();
            this.C_BtnSelectAll = new System.Windows.Forms.Button();
            this.C_BtnClearAll = new System.Windows.Forms.Button();
            this.C_BtnOK = new System.Windows.Forms.Button();
            this.C_BtnCancel = new System.Windows.Forms.Button();
            this.C_BtnCopy = new System.Windows.Forms.Button();
            this.C_PanelMain = new System.Windows.Forms.Panel();
            this.C_BtnNew = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.C_BtnMoveUp = new System.Windows.Forms.Button();
            this.C_BtnToRight = new System.Windows.Forms.Button();
            this.C_ListSelectedSecFilters = new System.Windows.Forms.ListBox();
            this.C_BtnToLeft = new System.Windows.Forms.Button();
            this.C_BtnMoveDown = new System.Windows.Forms.Button();
            this.C_ListSecFilters = new System.Windows.Forms.ListBox();
            this.lblText = new System.Windows.Forms.Label();
            this.C_PanelControls.SuspendLayout();
            this.C_PanelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // C_PanelControls
            // 
            this.C_PanelControls.Controls.Add(this.C_BtnDel);
            this.C_PanelControls.Controls.Add(this.C_BtnCustom);
            this.C_PanelControls.Controls.Add(this.C_BtnSelectAll);
            this.C_PanelControls.Controls.Add(this.C_BtnClearAll);
            this.C_PanelControls.Controls.Add(this.C_BtnOK);
            this.C_PanelControls.Controls.Add(this.C_BtnCancel);
            this.C_PanelControls.Controls.Add(this.C_BtnCopy);
            this.C_PanelControls.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.C_PanelControls.Location = new System.Drawing.Point(0, 444);
            this.C_PanelControls.Name = "C_PanelControls";
            this.C_PanelControls.Size = new System.Drawing.Size(707, 60);
            this.C_PanelControls.TabIndex = 0;
            // 
            // C_BtnDel
            // 
            this.C_BtnDel.Location = new System.Drawing.Point(13, 16);
            this.C_BtnDel.Name = "C_BtnDel";
            this.C_BtnDel.Size = new System.Drawing.Size(86, 25);
            this.C_BtnDel.TabIndex = 8;
            this.C_BtnDel.Text = "Delete";
            this.C_BtnDel.Click += new System.EventHandler(this.C_BtnDel_Click);
            // 
            // C_BtnCustom
            // 
            this.C_BtnCustom.Location = new System.Drawing.Point(117, 16);
            this.C_BtnCustom.Name = "C_BtnCustom";
            this.C_BtnCustom.Size = new System.Drawing.Size(88, 25);
            this.C_BtnCustom.TabIndex = 1;
            this.C_BtnCustom.Text = "Edit";
            this.C_BtnCustom.Click += new System.EventHandler(this.C_BtnCustom_Click);
            // 
            // C_BtnSelectAll
            // 
            this.C_BtnSelectAll.Location = new System.Drawing.Point(318, 16);
            this.C_BtnSelectAll.Name = "C_BtnSelectAll";
            this.C_BtnSelectAll.Size = new System.Drawing.Size(79, 25);
            this.C_BtnSelectAll.TabIndex = 1;
            this.C_BtnSelectAll.Text = "Select All";
            this.C_BtnSelectAll.Click += new System.EventHandler(this.C_BtnSelectAll_Click);
            // 
            // C_BtnClearAll
            // 
            this.C_BtnClearAll.Location = new System.Drawing.Point(414, 16);
            this.C_BtnClearAll.Name = "C_BtnClearAll";
            this.C_BtnClearAll.Size = new System.Drawing.Size(79, 25);
            this.C_BtnClearAll.TabIndex = 1;
            this.C_BtnClearAll.Text = "Clear All";
            this.C_BtnClearAll.Click += new System.EventHandler(this.C_BtnClearAll_Click);
            // 
            // C_BtnOK
            // 
            this.C_BtnOK.Location = new System.Drawing.Point(523, 16);
            this.C_BtnOK.Name = "C_BtnOK";
            this.C_BtnOK.Size = new System.Drawing.Size(79, 25);
            this.C_BtnOK.TabIndex = 1;
            this.C_BtnOK.Text = "OK";
            this.C_BtnOK.Click += new System.EventHandler(this.C_BtnOK_Click);
            // 
            // C_BtnCancel
            // 
            this.C_BtnCancel.Location = new System.Drawing.Point(607, 16);
            this.C_BtnCancel.Name = "C_BtnCancel";
            this.C_BtnCancel.Size = new System.Drawing.Size(79, 25);
            this.C_BtnCancel.TabIndex = 1;
            this.C_BtnCancel.Text = "Cancel";
            this.C_BtnCancel.Click += new System.EventHandler(this.C_BtnCancel_Click);
            // 
            // C_BtnCopy
            // 
            this.C_BtnCopy.Location = new System.Drawing.Point(220, 16);
            this.C_BtnCopy.Name = "C_BtnCopy";
            this.C_BtnCopy.Size = new System.Drawing.Size(79, 25);
            this.C_BtnCopy.TabIndex = 1;
            this.C_BtnCopy.Text = "Copy";
            this.C_BtnCopy.Click += new System.EventHandler(this.C_BtnCopy_Click);
            // 
            // C_PanelMain
            // 
            this.C_PanelMain.Controls.Add(this.lblText);
            this.C_PanelMain.Controls.Add(this.C_BtnNew);
            this.C_PanelMain.Controls.Add(this.label2);
            this.C_PanelMain.Controls.Add(this.label1);
            this.C_PanelMain.Controls.Add(this.C_BtnMoveUp);
            this.C_PanelMain.Controls.Add(this.C_BtnToRight);
            this.C_PanelMain.Controls.Add(this.C_ListSelectedSecFilters);
            this.C_PanelMain.Controls.Add(this.C_BtnToLeft);
            this.C_PanelMain.Controls.Add(this.C_BtnMoveDown);
            this.C_PanelMain.Controls.Add(this.C_ListSecFilters);
            this.C_PanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.C_PanelMain.Location = new System.Drawing.Point(0, 0);
            this.C_PanelMain.Name = "C_PanelMain";
            this.C_PanelMain.Size = new System.Drawing.Size(707, 444);
            this.C_PanelMain.TabIndex = 3;
            // 
            // C_BtnNew
            // 
            this.C_BtnNew.Location = new System.Drawing.Point(12, 408);
            this.C_BtnNew.Name = "C_BtnNew";
            this.C_BtnNew.Size = new System.Drawing.Size(159, 25);
            this.C_BtnNew.TabIndex = 6;
            this.C_BtnNew.Text = "Create New Filter";
            this.C_BtnNew.Click += new System.EventHandler(this.C_BtnNew_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(11, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(203, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "All available section Filters";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(420, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(179, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Selected Section Filters";
            // 
            // C_BtnMoveUp
            // 
            this.C_BtnMoveUp.Location = new System.Drawing.Point(451, 408);
            this.C_BtnMoveUp.Name = "C_BtnMoveUp";
            this.C_BtnMoveUp.Size = new System.Drawing.Size(90, 25);
            this.C_BtnMoveUp.TabIndex = 4;
            this.C_BtnMoveUp.Text = "Move Up";
            this.C_BtnMoveUp.Click += new System.EventHandler(this.C_BtnMoveUp_Click);
            // 
            // C_BtnToRight
            // 
            this.C_BtnToRight.Location = new System.Drawing.Point(316, 221);
            this.C_BtnToRight.Name = "C_BtnToRight";
            this.C_BtnToRight.Size = new System.Drawing.Size(90, 25);
            this.C_BtnToRight.TabIndex = 1;
            this.C_BtnToRight.Text = "=> ";
            this.C_BtnToRight.Click += new System.EventHandler(this.C_BtnToRight_Click);
            // 
            // C_ListSelectedSecFilters
            // 
            this.C_ListSelectedSecFilters.ItemHeight = 12;
            this.C_ListSelectedSecFilters.Location = new System.Drawing.Point(422, 74);
            this.C_ListSelectedSecFilters.Name = "C_ListSelectedSecFilters";
            this.C_ListSelectedSecFilters.Size = new System.Drawing.Size(269, 316);
            this.C_ListSelectedSecFilters.TabIndex = 0;
            this.C_ListSelectedSecFilters.SelectedIndexChanged += new System.EventHandler(this.C_ListSelectedSecFilters_SelectedIndexChanged);
            this.C_ListSelectedSecFilters.DoubleClick += new System.EventHandler(this.C_ListSelectedSecFilters_DoubleClick);
            // 
            // C_BtnToLeft
            // 
            this.C_BtnToLeft.Location = new System.Drawing.Point(313, 282);
            this.C_BtnToLeft.Name = "C_BtnToLeft";
            this.C_BtnToLeft.Size = new System.Drawing.Size(90, 25);
            this.C_BtnToLeft.TabIndex = 1;
            this.C_BtnToLeft.Text = "<=";
            this.C_BtnToLeft.Click += new System.EventHandler(this.C_BtnToLeft_Click);
            // 
            // C_BtnMoveDown
            // 
            this.C_BtnMoveDown.Location = new System.Drawing.Point(547, 408);
            this.C_BtnMoveDown.Name = "C_BtnMoveDown";
            this.C_BtnMoveDown.Size = new System.Drawing.Size(90, 25);
            this.C_BtnMoveDown.TabIndex = 4;
            this.C_BtnMoveDown.Text = "Move Down";
            this.C_BtnMoveDown.Click += new System.EventHandler(this.C_BtnMoveDown_Click);
            // 
            // C_ListSecFilters
            // 
            this.C_ListSecFilters.ItemHeight = 12;
            this.C_ListSecFilters.Location = new System.Drawing.Point(10, 74);
            this.C_ListSecFilters.Name = "C_ListSecFilters";
            this.C_ListSecFilters.Size = new System.Drawing.Size(289, 328);
            this.C_ListSecFilters.Sorted = true;
            this.C_ListSecFilters.TabIndex = 0;
            this.C_ListSecFilters.SelectedIndexChanged += new System.EventHandler(this.C_ListSecFilters_SelectedIndexChanged);
            this.C_ListSecFilters.DoubleClick += new System.EventHandler(this.C_ListSecFilters_DoubleClick);
            // 
            // lblText
            // 
            this.lblText.AutoSize = true;
            this.lblText.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Bold);
            this.lblText.Location = new System.Drawing.Point(7, 9);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(637, 18);
            this.lblText.TabIndex = 7;
            this.lblText.Text = "Please use \"=>\" button to add filters and use \"<=\" button to remove filters ";
            // 
            // CustomSectionFiltersForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(707, 504);
            this.ControlBox = false;
            this.Controls.Add(this.C_PanelMain);
            this.Controls.Add(this.C_PanelControls);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "CustomSectionFiltersForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Custom Section Filters";
            this.C_PanelControls.ResumeLayout(false);
            this.C_PanelMain.ResumeLayout(false);
            this.C_PanelMain.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		//OK
		private void C_BtnOK_Click(object sender, System.EventArgs e)
		{
            foreach (SectionFilter scFilter in this.C_ListSecFilters.Items)
            {
                string strFileName = this.GetSecFiltersFilePath(scFilter.FilterName);

                if(!System.IO.File.Exists(strFileName))
                {
                    scFilter.Save(strFileName);
                }
            }

			this.DialogResult = DialogResult.OK;
			
			this.Close();
		}

		//Cancel
		private void C_BtnCancel_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			
			this.Close();
        }
        #region Old
        //Custom...
        //private void C_BtnCustom_Click(object sender, System.EventArgs e)
        //{

        //    object item = null;

        //    bool secAdd = false;

        //    if (this.C_ListSecFilters.SelectedIndex >= 0)
        //    {
        //        item = this.C_ListSecFilters.SelectedItem;
        //        secAdd = false;
        //    }
        //    else
        //    {
        //        item = this.C_ListSelectedSecFilters.SelectedItem;
        //        secAdd = true;
        //    }

        //    if (!(item is SectionFilter)) return;

        //    SectionFilter filter = item as SectionFilter;

        //    filter.Filter.IsCustomFilter = true;

        //    string strOldName = filter.FilterName;

        //    this._PropertyForm.BindProperty(filter);

        //    if (this._PropertyForm.ShowDialog(this) == DialogResult.OK)
        //    {
        //        if (strOldName != filter.FilterName)
        //        {
        //            if (System.IO.File.Exists(this.GetSecFiltersFilePath(filter.FilterName)))
        //            {
        //                MessageBox.Show(string.Format("[{0}] was exist.", filter.FilterName));

        //                filter.FilterName = strOldName;

        //                return;
        //            }
        //            else
        //            {
        //                System.IO.File.Delete(this.GetSecFiltersFilePath(strOldName));

        //                filter.Filter.Name = string.Empty;

        //                if (secAdd)
        //                {
        //                    this.C_ListSelectedSecFilters.Items.Remove(filter);
        //                    this.C_ListSelectedSecFilters.Items.Add(filter);
        //                }
        //                else
        //                {
        //                    this.C_ListSecFilters.Items.Remove(filter);

        //                    this.C_ListSecFilters.Items.Add(filter);
        //                }
        //            }
        //        }

        //        filter.Save(this.GetSecFiltersFilePath(filter.FilterName));
        //    }

        //    filter.Load(this.GetSecFiltersFilePath(filter.FilterName));
        //}
        #endregion
       
        private void C_BtnCustom_Click(object sender, System.EventArgs e)
        {
            object item = null;

            ListBox selectedItemListBox = null;

            if (this.C_ListSecFilters.SelectedIndex >= 0)
            {
                item = this.C_ListSecFilters.SelectedItem;

                selectedItemListBox = C_ListSecFilters;
            }
            else if (this.C_ListSelectedSecFilters.SelectedIndex >= 0)
            {
                item = this.C_ListSelectedSecFilters.SelectedItem;

                selectedItemListBox = C_ListSelectedSecFilters;
            }

            if (item==null||!(item is SectionFilter)) return;

            SectionFilter filter = item as SectionFilter;

            filter.Filter.IsCustomFilter = true;

            EditSecFilterForm editform = new EditSecFilterForm(filter,this._WebbDBTypes);

            if (editform.ShowDialog(this) == DialogResult.OK)
            {
                SectionFilter newFilter = editform.Value;

                if (newFilter.FilterName != filter.FilterName)
                {
                    if (System.IO.File.Exists(this.GetSecFiltersFilePath(newFilter.FilterName)))
                    {
                        MessageBox.Show(string.Format("[{0}] was exist.", newFilter.FilterName));

                        return;
                    }
                    else
                    {
                        System.IO.File.Delete(this.GetSecFiltersFilePath(filter.FilterName));

                        selectedItemListBox.Items.Remove(filter);

                        selectedItemListBox.Items.Add(newFilter);

                        selectedItemListBox.SelectedItem = newFilter;

                    }
                }
                else
                {
                    filter.Apply(newFilter);
                }

                newFilter.Save(this.GetSecFiltersFilePath(newFilter.FilterName));
            }


           
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

        private bool ListBoxContainSecFilter(ListBox lstBox, SectionFilter secFilter)
        {
            foreach (SectionFilter scFilter in lstBox.Items)
            {
                if (scFilter.FilterName == secFilter.FilterName) return true;
            }

            return false;
        }

		//Clear All
		private void C_BtnClearAll_Click(object sender, System.EventArgs e)
		{
			foreach(object item in this.C_ListSelectedSecFilters.Items)
			{
				if(!(item is SectionFilter)) continue;

                SectionFilter scFilter = item as SectionFilter;

                if (this.ListBoxContainSecFilter(this.C_ListSecFilters, scFilter)) continue;

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

			if(item.ToString()!="[NoValue]")
			{
				this.C_ListSecFilters.Items.Remove(item);
			}
		}

		private void ToLeft()
		{
			object item = this.C_ListSelectedSecFilters.SelectedItem;

			if(!(item is SectionFilter)) return;		

			this.C_ListSelectedSecFilters.Items.Remove(item);

            foreach (SectionFilter scFilter in this.C_ListSecFilters.Items)
            {
                if (scFilter.ToString() == item.ToString()) return;
            }
           
            this.C_ListSecFilters.Items.Add(item);
           
		}

		//
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

    
        //New
		private void C_BtnNew_Click(object sender, System.EventArgs e)
		{
			this.CreateSecFiltersFolder();

            #region Old

            //SectionFilter filter = new SectionFilter();

            //filter.FilterName = string.Empty;

            //_SaveFileDialog.FileName=string.Empty;

            //if(this._SaveFileDialog.ShowDialog(this) == DialogResult.OK)
            //{
            //    filter.FilterName = this._SaveFileDialog.FileName;

            //    filter.Filter.IsCustomFilter=true;

            //    string strFile = this.GetSecFiltersFilePath(filter.FilterName);

            //    if(System.IO.File.Exists(strFile))
            //    {
            //        if(MessageBox.Show(this,"The filter file is existed , do you want overwrite it?","Warnning",MessageBoxButtons.YesNo) == DialogResult.No)
            //        {
            //            return;
            //        }
            //    }

            //    if(!System.IO.File.Exists(strFile))
            //    {
            //        this.C_ListSecFilters.Items.Add(filter);
            //    }

            //    filter.Save(strFile);
            //}
            #endregion

            #region  New 
            EditSecFilterForm editSecForm = new EditSecFilterForm(this._WebbDBTypes);

            if (editSecForm.ShowDialog(this) == DialogResult.OK)
            {
               SectionFilter filter = editSecForm.Value;

                filter.Filter.IsCustomFilter = true;

                string strFile = this.GetSecFiltersFilePath(filter.FilterName);

                if (System.IO.File.Exists(strFile))
                {
                    if (MessageBox.Show(this, "The filter file is existed , do you want overwrite it?", "Warnning", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        return;
                    }
                }

                if (!System.IO.File.Exists(strFile))
                {
                    this.C_ListSecFilters.Items.Add(filter);
                }

                filter.Save(strFile);
            }
            #endregion

        }
     
        //Delete
		private void C_BtnDel_Click(object sender, System.EventArgs e)
		{
			object item = this.C_ListSecFilters.SelectedItem;

			if(!(item is SectionFilter)) return;

			SectionFilter filter = item as SectionFilter;
			
			string strFilePath = this.GetSecFiltersFilePath(filter.FilterName);
			
			if(MessageBox.Show(this,string.Format("Do you want to delete [{0}]?",filter.FilterName),"Warnning",MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				if(System.IO.File.Exists(strFilePath))
				{
					System.IO.File.Delete(strFilePath);	
				}
				this.C_ListSecFilters.Items.Remove(item);
				this.C_ListSelectedSecFilters.Items.Remove(item);
			}
		}

		private string GetSecFiltersFileFolder()
		{
            string strFolder=Webb.Utility.ApplicationDirectory + Webb.Utility.SectionFiltersPath + "\\";	

            switch (this._WebbDBTypes)
            {
                case WebbDBTypes.WebbVictoryFootball:
                case WebbDBTypes.WebbVictoryBasketball:
                case WebbDBTypes.WebbVictoryVolleyball:
                case WebbDBTypes.WebbVictoryHockey:
                case WebbDBTypes.WebbVictoryLacrosse:
                case WebbDBTypes.WebbVictorySoccer:
                    strFolder+=@"Victory\";
                    break;
                case WebbDBTypes.CoachCRM :
                    strFolder += @"CoachCRM\";
                    break;
                default:
                    break;
            }
            return strFolder;
		}

		private void CreateSecFiltersFolder()
		{
			string strFolder = this.GetSecFiltersFileFolder();

			if(!System.IO.Directory.Exists(strFolder)) 
			{
				System.IO.Directory.CreateDirectory(strFolder);
			}
		}

		private string GetSecFiltersFilePath(string strStyleName)
		{
            char[] invalidChars = "/\\.\"|<>*?".ToCharArray();

            foreach (char c in invalidChars)
            {
                strStyleName = strStyleName.Replace(c,'&');
            }

            string strFolder = this.GetSecFiltersFileFolder();

            if (!System.IO.Directory.Exists(strFolder))
            {
                System.IO.Directory.CreateDirectory(strFolder);
            }

            return strFolder + strStyleName + ".wrsf";
		}

		//Fill Left List
		private void LoadSectionFilters()
		{
			this.C_ListSecFilters.Items.Clear();

			this.C_ListSelectedSecFilters.Items.Clear();

			string strFolder = this.GetSecFiltersFileFolder();

			if(!System.IO.Directory.Exists(strFolder)) return;

			string[] strFiles = System.IO.Directory.GetFiles(strFolder);

			ArrayList arrList=new ArrayList();

			if(this._InitSectionFilters!=null)
			{
				foreach(SectionFilter scFilter in this._InitSectionFilters)
				{
					scFilter.Filter.IsCustomFilter=true;

					this.C_ListSelectedSecFilters.Items.Add(scFilter);					

					arrList.Add(scFilter.FilterName);
				}
			}			
			
			foreach(string strFile in strFiles)
			{
				SectionFilter filter = new SectionFilter();

				if(!filter.Load(strFile)) continue;

				if(!arrList.Contains(filter.FilterName))
				{
					filter.Filter.IsCustomFilter=true;

					this.C_ListSecFilters.Items.Add(filter);
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

		private void C_ListSecFilters_SelectedIndexChanged(object sender, System.EventArgs e)
        {
        //    object item = this.C_ListSecFilters.SelectedItem;

        //    this.C_PropertyGrid.SelectedObject = item;

        //    if(this.C_ListSecFilters.SelectedIndex>=0)
        //    {
        //        this.C_ListSelectedSecFilters.SelectedIndex=-1;
        //    }
		}

		private void C_PropertyGrid_PropertyValueChanged(object s, System.Windows.Forms.PropertyValueChangedEventArgs e)
		{
			
		}

		private void C_BtnCopy_Click(object sender, System.EventArgs e)
		{
			object item = this.C_ListSecFilters.SelectedItem;

			if(!(item is SectionFilter)) return;

			SectionFilter filter = new SectionFilter((item as SectionFilter).Filter); 

			if(this._SaveFileDialog.ShowDialog(this) == DialogResult.OK)
			{
				filter.FilterName = this._SaveFileDialog.FileName;

				string strFile = this.GetSecFiltersFilePath(filter.FilterName);

				if(System.IO.File.Exists(strFile))
				{
					if(MessageBox.Show(this,"The filter file is existed , do you want overwrite it?","Warnning",MessageBoxButtons.YesNo)!= DialogResult.Yes)
					{
						return;
					}
				}
				else
				{
					this.C_ListSecFilters.Items.Add(filter);
				}

				filter.Save(strFile);
			}
		}

		private void C_ListSelectedSecFilters_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.C_ListSelectedSecFilters.SelectedIndex>=0)
			{
				this.C_ListSecFilters.SelectedIndex=-1;
			}
		}

     
	}
}
