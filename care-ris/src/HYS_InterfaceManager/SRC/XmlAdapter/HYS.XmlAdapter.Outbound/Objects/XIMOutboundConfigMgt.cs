using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.XmlAdapter.Common.Objects;

namespace HYS.XmlAdapter.Outbound.Objects
{
    public class XIMOutboundConfigMgt
    {
        public string FileName = "XIMOutbound.xml";
        private const string XMLHeader = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
        public XIMOutboundConfig Config = new XIMOutboundConfig();

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
                    Config = XObjectManager.CreateObject(str, typeof(XIMOutboundConfig)) as XIMOutboundConfig;
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

                XIMMappingHelper.GenerateTargetFieldName(Config.Messages);

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
