using System;
using System.IO;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.Common.Dicom.Net;
using HYS.Common.Objects.Rule;
using HYS.DicomAdapter.Common;

namespace HYS.DicomAdapter.StorageServer.Objects
{
    public class StorageServerConfigMgt : IDicomConfigMgt
    {
        public string FileName = "StorageServer.xml";
        private const string XMLHeader = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
        public StorageServerConfig Config = new StorageServerConfig();
        private void InitailizeParameter()
        {
            if (Config == null) return;
        }
        public SCPConfig GetSCPConfig()
        {
            return Config.SCPConfig;
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

        private bool _hasSaved;
        public bool Load()
        {
            try
            {
                using (StreamReader sr = File.OpenText(FileName))
                {
                    string str = sr.ReadToEnd();
                    Config = XObjectManager.CreateObject(str, typeof(StorageServerConfig)) as StorageServerConfig;

                    //DicomMappingHelper.SetFixValue<MWLQueryCriteriaItem, MWLQueryResultItem>(Config.Rule);
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
                Config.UpdatePrivateTagList();
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
                Program.Log.Write(e);
                _lastError = e;
                return false;
            }
        }
    }
}
