using GtAcademy.Domain.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GtAcademy.Infrastructure.Courses.Persistence
{
    public class EpisodeConfiguration : IEntityTypeConfiguration<Episode>
    {
        public void Configure(EntityTypeBuilder<Episode> builder)
        {
            builder.HasKey(e => e.EpisodeId);

            builder.Property(e => e.EpisodeId)
                .ValueGeneratedNever();

            builder.Property(e => e.Title)
                .HasMaxLength(40)
                .IsRequired();

            builder.Property(e => e.Time)
                .IsRequired();

            builder.Property(e => e.FileName)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(e => e.CourseId)
                .IsRequired();

            builder.Property(e => e.TopicId)
                .IsRequired();
        }
    }
}
