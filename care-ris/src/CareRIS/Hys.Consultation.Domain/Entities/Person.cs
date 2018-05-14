using Hys.Platform.Domain;

namespace Hys.Consultation.Domain.Entities
{
    public partial class Person : Entity
    {
        public override object UniqueId { get { return UniqueID; } }

        public string UniqueID { get; set; }
        public string PatientNo { get; set; }
    }
}
