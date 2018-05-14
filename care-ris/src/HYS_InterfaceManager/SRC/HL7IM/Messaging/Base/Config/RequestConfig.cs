using System;
using System.Text;
using System.Collections.Generic;
using HYS.IM.Messaging.Objects;
using HYS.IM.Messaging.Objects.PublishModel;
using HYS.IM.Messaging.Objects.RequestModel;
using HYS.IM.Messaging.Queuing;
using HYS.IM.Messaging.Queuing.MSMQ;
using HYS.IM.Messaging.Queuing.RPC;
using HYS.IM.Messaging.Mapping;
using HYS.IM.Messaging.Objects.RoutingModel;
using HYS.Common.Xml;

namespace HYS.IM.Messaging.Base.Config
{
    public class RequestConfig : XObject
    {
        //private RequestDescription _requestContract = new RequestDescription();
        //public RequestDescription RequestContract
        //{
        //    get { return _requestContract; }
        //    set { _requestContract = value; }
        //}

        private XCollection<PullChannelConfig> _channels = new XCollection<PullChannelConfig>();
        public XCollection<PullChannelConfig> Channels
        {
            get { return _channels; }
            set { _channels = value; }
        }

        public PullChannelConfig FindTheFirstMatchedChannel(Message msg)
        {
            if (msg == null) return null;
            foreach (PullChannelConfig chn in Channels)
            {
                RoutingRuleValidator v = RoutingRuleValidator.CreateRoutingRuleValidatorFromCache(chn.RequestContract);
                if (v != null && v.Match(msg)) return chn;
            }
            return null;
        }

        private DuplexProcessConfig _processConfig = new DuplexProcessConfig();
        public DuplexProcessConfig ProcessConfig
        {
            get { return _processConfig; }
            set { _processConfig = value; }
        }
    }
}
