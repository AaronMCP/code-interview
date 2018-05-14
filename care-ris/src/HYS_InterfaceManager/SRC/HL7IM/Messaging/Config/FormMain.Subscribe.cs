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
        private class SubscribeConfigControler : IConfigPage
        {
            private FormMain _formMain;
            private EntityConfigBase _entityConfig;
            public SubscribeConfigControler(FormMain formMain)
            {
                _formMain = formMain;

                _formMain.buttonSUBChannelAdd.Click += new EventHandler(buttonSUBChannelAdd_Click);
                _formMain.buttonSUBChannelEdit.Click += new EventHandler(buttonSUBChannelEdit_Click);
                _formMain.buttonSUBChannelDelete.Click += new EventHandler(buttonSUBChannelDelete_Click);
                _formMain.listViewSUBChannelList.SelectedIndexChanged += new EventHandler(listViewSUBChannelList_SelectedIndexChanged);
                _formMain.buttonSUBAdvance.Click += new EventHandler(buttonSUBAdvance_Click);
            }

            #region IConfigPage Members

            public bool LoadSetting(EntityConfigBase cfg)
            {
                _entityConfig = cfg;

                RefreshChannelList();
                RefreshChannelButton();

                return true;
            }

            public bool SaveSetting()
            {
                return true;
            }

            #endregion

            private void RefreshChannelList()
            {
                _formMain.listViewSUBChannelList.Items.Clear();
                foreach (PushChannelConfig c in _entityConfig.SubscribeConfig.Channels)
                {
                    ListViewItem i = new ListViewItem(c.SenderEntityID.ToString());
                    i.SubItems.Add(c.SenderEntityName);
                    i.SubItems.Add(c.Subscription.Type.ToString());
                    i.SubItems.Add(c.ProtocolType.ToString());
                    i.Tag = c;

                    _formMain.listViewSUBChannelList.Items.Add(i);
                }
            }
            private void RefreshChannelButton()
            {
                _formMain.buttonSUBChannelDelete.Enabled =
                    _formMain.buttonSUBChannelEdit.Enabled = GetSelectedChannel() != null;
            }

            private PushChannelConfig GetSelectedChannel()
            {
                if (_formMain.listViewSUBChannelList.SelectedItems.Count < 1) return null;
                return _formMain.listViewSUBChannelList.SelectedItems[0].Tag as PushChannelConfig;
            }
            private void SelectChannel(PushChannelConfig t)
            {
                foreach (ListViewItem i in _formMain.listViewSUBChannelList.Items)
                {
                    if (i.Tag as PushChannelConfig == t)
                    {
                        i.Selected = true;
                        i.EnsureVisible();
                        break;
                    }
                }
            }

            private void AddChannel()
            {
                PushChannelConfig c = new PushChannelConfig();
                c.ReceiverEntityID = _entityConfig.EntityID;
                c.ReceiverEntityName = _entityConfig.Name;

                FormPushChannel frm = new FormPushChannel(c, FormPushChannel.ActionType.Add, _entityConfig.SubscribeConfig.Channels);
                if (frm.ShowDialog(_formMain) != DialogResult.OK) return;

                PushChannelConfig t = frm.ChannelConfig;
                if (t == null) return;

                _entityConfig.SubscribeConfig.Channels.Add(t);

                RefreshChannelList();
                SelectChannel(t);
            }
            private void EditChannel()
            {
                PushChannelConfig t = GetSelectedChannel();
                if (t == null) return;

                FormPushChannel frm = new FormPushChannel(t, FormPushChannel.ActionType.Edit, _entityConfig.SubscribeConfig.Channels);
                if (frm.ShowDialog(_formMain) != DialogResult.OK) return;

                RefreshChannelList();
                SelectChannel(t);
            }
            private void DeleteChannel()
            {
                PushChannelConfig t = GetSelectedChannel();
                if (t == null) return;

                _entityConfig.SubscribeConfig.Channels.Remove(t);

                RefreshChannelList();
                RefreshChannelButton();
            }

            private void OpenProcessSetting()
            {
                FormOneWayProcess frm = new FormOneWayProcess(
                    _entityConfig.SubscribeConfig.ProcessConfig,
                    _formMain._agent.Config.InitializeArgument.ConfigFilePath,
                    true);
                frm.ShowDialog(_formMain);
            }

            private void buttonSUBChannelAdd_Click(object sender, EventArgs e)
            {
                AddChannel();
            }
            private void buttonSUBChannelEdit_Click(object sender, EventArgs e)
            {
                EditChannel();
            }
            private void buttonSUBChannelDelete_Click(object sender, EventArgs e)
            {
                DeleteChannel();
            }
            private void listViewSUBChannelList_SelectedIndexChanged(object sender, EventArgs e)
            {
                RefreshChannelButton();
            }
            private void buttonSUBAdvance_Click(object sender, EventArgs e)
            {
                OpenProcessSetting();
            }
        }
    }
}
