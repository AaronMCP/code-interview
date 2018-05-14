using System;
using System.Collections.Generic;
using System.Text;

namespace HYS.Common.Objects.License2
{
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
        HL7_SENDER,
        HL7_RECEIVER,
        DICOM_QR_SCU,
        DICOM_GPWL_SCP,
        DICOM_GPWL_SCU,
        DICOM_GPPS_SCP,
        DICOM_GPPS_SCU,
        DICOM_MWL_IN,
        DICOM_MPPS_OUT,
        DAP_INOUT
    }
}
