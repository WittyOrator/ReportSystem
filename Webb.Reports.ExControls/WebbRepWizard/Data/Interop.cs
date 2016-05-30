using System;
using System.Text;

using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Webb.Reports.ReportWizard.WizardInfo
{
    public class InteropHelper
    {

        [DllImport("User32.dll")]
        public static extern bool SendMessage(IntPtr hWnd, int wMsg, short wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr PostMessage(IntPtr hwnd, int wMsg, short wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);      
  
        //该函数设置由不同线程产生的窗口的显示状态;

        //如果函数原来可见，返回值为非零；如果函数原来被隐藏，返回值为零。


        [DllImport("user32.dll")]
        public static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        //该函数将创建指定窗口的线程设置到前台，并且激活该窗口。键盘输入转向该窗口，并为用户改各种可视的记号。系统给创建前台窗口的线程分配的权限稍高于其他线程。 

        //如果窗口设入了前台，返回值为非零；如果窗口未被设入前台，返回值为零。


        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        // IsIconic、IsZoomed ------ 分别判断窗口是否已最小化、最大化 

        [DllImport("user32.dll")]
        public static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool IsZoomed(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern uint GetWindowThreadProcessId(IntPtr hwnd, ref int lpdwProcessId);

		[DllImport("user32.dll")]
		public static extern IntPtr GetWindow(IntPtr hwnd, int wCmd);

		[DllImport("user32.dll")] 
		public static extern bool IsWindowVisible(IntPtr hWnd); 

		[DllImport("user32.dll")]
		public extern static int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

		public const int WM_Lbutton = 0x201; //定义了鼠标的左键点击消息

		public static IntPtr GetProcessWindow(Process process)
		{
			IntPtr Tmp_Hwnd =FindWindow(null,null);

			int TmpPid=0;

			while((int)Tmp_Hwnd!=0)
			{
				GetWindowThreadProcessId(Tmp_Hwnd,ref TmpPid) ;

				StringBuilder str = new StringBuilder(512);			

				if(TmpPid == process.Id)
				{
					if(IsWindowVisible(Tmp_Hwnd))
					{
						GetWindowText(Tmp_Hwnd, str, str.Capacity);

						string text=str.ToString();

						if(text!=string.Empty)
						{
							return Tmp_Hwnd;
						}
					}
				    
				}

				 Tmp_Hwnd = GetWindow(Tmp_Hwnd, 2) ;

			}
			return  Tmp_Hwnd; 
		} 

		public static void SendClickMessage(IntPtr hwnd)
		{
			SendMessage(hwnd,WM_Lbutton,0,0);
		}
    }  
}
