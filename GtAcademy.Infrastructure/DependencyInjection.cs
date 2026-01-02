using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Application.Tools.RandomCodeGenerator;
using GtAcademy.Infrastructure.Common.Persistence;
using GtAcademy.Infrastructure.Courses.Persistence;
using GtAcademy.Infrastructure.Orders.Persistence;
using GtAcademy.Infrastructure.Tools.Persistence;
using GtAcademy.Infrastructure.Users.Persistence;
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
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IOrderService, OrderService>();

            return services;
        }
    }
}
