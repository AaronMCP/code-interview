using System;
using System.Text;
using System.Collections.Generic;
using HYS.Common.Xml;

namespace HYS.IM.Messaging.Queuing.LPC
{
    public class LPCChannelConfig : XObject
    {
        private Guid _senderEntityID;
        public Guid SenderEntityID
        {
            get { return _senderEntityID; }
            set { _senderEntityID = value; }
        }

        private Guid _receiverEntityID;
        public Guid ReceiverEntityID
        {
            get { return _receiverEntityID; }
            set { _receiverEntityID = value; }
        }
    }
}
