using System;
using Hys.Platform.Domain;

namespace Hys.Consultation.Domain.Entities
{
    public class ServiceType : Entity
    {
        public override object UniqueId { get { return UniqueID; } }

        public string UniqueID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string LastEditUser { get; set; }
        public DateTime LastEditTime { get; set; }
    }
}
