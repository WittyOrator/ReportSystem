#define DEMO

using System;
using System.Collections;
using System.IO;

namespace Webb.Reports.MoviePlayer
{
    #region public interface IVideoPlayer

    /// <summary>
    /// 
    /// </summary>
    public interface IVideoPlayer
    {
        event EventHandler OnMediaEnd;
        void SetWorkSpace(string i_Path);
        void AddVideos(VideoInfoCollection i_Videos);
        void Pause();
        void Stop();
        void ClosePlayer();
        void Play(PlayTypes i_PlayTypes);
        void HideWindow();
        void ShowWindow();
        int CurrentFrame { get; }
        int TotalFrames { get; }
        bool ShowFrames { get; set; }
        bool LockWinSizeRate { get; set; }
        bool AutoCloseWindowsWhenMediaEnd { get; set; }
        System.Windows.Forms.Form WinForm { get; }
    }

    public enum PlayTypes
    {
        Default = 0,
        Stop = 1,
        Play = 2,
        Pause = 3,
        Play2 = 4,
        Slow = 5,
    }
    #endregion

    #region public class VideoInfo & collection
    /// <summary>
    /// 
    /// </summary>
    public class VideoInfo
    {
        static readonly int _DefaulFrameRate = 30;
        static readonly char[] _TimeSpanSepraters = new char[] { ':', '.' };


        static public int SubStringCount(string WithinString, string search)
        {
            if (string.IsNullOrEmpty(search))
                throw new ArgumentNullException("search");
            int counter = 0;
            int index = WithinString.IndexOf(search, 0);
            while (index >= 0 && index < WithinString.Length)
            {
                counter++;
                index = WithinString.IndexOf(search, index + search.Length);
            }
            return counter;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i_String">TimeSpan formate:00:00:00.00?? - hh:mm:ss.tttt</param>
        /// <returns>TimeSpan, or TimeSpan.Zero at any error occur</returns>
        static public TimeSpan NewTimeSpan(string i_String)
        {
            int ColonCount= SubStringCount(i_String, ":");

            for (int i = 0; i < 2 - ColonCount; i++)
            {
                i_String = "0:" + i_String;
            }
     
            string[] m_Values = i_String.Split(_TimeSpanSepraters);            

            if (m_Values.Length < 3) return TimeSpan.Zero;

            int m_Days = 0, m_Hours = 0, m_Minutes = 0, m_Seconds = 0, m_MilliseSec = 0;
            try
            {
                m_Hours = Convert.ToInt32(m_Values[0]);
                m_Minutes = Convert.ToInt32(m_Values[1]);
                m_Seconds = Convert.ToInt32(m_Values[2]);
                if (m_Values.Length == 4)
                {
                    m_MilliseSec = Convert.ToInt32(m_Values[3]) * 10;
                }
                return new TimeSpan(m_Days, m_Hours, m_Minutes, m_Seconds, m_MilliseSec);
            }
            catch
            {
                return TimeSpan.Zero;
            }
        }
        //members
        string _GameName = string.Empty;	//05-06-2008@Scott
        string _FilePath = string.Empty;
        int _PlayNum = -1;					//05-06-2008@Scott
        int _MasterNum = -1;				//05-07-2008@Scott
        int _StartFrame = 0;
        int _EndFrame = 0;
        TimeSpan _StartTime = TimeSpan.Zero;
        TimeSpan _EndTime = TimeSpan.Zero;
        short _Angle = 1;
        int _PlayID = 0;
        int _GameID = 0;
        //properties
        public int MasterNum
        {
            get { return this._MasterNum; }
            set { this._MasterNum = value; }
        }
        public string GameName
        {
            get { return this._GameName; }
            set { this._GameName = value; }
        }
        public int PlayNum
        {
            get { return this._PlayNum; }
            set { this._PlayNum = value; }
        }
        public int GameID
        {
            get { return this._GameID; }
            set { this._GameID = value; }
        }
        public int PlayID
        {
            get { return this._PlayID; }
            set { this._PlayID = value; }
        }
        public TimeSpan StartTimeSpan
        {
            get { return this._StartTime; }
            set
            {
                this._StartTime = value;
                this.OnTimeSpanChanged();
            }
        }
        public TimeSpan EndTimeSpan
        {
            get { return this._EndTime; }
            set
            {
                this._EndTime = value;
                this.OnTimeSpanChanged();
            }
        }
        public string FilePath
        {
            get { return this._FilePath; }
            set
            {
#if !DEMO
				string m_Path = value;
				while(m_Path.EndsWith(@"\")||m_Path.EndsWith(@"/"))
				{
					m_Path = m_Path.Substring(0,m_Path.Length-1);
				}
				if(!Directory.Exists(m_Path))
				{	
					m_Path = AppDomain.CurrentDomain.BaseDirectory;
					//throw new IOException("Directory does not exist.");					
				}
				this._FilePath = m_Path;
#else
                this._FilePath = value;
#endif
            }
        }
        public int StartFrame
        {
            get { return this._StartFrame; }
            set { this._StartFrame = value; }
        }
        public int EndFrame
        {
            get { return this._EndFrame; }
            set { this._EndFrame = value; }
        }
        public short Angle
        {
            get { return this._Angle; }
            set { this._Angle = value; }
        }
        private void OnTimeSpanChanged()
        {
            this._StartFrame = Convert.ToInt32(this._StartTime.TotalSeconds * _DefaulFrameRate);
            this._EndFrame = Convert.ToInt32(this._EndTime.TotalSeconds * _DefaulFrameRate);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public sealed class VideoInfoCollection : CollectionBase
    {
        public int Add(VideoInfo i_VideoInfo)
        {
            return this.List.Add(i_VideoInfo);
        }
        public void Remove(VideoInfo i_VideoInfo)
        {
            this.List.Remove(i_VideoInfo);
        }
        //		public void RemoveAt(int i_Index)
        //		{
        //			this.List.RemoveAt(i_Index);
        //		}
        public bool Contains(VideoInfo i_VideoInfo)
        {
            return this.List.Contains(i_VideoInfo);
        }
        public void Insert(VideoInfo i_VideoInfo, int i_Index)
        {
            this.List.Insert(i_Index, i_VideoInfo);
        }
    }
    #endregion  
}
