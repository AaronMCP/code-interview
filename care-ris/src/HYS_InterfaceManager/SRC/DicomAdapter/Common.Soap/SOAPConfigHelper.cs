using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using HYS.Common.Soap.DefaultConfiguration;
using HYS.Common.Objects.Logging;

namespace HYS.Common.Soap
{
    public class SOAPConfigHelper
    {
        public const string SOAPClientWCFConfigFileName = "SOAPClientConfig.WCF.xml";
        public const string SOAPServerWCFConfigFileName = "SOAPServerConfig.WCF.xml";
        public const string SOAPErrorMessageFileName = "SOAPErrorMessage.xml";

        public static string SOAPEnvelopeXmlNamespace = "http://schemas.xmlsoap.org/soap/envelope/";

        private ILogging _log;
        private string _cfgFolderPath;
        private bool _overWrite;

        public SOAPConfigHelper(string cfgFolderPath, ILogging log, bool overWrite)
        {
            _log = log;
            _cfgFolderPath = cfgFolderPath;
            _overWrite = overWrite;
        }
        public SOAPConfigHelper(string cfgFolderPath, ILogging log)
            : this(cfgFolderPath, log, false)
        {
        }

        public void WriteDefaultSOAPClientWCFConfigFile()
        {
            ConfigHelper.WriteConfigToFolder(SOAPClientWCFConfigFileName, _cfgFolderPath, _overWrite, _log);
        }
        public void WriteDefaultSOAPServerWCFConfigFile()
        {
            ConfigHelper.WriteConfigToFolder(SOAPServerWCFConfigFileName, _cfgFolderPath, _overWrite, _log);
        }
        public void WriteDefaultSOAPErrorMessageFile()
        {
            ConfigHelper.WriteConfigToFolder(SOAPErrorMessageFileName, _cfgFolderPath, _overWrite, _log);
        }
    }
}
