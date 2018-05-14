using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using HYS.Common.Xml;

namespace HYS.Common.Objects.License
{
    [Obsolete("Please use classes in HYS.Common.Objects.License2 namespace instead.", true)]
    public class GWLicenseManager
    {
        public const string XMLHeader = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";

        protected string _filename = "HYS.LicensePolicy.dat";
        public string FileName
        {
            get { return _filename; }
            set { _filename = value; }
        }

        protected GWLicense _license;
        public GWLicense License
        {
            get { return _license; }
            set { _license = value; }
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
                    //string xmlstr = sr.ReadToEnd();
                    DataCrypto dc = new DataCrypto();
                    string xmlstr = dc.Decrypto(sr.ReadToEnd());
                    _license = XObjectManager.CreateObject(xmlstr, typeof(GWLicense)) as GWLicense;
                }

                _lastError = null;
                return (_license != null);
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
                if (_license == null) return false;

                using (StreamWriter sw = File.CreateText(FileName))
                {
                    DataCrypto dc = new DataCrypto();
                    string xmlstr = _license.ToXMLString();
                    sw.Write(dc.Encrypto(XMLHeader + xmlstr));
                    //sw.Write(XMLHeader + xmlstr);
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
