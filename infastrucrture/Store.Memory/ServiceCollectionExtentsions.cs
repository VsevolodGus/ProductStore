using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Store.Memory
{
    public static class ServiceCollectionExtentsions
    {
        public static IServiceCollection AddEfRepositories(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<StoreDbContext>(
                options =>
                {
                    options.UseSqlServer(connectionString);
                },
                ServiceLifetime.Transient
            );

            services.AddScoped<Dictionary<Type,StoreDbContext>>();
            services.AddSingleton<DbContextFactory>();
            services.AddSingleton<IProductRepository, ProductRepository>();
            services.AddSingleton<IMakerRepository, MakerRepository>();
            services.AddSingleton<IOrderRepository, OrderRepository>();

            return services;
        }
    }
}
