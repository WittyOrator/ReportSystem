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
using DevExpress.XtraGrid.Design;

namespace Webb.Reports.ExControls.Design
{
	/// <summary>
	/// Summary description for HolePanelDesigner.
	/// </summary>
	public class HolePanelDesigner : ExControlDesigner
	{
		public HolePanelDesigner(){}

		public override void Initialize(IComponent component)
		{
			base.Initialize (component);
			this._DesignForm = new UI.DF_HolePanel();
			this._DesignForm.SetView(this._Control.MainView);
		}

		public override void InitializeActionList()
		{
			//base.InitializeActionList();
			this._ActionLists.Add(new HolePanelActionList(this));
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
			//base.RunDesigner (sender, e);
			this._DesignForm.SetView(this._Control.MainView);
			DialogResult m_Result = this._DesignForm.ShowDialog();
			if(m_Result==DialogResult.OK)
			{
				this._DesignForm.UpdateView(this._Control.MainView);
				this._Control.CalculateResult();
			}
		}


		internal class HolePanelActionList : Webb.Reports.ExControls.Design.ExControlDesigner.ExControlActionList
		{
			//ctor
			public HolePanelActionList(ExControlDesigner designer) : base(designer){}

			public Webb.Data.DBFilter Filter 
			{
				get { return ((HolePanel)Component).Filter; }
			}

			protected override void FillActionItemCollection(DesignerActionItemCollection actionItems)
			{
				base.FillActionItemCollection (actionItems);
				AddPropertyItem(actionItems, "Filter", "Filter", "Filters");
			}
		}
	}
}
