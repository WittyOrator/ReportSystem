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
	public class ReportInfoLabelDesigner : ExControlDesigner
	{
		public ReportInfoLabelDesigner(){}

		public override void InitializeActionList()
		{
			this._ActionLists.Add(new ReportInfoLabelActionList(this));
		}

		public override void InitializeVerbs()
		{
			base.InitializeVerbs(false);

			this._Verbs	.Add(new DesignerVerb("Update View",new EventHandler(UpdateView)));
		}

		private void UpdateView(object sender,EventArgs e)
		{
			this._Control.MainView.UpdateView();
		}

		internal class ReportInfoLabelActionList : Webb.Reports.ExControls.Design.ExControlDesigner.ExControlActionList
		{
			//ctor
			public ReportInfoLabelActionList(ExControlDesigner designer) : base(designer){}

			public LabelTypes LabelType
			{
				get{return ((ReportInfoLabel)Component).LabelType;}
				set{((ReportInfoLabel)Component).LabelType = value;}
			}

			public Align Align
			{
				get{return ((ReportInfoLabel)Component).Align;}
				set{((ReportInfoLabel)Component).Align = value;}
			}

			public bool Multiline
			{
				get{return ((ReportInfoLabel)Component).Multiline;}
				set{((ReportInfoLabel)Component).Multiline = value;}
			}

			public bool WordWrap
			{
				get{return ((ReportInfoLabel)Component).WordWrap;}
				set{((ReportInfoLabel)Component).WordWrap = value;}
			}

			public bool Title
			{
				get{return ((ReportInfoLabel)Component).Title;}
				set{((ReportInfoLabel)Component).Title = value;}
			}

			public bool Object
			{
				get{return ((ReportInfoLabel)Component).Object;}
				set{((ReportInfoLabel)Component).Object = value;}
			}

			public bool FirstObjectOnly
			{
				get{return ((ReportInfoLabel)Component).FirstObjectOnly;}
				set{((ReportInfoLabel)Component).FirstObjectOnly = value;}
			}

			public bool Opponent
			{
				get{return ((ReportInfoLabel)Component).Opponent;}
				set{((ReportInfoLabel)Component).Opponent = value;}
			}

			public bool Date
			{
				get{return ((ReportInfoLabel)Component).Date;}
				set{((ReportInfoLabel)Component).Date = value;}
			}

			public bool Location
			{
				get{return ((ReportInfoLabel)Component).Location;}
				set{((ReportInfoLabel)Component).Location = value;}
			}

			public bool ScoutType
			{
				get{return ((ReportInfoLabel)Component).ScoutType;}
				set{((ReportInfoLabel)Component).ScoutType = value;}
			}
			public bool OnceScoutType  //add this property at 2008-10-22 14:00:23@Simon
			{
				get{return ((ReportInfoLabel)Component).OnceScoutType;}
				set{((ReportInfoLabel)Component).OnceScoutType = value;}
			}

			public Font Font
			{
				get{return ((ReportInfoLabel)Component).Font;}
				set{((ReportInfoLabel)Component).Font = value;}
			}

			public Color TextColor
			{
				get{return ((ReportInfoLabel)Component).TextColor;}
				set{((ReportInfoLabel)Component).TextColor = value;}
			}

			public Color BackColor
			{
				get{return ((ReportInfoLabel)Component).BackColor;}
				set{((ReportInfoLabel)Component).BackColor = value;}
			}

			public string Text
			{
				get{return ((ReportInfoLabel)Component).Text;}
				set{((ReportInfoLabel)Component).Text = value;}
			}

			public DevExpress.Utils.HorzAlignment HorzAlignment
			{
				get{return ((ReportInfoLabel)Component).HorzAlignment;}
				set{((ReportInfoLabel)Component).HorzAlignment = value;}
			}

			protected override void FillActionItemCollection(CurrentDesign.DesignerActionItemCollection actionItems)
			{
				base.FillActionItemCollection (actionItems);
				AddPropertyItem(actionItems, "Text", "Text", "Text");
				AddPropertyItem(actionItems, "LabelType", "LabelType", "LabelType");
				AddPropertyItem(actionItems, "Multiline", "Multiline", "Multiline");
				AddPropertyItem(actionItems, "WordWrap", "WordWrap", "WordWrap");
				AddPropertyItem(actionItems, "Title", "Title", "Title");
				AddPropertyItem(actionItems, "Object", "Object", "Object");
				AddPropertyItem(actionItems, "FirstObjectOnly", "FirstObjectOnly", "1st Object Only");
				AddPropertyItem(actionItems, "Opponent", "Opponent", "Opponent");
				AddPropertyItem(actionItems, "Date", "Date", "Date");
				AddPropertyItem(actionItems, "ScoutType", "ScoutType", "Scout Type");
				AddPropertyItem(actionItems, "OnceScoutType", "OnceScoutType", "Show Scout Type Once");
				AddPropertyItem(actionItems, "Location", "Location", "Location");
				AddPropertyItem(actionItems, "Font", "Font", "Font");
				AddPropertyItem(actionItems, "TextColor", "TextColor", "TextColor");
				AddPropertyItem(actionItems, "BackColor", "BackColor", "BackColor");
				//AddPropertyItem(actionItems, "HorzAlignment", "HorzAlignment", "Align");
			}
		}
	}
}
