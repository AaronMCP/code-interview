using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HYS.IM.Forms
{
    public partial class FormPWD : Form
    {
        public FormPWD()
        {
            InitializeComponent();
        }

        private void FormPWD_Load(object sender, EventArgs e)
        {
            this.textBoxUser.Text = Program.ConfigMgt.Config.LoginUser;
            //this.textBoxPWD.Text = Program.ConfigMgt.Config.LoginPassword;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            //Program.ConfigMgt.Config.LoginUser = this.textBoxUser.Text;
            //Program.ConfigMgt.Config.LoginPassword = this.textBoxPWD.Text;

            //if (Program.ConfigMgt.Config.LoginUser.Length < 1 ||
            //    Program.ConfigMgt.Config.LoginPassword.Length < 1)
            //{
            //    MessageBox.Show(this, "Not allow empty user or empty password.", "Change Password",
            //        MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}

            string user = this.textBoxUser.Text;
            string oldPwd = this.textBoxPWD.Text;
            string newPwd = this.textBoxNewPWD.Text;
            string confirmNewPwd = this.textBoxConfirmNewPWD.Text;

            if (user.Length < 1 ||
                oldPwd.Length < 1 ||
                newPwd.Length < 1 ||
                confirmNewPwd.Length < 1)
            {
                MessageBox.Show(this, "Not allow empty user or empty password.", "Change Password",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (oldPwd != Program.ConfigMgt.Config.LoginPassword)
            {
                MessageBox.Show(this, "Old Password is incorrect.", "Change Password",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (newPwd != confirmNewPwd)
            {
                MessageBox.Show(this, "New Password and Confirm New Password should be the same.", "Change Password",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Program.ConfigMgt.Config.LoginUser = user;
            Program.ConfigMgt.Config.LoginPassword = newPwd;

            if (Program.ConfigMgt.Save())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(this, "Write to configuration file failed.", "Change Password",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}