using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Domain.Courses;
using GtAcademy.Domain.Forum;
using GtAcademy.Domain.Orders;
using GtAcademy.Domain.Referral;
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

            #region Seed data

            var role1 = new Role() { RoleId = 1, Title = "ادمین", Description = "دسترسی کامل" };
            var role2 = new Role() { RoleId = 2, Title = "مدرس", Description = "دسترسی به بخش دوره ها" };
            var role3 = new Role() { RoleId = 3, Title = "مدیر محصول", Description = "دسترسی به بخش محصولات" };
            var role4 = new Role() { RoleId = 4, Title = "مدیر کاربران", Description = "دسترسی به بخش کاربران" };

            modelBuilder.Entity<Role>().HasData(role1, role2, role3, role4);

            //var user = new User()
            //{
            //    UserId = Guid.NewGuid(),
            //    UserName = "admin",
            //    PhoneNumber = "00000000000",
            //    AvatarName = "default.jpg",
            //    IsActive = false,
            //    ReferralCode = "ADMINREF",
            //    RegisterDate = DateTime.Now,
            //    VerifyToken = "1111",
            //    Roles = [role1]
            //};

            //modelBuilder.Entity<User>().HasData(user);

            //var wallet = new Wallet()
            //{
            //    WalletId = Guid.NewGuid(),
            //    UserId = user.UserId,
            //    WalletBalance = 0
            //};

            //modelBuilder.Entity<Wallet>().HasData(wallet);

            #endregion

            base.OnModelCreating(modelBuilder);
        }

        public async Task CommitAsync()
        {
            await SaveChangesAsync();
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Wallet> Wallets { get; set; }

        public DbSet<WalletIncome> WalletIncomes { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Topic> Topics { get; set; }

        public DbSet<Episode> Episodes { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Referral> Referrals { get; set; }

        public DbSet<ReferralOptions> ReferralOptions { get; set; }

        public DbSet<CourseComment> CourseComments { get; set; }

        public DbSet<CourseCategory> CourseCategories { get; set; }

        public DbSet<ForumQuestion> ForumQuestions { get; set; }

        public DbSet<ForumAnswer> ForumAnswers { get; set; }
    }
}
