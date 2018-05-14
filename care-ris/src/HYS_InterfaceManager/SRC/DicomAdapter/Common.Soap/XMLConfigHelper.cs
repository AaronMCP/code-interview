using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using HYS.Common.Soap.DefaultConfiguration;
using HYS.Common.Objects.Logging;

namespace HYS.Common.Soap
{
    public class XMLConfigHelper
    {
        public const string RequestXSLTFileNameForSOAPClient = "SOAPClient.Request.xslt"; //"RQ_XML2STRING.xslt";
        public const string ResponseXSLTFileNameForSOAPClient = "SOAPClient.Response.xslt"; //"RSP_STRING2XML.xslt";

        public const string RequestXSLTFileNameForSOAPServer = "SOAPServer.Request.xslt"; //"RQ_STRING2XML.xslt";
        public const string ResponseXSLTFileNameForSOAPServer = "SOAPServer.Response.xslt"; //"RSP_XML2STRING.xslt";

        private ILogging _log;
        private string _cfgFolderPath;
        private bool _overWrite;

        public XMLConfigHelper(string cfgFolderPath, ILogging log, bool overWrite)
        {
            _log = log;
            _cfgFolderPath = cfgFolderPath;
            _overWrite = overWrite;
        }
        public XMLConfigHelper(string cfgFolderPath, ILogging log)
            : this(cfgFolderPath, log, false)
        {
        }

        public void WriteDefaultRequestXSLTForSOAPClient()
        {
            ConfigHelper.WriteConfigToFolder(RequestXSLTFileNameForSOAPClient, _cfgFolderPath, _overWrite, _log);
        }
        public void WriteDefaultResponseXSLTForSOAPClient()
        {
            ConfigHelper.WriteConfigToFolder(ResponseXSLTFileNameForSOAPClient, _cfgFolderPath, _overWrite, _log);
        }

        public void WriteDefaultRequestXSLTForSOAPServer()
        {
            ConfigHelper.WriteConfigToFolder(RequestXSLTFileNameForSOAPServer, _cfgFolderPath, _overWrite, _log);
        }
        public void WriteDefaultResponseXSLTForSOAPServer()
        {
            ConfigHelper.WriteConfigToFolder(ResponseXSLTFileNameForSOAPServer, _cfgFolderPath, _overWrite, _log);
        }
    }
}
