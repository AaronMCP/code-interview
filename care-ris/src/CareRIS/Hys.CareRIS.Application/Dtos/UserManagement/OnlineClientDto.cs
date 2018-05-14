using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Application.Dtos.UserManagement
{
    public class OnlineClientDto
    {
        public string UniqueID { get; set; }
        public string MachineIP { get; set; }
        public string RoleName { get; set; }
        public string IISUrl { get; set; }
        public DateTime? LoginTime { get; set; }
        public string Comments { get; set; }
        public string SessionID { get; set; }
        public bool? IsOnline { get; set; }
        public string Domain { get; set; }
        public string Site { get; set; }
        public string MachineName { get; set; }
        public string MACAddress { get; set; }
        public string Location { get; set; }
    }
}
