using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.IM.Common.Logging;
using HYS.Common.Xml;

namespace HYS.IM.Messaging.Management.Config
{
    public class PlatformConfig : XObject
    {
        public const string PlatformDirFileName = "PlatformDir.xml";

        public PlatformConfig()
        {
        }

        private LogConfig _logConfig = new LogConfig();
        public LogConfig LogConfig
        {
            get { return _logConfig; }
            set { _logConfig = value; }
        }

        private XCollection<SolutionInfo> _solutions = new XCollection<SolutionInfo>();
        public XCollection<SolutionInfo> Solutions
        {
            get { return _solutions; }
            set { _solutions = value; }
        }

        private XCollection<SuiteInfo> _suites = new XCollection<SuiteInfo>();
        public XCollection<SuiteInfo> Suites
        {
            get { return _suites; }
            set { _suites = value; }
        }

        private DBParameter _dbParameter = new DBParameter();
        public DBParameter DBParameter
        {
            get { return _dbParameter; }
            set { _dbParameter = value; }
        }
    }

    public class DBParameter : XObject
    {
        public string DBInstance { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
