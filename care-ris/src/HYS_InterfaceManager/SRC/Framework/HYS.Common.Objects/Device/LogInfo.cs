using System;
using System.Collections.Generic;
using System.Text;
using HYS.Common.Xml;
using HYS.Common.Objects.Logging;

namespace HYS.Common.Objects.Device
{
    public class LogInfo:XObject
    {
        private LogType _logType = LogType.Debug;
        public LogType LogType{
            get { return _logType; }
            set { _logType = value; }
        }

        private int _fileDuration = 0;
        public int FileDuration
        {
            get { return _fileDuration; }
            set { _fileDuration = value; }
        }

    }
}
