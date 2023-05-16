using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Store.IntarfaceRepositroy;

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
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped(typeof(IUnitOfWork), typeof(StoreDbContext));

        return services;
    }
}
