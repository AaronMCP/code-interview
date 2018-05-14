//using System;
//using System.Collections.Generic;
//using System.Text;
//using HYS.IM.Common.Logging;
//using NHapi.Base.Parser;
//using NHapi.Base.Model;

//namespace HYS.IM.Common.HL7v2.Xml
//{
//    public class NHApiTransformer : XmlTransformerBase
//    {
//        public const string DEVICE_NAME = "NHApi";

//        public NHApiTransformer(ILog log)
//            : base(log)
//        {
//        }

//        private PipeParser hl7Parser = new PipeParser();
//        private DefaultXMLParser xmlParser = new DefaultXMLParser();

//        public override bool TransformHL7v2ToXml(string hl7Msg, out string xmlMsg)
//        {
//            xmlMsg = "";
//            if (hl7Msg == null || hl7Msg.Length < 1) return false;

//            try
//            {
//                IMessage msg = hl7Parser.Parse(hl7Msg);
//                xmlMsg = xmlParser.Encode(msg);
//                return true;
//            }
//            catch (Exception e)
//            {
//                _log.Write(e);
//                return false;
//            }
//        }

//        public override bool TransformXmlToHL7v2(string xmlMsg, out string hl7Msg)
//        {
//            hl7Msg = "";
//            if (xmlMsg == null || xmlMsg.Length < 1) return false;

//            try
//            {
//                IMessage msg = xmlParser.Parse(xmlMsg);
//                hl7Msg = hl7Parser.Encode(msg);
//                return true;
//            }
//            catch (Exception e)
//            {
//                _log.Write(e);
//                return false;
//            }
//        }
//    }
//}
