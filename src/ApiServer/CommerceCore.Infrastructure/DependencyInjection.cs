using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using CommerceCore.Infrastructure.Data;

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

        services.AddDbContext<DbContext, ApplicationDbContext>(options => options.UseSqlServer(connectionString));

        return services;
    }
}