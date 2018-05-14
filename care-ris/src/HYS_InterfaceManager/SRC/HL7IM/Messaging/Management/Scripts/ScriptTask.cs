using System;
using System.Collections.Generic;
using System.Text;

namespace HYS.IM.Messaging.Management.Scripts
{
    [Serializable]
    public class ScriptTask
    {
        private string _file = "";
        public string File
        {
            get { return _file; }
            set { _file = value; }
        }

        private string _argument = "";
        public string Argument
        {
            get { return _argument; }
            set { _argument = value; }
        }

        private string _workPath = "";
        public string WorkPath
        {
            get { return _workPath; }
            set { _workPath = value; }
        }
    }
}
