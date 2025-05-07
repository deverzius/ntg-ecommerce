using CommerceCore.IdentityServer.Data;
using CommerceCore.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using OpenIddict.Abstractions;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace CommerceCore.IdentityServer;

public class Worker(IServiceProvider serviceProvider) : IHostedService
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await using var scope = _serviceProvider.CreateAsyncScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await context.Database.EnsureCreatedAsync(cancellationToken);

        var openIddictManager =
            scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

        if (
            await openIddictManager.FindByClientIdAsync("ecommerce-client", cancellationToken)
            is null
        )
        {
            await openIddictManager.CreateAsync(
                new OpenIddictApplicationDescriptor
                {
                    DisplayName = "ECommerce Client",
                    ConsentType = ConsentTypes.Explicit,
                    ClientId = "ecommerce-client",
                    ClientSecret = "secret",
                    RedirectUris =
                    {
                        new Uri("https://oauth.pstmn.io/v1/callback"),
                        new Uri("https://localhost:5173/callback"),
                        new Uri("https://localhost:7136/callback"),
                    },
                    PostLogoutRedirectUris =
                    {
                        new Uri("https://localhost:5173"),
                        new Uri("https://localhost:7136/SignoutCallback"),
                    },
                    Permissions =
                    {
                        Permissions.Endpoints.Authorization,
                        Permissions.Endpoints.EndSession,
                        Permissions.Endpoints.Token,
                        Permissions.GrantTypes.AuthorizationCode,
                        Permissions.GrantTypes.RefreshToken,
                        Permissions.ResponseTypes.Code,
                        Permissions.Scopes.Email,
                        Permissions.Scopes.Profile,
                        Permissions.Scopes.Roles,
                    },
                },
                cancellationToken
            );
        }

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        if (await roleManager.RoleExistsAsync("Admin") is false)
        {
            await roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
            await roleManager.CreateAsync(new IdentityRole { Name = "User" });
            await userManager.CreateAsync(
                new ApplicationUser
                {
                    UserName = "admin@nashmail.com",
                    Email = "admin@nashmail.com",
                },
                "Admin123!"
            );
            var adminUser = await userManager.FindByEmailAsync("admin@nashmail.com");
            await userManager.AddToRoleAsync(adminUser!, "Admin");
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
