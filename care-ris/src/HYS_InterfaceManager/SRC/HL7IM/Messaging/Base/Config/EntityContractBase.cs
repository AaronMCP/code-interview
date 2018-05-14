using System;
using System.Text;
using System.Collections.Generic;
using HYS.IM.Messaging.Objects;
using HYS.IM.Messaging.Objects.Entity;
using HYS.IM.Messaging.Objects.PublishModel;
using HYS.IM.Messaging.Objects.RequestModel;
using HYS.IM.Messaging.Queuing;
using HYS.IM.Messaging.Queuing.MSMQ;
using HYS.IM.Messaging.Mapping;
using HYS.Common.Xml;

namespace HYS.IM.Messaging.Base.Config
{
    public class EntityContractBase : XObject
    {
        private Guid _entityID;
        public Guid EntityID
        {
            get { return _entityID; }
            set { _entityID = value; }
        }

        private string _name = "";
        [XCData(true)]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _deviceName = "";
        [XCData(true)]
        public string DeviceName
        {
            get { return _deviceName; }
            set { _deviceName = value; }
        }

        private string _description = "";
        [XCData(true)]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

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

        private PublicationDescription _publication = new PublicationDescription();
        public PublicationDescription Publication
        {
            get { return _publication; }
            set { _publication = value; }
        }

        private SubscriptionDescription _subscription = new SubscriptionDescription();
        public SubscriptionDescription Subscription
        {
            get { return _subscription; }
            set { _subscription = value; }
        }

        private RequestDescription _requestDescription = new RequestDescription();
        public RequestDescription RequestDescription
        {
            get { return _requestDescription; }
            set { _requestDescription = value; }
        }

        private ResponseDescription _responseDescription = new ResponseDescription();
        public ResponseDescription ResponseDescription
        {
            get { return _responseDescription; }
            set { _responseDescription = value; }
        }

        private EntityAssemblyConfig _assemblyConfig = new EntityAssemblyConfig();
        public EntityAssemblyConfig AssemblyConfig
        {
            get { return _assemblyConfig; }
            set { _assemblyConfig = value; }
        }
    }
}
