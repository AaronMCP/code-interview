using Hys.CareRIS.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Hys.CareRIS.EntityFramework.Mappers
{
    public class GWDataIndexMapper : EntityTypeConfiguration<GWDataIndex>
    {
        public GWDataIndexMapper()
        {
            this.ToTable("dbo.tbGwDataIndex");
            this.HasKey(u => u.UniqueID);
            this.Property(u => u.UniqueID).IsRequired().HasColumnName("DATA_ID").HasMaxLength(128);
            this.Property(u => u.DataTime).IsRequired().HasColumnName("DATA_DT");

            this.Property(u => u.EventType).IsRequired().HasColumnName("EVENT_TYPE").HasMaxLength(128);
            this.Property(u => u.RecordIndex1).IsOptional().HasColumnName("RECORD_INDEX_1").HasMaxLength(512);
            this.Property(u => u.RecordIndex2).IsOptional().HasColumnName("RECORD_INDEX_2").HasMaxLength(512);
            this.Property(u => u.RecordIndex3).IsOptional().HasColumnName("RECORD_INDEX_3").HasMaxLength(512);
            this.Property(u => u.RecordIndex4).IsOptional().HasColumnName("RECORD_INDEX_4").HasMaxLength(512);
            this.Property(u => u.DataSource).IsOptional().HasColumnName("DATA_SOURCE").HasMaxLength(128);
            //this.Property(u => u.ProcessFlag).IsRequired().HasColumnName("PROCESS_FLAG").HasMaxLength(128);

        }
    }
}
