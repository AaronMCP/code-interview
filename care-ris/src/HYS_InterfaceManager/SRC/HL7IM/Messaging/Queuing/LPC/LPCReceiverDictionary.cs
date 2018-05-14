using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HYS.IM.Messaging.Queuing.LPC
{
    public class LPCReceiverDictionary
    {
        public static Dictionary<string, LPCNotificationReceiver> PushReceivers = new Dictionary<string, LPCNotificationReceiver>();
        public static Dictionary<string, LPCQueryReceiver> PullReceivers = new Dictionary<string, LPCQueryReceiver>();

        public static void RegisterPushReceiver(string pushRouteID, LPCNotificationReceiver pushReceiver)
        {
            if (PushReceivers.ContainsKey(pushRouteID)) return;
            PushReceivers.Add(pushRouteID, pushReceiver);
        }
        public static void RegisterPullReceiver(string pullRouteID, LPCQueryReceiver pullReceiver)
        {
            if (PullReceivers.ContainsKey(pullRouteID)) return;
            PullReceivers.Add(pullRouteID, pullReceiver);
        }
        public static LPCNotificationReceiver GetPushReceiver(string pushRouteID)
        {
            if (PushReceivers.ContainsKey(pushRouteID)) return PushReceivers[pushRouteID];
            return null;
        }
        public static LPCQueryReceiver GetPullReceiver(string pullRouteID)
        {
            if (PullReceivers.ContainsKey(pullRouteID)) return PullReceivers[pullRouteID];
            return null;
        }
    }
}
