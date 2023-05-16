using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Store.Memory;

public static class ServiceCollectionExtension
{
    /// <summary>
    /// Добавление регистрация в DI для работы с ef core
    /// </summary>
    /// <param name="services">коллекция сервисов</param>
    /// <param name="connectionString">строка подключения к БД</param>
    /// <returns>коллекция сервисов</returns>
    public static IServiceCollection AddEfRepositories(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<StoreDbContext>(
            options =>
            {
                options.UseSqlServer(connectionString);
            },
            ServiceLifetime.Transient
        );

        services.AddScoped(typeof(IReadonlyRepository<>), typeof(ReadonlyRepository<>));

        return services;
    }
}
