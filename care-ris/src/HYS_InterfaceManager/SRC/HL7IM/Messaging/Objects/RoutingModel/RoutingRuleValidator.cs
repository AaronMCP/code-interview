using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;
using System.Xml;
using System.IO;
using System.Xml.XPath;
using HYS.Common.Xml;

namespace HYS.IM.Messaging.Objects.RoutingModel
{
    public class RoutingRuleValidator
    {
        private Hashtable _regExpressionList = new Hashtable();
        private bool MatchRegularExpression(string strValue, string strReg)
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
                //if (log != null) log.Write(e);
                _lastError = e;
                return false;
            }
        }
        
        private void FillXmlNamespaceManager(XmlNamespaceManager mgr, string prefixDef)
        {
            if (mgr == null || prefixDef == null || prefixDef.Length < 1) return;

            int i = 0;
            string[] prefixList = prefixDef.Split('|');
            while (i < prefixList.Length)
            {
                string prefix = prefixList[i].Trim();
                if (++i >= prefixList.Length) break;
                string nsURI = prefixList[i++].Trim();
                mgr.AddNamespace(prefix, nsURI);
            }
        }
        private string GetMessageKeyword(string xmlString, string xpath, string prefixDef)
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
                        FillXmlNamespaceManager(nsMgr, prefixDef);
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
                _lastError = e;
                //if (log != null) log.Write(e);
                return "";  //"(error)";
            }
        }

        private Hashtable _msgContentList = new Hashtable();
        private bool MatchMessageContent(Message msg, ContentCriteria criteria)
        {
            if (criteria == null) return false;

            string msgXml = "";
            lock (_msgContentList.SyncRoot)
            {
                msgXml = _msgContentList[msg] as string;
                if (msgXml == null)
                {
                    msgXml = msg.ToXMLString();
                    _msgContentList.Add(msg, msgXml);
                }
            }

            string keyword = GetMessageKeyword(msgXml, criteria.XPath, criteria.XPathPrefixDefinition);
            return MatchRegularExpression(keyword, criteria.RegularExpression);
        }

        private bool MatchMessage(Message msg, IRoutingRule rule)
        {
            bool res = false;
            if (msg == null || rule == null) return res;

            switch (rule.Type)
            {
                default:
                case RoutingRuleType.MessageType:
                    {
                        if (msg.Header == null || msg.Header.Type == null) return res;
                        XCollection<MessageType> mtlist = rule.MessageTypeList;
                        if (mtlist == null) return res;
                        foreach (MessageType mt in mtlist)
                        {
                            if (msg.Header.Type.EqualsTo(mt))
                            {
                                res = true;
                                break;
                            }
                        }
                        break;
                    }
                case RoutingRuleType.ContentBased:
                    {
                        return MatchMessageContent(msg, rule.ContentCriteria);
                    }
            }

            return res;
        }
        public bool Match(Message msg)
        {
            return MatchMessage(msg, _rule);
        }

        private IRoutingRule _rule;
        public RoutingRuleValidator(IRoutingRule rule)
        {
            _rule = rule;
        }

        private Exception _lastError;
        public Exception LastError
        {
            get { return _lastError; }
        }
        public string LastErrorInfo
        {
            get { return (_lastError == null) ? "" : _lastError.ToString(); }
        }

        private static Hashtable _routingRuleValidatorList = new Hashtable();
        public static RoutingRuleValidator CreateRoutingRuleValidatorFromCache(IRoutingRule r)
        {
            if (r == null) return null;

            RoutingRuleValidator v = null;
            lock (_routingRuleValidatorList.SyncRoot)
            {
                v = _routingRuleValidatorList[r] as RoutingRuleValidator;
                if (v == null)
                {
                    v = new RoutingRuleValidator(r);
                    _routingRuleValidatorList.Add(r, v);
                }
            }

            return v;
        }
    }
}
