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
        private class RequestConfigControler : IConfigPage
        {
            private FormMain _formMain;
            private EntityConfigBase _entityConfig;
            public RequestConfigControler(FormMain formMain)
            {
                _formMain = formMain;

                _formMain.buttonRQChannelAdd.Click += new EventHandler(buttonRQChannelAdd_Click);
                _formMain.buttonRQChannelEdit.Click += new EventHandler(buttonRQChannelEdit_Click);
                _formMain.buttonRQChannelDelete.Click += new EventHandler(buttonRQChannelDelete_Click);
                _formMain.listViewRQChannelList.SelectedIndexChanged += new EventHandler(listViewRQChannelList_SelectedIndexChanged);
                _formMain.buttonRQAdvance.Click += new EventHandler(buttonRQAdvance_Click);
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
                _formMain.listViewRQChannelList.Items.Clear();
                foreach (PullChannelConfig c in _entityConfig.RequestConfig.Channels)
                {
                    ListViewItem i = new ListViewItem(c.ReceiverEntityID.ToString());
                    i.SubItems.Add(c.ReceiverEntityName);
                    i.SubItems.Add(c.ProtocolType.ToString());
                    i.Tag = c;

                    _formMain.listViewRQChannelList.Items.Add(i);
                }
            }
            private void RefreshChannelButton()
            {
                _formMain.buttonRQChannelDelete.Enabled =
                    _formMain.buttonRQChannelEdit.Enabled = GetSelectedChannel() != null;
            }

            private PullChannelConfig GetSelectedChannel()
            {
                if (_formMain.listViewRQChannelList.SelectedItems.Count < 1) return null;
                return _formMain.listViewRQChannelList.SelectedItems[0].Tag as PullChannelConfig;
            }
            private void SelectChannel(PullChannelConfig t)
            {
                foreach (ListViewItem i in _formMain.listViewRQChannelList.Items)
                {
                    if (i.Tag as PullChannelConfig == t)
                    {
                        i.Selected = true;
                        i.EnsureVisible();
                        break;
                    }
                }
            }

            private void AddChannel()
            {
                PullChannelConfig c = new PullChannelConfig();
                c.SenderEntityID = _entityConfig.EntityID;
                c.SenderEntityName = _entityConfig.Name;

                FormPullChannel frm = new FormPullChannel(c, FormPullChannel.ActionType.Add, _entityConfig.RequestConfig.Channels);
                if (frm.ShowDialog(_formMain) != DialogResult.OK) return;

                PullChannelConfig t = frm.ChannelConfig;
                if (t == null) return;

                _entityConfig.RequestConfig.Channels.Add(t);

                RefreshChannelList();
                SelectChannel(t);
            }
            private void EditChannel()
            {
                PullChannelConfig t = GetSelectedChannel();
                if (t == null) return;

                FormPullChannel frm = new FormPullChannel(t, FormPullChannel.ActionType.Edit, _entityConfig.RequestConfig.Channels);
                if (frm.ShowDialog(_formMain) != DialogResult.OK) return;

                RefreshChannelList();
                SelectChannel(t);
            }
            private void DeleteChannel()
            {
                PullChannelConfig t = GetSelectedChannel();
                if (t == null) return;

                _entityConfig.RequestConfig.Channels.Remove(t);

                RefreshChannelList();
                RefreshChannelButton();
            }

            private void OpenProcessSetting()
            {
                FormDuplexProcess frm = new FormDuplexProcess(
                    _entityConfig.RequestConfig.ProcessConfig,
                    _formMain._agent.Config.InitializeArgument.ConfigFilePath,
                    true);
                frm.ShowDialog(_formMain);
            }

            private void buttonRQChannelAdd_Click(object sender, EventArgs e)
            {
                AddChannel();
            }
            private void buttonRQChannelEdit_Click(object sender, EventArgs e)
            {
                EditChannel();
            }
            private void buttonRQChannelDelete_Click(object sender, EventArgs e)
            {
                DeleteChannel();
            }
            private void listViewRQChannelList_SelectedIndexChanged(object sender, EventArgs e)
            {
                RefreshChannelButton();
            }
            private void buttonRQAdvance_Click(object sender, EventArgs e)
            {
                OpenProcessSetting();
            }
        }
    }
}
