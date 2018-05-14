using Hys.CareRIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.EntityFramework.Mappers
{
    public class ShortcutMapper : EntityTypeConfiguration<Shortcut>
    {
        public ShortcutMapper()
        {
            this.ToTable("dbo.tbShortcut");
            this.HasKey(u => u.UniqueID);

            this.Property(u => u.UniqueID).IsRequired().HasColumnName("ShortcutGuid").HasMaxLength(128);
            this.Property(u => u.Type).IsRequired();
            this.Property(u => u.Category).IsRequired();
            this.Property(u => u.Name).IsRequired().HasMaxLength(128);
            this.Property(u => u.Value).IsOptional().HasMaxLength(4000);
            this.Property(u => u.Owner).IsRequired().HasMaxLength(128);
            this.Property(u => u.Domain).IsOptional().HasMaxLength(64);
        }
    }
}
