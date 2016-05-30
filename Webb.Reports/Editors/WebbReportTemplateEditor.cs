using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using DevExpress.XtraPrinting;
using System.Drawing.Drawing2D;


namespace Webb.Reports.Editors
{
    #region public class WebbReportEditorForm : System.Windows.Forms.Form
    /// <summary>
    /// Summary description for SectionFiltersEditorForm
    /// </summary>
    public class WebbReportEditorForm : System.Windows.Forms.Form
    {
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage C_TabPageOneValue;
		private System.Windows.Forms.TabPage C_TabPageRepeat;
        public Webb.Reports.WebbReportTemplate Template = new WebbReportTemplate();
      
        public WebbReportEditorForm(object value)
        {    
            InitializeComponent();
			
        }

        private void InitializeComponent()
        {
			this.panel1 = new System.Windows.Forms.Panel();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.C_TabPageOneValue = new System.Windows.Forms.TabPage();
			this.C_TabPageRepeat = new System.Windows.Forms.TabPage();
			this.panel1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.tabControl1);
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(568, 336);
			this.panel1.TabIndex = 0;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.C_TabPageOneValue);
			this.tabControl1.Controls.Add(this.C_TabPageRepeat);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(568, 336);
			this.tabControl1.TabIndex = 0;
			// 
			// C_TabPageOneValue
			// 
			this.C_TabPageOneValue.Location = new System.Drawing.Point(4, 21);
			this.C_TabPageOneValue.Name = "C_TabPageOneValue";
			this.C_TabPageOneValue.Size = new System.Drawing.Size(560, 311);
			this.C_TabPageOneValue.TabIndex = 0;
			this.C_TabPageOneValue.Text = "One Value Per Page Setting";
			// 
			// C_TabPageRepeat
			// 
			this.C_TabPageRepeat.Location = new System.Drawing.Point(4, 21);
			this.C_TabPageRepeat.Name = "C_TabPageRepeat";
			this.C_TabPageRepeat.Size = new System.Drawing.Size(560, 311);
			this.C_TabPageRepeat.TabIndex = 1;
			this.C_TabPageRepeat.Text = "Repeat Setting";
			// 
			// WebbReportEditorForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(570, 415);
			this.ControlBox = false;
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "WebbReportEditorForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Webb Report Editor";
			this.panel1.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
    }
    #endregion

    #region class  WebbReportTemplateEditor
    public class WebbReportTemplateEditor : System.Drawing.Design.UITypeEditor
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
            if (!(value is Webb.Reports.WebbReportTemplate))
                return value;

            IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

            WebbReportEditorForm webbReportForm = new WebbReportEditorForm(value);
            if (edSvc != null)
            {
                if (edSvc.ShowDialog(webbReportForm) == DialogResult.OK)
                {
                    return webbReportForm.Template;
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
            return false;
        }
    }
    #endregion
}