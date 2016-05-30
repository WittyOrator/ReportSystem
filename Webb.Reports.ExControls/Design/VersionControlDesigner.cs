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
//using DevExpress.XtraGrid.Design;

using CurrentDesign = DevExpress.Utils.Design;  //08-13-2008@Scott

namespace Webb.Reports.ExControls.Design
{
	/// <summary>
	/// Summary description for DateTimeControlDesigner.
	/// </summary>
	public class VersionControlDesigner : ExControlDesigner
	{
		public VersionControlDesigner(){}

		public override void InitializeActionList()
		{
			this._ActionLists.Add(new VersionControlActionList(this));
		}

		public override void InitializeVerbs()
		{
			this._Verbs	.Add(new DesignerVerb("Auto Adjust Size",new EventHandler(base.AutoAdjustSize)));

			//base.InitializeVerbs(false);

			//this._Verbs.Add(new DesignerVerb("Update View",new EventHandler(UpdateView)));
		}

		private void UpdateView(object sender,EventArgs e)
		{
			this._Control.MainView.UpdateView();
		}

		internal class VersionControlActionList : Webb.Reports.ExControls.Design.ExControlDesigner.ExControlActionList
		{
			//ctor
			public VersionControlActionList(ExControlDesigner designer) : base(designer){}

			public Align Align
			{
				get{return ((VersionControl)Component).Align;}
				set{((VersionControl)Component).Align = value;}
			}

			public DevExpress.Utils.HorzAlignment HorzAlignment
			{
				get{return ((VersionControl)Component).HorzAlignment;}
				set{((VersionControl)Component).HorzAlignment = value;}
			}

			public Font Font
			{
				get{return ((VersionControl)Component).Font;}
				set{((VersionControl)Component).Font = value;}
			}

			public Color TextColor
			{
				get{return ((VersionControl)Component).TextColor;}
				set{((VersionControl)Component).TextColor = value;}
			}

			public Color BackColor
			{
				get{return ((VersionControl)Component).BackColor;}
				set{((VersionControl)Component).BackColor = value;}
			}
			public string FormattedText                  //Added this code at 2008-11-20 9:58:51@Simon
			{
				get{return ((VersionControl)Component).FormattedText;}
				set{((VersionControl)Component).FormattedText = value;}
			}
			
			protected override void FillActionItemCollection(CurrentDesign.DesignerActionItemCollection actionItems)
			{
				//base.FillActionItemCollection (actionItems);
				base.FillActionItemCollectionWithoutDataSource(actionItems);	//Modified at 2008-12-22 11:35:53@Scott	
				AddPropertyItem(actionItems, "Font", "Font", "Font");
				AddPropertyItem(actionItems, "TextColor", "TextColor", "TextColor");
				AddPropertyItem(actionItems, "BackColor", "BackColor", "BackColor");
				//AddPropertyItem(actionItems, "HorzAlignment", "HorzAlignment", "Align");
				AddPropertyItem(actionItems, "FormattedText", "FormattedText", "Text Format");
			}
		}
	}
}
