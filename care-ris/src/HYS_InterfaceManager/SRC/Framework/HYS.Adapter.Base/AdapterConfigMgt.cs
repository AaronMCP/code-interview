using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.Common.Xml;
using System.IO;
using HYS.Common.Objects.Logging;

namespace HYS.Adapter.Base
{
    public class AdapterConfigMgt<T> where T : XObject
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

        private bool _hasSaved; // the Adapter.Config.exe may call the Save() method twice when there are several IConfigUI form.
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
                if (_hasSaved) return true;
                using (StreamWriter sw = File.CreateText(FileName))
                {
                    string str = Config.ToXMLString();
                    str = XMLHeader + str;
                    sw.Write(str);

                    _hasSaved = true;
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
