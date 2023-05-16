using Microsoft.Extensions.DependencyInjection;
using ProductStore.Web.App.Service;

namespace ProductStore.Web.App;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IMakerService, MakerService>();
        services.AddScoped<IProductService, ProductService>();

        return services;
    }
}
