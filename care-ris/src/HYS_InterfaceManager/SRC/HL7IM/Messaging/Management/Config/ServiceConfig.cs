using System;
using System.Collections.Generic;
using System.Text;
using HYS.Common.Xml;
using System.IO;

namespace HYS.IM.Messaging.Management.Config
{
    public class ServiceConfig : XObject
    {
        public const string ServiceName = "ManagementService.svc";
        public const string FileName = "ManagementService.xml";

        public static string GetConfigFileName(string solutionPath)
        {
            return Path.Combine(solutionPath, "../../Platform/Service/" + FileName);
        }

        private int _remotingPort = 10808;
        public int RemotingPort
        {
            get { return _remotingPort; }
            set { _remotingPort = value; }
        }

        private string _remotingUrl = "";
        public string RemotingUrl
        {
            get { return _remotingUrl; }
            set { _remotingUrl = value; }
        }
    }
}
