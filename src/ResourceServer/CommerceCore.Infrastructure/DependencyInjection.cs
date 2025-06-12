using CommerceCore.Application.Common.Configurations;
using CommerceCore.Application.Common.Interfaces;
using CommerceCore.Application.Common.Interfaces.Repositories;
using CommerceCore.Infrastructure.Data;
using CommerceCore.Infrastructure.Data.Repositories;
using CommerceCore.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CommerceCore.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services
    )
    {
        using var scope = services.BuildServiceProvider().CreateScope();
        var serviceProvider = scope.ServiceProvider;

        var connectionString = serviceProvider.GetRequiredService<IOptions<ConnectionStringsConfigurations>>().Value.DefaultConnection;

        services.AddDbContext<IAppDbContext, AppDbContext>(options =>
            options.UseSqlServer(connectionString)
        );

        services.AddTransient<IStorageService, SupabaseStorageService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductVariantRepository, ProductVariantRepository>();
        services.AddScoped<IImageRepository, ImageRepository>();
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();

        return services;
    }
}
