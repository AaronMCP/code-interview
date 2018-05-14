using Hys.Consultation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.Consultation.EntityFramework.Mappers
{
    public class PersonMapper : EntityTypeConfiguration<Person>
    {
        public PersonMapper()
        {
            this.ToTable("dbo.tbPerson");
            this.HasKey(u => u.UniqueID);

            this.Property(u => u.UniqueID).IsRequired().HasColumnName("PersonID").HasMaxLength(128);
            this.Property(u => u.PatientNo).IsRequired().HasColumnName("PatientID").HasMaxLength(128).HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(
                new IndexAttribute("Person_PatientID_Index", 1) { IsUnique = false }));
        }
    }
}
