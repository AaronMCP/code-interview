using System;
using System.Text;

namespace HYS.Common.Xml
{
	/// <summary>
	/// 可映射到XML的对象的支持者
	/// </summary>
    public class XObjectHelper
    {
        private static bool _compressEmptyElement = true;
        public static bool CompressEmptyElement
        {
            get { return _compressEmptyElement; }
            set { _compressEmptyElement = value; }
        }

        public static string GetEmptyElement(string name)
        {
            return GetEmptyElementWithAttribute(name, "");
        }
        public static string GetEmptyElementWithAttribute(string name, string attribute)
        {
            string str = attribute;
            if (str.Length > 1) str = " " + str;

            StringBuilder sb = new StringBuilder();
            sb.Append("<");
            sb.Append(name);
            sb.Append(str);

            if (_compressEmptyElement)
            {
                sb.Append(" />");
                return sb.ToString();
            }

            sb.Append("></");
            sb.Append(name);
            sb.Append(">");
            return sb.ToString();
        }

        public static string GetXMLElement(string name, object obj)
        {
            return GetXMLElement(name, obj, false);
        }
        public static string GetXMLElement(string name, object obj, bool isdata)
        {
            return GetXMLElementWithAttribute(name, "", obj, isdata);
        }

        public static string GetXMLElementWithAttribute(string name, string attribute, object obj)
        {
            return GetXMLElementWithAttribute(name, attribute, obj, false);
        }
        public static string GetXMLElementWithAttribute(string name, string attribute, object obj, bool isdata)
        {
            string text = (obj == null) ? "" : obj.ToString();
            if (text == null || text.Length < 1) return GetEmptyElementWithAttribute(name, attribute);

            string str = attribute;
            if (str.Length > 1) str = " " + str;

            StringBuilder sb = new StringBuilder();
            sb.Append("<");
            sb.Append(name);
            sb.Append(str);

            if (isdata)
            {
                sb.Append("><![CDATA[");
            }
            else
            {
                sb.Append(">");
            }

            sb.Append(text);

            if (isdata)
            {
                sb.Append("]]></");
            }
            else
            {
                sb.Append("</");
            }

            sb.Append(name);
            sb.Append(">\r\n");
            return sb.ToString();
        }

        public static Type XBaseType = typeof(XBase);
        public static Type XObjectType = typeof(XObject);
        public static Type XObjectCollectionType = typeof(XObjectCollection);
        public static Type XRawXmlStringAttributeType = typeof(XRawXmlStringAttribute);
        public static Type XCDataAttributeType = typeof(XCDataAttribute);
        public static Type XNodeAttributeType = typeof(XNodeAttribute);

        public static bool IsXBaseType(Type t)
        {
            if (t == null) return false;

            if (t == XBaseType)
                return true;
            else
                return IsXBaseType(t.BaseType);
        }
        public static bool IsXObjectType(Type t)
        {
            if (t == null) return false;

            if (t == XObjectType)
                return true;
            else
                return IsXObjectType(t.BaseType);
        }
        public static bool IsXObjectCollectionType(Type t)
        {
            if (t == null) return false;

            if (t == XObjectCollectionType)
                return true;
            else
                return IsXObjectCollectionType(t.BaseType);
        }

        public static string GetCleanName(string str)
        {
            if (str == null || str.Length < 1) return "";
            int i = str.IndexOf('`');
            string ostr = str;
            if (i >= 0) ostr = str.Substring(0, i);
            return ostr;
        }
    }
}
