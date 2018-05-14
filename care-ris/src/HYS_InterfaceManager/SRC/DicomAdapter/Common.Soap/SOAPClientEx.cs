using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.Common.Xml;
using HYS.Common.Objects.Logging;
using System.IO;

namespace HYS.Common.Soap
{
    public class SOAPClientExConfig : XObject
    {
        public SOAPClientExConfig()
        {

        }

        public SOAPClientExConfig(string ServerURI, string SOAPAction, bool EnableRequestTransform, bool EnableResponseTransform)
        {
            this.ServerURI = ServerURI;
            this.SOAPAction = SOAPAction;
            this.EnableRequestTransform = EnableRequestTransform;
            this.EnableResponseTransform = EnableResponseTransform;
        }

        [XCData(true)]
        public string ServerURI { get; set; }
        [XCData(true)]
        public string SOAPAction { get; set; }

        public bool DumpMessage { get; set; }
        public bool EnableRequestTransform { get; set; }
        public bool EnableResponseTransform { get; set; }
    }

    public class SOAPClientEx
    {
        private ILogging _log;
        private SOAPClient _client;
        private SOAPClientExConfig _cfg;
        private string _cfgFolderPath;
        private string _rspXsltFilePath;
        private string _reqXsltFilePath;

        public SOAPClientEx(SOAPClientExConfig cfg, string cfgFolderPath, ILogging log)
        {
            _log = log;
            _cfg = cfg;
            _cfgFolderPath = cfgFolderPath;
            _rspXsltFilePath = Path.Combine(_cfgFolderPath, XMLConfigHelper.ResponseXSLTFileNameForSOAPClient);
            _reqXsltFilePath = Path.Combine(_cfgFolderPath, XMLConfigHelper.RequestXSLTFileNameForSOAPClient);
            string wcfFilePath = Path.Combine(_cfgFolderPath, SOAPConfigHelper.SOAPClientWCFConfigFileName);
            _client = new SOAPClient(wcfFilePath, log);
        }
        public bool SendMessage(string requestSOAPEnvelope, out string responseSOAPEnvelope)
        {
            responseSOAPEnvelope = null;

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
                    _log.Write("--- Requesting SOAP Message (trasformed) ---");
                    _log.Write(requestSOAPEnvelope);
                    _log.Write("------------------------------------------");
                }
            }

            if (!_client.SendMessage(_cfg.ServerURI, _cfg.SOAPAction, request, out responseSOAPEnvelope)) return false;

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
    }
}
