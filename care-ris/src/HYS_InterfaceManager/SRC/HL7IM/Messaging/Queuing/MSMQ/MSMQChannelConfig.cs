using System;
using System.Text;
using System.Collections.Generic;
using HYS.Common.Xml;

namespace HYS.IM.Messaging.Queuing.MSMQ
{
    public class MSMQChannelConfig : XObject
    {
        private MSMQSenderParameter _senderParameter = new MSMQSenderParameter();
        public MSMQSenderParameter SenderParameter
        {
            get { return _senderParameter; }
            set { _senderParameter = value; }
        }

        private MSMQReceiverParameter _receiverParameter = new MSMQReceiverParameter();
        public MSMQReceiverParameter ReceiverParameter
        {
            get { return _receiverParameter; }
            set { _receiverParameter = value; }
        }
    }
}
