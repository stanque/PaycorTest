using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaycorTest.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaycorTest.Service.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AdvantureWorksContext>
            (
                options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
            );

            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
            services.AddScoped(typeof(IRepository<,,>), typeof(Repository<,,>));
            services.AddScoped(typeof(IRepository<,,,>), typeof(Repository<,,,>));

            return services;
        }
    }
}
