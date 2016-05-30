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

using System.Drawing.Drawing2D;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Windows.Forms.Design;
using System.ComponentModel.Design.Serialization;
using System.Windows.Forms.ComponentModel;
using System.Reflection;
using DevExpress.XtraEditors.Design;

using DevExpress.XtraReports.Design;
using DevExpress.XtraReports.Localization;
using DevExpress.XtraReports.UI;
using Webb.Reports.ExControls.UI;

using Webb.Reports.ExControls.Views;
using Webb.Reports.ExControls.Data;

using CurrentDesign = DevExpress.Utils.Design;  //08-13-2008@Scott

namespace Webb.Reports.ExControls.Design
{
	#region ExShapeDesigner
	public class ExShapeDesigner: ExControlDesigner
	{
		public ExShapeDesigner()
		{			
		}
		public override void Initialize(IComponent component)
		{
			base.Initialize (component);
		}
		public override void RunDesigner(object sender, EventArgs e)
		{				
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
		
		public override void InitializeActionList()
		{
			this._ActionLists.Add(new ShapesActionList(this));
		}
		#region internal class ShapesActionList : ExControlActionList
		/*Descrition:   */
		internal class ShapesActionList : ExControlActionList
		{
			//internal class added for the action below the task of "prorperty designing  mode"
			public ShapesActionList(ExControlDesigner designer) : base(designer) 
			{
			}
		
			[DefaultValue("Black"),Description("Border color"),Category("Appearance")] 
			public Color BorderColor
			{
				get 
				{					
					return ((ExShapeControl)Component).BorderColor;
				}
				set 
				{
					((ExShapeControl)Component).BorderColor=value;					
				}
			}
			[DefaultValue(1),Description("Border width"),Category("Appearance")] 
			public int BorderWidth
			{
				get 
				{					
					return ((ExShapeControl)Component).BorderWidth;
				}
				set 
				{
					((ExShapeControl)Component).BorderWidth=value;					
				}
			}

			public FillStyle FillStyle
			{
				get{return ((ExShapeControl)Component).FillStyle;}
				set{((ExShapeControl)Component).FillStyle=value;}
			}
			public Image FillImage
			{
				get{return ((ExShapeControl)Component).FillImage;}
				set{((ExShapeControl)Component).FillImage=value;}
			}
             public bool AutoFit
			 {
				get{return ((ExShapeControl)Component).AutoFit;}
				set{((ExShapeControl)Component).AutoFit=value;}
			 }

			[Description("Border style"),Category("Appearance")] 
			public DashStyle BorderStyle
			{
				get 
				{					
					return ((ExShapeControl)Component).BorderStyle;
				}
				set 
				{
					((ExShapeControl)Component).BorderStyle=value;					
				}
			}
             public HatchStyle HatchStyle
			{
				get 
				{					
					return ((ExShapeControl)Component).HatchStyle;
				}
				set 
				{
					((ExShapeControl)Component).HatchStyle=value;					
				}
			}
		
			[Description("Shape style mode"),Category("Appearance")] 
			public ShapeStyleMode shape
			{
				get{return ((ExShapeControl)Component).shape;}
				set{
					((ExShapeControl)Component).shape=value;  
				   }
			}
			

			[Description("Line style"),Category("LineDirection")] 
			public LineStyle LineDirection
			{
				get
				{
					return ((ExShapeControl)Component).LineDirection;
				}
				set 
				{
					((ExShapeControl)Component).LineDirection=value;
				}
			}

			[Description("Line Arrow Style")] 
			public LineArrowStyle LineArrow
			{
				get
				{
					return ((ExShapeControl)Component).LineArrow;
				}
				set 
				{
					((ExShapeControl)Component).LineArrow=value;
				}
			}
          
			public Color FillColor
			{
				get
				{
					return ((ExShapeControl)Component).FillColor;
				}
				set 
				{
					((ExShapeControl)Component).FillColor=value;
				}

			} 
			
		
			protected override void FillActionItemCollection(CurrentDesign.DesignerActionItemCollection actionItems)
			{
				//base.FillActionItemCollection (actionItems);
				base.FillActionItemCollectionWithoutDataSource(actionItems);	//Modified at 2008-12-22 11:35:53@Scott	
				AddPropertyItem(actionItems, "shape", "shape", "Shape");
				if(this.shape==ShapeStyleMode.Rectangle)
				{
					AddPropertyItem(actionItems, "AutoFit", "AutoFit", "Auto size when previewing/printing");					
				}

				AddPropertyItem(actionItems, "BorderWidth", "BorderWidth", "BorderWidth");	
				AddPropertyItem(actionItems, "BorderStyle", "BorderStyle", "BorderStyle");
				AddPropertyItem(actionItems, "BorderColor", "BorderColor", "BorderColor");
				if(this.shape==ShapeStyleMode.Line)
				{
					AddPropertyItem(actionItems, "LineDirection", "LineDirection", "LineDirection");
					AddPropertyItem(actionItems, "LineArrow", "LineArrow", "Line Arrow Style");
				}
				else
				{
					AddPropertyItem(actionItems, "FillStyle", "FillStyle", "Filled");
					AddPropertyItem(actionItems, "FillColor", "FillColor", "FillColor");				
					AddPropertyItem(actionItems, "HatchStyle", "HatchStyle","HatchStyle");
					AddPropertyItem(actionItems, "FillImage", "FillImage", "FillImage");	
				}
			}
		}
		#endregion

	}
	#endregion
}
