using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Collections;
using System.Data;

using Webb.Reports.ExControls.Data;
using Webb.Data;



namespace Webb.Reports.ExControls.Editors
{
    /// <summary>
    /// [EditorAttribute(typeof(DBFilterEditor), typeof(System.Drawing.Design.UITypeEditor))]
    /// </summary>

    #region public class CalculateColumnEditorForm : System.Windows.Forms.Form
    /// <summary>
    /// Summary description for FilterEditForm.
    /// </summary>
    public class CalculateColumnEditorForm : System.Windows.Forms.Form
    {   
        private DataTable conditionTable;
        private CalculateColumnInfo value;
        private int nSelectedRow;
        private int nSelectedCol;
        private string strTableName = "Condition Table";
      
        private System.Windows.Forms.Button C_BtnOK;
        private System.Windows.Forms.Button C_BtnCancel;
        private System.Windows.Forms.Button C_BtnAddCondition;
        private System.Windows.Forms.Button C_BtnRemoveCondition;
        private System.Windows.Forms.ComboBox C_ComboValue;
        private System.Windows.Forms.DataGrid C_DataGrid;
        private System.Windows.Forms.Button C_BtnExport;
        private System.Windows.Forms.Button C_BtnImport;
        private Button BtnEditField;
        private CheckBox chkDisplayClearSteps;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public CalculateColumnInfo Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public void HidechkDisplayedSteps()
        {
            this.chkDisplayClearSteps.Visible = false;
        }

        public CalculateColumnEditorForm(CalculateColumnInfo calcuateColumnInfo)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            //Create table style

            DataGridTableStyle style = new DataGridTableStyle();

            style.PreferredRowHeight = 22;

            style.MappingName = strTableName;

            this.C_DataGrid.TableStyles.Add(style);
            //
            this.Value = calcuateColumnInfo.Copy();

            this.chkDisplayClearSteps.Checked = this.value.DisplayedCalculteStep;

            this.CreateConditionTable();               
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (this.conditionTable != null) this.conditionTable.Dispose();

            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        //Ok
        private void C_BtnOK_Click(object sender, System.EventArgs e)
        {
            this.HideCombo();          

            this.UpdateFilter();

            bool CheckBrackt = this.CheckBracketAreCompared();

            if (!CheckBrackt)
            {
                Webb.Utilities.MessageBoxEx.ShowError("Invalid filter setting about brackets:Bracket_Start not compared with Bracket_End!");

                return;
            }

            this.DialogResult = DialogResult.OK;

            this.Close();
        }



        //Cancel
        private void C_BtnCancel_Click(object sender, System.EventArgs e)
        {          

            this.DialogResult = DialogResult.Cancel;

            this.Close();
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.C_BtnOK = new System.Windows.Forms.Button();
            this.C_BtnCancel = new System.Windows.Forms.Button();
            this.C_BtnAddCondition = new System.Windows.Forms.Button();
            this.C_BtnRemoveCondition = new System.Windows.Forms.Button();
            this.C_ComboValue = new System.Windows.Forms.ComboBox();
            this.C_DataGrid = new System.Windows.Forms.DataGrid();
            this.C_BtnExport = new System.Windows.Forms.Button();
            this.C_BtnImport = new System.Windows.Forms.Button();
            this.BtnEditField = new System.Windows.Forms.Button();
            this.chkDisplayClearSteps = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.C_DataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // C_BtnOK
            // 
            this.C_BtnOK.Location = new System.Drawing.Point(425, 293);
            this.C_BtnOK.Name = "C_BtnOK";
            this.C_BtnOK.Size = new System.Drawing.Size(76, 23);
            this.C_BtnOK.TabIndex = 1;
            this.C_BtnOK.Text = "OK";
            this.C_BtnOK.Click += new System.EventHandler(this.C_BtnOK_Click);
            // 
            // C_BtnCancel
            // 
            this.C_BtnCancel.Location = new System.Drawing.Point(534, 293);
            this.C_BtnCancel.Name = "C_BtnCancel";
            this.C_BtnCancel.Size = new System.Drawing.Size(75, 23);
            this.C_BtnCancel.TabIndex = 2;
            this.C_BtnCancel.Text = "Cancel";
            this.C_BtnCancel.Click += new System.EventHandler(this.C_BtnCancel_Click);
            // 
            // C_BtnAddCondition
            // 
            this.C_BtnAddCondition.Location = new System.Drawing.Point(19, 9);
            this.C_BtnAddCondition.Name = "C_BtnAddCondition";
            this.C_BtnAddCondition.Size = new System.Drawing.Size(77, 23);
            this.C_BtnAddCondition.TabIndex = 3;
            this.C_BtnAddCondition.Text = "Add Row";
            this.C_BtnAddCondition.Click += new System.EventHandler(this.C_BtnAddCondition_Click);
            // 
            // C_BtnRemoveCondition
            // 
            this.C_BtnRemoveCondition.Location = new System.Drawing.Point(126, 9);
            this.C_BtnRemoveCondition.Name = "C_BtnRemoveCondition";
            this.C_BtnRemoveCondition.Size = new System.Drawing.Size(103, 23);
            this.C_BtnRemoveCondition.TabIndex = 4;
            this.C_BtnRemoveCondition.Text = "Remove Row";
            this.C_BtnRemoveCondition.Click += new System.EventHandler(this.C_BtnRemoveCondition_Click);
            // 
            // C_ComboValue
            // 
            this.C_ComboValue.DropDownHeight = 350;
            this.C_ComboValue.IntegralHeight = false;
            this.C_ComboValue.Items.AddRange(new object[] {
            "1",
            "2",
            "3"});
            this.C_ComboValue.Location = new System.Drawing.Point(566, 263);
            this.C_ComboValue.Name = "C_ComboValue";
            this.C_ComboValue.Size = new System.Drawing.Size(120, 20);
            this.C_ComboValue.Sorted = true;
            this.C_ComboValue.TabIndex = 8;
            this.C_ComboValue.Visible = false;
            this.C_ComboValue.VisibleChanged += new System.EventHandler(this.C_ComboValue_VisibleChanged);
            this.C_ComboValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.C_ComboValue_KeyDown);
            // 
            // C_DataGrid
            // 
            this.C_DataGrid.AllowSorting = false;
            this.C_DataGrid.AlternatingBackColor = System.Drawing.Color.GhostWhite;
            this.C_DataGrid.BackColor = System.Drawing.Color.GhostWhite;
            this.C_DataGrid.BackgroundColor = System.Drawing.Color.Lavender;
            this.C_DataGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.C_DataGrid.CaptionBackColor = System.Drawing.Color.RoyalBlue;
            this.C_DataGrid.CaptionForeColor = System.Drawing.Color.White;
            this.C_DataGrid.DataMember = "";
            this.C_DataGrid.FlatMode = true;
            this.C_DataGrid.Font = new System.Drawing.Font("Tahoma", 8F);
            this.C_DataGrid.ForeColor = System.Drawing.Color.MidnightBlue;
            this.C_DataGrid.GridLineColor = System.Drawing.Color.RoyalBlue;
            this.C_DataGrid.HeaderBackColor = System.Drawing.Color.MidnightBlue;
            this.C_DataGrid.HeaderFont = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.C_DataGrid.HeaderForeColor = System.Drawing.Color.Lavender;
            this.C_DataGrid.LinkColor = System.Drawing.Color.Teal;
            this.C_DataGrid.Location = new System.Drawing.Point(19, 43);
            this.C_DataGrid.Name = "C_DataGrid";
            this.C_DataGrid.ParentRowsBackColor = System.Drawing.Color.Lavender;
            this.C_DataGrid.ParentRowsForeColor = System.Drawing.Color.MidnightBlue;
            this.C_DataGrid.PreferredRowHeight = 22;
            this.C_DataGrid.ReadOnly = true;
            this.C_DataGrid.SelectionBackColor = System.Drawing.Color.Teal;
            this.C_DataGrid.SelectionForeColor = System.Drawing.Color.PaleGreen;
            this.C_DataGrid.Size = new System.Drawing.Size(590, 227);
            this.C_DataGrid.TabIndex = 9;
            this.C_DataGrid.Scroll += new System.EventHandler(this.C_DataGrid_Scroll);
            this.C_DataGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.C_DataGrid_MouseDown);
            // 
            // C_BtnExport
            // 
            this.C_BtnExport.Location = new System.Drawing.Point(108, 293);
            this.C_BtnExport.Name = "C_BtnExport";
            this.C_BtnExport.Size = new System.Drawing.Size(76, 23);
            this.C_BtnExport.TabIndex = 10;
            this.C_BtnExport.Text = "Export";
            this.C_BtnExport.Click += new System.EventHandler(this.C_BtnExport_Click);
            // 
            // C_BtnImport
            // 
            this.C_BtnImport.Location = new System.Drawing.Point(22, 293);
            this.C_BtnImport.Name = "C_BtnImport";
            this.C_BtnImport.Size = new System.Drawing.Size(74, 23);
            this.C_BtnImport.TabIndex = 11;
            this.C_BtnImport.Text = "Import";
            this.C_BtnImport.Click += new System.EventHandler(this.C_BtnImport_Click);
            // 
            // BtnEditField
            // 
            this.BtnEditField.Font = new System.Drawing.Font("Tahoma", 8F);
            this.BtnEditField.Location = new System.Drawing.Point(576, 247);
            this.BtnEditField.Name = "BtnEditField";
            this.BtnEditField.Size = new System.Drawing.Size(100, 23);
            this.BtnEditField.TabIndex = 20;
            this.BtnEditField.Text = "Edit";
            this.BtnEditField.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.BtnEditField.UseVisualStyleBackColor = true;
            this.BtnEditField.Visible = false;
            this.BtnEditField.Click += new System.EventHandler(this.BtnEditField_Click);
            // 
            // chkDisplayClearSteps
            // 
            this.chkDisplayClearSteps.AutoSize = true;
            this.chkDisplayClearSteps.Location = new System.Drawing.Point(425, 13);
            this.chkDisplayClearSteps.Name = "chkDisplayClearSteps";
            this.chkDisplayClearSteps.Size = new System.Drawing.Size(174, 16);
            this.chkDisplayClearSteps.TabIndex = 21;
            this.chkDisplayClearSteps.Text = "Display calculating Steps";
            this.chkDisplayClearSteps.UseVisualStyleBackColor = true;
            // 
            // CalculateColumnEditorForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(621, 328);
            this.Controls.Add(this.chkDisplayClearSteps);
            this.Controls.Add(this.BtnEditField);
            this.Controls.Add(this.C_BtnImport);
            this.Controls.Add(this.C_BtnExport);
            this.Controls.Add(this.C_ComboValue);
            this.Controls.Add(this.C_DataGrid);
            this.Controls.Add(this.C_BtnRemoveCondition);
            this.Controls.Add(this.C_BtnAddCondition);
            this.Controls.Add(this.C_BtnCancel);
            this.Controls.Add(this.C_BtnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CalculateColumnEditorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CalculateColumn EditForm";
            ((System.ComponentModel.ISupportInitialize)(this.C_DataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private void CreateConditionTable()
        {
            this.CreateConditionTableStructure();

            this.FillConditionTable();

            this.C_DataGrid.DataSource = this.conditionTable.DefaultView;

            //			conditionTable.GetChanges(DataRowState.

            this.C_DataGrid.TableStyles[strTableName].GridColumnStyles["Field"].Width = 220;
            this.C_DataGrid.TableStyles[strTableName].GridColumnStyles["ConstantOperator"].Width = 100;
            this.C_DataGrid.TableStyles[strTableName].GridColumnStyles["ConstantValue"].Width = 80;
            this.C_DataGrid.TableStyles[strTableName].GridColumnStyles["FieldOperator"].Width = 100;    
            this.C_DataGrid.TableStyles[strTableName].GridColumnStyles["Bracket"].Width = 50;

            this.C_DataGrid.TableStyles[strTableName].AllowSorting = false;
        }

        private void CreateConditionTableStructure()
        {
            conditionTable = new DataTable(strTableName);

            //field column
            DataColumn colField = new DataColumn("Field", typeof(object));

            colField.DefaultValue = string.Empty;

            //operator column
            DataColumn colConstantOperator = new DataColumn("ConstantOperator", typeof(CombinedOperation));

            colConstantOperator.DefaultValue = CombinedOperation.StrConcat;


            //ConstantValue column
            DataColumn colConstantValue = new DataColumn("ConstantValue", typeof(string));

            colConstantValue.DefaultValue = string.Empty;


            //operator column
            DataColumn colFieldOperator = new DataColumn("FieldOperator", typeof(CombinedOperation));

            colFieldOperator.DefaultValue = CombinedOperation.StrConcat;

            //bracket column
            DataColumn colBracket = new DataColumn("Bracket", typeof(Webb.Data.Bracket));

            colBracket.DefaultValue = Webb.Data.Bracket.NONE;

            //add columns
            conditionTable.Columns.Add(colField);
            conditionTable.Columns.Add(colConstantOperator);
            conditionTable.Columns.Add(colConstantValue);
            conditionTable.Columns.Add(colFieldOperator);         
            conditionTable.Columns.Add(colBracket);
        }


        private void FillConditionTable()
        {
            if (this.Value == null) return;

            foreach (CalcElement calcElement in this.Value)
            {
                DataRow row = this.conditionTable.NewRow();

                row[0] = calcElement.Field;
                row[1] = calcElement.CacluteConstantOperation;
                row[2] = calcElement.ConstantValue;
                row[3] = calcElement.CacluteFieldOperation;
                row[4] = calcElement.Bracket;

                this.conditionTable.Rows.Add(row);
            }
        }

        private bool CheckBracketAreCompared()
        {
            Stack BracketStack = new Stack();
            foreach (CalcElement calcElement in value)
            {
                if (calcElement.Bracket == Bracket.Start)
                {
                    BracketStack.Push("S");
                }
                else if (calcElement.Bracket == Bracket.End)
                {
                    if (BracketStack.Count == 0)
                    {
                        return false;
                    }
                    else
                    {
                        BracketStack.Pop();
                    }
                }
            }
            if (BracketStack.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckAllBracketIsNone()
        {

            foreach (CalcElement calcElement in value)
            {
                if (calcElement.Bracket != Bracket.NONE)
                {
                    return false;
                }
            }
            return true;

        }

        private void UpdateFilter()
        {
            this.value.Clear();  

            foreach (DataRowView row in this.conditionTable.DefaultView)
            {
                CalcElement calcElement = new CalcElement();

                calcElement.Field = row[0];
                calcElement.CacluteConstantOperation = (CombinedOperation)row[1];
                calcElement.ConstantValue = row[2] == null ? string.Empty : row[2].ToString();
                calcElement.CacluteFieldOperation = (CombinedOperation)row[3];
                calcElement.Bracket = (Webb.Data.Bracket)row[4];
                this.value.Add(calcElement);
            }
            this.value.DisplayedCalculteStep = this.chkDisplayClearSteps.Checked;
        }

        //Add condition
        private void C_BtnAddCondition_Click(object sender, System.EventArgs e)
        {
            DataRow row = this.conditionTable.NewRow();

            this.conditionTable.Rows.Add(row);

            HideCombo();
           
        }

        //Remove condition
        private void C_BtnRemoveCondition_Click(object sender, System.EventArgs e)
        { 
            if (this.C_DataGrid.CurrentRowIndex >= 0 && this.C_DataGrid.CurrentRowIndex < this.conditionTable.Rows.Count)
            {
                this.conditionTable.DefaultView.Delete(this.C_DataGrid.CurrentRowIndex);
            }

            HideCombo();            
        }

        //Move up
        private void C_BtnMoveUp_Click(object sender, System.EventArgs e)
        {
            HideCombo();         

            int nRow = this.C_DataGrid.CurrentRowIndex;

            if (nRow <= this.conditionTable.Rows.Count - 1 && nRow > 0)
            {
                DataRow dr = this.conditionTable.Rows[nRow];

                this.conditionTable.Rows.Remove(dr);

                this.conditionTable.Rows.InsertAt(dr, nRow - 1);
            }
        }

        //Move down
        private void C_BtnMoveDown_Click(object sender, System.EventArgs e)
        {
            HideCombo();            

            int nRow = this.C_DataGrid.CurrentRowIndex;

            if (nRow < this.conditionTable.Rows.Count - 1 && nRow >= 0)
            {
                DataRow dr = this.conditionTable.Rows[nRow];

                this.conditionTable.Rows.Remove(dr);

                this.conditionTable.Rows.InsertAt(dr, nRow + 1);
            }
        }
     

        //DataGrid Mouse Down
        private void C_DataGrid_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            HideCombo();  

            DataGrid.HitTestInfo Hti = this.C_DataGrid.HitTest(e.X, e.Y);

            if (Hti.Row >= 0)
            {
                if(Hti.Column==0)
                {
                    ShowEditButton(Hti.Row, Hti.Column);
                }
                else if (Hti.Column >= 1)
                {
                    this.BtnEditField.Visible = false;

                    ShowCombo(Hti.Row, Hti.Column);
                }
            }

        }

        //Combo Key Down
        private void C_ComboValue_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                HideCombo();               
            }
        }

        //Hide edit combo
        private void HideCombo()
        {
            if (!this.C_ComboValue.Visible) return;

            this.C_ComboValue.Visible = false;

            this.BtnEditField.Visible = false;
            
        }
     
        //Show edit combo
        private void ShowCombo(int row, int col)
        {
            this.HideCombo();

            this.nSelectedRow = row;

            this.nSelectedCol = col;

            string strValue = string.Empty;

            this.C_ComboValue.Items.Clear();

            Rectangle bounds = this.C_DataGrid.GetCellBounds(row, col);

            object value = this.conditionTable.DefaultView[row][col];

            Type type = this.conditionTable.Columns[col].DataType;

            if (type.BaseType == typeof(Enum))
            {
                strValue = Enum.GetName(this.conditionTable.Columns[col].DataType, value);
            }
            else
            {
                strValue = value.ToString();
            }

            this.C_ComboValue.Sorted = true;

            switch (col)
            {               
                case 1: //ConstantOperator 
                case 3: //FieldOperator
                case 4: //Bracket  
                    {
                        this.C_ComboValue.Sorted = false;

                        foreach (object oper in Enum.GetValues(this.conditionTable.Columns[col].DataType))
                        {
                            this.C_ComboValue.Items.Add(oper);
                        }
                        break;
                    }           
                default:
                    break;
            }

            Rectangle tempRect = this.C_DataGrid.RectangleToScreen(bounds);
           
            this.C_ComboValue.Text = strValue;

            this.C_ComboValue.Bounds = this.RectangleToClient(tempRect);

            this.C_ComboValue.Visible = true;

            this.C_ComboValue.SelectAll();
            
        }

        private void ShowEditButton(int row, int col)
        {
            this.nSelectedRow = row;

            this.nSelectedCol = col;

            string strValue = string.Empty;

            this.C_ComboValue.Items.Clear();

            Rectangle bounds = this.C_DataGrid.GetCellBounds(row, col);

            object value = this.conditionTable.DefaultView[row][col];          
           
            strValue = value.ToString();    

            Rectangle tempRect = this.C_DataGrid.RectangleToScreen(bounds);

            this.BtnEditField.Text = strValue;

            this.BtnEditField.Bounds = this.RectangleToClient(tempRect);            

            this.BtnEditField.Visible = true;        

        }

        private void EditValue()
        {
            if (this.nSelectedRow >= this.conditionTable.Rows.Count
                || this.nSelectedCol >= this.conditionTable.Columns.Count)
            {
                System.Diagnostics.Debug.WriteLine("Cell was not exsit");
                return;
            }

            object value = this.C_ComboValue.Text;

            bool bFind = false;

            Type type = this.conditionTable.Columns[this.nSelectedCol].DataType;

            if (type.BaseType == typeof(Enum))
            {
                foreach (object name in Enum.GetNames(type))
                {
                    if (name.ToString() == this.C_ComboValue.Text)
                    {
                        value = Enum.Parse(type, this.C_ComboValue.Text, true);

                        bFind = true;

                        break;
                    }
                }

                if (!bFind)
                {
                    value = this.conditionTable.Columns[this.nSelectedCol].DefaultValue;
                }
            }
            this.conditionTable.Rows[this.nSelectedRow][this.nSelectedCol] = value;

        }

        //Combo visible change
        private void C_ComboValue_VisibleChanged(object sender, System.EventArgs e)
        {
            if (this.C_ComboValue.Visible == false)
            {
                this.EditValue();
            }
        }

        //Import
        private void C_BtnImport_Click(object sender, System.EventArgs e)
        {
            this.HideCombo();         

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = string.Format("Calculation ExportFile(*{0})|*{0}", CalculateColumnInfo.ExportFileExt);
                if (DialogResult.OK == openFileDialog.ShowDialog())
                {
                    if (System.IO.File.Exists(openFileDialog.FileName))
                    {
                        System.IO.FileStream stream = System.IO.File.Open(openFileDialog.FileName, System.IO.FileMode.Open);

                        try
                        {
                            CalculateColumnInfo calcuateColumnInfo = Webb.Utilities.Serializer.DeserializeObject(stream) as CalculateColumnInfo;

                            if (calcuateColumnInfo != null)
                            {
                                this.value = calcuateColumnInfo.Copy();

                                this.CreateConditionTable();

                                this.chkDisplayClearSteps.Checked = this.value.DisplayedCalculteStep;
                            }
                        }
                        catch (Exception ex)
                        {
                            Webb.Utilities.MessageBoxEx.ShowError("Failed to import filter!\n" + ex.Message);
                        }
                        finally
                        {
                            stream.Close();
                        }
                    }
                }
            }
        }

        //Export
        private void C_BtnExport_Click(object sender, System.EventArgs e)
        {
            this.HideCombo();           

            this.UpdateFilter();

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = string.Format("Calculation ExportFile(*{0})|*{0}", CalculateColumnInfo.ExportFileExt);

                if (DialogResult.OK == saveFileDialog.ShowDialog())
                {
                    System.IO.FileStream stream = System.IO.File.Open(saveFileDialog.FileName, System.IO.FileMode.OpenOrCreate);

                    Webb.Utilities.Serializer.SerializeObject(stream, this.value);

                    stream.Close();
                }
            }
        }
  
   
     
        private void C_DataGrid_Scroll(object sender, EventArgs e)
        {
            HideCombo();   
        }

        private void BtnEditField_Click(object sender, EventArgs e)
        { 
            object value = this.conditionTable.DefaultView[this.nSelectedRow][this.nSelectedCol];

            CalculateFieldTypeForm calucFieldTypeForm = new CalculateFieldTypeForm(value);

            if (calucFieldTypeForm.ShowDialog() == DialogResult.OK)
            {
                this.conditionTable.Rows[this.nSelectedRow][this.nSelectedCol] = calucFieldTypeForm.ObjField;

                this.BtnEditField.Visible = false;
            }
        }
        

    }
    #endregion

    #region public class CalculateColumnEditor : System.Drawing.Design.UITypeEditor
    // This UITypeEditor can be associated with DBFilter
    // properties to provide a design-mode angle selection interface.
    public class CalculateColumnEditor : System.Drawing.Design.UITypeEditor
    {
        public CalculateColumnEditor()
        {

        }

        // Indicates whether the UITypeEditor provides a form-based (modal) dialog, 
        // drop down dialog, or no UI outside of the properties window.
        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        // Indicates whether the UITypeEditor supports painting a 
        // representation of a property's value.
        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        public override bool GetPaintValueSupported(System.ComponentModel.ITypeDescriptorContext context)
        {
            return false;
        }

        // Displays the UI for value selection.
        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {
            // Return the value if the value is not of type Int32, Double and Single.
            if (!(value is CalculateColumnInfo)) return value;

            // Uses the IWindowsFormsEditorService to display a 
            // drop-down UI in the Properties window.
            IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            if (edSvc != null)
            {
                CalculateColumnInfo calcuateColumnInfo = value as CalculateColumnInfo;


                // Display an angle selection control and retrieve the value.
                CalculateColumnEditorForm editForm = new CalculateColumnEditorForm(calcuateColumnInfo);


                if (DialogResult.OK == edSvc.ShowDialog(editForm))
                {
                    // Return the value in the appropraite data format.
                    return editForm.Value;
                }
            }
            return value;
        }

        // Draws a representation of the property's value.
        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        public override void PaintValue(System.Drawing.Design.PaintValueEventArgs e)
        {
            base.PaintValue(e);
        }
    }
    #endregion
}