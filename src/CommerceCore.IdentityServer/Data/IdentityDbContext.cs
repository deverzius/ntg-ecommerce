using CommerceCore.IdentityServer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CommerceCore.IdentityServer.Data;

public class IdentityDbContext(DbContextOptions<IdentityDbContext> options)
    : IdentityDbContext<AppUser>(options)
{
}