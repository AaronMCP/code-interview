using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.IM.Messaging.Base.Config;

namespace HYS.IM.Messaging.Composer
{
    public partial class FormMain : Form
    {
        private void LoadWebSetting()
        {
            this.textBoxVirtualPath.Text = Program.ConfigMgt.Config.WebSetting.VirtualPathName;
            this.textBoxUserName.Text = Program.ConfigMgt.Config.WebSetting.UserName;
            this.textBoxPassword.Text = Program.ConfigMgt.Config.WebSetting.Password;
        }
        private bool SaveWebSetting()
        {
            Program.ConfigMgt.Config.WebSetting.VirtualPathName = this.textBoxVirtualPath.Text.Trim();
            Program.ConfigMgt.Config.WebSetting.UserName = this.textBoxUserName.Text.Trim();
            Program.ConfigMgt.Config.WebSetting.Password = this.textBoxPassword.Text.Trim();
            return true;
        }
    }
}
