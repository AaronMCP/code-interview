using System;
using Hys.Platform.Domain;

namespace Hys.CareRIS.Domain.Entities
{
    public partial class ICDTen : Entity
    {
        public override object UniqueId { get { return UniqueID; } }

        public string UniqueID { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public string PY { get; set; }
        public string WB { get; set; }
        public string TJM { get; set; }
        public string BZLB { get; set; }
        public string ZLBM { get; set; }
        public string JLZT { get; set; }
        public string Memo { get; set; }
        public string Domain { get; set; }
    }
}
