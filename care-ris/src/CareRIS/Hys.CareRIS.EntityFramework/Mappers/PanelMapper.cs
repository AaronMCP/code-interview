using Hys.CareRIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.EntityFramework.Mappers
{
    public class PanelMapper : EntityTypeConfiguration<Panel>
    {
        public PanelMapper()
        {
            this.ToTable("dbo.tbPanelMapper");
            this.HasKey(u => u.PanelID);
            this.Property(u => u.PanelID).IsRequired().HasColumnName("PanelID").HasMaxLength(128);
            this.Property(u => u.ModuleID).IsRequired().HasColumnName("ModuleID").HasMaxLength(128);
            this.Property(u => u.Title).IsRequired().HasMaxLength(128);
            this.Property(u => u.AssemblyQualifiedName).IsOptional().HasMaxLength(256);
            this.Property(u => u.Parameter).IsOptional();
            this.Property(u => u.Flag).IsOptional().HasMaxLength(128);
            this.Property(u => u.ImageIndex).IsOptional();
            this.Property(u => u.PanelKey).IsRequired().HasColumnName("Key");
            this.Property(u => u.OrderNo).IsOptional();
            this.Property(u => u.Domain).IsRequired().IsOptional().HasMaxLength(64);
        }
    }
}
