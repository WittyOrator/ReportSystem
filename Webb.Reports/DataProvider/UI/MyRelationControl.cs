using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace Webb.Reports.DataProvider.UI
{
	/// <summary>
	/// Summary description for CustomRelationControl.
	/// </summary>
	public class MyRelationControl : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.ComboBox cmbFirst;
		private System.Windows.Forms.Label lblEqual;
		private System.Windows.Forms.ComboBox cmbSecond;


		private ArrayList arrList=new ArrayList();
		private System.Windows.Forms.ComboBox cmbAnd;

		public object ParentConfig=null;
  
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public MyRelationControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			this.cmbAnd.SelectedIndex=0;

			// TODO: Add any initialization after the InitializeComponent call

		}


		public void SetData(ArrayList strFields)
		{
			// TODO: Add any initialization after the InitializeComponent call

			this.cmbFirst.Items.Clear();

			this.cmbSecond.Items.Clear();

			arrList.Clear();

			foreach(string strField in strFields)
			{
				this.cmbFirst.Items.Add(strField);

				this.cmbSecond.Items.Add(strField);

				arrList.Add(strField);
			}

			

			


		}
		

		public string GetResult()
		{
		   string strFirst=this.cmbFirst.Text.Trim();
		   string strSecond = this.cmbSecond.Text.Trim();
			if(strFirst==string.Empty||strSecond==string.Empty)
			{
				return string.Empty;
			}
			return string.Format(@"{0}={1} {2} ",strFirst,strSecond,this.cmbAnd.Text);
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
			this.cmbFirst = new System.Windows.Forms.ComboBox();
			this.lblEqual = new System.Windows.Forms.Label();
			this.cmbSecond = new System.Windows.Forms.ComboBox();
			this.cmbAnd = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// cmbFirst
			// 
			this.cmbFirst.Location = new System.Drawing.Point(8, 0);
			this.cmbFirst.Name = "cmbFirst";
			this.cmbFirst.Size = new System.Drawing.Size(256, 21);
			this.cmbFirst.Sorted = true;
			this.cmbFirst.TabIndex = 0;
			this.cmbFirst.SelectedIndexChanged += new System.EventHandler(this.cmbFirst_SelectedIndexChanged);
			// 
			// lblEqual
			// 
			this.lblEqual.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblEqual.Location = new System.Drawing.Point(264, 0);
			this.lblEqual.Name = "lblEqual";
			this.lblEqual.Size = new System.Drawing.Size(16, 16);
			this.lblEqual.TabIndex = 1;
			this.lblEqual.Text = "=";
			// 
			// cmbSecond
			// 
			this.cmbSecond.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left)));
			this.cmbSecond.Location = new System.Drawing.Point(280, 0);
			this.cmbSecond.Name = "cmbSecond";
			this.cmbSecond.Size = new System.Drawing.Size(224, 21);
			this.cmbSecond.Sorted = true;
			this.cmbSecond.TabIndex = 2;
			this.cmbSecond.SelectedIndexChanged += new System.EventHandler(this.cmbSecond_SelectedIndexChanged);
			// 
			// cmbAnd
			// 
			this.cmbAnd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbAnd.Items.AddRange(new object[] {
														"and",
														"or"});
			this.cmbAnd.Location = new System.Drawing.Point(504, 0);
			this.cmbAnd.Name = "cmbAnd";
			this.cmbAnd.Size = new System.Drawing.Size(56, 21);
			this.cmbAnd.TabIndex = 3;
			this.cmbAnd.SelectedIndexChanged += new System.EventHandler(this.cmbAnd_SelectedIndexChanged);
			// 
			// MyRelationControl
			// 
			this.Controls.Add(this.cmbAnd);
			this.Controls.Add(this.cmbSecond);
			this.Controls.Add(this.lblEqual);
			this.Controls.Add(this.cmbFirst);
			this.Name = "MyRelationControl";
			this.Size = new System.Drawing.Size(562, 24);
			this.ResumeLayout(false);

		}
		#endregion

		private void cmbFirst_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			int index=this.cmbFirst.Text.IndexOf(".");

			if(index<=0)return;

			string text=this.cmbFirst.Text.Substring(0,index+1);

			for(int i=this.cmbSecond.Items.Count-1;i>=0;i--)
			{
				if(this.cmbSecond.Items[i].ToString().StartsWith(text))
				{
					this.cmbSecond.Items.RemoveAt(i);
                }
			}

            if (ParentConfig is ConfigComonTable)
            {
                (ParentConfig as ConfigComonTable).SetSQLText();
            }
            else if (ParentConfig is ConfigCommonAccess)
            {
                (ParentConfig as ConfigCommonAccess).SetSQLText();
            }
		}

		private void cmbSecond_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			int index=this.cmbSecond.Text.IndexOf(".");

			if(index<=0)return;

			string text=this.cmbSecond.Text.Substring(0,index+1);

			for(int i=this.cmbFirst.Items.Count-1;i>=0;i--)
			{
				if(this.cmbFirst.Items[i].ToString().StartsWith(text))
				{
					this.cmbFirst.Items.RemoveAt(i);
				}
			}

            if (ParentConfig is ConfigComonTable)
            {
                (ParentConfig as ConfigComonTable).SetSQLText();
            }
            else if (ParentConfig is ConfigCommonAccess)
            {
                (ParentConfig as ConfigCommonAccess).SetSQLText();
            }
		}

		private void cmbAnd_SelectedIndexChanged(object sender, System.EventArgs e)
		{
            if (ParentConfig is ConfigComonTable)
            {
                (ParentConfig as ConfigComonTable).SetSQLText();
            }
            else if (ParentConfig is ConfigCommonAccess)
            {
                (ParentConfig as ConfigCommonAccess).SetSQLText();
            }
		}
	}
}
