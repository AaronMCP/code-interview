using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using HYS.Common.Xml;

namespace HYS.Common.Objects.Logging
{
    public class LogDataMgt
    {
        protected const string XMLHeader = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";

        private static Log _log = new Log();
        public static Log Log
        {
            get { return _log; }
            set { _log = value; }
        }

        public static bool Load(string fileName)
        {
            try
            {
                string strXml = GetLogInXML(fileName);
                _log = XObjectManager.CreateObject(strXml, typeof(Log)) as Log;
                return (_log != null);

            }
            catch (Exception err)
            {
                _lastError = err;
                return false;
            }
        }

        private static string GetLogInXML(string fileName) {
            StringBuilder sb = new StringBuilder(XMLHeader);

            
            try
            {
                using (StreamReader sr = File.OpenText(fileName))
                {

                }

                return sb.ToString();
            }
            catch (Exception err) {
                _lastError = err;
                return null; 
            }
        }

        public static bool Save(string fileName)
        {
            try
            {
                if (_log == null) return false;
                using (StreamWriter sw = File.CreateText(fileName))
                {
                    string strXml = XMLHeader + _log.ToXMLString();
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
