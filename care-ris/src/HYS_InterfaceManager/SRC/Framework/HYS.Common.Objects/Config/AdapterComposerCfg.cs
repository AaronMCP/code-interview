using System;
using System.Collections.Generic;
using HYS.Common.Objects.Device;

namespace HYS.Common.Objects.Config
{
    public class AdapterComposerCfg : ConfigBase
    {
        private string _deviceDirFileName = DeviceDirManager.IndexFileName;
        public string DeviceDirFileName
        {
            get { return _deviceDirFileName; }
            set { _deviceDirFileName = value; }
        }
        
        private string _configConfigFileName = ConfigHelper.ConfigDefaultFileName;
        public string ConfigConfigFileName
        {
            get { return _configConfigFileName; }
            set { _configConfigFileName = value; }
        }

        private string _serviceConfigFileName = ConfigHelper.ServiceDefaultFileName;
        public string ServiceConfigFileName
        {
            get { return _serviceConfigFileName; }
            set { _serviceConfigFileName = value; }
        }

        private string _monitorConfigFileName = ConfigHelper.MonitorDefaultFileName;
        public string MonitorConfigFileName
        {
            get { return _monitorConfigFileName; }
            set { _monitorConfigFileName = value; }
        }
    }
}
