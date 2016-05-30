//12-20-2007@Scott
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

using Webb.Reports.ExControls.Data;
using Webb.Reports.ExControls.Views;

using CurrentDesign = DevExpress.Utils.Design;  //08-13-2008@Scott

namespace Webb.Reports.ExControls.Design
{
	/// <summary>
	/// Summary description for ReportInfoLableDesigner.
	/// </summary>
	public class CustomImageDesigner : ExControlDesigner
	{
		private UI.PaintForm _PaintForm = new Webb.Reports.ExControls.UI.PaintForm();

		public CustomImageDesigner()
		{
		}

		public override void Initialize(IComponent component)
		{
			base.Initialize (component);
		}

		public override void InitializeActionList()
		{
			this._ActionLists.Add(new CustomImageActionList(this));
		}

		public override void InitializeVerbs()
		{
			base.InitializeVerbs();
			this._Verbs.Add(new DesignerVerb("Update View",new EventHandler(UpdateView)));
			//this._Verbs.Add(new DesignerVerb("Custom...",new EventHandler(Custom)));
			this._Verbs.RemoveAt(0);	//remove run designer
		}

		private void Custom(object sender,EventArgs e)
		{
			if(this._PaintForm.ShowDialog(this.Control) == DialogResult.OK)
			{
				(this._Control.MainView as Views.CustomImageView).Image = this._PaintForm.CustomImage;
				
				this._Control.MainView.UpdateView();
			}
		}

		private void UpdateView(object sender,EventArgs e)
		{
			this._Control.MainView.UpdateView();
		}

		internal class CustomImageActionList : Webb.Reports.ExControls.Design.ExControlDesigner.ExControlActionList
		{
			//ctor
			public CustomImageActionList(ExControlDesigner designer) : base(designer){}

			public PictureBoxSizeMode SizeMode
			{
				get{return ((CustomImage)Component).SizeMode;}
				set{((CustomImage)Component).SizeMode = value;}
			}
		
			public Image Image
			{
				get{return ((CustomImage)Component).Image;}
				set{((CustomImage)Component).Image = value;}
			}
			public bool OneValue
			{
				get{return ((CustomImage)Component).OneValue;}
				set{((CustomImage)Component).OneValue = value;}
			}
            public bool UsePicDir
            {
                get { return ((CustomImage)Component).UsePicDir; }
                set { ((CustomImage)Component).UsePicDir = value; }
            }
             public InvertPictureMode InvertPicture
             {
                  get { return ((CustomImage)Component).InvertPicture; }
                 set { ((CustomImage)Component).InvertPicture = value; }
             }
             public PlayBookScoutType PlayBookScoutType
             {
                 get { return ((CustomImage)Component).PlayBookScoutType; }
                 set { ((CustomImage)Component).PlayBookScoutType = value; }
             }
             public bool ReadDiagramFromPlaybook
             {
                 get { return ((CustomImage)Component).ReadDiagramFromPlaybook; }
                 set { ((CustomImage)Component).ReadDiagramFromPlaybook = value; }
             }
             public SortingColumnCollection SortingColumns
            {
                 get { return ((CustomImage)Component).SortingColumns; }
                 set { ((CustomImage)Component).SortingColumns = value; }
             }
			
			[Editor(typeof(Webb.Reports.Editors.DiaImageEditor), typeof(System.Drawing.Design.UITypeEditor))]
			public string DiagramImage         //Added this code at 2008-11-3 10:47:00@Simon
			{
				get{return ((CustomImage)Component).DiagramImage;}
				set{((CustomImage)Component).DiagramImage = value;}
			}

            [Editor(typeof(Webb.Reports.Editors.FieldSelectEditor), typeof(System.Drawing.Design.UITypeEditor))]
            public string Field
			{
				get{return ((CustomImage)Component).Field;}
				set{((CustomImage)Component).Field = value;}
			}

			protected override void FillActionItemCollection(CurrentDesign.DesignerActionItemCollection actionItems)
			{
				//base.FillActionItemCollection (actionItems);
				base.FillActionItemCollectionWithoutDataSource(actionItems);	//Modified at 2008-12-22 11:35:53@Scott	
				AddPropertyItem(actionItems, "Image", "Image", "Image");
				AddPropertyItem(actionItems, "SizeMode", "SizeMode", "SizeMode");
				AddPropertyItem(actionItems, "DiagramImage", "DiagramImage", "Diagram Image");
				AddPropertyItem(actionItems, "OneValue", "OneValue", "Get picture from OneValue/Repeat filter");
                AddPropertyItem(actionItems, "UsePicDir", "UsePicDir", "Use Picture Directory");
                AddPropertyItem(actionItems, "ReadDiagramFromPlaybook", "ReadDiagramFromPlaybook", "Read Diagram From Playbook");
                AddPropertyItem(actionItems, "SortingColumns", "SortingColumns", "Sorting");
                AddPropertyItem(actionItems, "PlayBookScoutType", "PlayBookScoutType", "ScoutType From Playbook");
                AddPropertyItem(actionItems, "InvertPicture", "InvertPicture", "Invert Picture");
				AddPropertyItem(actionItems, "Field", "Field", "Field");	//Modified at 2009-1-4 10:53:25@Scott
			}
		}
	}
}
