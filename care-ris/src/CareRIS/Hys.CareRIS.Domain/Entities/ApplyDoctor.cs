using Hys.Platform.Domain;

namespace Hys.CareRIS.Domain.Entities
{
    public partial class ApplyDoctor : Entity
    {
        public override object UniqueId { get { return UniqueID; } }

        public string UniqueID { get; set; }
        public string ApplyDeptID { get; set; }
        public string DoctorName { get; set; }
        public string Gender { get; set; }
        public string Mobile { get; set; }
        public string Telephone { get; set; }
        public string StaffNo { get; set; }
        public string Email { get; set; }
        public string ShortcutCode { get; set; }
        public string Site { get; set; }
        public string Domain { get; set; }
    }
}
