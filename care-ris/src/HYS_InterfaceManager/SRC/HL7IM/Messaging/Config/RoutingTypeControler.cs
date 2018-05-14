using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HYS.IM.Messaging.Objects.RoutingModel;

namespace HYS.IM.Messaging.Config
{
    public class RoutingTypeControler
    {
        private ComboBox _cb;
        public RoutingTypeControler(ComboBox cb)
        {
            _cb = cb;
        }

        public void Initialize()
        {
            _cb.Items.Clear();
            _cb.DropDownStyle = ComboBoxStyle.DropDownList;
            _cb.SelectedIndexChanged += new EventHandler(_cb_SelectedIndexChanged);

            _cb.Items.Add("Message Type Based");
            _cb.Items.Add("Message Content Based");

            _cb.SelectedIndex = 0;
        }
        public RoutingRuleType GetValue()
        {
            if (_cb.SelectedIndex < 0) return RoutingRuleType.MessageType;
            return (RoutingRuleType)_cb.SelectedIndex;
        }
        public void SetValue(RoutingRuleType t)
        {
            int index = (int)t;
            if (index < _cb.Items.Count) _cb.SelectedIndex = index;
        }
        public bool Enable
        {
            get { return _cb.Enabled; }
            set { _cb.Enabled = value; }
        }

        public event RoutingTypeSelectedHanlder Selected;
        private void _cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Selected != null) Selected(this, GetValue());
        }
    }

    public delegate void  RoutingTypeSelectedHanlder(RoutingTypeControler ctrl, RoutingRuleType t);
}
