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
using System.Globalization;
using System.Security.Permissions;

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
using Webb.Collections;
using Webb.Reports.DataProvider;

namespace Webb.Reports.ExControls.Views
{
	/// <summary>
	/// Summary description for ContainerControlView
	/// </summary>
	[Serializable]
	public class RepeatControlView :ExControlView, ISerializable
	{
		public RepeatControlView(RepeatControl i_Control) : base(i_Control)
		{
			   _RepeatedWidth=0f;		
		      _RepeatedHeight=0f;
		       _RepeatedCount=3;		
		      _RepeatedVerticalCount=3; 
             _AfterLast=false;
		}

		public override void Paint(PaintEventArgs e)
		{
			string strText="Cut the excess area and repeat controls when printing"; 

			StringFormat sf = new StringFormat();

			sf.LineAlignment = StringAlignment.Center;

			sf.Alignment = StringAlignment.Center;

			e.Graphics.DrawString(strText,this.ExControl.Font,Brushes.Black,this.ExControl.ClientRectangle,sf);	
		}

		#region Fields & Properties
		   protected PageGroupInfo _RepeatSetting;
		   protected float _RepeatedWidth;		
		   protected float _RepeatedHeight;
		   protected int   _RepeatedCount;		
		   protected int  _RepeatedVerticalCount; 
           protected int  _RepeatTopCount=0;
		   protected Color _ChartColorWhenMax=Color.Empty;
		   protected bool _AfterLast=false;
		   
		
			public Webb.Reports.PageGroupInfo RepeatSetting
			{
				get{ 
					if(_RepeatSetting==null)_RepeatSetting=new PageFieldInfo("");
					  _RepeatSetting.RepeatTitle="RepeatControls";
					  return _RepeatSetting; }
				set{ _RepeatSetting = value; }
			}
			public bool AfterLast
			{
				get
				{return _AfterLast; }
				set{ _AfterLast = value; }
			}


			public float RepeatedWidth
			{
				get{ return _RepeatedWidth; }
				set{ _RepeatedWidth = value; }
			}

			public float RepeatedHeight
			{
				get{ return _RepeatedHeight; }
				set{ _RepeatedHeight = value; }
			}

			public int RepeatedCount
			{
				get{ return _RepeatedCount; }
				set{ _RepeatedCount = value; }
			}

			public int RepeatedVerticalCount
			{
				get{ return _RepeatedVerticalCount; }
				set{ _RepeatedVerticalCount = value; }
			}

			public int RepeatTopCount
			{
				get{ return _RepeatTopCount; }
				set{ _RepeatTopCount = value; }
			}

			public System.Drawing.Color ChartColorWhenMax
			{
				get{ return _ChartColorWhenMax; }
				set{ _ChartColorWhenMax = value; }
			}

		#endregion

		#region Serialization By Simon's Macro 2009-6-18 15:22:43
		public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			base.GetObjectData(info,context);
			info.AddValue("_RepeatSetting",_RepeatSetting,typeof(Webb.Reports.PageGroupInfo));
			info.AddValue("_RepeatedWidth",_RepeatedWidth);
			info.AddValue("_RepeatedHeight",_RepeatedHeight);
			info.AddValue("_RepeatedCount",_RepeatedCount);
			info.AddValue("_RepeatedVerticalCount",_RepeatedVerticalCount);
			info.AddValue("_RepeatTopCount",_RepeatTopCount);
			info.AddValue("_ChartColorWhenMax",_ChartColorWhenMax,typeof(System.Drawing.Color));
			info.AddValue("_AfterLast",this._AfterLast);
		
		}

		public RepeatControlView(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context):base(info,context)
		{
			try
			{
				_AfterLast=info.GetBoolean("_AfterLast");
			}
			catch
			{
				_AfterLast=false;
			}
			try
			{
				_RepeatSetting=(Webb.Reports.PageGroupInfo)info.GetValue("_RepeatSetting",typeof(Webb.Reports.PageGroupInfo));
			}
			catch
			{
				_RepeatSetting=new PageFieldInfo("");
			}
			try
			{
				_RepeatedWidth=info.GetSingle("_RepeatedWidth");
			}
			catch
			{
				_RepeatedWidth=240f;
			}
			try
			{
				_RepeatedHeight=info.GetSingle("_RepeatedHeight");
			}
			catch
			{
				_RepeatedHeight=240f;
			}
			try
			{
				_RepeatedCount=info.GetInt32("_RepeatedCount");
			}
			catch
			{
				_RepeatedCount=3;
			}
			try
			{
				_RepeatedVerticalCount=info.GetInt32("_RepeatedVerticalCount");
			}
			catch
			{
				_RepeatedVerticalCount=3;
			}
			try
			{
				_RepeatTopCount=info.GetInt32("_RepeatTopCount");
			}
			catch
			{
				_RepeatTopCount=0;
			}
			try
			{
				_ChartColorWhenMax=(System.Drawing.Color)info.GetValue("_ChartColorWhenMax",typeof(System.Drawing.Color));
			}
			catch
			{
				_ChartColorWhenMax=Color.Empty;
			}
		}
		#endregion

        public override void GetALLUsedFields(ref ArrayList usedFields)
        {
            this.RepeatSetting.GetAllUsedFields(ref usedFields);
        }
		
	}
}
