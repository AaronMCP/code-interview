using System;
using System.Collections.Generic;
using HYS.Common.Dicom.Net;

namespace HYS.DicomAdapter.Common
{
    public interface IDicomConfigMgt
    {
        SCPConfig GetSCPConfig();
        bool Load();
        bool Save();
    }
}
