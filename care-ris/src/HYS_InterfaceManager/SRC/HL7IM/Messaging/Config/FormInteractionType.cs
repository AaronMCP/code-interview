using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.IM.Messaging.Base;

namespace HYS.IM.Messaging.Config
{
    public partial class FormInteractionType : Form
    {
        public FormInteractionType(InteractionTypes types)
        {
            InitializeComponent();

            _types = types;
        }

        private InteractionTypes _types;
        public InteractionTypes Types
        {
            get { return _types; }
            set { _types = value; }
        }

        private void InitializeCheckBoxs()
        {
            this.checkedListBoxTypes.Items.Clear();
            Array olist = Enum.GetValues(typeof(InteractionTypes));
            foreach (object o in olist)
            {
                if ((InteractionTypes)o == InteractionTypes.Unknown) continue;
                this.checkedListBoxTypes.Items.Add(o);
            }
        }

        private void LoadSetting()
        {
            InitializeCheckBoxs();

            for(int i=0; i<this.checkedListBoxTypes.Items.Count; i++)
            {
                InteractionTypes t = (InteractionTypes)this.checkedListBoxTypes.Items[i];
                if ((t & _types) == t)
                {
                    this.checkedListBoxTypes.SetItemChecked(i, true);
                }
                else
                {
                    this.checkedListBoxTypes.SetItemChecked(i, false);
                }
            }
        }

        private void SaveSetting()
        {
            InteractionTypes type = InteractionTypes.Unknown;
            foreach (object o in this.checkedListBoxTypes.CheckedItems)
            {
                InteractionTypes t = (InteractionTypes)o;
                type = (type | t);
            }
            _types = type;
        }

        private void FormInteractionType_Load(object sender, EventArgs e)
        {
            LoadSetting();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            SaveSetting();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}