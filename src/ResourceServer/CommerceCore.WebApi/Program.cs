using CommerceCore.Application;
using CommerceCore.Infrastructure;

namespace CommerceCore.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services
            .AddAppConfigurations(builder.Configuration)
            .AddAppSecurity()
            .AddAppSwagger()
            .AddAppRequiredServices();

        builder.Services
            .AddApplicationServices()
            .AddInfrastructureServices();

        var app = builder.Build();
        app.ConfigureMiddlewares();
        app.Run();
    }
}