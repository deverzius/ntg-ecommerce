using CommerceCore.IdentityServer.Middlewares;

namespace CommerceCore.IdentityServer.Extensions;

public static class ApplicationExtensions
{
    public static void UseMiddlewares(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseForwardedHeaders();

        app.UseCors();

        app.UseStaticFiles();

        app.UseRouting();

        app.UseMiddleware<CookieTokenMiddleware>();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        app.MapDefaultControllerRoute();

        app.MapRazorPages();
    }
}