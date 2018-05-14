using Hys.CareRIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.EntityFramework.Mappers
{
    class OrderMapper:EntityTypeConfiguration<Order>
    {
        public OrderMapper()
        {
            this.ToTable("dbo.tbRegOrder");
            this.HasKey(u => u.UniqueID);
            this.Property(u => u.UniqueID).IsRequired().HasColumnName("OrderGuid").HasMaxLength(64);
            this.Property(u => u.VisitID).IsRequired().HasColumnName("VisitGuid").HasMaxLength(128);
            this.Property(u => u.AccNo).IsRequired().HasMaxLength(128);
            this.Property(u => u.ApplyDept).IsOptional().HasMaxLength(128);
            this.Property(u => u.ApplyDoctor).IsOptional().HasMaxLength(128);
            this.Property(u => u.CreateTime).HasColumnName("CreateDt").IsOptional();
            this.Property(u => u.IsScan).IsOptional();
            this.Property(u => u.Comments).IsOptional();
            this.Property(u => u.RemoteAccNo).IsOptional().HasMaxLength(128);
            this.Property(u => u.TotalFee).IsOptional();
            this.Property(u => u.Optional1).IsOptional().HasMaxLength(512);
            this.Property(u => u.Optional2).IsOptional().HasMaxLength(512);
            this.Property(u => u.Optional3).IsOptional().HasMaxLength(512);
            this.Property(u => u.StudyInstanceUID).IsOptional().HasMaxLength(128);
            this.Property(u => u.HisID).IsOptional().HasMaxLength(128);
            this.Property(u => u.CardNo).IsOptional().HasMaxLength(128);
            this.Property(u => u.Domain).IsRequired().HasMaxLength(64);
            this.Property(u => u.PatientID).IsRequired().HasColumnName("PatientGuid").HasMaxLength(128);
            this.Property(u => u.InhospitalNo).IsOptional().HasMaxLength(128);
            this.Property(u => u.ClinicNo).IsOptional().HasMaxLength(128);
            this.Property(u => u.PatientType).IsOptional();
            this.Property(u => u.Observation).IsOptional();
            this.Property(u => u.HealthHistory).IsOptional();
            this.Property(u => u.InhospitalRegion).IsOptional().HasMaxLength(128);
            this.Property(u => u.IsEmergency).IsOptional();
            this.Property(u => u.BedNo).IsOptional().HasMaxLength(128);
            this.Property(u => u.CurrentAge).IsOptional().HasMaxLength(128);
            this.Property(u => u.AgeInDays).IsOptional();
            this.Property(u => u.VisitComment).IsOptional().HasColumnName("visitcomment");
            this.Property(u => u.ChargeType).IsOptional().HasMaxLength(50);
            this.Property(u => u.ErethismType).IsOptional().HasMaxLength(32);
            this.Property(u => u.ErethismCode).IsOptional().HasMaxLength(32);
            this.Property(u => u.ErethismGrade).IsOptional().HasMaxLength(32);
            this.Property(u => u.ReferralID).IsOptional().HasMaxLength(64);
            this.Property(u => u.IsReferral).IsOptional();
            this.Property(u => u.ExamAccNo).IsOptional().HasMaxLength(64);
            this.Property(u => u.ExamDomain).IsOptional().HasMaxLength(64);
            this.Property(u => u.InitialDomain).IsOptional().HasMaxLength(64);
            this.Property(u => u.ERequisition).IsOptional();
            this.Property(u => u.CurPatientName).IsOptional().HasMaxLength(128);
            this.Property(u => u.CurGender).IsOptional().HasMaxLength(32);
            this.Property(u => u.Priority).IsOptional();
            this.Property(u => u.IsCharge).IsOptional();
            this.Property(u => u.IsBedside).IsOptional().HasColumnName("Bedside");
            this.Property(u => u.IsFilmSent).IsOptional();
            this.Property(u => u.FilmSentOperator).IsOptional().HasMaxLength(128);
            this.Property(u => u.FilmSentTime).IsOptional().HasColumnName("FilmSentDt");
            this.Property(u => u.OrderMessage).HasColumnType("xml").IsOptional();
            this.Property(u => u.BookingSite).IsOptional().HasMaxLength(64);
            this.Property(u => u.RegSite).IsOptional().HasMaxLength(64);
            this.Property(u => u.ExamSite).IsOptional().HasMaxLength(64);
            this.Property(u => u.BodyWeight).IsOptional();
            this.Property(u => u.FilmFee).IsOptional();
            this.Property(u => u.IsThreeDRebuild).IsOptional().HasColumnName("ThreeDRebuild");
            this.Property(u => u.CurrentSite).IsOptional().HasMaxLength(64);
            this.Property(u => u.AssignTime).IsOptional().HasColumnName("AssignDt");
            this.Property(u => u.Assign2Site).IsOptional().HasMaxLength(64);
            this.Property(u => u.StudyID).IsOptional().HasMaxLength(16);
            this.Property(u => u.PathologicalFindings).IsOptional().HasMaxLength(16);
            this.Property(u => u.InternalOptional1).IsOptional().HasMaxLength(128);
            this.Property(u => u.InternalOptional2).IsOptional().HasMaxLength(128);
            this.Property(u => u.ExternalOptional1).IsOptional().HasMaxLength(128);
            this.Property(u => u.ExternalOptional2).IsOptional().HasMaxLength(128);
            this.Property(u => u.ExternalOptional3).IsOptional().HasMaxLength(128);
            this.Property(u => u.TakeReportDate).IsOptional();
            this.Property(u => u.InjectDose).IsOptional().HasMaxLength(32);
            this.Property(u => u.InjectTime).IsOptional().HasMaxLength(32);
            this.Property(u => u.BodyHeight).IsOptional().HasMaxLength(16);
            this.Property(u => u.BloodSugar).IsOptional().HasMaxLength(32);
            this.Property(u => u.Insulin).IsOptional().HasMaxLength(16);
            this.Property(u => u.GoOnGoTime).IsOptional().HasMaxLength(32);
            this.Property(u => u.SubmitHospital).IsOptional().HasMaxLength(64);
            this.Property(u => u.SubmitDepartment).IsOptional().HasColumnName("SubmitDept").HasMaxLength(32);
            this.Property(u => u.SubmitDoctor).IsOptional().HasMaxLength(32);
            this.Property(u => u.EFilmNumber).IsOptional();
            this.Property(u => u.TerminalCheckinPrintNumber).IsOptional();
            this.Property(u => u.FilmDrawerSign).IsOptional();
            this.Property(u => u.FilmDrawDepartment).IsOptional().HasColumnName("FilmDrawDept").HasMaxLength(128);
            this.Property(u => u.FilmDrawRegion).IsOptional().HasMaxLength(128);
            this.Property(u => u.FilmDrawComment).IsOptional().HasMaxLength(128);
            this.Property(u => u.LotNumber).IsOptional().HasMaxLength(32);
            this.Property(u => u.PhysicalCompany).IsOptional().HasMaxLength(128);
            this.Property(u => u.HeartDisease).IsOptional();
            this.Property(u => u.Hypertension).IsOptional();
            this.Property(u => u.Scoliosis).IsOptional();
            this.Property(u => u.ImagingExamSheets).IsOptional().HasColumnType("xml");
        }
    }
}
