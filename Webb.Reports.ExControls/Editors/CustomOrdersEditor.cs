using System;
using System.Data;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using DevExpress.XtraPrinting;

using System.Collections;
using System.Collections.Specialized;
using Webb.Reports.ExControls.Data;



namespace Webb.Reports.ExControls.Editors
{
	#region public class CustomOrdersEditorForm : System.Windows.Forms.Form	
	public class CustomOrdersEditorForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button BtnOk;
		private System.Windows.Forms.Button BtnCancel;
		private System.Windows.Forms.Button BtnUp;
        private System.Windows.Forms.Button BtnDown;
		private System.Windows.Forms.Button BtnLoad;
		private System.Windows.Forms.ListBox LstValue;
		public UserOrderClS UserOrderCls;

        private int indexoftarget, indexofsource;

        private Type dataType=null;

        DataTable backDataTable = null;

		private void InitializeComponent()
		{
            this.BtnOk = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.LstValue = new System.Windows.Forms.ListBox();
            this.BtnUp = new System.Windows.Forms.Button();
            this.BtnDown = new System.Windows.Forms.Button();
            this.BtnLoad = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BtnOk
            // 
            this.BtnOk.Location = new System.Drawing.Point(337, 395);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(64, 29);
            this.BtnOk.TabIndex = 0;
            this.BtnOk.Text = "OK";
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Location = new System.Drawing.Point(412, 395);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(62, 30);
            this.BtnCancel.TabIndex = 1;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // LstValue
            // 
            this.LstValue.AllowDrop = true;
            this.LstValue.ItemHeight = 12;
            this.LstValue.Location = new System.Drawing.Point(3, 37);
            this.LstValue.Name = "LstValue";
            this.LstValue.Size = new System.Drawing.Size(485, 352);
            this.LstValue.TabIndex = 2;
            this.LstValue.DragOver += new System.Windows.Forms.DragEventHandler(this.listBox2_DragOver);
            this.LstValue.DragDrop += new System.Windows.Forms.DragEventHandler(this.listBox2_DragDrop);
            this.LstValue.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listBox2_MouseDown);
            // 
            // BtnUp
            // 
            this.BtnUp.Location = new System.Drawing.Point(12, 394);
            this.BtnUp.Name = "BtnUp";
            this.BtnUp.Size = new System.Drawing.Size(97, 29);
            this.BtnUp.TabIndex = 3;
            this.BtnUp.Text = "Move Up";
            this.BtnUp.Click += new System.EventHandler(this.BtnUp_Click);
            // 
            // BtnDown
            // 
            this.BtnDown.Location = new System.Drawing.Point(141, 394);
            this.BtnDown.Name = "BtnDown";
            this.BtnDown.Size = new System.Drawing.Size(87, 31);
            this.BtnDown.TabIndex = 4;
            this.BtnDown.Text = "Move Down";
            this.BtnDown.Click += new System.EventHandler(this.BtnDown_Click);
            // 
            // BtnLoad
            // 
            this.BtnLoad.Font = new System.Drawing.Font("Tahoma", 10F);
            this.BtnLoad.Location = new System.Drawing.Point(2, 2);
            this.BtnLoad.Name = "BtnLoad";
            this.BtnLoad.Size = new System.Drawing.Size(204, 29);
            this.BtnLoad.TabIndex = 6;
            this.BtnLoad.Text = "Reset order from datasource ";
            this.BtnLoad.Click += new System.EventHandler(this.BtnLoad_Click);
            // 
            // CustomOrdersEditorForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(489, 429);
            this.ControlBox = false;
            this.Controls.Add(this.BtnLoad);
            this.Controls.Add(this.BtnDown);
            this.Controls.Add(this.BtnUp);
            this.Controls.Add(this.LstValue);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnOk);
            this.Name = "CustomOrdersEditorForm";
            this.Text = "Orders Editor";
            this.ResumeLayout(false);

		}
	
		public CustomOrdersEditorForm(object value)
		{ 
			InitializeComponent();
            UserOrderCls = value as UserOrderClS;
            this.SetView();
             
		}
		private void SetView()
		{
			this.LstValue.Items.Clear();

            foreach (object objValue in UserOrderCls.OrderValues)
			{
                this.LstValue.Items.Add(objValue);
			}

            DataSet ds=Webb.Reports.DataProvider.VideoPlayBackManager.DataSource;            

            if (ds != null && ds.Tables.Count > 0)
            {
                backDataTable = ds.Tables[0];                    
           
                if(UserOrderCls.RelativeGroupInfo is FieldGroupInfo)
                {
                    #region Field Group Info

                    string strField=(UserOrderCls.RelativeGroupInfo as FieldGroupInfo).GroupByField;

                    if (backDataTable.Columns.Contains(strField))
                    {
                        #region Contains the Field

                        dataType = backDataTable.Columns[strField].DataType;

                        this.BtnLoad.Enabled = true;                    
                      
                        foreach (DataRow dr in backDataTable.Rows)
                        {
                            object objValue = dr[strField];

                            if (objValue == null || objValue is DBNull) continue;

                            if (!this.LstValue.Items.Contains(objValue))
                            {
                                this.LstValue.Items.Add(objValue);
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        this.BtnLoad.Enabled = false;

                    }
                    #endregion
                }
                else
                {
                    #region Section Group Info

                    dataType = typeof(string);

                    this.BtnLoad.Enabled = true;

              

                    SectionGroupInfo sectionInfo = UserOrderCls.RelativeGroupInfo as SectionGroupInfo;

                    if (sectionInfo == null) return;

                    this.LstValue.Items.Clear();

                    foreach (SectionFilter scFilter in sectionInfo.SectionFiltersWrapper.SectionFilters)
                    {
                        object objValue = scFilter.FilterName;

                        if (objValue == null || objValue is DBNull) continue;

                        this.LstValue.Items.Add(objValue);

                    }
                    #endregion
                }
            }
            else
            {
                this.BtnLoad.Enabled=false;           
            }
		}
		public void UpdateView()
		{
            UserOrderCls.OrderValues.Clear();

			foreach(object objValue in this.LstValue.Items)
			{
                UserOrderCls.OrderValues.Add(objValue);
			}
		}

		private void BtnOk_Click(object sender, System.EventArgs e)
		{
			this.UpdateView();

			DialogResult=DialogResult.OK;
		  
		}
	

		private void listBox2_MouseDown(object sender,    System.Windows.Forms.MouseEventArgs e)
		{
			indexofsource = ((ListBox)sender).IndexFromPoint(e.X, e.Y);
			if (indexofsource != ListBox.NoMatches)
			{
				((ListBox)sender).DoDragDrop(((ListBox)sender).Items[indexofsource].ToString(), DragDropEffects.All);
			}
		}
 
		private void listBox2_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
		{    //拖动源和放置的目的地一定是一个ListBox
			if (e.Data.GetDataPresent(typeof(System.String)) && ((ListBox)sender).Equals(this.LstValue))
			{
				e.Effect = DragDropEffects.Move;
			}
			else
				e.Effect = DragDropEffects.None;
		}

        
		private void listBox2_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
		{
			
			ListBox listbox=(ListBox)sender;

			indexoftarget=listbox.IndexFromPoint(listbox.PointToClient(new Point(e.X, e.Y)));

			if(indexofsource==indexoftarget)return;

            if(indexofsource<0||indexofsource>=this.LstValue.Items.Count)return;

			 if(indexoftarget<0||indexoftarget>=this.LstValue.Items.Count)return;

			if(indexoftarget!=ListBox.NoMatches)
			{
				object temp=listbox.Items[indexofsource];			

				if(indexoftarget>indexofsource)
				{
					for(int index=indexofsource;index<indexoftarget;index++)
					{
						listbox.Items[index]=listbox.Items[index+1];
					}

				}
				else
				{
					for(int index=indexofsource;index>indexoftarget;index--)
					{
						listbox.Items[index]=listbox.Items[index-1];
					}
				}

			
				listbox.Items[indexoftarget]=temp;
				listbox.SelectedIndex=indexoftarget;
				
			}
		}


		private void BtnCancel_Click(object sender, System.EventArgs e)
		{
		    DialogResult=DialogResult.Cancel;
		}

		private void BtnUp_Click(object sender, System.EventArgs e)
		{
			int count=LstValue.Items.Count;
            int index=LstValue.SelectedIndex;
			if(index<=0||count<=1)return;
            object objValue = this.LstValue.SelectedItem;
            this.LstValue.Items[index]=this.LstValue.Items[index-1];
            this.LstValue.Items[index - 1] = objValue;
             this.LstValue.SelectedIndex--;		
		}

		private void BtnDown_Click(object sender, System.EventArgs e)
		{
			int count=this.LstValue.Items.Count;
			int index=this.LstValue.SelectedIndex;
			if(index<0||count<=1||index+1>=count)return;
			object objValue=this.LstValue.SelectedItem;
			this.LstValue.Items[index]=this.LstValue.Items[index+1];
            this.LstValue.Items[index + 1] = objValue;
			this.LstValue.SelectedIndex++;
		}

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            if (backDataTable == null)
            {
                Webb.Utilities.MessageBoxEx.ShowError("Error datasource!");

                return;
            }
            
            if (UserOrderCls.RelativeGroupInfo is FieldGroupInfo)
            {
                string strField = (UserOrderCls.RelativeGroupInfo as FieldGroupInfo).GroupByField;

                if (backDataTable.Columns.Contains(strField))
                {
                    string strQuestion = "The operation would lead to reset the orders of the values\n\nContinue?";

                    if (Webb.Utilities.MessageBoxEx.ShowQuestion_YesNo(strQuestion) != DialogResult.Yes) return;

                    this.LstValue.Items.Clear();

                    foreach (DataRow dr in backDataTable.Rows)
                    {  
                        object objValue=dr[strField];
                        
                        if(objValue==null||objValue is DBNull)continue;

                        if (!this.LstValue.Items.Contains(objValue))
                        {
                            this.LstValue.Items.Add(objValue);
                        }
                    }
                }
                
            }
            else
            {
                string strQuestion = "The operation would lead to reset the orders of the values\n\nContinue?";

                if (Webb.Utilities.MessageBoxEx.ShowQuestion_YesNo(strQuestion) != DialogResult.Yes) return;

                SectionGroupInfo  sectionInfo=UserOrderCls.RelativeGroupInfo as  SectionGroupInfo;

                if(sectionInfo==null)return;

                this.LstValue.Items.Clear();

                foreach (SectionFilter scFilter in sectionInfo.SectionFiltersWrapper.SectionFilters)
                {
                    object objValue =scFilter.FilterName;

                    if (objValue == null || objValue is DBNull) continue;
                   
                     this.LstValue.Items.Add(objValue);
                    
                }

            }
        }

        private void BtnCreate_Click(object sender, EventArgs e)
        {
            if (dataType == null)
            {
                Webb.Utilities.MessageBoxEx.ShowError("Failed to find the datatType in the table!");
                return;
            }

            Webb.Utilities.NameForm nameForm=new Webb.Utilities.NameForm();

            if(nameForm.ShowDialog()==DialogResult.OK)
            {
                string strName=nameForm.FileName;

                try
                {
                    object objValue=Convert.ChangeType(strName,dataType);

                    this.LstValue.Items.Add(objValue);
                }
                catch
                {
                    Webb.Utilities.MessageBoxEx.ShowError("Failed to add the item!\nplease check the type of data!");

                    return;
                }
            }

          
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (this.LstValue.SelectedIndex < 0) return;

            string strQuestion = "Would you like to delete the selected item in the list?";

            if (Webb.Utilities.MessageBoxEx.ShowQuestion_YesNo(strQuestion) != DialogResult.Yes) return;

            this.LstValue.Items.RemoveAt(this.LstValue.SelectedIndex);

        }
	}
	#endregion

	public class CustomOrdersEditor : System.Drawing.Design.UITypeEditor
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

			if (!(value is UserOrderClS ))
				return value;

			IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

			CustomOrdersEditorForm CustomOrders= new CustomOrdersEditorForm(value);
			if (edSvc != null)
			{
				if (edSvc.ShowDialog(CustomOrders) == DialogResult.OK)
				{
					return CustomOrders.UserOrderCls;
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
}
