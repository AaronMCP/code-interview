using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.IM.Messaging.Base.Config;
using System.IO;
using HYS.IM.Messaging.Base;
using HYS.IM.Messaging.Objects.RequestModel;
using HYS.Common.Xml;
using HYS.IM.MessageDevices.SOAPAdapter.SOAPClient.Controler;
using HYS.IM.Messaging.Registry;

namespace HYS.IM.MessageDevices.SOAPAdapter.SOAPClient.Config
{
    public partial class SOAPClientConfig : EntityConfigBase
    {
        public const string SOAPClientDeviceName = "SOAP_CLIENT";
        public const string SOAPClientConfigFileName = "SOAPClientConfig.xml";

        public SOAPClientConfig()
        {
            LoadDefaultConfiguration();
        }
        private void LoadDefaultConfiguration()
        {
            // Identity 

            EntityID = Guid.NewGuid();
            Description = "SOAP Client Adapter";
            DeviceName = SOAPClientDeviceName;
            Name = Program.Context.AppName;

            // Default Transferring(routing) Contract

            Interaction = InteractionTypes.Subscriber | InteractionTypes.Responser; // | InteractionTypes.Publisher;
            Direction = DirectionTypes.Outbound;
            RequestConfig = null;
            PublishConfig = null;

            MessageTypePair mtPair = new MessageTypePair();
            mtPair.RequestMessageType = MessageRegistry.GENERIC_RequestMessageType;
            mtPair.ResponseMessageType = MessageRegistry.GENERIC_ResponseMessageType;
            ResponseConfig.ResponseContract.MessageTypePairList.Add(mtPair);

            //PublishConfig.Publication.MessageTypeList.Add(MessageRegistry.DataTrackingLogMessageType);

            // Other Default Configuration

            //SOAPServiceURI = "http://localhost:8080/PIXService.asmx";
            SOAPServiceURI = "http://localhost:8080/CSBroker";
            SOAPAction = "http://www.carestreamhealth.com/MessageCom";

            // Default Configuration for RHIS PIX

            InboundProcessing.Model = MessageProcessModel.Xslt;
            InboundProcessing.XSLTExtensions = XSLTExtensionType.XmlNodeTransformer;
            OutboundProcessing.Model = MessageProcessModel.Xslt;
            OutboundProcessing.XSLTExtensions = XSLTExtensionType.XmlNodeTransformer;
        }

        [XCData(true)]
        public string SOAPServiceURI { get; set; }
        [XCData(true)]
        public string SOAPAction { get; set; }

        private MessageProcessingConfig _inboundProcessing = new MessageProcessingConfig();
        public MessageProcessingConfig InboundProcessing
        {
            get { return _inboundProcessing; }
            set { _inboundProcessing = value; }
        }

        private MessageProcessingConfig _outboundProcessing = new MessageProcessingConfig();
        public MessageProcessingConfig OutboundProcessing
        {
            get { return _outboundProcessing; }
            set { _outboundProcessing = value; }
        }

        //This configuration setting allows SOAP Outbound Adapter to failed (return error to inbound adapter) when receiving SOAP failure message from 3rd party SOAP server.
        //In the case of SOAP Sender Interface (CSB_IN -> SOAP_OUT), this setting can simplify the configuration by using publish/subscribe channel: when 3rd party SOAP server return failure message, do not update the Process Flag in CS Broker database. (You can also implement this by configuring XSLT with request/response channel, more flexible and more complex).
        //If you want to retry only when network problem (not 3rd party SOAP server problem), please remain the default configuration of not returning any error to the calling inbound adapter when the SOAP message is successfully sent to the 3rd party SOAP server, not matter it return success or failure.
        public bool ThrowExceptionWhenReceiveFailureResponse { get; set; }
    }

    public enum MessageProcessModel
    {
        Direct,
        Xslt,
    }

    public class MessageProcessingConfig : XObject
    {
        public MessageProcessModel Model { get; set; }
        public XSLTExtensionType XSLTExtensions { get; set; }
    }

}
