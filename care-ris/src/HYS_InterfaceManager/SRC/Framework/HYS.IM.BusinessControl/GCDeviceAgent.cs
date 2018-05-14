using System;
using System.Collections.Generic;
using System.Text;
using HYS.IM.BusinessEntity;
using HYS.IM.BusinessControl.SystemControl;
using HYS.IM.BusinessControl.DataControl;
using HYS.Common.Objects.Device;

namespace HYS.IM.BusinessControl
{
    public class GCDeviceAgent : GCDevice
    {
        private DeviceRec _deviceRec;
        public DeviceRec DeviceRec
        {
            get { return _deviceRec; }
        }

        public GCDeviceAgent(DeviceRec rec)
        {
            _deviceRec = rec;
            _folderPath = rec.Folder;
        }

        public override int DeviceID
        {
            get { return _deviceRec.ID; }
            set { _deviceRec.ID = value; }
        }

        public override string DeviceName
        {
            get { return _deviceRec.Name; }
            set { _deviceRec.Name = value; }
        }

        public override string FolderPath
        {
            get { return _deviceRec.Folder; }
            set { _deviceRec.Folder = value; }
        }

        public override DeviceDir Directory
        {
            get 
            {
                if (_directory == null)
                {
                    //_directory = DeviceFolder._LoadDeviceDir(_folderPath);
                    GCDevice d = FolderControl.LoadDevice(_folderPath);
                    if (d != null) _directory = d.Directory;
                }
                return _directory;
            }
            set { _directory = value; }
        }
    }
}
