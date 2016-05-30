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
	/// Summary description for HolePanelDesigner.
	/// </summary>
	public class FieldPanelDesigner : ExControlDesigner
	{
		public FieldPanelDesigner(){}

		public override void Initialize(IComponent component)
		{
			base.Initialize (component);
			this._DesignForm = new UI.DF_FieldPanel();
			this._DesignForm.SetView(this._Control.MainView);
		}

		public override void InitializeActionList()
		{
			//base.InitializeActionList();
			this._ActionLists.Add(new FieldPanelActionList(this));
		}

		public override void InitializeVerbs()
		{
			base.InitializeVerbs();
			this._Verbs	.Add(new DesignerVerb("Update View",new EventHandler(UpdateView)));
		}

		private void UpdateView(object sender,EventArgs e)
		{
			this._Control.MainView.UpdateView();
		}

		public override void RunDesigner(object sender, EventArgs e)
		{
			this._DesignForm.SetView(this._Control.MainView);
			
			DialogResult m_Result = this._DesignForm.ShowDialog();
			
			if(m_Result==DialogResult.OK)
			{
				this._DesignForm.UpdateView(this._Control.MainView);
				
				this._Control.CalculateResult();
			}
		}

		internal class FieldPanelActionList : Webb.Reports.ExControls.Design.ExControlDesigner.ExControlActionList
		{
			//ctor
			public FieldPanelActionList(ExControlDesigner designer) : base(designer){}

			[EditorAttribute(typeof(Webb.Reports.Editors.DBFilterEditor), typeof(System.Drawing.Design.UITypeEditor))]	//06-17-2008@Scott
			public Webb.Data.DBFilter Filter 
			{
				get { return ((FieldPanel)Component).Filter; }
				set { ((FieldPanel)Component).Filter = value;}
			}

			public bool GridLine
			{
				get { return ((FieldPanel)Component).GridLine; }
				set { ((FieldPanel)Component).GridLine = value;}
			}
			public Views.CellSizeAutoAdaptingTypes CellSizeAutoAdapting
			{
				get { return ((FieldPanel)Component).CellSizeAutoAdapting; }
				set { ((FieldPanel)Component).CellSizeAutoAdapting = value;}
			}
			public bool RemoveHeaderRows
			{
				get { return ((FieldPanel)Component).RemoveHeaderRows; }
				set { ((FieldPanel)Component).RemoveHeaderRows = value;}
			}

			protected override void FillActionItemCollection(CurrentDesign.DesignerActionItemCollection actionItems)
			{
				base.FillActionItemCollection (actionItems);
				AddPropertyItem(actionItems, "GridLine", "GridLine", "GridLine");
				AddPropertyItem(actionItems, "Filter", "Filter", "Filters");
				AddPropertyItem(actionItems, "CellSizeAutoAdapting", "CellSizeAutoAdapting", "CellSize Auto Adapting");
				AddPropertyItem(actionItems, "RemoveHeaderRows", "RemoveHeaderRows", "No Headers");
			}
		}
	}
}
