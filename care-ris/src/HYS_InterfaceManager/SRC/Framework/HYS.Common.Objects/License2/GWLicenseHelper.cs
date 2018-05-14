using System;
using System.Text;
using System.Collections.Generic;
using HYS.Common.Objects.Device;

namespace HYS.Common.Objects.License2
{
    public class GWLicenseHelper
    {
        public const int BytesLength = 128;
        public const int CreateDateOffset = 4;
        public const int CreateDateLength = 8;
        public const int DevicesOffset = 28;

        public const uint SentinelDevID = 0x994AD9FD;
        public const string DateFormat = "yyyyMMdd";

        public static Encoding Encoder = Encoding.UTF8;

        public static GWLicense GetFullLicense()
        {
            GWLicense lic = new GWLicense();

            lic.Devices.Add(new DeviceLicense(DeviceName.SQL_IN, DeviceType.SQL, DirectionType.INBOUND, 0xFF00));
            lic.Devices.Add(new DeviceLicense(DeviceName.SQL_OUT, DeviceType.SQL, DirectionType.OUTBOUND, 0xFF00));
            lic.Devices.Add(new DeviceLicense(DeviceName.GC_SOCKET_IN, DeviceType.SOCKET, DirectionType.INBOUND, 0xFF00));
            lic.Devices.Add(new DeviceLicense(DeviceName.GC_SOCKET_OUT, DeviceType.SOCKET, DirectionType.OUTBOUND, 0xFF00));
            lic.Devices.Add(new DeviceLicense(DeviceName.HL7_XML_IN, DeviceType.XML, DirectionType.INBOUND, 0xFF00));
            lic.Devices.Add(new DeviceLicense(DeviceName.XML_HL7_OUT, DeviceType.XML, DirectionType.OUTBOUND, 0xFF00));
            lic.Devices.Add(new DeviceLicense(DeviceName.FILE_IN, DeviceType.FILE, DirectionType.INBOUND, 0xFF00));
            lic.Devices.Add(new DeviceLicense(DeviceName.FILE_OUT, DeviceType.FILE, DirectionType.OUTBOUND, 0xFF00));
            //lic.Devices.Add(new DeviceLicense(DeviceName.DICOM_MPPS_IN, DeviceType.DICOM, DirectionType.INBOUND, 0xFF00));
            lic.Devices.Add(new DeviceLicense(DeviceName.DICOM_MWL_OUT, DeviceType.DICOM, DirectionType.OUTBOUND, 0xFF00));
            lic.Devices.Add(new DeviceLicense(DeviceName.RDET_MWL_OUT, DeviceType.RDET, DirectionType.OUTBOUND, 0xFF00));
            lic.Devices.Add(new DeviceLicense(DeviceName.XREG_IN, DeviceType.XReg, DirectionType.INBOUND, 0x0100, true));
            lic.Devices.Add(new DeviceLicense(DeviceName.HL7_SENDER, DeviceType.HL7, DirectionType.OUTBOUND, 0xFF00));
            lic.Devices.Add(new DeviceLicense(DeviceName.DICOM_SSCP_IN, DeviceType.DICOM, DirectionType.INBOUND, 0xFF00));
            lic.Devices.Add(new DeviceLicense(DeviceName.HL7_RECEIVER, DeviceType.HL7, DirectionType.INBOUND, 0xFF00));

            //lic.Devices.Add(new DeviceLicense(DeviceName.DICOM_QR_SCU, DeviceType.DICOM, DirectionType.BIDIRECTIONAL, 0xFF00));
            //lic.Devices.Add(new DeviceLicense(DeviceName.DICOM_GPWL_SCP, DeviceType.DICOM, DirectionType.BIDIRECTIONAL, 0xFF00));
            //lic.Devices.Add(new DeviceLicense(DeviceName.DICOM_GPWL_SCU, DeviceType.DICOM, DirectionType.BIDIRECTIONAL, 0xFF00));
            //lic.Devices.Add(new DeviceLicense(DeviceName.DICOM_GPPS_SCP, DeviceType.DICOM, DirectionType.BIDIRECTIONAL, 0xFF00));
            //lic.Devices.Add(new DeviceLicense(DeviceName.DICOM_GPPS_SCU, DeviceType.DICOM, DirectionType.BIDIRECTIONAL, 0xFF00));
            //lic.Devices.Add(new DeviceLicense(DeviceName.DICOM_MWL_IN, DeviceType.DICOM, DirectionType.INBOUND, 0xFF00));
            //lic.Devices.Add(new DeviceLicense(DeviceName.DICOM_MPPS_OUT, DeviceType.DICOM, DirectionType.OUTBOUND, 0xFF00));
            //lic.Devices.Add(new DeviceLicense(DeviceName.DAP_INOUT, DeviceType.DAP, DirectionType.BIDIRECTIONAL, 0xFF00));

            return lic;
        }

        public static GWLicense GetDefaultLicense()
        {
            GWLicense lic = new GWLicense();

            lic.Devices.Add(new DeviceLicense(DeviceName.SQL_IN, DeviceType.SQL, DirectionType.INBOUND, 0xFF00));
            lic.Devices.Add(new DeviceLicense(DeviceName.SQL_OUT, DeviceType.SQL, DirectionType.OUTBOUND, 0x0300));
            lic.Devices.Add(new DeviceLicense(DeviceName.GC_SOCKET_IN, DeviceType.SOCKET, DirectionType.INBOUND, 0x0300));
            lic.Devices.Add(new DeviceLicense(DeviceName.GC_SOCKET_OUT, DeviceType.SOCKET, DirectionType.OUTBOUND, 0x0300));
            lic.Devices.Add(new DeviceLicense(DeviceName.HL7_XML_IN, DeviceType.XML, DirectionType.INBOUND, 0x0300));
            lic.Devices.Add(new DeviceLicense(DeviceName.XML_HL7_OUT, DeviceType.XML, DirectionType.OUTBOUND, 0x0300));
            lic.Devices.Add(new DeviceLicense(DeviceName.FILE_IN, DeviceType.FILE, DirectionType.INBOUND, 0x0300));
            lic.Devices.Add(new DeviceLicense(DeviceName.FILE_OUT, DeviceType.FILE, DirectionType.OUTBOUND, 0x0300));
            //lic.Devices.Add(new DeviceLicense(DeviceName.DICOM_MPPS_IN, DeviceType.DICOM, DirectionType.INBOUND, 0x0000));
            lic.Devices.Add(new DeviceLicense(DeviceName.DICOM_MWL_OUT, DeviceType.DICOM, DirectionType.OUTBOUND, 0x0300));
            lic.Devices.Add(new DeviceLicense(DeviceName.RDET_MWL_OUT, DeviceType.RDET, DirectionType.OUTBOUND, 0x0300));
            lic.Devices.Add(new DeviceLicense(DeviceName.XREG_IN, DeviceType.XReg, DirectionType.INBOUND, 0x0300, true));
            lic.Devices.Add(new DeviceLicense(DeviceName.HL7_SENDER, DeviceType.HL7, DirectionType.OUTBOUND, 0x0300));
            lic.Devices.Add(new DeviceLicense(DeviceName.DICOM_SSCP_IN, DeviceType.DICOM, DirectionType.INBOUND, 0xFF00));
            lic.Devices.Add(new DeviceLicense(DeviceName.HL7_RECEIVER, DeviceType.HL7, DirectionType.INBOUND, 0x0300));

            //lic.Devices.Add(new DeviceLicense(DeviceName.DICOM_QR_SCU, DeviceType.DICOM, DirectionType.BIDIRECTIONAL, 0x0000));
            //lic.Devices.Add(new DeviceLicense(DeviceName.DICOM_GPWL_SCP, DeviceType.DICOM, DirectionType.BIDIRECTIONAL, 0x0000));
            //lic.Devices.Add(new DeviceLicense(DeviceName.DICOM_GPWL_SCU, DeviceType.DICOM, DirectionType.BIDIRECTIONAL, 0x0000));
            //lic.Devices.Add(new DeviceLicense(DeviceName.DICOM_GPPS_SCP, DeviceType.DICOM, DirectionType.BIDIRECTIONAL, 0x0000));
            //lic.Devices.Add(new DeviceLicense(DeviceName.DICOM_GPPS_SCU, DeviceType.DICOM, DirectionType.BIDIRECTIONAL, 0x0000));
            //lic.Devices.Add(new DeviceLicense(DeviceName.DICOM_MWL_IN, DeviceType.DICOM, DirectionType.INBOUND, 0x0000));
            //lic.Devices.Add(new DeviceLicense(DeviceName.DICOM_MPPS_OUT, DeviceType.DICOM, DirectionType.OUTBOUND, 0x0000));
            //lic.Devices.Add(new DeviceLicense(DeviceName.DAP_INOUT, DeviceType.DAP, DirectionType.BIDIRECTIONAL, 0x0000));

            return lic;
        }

        public static void SetTimeStamp(GWLicense lic)
        {
            if (lic != null) lic.Header.CreateDate = DateTime.Now;
        }

        public static byte[] GetBytes(GWLicense lic)
        {
            if (lic == null) return null;

            int length;
            byte[] bTemp;
            byte[] bArray = new byte[BytesLength];

            bTemp = Encoder.GetBytes(lic.Header.Title);
            length = bTemp.Length; if (length > CreateDateOffset) length = CreateDateOffset;
            Array.Copy(bTemp, 0, bArray, 0, length);

            bTemp = Encoder.GetBytes(lic.Header.CreateDate.ToString(DateFormat));
            length = bTemp.Length; if (length > CreateDateLength) length = CreateDateLength;
            Array.Copy(bTemp, 0, bArray, CreateDateOffset, length);

            int offset = DevicesOffset;
            foreach (DeviceLicense dl in lic.Devices)
            {
                if ((offset + 1) >= BytesLength) break;
                
                byte[] bList = dl.GetValue();
                if (bList == null || bList.Length < 1) bList = new byte[] { 0, 0 };

                bArray[offset++] = bList[0];
                bArray[offset++] = bList[1];
            }

            return bArray;
        }

        public static bool LoadBytes(GWLicense lic, byte[] bArray)
        {
            if (lic == null || bArray == null || bArray.Length != BytesLength) return false;

            byte[] bDate = new byte[CreateDateLength];
            Array.Copy(bArray, CreateDateOffset, bDate, 0, CreateDateLength);

            string strDate = Encoder.GetString(bDate);
            lic.Header.CreateDate = DateTime.ParseExact(strDate, DateFormat, null);

            int offset = DevicesOffset;
            foreach (DeviceLicense dl in lic.Devices)
            {
                if ((offset + 1) >= BytesLength) break;
                byte[] bList = new byte[] { bArray[offset++], bArray[offset++] };
                dl.SetValue(bList);
            }

            return true;
        }

        public const string LightMWLOutDeviceName = "LIGHT_MWL_OUT";
        public static DeviceLicense LightMWLOutDeviceLicense
            = new DeviceLicense(DeviceName.DICOM_MWL_OUT, DeviceType.DICOM, DirectionType.OUTBOUND, 0xFF00);
    }
}
