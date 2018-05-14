using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.Common.Xml;
using HYS.IM.Messaging.Objects;
using HYS.IM.MessageDevices.SOAPAdapter.SOAPServer.Config;
using System.IO;
using HYS.IM.Common.Logging;
using HYS.IM.Messaging.Base.Config;

namespace HYS.IM.MessageDevices.SOAPAdapter.SOAPServer.Controler
{
    public partial class SOAPServerControler
    {
        private Dictionary<string, string> _responseXsltFileList;
        private string GetXsltFileName(string key)
        {
            if (_responseXsltFileList == null)
            {
                _responseXsltFileList = new Dictionary<string, string>();

                //string path = Path.Combine(_context.AppArgument.ConfigFilePath, "ResponseTemplates");
                string path = _context.ConfigMgr.Config.GetResponseXDSGWMessageTemplateFolderFullPath();
                if (!Directory.Exists(path))
                {
                    _context.Log.Write(LogType.Error, "Cannot find folder: " + path);
                    return null;
                }

                string[] flist = Directory.GetFiles(path, "*.xsl*");
                foreach (string f in flist)
                {
                    string fn = Path.GetFileName(f);
                    string[] strList = fn.Split('.');
                    if (strList.Length < 1) continue;
                    
                    string fk = strList[0];
                    if (_responseXsltFileList.ContainsKey(fk))
                    {
                        _context.Log.Write(LogType.Information, "The following file has duplicated key and will be ignored: " + f);
                        continue;
                    }

                    _responseXsltFileList.Add(fk, f);
                }
            }

            if (_responseXsltFileList.ContainsKey(key)) return _responseXsltFileList[key];
            else if (_responseXsltFileList.ContainsKey("ANY")) return _responseXsltFileList["ANY"];
            else return null;
        }

        private Dictionary<string, XMLTransformer> _responseTransformerList;
        private XMLTransformer GetXMLTransformer(string key)
        {
            if (_responseTransformerList == null)
                _responseTransformerList = new Dictionary<string, XMLTransformer>();

            if (_responseTransformerList.ContainsKey(key))
            {
                return _responseTransformerList[key];
            }
            else
            {
                string xsltFileName = GetXsltFileName(key);
                if (xsltFileName == null) return null;

                XMLTransformer t = XMLTransformer.CreateFromFile(xsltFileName, _context.Log);
                if (t == null) return null;

                _responseTransformerList.Add(key, t);
                return t;
            }
        }

        public string GetSampleResponseMessageContent(string key, string xmlString)
        {
            string str = "";
            bool res = false;

            _context.Log.Write(string.Format("Begin generating response message for requesting message with key: {0}.", key));

            XMLTransformer t = GetXMLTransformer(key);
            if (t == null)
            {
                _context.Log.Write(LogType.Error, "Cannot find any template to generate response message, return an empty message any way.");
                str = "<Message><Header/><Body/></Message>";
            }
            else
            {
                res = t.TransformString(xmlString, ref str,
                   _context.ConfigMgr.Config.InboundMessageDispatching.XSLTExtensionsForGeneratingResponse);
            }

            _context.Log.Write(string.Format("End generating response message for requesting message with key: {0}. Result: {1}", key, res));

            return str;
        }

        public string GetPublishResponseMessageContent(string xsltFileName, string sourceXml)
        {
            bool res = false;
            string targetXml = "";
            
            _context.Log.Write(string.Format("Begin generating response message with file: {0}.", xsltFileName));
            string xslFile = ConfigHelper.GetFullPath(_context.AppArgument.ConfigFilePath, xsltFileName);
            XMLTransformer it = XMLTransformer.CreateFromFileWithCache(xslFile, _context.Log);
            res = it.TransformString(sourceXml, ref targetXml,
                _context.ConfigMgr.Config.InboundMessageDispatching.XSLTExtensionsForGeneratingResponse);
            _context.Log.Write(string.Format("End generating response message with file: {0}. Result: {1}", xsltFileName, res));

            return targetXml;
        }
    }
}
