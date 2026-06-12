using GtAcademy.Domain.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Infrastructure.Topics.Persistence
{
    public class TopicConfiguration : IEntityTypeConfiguration<Topic>
    {
        public void Configure(EntityTypeBuilder<Topic> builder)
        {
            builder.HasKey(t => t.TopicId);

            builder.Property(t => t.Title)
                .HasMaxLength(40)
                .IsRequired();

            builder.Property(t => t.CourseId)
                .IsRequired();

            builder.Property(t => t.CreateDate)
                .IsRequired();
        }
    }
}
