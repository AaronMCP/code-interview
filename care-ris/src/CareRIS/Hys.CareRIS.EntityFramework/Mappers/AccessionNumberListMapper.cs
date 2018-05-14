using Hys.CareRIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.EntityFramework.Mappers
{
    public class AccessionNumberListMapper : EntityTypeConfiguration<AccessionNumberList>
    {
        public AccessionNumberListMapper()
        {
            this.ToTable("dbo.tbAccessionNumberList");
            this.HasKey(u => u.UniqueID);

            this.Property(u => u.UniqueID).IsRequired().HasColumnName("ANLGuid").HasMaxLength(64);
            this.Property(u => u.AccNo).IsRequired().HasMaxLength(64);
            this.Property(u => u.OrderID).IsOptional().HasColumnName("OrderGuid").HasMaxLength(64);
            this.Property(u => u.PatientID).IsOptional().HasColumnName("PatientGuid").HasMaxLength(64);
            this.Property(u => u.HisID).IsOptional().HasMaxLength(64);
            this.Property(u => u.CreateTime).IsOptional().HasColumnName("CreateDt");
        }
    }
}
