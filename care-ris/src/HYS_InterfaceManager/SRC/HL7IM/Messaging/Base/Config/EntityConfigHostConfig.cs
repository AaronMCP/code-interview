using System;
using System.Text;
using System.Drawing;
using System.Collections.Generic;
using HYS.IM.Common.Logging;
using HYS.Common.Xml;

namespace HYS.IM.Messaging.Base.Config
{
    public class EntityConfigHostConfig : XObject
    {
        public const string ConfigHostConfigFileName = "EntityConfigHost.xml";

        private EntityAssemblyConfig _entityAssembly = new EntityAssemblyConfig();
        public EntityAssemblyConfig EntityAssembly
        {
            get { return _entityAssembly; }
            set { _entityAssembly = value; }
        }

        private LogConfig _logConfig = new LogConfig();
        public LogConfig LogConfig
        {
            get { return _logConfig; }
            set { _logConfig = value; }
        }

        private Size _windowSize = new Size(696, 457);
        public Size WindowSize
        {
            get { return _windowSize; }
            set { _windowSize = value; }
        }
    }
}
