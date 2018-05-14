using System;
using System.Text;
using System.Collections.Generic;
using HYS.Common.Xml;

namespace HYS.IM.Messaging.Objects
{
    public class MessageHeader : XObject
    {
        private Guid _id;
        public Guid ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private MessageType _type = new MessageType();
        public MessageType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        private MessageRelationCollection _relationList = new MessageRelationCollection();
        public MessageRelationCollection RelationList
        {
            get { return _relationList; }
            set { _relationList = value; }
        }

        private MessageHistoryCollection _historyList = new MessageHistoryCollection();
        public MessageHistoryCollection HistoryList
        {
            get { return _historyList; }
            set { _historyList = value; }
        }
    }
}
