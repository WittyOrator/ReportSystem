 /***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:MaskedTextControlDesigner.cs
 * Author:Automatic Macro@simon
 * Create Time:2009-12-2 11:09:37
 * Copyright:1986-2009@Webb Electronics all right reserved.
 * Purpose:
 * ***********************************************************************/
using System;
using System.Windows;
using System.Windows.Forms;
using System.ComponentModel.Design;
using System.Drawing;
using System.ComponentModel;
using DevExpress.XtraReports;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Design;
using DevExpress.Utils.Design;
using DevExpress.Utils;
using DevExpress.XtraReports.Localization;
using CurrentDesign = DevExpress.Utils.Design;
using Webb.Reports.ExControls.Views;

namespace Webb.Reports.ExControls.Design
{
	public class MaskedTextControlDesigner : ExControlDesigner
	{
		public MaskedTextControlDesigner()
		{
		}

		public override void Initialize(IComponent component)
		{
			base.Initialize (component);
			this._DesignForm = new UI.DF_MaskedText();
			this._DesignForm.SetView(this._Control.MainView);		
		}

		public override  void InitializeActionList()
		{			
			this._ActionLists.Add(new MaskedTextControlActionList(this));		
		}

		public override void InitializeVerbs()
		{
			this._Verbs	.Add(new DesignerVerb("Run Designer",new EventHandler(RunDesigner)));
			this._Verbs	.Add(new DesignerVerb("Export Control Data",new EventHandler(ExportView)));
			this._Verbs	.Add(new DesignerVerb("Import Control Data",new EventHandler(ImportView)));
			this._Verbs	.Add(new DesignerVerb("Auto Adjust Size",new EventHandler(AutoAdjustSize)));
			this._Verbs	.Add(new DesignerVerb("Update View",new EventHandler(UpdateView)));	
		}

		private void UpdateView(object sender, EventArgs e)
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

		internal class MaskedTextControlActionList : Webb.Reports.ExControls.Design.ExControlDesigner.ExControlActionList
		{
			public MaskedTextControlActionList(Webb.Reports.ExControls.Design.ExControlDesigner designer) : base(designer)
			{
			}

			public Webb.Reports.ExControls.Data.MaskInfoCollection MaskInfoSetting
			{
				get{ return ((MaskedTextControl)Component).MaskInfoSetting; }
				set{ ((MaskedTextControl)Component).MaskInfoSetting=value; }
			}
			public bool AutoAdjustTitleSize
			{
				get{ return ((MaskedTextControl)Component).AutoAdjustTitleSize; }
				set{ ((MaskedTextControl)Component).AutoAdjustTitleSize=value; }
			}
			[EditorAttribute(typeof(Webb.Reports.Editors.DBFilterEditor), typeof(System.Drawing.Design.UITypeEditor))]	//06-17-2008@Scott
			public Webb.Data.DBFilter Filter
			{
				get{ return ((MaskedTextControl)Component).Filter; }
				set{ ((MaskedTextControl)Component).Filter=value; }
			}

			public int IndexRow
			{
				get{ return ((MaskedTextControl)Component).IndexRow; }
				set{ ((MaskedTextControl)Component).IndexRow=value; }
			}
			public SortingColumnCollection SortingColumns
			{			
				get{ return ((MaskedTextControl)Component).SortingColumns; }
				set{ ((MaskedTextControl)Component).SortingColumns=value; }
			}

			protected override void FillActionItemCollection(CurrentDesign.DesignerActionItemCollection  actionItems)
			{
				AddPropertyItem(actionItems, "Repeat", "Repeat","Repeat");
				AddPropertyItem(actionItems, "AutoAdjustTitleSize", "AutoAdjustTitleSize","Auto Adjust CellSize");
				AddPropertyItem(actionItems, "Filter", "Filter","Filter");
				AddPropertyItem(actionItems, "SortingColumns", "SortingColumns","Sorting Option");
				AddPropertyItem(actionItems, "IndexRow", "IndexRow","Record RowIndex");
			}
		}
	}
}
