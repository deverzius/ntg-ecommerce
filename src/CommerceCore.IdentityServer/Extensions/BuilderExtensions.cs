using CommerceCore.IdentityServer.Configurations;
using CommerceCore.IdentityServer.Data;
using CommerceCore.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace CommerceCore.IdentityServer.Extensions;

public static class BuilderExtensions
{
    public static IServiceCollection AddAppConfigurations(
       this IServiceCollection services,
       IConfigurationManager configurationManager
    )
    {
        services.Configure<ConnectionStringsConfigurations>(configurationManager.GetSection("ConnectionStrings"));
        services.Configure<IdentityServerConfigurations>(configurationManager.GetSection("IdentityServer"));
        services.Configure<CorsConfigurations>(configurationManager.GetSection("Cors"));

        return services;
    }

    public static IServiceCollection AddAppDbContext(
       this IServiceCollection services
    )
    {
        using var scope = services.BuildServiceProvider().CreateScope();
        var serviceProvider = scope.ServiceProvider;

        var connectionString = serviceProvider.GetRequiredService<IOptions<ConnectionStringsConfigurations>>().Value.DefaultConnection;

        services.AddDbContext<IdentityDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
            options.UseOpenIddict();
        });

        return services;
    }

    public static IServiceCollection AddAppRequiredServices(
        this IServiceCollection services
    )
    {
        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddControllersWithViews();

        services.AddRazorPages();

        return services;
    }

    public static IServiceCollection AddAppIdentity(
       this IServiceCollection services
    )
    {
        services
            .AddIdentity<AppUser, IdentityRole>(options =>
            {
                // options.SignIn.RequireConfirmedAccount = true;
            })
            .AddEntityFrameworkStores<IdentityDbContext>()
            .AddUserManager<UserManager<AppUser>>()
            .AddDefaultTokenProviders()
            .AddDefaultUI();

        return services;
    }

    public static IServiceCollection AddAppSecurity(
       this IServiceCollection services
    )
    {
        // CORS
        using var scope = services.BuildServiceProvider().CreateScope();
        var serviceProvider = scope.ServiceProvider;

        var corsConfigurations = serviceProvider.GetRequiredService<IOptions<CorsConfigurations>>().Value;

        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.WithOrigins(corsConfigurations.AllowedOrigins)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
        });

        // Cookie
        services.ConfigureApplicationCookie(options =>
        {
            // options.LoginPath = "/authentication/login";
            options.Cookie.HttpOnly = true;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            options.Cookie.SameSite = SameSiteMode.Lax;
        });

        // OpenIddict
        var identityServerConfigurations = serviceProvider.GetRequiredService<IOptions<IdentityServerConfigurations>>().Value;
        var serverConfigurations = identityServerConfigurations.Server;
        var clientConfigurations = identityServerConfigurations.Client;

        services
            .AddOpenIddict()
            .AddCore(options =>
            {
                options.UseEntityFrameworkCore().UseDbContext<IdentityDbContext>();
            })
            .AddClient(options =>
            {
                options.AllowAuthorizationCodeFlow().AllowRefreshTokenFlow();

                options.AddDevelopmentEncryptionCertificate().AddDevelopmentSigningCertificate();

                options
                    .UseAspNetCore()
                    .EnableStatusCodePagesIntegration()
                    .EnableRedirectionEndpointPassthrough();

                options.UseSystemNetHttp();

                options.SetRedirectionEndpointUris(clientConfigurations.RedirectionEndpointUris);
            })
            .AddServer(options =>
            {
                options
                    .AllowAuthorizationCodeFlow()
                    .AllowRefreshTokenFlow()
                    .SetAccessTokenLifetime(TimeSpan.FromHours(3))
                    .SetRefreshTokenLifetime(TimeSpan.FromDays(30));

                options
                    .SetAuthorizationEndpointUris(serverConfigurations.AuthorizationEndpointUris)
                    .SetEndSessionEndpointUris(serverConfigurations.EndSessionEndpointUris)
                    .SetTokenEndpointUris(serverConfigurations.TokenEndpointUris)
                    .SetUserInfoEndpointUris(serverConfigurations.UserInfoEndpointUris);

                options.RegisterScopes(
                    Scopes.Email,
                    Scopes.Profile,
                    Scopes.Roles,
                    Scopes.OfflineAccess
                );

                options.AddEncryptionKey(
                    new SymmetricSecurityKey(Convert.FromBase64String(identityServerConfigurations.EncryptionKey))
                );

                options.AddDevelopmentEncryptionCertificate().AddDevelopmentSigningCertificate();

                options
                    .UseAspNetCore()
                    .EnableAuthorizationEndpointPassthrough()
                    .EnableEndSessionEndpointPassthrough()
                    .EnableTokenEndpointPassthrough()
                    .EnableUserInfoEndpointPassthrough()
                    .EnableStatusCodePagesIntegration();
            })
            .AddValidation(options =>
            {
                options.UseLocalServer();
                options.UseAspNetCore();
            });

        return services;
    }

    public static IServiceCollection AddWorkers(
       this IServiceCollection services
    )
    {
        services.AddHostedService<Worker>();

        return services;
    }
}