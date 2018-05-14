using System;
using System.Collections.Generic;
using Hys.Platform.Domain;

namespace Hys.Consultation.Domain.Entities
{
    public partial class UserExtention : Entity
    {
        public UserExtention()
        {
            Roles = new List<Role>();
        }

        public override object UniqueId { get { return UniqueID; } }

        public string UniqueID { get; set; }
        public string Avatar { get; set; }
        public string LastStatus { get; set; }
        public string ExpertLevel { get; set; }
        public string ResearchDomain { get; set; }
        public string Introduction { get; set; }
        public string Description { get; set; }
        public string HospitalID { get; set; }
        public HospitalProfile Hospital { get; set; }
        public string DepartmentID { get; set; }
        public Department Department { get; set; }
        public string DefaultRoleID { get; set; }
        public List<Role> Roles { get; set; }
    }
}
