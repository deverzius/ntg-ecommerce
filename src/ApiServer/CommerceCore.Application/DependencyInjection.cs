using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace CommerceCore.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // services.AddScoped<IProductService, ProductService>();
        // services.AddScoped<ICategoryService, CategoryService>();
        // services.AddScoped<IBrandService, BrandService>();
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly())
        );

        return services;
    }
}
