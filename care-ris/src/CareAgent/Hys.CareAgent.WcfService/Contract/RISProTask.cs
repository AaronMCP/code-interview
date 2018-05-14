#region

using System;

#endregion

namespace Hys.CareAgent.WcfService.Contract
{
    public class PrintTask
    {
        public DateTime DateTime { get; set; }
        public string Url { get; set; }
        public PrintTaskStatus Status { get; set; }
        public int FailedTimes { get; set; }
        public string Printer { get; set; }
    }

    public enum PrintTaskStatus
    {
        Ok,
        Failed,
        Unkown
    }
}