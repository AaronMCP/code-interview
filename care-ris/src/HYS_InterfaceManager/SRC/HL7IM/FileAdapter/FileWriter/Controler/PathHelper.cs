using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.IM.MessageDevices.FileAdpater.FileWriter.Config;
using System.IO;

namespace HYS.IM.MessageDevices.FileAdpater.FileWriter.Controler
{
    public static class PathHelper
    {
        public static string GetRelativePathByMode(FileOrganizationMode mode)
        {
            switch (mode)
            {
                case FileOrganizationMode.Year:
                    return GetModeYearPath();
                case FileOrganizationMode.Quarter:
                    return GetModeQuarterPath();
                case FileOrganizationMode.Month:
                    return GetModeMonthPath();
                case FileOrganizationMode.Week:
                    return GetModeWeekPath();
                case FileOrganizationMode.Day:
                    return GetModeDayPath();
                case FileOrganizationMode.Hour:
                    return GetModeHourPath();
                default:
                    return string.Empty;
            }
        }

        private static string GetModeQuarterPath()
        {
            return Path.Combine(GetModeYearPath(), "Quarter" + ((DateTime.Now.Month - 1) / 3 + 1).ToString());
        }

        private static string GetModeWeekPath()
        {
            return Path.Combine(GetModeMonthPath(), "Week" + ((DateTime.Now.Day - 1) / 7 + 1).ToString());
        }

        private static string GetModeHourPath()
        {
            return Path.Combine(GetModeDayPath(), "Hour" + DateTime.Now.Hour.ToString());
        }

        private static string GetModeDayPath()
        {
            return Path.Combine(GetModeMonthPath(), "Day" + DateTime.Now.Day.ToString());
        }

        private static string GetModeMonthPath()
        {
            return Path.Combine(GetModeYearPath(), "Month" + DateTime.Now.Month.ToString());
        }

        private static string GetModeYearPath()
        {
            return "Year" + DateTime.Now.Year.ToString();
        }
    }
}
