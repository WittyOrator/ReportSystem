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
using Webb.Reports.Editors;

//using DevExpress.XtraGrid.Design;

using CurrentDesign = DevExpress.Utils.Design;  //08-13-2008@Scott

namespace Webb.Reports.ExControls.Design
{
	/// <summary>
	/// Summary description for DateTimeControlDesigner.
	/// </summary>
	public class DateTimeControlDesigner : ExControlDesigner
	{
		public DateTimeControlDesigner(){}

		public override void InitializeActionList()
		{
			this._ActionLists.Add(new DateTimeControlActionList(this));
		}

		public override void InitializeVerbs()
		{
			this._Verbs	.Add(new DesignerVerb("Auto Adjust Size",new EventHandler(base.AutoAdjustSize)));

			
		}

		private void UpdateView(object sender,EventArgs e)
		{
			this._Control.MainView.UpdateView();
		}

		internal class DateTimeControlActionList : Webb.Reports.ExControls.Design.ExControlDesigner.ExControlActionList
		{
			//ctor
			public DateTimeControlActionList(ExControlDesigner designer) : base(designer){}

			public Align Align
			{
				get{return ((DateTimeControl)Component).Align;}
				set{((DateTimeControl)Component).Align = value;}
			}

			public DevExpress.Utils.HorzAlignment HorzAlignment
			{
				get{return ((ReportInfoLabel)Component).HorzAlignment;}
				set{((ReportInfoLabel)Component).HorzAlignment = value;}
			}

			public Font Font
			{
				get{return ((DateTimeControl)Component).Font;}
				set{((DateTimeControl)Component).Font = value;}
			}

			public Color TextColor
			{
				get{return ((DateTimeControl)Component).TextColor;}
				set{((DateTimeControl)Component).TextColor = value;}
			}

			public Color BackColor
			{
				get{return ((DateTimeControl)Component).BackColor;}
				set{((DateTimeControl)Component).BackColor = value;}
			}
            [Editor(typeof(Webb.Reports.Editors.FormatDateTimeEditor), typeof(System.Drawing.Design.UITypeEditor))]
            public string DataFormat
            {
                get { return ((DateTimeControl)Component).DataFormat; }
                set { ((DateTimeControl)Component).DataFormat = value; }
            }
			
			protected override void FillActionItemCollection(CurrentDesign.DesignerActionItemCollection actionItems)
			{
				//base.FillActionItemCollection (actionItems);
				base.FillActionItemCollectionWithoutDataSource(actionItems);	//Modified at 2008-12-22 11:35:53@Scott	
				AddPropertyItem(actionItems, "Font", "Font", "Font");
				AddPropertyItem(actionItems, "TextColor", "TextColor", "TextColor");
				AddPropertyItem(actionItems, "BackColor", "BackColor", "BackColor");
                AddPropertyItem(actionItems, "DataFormat", "DataFormat", "Datetime Format Style");
				//AddPropertyItem(actionItems, "HorzAlignment", "HorzAlignment", "Align");
			}
		}
	}
}