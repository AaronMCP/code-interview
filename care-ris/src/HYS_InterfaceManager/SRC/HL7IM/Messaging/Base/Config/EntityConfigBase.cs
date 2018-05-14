using System;
using System.Text;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.IM.Messaging.Objects;
using HYS.IM.Messaging.Objects.Entity;
using HYS.IM.Messaging.Objects.PublishModel;
using HYS.IM.Messaging.Objects.RequestModel;
using HYS.IM.Messaging.Queuing;
using HYS.IM.Messaging.Queuing.MSMQ;
using HYS.IM.Messaging.Mapping;

namespace HYS.IM.Messaging.Base.Config
{
    public class EntityConfigBase : EntityInformation
    {
        private DirectionTypes _direction;
        public DirectionTypes Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }

        private InteractionTypes _interaction;
        public InteractionTypes Interaction
        {
            get { return _interaction; }
            set { _interaction = value; }
        }

        private PublishConfig _publishConfig = new PublishConfig();
        public PublishConfig PublishConfig
        {
            get { return _publishConfig; }
            set { _publishConfig = value; }
        }

        private SubscribeConfig _subscribeConfig = new SubscribeConfig();
        public SubscribeConfig SubscribeConfig
        {
            get { return _subscribeConfig; }
            set { _subscribeConfig = value; }
        }

        private RequestConfig _requestConfig = new RequestConfig();
        public RequestConfig RequestConfig
        {
            get { return _requestConfig; }
            set { _requestConfig = value; }
        }

        private ResponseConfig _responseConfig = new ResponseConfig();
        public ResponseConfig ResponseConfig
        {
            get { return _responseConfig; }
            set { _responseConfig = value; }
        }
    }
}
