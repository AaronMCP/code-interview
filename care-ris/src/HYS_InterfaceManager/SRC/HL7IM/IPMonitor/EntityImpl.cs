using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.IM.Messaging.Base;
using HYS.IM.Messaging.Base.Config;

namespace HYS.IM.IPMonitor
{
    [MessageEntityEntry(
        "HL7 Gateway IP Monitor Service", 
        DirectionTypes.Unknown, 
        InteractionTypes.Unknown, 
        "HL7 Gateway IP Monitor Service to Implement Virtual IP Address Mechanism")]
    public class EntityImpl : IMessageEntity
    {
        private MonitorControl _ctrl;

        #region IMessageEntity Members

        public bool Start()
        {
            if (_ctrl == null) return false;
            _ctrl.Start();
            return true;
        }

        public bool Stop()
        {
            if (_ctrl == null) return false;
            _ctrl.Stop();
            return true;
        }

        public event EventHandler BaseServiceStop;

        #endregion

        #region IEntry Members

        public bool Initialize(EntityInitializeArgument arg)
        {
            if (!Program.PreLoading(arg)) return false;
            _ctrl = new MonitorControl(this);
            return true;
        }

        public EntityConfigBase GetConfiguration()
        {
            return Program.ConfigMgt.Config;
        }

        public bool Uninitalize()
        {
            Program.BeforeExit();
            return true;
        }

        public void RaiseBaseServiceStop()
        {
            BaseServiceStop(null, new EventArgs());
        }

        #endregion
    }
}
