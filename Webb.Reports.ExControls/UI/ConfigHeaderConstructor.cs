using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Reflection;


using Webb.Collections;
using Webb.Reports.ExControls.Data;
using Webb.Reports.ExControls.Views;

namespace Webb.Reports.ExControls.UI
{
	/// <summary>
	/// Summary description for ConfigHeaderConstructor.
	/// </summary>
	public class ConfigHeaderConstructor : Webb.Reports.ExControls.UI.ExControlDesignerControlBase
	{

		private ExControlView _ExControlView=null;
		private System.Windows.Forms.Panel palpaint;

		private HeadersData Headers=new HeadersData();
		private System.Windows.Forms.Panel paledit;
		private System.Windows.Forms.Button BtnUnmerge;
		private System.Windows.Forms.Button BtnMerge;
		private System.Windows.Forms.Panel palbtn;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Button BtnRemoveRow;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.TextBox txtCell;
		private System.Windows.Forms.Button BtnEdit;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.Button btnColUnion;

		private int lblHeight=30;
		private System.Windows.Forms.Button BtnMoveLeft;
		private System.Windows.Forms.Button BtnMoveRight;
		private System.Windows.Forms.Button BtnShowall;		
		int MaxlblHeight=0;
		private System.Windows.Forms.Button BtnCopy;
		private System.Windows.Forms.Button BtnStyle;                      //Added this code at 2008-11-5 11:27:49@Simon
		bool MergedHeader=false;


	
		private void InitializeComponent()
		{
			this.palpaint = new System.Windows.Forms.Panel();
			this.paledit = new System.Windows.Forms.Panel();
			this.txtCell = new System.Windows.Forms.TextBox();
			this.BtnEdit = new System.Windows.Forms.Button();
			this.BtnMerge = new System.Windows.Forms.Button();
			this.BtnUnmerge = new System.Windows.Forms.Button();
			this.btnColUnion = new System.Windows.Forms.Button();
			this.BtnShowall = new System.Windows.Forms.Button();
			this.palbtn = new System.Windows.Forms.Panel();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.BtnCopy = new System.Windows.Forms.Button();
			this.BtnMoveRight = new System.Windows.Forms.Button();
			this.BtnMoveLeft = new System.Windows.Forms.Button();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.BtnRemoveRow = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.BtnStyle = new System.Windows.Forms.Button();
			this.paledit.SuspendLayout();
			this.palbtn.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// palpaint
			// 
			this.palpaint.AutoScroll = true;
			this.palpaint.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.palpaint.Dock = System.Windows.Forms.DockStyle.Fill;
			this.palpaint.Location = new System.Drawing.Point(0, 83);
			this.palpaint.Name = "palpaint";
			this.palpaint.Size = new System.Drawing.Size(700, 309);
			this.palpaint.TabIndex = 6;
			// 
			// paledit
			// 
			this.paledit.Controls.Add(this.txtCell);
			this.paledit.Controls.Add(this.BtnEdit);
			this.paledit.Controls.Add(this.BtnMerge);
			this.paledit.Controls.Add(this.BtnUnmerge);
			this.paledit.Controls.Add(this.btnColUnion);
			this.paledit.Controls.Add(this.BtnShowall);
			this.paledit.Dock = System.Windows.Forms.DockStyle.Top;
			this.paledit.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.paledit.Location = new System.Drawing.Point(0, 43);
			this.paledit.Name = "paledit";
			this.paledit.Size = new System.Drawing.Size(700, 40);
			this.paledit.TabIndex = 5;
			// 
			// txtCell
			// 
			this.txtCell.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtCell.Location = new System.Drawing.Point(8, 8);
			this.txtCell.Name = "txtCell";
			this.txtCell.Size = new System.Drawing.Size(216, 21);
			this.txtCell.TabIndex = 0;
			this.txtCell.Text = "";
			// 
			// BtnEdit
			// 
			this.BtnEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.BtnEdit.Location = new System.Drawing.Point(232, 8);
			this.BtnEdit.Name = "BtnEdit";
			this.BtnEdit.Size = new System.Drawing.Size(72, 24);
			this.BtnEdit.TabIndex = 0;
			this.BtnEdit.Text = "Edit";
			this.BtnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
			// 
			// BtnMerge
			// 
			this.BtnMerge.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.BtnMerge.Location = new System.Drawing.Point(312, 8);
			this.BtnMerge.Name = "BtnMerge";
			this.BtnMerge.Size = new System.Drawing.Size(72, 24);
			this.BtnMerge.TabIndex = 13;
			this.BtnMerge.Text = "Merge";
			this.BtnMerge.Click += new System.EventHandler(this.BtnMerge_Click);
			// 
			// BtnUnmerge
			// 
			this.BtnUnmerge.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.BtnUnmerge.Location = new System.Drawing.Point(392, 8);
			this.BtnUnmerge.Name = "BtnUnmerge";
			this.BtnUnmerge.Size = new System.Drawing.Size(72, 24);
			this.BtnUnmerge.TabIndex = 14;
			this.BtnUnmerge.Text = "Unmerge";
			this.BtnUnmerge.Click += new System.EventHandler(this.BtnUnMerge_Click);
			// 
			// btnColUnion
			// 
			this.btnColUnion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnColUnion.Location = new System.Drawing.Point(472, 8);
			this.btnColUnion.Name = "btnColUnion";
			this.btnColUnion.Size = new System.Drawing.Size(96, 24);
			this.btnColUnion.TabIndex = 15;
			this.btnColUnion.Text = "Vertical Union";
			this.btnColUnion.Click += new System.EventHandler(this.btnColUnion_Click);
			// 
			// BtnShowall
			// 
			this.BtnShowall.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.BtnShowall.Location = new System.Drawing.Point(600, 8);
			this.BtnShowall.Name = "BtnShowall";
			this.BtnShowall.Size = new System.Drawing.Size(80, 24);
			this.BtnShowall.TabIndex = 16;
			this.BtnShowall.Text = "Divide";
			this.BtnShowall.Click += new System.EventHandler(this.btnshowall_Click);
			// 
			// palbtn
			// 
			this.palbtn.Controls.Add(this.groupBox3);
			this.palbtn.Dock = System.Windows.Forms.DockStyle.Top;
			this.palbtn.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.palbtn.Location = new System.Drawing.Point(0, 0);
			this.palbtn.Name = "palbtn";
			this.palbtn.Size = new System.Drawing.Size(700, 43);
			this.palbtn.TabIndex = 0;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.BtnStyle);
			this.groupBox3.Controls.Add(this.BtnCopy);
			this.groupBox3.Controls.Add(this.BtnMoveRight);
			this.groupBox3.Controls.Add(this.BtnMoveLeft);
			this.groupBox3.Controls.Add(this.checkBox1);
			this.groupBox3.Controls.Add(this.BtnRemoveRow);
			this.groupBox3.Controls.Add(this.btnAdd);
			this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox3.Location = new System.Drawing.Point(0, 0);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(700, 56);
			this.groupBox3.TabIndex = 0;
			this.groupBox3.TabStop = false;
			// 
			// BtnCopy
			// 
			this.BtnCopy.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
			this.BtnCopy.Location = new System.Drawing.Point(376, 16);
			this.BtnCopy.Name = "BtnCopy";
			this.BtnCopy.Size = new System.Drawing.Size(104, 24);
			this.BtnCopy.TabIndex = 21;
			this.BtnCopy.Text = "CopyFromField";
			this.BtnCopy.Click += new System.EventHandler(this.BtnCopy_Click);
			// 
			// BtnMoveRight
			// 
			this.BtnMoveRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
			this.BtnMoveRight.Location = new System.Drawing.Point(280, 16);
			this.BtnMoveRight.Name = "BtnMoveRight";
			this.BtnMoveRight.Size = new System.Drawing.Size(80, 24);
			this.BtnMoveRight.TabIndex = 19;
			this.BtnMoveRight.Text = "Move Right";
			this.BtnMoveRight.Click += new System.EventHandler(this.BtnMoveRight_Click);
			// 
			// BtnMoveLeft
			// 
			this.BtnMoveLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
			this.BtnMoveLeft.Location = new System.Drawing.Point(208, 16);
			this.BtnMoveLeft.Name = "BtnMoveLeft";
			this.BtnMoveLeft.Size = new System.Drawing.Size(72, 24);
			this.BtnMoveLeft.TabIndex = 18;
			this.BtnMoveLeft.Text = "Move Left";
			this.BtnMoveLeft.Click += new System.EventHandler(this.BtnMoveLeft_Click);
			// 
			// checkBox1
			// 
			this.checkBox1.Location = new System.Drawing.Point(600, 16);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(80, 24);
			this.checkBox1.TabIndex = 17;
			this.checkBox1.Text = "Grid Line";
			this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
			// 
			// BtnRemoveRow
			// 
			this.BtnRemoveRow.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
			this.BtnRemoveRow.Location = new System.Drawing.Point(90, 16);
			this.BtnRemoveRow.Name = "BtnRemoveRow";
			this.BtnRemoveRow.Size = new System.Drawing.Size(93, 24);
			this.BtnRemoveRow.TabIndex = 15;
			this.BtnRemoveRow.Text = "Remove Row";
			this.BtnRemoveRow.Click += new System.EventHandler(this.BtnRemoveRow_Click);
			// 
			// btnAdd
			// 
			this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
			this.btnAdd.Location = new System.Drawing.Point(8, 16);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(80, 24);
			this.btnAdd.TabIndex = 14;
			this.btnAdd.Text = "Add Row";
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// BtnStyle
			// 
			this.BtnStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.BtnStyle.Location = new System.Drawing.Point(496, 16);
			this.BtnStyle.Name = "BtnStyle";
			this.BtnStyle.Size = new System.Drawing.Size(88, 24);
			this.BtnStyle.TabIndex = 22;
			this.BtnStyle.Text = "Set CellStyle";
			this.BtnStyle.Click += new System.EventHandler(this.BtnStyle_Click);
			// 
			// ConfigHeaderConstructor
			// 
			this.Controls.Add(this.palpaint);
			this.Controls.Add(this.paledit);
			this.Controls.Add(this.palbtn);
			this.Name = "ConfigHeaderConstructor";
			this.Size = new System.Drawing.Size(700, 392);
			this.Resize += new System.EventHandler(this.ConfigHeaderConstructor_Resize);
			this.Load += new System.EventHandler(this.ConfigHeaderConstructor_Load);
			this.paledit.ResumeLayout(false);
			this.palbtn.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);

		}
	
		public ConfigHeaderConstructor()
		{
			InitializeComponent();
		
		}		
		public override void SetView(ExControlView i_View)
		{
			this.Headers.txtEdit=this.txtCell;

			if(i_View==null||!(i_View is IMultiHeader))return;

			_ExControlView=i_View; 
           
			IMultiHeader mainView=i_View as IMultiHeader;

            if(i_View.PrintingTable==null)return;

			int colcount=i_View.PrintingTable.GetColumns();

			colcount=colcount<=0?0:colcount;

			if(mainView.HeadersData==null)
			{
				this.Headers=new HeadersData(0,colcount);

			}
			else
			{
				this.Headers=mainView.HeadersData.Copy();
								
				int count= colcount-this.Headers.ColCount;

				for(int i=0;i<count;i++)
				{					  
					this.Headers.AddCol();
				}
				for(int i=count;i<0;i++)
				{					  
					this.Headers.RemoveCol();
				}
								
			}
			
			this.LayoutHeaders();
		   	
		}
		private void LayoutHeaders()
		{  
			if(this.Headers==null)return;
			this.Headers.txtEdit=this.txtCell;
			this.checkBox1.Checked=this.Headers.GridLine;
		
			foreach(Control ctrl in this.groupBox3.Controls)
			{
				ctrl.Enabled=true;
			}
			foreach(Control ctrl in this.paledit.Controls)
			{
				ctrl.Enabled=true;
			}			
			this.BtnEdit.Text="Edit";
			this.txtCell.Enabled=false;				

			paintAllCells();
		}

		public override void UpdateView(ExControlView i_View)
		{
			if(i_View==null)return;

			IMultiHeader mainView=i_View as IMultiHeader;
			
			if(this.Headers!=null)
			{							
				mainView.HeadersData=this.Headers.Copy();
			}
		
		}

		
		private void AddHeaderRows()
		{  
			if(Headers.ColCount<=0)
			{
				MessageBox.Show("There is no Columns there,please set the fields first!","No Columns");
				return;
			}
			if(Headers.RowCount>=3)
			{
				MessageBox.Show("The HeaderRows's count shouldn't be more than 3.!","HeaderRows's count");
				return;
			}

			string[] strRow=new string[Headers.ColCount];

			for(int j=0;j<Headers.ColCount;j++)
			{
				strRow[j] = string.Empty ;
			}
			this.Headers.AddRow(strRow);
			paintAllCells();
			
		}
		
		
		private void btnAdd_Click(object sender, System.EventArgs e)
		{	
			AddHeaderRows();	
		}

		private void ConfigHeaderConstructor_Load(object sender, System.EventArgs e)
		{
		
		}

		private void BtnRemoveCol_Click(object sender, System.EventArgs e)
		{
			Headers.RemoveCol();
			paintAllCells();
			
		}

		private void BtnRemoveRow_Click(object sender, System.EventArgs e)
		{
			Headers.RemoveRow();
			paintAllCells();
		
		
		}

		private void BtnMerge_Click(object sender, System.EventArgs e)
		{
			HeaderCell hc=Headers.MergeCells();
			paintAllCells();			
			Headers.RaiseClickEvents(hc);			
		}

		private void BtnUnMerge_Click(object sender, System.EventArgs e)
		{
			Headers.UnMergeCells();
			paintAllCells();	
		
		}

		private void BtnEdit_Click(object sender, System.EventArgs e)
		{
			if(this.BtnEdit.Text=="Edit")
			{
				if(this.Headers.SelCount<=0)
				{
					MessageBox.Show("Please select the cell in the table below");
					return;
				}
				this.BtnEdit.Text="Update";
				foreach(Control ctrl in this.groupBox3.Controls)
				{
					ctrl.Enabled=false;
				}	
				foreach(Control ctrl in this.paledit.Controls)
				{
					if((ctrl is Button)&&ctrl.Name!="BtnEdit")ctrl.Enabled=false;
				}
				this.BtnEdit.Enabled=true;
				this.txtCell.Enabled=true;					
				this.txtCell.Focus();
			
				
			}
			else
			{
				foreach(Control ctrl in this.groupBox3.Controls)
				{
					ctrl.Enabled=true;
				}	
				foreach(Control ctrl in this.paledit.Controls)
				{
					ctrl.Enabled=true;
				}
				this.Headers.UpdateText(this.txtCell.Text);
				this.BtnEdit.Text="Edit";
				this.txtCell.Enabled=false;		
				this.paintAllCells();			}
		
		}

	
		private void DrawLabelPaint(object sender,PaintEventArgs arg)
		{   
			Label drawlabel=sender as Label;
			int index=drawlabel.Name.IndexOf("|");
			string text=drawlabel.Name.Substring(index+1);
			if(text.Length<=0)return;
			Graphics g=arg.Graphics;
			g.Clear(drawlabel.BackColor);		
			StringFormat sf=new StringFormat();
			sf.LineAlignment=StringAlignment.Center;
			sf.Alignment=StringAlignment.Center;
			StringFormatFlags strflags=(StringFormatFlags)drawlabel.Tag;
			if((strflags&StringFormatFlags.DirectionVertical)==StringFormatFlags.DirectionVertical)
			{
				strflags|=StringFormatFlags.NoWrap;
				sf.Alignment=StringAlignment.Far;
			}
			sf.FormatFlags=strflags;			
			g.DrawString(text,drawlabel.Font,new SolidBrush(drawlabel.ForeColor),arg.ClipRectangle,sf);			
		}

		private void TestMaxlblheight(Graphics g,Font drawfont,string text)
		{
			SizeF szftext=g.MeasureString(text,drawfont);
			int sqrHigh=(int)Math.Max(szftext.Width,szftext.Height)+3;
			MaxlblHeight=Math.Max(sqrHigh,MaxlblHeight);	
		}
		private void PaintTable(Panel paintControl,Rectangle rect,Rectangle gridlineArea)
		{	
			Label drawlabel;    
			if(_ExControlView==null||_ExControlView.PrintingTable==null)
			{
                drawlabel=new Label();
				drawlabel.Bounds=rect;
			    drawlabel.Text="There isn't any report data here!";
				drawlabel.BorderStyle=BorderStyle.FixedSingle;
				drawlabel.TextAlign=System.Drawing.ContentAlignment.MiddleCenter;
				drawlabel.Name="cell";				
                paintControl.Controls.Add(drawlabel);
				lblHeight=30;
				return;
			}	
			WebbTable table=_ExControlView.PrintingTable;			        
			
			if(table==null||table.GetColumns()<=0||table.GetRows()<=0)
			{
				drawlabel=new Label();
				drawlabel.Bounds=rect;
				drawlabel.Text="No Data!";	
				drawlabel.BorderStyle=BorderStyle.FixedSingle;
				drawlabel.TextAlign=System.Drawing.ContentAlignment.MiddleCenter;
				drawlabel.Name="cell";			
				paintControl.Controls.Add(drawlabel);
				lblHeight=30;
				return;
			}	
			
		
			ArrayList prnHeaders=new ArrayList();
			ArrayList formats=new ArrayList();		
	
			IMultiHeader mainView=_ExControlView as IMultiHeader;	
		
			prnHeaders=mainView.GetPrnHeader(out formats);

								
			int tempHeight=0;

			if(mainView.HaveHeader)   //Modified at 2008-10-20 11:24:01@Simon
			{
				int lblWidth=rect.Width/prnHeaders.Count;

				ArrayList lblhigharr=new ArrayList();  //Added this code at 2008-11-5 9:33:39@Simon

				bool b_ver=false;

				Bitmap bmpmemory=new Bitmap(rect.Width,rect.Height);    //Added this code at 2008-11-7 17:23:35@Simon

				Graphics g0=Graphics.FromImage(bmpmemory);             //Added this code at 2008-11-7 17:23:31@Simon

				drawlabel=new Label();

                MergedHeader=false;

				for(int i=0;i<prnHeaders.Count;i++)
				{
					string text=(string)prnHeaders[i];

                    tempHeight=lblHeight;

					Rectangle lblrect=new Rectangle(rect.X+i*lblWidth,rect.Y,lblWidth,tempHeight);

					if(text=="\n")
					{
						drawlabel.Width+=lblrect.Width;

                        MergedHeader=true;
						continue;

					}					

					StringFormatFlags strflags=(StringFormatFlags)formats[i];  //Added this code at 2008-11-5 8:53:15@Simon
					
					drawlabel=new Label();

					if(Headers.ColsToMerge.Contains(i))
					{
						drawlabel.Name="cell0|"+text;
					}
					else
					{
						drawlabel.Name="cell1|"+text;
					}
					drawlabel.Tag=strflags; 

					#region Modify codes at 2008-11-5 16:02:24@Simon
					if(this.Headers.GridLine)
					{ 
						drawlabel.BorderStyle=BorderStyle.FixedSingle;
						if(Headers.ColsToMerge.Contains(i))
						{
							drawlabel.Location=new Point(lblrect.X,gridlineArea.Y);
							drawlabel.Size=new Size(lblrect.Width,lblrect.Height+gridlineArea.Height);
						}
						else
						{
							drawlabel.Bounds=lblrect;
						}                         
					}
					else
					{
						drawlabel.BorderStyle=BorderStyle.None;	
						if(Headers.ColsToMerge.Contains(i))
						{
							drawlabel.Location=new Point(lblrect.X+1,gridlineArea.Y+1);
							drawlabel.Size=new Size(lblrect.Width-2,lblrect.Height+gridlineArea.Height-2);
						}
						else
						{
							drawlabel.Location=new Point(lblrect.X+1,lblrect.Y+1);
							drawlabel.Size=new Size(lblrect.Width-2,lblrect.Height-2);
						}
					}		
					#endregion        //End Modify

					drawlabel.Paint+=new PaintEventHandler(DrawLabelPaint);
					drawlabel.BackColor=Control.DefaultBackColor;
					drawlabel.TextAlign=System.Drawing.ContentAlignment.MiddleCenter;					
                    paintControl.Controls.Add(drawlabel);                  

					#region add codes at 2008-11-5 9:44:30@Simon
                      
					    lblhigharr.Add(drawlabel);					   
						if(strflags==StringFormatFlags.DirectionVertical)
						{   this.TestMaxlblheight(g0,drawlabel.Font,text);
							if(!Headers.ColsToMerge.Contains(i)||Headers.RowCount==0)
							{
								b_ver=true;
							}
							
						}
					    
					#endregion        //End Modify

				}

				#region Modify codes at 2008-11-5 9:54:22@Simon
				MaxlblHeight=Math.Min(rect.Height-10,MaxlblHeight);
				if(b_ver&&MaxlblHeight>0)
				{
					tempHeight=MaxlblHeight;
					foreach(object objtext in lblhigharr)
					{ 
						if((objtext as Label).Name.Substring(4,1)=="0")					
						{
							(objtext as Label).Height=this.Headers.GridLine?gridlineArea.Height+MaxlblHeight:gridlineArea.Height+MaxlblHeight-2;	
						}
						else
						{
							(objtext as Label).Height=this.Headers.GridLine?MaxlblHeight:MaxlblHeight-2;	
						}

					}
				}
                MaxlblHeight=0;  
				g0.Dispose();
				bmpmemory.Dispose();
				#endregion        //End Modify
			
				rect.Y+=tempHeight;
				rect.Width=lblWidth*prnHeaders.Count;
				rect.Height-=tempHeight;
				gridlineArea.Height+=tempHeight;
				if(gridlineArea.Width<=0)
				{
					gridlineArea.X=rect.X;
					gridlineArea.Width=rect.Width;

				}			
				
			}	
		    if(!this.Headers.GridLine)
			{
				drawlabel=new Label();
				drawlabel.Bounds=gridlineArea;
				drawlabel.Text="";	
				drawlabel.TextAlign=System.Drawing.ContentAlignment.MiddleCenter;
				drawlabel.BorderStyle=BorderStyle.FixedSingle;	
				drawlabel.BackColor = Control.DefaultBackColor; 		    
				drawlabel.Name="cell";
				paintControl.Controls.Add(drawlabel);
				drawlabel.SendToBack();	
			}
			drawlabel=new Label();
			drawlabel.Bounds=rect;
			drawlabel.Text="Table Data";	
			drawlabel.TextAlign=System.Drawing.ContentAlignment.MiddleCenter;
			drawlabel.BorderStyle=BorderStyle.FixedSingle;			
			drawlabel.Name="cell";
			paintControl.Controls.Add(drawlabel);
			lblHeight=30;	
		}

	

		private void paintAllCells()
		{
			int BoundHeight=this.palpaint.Height;
			int MaxSelAreaHeight=(BoundHeight-30)/2;

           Rectangle gridlineArea=new Rectangle(15,15,0,0);
         
			int rectWidth=this.palpaint.Width-30;
           
			Rectangle rect=new Rectangle(15,15,rectWidth,BoundHeight-30);     

			int SelAreaHeight=this.Headers.RowCount*lblHeight;
            SelAreaHeight=SelAreaHeight<=MaxSelAreaHeight?SelAreaHeight:MaxSelAreaHeight;		

             palpaint.Controls.Clear();			
	       
			if(Headers!=null&&Headers.RowCount>0&&Headers.ColCount>0)
			{
                lblHeight=SelAreaHeight/this.Headers.RowCount;
                Rectangle SelArea=new Rectangle(rect.X,rect.Y,rect.Width,SelAreaHeight);
				
				#region Modify codes at 2008-11-6 9:22:26@Simon					

					IMultiHeader mainView=_ExControlView as IMultiHeader;
					
				     Headers.PaintCells(palpaint,SelArea,mainView);	
				#endregion        //End Modify

				rect.Y=rect.Y+ SelAreaHeight;
				rect.Height=rect.Height-SelAreaHeight;
                				
				int SelAreaWidth=(int)(rect.Width/this.Headers.ColCount)*this.Headers.ColCount;				
				rect.Width=SelAreaWidth;
                gridlineArea=new Rectangle(15,15,SelAreaWidth,SelAreaHeight);

			}  			
			this.PaintTable(palpaint,rect,gridlineArea);
		}

		private void BtnAddCol_Click(object sender, System.EventArgs e)
		{
			this.Headers.AddCol();
			paintAllCells();
		}

		private void checkBox1_CheckedChanged(object sender, System.EventArgs e)
		{
			this.Headers.GridLine=this.checkBox1.Checked;
            paintAllCells();
		}

		private void btnColUnion_Click(object sender, System.EventArgs e)
		{
			if(this.MergedHeader)
			{
				MessageBox.Show("Couldnn't merge cells whose column contains merged cells!","Failed");
				return; 
			}
			this.Headers.HideHeaderColumns();
			paintAllCells();
		}

		private void btnshowall_Click(object sender, System.EventArgs e)
		{
			this.Headers.ColsToMerge.Clear();
			paintAllCells();
		
		}

		private void ConfigHeaderConstructor_Resize(object sender, System.EventArgs e)
		{
			paintAllCells();
		}

		private void BtnMoveLeft_Click(object sender, System.EventArgs e)
		{
			this.Headers.MoveLeft();
			paintAllCells();
		}

		private void BtnMoveRight_Click(object sender, System.EventArgs e)
		{
			this.Headers.MoveRight();;
			paintAllCells();
		}

		private void BtnCopy_Click(object sender, System.EventArgs e)
		{
			if(this.Headers.SelCount<=0)
			{
				MessageBox.Show("Please select the cell in the table below");
				return;
			}
			
			ArrayList formats=new ArrayList();		
	
			IMultiHeader mainView=_ExControlView as IMultiHeader;	
		
			ArrayList prnHeaders=mainView.GetPrnHeader(out formats);

			this.Headers.CopyHeaderFromField(prnHeaders);

             paintAllCells();
		}

		private void BtnStyle_Click(object sender, System.EventArgs e)
		{
			if(this.Headers.SelCount<=0)
			{
				MessageBox.Show("Please select the cell in the table below");
				return;
			}
			Webb.Utilities.PropertyForm proprtyForm=new Webb.Utilities.PropertyForm();

			HeaderCellStyle headerstyle=new HeaderCellStyle();

            this.Headers.SetInitStyle(headerstyle);

            proprtyForm.BindProperty(headerstyle);

			if(proprtyForm.ShowDialog()==DialogResult.OK)
			{
				this.Headers.UpdateCellStyle(headerstyle);
				this.paintAllCells();
			}	
			
		}
	}
}
