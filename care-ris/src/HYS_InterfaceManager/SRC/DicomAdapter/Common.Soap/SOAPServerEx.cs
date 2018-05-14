using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using HYS.Common.Objects.Logging;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;

namespace HYS.Common.Soap
{
    public class SOAPServerExConfig : XObject
    {
        [XCData(true)]
        public string ServerURI { get; set; }

        public bool DumpMessage { get; set; }
        public bool EnableRequestTransform { get; set; }
        public bool EnableResponseTransform { get; set; }
    }

    public class SOAPServerEx
    {
        private ILogging _log;
        private SOAPServer _server;
        private SOAPServerExConfig _cfg;
        private string _cfgFolderPath;
        private string _reqXsltFilePath;
        private string _rspXsltFilePath;

        public SOAPServerEx(SOAPServerExConfig cfg, string cfgFolderPath, ILogging log)
        {
            _log = log;
            _cfg = cfg;
            _cfgFolderPath = cfgFolderPath;
            _reqXsltFilePath = Path.Combine(_cfgFolderPath, XMLConfigHelper.RequestXSLTFileNameForSOAPServer);
            _rspXsltFilePath = Path.Combine(_cfgFolderPath, XMLConfigHelper.ResponseXSLTFileNameForSOAPServer);
            string wcfFilePath = Path.Combine(_cfgFolderPath, SOAPConfigHelper.SOAPServerWCFConfigFileName);
            string errFilePath = Path.Combine(_cfgFolderPath, SOAPConfigHelper.SOAPErrorMessageFileName);
            _server = new SOAPServer(wcfFilePath, errFilePath, log);
            _server.OnMessageReceived += new ReceiveSOAPMessageHandler(_server_OnMessageReceived);
        }

        public event ReceiveSOAPMessageHandler OnMessageReceived;
        private bool _server_OnMessageReceived(string requestSOAPEnvelope, out string responseSOAPEnvelope)
        {
            responseSOAPEnvelope = null;
            if (OnMessageReceived == null) return false;

            if (_cfg.DumpMessage)
            {
                _log.Write("--- Requesting SOAP Message (original) ---");
                _log.Write(requestSOAPEnvelope);
                _log.Write("------------------------------------------");
            }

            string request = requestSOAPEnvelope;
            if (_cfg.EnableRequestTransform)
            {
                if (!TransformXML(_reqXsltFilePath, requestSOAPEnvelope, ref request, _log)) return false;

                if (_cfg.DumpMessage)
                {
                    _log.Write("--- Requesting SOAP Message (transformed) ---");
                    _log.Write(request);
                    _log.Write("------------------------------------------");
                }
            }

            if (!OnMessageReceived(request, out responseSOAPEnvelope)) return false;

            if (_cfg.DumpMessage)
            {
                _log.Write("--- Responding SOAP Message (original) ---");
                _log.Write(responseSOAPEnvelope);
                _log.Write("------------------------------------------");
            }

            if (_cfg.EnableResponseTransform)
            {
                string response = responseSOAPEnvelope;
                if (!TransformXML(_rspXsltFilePath, responseSOAPEnvelope, ref response, _log)) return false;
                responseSOAPEnvelope = response;

                if (_cfg.DumpMessage)
                {
                    _log.Write("--- Responding SOAP Message (transformed) ---");
                    _log.Write(responseSOAPEnvelope);
                    _log.Write("------------------------------------------");
                }
            }

            return true;
        }
        private bool TransformXML(string xslFilePath, string sourceXml, ref string targetXml, ILogging log)
        {
            XMLTransformer t = XMLTransformer.CreateFromFileWithCache(xslFilePath, log, true);
            if (t == null) return false;
            return t.TransformString(sourceXml, ref targetXml, XSLTExtensionTypes.XmlNodeTransformer);
        }

        public bool Start()
        {
            return _server.Start(_cfg.ServerURI);
        }
        public bool Stop()
        {
            return _server.Stop();
        }
    }
}
