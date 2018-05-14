using System;
using Hys.Platform.Domain;

namespace Hys.Consultation.Domain.Entities
{
    public partial class Department : Entity
    {
        public override object UniqueId { get { return UniqueID; } }

        public string UniqueID { get; set; }
        public string Name { get; set; }
    }
}
