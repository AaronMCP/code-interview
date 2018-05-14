using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Hys.CareAgent.WcfService
{
    public sealed partial class VideoForm : Form
    {
        private static VideoForm instance = new VideoForm() { ShowInTaskbar = false };
        private static bool IsShown = false;
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger("App");
        public static VideoForm VideoInstance
        {
            get { return instance ?? (instance = new VideoForm() { ShowInTaskbar = false }); }
        }
        private VideoForm()
        {
            InitializeComponent();
            this.Text = Lang.Camera;
            int x = Screen.PrimaryScreen.WorkingArea.Width - this.Width;
            int y = Screen.PrimaryScreen.WorkingArea.Height - this.Height;
            this.Location = new Point(x, y);
        }

        static VideoForm()
        {
            instance.FormClosing += new FormClosingEventHandler(OnlyInstance_FormClosing);
        }

        public new void Show()
        {
            showCamera();
            if (!this.Visible)
            {
                base.Show();
            }
        }

        public new void Hide()
        {
            closeCamera();
            if (this.Visible)
            {
                base.Hide();
            }
        }

        private static void OnlyInstance_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            instance.Hide();
        }

        private static void showCamera()
        {
            DShowNET.Device.DsDevice[] devices = CameraCpature.GetDevices();
            if (devices.Length > 0)
            {
                CameraSingleton.Instance.StartPreview(devices[0]);
            }
            else
            {
                MessageBox.Show(Lang.CameraInitializeError);
            }
        }

        private static void closeCamera()
        {
            CameraSingleton.Instance.RestartCamera();
            CameraSingleton.Instance.CameraCpature.CloseInterfaces();
        }

        public void InitHispeed()
        {
            try
            {
                contextMenuStrip1.Items.Clear();
                ToolStripMenuItem reloadItem = new ToolStripMenuItem();
                reloadItem.Text = Lang.Restart;
                reloadItem.Name = Lang.Restart;
                reloadItem.Click += new EventHandler(Reload_Click);
                contextMenuStrip1.Items.Add(reloadItem);
                DShowNET.Device.DsDevice[] devices = CameraCpature.GetDevices();
                foreach (DShowNET.Device.DsDevice de in devices)
                {
                    ToolStripMenuItem item = new ToolStripMenuItem();
                    item.Text = de.Name;
                    item.Name = de.Name;
                    item.Tag = de;
                    item.Click += new EventHandler(item_Click);
                    contextMenuStrip1.Items.Add(item);
                }
                //capture1 = new CameraCpature(this.panelPreview);
                this.videoPanel.Controls.Add(CameraSingleton.Instance.PreviewPanel);
                CameraSingleton.Instance.PreviewPanel.Dock = DockStyle.Fill;
            }

            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void Reload_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Lang.RestartInfo, string.Empty,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var process = Process.Start(Application.ExecutablePath); // to start new instance of application
                // //close this one
                Process.GetCurrentProcess().Kill();
            }
        }

        private void item_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripMenuItem itemSender = sender as ToolStripMenuItem;
                if (itemSender == null || itemSender.Tag == null)
                {
                    return;
                }
                foreach (ToolStripMenuItem item in contextMenuStrip1.Items)
                {
                    item.Checked = false;
                    item.Text = item.Name;
                }

                itemSender.Checked = true;
                itemSender.Text = Lang.ReConnect + itemSender.Text;
                DShowNET.Device.DsDevice de = itemSender.Tag as DShowNET.Device.DsDevice;
                CameraSingleton.Instance.ChangeDevice(de);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        private void VideoForm_Load(object sender, EventArgs e)
        {
            InitHispeed();
        }
    }
}
