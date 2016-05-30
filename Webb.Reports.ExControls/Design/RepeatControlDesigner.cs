 /***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:RepeatControlDesigner.cs
 * Author:Automatic Macro@simon
 * Create Time:2009-6-18 15:26:50
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

namespace Webb.Reports.ExControls.Design
{
	public class RepeatControlDesigner : ExControlDesigner
	{
		public RepeatControlDesigner()
		{
		}		

		public override  void InitializeActionList()
		{			
			this._ActionLists.Add(new RepeatControlActionList(this));		
		}
		public override void InitializeVerbs()
		{
			base.InitializeVerbs(false);			
		}
	

		internal class RepeatControlActionList : Webb.Reports.ExControls.Design.ExControlDesigner.ExControlActionList
		{
			public RepeatControlActionList(Webb.Reports.ExControls.Design.ExControlDesigner designer) : base(designer)
			{
			}

			[EditorAttribute(typeof(Webb.Reports.Editors.PageGroupEditor), typeof(System.Drawing.Design.UITypeEditor))]
			public Webb.Reports.PageGroupInfo RepeatSetting
			{
				get{ return ((RepeatControl)Component).RepeatSetting; }
				set{ ((RepeatControl)Component).RepeatSetting=value; }
			}

			public float RepeatedWidth
			{
				get{ return ((RepeatControl)Component).RepeatedWidth; }
				set{ ((RepeatControl)Component).RepeatedWidth=value; }
			}

			public float RepeatedHeight
			{
				get{ return ((RepeatControl)Component).RepeatedHeight; }
				set{ ((RepeatControl)Component).RepeatedHeight=value; }
			}

			public int RepeatedCount
			{
				get{ return ((RepeatControl)Component).RepeatedCount; }
				set{ ((RepeatControl)Component).RepeatedCount=value; }
			}

			public int RepeatedVerticalCount
			{
				get{ return ((RepeatControl)Component).RepeatedVerticalCount; }
				set{ ((RepeatControl)Component).RepeatedVerticalCount=value; }
			}
			public bool AfterLast
			{
				get{ return ((RepeatControl)Component).AfterLast; }
				set{ ((RepeatControl)Component).AfterLast=value; }
			}

			public int RepeatTopCount
			{
				get{ return ((RepeatControl)Component).RepeatTopCount; }
				set{ ((RepeatControl)Component).RepeatTopCount=value; }
			}

			public System.Drawing.Color ChartColorWhenMax
			{
				get{ return ((RepeatControl)Component).ChartColorWhenMax; }
				set{ ((RepeatControl)Component).ChartColorWhenMax=value; }
			}

			protected override void FillActionItemCollection(CurrentDesign.DesignerActionItemCollection  actionItems)
			{
//				base.FillActionItemCollection (actionItems);
				AddPropertyItem(actionItems,"AfterLast","AfterLast", "Print After Previous SubReport Control");
				AddPropertyItem(actionItems,"RepeatSetting","RepeatSetting", "RepeatSetting");
				AddPropertyItem(actionItems,"RepeatedWidth","RepeatedWidth", "RepeatedWidth");
				AddPropertyItem(actionItems,"RepeatedHeight","RepeatedHeight", "RepeatedHeight");
				AddPropertyItem(actionItems,"RepeatedCount","RepeatedCount", "RepeatedHorizonCount");
				AddPropertyItem(actionItems,"RepeatedVerticalCount","RepeatedVerticalCount", "RepeatedVerticalCount");
				AddPropertyItem(actionItems,"RepeatTopCount","RepeatTopCount", "RepeatTopCount");
				AddPropertyItem(actionItems,"ChartColorWhenMax","ChartColorWhenMax", "ChartColor When MaxValue");			
			}
		}
	}
}
