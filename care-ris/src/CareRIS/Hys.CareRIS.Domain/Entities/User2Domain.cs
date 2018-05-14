using System;
using Hys.Platform.Domain;

namespace Hys.CareRIS.Domain.Entities
{
    public class User2Domain : Entity
    {
        public override object UniqueId { get { return UniqueID; } }

        public string UniqueID { get; set; }

        public string Department { get; set; }
        public string DomainLoginName { get; set; }
        public string Telephone { get; set; }
        public int? IsSetExpireDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Domain { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
    }
}
