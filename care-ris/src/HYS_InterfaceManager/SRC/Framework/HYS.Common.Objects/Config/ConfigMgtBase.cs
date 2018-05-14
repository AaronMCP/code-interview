using System;
using System.IO;
using System.Collections.Generic;
using HYS.Common.Xml;

namespace HYS.Common.Objects.Config
{
    public abstract class ConfigMgtBase
    {
        public const string XMLHeader = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";

        // these three property should be initialize in constructure of class that inherate ConfigMgtBase
        protected string _filename;
        public string FileName
        {
            get { return _filename; }
            set { _filename = value; }
        }

        protected Type _configType;
        public Type ConfigType
        {
            get { return _configType; }
            set { _configType = value; }
        }

        protected ConfigBase _config;
        public ConfigBase Config
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

        public virtual bool Load()
        {
            try
            {
                using (StreamReader sr = File.OpenText(FileName))
                {
                    string xmlstr = sr.ReadToEnd();
                    _config = XObjectManager.CreateObject(xmlstr, _configType) as ConfigBase;
                }

                _lastError = null;
                return (_config != null);
            }
            catch (Exception err)
            {
                _lastError = err;
                return false;
            }
        }
        public virtual bool Save()
        {
            try
            {
                if (_config == null) return false;

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

        public bool SetValue(string propertyName, string value)
        {
            if (_config == null) return false;
            return _config.SetValue(propertyName, value);
        }
        public object GetValue(string propertyName)
        {
            if (_config == null) return false;
            return _config.GetValue(propertyName);
        }
    }
}
