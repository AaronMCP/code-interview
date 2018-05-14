using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.Common.Xml;
using HYS.IM.Messaging.Base.Config;
using HYS.IM.Common.HL7v2.MLLP;
using HYS.IM.Messaging.Base;
using HYS.IM.Messaging.Objects.RequestModel;
//using HYS.IM.EMRMessages;
using HYS.IM.Common.HL7v2.Xml;
using HYS.IM.Messaging.Registry;
using HYS.IM.Messaging.Mapping.Transforming;

namespace HYS.IM.MessageDevices.HL7Adapter.HL7Sender.Config
{
    /// <summary>
    /// This class contains all the configuration setting of this message entity.
    /// Please add your configuration setting as a public CLR type property in this class.
    /// Please add your configuration setting package as a public XObject property in this class.
    /// Please add your configuration setting package list as a public XCollection property in this class.
    /// </summary>
    public partial class HL7OutboundConfig : EntityConfigBase
    {
        public const string HL7OutboundDeviceName = "HL7_OUT";
        public const string HL7OutboundConfigFileName = "HL7OutboundConfig.xml";

        public HL7OutboundConfig()
        {
            LoadDefaultConfiguration();
        }
        private void LoadDefaultConfiguration()
        {
            // Identity 

            EntityID = Guid.NewGuid();
            Description = "HL7 Outbound Adapter";
            DeviceName = HL7OutboundDeviceName;
            Name = Program.Context.AppName;

            // Default Transferring(routing) Contract

            Interaction = InteractionTypes.Subscriber | InteractionTypes.Responser; // | InteractionTypes.Publisher;
            Direction = DirectionTypes.Outbound;
            RequestConfig = null;
            PublishConfig = null;

            //MessageTypePair mtPair = new MessageTypePair();
            //mtPair.RequestMessageType = MessageRegistry.HL7V2_QueryRequestMessageType;
            //mtPair.ResponseMessageType = MessageRegistry.HL7V2_QueryResultMessageType;
            //ResponseConfig.ResponseContract.MessageTypePairList.Add(mtPair);

            //MessageTypePair mtPair2 = new MessageTypePair();
            //mtPair2.RequestMessageType = MessageRegistry.HL7V2XML_QueryRequestMessageType;
            //mtPair2.ResponseMessageType = MessageRegistry.HL7V2XML_QueryResultMessageType;
            //ResponseConfig.ResponseContract.MessageTypePairList.Add(mtPair2);

            //PublishConfig.Publication.MessageTypeList.Add(MessageRegistry.DataTrackingLogMessageType);

            MessageTypePair mtPair = new MessageTypePair();
            mtPair.RequestMessageType = MessageRegistry.GENERIC_RequestMessageType;
            mtPair.ResponseMessageType = MessageRegistry.GENERIC_ResponseMessageType;
            ResponseConfig.ResponseContract.MessageTypePairList.Add(mtPair);

            // Other Default Configuration

            SocketConfig = new SocketClientConfig();
            MessageProcessingType = MessageProcessType.HL7v2XML;
            HL7XMLTransformerType = NHL7ToolkitTransformer.DEVICE_NAME;
            //EnableHL7XMLTransform = true;
        }

        public SocketClientConfig SocketConfig { get; set; }

        [XCData(true)]
        public string HL7XMLTransformerType { get; set; }

        ///// <summary>
        ///// As we use GENERIC_* message types to replace the HL7V2_* and HL7V2XML_* message types,
        ///// we need to use this property to control how to process these difference types of messages.
        ///// Set this property to True if you send HL7v2 text message via MLLP.
        ///// Set this property to False if you send XML message via MLLP.
        ///// (In some special cases, you can also use the False setting to send HL7v2 text message,
        ///// when the requesting or the publishing XDSGW message contains HL7v2 text in the Body).
        ///// </summary>
        //public bool EnableHL7XMLTransform { get; set; }

        public MessageProcessType MessageProcessingType { get; set; }
    }

    public enum MessageProcessType
    {
        /// <summary>
        /// Assume the requesting or publishing message received from XDSGW framework contains raw HL7v2 text in it.
        /// </summary>
        HL7v2Text = 0,
        /// <summary>
        /// Assume the requesting or publishing message received from XDSGW framework contains HL7v2 XML in it,
        /// and could be transform to HL7v2 text by XmlTransformer in Common.HL7v2 namespace.
        /// </summary>
        HL7v2XML = 1,
        /// <summary>
        /// Assume the requesting or publishing message received from XDSGW framework contains other type of XML in it,
        /// including HL7v3 XML and non-standard XML.
        /// </summary>
        OtherXML = 2,
    }
}
