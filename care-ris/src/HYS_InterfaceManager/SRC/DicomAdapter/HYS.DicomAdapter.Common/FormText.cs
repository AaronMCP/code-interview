using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HYS.Common.Objects.Logging;
using System.IO;

namespace HYS.DicomAdapter.Common
{
    public partial class FormText : Form
    {
        private ILogging _log;
        public string FileName;

        public FormText(string utf8FileName, ILogging log) :
            this(utf8FileName, Path.GetFileName(utf8FileName), log)
        {
        }
        public FormText(string utf8FileName, string formTitle, ILogging log)
        {
            InitializeComponent();

            FileName = utf8FileName;
            Text = formTitle;
            _log = log;
        }

        private bool LoadFile()
        {
            try
            {
                this.textBoxMain.Text = File.ReadAllText(FileName, Encoding.UTF8);
                return true;
            }
            catch (Exception err)
            {
                _log.Write(err);
                MessageBox.Show(this, string.Format("Open file failed from:{0}\r\nReason:{1}",
                    FileName, err.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }
        private bool SaveFile()
        {
            try
            {
                File.WriteAllText(FileName, this.textBoxMain.Text, Encoding.UTF8);
                return true;
            }
            catch (Exception err)
            {
                _log.Write(err);
                MessageBox.Show(this, string.Format("Open file failed from:{0}\r\nReason:{1}",
                    FileName, err.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        private void FormText_Load(object sender, EventArgs e)
        {
            if (!LoadFile()) this.Close();
        }
        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (SaveFile())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
