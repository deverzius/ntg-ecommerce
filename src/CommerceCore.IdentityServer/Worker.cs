using CommerceCore.IdentityServer.Data;
using OpenIddict.Abstractions;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace CommerceCore.IdentityServer;

public class Worker : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public Worker(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await using var scope = _serviceProvider.CreateAsyncScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await context.Database.EnsureCreatedAsync();

        var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

        if (await manager.FindByClientIdAsync("commerce") is null)
        {
            // TODO: update seeding data
            await manager.CreateAsync(new OpenIddictApplicationDescriptor
            {
                DisplayName = "ECommerce Client",
                ConsentType = ConsentTypes.Implicit,
                ClientId = "commerce",
                ClientSecret = "secret",
                RedirectUris =
                {
                    new Uri("http://localhost:5256/connect/redirect"),
                    new Uri("https://oauth.pstmn.io/v1/callback")
                },
                PostLogoutRedirectUris =
                {
                    new Uri("http://localhost:5256/callback/login/local")
                },
                Permissions =
                {
                    Permissions.Endpoints.Authorization,
                    Permissions.Endpoints.EndSession,
                    Permissions.Endpoints.Token,
                    Permissions.GrantTypes.AuthorizationCode,
                    Permissions.ResponseTypes.Code,
                    Permissions.Scopes.Email,
                    Permissions.Scopes.Profile,
                    Permissions.Scopes.Roles
                }
            });
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
