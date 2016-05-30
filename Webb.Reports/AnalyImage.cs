using System;
using System.Drawing;

using System.Data;   
using System.IO;  
using Webb.Collections;

using System.Runtime.Serialization;   
using Webb.Reports.DataProvider;
using System.Windows.Forms;
using System.Collections;



namespace Webb.Reports
{
	/// <summary>
	/// Summary description for AnalyImage.
	/// </summary>
	[Serializable]
	public class AnalyImage:ISerializable
	{

		protected string _FileName=string.Empty;
		protected Image _Image=null;
		public AnalyImage(string fileName, Image image)
		{	
			_FileName=fileName;
			_Image=image;
		}
    	public string FileName
		{
			get{ return _FileName; }
			set{ _FileName = value; }
		}

		public System.Drawing.Image Image
		{
			get{ return _Image; }
			set{ _Image = value; }
		}

		#region Serialization By Simon's Macro 2009-7-22 11:44:08
		public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			info.AddValue("_FileName",_FileName);
			info.AddValue("_Image",_Image,typeof(System.Drawing.Image));
		
		}

		public AnalyImage(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			try
			{
				_FileName=info.GetString("_FileName");
			}
			catch
			{
				_FileName=string.Empty;
			}
			try
			{
				_Image=(System.Drawing.Image)info.GetValue("_Image",typeof(System.Drawing.Image));
			}
			catch
			{
				_Image=null;
			}
		}
		#endregion		

	}


	[Serializable]
	public class AnalyImageCollection:WebbCollection
	{
		public AnalyImageCollection()
		{
		}
		public AnalyImage this[int index]
		{
			get{return this.innerList[index] as AnalyImage;}
			set{this.innerList[index]=value;}
		}
		public AnalyImage this[string filename]
		{
			get
			{
				foreach(AnalyImage analyimage in this)
				{
					if(analyimage.FileName==filename)return analyimage;
				}			
			    return null;
			}	
		}
		public void Add(string filename,Image image)		
		{	
			Image newImage=null;

			if(image!=null)newImage=new Bitmap(image);

			if(this[filename]!=null)
			{
				this[filename].Image=newImage;

				return;
			}	
           
			this.innerList.Add(new AnalyImage(filename,newImage));

		}	
		public  static WebbDataSource CreateDataSource()
		{
			WebbDataProvider dataProvider=new WebbDataProvider();

			WebbDataSource m_DBSource=new WebbDataSource();

			//Show wizard
			DialogResult m_Result=dataProvider.ShowWizard(null,m_DBSource);
		
			if(m_Result!=DialogResult.OK) return null;	
		
			Webb.Utilities.WaitingForm.ShowWaitingForm();

			Webb.Utilities.WaitingForm.SetWaitingMessage("Loading Datasource....");

			bool m_result = dataProvider.GetDataSource(m_DBSource);

			if(!m_result|| m_DBSource.DataSource.Tables.Count==0)
			{
				MessageBox.Show("No data in you selected dataSource!","Failed",MessageBoxButtons.OK,MessageBoxIcon.Information);

				return null;
			}
			
			SetDataSource(dataProvider,m_DBSource);			
		
			Webb.Utilities.WaitingForm.CloseWaitingForm();

			return m_DBSource;
		}


		private static void  SetDataSource(WebbDataProvider m_DBProvider,WebbDataSource m_DBSource)
		{
			ArrayList m_Fields = new ArrayList();

			foreach(System.Data.DataColumn m_col in m_DBSource.DataSource.Tables[0].Columns)
			{
                if (m_col.Caption == "{EXTENDCOLUMNS}" && m_col.ColumnName.StartsWith("C_")) continue;

				m_Fields.Add(m_col.ColumnName);
			}		

			Webb.Data.PublicDBFieldConverter.SetAvailableFields(m_Fields);

			Webb.Reports.DataProvider.VideoPlayBackManager.DataSource = m_DBSource.DataSource;	//Set dataset for click event

			Webb.Reports.DataProvider.VideoPlayBackManager.PublicDBProvider = m_DBProvider;

			Webb.Reports.DataProvider.VideoPlayBackManager.LoadAdvScFilters();	//Modified at 2009-1-19 13:48:30@Scott

            Webb.Reports.DataProvider.VideoPlayBackManager.ReadPictureDirFromRegistry();

		}

		#region Serialization By Simon's Macro 2009-7-22 11:44:08
		public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			base.GetObjectData(info,context);
		
		}

		public AnalyImageCollection(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context):base(info,context)
		{
			
		}
		#endregion		

	}

}
