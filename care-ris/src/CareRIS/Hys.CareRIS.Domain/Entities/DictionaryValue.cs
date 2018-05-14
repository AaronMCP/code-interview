using Hys.Platform.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Domain.Entities
{
    public partial class DictionaryValue : Entity
    {
        public override object UniqueId { get { return UniqueID; } }

        public string UniqueID { get; set; }
        public int Tag { get; set; }
        public string Value { get; set; }
        public string Text { get; set; }
        public int? IsDefault { get; set; }
        public string ShortcutCode { get; set; }
        public int? OrderID { get; set; }
        public int? MapTag { get; set; }
        public string MapValue { get; set; }
        public string Site { get; set; }
        public string Domain { get; set; }
    }
}
