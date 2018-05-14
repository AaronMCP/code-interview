using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.Common.Dicom;
//using HYS.Common.DataAccess;
using HYS.Common.Objects.Rule;
using HYS.Common.Objects.Translation;
using Dicom;

namespace HYS.DicomAdapter.Common
{
    public class TagControler2
    {
        private ComboBox _comboxBoxVR;
        private ComboBox _comboxBoxTag;
        private TextBox _textBoxGroupNum;
        private TextBox _textBoxElementNum;
        private GroupBox _groupBoxDicom;

        private bool _enable;

        public TagControler2(GroupBox gDicom, ComboBox cVR, ComboBox cTag, TextBox tGroup, TextBox tElement)
        {
            _comboxBoxVR = cVR;
            _comboxBoxTag = cTag;
            _groupBoxDicom = gDicom;
            _textBoxGroupNum = tGroup;
            _textBoxElementNum = tElement;

            _comboxBoxVR.SelectedIndexChanged += new EventHandler(_comboxBoxVR_SelectedIndexChanged);
            _comboxBoxTag.SelectedIndexChanged += new EventHandler(_comboxBoxTag_SelectedIndexChanged);
            _comboxBoxTag.TextChanged += new EventHandler(_comboxBoxTag_TextChanged);
            _textBoxGroupNum.TextChanged += new EventHandler(_textBoxGroupNum_TextChanged);
            _textBoxElementNum.TextChanged += new EventHandler(_textBoxElementNum_TextChanged);
        }

        private interface IValue
        {
            int getValue();
        }
        private class VrItem : IValue
        {
            public DVR VR;
            public string EnumName;
            public string DisplayName;
            public override string ToString()
            {
                return DisplayName;
            }
            public int getValue()
            {
                return (int)VR;
            }
        }
        private static List<VrItem> GetVrItemList()
        {
            string[] tagList = DHelper.GetVRNames();
            SortedList<string, VrItem> slist = new SortedList<string, VrItem>();
            foreach (string str in tagList)
            {
                VrItem ti = new VrItem();
                ti.EnumName = str;
                ti.VR = (DVR) DHelper.GetVR(str);
                ti.DisplayName = (ti.VR == DVR.Unknown) ? "" : str;
                slist.Add(ti.DisplayName, ti);
            }
            List<VrItem> tlist = new List<VrItem>();
            foreach (KeyValuePair<string, VrItem> p in slist) tlist.Add(p.Value);
            return tlist;
        }
        private class TagItem : IValue
        {
            public int Tag;
            public string EnumName;
            public string DisplayName;
            public override string ToString()
            {
                return DisplayName;
            }
            public int getValue()
            {
                return (int)Tag;
            }
        }
        private static List<TagItem> GetTagItemList()
        {
            string[] tagList = DHelper.GetTagNames();
            SortedList<string, TagItem> slist = new SortedList<string, TagItem>();
            foreach (string str in tagList)
            {
                TagItem ti = new TagItem();
                ti.EnumName = str;
                ti.Tag = DHelper.GetTag(str);
                ti.DisplayName = str.TrimStart('k');
                slist.Add(ti.DisplayName, ti);
            }
            List<TagItem> tlist = new List<TagItem>();
            foreach (KeyValuePair<string, TagItem> p in slist) tlist.Add(p.Value);
            return tlist;
        }

        public void Initialize()
        {
            List<VrItem> vrList = GetVrItemList();
            List<TagItem> tagList = GetTagItemList();

            this._comboxBoxTag.Items.Add("");
            this._comboxBoxTag.SelectedIndex = 0;
            foreach (TagItem tag in tagList)
            {
                this._comboxBoxTag.Items.Add(tag);
            }

            foreach (VrItem vr in vrList)
            {
                if (vr.VR == DVR.UN) continue;
                this._comboxBoxVR.Items.Add(vr);
            }
            if (this._comboxBoxVR.Items.Count > 0)
                this._comboxBoxVR.SelectedIndex = 0;
        }
        public void LoadSetting(DPath path)
        {
            if (path == null) return;
            SelectItem(this._comboxBoxVR, (int)path.VR);
            if (!SelectItem(this._comboxBoxTag, path.GetTag()))
            {
                this._textBoxGroupNum.Text = path.GetGroupString();
                this._textBoxElementNum.Text = path.GetElementString();
                this._comboxBoxTag.Text = path.Description;
            }
        }
        public void SaveSetting(DPath path, DPath sqPath)
        {
            if (path == null) return;

            string strPath = (sqPath == null) ? "" : sqPath.Path;
            int index = (sqPath == null) ? -1 : 0;

            string description = this._comboxBoxTag.Text;
            int tag = GetTag();
            DicomTag dicomTag = DHelper.Int2DicomTag(tag);
            DPath p = DPath.GetDPath(dicomTag, GetVR(), description, strPath, index);
            path.Description = p.Description;
            path.Path = p.Path;
            path.VR = p.VR;
        }
        public bool ValidateValue()
        {
            int tag = GetTag();
            DVR vr = GetVR();
            string desc = this._comboxBoxTag.Text;

            return (tag != 0) && (vr != DVR.Unknown) && desc.Length > 0;
        }
        public bool Enabled
        {
            get { return _enable; }
            set
            {
                _enable = value;
                //this._comboxBoxTag.Enabled = this._comboxBoxVR.Enabled = _enable;
                this._groupBoxDicom.Enabled = _enable;
            }
        }

        public event EventHandler OnValueChanged;

        private void RefreshGroupElement()
        {
            TagItem item = this._comboxBoxTag.SelectedItem as TagItem;
            
            this._textBoxGroupNum.ReadOnly =
                    this._textBoxElementNum.ReadOnly = (item != null);

            if (item != null)
            {
                int tag = item.Tag;
                int g = DHelper.GetGroup(tag);
                int e = DHelper.GetElement(tag);
                string strG = DHelper.Int2HexString(g);
                string strE = DHelper.Int2HexString(e);
                
                _changingText = true;
                this._textBoxGroupNum.Text = strG;
                this._textBoxElementNum.Text = strE;
                _changingText = false;
            }
        }
        private bool SelectItem(ComboBox cb, int value)
        {
            foreach (object o in cb.Items)
            {
                IValue s = o as IValue;
                if (s!= null && s.getValue() == value)
                {
                    cb.SelectedItem = o;
                    return true;
                }
            }
            return false;
        }
        private int GetTag()
        {
            TagItem item = this._comboxBoxTag.SelectedItem as TagItem;
            if (item != null) return item.Tag;

            string g = this._textBoxGroupNum.Text.Trim();
            string e = this._textBoxElementNum.Text.Trim();

            if (g.Length == 0) g = "0000";
            if (g.Length == 1) g = "000" + g;
            if (g.Length == 2) g = "00" + g;
            if (g.Length == 3) g = "0" + g;
            if (e.Length == 0) e = "0000";
            if (e.Length == 1) e = "000" + e;
            if (e.Length == 2) e = "00" + e;
            if (e.Length == 3) e = "0" + e;

            return DHelper.HexString2Int(g + e);
        }
        public DVR GetVR()
        {
            VrItem item = this._comboxBoxVR.SelectedItem as VrItem;
            if (item == null) return DVR.Unknown;
            return item.VR;
        }

        private bool _changingText;
        private void _comboxBoxTag_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshGroupElement();
            //if (OnValueChanged != null) OnValueChanged(sender, e);

            if (_comboxBoxTag.SelectedIndex > 0)
            {
                _comboxBoxTag.DropDownStyle = ComboBoxStyle.DropDownList;
            }
            else
            {
                _comboxBoxTag.DropDownStyle = ComboBoxStyle.DropDown;
            }
        }
        private void _comboxBoxVR_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OnValueChanged != null) OnValueChanged(sender, e);
        }
        private void _textBoxElementNum_TextChanged(object sender, EventArgs e)
        {
            if (_changingText) return;
            if (OnValueChanged != null) OnValueChanged(sender, e);
        }
        private void _textBoxGroupNum_TextChanged(object sender, EventArgs e)
        {
            if (_changingText) return;
            if (OnValueChanged != null) OnValueChanged(sender, e);
        }
        private void _comboxBoxTag_TextChanged(object sender, EventArgs e)
        {
            if (OnValueChanged != null) OnValueChanged(sender, e);
        }
    }
}
