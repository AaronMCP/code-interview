using System;
using System.Text;
using System.Collections.Generic;
using HYS.IM.Messaging.Objects;
using HYS.IM.Messaging.Objects.PublishModel;
using HYS.IM.Messaging.Objects.RequestModel;
using HYS.IM.Messaging.Queuing;
using HYS.IM.Messaging.Queuing.MSMQ;
using HYS.IM.Messaging.Mapping;
using HYS.Common.Xml;

namespace HYS.IM.Messaging.Base.Config
{
    public class PublishConfig : XObject
    {
        private PublicationDescription _publication = new PublicationDescription();
        public PublicationDescription Publication
        {
            get { return _publication; }
            set { _publication = value; }
        }

        private XCollection<PushChannelConfig> _channels = new XCollection<PushChannelConfig>();
        public XCollection<PushChannelConfig> Channels
        {
            get { return _channels; }
            set { _channels = value; }
        }

        private OneWayProcessConfig _processConfig = new OneWayProcessConfig();
        public OneWayProcessConfig ProcessConfig
        {
            get { return _processConfig; }
            set { _processConfig = value; }
        }
    }
}
