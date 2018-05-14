using Hys.Consultation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.Consultation.EntityFramework.Mappers
{
    public class ConsultationDictionaryMapper : EntityTypeConfiguration<ConsultationDictionary>
    {
        public ConsultationDictionaryMapper()
        {
            this.ToTable("dbo.tbDictionary");
            this.HasKey(u => u.DictionaryID);

            this.Property(u => u.DictionaryID).IsRequired().HasColumnName("DictionaryID").HasMaxLength(128);
            this.Property(u => u.Type).IsRequired().HasColumnName("Type").HasColumnType("int");
            this.Property(u => u.Name).IsRequired().HasColumnName("Name").HasMaxLength(128);
            this.Property(u => u.Value).IsOptional().HasColumnName("Value").HasMaxLength(100);
            this.Property(u => u.Description).IsOptional().HasColumnName("Description").IsMaxLength();
            this.Property(u => u.LastEditUser).HasColumnName("LastEditUser").IsRequired().HasMaxLength(128);
            this.Property(u => u.LastEditTime).HasColumnName("LastEditTime").IsRequired().HasColumnType("datetime");
        }
    }
}
