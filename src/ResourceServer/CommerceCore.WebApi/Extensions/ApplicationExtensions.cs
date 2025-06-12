using CommerceCore.Application.Common.Configurations;
using CommerceCore.WebAPI.Middlewares;
using Microsoft.Extensions.Options;

namespace CommerceCore.WebAPI.Extensions;

public static class ApplicationExtensions
{
    public static void UseMiddlewares(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            var configuration = app.Services.GetRequiredService<IOptions<SwaggerConfigurations>>().Value;

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.OAuthClientId(configuration.OAuth2.ClientId);
                options.OAuthClientSecret(configuration.OAuth2.ClientSecret);
                options.OAuth2RedirectUrl(configuration.OAuth2.RedirectUrl);
            });
        }

        app.UseHttpsRedirection();

        app.UseMiddleware<CookieTokenMiddleware>();

        app.UseCors();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();
    }
}
