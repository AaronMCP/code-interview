using System;
using System.Text;
using System.Collections.Generic;
using HYS.Common.Xml;
using Dicom;
using HYS.Common.Dicom.Net;

namespace HYS.Common.Dicom
{
    public class DPath : XObject
    {

        public static char Seperator = '_';

        private string _path = "";
        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }

        private DVR _vr = DVR.Unknown;
        public DVR VR
        {
            get { return _vr; }
            set { _vr = value; }
        }

        private bool _enable;
        public bool Enable
        {
            get { return _enable; }
            set { _enable = value; }
        }

        private string _catagory = "";
        public string Catagory
        {
            get { return _catagory; }
            set { _catagory = value; }
        }

        public DPathType Type = DPathType.Normal;

        [XNode(false)]
        public DRangeType Range = DRangeType.None;

        // store description of private/extension tag
        private string _description = "";
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public DPath Clone()
        {
            DPath path = new DPath();
            path.Enable = Enable;
            path.Range = Range;
            path.Type = Type;
            path.Path = Path;
            path.VR = VR;
            return path;
        }

        public DPath()
        {
        }
        public DPath(string path)
        {
            _path = path;
        }

        public int GetTag()
        {
            string strG = GetGroupString();
            string strE = GetElementString();
            if (strG.Length < 1 || strE.Length < 1) return 0;
            int tag = DHelper.HexString2Int(strG + strE);
            return tag;
        }
        public string GetGroupString()
        {
            string[] strlist = Path.Split(DPath.Seperator);
            int length = strlist.Length;
            if (length > 2) return strlist[length - 3];
            return "";
        }
        public string GetElementString()
        {
            string[] strlist = Path.Split(DPath.Seperator);
            int length = strlist.Length;
            if (length > 1) return strlist[length - 2];
            return "";
        }

        public static DPath GetItemGroupPathBegin(int itemIndex)
        {
            DPath p = new DPath();
            SetItemGroupPathBegin(p, itemIndex);
            return p;
        }
        public static DPath GetItemGroupPathEnd(int itemIndex)
        {
            DPath p = new DPath();
            SetItemGroupPathEnd(p, itemIndex);
            return p;
        }
        public static void SetItemGroupPathBegin(DPath p, int itemIndex)
        {
            if (p == null) return;
            p.Type = DPathType.BeginItem;
            p.Path = "Begin Item " + itemIndex.ToString();
        }
        public static void SetItemGroupPathEnd(DPath p, int itemIndex)
        {
            if (p == null) return;
            p.Type = DPathType.EndItem;
            p.Path = "End Item " + itemIndex.ToString();
        }

        public int GetTag(int depth)
        {
            if (depth < 0) return 0;
            int gIndex = depth * 4;
            int eIndex = depth * 4 + 1;

            string[] strlist = Path.Split(DPath.Seperator);
            if (strlist.Length <= eIndex) return 0;

            string gStr = strlist[gIndex];
            string eStr = strlist[eIndex];

            return DHelper.HexString2Int(gStr + eStr);
        }

        public List<string> GetTagGE(int depth)
        {
            if (depth < 0) return null;
            int gIndex = depth * 4;
            int eIndex = depth * 4 + 1;

            string[] strlist = Path.Split(DPath.Seperator);
            if (strlist.Length <= eIndex) return null;

            string gStr = strlist[gIndex];
            string eStr = strlist[eIndex];
            List<string> list = new List<string>();
            list.Add(gStr);
            list.Add(eStr);
            return list;
        }

        public static DPath GetDPath(DicomTag tag, DVR vr, string description, string sqRootPath, int sqIndex)
        {
            string strG = DHelper.Int2HexString(tag.Group);
            string strE = DHelper.Int2HexString(tag.Element);
            //string name = DHelper.GetTagName((uint)ele.Tag);

            // for shorten the DPath string length 20071010
            string name = "A";

            StringBuilder sb = new StringBuilder();
            if (sqRootPath != null && sqIndex >= 0)
            {
                sb.Append(sqRootPath).Append(Seperator).Append(sqIndex.ToString()).Append(Seperator);
            }
            sb.Append(strG).Append(Seperator).Append(strE).Append(Seperator).Append(name).Append(Seperator);
            string str = sb.ToString();

            DPath dpath = new DPath();
            dpath.Path = str.TrimEnd(Seperator);
            dpath.Description = description;
            dpath.VR = vr;
            return dpath;
        }

        public string GetTagName()
        {
            if (_description != null && _description.Length > 0) return _description;
            else return DHelper.GetTagName((uint)GetTag());
        }

        public static string GetTagName(uint tag)
        {
            return ((tag_t)tag).ToString();
        }
    }
}
