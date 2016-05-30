using System;
using System.Runtime.InteropServices;


namespace Webb
{
	/// <summary>
	/// Summary description for SendWebbMessage.
	/// </summary>
	public class SendWebbMessage
	{		
			//����   API   ����   
    
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
			//������Ϣ����   
			public   const   int   USER   =   0x500;   
			public   const   int   TEST     =   USER   +   1;   
    
    
			//���巢����Ϣ�ĺ���   
    
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
