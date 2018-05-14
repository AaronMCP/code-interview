using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HYS.IM.Common.HL7v2.Xml
{
    public class XmlHelper
    {
        public const string XML_DELARATION_FORMAT = "<?xml version= \"1.0\" encoding= \"{0}\"?>";
        private const string XmlDeclareBegin = "<?";
        private const string XmlDeclareEnd = "?>";
        public static string EatXmlDeclaration(string xmlString)
        {
            if (xmlString == null || xmlString.Length < 1) return "";
            int indexBegin = xmlString.IndexOf(XmlDeclareBegin);
            if (indexBegin < 0) return xmlString;
            int indexEnd = xmlString.IndexOf(XmlDeclareEnd, indexBegin + XmlDeclareBegin.Length);
            if (indexEnd < 0) return xmlString;
            indexEnd += XmlDeclareEnd.Length;
            if (indexEnd >= xmlString.Length) return xmlString;
            return xmlString.Remove(indexBegin, indexEnd - indexBegin);
        }
    }
}
