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
using HYS.IM.Messaging.Objects.PublishModel;
using HYS.IM.Messaging.Objects.RequestModel;
using HYS.IM.Messaging.Mapping;
using HYS.IM.Messaging.Queuing;

namespace HYS.IM.Messaging.Config
{
    public partial class FormMain : Form
    {
        private class ResponseConfigControler : IConfigPage
        {
            private FormMain _formMain;
            private EntityConfigBase _entityConfig;
            public ResponseConfigControler(FormMain formMain)
            {
                _formMain = formMain;

                _formMain.listViewRSPChannelList.SelectedIndexChanged += new EventHandler(listViewRSPChannelList_SelectedIndexChanged);
                _formMain.listViewRSPMessageTypeList.SelectedIndexChanged += new EventHandler(listViewRSPMessageTypeList_SelectedIndexChanged);
                _formMain.buttonRSPMessageTypeAdd.Click += new EventHandler(buttonRSPMessageTypeAdd_Click);
                _formMain.buttonRSPMessageTypeEdit.Click += new EventHandler(buttonRSPMessageTypeEdit_Click);
                _formMain.buttonRSPMessageTypeDelete.Click += new EventHandler(buttonRSPMessageTypeDelete_Click);
                _formMain.buttonRSPChannelDetails.Click += new EventHandler(buttonRSPChannelDetails_Click);
                _formMain.buttonRSPChannelDelete.Click += new EventHandler(buttonRSPChannelDelete_Click);
                _formMain.buttonRSPAdvance.Click += new EventHandler(buttonRSPAdvance_Click);
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
                _formMain.listViewRSPMessageTypeList.Items.Clear();
                foreach (MessageTypePair p in _entityConfig.ResponseConfig.ResponseContract.MessageTypePairList)
                {
                    ListViewItem i = new ListViewItem(p.RequestMessageType.ToString());
                    i.SubItems.Add(p.ResponseMessageType.ToString());
                    i.SubItems.Add(p.Description);
                    i.Tag = p;

                    _formMain.listViewRSPMessageTypeList.Items.Add(i);
                }
            }
            private void RefreshMessageTypeButton()
            {
                _formMain.buttonRSPMessageTypeDelete.Enabled =
                    _formMain.buttonRSPMessageTypeEdit.Enabled = GetSelectedMessagseType() != null;
            }

            private MessageTypePair GetSelectedMessagseType()
            {
                if (_formMain.listViewRSPMessageTypeList.SelectedItems.Count < 1) return null;
                return _formMain.listViewRSPMessageTypeList.SelectedItems[0].Tag as MessageTypePair;
            }
            private void SelectMessageType(MessageTypePair t)
            {
                foreach (ListViewItem i in _formMain.listViewRSPMessageTypeList.Items)
                {
                    if (i.Tag as MessageTypePair == t)
                    {
                        i.Selected = true;
                        i.EnsureVisible();
                        break;
                    }
                }
            }

            private void AddMessageType()
            {
                FormMessageTypePair frm = new FormMessageTypePair(null);
                if (frm.ShowDialog(_formMain) != DialogResult.OK) return;

                MessageTypePair t = frm.MessageTypePair;
                if (t == null) return;

                _entityConfig.ResponseConfig.ResponseContract.MessageTypePairList.Add(t);

                RefreshMessageTypeList();
                SelectMessageType(t);
            }
            private void EditMessageType()
            {
                MessageTypePair t = GetSelectedMessagseType();
                if (t == null) return;

                FormMessageTypePair frm = new FormMessageTypePair(t);
                if (frm.ShowDialog(_formMain) != DialogResult.OK) return;

                RefreshMessageTypeList();
                SelectMessageType(t);
            }
            private void DeleteMessageType()
            {
                MessageTypePair t = GetSelectedMessagseType();
                if (t == null) return;

                _entityConfig.ResponseConfig.ResponseContract.MessageTypePairList.Remove(t);

                RefreshMessageTypeList();
                RefreshMessageTypeButton();
            }

            private void RefreshChannelList()
            {
                _formMain.listViewRSPChannelList.Items.Clear();
                foreach (PullChannelConfig c in _entityConfig.ResponseConfig.Channels)
                {
                    ListViewItem i = new ListViewItem(c.SenderEntityID.ToString());
                    i.SubItems.Add(c.SenderEntityName);
                    i.SubItems.Add(c.ProtocolType.ToString());
                    i.Tag = c;

                    _formMain.listViewRSPChannelList.Items.Add(i);
                }
            }
            private void RefreshChannelButton()
            {
                _formMain.buttonRSPChannelDetails.Enabled =
                    _formMain.buttonRSPChannelDelete.Enabled = GetSelectedChannel() != null;
            }

            private PullChannelConfig GetSelectedChannel()
            {
                if (_formMain.listViewRSPChannelList.SelectedItems.Count < 1) return null;
                return _formMain.listViewRSPChannelList.SelectedItems[0].Tag as PullChannelConfig;
            }

            private void ViewChannel()
            {
                PullChannelConfig chn = GetSelectedChannel();
                if (chn == null) return;

                FormPullChannel frm = new FormPullChannel(chn, FormPullChannel.ActionType.View);
                frm.ShowDialog(_formMain);
            }
            private void DeleteChannel()
            {
                PullChannelConfig chn = GetSelectedChannel();
                if (chn == null) return;

                _entityConfig.ResponseConfig.Channels.Remove(chn);

                RefreshChannelList();
                RefreshChannelButton();
            }

            private void OpenProcessSetting()
            {
                FormDuplexProcess frm = new FormDuplexProcess(
                    _entityConfig.ResponseConfig.ProcessConfig,
                    _formMain._agent.Config.InitializeArgument.ConfigFilePath,
                    false);
                frm.ShowDialog(_formMain);
            }

            private void listViewRSPMessageTypeList_SelectedIndexChanged(object sender, EventArgs e)
            {
                RefreshMessageTypeButton();
            }
            private void listViewRSPChannelList_SelectedIndexChanged(object sender, EventArgs e)
            {
                RefreshChannelButton();
            }
            private void buttonRSPMessageTypeAdd_Click(object sender, EventArgs e)
            {
                AddMessageType();
            }
            private void buttonRSPMessageTypeEdit_Click(object sender, EventArgs e)
            {
                EditMessageType();
            }
            private void buttonRSPMessageTypeDelete_Click(object sender, EventArgs e)
            {
                DeleteMessageType();
            }
            private void buttonRSPChannelDetails_Click(object sender, EventArgs e)
            {
                ViewChannel();
            }
            private void buttonRSPChannelDelete_Click(object sender, EventArgs e)
            {
                DeleteChannel();
            }
            private void buttonRSPAdvance_Click(object sender, EventArgs e)
            {
                OpenProcessSetting();
            }
        }
    }
}
