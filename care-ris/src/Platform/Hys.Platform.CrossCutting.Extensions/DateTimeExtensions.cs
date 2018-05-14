#region

using System;

#endregion

namespace Hys.Platform.CrossCutting.Extensions
{
    public static class DateTimeExtensions
    {
        public static string FormatTimeSpanHmsm(this TimeSpan tsSpan)
        {
            var r = string.Format("{0:00}:{1:00}:{2:00}.{3:000}", tsSpan.Hours, tsSpan.Minutes, tsSpan.Seconds,
                tsSpan.Milliseconds);
            return r;
        }
    }
}