using Hys.CareRIS.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Hys.CareRIS.EntityFramework.Mappers
{
    public class ReportMapper : EntityTypeConfiguration<Report>
    {
        public ReportMapper()
        {
            this.ToTable("dbo.tbReport");
            this.HasKey(u => u.UniqueID);
            this.Property(u => u.UniqueID).IsRequired().HasColumnName("ReportGuid").HasMaxLength(128);
            this.Property(u => u.ReportName).IsRequired().HasMaxLength(512);
            this.Property(u => u.WYS).IsOptional();
            this.Property(u => u.WYG).IsOptional();
            this.Property(u => u.AppendInfo).IsOptional();
            this.Property(u => u.ReportText).IsOptional();
            this.Property(u => u.DoctorAdvice).IsOptional();
            this.Property(u => u.IsPositive).IsOptional();
            this.Property(u => u.AcrCode).IsOptional().HasMaxLength(128);
            this.Property(u => u.AcrAnatomic).IsOptional().HasMaxLength(128);
            this.Property(u => u.AcrPathologic).IsOptional().HasMaxLength(128);
            this.Property(u => u.Creater).IsOptional().HasMaxLength(128);
            this.Property(u => u.CreateTime).HasColumnName("CreateDt").IsOptional();
            this.Property(u => u.Submitter).IsOptional().HasMaxLength(128);
            this.Property(u => u.SubmitTime).HasColumnName("SubmitDt").IsOptional();
            this.Property(u => u.FirstApprover).IsOptional().HasMaxLength(128);
            this.Property(u => u.FirstApproveTime).HasColumnName("FirstApproveDt").IsOptional();
            this.Property(u => u.SecondApprover).IsOptional().HasMaxLength(128);
            this.Property(u => u.SecondApproveTime).IsOptional().HasColumnName("SecondApproveDt");
            this.Property(u => u.IsDiagnosisRight).IsOptional();
            this.Property(u => u.KeyWord).IsOptional().HasMaxLength(128);
            this.Property(u => u.ReportQuality).IsOptional().HasMaxLength(128);
            this.Property(u => u.RejectToObject).IsOptional().HasMaxLength(128);
            this.Property(u => u.Rejecter).IsOptional().HasMaxLength(128);
            this.Property(u => u.RejectTime).IsOptional().HasColumnName("RejectDt");
            this.Property(u => u.Status).IsOptional();
            this.Property(u => u.Comments).IsOptional();
            //this.Property(u => u.DeleteMark).IsOptional();
            //this.Property(u => u.Deleter).IsOptional().HasMaxLength(128);
            //this.Property(u => u.DeleteTime).HasColumnName("DeleteDt").IsOptional();
            //this.Property(u => u.Recuperator).IsOptional().HasMaxLength(128);
            //this.Property(u => u.ReconvertDt).IsOptional();
            this.Property(u => u.Mender).IsOptional().HasMaxLength(128);
            this.Property(u => u.ModifyTime).IsOptional().HasColumnName("ModifyDt");
            this.Property(u => u.IsPrint).IsOptional();
            this.Property(u => u.CheckItemName).IsOptional().HasMaxLength(512);
            this.Property(u => u.Optional1).IsOptional().HasMaxLength(512);
            this.Property(u => u.Optional2).IsOptional().HasMaxLength(512);
            this.Property(u => u.Optional3).IsOptional().HasMaxLength(512);
            this.Property(u => u.IsLeaveWord).IsOptional();
            this.Property(u => u.WYSText).IsOptional();
            this.Property(u => u.WYGText).IsOptional();
            this.Property(u => u.IsDraw).IsOptional();
            this.Property(u => u.DrawerSign).IsOptional();
            this.Property(u => u.DrawTime).IsOptional();
            this.Property(u => u.IsLeaveSound).IsOptional();
            this.Property(u => u.TakeFilmDept).IsOptional().HasMaxLength(128);
            this.Property(u => u.TakeFilmRegion).IsOptional().HasMaxLength(128);
            this.Property(u => u.TakeFilmComment).IsOptional().HasMaxLength(512);
            this.Property(u => u.PrintCopies).IsOptional();
            this.Property(u => u.PrintTemplateID).IsOptional().HasColumnName("PrintTemplateGuid").HasMaxLength(128);
            this.Property(u => u.Domain).IsRequired().HasMaxLength(64);
            this.Property(u => u.ReadOnly).IsOptional();
            this.Property(u => u.SubmitDomain).IsOptional().HasMaxLength(64);
            this.Property(u => u.RejectDomain).IsOptional().HasMaxLength(64);
            this.Property(u => u.FirstApproveDomain).IsOptional().HasMaxLength(64);
            this.Property(u => u.SecondApproveDomain).IsOptional().HasMaxLength(64);
            //this.Property(u => u.ReportTextApprovedSign).IsOptional();
            //this.Property(u => u.ReportTextSubmittedSign).IsOptional();
            //this.Property(u => u.CombinedForCertification).IsOptional();
            //this.Property(u => u.SignCombinedForCertification).IsOptional();
            this.Property(u => u.RejectSite).IsOptional().HasMaxLength(64);
            this.Property(u => u.SubmitSite).IsOptional().HasMaxLength(64);
            this.Property(u => u.FirstApproveSite).IsOptional().HasMaxLength(64);
            this.Property(u => u.SecondApproveSite).IsOptional().HasMaxLength(64);
            this.Property(u => u.RebuildMark).IsOptional();
            this.Property(u => u.ReportQuality2).IsOptional().HasMaxLength(256);
            this.Property(u => u.UpdateTime).IsOptional();
            this.Property(u => u.Uploaded).IsOptional();
            this.Property(u => u.SubmitterName).IsOptional().HasMaxLength(32);
            this.Property(u => u.FirstApproverName).IsOptional().HasMaxLength(32);
            this.Property(u => u.SecondApproverName).IsOptional().HasMaxLength(32);
            this.Property(u => u.ReportQualityComments).IsOptional();
            this.Property(u => u.CreaterName).IsOptional().HasMaxLength(32);
            this.Property(u => u.MenderName).IsOptional().HasMaxLength(32);
            this.Property(u => u.TechInfo).IsOptional();
            this.Property(u => u.TerminalReportPrintNumber).IsOptional();
            this.Property(u => u.ScoringVersion).IsRequired().HasMaxLength(256);
            this.Property(u => u.AccordRate).IsRequired().HasMaxLength(256);
            //this.Property(u => u.SubmitterSign).IsOptional();
            //this.Property(u => u.FirstApproverSign).IsOptional();
            //this.Property(u => u.SecondApproverSign).IsOptional();
            //this.Property(u => u.SubmitterSignTimeStamp).IsOptional();
            //this.Property(u => u.FirstApproverSignTimeStamp).IsOptional();
            //this.Property(u => u.SecondApproverSignTimeStamp).IsOptional();
            this.Property(u => u.IsModified).IsOptional();
        }
    }
}
