using System;
using System.Runtime.InteropServices;


namespace Webb
{
	/// <summary>
	/// Summary description for SendWebbMessage.
	/// </summary>
	public class SendWebbMessage
	{		
			//声明   API   函数   
    
			[DllImport("User32.dll",EntryPoint="SendMessage")]   
			private   static   extern   int   SendMessage(   
				int   hWnd,             //   handle   to   destination   window   
				int   Msg,               //   message   
				int   wParam,       //   first   message   parameter   
				int   lParam         //   second   message   parameter   
				);   
			[DllImport("User32.dll",EntryPoint="FindWindow")]   
			private   static   extern   int   FindWindow(string   lpClassName,string   
				lpWindowName);   
			//定义消息常数   
			public   const   int   USER   =   0x500;   
			public   const   int   TEST     =   USER   +   1;   
    
    
			//向窗体发送消息的函数   
    
			public static   void   SendMsgToMainForm(string WindowCaption)   
			{   
				int   WINDOW_HANDLER   =   FindWindow(null,WindowCaption);   
				if(WINDOW_HANDLER   ==   0)   
				{   
//					throw   new   Exception("Could   not  find   Main   window!"); 
					return;
				} 
  
				SendMessage(WINDOW_HANDLER,USER,100,200);   
			}   
	}
}
