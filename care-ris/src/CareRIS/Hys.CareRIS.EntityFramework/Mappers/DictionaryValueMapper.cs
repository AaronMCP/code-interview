using Hys.CareRIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.EntityFramework.Mappers
{
    public class DictionaryValueMapper: EntityTypeConfiguration<DictionaryValue>
    {
        public DictionaryValueMapper()
        {
            this.ToTable("dbo.tbDictionaryValue");
            this.HasKey(u => u.UniqueID);

            this.Property(u => u.Tag).IsRequired();
            this.Property(u => u.Value).IsRequired().HasMaxLength(256);
            this.Property(u => u.Text).IsOptional().HasMaxLength(512);
            this.Property(u => u.IsDefault).IsOptional();
            this.Property(u => u.ShortcutCode).IsOptional().HasMaxLength(512);
            this.Property(u => u.OrderID).IsOptional();
            this.Property(u => u.MapTag).IsOptional().HasColumnName("mapTag");
            this.Property(u => u.MapValue).IsOptional().HasMaxLength(256);
            this.Property(u => u.Site).IsOptional().HasMaxLength(64);
            this.Property(u => u.Domain).IsOptional().HasMaxLength(64);
        }
    }
}
