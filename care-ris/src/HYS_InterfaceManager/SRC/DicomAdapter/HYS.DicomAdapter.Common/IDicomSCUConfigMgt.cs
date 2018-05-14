using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.Common.Dicom.Net;

namespace HYS.DicomAdapter.Common
{
    public interface IDicomSCUConfigMgt
    {
        Modality GetSCPConfig();
        SCUConfig GetSCUConfig();
        bool Load();
        bool Save();

        bool EnableTimerSetting { get; }
        int InvokeInterval { get; set; }
        string CharacterSetName { get; set; }
        bool SendCharacterSetTag { get; set; }
    }
}
