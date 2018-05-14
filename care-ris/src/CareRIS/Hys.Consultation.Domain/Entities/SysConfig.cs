using Hys.Platform.Domain;
using System;

namespace Hys.Consultation.Domain.Entities
{
    public partial class SysConfig : Entity
    {
        public override object UniqueId { get { return UniqueID; } }

        public string UniqueID { get; set; }
        public int Module { get; set; }
        public string GroupName { get; set; }
        public string ConfigOwner { get; set; }
        public string ConfigKey { get; set; }
        public string ConfigValue { get; set; }
        public string ConfigDescription { get; set; }
        public string LastEditUser { get; set; }
        public DateTime LastEditTime { get; set; }
    }
}
