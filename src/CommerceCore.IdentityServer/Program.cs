using CommerceCore.IdentityServer.Data;
using CommerceCore.IdentityServer.Extensions;
using CommerceCore.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CommerceCore.Data;

namespace CommerceCore.IdentityServer
{
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
}
