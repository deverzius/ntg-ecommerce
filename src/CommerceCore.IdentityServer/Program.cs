using CommerceCore.IdentityServer.Extensions;

namespace CommerceCore.IdentityServer;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.ConfigureServices();

        var app = builder.Build();

        app.ConfigureMiddlewares();
        app.Run();
    }
}
