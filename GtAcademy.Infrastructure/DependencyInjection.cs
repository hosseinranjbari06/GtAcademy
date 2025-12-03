using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Infrastructure.Common.Persistence;
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
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
