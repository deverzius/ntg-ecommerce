using System.Text.Json.Serialization;
using CommerceCore.Application.Common.Configurations;
using CommerceCore.WebApi.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OpenIddict.Validation.AspNetCore;

namespace CommerceCore.WebApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAppConfigurations(
           this IServiceCollection services,
           IConfigurationManager configurationManager
        )
    {
        services.Configure<ConnectionStringsConfigurations>(configurationManager.GetSection("ConnectionStrings"));
        services.Configure<IdentityServerConfigurations>(configurationManager.GetSection("IdentityServer"));
        services.Configure<CorsConfigurations>(configurationManager.GetSection("Cors"));
        services.Configure<SupabaseStorageConfigurations>(configurationManager.GetSection("SupabaseStorage"));

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
                policy
                    .WithOrigins(corsConfigurations.AllowedOrigins)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
        });

        // OpenIddict
        services.AddOpenIddict()
            .AddServer(options =>
            {
                options.AddDevelopmentSigningCertificate();
            })
            .AddValidation(options =>
            {
                var identityServerConfigurations = serviceProvider.GetRequiredService<IOptions<IdentityServerConfigurations>>().Value;

                options.SetIssuer(identityServerConfigurations.Authority);

                var encryptionKey = identityServerConfigurations.EncryptionKey;
                options.AddEncryptionKey(
                    new SymmetricSecurityKey(Convert.FromBase64String(encryptionKey))
                );

                options.UseSystemNetHttp();

                options.UseAspNetCore();
            });


        // Authentication and Authorization
        services.AddAuthentication(options =>
        {
            options.DefaultScheme = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
        });

        services
            .AddAuthorizationBuilder()
            .AddPolicy("RequireAdminRole", policy => policy.RequireClaim("Role", "Admin"));

        return services;
    }

    public static IServiceCollection AddAppSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    AuthorizationCode = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri("https://localhost:7001/connect/authorize"),
                        TokenUrl = new Uri("https://localhost:7001/connect/token"),
                        Scopes = new Dictionary<string, string>
                        {
                    { "openid", "OpenId" },
                    { "offline_access", "Offline Access" }
                        }
                    }
                }
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "oauth2"
                        }
                    },
                    new List<string> { "read", "write" }
                }
            });
        });

        return services;
    }

    public static IServiceCollection AddAppRequiredServices(this IServiceCollection services)
    {
        services.AddControllers(options =>
            {
                options.Filters.Add<GlobalExceptionFilter>();
            })
            .AddJsonOptions(options =>
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
            );

        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
        });

        services.AddEndpointsApiExplorer();
        services.AddHttpClient();

        return services;
    }
}
