using System;
using System.ComponentModel;
using System.Collections;
using System.Drawing;

using DevExpress.XtraReports;

using Webb.Reports.DataProvider;
using Webb.Reports.ExControls.Views;

namespace Webb.Reports.ExControls
{
	#region public class ContainerControl : ExControl
		/// <summary>
		/// Summary description for DateTimeControl.
		/// </summary>
		[XRDesigner("Webb.Reports.ExControls.Design.ContainerControlDesigner,Webb.Reports.ExControls"),
		Designer("Webb.Reports.ExControls.Design.ContainerControlDesigner,Webb.Reports.ExControls"),
		ToolboxBitmap(typeof(Webb.Reports.ExControls.ResourceManager), "Bitmaps.XRPanel.bmp")]
		public class ContainerControl : ExControl,INonePrintControl
		{
			public ContainerControlView ContainerControlView
			{
				get{return this.MainView as ContainerControlView;}
			}
		

			protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
			{
				//base.OnPaint(e);
				this._MainView.Paint(e);
			}

			public ContainerControl()
			{
				this._MainView = new ContainerControlView(this);
			}

			protected override void OnSizeChanged(EventArgs e)
			{
				base.OnSizeChanged (e);
			}

			public void ResetPrintingTable()
			{
				this._MainView.CreatePrintingTable();
			}

			public override void CreateArea(string areaName, DevExpress.XtraPrinting.IBrickGraphics graph)
			{
				base.CreateArea (areaName, graph);
			}

			public override void AutoAdjustSize()
			{
				base.AutoAdjustSize ();
			}
			public bool KeepDistance
			{
				get
				{
					return this.ContainerControlView.KeepDistance;
				}
				set
				{
                    this.ContainerControlView.KeepDistance=value;
				}
			}
		}

	#endregion

	#region public class PrintInfo
	public class PrintInfo
	{
		#region Auto Constructor By Macro 2009-3-9 8:58:27
		public PrintInfo()
		{
			this.Init();
		}
		public void Init()
		{
			_CtrlBounds=new Rectangle(-1,-1,-1,-1);
			_Left=-1;
			_Right=-1;
			_Top=-1;
			_Bottom=-1;
		}
		#endregion	
   
		
		private Rectangle _CtrlBounds;
		private int _Left,_Right,_Top,_Bottom;

		#region Property By Macro 2009-3-9 8:59:38
		public System.Drawing.Rectangle CtrlBounds
		{
			get{ return _CtrlBounds; }
			set{ _CtrlBounds = value; }
		}

		public int Left
		{
			get{ return _Left; }
			set{ _Left = value; }
		}

		public int Right
		{
			get{ return _Right; }
			set{ _Right = value; }
		}

		public int Top
		{
			get{ return _Top; }
			set{ _Top = value; }
		}

		public int Bottom
		{
			get{ return _Bottom; }
			set{ _Bottom = value; }
		}

		#endregion

		#region Copy Function By Macro 2009-3-9 8:59:39
		public PrintInfo Copy()
		{
			PrintInfo thiscopy=new PrintInfo();
			thiscopy._CtrlBounds=this._CtrlBounds;
			thiscopy._Left=this._Left;
			thiscopy._Right=this._Right;
			thiscopy._Top=this._Top;
			thiscopy._Bottom=this._Bottom;
			return thiscopy;
		}
		#endregion

		public void Update(ExControl i_Control,int left,int right,int top,int bottom)
	    {			
			_CtrlBounds=i_Control.XtraContainer.Bounds; 
			_Left=left;
			_Right=right;
			_Top=Math.Max(top,_Top);
			_Bottom=Math.Max(bottom,_Bottom);           
		}

	}
	#endregion

	#region class LinePrintInfo
	public class LinePrintInfo
	{
		private int _LineCtrlBottom=-1;
		private int _LastLineCtrlBottom=-1;
		private int _LastTop=-1;

		public int LineCtrlBottom
		{
			get{ return _LineCtrlBottom; }
			set{ _LineCtrlBottom = value; }
		}

		public int LastLineCtrlBottom
		{
			get{ return _LastLineCtrlBottom; }
			set{ _LastLineCtrlBottom = value; }
		}

		public int LastTop
		{
			get{ return _LastTop; }
			set{ _LastTop = value; }
		}
		public void Init()
		{
            _LineCtrlBottom=-1;
		    _LastLineCtrlBottom=-1;
		    _LastTop=-1;
		}
	}
	#endregion

	#region public class RectInfo
	public class RectInfo  //2009-3-6 11:27:57@Simon
	{
		private Rectangle _Rect;
		private PrintInfo _LastRect=new PrintInfo();
		private object _Tag;	
		private bool _KeepDistance=false;
		private LinePrintInfo _LinePrintInfo=new LinePrintInfo();  //2009-5-21 10:39:46@Simon Add this Code
		public ExShapeControl shapeControl=null;
	
		public bool KeepDistance
		{
			get{return this._KeepDistance;}
			set{this._KeepDistance=value;}
		}

        public LinePrintInfo LinePrintInfo
		{
			get{return _LinePrintInfo;}
			set{this._LinePrintInfo=value;}
		}
		public Rectangle Rect
		{
			get{return _Rect;}
			set{this._Rect=value;}
		}
		public object Tag
		{
			get{return this._Tag;}
			set{this._Tag=value;}
		}
	
		public int LastTop
		{
			get{return _LastRect.Top;}
			set{this._LastRect.Top=value;}
		}
		public int LastBottom
		{
			get{return _LastRect.Bottom;}
			set{this._LastRect.Bottom=value;}
		}
		public PrintInfo PrintInfo
		{
			get{return _LastRect;}
			set{this._LastRect=value;}
		}
		public RectInfo(ContainerControl control)
		{
             _Rect=control.XtraContainer.Bounds;
			
             _KeepDistance=control.KeepDistance;

			_LastRect.Init();

			LinePrintInfo.Init();
		}
		public RectInfo(ExShapeControl control)
		{
			_Rect=control.XtraContainer.Bounds;			
			
             shapeControl=control;

			_LastRect.Init();

			LinePrintInfo.Init();
		}
		public bool Contains(ExControl control)
		{
			Rectangle ctrlRect=control.XtraContainer.Bounds;

           if(ctrlRect.Left<_Rect.Left||ctrlRect.Right>_Rect.Right)return false;

           if(ctrlRect.Top<_Rect.Top||ctrlRect.Bottom>_Rect.Bottom)return false;

			return true;
		}
		public void UpdatePrintInfo(ExControl i_Control,int left,int right,int top,int bottom)
		{
           _LastRect.Update(i_Control,left,right,top,bottom);
		}
		public void UpdateLineInfo(ExControl m_Control,bool iNewLine)
		{
			if(iNewLine)
			{
				_LinePrintInfo.LastLineCtrlBottom=_LinePrintInfo.LineCtrlBottom;
						
				_LinePrintInfo.LineCtrlBottom=m_Control.XtraContainer.Bottom;
			}
			else
			{
                _LinePrintInfo.LineCtrlBottom=Math.Max(m_Control.XtraContainer.Bottom,_LinePrintInfo.LineCtrlBottom);;
			}
			
		}
	}
	#endregion

	#region public class RectInfoCollection
	/*Descrition:   */
	public class RectInfoCollection : CollectionBase,IComparer
	{
		//2009-3-6 11:27:30@Simon
		public RectInfo this[int i_Index]
		{ 
			get { return this.InnerList[i_Index] as RectInfo; } 
			set { this.InnerList[i_Index] = value; } 
		} 
		//ctor
		public RectInfoCollection() {} 
		//Methods
		public int Add(object i_Object)
		{
			return this.InnerList.Add(i_Object);
		} 
		public void Remove(RectInfo i_Obj)
		{
			this.InnerList.Remove(i_Obj);
		} 

		public void Sort()
		{
			this.InnerList.Sort(this);
		}

		public void Reverse()
		{
			this.InnerList.Reverse();
		}
		public void Init(WinControlContainerCollection wincontrols)
		{
			this.InnerList.Clear();

			foreach(DevExpress.XtraReports.UI.WinControlContainer m_Container in WebbReport.ExControls)
			{
				ContainerControl m_Control = m_Container.WinControl as ContainerControl;
                
				if(m_Control==null)continue;

				foreach(RectInfo rectInfo in this)
				{
					if(rectInfo.Contains(m_Control))continue;
				}
                  
				this.Add(new RectInfo(m_Control));   

			}
		}
		public void InitShapeControls(WinControlContainerCollection wincontrols,RepeatControlCollection repeatContains)
		{
			this.InnerList.Clear();

			foreach(DevExpress.XtraReports.UI.WinControlContainer m_Container in WebbReport.ExControls)
			{
				ExShapeControl m_Control = m_Container.WinControl as ExShapeControl;
                
				if(m_Control==null||!m_Control.AutoFit)continue;

				foreach(RectInfo rectInfo in this)
				{
					if(rectInfo.Contains(m_Control))continue;
				}

                if (repeatContains.HasContainShapeControl(m_Control)) continue;

				this.Add(new RectInfo(m_Control));   

			}
		}
		public int Indexof(ExControl m_Control)
		{
			for(int i=0;i<this.Count;i++)
			{
				if(this[i].Contains(m_Control))return i;
			}

			return -1;
		}
		public void UpdatePrintInfo(ExControl i_Control,int left,int right,int top,int bottom)
		{
			foreach(RectInfo rectInfo in this)
			{
				if(!rectInfo.Contains(i_Control))continue;

				rectInfo.UpdatePrintInfo(i_Control,left,right,top,bottom);				
			}		
		}
		public void UpdateLineInfo(ExControl i_Control,bool iNewLine)
		{
			foreach(RectInfo rectInfo in this)
			{
				if(!rectInfo.Contains(i_Control))continue;

				rectInfo.UpdateLineInfo(i_Control,iNewLine);
			}		
		}
		public void InitPrintInfo()
		{
			foreach(RectInfo rectInfo in this)
			{
				rectInfo.PrintInfo.Init();
				rectInfo.LinePrintInfo.Init();
			}		
		}

		#region IComparer Members
		public int Compare(object x, object y)
		{
			// TODO:  Add WinControlContainerCollection.Compare implementation
			RectInfo m_X = x as RectInfo;
			RectInfo m_Y = y as RectInfo;			
			//System.Diagnostics.Debug.Assert(m_X!=null&&m_Y!=null);
			if(m_X==null||m_Y==null) return 0;
			int m_Result = 0;
			if(m_X.Rect.Top==m_Y.Rect.Top)
			{
				m_Result = m_X.Rect.Left - m_Y.Rect.Left;
			}
			else
			{
				m_Result = m_X.Rect.Top - m_Y.Rect.Top;
			}
			return m_Result;
		}
		#endregion
	} 
	#endregion
}
