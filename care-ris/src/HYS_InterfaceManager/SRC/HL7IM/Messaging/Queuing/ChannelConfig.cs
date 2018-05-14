using System;
using System.Text;
using System.Collections.Generic;
using HYS.Common.Xml;

namespace HYS.IM.Messaging.Queuing
{
    public class ChannelConfig : XObject
    {
        private Guid _senderEntityID;
        public Guid SenderEntityID
        {
            get { return _senderEntityID; }
            set { _senderEntityID = value; }
        }

        private string _senderEntityName = "";
        [XCData(true)]
        public string SenderEntityName
        {
            get { return _senderEntityName; }
            set { _senderEntityName = value; }
        }

        private Guid _receiverEntityID;
        public Guid ReceiverEntityID
        {
            get { return _receiverEntityID; }
            set { _receiverEntityID = value; }
        }

        private string _receiverEntityName = "";
        [XCData(true)]
        public string ReceiverEntityName
        {
            get { return _receiverEntityName; }
            set { _receiverEntityName = value; }
        }

        public bool EqualsTo(ChannelConfig cfg)
        {
            if (cfg == null) return false;
            return _senderEntityID == cfg._senderEntityID &&
                _receiverEntityID == cfg._receiverEntityID;
        }
    }
}
