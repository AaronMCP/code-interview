using System;
using System.Collections.Generic;
using System.Text;
using HYS.IM.Common.Logging;

namespace HYS.IM.Common.HL7v2.Xml
{
    public abstract class XmlTransformerBase
    {
        protected ILog _log;
        
        public XmlTransformerBase(ILog log)
        {
            _log = log;
        }

        public abstract bool TransformHL7v2ToXml(string hl7Msg, out string xmlMsg);
        public abstract bool TransformXmlToHL7v2(string xmlMsg, out string hl7Msg);
    }
}
