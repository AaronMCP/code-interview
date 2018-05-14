using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.MessageDevices.MessagePipe.Base.Channel;
using HYS.Common.Xml;
using HYS.Messaging.Objects;

namespace HYS.MessageDevices.MessagePipe.Channels.SubscribePublish
{
    public class SubscribePublishChannelConfig : ChannelConfigBase
    {
        internal const string DEVICE_NAME = "Simple One Way Channel";
        internal const string DEVICE_DESC = "Subscribe messsage, process it and publish it.";

        private MessageType _publishMessageType = new MessageType();
        public MessageType PublishMessageType
        {
            get { return _publishMessageType; }
            set { _publishMessageType = value; }
        }

        private XCollection<ProcessorInstance> _processors = new XCollection<ProcessorInstance>();
        public XCollection<ProcessorInstance> Processors
        {
            get { return _processors; }
            set { _processors = value; }
        }

        public SubscribePublishChannelConfig()
        {
            DeviceName = DEVICE_NAME;
            DeviceDescription = DEVICE_DESC;
        }
    }
}
