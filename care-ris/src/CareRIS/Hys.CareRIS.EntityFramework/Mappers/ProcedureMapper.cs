using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hys.CareRIS.Domain.Entities;

namespace Hys.CareRIS.EntityFramework.Mappers
{
    public class ProcedureMapper : EntityTypeConfiguration<Procedure>
    {
        public ProcedureMapper()
        {
            this.ToTable("dbo.tbRegProcedure");
            this.HasKey(u => u.UniqueID);
            this.Property(u => u.UniqueID).IsRequired().HasColumnName("ProcedureGuid").HasMaxLength(128);
            this.Property(u => u.OrderID).IsRequired().HasColumnName("OrderGuid").HasMaxLength(128);
            this.Property(u => u.ReportID).IsOptional().HasColumnName("ReportGuid").HasMaxLength(128);
            this.Property(u => u.ProcedureCode).IsRequired().HasMaxLength(128);
            this.Property(u => u.ExamSystem).IsOptional().HasMaxLength(128);
            this.Property(u => u.WarningTime).IsRequired();
            this.Property(u => u.FilmSpec).IsOptional().HasMaxLength(128);
            this.Property(u => u.FilmCount).IsOptional();
            this.Property(u => u.ContrastName).IsOptional().HasMaxLength(128);
            this.Property(u => u.ContrastDose).IsOptional().HasMaxLength(128);
            this.Property(u => u.ImageCount).IsOptional();
            this.Property(u => u.ExposalCount).IsOptional();
            this.Property(u => u.Deposit).IsOptional();
            this.Property(u => u.Charge).IsOptional();
            this.Property(u => u.ModalityType).IsRequired().HasMaxLength(128);
            this.Property(u => u.Modality).IsRequired().HasMaxLength(128);
            this.Property(u => u.Registrar).IsOptional().HasMaxLength(128);
            this.Property(u => u.RegisterTime).IsOptional().HasColumnName("RegisterDt");
            this.Property(u => u.Technician).IsOptional().HasMaxLength(512);
            this.Property(u => u.TechDoctor).IsOptional().HasMaxLength(128);
            this.Property(u => u.TechNurse).IsOptional().HasMaxLength(128);
            this.Property(u => u.OperationStep).IsOptional().HasMaxLength(512);
            this.Property(u => u.ExamineTime).IsOptional().HasColumnName("ExamineDt");
            this.Property(u => u.Mender).IsOptional().HasMaxLength(128);
            this.Property(u => u.ModifyTime).IsOptional().HasColumnName("ModifyDt");
            this.Property(u => u.IsExistImage).IsOptional();
            this.Property(u => u.Status).IsRequired();
            this.Property(u => u.Comments).IsOptional().HasMaxLength(1024);
            this.Property(u => u.BookingBeginTime).IsOptional().HasColumnName("BookingBeginDt");
            this.Property(u => u.BookingEndTime).IsOptional().HasColumnName("BookingEndDt");
            this.Property(u => u.Booker).IsOptional().HasMaxLength(128);
            this.Property(u => u.IsCharge).IsOptional();
            this.Property(u => u.RemoteRPID).IsOptional().HasMaxLength(512);
            this.Property(u => u.Optional1).IsOptional().HasMaxLength(512);
            this.Property(u => u.Optional2).IsOptional().HasMaxLength(512);
            this.Property(u => u.Optional3).IsOptional().HasMaxLength(512);
            this.Property(u => u.QueueNo).IsOptional().HasMaxLength(128);
            this.Property(u => u.BookingNotice).IsOptional();
            this.Property(u => u.BookingTimeAlias).IsOptional().HasMaxLength(128);
            this.Property(u => u.CreateTime).IsOptional().HasColumnName("CreateDt");
            this.Property(u => u.Technician1).IsOptional().HasMaxLength(62);
            this.Property(u => u.Technician2).IsOptional().HasMaxLength(62);
            this.Property(u => u.Technician3).IsOptional().HasMaxLength(62);
            this.Property(u => u.Technician4).IsOptional().HasMaxLength(62);
            this.Property(u => u.Domain).IsRequired().HasMaxLength(64);
            this.Property(u => u.PreStatus).IsOptional();
            this.Property(u => u.BookerName).IsOptional().HasMaxLength(32);
            this.Property(u => u.RegistrarName).IsOptional().HasMaxLength(32);
            this.Property(u => u.TechnicianName).IsOptional().HasMaxLength(32);
            this.Property(u => u.BodyCategory).IsOptional().HasMaxLength(64);
            this.Property(u => u.BodyPart).IsOptional().HasColumnName("Bodypart").HasMaxLength(64);
            this.Property(u => u.CheckingItem).IsOptional().HasMaxLength(64);
            this.Property(u => u.RPDesc).IsOptional().HasMaxLength(64);
            this.Property(u => u.ScanDelayTime).IsOptional().HasMaxLength(128);
            this.Property(u => u.CheckItemName).IsOptional().HasMaxLength(512);
            this.Property(u => u.Posture).IsOptional().HasMaxLength(128);
            this.Property(u => u.MedicineUsage).IsOptional().HasMaxLength(512);
        }
    }
}
