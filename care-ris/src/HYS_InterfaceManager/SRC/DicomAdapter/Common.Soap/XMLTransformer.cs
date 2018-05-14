using System;
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
    public class XMLTransformer
    {
        private ILogging _log;
        private XslCompiledTransform _xslTrans;
        private static Hashtable _transformerList = new Hashtable();

        private XMLTransformer(XslCompiledTransform xslTrans, ILogging log)
        {
            _xslTrans = xslTrans;
            _log = log;
        }
        
        private void NotifyError(Exception e)
        {
            if (_log != null) _log.Write(e);
        }
        private void NotifyInfo(string s)
        {
            if (_log != null) _log.Write(LogType.Info, s);
        }

        public static XMLTransformer CreateFromFile(string xslFileName, ILogging log)
        {
            return CreateFromFile(xslFileName, log, false);
        }
        public static XMLTransformer CreateFromFileWithCache(string xslFileName, ILogging log)
        {
            return CreateFromFileWithCache(xslFileName, log, false);
        }
        public static XMLTransformer CreateFromString(string xslString, ILogging log)
        {
            return CreateFromString(xslString, log, false);
        }

        /// <summary>
        /// If your XSLT file need to include/import other XLST file, you cannot use script or call any extension in the XSLT. So set enableExtension as false.
        /// </summary>
        /// <param name="xslFileName"></param>
        /// <param name="log"></param>
        /// <param name="enableExtension"></param>
        /// <returns></returns>
        public static XMLTransformer CreateFromFile(string xslFileName, ILogging log, bool enableExtension)
        {
            try
            {
                XslCompiledTransform myXslTrans = new XslCompiledTransform();
                if (enableExtension) myXslTrans.Load(xslFileName, new XsltSettings(true, true), null);
                else myXslTrans.Load(xslFileName);
                return new XMLTransformer(myXslTrans, log);
            }
            catch (Exception err)
            {
                if (log != null) log.Write(err);
                return null;
            }
        }
        /// <summary>
        /// If your XSLT file need to include/import other XLST file, you cannot use script or call any extension in the XSLT. So set enableExtension as false.
        /// </summary>
        /// <param name="xslFileName"></param>
        /// <param name="log"></param>
        /// <param name="enableExtension"></param>
        /// <returns></returns>
        public static XMLTransformer CreateFromFileWithCache(string xslFileName, ILogging log, bool enableExtension)
        {
            if (xslFileName == null) return null;
            XMLTransformer t = null;
            lock (_transformerList.SyncRoot)
            {
                t = _transformerList[xslFileName] as XMLTransformer;
                if (t == null)
                {
                    t = CreateFromFile(xslFileName, log, enableExtension);
                    if (t != null) _transformerList.Add(xslFileName, t);
                }
            }
            return t;
        }
        /// <summary>
        /// If your XSLT file need to include/import other XLST file, you cannot use script or call any extension in the XSLT. So set enableExtension as false.
        /// </summary>
        /// <param name="xslFileName"></param>
        /// <param name="log"></param>
        /// <param name="enableExtension"></param>
        /// <returns></returns>
        public static XMLTransformer CreateFromString(string xslString, ILogging log, bool enableExtension)
        {
            try
            {
                using (StringReader sr = new StringReader(xslString))
                {
                    using (XmlReader xr = XmlReader.Create(sr))
                    {
                        XslCompiledTransform myXslTrans = new XslCompiledTransform();
                        if (enableExtension) myXslTrans.Load(xr, new XsltSettings(true, true), null);
                        else myXslTrans.Load(xr);
                        return new XMLTransformer(myXslTrans, log);
                    }
                }
            }
            catch (Exception err)
            {
                if (log != null) log.Write(err);
                return null;
            }
        }
        public static void ClearTransformerCache()
        {
            lock (_transformerList.SyncRoot)
            {
                _transformerList.Clear();
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
            return TransformXmlWithExtension(sourceXml, targetXml, null);
        }
        public bool TransformString(string sourceString, ref string targetString)
        {
            return TransformString(sourceString, ref targetString, XSLTExtensionTypes.None, null);
        }
        public bool TransformString(string sourceString, ref string targetString, XSLTExtensionTypes extType)
        {
            return TransformString(sourceString, ref targetString, extType, null);
        }
        public bool TransformString(string sourceString, ref string targetString, XSLTExtensionTypes extType, string additionalString)
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

                                XsltArgumentList arg = XSLTExtension.GetXsltArgumentList(extType, sourceString, additionalString);
                                bool res = TransformXmlWithExtension(xtr, stw, arg);

                                if (res) targetString = sb.ToString();
                                return res;

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

        private bool TransformXmlWithExtension(XmlReader sourceXml, XmlWriter targetXml, XsltArgumentList args)
        {
            try
            {
                if (args == null)
                {
                    _xslTrans.Transform(sourceXml, targetXml);
                }
                else
                {
                    args.XsltMessageEncountered += new XsltMessageEncounteredEventHandler(XsltArgumentList_XsltMessageEncountered);
                    _xslTrans.Transform(sourceXml, args, targetXml);
                }
                return true;
            }
            catch (Exception err)
            {
                NotifyError(err);
                return false;
            }
            finally
            {
                if (args != null)
                    args.XsltMessageEncountered -= new XsltMessageEncounteredEventHandler(XsltArgumentList_XsltMessageEncountered);
            }
        }
        private void XsltArgumentList_XsltMessageEncountered(object sender, XsltMessageEncounteredEventArgs e)
        {
            NotifyInfo(e.Message);
        }
    }
}
