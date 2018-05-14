using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.Common.Objects.Rule;
using HYS.DicomAdapter.Common;
using HYS.DicomAdapter.MWLServer.Objects;

namespace HYS.DicomAdapter.MWLServer.Forms
{
    public partial class FormQCAdvance : Form
    {
        public FormQCAdvance()
        {
            InitializeComponent();

            InitializeJoinType();

            this.buttonDefault.Visible = Program.StandAlone;
        }

        private void InitializeJoinType()
        {
            this.comboBoxOperator.Items.Clear();
            Array tlist = Enum.GetValues(typeof(QueryCriteriaType));
            foreach (QueryCriteriaType t in tlist)
            {
                if (t == QueryCriteriaType.None) continue;
                this.comboBoxOperator.Items.Add(t);
            }
        }

        private void LoadSetting()
        {
            switch (Program.ConfigMgt.Config.PersonNameRule.SelectedGroup)
            {
                case PersonNameGroupType.SingleByte:
                    this.radioButtonSingleByte.Checked = true;
                    break;
                case PersonNameGroupType.Ideographic:
                    this.radioButtonIdeographic.Checked = true;
                    break;
                case PersonNameGroupType.Phonetic:
                    this.radioButtonPhonetic.Checked = true;
                    break;
            }

            RefreshList();

            this.comboBoxOperator.SelectedItem = Program.ConfigMgt.Config.AdditionalQueryCriteriaJoinType;

            RefreshQCList();
            RefreshQCButtons();
        }
        private void SaveSetting()
        {
            if (this.radioButtonSingleByte.Checked)
            {
                Program.ConfigMgt.Config.PersonNameRule.SelectedGroup = PersonNameGroupType.SingleByte;
            }
            else if (this.radioButtonIdeographic.Checked)
            {
                Program.ConfigMgt.Config.PersonNameRule.SelectedGroup = PersonNameGroupType.Ideographic;
            }
            else if (this.radioButtonPhonetic.Checked)
            {
                Program.ConfigMgt.Config.PersonNameRule.SelectedGroup = PersonNameGroupType.Phonetic;
            }

            Program.ConfigMgt.Config.AdditionalQueryCriteriaJoinType = (QueryCriteriaType)this.comboBoxOperator.SelectedItem;
        }

        private void RefreshList()
        {
            this.checkedListBoxComponents.Items.Clear();
            foreach (PersonNameComponent c in Program.ConfigMgt.Config.PersonNameRule.Components)
            {
                this.checkedListBoxComponents.Items.Add(c, c.Enable ? CheckState.Checked : CheckState.Unchecked);
            }
            RefreshButtons();
        }
        private void RefreshButtons()
        {
            PersonNameComponent c = this.checkedListBoxComponents.SelectedItem as PersonNameComponent;
            int index = Program.ConfigMgt.Config.PersonNameRule.Components.IndexOf(c);
            int count = Program.ConfigMgt.Config.PersonNameRule.Components.Count;
            if (index < 0)
            {
                this.buttonBackward.Enabled = this.buttonForward.Enabled = false;
            }
            else if (index == 0)
            {
                this.buttonBackward.Enabled = true;
                this.buttonForward.Enabled = false;
            }
            else if (index >= count - 1)
            {
                this.buttonBackward.Enabled = false;
                this.buttonForward.Enabled = true;
            }
            else
            {
                this.buttonForward.Enabled = this.buttonBackward.Enabled = true;
            }
        }

        private void AddQC()
        {
            FormQCItem frm = new FormQCItem(null, Program.ConfigMgt.Config.AdditionalQueryCriteria);
            if (frm.ShowDialog(this) != DialogResult.OK) return;

            MWLQueryCriteriaItem item = frm.QCItem;
            if (item == null) return;

            Program.ConfigMgt.Config.AdditionalQueryCriteria.Add(item);

            RefreshQCList();
            SelectQCItem(item);
        }
        private void EditQC()
        {
            MWLQueryCriteriaItem item = GetSelectedQCitem();
            if (item == null) return;

            FormQCItem frm = new FormQCItem(item, Program.ConfigMgt.Config.AdditionalQueryCriteria);
            if (frm.ShowDialog(this) != DialogResult.OK) return;

            RefreshQCList();
            SelectQCItem(item);
        }
        private void DeleteQC()
        {
            MWLQueryCriteriaItem item = GetSelectedQCitem();
            if (item == null) return;

            Program.ConfigMgt.Config.AdditionalQueryCriteria.Remove(item);

            RefreshQCList();
            RefreshQCButtons();
        }
        private void MoveUpQC()
        {
            MWLQueryCriteriaItem item = GetSelectedQCitem();
            int count = Program.ConfigMgt.Config.AdditionalQueryCriteria.Count;
            int index = Program.ConfigMgt.Config.AdditionalQueryCriteria.IndexOf(item);
            if (index > 0 && index <= count - 1)
            {
                Program.ConfigMgt.Config.AdditionalQueryCriteria.Remove(item);
                Program.ConfigMgt.Config.AdditionalQueryCriteria.Insert(index - 1, item);

                RefreshQCList();
                SelectQCItem(item);
            }
        }
        private void MoveDownQC()
        {
            MWLQueryCriteriaItem item = GetSelectedQCitem();
            int count = Program.ConfigMgt.Config.AdditionalQueryCriteria.Count;
            int index = Program.ConfigMgt.Config.AdditionalQueryCriteria.IndexOf(item);
            if (index >= 0 && index < count - 1)
            {
                Program.ConfigMgt.Config.AdditionalQueryCriteria.Remove(item);
                Program.ConfigMgt.Config.AdditionalQueryCriteria.Insert(index + 1, item);

                RefreshQCList();
                SelectQCItem(item);
            }
        }

        private MWLQueryCriteriaItem GetSelectedQCitem()
        {
            if (this.listViewCriteria.SelectedItems.Count < 1) return null;
            return this.listViewCriteria.SelectedItems[0].Tag as MWLQueryCriteriaItem;
        }
        private void SelectQCItem(MWLQueryCriteriaItem item)
        {
            foreach (ListViewItem i in this.listViewCriteria.Items)
            {
                if (item == i.Tag as QueryCriteriaItem)
                {
                    i.Selected = true;
                    i.EnsureVisible();
                    break;
                }
            }
        }

        private void RefreshQCList()
        {
            bool isFirstItem = true;
            this.listViewCriteria.Items.Clear();
            foreach (QueryCriteriaItem qc in Program.ConfigMgt.Config.AdditionalQueryCriteria)
            {
                ListViewItem i = new ListViewItem(qc.GWDataDBField.ToString());
                i.SubItems.Add(qc.Operator.ToString());
                i.SubItems.Add(qc.Translating.ConstValue);
                if (isFirstItem)
                {
                    isFirstItem = false;
                }
                else
                {
                    i.SubItems.Add(qc.Type.ToString());
                }
                i.Tag = qc;

                this.listViewCriteria.Items.Add(i);
            }
        }
        private void RefreshQCButtons()
        {
            QueryCriteriaItem item = GetSelectedQCitem();
            this.buttonEditCriteria.Enabled = this.buttonDeleteCriteria.Enabled = item != null;

            int count = this.listViewCriteria.Items.Count;
            int index = this.listViewCriteria.SelectedItems.Count > 0 ? this.listViewCriteria.SelectedItems[0].Index : -1;

            this.buttonDownCriteria.Enabled = index >= 0 && index < count - 1;
            this.buttonUpCriteria.Enabled = index > 0 && index <= count - 1;
        }

        private void MoveForward()
        {
            PersonNameComponent c = this.checkedListBoxComponents.SelectedItem as PersonNameComponent;
            int index = Program.ConfigMgt.Config.PersonNameRule.Components.IndexOf(c);

            index--;
            if (index < 0) return;

            Program.ConfigMgt.Config.PersonNameRule.Components.Remove(c);
            Program.ConfigMgt.Config.PersonNameRule.Components.Insert(index, c);

            RefreshList();
            SelectItem(c);
        }
        private void MoveBackward()
        {
            PersonNameComponent c = this.checkedListBoxComponents.SelectedItem as PersonNameComponent;
            int index = Program.ConfigMgt.Config.PersonNameRule.Components.IndexOf(c);

            index++;
            if (index >= Program.ConfigMgt.Config.PersonNameRule.Components.Count) return;

            Program.ConfigMgt.Config.PersonNameRule.Components.Remove(c);
            Program.ConfigMgt.Config.PersonNameRule.Components.Insert(index, c);

            RefreshList();
            SelectItem(c);
        }
        private void SelectItem(PersonNameComponent c)
        {
            foreach (PersonNameComponent comp in this.checkedListBoxComponents.Items)
            {
                if (comp == c)
                {
                    this.checkedListBoxComponents.SelectedItem = comp;
                    break;
                }
            }
        }

        private void checkedListBoxComponents_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            PersonNameComponent c = this.checkedListBoxComponents.Items[e.Index] as PersonNameComponent;
            if (c != null) c.Enable = e.NewValue == CheckState.Checked;
        }
        private void checkedListBoxComponents_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshButtons();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            SaveSetting();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void buttonDefault_Click(object sender, EventArgs e)
        {
            Program.ConfigMgt.Config.PersonNameRule = PersonNameRule.GetDefault();
            LoadSetting();
        }
        private void buttonBackward_Click(object sender, EventArgs e)
        {
            MoveBackward();
        }
        private void buttonForward_Click(object sender, EventArgs e)
        {
            MoveForward();
        }
        private void FormQCAdvance_Load(object sender, EventArgs e)
        {
            LoadSetting();
        }

        private void buttonAddCriteria_Click(object sender, EventArgs e)
        {
            AddQC();
        }
        private void buttonEditCriteria_Click(object sender, EventArgs e)
        {
            EditQC();
        }
        private void buttonDeleteCriteria_Click(object sender, EventArgs e)
        {
            DeleteQC();
        }
        private void listViewCriteria_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshQCButtons();
        }
        private void buttonUpCriteria_Click(object sender, EventArgs e)
        {
            MoveUpQC();
        }
        private void buttonDownCriteria_Click(object sender, EventArgs e)
        {
            MoveDownQC();
        }
    }
}