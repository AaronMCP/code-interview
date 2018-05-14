using System;
using System.Text;
using System.Collections.Generic;
using HYS.Common.Xml;

namespace HYS.Messaging.Objects.ProcessModel
{
    public class ProcesserInfo : XObject
    {
        private string _codeSystem = "";
        public string CodeSystem
        {
            get { return _codeSystem; }
            set { _codeSystem = value; }
        }

        private string _code = "";
        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }

        private string _description = "";
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        private string _assemblyLocation = "";
        public string AssemblyLocation
        {
            get { return _assemblyLocation; }
            set { _assemblyLocation = value; }
        }

        private string _className = "";
        public string ClassName
        {
            get { return _className; }
            set { _className = value; }
        }
    }
}
