using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing.Drawing2D;
using System.IO;
using System.Collections.Specialized;
using System.Collections;

namespace Webb.Reports.Editors     //Added this Editor at 2008-11-26 10:42:07@Simon
{
	
	public class SortingEditorControl : System.Windows.Forms.UserControl
	{

	    string strResult=string.Empty;
		StringCollection strCols=new StringCollection();
		private System.Windows.Forms.ListBox box1;
        IWindowsFormsEditorService edSvc;
		private bool b_MultiSel=false;

        public static ArrayList StandardList = new ArrayList(); 

		private void InitializeComponent()
		{
            this.box1 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // box1
            // 
            this.box1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.box1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.box1.ItemHeight = 12;
            this.box1.Location = new System.Drawing.Point(0, 0);
            this.box1.Name = "box1";
            this.box1.Size = new System.Drawing.Size(165, 312);
            this.box1.TabIndex = 0;
            this.box1.Click += new System.EventHandler(this.listBox1_Click);
            // 
            // SortingEditorControl
            // 
            this.Controls.Add(this.box1);
            this.Name = "SortingEditorControl";
            this.Size = new System.Drawing.Size(165, 312);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.SortingEditorControl_Paint);
            this.ResumeLayout(false);

		}
		public SortingEditorControl(IWindowsFormsEditorService eSvc,object value)
		{
			if(value is string)
			{
				b_MultiSel=false;				
			}
			else
			{
				b_MultiSel=true;
			}

			InitializeComponent();

			InitList(value);

			edSvc=eSvc;
			
		}
		public object Result
		{
			get
			{
				if(b_MultiSel)
				{
					return strCols;
				}
				else
				{
					return strResult;
				}
			}
		}
		private void listBox1_Click(object sender, System.EventArgs e)
		{	
			if(box1.SelectedIndices.Count>0)
			{	
				if(b_MultiSel)
				{
					box1.BeginUpdate();

					this.strCols.Clear();
					if (box1.SelectedIndices.Contains(0))
					{	
						for(int i=0;i<box1.Items.Count;i++)
						{
							box1.SetSelected(i,false);
						}						
					}
					else
					{
						foreach(int index in box1.SelectedIndices)
						{
							this.strCols.Add(box1.Items[index].ToString());
						}
					}
					
					box1.EndUpdate();
				}
				else
				{
					this.strResult=(string)box1.SelectedItem;

					edSvc.CloseDropDown();					
					
				}
				
			}
			
		}		
		private void InitList(object value)
		{			
            if (StandardList.Count > 0)
            {
                foreach (object strValue in StandardList)
                {
                    if (strValue.ToString() != string.Empty) box1.Items.Add(strValue.ToString());
                }
            }
            else
            {
                foreach (object strValue in Webb.Data.PublicDBFieldConverter.AvialableFields)
                {
                    if (strValue.ToString() != string.Empty) box1.Items.Add(strValue.ToString());
                }
            }

			if(b_MultiSel)
			{
				strCols=value as StringCollection;

				box1.SelectionMode = SelectionMode.MultiSimple;
				
			}
			else
			{
				box1.SelectionMode=SelectionMode.One;	
			
				this.strResult=value as string;
			}			
			
		}

		private void SortingEditorControl_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
           this.SetChecked();
		  
		}
		private void SetChecked()
		{ 
			if(b_MultiSel)
			{
				for(int i=0;i<box1.Items.Count;i++)
				{ 
					string strField=box1.Items[i].ToString();
					if(strCols.Contains(strField))
					{
						box1.SetSelected(i,true);
					}
					else
					{
						box1.SetSelected(i,false);
					}
				}
				
			}
			else
			{
				int index=box1.FindStringExact(strResult);

				box1.SelectedIndex=index;
			}			
		}
		
	}

	public class SortingColumnEditor:System.Drawing.Design.UITypeEditor
	{			
		
		[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name="FullTrust")] 
		public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
	    {
		   return UITypeEditorEditStyle.DropDown;
	    }

		// Displays the UI for value selection.
		[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name="FullTrust")]
		public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, System.IServiceProvider provider, object value)
		{
			IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
			
			if(edSvc!=null)
			{
				SortingEditorControl edtorControl=new SortingEditorControl(edSvc,value);

				edSvc.DropDownControl(edtorControl);	
			
				return edtorControl.Result;
				
			}		
			return value;
		}

	
		[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name="FullTrust")]   
		public override void PaintValue(System.Drawing.Design.PaintValueEventArgs e)
		{			
		}        
		
		[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name="FullTrust")]
		public override bool GetPaintValueSupported(System.ComponentModel.ITypeDescriptorContext context)
		{
			return false;
		}	
	}
	
}
