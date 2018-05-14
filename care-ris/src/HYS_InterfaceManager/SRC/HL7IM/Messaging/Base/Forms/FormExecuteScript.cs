using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using HYS.IM.Common.Logging;

namespace HYS.IM.Messaging.Base.Forms
{
    public partial class FormExecuteScript : Form
    {
        private ILog _log;
        private List<ScriptTask> _scriptTasks;

        public FormExecuteScript(List<ScriptTask> tasks)
            : this(tasks, null, null)
        {
        }
        public FormExecuteScript(List<ScriptTask> tasks, string title)
            : this(tasks, title, null)
        {
        }
        public FormExecuteScript(List<ScriptTask> tasks, string title, ILog log)
        {
            _log = null;

            InitializeComponent();

            if (title != null && title.Length > 0) this.labelPlease.Text = title;

            _scriptTasks = tasks;
            LoadScriptTasks();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            RunScriptTasks();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void LoadScriptTasks()
        {
            if (_scriptTasks == null) return;

            foreach (ScriptTask t in _scriptTasks)
            {
                this.checkedListBoxScript.Items.Add(t, t.Enable);
            }
        }

        private void RunScriptTasks()
        {
            foreach (ScriptTask t in this.checkedListBoxScript.CheckedItems)
            {
                string info = t.ToString();

                try
                {
                    _log.Write("Begin running " + info);

                    using (Process proc = Process.Start(t.Command, t.Argument))
                    {
                        proc.Start();
                        proc.WaitForExit();
                    }

                    _log.Write("Finish running " + info);

                    t.NotifyTaskExecuted();
                }
                catch (Exception err)
                {
                    _log.Write(LogType.Error, "Error when running " + t.ToString());
                    _log.Write(err);

                    if (MessageBox.Show(this,
                        "Run the following script failed. Please click \"Yes\" to continue, or click \"No\" to abort.\r\n\r\n"
                        + info + "\r\n\r\nError information: " + err.Message
                        , "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error)
                        == DialogResult.Yes)
                    {
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }
}