using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Domain.Courses;
using GtAcademy.Domain.Orders;
using GtAcademy.Domain.Roles;
using GtAcademy.Domain.Users;
using GtAcademy.Domain.Wallets;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace GtAcademy.Infrastructure.Common.Persistence
{
    public class GtAcademyDbContext : DbContext, IUnitOfWork
    {
        public GtAcademyDbContext(DbContextOptions<GtAcademyDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        public async Task CommitAsync()
        {
            await SaveChangesAsync();
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Wallet> Wallets { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<CourseComment> CourseComments { get; set; }

        public DbSet<CourseCategory> CourseCategories { get; set; }
    }
}
