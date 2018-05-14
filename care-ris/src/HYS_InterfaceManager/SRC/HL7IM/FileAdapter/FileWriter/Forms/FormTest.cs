using System;
using System.Windows.Forms;
using HYS.IM.Messaging.Registry;
using MSG = HYS.IM.Messaging.Objects;
using HYS.IM.MessageDevices.FileAdpater.FileWriter.Controler;
using System.Diagnostics;

namespace HYS.IM.MessageDevices.FileAdpater.FileWriter.Forms
{
    public partial class FormTest : Form
    {
        private FileWriterControler _controler = null;
        public FormTest()
        {
            InitializeComponent();

            _controler = new FileWriterControler(Program.Context);
        }

        private void buttonReceive_Click(object sender, EventArgs e)
        {
            MSG.Message notify = new MSG.Message();
            //notify.Header.Type = MessageRegistry.HL7V2_NotificationMessageType;
            notify.Header.Type = MessageRegistry.GENERIC_NotificationMessageType;
            notify.Body = this.textBoxMessageContent.Text;

            _controler.ProcessSubscribedMessage(notify);
        }

        private void buttonOpenFile_Click(object sender, EventArgs e)
        {
            Process proc = Process.Start("explorer.exe", "\"" + Program.Context.ConfigMgr.Config.OutputFileFolder + "\"");
            proc.EnableRaisingEvents = false;
        }
    }
}
