using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HYS.XmlAdapter.Common.Objects
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

        private static Hashtable _transformerList = new Hashtable();
        public static XMLTransformer CreateFromMessage(XIMMessage message)
        {
            if (message == null) return null;
            XMLTransformer t = _transformerList[message] as XMLTransformer;
            if (t == null)
            {
                string path = Application.StartupPath + "\\" + XIMTransformHelper.XSLFolder + "\\" + message.XSLFileName;
                t = CreateFromFile(path);
                if (t == null) return null;
                _transformerList.Add(message, t);
            }
            return t;
        }
        public static XMLTransformer CreateFromFile(string xslFileName)
        {
            try
            {
                XslCompiledTransform myXslTrans = new XslCompiledTransform();
                myXslTrans.Load(xslFileName);
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
                        myXslTrans.Load(xr);
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
                                if (!TransformXml(xtr, stw)) return false;
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
    }
}
