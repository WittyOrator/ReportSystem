/***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:VideoPlayBackManager.cs
 * Author:Country.Wu [EMail:Webb.Country.Wu@163.com]
 * Create Time:11/9/2007 10:49:21 AM
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
using System.Windows.Forms;
using System.Threading;
using System.IO;

using Webb.Reports.DataProvider;
using Webb.Reports.MoviePlayer;
using WEBBSHAREMEMORYLib;

namespace Webb.Reports.DataProvider
{
	//05-06-2008@Scott
	public struct IRPLAY
	{
		public int nPlayNum;
		public int nMasterNum;
		public string bstrGameName;
	}

	public struct IREDIT
	{
		public int nStartTime;
		public int nEndTime;
		public int nSnapTime;
		public int nAngleNum;
		public string bstrVideo;

        //public int nPlayNum;  //Add this code at 2011-5-25 15:23:15@simon
        //public int nMasterNum;  //Add this code at 2011-5-25 15:23:15@simon
        //public string bstrGameName;  //Add this code at 2011-5-25 15:23:15@simon
	}

	public struct IRMSG
	{
		public int nHwnd;
		public int nMessage;
		public int nWParam;
		public int nLParam;
		public string strDisc;
	}

	public class ProcessInfo
	{
		public const string I_MemoryName = "Advantage To Interactive Report - MSG";

		public const string O_MemoryName = "Interactive Report To Advantage - MSG";

		public const int User_Message = 0x501;
	
		public const int MSG_Length = 276;

		public const int EDIT_Length = 276;

		public const int PLAY_Length = 268;

		public const int Str_Length = 260;

		public const int MAX_LEN = 1000;

		public static ShareMemoryClass I_ShareMemoryFile = new ShareMemoryClass();

		public static ShareMemoryClass O_ShareMemoryFile = new ShareMemoryClass();

		public static IRMSG Current_Msg = new IRMSG();

		public static IntPtr MainWndHandle;

		public static byte[] RawSerialize( object anything )
		{
			int rawsize = Marshal.SizeOf( anything );

			IntPtr buffer = Marshal.AllocHGlobal( rawsize );

			Marshal.StructureToPtr( anything, buffer, false );

			byte[] rawdatas = new byte[ rawsize ];

			Marshal.Copy( buffer, rawdatas, 0, rawsize );

			Marshal.FreeHGlobal( buffer );

			return rawdatas;
		}

		public static object RawDeserialize( byte[] rawdatas, Type anytype )
		{
			int rawsize = Marshal.SizeOf( anytype );

			if( rawsize > rawdatas.Length )

			return null;

			IntPtr buffer = Marshal.AllocHGlobal( rawsize );

			Marshal.Copy( rawdatas, 0, buffer, rawsize );

			object retobj = Marshal.PtrToStructure( buffer, anytype );

			Marshal.FreeHGlobal( buffer );

			return retobj;
		} 
 
		public static void SetMsg(ref IRMSG msg)
		{
			msg.nHwnd = 0;
			msg.nMessage = 0;
			msg.nWParam = 0;
			msg.nLParam = 0;
			msg.strDisc = null;
		}

		public static void SetMsg(ref IRMSG msg,int nHwnd,int nMessage,int nWParam,int nLParam,string strDisc)
		{
			msg.nHwnd = nHwnd;
			msg.nMessage = nMessage;
			msg.nWParam = nWParam;
			msg.nLParam = nLParam;
			msg.strDisc = strDisc;
		}

		public static void ResetShareInputFile()
		{
			ProcessInfo.I_ShareMemoryFile.ResetMemoryInfo();
		}

		public static void SendMemoryMessage(IRMSG msg,IREDIT[] edits)
		{
			ProcessInfo.O_ShareMemoryFile.SetMemoryName(O_MemoryName);

			//byte[] byteArray = ProcessInfo.RawSerialize(msg);

			byte[] byteArray = ProcessInfo.GetBytesFromMsg(msg,edits);

			ProcessInfo.O_ShareMemoryFile.WriteMemoryInfo(ref byteArray[0],byteArray.Length);
		}

		//05-06-2008@Scott
		public static void SendMemoryMessage(IRMSG msg,IRPLAY[] plays)
		{
			ProcessInfo.O_ShareMemoryFile.SetMemoryName(O_MemoryName);

			//byte[] byteArray = ProcessInfo.RawSerialize(msg);

			byte[] byteArray = ProcessInfo.GetBytesFromMsg(msg,plays);

			ProcessInfo.O_ShareMemoryFile.WriteMemoryInfo(ref byteArray[0],byteArray.Length);
		}

		public static byte[] GetBytesFromMsg(IRMSG msg,IREDIT[] edits)
		{
			int len = 0,offset = 0;

			byte[] allByteArray = new byte[MSG_Length + MAX_LEN * EDIT_Length];

			byte[] byteArray = ProcessInfo.MakeIRMSGToBytes(msg);

			len = ProcessInfo.MSG_Length;

			Buffer.BlockCopy(byteArray,0,allByteArray,offset,len);

			offset += MSG_Length;

			for(int i = 0;i<MAX_LEN;i++)
			{
				byte[] byteEditArray = ProcessInfo.MakeIREDITToBytes(edits[i]);

				len = ProcessInfo.EDIT_Length;

				Buffer.BlockCopy(byteEditArray,0,allByteArray,offset,len);

				offset += EDIT_Length;
			}

			return allByteArray;
		}

		//05-06-2008@Scott
		public static byte[] GetBytesFromMsg(IRMSG msg,IRPLAY[] plays)
		{
			int len = 0,offset = 0;

			byte[] allByteArray = new byte[MSG_Length + MAX_LEN * PLAY_Length];

			byte[] byteArray = ProcessInfo.MakeIRMSGToBytes(msg);

			len = ProcessInfo.MSG_Length;

			Buffer.BlockCopy(byteArray,0,allByteArray,offset,len);

			offset += MSG_Length;

			for(int i = 0;i<MAX_LEN;i++)
			{
				byte[] bytePlayArray = ProcessInfo.MakeIRPLAYToBytes(plays[i]);

				len = ProcessInfo.PLAY_Length;

				Buffer.BlockCopy(bytePlayArray,0,allByteArray,offset,len);

				offset += PLAY_Length;
			}

			return allByteArray;
		}

		public static void SetMemoryMessage(int message, int handle)
		{
			ProcessInfo.I_ShareMemoryFile.SetMemoryName(I_MemoryName);

			ProcessInfo.I_ShareMemoryFile.SetMessageID(message);

			ProcessInfo.I_ShareMemoryFile.SetReceiver(handle);

			ProcessInfo.I_ShareMemoryFile.StartMemoryMonitor();
		}

		public static void GetMessage()
		{
			int address = 0;

			ProcessInfo.I_ShareMemoryFile.ReadMemoryInfo(out address);

			ProcessInfo.SetMsg(ref ProcessInfo.Current_Msg);

			IntPtr ptr = new IntPtr(address);

			ProcessInfo.Current_Msg = (IRMSG)Marshal.PtrToStructure(ptr,typeof(IRMSG));

			Marshal.Release(ptr);
		}

		public static void GetMessage(ref IRMSG msg)
		{
			int address = 0;

			ProcessInfo.I_ShareMemoryFile.ReadMemoryInfo(out address);

			ProcessInfo.SetMsg(ref msg);

			IntPtr ptr = new IntPtr(address);

			msg = (IRMSG)Marshal.PtrToStructure(ptr,typeof(IRMSG));

			Marshal.Release(ptr);
		}

		public static byte[] MakeIRMSGToBytes(IRMSG msg)
		{
			int len = 0,offset = 0,size = ProcessInfo.MSG_Length;

			byte[] allBytes = new byte[size];
			
			byte[] bytesHwnd = BitConverter.GetBytes(msg.nHwnd);

			len = bytesHwnd.Length;

			Buffer.BlockCopy(bytesHwnd,0,allBytes,offset,len);

			offset += len;

			byte[] bytesMessage = BitConverter.GetBytes(msg.nMessage);

			len = bytesMessage.Length;

			Buffer.BlockCopy(bytesMessage,0,allBytes,offset,len);

			offset += len;

			byte[] bytesWParam = BitConverter.GetBytes(msg.nWParam);

			len = bytesWParam.Length;

			Buffer.BlockCopy(bytesWParam,0,allBytes,offset,len);

			offset += len;

			byte[] bytesLParam = BitConverter.GetBytes(msg.nLParam);

			len = bytesLParam.Length;

			Buffer.BlockCopy(bytesLParam,0,allBytes,offset,len);

			offset += len;

			byte[] byteArray = System.Text.Encoding.Default.GetBytes(msg.strDisc);

			len = msg.strDisc.Length;

			Buffer.BlockCopy(byteArray,0,allBytes,offset,len);

			return allBytes;
		}

		public static byte[] MakeIREDITToBytes(IREDIT edit)
		{
			int len = 0,offset = 0,size = ProcessInfo.EDIT_Length;

			byte[] allBytes = new byte[size];
			
			byte[] bytesStartTime = BitConverter.GetBytes(edit.nStartTime);

			len = bytesStartTime.Length;

			Buffer.BlockCopy(bytesStartTime,0,allBytes,offset,len);

			offset += len;

			byte[] bytesEndTime = BitConverter.GetBytes(edit.nEndTime);

			len = bytesEndTime.Length;

			Buffer.BlockCopy(bytesEndTime,0,allBytes,offset,len);

			offset += len;

			byte[] bytesSnapTime = BitConverter.GetBytes(edit.nSnapTime);

			len = bytesSnapTime.Length;

			Buffer.BlockCopy(bytesSnapTime,0,allBytes,offset,len);

			offset += len;

			byte[] bytesAngle = BitConverter.GetBytes(edit.nAngleNum);

			len = bytesAngle.Length;

			Buffer.BlockCopy(bytesAngle,0,allBytes,offset,len);

			offset += len;

			byte[] byteVideo = System.Text.Encoding.Default.GetBytes(edit.bstrVideo);

			len = edit.bstrVideo.Length;

			Buffer.BlockCopy(byteVideo,0,allBytes,offset,len);

			return allBytes;
		}

		//05-06-2008@Scott
		public static byte[] MakeIRPLAYToBytes(IRPLAY play)
		{
			int len = 0,offset = 0,size = ProcessInfo.PLAY_Length;

			byte[] allBytes = new byte[size];
			
			byte[] bytesPlayNum = BitConverter.GetBytes(play.nPlayNum);

			len = bytesPlayNum.Length;

			Buffer.BlockCopy(bytesPlayNum,0,allBytes,offset,len);

			offset += len;

			byte[] bytesMasterNum = BitConverter.GetBytes(play.nMasterNum);

			len = bytesMasterNum.Length;

			Buffer.BlockCopy(bytesMasterNum,0,allBytes,offset,len);

			offset += len;

			byte[] bytesGameName = System.Text.Encoding.Default.GetBytes(play.bstrGameName);

			len = play.bstrGameName.Length;

			Buffer.BlockCopy(bytesGameName,0,allBytes,offset,len);

			return allBytes;
		}
	}

    

	/// <summary>
	/// Summary description for VideoPlayBackManager.
	/// </summary>
	public sealed class VideoPlayBackManager
	{
		[DllImport("User32")]
		public extern static void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);


		public static DataSet DataSource;

		public static DataProvider.WebbDataProvider PublicDBProvider;	
	
		public static Browser.ReportBrowserBase PublicBrowser;	//05-04-2008@Scott

		public static MouseEventArgs CurEventArgs;	//05-05-2008@Scott

		public static VideoPlayBackArgs CurVideoPlayBackArgs;	//05-06-2008@Scott

		public static string DiagramScoutType = "Offense";	//Modified at 2008-10-16 11:08:37@Scott

        public static string DiaPath=string.Empty;

        public static string PictureDir= string.Empty;

		public static bool InvertDiagram=false;

		public static AdvScoutType AdvSectionType=AdvScoutType.None;  //2009-6-9 11:42:25@Simon Add this Code

		public static ScFilterList AdvReportFilters = new ScFilterList();	//Modified at 2009-1-19 11:28:12@Scott
	
		public static bool ClickEvent = true;

		private static object lastSender = null;

		private static System.DateTime lastTime;

		private static double speedDClick = 0.5f;

		public static Cursor DefaultCursor=null;

        public static void ReadPictureDirFromRegistry()
        {
            string RegSharekeyPath = "Software\\WEBB ELECTRONICS, INC.\\W4W32\\SYSTEM";

            Microsoft.Win32.RegistryKey RegShareKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(RegSharekeyPath, true);

            if (RegShareKey == null)
            {
                return;
            }

            string strFolder = string.Empty;

            object Folder = RegShareKey.GetValue("PICTUREDIR");

            if (Folder != null) strFolder = Folder.ToString().TrimEnd(@"\ ".ToCharArray());

            PictureDir = strFolder;            
        }

		private static bool IsDoubleClick(object Sender)
		{
			bool ret = false;

			System.DateTime now = System.DateTime.Now;
			
			if(lastTime.AddSeconds(speedDClick) > now && Sender == lastSender)
			{
				ret = true;
			}

			lastSender = Sender;
			
			lastTime = now;
			
			return ret;
		}

		private static bool CheckClickArgs(EventArgs e)
		{
			if(e is VideoPlayBackArgs)
			{
				VideoPlayBackArgs m_Arg = e as VideoPlayBackArgs;

				if(m_Arg == null) return false;
 
				if(VideoPlayBackManager.DataSource == null) return false;

				if(m_Arg.RowIndicators == null || m_Arg.RowIndicators.Count<=0)
				{
					Webb.Utilities.TopMostMessageBox.ShowMessage("No Associated Plays",MessageBoxButtons.OK);
                   
					mouse_event( 0x0004,0, 0, 0, 0); 
            
					return false;
				}
			}

			return true;
		}

		private static IREDIT[] MakeEdits(VideoInfoCollection videoInfo)
		{
			int index = 0;
				
			IREDIT[] edits = new IREDIT[ProcessInfo.MAX_LEN];

			for(int i = 0;i<ProcessInfo.MAX_LEN;i++)
			{
				edits[i].bstrVideo = string.Empty;
			}

			foreach(VideoInfo vi in videoInfo)
			{
				edits[index].bstrVideo = vi.FilePath;
				edits[index].nStartTime = vi.StartFrame;
				edits[index].nEndTime = vi.EndFrame;
				edits[index].nAngleNum = vi.Angle;

                //edits[index].nPlayNum = vi.PlayNum;  //Add this code at 2011-5-25 15:22:40@simon
                //edits[index].nMasterNum = vi.nMasterNum;  //Add this code at 2011-5-25 15:22:40@simon
                //edits[index].bstrGameName = vi.GameName;  //Add this code at 2011-5-25 15:22:40@simon

				index ++;
			}

			return edits;
		}

		//05-06-2008@Scott
		private static IRPLAY[] MakeSaveToEdl(VideoInfoCollection videoInfo)
		{
			ArrayList arrExistPlays = new ArrayList();

			int index = 0;
				
			IRPLAY[] plays = new IRPLAY[ProcessInfo.MAX_LEN];

			for(int i = 0;i<ProcessInfo.MAX_LEN;i++)
			{
				plays[i].bstrGameName = string.Empty;
				plays[i].nPlayNum = -1;
				plays[i].nMasterNum = -1;
			}

			foreach(VideoInfo vi in videoInfo)
			{
				if(arrExistPlays.Contains(vi.GameName + vi.PlayNum.ToString()))
				{
					continue;
				}
				else
				{
					arrExistPlays.Add(vi.GameName + vi.PlayNum.ToString());
				}

				plays[index].bstrGameName = vi.GameName;
				plays[index].nPlayNum = vi.PlayNum;
				plays[index].nMasterNum = vi.MasterNum;
				index ++;
			}

			return plays;
		}

		public static void OnBrickDown(object sender, MouseEventArgs e)
		{
			CurEventArgs = e;
		}

	

		public static void OnBrickUp(object sender, MouseEventArgs e)
		{
			CurEventArgs = e;
		}


		//05-06-2008@Scott
		public static void OnSaveToEdl(object Sender, EventArgs e)
		{
			VideoInfoCollection m_videoInfo = PublicDBProvider.LoadVideoInfo(VideoPlayBackManager.DataSource,CurVideoPlayBackArgs.RowIndicators);	

			if(m_videoInfo == null || m_videoInfo.Count == 0)
			{
				Webb.Utilities.WaitingForm.CloseWaitingForm();

				Webb.Utilities.TopMostMessageBox.ShowMessage("No Associated Plays",MessageBoxButtons.OK);

				mouse_event( 0x0004,0, 0, 0, 0); 

				return;
			}
			
			if(VideoPlayBackManager.PublicDBProvider == null)
			{
				Webb.Utilities.TopMostMessageBox.ShowMessage("Can't find Data Provider.",MessageBoxButtons.OK);

				return;
			}

			int type=(int)VideoPlayBackManager.PublicDBProvider.DBSourceConfig.WebbDBType;

			if(type ==(int)WebbDBTypes.WebbAdvantageFootball||(type>0&&type<9))
			{
				IRMSG msg = new IRMSG();
				
				ProcessInfo.SetMsg(ref msg,0,0x0204,0,0,"Save As EDL");

				IRPLAY[] plays = MakeSaveToEdl(m_videoInfo);

				ProcessInfo.SendMemoryMessage(msg,plays);

				return;
			}
		}

		public static void OnClickEvent(object Sender, EventArgs e)
		{
			if(IsDoubleClick(Sender)) return;

			if(!CheckClickArgs(e))
			{
				return;
			}

			VideoPlayBackArgs m_Arg = e as VideoPlayBackArgs;

			CurVideoPlayBackArgs = m_Arg;			

			if(CurEventArgs!=null&&CurEventArgs.Button == MouseButtons.Right)
			{//Right click
				if(PublicBrowser == null) return;

				ContextMenu menu = new ContextMenu(new MenuItem[]{new MenuItem("Save As Cutup",new EventHandler(OnSaveToEdl))});
				
				System.Drawing.Point pos = Cursor.Position;

				menu.Show(PublicBrowser,pos);
			}
			else
			{//left click
				Webb.Utilities.WaitingForm.ShowWaitingForm();
				
				try
				{
					VideoInfoCollection m_videoInfo = PublicDBProvider.LoadVideoInfo(VideoPlayBackManager.DataSource,m_Arg.RowIndicators);	

					if(m_videoInfo == null || m_videoInfo.Count == 0)
					{
						Webb.Utilities.WaitingForm.CloseWaitingForm();

						Webb.Utilities.TopMostMessageBox.ShowMessage("No Associated Video",MessageBoxButtons.OK);

						mouse_event( 0x0004,0, 0, 0, 0); 

						return;
					}
					//If advantage report , don't play video , send message back to advantage.
					if(VideoPlayBackManager.PublicDBProvider == null)
					{
						Webb.Utilities.TopMostMessageBox.ShowMessage("Can't find Data Provider.",MessageBoxButtons.OK);

						return;
					}

					int type=(int)VideoPlayBackManager.PublicDBProvider.DBSourceConfig.WebbDBType;

					if(type ==(int)WebbDBTypes.WebbAdvantageFootball||(type>0&&type<9))
					{
						IRMSG msg = new IRMSG();
				
						ProcessInfo.SetMsg(ref msg,0,0x0201,0,0,"Video Playback");

						IREDIT[] edits = MakeEdits(m_videoInfo);

						ProcessInfo.SendMemoryMessage(msg,edits);

						Webb.Utilities.WaitingForm.CloseWaitingForm();

						return;
					}

//					MoviePlayer.VPManager.MoviePlayer.AddVideos(m_videoInfo);
//
//					if(m_videoInfo.Count > 0)
//					{
//						MoviePlayer.VPManager.MoviePlayer.ShowWindow();
//
//						MoviePlayer.VPManager.MoviePlayer.Play(PlayTypes.Play);
//					}
				}
				catch(Exception ex)
				{	
					Webb.Utilities.TopMostMessageBox.ShowMessage(string.Format("Click event error. Please contact Webb for help. Error message:{0}",ex.Message),MessageBoxButtons.OK);
				}
				finally
				{
					Webb.Utilities.WaitingForm.CloseWaitingForm();
				}
			}
		}

		public static void LoadAdvScFilters()
		{
            if (PublicDBProvider == null) return;

			string strUserFolder = PublicDBProvider.DBSourceConfig.UserFolder;

			if(strUserFolder == null||strUserFolder==string.Empty) return;			

			string[] userFolders=strUserFolder.Split(';');

			if(userFolders.Length==2)
			{				
				strUserFolder=userFolders[0];									
			}

            if (strUserFolder == null) return;		

			if(strUserFolder.EndsWith("\\")) strUserFolder = strUserFolder.Remove(strUserFolder.Length - 1,1);
    			
            int index = strUserFolder.LastIndexOf('\\');

            if (strUserFolder == string.Empty||index<=0) return;		

            string strUpFolder=strUserFolder.Remove(index + 1,strUserFolder.Length - index - 1);

            string ReportFilterFile = strUpFolder + @"\WebbRpt\ScFilter.dat";

			if(!File.Exists(ReportFilterFile))
			{
                ReportFilterFile = strUserFolder + @"\WebbRpt\ScFilter.dat"; ;
			}

            if (System.IO.File.Exists(ReportFilterFile))
			{				
				AdvReportFilters.ScFilters.Clear();	//Modified at 2009-2-16 10:46:18@Scott

                AdvReportFilters.ReadOldFiltersFromDisk(ReportFilterFile);
			}
		}

		private VideoPlayBackManager()
		{
			//
			// TODO: Add constructor logic here
			//
		}
	}

	[Serializable]
	public class VideoPlayBackArgs : EventArgs
	{
		public Webb.Collections.Int32Collection RowIndicators;
		public DataSet DataSource;

		public VideoPlayBackArgs(DataSet i_DataSource,Webb.Collections.Int32Collection i_Rows)
		{
			DataSource = null; //i_DataSource;	//one slow reason
			RowIndicators = i_Rows;
		}
	}
}
