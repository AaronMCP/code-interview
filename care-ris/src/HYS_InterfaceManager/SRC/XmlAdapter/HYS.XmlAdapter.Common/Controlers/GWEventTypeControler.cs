using System;
using System.Windows.Forms;
using System.Collections.Generic;
using HYS.Common.Objects.Rule;


namespace HYS.XmlAdapter.Common.Controlers
{
    public class GWEventTypeControler
    {
        private ComboBox _comboBox;

        public GWEventTypeControler(ComboBox comboBox)
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

            // --- 20080520 ---
            string appPath = Application.StartupPath;
            string deviceDirFile = appPath + "\\" + HYS.Common.Objects.Device.DeviceDirManager.IndexFileName;
            HYS.Common.Objects.Device.DeviceDirManager mgt = new HYS.Common.Objects.Device.DeviceDirManager(deviceDirFile);
            if (mgt.LoadDeviceDir())
            {
                foreach (GWEventType et in mgt.DeviceDirInfor.Header.EventTypes)
                {
                    _comboBox.Items.Add(new GWEventTypeWrapper(et));
                }
                
                //return;                                       // code in 20080520 (which will cause no item in the list on XMLOut config GUI)
                if (_comboBox.Items.Count > 0) return;          // code in 20081203 (however, if you want to add new event type into the list on XMLOut config GUI, you have to add event type into DeviceDir of XMLOut manually)
            }
            // ----------------

            GWEventType[] list = GWEventType.EventTypes;
            foreach (GWEventType et in list)
            {
                _comboBox.Items.Add(new GWEventTypeWrapper(et));
            }
        }

        public GWEventType Value
        {
            get
            {
                GWEventType t = _comboBox.SelectedItem as GWEventType;
                if (t == null) t = GWEventType.Empty;
                return t.Clone();
            }
            set
            {
                GWEventType t = value;
                if (t == null) t = GWEventType.Empty;
                foreach (GWEventType item in _comboBox.Items)
                {
                    if (item.Code == t.Code)
                    {
                        _comboBox.SelectedItem = item;
                        break;
                    }
                }
            }
        }

        internal class GWEventTypeWrapper : GWEventType
        {
            public GWEventTypeWrapper(GWEventType t)
            {
                this.Code = t.Code;
                this.Description = t.Description;
                this.Enable = t.Enable;
            }

            public override string ToString()
            {
                return Code + " : " + Description;
            }
        }
    }
}
