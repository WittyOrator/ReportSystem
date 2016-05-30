//08-15-2008@Simon
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Reflection;

using Webb.Reports.ExControls.Views;
using Webb.Reports;
using Webb.Collections;

namespace Webb.Reports.ExControls.Data
{
    public struct ButtonTags
    {
        public int RowIndex, ColIndex, RowMax, ColMax;
        public Point btnLoc;
        public Size btnSize;
        public SectionFilter filter;

    }
    public class SplitButtons
    {

        private enum EnumMousePointPosition
        {
            MouseSizeNone = 0, //'无
            MouseSizeRight = 1, //'拉伸右边框
            MouseSizeLeft = 2, //'拉伸左边框
            MouseSizeBottom = 3, //'拉伸下边框
            MouseSizeTop = 4, //'拉伸上边框
            MouseSizeTopLeft = 5, //'拉伸左上角
            MouseSizeTopRight = 6, //'拉伸右上角
            MouseSizeBottomLeft = 7, //'拉伸左下角
            MouseSizeBottomRight = 8, //'拉伸右下角
            MouseDrag = 9  // '鼠标拖动
        }

        const int Band = 10;
        const int MinWidth = 40;
        const int MinHeight = 40;
        private EnumMousePointPosition m_MousePointPosition;


        private Point p=Point.Empty, p1=Point.Empty;

        private ButtonTags btntag;
        public System.Windows.Forms.PropertyGrid C_PropertyGrid;

        private System.Windows.Forms.Button[][] Buttons;
        private System.Windows.Forms.Panel[] btnContainer;
        private System.Windows.Forms.Button _CurSelectedButton = null;

        public Control mainControl;

        private System.Windows.Forms.MouseEventArgs moveArgs;
        private FieldLayOut _layout;

        public SplitButtons()
        {
           
        }
        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.mainControl != null)
                { 
                    Buttons = null;
                    foreach (Control lctrl in mainControl.Controls)
                    {
                        lctrl.Dispose();
                    }
                }
            }           
        }

        private int GetCount(Int32Collection nCols)
        {
            if (nCols == null) return 0;           
            int all = 0;
            for (int i = 0; i < nCols.Count; i++)
            {
                all += nCols[i];
            }
            return all;            
        }
        public bool LoadSectionFilters(Int32Collection nCols,SectionFilterCollection secFilters)
        {
            if (nCols == null) return false;       
            if (secFilters == null) return false;                  

            int lack = GetCount(nCols) - secFilters.Count;

            if (lack > 0)
            {
                for (int i = 0; i < lack; i++)
                {
                    secFilters.Add(new SectionFilter(new Webb.Data.DBFilter()));
                }
            }
            return true;
        }

        public ArrayList GetButton()
        {
            ArrayList pics = new ArrayList();
            for (int i = 0; i < this.Buttons.Length;i++ )
            {
                foreach (Button btn in this.Buttons[i])
                {
                    pics.Add(btn);
                }
            }
            return pics;
        }

        public Button GetButton(int index)
        {
            int m = this.Buttons.Length;
            int all = 0;
            int i = 0;
            if (m <= 0) return null;

            for (i = 0; i < m; i++)
            {
                if (index < all + Buttons[i].Length) break;
                all += Buttons[i].Length;
            }
            if (i >= m) return null;

            int j = index - all;
            return Buttons[i][j];
        }

        public Button GetButton(int rindex,int cindex)
        {
            if (rindex< 0||rindex>=this.Buttons.Length) return null;

            if (cindex<0||cindex >= this.Buttons[rindex].Length) return null;
           
            return Buttons[rindex][cindex];
        }
        private float GetMaxRadio(FieldLayOut layout, int i)
        {   
             float maxradio=0f; 
            if (i < 0 || i >= layout.ColumnsEachRow.Count) return 0;

            foreach (Field fld in layout.FieldTable[i])
            {
                SizeF szf = fld.Ratio;
                if (maxradio < szf.Height) maxradio = szf.Height;
             }
             return maxradio;

        }
        private Size GetButtonSize(int row, int col)
        {
            Field field = this._layout.GetField(row, col);

            if (field == null) return Size.Empty;
            int iWidth = (int)(this.mainControl.Width * field.Ratio.Width);
            int iHeight=(int)(this.mainControl.Height * field.Ratio.Height);
            return new Size(iWidth,iHeight);
        }
		private int GetRestWidth(int row)  //Added this code at 2008-12-12 13:57:44@Simon
		{
            if(this.mainControl==null||this._layout.FieldTable==null) return 0;
			int iRestWidth = this.mainControl.Width;
			
			for(int col=0;col<this._layout.FieldTable[row].Count;col++)
			{
				Field field = this._layout.GetField(row, col);
				if (field == null||this.mainControl==null) return 0;
				iRestWidth-= (int)(this.mainControl.Width * field.Ratio.Width);
			}	
			
			return iRestWidth;
		}

//        public void LoadLayout(FieldLayOut layout, Control ctnControl, SectionFilterCollection sfcfilter, System.Windows.Forms.PropertyGrid propertyGrid)
       public void LoadLayout(FieldLayOut layout, SectionFilterCollection sections)
	  {
		   if(layout==null||layout.FieldTable == null)return;

		   _layout = layout;   
          
             Int32Collection nCols = _layout.ColumnsEachRow;
		
            int nRowCount = nCols.Count;

		     if (nRowCount <= 0||!LoadSectionFilters(nCols, sections)) return;  
		  
             mainControl.Controls.Clear();
            
             Rectangle rect = mainControl.ClientRectangle;          

             mainControl.BackColor = Control.DefaultBackColor;           

            this.C_PropertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.C_PropertyGrid_PropertyValueChanged);
          
            Buttons = new Button[nRowCount][];

            btnContainer = new Panel[nRowCount];

            int sfcindex = 0;

           
            int panelLocX = rect.X;
            int panelLocY = rect.Y;
            int nButtonHeight=0;			
           
            for (int i = 0; i < nRowCount; i++)
            {   
                panelLocY+= nButtonHeight;
                nButtonHeight = (int)(rect.Height*this.GetMaxRadio(layout,i));
               
                btnContainer[i] = new Panel();
                btnContainer[i].Name = i.ToString();
                btnContainer[i].Location = new Point(panelLocX, panelLocY);
                btnContainer[i].Size = new Size(rect.Width, nButtonHeight);
                btnContainer[i].BorderStyle = BorderStyle.None;

                int iColCount = nCols[i];
                if (iColCount > 0)
                {
                    Buttons[i] = new Button[iColCount];
                }
                
                int buttonLocX = 0;

				int restWidth=this.GetRestWidth(i); //Added this code at 2008-12-12 13:58:28@Simon
                
                for (int j = 0; j < iColCount; j++)
                {
                    Button newButton = new Button();
                    newButton.Location = new Point(buttonLocX, 0);
                    newButton.Size = this.GetButtonSize(i, j);
					if(restWidth>0)
					{
						newButton.Width+=1;
                        restWidth--;

					}
					else if(restWidth<0)
					{
						newButton.Width-=1;
						restWidth++;
					}
                    buttonLocX+=newButton.Size.Width   ;                   
                    newButton.BackColor = Control.DefaultBackColor;                    
                    
                    
                    btntag = new ButtonTags();
                    btntag.RowIndex = i;
                    btntag.ColIndex = j;
                    btntag.RowMax = nRowCount - 1;
                    btntag.ColMax = nRowCount - 1;
                    btntag.btnLoc = new Point(newButton.Location.X, newButton.Location.X);
                    btntag.btnSize = new Size(newButton.Width,newButton.Height);
                    btntag.filter = sections[sfcindex];                   

                    sfcindex++;
                    newButton.Name = string.Format("Button{0}", sfcindex);
                    newButton.Text = btntag.filter.FilterName;
                    newButton.Tag = btntag;



                    newButton.MouseDown += new System.Windows.Forms.MouseEventHandler(MyMouseDown);
                    newButton.MouseLeave += new System.EventHandler(MyMouseLeave);
                    newButton.MouseMove += new System.Windows.Forms.MouseEventHandler(MyMouseMove);                   
                    newButton.Click += new System.EventHandler(MyClick);

                    Buttons[i][j] = newButton;
                    btnContainer[i].Controls.Add(newButton);				
                   
                }
                this.mainControl.Controls.Add(btnContainer[i]);
            }

           
        }

        


        #region mouseEvent
        private void MyClick(object sender, System.EventArgs e)
        {
            Button lctl; 
            ButtonTags thistag ;          
            foreach (object obj in this.GetButton())
            {
                lctl = obj as Button;
                thistag= (ButtonTags)lctl.Tag;
                lctl.BackColor = Control.DefaultBackColor;
                lctl.Text=thistag.filter.FilterName;           

            }
            lctl = sender as Button;
            thistag = (ButtonTags)lctl.Tag;
            lctl.BackColor = Color.Tomato;
            lctl.Text = thistag.filter.FilterName;

            _CurSelectedButton = lctl;

            this.C_PropertyGrid.SelectedObject = thistag.filter;

            //string last = thistag.btnLoc.ToString() + '\n' + thistag.btnSize.ToString();
            //last = string.Format("第{0}行，第{1}列", thistag.RowIndex + 1, thistag.ColIndex + 1) + '\n' + last;
            //MessageBox.Show(last);
        }


        private void C_PropertyGrid_PropertyValueChanged(object s, System.Windows.Forms.PropertyValueChangedEventArgs e)
        {
            if (_CurSelectedButton != null)
            {

                ButtonTags thistag = (ButtonTags)_CurSelectedButton.Tag;

                this._CurSelectedButton.Text = thistag.filter.FilterName;

            }
        }

        private void MyMouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            p.X = e.X;
            p.Y = e.Y;
            p1.X = e.X;
            p1.Y = e.Y;
        }

        private void MyMouseLeave(object sender, System.EventArgs e)
        {            
            m_MousePointPosition = EnumMousePointPosition.MouseSizeNone;
            mainControl.Cursor = Cursors.Arrow;
        }

        private void MyMouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            moveArgs = e;
            
                #region origin move
                Control lCtrl = (sender as Control);
                if (!(lCtrl is Button)) return;

                btntag = (ButtonTags)lCtrl.Tag;
                int rowindex = btntag.RowIndex;
                int colindex = btntag.ColIndex;
                int rowmax = btntag.RowMax;
                int colmax = btntag.ColMax;

                if (moveArgs.Button == MouseButtons.Left)
                {
                    switch (m_MousePointPosition)
                    {
                        case EnumMousePointPosition.MouseSizeBottom:
                            if (rowindex >= rowmax) return;
                            if (lCtrl.Height + (moveArgs.Y - p1.Y) <= MinHeight) return;
                            if (Buttons[rowindex + 1][colindex].Height - moveArgs.Y + p1.Y <= MinHeight) return;

                            foreach (Button btn in Buttons[rowindex])
                            {
                                btn.Height += (moveArgs.Y - p1.Y);

                                btntag = (ButtonTags)btn.Tag;
                                btntag.btnSize.Height = btn.Height;
                                btn.Tag = btntag;

                            }                           
                            foreach (Button btn in Buttons[rowindex + 1])
                            {
                                //btn.Top += (moveArgs.Y - p1.Y);
                                btn.Height -= (moveArgs.Y - p1.Y);

                                btntag = (ButtonTags)btn.Tag;
                                btntag.btnLoc.Y = btn.Top;
                                btntag.btnSize.Height = btn.Height;
                                btn.Tag = btntag;
                            }
                            this.btnContainer[rowindex].Height += (moveArgs.Y - p1.Y);
                            btnContainer[rowindex + 1].Top += (moveArgs.Y - p1.Y);
                            btnContainer[rowindex + 1].Height -= (moveArgs.Y - p1.Y);
                            btnContainer[rowindex + 1].Refresh();
                            btnContainer[rowindex].Refresh();

                            p1.X = moveArgs.X;
                            p1.Y = moveArgs.Y; //'记录光标拖动的当前点
                            break;
                        case EnumMousePointPosition.MouseSizeRight:
                            if (colindex >= colmax) return;
                            if (Buttons[rowindex][colindex + 1].Width - moveArgs.X + p1.X <= MinWidth) return;
                            if (lCtrl.Width + moveArgs.X - p1.X <= MinWidth) return;

                            lCtrl.Width = lCtrl.Width + moveArgs.X - p1.X;

                            btntag = (ButtonTags)lCtrl.Tag;
                            btntag.btnSize.Width = lCtrl.Width;
                            lCtrl.Tag = btntag;

                            Buttons[rowindex][colindex + 1].Left += moveArgs.X - p1.X;
                            Buttons[rowindex][colindex + 1].Width -= moveArgs.X - p1.X;

                            btntag = (ButtonTags)Buttons[rowindex][colindex + 1].Tag;
                            btntag.btnLoc.X = Buttons[rowindex][colindex + 1].Left;
                            btntag.btnSize.Width = Buttons[rowindex][colindex + 1].Width;
                            Buttons[rowindex][colindex + 1].Tag = btntag;

                            p1.X = moveArgs.X;
                            p1.Y = moveArgs.Y; //'记录光标拖动的当前点	

                            lCtrl.Refresh();
                            Buttons[rowindex][colindex + 1].Refresh();

                            break;
                        case EnumMousePointPosition.MouseSizeTop:
                            if (rowindex <= 0) return;
                            if (lCtrl.Height - (moveArgs.Y - p.Y) <= MinHeight) return;
                            if (Buttons[rowindex - 1][colindex].Height + moveArgs.Y - p.Y <= MinHeight) return;

                            
                            foreach (Button btn in Buttons[rowindex - 1])
                            {
                                btn.Height += (moveArgs.Y - p.Y);

                                btntag = (ButtonTags)btn.Tag;
                                btntag.btnSize.Height = btn.Height;
                                btn.Tag = btntag;
                            }

                           

                            foreach (Button btn in Buttons[rowindex])
                            {
                                //btn.Top += (moveArgs.Y - p.Y);
                                btn.Height -= (moveArgs.Y - p.Y);

                                btntag = (ButtonTags)btn.Tag;
                                btntag.btnLoc.Y = btn.Top;
                                btntag.btnSize.Height = btn.Height;
                                btn.Tag = btntag;

                            }
                            btnContainer[rowindex - 1].Height += (moveArgs.Y - p.Y);
                            btnContainer[rowindex - 1].Refresh();
                            btnContainer[rowindex].Top += (moveArgs.Y - p.Y);
                            btnContainer[rowindex].Height -= (moveArgs.Y - p.Y);
                            btnContainer[rowindex].Refresh();
                            break;
                        case EnumMousePointPosition.MouseSizeLeft:
                            if (colindex <= 0) return;
                            if (Buttons[rowindex][colindex - 1].Width + moveArgs.X - p1.X <= MinWidth) return;
                            if (lCtrl.Width - (moveArgs.X - p.X) <= MinWidth) return;

                            Buttons[rowindex][colindex - 1].Width += moveArgs.X - p1.X;

                            btntag = (ButtonTags)Buttons[rowindex][colindex - 1].Tag;
                            btntag.btnSize.Width = Buttons[rowindex][colindex - 1].Width;
                            Buttons[rowindex][colindex - 1].Tag = btntag;
                            Buttons[rowindex][colindex - 1].Refresh();

                            lCtrl.Left = lCtrl.Left + moveArgs.X - p.X;
                            lCtrl.Width = lCtrl.Width - (moveArgs.X - p.X);
                           

                            btntag = (ButtonTags)lCtrl.Tag;
                            btntag.btnLoc.X = lCtrl.Left;
                            btntag.btnSize.Width = lCtrl.Width;
                            lCtrl.Tag = btntag;
                            lCtrl.Refresh();

                            break;
                        default:
                            break;
                    }
                    
                }

                else
                {
                    m_MousePointPosition = MousePointPosition(lCtrl.Size, e);  //'判断光标的位置状态
                    switch (m_MousePointPosition)  //'改变光标
                    {
                        case EnumMousePointPosition.MouseSizeNone:
                            mainControl.Cursor = Cursors.Arrow;       //'箭头
                            break;
                        case EnumMousePointPosition.MouseDrag:
                            mainControl.Cursor = Cursors.Arrow;     //'四方向
                            break;
                        case EnumMousePointPosition.MouseSizeBottom:
                            if (rowindex < rowmax)
                                mainControl.Cursor = Cursors.SizeNS;      //'南北
                            break;
                        case EnumMousePointPosition.MouseSizeTop:
                            if (rowindex > 0)
                                mainControl.Cursor = Cursors.SizeNS;      //'南北
                            break;
                        case EnumMousePointPosition.MouseSizeLeft:
                            if (colindex > 0)
                                mainControl.Cursor = Cursors.SizeWE;      //'东西
                            break;
                        case EnumMousePointPosition.MouseSizeRight:
                            if (colindex < colmax)
                                mainControl.Cursor = Cursors.SizeWE;      //'东西
                            break;
                        default:
                            break;
                    }
                }
                #endregion
               
            

        }

        
        #endregion

        private EnumMousePointPosition MousePointPosition(Size size, System.Windows.Forms.MouseEventArgs e)
        {

            if ((e.X >= -1 * Band) | (e.X <= size.Width) | (e.Y >= -1 * Band) | (e.Y <= size.Height))
            {
                if (e.X < Band)
                {
                    if (e.Y < Band) { return EnumMousePointPosition.MouseSizeTopLeft; }
                    else
                    {
                        if (e.Y > -1 * Band + size.Height)
                        { return EnumMousePointPosition.MouseSizeBottomLeft; }
                        else
                        { return EnumMousePointPosition.MouseSizeLeft; }
                    }
                }
                else
                {
                    if (e.X > -1 * Band + size.Width)
                    {
                        if (e.Y < Band)
                        { return EnumMousePointPosition.MouseSizeTopRight; }
                        else
                        {
                            if (e.Y > -1 * Band + size.Height)
                            { return EnumMousePointPosition.MouseSizeBottomRight; }
                            else
                            { return EnumMousePointPosition.MouseSizeRight; }
                        }
                    }
                    else
                    {
                        if (e.Y < Band)
                        { return EnumMousePointPosition.MouseSizeTop; }
                        else
                        {
                            if (e.Y > -1 * Band + size.Height)
                            { return EnumMousePointPosition.MouseSizeBottom; }
                            else
                            { return EnumMousePointPosition.MouseDrag; }
                        }
                    }
                }
            }
            else
            { return EnumMousePointPosition.MouseSizeNone; }
        }



    }
}
