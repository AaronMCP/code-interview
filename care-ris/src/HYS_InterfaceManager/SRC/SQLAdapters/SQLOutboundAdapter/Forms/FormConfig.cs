using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SQLOutboundAdapter.Objects;
using HYS.Adapter.Base;

namespace SQLOutboundAdapter.Forms
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
                return this.panelMain;
            }
        }

        
        internal void LoadConfig()
        {
            //if (ConfigurationMgt.Load())
            //{
            //    this.textBoxSQL.Text = ConfigurationMgt.Config.SQLStatement;
            //    this.textBoxDBCnn.Text = ConfigurationMgt.Config.DBConnection;
            //    this.numericUpDownInterval.Value = ConfigurationMgt.Config.QueryInterval;
            //}
            //else
            //{
            //    Program.Log.Write(LogType.Error, "Load configuration failed.\r\n" + ConfigurationMgt.LastError);
            //    MessageBox.Show(this, "Load configuration failed.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
        }

        internal void SaveConfig()
        {
            //ConfigurationMgt.Config.SQLStatement = this.textBoxSQL.Text;
            //ConfigurationMgt.Config.DBConnection = this.textBoxDBCnn.Text;
            //ConfigurationMgt.Config.QueryInterval = (int) this.numericUpDownInterval.Value;

            //if (ConfigurationMgt.Save())
            //{
            //    this.DialogResult = DialogResult.OK;
            //    this.Close();
            //}
            //else
            //{
            //    Program.Log.Write(LogType.Error, "Save configuration failed.\r\n" + ConfigurationMgt.LastError );
            //    MessageBox.Show(this, "Save configuration failed.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
        }

        private void FormConfig_Load(object sender, EventArgs e)
        {
            LoadConfig();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            SaveConfig();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}