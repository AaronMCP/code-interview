using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HYS.IM.Messaging.Base;
using System.Data.OleDb;

namespace HYS.IM.MessageDevices.CSBAdpater.Outbound.Forms
{
    public partial class FormConfig : Form, IConfigUI
    {
        public FormConfig()
        {
            InitializeComponent();
            LoadConfig();
        }

        #region IConfigUI Members

        public Control GetControl()
        {
            this.buttonCancel.Visible
                = this.buttonOK.Visible = false;
            return this;
        }

        public bool LoadConfig()
        {
            this.textBoxDBCnn.Text = Program.Context.ConfigMgr.Config.CSBrokerOLEDBConnectionString;
            this.textBoxSQLIn.Text = Program.Context.ConfigMgr.Config.CSBrokerPassiveSQLInboundInterfaceName;
            this.checkBoxTransform.Checked = Program.Context.ConfigMgr.Config.EnableXMLTransform;
            this.textBoxXSLTPath.Text = Program.Context.ConfigMgr.Config.XSLTFilePath;
            return true;
        }

        public bool ValidateConfig()
        {
            string dbcnn = this.textBoxDBCnn.Text.Trim();
            string sqlin = this.textBoxSQLIn.Text.Trim();
            Program.Context.ConfigMgr.Config.CSBrokerOLEDBConnectionString = dbcnn;
            Program.Context.ConfigMgr.Config.CSBrokerPassiveSQLInboundInterfaceName = sqlin;
            Program.Context.ConfigMgr.Config.EnableXMLTransform = this.checkBoxTransform.Checked;
            Program.Context.ConfigMgr.Config.XSLTFilePath = this.textBoxXSLTPath.Text.Trim();
            return true;
        }

        public string Title
        {
            get { return this.Text; }
        }

        #endregion

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (!ValidateConfig()) return;
            if (Program.Context.ConfigMgr.Save())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                Program.Context.Log.Write(Program.Context.ConfigMgr.LastError);
                MessageBox.Show(this,
                    "Save configuration file failed",
                    Program.Context.AppName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void buttonDBTest_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                string dbcnn = this.textBoxDBCnn.Text.Trim();
                using (OleDbConnection conn = new OleDbConnection(dbcnn))
                {
                    conn.Open();
                    conn.Close();
                }

                MessageBox.Show(this, "Test database connection success.",
                   this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception err)
            {
                Program.Context.Log.Write(err);
                MessageBox.Show(this, "Test database connection failed.\r\n\r\nMessage: "
                    + err.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}
