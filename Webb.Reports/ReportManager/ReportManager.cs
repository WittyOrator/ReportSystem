/***********************************************************************
 * IDE:Microsoft Development Environment Ver:7.10
 * Module:TemplateManager.cs
 * Author:Country.Wu [EMail:Webb.Country.Wu@163.com]
 * Create Time:11/26/2007 08:28:10 AM
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
using System.IO;
//
using Microsoft.Win32;
using System.Windows.Forms;

namespace Webb.Reports.ReportManager
{
	public enum TemplateTypes
	{
		Victory,
		Advantage,
		Custom,	
		PCTools,
		Other,
	}
	/// <summary>
	/// Summary description for TemplateManager.
	/// </summary>
	public class ReportManager
	{
		//Wu.Country@2007-11-26 12:39 modified some of the following code.
		#region static functions
		static public ReportTemplateCollection LoadAllTemplates(TemplateTypes i_type)
		{
			return LoadAllTemplates(ReportTemplate.ConvertTypeToString(i_type));
		}
		static public ReportTemplateCollection LoadAllTemplates()
		{
			return LoadAllTemplates("All");
		}
		static ReportTemplateCollection LoadAllTemplates(string i_CatalogName)
		{
			RegistryKey m_RootKey = ReportTemplate.GetRootRegistry();
			string[] m_Catalogs = m_RootKey.GetSubKeyNames();
			//new collection
			ReportTemplateCollection m_TemplateCollection = new ReportTemplateCollection();
			foreach(string m_Catalog in m_Catalogs)
			{
				if((i_CatalogName!=m_Catalog)&&(i_CatalogName!="All")) continue;
				//
				RegistryKey m_SubKey = m_RootKey.OpenSubKey(m_Catalog,true);
				string[] m_Templates = m_SubKey.GetSubKeyNames();
				foreach(string m_TemplateName in m_Templates)
				{
					RegistryKey m_TemplateKey = m_SubKey.OpenSubKey(m_TemplateName);
					string[] m_Names = m_TemplateKey.GetValueNames();
					if(!ReportTemplate.CheckInfoValid(m_Names,m_TemplateKey))
					{
						m_SubKey.DeleteSubKey(m_TemplateName,false);
						continue;
					}
					ReportTemplate m_ReportTemplate = new ReportTemplate();
					foreach(string m_Name in m_Names)
					{
						object m_value = m_TemplateKey.GetValue(m_Name);
						m_ReportTemplate.SetValue(m_Name,m_value);
					}
					m_TemplateCollection.Add(m_ReportTemplate);
				}
			}
			return m_TemplateCollection;
		}
		#endregion

		public static WebbReport LoadReport(string i_ReportID)
		{
			if(i_ReportID==null||i_ReportID==string.Empty)
			{
				return null;
			}

			string m_FilePath = ReportTemplate.GetTemplateFilePath(i_ReportID);

			WebbReport m_Report = new WebbReport();

			if(File.Exists(m_FilePath))
			{
				m_Report.LoadLayout(m_FilePath);
			}

			return m_Report;
		}

		public ReportManager()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		//Wu.Country@2007-11-28 13:26 modified some of the following code.
		UI.ReportSelector _ReportSelectForm;// = new Webb.Reports.ReportManager.UI.ReportSelector();
		public DialogResult ShowReportSelector(Form i_Owner,ReportTemplateCollection i_SelectedReports)
		{
			DialogResult m_Result = DialogResult.Cancel;
			
			if(this._ReportSelectForm==null) this._ReportSelectForm = new Webb.Reports.ReportManager.UI.ReportSelector();
			
			m_Result = this._ReportSelectForm.ShowDialog(i_Owner);

			if(m_Result == DialogResult.OK)
			{
				this._ReportSelectForm.GetSelectedReports(i_SelectedReports);
			}

			return m_Result;
		}
	}

	#region public class ReportTemplate
	/// <summary>
	/// 
	/// </summary>
	public class ReportTemplate
	{
		//Wu.Country@2007-11-26 12:39 modified some of the following code.
		#region static functions
		static public readonly string RegPath = @"SOFTWARE\Webb Electronics\WebbReport\ReportTemplates";
		//
		static public RegistryKey GetRootRegistry()
		{
			RegistryKey m_Key = Registry.CurrentUser.OpenSubKey(RegPath,true);
			if(m_Key==null)
			{
				m_Key = Registry.CurrentUser.CreateSubKey(RegPath);
			}
			return m_Key;
		}
		//
		static public bool UnregistryTemplate(string i_TemplateName, TemplateTypes i_Type,string i_Path)
		{
			string m_Catalog = ReportTemplate.ConvertTypeToString(i_Type);
			RegistryKey m_RootKey = GetRootRegistry();
			RegistryKey m_CatalogKey = m_RootKey.OpenSubKey(m_Catalog,true);
			if(m_CatalogKey==null) return false;
			string m_p = string.Format("{0}_{1}",m_Catalog,i_Path.GetHashCode().ToString());
			m_CatalogKey.DeleteSubKey(m_p,false);
			return true;
		}
		//
		static public bool RegstryTemplage(ReportTemplate i_Template)
		{
			string m_Catalog = ReportTemplate.ConvertTypeToString(i_Template.TemplateType);
			RegistryKey m_RootKey = GetRootRegistry();
			RegistryKey m_CatalogKey = m_RootKey.OpenSubKey(m_Catalog);
			if(m_Catalog==null)
			{
				m_RootKey.CreateSubKey(m_Catalog);
			}
			RegistryKey m_TemplateKey = m_RootKey.CreateSubKey(i_Template.TemplateName);
			//m_TemplateKey.SetValue();
			//m_CatalogKey.DeleteSubKey(i_TemplateName,false);
			return true;
		}
		//Scott@2007-11-27 16:48 modified some of the following code.
		static public string GetTemplateFilePath(string TemplateID)
		{
			RegistryKey m_RootKey = GetRootRegistry();
			string[] m_Catalog = m_RootKey.GetSubKeyNames();
			foreach(string i_Catalog in m_Catalog)
			{
				RegistryKey m_CatalogKey = m_RootKey.OpenSubKey(i_Catalog);
				string[] m_TemplateIDs = m_CatalogKey.GetSubKeyNames();
				foreach(string i_TemplateID in m_TemplateIDs)
				{
					if(i_TemplateID == TemplateID)
					{
						RegistryKey m_TemplateKey = m_CatalogKey.OpenSubKey(i_TemplateID);
						return m_TemplateKey.GetValue(Str_Path).ToString();
					}
				}
			}
			return string.Empty;
		}
		/// <summary>
		/// 
		/// </summary>
		public const string Str_Name = "TemplateName";
		public const string Str_TemplateType = "TemplateType";
		public const string Str_Path = "Path";
		public const string Str_RegDate = "RegDate";
		public const string Str_CreateDate = "CreateDate";
		//
		static public string ConvertTypeToString(TemplateTypes i_Type)
		{
			return i_Type.ToString();
		}
		static public TemplateTypes ConvertStringToType(string i_TypeName)
		{
			return (TemplateTypes)Enum.Parse(typeof(TemplateTypes),i_TypeName,true);
		}
		#endregion
		//fields
		string _FilePath = string.Empty;
		DateTime _CreateDate;
		DateTime _RegisterDate;
		string _TemplateName;
		TemplateTypes _TemplateType = TemplateTypes.Other;
		//properties
		public string FilePath
		{
			get{return this._FilePath;}
			set
			{
				if(!File.Exists(value))
				{
					throw new FileNotFoundException("File does not exist.",value);
				}
				this._FilePath = value;
			}
		}

		public string TemplateID
		{
			get
			{
				return string.Format("{0}_{1}",ConvertTypeToString(this.TemplateType),this.FilePath.GetHashCode());
			}
		}

		public string TemplateName
		{
			get
			{
				//return string.Format("{0}_{1}",this._TemplateType.ToString(),this.FileNameWithoutExtension);
				if(this.FilePath!=null)
				{
					this._TemplateName = System.IO.Path.GetFileNameWithoutExtension(this.FilePath);
				}
				return this._TemplateName;
			}
			//set{this._TemplateName = value;}
		}
		public DateTime CreateDate
		{
			get{return this._CreateDate;}
			set{this._CreateDate = value;}
		}
		public DateTime RegisterDate
		{
			get{return this._RegisterDate;}
			set{this._RegisterDate = value;}
		}
		public TemplateTypes TemplateType
		{
			get{return this._TemplateType;}
			set{this._TemplateType = value;}
		}
		public string TypeName
		{
			get{return ReportTemplate.ConvertTypeToString(this._TemplateType);}
		}
		public string FileName
		{
			get{return Path.GetFileName(this._FilePath);}
		}
		public string FileNameWithoutExtension
		{
			get{return Path.GetFileNameWithoutExtension(this._FilePath);}
		}
		//ctor
		public ReportTemplate(string i_FilePath,TemplateTypes i_Type)
		{
			if(!File.Exists(i_FilePath))
			{
				throw new FileNotFoundException("File dose not exist.",i_FilePath);
			}
			this._FilePath = i_FilePath;
			this._TemplateType = i_Type;
		}
		public ReportTemplate()
		{
		}
		//methods
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public bool Register()
		{
			RegistryKey m_RootKey = GetRootRegistry();
			string m_Catalog = ConvertTypeToString(this.TemplateType);
			RegistryKey m_CatalogKey = m_RootKey.OpenSubKey(m_Catalog,true);
			if(m_CatalogKey==null)
			{
				m_CatalogKey = m_RootKey.CreateSubKey(m_Catalog);
			}

			try
			{
				if(System.IO.File.Exists(this.FilePath))
				{
					//string m_p = string.Format("{0}_{1}",ConvertTypeToString(this.TemplateType),this.FilePath.GetHashCode().ToString());
					string m_p = this.TemplateID;
					m_CatalogKey.CreateSubKey(m_p);
					DateTime m_reporttime = System.IO.File.GetCreationTime(this.FilePath);
					DateTime m_registertime = DateTime.Now;

					RegistryKey m_Values = m_CatalogKey.OpenSubKey(m_p,true);
					m_Values.SetValue("TemplateName",this.TemplateName);
					m_Values.SetValue("TemplateType",ConvertTypeToString(this.TemplateType));
					m_Values.SetValue("Path",this.FilePath);
					m_Values.SetValue("RegDate",m_registertime.ToString());
					m_Values.SetValue("CreateDate",m_reporttime.ToString());
				}
			}
			catch (System.Exception e)
			{
                System.Diagnostics.Debug.WriteLine(e.Message);

				return false;
			}

			return true;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public bool Unregister()
		{
			return ReportTemplate.UnregistryTemplate(this.TemplateName,this.TemplateType,this.FilePath);
		}
		//
		public void SetValue(string i_Name,object i_value)
		{			
			switch(i_Name)
			{
				case Str_Name:
					this._TemplateName = i_value.ToString();
					return;
				case Str_TemplateType:
					this.TemplateType = ConvertStringToType(i_value.ToString());
					return;
				case Str_Path:
					this.FilePath = i_value.ToString();
					return;
				case Str_CreateDate:
					this.CreateDate = Convert.ToDateTime(i_value);
					return;
				case Str_RegDate:
					this.RegisterDate = Convert.ToDateTime(i_value);
					return;
			}
		}

		public string[] GetTemplateInfo()
		{
			string[] m_infor = new string[6];
			m_infor[0] = this.TemplateName;
			m_infor[1] = this.FilePath;
			m_infor[2] = this.RegisterDate.ToString();
			m_infor[3] = this.CreateDate.ToString();
			m_infor[4] = this.TypeName;
			m_infor[5] = this.TemplateID;
			return m_infor;
		}

		//Scott@2007-12-03 11:10 modified some of the following code.
		public string GetTemplateInfoText()
		{
			StringBuilder m_StringBuilder = new StringBuilder();
			
			m_StringBuilder.AppendFormat("{0} - {1}",this.TemplateType.ToString(),this.TemplateName);

			return m_StringBuilder.ToString();
		}

		public static bool CheckInfoValid(string[] i_Names,RegistryKey i_Key)
		{
			foreach(string _Name in i_Names)
			{
				if(_Name == Str_Path)
				{
					if(System.IO.File.Exists(i_Key.GetValue(_Name).ToString()))	return true;
				}
			}
			return false;
		}

	}
	#endregion

	#region public class ReportTemplateCollection : CollectionBase
	/// <summary>
	/// Collection for ReportTemplate
	/// </summary>
	public class ReportTemplateCollection : CollectionBase
	{
		public ReportTemplate this[ int index ]  
		{
			get  
			{
				return(  List[index]  as ReportTemplate);
			}
			set  
			{
				List[index] = value;
			}
		}
		public int Add(ReportTemplate i_Template)
		{
			return this.InnerList.Add(i_Template);
		}
		//Scott@2007-12-03 09:51 modified some of the following code.
		public void Add(ReportTemplateCollection i_Templates)
		{
			if(i_Templates == null) return;

			foreach(ReportTemplate i_Template in i_Templates)
			{
				this.InnerList.Add(i_Template);
			}
		}
		public void Remove(ReportTemplate i_Template)
		{
			this.InnerList.Remove(i_Template);
		}
		public int IndexOf( ReportTemplate value )  
		{
			return( List.IndexOf( value ) );
		}
		public void Insert( int index, ReportTemplate value )  
		{
			List.Insert( index, value );
		}
		public bool Contains( ReportTemplate value )  
		{			
			return( List.Contains( value ) );
		}
		public ReportTemplate FindTemplateByID(string i_TempateID)
		{
			foreach(ReportTemplate m_Template in this.InnerList)
			{
				if(m_Template.TemplateID==i_TempateID)
				{
					return m_Template;
				}
			}
			return null;
		}
		//Scott@2007-12-03 10:36 modified some of the following code.
		public ReportTemplate FindTemplateByName(TemplateTypes i_TemplateType,string i_TemplateName)
		{
			foreach(ReportTemplate m_Template in this.InnerList)
			{
				if(m_Template.TemplateType == i_TemplateType && m_Template.TemplateName == i_TemplateName)
				{
					return m_Template;
				}
			}
			return null;
		}
	}
	#endregion
}