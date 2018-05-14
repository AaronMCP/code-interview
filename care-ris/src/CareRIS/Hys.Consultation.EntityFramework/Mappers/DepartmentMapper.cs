using Hys.Consultation.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Hys.Consultation.EntityFramework.Mappers
{
    public class DepartmentMapper : EntityTypeConfiguration<Department>
    {
        public DepartmentMapper()
        {
            this.ToTable("dbo.tbDepartment");
            this.HasKey(u => u.UniqueID);
            this.Property(u => u.Name).HasMaxLength(128).IsRequired();
        }
    }
}
