using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.IM.Messaging.Base.Config;
using HYS.IM.Common.Logging;
using System.IO;
using System.Reflection;

namespace HYS.IM.Messaging.Base.Controler
{
    public class EntityHost
    {
        public string HostName = "CustomizedHost";
        public string HostConfigFileName = NTServiceHostConfig.NTServiceHostConfigFileName;    // "NTServiceHost.xml";;

        public LogControler Log;
        public ConfigManager<NTServiceHostConfig> ConfigMgt;
        public EntityContainer Container;

        private bool PreLoading(string hostConfigFilePath)
        {
            HostConfigFileName = Path.Combine(hostConfigFilePath, NTServiceHostConfig.NTServiceHostConfigFileName);
            ConfigMgt = new ConfigManager<NTServiceHostConfig>(HostConfigFileName);
            if (ConfigMgt.Load())
            {
                LogControler.BaseDirectory = hostConfigFilePath;
                Log = new LogControler(HostName, ConfigMgt.Config.LogConfig);
                LogHelper.EnableApplicationLogging(Log);
                LogHelper.EnableXmlLogging(Log);
                Log.WriteAppStart(HostName);

                Log.Write("Load config succeeded. " + ConfigMgt.FileName);
                return true;
            }
            else
            {
                LogControler.BaseDirectory = hostConfigFilePath;
                LogConfig logCfg = new LogConfig();
                logCfg.LogType = LogType.Debug;
                Log = new LogControler(HostName, logCfg);
                LogHelper.EnableApplicationLogging(Log);
                LogHelper.EnableXmlLogging(Log);
                Log.WriteAppStart(HostName);

                Log.Write(LogType.Error, "Load config failed. " + ConfigMgt.FileName);
                Log.Write(ConfigMgt.LastError);
                return false;
            }
        }
        private void BeforeExit()
        {
            Log.WriteAppExit(HostName);
        }

        private bool _initialized;
        public bool Initialize(string hostConfigFilePath)
        {
            return Initialize(hostConfigFilePath, null);
        }
        public bool Initialize(string hostConfigFilePath, Assembly hostingAsm)
        {
            lock (this)
            {
                if (!PreLoading(hostConfigFilePath)) return false;
                EntityLoader.BaseDirectory = hostConfigFilePath;
                EntityLoader.InitializeAssemblyList(hostingAsm);
                EntityContainer.BaseDirectory = hostConfigFilePath;
                Container = new EntityContainer(ConfigMgt.Config, Log);
                if (!Container.Initialize(ConfigMgt.Config.LogConfig, HostName)) return false;
                if (!Container.Start()) return false;
                _initialized = true;
                return true;
            }
        }
        public bool Uninitialize()
        {
            lock (this)
            {
                if (!_initialized) return false;
                if (!Container.Stop()) return false;
                if (!Container.Uninitalize()) return false;
                BeforeExit();
                return true;
            }
        }

        public IMessageEntity FindEntityInstance(Type entityImplType)
        {
            lock (this)
            {
                if (Container == null) return null;
                EntityAgent agent = Container.FindEntityAgent(entityImplType);
                if (agent == null) return null;
                return agent.EntityInstance;
            }
        }
    }
}
