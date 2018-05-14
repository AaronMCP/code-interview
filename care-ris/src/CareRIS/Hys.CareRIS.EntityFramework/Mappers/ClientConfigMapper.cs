using Hys.CareRIS.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Hys.CareRIS.EntityFramework.Mappers
{
    public class ClientConfigMapper : EntityTypeConfiguration<ClientConfig>
    {
        public ClientConfigMapper()
        {
            this.ToTable("dbo.tbClientConfig");
            this.HasKey(u => u.UniqueID);
            this.Property(u => u.UniqueID).IsRequired().HasMaxLength(128);
            
            this.Property(u => u.StationName).HasMaxLength(200);
            this.Property(u => u.Location).HasMaxLength(200);

            this.Property(u => u.DefaultPrinter);
            this.Property(u => u.BarcodePrinter);
            this.Property(u => u.NoticePrinter);
            
            this.Property(u => u.ScanQualityLevel);
            this.Property(u => u.IntegrationType);

            this.Property(u => u.DisabledModalities);
            this.Property(u => u.DisabledModalityTypes);

            this.Property(u => u.AutoPrintBarcode);
            this.Property(u => u.AutoPrintNotice);

            this.Property(u => u.AppointmentDisabledModalities);
            this.Property(u => u.AppointmentDisabledModalityTypes);

            this.Property(u => u.AppointmentAutoPrintBarcode);
            this.Property(u => u.AppointmentAutoPrintNotice);
        }
    }
}
