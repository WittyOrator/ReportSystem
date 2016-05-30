/***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:View.cs
 * Author:Country.Wu [EMail:Webb.Country.Wu@163.com]
 * Create Time:10/31/2007 03:11:26 PM
 * Copyright:1986-2007@Webb Electronics all right reserved.
 * Purpose:
 * ***********************************************************************/
#region History
/*
 * //Author@DateTime : Description
 * */
#endregion History

using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Collections.Specialized;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Serialization;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Container;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraPrinting;
using DevExpress.Utils.Design;
using DevExpress.Utils;

using Webb.Reports.ExControls;
using Webb.Reports.ExControls.Data;
using System.Security.Permissions;
using Webb.Collections;
using Webb.Reports.DataProvider;
using Webb.Reports.ExControls.Styles;

using System.ComponentModel.Design;
using System.ComponentModel;
using System.Drawing.Design;

namespace Webb.Reports.ExControls.Views
{
   

    #region public class ExControlView
    /*Descrition:   */	
	[Serializable]
	public class ExControlView:ISerializable
	{
		//Wu.Country@2007-11-09 01:36 PM added this class.
		//Fields
		[NonSerialized]
        internal ExControl ExControl;
		internal SectionFilterCollection _SectionFilters;
		internal SectionFilterCollectionWrapper _SectionFiltersWrapper;	//Modified at 2009-1-15 16:21:43@Scott
		internal BasicStyle MainStyle;
		public WebbTable PrintingTable;
		internal SectionFilter _OneValueScFilter;			//07-09-2008@Scott
		internal bool _ThreeD;	//Scott@12082008
		internal bool _Repeat;	//Modified at 2008-12-22 11:24:03@Scott

        internal SectionFilter _RepeatFilter;	//Added this code at 2008-12-26 11:05:35@Simon		
		public SectionFilter RepeatFilter
		{
			get
			{
				if(this._RepeatFilter == null) this._RepeatFilter = new SectionFilter();

				return this._RepeatFilter;
			}
			set{this._RepeatFilter = value;}
		}
		
		//Properties
		public bool Repeat
		{
			get{return this._Repeat;}
			set
			{
				if(this._Repeat != value)
				{
					this._Repeat = value;
				}
			}
		}

		public bool ThreeD	//Scott@12082008
		{
			get{return this._ThreeD;}
			set
			{
				this._ThreeD = value;
			}
		}

		public SectionFilter OneValueScFilter
		{
			get
			{
				if(this._OneValueScFilter == null) this._OneValueScFilter = new SectionFilter();

				return this._OneValueScFilter;
			}
			set{this._OneValueScFilter = value;}
		}
        public SectionFilterCollection SectionFilters
        {
            get
            {
                if (this._SectionFilters == null) this._SectionFilters = new SectionFilterCollection();

                return this._SectionFilters;
            }
            set { this._SectionFilters = value; }
        }
		//Modified at 2009-1-15 16:36:50@Scott
		public SectionFilterCollectionWrapper SectionFiltersWrapper
		{
			get
			{
				if(this._SectionFiltersWrapper == null) this._SectionFiltersWrapper = new SectionFilterCollectionWrapper();
				this._SectionFiltersWrapper.SectionFilters = this.SectionFilters;				
				return this._SectionFiltersWrapper;
			}
			set
			{
				this._SectionFiltersWrapper = value;
				
				_SectionFiltersWrapper.ReportScType=AdvFilterConvertor.GetScType(_SectionFiltersWrapper.ReportScType,DataProvider.VideoPlayBackManager.AdvSectionType);  //2009-6-16 10:18:47@Simon Add this Code
				
				this._SectionFiltersWrapper.UpdateSectionFilters();
				
				if(this._SectionFiltersWrapper.SectionFilters.Count > 0)	//Modified at 2009-2-6 10:51:42@Scott
				{
					this.SectionFilters = this._SectionFiltersWrapper.SectionFilters;
				}
			}
		}
		public bool OneValuePerPage	//07-07-2008@Scott
		{
			get
			{
				if(this.ExControl != null && this.ExControl.Report != null)
				{
					return this.ExControl.Report.Template.OneValuePerPage;
				}
				else
				{
					return false;
				}
			}
		}

		//ctor
		public ExControlView(ExControl i_Control)
		{
			this.ExControl = i_Control;

			this.MainStyle = new BasicStyle();

			this.ThreeD = false;

			this.Repeat = false;
		}
		//Methods
		public virtual bool CreatePrintingTable()
		{
			return false;
		}

		public virtual void CalculateResult(DataTable i_Table)
		{
			
		}

		virtual public void DrawContent(IGraphics gr, RectangleF rectf)	//Modified at 2009-1-6 15:28:17@Scott
		{
			
		}

        virtual public void GetALLUsedFields(ref ArrayList usedFields)
        {           
        }

		virtual public int CreateArea(string areaName, IBrickGraphics graph)
		{
			if(this.PrintingTable==null) return 0;

			if(this.ThreeD)
			{
				return this.PrintingTable.CreateArea3D(areaName,graph);	//Scott@12082008
			}
			else
			{
				return this.PrintingTable.CreateArea(areaName,graph);
			}
		}

		public virtual void SetOffset(int left,int top)
		{
			if(this.PrintingTable==null) return;
			this.PrintingTable.SetOffset(left,top);
		}

		virtual public void Paint(PaintEventArgs e)
		{
			StringFormat sf = new StringFormat();

			sf.LineAlignment = StringAlignment.Center;

			sf.Alignment = StringAlignment.Center;

			e.Graphics.DrawString(this.ExControl.Name,this.ExControl.Font,Brushes.Black,this.ExControl.ClientRectangle,sf);	//06-10-2008@Scott
		}

		public virtual void ClearPrintingTable()
		{
			if(this.PrintingTable!=null)
			{
				this.PrintingTable.Dispose();

				this.PrintingTable = null;
			}
		}

		public virtual void BuilderStyles(string i_StyleName)
		{
			
		}

		public virtual void UpdateView()
		{
			//if(this.ExControl==null) return;
			DataTable m_table = this.ExControl.GetDataSource();
			
			this.CalculateResult(m_table);
			
			this.CreatePrintingTable();
		}

		public void AdjustPrintTableSize()
		{
		}

		#region ISerializable Members
		[SecurityPermission(SecurityAction.Demand,SerializationFormatter=true)]
		virtual public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			// TODO:  Add ExControlView.GetObjectData implementation
			info.AddValue("MainStyle",this.MainStyle,typeof(BasicStyle));	
			info.AddValue("PrintingTable",this.PrintingTable,typeof(WebbTable));
			info.AddValue("OneValueScFilter",this.OneValueScFilter,typeof(SectionFilter));
			info.AddValue("ThreeD",this.ThreeD);
			info.AddValue("Repeat",this.Repeat);
		}

		public ExControlView(SerializationInfo info, StreamingContext context)
		{
			// TODO:  Add ExControlView.GetObjectData implementation
			this.MainStyle = info.GetValue("MainStyle",typeof(BasicStyle)) as BasicStyle;
			try
			{
				this.PrintingTable = info.GetValue("PrintingTable",typeof(WebbTable)) as WebbTable;
			}
			catch
			{
				this.PrintingTable = new WebbTable();	//scott@12022008
			}
			try
			{
				this.OneValueScFilter = info.GetValue("OneValueScFilter",typeof(SectionFilter)) as SectionFilter;
			}
			catch
			{
				
			}
			try
			{
				this.ThreeD = info.GetBoolean("ThreeD");
			}
			catch
			{
				this.ThreeD = false;	
			}
			try
			{
				this.Repeat = info.GetBoolean("Repeat");
			}
			catch
			{
				this.Repeat = false;
			}

		}
		#endregion
	}
	#endregion

	//09-08-2008@Scott add this class
	#region public class ExControlViewCollection
	[Serializable]
	public class ExControlViewCollection : CollectionBase
	{
		public ExControlView this[int i_Index]
		{ 
			get { return this.InnerList[i_Index] as ExControlView; } 
			set { this.InnerList[i_Index] = value; } 
		} 
		//ctor
		public ExControlViewCollection() {} 
		//Methods
		public int Add(ExControlView i_Object)
		{ 
			if(this.InnerList.Contains(i_Object)) return -1;
			return this.InnerList.Add(i_Object);
		} 
		public void Remove(ExControlView i_Obj)
		{
			this.InnerList.Remove(i_Obj);
		} 
	} 
	#endregion

	
   
    
}
