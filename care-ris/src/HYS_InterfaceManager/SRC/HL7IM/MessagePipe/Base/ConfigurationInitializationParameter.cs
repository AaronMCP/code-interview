using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.Common.Logging;
using HYS.Messaging.Base.Config;

namespace HYS.MessageDevices.MessagePipe.Base
{
    public class ConfigurationInitializationParameter
    {
        public readonly string StartupPath;
        public readonly ILog Log;

        public string GetFullPath(string path)
        {
            return ConfigHelper.GetFullPath(ConfigHelper.GetFullPath(StartupPath), path);
        }
        public string GetRelativePath(string path)
        {
            return ConfigHelper.GetRelativePath(ConfigHelper.GetFullPath(StartupPath), path);
        }

        public ConfigurationInitializationParameter(string startPath, ILog log)
        {
            Log = log;
            StartupPath = startPath;
        }
    }
}
