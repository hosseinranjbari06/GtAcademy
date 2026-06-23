using GtAcademy.Domain.Forum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Infrastructure.Forum.Persistence
{
    public class ForumQuestionConfigurations : IEntityTypeConfiguration<ForumQuestion>
    {
        public void Configure(EntityTypeBuilder<ForumQuestion> builder)
        {
            builder.HasKey(q => q.QuestionId);

            builder.Property(q => q.QuestionId)
                .ValueGeneratedNever();

            builder.Property(q => q.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(q => q.Content)
                .IsRequired()
                .HasMaxLength(10000);

            builder.Property(q => q.CreateDate)
                .IsRequired();

            builder.Property(q => q.CourseId)
                .IsRequired();

            builder.Property(q => q.UserId)
                .IsRequired();

            builder
                .HasOne(q => q.User)
                .WithMany(u => u.ForumQuestions)
                .HasForeignKey(q => q.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
