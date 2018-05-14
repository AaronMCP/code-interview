using System;
using System.Drawing;
using System.ComponentModel;
using System.Collections.Generic;
using HYS.Common.Objects.Device;

namespace HYS.Common.Objects.Config
{
    public class AdapterConfigCfg : ConfigBase
    {
        private string _adapterFileName = "";
        [Category("1. Should be modified when composing a device.")]
        [Description("Adapter configuration implementation assembly. Should better be a indirect path.")]
        public string AdapterFileName
        {
            get { return _adapterFileName; }
            set { _adapterFileName = value; }
        }

        private DirectionType _adapterDirection = DirectionType.UNKNOWN;
        [Category("1. Should be modified when composing a device.")]
        [Description("Adapter direction.")]
        public DirectionType AdapterDirection
        {
            get { return _adapterDirection; }
            set { _adapterDirection = value; }
        }

        private string _dataDBConnection = "";
        [Category("3. Will be modified by IM when installing an interface. Don't need to be modified when composing a device.")]
        [Description("OLEDB connection string of GWDataDB")]
        public string DataDBConnection
        {
            get { return _dataDBConnection; }
            set { _dataDBConnection = value; }
        }

        private string _configDBConnection = "";
        [Category("3. Will be modified by IM when installing an interface. Don't need to be modified when composing a device.")]
        [Description("OLEDB connection string of GWConfigDB")]
        public string ConfigDBConnection
        {
            get { return _configDBConnection; }
            set { _configDBConnection = value; }
        }

        private Size _windowSize = new Size(800, 600);
        [Category("2. Should be modified when composing a device. However, better remain its default value.")]
        [Description("Main window size of Adapter.Config.exe. You can drag the main window of Adapter.Config.exe to change this value.")]
        public Size WindowSize
        {
            get { return _windowSize; }
            set
            {
                _windowSize = value;
                if (_windowSize.Width < 800) _windowSize.Width = 800;
                if (_windowSize.Height < 600) _windowSize.Height = 600;
            }
        }

        private bool _warnBeforeCancel;
        [Category("2. Should be modified when composing a device. However, better remain its default value.")]
        [Description("Warn user before he/she click \"Cancel\" button to exit configuration GUI without saving configuration.")]
        public bool WarnBeforeCancel
        {
            get { return _warnBeforeCancel; }
            set { _warnBeforeCancel = value; }
        }
    }
}
