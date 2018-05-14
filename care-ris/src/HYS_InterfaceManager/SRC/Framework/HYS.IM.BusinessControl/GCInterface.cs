using System;
using System.Collections.Generic;
using System.Text;
using HYS.IM.BusinessEntity;
using HYS.IM.BusinessControl.DataControl;
using HYS.IM.BusinessControl.SystemControl;
using HYS.Common.Objects.Device;
using HYS.Common.DataAccess;
using HYS.Adapter.Base;

namespace HYS.IM.BusinessControl
{
    public class GCInterface : IDevice
    {
        internal GCInterface()
        {
            _interfaceRec = new InterfaceRec();
        }
        public GCInterface( InterfaceRec rec )
        {
            _interfaceRec = rec;
        }

        public override string ToString()
        {
            //return InterfaceID.ToString() + " " + InterfaceName;
            return InterfaceName;
        }

        private InterfaceRec _interfaceRec;
        public InterfaceRec InterfaceRec
        {
            get { return _interfaceRec; }
            set { _interfaceRec = value; }
        }

        protected DeviceDir _directory;
        public virtual DeviceDir Directory
        {
            get 
            {
                if (_directory == null)
                {
                    GCDevice d = FolderControl.LoadDevice(FolderPath);
                    if (d != null) _directory = d.Directory;
                }
                return _directory;
            }
            set { _directory = value; }
        }

        public int InterfaceID
        {
            get { return _interfaceRec.ID; }
            set { _interfaceRec.ID = value; }
        }
        public string InterfaceName
        {
            get { return _interfaceRec.Name; }
            set { _interfaceRec.Name = value; }
        }
        public string FolderPath
        {
            get { return _interfaceRec.Folder; }
            set { _interfaceRec.Folder = value; }
        }

        protected GCDevice _device;
        public virtual GCDevice Device
        {
            get 
            {
                if (_device == null)
                {
                    DObjectManager mgt = _interfaceRec.GetDataManager();
                    if( mgt != null )
                    {
                        DeviceRecManager recMgt = new DeviceRecManager(mgt.DataBase);
                        _device = recMgt.GetDeviceByID(_interfaceRec.DeviceID);
                    }
                }
                return _device;
            }
            set { _device = value; }
        }

        private AdapterStatus _status;
        public AdapterStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }
    }
}
