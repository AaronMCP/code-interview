using System;
using System.Collections.Generic;
using HYS.Common.DataAccess;

namespace HYS.Common.Objects.Translation
{
    public class LUTItemMgt : DObjectManager
    {
        public LUTItemMgt(DataBase db, string tableName)
            : base(db, tableName, typeof(LUTItem))
        {
        }

        private LUTItem[] _lut;
        public LUTItem[] LUT
        {
            get
            {
                if (_lut == null)
                {
                    //DObjectCollection olist = SelectAll();
                    string sql = "SELECT * FROM " + TableName + " ORDER BY ID";
                    DObjectCollection olist = Select(sql);
                    if (olist == null) return null;

                    List<LUTItem> list = new List<LUTItem>();
                    foreach (LUTItem i in olist)
                    {
                        list.Add(i);
                    }

                    _lut = list.ToArray();
                }
                return _lut;
            }
        }
        public LUTItem[] ReloadLUT()
        {
            _lut = null;
            return LUT;
        }
        
        /// <summary>
        /// Get target value according to source value.
        /// </summary>
        /// <param name="sourceValue">Source value</param>
        /// <returns>Return null when meeting with any poblem, return empty string when cannot find target value according to source value.</returns>
        public string GetTargetValue(string sourceValue)
        {
            if (sourceValue == null) return null;
            
            LUTItem[] lut = LUT;
            if (lut == null) return null;

            foreach (LUTItem i in lut)
            {
                if (i.SourceValue == sourceValue) return i.TargetValue;
            }

            return "";
        }
        /// <summary>
        /// Get source value according to target value.
        /// </summary>
        /// <param name="sourceValue">Target value</param>
        /// <returns>Return null when meeting with any poblem, return empty string when cannot find source value according to target value.</returns>
        public string GetSourceValue(string targetValue)
        {
            if (targetValue == null) return null;

            LUTItem[] lut = LUT;
            if (lut == null) return null;

            foreach (LUTItem i in lut)
            {
                if (i.TargetValue == targetValue) return i.SourceValue;
            }

            return "";
        }
    }
}
