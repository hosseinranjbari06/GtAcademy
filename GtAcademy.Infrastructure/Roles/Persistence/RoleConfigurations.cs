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

            builder.HasData(
                new Role() { RoleId = 1, Title = "ادمین", Description = "دسترسی کامل"},
                new Role() { RoleId = 2, Title = "مدرس", Description = "دسترسی به بخش دوره ها" },
                new Role() { RoleId = 3, Title = "مدیر محصول", Description = "دسترسی به بخش محصولات" },
                new Role() { RoleId = 4, Title = "مدیر کاربران", Description = "دسترسی به بخش کاربران" });
        }
    }
}
