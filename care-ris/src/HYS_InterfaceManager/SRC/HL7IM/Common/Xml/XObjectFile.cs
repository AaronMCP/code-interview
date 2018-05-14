using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace HYS.HL7IM.Common.Xml
{
    public class XObjectFile<T> where T : XObject
    {
        public XObjectFile(string fileName)
        {
            _filename = fileName;

            //if (!Path.IsPathRooted(_filename))
            //{
            //    _filename = ConfigHelper.GetFullPath(_filename);        // assembly folder
            //    //_filename = Path.GetFullPath(_filename);              // process current folder
            //}
        }

        public const string XMLHeader = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";

        private string _filename = "xobject.xml";
        public string FileName
        {
            get { return _filename; }
            set { _filename = value; }
        }

        private T _content;
        public T Content
        {
            get { return _content; }
            set { _content = value; }
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
                    _content = XObjectManager.CreateObject<T>(xmlstr);
                }

                _lastError = null;
                return (_content != null);
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
                if (_content == null) return false;

                using (StreamWriter sw = File.CreateText(FileName))
                {
                    string xmlstr = _content.ToXMLString();
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
    }
}
