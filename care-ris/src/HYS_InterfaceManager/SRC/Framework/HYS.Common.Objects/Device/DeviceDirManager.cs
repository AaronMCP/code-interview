using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using HYS.Common.Xml;

namespace HYS.Common.Objects.Device
{
    public class DeviceDirManager
    {
        public const string IndexFileName = "DeviceDir";
        public const string XMLHeader = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";

        private string _filename;
        public string FileName
        {
            get { return _filename; }
            set { _filename = value; }
        }

        private DeviceDir _deviceDirInfor = new DeviceDir();
        public DeviceDir DeviceDirInfor
        {
            get { return _deviceDirInfor; }
            set { _deviceDirInfor = value; }
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

        public DeviceDirManager()
        {
        }
        public DeviceDirManager(string deviceDirFileName)
        {
            FileName = deviceDirFileName;
        }

        public bool LoadDeviceDir()
        {
            try
            {
                using (StreamReader sr = File.OpenText(FileName))
                {
                    string xmlstr = sr.ReadToEnd();
                    DeviceDirInfor = XObjectManager.CreateObject(xmlstr, typeof(DeviceDir)) as DeviceDir;
                }

                _lastError = null;
                return (DeviceDirInfor != null);
            }
            catch (Exception err)
            {
                _lastError = err;
                return false;
            }
        }
        public bool SaveDeviceDir()
        {
            try
            {
                if (DeviceDirInfor == null) return false;

                using (StreamWriter sw = File.CreateText(FileName))
                {
                    string xmlstr = DeviceDirInfor.ToXMLString();
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
