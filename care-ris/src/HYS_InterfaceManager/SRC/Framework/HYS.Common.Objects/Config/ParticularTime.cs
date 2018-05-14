using System;
using System.Globalization;
using System.ComponentModel;
using System.Collections.Generic;
using HYS.Common.Xml;

namespace HYS.Common.Objects.Config
{
    public class ParticularTime : XObject
    {
        private int _interval = 30 * 1000;  //ms    : GC may occur twice in one minute (for reliability reason)
        [ReadOnly(true)]
        public int Interval
        {
            get { return _interval; }
            set { _interval = value; }
        }

        private DateTime _time = DateTime.Now;
        public DateTime Time
        {
            get { return _time; }
            set { _time = value; }
        }

        private WeekDay _weekDay;
        public WeekDay WeekDay
        {
            get { return _weekDay; }
            set { _weekDay = value; }
        }

        private MonthDay _monthDay;
        public MonthDay MonthDay
        {
            get { return _monthDay; }
            set { _monthDay = value; }
        }

        private Month _month;
        public Month Month
        {
            get { return _month; }
            set { _month = value; }
        }

        public bool TimeIsUp()
        {
            DateTime dtNow = DateTime.Now;

            if (_time.Hour != dtNow.Hour) return false;
            if (_time.Minute != dtNow.Minute) return false;

            if (WeekDay != WeekDay.Unknown)
            {
                if ((int)WeekDay != ((int)dtNow.DayOfWeek + 1)) return false;
            }

            if (MonthDay != MonthDay.Unknown)
            {
                if((int)MonthDay != dtNow.Day) return false;
            }

            if (Month != Month.Unknown)
            {
                if ((int)Month != dtNow.Month) return false;
            }

            return true;
        }

        public static string[] GetMonths()
        {
            return GetEnum(typeof(Month));
        }
        public static string[] GetWeekDays()
        {
            return GetEnum(typeof(WeekDay));
        }
        public static string[] GetMonthDays()
        {
            List<string> list = new List<string>();
            string[] strList = Enum.GetNames(typeof(MonthDay));
            for (int i = 1; i < strList.Length; i++) list.Add(strList[i].Replace("Day_",""));
            return list.ToArray();
        }
        private static string[] GetEnum(Type enumType)
        {
            List<string> list = new List<string>();
            string[] strList = Enum.GetNames(enumType);
            for (int i = 1; i < strList.Length; i++) list.Add(strList[i]);
            return list.ToArray();
        }

        public static WeekDay ParseWeekDay(string str)
        {
            return (WeekDay)Enum.Parse(typeof(WeekDay), str);
        }
        public static MonthDay ParseMonthDay(string str)
        {
            return (MonthDay)Enum.Parse(typeof(MonthDay), "Day_" + str);
        }
        public static Month ParseMonth(string str)
        {
            return (Month)Enum.Parse(typeof(Month), str);
        }
    }

    public enum WeekDay
    {
        Unknown = 0,
        Sunday = 1,
        Monday,
        Tuesday,
        Wednsday,
        Thursday,
        Friday,
        Saturday,
    }

    public enum MonthDay
    {
        Unknown = 0,
        Day_1,
        Day_2,
        Day_3,
        Day_4,
        Day_5,
        Day_6,
        Day_7,
        Day_8,
        Day_9,
        Day_10,
        Day_11,
        Day_12,
        Day_13,
        Day_14,
        Day_15,
        Day_16,
        Day_17,
        Day_18,
        Day_19,
        Day_20,
        Day_21,
        Day_22,
        Day_23,
        Day_24,
        Day_25,
        Day_26,
        Day_27,
        Day_28,
    }

    public enum Month
    {
        Unknown = 0,
        January,
        Feburary,
        March,
        April,
        May,
        June,
        July,
        August,
        September,
        October,
        November,
        December,
    }
}
