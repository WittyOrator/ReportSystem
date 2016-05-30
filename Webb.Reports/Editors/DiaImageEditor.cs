using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using DevExpress.XtraPrinting;
using System.Drawing.Drawing2D;
using System.IO;

namespace Webb.Reports.Editors
{
	#region class DiaImageEditor
	public class  DiaImageEditor: System.Drawing.Design.UITypeEditor
	{
		[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name="FullTrust")] 
		public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}

		// Displays the UI for value selection.
		[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name="FullTrust")]
		public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, System.IServiceProvider provider, object value)
		{
			if(!(value is string))
				return value;
			string resultFile = "";
			OpenFileDialog openFileDialog1 = new OpenFileDialog();
			openFileDialog1.InitialDirectory = Webb.Reports.DataProvider.VideoPlayBackManager.PublicDBProvider.DBSourceConfig.UserFolder;
			openFileDialog1.Filter = "All files (*.*)|*.*|Diagram files (*.dia)|*.dia";
			openFileDialog1.FilterIndex = 2;
			openFileDialog1.RestoreDirectory = true;			
			if (openFileDialog1.ShowDialog()== DialogResult.OK)resultFile = openFileDialog1.FileName;
			if(resultFile.ToLower().EndsWith(".dia"))
			{					
				return resultFile;
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
	#endregion
}
