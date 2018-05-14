using System;
using System.Collections.Generic;

namespace Hys.Consultation.Application.Dtos
{
    public class UserDto
    {
        public UserDto()
        {
            Roles = new List<RoleDto>();
        }
        public string UniqueID { get; set; }
        public string LoginName { get; set; }
        public string LocalName { get; set; }
        public string EnglishName { get; set; }
        public string Password { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string Comments { get; set; }
        public int? DeleteMark { get; set; }
        public byte[] SignImage { get; set; }
        public string PrivateKey { get; set; }
        public string PublicKey { get; set; }
        public string IkeySn { get; set; }
        public string Domain { get; set; }
        public string Site { get; set; }
        public string DisplayName { get; set; }
        public int? InvalidLoginCount { get; set; }
        public bool? IsLocked { get; set; }

        public bool HasValidPeriod { get; set; }
        public DateTime? ValidStartDate { get; set; }
        public DateTime? ValidEndDate { get; set; }
        public String RoleName { get; set; }
        public string Language { get; set; }

        // user extentions
        public string Mobile { get; set; }
        public bool IsMobileChanged { get; set; }

        public string ExpertLevel { get; set; }
        public string ResearchDomain { get; set; }
        public string Introduction { get; set; }
        public string Description { get; set; }
        public string Avatar { get; set; }
        public string LastStatus { get; set; }
        public string HospitalID { get; set; }
        public HospitalProfileDto Hospital { get; set; }
        public string DepartmentID { get; set; }
        public string DefaultRoleID { get; set; }
        public DepartmentDto Department { get; set; }
        public List<RoleDto> Roles { get; set; }
        public string RisRole { get; set; }
    }
}
