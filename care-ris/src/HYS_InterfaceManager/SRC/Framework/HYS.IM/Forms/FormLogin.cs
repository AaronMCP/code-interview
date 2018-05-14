using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.Common.Objects.License2;

namespace HYS.IM.Forms
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private GWLicense _license;
        public GWLicense License
        {
            get { return _license; }
        }

        private bool CheckPassword()
        {
            string user = this.textBoxUser.Text;
            string password = this.textBoxPassword.Text;
            if (Program.ConfigMgt.Config.LoginUser == user &&
                Program.ConfigMgt.Config.LoginPassword == password)
            {
                return true;
            }
            else
            {
                MessageBox.Show(this, "User or password is not correct.", "Login Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }

        private bool CheckLicenseDog()
        {
            GWLicenseAgent a = new GWLicenseAgent();
            _license = a.LoginGetLicenseLogout(this, Program.Log);
            return _license != null;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (!CheckPassword()) return;
            if (CheckLicenseDog())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}