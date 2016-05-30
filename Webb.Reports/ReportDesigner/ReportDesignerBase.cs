/***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:XtraForm1.cs
 * Author:Country.Wu [EMail:Webb.Country.Wu@163.com]
 * Create Time:11/20/2007 10:34:09 AM
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
using DevExpress.XtraEditors;
using DevExpress.XtraReports;
using System.Drawing.Design;
using System.ComponentModel.Design;
using System.Xml;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;

using DevExpress.XtraReports.Design;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.UserDesigner;
using Webb.Data;

namespace Webb.Reports.ReportDesigner
{
    /// <summary>
    /// Summary description for XtraForm1.
    /// </summary>
    public class ReportDesignerBase : DevExpress.XtraEditors.XtraForm
    {
        private Process BrowserProcess;

        private bool CanDebugWithDataFile = true;

        #region Components

        private Webb.Reports.DataProvider.WebbDataProvider DataProvider;
        protected Webb.Reports.WebbDataSource _DataSource;
        //private System.Data.IDataAdapter _DataAdapter;
        //	
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraReports.UserDesigner.RecentlyUsedItemsComboBox recentlyUsedItemsComboBox1;
        private DevExpress.XtraReports.UserDesigner.DesignRepositoryItemComboBox designRepositoryItemComboBox1;
        private DevExpress.XtraReports.UserDesigner.DesignRepositoryItemComboBox designRepositoryItemComboBox2;
        private DevExpress.XtraReports.UserDesigner.DesignBar designBar5;
        private DevExpress.XtraBars.BarEditItem barEditItem1;
        private DevExpress.XtraBars.BarEditItem barEditItem2;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem0;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem1;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem2;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem3;
        private DevExpress.XtraReports.UserDesigner.CommandColorBarItem commandColorBarItem1;
        private DevExpress.XtraReports.UserDesigner.CommandColorBarItem commandColorBarItem2;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem4;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem5;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem6;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem7;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem8;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem9;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem10;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem11;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem12;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem13;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem14;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem15;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem16;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem17;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem18;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem19;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem20;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem21;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem22;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem23;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem24;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem25;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem26;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem27;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem28;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem29;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem30;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem31;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem32;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem33;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem34;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem35;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem36;
        private DevExpress.XtraBars.BarSubItem barSubItem0;
        private DevExpress.XtraBars.BarSubItem barSubItem1;
        private DevExpress.XtraBars.BarSubItem barSubItem2;
        private DevExpress.XtraBars.BarSubItem barSubItem3;
        private DevExpress.XtraBars.BarSubItem barSubItemDebug;
        private DevExpress.XtraReports.UserDesigner.BarReportTabButtonsListItem barReportTabButtonsListItem1;
        private DevExpress.XtraBars.BarSubItem barSubItem4;
        private DevExpress.XtraReports.UserDesigner.XRBarToolbarsListItem xrBarToolbarsListItem1;
        private DevExpress.XtraBars.BarSubItem barSubItem5;
        private DevExpress.XtraReports.UserDesigner.BarDockPanelsListItem barDockPanelsListItem1;
        private DevExpress.XtraBars.BarSubItem barSubItem6;
        private DevExpress.XtraBars.BarSubItem barSubItem7;
        private DevExpress.XtraBars.BarSubItem barSubItem8;
        private DevExpress.XtraBars.BarSubItem barSubItem9;
        private DevExpress.XtraBars.BarSubItem barSubItem10;
        private DevExpress.XtraBars.BarSubItem barSubItem11;
        private DevExpress.XtraBars.BarSubItem barSubItem12;
        private DevExpress.XtraBars.BarSubItem barSubItem13;
        private DevExpress.XtraBars.BarSubItem barSubItem14;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem39;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem40;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem41;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem42;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem43;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem44;
        private DevExpress.XtraReports.UserDesigner.XRZoomBarEditItem xrZoomBarEditItem1;
        private DevExpress.XtraReports.UserDesigner.CommandBarItem commandBarItem45;
        private DevExpress.XtraReports.UserDesigner.DesignControlContainer fieldListDockPanel1_Container;
        private DevExpress.XtraReports.UserDesigner.DesignControlContainer propertyGridDockPanel1_Container;
        private DevExpress.XtraReports.UserDesigner.DesignControlContainer reportExplorerDockPanel1_Container;
        private DevExpress.XtraReports.UserDesigner.DesignControlContainer toolBoxDockPanel1_Container;
        private DevExpress.XtraReports.UserDesigner.XRDesignBarManager C_DesignBarManager;
        private DevExpress.XtraReports.UserDesigner.XRDesignDockManager C_DesignDockManager;
        private DevExpress.XtraReports.UserDesigner.DesignBar C_MainMenu;
        private DevExpress.XtraReports.UserDesigner.DesignBar C_MainToolbar;
        private DevExpress.XtraReports.UserDesigner.DesignBar C_FormattingToolbar;
        private DevExpress.XtraReports.UserDesigner.DesignBar C_LayoutToolbar;
        private DevExpress.XtraReports.UserDesigner.DesignBar C_ZoomBar;
        private DevExpress.XtraReports.UserDesigner.FieldListDockPanel C_FieldListDockPanel;
        private DevExpress.XtraReports.UserDesigner.PropertyGridDockPanel C_PropertyGridDockPanel;
        private DevExpress.XtraReports.UserDesigner.ReportExplorerDockPanel C_ReportExplorerDockPanel;
        private DevExpress.XtraReports.UserDesigner.ToolBoxDockPanel C_ToolBoxDockPanel;
        private DevExpress.XtraBars.BarButtonItem C_OpenDataSource;
        private DevExpress.XtraBars.BarButtonItem C_RemoveDataSource;
        private DevExpress.XtraBars.BarButtonItem C_PreviewDataSource;
        private DevExpress.XtraBars.BarButtonItem C_LoadDataConfig;        //Added this code at 2009-2-4 13:04:19@Simon
        private DevExpress.XtraBars.BarButtonItem C_SaveDataConfig;       //Added this code at 2009-2-4 13:04:23@Simon
        protected DevExpress.XtraReports.UserDesigner.XRDesignPanel C_ReportDesignPanel;
        private DevExpress.XtraBars.BarStaticItem C_StatusBar;
        private DevExpress.XtraBars.Docking.DockPanel C_FilterPanel;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraBars.Docking.DockPanel panelContainer3;
        private System.Windows.Forms.TreeView C_FilterList;
        private DevExpress.XtraBars.PopupMenu C_FiledListMenu;
        private DevExpress.XtraBars.PopupMenu C_FilterListMenu;
        private DevExpress.XtraBars.BarButtonItem C_LoadSectionFilters;
        private DevExpress.XtraBars.BarButtonItem C_RemoveFilters;
        private DevExpress.XtraBars.BarButtonItem C_EditFilters;  //2009-8-21 12:33:29@Simon Add this Code
        private DevExpress.XtraBars.BarButtonItem C_ClearAllFilters;
        private DevExpress.XtraBars.BarSubItem barSubItem15;
        private DevExpress.XtraBars.BarButtonItem C_LoadCustomSectionFilters;
        private DevExpress.XtraBars.BarButtonItem C_LoadAdvReportFilters;
        private DevExpress.XtraBars.BarEditItem barEditItem3;
        private DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup repositoryItemRadioGroup1;
        private DevExpress.XtraBars.BarCheckItem C_CheckZoneO;
        private DevExpress.XtraBars.BarCheckItem C_CheckZoneD;
        private DevExpress.XtraBars.BarCheckItem C_CheckDDO;
        private DevExpress.XtraBars.BarCheckItem C_CheckDDD;
        private DevExpress.XtraBars.BarToolbarsListItem barToolbarsListItem1;
        private DevExpress.XtraBars.BarSubItem barSubItem16;
        private DevExpress.XtraBars.BarCheckItem C_CheckCustom;
        private DevExpress.XtraBars.BarButtonItem barButtonRun;
        private DevExpress.XtraBars.BarButtonItem barButtonStop;

        private System.ComponentModel.IContainer components;
        #endregion

        public ReportDesignerBase()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            BrowserProcess = new Process();
            BrowserProcess.EnableRaisingEvents = true;
            BrowserProcess.Exited += new EventHandler(BrowserProcess_Exited);


            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            this.InitializeDesigner();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (this.DataProvider != null)
            {
                this.DataProvider.Dispose();
                this.DataProvider = null;
            }
            if (this.BrowserProcess != null)  //Added this code at 2009-2-9 10:55:00@Simon
            {
                try
                {
                    if (!this.BrowserProcess.HasExited)
                    {
                        BrowserProcess.Kill();
                    }
                }
                catch { }
                BrowserProcess.Dispose();
                BrowserProcess = null;
            }
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportDesignerBase));
            this.C_DesignBarManager = new DevExpress.XtraReports.UserDesigner.XRDesignBarManager();
            this.C_MainMenu = new DevExpress.XtraReports.UserDesigner.DesignBar();
            this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
            this.commandBarItem31 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
            this.commandBarItem39 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
            this.commandBarItem32 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
            this.commandBarItem33 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
            this.commandBarItem40 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
            this.commandBarItem41 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
            this.barSubItem2 = new DevExpress.XtraBars.BarSubItem();
            this.commandBarItem34 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
            this.commandBarItem35 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
            this.commandBarItem36 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
            this.commandBarItem42 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
            this.commandBarItem43 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
            this.barSubItem3 = new DevExpress.XtraBars.BarSubItem();
            this.barReportTabButtonsListItem1 = new DevExpress.XtraReports.UserDesigner.BarReportTabButtonsListItem();
            this.barSubItem4 = new DevExpress.XtraBars.BarSubItem();
            this.xrBarToolbarsListItem1 = new DevExpress.XtraReports.UserDesigner.XRBarToolbarsListItem();
            this.barSubItem5 = new DevExpress.XtraBars.BarSubItem();
            this.barDockPanelsListItem1 = new DevExpress.XtraReports.UserDesigner.BarDockPanelsListItem();
            this.barSubItem6 = new DevExpress.XtraBars.BarSubItem();
            this.commandColorBarItem1 = new DevExpress.XtraReports.UserDesigner.CommandColorBarItem();
            this.commandColorBarItem2 = new DevExpress.XtraReports.UserDesigner.CommandColorBarItem();
            this.barSubItem7 = new DevExpress.XtraBars.BarSubItem();
            this.commandBarItem1 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
            this.commandBarItem2 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
            this.commandBarItem3 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
            this.barSubItem8 = new DevExpress.XtraBars.BarSubItem();
            this.commandBarItem4 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
            this.commandBarItem5 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
            this.commandBarItem6 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
            this.commandBarItem7 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
            this.barSubItem9 = new DevExpress.XtraBars.BarSubItem();
            this.commandBarItem9 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
            this.commandBarItem10 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
            this.commandBarItem11 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
            this.commandBarItem12 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
            this.commandBarItem13 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
            this.commandBarItem14 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
            this.commandBarItem8 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
            this.barSubItem10 = new DevExpress.XtraBars.BarSubItem();
            this.commandBarItem15 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
            this.commandBarItem16 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
            this.commandBarItem17 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
            this.commandBarItem18 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
            this.barSubItem11 = new DevExpress.XtraBars.BarSubItem();
            this.commandBarItem19 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
            this.commandBarItem20 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
            this.commandBarItem21 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
            this.commandBarItem22 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
            this.barSubItem12 = new DevExpress.XtraBars.BarSubItem();
            this.commandBarItem23 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
            this.commandBarItem24 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
            this.commandBarItem25 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
            this.commandBarItem26 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
            this.barSubItem13 = new DevExpress.XtraBars.BarSubItem();
            this.commandBarItem27 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
            this.commandBarItem28 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
            this.barSubItem14 = new DevExpress.XtraBars.BarSubItem();
            this.commandBarItem29 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
            this.commandBarItem30 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
            this.barSubItemDebug = new DevExpress.XtraBars.BarSubItem();
            this.barButtonRun = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonStop = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem0 = new DevExpress.XtraBars.BarSubItem();
            this.commandBarItem0 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
            this.C_MainToolbar = new DevExpress.XtraReports.UserDesigner.DesignBar();
            this.C_FormattingToolbar = new DevExpress.XtraReports.UserDesigner.DesignBar();
            this.barEditItem1 = new DevExpress.XtraBars.BarEditItem();
            this.recentlyUsedItemsComboBox1 = new DevExpress.XtraReports.UserDesigner.RecentlyUsedItemsComboBox();
            this.barEditItem2 = new DevExpress.XtraBars.BarEditItem();
            this.designRepositoryItemComboBox1 = new DevExpress.XtraReports.UserDesigner.DesignRepositoryItemComboBox();
            this.C_LayoutToolbar = new DevExpress.XtraReports.UserDesigner.DesignBar();
            this.designBar5 = new DevExpress.XtraReports.UserDesigner.DesignBar();
            this.C_StatusBar = new DevExpress.XtraBars.BarStaticItem();
            this.C_ZoomBar = new DevExpress.XtraReports.UserDesigner.DesignBar();
            this.commandBarItem44 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
            this.xrZoomBarEditItem1 = new DevExpress.XtraReports.UserDesigner.XRZoomBarEditItem();
            this.designRepositoryItemComboBox2 = new DevExpress.XtraReports.UserDesigner.DesignRepositoryItemComboBox();
            this.commandBarItem45 = new DevExpress.XtraReports.UserDesigner.CommandBarItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.C_DesignDockManager = new DevExpress.XtraReports.UserDesigner.XRDesignDockManager();
            this.panelContainer3 = new DevExpress.XtraBars.Docking.DockPanel();
            this.C_PropertyGridDockPanel = new DevExpress.XtraReports.UserDesigner.PropertyGridDockPanel();
            this.propertyGridDockPanel1_Container = new DevExpress.XtraReports.UserDesigner.DesignControlContainer();
            this.C_ReportDesignPanel = new DevExpress.XtraReports.UserDesigner.XRDesignPanel();
            this.C_FieldListDockPanel = new DevExpress.XtraReports.UserDesigner.FieldListDockPanel();
            this.fieldListDockPanel1_Container = new DevExpress.XtraReports.UserDesigner.DesignControlContainer();
            this.C_FilterPanel = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.C_FilterList = new System.Windows.Forms.TreeView();
            this.C_ReportExplorerDockPanel = new DevExpress.XtraReports.UserDesigner.ReportExplorerDockPanel();
            this.reportExplorerDockPanel1_Container = new DevExpress.XtraReports.UserDesigner.DesignControlContainer();
            this.C_ToolBoxDockPanel = new DevExpress.XtraReports.UserDesigner.ToolBoxDockPanel();
            this.toolBoxDockPanel1_Container = new DevExpress.XtraReports.UserDesigner.DesignControlContainer();
            this.C_OpenDataSource = new DevExpress.XtraBars.BarButtonItem();
            this.C_RemoveDataSource = new DevExpress.XtraBars.BarButtonItem();
            this.C_PreviewDataSource = new DevExpress.XtraBars.BarButtonItem();
            this.C_LoadSectionFilters = new DevExpress.XtraBars.BarButtonItem();
            this.C_EditFilters = new DevExpress.XtraBars.BarButtonItem();
            this.C_RemoveFilters = new DevExpress.XtraBars.BarButtonItem();
            this.C_ClearAllFilters = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem15 = new DevExpress.XtraBars.BarSubItem();
            this.C_LoadCustomSectionFilters = new DevExpress.XtraBars.BarButtonItem();
            this.C_LoadAdvReportFilters = new DevExpress.XtraBars.BarButtonItem();
            this.barEditItem3 = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemRadioGroup1 = new DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup();
            this.C_CheckZoneO = new DevExpress.XtraBars.BarCheckItem();
            this.C_CheckZoneD = new DevExpress.XtraBars.BarCheckItem();
            this.C_CheckDDO = new DevExpress.XtraBars.BarCheckItem();
            this.C_CheckDDD = new DevExpress.XtraBars.BarCheckItem();
            this.barToolbarsListItem1 = new DevExpress.XtraBars.BarToolbarsListItem();
            this.barSubItem16 = new DevExpress.XtraBars.BarSubItem();
            this.C_CheckCustom = new DevExpress.XtraBars.BarCheckItem();
            this.C_LoadDataConfig = new DevExpress.XtraBars.BarButtonItem();
            this.C_SaveDataConfig = new DevExpress.XtraBars.BarButtonItem();
            this.C_FiledListMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            this.C_FilterListMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.C_DesignBarManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.recentlyUsedItemsComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.designRepositoryItemComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.designRepositoryItemComboBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.C_DesignDockManager)).BeginInit();
            this.panelContainer3.SuspendLayout();
            this.C_PropertyGridDockPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.C_ReportDesignPanel)).BeginInit();
            this.C_FieldListDockPanel.SuspendLayout();
            this.C_FilterPanel.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            this.C_ReportExplorerDockPanel.SuspendLayout();
            this.C_ToolBoxDockPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRadioGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.C_FiledListMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.C_FilterListMenu)).BeginInit();
            this.SuspendLayout();
            // 
            // C_DesignBarManager
            // 
            this.C_DesignBarManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.C_MainMenu,
            this.C_MainToolbar,
            this.C_FormattingToolbar,
            this.C_LayoutToolbar,
            this.designBar5,
            this.C_ZoomBar});
            this.C_DesignBarManager.DockControls.Add(this.barDockControlTop);
            this.C_DesignBarManager.DockControls.Add(this.barDockControlBottom);
            this.C_DesignBarManager.DockControls.Add(this.barDockControlLeft);
            this.C_DesignBarManager.DockControls.Add(this.barDockControlRight);
            this.C_DesignBarManager.DockManager = this.C_DesignDockManager;
            this.C_DesignBarManager.FontNameBox = this.recentlyUsedItemsComboBox1;
            this.C_DesignBarManager.FontNameEdit = this.barEditItem1;
            this.C_DesignBarManager.FontSizeBox = this.designRepositoryItemComboBox1;
            this.C_DesignBarManager.FontSizeEdit = this.barEditItem2;
            this.C_DesignBarManager.Form = this;
            this.C_DesignBarManager.FormattingToolbar = this.C_FormattingToolbar;
            this.C_DesignBarManager.HintStaticItem = this.C_StatusBar;
            this.C_DesignBarManager.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("C_DesignBarManager.ImageStream")));
            this.C_DesignBarManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barEditItem1,
            this.barEditItem2,
            this.commandBarItem0,
            this.commandBarItem1,
            this.commandBarItem2,
            this.commandBarItem3,
            this.commandColorBarItem1,
            this.commandColorBarItem2,
            this.commandBarItem4,
            this.commandBarItem5,
            this.commandBarItem6,
            this.commandBarItem7,
            this.commandBarItem8,
            this.commandBarItem9,
            this.commandBarItem10,
            this.commandBarItem11,
            this.commandBarItem12,
            this.commandBarItem13,
            this.commandBarItem14,
            this.commandBarItem15,
            this.commandBarItem16,
            this.commandBarItem17,
            this.commandBarItem18,
            this.commandBarItem19,
            this.commandBarItem20,
            this.commandBarItem21,
            this.commandBarItem22,
            this.commandBarItem23,
            this.commandBarItem24,
            this.commandBarItem25,
            this.commandBarItem26,
            this.commandBarItem27,
            this.commandBarItem28,
            this.commandBarItem29,
            this.commandBarItem30,
            this.commandBarItem31,
            this.commandBarItem32,
            this.commandBarItem33,
            this.commandBarItem34,
            this.commandBarItem35,
            this.commandBarItem36,
            this.C_StatusBar,
            this.barSubItemDebug,
            this.barSubItem0,
            this.barSubItem1,
            this.barSubItem2,
            this.barSubItem3,
            this.barReportTabButtonsListItem1,
            this.barSubItem4,
            this.xrBarToolbarsListItem1,
            this.barSubItem5,
            this.barDockPanelsListItem1,
            this.barSubItem6,
            this.barSubItem7,
            this.barSubItem8,
            this.barSubItem9,
            this.barSubItem10,
            this.barSubItem11,
            this.barSubItem12,
            this.barSubItem13,
            this.barSubItem14,
            this.commandBarItem39,
            this.commandBarItem40,
            this.commandBarItem41,
            this.commandBarItem42,
            this.commandBarItem43,
            this.commandBarItem44,
            this.xrZoomBarEditItem1,
            this.commandBarItem45,
            this.C_OpenDataSource,
            this.C_RemoveDataSource,
            this.C_PreviewDataSource,
            this.C_LoadSectionFilters,
            this.C_EditFilters,
            this.C_RemoveFilters,
            this.C_ClearAllFilters,
            this.barSubItem15,
            this.C_LoadCustomSectionFilters,
            this.C_LoadAdvReportFilters,
            this.barEditItem3,
            this.C_CheckZoneO,
            this.C_CheckZoneD,
            this.C_CheckDDO,
            this.C_CheckDDD,
            this.barToolbarsListItem1,
            this.barSubItem16,
            this.C_CheckCustom,
            this.C_LoadDataConfig,
            this.C_SaveDataConfig,
            this.barButtonRun,
            this.barButtonStop});
            this.C_DesignBarManager.LayoutToolbar = this.C_LayoutToolbar;
            this.C_DesignBarManager.MainMenu = this.C_MainMenu;
            this.C_DesignBarManager.MaxItemId = 88;
            this.C_DesignBarManager.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.recentlyUsedItemsComboBox1,
            this.designRepositoryItemComboBox1,
            this.designRepositoryItemComboBox2,
            this.repositoryItemRadioGroup1});
            this.C_DesignBarManager.StatusBar = this.designBar5;
            this.C_DesignBarManager.Toolbar = this.C_MainToolbar;
            this.C_DesignBarManager.XRDesignPanel = this.C_ReportDesignPanel;
            this.C_DesignBarManager.ZoomItem = this.xrZoomBarEditItem1;
            // 
            // C_MainMenu
            // 
            this.C_MainMenu.BarName = "Main Menu";
            this.C_MainMenu.DockCol = 0;
            this.C_MainMenu.DockRow = 0;
            this.C_MainMenu.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.C_MainMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem2),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem3),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem6),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItemDebug),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem0)});
            this.C_MainMenu.OptionsBar.MultiLine = true;
            this.C_MainMenu.OptionsBar.UseWholeRow = true;
            this.C_MainMenu.Text = "Main Menu";
            // 
            // barSubItem1
            // 
            this.barSubItem1.Caption = "&File";
            this.barSubItem1.Id = 43;
            this.barSubItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem31),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem39),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem32),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem33, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem40),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem41, true)});
            this.barSubItem1.Name = "barSubItem1";
            // 
            // commandBarItem31
            // 
            this.commandBarItem31.Caption = "&New";
            this.commandBarItem31.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.NewReport;
            this.commandBarItem31.Hint = "Create a new blank report";
            this.commandBarItem31.Id = 34;
            this.commandBarItem31.ImageIndex = 9;
            this.commandBarItem31.Name = "commandBarItem31";
            this.commandBarItem31.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.NewReport_ItemClick);
            // 
            // commandBarItem39
            // 
            this.commandBarItem39.Caption = "New with &Wizard...";
            this.commandBarItem39.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.NewReportWizard;
            this.commandBarItem39.Hint = "Create a new report using the Wizard";
            this.commandBarItem39.Id = 60;
            this.commandBarItem39.Name = "commandBarItem39";
            this.commandBarItem39.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.NewReportWizard_ItemClick);
            // 
            // commandBarItem32
            // 
            this.commandBarItem32.Caption = "&Open...";
            this.commandBarItem32.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.OpenFile;
            this.commandBarItem32.Hint = "Open a report";
            this.commandBarItem32.Id = 35;
            this.commandBarItem32.ImageIndex = 10;
            this.commandBarItem32.Name = "commandBarItem32";
            this.commandBarItem32.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OpenReportItem_Click);
            // 
            // commandBarItem33
            // 
            this.commandBarItem33.Caption = "&Save";
            this.commandBarItem33.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.SaveFile;
            this.commandBarItem33.Enabled = false;
            this.commandBarItem33.Hint = "Save a report";
            this.commandBarItem33.Id = 36;
            this.commandBarItem33.ImageIndex = 11;
            this.commandBarItem33.Name = "commandBarItem33";
            this.commandBarItem33.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.Save_ItemClick);
            // 
            // commandBarItem40
            // 
            this.commandBarItem40.Caption = "Save &As...";
            this.commandBarItem40.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.SaveFileAs;
            this.commandBarItem40.Enabled = false;
            this.commandBarItem40.Hint = "Save a report with a new name";
            this.commandBarItem40.Id = 61;
            this.commandBarItem40.Name = "commandBarItem40";
            this.commandBarItem40.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.SaveAs_ItemClick);
            // 
            // commandBarItem41
            // 
            this.commandBarItem41.Caption = "E&xit";
            this.commandBarItem41.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.Exit;
            this.commandBarItem41.Hint = "Close the designer";
            this.commandBarItem41.Id = 62;
            this.commandBarItem41.Name = "commandBarItem41";
            // 
            // barSubItem2
            // 
            this.barSubItem2.Caption = "&Edit";
            this.barSubItem2.Id = 44;
            this.barSubItem2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem34, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem35),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem36),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem42),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem43, true)});
            this.barSubItem2.Name = "barSubItem2";
            // 
            // commandBarItem34
            // 
            this.commandBarItem34.Caption = "Cu&t";
            this.commandBarItem34.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.Cut;
            this.commandBarItem34.Enabled = false;
            this.commandBarItem34.Hint = "Delete the control and copy it to the clipboard";
            this.commandBarItem34.Id = 37;
            this.commandBarItem34.ImageIndex = 12;
            this.commandBarItem34.Name = "commandBarItem34";
            // 
            // commandBarItem35
            // 
            this.commandBarItem35.Caption = "&Copy";
            this.commandBarItem35.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.Copy;
            this.commandBarItem35.Enabled = false;
            this.commandBarItem35.Hint = "Copy the control to the clipboard";
            this.commandBarItem35.Id = 38;
            this.commandBarItem35.ImageIndex = 13;
            this.commandBarItem35.Name = "commandBarItem35";
            // 
            // commandBarItem36
            // 
            this.commandBarItem36.Caption = "&Paste";
            this.commandBarItem36.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.Paste;
            this.commandBarItem36.Enabled = false;
            this.commandBarItem36.Hint = "Add the control from the clipboard";
            this.commandBarItem36.Id = 39;
            this.commandBarItem36.ImageIndex = 14;
            this.commandBarItem36.Name = "commandBarItem36";
            // 
            // commandBarItem42
            // 
            this.commandBarItem42.Caption = "&Delete";
            this.commandBarItem42.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.Delete;
            this.commandBarItem42.Enabled = false;
            this.commandBarItem42.Hint = "Delete the control";
            this.commandBarItem42.Id = 63;
            this.commandBarItem42.Name = "commandBarItem42";
            // 
            // commandBarItem43
            // 
            this.commandBarItem43.Caption = "Select &All";
            this.commandBarItem43.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.SelectAll;
            this.commandBarItem43.Enabled = false;
            this.commandBarItem43.Hint = "Select all the controls in the document";
            this.commandBarItem43.Id = 64;
            this.commandBarItem43.Name = "commandBarItem43";
            // 
            // barSubItem3
            // 
            this.barSubItem3.Caption = "&View";
            this.barSubItem3.Id = 45;
            this.barSubItem3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barReportTabButtonsListItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem4, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem5, true)});
            this.barSubItem3.Name = "barSubItem3";
            // 
            // barReportTabButtonsListItem1
            // 
            this.barReportTabButtonsListItem1.Caption = "Tab Buttons";
            this.barReportTabButtonsListItem1.Id = 46;
            this.barReportTabButtonsListItem1.Name = "barReportTabButtonsListItem1";
            // 
            // barSubItem4
            // 
            this.barSubItem4.Caption = "&Toolbars";
            this.barSubItem4.Id = 47;
            this.barSubItem4.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.xrBarToolbarsListItem1)});
            this.barSubItem4.Name = "barSubItem4";
            // 
            // xrBarToolbarsListItem1
            // 
            this.xrBarToolbarsListItem1.Caption = "&Toolbars";
            this.xrBarToolbarsListItem1.Id = 48;
            this.xrBarToolbarsListItem1.Name = "xrBarToolbarsListItem1";
            // 
            // barSubItem5
            // 
            this.barSubItem5.Caption = "&Windows";
            this.barSubItem5.Id = 49;
            this.barSubItem5.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barDockPanelsListItem1)});
            this.barSubItem5.Name = "barSubItem5";
            // 
            // barDockPanelsListItem1
            // 
            this.barDockPanelsListItem1.Caption = "&Windows";
            this.barDockPanelsListItem1.Id = 50;
            this.barDockPanelsListItem1.Name = "barDockPanelsListItem1";
            this.barDockPanelsListItem1.ShowCustomizationItem = false;
            this.barDockPanelsListItem1.ShowDockPanels = true;
            this.barDockPanelsListItem1.ShowToolbars = false;
            // 
            // barSubItem6
            // 
            this.barSubItem6.Caption = "Fo&rmat";
            this.barSubItem6.Id = 51;
            this.barSubItem6.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.commandColorBarItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandColorBarItem2),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem7, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem8),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem9, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem10),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem11, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem12),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem13, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem14, true)});
            this.barSubItem6.Name = "barSubItem6";
            // 
            // commandColorBarItem1
            // 
            this.commandColorBarItem1.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.commandColorBarItem1.Caption = "For&eground Color";
            this.commandColorBarItem1.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.ForeColor;
            this.commandColorBarItem1.Enabled = false;
            this.commandColorBarItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("commandColorBarItem1.Glyph")));
            this.commandColorBarItem1.Hint = "Set the foreground color of the control";
            this.commandColorBarItem1.Id = 5;
            this.commandColorBarItem1.Name = "commandColorBarItem1";
            // 
            // commandColorBarItem2
            // 
            this.commandColorBarItem2.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.commandColorBarItem2.Caption = "Bac&kground Color";
            this.commandColorBarItem2.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.BackColor;
            this.commandColorBarItem2.Enabled = false;
            this.commandColorBarItem2.Glyph = ((System.Drawing.Image)(resources.GetObject("commandColorBarItem2.Glyph")));
            this.commandColorBarItem2.Hint = "Set the background color of the control";
            this.commandColorBarItem2.Id = 6;
            this.commandColorBarItem2.Name = "commandColorBarItem2";
            // 
            // barSubItem7
            // 
            this.barSubItem7.Caption = "&Font";
            this.barSubItem7.Id = 52;
            this.barSubItem7.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem1, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem2),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem3)});
            this.barSubItem7.Name = "barSubItem7";
            // 
            // commandBarItem1
            // 
            this.commandBarItem1.Caption = "&Bold";
            this.commandBarItem1.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.FontBold;
            this.commandBarItem1.Enabled = false;
            this.commandBarItem1.Hint = "Make the font bold";
            this.commandBarItem1.Id = 2;
            this.commandBarItem1.ImageIndex = 0;
            this.commandBarItem1.Name = "commandBarItem1";
            // 
            // commandBarItem2
            // 
            this.commandBarItem2.Caption = "&Italic";
            this.commandBarItem2.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.FontItalic;
            this.commandBarItem2.Enabled = false;
            this.commandBarItem2.Hint = "Make the font italic";
            this.commandBarItem2.Id = 3;
            this.commandBarItem2.ImageIndex = 1;
            this.commandBarItem2.Name = "commandBarItem2";
            // 
            // commandBarItem3
            // 
            this.commandBarItem3.Caption = "&Underline";
            this.commandBarItem3.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.FontUnderline;
            this.commandBarItem3.Enabled = false;
            this.commandBarItem3.Hint = "Underline the font";
            this.commandBarItem3.Id = 4;
            this.commandBarItem3.ImageIndex = 2;
            this.commandBarItem3.Name = "commandBarItem3";
            // 
            // barSubItem8
            // 
            this.barSubItem8.Caption = "&Justify";
            this.barSubItem8.Id = 53;
            this.barSubItem8.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem4, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem5),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem6),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem7)});
            this.barSubItem8.Name = "barSubItem8";
            // 
            // commandBarItem4
            // 
            this.commandBarItem4.Caption = "&Left";
            this.commandBarItem4.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.JustifyLeft;
            this.commandBarItem4.Enabled = false;
            this.commandBarItem4.Hint = "Align the control\'s text to the left";
            this.commandBarItem4.Id = 7;
            this.commandBarItem4.ImageIndex = 5;
            this.commandBarItem4.Name = "commandBarItem4";
            // 
            // commandBarItem5
            // 
            this.commandBarItem5.Caption = "&Center";
            this.commandBarItem5.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.JustifyCenter;
            this.commandBarItem5.Enabled = false;
            this.commandBarItem5.Hint = "Align the control\'s text to the center";
            this.commandBarItem5.Id = 8;
            this.commandBarItem5.ImageIndex = 6;
            this.commandBarItem5.Name = "commandBarItem5";
            // 
            // commandBarItem6
            // 
            this.commandBarItem6.Caption = "&Right";
            this.commandBarItem6.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.JustifyRight;
            this.commandBarItem6.Enabled = false;
            this.commandBarItem6.Hint = "Align the control\'s text to the right";
            this.commandBarItem6.Id = 9;
            this.commandBarItem6.ImageIndex = 7;
            this.commandBarItem6.Name = "commandBarItem6";
            // 
            // commandBarItem7
            // 
            this.commandBarItem7.Caption = "&Justify";
            this.commandBarItem7.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.JustifyJustify;
            this.commandBarItem7.Enabled = false;
            this.commandBarItem7.Hint = "Justify the control\'s text";
            this.commandBarItem7.Id = 10;
            this.commandBarItem7.ImageIndex = 8;
            this.commandBarItem7.Name = "commandBarItem7";
            // 
            // barSubItem9
            // 
            this.barSubItem9.Caption = "&Align";
            this.barSubItem9.Id = 54;
            this.barSubItem9.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem9, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem10),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem11),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem12, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem13),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem14),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem8, true)});
            this.barSubItem9.Name = "barSubItem9";
            // 
            // commandBarItem9
            // 
            this.commandBarItem9.Caption = "&Lefts";
            this.commandBarItem9.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.AlignLeft;
            this.commandBarItem9.Enabled = false;
            this.commandBarItem9.Hint = "Left align the selected controls";
            this.commandBarItem9.Id = 12;
            this.commandBarItem9.ImageIndex = 18;
            this.commandBarItem9.Name = "commandBarItem9";
            // 
            // commandBarItem10
            // 
            this.commandBarItem10.Caption = "&Centers";
            this.commandBarItem10.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.AlignVerticalCenters;
            this.commandBarItem10.Enabled = false;
            this.commandBarItem10.Hint = "Align the centers of the selected controls vertically";
            this.commandBarItem10.Id = 13;
            this.commandBarItem10.ImageIndex = 19;
            this.commandBarItem10.Name = "commandBarItem10";
            // 
            // commandBarItem11
            // 
            this.commandBarItem11.Caption = "&Rights";
            this.commandBarItem11.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.AlignRight;
            this.commandBarItem11.Enabled = false;
            this.commandBarItem11.Hint = "Right align the selected controls";
            this.commandBarItem11.Id = 14;
            this.commandBarItem11.ImageIndex = 20;
            this.commandBarItem11.Name = "commandBarItem11";
            // 
            // commandBarItem12
            // 
            this.commandBarItem12.Caption = "&Tops";
            this.commandBarItem12.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.AlignTop;
            this.commandBarItem12.Enabled = false;
            this.commandBarItem12.Hint = "Align the tops of the selected controls";
            this.commandBarItem12.Id = 15;
            this.commandBarItem12.ImageIndex = 21;
            this.commandBarItem12.Name = "commandBarItem12";
            // 
            // commandBarItem13
            // 
            this.commandBarItem13.Caption = "&Middles";
            this.commandBarItem13.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.AlignHorizontalCenters;
            this.commandBarItem13.Enabled = false;
            this.commandBarItem13.Hint = "Align the centers of the selected controls horizontally";
            this.commandBarItem13.Id = 16;
            this.commandBarItem13.ImageIndex = 22;
            this.commandBarItem13.Name = "commandBarItem13";
            // 
            // commandBarItem14
            // 
            this.commandBarItem14.Caption = "&Bottoms";
            this.commandBarItem14.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.AlignBottom;
            this.commandBarItem14.Enabled = false;
            this.commandBarItem14.Hint = "Align the bottoms of the selected controls";
            this.commandBarItem14.Id = 17;
            this.commandBarItem14.ImageIndex = 23;
            this.commandBarItem14.Name = "commandBarItem14";
            // 
            // commandBarItem8
            // 
            this.commandBarItem8.Caption = "to &Grid";
            this.commandBarItem8.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.AlignToGrid;
            this.commandBarItem8.Enabled = false;
            this.commandBarItem8.Hint = "Align the positions of the selected controls to the grid";
            this.commandBarItem8.Id = 11;
            this.commandBarItem8.ImageIndex = 17;
            this.commandBarItem8.Name = "commandBarItem8";
            // 
            // barSubItem10
            // 
            this.barSubItem10.Caption = "&Make Same Size";
            this.barSubItem10.Id = 55;
            this.barSubItem10.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem15, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem16),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem17),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem18)});
            this.barSubItem10.Name = "barSubItem10";
            // 
            // commandBarItem15
            // 
            this.commandBarItem15.Caption = "&Width";
            this.commandBarItem15.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.SizeToControlWidth;
            this.commandBarItem15.Enabled = false;
            this.commandBarItem15.Hint = "Make the selected controls have the same width";
            this.commandBarItem15.Id = 18;
            this.commandBarItem15.ImageIndex = 24;
            this.commandBarItem15.Name = "commandBarItem15";
            // 
            // commandBarItem16
            // 
            this.commandBarItem16.Caption = "Size to Gri&d";
            this.commandBarItem16.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.SizeToGrid;
            this.commandBarItem16.Enabled = false;
            this.commandBarItem16.Hint = "Size the selected controls to the grid";
            this.commandBarItem16.Id = 19;
            this.commandBarItem16.ImageIndex = 25;
            this.commandBarItem16.Name = "commandBarItem16";
            // 
            // commandBarItem17
            // 
            this.commandBarItem17.Caption = "&Height";
            this.commandBarItem17.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.SizeToControlHeight;
            this.commandBarItem17.Enabled = false;
            this.commandBarItem17.Hint = "Make the selected controls have the same height";
            this.commandBarItem17.Id = 20;
            this.commandBarItem17.ImageIndex = 26;
            this.commandBarItem17.Name = "commandBarItem17";
            // 
            // commandBarItem18
            // 
            this.commandBarItem18.Caption = "&Both";
            this.commandBarItem18.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.SizeToControl;
            this.commandBarItem18.Enabled = false;
            this.commandBarItem18.Hint = "Make the selected controls the same size";
            this.commandBarItem18.Id = 21;
            this.commandBarItem18.ImageIndex = 27;
            this.commandBarItem18.Name = "commandBarItem18";
            // 
            // barSubItem11
            // 
            this.barSubItem11.Caption = "&Horizontal Spacing";
            this.barSubItem11.Id = 56;
            this.barSubItem11.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem19, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem20),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem21),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem22)});
            this.barSubItem11.Name = "barSubItem11";
            // 
            // commandBarItem19
            // 
            this.commandBarItem19.Caption = "Make &Equal";
            this.commandBarItem19.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.HorizSpaceMakeEqual;
            this.commandBarItem19.Enabled = false;
            this.commandBarItem19.Hint = "Make the spacing between the selected controls equal";
            this.commandBarItem19.Id = 22;
            this.commandBarItem19.ImageIndex = 28;
            this.commandBarItem19.Name = "commandBarItem19";
            // 
            // commandBarItem20
            // 
            this.commandBarItem20.Caption = "&Increase";
            this.commandBarItem20.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.HorizSpaceIncrease;
            this.commandBarItem20.Enabled = false;
            this.commandBarItem20.Hint = "Increase the spacing between the selected controls";
            this.commandBarItem20.Id = 23;
            this.commandBarItem20.ImageIndex = 29;
            this.commandBarItem20.Name = "commandBarItem20";
            // 
            // commandBarItem21
            // 
            this.commandBarItem21.Caption = "&Decrease";
            this.commandBarItem21.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.HorizSpaceDecrease;
            this.commandBarItem21.Enabled = false;
            this.commandBarItem21.Hint = "Decrease the spacing between the selected controls";
            this.commandBarItem21.Id = 24;
            this.commandBarItem21.ImageIndex = 30;
            this.commandBarItem21.Name = "commandBarItem21";
            // 
            // commandBarItem22
            // 
            this.commandBarItem22.Caption = "&Remove";
            this.commandBarItem22.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.HorizSpaceConcatenate;
            this.commandBarItem22.Enabled = false;
            this.commandBarItem22.Hint = "Remove the spacing between the selected controls";
            this.commandBarItem22.Id = 25;
            this.commandBarItem22.ImageIndex = 31;
            this.commandBarItem22.Name = "commandBarItem22";
            // 
            // barSubItem12
            // 
            this.barSubItem12.Caption = "&Vertical Spacing";
            this.barSubItem12.Id = 57;
            this.barSubItem12.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem23, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem24),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem25),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem26)});
            this.barSubItem12.Name = "barSubItem12";
            // 
            // commandBarItem23
            // 
            this.commandBarItem23.Caption = "Make &Equal";
            this.commandBarItem23.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.VertSpaceMakeEqual;
            this.commandBarItem23.Enabled = false;
            this.commandBarItem23.Hint = "Make the spacing between the selected controls equal";
            this.commandBarItem23.Id = 26;
            this.commandBarItem23.ImageIndex = 32;
            this.commandBarItem23.Name = "commandBarItem23";
            // 
            // commandBarItem24
            // 
            this.commandBarItem24.Caption = "&Increase";
            this.commandBarItem24.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.VertSpaceIncrease;
            this.commandBarItem24.Enabled = false;
            this.commandBarItem24.Hint = "Increase the spacing between the selected controls";
            this.commandBarItem24.Id = 27;
            this.commandBarItem24.ImageIndex = 33;
            this.commandBarItem24.Name = "commandBarItem24";
            // 
            // commandBarItem25
            // 
            this.commandBarItem25.Caption = "&Decrease";
            this.commandBarItem25.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.VertSpaceDecrease;
            this.commandBarItem25.Enabled = false;
            this.commandBarItem25.Hint = "Decrease the spacing between the selected controls";
            this.commandBarItem25.Id = 28;
            this.commandBarItem25.ImageIndex = 34;
            this.commandBarItem25.Name = "commandBarItem25";
            // 
            // commandBarItem26
            // 
            this.commandBarItem26.Caption = "&Remove";
            this.commandBarItem26.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.VertSpaceConcatenate;
            this.commandBarItem26.Enabled = false;
            this.commandBarItem26.Hint = "Remove the spacing between the selected controls";
            this.commandBarItem26.Id = 29;
            this.commandBarItem26.ImageIndex = 35;
            this.commandBarItem26.Name = "commandBarItem26";
            // 
            // barSubItem13
            // 
            this.barSubItem13.Caption = "&Center in Form";
            this.barSubItem13.Id = 58;
            this.barSubItem13.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem27, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem28)});
            this.barSubItem13.Name = "barSubItem13";
            // 
            // commandBarItem27
            // 
            this.commandBarItem27.Caption = "&Horizontally";
            this.commandBarItem27.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.CenterHorizontally;
            this.commandBarItem27.Enabled = false;
            this.commandBarItem27.Hint = "Horizontally center the selected controls within a band";
            this.commandBarItem27.Id = 30;
            this.commandBarItem27.ImageIndex = 36;
            this.commandBarItem27.Name = "commandBarItem27";
            // 
            // commandBarItem28
            // 
            this.commandBarItem28.Caption = "&Vertically";
            this.commandBarItem28.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.CenterVertically;
            this.commandBarItem28.Enabled = false;
            this.commandBarItem28.Hint = "Vertically center the selected controls within a band";
            this.commandBarItem28.Id = 31;
            this.commandBarItem28.ImageIndex = 37;
            this.commandBarItem28.Name = "commandBarItem28";
            // 
            // barSubItem14
            // 
            this.barSubItem14.Caption = "&Order";
            this.barSubItem14.Id = 59;
            this.barSubItem14.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem29, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem30)});
            this.barSubItem14.Name = "barSubItem14";
            // 
            // commandBarItem29
            // 
            this.commandBarItem29.Caption = "&Bring to Front";
            this.commandBarItem29.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.BringToFront;
            this.commandBarItem29.Enabled = false;
            this.commandBarItem29.Hint = "Bring the selected controls to the front";
            this.commandBarItem29.Id = 32;
            this.commandBarItem29.ImageIndex = 38;
            this.commandBarItem29.Name = "commandBarItem29";
            // 
            // commandBarItem30
            // 
            this.commandBarItem30.Caption = "&Send to Back";
            this.commandBarItem30.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.SendToBack;
            this.commandBarItem30.Enabled = false;
            this.commandBarItem30.Hint = "Move the selected controls to the back";
            this.commandBarItem30.Id = 33;
            this.commandBarItem30.ImageIndex = 39;
            this.commandBarItem30.Name = "commandBarItem30";
            // 
            // barSubItemDebug
            // 
            this.barSubItemDebug.Caption = "&Debug";
            this.barSubItemDebug.Id = 100;
            this.barSubItemDebug.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonRun),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonStop)});
            this.barSubItemDebug.Name = "barSubItemDebug";
            // 
            // barButtonRun
            // 
            this.barButtonRun.Caption = "&RunBrowser";
            this.barButtonRun.Hint = "Run Browser to Preview Your Current Work ";
            this.barButtonRun.Id = 86;
            this.barButtonRun.ImageIndex = 42;
            this.barButtonRun.Name = "barButtonRun";
            this.barButtonRun.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonRun_ItemClick);
            // 
            // barButtonStop
            // 
            this.barButtonStop.Caption = "Stop&Browser";
            this.barButtonStop.Enabled = false;
            this.barButtonStop.Hint = "Stop your Current Browser";
            this.barButtonStop.Id = 87;
            this.barButtonStop.ImageIndex = 43;
            this.barButtonStop.Name = "barButtonStop";
            this.barButtonStop.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonStop_ItemClick);
            // 
            // barSubItem0
            // 
            this.barSubItem0.Caption = "&Help";
            this.barSubItem0.Id = 103;
            this.barSubItem0.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem0)});
            this.barSubItem0.Name = "barSubItem0";
            // 
            // commandBarItem0
            // 
            this.commandBarItem0.Caption = "&About";
            this.commandBarItem0.Id = 104;
            this.commandBarItem0.Name = "commandBarItem0";
            this.commandBarItem0.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.About_ItemClick);
            // 
            // C_MainToolbar
            // 
            this.C_MainToolbar.BarName = "Toolbar";
            this.C_MainToolbar.DockCol = 0;
            this.C_MainToolbar.DockRow = 1;
            this.C_MainToolbar.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.C_MainToolbar.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem31),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem32),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem33),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem34, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem35),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Caption, this.commandBarItem36, "&Paste"),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonRun, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonStop)});
            this.C_MainToolbar.Text = "Main Toolbar";
            // 
            // C_FormattingToolbar
            // 
            this.C_FormattingToolbar.BarName = "Formatting Toolbar";
            this.C_FormattingToolbar.DockCol = 1;
            this.C_FormattingToolbar.DockRow = 1;
            this.C_FormattingToolbar.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.C_FormattingToolbar.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barEditItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.barEditItem2),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem2),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem3),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandColorBarItem1, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandColorBarItem2),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem4, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem5),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem6),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem7)});
            this.C_FormattingToolbar.Text = "Formatting Toolbar";
            // 
            // barEditItem1
            // 
            this.barEditItem1.Caption = "Font Name";
            this.barEditItem1.Edit = this.recentlyUsedItemsComboBox1;
            this.barEditItem1.Hint = "Font Name";
            this.barEditItem1.Id = 0;
            this.barEditItem1.Name = "barEditItem1";
            this.barEditItem1.Width = 120;
            // 
            // recentlyUsedItemsComboBox1
            // 
            this.recentlyUsedItemsComboBox1.AutoHeight = false;
            this.recentlyUsedItemsComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.recentlyUsedItemsComboBox1.DropDownRows = 12;
            this.recentlyUsedItemsComboBox1.Name = "recentlyUsedItemsComboBox1";
            // 
            // barEditItem2
            // 
            this.barEditItem2.Caption = "Font Size";
            this.barEditItem2.Edit = this.designRepositoryItemComboBox1;
            this.barEditItem2.Hint = "Font Size";
            this.barEditItem2.Id = 1;
            this.barEditItem2.Name = "barEditItem2";
            // 
            // designRepositoryItemComboBox1
            // 
            this.designRepositoryItemComboBox1.AutoHeight = false;
            this.designRepositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.designRepositoryItemComboBox1.Name = "designRepositoryItemComboBox1";
            // 
            // C_LayoutToolbar
            // 
            this.C_LayoutToolbar.BarName = "Layout Toolbar";
            this.C_LayoutToolbar.DockCol = 0;
            this.C_LayoutToolbar.DockRow = 2;
            this.C_LayoutToolbar.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.C_LayoutToolbar.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem8),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem9, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem10),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem11),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem12, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem13),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem14),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem15, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem16),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem17),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem18),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem19, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem20),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem21),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem22),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem23, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem24),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem25),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem26),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem27, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem28),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem29, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem30)});
            this.C_LayoutToolbar.Text = "Layout Toolbar";
            // 
            // designBar5
            // 
            this.designBar5.BarName = "Status Bar";
            this.designBar5.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.designBar5.DockCol = 0;
            this.designBar5.DockRow = 0;
            this.designBar5.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.designBar5.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.C_StatusBar)});
            this.designBar5.OptionsBar.AllowQuickCustomization = false;
            this.designBar5.OptionsBar.DrawDragBorder = false;
            this.designBar5.OptionsBar.UseWholeRow = true;
            this.designBar5.Text = "Status Bar";
            // 
            // C_StatusBar
            // 
            this.C_StatusBar.AutoSize = DevExpress.XtraBars.BarStaticItemSize.Spring;
            this.C_StatusBar.Id = 42;
            this.C_StatusBar.Name = "C_StatusBar";
            this.C_StatusBar.TextAlignment = System.Drawing.StringAlignment.Near;
            this.C_StatusBar.Width = 32;
            // 
            // C_ZoomBar
            // 
            this.C_ZoomBar.BarName = "Zoom Bar";
            this.C_ZoomBar.DockCol = 1;
            this.C_ZoomBar.DockRow = 2;
            this.C_ZoomBar.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.C_ZoomBar.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem44),
            new DevExpress.XtraBars.LinkPersistInfo(this.xrZoomBarEditItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.commandBarItem45)});
            this.C_ZoomBar.Text = "Zoom Bar";
            // 
            // commandBarItem44
            // 
            this.commandBarItem44.Caption = "Zoom Out";
            this.commandBarItem44.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.ZoomOut;
            this.commandBarItem44.Enabled = false;
            this.commandBarItem44.Hint = "Zoom out the design surface";
            this.commandBarItem44.Id = 65;
            this.commandBarItem44.ImageIndex = 40;
            this.commandBarItem44.Name = "commandBarItem44";
            // 
            // xrZoomBarEditItem1
            // 
            this.xrZoomBarEditItem1.Caption = "Zoom";
            this.xrZoomBarEditItem1.Edit = this.designRepositoryItemComboBox2;
            this.xrZoomBarEditItem1.Enabled = false;
            this.xrZoomBarEditItem1.Hint = "Select or input the zoom factor";
            this.xrZoomBarEditItem1.Id = 66;
            this.xrZoomBarEditItem1.Name = "xrZoomBarEditItem1";
            this.xrZoomBarEditItem1.Width = 70;
            // 
            // designRepositoryItemComboBox2
            // 
            this.designRepositoryItemComboBox2.AutoComplete = false;
            this.designRepositoryItemComboBox2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.designRepositoryItemComboBox2.Name = "designRepositoryItemComboBox2";
            // 
            // commandBarItem45
            // 
            this.commandBarItem45.Caption = "Zoom In";
            this.commandBarItem45.Command = DevExpress.XtraReports.UserDesigner.ReportCommand.ZoomIn;
            this.commandBarItem45.Enabled = false;
            this.commandBarItem45.Hint = "Zoom in the design surface";
            this.commandBarItem45.Id = 67;
            this.commandBarItem45.ImageIndex = 41;
            this.commandBarItem45.Name = "commandBarItem45";
            // 
            // C_DesignDockManager
            // 
            this.C_DesignDockManager.Form = this;
            this.C_DesignDockManager.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("C_DesignDockManager.ImageStream")));
            this.C_DesignDockManager.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.panelContainer3,
            this.C_ToolBoxDockPanel});
            this.C_DesignDockManager.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            this.C_DesignDockManager.XRDesignPanel = this.C_ReportDesignPanel;
            // 
            // panelContainer3
            // 
            this.panelContainer3.ActiveChild = this.C_PropertyGridDockPanel;
            this.panelContainer3.Controls.Add(this.C_FieldListDockPanel);
            this.panelContainer3.Controls.Add(this.C_FilterPanel);
            this.panelContainer3.Controls.Add(this.C_ReportExplorerDockPanel);
            this.panelContainer3.Controls.Add(this.C_PropertyGridDockPanel);
            this.panelContainer3.Dock = DevExpress.XtraBars.Docking.DockingStyle.Right;
            this.panelContainer3.FloatVertical = true;
            this.panelContainer3.ID = new System.Guid("dff1b91d-3d4a-42d4-b2e6-bd90e079c085");
            this.panelContainer3.Location = new System.Drawing.Point(729, 75);
            this.panelContainer3.Name = "panelContainer3";
            this.panelContainer3.Size = new System.Drawing.Size(286, 595);
            this.panelContainer3.Tabbed = true;
            this.panelContainer3.Text = "panelContainer3";
            // 
            // C_PropertyGridDockPanel
            // 
            this.C_PropertyGridDockPanel.Controls.Add(this.propertyGridDockPanel1_Container);
            this.C_PropertyGridDockPanel.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.C_PropertyGridDockPanel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.C_PropertyGridDockPanel.ID = new System.Guid("b38d12c3-cd06-4dec-b93d-63a0088e495a");
            this.C_PropertyGridDockPanel.ImageIndex = 1;
            this.C_PropertyGridDockPanel.Location = new System.Drawing.Point(3, 25);
            this.C_PropertyGridDockPanel.Name = "C_PropertyGridDockPanel";
            this.C_PropertyGridDockPanel.Size = new System.Drawing.Size(280, 544);
            this.C_PropertyGridDockPanel.Text = "Property Grid";
            this.C_PropertyGridDockPanel.XRDesignPanel = this.C_ReportDesignPanel;
            // 
            // propertyGridDockPanel1_Container
            // 
            this.propertyGridDockPanel1_Container.Location = new System.Drawing.Point(0, 0);
            this.propertyGridDockPanel1_Container.Name = "propertyGridDockPanel1_Container";
            this.propertyGridDockPanel1_Container.Size = new System.Drawing.Size(280, 544);
            this.propertyGridDockPanel1_Container.TabIndex = 0;
            // 
            // C_ReportDesignPanel
            // 
            this.C_ReportDesignPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.C_ReportDesignPanel.Location = new System.Drawing.Point(198, 75);
            this.C_ReportDesignPanel.Name = "C_ReportDesignPanel";
            this.C_ReportDesignPanel.Padding = new System.Windows.Forms.Padding(1);
            this.C_ReportDesignPanel.Size = new System.Drawing.Size(531, 595);
            this.C_ReportDesignPanel.TabIndex = 4;
            this.C_ReportDesignPanel.Text = "xrDesignPanel1";
            // 
            // C_FieldListDockPanel
            // 
            this.C_FieldListDockPanel.Controls.Add(this.fieldListDockPanel1_Container);
            this.C_FieldListDockPanel.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.C_FieldListDockPanel.FloatVertical = true;
            this.C_FieldListDockPanel.Font = new System.Drawing.Font("Verdana", 9F);
            this.C_FieldListDockPanel.ID = new System.Guid("faf69838-a93f-4114-83e8-d0d09cc5ce95");
            this.C_FieldListDockPanel.ImageIndex = 0;
            this.C_FieldListDockPanel.Location = new System.Drawing.Point(3, 25);
            this.C_FieldListDockPanel.Name = "C_FieldListDockPanel";
            this.C_FieldListDockPanel.Size = new System.Drawing.Size(280, 544);
            this.C_FieldListDockPanel.Text = "Field List";
            this.C_FieldListDockPanel.XRDesignPanel = this.C_ReportDesignPanel;
            // 
            // fieldListDockPanel1_Container
            // 
            this.fieldListDockPanel1_Container.Location = new System.Drawing.Point(0, 0);
            this.fieldListDockPanel1_Container.Name = "fieldListDockPanel1_Container";
            this.fieldListDockPanel1_Container.Size = new System.Drawing.Size(280, 544);
            this.fieldListDockPanel1_Container.TabIndex = 0;
            // 
            // C_FilterPanel
            // 
            this.C_FilterPanel.Controls.Add(this.dockPanel1_Container);
            this.C_FilterPanel.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.C_FilterPanel.Font = new System.Drawing.Font("Verdana", 9F);
            this.C_FilterPanel.ID = new System.Guid("f9dab313-3e40-4a04-abbd-4a08d0ba441d");
            this.C_FilterPanel.Location = new System.Drawing.Point(3, 25);
            this.C_FilterPanel.Name = "C_FilterPanel";
            this.C_FilterPanel.Size = new System.Drawing.Size(280, 544);
            this.C_FilterPanel.Text = "Filter List";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.C_FilterList);
            this.dockPanel1_Container.Location = new System.Drawing.Point(0, 0);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(280, 544);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // C_FilterList
            // 
            this.C_FilterList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.C_FilterList.Location = new System.Drawing.Point(0, 0);
            this.C_FilterList.Name = "C_FilterList";
            this.C_FilterList.Size = new System.Drawing.Size(280, 544);
            this.C_FilterList.TabIndex = 0;
            this.C_FilterList.DoubleClick += new System.EventHandler(this.C_FilterList_DoubleClick);
            this.C_FilterList.MouseUp += new System.Windows.Forms.MouseEventHandler(this.C_FilterList_MouseUp);
            // 
            // C_ReportExplorerDockPanel
            // 
            this.C_ReportExplorerDockPanel.Controls.Add(this.reportExplorerDockPanel1_Container);
            this.C_ReportExplorerDockPanel.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.C_ReportExplorerDockPanel.FloatVertical = true;
            this.C_ReportExplorerDockPanel.Font = new System.Drawing.Font("Verdana", 9F);
            this.C_ReportExplorerDockPanel.ID = new System.Guid("fb3ec6cc-3b9b-4b9c-91cf-cff78c1edbf1");
            this.C_ReportExplorerDockPanel.ImageIndex = 2;
            this.C_ReportExplorerDockPanel.Location = new System.Drawing.Point(3, 25);
            this.C_ReportExplorerDockPanel.Name = "C_ReportExplorerDockPanel";
            this.C_ReportExplorerDockPanel.Size = new System.Drawing.Size(280, 544);
            this.C_ReportExplorerDockPanel.Text = "Report Explorer";
            this.C_ReportExplorerDockPanel.XRDesignPanel = this.C_ReportDesignPanel;
            // 
            // reportExplorerDockPanel1_Container
            // 
            this.reportExplorerDockPanel1_Container.Location = new System.Drawing.Point(0, 0);
            this.reportExplorerDockPanel1_Container.Name = "reportExplorerDockPanel1_Container";
            this.reportExplorerDockPanel1_Container.Size = new System.Drawing.Size(280, 544);
            this.reportExplorerDockPanel1_Container.TabIndex = 0;
            // 
            // C_ToolBoxDockPanel
            // 
            this.C_ToolBoxDockPanel.Controls.Add(this.toolBoxDockPanel1_Container);
            this.C_ToolBoxDockPanel.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.C_ToolBoxDockPanel.GroupsStyle = DevExpress.XtraNavBar.NavBarGroupStyle.SmallIconsText;
            this.C_ToolBoxDockPanel.ID = new System.Guid("161a5a1a-d9b9-4f06-9ac4-d0c3e507c54f");
            this.C_ToolBoxDockPanel.ImageIndex = 3;
            this.C_ToolBoxDockPanel.Location = new System.Drawing.Point(0, 75);
            this.C_ToolBoxDockPanel.Name = "C_ToolBoxDockPanel";
            this.C_ToolBoxDockPanel.Size = new System.Drawing.Size(198, 595);
            this.C_ToolBoxDockPanel.Text = "Tool Box";
            this.C_ToolBoxDockPanel.XRDesignPanel = this.C_ReportDesignPanel;
            // 
            // toolBoxDockPanel1_Container
            // 
            this.toolBoxDockPanel1_Container.Location = new System.Drawing.Point(3, 25);
            this.toolBoxDockPanel1_Container.Name = "toolBoxDockPanel1_Container";
            this.toolBoxDockPanel1_Container.Size = new System.Drawing.Size(192, 567);
            this.toolBoxDockPanel1_Container.TabIndex = 0;
            // 
            // C_OpenDataSource
            // 
            this.C_OpenDataSource.Caption = "Open Data Source";
            this.C_OpenDataSource.Id = 68;
            this.C_OpenDataSource.Name = "C_OpenDataSource";
            this.C_OpenDataSource.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.C_OpenDataSource_ItemClick);
            // 
            // C_RemoveDataSource
            // 
            this.C_RemoveDataSource.Caption = "Remove Data Source";
            this.C_RemoveDataSource.Id = 69;
            this.C_RemoveDataSource.Name = "C_RemoveDataSource";
            this.C_RemoveDataSource.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.C_RemoveDataSource_ItemClick);
            // 
            // C_PreviewDataSource
            // 
            this.C_PreviewDataSource.Caption = "Preview Data Source";
            this.C_PreviewDataSource.Id = 70;
            this.C_PreviewDataSource.Name = "C_PreviewDataSource";
            this.C_PreviewDataSource.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.C_PreviewDataSource_ItemClick);
            // 
            // C_LoadSectionFilters
            // 
            this.C_LoadSectionFilters.Caption = "Load Section Filters";
            this.C_LoadSectionFilters.Id = 72;
            this.C_LoadSectionFilters.Name = "C_LoadSectionFilters";
            this.C_LoadSectionFilters.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.C_LoadSectionFilters_ItemClick);
            // 
            // C_EditFilters
            // 
            this.C_EditFilters.Caption = "Edit Filter";
            this.C_EditFilters.Id = 87;
            this.C_EditFilters.Name = "C_EditFilters";
            this.C_EditFilters.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.C_EditFilters_ItemClick);
            // 
            // C_RemoveFilters
            // 
            this.C_RemoveFilters.Caption = "Remove Filter";
            this.C_RemoveFilters.Id = 73;
            this.C_RemoveFilters.Name = "C_RemoveFilters";
            this.C_RemoveFilters.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.C_RemoveFilters_ItemClick);
            // 
            // C_ClearAllFilters
            // 
            this.C_ClearAllFilters.Caption = "Clear All Filters";
            this.C_ClearAllFilters.Id = 74;
            this.C_ClearAllFilters.Name = "C_ClearAllFilters";
            this.C_ClearAllFilters.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.C_ClearAllFilters_ItemClick);
            // 
            // barSubItem15
            // 
            this.barSubItem15.Caption = "Load Section Filters";
            this.barSubItem15.Id = 75;
            this.barSubItem15.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Caption, this.C_LoadCustomSectionFilters, "Load Custom Section Filters"),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Caption, this.C_LoadAdvReportFilters, "Load AdvReport Filters"),
            new DevExpress.XtraBars.LinkPersistInfo(this.C_LoadSectionFilters)});
            this.barSubItem15.Name = "barSubItem15";
            // 
            // C_LoadCustomSectionFilters
            // 
            this.C_LoadCustomSectionFilters.Caption = "Load Custom Section Filters";
            this.C_LoadCustomSectionFilters.Id = 76;
            this.C_LoadCustomSectionFilters.Name = "C_LoadCustomSectionFilters";
            this.C_LoadCustomSectionFilters.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.C_LoadCustomSectionFilters_ItemClick);
            // 
            // C_LoadAdvReportFilters
            // 
            this.C_LoadAdvReportFilters.Caption = "Load AdvReport Filters";
            this.C_LoadAdvReportFilters.Id = 152;
            this.C_LoadAdvReportFilters.Name = "C_LoadAdvReportFilters";
            this.C_LoadAdvReportFilters.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.C_LoadAdvReportFilters_ItemClick);
            // 
            // barEditItem3
            // 
            this.barEditItem3.Caption = "barEditItem3";
            this.barEditItem3.Edit = this.repositoryItemRadioGroup1;
            this.barEditItem3.Id = 77;
            this.barEditItem3.Name = "barEditItem3";
            // 
            // repositoryItemRadioGroup1
            // 
            this.repositoryItemRadioGroup1.Name = "repositoryItemRadioGroup1";
            // 
            // C_CheckZoneO
            // 
            this.C_CheckZoneO.Caption = "Field Zone Offense";
            this.C_CheckZoneO.Id = 78;
            this.C_CheckZoneO.Name = "C_CheckZoneO";
            this.C_CheckZoneO.Tag = 1;
            this.C_CheckZoneO.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.C_ReportScTypeCheck_CheckedChanged);
            // 
            // C_CheckZoneD
            // 
            this.C_CheckZoneD.Caption = "Field Zone Defense";
            this.C_CheckZoneD.Id = 79;
            this.C_CheckZoneD.Name = "C_CheckZoneD";
            this.C_CheckZoneD.Tag = 3;
            this.C_CheckZoneD.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.C_ReportScTypeCheck_CheckedChanged);
            // 
            // C_CheckDDO
            // 
            this.C_CheckDDO.Caption = "Down And Distance Offense";
            this.C_CheckDDO.Id = 80;
            this.C_CheckDDO.Name = "C_CheckDDO";
            this.C_CheckDDO.Tag = 2;
            this.C_CheckDDO.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.C_ReportScTypeCheck_CheckedChanged);
            // 
            // C_CheckDDD
            // 
            this.C_CheckDDD.Caption = "Down And Distance Defense";
            this.C_CheckDDD.Id = 81;
            this.C_CheckDDD.Name = "C_CheckDDD";
            this.C_CheckDDD.Tag = 4;
            this.C_CheckDDD.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.C_ReportScTypeCheck_CheckedChanged);
            // 
            // barToolbarsListItem1
            // 
            this.barToolbarsListItem1.Caption = "Atrribu";
            this.barToolbarsListItem1.Id = 82;
            this.barToolbarsListItem1.Name = "barToolbarsListItem1";
            // 
            // barSubItem16
            // 
            this.barSubItem16.Caption = "Attributes";
            this.barSubItem16.Id = 83;
            this.barSubItem16.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.C_CheckCustom),
            new DevExpress.XtraBars.LinkPersistInfo(this.C_CheckZoneO),
            new DevExpress.XtraBars.LinkPersistInfo(this.C_CheckZoneD),
            new DevExpress.XtraBars.LinkPersistInfo(this.C_CheckDDO),
            new DevExpress.XtraBars.LinkPersistInfo(this.C_CheckDDD)});
            this.barSubItem16.Name = "barSubItem16";
            // 
            // C_CheckCustom
            // 
            this.C_CheckCustom.Caption = "Custom";
            this.C_CheckCustom.Checked = true;
            this.C_CheckCustom.Id = 84;
            this.C_CheckCustom.Name = "C_CheckCustom";
            this.C_CheckCustom.Tag = 0;
            this.C_CheckCustom.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.C_ReportScTypeCheck_CheckedChanged);
            // 
            // C_LoadDataConfig
            // 
            this.C_LoadDataConfig.Caption = "Load Data Template";
            this.C_LoadDataConfig.Id = 85;
            this.C_LoadDataConfig.Name = "C_LoadDataConfig";
            this.C_LoadDataConfig.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.C_LoadDataConfig_ItemClick);
            // 
            // C_SaveDataConfig
            // 
            this.C_SaveDataConfig.Caption = "Save Data Template";
            this.C_SaveDataConfig.Id = 86;
            this.C_SaveDataConfig.Name = "C_SaveDataConfig";
            this.C_SaveDataConfig.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.C_SaveDataConfig_ItemClick);
            // 
            // C_FiledListMenu
            // 
            this.C_FiledListMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.C_OpenDataSource),
            new DevExpress.XtraBars.LinkPersistInfo(this.C_RemoveDataSource),
            new DevExpress.XtraBars.LinkPersistInfo(this.C_PreviewDataSource),
            new DevExpress.XtraBars.LinkPersistInfo(this.C_LoadDataConfig, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.C_SaveDataConfig)});
            this.C_FiledListMenu.Manager = this.C_DesignBarManager;
            this.C_FiledListMenu.Name = "C_FiledListMenu";
            // 
            // C_FilterListMenu
            // 
            this.C_FilterListMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem15),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem16),
            new DevExpress.XtraBars.LinkPersistInfo(this.C_EditFilters),
            new DevExpress.XtraBars.LinkPersistInfo(this.C_RemoveFilters),
            new DevExpress.XtraBars.LinkPersistInfo(this.C_ClearAllFilters)});
            this.C_FilterListMenu.Manager = this.C_DesignBarManager;
            this.C_FilterListMenu.Name = "C_FilterListMenu";
            // 
            // ReportDesignerBase
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.ClientSize = new System.Drawing.Size(1015, 692);
            this.Controls.Add(this.C_ReportDesignPanel);
            this.Controls.Add(this.panelContainer3);
            this.Controls.Add(this.C_ToolBoxDockPanel);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "ReportDesignerBase";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Webb Report Designer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ReportDesignerBase_Load);
            ((System.ComponentModel.ISupportInitialize)(this.C_DesignBarManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.recentlyUsedItemsComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.designRepositoryItemComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.designRepositoryItemComboBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.C_DesignDockManager)).EndInit();
            this.panelContainer3.ResumeLayout(false);
            this.C_PropertyGridDockPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.C_ReportDesignPanel)).EndInit();
            this.C_FieldListDockPanel.ResumeLayout(false);
            this.C_FilterPanel.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            this.C_ReportExplorerDockPanel.ResumeLayout(false);
            this.C_ToolBoxDockPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRadioGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.C_FiledListMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.C_FilterListMenu)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private void InitializeDesigner()
        {
            this.C_FieldListDockPanel.FieldList.MouseUp += new MouseEventHandler(FieldList_MouseUp);
            this.C_ReportDesignPanel.DesignerHostLoaded += new DevExpress.XtraReports.UserDesigner.DesignerLoadedEventHandler(C_ReportDesignPanel_DesignerHostLoaded);
            this.C_ReportDesignPanel.ReportStateChanged += new DevExpress.XtraReports.UserDesigner.ReportStateEventHandler(C_ReportDesignPanel_ReportStateChanged);
            this.C_ReportDesignPanel.GotFocus += new EventHandler(C_ReportDesignPanel_GotFocus);
            //
            this.C_ReportDesignPanel.ReportSaveChanges += new SavedChangeEventHandler(C_ReportDesignPanel_ReportSaveChanges);
            this.DataProvider = new Webb.Reports.DataProvider.WebbDataProvider();
            this._DataSource = new WebbDataSource();
            //
            Webb.Reports.DataProvider.VideoPlayBackManager.PublicDBProvider = this.DataProvider;	//Set the data provider for the click events.
            //
            //System.Windows.Forms.MessageBox.Show("ActivateHost");
            //this.C_ReportDesignPanel.ActivateHost(new WebbReport());
            this.C_ReportDesignPanel.OpenReport(this.GetDefaultReport());
        }

        #region Content Menu Event Functions

        private void C_LoadSectionFilters_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Wu.Country@2007-11-21 09:51 modified some of the following code.
            if (this._DataSource.SectionFilters == null) this._DataSource.SectionFilters = new SectionFilterCollection();
            DialogResult m_result = this.DataProvider.ShowSectionFilterSelector(this._DataSource.SectionFilters);

            //04-29-2008@Scott
            if (this.C_ReportDesignPanel.Report is WebbReport)
            {
                WebbReport report = this.C_ReportDesignPanel.Report as WebbReport;

                report.Template.SectionFilters = this._DataSource.SectionFilters.Copy();
            }

            if (m_result == DialogResult.OK)
            {
                this.AddFilters(this._DataSource.SectionFilters);
            }
            this.UpdateExControls(DataSourceStatus.Update);
        }

        //02-26-2008@Scott
        private void C_LoadCustomSectionFilters_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            WebbReport report = this.C_ReportDesignPanel.Report as WebbReport;

            if (report == null) return;

            if (this._DataSource.SectionFilters == null) this._DataSource.SectionFilters = new SectionFilterCollection();

            DialogResult m_result = this.DataProvider.ShowCustomSectionFilterSelector(this._DataSource.SectionFilters);

            //04-29-2008@Scott

            report.Template.SectionFilters = this._DataSource.SectionFilters.Copy();


            if (m_result == DialogResult.OK)
            {
                report.Template.ReportScType = ReportScType.Custom;

                this.CheckItem(ReportScType.Custom);

                this.AddFilters(this._DataSource.SectionFilters);
            }
            this.UpdateExControls(DataSourceStatus.Update);
        }

        //08-15-2008@Scott
        private void C_LoadAdvReportFilters_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            WebbReport report = this.C_ReportDesignPanel.Report as WebbReport;

            //04-29-2008@Scott
            if (report == null)
            {
                return;
            }

            if (this._DataSource.SectionFilters == null) this._DataSource.SectionFilters = new SectionFilterCollection();

            DialogResult m_result = this.DataProvider.ShowAdvReportFilterSelector(this._DataSource.SectionFilters);

            if (m_result == DialogResult.OK)
            {
                report.Template.ReportScType = ReportScType.Custom;

                this.CheckItem(ReportScType.Custom);

                this.AddFilters(this._DataSource.SectionFilters);
            }
            this.UpdateExControls(DataSourceStatus.Update);
        }

        private void C_ReportScTypeCheck_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.C_CheckCustom.Checked = false;
            this.C_CheckDDD.Checked = false;
            this.C_CheckDDO.Checked = false;
            this.C_CheckZoneD.Checked = false;
            this.C_CheckZoneO.Checked = false;

            DevExpress.XtraBars.BarCheckItem checkItem = e.Item as DevExpress.XtraBars.BarCheckItem;

            checkItem.Checked = true;

            string strReportScType = checkItem.Tag.ToString();

            WebbReport report = this.C_ReportDesignPanel.Report as WebbReport;

            report.Template.ReportScType = (ReportScType)Enum.Parse(typeof(ReportScType), strReportScType, true);

            if (report.Template.ReportScType == ReportScType.Custom)
            {
                this.RemoveSectionFilters();

                return;
            }

            string strUserFolder = this.DataProvider.DBSourceConfig.UserFolder;

            report.LoadAdvSectionFilters(strUserFolder);

            this.AddFilters(report.Template.SectionFilters);

            this.UpdateExControls(DataSourceStatus.Update);
        }

        protected void AddFilters(SectionFilterCollection i_SectionFilters)
        {
            this.C_FilterList.Nodes.Clear();

            foreach (SectionFilter m_Filter in i_SectionFilters)
            {
                TreeNode m_Node = new TreeNode(m_Filter.FilterName);
                m_Node.Tag = m_Filter;
                this.C_FilterList.Nodes.Add(m_Node);
            }
        }

        private void C_RemoveFilters_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            WebbReport m_Report = this.C_ReportDesignPanel.Report as WebbReport;

            if (m_Report == null) return;

            if (m_Report.Template.ReportScType != ReportScType.Custom)
            {
                Webb.Utilities.MessageBoxEx.ShowMessage("Current Non-Custom advantage sectionfilters can not be removed in designer!");

                return;
            }

            if (this.C_FilterList.SelectedNode != null)
            {

                this._DataSource.SectionFilters.Remove(this.C_FilterList.SelectedNode.Tag as SectionFilter);

                this.C_FilterList.SelectedNode.Remove();

                SectionFilterCollection sectionFilter = new SectionFilterCollection();

                foreach (TreeNode node in this.C_FilterList.Nodes)
                {
                    sectionFilter.Add(node.Tag as SectionFilter);
                }

                m_Report.Template.SectionFilters = sectionFilter;

            }
            this.UpdateExControls(DataSourceStatus.Update);
        }

        private void C_OpenDataSource_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.LoadDataSource();
        }

        //Remove all data
        private void C_RemoveDataSource_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.RemoveDataSource();
        }

        //Load Data Config
        private void C_LoadDataConfig_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.LoadDataConfig();
        }

        //Load Data Config
        private void C_SaveDataConfig_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.SaveDataConfig();
        }

        //Update data source for ExControls 
        protected void UpdateExControls(DataSourceStatus i_Status)
        {
            if (this.C_ReportDesignPanel.Report != null)
            {
                bool b = this.C_ReportDesignPanel.Report is WebbReport;

                WebbReport m_Report = this.C_ReportDesignPanel.Report as WebbReport;

                if (m_Report == null)
                {
                    System.Diagnostics.Debug.WriteLine("Convert report to webbreport failed !");

                    return;
                }

                if (i_Status == DataSourceStatus.Remove)
                {
                    m_Report.MinimizeDesignArea();	//06-13-2008@Scott When remove data, minimize design area
                }

                m_Report.UpdataDBSourceForExControls(null, string.Empty);	//remove at first

                if (i_Status == DataSourceStatus.AddNew || i_Status == DataSourceStatus.Update)
                {
                    m_Report.UpdataDBSourceForExControls(this._DataSource.DataSource, this._DataSource.DataMember);	//add
                }
                m_Report.WebbDataSource = this._DataSource;
            }
        }

        //Show preview window
        private void C_PreviewDataSource_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this._DataSource.DataSource == null)
            {
                MessageBox.Show("Please set data source.");

                return;
            }
            this.DataProvider.ShowPreviewForm(this._DataSource);
        }

        //Remove section filters
        private void C_ClearAllFilters_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.RemoveSectionFilters();
        }

        private void C_EditFilters_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.EditFilters();

        }
        protected void EditFilters()
        {
            WebbReport m_Report = this.C_ReportDesignPanel.Report as WebbReport;

            if (m_Report == null) return;

            if (this.C_FilterList.SelectedNode != null)
            {
                SectionFilter scFilter = this.C_FilterList.SelectedNode.Tag as SectionFilter;

                if (scFilter == null) return;

                DBFilter filter = scFilter.Filter.Copy();

                Editors.FilterEditForm filterEditForm = new Editors.FilterEditForm(filter);

                if (filter.Name == string.Empty)
                {
                    filter.IsCustomFilter = true;
                }
                else if (!filter.IsCustomFilter)
                {
                    filterEditForm.NoEdit = true;

                    //filterEditForm.NoImportOld = true;
                }
                else
                {
                    filterEditForm.NoEdit = false;
                    //filterEditForm.NoImportOld = false;
                }

                if (filterEditForm.ShowDialog() == DialogResult.OK)
                {
                    DBFilter formFilter = filterEditForm.Value.Copy();

                    if (DBFilter.Equals(filter, formFilter)) return;

                    if (m_Report.Template.ReportScType != ReportScType.Custom)
                    {
                        Webb.Utilities.MessageBoxEx.ShowError("Unable to to edit this Non-Custom advantage Sectionfilters!\n\nYou could only edit it in Advantage/WebbGameDay/Playmaker", "Failed");

                        return;

                    }


                    scFilter.Filter = formFilter;

                    if (scFilter.Filter.Name != string.Empty)
                    {
                        scFilter.FilterName = scFilter.Filter.Name;

                        this.C_FilterList.SelectedNode.Text = scFilter.FilterName;

                    }
                }
                SectionFilterCollection sectionFilter = new SectionFilterCollection();

                foreach (TreeNode node in this.C_FilterList.Nodes)
                {
                    sectionFilter.Add(node.Tag as SectionFilter);
                }

                m_Report.Template.SectionFilters = sectionFilter;

                this._DataSource.SectionFilters = sectionFilter.Copy();


                this.UpdateExControls(DataSourceStatus.Update);
            }

        }

        protected void RemoveSectionFilters()
        {

            this.C_FilterList.Nodes.Clear();

            if (this.C_ReportDesignPanel.Report is WebbReport)
            {
                WebbReport report = this.C_ReportDesignPanel.Report as WebbReport;

                report.Template.SectionFilters.Clear();

                report.Template.ReportScType = ReportScType.Custom;

                this.CheckItem(ReportScType.Custom);
            }

            if (this._DataSource != null && this._DataSource.SectionFilters != null)
            {
                this._DataSource.SectionFilters.Clear();
            }
        }

        private bool CheckedEmptyDataSource()
        {
            IDesignerHost m_host = this.C_ReportDesignPanel.GetService(typeof(IDesignerHost)) as IDesignerHost;

            if (m_host == null) return true;

            if (m_host.Container.Components[Webb.Utility.WebbDataSource] != null)	//has data source
            {
                return false;
            }

            return true;
        }

        //Section filter menu
        private void C_FilterList_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (this.C_ReportDesignPanel.Report == null) return;

            if (e.Button == MouseButtons.Right)
            {
                IDesignerHost m_host = this.C_ReportDesignPanel.GetService(typeof(IDesignerHost)) as IDesignerHost;

                if (m_host == null) return;

                if (m_host.Container.Components[Webb.Utility.WebbDataSource] != null)	//has data source
                {
                    this.C_LoadSectionFilters.Enabled = true;
                }
                else
                {
                    this.C_LoadSectionFilters.Enabled = false;
                }

                if (this.C_FilterList.Nodes.Count <= 0)	//no section filter
                {
                    this.C_EditFilters.Enabled = false;

                    this.C_ClearAllFilters.Enabled = false;

                    this.C_RemoveFilters.Enabled = false;
                }
                else
                {
                    this.C_EditFilters.Enabled = true;  //2009-8-21 12:38:57@Simon Add this Code

                    this.C_ClearAllFilters.Enabled = true;

                    this.C_RemoveFilters.Enabled = true;
                }
                this.C_FilterListMenu.ShowPopup(Control.MousePosition);
            }
        }

        //Data source menu
        private void FieldList_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.C_ReportDesignPanel.Report == null) return;

            if (e.Button == MouseButtons.Right)
            {
                IDesignerHost m_host = this.C_ReportDesignPanel.GetService(typeof(IDesignerHost)) as IDesignerHost;

                if (m_host == null) return;

                if (m_host.Container.Components[Webb.Utility.WebbDataSource] != null)	//has data source
                {
                    this.C_OpenDataSource.Enabled = false;
                    this.C_RemoveDataSource.Enabled = true;
                    this.C_PreviewDataSource.Enabled = true;
                    this.C_LoadSectionFilters.Enabled = true;
                    this.C_LoadDataConfig.Enabled = false;    //Added this code at 2009-2-4 13:15:11@Simon
                    this.C_SaveDataConfig.Enabled = true;
                }
                else
                {
                    this.C_OpenDataSource.Enabled = true;
                    this.C_RemoveDataSource.Enabled = false;
                    this.C_PreviewDataSource.Enabled = false;
                    this.C_LoadSectionFilters.Enabled = false;
                    this.C_LoadDataConfig.Enabled = true;
                    this.C_SaveDataConfig.Enabled = false;
                }
                this.C_FiledListMenu.ShowPopup(Control.MousePosition);
            }
        }

        private void LoadDataConfig()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            string fileName = Webb.Utility.CurFileName;

            fileDialog.Filter = "Data Source Files(*.inw,*.wrdf)|*.inw;*.wrdf";

            try
            {
                if (fileName != null && fileName.Length > 0)
                {
                    string s = System.IO.Path.GetDirectoryName(fileName);

                    if (s.Length > 0) fileDialog.InitialDirectory = s;
                }
            }
            catch { }

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string filename = fileDialog.FileName;

                WebbDataSource dataSourse = null;

                Webb.Utilities.WaitingForm.ShowWaitingForm();

                if (filename.EndsWith(".wrdf"))
                {
                    dataSourse = WrdfFileManager.ReadDataConfig(filename, Webb.Utility.CurFileName);

                    CanDebugWithDataFile = false;
                }
                else if (filename.EndsWith(".inw"))
                {
                    string[] args = InwManager.ReadInwFile(fileDialog.FileName);

                    dataSourse = InwManager.CreateDataSourse(args, true);

                    CanDebugWithDataFile = true;
                }

                if (dataSourse != null)
                {
                    bool LoadOK = this.LoadDataSource(dataSourse);

                    Webb.Utilities.WaitingForm.CloseWaitingForm();

                    if (!LoadOK)
                    {
                        Webb.Utilities.MessageBoxEx.ShowError("Load data source error. Please concat Webb for help!");
                    }

                }
                else
                {
                    Webb.Utilities.WaitingForm.CloseWaitingForm();
                }

            }
        }

        private void SaveDataConfig()
        {
            SaveFileDialog fileDialog = new SaveFileDialog();

            string filename = Webb.Utility.CurFileName.Replace("repx", "wrdf");

            if (Webb.Utility.CurFileName.EndsWith(".repw"))
            {
                filename = Webb.Utility.CurFileName.Replace(".repw", ".inw");
            }

            fileDialog.DefaultExt = "inw";

            fileDialog.Filter = "Data Source Template Files(*.inw)|*.inw|WebbReport DataFile(*.wrdf)|*.wrdf";

            if (filename != string.Empty)
            {
                filename = filename.Replace(".wrdf", "");
                filename = filename.Replace(".inw", "");
                fileDialog.FileName = filename;
            }
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                Webb.Utilities.WaitingForm.ShowWaitingForm();

                Webb.Utilities.WaitingForm.SetWaitingMessage("Saving data, please wait......");

                filename = fileDialog.FileName;

                if (filename.EndsWith(".wrdf"))
                {
                    WrdfFileManager.WriteDataConfig(filename, Webb.Utility.CurFileName);
                }
                else if (filename.EndsWith(".inw"))
                {
                    InwManager.WriteInwFile(filename, Webb.Utility.CurFileName);
                }

                Webb.Utilities.WaitingForm.CloseWaitingForm();

            }
        }

        protected void LoadDataSource()
        {
            //Show wizard
            DialogResult m_Result = this.DataProvider.ShowWizard(this, this._DataSource);

            if (m_Result != DialogResult.OK) return;

            //Get data source
            Webb.Utilities.WaitingForm.ShowWaitingForm();

            bool m_result = this.DataProvider.GetDataSource(this._DataSource);

            //Add data source to Report
            if (m_result && this._DataSource != null)
            {
                if (this._DataSource.DataSource == null || this._DataSource.DataSource.Tables.Count == 0) return;

                Webb.Reports.DataProvider.VideoPlayBackManager.PublicDBProvider = this.DataProvider;

                Webb.Reports.DataProvider.VideoPlayBackManager.LoadAdvScFilters();	//Modified at 2009-1-19 13:48:30@Scott

                Webb.Reports.DataProvider.VideoPlayBackManager.ReadPictureDirFromRegistry();

                Webb.Utilities.WaitingForm.CloseWaitingForm();

                DataProvider.UpdateEFFDataSource(this._DataSource);

                Webb.Reports.DataProvider.VideoPlayBackManager.DataSource = this._DataSource.DataSource;	//Set dataset for click event

                IDesignerHost m_host = this.C_ReportDesignPanel.GetService(typeof(IDesignerHost)) as IDesignerHost;

                if (m_host.Container.Components[Webb.Utility.WebbDataAdapter] == null && this._DataSource.DataAdapter != null)
                {//Add data adapter
                    DevExpress.XtraReports.Design.ReportDesigner.ForceAddToContainer(m_host.Container, this._DataSource.DataAdapter as IComponent, Webb.Utility.WebbDataAdapter);
                }
                if (m_host.Container.Components[Webb.Utility.WebbDataSource] == null)
                {//Add dataset
                    DevExpress.XtraReports.Design.ReportDesigner.ForceAddToContainer(m_host.Container, this._DataSource.DataSource as IComponent, Webb.Utility.WebbDataSource);
                }

                WebbReport report = this.C_ReportDesignPanel.Report as WebbReport;

                if (report != null && DataProvider.DBSourceConfig != null)
                {
                    report.LoadAdvSectionFilters(DataProvider.DBSourceConfig.UserFolder);

                    this.AddFilters(report.Template.SectionFilters);
                }


                ArrayList m_Fields = new ArrayList();

                foreach (System.Data.DataColumn m_col in this._DataSource.DataSource.Tables[0].Columns)
                {
                    if (m_col.Caption == "{EXTENDCOLUMNS}" && m_col.ColumnName.StartsWith("C_")) continue;

                    m_Fields.Add(m_col.ColumnName);
                }

                Webb.Data.PublicDBFieldConverter.SetAvailableFields(m_Fields);

                this.UpdateExControls(DataSourceStatus.AddNew);

                CanDebugWithDataFile = true;
            }
            else
            {
                Webb.Utilities.WaitingForm.CloseWaitingForm();

                Webb.Utilities.MessageBoxEx.ShowError("Load data source error. Please concat Webb for help!");
            }
        }


        #region Modify codes at 2009-2-3 9:32:38@Simon
        public bool LoadDataSource(WebbDataSource dataSourse)
        {
            if (dataSourse.DataSource == null || dataSourse.DataSource.Tables.Count == 0) return true;

            try
            {
                Webb.Reports.DataProvider.VideoPlayBackManager.LoadAdvScFilters();	//Modified at 2009-2-1 13:48:30@Simon	

                Webb.Reports.DataProvider.VideoPlayBackManager.ReadPictureDirFromRegistry();

                WebbReport report = this.C_ReportDesignPanel.Report as WebbReport;

                Webb.Reports.DataProvider.WebbDataProvider dataProvider = Webb.Reports.DataProvider.VideoPlayBackManager.PublicDBProvider;

                if (dataProvider != null && dataProvider.DBSourceConfig != null)
                {
                    report.LoadAdvSectionFilters(dataProvider.DBSourceConfig.UserFolder);

                    this.AddFilters(report.Template.SectionFilters);

                    dataProvider.UpdateEFFDataSource(dataSourse);
                }


                Webb.Reports.DataProvider.VideoPlayBackManager.DataSource = dataSourse.DataSource;	//Set dataset for click event

                IDesignerHost m_host = this.C_ReportDesignPanel.GetService(typeof(IDesignerHost)) as IDesignerHost;

                if (m_host.Container.Components[Webb.Utility.WebbDataAdapter] == null && dataSourse.DataAdapter != null)
                {//Add data adapter
                    DevExpress.XtraReports.Design.ReportDesigner.ForceAddToContainer(m_host.Container, dataSourse.DataAdapter as IComponent, Webb.Utility.WebbDataAdapter);
                }
                if (m_host.Container.Components[Webb.Utility.WebbDataSource] == null)
                {//Add dataset
                    DevExpress.XtraReports.Design.ReportDesigner.ForceAddToContainer(m_host.Container, dataSourse.DataSource as IComponent, Webb.Utility.WebbDataSource);
                }

                ArrayList m_Fields = new ArrayList();

                foreach (System.Data.DataColumn m_col in dataSourse.DataSource.Tables[0].Columns)
                {
                    if (m_col.Caption == "{EXTENDCOLUMNS}" && m_col.ColumnName.StartsWith("C_")) continue;

                    m_Fields.Add(m_col.ColumnName);
                }

                Webb.Data.PublicDBFieldConverter.SetAvailableFields(m_Fields);

                this.UpdateExControls(DataSourceStatus.AddNew);

                report.SetDataSource(dataSourse);

                this._DataSource = dataSourse;

                return true;

            }
            catch
            {
                RemoveDataSource();

                return false;
            }
        }
        #endregion        //End Modify

        public void RemoveDataSource()
        {
            this.RemoveComponent(typeof(System.Data.DataSet));

            this.RemoveComponent(typeof(System.Data.IDataAdapter));

            //this.RemoveSectionFilters();

            Webb.Reports.DataProvider.VideoPlayBackManager.DataSource = null;

            this.UpdateExControls(DataSourceStatus.Remove);

            Webb.Data.PublicDBFieldConverter.AvialableFields.Clear();

            Webb.Data.PublicDBFieldConverter.AvialableFields.Add(string.Empty);

        }
        #endregion

        #region private void C_ReportDesignPanel_DesignerHostLoaded(object sender, DevExpress.XtraReports.UserDesigner.DesignerLoadedEventArgs e)
        //Wu.Country@2007-12-04 16:01 added this region.
        /// <summary>
        /// Load ExControls for the report designer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void C_ReportDesignPanel_DesignerHostLoaded(object sender, DevExpress.XtraReports.UserDesigner.DesignerLoadedEventArgs e)
        {
            IToolboxService m_ToolboxService = (IToolboxService)e.DesignerHost.GetService(typeof(IToolboxService));
            //Add new tools
            //System.Reflection.Assembly m_Assembly = System.Reflection.Assembly.Load("Webb.Reports.ExControls");
            //Load Label cotnrol
            Type m_type = LoadConfigType("Webb.Reports.ExControls", "Webb.Reports.ExControls.LabelControl");
            if (m_type != null)
            {
                ToolboxItem m_LabelControl = new ToolboxItem(m_type);
                m_LabelControl.DisplayName = "Label";
                m_ToolboxService.AddToolboxItem(m_LabelControl, "Extended Controls");
            }

            //Load DataTime control
            m_type = LoadConfigType("Webb.Reports.ExControls", "Webb.Reports.ExControls.DateTimeControl");
            if (m_type != null)
            {
                ToolboxItem m_DateTimeControl = new ToolboxItem(m_type);
                m_DateTimeControl.DisplayName = "Date";
                m_ToolboxService.AddToolboxItem(m_DateTimeControl, "Extended Controls");
            }
            //Load Version cotnrol
            m_type = LoadConfigType("Webb.Reports.ExControls", "Webb.Reports.ExControls.VersionControl");
            if (m_type != null)
            {
                ToolboxItem m_VersionControl = new ToolboxItem(m_type);
                m_VersionControl.DisplayName = "Version";
                m_ToolboxService.AddToolboxItem(m_VersionControl, "Extended Controls");
            }
            //Load FileName cotnrol
            m_type = LoadConfigType("Webb.Reports.ExControls", "Webb.Reports.ExControls.FileNameControl");
            if (m_type != null)
            {
                ToolboxItem m_FileNameControl = new ToolboxItem(m_type);
                m_FileNameControl.DisplayName = "FileName";
                m_ToolboxService.AddToolboxItem(m_FileNameControl, "Extended Controls");
            }
            //Load ReportInfoLabel control
            m_type = LoadConfigType("Webb.Reports.ExControls", "Webb.Reports.ExControls.ReportInfoLabel");
            if (m_type != null)
            {
                ToolboxItem m_ReportInfoLabel = new ToolboxItem(m_type);
                m_ReportInfoLabel.DisplayName = "Games List Info";
                m_ToolboxService.AddToolboxItem(m_ReportInfoLabel, "Extended Controls");
            }
            //Load Image control
            m_type = LoadConfigType("Webb.Reports.ExControls", "Webb.Reports.ExControls.CustomImage");
            if (m_type != null)
            {
                ToolboxItem m_Image = new ToolboxItem(m_type);
                m_Image.DisplayName = "Image";
                m_ToolboxService.AddToolboxItem(m_Image, "Extended Controls");
            }
            //Load Grid control
            m_type = LoadConfigType("Webb.Reports.ExControls", "Webb.Reports.ExControls.GridControl");
            if (m_type != null)
            {
                ToolboxItem m_GridControl = new ToolboxItem(m_type);
                m_GridControl.DisplayName = "Grid Control";
                m_ToolboxService.AddToolboxItem(m_GridControl, "Extended Controls");
            }

            //Load Simple Grouping control
            //			m_type = this.LoadConfigType("Webb.Reports.ExControls","Webb.Reports.ExControls.SimpleGroupingControl");
            //			if(m_type!=null)
            //			{
            //				ToolboxItem m_SimpleGroupControl = new ToolboxItem(m_type);
            //				m_SimpleGroupControl.DisplayName = "Simple Group Control";
            //				m_ToolboxService.AddToolboxItem(m_SimpleGroupControl,"Extended Controls");
            //			}
            //Load Grouping control
            m_type = LoadConfigType("Webb.Reports.ExControls", "Webb.Reports.ExControls.GroupingControl");
            if (m_type != null)
            {
                ToolboxItem m_GroupControl = new ToolboxItem(m_type);
                m_GroupControl.DisplayName = "Group Control";
                m_ToolboxService.AddToolboxItem(m_GroupControl, "Extended Controls");
            }
            //Load Compact Grouping control
            m_type = LoadConfigType("Webb.Reports.ExControls", "Webb.Reports.ExControls.CompactGroupingControl");
            if (m_type != null)
            {
                ToolboxItem m_CompactGroupControl = new ToolboxItem(m_type);
                m_CompactGroupControl.DisplayName = "CompactGroup Control";
                m_ToolboxService.AddToolboxItem(m_CompactGroupControl, "Extended Controls");
            }
            //Load Hit Chart control
            m_type = LoadConfigType("Webb.Reports.ExControls", "Webb.Reports.ExControls.FieldPanel");
            if (m_type != null)
            {
                ToolboxItem m_FieldPanel = new ToolboxItem(m_type);
                m_FieldPanel.DisplayName = "Hit Chart Control";
                m_ToolboxService.AddToolboxItem(m_FieldPanel, "Extended Controls");
            }

            m_type = LoadConfigType("Webb.Reports.ExControls", "Webb.Reports.ExControls.GradingControl");
            if (m_type != null)
            {
                ToolboxItem m_GradingControl = new ToolboxItem(m_type);
                m_GradingControl.DisplayName = "Grading Control";
                m_ToolboxService.AddToolboxItem(m_GradingControl, "Extended Controls");
            }

            //Load Compact Grouping control
            m_type = LoadConfigType("Webb.Reports.ExControls", "Webb.Reports.ExControls.MatrixGroupControl");
            if (m_type != null)
            {
                ToolboxItem m_MatrixControl = new ToolboxItem(m_type);
                m_MatrixControl.DisplayName = "MatrixGroup Control";
                m_ToolboxService.AddToolboxItem(m_MatrixControl, "Extended Controls");
            }
            //Load Hit Chart Ex control
            //			m_type = this.LoadConfigType("Webb.Reports.ExControls","Webb.Reports.ExControls.HitChartPanel");
            //			if(m_type!=null)
            //			{
            //				ToolboxItem m_HitChartPanel = new ToolboxItem(m_type);
            //				m_HitChartPanel.DisplayName = "Hit Chart Ex Control";
            //				m_ToolboxService.AddToolboxItem(m_HitChartPanel,"Extended Controls");
            //			}			
            //Load Stat control
            m_type = LoadConfigType("Webb.Reports.ExControls", "Webb.Reports.ExControls.StatControl");
            if (m_type != null)
            {
                ToolboxItem m_StatControl = new ToolboxItem(m_type);
                m_StatControl.DisplayName = "Statistical Control";
                m_ToolboxService.AddToolboxItem(m_StatControl, "Extended Controls");
            }
            //			//Load Chart control
            //			m_type = LoadConfigType("Webb.Reports.ExControls","Webb.Reports.ExControls.ChartControl");
            //			if(m_type!=null)
            //			{
            //				ToolboxItem m_ChartControl = new ToolboxItem(m_type);
            //				m_ChartControl.DisplayName = "Flat Chart Control";
            //				m_ToolboxService.AddToolboxItem(m_ChartControl,"Extended Controls");
            //			}
            //Load Chart control Ex	//07-30-2008@Scott
            m_type = LoadConfigType("Webb.Reports.ExControls", "Webb.Reports.ExControls.ChartControlEx");
            if (m_type != null)
            {
                ToolboxItem m_ChartControlEx = new ToolboxItem(m_type);
                m_ChartControlEx.DisplayName = "Chart Control";
                m_ToolboxService.AddToolboxItem(m_ChartControlEx, "Extended Controls");
            }
            //Load Shape control //07-17-2008@Simon
            m_type = LoadConfigType("Webb.Reports.ExControls", "Webb.Reports.ExControls.ExShapeControl");
            if (m_type != null)
            {
                ToolboxItem m_ShapeControl = new ToolboxItem(m_type);
                m_ShapeControl.DisplayName = "Shape Control";
                m_ToolboxService.AddToolboxItem(m_ShapeControl, "Extended Controls");
            }
            m_type = LoadConfigType("Webb.Reports.ExControls", "Webb.Reports.ExControls.MaskedTextControl");
            if (m_type != null)
            {
                ToolboxItem m_MaskedTextControl = new ToolboxItem(m_type);
                m_MaskedTextControl.DisplayName = "MaskedText Control";
                m_ToolboxService.AddToolboxItem(m_MaskedTextControl, "Extended Controls");
            }
            //Load Horizontal Grouping control  10-11-2011 Scott
            m_type = LoadConfigType("Webb.Reports.ExControls", "Webb.Reports.ExControls.HorizontalGridControl");
            if (m_type != null)
            {
                ToolboxItem m_HorizontalGridControl = new ToolboxItem(m_type);
                m_HorizontalGridControl.DisplayName = "HorizontalGrid Control";
                m_ToolboxService.AddToolboxItem(m_HorizontalGridControl, "Extended Controls");
            }
            //Load Horizontal Grouping control
            m_type = LoadConfigType("Webb.Reports.ExControls", "Webb.Reports.ExControls.HorizonGroupControl");
            if (m_type != null)
            {
                ToolboxItem m_HorizonGroupControl = new ToolboxItem(m_type);
                m_HorizonGroupControl.DisplayName = "HorizontalGroup Control";
                m_ToolboxService.AddToolboxItem(m_HorizonGroupControl, "Extended Controls");
            }

            //Load containerControl by brian
            m_type = LoadConfigType("Webb.Reports.ExControls", "Webb.Reports.ExControls.ContainerControl");
            if (m_type != null)
            {
                ToolboxItem m_ContainerControl = new ToolboxItem(m_type);
                m_ContainerControl.DisplayName = "Simple Container";
                m_ToolboxService.AddToolboxItem(m_ContainerControl, "Extended Controls");
            }

            //Load RepeatControl by brian
            m_type = LoadConfigType("Webb.Reports.ExControls", "Webb.Reports.ExControls.RepeatControl");
            if (m_type != null)
            {
                ToolboxItem m_RepeatControl = new ToolboxItem(m_type);
                m_RepeatControl.DisplayName = "SubReport Control";
                m_ToolboxService.AddToolboxItem(m_RepeatControl, "Extended Controls");
            }


            #region Modify codes at 2009-4-10 10:25:49@Simon

            //Load Standard Controls
            if (m_ToolboxService is DevExpress.XtraReports.UserDesigner.Native.XRToolboxService)
            {
                DevExpress.XtraReports.UserDesigner.Native.XRToolboxService toolService = m_ToolboxService as DevExpress.XtraReports.UserDesigner.Native.XRToolboxService;

                toolService.RemoveCategory("Standard Controls");

                toolService.AddStandardCategory();

                ToolboxItemCollection m_toolboxItems = toolService.GetToolboxItems("Standard Controls");

                ArrayList keepToolItems = new ArrayList();

                keepToolItems.Add("XRLabel");
                keepToolItems.Add("XRPanel");
                keepToolItems.Add("XRPageInfo");

                foreach (ToolboxItem toolboxItem in m_toolboxItems)
                {
                    if (keepToolItems.Contains(toolboxItem.DisplayName)) continue;
                    toolService.RemoveToolboxItem(toolboxItem);
                }

            }

            #endregion        //End Modify

            //			//Load PivotGridReport control
            //			m_type = this.LoadConfigType("Webb.Reports.ExControls","Webb.Reports.ExControls.PivotGridReport");
            //			if(m_type!=null)
            //			{
            //				ToolboxItem m_PivotReport = new ToolboxItem(m_type);
            //				m_PivotReport.DisplayName = "Pivot Control";
            //				m_ToolboxService.AddToolboxItem(m_PivotReport,"Standard Controls");
            //			}
            //			//Load GridReport control
            //			m_type = this.LoadConfigType("Webb.Reports.ExControls","Webb.Reports.ExControls.GridReport");
            //			if(m_type!=null)
            //			{
            //				ToolboxItem m_GridReport = new ToolboxItem(m_type);
            //				m_GridReport.DisplayName = "Grid Report";
            //				m_ToolboxService.AddToolboxItem(m_GridReport,"Standard Controls");
            //			}
        }

        #endregion

        #region File_ItemClick
        #region Debug
        private void RunBrowser()
        {
            try
            {
                if (Webb.Utility.CurFileName == string.Empty || !(Webb.Utility.CurFileName.EndsWith(".repx") || Webb.Utility.CurFileName.EndsWith(".repw")))
                {
                    MessageBox.Show("Bad report filename. please save you report at first!", "Failed To Debug Report", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                    return;
                }
                if (this._DataSource == null || this._DataSource.DataSource == null || this._DataSource.DataSource.Tables.Count == 0)
                {
                    MessageBox.Show("Failed to preview your current report,please load datasource first!", "Failed To Debug Report", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                if (!CanDebugWithDataFile)
                {
                    MessageBox.Show("Failed to debug the report in WRB with *.wrdf file\n(*.wrdf file could be only used in designer to debug remote data and report)!", "Failed To Debug Report", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                    return;
                }

                this.C_ReportDesignPanel.SaveReport();

                this.C_ReportDesignPanel.ReportState = ReportState.Saved;

                string filename = Webb.Utility.CurFileName.Replace("repx", "inw");

                if (Webb.Utility.CurFileName.EndsWith(".repw"))
                {
                    filename = Webb.Utility.CurFileName.Replace("repw", "inw");
                }

                if (filename == string.Empty) return;

                InwManager.WriteInwFile(filename, Webb.Utility.CurFileName);

                string myExePath = Webb.Utility.ApplicationDirectory + "WebbRepBrowser.exe";

                if (!File.Exists(myExePath))
                {
                    int index = Application.StartupPath.LastIndexOf("\\");

                    myExePath = myExePath.Substring(0, index) + "\\Browser\\WebbRepBrowser.exe";
                }

                BrowserProcess.StartInfo.FileName = myExePath;

                BrowserProcess.StartInfo.Arguments = "\"" + filename + "\"";

                BrowserProcess.Start();

                barButtonRun.Enabled = false;
                barButtonStop.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void StopBrowser()
        {
            try
            {
                BrowserProcess.Kill();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void barButtonRun_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            RunBrowser();
        }

        private void barButtonStop_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            StopBrowser();
        }
        #endregion

        private void About_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (Webb.Utilities.ProductAboutForm aboutForm = new Webb.Utilities.ProductAboutForm())
            {
                aboutForm.ShowDialog(this);
            }
        }

        private void NewReport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            e.Handled = true;

            DialogResult m_result = this.C_ReportDesignPanel.SaveChangedReport();

            if (m_result == DialogResult.Cancel)
            {
                return;
            }
            else
            {
                this.RemoveSectionFilters();

                this.C_ReportDesignPanel.OpenReport(GetDefaultReport());
            }

            this.RemoveDataSource();


            this.UpdateExControls(DataSourceStatus.Update);
        }
        private void NewReportWizard_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            e.Handled = true;
            this.ShowReportWizard();
        }

        private void OpenReportItem_Click(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.OpenWebbReport();
        }

        //06-13-2008@Scott Before save report , we need clear all data , to reduce template size
        private void Save_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            //			this.UpdateExControls(DataSourceStatus.Remove);
            this.SaveWebbReport(true);


        }

        //06-13-2008@Scott Before save report , we need clear all data , to reduce template size
        private void SaveAs_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.SaveWebbReportAs(true);
            //			this.UpdateExControls(DataSourceStatus.Remove);			
        }

        //04-28-2008@Scott
        private void OpenWebbReport()
        {
            System.Diagnostics.Debug.WriteLine("Open Webb Report");

            if (this.C_ReportDesignPanel.SaveChangedReport() == DialogResult.Cancel) return;

            OpenFileDialog fileDialog = new OpenFileDialog();

            fileDialog.Filter = "WebbReport Files(*.repx)|*.repx";

            try
            {
                string fileName = Webb.Utility.CurFileName;

                if (fileName != null && fileName.Length > 0)
                {
                    string s = System.IO.Path.GetDirectoryName(fileName);

                    if (s.Length > 0) fileDialog.InitialDirectory = s;
                }
            }
            catch { }

            #region Modify codes at 2009-2-3 9:44:54@Simon
            //for Keep DataSourse When open
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                WebbDataSource dataSourse = this._DataSource;

                bool HasDataSource = (dataSourse != null && !CheckedEmptyDataSource());

                Webb.Reports.DataProvider.WebbDataProvider dataProvider = Webb.Reports.DataProvider.VideoPlayBackManager.PublicDBProvider;

                this.OpenReport(fileDialog.FileName, false);

                this.UpdateExControls(DataSourceStatus.Remove);

                if (HasDataSource)
                {
                    bool LoadOK = this.LoadDataSource(dataSourse);

                    WebbReport report = this.C_ReportDesignPanel.Report as WebbReport;

                    SectionFilterCollection sectionFilter = report.Template.SectionFilters.Copy();

                    if (report.Template.ReportScType == ReportScType.Custom)
                    {
                        report.Template.SectionFilters = AdvFilterConvertor.GetCustomFilters(Webb.Reports.DataProvider.VideoPlayBackManager.AdvReportFilters, report.Template.SectionFilters);
                    }

                    this._DataSource.SectionFilters = sectionFilter.Copy();

                    this.AddFilters(this._DataSource.SectionFilters);

                    this.CheckItem(report.Template.ReportScType);
                }

                Webb.Reports.DataProvider.VideoPlayBackManager.PublicDBProvider = dataProvider;
            }
            fileDialog.Dispose();
            #endregion        //End Modify

            Webb.Utilities.WaitingForm.CloseWaitingForm();
        }
        protected virtual void ShowReportWizard()
        {


        }

        #endregion

        //Read this code at 2009-1-9 8:31:21@Simon
        private void C_ReportDesignPanel_ReportStateChanged(object sender, DevExpress.XtraReports.UserDesigner.ReportStateEventArgs e)
        {
            if (e.ReportState == ReportState.Saved)
            {
                //04-29-2008@Scott
                WebbReport report = this.C_ReportDesignPanel.Report as WebbReport;

                if (report == null) return;

                string strWebbReportTemplateFilePath = this.C_ReportDesignPanel.FileName.Replace(Webb.Utility.ReportExt, Webb.Utility.WebbReportExt);

                if (strWebbReportTemplateFilePath.EndsWith(Webb.Utility.WebbReportExt) && File.Exists(strWebbReportTemplateFilePath))
                {
                    File.Delete(strWebbReportTemplateFilePath);      //Delete WebbReport TemplateFile at 2009-2-4 8:39:46@Simon     
                }

                //				WebbReportTemplate template = new WebbReportTemplate();
                //
                //				report.UpdateTemplate(template);

                //				template.Save(strWebbReportTemplateFilePath);			

                Webb.Utility.CurFileName = this.C_ReportDesignPanel.FileName;	//06-23-2008@Scott

                bool emptyDataSource = this.CheckedEmptyDataSource();

                if (!emptyDataSource)
                {
                    Webb.Reports.ConfigFileManager.WriteDataConfig(this.C_ReportDesignPanel.FileName);  //Added this code at 2009-2-2 10:53:51@Simon
                }
            }

            if (e.ReportState == ReportState.Opened)
            {
                this.RemoveComponent(typeof(System.Data.DataSet));

                Webb.Utility.CurFileName = this.C_ReportDesignPanel.FileName;	//06-23-2008@Scott
            }

            if (e.ReportState == ReportState.Opened || e.ReportState == ReportState.Saved)	//Modified at 2009-1-19 14:58:03@Scott
            {
                this.UpdateExControls(DataSourceStatus.Update);
            }
        }

        private void C_ReportDesignPanel_GotFocus(object sender, EventArgs e)
        {
            Webb.Utility.CurFileName = this.C_ReportDesignPanel.FileName;	//12-12-2008@Scott
        }

        #region private WebbReport GetDefaultReport()
        protected WebbReport GetDefaultReport()
        {
            WebbReport m_Report = new WebbReport();

            m_Report.InitializeDefaultReport();

            m_Report.WebbDataSource = this._DataSource;

            this.C_ReportDesignPanel.FileName = string.Empty;

            return m_Report;
        }
        #endregion

        #region private Type LoadConfigType(string i_Assembly,string i_Type)
        public static Type LoadConfigType(string i_Assembly, string i_Type)
        {
            try
            {
                System.Reflection.Assembly m_Assembly = System.Reflection.Assembly.Load(i_Assembly);

                return m_Assembly.GetType(i_Type);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        public void OpenReoprt(WebbReport i_Report, String strFileName, bool bNeedActivateHost)
        {
            Webb.Utilities.WaitingForm.SetWaitingMessage("Open report...");

            this.C_ReportDesignPanel.OpenReport(i_Report);

            this.C_ReportDesignPanel.FileName = strFileName;	//06-23-2008@Scott

            Webb.Utility.CurFileName = strFileName;

            if (bNeedActivateHost)
            {
                Webb.Utilities.WaitingForm.SetWaitingMessage("Activate report...");	//Modified at 2009-1-19 15:16:26@Scott

                this.C_ReportDesignPanel.ActivateHost(i_Report);	//Modified at 2009-1-19 15:10:19@Scott
            }
        }


        protected void RemoveComponent(Type type)
        {
            IDesignerHost m_host = this.C_ReportDesignPanel.GetService(typeof(IDesignerHost)) as IDesignerHost;

            if (m_host == null) return;

            foreach (IComponent component in m_host.Container.Components)
            {
                Type t = component.GetType();

                if (t.BaseType == type || t == type)
                {
                    DevExpress.XtraReports.Design.ReportDesigner.RemoveFromContainer(m_host, component);
                }
            }
        }

        public void OpenReport(string i_Path, bool bNeedActivateHost)
        {//tag
            try
            {
                Webb.Utilities.WaitingForm.ShowWaitingForm();

                Webb.Utilities.WaitingForm.SetWaitingMessage("Create report...");

                WebbReport m_report = new WebbReport();

                Webb.Utilities.WaitingForm.SetWaitingMessage("Load layout...");

                // 02-01-2012 Scott
                //FileStream fs = new FileStream(i_Path, FileMode.Open);

                //m_report = WebbReport.FromStream(fs, true) as WebbReport;

                m_report.LoadLayout(i_Path);

                // end

                this.OpenReoprt(m_report, i_Path, bNeedActivateHost);

                #region Old Codes

                //this.RemoveSectionFilters();

                //				//04-29-2008@scott
                //				string strWebbReportTemplateFilePath = i_Path.Replace(Webb.Utility.ReportExt,Webb.Utility.WebbReportExt);
                //
                //				if(System.IO.File.Exists(strWebbReportTemplateFilePath))
                //				{
                //					WebbReportTemplate template = new WebbReportTemplate();
                //
                //					template.Load(strWebbReportTemplateFilePath);
                //
                //					if(this.C_ReportDesignPanel.Report is WebbReport)
                //					{
                //						WebbReport report = this.C_ReportDesignPanel.Report as WebbReport;
                //
                //						report.ApplyTemplate(template);
                //
                //						this._DataSource.SectionFilters = report.Template.SectionFilters.Copy();
                //
                //						this.AddFilters(this._DataSource.SectionFilters);
                //
                //						this.CheckItem(report.Template.ReportScType);
                //					}
                //				}
                #endregion
                #region new codes
                if (this.C_ReportDesignPanel.Report is WebbReport)
                {
                    WebbReport report = this.C_ReportDesignPanel.Report as WebbReport;

                    SectionFilterCollection sectionFilter = report.Template.SectionFilters.Copy();

                    if (report.Template.ReportScType == ReportScType.Custom)
                    {
                        report.Template.SectionFilters = AdvFilterConvertor.GetCustomFilters(Webb.Reports.DataProvider.VideoPlayBackManager.AdvReportFilters, report.Template.SectionFilters);
                    }

                    this._DataSource.SectionFilters = sectionFilter.Copy();

                    this.AddFilters(this._DataSource.SectionFilters);

                    this.CheckItem(report.Template.ReportScType);
                }
                #endregion
            }
            catch (Exception ex)
            {
                Webb.Utilities.WaitingForm.CloseWaitingForm();

                Webb.Utilities.TopMostMessageBox.ShowMessage("Error", string.Format("Load Report Failed : {0}", ex.Message), MessageBoxButtons.OK);
            }
            this.RemoveComponent(typeof(System.Data.DataSet));

            this.UpdateExControls(DataSourceStatus.Update);
        }


        public WebbReport OpenAnalyReport(string strFileName)
        {//tag	

            WebbReport i_Report = new WebbReport();

            i_Report.LoadLayout(strFileName);

            try
            {
                this.C_ReportDesignPanel.OpenReport(i_Report);

                this.C_ReportDesignPanel.FileName = strFileName;	//06-23-2008@Scott

                Webb.Utility.CurFileName = strFileName;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            this.RemoveComponent(typeof(System.Data.DataSet));

            this.UpdateExControls(DataSourceStatus.Update);

            return i_Report;
        }

        public void CheckItem(ReportScType reportScType)
        {
            this.C_CheckCustom.Checked = false;
            this.C_CheckDDD.Checked = false;
            this.C_CheckDDO.Checked = false;
            this.C_CheckZoneD.Checked = false;
            this.C_CheckZoneO.Checked = false;

            switch (reportScType)
            {
                case ReportScType.Custom:
                    this.C_CheckCustom.Checked = true;
                    break;
                case ReportScType.DownAndDistanceDefense:
                    this.C_CheckDDD.Checked = true;
                    break;
                case ReportScType.DownAndDistanceOffense:
                    this.C_CheckDDO.Checked = true;
                    break;
                case ReportScType.FieldZoneDefense:
                    this.C_CheckZoneD.Checked = true;
                    break;
                case ReportScType.FieldZoneOffense:
                    this.C_CheckZoneO.Checked = true;
                    break;
            }
        }


        public void SaveWebbReportAs(bool _ReloadDataSource)
        {//tag

            Webb.Utilities.WaitingForm.ShowWaitingForm();

            Webb.Utilities.WaitingForm.SetWaitingMessage("Saving Layout......");

            Webb.Reports.DataProvider.WebbDataProvider dataProvider = Webb.Reports.DataProvider.VideoPlayBackManager.PublicDBProvider;

            bool emptyData = this.CheckedEmptyDataSource();

            WebbDataSource dataSource = this._DataSource;

            if (dataProvider != null && !emptyData)
            {
                Webb.Utilities.WaitingForm.SetWaitingMessage("Clear all data to reduce template size, please wait......");

                this.RemoveDataSource();   //2009-7-17 15:14:39@Simon Add this Code   
            }

            try
            {
                SaveFileDialog fileDialog = new SaveFileDialog();

                string initFilename = Webb.Utility.CurFileName;

                Webb.Utility.InitFileDialog(fileDialog, initFilename);

                if (initFilename == null || initFilename == string.Empty || initFilename.EndsWith(".repw"))
                {
                    if (initFilename.EndsWith(".repw"))
                    {
                        initFilename = System.IO.Path.GetFileNameWithoutExtension(initFilename);

                        fileDialog.FileName = initFilename;

                    }

                }

                #region Modify codes at 2009-2-3 9:44:54@Simon
                //for Keep DataSourse When open
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    string i_Path = fileDialog.FileName;

                    string InvalidChars = Webb.Utility.SimpleInFileName.Replace(".", "");

                    if (i_Path == null || !i_Path.EndsWith(".repx") || i_Path.IndexOfAny(InvalidChars.ToCharArray()) >= 0)
                    {
                        MessageBox.Show("Bad filename to save this report!!!\r\nSuch chars(" + Webb.Utility.InvalidSignsInFileName + ") should be exclude in the report name!", "Failed to save", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                        Webb.Utilities.WaitingForm.CloseWaitingForm();

                        return;
                    }
                    else
                    {

                        WebbReport webbReport = this.GetReport();

                        bool CheckIsWizardFile = (i_Path == Webb.Utility.CurFileName && webbReport.Template.ReportWizardSetting != null && webbReport.Template.ReportWizardSetting.CreateByWizard);

                        CheckIsWizardFile = CheckIsWizardFile || Webb.Utility.IsCreatedByWizard(i_Path);

                        if (CheckIsWizardFile)
                        {
                            string strMessage = "                  Warinng:\n";
                            strMessage += "........................................................\n\n";
                            strMessage += "The report which you want to overwrite was created by 'Webb Report Wizard',\n\nif overwrite it,it could not be opened by 'WebbReport Wizard' again, continue?";

                            DialogResult dr = MessageBox.Show(strMessage,
                                "Waring", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);

                            if (dr == DialogResult.Yes)
                            {
                                webbReport.Template.ReportWizardSetting = null;

                            }
                            else
                            {
                                Webb.Utilities.WaitingForm.CloseWaitingForm();

                                return;
                            }

                        }
                        else
                        {
                            webbReport.Template.ReportWizardSetting = null;
                        }

                        Webb.Utilities.WaitingForm.SetWaitingMessage("Save report...");

                        this.C_ReportDesignPanel.FileName = i_Path;

                        this.C_ReportDesignPanel.SaveReportState();
                    }
                }
                if (!emptyData && _ReloadDataSource)
                {
                    this.ReloadDataSource(dataProvider, dataSource);

                    this.C_ReportDesignPanel.ReportState = ReportState.Saved;
                }

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to saved this report!\nException:" + ex.Message, "Failed", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                Webb.Utilities.WaitingForm.CloseWaitingForm();
            }
        }
        //		private void ReloadDataSource(Webb.Reports.DataProvider.WebbDataProvider dataProvider)
        //		{			
        //			Webb.Utilities.WaitingForm.SetWaitingMessage("Reloading DataSource......");
        //                
        //			WebbDataSource dataSourse=new WebbDataSource();
        //
        //			dataProvider.GetDataSource(dataSourse);
        //
        //			if(dataSourse!=null)
        //			{
        //				bool LoadOK=this.LoadDataSource(dataSourse);
        //
        //				if(!LoadOK)
        //				{
        //					Webb.Utilities.MessageBoxEx.ShowError("Load data source error. Please concat Webb for help!");
        //				}
        //				
        //				WebbReport report = this.C_ReportDesignPanel.Report as WebbReport;
        //
        //				SectionFilterCollection sectionFilter=report.Template.SectionFilters.Copy();
        //
        //				if(report.Template.ReportScType==ReportScType.Custom)
        //				{
        //					report.Template.SectionFilters=AdvFilterConvertor.GetCustomFilters(Webb.Reports.DataProvider.VideoPlayBackManager.AdvReportFilters,report.Template.SectionFilters);
        //				}			
        //				this._DataSource.SectionFilters =sectionFilter.Copy();	
        //						
        //				this.AddFilters(this._DataSource.SectionFilters);
        //
        //				this.CheckItem(report.Template.ReportScType);
        //			}		
        //					
        //		    Webb.Reports.DataProvider.VideoPlayBackManager.PublicDBProvider=dataProvider;
        //		}

        private void ReloadDataSource(Webb.Reports.DataProvider.WebbDataProvider dataProvider, WebbDataSource dataSourse)
        {
            Webb.Utilities.WaitingForm.SetWaitingMessage("Reloading DataSource......");

            if (dataSourse != null)
            {
                bool LoadOK = this.LoadDataSource(dataSourse);

                if (!LoadOK)
                {
                    Webb.Utilities.MessageBoxEx.ShowError("Load data source error. Please concat Webb for help!");
                }

                WebbReport report = this.C_ReportDesignPanel.Report as WebbReport;

                SectionFilterCollection sectionFilter = report.Template.SectionFilters.Copy();

                if (report.Template.ReportScType == ReportScType.Custom)
                {
                    report.Template.SectionFilters = AdvFilterConvertor.GetCustomFilters(Webb.Reports.DataProvider.VideoPlayBackManager.AdvReportFilters, report.Template.SectionFilters);
                }
                this._DataSource.SectionFilters = sectionFilter.Copy();

                this.AddFilters(this._DataSource.SectionFilters);

                this.CheckItem(report.Template.ReportScType);
            }

            Webb.Reports.DataProvider.VideoPlayBackManager.PublicDBProvider = dataProvider;
        }
        private void SaveWebbReport(bool _ReloadDataSource)
        {
            string filename = this.C_ReportDesignPanel.FileName;

            if (filename == null || filename == string.Empty || !filename.EndsWith(".repx"))
            {
                this.SaveWebbReportAs(_ReloadDataSource);

                return;
            }
            WebbReport webbReport = this.GetReport();

            if (webbReport.Template.ReportWizardSetting != null && webbReport.Template.ReportWizardSetting.CreateByWizard)
            {
                this.SaveWebbReportAs(_ReloadDataSource);

                return;
            }

            Webb.Utilities.WaitingForm.ShowWaitingForm();

            Webb.Utilities.WaitingForm.SetWaitingMessage("Saving Layout......");

            try
            {
                Webb.Reports.DataProvider.WebbDataProvider dataProvider = Webb.Reports.DataProvider.VideoPlayBackManager.PublicDBProvider;

                bool emptyData = this.CheckedEmptyDataSource();

                WebbDataSource dataSource = this._DataSource;

                if (dataProvider != null && !emptyData)
                {
                    Webb.Utilities.WaitingForm.SetWaitingMessage("Clear all data to reduce template size, please wait......");

                    this.RemoveDataSource();   //2009-7-17 15:14:39@Simon Add this Code

                    Webb.Utilities.WaitingForm.SetWaitingMessage("Saving Report......");
                }

                this.C_ReportDesignPanel.SaveReportState();

                if (!emptyData && _ReloadDataSource)
                {
                    this.ReloadDataSource(dataProvider, dataSource);

                    this.C_ReportDesignPanel.ReportState = ReportState.Saved;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to saved this report!\nException:" + ex.Message, "Failed", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                Webb.Utilities.WaitingForm.CloseWaitingForm();
            }




        }

        public WebbReport GetReport()
        {
            return this.C_ReportDesignPanel.Report as WebbReport;
        }

        #region static funcions
        //Wu.Country@2007-11-20 10:51 added this region.
        private static ReportDesignerBase M_BaseDesignerForm;
        public static void LoadReport(WebbReport i_Report)
        {
            if (M_BaseDesignerForm == null || M_BaseDesignerForm.IsDisposed)
            {
                M_BaseDesignerForm = new ReportDesignerBase();
            }
            M_BaseDesignerForm.OpenReoprt(i_Report, null, false);
            M_BaseDesignerForm.Show();
        }
        #endregion

        private void ReportDesignerBase_Load(object sender, System.EventArgs e)
        {

        }
        private void BrowserProcess_Exited(object sender, System.EventArgs e)
        {
            barButtonRun.Enabled = true;

            barButtonStop.Enabled = false;
        }


        protected void OpenDefReport(string Filename)
        {
            if (this.C_ReportDesignPanel.SaveChangedReport() == DialogResult.Cancel) return;

            WebbDataSource dataSourse = this._DataSource;

            Webb.Reports.DataProvider.WebbDataProvider dataProvider = Webb.Reports.DataProvider.VideoPlayBackManager.PublicDBProvider;

            this.OpenReport(Filename, false);

            this.UpdateExControls(DataSourceStatus.Remove);

            if (dataSourse != null)
            {
                bool LoadOK = this.LoadDataSource(dataSourse);

                if (!LoadOK)
                {
                    Webb.Utilities.MessageBoxEx.ShowError("Load data source error. Please concat Webb for help!");
                }

                WebbReport report = this.C_ReportDesignPanel.Report as WebbReport;

                this._DataSource.SectionFilters = report.Template.SectionFilters.Copy();

                this.AddFilters(this._DataSource.SectionFilters);

                this.CheckItem(report.Template.ReportScType);
            }

            Webb.Reports.DataProvider.VideoPlayBackManager.PublicDBProvider = dataProvider;
        }


        public enum DataSourceStatus
        {
            AddNew,
            Update,
            Remove,
        }

        private void C_FilterList_DoubleClick(object sender, EventArgs e)
        {
            this.EditFilters();
        }

        private void C_ReportDesignPanel_ReportSaveChanges(object sender, SavedChangeArgs e)
        {
            this.SaveWebbReport(false);
        }

    }
}

