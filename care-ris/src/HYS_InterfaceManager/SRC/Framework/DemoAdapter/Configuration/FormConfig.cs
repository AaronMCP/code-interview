using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DemoAdapter.Configuration
{
    public partial class FormConfig : Form
    {
        public FormConfig()
        {
            InitializeComponent();
        }

        internal Panel ConfigPanel
        {
            get
            {
                return this.panelConfig;
            }
        }
        
        internal void LoadConfig()
        {
            this.textBoxFileName.Text = Properties.Settings.Default.FileName;
            this.numericUpDownInterval.Value = Properties.Settings.Default.TimerInterval;
        }

        internal void SaveConfig()
        {
            Properties.Settings.Default.FileName = this.textBoxFileName.Text;
            Properties.Settings.Default.TimerInterval = (int)this.numericUpDownInterval.Value;
            Properties.Settings.Default.Save();
        }

        private void FormConfig_Load(object sender, EventArgs e)
        {
            LoadConfig();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            SaveConfig();
            this.Close();
        }
    }
}