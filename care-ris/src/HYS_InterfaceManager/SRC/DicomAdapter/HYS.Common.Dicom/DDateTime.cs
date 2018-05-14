using System;
using System.Collections.Generic;
using System.Text;

namespace HYS.Common.Dicom
{
    public class DDateTime2
    {
        private DVR vr;
        private string endString;
        private string beginString;
        private string dicomString;
        private DDateTime2(DVR dvr, string dcmString)
        {
            vr = dvr;
            dicomString = dcmString;
            Initialize();
        }
        private void Initialize()
        {
            string[] strList = dicomString.Split('-');
            if (strList.Length == 2)
            {
                beginString = strList[0];
                endString = strList[1];

                if (beginString.Length < 1) beginString = null;
                if (endString.Length < 1) endString = null;
            }
        }

        public DDateTimeType Type
        {
            get
            {
                if (endString == null && beginString == null) return DDateTimeType.SINGLE;
                if (endString == null && beginString != null) return DDateTimeType.START_ONLY;
                if (endString != null && beginString == null) return DDateTimeType.END_ONLY;
                return DDateTimeType.RANGE;
            }
        }
        public object GetDateTime()
        {
            return GetDT(vr, dicomString);
        }
        public object GetStartDateTime()
        {
            return GetDT(vr, beginString);
        }
        public object GetEndDateTime()
        {
            return GetDT(vr, endString);
        }

        private static object GetDT(DVR vr, string str)
        {
            System.DateTime dt;
            string strDicom = str;
            if (vr == DVR.DA)
            {
                if (System.DateTime.TryParseExact(strDicom,
                    new string[] { "yyyyMMdd", "yyyy.MM.dd" }, null,
                    System.Globalization.DateTimeStyles.None, out dt)) return dt;
            }
            else if (vr == DVR.TM)
            {
                // ignore millisecond ...
                int dotIndex = strDicom.IndexOf('.');
                if (dotIndex >= 0) strDicom = strDicom.Substring(0, dotIndex);
                if (System.DateTime.TryParseExact(strDicom,
                    new string[] { "HHmmss", "HHmm", "HH", "HH:mm:ss" }, null,
                    System.Globalization.DateTimeStyles.None, out dt)) return dt;
            }
            if (vr == DVR.DT)
            {
                // ignore millisecond ...
                int dotIndex = strDicom.IndexOf('.');
                if (dotIndex >= 0) strDicom = strDicom.Substring(0, dotIndex);
                if (System.DateTime.TryParseExact(strDicom,
                    new string[] { "yyyyMMddHHmmss" }, null,
                    System.Globalization.DateTimeStyles.None, out dt)) return dt;
            }
            return null;
        }
        public static DDateTime2 FromDateTime(DVR vr, string dicomString)
        {
            if (dicomString == null || dicomString.Length < 1) return null;
            return new DDateTime2(vr, dicomString);
        }
    }

}
