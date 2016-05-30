/***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:Class.cs
 * Author:Country.Wu [EMail:Webb.Country.Wu@163.com]
 * Create Time:10/31/2007 03:08:16 PM
 * Copyright:1986-2007@Webb Electronics all right reserved.
 * Purpose:
 * ***********************************************************************/
#region History
/*
 * //Author@DateTime : Description
 * */
#endregion History

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

using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization;

using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Container;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraPrinting;
using DevExpress.Utils.Design;
using DevExpress.Utils;
using DevExpress.XtraReports;
using DevExpress.XtraReports.UI;

using Webb;
using Webb.Data;
using Webb.Utilities;

namespace Webb.Reports.ExControls
{
	public enum GroupTypes{NoGroup = 0, MultiGroup = 1, NestingGroup = 2, BandedGroup = 3,};
	public enum FootballTypes{Both = 0, Offense=1, Defense=2,}
	public enum ClickEvent{None=0, PlayVideo = 1,}

	#region public class ClickEventManager
	/*Descrition:   */
	public class ClickEventManager
	{
		//Wu.Country@2007-11-13 08:06 AM added this class.
		//Fields

		//Properties

		//ctor
		public ClickEventManager()
		{
		}
		//Methods

	}
	#endregion

	#region public class ResourceManager
	/*Descrition:   */
	public class ResourceManager
	{
		//Wu.Country@2007-10-31 02:50 PM added this class.
		//Fields

		//Properties

		//ctor
		public ResourceManager()
		{
		}
		//Methods
	}
	#endregion	

	#region public class ExControlBase
	/*Descrition:   */
	[ToolboxItem("Extended Control"),
	XRDesigner("Webb.Reports.ExControls.Design.GroupDesigner,Webb.Reports.ExControls"),
	Designer("Webb.Reports.ExControls.Design.GroupDesigner,Webb.Reports.ExControls"),
	Docking(DockingBehavior.Ask),ComplexBindingProperties("DataSource", "DataMember"),
	ToolboxBitmap(typeof(Webb.Reports.ExControls.ResourceManager), "Bitmaps.Group.bmp")
	]
	public class ExControl : EditorContainer,IExControl
	{
		//Wu.Country@2007-11-08 11:03 AM added this class.
		//Fields
		private WinControlContainer _ControlContainer;

		//Properties
		//ctor
		public ExControl()
		{
		}
		//Methods

		#region IExControl Members

		public WinControlContainer XtraContainer
		{
			get
			{
				// TODO:  Add ExControlBase.XtraContainer getter implementation
				return this._ControlContainer;
			}
			set
			{
				// TODO:  Add ExControlBase.XtraContainer setter implementation
				this._ControlContainer = value;
			}
		}

		public object DataSource
		{
			get
			{
				// TODO:  Add ExControlBase.DataSource getter implementation
				return null;
			}
			set
			{
				// TODO:  Add ExControlBase.DataSource setter implementation
			}
		}

		public string DataMember
		{
			get
			{
				// TODO:  Add ExControlBase.DataMember getter implementation
				return null;
			}
			set
			{
				// TODO:  Add ExControlBase.DataMember setter implementation
			}
		}

		#endregion

		#region IBasePrintable Members

		virtual public void Initialize(IPrintingSystem ps, ILink link)
		{
			// TODO:  Add ExControlBase.Initialize implementation
		}

		virtual public void CreateArea(string areaName, IBrickGraphics graph)
		{
			// TODO:  Add ExControlBase.CreateArea implementation
		}

		virtual public void Finalize(IPrintingSystem ps, ILink link)
		{
			// TODO:  Add ExControlBase.Finalize implementation
		}

		#endregion

		#region protected override EditorContainerHelper CreateHelper()
		protected override EditorContainerHelper CreateHelper()
		{
			return new ExControlHelper(this);
		}

		public class ExControlHelper : EditorContainerHelper 
		{
			public ExControlHelper(ExControl owner) : base(owner) 
			{
			}

			protected new ExControl Owner { get { return base.Owner as ExControl; } }

			protected override void OnRepositoryItemRemoved(RepositoryItem item) 
			{
			}

			protected override void OnRepositoryItemChanged(RepositoryItem item) 
			{
			}
			protected override void RaiseInvalidValueException(InvalidValueExceptionEventArgs e) 
			{
			}
			protected override void RaiseValidatingEditor(BaseContainerValidateEditorEventArgs va) 
			{
			}
		}
		#endregion

	}
	#endregion


}
