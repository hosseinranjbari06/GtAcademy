using GtAcademy.Domain.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Infrastructure.Courses.Persistence
{
    public class CourseCategoryConfigurations : IEntityTypeConfiguration<CourseCategory>
    {
        public void Configure(EntityTypeBuilder<CourseCategory> builder)
        {
            builder.HasKey(c => c.CategoryId);

            builder.Property(c => c.CategoryId)
                .ValueGeneratedNever();

            builder.Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
