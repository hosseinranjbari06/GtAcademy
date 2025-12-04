using GtAcademy.Domain.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GtAcademy.Infrastructure.Courses.Persistence
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(c => c.CourseId);

            builder.Property(c => c.CourseId)
                .ValueGeneratedNever();

            builder.Property(c => c.Title)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.BannerName)
              .HasMaxLength(100)
              .IsRequired();

            builder.Property(c => c.Description)
              .HasMaxLength(5000)
              .IsRequired();

            builder.Property(c => c.Tags)
              .HasMaxLength(100)
              .IsRequired();

            builder.Property(c => c.CreatorId)
              .IsRequired();
        }
    }
}
