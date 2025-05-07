using System.Text.Json.Serialization;
using Ardalis.GuardClauses;
using CommerceCore.Application;
using CommerceCore.Infrastructure;
using CommerceCore.WebApi.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Validation.AspNetCore;

namespace CommerceCore.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder
            .Services.AddControllers()
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

        builder
            .Services.AddOpenIddict()
            .AddServer(options =>
            {
                options.AddDevelopmentSigningCertificate();
            })
            .AddValidation(options =>
            {
                options.SetIssuer(
                    builder.Configuration["IdentityServer:Authority"]
                        ?? Guard.Against.NullOrWhiteSpace(
                            "IdentityServer:Authority",
                            "IdentityServer Authority is not configured."
                        )
                );

                var encryptionKey =
                    builder.Configuration["IdentityServer:EncryptionKey"]
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

        builder.Services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddHttpClient();

        builder.Services.AddInfrastructureServices(builder.Configuration);
        builder.Services.AddApplicationServices();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseMiddleware<CookieTokenMiddleware>();

        app.UseCors();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
