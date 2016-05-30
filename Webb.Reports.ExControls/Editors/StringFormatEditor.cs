using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Collections;
using System.Data;

using Webb.Data;
using Webb.Reports.ExControls.Data;

namespace Webb.Reports.ExControls.Editors
{
	/// <summary>
	/// Summary description for PropertyEditor.
	/// </summary>	
	#region public class StringFormatEditor : System.Drawing.Design.UITypeEditor

	public class StringFormatEditor : System.Drawing.Design.UITypeEditor
	{
        IWindowsFormsEditorService edSvc;

        public StringFormatEditor()
		{

		}

		// Indicates whether the UITypeEditor provides a form-based (modal) dialog, 
		// drop down dialog, or no UI outside of the properties window.
		[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name="FullTrust")] 
		public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.DropDown;
		}

		// Indicates whether the UITypeEditor supports painting a 
		// representation of a property's value.
		[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name="FullTrust")]
		public override bool GetPaintValueSupported(System.ComponentModel.ITypeDescriptorContext context)
		{
			return false;
		}


        private void SetSelectFlag(StringFormatFlags flag,ListBox box)
        {
            if (flag == StringFormatFlags.DirectionRightToLeft)
            {
                box.SelectedIndex = 1;
            }
            else if (flag == StringFormatFlags.DirectionVertical)
            {
                box.SelectedIndex = 2;
            }
            else if (flag == StringFormatFlags.NoWrap)
            {
                box.SelectedIndex = 3;
            }
            else
            {
               box.SelectedIndex = 0 ;
            }
        }
        private StringFormatFlags GetStringFormat(ListBox box)
        {
            switch (box.SelectedIndex)
            {
                case 1:
                    return StringFormatFlags.DirectionRightToLeft;
                case 2:
                    return StringFormatFlags.DirectionVertical;
                case 3:
                     return StringFormatFlags.NoWrap;
                default:
                    return (StringFormatFlags)0;
            }
        }

		// Displays the UI for value selection.
		[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name="FullTrust")]
		public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, System.IServiceProvider provider, object value)
		{			
			edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

			if( edSvc != null )
            {
                StringFormatFlags formatflag=(StringFormatFlags)value;

                ListBox box = new ListBox();

                box.BorderStyle = BorderStyle.None;

                box.Click += new EventHandler(box_Click);
 
                box.Items.AddRange(new string[]{"Default","Right To Left","Vertical","NoWrap"});

                SetSelectFlag(formatflag, box);

                edSvc.DropDownControl(box);

                if(box.SelectedIndex>=0)
                {
                    return GetStringFormat(box);

                }
			}

			return value;
		}

        void box_Click(object sender, EventArgs e)
        {
            if (edSvc != null)
            {
                edSvc.CloseDropDown();
            }
        }

		// Draws a representation of the property's value.
		[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name="FullTrust")]
		public override void PaintValue(System.Drawing.Design.PaintValueEventArgs e)
		{
			base.PaintValue(e);
		}
	}
	#endregion


    #region public class SeparatorEditor : System.Drawing.Design.UITypeEditor

    public class SeparatorEditor: System.Drawing.Design.UITypeEditor
    {
        IWindowsFormsEditorService edSvc;

        public SeparatorEditor()
        {

        }

        // Indicates whether the UITypeEditor provides a form-based (modal) dialog, 
        // drop down dialog, or no UI outside of the properties window.
        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        // Indicates whether the UITypeEditor supports painting a 
        // representation of a property's value.
        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        public override bool GetPaintValueSupported(System.ComponentModel.ITypeDescriptorContext context)
        {
            return false;
        }  

        // Displays the UI for value selection.
        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {
            edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

            if (edSvc != null)
            {          

                ListBox box = new ListBox();

                box.BorderStyle = BorderStyle.None;

                box.Click += new EventHandler(box_Click);

                box.Items.AddRange(new string[] { "", "[Space]", "[2 Space]", "[4 Space]","[Tab]","[LineBreak]" });

                box.SelectedIndex = box.FindStringExact(value.ToString());
               
                edSvc.DropDownControl(box);

                if (box.SelectedIndex >= 0)
                {
                    return box.SelectedItem.ToString();                   
                }
            }

            return value;
        }

        void box_Click(object sender, EventArgs e)
        {
            if (edSvc != null)
            {
                edSvc.CloseDropDown();
            }
        }

        // Draws a representation of the property's value.
        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        public override void PaintValue(System.Drawing.Design.PaintValueEventArgs e)
        {
            base.PaintValue(e);
        }
    }
    #endregion
}

