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

using Webb.Reports.ExControls.Views;

namespace Webb.Reports.ExControls.Design
{
	/// <summary>
	/// Summary description for DateTimeControlDesigner.
	/// </summary>
	public class LabelControlDesigner : ExControlDesigner
	{
		public LabelControlDesigner(){}

		public override void InitializeActionList()
		{
			this._ActionLists.Add(new LabelControlActionList(this));
		}

		public override void InitializeVerbs()
		{
			//this._Verbs.Add(new DesignerVerb("Edit",new EventHandler(RunDesigner)));

			//this._Verbs.Add(new DesignerVerb("Auto Adjust Size",new EventHandler(base.AutoAdjustSize)));

			this._Verbs	.Add(new DesignerVerb("3D View",new EventHandler(Switch3DFeel)));	//Modified at 2008-12-22 9:12:16@Scott
			//base.InitializeVerbs(false);

			//this._Verbs.Add(new DesignerVerb("Update View",new EventHandler(UpdateView)));
		}

		public override void RunDesigner(object sender, EventArgs e)
		{
			//base.RunDesigner (sender, e);
		}


		private void UpdateView(object sender,EventArgs e)
		{
			this._Control.MainView.UpdateView();
		}

		internal class LabelControlActionList : Webb.Reports.ExControls.Design.ExControlDesigner.ExControlActionList
		{
			//ctor
			public LabelControlActionList(ExControlDesigner designer) : base(designer){}

			public string Text
			{
				get{return ((LabelControl)Component).Text;}
				set{((LabelControl)Component).Text = value;}
			}

            //09-01-2011 Scott
            public bool IsTitle
            {
                get { return ((LabelControl)Component).IsTitle; }
                set { ((LabelControl)Component).IsTitle = value; }
            }

			public Align Align
			{
				get{return ((LabelControl)Component).Align;}
				set{((LabelControl)Component).Align = value;}
			}

			public DevExpress.Utils.HorzAlignment HorzAlignment
			{
				get{return ((LabelControl)Component).HorzAlignment;}
				set{((LabelControl)Component).HorzAlignment = value;}
			}

			public DevExpress.Utils.VertAlignment VertAlignment
			{
				get{return ((LabelControl)Component).VertAlignment;}
				set{((LabelControl)Component).VertAlignment = value;}
			}

			public Font Font
			{
				get{return ((LabelControl)Component).Font;}
				set{((LabelControl)Component).Font = value;}
			}

			public Color TextColor
			{
				get{return ((LabelControl)Component).TextColor;}
				set{((LabelControl)Component).TextColor = value;}
			}

            public SortingColumnCollection SortingColumns
            {
                get { return ((LabelControl)Component).SortingColumns; }
                set { ((LabelControl)Component).SortingColumns = value; }
            }

			public Color BackColor
			{
				get{return ((LabelControl)Component).BackColor;}
				set{((LabelControl)Component).BackColor = value;}
			}

			public bool OneValuePerPage
			{
				get{return ((LabelControl)Component).OneValuePerPage;}
				set{((LabelControl)Component).OneValuePerPage = value;}
			}

		
            [Editor(typeof(Webb.Reports.Editors.FieldSelectEditor), typeof(System.Drawing.Design.UITypeEditor))]
			public string Field
			{
				get{return ((LabelControl)Component).Field;}
				set{((LabelControl)Component).Field = value;}
			}
			
			protected override void FillActionItemCollection(CurrentDesign.DesignerActionItemCollection actionItems)
			{
				//base.FillActionItemCollection (actionItems);
				base.FillActionItemCollectionWithoutDataSource(actionItems);	//Modified at 2008-12-22 11:35:53@Scott	
				AddPropertyItem(actionItems, "Text", "Text", "Text");
				AddPropertyItem(actionItems, "Font", "Font", "Font");
				AddPropertyItem(actionItems, "TextColor", "TextColor", "TextColor");
				AddPropertyItem(actionItems, "BackColor", "BackColor", "BackColor");
				AddPropertyItem(actionItems, "HorzAlignment", "HorzAlignment", "HorzAlignment");
				AddPropertyItem(actionItems, "VertAlignment", "VertAlignment", "VertAlignment");
				AddPropertyItem(actionItems, "OneValuePerPage", "OneValuePerPage", "Display filter's name per page");
                AddPropertyItem(actionItems, "IsTitle", "IsTitle", "Is Title");   //09-01-2011 Scott
                AddPropertyItem(actionItems, "SortingColumns", "SortingColumns", "Sorting");
				AddPropertyItem(actionItems, "Field", "Field", "Field");
			}
		}
	}
}

