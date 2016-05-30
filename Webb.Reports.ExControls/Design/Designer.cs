/***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:Designer.cs
 * Author:Country.Wu [EMail:Webb.Country.Wu@163.com]
 * Create Time:10/31/2007 03:15:09 PM
 * Copyright:1986-2007@Webb Electronics all right reserved.
 * Purpose:
 * ***********************************************************************/
#region History
/*
 * //Author@DateTime : Description
 * */
#endregion History

#define OldVersion

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
	#region public class ExControlDesigner
	/*Descrition:   */
    public abstract class ExControlDesigner : ControlDesigner
	{
        
		//
		protected Webb.Reports.ExControls.ExControl _Control;
        protected CurrentDesign.DesignerActionListCollection _ActionLists;
		protected DesignerVerbCollection _Verbs;
		protected UI.ExControlDesignerFormBase _DesignForm;
		protected OpenFileDialog _OpenFileDialog;
		protected SaveFileDialog _SaveFileDialog;
		// 
		//Properties
		public override DesignerVerbCollection Verbs
		{
			get
			{
				return this._Verbs;
			}
		}

        public CurrentDesign.DesignerActionListCollection ActionLists 
		{
			get 
			{
				return _ActionLists;
			}
		}
		//
		public ExControlDesigner()
		{
		}

		public override void Initialize(IComponent component)
		{
			base.Initialize (component);
			this._Control = component as Webb.Reports.ExControls.ExControl;
			this._Verbs = new DesignerVerbCollection();
			this.InitializeVerbs();
			this._ActionLists = new CurrentDesign.DesignerActionListCollection();
			this.InitializeActionList();
		}

		public virtual void InitializeActionList()
		{
			this._ActionLists.Add(new ExControlActionList(this));
		}

		public virtual void InitializeVerbs()
		{
			//
			this._Verbs	.Add(new DesignerVerb("Run Designer",new EventHandler(RunDesigner)));
			this._Verbs	.Add(new DesignerVerb("Export Control Data",new EventHandler(ExportView)));
			this._Verbs	.Add(new DesignerVerb("Import Control Data",new EventHandler(ImportView)));
			this._Verbs	.Add(new DesignerVerb("Auto Adjust Size",new EventHandler(AutoAdjustSize)));
			this._Verbs	.Add(new DesignerVerb("3D View",new EventHandler(Switch3DFeel)));	//Scott@12102008
		}

		public virtual void InitializeVerbs(bool bDesignForm)
		{
			if(bDesignForm) this._Verbs.Add(new DesignerVerb("Run Designer",new EventHandler(RunDesigner)));
			this._Verbs	.Add(new DesignerVerb("Export Control Data",new EventHandler(ExportView)));
			this._Verbs	.Add(new DesignerVerb("Import Control Data",new EventHandler(ImportView)));
			this._Verbs	.Add(new DesignerVerb("Auto Adjust Size",new EventHandler(AutoAdjustSize)));
			this._Verbs	.Add(new DesignerVerb("3D View",new EventHandler(Switch3DFeel)));	//Scott@12102008
		}

		public void AutoAdjustSize(object sender,EventArgs e)
		{
			this._Control.AutoAdjustSize();
		}

		public void Switch3DFeel(object sender,EventArgs e)	//Scott@12102008
		{
			this._Control.ThreeD = !(this._Control.ThreeD);	//Modified at 2008-12-22 15:58:06@Scott
		}

		public virtual void RunDesigner(object sender,EventArgs e)
		{
			
		}

		protected void ExportView(object sender,EventArgs e)
		{
			if(this._SaveFileDialog==null)
			{
				this._SaveFileDialog = new SaveFileDialog();
				this._SaveFileDialog.DefaultExt = "exc";
				this._SaveFileDialog.Filter = "Webb Report Extened Control files (*.exc)|*.exc";
			}
			if(this._SaveFileDialog.ShowDialog()==DialogResult.OK)
			{
				string m_FilePath = this._SaveFileDialog.FileName;
				if(this._Control.Save(m_FilePath))
				{
					Webb.Utilities.MessageBoxEx.ShowMessage("Success to export control data.");
				}
				else
				{
					Webb.Utilities.MessageBoxEx.ShowError("Failed to export control data. Please contact Webb for help.");
				}
			}
		}

		protected void ImportView(object sender,EventArgs e)
		{
			if(this._OpenFileDialog==null)
			{
				this._OpenFileDialog = new OpenFileDialog();
				this._OpenFileDialog.DefaultExt = "exc";
				this._OpenFileDialog.Filter = "Webb Report Extened Control files (*.exc)|*.exc";
			}
			if(this._OpenFileDialog.ShowDialog()==DialogResult.OK)
			{
				string m_FilePath = this._OpenFileDialog.FileName;
				if(this._Control.Load(m_FilePath))
				{
					Webb.Utilities.MessageBoxEx.ShowMessage("Success to import control data.");					
				}
				else
				{
					Webb.Utilities.MessageBoxEx.ShowError("Failed to import control data. Please contact Webb for help.");
				}
			}
		}

		#region internal class GroupActionList : DesignerActionList 
		internal class ExControlActionList : CurrentDesign.DesignerActionList 
		{
			//Wu.Country@2007-10-31 16:04 modified some of the following code.
			#region static
			static protected CurrentDesign.DesignerActionPropertyItem CreatePropertyItem(string memberName, ReportStringId id) 
			{
                return new CurrentDesign.DesignerActionPropertyItem(memberName, ReportLocalizer.GetString(id), "");
			}
			//
            static protected CurrentDesign.DesignerActionPropertyItem CreatePropertyItem(string memberName, string id) 
			{
                return new CurrentDesign.DesignerActionPropertyItem(memberName, id, "");
			}
			//
			static protected PropertyDescriptor GetPropertyDescriptor(string name, object editedObject) 
			{
				return TypeDescriptor.GetProperties(editedObject)[name];
			}
            protected static void AddPropertyItem(CurrentDesign.DesignerActionItemCollection actionItems, string name, string reflectName, ReportStringId id, object editedObject) 
			{
				if(GetPropertyDescriptor(name, editedObject) != null)
					actionItems.Add(CreatePropertyItem(reflectName, id));
			}

			//
            protected static void AddPropertyItem(CurrentDesign.DesignerActionItemCollection actionItems, string name, string reflectName, string id, object editedObject) 
			{
				if(GetPropertyDescriptor(name, editedObject) != null)
					actionItems.Add(CreatePropertyItem(reflectName, id));
			}
			#endregion

			protected ExControlDesigner _ExControlDesigner;
			/// <summary>
			/// 
			/// </summary>
			[
			Category("Data"), 
			Description("Gets or sets the data source member whose data is manipulated by the DataNavigator control."), DefaultValue(""), Editor(ControlConstants.DataMemberEditor, typeof(System.Drawing.Design.UITypeEditor)),
			DevExpress.XtraReports.SRCategory(DevExpress.XtraReports.Localization.ReportStringId.CatData)]
			public string DataMember
			{
				get { return ((DevExpress.XtraReports.IExControl)Component).DataMember; }
				set { SetPropertyValue("DataMember", value); }
			}

			[Category("Data"), Description("Gets or sets the source of data displayed in the dropdown window."), DefaultValue(null),
			TypeConverter("System.Windows.Forms.Design.DataSourceConverter, System.Design"),
			DevExpress.XtraReports.SRCategory(DevExpress.XtraReports.Localization.ReportStringId.CatData)]
			public object DataSource 
			{
				get { return ((DevExpress.XtraReports.IExControl)Component).DataSource; }
				set { SetPropertyValue("DataSource", value); }
			}

			public bool Repeat	//Modified at 2008-12-22 11:29:57@Scott
			{
				get { return ((ExControl)Component).Repeat; }
				set { ((ExControl)Component).Repeat = value; }
			}

			//		public GroupTypes GroupType
			//		{
			//			get{return ((GroupingControl)Component).GroupType;}
			//			set { SetPropertyValue("GroupType", value); }
			//		}

			public ExControlActionList(ExControlDesigner designer) : base(designer.Component) 
			{
				this._ExControlDesigner = designer;
			}

            protected virtual void FillActionItemCollection(CurrentDesign.DesignerActionItemCollection actionItems) 
			{
				AddPropertyItem(actionItems, "DataMember", "DataMember", ReportStringId.STag_Name_DataMember);
				AddPropertyItem(actionItems, "DataSource", "DataSource", ReportStringId.STag_Name_DataSource);
				AddPropertyItem(actionItems, "Repeat", "Repeat","Repeat");
				//AddPropertyItem(actionItems, "GroupType", "GroupType", "GroupType");
			}

			//Modified at 2008-12-22 11:34:26@Scott
			protected virtual void FillActionItemCollectionWithoutDataSource(CurrentDesign.DesignerActionItemCollection actionItems) 
			{
				AddPropertyItem(actionItems, "Repeat", "Repeat","Repeat");
				//AddPropertyItem(actionItems, "GroupType", "GroupType", "GroupType");
			}

			protected void SetPropertyValue(string name, object val) 
			{
				SetPropertyValue(Component, name, val);
			}
			protected void SetPropertyValue(object component, string name, object val) 
			{
				EditorContextHelper.SetPropertyValue(this._ExControlDesigner, component, name, val);
				EditorContextHelper.FireChanged(this._ExControlDesigner, Component);
			}
            public override CurrentDesign.DesignerActionItemCollection GetSortedActionItems() 
			{
                CurrentDesign.DesignerActionItemCollection actionItems = new CurrentDesign.DesignerActionItemCollection();
				FillActionItemCollection(actionItems);
				return actionItems;
			}
            protected void AddPropertyItem(CurrentDesign.DesignerActionItemCollection actionItems, string name, string reflectName, ReportStringId id) 
			{
				AddPropertyItem(actionItems, name, reflectName, id, Component);
			}

            protected void AddPropertyItem(CurrentDesign.DesignerActionItemCollection actionItems, string name, string reflectName, string id) 
			{
				AddPropertyItem(actionItems, name, reflectName, id, Component);
			}
		}
		#endregion
	}
	#endregion
	
}
