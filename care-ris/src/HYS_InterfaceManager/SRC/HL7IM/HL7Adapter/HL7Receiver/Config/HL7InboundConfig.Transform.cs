using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.IM.Messaging.Base.Config;
using System.IO;
using HYS.Common.Xml;
using HYS.IM.Messaging.Mapping.Replacing;

namespace HYS.IM.MessageDevices.HL7Adapter.HL7Receiver.Config
{
    public partial class HL7InboundConfig : EntityConfigBase
    {
        // For simplicity,
        // instead of transforming between XDSGW schema and transporting schema,
        // current we just use XDSGW message body contain as transporting payload.
        // BTW, the SOAP In/Out Adapter need to transform between XDSGW schema and SOAP schema,
        // because some 3rd party SOAP Server/Client transport XML as string in the SOAP envelope.

        //public const string XSLTFileName_XDSGatewayMessageToMLLPXMLMessage = "TransportTemplates\\XDSGW2MLLP.xslt";
        //public const string XSLTFileName_MLLPXMLMessageToXDSGatewayMessage = "TransportTemplates\\MLLP2XDSGW.xslt";

        //public XSLTExtensionTypes MessageProcessingXSLTExtensions { get; set; }

        private MessagePreprocessingPipe _messagePreprocessing = new MessagePreprocessingPipe();
        public MessagePreprocessingPipe MessagePreprocessing
        {
            get { return _messagePreprocessing; }
            set { _messagePreprocessing = value; }
        }
    }

    public class MessagePreprocessingPipe : XObject
    {
        public bool Enable { get; set; }
        
        private XCollection<ReplacementRule> _replacements = new XCollection<ReplacementRule>();
        public XCollection<ReplacementRule> Replacements
        {
            get { return _replacements; }
            set { _replacements = value; }
        }

        public string Replace(string originalString)
        {
            string str = originalString;
            foreach (ReplacementRule r in Replacements) str = r.Replace(str);
            return str;
        }
    }
}
