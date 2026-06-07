using GtAcademy.Domain.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Infrastructure.Roles.Persistence
{
    public class RoleConfigurations : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.RoleId);

            builder.Property(r => r.Title)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(r => r.Description)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
