using System;
using System.Text;
using System.Threading;
using System.Management;
using System.Collections.Generic;
using HYS.Common.Dicom.Net;
using System.Xml;
using System.IO;
using HYS.Common.Xml;
using Dicom;

namespace HYS.Common.Dicom
{
    public class DHelper
    {
        public static char ValueDelimiter = '\\';
        public static string CharacterSetName = "GB18030";
        public static string XMLNameSpace = "http://www.HaoYiSheng.com/DCM_SDK";
        public static DPersonNameEncodingRule PersonNameEncodingRule = new DPersonNameEncodingRule();

        public static DicomTag Int2DicomTag(int num)
        {
            ushort uGroup = (ushort)(num / 65536);
            ushort uElement = (ushort)(num % 65536);
            DicomTag tag = new DicomTag(uGroup, uElement);
            return tag;

        }

        public static DicomTag uInt2DicomTag(uint num)
        {
            ushort uGroup = (ushort)(num / 65536);
            ushort uElement = (ushort)(num % 65536);
            DicomTag tag = new DicomTag(uGroup, uElement);
            return tag;

        }

        public static string Int2HexString(int num)
        {
            string str = num.ToString("x");
            if (str.Length == 0) str = "0000";
            else if (str.Length == 1) str = "000" + str;
            else if (str.Length == 2) str = "00" + str;
            else if (str.Length == 3) str = "0" + str;
            return str.ToUpper();
        }
        public static int HexString2Int(string str)
        {
            try
            {
                int num = int.Parse(str, System.Globalization.NumberStyles.AllowHexSpecifier);
                return num;
            }
            catch (Exception err)
            {
                LogMgt.Logger.Write(err);
                return 0;
            }
        }
        public static ushort HexString2ushort(string str)
        {
            try
            {
                ushort num = ushort.Parse(str, System.Globalization.NumberStyles.AllowHexSpecifier);
                return num;
            }
            catch (Exception err)
            {
                LogMgt.Logger.Write(err);
                return 0;
            }
        }

        public static int GE2int(ushort ugroup, ushort uelement)
        {
            try
            {
                int num = ugroup * 65536 + uelement;
                return num;
            }
            catch (Exception err)
            {
                LogMgt.Logger.Write(err);
                return 0;
            }
        }

        public static uint GE2uint(ushort ugroup, ushort uelement)
        {
            try
            {
                uint num = (uint)(ugroup * 65536 + uelement);
                return num;
            }
            catch (Exception err)
            {
                LogMgt.Logger.Write(err);
                return 0;
            }
        }

        public static int DecString2Int(string str)
        {
            try
            {
                int num = int.Parse(str);
                return num;
            }
            catch (Exception err)
            {
                LogMgt.Logger.Write(err);
                return 0;
            }
        }


        public static string[] GetVRNames()
        {
            return Enum.GetNames(typeof(DicomVR));
        }
        public static string GetTagName(uint tag)
        {
            return ((tag_t)tag).ToString();
        }

        public static string[] GetTagNames()
        {
            return Enum.GetNames(typeof(tag_t));
        }

        public static object GetVR(string name)
        {
            if (name == null || name.Length < 1) return null;
            return (DicomVR)Enum.Parse(typeof(DicomVR), name);
        }

        public static int GetGroup(int tag)
        {
            DicomTag dicomTag = DHelper.Int2DicomTag(tag);
            return dicomTag.Group;
        }

        public static int GetElement(int tag)
        {
            DicomTag dicomTag = DHelper.Int2DicomTag(tag);
            return dicomTag.Element;
        }

        public static int GetTag(string name)
        {
            if (name == null || name.Length < 1) return 0;
            tag_t t = (tag_t)Enum.Parse(typeof(tag_t), name);
            return (int)t;
        }

        public static bool IsStringLike(DVR vr)
        {
            switch (vr)
            {
                case DVR.AE:
                case DVR.AS:
                case DVR.CS:
                case DVR.DS:
                case DVR.IS:
                case DVR.LO:
                case DVR.LT:
                case DVR.SH:
                case DVR.ST:
                case DVR.UT:
                case DVR.PN:
                    {
                        return true;
                    }
                default:
                    {
                        return false;
                    }
            }
        }

        public static DicomVR ConvertToDicomVR(DVR vr)
        {
            if (vr == DVR.AE)
            {
                return DicomVR.AE;
            }
            else if (vr == DVR.AS)
            {
                return DicomVR.AS;
            }
            else if (vr == DVR.AT)
            {
                return DicomVR.AT;
            }
            else if (vr == DVR.CS)
            {
                return DicomVR.CS;
            }
            else if (vr == DVR.DA)
            {
                return DicomVR.DA;
            }
            else if (vr == DVR.DS)
            {
                return DicomVR.DS;
            }
            else if (vr == DVR.DT)
            {
                return DicomVR.DT;
            }
            else if (vr == DVR.FD)
            {
                return DicomVR.FD;
            }
            else if (vr == DVR.FL)
            {
                return DicomVR.FL;
            }
            else if (vr == DVR.IS)
            {
                return DicomVR.IS;
            }
            else if (vr == DVR.LO)
            {
                return DicomVR.LO;
            }
            else if (vr == DVR.LT)
            {
                return DicomVR.LT;
            }
            else if (vr == DVR.OB)
            {
                return DicomVR.OB;
            }
            else if (vr == DVR.FD)
            {
                //no
                return DicomVR.OD;
            }
            else if (vr == DVR.OF)
            {
                return DicomVR.OF;
            }
            else if (vr == DVR.OW)
            {
                return DicomVR.OW;
            }
            else if (vr == DVR.PN)
            {
                return DicomVR.PN;
            }
            else if (vr == DVR.SH)
            {
                return DicomVR.SH;
            }
            else if (vr == DVR.SL)
            {
                return DicomVR.SL;
            }
            else if (vr == DVR.SQ)
            {
                return DicomVR.SQ;
            }
            else if (vr == DVR.SS)
            {
                return DicomVR.SS;
            }
            else if (vr == DVR.ST)
            {
                return DicomVR.ST;
            }
            else if (vr == DVR.TM)
            {
                return DicomVR.TM;
            }
            else if (vr == DVR.Unknown)
            {
                //no
                return DicomVR.UC;
            }
            else if (vr == DVR.UI)
            {
                return DicomVR.UI;
            }
            else if (vr == DVR.UL)
            {
                return DicomVR.UL;
            }
            else if (vr == DVR.UN)
            {
                return DicomVR.UN;
            }
            else if (vr == DVR.UT)
            {
                //no
                return DicomVR.UR;
            }
            else if (vr == DVR.US)
            {
                return DicomVR.US;
            }
            else if (vr == DVR.UT)
            {
                return DicomVR.UT;
            }

            return DicomVR.NONE;
        }

        public static DVR ConvertToDVR(DicomVR vr)
        {
            if (vr == DicomVR.AE)
            {
                return DVR.AE;
            }
            else if (vr == DicomVR.AS)
            {
                return DVR.AS;
            }
            else if (vr == DicomVR.AT)
            {
                return DVR.AT;
            }
            else if (vr == DicomVR.CS)
            {
                return DVR.CS;
            }
            else if (vr == DicomVR.DA)
            {
                return DVR.DA;
            }
            else if (vr == DicomVR.DS)
            {
                return DVR.DS;
            }
            else if (vr == DicomVR.DT)
            {
                return DVR.DT;
            }
            else if (vr == DicomVR.FD)
            {
                return DVR.FD;
            }
            else if (vr == DicomVR.FL)
            {
                return DVR.FL;
            }
            else if (vr == DicomVR.IS)
            {
                return DVR.IS;
            }
            else if (vr == DicomVR.LO)
            {
                return DVR.LO;
            }
            else if (vr == DicomVR.LT)
            {
                return DVR.LT;
            }
            else if (vr == DicomVR.OB)
            {
                return DVR.OB;
            }
            else if (vr == DicomVR.OD)
            {
                //no
                return DVR.FD;
            }
            else if (vr == DicomVR.OF)
            {
                return DVR.OF;
            }
            else if (vr == DicomVR.OW)
            {
                return DVR.OW;
            }
            else if (vr == DicomVR.PN)
            {
                return DVR.PN;
            }
            else if (vr == DicomVR.SH)
            {
                return DVR.SH;
            }
            else if (vr == DicomVR.SL)
            {
                return DVR.SL;
            }
            else if (vr == DicomVR.SQ)
            {
                return DVR.SQ;
            }
            else if (vr == DicomVR.SS)
            {
                return DVR.SS;
            }
            else if (vr == DicomVR.ST)
            {
                return DVR.ST;
            }
            else if (vr == DicomVR.TM)
            {
                return DVR.TM;
            }
            else if (vr == DicomVR.UC)
            {
                //no
                return DVR.Unknown;
            }
            else if (vr == DicomVR.UI)
            {
                return DVR.UI;
            }
            else if (vr == DicomVR.UL)
            {
                return DVR.UL;
            }
            else if (vr == DicomVR.UN)
            {
                return DVR.UN;
            }
            else if (vr == DicomVR.UR)
            {
                //no
                return DVR.UT;
            }
            else if (vr == DicomVR.US)
            {
                return DVR.US;
            }
            else if (vr == DicomVR.UT)
            {
                return DVR.UT;
            }
            return DVR.UN;
        }

        public static bool IsDateTime(DVR vr)
        {
            switch (vr)
            {
                case DVR.DA:
                case DVR.TM:
                case DVR.DT:
                    {
                        return true;
                    }
                default:
                    {
                        return false;
                    }
            }
        }

        private static string[] _icharacterSet;
        public static string[] iCharacterSet
        {
            get
            {
                if (_icharacterSet == null)
                {
                    _icharacterSet = CharacterSetName.Split(ValueDelimiter);
                }
                return _icharacterSet;
            }
        }

        public static string GUIDRoot = "1.2.840.113564";
        /// <summary>
        /// InterfaceID should be a zero based Int32.ToString(). It is no more than 10 character.
        /// </summary>
        /// <param name="interfaceID"></param>
        /// <returns></returns>
        public static string GetDicomGUID(string interfaceID)
        {
            return GetDicomGUID(interfaceID, 64);                                         // 64 is the limitation of DICOM

        }

        private static int number = 0;
        //private static int number = int.MaxValue - 1;                                 // for testing
        public static int GetIncreasingNumber()
        {
            Interlocked.Increment(ref number);
            if (number < 0) number = 0;
            return number;
        }

        public static Int64 GetMACAddressNumber()
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

        private static string _macAddress;
        public static string GetMACAddress()
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

        public static string GetDicomGUID(string interfaceID, int maxLength)
        {
            string num = GetIncreasingNumber().ToString();
            string mac = GetMACAddressNumber().ToString();
            //string dtm = System.DateTime.Now.ToString("yyyyMMddhhmmssfffffff");       // 21 character
            string dtm = System.DateTime.Now.Ticks.ToString();                          // max 19 character
            string iid = string.IsNullOrEmpty(interfaceID) ? "0" : interfaceID;

            StringBuilder sb = new StringBuilder();
            sb.Append(GUIDRoot).Append('.')                                             // 15 character
            .Append(mac).Append('.')                                                    // 13 character
            .Append(iid).Append('.')                                                    // max 11 character
            .Append(dtm).Append('.')                                                    // max 20 character
            .Append(num);                                                               // max 10 character

            string str = sb.ToString();
            if (str.Length > maxLength)
            {
                number = 0;
                return GetDicomGUID(iid);  // cannot meet the maxlength limitation, just do the better it can to shorten the ID
            }
            else
            {
                return str;
            }
        }

        public static string ConvertToDicomXml(DElementList[] lists)
        {
            StringBuilder sbXml = new StringBuilder();
            sbXml.AppendFormat("<DICOM xmlns=\"{0}\">", DHelper.XMLNameSpace);
            if (lists != null) foreach (DElementList l in lists) sbXml.Append(l.ToXMLString(""));
            sbXml.Append("</DICOM>");
            string str = sbXml.ToString();
            return str;
        }

        public static string ConvertToDicomXml(DElementList list)
        {
            StringBuilder sbXml = new StringBuilder();
            sbXml.AppendFormat("<DICOM xmlns=\"{0}\">", DHelper.XMLNameSpace);
            if (list != null) sbXml.Append(list.ToXMLString(""));
            sbXml.Append("</DICOM>");
            string str = sbXml.ToString();
            return str;
        }

        /// <param name="xml">Please use XObjectHelper to dismiss the XML declaration first.</param>
        public static string GetElementListXmlFromDicomXml(string xml)
        {
            try
            {
                if (string.IsNullOrEmpty(xml)) return "";
                using (XmlReader reader = XmlTextReader.Create(new StringReader(xml)))
                {
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "DElementList")
                        {
                            string str = reader.ReadOuterXml();
                            return str;
                        }
                    }
                }
                return "";
            }
            catch (Exception err)
            {
                LogMgt.Logger.Write(err.ToString());
                return null;
            }
        }

    }
}
