using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HYS.IM.BusinessControl;
using HYS.Common.Objects.Logging;

namespace HYS.IM.Forms
{
    public partial class FormCopyInterface : Form
    {
        public FormCopyInterface(GCInterfaceManager interfaceMgt, GCInterface fromInterface)
        {
            InitializeComponent();

            ToInterface = null;
            _interfaceMgt = interfaceMgt;
            _fromInterface = fromInterface;

            this.Text = string.Format(this.Text, _fromInterface.InterfaceName);
            _progressListener = new ProgressListener(this.labelProcess, this.progressBarProcess);
            _progressListener.AttachProgress(_interfaceMgt);
        }

        public GCInterface ToInterface;
        private GCInterface _fromInterface;
        private GCInterfaceManager _interfaceMgt;
        private ProgressListener _progressListener;

        private bool CheckInterfaceName(string name)
        {
            this.Cursor = Cursors.WaitCursor;
            bool result = _interfaceMgt.IsValidInterfaceName(name);
            this.Cursor = Cursors.Default;

            if (!result)
            {
                MessageBox.Show(this, "The interface name should only contains charactor or number or '_', and should begins with charactor, please input another name.",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.textBoxInterfaceName.Focus();
                return false;
            }

            this.Cursor = Cursors.WaitCursor;
            result = _interfaceMgt.HasSampleInterfaceName(name);
            this.Cursor = Cursors.Default;

            if (result)
            {
                MessageBox.Show(this, "The interface name \"" + name + "\" is duplicated, please input another name.",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.textBoxInterfaceName.Focus();
                return false;
            }

            return true;
        }
        private bool CopyInterface()
        {
            string name = this.textBoxInterfaceName.Text.Trim();
            if (!CheckInterfaceName(name)) return false;
            string desc = this.textBoxInterfaceDescription.Text;

            this.Cursor = Cursors.WaitCursor;
            this.buttonCancel.Enabled = this.buttonOK.Enabled = false;
            ToInterface = _interfaceMgt.CopyInterface(_fromInterface, name, desc);
            this.buttonCancel.Enabled = this.buttonOK.Enabled = true;
            this.Cursor = Cursors.Default;

            if (ToInterface == null)
            {
                Program.Log.Write(LogType.Warning, "{Interface} Copy interface failed : " + GCError.LastErrorInfor);
                if (GCError.LastError != null) Program.Log.Write(LogType.Error, GCError.LastError.ToString());
                MessageBox.Show(this, "Copy interface failed.\r\n\r\n" + GCError.LastErrorInfor,
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (CopyInterface())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
