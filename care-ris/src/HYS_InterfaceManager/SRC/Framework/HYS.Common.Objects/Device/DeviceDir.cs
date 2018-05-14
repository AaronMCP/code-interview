using System;
using System.Collections.Generic;
using System.Text;
using HYS.Common.Xml;

namespace HYS.Common.Objects.Device
{
    public class DeviceDir : XObject
    {
        private DeviceInfor _header = new DeviceInfor();
        public DeviceInfor Header
        {
            get { return _header; }
            set { _header = value; }
        }

        private DeviceFileCollection _files = new DeviceFileCollection();
        public DeviceFileCollection Files
        {
            get { return _files; }
            set { _files = value; }
        }

        private CommandCollection _commands = new CommandCollection();
        public CommandCollection Commands
        {
            get { return _commands; }
            set { _commands = value; }
        }

        private LogInfo _logInfo = new LogInfo();
        public LogInfo LogInfo {
            get { return _logInfo; }
            set { _logInfo = value; }
        }
    }
}
