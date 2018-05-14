using System;
using System.Text;
using System.Collections.Generic;
using HYS.Common.Xml;

namespace HYS.IM.Messaging.Objects.PublishModel
{
    public class PublicationDescription : XObject
    {
        private XCollection<MessageType> _messageTypeList = new XCollection<MessageType>();
        public XCollection<MessageType> MessageTypeList
        {
            get { return _messageTypeList; }
            set { _messageTypeList = value; }
        }
    }
}
