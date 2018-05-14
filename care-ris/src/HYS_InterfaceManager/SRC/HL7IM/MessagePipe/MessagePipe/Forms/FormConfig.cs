using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HYS.Messaging.Base;
using HYS.MessageDevices.MessagePipe.Config;
using HYS.MessageDevices.MessagePipe.Channels;

namespace HYS.MessageDevices.MessagePipe.Forms
{
    public partial class FormConfig : Form, IConfigUI
    {
        private MessagePipeConfig _config = new MessagePipeConfig();
        public FormConfig()
        {
            InitializeComponent();         
           
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
            if (Program.ConfigMgr.Config != null)
            {
                labGUID.Text = Program.ConfigMgr.Config.EntityID.ToString();
                labDesc.Text = Program.ConfigMgr.Config.Description;

                
                btnEditChannel.Enabled = false;
                btnDeleteChannel.Enabled = false;
                btnAddChannel.Enabled = true;
              
                lViewChannels.Items.Clear();

                if (Program.ConfigMgr.Config.Channels.Count > 0)
                {
                    for (int i = 0; i < Program.ConfigMgr.Config.Channels.Count; i++)
                    {
                        ListViewItem lvi = new ListViewItem((i + 1).ToString());

                        lvi.SubItems.Add(Program.ConfigMgr.Config.Channels[i].Name);
                        lvi.SubItems.Add(Program.ConfigMgr.Config.Channels[i].DeviceName);
                        lvi.SubItems.Add(Program.ConfigMgr.Config.Channels[i].Description);
                        lvi.SubItems.Add(Program.ConfigMgr.Config.Channels[i].Enable.ToString());

                        lViewChannels.Items.Add(lvi);
                    }
                }
                else
                {                    
                    lViewChannels.Enabled = false;                    
                }
                
            }
            return true;
        }

        public bool ValidateConfig()
        {
            string str = " ";
            if (str.Length > 0)
            {
                //Program.ConfigMgr.Config.MySetting = str;
            }
            else
            {
                MessageBox.Show(this,
                    "Please input the value of My Setting.",
                    Program.AppName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return false;
            }

            return true;
        }

        public string Title
        {
            get { return this.Text; }
        }

        #endregion

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (!ValidateConfig()) return;
            if (Program.ConfigMgr.Save())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(this,
                    "Save configuration file failed",
                    Program.AppName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }
       
        private void btnAddChannel_Click(object sender, EventArgs e)
        {
            ChannelInstance chi = new ChannelInstance();
            chi.Name = "";
            chi.Description = "";
            chi.DeviceName = "";
            chi.Setting = "";

            FormChannel frm = new FormChannel(chi);
            //frm.Channel = chi;
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                Program.ConfigMgr.Config.Channels.Add(frm.Channel);

                ListViewItem lvi = new ListViewItem((lViewChannels.Items.Count+1).ToString());
                lvi.SubItems.Add(frm.Channel.Name);
                lvi.SubItems.Add(frm.Channel.DeviceName);
                lvi.SubItems.Add(frm.Channel.Description);
                lvi.SubItems.Add(frm.Channel.Enable.ToString());
                lViewChannels.Items.Add(lvi);
            }
        }

        private void lViewChannels_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lViewChannels.SelectedItems.Count > 0)
            {
                btnAddChannel.Enabled = true;
                btnEditChannel.Enabled = true;
                btnDeleteChannel.Enabled = true;
            }
            else
            {
                btnDeleteChannel.Enabled = false;
                btnEditChannel.Enabled = false;
            }
        }

        private void btnEditChannel_Click(object sender, EventArgs e)
        {
            if (lViewChannels.SelectedItems.Count<1)
            {
                btnDeleteChannel.Enabled = false;
                btnEditChannel.Enabled = false;
                return;
            }

            if (!lViewChannels.SelectedItems[0].SubItems[1].ToString().Trim().Equals(""))
            {
                int ChannelIndex = lViewChannels.Items.IndexOf(lViewChannels.SelectedItems[0]);
                FormChannel frm = new FormChannel(Program.ConfigMgr.Config.Channels[ChannelIndex]);
                //frm.Channel = Program.ConfigMgr.Config.Channels[ChannelIndex];

                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    Program.ConfigMgr.Config.Channels[ChannelIndex].Enable = frm.Channel.Enable;
                    Program.ConfigMgr.Config.Channels[ChannelIndex].Name = frm.Channel.Name;
                    Program.ConfigMgr.Config.Channels[ChannelIndex].DeviceName = frm.Channel.DeviceName;
                    Program.ConfigMgr.Config.Channels[ChannelIndex].Description = frm.Channel.Description;
                    Program.ConfigMgr.Config.Channels[ChannelIndex].Setting = frm.Channel.Setting;

                    lViewChannels.Items[ChannelIndex].SubItems[1].Text = frm.Channel.Name;
                    lViewChannels.Items[ChannelIndex].SubItems[2].Text = frm.Channel.DeviceName;
                    lViewChannels.Items[ChannelIndex].SubItems[3].Text = frm.Channel.Description;
                    lViewChannels.Items[ChannelIndex].SubItems[4].Text = frm.Channel.Enable.ToString();
                }
            }
        }

        private void btnDeleteChannel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("The selected channel will be deleted, continue?","Warning",MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                int i = lViewChannels.Items.IndexOf(lViewChannels.SelectedItems[0]);

                Program.ConfigMgr.Config.Channels.Remove(Program.ConfigMgr.Config.Channels[i]);
                lViewChannels.Items.Remove(lViewChannels.Items[i]);

                if (lViewChannels.Items.Count<1)
                {
                    btnEditChannel.Enabled = false;
                    btnDeleteChannel.Enabled = false;
                }
            }
        }

        private void FormConfig_Load(object sender, EventArgs e)
        {
            LoadConfig();
        }

       
    }
}
