using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;
using System.IO;
using System.Xml.XPath;
using System.Xml;
using HYS.Common.Logging;

namespace HYS.MessageDevices.MessagePipe.Base.Channel
{
    public class MessageEntryHelper
    {
        private static Hashtable _regExpressionList = new Hashtable();
        internal static bool MatchRegularExpression(ILog log, string strValue, string strReg)
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
                if (log != null) log.Write(e);
                return false;
            }
        }

        private static void FillXmlNamespaceManager(XmlNamespaceManager mgr, string prefixDef)
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
        internal static string GetMessageKeyword(ILog log, string xmlString, string xpath, string prefixDef)
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
                if (log != null) log.Write(e);
                return "";  //"(error)";
            }
        }

        internal static bool MatchEntryCriteria(ILog log, string msgXml, MessageEntryCriteria criteria)
        {
            if (msgXml == null || criteria == null) return false;
            string keyword = GetMessageKeyword(log, msgXml, criteria.XPath, criteria.XPathPrefixDefinition);
            return MatchRegularExpression(log, keyword, criteria.RegularExpression);
        }
        public static bool MatchEntryCriteria(ChannelInitializationParameter param, MessagePackage msgPackage, MessageEntryConfig cfg)
        {
            if (msgPackage == null || 
                msgPackage.OriginalMessage == null ||
                msgPackage.OriginalMessage.Header == null ||
                cfg == null || param == null) return false;

            if (msgPackage.IsAccepted())
            {
                param.Log.Write(LogType.Information,
                    string.Format("The message package {0} has been accepted by other channel.",
                    msgPackage.OriginalMessage.Header.ToString()));
                return false;
            }

            bool res = false;
            switch (cfg.CheckingModel)
            {
                default:
                case MessageEntryCheckingModel.AcceptAnyUnacceptedMessage:
                    {
                        res = true;
                        break;
                    }
                case MessageEntryCheckingModel.AcceptUnacceptedMessageAccordingToMessageType:
                    {
                        res = cfg.EntryMessageType.EqualsTo(msgPackage.OriginalMessage.Header.Type);
                        break;
                    }
                case MessageEntryCheckingModel.AcceptUnacceptedMessageAccordingToEntryCriteria:
                    {
                        res = MatchEntryCriteria(param.Log, msgPackage.GetMessageXml(), cfg.EntryCriteria);
                        break;
                    }
            }

            if (res) msgPackage.Accept(param.ChannelName);

            return res;
        }
    }
}
