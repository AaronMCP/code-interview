﻿using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.Collections;
using System.Collections.Generic;
//using System.Windows.Forms;
using System.Web;
using HYS.Common.Objects.Logging;

namespace HYS.Common.Soap
{
    [Flags]
    public enum XSLTExtensionTypes
    {
        None = 0,
        XmlNodeTransformer = 1,
        AdditionalXmlDocument = 2
    }

    public class XSLTExtension
    {
        //public static XsltArgumentList GetXsltArgumentList(XSLTExtensionTypes t, string sourceXml)
        //{
        //    switch ((int)t)
        //    {
        //        default:
        //        case 0:
        //            return null;
        //        case 1:
        //            XsltArgumentList list = new XsltArgumentList();
        //            list.AddExtensionObject("urn:xdsgw:XmlNodeTransformer", new XmlNodeTransformer(sourceXml));
        //            return list;
        //    }
        //}

        internal static XsltArgumentList GetXsltArgumentList(XSLTExtensionTypes t, string sourceXml, string additionalXml)
        {
            if ((int)t == 0) return null;
            XsltArgumentList list = new XsltArgumentList();
            if ((t & XSLTExtensionTypes.XmlNodeTransformer) == XSLTExtensionTypes.XmlNodeTransformer)
                list.AddExtensionObject("urn:xdsgw:XmlNodeTransformer", new XmlNodeTransformer(sourceXml));
            if ((t & XSLTExtensionTypes.AdditionalXmlDocument) == XSLTExtensionTypes.AdditionalXmlDocument)
                list.AddExtensionObject("urn:xdsgw:AdditionalXmlDocument", new AdditionalXmlDocument(additionalXml));
            return list;
        }

        public static void FillXmlNamespaceManager(XmlNamespaceManager mgr, string prefixDef)
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
        internal static string GetValueWithXPath(string xmlString, string xpath, string prefixDef)
        {
            if (xpath == null || xpath.Length < 1) return "";
            if (xmlString == null || xmlString.Length < 1) return "";

            //try
            //{
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
            //}
            //catch (Exception e)
            //{
            //    Program.Log.Write(e);
            //    return "";  //"(error)";
            //}
        }
        public static string GetValueWithXPath(string xmlString, string xpath, string prefixDef, ILogging log)
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
                log.Write(e);
                return "";  //"(error)";
            }
        }
    }

    public class XmlNodeTransformer
    {
        private XmlDocument _doc;
        public XmlNodeTransformer(string xmlString)
        {
            _doc = new XmlDocument();
            _doc.LoadXml(xmlString);
        }

        private XmlNode FindXmlNode(string xpath, string prefixes)
        {
            XmlNode node = null;

            if (prefixes == null || prefixes.Length < 1)
            {
                node = _doc.SelectSingleNode(xpath);
            }
            else
            {
                XmlNamespaceManager nsm = new XmlNamespaceManager(_doc.NameTable);
                XSLTExtension.FillXmlNamespaceManager(nsm, prefixes);

                //int i = 0;
                //string[] prefixList = prefixes.Split('|');
                //while (i < prefixList.Length)
                //{
                //    string prefix = prefixList[i].Trim();
                //    if (++i >= prefixList.Length) break;
                //    string nsURI = prefixList[i++].Trim();
                //    nsm.AddNamespace(prefix, nsURI);
                //}

                node = _doc.SelectSingleNode(xpath, nsm);
            }

            return node;
        }

        public string GetHtmlEncodedInnerXml(string xpath)
        {
            return GetHtmlEncodedInnerXml(xpath, null);
        }
        public string GetHtmlEncodedInnerXml(string xpath, string prefixes)
        {
            if (xpath == null || xpath.Length < 1) return "";
            XmlNode node = FindXmlNode(xpath, prefixes);
            if (node == null) return "";
            string str = node.InnerXml;
            return HttpUtility.HtmlEncode(str);
        }
        public string GetHtmlEncodedOuterXml(string xpath)
        {
            return GetHtmlEncodedOuterXml(xpath, null);
        }
        public string GetHtmlEncodedOuterXml(string xpath, string prefixes)
        {
            if (xpath == null || xpath.Length < 1) return "";
            XmlNode node = FindXmlNode(xpath, prefixes);
            if (node == null) return "";
            string str = node.OuterXml;
            return HttpUtility.HtmlEncode(str);
        }

        /// <summary>
        /// In soap message, it seams that use this Xml Escaping is enough. 
        /// It only replace the < and > character.
        /// However the HtmlEncoded replace <, >, &, ", ', etc. characters.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        internal static string XmlEscape(string str)
        {
            return str.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;");
        }

        public string GetEscapingInnerXml(string xpath)
        {
            return GetEscapingInnerXml(xpath, null);
        }
        public string GetEscapingInnerXml(string xpath, string prefixes)
        {
            if (xpath == null || xpath.Length < 1) return "";
            XmlNode node = FindXmlNode(xpath, prefixes);
            if (node == null) return "";
            string str = node.InnerXml;
            return XmlEscape(str);
        }
        public string GetEscapingOuterXml(string xpath)
        {
            return GetEscapingOuterXml(xpath, null);
        }
        public string GetEscapingOuterXml(string xpath, string prefixes)
        {
            if (xpath == null || xpath.Length < 1) return "";
            XmlNode node = FindXmlNode(xpath, prefixes);
            if (node == null) return "";
            string str = node.OuterXml;
            return XmlEscape(str);
        }

        /// <summary>
        /// The disable-output-escaping="yes" parameter in the value-of element will also replace &amp; to &.
        /// However, the & is not allow in XML. 
        /// Therefore, please use the following function to process the Xml escaping value of an element.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        internal static string XmlDescape(string str)
        {
            return str.Replace("&amp;", "&").Replace("&lt;", "<").Replace("&gt;", ">");
        }

        public string GetDescapingInnerXml(string xpath)
        {
            return GetDescapingInnerXml(xpath, null);
        }
        public string GetDescapingInnerXml(string xpath, string prefixes)
        {
            if (xpath == null || xpath.Length < 1) return "";
            XmlNode node = FindXmlNode(xpath, prefixes);
            if (node == null) return "";
            string str = node.InnerXml;
            return XmlDescape(str);
        }
        public string GetDescapingOuterXml(string xpath)
        {
            return GetDescapingOuterXml(xpath, null);
        }
        public string GetDescapingOuterXml(string xpath, string prefixes)
        {
            if (xpath == null || xpath.Length < 1) return "";
            XmlNode node = FindXmlNode(xpath, prefixes);
            if (node == null) return "";
            string str = node.OuterXml;
            return XmlDescape(str);
        }
    }

    public class AdditionalXmlDocument
    {
        private string _xmlString;
        public AdditionalXmlDocument(string xmlString)
        {
            _xmlString = xmlString;
        }

        public string GetValue(string xpath)
        {
            return GetValue(xpath, null);
        }
        public string GetValue(string xpath, string prefixes)
        {
            if (xpath == null || xpath.Length < 1) return "";
            return XSLTExtension.GetValueWithXPath(_xmlString, xpath, prefixes);
        }
    }
}
