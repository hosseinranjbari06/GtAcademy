using GtAcademy.Domain.Courses;
using GtAcademy.Domain.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Infrastructure.Courses.Persistence
{
    public class CourseCommentConfigurations : IEntityTypeConfiguration<CourseComment>
    {
        public void Configure(EntityTypeBuilder<CourseComment> builder)
        {
            builder.HasKey(c => c.CommentId);

            builder.Property(c => c.CommentId)
                .ValueGeneratedNever();

            builder.Property(c => c.Content)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(c => c.UserId)
                .IsRequired();

            builder.Property(c => c.CourseId)
                .IsRequired();
        }
    }
}
