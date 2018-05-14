﻿using Hys.Consultation.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Hys.Consultation.EntityFramework.Mappers
{
    public class ConsultatReportTemplateDirecMapper : EntityTypeConfiguration<ConsultatReportTemplateDirec>
    {
        public ConsultatReportTemplateDirecMapper()
        {
            this.ToTable("dbo.tbConsultatReportTemplateDirec");
            this.HasKey(u => u.UniqueID);
            this.Property(u => u.UniqueID).IsRequired().HasColumnName("ItemGUID").HasMaxLength(128);
            this.Property(u => u.ParentID).IsOptional().HasMaxLength(128);
            this.Property(u => u.Depth).IsOptional();
            this.Property(u => u.ItemName).IsRequired().HasMaxLength(128);
            this.Property(u => u.ItemOrder).IsOptional();
            this.Property(u => u.Type).IsOptional();
            this.Property(u => u.UserID).IsOptional().HasColumnName("UserGuid").HasMaxLength(128);
            this.Property(u => u.TemplateID).IsOptional().HasColumnName("TemplateGuid").HasMaxLength(128);
            this.Property(u => u.Leaf).IsOptional();
            this.Property(u => u.Domain).IsRequired().HasMaxLength(64);
            this.Property(u => u.DirectoryType).IsOptional().HasMaxLength(128);
        }
    }
}
