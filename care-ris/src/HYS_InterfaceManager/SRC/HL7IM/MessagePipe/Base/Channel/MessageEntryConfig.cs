using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.Common.Xml;
using HYS.Messaging.Objects;

namespace HYS.MessageDevices.MessagePipe.Base.Channel
{
    public class MessageEntryConfig : XObject
    {
        public MessageEntryCheckingModel CheckingModel { get; set; }

        private MessageEntryCriteria _entryCriteria = new MessageEntryCriteria();
        public MessageEntryCriteria EntryCriteria
        {
            get { return _entryCriteria; }
            set { _entryCriteria = value; }
        }

        private MessageType _messageType = new MessageType();
        public MessageType EntryMessageType
        {
            get { return _messageType; }
            set { _messageType = value; }
        }
    }
}
