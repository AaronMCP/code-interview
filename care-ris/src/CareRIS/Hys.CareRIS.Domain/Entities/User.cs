using Hys.Platform.Domain;
using System;

namespace Hys.CareRIS.Domain.Entities
{
    public partial class User : Entity
    {
        public override object UniqueId
        {
            get
            {
                return UniqueID;
            }
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
        public string DisplayName { get; set; }
        public int? InvalidLoginCount { get; set; }
        public int? IsLocked { get; set; }
    }
}
