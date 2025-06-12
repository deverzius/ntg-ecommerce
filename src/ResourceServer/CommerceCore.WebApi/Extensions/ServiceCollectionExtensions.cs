using System.Text.Json.Serialization;
using CommerceCore.Application.Common.Configurations;
using CommerceCore.WebAPI.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OpenIddict.Validation.AspNetCore;

namespace CommerceCore.WebAPI.Extensions;

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
        services.Configure<SwaggerConfigurations>(configurationManager.GetSection("Swagger"));

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
        using var scope = services.BuildServiceProvider().CreateScope();
        var serviceProvider = scope.ServiceProvider;

        var swaggerConfigurations = serviceProvider.GetRequiredService<IOptions<SwaggerConfigurations>>().Value;

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc(swaggerConfigurations.Version, new OpenApiInfo
            {
                Title = swaggerConfigurations.Title,
                Version = swaggerConfigurations.Version,
                Description = swaggerConfigurations.Description
            });

            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    AuthorizationCode = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri(swaggerConfigurations.OAuth2.AuthorizationUrl),
                        TokenUrl = new Uri(swaggerConfigurations.OAuth2.TokenUrl),
                        Scopes = swaggerConfigurations.OAuth2.Scopes
                            .Select(scope => new KeyValuePair<string, string>(scope[0], scope[1]))
                            .ToDictionary()
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

    public static IServiceCollection AddWorkers(
       this IServiceCollection services
    )
    {
        services.AddHostedService<Worker>();

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
