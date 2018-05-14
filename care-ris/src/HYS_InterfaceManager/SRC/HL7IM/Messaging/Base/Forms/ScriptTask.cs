using System;
using System.Collections.Generic;
using System.Text;

namespace HYS.IM.Messaging.Base.Forms
{
    public class ScriptTask
    {
        public ScriptTask(string cmd)
            : this(cmd, "")
        {
        }

        public ScriptTask(string cmd, string arg)
        {
            _command = cmd;
            _argument = arg;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(_command);
            if (_argument != null && _argument.Length > 0) sb.Append(" ").Append(_argument);
            string str = sb.ToString();
            return str;
        }

        private string _command = "";
        public string Command
        {
            get { return _command; }
            set { _command = value; }
        }

        private string _argument = "";
        public string Argument
        {
            get { return _argument; }
            set { _argument = value; }
        }

        private bool _enable;
        public bool Enable
        {
            get { return _enable; }
            set { _enable = value; }
        }

        public event EventHandler Executed;
        internal void NotifyTaskExecuted()
        {
            if (Executed != null) Executed(this, EventArgs.Empty);
        }
    }
}
