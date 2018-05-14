using System;
using System.Collections.Generic;
using System.Text;

namespace HYS.Common.Objects.License
{
    [Obsolete("Please use classes in HYS.Common.Objects.License2 namespace instead.", true)]
    public enum DeviceName
    {
        UNKNOWN,
        SQL_IN,
        SQL_OUT,
        GC_SOCKET_IN,
        GC_SOCKET_OUT,
        FILE_IN,
        FILE_OUT,
        DICOM_MPPS_IN,
        DICOM_SSCP_IN,
        DICOM_MWL_OUT,
        RDET_MWL_OUT,
        HL7_XML_IN,
        XML_HL7_OUT,
        XREG_IN,
        HL7_GATEWAY,
    }
}
