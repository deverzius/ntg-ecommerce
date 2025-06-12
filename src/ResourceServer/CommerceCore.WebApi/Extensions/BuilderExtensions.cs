using System.Text.Json.Serialization;
using Ardalis.GuardClauses;
using CommerceCore.Application;
using CommerceCore.Infrastructure;
using CommerceCore.WebApi.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OpenIddict.Validation.AspNetCore;

namespace CommerceCore.WebApi.Extensions;

public static class BuilderExtensions
{
    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder
            .Services.AddControllers(options =>
            {
                options.Filters.Add<GlobalExceptionFilter>();
            })
            .AddJsonOptions(options =>
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
            );

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy
                    .WithOrigins("https://localhost:5173", "https://localhost:7136")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
        });

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultScheme = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
        });

        builder
            .Services.AddAuthorizationBuilder()
            .AddPolicy("RequireAdminRole", policy => policy.RequireClaim("Role", "Admin"));

        builder.Services.AddOpenIddictService(builder.Configuration);

        builder.Services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
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

        builder.Services.AddHttpClient();

        builder.Services.AddInfrastructureServices(builder.Configuration);
        builder.Services.AddApplicationServices();
    }

    private static IServiceCollection AddOpenIddictService(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddOpenIddict()
            .AddServer(options => { options.AddDevelopmentSigningCertificate(); })
            .AddValidation(options =>
            {
                options.SetIssuer(
                    configuration["IdentityServer:Authority"]
                    ?? Guard.Against.NullOrWhiteSpace(
                        "IdentityServer:Authority",
                        "IdentityServer Authority is not configured."
                    )
                );

                var encryptionKey =
                    configuration["IdentityServer:EncryptionKey"]
                    ?? Guard.Against.NullOrWhiteSpace(
                        "IdentityServer:EncryptionKey",
                        "IdentityServer EncryptionKey is not configured."
                    );

                options.AddEncryptionKey(
                    new SymmetricSecurityKey(Convert.FromBase64String(encryptionKey))
                );

                options.UseSystemNetHttp();

                options.UseAspNetCore();
            });

        return services;
    }
}
