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
	public class FileNameControlDesigner : ExControlDesigner
	{
		public FileNameControlDesigner(){}

		public override void InitializeActionList()
		{
			this._ActionLists.Add(new FileNameControlActionList(this));
		}

		public override void InitializeVerbs()
		{
			this._Verbs	.Add(new DesignerVerb("Auto Adjust Size",new EventHandler(base.AutoAdjustSize)));

			//base.InitializeVerbs(false);

			this._Verbs.Add(new DesignerVerb("Update View",new EventHandler(UpdateView)));
		}

		private void UpdateView(object sender,EventArgs e)
		{
			this._Control.MainView.UpdateView();
		}

		internal class FileNameControlActionList : Webb.Reports.ExControls.Design.ExControlDesigner.ExControlActionList
		{
			//ctor
			public FileNameControlActionList(ExControlDesigner designer) : base(designer){}

			public Align Align
			{
				get{return ((FileNameControl)Component).Align;}
				set{((FileNameControl)Component).Align = value;}
			}

			public DevExpress.Utils.HorzAlignment HorzAlignment
			{
				get{return ((FileNameControl)Component).HorzAlignment;}
				set{((FileNameControl)Component).HorzAlignment = value;}
			}

			public Font Font
			{
				get{return ((FileNameControl)Component).Font;}
				set{((FileNameControl)Component).Font = value;}
			}

			public Color TextColor
			{
				get{return ((FileNameControl)Component).TextColor;}
				set{((FileNameControl)Component).TextColor = value;}
			}

			public Color BackColor
			{
				get{return ((FileNameControl)Component).BackColor;}
				set{((FileNameControl)Component).BackColor = value;}
			}
			
			protected override void FillActionItemCollection(CurrentDesign.DesignerActionItemCollection actionItems)
			{
				//base.FillActionItemCollection (actionItems);
				base.FillActionItemCollectionWithoutDataSource(actionItems);	//Modified at 2008-12-22 11:35:53@Scott	
				AddPropertyItem(actionItems, "Font", "Font", "Font");
				AddPropertyItem(actionItems, "TextColor", "TextColor", "TextColor");
				AddPropertyItem(actionItems, "BackColor", "BackColor", "BackColor");
				//AddPropertyItem(actionItems, "HorzAlignment", "HorzAlignment", "Align");
			}
		}
	}
}
