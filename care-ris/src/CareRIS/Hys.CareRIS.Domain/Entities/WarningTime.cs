using Hys.Platform.Domain;

namespace Hys.CareRIS.Domain.Entities
{
    public partial class WarningTime : Entity
    {
        public override object UniqueId { get { return UniqueID; } }

        public string UniqueID { get; set; }
        public string ModalityType { get; set; }
        public string PatientType { get; set; }
        public int? WarningTimeValue { get; set; }
        public string Type { get; set; }
        public string Site { get; set; }
        public string Domain { get; set; }
    }
}
