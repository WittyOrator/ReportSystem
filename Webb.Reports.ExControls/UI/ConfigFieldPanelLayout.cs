using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Reflection;

using Webb.Reports.ExControls.Views;
using Webb.Reports.ExControls.Data;
using Webb.Collections;

namespace Webb.Reports.ExControls.UI
{
	/// <summary>
	/// Summary description for ConfigFieldPanelLayout.
	/// </summary>
	public class ConfigFieldPanelLayout : Webb.Reports.ExControls.UI.ExControlDesignerControlBase
	{
		private System.Windows.Forms.PropertyGrid C_PropertyGrid;
		private System.Windows.Forms.Splitter C_Splitter;
		private System.Windows.Forms.Panel C_FieldLayoutPanel;
		private ArrayList C_Buttons;
		private FieldLayOut _LayOut;
		private Int32Collection _ColsInRows;
		private SectionFilterCollection _SectionFilters;
		private System.Windows.Forms.Panel C_ControlPanel;
		private System.Windows.Forms.Button C_Refresh;
		private System.Windows.Forms.Splitter C_HSplitter;
		private System.Windows.Forms.Button C_btnLayOut;
     
		private const int MIN_SIZE = 50;
//        private Control _CurSelectedButton;
//		private int nTriggerLen = 10;
        private SplitButtons CustomSplit = new SplitButtons();
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public int LayoutPanelWidth
		{
			get{return this.C_FieldLayoutPanel.Width;}
		}

		public int LayoutPanelHeight
		{
			get{return this.C_FieldLayoutPanel.Height;}
		}

		public Rectangle LayoutRect
		{
			get{return new Rectangle(new Point(this.C_FieldLayoutPanel.Location.X,this.C_FieldLayoutPanel.Location.Y - 2),this.C_FieldLayoutPanel.Size);}
		}

		public ConfigFieldPanelLayout()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			this.C_Buttons = new ArrayList();
			this._ColsInRows = new Int32Collection();
			this._LayOut = new FieldLayOut();
			this._SectionFilters = new SectionFilterCollection();
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.Opaque, true); 
			// TODO: Add any initialization after the InitializeComponent call
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
                if (this.CustomSplit != null)
                {
                    CustomSplit.Dispose(disposing);
                }
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.C_PropertyGrid = new System.Windows.Forms.PropertyGrid();
			this.C_Splitter = new System.Windows.Forms.Splitter();
			this.C_FieldLayoutPanel = new System.Windows.Forms.Panel();
			this.C_ControlPanel = new System.Windows.Forms.Panel();
			this.C_btnLayOut = new System.Windows.Forms.Button();
			this.C_Refresh = new System.Windows.Forms.Button();
			this.C_HSplitter = new System.Windows.Forms.Splitter();
			this.C_ControlPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// C_PropertyGrid
			// 
			this.C_PropertyGrid.CommandsVisibleIfAvailable = true;
			this.C_PropertyGrid.Dock = System.Windows.Forms.DockStyle.Right;
			this.C_PropertyGrid.LargeButtons = false;
			this.C_PropertyGrid.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.C_PropertyGrid.Location = new System.Drawing.Point(451, 0);
			this.C_PropertyGrid.Name = "C_PropertyGrid";
			this.C_PropertyGrid.Size = new System.Drawing.Size(200, 422);
			this.C_PropertyGrid.TabIndex = 0;
			this.C_PropertyGrid.Text = "propertyGrid1";
			this.C_PropertyGrid.ViewBackColor = System.Drawing.SystemColors.Window;
			this.C_PropertyGrid.ViewForeColor = System.Drawing.SystemColors.WindowText;
			// 
			// C_Splitter
			// 
			this.C_Splitter.Dock = System.Windows.Forms.DockStyle.Right;
			this.C_Splitter.Location = new System.Drawing.Point(448, 0);
			this.C_Splitter.Name = "C_Splitter";
			this.C_Splitter.Size = new System.Drawing.Size(3, 422);
			this.C_Splitter.TabIndex = 1;
			this.C_Splitter.TabStop = false;
			// 
			// C_FieldLayoutPanel
			// 
			this.C_FieldLayoutPanel.BackColor = System.Drawing.Color.LightBlue;
			this.C_FieldLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.C_FieldLayoutPanel.Location = new System.Drawing.Point(0, 3);
			this.C_FieldLayoutPanel.Name = "C_FieldLayoutPanel";
			this.C_FieldLayoutPanel.Size = new System.Drawing.Size(448, 347);
			this.C_FieldLayoutPanel.TabIndex = 0;
			this.C_FieldLayoutPanel.SizeChanged += new System.EventHandler(this.C_FieldLayoutPanel_SizeChanged);
			this.C_FieldLayoutPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.C_FieldLayoutPanel_Paint);
			// 
			// C_ControlPanel
			// 
			this.C_ControlPanel.AutoScroll = true;
			this.C_ControlPanel.AutoScrollMinSize = new System.Drawing.Size(300, 50);
			this.C_ControlPanel.Controls.Add(this.C_btnLayOut);
			this.C_ControlPanel.Controls.Add(this.C_Refresh);
			this.C_ControlPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.C_ControlPanel.Location = new System.Drawing.Point(0, 350);
			this.C_ControlPanel.Name = "C_ControlPanel";
			this.C_ControlPanel.Size = new System.Drawing.Size(448, 72);
			this.C_ControlPanel.TabIndex = 2;
			// 
			// C_btnLayOut
			// 
			this.C_btnLayOut.Location = new System.Drawing.Point(16, 24);
			this.C_btnLayOut.Name = "C_btnLayOut";
			this.C_btnLayOut.TabIndex = 1;
			this.C_btnLayOut.Text = "Layout";
			this.C_btnLayOut.Click += new System.EventHandler(this.C_btnLayOut_Click);
			// 
			// C_Refresh
			// 
			this.C_Refresh.Location = new System.Drawing.Point(104, 24);
			this.C_Refresh.Name = "C_Refresh";
			this.C_Refresh.Size = new System.Drawing.Size(75, 24);
			this.C_Refresh.TabIndex = 0;
			this.C_Refresh.Text = "Refresh";
			this.C_Refresh.Visible = false;
			this.C_Refresh.Click += new System.EventHandler(this.C_Refresh_Click);
			// 
			// C_HSplitter
			// 
			this.C_HSplitter.Cursor = System.Windows.Forms.Cursors.HSplit;
			this.C_HSplitter.Dock = System.Windows.Forms.DockStyle.Top;
			this.C_HSplitter.Location = new System.Drawing.Point(0, 0);
			this.C_HSplitter.Name = "C_HSplitter";
			this.C_HSplitter.Size = new System.Drawing.Size(448, 3);
			this.C_HSplitter.TabIndex = 3;
			this.C_HSplitter.TabStop = false;
			// 
			// ConfigFieldPanelLayout
			// 
			this.Controls.Add(this.C_FieldLayoutPanel);
			this.Controls.Add(this.C_HSplitter);
			this.Controls.Add(this.C_ControlPanel);
			this.Controls.Add(this.C_Splitter);
			this.Controls.Add(this.C_PropertyGrid);
			this.Name = "ConfigFieldPanelLayout";
			this.Size = new System.Drawing.Size(651, 422);
			this.C_ControlPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		public override void SetView(ExControlView i_View)
		{
			
            FieldPanelView mainView = i_View as FieldPanelView;

            this._LayOut.Apply(mainView.LayOut);

            this._SectionFilters.Apply(mainView.SectionFilters);

            this.C_PropertyGrid.SelectedObject = this._LayOut;

            this.LoadButtons(true);

            //this.AdjustButtons(this.LayoutRect,this._LayOut);
		}

		public override void UpdateView(ExControlView i_View)
		{
			#region Modify this property here for Excluding numbers not bigger than 0 at 2008-11-27 14:39:35@Simon			
			for(int i=0;i<_LayOut.ColumnsEachRow.Count;i++)
			{
				if(i>=_LayOut.ColumnsEachRow.Count)break;
				if(_LayOut.ColumnsEachRow[i]<=0)
				{ 
					_LayOut.ColumnsEachRow.RemoveAt(i);
					i--;
				}
			}
			#endregion        //End Modify

			this.RefreshLayout();

			FieldPanelView mainView = i_View as FieldPanelView;

			this.UpdateLayout(this._LayOut);
			
			this.UpdateSectionFilters(mainView.SectionFilters);

			mainView.LayOut.Apply(this._LayOut);
		}

		public void UpdateSectionFilters(SectionFilterCollection secFilters)
		{
            if (secFilters == null) return;

            secFilters.Clear();

            for (int i = 0; i < this.C_Buttons.Count; i++)
            {
                ButtonTags pctag = (ButtonTags)(this.C_Buttons[i] as Control).Tag;

                SectionFilter secFilter = pctag.filter;

                SectionFilter newSecFilter = new SectionFilter();

                newSecFilter.Apply(secFilter);

                secFilters.Add(newSecFilter);
            }
		}

		/// <summary>
		/// Update layout by UI
		/// </summary>
		/// <param name="layout"></param>
		private void UpdateLayout(FieldLayOut layout)
		{
            for (int row = 0; row < layout.ColumnsEachRow.Count; row++)
            {
                int nCols = layout.ColumnsEachRow[row];

                for (int col = 0; col < nCols; col++)
                {
                    Button btn = CustomSplit.GetButton(row, col);

                    System.Diagnostics.Debug.Assert(btn != null);

                    Field field = layout.FieldTable.GetField(row, col);

					if(field != null)
					{
//						System.Diagnostics.Debug.Assert(field != null);

						ButtonTags pctag = (ButtonTags)btn.Tag;

						field.Filter.Apply(pctag.filter);

						field.SetWidthRatio((float)btn.Width / this.LayoutPanelWidth);

						field.SetHeightRatio((float)btn.Height / this.LayoutPanelHeight);
					}
                }
            }
		}

		private void C_FieldLayoutPanel_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			
		}

		private void CreateFieldTable(FieldLayOut layout)
		{
			layout.UpdateFields();
		}
		private void LoadLayoutSetting()
		{
			this.CustomSplit.mainControl=this.C_FieldLayoutPanel;

			CustomSplit.C_PropertyGrid=this.C_PropertyGrid;		
		           
			if (!CustomSplit.LoadSectionFilters(_LayOut.ColumnsEachRow, this._SectionFilters)) return;

			CustomSplit.LoadLayout(this._LayOut,this._SectionFilters);

		}

        private void LoadButtons(bool flag)
        {

            if (!this._LayOut.ColumnsEachRow.ValueEqual(this._ColsInRows) || (this._LayOut.ColumnsEachRow.Count != 0 && this._LayOut.FieldTable.Count == 0))	//Modified at 2008-11-12 16:44:06@Scott
            {
                this._LayOut.ColumnsEachRow.CopyTo(this._ColsInRows);

                this.CreateFieldTable(this._LayOut);

                LoadLayoutSetting();
               
                this.C_Buttons = CustomSplit.GetButton();

                this.Refresh();

            }

        }
		private void CreateLayOut()
		{
			this._LayOut.ColumnsEachRow.CopyTo(this._ColsInRows);
			this.CreateFieldTable(this._LayOut);

			LoadLayoutSetting();
               
			this.C_Buttons = CustomSplit.GetButton();
			this.Refresh();
		}

        private void C_btnLayOut_Click(object sender, System.EventArgs e)
        {
            foreach (object o in this.C_Buttons)
            {

                Control btn = o as Control;
                btn.BackColor = Control.DefaultBackColor;
            }
            this.C_PropertyGrid.SelectedObject = this._LayOut;
        }

		private void C_Refresh_Click(object sender, System.EventArgs e)
		{
			this.RefreshLayout();
		}

        private void C_FieldLayoutPanel_SizeChanged(object sender, System.EventArgs e)
        {	
			CreateLayOut();
        }        


        private void RefreshLayout()
        {
            this.LoadButtons(true);

        }
    }
}
