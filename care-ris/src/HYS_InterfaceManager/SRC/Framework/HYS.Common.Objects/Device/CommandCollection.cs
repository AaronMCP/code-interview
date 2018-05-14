using System;
using System.Collections.Generic;
using System.Text;
using HYS.Common.Xml;

namespace HYS.Common.Objects.Device
{
    public class CommandCollection : XObjectCollection
    {
        public CommandCollection() : base( typeof( Command ) )
        {
        }

        public Command[] FindCommands(CommandType type)
        {
            List<Command> list = new List<Command>();
            foreach (Command c in this)
            {
                if (c.Type == type) list.Add(c);
            }
            return list.ToArray();
        }
    }
}
