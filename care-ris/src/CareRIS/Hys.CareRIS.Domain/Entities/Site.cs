using Hys.Platform.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Domain.Entities
{
    public partial class Site : Entity
    {
        public string SiteName { get; set; }
        public string Domain { get; set; }
        public string DomainPrefix { get; set; }
        public string Connstring { get; set; }
        public string FtpServer { get; set; }
        public int? FtpPort { get; set; }
        public string FtpUser { get; set; }
        public string FtpPassword { get; set; }
        public string PacsAETitle { get; set; }
        public string Telephone { get; set; }
        public string Address { get; set; }
        public string PacsServer { get; set; }
        public string PacsWebServer { get; set; }
        public int? Tab { get; set; }
        public string Alias { get; set; }
        public string IISUrl { get; set; }
    }
}
