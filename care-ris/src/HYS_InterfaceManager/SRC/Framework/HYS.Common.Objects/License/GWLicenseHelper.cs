using System;
using System.Text;
using System.Collections.Generic;
using HYS.Common.Objects.Device;

namespace HYS.Common.Objects.License
{
    [Obsolete("Please use classes in HYS.Common.Objects.License2 namespace instead.", true)]
    public class GWLicenseHelper
    {
        public const int BytesLength = 128;
        public const int CreateDateOffset = 4;
        public const int CreateDateLength = 8;
        public const int DevicesOffset = 28;

        public const uint SentinelDevID = 0x994AD9FD; 
        public const string DateFormat = "yyyyMMdd";

        public static Encoding Encoder = Encoding.UTF8;

        public static GWLicense GetDefaultLicense()
        {
            GWLicense lic = new GWLicense();

            lic.LicenseLevels.Add(new DeviceLicenseLevel(0x0, 0, 0, "Disable This Device"));
            lic.LicenseLevels.Add(new DeviceLicenseLevel(0x1, 1, -1, "Interface Count: 1, Day Count: (infinite)"));
            lic.LicenseLevels.Add(new DeviceLicenseLevel(0x2, 2, -1, "Interface Count: 2, Day Count: (infinite)"));
            lic.LicenseLevels.Add(new DeviceLicenseLevel(0x3, 3, -1, "Interface Count: 3, Day Count: (infinite)"));
            lic.LicenseLevels.Add(new DeviceLicenseLevel(0x4, 4, -1, "Interface Count: 4, Day Count: (infinite)"));
            lic.LicenseLevels.Add(new DeviceLicenseLevel(0x5, 5, -1, "Interface Count: 5, Day Count: (infinite)"));
            lic.LicenseLevels.Add(new DeviceLicenseLevel(0x6, 6, -1, "Interface Count: 6, Day Count: (infinite)"));
            lic.LicenseLevels.Add(new DeviceLicenseLevel(0x7, 7, -1, "Interface Count: 7, Day Count: (infinite)"));
            lic.LicenseLevels.Add(new DeviceLicenseLevel(0x8, 8, -1, "Interface Count: 8, Day Count: (infinite)"));
            lic.LicenseLevels.Add(new DeviceLicenseLevel(0x9, 9, -1, "Interface Count: 9, Day Count: (infinite)"));
            lic.LicenseLevels.Add(new DeviceLicenseLevel(0xA, 10, -1, "Interface Count: 10, Day Count: (infinite)"));
            lic.LicenseLevels.Add(new DeviceLicenseLevel(0xB, 20, -1, "Interface Count: 20, Day Count: (infinite)"));
            lic.LicenseLevels.Add(new DeviceLicenseLevel(0xC, 100, -1, "Interface Count: 100, Day Count: (infinite)"));
            lic.LicenseLevels.Add(new DeviceLicenseLevel(0xD, 1, 30, "Interface Count: 1, Day Count: 30"));
            lic.LicenseLevels.Add(new DeviceLicenseLevel(0xE, 10, 30, "Interface Count: 10, Day Count: 30"));
            lic.LicenseLevels.Add(new DeviceLicenseLevel(0xF, -1, -1, "Interface Count: (infinite), Day Count: (infinite)"));

            lic.Devices.Add(new DeviceLicense(DeviceName.SQL_IN, DeviceType.SQL, DirectionType.INBOUND, 0xF0));
            lic.Devices.Add(new DeviceLicense(DeviceName.SQL_OUT, DeviceType.SQL, DirectionType.OUTBOUND, 0xF0));
            lic.Devices.Add(new DeviceLicense(DeviceName.GC_SOCKET_IN, DeviceType.SOCKET, DirectionType.INBOUND, 0xF0));
            lic.Devices.Add(new DeviceLicense(DeviceName.GC_SOCKET_OUT, DeviceType.SOCKET, DirectionType.OUTBOUND, 0xF0));
            lic.Devices.Add(new DeviceLicense(DeviceName.HL7_XML_IN, DeviceType.XML, DirectionType.INBOUND, 0xF0));
            lic.Devices.Add(new DeviceLicense(DeviceName.XML_HL7_OUT, DeviceType.XML, DirectionType.OUTBOUND, 0xF0));
            lic.Devices.Add(new DeviceLicense(DeviceName.FILE_IN, DeviceType.FILE, DirectionType.INBOUND, 0xF0));
            lic.Devices.Add(new DeviceLicense(DeviceName.FILE_OUT, DeviceType.FILE, DirectionType.OUTBOUND, 0xF0));
            lic.Devices.Add(new DeviceLicense(DeviceName.DICOM_MPPS_IN, DeviceType.DICOM, DirectionType.INBOUND, 0xF0));
            lic.Devices.Add(new DeviceLicense(DeviceName.DICOM_MWL_OUT, DeviceType.DICOM, DirectionType.OUTBOUND, 0xF0));
            lic.Devices.Add(new DeviceLicense(DeviceName.RDET_MWL_OUT, DeviceType.RDET, DirectionType.OUTBOUND, 0xF0));
            lic.Devices.Add(new DeviceLicense(DeviceName.XREG_IN, DeviceType.XReg, DirectionType.INBOUND, 0x10));
            lic.Devices.Add(new DeviceLicense(DeviceName.HL7_GATEWAY, DeviceType.HL7, DirectionType.UNKNOWN, 0x10));
            lic.Devices.Add(new DeviceLicense(DeviceName.DICOM_SSCP_IN, DeviceType.DICOM, DirectionType.INBOUND, 0xF0));

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
                if ((offset) >= BytesLength) break;
                bArray[offset] = dl.GetValue();
                offset++;
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
                if ((offset) >= BytesLength) break;
                dl.SetValue(bArray[offset]);
                offset++;
            }

            return true;
        }
    }
}
