using System;
using Hys.Platform.Domain;

namespace Hys.Consultation.Domain.Entities
{
    public class InitialDataHistory : Entity
    {
        public override object UniqueId { get { return UniqueID; } }

        public string UniqueID { get; set; }

        public string Version { get; set; }

        public bool IsUpdated { get; set; }

        public string LastEditUser { get; set; }

        public DateTime LastEditTime { get; set; }
    }
}
