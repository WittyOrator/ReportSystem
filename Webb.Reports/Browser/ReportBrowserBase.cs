/***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:ReportBrowserBase.cs
 * Author:Country.Wu [EMail:Webb.Country.Wu@163.com]
 * Create Time:11/8/2007 11:01:14 AM
 * Copyright:1986-2007@Webb Electronics all right reserved.
 * Purpose:
 * ***********************************************************************/
#region History
/*
 * //Author@DateTime : Description
 * */
#endregion History

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Webb.Reports.Browser
{
	/// <summary>
	/// Summary description for ReportBrowserBase.
	/// </summary>
	public class ReportBrowserBase : System.Windows.Forms.Form
	{
		private DevExpress.XtraBars.BarDockControl barDockControlTop;
		private DevExpress.XtraBars.BarDockControl barDockControlBottom;
		private DevExpress.XtraBars.BarDockControl barDockControlLeft;
		private DevExpress.XtraBars.BarDockControl barDockControlRight;
		private DevExpress.XtraPrinting.Preview.MultiplePagesControlContainer multiplePagesControlContainer1;
//		private DevExpress.XtraPrinting.Preview.ColorPopupControlContainer colorPopupControlContainer1;
		private DevExpress.XtraPrinting.Preview.PrintPreviewRepositoryItemComboBox printPreviewRepositoryItemComboBox1;
		private DevExpress.XtraPrinting.Preview.PreviewBar previewBar1;
		private DevExpress.XtraPrinting.Preview.PreviewBar previewBar2;
		private DevExpress.XtraPrinting.Preview.PreviewBar previewBar3;
		private DevExpress.XtraPrinting.Preview.PrintPreviewStaticItem printPreviewStaticItem1;
		private DevExpress.XtraPrinting.Preview.PrintPreviewStaticItem printPreviewStaticItem2;
		private DevExpress.XtraPrinting.Preview.PrintPreviewStaticItem printPreviewStaticItem3;
		private DevExpress.XtraPrinting.Preview.PrintPreviewBarItem printPreviewBarItem0;
		private DevExpress.XtraPrinting.Preview.PrintPreviewBarItem printPreviewBarItem1;
		private DevExpress.XtraPrinting.Preview.PrintPreviewBarItem printPreviewBarItem2;
		private DevExpress.XtraPrinting.Preview.PrintPreviewBarItem printPreviewBarItem3;
		private DevExpress.XtraPrinting.Preview.PrintPreviewBarItem printPreviewBarItem4;
		private DevExpress.XtraPrinting.Preview.PrintPreviewBarItem printPreviewBarItem5;
		private DevExpress.XtraPrinting.Preview.PrintPreviewBarItem printPreviewBarItem6;
		private DevExpress.XtraPrinting.Preview.PrintPreviewBarItem printPreviewBarItem7;
		protected DevExpress.XtraPrinting.Preview.PrintPreviewBarItem printPreviewBarItem8;
		private DevExpress.XtraPrinting.Preview.PrintPreviewBarItem printPreviewBarItem10;
		private DevExpress.XtraPrinting.Preview.ZoomBarEditItem zoomBarEditItem1;
		private DevExpress.XtraPrinting.Preview.PrintPreviewBarItem printPreviewBarItem11;
		private DevExpress.XtraPrinting.Preview.PrintPreviewBarItem printPreviewBarItem12;
		private DevExpress.XtraPrinting.Preview.PrintPreviewBarItem printPreviewBarItem13;
		private DevExpress.XtraPrinting.Preview.PrintPreviewBarItem printPreviewBarItem14;
		private DevExpress.XtraPrinting.Preview.PrintPreviewBarItem printPreviewBarItem15;
		private DevExpress.XtraPrinting.Preview.PrintPreviewBarItem printPreviewBarItem16;
		private DevExpress.XtraPrinting.Preview.PrintPreviewBarItem printPreviewBarItem17;
		private DevExpress.XtraPrinting.Preview.PrintPreviewBarItem printPreviewBarItem18;
		private DevExpress.XtraPrinting.Preview.PrintPreviewBarItem printPreviewBarItem19;
		private DevExpress.XtraPrinting.Preview.PrintPreviewBarItem printPreviewBarItem20;
		private DevExpress.XtraPrinting.Preview.PrintPreviewBarItem printPreviewBarItem21;
		private DevExpress.XtraPrinting.Preview.PrintPreviewSubItem printPreviewSubItem0;
		private DevExpress.XtraPrinting.Preview.PrintPreviewSubItem printPreviewSubItem1;
		private DevExpress.XtraPrinting.Preview.PrintPreviewSubItem printPreviewSubItem2;
		private DevExpress.XtraPrinting.Preview.PrintPreviewSubItem printPreviewSubItem3;
		private DevExpress.XtraPrinting.Preview.PrintPreviewSubItem printPreviewSubItem4;
		private DevExpress.XtraPrinting.Preview.PrintPreviewBarItem printPreviewBarItem22;
		private DevExpress.XtraPrinting.Preview.PrintPreviewBarItem printPreviewBarItem23;
		private DevExpress.XtraBars.BarToolbarsListItem barToolbarsListItem1;
		private DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem printPreviewBarCheckItem1;
		private DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem printPreviewBarCheckItem2;
		private DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem printPreviewBarCheckItem3;
		private DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem printPreviewBarCheckItem4;
		private DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem printPreviewBarCheckItem6;
		private DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem printPreviewBarCheckItem7;
		private DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem printPreviewBarCheckItem8;
		private DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem printPreviewBarCheckItem9;
		private DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem printPreviewBarCheckItem10;
		private DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem printPreviewBarCheckItem11;
		private DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem printPreviewBarCheckItem13;
		private DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem printPreviewBarCheckItem15;
		private DevExpress.XtraPrinting.Preview.PrintBarManager C_PrintBarManager;
		protected DevExpress.XtraPrinting.Control.PrintControl C_PrintControl;
		private DevExpress.LookAndFeel.DefaultLookAndFeel C_DefaultLookAndFeel;
		private DevExpress.XtraBars.BarButtonItem barButtonItem1;
		private System.ComponentModel.IContainer components;
		public string ReportName=string.Empty;

		public ReportBrowserBase()
		{
		
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ReportBrowserBase));
			this.C_PrintBarManager = new DevExpress.XtraPrinting.Preview.PrintBarManager();
			this.previewBar1 = new DevExpress.XtraPrinting.Preview.PreviewBar();
			this.printPreviewBarItem4 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			this.printPreviewBarItem5 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			this.printPreviewBarItem6 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			this.printPreviewBarItem7 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			this.printPreviewBarItem8 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			this.printPreviewBarItem11 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			this.printPreviewBarItem10 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			this.zoomBarEditItem1 = new DevExpress.XtraPrinting.Preview.ZoomBarEditItem();
			this.printPreviewRepositoryItemComboBox1 = new DevExpress.XtraPrinting.Preview.PrintPreviewRepositoryItemComboBox();
			this.printPreviewBarItem12 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			this.printPreviewBarItem13 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			this.printPreviewBarItem14 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			this.printPreviewBarItem15 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			this.printPreviewBarItem16 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			this.multiplePagesControlContainer1 = new DevExpress.XtraPrinting.Preview.MultiplePagesControlContainer();
			this.printPreviewBarItem17 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
//			this.colorPopupControlContainer1 = new DevExpress.XtraPrinting.Preview.ColorPopupControlContainer();
			this.printPreviewBarItem18 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			this.printPreviewBarItem19 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			this.printPreviewBarItem0 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			this.previewBar2 = new DevExpress.XtraPrinting.Preview.PreviewBar();
			this.printPreviewStaticItem1 = new DevExpress.XtraPrinting.Preview.PrintPreviewStaticItem();
			this.printPreviewStaticItem2 = new DevExpress.XtraPrinting.Preview.PrintPreviewStaticItem();
			this.printPreviewStaticItem3 = new DevExpress.XtraPrinting.Preview.PrintPreviewStaticItem();
			this.previewBar3 = new DevExpress.XtraPrinting.Preview.PreviewBar();
			this.printPreviewSubItem1 = new DevExpress.XtraPrinting.Preview.PrintPreviewSubItem();
			this.printPreviewBarItem20 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			this.printPreviewSubItem2 = new DevExpress.XtraPrinting.Preview.PrintPreviewSubItem();
			this.printPreviewSubItem4 = new DevExpress.XtraPrinting.Preview.PrintPreviewSubItem();
			this.printPreviewBarItem22 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			this.printPreviewBarItem23 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			this.barToolbarsListItem1 = new DevExpress.XtraBars.BarToolbarsListItem();
			this.printPreviewSubItem3 = new DevExpress.XtraPrinting.Preview.PrintPreviewSubItem();
			this.printPreviewSubItem0 = new DevExpress.XtraPrinting.Preview.PrintPreviewSubItem();
			this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			this.printPreviewBarItem1 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			this.printPreviewBarItem2 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			this.printPreviewBarItem3 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			this.printPreviewBarItem21 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			this.printPreviewBarCheckItem1 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem();
			this.printPreviewBarCheckItem2 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem();
			this.printPreviewBarCheckItem3 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem();
			this.printPreviewBarCheckItem4 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem();
			this.printPreviewBarCheckItem6 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem();
			this.printPreviewBarCheckItem7 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem();
			this.printPreviewBarCheckItem8 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem();
			this.printPreviewBarCheckItem9 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem();
			this.printPreviewBarCheckItem10 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem();
			this.printPreviewBarCheckItem11 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem();
			this.printPreviewBarCheckItem13 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem();
			this.printPreviewBarCheckItem15 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem();
			this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
			this.C_PrintControl = new DevExpress.XtraPrinting.Control.PrintControl();
			this.C_DefaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
			((System.ComponentModel.ISupportInitialize)(this.C_PrintBarManager)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.printPreviewRepositoryItemComboBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.multiplePagesControlContainer1)).BeginInit();
//			((System.ComponentModel.ISupportInitialize)(this.colorPopupControlContainer1)).BeginInit();
			this.SuspendLayout();
			// 
			// C_PrintBarManager
			// 
			this.C_PrintBarManager.AllowCustomization = false;
			this.C_PrintBarManager.AllowQuickCustomization = false;
			this.C_PrintBarManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
																				   this.previewBar1,
																				   this.previewBar2,
																				   this.previewBar3});
//			this.C_PrintBarManager.ColorPopupControlContainer = this.colorPopupControlContainer1;
			this.C_PrintBarManager.DockControls.Add(this.barDockControlTop);
			this.C_PrintBarManager.DockControls.Add(this.barDockControlBottom);
			this.C_PrintBarManager.DockControls.Add(this.barDockControlLeft);
			this.C_PrintBarManager.DockControls.Add(this.barDockControlRight);
			this.C_PrintBarManager.Form = this;
			this.C_PrintBarManager.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("C_PrintBarManager.ImageStream")));
			this.C_PrintBarManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
																						this.printPreviewBarItem17,
																						this.printPreviewStaticItem1,
																						this.printPreviewStaticItem2,
																						this.printPreviewStaticItem3,
																						this.printPreviewBarItem0,
																						this.printPreviewBarItem1,
																						this.printPreviewBarItem2,
																						this.printPreviewBarItem3,
																						this.printPreviewBarItem4,
																						this.printPreviewBarItem5,
																						this.printPreviewBarItem6,
																						this.printPreviewBarItem7,
																						this.printPreviewBarItem8,
																						this.printPreviewBarItem10,
																						this.zoomBarEditItem1,
																						this.printPreviewBarItem11,
																						this.printPreviewBarItem12,
																						this.printPreviewBarItem13,
																						this.printPreviewBarItem14,
																						this.printPreviewBarItem15,
																						this.printPreviewBarItem16,
																						this.printPreviewBarItem18,
																						this.printPreviewBarItem19,
																						this.printPreviewBarItem20,
																						this.printPreviewBarItem21,
																						this.printPreviewSubItem0,
																						this.printPreviewSubItem1,
																						this.printPreviewSubItem2,
																						this.printPreviewSubItem3,
																						this.printPreviewSubItem4,
																						this.printPreviewBarItem22,
																						this.printPreviewBarItem23,
																						this.barToolbarsListItem1,
																						this.printPreviewBarCheckItem1,
																						this.printPreviewBarCheckItem2,
																						this.printPreviewBarCheckItem3,
																						this.printPreviewBarCheckItem4,
																						this.printPreviewBarCheckItem6,
																						this.printPreviewBarCheckItem7,
																						this.printPreviewBarCheckItem8,
																						this.printPreviewBarCheckItem9,
																						this.printPreviewBarCheckItem10,
																						this.printPreviewBarCheckItem11,
																						this.printPreviewBarCheckItem13,
																						this.printPreviewBarCheckItem15,
																						this.barButtonItem1});	
			this.C_PrintBarManager.MainMenu = this.previewBar3;
			this.C_PrintBarManager.MaxItemId = 50;
			this.C_PrintBarManager.MultiplePagesControlContainer = this.multiplePagesControlContainer1;
			this.C_PrintBarManager.PreviewBar = this.previewBar1;
			this.C_PrintBarManager.PrintControl = this.C_PrintControl;
			this.C_PrintBarManager.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
																													   this.printPreviewRepositoryItemComboBox1});
			this.C_PrintBarManager.StatusBar = this.previewBar2;
			this.C_PrintBarManager.ZoomItem = this.zoomBarEditItem1;
			// 
			// previewBar1
			// 
			this.previewBar1.BarName = "Toolbar";
			this.previewBar1.DockCol = 0;
			this.previewBar1.DockRow = 1;
			this.previewBar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
			this.previewBar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
																									 new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewBarItem4, true),
																									 new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewBarItem5),
																									 new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewBarItem6),
																									 new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewBarItem7),
																									 new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewBarItem8, true),
																									 new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewBarItem11),
																									 new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewBarItem10),
																									 new DevExpress.XtraBars.LinkPersistInfo(this.zoomBarEditItem1),
																									 new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewBarItem12, true),
																									 new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewBarItem13),
																									 new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewBarItem14),
																									 new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewBarItem15),
																									 new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewBarItem16, true),
																									 new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewBarItem17),
																									 new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewBarItem18),
																									 new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewBarItem19),
																									 new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewBarItem0)});
			this.previewBar1.Text = "Toolbar";
			// 
			// printPreviewBarItem4
			// 
			this.printPreviewBarItem4.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
			this.printPreviewBarItem4.Caption = "&Print...";
			this.printPreviewBarItem4.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Print;
			this.printPreviewBarItem4.Enabled = false;
			this.printPreviewBarItem4.Hint = "Print";
			this.printPreviewBarItem4.Id = 6;
			this.printPreviewBarItem4.ImageIndex = 0;
			this.printPreviewBarItem4.Name = "printPreviewBarItem4";
			// 
			// printPreviewBarItem5
			// 
			this.printPreviewBarItem5.Caption = "P&rint";
			this.printPreviewBarItem5.Command = DevExpress.XtraPrinting.PrintingSystemCommand.PrintDirect;
			this.printPreviewBarItem5.Enabled = false;
			this.printPreviewBarItem5.Hint = "Print Direct";
			this.printPreviewBarItem5.Id = 7;
			this.printPreviewBarItem5.ImageIndex = 1;
			this.printPreviewBarItem5.Name = "printPreviewBarItem5";
			// 
			// printPreviewBarItem6
			// 
			this.printPreviewBarItem6.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
			this.printPreviewBarItem6.Caption = "Page Set&up...";
			this.printPreviewBarItem6.Command = DevExpress.XtraPrinting.PrintingSystemCommand.PageSetup;
			this.printPreviewBarItem6.Enabled = false;
			this.printPreviewBarItem6.Hint = "Page Setup";
			this.printPreviewBarItem6.Id = 8;
			this.printPreviewBarItem6.ImageIndex = 2;
			this.printPreviewBarItem6.Name = "printPreviewBarItem6";
			// 
			// printPreviewBarItem7
			// 
			this.printPreviewBarItem7.Caption = "Header And Footer";
			this.printPreviewBarItem7.Command = DevExpress.XtraPrinting.PrintingSystemCommand.EditPageHF;
			this.printPreviewBarItem7.Enabled = false;
			this.printPreviewBarItem7.Hint = "Header And Footer";
			this.printPreviewBarItem7.Id = 9;
			this.printPreviewBarItem7.ImageIndex = 15;
			this.printPreviewBarItem7.Name = "printPreviewBarItem7";
			// 
			// printPreviewBarItem8
			// 
			this.printPreviewBarItem8.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
			this.printPreviewBarItem8.Caption = "Hand Tool";
			this.printPreviewBarItem8.Command = DevExpress.XtraPrinting.PrintingSystemCommand.HandTool;
			this.printPreviewBarItem8.Enabled = false;
			this.printPreviewBarItem8.Hint = "Hand Tool";
			this.printPreviewBarItem8.Id = 10;
			this.printPreviewBarItem8.ImageIndex = 16;
			this.printPreviewBarItem8.Name = "printPreviewBarItem8";		
			// 
			// printPreviewBarItem11
			// 
			this.printPreviewBarItem11.Caption = "Zoom In";
			this.printPreviewBarItem11.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ZoomIn;
			this.printPreviewBarItem11.Enabled = false;
			this.printPreviewBarItem11.Hint = "Zoom In";
			this.printPreviewBarItem11.Id = 14;
			this.printPreviewBarItem11.ImageIndex = 4;
			this.printPreviewBarItem11.Name = "printPreviewBarItem11";
			// 
			// printPreviewBarItem10
			// 
			this.printPreviewBarItem10.Caption = "Zoom Out";
			this.printPreviewBarItem10.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ZoomOut;
			this.printPreviewBarItem10.Enabled = false;
			this.printPreviewBarItem10.Hint = "Zoom Out";
			this.printPreviewBarItem10.Id = 12;
			this.printPreviewBarItem10.ImageIndex = 5;
			this.printPreviewBarItem10.Name = "printPreviewBarItem10";
			// 
			// zoomBarEditItem1
			// 
			this.zoomBarEditItem1.Caption = "Zoom";
			this.zoomBarEditItem1.Edit = this.printPreviewRepositoryItemComboBox1;
			this.zoomBarEditItem1.EditValue = "100%";
			this.zoomBarEditItem1.Enabled = false;
			this.zoomBarEditItem1.Hint = "Zoom";
			this.zoomBarEditItem1.Id = 13;
			this.zoomBarEditItem1.Name = "zoomBarEditItem1";
			this.zoomBarEditItem1.Width = 70;
			// 
			// printPreviewRepositoryItemComboBox1
			// 
			this.printPreviewRepositoryItemComboBox1.AutoComplete = false;
			this.printPreviewRepositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
																															 new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.printPreviewRepositoryItemComboBox1.DropDownRows = 11;
			this.printPreviewRepositoryItemComboBox1.Name = "printPreviewRepositoryItemComboBox1";
			// 
			// printPreviewBarItem12
			// 
			this.printPreviewBarItem12.Caption = "First Page";
			this.printPreviewBarItem12.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ShowFirstPage;
			this.printPreviewBarItem12.Enabled = false;
			this.printPreviewBarItem12.Hint = "First Page";
			this.printPreviewBarItem12.Id = 15;
			this.printPreviewBarItem12.ImageIndex = 7;
			this.printPreviewBarItem12.Name = "printPreviewBarItem12";
			// 
			// printPreviewBarItem13
			// 
			this.printPreviewBarItem13.Caption = "Previous Page";
			this.printPreviewBarItem13.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ShowPrevPage;
			this.printPreviewBarItem13.Enabled = false;
			this.printPreviewBarItem13.Hint = "Previous Page";
			this.printPreviewBarItem13.Id = 16;
			this.printPreviewBarItem13.ImageIndex = 8;
			this.printPreviewBarItem13.Name = "printPreviewBarItem13";
			// 
			// printPreviewBarItem14
			// 
			this.printPreviewBarItem14.Caption = "Next Page";
			this.printPreviewBarItem14.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ShowNextPage;
			this.printPreviewBarItem14.Enabled = false;
			this.printPreviewBarItem14.Hint = "Next Page";
			this.printPreviewBarItem14.Id = 17;
			this.printPreviewBarItem14.ImageIndex = 9;
			this.printPreviewBarItem14.Name = "printPreviewBarItem14";
			// 
			// printPreviewBarItem15
			// 
			this.printPreviewBarItem15.Caption = "Last Page";
			this.printPreviewBarItem15.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ShowLastPage;
			this.printPreviewBarItem15.Enabled = false;
			this.printPreviewBarItem15.Hint = "Last Page";
			this.printPreviewBarItem15.Id = 18;
			this.printPreviewBarItem15.ImageIndex = 10;
			this.printPreviewBarItem15.Name = "printPreviewBarItem15";
			// 
			// printPreviewBarItem16
			// 
			this.printPreviewBarItem16.ActAsDropDown = true;
			this.printPreviewBarItem16.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
			this.printPreviewBarItem16.Caption = "Multiple Pages";
			this.printPreviewBarItem16.Command = DevExpress.XtraPrinting.PrintingSystemCommand.MultiplePages;
			this.printPreviewBarItem16.DropDownControl = this.multiplePagesControlContainer1;
			this.printPreviewBarItem16.Enabled = false;
			this.printPreviewBarItem16.Hint = "Multiple Pages";
			this.printPreviewBarItem16.Id = 19;
			this.printPreviewBarItem16.ImageIndex = 11;
			this.printPreviewBarItem16.Name = "printPreviewBarItem16";
			// 
			// multiplePagesControlContainer1
			// 
			this.multiplePagesControlContainer1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.multiplePagesControlContainer1.Location = new System.Drawing.Point(17, 74);
			this.multiplePagesControlContainer1.Manager = this.C_PrintBarManager;
			this.multiplePagesControlContainer1.Name = "multiplePagesControlContainer1";
			this.multiplePagesControlContainer1.Size = new System.Drawing.Size(0, 0);
			this.multiplePagesControlContainer1.TabIndex = 0;
			this.multiplePagesControlContainer1.Visible = false;
			// 
			// printPreviewBarItem17
			// 
//			this.printPreviewBarItem17.ActAsDropDown = true;
//			this.printPreviewBarItem17.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
			this.printPreviewBarItem17.Caption = "&Color...";
			this.printPreviewBarItem17.Command = DevExpress.XtraPrinting.PrintingSystemCommand.FillBackground;
//			this.printPreviewBarItem17.DropDownControl = this.colorPopupControlContainer1;
			this.printPreviewBarItem17.Enabled = false;
			this.printPreviewBarItem17.Hint = "Background";
			this.printPreviewBarItem17.Id = 20;
			this.printPreviewBarItem17.ImageIndex = 12;
			this.printPreviewBarItem17.Name = "printPreviewBarItem17";
//			// 
//			// colorPopupControlContainer1
//			// 
//			this.colorPopupControlContainer1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
//			this.colorPopupControlContainer1.ColorRectangle = new System.Drawing.Rectangle(7, 7, 8, 8);
//			this.colorPopupControlContainer1.DrawColorRectangle = false;
//			this.colorPopupControlContainer1.Item = this.printPreviewBarItem17;
//			this.colorPopupControlContainer1.Location = new System.Drawing.Point(254, 37);
//			this.colorPopupControlContainer1.Manager = this.C_PrintBarManager;
//			this.colorPopupControlContainer1.Name = "colorPopupControlContainer1";
//			this.colorPopupControlContainer1.ResultColor = System.Drawing.Color.Empty;
//			this.colorPopupControlContainer1.Size = new System.Drawing.Size(188, 223);
//			this.colorPopupControlContainer1.TabIndex = 0;
//			this.colorPopupControlContainer1.Visible = false;
			// 
			// printPreviewBarItem18
			// 
			this.printPreviewBarItem18.Caption = "&Watermark...";
			this.printPreviewBarItem18.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Watermark;
			this.printPreviewBarItem18.Enabled = false;
			this.printPreviewBarItem18.Hint = "Watermark";
			this.printPreviewBarItem18.Id = 21;
			this.printPreviewBarItem18.ImageIndex = 21;
			this.printPreviewBarItem18.Name = "printPreviewBarItem18";
			// 
			// printPreviewBarItem19
			// 
			this.printPreviewBarItem19.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
			this.printPreviewBarItem19.Caption = "Export Document...";
			this.printPreviewBarItem19.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportFile;
			this.printPreviewBarItem19.Enabled = false;
			this.printPreviewBarItem19.Hint = "Export Document...";
			this.printPreviewBarItem19.Id = 22;
			this.printPreviewBarItem19.ImageIndex = 18;
			this.printPreviewBarItem19.Name = "printPreviewBarItem19";
			// 
			// printPreviewBarItem0
			// 
			this.printPreviewBarItem0.Caption = "&About";
			this.printPreviewBarItem0.Command = DevExpress.XtraPrinting.PrintingSystemCommand.About;
			this.printPreviewBarItem0.Enabled = false;
			this.printPreviewBarItem0.Id = 101;
			this.printPreviewBarItem0.ImageIndex = 22;
			this.printPreviewBarItem0.Name = "printPreviewBarItem0";
			this.printPreviewBarItem0.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.About_ItemClick);
			// 
			// previewBar2
			// 
			this.previewBar2.BarName = "Status Bar";
			this.previewBar2.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
			this.previewBar2.DockCol = 0;
			this.previewBar2.DockRow = 0;
			this.previewBar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
			this.previewBar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
																									 new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewStaticItem1),
																									 new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewStaticItem2),
																									 new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewStaticItem3)});
			this.previewBar2.OptionsBar.AllowQuickCustomization = false;
			this.previewBar2.OptionsBar.DrawDragBorder = false;
			this.previewBar2.OptionsBar.UseWholeRow = true;
			this.previewBar2.Text = "Status Bar";
			// 
			// printPreviewStaticItem1
			// 
			this.printPreviewStaticItem1.AutoSize = DevExpress.XtraBars.BarStaticItemSize.Spring;
			this.printPreviewStaticItem1.Caption = "Current Page No: none";
			this.printPreviewStaticItem1.Id = 0;
			this.printPreviewStaticItem1.LeftIndent = 1;
			this.printPreviewStaticItem1.Name = "printPreviewStaticItem1";
			this.printPreviewStaticItem1.RightIndent = 1;
			this.printPreviewStaticItem1.TextAlignment = System.Drawing.StringAlignment.Near;
			this.printPreviewStaticItem1.Type = "CurrentPageNo";
			this.printPreviewStaticItem1.Width = 200;
			// 
			// printPreviewStaticItem2
			// 
			this.printPreviewStaticItem2.AutoSize = DevExpress.XtraBars.BarStaticItemSize.Spring;
			this.printPreviewStaticItem2.Caption = "Total Page No: 0";
			this.printPreviewStaticItem2.Id = 1;
			this.printPreviewStaticItem2.LeftIndent = 1;
			this.printPreviewStaticItem2.Name = "printPreviewStaticItem2";
			this.printPreviewStaticItem2.RightIndent = 1;
			this.printPreviewStaticItem2.TextAlignment = System.Drawing.StringAlignment.Near;
			this.printPreviewStaticItem2.Type = "TotalPageNo";
			this.printPreviewStaticItem2.Width = 200;
			// 
			// printPreviewStaticItem3
			// 
			this.printPreviewStaticItem3.AutoSize = DevExpress.XtraBars.BarStaticItemSize.Spring;
			this.printPreviewStaticItem3.Caption = "Zoom Factor: 100%";
			this.printPreviewStaticItem3.Id = 2;
			this.printPreviewStaticItem3.LeftIndent = 1;
			this.printPreviewStaticItem3.Name = "printPreviewStaticItem3";
			this.printPreviewStaticItem3.RightIndent = 1;
			this.printPreviewStaticItem3.TextAlignment = System.Drawing.StringAlignment.Near;
			this.printPreviewStaticItem3.Type = "ZoomFactor";
			this.printPreviewStaticItem3.Width = 200;
			// 
			// previewBar3
			// 
			this.previewBar3.BarName = "Main Menu";
			this.previewBar3.DockCol = 0;
			this.previewBar3.DockRow = 0;
			this.previewBar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
			this.previewBar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
																									 new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewSubItem1),
																									 new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewSubItem2),
																									 new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewSubItem3),
																									 new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewSubItem0)});
			this.previewBar3.OptionsBar.MultiLine = true;
			this.previewBar3.OptionsBar.UseWholeRow = true;
			this.previewBar3.Text = "Main Menu";
			this.previewBar3.Visible = false;
			// 
			// printPreviewSubItem1
			// 
			this.printPreviewSubItem1.Caption = "&File";
			this.printPreviewSubItem1.Command = DevExpress.XtraPrinting.PrintingSystemCommand.File;
			this.printPreviewSubItem1.Id = 25;
			this.printPreviewSubItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
																											  new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewBarItem6),
																											  new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewBarItem4),
																											  new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewBarItem5),
																											  new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewBarItem20, true)});
			this.printPreviewSubItem1.Name = "printPreviewSubItem1";
			// 
			// printPreviewBarItem20
			// 
			this.printPreviewBarItem20.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
			this.printPreviewBarItem20.Caption = "Send E-mail...";
			this.printPreviewBarItem20.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendFile;
			this.printPreviewBarItem20.Enabled = false;
			this.printPreviewBarItem20.Hint = "Send E-mail...";
			this.printPreviewBarItem20.Id = 23;
			this.printPreviewBarItem20.ImageIndex = 17;
			this.printPreviewBarItem20.Name = "printPreviewBarItem20";
			// 
			// printPreviewSubItem2
			// 
			this.printPreviewSubItem2.Caption = "&View";
			this.printPreviewSubItem2.Command = DevExpress.XtraPrinting.PrintingSystemCommand.View;
			this.printPreviewSubItem2.Id = 26;
			this.printPreviewSubItem2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
																											  new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewSubItem4, true),
																											  new DevExpress.XtraBars.LinkPersistInfo(this.barToolbarsListItem1, true)});
			this.printPreviewSubItem2.Name = "printPreviewSubItem2";
			// 
			// printPreviewSubItem4
			// 
			this.printPreviewSubItem4.Caption = "&Page Layout";
			this.printPreviewSubItem4.Command = DevExpress.XtraPrinting.PrintingSystemCommand.PageLayout;
			this.printPreviewSubItem4.Id = 28;
			this.printPreviewSubItem4.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
																											  new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewBarItem22),
																											  new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewBarItem23)});
			this.printPreviewSubItem4.Name = "printPreviewSubItem4";
			// 
			// printPreviewBarItem22
			// 
			this.printPreviewBarItem22.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
			this.printPreviewBarItem22.Caption = "&Facing";
			this.printPreviewBarItem22.Command = DevExpress.XtraPrinting.PrintingSystemCommand.PageLayoutFacing;
			this.printPreviewBarItem22.Enabled = false;
			this.printPreviewBarItem22.GroupIndex = 100;
			this.printPreviewBarItem22.Id = 29;
			this.printPreviewBarItem22.Name = "printPreviewBarItem22";
			// 
			// printPreviewBarItem23
			// 
			this.printPreviewBarItem23.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
			this.printPreviewBarItem23.Caption = "&Continuous";
			this.printPreviewBarItem23.Command = DevExpress.XtraPrinting.PrintingSystemCommand.PageLayoutContinuous;
			this.printPreviewBarItem23.Enabled = false;
			this.printPreviewBarItem23.GroupIndex = 100;
			this.printPreviewBarItem23.Id = 30;
			this.printPreviewBarItem23.Name = "printPreviewBarItem23";
			// 
			// barToolbarsListItem1
			// 
			this.barToolbarsListItem1.Caption = "Bars";
			this.barToolbarsListItem1.Id = 31;
			this.barToolbarsListItem1.Name = "barToolbarsListItem1";
			// 
			// printPreviewSubItem3
			// 
			this.printPreviewSubItem3.Caption = "&Background";
			this.printPreviewSubItem3.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Background;
			this.printPreviewSubItem3.Id = 27;
			this.printPreviewSubItem3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
																											  new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewBarItem17),
																											  new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewBarItem18)});
			this.printPreviewSubItem3.Name = "printPreviewSubItem3";
			// 
			// printPreviewSubItem0
			// 
			this.printPreviewSubItem0.Caption = "&Help";
			this.printPreviewSubItem0.Id = 100;
			this.printPreviewSubItem0.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
																											  new DevExpress.XtraBars.LinkPersistInfo(this.printPreviewBarItem0)});
			this.printPreviewSubItem0.Name = "printPreviewSubItem0";
			// 
			// printPreviewBarItem1
			// 
			this.printPreviewBarItem1.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
			this.printPreviewBarItem1.Caption = "Document Map";
			this.printPreviewBarItem1.Command = DevExpress.XtraPrinting.PrintingSystemCommand.DocumentMap;
			this.printPreviewBarItem1.Enabled = false;
			this.printPreviewBarItem1.Hint = "Document Map";
			this.printPreviewBarItem1.Id = 3;
			this.printPreviewBarItem1.ImageIndex = 19;
			this.printPreviewBarItem1.Name = "printPreviewBarItem1";
			this.printPreviewBarItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
			// 
			// printPreviewBarItem2
			// 
			this.printPreviewBarItem2.Caption = "Search";
			this.printPreviewBarItem2.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Find;
			this.printPreviewBarItem2.Enabled = false;
			this.printPreviewBarItem2.Hint = "Search";
			this.printPreviewBarItem2.Id = 4;
			this.printPreviewBarItem2.ImageIndex = 20;
			this.printPreviewBarItem2.Name = "printPreviewBarItem2";
			// 
			// printPreviewBarItem3
			// 
			this.printPreviewBarItem3.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
			this.printPreviewBarItem3.Caption = "Customize";
			this.printPreviewBarItem3.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Customize;
			this.printPreviewBarItem3.Enabled = false;
			this.printPreviewBarItem3.Hint = "Customize";
			this.printPreviewBarItem3.Id = 5;
			this.printPreviewBarItem3.ImageIndex = 14;
			this.printPreviewBarItem3.Name = "printPreviewBarItem3";
			// 
			// printPreviewBarItem21
			// 
			this.printPreviewBarItem21.Caption = "E&xit";
			this.printPreviewBarItem21.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ClosePreview;
			this.printPreviewBarItem21.Enabled = false;
			this.printPreviewBarItem21.Hint = "Close Preview";
			this.printPreviewBarItem21.Id = 24;
			this.printPreviewBarItem21.ImageIndex = 13;
			this.printPreviewBarItem21.Name = "printPreviewBarItem21";
			// 
			// printPreviewBarCheckItem1
			// 
			this.printPreviewBarCheckItem1.Caption = "PDF Document";
			this.printPreviewBarCheckItem1.Checked = true;
			this.printPreviewBarCheckItem1.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportPdf;
			this.printPreviewBarCheckItem1.Enabled = false;
			this.printPreviewBarCheckItem1.GroupIndex = 1;
			this.printPreviewBarCheckItem1.Hint = "PDF Document";
			this.printPreviewBarCheckItem1.Id = 32;
			this.printPreviewBarCheckItem1.Name = "printPreviewBarCheckItem1";
			// 
			// printPreviewBarCheckItem2
			// 
			this.printPreviewBarCheckItem2.Caption = "HTML Document";
			this.printPreviewBarCheckItem2.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportHtm;
			this.printPreviewBarCheckItem2.Enabled = false;
			this.printPreviewBarCheckItem2.GroupIndex = 1;
			this.printPreviewBarCheckItem2.Hint = "HTML Document";
			this.printPreviewBarCheckItem2.Id = 33;
			this.printPreviewBarCheckItem2.Name = "printPreviewBarCheckItem2";
			// 
			// printPreviewBarCheckItem3
			// 
			this.printPreviewBarCheckItem3.Caption = "Text Document";
			this.printPreviewBarCheckItem3.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportTxt;
			this.printPreviewBarCheckItem3.Enabled = false;
			this.printPreviewBarCheckItem3.GroupIndex = 1;
			this.printPreviewBarCheckItem3.Hint = "Text Document";
			this.printPreviewBarCheckItem3.Id = 34;
			this.printPreviewBarCheckItem3.Name = "printPreviewBarCheckItem3";
			// 
			// printPreviewBarCheckItem4
			// 
			this.printPreviewBarCheckItem4.Caption = "CSV Document";
			this.printPreviewBarCheckItem4.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportCsv;
			this.printPreviewBarCheckItem4.Enabled = false;
			this.printPreviewBarCheckItem4.GroupIndex = 1;
			this.printPreviewBarCheckItem4.Hint = "CSV Document";
			this.printPreviewBarCheckItem4.Id = 35;
			this.printPreviewBarCheckItem4.Name = "printPreviewBarCheckItem4";
			// 
			// printPreviewBarCheckItem6
			// 
			this.printPreviewBarCheckItem6.Caption = "Excel Document";
			this.printPreviewBarCheckItem6.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportXls;
			this.printPreviewBarCheckItem6.Enabled = false;
			this.printPreviewBarCheckItem6.GroupIndex = 1;
			this.printPreviewBarCheckItem6.Hint = "Excel Document";
			this.printPreviewBarCheckItem6.Id = 37;
			this.printPreviewBarCheckItem6.Name = "printPreviewBarCheckItem6";
			// 
			// printPreviewBarCheckItem7
			// 
			this.printPreviewBarCheckItem7.Caption = "Rich Text Document";
			this.printPreviewBarCheckItem7.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportRtf;
			this.printPreviewBarCheckItem7.Enabled = false;
			this.printPreviewBarCheckItem7.GroupIndex = 1;
			this.printPreviewBarCheckItem7.Hint = "Rich Text Document";
			this.printPreviewBarCheckItem7.Id = 38;
			this.printPreviewBarCheckItem7.Name = "printPreviewBarCheckItem7";
			// 
			// printPreviewBarCheckItem8
			// 
			this.printPreviewBarCheckItem8.Caption = "Graphic Document";
			this.printPreviewBarCheckItem8.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportGraphic;
			this.printPreviewBarCheckItem8.Enabled = false;
			this.printPreviewBarCheckItem8.GroupIndex = 1;
			this.printPreviewBarCheckItem8.Hint = "Graphic Document";
			this.printPreviewBarCheckItem8.Id = 39;
			this.printPreviewBarCheckItem8.Name = "printPreviewBarCheckItem8";
			// 
			// printPreviewBarCheckItem9
			// 
			this.printPreviewBarCheckItem9.Caption = "PDF Document";
			this.printPreviewBarCheckItem9.Checked = true;
			this.printPreviewBarCheckItem9.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendPdf;
			this.printPreviewBarCheckItem9.Enabled = false;
			this.printPreviewBarCheckItem9.GroupIndex = 2;
			this.printPreviewBarCheckItem9.Hint = "PDF Document";
			this.printPreviewBarCheckItem9.Id = 40;
			this.printPreviewBarCheckItem9.Name = "printPreviewBarCheckItem9";
			// 
			// printPreviewBarCheckItem10
			// 
			this.printPreviewBarCheckItem10.Caption = "Text Document";
			this.printPreviewBarCheckItem10.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendTxt;
			this.printPreviewBarCheckItem10.Enabled = false;
			this.printPreviewBarCheckItem10.GroupIndex = 2;
			this.printPreviewBarCheckItem10.Hint = "Text Document";
			this.printPreviewBarCheckItem10.Id = 41;
			this.printPreviewBarCheckItem10.Name = "printPreviewBarCheckItem10";
			// 
			// printPreviewBarCheckItem11
			// 
			this.printPreviewBarCheckItem11.Caption = "CSV Document";
			this.printPreviewBarCheckItem11.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendCsv;
			this.printPreviewBarCheckItem11.Enabled = false;
			this.printPreviewBarCheckItem11.GroupIndex = 2;
			this.printPreviewBarCheckItem11.Hint = "CSV Document";
			this.printPreviewBarCheckItem11.Id = 42;
			this.printPreviewBarCheckItem11.Name = "printPreviewBarCheckItem11";
			// 
			// printPreviewBarCheckItem13
			// 
			this.printPreviewBarCheckItem13.Caption = "Excel Document";
			this.printPreviewBarCheckItem13.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendXls;
			this.printPreviewBarCheckItem13.Enabled = false;
			this.printPreviewBarCheckItem13.GroupIndex = 2;
			this.printPreviewBarCheckItem13.Hint = "Excel Document";
			this.printPreviewBarCheckItem13.Id = 44;
			this.printPreviewBarCheckItem13.Name = "printPreviewBarCheckItem13";
			// 
			// printPreviewBarCheckItem15
			// 
			this.printPreviewBarCheckItem15.Caption = "Graphic Document";
			this.printPreviewBarCheckItem15.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendGraphic;
			this.printPreviewBarCheckItem15.Enabled = false;
			this.printPreviewBarCheckItem15.GroupIndex = 2;
			this.printPreviewBarCheckItem15.Hint = "Graphic Document";
			this.printPreviewBarCheckItem15.Id = 46;
			this.printPreviewBarCheckItem15.Name = "printPreviewBarCheckItem15";
			// 
			// barButtonItem1
			// 
			this.barButtonItem1.Caption = "barButtonItem1";
			this.barButtonItem1.Id = 47;
			this.barButtonItem1.Name = "barButtonItem1";
			// 
			// C_PrintControl
			// 
			this.C_PrintControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.C_PrintControl.IsMetric = false;
			this.C_PrintControl.Location = new System.Drawing.Point(0, 46);
			this.C_PrintControl.Name = "C_PrintControl";
			this.C_PrintControl.Size = new System.Drawing.Size(685, 337);
			this.C_PrintControl.TabIndex = 4;
			// 
			// C_DefaultLookAndFeel
			// 
			this.C_DefaultLookAndFeel.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003;
			// 
			// ReportBrowserBase
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(685, 402);
			this.Controls.Add(this.C_PrintControl);
			this.Controls.Add(this.barDockControlLeft);
			this.Controls.Add(this.barDockControlRight);
			this.Controls.Add(this.barDockControlBottom);
			this.Controls.Add(this.barDockControlTop);
			this.Name = "ReportBrowserBase";
			this.Text = "ReportBrowserBase";
			((System.ComponentModel.ISupportInitialize)(this.C_PrintBarManager)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.printPreviewRepositoryItemComboBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.multiplePagesControlContainer1)).EndInit();
//			((System.ComponentModel.ISupportInitialize)(this.colorPopupControlContainer1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		public void LoadReport(WebbReport i_Report)
		{
			
			if(this.C_PrintControl.PrintingSystem==null) this.C_PrintControl.PrintingSystem = new DevExpress.XtraPrinting.PrintingSystem();
			this.C_PrintControl.PrintingSystem.ClearContent();
			this.Invalidate();
			this.Update();
			i_Report.PrintingSystem = this.C_PrintControl.PrintingSystem;
			i_Report.CreateDocument();			
		}

		public void LoadReport(WebbReport i_Report, bool i_ClearContent,bool i_InsertPageBreak)
		{
			if(this.C_PrintControl.PrintingSystem==null) this.C_PrintControl.PrintingSystem = new DevExpress.XtraPrinting.PrintingSystem();
			if(i_ClearContent)this.C_PrintControl.PrintingSystem.ClearContent();
			this.Invalidate();
			this.Update();
			if(i_InsertPageBreak) this.C_PrintControl.PrintingSystem.InsertPageBreak(500);
			i_Report.PrintingSystem = this.C_PrintControl.PrintingSystem;
			i_Report.CreateDocument();
		}

		public void LoadReport(WebbReport[] i_Reports)
		{
			if(this.C_PrintControl.PrintingSystem==null) this.C_PrintControl.PrintingSystem = new DevExpress.XtraPrinting.PrintingSystem();
			this.C_PrintControl.PrintingSystem.ClearContent();
			this.Invalidate();
			this.Update();
			foreach(WebbReport m_rep in i_Reports)
			{
				//this.C_PrintControl.PrintingSystem.BeginSubreport(new PointF(0,0));
				m_rep.PrintingSystem = this.C_PrintControl.PrintingSystem;
				m_rep.CreateDocument();
				this.C_PrintControl.PrintingSystem.EndSubreport();
			}
		}

		private void About_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			using(Webb.Utilities.ProductAboutForm aboutForm = new Webb.Utilities.ProductAboutForm())
			{
				aboutForm.ShowDialog(this);
			}
			
		}

		//05-04-2008@Scott
		public DevExpress.XtraPrinting.Brick FindBrick(Point sreenPoint)
		{
			return this.C_PrintControl.FindBrick(sreenPoint);
		}

		private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			Application.Exit();
		}
		
	}
}
