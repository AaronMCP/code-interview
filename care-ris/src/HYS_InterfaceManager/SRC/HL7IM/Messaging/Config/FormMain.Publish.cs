using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.Common.Xml;
using HYS.IM.Common.Logging;
using HYS.IM.Messaging.Base;
using HYS.IM.Messaging.Base.Forms;
using HYS.IM.Messaging.Base.Config;
using HYS.IM.Messaging.Base.Controler;
using HYS.IM.Messaging.Objects;
using HYS.IM.Messaging.Mapping;
using HYS.IM.Messaging.Queuing;

namespace HYS.IM.Messaging.Config
{
    public partial class FormMain : Form
    {
        private class PublishConfigControler : IConfigPage
        {
            private FormMain _formMain;
            private EntityConfigBase _entityConfig;
            public PublishConfigControler(FormMain formMain)
            {
                _formMain = formMain;

                _formMain.listViewPUBChannelList.SelectedIndexChanged +=new EventHandler(listViewPUBChannelList_SelectedIndexChanged);
                _formMain.listViewPUBMessageTypeList.SelectedIndexChanged +=new EventHandler(listViewPUBMessageTypeList_SelectedIndexChanged);
                _formMain.buttonPUBMessageTypeAdd.Click +=new EventHandler(buttonPUBMessageTypeAdd_Click);
                _formMain.buttonPUBMessageTypeEdit.Click +=new EventHandler(buttonPUBMessageTypeEdit_Click);
                _formMain.buttonPUBMessageTypeDelete.Click +=new EventHandler(buttonPUBMessageTypeDelete_Click);
                _formMain.buttonPUBChannelDetails.Click +=new EventHandler(buttonPUBChannelDetails_Click);
                _formMain.buttonPUBChannelDelete.Click += new EventHandler(buttonPUBChannelDelete_Click);
                _formMain.buttonPUBAdvance.Click += new EventHandler(buttonPUBAdvance_Click);
            }

            #region IConfigPage Members

            public bool LoadSetting(EntityConfigBase cfg)
            {
                _entityConfig = cfg;

                RefreshMessageTypeList();
                RefreshMessageTypeButton();

                RefreshChannelList();
                RefreshChannelButton();

                return true;
            }

            public bool SaveSetting()
            {
                return true;
            }

            #endregion

            private void RefreshMessageTypeList()
            {
                _formMain.listViewPUBMessageTypeList.Items.Clear();
                foreach (MessageType mt in _entityConfig.PublishConfig.Publication.MessageTypeList)
                {
                    ListViewItem i = new ListViewItem(mt.CodeSystem);
                    i.SubItems.Add(mt.Code);
                    i.SubItems.Add(mt.Meaning);
                    i.Tag = mt;

                    _formMain.listViewPUBMessageTypeList.Items.Add(i);
                }
            }
            private void RefreshMessageTypeButton()
            {
                _formMain.buttonPUBMessageTypeDelete.Enabled =
                    _formMain.buttonPUBMessageTypeEdit.Enabled = GetSelectedMessagseType() != null;
            }

            private MessageType GetSelectedMessagseType()
            {
                if (_formMain.listViewPUBMessageTypeList.SelectedItems.Count < 1) return null;
                return _formMain.listViewPUBMessageTypeList.SelectedItems[0].Tag as MessageType;
            }
            private void SelectMessageType(MessageType t)
            {
                foreach (ListViewItem i in _formMain.listViewPUBMessageTypeList.Items)
                {
                    if (i.Tag as MessageType == t)
                    {
                        i.Selected = true;
                        i.EnsureVisible();
                        break;
                    }
                }
            }

            private void AddMessageType()
            {
                FormMessageType frm = new FormMessageType(null);
                if (frm.ShowDialog(_formMain) != DialogResult.OK) return;

                MessageType t = frm.MessageType;
                if (t == null) return;

                _entityConfig.PublishConfig.Publication.MessageTypeList.Add(t);

                RefreshMessageTypeList();
                SelectMessageType(t);
            }
            private void EditMessageType()
            {
                MessageType t = GetSelectedMessagseType();
                if (t == null) return;

                FormMessageType frm = new FormMessageType(t);
                if (frm.ShowDialog(_formMain) != DialogResult.OK) return;

                RefreshMessageTypeList();
                SelectMessageType(t);
            }
            private void DeleteMessageType()
            {
                MessageType t = GetSelectedMessagseType();
                if (t == null) return;

                _entityConfig.PublishConfig.Publication.MessageTypeList.Remove(t);

                RefreshMessageTypeList();
                RefreshMessageTypeButton();
            }

            private void RefreshChannelList()
            {
                _formMain.listViewPUBChannelList.Items.Clear();
                foreach (PushChannelConfig c in _entityConfig.PublishConfig.Channels)
                {
                    ListViewItem i = new ListViewItem(c.ReceiverEntityID.ToString());
                    i.SubItems.Add(c.ReceiverEntityName);
                    i.SubItems.Add(c.Subscription.Type.ToString());
                    i.SubItems.Add(c.ProtocolType.ToString());
                    i.Tag = c;

                    _formMain.listViewPUBChannelList.Items.Add(i);
                }
            }
            private void RefreshChannelButton()
            {
                _formMain.buttonPUBChannelDetails.Enabled =
                    _formMain.buttonPUBChannelDelete.Enabled = GetSelectedChannel() != null;
            }

            private PushChannelConfig GetSelectedChannel()
            {
                if (_formMain.listViewPUBChannelList.SelectedItems.Count < 1) return null;
                return _formMain.listViewPUBChannelList.SelectedItems[0].Tag as PushChannelConfig;
            }

            private void ViewChannel()
            {
                PushChannelConfig chn = GetSelectedChannel();
                if (chn == null) return;

                FormPushChannel frm = new FormPushChannel(chn, FormPushChannel.ActionType.View);
                frm.ShowDialog(_formMain);
            }
            private void DeleteChannel()
            {
                PushChannelConfig chn = GetSelectedChannel();
                if (chn == null) return;

                _entityConfig.PublishConfig.Channels.Remove(chn);

                RefreshChannelList();
                RefreshChannelButton();
            }

            private void OpenProcessSetting()
            {
                FormOneWayProcess frm = new FormOneWayProcess(
                    _entityConfig.PublishConfig.ProcessConfig,
                    _formMain._agent.Config.InitializeArgument.ConfigFilePath, 
                    false);
                frm.ShowDialog(_formMain);
            }

            private void listViewPUBMessageTypeList_SelectedIndexChanged(object sender, EventArgs e)
            {
                RefreshMessageTypeButton();
            }
            private void listViewPUBChannelList_SelectedIndexChanged(object sender, EventArgs e)
            {
                RefreshChannelButton();
            }
            private void buttonPUBMessageTypeAdd_Click(object sender, EventArgs e)
            {
                AddMessageType();
            }
            private void buttonPUBMessageTypeEdit_Click(object sender, EventArgs e)
            {
                EditMessageType();
            }
            private void buttonPUBMessageTypeDelete_Click(object sender, EventArgs e)
            {
                DeleteMessageType();
            }
            private void buttonPUBChannelDetails_Click(object sender, EventArgs e)
            {
                ViewChannel();
            }
            private void buttonPUBChannelDelete_Click(object sender, EventArgs e)
            {
                DeleteChannel();
            }
            private void buttonPUBAdvance_Click(object sender, EventArgs e)
            {
                OpenProcessSetting();
            }
        }
    }
}
