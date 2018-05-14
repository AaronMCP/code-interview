using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using HYS.Adapter.Base;
using HYS.Common.Objects.Rule;
using HYS.Common.Objects.Config;
using HYS.Common.Objects.Device;
using HYS.Common.Objects.Logging;
using HYS.Common.Objects.Translation;

namespace HYS.Adapter.Config.Controls
{
    public partial class DataProcessControl : UserControl, IConfigControl
    {
        public DataProcessControl()
        {
            InitializeComponent();
        }

        #region IConfigControl Members

        public bool LoadConfig()
        {
            if (Program.ServiceMgt != null)
            {
                RefreshFieldList();
                this.checkBoxCC2PY.Checked = Program.ServiceMgt.Config.Chinese2Pinyin.Enable;
                
                RefreshReplaceList();
                this.checkBoxReplace.Checked = Program.ServiceMgt.Config.Replacement.Enable;

                RefreshComposeList();
                this.checkBoxCompose.Checked = Program.ServiceMgt.Config.Composing.Enable;

                RefreshFieldKanJiList();
                this.chkLevel3JP.Checked = Program.ServiceMgt.Config.L3KanJiReplacement.Enable;
            }
            else
            {
                this.groupBoxCC2PY.Enabled = false;
                this.groupBoxReplace.Enabled = false;
                this.groupBoxCompose.Enabled = false;
                this.groupBoxKanJi.Enabled = false;
            }

            return true;
        }

        public bool SaveConfig()
        {
            if (Program.ServiceMgt != null)
            {
                Program.ServiceMgt.Config.Chinese2Pinyin.Enable = this.checkBoxCC2PY.Checked;
                Program.ServiceMgt.Config.Replacement.Enable = this.checkBoxReplace.Checked;
                Program.ServiceMgt.Config.Composing.Enable = this.checkBoxCompose.Checked;
                Program.ServiceMgt.Config.L3KanJiReplacement.Enable = this.chkLevel3JP.Checked;
                if (!Program.ServiceMgt.Save())
                {
                    MessageBox.Show("Save Data Process configuration failed.");
                }
            }

            return true;
        }

        #endregion

        #region Translation GUI control

        private void AddField()
        {
            FormField frm = new FormField(null);
            if (frm.ShowDialog(this) != DialogResult.OK) return;

            Chinese2PinyinRuleItem f = frm.Field;
            if (f == null) return;

            foreach (Chinese2PinyinRuleItem field in Program.ServiceMgt.Config.Chinese2Pinyin.Fields)
            {
                if (field.Table == f.Table &&
                    field.FieldName == f.FieldName)
                {
                    MessageBox.Show(this, "Field " + f.ToString() + " is already in the list.", "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            Program.ServiceMgt.Config.Chinese2Pinyin.Fields.Add(f);
            RefreshFieldList();
        }
        private void EditField()
        {
            if (this.listViewCC2PY.SelectedItems.Count < 1) return;

            Chinese2PinyinRuleItem f = this.listViewCC2PY.SelectedItems[0].Tag as Chinese2PinyinRuleItem;
            if (f == null) return;

            FormField frm = new FormField(f);
            if (frm.ShowDialog(this) != DialogResult.OK) return;

            RefreshFieldList();
        }
        private void DeleteField()
        {
            if (this.listViewCC2PY.SelectedItems.Count < 1) return;
            Chinese2PinyinRuleItem f = this.listViewCC2PY.SelectedItems[0].Tag as Chinese2PinyinRuleItem;
            if (f == null) return;

            Program.ServiceMgt.Config.Chinese2Pinyin.Fields.Remove(f);
            RefreshFieldList();
        }
        private void RefreshFieldList()
        {
            this.listViewCC2PY.Items.Clear();
            foreach (Chinese2PinyinRuleItem f in Program.ServiceMgt.Config.Chinese2Pinyin.Fields)
            {
                ListViewItem item = new ListViewItem(f.ToString());
                item.SubItems.Add((GetConvertTypeName(f.ConvertType, false) + ";" + GetPinyinTypeName(f.Type, false)).Trim(';'));
                item.Tag = f;
                this.listViewCC2PY.Items.Add(item);
            }
            RefreshFieldButton();
        }
        private void RefreshFieldButton()
        {
            this.buttonEdit.Enabled =
            this.buttonDelete.Enabled = this.listViewCC2PY.SelectedItems.Count > 0;
        }

        internal static string GetPinyinTypeName(PinyinType t, bool forCombox)
        {
            switch (t)
            {
                default:
                    {
                        if (forCombox) return "None";
                        else return "";
                    }
                case PinyinType.BIG52RomaPinyin: return "BIG5 to Roma Pinyin";
                case PinyinType.GBK2RomaPinyin: return "GBK to Roma Pinyin";
                case PinyinType.GB2Pinyin: return "GB to Pinyin";
            }
        }
        internal static string GetConvertTypeName(ChineseCodeConvertType t, bool forCombox)
        {
            switch (t)
            {
                default:
                    {
                        if (forCombox) return "None";
                        else return "";
                    }
                case ChineseCodeConvertType.BIG52GB: return "BIG5 to GB";
                case ChineseCodeConvertType.BIG52GBK: return "BIG5 to GBK";
                case ChineseCodeConvertType.GB2BIG5: return "GB to BIG5";
                case ChineseCodeConvertType.GB2GBK: return "GB to GBK";
                case ChineseCodeConvertType.GBK2BIG5: return "GBK to BIG5";
                case ChineseCodeConvertType.GBK2GB: return "GBK to GB";
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            AddField();
        }
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            EditField();
        }
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            DeleteField();
        }
        private void checkBoxCC2PY_CheckedChanged(object sender, EventArgs e)
        {
            this.listViewCC2PY.Enabled =
                this.buttonDelete.Enabled =
                this.buttonEdit.Enabled =
                this.buttonAdd.Enabled = this.checkBoxCC2PY.Checked;
            if (this.checkBoxCC2PY.Checked) RefreshFieldButton();
        }
        private void listViewCC2PY_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshFieldButton();
        }
        private void listViewCC2PY_DoubleClick(object sender, EventArgs e)
        {
            EditField();
        }

        #endregion

        #region Replacement GUI control

        private void AddReplace()
        {
            FormReplace frm = new FormReplace(null);
            if (frm.ShowDialog(this) != DialogResult.OK) return;

            ReplacementRuleItem item = frm.RuleItem;
            if (item == null) return;

            foreach (ReplacementRuleItem i in Program.ServiceMgt.Config.Replacement.Fields)
            {
                if (item.Table == i.Table &&
                    item.FieldName == i.FieldName)
                {
                    MessageBox.Show(this, "Field " + i.ToString() + " is already in the list.", "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            Program.ServiceMgt.Config.Replacement.Fields.Add(item);
            RefreshReplaceList();
        }
        private void EditReplace()
        {
            if (this.listViewReplace.SelectedItems.Count < 1) return;

            ReplacementRuleItem f = this.listViewReplace.SelectedItems[0].Tag as ReplacementRuleItem;
            if (f == null) return;

            FormReplace frm = new FormReplace(f);
            if (frm.ShowDialog(this) != DialogResult.OK) return;

            RefreshReplaceList();
        }
        private void DeleteReplace()
        {
            if (this.listViewReplace.SelectedItems.Count < 1) return;

            ReplacementRuleItem f = this.listViewReplace.SelectedItems[0].Tag as ReplacementRuleItem;
            if (f == null) return;

            Program.ServiceMgt.Config.Replacement.Fields.Remove(f);
            RefreshReplaceList();
        }
        private void RefreshReplaceList()
        {
            this.listViewReplace.Items.Clear();
            foreach (ReplacementRuleItem i in Program.ServiceMgt.Config.Replacement.Fields)
            {
                ListViewItem item = new ListViewItem(i.ToString());
                item.SubItems.Add(i.RegularExpression.Expression);
                item.SubItems.Add(i.RegularExpression.Replacement);
                item.SubItems.Add(i.RegularExpression.Description);
                item.Tag = i;
                this.listViewReplace.Items.Add(item);
            }
            RefreshReplaceButton();
        }
        private void RefreshReplaceButton()
        {
            this.buttonEditReplace.Enabled =
            this.buttonDeleteReplace.Enabled = this.listViewReplace.SelectedItems.Count > 0;
        }

        private void buttonAddReplace_Click(object sender, EventArgs e)
        {
            AddReplace();
        }
        private void buttonEditReplace_Click(object sender, EventArgs e)
        {
            EditReplace();
        }
        private void buttonDeleteReplace_Click(object sender, EventArgs e)
        {
            DeleteReplace();
        }
        private void checkBoxReplace_CheckedChanged(object sender, EventArgs e)
        {
            this.listViewReplace.Enabled =
                this.buttonAddReplace.Enabled =
                this.buttonEditReplace.Enabled =
                this.buttonDeleteReplace.Enabled = this.checkBoxReplace.Checked;
            if (this.checkBoxReplace.Checked) RefreshReplaceButton();
        }
        private void listViewReplace_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshReplaceButton();
        }
        private void listViewReplace_DoubleClick(object sender, EventArgs e)
        {
            EditReplace();
        }

        #endregion        

        #region Composing GUI control

        private void AddCompose()
        {
            FormCompose frm = new FormCompose(null);
            if (frm.ShowDialog(this) != DialogResult.OK) return;

            ComposingRuleItem item = frm.RuleItem;
            if (item == null) return;

            foreach (ComposingRuleItem i in Program.ServiceMgt.Config.Composing.Fields)
            {
                if (item.Table == i.Table &&
                    item.FieldName == i.FieldName)
                {
                    MessageBox.Show(this, "Field " + i.ToString() + " is already in the list.", "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            Program.ServiceMgt.Config.Composing.Fields.Add(item);
            RefreshComposeList();
        }
        private void EditCompose()
        {
            if (this.listViewCompose.SelectedItems.Count < 1) return;

            ComposingRuleItem f = this.listViewCompose.SelectedItems[0].Tag as ComposingRuleItem;
            if (f == null) return;

            ComposingRuleItem tmpf = f.Clone();
            FormCompose frm = new FormCompose(tmpf);
            if (frm.ShowDialog(this) != DialogResult.OK) return;

            foreach (ComposingRuleItem i in Program.ServiceMgt.Config.Composing.Fields)
            {
                if (i.Table == f.Table &&
                    i.FieldName == f.FieldName) continue;

                if (tmpf.Table == i.Table &&
                    tmpf.FieldName == i.FieldName)
                {
                    MessageBox.Show(this, "Field " + i.ToString() + " is already in the list.", "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            int index = Program.ServiceMgt.Config.Composing.Fields.IndexOf(f);
            Program.ServiceMgt.Config.Composing.Fields.Insert(index, tmpf);
            Program.ServiceMgt.Config.Composing.Fields.Remove(f);

            RefreshComposeList();
        }
        private void DeleteCompose()
        {
            if (this.listViewCompose.SelectedItems.Count < 1) return;

            ComposingRuleItem f = this.listViewCompose.SelectedItems[0].Tag as ComposingRuleItem;
            if (f == null) return;

            Program.ServiceMgt.Config.Composing.Fields.Remove(f);
            RefreshComposeList();
        }
        private void RefreshComposeList()
        {
            this.listViewCompose.Items.Clear();
            foreach (ComposingRuleItem i in Program.ServiceMgt.Config.Composing.Fields)
            {
                ListViewItem item = new ListViewItem(i.ToString());
                item.SubItems.Add(i.ComposePattern);
                StringBuilder sb = new StringBuilder();
                foreach (GWDataDBField f in i.FromFields)
                {
                    sb.Append(f.ToString()).Append(',');
                }
                item.SubItems.Add(sb.ToString().TrimEnd(','));
                item.SubItems.Add(i.Description);
                item.Tag = i;
                this.listViewCompose.Items.Add(item);
            }
            RefreshComposeButton();
        }
        private void RefreshComposeButton()
        {
            this.buttonEditCompose.Enabled =
            this.buttonDeleteCompose.Enabled = this.listViewCompose.SelectedItems.Count > 0;
        }

        private void buttonAddCompose_Click(object sender, EventArgs e)
        {
            AddCompose();
        }
        private void buttonEditCompose_Click(object sender, EventArgs e)
        {
            EditCompose();
        }
        private void buttonDeleteCompose_Click(object sender, EventArgs e)
        {
            DeleteCompose();
        }
        private void checkBoxCompose_CheckedChanged(object sender, EventArgs e)
        {
            this.listViewCompose.Enabled =
                this.buttonAddCompose.Enabled =
                this.buttonEditCompose.Enabled =
                this.buttonDeleteCompose.Enabled = this.checkBoxCompose.Checked;
            if (this.checkBoxCompose.Checked) RefreshComposeButton();
        }
        private void listViewCompose_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshComposeButton();
        }
        private void listViewCompose_DoubleClick(object sender, EventArgs e)
        {
            EditCompose();
        }

        #endregion

        private void btnAddKanJi_Click(object sender, EventArgs e)
        {
            FormFieldKanJi frm = new FormFieldKanJi(null);
            if (frm.ShowDialog(this) != DialogResult.OK) 
                return;

            Level3KanJiReplacementRuleItem f = frm.Field;
            if (f == null) 
                return;

            foreach (Level3KanJiReplacementRuleItem field in Program.ServiceMgt.Config.L3KanJiReplacement.Fields)
            {
                if (field.Table == f.Table &&
                    field.FieldName == f.FieldName)
                {
                    MessageBox.Show(this, "Field " + f.ToString() + " is already in the list.", "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            Program.ServiceMgt.Config.L3KanJiReplacement.Fields.Add(f);
            RefreshFieldKanJiList();
        }

        private void btnEditKanJi_Click(object sender, EventArgs e)
        {
            EditKanJi();
        }

        private void EditKanJi()
        {
            if (this.listViewKanJi.SelectedItems.Count < 1)
            {
                return;
            }
            Level3KanJiReplacementRuleItem f = this.listViewKanJi.SelectedItems[0].Tag as Level3KanJiReplacementRuleItem;
            if (f == null)
            {
                return;
            }
            FormFieldKanJi frm = new FormFieldKanJi(f);
            if (frm.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }
            RefreshFieldKanJiList();
        }

        private void btnDelKanJi_Click(object sender, EventArgs e)
        {
            if (this.listViewKanJi.SelectedItems.Count < 1) return;
            Level3KanJiReplacementRuleItem f = this.listViewKanJi.SelectedItems[0].Tag as Level3KanJiReplacementRuleItem;
            if (f == null) return;

            Program.ServiceMgt.Config.L3KanJiReplacement.Fields.Remove(f);
            RefreshFieldKanJiList();
        }

        private void RefreshFieldKanJiList()
        {
            this.listViewKanJi.Items.Clear();
            foreach (Level3KanJiReplacementRuleItem f in Program.ServiceMgt.Config.L3KanJiReplacement.Fields)
            {
                ListViewItem item = new ListViewItem(f.ToString());
                item.SubItems.Add(f.ReplacementChar);
                item.Tag = f;
                this.listViewKanJi.Items.Add(item);
            }
            RefreshFieldButton();
        }
        private void RefreshFieldKanJiButton()
        {
            this.btnEditKanJi.Enabled =
            this.btnDelKanJi.Enabled = this.listViewKanJi.SelectedItems.Count > 0;
        }

        private void chkLevel3JP_CheckedChanged(object sender, EventArgs e)
        {
            this.listViewKanJi.Enabled =
               this.btnDelKanJi.Enabled =
               this.btnEditKanJi.Enabled =
               this.btnAddKanJi.Enabled = this.chkLevel3JP.Checked;
            if (this.chkLevel3JP.Checked)
                RefreshFieldKanJiButton();
        }

        private void listViewKanJi_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshFieldKanJiButton();
        }

        private void listViewKanJi_DoubleClick(object sender, EventArgs e)
        {
            EditKanJi();
        }

    }
}
