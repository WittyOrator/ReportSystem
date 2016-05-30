using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;



namespace Webb.Reports.ReportWizard.Wizards.Steps
{
	/// <summary>
	/// Summary description for StepControl.
	/// </summary>
	public class WizardPageControlBase: System.Windows.Forms.UserControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		protected string _Title = "Wizard Step 0";
		protected  object _ParentWizardForm;	

		protected Webb.Reports.ReportWizard.WizardInfo.ReportSetting _setting;
		
		[Browsable(false)]
		public object ParentWizardForm
		{
			get{return this._ParentWizardForm;}
			set{this._ParentWizardForm = value;}
		}

		public string WizardTitle
		{
			get{return this._Title;}
			set{this._Title = value;}
		}

		public WizardPageControlBase()
		{
           
            

			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

		}

        public void lblhelpContext_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            MessageBox.Show("No");
            hlpevent.Handled = true;
        }
		public virtual bool UpdateSetting(ref Webb.Reports.ReportWizard.WizardInfo.ReportSetting setting)
		{
			return true;
		}
		public virtual void SetSetting(Webb.Reports.ReportWizard.WizardInfo.ReportSetting setting)
		{
			
		}

		public void DrawBackImage(Graphics g,Image image)
		{
            g.SmoothingMode=System.Drawing.Drawing2D.SmoothingMode.HighSpeed;			

			g.DrawImage(image,this.ClientRectangle);
		
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
            this.SuspendLayout();
            // 
            // WizardPageControlBase
            // 
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "WizardPageControlBase";
            this.Size = new System.Drawing.Size(817, 473);
            this.ResumeLayout(false);

		}
		#endregion

    }

	
}
