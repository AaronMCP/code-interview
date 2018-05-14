using Hys.Platform.Domain;

namespace Hys.CareRIS.Domain.Entities
{
    public partial class SystemProfile : Entity
    {
        public override object UniqueId { get { return UniqueID; } }

        public string UniqueID { get; set; }
        public string Name { get; set; }
        public string ModuleID { get; set; }
        public string Value { get; set; }
        public string PropertyDesc { get; set; }
        public string PropertyOptions { get; set; }
        public int IsExportable { get; set; }
        public int CanBeInherited { get; set; }
        public int PropertyType { get; set; }
        public int IsHidden { get; set; }
        public string OrderingPos { get; set; }
        public string Domain { get; set; }
    }
}
