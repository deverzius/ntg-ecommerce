using CommerceCore.IdentityServer.Data;
using CommerceCore.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace CommerceCore.IdentityServer.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
            options.UseOpenIddict();
        });

        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services
            .AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                // options.SignIn.RequireConfirmedAccount = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddUserManager<UserManager<ApplicationUser>>()
            .AddDefaultTokenProviders()
            .AddDefaultUI();

        // builder.Services.ConfigureApplicationCookie(options =>
        // {
        //     options.LoginPath = "/authentication/login";
        // });

        builder.Services.AddControllersWithViews();

        builder.Services.AddRazorPages();

        builder.Services
            .AddOpenIddict()
            .AddCore(options =>
            {
                options.UseEntityFrameworkCore()
                       .UseDbContext<ApplicationDbContext>();
            })
            .AddClient(options =>
            {
                options.AllowAuthorizationCodeFlow();

                // Register the signing and encryption credentials used to protect
                // sensitive data like the state tokens produced by OpenIddict.
                options.AddDevelopmentEncryptionCertificate()
                       .AddDevelopmentSigningCertificate();

                options.UseAspNetCore()
                       .EnableStatusCodePagesIntegration()
                       .EnableRedirectionEndpointPassthrough();

                options.UseSystemNetHttp();
                options.SetRedirectionEndpointUris("connect/redirect");
            })
            .AddServer(options =>
            {
                options.AllowAuthorizationCodeFlow();

                // Enable endpoints.
                options.SetAuthorizationEndpointUris("connect/authorize")
                       .SetEndSessionEndpointUris("connect/logout")
                       .SetTokenEndpointUris("connect/token")
                       .SetUserInfoEndpointUris("connect/userinfo");

                options.RegisterScopes(Scopes.Email, Scopes.Profile, Scopes.Roles);

                // Register the signing and encryption credentials.
                options.AddDevelopmentEncryptionCertificate()
                       .AddDevelopmentSigningCertificate();

                options.UseAspNetCore()
                       .EnableAuthorizationEndpointPassthrough()
                       .EnableEndSessionEndpointPassthrough()
                       .EnableTokenEndpointPassthrough()
                       .EnableUserInfoEndpointPassthrough()
                       .EnableStatusCodePagesIntegration();

                // Disable HTTPS only requests for development purposes.
                if (builder.Environment.IsDevelopment())
                {
                    options.UseAspNetCore().DisableTransportSecurityRequirement();
                }
            })
            .AddValidation(options =>
            {
                // Import the configuration from the local OpenIddict server instance.
                options.UseLocalServer();

                options.UseAspNetCore();
            });

        // Register the worker responsible for seeding the database.
        // Note: in a real world application, this step should be part of a setup script.
        builder.Services.AddHostedService<Worker>();
    }
}
