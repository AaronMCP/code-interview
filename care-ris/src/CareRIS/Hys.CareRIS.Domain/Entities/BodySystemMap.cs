using Hys.Platform.Domain;

namespace Hys.CareRIS.Domain.Entities
{
    public partial class BodySystemMap : Entity
    {
        public override object UniqueId { get { return UniqueID; } }
 
        public string UniqueID { get; set; }
        public string ModalityType { get; set; }
        public string BodyPart { get; set; }
        public string ExamSystem { get; set; }
        public string Site { get; set; }
        public string Domain { get; set; }
    }
}
