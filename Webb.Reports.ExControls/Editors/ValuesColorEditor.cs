using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using Webb.Reports.ExControls.Data;

namespace Webb.Reports.ExControls.Editors
{
	#region class ValuesColorEditorForm
	public class ValuesColorEditorForm:System.Windows.Forms.Form
	{		
		private System.Windows.Forms.Splitter C_SplitterLR;
		private System.Windows.Forms.Panel C_PanelCtrls;
		private System.Windows.Forms.Splitter C_SplitterTB;
		private System.Windows.Forms.Button BtnOk;
		private System.Windows.Forms.Button BtnCancel;
		private System.Windows.Forms.PropertyGrid propertyGrid1;
		private System.Windows.Forms.Panel C_PanelMain;
		private System.Windows.Forms.Label LblFields;
		private System.Windows.Forms.Button C_BtnRemove;
		private System.Windows.Forms.Button C_BtnAdd;
		private System.Windows.Forms.ListBox C_LBFields;
		private System.Windows.Forms.ListBox C_LBSelFields;
		private System.Windows.Forms.Button C_BtnMoveUp;
		private System.Windows.Forms.Button C_BtnMoveDown;
		private System.Windows.Forms.Label LbSel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PropertyGrid C_PropertyGrid;
		public ValueColorCollection valuesColor;
		private ValueColorCollection OriginalValuesColor;

	

		public ValuesColorEditorForm(object value)
		{
            InitializeComponent();

			valuesColor=(value as ValueColorCollection).Copy();

			OriginalValuesColor=valuesColor.CreateValuesColor();

			this.SetView();

		}

		private void InitializeComponent()
		{
            this.C_SplitterLR = new System.Windows.Forms.Splitter();
            this.C_PanelCtrls = new System.Windows.Forms.Panel();
            this.BtnOk = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.C_SplitterTB = new System.Windows.Forms.Splitter();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.C_PanelMain = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.LblFields = new System.Windows.Forms.Label();
            this.C_BtnRemove = new System.Windows.Forms.Button();
            this.C_BtnAdd = new System.Windows.Forms.Button();
            this.C_LBFields = new System.Windows.Forms.ListBox();
            this.C_LBSelFields = new System.Windows.Forms.ListBox();
            this.C_BtnMoveUp = new System.Windows.Forms.Button();
            this.C_BtnMoveDown = new System.Windows.Forms.Button();
            this.LbSel = new System.Windows.Forms.Label();
            this.C_PropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.C_PanelCtrls.SuspendLayout();
            this.C_PanelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // C_SplitterLR
            // 
            this.C_SplitterLR.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.C_SplitterLR.Dock = System.Windows.Forms.DockStyle.Right;
            this.C_SplitterLR.Location = new System.Drawing.Point(580, 0);
            this.C_SplitterLR.Name = "C_SplitterLR";
            this.C_SplitterLR.Size = new System.Drawing.Size(4, 350);
            this.C_SplitterLR.TabIndex = 1;
            this.C_SplitterLR.TabStop = false;
            // 
            // C_PanelCtrls
            // 
            this.C_PanelCtrls.Controls.Add(this.BtnOk);
            this.C_PanelCtrls.Controls.Add(this.BtnCancel);
            this.C_PanelCtrls.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.C_PanelCtrls.Location = new System.Drawing.Point(0, 281);
            this.C_PanelCtrls.Name = "C_PanelCtrls";
            this.C_PanelCtrls.Size = new System.Drawing.Size(580, 69);
            this.C_PanelCtrls.TabIndex = 2;
            // 
            // BtnOk
            // 
            this.BtnOk.Location = new System.Drawing.Point(432, 17);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(96, 35);
            this.BtnOk.TabIndex = 0;
            this.BtnOk.Text = "OK";
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Location = new System.Drawing.Point(576, 17);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(96, 35);
            this.BtnCancel.TabIndex = 0;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // C_SplitterTB
            // 
            this.C_SplitterTB.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.C_SplitterTB.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.C_SplitterTB.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.C_SplitterTB.Location = new System.Drawing.Point(0, 278);
            this.C_SplitterTB.Name = "C_SplitterTB";
            this.C_SplitterTB.Size = new System.Drawing.Size(580, 3);
            this.C_SplitterTB.TabIndex = 3;
            this.C_SplitterTB.TabStop = false;
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.LineColor = System.Drawing.SystemColors.ScrollBar;
            this.propertyGrid1.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(130, 130);
            this.propertyGrid1.TabIndex = 0;
            // 
            // C_PanelMain
            // 
            this.C_PanelMain.Controls.Add(this.label1);
            this.C_PanelMain.Controls.Add(this.LblFields);
            this.C_PanelMain.Controls.Add(this.C_BtnRemove);
            this.C_PanelMain.Controls.Add(this.C_BtnAdd);
            this.C_PanelMain.Controls.Add(this.C_LBFields);
            this.C_PanelMain.Controls.Add(this.C_LBSelFields);
            this.C_PanelMain.Controls.Add(this.C_BtnMoveUp);
            this.C_PanelMain.Controls.Add(this.C_BtnMoveDown);
            this.C_PanelMain.Controls.Add(this.LbSel);
            this.C_PanelMain.Dock = System.Windows.Forms.DockStyle.Left;
            this.C_PanelMain.Location = new System.Drawing.Point(0, 0);
            this.C_PanelMain.Name = "C_PanelMain";
            this.C_PanelMain.Size = new System.Drawing.Size(490, 278);
            this.C_PanelMain.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(307, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "ValuesColor";
            // 
            // LblFields
            // 
            this.LblFields.Location = new System.Drawing.Point(10, 9);
            this.LblFields.Name = "LblFields";
            this.LblFields.Size = new System.Drawing.Size(163, 17);
            this.LblFields.TabIndex = 3;
            this.LblFields.Text = "Field Values In Table";
            // 
            // C_BtnRemove
            // 
            this.C_BtnRemove.Location = new System.Drawing.Point(182, 86);
            this.C_BtnRemove.Name = "C_BtnRemove";
            this.C_BtnRemove.Size = new System.Drawing.Size(77, 26);
            this.C_BtnRemove.TabIndex = 2;
            this.C_BtnRemove.Text = "<=";
            this.C_BtnRemove.Click += new System.EventHandler(this.C_BtnRemove_Click);
            // 
            // C_BtnAdd
            // 
            this.C_BtnAdd.Location = new System.Drawing.Point(182, 43);
            this.C_BtnAdd.Name = "C_BtnAdd";
            this.C_BtnAdd.Size = new System.Drawing.Size(77, 26);
            this.C_BtnAdd.TabIndex = 1;
            this.C_BtnAdd.Text = "=>";
            this.C_BtnAdd.Click += new System.EventHandler(this.C_BtnAdd_Click);
            // 
            // C_LBFields
            // 
            this.C_LBFields.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.C_LBFields.HorizontalScrollbar = true;
            this.C_LBFields.ItemHeight = 12;
            this.C_LBFields.Location = new System.Drawing.Point(0, 34);
            this.C_LBFields.Name = "C_LBFields";
            this.C_LBFields.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.C_LBFields.Size = new System.Drawing.Size(163, 220);
            this.C_LBFields.Sorted = true;
            this.C_LBFields.TabIndex = 0;
            this.C_LBFields.DoubleClick += new System.EventHandler(this.C_LBFields_DoubleClick);
            // 
            // C_LBSelFields
            // 
            this.C_LBSelFields.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.C_LBSelFields.HorizontalScrollbar = true;
            this.C_LBSelFields.ItemHeight = 12;
            this.C_LBSelFields.Location = new System.Drawing.Point(298, 34);
            this.C_LBSelFields.Name = "C_LBSelFields";
            this.C_LBSelFields.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.C_LBSelFields.Size = new System.Drawing.Size(182, 220);
            this.C_LBSelFields.TabIndex = 0;
            this.C_LBSelFields.DoubleClick += new System.EventHandler(this.C_LBSelFields_DoubleClick);
            this.C_LBSelFields.SelectedValueChanged += new System.EventHandler(this.C_LBSelFields_SelectedValueChanged);
            // 
            // C_BtnMoveUp
            // 
            this.C_BtnMoveUp.Location = new System.Drawing.Point(182, 181);
            this.C_BtnMoveUp.Name = "C_BtnMoveUp";
            this.C_BtnMoveUp.Size = new System.Drawing.Size(87, 25);
            this.C_BtnMoveUp.TabIndex = 0;
            this.C_BtnMoveUp.Text = "Move Up";
            this.C_BtnMoveUp.Click += new System.EventHandler(this.C_BtnMoveUp_Click);
            // 
            // C_BtnMoveDown
            // 
            this.C_BtnMoveDown.Location = new System.Drawing.Point(182, 241);
            this.C_BtnMoveDown.Name = "C_BtnMoveDown";
            this.C_BtnMoveDown.Size = new System.Drawing.Size(87, 25);
            this.C_BtnMoveDown.TabIndex = 1;
            this.C_BtnMoveDown.Text = "Move Down";
            this.C_BtnMoveDown.Click += new System.EventHandler(this.C_BtnMoveDown_Click);
            // 
            // LbSel
            // 
            this.LbSel.Location = new System.Drawing.Point(566, 9);
            this.LbSel.Name = "LbSel";
            this.LbSel.Size = new System.Drawing.Size(106, 17);
            this.LbSel.TabIndex = 3;
            this.LbSel.Text = "ValuesColor";
            // 
            // C_PropertyGrid
            // 
            this.C_PropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.C_PropertyGrid.LineColor = System.Drawing.SystemColors.ScrollBar;
            this.C_PropertyGrid.Location = new System.Drawing.Point(490, 0);
            this.C_PropertyGrid.Name = "C_PropertyGrid";
            this.C_PropertyGrid.Size = new System.Drawing.Size(90, 278);
            this.C_PropertyGrid.TabIndex = 7;
            // 
            // ValuesColorEditorForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(584, 350);
            this.Controls.Add(this.C_PropertyGrid);
            this.Controls.Add(this.C_PanelMain);
            this.Controls.Add(this.C_SplitterTB);
            this.Controls.Add(this.C_PanelCtrls);
            this.Controls.Add(this.C_SplitterLR);
            this.Name = "ValuesColorEditorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ValuesColor Editor";
            this.C_PanelCtrls.ResumeLayout(false);
            this.C_PanelMain.ResumeLayout(false);
            this.ResumeLayout(false);

		}


		private void SetView()
		{
			this.C_LBFields.Items.Clear();
			this.C_LBSelFields.Items.Clear();

			foreach(ValueColor valueColor in this.valuesColor)
			{
				if(!OriginalValuesColor.Contains(valueColor))
				{
					valueColor.Hidden=true;
				}

				this.C_LBSelFields.Items.Add(valueColor);
			}		 

			foreach(ValueColor valueColor in this.OriginalValuesColor)
			{
				if(!valuesColor.Contains(valueColor))
				{
					this.C_LBFields.Items.Add(valueColor);
				}
			}
		}
		private void UpdateView()
		{
			ValueColorCollection InitValues=new ValueColorCollection();

			InitValues.series=valuesColor.series;
			
			foreach(ValueColor value in this.C_LBSelFields.Items)
			{
				value.Hidden=false;
				InitValues.Add(value.Copy());
			}
			this.valuesColor=InitValues;

		}
		
		private void BtnOk_Click(object sender, System.EventArgs e)
		{
			this.UpdateView();
			this.DialogResult=DialogResult.OK;
		}

		private void BtnCancel_Click(object sender, System.EventArgs e)
		{			
			DialogResult=DialogResult.Cancel;
		}

		private void C_BtnAdd_Click(object sender, System.EventArgs e)
		{
			ArrayList selected = new ArrayList();

			foreach(object value in this.C_LBFields.SelectedItems)
			{				
				selected.Add(value);				
			}

			foreach(object value in selected)
			{				
				this.AddItem(this.C_LBSelFields,value);

				this.C_LBFields.Items.Remove(value);				
			}

			this.C_LBSelFields.SelectedIndex = -1;
		}

		private void C_BtnRemove_Click(object sender, System.EventArgs e)
		{			
			ValueColorCollection InitValues=new ValueColorCollection();		
	
			InitValues.series=valuesColor.series;

			for(int i=0;i<this.C_LBSelFields.Items.Count;i++)
			{
				ValueColor elet=this.C_LBSelFields.Items[i] as ValueColor;

				if(!this.C_LBSelFields.SelectedIndices.Contains(i))
				{
					InitValues.Add(elet.Copy());
				}		
			}
			
			this.valuesColor=InitValues;

			this.SetView();			

			this.C_LBSelFields.SelectedIndex = -1;
			this.C_PropertyGrid.SelectedObject =null;
		}

		private void C_LBFields_DoubleClick(object sender, System.EventArgs e)
		{
			object value = this.C_LBFields.SelectedItem;

			if(value == null) return;
			
			this.AddItem(this.C_LBSelFields,value);
			
			this.C_LBFields.Items.Remove(value);
			
			
			this.C_LBSelFields.SelectedIndex = -1;
		}

		private void C_LBSelFields_DoubleClick(object sender, System.EventArgs e)
		{
			object value = this.C_LBSelFields.SelectedItem;

			if(value == null) return;

			this.AddItem(this.C_LBFields,value);

			this.C_LBSelFields.Items.Remove(value);

			this.C_LBSelFields.SelectedIndex = -1;
			
		}
		
		private void AddItem(ListBox lb, object item)
		{
			ValueColor itemEle=item as ValueColor;

			foreach(ValueColor element in lb.Items)
			{
				if(element.Value == itemEle.Value)
				{
					return;
				}
			}
			
			lb.Items.Add(itemEle.Copy());
		}
		
		private void C_BtnMoveUp_Click(object sender, System.EventArgs e)
		{
			int index = this.C_LBSelFields.SelectedIndex;

			if(index <= 0) return;

			object value = this.C_LBSelFields.Items[index];

			this.C_LBSelFields.Items.RemoveAt(index);

			this.C_LBSelFields.Items.Insert(index - 1,value);

			this.C_LBSelFields.SelectedIndex = index - 1;
		}

		private void C_BtnMoveDown_Click(object sender, System.EventArgs e)
		{
			int index = this.C_LBSelFields.SelectedIndex;

			if(index < 0 || index > this.C_LBSelFields.Items.Count - 2) return;

			object value = this.C_LBSelFields.Items[index];

			this.C_LBSelFields.Items.RemoveAt(index);

			this.C_LBSelFields.Items.Insert(index + 1,value);

			this.C_LBSelFields.SelectedIndex = index + 1;
		}


		private void C_LBSelFields_SelectedValueChanged(object sender, System.EventArgs e)
		{//06-30-2008@Scott edit multiply properties
			if(this.C_LBSelFields.SelectedItems.Count <= 0) return;

			object[] items = new object[this.C_LBSelFields.SelectedItems.Count];

			int index = 0;

			foreach(object item in this.C_LBSelFields.SelectedItems)
			{
				if(item is ValueColor && index < this.C_LBSelFields.SelectedItems.Count)
				{
					items.SetValue(item as ValueColor, index++);
				}
				else
				{
					items.SetValue(item as ValueColor, index++);
				}
			}

			this.C_PropertyGrid.SelectedObjects = items;
		}
	
		
	}
	#endregion

	#region class ValuesColorEditor
	public class  ValuesColorEditor: System.Drawing.Design.UITypeEditor
	{
		[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name="FullTrust")] 
		public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
		{
			return System.Drawing.Design.UITypeEditorEditStyle.Modal;
		}

		// Displays the UI for value selection.
		[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name="FullTrust")]
		public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, System.IServiceProvider provider, object value)
		{
			if(!(value is ValueColorCollection))
				return value;
			
			ValuesColorEditorForm editorform=new ValuesColorEditorForm(value);

			System.Windows.Forms.Design.IWindowsFormsEditorService edSvc = (System.Windows.Forms.Design.IWindowsFormsEditorService)provider.GetService(typeof(System.Windows.Forms.Design.IWindowsFormsEditorService));
		
			if (edSvc != null)
			{
				if (edSvc.ShowDialog(editorform) == DialogResult.OK)
				{
					return editorform.valuesColor;
				}
			}
            
			return value;
		}

	
		[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name="FullTrust")]   
		public override void PaintValue(System.Drawing.Design.PaintValueEventArgs e)
		{			
		}        
		
		[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name="FullTrust")]
		public override bool GetPaintValueSupported(System.ComponentModel.ITypeDescriptorContext context)
		{
			return false;
		}
	}
	#endregion
}
