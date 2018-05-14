using System;
using System.Text;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.IM.Messaging.Objects.Entity;
using HYS.IM.Messaging.Objects.RoutingModel;

namespace HYS.IM.Messaging.Objects.RequestModel
{
    public class RequestRule : XObject, IPullRoute, IRoutingRule
    {
        public RequestRule()
        {
            _id = EntityDictionary.GetRandomNumber();
        }

        private string _id;
        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private XCollection<MessageTypePair> _messageTypePairs = new XCollection<MessageTypePair>();
        public XCollection<MessageTypePair> MessageTypePairs
        {
            get { return _messageTypePairs; }
            set { _messageTypePairs = value; }
        }

        [XNode(false)]
        public XCollection<MessageType> MessageTypeList
        {
            get
            {
                XCollection<MessageType> mtlist = new XCollection<MessageType>();
                foreach (MessageTypePair mtp in MessageTypePairs) mtlist.Add(mtp.RequestMessageType);
                return mtlist;
            }
        }

        private ContentCriteria _contentCriteria = new ContentCriteria();
        public ContentCriteria ContentCriteria
        {
            get { return _contentCriteria; }
            set { _contentCriteria = value; }
        }

        private RoutingRuleType _type;
        public RoutingRuleType Type
        {
            get { return _type; }
            set { _type = value; }
        }
    }
}
