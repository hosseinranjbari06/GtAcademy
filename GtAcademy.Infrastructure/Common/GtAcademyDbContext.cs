using GtAcademy.Domain.Courses;
using GtAcademy.Domain.Orders;
using GtAcademy.Domain.Users;
using GtAcademy.Domain.Wallets;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Infrastructure.Common
{
    public class GtAcademyDbContext : DbContext
    {
        public GtAcademyDbContext(DbContextOptions<GtAcademyDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Wallet> Wallets { get; set; }

        public DbSet<Course> Courses { get; set; }
    }
}
