using Ardalis.GuardClauses;
using CommerceCore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CommerceCore.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        ConfigurationManager configurationManager
    )
    {
        var connectionString = configurationManager.GetConnectionString("DefaultConnection");

        Guard.Against.NullOrWhiteSpace(
            connectionString,
            message: "Connection string is not found or is white space."
        );

        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString)
        );

        return services;
    }
}
