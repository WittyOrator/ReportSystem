/***********************************************************************
IDE:Microsoft Development Environment Ver:7.10
Module:ReportInfoLableView.cs
Author:Country.Wu [EMail:Webb.Country.Wu@163.com]
Create Time:11/29/2007 12:56:52 PM
Copyright:1986-2007@Webb Electronics all right reserved.
Purpose:
***********************************************************************/
#region History
/*
//Author@DateTime : Description
*/
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
using System.Globalization;

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

namespace Webb.Reports.ExControls.Views
{
	/// <summary>
	/// Summary description for ReportInfoLableView.
	/// </summary>
	//Scott@2007-11-27 13:40 modified some of the following code.
	[Serializable]
	public class ReportInfoLabelView : ExControlView,ISerializable
	{
		private LabelTypes _LabelType = LabelTypes.GameName;
		private Align _Align = Align.Horizontal;
		public bool _WordWrap = true;
		private bool _Multiline = false;
		private int _Width = 0;
		private int _Height = 0;
		private bool _Title = false;
		private bool _Object = true;
		private bool _FirstObjectOnly = false;		//05-27-2008@Scott
		private bool _Opponent = true;
		private bool _Date = true;
		private bool _Location = true;
		private bool _ScoutType = true;				//05-27-2008@Scott
		private Font _Font = new Font(AppearanceObject.DefaultFont.FontFamily, 8f);
		private Color _TextColor = Color.Black;
		private Color _BackColor = Color.Transparent;
		private string _Text;
		private string _DisplayText;	//05-13-2008
		private HorzAlignment _HorzAlignment = HorzAlignment.Default;
		private bool _OnceScoutType=false;  //added at 2008-10-22 13:58:10@Simon
     
		public bool OnceScoutType  //add this property at 2008-10-22 14:00:23@Simon
		{
			get{return this._OnceScoutType;}
			set{this._OnceScoutType = value;}
		}
		public string DisplayText
		{
			get{return this._DisplayText;}
			set{this._DisplayText = value;}
		}

		public string Text
		{
			get{return this._Text;}
			set{this._Text = value;}
		}

		public Font Font
		{
			get{return this._Font;}
			set{this._Font = value;}
		}

		public Color TextColor
		{
			get{return this._TextColor;}
			set{this._TextColor = value;}
		}

		public Color BackColor
		{
			get{return this._BackColor;}
			set{this._BackColor = value;}
		}

		public int Width
		{
			get{return this._Width;}
			set{this._Width = value;}
		}

		public int Height
		{
			get{return this._Height;}
			set{this._Height = value;}
		}

		public LabelTypes LabelType
		{
			get{return this._LabelType;}
			set
			{
				if(this._LabelType == value) return;

				this._LabelType = value;
			}
		}

		public Align Align
		{
			get{return this._Align;}
			set
			{
				if(this._Align == value) return;

				this._Align = value;
			}
		}

		public bool Multiline
		{
			get{return this._Multiline;}
			set{this._Multiline = value;}
		}

		public bool WordWrap
		{
			get{return this._WordWrap;}
			set{this._WordWrap = value;}
		}

		public bool Title
		{
			get{return this._Title;}
			set{this._Title = value;}
		}

		public bool Object
		{
			get{return this._Object;}
			set{this._Object = value;}
		}

		public bool FirstObjectOnly
		{
			get{return this._FirstObjectOnly;}
			set{this._FirstObjectOnly = value;}
		}

		public bool Opponent
		{
			get{return this._Opponent;}
			set{this._Opponent = value;}
		}

		public bool Date
		{
			get{return this._Date;}
			set{this._Date = value;}
		}

		public bool Location
		{
			get{return this._Location;}
			set{this._Location = value;}
		}

		public bool ScoutType
		{
			get{return this._ScoutType;}
			set{this._ScoutType = value;}
		}

		public HorzAlignment HorzAlignment
		{
			get{return this._HorzAlignment;}
			set{this._HorzAlignment = value;}
		}

		public ReportInfoLabelView(ReportInfoLabel i_Control):base(i_Control as ExControl)
		{
			this.CreatePrintingTable();
		}

		private WebbDataSource GetWebbDataSource()
		{
			if(this.ExControl == null) return null;

			if(this.ExControl.Report == null) return null;

			WebbReport m_WebbReport = this.ExControl.Report as WebbReport;

			if(m_WebbReport == null) return null;

			return m_WebbReport.WebbDataSource;
		}

		private string GetVersionString()
		{	
			string version=Webb.Assembly.Version;

			if(version.Length>7)
			{
				string Ver=Webb.Assembly.Version.Substring(0,7);

				string InternalVer=Webb.Assembly.Version.Remove(0,7);

				if(InternalVer.Length!=1)
				{
					version ="Internal "+ Ver + "." + InternalVer;
				}
				else
				{
					int number=int.Parse(InternalVer);

					char a=(char)((short)'a'+number);

					version=Ver+a.ToString();
				}
			}
			return version;  
		}

		private string TranslateCustomText()
		{
			this.DisplayText = this.Text;

			if(this.DisplayText != null)
			{
				this.DisplayText = this.DisplayText.Replace(@"[version]", GetVersionString());

				this.DisplayText = this.DisplayText.Replace(@"[date]",System.DateTime.Now.ToString("d",DateTimeFormatInfo.InvariantInfo));

				this.DisplayText = this.DisplayText.Replace(@"[time]",System.DateTime.Now.ToString("T",DateTimeFormatInfo.InvariantInfo));
			}

			return this.DisplayText;
		}

		public string GetReportInfoText()
		{
			if(this._LabelType == LabelTypes.Custom) 
			{
				return this.TranslateCustomText();
			}

			WebbDataSource m_DataSource = this.GetWebbDataSource();

			if(m_DataSource == null || this.ExControl.GetDataSource() == null) return "<No Data Source>";

			switch(this._LabelType)
			{
				case LabelTypes.GameName:

					this.Text = MakeGameInfoString(m_DataSource.Games,m_DataSource.EdlInfos,this._Multiline);

					break;

				case LabelTypes.FilterName:

					this.Text = MakeFilterInfoString(m_DataSource.Filters, this._Multiline);
					
					break;

				case LabelTypes.Both:

					this.Text = MakeGameFilterString(m_DataSource.Games,m_DataSource.EdlInfos, m_DataSource.Filters, this._Multiline);

					break;

				default:

					break;
			}

			//Modified at 2008-10-22 14:48:06@Simon
			while(this.Text.EndsWith(" ")||this.Text.EndsWith(","))
			{
				this.Text=this.Text.Trim(new char[]{' ',','});
			}    //End modify
			return this.Text;
		}
        public void MoveDown(int increaseHeight)
        {          
            this.ExControl.XtraContainer.Top+=increaseHeight;
        }
        #region Modeified codes at 2011-01-04

        #region Old
        //        private string MakeGameInfoString(GameInfoCollection i_Games, bool i_MultiLine)
//        {
//            string m_RetStr = "<No Game>";

//            if(i_Games == null || i_Games.Count == 0) return m_RetStr;

//            StringBuilder m_SBGames = new StringBuilder();

//            if(this._Title)
//            {
//                m_SBGames.AppendFormat("{0}","Games:\n");
//            }

//            int times = 0;

//            int scouttypetimes=0 , maxtime=i_Games.Count-1;  //added at 2008-10-22 14:24:25@Simon

//            foreach(GameInfo i_Game in i_Games)
//            {
//                if((i_Game.Object == null || i_Game.Object == string.Empty)
//                    &&(i_Game.Opponent == null || i_Game.Opponent == string.Empty))
//                {
//                    //if obj and opponent is null , add game name
//                    if(i_Game.GameName == null || i_Game.GameName == string.Empty)
//                    {
//                        m_SBGames.Append("<None>");
//                    }
//                    else
//                    {
//                        m_SBGames.AppendFormat("{0}",i_Game.GameName);
//                    }
//                }
//                else
//                {
//                    //format game name
//                    if(!this.FirstObjectOnly || times == 0)
//                    {
//                        if(this._Object)
//                        {
//                            m_SBGames.AppendFormat("{0} ",i_Game.Object);
//                        }

//                        m_SBGames.Append("VS");

//                        times++;
//                    }

//                    if(this._Opponent)
//                    {
//                        m_SBGames.AppendFormat(" {0} ",i_Game.Opponent);
//                    }
					
//                    if(this._Location)
//                    {
//                        m_SBGames.AppendFormat("AT {0} ",i_Game.Location);
//                    }

//                    if(this._Date)
//                    {
//                        m_SBGames.AppendFormat("ON {0} ",i_Game.GameDate);
//                    }

//                    if(this._ScoutType)
//                    {
////						m_SBGames.AppendFormat("- {0} ",i_Game.ScoutType);
//                        #region Modified Area  at 2008-10-22 14:32:22@Simon
//                        //add his code for show Scout Type once
//                        if(!this.OnceScoutType||scouttypetimes==maxtime)
//                        {
//                            m_SBGames.AppendFormat("- {0} ",i_Game.ScoutType);						 
//                        }
//                        scouttypetimes++;

//                        #endregion        //End Modify
//                    }
//                }

//                if(i_MultiLine)
//                {
//                    m_SBGames.AppendFormat("\n");
//                }
//                else
//                {
//                    #region Modified Area 2008-10-27 9:19:25@Simon
////					m_SBGames.AppendFormat(",");
//                    if(!this.FirstObjectOnly)
//                    {
//                        m_SBGames.AppendFormat(",");
//                    }
//                    #endregion        //End Modify 
//                }
//            }
//            m_SBGames.Length--;	
			

//            return m_SBGames.ToString();
//        }

//        private string MakeGameFilterString(GameInfoCollection i_Games, FilterInfoCollection i_Filters, bool i_MultiLine)
//        {
//            StringBuilder m_SBGameFilter = new StringBuilder();

//            string m_StrGames = MakeGameInfoString(i_Games, i_MultiLine);

//            string m_StrFilters = MakeFilterInfoString(i_Filters, i_MultiLine);

//            m_SBGameFilter.AppendFormat("{0} \n\n{1}", m_StrGames, m_StrFilters);

//            return m_SBGameFilter.ToString();
//        }

        #endregion

        #region New
        private string MakeGameInfoString(GameInfoCollection i_Games,EdlInfoCollection i_EdlInfos,bool i_MultiLine)
        {
            string m_RetStr = "<No Game>";

            if ((i_Games == null || i_Games.Count == 0)&&(i_EdlInfos==null||i_EdlInfos.Count==0)) return m_RetStr;

            if (i_Games == null)i_Games=new GameInfoCollection();

            if(i_EdlInfos==null)i_EdlInfos=new EdlInfoCollection();

            StringBuilder m_SBGames = new StringBuilder();

            if (this._Title)
            {
                m_SBGames.AppendFormat("{0}", "Games :\n");
            }

            int times = 0;

            int scouttypetimes = 0, maxtime = i_Games.Count - 1;  //added at 2008-10-22 14:24:25@Simon

            foreach (GameInfo i_Game in i_Games)
            {
                if ((i_Game.Object == null || i_Game.Object == string.Empty)
                    && (i_Game.Opponent == null || i_Game.Opponent == string.Empty))
                {
                    //if obj and opponent is null , add game name
                    if (i_Game.GameName == null || i_Game.GameName == string.Empty)
                    {
                        m_SBGames.Append("<None>");
                    }
                    else
                    {
                        m_SBGames.AppendFormat("{0}", i_Game.GameName);
                    }
                }
                else
                {
                    #region Format Game Display only some parts of game Name
                    //format game name
                    if (!this.FirstObjectOnly || times == 0)
                    {
                        if (this._Object)
                        {
                            m_SBGames.AppendFormat("{0} ", i_Game.Object);
                        }

                        m_SBGames.Append("VS");

                        times++;
                    }

                    if (this._Opponent)
                    {
                        m_SBGames.AppendFormat(" {0} ", i_Game.Opponent);
                    }

                    if (this._Location)
                    {
                        m_SBGames.AppendFormat("AT {0} ", i_Game.Location);
                    }

                    if (this._Date)
                    {
                        m_SBGames.AppendFormat("ON {0} ", i_Game.GameDate);
                    }

                    if (this._ScoutType)
                    {
                        //						m_SBGames.AppendFormat("- {0} ",i_Game.ScoutType);
                        #region Modified Area  at 2008-10-22 14:32:22@Simon
                        //add his code for show Scout Type once
                        if (!this.OnceScoutType || scouttypetimes == maxtime)
                        {
                            m_SBGames.AppendFormat("- {0} ", i_Game.ScoutType);
                        }
                        scouttypetimes++;

                        #endregion        //End Modify
                    }
                    #endregion
                }

                if (i_MultiLine)
                {
                    m_SBGames.AppendFormat("\n");
                }
                else
                {
                    #region Modified Area 2008-10-27 9:19:25@Simon
                    //					m_SBGames.AppendFormat(",");
                    if (!this.FirstObjectOnly)
                    {
                        m_SBGames.AppendFormat(",");
                    }
                    #endregion        //End Modify
                }
            }
            if (!i_MultiLine && this.FirstObjectOnly && maxtime > 0)
            {
                m_SBGames.AppendFormat(",");
            }
            foreach (EdlInfo edlInfo in i_EdlInfos)
            {
                m_SBGames.AppendFormat("{0} ", edlInfo.EDLName);

                if (i_MultiLine)
                {
                    m_SBGames.AppendFormat("\n");
                }
                else
                {
                    m_SBGames.AppendFormat(",");                   
                }
            }            

            m_SBGames.Length--;

            return m_SBGames.ToString();
        }

        private string MakeGameFilterString(GameInfoCollection i_Games, EdlInfoCollection i_EdlInfos, FilterInfoCollection i_Filters, bool i_MultiLine)
        {
            StringBuilder m_SBGameFilter = new StringBuilder();

            string m_StrGames = MakeGameInfoString(i_Games,i_EdlInfos, i_MultiLine);

            string m_StrFilters = MakeFilterInfoString(i_Filters, i_MultiLine);

            m_SBGameFilter.AppendFormat("{0} \n\n{1}", m_StrGames, m_StrFilters);

            return m_SBGameFilter.ToString();
        }

        #endregion
       #endregion


        private string MakeFilterInfoString(FilterInfoCollection i_Filters, bool i_MultiLine)
		{
			string m_RetStr = "<No Filter>";

			if(i_Filters == null || i_Filters.Count == 0) return m_RetStr;

			StringBuilder m_SBFilters = new StringBuilder();

			if(this._Title)
			{
				m_SBFilters.AppendFormat("{0}","Filters:\n");
			}

			foreach(FilterInfo i_Filter in i_Filters)
			{
				m_SBFilters.AppendFormat("{0} ",i_Filter.FilterName);

				if(i_MultiLine)
				{
					m_SBFilters.AppendFormat("\n");
				}
				else
				{
					m_SBFilters.AppendFormat(",");
				}
			}
			m_SBFilters.Length--;

			string strRet = m_SBFilters.ToString();

			strRet=strRet.Trim(" ,\n".ToCharArray());  //2009-6-11 16:27:11@Simon Add this Code

			if(strRet== string.Empty)
			{
				return m_RetStr;
			}
			
			return strRet;
		}

	
		private string MakeEmptyString()
		{
			return string.Empty;
		}

		public override bool CreatePrintingTable()
		{
			if(this.PrintingTable == null)
			{
				this.PrintingTable = new WebbTable(1,1);
			}

			IWebbTableCell m_Cell = this.PrintingTable.GetCell(0,0);

			this.SetCellStyle(m_Cell);

			return true;
		}

		private void SetCellStyle(IWebbTableCell cell)
		{
			cell.CellStyle.HorzAlignment = this.HorzAlignment;
 
			cell.CellStyle.HorzAlignment = HorzAlignment.Near;

			cell.CellStyle.VertAlignment = VertAlignment.Top;

			cell.CellStyle.Font = this._Font;

			cell.CellStyle.ForeColor = this.TextColor;

			cell.CellStyle.BackgroundColor = this.BackColor;

			cell.CellStyle.BorderWidth = 0;

			cell.CellStyle.BorderColor = Color.Transparent;

			cell.Text = this.GetReportInfoText();  
			
			SizeF m_Size = this.MeasureSize(cell);

			cell.CellStyle.Width = (int)m_Size.Width;

			cell.CellStyle.Height = (int)m_Size.Height;
		}

		public void SetSize(int i_Width, int i_Height)
		{
			this._Width = i_Width;

			this._Height = i_Height;
		
			IWebbTableCell m_Cell = this.PrintingTable.GetCell(0,0);

			SizeF m_Size = this.MeasureSize(m_Cell);

			m_Cell.CellStyle.Width = (int)m_Size.Width;

			m_Cell.CellStyle.Height = (int)m_Size.Height;
		}

		public SizeF MeasureSize(IWebbTableCell i_Cell)
		{
			Graphics m_Graph = this.ExControl.CreateGraphics();

			SizeF m_Size = SizeF.Empty;

			if(this.WordWrap)
			{
				m_Size = m_Graph.MeasureString(i_Cell.Text,i_Cell.CellStyle.Font,this.Width);
			}
			else
			{
				m_Size = m_Graph.MeasureString(i_Cell.Text,i_Cell.CellStyle.Font);
			}

			m_Size.Width += 5;

			m_Size.Height += 5;

			return m_Size;
		}

		public override void Paint(PaintEventArgs e)
		{//Modified at 2009-1-7 14:30:20@Scott
//			if(this.PrintingTable != null)
//			{
//				this.PrintingTable.PaintTable(e,true,Rectangle.Empty);
//			}
//			else
//			{
//				base.Paint (e);
//			}
		}

		//Modified at 2009-1-7 14:27:57@Scott
		public override void DrawContent(IGraphics gr, RectangleF rectf)
		{
			base.DrawContent (gr, rectf);

			if(this.PrintingTable != null)
			{
				WebbTableCell m_Cell = this.PrintingTable.GetCell(0,0) as WebbTableCell;

				if(m_Cell != null)
				{
					m_Cell.DrawContent(gr,rectf);
				}
			}
		}

		public override int CreateArea(string areaName, IBrickGraphics graph)
		{
			if(PrintingTable!=null)
			{
				this.PrintingTable.ExControl=this.ExControl;
			}
			return base.CreateArea (areaName, graph);
		}

		#region ISerializable Members
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData (info, context);

			info.AddValue("LabelType",this.LabelType,typeof(LabelTypes));

			info.AddValue("Multiline",this.Multiline);
			
			info.AddValue("Height",this.Height);
			
			info.AddValue("Width",this.Width);
			
			info.AddValue("Date",this.Date);
			
			info.AddValue("Location",this.Location);
			
			info.AddValue("Title",this.Title);

			info.AddValue("Font",this.Font,typeof(Font));

			info.AddValue("TextColor",this.TextColor,typeof(Color));

			info.AddValue("BackColor",this.BackColor,typeof(Color));

			info.AddValue("Text",this.Text);

			info.AddValue("Object",this.Object);

			info.AddValue("Opponent",this.Opponent);
			
			info.AddValue("WordWrap",this.WordWrap);

			info.AddValue("HorzAlignment",this.HorzAlignment,typeof(HorzAlignment));

			info.AddValue("FirstObjectOnly",this.FirstObjectOnly);

			info.AddValue("ScoutType",this.ScoutType);
		}

		public ReportInfoLabelView(SerializationInfo info, StreamingContext context):base(info,context)
		{
			try
			{
				this.LabelType = (LabelTypes)info.GetValue("LabelType",typeof(LabelTypes));
			}
			catch
			{
				this.LabelType = LabelTypes.GameName;
			}

			try
			{
				this.Multiline = info.GetBoolean("Multiline");
			}
			catch
			{
				this.Multiline = false;
			}

			try
			{
				this.Height = info.GetInt32("Height");
			}
			catch
			{
				this.Height = 0;
			}

			try
			{
				this.Width = info.GetInt32("Width");
			}
			catch
			{
				this.Width = 0;
			}

			try
			{
				this.Date = info.GetBoolean("Date");
			}
			catch
			{
				this.Date = true;
			}

			try
			{
				this.Location = info.GetBoolean("Location");
			}
			catch
			{
				this.Location = true;
			}

			try
			{
				this.Title = info.GetBoolean("Title");
			}
			catch
			{
				this.Title = false;;
			}

			try
			{
				this.Font = info.GetValue("Font",typeof(Font)) as Font;
			}
			catch
			{
				this.Font = new Font(AppearanceObject.DefaultFont.FontFamily, 8f);
			}

			try
			{
				this.TextColor = (Color)info.GetValue("TextColor",typeof(Color));
			}
			catch
			{
				this.TextColor = Color.Black;
			}

			try
			{
				this.BackColor = (Color)info.GetValue("BackColor",typeof(Color));
			}
			catch
			{
				this.BackColor = Color.Transparent;
			}

			try
			{
				this.Text = info.GetString("Text");
			}
			catch
			{
				this.Text = string.Empty;
			}

			try
			{
				this.Object = info.GetBoolean("Object");
			}
			catch
			{
				this.Object = true;
			}

			try
			{
				this.Opponent = info.GetBoolean("Opponent");
			}
			catch
			{
				this.Opponent = true;
			}

			try
			{
				this.WordWrap = info.GetBoolean("WordWrap");
			}
			catch
			{
				this.WordWrap = true;
			}

			try
			{
				this.HorzAlignment = (HorzAlignment)info.GetValue("HorzAlignment",typeof(HorzAlignment));
			}
			catch
			{
				this.HorzAlignment = HorzAlignment.Default;
			}

			try
			{
				this.FirstObjectOnly = info.GetBoolean("FirstObjectOnly");
			}
			catch
			{
				this.FirstObjectOnly = false;
			}
			try
			{
				this.ScoutType = info.GetBoolean("ScoutType");
			}
			catch
			{
				this.ScoutType = true;
			}
		}

		#endregion
	}
}
