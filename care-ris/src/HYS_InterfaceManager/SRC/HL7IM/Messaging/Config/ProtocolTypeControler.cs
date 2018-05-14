using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HYS.IM.Messaging.Queuing;

namespace HYS.IM.Messaging.Config
{
    public class ProtocolTypeControler
    {
        private ComboBox _cb;
        public ProtocolTypeControler(ComboBox cb)
        {
            _cb = cb;
        }

        public void Initialize()
        {
            _cb.Items.Clear();
            _cb.DropDownStyle = ComboBoxStyle.DropDownList;
            _cb.SelectedIndexChanged += new EventHandler(_cb_SelectedIndexChanged);

            _cb.Items.Add("LPC  (call back)");
            _cb.Items.Add("MSMQ  (message queue)");
            _cb.Items.Add("RPC on Local Machine  (named pipe)");
            _cb.Items.Add("RPC on LAN/Intranet  (tcp/ip)");
            _cb.Items.Add("RPC on WAN/Internet  (soap)");

            _cb.SelectedIndex = 0;
        }
        public ProtocolType GetValue()
        {
            if (_cb.SelectedIndex < 0) return ProtocolType.LPC;
            return (ProtocolType)_cb.SelectedIndex;
        }
        public void SetValue(ProtocolType t)
        {
            int index = (int)t;
            if (index < _cb.Items.Count) _cb.SelectedIndex = index;
        }
        public bool Enable
        {
            get { return _cb.Enabled; }
            set { _cb.Enabled = value; }
        }

        public event ProtocolTypeSelectedHanlder Selected;
        private void _cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Selected != null) Selected(this, GetValue());
        }
    }

    public delegate void ProtocolTypeSelectedHanlder(ProtocolTypeControler ctrl, ProtocolType t);
}
