using System;
using System.Xml;
using System.Text;
using System.Reflection;
using System.Collections;

namespace HYS.HL7IM.Common.Xml
{
	/// <summary>
	/// 可映射到XML的对象集合
	/// </summary>
	public class XObjectCollection : XBase
	{
		public XObjectCollection()
		{
		}
		public XObjectCollection( Type t )
		{
			if( XObjectHelper.IsXBaseType( t ) == false )
				throw new Exception("Type of argument should be or be inherated from " + XObjectHelper.XBaseType.ToString() );
			
			_ChildItemType = t;
		}

		private Type _ChildItemType = XObjectHelper.XBaseType;
        internal virtual string XMLChildNodeName    // 20080708 : add virtual keyword to support customized node name in collection
		{
			get
			{
				return XObjectHelper.GetCleanName(_ChildItemType.Name);
			}
		}


		internal override void InnerLoad( XmlNodeReader reader, string endpoint )
		{
			this.Clear();
			if( reader == null ) return;
			bool emptyNode = reader.IsEmptyElement;

			while(reader.MoveToNextAttribute())
			{
				string attributeName = reader.Name;
				string attributeValue = reader.Value;
				SetValueEx( attributeName, attributeValue );
			}

			if( emptyNode ) return;

            reader.Read();
			while (!reader.EOF)
			{
				string strEleName = reader.Name.Trim();
                if (strEleName.Length == 0)
                {
                    reader.Read();
                    continue;
                }
				if( strEleName == endpoint ) break;

                bool hasAlreadyMoveToNext = false;

				object item = null;
				string tname = _ChildItemType.ToString();
				if( strEleName == XMLChildNodeName )
				{
					if( reader.NodeType == XmlNodeType.Element )
					{
						item = _ChildItemType.Assembly.CreateInstance( tname );

						XObjectHelper.XBaseType.InvokeMember( "InnerLoad", 
							BindingFlags.DeclaredOnly | 
							BindingFlags.Public | BindingFlags.NonPublic | 
							BindingFlags.Instance | BindingFlags.InvokeMethod,
							null, item, new object[]{reader,strEleName} );

						this.Add( item as XBase );
					}
					else
					{
						item = null;
					}

                    // assume that InnerLoad should not exceed the border of element
				}
				else
				{
                    SetValue(strEleName, reader, ref hasAlreadyMoveToNext);
				}

                if (!hasAlreadyMoveToNext) reader.Read();
			}
		}
        internal override string ToNakedString(Type t)
		{
			StringBuilder sb = new StringBuilder();

            string[] proplist = GetPropertyNames(t);
			foreach( string str in proplist )
				sb.Append( GetNodeXMLString( str ) );

			foreach( XBase item in _List )
			{
                sb.Append(item.ToFullString(XMLChildNodeName)); // 20080708 : get value from XMLChildNodeName property to support customized node name in collection
			}

			return sb.ToString();
		}

		internal override string XMLNodeName
		{
			get
			{
				Type t = this.GetType();
				if( t == null ) return "XObjectCollection";
                return XObjectHelper.GetCleanName(t.Name);
			}
		}

 
		private XBaseCollection _List = new XBaseCollection();
        public virtual XBase Add(XBase value)
		{
			//if( value == null || TypeHelper.IsXMLObjectType( value.GetType() ) == false )
            if (value == null)
            {
                throw new ArgumentNullException();
            }
            if (value.GetType() != _ChildItemType)
            {
                throw new Exception("类型不正确。请输入类型为" + XObjectHelper.XBaseType.ToString() + "的变量。_ChildItemType=" + _ChildItemType.ToString() + " valueType" + value.GetType().ToString() );
            }
			
			// Use base class to process actual collection operation
			return _List.Add(value);
		}

		public void Remove(XBase value)
		{
			// Use base class to process actual collection operation
			_List.Remove(value);
		}

		public void Clear()
		{
			_List.Clear();
		}
		public void Insert(int index, XBase value)
		{
			if( value == null || value.GetType() != _ChildItemType )
				throw new Exception("类型不正确。请输入类型为" + _ChildItemType.ToString() + "的变量。");
			
			// Use base class to process actual collection operation
			_List.Insert(index, value);
		}

		public bool Contains(XBase value)
		{
			// Use base class to process actual collection operation
			return _List.Contains(value);
		}

		public XBase this[int index]
		{
			// Use base class to process actual collection operation
			get { return (_List[index] as XBase); }
		}

		public int Count
		{
			get
			{
				return _List.Count;
			}
		}
		public int IndexOf(XBase value)
		{
			// Find the 0 based index of the requested entry
			return _List.IndexOf(value);
		}

		public XObjectCollection Copy()
		{
			XObjectCollection clone = new XObjectCollection( _ChildItemType );

			// Copy each reference across
			foreach(XBase c in _List)
				clone.Add(c);

			return clone;
		}
		
		public IEnumerator GetEnumerator()
		{
			return _List.GetEnumerator();
		}

        public IList GetList()
        {
            return _List.GetList();
        }
	}
}
