using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.Messaging.Base.Config;
using HYS.Messaging.Base;
using HYS.Common.Xml;
using HYS.Messaging.Objects.RequestModel;

namespace HYS.MessageDevices.MessagePipe.Config
{
    public class MessagePipeConfig : EntityConfigBase
    {
        public const string MessagePipeDeviceName = "MSGPIPE";
        public const string MessagePipeConfigFileName = "MessagePipe.xml";

        public MessagePipeConfig()
        {
            LoadDefaultConfiguration();
        }
        private void LoadDefaultConfiguration()
        {
            // Identity 

            EntityID = Guid.NewGuid();
            Description = "Message Processing Pipeline";
            DeviceName = MessagePipeDeviceName;
            Name = Program.AppName;

            // Default Transferring(routing) Contract

            Interaction = InteractionTypes.Publisher | InteractionTypes.Subscriber | InteractionTypes.Requester | InteractionTypes.Responser;
            Direction = DirectionTypes.Inbound | DirectionTypes.Outbound;

            //PublishConfig.Publication.MessageTypeList.Add(MessageRegistry.HL7V2XML_NotificationMessageType);
            //PublishConfig.Publication.MessageTypeList.Add(MessageRegistry.RHISPIX_NotificationMessageType);

            //MessageTypePair mtPair = new MessageTypePair();
            //mtPair.RequestMessageType = MessageRegistry.RHISPIX_RequestMessageType;
            //mtPair.ResponseMessageType = MessageRegistry.RHISPIX_ResponseMessageType;
            //ResponseConfig.ResponseContract.MessageTypePairList.Add(mtPair);

            //MessageTypePair mtPair2 = new MessageTypePair();
            //mtPair2.RequestMessageType = MessageRegistry.HL7V2XML_QueryRequestMessageType;
            //mtPair2.ResponseMessageType = MessageRegistry.HL7V2XML_QueryResultMessageType;
            //ResponseConfig.ResponseContract.MessageTypePairList.Add(mtPair2);

            // Other Default Configuration
        }

        private XCollection<ChannelInstance> _channels = new XCollection<ChannelInstance>();
        public XCollection<ChannelInstance> Channels
        {
            get { return _channels; }
            set { _channels = value; }
        }
    }
}
