using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;

namespace HYS.IM.UIControl
{
    public class DPanel : Panel
    {
        public DPanel(DPanelButton[] buttons)
        {
            _buttons = buttons;
            _title = new DPanelTitle();
            if (_buttons == null) throw(new ArgumentNullException());
            InitializeControls();
            RefreshPanel();
        }
        public DPanel(DPanelTitle title, DPanelButton[] buttons)
        {
            _title = title;
            _buttons = buttons;
            if (_title == null) throw (new ArgumentNullException());
            if (_buttons == null) throw (new ArgumentNullException());
            InitializeControls();
            RefreshPanel();
        }
        
        private DPanelTitle _title;
        public DPanelTitle Title
        {
            get { return _title; }
        }

        private DPanelButton[] _buttons;
        public DPanelButton[] Buttons
        {
            get { return _buttons; }
        }

        private int _titleHeight = 23;
        public int TitleHeight
        {
            get { return _titleHeight; }
            set 
            {
                _titleHeight = value;
                if (_titleHeight < 1) _titleHeight = 1;
                RefreshControls();
            }
        }

        private int _buttonHeight = 72;
        public int ButtonHeight
        {
            get { return _buttonHeight; }
            set 
            {
                _buttonHeight = value;
                if (_buttonHeight < 1) _buttonHeight = 1;
                RefreshControls();
            }
        }

        private void RefreshControls()
        {
            this.SuspendLayout();

            _title.Height = _titleHeight;
            _title.Dock = DockStyle.Top;

            int buttonAreaWidth = this.ClientSize.Width;
            int buttonAreaHeight = this.ClientSize.Height - _titleHeight;
            int buttonVisibleCount = buttonAreaHeight / _buttonHeight;

            int index = 0;
            foreach (DPanelButton btn in _buttons)
            {
                btn.Visible = index < buttonVisibleCount;
                btn.Top = index * _buttonHeight + _titleHeight;
                btn.Width = buttonAreaWidth;
                btn.Height = _buttonHeight;
                btn.Left = 0;
                index++;
            }

            this.ResumeLayout();
        }
        private void InitializeControls()
        {
            this.Controls.Add(_title);
            this.Controls.AddRange(_buttons);
            
            _title.Expand = true;
            _title.Click += new EventHandler(Title_Click);
        }

        private void Title_Click(object sender, EventArgs e)
        {
            DPanelTitle title = sender as DPanelTitle;
            if (title != null) NotifyExpanded(title.Expand);
        }
        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            RefreshControls();
        }

        private void NotifyExpanded(bool expand)
        {
            RefreshPanel();
            if (Expanded != null) Expanded(this, expand);
        }
        public event PanelExpandHandler Expanded;

        private void RefreshPanel()
        {
            if (Title.Expand)
            {
                this.Height = this.TitleHeight + this.Buttons.Length * this.ButtonHeight;
            }
            else
            {
                this.Height = this.TitleHeight;
            }
        }
        public void Collapse()
        {
            Title.Expand = false;
            NotifyExpanded(false);
        }
        public void Expand()
        {
            Title.Expand = true;
            NotifyExpanded(true);
        }
    }

    public delegate void PanelExpandHandler(DPanel sender, bool expand);
}
