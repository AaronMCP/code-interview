using System;
using System.Text;
using System.Collections.Generic;
using HYS.Common.Xml;

namespace HYS.IM.Messaging.Objects.RequestModel
{
    public class ResponseDescription : XObject
    {
        private XCollection<MessageTypePair> _messageTypePairList = new XCollection<MessageTypePair>();
        public XCollection<MessageTypePair> MessageTypePairList
        {
            get { return _messageTypePairList; }
            set { _messageTypePairList = value; }
        }
    }
}
