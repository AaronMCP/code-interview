
namespace Hys.CareRIS.Application.Dtos
{
    public class ClientConfigDto
    {
        public string UniqueID { get; set; }

        public int ScanQualityLevel { get; set; }
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

        public bool AppointmentAutoPrintBarcode { get; set; }
        public bool AppointmentAutoPrintNotice { get; set; }

        public int IntegrationType { get; set; }
    }
}
