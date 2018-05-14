using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.ServiceProcess;
using HYS.IM.Common.Logging;
using HYS.Common.Xml;
using HYS.Common.Xml;

namespace HYS.IM.Messaging.Base.Config
{
    public class NTServiceHostConfig : EntityHostConfig
    {
        public const char DependOnServiceNameListSpliter = ',';
        //public const string NTServiceDisplayNamePrefix = "CSH eHealth ";
        public const string NTServiceDisplayNamePrefix = "CSH ";//"CSH HL7 Gateway ";
        public const string NTServiceHostConfigFileName = "NTServiceHost.xml";

        private string _serviceName = "";
        [XCData(true)]
        public string ServiceName
        {
            get { return _serviceName; }
            set { _serviceName = value; }
        }

        private string _dependOnServiceNameList = "MSMQ";
        [XCData(true)]
        public string DependOnServiceNameList
        {
            get { return _dependOnServiceNameList; }
            set { _dependOnServiceNameList = value; }
        }

        // start service and set start mode as automatic
        // stop service and set start mode as manual

        private ServiceStartMode _startType = ServiceStartMode.Manual;
        public ServiceStartMode StartType
        {
            get { return _startType; }
            set { _startType = value; }
        }

        private ServiceAccount _accountType = ServiceAccount.LocalSystem;
        public ServiceAccount AccountType
        {
            get { return _accountType; }
            set { _accountType = value; }
        }

        private bool _interactWithDesktop;
        public bool InteractWithDesktop
        {
            get { return _interactWithDesktop; }
            set { _interactWithDesktop = value; }
        }

        private string _userName = "";
        [XCData(true)]
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        private string _password = "";
        [XCData(true)]
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        private string _description = "XDS Gateway Application Host for Healthcare Integration";
        [XCData(true)]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        private bool _notifyStatusToManager;
        public bool NotifyStatusToManager
        {
            get { return _notifyStatusToManager; }
            set { _notifyStatusToManager = value; }
        }

        private string _managerWindowCaption = "";
        public string ManagerWindowCaption
        {
            get { return _managerWindowCaption; }
            set { _managerWindowCaption = value; }
        }

        public static bool IsValidServiceName(string name)
        {
            if (name == null) return false;

            string strName = name.Trim();
            if (strName.Length < 1 || strName.Length > 256) return false;

            string newName = Regex.Replace(strName, @"[^\w]", "");
            newName = Regex.Replace(newName, @"[^\x00-\xff]", "");
            if (newName.Length != strName.Length) return false;

            char c = newName[0];
            return c >= 'A' && c <= 'z';
        }
    }
}
