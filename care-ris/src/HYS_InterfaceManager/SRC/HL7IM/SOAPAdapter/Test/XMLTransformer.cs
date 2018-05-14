using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Web;

namespace HYS.MessageDevices.SOAPAdapter.Test
{
    public class XMLTransformer
    {
        protected XslCompiledTransform _xslTrans;
        protected XMLTransformer(XslCompiledTransform xslTrans)
        {
            _xslTrans = xslTrans;
        }

        public static Exception LastError;
        public static string LastErrorInfor
        {
            get { return LastError == null ? "" : LastError.ToString(); }
        }
        public static event EventHandler OnError;
        protected static void NotifyError(Exception err)
        {
            LastError = err;
            if (OnError != null) OnError(typeof(XMLTransformer), EventArgs.Empty);
        }

        //private static Hashtable _transformerList = new Hashtable();
        //public static XMLTransformer CreateFromMessage(XIMMessage message)
        //{
        //    if (message == null) return null;
        //    XMLTransformer t = _transformerList[message] as XMLTransformer;
        //    if (t == null)
        //    {
        //        string path = Application.StartupPath + "\\" + XIMTransformHelper.XSLFolder + "\\" + message.XSLFileName;
        //        t = CreateFromFile(path);
        //        if (t == null) return null;
        //        _transformerList.Add(message, t);
        //    }
        //    return t;
        //}
        public static XMLTransformer CreateFromFile(string xslFileName)
        {
            try
            {
                XslCompiledTransform myXslTrans = new XslCompiledTransform(true);
                myXslTrans.Load(xslFileName, new XsltSettings(true, true), null);
                return new XMLTransformer(myXslTrans);
            }
            catch (Exception err)
            {
                NotifyError(err);
                return null;
            }
        }
        public static XMLTransformer CreateFromString(string xslString)
        {
            try
            {
                using (StringReader sr = new StringReader(xslString))
                {
                    using (XmlReader xr = XmlReader.Create(sr))
                    {
                        XslCompiledTransform myXslTrans = new XslCompiledTransform();
                        myXslTrans.Load(xr, new XsltSettings(true, true), null);
                        return new XMLTransformer(myXslTrans);
                    }
                }
            }
            catch (Exception err)
            {
                NotifyError(err);
                return null;
            }
        }

        public bool TransformFile(string sourceFile, string targetFile)
        {
            try
            {   
                XPathDocument myXPathDoc = new XPathDocument(sourceFile);
                using (XmlTextWriter myWriter = new XmlTextWriter(targetFile, null))
                {
                    _xslTrans.Transform(myXPathDoc, null, myWriter);
                    return true;
                }
            }
            catch (Exception err)
            {
                NotifyError(err);
                return false;
            }
        }
        public bool TransformXml(XmlReader sourceXml, XmlWriter targetXml)
        {
            try
            {
                _xslTrans.Transform(sourceXml, targetXml);

                return true;
            }
            catch (Exception err)
            {
                NotifyError(err);
                return false;
            }
        }
        public bool TransformXml2(XmlReader sourceXml, XmlWriter targetXml, string xmlString)
        {
            XsltArgumentList args = new XsltArgumentList();
            try
            {
                // right
                MyXmlDomImpl myDom = new MyXmlDomImpl(xmlString);

                // not right
                //MyXmlDomImpl myDom = new MyXmlDomImpl(XmlReader.Create(sourceXml, new XmlReaderSettings()));

                args.XsltMessageEncountered += new XsltMessageEncounteredEventHandler(args_XsltMessageEncountered);
                args.AddExtensionObject("urn:myXmlDom", myDom);
                _xslTrans.Transform(sourceXml, args, targetXml);

                return true;
            }
            catch (Exception err)
            {
                NotifyError(err);
                return false;
            }
            finally
            {
                args.XsltMessageEncountered -= new XsltMessageEncounteredEventHandler(args_XsltMessageEncountered);
            }
        }

        private void args_XsltMessageEncountered(object sender, XsltMessageEncounteredEventArgs e)
        {
            throw new Exception(e.Message);
        }
        public bool TransformString(string sourceString, ref string targetString)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                using (StringWriter sw = new StringWriter(sb))
                {
                    using (StringReader sr = new StringReader(sourceString))
                    {
                        using (XmlTextReader xtr = new XmlTextReader(sr))
                        {
                            using (XmlTextWriter stw = new XmlTextWriter(sw))
                            {
                                stw.Formatting = Formatting.Indented;
                                //if (!TransformXml(xtr, stw)) return false;
                                if (!TransformXml2(xtr, stw, sourceString)) return false;
                                targetString = sb.ToString();
                                return true;
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                NotifyError(err);
                return false;
            }
        }

        public class MyXmlDomImpl
        {
            private XmlDocument _doc;
            public MyXmlDomImpl(string xmlString)
            {
                _doc = new XmlDocument();
                _doc.LoadXml(xmlString);
            }
            public MyXmlDomImpl(XmlReader sourceXml)
            {
                _doc = new XmlDocument();
                _doc.Load(sourceXml);
            }

            public string GetHtmlEncodedInnerXml(string xpath)
            {
                XmlNamespaceManager nsm = new XmlNamespaceManager(_doc.NameTable);
                nsm.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
                nsm.AddNamespace("csh", "http://www.carestreamhealth.com/");
                XmlNode node = _doc.SelectSingleNode(xpath, nsm);
                string str = node.InnerXml;
                return HttpUtility.HtmlEncode(str);
            }

            //private XmlNode FindXmlNode(string xpath, string prefixes)
            //{
            //    XmlNode node = null;

            //    if (prefixes == null || prefixes.Length < 1)
            //    {
            //        node = _doc.SelectSingleNode(xpath);
            //    }
            //    else
            //    {
            //        XmlNamespaceManager nsm = new XmlNamespaceManager(_doc.NameTable);
            //        string[] prefixList = prefixes.Split('|');

            //        int i = 0;
            //        while (i < prefixList.Length)
            //        {
            //            string prefix = prefixList[i];
            //            if (++i >= prefixList.Length) break;
            //            string nsURI = prefixList[i++];
            //            nsm.AddNamespace(prefix, nsURI);
            //        }

            //        node = _doc.SelectSingleNode(xpath, nsm);
            //    }

            //    return node;
            //}

            //public string GetHtmlEncodedInnerXml(string xpath)
            //{
            //    return GetHtmlEncodedInnerXml(xpath, null);
            //}
            //public string GetHtmlEncodedInnerXml(string xpath, string prefixes)
            //{
            //    if (xpath == null || xpath.Length < 1) return "";
            //    XmlNode node = FindXmlNode(xpath, prefixes);
            //    if (node == null) return "";
            //    string str = node.InnerXml;
            //    return HttpUtility.HtmlEncode(str);
            //}
            //public string GetHtmlEncodedOuterXml(string xpath)
            //{
            //    return GetHtmlEncodedOuterXml(xpath, null);
            //}
            //public string GetHtmlEncodedOuterXml(string xpath, string prefixes)
            //{
            //    if (xpath == null || xpath.Length < 1) return "";
            //    XmlNode node = FindXmlNode(xpath, prefixes);
            //    if (node == null) return "";
            //    string str = node.OuterXml;
            //    return HttpUtility.HtmlEncode(str);
            //}
        }
    }
}
