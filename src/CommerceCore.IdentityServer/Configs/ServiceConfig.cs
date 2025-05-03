using CommerceCore.IdentityServer.Data;
using CommerceCore.IdentityServer.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace CommerceCore.IdentityServer.Configs;

public static class ServiceConfig
{
    public static IServiceCollection AddServices(
        this IServiceCollection services,
        IConfigurationManager configurationManager
    )
    {
        var connectionString =
            configurationManager.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException(
                "Connection string 'DefaultConnection' not found."
            );

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
            options.UseOpenIddict();
        });

        services
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.HttpOnly = false;
                options.Cookie.SameSite = SameSiteMode.Strict;
            });

        services.AddDatabaseDeveloperPageExceptionFilter();

        services
            .AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                // options.SignIn.RequireConfirmedAccount = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddUserManager<UserManager<ApplicationUser>>()
            .AddDefaultTokenProviders()
            .AddDefaultUI();

        // services.ConfigureApplicationCookie(options =>
        // {
        //     options.LoginPath = "/authentication/login";
        // });

        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.WithOrigins("https://localhost:5173");
                policy.WithMethods("GET", "POST");
                policy.AllowCredentials();
                policy.AllowAnyHeader();
            });
        });

        services.AddControllersWithViews();

        services.AddRazorPages();

        services
            .AddOpenIddict()
            .AddCore(options =>
            {
                options.UseEntityFrameworkCore().UseDbContext<ApplicationDbContext>();
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

                options.SetRedirectionEndpointUris("connect/redirect");
            })
            .AddServer(options =>
            {
                options
                    .AllowAuthorizationCodeFlow()
                    .AllowRefreshTokenFlow()
                    .SetAccessTokenLifetime(TimeSpan.FromMinutes(6))
                    .SetIdentityTokenLifetime(TimeSpan.FromMinutes(6))
                    .SetRefreshTokenLifetime(TimeSpan.FromDays(30));

                options
                    .SetAuthorizationEndpointUris("connect/authorize")
                    .SetEndSessionEndpointUris("connect/logout")
                    .SetTokenEndpointUris("connect/token")
                    .SetUserInfoEndpointUris("connect/userinfo");

                options.RegisterScopes(
                    Scopes.Email,
                    Scopes.Profile,
                    Scopes.Roles,
                    Scopes.OfflineAccess
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

        services.AddHostedService<Worker>();

        return services;
    }
}
