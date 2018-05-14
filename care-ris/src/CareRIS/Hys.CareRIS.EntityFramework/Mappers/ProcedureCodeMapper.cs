using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hys.CareRIS.Domain.Entities;

namespace Hys.CareRIS.EntityFramework.Mappers
{
    public class ProcedureCodeMapper : EntityTypeConfiguration<Procedurecode>
    {
        public ProcedureCodeMapper()
        {
            this.ToTable("dbo.tbProcedureCode");
            this.HasKey(u => u.UniqueID);

            this.Property(u => u.UniqueID).IsRequired().HasColumnName("UniqueID").HasMaxLength(36);
            this.Property(u => u.ProcedureCode).IsRequired().HasMaxLength(128);
            this.Property(u => u.Description).IsOptional().HasMaxLength(512);
            this.Property(u => u.EnglishDescription).IsOptional().HasMaxLength(512);
            this.Property(u => u.ModalityType).IsOptional().HasMaxLength(64);
            this.Property(u => u.BodyPart).IsOptional().HasMaxLength(256);
            this.Property(u => u.CheckingItem).IsOptional().HasMaxLength(256);
            this.Property(u => u.Charge).IsOptional();
            this.Property(u => u.Preparation).IsOptional().HasMaxLength(512);
            this.Property(u => u.Frequency).IsOptional();
            this.Property(u => u.BodyCategory).IsOptional().HasMaxLength(64);
            this.Property(u => u.Duration).IsOptional();
            this.Property(u => u.FilmSpec).IsOptional().HasMaxLength(128);
            this.Property(u => u.FilmCount).IsOptional();
            this.Property(u => u.ContrastName).IsOptional().HasMaxLength(128);
            this.Property(u => u.ContrastDose).IsOptional().HasMaxLength(128);
            this.Property(u => u.ImageCount).IsOptional();
            this.Property(u => u.ExposalCount).IsOptional();
            this.Property(u => u.BookingNotice).IsOptional().HasMaxLength(512);
            this.Property(u => u.ShortcutCode).IsOptional().HasMaxLength(128);
            this.Property(u => u.Enhance).IsOptional();
            this.Property(u => u.ApproveWarningTime).IsOptional();
            this.Property(u => u.Effective).IsOptional();
            this.Property(u => u.Domain).IsRequired().HasMaxLength(128);
            this.Property(u => u.Externals).IsOptional();
            this.Property(u => u.BodypartFrequency).IsOptional();
            this.Property(u => u.CheckingItemFrequency).IsOptional();
            this.Property(u => u.TechnicianWeight).IsRequired();
            this.Property(u => u.RadiologistWeight).IsRequired();
            this.Property(u => u.ApprovedRadiologistWeight).IsRequired();
            this.Property(u => u.DefaultModality).IsOptional().HasMaxLength(64);
            this.Property(u => u.Site).IsOptional().HasMaxLength(64);
            this.Property(u => u.Puncture).IsOptional();
            this.Property(u => u.Radiography).IsOptional();
        }
    }
}
