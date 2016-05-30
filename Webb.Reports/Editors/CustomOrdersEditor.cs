using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using DevExpress.XtraPrinting;

using System.Collections;
using System.Collections.Specialized;



namespace Webb.Reports.Editors
{
	#region public class CustomOrdersEditorForm : System.Windows.Forms.Form	
	public class CustomOrdersEditorForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button BtnOk;
		private System.Windows.Forms.Button BtnCancel;
		private System.Windows.Forms.Button BtnUp;
		private System.Windows.Forms.Button BtnDown;
		private System.Windows.Forms.Button BtnReverse;
		private System.Windows.Forms.Button ReLoad;
		private System.Windows.Forms.ListBox LstValue;
		public StringCollection strResults=new StringCollection();

		private int indexoftarget,indexofsource;

		private void InitializeComponent()
		{
			this.BtnOk = new System.Windows.Forms.Button();
			this.BtnCancel = new System.Windows.Forms.Button();
			this.LstValue = new System.Windows.Forms.ListBox();
			this.BtnUp = new System.Windows.Forms.Button();
			this.BtnDown = new System.Windows.Forms.Button();
			this.BtnReverse = new System.Windows.Forms.Button();
			this.ReLoad = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// BtnOk
			// 
			this.BtnOk.Location = new System.Drawing.Point(96, 272);
			this.BtnOk.Name = "BtnOk";
			this.BtnOk.Size = new System.Drawing.Size(72, 24);
			this.BtnOk.TabIndex = 0;
			this.BtnOk.Text = "OK";
			this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
			// 
			// BtnCancel
			// 
			this.BtnCancel.Location = new System.Drawing.Point(200, 272);
			this.BtnCancel.Name = "BtnCancel";
			this.BtnCancel.Size = new System.Drawing.Size(72, 24);
			this.BtnCancel.TabIndex = 1;
			this.BtnCancel.Text = "Cancel";
			this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
			// 
			// LstValue
			// 
			this.LstValue.AllowDrop = true;
			this.LstValue.Location = new System.Drawing.Point(8, 8);
			this.LstValue.Name = "LstValue";
			this.LstValue.Size = new System.Drawing.Size(248, 251);
			this.LstValue.TabIndex = 2;
			this.LstValue.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listBox2_MouseDown);
			this.LstValue.DragOver += new System.Windows.Forms.DragEventHandler(this.listBox2_DragOver);
			this.LstValue.DragDrop += new System.Windows.Forms.DragEventHandler(this.listBox2_DragDrop);
			// 
			// BtnUp
			// 
			this.BtnUp.Location = new System.Drawing.Point(272, 24);
			this.BtnUp.Name = "BtnUp";
			this.BtnUp.Size = new System.Drawing.Size(72, 32);
			this.BtnUp.TabIndex = 3;
			this.BtnUp.Text = "MoveUp";
			this.BtnUp.Click += new System.EventHandler(this.BtnUp_Click);
			// 
			// BtnDown
			// 
			this.BtnDown.Location = new System.Drawing.Point(272, 72);
			this.BtnDown.Name = "BtnDown";
			this.BtnDown.Size = new System.Drawing.Size(72, 32);
			this.BtnDown.TabIndex = 4;
			this.BtnDown.Text = "MoveDown";
			this.BtnDown.Click += new System.EventHandler(this.BtnDown_Click);
			// 
			// BtnReverse
			// 
			this.BtnReverse.Location = new System.Drawing.Point(272, 136);
			this.BtnReverse.Name = "BtnReverse";
			this.BtnReverse.Size = new System.Drawing.Size(72, 24);
			this.BtnReverse.TabIndex = 5;
			this.BtnReverse.Text = "Reverse";
			this.BtnReverse.Click += new System.EventHandler(this.BtnReverse_Click);
			// 
			// ReLoad
			// 
			this.ReLoad.Location = new System.Drawing.Point(272, 176);
			this.ReLoad.Name = "ReLoad";
			this.ReLoad.Size = new System.Drawing.Size(72, 24);
			this.ReLoad.TabIndex = 6;
			this.ReLoad.Text = "Reload";
			this.ReLoad.Click += new System.EventHandler(this.ReLoad_Click);
			// 
			// CustomOrdersEditorForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(352, 310);
			this.ControlBox = false;
			this.Controls.Add(this.ReLoad);
			this.Controls.Add(this.BtnReverse);
			this.Controls.Add(this.BtnDown);
			this.Controls.Add(this.BtnUp);
			this.Controls.Add(this.LstValue);
			this.Controls.Add(this.BtnCancel);
			this.Controls.Add(this.BtnOk);
			this.Name = "CustomOrdersEditorForm";
			this.Text = "Orders Editor";
			this.ResumeLayout(false);

		}
	
		public CustomOrdersEditorForm(object value)
		{ 
			InitializeComponent();
            strResults=value as StringCollection;
            this.SetView();
             
		}
		private void SetView()
		{
			this.LstValue.Items.Clear();			
			foreach(string strvalue in this.strResults)
			{
				this.LstValue.Items.Add(strvalue);
			}
		}
		public void UpdateView()
		{
            strResults.Clear();
			foreach(string strvalue in this.LstValue.Items)
			{
				strResults.Add(strvalue);
			}
		}

		private void BtnOk_Click(object sender, System.EventArgs e)
		{
			this.UpdateView();
			DialogResult=DialogResult.OK;
		  
		}
	

		private void listBox2_MouseDown(object sender,    System.Windows.Forms.MouseEventArgs e)
		{
			indexofsource = ((ListBox)sender).IndexFromPoint(e.X, e.Y);
			if (indexofsource != ListBox.NoMatches)
			{
				((ListBox)sender).DoDragDrop(((ListBox)sender).Items[indexofsource].ToString(), DragDropEffects.All);
			}
		}
 
		private void listBox2_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
		{    //拖动源和放置的目的地一定是一个ListBox
			if (e.Data.GetDataPresent(typeof(System.String)) && ((ListBox)sender).Equals(this.LstValue))
			{
				e.Effect = DragDropEffects.Move;
			}
			else
				e.Effect = DragDropEffects.None;
		}

        
		private void listBox2_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
		{
			
			ListBox listbox=(ListBox)sender;

			indexoftarget=listbox.IndexFromPoint(listbox.PointToClient(new Point(e.X, e.Y)));

			if(indexofsource==indexoftarget)return;

            if(indexofsource<0||indexofsource>=this.LstValue.Items.Count)return;

			 if(indexoftarget<0||indexoftarget>=this.LstValue.Items.Count)return;

			if(indexoftarget!=ListBox.NoMatches)
			{
				string temp=listbox.Items[indexofsource].ToString();			

				if(indexoftarget>indexofsource)
				{
					for(int index=indexofsource;index<indexoftarget;index++)
					{
						listbox.Items[index]=listbox.Items[index+1];
					}

				}
				else
				{
					for(int index=indexofsource;index>indexoftarget;index--)
					{
						listbox.Items[index]=listbox.Items[index-1];
					}
				}

			
				listbox.Items[indexoftarget]=temp;
				listbox.SelectedIndex=indexoftarget;
				
			}
		}


		private void BtnCancel_Click(object sender, System.EventArgs e)
		{
		    DialogResult=DialogResult.Cancel;
		}

		private void BtnReverse_Click(object sender, System.EventArgs e)
		{
			int count=this.LstValue.Items.Count;

			if(count<0)return;

			string strValue=string.Empty;

			for(int i=0;i<count/2;i++)
			{
                 strValue=(string)this.LstValue.Items[i];
                 this.LstValue.Items[i]=(string)this.LstValue.Items[count-1-i];
                 this.LstValue.Items[count-1-i]=strValue;
			}
		}

		private void ReLoad_Click(object sender, System.EventArgs e)
		{
			this.SetView();
		}

		private void BtnUp_Click(object sender, System.EventArgs e)
		{
			int count=LstValue.Items.Count;
            int index=LstValue.SelectedIndex;
			if(index<=0||count<=1)return;
            string strValue=(string)this.LstValue.SelectedItem;
            this.LstValue.Items[index]=(string)this.LstValue.Items[index-1];
			this.LstValue.Items[index-1]=strValue;
             this.LstValue.SelectedIndex--;		
		}

		private void BtnDown_Click(object sender, System.EventArgs e)
		{
			int count=this.LstValue.Items.Count;
			int index=this.LstValue.SelectedIndex;
			if(index<0||count<=1||index+1>=count)return;
			string strValue=(string)this.LstValue.SelectedItem;
			this.LstValue.Items[index]=(string)this.LstValue.Items[index+1];
			this.LstValue.Items[index+1]=strValue;
			this.LstValue.SelectedIndex++;
		}
	}
	#endregion

	public class CustomOrdersEditor : System.Drawing.Design.UITypeEditor
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

			if (!(value is StringCollection))
				return value;

			IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

			CustomOrdersEditorForm CustomOrders= new CustomOrdersEditorForm(value);
			if (edSvc != null)
			{
				if (edSvc.ShowDialog(CustomOrders) == DialogResult.OK)
				{
					return CustomOrders.strResults;
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
}
