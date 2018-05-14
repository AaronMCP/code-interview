using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using HYS.Common.Xml;
using Dicom;
using HYS.Common.Dicom.Net;

namespace HYS.Common.Dicom
{
    public class PrivateTag : XObject
    {
        private string _tag = "";
        public string Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }

        private uint _intTag;
        internal uint IntTag
        {
            get
            {
                if (_intTag > 0) return _intTag;
                _intTag = (uint)DHelper.HexString2Int(Tag);
                return _intTag;
            }
        }

        private DVR _vr = DVR.Unknown;
        public DVR VR
        {
            get { return _vr; }
            set { _vr = value; }
        }

        public PrivateTag()
        {
        }
        public PrivateTag(string tag, DVR vr)
        {
            _tag = tag;
            _vr = vr;
        }
    }

    public class PrivateTagHelper
    {
        public static XCollection<PrivateTag> PrivateTagList = new XCollection<PrivateTag>();

        public static bool IsPrivateTag(int tag)
        {
            // private tag or public tag which is not included in KDT's dictionary

            int group = DHelper.GetGroup(tag);
            return (group % 2 != 0) || !Enum.IsDefined(typeof(tag_t), tag);
        }

        internal static DicomVR GetPrivateTagVR(uint tag)
        {
            if (PrivateTagList == null || PrivateTagList.Count < 1 || tag < 1) return DicomVR.UN;

            foreach (PrivateTag t in PrivateTagList)
            {
                if (t.IntTag == tag)
                {
                    return DHelper.ConvertToDicomVR(t.VR);
                }
            }

            return DicomVR.UN;
        }

        
    }
}
