using System;
using System.Drawing;
using System.ComponentModel;
using System.Collections.Generic;
using HYS.Common.Objects.Device;

namespace HYS.Common.Objects.Config
{
    public class AdapterMonitorCfg : ConfigBase
    {
        private string _dataDBConnection = "";
        [Category("1. Will be modified by IM when installing an interface. Don't need to be modified when composing a device.")]
        [Description("OLEDB connection string of GWDataDB")]
        public string DataDBConnection
        {
            get { return _dataDBConnection; }
            set { _dataDBConnection = value; }
        }

        private string _configDBConnection = "";
        [Category("1. Will be modified by IM when installing an interface. Don't need to be modified when composing a device.")]
        [Description("OLEDB connection string of GWConfigDB")]
        public string ConfigDBConnection
        {
            get { return _configDBConnection; }
            set { _configDBConnection = value; }
        }
    }
}
