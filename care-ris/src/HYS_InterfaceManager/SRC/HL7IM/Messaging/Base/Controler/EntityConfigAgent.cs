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
    public class EntityConfigAgent
    {
        private ILog _log;
        public EntityConfigAgent(EntityAssemblyConfig cfg, ILog log)
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

        private MessageEntityConfigEntryAttribute _entityConfigAttribute;
        public MessageEntityConfigEntryAttribute EntityConfigAttribute
        {
            get
            {
                if (_entityConfigAttribute == null)
                {
                    _entityConfigAttribute = EntityLoader.GetEntryAttribute<MessageEntityConfigEntryAttribute>(EntityType);
                    if (_entityConfigAttribute == null)
                    {
                        _log.Write(LogType.Error, "Cannot load message entity config attribute from assembly " + _config.AssemblyLocation);
                        _log.Write(EntityLoader.LastError);
                    }
                }
                return _entityConfigAttribute;
            }
        }

        private IMessageEntityConfig _entityConfigInstance;
        public IMessageEntityConfig EntityConfigInstance
        {
            get
            {
                if (_entityConfigInstance == null)
                {
                    _entityConfigInstance = EntityLoader.CreateEntry<IMessageEntityConfig>(EntityType);
                    if (_entityConfigInstance == null)
                    {
                        _log.Write(LogType.Error, "Cannot load message entity config instance from assembly " + _config.AssemblyLocation);
                        _log.Write(EntityLoader.LastError);
                    }
                }
                return _entityConfigInstance;
            }
        }

        private EntityConfigBase _entityConfig;
        public EntityConfigBase EntityConfig
        {
            get
            {
                if (_entityConfig == null)
                {
                    _entityConfig = EntityConfigInstance.GetConfiguration();
                    if (_entityConfig == null)
                    {
                        _log.Write(LogType.Error, "Cannot load message entity config configuration from assembly " + _config.AssemblyLocation);
                        _log.Write(EntityLoader.LastError);
                    }
                }
                return _entityConfig;
            }
        }

        public bool Initialize(EntityInitializeArgument arg)
        {
            IMessageEntityConfig e = EntityConfigInstance;
            if (e == null) return false;

            if (!e.Initialize(arg))
            {
                _log.Write("Initialize message entity config failed. " + ToString());
                return false;
            }

            EntityConfigBase c = EntityConfig;
            if (c == null) return false;

            return true;
        }

        public bool Uninitialize()
        {
            IMessageEntityConfig e = EntityConfigInstance;
            if (e != null) e.Uninitalize();

            return true;
        }
    }
}
