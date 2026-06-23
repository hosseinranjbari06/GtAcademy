using GtAcademy.Domain.Forum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Infrastructure.Forum.Persistence
{
    public class ForumAnswerConfigurations : IEntityTypeConfiguration<ForumAnswer>
    {
        public void Configure(EntityTypeBuilder<ForumAnswer> builder)
        {
            builder.HasKey(a => a.AnswerId);

            builder.Property(a => a.AnswerId)
                .ValueGeneratedNever();

            builder.Property(a => a.Content)
                .IsRequired()
                .HasMaxLength(10000);

            builder.Property(a => a.CreateDate)
                .IsRequired();

            builder.Property(a => a.UserId)
                .IsRequired();

            builder.Property(a => a.QuestionId)
                .IsRequired();

            builder
                .HasOne(a => a.User)
                .WithMany(u => u.ForumAnswers)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(a => a.ForumQuestion)
                .WithMany(u => u.ForumAnswers)
                .HasForeignKey(a => a.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
