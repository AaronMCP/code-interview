using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace CarestreamCommonCtrls
{
    public partial class CSColorSelectForm : Telerik.WinControls.UI.RadForm
    {
        private Color _selectedColor = Color.Black;
        public Color SelectedColor
        {
            get { return _selectedColor; }
            set { _selectedColor = value; }
        }

        public CSColorSelectForm()
        {
            InitializeComponent();
        }

        private void CSColorSelectForm_Load(object sender, EventArgs e)
        {
            RadColorSelector cs = new RadColorSelector();
            this.Controls.Add(cs);
            cs.BorderStyle = BorderStyle.Fixed3D;
            cs.BackColor = Color.Transparent;
            cs.SelectedColor = _selectedColor;
            cs.Dock = DockStyle.Fill;
            cs.OkButtonClicked += new ColorChangedEventHandler(_cs_OkButtonClicked);
            cs.CancelButtonClicked += new ColorChangedEventHandler(_cs_CancelButtonClicked);

        }

        void _cs_CancelButtonClicked(object sender, ColorChangedEventArgs args)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        void _cs_OkButtonClicked(object sender, ColorChangedEventArgs args)
        {
            _selectedColor = args.SelectedColor;
            this.DialogResult = DialogResult.OK;
        }
    }
}
