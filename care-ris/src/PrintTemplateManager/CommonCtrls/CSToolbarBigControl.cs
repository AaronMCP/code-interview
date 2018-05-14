using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using Telerik.WinControls.Primitives;

namespace CarestreamCommonCtrls
{
    public class CSToolbarBigControl : UserControl
    {
        const int ITEM_WIDTH = 64;
        const int ICON_WIDTH = 34;
        public  SplitContainer _splitContainer = new SplitContainer();
       // SplitContainer _splitContainer = new SplitContainer();
        FlowLayoutPanel _dropdownPanel = new FlowLayoutPanel();
        //FlowLayoutPanel _funtionButtonPanel = new FlowLayoutPanel();

        RadDropDownButton _btnDropdown = new RadDropDownButton();

        Dictionary<RadButton, System.EventHandler> _mapBtnEvents = new Dictionary<RadButton, System.EventHandler>();

        public CSToolbarBigControl()
        {
            _splitContainer.Dock = DockStyle.Fill;
            _splitContainer.IsSplitterFixed = true;
            _splitContainer.Orientation = Orientation.Vertical;
            _splitContainer.FixedPanel = FixedPanel.Panel2;
            _splitContainer.SplitterWidth = 1;
            _splitContainer.SplitterDistance = _splitContainer.Width - ITEM_WIDTH;

            this.Controls.Add(_splitContainer);

            //
            _dropdownPanel.Width = 300;
            _dropdownPanel.Height = 300;
            _dropdownPanel.BackColor = Color.Blue;
            _dropdownPanel.BringToFront();
            _dropdownPanel.FlowDirection = FlowDirection.TopDown;
            this.Controls.Add(_dropdownPanel);
            _dropdownPanel.Show();

            _btnDropdown.Dock = DockStyle.Fill;
            _btnDropdown.Text = "Others";
            _btnDropdown.TextImageRelation = TextImageRelation.ImageAboveText;
            //_btnDropdown.ButtonElement.Children[0].Visibility = ElementVisibility.Hidden;

            _splitContainer.Panel2.Controls.Add(_btnDropdown);
        }

        public RadButton AddNew(string name, string text, string imagefile)
        {
            return AddNew(name, text, imagefile, null);
        }

        public RadButton AddNew(string name, string text, string imagefile, System.EventHandler eventHander)
        {
            RadButton btn = new RadButton();

            btn.Height = this.Height - this.Margin.Bottom - this.Margin.Top;
            btn.Name = string.IsNullOrWhiteSpace(name) ? text : name;
            btn.Text = text;
            btn.TextImageRelation = TextImageRelation.ImageAboveText;
            btn.Image = new Bitmap(Image.FromFile(imagefile), ICON_WIDTH, ICON_WIDTH);
            btn.Top = 0;
            btn.Left = 0;
            btn.Width = ITEM_WIDTH;

            if (eventHander != null)
            {
                btn.Click += eventHander;
                //btn.Tag = eventHander;
                if (!_mapBtnEvents.ContainsKey(btn))
                {
                    _mapBtnEvents.Add(btn, eventHander);
                }
            }

            if (_splitContainer.Panel1.Controls.Count > 0)
                btn.Left = _splitContainer.Panel1.Controls[_splitContainer.Panel1.Controls.Count - 1].Right;

            BorderPrimitive border = btn.ButtonElement.Children[2] as BorderPrimitive;
            border.Visibility = ElementVisibility.Hidden;

            ImagePrimitive imgPM = btn.ButtonElement.Children[1].Children[0] as ImagePrimitive;
            TextPrimitive txtPM = btn.ButtonElement.Children[1].Children[1] as TextPrimitive;

            imgPM.AutoSize = false;
            imgPM.Size = new System.Drawing.Size(ITEM_WIDTH, btn.Height * 2 / 3);
            //imgPM.Size = new System.Drawing.Size(Math.Max(txtPM.Size.Width, 60), img == null ? 60 : img.Size.Height);
            //imgPM.Size = new System.Drawing.Size(Math.Max(btn.Children[1].Size.Width, 60), img.Size.Height);
            imgPM.Alignment = ContentAlignment.MiddleCenter;

            _splitContainer.Panel1.Controls.Add(btn);

            dealHiddenItems();

            return btn;
        }

        public void RemoveControl(Control ctrl)
        {
            _splitContainer.Panel1.Controls.Remove(ctrl);

            int left = 0;
            for (int i = 0; i < _splitContainer.Panel1.Controls.Count; i++)
            {
                _splitContainer.Panel1.Controls[i].Left = left;
                left = _splitContainer.Panel1.Controls[i].Right;
            }

            dealHiddenItems();
        }

        public bool ContainControl(Control ctrl)
        {
            return _splitContainer.Panel1.Controls.Contains(ctrl);
        }

        public void SetIntentVisible(RadButton btn, bool bVisible)
        {
            if (btn != null)
            {
                btn.Visible = bVisible;
                btn.Tag = bVisible ? null : "invisible";

                int left = 0;
                for (int i = 0; i < _splitContainer.Panel1.Controls.Count; i++)
                {
                    Control ctrl = _splitContainer.Panel1.Controls[i];
                    if (!isIntentInvisible(ctrl))
                    {
                        ctrl.Left = left;
                        left = ctrl.Right;
                    }
                }
            }
        }

        bool isIntentInvisible(Control ctrl)
        {
            return "invisible" == Convert.ToString(ctrl.Tag);
        }

        public void dealHiddenItems()
        {
            foreach (Control ctrl in _splitContainer.Panel1.Controls)
            {
                if (ctrl.Right > _splitContainer.Panel1.Width)
                {
                    ctrl.Visible = false;
                }
                else
                {
                    if (!isIntentInvisible(ctrl))
                        ctrl.Visible = true;
                }
            }

            _btnDropdown.Items.Clear();

            foreach (Control ctrl in _splitContainer.Panel1.Controls)
            {
                if (!ctrl.Visible && !isIntentInvisible(ctrl))
                {
                    RadMenuItem mi = new RadMenuItem(ctrl.Text);
                    mi.Enabled = ctrl.Enabled;
                    mi.Tag = ctrl.Tag;

                    RadButton btn = ctrl as RadButton;
                    if (btn != null && _mapBtnEvents.ContainsKey(btn))
                    {
                        mi.Click += _mapBtnEvents[btn];// ctrl.Tag as System.EventHandler;
                    }

                    _btnDropdown.Items.Add(mi);
                }
            }

            _btnDropdown.Enabled = _btnDropdown.Items.Count > 0;
        }

        public void ResortButtonsByName(string sortingString,ref string visibleString)
        {
            string[] arrNames = sortingString.Split(';');

            string[] arrVisibleNames = visibleString.Split(';');
            int index = 0;
            Dictionary<string, Control> map = new Dictionary<string, Control>();

            foreach (Control ctrl in _splitContainer.Panel1.Controls)
            {
                map.Add(ctrl.Name.ToUpper(), ctrl);
            }

            _splitContainer.Panel1.Controls.Clear();

            foreach (string visibletmp in arrVisibleNames)
            {
                foreach (string tmp in arrNames)
                {
                    if (tmp == visibletmp)
                    {
                        string tmpname = tmp.ToUpper();
                        if (tmpname.IndexOf(',') > 0)
                            tmpname = tmpname.Substring(0, tmpname.IndexOf(','));

                        if (map.ContainsKey(tmpname))
                        {
                            Control sub = map[tmpname];
                            sub.Left = ITEM_WIDTH * index++;
                            _splitContainer.Panel1.Controls.Add(sub);

                            map.Remove(tmpname);
                        }
                        break;
                    }
                }
            }
            foreach (string tmp in map.Keys)
            {
                Control sub = map[tmp];
                sub.Left = ITEM_WIDTH * index++;
                _splitContainer.Panel1.Controls.Add(sub);
            }

            map.Clear();
        }

        private Control getChildButtonByName(string name)
        {
            name = name.ToUpper();

            foreach (Control ctrl in _splitContainer.Panel1.Controls)
            {
                if (ctrl.Name.ToUpper() == name)
                    return ctrl;
            }

            return null;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            dealHiddenItems();
        }
    }
}
