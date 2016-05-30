/***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:WinzardControlBase.cs
 * Author:Country.Wu [EMail:Webb.Country.Wu@163.com]
 * Create Time:11/8/2007 10:09:48 AM
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

namespace Webb.Utilities.Wizards
{
	/// <summary>
	/// Summary description for WinzardControlBase.
	/// </summary>
	public class WinzardControlBase : System.Windows.Forms.UserControl
	{
		private WinzardControlBase C_PreControl = null;	//11-12-2007@scott
		private bool _FirstControl = false;				//11-13-2007@scott
		private bool _LastControl = false;				//11-13-2007@scott
		private bool _FinishControl = false;			//11-13-2007@scott
		private bool _SelectStep = false;
		private bool _SingleStep = false;
		private string _Title = "Wizard Step 0";
		WizardBaseForm _ParentWizardForm;

//		private int _StepIndex = -1;
//
//		public int SetepIndex
//		{
//			get{return this._StepIndex;}
//			set{this._StepIndex = value;}
//		}

		public bool SelectStep
		{
			set{this._SelectStep = value;}
			get{return this._SelectStep;}
		}

		public bool SingleStep
		{
			set{this._SingleStep = value;}
			get{return this._SingleStep;}
		}

		[Browsable(false)]
		public WinzardControlBase PreControl
		{
			get{return this.C_PreControl;}
			set{this.C_PreControl = value;}
		}

		[Browsable(false)]
		public bool FirstControl
		{
			get{return this._FirstControl;}
			set{this._FirstControl = value;}
		}

		[Browsable(false)]
		public bool LastControl
		{
			get{return this._LastControl;}
			set{this._LastControl = value;}
		}

		[Browsable(false)]
		public bool FinishControl
		{
			get{return this._FinishControl;}
			set{this._FinishControl = value;}
		}

		[Browsable(false)]
		public WizardBaseForm ParentWizardForm
		{
			get{return this._ParentWizardForm;}
			set{this._ParentWizardForm = value;}
		}

		public string WizardTitle
		{
			get{return this._Title;}
			set{this._Title = value;}
		}


//		new public Size Size
//		{
//			get{return base.Size;}//
//		}
		

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		

		public WinzardControlBase()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

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
			// WinzardControlBase
			// 
			this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "WinzardControlBase";
			this.Size = new System.Drawing.Size(790, 480);

		}
		#endregion

		public virtual void UpdateData(object i_Data)
		{
			// TODO: implement
		}
      
		public virtual void SetData(object i_Data)
		{
			// TODO: implement
		}
      
		public virtual bool ValidateSetting()
		{
			// TODO: implement
			return false;
		}

		public virtual void ResetControl()
		{
			// TODO: implement
		}

		//Scott@2007-11-30 11:33 modified some of the following code.
		public virtual void OnSelectAll()
		{
			
		}

		public virtual void OnClearAll()
		{

		}
	}
}
