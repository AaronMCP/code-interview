using System;
using System.Collections.Generic;
using System.Text;
using HYS.Common.Objects.Device;

namespace HYS.IM.BusinessEntity
{
    public class GCDevice : IDevice
    {
        protected GCDevice()
        {
        }
        public GCDevice(DeviceDir dir, string folder)
        {
            _directory = dir;
            _folderPath = folder;
            _deviceName = _directory.Header.Name;
        }

        public override string ToString()
        {
            //return DeviceID.ToString() + " " + DeviceName;
            return DeviceName;
        }

        protected int _deviceID;
        public virtual int DeviceID
        {
            get { return _deviceID; }
            set { _deviceID = value; }
        }

        protected string _deviceName;
        public virtual string DeviceName
        {
            get { return _deviceName; }
            set { _deviceName = value; }
        }

        protected string _folderPath;
        public virtual string FolderPath
        {
            get { return _folderPath; }
            set { _folderPath = value; }
        }

        protected DeviceDir _directory;
        public virtual DeviceDir Directory
        {
            get { return _directory; }
            set { _directory = value; }
        }
    }
}
