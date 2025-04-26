using CommerceCore.Application.Common.Interfaces;
using CommerceCore.Application.Services;
using CommerceCore.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace CommerceCore.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ISingleModelService<Product, Guid>, ProductService>();

        return services;
    }
}