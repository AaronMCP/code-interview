using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.Common.Logging;
using HYS.Messaging.Base.Config;

namespace HYS.MessageDevices.MessagePipe.Base
{
    public class ChannelInitializationParameter
    {
        public readonly string ConfigXmlString;
        public readonly string StartupPath;
        public readonly string ChannelName;
        public readonly ISender Sender;
        public readonly ILog Log;

        public string GetFullPath(string path)
        {
            return ConfigHelper.GetFullPath(ConfigHelper.GetFullPath(StartupPath), path);
        }
        public string GetRelativePath(string path)
        {
            return ConfigHelper.GetRelativePath(ConfigHelper.GetFullPath(StartupPath), path);
        }

        public ChannelInitializationParameter(string chnName, string startPath, string cfgXml, ISender sender, ILog log)
        {
            Log = log;
            Sender = sender;
            ChannelName = chnName;
            StartupPath = startPath;
            ConfigXmlString = cfgXml;
        }
    }
}
