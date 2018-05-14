using System;
using System.Collections.Generic;
using System.Text;

namespace HYS.Common.Objects.Device
{
    public enum DeviceFileType
    {
        Other,
        Installer,          // called by IM
        ServiceAssembly,    // called by IM
        ServiceConfig,      // modified by IM
        ConfigAssembly,     // called by IM
        ConfigConfig,       // modified by IM
        MonitorAssembly,    // called by IM
        MonitorConfig,
        InstallScript,
        UninstallScript,
        StartScript,
        StopScript,
        OtherScript,
        DBInstallScript,    // called by IM
        DBUnintallScript,   // called by IM
    }
}
