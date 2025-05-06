using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace CommerceCore.Web.CustomersSite;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        builder.Services.AddHttpClient();

        builder
            .Services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(
                CookieAuthenticationDefaults.AuthenticationScheme,
                options =>
                {
                    options.Cookie.SameSite = SameSiteMode.None;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                }
            )
            .AddOpenIdConnect(
                OpenIdConnectDefaults.AuthenticationScheme,
                options =>
                {
                    options.Authority =
                        builder.Configuration["IdentityServer:BaseUrl"]
                        ?? Guard.Against.NullOrEmpty(
                            builder.Configuration["IdentityServer:BaseUrl"]
                        );

                    options.ClientId =
                        builder.Configuration["Identity:ClientId"]
                        ?? Guard.Against.NullOrEmpty(builder.Configuration["Identity:ClientId"]);

                    options.ClientSecret =
                        builder.Configuration["Identity:Secret"]
                        ?? Guard.Against.NullOrEmpty(builder.Configuration["Identity:Secret"]);

                    options.ResponseType = OpenIdConnectResponseType.Code;
                    options.SaveTokens = true;

                    options.Scope.Clear();
                    options.Scope.Add("openid profile offline_access");

                    options.CallbackPath = "/callback";
                    options.SignedOutCallbackPath = "/SignoutCallback";

                    options.SkipUnrecognizedRequests = true;

                    options.Events = new OpenIdConnectEvents
                    {
                        OnTokenResponseReceived = context =>
                        {
                            var accessToken = context.TokenEndpointResponse.AccessToken;
                            var refreshToken = context.TokenEndpointResponse.RefreshToken;

                            int.TryParse(
                                context.TokenEndpointResponse.ExpiresIn,
                                out var expiresIn
                            );

                            var cookieOptions = new CookieOptions
                            {
                                MaxAge = TimeSpan.FromSeconds(expiresIn),
                                HttpOnly = false,
                                Secure = true,
                            };

                            context.Response.Cookies.Append(
                                "access_token",
                                accessToken,
                                cookieOptions
                            );
                            context.Response.Cookies.Append(
                                "refresh_token",
                                accessToken,
                                cookieOptions
                            );

                            return Task.CompletedTask;
                        },
                        OnSignedOutCallbackRedirect = async (context) =>
                        {
                            if (context.HttpContext.Response.HasStarted)
                            {
                                context.HttpContext.Response.Clear();
                            }

                            context.HttpContext.Response.Headers.CacheControl =
                                "no-cache, no-store";
                            context.HttpContext.Response.Headers.Pragma = "no-cache";
                            context.HttpContext.Response.Headers.Expires = "-1";

                            await context.HttpContext.SignOutAsync(
                                CookieAuthenticationDefaults.AuthenticationScheme
                            );

                            context.Response.Redirect("/");
                            context.HandleResponse();
                            await Task.CompletedTask;
                        },
                    };
                }
            );

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
