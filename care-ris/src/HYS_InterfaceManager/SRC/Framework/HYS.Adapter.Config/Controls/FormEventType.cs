using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.Common.Objects.Rule;

namespace HYS.Adapter.Config.Controls
{
    public partial class FormEventType : Form
    {
        public FormEventType()
        {
            InitializeComponent();
        }

        private GWEventType _eventType;
        public GWEventType EventType
        {
            get { return _eventType; }
        }

        private static bool NumericEqual(string str1, string str2)
        {
            try
            {
                int num1 = (str1.Trim() == "0") ? 0 : int.Parse(str1.Trim().TrimStart('0'));
                int num2 = (str2.Trim() == "0") ? 0 : int.Parse(str2.Trim().TrimStart('0'));
                return num1 == num2;
            }
            catch
            {
                return false;
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            _eventType = new GWEventType(this.textBoxCode.Text.Trim(), this.textBoxDescription.Text.Trim());
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void FormEventType_Load(object sender, EventArgs e)
        {
            foreach (GWEventType et in GWEventType.EventTypes)
            {
                ListViewItem i = this.listViewEventTypes.Items.Add(et.Code);
                i.SubItems.Add(et.Description);
                i.Tag = et;
            }
        }

        private void textBoxCode_TextChanged(object sender, EventArgs e)
        {
            this.buttonOK.Enabled =
                this.textBoxCode.Text.Trim().Length > 0 && this.textBoxDescription.Text.Trim().Length > 0;
        }

        private void listViewEventTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listViewEventTypes.SelectedItems.Count < 1) return;
            GWEventType et = this.listViewEventTypes.SelectedItems[0].Tag as GWEventType;
            if (et == null) return;

            this.textBoxCode.Text = et.Code;
            this.textBoxDescription.Text = et.Description;
        }

        private void textBoxCode_Leave(object sender, EventArgs e)
        {
            string code = this.textBoxCode.Text.Trim();
            if (code.Length < 1) return;

            foreach (GWEventType et in GWEventType.EventTypes)
            {
                if (NumericEqual(et.Code, code))
                {
                    this.textBoxDescription.Text = et.Description;
                    break;
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}