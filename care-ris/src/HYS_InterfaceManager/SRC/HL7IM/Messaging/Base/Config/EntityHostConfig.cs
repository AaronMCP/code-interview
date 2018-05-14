using System;
using System.Text;
using System.Collections.Generic;
using HYS.IM.Common.Logging;
using HYS.Common.Xml;

namespace HYS.IM.Messaging.Base.Config
{
    public class EntityHostConfig : XObject
    {
        private XCollection<EntityAssemblyConfig> _entities = new XCollection<EntityAssemblyConfig>();
        public XCollection<EntityAssemblyConfig> Entities
        {
            get { return _entities; }
            set { _entities = value; }
        }

        public EntityAssemblyConfig FindEntityByName(string entityName)
        {
            foreach (EntityAssemblyConfig cfg in _entities)
                if (cfg.EntityInfo.Name == entityName) return cfg;
            return null;
        }

        private LogConfig _logConfig = new LogConfig();
        public LogConfig LogConfig
        {
            get { return _logConfig; }
            set { _logConfig = value; }
        }
    }
}
