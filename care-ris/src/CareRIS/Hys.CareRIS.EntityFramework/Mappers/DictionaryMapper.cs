using Hys.CareRIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.EntityFramework.Mappers
{
    public class DictionaryMapper : EntityTypeConfiguration<Dictionary>
    {
        public DictionaryMapper()
        {
            this.ToTable("dbo.tbDictionary");
            this.HasKey(u => u.Tag);

            this.Property(u => u.Tag).IsRequired();
            this.Property(u => u.Name).IsRequired().HasMaxLength(128);
            this.Property(u => u.IsHidden).IsOptional();
            this.Property(u => u.Description).IsOptional().HasMaxLength(512);
            this.Property(u => u.Domain).IsOptional().HasMaxLength(64);
        }
    }
}
