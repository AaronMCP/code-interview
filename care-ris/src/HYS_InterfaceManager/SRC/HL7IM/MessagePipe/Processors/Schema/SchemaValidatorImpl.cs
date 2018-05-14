using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.Common.Logging;
using HYS.MessageDevices.MessagePipe.Base;
using HYS.MessageDevices.MessagePipe.Base.Processor;
using HYS.Messaging.Objects;
using HYS.Common.Xml;
using HYS.Messaging.Base.Config;
using System.Xml;
using System.IO;

namespace HYS.MessageDevices.MessagePipe.Processors.Schema
{
    public class SchemaValidatorImpl : IProcessor
    {
        private SchemaValidatorConfig _config;
        private ProcessorInitializationParameter _param;
        private bool _isValid;

        private void WriteLog(Exception err)
        {
            _param.Log.Write(err);
        }
        private void WriteLog(LogType t, string logMsg)
        {
            _param.Log.Write(t, string.Format("[{0}]: {1}",
                SchemaValidatorConfig.DEVICE_NAME, logMsg ));
        }

        #region IProcessor Members

        public bool Initialize(ProcessorInitializationParameter parameter)
        {
            if (parameter == null) return false;

            _param = parameter;
            _config = XObjectManager.CreateObject<SchemaValidatorConfig>(parameter.ConfigXmlString);
            
            if (_config == null)
            {
                WriteLog(LogType.Error, "Deserialize configuration object failed.");
                return false;
            }
            
            return true;
        }

        public bool ProcessMessage(MessagePackage message)
        {
            try
            {
                string xmlString = message.GetMessageXml();
               
                string schemaFile = _param.GetFullPath(_config.SchemaFileName);

                try
                {
                    _param.Log.Write(LogType.Debug, "Start Validate Meta File");
                    XmlReaderSettings rs = new XmlReaderSettings();
                    try
                    {
                        rs.ValidationType = ValidationType.Schema;
                        rs.Schemas.Add(null, schemaFile);

                        rs.ValidationEventHandler += new System.Xml.Schema.ValidationEventHandler(RS_ValidationEventHandler);
                    }
                    catch (Exception ex)
                    {
                        _param.Log.Write(LogType.Debug, "Load schema file error" + ex.Message);
                        _isValid = false;
                        return false;
                    }
                    finally
                    {

                    }

                    System.Xml.XmlReader xr = XmlReader.Create(new StringReader(xmlString), rs);

                    try
                    {

                        while (xr.Read() && _isValid)
                        {
                            _param.Log.Write(LogType.Debug, "Read XML Information: " + xr.Value);
                        }
                    }
                    catch (Exception ex)
                    {
                        _param.Log.Write(LogType.Error, "System gets error while reading XML, " + ex.Message);
                        _isValid = false;
                    }
                    finally
                    {
                        xr.Close();
                    }
                }
                catch (Exception ex)
                {
                    _param.Log.Write(LogType.Error, "System gets error while validateing XML, " + ex.Message);
                    
                    _isValid = false;
                }
                finally
                {

                }
                
                return _isValid;
            }
            catch (Exception e)
            {
                WriteLog(e);
                return false;
            }
        }

        internal void RS_ValidationEventHandler(object sender, System.Xml.Schema.ValidationEventArgs e)
        {
            //if (e.Severity == System.Xml.Schema.XmlSeverityType.Error)
            {
                _isValid = false;
                _param.Log.Write(LogType.Error, "Meta file is invalid, " + e.Message);

            }

        }

        public bool Uninitilize()
        {
            return true;
        }

        #endregion
    }
}
