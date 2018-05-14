using System;
using System.Collections.Generic;
using System.Text;

namespace CommonGlobalSettings.BillBoard
{

    public delegate void BroadcastEventHandler(object sender, BroadcastEventArgs submitArgs);

    public class BroadCastEvtWrapper : MarshalByRefObject
    {
        public event BroadcastEventHandler WrapperBroadcastEvent;
        public void WrapperBroadcastReciving(object sender, BroadcastEventArgs args)
        {
            if (WrapperBroadcastEvent != null)
            {
                WrapperBroadcastEvent(sender, args);
            }
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }
    }
}
