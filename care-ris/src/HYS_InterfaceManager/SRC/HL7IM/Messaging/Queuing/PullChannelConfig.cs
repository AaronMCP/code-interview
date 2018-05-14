using System;
using System.Text;
using System.Collections.Generic;
using HYS.IM.Messaging.Objects;
using HYS.IM.Messaging.Objects.Entity;
using HYS.IM.Messaging.Objects.RequestModel;
using HYS.IM.Messaging.Queuing.MSMQ;
using HYS.IM.Messaging.Queuing.LPC;
using HYS.IM.Messaging.Queuing.RPC;
using HYS.Common.Xml;

namespace HYS.IM.Messaging.Queuing
{
    public class PullChannelConfig : ChannelConfig, IPullRoute
    {
        private ProtocolType _protocolType;
        public ProtocolType ProtocolType
        {
            get { return _protocolType; }
            set { _protocolType = value; }
        }

        private RequestRule _requestContract = new RequestRule();
        public RequestRule RequestContract
        {
            get { return _requestContract; }
            set { _requestContract = value; }
        }

        private RPCChannelConfig _rpcConfig = new RPCChannelConfig();
        public RPCChannelConfig RPCConfig
        {
            get { return _rpcConfig; }
            set { _rpcConfig = value; }
        }

        private LPCChannelConfig _lpcConfig = new LPCChannelConfig();
        public LPCChannelConfig LPCConfig
        {
            get { return _lpcConfig; }
            set { _lpcConfig = value; }
        }

        #region IPullRoute Members

        [XNode(false)]
        public string ID
        {
            get { return RequestContract.ID; }
        }

        #endregion
    }
}
