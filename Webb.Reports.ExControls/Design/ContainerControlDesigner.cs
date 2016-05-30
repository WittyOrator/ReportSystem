using System;
using System.Windows;
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms.Design;
using System.Drawing;

using DevExpress.XtraReports;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Design;
using DevExpress.Utils.Design;
using DevExpress.XtraReports.Localization;
using DevExpress.Utils;

using CurrentDesign = DevExpress.Utils.Design; 

namespace Webb.Reports.ExControls.Design
{
	/// <summary>
	/// Summary description for ContainerControlExDesigner.
	/// </summary>
	public class ContainerControlDesigner: ExControlDesigner
	{
		public ContainerControlDesigner()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public override void InitializeActionList()
		{
			this._ActionLists.Add(new ContainerControlActionList(this));
		}

		public override void InitializeVerbs()
		{
			
		}

		public override void RunDesigner(object sender, EventArgs e)
		{
		
		}

		private void UpdateView(object sender,EventArgs e)
		{
			this._Control.MainView.UpdateView();
		}

		internal class ContainerControlActionList : Webb.Reports.ExControls.Design.ExControlDesigner.ExControlActionList
		{

			//ctor
			public ContainerControlActionList(ExControlDesigner designer) : base(designer){}		
			

			public bool KeepDistance
			{
				get{return ((ContainerControl)Component).KeepDistance;}
				set{((ContainerControl)Component).KeepDistance = value;}
			}
			protected override void FillActionItemCollection(CurrentDesign.DesignerActionItemCollection actionItems)
			{
                AddPropertyItem(actionItems, "KeepDistance", "KeepDistance", "Maintain Inner Distance"); 
			    
			}
		}
	}
}
