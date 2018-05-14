using System;
using System.Collections.Generic;
using System.Text;
using HYS.Common.Xml;

namespace HYS.Common.Objects.Logging
{
    public class LogItem:XObject
    {
        private string _dateTime = "";
        public string DateTime
        {
            get { return _dateTime; }
            set { _dateTime = value; }
        }

        private LogType _logType = LogType.Debug;
        public LogType LogType
        {
            get { return _logType; }
            set { _logType = value; }
        }

        private string _assemblyName = "";
        public string AssemblyName
        {
            get { return _assemblyName; }
            set { _assemblyName = value; }
        }

        private string _module = "";
        public string Module
        {
            get { return _module; }
            set { _module = value; }
        }

        private string _description = "";
        [XCData(true)]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public LogItem(string dateTime, LogType type, string module, string description) {
            this._dateTime = dateTime;
            this._logType = type;
            this._module = module;
            this._description = description;
        }

        public LogItem(string description) {
            this._dateTime = "";
            this._logType = LogType.Debug;
            this._module = "";
            this._description = description;
        }

        public LogItem() { }
    }
}
