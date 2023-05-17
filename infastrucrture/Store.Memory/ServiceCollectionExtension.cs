using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Store.Entities;
using Store.IntarfaceRepositroy;
using System;
using System.Collections.Generic;
using System.Linq;

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

        services.AddDbSet();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped(typeof(IUnitOfWork), typeof(StoreDbContext));
        services.AddScoped(typeof(IReadonlyRepository<>), typeof(ReadonlyRepository<>));

        return services;
    }

    /// <summary>
    /// Позволяет инъектить DBSet вместо DbContext, что дает выигрышь в производительности 
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    private static IServiceCollection AddDbSet(this IServiceCollection services)
    {
        var setMethod = typeof(DbContext).GetMethod(nameof(DbContext.Set), Array.Empty<Type>());

        foreach (var type in EntityTypes) 
        {
            var genericSet = setMethod.MakeGenericMethod(type);
            services.AddScoped(
                typeof(DbSet<>).MakeGenericType(type)
                , c => genericSet.Invoke(c.GetService<StoreDbContext>(), Array.Empty<object>()));
        }

        return services;
    }

    /// <summary>
    /// все типы Entities
    /// </summary>
    private static IEnumerable<Type> EntityTypes 
        => typeof(OrderEntity).Assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract) 
                .Where(t => t.Namespace.Contains(typeof(OrderEntity).Namespace))
                .ToArray();
}
