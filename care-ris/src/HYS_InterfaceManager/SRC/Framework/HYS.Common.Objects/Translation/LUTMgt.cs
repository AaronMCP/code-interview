using System;
using System.Collections;
using System.Collections.Generic;
using HYS.Common.DataAccess;

namespace HYS.Common.Objects.Translation
{
    public class LutMgt
    {
        private DataBase _db;

        public DataBase DataBase
        {
            get { return _db; }
        }

        public LutMgt(DataBase db)
        {
            _db = db;
        }

        public const string Prefix = "lut_";
        public const string PrefixPrivate = "lut_p_";

        public string[] GetLutNames()
        {
            List<string> list = new List<string>();

            string[] tlist = _db.GetTableNames();
            if (tlist != null)
            {
                foreach (string t in tlist)
                {
                    if (t.Length <= Prefix.Length || t.Substring(0, Prefix.Length).ToLower() != Prefix)
                        continue;

                    if (t.Length >= PrefixPrivate.Length &&
                        t.Substring(0, PrefixPrivate.Length).ToLower() == PrefixPrivate) continue;

                    list.Add(t);
                }
            }

            return list.ToArray();
        }

        public LUTItemMgt AddLut(string lutName)
        {
            if (lutName == null || lutName.Length < 1) return null;

            string name = lutName;
            if (name.Length <= Prefix.Length || name.Substring(0, Prefix.Length).ToUpper() != Prefix)
                name = Prefix + name;

            LUTItemMgt lut = new LUTItemMgt(_db, name);
            if (!lut.Create()) return null;

            return lut;
        }

        public bool DeleteLut(string lutName)
        {
            if (lutName == null || lutName.Length < 1) return false;

            LUTItemMgt lut = _lutList[lutName] as LUTItemMgt;
            if (lut != null)
            {
                _lutList.Remove(lutName);
            }
            else
            {
                lut = new LUTItemMgt(_db, lutName);
            }

            return lut.Drop(); ;
        }

        private Hashtable _lutList = new Hashtable();

        public LUTItemMgt GetLut(string lutName)
        {
            if (lutName == null) return null;

            LUTItemMgt lut = _lutList[lutName] as LUTItemMgt;
            if (lut == null)
            {
                lut = new LUTItemMgt(_db, lutName);
                if (lut.ReloadLUT() == null) return null;
                _lutList[lutName] = lut;
            }

            return lut;
        }

        public string GetTargetValue(string lutName, string sourceValue)
        {
            LUTItemMgt lut = GetLut(lutName);
            if (lut == null) return null;
            return lut.GetTargetValue(sourceValue);
        }

        public string GetSourceValue(string lutName, string targetValue)
        {
            LUTItemMgt lut = GetLut(lutName);
            if (lut == null) return null;
            return lut.GetSourceValue(targetValue);
        }
    }
}
