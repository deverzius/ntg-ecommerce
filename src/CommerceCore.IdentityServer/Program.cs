using CommerceCore.IdentityServer.Extensions;

namespace CommerceCore.IdentityServer;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services
            .AddAppConfigurations(builder.Configuration)
            .AddAppDbContext()
            .AddAppIdentity()
            .AddAppSecurity()
            .AddAppRequiredServices()
            .AddWorkers();

        var app = builder.Build();
        app.Configure();
        app.Run();
    }
}