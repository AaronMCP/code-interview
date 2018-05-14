using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.Common.Xml;
using HYS.IM.Messaging.Objects;
using HYS.IM.MessageDevices.SOAPAdapter.SOAPServer.Config;
using System.IO;
using HYS.IM.Common.Logging;
using System.Xml.XPath;
using System.Xml;
using System.Collections;
using System.Text.RegularExpressions;

namespace HYS.IM.MessageDevices.SOAPAdapter.SOAPServer.Controler
{
    public partial class SOAPServerControler
    {
        private Hashtable _regExpressionList = new Hashtable();
        public bool MatchRegularExpression(string strValue, string strReg)
        {
            if (strValue == null || strReg == null) return false;

            try
            {
                Regex r = null;
                lock (_regExpressionList.SyncRoot)
                {
                    r = _regExpressionList[strReg] as Regex;
                    if (r == null)
                    {
                        r = new Regex(strReg);
                        _regExpressionList.Add(strReg, r);
                    }
                }
                Match m = r.Match(strValue);
                return m.Success;
            }
            catch (Exception e)
            {
                _context.Log.Write(e);
                return false;
            }
        }

        public string GetMessageKeyword(string xmlString, string xpath, string prefixDef)
        {
            if (xpath == null || xpath.Length < 1) return "";
            if (xmlString == null || xmlString.Length < 1) return "";

            try
            {
                using (StringReader sr = new StringReader(xmlString))
                {
                    XPathDocument doc = new XPathDocument(sr);
                    XPathNavigator nvg = doc.CreateNavigator();

                    XmlNamespaceManager nsMgr = null;
                    if (prefixDef != null && prefixDef.Length > 0)
                    {
                        nsMgr = new XmlNamespaceManager(nvg.NameTable);
                        XSLTExtension.FillXmlNamespaceManager(nsMgr, prefixDef);
                    }

                    object o = (nsMgr == null) ? nvg.Evaluate(xpath) : nvg.Evaluate(xpath, nsMgr);
                    string value = (o != null) ? o.ToString() : "";     //"(null)";

                    XPathNodeIterator i = o as XPathNodeIterator;
                    if (i != null)
                    {
                        StringBuilder sb = new StringBuilder();
                        while (i.MoveNext())
                        {
                            sb.Append(i.Current.Value); //.Append(';');
                        }
                        value = sb.ToString();  //.TrimEnd(';');
                    }

                    return value;
                }
            }
            catch (Exception e)
            {
                _context.Log.Write(e);
                return "";  //"(error)";
            }
        }
    }
}
