using Hys.Platform.Domain;
using System;

namespace Hys.CareRIS.Domain.Entities
{
    public partial class OnlineClient : Entity
    {
        public override object UniqueId
        {
            get
            {
                return UniqueID;
            }
        }

        public string UniqueID { get; set; }
        public string MachineIP { get; set; }
        public string RoleName { get; set; }
        public string IISUrl { get; set; }
        public DateTime? LoginTime { get; set; }
        public string Comments { get; set; }
        public string SessionID { get; set; }
        public int? IsOnline { get; set; }
        public string Domain { get; set; }
        public string Site { get; set; }
        public string MachineName { get; set; }
        public string MACAddress { get; set; }
        public string Location { get; set; }
    }
}
