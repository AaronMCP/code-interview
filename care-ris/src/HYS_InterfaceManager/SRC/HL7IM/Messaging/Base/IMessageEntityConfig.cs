using System;
using System.Collections.Generic;
using System.Text;
using HYS.IM.Messaging.Objects.Entity;
using HYS.IM.Messaging.Base.Config;

namespace HYS.IM.Messaging.Base
{
    public interface IMessageEntityConfig : IEntry
    {
        IConfigUI[] GetIConfigUI();
        bool SaveConfiguration();
    }
}
