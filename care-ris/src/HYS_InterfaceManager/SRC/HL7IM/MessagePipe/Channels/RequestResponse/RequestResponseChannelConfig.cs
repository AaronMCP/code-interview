using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.MessageDevices.MessagePipe.Base.Channel;
using HYS.Common.Xml;
using HYS.Messaging.Objects.RequestModel;
using HYS.Messaging.Objects;

namespace HYS.MessageDevices.MessagePipe.Channels.RequestResponse
{
    public class RequestResponseChannelConfig : ChannelConfigBase
    {
        internal const string DEVICE_NAME = "Simple Duplex Channel";
        internal const string DEVICE_DESC = "Receive requesting messsage, process it, send it out and receive response, process it, and return the response.";

        private MessageTypePair _requestMessageTypePair = new MessageTypePair();
        public MessageTypePair RequestMessageTypePair
        {
            get { return _requestMessageTypePair; }
            set { _requestMessageTypePair = value; }
        }

        private MessageType _responseMessageType = new MessageType();
        public MessageType ResponseMessageType
        {
            get { return _responseMessageType; }
            set { _responseMessageType = value; }
        }

        private XCollection<ProcessorInstance> _requestMessageProcessors = new XCollection<ProcessorInstance>();
        public XCollection<ProcessorInstance> RequestMessageProcessors
        {
            get { return _requestMessageProcessors; }
            set { _requestMessageProcessors = value; }
        }

        private XCollection<ProcessorInstance> _responseMessageProcessors = new XCollection<ProcessorInstance>();
        public XCollection<ProcessorInstance> ResponseMessageProcessors
        {
            get { return _responseMessageProcessors; }
            set { _responseMessageProcessors = value; }
        }

        public RequestResponseChannelConfig()
        {
            DeviceName = DEVICE_NAME;
            DeviceDescription = DEVICE_DESC;
        }
    }
}
