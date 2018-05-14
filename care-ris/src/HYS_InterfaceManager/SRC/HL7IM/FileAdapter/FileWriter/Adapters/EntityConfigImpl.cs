using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.IM.MessageDevices.FileAdpater.FileWriter.Forms;
using HYS.IM.Messaging.Base;
using HYS.IM.Messaging.Base.Config;

namespace HYS.IM.MessageDevices.FileAdpater.FileWriter.Adapters
{
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
