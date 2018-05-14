using System;
using System.ComponentModel;
using System.Collections.Generic;
using HYS.Common.Xml;

namespace HYS.Common.Objects.Device
{
    public class Command : XObject
    {
        private CommandType _type = CommandType.Unkown;
        [Description("Type of command. When the value of property EnableCommands in DeviceDir header information is True, IM will interact with the interface by invoking specific type of commands in Command List.")]
        public CommandType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        private string _path;
        [Description("Command name. Should be an indirect path, from the directory of this interface.")]
        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }

        private string _argument;
        [Description("Command argument.")]
        public string Argument
        {
            get { return _argument; }
            set { _argument = value; }
        }

        private string _description;
        [Description("Command description.")]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public override string ToString()
        {
            return "[" + Type + "] - " + System.IO.Path.GetFileName(Path);
        }
    }
}
