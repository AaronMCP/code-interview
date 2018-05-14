using System;
using System.Text;
using System.Collections.Generic;
using System.Threading;
using System.Management;

namespace Hys.CareRIS.Application.Services.ServiceImpl
{
    public class GenerateStudyInstance
    {
        private static string _macAddress;
        private static string GetMACAddress()
        {
            if (_macAddress == null || _macAddress.Length < 1)
            {
                _macAddress = _GetMACAddress();
            }
            return _macAddress;
        }
        private static string _GetMACAddress()
        {
            try
            {
                ManagementClass mgt = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection objCol = mgt.GetInstances();
                foreach (ManagementObject obj in objCol)
                {
                    bool res = (bool)obj["IPEnabled"];
                    if (res) return obj["MacAddress"].ToString();
                }
                return "";
            }
            catch (Exception err)
            {

                return "";
            }

        }

        private static Int64 GetMACAddressNumber()
        {
            string mac = GetMACAddress();
            if (mac == null || mac.Length < 1) return 0;

            try
            {
                mac = mac.Replace(":", "").Trim();
                Int64 num = Int64.Parse(mac, System.Globalization.NumberStyles.AllowHexSpecifier);
                return num;
            }
            catch (Exception err)
            {

                return 0;
            }
        }

        private static int number = 0;
        //private static int number = int.MaxValue - 1;                                 // for testing
        private static int GetIncreasingNumber()
        {
            Interlocked.Increment(ref number);
            if (number < 0) number = 0;
            return number;
        }

        private static string GUIDRoot = "1.2.840.113564";
        /// <summary>
        /// ProcessID should be a zero based Int32.ToString(). It is no more than 10 character.
        /// </summary>
        /// <param name="ProcessID"></param>
        /// <returns></returns>
        public static string GetDicomGUID(string ProcessID)
        {
            string num = GetIncreasingNumber().ToString();
            string mac = GetMACAddressNumber().ToString();
            //string dtm = System.DateTime.Now.ToString("yyyyMMddhhmmssfffffff");       // 21 character
            string dtm = System.DateTime.Now.Ticks.ToString();                          // max 19 character

            StringBuilder sb = new StringBuilder();
            sb.Append(GUIDRoot).Append('.')                                             // 15 character
            .Append(mac).Append('.')                                                    // 13 character
            .Append(ProcessID).Append('.')                                            // max 11 character
            .Append(dtm).Append('.')                                                    // max 20 character
            .Append(num);                                                               // max 10 character

            string str = sb.ToString();
            if (str.Length > 64)
            {
                number = 0;
                return GetDicomGUID(ProcessID);
            }
            else
            {
                return str;
            }
        }
    }
}
