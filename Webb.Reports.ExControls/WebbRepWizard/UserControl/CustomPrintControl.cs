using System;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting.Drawing;
using System.Drawing;

namespace Webb.Reports.ReportWizard.WizardInfo
{
	/// <summary>
	/// Summary description for CustomPrintControl.
	/// </summary>
	public class CustomPrintControl: DevExpress.XtraPrinting.Control.PrintControl
	{
		
			private DevExpress.XtraPrinting.PrintingSystem ps;
			public CustomPrintControl() 
			{
				SetControlVisibility(new Control[] {vScrollBar, hPanel}, false);
				ps = new DevExpress.XtraPrinting.PrintingSystem();
				PrintingSystem = ps;
				fMinZoom = 0.00001f;
			}
			public void LoadReport(WebbReport i_Report)
			{				
				if(this.PrintingSystem==null) this.PrintingSystem = new DevExpress.XtraPrinting.PrintingSystem();
				this.PrintingSystem.ClearContent();
				this.Invalidate();
				this.Update();
				i_Report.PrintingSystem = this.PrintingSystem;
				i_Report.CreateDocument();			
			}
			
			void SetControlVisibility(Control[] controls, bool visible) 
			{
				foreach(Control control in controls)
					control.Visible = visible;
			}
			public void SetLandScape(bool landscape)
			{
				ps.PageSettings.Landscape=landscape;
			}
			protected override void OnHandleCreated(EventArgs e) 
			{
				base.OnHandleCreated(e);
				CreateDocument();
				ViewWholePage();
			}
			private void CreateDocument() 
			{
				ps.Begin();
				ps.Graph.Modifier =DevExpress.XtraPrinting.BrickModifier.Detail;
				DevExpress.XtraPrinting.EmptyBrick brick = new DevExpress.XtraPrinting.EmptyBrick();
				brick.Rect = new RectangleF(0, 0, 100, 100);
				ps.Graph.DrawBrick(brick);
				ps.End();
			}
			public void Update(Watermark watermark) 
			{
				ps.Watermark.CopyFrom(watermark);
				ps.Watermark.PageRange = "";
				Invalidate(true);
			} 
			protected override void Dispose(bool disposing) 
			{
				if(disposing) 
				{
					ps.Dispose();
				}
				base.Dispose(disposing);
			}
			public RectangleF GetPageRect()
			{
				return ps.PageSettings.UsablePageRect;
				
			}
		}
}
