using System;
using System.Text;
using System.Collections.Generic;
using HYS.IM.Common.Logging;
using System.IO;
using HYS.Common.Xml;

namespace HYS.IM.Messaging.Base
{
    public class EntityInitializeArgument : XObject
    {
        // configure-time parameters;

        private string _configFilePath = "";
        [XCData(true)]
        public string ConfigFilePath
        {
            get { return _configFilePath; }
            set { _configFilePath = value; }
        }

        // runtime parameters;

        private LogConfig _logConfig = new LogConfig();
        [XCData(false)]
        public LogConfig LogConfig
        {
            get { return _logConfig; }
            set { _logConfig = value; }
        }

        private string _description = "";
        [XCData(false)]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public string ToLog()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Message Entity Initialization Argument");
            sb.AppendLine("======================================");
            sb.Append("ConfigFilePath:").AppendLine(ConfigFilePath);
            sb.Append("LogConfig:type=").Append(LogConfig.LogType.ToString())
                .Append(";hostname=").Append(LogConfig.HostName)
                .Append(";durationday=").Append(LogConfig.DurationDay)
                .Append(";dumpdata=").AppendLine(LogConfig.DumpData.ToString());
            sb.Append("Description:").AppendLine(Description);
            sb.AppendLine("======================================");
            return sb.ToString();
        }
        public string GetLogFileName(string appName)
        {
            if (string.IsNullOrEmpty(ConfigFilePath)) return appName;
            return appName + "_" + Path.GetFileName(ConfigFilePath);
        }
    }
}
