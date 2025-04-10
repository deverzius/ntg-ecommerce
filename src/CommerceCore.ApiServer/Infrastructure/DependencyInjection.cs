using Ardalis.GuardClauses;
using CommerceCore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace CommerceCore.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        ConfigurationManager configurationManager)
    {
        var connectionString = configurationManager.GetConnectionString("DefaultConnection");

        Guard.Against.NullOrWhiteSpace(
            connectionString,
            message: "Connection string is not found or is white space.");

        services.AddDbContext<CommerceCoreDbContext>(options => options.UseSqlServer(connectionString));

        return services;
    }
}