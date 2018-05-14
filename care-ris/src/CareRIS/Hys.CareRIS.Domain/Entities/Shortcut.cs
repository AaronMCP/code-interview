using Hys.Platform.Domain;

namespace Hys.CareRIS.Domain.Entities
{
    public class Shortcut : Entity
    {
        public override object UniqueId { get { return UniqueID; } }

        public string UniqueID { get; set; }
        public int Type { get; set; }
        public int Category { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Owner { get; set; }
        public string Domain { get; set; }
    }
}
