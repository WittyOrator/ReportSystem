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
	public class ContainerControlView : ExControlView,ISerializable
	{	
	
		private string _Field = string.Empty;	//Modified at 2008-12-15 13:28:56@Scott	
		private bool _keepDistance=false;
		

		public ContainerControlView(ContainerControl i_Control):base(i_Control as ExControl)
		{
           
		}
		


		private WebbDataSource GetWebbDataSource()
		{
			if(this.ExControl == null) return null;

			if(this.ExControl.Report == null) return null;

			WebbReport m_WebbReport = this.ExControl.Report as WebbReport;

			if(m_WebbReport == null) return null;

			return m_WebbReport.WebbDataSource;
		}

		public override void CalculateResult(DataTable i_Table)
		{
		}

		public override bool CreatePrintingTable()
		{
			return false;
		}

	
		public override void Paint(PaintEventArgs e)
		{
     		string strText="Place controls here to keep them together and send them to back when printing"; 

			StringFormat sf = new StringFormat();

			sf.LineAlignment = StringAlignment.Center;

			sf.Alignment = StringAlignment.Center;

			e.Graphics.DrawString(strText,this.ExControl.Font,Brushes.Black,this.ExControl.ClientRectangle,sf);	
		}
		public override void DrawContent(IGraphics gr, RectangleF rectf)
		{
			base.DrawContent (gr, rectf);
		}

		public override int CreateArea(string areaName, IBrickGraphics graph)
		{
			return base.CreateArea (areaName, graph);
		}		

		public bool KeepDistance
		{
			get{ return _keepDistance; }
			set{ _keepDistance = value; }
		}

		#region Serialization By Simon's Macro 2009-5-21 9:14:19
		public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			base.GetObjectData(info,context);

			info.AddValue("_keepDistance",_keepDistance);		
		}

		public ContainerControlView(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context):base(info,context)
		{			
			try
			{
				_keepDistance=info.GetBoolean("_keepDistance");
			}
			catch
			{
				_keepDistance=false;
			}
		}
		#endregion

	}
}