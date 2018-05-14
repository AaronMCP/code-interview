using System;
using System.Collections.Generic;
using HYS.Common.Dicom;
using HYS.Common.Objects.Rule;

namespace HYS.DicomAdapter.Common
{
    public interface IDicomMappingItem
    {
        DPath DPath { get; }
        TranslatingRule Translating { get; }
        GWDataDBField GWDataDBField{ get; }
        bool RedundancyFlag { get; }
        
        bool IsValid();
        IDicomMappingItem Clone();

        void Refresh();
    }
}
