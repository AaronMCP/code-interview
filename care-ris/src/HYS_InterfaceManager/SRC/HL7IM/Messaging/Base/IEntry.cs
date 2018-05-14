using System;
using System.Text;
using System.Collections.Generic;
using HYS.IM.Messaging.Objects.Entity;
using HYS.IM.Messaging.Base.Config;

namespace HYS.IM.Messaging.Base
{
    public interface IEntry
    {
        bool Initialize(EntityInitializeArgument arg);
        EntityConfigBase GetConfiguration();
        bool Uninitalize();
    }
}
