/***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:MessageBox.cs
 * Author:Country.Wu [EMail:Webb.Country.Wu@163.com]
 * Create Time:11/9/2007 09:25:48 AM
 * Copyright:1986-2007@Webb Electronics all right reserved.
 * Purpose:
 * ***********************************************************************/
#region History
/*
 * //Author@DateTime : Description
 * */
#endregion History
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
using System;
using System.Windows.Forms;

namespace Webb.Utilities
{
	/// <summary>
	/// Summary description for MessageBox.
	/// </summary>
	public sealed class MessageBoxEx
	{
		private MessageBoxEx()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static DialogResult ShowError(string i_Message)
		{
			return MessageBox.Show(i_Message,"Error:",MessageBoxButtons.OK,MessageBoxIcon.Error);
		}
        public static DialogResult ShowError(string i_Message,string i_Title)
        {
            return MessageBox.Show(i_Message, i_Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static DialogResult ShowMessage(string i_Message, string i_Title)
		{
            return MessageBox.Show(i_Message, i_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
        public static DialogResult ShowMessage(string i_Message)
        {
            return MessageBox.Show(i_Message, "Information:", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

		public static DialogResult ShowError(Form i_Owner, string i_Message)
		{
			return MessageBox.Show(i_Owner,i_Message,"Error:",MessageBoxButtons.OK,MessageBoxIcon.Error);
		}

		public static DialogResult ShowWarning(string i_Message)
		{
			return MessageBox.Show(i_Message,"Warning:",MessageBoxButtons.OK,MessageBoxIcon.Warning);
		}
        public static DialogResult ShowWarning(string i_Message, string i_Title)
        {
            return MessageBox.Show(i_Message, i_Title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

		public static DialogResult ShowWarning(Form i_Owner,string i_Message)
		{
			return MessageBox.Show(i_Owner,i_Message,"Warning:",MessageBoxButtons.OK,MessageBoxIcon.Warning);
		}

		public static DialogResult ShowQuestion(string i_Message)
		{
			return MessageBox.Show(i_Message,"Question:",MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question);
		}
        public static DialogResult ShowQuestion(string i_Message, string i_Title)
        {
            return MessageBox.Show(i_Message, i_Title, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        }


		public static DialogResult ShowQuestion(Form i_Owner,string i_Message)
		{
			return MessageBox.Show(i_Owner,i_Message,"Question:",MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question);
		}

		public static DialogResult ShowQuestion_YesNoCancel(string i_Message)
		{
			return MessageBox.Show(i_Message,"Question:",MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question);
		}

		public static DialogResult ShowQuestion_YesNoCancel(Form i_Owner,string i_Message)
		{
			return MessageBox.Show(i_Owner,i_Message,"Question:",MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question);
		}

		public static DialogResult ShowQuestion_YesNo(string i_Message)
		{
			return MessageBox.Show(i_Message,"Question:",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
		}
        public static DialogResult ShowQuestion_YesNo(string i_Message, string i_Title)
        {
            return MessageBox.Show(i_Message, i_Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

		public static DialogResult ShowQuestion_YesNo(Form i_Owner,string i_Message)
		{
			return MessageBox.Show(i_Owner,i_Message,"Question:",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
		}
	}
}
