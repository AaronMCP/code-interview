using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.Common.Xml;
using System.Drawing;

namespace HYS.IM.Config
{
    public class MessageEntityConfigConfig : XObject
    {
        public MessageEntityConfigConfig()
        {
            ConfigWindowSize = new Size(300, 300);
        }

        public string EntityName { get; set; }
        public Size ConfigWindowSize { get; set; }
    }
}
