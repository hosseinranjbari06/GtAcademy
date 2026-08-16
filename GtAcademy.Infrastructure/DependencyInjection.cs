using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Application.Tools.RandomCodeGenerator;
using GtAcademy.Application.Tools.SmsSender;
using GtAcademy.Infrastructure.Common.Persistence;
using GtAcademy.Infrastructure.Courses.Persistence;
using GtAcademy.Infrastructure.Episodes.Persistence;
using GtAcademy.Infrastructure.Forum.Persistence;
using GtAcademy.Infrastructure.Orders.Persistence;
using GtAcademy.Infrastructure.Permissions.Persistence;
using GtAcademy.Infrastructure.Referrals.Persistence;
using GtAcademy.Infrastructure.Roles.Persistence;
using GtAcademy.Infrastructure.Tools.Persistence.RandomCodeGenerator;
using GtAcademy.Infrastructure.Tools.Persistence.SmsSender;
using GtAcademy.Infrastructure.Topics.Persistence;
using GtAcademy.Infrastructure.Users.Persistence;
using GtAcademy.Infrastructure.Wallets.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructre(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<GtAcademyDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("GtAcademyConnectionString"));
            });

            services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<GtAcademyDbContext>());
            services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
            services.AddScoped<ICodeGenerator, CodeGenerator>();
            services.AddScoped<ISmsSender, SmsSender>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IWalletService, WalletService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IReferralService, ReferralService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<ICourseCommentService, CourseCommentService>();
            services.AddScoped<ICourseCategoryService, CourseCategoryService>();
            services.AddScoped<ITopicService, TopicService>();
            services.AddScoped<IEpisodeService, EpisodeService>();
            services.AddScoped<IQuestionService, QuestionService>();

            return services;
        }
    }
}
