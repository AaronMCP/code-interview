using System;
using System.Windows.Forms;
using System.Collections.Generic;
using HYS.XmlAdapter.Common.Objects;

namespace HYS.XmlAdapter.Common.Controlers
{
    public class HL7EventTypeControler
    {
        private ComboBox _comboBox;

        public HL7EventTypeControler(ComboBox comboBox)
        {
            _comboBox = comboBox;
            Initailize();
            _comboBox.SelectedIndexChanged += new EventHandler(_comboBox_SelectedIndexChanged);
        }

        public event EventHandler OnSelectChanged;

        private void _comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OnSelectChanged != null) OnSelectChanged(sender, e);
        }

        private void Initailize()
        {
            _comboBox.Items.Clear();
            List<HL7EventType> list = HL7EventType.GetEventTypes();
            foreach (HL7EventType et in list)
            {
                _comboBox.Items.Add(et);
            }
        }

        public HL7EventType Value
        {
            get
            {
                HL7EventType t = _comboBox.SelectedItem as HL7EventType;
                if (t == null) t = HL7EventType.Empty;
                return t.Clone();
            }
            set
            {
                HL7EventType t = value;
                if( t == null ) t = HL7EventType.Empty;
                foreach (HL7EventType item in _comboBox.Items)
                {
                    if (item.Name == t.Name && item.Qualifier == t.Qualifier)
                    {
                        _comboBox.SelectedItem = item;
                        break;
                    }
                }
            }
        }
    }
}
