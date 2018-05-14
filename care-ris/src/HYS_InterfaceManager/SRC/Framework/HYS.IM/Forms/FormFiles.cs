using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.IM.BusinessEntity;
using HYS.IM.BusinessControl;
using HYS.Adapter.Base;
using HYS.Common.Objects.Logging;

namespace HYS.IM.Forms
{
    public partial class FormFiles : Form
    {
        private string _path;
        private string[] _files;
        private FileOperator _operator;
        private GCInterface _gcInterface;
        private ProgressListener _listener;
        private GCInterfaceManager _interfaceManager;

        public FormFiles( FileOperator op, GCInterface gcInterface, string[] files, string path )
        {
            _path = path;
            _files = files;
            _operator = op;
            _gcInterface = gcInterface;
            
            InitializeComponent();
            InitializeUI();
        }

        private void CopyFiles()
        {
            bool res = false;
            switch (_operator)
            {
                case FileOperator.Export:
                    res = _interfaceManager.ExportConfig(_gcInterface, _files, _path);
                    if (!res)
                    {
                        Program.Log.Write(LogType.Warning, "{Interface} Export interface configuration failed : " + GCError.LastErrorInfor);
                        if (GCError.LastError != null) Program.Log.Write(LogType.Error, GCError.LastError.ToString());

                        MessageBox.Show(this, "Export interface configuration failed.\r\n\r\n" + GCError.LastErrorInfor,
                            "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    break;
                case FileOperator.Import:
                    res = _interfaceManager.ImportConfig(_files, _path);
                    if (!res)
                    {
                        Program.Log.Write(LogType.Warning, "{Interface} Import interface configuration failed : " + GCError.LastErrorInfor);
                        if (GCError.LastError != null) Program.Log.Write(LogType.Error, GCError.LastError.ToString());

                        MessageBox.Show(this, "Import interface configuration failed.\r\n\r\n" + GCError.LastErrorInfor,
                            "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    break;
            }
            if (res)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        private void InitializeUI()
        {
            switch (_operator)
            {
                case FileOperator.Export:
                    this.Text = "Export Configuration";
                    this.labelTitle.Text = "Following files will be copied to folder " + _path
                        + ". Please click \"OK\" button to continue.";
                    break;
                case FileOperator.Import:
                    this.Text = "Import Configuration";
                    this.labelTitle.Text = "Following files will be copied from folder " + _path
                        + ". Please click \"OK\" button to continue.";
                    break;
            }

            this.listBoxFiles.Items.Clear();
            foreach (string f in _files)
            {
                this.listBoxFiles.Items.Add(f);
            }

            _interfaceManager = new GCInterfaceManager(Program.ConfigDB, Program.ConfigMgt.Config.InterfaceFolder);
            _listener = new ProgressListener(this.labelHide, this.progressBarMain);
            _listener.AttachProgress(_interfaceManager);
        }
        private void buttonOK_Click(object sender, EventArgs e)
        {
            CopyFiles();
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}