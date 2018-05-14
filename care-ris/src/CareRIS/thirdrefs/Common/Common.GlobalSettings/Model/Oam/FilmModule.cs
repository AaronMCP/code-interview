using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Action;

namespace CommonGlobalSettings
{
    [Serializable()]
    public class FilmStoreModule : OamBaseModel
    {
        public string FilmID { get; set; }
        public string FilmSpec { get; set; }
        public int FilmCount { get; set; }
        public string Supplier { get; set; }
        public string Manufacturer { get; set; }
        public DateTime Mfd { get; set; }
        public DateTime Exp { get; set; }
        public string LotNumber { get; set; }
        public string OperatorID { get; set; }
        public string OperatorName { get; set; }

    }

    [Serializable()]
    public class FilmReservedModule : OamBaseModel
    {
        public string ReservedID { get; set; }
        public string FilmID { get; set; }
        public string FilmSpec { get; set; }
        public int StoreCount { get; set; }
        public int UsedCount { get; set; }
        public int ReservedCount { get; set; }
        public DateTime OperateDt { get; set; }
      

    }
}
