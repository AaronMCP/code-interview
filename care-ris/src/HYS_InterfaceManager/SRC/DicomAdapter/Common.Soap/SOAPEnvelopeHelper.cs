using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.Common.Objects.Logging;
using HYS.Common.Soap.DefaultConfiguration;
using System.IO;

namespace HYS.Common.Soap
{
    public class SOAPEnvelopeHelper
    {
        public const string HaoYiShengWSDLFile = "CSH.WSDL.xml";
        public const string HaoYiShengSOAPRequestTemplate = "CSH.SOAPRequestTemplate.xml";
        public const string HaoYiShengSOAPResponseTemplate = "CSH.SOAPResponseTemplate.xml";

        public static string HaoYiShengDefaultURI = "http://localhost:8080/HYSIM";
        public static string HaoYiShengDefaultSOAPAction = "http://www.HaoYiShenghealth.com/MessageCom";

        public static string HaoYiShengDefaultSOAPActionXmlNamespace = "http://www.HaoYiShenghealth.com/";
        public static string HaoYiShengDefaultHL7StandardXmlNamespace = "http://www.HaoYiSheng.com/HL7_STD";

        private ILogging _log;
        private string _cfgFolderPath;
        private bool _overWrite;

        public SOAPEnvelopeHelper(string cfgFolderPath, ILogging log, bool overWrite)
        {
            _log = log;
            _cfgFolderPath = cfgFolderPath;
            _overWrite = overWrite;
        }
        public SOAPEnvelopeHelper(string cfgFolderPath, ILogging log)
            : this(cfgFolderPath, log, false)
        {
        }

        public void WriteHaoYiShengWSDLFile()
        {
            ConfigHelper.WriteConfigToFolder(HaoYiShengWSDLFile, _cfgFolderPath, _overWrite, _log);
        }

        public static string GetHaoYiShengSOAPRequestTemplate()
        {
            return ConfigHelper.GetConfig(HaoYiShengSOAPRequestTemplate);
        }
        public static string GetHaoYiShengSOAPResponseTemplate()
        {
            return ConfigHelper.GetConfig(HaoYiShengSOAPResponseTemplate);
        }

        public static string EscapeXMLString(string str)
        {
            return XmlNodeTransformer.XmlEscape(str);
        }
        public static string DeEscapeXMLString(string str)
        {
            return XmlNodeTransformer.XmlDescape(str);
        }

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
            return xmlString.Remove(indexBegin, indexEnd - indexBegin).Trim();
        }
        public static string AddXmlDeclaration(string xmlString, string charSet)
        {
            return string.Format("<?xml version=\"1.0\" encoding=\"{0}\"?>{1}", charSet, xmlString);
        }
    }
}
