using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.IM.Messaging.Base.Config;
using System.IO;
using HYS.IM.Messaging.Base;
using HYS.Common.Xml;
using HYS.IM.MessageDevices.SOAPAdapter.SOAPServer.Controler;
using HYS.IM.Messaging.Registry;

namespace HYS.IM.MessageDevices.SOAPAdapter.SOAPServer.Config
{
    public partial class SOAPServerConfig : EntityConfigBase
    {
        public const string SOAPServerDeviceName = "SOAP_SERVER";
        public const string SOAPServerConfigFileName = "SOAPServerConfig.xml";

        internal ProgramContext _context;

        private string GetFullPath(string relativePath)
        {
            string fullPath = ConfigHelper.GetFullPath(Path.Combine(_context.AppArgument.ConfigFilePath, relativePath));
            return fullPath;
        }

        public SOAPServerConfig()
        {
            LoadDefaultConfiguration();
        }
        private void LoadDefaultConfiguration()
        {
            // Identity 

            EntityID = Guid.NewGuid();
            Description = "SOAP Server Adapter";
            DeviceName = SOAPServerDeviceName;
            Name = Program.Context.AppName;

            // Default Transferring(routing) Contract

            Interaction = InteractionTypes.Publisher | InteractionTypes.Requester;
            Direction = DirectionTypes.Inbound;
            SubscribeConfig = null;
            ResponseConfig = null;

            PublishConfig.Publication.MessageTypeList.Add(MessageRegistry.GENERIC_NotificationMessageType);
            //PublishConfig.Publication.MessageTypeList.Add(MessageRegistry.DataTrackingLogMessageType);

            // Other Default Configuration

            SOAPServiceURI = "http://localhost:8080/CSBroker";

            // Default Configuration for RHIS PIX

            InboundProcessing.Model = MessageProcessModel.Xslt;
            InboundProcessing.XSLTExtensions = XSLTExtensionType.XmlNodeTransformer;
            InboundMessageDispatching.Model = MessageDispatchModel.Custom;
            //InboundMessageDispatching.CriteriaXPath = "/Message/Body/HL7/MSH/MessageType/@TriggerEvent";
            InboundMessageDispatching.CriteriaXPath = "/Message/Body/std:HL7/std:MSH/std:MessageType/@TriggerEvent";
            InboundMessageDispatching.CriteriaXPathPrefixDefinition = "std|http://www.carestream.com/HL7_STD";
            InboundMessageDispatching.CriteriaPublishValueExpression = "A01|A04|A05|A08|A40|A31";
            InboundMessageDispatching.CriteriaRequestValueExpression = "Q23|Q22";
            OutboundProcessing.Model = MessageProcessModel.Xslt;
            OutboundProcessing.XSLTExtensions = XSLTExtensionType.XmlNodeTransformer;

        }

        [XCData(true)]
        public string SOAPServiceURI { get; set; }

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

        private MessageDispatchConfig _inboundMessageDispatching = new MessageDispatchConfig();
        public MessageDispatchConfig InboundMessageDispatching
        {
            get { return _inboundMessageDispatching; }
            set { _inboundMessageDispatching = value; }
        }
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

    public enum MessageDispatchModel
    {
        Publish,        // dispatch message to XDSGW publisher
        Request,        // dispatch message to XDSGW requester
        Custom,         // dispatch message to XDSGW publisher or requester according to message content
        Test,           // generate response message using xml files in the folder "ResponseTemplate"
    }

    public class MessageDispatchConfig : XObject
    {
        public MessageDispatchModel Model { get; set; }

        [XCData(true)]
        public string CriteriaXPath { get; set; }
        [XCData(true)]
        public string CriteriaXPathPrefixDefinition { get; set; }
        [XCData(true)]
        public string CriteriaPublishValueExpression { get; set; }
        [XCData(true)]
        public string CriteriaRequestValueExpression { get; set; }

        /// <summary>
        /// This extension is used when generating success/failure response after publishing message to XDSGW framework,
        /// and also be used when generating response according XSLTs in SampleTemplates folder.
        /// </summary>
        public XSLTExtensionType XSLTExtensionsForGeneratingResponse { get; set; }

        /// <summary>
        /// Be used in Publish and Test model only, because response message is generated by responser in Request model.
        /// 
        /// The default value is False, to be compatible with XDSGW 1.1, which receive SOAP request from RHIS which contains enscape XML string in SOAP envelope,
        /// It can only be used to generate response message, after the SOAPServerControler.Transform/SOAP2XDSGW.xslt module convert it into XDSGW message.
        /// </summary>
        public bool GenerateResponseXDSGWMessageBasedOnIncomingSoapEnvelopeDirectly { get; set; }
    }
}
