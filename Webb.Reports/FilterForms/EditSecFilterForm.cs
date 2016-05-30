using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Data;
using Webb.Data;
using Webb.Reports.DataProvider;

namespace Webb.Reports
{
    #region public class SectionFilterForm : System.Windows.Forms.Form
    /// <summary>
    /// Summary description for FilterEditForm.
    /// </summary>
    public class EditSecFilterForm : System.Windows.Forms.Form
    {
        private DataTable conditionTable;
        private SectionFilter value;
        private int nSelectedRow;
        private int nSelectedCol;
        private string strTableName = "Condition Table";
        Webb.Reports.DataProvider.UI.AdvReportFilterSelector selector;

        private System.Windows.Forms.Button C_BtnOK;
        private System.Windows.Forms.Button C_BtnCancel;
        private System.Windows.Forms.Button C_BtnAddCondition;
        private System.Windows.Forms.Button C_BtnRemoveCondition;
        private System.Windows.Forms.CheckBox C_CheckPlayAfter;
        private System.Windows.Forms.ComboBox C_ComboValue;
        private System.Windows.Forms.DataGrid C_DataGrid;
        private System.Windows.Forms.Button C_BtnExport;
        private System.Windows.Forms.Button C_BtnImport;
        private System.Windows.Forms.Button C_BtnExportOld;
        private System.Windows.Forms.Button BtnClear;
        private System.Windows.Forms.Label label1;
        private TextBox txtFilterName;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        private bool _NoEdit=false;
        private DateTimePicker dateTimePicker1;

        private WebbDBTypes _WebbDBTypes = WebbDBTypes.WebbAdvantageFootball;

        public SectionFilter Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public bool NoEdit
        {
            set
            {
                if (value)
                {
                    this.C_BtnAddCondition.Enabled = false;
                    this.C_BtnRemoveCondition.Enabled = false;
                    this.BtnClear.Enabled = false;
                    this.C_BtnExportOld.Visible = false;
                    this.C_BtnImport.Enabled = false;

                }
                else
                {
                    this.C_BtnAddCondition.Enabled = true;
                    this.C_BtnImport.Enabled = true;
                    this.C_BtnExportOld.Visible = true;
                    this.C_BtnRemoveCondition.Enabled = true;
                    this.BtnClear.Enabled = true;
                }
            }
        }




        public EditSecFilterForm(SectionFilter scFilter, WebbDBTypes webbDBTypes)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            this._WebbDBTypes = webbDBTypes;

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            //Create table style
            DataGridTableStyle style = new DataGridTableStyle();

            style.MappingName = strTableName;

            this.C_DataGrid.TableStyles.Add(style);
            //
            this.Value =new SectionFilter();

            this.value.Apply(scFilter);
         
            this.CreateConditionTable();

            this.C_CheckPlayAfter.Checked = this.value.Filter.PlayAfter;

            this.txtFilterName.Text = this.value.FilterName;   //2009-5-19 10:15:19@Simon Add this Code
            
            //
            selector = new Webb.Reports.DataProvider.UI.AdvReportFilterSelector();

            selector.Visible = false;

            selector.Dock = DockStyle.Fill;

            this.Controls.Add(selector);

            selector.BringToFront();

            selector.VisibleChanged += new EventHandler(selector_VisibleChanged);
        }

        public EditSecFilterForm(WebbDBTypes webbDBTypes)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            this._WebbDBTypes = webbDBTypes;

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            //Create table style
            DataGridTableStyle style = new DataGridTableStyle();

            style.MappingName = strTableName;

            this.C_DataGrid.TableStyles.Add(style);
            //
            this.Value = new SectionFilter();

          
            this.CreateConditionTable();

            this.C_CheckPlayAfter.Checked = false;

            this.txtFilterName.Text = string.Empty;   //2009-5-19 10:15:19@Simon Add this Code

            //
            selector = new Webb.Reports.DataProvider.UI.AdvReportFilterSelector();

            selector.Visible = false;

            selector.Dock = DockStyle.Fill;

            this.Controls.Add(selector);

            selector.BringToFront();

            selector.VisibleChanged += new EventHandler(selector_VisibleChanged);

            _NoEdit = false;
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
            if (this.txtFilterName.Text.Trim()==string.Empty)
            {
                Webb.Utilities.MessageBoxEx.ShowWarning("'FilterName' should not be empty,\nplease input the filter name first!");

                this.txtFilterName.Focus();

                return;
            }
            this.value.FilterName = this.txtFilterName.Text;

            this.value.Filter.Name = this.txtFilterName.Text;

            this.HideCombo();

            HideDateTimePicker();

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
            this.HideCombo();

            HideDateTimePicker();

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
            this.C_CheckPlayAfter = new System.Windows.Forms.CheckBox();
            this.C_ComboValue = new System.Windows.Forms.ComboBox();
            this.C_DataGrid = new System.Windows.Forms.DataGrid();
            this.C_BtnExport = new System.Windows.Forms.Button();
            this.C_BtnImport = new System.Windows.Forms.Button();
            this.C_BtnExportOld = new System.Windows.Forms.Button();
            this.BtnClear = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFilterName = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.C_DataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // C_BtnOK
            // 
            this.C_BtnOK.Location = new System.Drawing.Point(459, 277);
            this.C_BtnOK.Name = "C_BtnOK";
            this.C_BtnOK.Size = new System.Drawing.Size(76, 23);
            this.C_BtnOK.TabIndex = 1;
            this.C_BtnOK.Text = "OK";
            this.C_BtnOK.Click += new System.EventHandler(this.C_BtnOK_Click);
            // 
            // C_BtnCancel
            // 
            this.C_BtnCancel.Location = new System.Drawing.Point(555, 277);
            this.C_BtnCancel.Name = "C_BtnCancel";
            this.C_BtnCancel.Size = new System.Drawing.Size(75, 23);
            this.C_BtnCancel.TabIndex = 2;
            this.C_BtnCancel.Text = "Cancel";
            this.C_BtnCancel.Click += new System.EventHandler(this.C_BtnCancel_Click);
            // 
            // C_BtnAddCondition
            // 
            this.C_BtnAddCondition.Location = new System.Drawing.Point(19, 39);
            this.C_BtnAddCondition.Name = "C_BtnAddCondition";
            this.C_BtnAddCondition.Size = new System.Drawing.Size(77, 23);
            this.C_BtnAddCondition.TabIndex = 3;
            this.C_BtnAddCondition.Text = "Add Row";
            this.C_BtnAddCondition.Click += new System.EventHandler(this.C_BtnAddCondition_Click);
            // 
            // C_BtnRemoveCondition
            // 
            this.C_BtnRemoveCondition.Location = new System.Drawing.Point(118, 39);
            this.C_BtnRemoveCondition.Name = "C_BtnRemoveCondition";
            this.C_BtnRemoveCondition.Size = new System.Drawing.Size(103, 23);
            this.C_BtnRemoveCondition.TabIndex = 4;
            this.C_BtnRemoveCondition.Text = "Remove Row";
            this.C_BtnRemoveCondition.Click += new System.EventHandler(this.C_BtnRemoveCondition_Click);
            // 
            // C_CheckPlayAfter
            // 
            this.C_CheckPlayAfter.Location = new System.Drawing.Point(525, 39);
            this.C_CheckPlayAfter.Name = "C_CheckPlayAfter";
            this.C_CheckPlayAfter.Size = new System.Drawing.Size(85, 24);
            this.C_CheckPlayAfter.TabIndex = 7;
            this.C_CheckPlayAfter.Text = "Play after";
            this.C_CheckPlayAfter.CheckedChanged += new System.EventHandler(this.C_CheckPlayAfter_CheckedChanged);
            // 
            // C_ComboValue
            // 
            this.C_ComboValue.DropDownHeight = 350;
            this.C_ComboValue.IntegralHeight = false;
            this.C_ComboValue.Items.AddRange(new object[] {
            "1",
            "2",
            "3"});
            this.C_ComboValue.Location = new System.Drawing.Point(558, 213);
            this.C_ComboValue.Name = "C_ComboValue";
            this.C_ComboValue.Size = new System.Drawing.Size(120, 20);
            this.C_ComboValue.Sorted = true;
            this.C_ComboValue.TabIndex = 8;
            this.C_ComboValue.Visible = false;
            this.C_ComboValue.VisibleChanged += new System.EventHandler(this.C_ComboValue_VisibleChanged);
            this.C_ComboValue.DropDownClosed += new System.EventHandler(this.C_ComboValue_DropDownClosed);
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
            this.C_DataGrid.Location = new System.Drawing.Point(19, 73);
            this.C_DataGrid.Name = "C_DataGrid";
            this.C_DataGrid.ParentRowsBackColor = System.Drawing.Color.Lavender;
            this.C_DataGrid.ParentRowsForeColor = System.Drawing.Color.MidnightBlue;
            this.C_DataGrid.ReadOnly = true;
            this.C_DataGrid.SelectionBackColor = System.Drawing.Color.Teal;
            this.C_DataGrid.SelectionForeColor = System.Drawing.Color.PaleGreen;
            this.C_DataGrid.Size = new System.Drawing.Size(610, 195);
            this.C_DataGrid.TabIndex = 9;
            this.C_DataGrid.Scroll += new System.EventHandler(this.C_DataGrid_Scroll);
            this.C_DataGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.C_DataGrid_MouseDown);
            // 
            // C_BtnExport
            // 
            this.C_BtnExport.Location = new System.Drawing.Point(108, 277);
            this.C_BtnExport.Name = "C_BtnExport";
            this.C_BtnExport.Size = new System.Drawing.Size(76, 23);
            this.C_BtnExport.TabIndex = 10;
            this.C_BtnExport.Text = "Export";
            this.C_BtnExport.Click += new System.EventHandler(this.C_BtnExport_Click);
            // 
            // C_BtnImport
            // 
            this.C_BtnImport.Location = new System.Drawing.Point(22, 277);
            this.C_BtnImport.Name = "C_BtnImport";
            this.C_BtnImport.Size = new System.Drawing.Size(74, 23);
            this.C_BtnImport.TabIndex = 11;
            this.C_BtnImport.Text = "Import";
            this.C_BtnImport.Click += new System.EventHandler(this.C_BtnImport_Click);
            // 
            // C_BtnExportOld
            // 
            this.C_BtnExportOld.Location = new System.Drawing.Point(195, 277);
            this.C_BtnExportOld.Name = "C_BtnExportOld";
            this.C_BtnExportOld.Size = new System.Drawing.Size(127, 23);
            this.C_BtnExportOld.TabIndex = 12;
            this.C_BtnExportOld.Text = "Import AdvFilter";
            this.C_BtnExportOld.Click += new System.EventHandler(this.C_BtnExportOld_Click);
            // 
            // BtnClear
            // 
            this.BtnClear.Location = new System.Drawing.Point(258, 39);
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.Size = new System.Drawing.Size(103, 23);
            this.BtnClear.TabIndex = 13;
            this.BtnClear.Text = "Clear Filter";
            this.BtnClear.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 17);
            this.label1.TabIndex = 18;
            this.label1.Text = "FilterName:";
            // 
            // txtFilterName
            // 
            this.txtFilterName.Location = new System.Drawing.Point(129, 7);
            this.txtFilterName.Name = "txtFilterName";
            this.txtFilterName.Size = new System.Drawing.Size(500, 21);
            this.txtFilterName.TabIndex = 20;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(410, 247);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 21);
            this.dateTimePicker1.TabIndex = 21;
            this.dateTimePicker1.Visible = false;
            this.dateTimePicker1.VisibleChanged += new System.EventHandler(this.dateTimePicker1_VisibleChanged);
            // 
            // EditSecFilterForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(641, 309);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.txtFilterName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnClear);
            this.Controls.Add(this.C_BtnExportOld);
            this.Controls.Add(this.C_BtnImport);
            this.Controls.Add(this.C_BtnExport);
            this.Controls.Add(this.C_ComboValue);
            this.Controls.Add(this.C_DataGrid);
            this.Controls.Add(this.C_CheckPlayAfter);
            this.Controls.Add(this.C_BtnRemoveCondition);
            this.Controls.Add(this.C_BtnAddCondition);
            this.Controls.Add(this.C_BtnCancel);
            this.Controls.Add(this.C_BtnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "EditSecFilterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Section Filter";
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
            this.C_DataGrid.TableStyles[strTableName].GridColumnStyles["BracketS"].Width = 40;
            this.C_DataGrid.TableStyles[strTableName].GridColumnStyles["BracketS"].Alignment = HorizontalAlignment.Center;
            this.C_DataGrid.TableStyles[strTableName].GridColumnStyles["Field"].Width = 140;
            this.C_DataGrid.TableStyles[strTableName].GridColumnStyles["Operator"].Width = 120;
            this.C_DataGrid.TableStyles[strTableName].GridColumnStyles["Value"].Width = 120;
            this.C_DataGrid.TableStyles[strTableName].GridColumnStyles["Operand"].Width = 50;
            this.C_DataGrid.TableStyles[strTableName].GridColumnStyles["BracketE"].Width = 40;
            this.C_DataGrid.TableStyles[strTableName].GridColumnStyles["BracketE"].Alignment = HorizontalAlignment.Center;

            this.C_DataGrid.TableStyles[strTableName].AllowSorting = false;
        }

        private void CreateConditionTableStructure()
        {
            conditionTable = new DataTable(strTableName);


            //Left Bracket
            DataColumn colStartBracket = new DataColumn("BracketS", typeof(string));

            colStartBracket.DefaultValue = string.Empty;

            //field column
            DataColumn colField = new DataColumn("Field", typeof(string));

            colField.DefaultValue = string.Empty;

            //operator column
            DataColumn colOperator = new DataColumn("Operator", typeof(Webb.Data.FilterTypes));

            colOperator.DefaultValue = Webb.Data.FilterTypes.StrStartWith;

            //value column
            DataColumn colValue = new DataColumn("Value", typeof(object));

            colValue.DefaultValue = string.Empty;

            DataColumn colEndBracket = new DataColumn("BracketE", typeof(string));

            colEndBracket.DefaultValue = string.Empty;

            //operand column
            DataColumn colOperand = new DataColumn("Operand", typeof(Webb.Data.FilterOperands));

            colOperand.DefaultValue = Webb.Data.FilterOperands.Or;


            //add columns
            conditionTable.Columns.Add(colStartBracket);
            conditionTable.Columns.Add(colField);
            conditionTable.Columns.Add(colOperator);
            conditionTable.Columns.Add(colValue);
            conditionTable.Columns.Add(colEndBracket);
            conditionTable.Columns.Add(colOperand);
        }

        private void FillConditionTable()
        {
            if (this.Value == null) return;

            foreach (DBCondition condition in this.Value.Filter)
            {
                DataRow row = this.conditionTable.NewRow();

                row["Field"] = condition.ColumnName;
                row["Operator"] = condition.FilterType;
                if (condition.Value == null)
                {
                    row["Value"] = string.Empty;
                }
                else if (condition.Value.GetType() == typeof(DateTime))
                {
                    row["Value"] = Convert.ToDateTime(condition.Value).ToString("M/dd/yyyy");
                }
                else
                {
                    row["Value"] = condition.Value;
                }
                row["Operand"] = condition.FollowedOperand;

                if (condition.Bracket == Bracket.Start)
                {
                    row["BracketS"] = "(";
                    row["BracketE"] = "";
                }
                else if (condition.Bracket == Bracket.End)
                {
                    row["BracketS"] = "";
                    row["BracketE"] = ")";
                }
                else
                {
                    row["BracketS"] = "";
                    row["BracketE"] = "";
                }

                this.conditionTable.Rows.Add(row);
            }
        }

        private bool CheckBracketAreCompared()
        {
            Stack BracketStack = new Stack();
            foreach (DBCondition condition in value.Filter)
            {
                if (condition.Bracket == Bracket.Start)
                {
                    BracketStack.Push("S");
                }
                else if (condition.Bracket == Bracket.End)
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

            foreach (DBCondition condition in value.Filter)
            {
                if (condition.Bracket != Bracket.NONE)
                {
                    return false;
                }
            }
            return true;

        }
        private void UpdateFilter()
        {
            this.value.Filter.Clear();

            DataSet ds = Webb.Reports.DataProvider.VideoPlayBackManager.DataSource; 
       
            foreach (DataRowView row in this.conditionTable.DefaultView)
            {
                DBCondition condition = new DBCondition();

                condition.ColumnName = row["Field"].ToString();
                condition.FilterType = (Webb.Data.FilterTypes)row["Operator"];

                #region Date Time
                bool isDateTimeData = false;

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Columns.Contains(condition.ColumnName) && ds.Tables[0].Columns[condition.ColumnName].DataType == typeof(DateTime))
                    {
                        isDateTimeData = true;
                    }
                }
                if (isDateTimeData)
                {
                    try
                    {
                        condition.Value = Convert.ToDateTime(row["Value"]);
                    }
                    catch
                    {
                        condition.Value = row["Value"];
                    }
                }
                else
                {
                    condition.Value = row["Value"];
                }
                #endregion

                condition.FollowedOperand = (Webb.Data.FilterOperands)row["Operand"];

                string startBracket = row["BracketS"].ToString().Trim();
                string endBracket = row["BracketE"].ToString().Trim();
                if (startBracket == "(")
                {
                    if (endBracket == ")")
                    {
                        condition.Bracket = Webb.Data.Bracket.NONE;
                    }
                    else
                    {
                        condition.Bracket = Webb.Data.Bracket.Start;
                    }
                }
                else
                {
                    if (endBracket== ")")
                    {
                        condition.Bracket = Webb.Data.Bracket.End;
                    }
                    else
                    {
                        condition.Bracket = Webb.Data.Bracket.NONE;
                    }
                }               

                this.value.Filter.Add(condition);
            }

            this.value.FilterName = this.txtFilterName.Text;

            this.value.Filter.PlayAfter = this.C_CheckPlayAfter.Checked;
        }

        //Add condition
        private void C_BtnAddCondition_Click(object sender, System.EventArgs e)
        {
            DataRow row = this.conditionTable.NewRow();

            this.conditionTable.Rows.Add(row);

            HideCombo();

            HideDateTimePicker();
        }

        //Remove condition
        private void C_BtnRemoveCondition_Click(object sender, System.EventArgs e)
        {          
            
            if (this.C_DataGrid.CurrentRowIndex >= 0 && this.C_DataGrid.CurrentRowIndex < this.conditionTable.Rows.Count)
            {
                this.conditionTable.DefaultView.Delete(this.C_DataGrid.CurrentRowIndex);
            }


            HideCombo();

            HideDateTimePicker();
        }

        //Move up
        private void C_BtnMoveUp_Click(object sender, System.EventArgs e)
        {
            HideCombo();

            HideDateTimePicker();

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

            HideDateTimePicker();

            int nRow = this.C_DataGrid.CurrentRowIndex;

            if (nRow < this.conditionTable.Rows.Count - 1 && nRow >= 0)
            {
                DataRow dr = this.conditionTable.Rows[nRow];

                this.conditionTable.Rows.Remove(dr);

                this.conditionTable.Rows.InsertAt(dr, nRow + 1);
            }
        }

        //Play after
        private void C_CheckPlayAfter_CheckedChanged(object sender, System.EventArgs e)
        {
            HideCombo();

            HideDateTimePicker();
        }

        //DataGrid Mouse Down
        private void C_DataGrid_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            HideCombo();

            HideDateTimePicker();

            if (this._NoEdit) return;

            DataGrid.HitTestInfo Hti = this.C_DataGrid.HitTest(e.X, e.Y);

            #region Allow sorting

            //if (Hti.Type.ToString().Trim() == "ColumnHeader")
            //{
            //    this.UpdateFilter();

            //    bool IsCheck = this.CheckAllBracketIsNone();

            //    if (IsCheck)
            //    {
            //        this.C_DataGrid.AllowSorting = true;
            //    }
            //    else
            //    {
            //        this.C_DataGrid.AllowSorting = false;
            //        //you   can   add   other   code   if   you   want 
            //        Webb.Utilities.MessageBoxEx.ShowError("Can not sort rows when filter have brackets!");
            //    }

            //    return;
            //}
            #endregion


            if (Hti.Row >= 0 && Hti.Column >= 0)
            {
                ShowCombo(Hti.Row, Hti.Column);
            }

        }

        //Combo Key Down
        private void C_ComboValue_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                HideCombo();

                HideDateTimePicker();
            }
        }

        //Hide edit combo
        private void HideCombo()
        {
            if (!this.C_ComboValue.Visible) return;

            this.C_ComboValue.Visible = false;

            this.C_CheckPlayAfter.Focus();
        }

        //Show edit combo
        private void ShowCombo(int row, int col)
        {
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

            bool isColumnDateTimeType = false;

            DataSet ds = Webb.Reports.DataProvider.VideoPlayBackManager.DataSource;

            string strTableField = this.conditionTable.Columns[col].ColumnName;

            switch (strTableField)
            {
                case "Field":	//field
                    {
                        foreach (object field in PublicDBFieldConverter.AvialableFields)
                        {
                            this.C_ComboValue.Items.Add(field);
                        }
                        break;
                    }
                case "Operator":
                    {
                        #region Deal DateTime Data
                        bool isDateTimeData = false;

                        if (ds != null && ds.Tables.Count > 0 && row < this.conditionTable.Rows.Count && row >= 0)
                        {
                            string columnName = this.conditionTable.Rows[row]["Field"].ToString();

                            if (ds.Tables[0].Columns.Contains(columnName) && ds.Tables[0].Columns[columnName].DataType == typeof(DateTime))
                            {
                                isDateTimeData = true;

                                if (!strValue.StartsWith("Num")) strValue = "NumLessOrEqual";
                            }
                        }
                        this.C_ComboValue.Sorted = false;

                        foreach (object oper in Enum.GetValues(this.conditionTable.Columns[col].DataType))
                        {
                            if (isDateTimeData && !oper.ToString().StartsWith("Num")) continue;

                            this.C_ComboValue.Items.Add(oper);
                        }
                        #endregion
                        break;
                    }
                case "Operand": //Operand                
                    {
                        this.C_ComboValue.Sorted = false;

                        foreach (object oper in Enum.GetValues(this.conditionTable.Columns[col].DataType))
                        {
                            this.C_ComboValue.Items.Add(oper);
                        }
                        break;
                    }
                case "BracketS":
                    {
                        this.C_ComboValue.Items.Add("(");
                        this.C_ComboValue.Items.Add("");
                        break;
                    }
                case "BracketE":
                    {
                        this.C_ComboValue.Items.Add("");
                        this.C_ComboValue.Items.Add(")");
                        break;
                    }
                case "Value":  //value
                    {
                        #region Value

                        if (ds != null && ds.Tables.Count > 0 && row < this.conditionTable.Rows.Count && row >= 0)
                        {
                            string columnName = this.conditionTable.Rows[row]["Field"].ToString();

                            if (ds.Tables[0].Columns.Contains(columnName))
                            {
                                if (ds.Tables[0].Columns[columnName].DataType == typeof(DateTime))
                                {
                                    #region Deal DateTimeme Data

                                    if (value == null || value.GetType() != typeof(DateTime))
                                    {
                                        this.dateTimePicker1.Value = DateTime.Now;
                                    }
                                    else
                                    {
                                        this.dateTimePicker1.Value = Convert.ToDateTime(value);
                                    }

                                    isColumnDateTimeType = true;
                                    #endregion
                                }
                                else
                                {
                                    #region Other Data Type

                                    foreach (DataRow dr in ds.Tables[0].Rows)
                                    {
                                        object enumValue = dr[columnName];

                                        if (!C_ComboValue.Items.Contains(enumValue))
                                        {
                                            this.C_ComboValue.Items.Add(enumValue);
                                        }
                                    }
                                    #endregion
                                }
                            }
                        }
                        break;
                        #endregion
                    }
            }
            Rectangle tempRect = this.C_DataGrid.RectangleToScreen(bounds);

            if (isColumnDateTimeType)
            {
                this.dateTimePicker1.Bounds = this.RectangleToClient(tempRect);

                this.dateTimePicker1.Visible = true;

            }
            else
            {
                this.C_ComboValue.Text = strValue;

                this.C_ComboValue.Bounds = this.RectangleToClient(tempRect);

                this.C_ComboValue.Visible = true;

                this.C_ComboValue.SelectAll();
            }
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
            else if (this.nSelectedCol >= 0 && this.nSelectedCol < this.conditionTable.Columns.Count)
            {
                string strTableColumnName = this.conditionTable.Columns[this.nSelectedCol].ColumnName;

                if (strTableColumnName == "BracketS" || strTableColumnName == "BracketE")
                {
                    if (!this.C_ComboValue.Items.Contains(value))
                    {
                        Webb.Utilities.MessageBoxEx.ShowError("Invalid Bracket Symbol!");
                        return;
                    }
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
        private void HideDateTimePicker()
        {
            if (!this.dateTimePicker1.Visible) return;

            this.dateTimePicker1.Visible = false;

            this.C_CheckPlayAfter.Focus();
        }


        //Import
        private void C_BtnImport_Click(object sender, System.EventArgs e)
        {
            this.HideCombo();

            HideDateTimePicker();

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if (DialogResult.OK == openFileDialog.ShowDialog())
                {
                    if (System.IO.File.Exists(openFileDialog.FileName))
                    {
                        System.IO.FileStream stream = System.IO.File.Open(openFileDialog.FileName, System.IO.FileMode.Open);

                        try
                        {
                            DBFilter filter = Webb.Utilities.Serializer.DeserializeObject(stream) as DBFilter;

                            if (filter != null)
                            {
                                this.value.Filter = filter.Copy();

                                this.CreateConditionTable();

                                this.C_CheckPlayAfter.Checked = this.value.Filter.PlayAfter;

                                this.txtFilterName.Text = this.value.Filter.Name;

                                this.value.FilterName = this.value.Filter.Name;
                            }
                        }
                        catch { }
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

            HideDateTimePicker();

            this.UpdateFilter();

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                if (DialogResult.OK == saveFileDialog.ShowDialog())
                {
                    System.IO.FileStream stream = System.IO.File.Open(saveFileDialog.FileName, System.IO.FileMode.OpenOrCreate);

                    Webb.Utilities.Serializer.SerializeObject(stream, this.value.Filter);

                    stream.Close();
                }
            }
        }

        private void C_BtnExportOld_Click(object sender, System.EventArgs e)
        {
            this.HideCombo();

            HideDateTimePicker();

            if (selector.Update())
            {
                selector.Visible = true;
            }
        }

        private void selector_VisibleChanged(object sender, EventArgs e)
        {
            if (selector.Visible == false && this.selector.SectionFilter != null)
            {
                this.value.Filter= this.selector.SectionFilter.Filter.Copy();

                this.CreateConditionTable();

                this.C_CheckPlayAfter.Checked = this.value.Filter.PlayAfter;

                this.txtFilterName.Text = this.value.Filter.Name;
            }
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            HideCombo();

            HideDateTimePicker();

            this.value.Filter= new DBFilter();

            value.Filter.IsCustomFilter = true;

            this.CreateConditionTable();

            this.C_CheckPlayAfter.Checked = this.value.Filter.PlayAfter;
           
        }

        private void C_DataGrid_Scroll(object sender, EventArgs e)
        {
            HideCombo();

            HideDateTimePicker();
        }

        private void C_ComboValue_DropDownClosed(object sender, EventArgs e)
        {
            DataSet ds = Webb.Reports.DataProvider.VideoPlayBackManager.DataSource;

            if (ds == null || nSelectedCol !=1||this.C_ComboValue.SelectedItem==null) return;

            if (ds != null && ds.Tables.Count > 0 && this.nSelectedRow < this.conditionTable.Rows.Count && this.nSelectedRow >= 0)
            {
                string columnName = this.C_ComboValue.SelectedItem.ToString();

                if (!ds.Tables[0].Columns.Contains(columnName)) return;

                if (ds.Tables[0].Columns[columnName].DataType == typeof(DateTime))
                {
                    this.conditionTable.Rows[this.nSelectedRow][this.nSelectedCol + 1] = FilterTypes.NumLessOrEqual;
                }
                else
                {
                    this.conditionTable.Rows[this.nSelectedRow][this.nSelectedCol + 1] = FilterTypes.StrStartWith;
                }
            }
        }

        private void dateTimePicker1_VisibleChanged(object sender, EventArgs e)
        {
            if (this.dateTimePicker1.Visible ==true) return;

            if (this.conditionTable == null) return;

            if (this.nSelectedRow >= this.conditionTable.Rows.Count
                || this.nSelectedCol >= this.conditionTable.Columns.Count)
            {
                System.Diagnostics.Debug.WriteLine("Cell was not exsit");
                return;
            }

            this.conditionTable.Rows[this.nSelectedRow][this.nSelectedCol] = dateTimePicker1.Value.ToString("M/dd/yyyy");
        }
    
    }
    #endregion
}
