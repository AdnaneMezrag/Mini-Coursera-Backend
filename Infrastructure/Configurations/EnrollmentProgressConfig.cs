using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Configurations
{
    public class EnrollmentProgressConfiguration : IEntityTypeConfiguration<EnrollmentProgress>
    {
        public void Configure(EntityTypeBuilder<EnrollmentProgress> builder)
        {
            builder.ToTable("EnrollmentProgresses");

            // Composite unique constraint
            builder.HasIndex(e => new { e.EnrollmentId, e.ModuleContentId })
                   .IsUnique();

            // Index on EnrollmentId
            builder.HasIndex(e => e.EnrollmentId);
        }
    }

}
