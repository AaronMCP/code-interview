using System;
using System.Collections.Generic;
using System.Text;

namespace HYS.Common.Objects.Device
{
    public enum DeviceType
    {
        UNKNOWN = 0,
        SQL,
        SOCKET,
        FILE,
        XML,
        DICOM,
        RDET,
        XReg,
        HL7,
        DAP,
    }
}
