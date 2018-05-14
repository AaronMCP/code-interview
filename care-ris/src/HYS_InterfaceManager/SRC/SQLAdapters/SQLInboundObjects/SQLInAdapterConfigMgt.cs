using System;
using System.IO;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;

namespace HYS.SQLInboundAdapterObjects
{
    public class SQLInAdapterConfigMgt
    {
        public const string _FileName = "SQLInAdapter.xml";
        protected const string XMLHeader = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";

        private static SQLInAdapterConfig _SQLInAdapterConfig = new SQLInAdapterConfig();
        public static SQLInAdapterConfig SQLInAdapterConfig
        {
            get { return _SQLInAdapterConfig; }
            set { _SQLInAdapterConfig = value; }
        }

        public static bool Load(string fileName)
        {
            try
            {
                using (StreamReader sr = File.OpenText(fileName))
                {
                    string strXml = sr.ReadToEnd();
                    _SQLInAdapterConfig = XObjectManager.CreateObject(strXml, typeof(SQLInAdapterConfig)) as SQLInAdapterConfig;
                    return (_SQLInAdapterConfig != null);
                }
            }
            catch (Exception err)
            {
                _lastError = err;
                return false;
            }
        }

        public static bool Save(string fileName)
        {
            try
            {
                if (_SQLInAdapterConfig == null) return false;
                using (StreamWriter sw = File.CreateText(fileName))
                {
                    string strXml = XMLHeader + _SQLInAdapterConfig.ToXMLString();
                    sw.Write(strXml);
                }
                return true;
            }
            catch (Exception err)
            {
                _lastError = err;
                return false;
            }
        }


        #region Exception definition

        private static Exception _lastError;

        public static Exception LastException
        {
            get
            {
                return _lastError;
            }
        }

        public static Exception LastXmlException
        {
            get
            {
                return XObjectManager.LastError;
            }
        }

        #endregion
    }
}
