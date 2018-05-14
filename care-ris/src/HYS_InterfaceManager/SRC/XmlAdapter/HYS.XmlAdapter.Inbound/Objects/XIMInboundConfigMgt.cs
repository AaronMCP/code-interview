using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.XmlAdapter.Common.Objects;

namespace HYS.XmlAdapter.Inbound.Objects
{
    public class XIMInboundConfigMgt
    {
        public string FileName = "XIMInbound.xml";
        private const string XMLHeader = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
        public XIMInboundConfig Config = new XIMInboundConfig();

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

        private bool _hasSaved;
        public bool Load()
        {
            try
            {
                using (StreamReader sr = File.OpenText(FileName))
                {
                    string str = sr.ReadToEnd();
                    Config = XObjectManager.CreateObject(str, typeof(XIMInboundConfig)) as XIMInboundConfig;
                    return true;
                }
            }
            catch (Exception e)
            {
                Program.Log.Write(e);
                _lastError = e;
                return false;
            }
        }
        public bool Save()
        {
            try
            {
                if (_hasSaved) return true;

                XIMMappingHelper.GenerateSourceFieldName(Config.Messages);
                
                using (StreamWriter sw = File.CreateText(FileName))
                {
                    string str = Config.ToXMLString();
                    str = XMLHeader + str;
                    sw.Write(str);
                }

                XIMMappingHelper.ClearMapping(Config.Messages);
                XIMMappingHelper.SaveXSLFiles(Config.Messages, Config.SocketConfig);

                _hasSaved = true;
                return true;
            }
            catch (Exception e)
            {
                Program.Log.Write(e);
                _lastError = e;
                return false;
            }
        }
    }
}
