using System;
using System.Text;
using System.Collections.Generic;
using HYS.IM.Messaging.Objects;
using HYS.IM.Messaging.Objects.Entity;
using HYS.IM.Messaging.Objects.PublishModel;
using HYS.IM.Messaging.Queuing.MSMQ;
using HYS.IM.Messaging.Queuing.LPC;
using HYS.Common.Xml;

namespace HYS.IM.Messaging.Queuing
{
    public class PushChannelConfig : ChannelConfig, IPushRoute
    {
        private ProtocolType _protocolType;
        public ProtocolType ProtocolType
        {
            get { return _protocolType; }
            set { _protocolType = value; }
        }

        private SubscriptionRule _subscription = new SubscriptionRule();
        public SubscriptionRule Subscription
        {
            get { return _subscription; }
            set { _subscription = value; }
        }

        private MSMQChannelConfig _msmqConfig = new MSMQChannelConfig();
        public MSMQChannelConfig MSMQConfig
        {
            get { return _msmqConfig; }
            set { _msmqConfig = value; }
        }

        private LPCChannelConfig _lpcConfig = new LPCChannelConfig();
        public LPCChannelConfig LPCConfig
        {
            get { return _lpcConfig; }
            set { _lpcConfig = value; }
        }

        #region IPushRoute Members

        [XNode(false)]
        public string ID
        {
            get { return Subscription.ID; }
        }

        #endregion
    }
}
