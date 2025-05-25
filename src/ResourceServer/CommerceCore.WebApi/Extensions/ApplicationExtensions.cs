using CommerceCore.WebApi.Middlewares;

namespace CommerceCore.WebApi.Extensions;

public static class ApplicationExtensions
{
    public static void ConfigureMiddlewares(this WebApplication app)
    {
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
    }
}
