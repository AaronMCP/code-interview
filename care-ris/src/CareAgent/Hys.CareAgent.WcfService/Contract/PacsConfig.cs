using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hys.CareAgent.WcfService.Contract
{
    public class PacsConfig
    {
        public DesktopClient desktopClient { get; set; }
        public WebClient webClient { get; set; }
    }

    public class ClientParam
    {
        public string AETitle { get; set; }
        public string IP { get; set; }
        public int port { get; set; }
        public int HeaderServicePort { get; set; }
        public int WadoServicePort { get; set; }
        public string PatientID { get; set; }
        public string AccessionNumber { get; set; }
    }

    public class DesktopClient
    {
        public bool disabled { get; set; }
        public string path { get; set; }
        public List<string> args { get; set; }
        public ClientParam param { get; set; }
    }

    public class WebClient
    {
        public bool disabled { get; set; }
        public string url { get; set; }
    }
}
