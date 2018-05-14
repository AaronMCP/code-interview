using System;
using System.Text;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace HYS.IM.Messaging.Objects.Entity
{
    public static class EntityDictionary
    {
        // This dictionary is not used for LPC channel binding any more. 
        // Instead, we use Messaging.Queuing.LPC.LPCReceiverDictionary to do LPC channel binding.
        // However, this dictionary remains for possible requirement of global accessing to the entity list in current host.
        // 20110328

        public static Dictionary<Guid, IEntity> Entities = new Dictionary<Guid, IEntity>();

        private static int count;
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static string GetRandomNumber()
        {
            string str = DateTime.Now.Ticks.ToString();
            return str + unchecked(count++).ToString();
        }
    }
}
