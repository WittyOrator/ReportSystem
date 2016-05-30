 /***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:RepeatControl.cs
 * Author:Automatic Macro@simon
 * Create Time:2009-6-18 15:26:46
 * Copyright:1986-2009@Webb Electronics all right reserved.
 * Purpose:
 * ***********************************************************************/
using System;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Container;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraPrinting;
using DevExpress.Utils.Design;
using DevExpress.Utils;
using DevExpress.XtraReports;
using DevExpress.XtraReports.UI;
using Webb.Reports.ExControls.Views;

namespace Webb.Reports.ExControls
{
	[XRDesigner("Webb.Reports.ExControls.Design.RepeatControlDesigner,Webb.Reports.ExControls"),
	Designer("Webb.Reports.ExControls.Design.RepeatControlDesigner,Webb.Reports.ExControls"),
	ToolboxBitmap(typeof(Webb.Reports.ExControls.ResourceManager), "Bitmaps.RepeatControl.bmp")]
	public class RepeatControl : ExControl,INonePrintControl
	{
		public RepeatControl()
		{
			this._MainView = new RepeatControlView(this);
		}

		public RepeatControlView RepeatControlView
		{
			get{ return this._MainView as RepeatControlView;}
			
		}

		override protected void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			this._MainView.Paint(e);
		
		}

		[EditorAttribute(typeof(Webb.Reports.Editors.PageGroupEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public Webb.Reports.PageGroupInfo RepeatSetting
		{
			get{ return this.RepeatControlView.RepeatSetting; }
			set
			{
			     this.RepeatControlView.RepeatSetting=value;			     
			}
		}
		public bool AfterLast
		{
			get
			{
				return RepeatControlView.AfterLast;
			}
			set{ RepeatControlView.AfterLast = value; }
		}

		public float RepeatedWidth
		{
			get{ return this.RepeatControlView.RepeatedWidth; }
			set
			{
			     this.RepeatControlView.RepeatedWidth=value;

			     if(DesignMode) this.RepeatControlView.UpdateView();
			}
		}

		public float RepeatedHeight
		{
			get{ return this.RepeatControlView.RepeatedHeight; }
			set
			{
			     this.RepeatControlView.RepeatedHeight=value;
			     
			}
		}

		public int RepeatedCount
		{
			get{ return this.RepeatControlView.RepeatedCount; }
			set
			{
			     this.RepeatControlView.RepeatedCount=value;
			    
			}
		}

		public int RepeatedVerticalCount
		{
			get{ return this.RepeatControlView.RepeatedVerticalCount; }
			set
			{
			     this.RepeatControlView.RepeatedVerticalCount=value;
			    
			}
		}

		public int RepeatTopCount
		{
			get{ return this.RepeatControlView.RepeatTopCount; }
			set
			{
			     this.RepeatControlView.RepeatTopCount=value;
			}
		}

		public System.Drawing.Color ChartColorWhenMax
		{
			get{ return this.RepeatControlView.ChartColorWhenMax; }
			set
			{
			     this.RepeatControlView.ChartColorWhenMax=value;			    
			}
		}

		public bool HasContain(ExControl control)
		{
			Rectangle ctrlRect=control.XtraContainer.Bounds;

			if(ctrlRect.Left<this.XtraContainer.Left||ctrlRect.Right>XtraContainer.Right)return false;

			if(ctrlRect.Top<XtraContainer.Top||ctrlRect.Bottom>XtraContainer.Bottom)return false;

			return true;
		}
        public bool HasContainShapeControl(ExShapeControl control)
        {
            foreach (RectInfo rectInfo in this.SubShapeContainers)
            {
                if (rectInfo.shapeControl == control) return true;
            }
            return false;
        }


		[NonSerialized]
		private ExControlCollection _subControls=new ExControlCollection();
        [Browsable(false)]
        public ExControlCollection SubControls
		{
			get{
				if(_subControls==null)_subControls=new ExControlCollection();
				return this._subControls; }
			set
			{
			     this._subControls=value;			    
			}
		}
        [NonSerialized]
        private RectInfoCollection _subShapeContainers = new RectInfoCollection();
        [Browsable(false)]
        public RectInfoCollection SubShapeContainers
        {
            get
            {
                if (_subShapeContainers == null) _subShapeContainers = new RectInfoCollection();
                return this._subShapeContainers;
            }
            set
            {
                this._subShapeContainers = value;
            }
        }

	}

	#region public class RepeatControlCollection
	/*Descrition:   */
	[Serializable]
	public class RepeatControlCollection : CollectionBase
	{		
		//Fields
		//Properties
		public RepeatControl this[int i_Index]
		{ 
			get { return this.InnerList[i_Index] as RepeatControl; } 
			set { this.InnerList[i_Index] = value; } 
		} 
		//ctor
		public RepeatControlCollection() {} 
		//Methods
		public int Add(RepeatControl i_Object)
		{ 
			if(this.HasContainControl(i_Object)) return -1;
			return this.InnerList.Add(i_Object);
		} 
		public void Remove(RepeatControl i_Obj)
		{
			this.InnerList.Remove(i_Obj);
		} 
		public bool HasContainControl(ExControl control)
		{
			foreach(RepeatControl R_Control in this)
			{
				if(R_Control.HasContain(control))return true;
			}
			return false;
		}
        public bool HasContainShapeControl(ExShapeControl  control)
        {
            foreach (RepeatControl R_Control in this)
            {
                if (R_Control.HasContainShapeControl(control)) return true;
            }
            return false;
        }


		public void Init(WinControlContainerCollection wincontrols)
		{
			this.InnerList.Clear();

			foreach(DevExpress.XtraReports.UI.WinControlContainer m_Container in WebbReport.ExControls)
			{
				RepeatControl m_Control = m_Container.WinControl as RepeatControl;
				                
				if(m_Control==null||this.HasContainControl(m_Control))continue;	
                  
				this.InnerList.Add(m_Control);   

			}

			foreach(RepeatControl control in this)
			{	
				float maxValue=-1;

                control.SubControls.Clear();

                control.SubShapeContainers.Clear();

				foreach(DevExpress.XtraReports.UI.WinControlContainer winControl in WebbReport.ExControls)
				{
                    ExControl  m_Control= winControl.WinControl as ExControl;

					if(m_Control==null||!control.HasContain(m_Control)||m_Control is RepeatControl)continue;

                    if((m_Control is ExShapeControl)&&(m_Control as ExShapeControl).AutoFit)
                    {
                       control.SubShapeContainers.Add(new RectInfo(m_Control as ExShapeControl));
                    }
                    else
                    {
					    control.SubControls.Add(m_Control);

					    if(m_Control is ChartControlEx&&!m_Control.Repeat)
					    {
						    ChartControlEx chartControl=m_Control as ChartControlEx;

						    chartControl.MaxValuesWhenTop=-1;

						    float maxchartValue=chartControl.GetMaxDatapoint();

						    maxValue=Math.Max(maxchartValue,maxValue);
					    }	
                   }
				}
				if(maxValue<0)continue;
                
				foreach(ExControl m_Control in control.SubControls)
				{
					if(!(m_Control is ChartControlEx)||m_Control.Repeat)continue;

					(m_Control as ChartControlEx).MaxValuesWhenTop=maxValue;					
				}
			}
		}
	} 
	#endregion
}
