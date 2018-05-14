using Dicom;
using System;
using System.Collections.Generic;
using System.Text;

namespace HYS.Common.Dicom
{
    public class DPersonName
    {
        private DPersonName(DicomPersonName name)
        {
            _name = name;
            _single_byte = true;
            _ideograph = true;
            _phonetic = true;
        }

        internal readonly DicomPersonName _name;
        private bool _single_byte;
        private bool _ideograph;
        private bool _phonetic;


        public const char GroupDelimiter = '=';
        public const char ComponentDelimiter = '^';

        public string ToDicomString()
        {
            StringBuilder sb = new StringBuilder();

            if (_single_byte)
            {
                StringBuilder sbSingle = new StringBuilder();


                string strSingle = sbSingle.ToString().TrimEnd(ComponentDelimiter);
                sb.Append(strSingle + GroupDelimiter);
            }

            if (_ideograph)
            {
                StringBuilder sbIdeograph = new StringBuilder();

                string strIdeograph = sbIdeograph.ToString().TrimEnd(ComponentDelimiter);
                sb.Append(strIdeograph + GroupDelimiter);
            }

            if (_phonetic)
            {
                StringBuilder sbPhonetic = new StringBuilder();


                string strPhonetic = sbPhonetic.ToString().TrimEnd(ComponentDelimiter);
                sb.Append(strPhonetic);
            }

            string str = sb.ToString().Trim(GroupDelimiter);

            return str;
        }



        public static DPersonName FromEastenName(string familyName, string firstName)
        {
            DPersonName name = new DPersonName(null);

            return name;
        }
        public static DPersonName FromEastenName(string pyFamilyName, string pyFirstName, string familyName, string firstName)
        {
            DPersonName name = new DPersonName(null);


            return name;
        }
        public static DPersonName FromWestenName(string firstName, string familyName)
        {
            return FromDicomString(familyName + ComponentDelimiter + firstName);
        }
        public static DPersonName FromDicomString(string strPersonName)
        {
            if (strPersonName == null) return null;

            DPersonName name = new DPersonName(null);

            bool endwithequal;
            if (strPersonName.EndsWith("="))
            {
                endwithequal = true;
            }
            else
            {
                endwithequal = false;
            }

            //if (strPersonName.Length > 0)
            //{
            //    if ((int)strPersonName[0] >= 128) strPersonName = GroupDelimiter + strPersonName;
            //}

            string[] gList = strPersonName.Split(GroupDelimiter);

            if (endwithequal)
            {
                gList[gList.Length - 2] = gList[gList.Length - 2] + "=";
            }

            if (gList.Length > 0)
            {
                string strGroup = gList[0];
                string[] cList = strGroup.Split(ComponentDelimiter);

            }

            if (gList.Length > 1)
            {
                string strGroup = gList[1];
                string[] cList = strGroup.Split(ComponentDelimiter);

            }

            if (gList.Length > 2)
            {
                string strGroup = gList[2];
                string[] cList = strGroup.Split(ComponentDelimiter);

            }


            return name;
        }
        internal static DPersonName FromPersonName(DicomPersonName name)
        {
            if (name == null) return null;
            return new DPersonName(name);
        }


    }
}
