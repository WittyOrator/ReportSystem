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
	/// Summary description for ReportInfoLableDesigner.
	/// </summary>
	public class ChartControlExDesigner : ExControlDesigner
	{
		public ChartControlExDesigner(){}

		public override void Initialize(IComponent component)
		{
			base.Initialize (component);
			this._DesignForm = new UI.DF_ChartControlEx();
			this._DesignForm.SetView(this._Control.MainView);
			bool Existed=((ChartControlEx)Component).HaveExisted;
			if(!Existed)
			{
				this.RunDesigner(null,null);
			}
		}

		public override void InitializeActionList()
		{
			this._ActionLists.Add(new ChartControlExActionList(this));
		}

		public override void InitializeVerbs()
		{
			base.InitializeVerbs(true);
			
			this._Verbs.Add(new DesignerVerb("Update View",new EventHandler(UpdateView)));
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

		private void UpdateView(object sender,EventArgs e)
		{
			this._Control.MainView.UpdateView();
		}

		internal class ChartControlExActionList : Webb.Reports.ExControls.Design.ExControlDesigner.ExControlActionList
		{
			//ctor
			public ChartControlExActionList(ExControlDesigner designer) : base(designer){}
			
			[EditorAttribute(typeof(Webb.Reports.Editors.DBFilterEditor), typeof(System.Drawing.Design.UITypeEditor))]	//06-17-2008@Scott
			public Webb.Data.DBFilter Filter
			{
				get { return ((ChartControlEx)Component).Filter; }
				set { ((ChartControlEx)Component).Filter = value;}
			}
			[EditorAttribute(typeof(Webb.Reports.Editors.DBFilterEditor), typeof(System.Drawing.Design.UITypeEditor))]	//06-18-2008@Scott
			public Webb.Data.DBFilter DenominatorFilter
			{
				get { return ((ChartControlEx)Component).DenominatorFilter; }
				set { ((ChartControlEx)Component).DenominatorFilter = value;}
			}

			public bool ShowAxesMode
			{
				get { return ((ChartControlEx)Component).ShowAxesMode; }
				set { ((ChartControlEx)Component).ShowAxesMode = value;}
			}
            public bool AutoFitSize
			{
				get { return ((ChartControlEx)Component).AutoFitSize; }
				set { ((ChartControlEx)Component).AutoFitSize = value;}
			} 
			public bool Relative
			{
				get { return ((ChartControlEx)Component).Relative; }
				set { ((ChartControlEx)Component).Relative = value;}
			}
			public bool CombinedTitle
			{
				get { return ((ChartControlEx)Component).CombinedTitle; }
				set { ((ChartControlEx)Component).CombinedTitle = value;}
			}
			public int TopCount
			{
				get { return ((ChartControlEx)Component).TopCount; }
				set { ((ChartControlEx)Component).TopCount = value;}
			}
			public Color BackgroundColor
			{
				get { return ((ChartControlEx)Component).BackgroundColor; }
				set { ((ChartControlEx)Component).BackgroundColor = value;}
			}
			public int BoundSpace
			{
				get { return ((ChartControlEx)Component).BoundSpace; }
				set { ((ChartControlEx)Component).BoundSpace = value;}
			}
			public Color TransparentBackground
			{
				get{return ((ChartControlEx)Component).TransparentBackground;}
				set
				{
					((ChartControlEx)Component).TransparentBackground=value;						
				}
			}
			protected override void FillActionItemCollection(CurrentDesign.DesignerActionItemCollection actionItems)
			{
				base.FillActionItemCollection (actionItems);

				AddPropertyItem(actionItems, "TransparentBackground", "TransparentBackground", "Chart Background Color");  //Added this code at 2008-12-17 9:33:12@Simon
				AddPropertyItem(actionItems, "BoundSpace", "BoundSpace", "Bound Space");  //Added this code at 2008-12-17 9:33:12@Simon
                AddPropertyItem(actionItems, "BackgroundColor", "BackgroundColor", "Axes-Area Background Color");  //Added this code at 2008-12-17 9:33:12@Simon
				AddPropertyItem(actionItems, "ShowAxesMode", "ShowAxesMode", "ShowAxesMode");  //Added this code at 2008-12-17 9:33:12@Simon
                AddPropertyItem(actionItems, "AutoFitSize", "AutoFitSize", "Group Percent");  //Added this code at 2008-12-17 9:33:12@Simon
				AddPropertyItem(actionItems, "Relative", "Relative", "Delete Extra Space");  //Added this code at 2008-12-17 9:33:12@Simon

				AddPropertyItem(actionItems, "CombinedTitle", "CombinedTitle", "Show 1st SeriesTitle Only");  //Added this code at 2008-12-17 9:33:12@Simon


				
				AddPropertyItem(actionItems, "TopCount", "TopCount", "TopCount");  //Added this code at 2008-12-17 9:33:12@Simon
				AddPropertyItem(actionItems, "Filter", "Filter", "Filters");

				AddPropertyItem(actionItems, "DenominatorFilter", "DenominatorFilter", "Denominator Filters");
			}
		}
	}
}