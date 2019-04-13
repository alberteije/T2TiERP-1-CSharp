using System;
using System.Xml;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;
using System.Reflection;
using System.Configuration;
using System.IO;

namespace UniNFeLibrary
{
	public class XMLIniFile
	{
        public enum Store { SameElement, MultiElements };

        public XmlDocument xmldoc{get; private set;}
		private string xmlfilename = "";
		private string aDocElementname = "Principal";
		private int aDocElementVersion = 1;
		private Store aXmlFormat = Store.SameElement;
		private bool Modified = true;
        public string namespaceURI { get; set; }
      
		const string stxmlini_ValueNotExists = "Value '{0} - {1}' not exists!";
		const string stxmlini_PathNotExists = "Path '{0}' not exists!";
		const string stxmlini_CannotCreatePath = "Cannot create '{0}'";
		const string stxmlini_Version = "Version";

		public XMLIniFile()
		{
			xmldoc = new XmlDocument();
			DocElementname = "Principal";
			DocElementVersion = 1;
			XmlFormat = Store.SameElement;
		}

		public XMLIniFile(string filename)
		{
			xmlfilename = filename;
			xmldoc = new XmlDocument();
			DocElementname = "Principal";
			DocElementVersion = 1;
			aXmlFormat = Store.SameElement;
			try
			{
				xmldoc.Load(filename);
			}
			catch{}
			Modified = false;
		}

		public XMLIniFile(string aaDocElementname, int aaVersion, Store aaStore)
		{
			xmldoc = new XmlDocument();
			DocElementname = aaDocElementname;
			DocElementVersion = aaVersion;
			XmlFormat = aaStore;
		}

		public XMLIniFile(string aaDocElementname, int aaVersion, Store aaStore, string filename)
		{
			xmldoc = new XmlDocument();
			xmlfilename = filename;
			DocElementname = aaDocElementname;
			DocElementVersion = aaVersion;
			XmlFormat = aaStore;
			Modified = false;
			try
			{
				xmldoc.Load(filename);
			}
			catch{}
		}
        ~XMLIniFile()
        {
            this.Save();
        }

		#region Property

		public string Filename
		{
			get{return xmlfilename;}
			set{xmlfilename = value;}
		}

		public string DocElementname
		{
			get{return aDocElementname;}
			set{aDocElementname = value;}
		}

		public int DocElementVersion
		{
			get{return aDocElementVersion;}
			set{aDocElementVersion = value;}
		}

		public Store XmlFormat
		{
			get{return aXmlFormat;}
			set{aXmlFormat = value;}
		}

        //public bool ReplaceValue
        //{
        //    get { return _replaceValue; }
        //    set { _replaceValue = value; }
        //}

		#endregion

		#region Save Functions

		public void Save()
		{
            if (this.xmlfilename == "")
                return;

			try
			{
				if (Modified)
					xmldoc.Save(this.xmlfilename);

                Modified = false;
			}
			catch//( Exception ex )
			{
				//MessageBox.Show( "Save: "+ex.Message );
			}
		}

		public void SaveAs(string filename)
		{
            if (string.IsNullOrEmpty(filename))
                return;

			try
			{
				xmldoc.Save(filename);
                Modified = false;
            }
			catch//(Exception ex)
			{
				//MessageBox.Show("SaveAs: "+ex.Message);
			}
		}

		#endregion

		#region Load Functions

		public void Load()
		{
			this.Load(this.xmlfilename);
			Modified = false;
		}

		public void Load(string aFileName)
		{
			xmlfilename = aFileName;
			xmldoc.Load(aFileName);
			Modified = false;
		}

		#endregion

		#region Delete Functions

		public bool DeletePath(string Path)	// OK
		{
			XmlNode n = GetPathNode(Path, false);
			if (n != null)
			{
				n.RemoveAll();
                this.Modified = true;

				XmlNode _RootNode = xmldoc.DocumentElement;
				if (_RootNode!=null)
				{
					try
					{
						_RootNode.RemoveChild(n);
					}
					catch{}
					return true;
				}
			}
			return (n!=null);
		}

		public void DeleteValue(string Path, string ValueSection)   //ok
		{
			XmlNode n = GetPathNode(Path, false);
			if (n != null)
			{
				//MessageBox.Show(Path+"\n\r"+ValueSection);
				if (n.Attributes != null && n.Attributes.GetNamedItem( ValueSection )!=null)
				{
					n.Attributes.RemoveNamedItem(ValueSection);
                    this.Modified = true;
					return;
				}
				for(int i = 0; i < n.ChildNodes.Count; ++i)
					if (n.ChildNodes[i].LocalName == ValueSection)
					{
						XmlNode d = n.ChildNodes[i];
                        n.RemoveChild(d);
                        this.Modified = true;
						return;
					}
			}
		}

		#endregion

		#region Read Functions

		public System.Drawing.Color ReadValue(string Path, string ValueSection, System.Drawing.Color Default)
		{
            string value = this.ReadThisValue(Path, ValueSection, Default.ToString());

			value = value.Replace("]","");
			value = value.Replace("Color [","");

			return System.Drawing.Color.FromName( value );
		}

		public Font ReadValue(string Path, Font Value)
		{
			if (ValueExists(Path, "Name"))
			{
				FontStyle style = FontStyle.Regular;

                string FontName = this.ReadThisValue(Translate(Path), "Name", Value.Name);

				float Size = (float)Convert.ToDouble( ReadThisValue( Translate(Path), "Size", Value.Size.ToString() ) );
				if (this.ReadThisValue( Translate(Path), "Bold", Value.Bold.ToString() )=="True")
					style |= FontStyle.Bold;
                if (this.ReadThisValue(Translate(Path), "Italic", Value.Italic.ToString()) == "True")
					style |= FontStyle.Italic;
                if (this.ReadThisValue(Translate(Path), "Underline", Value.Underline.ToString()) == "True")
					style |= FontStyle.Underline;
                if (this.ReadThisValue(Translate(Path), "Strikeout", Value.Strikeout.ToString()) == "True")
					style |= FontStyle.Strikeout;

				return new Font(FontName, Size, style);
			}
			else
				return Value;
		}

		public bool ReadValue(string Path, string ValueSection, bool Default)
		{
            if (ValueExists(Path, ValueSection))
                return Convert.ToBoolean(ReadThisValue(Path, ValueSection));
            return Default;
		}

		public DateTime ReadValue(string Path, string ValueSection, DateTime Default)
		{
			if (ValueExists(Path, ValueSection))
				return Convert.ToDateTime( ReadThisValue( Path, ValueSection ) );
			return Default;
		}

		public double ReadValue(string Path, string ValueSection, double Default)
		{
			if (ValueExists(Path, ValueSection))
				return Convert.ToDouble( "0" + ReadThisValue( Path, ValueSection ) );
			return Default;
		}

        public Single ReadValue(string Path, string ValueSection, Single Default)
        {
            if (ValueExists(Path, ValueSection))
            {
                string str = ReadThisValue(Path, ValueSection).Replace(".",",");
                if (str != "")
                    return Convert.ToSingle(str);
            }
            return Default;
        }

        public Int32 ReadValue(string Path, string ValueSection, Int32 Default)
		{
            if (ValueExists(Path, ValueSection))
                return Convert.ToInt32("0" + ReadThisValue(Path, ValueSection));
		    return Default;
		}

		public string ReadValue(string Path, string ValueSection, string Default)
		{
			if (ValueExists(Path, ValueSection))
				return ReadThisValue( Path, ValueSection );
			return Default;
		}
	
		public ArrayList ReadSections()
		{
			ArrayList List = new ArrayList();
			this.ReadSections(List);
			return List;
		}

		public void ReadSections(ArrayList List)	//ok
		{
			List.Clear();
			CheckInitialized();
			XmlNode _RootNode = xmldoc.DocumentElement;
			if (_RootNode!=null)
				for (int i = 0; i < _RootNode.ChildNodes.Count; ++i)
					if (_RootNode.ChildNodes[i].NodeType == XmlNodeType.Element)
						List.Add( _RootNode.ChildNodes[i].LocalName );
		}

        public ArrayList ReadFullSections(string path)
        {
            ArrayList List = new ArrayList();
            XmlNode _RootNode = GetPathNode(path, false);
            if (_RootNode != null)
            {
                for (int i = 0; i < _RootNode.ChildNodes.Count; ++i)
                {
                    string _path = path + "\\" + _RootNode.ChildNodes[i].Name;
                    if (List.IndexOf(_path)<0)
                        List.Add(_path);
                }
            }
            return List;
        }

        public void ReadSections(string Path, ArrayList List, bool IncludePath)
		{
			List.Clear();
			XmlNode _RootNode = GetPathNode(Path, false);
			if (_RootNode!=null)
                for (int i = 0; i < _RootNode.ChildNodes.Count; ++i)
                {
                    if (_RootNode.ChildNodes[i].NodeType == XmlNodeType.Element)
                    {
                        List.Add((IncludePath) ?
                            Path + @"\\" + _RootNode.ChildNodes[i].LocalName :
                            _RootNode.ChildNodes[i].LocalName);
                    }
                }
		}

        public ArrayList ReadSections(string Path, bool IncludePath)
		{
			ArrayList List = new ArrayList();
			this.ReadSections(Path, List, IncludePath);
			return List;
		}

		public ArrayList ReadSection(string Path)
		{
			ArrayList List = new ArrayList();
			this.ReadSection(Path, List);
			return List;
		}

		public void ReadSection(string Path, ArrayList List)
		{
			List.Clear();
			try
			{
				int C = Convert.ToInt16( ReadThisValue( Path,"ItemCount" ) );
				for (int I = 0; I < C; ++I)
				{
					string _Section = "Item"+Convert.ToString(I);
					string _Path = Path + "\\" + _Section;

					if (ValueExists(Path, _Section))
						List.Add( _Path + "=" + ReadThisValue( Path, _Section ));
				}
			}
			catch{}
		}

		public ArrayList ReadSectionValues(string Path)
		{
			ArrayList List = new ArrayList();
			this.ReadSectionValues(Path, List);
			return List;
		}

		public void ReadSectionValues(string Path, ArrayList List)
		{
			int I,C;
			string Section;

			List.Clear();
			try
			{
				C = Convert.ToInt16( ReadThisValue( Path,"ItemCount" ) );
				for (I = 0; I < C; ++I)
				{
					Section = "Item"+Convert.ToString(I);
					if (ValueExists(Path, Section))
						List.Add(ReadThisValue( Path, Section ));
				}
			}
			catch{}
		}

        public string ReadInnerXml(string path)	//ok
        {
            CheckInitialized();
            XmlNode _RootNode = GetPathNode(path, false);
            if (_RootNode != null)
                return _RootNode.InnerXml;
            return "";
        }

        public string ReadValue(string Path)
		{
			XmlNode n = GetPathNode(Path, false);
			if (n != null)
				return n.InnerText;
			else
				return "";
		}

		protected string ReadThisValue(string Path, string ValueSection, string Default)	//OK
		{
			if (ValueExists(Path, ValueSection))
				Default = this.ReadThisValue(Path, ValueSection);
			return Default;
		}

		protected string ReadThisValue(string Path, string ValueSection)	//OK
		{
			XmlNode n = GetPathNode(Path, false);
			try
			{
				if (n != null)
				{
					if (n.Attributes.GetNamedItem( ValueSection )!=null)
					{
						n = n.Attributes.GetNamedItem( ValueSection );
						return n.Value;
					}
					for(int i=0; i < n.ChildNodes.Count; ++i)
						if (n.ChildNodes[i].LocalName == ValueSection)
						{
							return n.ChildNodes[i].InnerText;
						}
				}
			}
			catch
			{
			}
			return "";
		}

		#endregion

		#region Write Functions

		public void WriteFontValue(string Path, Font Value)
		{
			WriteThisValue( Translate(Path), "Name", Value.Name );
			WriteThisValue( Translate(Path), "Size", Value.Size.ToString() );
			WriteThisValue( Translate(Path), "Bold", Value.Bold.ToString() );
			WriteThisValue( Translate(Path), "Italic", Value.Italic.ToString() );
			WriteThisValue( Translate(Path), "Underline", Value.Underline.ToString() );
			WriteThisValue( Translate(Path), "Strikeout", Value.Strikeout.ToString() );
			//WriteThisValue( Translate(Path), "Style", Value.Style.ToString() );
		}

		public void WriteValue(string Path, string ValueSection, bool Value)	//ok
		{
			WriteThisValue( Translate(Path), Translate(ValueSection), Convert.ToString(Value) );
		}

		public void WriteValue(string Path, string ValueSection, DateTime Value)
		{
			WriteThisValue( Translate(Path), Translate(ValueSection), Convert.ToString(Value) );
		}

		public void WriteValue(string Path, string ValueSection, int Value)	//ok
		{
			WriteThisValue( Translate(Path), Translate(ValueSection), Convert.ToString(Value) );
		}

		public void WriteValue(string Path, string ValueSection, double Value)	//ok
		{
			WriteThisValue( Translate(Path), Translate(ValueSection), Convert.ToString(Value) );//.Replace(',','.') );
		}

		public void WriteValue(string Path, string ValueSection, string Value)	//ok
		{
			WriteThisValue( Translate(Path), Translate(ValueSection), Value );
		}

		public void WriteValue(string Path, ArrayList List)	//ok
		{
			DeletePath(Path);
			WriteThisValue( Path,"ItemCount", List.Count.ToString());
			for (int I = 0; I < List.Count; ++I)
				WriteThisValue( Path, "Item"+Convert.ToString(I), List[I].ToString());
		}

		public void WriteValue(string Path, string Value)	//ok
		{
			try
			{
				XmlNode n = GetPathNode(Path, true);
                if (n != null)
                {
                    Modified = true;
                    if (!string.IsNullOrEmpty(Value))
                        n.InnerText = Value;
                }
			}
			catch
			{
				MessageBox.Show( String.Format(stxmlini_CannotCreatePath, Path) );
			}
		}

        public void WriteInnerXml(string Path, string Value)	//ok
        {
            try
            {
                XmlNode n = GetPathNode(Path, true);
                if (n != null)
                {
                    Modified = true;
                    n.InnerXml = Value;
                }
            }
            catch
            {
                MessageBox.Show(String.Format(stxmlini_CannotCreatePath, Path));
            }
        }

        public void WriteAttribute(string Path, string ValueSection, string Value)
        {
				XmlNode node = GetPathNode(Path, true);
                if (node != null)
                {
                    Modified = true;
                    if (node.Attributes.GetNamedItem(ValueSection) != null)
                    {
                        node = node.Attributes.GetNamedItem(ValueSection);
                        node.Value = Value;
                    }
                    else
                    {
                        XmlAttribute xmlSection;

                        xmlSection = xmldoc.CreateAttribute(ValueSection);
                        xmlSection.Value = Value;
                        node.Attributes.Append(xmlSection);
                    }
                }
        }

        protected void WriteThisValue(string Path, string ValueSection, string Value)
		{
			try
			{
				XmlNode node = GetPathNode(Path, true);
				if (node!=null)
				{
					if (node.Attributes.GetNamedItem( ValueSection )!=null)
					{
						node = node.Attributes.GetNamedItem( ValueSection );
						node.Value = Value;
					}
					else
					{
						XmlNode xmlInf;
						XmlAttribute xmlSection;

						switch(aXmlFormat)
						{
							case Store.SameElement:
							{
								//
								// cria um elemento no mesmo path+item
								// 
								xmlSection = xmldoc.CreateAttribute(ValueSection);
								xmlSection.Value = Value;
								node.Attributes.Append(xmlSection);
								break;
							}

							case Store.MultiElements:
							{
								//
								// cria um elemento no path com niveis
								// 
								for(int i=0; i<node.ChildNodes.Count; ++i)
									if (node.ChildNodes[i].LocalName==ValueSection)
									{
										node.ChildNodes[i].InnerText = Value;
										return;
									}
								xmlInf = xmldoc.CreateElement("", ValueSection, "");
                                if (!string.IsNullOrEmpty(Value))
								    xmlInf.InnerText = Value;
								node.AppendChild(xmlInf);
								break;
							}
						}
					}
                    Modified = true;
                }
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message+"\n\r"+String.Format("N„o pode salvar '{0} - {1} = {2}'", Path, ValueSection, Value));
			}
		}

		#endregion

		#region Misc Functions

        public bool LoadForm(Form xform, string aSection)
		{
			string section = xform.Name + aSection;

            if (!this.ValueExists(section, "top") && !this.ValueExists(section, "WindowState"))
			{
                if (xform.StartPosition == FormStartPosition.Manual && xform.MdiParent==null)
                {
                    xform.Left = (SystemInformation.VirtualScreen.Width - xform.Width) / 2;
                    xform.Top = (SystemInformation.VirtualScreen.Height - xform.Height) / 2;
                }
				return false;
			}
			else
			{
                if (this.ValueExists(section, "WindowState"))
                {
                    switch (this.ReadValue(section, "WindowState", 0))
                    {
                        case 2: 
                            xform.WindowState = FormWindowState.Maximized; 
                            break;
                        default: 
                            xform.WindowState = FormWindowState.Normal; 
                            break;
                    }
                }
				if (xform.WindowState == FormWindowState.Normal)
				{
					int left	= this.ReadValue(section, "left",xform.Location.X);//Left);
					int top		= this.ReadValue(section, "top", xform.Location.Y);//Top);

					int width  = xform.Size.Width, 
						height = xform.Size.Height;

					switch (xform.FormBorderStyle)
					{
						case FormBorderStyle.Sizable:
						case FormBorderStyle.SizableToolWindow:
							width	= this.ReadValue(section, "width",	xform.Size.Width);
							height	= this.ReadValue(section, "height", xform.Size.Height);
							break;
					}

					if (xform.MdiParent==null)
					{
						if ((height + top) > SystemInformation.VirtualScreen.Height)
							top = 0;
						if ((width + left) > SystemInformation.VirtualScreen.Width)
							left = 0;
					}
					xform.StartPosition = FormStartPosition.Manual;
					xform.Location = new Point(left, top);
					xform.Size = new Size(width, height);
				}
				return true;
			}
		}

		public void SaveForm(Form xform, string aSection)
		{
			string section = xform.Name + aSection;

			if (xform.WindowState != FormWindowState.Maximized)
			{
				this.WriteValue(section, "left",	xform.Location.X);
				this.WriteValue(section, "top",		xform.Location.Y);
				switch (xform.FormBorderStyle)
				{
					case FormBorderStyle.Sizable:
					case FormBorderStyle.SizableToolWindow:
						this.WriteValue(section, "width",	xform.Size.Width);
						this.WriteValue(section, "height",	xform.Size.Height);
						break;
					default:
						this.DeleteValue(section, "width");
						this.DeleteValue(section, "height");
						break;
				}
				this.DeleteValue(section, "WindowState");
			}
			else
			{
				this.DeleteValue(section, "left");
				this.DeleteValue(section, "top");
				this.DeleteValue(section, "width");
				this.DeleteValue(section, "height");
				this.WriteValue(section, "WindowState",(int)xform.WindowState);
			}
		}

        public string XmlText
        {
            get
            {
                try
                {
                    CheckInitialized();
                    return xmldoc.DocumentElement.OuterXml;
                }
                catch
                {
                    return "";
                }
            }
            set
            {
                xmldoc.LoadXml(value);
                Modified = true;
            }
        }

        protected void CheckInitialized()	//ok
		{
			if (xmldoc.DocumentElement == null)
			{
				if (DocElementname.Length==0)
					DocElementname = "Principal";

				Modified = true;

                XmlDeclaration node;
                node = xmldoc.CreateXmlDeclaration("1.0", "iso-8859-1", "yes");
                xmldoc.InsertBefore(node, xmldoc.DocumentElement);
				//XmlNode childNode = xmldoc.AppendChild(node);
                XmlNode xmlInf = xmldoc.CreateElement(DocElementname, this.namespaceURI);
                xmldoc.AppendChild(xmlInf);

				//XmlNode xmlInf = xmldoc.CreateElement("", DocElementname, "");
                if (DocElementVersion > 0)
                {
                    XmlAttribute xmlVersion = xmldoc.CreateAttribute("Version");
                    xmlVersion.Value = Convert.ToString(DocElementVersion);
                    xmlInf.Attributes.Append(xmlVersion);
                    /*childNode = */
                    xmldoc.AppendChild(xmlInf);
                }
			}
		}

        protected string ConvertToOEM(string FBuffer)
        {
            const string FAnsi = (" ·ÈÌÛ˙¡…Õ”⁄Á«‡ËÏÚ˘¿»Ã“Ÿ„ı√’∫™ß—‚‰ÂÍÎÔÓƒ≈Ù˚ˇ÷‹Ò¸¬");
            const string FOEM = (" aeiouAEIOUcCaeiouAEIOUaoAOoa.NaaaeeiiAAouyOUnuA");
            int L, P;
            char X;
            string result = "";

            for (L = 0; L < FBuffer.Length; ++L)
            {
                X = (char)FBuffer[L];
                P = FAnsi.IndexOf(X);
                if (P >= 0) X = FOEM[P];
                result += X;
            }
            return result;
        }

        protected string Translate(string Value)	// OK
		{
			int I;
			string T;
			string result;
			char X;

			result = "";
			T = ConvertToOEM(Value);
			try
			{
				result = XmlConvert.VerifyName(T);
			}
			catch
			{
				for (I=0; I < T.Length; ++I)
				{
					X = T[I];
					if ((X=='\\') || (((X>='a') && (X<='z')) || ((X>='A') && (X<='Z')) || ((X>='0') && (X<='9'))))
						result += X;
					else
						result += '_';
				}
                result = result.Replace(' ', '_');
			}
			return result;
		}

		protected XmlNode _FindNode(XmlNode _RootNode, string _Nodename)	//OK
		{
			XmlNode result = _RootNode.SelectSingleNode(_Nodename);
			if (result==null)
			{
				for (int i = 0; i < _RootNode.ChildNodes.Count; ++i)
					if (_RootNode.ChildNodes[i].NodeType == XmlNodeType.Element)
						if ( _RootNode.ChildNodes[i].LocalName == _Nodename) 
							return _RootNode.ChildNodes[i];
				return null;
			}
			return result;
		}

		protected ArrayList ParseString(string s)	// OK
		{
			ArrayList slist = new ArrayList();
			string[] a = new string[0];
			a = s.Split('\\');
			for (int c1 = 0; c1 < a.Length; ++c1)
				if (a[c1].ToString().Trim()!="")
					slist.Add( a[c1] );

			return slist;
		}

        public void AddValue(XmlNode node, string Path, string Section, string value)
        {
        }

		public XmlNode GetPathNode(string NodePath, bool CanCreate)
		{
			XmlNode lnode;
			XmlNode Result = null;
			ArrayList s1 = new ArrayList();

			CheckInitialized();
			Result = xmldoc.DocumentElement;
			try
			{
				s1 = ParseString( Translate(NodePath) );
				for (int i = 0; i < s1.Count; ++i)
				{
					lnode = Result;
					Result = _FindNode(lnode, Translate(s1[i].ToString()));
					if (Result == null)// || this.ReplaceValue)
						if (CanCreate)
						{
							XmlNode xmlProp = xmldoc.CreateElement("", Translate(s1[i].ToString()), "");
							Result = lnode.AppendChild(xmlProp);
						}
						else 
						{
							Result = null;
							break;
						}
				}
			}
			catch// (Exception ex)
			{
                Result = null;
				//MessageBox.Show("Erro em 'GetNodePath()': "+ex.Message);
			}
			return Result;
		}

		public bool ValueExists(string Path, string ValueSection)	// OK
		{
			XmlNode n = GetPathNode(Path, false);
			if (n!=null)
			{
				if ((n.ChildNodes.Count>0) && (n.HasChildNodes))
					for (int i=0; i<n.ChildNodes.Count; ++i)
						if (n.ChildNodes[i].LocalName == ValueSection)
							return true;

				return n.Attributes.GetNamedItem( ValueSection ) != null;
			}
			return false;
		}

		public string FullPathNode(XmlNode node)
		{
			string result = "";
			while (node.ParentNode!=null)
			{
				result = node.LocalName + "\\" + result;
				node = node.ParentNode;
			}
			return result;
		}

		#endregion
    

    }
}