using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HYS.MessageDevices.MessagePipe.Channels;
using HYS.MessageDevices.MessagePipe.Base.Channel;
using HYS.MessageDevices.MessagePipe.Base;
using HYS.MessageDevices.MessagePipe.Config;

namespace HYS.MessageDevices.MessagePipe.Forms
{
    public partial class FormChannel : Form
    {
        private ChannelInstance _Channel = new ChannelInstance();
        public ChannelInstance Channel 
        { get{return _Channel;}
            set { }
        }

        public FormChannel(ChannelInstance channel)
        {
            InitializeComponent();
            LoadChannelTypes();

            _Channel.Enable = channel.Enable;
            _Channel.Name = channel.Name;
            _Channel.DeviceName = channel.DeviceName;
            _Channel.Description = channel.Description;
            _Channel.Setting = channel.Setting;
            
        }

        private void LoadChannelTypes()
        {
            this.comboBoxChnType.Items.Clear();
            foreach (string str in ChannelFactory.ChannelRegistry)
            {
                this.comboBoxChnType.Items.Add(str);
            }
            if (this.comboBoxChnType.Items.Count > 0)
                this.comboBoxChnType.SelectedIndex = 0;
        }

        private void LoadChannel()
        {
            if (_Channel != null)
            {
                if (_Channel.Name != null)
                {
                    tBoxChannelName.Text = _Channel.Name;
                }
                else
                {
                    comboBoxChnType.Enabled = false;
                }

                if (_Channel.Description != null)
                tBoxChannelDescription.Text = _Channel.Description;

                if (_Channel.DeviceName != null && !_Channel.DeviceName.Equals(""))
                {
                    comboBoxChnType.Text = _Channel.DeviceName;
                    comboBoxChnType.Enabled = false;
                }
                else
                {
                    comboBoxChnType.SelectedIndex = 0;
                    comboBoxChnType.Enabled = true;
                }

                rtBoxSetting.Clear();

                if (!_Channel.Setting.Equals(""))
                {
                    rtBoxSetting.Text = _Channel.Setting;
                }

                ckBoxChannelEnable.Checked= _Channel.Enable;                
            }
        }

        private void buttonChnSetting_Click(object sender, EventArgs e)
        {
            if (_Channel.Setting == null || _Channel.Setting.Equals(""))
            {
                CreateNewConfig();
            }
            else
            {
                EditConfig();
            }               
        }

        private void EditConfig()
        {
            if (this.comboBoxChnType.SelectedItem == null) return;
            string chnType = this.comboBoxChnType.SelectedItem.ToString();

            IChannelConfig cfg = ChannelFactory.CreateChannelConfig(chnType, Program.Log);
            if (cfg == null) return;

            ConfigurationInitializationParameter param = new ConfigurationInitializationParameter
            (Program.AppArgument.ConfigFilePath, Program.Log);
            if (!cfg.Initialize(param)) return;

            string cfgXml = _Channel.Setting;
            if (cfg.EditConfig(this, ref cfgXml))
            {
                _Channel.Setting = cfgXml;
                rtBoxSetting.Clear();
                rtBoxSetting.Text = _Channel.Setting;
            }
            
        }

        private void CreateNewConfig()
        {
            if (this.comboBoxChnType.SelectedItem == null) return;
            string chnType = this.comboBoxChnType.SelectedItem.ToString();

            IChannelConfig cfg = ChannelFactory.CreateChannelConfig(chnType, Program.Log);
            if (cfg == null) return;

            ConfigurationInitializationParameter param = new ConfigurationInitializationParameter
            (Program.AppArgument.ConfigFilePath, Program.Log);
            if (!cfg.Initialize(param)) return;

            string cfgXml = "";
            if (cfg.CreateConfig(this, out cfgXml))
            {
                _Channel.Setting = cfgXml.ToString();
                rtBoxSetting.Clear();
                rtBoxSetting.Text = _Channel.Setting;
               
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (tBoxChannelName.Text.Trim().Equals(""))
            {
                MessageBox.Show("Channel name can not be empty, please define a name for this channel.");
                tBoxChannelName.Focus();
                return;
            }
            else
            {
                if (!tBoxChannelName.Text.Trim().Equals(_Channel.Name))
                {
                    for (int i = 0; i < Program.ConfigMgr.Config.Channels.Count; i++)
                    {
                        if(tBoxChannelName.Text.Equals(Program.ConfigMgr.Config.Channels[i].Name))
                        {
                            MessageBox.Show("Channel name was used by other channel, please modify it!");
                            return;
                        }
                    }
                }
            }

            if (comboBoxChnType.Text.Trim().Equals(""))
            {
                MessageBox.Show("Please select a channel type and configure it.");
                return;
            }
                
            if(_Channel.Setting.Equals(""))
            {
                MessageBox.Show("Channel configuration is not finished, please click Channel Setting button to configure the channel.");
                return;
            }

            _Channel.Enable = ckBoxChannelEnable.Checked;
            _Channel.Name = tBoxChannelName.Text.Trim();
            _Channel.DeviceName = comboBoxChnType.SelectedItem.ToString();
            _Channel.Description = tBoxChannelDescription.Text.Trim();
                       
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("All configuration will be canceled, do you want continue?","Confirmation",MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.No;
                this.Close();
            }
        }

        private void FormChannel_Load(object sender, EventArgs e)
        {
            LoadChannel();
        }

        private void comboBoxChnType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
