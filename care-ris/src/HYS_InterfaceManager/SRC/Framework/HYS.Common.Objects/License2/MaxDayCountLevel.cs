using System;
using System.Text;
using System.Collections.Generic;

namespace HYS.Common.Objects.License2
{
    public class MaxDayCountLevel
    {
        private byte _levelID;
        public byte LevelID
        {
            get { return _levelID; }
            set { _levelID = value; }
        }

        private int _maxDayCount;
        public int MaxDayCount
        {
            get { return _maxDayCount; }
            set { _maxDayCount = value; }
        }

        public MaxDayCountLevel()
        {
        }
        public MaxDayCountLevel(byte levelID, int maxDayCount)
        {
            _levelID = levelID;
            _maxDayCount = maxDayCount;
        }

        public override string ToString()
        {
            if (_maxDayCount == NeverExpire)
            {
                return "Never expire.";
            }
            else
            {
                return "Expire after " + _maxDayCount.ToString() + " day(s)";
            }
        }

        private static MaxDayCountLevel[] _maxDayCountLevels;
        public static MaxDayCountLevel[] MaxDayCountLevels
        {
            get
            {
                if (_maxDayCountLevels == null)
                {
                    _maxDayCountLevels = new MaxDayCountLevel[]
                    {
                    new MaxDayCountLevel(0, NeverExpire),    //infinite
                    new MaxDayCountLevel(1, 1),
                    new MaxDayCountLevel(2, 3),
                    new MaxDayCountLevel(3, 7),
                    new MaxDayCountLevel(4, 14),
                    new MaxDayCountLevel(5, 30),
                    new MaxDayCountLevel(6, 60),
                    new MaxDayCountLevel(7, 90),
                    new MaxDayCountLevel(8, 180),
                    new MaxDayCountLevel(9, 365),
                    };
                }
                return _maxDayCountLevels;
            }
        }
        public static int GetMaxDayCount(byte levelID)
        {
            foreach (MaxDayCountLevel level in MaxDayCountLevels)
            {
                if (levelID == level.LevelID) return level.MaxDayCount;
            }
            return NeverExpire;
        }

        public const int NeverExpire = -1;
    }
}
