using CommerceCore.WebApi.Extensions;

namespace CommerceCore.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.AddServices();

        var app = builder.Build();
        app.ConfigureMiddlewares();
        app.Run();
    }
}