using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Configuration;
using System.Timers;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Drawing;
using DevExpress.Data;
using DevExpress.LookAndFeel;
using DevExpress.LookAndFeel.Helpers;
using DevExpress.Utils.Design;
using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.Utils.Win;
using DevExpress.Utils.Controls;
using DevExpress.Utils.Drawing;
using DevExpress.Utils.Drawing.Helpers;
using DevExpress.XtraTab;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Container;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraPrinting;
using DevExpress.Accessibility;
using DevExpress.XtraEditors.Filtering;
using System.Runtime.Serialization;

using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Windows.Forms.Design;
using System.ComponentModel.Design.Serialization;
using System.Windows.Forms.ComponentModel;
using System.Reflection;
using DevExpress.XtraEditors.Design;
//
using DevExpress.XtraReports.Design;
using DevExpress.XtraReports.Localization;
using DevExpress.XtraReports.UI;

using Webb.Reports.ExControls.Views;

using CurrentDesign = DevExpress.Utils.Design;  //08-13-2008@Scott

namespace Webb.Reports.ExControls.Design
{
	#region public class SimpleGroupDesigner
	/*Descrition:   */
	public class SimpleGroupDesigner : ExControlDesigner
	{
		//ctor
		public SimpleGroupDesigner()
		{
			
		}
		//Methods
		public override void Initialize(IComponent component)
		{
			base.Initialize (component);
			this._DesignForm = new UI.DF_SimpleGroupingControl();
			this._DesignForm.SetView(this._Control.MainView);
		}
		//
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

		public override void InitializeVerbs()
		{
			base.InitializeVerbs ();
			this._Verbs	.Add(new DesignerVerb("Update View",new EventHandler(UpdateView)));
		}
		
		private void UpdateView(object sender,EventArgs e)
		{
			this._Control.MainView.UpdateView();
		}

		public override void InitializeActionList()
		{
			this._ActionLists.Add(new SimpleGroupingActionList(this));
		}

		#region internal class SimpleGroupingActionList : ExControlActionList
		/*Descrition:   */
		internal class SimpleGroupingActionList : ExControlActionList
		{
			//Properties
			[EditorAttribute(typeof(Webb.Reports.Editors.DBFilterEditor), typeof(System.Drawing.Design.UITypeEditor))]	//06-17-2008@Scott
			public Webb.Data.DBFilter Filter
			{
				get { return ((SimpleGroupingControl)Component).Filter; }
				set { SetPropertyValue("Filter", value); }
			}
			public bool ShowRowIndicators
			{
				get{return ((SimpleGroupingControl)Component).ShowRowIndicators;}
				set{((SimpleGroupingControl)Component).ShowRowIndicators = value;}
			}
			public bool OneValuePerPage
			{
				get{return ((SimpleGroupingControl)Component).OneValuePerPage;}
				set{((SimpleGroupingControl)Component).OneValuePerPage = value;}
			}
			public CellSizeAutoAdaptingTypes CellSizeAutoAdapting
			{
				get{return ((SimpleGroupingControl)Component).CellSizeAutoAdapting;}
				set{((SimpleGroupingControl)Component).CellSizeAutoAdapting = value;}
			}
			public bool SizeSelfAdapting
			{
				get{return ((SimpleGroupingControl)Component).SizeSelfAdapting;}
				set{((SimpleGroupingControl)Component).SizeSelfAdapting = value;}
			}

			public int TopCount
			{
				get{return ((SimpleGroupingControl)Component).TopCount;}
				set{((SimpleGroupingControl)Component).TopCount = value;}
			}

			public bool Total
			{
				get{return ((SimpleGroupingControl)Component).Total;}
				set{((SimpleGroupingControl)Component).Total = value;}
			}

			public string TotalTitle
			{
				get{return ((SimpleGroupingControl)Component).TotalTitle;}
				set{((SimpleGroupingControl)Component).TotalTitle = value;}
			}

			public string SectionTitle
			{
				get{return ((SimpleGroupingControl)Component).SectionTitle;}
				set{((SimpleGroupingControl)Component).SectionTitle = value;}
			}

			public bool SectionInOneRow
			{
				get{return ((SimpleGroupingControl)Component).SectionInOneRow;}
				set{((SimpleGroupingControl)Component).SectionInOneRow = value;}
			}

			public Webb.Collections.Int32Collection TotalColumns
			{
				get{return ((SimpleGroupingControl)Component).TotalColumns;}
				set{((SimpleGroupingControl)Component).TotalColumns = value;}
			}

			public int HeightPerPage
			{
				get{return ((SimpleGroupingControl)Component).HeightPerPage;}
				set{((SimpleGroupingControl)Component).HeightPerPage = value;}
			}

			public bool PlayAfter
			{
				get{return ((SimpleGroupingControl)Component).PlayAfter;}
				set{((SimpleGroupingControl)Component).PlayAfter = value;}
			}

			public bool AcrossPage
			{
				get{return ((SimpleGroupingControl)Component).AcrossPage;}
				set{((SimpleGroupingControl)Component).AcrossPage = value;}
			}

			//ctor
			public SimpleGroupingActionList(ExControlDesigner designer) : base(designer) 
			{

			}
			//Methods
			protected override void FillActionItemCollection(CurrentDesign.DesignerActionItemCollection actionItems)
			{
				base.FillActionItemCollection (actionItems);
				AddPropertyItem(actionItems, "Filter", "Filter", "Filters");
				AddPropertyItem(actionItems, "PlayAfter", "PlayAfter", "Play After");
				AddPropertyItem(actionItems, "ShowRowIndicators", "ShowRowIndicators", "Show Row Indicators");
				//AddPropertyItem(actionItems, "OneValuePerPage", "OneValuePerPage", "One Value Per Page");
				AddPropertyItem(actionItems, "Total", "Total", "Total");
				AddPropertyItem(actionItems, "TotalTitle", "TotalTitle", "Total Title");
				AddPropertyItem(actionItems, "TotalColumns", "TotalColumns", "Total Columns");
				AddPropertyItem(actionItems, "SectionInOneRow", "SectionInOneRow", "Section In One Row");
				AddPropertyItem(actionItems, "SectionTitle", "SectionTitle", "Section Title");
				AddPropertyItem(actionItems, "CellSizeAutoAdapting", "CellSizeAutoAdapting", "Size Auto-Adapting");
				AddPropertyItem(actionItems, "TopCount", "TopCount", "Top Count");
				AddPropertyItem(actionItems, "AcrossPage", "AcrossPage", "Across Page");
				//AddPropertyItem(actionItems, "HeightPerPage", "HeightPerPage", "Height Per Page");
			}
		}
		#endregion
	}
	#endregion    
}