using CommerceCore.Application.Common.Interfaces;
using CommerceCore.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CommerceCore.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IBrandService, BrandService>();

        return services;
    }
}