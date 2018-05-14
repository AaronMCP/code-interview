using Hys.Platform.Domain;

namespace Hys.CareRIS.Domain.Entities
{
    public partial class ApplyDept : Entity
    {
        public override object UniqueId { get { return UniqueID; } }

        public string UniqueID { get; set; }
        public string DeptName { get; set; }
        public string Telephone { get; set; }
        public string ShortcutCode { get; set; }
        public string Site { get; set; }
        public string Domain { get; set; }
    }
}
