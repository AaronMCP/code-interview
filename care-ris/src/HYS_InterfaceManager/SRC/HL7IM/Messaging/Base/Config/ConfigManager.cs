using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using HYS.Common.Xml;

namespace HYS.IM.Messaging.Base.Config
{
    public class ConfigManager<T> : IConfigManager where T : XObject
    {
        public ConfigManager(string fileName)
        {
            _filename = fileName;

            if (!Path.IsPathRooted(_filename))
            {
                _filename = ConfigHelper.GetFullPath(_filename);        // assembly folder
                //_filename = Path.GetFullPath(_filename);              // process current folder
            }
        }

        public const string XMLHeader = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";

        private string _filename = "config.xml";
        public string FileName
        {
            get { return _filename; }
            set { _filename = value; }
        }

        private T _config;
        public T Config
        {
            get { return _config; }
            set { _config = value; }
        }

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

        public bool Load()
        {
            try
            {
                using (StreamReader sr = File.OpenText(FileName))
                {
                    string xmlstr = sr.ReadToEnd();
                    _config = XObjectManager.CreateObject<T>(xmlstr);
                }

                _lastError = XObjectManager.LastError;
                return (_config != null);
            }
            catch (Exception err)
            {
                _lastError = err;
                return false;
            }
        }
        public bool Save()
        {
            try
            {
                if (_config == null) return false;

                string path = Path.GetDirectoryName(FileName);
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                using (StreamWriter sw = File.CreateText(FileName))
                {
                    string xmlstr = _config.ToXMLString();
                    sw.Write(XMLHeader + xmlstr);
                }

                _lastError = null;
                return true;
            }
            catch (Exception err)
            {
                _lastError = err;
                return false;
            }
        }

        #region IConfigManager Members

        XObject IConfigManager.Config
        {
            get { return this._config as XObject; }
        }

        #endregion
    }
}
