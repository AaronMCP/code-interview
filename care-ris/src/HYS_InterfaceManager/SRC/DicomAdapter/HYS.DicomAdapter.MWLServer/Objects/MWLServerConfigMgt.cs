using System;
using System.IO;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.Common.Dicom.Net;
using HYS.Common.Objects.Rule;
using HYS.DicomAdapter.Common;

namespace HYS.DicomAdapter.MWLServer.Objects
{
    public class MWLServerConfigMgt : IDicomConfigMgt
    {
        public string FileName = "MWLServer.xml";
        private const string XMLHeader = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
        public MWLServerConfig Config = new MWLServerConfig();
        private void InitailizeParameter()
        {
            if (Config == null) return;
            Config.Rule.CheckProcessFlag = false;
            Config.Rule.QueryCriteria.Type = QueryCriteriaRuleType.DataSet;
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
                    Config = XObjectManager.CreateObject(str, typeof(MWLServerConfig)) as MWLServerConfig;
                    DicomMappingHelper.SetFixValue<MWLQueryCriteriaItem, MWLQueryResultItem>(Config.Rule);
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
