using System;
using System.Windows.Forms;
using System.Drawing;

namespace Webb.Reports.ReportWizard.WizardInfo
{
	/// <summary>
	/// Summary description for WebbPageDataGrid.
	/// </summary>
	public class WebbPageDataGrid:DataGrid
	{
		public WebbPageDataGrid():base()
		{		
			
		}

		public int ScrollPosion
		{
			get
			{
				return this.HorizScrollBar.Value;
			}
		}		
	}
 
}
