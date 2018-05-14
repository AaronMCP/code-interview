using System;
using System.IO;
using HYS.Common.Objects.Logging;
using HYS.Common.Xml;

namespace HYS.HL7IM.Manager.Config
{
    internal class ConfigMgt<T> where T : XObject
    {
        private const string XMLHeader = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";

        /// <summary>
        /// Please set a direct path to the FileName.
        /// </summary>
        public string FileName { get; set; }
        public T Config { get; set; }

        private Exception _lastError;
        public Exception LastError
        {
            get { return _lastError; }
        }
        public string LastErrorInfor
        {
            get
            {
                if (LastError == null) return "";
                return LastError.ToString();
            }
        }

        public bool Load(ILogging log)
        {
            try
            {
                using (StreamReader sr = File.OpenText(FileName))
                {
                    string str = sr.ReadToEnd();
                    Config = XObjectManager.CreateObject(str, typeof(T)) as T;
                    return true;
                }
            }
            catch (Exception e)
            {
                log.Write(e);
                _lastError = e;
                return false;
            }
        }
        public bool Save(ILogging log)
        {
            try
            {
                using (StreamWriter sw = File.CreateText(FileName))
                {
                    string str = Config.ToXMLString();
                    str = XMLHeader + str;
                    sw.Write(str);

                    return true;
                }
            }
            catch (Exception e)
            {
                log.Write(e);
                _lastError = e;
                return false;
            }
        }
    }
}
