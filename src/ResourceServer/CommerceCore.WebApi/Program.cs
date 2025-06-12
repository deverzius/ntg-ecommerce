using CommerceCore.Application;
using CommerceCore.Infrastructure;

namespace CommerceCore.WebAPI;

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
        app.UseMiddlewares();
        app.Run();
    }
}