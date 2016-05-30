using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using DevExpress.XtraPrinting;
using System.Drawing.Drawing2D;
using Webb.Utilities;


namespace Webb.Reports.Editors
{
    #region public class SectionFiltersEditorForm : System.Windows.Forms.Form
    /// <summary>
    /// Summary description for SectionFiltersEditorForm
    /// </summary>
    public class SectionFiltersEditorForm : System.Windows.Forms.Form
    {
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TreeView C_FilterList;
        private System.Windows.Forms.Button BtnLoadCustomSectionFilters;
        private Button BtnLoadAdvReportFilters;
        private Button Btnok;
        private Button BtnCancel;
        private Panel panel1;
        private ComboBox cmbAttribute;
        private Label label1;
        private GroupBox groupBox2;
        private Label label2;
        public SectionFilterCollection SectionFilters=new SectionFilterCollection();
		public SectionFilterCollectionWrapper SectionFiltersWrapper = new SectionFilterCollectionWrapper();	//Modified at 2009-1-15 10:42:12@Scott
        private Webb.Reports.DataProvider.WebbDataProvider DataProvider = Webb.Reports.DataProvider.VideoPlayBackManager.PublicDBProvider;
       
		private System.Windows.Forms.Button button1;
        private ReportScType rsctype=ReportScType.Custom;
      
        public SectionFiltersEditorForm(object value)
        {   
            if(DataProvider==null)this.DataProvider=new Webb.Reports.DataProvider.WebbDataProvider();            
            InitializeComponent();
            this.cmbAttribute.SelectedIndex =0;
			if(value!=null)
			{
				SectionFilterCollection i_SectionFilters = null;

				if(value is SectionFilterCollectionWrapper)
				{
					i_SectionFilters = (value as SectionFilterCollectionWrapper).SectionFilters;	//Modified at 2009-1-15 10:27:08@Scott
					int index= (int)((value as SectionFilterCollectionWrapper).ReportScType);	//Modified at 2009-1-15 11:26:10@Scott
                    
                    if (index < 5)
                    {
                        this.cmbAttribute.SelectedIndex = index;
                    }
                    else
                    {
                        this.cmbAttribute.SelectedIndex = this.cmbAttribute.Items.Count-1;
                    }

                    if((value as SectionFilterCollectionWrapper).ReportScType != ReportScType.Custom)
					{
						this.LoadAdvSectionFilters(string.Empty,(value as SectionFilterCollectionWrapper).ReportScType);
						this.AddFilters(this.SectionFilters);
						return;
					}
				}
				else if(value is SectionFilterCollection)
				{
					i_SectionFilters = value as SectionFilterCollection;
				}
				else
				{
					i_SectionFilters = new SectionFilterCollection();
				}
				this.AddFilters(i_SectionFilters);
				this.SectionFilters=i_SectionFilters;	
				this.SectionFiltersWrapper.SectionFilters = this.SectionFilters;	//Modified at 2009-1-15 10:43:11@Scott
			}
        }

        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.BtnLoadAdvReportFilters = new System.Windows.Forms.Button();
            this.BtnLoadCustomSectionFilters = new System.Windows.Forms.Button();
            this.C_FilterList = new System.Windows.Forms.TreeView();
            this.Btnok = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.cmbAttribute = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.BtnLoadAdvReportFilters);
            this.groupBox1.Controls.Add(this.BtnLoadCustomSectionFilters);
            this.groupBox1.Location = new System.Drawing.Point(2, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(179, 167);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Load Section Filters:";
            // 
            // BtnLoadAdvReportFilters
            // 
            this.BtnLoadAdvReportFilters.Location = new System.Drawing.Point(10, 104);
            this.BtnLoadAdvReportFilters.Name = "BtnLoadAdvReportFilters";
            this.BtnLoadAdvReportFilters.Size = new System.Drawing.Size(160, 45);
            this.BtnLoadAdvReportFilters.TabIndex = 1;
            this.BtnLoadAdvReportFilters.Text = "Load AdvReport Filters";
            this.BtnLoadAdvReportFilters.Click += new System.EventHandler(this.BtnLoadAdvReportFilters_Click);
            // 
            // BtnLoadCustomSectionFilters
            // 
            this.BtnLoadCustomSectionFilters.Location = new System.Drawing.Point(10, 42);
            this.BtnLoadCustomSectionFilters.Name = "BtnLoadCustomSectionFilters";
            this.BtnLoadCustomSectionFilters.Size = new System.Drawing.Size(160, 45);
            this.BtnLoadCustomSectionFilters.TabIndex = 0;
            this.BtnLoadCustomSectionFilters.Text = "Load  Custom Section Filters";
            this.BtnLoadCustomSectionFilters.Click += new System.EventHandler(this.BtnLoadCustomSectionFilters_Click);
            // 
            // C_FilterList
            // 
            this.C_FilterList.Location = new System.Drawing.Point(184, 6);
            this.C_FilterList.Name = "C_FilterList";
            this.C_FilterList.Size = new System.Drawing.Size(202, 247);
            this.C_FilterList.TabIndex = 1;
            this.C_FilterList.DoubleClick += new System.EventHandler(this.C_FilterList_DoubleClick);
            // 
            // Btnok
            // 
            this.Btnok.Location = new System.Drawing.Point(10, 8);
            this.Btnok.Name = "Btnok";
            this.Btnok.Size = new System.Drawing.Size(75, 23);
            this.Btnok.TabIndex = 0;
            this.Btnok.Text = "OK";
            this.Btnok.Click += new System.EventHandler(this.button4_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Location = new System.Drawing.Point(92, 8);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(75, 23);
            this.BtnCancel.TabIndex = 1;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.BtnCancel);
            this.panel1.Controls.Add(this.Btnok);
            this.panel1.Location = new System.Drawing.Point(7, 256);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(377, 37);
            this.panel1.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(288, 8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(74, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Clear";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmbAttribute
            // 
            this.cmbAttribute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAttribute.Items.AddRange(new object[] {
            "Custom",
            "Field Zone Offense",
            "Down And Distance Offense",
            "Field Zone Defense",
            "Down And Distance Defense"});
            this.cmbAttribute.Location = new System.Drawing.Point(10, 50);
            this.cmbAttribute.Name = "cmbAttribute";
            this.cmbAttribute.Size = new System.Drawing.Size(148, 20);
            this.cmbAttribute.TabIndex = 0;
            this.cmbAttribute.SelectedIndexChanged += new System.EventHandler(this.cmbAttribute_SelectedIndexChanged);
            this.cmbAttribute.Click += new System.EventHandler(this.cmbAttribute_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Attributes :";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cmbAttribute);
            this.groupBox2.Location = new System.Drawing.Point(2, 160);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(180, 88);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            // 
            // SectionFiltersEditorForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(390, 301);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.C_FilterList);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "SectionFiltersEditorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SectionFilters Editor";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

		}

        public bool LoadAdvSectionFilters(string strUserFolder,ReportScType rsctype)
        {//Modified at 2009-1-19 14:23:11@Scott
            AdvFilterConvertor convertor = new AdvFilterConvertor();

            this.SectionFilters = convertor.GetReportFilters(Webb.Reports.DataProvider.VideoPlayBackManager.AdvReportFilters,rsctype);	//add 1-19-2008 scott
			
			this.SectionFiltersWrapper.SectionFilters = this.SectionFilters;	//Modified at 2009-1-15 10:43:11@Scott

            return true;
        }

        private void AddFilters(SectionFilterCollection i_SectionFilters)
        {
            this.C_FilterList.Nodes.Clear();

            foreach (SectionFilter m_Filter in i_SectionFilters)
            {
                TreeNode m_Node = new TreeNode(m_Filter.FilterName);
                m_Node.Tag = m_Filter;
                this.C_FilterList.Nodes.Add(m_Node);
            }
        }

        private void RemoveSectionFilters()
        {
            this.C_FilterList.Nodes.Clear();

            this.SectionFilters.Clear();

			this.SectionFiltersWrapper.SectionFilters = this.SectionFilters;	//Modified at 2009-1-15 10:43:11@Scott
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void BtnLoadCustomSectionFilters_Click(object sender, EventArgs e)
        {
			if(this.cmbAttribute.SelectedIndex != 0)
			{
				Webb.Utilities.MessageBoxEx.ShowError("Can not convert Non-Custom sections to custom, Please change the 'Attribute' to 'Custom'!");
				
				return;
			}
            if (this.SectionFilters == null) this.SectionFilters = new SectionFilterCollection();

            DialogResult m_result = this.DataProvider.ShowEditorCustomSectionFilterSelector(this.SectionFilters);

            if (m_result == DialogResult.OK)
            {
				this.cmbAttribute.SelectedIndex = 0;

                this.AddFilters(this.SectionFilters);
             
				this.SectionFiltersWrapper.SectionFilters = this.SectionFilters;	//Modified at 2009-1-15 10:43:11@Scott
            }
        }

        private void BtnLoadAdvReportFilters_Click(object sender, EventArgs e)
        {
			if(this.cmbAttribute.SelectedIndex != 0)
			{
				Webb.Utilities.MessageBoxEx.ShowError("Can not convert Non-Custom sections to custom, Please change the 'Attribute' to 'Custom'!");
				
				return;
			}
            if (this.SectionFilters == null) this.SectionFilters = new SectionFilterCollection();

            DialogResult m_result = this.DataProvider.ShowAdvReportFilterSelector(this.SectionFilters);

            if (m_result == DialogResult.OK)
            {
				this.cmbAttribute.SelectedIndex = 0;

                this.AddFilters(this.SectionFilters);
               
				this.SectionFiltersWrapper.SectionFilters = this.SectionFilters;	//Modified at 2009-1-15 10:43:11@Scott
            }
        }
		private bool CheckIsAdvantageFilter()
		{
           return false;
		}

     
        private void cmbAttribute_Click(object sender, EventArgs e)
        {

        }

        private void cmbAttribute_SelectedIndexChanged(object sender, EventArgs e)
        { 
            if (this.cmbAttribute.SelectedIndex < 0)
            {
                return;
            }

            if (this.DataProvider.DBSourceConfig.UserFolder == null) return;

            int selectIndex=this.cmbAttribute.SelectedIndex;

            //if (selectIndex == 5)
            //{
            //    rsctype = ReportScType.OffenseDefineEfficiency;
            //}
            //else if (selectIndex == 6)
            //{
            //    rsctype = ReportScType.DefenseDefineEfficiency;
            //}
            //else
            //{
                rsctype = (ReportScType)selectIndex;
            //}

			this.SectionFiltersWrapper.ReportScType = rsctype;	//Modified at 2009-1-15 11:02:27@Scott
            if (this.cmbAttribute.SelectedIndex == 0)
            {
                this.RemoveSectionFilters();

                return;
            }

            string strUserFolder = this.DataProvider.DBSourceConfig.UserFolder.Trim(";".ToCharArray());

			int index=strUserFolder.IndexOf(";");

			if(index>=0)
			{
				strUserFolder=strUserFolder.Substring(0,index);                 

			}

            this.LoadAdvSectionFilters(strUserFolder, rsctype);

            this.AddFilters(SectionFilters);
        }

		private void button1_Click(object sender, System.EventArgs e)
		{
			this.cmbAttribute.SelectedIndex = 0;

			this.RemoveSectionFilters();
		}

		private bool Contains(string filterName)
		{
			foreach(SectionFilter scFilter in this.SectionFilters)
			{
				if(scFilter.FilterName==filterName)
				{
					return true;
				}
			}
			return false;

		}
		private void UpdateScFilters(string strName,SectionFilter newFilter)
		{
			foreach(SectionFilter scFilter in this.SectionFilters)
			{
				if(scFilter.FilterName==strName)
				{
                  scFilter.Apply( newFilter);
				}
			}
			this.SectionFiltersWrapper.SectionFilters=this.SectionFilters;

			this.AddFilters(this.SectionFilters);
		}

		private void C_FilterList_DoubleClick(object sender, System.EventArgs e)
		{
			if(this.C_FilterList.SelectedNode==null)return;

			if(this.cmbAttribute.SelectedIndex>0)
			{
				Webb.Utilities.MessageBoxEx.ShowError("Can not edit advantage filters!");

				return;
			}

			SectionFilter scFilter=	this.C_FilterList.SelectedNode.Tag as SectionFilter;		

			if(scFilter==null) return;

             SectionFilter editFilter=new SectionFilter();

			editFilter.Apply(scFilter);

			PropertyForm _PropertyForm=new PropertyForm();

			string strOldName = editFilter.FilterName;

			string oldinnerFilterName=editFilter.Filter.Name;

			_PropertyForm.BindProperty(editFilter);

			if(_PropertyForm.ShowDialog(this) == DialogResult.OK)
			{
				if(strOldName != editFilter.FilterName)
				{
					bool exist=this.Contains(editFilter.FilterName);

					if(exist)
					{
						Webb.Utilities.MessageBoxEx.ShowError("FilterName'"+editFilter.FilterName+"' has been exist in thes filters!");
						return;
					}
					else
					{
						this.UpdateScFilters(strOldName,editFilter);
					}
				}
				else
				{
					this.UpdateScFilters(strOldName,editFilter);
				}						
			}
        }      
    }
    #endregion

    #region class  SectionFiltersEditor
    public class SectionFiltersEditor : System.Drawing.Design.UITypeEditor
    {
        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        // Displays the UI for value selection.
        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {

            if (!(value is SectionFilterCollection) && !(value is SectionFilterCollectionWrapper))	//Modified at 2009-1-15 10:29:03@Scott
                return value;

            IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

            SectionFiltersEditorForm SectionFiltersForm = new SectionFiltersEditorForm(value);
            if (edSvc != null)
            {
                if (edSvc.ShowDialog(SectionFiltersForm) == DialogResult.OK)
                {
					if(value is SectionFilterCollection) return SectionFiltersForm.SectionFilters;
                
					if(value is SectionFilterCollectionWrapper) return SectionFiltersForm.SectionFiltersWrapper;	//Modified at 2009-1-15 10:44:48@Scott
				}
            }
            return value;
        }


        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        public override void PaintValue(System.Drawing.Design.PaintValueEventArgs e)
        {
        }


        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        public override bool GetPaintValueSupported(System.ComponentModel.ITypeDescriptorContext context)
        {
            return true;
        }
    }
    #endregion
}