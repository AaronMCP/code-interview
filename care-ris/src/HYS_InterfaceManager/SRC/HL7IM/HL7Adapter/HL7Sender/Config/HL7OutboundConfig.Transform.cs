using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.IM.Messaging.Base.Config;

namespace HYS.IM.MessageDevices.HL7Adapter.HL7Sender.Config
{
    public partial class HL7OutboundConfig : EntityConfigBase
    {
        // For simplicity,
        // instead of transforming between XDSGW schema and transporting schema,
        // current we just use XDSGW message body contain as transporting payload.
        // BTW, the SOAP In/Out Adapter need to transform between XDSGW schema and SOAP schema,
        // because some 3rd party SOAP Server/Client transport XML as string in the SOAP envelope.

        //public const string XSLTFileName_XDSGatewayMessageToMLLPXMLMessage = "TransportTemplates\\XDSGW2MLLP.xslt";
        //public const string XSLTFileName_MLLPXMLMessageToXDSGatewayMessage = "TransportTemplates\\MLLP2XDSGW.xslt";

        //public XSLTExtensionTypes MessageProcessingXSLTExtensions { get; set; }
    }
}
