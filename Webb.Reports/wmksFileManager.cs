using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections;
using DevExpress.XtraPrinting.Native.WinControls;
using  DevExpress.XtraPrinting.Drawing ;
using DevExpress.XtraReports.UI ;


namespace Webb.Reports
{
	[Serializable]
	public class SerializableWatermark : IDisposable 
	{
		#region inner classes
		[Serializable]
		private class RangeParser 
		{
			string range = null;
			int[] values = new int[] {};

			public int[] GetIndexes(string range, int maxIndex) 
			{
				if(range == null)
					throw new ArgumentException("range");
				if(this.range == null || !this.range.Equals(range))
					values = GetValues(range, maxIndex);
				return values;
			}
			private int[] GetValues(string range, int maxIndex) 
			{
				ArrayList indexes = new ArrayList();
				string s = ValidateString(range);
				if(s.Length > 0) 
				{
					string[] items = s.Split(',');
					for(int i = 0; i < items.Length; i++) 
					{
						try 
						{
							int[] values = ParseElement(items[i], maxIndex);
							foreach(int val in values) 
							{
								if( !indexes.Contains(val - 1) )
									indexes.Add(val - 1);
							}
						} 
						catch {}
					}
				} 
				return (indexes.Count == 0) ? GetAllIndexes(maxIndex) :
					(int[])indexes.ToArray(typeof(int));
			}
			public string ValidateString(string s) 
			{
				if(s == null)
					return "";
				char[] chars  = new char[] {'0','1','2','3','4','5','6','7','8','9','-',','};
				for(int i = s.Length - 1; i >= 0; i--) 
				{
					if(Array.IndexOf(chars, s[i]) < 0)
						s = s.Remove(i, 1);
				}
				s = Replace(s, "--", "-");
				s = Replace(s, ",,", ",");
				s = s.TrimStart(',','-');
				s = s.TrimEnd(',');
				return s;
			}
			private int[] GetAllIndexes(int count) 
			{
				int[] indexes = new int[count];
				for(int i = 0; i < count; i++)
					indexes[i] = i;
				return indexes;
			}
			private int[] ParseElement(string  s, int maxIndex) 
			{
				string[] items = s.Split('-');
				int val1 = Convert.ToInt32(items[0]);
				if(items.Length == 1)
					return new int[] {val1};
		
				int val2 = items[1].Length > 0 ? Convert.ToInt32(items[1]) : maxIndex;
				if(val1 > val2) 
				{
					int val = val2;
					val2 = val1;
					val1 = val;
				}
				ArrayList values = new ArrayList();
				do 
				{
					values.Add(val1);
					val1++;
				} while(val1 <= val2);
				return (int[])values.ToArray(typeof(int));
			}
			private string Replace(string s, string oldValue, string newValue) 
			{
				while(s.IndexOf(oldValue) >= 0)
					s = s.Replace(oldValue, newValue);
				return s;
			}
		}
		#endregion

		protected static Font fDefaultFont = new Font("Verdana", 36);
		internal static Image CloneImage(Image img) 
		{
			return img != null ? (Image)img.Clone() : null;
		}


		private string text = "";
		private string pageRange = "";
		private int transparency = 50;
		private int imageTransparency = 0;
		private bool imageTiling = false;
		private Color foreColor = Color.Red;
		private ImageViewMode imageViewMode = ImageViewMode.Clip;
		private DirectionMode textDirection = DirectionMode.ForwardDiagonal;
		private ContentAlignment imageAlign = ContentAlignment.MiddleCenter;

		private Font font;
		private Image image;
		
		[NonSerialized]
		private Image actualImage;

		[NonSerialized]
		private StringFormat sf;

		private RangeParser rangeParser;

		private bool showBehind = true;

		[
		Description("Gets or sets a value indicating whether a watermark should be printed behind or in front of the contents on the page."),
		DefaultValue(true)]
		public bool ShowBehind { get { return showBehind; } set { showBehind = value; }
		}
		[Description("Gets or sets the range of pages which contain the watermark."), DefaultValue("")]
		public string PageRange 
		{ 
			get { return pageRange; } 
			set { pageRange = rangeParser.ValidateString(value); } 
		}
		[Description("Gets or sets the Watermark's picture."), DefaultValue(null)]
		public Image Image 
		{ 
			get { return image; } 
			set 
			{ 
				image = value;
				DisposeActualImage();
			}
		}
		[Description("Gets or sets the position of the Watermark's picture."), DefaultValue(ContentAlignment.MiddleCenter)]
		public ContentAlignment ImageAlign { get { return imageAlign; } set { imageAlign = value; }
		}
		[Description("Gets or sets the mode in which a picture Watermark is displayed."), DefaultValue(ImageViewMode.Clip)]
		public ImageViewMode ImageViewMode { get { return imageViewMode; } set { imageViewMode = value; }
		}
		[Description("Gets or sets a value indicating if a Watermark's picture should be tiled."), DefaultValue(false)]
		public bool ImageTiling { get { return imageTiling; } set { imageTiling = value; }
		}
		[Description("Gets or sets the incline of the  Watermark's text."), DefaultValue(DirectionMode.ForwardDiagonal)]
		public DirectionMode TextDirection { get { return textDirection; } set { textDirection = value; }
		}
		[Description("Gets or sets a Watermark's text."), 
		DefaultValue(""),		
		]
		public string Text { get { return text; } set { text = value; }
		}
		[Description("Gets or sets the font of the Watermark.")]
		public Font Font 
		{ 
			get { return font; } 
			set 
			{ 
				if(value != null) 
				{
					if(font != null) font.Dispose();
					font = (Font)value.Clone();
				}
			}
		}
		[Description("Gets or sets the foreground color of the Watermark's text."), DefaultValue(typeof(Color), "Red")]
		public Color ForeColor { get { return foreColor; } set { foreColor = value; }
		}
		
		public int Transparency 
		{ 
			get { return TextTransparency; } set { TextTransparency = value; }
		} 
		[Description("Gets or sets the transparency of the watermark's text."), DefaultValue(50)]
		public int TextTransparency 
		{ 
			get { return transparency; } set { transparency = Math.Max(1, Math.Min(value, 255) ); }
		} 
		[Description("Gets or sets the transparency of the watermark's image."), DefaultValue(0)]
		public int ImageTransparency 
		{ 
			get { return imageTransparency; } 
			set 
			{ 
				imageTransparency = Math.Max(0, Math.Min(value, 255));
				DisposeActualImage();
			}
		} 
	
		public SerializableWatermark() 
		{
			sf = new StringFormat();
			font = (Font)fDefaultFont.Clone();
			rangeParser = new RangeParser();
		}
		public void Dispose() 
		{
			sf.Dispose();
			DisposeGdiResources();
		}
		void DisposeGdiResources() 
		{
			if(font != null) 
			{
				font.Dispose();
				font = null;
			}
			if(image != null) 
			{
				image.Dispose();
				image = null;
			}
			DisposeActualImage();
		}
		void DisposeActualImage() 
		{
			if(actualImage != null) 
			{
				actualImage.Dispose();
				actualImage = null;
			}
		}	
		public void CopyFrom(Watermark watermark) 
		{
			if(watermark == null) 
				return;
			DisposeGdiResources();
			text = watermark.Text;
			showBehind = watermark.ShowBehind;
			pageRange = watermark.PageRange;
			transparency = watermark.TextTransparency;
			imageTransparency = watermark.ImageTransparency;
			imageTiling = watermark.ImageTiling;
			foreColor = watermark.ForeColor;
			imageViewMode = watermark.ImageViewMode;
			textDirection = watermark.TextDirection;
			imageAlign = watermark.ImageAlign;
			
			font = (Font)watermark.Font.Clone();
			image = CloneImage(watermark.Image);
		}	
	
	
		public XRWatermark ConvertTo() 
		{
			XRWatermark watermark=new XRWatermark();			
			watermark.Text=this.text;
			watermark.ShowBehind=showBehind ;
			watermark.PageRange=pageRange;
	        watermark.TextTransparency=transparency ;
        	watermark.ImageTransparency	 =imageTransparency ;
			 watermark.ImageTiling=imageTiling;
		     watermark.ForeColor=foreColor  ;
	         watermark.ImageViewMode=imageViewMode;
		     watermark.TextDirection=textDirection ;
		     watermark.ImageAlign=imageAlign  ;
			 watermark.Font = (Font)font.Clone();
	         watermark.Image = CloneImage(image);
			return watermark;
		}		
	}

	public class wmksFileManager
	{
		public static XRWatermark ReadFile(string i_Path)
		{
			if(System.IO.File.Exists(i_Path))
			{			
				try
				{
				    SerializableWatermark serilizeWatermark = Webb.Utilities.Serializer.Deserialize(i_Path) as SerializableWatermark;
				    
					XRWatermark xrwatermark=serilizeWatermark.ConvertTo();

					return xrwatermark;                     
				}
				catch(Exception ex)
				{
					Webb.Utilities.TextLog.WriteLine(string.Format("Read Watermark Error. Message:{0}", ex.Message));
				}				
			}
			return null;
		}
		public static void WriteFile(string i_Path)
		{
			XRWatermark xrwatermark=null;           

			if(System.IO.File.Exists(i_Path))
			{
				xrwatermark=ReadFile(i_Path);
			}	
		
            if(xrwatermark==null)xrwatermark=new XRWatermark();

			WatermarkEditorForm form = new WatermarkEditorForm();	

            form.StartPosition=FormStartPosition.CenterScreen;

			form.Assign(xrwatermark);	
			
			if(form.ShowDialog() == DialogResult.OK ) 
			{
				SerializableWatermark serilizeWatermark=new SerializableWatermark();

				serilizeWatermark.CopyFrom(form.Watermark);
				
				try
				{
					Webb.Utilities.Serializer.Serialize(serilizeWatermark,i_Path,true);						
				}
				catch(Exception ex)
				{									
					MessageBox.Show(string.Format("Saving Watermark error. Message:\n{0}", ex.Message));	
					Webb.Utilities.TextLog.WriteLine(string.Format("Saving Watermark error. Message:{0}", ex.Message));
				}
				finally
				{
                    serilizeWatermark.Dispose();
					
				}
			}
 
			form.Dispose();

			xrwatermark.Dispose();

			Environment.Exit(0);
			
		}
	}
}
