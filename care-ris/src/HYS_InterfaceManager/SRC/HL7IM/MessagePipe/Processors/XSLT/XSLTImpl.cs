using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.MessageDevices.MessagePipe.Base.Processor;
using HYS.MessageDevices.MessagePipe.Base;
using HYS.Common.Logging;
using HYS.Common.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.IO;
using System.Xml;
using HYS.Messaging.Objects;

namespace HYS.MessageDevices.MessagePipe.Processors.XSLT
{
    public class XSLTImpl : IProcessor
    {
        private XSLTConfig _config;
        private ProcessorInitializationParameter _param;

        XslCompiledTransform xslt;

        private void WriteLog(Exception err)
        {
            _param.Log.Write(err);
        }
        private void WriteLog(LogType t, string logMsg)
        {
            _param.Log.Write(t, string.Format("[{0}]: {1}",
                XSLTConfig.DEVICE_NAME, logMsg));
        }

        #region IProcessor Members

        public bool Initialize(ProcessorInitializationParameter parameter)
        {
            if (parameter == null) return false;

            _param = parameter;
            _config = XObjectManager.CreateObject<XSLTConfig>(parameter.ConfigXmlString);

            if (_config == null)
            {
                WriteLog(LogType.Error, "Deserialize configuration object failed.");
                return false;
            }

            try
            {
                string XSLTFile = _param.GetFullPath(_config.XSLTFileName);

                xslt = new XslCompiledTransform();
                xslt.Load(XSLTFile);
            }catch(Exception ex)
            {
                _param.Log.Write(ex);
                return false;
            }
            return true;
        }

        public bool ProcessMessage(MessagePackage message)
        {          

            try
            {
                string xmlString;
                try
                {                    
                    Message msg = XObjectManager.CreateObject<Message>(message.GetMessageXml());
                    TextReader tr = new StringReader(msg.Body);
                    //XmlDocument doc = new XmlDocument();
                    //doc.LoadXml(msg.Body);
                    //XPathDocument doc = new XPathDocument(tr);
                    Stream tmpS = new MemoryStream();
                    XmlWriter xw = new XmlTextWriter(tmpS, null);
                    //XPathNavigator nav = doc.CreateNavigator();

                    try
                    {
                        xslt.Transform(XmlReader.Create(tr), xw);
                        xw.Flush();
                        tmpS.Position = 0;

                        StreamReader sr = new StreamReader(tmpS);

                        xmlString = sr.ReadToEnd();
                        sr.Close();
                    }
                    catch (Exception ex)
                    {
                        throw (ex);
                    }
                    finally
                    {
                        xw.Close();
                        tmpS.Close();
                    }

                    msg.Body = xmlString;
                    message.SetMessageXml(msg.ToXMLString(), _config.DeviceName);
                }
                catch (Exception ee)
                {
                    _param.Log.Write(LogType.Error, "Get error while transforming XML, " + ee.Message);
                                       
                    return false;
                }

                               
                
                return true;
            }
            catch (Exception e)
            {
                WriteLog(e);
                return false;
            }
        }

        public bool Uninitilize()
        {
            return true;
        }

        #endregion
    }
}
