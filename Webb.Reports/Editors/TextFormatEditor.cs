	using System;
	using System.ComponentModel;
	using System.Drawing;
	using System.Drawing.Design;
	using System.Windows.Forms;
	using System.Windows.Forms.Design;
	using DevExpress.XtraPrinting;
	using System.Drawing.Drawing2D;
	using System.Globalization;

	namespace Webb.Reports.Editors
	{
		#region class TextFormatEditor
		public class TextFormatEditor : System.Drawing.Design.UITypeEditor
		{
			ListBox box1=new ListBox();

			IWindowsFormsEditorService edSvc ;

			string strResult=string.Empty;

			public TextFormatEditor()
			{				
				this.box1.Click += new System.EventHandler(this.box1_Click);

			}
			[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
			public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
			{
				return UITypeEditorEditStyle.DropDown;
			}

			// Displays the UI for value selection.
			[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
			public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, System.IServiceProvider provider, object value)
			{

				if (!(value is string))
					return value;

                strResult=value as string;

				edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

				if(edSvc!=null)
				{
					box1.Items.Clear();
					box1.BorderStyle=BorderStyle.None;
				
					box1.Items.AddRange(new object[] {  
														 "[Value]->[Count]",
														 "[Count]<-[Value]",
														 "[Value]:[Count]",
														 "[Count]:[Value]",														 
														 "[Count]",
													 }
						                  );
					edSvc.DropDownControl(box1);

					return strResult;
					
				}		
				return value;
			}
			
			[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
			public override bool GetPaintValueSupported(System.ComponentModel.ITypeDescriptorContext context)
			{
				return false;
			}		
			private void box1_Click(object sender, System.EventArgs e)
			{	
				if(box1.SelectedIndex>=0)
				{					
					this.strResult=(string)box1.SelectedItem;
					edSvc.CloseDropDown();
				
				}
			
			}
			
		}
		#endregion	
	}

