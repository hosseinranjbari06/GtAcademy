using GtAcademy.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Infrastructure.Users.Persistence
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.UserId);

            builder.Property(u => u.UserId)
                .ValueGeneratedNever();

            builder.Property(u => u.UserName)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(u => u.EmailAddress)
                .HasMaxLength(255);

            builder.Property(u => u.PhoneNumber)
                .HasMaxLength(15);

            builder.Property(u => u.HomeAddress)
                .HasMaxLength(500);

            builder.Property(u => u.Job)
                .HasMaxLength(50);
        }
    }
}
