using Hys.Platform.Domain;

namespace Hys.CareRIS.Domain.Entities
{
    public class ClientConfig : Entity
    {
        public override object UniqueId { get { return UniqueID; } }
        public string UniqueID { get; set; }

        public int ScanQualityLevel { get; set; }
        public int IntegrationType { get; set; }

        public string StationName { get; set; }
        public string Location { get; set; }
        
        public string DefaultPrinter { get; set; }
        public string BarcodePrinter { get; set; }
        public string NoticePrinter { get; set; }

        public string DisabledModalities { get; set; }
        public string DisabledModalityTypes { get; set; }

        public bool AutoPrintBarcode { get; set; }
        public bool AutoPrintNotice { get; set; }

        public string AppointmentDisabledModalities { get; set; }
        public string AppointmentDisabledModalityTypes { get; set; }

        public bool? AppointmentAutoPrintBarcode { get; set; }
        public bool? AppointmentAutoPrintNotice { get; set; }
    }
}
