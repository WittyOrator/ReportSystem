using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Webb.Utilities
{
	/// <summary>
	/// Summary description for ProductAboutForm.
	/// </summary>
	public class ProductAboutForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label C_LabelVersion;
		private System.Windows.Forms.Label C_LabelCopyright;
		private System.Windows.Forms.Label C_LabelAddress;
		private System.Windows.Forms.Label C_LabelContact;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label C_LabelProduct;
		private System.Windows.Forms.Label C_LabelCompany;
		private System.Windows.Forms.LinkLabel C_LinkLabelWebSite;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ProductAboutForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			
			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			this.Load += new EventHandler(ProductAboutForm_Load);
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ProductAboutForm));
			this.button1 = new System.Windows.Forms.Button();
			this.C_LabelProduct = new System.Windows.Forms.Label();
			this.C_LabelVersion = new System.Windows.Forms.Label();
			this.C_LabelCopyright = new System.Windows.Forms.Label();
			this.C_LabelCompany = new System.Windows.Forms.Label();
			this.C_LabelAddress = new System.Windows.Forms.Label();
			this.C_LabelContact = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.C_LinkLabelWebSite = new System.Windows.Forms.LinkLabel();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(204, 11);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(63, 21);
			this.button1.TabIndex = 0;
			this.button1.Text = "OK";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// C_LabelProduct
			// 
			this.C_LabelProduct.Location = new System.Drawing.Point(72, 15);
			this.C_LabelProduct.Name = "C_LabelProduct";
			this.C_LabelProduct.Size = new System.Drawing.Size(126, 21);
			this.C_LabelProduct.TabIndex = 1;
			this.C_LabelProduct.Text = "Product";
			this.C_LabelProduct.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// C_LabelVersion
			// 
			this.C_LabelVersion.Location = new System.Drawing.Point(72, 37);
			this.C_LabelVersion.Name = "C_LabelVersion";
			this.C_LabelVersion.Size = new System.Drawing.Size(160, 21);
			this.C_LabelVersion.TabIndex = 2;
			this.C_LabelVersion.Text = "Version";
			this.C_LabelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// C_LabelCopyright
			// 
			this.C_LabelCopyright.Location = new System.Drawing.Point(64, 59);
			this.C_LabelCopyright.Name = "C_LabelCopyright";
			this.C_LabelCopyright.Size = new System.Drawing.Size(160, 22);
			this.C_LabelCopyright.TabIndex = 3;
			this.C_LabelCopyright.Text = "Copyright";
			this.C_LabelCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// C_LabelCompany
			// 
			this.C_LabelCompany.Location = new System.Drawing.Point(32, 82);
			this.C_LabelCompany.Name = "C_LabelCompany";
			this.C_LabelCompany.Size = new System.Drawing.Size(216, 21);
			this.C_LabelCompany.TabIndex = 4;
			this.C_LabelCompany.Text = "Company";
			this.C_LabelCompany.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// C_LabelAddress
			// 
			this.C_LabelAddress.Location = new System.Drawing.Point(25, 104);
			this.C_LabelAddress.Name = "C_LabelAddress";
			this.C_LabelAddress.Size = new System.Drawing.Size(220, 21);
			this.C_LabelAddress.TabIndex = 5;
			this.C_LabelAddress.Text = "Address";
			this.C_LabelAddress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// C_LabelContact
			// 
			this.C_LabelContact.Location = new System.Drawing.Point(25, 126);
			this.C_LabelContact.Name = "C_LabelContact";
			this.C_LabelContact.Size = new System.Drawing.Size(221, 22);
			this.C_LabelContact.TabIndex = 6;
			this.C_LabelContact.Text = "Contact";
			this.C_LabelContact.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(13, 16);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(32, 32);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 7;
			this.pictureBox1.TabStop = false;
			// 
			// C_LinkLabelWebSite
			// 
			this.C_LinkLabelWebSite.Location = new System.Drawing.Point(55, 163);
			this.C_LinkLabelWebSite.Name = "C_LinkLabelWebSite";
			this.C_LinkLabelWebSite.Size = new System.Drawing.Size(160, 22);
			this.C_LinkLabelWebSite.TabIndex = 0;
			this.C_LinkLabelWebSite.TabStop = true;
			this.C_LinkLabelWebSite.Text = "WebSite";
			this.C_LinkLabelWebSite.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// ProductAboutForm
			// 
			
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.SystemColors.Window;
			this.ClientSize = new System.Drawing.Size(273, 200);
			this.ControlBox = false;
			this.Controls.Add(this.C_LinkLabelWebSite);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.C_LabelContact);
			this.Controls.Add(this.C_LabelAddress);
			this.Controls.Add(this.C_LabelCompany);
			this.Controls.Add(this.C_LabelCopyright);
			this.Controls.Add(this.C_LabelVersion);
			this.Controls.Add(this.C_LabelProduct);
			this.Controls.Add(this.button1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "ProductAboutForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "About";
			this.TopMost = true;
			this.ResumeLayout(false);

		}
		#endregion

		private string GetVersion()  //2009-7-13 11:16:14@Simon Add this Code
		{
			if(Webb.Assembly.Version.Length>7)
			{
				string Ver=Webb.Assembly.Version.Substring(0,7);
				string InternalVer=Webb.Assembly.Version.Remove(0,7);
				
				if(InternalVer.Length!=1)
				{
					return "Internal Version:"+ Ver+"."+InternalVer;
				}
				else
				{
					int number=int.Parse(InternalVer);

					char a=(char)((short)'a'+number);

					return "Release Version:"+ Ver+a.ToString();
				}
			}
			
			return "Release Version:"+ Webb.Assembly.Version;
		}

		private void ProductAboutForm_Load(object sender, EventArgs e)
		{
			this.C_LabelProduct.Text = Webb.Assembly.Product;
			this.C_LabelVersion.Text =GetVersion();  //2009-7-13 11:16:10@Simon Add this Code
			this.C_LabelCopyright.Text = Webb.Assembly.Copyright;
			this.C_LabelCompany.Text = Webb.Assembly.Company;
			this.C_LabelAddress.Text = Webb.Company.Address;
			this.C_LabelContact.Text = string.Format("Phone:{0} Fax:{1}",Webb.Company.Telephone,Webb.Company.Fax);
			this.C_LinkLabelWebSite.Text = Webb.Company.WebSite;
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void C_LinkLabelWebSite_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start(this.C_LinkLabelWebSite.Text);
		}
	}
}
