

using System;
using System.Drawing;
using System.Collections;

using DevExpress.XtraPrinting.Localization;

namespace Webb.Reports
{	
	using System.Windows.Forms;
	using DevExpress.XtraEditors;
	using DevExpress.XtraPrinting.Drawing;
	
	
	public class SerilizeWatermarkForm : Form
	{
		#region inner classes
		public class DirectionModeItem
		{
			string text;
			DirectionMode directionMode;

			public DirectionMode DirectionMode { get { return directionMode; } 
			}
			public string Text { get { return text; }
			}
			public DirectionModeItem(DirectionMode directionMode, string text) 
			{
				this.directionMode = directionMode;
				this.text = text;
			}
		}
		public class ViewModeItem
		{
			ImageViewMode viewMode;
			string text;

			public ImageViewMode ViewMode { get { return viewMode; }
			}
			public string Text { get { return text; }
			}
			public ViewModeItem(ImageViewMode viewMode, string text) 
			{
				this.viewMode = viewMode;
				this.text = text;
			}
		}
		public class ImageAlignItem
		{
			string text;
			string alignment;

			public string Alignment { get { return alignment; }
			}
			public string Text { get { return text; }
			}
			public ImageAlignItem(string alignment, string text) 
			{
				this.alignment = alignment;
				this.text = text;
			}
		}
		private class MyPrintControl : DevExpress.XtraPrinting.Control.PrintControl
		{
			private DevExpress.XtraPrinting.PrintingSystem ps;
			public MyPrintControl() 
			{
				SetControlVisibility(new Control[] {vScrollBar, hPanel}, false);
				ps = new DevExpress.XtraPrinting.PrintingSystem();
				PrintingSystem = ps;
				fMinZoom = 0.00001f;
			}
			void SetControlVisibility(Control[] controls, bool visible) 
			{
				foreach(Control control in controls)
					control.Visible = visible;
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
		}
		#endregion 

		const string alignTop = "Top", alignMiddle = "Middle", alignBottom = "Bottom", alignLeft = "Left", alignCenter = "Center", alignRight = "Right";
		static string[] 
			alignList = new string[] { ToString(alignBottom, alignCenter), 
										 ToString(alignBottom, alignLeft),
										 ToString(alignBottom, alignRight),
										 ToString(alignMiddle, alignCenter),
										 ToString(alignMiddle, alignLeft),
										 ToString(alignMiddle, alignRight),
										 ToString(alignTop, alignCenter),
										 ToString(alignTop, alignLeft),
										 ToString(alignTop, alignRight)};
		private System.Windows.Forms.Label lbPosition;
		private DevExpress.XtraEditors.RadioGroup rgrpPageRange;
		private DevExpress.XtraEditors.RadioGroup rgrpZOrder;
		private DevExpress.XtraTab.XtraTabControl xtraTabControl;
		private DevExpress.XtraTab.XtraTabPage tpTextWaterMark;
		private DevExpress.XtraTab.XtraTabPage tpPictureWatermark;
		private DevExpress.XtraEditors.GroupControl grpBoxPageRange;
		private DevExpress.XtraEditors.GroupControl grpBoxZOrder;
		private DevExpress.XtraEditors.GroupControl grpBoxAlign;
		private DevExpress.XtraEditors.PanelControl panelControl1;
		private DevExpress.XtraEditors.HScrollBar scrBarImageTransparent;
		private DevExpress.XtraEditors.TextEdit teImageTransparentValue;
		private DevExpress.XtraEditors.GroupControl grpBoxTransparent;
		private DevExpress.XtraEditors.GroupControl grpBoxImageTransparent;
		static ContentAlignment[] 
			contentAlignList = new ContentAlignment[] {ContentAlignment.BottomCenter,
														  ContentAlignment.BottomLeft,
														  ContentAlignment.BottomRight,
														  ContentAlignment.MiddleCenter,
														  ContentAlignment.MiddleLeft,
														  ContentAlignment.MiddleRight,
														  ContentAlignment.TopCenter,
														  ContentAlignment.TopLeft,
														  ContentAlignment.TopRight};
		static string ToString(string vAlign, string hAlign) 
		{
			return String.Format("{0},{1}", hAlign, vAlign);
		}

		private DirectionModeItem[] dsDirectionMode;
		private ViewModeItem[] dsImageViewMode;
		private ImageAlignItem[] dsImageHAlign;
		private ImageAlignItem[] dsImageVAlign;
		private DevExpress.XtraEditors.CheckEdit chbTiling;
		private DevExpress.XtraEditors.CheckEdit chbBold;
		private System.ComponentModel.IContainer components = null;
		private DevExpress.XtraEditors.SimpleButton btnOK;
		private DevExpress.XtraEditors.TextEdit tePageRange;
		private System.Windows.Forms.Label lbPageRangeComment;
		private DevExpress.XtraEditors.LookUpEdit lkpImageView;
		private DevExpress.XtraEditors.SimpleButton btnSelectPicture;
		private DevExpress.XtraEditors.TextEdit teTransparentValue;
		private DevExpress.XtraEditors.ComboBoxEdit cmbWatermarkFontSize;
		private DevExpress.XtraEditors.ComboBoxEdit cmbWatermarkFont;
		private DevExpress.XtraEditors.ComboBoxEdit cmbWatermarkText;
		private DevExpress.XtraEditors.LookUpEdit lkpTextDirection;
		private System.Windows.Forms.Label lbLayout;
		private System.Windows.Forms.Label lbFontColor;
		private System.Windows.Forms.Label lbFontSize;
		private System.Windows.Forms.Label lbFont;
		private System.Windows.Forms.Label lbText;

		private Watermark watermark = null;
		private DevExpress.XtraEditors.CheckEdit chbItalic;
		private DevExpress.XtraEditors.LookUpEdit lkpImageHAlign;
		private DevExpress.XtraEditors.LookUpEdit lkpImageVAlign;
		private System.Windows.Forms.Label lbHorzAlign;
		private System.Windows.Forms.Label lbVertAlign;
		private MyPrintControl pc;
		private DevExpress.XtraEditors.SimpleButton btnCancel;
		private DevExpress.XtraEditors.SimpleButton btnClear;
		private System.Windows.Forms.Label lbPageRange;
		private DevExpress.XtraEditors.ColorEdit ceWatermarkColor;
		private DevExpress.XtraEditors.HScrollBar scrBarTransparent;
		private bool canSync = false;

		public Watermark Watermark { get { return watermark; } 
		}
		
		public SerilizeWatermarkForm() 
		{
			InitializeComponent();

			this.watermark = new Watermark();
			InitComboBoxes();
			LocalizeRadioGroups();
			
			canSync = true;

		}
		public new DialogResult ShowDialog() 
		{
			return this.ShowDialog();
		}
		protected override void Dispose(bool disposing) 
		{
			if( disposing ) 
			{
				if(components != null)
					components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code
		private void InitializeComponent() 
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SerilizeWatermarkForm));
			this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
			this.btnClear = new DevExpress.XtraEditors.SimpleButton();
			this.tePageRange = new DevExpress.XtraEditors.TextEdit();
			this.lbPageRangeComment = new System.Windows.Forms.Label();
			this.btnOK = new DevExpress.XtraEditors.SimpleButton();
			this.lbPageRange = new System.Windows.Forms.Label();
			this.rgrpPageRange = new DevExpress.XtraEditors.RadioGroup();
			this.lbHorzAlign = new System.Windows.Forms.Label();
			this.lbPosition = new System.Windows.Forms.Label();
			this.lkpImageView = new DevExpress.XtraEditors.LookUpEdit();
			this.lkpImageHAlign = new DevExpress.XtraEditors.LookUpEdit();
			this.chbTiling = new DevExpress.XtraEditors.CheckEdit();
			this.btnSelectPicture = new DevExpress.XtraEditors.SimpleButton();
			this.chbItalic = new DevExpress.XtraEditors.CheckEdit();
			this.chbBold = new DevExpress.XtraEditors.CheckEdit();
			this.lbFontColor = new System.Windows.Forms.Label();
			this.ceWatermarkColor = new DevExpress.XtraEditors.ColorEdit();
			this.lbFontSize = new System.Windows.Forms.Label();
			this.scrBarTransparent = new DevExpress.XtraEditors.HScrollBar();
			this.lbFont = new System.Windows.Forms.Label();
			this.cmbWatermarkFontSize = new DevExpress.XtraEditors.ComboBoxEdit();
			this.cmbWatermarkFont = new DevExpress.XtraEditors.ComboBoxEdit();
			this.cmbWatermarkText = new DevExpress.XtraEditors.ComboBoxEdit();
			this.lkpTextDirection = new DevExpress.XtraEditors.LookUpEdit();
			this.lbText = new System.Windows.Forms.Label();
			this.teTransparentValue = new DevExpress.XtraEditors.TextEdit();
			this.lbLayout = new System.Windows.Forms.Label();
			this.lkpImageVAlign = new DevExpress.XtraEditors.LookUpEdit();
			this.lbVertAlign = new System.Windows.Forms.Label();
			this.pc = new MyPrintControl();
			this.rgrpZOrder = new DevExpress.XtraEditors.RadioGroup();
			this.xtraTabControl = new DevExpress.XtraTab.XtraTabControl();
			this.tpTextWaterMark = new DevExpress.XtraTab.XtraTabPage();
			this.grpBoxTransparent = new DevExpress.XtraEditors.GroupControl();
			this.tpPictureWatermark = new DevExpress.XtraTab.XtraTabPage();
			this.grpBoxAlign = new DevExpress.XtraEditors.GroupControl();
			this.grpBoxImageTransparent = new DevExpress.XtraEditors.GroupControl();
			this.teImageTransparentValue = new DevExpress.XtraEditors.TextEdit();
			this.scrBarImageTransparent = new DevExpress.XtraEditors.HScrollBar();
			this.grpBoxPageRange = new DevExpress.XtraEditors.GroupControl();
			this.grpBoxZOrder = new DevExpress.XtraEditors.GroupControl();
			this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
			((System.ComponentModel.ISupportInitialize)(this.tePageRange.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.rgrpPageRange.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lkpImageView.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lkpImageHAlign.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.chbTiling.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.chbItalic.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.chbBold.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ceWatermarkColor.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cmbWatermarkFontSize.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cmbWatermarkFont.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cmbWatermarkText.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lkpTextDirection.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.teTransparentValue.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lkpImageVAlign.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.rgrpZOrder.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).BeginInit();
			this.xtraTabControl.SuspendLayout();
			this.tpTextWaterMark.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grpBoxTransparent)).BeginInit();
			this.grpBoxTransparent.SuspendLayout();
			this.tpPictureWatermark.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grpBoxAlign)).BeginInit();
			this.grpBoxAlign.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grpBoxImageTransparent)).BeginInit();
			this.grpBoxImageTransparent.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.teImageTransparentValue.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.grpBoxPageRange)).BeginInit();
			this.grpBoxPageRange.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grpBoxZOrder)).BeginInit();
			this.grpBoxZOrder.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
			this.panelControl1.SuspendLayout();
			this.SuspendLayout();
			
			
			
			this.btnCancel.AccessibleDescription = ((string)(resources.GetObject("btnCancel.AccessibleDescription")));
			this.btnCancel.AccessibleName = ((string)(resources.GetObject("btnCancel.AccessibleName")));
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnCancel.Anchor")));
			this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnCancel.Dock")));
			this.btnCancel.Enabled = ((bool)(resources.GetObject("btnCancel.Enabled")));
			this.btnCancel.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnCancel.ImeMode")));
			this.btnCancel.Location = ((System.Drawing.Point)(resources.GetObject("btnCancel.Location")));
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnCancel.RightToLeft")));
			this.btnCancel.Size = ((System.Drawing.Size)(resources.GetObject("btnCancel.Size")));
			this.btnCancel.TabIndex = ((int)(resources.GetObject("btnCancel.TabIndex")));
			this.btnCancel.Text = resources.GetString("btnCancel.Text");
			this.btnCancel.ToolTip = resources.GetString("btnCancel.ToolTip");
			this.btnCancel.ToolTipIconType = ((DevExpress.Utils.ToolTipIconType)(resources.GetObject("btnCancel.ToolTipIconType")));
			this.btnCancel.ToolTipTitle = resources.GetString("btnCancel.ToolTipTitle");
			this.btnCancel.Visible = ((bool)(resources.GetObject("btnCancel.Visible")));
			
			
			
			this.btnClear.AccessibleDescription = ((string)(resources.GetObject("btnClear.AccessibleDescription")));
			this.btnClear.AccessibleName = ((string)(resources.GetObject("btnClear.AccessibleName")));
			this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnClear.Anchor")));
			this.btnClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClear.BackgroundImage")));
			this.btnClear.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnClear.Dock")));
			this.btnClear.Enabled = ((bool)(resources.GetObject("btnClear.Enabled")));
			this.btnClear.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnClear.ImeMode")));
			this.btnClear.Location = ((System.Drawing.Point)(resources.GetObject("btnClear.Location")));
			this.btnClear.Name = "btnClear";
			this.btnClear.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnClear.RightToLeft")));
			this.btnClear.Size = ((System.Drawing.Size)(resources.GetObject("btnClear.Size")));
			this.btnClear.TabIndex = ((int)(resources.GetObject("btnClear.TabIndex")));
			this.btnClear.Text = resources.GetString("btnClear.Text");
			this.btnClear.ToolTip = resources.GetString("btnClear.ToolTip");
			this.btnClear.ToolTipIconType = ((DevExpress.Utils.ToolTipIconType)(resources.GetObject("btnClear.ToolTipIconType")));
			this.btnClear.ToolTipTitle = resources.GetString("btnClear.ToolTipTitle");
			this.btnClear.Visible = ((bool)(resources.GetObject("btnClear.Visible")));
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			
			
			
			this.tePageRange.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("tePageRange.Anchor")));
			this.tePageRange.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tePageRange.BackgroundImage")));
			this.tePageRange.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("tePageRange.Dock")));
			this.tePageRange.EditValue = resources.GetString("tePageRange.EditValue");
			this.tePageRange.Enabled = ((bool)(resources.GetObject("tePageRange.Enabled")));
			this.tePageRange.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tePageRange.ImeMode")));
			this.tePageRange.Location = ((System.Drawing.Point)(resources.GetObject("tePageRange.Location")));
			this.tePageRange.Name = "tePageRange";
			
			
			
			this.tePageRange.Properties.AccessibleDescription = ((string)(resources.GetObject("tePageRange.Properties.AccessibleDescription")));
			this.tePageRange.Properties.AccessibleName = ((string)(resources.GetObject("tePageRange.Properties.AccessibleName")));
			this.tePageRange.Properties.AutoHeight = ((bool)(resources.GetObject("tePageRange.Properties.AutoHeight")));
			this.tePageRange.Properties.Mask.AutoComplete = ((DevExpress.XtraEditors.Mask.AutoCompleteType)(resources.GetObject("tePageRange.Properties.Mask.AutoComplete")));
			this.tePageRange.Properties.Mask.BeepOnError = ((bool)(resources.GetObject("tePageRange.Properties.Mask.BeepOnError")));
			this.tePageRange.Properties.Mask.EditMask = resources.GetString("tePageRange.Properties.Mask.EditMask");
			this.tePageRange.Properties.Mask.IgnoreMaskBlank = ((bool)(resources.GetObject("tePageRange.Properties.Mask.IgnoreMaskBlank")));
			this.tePageRange.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("tePageRange.Properties.Mask.MaskType")));
			this.tePageRange.Properties.Mask.PlaceHolder = ((char)(resources.GetObject("tePageRange.Properties.Mask.PlaceHolder")));
			this.tePageRange.Properties.Mask.SaveLiteral = ((bool)(resources.GetObject("tePageRange.Properties.Mask.SaveLiteral")));
			this.tePageRange.Properties.Mask.ShowPlaceHolders = ((bool)(resources.GetObject("tePageRange.Properties.Mask.ShowPlaceHolders")));
			this.tePageRange.Properties.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("tePageRange.Properties.Mask.UseMaskAsDisplayFormat")));
			this.tePageRange.Properties.NullText = resources.GetString("tePageRange.Properties.NullText");
			this.tePageRange.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tePageRange.RightToLeft")));
			this.tePageRange.Size = ((System.Drawing.Size)(resources.GetObject("tePageRange.Size")));
			this.tePageRange.TabIndex = ((int)(resources.GetObject("tePageRange.TabIndex")));
			this.tePageRange.Tag = "";
			this.tePageRange.ToolTip = resources.GetString("tePageRange.ToolTip");
			this.tePageRange.ToolTipIconType = ((DevExpress.Utils.ToolTipIconType)(resources.GetObject("tePageRange.ToolTipIconType")));
			this.tePageRange.ToolTipTitle = resources.GetString("tePageRange.ToolTipTitle");
			this.tePageRange.Visible = ((bool)(resources.GetObject("tePageRange.Visible")));
			this.tePageRange.EditValueChanged += new System.EventHandler(this.tePageRange_EditValueChanged);
			
			
			
			this.lbPageRangeComment.AccessibleDescription = ((string)(resources.GetObject("lbPageRangeComment.AccessibleDescription")));
			this.lbPageRangeComment.AccessibleName = ((string)(resources.GetObject("lbPageRangeComment.AccessibleName")));
			this.lbPageRangeComment.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbPageRangeComment.Anchor")));
			this.lbPageRangeComment.AutoSize = ((bool)(resources.GetObject("lbPageRangeComment.AutoSize")));
			this.lbPageRangeComment.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbPageRangeComment.Dock")));
			this.lbPageRangeComment.Enabled = ((bool)(resources.GetObject("lbPageRangeComment.Enabled")));
			this.lbPageRangeComment.Font = ((System.Drawing.Font)(resources.GetObject("lbPageRangeComment.Font")));
			this.lbPageRangeComment.Image = ((System.Drawing.Image)(resources.GetObject("lbPageRangeComment.Image")));
			this.lbPageRangeComment.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbPageRangeComment.ImageAlign")));
			this.lbPageRangeComment.ImageIndex = ((int)(resources.GetObject("lbPageRangeComment.ImageIndex")));
			this.lbPageRangeComment.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbPageRangeComment.ImeMode")));
			this.lbPageRangeComment.Location = ((System.Drawing.Point)(resources.GetObject("lbPageRangeComment.Location")));
			this.lbPageRangeComment.Name = "lbPageRangeComment";
			this.lbPageRangeComment.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbPageRangeComment.RightToLeft")));
			this.lbPageRangeComment.Size = ((System.Drawing.Size)(resources.GetObject("lbPageRangeComment.Size")));
			this.lbPageRangeComment.TabIndex = ((int)(resources.GetObject("lbPageRangeComment.TabIndex")));
			this.lbPageRangeComment.Text = resources.GetString("lbPageRangeComment.Text");
			this.lbPageRangeComment.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbPageRangeComment.TextAlign")));
			this.lbPageRangeComment.Visible = ((bool)(resources.GetObject("lbPageRangeComment.Visible")));
			
			
			
			this.btnOK.AccessibleDescription = ((string)(resources.GetObject("btnOK.AccessibleDescription")));
			this.btnOK.AccessibleName = ((string)(resources.GetObject("btnOK.AccessibleName")));
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnOK.Anchor")));
			this.btnOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOK.BackgroundImage")));
			this.btnOK.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnOK.Dock")));
			this.btnOK.Enabled = ((bool)(resources.GetObject("btnOK.Enabled")));
			this.btnOK.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnOK.ImeMode")));
			this.btnOK.Location = ((System.Drawing.Point)(resources.GetObject("btnOK.Location")));
			this.btnOK.Name = "btnOK";
			this.btnOK.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnOK.RightToLeft")));
			this.btnOK.Size = ((System.Drawing.Size)(resources.GetObject("btnOK.Size")));
			this.btnOK.TabIndex = ((int)(resources.GetObject("btnOK.TabIndex")));
			this.btnOK.Text = resources.GetString("btnOK.Text");
			this.btnOK.ToolTip = resources.GetString("btnOK.ToolTip");
			this.btnOK.ToolTipIconType = ((DevExpress.Utils.ToolTipIconType)(resources.GetObject("btnOK.ToolTipIconType")));
			this.btnOK.ToolTipTitle = resources.GetString("btnOK.ToolTipTitle");
			this.btnOK.Visible = ((bool)(resources.GetObject("btnOK.Visible")));
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			
			
			
			this.lbPageRange.AccessibleDescription = ((string)(resources.GetObject("lbPageRange.AccessibleDescription")));
			this.lbPageRange.AccessibleName = ((string)(resources.GetObject("lbPageRange.AccessibleName")));
			this.lbPageRange.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbPageRange.Anchor")));
			this.lbPageRange.AutoSize = ((bool)(resources.GetObject("lbPageRange.AutoSize")));
			this.lbPageRange.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbPageRange.Dock")));
			this.lbPageRange.Enabled = ((bool)(resources.GetObject("lbPageRange.Enabled")));
			this.lbPageRange.Font = ((System.Drawing.Font)(resources.GetObject("lbPageRange.Font")));
			this.lbPageRange.Image = ((System.Drawing.Image)(resources.GetObject("lbPageRange.Image")));
			this.lbPageRange.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbPageRange.ImageAlign")));
			this.lbPageRange.ImageIndex = ((int)(resources.GetObject("lbPageRange.ImageIndex")));
			this.lbPageRange.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbPageRange.ImeMode")));
			this.lbPageRange.Location = ((System.Drawing.Point)(resources.GetObject("lbPageRange.Location")));
			this.lbPageRange.Name = "lbPageRange";
			this.lbPageRange.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbPageRange.RightToLeft")));
			this.lbPageRange.Size = ((System.Drawing.Size)(resources.GetObject("lbPageRange.Size")));
			this.lbPageRange.TabIndex = ((int)(resources.GetObject("lbPageRange.TabIndex")));
			this.lbPageRange.Text = resources.GetString("lbPageRange.Text");
			this.lbPageRange.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbPageRange.TextAlign")));
			this.lbPageRange.Visible = ((bool)(resources.GetObject("lbPageRange.Visible")));
			
			
			
			this.rgrpPageRange.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("rgrpPageRange.Anchor")));
			this.rgrpPageRange.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("rgrpPageRange.BackgroundImage")));
			this.rgrpPageRange.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("rgrpPageRange.Dock")));
			this.rgrpPageRange.EditValue = ((bool)(resources.GetObject("rgrpPageRange.EditValue")));
			this.rgrpPageRange.Enabled = ((bool)(resources.GetObject("rgrpPageRange.Enabled")));
			this.rgrpPageRange.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("rgrpPageRange.ImeMode")));
			this.rgrpPageRange.Location = ((System.Drawing.Point)(resources.GetObject("rgrpPageRange.Location")));
			this.rgrpPageRange.Name = "rgrpPageRange";
			
			
			
			this.rgrpPageRange.Properties.AccessibleDescription = ((string)(resources.GetObject("rgrpPageRange.Properties.AccessibleDescription")));
			this.rgrpPageRange.Properties.AccessibleName = ((string)(resources.GetObject("rgrpPageRange.Properties.AccessibleName")));
			this.rgrpPageRange.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.rgrpPageRange.Properties.Appearance.Options.UseBackColor = true;
			this.rgrpPageRange.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.rgrpPageRange.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
																												  new DevExpress.XtraEditors.Controls.RadioGroupItem(false, "&All"),
																												  new DevExpress.XtraEditors.Controls.RadioGroupItem(true, "&Pages:")});
			this.rgrpPageRange.Properties.NullText = resources.GetString("rgrpPageRange.Properties.NullText");
			this.rgrpPageRange.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("rgrpPageRange.RightToLeft")));
			this.rgrpPageRange.Size = ((System.Drawing.Size)(resources.GetObject("rgrpPageRange.Size")));
			this.rgrpPageRange.TabIndex = ((int)(resources.GetObject("rgrpPageRange.TabIndex")));
			this.rgrpPageRange.ToolTip = resources.GetString("rgrpPageRange.ToolTip");
			this.rgrpPageRange.ToolTipIconType = ((DevExpress.Utils.ToolTipIconType)(resources.GetObject("rgrpPageRange.ToolTipIconType")));
			this.rgrpPageRange.ToolTipTitle = resources.GetString("rgrpPageRange.ToolTipTitle");
			this.rgrpPageRange.Visible = ((bool)(resources.GetObject("rgrpPageRange.Visible")));
			this.rgrpPageRange.EditValueChanged += new System.EventHandler(this.rgrpPageRange_EditValueChanged);
			
			
			
			this.lbHorzAlign.AccessibleDescription = ((string)(resources.GetObject("lbHorzAlign.AccessibleDescription")));
			this.lbHorzAlign.AccessibleName = ((string)(resources.GetObject("lbHorzAlign.AccessibleName")));
			this.lbHorzAlign.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbHorzAlign.Anchor")));
			this.lbHorzAlign.AutoSize = ((bool)(resources.GetObject("lbHorzAlign.AutoSize")));
			this.lbHorzAlign.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbHorzAlign.Dock")));
			this.lbHorzAlign.Enabled = ((bool)(resources.GetObject("lbHorzAlign.Enabled")));
			this.lbHorzAlign.Font = ((System.Drawing.Font)(resources.GetObject("lbHorzAlign.Font")));
			this.lbHorzAlign.Image = ((System.Drawing.Image)(resources.GetObject("lbHorzAlign.Image")));
			this.lbHorzAlign.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbHorzAlign.ImageAlign")));
			this.lbHorzAlign.ImageIndex = ((int)(resources.GetObject("lbHorzAlign.ImageIndex")));
			this.lbHorzAlign.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbHorzAlign.ImeMode")));
			this.lbHorzAlign.Location = ((System.Drawing.Point)(resources.GetObject("lbHorzAlign.Location")));
			this.lbHorzAlign.Name = "lbHorzAlign";
			this.lbHorzAlign.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbHorzAlign.RightToLeft")));
			this.lbHorzAlign.Size = ((System.Drawing.Size)(resources.GetObject("lbHorzAlign.Size")));
			this.lbHorzAlign.TabIndex = ((int)(resources.GetObject("lbHorzAlign.TabIndex")));
			this.lbHorzAlign.Text = resources.GetString("lbHorzAlign.Text");
			this.lbHorzAlign.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbHorzAlign.TextAlign")));
			this.lbHorzAlign.Visible = ((bool)(resources.GetObject("lbHorzAlign.Visible")));
			
			
			
			this.lbPosition.AccessibleDescription = ((string)(resources.GetObject("lbPosition.AccessibleDescription")));
			this.lbPosition.AccessibleName = ((string)(resources.GetObject("lbPosition.AccessibleName")));
			this.lbPosition.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbPosition.Anchor")));
			this.lbPosition.AutoSize = ((bool)(resources.GetObject("lbPosition.AutoSize")));
			this.lbPosition.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbPosition.Dock")));
			this.lbPosition.Enabled = ((bool)(resources.GetObject("lbPosition.Enabled")));
			this.lbPosition.Font = ((System.Drawing.Font)(resources.GetObject("lbPosition.Font")));
			this.lbPosition.Image = ((System.Drawing.Image)(resources.GetObject("lbPosition.Image")));
			this.lbPosition.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbPosition.ImageAlign")));
			this.lbPosition.ImageIndex = ((int)(resources.GetObject("lbPosition.ImageIndex")));
			this.lbPosition.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbPosition.ImeMode")));
			this.lbPosition.Location = ((System.Drawing.Point)(resources.GetObject("lbPosition.Location")));
			this.lbPosition.Name = "lbPosition";
			this.lbPosition.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbPosition.RightToLeft")));
			this.lbPosition.Size = ((System.Drawing.Size)(resources.GetObject("lbPosition.Size")));
			this.lbPosition.TabIndex = ((int)(resources.GetObject("lbPosition.TabIndex")));
			this.lbPosition.Text = resources.GetString("lbPosition.Text");
			this.lbPosition.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbPosition.TextAlign")));
			this.lbPosition.Visible = ((bool)(resources.GetObject("lbPosition.Visible")));
			
			
			
			this.lkpImageView.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lkpImageView.Anchor")));
			this.lkpImageView.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("lkpImageView.BackgroundImage")));
			this.lkpImageView.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lkpImageView.Dock")));
			this.lkpImageView.EditValue = ((object)(resources.GetObject("lkpImageView.EditValue")));
			this.lkpImageView.Enabled = ((bool)(resources.GetObject("lkpImageView.Enabled")));
			this.lkpImageView.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lkpImageView.ImeMode")));
			this.lkpImageView.Location = ((System.Drawing.Point)(resources.GetObject("lkpImageView.Location")));
			this.lkpImageView.Name = "lkpImageView";
			
			
			
			this.lkpImageView.Properties.AccessibleDescription = ((string)(resources.GetObject("lkpImageView.Properties.AccessibleDescription")));
			this.lkpImageView.Properties.AccessibleName = ((string)(resources.GetObject("lkpImageView.Properties.AccessibleName")));
			this.lkpImageView.Properties.AutoHeight = ((bool)(resources.GetObject("lkpImageView.Properties.AutoHeight")));
			this.lkpImageView.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
																												 new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.lkpImageView.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
																													 new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Text")});
			this.lkpImageView.Properties.Mask.AutoComplete = ((DevExpress.XtraEditors.Mask.AutoCompleteType)(resources.GetObject("lkpImageView.Properties.Mask.AutoComplete")));
			this.lkpImageView.Properties.Mask.BeepOnError = ((bool)(resources.GetObject("lkpImageView.Properties.Mask.BeepOnError")));
			this.lkpImageView.Properties.Mask.EditMask = resources.GetString("lkpImageView.Properties.Mask.EditMask");
			this.lkpImageView.Properties.Mask.IgnoreMaskBlank = ((bool)(resources.GetObject("lkpImageView.Properties.Mask.IgnoreMaskBlank")));
			this.lkpImageView.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("lkpImageView.Properties.Mask.MaskType")));
			this.lkpImageView.Properties.Mask.PlaceHolder = ((char)(resources.GetObject("lkpImageView.Properties.Mask.PlaceHolder")));
			this.lkpImageView.Properties.Mask.SaveLiteral = ((bool)(resources.GetObject("lkpImageView.Properties.Mask.SaveLiteral")));
			this.lkpImageView.Properties.Mask.ShowPlaceHolders = ((bool)(resources.GetObject("lkpImageView.Properties.Mask.ShowPlaceHolders")));
			this.lkpImageView.Properties.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("lkpImageView.Properties.Mask.UseMaskAsDisplayFormat")));
			this.lkpImageView.Properties.NullText = resources.GetString("lkpImageView.Properties.NullText");
			this.lkpImageView.Properties.ShowFooter = false;
			this.lkpImageView.Properties.ShowHeader = false;
			this.lkpImageView.Properties.ShowLines = false;
			this.lkpImageView.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lkpImageView.RightToLeft")));
			this.lkpImageView.Size = ((System.Drawing.Size)(resources.GetObject("lkpImageView.Size")));
			this.lkpImageView.TabIndex = ((int)(resources.GetObject("lkpImageView.TabIndex")));
			this.lkpImageView.ToolTip = resources.GetString("lkpImageView.ToolTip");
			this.lkpImageView.ToolTipIconType = ((DevExpress.Utils.ToolTipIconType)(resources.GetObject("lkpImageView.ToolTipIconType")));
			this.lkpImageView.ToolTipTitle = resources.GetString("lkpImageView.ToolTipTitle");
			this.lkpImageView.Visible = ((bool)(resources.GetObject("lkpImageView.Visible")));
			this.lkpImageView.EditValueChanged += new System.EventHandler(this.OnEditValueChanged);
			
			
			
			this.lkpImageHAlign.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lkpImageHAlign.Anchor")));
			this.lkpImageHAlign.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("lkpImageHAlign.BackgroundImage")));
			this.lkpImageHAlign.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lkpImageHAlign.Dock")));
			this.lkpImageHAlign.EditValue = ((object)(resources.GetObject("lkpImageHAlign.EditValue")));
			this.lkpImageHAlign.Enabled = ((bool)(resources.GetObject("lkpImageHAlign.Enabled")));
			this.lkpImageHAlign.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lkpImageHAlign.ImeMode")));
			this.lkpImageHAlign.Location = ((System.Drawing.Point)(resources.GetObject("lkpImageHAlign.Location")));
			this.lkpImageHAlign.Name = "lkpImageHAlign";
			
			
			
			this.lkpImageHAlign.Properties.AccessibleDescription = ((string)(resources.GetObject("lkpImageHAlign.Properties.AccessibleDescription")));
			this.lkpImageHAlign.Properties.AccessibleName = ((string)(resources.GetObject("lkpImageHAlign.Properties.AccessibleName")));
			this.lkpImageHAlign.Properties.AutoHeight = ((bool)(resources.GetObject("lkpImageHAlign.Properties.AutoHeight")));
			this.lkpImageHAlign.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
																												   new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.lkpImageHAlign.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
																													   new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Text")});
			this.lkpImageHAlign.Properties.Mask.AutoComplete = ((DevExpress.XtraEditors.Mask.AutoCompleteType)(resources.GetObject("lkpImageHAlign.Properties.Mask.AutoComplete")));
			this.lkpImageHAlign.Properties.Mask.BeepOnError = ((bool)(resources.GetObject("lkpImageHAlign.Properties.Mask.BeepOnError")));
			this.lkpImageHAlign.Properties.Mask.EditMask = resources.GetString("lkpImageHAlign.Properties.Mask.EditMask");
			this.lkpImageHAlign.Properties.Mask.IgnoreMaskBlank = ((bool)(resources.GetObject("lkpImageHAlign.Properties.Mask.IgnoreMaskBlank")));
			this.lkpImageHAlign.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("lkpImageHAlign.Properties.Mask.MaskType")));
			this.lkpImageHAlign.Properties.Mask.PlaceHolder = ((char)(resources.GetObject("lkpImageHAlign.Properties.Mask.PlaceHolder")));
			this.lkpImageHAlign.Properties.Mask.SaveLiteral = ((bool)(resources.GetObject("lkpImageHAlign.Properties.Mask.SaveLiteral")));
			this.lkpImageHAlign.Properties.Mask.ShowPlaceHolders = ((bool)(resources.GetObject("lkpImageHAlign.Properties.Mask.ShowPlaceHolders")));
			this.lkpImageHAlign.Properties.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("lkpImageHAlign.Properties.Mask.UseMaskAsDisplayFormat")));
			this.lkpImageHAlign.Properties.NullText = resources.GetString("lkpImageHAlign.Properties.NullText");
			this.lkpImageHAlign.Properties.ShowFooter = false;
			this.lkpImageHAlign.Properties.ShowHeader = false;
			this.lkpImageHAlign.Properties.ShowLines = false;
			this.lkpImageHAlign.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lkpImageHAlign.RightToLeft")));
			this.lkpImageHAlign.Size = ((System.Drawing.Size)(resources.GetObject("lkpImageHAlign.Size")));
			this.lkpImageHAlign.TabIndex = ((int)(resources.GetObject("lkpImageHAlign.TabIndex")));
			this.lkpImageHAlign.ToolTip = resources.GetString("lkpImageHAlign.ToolTip");
			this.lkpImageHAlign.ToolTipIconType = ((DevExpress.Utils.ToolTipIconType)(resources.GetObject("lkpImageHAlign.ToolTipIconType")));
			this.lkpImageHAlign.ToolTipTitle = resources.GetString("lkpImageHAlign.ToolTipTitle");
			this.lkpImageHAlign.Visible = ((bool)(resources.GetObject("lkpImageHAlign.Visible")));
			this.lkpImageHAlign.EditValueChanged += new System.EventHandler(this.OnEditValueChanged);
			
			
			
			this.chbTiling.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("chbTiling.Anchor")));
			this.chbTiling.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("chbTiling.BackgroundImage")));
			this.chbTiling.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("chbTiling.Dock")));
			this.chbTiling.EditValue = ((bool)(resources.GetObject("chbTiling.EditValue")));
			this.chbTiling.Enabled = ((bool)(resources.GetObject("chbTiling.Enabled")));
			this.chbTiling.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("chbTiling.ImeMode")));
			this.chbTiling.Location = ((System.Drawing.Point)(resources.GetObject("chbTiling.Location")));
			this.chbTiling.Name = "chbTiling";
			
			
			
			this.chbTiling.Properties.AccessibleDescription = ((string)(resources.GetObject("chbTiling.Properties.AccessibleDescription")));
			this.chbTiling.Properties.AccessibleName = ((string)(resources.GetObject("chbTiling.Properties.AccessibleName")));
			this.chbTiling.Properties.AutoHeight = ((bool)(resources.GetObject("chbTiling.Properties.AutoHeight")));
			this.chbTiling.Properties.Caption = resources.GetString("chbTiling.Properties.Caption");
			this.chbTiling.Properties.NullText = resources.GetString("chbTiling.Properties.NullText");
			this.chbTiling.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("chbTiling.RightToLeft")));
			this.chbTiling.Size = ((System.Drawing.Size)(resources.GetObject("chbTiling.Size")));
			this.chbTiling.TabIndex = ((int)(resources.GetObject("chbTiling.TabIndex")));
			this.chbTiling.ToolTip = resources.GetString("chbTiling.ToolTip");
			this.chbTiling.ToolTipIconType = ((DevExpress.Utils.ToolTipIconType)(resources.GetObject("chbTiling.ToolTipIconType")));
			this.chbTiling.ToolTipTitle = resources.GetString("chbTiling.ToolTipTitle");
			this.chbTiling.Visible = ((bool)(resources.GetObject("chbTiling.Visible")));
			this.chbTiling.CheckedChanged += new System.EventHandler(this.OnEditValueChanged);
			
			
			
			this.btnSelectPicture.AccessibleDescription = ((string)(resources.GetObject("btnSelectPicture.AccessibleDescription")));
			this.btnSelectPicture.AccessibleName = ((string)(resources.GetObject("btnSelectPicture.AccessibleName")));
			this.btnSelectPicture.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnSelectPicture.Anchor")));
			this.btnSelectPicture.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSelectPicture.BackgroundImage")));
			this.btnSelectPicture.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnSelectPicture.Dock")));
			this.btnSelectPicture.Enabled = ((bool)(resources.GetObject("btnSelectPicture.Enabled")));
			this.btnSelectPicture.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnSelectPicture.ImeMode")));
			this.btnSelectPicture.Location = ((System.Drawing.Point)(resources.GetObject("btnSelectPicture.Location")));
			this.btnSelectPicture.Name = "btnSelectPicture";
			this.btnSelectPicture.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnSelectPicture.RightToLeft")));
			this.btnSelectPicture.Size = ((System.Drawing.Size)(resources.GetObject("btnSelectPicture.Size")));
			this.btnSelectPicture.TabIndex = ((int)(resources.GetObject("btnSelectPicture.TabIndex")));
			this.btnSelectPicture.Text = resources.GetString("btnSelectPicture.Text");
			this.btnSelectPicture.ToolTip = resources.GetString("btnSelectPicture.ToolTip");
			this.btnSelectPicture.ToolTipIconType = ((DevExpress.Utils.ToolTipIconType)(resources.GetObject("btnSelectPicture.ToolTipIconType")));
			this.btnSelectPicture.ToolTipTitle = resources.GetString("btnSelectPicture.ToolTipTitle");
			this.btnSelectPicture.Visible = ((bool)(resources.GetObject("btnSelectPicture.Visible")));
			this.btnSelectPicture.Click += new System.EventHandler(this.btnSelectPicture_Click);
			
			
			
			this.chbItalic.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("chbItalic.Anchor")));
			this.chbItalic.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("chbItalic.BackgroundImage")));
			this.chbItalic.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("chbItalic.Dock")));
			this.chbItalic.EditValue = ((bool)(resources.GetObject("chbItalic.EditValue")));
			this.chbItalic.Enabled = ((bool)(resources.GetObject("chbItalic.Enabled")));
			this.chbItalic.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("chbItalic.ImeMode")));
			this.chbItalic.Location = ((System.Drawing.Point)(resources.GetObject("chbItalic.Location")));
			this.chbItalic.Name = "chbItalic";
			
			
			
			this.chbItalic.Properties.AccessibleDescription = ((string)(resources.GetObject("chbItalic.Properties.AccessibleDescription")));
			this.chbItalic.Properties.AccessibleName = ((string)(resources.GetObject("chbItalic.Properties.AccessibleName")));
			this.chbItalic.Properties.AutoHeight = ((bool)(resources.GetObject("chbItalic.Properties.AutoHeight")));
			this.chbItalic.Properties.Caption = resources.GetString("chbItalic.Properties.Caption");
			this.chbItalic.Properties.NullText = resources.GetString("chbItalic.Properties.NullText");
			this.chbItalic.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("chbItalic.RightToLeft")));
			this.chbItalic.Size = ((System.Drawing.Size)(resources.GetObject("chbItalic.Size")));
			this.chbItalic.TabIndex = ((int)(resources.GetObject("chbItalic.TabIndex")));
			this.chbItalic.ToolTip = resources.GetString("chbItalic.ToolTip");
			this.chbItalic.ToolTipIconType = ((DevExpress.Utils.ToolTipIconType)(resources.GetObject("chbItalic.ToolTipIconType")));
			this.chbItalic.ToolTipTitle = resources.GetString("chbItalic.ToolTipTitle");
			this.chbItalic.Visible = ((bool)(resources.GetObject("chbItalic.Visible")));
			this.chbItalic.CheckedChanged += new System.EventHandler(this.OnEditValueChanged);
			
			
			
			this.chbBold.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("chbBold.Anchor")));
			this.chbBold.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("chbBold.BackgroundImage")));
			this.chbBold.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("chbBold.Dock")));
			this.chbBold.EditValue = ((bool)(resources.GetObject("chbBold.EditValue")));
			this.chbBold.Enabled = ((bool)(resources.GetObject("chbBold.Enabled")));
			this.chbBold.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("chbBold.ImeMode")));
			this.chbBold.Location = ((System.Drawing.Point)(resources.GetObject("chbBold.Location")));
			this.chbBold.Name = "chbBold";
			
			
			
			this.chbBold.Properties.AccessibleDescription = ((string)(resources.GetObject("chbBold.Properties.AccessibleDescription")));
			this.chbBold.Properties.AccessibleName = ((string)(resources.GetObject("chbBold.Properties.AccessibleName")));
			this.chbBold.Properties.AutoHeight = ((bool)(resources.GetObject("chbBold.Properties.AutoHeight")));
			this.chbBold.Properties.Caption = resources.GetString("chbBold.Properties.Caption");
			this.chbBold.Properties.NullText = resources.GetString("chbBold.Properties.NullText");
			this.chbBold.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("chbBold.RightToLeft")));
			this.chbBold.Size = ((System.Drawing.Size)(resources.GetObject("chbBold.Size")));
			this.chbBold.TabIndex = ((int)(resources.GetObject("chbBold.TabIndex")));
			this.chbBold.ToolTip = resources.GetString("chbBold.ToolTip");
			this.chbBold.ToolTipIconType = ((DevExpress.Utils.ToolTipIconType)(resources.GetObject("chbBold.ToolTipIconType")));
			this.chbBold.ToolTipTitle = resources.GetString("chbBold.ToolTipTitle");
			this.chbBold.Visible = ((bool)(resources.GetObject("chbBold.Visible")));
			this.chbBold.CheckedChanged += new System.EventHandler(this.OnEditValueChanged);
			
			
			
			this.lbFontColor.AccessibleDescription = ((string)(resources.GetObject("lbFontColor.AccessibleDescription")));
			this.lbFontColor.AccessibleName = ((string)(resources.GetObject("lbFontColor.AccessibleName")));
			this.lbFontColor.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbFontColor.Anchor")));
			this.lbFontColor.AutoSize = ((bool)(resources.GetObject("lbFontColor.AutoSize")));
			this.lbFontColor.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbFontColor.Dock")));
			this.lbFontColor.Enabled = ((bool)(resources.GetObject("lbFontColor.Enabled")));
			this.lbFontColor.Font = ((System.Drawing.Font)(resources.GetObject("lbFontColor.Font")));
			this.lbFontColor.Image = ((System.Drawing.Image)(resources.GetObject("lbFontColor.Image")));
			this.lbFontColor.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbFontColor.ImageAlign")));
			this.lbFontColor.ImageIndex = ((int)(resources.GetObject("lbFontColor.ImageIndex")));
			this.lbFontColor.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbFontColor.ImeMode")));
			this.lbFontColor.Location = ((System.Drawing.Point)(resources.GetObject("lbFontColor.Location")));
			this.lbFontColor.Name = "lbFontColor";
			this.lbFontColor.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbFontColor.RightToLeft")));
			this.lbFontColor.Size = ((System.Drawing.Size)(resources.GetObject("lbFontColor.Size")));
			this.lbFontColor.TabIndex = ((int)(resources.GetObject("lbFontColor.TabIndex")));
			this.lbFontColor.Tag = "";
			this.lbFontColor.Text = resources.GetString("lbFontColor.Text");
			this.lbFontColor.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbFontColor.TextAlign")));
			this.lbFontColor.Visible = ((bool)(resources.GetObject("lbFontColor.Visible")));
			
			
			
			this.ceWatermarkColor.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("ceWatermarkColor.Anchor")));
			this.ceWatermarkColor.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ceWatermarkColor.BackgroundImage")));
			this.ceWatermarkColor.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("ceWatermarkColor.Dock")));
			this.ceWatermarkColor.EditValue = ((System.Drawing.Color)(resources.GetObject("ceWatermarkColor.EditValue")));
			this.ceWatermarkColor.Enabled = ((bool)(resources.GetObject("ceWatermarkColor.Enabled")));
			this.ceWatermarkColor.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("ceWatermarkColor.ImeMode")));
			this.ceWatermarkColor.Location = ((System.Drawing.Point)(resources.GetObject("ceWatermarkColor.Location")));
			this.ceWatermarkColor.Name = "ceWatermarkColor";
			
			
			
			this.ceWatermarkColor.Properties.AccessibleDescription = ((string)(resources.GetObject("ceWatermarkColor.Properties.AccessibleDescription")));
			this.ceWatermarkColor.Properties.AccessibleName = ((string)(resources.GetObject("ceWatermarkColor.Properties.AccessibleName")));
			this.ceWatermarkColor.Properties.AutoHeight = ((bool)(resources.GetObject("ceWatermarkColor.Properties.AutoHeight")));
			this.ceWatermarkColor.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
																													 new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.ceWatermarkColor.Properties.ColorAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.ceWatermarkColor.Properties.Mask.AutoComplete = ((DevExpress.XtraEditors.Mask.AutoCompleteType)(resources.GetObject("ceWatermarkColor.Properties.Mask.AutoComplete")));
			this.ceWatermarkColor.Properties.Mask.BeepOnError = ((bool)(resources.GetObject("ceWatermarkColor.Properties.Mask.BeepOnError")));
			this.ceWatermarkColor.Properties.Mask.EditMask = resources.GetString("ceWatermarkColor.Properties.Mask.EditMask");
			this.ceWatermarkColor.Properties.Mask.IgnoreMaskBlank = ((bool)(resources.GetObject("ceWatermarkColor.Properties.Mask.IgnoreMaskBlank")));
			this.ceWatermarkColor.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("ceWatermarkColor.Properties.Mask.MaskType")));
			this.ceWatermarkColor.Properties.Mask.PlaceHolder = ((char)(resources.GetObject("ceWatermarkColor.Properties.Mask.PlaceHolder")));
			this.ceWatermarkColor.Properties.Mask.SaveLiteral = ((bool)(resources.GetObject("ceWatermarkColor.Properties.Mask.SaveLiteral")));
			this.ceWatermarkColor.Properties.Mask.ShowPlaceHolders = ((bool)(resources.GetObject("ceWatermarkColor.Properties.Mask.ShowPlaceHolders")));
			this.ceWatermarkColor.Properties.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("ceWatermarkColor.Properties.Mask.UseMaskAsDisplayFormat")));
			this.ceWatermarkColor.Properties.NullText = resources.GetString("ceWatermarkColor.Properties.NullText");
			this.ceWatermarkColor.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("ceWatermarkColor.RightToLeft")));
			this.ceWatermarkColor.Size = ((System.Drawing.Size)(resources.GetObject("ceWatermarkColor.Size")));
			this.ceWatermarkColor.TabIndex = ((int)(resources.GetObject("ceWatermarkColor.TabIndex")));
			this.ceWatermarkColor.Tag = "";
			this.ceWatermarkColor.ToolTip = resources.GetString("ceWatermarkColor.ToolTip");
			this.ceWatermarkColor.ToolTipIconType = ((DevExpress.Utils.ToolTipIconType)(resources.GetObject("ceWatermarkColor.ToolTipIconType")));
			this.ceWatermarkColor.ToolTipTitle = resources.GetString("ceWatermarkColor.ToolTipTitle");
			this.ceWatermarkColor.Visible = ((bool)(resources.GetObject("ceWatermarkColor.Visible")));
			this.ceWatermarkColor.EditValueChanged += new System.EventHandler(this.OnEditValueChanged);
			
			
			
			this.lbFontSize.AccessibleDescription = ((string)(resources.GetObject("lbFontSize.AccessibleDescription")));
			this.lbFontSize.AccessibleName = ((string)(resources.GetObject("lbFontSize.AccessibleName")));
			this.lbFontSize.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbFontSize.Anchor")));
			this.lbFontSize.AutoSize = ((bool)(resources.GetObject("lbFontSize.AutoSize")));
			this.lbFontSize.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbFontSize.Dock")));
			this.lbFontSize.Enabled = ((bool)(resources.GetObject("lbFontSize.Enabled")));
			this.lbFontSize.Font = ((System.Drawing.Font)(resources.GetObject("lbFontSize.Font")));
			this.lbFontSize.Image = ((System.Drawing.Image)(resources.GetObject("lbFontSize.Image")));
			this.lbFontSize.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbFontSize.ImageAlign")));
			this.lbFontSize.ImageIndex = ((int)(resources.GetObject("lbFontSize.ImageIndex")));
			this.lbFontSize.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbFontSize.ImeMode")));
			this.lbFontSize.Location = ((System.Drawing.Point)(resources.GetObject("lbFontSize.Location")));
			this.lbFontSize.Name = "lbFontSize";
			this.lbFontSize.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbFontSize.RightToLeft")));
			this.lbFontSize.Size = ((System.Drawing.Size)(resources.GetObject("lbFontSize.Size")));
			this.lbFontSize.TabIndex = ((int)(resources.GetObject("lbFontSize.TabIndex")));
			this.lbFontSize.Tag = "";
			this.lbFontSize.Text = resources.GetString("lbFontSize.Text");
			this.lbFontSize.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbFontSize.TextAlign")));
			this.lbFontSize.Visible = ((bool)(resources.GetObject("lbFontSize.Visible")));
			
			
			
			this.scrBarTransparent.AccessibleDescription = ((string)(resources.GetObject("scrBarTransparent.AccessibleDescription")));
			this.scrBarTransparent.AccessibleName = ((string)(resources.GetObject("scrBarTransparent.AccessibleName")));
			this.scrBarTransparent.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("scrBarTransparent.Anchor")));
			this.scrBarTransparent.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("scrBarTransparent.Dock")));
			this.scrBarTransparent.Enabled = ((bool)(resources.GetObject("scrBarTransparent.Enabled")));
			this.scrBarTransparent.Location = ((System.Drawing.Point)(resources.GetObject("scrBarTransparent.Location")));
			this.scrBarTransparent.Maximum = 264;
			this.scrBarTransparent.Name = "scrBarTransparent";
			this.scrBarTransparent.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("scrBarTransparent.RightToLeft")));
			this.scrBarTransparent.Size = ((System.Drawing.Size)(resources.GetObject("scrBarTransparent.Size")));
			this.scrBarTransparent.TabIndex = ((int)(resources.GetObject("scrBarTransparent.TabIndex")));
			this.scrBarTransparent.Tag = "";
			this.scrBarTransparent.Value = 50;
			this.scrBarTransparent.Visible = ((bool)(resources.GetObject("scrBarTransparent.Visible")));
			this.scrBarTransparent.Scroll += new System.Windows.Forms.ScrollEventHandler(this.OnScrlBarTransparent_Scroll);
			
			
			
			this.lbFont.AccessibleDescription = ((string)(resources.GetObject("lbFont.AccessibleDescription")));
			this.lbFont.AccessibleName = ((string)(resources.GetObject("lbFont.AccessibleName")));
			this.lbFont.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbFont.Anchor")));
			this.lbFont.AutoSize = ((bool)(resources.GetObject("lbFont.AutoSize")));
			this.lbFont.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbFont.Dock")));
			this.lbFont.Enabled = ((bool)(resources.GetObject("lbFont.Enabled")));
			this.lbFont.Font = ((System.Drawing.Font)(resources.GetObject("lbFont.Font")));
			this.lbFont.Image = ((System.Drawing.Image)(resources.GetObject("lbFont.Image")));
			this.lbFont.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbFont.ImageAlign")));
			this.lbFont.ImageIndex = ((int)(resources.GetObject("lbFont.ImageIndex")));
			this.lbFont.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbFont.ImeMode")));
			this.lbFont.Location = ((System.Drawing.Point)(resources.GetObject("lbFont.Location")));
			this.lbFont.Name = "lbFont";
			this.lbFont.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbFont.RightToLeft")));
			this.lbFont.Size = ((System.Drawing.Size)(resources.GetObject("lbFont.Size")));
			this.lbFont.TabIndex = ((int)(resources.GetObject("lbFont.TabIndex")));
			this.lbFont.Tag = "";
			this.lbFont.Text = resources.GetString("lbFont.Text");
			this.lbFont.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbFont.TextAlign")));
			this.lbFont.Visible = ((bool)(resources.GetObject("lbFont.Visible")));
			
			
			
			this.cmbWatermarkFontSize.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cmbWatermarkFontSize.Anchor")));
			this.cmbWatermarkFontSize.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmbWatermarkFontSize.BackgroundImage")));
			this.cmbWatermarkFontSize.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cmbWatermarkFontSize.Dock")));
			this.cmbWatermarkFontSize.EditValue = ((object)(resources.GetObject("cmbWatermarkFontSize.EditValue")));
			this.cmbWatermarkFontSize.Enabled = ((bool)(resources.GetObject("cmbWatermarkFontSize.Enabled")));
			this.cmbWatermarkFontSize.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cmbWatermarkFontSize.ImeMode")));
			this.cmbWatermarkFontSize.Location = ((System.Drawing.Point)(resources.GetObject("cmbWatermarkFontSize.Location")));
			this.cmbWatermarkFontSize.Name = "cmbWatermarkFontSize";
			
			
			
			this.cmbWatermarkFontSize.Properties.AccessibleDescription = ((string)(resources.GetObject("cmbWatermarkFontSize.Properties.AccessibleDescription")));
			this.cmbWatermarkFontSize.Properties.AccessibleName = ((string)(resources.GetObject("cmbWatermarkFontSize.Properties.AccessibleName")));
			this.cmbWatermarkFontSize.Properties.AutoHeight = ((bool)(resources.GetObject("cmbWatermarkFontSize.Properties.AutoHeight")));
			this.cmbWatermarkFontSize.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
																														 new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.cmbWatermarkFontSize.Properties.Mask.AutoComplete = ((DevExpress.XtraEditors.Mask.AutoCompleteType)(resources.GetObject("cmbWatermarkFontSize.Properties.Mask.AutoComplete")));
			this.cmbWatermarkFontSize.Properties.Mask.BeepOnError = ((bool)(resources.GetObject("cmbWatermarkFontSize.Properties.Mask.BeepOnError")));
			this.cmbWatermarkFontSize.Properties.Mask.EditMask = resources.GetString("cmbWatermarkFontSize.Properties.Mask.EditMask");
			this.cmbWatermarkFontSize.Properties.Mask.IgnoreMaskBlank = ((bool)(resources.GetObject("cmbWatermarkFontSize.Properties.Mask.IgnoreMaskBlank")));
			this.cmbWatermarkFontSize.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("cmbWatermarkFontSize.Properties.Mask.MaskType")));
			this.cmbWatermarkFontSize.Properties.Mask.PlaceHolder = ((char)(resources.GetObject("cmbWatermarkFontSize.Properties.Mask.PlaceHolder")));
			this.cmbWatermarkFontSize.Properties.Mask.SaveLiteral = ((bool)(resources.GetObject("cmbWatermarkFontSize.Properties.Mask.SaveLiteral")));
			this.cmbWatermarkFontSize.Properties.Mask.ShowPlaceHolders = ((bool)(resources.GetObject("cmbWatermarkFontSize.Properties.Mask.ShowPlaceHolders")));
			this.cmbWatermarkFontSize.Properties.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("cmbWatermarkFontSize.Properties.Mask.UseMaskAsDisplayFormat")));
			this.cmbWatermarkFontSize.Properties.NullText = resources.GetString("cmbWatermarkFontSize.Properties.NullText");
			this.cmbWatermarkFontSize.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cmbWatermarkFontSize.RightToLeft")));
			this.cmbWatermarkFontSize.Size = ((System.Drawing.Size)(resources.GetObject("cmbWatermarkFontSize.Size")));
			this.cmbWatermarkFontSize.TabIndex = ((int)(resources.GetObject("cmbWatermarkFontSize.TabIndex")));
			this.cmbWatermarkFontSize.Tag = "";
			this.cmbWatermarkFontSize.ToolTip = resources.GetString("cmbWatermarkFontSize.ToolTip");
			this.cmbWatermarkFontSize.ToolTipIconType = ((DevExpress.Utils.ToolTipIconType)(resources.GetObject("cmbWatermarkFontSize.ToolTipIconType")));
			this.cmbWatermarkFontSize.ToolTipTitle = resources.GetString("cmbWatermarkFontSize.ToolTipTitle");
			this.cmbWatermarkFontSize.Visible = ((bool)(resources.GetObject("cmbWatermarkFontSize.Visible")));
			this.cmbWatermarkFontSize.TextChanged += new System.EventHandler(this.OnEditValueChanged);
			
			
			
			this.cmbWatermarkFont.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cmbWatermarkFont.Anchor")));
			this.cmbWatermarkFont.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmbWatermarkFont.BackgroundImage")));
			this.cmbWatermarkFont.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cmbWatermarkFont.Dock")));
			this.cmbWatermarkFont.EditValue = ((object)(resources.GetObject("cmbWatermarkFont.EditValue")));
			this.cmbWatermarkFont.Enabled = ((bool)(resources.GetObject("cmbWatermarkFont.Enabled")));
			this.cmbWatermarkFont.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cmbWatermarkFont.ImeMode")));
			this.cmbWatermarkFont.Location = ((System.Drawing.Point)(resources.GetObject("cmbWatermarkFont.Location")));
			this.cmbWatermarkFont.Name = "cmbWatermarkFont";
			
			
			
			this.cmbWatermarkFont.Properties.AccessibleDescription = ((string)(resources.GetObject("cmbWatermarkFont.Properties.AccessibleDescription")));
			this.cmbWatermarkFont.Properties.AccessibleName = ((string)(resources.GetObject("cmbWatermarkFont.Properties.AccessibleName")));
			this.cmbWatermarkFont.Properties.AutoHeight = ((bool)(resources.GetObject("cmbWatermarkFont.Properties.AutoHeight")));
			this.cmbWatermarkFont.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
																													 new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.cmbWatermarkFont.Properties.Mask.AutoComplete = ((DevExpress.XtraEditors.Mask.AutoCompleteType)(resources.GetObject("cmbWatermarkFont.Properties.Mask.AutoComplete")));
			this.cmbWatermarkFont.Properties.Mask.BeepOnError = ((bool)(resources.GetObject("cmbWatermarkFont.Properties.Mask.BeepOnError")));
			this.cmbWatermarkFont.Properties.Mask.EditMask = resources.GetString("cmbWatermarkFont.Properties.Mask.EditMask");
			this.cmbWatermarkFont.Properties.Mask.IgnoreMaskBlank = ((bool)(resources.GetObject("cmbWatermarkFont.Properties.Mask.IgnoreMaskBlank")));
			this.cmbWatermarkFont.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("cmbWatermarkFont.Properties.Mask.MaskType")));
			this.cmbWatermarkFont.Properties.Mask.PlaceHolder = ((char)(resources.GetObject("cmbWatermarkFont.Properties.Mask.PlaceHolder")));
			this.cmbWatermarkFont.Properties.Mask.SaveLiteral = ((bool)(resources.GetObject("cmbWatermarkFont.Properties.Mask.SaveLiteral")));
			this.cmbWatermarkFont.Properties.Mask.ShowPlaceHolders = ((bool)(resources.GetObject("cmbWatermarkFont.Properties.Mask.ShowPlaceHolders")));
			this.cmbWatermarkFont.Properties.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("cmbWatermarkFont.Properties.Mask.UseMaskAsDisplayFormat")));
			this.cmbWatermarkFont.Properties.NullText = resources.GetString("cmbWatermarkFont.Properties.NullText");
			this.cmbWatermarkFont.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			this.cmbWatermarkFont.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cmbWatermarkFont.RightToLeft")));
			this.cmbWatermarkFont.Size = ((System.Drawing.Size)(resources.GetObject("cmbWatermarkFont.Size")));
			this.cmbWatermarkFont.TabIndex = ((int)(resources.GetObject("cmbWatermarkFont.TabIndex")));
			this.cmbWatermarkFont.Tag = "";
			this.cmbWatermarkFont.ToolTip = resources.GetString("cmbWatermarkFont.ToolTip");
			this.cmbWatermarkFont.ToolTipIconType = ((DevExpress.Utils.ToolTipIconType)(resources.GetObject("cmbWatermarkFont.ToolTipIconType")));
			this.cmbWatermarkFont.ToolTipTitle = resources.GetString("cmbWatermarkFont.ToolTipTitle");
			this.cmbWatermarkFont.Visible = ((bool)(resources.GetObject("cmbWatermarkFont.Visible")));
			this.cmbWatermarkFont.EditValueChanged += new System.EventHandler(this.OnEditValueChanged);
			
			
			
			this.cmbWatermarkText.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cmbWatermarkText.Anchor")));
			this.cmbWatermarkText.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmbWatermarkText.BackgroundImage")));
			this.cmbWatermarkText.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cmbWatermarkText.Dock")));
			this.cmbWatermarkText.EditValue = ((object)(resources.GetObject("cmbWatermarkText.EditValue")));
			this.cmbWatermarkText.Enabled = ((bool)(resources.GetObject("cmbWatermarkText.Enabled")));
			this.cmbWatermarkText.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cmbWatermarkText.ImeMode")));
			this.cmbWatermarkText.Location = ((System.Drawing.Point)(resources.GetObject("cmbWatermarkText.Location")));
			this.cmbWatermarkText.Name = "cmbWatermarkText";
			
			
			
			this.cmbWatermarkText.Properties.AccessibleDescription = ((string)(resources.GetObject("cmbWatermarkText.Properties.AccessibleDescription")));
			this.cmbWatermarkText.Properties.AccessibleName = ((string)(resources.GetObject("cmbWatermarkText.Properties.AccessibleName")));
			this.cmbWatermarkText.Properties.AutoHeight = ((bool)(resources.GetObject("cmbWatermarkText.Properties.AutoHeight")));
			this.cmbWatermarkText.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
																													 new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.cmbWatermarkText.Properties.Mask.AutoComplete = ((DevExpress.XtraEditors.Mask.AutoCompleteType)(resources.GetObject("cmbWatermarkText.Properties.Mask.AutoComplete")));
			this.cmbWatermarkText.Properties.Mask.BeepOnError = ((bool)(resources.GetObject("cmbWatermarkText.Properties.Mask.BeepOnError")));
			this.cmbWatermarkText.Properties.Mask.EditMask = resources.GetString("cmbWatermarkText.Properties.Mask.EditMask");
			this.cmbWatermarkText.Properties.Mask.IgnoreMaskBlank = ((bool)(resources.GetObject("cmbWatermarkText.Properties.Mask.IgnoreMaskBlank")));
			this.cmbWatermarkText.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("cmbWatermarkText.Properties.Mask.MaskType")));
			this.cmbWatermarkText.Properties.Mask.PlaceHolder = ((char)(resources.GetObject("cmbWatermarkText.Properties.Mask.PlaceHolder")));
			this.cmbWatermarkText.Properties.Mask.SaveLiteral = ((bool)(resources.GetObject("cmbWatermarkText.Properties.Mask.SaveLiteral")));
			this.cmbWatermarkText.Properties.Mask.ShowPlaceHolders = ((bool)(resources.GetObject("cmbWatermarkText.Properties.Mask.ShowPlaceHolders")));
			this.cmbWatermarkText.Properties.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("cmbWatermarkText.Properties.Mask.UseMaskAsDisplayFormat")));
			this.cmbWatermarkText.Properties.NullText = resources.GetString("cmbWatermarkText.Properties.NullText");
			this.cmbWatermarkText.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cmbWatermarkText.RightToLeft")));
			this.cmbWatermarkText.Size = ((System.Drawing.Size)(resources.GetObject("cmbWatermarkText.Size")));
			this.cmbWatermarkText.TabIndex = ((int)(resources.GetObject("cmbWatermarkText.TabIndex")));
			this.cmbWatermarkText.Tag = "";
			this.cmbWatermarkText.ToolTip = resources.GetString("cmbWatermarkText.ToolTip");
			this.cmbWatermarkText.ToolTipIconType = ((DevExpress.Utils.ToolTipIconType)(resources.GetObject("cmbWatermarkText.ToolTipIconType")));
			this.cmbWatermarkText.ToolTipTitle = resources.GetString("cmbWatermarkText.ToolTipTitle");
			this.cmbWatermarkText.Visible = ((bool)(resources.GetObject("cmbWatermarkText.Visible")));
			this.cmbWatermarkText.TextChanged += new System.EventHandler(this.OnEditValueChanged);
			
			
			
			this.lkpTextDirection.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lkpTextDirection.Anchor")));
			this.lkpTextDirection.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("lkpTextDirection.BackgroundImage")));
			this.lkpTextDirection.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lkpTextDirection.Dock")));
			this.lkpTextDirection.EditValue = ((object)(resources.GetObject("lkpTextDirection.EditValue")));
			this.lkpTextDirection.Enabled = ((bool)(resources.GetObject("lkpTextDirection.Enabled")));
			this.lkpTextDirection.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lkpTextDirection.ImeMode")));
			this.lkpTextDirection.Location = ((System.Drawing.Point)(resources.GetObject("lkpTextDirection.Location")));
			this.lkpTextDirection.Name = "lkpTextDirection";
			
			
			
			this.lkpTextDirection.Properties.AccessibleDescription = ((string)(resources.GetObject("lkpTextDirection.Properties.AccessibleDescription")));
			this.lkpTextDirection.Properties.AccessibleName = ((string)(resources.GetObject("lkpTextDirection.Properties.AccessibleName")));
			this.lkpTextDirection.Properties.AutoHeight = ((bool)(resources.GetObject("lkpTextDirection.Properties.AutoHeight")));
			this.lkpTextDirection.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
																													 new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.lkpTextDirection.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
																														 new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Text")});
			this.lkpTextDirection.Properties.Mask.AutoComplete = ((DevExpress.XtraEditors.Mask.AutoCompleteType)(resources.GetObject("lkpTextDirection.Properties.Mask.AutoComplete")));
			this.lkpTextDirection.Properties.Mask.BeepOnError = ((bool)(resources.GetObject("lkpTextDirection.Properties.Mask.BeepOnError")));
			this.lkpTextDirection.Properties.Mask.EditMask = resources.GetString("lkpTextDirection.Properties.Mask.EditMask");
			this.lkpTextDirection.Properties.Mask.IgnoreMaskBlank = ((bool)(resources.GetObject("lkpTextDirection.Properties.Mask.IgnoreMaskBlank")));
			this.lkpTextDirection.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("lkpTextDirection.Properties.Mask.MaskType")));
			this.lkpTextDirection.Properties.Mask.PlaceHolder = ((char)(resources.GetObject("lkpTextDirection.Properties.Mask.PlaceHolder")));
			this.lkpTextDirection.Properties.Mask.SaveLiteral = ((bool)(resources.GetObject("lkpTextDirection.Properties.Mask.SaveLiteral")));
			this.lkpTextDirection.Properties.Mask.ShowPlaceHolders = ((bool)(resources.GetObject("lkpTextDirection.Properties.Mask.ShowPlaceHolders")));
			this.lkpTextDirection.Properties.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("lkpTextDirection.Properties.Mask.UseMaskAsDisplayFormat")));
			this.lkpTextDirection.Properties.NullText = resources.GetString("lkpTextDirection.Properties.NullText");
			this.lkpTextDirection.Properties.ShowFooter = false;
			this.lkpTextDirection.Properties.ShowHeader = false;
			this.lkpTextDirection.Properties.ShowLines = false;
			this.lkpTextDirection.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lkpTextDirection.RightToLeft")));
			this.lkpTextDirection.Size = ((System.Drawing.Size)(resources.GetObject("lkpTextDirection.Size")));
			this.lkpTextDirection.TabIndex = ((int)(resources.GetObject("lkpTextDirection.TabIndex")));
			this.lkpTextDirection.Tag = "";
			this.lkpTextDirection.ToolTip = resources.GetString("lkpTextDirection.ToolTip");
			this.lkpTextDirection.ToolTipIconType = ((DevExpress.Utils.ToolTipIconType)(resources.GetObject("lkpTextDirection.ToolTipIconType")));
			this.lkpTextDirection.ToolTipTitle = resources.GetString("lkpTextDirection.ToolTipTitle");
			this.lkpTextDirection.Visible = ((bool)(resources.GetObject("lkpTextDirection.Visible")));
			this.lkpTextDirection.EditValueChanged += new System.EventHandler(this.OnEditValueChanged);
			
			
			
			this.lbText.AccessibleDescription = ((string)(resources.GetObject("lbText.AccessibleDescription")));
			this.lbText.AccessibleName = ((string)(resources.GetObject("lbText.AccessibleName")));
			this.lbText.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbText.Anchor")));
			this.lbText.AutoSize = ((bool)(resources.GetObject("lbText.AutoSize")));
			this.lbText.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbText.Dock")));
			this.lbText.Enabled = ((bool)(resources.GetObject("lbText.Enabled")));
			this.lbText.Font = ((System.Drawing.Font)(resources.GetObject("lbText.Font")));
			this.lbText.Image = ((System.Drawing.Image)(resources.GetObject("lbText.Image")));
			this.lbText.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbText.ImageAlign")));
			this.lbText.ImageIndex = ((int)(resources.GetObject("lbText.ImageIndex")));
			this.lbText.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbText.ImeMode")));
			this.lbText.Location = ((System.Drawing.Point)(resources.GetObject("lbText.Location")));
			this.lbText.Name = "lbText";
			this.lbText.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbText.RightToLeft")));
			this.lbText.Size = ((System.Drawing.Size)(resources.GetObject("lbText.Size")));
			this.lbText.TabIndex = ((int)(resources.GetObject("lbText.TabIndex")));
			this.lbText.Tag = "";
			this.lbText.Text = resources.GetString("lbText.Text");
			this.lbText.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbText.TextAlign")));
			this.lbText.Visible = ((bool)(resources.GetObject("lbText.Visible")));
			
			
			
			this.teTransparentValue.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("teTransparentValue.Anchor")));
			this.teTransparentValue.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("teTransparentValue.BackgroundImage")));
			this.teTransparentValue.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("teTransparentValue.Dock")));
			this.teTransparentValue.EditValue = resources.GetString("teTransparentValue.EditValue");
			this.teTransparentValue.Enabled = ((bool)(resources.GetObject("teTransparentValue.Enabled")));
			this.teTransparentValue.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("teTransparentValue.ImeMode")));
			this.teTransparentValue.Location = ((System.Drawing.Point)(resources.GetObject("teTransparentValue.Location")));
			this.teTransparentValue.Name = "teTransparentValue";
			
			
			
			this.teTransparentValue.Properties.AccessibleDescription = ((string)(resources.GetObject("teTransparentValue.Properties.AccessibleDescription")));
			this.teTransparentValue.Properties.AccessibleName = ((string)(resources.GetObject("teTransparentValue.Properties.AccessibleName")));
			this.teTransparentValue.Properties.AutoHeight = ((bool)(resources.GetObject("teTransparentValue.Properties.AutoHeight")));
			this.teTransparentValue.Properties.Mask.AutoComplete = ((DevExpress.XtraEditors.Mask.AutoCompleteType)(resources.GetObject("teTransparentValue.Properties.Mask.AutoComplete")));
			this.teTransparentValue.Properties.Mask.BeepOnError = ((bool)(resources.GetObject("teTransparentValue.Properties.Mask.BeepOnError")));
			this.teTransparentValue.Properties.Mask.EditMask = resources.GetString("teTransparentValue.Properties.Mask.EditMask");
			this.teTransparentValue.Properties.Mask.IgnoreMaskBlank = ((bool)(resources.GetObject("teTransparentValue.Properties.Mask.IgnoreMaskBlank")));
			this.teTransparentValue.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("teTransparentValue.Properties.Mask.MaskType")));
			this.teTransparentValue.Properties.Mask.PlaceHolder = ((char)(resources.GetObject("teTransparentValue.Properties.Mask.PlaceHolder")));
			this.teTransparentValue.Properties.Mask.SaveLiteral = ((bool)(resources.GetObject("teTransparentValue.Properties.Mask.SaveLiteral")));
			this.teTransparentValue.Properties.Mask.ShowPlaceHolders = ((bool)(resources.GetObject("teTransparentValue.Properties.Mask.ShowPlaceHolders")));
			this.teTransparentValue.Properties.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("teTransparentValue.Properties.Mask.UseMaskAsDisplayFormat")));
			this.teTransparentValue.Properties.NullText = resources.GetString("teTransparentValue.Properties.NullText");
			this.teTransparentValue.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("teTransparentValue.RightToLeft")));
			this.teTransparentValue.Size = ((System.Drawing.Size)(resources.GetObject("teTransparentValue.Size")));
			this.teTransparentValue.TabIndex = ((int)(resources.GetObject("teTransparentValue.TabIndex")));
			this.teTransparentValue.Tag = "";
			this.teTransparentValue.ToolTip = resources.GetString("teTransparentValue.ToolTip");
			this.teTransparentValue.ToolTipIconType = ((DevExpress.Utils.ToolTipIconType)(resources.GetObject("teTransparentValue.ToolTipIconType")));
			this.teTransparentValue.ToolTipTitle = resources.GetString("teTransparentValue.ToolTipTitle");
			this.teTransparentValue.Visible = ((bool)(resources.GetObject("teTransparentValue.Visible")));
			this.teTransparentValue.EditValueChanged += new System.EventHandler(this.teTransparentValue_EditValueChanged);
			
			
			
			this.lbLayout.AccessibleDescription = ((string)(resources.GetObject("lbLayout.AccessibleDescription")));
			this.lbLayout.AccessibleName = ((string)(resources.GetObject("lbLayout.AccessibleName")));
			this.lbLayout.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbLayout.Anchor")));
			this.lbLayout.AutoSize = ((bool)(resources.GetObject("lbLayout.AutoSize")));
			this.lbLayout.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbLayout.Dock")));
			this.lbLayout.Enabled = ((bool)(resources.GetObject("lbLayout.Enabled")));
			this.lbLayout.Font = ((System.Drawing.Font)(resources.GetObject("lbLayout.Font")));
			this.lbLayout.Image = ((System.Drawing.Image)(resources.GetObject("lbLayout.Image")));
			this.lbLayout.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbLayout.ImageAlign")));
			this.lbLayout.ImageIndex = ((int)(resources.GetObject("lbLayout.ImageIndex")));
			this.lbLayout.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbLayout.ImeMode")));
			this.lbLayout.Location = ((System.Drawing.Point)(resources.GetObject("lbLayout.Location")));
			this.lbLayout.Name = "lbLayout";
			this.lbLayout.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbLayout.RightToLeft")));
			this.lbLayout.Size = ((System.Drawing.Size)(resources.GetObject("lbLayout.Size")));
			this.lbLayout.TabIndex = ((int)(resources.GetObject("lbLayout.TabIndex")));
			this.lbLayout.Tag = "";
			this.lbLayout.Text = resources.GetString("lbLayout.Text");
			this.lbLayout.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbLayout.TextAlign")));
			this.lbLayout.Visible = ((bool)(resources.GetObject("lbLayout.Visible")));
			
			
			
			this.lkpImageVAlign.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lkpImageVAlign.Anchor")));
			this.lkpImageVAlign.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("lkpImageVAlign.BackgroundImage")));
			this.lkpImageVAlign.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lkpImageVAlign.Dock")));
			this.lkpImageVAlign.EditValue = ((object)(resources.GetObject("lkpImageVAlign.EditValue")));
			this.lkpImageVAlign.Enabled = ((bool)(resources.GetObject("lkpImageVAlign.Enabled")));
			this.lkpImageVAlign.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lkpImageVAlign.ImeMode")));
			this.lkpImageVAlign.Location = ((System.Drawing.Point)(resources.GetObject("lkpImageVAlign.Location")));
			this.lkpImageVAlign.Name = "lkpImageVAlign";
			
			
			
			this.lkpImageVAlign.Properties.AccessibleDescription = ((string)(resources.GetObject("lkpImageVAlign.Properties.AccessibleDescription")));
			this.lkpImageVAlign.Properties.AccessibleName = ((string)(resources.GetObject("lkpImageVAlign.Properties.AccessibleName")));
			this.lkpImageVAlign.Properties.AutoHeight = ((bool)(resources.GetObject("lkpImageVAlign.Properties.AutoHeight")));
			this.lkpImageVAlign.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
																												   new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.lkpImageVAlign.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
																													   new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Text")});
			this.lkpImageVAlign.Properties.Mask.AutoComplete = ((DevExpress.XtraEditors.Mask.AutoCompleteType)(resources.GetObject("lkpImageVAlign.Properties.Mask.AutoComplete")));
			this.lkpImageVAlign.Properties.Mask.BeepOnError = ((bool)(resources.GetObject("lkpImageVAlign.Properties.Mask.BeepOnError")));
			this.lkpImageVAlign.Properties.Mask.EditMask = resources.GetString("lkpImageVAlign.Properties.Mask.EditMask");
			this.lkpImageVAlign.Properties.Mask.IgnoreMaskBlank = ((bool)(resources.GetObject("lkpImageVAlign.Properties.Mask.IgnoreMaskBlank")));
			this.lkpImageVAlign.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("lkpImageVAlign.Properties.Mask.MaskType")));
			this.lkpImageVAlign.Properties.Mask.PlaceHolder = ((char)(resources.GetObject("lkpImageVAlign.Properties.Mask.PlaceHolder")));
			this.lkpImageVAlign.Properties.Mask.SaveLiteral = ((bool)(resources.GetObject("lkpImageVAlign.Properties.Mask.SaveLiteral")));
			this.lkpImageVAlign.Properties.Mask.ShowPlaceHolders = ((bool)(resources.GetObject("lkpImageVAlign.Properties.Mask.ShowPlaceHolders")));
			this.lkpImageVAlign.Properties.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("lkpImageVAlign.Properties.Mask.UseMaskAsDisplayFormat")));
			this.lkpImageVAlign.Properties.NullText = resources.GetString("lkpImageVAlign.Properties.NullText");
			this.lkpImageVAlign.Properties.ShowFooter = false;
			this.lkpImageVAlign.Properties.ShowHeader = false;
			this.lkpImageVAlign.Properties.ShowLines = false;
			this.lkpImageVAlign.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lkpImageVAlign.RightToLeft")));
			this.lkpImageVAlign.Size = ((System.Drawing.Size)(resources.GetObject("lkpImageVAlign.Size")));
			this.lkpImageVAlign.TabIndex = ((int)(resources.GetObject("lkpImageVAlign.TabIndex")));
			this.lkpImageVAlign.ToolTip = resources.GetString("lkpImageVAlign.ToolTip");
			this.lkpImageVAlign.ToolTipIconType = ((DevExpress.Utils.ToolTipIconType)(resources.GetObject("lkpImageVAlign.ToolTipIconType")));
			this.lkpImageVAlign.ToolTipTitle = resources.GetString("lkpImageVAlign.ToolTipTitle");
			this.lkpImageVAlign.Visible = ((bool)(resources.GetObject("lkpImageVAlign.Visible")));
			this.lkpImageVAlign.EditValueChanged += new System.EventHandler(this.OnEditValueChanged);
			
			
			
			this.lbVertAlign.AccessibleDescription = ((string)(resources.GetObject("lbVertAlign.AccessibleDescription")));
			this.lbVertAlign.AccessibleName = ((string)(resources.GetObject("lbVertAlign.AccessibleName")));
			this.lbVertAlign.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lbVertAlign.Anchor")));
			this.lbVertAlign.AutoSize = ((bool)(resources.GetObject("lbVertAlign.AutoSize")));
			this.lbVertAlign.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lbVertAlign.Dock")));
			this.lbVertAlign.Enabled = ((bool)(resources.GetObject("lbVertAlign.Enabled")));
			this.lbVertAlign.Font = ((System.Drawing.Font)(resources.GetObject("lbVertAlign.Font")));
			this.lbVertAlign.Image = ((System.Drawing.Image)(resources.GetObject("lbVertAlign.Image")));
			this.lbVertAlign.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbVertAlign.ImageAlign")));
			this.lbVertAlign.ImageIndex = ((int)(resources.GetObject("lbVertAlign.ImageIndex")));
			this.lbVertAlign.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lbVertAlign.ImeMode")));
			this.lbVertAlign.Location = ((System.Drawing.Point)(resources.GetObject("lbVertAlign.Location")));
			this.lbVertAlign.Name = "lbVertAlign";
			this.lbVertAlign.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lbVertAlign.RightToLeft")));
			this.lbVertAlign.Size = ((System.Drawing.Size)(resources.GetObject("lbVertAlign.Size")));
			this.lbVertAlign.TabIndex = ((int)(resources.GetObject("lbVertAlign.TabIndex")));
			this.lbVertAlign.Text = resources.GetString("lbVertAlign.Text");
			this.lbVertAlign.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("lbVertAlign.TextAlign")));
			this.lbVertAlign.Visible = ((bool)(resources.GetObject("lbVertAlign.Visible")));
			
			
			
			this.pc.AccessibleDescription = ((string)(resources.GetObject("pc.AccessibleDescription")));
			this.pc.AccessibleName = ((string)(resources.GetObject("pc.AccessibleName")));
			this.pc.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("pc.Anchor")));
			this.pc.AutoScroll = ((bool)(resources.GetObject("pc.AutoScroll")));
			this.pc.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("pc.AutoScrollMargin")));
			this.pc.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("pc.AutoScrollMinSize")));
			this.pc.BackColor = ((System.Drawing.Color)(resources.GetObject("pc.BackColor")));
			this.pc.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pc.BackgroundImage")));
			this.pc.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("pc.Dock")));
			this.pc.Enabled = ((bool)(resources.GetObject("pc.Enabled")));
			this.pc.Font = ((System.Drawing.Font)(resources.GetObject("pc.Font")));
			this.pc.ForeColor = ((System.Drawing.Color)(resources.GetObject("pc.ForeColor")));
			this.pc.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("pc.ImeMode")));
			this.pc.IsMetric = ((bool)(resources.GetObject("pc.IsMetric")));
			this.pc.Location = ((System.Drawing.Point)(resources.GetObject("pc.Location")));
			this.pc.Name = "pc";
			this.pc.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("pc.RightToLeft")));
			this.pc.ShowPageMargins = false;
			this.pc.Size = ((System.Drawing.Size)(resources.GetObject("pc.Size")));
			this.pc.TabIndex = ((int)(resources.GetObject("pc.TabIndex")));
			this.pc.TabStop = false;
			this.pc.TooltipBackColor = ((System.Drawing.Color)(resources.GetObject("pc.TooltipBackColor")));
			this.pc.TooltipFont = ((System.Drawing.Font)(resources.GetObject("pc.TooltipFont")));
			this.pc.TooltipForeColor = ((System.Drawing.Color)(resources.GetObject("pc.TooltipForeColor")));
			this.pc.Visible = ((bool)(resources.GetObject("pc.Visible")));
			this.pc.Zoom = 0.2297794F;
			
			
			
			this.rgrpZOrder.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("rgrpZOrder.Anchor")));
			this.rgrpZOrder.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("rgrpZOrder.BackgroundImage")));
			this.rgrpZOrder.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("rgrpZOrder.Dock")));
			this.rgrpZOrder.EditValue = ((bool)(resources.GetObject("rgrpZOrder.EditValue")));
			this.rgrpZOrder.Enabled = ((bool)(resources.GetObject("rgrpZOrder.Enabled")));
			this.rgrpZOrder.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("rgrpZOrder.ImeMode")));
			this.rgrpZOrder.Location = ((System.Drawing.Point)(resources.GetObject("rgrpZOrder.Location")));
			this.rgrpZOrder.Name = "rgrpZOrder";
			
			
			
			this.rgrpZOrder.Properties.AccessibleDescription = ((string)(resources.GetObject("rgrpZOrder.Properties.AccessibleDescription")));
			this.rgrpZOrder.Properties.AccessibleName = ((string)(resources.GetObject("rgrpZOrder.Properties.AccessibleName")));
			this.rgrpZOrder.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.rgrpZOrder.Properties.Appearance.Options.UseBackColor = true;
			this.rgrpZOrder.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.rgrpZOrder.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
																											   new DevExpress.XtraEditors.Controls.RadioGroupItem(false, "In &front"),
																											   new DevExpress.XtraEditors.Controls.RadioGroupItem(true, "&Behind")});
			this.rgrpZOrder.Properties.NullText = resources.GetString("rgrpZOrder.Properties.NullText");
			this.rgrpZOrder.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("rgrpZOrder.RightToLeft")));
			this.rgrpZOrder.Size = ((System.Drawing.Size)(resources.GetObject("rgrpZOrder.Size")));
			this.rgrpZOrder.TabIndex = ((int)(resources.GetObject("rgrpZOrder.TabIndex")));
			this.rgrpZOrder.ToolTip = resources.GetString("rgrpZOrder.ToolTip");
			this.rgrpZOrder.ToolTipIconType = ((DevExpress.Utils.ToolTipIconType)(resources.GetObject("rgrpZOrder.ToolTipIconType")));
			this.rgrpZOrder.ToolTipTitle = resources.GetString("rgrpZOrder.ToolTipTitle");
			this.rgrpZOrder.Visible = ((bool)(resources.GetObject("rgrpZOrder.Visible")));
			this.rgrpZOrder.EditValueChanged += new System.EventHandler(this.OnEditValueChanged);
			
			
			
			this.xtraTabControl.AccessibleDescription = ((string)(resources.GetObject("xtraTabControl.AccessibleDescription")));
			this.xtraTabControl.AccessibleName = ((string)(resources.GetObject("xtraTabControl.AccessibleName")));
			this.xtraTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("xtraTabControl.Anchor")));
			this.xtraTabControl.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("xtraTabControl.BackgroundImage")));
			this.xtraTabControl.BorderStyle = ((DevExpress.XtraEditors.Controls.BorderStyles)(resources.GetObject("xtraTabControl.BorderStyle")));
			this.xtraTabControl.BorderStylePage = ((DevExpress.XtraEditors.Controls.BorderStyles)(resources.GetObject("xtraTabControl.BorderStylePage")));
			this.xtraTabControl.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("xtraTabControl.Dock")));
			this.xtraTabControl.Enabled = ((bool)(resources.GetObject("xtraTabControl.Enabled")));
			this.xtraTabControl.Font = ((System.Drawing.Font)(resources.GetObject("xtraTabControl.Font")));
			this.xtraTabControl.HeaderAutoFill = ((DevExpress.Utils.DefaultBoolean)(resources.GetObject("xtraTabControl.HeaderAutoFill")));
			this.xtraTabControl.HeaderButtons = ((DevExpress.XtraTab.TabButtons)(resources.GetObject("xtraTabControl.HeaderButtons")));
			this.xtraTabControl.HeaderButtonsShowMode = ((DevExpress.XtraTab.TabButtonShowMode)(resources.GetObject("xtraTabControl.HeaderButtonsShowMode")));
			this.xtraTabControl.HeaderLocation = ((DevExpress.XtraTab.TabHeaderLocation)(resources.GetObject("xtraTabControl.HeaderLocation")));
			this.xtraTabControl.HeaderOrientation = ((DevExpress.XtraTab.TabOrientation)(resources.GetObject("xtraTabControl.HeaderOrientation")));
			this.xtraTabControl.Images = ((object)(resources.GetObject("xtraTabControl.Images")));
			this.xtraTabControl.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("xtraTabControl.ImeMode")));
			this.xtraTabControl.Location = ((System.Drawing.Point)(resources.GetObject("xtraTabControl.Location")));
			this.xtraTabControl.MultiLine = ((DevExpress.Utils.DefaultBoolean)(resources.GetObject("xtraTabControl.MultiLine")));
			this.xtraTabControl.Name = "xtraTabControl";
			this.xtraTabControl.PageImagePosition = ((DevExpress.XtraTab.TabPageImagePosition)(resources.GetObject("xtraTabControl.PageImagePosition")));
			this.xtraTabControl.PaintStyleName = resources.GetString("xtraTabControl.PaintStyleName");
			this.xtraTabControl.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("xtraTabControl.RightToLeft")));
			this.xtraTabControl.SelectedTabPage = this.tpTextWaterMark;
			this.xtraTabControl.ShowHeaderFocus = ((DevExpress.Utils.DefaultBoolean)(resources.GetObject("xtraTabControl.ShowHeaderFocus")));
			this.xtraTabControl.ShowTabHeader = ((DevExpress.Utils.DefaultBoolean)(resources.GetObject("xtraTabControl.ShowTabHeader")));
			this.xtraTabControl.ShowToolTips = ((DevExpress.Utils.DefaultBoolean)(resources.GetObject("xtraTabControl.ShowToolTips")));
			this.xtraTabControl.Size = ((System.Drawing.Size)(resources.GetObject("xtraTabControl.Size")));
			this.xtraTabControl.TabIndex = ((int)(resources.GetObject("xtraTabControl.TabIndex")));
			this.xtraTabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
																						   this.tpTextWaterMark,
																						   this.tpPictureWatermark});
			this.xtraTabControl.Text = resources.GetString("xtraTabControl.Text");
			this.xtraTabControl.Visible = ((bool)(resources.GetObject("xtraTabControl.Visible")));
			
			
			
			this.tpTextWaterMark.AccessibleDescription = ((string)(resources.GetObject("tpTextWaterMark.AccessibleDescription")));
			this.tpTextWaterMark.AccessibleName = ((string)(resources.GetObject("tpTextWaterMark.AccessibleName")));
			this.tpTextWaterMark.AutoScroll = ((bool)(resources.GetObject("tpTextWaterMark.AutoScroll")));
			this.tpTextWaterMark.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("tpTextWaterMark.AutoScrollMargin")));
			this.tpTextWaterMark.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("tpTextWaterMark.AutoScrollMinSize")));
			this.tpTextWaterMark.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tpTextWaterMark.BackgroundImage")));
			this.tpTextWaterMark.Controls.AddRange(new System.Windows.Forms.Control[] {
																						  this.cmbWatermarkFontSize,
																						  this.cmbWatermarkText,
																						  this.lbFont,
																						  this.ceWatermarkColor,
																						  this.lkpTextDirection,
																						  this.lbFontSize,
																						  this.cmbWatermarkFont,
																						  this.chbBold,
																						  this.lbText,
																						  this.lbFontColor,
																						  this.chbItalic,
																						  this.lbLayout,
																						  this.grpBoxTransparent});
			this.tpTextWaterMark.Enabled = ((bool)(resources.GetObject("tpTextWaterMark.Enabled")));
			this.tpTextWaterMark.Font = ((System.Drawing.Font)(resources.GetObject("tpTextWaterMark.Font")));
			this.tpTextWaterMark.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tpTextWaterMark.ImeMode")));
			this.tpTextWaterMark.Name = "tpTextWaterMark";
			this.tpTextWaterMark.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tpTextWaterMark.RightToLeft")));
			this.tpTextWaterMark.Size = ((System.Drawing.Size)(resources.GetObject("tpTextWaterMark.Size")));
			this.tpTextWaterMark.Text = resources.GetString("tpTextWaterMark.Text");
			this.tpTextWaterMark.Tooltip = resources.GetString("tpTextWaterMark.Tooltip");
			
			
			
			this.grpBoxTransparent.AccessibleDescription = ((string)(resources.GetObject("grpBoxTransparent.AccessibleDescription")));
			this.grpBoxTransparent.AccessibleName = ((string)(resources.GetObject("grpBoxTransparent.AccessibleName")));
			this.grpBoxTransparent.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("grpBoxTransparent.Anchor")));
			this.grpBoxTransparent.AutoScroll = ((bool)(resources.GetObject("grpBoxTransparent.AutoScroll")));
			this.grpBoxTransparent.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("grpBoxTransparent.AutoScrollMargin")));
			this.grpBoxTransparent.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("grpBoxTransparent.AutoScrollMinSize")));
			this.grpBoxTransparent.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("grpBoxTransparent.BackgroundImage")));
			this.grpBoxTransparent.Controls.AddRange(new System.Windows.Forms.Control[] {
																							this.scrBarTransparent,
																							this.teTransparentValue});
			this.grpBoxTransparent.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("grpBoxTransparent.Dock")));
			this.grpBoxTransparent.Enabled = ((bool)(resources.GetObject("grpBoxTransparent.Enabled")));
			this.grpBoxTransparent.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("grpBoxTransparent.ImeMode")));
			this.grpBoxTransparent.Location = ((System.Drawing.Point)(resources.GetObject("grpBoxTransparent.Location")));
			this.grpBoxTransparent.Name = "grpBoxTransparent";
			this.grpBoxTransparent.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("grpBoxTransparent.RightToLeft")));
			this.grpBoxTransparent.Size = ((System.Drawing.Size)(resources.GetObject("grpBoxTransparent.Size")));
			this.grpBoxTransparent.TabIndex = ((int)(resources.GetObject("grpBoxTransparent.TabIndex")));
			this.grpBoxTransparent.Text = resources.GetString("grpBoxTransparent.Text");
			this.grpBoxTransparent.Visible = ((bool)(resources.GetObject("grpBoxTransparent.Visible")));
			
			
			
			this.tpPictureWatermark.AccessibleDescription = ((string)(resources.GetObject("tpPictureWatermark.AccessibleDescription")));
			this.tpPictureWatermark.AccessibleName = ((string)(resources.GetObject("tpPictureWatermark.AccessibleName")));
			this.tpPictureWatermark.AutoScroll = ((bool)(resources.GetObject("tpPictureWatermark.AutoScroll")));
			this.tpPictureWatermark.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("tpPictureWatermark.AutoScrollMargin")));
			this.tpPictureWatermark.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("tpPictureWatermark.AutoScrollMinSize")));
			this.tpPictureWatermark.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tpPictureWatermark.BackgroundImage")));
			this.tpPictureWatermark.Controls.AddRange(new System.Windows.Forms.Control[] {
																							 this.grpBoxAlign,
																							 this.lkpImageView,
																							 this.btnSelectPicture,
																							 this.lbPosition,
																							 this.chbTiling,
																							 this.grpBoxImageTransparent});
			this.tpPictureWatermark.Enabled = ((bool)(resources.GetObject("tpPictureWatermark.Enabled")));
			this.tpPictureWatermark.Font = ((System.Drawing.Font)(resources.GetObject("tpPictureWatermark.Font")));
			this.tpPictureWatermark.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("tpPictureWatermark.ImeMode")));
			this.tpPictureWatermark.Name = "tpPictureWatermark";
			this.tpPictureWatermark.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("tpPictureWatermark.RightToLeft")));
			this.tpPictureWatermark.Size = ((System.Drawing.Size)(resources.GetObject("tpPictureWatermark.Size")));
			this.tpPictureWatermark.Text = resources.GetString("tpPictureWatermark.Text");
			this.tpPictureWatermark.Tooltip = resources.GetString("tpPictureWatermark.Tooltip");
			
			
			
			this.grpBoxAlign.AccessibleDescription = ((string)(resources.GetObject("grpBoxAlign.AccessibleDescription")));
			this.grpBoxAlign.AccessibleName = ((string)(resources.GetObject("grpBoxAlign.AccessibleName")));
			this.grpBoxAlign.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("grpBoxAlign.Anchor")));
			this.grpBoxAlign.AutoScroll = ((bool)(resources.GetObject("grpBoxAlign.AutoScroll")));
			this.grpBoxAlign.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("grpBoxAlign.AutoScrollMargin")));
			this.grpBoxAlign.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("grpBoxAlign.AutoScrollMinSize")));
			this.grpBoxAlign.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("grpBoxAlign.BackgroundImage")));
			this.grpBoxAlign.Controls.AddRange(new System.Windows.Forms.Control[] {
																					  this.lbVertAlign,
																					  this.lkpImageVAlign,
																					  this.lkpImageHAlign,
																					  this.lbHorzAlign});
			this.grpBoxAlign.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("grpBoxAlign.Dock")));
			this.grpBoxAlign.Enabled = ((bool)(resources.GetObject("grpBoxAlign.Enabled")));
			this.grpBoxAlign.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("grpBoxAlign.ImeMode")));
			this.grpBoxAlign.Location = ((System.Drawing.Point)(resources.GetObject("grpBoxAlign.Location")));
			this.grpBoxAlign.Name = "grpBoxAlign";
			this.grpBoxAlign.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("grpBoxAlign.RightToLeft")));
			this.grpBoxAlign.Size = ((System.Drawing.Size)(resources.GetObject("grpBoxAlign.Size")));
			this.grpBoxAlign.TabIndex = ((int)(resources.GetObject("grpBoxAlign.TabIndex")));
			this.grpBoxAlign.Text = resources.GetString("grpBoxAlign.Text");
			this.grpBoxAlign.Visible = ((bool)(resources.GetObject("grpBoxAlign.Visible")));
			
			
			
			this.grpBoxImageTransparent.AccessibleDescription = ((string)(resources.GetObject("grpBoxImageTransparent.AccessibleDescription")));
			this.grpBoxImageTransparent.AccessibleName = ((string)(resources.GetObject("grpBoxImageTransparent.AccessibleName")));
			this.grpBoxImageTransparent.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("grpBoxImageTransparent.Anchor")));
			this.grpBoxImageTransparent.AutoScroll = ((bool)(resources.GetObject("grpBoxImageTransparent.AutoScroll")));
			this.grpBoxImageTransparent.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("grpBoxImageTransparent.AutoScrollMargin")));
			this.grpBoxImageTransparent.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("grpBoxImageTransparent.AutoScrollMinSize")));
			this.grpBoxImageTransparent.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("grpBoxImageTransparent.BackgroundImage")));
			this.grpBoxImageTransparent.Controls.AddRange(new System.Windows.Forms.Control[] {
																								 this.teImageTransparentValue,
																								 this.scrBarImageTransparent});
			this.grpBoxImageTransparent.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("grpBoxImageTransparent.Dock")));
			this.grpBoxImageTransparent.Enabled = ((bool)(resources.GetObject("grpBoxImageTransparent.Enabled")));
			this.grpBoxImageTransparent.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("grpBoxImageTransparent.ImeMode")));
			this.grpBoxImageTransparent.Location = ((System.Drawing.Point)(resources.GetObject("grpBoxImageTransparent.Location")));
			this.grpBoxImageTransparent.Name = "grpBoxImageTransparent";
			this.grpBoxImageTransparent.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("grpBoxImageTransparent.RightToLeft")));
			this.grpBoxImageTransparent.Size = ((System.Drawing.Size)(resources.GetObject("grpBoxImageTransparent.Size")));
			this.grpBoxImageTransparent.TabIndex = ((int)(resources.GetObject("grpBoxImageTransparent.TabIndex")));
			this.grpBoxImageTransparent.Text = resources.GetString("grpBoxImageTransparent.Text");
			this.grpBoxImageTransparent.Visible = ((bool)(resources.GetObject("grpBoxImageTransparent.Visible")));
			
			
			
			this.teImageTransparentValue.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("teImageTransparentValue.Anchor")));
			this.teImageTransparentValue.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("teImageTransparentValue.BackgroundImage")));
			this.teImageTransparentValue.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("teImageTransparentValue.Dock")));
			this.teImageTransparentValue.EditValue = resources.GetString("teImageTransparentValue.EditValue");
			this.teImageTransparentValue.Enabled = ((bool)(resources.GetObject("teImageTransparentValue.Enabled")));
			this.teImageTransparentValue.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("teImageTransparentValue.ImeMode")));
			this.teImageTransparentValue.Location = ((System.Drawing.Point)(resources.GetObject("teImageTransparentValue.Location")));
			this.teImageTransparentValue.Name = "teImageTransparentValue";
			
			
			
			this.teImageTransparentValue.Properties.AccessibleDescription = ((string)(resources.GetObject("teImageTransparentValue.Properties.AccessibleDescription")));
			this.teImageTransparentValue.Properties.AccessibleName = ((string)(resources.GetObject("teImageTransparentValue.Properties.AccessibleName")));
			this.teImageTransparentValue.Properties.AutoHeight = ((bool)(resources.GetObject("teImageTransparentValue.Properties.AutoHeight")));
			this.teImageTransparentValue.Properties.Mask.AutoComplete = ((DevExpress.XtraEditors.Mask.AutoCompleteType)(resources.GetObject("teImageTransparentValue.Properties.Mask.AutoComplete")));
			this.teImageTransparentValue.Properties.Mask.BeepOnError = ((bool)(resources.GetObject("teImageTransparentValue.Properties.Mask.BeepOnError")));
			this.teImageTransparentValue.Properties.Mask.EditMask = resources.GetString("teImageTransparentValue.Properties.Mask.EditMask");
			this.teImageTransparentValue.Properties.Mask.IgnoreMaskBlank = ((bool)(resources.GetObject("teImageTransparentValue.Properties.Mask.IgnoreMaskBlank")));
			this.teImageTransparentValue.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("teImageTransparentValue.Properties.Mask.MaskType")));
			this.teImageTransparentValue.Properties.Mask.PlaceHolder = ((char)(resources.GetObject("teImageTransparentValue.Properties.Mask.PlaceHolder")));
			this.teImageTransparentValue.Properties.Mask.SaveLiteral = ((bool)(resources.GetObject("teImageTransparentValue.Properties.Mask.SaveLiteral")));
			this.teImageTransparentValue.Properties.Mask.ShowPlaceHolders = ((bool)(resources.GetObject("teImageTransparentValue.Properties.Mask.ShowPlaceHolders")));
			this.teImageTransparentValue.Properties.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("teImageTransparentValue.Properties.Mask.UseMaskAsDisplayFormat")));
			this.teImageTransparentValue.Properties.NullText = resources.GetString("teImageTransparentValue.Properties.NullText");
			this.teImageTransparentValue.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("teImageTransparentValue.RightToLeft")));
			this.teImageTransparentValue.Size = ((System.Drawing.Size)(resources.GetObject("teImageTransparentValue.Size")));
			this.teImageTransparentValue.TabIndex = ((int)(resources.GetObject("teImageTransparentValue.TabIndex")));
			this.teImageTransparentValue.Tag = "";
			this.teImageTransparentValue.ToolTip = resources.GetString("teImageTransparentValue.ToolTip");
			this.teImageTransparentValue.ToolTipIconType = ((DevExpress.Utils.ToolTipIconType)(resources.GetObject("teImageTransparentValue.ToolTipIconType")));
			this.teImageTransparentValue.ToolTipTitle = resources.GetString("teImageTransparentValue.ToolTipTitle");
			this.teImageTransparentValue.Visible = ((bool)(resources.GetObject("teImageTransparentValue.Visible")));
			this.teImageTransparentValue.EditValueChanged += new System.EventHandler(this.teImageTransparentValue_EditValueChanged);
			
			
			
			this.scrBarImageTransparent.AccessibleDescription = ((string)(resources.GetObject("scrBarImageTransparent.AccessibleDescription")));
			this.scrBarImageTransparent.AccessibleName = ((string)(resources.GetObject("scrBarImageTransparent.AccessibleName")));
			this.scrBarImageTransparent.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("scrBarImageTransparent.Anchor")));
			this.scrBarImageTransparent.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("scrBarImageTransparent.Dock")));
			this.scrBarImageTransparent.Enabled = ((bool)(resources.GetObject("scrBarImageTransparent.Enabled")));
			this.scrBarImageTransparent.Location = ((System.Drawing.Point)(resources.GetObject("scrBarImageTransparent.Location")));
			this.scrBarImageTransparent.Maximum = 264;
			this.scrBarImageTransparent.Name = "scrBarImageTransparent";
			this.scrBarImageTransparent.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("scrBarImageTransparent.RightToLeft")));
			this.scrBarImageTransparent.Size = ((System.Drawing.Size)(resources.GetObject("scrBarImageTransparent.Size")));
			this.scrBarImageTransparent.TabIndex = ((int)(resources.GetObject("scrBarImageTransparent.TabIndex")));
			this.scrBarImageTransparent.Tag = "";
			this.scrBarImageTransparent.Value = 50;
			this.scrBarImageTransparent.Visible = ((bool)(resources.GetObject("scrBarImageTransparent.Visible")));
			this.scrBarImageTransparent.Scroll += new System.Windows.Forms.ScrollEventHandler(this.OnScrlBarImageTransparent_Scroll);
			
			
			
			this.grpBoxPageRange.AccessibleDescription = ((string)(resources.GetObject("grpBoxPageRange.AccessibleDescription")));
			this.grpBoxPageRange.AccessibleName = ((string)(resources.GetObject("grpBoxPageRange.AccessibleName")));
			this.grpBoxPageRange.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("grpBoxPageRange.Anchor")));
			this.grpBoxPageRange.AutoScroll = ((bool)(resources.GetObject("grpBoxPageRange.AutoScroll")));
			this.grpBoxPageRange.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("grpBoxPageRange.AutoScrollMargin")));
			this.grpBoxPageRange.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("grpBoxPageRange.AutoScrollMinSize")));
			this.grpBoxPageRange.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("grpBoxPageRange.BackgroundImage")));
			this.grpBoxPageRange.Controls.AddRange(new System.Windows.Forms.Control[] {
																						  this.lbPageRange,
																						  this.rgrpPageRange,
																						  this.tePageRange});
			this.grpBoxPageRange.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("grpBoxPageRange.Dock")));
			this.grpBoxPageRange.Enabled = ((bool)(resources.GetObject("grpBoxPageRange.Enabled")));
			this.grpBoxPageRange.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("grpBoxPageRange.ImeMode")));
			this.grpBoxPageRange.Location = ((System.Drawing.Point)(resources.GetObject("grpBoxPageRange.Location")));
			this.grpBoxPageRange.Name = "grpBoxPageRange";
			this.grpBoxPageRange.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("grpBoxPageRange.RightToLeft")));
			this.grpBoxPageRange.Size = ((System.Drawing.Size)(resources.GetObject("grpBoxPageRange.Size")));
			this.grpBoxPageRange.TabIndex = ((int)(resources.GetObject("grpBoxPageRange.TabIndex")));
			this.grpBoxPageRange.Text = resources.GetString("grpBoxPageRange.Text");
			this.grpBoxPageRange.Visible = ((bool)(resources.GetObject("grpBoxPageRange.Visible")));
			
			
			
			this.grpBoxZOrder.AccessibleDescription = ((string)(resources.GetObject("grpBoxZOrder.AccessibleDescription")));
			this.grpBoxZOrder.AccessibleName = ((string)(resources.GetObject("grpBoxZOrder.AccessibleName")));
			this.grpBoxZOrder.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("grpBoxZOrder.Anchor")));
			this.grpBoxZOrder.AutoScroll = ((bool)(resources.GetObject("grpBoxZOrder.AutoScroll")));
			this.grpBoxZOrder.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("grpBoxZOrder.AutoScrollMargin")));
			this.grpBoxZOrder.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("grpBoxZOrder.AutoScrollMinSize")));
			this.grpBoxZOrder.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("grpBoxZOrder.BackgroundImage")));
			this.grpBoxZOrder.Controls.AddRange(new System.Windows.Forms.Control[] {
																					   this.rgrpZOrder});
			this.grpBoxZOrder.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("grpBoxZOrder.Dock")));
			this.grpBoxZOrder.Enabled = ((bool)(resources.GetObject("grpBoxZOrder.Enabled")));
			this.grpBoxZOrder.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("grpBoxZOrder.ImeMode")));
			this.grpBoxZOrder.Location = ((System.Drawing.Point)(resources.GetObject("grpBoxZOrder.Location")));
			this.grpBoxZOrder.Name = "grpBoxZOrder";
			this.grpBoxZOrder.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("grpBoxZOrder.RightToLeft")));
			this.grpBoxZOrder.Size = ((System.Drawing.Size)(resources.GetObject("grpBoxZOrder.Size")));
			this.grpBoxZOrder.TabIndex = ((int)(resources.GetObject("grpBoxZOrder.TabIndex")));
			this.grpBoxZOrder.Text = resources.GetString("grpBoxZOrder.Text");
			this.grpBoxZOrder.Visible = ((bool)(resources.GetObject("grpBoxZOrder.Visible")));
			
			
			
			this.panelControl1.AccessibleDescription = ((string)(resources.GetObject("panelControl1.AccessibleDescription")));
			this.panelControl1.AccessibleName = ((string)(resources.GetObject("panelControl1.AccessibleName")));
			this.panelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("panelControl1.Anchor")));
			this.panelControl1.AutoScroll = ((bool)(resources.GetObject("panelControl1.AutoScroll")));
			this.panelControl1.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("panelControl1.AutoScrollMargin")));
			this.panelControl1.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("panelControl1.AutoScrollMinSize")));
			this.panelControl1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelControl1.BackgroundImage")));
			this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
			this.panelControl1.Controls.AddRange(new System.Windows.Forms.Control[] {
																						this.pc});
			this.panelControl1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("panelControl1.Dock")));
			this.panelControl1.Enabled = ((bool)(resources.GetObject("panelControl1.Enabled")));
			this.panelControl1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("panelControl1.ImeMode")));
			this.panelControl1.Location = ((System.Drawing.Point)(resources.GetObject("panelControl1.Location")));
			this.panelControl1.Name = "panelControl1";
			this.panelControl1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("panelControl1.RightToLeft")));
			this.panelControl1.Size = ((System.Drawing.Size)(resources.GetObject("panelControl1.Size")));
			this.panelControl1.TabIndex = ((int)(resources.GetObject("panelControl1.TabIndex")));
			this.panelControl1.Text = resources.GetString("panelControl1.Text");
			this.panelControl1.Visible = ((bool)(resources.GetObject("panelControl1.Visible")));
			
			
			
			this.AccessibleDescription = ((string)(resources.GetObject("$this.AccessibleDescription")));
			this.AccessibleName = ((string)(resources.GetObject("$this.AccessibleName")));
			this.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("$this.Anchor")));
			this.AutoScaleBaseSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScaleBaseSize")));
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.CancelButton = this.btnCancel;
			this.ClientSize = ((System.Drawing.Size)(resources.GetObject("$this.ClientSize")));
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.panelControl1,
																		  this.grpBoxZOrder,
																		  this.grpBoxPageRange,
																		  this.xtraTabControl,
																		  this.btnCancel,
																		  this.btnOK,
																		  this.btnClear});
			this.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("$this.Dock")));
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximizeBox = false;
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimizeBox = false;
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "WatermarkEditorForm";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.ShowInTaskbar = false;
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.Visible = ((bool)(resources.GetObject("$this.Visible")));
			((System.ComponentModel.ISupportInitialize)(this.tePageRange.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.rgrpPageRange.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lkpImageView.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lkpImageHAlign.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.chbTiling.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.chbItalic.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.chbBold.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ceWatermarkColor.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cmbWatermarkFontSize.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cmbWatermarkFont.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cmbWatermarkText.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lkpTextDirection.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.teTransparentValue.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lkpImageVAlign.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.rgrpZOrder.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).EndInit();
			this.xtraTabControl.ResumeLayout(false);
			this.tpTextWaterMark.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grpBoxTransparent)).EndInit();
			this.grpBoxTransparent.ResumeLayout(false);
			this.tpPictureWatermark.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grpBoxAlign)).EndInit();
			this.grpBoxAlign.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grpBoxImageTransparent)).EndInit();
			this.grpBoxImageTransparent.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.teImageTransparentValue.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.grpBoxPageRange)).EndInit();
			this.grpBoxPageRange.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grpBoxZOrder)).EndInit();
			this.grpBoxZOrder.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
			this.panelControl1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		protected override void OnFontChanged(EventArgs e) 
		{
			base.OnFontChanged(e);
			if( !Font.Equals(DefaultFont) )
				Font = DefaultFont;
		}
		private void LocalizeRadioGroups() 
		{
			rgrpPageRange.Properties.Items[0].Description = PreviewLocalizer.GetString(PreviewStringId.WMForm_PageRangeRgrItem_All);
			rgrpPageRange.Properties.Items[1].Description = PreviewLocalizer.GetString(PreviewStringId.WMForm_PageRangeRgrItem_Pages);
			rgrpZOrder.Properties.Items[0].Description = PreviewLocalizer.GetString(PreviewStringId.WMForm_ZOrderRgrItem_InFront);
			rgrpZOrder.Properties.Items[1].Description = PreviewLocalizer.GetString(PreviewStringId.WMForm_ZOrderRgrItem_Behind);
		}
		private void InitComboBoxes() 
		{
			int n = 0;  
			dsDirectionMode = new DirectionModeItem[4]; 
			dsDirectionMode[n++] = new DirectionModeItem(DirectionMode.Horizontal, PreviewLocalizer.GetString(PreviewStringId.WMForm_Direction_Horizontal));
			dsDirectionMode[n++] = new DirectionModeItem(DirectionMode.Vertical, PreviewLocalizer.GetString(PreviewStringId.WMForm_Direction_Vertical));
			dsDirectionMode[n++] = new DirectionModeItem(DirectionMode.BackwardDiagonal, PreviewLocalizer.GetString(PreviewStringId.WMForm_Direction_BackwardDiagonal));
			dsDirectionMode[n++] = new DirectionModeItem(DirectionMode.ForwardDiagonal, PreviewLocalizer.GetString(PreviewStringId.WMForm_Direction_ForwardDiagonal));
			lkpTextDirection.Properties.DataSource = dsDirectionMode;
			lkpTextDirection.Properties.DropDownRows = dsDirectionMode.Length;
			lkpTextDirection.Properties.DisplayMember = "Text";
			lkpTextDirection.Properties.ValueMember = "DirectionMode";

			n = 0;
			dsImageViewMode = new ViewModeItem[3];
			dsImageViewMode[n++] = new ViewModeItem(ImageViewMode.Clip, PreviewLocalizer.GetString(PreviewStringId.WMForm_ImageClip));
			dsImageViewMode[n++] = new ViewModeItem(ImageViewMode.Stretch, PreviewLocalizer.GetString(PreviewStringId.WMForm_ImageStretch));
			dsImageViewMode[n++] = new ViewModeItem(ImageViewMode.Zoom, PreviewLocalizer.GetString(PreviewStringId.WMForm_ImageZoom));
			lkpImageView.Properties.DataSource = dsImageViewMode;
			lkpImageView.Properties.DropDownRows = dsImageViewMode.Length;
			lkpImageView.Properties.DisplayMember = "Text";
			lkpImageView.Properties.ValueMember = "ViewMode";

			n = 0;
			dsImageHAlign = new ImageAlignItem[3];
			dsImageHAlign[n++] = new ImageAlignItem(alignLeft, PreviewLocalizer.GetString(PreviewStringId.WMForm_HorzAlign_Left));
			dsImageHAlign[n++] = new ImageAlignItem(alignCenter, PreviewLocalizer.GetString(PreviewStringId.WMForm_HorzAlign_Center));
			dsImageHAlign[n++] = new ImageAlignItem(alignRight, PreviewLocalizer.GetString(PreviewStringId.WMForm_HorzAlign_Right));
			lkpImageHAlign.Properties.DataSource = dsImageHAlign;
			lkpImageHAlign.Properties.DropDownRows = dsImageHAlign.Length;
			lkpImageHAlign.Properties.DisplayMember = "Text";
			lkpImageHAlign.Properties.ValueMember = "Alignment";

			n = 0;
			dsImageVAlign = new ImageAlignItem[3];
			dsImageVAlign[n++] = new ImageAlignItem(alignTop, PreviewLocalizer.GetString(PreviewStringId.WMForm_VertAlign_Top));
			dsImageVAlign[n++] = new ImageAlignItem(alignMiddle, PreviewLocalizer.GetString(PreviewStringId.WMForm_VertAlign_Middle));
			dsImageVAlign[n++] = new ImageAlignItem(alignBottom, PreviewLocalizer.GetString(PreviewStringId.WMForm_VertAlign_Bottom));
			lkpImageVAlign.Properties.DataSource = dsImageVAlign;
			lkpImageVAlign.Properties.DropDownRows = dsImageVAlign.Length;
			lkpImageVAlign.Properties.DisplayMember = "Text";
			lkpImageVAlign.Properties.ValueMember = "Alignment";

			cmbWatermarkText.EditValue = String.Empty;
			cmbWatermarkText.Properties.Items.AddRange(DevExpress.XtraPrinting.Design.WMTextConverter.StandardValues);

			byte[] fontSizes = { 36, 40, 44, 48, 54, 60, 66, 72, 80, 90, 96, 105, 120, 144 };
			foreach(byte size in fontSizes)
				cmbWatermarkFontSize.Properties.Items.Add(size.ToString().Trim());
			cmbWatermarkFontSize.SelectedIndex = 0;

			foreach(FontFamily family in FontFamily.Families)
				cmbWatermarkFont.Properties.Items.Add(family.Name);
			cmbWatermarkFont.SelectedIndex = 0;
		}
		private Image LoadImage(string title) 
		{
			OpenFileDialog openDialog = new OpenFileDialog();
			openDialog.Title = title;
			openDialog.Filter = "All Picture Files|*.bmp;*.gif;*.jpg;*.png;*.tiff;*.emf;*.wmf";
			DialogResult res = openDialog.ShowDialog();
			return res == DialogResult.OK && openDialog.FileName.Length > 0 ?
                Webb.Utility.ReadImageFromPath(openDialog.FileName) : null;
		}
		private Image ColneImage(Image image)
		{
			return  image==null?null:(Image)(image.Clone());
		}
		private void btnSelectPicture_Click(object sender, System.EventArgs e) 
		{
			Image image = LoadImage(PreviewLocalizer.GetString(PreviewStringId.WMForm_PictureDlg_Title));
			if(image == null)
				return;
			SetControlEnabled(new Control[] {lkpImageView, lkpImageHAlign, lkpImageVAlign, chbTiling}, image != null);
			watermark.Image = image==null?null:(Image)(image.Clone());
			UpdateWatermarkView();
		}
		private void UpdateWatermarkView() 
		{
			pc.Update(watermark);
		}
		public void Assign(Watermark watermark) 
		{
			this.watermark.CopyFrom(watermark);
			InitControls();
		}
		private void InitControls() 
		{
			canSync = false;
			try 
			{
				InitTextTab();
				InitPictureTab();
				rgrpZOrder.EditValue = watermark.ShowBehind;
			} 
			finally 
			{
				canSync = true;
			}
			SyncWatermark();
		}
		private void InitTextTab() 
		{
			this.tePageRange.EditValue = watermark.PageRange;
			cmbWatermarkText.EditValue = watermark.Text;
			ceWatermarkColor.Color = watermark.ForeColor;
			Font f = watermark.Font;
			if(f != null) 
			{
				cmbWatermarkFontSize.EditValue = ((int)f.Size).ToString().Trim();
				cmbWatermarkFont.EditValue = f.Name;
				chbBold.Checked = f.Bold;
				chbItalic.Checked = f.Italic;

			}
			UpdateTransparentValue(watermark.TextTransparency, scrBarTransparent, teTransparentValue);
			cmbWatermarkText.EditValue = watermark.Text; 

			lkpTextDirection.EditValue = watermark.TextDirection;
		}
		private void InitPictureTab() 
		{
			lkpImageView.EditValue = watermark.ImageViewMode;

			int index = Array.IndexOf(contentAlignList,watermark.ImageAlign);
			string[] items = alignList[index].Split(',');

			lkpImageHAlign.EditValue = items[0];
			lkpImageVAlign.EditValue = items[1];

			chbTiling.Checked = watermark.ImageTiling;
			SetControlEnabled(new Control[] {lkpImageView, lkpImageHAlign, lkpImageVAlign, chbTiling}, watermark.Image != null);
			UpdateTransparentValue(watermark.ImageTransparency, scrBarImageTransparent, teImageTransparentValue);
		}
		private void SetControlEnabled(Control[] controls, bool val) 
		{
			foreach(Control control in controls)
				control.Enabled = val;
		}
		private void OnEditValueChanged(object sender, System.EventArgs e) 
		{
			SyncWatermark();
		}
		void SyncWatermark() 
		{
			if(!canSync)
				return;
			watermark.Text = (string)cmbWatermarkText.EditValue;
			watermark.ShowBehind = (bool)rgrpZOrder.EditValue;
			watermark.PageRange = (bool)rgrpPageRange.EditValue ? (string)tePageRange.EditValue : String.Empty;
			watermark.ImageTiling = chbTiling.Checked;			
			watermark.TextTransparency  = scrBarTransparent.Value;
			watermark.ImageTransparency  = scrBarImageTransparent.Value;
			watermark.ForeColor = ceWatermarkColor.Color;
			watermark.ImageViewMode = GetImageViewMode();
			watermark.TextDirection = GetWatermarkDirection();
			watermark.ImageAlign = GetImageAlignment();

			try 
			{
				try 
				{
					watermark.Font = new Font((string)cmbWatermarkFont.EditValue, GetFontSize(), GetFontStyle());
				} 
				catch {}
				UpdateWatermarkView();
			} 
			catch {}
		}
		private DirectionMode GetWatermarkDirection() 
		{
			return (DirectionMode)lkpTextDirection.EditValue;
		}
		private ImageViewMode GetImageViewMode() 
		{
			return (ImageViewMode)lkpImageView.EditValue;
		}
		private ContentAlignment GetImageAlignment() 
		{
			string vertAling = (string)lkpImageVAlign.EditValue;
			string horzAling = (string)lkpImageHAlign.EditValue;
			int index = Array.IndexOf(alignList, ToString(vertAling, horzAling));
			return (index != -1 && contentAlignList.Length > index) ? contentAlignList[index] :
				ContentAlignment.MiddleCenter;
		}
		FontStyle GetFontStyle() 
		{
			FontStyle style = FontStyle.Regular;
			if(chbBold.Checked) 
				style |= FontStyle.Bold;
			if(chbItalic.Checked) 
				style |= FontStyle.Italic;
			return style; 
		}
		float GetFontSize() 
		{
			return Convert.ToSingle(cmbWatermarkFontSize.EditValue);
		}
		private void teImageTransparentValue_EditValueChanged(object sender, System.EventArgs e) 
		{
			UpdateTransparentValue(ToInt32(((TextEdit)sender).EditValue), scrBarImageTransparent, teImageTransparentValue);
			OnEditValueChanged(null, EventArgs.Empty);
		}
		private void OnScrlBarImageTransparent_Scroll(object sender, System.Windows.Forms.ScrollEventArgs e) 
		{
			UpdateTransparentValue(e.NewValue, scrBarImageTransparent, teImageTransparentValue);
			OnEditValueChanged(null, EventArgs.Empty);
		}
		private void teTransparentValue_EditValueChanged(object sender, System.EventArgs e) 
		{
			UpdateTransparentValue(ToInt32(ToInt32(((TextEdit)sender).EditValue)), scrBarTransparent, teTransparentValue);
			OnEditValueChanged(null, EventArgs.Empty);
		}
		private void OnScrlBarTransparent_Scroll(object sender, System.Windows.Forms.ScrollEventArgs e) 
		{
			UpdateTransparentValue(e.NewValue, scrBarTransparent, teTransparentValue);
			OnEditValueChanged(null, EventArgs.Empty);
		}
		int ToInt32(object obj) 
		{
			try 
			{
				return Convert.ToInt32(obj);
			} 
			catch {}
			return 0;
		}
		private void UpdateTransparentValue(int val, DevExpress.XtraEditors.HScrollBar scrBar, TextEdit textEdit) 
		{
			val = Math.Max(0, Math.Min(val, 255));
			scrBar.Value = val;
			textEdit.EditValue = Convert.ToString(val, 10).Trim();
		}
		private void btnClear_Click(object sender, System.EventArgs e) 
		{
			watermark = new Watermark();
			InitControls();
			UpdateWatermarkView();
		}
		private void rgrpPageRange_EditValueChanged(object sender, System.EventArgs e) 
		{
			if ((bool)rgrpPageRange.EditValue)
				tePageRange.Focus();
			else
				tePageRange.EditValue = "";
		}
		private void tePageRange_EditValueChanged(object sender, System.EventArgs e) 
		{
			rgrpPageRange.EditValue = !tePageRange.EditValue.Equals("");
			SyncWatermark();
		}
		private void btnOK_Click(object sender, System.EventArgs e) 
		{
			try 
			{
				if(GetFontSize() <= 0)
					MessageBox.Show("Error Font Size!");
				DialogResult = DialogResult.OK;
			} 
			catch (Exception exc) 
			{
				MessageBox.Show(exc.Message);
				cmbWatermarkFontSize.Focus();
			}
		}
	}
}

