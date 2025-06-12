using CommerceCore.WebApi.Middlewares;

namespace CommerceCore.WebApi.Extensions;

public static class ApplicationExtensions
{
    public static void ConfigureMiddlewares(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.OAuthClientId("ecommerce-client");
                options.OAuthClientSecret("secret");
                options.OAuth2RedirectUrl("https://localhost:7000/swagger/oauth2-redirect.html");
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
