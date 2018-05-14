using Hys.Consultation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.Consultation.EntityFramework.Mappers
{
    public class ExamModuleMapper : EntityTypeConfiguration<ExamModule>
    {
        public ExamModuleMapper()
        {
            this.ToTable("dbo.tbExamModule");
            this.HasKey(u => u.ID);

            this.Property(u => u.ID).IsRequired();
            this.Property(u => u.Owner).IsOptional().HasMaxLength(128);
            this.Property(u => u.Type).IsRequired().HasMaxLength(64);
            this.Property(u => u.Title).IsRequired().HasMaxLength(512);
            this.Property(u => u.Position).IsRequired().HasMaxLength(100);
            this.Property(u => u.Visible).IsRequired();
            this.Property(u => u.LastEditUser).HasColumnName("LastEditUser").IsRequired().HasMaxLength(128);
            this.Property(u => u.LastEditTime).HasColumnName("LastEditTime").IsRequired();
        }
    }
}
