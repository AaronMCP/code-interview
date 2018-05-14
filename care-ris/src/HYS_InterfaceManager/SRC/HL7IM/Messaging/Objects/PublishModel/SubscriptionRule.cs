using System;
using System.Text;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.IM.Messaging.Objects.Entity;
using HYS.IM.Messaging.Objects.RoutingModel;

namespace HYS.IM.Messaging.Objects.PublishModel
{
    public class SubscriptionRule : XObject, IPushRoute, IRoutingRule
    {
        public SubscriptionRule()
        {
            _id = EntityDictionary.GetRandomNumber();
        }

        private string _id;
        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private RoutingRuleType _type;
        public RoutingRuleType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        private XCollection<MessageType> _messageTypeList = new XCollection<MessageType>();
        public XCollection<MessageType> MessageTypeList
        {
            get { return _messageTypeList; }
            set { _messageTypeList = value; }
        }

        private ContentCriteria _contentCriteria = new ContentCriteria();
        public ContentCriteria ContentCriteria
        {
            get { return _contentCriteria; }
            set { _contentCriteria = value; }
        }

        //private XCollection<ValueCriteria> _contentCriteriaList = new XCollection<ValueCriteria>();
        //public XCollection<ValueCriteria> ContentCriteriaList
        //{
        //    get { return _contentCriteriaList; }
        //    set { _contentCriteriaList = value; }
        //}
    }
}
