using System;
using Hys.Platform.Domain;

namespace Hys.Consultation.Domain.Entities
{
    public partial class Role : Entity
    {
        public override object UniqueId { get { return UniqueID; } }

        public string UniqueID { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public string Permissions { get; set; }
        public DateTime LastEditTime { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsSystem { get; set; }
    }
}
