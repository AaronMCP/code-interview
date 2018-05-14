using System;
using System.Collections.Generic;
using System.Text;
using HYS.IM.Common.Logging;
using HYS.IM.Messaging.Objects;

namespace HYS.IM.Messaging.Queuing
{
    public class DumpHelper
    {
        private static string GetFileName(Message msg)
        {
            if (msg == null) return null;

            string fname = null;
            if (msg.Header != null) fname = "_" + msg.Header.ID + ".xml";
            if (msg.Header.Type != null) fname = msg.Header.Type.CodeSystem + "_" + msg.Header.Type.Code + fname;

            return fname;
        }
        public static void DumpPublisherMessage(ILog log, Message msg)
        {
            if (log == null || log.DumpData == false) return;

            string fname = GetFileName(msg);
            if (fname == null) return;

            log.Write("Dumping publisher message to file: " + fname);
            log.DumpToFile("Publisher", fname, msg);
        }
        public static void DumpSubscriberMessage(ILog log, Message msg)
        {
            if (log == null || log.DumpData == false) return;

            string fname = GetFileName(msg);
            if (fname == null) return;

            log.Write("Dumping subscriber message to file: " + fname);
            log.DumpToFile("Subscriber", fname, msg);
        }
        public static void DumpResponserSendingMessage(ILog log, Message msg)
        {
            if (log == null || log.DumpData == false) return;

            string fname = GetFileName(msg);
            if (fname == null) return;

            log.Write("Dumping responser sending message to file: " + fname);
            log.DumpToFile("Responser", "s_" + fname, msg);
        }
        public static void DumpResponserReceivingMessage(ILog log, Message msg)
        {
            if (log == null || log.DumpData == false) return;

            string fname = GetFileName(msg);
            if (fname == null) return;

            log.Write("Dumping responser receiving message to file: " + fname);
            log.DumpToFile("Responser", "r_" + fname, msg);
        }
        public static void DumpRequesterSendingMessage(ILog log, Message msg)
        {
            if (log == null || log.DumpData == false) return;

            string fname = GetFileName(msg);
            if (fname == null) return;

            log.Write("Dumping requester sending message to file: " + fname);
            log.DumpToFile("Requester", "s_" + fname, msg);
        }
        public static void DumpRequesterReceivingMessage(ILog log, Message msg)
        {
            if (log == null || log.DumpData == false) return;

            string fname = GetFileName(msg);
            if (fname == null) return;

            log.Write("Dumping requester receiving message to file: " + fname);
            log.DumpToFile("Requester", "r_" + fname, msg);
        }
    }
}
