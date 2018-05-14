using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.Common.Xml;
using HYS.IM.Messaging.Base.Config;
using HYS.IM.Messaging.Base;
using HYS.IM.Common.HL7v2.MLLP;
using HYS.IM.MessageDevices.HL7Adapter.HL7Receiver.Controler;
using System.IO;
//using HYS.IM.EMRMessages;
using HYS.IM.Common.HL7v2.Xml;
using HYS.IM.Messaging.Registry;
using HYS.IM.Messaging.Mapping.Transforming;
using HYS.IM.Messaging.Mapping.Replacing;

namespace HYS.IM.MessageDevices.HL7Adapter.HL7Receiver.Config
{
    /// <summary>
    /// This class contains all the configuration setting of this message entity.
    /// Please add your configuration setting as a public CLR type property in this class.
    /// Please add your configuration setting package as a public XObject property in this class.
    /// Please add your configuration setting package list as a public XCollection property in this class.
    /// </summary>
    public partial class HL7InboundConfig : EntityConfigBase
    {
        public const string HL7InboundDeviceName = "HL7_IN";
        public const string HL7InboundConfigFileName = "HL7InboundConfig.xml";

        public HL7InboundConfig()
        {
            LoadDefaultConfiguration();
        }
        private void LoadDefaultConfiguration()
        {
            // Identity 

            EntityID = Guid.NewGuid();
            Description = "HL7 Inbound Adapter";
            DeviceName = HL7InboundDeviceName;
            Name = Program.Context.AppName;

            // Default Transferring(routing) Contract

            Interaction = InteractionTypes.Publisher | InteractionTypes.Requester;
            Direction = DirectionTypes.Inbound;
            //PublishConfig.Publication.MessageTypeList.Add(MessageRegistry.HL7V2_NotificationMessageType);
            //PublishConfig.Publication.MessageTypeList.Add(MessageRegistry.HL7V2XML_NotificationMessageType);
            PublishConfig.Publication.MessageTypeList.Add(MessageRegistry.GENERIC_NotificationMessageType);
            //PublishConfig.Publication.MessageTypeList.Add(MessageRegistry.DataTrackingLogMessageType);
            ResponseConfig = null;
            SubscribeConfig = null;

            // Other Default Configuration

            SocketConfig = new SocketServerConfig();

            //EnableHL7XMLTransform = true;
            MessageProcessingType = MessageProcessType.HL7v2XML;
            HL7XMLTransformerType = NHL7ToolkitTransformer.DEVICE_NAME;
            InboundMessageDispatching.Model = MessageDispatchModel.Custom;
            InboundMessageDispatching.CriteriaXPath = "/Message/Body/node()/MSH/FIELD.9/FIELD_ITEM/COMPONENT.2";
            InboundMessageDispatching.CriteriaPublishValueExpression = "A01|A04|A05|A08|A40|A31";
            InboundMessageDispatching.CriteriaRequestValueExpression = "Q23|Q22";
            InboundMessageDispatching.GenerateResponseXmlMLLPPayloadWithXSLTExtensions = true;

            // in order to support 0A as segment separator
            MessagePreprocessing.Enable = false;
            MessagePreprocessing.Replacements.Add(new ReplacementRule() { MatchExpression = "\n" /* 0A */, ReplaceExpression = "\r" /* 0D */});
            MessagePreprocessing.Replacements.Add(new ReplacementRule() { MatchExpression = "\r\r" /* 0D0D */, ReplaceExpression = "\r" /* 0D */});
        }

        public SocketServerConfig SocketConfig { get; set; }

        private MessageDispatchConfig _inboundMessageDispatching = new MessageDispatchConfig();
        public MessageDispatchConfig InboundMessageDispatching
        {
            get { return _inboundMessageDispatching; }
            set { _inboundMessageDispatching = value; }
        }

        [XCData(true)]
        public string HL7XMLTransformerType { get; set; }

        ///// <summary>
        ///// Set this property to True if you receive HL7v2 text message via MLLP.
        ///// Set this property to False if you receive XML message via MLLP.
        ///// (In some special cases, you can also use the False setting to receive HL7v2 text message,
        ///// in order to create a XDSGW Message with HL7v2 text in the Body).
        ///// </summary>
        //public bool EnableHL7XMLTransform { get; set; }

        public MessageProcessType MessageProcessingType { get; set; }
    }

    public enum MessageDispatchModel
    {
        Publish,        // dispatch message to XDSGW publisher
        Request,        // dispatch message to XDSGW requester
        Custom,         // dispatch message to XDSGW publisher or requester according to message content
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

        public bool GenerateResponseXmlMLLPPayloadWithXSLTExtensions { get; set; }
    }

    public enum MessageProcessType
    {
        /// <summary>
        /// Assume receiving HL7v2 text message, 
        /// and send publishing and requesting XDSGW message to the framework with raw HL7v2 text in it.
        /// </summary>
        HL7v2Text = 0,
        /// <summary>
        /// Assume receiving HL7v2 text message, 
        /// and send publishing and requesting XDSGW message to the framework contains HL7v2 XML in it,
        /// and the XML is generated by XmlTransformer in Common.HL7v2 namespace.
        /// </summary>
        HL7v2XML = 1,
        /// <summary>
        /// Assume receiving other type of XML message (including HL7v3 XML and non-standard XML), 
        /// and send publishing and requesting XDSGW message to the framework contains the XML in it.
        /// </summary>
        OtherXML = 2,
    }
}
