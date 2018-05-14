using System;
using System.Data.OleDb;
using System.Windows.Forms;
using HYS.IM.Messaging.Base;

namespace HYS.IM.MessageDevices.CSBAdapter.Inbound.Forms
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
            this.textBoxDBCnn.Text = Program.Context.ConfigMgr.Config.CSBrokerConnectionString;
            this.textBoxSQLOut.Text = Program.Context.ConfigMgr.Config.CSBrokerSQLOutboundName;
            this.textBoxTimeInterval.Value = (Decimal)Program.Context.ConfigMgr.Config.TimerInterval;

            return true;
        }

        public bool ValidateConfig()
        {
            if (string.IsNullOrEmpty(textBoxSQLOut.Text.Trim()))
            {
                MessageBox.Show("Please input SQL Outbound interface name.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.textBoxSQLOut.Focus();
                return false;
            }

            Program.Context.ConfigMgr.Config.TimerInterval = (double)textBoxTimeInterval.Value;
            Program.Context.ConfigMgr.Config.CSBrokerSQLOutboundName = this.textBoxSQLOut.Text;
            Program.Context.ConfigMgr.Config.CSBrokerConnectionString = this.textBoxDBCnn.Text;

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

        private void buttonDispatch_Click(object sender, EventArgs e)
        {
            FormConfigDispatch frm = new FormConfigDispatch(Program.Context.ConfigMgr.Config.MessageDispatchConfig);
            frm.ShowDialog(this);
        }

    }
}
