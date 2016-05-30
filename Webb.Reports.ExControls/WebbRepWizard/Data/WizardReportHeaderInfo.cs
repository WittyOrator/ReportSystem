using System;
using System.Text;
using System.Drawing;
using System.Data;
using System.Collections;
using System.Runtime.Serialization;
using System.Windows.Forms;
using Webb;
using Webb.Reports.ExControls;
using Webb.Reports;
using Webb.Reports.DataProvider;
using Webb.Reports.ExControls.Data;
using System.IO;
using System.ComponentModel;

namespace Webb.Reports.ReportWizard.WizardInfo
{
	/// <summary>
	/// Summary description for ReportHeaderInfo.
	/// </summary>
	#region HeaderStyle
	[Serializable]
	public class ReportHeaderStyle:ISerializable
	{
		public ReportHeaderStyle(string _StyleName)
		{
			styleName=_StyleName;
		}

		private string styleName=string.Empty;
		private Font font=new Font("Tahoma",18,FontStyle.Bold);
		private Color textColor=Color.Black;
		private Color backColor=Color.LightSkyBlue;
		protected bool showBorder=false;

		public override string ToString()
		{
			return StyleName;
		}

	    [Category("StyleName")]
		public string StyleName
		{
			get{if(styleName==null)styleName=string.Empty;
				return styleName; }
			set{ styleName = value; }
		}
        [Category("Main")]
		public System.Drawing.Font Font
		{
			get{ return font; }
			set{ font = value; }
		}
         [Category("Main")]
		public System.Drawing.Color TextColor
		{
			get{ return textColor; }
			set{ textColor = value; }
		}
        [Category("Main")]
		public System.Drawing.Color BackColor
		{
			get{ return backColor; }
			set{ backColor = value; }
		}
		[Category("Main")]
		public bool ShowBorder
		{
			get{ return showBorder; }
			set{ showBorder = value; }
		}

		

		#region Copy Function By Macro 2010-2-9 15:23:55
		public ReportHeaderStyle Copy()
		{
			ReportHeaderStyle thiscopy=new ReportHeaderStyle(this.styleName);
			thiscopy.styleName=this.styleName;
			thiscopy.font=(Font)this.font.Clone();
			thiscopy.textColor=this.textColor;
			thiscopy.backColor=this.backColor;
			thiscopy.showBorder=this.showBorder;
			return thiscopy;
		}
		#endregion

		#region Serialization By Simon's Macro 2010-2-10 15:12:00
		public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			info.AddValue("styleName",styleName);
			info.AddValue("font",font,typeof(System.Drawing.Font));
			info.AddValue("textColor",textColor,typeof(System.Drawing.Color));
			info.AddValue("backColor",backColor,typeof(System.Drawing.Color));
			info.AddValue("showBorder",showBorder);
		
		}

		public ReportHeaderStyle(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			try
			{
				styleName=info.GetString("styleName");
			}
			catch
			{
				styleName=string.Empty;
			}
			try
			{
				font=(System.Drawing.Font)info.GetValue("font",typeof(System.Drawing.Font));
			}
			catch
			{
				font=new Font("Tahoma",18,FontStyle.Bold);
			}
			try
			{
				textColor=(System.Drawing.Color)info.GetValue("textColor",typeof(System.Drawing.Color));
			}
			catch
			{
				textColor=Color.Black;
			}
			try
			{
				backColor=(System.Drawing.Color)info.GetValue("backColor",typeof(System.Drawing.Color));
			}
			catch
			{
				backColor=Color.LightSkyBlue;
			}
			try
			{
				showBorder=info.GetBoolean("showBorder");
			}
			catch
			{
				showBorder=false;
			}
		}
		#endregion
	}

	[Serializable]
	public class HeaderStyleCollection:Webb.Collections.WebbCollection
	{
		public const string FileExtesion=".hsr";

		public HeaderStyleCollection():base()
		{
		}
		public ReportHeaderStyle this[int index]
		{
			get{return this.innerList[index] as ReportHeaderStyle;}

			set{this.innerList[index]=value;}
		}
		public ReportHeaderStyle this[string stylename]
		{
			get
			{
				foreach(ReportHeaderStyle styles in this)
				{
					if(styles.StyleName==stylename)return styles;
				}

				return null;		
			}		
		}
		

		public bool DeleteStyle(string stylename)
		{
			ReportHeaderStyle style=this[stylename];

			if(style==null)return false;

			innerList.Remove(style);

			return true;

		}

		private static HeaderStyleCollection avaliableHeaderStyles=null;
		public static HeaderStyleCollection AvaliableHeaderStyles
		{
			get{				
				 return avaliableHeaderStyles;
			  }
			set
			{
				avaliableHeaderStyles=value;
			}
		}

		public static string GetStylePath()
		{
            string dir = Webb.Utility.ApplicationDirectory + @"Template\Styles\ReportHeaderStyle";

			if(!System.IO.Directory.Exists(dir))
			{	
				System.IO.Directory.CreateDirectory(dir);
			}
			return string.Format(@"{0}\Header{1}",dir,FileExtesion);
		}
		public static void SerirlizeHeaderStyles()
		{	
			string stylepath=GetStylePath();

			if(File.Exists(stylepath))
			{
				File.Delete(stylepath);
			}
		
			System.IO.FileStream stream = System.IO.File.Open(stylepath,System.IO.FileMode.OpenOrCreate);

			Webb.Utilities.WaitingForm.ShowWaitingForm();

			Webb.Utilities.WaitingForm.SetWaitingMessage("Saving header styles..");

			try
			{	


				Webb.Utilities.Serializer.SerializeObject(stream,avaliableHeaderStyles);
			}
			catch(Exception ex)
			{
				Webb.Utilities.MessageBoxEx.ShowError("Failed to save reportheaderstyle collection!\n"+ex.Message);				
			}
			finally
			{
				stream.Close();

				Webb.Utilities.WaitingForm.CloseWaitingForm();

			}
		

		}
    
		public static void DeSerirlizeHeaderStyles()
		{
			string stylepath=GetStylePath();

			if(File.Exists(stylepath))
			{	
				System.IO.FileStream stream = System.IO.File.Open(stylepath,System.IO.FileMode.Open);

				try
				{
					avaliableHeaderStyles= Webb.Utilities.Serializer.DeserializeObject(stream) as HeaderStyleCollection;
				}
				catch(Exception ex)
				{
					Webb.Utilities.MessageBoxEx.ShowError("Failed to load headerstyle collection!\n"+ex.Message);

					avaliableHeaderStyles=null;
				}
				finally
				{
					stream.Close();
					
				}
			}
			else
			{				
				avaliableHeaderStyles=null;
			}
		}

		#region Serialization By Simon's Macro 2010-2-10 14:13:10
		public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
             base.GetObjectData(info,context);
		
		}

		public HeaderStyleCollection(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context):base(info,context)
		{
		}
		#endregion
		
	}
	#endregion

	#region Game List Info
	[Serializable]
	public class HeaderGameInfo:ISerializable
	{
		private string styleName=string.Empty;

		private bool _FirstObjectOnly=false;
		private bool _Multiline=false;
		private bool _Date=true;
		private bool _Object=true;
		private bool _Location=true;
		private bool _ScoutType=true;
		private bool _Title=false;
		private bool _WordWrap=true;
		private bool _Opponent=true;
		
		public override string ToString()
		{
			return StyleName;
		}


		public HeaderGameInfo(string _styleName)
		{
             styleName=_styleName;
		}
		[Category("StyleName")]
		public string StyleName
		{
			get{ return styleName; }
			set{ styleName = value; }
		}

		public bool FirstObjectOnly
		{
			get{ return _FirstObjectOnly; }
			set{ _FirstObjectOnly = value; }
		}

		public bool Multiline
		{
			get{ return _Multiline; }
			set{ _Multiline = value; }
		}

		public bool Date
		{
			get{ return _Date; }
			set{ _Date = value; }
		}

		public bool Object
		{
			get{ return _Object; }
			set{ _Object = value; }
		}

		public bool Location
		{
			get{ return _Location; }
			set{ _Location = value; }
		}

		public bool ScoutType
		{
			get{ return _ScoutType; }
			set{ _ScoutType = value; }
		}

		public bool Title
		{
			get{ return _Title; }
			set{ _Title = value; }
		}

		[Browsable(false)]
		public bool WordWrap
		{
			get{ return _WordWrap; }
			set{ _WordWrap = value; }
		}

		public bool Opponent
		{
			get{ return _Opponent; }
			set{ _Opponent = value; }
		}

		public string MakeGameInfoString(GameInfoCollection i_Games)
		{
			string m_RetStr = "<No Game>";

			if(i_Games == null || i_Games.Count == 0) return m_RetStr;

			StringBuilder m_SBGames = new StringBuilder();

			if(this._Title)
			{
				m_SBGames.AppendFormat("{0}","Games:\n");
			}

			int times = 0;
		

			foreach(GameInfo i_Game in i_Games)
			{
				if((i_Game.Object == null || i_Game.Object == string.Empty)
					&&(i_Game.Opponent == null || i_Game.Opponent == string.Empty))
				{
					//if obj and opponent is null , add game name
					if(i_Game.GameName == null || i_Game.GameName == string.Empty)
					{
						m_SBGames.Append("<None>");
					}
					else
					{
						m_SBGames.AppendFormat("{0}",i_Game.GameName);
					}
				}
				else
				{
					//format game name
					if(!this.FirstObjectOnly || times == 0)
					{
						if(this._Object)
						{
							m_SBGames.AppendFormat("{0} ",i_Game.Object);
						}

						m_SBGames.Append("VS");

						times++;
					}

					if(this._Opponent)
					{
						m_SBGames.AppendFormat(" {0} ",i_Game.Opponent);
					}
					
					if(this._Location)
					{
						m_SBGames.AppendFormat("AT {0} ",i_Game.Location);
					}

					if(this._Date)
					{
						m_SBGames.AppendFormat("ON {0} ",i_Game.GameDate);
					}

					if(this._ScoutType)
					{
						m_SBGames.AppendFormat("- {0} ",i_Game.ScoutType);	
						
					}
				}

				if(this.Multiline)
				{
					m_SBGames.AppendFormat("\n");
				}
				else
				{					
					if(!this.FirstObjectOnly)
					{
						m_SBGames.AppendFormat(",");
					}					
				}
			}
			m_SBGames.Length--;	
			

			return m_SBGames.ToString();
		}
		

		#region Copy Function By Macro 2010-2-9 15:28:33
		public HeaderGameInfo Copy()
		{
			HeaderGameInfo thiscopy=new HeaderGameInfo(this.styleName);
			
			thiscopy._FirstObjectOnly=this._FirstObjectOnly;
			thiscopy._Multiline=this._Multiline;
			thiscopy._Date=this._Date;
			thiscopy._Object=this._Object;
			thiscopy._Location=this._Location;
			thiscopy._ScoutType=this._ScoutType;
			thiscopy._Title=this._Title;
			thiscopy._WordWrap=this._WordWrap;
			thiscopy._Opponent=this._Opponent;
			return thiscopy;
		}
		#endregion		

		#region Serialization By Simon's Macro 2010-2-21 10:41:47
		public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			info.AddValue("styleName",styleName);
			info.AddValue("_FirstObjectOnly",_FirstObjectOnly);
			info.AddValue("_Multiline",_Multiline);
			info.AddValue("_Date",_Date);
			info.AddValue("_Object",_Object);
			info.AddValue("_Location",_Location);
			info.AddValue("_ScoutType",_ScoutType);
			info.AddValue("_Title",_Title);
			info.AddValue("_WordWrap",_WordWrap);
			info.AddValue("_Opponent",_Opponent);
		
		}

		public HeaderGameInfo(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			try
			{
				styleName=info.GetString("styleName");
			}
			catch
			{
				styleName=string.Empty;
			}
			try
			{
				_FirstObjectOnly=info.GetBoolean("_FirstObjectOnly");
			}
			catch
			{
				_FirstObjectOnly=false;
			}
			try
			{
				_Multiline=info.GetBoolean("_Multiline");
			}
			catch
			{
				_Multiline=false;
			}
			try
			{
				_Date=info.GetBoolean("_Date");
			}
			catch
			{
				_Date=true;
			}
			try
			{
				_Object=info.GetBoolean("_Object");
			}
			catch
			{
				_Object=true;
			}
			try
			{
				_Location=info.GetBoolean("_Location");
			}
			catch
			{
				_Location=true;
			}
			try
			{
				_ScoutType=info.GetBoolean("_ScoutType");
			}
			catch
			{
				_ScoutType=true;
			}
			try
			{
				_Title=info.GetBoolean("_Title");
			}
			catch
			{
				_Title=false;
			}
			try
			{
				_WordWrap=info.GetBoolean("_WordWrap");
			}
			catch
			{
				_WordWrap=true;
			}
			try
			{
				_Opponent=info.GetBoolean("_Opponent");
			}
			catch
			{
				_Opponent=true;
			}
		}
		#endregion

		
		
		
	}

	[Serializable]
	public class HeaderGameInfoCollection:Webb.Collections.WebbCollection
	{
		public const string FileExtesion=".hgr";

		public HeaderGameInfoCollection():base()
		{
		}
		public HeaderGameInfo this[int index]
		{
			get{return this.innerList[index] as HeaderGameInfo;}

			set{this.innerList[index]=value;}
		}
		public HeaderGameInfo this[string stylename]
		{
			get
			{
				foreach(HeaderGameInfo styles in this)
				{
					if(styles.StyleName==stylename)return styles;
				}
				return null;		
			}		
		}
		

		public bool DeleteStyle(string stylename)
		{
			HeaderGameInfo style=this[stylename];

			if(style==null)return false;

			innerList.Remove(style);

			return true;

		}

		private static HeaderGameInfoCollection avaliableGameInfos;
		public static HeaderGameInfoCollection AvaliableGameInfos
		{
			get
			{				
				return avaliableGameInfos;
			}
			set
			{
				avaliableGameInfos=value;
			}
		}


		public static string GetStylePath()
		{
            string dir = Webb.Utility.ApplicationDirectory + @"Template\Styles\ReportHeaderStyle";

			if(!System.IO.Directory.Exists(dir))
			{	
				System.IO.Directory.CreateDirectory(dir);
			}
			return string.Format(@"{0}\GameInfo{1}",dir,FileExtesion);
		}
		public static void SerirlizeGameInfos()
		{	
			string stylepath=GetStylePath();

			if(File.Exists(stylepath))
			{
				File.Delete(stylepath);
			}
		
			System.IO.FileStream stream = System.IO.File.Open(stylepath,System.IO.FileMode.OpenOrCreate);

			try
			{	
				Webb.Utilities.Serializer.SerializeObject(stream,avaliableGameInfos);
			}
			catch(Exception ex)
			{
				Webb.Utilities.MessageBoxEx.ShowError("Failed to save game list info collection!\n"+ex.Message);				
			}
			finally
			{
				stream.Close();
			}

		}
		public static void DeSerirlizeGameInfos()
		{
			string stylepath=GetStylePath();

			if(File.Exists(stylepath))
			{				
				System.IO.FileStream stream = System.IO.File.Open(stylepath,System.IO.FileMode.Open);

				try
				{
					avaliableGameInfos= Webb.Utilities.Serializer.DeserializeObject(stream) as HeaderGameInfoCollection;
				}
				catch(Exception ex)
				{
					Webb.Utilities.MessageBoxEx.ShowError("Failed to load game list info collection!\n"+ex.Message);
				}
				finally
				{
					stream.Close();
				}
			}
			else
			{
				Webb.Utilities.MessageBoxEx.ShowError("Failed to located at the gamelist Info style!\n"+stylepath);
			}
		}

		#region Serialization By Simon's Macro 2010-2-10 14:13:10
		public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			base.GetObjectData(info,context);
		
		}

		public HeaderGameInfoCollection(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context):base(info,context)
	    {
	    }
		#endregion
	}

	#endregion
}
