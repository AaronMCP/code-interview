using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using HYS.IM.Messaging.Objects;
using HYS.IM.Messaging.Objects.Entity;
using HYS.IM.Messaging.Objects.PublishModel;
using HYS.IM.Messaging.Objects.RequestModel;
using HYS.IM.Messaging.Base.Config;
using HYS.IM.Common.Logging;

namespace HYS.IM.Messaging.Base.Controler
{
    public partial class EntityAgent
    {
        private ILog _log;
        public EntityAgent(EntityAssemblyConfig cfg, ILog log)
        {
            _config = cfg;
            _log = log;
        }

        public override string ToString()
        {
            return "ID:" + EntityConfig.EntityID + " Name:" + EntityConfig.Name;
        }

        private EntityAssemblyConfig _config;
        public EntityAssemblyConfig Config
        {
            get { return _config; }
        }

        private Assembly _assembly;
        public Assembly Assembly
        {
            get
            {
                if (_assembly == null)
                {
                    _assembly = EntityLoader.LoadAssembly(_config.AssemblyLocation);
                    if (_assembly == null)
                    {
                        _log.Write(LogType.Error, "Cannot load assembly from " + _config.AssemblyLocation);
                        _log.Write(EntityLoader.LastError);
                    }
                }
                return _assembly;
            }
        }

        private Type _entityType;
        public Type EntityType
        {
            get
            {
                if (_entityType == null)
                {
                    string tname = _config.ClassName;
                    if(tname == null ) tname = null;
                    else tname = tname.Trim();
                    if (tname.Length > 0)
                    {
                        _entityType = EntityLoader.FindEntryType(Assembly, tname);
                    }
                    else
                    {
                        Type[] tlist = EntityLoader.FindEntryType<MessageEntityEntryAttribute>(Assembly);
                        if (tlist != null && tlist.Length > 0) _entityType = tlist[0];
                    }
                    if (_entityType == null)
                    {
                        _log.Write(LogType.Error, "Cannot load message entity type from assembly " + _config.AssemblyLocation);
                        _log.Write(EntityLoader.LastError);
                    }
                }
                return _entityType;
            }
        }

        private MessageEntityEntryAttribute _entityAttribute;
        public MessageEntityEntryAttribute EntityAttribute
        {
            get
            {
                if (_entityAttribute == null)
                {
                    _entityAttribute = EntityLoader.GetEntryAttribute<MessageEntityEntryAttribute>(EntityType);
                    if (_entityAttribute == null)
                    {
                        _log.Write(LogType.Error, "Cannot load message entity attribute from assembly " + _config.AssemblyLocation);
                        _log.Write(EntityLoader.LastError);
                    }
                }
                return _entityAttribute;
            }
        }

        private IMessageEntity _entityInstance;
        public IMessageEntity EntityInstance
        {
            get
            {
                if (_entityInstance == null)
                {
                    _entityInstance = EntityLoader.CreateEntry<IMessageEntity>(EntityType);
                    if (_entityInstance == null)
                    {
                        _log.Write(LogType.Error, "Cannot load message entity instance from assembly " + _config.AssemblyLocation);
                        _log.Write(EntityLoader.LastError);
                    }
                }
                return _entityInstance;
            }
        }

        private EntityConfigBase _entityConfig;
        public EntityConfigBase EntityConfig
        {
            get
            {
                if (_entityConfig == null)
                {
                    _entityConfig = EntityInstance.GetConfiguration();
                    if (_entityConfig == null)
                    {
                        _log.Write(LogType.Error, "Cannot load message entity configuration from assembly " + _config.AssemblyLocation);
                        _log.Write(EntityLoader.LastError);
                    }
                }
                return _entityConfig;
            }
        }

        public bool Initialize(EntityInitializeArgument arg)
        {
            IMessageEntity e = EntityInstance;
            if (e == null) return false;

            if (!e.Initialize(arg))
            {
                _log.Write("Initialize message entity failed. " + ToString());
                return false;
            }

            EntityConfigBase c = EntityConfig;
            if (c == null) return false;

            return true;
        }

        public bool InitializeInteraction(InteractionTypes t)
        {
            EntityConfigBase c = EntityConfig;
            if (c == null) return false;

            _log.Write(string.Format("Initializing {0} agent in entity {1}({2}).",
                t.ToString(), c.Name, c.EntityID.ToString()));

            if ((t & InteractionTypes.Publisher) == InteractionTypes.Publisher) this.InitializePublisherAgent(c);
            if ((t & InteractionTypes.Subscriber) == InteractionTypes.Subscriber) this.InitializeSubscriberAgent(c);
            if ((t & InteractionTypes.Requester) == InteractionTypes.Requester) this.InitializeRequesterAgent(c);
            if ((t & InteractionTypes.Responser) == InteractionTypes.Responser) this.InitializeResponserAgent(c);

            return true;
        }

        public bool Start()
        {
            this.StartSubscriberAgent();
            this.StartResponserAgent();

            IMessageEntity e = EntityInstance;
            if (e != null) e.Start();

            return true;
        }

        public bool Stop()
        {
            this.StopSubscriberAgent();
            this.StopResponserrAgent();

            IMessageEntity e = EntityInstance;
            if (e != null) e.Stop();

            return true;
        }

        public bool Uninitialize()
        {
            this.UnintializePublisherAgent();
            this.UninitializeSubscriberAgent();
            this.UnintializeRequesterAgent();
            this.UnintializeResponserAgent();

            IMessageEntity e = EntityInstance;
            if (e != null) e.Uninitalize();

            return true;
        }
    }
}
