using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;
using System.Data;
using System.Reflection;

namespace Webb.Reports.ExControls.UI
{
	/// <summary>
	/// Form1 的摘要说明。
	/// </summary>
	public class PaintForm : System.Windows.Forms.Form
	{
		private Point ptStart = Point.Empty, ptEnd = Point.Empty;
		private bool isMouseDown = false, isMouseUp = false;
		private Color _ForeColor = Color.Black;
		private Color _BackColor = Color.White;
		private string shape;
		private Bitmap _Bitmap;
		private Graphics _Graphics;
		private ArrayList _Shapes;

		private Cursor Cursor_Line;
		private Cursor Cursor_Ellipse;
		private Cursor Cursor_Rectangle;
		private Cursor Cursor_Eraser;
		private SaveFileDialog _SaveFileDialog;

		private System.Windows.Forms.Button C_BtnCurve;
		private System.Windows.Forms.Button C_BtnRectangle;
		private System.Windows.Forms.Button C_BtnEllipse;
		private System.Windows.Forms.Button C_BtnLine;
		private System.Windows.Forms.Button C_BtnEraser;
		private System.Windows.Forms.Button C_BtnForeColor;
		private System.Windows.Forms.Button C_BtnBackColor;

		private System.Windows.Forms.ComboBox C_CBWidth;
		private System.Windows.Forms.Splitter C_Splitter;
		private System.Windows.Forms.Panel C_PanelControls;
		private System.Windows.Forms.Label C_LabelWidth;
		private System.Windows.Forms.Button C_BtnFillRectangle;
		private System.Windows.Forms.Button C_BtnFillEllipse;
		private System.Windows.Forms.Panel C_PanelPaint;
		private System.Windows.Forms.PictureBox PictureBox;
		private System.Windows.Forms.MainMenu C_MainMenu;
		private System.Windows.Forms.MenuItem C_MItem_File;
		private System.Windows.Forms.MenuItem C_MItem_Save;
		private System.Windows.Forms.MenuItem C_MItem_Clear;
		private System.Windows.Forms.Button C_BtnDashLine;
		private System.Windows.Forms.Button C_BtnOK;
		private System.Windows.Forms.Button C_BtnCancel;
		private System.ComponentModel.IContainer components;

		public struct ShapeType
		{
			public string type;
			public Point ptStart, ptEnd;
			public Color foreColor, backColor;
			public Brush brush;
			public int width;
			public ShapeType(string type, Point ptStart, Point ptEnd, Color foreColor, Color backColor, Brush brush,int nWidth )
			{
				this.type = type;
				this.ptStart = ptStart;
				this.ptEnd = ptEnd;
				this.foreColor = foreColor;
				this.backColor = backColor;
				this.brush = brush;
				this.width = nWidth;
			}
		}

		public PaintForm()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
			this.CustomInit();
		}

		private void CustomInit()
		{
			this.C_CBWidth.SelectedIndex = 2;
			
			this._Shapes = new ArrayList();
			
			this._Bitmap = new Bitmap(this.PictureBox.Width,this.PictureBox.Height,this.PictureBox.CreateGraphics());
			
			this.PictureBox.Image = this._Bitmap;
			
			this._Graphics = Graphics.FromImage(this._Bitmap);
			
			this._Graphics.FillRectangle(new SolidBrush(Color.White),this.PictureBox.ClientRectangle);
			
			this.Cursor_Line = new Cursor(typeof(Webb.Reports.ExControls.ResourceManager),"Cursors.Draw_Line.cur");
			this.Cursor_Ellipse = new Cursor(typeof(Webb.Reports.ExControls.ResourceManager),"Cursors.Draw_Circle.cur");
			this.Cursor_Rectangle = new Cursor(typeof(Webb.Reports.ExControls.ResourceManager),"Cursors.Draw_Rectangle.cur");
			this.Cursor_Eraser = new Cursor(typeof(Webb.Reports.ExControls.ResourceManager),"Cursors.Draw_Eraser.cur");

			this._SaveFileDialog = new SaveFileDialog();
			this._SaveFileDialog.DefaultExt = "bmp";
			this._SaveFileDialog.Filter = "Bitmap (*.bmp)|*.bmp";

			this.SetStyle(ControlStyles.AllPaintingInWmPaint,true);   
			this.SetStyle(ControlStyles.DoubleBuffer,true);   
			this.SetStyle(ControlStyles.UserPaint,true);
		}

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(PaintForm));
			this.C_BtnRectangle = new System.Windows.Forms.Button();
			this.C_BtnEllipse = new System.Windows.Forms.Button();
			this.C_BtnLine = new System.Windows.Forms.Button();
			this.C_BtnForeColor = new System.Windows.Forms.Button();
			this.C_BtnCurve = new System.Windows.Forms.Button();
			this.C_BtnEraser = new System.Windows.Forms.Button();
			this.C_BtnBackColor = new System.Windows.Forms.Button();
			this.C_CBWidth = new System.Windows.Forms.ComboBox();
			this.C_Splitter = new System.Windows.Forms.Splitter();
			this.C_PanelControls = new System.Windows.Forms.Panel();
			this.C_BtnCancel = new System.Windows.Forms.Button();
			this.C_BtnOK = new System.Windows.Forms.Button();
			this.C_BtnDashLine = new System.Windows.Forms.Button();
			this.C_BtnFillEllipse = new System.Windows.Forms.Button();
			this.C_BtnFillRectangle = new System.Windows.Forms.Button();
			this.C_LabelWidth = new System.Windows.Forms.Label();
			this.C_PanelPaint = new System.Windows.Forms.Panel();
			this.PictureBox = new System.Windows.Forms.PictureBox();
			this.C_MainMenu = new System.Windows.Forms.MainMenu();
			this.C_MItem_File = new System.Windows.Forms.MenuItem();
			this.C_MItem_Save = new System.Windows.Forms.MenuItem();
			this.C_MItem_Clear = new System.Windows.Forms.MenuItem();
			this.C_PanelControls.SuspendLayout();
			this.C_PanelPaint.SuspendLayout();
			this.SuspendLayout();
			// 
			// C_BtnRectangle
			// 
			this.C_BtnRectangle.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("C_BtnRectangle.BackgroundImage")));
			this.C_BtnRectangle.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.C_BtnRectangle.Location = new System.Drawing.Point(104, 8);
			this.C_BtnRectangle.Name = "C_BtnRectangle";
			this.C_BtnRectangle.Size = new System.Drawing.Size(30, 30);
			this.C_BtnRectangle.TabIndex = 4;
			this.C_BtnRectangle.Tag = "Rectangle";
			this.C_BtnRectangle.Click += new System.EventHandler(this.C_DrawBtn_Click);
			// 
			// C_BtnEllipse
			// 
			this.C_BtnEllipse.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("C_BtnEllipse.BackgroundImage")));
			this.C_BtnEllipse.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.C_BtnEllipse.Location = new System.Drawing.Point(168, 8);
			this.C_BtnEllipse.Name = "C_BtnEllipse";
			this.C_BtnEllipse.Size = new System.Drawing.Size(30, 30);
			this.C_BtnEllipse.TabIndex = 6;
			this.C_BtnEllipse.Tag = "Ellipse";
			this.C_BtnEllipse.Click += new System.EventHandler(this.C_DrawBtn_Click);
			// 
			// C_BtnLine
			// 
			this.C_BtnLine.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.C_BtnLine.Image = ((System.Drawing.Image)(resources.GetObject("C_BtnLine.Image")));
			this.C_BtnLine.Location = new System.Drawing.Point(8, 8);
			this.C_BtnLine.Name = "C_BtnLine";
			this.C_BtnLine.Size = new System.Drawing.Size(30, 30);
			this.C_BtnLine.TabIndex = 1;
			this.C_BtnLine.Tag = "Line";
			this.C_BtnLine.Click += new System.EventHandler(this.C_DrawBtn_Click);
			// 
			// C_BtnForeColor
			// 
			this.C_BtnForeColor.BackColor = System.Drawing.Color.Black;
			this.C_BtnForeColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.C_BtnForeColor.Location = new System.Drawing.Point(9, 48);
			this.C_BtnForeColor.Name = "C_BtnForeColor";
			this.C_BtnForeColor.Size = new System.Drawing.Size(27, 21);
			this.C_BtnForeColor.TabIndex = 9;
			this.C_BtnForeColor.Click += new System.EventHandler(this.Btn_ForeColor_Click);
			// 
			// C_BtnCurve
			// 
			this.C_BtnCurve.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("C_BtnCurve.BackgroundImage")));
			this.C_BtnCurve.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.C_BtnCurve.Location = new System.Drawing.Point(72, 8);
			this.C_BtnCurve.Name = "C_BtnCurve";
			this.C_BtnCurve.Size = new System.Drawing.Size(30, 30);
			this.C_BtnCurve.TabIndex = 3;
			this.C_BtnCurve.Tag = "Curve";
			this.C_BtnCurve.Click += new System.EventHandler(this.C_DrawBtn_Click);
			// 
			// C_BtnEraser
			// 
			this.C_BtnEraser.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("C_BtnEraser.BackgroundImage")));
			this.C_BtnEraser.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.C_BtnEraser.Location = new System.Drawing.Point(232, 8);
			this.C_BtnEraser.Name = "C_BtnEraser";
			this.C_BtnEraser.Size = new System.Drawing.Size(30, 30);
			this.C_BtnEraser.TabIndex = 8;
			this.C_BtnEraser.Tag = "Eraser";
			this.C_BtnEraser.Click += new System.EventHandler(this.C_DrawBtn_Click);
			// 
			// C_BtnBackColor
			// 
			this.C_BtnBackColor.BackColor = System.Drawing.Color.White;
			this.C_BtnBackColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.C_BtnBackColor.Location = new System.Drawing.Point(17, 56);
			this.C_BtnBackColor.Name = "C_BtnBackColor";
			this.C_BtnBackColor.Size = new System.Drawing.Size(27, 21);
			this.C_BtnBackColor.TabIndex = 10;
			this.C_BtnBackColor.Click += new System.EventHandler(this.C_BtnBackColor_Click);
			// 
			// C_CBWidth
			// 
			this.C_CBWidth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.C_CBWidth.Items.AddRange(new object[] {
														   "1",
														   "2",
														   "3",
														   "4",
														   "5",
														   "6",
														   "7",
														   "8",
														   "9",
														   "10",
														   "11",
														   "12",
														   "13",
														   "14",
														   "15",
														   "16",
														   "17",
														   "18",
														   "19",
														   "20"});
			this.C_CBWidth.Location = new System.Drawing.Point(95, 50);
			this.C_CBWidth.MaxDropDownItems = 5;
			this.C_CBWidth.Name = "C_CBWidth";
			this.C_CBWidth.Size = new System.Drawing.Size(40, 21);
			this.C_CBWidth.TabIndex = 12;
			// 
			// C_Splitter
			// 
			this.C_Splitter.BackColor = System.Drawing.Color.Black;
			this.C_Splitter.Cursor = System.Windows.Forms.Cursors.HSplit;
			this.C_Splitter.Dock = System.Windows.Forms.DockStyle.Top;
			this.C_Splitter.Location = new System.Drawing.Point(0, 93);
			this.C_Splitter.Name = "C_Splitter";
			this.C_Splitter.Size = new System.Drawing.Size(459, 3);
			this.C_Splitter.TabIndex = 12;
			this.C_Splitter.TabStop = false;
			// 
			// C_PanelControls
			// 
			this.C_PanelControls.Controls.Add(this.C_BtnCancel);
			this.C_PanelControls.Controls.Add(this.C_BtnOK);
			this.C_PanelControls.Controls.Add(this.C_BtnDashLine);
			this.C_PanelControls.Controls.Add(this.C_BtnFillEllipse);
			this.C_PanelControls.Controls.Add(this.C_BtnFillRectangle);
			this.C_PanelControls.Controls.Add(this.C_LabelWidth);
			this.C_PanelControls.Controls.Add(this.C_BtnCurve);
			this.C_PanelControls.Controls.Add(this.C_BtnRectangle);
			this.C_PanelControls.Controls.Add(this.C_BtnEllipse);
			this.C_PanelControls.Controls.Add(this.C_BtnLine);
			this.C_PanelControls.Controls.Add(this.C_BtnEraser);
			this.C_PanelControls.Controls.Add(this.C_BtnForeColor);
			this.C_PanelControls.Controls.Add(this.C_CBWidth);
			this.C_PanelControls.Controls.Add(this.C_BtnBackColor);
			this.C_PanelControls.Dock = System.Windows.Forms.DockStyle.Top;
			this.C_PanelControls.Location = new System.Drawing.Point(0, 0);
			this.C_PanelControls.Name = "C_PanelControls";
			this.C_PanelControls.Size = new System.Drawing.Size(459, 93);
			this.C_PanelControls.TabIndex = 13;
			// 
			// C_BtnCancel
			// 
			this.C_BtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.C_BtnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.C_BtnCancel.Location = new System.Drawing.Point(368, 43);
			this.C_BtnCancel.Name = "C_BtnCancel";
			this.C_BtnCancel.TabIndex = 14;
			this.C_BtnCancel.Text = "Cancel";
			this.C_BtnCancel.Click += new System.EventHandler(this.C_BtnCancel_Click);
			// 
			// C_BtnOK
			// 
			this.C_BtnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.C_BtnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.C_BtnOK.Location = new System.Drawing.Point(368, 11);
			this.C_BtnOK.Name = "C_BtnOK";
			this.C_BtnOK.TabIndex = 13;
			this.C_BtnOK.Text = "OK";
			this.C_BtnOK.Click += new System.EventHandler(this.C_BtnOK_Click);
			// 
			// C_BtnDashLine
			// 
			this.C_BtnDashLine.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.C_BtnDashLine.Image = ((System.Drawing.Image)(resources.GetObject("C_BtnDashLine.Image")));
			this.C_BtnDashLine.Location = new System.Drawing.Point(40, 8);
			this.C_BtnDashLine.Name = "C_BtnDashLine";
			this.C_BtnDashLine.Size = new System.Drawing.Size(30, 30);
			this.C_BtnDashLine.TabIndex = 2;
			this.C_BtnDashLine.Tag = "DashLine";
			this.C_BtnDashLine.Click += new System.EventHandler(this.C_DrawBtn_Click);
			// 
			// C_BtnFillEllipse
			// 
			this.C_BtnFillEllipse.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("C_BtnFillEllipse.BackgroundImage")));
			this.C_BtnFillEllipse.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.C_BtnFillEllipse.Location = new System.Drawing.Point(200, 8);
			this.C_BtnFillEllipse.Name = "C_BtnFillEllipse";
			this.C_BtnFillEllipse.Size = new System.Drawing.Size(30, 30);
			this.C_BtnFillEllipse.TabIndex = 7;
			this.C_BtnFillEllipse.Tag = "FillEllipse";
			this.C_BtnFillEllipse.Click += new System.EventHandler(this.C_DrawBtn_Click);
			// 
			// C_BtnFillRectangle
			// 
			this.C_BtnFillRectangle.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("C_BtnFillRectangle.BackgroundImage")));
			this.C_BtnFillRectangle.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.C_BtnFillRectangle.Location = new System.Drawing.Point(136, 8);
			this.C_BtnFillRectangle.Name = "C_BtnFillRectangle";
			this.C_BtnFillRectangle.Size = new System.Drawing.Size(30, 30);
			this.C_BtnFillRectangle.TabIndex = 5;
			this.C_BtnFillRectangle.Tag = "FillRectangle";
			this.C_BtnFillRectangle.Click += new System.EventHandler(this.C_DrawBtn_Click);
			// 
			// C_LabelWidth
			// 
			this.C_LabelWidth.Location = new System.Drawing.Point(57, 53);
			this.C_LabelWidth.Name = "C_LabelWidth";
			this.C_LabelWidth.Size = new System.Drawing.Size(34, 12);
			this.C_LabelWidth.TabIndex = 11;
			this.C_LabelWidth.Text = "Width";
			// 
			// C_PanelPaint
			// 
			this.C_PanelPaint.AutoScroll = true;
			this.C_PanelPaint.Controls.Add(this.PictureBox);
			this.C_PanelPaint.Dock = System.Windows.Forms.DockStyle.Fill;
			this.C_PanelPaint.Location = new System.Drawing.Point(0, 96);
			this.C_PanelPaint.Name = "C_PanelPaint";
			this.C_PanelPaint.Size = new System.Drawing.Size(459, 272);
			this.C_PanelPaint.TabIndex = 14;
			// 
			// PictureBox
			// 
			this.PictureBox.BackColor = System.Drawing.Color.White;
			this.PictureBox.Cursor = System.Windows.Forms.Cursors.Default;
			this.PictureBox.Location = new System.Drawing.Point(0, 0);
			this.PictureBox.Name = "PictureBox";
			this.PictureBox.Size = new System.Drawing.Size(500, 300);
			this.PictureBox.TabIndex = 15;
			this.PictureBox.TabStop = false;
			this.PictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.PictureBox_Paint);
			this.PictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseUp);
			this.PictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseMove);
			this.PictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseDown);
			// 
			// C_MainMenu
			// 
			this.C_MainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					   this.C_MItem_File});
			// 
			// C_MItem_File
			// 
			this.C_MItem_File.Index = 0;
			this.C_MItem_File.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.C_MItem_Save,
																						 this.C_MItem_Clear});
			this.C_MItem_File.Text = "File";
			// 
			// C_MItem_Save
			// 
			this.C_MItem_Save.Index = 0;
			this.C_MItem_Save.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
			this.C_MItem_Save.Text = "&Save";
			this.C_MItem_Save.Visible = false;
			this.C_MItem_Save.Click += new System.EventHandler(this.Btn_Save_Click);
			// 
			// C_MItem_Clear
			// 
			this.C_MItem_Clear.Index = 1;
			this.C_MItem_Clear.Shortcut = System.Windows.Forms.Shortcut.CtrlR;
			this.C_MItem_Clear.Text = "Clea&r";
			this.C_MItem_Clear.Click += new System.EventHandler(this.Btn_Clear_Click);
			// 
			// PaintForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(459, 368);
			this.ControlBox = false;
			this.Controls.Add(this.C_PanelPaint);
			this.Controls.Add(this.C_Splitter);
			this.Controls.Add(this.C_PanelControls);
			this.Menu = this.C_MainMenu;
			this.Name = "PaintForm";
			this.Text = "Paint";
			this.C_PanelControls.ResumeLayout(false);
			this.C_PanelPaint.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void PictureBox_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if( ! isMouseUp )
			{
				this.isMouseDown = true;
				this.ptStart = new Point( e.X, e.Y );
			}

			switch(this.shape)
			{
				case "DrawLine":
				case "DrawCurve":
					this.PictureBox.Cursor = this.Cursor_Line;
					break;
				case "DrawEraser":
					break;
				default:
					this.PictureBox.Cursor = Cursors.Default;
					break;
			}
		}

		public Image CustomImage
		{
			get{return this.PictureBox.Image;}
		}

		public int LineWidth
		{
			get{return Int32.Parse(this.C_CBWidth.Text);}
		}

		private Rectangle GetEraserRect(Point ptTopLeft)
		{
			int nRadius = 20;

			ptTopLeft.Offset(-1*nRadius/4 - 2,-1*nRadius/4);	//tag

			return new Rectangle(ptTopLeft,new Size(nRadius,nRadius));
		}

		private void PictureBox_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			this.isMouseDown = false;
			
			ptEnd = new Point( e.X, e.Y );

			Graphics g = this.PictureBox.CreateGraphics();
			
			Rectangle rect = this.CalculateRect(ptStart,ptEnd);
			
			Brush brush = new SolidBrush(this._BackColor);

			Pen pen = new Pen(this._ForeColor,this.LineWidth); 

			if(this.shape=="DrawFillEllipse")
			{
				this._Graphics.FillEllipse( brush, rect );
				this._Graphics.DrawEllipse( pen, rect );
				_Shapes.Add( new ShapeType( this.shape, ptStart, ptEnd, this._ForeColor, this._BackColor, Brushes.Black ,this.LineWidth) );
			}
			if(this.shape=="DrawEllipse")
			{
				this._Graphics.DrawEllipse( pen,rect );
				_Shapes.Add( new ShapeType( this.shape, ptStart, ptEnd, this._ForeColor, this._BackColor, Brushes.Black ,this.LineWidth) );
			}
			if(this.shape=="DrawFillRectangle")
			{
				this._Graphics.FillRectangle( brush,rect );
				this._Graphics.DrawRectangle(pen, rect );
				_Shapes.Add( new ShapeType(this.shape, ptStart, ptEnd, this._ForeColor, this._BackColor, Brushes.Black ,this.LineWidth ) );
			}
			if(this.shape=="DrawRectangle")
			{
				this._Graphics.DrawRectangle(pen, rect );
				_Shapes.Add( new ShapeType(this.shape, ptStart, ptEnd, this._ForeColor, this._BackColor, Brushes.Black ,this.LineWidth ) );
			}
			if(this.shape=="DrawLine")
			{
				this._Graphics.DrawLine(pen,this.ptStart,this.ptEnd);
				_Shapes.Add( new ShapeType(this.shape, ptStart, ptEnd, this._ForeColor, this._BackColor, Brushes.Black,this.LineWidth ) );
			}
			if(this.shape=="DrawCurve")
			{
				this._Graphics.DrawLine(pen,this.ptStart,this.ptEnd);
				_Shapes.Add( new ShapeType(this.shape, ptStart, ptEnd, this._ForeColor, this._BackColor, Brushes.Black ,this.LineWidth) );
			}
			if(this.shape=="DrawEraser")
			{
				this._Graphics.FillEllipse(Brushes.White,GetEraserRect(ptStart));
				_Shapes.Add( new ShapeType(this.shape, ptStart, ptEnd, Color.White, Color.White, Brushes.White , this.LineWidth ) );
			}
			if(this.shape=="DrawDashLine")
			{
				pen.DashStyle = DashStyle.Dash;
				this._Graphics.DrawLine(pen,this.ptStart,this.ptEnd);
				_Shapes.Add( new ShapeType(this.shape, ptStart, ptEnd, this._ForeColor, this._BackColor, Brushes.Black,this.LineWidth ) );
				pen.DashStyle = DashStyle.Solid;
			}

			ptStart = Point.Empty;
			ptEnd = Point.Empty;
			g.Dispose();

			this.PictureBox.Invalidate();

			this.SetCursor();
		}

		private void PictureBox_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if( e.Button != System.Windows.Forms.MouseButtons.Left ) return;

			this.PictureBox.Invalidate();
		}

		private Rectangle CalculateRect(Point pStart,Point pEnd)
		{
			if(pStart == Point.Empty || pEnd == Point.Empty) return Rectangle.Empty;

			int x = Math.Min(pStart.X,pEnd.X);

			int y = Math.Min(pStart.Y,pEnd.Y);

			int width = Math.Abs(pStart.X - pEnd.X);

			int height = Math.Abs(pStart.Y - pEnd.Y);

			return new Rectangle(x,y,width,height);
		}

		private void C_DrawBtn_Click(object sender, System.EventArgs e)
		{
			shape = string.Format("{0}{1}","Draw",(sender as Button).Tag.ToString());

			this.SetCursor();
		}

		//set cursor
		private void SetCursor()
		{
			switch(this.shape)
			{
				case "DrawEllipse":
				case "DrawFillEllipse":
					this.PictureBox.Cursor = this.Cursor_Ellipse;
					break;
				case "DrawRectangle":
				case "DrawFillRectangle":
					this.PictureBox.Cursor = this.Cursor_Rectangle;
					break;
				case "DrawLine":
				case "DrawCurve":
					this.PictureBox.Cursor = this.Cursor_Line;
					break;
				case "DrawEraser":
					this.PictureBox.Cursor = this.Cursor_Eraser;
					break;
				default:
					this.PictureBox.Cursor = Cursors.Default;
					break;
			}
		}

		//select fore color
		private void Btn_ForeColor_Click(object sender, System.EventArgs e)
		{
			ColorDialog _ForeColorDialog = new ColorDialog();	
			
			_ForeColorDialog.Color = this._ForeColor ;
			
			if ( _ForeColorDialog.ShowDialog ( ) != DialogResult.Cancel )
			{
				this._ForeColor = _ForeColorDialog.Color ;
			} 
			else
			{
				this._ForeColor=Color.Black;
			}

			(sender as Button).BackColor = this._ForeColor;
		}

		//select fill color
		private void C_BtnBackColor_Click(object sender, System.EventArgs e)
		{
			ColorDialog _BackColorDialog = new ColorDialog();	
			
			_BackColorDialog.Color = this._BackColor ;
			
			if ( _BackColorDialog.ShowDialog ( ) != DialogResult.Cancel )
			{
				this._BackColor = _BackColorDialog.Color ;
			} 
			else
			{
				this._BackColor = Color.White;
			}

			(sender as Button).BackColor = this._BackColor;
		}

		private void Btn_Clear_Click(object sender, System.EventArgs e)
		{
			this._Shapes.Clear();
			
			this._Graphics.FillRectangle(new SolidBrush(Color.White),this.PictureBox.ClientRectangle);
			
			this.PictureBox.Refresh();
		}

		private void Btn_Save_Click(object sender, System.EventArgs e)
		{
			if(this.PictureBox.Image == null)
			{
				return;
			}

			if(this._SaveFileDialog.ShowDialog(this) == DialogResult.OK)
			{
				this.PictureBox.Image.Save(this._SaveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
			}
		}

		private void PictureBox_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			Rectangle rect = Rectangle.Empty;

			//eraser temp shapes
			#region Eraser_Temp
			if( isMouseDown && ptEnd != Point.Empty )
			{
				rect = this.CalculateRect(ptStart,ptEnd);

				if(this.shape=="DrawEllipse" || this.shape=="DrawFillEllipse")
				{
					e.Graphics.DrawEllipse( Pens.White, rect );
				}
				if(this.shape=="DrawRectangle" || this.shape=="DrawFillRectangle")
				{
					e.Graphics.DrawRectangle( Pens.White, rect );
				}
				if(this.shape=="DrawLine" || this.shape=="DrawDashLine")
				{
					e.Graphics.DrawLine(Pens.White,this.ptStart,this.ptEnd);
				}
			}
			#endregion

			//draw all shapes
			foreach( ShapeType type in _Shapes )
			{
				Pen pen = new Pen(type.foreColor,type.width);

				Brush brush = new SolidBrush(type.backColor);

				rect = this.CalculateRect(type.ptStart,type.ptEnd);

				if(type.type=="DrawFillEllipse")
				{
					//e.Graphics.FillEllipse( brush,rect );
					//e.Graphics.DrawEllipse( pen, rect );
					this._Graphics.FillEllipse( brush,rect );
					this._Graphics.DrawEllipse( pen, rect );
				}
				if(type.type=="DrawEllipse")
				{
					//e.Graphics.DrawEllipse( pen, rect );
					this._Graphics.DrawEllipse( pen, rect );
				}
				if(type.type=="DrawFillRectangle")
				{
					//e.Graphics.FillRectangle( brush,rect );
					//e.Graphics.DrawRectangle(pen, rect );
					this._Graphics.FillRectangle( brush,rect );
					this._Graphics.DrawRectangle(pen, rect );
				}
				if(type.type=="DrawRectangle")
				{
					//e.Graphics.DrawRectangle(pen, rect );
					this._Graphics.DrawRectangle(pen, rect );
				}
				if(type.type=="DrawLine")
				{
					//e.Graphics.DrawLine(pen,type.ptStart,type.ptEnd);
					this._Graphics.DrawLine(pen,type.ptStart,type.ptEnd);
				}
				if(type.type=="DrawDashLine")
				{
					pen.DashStyle = DashStyle.Dash;
					//e.Graphics.DrawLine(pen,type.ptStart,type.ptEnd);
					this._Graphics.DrawLine(pen,type.ptStart,type.ptEnd);
					pen.DashStyle = DashStyle.Solid;
				}
				if(type.type=="DrawCurve")
				{
					//e.Graphics.DrawLine(pen,type.ptStart,type.ptEnd);
					this._Graphics.DrawLine(pen,type.ptStart,type.ptEnd);
				}
				if(type.type=="DrawEraser")
				{
					//e.Graphics.FillEllipse(brush,GetEraserRect(type.ptStart));
					this._Graphics.FillEllipse(brush,GetEraserRect(type.ptStart));
				}
			}

			//draw current shapes
			if( isMouseDown && ! isMouseUp )
			{	
				ptEnd = this.PictureBox.PointToClient(Cursor.Position);

				rect = this.CalculateRect(ptStart,ptEnd);

				Pen pen = new Pen(this._ForeColor,1);

				//draw temp shapes
				#region Draw_Temp
				if(this.shape=="DrawEllipse" || this.shape=="DrawFillEllipse")
				{
					e.Graphics.DrawEllipse( pen, rect );
				}
				if(this.shape=="DrawRectangle" || this.shape=="DrawFillRectangle")
				{
					e.Graphics.DrawRectangle( pen, rect );
				}
				if(this.shape=="DrawLine")
				{
					e.Graphics.DrawLine(pen,this.ptStart,this.ptEnd);
				}
				if(this.shape=="DrawDashLine")
				{
					pen.DashStyle = DashStyle.Dash;

					e.Graphics.DrawLine(pen,this.ptStart,this.ptEnd);
					
					pen.DashStyle = DashStyle.Solid;
				}
				#endregion

				if(this.shape=="DrawCurve")
				{
					//e.Graphics.DrawLine(new Pen(this._ForeColor,this.LineWidth),this.ptStart,this.ptEnd);
					
					this._Graphics.DrawLine(new Pen(this._ForeColor,this.LineWidth),this.ptStart,this.ptEnd);
					
					_Shapes.Add( new ShapeType(this.shape, ptStart, ptEnd, this._ForeColor, this._BackColor, Brushes.Black , this.LineWidth ) );
					
					this.ptStart = this.ptEnd;
					
					this.ptEnd = Point.Empty;
				}

				if(this.shape=="DrawEraser")
				{
					//e.Graphics.FillEllipse(Brushes.White,rect);

					this._Graphics.FillEllipse(Brushes.White,GetEraserRect(ptStart));
					
					_Shapes.Add( new ShapeType(this.shape, ptStart, ptEnd, Color.White, Color.White, Brushes.White , this.LineWidth ) );

					this.ptStart = this.ptEnd;
					
					this.ptEnd = Point.Empty;
				}
			}
		}

		private void C_BtnOK_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void C_BtnCancel_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
	}
}
