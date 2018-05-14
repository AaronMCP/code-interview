using System;
using System.Collections.Generic;
using System.Text;
using HYS.Common.Xml;
using Dicom;

namespace HYS.Common.Dicom
{
    public enum DByteEndian
    {
        Little = 0,
        Big = 1,
        No = 2,
    }

    public class DPersonNameEncodingRule : XObject
    {
        public DPersonNameEncodingRule()
        {
            Enable = false;
            Endian = DByteEndian.Little;
            SingleByteCodPage = Encoding.ASCII.CodePage;
            IdeographCodePage = 50220;  // 20932;   //EUC-JP : Japanese (JIS 0208-1990 and 0121-1990) 
            PhoneticCodePage = 932; // 20932;   // 50221;   // 50222;    //iso-2022-jp : ISO 2022 Japanese JIS X 0201-1989; Japanese (JIS-Allow 1 byte Kana - SO/SI) 

            // 关于Phonetic段的编码：

            // 932    单字节片假名，用高位ASCII码表示
            // 20932  双字节片假名，用一个0x8E加上一个高位ASCII码表示
            // 50222  多字节片假名，用低位ASCII码表示
            // 50221  多字节片假名，用低位ASCII码表示，包含secape sequence，如ESC)I、ESC)B等。
            // 50222  双字节片假名，用低位ASCII码表示，包含secape sequence，如ESC$B、ESC(B等。

            // KDT编码双字节片假名时就是用上面的50222编码。
            // KDT编码单字节片假名时在上面的932编码的前面增加一个secape sequence，即ESC)I。

        }

        public bool Enable;
        public DByteEndian Endian;
        public int SingleByteCodPage;
        public int IdeographCodePage;
        public int PhoneticCodePage;
    }

    internal class DPersonName2
    {
        public const char GroupDelimiter = '=';
        public const char ComponentDelimiter = '^';

        private DPersonName2()
        {
            _single_byte = true;
            _ideograph = true;
            _phonetic = true;
        }

        private bool _single_byte;
        private bool _ideograph;
        private bool _phonetic;

        private string _sFamilyName;
        private string _sFirstName;
        private string _sMiddleName;
        private string _sPrefix;
        private string _sSuffix;

        private string _iFamilyName;
        private string _iFirstName;
        private string _iMiddleName;
        private string _iPrefix;
        private string _iSuffix;

        private string _pFamilyName;
        private string _pFirstName;
        private string _pMiddleName;
        private string _pPrefix;
        private string _pSuffix;

        public string sFamilyName
        {
            get { return _sFamilyName; }
            set { _sFamilyName = value; _single_byte = true; }
        }
        public string sFirstName
        {
            get { return _sFirstName; }
            set { _sFirstName = value; _single_byte = true; }
        }
        public string sMiddleName
        {
            get { return _sMiddleName; }
            set { _sMiddleName = value; _single_byte = true; }
        }
        public string sPrefix
        {
            get { return _sPrefix; }
            set { _sPrefix = value; _single_byte = true; }
        }
        public string sSuffix
        {
            get { return _sSuffix; }
            set { _sSuffix = value; _single_byte = true; }
        }

        public string iFamilyName
        {
            get { return _iFamilyName; }
            set { _iFamilyName = value; _ideograph = true; }
        }
        public string iFirstName
        {
            get { return _iFirstName; }
            set { _iFirstName = value; _ideograph = true; }
        }
        public string iMiddleName
        {
            get { return _iMiddleName; }
            set { _iMiddleName = value; _ideograph = true; }
        }
        public string iPrefix
        {
            get { return _iPrefix; }
            set { _iPrefix = value; _ideograph = true; }
        }
        public string iSuffix
        {
            get { return _iSuffix; }
            set { _iSuffix = value; _ideograph = true; }
        }

        public string pFamilyName
        {
            get { return _pFamilyName; }
            set { _pFamilyName = value; _phonetic = true; }
        }
        public string pFirstName
        {
            get { return _pFirstName; }
            set { _pFirstName = value; _phonetic = true; }
        }
        public string pMiddleName
        {
            get { return _pMiddleName; }
            set { _pMiddleName = value; _phonetic = true; }
        }
        public string pPrefix
        {
            get { return _pPrefix; }
            set { _pPrefix = value; _phonetic = true; }
        }
        public string pSuffix
        {
            get { return _pSuffix; }
            set { _pSuffix = value; _phonetic = true; }
        }

        public static DPersonName2 FromDicomString(string strPersonName)
        {
            if (strPersonName == null) return null;


            if (strPersonName.EndsWith("="))
                strPersonName += "^";

            DPersonName2 name = new DPersonName2();

            string[] gList = strPersonName.Split(GroupDelimiter);

            if (gList.Length > 0)
            {
                string strGroup = gList[0];
                string[] cList = strGroup.Split(ComponentDelimiter);
                if (cList.Length > 0) name.sFamilyName = cList[0];
                if (cList.Length > 1) name.sFirstName = cList[1];
                if (cList.Length > 2) name.sMiddleName = cList[2];
                if (cList.Length > 3) name.sPrefix = cList[3];
                if (cList.Length > 4) name.sSuffix = cList[4];
            }

            if (gList.Length > 1)
            {
                string strGroup = gList[1];
                string[] cList = strGroup.Split(ComponentDelimiter);
                if (cList.Length > 0) name.iFamilyName = cList[0];
                if (cList.Length > 1) name.iFirstName = cList[1];
                if (cList.Length > 2) name.iMiddleName = cList[2];
                if (cList.Length > 3) name.iPrefix = cList[3];
                if (cList.Length > 4) name.iSuffix = cList[4];
            }

            if (gList.Length > 2)
            {
                string strGroup = gList[2];
                string[] cList = strGroup.Split(ComponentDelimiter);
                if (cList.Length > 0) name.pFamilyName = cList[0];
                if (cList.Length > 1) name.pFirstName = cList[1];
                if (cList.Length > 2) name.pMiddleName = cList[2];
                if (cList.Length > 3) name.pPrefix = cList[3];
                if (cList.Length > 4) name.pSuffix = cList[4];
            }

            return name;
        }

        private string GetSingleByteName()
        {
            StringBuilder sbSingle = new StringBuilder();

            if (_single_byte)
            {
                sbSingle.Append(sFamilyName + ComponentDelimiter);
                sbSingle.Append(sFirstName + ComponentDelimiter);
                sbSingle.Append(sMiddleName + ComponentDelimiter);
                sbSingle.Append(sPrefix + ComponentDelimiter);
                sbSingle.Append(sSuffix);
            }

            string strSingle = sbSingle.ToString().TrimEnd(ComponentDelimiter);
            return strSingle;
        }
        private string GetIdeographName()
        {
            StringBuilder sbIdeograph = new StringBuilder();

            if (_ideograph)
            {
                sbIdeograph.Append(iFamilyName + ComponentDelimiter);
                sbIdeograph.Append(iFirstName + ComponentDelimiter);
                sbIdeograph.Append(iMiddleName + ComponentDelimiter);
                sbIdeograph.Append(iPrefix + ComponentDelimiter);
                sbIdeograph.Append(iSuffix);
            }

            string strIdeograph = sbIdeograph.ToString().TrimEnd(ComponentDelimiter);
            return strIdeograph;
        }
        private string GetPhoneticName()
        {
            StringBuilder sbPhonetic = new StringBuilder();

            if (_phonetic)
            {
                sbPhonetic.Append(pFamilyName + ComponentDelimiter);
                sbPhonetic.Append(pFirstName + ComponentDelimiter);
                sbPhonetic.Append(pMiddleName + ComponentDelimiter);
                sbPhonetic.Append(pPrefix + ComponentDelimiter);
                sbPhonetic.Append(pSuffix);
            }

            string strPhonetic = sbPhonetic.ToString().TrimEnd(ComponentDelimiter);
            return strPhonetic;
        }
        public string ToDicomString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(GetSingleByteName() + GroupDelimiter);
            sb.Append(GetIdeographName() + GroupDelimiter);
            sb.Append(GetPhoneticName());

            string str = sb.ToString().Trim(GroupDelimiter);

            return str;
        }

        internal string ToNamestring(DPersonNameEncodingRule rule)
        {
            string strSingle = GetSingleByteName();
            string strIdeograph = GetIdeographName();
            string strPhonetic = GetPhoneticName();

            if (strIdeograph.Length > 0) strIdeograph = GroupDelimiter + strIdeograph + GroupDelimiter;
            if (strPhonetic.Length > 0) strPhonetic = GroupDelimiter + strPhonetic + GroupDelimiter;
            // add GroupDelimiter in the both sides, in order to encode the escape sequence

            return strSingle + strIdeograph + strPhonetic;
        }

    }

}
