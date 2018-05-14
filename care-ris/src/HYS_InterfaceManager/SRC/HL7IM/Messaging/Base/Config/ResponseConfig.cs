using System;
using System.Text;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.IM.Messaging.Objects;
using HYS.IM.Messaging.Objects.PublishModel;
using HYS.IM.Messaging.Objects.RequestModel;
using HYS.IM.Messaging.Queuing;
using HYS.IM.Messaging.Queuing.MSMQ;
using HYS.IM.Messaging.Queuing.RPC;
using HYS.IM.Messaging.Mapping;
using HYS.Common.Xml;

namespace HYS.IM.Messaging.Base.Config
{
    public class ResponseConfig : XObject
    {
        private ResponseDescription _responseContract = new ResponseDescription();
        public ResponseDescription ResponseContract
        {
            get { return _responseContract; }
            set { _responseContract = value; }
        }

        private XCollection<PullChannelConfig> _channels = new XCollection<PullChannelConfig>();
        public XCollection<PullChannelConfig> Channels
        {
            get { return _channels; }
            set { _channels = value; }
        }

        public PullChannelConfig FindChannel(IPullRoute route)
        {
            if (route == null) return null;
            foreach (PullChannelConfig chn in _channels)
            {
                if (chn.ID == route.ID) return chn;
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
