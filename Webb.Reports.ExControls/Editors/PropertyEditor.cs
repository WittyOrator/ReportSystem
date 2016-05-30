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
	#region public class PropertyEditor : System.Drawing.Design.UITypeEditor

	public class PropertyEditor : System.Drawing.Design.UITypeEditor
	{
		public PropertyEditor()
		{

		}

		// Indicates whether the UITypeEditor provides a form-based (modal) dialog, 
		// drop down dialog, or no UI outside of the properties window.
		[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name="FullTrust")] 
		public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}

		// Indicates whether the UITypeEditor supports painting a 
		// representation of a property's value.
		[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name="FullTrust")]
		public override bool GetPaintValueSupported(System.ComponentModel.ITypeDescriptorContext context)
		{
			return false;
		}

		// Displays the UI for value selection.
		[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name="FullTrust")]
		public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, System.IServiceProvider provider, object value)
		{	
			IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
			
			object  EditValue=value;

			if(value is ChartSummaryInformation)EditValue=(value as ChartSummaryInformation).Copy();

            if (value is Views.GroupAdvancedSetting) EditValue = (value as Views.GroupAdvancedSetting).Copy();

            if (value is GroupSummary) EditValue = (value as GroupSummary).Copy();

            if (value is GradingPostion) EditValue = (value as GradingPostion).Copy();

			if( edSvc != null )
			{				
				Webb.Utilities.PropertyForm propertyForm = new Webb.Utilities.PropertyForm();

				propertyForm.BindProperty("Object Editor",EditValue);

				if(DialogResult.OK == edSvc.ShowDialog(propertyForm ))
				{					
					return propertyForm.Object;
				}
			}

			return value;
		}

		// Draws a representation of the property's value.
		[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name="FullTrust")]
		public override void PaintValue(System.Drawing.Design.PaintValueEventArgs e)
		{
			base.PaintValue(e);
		}
	}
	#endregion
}
