using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Collections.Generic;
using HYS.Common.Dicom;
using System.Text;

namespace UITest.XmlMapper
{
    public class DListViewItem : TListViewItem
    {
        private UInt32 _tag;
        public UInt32 Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }

        private DVR _vr = DVR.Unknown;
        public DVR VR
        {
            get { return _vr; }
            set { _vr = value; }
        }

        public DListViewItem(UInt32 tag, DVR vr)
        {
            _tag = tag;
            _vr = vr;
        }

        public override void Refresh()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("(").Append(DHelper.Int2HexString((int)(0x0000FFFF & Tag)));
            sb.Append(",").Append(DHelper.Int2HexString((int)(Tag >> 16))).Append(")");
            string strTag = sb.ToString();

            if (SubItems.Count < 2)
            {
                SubItems.Add(strTag);
            }
            else
            {
                SubItems[1].Text = strTag;
            }

            string strDescription = DHelper.GetTagName(Tag);

            if (SubItems.Count < 3)
            {
                SubItems.Add(strDescription);
            }
            else
            {
                SubItems[2].Text = strDescription;
            }

            base.Refresh();
        }
    }
}
