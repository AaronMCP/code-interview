using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;
using HYS.Common.Objects.Config;
using HYS.Common.Objects.Device;

namespace HYS.Adapter.Composer.Forms
{
    public partial class FormDevice : Form
    {
        private DeviceDirManager _dirMgt;

        public FormDevice()
        {
            InitializeComponent();

            _dirMgt = new DeviceDirManager();
            string filename = Program.ConfigMgt.Config.DeviceDirFileName;
            this.textBoxLocation.Text = ConfigHelper.GetFullPath(filename);
        }

        private void LoadFile()
        {
            _dirMgt.FileName = this.textBoxLocation.Text;
            if (_dirMgt.LoadDeviceDir())
            {
                RefreshDevice();
            }
            else
            {
                MessageBox.Show(this, "Load DeviceDir file failed.\r\n\r\n" + _dirMgt.LastErrorInfor, "Error");
            }
        }

        private void SaveFile()
        {
            _dirMgt.FileName = this.textBoxLocation.Text;
            if (!_dirMgt.SaveDeviceDir())
            {
                MessageBox.Show(this, "Save DeviceDir file failed.\r\n\r\n" + _dirMgt.LastErrorInfor, "Error");
            }
        }

        private void CheckFiles()
        {
            List<string> outsideFileList = new List<string>();
            List<DeviceFile> unfoundFileList = new List<DeviceFile>();
            string[] fileList = Directory.GetFiles(Application.StartupPath);
            foreach( string f in fileList ) outsideFileList.Add(f);
            foreach (DeviceFile f in _dirMgt.DeviceDirInfor.Files)
            {
                string fname = ConfigHelper.GetFullPath(f.Location);
                if (outsideFileList.Contains(fname)) outsideFileList.Remove(fname);
                if (!File.Exists(fname)) unfoundFileList.Add(f);
            }

            string dirFileName = ConfigHelper.GetFullPath(DeviceDirManager.IndexFileName);
            if (outsideFileList.Contains(dirFileName)) outsideFileList.Remove(dirFileName);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Following files are in DeviceDir file list, but do not exist on current directory.");
            sb.AppendLine("---------------------------------------------------------------------------------");
            foreach (DeviceFile f in unfoundFileList) sb.AppendLine(f.ToString());
            sb.AppendLine("");
            sb.AppendLine("Following files exist on current directory, but are not in DeviceDir file list.");
            sb.AppendLine("------------------------------------------------------------------------------");
            foreach (string f in outsideFileList) sb.AppendLine(Path.GetFileName(f));

            Form frm = new Form();
            TextBox tb = new TextBox();
            tb.ScrollBars = ScrollBars.Both;
            tb.Dock = DockStyle.Fill;
            tb.Text = sb.ToString();
            tb.Multiline = true;
            tb.WordWrap = false;
            frm.Controls.Add(tb);
            frm.Size = this.Size;
            frm.Text = "Check Files";
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog(this);
        }

        private void CheckCommands()
        {
            List<Command> unfoundFileList = new List<Command>();
            foreach (Command c in _dirMgt.DeviceDirInfor.Commands)
            {
                string fname = ConfigHelper.GetFullPath(c.Path);
                if (!File.Exists(fname)) unfoundFileList.Add(c);
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Following commands are in DeviceDir command list, but do not exist on current directory.");
            sb.AppendLine("---------------------------------------------------------------------------------");
            foreach (Command c in unfoundFileList) sb.AppendLine(c.ToString());

            Form frm = new Form();
            TextBox tb = new TextBox();
            tb.ScrollBars = ScrollBars.Both;
            tb.Dock = DockStyle.Fill;
            tb.Text = sb.ToString();
            tb.Multiline = true;
            tb.WordWrap = false;
            frm.Controls.Add(tb);
            frm.Size = this.Size;
            frm.Text = "Check Commands";
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog(this);
        }

        private void RefreshDevice()
        {
            this.propertyGridHeader.SelectedObject = _dirMgt.DeviceDirInfor.Header;
            this.propertyGridCommand.SelectedObject = null;
            this.propertyGridFile.SelectedObject = null;
            RefreshCommandList(null);
            RefreshFileList(null);
        }

        private void RefreshCommandList(Command cmd)
        {
            this.listBoxCommand.Items.Clear();
            foreach (Command c in _dirMgt.DeviceDirInfor.Commands)
            {
                int index = this.listBoxCommand.Items.Add(c);
                if (cmd == c) this.listBoxCommand.SelectedIndex = index;
            }
        }

        private void RefreshFileList(DeviceFile file)
        {
            this.listBoxFile.Items.Clear();
            foreach (DeviceFile f in _dirMgt.DeviceDirInfor.Files)
            {
                int index = this.listBoxFile.Items.Add(f);
                if (file == f) this.listBoxFile.SelectedIndex = index;
            }
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            LoadFile();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void buttonFileAdd_Click(object sender, EventArgs e)
        {
            DeviceFile file = new DeviceFile();
            _dirMgt.DeviceDirInfor.Files.Add(file);
            RefreshFileList(file);
        }

        private void buttonCommandAdd_Click(object sender, EventArgs e)
        {
            Command cmd = new Command();
            _dirMgt.DeviceDirInfor.Commands.Add(cmd);
            RefreshCommandList(cmd);
        }

        private void buttonFileDelete_Click(object sender, EventArgs e)
        {
            DeviceFile file = this.listBoxFile.SelectedItem as DeviceFile;
            if (file != null) _dirMgt.DeviceDirInfor.Files.Remove(file);
            RefreshFileList(null);
        }

        private void buttonCommandDelete_Click(object sender, EventArgs e)
        {
            Command cmd = this.listBoxCommand.SelectedItem as Command;
            if (cmd != null) _dirMgt.DeviceDirInfor.Commands.Remove(cmd);
            RefreshCommandList(null);
        }

        private void listBoxFile_SelectedIndexChanged(object sender, EventArgs e)
        {
            DeviceFile file = this.listBoxFile.SelectedItem as DeviceFile;
            if (file != null) this.propertyGridFile.SelectedObject = file;
        }

        private void listBoxCommand_SelectedIndexChanged(object sender, EventArgs e)
        {
            Command cmd = this.listBoxCommand.SelectedItem as Command;
            if (cmd != null) this.propertyGridCommand.SelectedObject = cmd;
        }

        private void propertyGridFile_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            DeviceFile file = this.propertyGridFile.SelectedObject as DeviceFile;
            RefreshFileList(file);
        }

        private void propertyGridCommand_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            Command cmd = this.propertyGridCommand.SelectedObject as Command;
            RefreshCommandList(cmd);
        }

        private void buttonFileCheck_Click(object sender, EventArgs e)
        {
            CheckFiles();
        }

        private void buttonCommandCheck_Click(object sender, EventArgs e)
        {
            CheckCommands();
        }
        
        private void buttonDefaultInbound_Click(object sender, EventArgs e)
        {
            DeviceDir dir = new DeviceDir();

            dir.Header.Description = "Inbound [device name] adapter";
            dir.Header.Direction = DirectionType.INBOUND;
            dir.Header.EventTypes.Add(GWEventType.Empty);
            dir.Header.Version = "[device version]";
            dir.Header.Type = DeviceType.UNKNOWN;
            dir.Header.Name = "[device name]";
            dir.Header.UseCommandOnly = false;

            dir.Files.Add(new DeviceFile(DeviceFileType.Installer,"InstallUtil.exe"));
            dir.Files.Add(new DeviceFile(DeviceFileType.ServiceAssembly, "HYS.IM.Adapter.Service.exe"));
            dir.Files.Add(new DeviceFile(DeviceFileType.ServiceConfig, "HYS.Adapter.Service.config"));
            dir.Files.Add(new DeviceFile(DeviceFileType.ConfigAssembly, "HYS.IM.Adapter.Config.exe"));
            dir.Files.Add(new DeviceFile(DeviceFileType.ConfigConfig, "HYS.Adapter.Config.config"));
            dir.Files.Add(new DeviceFile(DeviceFileType.MonitorAssembly, "HYS.IM.Adapter.Monitor.exe"));
            dir.Files.Add(new DeviceFile(DeviceFileType.MonitorConfig, "HYS.Adapter.Monitor.config"));
            dir.Files.Add(new DeviceFile(DeviceFileType.Other, "HYS.IM.Adapter.Composer.exe"));
            dir.Files.Add(new DeviceFile(DeviceFileType.Other, "HYS.Adapter.Composer.config"));
            dir.Files.Add(new DeviceFile(DeviceFileType.Other, "HYS.Adapter.Base.dll"));
            dir.Files.Add(new DeviceFile(DeviceFileType.Other, "HYS.Common.DataAccess.dll"));
            dir.Files.Add(new DeviceFile(DeviceFileType.Other, "HYS.Common.Objects.dll"));
            dir.Files.Add(new DeviceFile(DeviceFileType.Other, "HYS.Common.Xml.dll"));
            dir.Files.Add(new DeviceFile(DeviceFileType.Other, "Interop.DOGMANAGERLib.dll"));
            dir.Files.Add(new DeviceFile(DeviceFileType.Other, "gatewaylang_SC.dll"));

            _dirMgt.DeviceDirInfor = dir;
            RefreshDevice();
        }

        private void buttonDefaultOutbound_Click(object sender, EventArgs e)
        {
            DeviceDir dir = new DeviceDir();

            dir.Header.Description = "Inbound [device name] adapter";
            dir.Header.Direction = DirectionType.OUTBOUND;
            dir.Header.Version = "[device version]";
            dir.Header.Type = DeviceType.UNKNOWN;
            dir.Header.Name = "[device name]";
            dir.Header.UseCommandOnly = false;

            dir.Files.Add(new DeviceFile(DeviceFileType.Installer, "InstallUtil.exe"));
            dir.Files.Add(new DeviceFile(DeviceFileType.ServiceAssembly, "HYS.IM.Adapter.Service.exe"));
            dir.Files.Add(new DeviceFile(DeviceFileType.ServiceConfig, "HYS.Adapter.Service.config"));
            dir.Files.Add(new DeviceFile(DeviceFileType.ConfigAssembly, "HYS.IM.Adapter.Config.exe"));
            dir.Files.Add(new DeviceFile(DeviceFileType.ConfigConfig, "HYS.Adapter.Config.config"));
            dir.Files.Add(new DeviceFile(DeviceFileType.MonitorAssembly, "HYS.IM.Adapter.Monitor.exe"));
            dir.Files.Add(new DeviceFile(DeviceFileType.MonitorConfig, "HYS.Adapter.Monitor.config"));
            dir.Files.Add(new DeviceFile(DeviceFileType.Other, "HYS.IM.Adapter.Composer.exe"));
            dir.Files.Add(new DeviceFile(DeviceFileType.Other, "HYS.Adapter.Composer.config"));
            dir.Files.Add(new DeviceFile(DeviceFileType.Other, "HYS.Adapter.Base.dll"));
            dir.Files.Add(new DeviceFile(DeviceFileType.Other, "HYS.Common.DataAccess.dll"));
            dir.Files.Add(new DeviceFile(DeviceFileType.Other, "HYS.Common.Objects.dll"));
            dir.Files.Add(new DeviceFile(DeviceFileType.Other, "HYS.Common.Xml.dll"));
            dir.Files.Add(new DeviceFile(DeviceFileType.Other, "OutboundDBInstall.exe"));
            dir.Files.Add(new DeviceFile(DeviceFileType.Other, "OutboundDBInstallConfig.xml"));
            dir.Files.Add(new DeviceFile(DeviceFileType.Other, "Interop.DOGMANAGERLib.dll"));
            dir.Files.Add(new DeviceFile(DeviceFileType.Other, "gatewaylang_SC.dll"));
            
            _dirMgt.DeviceDirInfor = dir;
            RefreshDevice();
        }

        private void buttonDefaultBidirectional_Click(object sender, EventArgs e)
        {
            DeviceDir dir = new DeviceDir();

            dir.Header.Description = "Bidirectional [device name] adapter";
            dir.Header.Direction = DirectionType.BIDIRECTIONAL;
            dir.Header.EventTypes.Add(GWEventType.Empty);
            dir.Header.Version = "[device version]";
            dir.Header.Type = DeviceType.UNKNOWN;
            dir.Header.Name = "[device name]";
            dir.Header.UseCommandOnly = false;

            dir.Files.Add(new DeviceFile(DeviceFileType.Installer, "InstallUtil.exe"));
            dir.Files.Add(new DeviceFile(DeviceFileType.ServiceAssembly, "HYS.IM.Adapter.Service.exe"));
            dir.Files.Add(new DeviceFile(DeviceFileType.ServiceConfig, "HYS.Adapter.Service.config"));
            dir.Files.Add(new DeviceFile(DeviceFileType.ConfigAssembly, "HYS.IM.Adapter.Config.exe"));
            dir.Files.Add(new DeviceFile(DeviceFileType.ConfigConfig, "HYS.Adapter.Config.config"));
            dir.Files.Add(new DeviceFile(DeviceFileType.MonitorAssembly, "HYS.IM.Adapter.Monitor.exe"));
            dir.Files.Add(new DeviceFile(DeviceFileType.MonitorConfig, "HYS.Adapter.Monitor.config"));
            dir.Files.Add(new DeviceFile(DeviceFileType.Other, "HYS.IM.Adapter.Composer.exe"));
            dir.Files.Add(new DeviceFile(DeviceFileType.Other, "HYS.Adapter.Composer.config"));
            dir.Files.Add(new DeviceFile(DeviceFileType.Other, "HYS.Adapter.Base.dll"));
            dir.Files.Add(new DeviceFile(DeviceFileType.Other, "HYS.Common.DataAccess.dll"));
            dir.Files.Add(new DeviceFile(DeviceFileType.Other, "HYS.Common.Objects.dll"));
            dir.Files.Add(new DeviceFile(DeviceFileType.Other, "HYS.Common.Xml.dll"));
            dir.Files.Add(new DeviceFile(DeviceFileType.Other, "Interop.DOGMANAGERLib.dll"));
            dir.Files.Add(new DeviceFile(DeviceFileType.Other, "gatewaylang_SC.dll"));

            _dirMgt.DeviceDirInfor = dir;
            RefreshDevice();
        }

        private void buttonDeviceName_Click(object sender, EventArgs e)
        {
            Form frm = new Form();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.Text = "Licensed Device Name";

            TextBox tb = new TextBox();
            tb.Dock = DockStyle.Fill;
            tb.ScrollBars = ScrollBars.Both;
            tb.WordWrap = false;
            tb.Multiline = true;

            string[] strList = Enum.GetNames(typeof(HYS.Common.Objects.License2.DeviceName));
            foreach (string str in strList)
            {
                tb.Text += str + "\r\n";
            }

            tb.Select(0, 0);
            frm.Controls.Add(tb);
            frm.ShowDialog(this);
        }
    }
}