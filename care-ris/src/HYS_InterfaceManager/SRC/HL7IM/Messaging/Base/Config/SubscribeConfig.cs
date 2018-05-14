using System;
using System.Text;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.IM.Messaging.Objects;
using HYS.IM.Messaging.Objects.PublishModel;
using HYS.IM.Messaging.Objects.RequestModel;
using HYS.IM.Messaging.Queuing;
using HYS.IM.Messaging.Queuing.MSMQ;
using HYS.IM.Messaging.Mapping;

namespace HYS.IM.Messaging.Base.Config
{
    public class SubscribeConfig : XObject
    {
        //private SubscriptionDescription _subscription = new SubscriptionDescription();
        //public SubscriptionDescription Subscription
        //{
        //    get { return _subscription; }
        //    set { _subscription = value; }
        //}

        private XCollection<PushChannelConfig> _channels = new XCollection<PushChannelConfig>();
        public XCollection<PushChannelConfig> Channels
        {
            get { return _channels; }
            set { _channels = value; }
        }

        public PushChannelConfig FindChannel(IPushRoute route)
        {
            if(route==null) return null;
            foreach (PushChannelConfig chn in _channels)
            {
                if (chn.ID == route.ID) return chn;
            }
            return null;
        }

        private OneWayProcessConfig _processConfig = new OneWayProcessConfig();
        public OneWayProcessConfig ProcessConfig
        {
            get { return _processConfig; }
            set { _processConfig = value; }
        }
    }
}
