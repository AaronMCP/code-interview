using System;
using System.Xml;
using System.Text;

namespace HYS.HL7IM.Common.Xml
{
	/// <summary>
	/// 可映射到XML的对象
	/// </summary>
	public class XObject : XBase
	{
		public XObject()
		{
		}

		internal override void InnerLoad( XmlNodeReader reader, string endpoint )
		{
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
			while(!reader.EOF)
			{
				if( reader.Name == endpoint ) break;
                if (reader.NodeType != XmlNodeType.Element)
                {
                    reader.Read();
                    continue;
                }

				string strEleName = reader.Name.Trim();
                if (strEleName.Length == 0)
                {
                    reader.Read();
                    continue;
                }

                bool hasAlreadyMoveToNext = false;
                SetValue(strEleName, reader, ref hasAlreadyMoveToNext);

                if (!hasAlreadyMoveToNext) reader.Read();
			}
		}

        internal override string ToNakedString(Type t)
		{
			StringBuilder sb = new StringBuilder();

            string[] proplist = GetPropertyNames(t);
			foreach( string str in proplist )
				sb.Append( GetNodeXMLString( str ) );

			return sb.ToString();
		}

		internal override string XMLNodeName
		{
			get
			{
				Type t = this.GetType();
				if( t == null ) return "XObject";
                return XObjectHelper.GetCleanName(t.Name);
			}
		}
	}
}
