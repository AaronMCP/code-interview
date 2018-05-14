using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.Messaging.Base;
using HYS.MessageDevices.MessagePipe.Forms;
using HYS.Messaging.Base.Config;

namespace HYS.MessageDevices.MessagePipe.Adapters
{
    /// <summary>
    /// This class is the entry point of the Configuration GUI Host.
    /// </summary>
    [MessageEntityConfigEntry]
    public class EntityConfigImpl : IMessageEntityConfig
    {
        #region IMessageEntityConfig Members

        public IConfigUI[] GetIConfigUI()
        {
            return new IConfigUI[] { new FormConfig() };
        }

        public bool SaveConfiguration()
        {
            bool res = Program.ConfigMgr.Save();
            if (!res) Program.Log.Write(Program.ConfigMgr.LastError);
            return res;
        }

        #endregion

        #region IEntry Members

        public bool Initialize(EntityInitializeArgument arg)
        {
            Program.PreLoading(arg);
            return true;
        }

        public EntityConfigBase GetConfiguration()
        {
            return Program.ConfigMgr.Config;
        }

        public bool Uninitalize()
        {
            Program.BeforeExit();
            return true;
        }

        #endregion
    }
}
