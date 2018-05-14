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

namespace HYS.DicomAdapter.Common
{
    public class TagControler
    {
        private ComboBox _comboxBoxVR;
        private ComboBox _comboxBoxTag;
        private TextBox _textBoxGroupNum;
        private TextBox _textBoxElementNum;
        private GroupBox _groupBoxDicom;

        private bool _enable;
        private bool _asQueryResult;

        public TagControler(bool asQueryResult, GroupBox gDicom, ComboBox cVR, ComboBox cTag, TextBox tGroup, TextBox tElement)
        {
            _comboxBoxVR = cVR;
            _comboxBoxTag = cTag;
            _groupBoxDicom = gDicom;
            _textBoxGroupNum = tGroup;
            _textBoxElementNum = tElement;
            _asQueryResult = asQueryResult;

            _comboxBoxVR.SelectedIndexChanged += new EventHandler(_comboxBoxVR_SelectedIndexChanged);
            _comboxBoxTag.SelectedIndexChanged += new EventHandler(_comboxBoxTag_SelectedIndexChanged);
        }

        public void Initialize()
        {
            string[] vrList = DHelper.GetVRNames();
            string[] tagList = DHelper.GetTagNames();

            this._comboxBoxTag.Items.Add("");
            this._comboxBoxTag.SelectedIndex = 0;
            foreach (string tag in tagList)
            {
                this._comboxBoxTag.Items.Add(tag);
            }

            foreach (string vr in vrList)
            {
                this._comboxBoxVR.Items.Add(vr);
            }
            if (this._comboxBoxVR.Items.Count > 0)
                this._comboxBoxVR.SelectedIndex = 0;
        }
        public void LoadSetting(DPath path)
        {
            if (path == null) return;
            SelectItem(this._comboxBoxTag, DHelper.GetTagName((uint)path.GetTag()));
            SelectItem(this._comboxBoxVR, path.VR.ToString());
        }
        public void SaveSetting(DPath path)
        {
            if (path == null) return;
            //cfg.Tag = GetTag();       //todo...
            path.VR = (DVR)GetVR();
        }
        public bool ValidateValue()
        {
            int tag = GetTag();
            object vr = GetVR();

            return (tag != 0) && (vr != null);
        }
        public bool Enabled
        {
            get { return _enable; }
            set 
            {
                _enable = value;
                this._comboxBoxTag.Enabled = this._comboxBoxVR.Enabled = _enable;
            }
        }

        public event EventHandler OnValueChanged;

        private void RefreshGroupElement(int tag)
        {
            int g = DHelper.GetGroup(tag);
            int e = DHelper.GetElement(tag);
            string strG = DHelper.Int2HexString(g);
            string strE = DHelper.Int2HexString(e);
            this._textBoxGroupNum.Text = strE;
            this._textBoxElementNum.Text = strG;
        }
        private void SelectItem(ComboBox cb, string str)
        {
            foreach (object o in cb.Items)
            {
                string s = o as string;
                if (s == str)
                {
                    cb.SelectedItem = o;
                    break;
                }
            }
        }
        private object GetVR()
        {
            string str = this._comboxBoxVR.SelectedItem as string;
            if (str == null || str.Length < 1) return null;
            return DHelper.GetVR(str);
        }
        private int GetTag()
        {
            string str = this._comboxBoxTag.SelectedItem as string;
            if (str == null || str.Length < 1) return 0;
            return DHelper.GetTag(str);
        }

        private void _comboxBoxTag_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshGroupElement(GetTag());
            if (OnValueChanged != null) OnValueChanged(sender, e);
        }
        private void _comboxBoxVR_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OnValueChanged != null) OnValueChanged(sender, e);
        }
    }
}
