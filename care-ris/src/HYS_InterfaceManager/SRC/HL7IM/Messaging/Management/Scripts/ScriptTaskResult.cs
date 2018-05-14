using System;
using System.Collections.Generic;
using System.Text;

namespace HYS.IM.Messaging.Management.Scripts
{
    [Serializable]
    public class ScriptTaskResult
    {
        private bool _success;
        public bool Success
        {
            get { return _success; }
            set { _success = value; }
        }

        private string _message = "";
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }
    }
}
