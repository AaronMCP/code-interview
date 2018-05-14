using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.Messaging.Base.Config;
using HYS.Common.Logging;

namespace HYS.MessageDevices.MessagePipe.Base
{
    public class ProcessorInitializationParameter
    {
        public readonly string ConfigXmlString;
        public readonly string StartupPath;
        public readonly string ProcessorName;
        public readonly ILog Log;

        public string GetFullPath(string path)
        {
            return ConfigHelper.GetFullPath(ConfigHelper.GetFullPath(StartupPath), path);
        }
        public string GetRelativePath(string path)
        {
            return ConfigHelper.GetRelativePath(ConfigHelper.GetFullPath(StartupPath), path);
        }

        public ProcessorInitializationParameter(string procName, string startPath, string cfgXml, ILog log)
        {
            Log = log;
            StartupPath = startPath;
            ConfigXmlString = cfgXml;
            ProcessorName = procName;
        }
    }
}
