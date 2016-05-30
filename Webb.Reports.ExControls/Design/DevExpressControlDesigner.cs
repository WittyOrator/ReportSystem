/***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:DevExpressControlDesigner.cs
 * Author:Country.Wu [EMail:Webb.Country.Wu@163.com]
 * Create Time:11/21/2007 01:58:53 PM
 * Copyright:1986-2007@Webb Electronics all right reserved.
 * Purpose:
 * ***********************************************************************/
#region History
/*
 * //Author@DateTime : Description
 * */
#endregion History

//using System;
//using System.Windows;
//using System.Windows.Forms;
//using System.ComponentModel;
//using System.ComponentModel.Design;
//using System.Windows.Forms.Design;
//
//using DevExpress.XtraReports;
//using DevExpress.XtraReports.UI;
//using DevExpress.XtraReports.Design;
//using DevExpress.Utils.Design;
//using DevExpress.XtraReports.Localization;
//using DevExpress.Utils;
//using DevExpress.XtraGrid.Design;
//
//namespace Webb.Reports.ExControls.Design
//{
//	#region public class PivotGridReportDesign : DevExpress.XtraPivotGrid.Design.PivotGridControlDesigner
//	//Wu.Country@2007-11-29 13:07 added this region.
//
//
//	//Scott@2007-11-22 10:00 modified some of the following code.
//	/// <summary>
//	/// Summary description for DevExpressControlDesigner.
//	/// </summary>
//	public class PivotGridReportDesign : DevExpress.XtraPivotGrid.Design.PivotGridControlDesigner
//	{
//		DesignerVerbCollection _Verbs;
//		DesignerActionListCollection _ActionLists;
//
//		public override DesignerActionListCollection ActionLists
//		{
//			get
//			{
//				if(this._ActionLists == null) this._ActionLists = CreateActionLists();
//
//				return this._ActionLists;
//			}
//		}
//		
//		protected override DesignerActionListCollection CreateActionLists()
//		{
//			DesignerActionListCollection m_ALCollection = new DesignerActionListCollection();
//
//			this.RegisterActionLists(m_ALCollection);
//			
//			return m_ALCollection;
//		}
//
//		protected override void RegisterActionLists(DesignerActionListCollection list)
//		{
//			list.Add(new DevExpressActionList(this));
//		}
//
//		public PivotGridReportDesign()
//		{
//			//
//			// TODO: Add constructor logic here
//			//
//		}
//
//		public override void Initialize(IComponent component)
//		{
//			base.Initialize (component);
//			
//			this._Verbs = new DesignerVerbCollection();
//
//			foreach(System.ComponentModel.Design.DesignerVerb i_Verb in base.Verbs)
//			{
//				if(i_Verb.Text != "About")
//				{
//					this._Verbs.Add(i_Verb);
//				}
//			}
//		}
//
//		public override DesignerVerbCollection Verbs
//		{
//			get
//			{
//				return this._Verbs;
//			}
//		}
//
//	}
//	#endregion
//
//	#region public class GridReportDesign : DevExpress.XtraGrid.Design.GridControlDesigner
//	//Wu.Country@2007-11-29 13:08 added this region.
//	public class GridReportDesign : DevExpress.XtraGrid.Design.GridControlDesigner
//	{
//		DesignerVerbCollection _Verbs;
//		DesignerActionListCollection _ActionLists;
//
//		public override DesignerActionListCollection ActionLists 
//		{
//			get 
//			{
//				if (_ActionLists == null) _ActionLists = CreateActionLists();
//
//				return _ActionLists;
//			}
//		}
//
//		protected override DesignerActionListCollection CreateActionLists() 
//		{
//			DesignerActionListCollection m_Collection = new DesignerActionListCollection();
//
//			RegisterActionLists(m_Collection);
//			
//			return m_Collection;
//		}
//
//		protected override void RegisterActionLists(DesignerActionListCollection list) 
//		{
//			list.Add(new DevExpressActionList(this));
//		}
//
//		public GridReportDesign(){}
//
//		public override void Initialize(IComponent component)
//		{
//			base.Initialize (component);
//			
//			this._Verbs = new DesignerVerbCollection();
//			
//			foreach( System.ComponentModel.Design.DesignerVerb m_verb in base.Verbs)
//			{
//				if(m_verb.Text!="About")
//				{
//					this._Verbs.Add(m_verb);
//				}
//			}
//		}
//
//		public override DesignerVerbCollection Verbs
//		{
//			get{return this._Verbs;}
//		}
//	}
//	#endregion
//
//	#region public class DevExpressActionList : DesignerActionList 
//	//Wu.Country@2007-11-29 13:08 added this region.
//	public class DevExpressActionList : DesignerActionList 
//	{
//		ControlDesigner designer;
//		
//		static protected DesignerActionPropertyItem CreatePropertyItem(string memberName, ReportStringId id) 
//		{
//			return new DesignerActionPropertyItem(memberName, ReportLocalizer.GetString(id), "");
//		}
//
//		static protected PropertyDescriptor GetPropertyDescriptor(string name, object editedObject) 
//		{
//			return TypeDescriptor.GetProperties(editedObject)[name];
//		}
//
//		protected static void AddPropertyItem(DesignerActionItemCollection actionItems, string name, string reflectName, ReportStringId id, object editedObject) 
//		{
//			if(GetPropertyDescriptor(name, editedObject) != null)
//				actionItems.Add(CreatePropertyItem(reflectName, id));
//		}
//		
//		[Category("Data"), 
//		Description("Gets or sets the data source member whose data is manipulated by the DataNavigator control."), DefaultValue(""), 
//		Editor(ControlConstants.DataMemberEditor, typeof(System.Drawing.Design.UITypeEditor)),
//		DevExpress.XtraReports.SRCategory(DevExpress.XtraReports.Localization.ReportStringId.CatData)]
//		public string DataMember
//		{
//			get { return ((IExControl)Component).DataMember; }
//			set { SetPropertyValue("DataMember", value); }
//		}
//
//		[Category("Data"), Description("Gets or sets the source of data displayed in the dropdown window."), DefaultValue(null),
//		TypeConverter("System.Windows.Forms.Design.DataSourceConverter, System.Design"),
//		DevExpress.XtraReports.SRCategory(DevExpress.XtraReports.Localization.ReportStringId.CatData)]
//		public object DataSource 
//		{
//			get { return ((IExControl)Component).DataSource; }
//			set { SetPropertyValue("DataSource", value); }
//		}
//        
//		public DevExpressActionList(ControlDesigner designer) : base(designer.Component) 
//		{
//			this.designer = designer;
//		}
//
//		protected void FillActionItemCollection(DesignerActionItemCollection actionItems) 
//		{
//			AddPropertyItem(actionItems, "DataMember", "DataMember", ReportStringId.STag_Name_DataMember);
//			AddPropertyItem(actionItems, "DataSource", "DataSource", ReportStringId.STag_Name_DataSource);
//		}
//
//		protected void SetPropertyValue(string name, object val) 
//		{
//			SetPropertyValue(Component, name, val);
//		}
//		protected void SetPropertyValue(object component, string name, object val) 
//		{
//			EditorContextHelper.SetPropertyValue(designer, component, name, val);
//			EditorContextHelper.FireChanged(designer, Component);
//		}
//		public override DesignerActionItemCollection GetSortedActionItems() 
//		{
//			DesignerActionItemCollection actionItems = new DesignerActionItemCollection();
//			FillActionItemCollection(actionItems);
//			return actionItems;
//		}
//		protected void AddPropertyItem(DesignerActionItemCollection actionItems, string name, string reflectName, ReportStringId id) 
//		{
//			AddPropertyItem(actionItems, name, reflectName, id, Component);
//		}
//	}
//	#endregion
//
//	#region public class NewClass
//	/*Descrition:   */
//	public class WebbChartControlDesign : ExControlDesigner
//	{
//		//Wu.Country@2007-11-29 01:07 PM added this class.
//		//Fields
//
//		//Properties
//
//		//ctor
//		public WebbChartControlDesign()
//		{
//		}
//		//Methods
//	}
//	#endregion
//
//}
