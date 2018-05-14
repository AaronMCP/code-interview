using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Application.Dtos.UserManagement
{
    public class UserDto
    {
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
        public string PdfService { get; set; }

        //[User2Domain]
        public string Department { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string DomainLoginName { get; set; }
        public bool IsSetExpireDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        //角色list
        public List<string> RolesName { get; set; }
        public string AccessSites { get; set; }
        //用户配置list
        public List<ProfileDto> UserProfileList { get; set; }

    }
}
