using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.IM.Common.HL7v2.Xml.Transforming;
using HYS.IM.Common.Logging;

namespace HYS.IM.Common.HL7v2.Xml
{
    public class NHL7ToolkitTransformer : XmlTransformerBase
    {
        public const string DEVICE_NAME = "NHL7Toolkit";

        public NHL7ToolkitTransformer(ILog log)
            : base(log)
        {
        }

        public override bool TransformHL7v2ToXml(string hl7Msg, out string xmlMsg)
        {
            try
            {
                HL7Message msg = new HL7Message(hl7Msg, 0);
                xmlMsg = msg.getXML();
                return true;
            }
            catch (Exception e)
            {
                xmlMsg = "";
                _log.Write(e);
                return false;
            }
        }

        public override bool TransformXmlToHL7v2(string xmlMsg, out string hl7Msg)
        {
            try
            {
                HL7Message msg = new HL7Message(xmlMsg, 1);
                hl7Msg = msg.get();

                // ensure the ending of the HL7 text message payload is "\r" or "\r\n" 
                // so that the outgoing message should have a '\r' on the end of the message payload
                // in order to pass the Mesa testing
                if (hl7Msg != null && hl7Msg.Length > 1)
                {
                    if (hl7Msg.Substring(hl7Msg.Length - 2, 2) != "\r\n" &&
                        hl7Msg.Substring(hl7Msg.Length - 1, 1) != "\r")
                    {
                        hl7Msg = hl7Msg + "\r";
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                hl7Msg = "";
                _log.Write(e);
                return false;
            }
        }
    }
}
