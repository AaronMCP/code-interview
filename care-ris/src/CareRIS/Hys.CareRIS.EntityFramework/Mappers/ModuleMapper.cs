using Hys.CareRIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.EntityFramework.Mappers
{
    public class ModuleMapper : EntityTypeConfiguration<Module>
    {
        public ModuleMapper()
        {
            this.ToTable("dbo.tbModule");
            this.HasKey(u => u.ModuleID);

            this.Property(u => u.ModuleID).IsRequired().HasColumnName("ModuleID").HasMaxLength(128);
            this.Property(u => u.Title).IsRequired().HasMaxLength(128);
            this.Property(u => u.Parameter).IsOptional();
            this.Property(u => u.ImageIndex).IsOptional();
            this.Property(u => u.OrderNo).IsOptional();
            this.Property(u => u.Domain).IsRequired().IsOptional().HasMaxLength(64);
        }
    }
}
