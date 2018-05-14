//using System;
//using System.Collections.Generic;
//using System.Text;
//using HYS.IM.Common.Logging;
//using HYS.IM.Common.HL7v2.Xml.Transforming;

//namespace HYS.IM.Common.HL7v2.Xml
//{
//    public class HL7ToolkitTransformer : XmlTransformerBase
//    {
//        public const string DEVICE_NAME = "HL7Toolkit";

//        public HL7ToolkitTransformer(ILog log)
//            : base(log)
//        {
//        }

//        private const string XmlDeclareBegin = "<?";
//        private const string XmlDeclareEnd = "?>";
//        private string EatXmlDeclaration(string xmlString)
//        {
//            if (xmlString == null || xmlString.Length < 1) return "";
//            int indexBegin = xmlString.IndexOf(XmlDeclareBegin);
//            if (indexBegin < 0) return xmlString;
//            int indexEnd = xmlString.IndexOf(XmlDeclareEnd, indexBegin + XmlDeclareBegin.Length);
//            if (indexEnd < 0) return xmlString;
//            indexEnd += XmlDeclareEnd.Length;
//            if (indexEnd >= xmlString.Length) return xmlString;
//            return xmlString.Remove(indexBegin, indexEnd - indexBegin);
//        }

//        public override bool TransformHL7v2ToXml(string hl7Msg, out string xmlMsg)
//        {
//            xmlMsg = "";
//            if (hl7Msg == null || hl7Msg.Length < 1) return false;

//            try
//            {
//                // it would be better if we use"UTF-16" here
//                // see function TransformXmlToHL7v2() for details
//                HL7Message msg = new HL7Message(hl7Msg, HL7Message.HL7_SYNTAX, "UTF-8");
//                using (ByteArrayOutputStream os = new ByteArrayOutputStream())
//                {
//                    msg.outputXML(os);
//                    xmlMsg = os.toString();
//                    xmlMsg = EatXmlDeclaration(os.toString()).Trim();
//                }
//                return true;
//            }
//            catch (Exception e)
//            {
//                _log.Write(e);
//                _log.Write(LogType.Error, hl7Msg);
//                return false;
//            }
//        }

//        public override bool TransformXmlToHL7v2(string xmlMsg, out string hl7Msg)
//        {
//            hl7Msg = "";
//            if (xmlMsg == null || xmlMsg.Length < 1) return false;

//            try
//            {
//                // there will be some problem if you use "UTF-16" here
//                // see HL7Adapter.Note.txt for details
//                HL7Message msg = new HL7Message(xmlMsg, HL7Message.XML_SYNTAX, "UTF-8");
//                using (ByteArrayOutputStream os = new ByteArrayOutputStream())
//                {
//                    msg.outputHL7(os);
//                    hl7Msg = os.toString();
//                }
//                return true;
//            }
//            catch (Exception e)
//            {
//                _log.Write(e);
//                _log.Write(LogType.Error, xmlMsg);
//                return false;
//            }
//        }
//    }
//}
