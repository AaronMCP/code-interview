using System;
using System.Collections.Generic;
using HYS.Common.Xml;

namespace HYS.XmlAdapter.Common.Objects
{
    public class XmlElement : XObject
    {
        public const char XPathSeperator = '/';
        public const string MultiItemSymbol = "[]";

        public string GetXPath()
        {
            string str = XPath;
            if (str == null) return "";
            return str.Replace(MultiItemSymbol, "");
        }
        public string GetXDirectory()
        {
            if (_xPath == null || _xPath.Length < 1) return "";
            int index = _xPath.LastIndexOf(XPathSeperator);
            if (index < 0) return "";
            return _xPath.Substring(0, index);
        }

        private string _xPath = "";
        public string XPath
        {
            get { return _xPath; }
            set { _xPath = value; }
        }

        private XIMType _typeCode = XIMType.STR;
        public XIMType Type
        {
            get { return _typeCode; }
            set { _typeCode = value; }
        }

        private string _classTypeName = "";
        public string ClassTypeName
        {
            get { return _classTypeName; }
            set { _classTypeName = value; }
        }
        
        [XNode(false)]
        public Type ClassType
        {
            get { return System.Type.GetType(ClassTypeName); }
            set { ClassTypeName = value.ToString(); }
        }

        public XmlElement Clone()
        {
            XmlElement ele = new XmlElement();
            ele._classTypeName = _classTypeName;
            ele._typeCode = _typeCode;
            ele._xPath = _xPath;
            return ele;
        }
        public string GetLastName()
        {
            if (_xPath == null || _xPath.Length < 1) return "";
            string[] list = _xPath.Split(XPathSeperator);
            if (list.Length < 1) return "";
            string name = list[list.Length - 1];
            name = name.TrimEnd(']').TrimEnd('[');
            for (int i = 2; i < list.Length - 1; i++) name = _prefix + name;
            return name;
        }
        private const string _prefix = "  ";

        public bool IsChildOf(XmlElement ele)
        {
            if (ele == null || ele.XPath == null || ele.XPath.Length < 1) return false;
            int index = _xPath.Replace(ele.XPath + XPathSeperator, "").IndexOf(XPathSeperator);
            return index < 0;
        }
        public bool IsRoot()
        {
            if (_xPath == null || _xPath.Length < 1) return true;
            char[] chrList = _xPath.ToCharArray();
            int count = 0;
            foreach (char chr in chrList)
            {
                if (chr == XPathSeperator) count++;
            }
            return count <= 2;
        }

        public XmlElement()
        {
        }
        public XmlElement(string xPath)
        {
            _xPath = xPath;
        }
        public XmlElement(string xPath, Type type)
        {
            _xPath = xPath;
            ClassType = type;
            _typeCode = XIMType.complex_type;
        }
        public XmlElement(string xPath, XIMType tcode)
        {
            _xPath = xPath;
            _typeCode = tcode;
        }
    }
}
