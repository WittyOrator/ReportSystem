using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using DevExpress.XtraPrinting;
using System.Drawing.Drawing2D;
using System.Globalization;

namespace Webb.Reports.Editors
{
    #region public class FormatEditForm : System.Windows.Forms.Form
    /// <summary>
    /// Summary description for FormatEditForm
    /// </summary>
    public class FormatEditForm : System.Windows.Forms.Form
    {

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private GroupBox groupBox1;
        private Button button1;
        private Button button2;
        private Label lblshow;
        private GroupBox groupBox2;
        private ListBox Lstformat;
        private GroupBox groupBox3;
        private TextBox txbcus;
        private ListBox lsbcus;
        public string formatstring = "";
        private Button button3;
        private string[] dateformatStan;
        public FormatEditForm(string data)
        {
            dateformatStan = new string[]{  "d",      // ShortDatePattern
                                            "D",      // LongDatePattern
                                            "t",      // ShortTimePattern
                                            "T",      // LongTimePattern
                                            "f",      // Long Date With Short Time Pattern
                                            "F",      // FullDateTimePattern
                                            "g",      // Normal（Short Date With Short Time Pattern）
                                            "G",      // Short Date but Long time
                                            "M",      // MonthDayPattern
                                            "R",      // RFC1123Pattern
                                            "s",      //  SortableDateTimePattern（Based on ISO 8601）
                                            "u",      // UniversalSortableDateTimePattern 用于显示通用时间的格式
                                            "U",      //Long Date and Lont Time
                                            "Y",       // YearMonthPattern
                                            "dddd, dd MMMM yyyy h:mm tt"
                                         };


            formatstring = data;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.Lstformat = new System.Windows.Forms.ListBox();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.button3 = new System.Windows.Forms.Button();
			this.txbcus = new System.Windows.Forms.TextBox();
			this.lsbcus = new System.Windows.Forms.ListBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.lblshow = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Location = new System.Drawing.Point(7, 50);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(209, 209);
			this.tabControl1.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.groupBox2);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(201, 183);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Standard";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.Lstformat);
			this.groupBox2.Location = new System.Drawing.Point(4, 5);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(193, 174);
			this.groupBox2.TabIndex = 0;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Choose Format";
			// 
			// Lstformat
			// 
			this.Lstformat.Location = new System.Drawing.Point(5, 18);
			this.Lstformat.Name = "Lstformat";
			this.Lstformat.Size = new System.Drawing.Size(183, 147);
			this.Lstformat.TabIndex = 1;
			this.Lstformat.DoubleClick += new System.EventHandler(this.Lstformat_DoubleClick);
			this.Lstformat.SelectedIndexChanged += new System.EventHandler(this.Lstformat_SelectedIndexChanged);
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.groupBox3);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(201, 183);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Custom";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.button3);
			this.groupBox3.Controls.Add(this.txbcus);
			this.groupBox3.Controls.Add(this.lsbcus);
			this.groupBox3.Location = new System.Drawing.Point(2, 6);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(198, 172);
			this.groupBox3.TabIndex = 0;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Type Or Select";
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(145, 17);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(43, 20);
			this.button3.TabIndex = 4;
			this.button3.Text = "Clear";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// txbcus
			// 
			this.txbcus.Location = new System.Drawing.Point(7, 18);
			this.txbcus.Name = "txbcus";
			this.txbcus.Size = new System.Drawing.Size(138, 20);
			this.txbcus.TabIndex = 3;
			this.txbcus.Text = "";
			this.txbcus.TextChanged += new System.EventHandler(this.txbcus_TextChanged);
			// 
			// lsbcus
			// 
			this.lsbcus.ItemHeight = 12;
			this.lsbcus.Items.AddRange(new object[] {
														"y",
														"yy",
														"yyyy",
														"M",
														"MM",
														"MMM",
														"MMMM",
														"d",
														"dd",
														"ddd",
														"dddd",
														"/",
														"h",
														"hh",
														"H",
														"HH",
														"m",
														"mm",
														"s",
														"ss",
														"f",
														"ff",
														"fff",
														"ffff",
														":",
														"t",
														"tt",
														"z",
														"zz",
														"zzz"});
			this.lsbcus.Location = new System.Drawing.Point(6, 40);
			this.lsbcus.Name = "lsbcus";
			this.lsbcus.Size = new System.Drawing.Size(182, 115);
			this.lsbcus.TabIndex = 2;
			this.lsbcus.SelectedIndexChanged += new System.EventHandler(this.lsbcus_SelectedIndexChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.lblshow);
			this.groupBox1.Location = new System.Drawing.Point(7, 6);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(209, 39);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Sample";
			// 
			// lblshow
			// 
			this.lblshow.AutoSize = true;
			this.lblshow.Location = new System.Drawing.Point(10, 15);
			this.lblshow.Name = "lblshow";
			this.lblshow.Size = new System.Drawing.Size(0, 16);
			this.lblshow.TabIndex = 0;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(30, 262);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(57, 24);
			this.button1.TabIndex = 2;
			this.button1.Text = "OK";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(136, 262);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(56, 24);
			this.button2.TabIndex = 3;
			this.button2.Text = "Cancel";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// FormatEditForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(225, 291);
			this.ControlBox = false;
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "FormatEditForm";
			this.Text = "Format DateTime Editor";
			this.Load += new System.EventHandler(this.FormatEditForm_Load);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void FormatEditForm_Load(object sender, EventArgs e)
        {
            int index = -1, formatindex = -1;
            this.Lstformat.Items.Clear();
            foreach (string sdata in dateformatStan)
            {
                index++;
				string text=DateTime.Now.ToString(sdata, DateTimeFormatInfo.InvariantInfo);
				Lstformat.Items.Add(text);
                if (sdata.Equals(formatstring)) formatindex = index;
            }
            if (formatstring != "")
            {
                if (formatindex >= 0)
                {
                    this.tabControl1.SelectedIndex = 0;
                    Lstformat.SelectedIndex = formatindex;
                }
                else
                {
                    this.tabControl1.SelectedIndex = 1;
                    this.txbcus.Text = formatstring;
                }

            }

        }


        private void Lstformat_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = this.Lstformat.SelectedIndex;
            if (index != -1)
            {
                this.lblshow.Text = this.Lstformat.Items[index].ToString();
                this.formatstring = dateformatStan[index];
            }
        }


        private void lsbcus_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = this.lsbcus.SelectedIndex;
            if (index != -1)
            {
                this.txbcus.SelectedText = this.lsbcus.Items[index].ToString();

            }

        }

        private void txbcus_TextChanged(object sender, EventArgs e)
        {
            string Textshow = this.txbcus.Text.Trim();
            if (Textshow.Length == 0)
            {
                this.lblshow.Text = DateTime.Now.ToString("F", DateTimeFormatInfo.InvariantInfo);
                formatstring = "";
                return;
            }
            try
            {
				 formatstring = Textshow;
                this.lblshow.Text =DateTime.Now.ToString(formatstring, DateTimeFormatInfo.InvariantInfo);

            }
            catch
            {
                this.lblshow.Text = "Bad Format!";
                this.formatstring = "";
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void Lstformat_DoubleClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.txbcus.Text = "";
            this.formatstring = "";
        }


    }
    #endregion

    #region class FormatDateTimeEditor
    public class FormatDateTimeEditor : System.Drawing.Design.UITypeEditor
    {
        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        // Displays the UI for value selection.
        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {

            if (!(value is string))
                return value;

            IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

            FormatEditForm Formatform = new FormatEditForm((string)value);
            if (edSvc != null)
            {
                if (edSvc.ShowDialog(Formatform) == DialogResult.OK)
                {
                    return Formatform.formatstring;
                }
            }
            return value;
        }


        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        public override void PaintValue(System.Drawing.Design.PaintValueEventArgs e)
        {
        }


        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        public override bool GetPaintValueSupported(System.ComponentModel.ITypeDescriptorContext context)
        {
            return false;
        }
    }
    #endregion
}
