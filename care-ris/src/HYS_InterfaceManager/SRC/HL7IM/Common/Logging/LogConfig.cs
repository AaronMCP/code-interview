using System;
using System.Text;
using System.Collections.Generic;
using HYS.Common.Xml;

namespace HYS.IM.Common.Logging
{
    public class LogConfig : XObject
    {
        private LogType _logType = LogType.Error;
        public LogType LogType
        {
            get { return _logType; }
            set { _logType = value; }
        }

        private int _durationDay = 7;
        public int DurationDay
        {
            get { return _durationDay; }
            set { _durationDay = value; }
        }

        private string _hostName = "";
        public string HostName
        {
            get { return _hostName; }
            set { _hostName = value; }
        }

        private bool _dumpData;
        public bool DumpData
        {
            get { return _dumpData; }
            set { _dumpData = value; }
        }
    }
}
