/***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:ExControlDesignerControlBase.cs
 * Author:Country.Wu [EMail:Webb.Country.Wu@163.com]
 * Create Time:11/9/2007 02:56:18 PM
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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
//
using Webb.Reports.ExControls.Views;

namespace Webb.Reports.ExControls.UI
{
	/// <summary>
	/// Summary description for ExControlDesignerControlBase.
	/// </summary>
	public class ExControlDesignerControlBase : System.Windows.Forms.UserControl
	{
		protected ExControlDesignerFormBase _DesignerForm;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		public ExControlDesignerControlBase()
		{
			InitializeComponent();
		}

		public ExControlDesignerControlBase(ExControlDesignerFormBase i_DesignerForm)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
			this._DesignerForm = i_DesignerForm;

		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// ExControlDesignerControlBase
			// 
			this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "ExControlDesignerControlBase";
			this.Size = new System.Drawing.Size(432, 240);

		}
		#endregion

		public virtual void SetView(ExControlView i_View)
		{
			// TODO: implement
		}
      
		public virtual void UpdateView(ExControlView i_View)
		{
			// TODO: implement
		}
	}
}
