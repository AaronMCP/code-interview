using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.IM.Messaging.Base;
using HYS.IM.MessageDevices.HL7Adapter.HL7Sender.Forms;
using HYS.IM.Messaging.Base.Config;

namespace HYS.IM.MessageDevices.HL7Adapter.HL7Sender.Adapters
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
            bool res = Program.Context.ConfigMgr.Save();
            if (!res) Program.Context.Log.Write(Program.Context.ConfigMgr.LastError);
            return res;
        }

        #endregion

        #region IEntry Members

        public bool Initialize(EntityInitializeArgument arg)
        {
            Program.Context.PreLoading(arg);
            return true;
        }

        public EntityConfigBase GetConfiguration()
        {
            return Program.Context.ConfigMgr.Config;
        }

        public bool Uninitalize()
        {
            Program.Context.BeforeExit();
            return true;
        }

        #endregion
    }
}
